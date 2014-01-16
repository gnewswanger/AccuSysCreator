<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FormUserInput
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
    Me.Button2 = New System.Windows.Forms.Button
    Me.Button1 = New System.Windows.Forms.Button
    Me.txtJobNumber = New System.Windows.Forms.TextBox
    Me.lblPrompt = New System.Windows.Forms.Label
    Me.SuspendLayout()
    '
    'Button2
    '
    Me.Button2.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.Button2.DialogResult = System.Windows.Forms.DialogResult.Cancel
    Me.Button2.Location = New System.Drawing.Point(144, 79)
    Me.Button2.Name = "Button2"
    Me.Button2.Size = New System.Drawing.Size(72, 25)
    Me.Button2.TabIndex = 8
    Me.Button2.Text = "Cancel"
    '
    'Button1
    '
    Me.Button1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.Button1.DialogResult = System.Windows.Forms.DialogResult.OK
    Me.Button1.Location = New System.Drawing.Point(57, 79)
    Me.Button1.Name = "Button1"
    Me.Button1.Size = New System.Drawing.Size(75, 23)
    Me.Button1.TabIndex = 7
    Me.Button1.Text = "OK"
    '
    'txtJobNumber
    '
    Me.txtJobNumber.Location = New System.Drawing.Point(28, 33)
    Me.txtJobNumber.Name = "txtJobNumber"
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
    'FormUserInput
    '
    Me.AcceptButton = Me.Button1
    Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
    Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
    Me.CancelButton = Me.Button2
    Me.ClientSize = New System.Drawing.Size(238, 114)
    Me.Controls.Add(Me.Button2)
    Me.Controls.Add(Me.Button1)
    Me.Controls.Add(Me.txtJobNumber)
    Me.Controls.Add(Me.lblPrompt)
    Me.Name = "FormUserInput"
    Me.Text = "User Input"
    Me.ResumeLayout(False)
    Me.PerformLayout()

  End Sub
    Friend WithEvents Button2 As System.Windows.Forms.Button
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents txtJobNumber As System.Windows.Forms.TextBox
    Friend WithEvents lblPrompt As System.Windows.Forms.Label
End Class
