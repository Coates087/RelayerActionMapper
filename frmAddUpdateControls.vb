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
#Region "Keyboard Mapping"

    Private Sub btnKeyStart_Click(sender As Object, e As EventArgs) Handles btnKeyStart.Click
        Dim strFile As String = "Xbox_start_button.png"
        ShowKeyboardDialog(strFile, LoadToolTipText(strFile), gControls.Escape)
    End Sub

    Private Sub btnKeyBack_Click(sender As Object, e As EventArgs) Handles btnKeyBack.Click
        Dim strFile As String = "Xbox_back_button.png"
        ShowKeyboardDialog(strFile, LoadToolTipText(strFile), gControls.V)
    End Sub

    Private Sub btnKeyA_Click(sender As Object, e As EventArgs) Handles btnKeyA.Click
        Dim strFile As String = "Xbox_A_button.png"
        ShowKeyboardDialog(strFile, LoadToolTipText(strFile), gControls.Enter)
    End Sub

    Private Sub btnKeyB_Click(sender As Object, e As EventArgs) Handles btnKeyB.Click
        Dim strFile As String = "Xbox_B_button.png"
        ShowKeyboardDialog(strFile, LoadToolTipText(strFile), gControls.Backspace)
    End Sub

    Private Sub btnKeyX_Click(sender As Object, e As EventArgs) Handles btnKeyX.Click
        Dim strFile As String = "Xbox_X_button.png"
        ShowKeyboardDialog(strFile, LoadToolTipText(strFile), gControls.Tab)
    End Sub

    Private Sub btnKeyY_Click(sender As Object, e As EventArgs) Handles btnKeyY.Click
        Dim strFile As String = "Xbox_Y_button.png"
        ShowKeyboardDialog(strFile, LoadToolTipText(strFile), gControls.Shift)
    End Sub

    Private Sub btnKeyLB_Click(sender As Object, e As EventArgs) Handles btnKeyLB.Click
        Dim strFile As String = "Icon_BtnXbox_LB.png"
        ShowKeyboardDialog(strFile, LoadToolTipText(strFile), gControls.Q)
    End Sub

    Private Sub btnKeyRB_Click(sender As Object, e As EventArgs) Handles btnKeyRB.Click
        Dim strFile As String = "Icon_BtnXbox_RB.png"
        ShowKeyboardDialog(strFile, LoadToolTipText(strFile), gControls.R)
    End Sub

    Private Sub btnKeyLT_Click(sender As Object, e As EventArgs) Handles btnKeyLT.Click
        Dim strFile As String = "Icon_BtnXbox_LT.png"
        ShowKeyboardDialog(strFile, LoadToolTipText(strFile), gControls.WheelUp)
    End Sub

    Private Sub btnKeyRT_Click(sender As Object, e As EventArgs) Handles btnKeyRT.Click
        Dim strFile As String = "Icon_BtnXbox_RT.png"
        ShowKeyboardDialog(strFile, LoadToolTipText(strFile), gControls.WheelDown)
    End Sub

    Private Sub btnKey_dUp_Click(sender As Object, e As EventArgs) Handles btnKey_dUp.Click
        Dim strFile As String = "Icon_BtnXbox_dpad_up.png"
        ShowKeyboardDialog(strFile, LoadToolTipText(strFile), gControls.W)
    End Sub

    Private Sub btnKey_dRight_Click(sender As Object, e As EventArgs) Handles btnKey_dRight.Click
        Dim strFile As String = "Icon_BtnXbox_dpad_right.png"
        ShowKeyboardDialog(strFile, LoadToolTipText(strFile), gControls.D)
    End Sub

    Private Sub btnKey_dLeft_Click(sender As Object, e As EventArgs) Handles btnKey_dLeft.Click
        Dim strFile As String = "Icon_BtnXbox_dpad_left.png"
        ShowKeyboardDialog(strFile, LoadToolTipText(strFile), gControls.A)
    End Sub

    Private Sub btnKey_dDown_Click(sender As Object, e As EventArgs) Handles btnKey_dDown.Click
        Dim strFile As String = "Icon_BtnXbox_dpad_down.png"
        ShowKeyboardDialog(strFile, LoadToolTipText(strFile), gControls.S)
    End Sub

    Private Sub btnKeyLsMod_Click(sender As Object, e As EventArgs) Handles btnKeyLsMod.Click
        Dim strFile As String = "Xbox_L_Sticks.png"
        ShowKeyboardDialog(strFile, gControls.Ctrl)
    End Sub

    Private Sub btnKeyLsButton_Click(sender As Object, e As EventArgs) Handles btnKeyLsButton.Click
        Dim strFile As String = "Xbox_L_StickClick.png"
        ShowKeyboardDialog(strFile, LoadToolTipText(strFile), gControls.F)
    End Sub

    Private Sub btnKeyRsUp_Click(sender As Object, e As EventArgs) Handles btnKeyRsUp.Click
        Dim strFile As String = "Xbox_R_Sticks_up.png"
        ShowKeyboardDialog(strFile, LoadToolTipText(strFile), gControls.UpArrow)
    End Sub

    Private Sub btnKeyRsDown_Click(sender As Object, e As EventArgs) Handles btnKeyRsDown.Click
        Dim strFile As String = "Xbox_R_Sticks_down.png"
        ShowKeyboardDialog(strFile, LoadToolTipText(strFile), gControls.DownArrow)
    End Sub

    Private Sub btnKeyRsLeft_Click(sender As Object, e As EventArgs) Handles btnKeyRsLeft.Click
        Dim strFile As String = "Xbox_R_Sticks_left.png"
        ShowKeyboardDialog(strFile, LoadToolTipText(strFile), gControls.LeftArrow)
    End Sub

    Private Sub btnKeyRsRight_Click(sender As Object, e As EventArgs) Handles btnKeyRsRight.Click
        Dim strFile As String = "Xbox_R_Sticks_right.png"
        ShowKeyboardDialog(strFile, LoadToolTipText(strFile), gControls.RightArrow)
    End Sub

    Private Sub btnKeyRsButton_Click(sender As Object, e As EventArgs) Handles btnKeyRsButton.Click
        Dim strFile As String = "Xbox_R_StickClick.png"
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
        fileResourceList = New List(Of String) From {
        "Icon_BtnXbox_dpad_down.png",
        "Icon_BtnXbox_dpad_left.png",
        "Icon_BtnXbox_dpad_right.png",
        "Icon_BtnXbox_dpad_up.png",
        "Icon_BtnXbox_LB.png",
        "Icon_BtnXbox_LT.png",
        "Icon_BtnXbox_RB.png",
        "Icon_BtnXbox_RT.png",
        "Xbox_A_button.png",
        "Xbox_back_button.png",
        "Xbox_B_button.png",
        "Xbox_L_StickClick.png",
        "Xbox_L_Sticks.png",
        "Xbox_L_Sticks_down.png",
        "Xbox_L_Sticks_left.png",
        "Xbox_L_Sticks_right.png",
        "Xbox_L_Sticks_up.png",
        "Xbox_menu_button.png",
        "Xbox_R_StickClick.png",
        "Xbox_R_Sticks_down.png",
        "Xbox_R_Sticks_left.png",
        "Xbox_R_Sticks_right.png",
        "Xbox_R_Sticks_up.png",
        "Xbox_start_button.png",
        "Xbox_view_button.png",
        "Xbox_X_button.png",
        "Xbox_Y_button.png"
        }
        imageStreamList = New List(Of ImageName)

        For Each strFile As String In fileResourceList

            Dim imageResult As Bitmap = LoadImage(strFile)
            Dim tooTipText As String = LoadToolTipText(strFile)
            imageStreamList.Add(New ImageName With {.FileName = strFile, .ImageData = imageResult, .ToolTipText = tooTipText})
        Next


        For Each imageStream As ImageName In imageStreamList
            If Not IsNothing(imageStream.ImageData) Then
                If imageStream.FileName = "Xbox_start_button.png" Then
                    picBoxStart.Image = imageStream.ImageData
                    tip1.SetToolTip(picBoxStart, imageStream.ToolTipText)
                ElseIf imageStream.FileName = "Xbox_back_button.png" Then
                    picBoxBack.Image = imageStream.ImageData
                    tip1.SetToolTip(picBoxBack, imageStream.ToolTipText)
                ElseIf imageStream.FileName = "Xbox_A_button.png" Then
                    picBoxA.Image = imageStream.ImageData
                    tip1.SetToolTip(picBoxA, imageStream.ToolTipText)
                ElseIf imageStream.FileName = "Xbox_B_button.png" Then
                    picBoxB.Image = imageStream.ImageData
                    tip1.SetToolTip(picBoxB, imageStream.ToolTipText)
                ElseIf imageStream.FileName = "Xbox_X_button.png" Then
                    picBoxX.Image = imageStream.ImageData
                    tip1.SetToolTip(picBoxX, imageStream.ToolTipText)
                ElseIf imageStream.FileName = "Xbox_Y_button.png" Then
                    picBoxY.Image = imageStream.ImageData
                    tip1.SetToolTip(picBoxY, imageStream.ToolTipText)
                ElseIf imageStream.FileName = "Icon_BtnXbox_LB.png" Then
                    picBoxLB.Image = imageStream.ImageData
                    tip1.SetToolTip(picBoxLB, imageStream.ToolTipText)
                ElseIf imageStream.FileName = "Icon_BtnXbox_RB.png" Then
                    picBoxRB.Image = imageStream.ImageData
                    tip1.SetToolTip(picBoxRB, imageStream.ToolTipText)
                ElseIf imageStream.FileName = "Icon_BtnXbox_LT.png" Then
                    picBoxLT.Image = imageStream.ImageData
                    tip1.SetToolTip(picBoxLT, imageStream.ToolTipText)
                ElseIf imageStream.FileName = "Icon_BtnXbox_RT.png" Then
                    picBoxRT.Image = imageStream.ImageData
                    tip1.SetToolTip(picBoxRT, imageStream.ToolTipText)
                    ''dpad
                ElseIf imageStream.FileName = "Icon_BtnXbox_dpad_up.png" Then
                    picBox_dUp.Image = imageStream.ImageData
                    tip1.SetToolTip(picBox_dUp, imageStream.ToolTipText)
                ElseIf imageStream.FileName = "Icon_BtnXbox_dpad_down.png" Then
                    picBox_dDown.Image = imageStream.ImageData
                    tip1.SetToolTip(picBox_dDown, imageStream.ToolTipText)
                ElseIf imageStream.FileName = "Icon_BtnXbox_dpad_left.png" Then
                    picBox_dLeft.Image = imageStream.ImageData
                    tip1.SetToolTip(picBox_dLeft, imageStream.ToolTipText)
                ElseIf imageStream.FileName = "Icon_BtnXbox_dpad_right.png" Then
                    picBox_dRight.Image = imageStream.ImageData
                    tip1.SetToolTip(picBox_dRight, imageStream.ToolTipText)

                    ''left stick
                ElseIf imageStream.FileName = "Xbox_L_Sticks.png" Then
                    picBoxLsMod.Image = imageStream.ImageData
                    tip1.SetToolTip(picBoxLsMod, imageStream.ToolTipText)
                ElseIf imageStream.FileName = "Xbox_L_Sticks_up.png" Then
                    picBoxLsUp.Image = imageStream.ImageData
                    tip1.SetToolTip(picBoxLsUp, imageStream.ToolTipText)
                ElseIf imageStream.FileName = "Xbox_L_Sticks_down.png" Then
                    picBoxLsDown.Image = imageStream.ImageData
                    tip1.SetToolTip(picBoxLsDown, imageStream.ToolTipText)
                ElseIf imageStream.FileName = "Xbox_L_Sticks_left.png" Then
                    picBoxLsLeft.Image = imageStream.ImageData
                    tip1.SetToolTip(picBoxLsLeft, imageStream.ToolTipText)
                ElseIf imageStream.FileName = "Xbox_L_Sticks_right.png" Then
                    picBoxLsRight.Image = imageStream.ImageData
                    tip1.SetToolTip(picBoxLsRight, imageStream.ToolTipText)
                ElseIf imageStream.FileName = "Xbox_L_StickClick.png" Then
                    picBoxLsButton.Image = imageStream.ImageData
                    tip1.SetToolTip(picBoxLsButton, imageStream.ToolTipText)

                    ''right stick
                ElseIf imageStream.FileName = "Xbox_R_Sticks_up.png" Then
                    picBoxRsUp.Image = imageStream.ImageData
                    tip1.SetToolTip(picBoxRsUp, imageStream.ToolTipText)
                ElseIf imageStream.FileName = "Xbox_R_Sticks_down.png" Then
                    picBoxRsDown.Image = imageStream.ImageData
                    tip1.SetToolTip(picBoxRsDown, imageStream.ToolTipText)
                ElseIf imageStream.FileName = "Xbox_R_Sticks_left.png" Then
                    picBoxRsLeft.Image = imageStream.ImageData
                    tip1.SetToolTip(picBoxRsLeft, imageStream.ToolTipText)
                ElseIf imageStream.FileName = "Xbox_R_Sticks_right.png" Then
                    picBoxRsRight.Image = imageStream.ImageData
                    tip1.SetToolTip(picBoxRsRight, imageStream.ToolTipText)
                ElseIf imageStream.FileName = "Xbox_R_StickClick.png" Then
                    picBoxRsButton.Image = imageStream.ImageData
                    tip1.SetToolTip(picBoxRsButton, imageStream.ToolTipText)
                End If
            End If
        Next

        Dim xboxButtonList As New List(Of XboxButton)

        xboxButtonList.Add(New XboxButton With {.XboxButtonName = "", .XboxButtonValue = ""})
        xboxButtonList.AddRange(GetXboxButtons())

        Dim myControls = gboxFields.Controls.Cast(Of Control)().Where(Function(e) e.GetType = GetType(ComboBox) And e.Tag?.ToString = "XboxCBox").ToList
        Dim xboxCboxes As New List(Of ComboBox)
        xboxCboxes = myControls.ConvertAll(Of ComboBox)(Function(t As Control) DirectCast(t, ComboBox))
        For Each xboxCBox In xboxCboxes
            xboxCBox.DisplayMember = "XboxButtonName"
            xboxCBox.ValueMember = "XboxButtonValue"
            xboxCBox.DataSource = New List(Of XboxButton)(xboxButtonList)

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
            cboRsLeft.SelectedValue = gControls.DownArrow.ButtonName
            cboRsRight.SelectedValue = gControls.DownArrow.ButtonName
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

    Public Function LoadImage(strFile As String) As Bitmap
        Dim image As Bitmap = Nothing

        Dim fullResourceName As String = assemblyName + "." + strFile

        Dim thisAssembly As Assembly = Assembly.GetExecutingAssembly()
        Dim obj = thisAssembly.GetManifestResourceNames()
        Dim readReflect = thisAssembly.GetManifestResourceStream(fullResourceName)

        If Not IsNothing(readReflect) Then

            image = New Bitmap(readReflect)
        End If
        Return image
    End Function

    Public Function LoadToolTipText(strFile As String) As String
        Dim tipMessage As String = ""


        Select Case strFile
            Case "Icon_BtnXbox_dpad_down.png"
                tipMessage = $"Move cursor down"
            Case "Icon_BtnXbox_dpad_left.png"
                tipMessage = $"Move cursor left"
            Case "Icon_BtnXbox_dpad_right.png"
                tipMessage = $"Move cursor right"
            Case "Icon_BtnXbox_dpad_up.png"
                tipMessage = $"Move cursor up"

            Case "Icon_BtnXbox_LB.png"
                tipMessage = $"Switch page/unit left"
            Case "Icon_BtnXbox_LT.png"
                tipMessage = $"Zoom Out"
            Case "Icon_BtnXbox_RB.png"
                tipMessage = $"Switch page/unit left{vbCrLf}VN Event/Cutscene: Fast Forword"
            Case "Icon_BtnXbox_RT.png"
                tipMessage = $"Zoom In"

            Case "Xbox_start_button.png", "Xbox_menu_button.png"
                tipMessage = $"Options Menu: revert to default settings{vbCrLf}Battle Screen: Battle menu{vbCrLf}VN Event/Cutscene: Skip event"
            Case "Xbox_back_button.png", "Xbox_view_button.png"
                tipMessage = $"Battle Screen: Display Stage information"

            Case "Xbox_A_button.png"
                tipMessage = $"Confirm"
            Case "Xbox_B_button.png"
                tipMessage = $"Cancel/Back"
            Case "Xbox_X_button.png"
                tipMessage = $"Battle Screen:Display Detailed Info{vbCrLf}VN Event/Cutscene: Backlog{vbCrLf}Shop Screen: Overview{vbCrLf}Star Cube Screen: Overview"
            Case "Xbox_Y_button.png"
                tipMessage = $"Equipment Screen: Remove skill/equipment{vbCrLf}Battle Screen: Display threat area{vbCrLf}VN Event/Cutscene: Auto-Advance{vbCrLf}Shop Screen: Confirm Purchase"

            Case "Xbox_R_Sticks_down.png"
                tipMessage = $"Battle Screen: Move camera down{vbCrLf}Star Cube Screen: freely move cursor down"
            Case "Xbox_R_Sticks_left.png"
                tipMessage = $"Battle Screen: Move camera left{vbCrLf}Star Cube Screen: freely move cursor left"
            Case "Xbox_R_Sticks_right.png"
                tipMessage = $"Battle Screen: Move camera right{vbCrLf}Star Cube Screen: freely move cursor right"
            Case "Xbox_R_Sticks_up.png"
                tipMessage = $"Battle Screen: Move camera up{vbCrLf}Star Cube Screen: freely move cursor up"

            Case "Xbox_R_StickClick.png"
                tipMessage = $"Battle Screen: Toggle Auto Battle"

            Case "Xbox_L_Sticks_down.png"
                tipMessage = $"Move cursor down"
            Case "Xbox_L_Sticks_left.png"
                tipMessage = $"Move cursor left"
            Case "Xbox_L_Sticks_right.png"
                tipMessage = $"Move cursor right"
            Case "Xbox_L_Sticks_up.png"
                tipMessage = $"Move cursor up"

            Case "Xbox_L_StickClick.png"
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
                    root.Item(aKey).Item("KeyCode").AsArray().Add("Control")
                Else
                    root.Item(aKey).Item("KeyCode").AsArray().Clear()
                    Dim result = GetRightKey(root.Item(aKey).Item("ButtonName"))
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
                keyName = "Shift"
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
                keyName = ""
            Case "Axis_10_P"
                keyName = ""
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