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
        Me.panPreview = New System.Windows.Forms.Panel()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.rTxtJson = New System.Windows.Forms.RichTextBox()
        Me.btnClose = New System.Windows.Forms.Button()
        Me.panPreview.SuspendLayout()
        Me.SuspendLayout()
        '
        'panPreview
        '
        Me.panPreview.Controls.Add(Me.Label1)
        Me.panPreview.Controls.Add(Me.rTxtJson)
        Me.panPreview.Location = New System.Drawing.Point(12, 12)
        Me.panPreview.Name = "panPreview"
        Me.panPreview.Size = New System.Drawing.Size(734, 395)
        Me.panPreview.TabIndex = 0
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(3, 8)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(472, 24)
        Me.Label1.TabIndex = 6
        Me.Label1.Text = "Use Ctrl + Mouse Wheel Up/Down to Zoom In/Out"
        '
        'rTxtJson
        '
        Me.rTxtJson.Font = New System.Drawing.Font("Consolas", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rTxtJson.Location = New System.Drawing.Point(4, 35)
        Me.rTxtJson.Name = "rTxtJson"
        Me.rTxtJson.Size = New System.Drawing.Size(727, 357)
        Me.rTxtJson.TabIndex = 0
        Me.rTxtJson.Text = ""
        '
        'btnClose
        '
        Me.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnClose.Location = New System.Drawing.Point(588, 413)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(128, 29)
        Me.btnClose.TabIndex = 7
        Me.btnClose.Text = "Close"
        Me.btnClose.UseVisualStyleBackColor = True
        '
        'frmPreviewConfigFile
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.AutoSize = True
        Me.ClientSize = New System.Drawing.Size(800, 450)
        Me.ControlBox = False
        Me.Controls.Add(Me.btnClose)
        Me.Controls.Add(Me.panPreview)
        Me.Name = "frmPreviewConfigFile"
        Me.Text = "Preview Config File"
        Me.panPreview.ResumeLayout(False)
        Me.panPreview.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents panPreview As Panel
    Friend WithEvents btnClose As Button
    Friend WithEvents rTxtJson As RichTextBox
    Friend WithEvents Label1 As Label
End Class
