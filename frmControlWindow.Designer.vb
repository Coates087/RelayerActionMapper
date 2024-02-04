<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmControlWindow
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
        Me.components = New System.ComponentModel.Container()
        Me.btnSave = New System.Windows.Forms.Button()
        Me.btnClose = New System.Windows.Forms.Button()
        Me.btnAddKey = New System.Windows.Forms.Button()
        Me.gBoxKeys = New System.Windows.Forms.GroupBox()
        Me.lblAction = New System.Windows.Forms.Label()
        Me.btnRemove = New System.Windows.Forms.Button()
        Me.picBoxButton = New System.Windows.Forms.PictureBox()
        Me.cboDefaultKey = New System.Windows.Forms.ComboBox()
        Me.panKeys = New System.Windows.Forms.Panel()
        Me.tip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.gBoxKeys.SuspendLayout()
        CType(Me.picBoxButton, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.panKeys.SuspendLayout()
        Me.SuspendLayout()
        '
        'btnSave
        '
        Me.btnSave.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.btnSave.Location = New System.Drawing.Point(462, 221)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(83, 26)
        Me.btnSave.TabIndex = 13
        Me.btnSave.Text = "Save"
        Me.btnSave.UseVisualStyleBackColor = True
        '
        'btnClose
        '
        Me.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnClose.Location = New System.Drawing.Point(462, 253)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(83, 26)
        Me.btnClose.TabIndex = 12
        Me.btnClose.Text = "Cancel"
        Me.btnClose.UseVisualStyleBackColor = True
        '
        'btnAddKey
        '
        Me.btnAddKey.Location = New System.Drawing.Point(462, 42)
        Me.btnAddKey.Name = "btnAddKey"
        Me.btnAddKey.Size = New System.Drawing.Size(64, 32)
        Me.btnAddKey.TabIndex = 15
        Me.btnAddKey.Text = "Add Key"
        Me.btnAddKey.UseVisualStyleBackColor = True
        '
        'gBoxKeys
        '
        Me.gBoxKeys.AutoSize = True
        Me.gBoxKeys.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.gBoxKeys.Controls.Add(Me.lblAction)
        Me.gBoxKeys.Controls.Add(Me.btnRemove)
        Me.gBoxKeys.Controls.Add(Me.picBoxButton)
        Me.gBoxKeys.Controls.Add(Me.cboDefaultKey)
        Me.gBoxKeys.Location = New System.Drawing.Point(11, 10)
        Me.gBoxKeys.Name = "gBoxKeys"
        Me.gBoxKeys.Size = New System.Drawing.Size(409, 90)
        Me.gBoxKeys.TabIndex = 16
        Me.gBoxKeys.TabStop = False
        Me.gBoxKeys.Text = "Keys"
        '
        'lblAction
        '
        Me.lblAction.AutoSize = True
        Me.lblAction.Location = New System.Drawing.Point(66, 22)
        Me.lblAction.Name = "lblAction"
        Me.lblAction.Size = New System.Drawing.Size(38, 13)
        Me.lblAction.TabIndex = 7
        Me.lblAction.Text = "Button"
        '
        'btnRemove
        '
        Me.btnRemove.Location = New System.Drawing.Point(339, 39)
        Me.btnRemove.Name = "btnRemove"
        Me.btnRemove.Size = New System.Drawing.Size(64, 32)
        Me.btnRemove.TabIndex = 16
        Me.btnRemove.Tag = "btn-remove-0"
        Me.btnRemove.Text = "Remove"
        Me.btnRemove.UseVisualStyleBackColor = True
        Me.btnRemove.Visible = False
        '
        'picBoxButton
        '
        Me.picBoxButton.Location = New System.Drawing.Point(8, 22)
        Me.picBoxButton.Name = "picBoxButton"
        Me.picBoxButton.Size = New System.Drawing.Size(52, 48)
        Me.picBoxButton.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.picBoxButton.TabIndex = 9
        Me.picBoxButton.TabStop = False
        '
        'cboDefaultKey
        '
        Me.cboDefaultKey.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboDefaultKey.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboDefaultKey.FormattingEnabled = True
        Me.cboDefaultKey.Location = New System.Drawing.Point(66, 38)
        Me.cboDefaultKey.Name = "cboDefaultKey"
        Me.cboDefaultKey.Size = New System.Drawing.Size(267, 32)
        Me.cboDefaultKey.TabIndex = 8
        Me.cboDefaultKey.Tag = "key-0"
        '
        'panKeys
        '
        Me.panKeys.AutoScroll = True
        Me.panKeys.Controls.Add(Me.gBoxKeys)
        Me.panKeys.Location = New System.Drawing.Point(1, 2)
        Me.panKeys.Name = "panKeys"
        Me.panKeys.Size = New System.Drawing.Size(455, 297)
        Me.panKeys.TabIndex = 17
        '
        'frmControlWindow
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.AutoSize = True
        Me.ClientSize = New System.Drawing.Size(557, 305)
        Me.ControlBox = False
        Me.Controls.Add(Me.panKeys)
        Me.Controls.Add(Me.btnAddKey)
        Me.Controls.Add(Me.btnSave)
        Me.Controls.Add(Me.btnClose)
        Me.Name = "frmControlWindow"
        Me.Text = "Control Window"
        Me.gBoxKeys.ResumeLayout(False)
        Me.gBoxKeys.PerformLayout()
        CType(Me.picBoxButton, System.ComponentModel.ISupportInitialize).EndInit()
        Me.panKeys.ResumeLayout(False)
        Me.panKeys.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents btnSave As Button
    Friend WithEvents btnClose As Button
    Friend WithEvents btnAddKey As Button
    Friend WithEvents gBoxKeys As GroupBox
    Friend WithEvents lblAction As Label
    Friend WithEvents btnRemove As Button
    Friend WithEvents picBoxButton As PictureBox
    Friend WithEvents cboDefaultKey As ComboBox
    Friend WithEvents panKeys As Panel
    Friend WithEvents tip1 As ToolTip
End Class
