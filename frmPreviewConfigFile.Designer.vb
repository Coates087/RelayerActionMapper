<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmPreviewConfigFile
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
        panPreview = New Panel()
        Label1 = New Label()
        rTxtJson = New RichTextBox()
        btnClose = New Button()
        chkShowDesc = New CheckBox()
        panPreview.SuspendLayout()
        SuspendLayout()
        ' 
        ' panPreview
        ' 
        panPreview.Controls.Add(chkShowDesc)
        panPreview.Controls.Add(Label1)
        panPreview.Controls.Add(rTxtJson)
        panPreview.Location = New Point(14, 12)
        panPreview.Margin = New Padding(4, 3, 4, 3)
        panPreview.Name = "panPreview"
        panPreview.Size = New Size(856, 400)
        panPreview.TabIndex = 0
        ' 
        ' Label1
        ' 
        Label1.AutoSize = True
        Label1.Font = New Font("Microsoft Sans Serif", 14.25F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        Label1.Location = New Point(4, 18)
        Label1.Margin = New Padding(4, 0, 4, 0)
        Label1.Name = "Label1"
        Label1.Size = New Size(472, 24)
        Label1.TabIndex = 6
        Label1.Text = "Use Ctrl + Mouse Wheel Up/Down to Zoom In/Out"
        ' 
        ' rTxtJson
        ' 
        rTxtJson.Font = New Font("Consolas", 15.75F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        rTxtJson.Location = New Point(5, 49)
        rTxtJson.Margin = New Padding(4, 3, 4, 3)
        rTxtJson.Name = "rTxtJson"
        rTxtJson.Size = New Size(848, 341)
        rTxtJson.TabIndex = 0
        rTxtJson.Text = ""
        ' 
        ' btnClose
        ' 
        btnClose.DialogResult = DialogResult.Cancel
        btnClose.Location = New Point(686, 430)
        btnClose.Margin = New Padding(4, 3, 4, 3)
        btnClose.Name = "btnClose"
        btnClose.Size = New Size(149, 33)
        btnClose.TabIndex = 7
        btnClose.Text = "Close"
        btnClose.UseVisualStyleBackColor = True
        ' 
        ' chkShowDesc
        ' 
        chkShowDesc.AutoSize = True
        chkShowDesc.Location = New Point(649, 3)
        chkShowDesc.Name = "chkShowDesc"
        chkShowDesc.Size = New Size(204, 19)
        chkShowDesc.TabIndex = 14
        chkShowDesc.Text = "Show Description For Each Action"
        chkShowDesc.UseVisualStyleBackColor = True
        ' 
        ' frmPreviewConfigFile
        ' 
        AutoScaleDimensions = New SizeF(7F, 15F)
        AutoScaleMode = AutoScaleMode.Font
        AutoSize = True
        ClientSize = New Size(933, 483)
        ControlBox = False
        Controls.Add(btnClose)
        Controls.Add(panPreview)
        Margin = New Padding(4, 3, 4, 3)
        Name = "frmPreviewConfigFile"
        Text = "Preview Config File"
        panPreview.ResumeLayout(False)
        panPreview.PerformLayout()
        ResumeLayout(False)

    End Sub

    Friend WithEvents panPreview As Panel
    Friend WithEvents btnClose As Button
    Friend WithEvents rTxtJson As RichTextBox
    Friend WithEvents Label1 As Label
    Friend WithEvents chkShowDesc As CheckBox
End Class
