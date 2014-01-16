<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmEditProgramText
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
    Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmEditProgramText))
    Me.ToolStripContainer1 = New System.Windows.Forms.ToolStripContainer
    Me.SplitContainer1 = New System.Windows.Forms.SplitContainer
    Me.RichTextBox1 = New System.Windows.Forms.RichTextBox
    Me.FramePartImagePanel1 = New FramePartImagePanel.FramePartImagePanel
    Me.MenuStrip1 = New System.Windows.Forms.MenuStrip
    Me.mnuOpenFile = New System.Windows.Forms.ToolStripMenuItem
    Me.OpenFileToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
    Me.mnuSaveFile = New System.Windows.Forms.ToolStripMenuItem
    Me.RevertToLastSavedToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
    Me.mnuExitProgram = New System.Windows.Forms.ToolStripMenuItem
    Me.OptionsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
    Me.mnuGraphicBackColor = New System.Windows.Forms.ToolStripMenuItem
    Me.ToolStrip1 = New System.Windows.Forms.ToolStrip
    Me.toolBtnSave = New System.Windows.Forms.ToolStripButton
    Me.ToolStripSeparator2 = New System.Windows.Forms.ToolStripSeparator
    Me.toolBtnRevert = New System.Windows.Forms.ToolStripButton
    Me.toolBtnRefresh = New System.Windows.Forms.ToolStripButton
    Me.ToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator
    Me.toolBtnPartSize = New System.Windows.Forms.ToolStripDropDownButton
    Me.txtPartWidth = New System.Windows.Forms.ToolStripTextBox
    Me.txtPartLength = New System.Windows.Forms.ToolStripTextBox
    Me.ToolStripSeparator3 = New System.Windows.Forms.ToolStripSeparator
    Me.toolBtnClose = New System.Windows.Forms.ToolStripButton
    Me.ToolStripButton1 = New System.Windows.Forms.ToolStripButton
    Me.OpenFileDialog1 = New System.Windows.Forms.OpenFileDialog
    Me.ToolStripContainer1.ContentPanel.SuspendLayout()
    Me.ToolStripContainer1.TopToolStripPanel.SuspendLayout()
    Me.ToolStripContainer1.SuspendLayout()
    Me.SplitContainer1.Panel1.SuspendLayout()
    Me.SplitContainer1.Panel2.SuspendLayout()
    Me.SplitContainer1.SuspendLayout()
    Me.MenuStrip1.SuspendLayout()
    Me.ToolStrip1.SuspendLayout()
    Me.SuspendLayout()
    '
    'ToolStripContainer1
    '
    '
    'ToolStripContainer1.ContentPanel
    '
    Me.ToolStripContainer1.ContentPanel.Controls.Add(Me.SplitContainer1)
    Me.ToolStripContainer1.ContentPanel.Size = New System.Drawing.Size(971, 716)
    Me.ToolStripContainer1.Dock = System.Windows.Forms.DockStyle.Fill
    Me.ToolStripContainer1.Location = New System.Drawing.Point(0, 0)
    Me.ToolStripContainer1.Name = "ToolStripContainer1"
    Me.ToolStripContainer1.Size = New System.Drawing.Size(971, 765)
    Me.ToolStripContainer1.TabIndex = 1
    Me.ToolStripContainer1.Text = "ToolStripContainer1"
    '
    'ToolStripContainer1.TopToolStripPanel
    '
    Me.ToolStripContainer1.TopToolStripPanel.Controls.Add(Me.MenuStrip1)
    Me.ToolStripContainer1.TopToolStripPanel.Controls.Add(Me.ToolStrip1)
    '
    'SplitContainer1
    '
    Me.SplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill
    Me.SplitContainer1.Location = New System.Drawing.Point(0, 0)
    Me.SplitContainer1.Name = "SplitContainer1"
    Me.SplitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal
    '
    'SplitContainer1.Panel1
    '
    Me.SplitContainer1.Panel1.Controls.Add(Me.RichTextBox1)
    '
    'SplitContainer1.Panel2
    '
    Me.SplitContainer1.Panel2.Controls.Add(Me.FramePartImagePanel1)
    Me.SplitContainer1.Size = New System.Drawing.Size(971, 716)
    Me.SplitContainer1.SplitterDistance = 579
    Me.SplitContainer1.TabIndex = 2
    '
    'RichTextBox1
    '
    Me.RichTextBox1.AcceptsTab = True
    Me.RichTextBox1.AutoWordSelection = True
    Me.RichTextBox1.Dock = System.Windows.Forms.DockStyle.Fill
    Me.RichTextBox1.Location = New System.Drawing.Point(0, 0)
    Me.RichTextBox1.Name = "RichTextBox1"
    Me.RichTextBox1.Size = New System.Drawing.Size(971, 579)
    Me.RichTextBox1.TabIndex = 2
    Me.RichTextBox1.Text = ""
    '
    'FramePartImagePanel1
    '
    Me.FramePartImagePanel1.Dock = System.Windows.Forms.DockStyle.Fill
    Me.FramePartImagePanel1.FileText = ""
    Me.FramePartImagePanel1.GraphicDisplay_BackColor = System.Drawing.SystemColors.ControlDarkDark
    Me.FramePartImagePanel1.Location = New System.Drawing.Point(0, 0)
    Me.FramePartImagePanel1.Name = "FramePartImagePanel1"
    Me.FramePartImagePanel1.PartSize = New System.Drawing.SizeF(0.0!, 0.0!)
    Me.FramePartImagePanel1.Size = New System.Drawing.Size(971, 133)
    Me.FramePartImagePanel1.TabIndex = 1
    '
    'MenuStrip1
    '
    Me.MenuStrip1.Dock = System.Windows.Forms.DockStyle.None
    Me.MenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuOpenFile, Me.OptionsToolStripMenuItem})
    Me.MenuStrip1.Location = New System.Drawing.Point(0, 0)
    Me.MenuStrip1.Name = "MenuStrip1"
    Me.MenuStrip1.Size = New System.Drawing.Size(971, 24)
    Me.MenuStrip1.TabIndex = 1
    Me.MenuStrip1.Text = "MenuStrip1"
    '
    'mnuOpenFile
    '
    Me.mnuOpenFile.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.OpenFileToolStripMenuItem, Me.mnuSaveFile, Me.RevertToLastSavedToolStripMenuItem, Me.mnuExitProgram})
    Me.mnuOpenFile.Name = "mnuOpenFile"
    Me.mnuOpenFile.Size = New System.Drawing.Size(35, 20)
    Me.mnuOpenFile.Text = "&File"
    '
    'OpenFileToolStripMenuItem
    '
    Me.OpenFileToolStripMenuItem.Name = "OpenFileToolStripMenuItem"
    Me.OpenFileToolStripMenuItem.Size = New System.Drawing.Size(187, 22)
    Me.OpenFileToolStripMenuItem.Text = "&Open File"
    '
    'mnuSaveFile
    '
    Me.mnuSaveFile.Name = "mnuSaveFile"
    Me.mnuSaveFile.Size = New System.Drawing.Size(187, 22)
    Me.mnuSaveFile.Text = "&Save File"
    '
    'RevertToLastSavedToolStripMenuItem
    '
    Me.RevertToLastSavedToolStripMenuItem.Name = "RevertToLastSavedToolStripMenuItem"
    Me.RevertToLastSavedToolStripMenuItem.Size = New System.Drawing.Size(187, 22)
    Me.RevertToLastSavedToolStripMenuItem.Text = "Revert to Last Saved"
    '
    'mnuExitProgram
    '
    Me.mnuExitProgram.Name = "mnuExitProgram"
    Me.mnuExitProgram.Size = New System.Drawing.Size(187, 22)
    Me.mnuExitProgram.Text = "E&xit"
    '
    'OptionsToolStripMenuItem
    '
    Me.OptionsToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuGraphicBackColor})
    Me.OptionsToolStripMenuItem.Name = "OptionsToolStripMenuItem"
    Me.OptionsToolStripMenuItem.Size = New System.Drawing.Size(56, 20)
    Me.OptionsToolStripMenuItem.Text = "&Options"
    '
    'mnuGraphicBackColor
    '
    Me.mnuGraphicBackColor.Name = "mnuGraphicBackColor"
    Me.mnuGraphicBackColor.Size = New System.Drawing.Size(171, 22)
    Me.mnuGraphicBackColor.Text = "Graphic BackColor"
    '
    'ToolStrip1
    '
    Me.ToolStrip1.Dock = System.Windows.Forms.DockStyle.None
    Me.ToolStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.toolBtnSave, Me.ToolStripSeparator2, Me.toolBtnRevert, Me.toolBtnRefresh, Me.ToolStripSeparator1, Me.toolBtnPartSize, Me.ToolStripSeparator3, Me.toolBtnClose, Me.ToolStripButton1})
    Me.ToolStrip1.Location = New System.Drawing.Point(3, 24)
    Me.ToolStrip1.Name = "ToolStrip1"
    Me.ToolStrip1.Size = New System.Drawing.Size(485, 25)
    Me.ToolStrip1.TabIndex = 0
    '
    'toolBtnSave
    '
    Me.toolBtnSave.Image = CType(resources.GetObject("toolBtnSave.Image"), System.Drawing.Image)
    Me.toolBtnSave.ImageTransparentColor = System.Drawing.Color.Magenta
    Me.toolBtnSave.Name = "toolBtnSave"
    Me.toolBtnSave.Size = New System.Drawing.Size(51, 22)
    Me.toolBtnSave.Text = "Save"
    '
    'ToolStripSeparator2
    '
    Me.ToolStripSeparator2.Name = "ToolStripSeparator2"
    Me.ToolStripSeparator2.Size = New System.Drawing.Size(6, 25)
    '
    'toolBtnRevert
    '
    Me.toolBtnRevert.Image = CType(resources.GetObject("toolBtnRevert.Image"), System.Drawing.Image)
    Me.toolBtnRevert.ImageTransparentColor = System.Drawing.Color.Magenta
    Me.toolBtnRevert.Name = "toolBtnRevert"
    Me.toolBtnRevert.Size = New System.Drawing.Size(129, 22)
    Me.toolBtnRevert.Text = "Revert to Last Saved"
    '
    'toolBtnRefresh
    '
    Me.toolBtnRefresh.Image = CType(resources.GetObject("toolBtnRefresh.Image"), System.Drawing.Image)
    Me.toolBtnRefresh.ImageTransparentColor = System.Drawing.Color.Magenta
    Me.toolBtnRefresh.Name = "toolBtnRefresh"
    Me.toolBtnRefresh.Size = New System.Drawing.Size(104, 22)
    Me.toolBtnRefresh.Text = "Refresh Graphic"
    '
    'ToolStripSeparator1
    '
    Me.ToolStripSeparator1.Name = "ToolStripSeparator1"
    Me.ToolStripSeparator1.Size = New System.Drawing.Size(6, 25)
    '
    'toolBtnPartSize
    '
    Me.toolBtnPartSize.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.txtPartWidth, Me.txtPartLength})
    Me.toolBtnPartSize.Name = "toolBtnPartSize"
    Me.toolBtnPartSize.Size = New System.Drawing.Size(66, 22)
    Me.toolBtnPartSize.Text = "Part Size:"
    '
    'txtPartWidth
    '
    Me.txtPartWidth.AutoToolTip = True
    Me.txtPartWidth.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
    Me.txtPartWidth.Name = "txtPartWidth"
    Me.txtPartWidth.Size = New System.Drawing.Size(100, 21)
    Me.txtPartWidth.Text = "Width"
    Me.txtPartWidth.ToolTipText = "Part Width"
    '
    'txtPartLength
    '
    Me.txtPartLength.AutoToolTip = True
    Me.txtPartLength.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
    Me.txtPartLength.Name = "txtPartLength"
    Me.txtPartLength.Size = New System.Drawing.Size(100, 21)
    Me.txtPartLength.Text = "Length"
    Me.txtPartLength.ToolTipText = "Part Length"
    '
    'ToolStripSeparator3
    '
    Me.ToolStripSeparator3.Name = "ToolStripSeparator3"
    Me.ToolStripSeparator3.Size = New System.Drawing.Size(6, 25)
    '
    'toolBtnClose
    '
    Me.toolBtnClose.Image = CType(resources.GetObject("toolBtnClose.Image"), System.Drawing.Image)
    Me.toolBtnClose.ImageTransparentColor = System.Drawing.Color.Magenta
    Me.toolBtnClose.Name = "toolBtnClose"
    Me.toolBtnClose.Size = New System.Drawing.Size(53, 22)
    Me.toolBtnClose.Text = "Close"
    '
    'ToolStripButton1
    '
    Me.ToolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
    Me.ToolStripButton1.Image = CType(resources.GetObject("ToolStripButton1.Image"), System.Drawing.Image)
    Me.ToolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta
    Me.ToolStripButton1.Name = "ToolStripButton1"
    Me.ToolStripButton1.Size = New System.Drawing.Size(23, 22)
    Me.ToolStripButton1.Text = "ToolStripButton1"
    '
    'OpenFileDialog1
    '
    Me.OpenFileDialog1.FileName = "OpenFileDialog1"
    '
    'frmEditProgramText
    '
    Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
    Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
    Me.ClientSize = New System.Drawing.Size(971, 765)
    Me.Controls.Add(Me.ToolStripContainer1)
    Me.Name = "frmEditProgramText"
    Me.Text = "frmEditProgramText"
    Me.ToolStripContainer1.ContentPanel.ResumeLayout(False)
    Me.ToolStripContainer1.TopToolStripPanel.ResumeLayout(False)
    Me.ToolStripContainer1.TopToolStripPanel.PerformLayout()
    Me.ToolStripContainer1.ResumeLayout(False)
    Me.ToolStripContainer1.PerformLayout()
    Me.SplitContainer1.Panel1.ResumeLayout(False)
    Me.SplitContainer1.Panel2.ResumeLayout(False)
    Me.SplitContainer1.ResumeLayout(False)
    Me.MenuStrip1.ResumeLayout(False)
    Me.MenuStrip1.PerformLayout()
    Me.ToolStrip1.ResumeLayout(False)
    Me.ToolStrip1.PerformLayout()
    Me.ResumeLayout(False)

  End Sub
  Friend WithEvents ToolStripContainer1 As System.Windows.Forms.ToolStripContainer
  Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
  Friend WithEvents RichTextBox1 As System.Windows.Forms.RichTextBox
  Friend WithEvents FramePartImagePanel1 As FramePartImagePanel.FramePartImagePanel
  Friend WithEvents MenuStrip1 As System.Windows.Forms.MenuStrip
  Friend WithEvents mnuOpenFile As System.Windows.Forms.ToolStripMenuItem
  Friend WithEvents OpenFileToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
  Friend WithEvents mnuSaveFile As System.Windows.Forms.ToolStripMenuItem
  Friend WithEvents RevertToLastSavedToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
  Friend WithEvents mnuExitProgram As System.Windows.Forms.ToolStripMenuItem
  Friend WithEvents OptionsToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
  Friend WithEvents mnuGraphicBackColor As System.Windows.Forms.ToolStripMenuItem
  Friend WithEvents ToolStrip1 As System.Windows.Forms.ToolStrip
  Friend WithEvents toolBtnSave As System.Windows.Forms.ToolStripButton
  Friend WithEvents ToolStripSeparator2 As System.Windows.Forms.ToolStripSeparator
  Friend WithEvents toolBtnRevert As System.Windows.Forms.ToolStripButton
  Friend WithEvents toolBtnRefresh As System.Windows.Forms.ToolStripButton
  Friend WithEvents ToolStripSeparator1 As System.Windows.Forms.ToolStripSeparator
  Friend WithEvents toolBtnPartSize As System.Windows.Forms.ToolStripDropDownButton
  Friend WithEvents txtPartWidth As System.Windows.Forms.ToolStripTextBox
  Friend WithEvents txtPartLength As System.Windows.Forms.ToolStripTextBox
  Friend WithEvents ToolStripSeparator3 As System.Windows.Forms.ToolStripSeparator
  Friend WithEvents toolBtnClose As System.Windows.Forms.ToolStripButton
  Friend WithEvents OpenFileDialog1 As System.Windows.Forms.OpenFileDialog
  Friend WithEvents ToolStripButton1 As System.Windows.Forms.ToolStripButton
End Class
