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
        '//     "-ps",
        '    //     // "-Load", "C:\\Users\\User\\Desktop\\Relayer Temp Folder\\KeyConfig45.json",
        '    //     "-save", "C:\\Users\\User\\Desktop\\Relayer Temp Folder\"",
        '    //     "-overridesave"

        Dim arguments As String() = Environment.GetCommandLineArgs()

        Console.WriteLine("GetCommandLineArgs: {0}", String.Join(", ", arguments))
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
