<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class UserInputBox
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
    Me.btnCancel = New System.Windows.Forms.Button
    Me.btnOk = New System.Windows.Forms.Button
    Me.txtJobNumber = New System.Windows.Forms.TextBox
    Me.lblPrompt = New System.Windows.Forms.Label
    Me.SuspendLayout()
    '
    'btnCancel
    '
    Me.btnCancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
    Me.btnCancel.Location = New System.Drawing.Point(164, 89)
    Me.btnCancel.Name = "btnCancel"
    Me.btnCancel.Size = New System.Drawing.Size(72, 25)
    Me.btnCancel.TabIndex = 8
    Me.btnCancel.Text = "Cancel"
    '
    'btnOk
    '
    Me.btnOk.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.btnOk.DialogResult = System.Windows.Forms.DialogResult.OK
    Me.btnOk.Location = New System.Drawing.Point(76, 89)
    Me.btnOk.Name = "btnOk"
    Me.btnOk.Size = New System.Drawing.Size(75, 23)
    Me.btnOk.TabIndex = 7
    Me.btnOk.Text = "OK"
    '
    'txtJobNumber
    '
    Me.txtJobNumber.Location = New System.Drawing.Point(28, 33)
    Me.txtJobNumber.Name = "txtJobNumber"
    Me.txtJobNumber.ShortcutsEnabled = False
    Me.txtJobNumber.Size = New System.Drawing.Size(189, 20)
    Me.txtJobNumber.TabIndex = 6
    '
    'lblPrompt
    '
    Me.lblPrompt.AutoSize = True
    Me.lblPrompt.Location = New System.Drawing.Point(12, 9)
    Me.lblPrompt.Name = "lblPrompt"
    Me.lblPrompt.Size = New System.Drawing.Size(191, 13)
    Me.lblPrompt.TabIndex = 5
    Me.lblPrompt.Text = "Enter a Folder Name to store programs:"
    '
    'UserInputBox
    '
    Me.AcceptButton = Me.btnOk
    Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
    Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
    Me.AutoSize = True
    Me.CancelButton = Me.btnCancel
    Me.ClientSize = New System.Drawing.Size(260, 130)
    Me.Controls.Add(Me.btnCancel)
    Me.Controls.Add(Me.btnOk)
    Me.Controls.Add(Me.txtJobNumber)
    Me.Controls.Add(Me.lblPrompt)
    Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
    Me.Name = "UserInputBox"
    Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
    Me.Text = "UserInputBox"
    Me.ResumeLayout(False)
    Me.PerformLayout()

  End Sub
  Friend WithEvents btnCancel As System.Windows.Forms.Button
    Friend WithEvents btnOk As System.Windows.Forms.Button
    Friend WithEvents txtJobNumber As System.Windows.Forms.TextBox
    Friend WithEvents lblPrompt As System.Windows.Forms.Label
End Class
