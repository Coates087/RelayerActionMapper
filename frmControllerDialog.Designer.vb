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
        Me.components = New System.ComponentModel.Container()
        Me.panPreview = New System.Windows.Forms.Panel()
        Me.rTxtMessage = New System.Windows.Forms.RichTextBox()
        Me.btnClose = New System.Windows.Forms.Button()
        Me.btnYes = New System.Windows.Forms.Button()
        Me.tip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.panPreview.SuspendLayout()
        Me.SuspendLayout()
        '
        'panPreview
        '
        Me.panPreview.Controls.Add(Me.rTxtMessage)
        Me.panPreview.Location = New System.Drawing.Point(12, 12)
        Me.panPreview.Name = "panPreview"
        Me.panPreview.Size = New System.Drawing.Size(734, 327)
        Me.panPreview.TabIndex = 0
        '
        'rTxtMessage
        '
        Me.rTxtMessage.BackColor = System.Drawing.SystemColors.Control
        Me.rTxtMessage.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.rTxtMessage.DetectUrls = False
        Me.rTxtMessage.Font = New System.Drawing.Font("Consolas", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rTxtMessage.Location = New System.Drawing.Point(4, 3)
        Me.rTxtMessage.Name = "rTxtMessage"
        Me.rTxtMessage.Size = New System.Drawing.Size(727, 310)
        Me.rTxtMessage.TabIndex = 0
        Me.rTxtMessage.Text = ""
        '
        'btnClose
        '
        Me.btnClose.DialogResult = System.Windows.Forms.DialogResult.No
        Me.btnClose.Location = New System.Drawing.Point(408, 366)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(128, 29)
        Me.btnClose.TabIndex = 2
        Me.btnClose.Text = "No"
        Me.btnClose.UseVisualStyleBackColor = True
        '
        'btnYes
        '
        Me.btnYes.DialogResult = System.Windows.Forms.DialogResult.Yes
        Me.btnYes.Location = New System.Drawing.Point(109, 366)
        Me.btnYes.Name = "btnYes"
        Me.btnYes.Size = New System.Drawing.Size(128, 29)
        Me.btnYes.TabIndex = 1
        Me.btnYes.Text = "Yes"
        Me.btnYes.UseVisualStyleBackColor = True
        '
        'frmControllerDialog
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.AutoSize = True
        Me.ClientSize = New System.Drawing.Size(803, 417)
        Me.ControlBox = False
        Me.Controls.Add(Me.btnYes)
        Me.Controls.Add(Me.btnClose)
        Me.Controls.Add(Me.panPreview)
        Me.Name = "frmControllerDialog"
        Me.Text = "Information"
        Me.panPreview.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents panPreview As Panel
    Friend WithEvents btnClose As Button
    Friend WithEvents rTxtMessage As RichTextBox
    Friend WithEvents btnYes As Button
    Friend WithEvents tip1 As ToolTip
End Class
