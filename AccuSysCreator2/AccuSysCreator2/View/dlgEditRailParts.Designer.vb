<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class dlgEditRailParts
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
    Me.pnlPartStyle = New System.Windows.Forms.Panel
    Me.comboPartStyle = New System.Windows.Forms.ComboBox
    Me.Label5 = New System.Windows.Forms.Label
    Me.pnlPartName = New System.Windows.Forms.Panel
    Me.txtPartName = New System.Windows.Forms.TextBox
    Me.Label4 = New System.Windows.Forms.Label
    Me.checkIncludeInCutlist = New System.Windows.Forms.CheckBox
    Me.labelTenonFar = New System.Windows.Forms.Label
    Me.labelTenonZero = New System.Windows.Forms.Label
    Me.txtTenonFar = New System.Windows.Forms.TextBox
    Me.txtTenonNear = New System.Windows.Forms.TextBox
    Me.Label2 = New System.Windows.Forms.Label
    Me.Label1 = New System.Windows.Forms.Label
    Me.ckNoHaunchatZero = New System.Windows.Forms.CheckBox
    Me.ckNoHaunchatFarend = New System.Windows.Forms.CheckBox
    Me.Label3 = New System.Windows.Forms.Label
    Me.txtPartLength = New System.Windows.Forms.TextBox
    Me.Label54 = New System.Windows.Forms.Label
    Me.Label53 = New System.Windows.Forms.Label
    Me.txtPartWdth = New System.Windows.Forms.TextBox
    Me.btnOk = New System.Windows.Forms.Button
    Me.btnCancel = New System.Windows.Forms.Button
    Me.pnlPartStyle.SuspendLayout()
    Me.pnlPartName.SuspendLayout()
    Me.SuspendLayout()
    '
    'pnlPartStyle
    '
    Me.pnlPartStyle.Controls.Add(Me.comboPartStyle)
    Me.pnlPartStyle.Controls.Add(Me.Label5)
    Me.pnlPartStyle.Dock = System.Windows.Forms.DockStyle.Top
    Me.pnlPartStyle.Location = New System.Drawing.Point(0, 48)
    Me.pnlPartStyle.Name = "pnlPartStyle"
    Me.pnlPartStyle.Size = New System.Drawing.Size(284, 40)
    Me.pnlPartStyle.TabIndex = 149
    '
    'comboPartStyle
    '
    Me.comboPartStyle.Location = New System.Drawing.Point(93, 8)
    Me.comboPartStyle.Name = "comboPartStyle"
    Me.comboPartStyle.Size = New System.Drawing.Size(152, 21)
    Me.comboPartStyle.TabIndex = 1
    '
    'Label5
    '
    Me.Label5.AutoSize = True
    Me.Label5.Location = New System.Drawing.Point(7, 7)
    Me.Label5.Name = "Label5"
    Me.Label5.Size = New System.Drawing.Size(84, 13)
    Me.Label5.TabIndex = 0
    Me.Label5.Text = "Part Edge Type:"
    '
    'pnlPartName
    '
    Me.pnlPartName.Controls.Add(Me.txtPartName)
    Me.pnlPartName.Controls.Add(Me.Label4)
    Me.pnlPartName.Dock = System.Windows.Forms.DockStyle.Top
    Me.pnlPartName.Location = New System.Drawing.Point(0, 0)
    Me.pnlPartName.Name = "pnlPartName"
    Me.pnlPartName.Size = New System.Drawing.Size(284, 48)
    Me.pnlPartName.TabIndex = 148
    '
    'txtPartName
    '
    Me.txtPartName.Location = New System.Drawing.Point(27, 24)
    Me.txtPartName.Name = "txtPartName"
    Me.txtPartName.Size = New System.Drawing.Size(215, 20)
    Me.txtPartName.TabIndex = 1
    '
    'Label4
    '
    Me.Label4.AutoSize = True
    Me.Label4.Location = New System.Drawing.Point(8, 8)
    Me.Label4.Name = "Label4"
    Me.Label4.Size = New System.Drawing.Size(60, 13)
    Me.Label4.TabIndex = 0
    Me.Label4.Text = "Part Name:"
    '
    'checkIncludeInCutlist
    '
    Me.checkIncludeInCutlist.Location = New System.Drawing.Point(16, 130)
    Me.checkIncludeInCutlist.Name = "checkIncludeInCutlist"
    Me.checkIncludeInCutlist.Size = New System.Drawing.Size(152, 24)
    Me.checkIncludeInCutlist.TabIndex = 171
    Me.checkIncludeInCutlist.Text = "Include In Cutlist"
    '
    'labelTenonFar
    '
    Me.labelTenonFar.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.labelTenonFar.AutoSize = True
    Me.labelTenonFar.Location = New System.Drawing.Point(23, 267)
    Me.labelTenonFar.Name = "labelTenonFar"
    Me.labelTenonFar.Size = New System.Drawing.Size(170, 13)
    Me.labelTenonFar.TabIndex = 170
    Me.labelTenonFar.Text = "Adjusted Tenon Length at Far End"
    '
    'labelTenonZero
    '
    Me.labelTenonZero.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.labelTenonZero.AutoSize = True
    Me.labelTenonZero.Location = New System.Drawing.Point(23, 246)
    Me.labelTenonZero.Name = "labelTenonZero"
    Me.labelTenonZero.Size = New System.Drawing.Size(166, 13)
    Me.labelTenonZero.TabIndex = 169
    Me.labelTenonZero.Text = "Adjusted Tenon Length at Point 0"
    '
    'txtTenonFar
    '
    Me.txtTenonFar.BackColor = System.Drawing.Color.White
    Me.txtTenonFar.Location = New System.Drawing.Point(217, 263)
    Me.txtTenonFar.Name = "txtTenonFar"
    Me.txtTenonFar.Size = New System.Drawing.Size(39, 20)
    Me.txtTenonFar.TabIndex = 168
    Me.txtTenonFar.Text = "0"
    '
    'txtTenonNear
    '
    Me.txtTenonNear.BackColor = System.Drawing.Color.White
    Me.txtTenonNear.Location = New System.Drawing.Point(217, 243)
    Me.txtTenonNear.Name = "txtTenonNear"
    Me.txtTenonNear.Size = New System.Drawing.Size(39, 20)
    Me.txtTenonNear.TabIndex = 167
    Me.txtTenonNear.Text = "0"
    '
    'Label2
    '
    Me.Label2.AutoSize = True
    Me.Label2.Location = New System.Drawing.Point(107, 104)
    Me.Label2.Name = "Label2"
    Me.Label2.Size = New System.Drawing.Size(14, 13)
    Me.Label2.TabIndex = 163
    Me.Label2.Text = "X"
    '
    'Label1
    '
    Me.Label1.Location = New System.Drawing.Point(7, 168)
    Me.Label1.Name = "Label1"
    Me.Label1.Size = New System.Drawing.Size(200, 32)
    Me.Label1.TabIndex = 154
    Me.Label1.Text = "Selecting or Deselecting the following will override the Job settings."
    '
    'ckNoHaunchatZero
    '
    Me.ckNoHaunchatZero.Location = New System.Drawing.Point(26, 194)
    Me.ckNoHaunchatZero.Name = "ckNoHaunchatZero"
    Me.ckNoHaunchatZero.Size = New System.Drawing.Size(184, 31)
    Me.ckNoHaunchatZero.TabIndex = 165
    Me.ckNoHaunchatZero.Text = "Suppress Haunch at Point 0"
    '
    'ckNoHaunchatFarend
    '
    Me.ckNoHaunchatFarend.Location = New System.Drawing.Point(26, 215)
    Me.ckNoHaunchatFarend.Name = "ckNoHaunchatFarend"
    Me.ckNoHaunchatFarend.Size = New System.Drawing.Size(184, 32)
    Me.ckNoHaunchatFarend.TabIndex = 166
    Me.ckNoHaunchatFarend.Text = "Suppress Haunch at Far End"
    '
    'Label3
    '
    Me.Label3.AutoSize = True
    Me.Label3.Location = New System.Drawing.Point(13, 104)
    Me.Label3.Name = "Label3"
    Me.Label3.Size = New System.Drawing.Size(30, 13)
    Me.Label3.TabIndex = 164
    Me.Label3.Text = "Size:"
    '
    'txtPartLength
    '
    Me.txtPartLength.BackColor = System.Drawing.Color.White
    Me.txtPartLength.Location = New System.Drawing.Point(131, 104)
    Me.txtPartLength.Name = "txtPartLength"
    Me.txtPartLength.Size = New System.Drawing.Size(56, 20)
    Me.txtPartLength.TabIndex = 160
    Me.txtPartLength.Text = "0"
    '
    'Label54
    '
    Me.Label54.AutoSize = True
    Me.Label54.Location = New System.Drawing.Point(131, 91)
    Me.Label54.Name = "Label54"
    Me.Label54.Size = New System.Drawing.Size(43, 13)
    Me.Label54.TabIndex = 161
    Me.Label54.Text = "Length:"
    '
    'Label53
    '
    Me.Label53.AutoSize = True
    Me.Label53.Location = New System.Drawing.Point(43, 91)
    Me.Label53.Name = "Label53"
    Me.Label53.Size = New System.Drawing.Size(38, 13)
    Me.Label53.TabIndex = 162
    Me.Label53.Text = "Width:"
    '
    'txtPartWdth
    '
    Me.txtPartWdth.BackColor = System.Drawing.Color.White
    Me.txtPartWdth.Location = New System.Drawing.Point(43, 104)
    Me.txtPartWdth.Name = "txtPartWdth"
    Me.txtPartWdth.Size = New System.Drawing.Size(56, 20)
    Me.txtPartWdth.TabIndex = 159
    Me.txtPartWdth.Text = "0"
    '
    'btnOk
    '
    Me.btnOk.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.btnOk.DialogResult = System.Windows.Forms.DialogResult.OK
    Me.btnOk.Location = New System.Drawing.Point(104, 300)
    Me.btnOk.Name = "btnOk"
    Me.btnOk.Size = New System.Drawing.Size(75, 24)
    Me.btnOk.TabIndex = 156
    Me.btnOk.Text = "OK"
    '
    'btnCancel
    '
    Me.btnCancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
    Me.btnCancel.Location = New System.Drawing.Point(191, 300)
    Me.btnCancel.Name = "btnCancel"
    Me.btnCancel.Size = New System.Drawing.Size(75, 24)
    Me.btnCancel.TabIndex = 155
    Me.btnCancel.Text = "Cancel"
    '
    'dlgEditRailParts
    '
    Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
    Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
    Me.ClientSize = New System.Drawing.Size(284, 340)
    Me.Controls.Add(Me.checkIncludeInCutlist)
    Me.Controls.Add(Me.labelTenonFar)
    Me.Controls.Add(Me.labelTenonZero)
    Me.Controls.Add(Me.txtTenonFar)
    Me.Controls.Add(Me.txtTenonNear)
    Me.Controls.Add(Me.Label2)
    Me.Controls.Add(Me.Label1)
    Me.Controls.Add(Me.ckNoHaunchatZero)
    Me.Controls.Add(Me.ckNoHaunchatFarend)
    Me.Controls.Add(Me.Label3)
    Me.Controls.Add(Me.txtPartLength)
    Me.Controls.Add(Me.Label54)
    Me.Controls.Add(Me.txtPartWdth)
    Me.Controls.Add(Me.btnOk)
    Me.Controls.Add(Me.btnCancel)
    Me.Controls.Add(Me.pnlPartStyle)
    Me.Controls.Add(Me.pnlPartName)
    Me.Controls.Add(Me.Label53)
    Me.Name = "dlgEditRailParts"
    Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
    Me.Text = "dlgEditRailParts"
    Me.pnlPartStyle.ResumeLayout(False)
    Me.pnlPartStyle.PerformLayout()
    Me.pnlPartName.ResumeLayout(False)
    Me.pnlPartName.PerformLayout()
    Me.ResumeLayout(False)
    Me.PerformLayout()

  End Sub
  Friend WithEvents pnlPartStyle As System.Windows.Forms.Panel
  Friend WithEvents comboPartStyle As System.Windows.Forms.ComboBox
  Friend WithEvents Label5 As System.Windows.Forms.Label
  Friend WithEvents pnlPartName As System.Windows.Forms.Panel
  Friend WithEvents txtPartName As System.Windows.Forms.TextBox
  Friend WithEvents Label4 As System.Windows.Forms.Label
  Friend WithEvents checkIncludeInCutlist As System.Windows.Forms.CheckBox
  Friend WithEvents labelTenonFar As System.Windows.Forms.Label
  Friend WithEvents labelTenonZero As System.Windows.Forms.Label
  Friend WithEvents txtTenonFar As System.Windows.Forms.TextBox
  Friend WithEvents txtTenonNear As System.Windows.Forms.TextBox
  Friend WithEvents Label2 As System.Windows.Forms.Label
  Friend WithEvents Label1 As System.Windows.Forms.Label
  Friend WithEvents ckNoHaunchatZero As System.Windows.Forms.CheckBox
  Friend WithEvents ckNoHaunchatFarend As System.Windows.Forms.CheckBox
  Friend WithEvents Label3 As System.Windows.Forms.Label
  Friend WithEvents txtPartLength As System.Windows.Forms.TextBox
  Friend WithEvents Label54 As System.Windows.Forms.Label
  Friend WithEvents Label53 As System.Windows.Forms.Label
  Friend WithEvents txtPartWdth As System.Windows.Forms.TextBox
  Friend WithEvents btnOk As System.Windows.Forms.Button
  Friend WithEvents btnCancel As System.Windows.Forms.Button
End Class
