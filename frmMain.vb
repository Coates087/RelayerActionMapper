Imports System.IO
Imports System.Text.Json
Imports System.Reflection

Public Class frmMain
    Delegate Sub InvokeDelegate()

    Public assemblyName As String = String.Empty
    Public gStrOpenFileName As String = String.Empty
    Public gControls As GameControls = Nothing
    Public gUnsavedChanges As Boolean = False

    Const resKeyConfigDefault As String = "KeyConfigDefault.json"
    Const resKeyGamePadConfigDefault As String = "KeyConfigDefaultController.json"
    Const resKeyGamePadConfigDefault_PS As String = "KeyConfig_PS.json"
    Const resKeyboardConfigDefault As String = "KeyConfigDefaultKeyboard.json"

    Public gPlatform As Platform = Platform.Xbox_btns

    Public defaultSavePath As String = ""

    Private Sub frmMain_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim thisAssembly As Assembly = Assembly.GetExecutingAssembly()
        Dim anAssemblyName = thisAssembly.GetName()

        ' ' Comment out for testing
        TestPS.Visible = False
        TestXbox.Visible = False
        'btnTest.Visible = True
        assemblyName = "RelayerActionMapper" ''anAssemblyName.Name
        lblFile.Text = ""
        ClearSaveLabel()

        HandleArgs()
    End Sub


    Private Sub btnHelp_Click(sender As Object, e As EventArgs) Handles btnHelp.Click
        OpenHelpSection()
    End Sub

    Private Sub rbnXbox_CheckedChanged(sender As Object, e As EventArgs) Handles rbnXbox.CheckedChanged

        If rbnXbox.Checked Then
            gPlatform = Platform.Xbox_btns
        End If
    End Sub

    Private Sub rbnPS_CheckedChanged(sender As Object, e As EventArgs) Handles rbnPS.CheckedChanged
        If rbnPS.Checked Then
            gPlatform = Platform.PS_btns
        End If
    End Sub

    Private Sub frmMain_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        ' Determine if text has changed in the textbox by comparing to original text.
        If gUnsavedChanges Then
            ' Display a MsgBox asking the user to save changes or abort.
            Dim myMessage As String = "There are unsaved changes. Any unsaved changes to a file will be lossed. Are you sure you want to exit the program?"
            Dim msgResult = MessageBox.Show(myMessage, "Relayer Action Mapper", MessageBoxButtons.YesNo)
            If msgResult = DialogResult.No Then
                ' Cancel the Closing event from closing the form.
                e.Cancel = True
            End If
        End If
    End Sub

    Private Sub txtFileName_TextChanged(sender As Object, e As EventArgs)

    End Sub

    Private Sub Label1_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub btnExit_Click(sender As Object, e As EventArgs) Handles btnExit.Click
        Application.Exit()
    End Sub

    Private Sub btnDefaultFile_Click(sender As Object, e As EventArgs) Handles btnDefaultFile.Click
        CreateDefaultJsonFile(0)
    End Sub

    Private Sub btnDefaultGamePadFile_Click(sender As Object, e As EventArgs) Handles btnDefaultGamePadFile.Click
        CreateDefaultJsonFile(1)
    End Sub
    Private Sub btnPSGamePadFile_Click(sender As Object, e As EventArgs) Handles btnPSGamePadFile.Click
        CreateDefaultJsonFile(3)
    End Sub

    Private Sub btnDefaultKBMFile_Click(sender As Object, e As EventArgs) Handles btnDefaultKBMFile.Click
        CreateDefaultJsonFile(2)
    End Sub


    Private Sub TestPS_Click(sender As Object, e As EventArgs) Handles TestPS.Click
        LoadJsonIntoMemory(3)
    End Sub


    Private Sub TestXbox_Click(sender As Object, e As EventArgs) Handles TestXbox.Click
        LoadJsonIntoMemory(1)
    End Sub

    Private Sub btnLoadFile_Click(sender As Object, e As EventArgs) Handles btnLoadFile.Click
        'BeginInvoke(New InvokeDelegate(AddressOf DisableFileTextBox))
        'LoadFile()
        'BeginInvoke(New InvokeDelegate(AddressOf EnableFileTextBox))

        Dim oFile As New frmOpenFile

        Dim controlDialog As DialogResult = oFile.ShowDialog()
        lblFile.Text = ""

        If controlDialog = DialogResult.OK Then
            gControls = oFile.gControls
            gStrOpenFileName = oFile.gStrOpenFileName
            lblFile.Text = "File Loaded: " & Path.GetFileName(gStrOpenFileName)
            ''gUnsavedChanges = True
        End If
    End Sub

    Private Sub OpenHelpSection()

        Dim myHelpText As String = ""
        Dim myHelpClass As New HelpSectionClass
        myHelpText = myHelpClass.GetHelpSectionText()

        Dim myHelp As New frmControllerDialog With {.UseCustomText = True, .CustomText = myHelpText}

        myHelp.ShowDialog()
    End Sub

    Private Sub btnOpenControls_Click(sender As Object, e As EventArgs) Handles btnOpenControls.Click
        Dim specialMessage As String = "No config file has been loaded. Do you wish to edit the controls from a pre-made configuration?"
        If IsNothing(gControls) Then

            Dim msgResult = MessageBox.Show(specialMessage, "Information", MessageBoxButtons.YesNoCancel)

            If msgResult = DialogResult.Yes Then
                Dim tempControls As GameControls = Nothing
                Dim strReult As String = LoadResourceFile(assemblyName + "." + resKeyConfigDefault)

                tempControls = JsonSerializer.Deserialize(Of GameControls)(strReult)
                gControls = tempControls
                gUnsavedChanges = True
                PopulateSaveLabel()
            ElseIf msgResult = DialogResult.Cancel Then
                Exit Sub
            End If
        End If

        Dim editControls As New frmAddUpdateControls
        editControls.assemblyName = assemblyName
        editControls.gStrOpenFileName = gStrOpenFileName
        editControls.gControls = gControls
        editControls.gPlatform = gPlatform
        Dim controlDialog As DialogResult = editControls.ShowDialog()

        If controlDialog = DialogResult.OK Then
            gControls = editControls.gSaveControls
            gUnsavedChanges = True
            PopulateSaveLabel()
        End If
    End Sub

    Private Sub btnPreviewConfig_Click(sender As Object, e As EventArgs) Handles btnPreviewConfig.Click
        Dim previewControls As New frmPreviewConfigFile

        Dim myOptions As New JsonSerializerOptions
        myOptions.WriteIndented = True
        myOptions.Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping
        myOptions.WriteIndented = True

        Dim myControls As GameControlsPlus = Nothing
        '' build for .Net Desktop Runtime

        If Not IsNothing(gControls) Then
            Dim strJson As String = JsonSerializer.Serialize(gControls, myOptions)
            myControls = JsonSerializer.Deserialize(Of GameControlsPlus)(strJson)

            myControls = myControls.GetDescriptonsForControlsJSON() ''AddDescriptonsForControls(myControls)
        End If

        previewControls.gControls = myControls
        Dim controlDialog As DialogResult = previewControls.ShowDialog()

        'If controlDialog = DialogResult.OK Then
        '    gControls = editControls.gSaveControls
        '    gUnsavedChanges = True
        'End If
    End Sub

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        If IsNothing(gControls) Then
            MessageBox.Show("Controls have not been set", "Save Unsuccessful", MessageBoxButtons.OK)
        Else
            SaveFile()
        End If
    End Sub

    Private Sub HandleArgs()

        Dim arguments As List(Of String) = Environment.GetCommandLineArgs().ToList()

        Dim filteredArgs As New List(Of String)
        Dim finalArgs As New List(Of String)

        If arguments?.Count > 0 Then
            For i As Integer = 1 To arguments.Count - 1 '' We want to skip the first "argument" since that refers to just the dll or exe of the program
                filteredArgs.Add(arguments(i))
            Next
        End If

        Dim strJoinedArgs As String = String.Join(", ", filteredArgs)

        If Not String.IsNullOrWhiteSpace(strJoinedArgs) Then
            strJoinedArgs = strJoinedArgs.Replace("""", ", ")
            finalArgs = strJoinedArgs.Split(", ").ToList()

        End If
        ' defaultSavePath
        ' chkOverride

        Dim index As Integer = 0
        For Each arg In finalArgs
            Dim argLower As String = Trim(arg.ToLower())

            Select Case argLower
                Case "-ps"
                    rbnPS.Checked = True
                Case "-load"
                    If index + 1 <= finalArgs.Count Then
                        Dim loadFileName As String = finalArgs(index + 1)
                        LoadFile(loadFileName)
                    End If
                Case "-save"
                    If index + 1 <= finalArgs.Count Then
                        defaultSavePath = finalArgs(index + 1)
                    End If
                Case "-overridesave"
                    chkOverride.Checked = True

            End Select

            index += 1
        Next
    End Sub


    Public Sub LoadFile(strFilename As String)
        If Not IO.File.Exists(strFilename) Then
            lblFile.Text = "JSON file not formatted properly. Invalid File."
            Exit Sub
        End If

        Dim strFileData As String = My.Computer.FileSystem.ReadAllText(strFilename)

        Dim myOptions As New JsonSerializerOptions
        myOptions.WriteIndented = True
        myOptions.Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping

        Dim myControls As GameControls = Nothing
        Dim validResult As String = ""

        Try

            myControls = JsonSerializer.Deserialize(Of GameControls)(strFileData)
            validResult = ValidateJSON(myControls)

            If Not validResult = "" Then
                lblFile.Text = "JSON file not formatted properly. Invalid File."
            Else
                gControls = myControls
                gStrOpenFileName = strFilename
                lblFile.Text = "File Loaded: " & Path.GetFileName(gStrOpenFileName)
            End If

        Catch ex As Exception
            lblFile.Text = "JSON file not formatted properly. Invalid File."
        End Try
    End Sub

    Public Function AddDescriptonsForControls(ByVal myControls As GameControlsPlus) As GameControlsPlus
        myControls.W.Description = $"Move cursor up"
        myControls.A.Description = $"Move cursor left"
        myControls.D.Description = $"Move cursor right"
        myControls.S.Description = $"Move cursor down"

        myControls.CtrlW.Description = $"Move cursor up"
        myControls.CtrlA.Description = $"Move cursor left"
        myControls.CtrlD.Description = $"Move cursor right"
        myControls.CtrlS.Description = $"Move cursor down"

        myControls.Q.Description = $"Switch page/unit left"
        myControls.E.Description = $"Switch page/unit left{vbCrLf}VN Event/Cutscene: Fast Forword"
        myControls.WheelDown.Description = $"Zoom In"
        myControls.WheelUp.Description = $"Zoom Out"

        myControls.V.Description = $"Battle Screen: Display Stage information"
        myControls.Escape.Description = $"Options Menu: revert to default settings{vbCrLf}Battle Screen: Battle menu{vbCrLf}VN Event/Cutscene: Skip event"

        myControls.F.Description = $"Battle Screen: View Aggro List"
        myControls.R.Description = $"Battle Screen: Toggle Auto Battle"

        myControls.Enter.Description = $"Confirm"
        myControls.Backspace.Description = $"Cancel/Back"
        myControls.Tab.Description = $"Battle Screen:Display Detailed Info{vbCrLf}VN Event/Cutscene: Backlog{vbCrLf}Shop Screen: Overview{vbCrLf}Star Cube Screen: Overview"
        myControls.Shift.Description = $"Equipment Screen: Remove skill/equipment{vbCrLf}Battle Screen: Display threat area{vbCrLf}VN Event/Cutscene: Auto-Advance{vbCrLf}Shop Screen: Confirm Purchase"

        myControls.UpArrow.Description = $"Battle Screen: Move camera up{vbCrLf}Star Cube Screen: freely move cursor up"
        myControls.LeftArrow.Description = $"Battle Screen: Move camera left{vbCrLf}Star Cube Screen: freely move cursor left"
        myControls.DownArrow.Description = $"Battle Screen: Move camera down{vbCrLf}Star Cube Screen: freely move cursor down"
        myControls.RightArrow.Description = $"Battle Screen: Move camera right{vbCrLf}Star Cube Screen: freely move cursor right"

        Return myControls
    End Function



    Private Sub PopulateSaveLabel()

        lblUnSave.Text = "Click on the <<Save File>> button to save your changes."
    End Sub

    Private Sub ClearSaveLabel()
        gUnsavedChanges = False
        lblUnSave.Text = ""
    End Sub

    Public Sub LoadJsonIntoMemory(intDefaultType As Integer)

        Dim fullResourceName As String = ""
        Dim strPlatform As String = "Xbox"
        '' 1 = Xbox
        '' 3 = PS

        Select Case intDefaultType
            Case 1
                fullResourceName = assemblyName + "." + resKeyGamePadConfigDefault '' Xbox
            Case 3
                fullResourceName = assemblyName + "." + resKeyGamePadConfigDefault_PS '' PS
                strPlatform = "PlayStation"
            Case Else
                fullResourceName = assemblyName + "." + resKeyGamePadConfigDefault
        End Select


        Dim strFileData As String = LoadResourceFile(fullResourceName)


        Dim myControls As GameControls = Nothing

        Dim validResult As String = ""

        myControls = JsonSerializer.Deserialize(Of GameControls)(strFileData)

        'validResult = ValidateJSON(myControls)

        'If Not validResult = "" Then
        '    MessageBox.Show("JSON file not formatted properly")
        'End If


        gControls = myControls

        If intDefaultType = 1 Then
            'gPlatform = Platform.Xbox_btns
            rbnXbox.Checked = True
        ElseIf intDefaultType = 3 Then
            'gPlatform = Platform.PS_btns
            rbnPS.Checked = True
        End If


        lblFile.Text = $"Loaded {strPlatform} Config into memory!!!"
    End Sub

    Public Sub CreateDefaultJsonFile(intDefaultType As Integer)
        Dim sFile As New SaveFileDialog
        sFile.DefaultExt = ".json"
        sFile.Filter = "JSON File (.json)|*.json" ' Filter files by extension
        sFile.FileName = "KeyConfig.json"
        Dim strSaveFilePath As String = ""

        Dim strFileData As String = String.Empty
        If chkOverride.Checked Then
            sFile.OverwritePrompt = False
        End If

        If Directory.Exists(defaultSavePath) Then
            sFile.InitialDirectory = defaultSavePath
        End If

        ' Show open file dialog box
        Dim result = sFile.ShowDialog()

        ' Process open file dialog box results
        If result = DialogResult.OK Then
            ' Open document
            strSaveFilePath = sFile.FileName
        Else
            Exit Sub
        End If

        Dim fullResourceName As String = ""

        '' 1 = Xbox
        '' 3 = PS

        Select Case intDefaultType
            Case 0
                fullResourceName = assemblyName + "." + resKeyConfigDefault
            Case 1
                fullResourceName = assemblyName + "." + resKeyGamePadConfigDefault
            Case 2
                fullResourceName = assemblyName + "." + resKeyboardConfigDefault
            Case 3
                fullResourceName = assemblyName + "." + resKeyGamePadConfigDefault_PS
            Case Else
                fullResourceName = assemblyName + "." + resKeyConfigDefault
        End Select

        Dim strResult As String = LoadResourceFile(fullResourceName)

        Dim writeFileResult = WriteToFile(strResult, strSaveFilePath)
        Dim dirPath As String = ""
        If writeFileResult = True Then
            If intDefaultType = 1 Then
                'gPlatform = Platform.Xbox_btns
                rbnXbox.Checked = True
            ElseIf intDefaultType = 3 Then
                'gPlatform = Platform.PS_btns
                rbnPS.Checked = True
            End If

            If String.IsNullOrWhiteSpace(gStrOpenFileName) Then
                LoadFile(strSaveFilePath)
            End If

            Dim msgResult = MessageBox.Show("Do you wish to open the directory for this file?", "Save Successful", MessageBoxButtons.YesNo)

            If msgResult = DialogResult.Yes Then
                dirPath = IO.Path.GetDirectoryName(strSaveFilePath)
                Process.Start("explorer.exe", "/select,/separate," & Chr(34) & strSaveFilePath & Chr(34))
                'Process.Start("explorer.exe", "/select" & Chr(34) & strSaveFilePath & Chr(34))
                'Process.Start(dirPath)
            End If
        End If

    End Sub

    Private Function LoadResourceFile(strResourcePath As String) As String
        Dim strResult As String = String.Empty

        Dim thisAssembly As Assembly = Assembly.GetExecutingAssembly()
        Dim readReflect = thisAssembly.GetManifestResourceStream(strResourcePath)
        Dim reader = New StreamReader(readReflect)
        strResult = reader.ReadToEnd()
        reader.Close()
        Return strResult
    End Function

    Public Function WriteToFile(strText, strPath) As Boolean
        Dim result As Boolean = True
        Try

            File.WriteAllText(strPath, strText)
        Catch ex As Exception
            result = False
        End Try

        Return result
    End Function

    Private Sub SaveFile()

        Dim sFile As New SaveFileDialog
        sFile.DefaultExt = ".json"
        sFile.Filter = "JSON File (.json)|*.json" ' Filter files by extension
        sFile.FileName = "KeyConfig.json"
        Dim strSaveFilePath As String = ""

        Dim strFileData As String = String.Empty

        If chkOverride.Checked Then
            sFile.OverwritePrompt = False
        End If

        If Directory.Exists(defaultSavePath) Then
            sFile.InitialDirectory = defaultSavePath
        End If

        ' Show open file dialog box
        Dim result = sFile.ShowDialog()

        ' Process open file dialog box results
        If result = DialogResult.OK Then
            ' Open document
            strSaveFilePath = sFile.FileName

            ClearSaveLabel()
        Else
            Exit Sub
        End If

        Dim myOptions As New JsonSerializerOptions
        myOptions.WriteIndented = True
        myOptions.Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping
        myOptions.WriteIndented = True

        Dim json As String = JsonSerializer.Serialize(gControls, myOptions)

        Dim writeFileResult = WriteToFile(json, strSaveFilePath)
        Dim dirPath As String = ""
        If writeFileResult = True Then
            gUnsavedChanges = False
            Dim msgResult = MessageBox.Show("Do you wish to open the directory for this file?", "Save Successful", MessageBoxButtons.YesNo)

            If msgResult = DialogResult.Yes Then
                dirPath = IO.Path.GetDirectoryName(strSaveFilePath)
                Process.Start("explorer.exe", "/select,/separate," & Chr(34) & strSaveFilePath & Chr(34))
            End If
        End If
    End Sub


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


    Public Function ValidateJSON(ByRef myControls As GameControls) As String

        Dim result As String = ""
        Dim objEmpty As Boolean = False
        If IsNothing(myControls) Then
            objEmpty = True
            result = "JSON object is empty"
            Return result
        End If


        '' dpad up
        If objEmpty = False AndAlso IsNothing(myControls.W) Then
            objEmpty = True
            result = "{W} Key is invalid"
        End If
        If objEmpty = False AndAlso IsNothing(myControls.W.ButtonName) Then
            objEmpty = True
            result = "{W} Key's ButtonName is invalid"
        Else
            If objEmpty = False Then
                myControls.W.XBoxButton = "dpad_up"
            End If
        End If

        '' dpad left
        If objEmpty = False AndAlso IsNothing(myControls.A) Then
            objEmpty = True
            result = "{A} Key is invalid"
        End If
        If objEmpty = False AndAlso IsNothing(myControls.A.ButtonName) Then
            objEmpty = True
            result = "{A} Key's ButtonName is invalid"
        Else
            If objEmpty = False Then
                myControls.A.XBoxButton = "dpad_left"
            End If
        End If

        '' dpad down
        If objEmpty = False AndAlso IsNothing(myControls.S) Then
            objEmpty = True
            result = "{S} Key is invalid"
        End If
        If objEmpty = False AndAlso IsNothing(myControls.S.ButtonName) Then
            objEmpty = True
            result = "{S} Key's ButtonName is invalid"
        Else
            If objEmpty = False Then
                myControls.S.XBoxButton = "dpad_down"
            End If
        End If

        '' dpad right
        If objEmpty = False AndAlso IsNothing(myControls.D) Then
            objEmpty = True
            result = "{D} Key is invalid"
        End If
        If objEmpty = False AndAlso IsNothing(myControls.D.ButtonName) Then
            objEmpty = True
            result = "{D} Key's ButtonName is invalid"
        Else
            If objEmpty = False Then
                myControls.D.XBoxButton = "dpad_right"
            End If
        End If

        '' A
        If objEmpty = False AndAlso IsNothing(myControls.Enter) Then
            objEmpty = True
            result = "{Enter} Key is invalid"
        End If
        If objEmpty = False AndAlso IsNothing(myControls.Enter.ButtonName) Then
            objEmpty = True
            result = "{Enter} Key's ButtonName is invalid"
        Else
            If objEmpty = False Then
                myControls.Enter.XBoxButton = "xbox_a"
            End If
        End If

        '' B
        If objEmpty = False AndAlso IsNothing(myControls.Backspace) Then
            objEmpty = True
            result = "{Backspace} Key is invalid"
        End If
        If objEmpty = False AndAlso IsNothing(myControls.Backspace.ButtonName) Then
            objEmpty = True
            result = "{Backspace} Key's ButtonName is invalid"
        Else
            If objEmpty = False Then
                myControls.Backspace.XBoxButton = "xbox_b"
            End If
        End If

        '' Xbox Start
        If objEmpty = False AndAlso IsNothing(myControls.Escape) Then
            objEmpty = True
            result = "{Escape} Key is invalid"
        End If
        If objEmpty = False AndAlso IsNothing(myControls.Escape.ButtonName) Then
            objEmpty = True
            result = "{Escape} Key's ButtonName is invalid"
        Else
            If objEmpty = False Then
                myControls.Escape.XBoxButton = "xbox_start"
            End If
        End If

        '' Y
        If objEmpty = False AndAlso IsNothing(myControls.Shift) Then
            objEmpty = True
            result = "{Shift} Key is invalid"
        End If
        If objEmpty = False AndAlso IsNothing(myControls.Shift.ButtonName) Then
            objEmpty = True
            result = "{Shift} Key's ButtonName is invalid"
        Else
            If objEmpty = False Then
                myControls.Shift.XBoxButton = "xbox_y"
            End If
        End If

        '' X
        If objEmpty = False AndAlso IsNothing(myControls.Tab) Then
            objEmpty = True
            result = "{Tab} Key is invalid"
        End If
        If objEmpty = False AndAlso IsNothing(myControls.Tab.ButtonName) Then
            objEmpty = True
            result = "{Tab} Key's ButtonName is invalid"
        Else
            If objEmpty = False Then
                myControls.Tab.XBoxButton = "xbox_x"
            End If
        End If

        '' LB
        If objEmpty = False AndAlso IsNothing(myControls.Q) Then
            objEmpty = True
            result = "{Q} Key is invalid"
        End If
        If objEmpty = False AndAlso IsNothing(myControls.Q.ButtonName) Then
            objEmpty = True
            result = "{Q} Key's ButtonName is invalid"
        Else
            If objEmpty = False Then
                myControls.Q.XBoxButton = "xbox_lb"
            End If
        End If

        '' RB
        If objEmpty = False AndAlso IsNothing(myControls.E) Then
            objEmpty = True
            result = "{E} Key is invalid"
        End If
        If objEmpty = False AndAlso IsNothing(myControls.E.ButtonName) Then
            objEmpty = True
            result = "{E} Key's ButtonName is invalid"
        Else
            If objEmpty = False Then
                myControls.E.XBoxButton = "xbox_rb"
            End If
        End If

        '' Left Stick Button
        If objEmpty = False AndAlso IsNothing(myControls.F) Then
            objEmpty = True
            result = "{F} Key is invalid"
        End If
        If objEmpty = False AndAlso IsNothing(myControls.F.ButtonName) Then
            objEmpty = True
            result = "{F} Key's ButtonName is invalid"
        Else
            If objEmpty = False Then
                myControls.F.XBoxButton = "xbox_lStickBtn"
            End If
        End If

        '' Right Stick Button
        If objEmpty = False AndAlso IsNothing(myControls.R) Then
            objEmpty = True
            result = "{R} Key is invalid"
        End If
        If objEmpty = False AndAlso IsNothing(myControls.R.ButtonName) Then
            objEmpty = True
            result = "{R} Key's ButtonName is invalid"
        Else
            If objEmpty = False Then
                myControls.R.XBoxButton = "xbox_rStickBtn"
            End If
        End If

        '' Xbox Back
        If objEmpty = False AndAlso IsNothing(myControls.V) Then
            objEmpty = True
            result = "{VE} Key is invalid"
        End If
        If objEmpty = False AndAlso IsNothing(myControls.V.ButtonName) Then
            objEmpty = True
            result = "{V} Key's ButtonName is invalid"
        Else
            If objEmpty = False Then
                myControls.V.XBoxButton = "xbox_back"
            End If
        End If

        '' Right Stick Up
        If objEmpty = False AndAlso IsNothing(myControls.UpArrow) Then
            objEmpty = True
            result = "{UpArrow} Key is invalid"
        End If
        If objEmpty = False AndAlso IsNothing(myControls.UpArrow.ButtonName) Then
            objEmpty = True
            result = "{UpArrow} Key's ButtonName is invalid"
        Else
            If objEmpty = False Then
                myControls.UpArrow.XBoxButton = "xbox_rStickUp"
            End If
        End If

        '' Right Stick Down
        If objEmpty = False AndAlso IsNothing(myControls.DownArrow) Then
            objEmpty = True
            result = "{DownArrow} Key is invalid"
        End If
        If objEmpty = False AndAlso IsNothing(myControls.DownArrow.ButtonName) Then
            objEmpty = True
            result = "{DownArrow} Key's ButtonName is invalid"
        Else
            If objEmpty = False Then
                myControls.DownArrow.XBoxButton = "xbox_rStickDown"
            End If
        End If

        '' Right Stick Left
        If objEmpty = False AndAlso IsNothing(myControls.LeftArrow) Then
            objEmpty = True
            result = "{LeftArrow} Key is invalid"
        End If
        If objEmpty = False AndAlso IsNothing(myControls.LeftArrow.ButtonName) Then
            objEmpty = True
            result = "{LeftArrow} Key's ButtonName is invalid"
        Else
            If objEmpty = False Then
                myControls.LeftArrow.XBoxButton = "xbox_rStickLeft"
            End If
        End If

        '' Right Stick Right
        If objEmpty = False AndAlso IsNothing(myControls.RightArrow) Then
            objEmpty = True
            result = "{RightArrow} Key is invalid"
        End If
        If objEmpty = False AndAlso IsNothing(myControls.RightArrow.ButtonName) Then
            objEmpty = True
            result = "{RightArrow} Key's ButtonName is invalid"
        Else
            If objEmpty = False Then
                myControls.RightArrow.XBoxButton = "xbox_rStickRight"
            End If
        End If

        '' LT
        If objEmpty = False AndAlso IsNothing(myControls.WheelUp) Then
            objEmpty = True
            result = "{WheelUp} Key is invalid"
        End If
        If objEmpty = False AndAlso IsNothing(myControls.WheelUp.ButtonName) Then
            objEmpty = True
            result = "{WheelUp} Key's ButtonName is invalid"
        Else
            If objEmpty = False Then
                myControls.WheelUp.XBoxButton = "xbox_lt"
            End If
        End If

        '' RT
        If objEmpty = False AndAlso IsNothing(myControls.WheelDown) Then
            objEmpty = True
            result = "{WheelDown} Key is invalid"
        End If
        If objEmpty = False AndAlso IsNothing(myControls.WheelDown.ButtonName) Then
            objEmpty = True
            result = "{WheelDown} Key's ButtonName is invalid"
        Else
            If objEmpty = False Then
                myControls.WheelDown.XBoxButton = "xbox_rt"
            End If
        End If


        '' Left Stick Up
        If objEmpty = False AndAlso IsNothing(myControls.CtrlW) Then
            objEmpty = True
            result = "{CtrlW} Key is invalid"
        End If
        If objEmpty = False AndAlso IsNothing(myControls.CtrlW.ButtonName) Then
            objEmpty = True
            result = "{CtrlW} Key's ButtonName is invalid"
        Else
            If objEmpty = False Then
                myControls.CtrlW.XBoxButton = "xbox_lStickup"
            End If
        End If

        '' Left Stick Down
        If objEmpty = False AndAlso IsNothing(myControls.CtrlS) Then
            objEmpty = True
            result = "{CtrlS} Key is invalid"
        End If
        If objEmpty = False AndAlso IsNothing(myControls.CtrlS.ButtonName) Then
            objEmpty = True
            result = "{CtrlS} Key's ButtonName is invalid"
        Else
            If objEmpty = False Then
                myControls.CtrlS.XBoxButton = "xbox_lStickDown"
            End If
        End If

        '' Left Stick Left
        If objEmpty = False AndAlso IsNothing(myControls.CtrlA) Then
            objEmpty = True
            result = "{CtrlA} Key is invalid"
        End If
        If objEmpty = False AndAlso IsNothing(myControls.CtrlA.ButtonName) Then
            objEmpty = True
            result = "{CtrlA} Key's ButtonName is invalid"
        Else
            If objEmpty = False Then
                myControls.CtrlA.XBoxButton = "xbox_lStickLeft"
            End If
        End If

        '' Left Stick Right
        If objEmpty = False AndAlso IsNothing(myControls.CtrlD) Then
            objEmpty = True
            result = "{CtrlD} Key is invalid"
        End If
        If objEmpty = False AndAlso IsNothing(myControls.CtrlD.ButtonName) Then
            objEmpty = True
            result = "{CtrlD} Key's ButtonName is invalid"
        Else
            If objEmpty = False Then
                myControls.CtrlD.XBoxButton = "xbox_lStickRight"
            End If
        End If

        Return result
    End Function


    Private Function OpenByteFile() As String
        Dim oFile As New OpenFileDialog
        Dim byt As Byte() = Nothing
        Dim fileString As String = ""
        Dim strFileData As String = String.Empty
        ' Show open file dialog box
        Dim result? As Boolean = oFile.ShowDialog()

        ' Process open file dialog box results
        If result = True Then
            ' Open document
            Dim strFilename As String = oFile.FileName
            If Not IO.File.Exists(strFilename) Then

            Else
                byt = IO.File.ReadAllBytes(strFilename)
                fileString = Convert.ToBase64String(byt)
            End If
        End If
        Return fileString
    End Function
    Private Sub SaveByteFile(fileString As String)
        Dim sFile As New SaveFileDialog
        sFile.DefaultExt = ".txt"
        sFile.Filter = "Text File (.txt)|*.txt" ' Filter files by extension
        sFile.FileName = "dataFile.txt"
        sFile.OverwritePrompt = False

        ' Show open file dialog box
        Dim result = sFile.ShowDialog()

        Dim strMySavePath As String = sFile.FileName
        ' Process open file dialog box results
        If result = DialogResult.OK Then

            Dim writeFileResult = WriteToFile(fileString, strMySavePath)

            If writeFileResult Then
                MessageBox.Show("Save Successful", "Relayer Action Mapper", MessageBoxButtons.OK)
            End If
        Else
            Exit Sub
        End If
    End Sub

    Private Sub btnTest_Click(sender As Object, e As EventArgs) Handles btnTest.Click

        Dim fileString As String = OpenByteFile()

        If Not String.IsNullOrWhiteSpace(fileString) Then

            SaveByteFile(fileString)
        End If

    End Sub

End Class
