<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmControllerDialog
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
        components = New ComponentModel.Container()
        panPreview = New Panel()
        rTxtMessage = New RichTextBox()
        btnClose = New Button()
        btnYes = New Button()
        tip1 = New ToolTip(components)
        panPreview.SuspendLayout()
        SuspendLayout()
        ' 
        ' panPreview
        ' 
        panPreview.Controls.Add(rTxtMessage)
        panPreview.Location = New Point(14, 14)
        panPreview.Margin = New Padding(4, 3, 4, 3)
        panPreview.Name = "panPreview"
        panPreview.Size = New Size(856, 293)
        panPreview.TabIndex = 0
        ' 
        ' rTxtMessage
        ' 
        rTxtMessage.BackColor = SystemColors.ControlLight
        rTxtMessage.BorderStyle = BorderStyle.None
        rTxtMessage.DetectUrls = False
        rTxtMessage.Font = New Font("Consolas", 15.75F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        rTxtMessage.Location = New Point(5, 3)
        rTxtMessage.Margin = New Padding(4, 3, 4, 3)
        rTxtMessage.Name = "rTxtMessage"
        rTxtMessage.Size = New Size(848, 269)
        rTxtMessage.TabIndex = 0
        rTxtMessage.Text = ""
        ' 
        ' btnClose
        ' 
        btnClose.DialogResult = DialogResult.No
        btnClose.Font = New Font("Segoe UI", 18F, FontStyle.Bold)
        btnClose.Location = New Point(457, 328)
        btnClose.Margin = New Padding(4, 3, 4, 3)
        btnClose.Name = "btnClose"
        btnClose.Size = New Size(168, 50)
        btnClose.TabIndex = 2
        btnClose.Text = "No"
        btnClose.UseVisualStyleBackColor = True
        ' 
        ' btnYes
        ' 
        btnYes.DialogResult = DialogResult.Yes
        btnYes.Font = New Font("Segoe UI", 18F, FontStyle.Bold)
        btnYes.Location = New Point(127, 328)
        btnYes.Margin = New Padding(4, 3, 4, 3)
        btnYes.Name = "btnYes"
        btnYes.Size = New Size(161, 50)
        btnYes.TabIndex = 1
        btnYes.Text = "Yes"
        btnYes.UseVisualStyleBackColor = True
        ' 
        ' frmControllerDialog
        ' 
        AutoScaleDimensions = New SizeF(7F, 15F)
        AutoScaleMode = AutoScaleMode.Font
        AutoSize = True
        ClientSize = New Size(937, 399)
        ControlBox = False
        Controls.Add(btnYes)
        Controls.Add(btnClose)
        Controls.Add(panPreview)
        Margin = New Padding(4, 3, 4, 3)
        Name = "frmControllerDialog"
        Text = "Information"
        panPreview.ResumeLayout(False)
        ResumeLayout(False)

    End Sub

    Friend WithEvents panPreview As Panel
    Friend WithEvents btnClose As Button
    Friend WithEvents rTxtMessage As RichTextBox
    Friend WithEvents btnYes As Button
    Friend WithEvents tip1 As ToolTip
End Class
