<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmMain
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmMain))
        btnLoadFile = New Button()
        btnDefaultFile = New Button()
        btnDefaultGamePadFile = New Button()
        btnDefaultKBMFile = New Button()
        btnOpenControls = New Button()
        lblFile = New Label()
        btnExit = New Button()
        btnPreviewConfig = New Button()
        lblUnSave = New Label()
        btnSave = New Button()
        btnTest = New Button()
        chkOverride = New CheckBox()
        gboxControllerMode = New GroupBox()
        rbnPS = New RadioButton()
        rbnXbox = New RadioButton()
        btnPSGamePadFile = New Button()
        TestPS = New Button()
        TestXbox = New Button()
        btnHelp = New Button()
        gboxControllerMode.SuspendLayout()
        SuspendLayout()
        ' 
        ' btnLoadFile
        ' 
        btnLoadFile.Location = New Point(153, 87)
        btnLoadFile.Margin = New Padding(4, 3, 4, 3)
        btnLoadFile.Name = "btnLoadFile"
        btnLoadFile.Size = New Size(133, 33)
        btnLoadFile.TabIndex = 0
        btnLoadFile.Text = "Load File"
        btnLoadFile.UseVisualStyleBackColor = True
        ' 
        ' btnDefaultFile
        ' 
        btnDefaultFile.Location = New Point(350, 140)
        btnDefaultFile.Margin = New Padding(4, 3, 4, 3)
        btnDefaultFile.Name = "btnDefaultFile"
        btnDefaultFile.Size = New Size(244, 33)
        btnDefaultFile.TabIndex = 3
        btnDefaultFile.Text = "Create Default Config File"
        btnDefaultFile.UseVisualStyleBackColor = True
        ' 
        ' btnDefaultGamePadFile
        ' 
        btnDefaultGamePadFile.Location = New Point(350, 180)
        btnDefaultGamePadFile.Margin = New Padding(4, 3, 4, 3)
        btnDefaultGamePadFile.Name = "btnDefaultGamePadFile"
        btnDefaultGamePadFile.Size = New Size(244, 37)
        btnDefaultGamePadFile.TabIndex = 4
        btnDefaultGamePadFile.Text = "Create Xbox Game Pad Config File"
        btnDefaultGamePadFile.UseVisualStyleBackColor = True
        ' 
        ' btnDefaultKBMFile
        ' 
        btnDefaultKBMFile.Location = New Point(350, 265)
        btnDefaultKBMFile.Margin = New Padding(4, 3, 4, 3)
        btnDefaultKBMFile.Name = "btnDefaultKBMFile"
        btnDefaultKBMFile.Size = New Size(244, 37)
        btnDefaultKBMFile.TabIndex = 5
        btnDefaultKBMFile.Text = "Create Keyboard Config File"
        btnDefaultKBMFile.UseVisualStyleBackColor = True
        ' 
        ' btnOpenControls
        ' 
        btnOpenControls.Location = New Point(312, 87)
        btnOpenControls.Margin = New Padding(4, 3, 4, 3)
        btnOpenControls.Name = "btnOpenControls"
        btnOpenControls.Size = New Size(133, 33)
        btnOpenControls.TabIndex = 6
        btnOpenControls.Text = "Edit Controls"
        btnOpenControls.UseVisualStyleBackColor = True
        ' 
        ' lblFile
        ' 
        lblFile.BorderStyle = BorderStyle.FixedSingle
        lblFile.Font = New Font("Segoe UI", 9.75F)
        lblFile.Location = New Point(14, 47)
        lblFile.Margin = New Padding(4, 0, 4, 0)
        lblFile.Name = "lblFile"
        lblFile.Size = New Size(599, 23)
        lblFile.TabIndex = 7
        lblFile.Text = "<sample>"
        ' 
        ' btnExit
        ' 
        btnExit.DialogResult = DialogResult.Cancel
        btnExit.Location = New Point(518, 329)
        btnExit.Margin = New Padding(4, 3, 4, 3)
        btnExit.Name = "btnExit"
        btnExit.Size = New Size(96, 35)
        btnExit.TabIndex = 8
        btnExit.Text = "Exit"
        btnExit.UseVisualStyleBackColor = True
        ' 
        ' btnPreviewConfig
        ' 
        btnPreviewConfig.Location = New Point(461, 87)
        btnPreviewConfig.Margin = New Padding(4, 3, 4, 3)
        btnPreviewConfig.Name = "btnPreviewConfig"
        btnPreviewConfig.Size = New Size(133, 33)
        btnPreviewConfig.TabIndex = 9
        btnPreviewConfig.Text = "Preview Config File"
        btnPreviewConfig.UseVisualStyleBackColor = True
        ' 
        ' lblUnSave
        ' 
        lblUnSave.BorderStyle = BorderStyle.FixedSingle
        lblUnSave.Font = New Font("Segoe UI", 9.75F)
        lblUnSave.ForeColor = Color.Crimson
        lblUnSave.Location = New Point(1, 400)
        lblUnSave.Margin = New Padding(4, 0, 4, 0)
        lblUnSave.Name = "lblUnSave"
        lblUnSave.Size = New Size(566, 23)
        lblUnSave.TabIndex = 10
        lblUnSave.Text = "<sample>"
        ' 
        ' btnSave
        ' 
        btnSave.Location = New Point(153, 140)
        btnSave.Margin = New Padding(4, 3, 4, 3)
        btnSave.Name = "btnSave"
        btnSave.Size = New Size(133, 33)
        btnSave.TabIndex = 11
        btnSave.Text = "Save File"
        btnSave.UseVisualStyleBackColor = True
        ' 
        ' btnTest
        ' 
        btnTest.Location = New Point(191, 299)
        btnTest.Margin = New Padding(4, 3, 4, 3)
        btnTest.Name = "btnTest"
        btnTest.Size = New Size(133, 33)
        btnTest.TabIndex = 12
        btnTest.Text = "Save File2"
        btnTest.UseVisualStyleBackColor = True
        btnTest.Visible = False
        ' 
        ' chkOverride
        ' 
        chkOverride.AutoSize = True
        chkOverride.Location = New Point(385, 12)
        chkOverride.Name = "chkOverride"
        chkOverride.Size = New Size(132, 19)
        chkOverride.TabIndex = 13
        chkOverride.Text = "Always Override File"
        chkOverride.UseVisualStyleBackColor = True
        ' 
        ' gboxControllerMode
        ' 
        gboxControllerMode.Controls.Add(rbnPS)
        gboxControllerMode.Controls.Add(rbnXbox)
        gboxControllerMode.Location = New Point(11, 5)
        gboxControllerMode.Margin = New Padding(4, 3, 4, 3)
        gboxControllerMode.Name = "gboxControllerMode"
        gboxControllerMode.Padding = New Padding(4, 3, 4, 3)
        gboxControllerMode.Size = New Size(368, 32)
        gboxControllerMode.TabIndex = 14
        gboxControllerMode.TabStop = False
        gboxControllerMode.Text = "View Controls"
        ' 
        ' rbnPS
        ' 
        rbnPS.AutoSize = True
        rbnPS.Location = New Point(127, 14)
        rbnPS.Margin = New Padding(4, 3, 4, 3)
        rbnPS.Name = "rbnPS"
        rbnPS.Size = New Size(72, 19)
        rbnPS.TabIndex = 13
        rbnPS.Text = "PS Mode"
        rbnPS.UseVisualStyleBackColor = True
        ' 
        ' rbnXbox
        ' 
        rbnXbox.AutoSize = True
        rbnXbox.Checked = True
        rbnXbox.Location = New Point(8, 14)
        rbnXbox.Margin = New Padding(4, 3, 4, 3)
        rbnXbox.Name = "rbnXbox"
        rbnXbox.Size = New Size(86, 19)
        rbnXbox.TabIndex = 12
        rbnXbox.TabStop = True
        rbnXbox.Text = "Xbox Mode"
        rbnXbox.UseVisualStyleBackColor = True
        ' 
        ' btnPSGamePadFile
        ' 
        btnPSGamePadFile.Location = New Point(350, 222)
        btnPSGamePadFile.Margin = New Padding(4, 3, 4, 3)
        btnPSGamePadFile.Name = "btnPSGamePadFile"
        btnPSGamePadFile.Size = New Size(244, 37)
        btnPSGamePadFile.TabIndex = 15
        btnPSGamePadFile.Text = "Create PlayStation Game Pad Config File"
        btnPSGamePadFile.UseVisualStyleBackColor = True
        ' 
        ' TestPS
        ' 
        TestPS.Location = New Point(99, 241)
        TestPS.Margin = New Padding(4, 3, 4, 3)
        TestPS.Name = "TestPS"
        TestPS.Size = New Size(162, 37)
        TestPS.TabIndex = 16
        TestPS.Text = "Test PlayStation Config"
        TestPS.UseVisualStyleBackColor = True
        ' 
        ' TestXbox
        ' 
        TestXbox.Location = New Point(99, 198)
        TestXbox.Margin = New Padding(4, 3, 4, 3)
        TestXbox.Name = "TestXbox"
        TestXbox.Size = New Size(162, 37)
        TestXbox.TabIndex = 17
        TestXbox.Text = "Test Xbox Config"
        TestXbox.UseVisualStyleBackColor = True
        ' 
        ' btnHelp
        ' 
        btnHelp.BackColor = Color.LightSkyBlue
        btnHelp.FlatStyle = FlatStyle.Flat
        btnHelp.Location = New Point(543, 9)
        btnHelp.Margin = New Padding(4, 3, 4, 3)
        btnHelp.Name = "btnHelp"
        btnHelp.Size = New Size(75, 28)
        btnHelp.TabIndex = 18
        btnHelp.Text = "Help"
        btnHelp.UseVisualStyleBackColor = False
        ' 
        ' frmMain
        ' 
        AutoScaleDimensions = New SizeF(7F, 15F)
        AutoScaleMode = AutoScaleMode.Font
        AutoSize = True
        ClientSize = New Size(631, 434)
        Controls.Add(btnHelp)
        Controls.Add(TestXbox)
        Controls.Add(TestPS)
        Controls.Add(btnPSGamePadFile)
        Controls.Add(gboxControllerMode)
        Controls.Add(chkOverride)
        Controls.Add(btnTest)
        Controls.Add(btnSave)
        Controls.Add(lblUnSave)
        Controls.Add(btnPreviewConfig)
        Controls.Add(btnExit)
        Controls.Add(lblFile)
        Controls.Add(btnOpenControls)
        Controls.Add(btnDefaultKBMFile)
        Controls.Add(btnDefaultGamePadFile)
        Controls.Add(btnDefaultFile)
        Controls.Add(btnLoadFile)
        Icon = CType(resources.GetObject("$this.Icon"), Icon)
        Margin = New Padding(4, 3, 4, 3)
        Name = "frmMain"
        Text = "Relayer Action Mapper"
        gboxControllerMode.ResumeLayout(False)
        gboxControllerMode.PerformLayout()
        ResumeLayout(False)
        PerformLayout()

    End Sub

    Friend WithEvents btnLoadFile As Button
    Friend WithEvents btnDefaultFile As Button
    Friend WithEvents btnDefaultGamePadFile As Button
    Friend WithEvents btnDefaultKBMFile As Button
    Friend WithEvents btnOpenControls As Button
    Friend WithEvents lblFile As Label
    Friend WithEvents btnExit As Button
    Friend WithEvents btnPreviewConfig As Button
    Friend WithEvents lblUnSave As Label
    Friend WithEvents btnSave As Button
    Friend WithEvents btnTest As Button
    Friend WithEvents chkOverride As CheckBox
    Friend WithEvents gboxControllerMode As GroupBox
    Friend WithEvents rbnPS As RadioButton
    Friend WithEvents rbnXbox As RadioButton
    Friend WithEvents btnPSGamePadFile As Button
    Friend WithEvents TestPS As Button
    Friend WithEvents TestXbox As Button
    Friend WithEvents btnHelp As Button
End Class
