<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmOpenFile
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
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
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Label1 = New Label()
        txtFileName = New TextBox()
        btnLoadFile = New Button()
        btnCancel = New Button()
        btnOk = New Button()
        lblUnSave = New Label()
        SuspendLayout()
        ' 
        ' Label1
        ' 
        Label1.AutoSize = True
        Label1.Location = New Point(21, 38)
        Label1.Margin = New Padding(4, 0, 4, 0)
        Label1.Name = "Label1"
        Label1.Size = New Size(57, 15)
        Label1.TabIndex = 5
        Label1.Text = "FileName"
        ' 
        ' txtFileName
        ' 
        txtFileName.BackColor = SystemColors.ButtonHighlight
        txtFileName.Location = New Point(21, 59)
        txtFileName.Margin = New Padding(4, 3, 4, 3)
        txtFileName.Name = "txtFileName"
        txtFileName.ReadOnly = True
        txtFileName.Size = New Size(434, 23)
        txtFileName.TabIndex = 4
        ' 
        ' btnLoadFile
        ' 
        btnLoadFile.Location = New Point(474, 47)
        btnLoadFile.Margin = New Padding(4, 3, 4, 3)
        btnLoadFile.Name = "btnLoadFile"
        btnLoadFile.Size = New Size(133, 35)
        btnLoadFile.TabIndex = 3
        btnLoadFile.Text = "Load File"
        btnLoadFile.UseVisualStyleBackColor = True
        ' 
        ' btnCancel
        ' 
        btnCancel.AutoSize = True
        btnCancel.DialogResult = DialogResult.Cancel
        btnCancel.Location = New Point(238, 120)
        btnCancel.Margin = New Padding(4, 3, 4, 3)
        btnCancel.Name = "btnCancel"
        btnCancel.Size = New Size(133, 35)
        btnCancel.TabIndex = 6
        btnCancel.Text = "Cancel"
        btnCancel.UseVisualStyleBackColor = True
        ' 
        ' btnOk
        ' 
        btnOk.DialogResult = DialogResult.OK
        btnOk.Location = New Point(41, 120)
        btnOk.Margin = New Padding(4, 3, 4, 3)
        btnOk.Name = "btnOk"
        btnOk.Size = New Size(133, 35)
        btnOk.TabIndex = 7
        btnOk.Text = "OK"
        btnOk.UseVisualStyleBackColor = True
        ' 
        ' lblUnSave
        ' 
        lblUnSave.BorderStyle = BorderStyle.FixedSingle
        lblUnSave.Font = New Font("Segoe UI", 9.75F)
        lblUnSave.ForeColor = Color.Crimson
        lblUnSave.Location = New Point(21, 171)
        lblUnSave.Margin = New Padding(4, 0, 4, 0)
        lblUnSave.Name = "lblUnSave"
        lblUnSave.Size = New Size(566, 23)
        lblUnSave.TabIndex = 11
        lblUnSave.Text = "<sample>"
        ' 
        ' frmOpenFile
        ' 
        AutoScaleDimensions = New SizeF(7F, 15F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(623, 203)
        ControlBox = False
        Controls.Add(lblUnSave)
        Controls.Add(btnOk)
        Controls.Add(btnCancel)
        Controls.Add(Label1)
        Controls.Add(txtFileName)
        Controls.Add(btnLoadFile)
        Margin = New Padding(4, 3, 4, 3)
        Name = "frmOpenFile"
        Text = "Open File"
        ResumeLayout(False)
        PerformLayout()

    End Sub

    Friend WithEvents Label1 As Label
    Friend WithEvents txtFileName As TextBox
    Friend WithEvents btnLoadFile As Button
    Friend WithEvents btnCancel As Button
    Friend WithEvents btnOk As Button
    Friend WithEvents lblUnSave As Label
End Class
