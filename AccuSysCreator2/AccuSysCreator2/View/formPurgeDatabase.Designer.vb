<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class formPurgeDatabase
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
    Me.DateTimePicker1 = New System.Windows.Forms.DateTimePicker
    Me.txtWarning = New System.Windows.Forms.RichTextBox
    Me.TextBox1 = New System.Windows.Forms.TextBox
    Me.btnYes = New System.Windows.Forms.Button
    Me.btnNo = New System.Windows.Forms.Button
    Me.GroupBox1 = New System.Windows.Forms.GroupBox
    Me.radio90Days = New System.Windows.Forms.RadioButton
    Me.radio180Days = New System.Windows.Forms.RadioButton
    Me.radio270Days = New System.Windows.Forms.RadioButton
    Me.radioCustomDays = New System.Windows.Forms.RadioButton
    Me.GroupBox1.SuspendLayout()
    Me.SuspendLayout()
    '
    'DateTimePicker1
    '
    Me.DateTimePicker1.Location = New System.Drawing.Point(37, 50)
    Me.DateTimePicker1.Name = "DateTimePicker1"
    Me.DateTimePicker1.Size = New System.Drawing.Size(208, 20)
    Me.DateTimePicker1.TabIndex = 0
    '
    'txtWarning
    '
    Me.txtWarning.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.txtWarning.BorderStyle = System.Windows.Forms.BorderStyle.None
    Me.txtWarning.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.txtWarning.Location = New System.Drawing.Point(23, 212)
    Me.txtWarning.Name = "txtWarning"
    Me.txtWarning.ReadOnly = True
    Me.txtWarning.Size = New System.Drawing.Size(236, 88)
    Me.txtWarning.TabIndex = 1
    Me.txtWarning.Text = """You are about to permanently delete all data that has not been updated in the pa" & _
        "st 180 days! " & Global.Microsoft.VisualBasic.ChrW(10) & " " & Global.Microsoft.VisualBasic.ChrW(10) & "Do you wish to continue?"""
    '
    'TextBox1
    '
    Me.TextBox1.BorderStyle = System.Windows.Forms.BorderStyle.None
    Me.TextBox1.Location = New System.Drawing.Point(12, 12)
    Me.TextBox1.Multiline = True
    Me.TextBox1.Name = "TextBox1"
    Me.TextBox1.ReadOnly = True
    Me.TextBox1.Size = New System.Drawing.Size(178, 32)
    Me.TextBox1.TabIndex = 3
    Me.TextBox1.Text = "Enter a deletion date at least 90 days prior to today."
    '
    'btnYes
    '
    Me.btnYes.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.btnYes.DialogResult = System.Windows.Forms.DialogResult.Yes
    Me.btnYes.Location = New System.Drawing.Point(107, 306)
    Me.btnYes.Name = "btnYes"
    Me.btnYes.Size = New System.Drawing.Size(75, 23)
    Me.btnYes.TabIndex = 4
    Me.btnYes.Text = "Yes"
    Me.btnYes.UseVisualStyleBackColor = True
    '
    'btnNo
    '
    Me.btnNo.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.btnNo.DialogResult = System.Windows.Forms.DialogResult.No
    Me.btnNo.Location = New System.Drawing.Point(188, 306)
    Me.btnNo.Name = "btnNo"
    Me.btnNo.Size = New System.Drawing.Size(75, 23)
    Me.btnNo.TabIndex = 5
    Me.btnNo.Text = "No"
    Me.btnNo.UseVisualStyleBackColor = True
    '
    'GroupBox1
    '
    Me.GroupBox1.Controls.Add(Me.radioCustomDays)
    Me.GroupBox1.Controls.Add(Me.radio270Days)
    Me.GroupBox1.Controls.Add(Me.radio180Days)
    Me.GroupBox1.Controls.Add(Me.radio90Days)
    Me.GroupBox1.Location = New System.Drawing.Point(75, 76)
    Me.GroupBox1.Name = "GroupBox1"
    Me.GroupBox1.Size = New System.Drawing.Size(132, 118)
    Me.GroupBox1.TabIndex = 6
    Me.GroupBox1.TabStop = False
    Me.GroupBox1.Text = "Presets"
    '
    'radio90Days
    '
    Me.radio90Days.AutoSize = True
    Me.radio90Days.Location = New System.Drawing.Point(20, 19)
    Me.radio90Days.Name = "radio90Days"
    Me.radio90Days.Size = New System.Drawing.Size(65, 17)
    Me.radio90Days.TabIndex = 0
    Me.radio90Days.TabStop = True
    Me.radio90Days.Text = "90 days."
    Me.radio90Days.UseVisualStyleBackColor = True
    '
    'radio180Days
    '
    Me.radio180Days.AutoSize = True
    Me.radio180Days.Checked = True
    Me.radio180Days.Location = New System.Drawing.Point(20, 42)
    Me.radio180Days.Name = "radio180Days"
    Me.radio180Days.Size = New System.Drawing.Size(71, 17)
    Me.radio180Days.TabIndex = 1
    Me.radio180Days.TabStop = True
    Me.radio180Days.Text = "180 days."
    Me.radio180Days.UseVisualStyleBackColor = True
    '
    'radio270Days
    '
    Me.radio270Days.AutoSize = True
    Me.radio270Days.Location = New System.Drawing.Point(20, 65)
    Me.radio270Days.Name = "radio270Days"
    Me.radio270Days.Size = New System.Drawing.Size(71, 17)
    Me.radio270Days.TabIndex = 2
    Me.radio270Days.TabStop = True
    Me.radio270Days.Text = "270 days."
    Me.radio270Days.UseVisualStyleBackColor = True
    '
    'radioCustomDays
    '
    Me.radioCustomDays.AutoSize = True
    Me.radioCustomDays.Enabled = False
    Me.radioCustomDays.Location = New System.Drawing.Point(20, 88)
    Me.radioCustomDays.Name = "radioCustomDays"
    Me.radioCustomDays.Size = New System.Drawing.Size(63, 17)
    Me.radioCustomDays.TabIndex = 3
    Me.radioCustomDays.Text = "Custom."
    Me.radioCustomDays.UseVisualStyleBackColor = True
    '
    'formPurgeDatabase
    '
    Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
    Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
    Me.ClientSize = New System.Drawing.Size(281, 341)
    Me.Controls.Add(Me.GroupBox1)
    Me.Controls.Add(Me.btnNo)
    Me.Controls.Add(Me.btnYes)
    Me.Controls.Add(Me.TextBox1)
    Me.Controls.Add(Me.txtWarning)
    Me.Controls.Add(Me.DateTimePicker1)
    Me.Name = "formPurgeDatabase"
    Me.Text = "formPurgeDatabase"
    Me.GroupBox1.ResumeLayout(False)
    Me.GroupBox1.PerformLayout()
    Me.ResumeLayout(False)
    Me.PerformLayout()

  End Sub
  Friend WithEvents DateTimePicker1 As System.Windows.Forms.DateTimePicker
  Friend WithEvents txtWarning As System.Windows.Forms.RichTextBox
  Friend WithEvents TextBox1 As System.Windows.Forms.TextBox
  Friend WithEvents btnYes As System.Windows.Forms.Button
  Friend WithEvents btnNo As System.Windows.Forms.Button
  Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
  Friend WithEvents radio270Days As System.Windows.Forms.RadioButton
  Friend WithEvents radio180Days As System.Windows.Forms.RadioButton
  Friend WithEvents radio90Days As System.Windows.Forms.RadioButton
  Friend WithEvents radioCustomDays As System.Windows.Forms.RadioButton
End Class
