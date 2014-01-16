<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FormMaterialReportWasteFactor
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
    Me.btnDeselectAll = New System.Windows.Forms.Button
    Me.btnSelectAll = New System.Windows.Forms.Button
    Me.ListView1 = New System.Windows.Forms.ListView
    Me.Label2 = New System.Windows.Forms.Label
    Me.btnCancel = New System.Windows.Forms.Button
    Me.btnOk = New System.Windows.Forms.Button
    Me.Label1 = New System.Windows.Forms.Label
    Me.labelWasteFactor = New System.Windows.Forms.Label
    Me.trackWasteFactor = New System.Windows.Forms.TrackBar
    CType(Me.trackWasteFactor, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.SuspendLayout()
    '
    'btnDeselectAll
    '
    Me.btnDeselectAll.Font = New System.Drawing.Font("Microsoft Sans Serif", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.btnDeselectAll.Location = New System.Drawing.Point(106, 255)
    Me.btnDeselectAll.Name = "btnDeselectAll"
    Me.btnDeselectAll.Size = New System.Drawing.Size(70, 23)
    Me.btnDeselectAll.TabIndex = 18
    Me.btnDeselectAll.Text = "Deselect All"
    Me.btnDeselectAll.UseVisualStyleBackColor = True
    '
    'btnSelectAll
    '
    Me.btnSelectAll.Font = New System.Drawing.Font("Microsoft Sans Serif", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.btnSelectAll.Location = New System.Drawing.Point(37, 255)
    Me.btnSelectAll.Name = "btnSelectAll"
    Me.btnSelectAll.Size = New System.Drawing.Size(70, 23)
    Me.btnSelectAll.TabIndex = 17
    Me.btnSelectAll.Text = "Select All"
    Me.btnSelectAll.UseVisualStyleBackColor = True
    '
    'ListView1
    '
    Me.ListView1.CheckBoxes = True
    Me.ListView1.Location = New System.Drawing.Point(37, 119)
    Me.ListView1.Name = "ListView1"
    Me.ListView1.Size = New System.Drawing.Size(225, 136)
    Me.ListView1.TabIndex = 16
    Me.ListView1.UseCompatibleStateImageBehavior = False
    Me.ListView1.View = System.Windows.Forms.View.List
    '
    'Label2
    '
    Me.Label2.AutoSize = True
    Me.Label2.Location = New System.Drawing.Point(18, 97)
    Me.Label2.Name = "Label2"
    Me.Label2.Size = New System.Drawing.Size(138, 13)
    Me.Label2.TabIndex = 15
    Me.Label2.Text = "Select Sizes to be included:"
    '
    'btnCancel
    '
    Me.btnCancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
    Me.btnCancel.Location = New System.Drawing.Point(207, 309)
    Me.btnCancel.Name = "btnCancel"
    Me.btnCancel.Size = New System.Drawing.Size(75, 23)
    Me.btnCancel.TabIndex = 14
    Me.btnCancel.Text = "&Cancel"
    Me.btnCancel.UseVisualStyleBackColor = True
    '
    'btnOk
    '
    Me.btnOk.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.btnOk.DialogResult = System.Windows.Forms.DialogResult.OK
    Me.btnOk.Location = New System.Drawing.Point(116, 308)
    Me.btnOk.Name = "btnOk"
    Me.btnOk.Size = New System.Drawing.Size(75, 23)
    Me.btnOk.TabIndex = 13
    Me.btnOk.Text = "&OK"
    Me.btnOk.UseVisualStyleBackColor = True
    '
    'Label1
    '
    Me.Label1.AutoSize = True
    Me.Label1.Location = New System.Drawing.Point(21, 21)
    Me.Label1.Name = "Label1"
    Me.Label1.Size = New System.Drawing.Size(74, 13)
    Me.Label1.TabIndex = 12
    Me.Label1.Text = "Waste Factor:"
    '
    'labelWasteFactor
    '
    Me.labelWasteFactor.AutoSize = True
    Me.labelWasteFactor.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.labelWasteFactor.Location = New System.Drawing.Point(138, 19)
    Me.labelWasteFactor.Name = "labelWasteFactor"
    Me.labelWasteFactor.Size = New System.Drawing.Size(71, 16)
    Me.labelWasteFactor.TabIndex = 11
    Me.labelWasteFactor.Text = "(Percent)"
    '
    'trackWasteFactor
    '
    Me.trackWasteFactor.LargeChange = 10
    Me.trackWasteFactor.Location = New System.Drawing.Point(21, 49)
    Me.trackWasteFactor.Maximum = 100
    Me.trackWasteFactor.Name = "trackWasteFactor"
    Me.trackWasteFactor.Size = New System.Drawing.Size(256, 42)
    Me.trackWasteFactor.SmallChange = 5
    Me.trackWasteFactor.TabIndex = 10
    Me.trackWasteFactor.TickFrequency = 10
    '
    'FormMaterialReportWasteFactor
    '
    Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
    Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
    Me.ClientSize = New System.Drawing.Size(300, 352)
    Me.Controls.Add(Me.btnDeselectAll)
    Me.Controls.Add(Me.btnSelectAll)
    Me.Controls.Add(Me.ListView1)
    Me.Controls.Add(Me.Label2)
    Me.Controls.Add(Me.btnCancel)
    Me.Controls.Add(Me.btnOk)
    Me.Controls.Add(Me.Label1)
    Me.Controls.Add(Me.labelWasteFactor)
    Me.Controls.Add(Me.trackWasteFactor)
    Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
    Me.Name = "FormMaterialReportWasteFactor"
    Me.Text = "FormMaterialReportWasteFactor"
    CType(Me.trackWasteFactor, System.ComponentModel.ISupportInitialize).EndInit()
    Me.ResumeLayout(False)
    Me.PerformLayout()

  End Sub
  Friend WithEvents btnDeselectAll As System.Windows.Forms.Button
  Friend WithEvents btnSelectAll As System.Windows.Forms.Button
  Friend WithEvents ListView1 As System.Windows.Forms.ListView
  Friend WithEvents Label2 As System.Windows.Forms.Label
  Friend WithEvents btnCancel As System.Windows.Forms.Button
  Friend WithEvents btnOk As System.Windows.Forms.Button
  Friend WithEvents Label1 As System.Windows.Forms.Label
  Friend WithEvents labelWasteFactor As System.Windows.Forms.Label
  Friend WithEvents trackWasteFactor As System.Windows.Forms.TrackBar
End Class
