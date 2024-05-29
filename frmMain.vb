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
        'TestPS.Visible = False
        'TestXbox.Visible = False

        assemblyName = "RelayerActionMapper" ''anAssemblyName.Name
        lblFile.Text = ""
        ClearSaveLabel()
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

        'Dim byt As Byte() = Nothing
        'Dim fileString As String = ""
        'fileString = "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"

        ''byt = IO.File.ReadAllBytes("C:\Users\LoCo\Desktop\Relayer Temp Folder\Test doc.docx")
        ''fileString = Convert.ToBase64String(byt)


        'byt = Convert.FromBase64String(fileString)

        'IO.File.WriteAllBytes("C:\Users\LoCo\Desktop\Relayer Temp Folder\.docx", byt)
    End Sub

End Class
