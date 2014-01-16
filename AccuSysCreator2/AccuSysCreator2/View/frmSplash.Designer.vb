<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmSplash
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
    Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmSplash))
    Me.lblMachine = New System.Windows.Forms.Label
    Me.lblMemory = New System.Windows.Forms.Label
    Me.lblUser = New System.Windows.Forms.Label
    Me.lblOpSystem = New System.Windows.Forms.Label
    Me.lblDomain = New System.Windows.Forms.Label
    Me.btnOk = New System.Windows.Forms.Button
    Me.lblOneMoment = New System.Windows.Forms.Label
    Me.lblVersion = New System.Windows.Forms.Label
    Me.Label3 = New System.Windows.Forms.Label
    Me.Label2 = New System.Windows.Forms.Label
    Me.PictureBox1 = New System.Windows.Forms.PictureBox
    Me.lblNoProcessors = New System.Windows.Forms.Label
    CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.SuspendLayout()
    '
    'lblMachine
    '
    Me.lblMachine.AutoSize = True
    Me.lblMachine.Location = New System.Drawing.Point(224, 172)
    Me.lblMachine.Name = "lblMachine"
    Me.lblMachine.Size = New System.Drawing.Size(51, 13)
    Me.lblMachine.TabIndex = 23
    Me.lblMachine.Text = "Machine:"
    '
    'lblMemory
    '
    Me.lblMemory.AutoSize = True
    Me.lblMemory.Location = New System.Drawing.Point(224, 146)
    Me.lblMemory.Name = "lblMemory"
    Me.lblMemory.Size = New System.Drawing.Size(50, 13)
    Me.lblMemory.TabIndex = 22
    Me.lblMemory.Text = "Memory: "
    '
    'lblUser
    '
    Me.lblUser.AutoSize = True
    Me.lblUser.Location = New System.Drawing.Point(224, 187)
    Me.lblUser.Name = "lblUser"
    Me.lblUser.Size = New System.Drawing.Size(66, 13)
    Me.lblUser.TabIndex = 21
    Me.lblUser.Text = "User Name: "
    '
    'lblOpSystem
    '
    Me.lblOpSystem.AutoSize = True
    Me.lblOpSystem.Location = New System.Drawing.Point(224, 116)
    Me.lblOpSystem.Name = "lblOpSystem"
    Me.lblOpSystem.Size = New System.Drawing.Size(93, 13)
    Me.lblOpSystem.TabIndex = 20
    Me.lblOpSystem.Text = "Operating System:"
    '
    'lblDomain
    '
    Me.lblDomain.AutoSize = True
    Me.lblDomain.Location = New System.Drawing.Point(224, 202)
    Me.lblDomain.Name = "lblDomain"
    Me.lblDomain.Size = New System.Drawing.Size(46, 13)
    Me.lblDomain.TabIndex = 19
    Me.lblDomain.Text = "Domain:"
    '
    'btnOk
    '
    Me.btnOk.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.btnOk.Cursor = System.Windows.Forms.Cursors.Hand
    Me.btnOk.DialogResult = System.Windows.Forms.DialogResult.OK
    Me.btnOk.Location = New System.Drawing.Point(349, 241)
    Me.btnOk.Name = "btnOk"
    Me.btnOk.Size = New System.Drawing.Size(80, 24)
    Me.btnOk.TabIndex = 18
    Me.btnOk.Text = "Ok"
    Me.btnOk.Visible = False
    '
    'lblOneMoment
    '
    Me.lblOneMoment.AutoSize = True
    Me.lblOneMoment.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.lblOneMoment.Location = New System.Drawing.Point(136, 241)
    Me.lblOneMoment.Name = "lblOneMoment"
    Me.lblOneMoment.Size = New System.Drawing.Size(181, 18)
    Me.lblOneMoment.TabIndex = 17
    Me.lblOneMoment.Text = "One Moment Please ..."
    '
    'lblVersion
    '
    Me.lblVersion.AutoSize = True
    Me.lblVersion.Location = New System.Drawing.Point(24, 85)
    Me.lblVersion.Name = "lblVersion"
    Me.lblVersion.Size = New System.Drawing.Size(48, 13)
    Me.lblVersion.TabIndex = 16
    Me.lblVersion.Text = "Version: "
    '
    'Label3
    '
    Me.Label3.AutoSize = True
    Me.Label3.Location = New System.Drawing.Point(24, 68)
    Me.Label3.Name = "Label3"
    Me.Label3.Size = New System.Drawing.Size(186, 13)
    Me.Label3.TabIndex = 15
    Me.Label3.Text = "Copyright © 2004 Galen Newswanger"
    '
    'Label2
    '
    Me.Label2.AutoSize = True
    Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 18.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.Label2.Location = New System.Drawing.Point(16, 28)
    Me.Label2.Name = "Label2"
    Me.Label2.Size = New System.Drawing.Size(355, 29)
    Me.Label2.TabIndex = 14
    Me.Label2.Text = "AccuSystem Program Creator"
    '
    'PictureBox1
    '
    Me.PictureBox1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.PictureBox1.Image = CType(resources.GetObject("PictureBox1.Image"), System.Drawing.Image)
    Me.PictureBox1.Location = New System.Drawing.Point(24, 116)
    Me.PictureBox1.Name = "PictureBox1"
    Me.PictureBox1.Size = New System.Drawing.Size(184, 99)
    Me.PictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize
    Me.PictureBox1.TabIndex = 13
    Me.PictureBox1.TabStop = False
    '
    'lblNoProcessors
    '
    Me.lblNoProcessors.AutoSize = True
    Me.lblNoProcessors.Location = New System.Drawing.Point(224, 131)
    Me.lblNoProcessors.Name = "lblNoProcessors"
    Me.lblNoProcessors.Size = New System.Drawing.Size(72, 13)
    Me.lblNoProcessors.TabIndex = 24
    Me.lblNoProcessors.Text = "# Processors:"
    '
    'frmSplash
    '
    Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
    Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
    Me.BackColor = System.Drawing.SystemColors.ControlDarkDark
    Me.ClientSize = New System.Drawing.Size(471, 286)
    Me.Controls.Add(Me.lblNoProcessors)
    Me.Controls.Add(Me.lblMachine)
    Me.Controls.Add(Me.lblMemory)
    Me.Controls.Add(Me.lblUser)
    Me.Controls.Add(Me.lblOpSystem)
    Me.Controls.Add(Me.lblDomain)
    Me.Controls.Add(Me.btnOk)
    Me.Controls.Add(Me.lblOneMoment)
    Me.Controls.Add(Me.lblVersion)
    Me.Controls.Add(Me.Label3)
    Me.Controls.Add(Me.Label2)
    Me.Controls.Add(Me.PictureBox1)
    Me.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
    Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
    Me.Name = "frmSplash"
    Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
    Me.Text = "AccuSystem MTH "
    CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
    Me.ResumeLayout(False)
    Me.PerformLayout()

  End Sub
  Friend WithEvents lblMachine As System.Windows.Forms.Label
  Friend WithEvents lblMemory As System.Windows.Forms.Label
  Friend WithEvents lblUser As System.Windows.Forms.Label
  Friend WithEvents lblOpSystem As System.Windows.Forms.Label
  Friend WithEvents lblDomain As System.Windows.Forms.Label
  Friend WithEvents btnOk As System.Windows.Forms.Button
  Friend WithEvents lblOneMoment As System.Windows.Forms.Label
  Friend WithEvents lblVersion As System.Windows.Forms.Label
  Friend WithEvents Label3 As System.Windows.Forms.Label
  Friend WithEvents Label2 As System.Windows.Forms.Label
  Friend WithEvents PictureBox1 As System.Windows.Forms.PictureBox
  Friend WithEvents lblNoProcessors As System.Windows.Forms.Label
End Class
