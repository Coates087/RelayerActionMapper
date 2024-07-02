Imports System.IO
Imports System.Text.Json
Imports System.Reflection
Imports RelayerActionMapper.ButtonItems
Imports System.Text.Json.Nodes

'' Add toggle for when the user only wants to set keyboard and mouse, gamepad or both
'' Add form for warning use about gamepad only mode. Using built-in message dialog box isn't that great
Public Class frmAddUpdateControls
    Public assemblyName As String = String.Empty
    Public gStrOpenFileName As String = String.Empty
    Public gControls As GameControls = Nothing

    Public gSaveControls As GameControls = Nothing
    Public fileResourceList As New List(Of String)

    Public imageStreamList As New List(Of ImageName)
    Public gamepadOnlyFirstTime As Boolean = True
    Public gamepadOnly As Boolean = False

    Private Const conDlbQuote As String = Chr(34)
    Private Const gamepadOnlyWarningText As String = "You have selected "“Edit for Controller Only”". This mode is intended for the Controller Button Prompts mod on Nexus Mods and Game Banana. This mode will override all changes made for keyboard and mouse controls. Do you wish to use this mode?"

    Public gPlatform As Platform = Platform.Xbox_btns

    Private buttonStringBytes As ButtonStrings = New ButtonStrings

    Public Class ImageName
        Public FileName As String = String.Empty
        Public ImageData As Bitmap = Nothing
        Public ToolTipText As String = String.Empty
    End Class

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        gControls = Nothing
        Me.Close()
    End Sub

    Private Sub frmAddUpdateControls_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        btnClose.DialogResult = DialogResult.Cancel
        btnSave.DialogResult = DialogResult.OK
        lblFile.Text = ""
        If Not String.IsNullOrWhiteSpace(gStrOpenFileName) Then
            lblFile.Text = "Reading from: " & Path.GetFileName(gStrOpenFileName)
        End If
        ShowXboxControls()
        HideKeyboardControls()
        LoadForm()
        SetToolTips()
    End Sub


    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click

        Dim saveControls As New GameControls
        saveControls = PrepSaveControls()
        gSaveControls = saveControls
        Me.Close()
    End Sub
    Private Sub rbnController_CheckedChanged(sender As Object, e As EventArgs) Handles rbnController.CheckedChanged

        If rbnController.Checked Then
            If gamepadOnlyFirstTime Then

                Dim myWarning As New frmControllerDialog

                Dim myResult = myWarning.ShowDialog()

                If myResult = vbYes Then
                    gamepadOnlyFirstTime = False
                    ForceViewGamePadOnly()
                    DisableViewControls()
                Else
                    rbnEditAll.Checked = True
                    EnableViewControls()
                End If
            Else
                ForceViewGamePadOnly()
                DisableViewControls()
            End If
            gamepadOnly = True
        Else
            gamepadOnly = False
            EnableViewControls()
        End If
    End Sub

    Private Sub rbnEditAll_CheckedChanged(sender As Object, e As EventArgs) Handles rbnEditAll.CheckedChanged
        If rbnEditAll.Checked Then
            EnableViewControls()
        End If
    End Sub

    Private Sub rbnViewGamePad_CheckedChanged(sender As Object, e As EventArgs) Handles rbnViewGamePad.CheckedChanged
        If rbnViewGamePad.Checked Then
            ShowXboxControls()
            HideKeyboardControls()
        End If
    End Sub

    Private Sub rbnViewKeyboard_CheckedChanged(sender As Object, e As EventArgs) Handles rbnViewKeyboard.CheckedChanged
        If rbnViewKeyboard.Checked Then
            ShowKeyboardControls()
            HideXboxControls()
        End If
    End Sub


    Private Sub cboMouseOverScroll(sender As Object, e As EventArgs)
        '' See here for solution for accidental mousewheel scrolling when the drop down isn't open:
        '' https://stackoverflow.com/a/66100313

        Dim myCbo As ComboBox = DirectCast(sender, ComboBox)

        If myCbo.DroppedDown = False Then
            DirectCast(e, HandledMouseEventArgs).Handled = True
        End If
    End Sub

#Region "Keyboard Mapping"

    Private Sub btnKeyStart_Click(sender As Object, e As EventArgs) Handles btnKeyStart.Click
        Dim strFile As String = "Xbox_start_button"

        If gPlatform = Platform.PS_btns Then
            strFile = "PS_Options"
        End If
        ShowKeyboardDialog(strFile, LoadToolTipText(strFile), gControls.Escape)
    End Sub

    Private Sub btnKeyBack_Click(sender As Object, e As EventArgs) Handles btnKeyBack.Click
        Dim strFile As String = "Xbox_back_button"
        If gPlatform = Platform.PS_btns Then
            strFile = "PS_Share"
        End If
        ShowKeyboardDialog(strFile, LoadToolTipText(strFile), gControls.V)
    End Sub

    Private Sub btnKeyA_Click(sender As Object, e As EventArgs) Handles btnKeyA.Click
        Dim strFile As String = "Xbox_A_button"
        If gPlatform = Platform.PS_btns Then
            strFile = "Cross"
        End If
        ShowKeyboardDialog(strFile, LoadToolTipText(strFile), gControls.Enter)
    End Sub

    Private Sub btnKeyB_Click(sender As Object, e As EventArgs) Handles btnKeyB.Click
        Dim strFile As String = "Xbox_B_button"
        If gPlatform = Platform.PS_btns Then
            strFile = "Circle"
        End If
        ShowKeyboardDialog(strFile, LoadToolTipText(strFile), gControls.Backspace)
    End Sub

    Private Sub btnKeyX_Click(sender As Object, e As EventArgs) Handles btnKeyX.Click
        Dim strFile As String = "Xbox_X_button"
        If gPlatform = Platform.PS_btns Then
            strFile = "Square"
        End If
        ShowKeyboardDialog(strFile, LoadToolTipText(strFile), gControls.Tab)
    End Sub

    Private Sub btnKeyY_Click(sender As Object, e As EventArgs) Handles btnKeyY.Click
        Dim strFile As String = "Xbox_Y_button"
        If gPlatform = Platform.PS_btns Then
            strFile = "Triangle"
        End If
        ShowKeyboardDialog(strFile, LoadToolTipText(strFile), gControls.Shift)
    End Sub

    Private Sub btnKeyLB_Click(sender As Object, e As EventArgs) Handles btnKeyLB.Click
        Dim strFile As String = "Icon_BtnXbox_LB"
        If gPlatform = Platform.PS_btns Then
            strFile = "PS_L1"
        End If
        ShowKeyboardDialog(strFile, LoadToolTipText(strFile), gControls.Q)
    End Sub

    Private Sub btnKeyRB_Click(sender As Object, e As EventArgs) Handles btnKeyRB.Click
        Dim strFile As String = "Icon_BtnXbox_RB"
        If gPlatform = Platform.PS_btns Then
            strFile = "PS_R1"
        End If
        ShowKeyboardDialog(strFile, LoadToolTipText(strFile), gControls.R)
    End Sub

    Private Sub btnKeyLT_Click(sender As Object, e As EventArgs) Handles btnKeyLT.Click
        Dim strFile As String = "Icon_BtnXbox_LT"
        If gPlatform = Platform.PS_btns Then
            strFile = "PS_L2"
        End If
        ShowKeyboardDialog(strFile, LoadToolTipText(strFile), gControls.WheelUp)
    End Sub

    Private Sub btnKeyRT_Click(sender As Object, e As EventArgs) Handles btnKeyRT.Click
        Dim strFile As String = "Icon_BtnXbox_RT"
        If gPlatform = Platform.PS_btns Then
            strFile = "PS_R2"
        End If
        ShowKeyboardDialog(strFile, LoadToolTipText(strFile), gControls.WheelDown)
    End Sub

    Private Sub btnKey_dUp_Click(sender As Object, e As EventArgs) Handles btnKey_dUp.Click
        Dim strFile As String = "Icon_BtnXbox_dpad_up"
        If gPlatform = Platform.PS_btns Then
            strFile = "PS_dpad_up"
        End If
        ShowKeyboardDialog(strFile, LoadToolTipText(strFile), gControls.W)
    End Sub

    Private Sub btnKey_dRight_Click(sender As Object, e As EventArgs) Handles btnKey_dRight.Click
        Dim strFile As String = "Icon_BtnXbox_dpad_right"
        If gPlatform = Platform.PS_btns Then
            strFile = "PS_dpad_right"
        End If
        ShowKeyboardDialog(strFile, LoadToolTipText(strFile), gControls.D)
    End Sub

    Private Sub btnKey_dLeft_Click(sender As Object, e As EventArgs) Handles btnKey_dLeft.Click
        Dim strFile As String = "Icon_BtnXbox_dpad_left"
        If gPlatform = Platform.PS_btns Then
            strFile = "PS_dpad_left"
        End If
        ShowKeyboardDialog(strFile, LoadToolTipText(strFile), gControls.A)
    End Sub

    Private Sub btnKey_dDown_Click(sender As Object, e As EventArgs) Handles btnKey_dDown.Click
        Dim strFile As String = "Icon_BtnXbox_dpad_down"
        If gPlatform = Platform.PS_btns Then
            strFile = "PS_dpad_down"
        End If
        ShowKeyboardDialog(strFile, LoadToolTipText(strFile), gControls.S)
    End Sub

    Private Sub btnKeyLsMod_Click(sender As Object, e As EventArgs) Handles btnKeyLsMod.Click
        Dim strFile As String = "Xbox_L_Sticks"
        ShowKeyboardDialog(strFile, gControls.Ctrl)
    End Sub

    Private Sub btnKeyLsButton_Click(sender As Object, e As EventArgs) Handles btnKeyLsButton.Click
        Dim strFile As String = "Xbox_L_StickClick"
        If gPlatform = Platform.PS_btns Then
            strFile = "PS_L3"
        End If
        ShowKeyboardDialog(strFile, LoadToolTipText(strFile), gControls.F)
    End Sub

    Private Sub btnKeyRsUp_Click(sender As Object, e As EventArgs) Handles btnKeyRsUp.Click
        Dim strFile As String = "Xbox_R_Sticks_up"
        ShowKeyboardDialog(strFile, LoadToolTipText(strFile), gControls.UpArrow)
    End Sub

    Private Sub btnKeyRsDown_Click(sender As Object, e As EventArgs) Handles btnKeyRsDown.Click
        Dim strFile As String = "Xbox_R_Sticks_down"
        ShowKeyboardDialog(strFile, LoadToolTipText(strFile), gControls.DownArrow)
    End Sub

    Private Sub btnKeyRsLeft_Click(sender As Object, e As EventArgs) Handles btnKeyRsLeft.Click
        Dim strFile As String = "Xbox_R_Sticks_left"
        ShowKeyboardDialog(strFile, LoadToolTipText(strFile), gControls.LeftArrow)
    End Sub

    Private Sub btnKeyRsRight_Click(sender As Object, e As EventArgs) Handles btnKeyRsRight.Click
        Dim strFile As String = "Xbox_R_Sticks_right"
        ShowKeyboardDialog(strFile, LoadToolTipText(strFile), gControls.RightArrow)
    End Sub

    Private Sub btnKeyRsButton_Click(sender As Object, e As EventArgs) Handles btnKeyRsButton.Click
        Dim strFile As String = "Xbox_R_StickClick"
        If gPlatform = Platform.PS_btns Then
            strFile = "PS_R3"
        End If
        ShowKeyboardDialog(strFile, LoadToolTipText(strFile), gControls.R)
    End Sub

#End Region

    Private Sub ShowKeyboardDialog(xboxButtonName As String, toolTipText As String, aButton As GenericKey)
        Dim selectedImage = imageStreamList.Where(Function(e) e.FileName = xboxButtonName).FirstOrDefault()

        If Not IsNothing(selectedImage) Then
            Dim myControlForm As New frmControlWindow
            myControlForm.gImageData = selectedImage.ImageData
            myControlForm.gControls = gControls
            myControlForm.gButton = aButton
            myControlForm.toolTipText = toolTipText
            Dim dialogResults = myControlForm.ShowDialog()

            If dialogResults = DialogResult.OK Then
                aButton.KeyCode = myControlForm.gStrKeys
                MapValues()
            End If

            SetToolTips()
        End If
    End Sub

    Private Sub ShowKeyboardDialog(xboxButtonName As String, aKeyList As GenericKeyCode)
        Dim selectedImage = imageStreamList.Where(Function(e) e.FileName = xboxButtonName).FirstOrDefault()

        If Not IsNothing(selectedImage) Then
            Dim myControlForm As New frmControlWindow
            myControlForm.gImageData = selectedImage.ImageData
            myControlForm.gControls = gControls
            myControlForm.gKeyList = aKeyList
            myControlForm.gButton = Nothing

            Dim dialogResults = myControlForm.ShowDialog()

            If dialogResults = DialogResult.OK Then
                aKeyList.KeyCode = myControlForm.gStrKeys
                MapValues()
            End If

            SetToolTips()
        End If
    End Sub

    Private Sub ForceViewGamePadOnly()
        rbnViewGamePad.Checked = True
    End Sub

    Private Sub DisableViewControls()
        rbnViewKeyboard.Enabled = False
        rbnViewGamePad.Enabled = False
    End Sub

    Private Sub EnableViewControls()
        rbnViewKeyboard.Enabled = True
        rbnViewGamePad.Enabled = True
    End Sub

    Private Sub HideXboxControls()

        Dim myControls As List(Of Control) = gboxFields.Controls.Cast(Of Control)().Where(Function(e) {"XboxLabel", "XboxCBox"}.Contains(e.Tag?.ToString)).ToList
        HideControls(myControls)
    End Sub

    Private Sub HideKeyboardControls()

        Dim myControls As List(Of Control) = gboxFields.Controls.Cast(Of Control)().Where(Function(e) {"KeyLabel", "KeyButton"}.Contains(e.Tag?.ToString)).ToList
        HideControls(myControls)
        'picBoxLsMod.Visible = False
    End Sub

    Private Sub ShowXboxControls()

        Dim myControls As List(Of Control) = gboxFields.Controls.Cast(Of Control)().Where(Function(e) {"XboxLabel", "XboxCBox"}.Contains(e.Tag?.ToString)).ToList
        ShowControls(myControls)
    End Sub

    Private Sub ShowKeyboardControls()

        Dim myControls As List(Of Control) = gboxFields.Controls.Cast(Of Control)().Where(Function(e) {"KeyLabel", "KeyButton"}.Contains(e.Tag?.ToString)).ToList
        ShowControls(myControls)
        'picBoxLsMod.Visible = True
    End Sub

    Private Sub HideControls(myControls As IEnumerable(Of Control))
        For Each aControl In myControls
            aControl.Visible = False
        Next
    End Sub

    Private Sub ShowControls(myControls As IEnumerable(Of Control))
        For Each aControl In myControls
            aControl.Visible = True
        Next
    End Sub

    Private Sub SetToolTips()
        Dim myControls As List(Of Control) = gboxFields.Controls.Cast(Of Control)().Where(Function(e) {"KeyLabel"}.Contains(e.Tag?.ToString)).ToList

        For Each aLabelControl In myControls
            'aLabelControl.too
            tip1.SetToolTip(aLabelControl, aLabelControl.Text)
        Next
        ' tip1.Sho
    End Sub


    Public Sub LoadForm()

        Dim PS_List = New List(Of String) From {
            "Circle",
            "Cross",
            "PS_L1",
            "PS_L2",
            "PS_L3",
            "PS_Options",
            "PS_R1",
            "PS_R2",
            "PS_R3",
            "PS_Share",
            "Square",
            "Triangle",
            "PS_dpad_down",
            "PS_dpad_left",
            "PS_dpad_right",
            "PS_dpad_up",
            "Xbox_R_Sticks_down",
            "Xbox_R_Sticks_left",
            "Xbox_R_Sticks_right",
            "Xbox_R_Sticks_up",
            "Xbox_L_Sticks",
            "Xbox_L_Sticks_down",
            "Xbox_L_Sticks_left",
            "Xbox_L_Sticks_right",
            "Xbox_L_Sticks_up"
        }


        Dim Xbox_List = New List(Of String) From {
        "Icon_BtnXbox_dpad_down",
        "Icon_BtnXbox_dpad_left",
        "Icon_BtnXbox_dpad_right",
        "Icon_BtnXbox_dpad_up",
        "Icon_BtnXbox_LB",
        "Icon_BtnXbox_LT",
        "Icon_BtnXbox_RB",
        "Icon_BtnXbox_RT",
        "Xbox_A_button",
        "Xbox_back_button",
        "Xbox_B_button",
        "Xbox_L_StickClick",
        "Xbox_L_Sticks",
        "Xbox_L_Sticks_down",
        "Xbox_L_Sticks_left",
        "Xbox_L_Sticks_right",
        "Xbox_L_Sticks_up",
        "Xbox_menu_button",
        "Xbox_R_StickClick",
        "Xbox_R_Sticks_down",
        "Xbox_R_Sticks_left",
        "Xbox_R_Sticks_right",
        "Xbox_R_Sticks_up",
        "Xbox_start_button",
        "Xbox_view_button",
        "Xbox_X_button",
        "Xbox_Y_button"
        }

        If gPlatform = Platform.PS_btns Then
            fileResourceList.AddRange(PS_List)
        Else
            fileResourceList.AddRange(Xbox_List)

        End If

        imageStreamList = New List(Of ImageName)

        For Each strFile As String In fileResourceList

            Dim imageResult As Bitmap = LoadImage(strFile)
            Dim tooTipText As String = LoadToolTipText(strFile)
            imageStreamList.Add(New ImageName With {.FileName = strFile, .ImageData = imageResult, .ToolTipText = tooTipText})
        Next


        For Each imageStream As ImageName In imageStreamList
            If Not IsNothing(imageStream.ImageData) Then
                If {"Xbox_start_button", "PS_Options"}.Contains(imageStream.FileName) Then
                    picBoxStart.Image = imageStream.ImageData
                    tip1.SetToolTip(picBoxStart, imageStream.ToolTipText)
                ElseIf {"Xbox_back_button", "PS_Share"}.Contains(imageStream.FileName) Then
                    picBoxBack.Image = imageStream.ImageData
                    tip1.SetToolTip(picBoxBack, imageStream.ToolTipText)
                ElseIf {"Xbox_A_button", "Cross"}.Contains(imageStream.FileName) Then
                    picBoxA.Image = imageStream.ImageData
                    tip1.SetToolTip(picBoxA, imageStream.ToolTipText)
                ElseIf {"Xbox_B_button", "Circle"}.Contains(imageStream.FileName) Then
                    picBoxB.Image = imageStream.ImageData
                    tip1.SetToolTip(picBoxB, imageStream.ToolTipText)
                ElseIf {"Xbox_X_button", "Square"}.Contains(imageStream.FileName) Then
                    picBoxX.Image = imageStream.ImageData
                    tip1.SetToolTip(picBoxX, imageStream.ToolTipText)
                ElseIf {"Xbox_Y_button", "Triangle"}.Contains(imageStream.FileName) Then
                    picBoxY.Image = imageStream.ImageData
                    tip1.SetToolTip(picBoxY, imageStream.ToolTipText)
                ElseIf {"Icon_BtnXbox_LB", "PS_L1"}.Contains(imageStream.FileName) Then
                    picBoxLB.Image = imageStream.ImageData
                    tip1.SetToolTip(picBoxLB, imageStream.ToolTipText)
                ElseIf {"Icon_BtnXbox_RB", "PS_R1"}.Contains(imageStream.FileName) Then
                    picBoxRB.Image = imageStream.ImageData
                    tip1.SetToolTip(picBoxRB, imageStream.ToolTipText)
                ElseIf {"Icon_BtnXbox_LT", "PS_L2"}.Contains(imageStream.FileName) Then
                    picBoxLT.Image = imageStream.ImageData
                    tip1.SetToolTip(picBoxLT, imageStream.ToolTipText)
                ElseIf {"Icon_BtnXbox_RT", "PS_R2"}.Contains(imageStream.FileName) Then
                    picBoxRT.Image = imageStream.ImageData
                    tip1.SetToolTip(picBoxRT, imageStream.ToolTipText)
                    ''dpad
                ElseIf {"Icon_BtnXbox_dpad_up", "PS_dpad_up"}.Contains(imageStream.FileName) Then
                    picBox_dUp.Image = imageStream.ImageData
                    tip1.SetToolTip(picBox_dUp, imageStream.ToolTipText)
                ElseIf {"Icon_BtnXbox_dpad_down", "PS_dpad_down"}.Contains(imageStream.FileName) Then
                    picBox_dDown.Image = imageStream.ImageData
                    tip1.SetToolTip(picBox_dDown, imageStream.ToolTipText)
                ElseIf {"Icon_BtnXbox_dpad_left", "PS_dpad_left"}.Contains(imageStream.FileName) Then
                    picBox_dLeft.Image = imageStream.ImageData
                    tip1.SetToolTip(picBox_dLeft, imageStream.ToolTipText)
                ElseIf {"Icon_BtnXbox_dpad_right", "PS_dpad_right"}.Contains(imageStream.FileName) Then
                    picBox_dRight.Image = imageStream.ImageData
                    tip1.SetToolTip(picBox_dRight, imageStream.ToolTipText)

                    ''left stick
                ElseIf imageStream.FileName = "Xbox_L_Sticks" Then
                    picBoxLsMod.Image = imageStream.ImageData
                    tip1.SetToolTip(picBoxLsMod, imageStream.ToolTipText)
                ElseIf imageStream.FileName = "Xbox_L_Sticks_up" Then
                    picBoxLsUp.Image = imageStream.ImageData
                    tip1.SetToolTip(picBoxLsUp, imageStream.ToolTipText)
                ElseIf imageStream.FileName = "Xbox_L_Sticks_down" Then
                    picBoxLsDown.Image = imageStream.ImageData
                    tip1.SetToolTip(picBoxLsDown, imageStream.ToolTipText)
                ElseIf imageStream.FileName = "Xbox_L_Sticks_left" Then
                    picBoxLsLeft.Image = imageStream.ImageData
                    tip1.SetToolTip(picBoxLsLeft, imageStream.ToolTipText)
                ElseIf imageStream.FileName = "Xbox_L_Sticks_right" Then
                    picBoxLsRight.Image = imageStream.ImageData
                    tip1.SetToolTip(picBoxLsRight, imageStream.ToolTipText)
                ElseIf {"Xbox_L_StickClick", "PS_L3"}.Contains(imageStream.FileName) Then
                    picBoxLsButton.Image = imageStream.ImageData
                    tip1.SetToolTip(picBoxLsButton, imageStream.ToolTipText)

                    ''right stick
                ElseIf imageStream.FileName = "Xbox_R_Sticks_up" Then
                    picBoxRsUp.Image = imageStream.ImageData
                    tip1.SetToolTip(picBoxRsUp, imageStream.ToolTipText)
                ElseIf imageStream.FileName = "Xbox_R_Sticks_down" Then
                    picBoxRsDown.Image = imageStream.ImageData
                    tip1.SetToolTip(picBoxRsDown, imageStream.ToolTipText)
                ElseIf imageStream.FileName = "Xbox_R_Sticks_left" Then
                    picBoxRsLeft.Image = imageStream.ImageData
                    tip1.SetToolTip(picBoxRsLeft, imageStream.ToolTipText)
                ElseIf imageStream.FileName = "Xbox_R_Sticks_right" Then
                    picBoxRsRight.Image = imageStream.ImageData
                    tip1.SetToolTip(picBoxRsRight, imageStream.ToolTipText)
                ElseIf {"Xbox_R_StickClick", "PS_R3"}.Contains(imageStream.FileName) Then
                    picBoxRsButton.Image = imageStream.ImageData
                    tip1.SetToolTip(picBoxRsButton, imageStream.ToolTipText)
                End If
            End If
        Next

        Dim xboxButtonList As New List(Of XboxButton)

        xboxButtonList.Add(New XboxButton With {.XboxButtonName = "", .XboxButtonValue = ""})

        If gPlatform = Platform.PS_btns Then
            xboxButtonList.AddRange(GetPS_Buttons())
        Else
            xboxButtonList.AddRange(GetXboxButtons())
        End If

        Dim myControls = gboxFields.Controls.Cast(Of Control)().Where(Function(e) e.GetType = GetType(ComboBox) And e.Tag?.ToString = "XboxCBox").ToList
        Dim xboxCboxes As New List(Of ComboBox)
        xboxCboxes = myControls.ConvertAll(Of ComboBox)(Function(t As Control) DirectCast(t, ComboBox))
        For Each xboxCBox In xboxCboxes
            xboxCBox.DisplayMember = "XboxButtonName"
            xboxCBox.ValueMember = "XboxButtonValue"
            xboxCBox.DataSource = New List(Of XboxButton)(xboxButtonList)
            ''cboMouseOverScroll
            AddHandler xboxCBox.MouseWheel, AddressOf cboMouseOverScroll
        Next
        MapValues()
    End Sub


    Public Sub MapValues()
        If Not IsNothing(gControls) Then
            cboStart.SelectedValue = gControls.Escape.ButtonName
            cboBack.SelectedValue = gControls.V.ButtonName
            cboA.SelectedValue = gControls.Enter.ButtonName
            cboB.SelectedValue = gControls.Backspace.ButtonName
            cboX.SelectedValue = gControls.Tab.ButtonName
            cboY.SelectedValue = gControls.Shift.ButtonName

            cboLB.SelectedValue = gControls.Q.ButtonName
            cboRB.SelectedValue = gControls.E.ButtonName
            cboLT.SelectedValue = gControls.WheelUp.ButtonName
            cboRT.SelectedValue = gControls.WheelDown.ButtonName

            cbodUp.SelectedValue = gControls.W.ButtonName
            cbodDown.SelectedValue = gControls.S.ButtonName
            cbodLeft.SelectedValue = gControls.A.ButtonName
            cbodRight.SelectedValue = gControls.D.ButtonName

            cboLsUp.SelectedValue = gControls.CtrlW.ButtonName
            cboLsDown.SelectedValue = gControls.CtrlS.ButtonName
            cboLsLeft.SelectedValue = gControls.CtrlA.ButtonName
            cboLsRight.SelectedValue = gControls.CtrlD.ButtonName
            cboLsButton.SelectedValue = gControls.F.ButtonName

            cboRsUp.SelectedValue = gControls.UpArrow.ButtonName
            cboRsDown.SelectedValue = gControls.DownArrow.ButtonName
            cboRsLeft.SelectedValue = gControls.LeftArrow.ButtonName
            cboRsRight.SelectedValue = gControls.RightArrow.ButtonName
            cboRsButton.SelectedValue = gControls.R.ButtonName

            lblStart.Text = String.Join(", ", gControls.Escape.KeyCode)
            lblBack.Text = String.Join(", ", gControls.V.KeyCode)
            lblA.Text = String.Join(", ", gControls.Enter.KeyCode)
            lblB.Text = String.Join(", ", gControls.Backspace.KeyCode)
            lblX.Text = String.Join(", ", gControls.Tab.KeyCode)
            lblY.Text = String.Join(", ", gControls.Shift.KeyCode)

            lblLB.Text = String.Join(", ", gControls.Q.KeyCode)
            lblRB.Text = String.Join(", ", gControls.E.KeyCode)
            lblLT.Text = String.Join(", ", gControls.WheelUp.KeyCode)
            lblRT.Text = String.Join(", ", gControls.WheelDown.KeyCode)

            lbl_dUp.Text = String.Join(", ", gControls.W.KeyCode)
            lbl_dDown.Text = String.Join(", ", gControls.S.KeyCode)
            lbl_dLeft.Text = String.Join(", ", gControls.A.KeyCode)
            lbl_dRight.Text = String.Join(", ", gControls.D.KeyCode)

            lbl_dUp.Text = String.Join(", ", gControls.W.KeyCode)
            lbl_dDown.Text = String.Join(", ", gControls.S.KeyCode)
            lbl_dLeft.Text = String.Join(", ", gControls.A.KeyCode)
            lbl_dRight.Text = String.Join(", ", gControls.D.KeyCode)


            lblLsMod.Text = String.Join(", ", gControls.Ctrl.KeyCode)

            lblLsUp.Text = $"[{lblLsMod.Text}] + [{lbl_dUp.Text}]"
            lblLsDown.Text = $"[{lblLsMod.Text}] + [{lbl_dDown.Text}]"
            lblLsLeft.Text = $"[{lblLsMod.Text}] + [{lbl_dLeft.Text}]"
            lblLsRight.Text = $"[{lblLsMod.Text}] + [{lbl_dRight.Text}]"

            lblLsButton.Text = String.Join(", ", gControls.F.KeyCode)


            lblRsUp.Text = String.Join(", ", gControls.UpArrow.KeyCode)
            lblRsDown.Text = String.Join(", ", gControls.DownArrow.KeyCode)
            lblRsLeft.Text = String.Join(", ", gControls.LeftArrow.KeyCode)
            lblRsRight.Text = String.Join(", ", gControls.RightArrow.KeyCode)
            lblRsButton.Text = String.Join(", ", gControls.R.KeyCode)
        End If
    End Sub

    'Public Function LoadImage(strFile As String) As Bitmap
    '    Dim image As Bitmap = Nothing
    '    Dim byt As Byte() = Nothing
    '    Dim fileString64 As String = ""
    '    Dim fullResourceName As String = assemblyName + "." + strFile

    '    Dim thisAssembly As Assembly = Assembly.GetExecutingAssembly()
    '    Dim obj = thisAssembly.GetManifestResourceNames()
    '    Dim readReflect = thisAssembly.GetManifestResourceStream(fullResourceName)

    '    Dim ms As MemoryStream = New MemoryStream(byt)
    '    If Not IsNothing(ms) Then

    '        image = New Bitmap(ms)
    '    End If
    '    Return image
    'End Function


    Public Function LoadImage(strFile As String) As Bitmap
        Dim image As Bitmap = Nothing
        Dim byt As Byte() = Nothing
        Dim fileString64 As String = ""
        Dim tipMessage As String = ""


        Select Case strFile
            Case "Icon_BtnXbox_dpad_down"
                fileString64 = buttonStringBytes.xbox_dpad_Down
            Case "PS_dpad_down"
                fileString64 = buttonStringBytes.ps_dpad_down
            Case "Icon_BtnXbox_dpad_left"
                fileString64 = buttonStringBytes.xbox_dpad_Left
            Case "PS_dpad_left"
                fileString64 = buttonStringBytes.ps_dpad_left
            Case "Icon_BtnXbox_dpad_right"
                fileString64 = buttonStringBytes.xbox_dpad_Right
            Case "PS_dpad_right"
                fileString64 = buttonStringBytes.ps_dpad_right
            Case "Icon_BtnXbox_dpad_up"
                fileString64 = buttonStringBytes.xbox_dpad_Up
            Case "PS_dpad_up"
                fileString64 = buttonStringBytes.ps_dpad_up

            Case "Icon_BtnXbox_LB"
                fileString64 = buttonStringBytes.xbox_LB
            Case "PS_L1"
                fileString64 = buttonStringBytes.ps_L1
            Case "Icon_BtnXbox_LT"
                fileString64 = buttonStringBytes.xbox_LT
            Case "PS_L2"
                fileString64 = buttonStringBytes.ps_L2
            Case "Icon_BtnXbox_RB"
                fileString64 = buttonStringBytes.xbox_RB
            Case "PS_R1"
                fileString64 = buttonStringBytes.ps_R1
            Case "Icon_BtnXbox_RT"
                fileString64 = buttonStringBytes.xbox_RT
            Case "PS_R2"
                fileString64 = buttonStringBytes.ps_R2

            Case "Xbox_start_button", "Xbox_menu_button"
                fileString64 = buttonStringBytes.xbox_Start
            Case "PS_Options"
                fileString64 = buttonStringBytes.ps_options
            Case "Xbox_back_button", "Xbox_view_button"
                fileString64 = buttonStringBytes.xbox_Back
            Case "PS_Share"
                fileString64 = buttonStringBytes.ps_share

            Case "Xbox_A_button"
                fileString64 = buttonStringBytes.xbox_A
            Case "Cross"
                fileString64 = buttonStringBytes.ps_cross
            Case "Xbox_B_button"
                fileString64 = buttonStringBytes.xbox_B
            Case "Circle"
                fileString64 = buttonStringBytes.ps_circle
            Case "Xbox_X_button"
                fileString64 = buttonStringBytes.xbox_X
            Case "Square"
                fileString64 = buttonStringBytes.ps_square
            Case "Xbox_Y_button"
                fileString64 = buttonStringBytes.xbox_Y
            Case "Triangle"
                fileString64 = buttonStringBytes.ps_triangle

            Case "Xbox_R_Sticks_down"
                fileString64 = buttonStringBytes.xbox_RStick_Down
            Case "Xbox_R_Sticks_left"
                fileString64 = buttonStringBytes.xbox_RStick_Left
            Case "Xbox_R_Sticks_right"
                fileString64 = buttonStringBytes.xbox_RStick_Right
            Case "Xbox_R_Sticks_up"
                fileString64 = buttonStringBytes.xbox_RStick_Up

            Case "Xbox_R_StickClick"
                fileString64 = buttonStringBytes.xbox_RStickClick
            Case "PS_R3"
                fileString64 = buttonStringBytes.ps_R3

            Case "Xbox_L_Sticks"
                fileString64 = buttonStringBytes.xbox_LStick
            Case "Xbox_L_Sticks_down"
                fileString64 = buttonStringBytes.xbox_LStick_Down
            Case "Xbox_L_Sticks_left"
                fileString64 = buttonStringBytes.xbox_LStick_Left
            Case "Xbox_L_Sticks_right"
                fileString64 = buttonStringBytes.xbox_LStick_Right
            Case "Xbox_L_Sticks_up"
                fileString64 = buttonStringBytes.xbox_LStick_Up

            Case "Xbox_L_StickClick"
                fileString64 = buttonStringBytes.xbox_LStickClick
            Case "PS_L3"
                fileString64 = buttonStringBytes.ps_L3
            Case Else
                Return image
        End Select

        byt = Convert.FromBase64String(fileString64)
        Dim ms As MemoryStream = New MemoryStream(byt)
        If Not IsNothing(ms) Then

            image = New Bitmap(ms)
        End If
        Return image
    End Function

    Public Function LoadToolTipText(strFile As String) As String
        Dim tipMessage As String = ""


        Select Case strFile
            Case "Icon_BtnXbox_dpad_down", "PS_dpad_down"
                tipMessage = $"Move cursor down"
            Case "Icon_BtnXbox_dpad_left", "PS_dpad_left"
                tipMessage = $"Move cursor left"
            Case "Icon_BtnXbox_dpad_right", "PS_dpad_right"
                tipMessage = $"Move cursor right"
            Case "Icon_BtnXbox_dpad_up", "PS_dpad_up"
                tipMessage = $"Move cursor up"

            Case "Icon_BtnXbox_LB", "PS_L1"
                tipMessage = $"Switch page/unit left"
            Case "Icon_BtnXbox_LT", "PS_L2"
                tipMessage = $"Zoom Out"
            Case "Icon_BtnXbox_RB", "PS_R1"
                tipMessage = $"Switch page/unit left{vbCrLf}VN Event/Cutscene: Fast Forword"
            Case "Icon_BtnXbox_RT", "PS_R2"
                tipMessage = $"Zoom In"

            Case "Xbox_start_button", "Xbox_menu_button", "PS_Options"
                tipMessage = $"Options Menu: revert to default settings{vbCrLf}Battle Screen: Battle menu{vbCrLf}VN Event/Cutscene: Skip event"
            Case "Xbox_back_button", "Xbox_view_button", "PS_Share"
                tipMessage = $"Battle Screen: Display Stage information"

            Case "Xbox_A_button", "Cross"
                tipMessage = $"Confirm"
            Case "Xbox_B_button", "Circle"
                tipMessage = $"Cancel/Back"
            Case "Xbox_X_button", "Square"
                tipMessage = $"Battle Screen:Display Detailed Info{vbCrLf}VN Event/Cutscene: Backlog{vbCrLf}Shop Screen: Overview{vbCrLf}Star Cube Screen: Overview"
            Case "Xbox_Y_button", "Triangle"
                tipMessage = $"Equipment Screen: Remove skill/equipment{vbCrLf}Battle Screen: Display threat area{vbCrLf}VN Event/Cutscene: Auto-Advance{vbCrLf}Shop Screen: Confirm Purchase"

            Case "Xbox_R_Sticks_down"
                tipMessage = $"Battle Screen: Move camera down{vbCrLf}Star Cube Screen: freely move cursor down"
            Case "Xbox_R_Sticks_left"
                tipMessage = $"Battle Screen: Move camera left{vbCrLf}Star Cube Screen: freely move cursor left"
            Case "Xbox_R_Sticks_right"
                tipMessage = $"Battle Screen: Move camera right{vbCrLf}Star Cube Screen: freely move cursor right"
            Case "Xbox_R_Sticks_up"
                tipMessage = $"Battle Screen: Move camera up{vbCrLf}Star Cube Screen: freely move cursor up"

            Case "Xbox_R_StickClick", "PS_R3"
                tipMessage = $"Battle Screen: Toggle Auto Battle"

            Case "Xbox_L_Sticks_down"
                tipMessage = $"Move cursor down"
            Case "Xbox_L_Sticks_left"
                tipMessage = $"Move cursor left"
            Case "Xbox_L_Sticks_right"
                tipMessage = $"Move cursor right"
            Case "Xbox_L_Sticks_up"
                tipMessage = $"Move cursor up"

            Case "Xbox_L_StickClick", "PS_L3"
                tipMessage = $"Battle Screen: View Aggro List"
        End Select

        Return tipMessage
    End Function

    Public Function PrepSaveControls() As GameControls
        Dim saveControls As New GameControls
        saveControls = gControls

        saveControls.Escape.ButtonName = ReplaceIfNothing(cboStart.SelectedValue, "")
        saveControls.V.ButtonName = ReplaceIfNothing(cboBack.SelectedValue, "")
        saveControls.Enter.ButtonName = ReplaceIfNothing(cboA.SelectedValue, "")
        saveControls.Backspace.ButtonName = ReplaceIfNothing(cboB.SelectedValue, "")
        saveControls.Tab.ButtonName = ReplaceIfNothing(cboX.SelectedValue, "")
        saveControls.Shift.ButtonName = ReplaceIfNothing(cboY.SelectedValue, "")

        saveControls.Q.ButtonName = ReplaceIfNothing(cboLB.SelectedValue, "")
        saveControls.E.ButtonName = ReplaceIfNothing(cboRB.SelectedValue, "")
        saveControls.WheelUp.ButtonName = ReplaceIfNothing(cboLT.SelectedValue, "")
        saveControls.WheelDown.ButtonName = ReplaceIfNothing(cboRT.SelectedValue, "")

        saveControls.W.ButtonName = ReplaceIfNothing(cbodUp.SelectedValue, "")
        saveControls.S.ButtonName = ReplaceIfNothing(cbodDown.SelectedValue, "")
        saveControls.A.ButtonName = ReplaceIfNothing(cbodLeft.SelectedValue, "")
        saveControls.D.ButtonName = ReplaceIfNothing(cbodRight.SelectedValue, "")

        saveControls.CtrlW.ButtonName = ReplaceIfNothing(cboLsUp.SelectedValue, "")
        saveControls.CtrlS.ButtonName = ReplaceIfNothing(cboLsDown.SelectedValue, "")
        saveControls.CtrlA.ButtonName = ReplaceIfNothing(cboLsLeft.SelectedValue, "")
        saveControls.CtrlD.ButtonName = ReplaceIfNothing(cboLsRight.SelectedValue, "")
        saveControls.F.ButtonName = ReplaceIfNothing(cboLsButton.SelectedValue, "")

        saveControls.UpArrow.ButtonName = ReplaceIfNothing(cboRsUp.SelectedValue, "")
        saveControls.DownArrow.ButtonName = ReplaceIfNothing(cboRsDown.SelectedValue, "")
        saveControls.LeftArrow.ButtonName = ReplaceIfNothing(cboRsLeft.SelectedValue, "")
        saveControls.RightArrow.ButtonName = ReplaceIfNothing(cboRsRight.SelectedValue, "")
        saveControls.R.ButtonName = ReplaceIfNothing(cboRsButton.SelectedValue, "")
        saveControls = SetControlsForGamepadOnly(saveControls)
        Return saveControls
    End Function

    Private Function SetControlsForGamepadOnly(saveControls As GameControls) As GameControls
        Dim myControls As New GameControls
        myControls = saveControls
        If gamepadOnly Then
            Dim myOptions As New JsonSerializerOptions
            myOptions.WriteIndented = True
            myOptions.Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping
            Dim root = JsonNode.Parse(JsonSerializer.Serialize(myControls, myOptions)).AsObject()
            Dim keyActions As List(Of String) = GetKeyActionNameList()

            For Each aKey In keyActions
                Dim aKeyList As New List(Of String)
                If aKey = "Ctrl" Then
                    root.Item(aKey).Item("KeyCode").AsArray().Clear()
                    root.Item(aKey).Item("KeyCode").AsArray().Add("LeftControl")
                Else
                    root.Item(aKey).Item("KeyCode").AsArray().Clear()
                    Dim result As String = String.Empty

                    If gPlatform = Platform.PS_btns Then
                        result = GetRightKey_PS(root.Item(aKey).Item("ButtonName"))
                    Else
                        result = GetRightKey(root.Item(aKey).Item("ButtonName"))
                    End If
                    'result = GetRightKey(root.Item(aKey).Item("ButtonName"))
                    root.Item(aKey).Item("KeyCode").AsArray().Add(result)
                End If
            Next
            myControls = JsonSerializer.Deserialize(Of GameControls)(root.ToString)
        End If
        Return myControls
    End Function

    Private Function GetRightKey(buttonName As String) As String
        Dim keyName As String = String.Empty

        Select Case buttonName
            Case "joystick_button_0"
                keyName = "Return"
            Case "joystick_button_1"
                keyName = "Backspace"
            Case "joystick_button_3"
                keyName = "LeftShift"
            Case "joystick_button_2"
                keyName = "Tab"
            Case "Axis_7_P"
                keyName = "W"
            Case "Axis_7_N"
                keyName = "S"
            Case "Axis_6_N"
                keyName = "A"
            Case "Axis_6_P"
                keyName = "D"
            Case "joystick_button_4"
                keyName = "Q"
            Case "joystick_button_5"
                keyName = "E"
            Case "joystick_button_8"
                keyName = "F"
            Case "joystick_button_9"
                keyName = "R"
            Case "joystick_button_6"
                keyName = "V"
            Case "joystick_button_7"
                keyName = "Escape"

            Case "Axis_5_N"
                keyName = "UpArrow"
            Case "Axis_5_P"
                keyName = "DownArrow"
            Case "Axis_4_N"
                keyName = "LeftArrow"
            Case "Axis_4_P"
                keyName = "RightArrow"

            Case "Axis_9_P"
                keyName = "None"
            Case "Axis_10_P"
                keyName = "None"
        End Select

        Return keyName
    End Function

    Private Function GetRightKey_PS(buttonName As String) As String
        Dim keyName As String = String.Empty

        Select Case buttonName
            Case "joystick_button_1"
                keyName = "Return"
            Case "joystick_button_2"
                keyName = "Backspace"
            Case "joystick_button_3"
                keyName = "LeftShift"
            Case "joystick_button_0"
                keyName = "Tab"
            Case "Axis_8_P"
                keyName = "W"
            Case "Axis_8_N"
                keyName = "S"
            Case "Axis_7_N"
                keyName = "A"
            Case "Axis_7_P"
                keyName = "D"
            Case "joystick_button_4"
                keyName = "Q"
            Case "joystick_button_5"
                keyName = "E"
            Case "joystick_button_10"
                keyName = "F"
            Case "joystick_button_11"
                keyName = "R"
            Case "joystick_button_8"
                keyName = "V"
            Case "joystick_button_9"
                keyName = "Escape"

            Case "Axis_6_N"
                keyName = "UpArrow"
            Case "Axis_6_P"
                keyName = "DownArrow"
            Case "Axis_3_N"
                keyName = "LeftArrow"
            Case "Axis_3_P"
                keyName = "RightArrow"

            Case "joystick_button_6"
                keyName = "None"
            Case "joystick_button_7"
                keyName = "None"
        End Select

        Return keyName
    End Function

    Private Function GetKeyActionNameList() As List(Of String)
        Dim keyActions As New List(Of String)

        keyActions.Add("Enter")
        keyActions.Add("Backspace")
        keyActions.Add("Shift")
        keyActions.Add("Tab")
        keyActions.Add("W")
        keyActions.Add("S")
        keyActions.Add("A")
        keyActions.Add("D")
        keyActions.Add("Q")
        keyActions.Add("E")
        keyActions.Add("F")
        keyActions.Add("R")
        keyActions.Add("V")
        keyActions.Add("Escape")
        keyActions.Add("UpArrow")
        keyActions.Add("DownArrow")
        keyActions.Add("LeftArrow")
        keyActions.Add("RightArrow")
        keyActions.Add("WheelUp")
        keyActions.Add("WheelDown")
        keyActions.Add("Ctrl")
        'keyActions.Add("CtrlW")
        'keyActions.Add("CtrlS")
        'keyActions.Add("CtrlA")
        'keyActions.Add("CtrlD")

        Return keyActions
    End Function

    Public Function ReplaceIfNothing(ByVal objValue As Object, ByVal altValue As Object) As Object
        Dim result As Object = objValue
        If IsNothing(objValue) Then
            result = altValue
        End If
        Return result
    End Function

End Class