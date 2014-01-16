<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class MainForm
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
    Me.components = New System.ComponentModel.Container
    Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(MainForm))
    Me.ToolStripContainer1 = New System.Windows.Forms.ToolStripContainer
    Me.TabControl1 = New System.Windows.Forms.TabControl
    Me.tpNewStart = New System.Windows.Forms.TabPage
    Me.SplitContainer3 = New System.Windows.Forms.SplitContainer
    Me.btnSelectJobNumber = New System.Windows.Forms.Button
    Me.browserSchedule = New System.Windows.Forms.WebBrowser
    Me.pnlFilterBase = New System.Windows.Forms.Panel
    Me.pnlDateRangeFilter = New System.Windows.Forms.Panel
    Me.Label17 = New System.Windows.Forms.Label
    Me.btnRefreshSchedule = New System.Windows.Forms.Button
    Me.Label15 = New System.Windows.Forms.Label
    Me.Label16 = New System.Windows.Forms.Label
    Me.datePickFilterEnd = New System.Windows.Forms.DateTimePicker
    Me.datePickFilterStart = New System.Windows.Forms.DateTimePicker
    Me.tpSelect = New System.Windows.Forms.TabPage
    Me.SplitContainer2 = New System.Windows.Forms.SplitContainer
    Me.TabControl2 = New System.Windows.Forms.TabControl
    Me.tpStyles = New System.Windows.Forms.TabPage
    Me.FlowLayoutPanel2 = New System.Windows.Forms.FlowLayoutPanel
    Me.groupFrameStyle = New System.Windows.Forms.GroupBox
    Me.groupHingeStyle = New System.Windows.Forms.GroupBox
    Me.groupThickness = New System.Windows.Forms.GroupBox
    Me.pnlStyleLabel = New System.Windows.Forms.Panel
    Me.Label14 = New System.Windows.Forms.Label
    Me.Panel6 = New System.Windows.Forms.Panel
    Me.groupRadioCSV = New System.Windows.Forms.GroupBox
    Me.radioImportTemplateCSV = New System.Windows.Forms.RadioButton
    Me.radioImportCSV = New System.Windows.Forms.RadioButton
    Me.radioGenerateCSV = New System.Windows.Forms.RadioButton
    Me.btnLoadJob = New System.Windows.Forms.Button
    Me.radioImportData = New System.Windows.Forms.RadioButton
    Me.groupExistingData = New System.Windows.Forms.GroupBox
    Me.radioGetExistingData = New System.Windows.Forms.RadioButton
    Me.radioUseExistingData = New System.Windows.Forms.RadioButton
    Me.pnlSourceLabel = New System.Windows.Forms.Panel
    Me.Label18 = New System.Windows.Forms.Label
    Me.browserJobHeader = New System.Windows.Forms.WebBrowser
    Me.tpProgList = New System.Windows.Forms.TabPage
    Me.SplitContainer1 = New System.Windows.Forms.SplitContainer
    Me.tvProgList = New System.Windows.Forms.TreeView
    Me.popupTvProg = New System.Windows.Forms.ContextMenuStrip(Me.components)
    Me.mnuAddPartComment = New System.Windows.Forms.ToolStripMenuItem
    Me.lbCabList = New System.Windows.Forms.ListBox
    Me.Panel3 = New System.Windows.Forms.Panel
    Me.btnSaveProgList = New System.Windows.Forms.Button
    Me.btnTransNode = New System.Windows.Forms.Button
    Me.ImageList1 = New System.Windows.Forms.ImageList(Me.components)
    Me.btnNextPage = New System.Windows.Forms.Button
    Me.Panel2 = New System.Windows.Forms.Panel
    Me.btnTvUp = New System.Windows.Forms.Button
    Me.btnTvDown = New System.Windows.Forms.Button
    Me.btnRemoveProg = New System.Windows.Forms.Button
    Me.tpPartsLayout = New System.Windows.Forms.TabPage
    Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel
    Me.FramePartImagePanel1 = New FramePartImagePanel.FramePartImagePanel
    Me.Label12 = New System.Windows.Forms.Label
    Me.cbxPartName = New System.Windows.Forms.ComboBox
    Me.pnlCurrentPart = New System.Windows.Forms.Panel
    Me.txtPartItem = New System.Windows.Forms.TextBox
    Me.txtPartThick = New System.Windows.Forms.TextBox
    Me.Label9 = New System.Windows.Forms.Label
    Me.Label8 = New System.Windows.Forms.Label
    Me.txtPartHght = New System.Windows.Forms.TextBox
    Me.txtPartWdth = New System.Windows.Forms.TextBox
    Me.txtPartCode = New System.Windows.Forms.TextBox
    Me.comboPartType = New System.Windows.Forms.ComboBox
    Me.Label7 = New System.Windows.Forms.Label
    Me.Label6 = New System.Windows.Forms.Label
    Me.Label5 = New System.Windows.Forms.Label
    Me.Label4 = New System.Windows.Forms.Label
    Me.Label3 = New System.Windows.Forms.Label
    Me.Label2 = New System.Windows.Forms.Label
    Me.Label1 = New System.Windows.Forms.Label
    Me.txtPartName = New System.Windows.Forms.TextBox
    Me.ContextMenuBlank = New System.Windows.Forms.ContextMenuStrip(Me.components)
    Me.Panel4 = New System.Windows.Forms.Panel
    Me.btnImportItem = New System.Windows.Forms.Button
    Me.btnProcessPart = New System.Windows.Forms.Button
    Me.btnClearOperations = New System.Windows.Forms.Button
    Me.btnPartsClearForm = New System.Windows.Forms.Button
    Me.Panel5 = New System.Windows.Forms.Panel
    Me.lbAdjoinParts = New System.Windows.Forms.ListBox
    Me.btnUp = New System.Windows.Forms.Button
    Me.btnDown = New System.Windows.Forms.Button
    Me.btnRemove = New System.Windows.Forms.Button
    Me.Label10 = New System.Windows.Forms.Label
    Me.Panel8 = New System.Windows.Forms.Panel
    Me.btnNewFrame = New System.Windows.Forms.Button
    Me.btnEditPart = New System.Windows.Forms.Button
    Me.pnlCabinetInfo = New System.Windows.Forms.Panel
    Me.txtCabinetInfo = New System.Windows.Forms.RichTextBox
    Me.btnNewCustPart = New System.Windows.Forms.Button
    Me.Label13 = New System.Windows.Forms.Label
    Me.lbStilesRails = New System.Windows.Forms.ListBox
    Me.btnTransCurrPart = New System.Windows.Forms.Button
    Me.btnTransAdjoinPart = New System.Windows.Forms.Button
    Me.Panel9 = New System.Windows.Forms.Panel
    Me.btnCustOpg = New System.Windows.Forms.Button
    Me.btnEditOpg = New System.Windows.Forms.Button
    Me.Label11 = New System.Windows.Forms.Label
    Me.lblCurrOpening = New System.Windows.Forms.Label
    Me.txtLibPath = New System.Windows.Forms.TextBox
    Me.checkHingeR = New System.Windows.Forms.CheckBox
    Me.checkHingeL = New System.Windows.Forms.CheckBox
    Me.lbOpenings = New System.Windows.Forms.ListBox
    Me.btnTransAdjoinOpg = New System.Windows.Forms.Button
    Me.Panel7 = New System.Windows.Forms.Panel
    Me.lbSavedFiles = New System.Windows.Forms.ListBox
    Me.popupMenus = New System.Windows.Forms.ContextMenuStrip(Me.components)
    Me.puMnuItemDelSelFile = New System.Windows.Forms.ToolStripMenuItem
    Me.puMnuItemEditSelFile = New System.Windows.Forms.ToolStripMenuItem
    Me.puMnuShowPartsSelFile = New System.Windows.Forms.ToolStripMenuItem
    Me.tpJobDetail = New System.Windows.Forms.TabPage
    Me.browserJobDetail = New System.Windows.Forms.WebBrowser
    Me.tpCatalog = New System.Windows.Forms.TabPage
    Me.browserCatalog = New System.Windows.Forms.WebBrowser
    Me.tpStart = New System.Windows.Forms.TabPage
    Me.FlowLayoutPanel1 = New System.Windows.Forms.FlowLayoutPanel
    Me.Panel1 = New System.Windows.Forms.Panel
    Me.btnManualCreateFrame = New System.Windows.Forms.Button
    Me.btnJob = New System.Windows.Forms.Button
    Me.groupFrameStyleold = New System.Windows.Forms.GroupBox
    Me.groupHingeStyleold = New System.Windows.Forms.GroupBox
    Me.groupThicknessold = New System.Windows.Forms.GroupBox
    Me.pnlTpStart = New System.Windows.Forms.Panel
    Me.PictureBox1 = New System.Windows.Forms.PictureBox
    Me.StatusStrip1 = New System.Windows.Forms.StatusStrip
    Me.sbPanelMessage = New System.Windows.Forms.ToolStripStatusLabel
    Me.sbPanelVersion = New System.Windows.Forms.ToolStripStatusLabel
    Me.MenuStrip1 = New System.Windows.Forms.MenuStrip
    Me.FileToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
    Me.mnuSelectJob = New System.Windows.Forms.ToolStripMenuItem
    Me.PrintToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
    Me.mnuPrintBC4Files = New System.Windows.Forms.ToolStripMenuItem
    Me.mnuExit = New System.Windows.Forms.ToolStripMenuItem
    Me.EditToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
    Me.menuEditPrograms = New System.Windows.Forms.ToolStripMenuItem
    Me.toolStripSeparator3 = New System.Windows.Forms.ToolStripSeparator
    Me.CutToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
    Me.CopyToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
    Me.PasteToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
    Me.toolStripSeparator4 = New System.Windows.Forms.ToolStripSeparator
    Me.SelectAllToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
    Me.ToolStripMenuItem1 = New System.Windows.Forms.ToolStripMenuItem
    Me.mnuBarcodePartReport = New System.Windows.Forms.ToolStripMenuItem
    Me.mnuPrintBarcodeReport = New System.Windows.Forms.ToolStripMenuItem
    Me.mnuPrintBarcodes2 = New System.Windows.Forms.ToolStripMenuItem
    Me.mnuMateralRequirements = New System.Windows.Forms.ToolStripMenuItem
    Me.toolsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
    Me.menuPurgeOldData = New System.Windows.Forms.ToolStripMenuItem
    Me.menuOptions = New System.Windows.Forms.ToolStripMenuItem
    Me.menuEditSetupData = New System.Windows.Forms.ToolStripMenuItem
    Me.mnuUpdateFiles = New System.Windows.Forms.ToolStripMenuItem
    Me.mnuAbout = New System.Windows.Forms.ToolStripMenuItem
    Me.AboutToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
    Me.ToolStrip1 = New System.Windows.Forms.ToolStrip
    Me.labelJobNumberKey = New System.Windows.Forms.ToolStripLabel
    Me.labelStatusJobNo = New System.Windows.Forms.ToolStripLabel
    Me.ToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator
    Me.lblSelectItem = New System.Windows.Forms.ToolStripLabel
    Me.comboItems = New System.Windows.Forms.ToolStripComboBox
    Me.ToolStripSeparator2 = New System.Windows.Forms.ToolStripSeparator
    Me.toolBtnCabInfo = New System.Windows.Forms.ToolStripButton
    Me.toolBtnJobdetails = New System.Windows.Forms.ToolStripButton
    Me.toolBtnCatalog = New System.Windows.Forms.ToolStripButton
    Me.toolBtnPrintBarcodeReport = New System.Windows.Forms.ToolStripButton
    Me.ToolStripSeparator5 = New System.Windows.Forms.ToolStripSeparator
    Me.toolBtnNavHome = New System.Windows.Forms.ToolStripButton
    Me.toolBtnNavBack = New System.Windows.Forms.ToolStripButton
    Me.toolBtnNavForward = New System.Windows.Forms.ToolStripButton
    Me.toolBtnPrintBrowser = New System.Windows.Forms.ToolStripButton
    Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
    Me.popupCabList = New System.Windows.Forms.ContextMenuStrip(Me.components)
    Me.mnuMove2List = New System.Windows.Forms.ToolStripMenuItem
    Me.popupParts = New System.Windows.Forms.ContextMenuStrip(Me.components)
    Me.mnuAddEditPartComment = New System.Windows.Forms.ToolStripMenuItem
    Me.ToolStripContainer1.ContentPanel.SuspendLayout()
    Me.ToolStripContainer1.TopToolStripPanel.SuspendLayout()
    Me.ToolStripContainer1.SuspendLayout()
    Me.TabControl1.SuspendLayout()
    Me.tpNewStart.SuspendLayout()
    Me.SplitContainer3.Panel1.SuspendLayout()
    Me.SplitContainer3.Panel2.SuspendLayout()
    Me.SplitContainer3.SuspendLayout()
    Me.pnlFilterBase.SuspendLayout()
    Me.pnlDateRangeFilter.SuspendLayout()
    Me.tpSelect.SuspendLayout()
    Me.SplitContainer2.Panel1.SuspendLayout()
    Me.SplitContainer2.Panel2.SuspendLayout()
    Me.SplitContainer2.SuspendLayout()
    Me.TabControl2.SuspendLayout()
    Me.tpStyles.SuspendLayout()
    Me.FlowLayoutPanel2.SuspendLayout()
    Me.pnlStyleLabel.SuspendLayout()
    Me.Panel6.SuspendLayout()
    Me.groupRadioCSV.SuspendLayout()
    Me.groupExistingData.SuspendLayout()
    Me.pnlSourceLabel.SuspendLayout()
    Me.tpProgList.SuspendLayout()
    Me.SplitContainer1.Panel1.SuspendLayout()
    Me.SplitContainer1.Panel2.SuspendLayout()
    Me.SplitContainer1.SuspendLayout()
    Me.popupTvProg.SuspendLayout()
    Me.Panel3.SuspendLayout()
    Me.Panel2.SuspendLayout()
    Me.tpPartsLayout.SuspendLayout()
    Me.TableLayoutPanel1.SuspendLayout()
    Me.pnlCurrentPart.SuspendLayout()
    Me.Panel4.SuspendLayout()
    Me.Panel5.SuspendLayout()
    Me.Panel8.SuspendLayout()
    Me.pnlCabinetInfo.SuspendLayout()
    Me.Panel9.SuspendLayout()
    Me.Panel7.SuspendLayout()
    Me.popupMenus.SuspendLayout()
    Me.tpJobDetail.SuspendLayout()
    Me.tpCatalog.SuspendLayout()
    Me.tpStart.SuspendLayout()
    Me.FlowLayoutPanel1.SuspendLayout()
    Me.Panel1.SuspendLayout()
    Me.pnlTpStart.SuspendLayout()
    CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.StatusStrip1.SuspendLayout()
    Me.MenuStrip1.SuspendLayout()
    Me.ToolStrip1.SuspendLayout()
    Me.popupCabList.SuspendLayout()
    Me.popupParts.SuspendLayout()
    Me.SuspendLayout()
    '
    'ToolStripContainer1
    '
    '
    'ToolStripContainer1.ContentPanel
    '
    Me.ToolStripContainer1.ContentPanel.Controls.Add(Me.TabControl1)
    Me.ToolStripContainer1.ContentPanel.Controls.Add(Me.StatusStrip1)
    Me.ToolStripContainer1.ContentPanel.Size = New System.Drawing.Size(915, 653)
    Me.ToolStripContainer1.Dock = System.Windows.Forms.DockStyle.Fill
    Me.ToolStripContainer1.Location = New System.Drawing.Point(0, 0)
    Me.ToolStripContainer1.Name = "ToolStripContainer1"
    Me.ToolStripContainer1.Size = New System.Drawing.Size(915, 702)
    Me.ToolStripContainer1.TabIndex = 0
    Me.ToolStripContainer1.Text = "ToolStripContainer1"
    '
    'ToolStripContainer1.TopToolStripPanel
    '
    Me.ToolStripContainer1.TopToolStripPanel.Controls.Add(Me.MenuStrip1)
    Me.ToolStripContainer1.TopToolStripPanel.Controls.Add(Me.ToolStrip1)
    '
    'TabControl1
    '
    Me.TabControl1.Controls.Add(Me.tpNewStart)
    Me.TabControl1.Controls.Add(Me.tpSelect)
    Me.TabControl1.Controls.Add(Me.tpProgList)
    Me.TabControl1.Controls.Add(Me.tpPartsLayout)
    Me.TabControl1.Controls.Add(Me.tpJobDetail)
    Me.TabControl1.Controls.Add(Me.tpCatalog)
    Me.TabControl1.Controls.Add(Me.tpStart)
    Me.TabControl1.Dock = System.Windows.Forms.DockStyle.Fill
    Me.TabControl1.Location = New System.Drawing.Point(0, 0)
    Me.TabControl1.Name = "TabControl1"
    Me.TabControl1.SelectedIndex = 0
    Me.TabControl1.Size = New System.Drawing.Size(915, 631)
    Me.TabControl1.TabIndex = 1
    Me.TabControl1.TabStop = False
    '
    'tpNewStart
    '
    Me.tpNewStart.Controls.Add(Me.SplitContainer3)
    Me.tpNewStart.Location = New System.Drawing.Point(4, 22)
    Me.tpNewStart.Name = "tpNewStart"
    Me.tpNewStart.Padding = New System.Windows.Forms.Padding(3)
    Me.tpNewStart.Size = New System.Drawing.Size(907, 605)
    Me.tpNewStart.TabIndex = 6
    Me.tpNewStart.Text = "Start"
    Me.tpNewStart.UseVisualStyleBackColor = True
    '
    'SplitContainer3
    '
    Me.SplitContainer3.Dock = System.Windows.Forms.DockStyle.Fill
    Me.SplitContainer3.Location = New System.Drawing.Point(3, 3)
    Me.SplitContainer3.Name = "SplitContainer3"
    Me.SplitContainer3.Orientation = System.Windows.Forms.Orientation.Horizontal
    '
    'SplitContainer3.Panel1
    '
    Me.SplitContainer3.Panel1.Controls.Add(Me.btnSelectJobNumber)
    '
    'SplitContainer3.Panel2
    '
    Me.SplitContainer3.Panel2.Controls.Add(Me.browserSchedule)
    Me.SplitContainer3.Panel2.Controls.Add(Me.pnlFilterBase)
    Me.SplitContainer3.Size = New System.Drawing.Size(901, 599)
    Me.SplitContainer3.SplitterDistance = 56
    Me.SplitContainer3.TabIndex = 0
    '
    'btnSelectJobNumber
    '
    Me.btnSelectJobNumber.Image = CType(resources.GetObject("btnSelectJobNumber.Image"), System.Drawing.Image)
    Me.btnSelectJobNumber.ImageAlign = System.Drawing.ContentAlignment.TopLeft
    Me.btnSelectJobNumber.Location = New System.Drawing.Point(17, 13)
    Me.btnSelectJobNumber.Name = "btnSelectJobNumber"
    Me.btnSelectJobNumber.Size = New System.Drawing.Size(136, 23)
    Me.btnSelectJobNumber.TabIndex = 1
    Me.btnSelectJobNumber.Text = "Select Job Number" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10)
    Me.btnSelectJobNumber.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
    Me.btnSelectJobNumber.UseVisualStyleBackColor = True
    '
    'browserSchedule
    '
    Me.browserSchedule.Dock = System.Windows.Forms.DockStyle.Fill
    Me.browserSchedule.Location = New System.Drawing.Point(0, 0)
    Me.browserSchedule.MinimumSize = New System.Drawing.Size(20, 20)
    Me.browserSchedule.Name = "browserSchedule"
    Me.browserSchedule.Size = New System.Drawing.Size(901, 501)
    Me.browserSchedule.TabIndex = 12
    '
    'pnlFilterBase
    '
    Me.pnlFilterBase.Controls.Add(Me.pnlDateRangeFilter)
    Me.pnlFilterBase.Dock = System.Windows.Forms.DockStyle.Bottom
    Me.pnlFilterBase.Location = New System.Drawing.Point(0, 501)
    Me.pnlFilterBase.Name = "pnlFilterBase"
    Me.pnlFilterBase.Size = New System.Drawing.Size(901, 38)
    Me.pnlFilterBase.TabIndex = 11
    '
    'pnlDateRangeFilter
    '
    Me.pnlDateRangeFilter.Controls.Add(Me.Label17)
    Me.pnlDateRangeFilter.Controls.Add(Me.btnRefreshSchedule)
    Me.pnlDateRangeFilter.Controls.Add(Me.Label15)
    Me.pnlDateRangeFilter.Controls.Add(Me.Label16)
    Me.pnlDateRangeFilter.Controls.Add(Me.datePickFilterEnd)
    Me.pnlDateRangeFilter.Controls.Add(Me.datePickFilterStart)
    Me.pnlDateRangeFilter.Dock = System.Windows.Forms.DockStyle.Fill
    Me.pnlDateRangeFilter.Location = New System.Drawing.Point(0, 0)
    Me.pnlDateRangeFilter.Name = "pnlDateRangeFilter"
    Me.pnlDateRangeFilter.Size = New System.Drawing.Size(901, 38)
    Me.pnlDateRangeFilter.TabIndex = 11
    '
    'Label17
    '
    Me.Label17.AutoSize = True
    Me.Label17.Location = New System.Drawing.Point(14, 13)
    Me.Label17.Name = "Label17"
    Me.Label17.Size = New System.Drawing.Size(80, 13)
    Me.Label17.TabIndex = 5
    Me.Label17.Text = "Schedule Filter:"
    '
    'btnRefreshSchedule
    '
    Me.btnRefreshSchedule.Location = New System.Drawing.Point(694, 7)
    Me.btnRefreshSchedule.Name = "btnRefreshSchedule"
    Me.btnRefreshSchedule.Size = New System.Drawing.Size(89, 23)
    Me.btnRefreshSchedule.TabIndex = 4
    Me.btnRefreshSchedule.Text = "Refresh List"
    Me.btnRefreshSchedule.UseVisualStyleBackColor = True
    '
    'Label15
    '
    Me.Label15.AutoSize = True
    Me.Label15.Location = New System.Drawing.Point(414, 13)
    Me.Label15.Name = "Label15"
    Me.Label15.Size = New System.Drawing.Size(55, 13)
    Me.Label15.TabIndex = 3
    Me.Label15.Text = "End Date:"
    '
    'Label16
    '
    Me.Label16.AutoSize = True
    Me.Label16.Location = New System.Drawing.Point(116, 13)
    Me.Label16.Name = "Label16"
    Me.Label16.Size = New System.Drawing.Size(58, 13)
    Me.Label16.TabIndex = 2
    Me.Label16.Text = "Start Date:"
    '
    'datePickFilterEnd
    '
    Me.datePickFilterEnd.Location = New System.Drawing.Point(475, 10)
    Me.datePickFilterEnd.Name = "datePickFilterEnd"
    Me.datePickFilterEnd.Size = New System.Drawing.Size(200, 20)
    Me.datePickFilterEnd.TabIndex = 1
    '
    'datePickFilterStart
    '
    Me.datePickFilterStart.Location = New System.Drawing.Point(180, 11)
    Me.datePickFilterStart.Name = "datePickFilterStart"
    Me.datePickFilterStart.Size = New System.Drawing.Size(200, 20)
    Me.datePickFilterStart.TabIndex = 0
    '
    'tpSelect
    '
    Me.tpSelect.Controls.Add(Me.SplitContainer2)
    Me.tpSelect.Location = New System.Drawing.Point(4, 22)
    Me.tpSelect.Name = "tpSelect"
    Me.tpSelect.Padding = New System.Windows.Forms.Padding(3)
    Me.tpSelect.Size = New System.Drawing.Size(907, 605)
    Me.tpSelect.TabIndex = 5
    Me.tpSelect.Text = "Select"
    Me.tpSelect.UseVisualStyleBackColor = True
    '
    'SplitContainer2
    '
    Me.SplitContainer2.Dock = System.Windows.Forms.DockStyle.Fill
    Me.SplitContainer2.Location = New System.Drawing.Point(3, 3)
    Me.SplitContainer2.Name = "SplitContainer2"
    Me.SplitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal
    '
    'SplitContainer2.Panel1
    '
    Me.SplitContainer2.Panel1.Controls.Add(Me.TabControl2)
    '
    'SplitContainer2.Panel2
    '
    Me.SplitContainer2.Panel2.Controls.Add(Me.browserJobHeader)
    Me.SplitContainer2.Size = New System.Drawing.Size(901, 599)
    Me.SplitContainer2.SplitterDistance = 272
    Me.SplitContainer2.TabIndex = 0
    '
    'TabControl2
    '
    Me.TabControl2.Alignment = System.Windows.Forms.TabAlignment.Left
    Me.TabControl2.Controls.Add(Me.tpStyles)
    Me.TabControl2.Dock = System.Windows.Forms.DockStyle.Fill
    Me.TabControl2.Location = New System.Drawing.Point(0, 0)
    Me.TabControl2.Multiline = True
    Me.TabControl2.Name = "TabControl2"
    Me.TabControl2.SelectedIndex = 0
    Me.TabControl2.Size = New System.Drawing.Size(901, 272)
    Me.TabControl2.TabIndex = 0
    '
    'tpStyles
    '
    Me.tpStyles.Controls.Add(Me.FlowLayoutPanel2)
    Me.tpStyles.Controls.Add(Me.pnlStyleLabel)
    Me.tpStyles.Controls.Add(Me.Panel6)
    Me.tpStyles.Controls.Add(Me.pnlSourceLabel)
    Me.tpStyles.Location = New System.Drawing.Point(23, 4)
    Me.tpStyles.Name = "tpStyles"
    Me.tpStyles.Padding = New System.Windows.Forms.Padding(3)
    Me.tpStyles.Size = New System.Drawing.Size(874, 264)
    Me.tpStyles.TabIndex = 0
    Me.tpStyles.UseVisualStyleBackColor = True
    '
    'FlowLayoutPanel2
    '
    Me.FlowLayoutPanel2.Controls.Add(Me.groupFrameStyle)
    Me.FlowLayoutPanel2.Controls.Add(Me.groupHingeStyle)
    Me.FlowLayoutPanel2.Controls.Add(Me.groupThickness)
    Me.FlowLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill
    Me.FlowLayoutPanel2.Location = New System.Drawing.Point(284, 3)
    Me.FlowLayoutPanel2.Name = "FlowLayoutPanel2"
    Me.FlowLayoutPanel2.Size = New System.Drawing.Size(587, 258)
    Me.FlowLayoutPanel2.TabIndex = 1
    '
    'groupFrameStyle
    '
    Me.groupFrameStyle.Location = New System.Drawing.Point(3, 3)
    Me.groupFrameStyle.Name = "groupFrameStyle"
    Me.groupFrameStyle.Size = New System.Drawing.Size(200, 100)
    Me.groupFrameStyle.TabIndex = 4
    Me.groupFrameStyle.TabStop = False
    Me.groupFrameStyle.Text = "Frame Style"
    '
    'groupHingeStyle
    '
    Me.groupHingeStyle.Location = New System.Drawing.Point(209, 3)
    Me.groupHingeStyle.Name = "groupHingeStyle"
    Me.groupHingeStyle.Size = New System.Drawing.Size(185, 100)
    Me.groupHingeStyle.TabIndex = 5
    Me.groupHingeStyle.TabStop = False
    Me.groupHingeStyle.Text = "Hinge Style"
    '
    'groupThickness
    '
    Me.groupThickness.Location = New System.Drawing.Point(400, 3)
    Me.groupThickness.Name = "groupThickness"
    Me.groupThickness.Size = New System.Drawing.Size(175, 100)
    Me.groupThickness.TabIndex = 6
    Me.groupThickness.TabStop = False
    Me.groupThickness.Text = "Frame Thickness"
    '
    'pnlStyleLabel
    '
    Me.pnlStyleLabel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
    Me.pnlStyleLabel.Controls.Add(Me.Label14)
    Me.pnlStyleLabel.Dock = System.Windows.Forms.DockStyle.Left
    Me.pnlStyleLabel.Location = New System.Drawing.Point(257, 3)
    Me.pnlStyleLabel.Name = "pnlStyleLabel"
    Me.pnlStyleLabel.Size = New System.Drawing.Size(27, 258)
    Me.pnlStyleLabel.TabIndex = 2
    '
    'Label14
    '
    Me.Label14.AutoSize = True
    Me.Label14.Location = New System.Drawing.Point(3, 2)
    Me.Label14.Name = "Label14"
    Me.Label14.Size = New System.Drawing.Size(23, 13)
    Me.Label14.TabIndex = 0
    Me.Label14.Text = "#2."
    '
    'Panel6
    '
    Me.Panel6.Controls.Add(Me.groupRadioCSV)
    Me.Panel6.Controls.Add(Me.btnLoadJob)
    Me.Panel6.Controls.Add(Me.radioImportData)
    Me.Panel6.Controls.Add(Me.groupExistingData)
    Me.Panel6.Controls.Add(Me.radioUseExistingData)
    Me.Panel6.Dock = System.Windows.Forms.DockStyle.Left
    Me.Panel6.Location = New System.Drawing.Point(30, 3)
    Me.Panel6.Name = "Panel6"
    Me.Panel6.Size = New System.Drawing.Size(227, 258)
    Me.Panel6.TabIndex = 0
    '
    'groupRadioCSV
    '
    Me.groupRadioCSV.Controls.Add(Me.radioImportTemplateCSV)
    Me.groupRadioCSV.Controls.Add(Me.radioImportCSV)
    Me.groupRadioCSV.Controls.Add(Me.radioGenerateCSV)
    Me.groupRadioCSV.Location = New System.Drawing.Point(23, 105)
    Me.groupRadioCSV.Name = "groupRadioCSV"
    Me.groupRadioCSV.Size = New System.Drawing.Size(189, 86)
    Me.groupRadioCSV.TabIndex = 6
    Me.groupRadioCSV.TabStop = False
    '
    'radioImportTemplateCSV
    '
    Me.radioImportTemplateCSV.AutoSize = True
    Me.radioImportTemplateCSV.Enabled = False
    Me.radioImportTemplateCSV.Location = New System.Drawing.Point(6, 58)
    Me.radioImportTemplateCSV.Name = "radioImportTemplateCSV"
    Me.radioImportTemplateCSV.Size = New System.Drawing.Size(146, 17)
    Me.radioImportTemplateCSV.TabIndex = 2
    Me.radioImportTemplateCSV.Text = "Import a CSV template file"
    Me.radioImportTemplateCSV.UseVisualStyleBackColor = True
    '
    'radioImportCSV
    '
    Me.radioImportCSV.AutoSize = True
    Me.radioImportCSV.Location = New System.Drawing.Point(6, 35)
    Me.radioImportCSV.Name = "radioImportCSV"
    Me.radioImportCSV.Size = New System.Drawing.Size(132, 17)
    Me.radioImportCSV.TabIndex = 1
    Me.radioImportCSV.Text = "Import existing CSV file"
    Me.radioImportCSV.UseVisualStyleBackColor = True
    '
    'radioGenerateCSV
    '
    Me.radioGenerateCSV.AutoSize = True
    Me.radioGenerateCSV.Checked = True
    Me.radioGenerateCSV.Location = New System.Drawing.Point(6, 12)
    Me.radioGenerateCSV.Name = "radioGenerateCSV"
    Me.radioGenerateCSV.Size = New System.Drawing.Size(132, 17)
    Me.radioGenerateCSV.TabIndex = 0
    Me.radioGenerateCSV.TabStop = True
    Me.radioGenerateCSV.Text = "Generate new CSV file"
    Me.radioGenerateCSV.UseVisualStyleBackColor = True
    '
    'btnLoadJob
    '
    Me.btnLoadJob.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.btnLoadJob.Location = New System.Drawing.Point(48, 210)
    Me.btnLoadJob.Name = "btnLoadJob"
    Me.btnLoadJob.Size = New System.Drawing.Size(127, 28)
    Me.btnLoadJob.TabIndex = 5
    Me.btnLoadJob.Text = "#3. Load Job"
    Me.btnLoadJob.UseVisualStyleBackColor = True
    '
    'radioImportData
    '
    Me.radioImportData.AutoSize = True
    Me.radioImportData.Location = New System.Drawing.Point(6, 89)
    Me.radioImportData.Name = "radioImportData"
    Me.radioImportData.Size = New System.Drawing.Size(108, 17)
    Me.radioImportData.TabIndex = 4
    Me.radioImportData.TabStop = True
    Me.radioImportData.Text = "Import New Data."
    Me.radioImportData.UseVisualStyleBackColor = True
    '
    'groupExistingData
    '
    Me.groupExistingData.Controls.Add(Me.radioGetExistingData)
    Me.groupExistingData.Location = New System.Drawing.Point(23, 41)
    Me.groupExistingData.Name = "groupExistingData"
    Me.groupExistingData.Size = New System.Drawing.Size(189, 34)
    Me.groupExistingData.TabIndex = 2
    Me.groupExistingData.TabStop = False
    '
    'radioGetExistingData
    '
    Me.radioGetExistingData.AutoSize = True
    Me.radioGetExistingData.Checked = True
    Me.radioGetExistingData.Location = New System.Drawing.Point(6, 12)
    Me.radioGetExistingData.Name = "radioGetExistingData"
    Me.radioGetExistingData.Size = New System.Drawing.Size(174, 17)
    Me.radioGetExistingData.TabIndex = 0
    Me.radioGetExistingData.TabStop = True
    Me.radioGetExistingData.Text = "Get existing data from database"
    Me.radioGetExistingData.UseVisualStyleBackColor = True
    '
    'radioUseExistingData
    '
    Me.radioUseExistingData.AutoSize = True
    Me.radioUseExistingData.Location = New System.Drawing.Point(6, 27)
    Me.radioUseExistingData.Name = "radioUseExistingData"
    Me.radioUseExistingData.Size = New System.Drawing.Size(112, 17)
    Me.radioUseExistingData.TabIndex = 1
    Me.radioUseExistingData.TabStop = True
    Me.radioUseExistingData.Text = "Use Existing Data."
    Me.radioUseExistingData.UseVisualStyleBackColor = True
    '
    'pnlSourceLabel
    '
    Me.pnlSourceLabel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
    Me.pnlSourceLabel.Controls.Add(Me.Label18)
    Me.pnlSourceLabel.Dock = System.Windows.Forms.DockStyle.Left
    Me.pnlSourceLabel.Location = New System.Drawing.Point(3, 3)
    Me.pnlSourceLabel.Name = "pnlSourceLabel"
    Me.pnlSourceLabel.Size = New System.Drawing.Size(27, 258)
    Me.pnlSourceLabel.TabIndex = 3
    '
    'Label18
    '
    Me.Label18.AutoSize = True
    Me.Label18.Location = New System.Drawing.Point(3, 2)
    Me.Label18.Name = "Label18"
    Me.Label18.Size = New System.Drawing.Size(23, 13)
    Me.Label18.TabIndex = 0
    Me.Label18.Text = "#1."
    '
    'browserJobHeader
    '
    Me.browserJobHeader.Dock = System.Windows.Forms.DockStyle.Fill
    Me.browserJobHeader.Location = New System.Drawing.Point(0, 0)
    Me.browserJobHeader.MinimumSize = New System.Drawing.Size(20, 20)
    Me.browserJobHeader.Name = "browserJobHeader"
    Me.browserJobHeader.Size = New System.Drawing.Size(901, 323)
    Me.browserJobHeader.TabIndex = 0
    '
    'tpProgList
    '
    Me.tpProgList.Controls.Add(Me.SplitContainer1)
    Me.tpProgList.Controls.Add(Me.Panel3)
    Me.tpProgList.Controls.Add(Me.Panel2)
    Me.tpProgList.Location = New System.Drawing.Point(4, 22)
    Me.tpProgList.Name = "tpProgList"
    Me.tpProgList.Padding = New System.Windows.Forms.Padding(3)
    Me.tpProgList.Size = New System.Drawing.Size(907, 605)
    Me.tpProgList.TabIndex = 1
    Me.tpProgList.Text = "Program List"
    Me.tpProgList.UseVisualStyleBackColor = True
    '
    'SplitContainer1
    '
    Me.SplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill
    Me.SplitContainer1.Location = New System.Drawing.Point(88, 3)
    Me.SplitContainer1.Name = "SplitContainer1"
    '
    'SplitContainer1.Panel1
    '
    Me.SplitContainer1.Panel1.Controls.Add(Me.tvProgList)
    '
    'SplitContainer1.Panel2
    '
    Me.SplitContainer1.Panel2.Controls.Add(Me.lbCabList)
    Me.SplitContainer1.Size = New System.Drawing.Size(662, 599)
    Me.SplitContainer1.SplitterDistance = 267
    Me.SplitContainer1.TabIndex = 2
    '
    'tvProgList
    '
    Me.tvProgList.AllowDrop = True
    Me.tvProgList.ContextMenuStrip = Me.popupTvProg
    Me.tvProgList.Dock = System.Windows.Forms.DockStyle.Fill
    Me.tvProgList.Location = New System.Drawing.Point(0, 0)
    Me.tvProgList.Name = "tvProgList"
    Me.tvProgList.ShowNodeToolTips = True
    Me.tvProgList.Size = New System.Drawing.Size(267, 599)
    Me.tvProgList.TabIndex = 0
    '
    'popupTvProg
    '
    Me.popupTvProg.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuAddPartComment})
    Me.popupTvProg.Name = "popupTvProg"
    Me.popupTvProg.Size = New System.Drawing.Size(195, 26)
    '
    'mnuAddPartComment
    '
    Me.mnuAddPartComment.Name = "mnuAddPartComment"
    Me.mnuAddPartComment.Size = New System.Drawing.Size(194, 22)
    Me.mnuAddPartComment.Text = "Add/Replace Comment"
    '
    'lbCabList
    '
    Me.lbCabList.AllowDrop = True
    Me.lbCabList.Dock = System.Windows.Forms.DockStyle.Fill
    Me.lbCabList.FormattingEnabled = True
    Me.lbCabList.Location = New System.Drawing.Point(0, 0)
    Me.lbCabList.Name = "lbCabList"
    Me.lbCabList.Size = New System.Drawing.Size(391, 589)
    Me.lbCabList.TabIndex = 0
    '
    'Panel3
    '
    Me.Panel3.Controls.Add(Me.btnSaveProgList)
    Me.Panel3.Controls.Add(Me.btnTransNode)
    Me.Panel3.Controls.Add(Me.btnNextPage)
    Me.Panel3.Dock = System.Windows.Forms.DockStyle.Right
    Me.Panel3.Location = New System.Drawing.Point(750, 3)
    Me.Panel3.Name = "Panel3"
    Me.Panel3.Size = New System.Drawing.Size(154, 599)
    Me.Panel3.TabIndex = 1
    '
    'btnSaveProgList
    '
    Me.btnSaveProgList.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.btnSaveProgList.Location = New System.Drawing.Point(75, 3)
    Me.btnSaveProgList.Name = "btnSaveProgList"
    Me.btnSaveProgList.Size = New System.Drawing.Size(74, 45)
    Me.btnSaveProgList.TabIndex = 154
    Me.btnSaveProgList.Text = "Save Program List"
    Me.btnSaveProgList.UseVisualStyleBackColor = True
    '
    'btnTransNode
    '
    Me.btnTransNode.ImageIndex = 3
    Me.btnTransNode.ImageList = Me.ImageList1
    Me.btnTransNode.Location = New System.Drawing.Point(6, 222)
    Me.btnTransNode.Name = "btnTransNode"
    Me.btnTransNode.Size = New System.Drawing.Size(24, 24)
    Me.btnTransNode.TabIndex = 153
    Me.btnTransNode.UseVisualStyleBackColor = True
    '
    'ImageList1
    '
    Me.ImageList1.ImageStream = CType(resources.GetObject("ImageList1.ImageStream"), System.Windows.Forms.ImageListStreamer)
    Me.ImageList1.TransparentColor = System.Drawing.Color.Transparent
    Me.ImageList1.Images.SetKeyName(0, "camera_picture.png")
    Me.ImageList1.Images.SetKeyName(1, "arrow.png")
    Me.ImageList1.Images.SetKeyName(2, "arrow-090.png")
    Me.ImageList1.Images.SetKeyName(3, "arrow-180.png")
    Me.ImageList1.Images.SetKeyName(4, "arrow-270.png")
    Me.ImageList1.Images.SetKeyName(5, "arrow-turn-270.png")
    Me.ImageList1.Images.SetKeyName(6, "arrow-continue.png")
    Me.ImageList1.Images.SetKeyName(7, "arrow-curve-270.png")
    Me.ImageList1.Images.SetKeyName(8, "contract.png")
    Me.ImageList1.Images.SetKeyName(9, "copy.png")
    Me.ImageList1.Images.SetKeyName(10, "exit.png")
    Me.ImageList1.Images.SetKeyName(11, "clipboard.png")
    Me.ImageList1.Images.SetKeyName(12, "clipboard_empty.png")
    Me.ImageList1.Images.SetKeyName(13, "clipboard_next_down.png")
    Me.ImageList1.Images.SetKeyName(14, "copy.png")
    Me.ImageList1.Images.SetKeyName(15, "cut.png")
    Me.ImageList1.Images.SetKeyName(16, "delete2-16x16.png")
    Me.ImageList1.Images.SetKeyName(17, "delete-16x16.png")
    Me.ImageList1.Images.SetKeyName(18, "desktop.png")
    Me.ImageList1.Images.SetKeyName(19, "document_down.png")
    Me.ImageList1.Images.SetKeyName(20, "document_exchange.png")
    Me.ImageList1.Images.SetKeyName(21, "garbage.png")
    Me.ImageList1.Images.SetKeyName(22, "gears.png")
    Me.ImageList1.Images.SetKeyName(23, "import1.png")
    Me.ImageList1.Images.SetKeyName(24, "erase.png")
    '
    'btnNextPage
    '
    Me.btnNextPage.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.btnNextPage.Cursor = System.Windows.Forms.Cursors.Hand
    Me.btnNextPage.Location = New System.Drawing.Point(75, 54)
    Me.btnNextPage.Name = "btnNextPage"
    Me.btnNextPage.Size = New System.Drawing.Size(74, 35)
    Me.btnNextPage.TabIndex = 152
    Me.btnNextPage.Text = "Next ->"
    '
    'Panel2
    '
    Me.Panel2.Controls.Add(Me.btnTvUp)
    Me.Panel2.Controls.Add(Me.btnTvDown)
    Me.Panel2.Controls.Add(Me.btnRemoveProg)
    Me.Panel2.Dock = System.Windows.Forms.DockStyle.Left
    Me.Panel2.Location = New System.Drawing.Point(3, 3)
    Me.Panel2.Name = "Panel2"
    Me.Panel2.Size = New System.Drawing.Size(85, 599)
    Me.Panel2.TabIndex = 0
    '
    'btnTvUp
    '
    Me.btnTvUp.Image = CType(resources.GetObject("btnTvUp.Image"), System.Drawing.Image)
    Me.btnTvUp.Location = New System.Drawing.Point(55, 66)
    Me.btnTvUp.Name = "btnTvUp"
    Me.btnTvUp.Size = New System.Drawing.Size(24, 24)
    Me.btnTvUp.TabIndex = 2
    Me.btnTvUp.UseVisualStyleBackColor = True
    '
    'btnTvDown
    '
    Me.btnTvDown.Image = CType(resources.GetObject("btnTvDown.Image"), System.Drawing.Image)
    Me.btnTvDown.Location = New System.Drawing.Point(55, 96)
    Me.btnTvDown.Name = "btnTvDown"
    Me.btnTvDown.Size = New System.Drawing.Size(24, 24)
    Me.btnTvDown.TabIndex = 1
    Me.btnTvDown.UseVisualStyleBackColor = True
    '
    'btnRemoveProg
    '
    Me.btnRemoveProg.Image = CType(resources.GetObject("btnRemoveProg.Image"), System.Drawing.Image)
    Me.btnRemoveProg.Location = New System.Drawing.Point(55, 24)
    Me.btnRemoveProg.Name = "btnRemoveProg"
    Me.btnRemoveProg.Size = New System.Drawing.Size(24, 24)
    Me.btnRemoveProg.TabIndex = 0
    Me.btnRemoveProg.UseVisualStyleBackColor = True
    '
    'tpPartsLayout
    '
    Me.tpPartsLayout.Controls.Add(Me.TableLayoutPanel1)
    Me.tpPartsLayout.Location = New System.Drawing.Point(4, 22)
    Me.tpPartsLayout.Name = "tpPartsLayout"
    Me.tpPartsLayout.Padding = New System.Windows.Forms.Padding(3)
    Me.tpPartsLayout.Size = New System.Drawing.Size(907, 605)
    Me.tpPartsLayout.TabIndex = 2
    Me.tpPartsLayout.Text = "Parts Layout"
    Me.tpPartsLayout.UseVisualStyleBackColor = True
    '
    'TableLayoutPanel1
    '
    Me.TableLayoutPanel1.ColumnCount = 3
    Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 120.0!))
    Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 267.0!))
    Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
    Me.TableLayoutPanel1.Controls.Add(Me.FramePartImagePanel1, 0, 6)
    Me.TableLayoutPanel1.Controls.Add(Me.Label12, 0, 4)
    Me.TableLayoutPanel1.Controls.Add(Me.cbxPartName, 1, 0)
    Me.TableLayoutPanel1.Controls.Add(Me.pnlCurrentPart, 0, 1)
    Me.TableLayoutPanel1.Controls.Add(Me.Panel4, 0, 2)
    Me.TableLayoutPanel1.Controls.Add(Me.Panel5, 1, 2)
    Me.TableLayoutPanel1.Controls.Add(Me.Panel8, 2, 0)
    Me.TableLayoutPanel1.Controls.Add(Me.Panel9, 2, 3)
    Me.TableLayoutPanel1.Controls.Add(Me.Panel7, 0, 5)
    Me.TableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill
    Me.TableLayoutPanel1.Location = New System.Drawing.Point(3, 3)
    Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
    Me.TableLayoutPanel1.RowCount = 7
    Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30.0!))
    Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 90.0!))
    Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle)
    Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle)
    Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle)
    Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle)
    Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle)
    Me.TableLayoutPanel1.Size = New System.Drawing.Size(901, 599)
    Me.TableLayoutPanel1.TabIndex = 20
    '
    'FramePartImagePanel1
    '
    Me.TableLayoutPanel1.SetColumnSpan(Me.FramePartImagePanel1, 3)
    Me.FramePartImagePanel1.Dock = System.Windows.Forms.DockStyle.Fill
    Me.FramePartImagePanel1.FileText = ""
    Me.FramePartImagePanel1.GraphicDisplay_BackColor = System.Drawing.SystemColors.ControlDarkDark
    Me.FramePartImagePanel1.Location = New System.Drawing.Point(3, 490)
    Me.FramePartImagePanel1.Name = "FramePartImagePanel1"
    Me.FramePartImagePanel1.PartSize = New System.Drawing.SizeF(0.0!, 0.0!)
    Me.FramePartImagePanel1.Size = New System.Drawing.Size(895, 114)
    Me.FramePartImagePanel1.TabIndex = 14
    '
    'Label12
    '
    Me.Label12.AutoSize = True
    Me.Label12.Location = New System.Drawing.Point(3, 326)
    Me.Label12.Name = "Label12"
    Me.Label12.Size = New System.Drawing.Size(104, 13)
    Me.Label12.TabIndex = 12
    Me.Label12.Text = "Saved Program Files"
    '
    'cbxPartName
    '
    Me.cbxPartName.BackColor = System.Drawing.SystemColors.Info
    Me.cbxPartName.FormattingEnabled = True
    Me.cbxPartName.Location = New System.Drawing.Point(123, 3)
    Me.cbxPartName.Name = "cbxPartName"
    Me.cbxPartName.Size = New System.Drawing.Size(200, 21)
    Me.cbxPartName.TabIndex = 0
    '
    'pnlCurrentPart
    '
    Me.pnlCurrentPart.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
    Me.TableLayoutPanel1.SetColumnSpan(Me.pnlCurrentPart, 2)
    Me.pnlCurrentPart.Controls.Add(Me.txtPartItem)
    Me.pnlCurrentPart.Controls.Add(Me.txtPartThick)
    Me.pnlCurrentPart.Controls.Add(Me.Label9)
    Me.pnlCurrentPart.Controls.Add(Me.Label8)
    Me.pnlCurrentPart.Controls.Add(Me.txtPartHght)
    Me.pnlCurrentPart.Controls.Add(Me.txtPartWdth)
    Me.pnlCurrentPart.Controls.Add(Me.txtPartCode)
    Me.pnlCurrentPart.Controls.Add(Me.comboPartType)
    Me.pnlCurrentPart.Controls.Add(Me.Label7)
    Me.pnlCurrentPart.Controls.Add(Me.Label6)
    Me.pnlCurrentPart.Controls.Add(Me.Label5)
    Me.pnlCurrentPart.Controls.Add(Me.Label4)
    Me.pnlCurrentPart.Controls.Add(Me.Label3)
    Me.pnlCurrentPart.Controls.Add(Me.Label2)
    Me.pnlCurrentPart.Controls.Add(Me.Label1)
    Me.pnlCurrentPart.Controls.Add(Me.txtPartName)
    Me.pnlCurrentPart.Location = New System.Drawing.Point(3, 33)
    Me.pnlCurrentPart.Name = "pnlCurrentPart"
    Me.pnlCurrentPart.Size = New System.Drawing.Size(376, 80)
    Me.pnlCurrentPart.TabIndex = 1
    '
    'txtPartItem
    '
    Me.txtPartItem.BackColor = System.Drawing.SystemColors.ControlLightLight
    Me.txtPartItem.Location = New System.Drawing.Point(328, 48)
    Me.txtPartItem.Name = "txtPartItem"
    Me.txtPartItem.ReadOnly = True
    Me.txtPartItem.Size = New System.Drawing.Size(32, 20)
    Me.txtPartItem.TabIndex = 15
    '
    'txtPartThick
    '
    Me.txtPartThick.BackColor = System.Drawing.SystemColors.ControlLightLight
    Me.txtPartThick.Location = New System.Drawing.Point(274, 48)
    Me.txtPartThick.Name = "txtPartThick"
    Me.txtPartThick.ReadOnly = True
    Me.txtPartThick.Size = New System.Drawing.Size(45, 20)
    Me.txtPartThick.TabIndex = 14
    '
    'Label9
    '
    Me.Label9.AutoSize = True
    Me.Label9.Location = New System.Drawing.Point(261, 52)
    Me.Label9.Name = "Label9"
    Me.Label9.Size = New System.Drawing.Size(14, 13)
    Me.Label9.TabIndex = 13
    Me.Label9.Text = "X"
    '
    'Label8
    '
    Me.Label8.AutoSize = True
    Me.Label8.Location = New System.Drawing.Point(216, 52)
    Me.Label8.Name = "Label8"
    Me.Label8.Size = New System.Drawing.Size(14, 13)
    Me.Label8.TabIndex = 12
    Me.Label8.Text = "X"
    '
    'txtPartHght
    '
    Me.txtPartHght.BackColor = System.Drawing.SystemColors.ControlLightLight
    Me.txtPartHght.Location = New System.Drawing.Point(229, 48)
    Me.txtPartHght.Name = "txtPartHght"
    Me.txtPartHght.ReadOnly = True
    Me.txtPartHght.Size = New System.Drawing.Size(32, 20)
    Me.txtPartHght.TabIndex = 11
    '
    'txtPartWdth
    '
    Me.txtPartWdth.BackColor = System.Drawing.SystemColors.ControlLightLight
    Me.txtPartWdth.Location = New System.Drawing.Point(184, 48)
    Me.txtPartWdth.Name = "txtPartWdth"
    Me.txtPartWdth.ReadOnly = True
    Me.txtPartWdth.Size = New System.Drawing.Size(32, 20)
    Me.txtPartWdth.TabIndex = 10
    '
    'txtPartCode
    '
    Me.txtPartCode.BackColor = System.Drawing.SystemColors.ControlLightLight
    Me.txtPartCode.Location = New System.Drawing.Point(136, 48)
    Me.txtPartCode.Name = "txtPartCode"
    Me.txtPartCode.ReadOnly = True
    Me.txtPartCode.Size = New System.Drawing.Size(48, 20)
    Me.txtPartCode.TabIndex = 9
    '
    'comboPartType
    '
    Me.comboPartType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.Simple
    Me.comboPartType.FlatStyle = System.Windows.Forms.FlatStyle.Flat
    Me.comboPartType.FormattingEnabled = True
    Me.comboPartType.Location = New System.Drawing.Point(8, 48)
    Me.comboPartType.Name = "comboPartType"
    Me.comboPartType.Size = New System.Drawing.Size(125, 20)
    Me.comboPartType.TabIndex = 8
    '
    'Label7
    '
    Me.Label7.AutoSize = True
    Me.Label7.Location = New System.Drawing.Point(328, 32)
    Me.Label7.Name = "Label7"
    Me.Label7.Size = New System.Drawing.Size(27, 13)
    Me.Label7.TabIndex = 7
    Me.Label7.Text = "Item"
    '
    'Label6
    '
    Me.Label6.AutoSize = True
    Me.Label6.Location = New System.Drawing.Point(277, 32)
    Me.Label6.Name = "Label6"
    Me.Label6.Size = New System.Drawing.Size(34, 13)
    Me.Label6.TabIndex = 6
    Me.Label6.Text = "Thick"
    '
    'Label5
    '
    Me.Label5.AutoSize = True
    Me.Label5.Location = New System.Drawing.Point(227, 32)
    Me.Label5.Name = "Label5"
    Me.Label5.Size = New System.Drawing.Size(38, 13)
    Me.Label5.TabIndex = 5
    Me.Label5.Text = "Height"
    '
    'Label4
    '
    Me.Label4.AutoSize = True
    Me.Label4.Location = New System.Drawing.Point(184, 32)
    Me.Label4.Name = "Label4"
    Me.Label4.Size = New System.Drawing.Size(35, 13)
    Me.Label4.TabIndex = 4
    Me.Label4.Text = "Width"
    '
    'Label3
    '
    Me.Label3.AutoSize = True
    Me.Label3.Location = New System.Drawing.Point(136, 32)
    Me.Label3.Name = "Label3"
    Me.Label3.Size = New System.Drawing.Size(32, 13)
    Me.Label3.TabIndex = 3
    Me.Label3.Text = "Code"
    '
    'Label2
    '
    Me.Label2.AutoSize = True
    Me.Label2.Location = New System.Drawing.Point(8, 32)
    Me.Label2.Name = "Label2"
    Me.Label2.Size = New System.Drawing.Size(86, 13)
    Me.Label2.TabIndex = 2
    Me.Label2.Text = "Select Part Type"
    '
    'Label1
    '
    Me.Label1.AutoSize = True
    Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Underline), System.Drawing.FontStyle))
    Me.Label1.Location = New System.Drawing.Point(10, 8)
    Me.Label1.Name = "Label1"
    Me.Label1.Size = New System.Drawing.Size(97, 16)
    Me.Label1.TabIndex = 1
    Me.Label1.Text = "Current Part: "
    '
    'txtPartName
    '
    Me.txtPartName.BackColor = System.Drawing.SystemColors.Info
    Me.txtPartName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
    Me.txtPartName.ContextMenuStrip = Me.ContextMenuBlank
    Me.txtPartName.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Bold)
    Me.txtPartName.Location = New System.Drawing.Point(120, 3)
    Me.txtPartName.Name = "txtPartName"
    Me.txtPartName.ReadOnly = True
    Me.txtPartName.Size = New System.Drawing.Size(200, 26)
    Me.txtPartName.TabIndex = 0
    '
    'ContextMenuBlank
    '
    Me.ContextMenuBlank.Name = "ContextMenuBlank"
    Me.ContextMenuBlank.Size = New System.Drawing.Size(61, 4)
    '
    'Panel4
    '
    Me.Panel4.Controls.Add(Me.btnImportItem)
    Me.Panel4.Controls.Add(Me.btnProcessPart)
    Me.Panel4.Controls.Add(Me.btnClearOperations)
    Me.Panel4.Controls.Add(Me.btnPartsClearForm)
    Me.Panel4.Dock = System.Windows.Forms.DockStyle.Fill
    Me.Panel4.Location = New System.Drawing.Point(3, 123)
    Me.Panel4.Name = "Panel4"
    Me.TableLayoutPanel1.SetRowSpan(Me.Panel4, 2)
    Me.Panel4.Size = New System.Drawing.Size(114, 200)
    Me.Panel4.TabIndex = 17
    '
    'btnImportItem
    '
    Me.btnImportItem.Cursor = System.Windows.Forms.Cursors.Hand
    Me.btnImportItem.Enabled = False
    Me.btnImportItem.Location = New System.Drawing.Point(3, 138)
    Me.btnImportItem.Name = "btnImportItem"
    Me.btnImportItem.Size = New System.Drawing.Size(112, 37)
    Me.btnImportItem.TabIndex = 154
    Me.btnImportItem.Text = "Re-import Frame from CSV"
    '
    'btnProcessPart
    '
    Me.btnProcessPart.ImageAlign = System.Drawing.ContentAlignment.TopCenter
    Me.btnProcessPart.ImageIndex = 6
    Me.btnProcessPart.ImageList = Me.ImageList1
    Me.btnProcessPart.Location = New System.Drawing.Point(3, 5)
    Me.btnProcessPart.Name = "btnProcessPart"
    Me.btnProcessPart.Size = New System.Drawing.Size(112, 38)
    Me.btnProcessPart.TabIndex = 4
    Me.btnProcessPart.Text = "Process Part"
    Me.btnProcessPart.TextAlign = System.Drawing.ContentAlignment.BottomCenter
    Me.btnProcessPart.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
    Me.btnProcessPart.UseVisualStyleBackColor = True
    '
    'btnClearOperations
    '
    Me.btnClearOperations.ImageAlign = System.Drawing.ContentAlignment.TopCenter
    Me.btnClearOperations.ImageKey = "erase.png"
    Me.btnClearOperations.ImageList = Me.ImageList1
    Me.btnClearOperations.Location = New System.Drawing.Point(3, 49)
    Me.btnClearOperations.Name = "btnClearOperations"
    Me.btnClearOperations.Size = New System.Drawing.Size(112, 38)
    Me.btnClearOperations.TabIndex = 5
    Me.btnClearOperations.Text = "Clear Image"
    Me.btnClearOperations.TextAlign = System.Drawing.ContentAlignment.BottomCenter
    Me.btnClearOperations.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
    Me.btnClearOperations.UseVisualStyleBackColor = True
    '
    'btnPartsClearForm
    '
    Me.btnPartsClearForm.ImageAlign = System.Drawing.ContentAlignment.TopCenter
    Me.btnPartsClearForm.ImageKey = "garbage.png"
    Me.btnPartsClearForm.ImageList = Me.ImageList1
    Me.btnPartsClearForm.Location = New System.Drawing.Point(2, 94)
    Me.btnPartsClearForm.Name = "btnPartsClearForm"
    Me.btnPartsClearForm.Size = New System.Drawing.Size(112, 38)
    Me.btnPartsClearForm.TabIndex = 6
    Me.btnPartsClearForm.Text = "Clear Current Part"
    Me.btnPartsClearForm.TextAlign = System.Drawing.ContentAlignment.BottomCenter
    Me.btnPartsClearForm.UseVisualStyleBackColor = True
    '
    'Panel5
    '
    Me.Panel5.Controls.Add(Me.lbAdjoinParts)
    Me.Panel5.Controls.Add(Me.btnUp)
    Me.Panel5.Controls.Add(Me.btnDown)
    Me.Panel5.Controls.Add(Me.btnRemove)
    Me.Panel5.Controls.Add(Me.Label10)
    Me.Panel5.Dock = System.Windows.Forms.DockStyle.Fill
    Me.Panel5.Location = New System.Drawing.Point(123, 123)
    Me.Panel5.Name = "Panel5"
    Me.TableLayoutPanel1.SetRowSpan(Me.Panel5, 3)
    Me.Panel5.Size = New System.Drawing.Size(261, 214)
    Me.Panel5.TabIndex = 18
    '
    'lbAdjoinParts
    '
    Me.lbAdjoinParts.BackColor = System.Drawing.SystemColors.Info
    Me.lbAdjoinParts.Dock = System.Windows.Forms.DockStyle.Right
    Me.lbAdjoinParts.FormattingEnabled = True
    Me.lbAdjoinParts.Location = New System.Drawing.Point(27, 0)
    Me.lbAdjoinParts.Name = "lbAdjoinParts"
    Me.lbAdjoinParts.ScrollAlwaysVisible = True
    Me.lbAdjoinParts.Size = New System.Drawing.Size(234, 212)
    Me.lbAdjoinParts.TabIndex = 3
    '
    'btnUp
    '
    Me.btnUp.ImageIndex = 2
    Me.btnUp.ImageList = Me.ImageList1
    Me.btnUp.Location = New System.Drawing.Point(3, 14)
    Me.btnUp.Name = "btnUp"
    Me.btnUp.Size = New System.Drawing.Size(24, 24)
    Me.btnUp.TabIndex = 7
    Me.btnUp.UseVisualStyleBackColor = True
    '
    'btnDown
    '
    Me.btnDown.ImageIndex = 4
    Me.btnDown.ImageList = Me.ImageList1
    Me.btnDown.Location = New System.Drawing.Point(3, 42)
    Me.btnDown.Name = "btnDown"
    Me.btnDown.Size = New System.Drawing.Size(24, 24)
    Me.btnDown.TabIndex = 8
    Me.btnDown.UseVisualStyleBackColor = True
    '
    'btnRemove
    '
    Me.btnRemove.ImageIndex = 16
    Me.btnRemove.ImageList = Me.ImageList1
    Me.btnRemove.Location = New System.Drawing.Point(3, 96)
    Me.btnRemove.Name = "btnRemove"
    Me.btnRemove.Size = New System.Drawing.Size(24, 24)
    Me.btnRemove.TabIndex = 9
    Me.btnRemove.UseVisualStyleBackColor = True
    '
    'Label10
    '
    Me.Label10.AutoSize = True
    Me.Label10.Location = New System.Drawing.Point(29, 0)
    Me.Label10.Name = "Label10"
    Me.Label10.Size = New System.Drawing.Size(96, 13)
    Me.Label10.TabIndex = 2
    Me.Label10.Text = "Adjoining Parts List"
    '
    'Panel8
    '
    Me.Panel8.Controls.Add(Me.btnNewFrame)
    Me.Panel8.Controls.Add(Me.btnEditPart)
    Me.Panel8.Controls.Add(Me.pnlCabinetInfo)
    Me.Panel8.Controls.Add(Me.btnNewCustPart)
    Me.Panel8.Controls.Add(Me.Label13)
    Me.Panel8.Controls.Add(Me.lbStilesRails)
    Me.Panel8.Controls.Add(Me.btnTransCurrPart)
    Me.Panel8.Controls.Add(Me.btnTransAdjoinPart)
    Me.Panel8.Dock = System.Windows.Forms.DockStyle.Fill
    Me.Panel8.Location = New System.Drawing.Point(390, 3)
    Me.Panel8.Name = "Panel8"
    Me.TableLayoutPanel1.SetRowSpan(Me.Panel8, 3)
    Me.Panel8.Size = New System.Drawing.Size(508, 217)
    Me.Panel8.TabIndex = 21
    '
    'btnNewFrame
    '
    Me.btnNewFrame.Location = New System.Drawing.Point(376, 22)
    Me.btnNewFrame.Name = "btnNewFrame"
    Me.btnNewFrame.Size = New System.Drawing.Size(61, 40)
    Me.btnNewFrame.TabIndex = 154
    Me.btnNewFrame.Text = "Add New Frame"
    Me.btnNewFrame.UseVisualStyleBackColor = True
    '
    'btnEditPart
    '
    Me.btnEditPart.Cursor = System.Windows.Forms.Cursors.Hand
    Me.btnEditPart.Enabled = False
    Me.btnEditPart.Location = New System.Drawing.Point(376, 131)
    Me.btnEditPart.Name = "btnEditPart"
    Me.btnEditPart.Size = New System.Drawing.Size(61, 57)
    Me.btnEditPart.TabIndex = 153
    Me.btnEditPart.Text = "Edit Frame Part"
    '
    'pnlCabinetInfo
    '
    Me.pnlCabinetInfo.Controls.Add(Me.txtCabinetInfo)
    Me.pnlCabinetInfo.Location = New System.Drawing.Point(90, 2)
    Me.pnlCabinetInfo.Name = "pnlCabinetInfo"
    Me.pnlCabinetInfo.Size = New System.Drawing.Size(264, 69)
    Me.pnlCabinetInfo.TabIndex = 0
    Me.pnlCabinetInfo.Visible = False
    '
    'txtCabinetInfo
    '
    Me.txtCabinetInfo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
    Me.txtCabinetInfo.Location = New System.Drawing.Point(3, 3)
    Me.txtCabinetInfo.Name = "txtCabinetInfo"
    Me.txtCabinetInfo.ReadOnly = True
    Me.txtCabinetInfo.Size = New System.Drawing.Size(258, 63)
    Me.txtCabinetInfo.TabIndex = 1
    Me.txtCabinetInfo.Text = ""
    '
    'btnNewCustPart
    '
    Me.btnNewCustPart.Cursor = System.Windows.Forms.Cursors.Hand
    Me.btnNewCustPart.Location = New System.Drawing.Point(376, 68)
    Me.btnNewCustPart.Name = "btnNewCustPart"
    Me.btnNewCustPart.Size = New System.Drawing.Size(61, 57)
    Me.btnNewCustPart.TabIndex = 152
    Me.btnNewCustPart.Text = "New Frame Part"
    '
    'Label13
    '
    Me.Label13.Location = New System.Drawing.Point(25, 2)
    Me.Label13.Name = "Label13"
    Me.Label13.Size = New System.Drawing.Size(113, 19)
    Me.Label13.TabIndex = 16
    Me.Label13.Text = "Frame Parts List"
    Me.Label13.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
    '
    'lbStilesRails
    '
    Me.lbStilesRails.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
    Me.lbStilesRails.FormattingEnabled = True
    Me.lbStilesRails.Location = New System.Drawing.Point(28, 22)
    Me.lbStilesRails.Name = "lbStilesRails"
    Me.lbStilesRails.Size = New System.Drawing.Size(342, 186)
    Me.lbStilesRails.TabIndex = 15
    '
    'btnTransCurrPart
    '
    Me.btnTransCurrPart.ImageIndex = 3
    Me.btnTransCurrPart.ImageList = Me.ImageList1
    Me.btnTransCurrPart.Location = New System.Drawing.Point(3, 34)
    Me.btnTransCurrPart.Name = "btnTransCurrPart"
    Me.btnTransCurrPart.Size = New System.Drawing.Size(24, 24)
    Me.btnTransCurrPart.TabIndex = 17
    Me.btnTransCurrPart.UseVisualStyleBackColor = True
    '
    'btnTransAdjoinPart
    '
    Me.btnTransAdjoinPart.ImageIndex = 3
    Me.btnTransAdjoinPart.ImageList = Me.ImageList1
    Me.btnTransAdjoinPart.Location = New System.Drawing.Point(2, 139)
    Me.btnTransAdjoinPart.Name = "btnTransAdjoinPart"
    Me.btnTransAdjoinPart.Size = New System.Drawing.Size(24, 24)
    Me.btnTransAdjoinPart.TabIndex = 18
    Me.btnTransAdjoinPart.UseVisualStyleBackColor = True
    '
    'Panel9
    '
    Me.Panel9.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
    Me.Panel9.Controls.Add(Me.btnCustOpg)
    Me.Panel9.Controls.Add(Me.btnEditOpg)
    Me.Panel9.Controls.Add(Me.Label11)
    Me.Panel9.Controls.Add(Me.lblCurrOpening)
    Me.Panel9.Controls.Add(Me.txtLibPath)
    Me.Panel9.Controls.Add(Me.checkHingeR)
    Me.Panel9.Controls.Add(Me.checkHingeL)
    Me.Panel9.Controls.Add(Me.lbOpenings)
    Me.Panel9.Controls.Add(Me.btnTransAdjoinOpg)
    Me.Panel9.Location = New System.Drawing.Point(390, 226)
    Me.Panel9.Name = "Panel9"
    Me.TableLayoutPanel1.SetRowSpan(Me.Panel9, 3)
    Me.Panel9.Size = New System.Drawing.Size(495, 258)
    Me.Panel9.TabIndex = 22
    '
    'btnCustOpg
    '
    Me.btnCustOpg.Cursor = System.Windows.Forms.Cursors.Hand
    Me.btnCustOpg.Enabled = False
    Me.btnCustOpg.Location = New System.Drawing.Point(376, 5)
    Me.btnCustOpg.Name = "btnCustOpg"
    Me.btnCustOpg.Size = New System.Drawing.Size(61, 57)
    Me.btnCustOpg.TabIndex = 139
    Me.btnCustOpg.Text = "Add Opening"
    '
    'btnEditOpg
    '
    Me.btnEditOpg.Cursor = System.Windows.Forms.Cursors.Hand
    Me.btnEditOpg.Enabled = False
    Me.btnEditOpg.Location = New System.Drawing.Point(376, 66)
    Me.btnEditOpg.Name = "btnEditOpg"
    Me.btnEditOpg.Size = New System.Drawing.Size(61, 57)
    Me.btnEditOpg.TabIndex = 138
    Me.btnEditOpg.Text = "Edit " & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Opening"
    '
    'Label11
    '
    Me.Label11.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.Label11.AutoSize = True
    Me.Label11.Location = New System.Drawing.Point(7, 237)
    Me.Label11.Name = "Label11"
    Me.Label11.Size = New System.Drawing.Size(66, 13)
    Me.Label11.TabIndex = 10
    Me.Label11.Text = "Library Path:"
    '
    'lblCurrOpening
    '
    Me.lblCurrOpening.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.lblCurrOpening.AutoSize = True
    Me.lblCurrOpening.Location = New System.Drawing.Point(230, 208)
    Me.lblCurrOpening.Name = "lblCurrOpening"
    Me.lblCurrOpening.Size = New System.Drawing.Size(109, 13)
    Me.lblCurrOpening.TabIndex = 129
    Me.lblCurrOpening.Text = "No Opening Selected"
    '
    'txtLibPath
    '
    Me.txtLibPath.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.txtLibPath.Location = New System.Drawing.Point(72, 237)
    Me.txtLibPath.Name = "txtLibPath"
    Me.txtLibPath.ReadOnly = True
    Me.txtLibPath.Size = New System.Drawing.Size(420, 20)
    Me.txtLibPath.TabIndex = 11
    '
    'checkHingeR
    '
    Me.checkHingeR.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.checkHingeR.Location = New System.Drawing.Point(114, 208)
    Me.checkHingeR.Name = "checkHingeR"
    Me.checkHingeR.Size = New System.Drawing.Size(94, 18)
    Me.checkHingeR.TabIndex = 128
    Me.checkHingeR.Text = "Hinge Right"
    '
    'checkHingeL
    '
    Me.checkHingeL.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.checkHingeL.Location = New System.Drawing.Point(26, 208)
    Me.checkHingeL.Name = "checkHingeL"
    Me.checkHingeL.Size = New System.Drawing.Size(82, 18)
    Me.checkHingeL.TabIndex = 127
    Me.checkHingeL.Text = "Hinge Left"
    '
    'lbOpenings
    '
    Me.lbOpenings.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
    Me.lbOpenings.FormattingEnabled = True
    Me.lbOpenings.Location = New System.Drawing.Point(28, 4)
    Me.lbOpenings.Name = "lbOpenings"
    Me.lbOpenings.Size = New System.Drawing.Size(342, 173)
    Me.lbOpenings.TabIndex = 0
    '
    'btnTransAdjoinOpg
    '
    Me.btnTransAdjoinOpg.ImageIndex = 3
    Me.btnTransAdjoinOpg.ImageList = Me.ImageList1
    Me.btnTransAdjoinOpg.Location = New System.Drawing.Point(3, 13)
    Me.btnTransAdjoinOpg.Name = "btnTransAdjoinOpg"
    Me.btnTransAdjoinOpg.Size = New System.Drawing.Size(24, 24)
    Me.btnTransAdjoinOpg.TabIndex = 19
    Me.btnTransAdjoinOpg.UseVisualStyleBackColor = True
    '
    'Panel7
    '
    Me.TableLayoutPanel1.SetColumnSpan(Me.Panel7, 2)
    Me.Panel7.Controls.Add(Me.lbSavedFiles)
    Me.Panel7.Dock = System.Windows.Forms.DockStyle.Fill
    Me.Panel7.Location = New System.Drawing.Point(3, 343)
    Me.Panel7.Margin = New System.Windows.Forms.Padding(3, 3, 3, 8)
    Me.Panel7.Name = "Panel7"
    Me.Panel7.Size = New System.Drawing.Size(381, 136)
    Me.Panel7.TabIndex = 20
    '
    'lbSavedFiles
    '
    Me.lbSavedFiles.ContextMenuStrip = Me.popupMenus
    Me.lbSavedFiles.Dock = System.Windows.Forms.DockStyle.Fill
    Me.lbSavedFiles.FormattingEnabled = True
    Me.lbSavedFiles.Location = New System.Drawing.Point(0, 0)
    Me.lbSavedFiles.MultiColumn = True
    Me.lbSavedFiles.Name = "lbSavedFiles"
    Me.lbSavedFiles.Size = New System.Drawing.Size(381, 134)
    Me.lbSavedFiles.TabIndex = 13
    '
    'popupMenus
    '
    Me.popupMenus.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.puMnuItemDelSelFile, Me.puMnuItemEditSelFile, Me.puMnuShowPartsSelFile})
    Me.popupMenus.Name = "popupMenus"
    Me.popupMenus.Size = New System.Drawing.Size(179, 70)
    '
    'puMnuItemDelSelFile
    '
    Me.puMnuItemDelSelFile.Name = "puMnuItemDelSelFile"
    Me.puMnuItemDelSelFile.Size = New System.Drawing.Size(178, 22)
    Me.puMnuItemDelSelFile.Text = "Delect Selected File"
    '
    'puMnuItemEditSelFile
    '
    Me.puMnuItemEditSelFile.Name = "puMnuItemEditSelFile"
    Me.puMnuItemEditSelFile.Size = New System.Drawing.Size(178, 22)
    Me.puMnuItemEditSelFile.Text = "Edit Selected File"
    '
    'puMnuShowPartsSelFile
    '
    Me.puMnuShowPartsSelFile.Name = "puMnuShowPartsSelFile"
    Me.puMnuShowPartsSelFile.Size = New System.Drawing.Size(178, 22)
    Me.puMnuShowPartsSelFile.Text = "Show Parts"
    '
    'tpJobDetail
    '
    Me.tpJobDetail.Controls.Add(Me.browserJobDetail)
    Me.tpJobDetail.Location = New System.Drawing.Point(4, 22)
    Me.tpJobDetail.Name = "tpJobDetail"
    Me.tpJobDetail.Padding = New System.Windows.Forms.Padding(3)
    Me.tpJobDetail.Size = New System.Drawing.Size(907, 605)
    Me.tpJobDetail.TabIndex = 4
    Me.tpJobDetail.Text = "Job Detail"
    Me.tpJobDetail.UseVisualStyleBackColor = True
    '
    'browserJobDetail
    '
    Me.browserJobDetail.Dock = System.Windows.Forms.DockStyle.Fill
    Me.browserJobDetail.Location = New System.Drawing.Point(3, 3)
    Me.browserJobDetail.MinimumSize = New System.Drawing.Size(20, 20)
    Me.browserJobDetail.Name = "browserJobDetail"
    Me.browserJobDetail.Size = New System.Drawing.Size(901, 599)
    Me.browserJobDetail.TabIndex = 0
    '
    'tpCatalog
    '
    Me.tpCatalog.Controls.Add(Me.browserCatalog)
    Me.tpCatalog.Location = New System.Drawing.Point(4, 22)
    Me.tpCatalog.Name = "tpCatalog"
    Me.tpCatalog.Padding = New System.Windows.Forms.Padding(3)
    Me.tpCatalog.Size = New System.Drawing.Size(907, 605)
    Me.tpCatalog.TabIndex = 3
    Me.tpCatalog.Text = "eCatalog"
    Me.tpCatalog.UseVisualStyleBackColor = True
    '
    'browserCatalog
    '
    Me.browserCatalog.Dock = System.Windows.Forms.DockStyle.Fill
    Me.browserCatalog.Location = New System.Drawing.Point(3, 3)
    Me.browserCatalog.MinimumSize = New System.Drawing.Size(20, 20)
    Me.browserCatalog.Name = "browserCatalog"
    Me.browserCatalog.Size = New System.Drawing.Size(901, 599)
    Me.browserCatalog.TabIndex = 0
    '
    'tpStart
    '
    Me.tpStart.Controls.Add(Me.FlowLayoutPanel1)
    Me.tpStart.Controls.Add(Me.pnlTpStart)
    Me.tpStart.Location = New System.Drawing.Point(4, 22)
    Me.tpStart.Name = "tpStart"
    Me.tpStart.Padding = New System.Windows.Forms.Padding(3)
    Me.tpStart.Size = New System.Drawing.Size(907, 605)
    Me.tpStart.TabIndex = 0
    Me.tpStart.Text = "Start"
    Me.tpStart.UseVisualStyleBackColor = True
    '
    'FlowLayoutPanel1
    '
    Me.FlowLayoutPanel1.Controls.Add(Me.Panel1)
    Me.FlowLayoutPanel1.Controls.Add(Me.groupFrameStyleold)
    Me.FlowLayoutPanel1.Controls.Add(Me.groupHingeStyleold)
    Me.FlowLayoutPanel1.Controls.Add(Me.groupThicknessold)
    Me.FlowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill
    Me.FlowLayoutPanel1.Location = New System.Drawing.Point(3, 3)
    Me.FlowLayoutPanel1.Name = "FlowLayoutPanel1"
    Me.FlowLayoutPanel1.Size = New System.Drawing.Size(557, 599)
    Me.FlowLayoutPanel1.TabIndex = 1
    '
    'Panel1
    '
    Me.Panel1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.Panel1.Controls.Add(Me.btnManualCreateFrame)
    Me.Panel1.Controls.Add(Me.btnJob)
    Me.Panel1.Location = New System.Drawing.Point(3, 3)
    Me.Panel1.Name = "Panel1"
    Me.Panel1.Size = New System.Drawing.Size(893, 35)
    Me.Panel1.TabIndex = 1
    '
    'btnManualCreateFrame
    '
    Me.btnManualCreateFrame.Image = CType(resources.GetObject("btnManualCreateFrame.Image"), System.Drawing.Image)
    Me.btnManualCreateFrame.ImageAlign = System.Drawing.ContentAlignment.TopLeft
    Me.btnManualCreateFrame.Location = New System.Drawing.Point(155, 4)
    Me.btnManualCreateFrame.Name = "btnManualCreateFrame"
    Me.btnManualCreateFrame.Size = New System.Drawing.Size(150, 23)
    Me.btnManualCreateFrame.TabIndex = 1
    Me.btnManualCreateFrame.Text = "Create Frames Manually"
    Me.btnManualCreateFrame.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
    Me.btnManualCreateFrame.UseVisualStyleBackColor = True
    '
    'btnJob
    '
    Me.btnJob.Image = CType(resources.GetObject("btnJob.Image"), System.Drawing.Image)
    Me.btnJob.ImageAlign = System.Drawing.ContentAlignment.TopLeft
    Me.btnJob.Location = New System.Drawing.Point(4, 4)
    Me.btnJob.Name = "btnJob"
    Me.btnJob.Size = New System.Drawing.Size(136, 23)
    Me.btnJob.TabIndex = 0
    Me.btnJob.Text = "Import Parts from Job"
    Me.btnJob.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
    Me.btnJob.UseVisualStyleBackColor = True
    '
    'groupFrameStyleold
    '
    Me.groupFrameStyleold.Location = New System.Drawing.Point(3, 44)
    Me.groupFrameStyleold.Name = "groupFrameStyleold"
    Me.groupFrameStyleold.Size = New System.Drawing.Size(200, 100)
    Me.groupFrameStyleold.TabIndex = 3
    Me.groupFrameStyleold.TabStop = False
    Me.groupFrameStyleold.Text = "Frame Style"
    '
    'groupHingeStyleold
    '
    Me.groupHingeStyleold.Location = New System.Drawing.Point(209, 44)
    Me.groupHingeStyleold.Name = "groupHingeStyleold"
    Me.groupHingeStyleold.Size = New System.Drawing.Size(200, 100)
    Me.groupHingeStyleold.TabIndex = 4
    Me.groupHingeStyleold.TabStop = False
    Me.groupHingeStyleold.Text = "Hinge Style"
    '
    'groupThicknessold
    '
    Me.groupThicknessold.Location = New System.Drawing.Point(3, 150)
    Me.groupThicknessold.Name = "groupThicknessold"
    Me.groupThicknessold.Size = New System.Drawing.Size(200, 100)
    Me.groupThicknessold.TabIndex = 5
    Me.groupThicknessold.TabStop = False
    Me.groupThicknessold.Text = "Frame Thickness"
    '
    'pnlTpStart
    '
    Me.pnlTpStart.Controls.Add(Me.PictureBox1)
    Me.pnlTpStart.Dock = System.Windows.Forms.DockStyle.Right
    Me.pnlTpStart.Location = New System.Drawing.Point(560, 3)
    Me.pnlTpStart.Name = "pnlTpStart"
    Me.pnlTpStart.Size = New System.Drawing.Size(344, 599)
    Me.pnlTpStart.TabIndex = 0
    '
    'PictureBox1
    '
    Me.PictureBox1.Image = CType(resources.GetObject("PictureBox1.Image"), System.Drawing.Image)
    Me.PictureBox1.Location = New System.Drawing.Point(40, 137)
    Me.PictureBox1.Name = "PictureBox1"
    Me.PictureBox1.Size = New System.Drawing.Size(273, 252)
    Me.PictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
    Me.PictureBox1.TabIndex = 1
    Me.PictureBox1.TabStop = False
    '
    'StatusStrip1
    '
    Me.StatusStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.sbPanelMessage, Me.sbPanelVersion})
    Me.StatusStrip1.Location = New System.Drawing.Point(0, 631)
    Me.StatusStrip1.Name = "StatusStrip1"
    Me.StatusStrip1.ShowItemToolTips = True
    Me.StatusStrip1.Size = New System.Drawing.Size(915, 22)
    Me.StatusStrip1.TabIndex = 0
    Me.StatusStrip1.Text = "StatusStrip1"
    '
    'sbPanelMessage
    '
    Me.sbPanelMessage.AutoSize = False
    Me.sbPanelMessage.Name = "sbPanelMessage"
    Me.sbPanelMessage.Size = New System.Drawing.Size(450, 17)
    Me.sbPanelMessage.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
    '
    'sbPanelVersion
    '
    Me.sbPanelVersion.AutoSize = False
    Me.sbPanelVersion.Name = "sbPanelVersion"
    Me.sbPanelVersion.Size = New System.Drawing.Size(350, 17)
    Me.sbPanelVersion.Text = "sbPanelVersion"
    '
    'MenuStrip1
    '
    Me.MenuStrip1.Dock = System.Windows.Forms.DockStyle.None
    Me.MenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.FileToolStripMenuItem, Me.EditToolStripMenuItem, Me.ToolStripMenuItem1, Me.toolsToolStripMenuItem, Me.mnuAbout})
    Me.MenuStrip1.Location = New System.Drawing.Point(0, 0)
    Me.MenuStrip1.Name = "MenuStrip1"
    Me.MenuStrip1.Size = New System.Drawing.Size(915, 24)
    Me.MenuStrip1.TabIndex = 0
    Me.MenuStrip1.Text = "MenuStrip1"
    '
    'FileToolStripMenuItem
    '
    Me.FileToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuSelectJob, Me.PrintToolStripMenuItem, Me.mnuExit})
    Me.FileToolStripMenuItem.Name = "FileToolStripMenuItem"
    Me.FileToolStripMenuItem.Size = New System.Drawing.Size(35, 20)
    Me.FileToolStripMenuItem.Text = "&File"
    '
    'mnuSelectJob
    '
    Me.mnuSelectJob.Name = "mnuSelectJob"
    Me.mnuSelectJob.Size = New System.Drawing.Size(134, 22)
    Me.mnuSelectJob.Text = "Select Job"
    '
    'PrintToolStripMenuItem
    '
    Me.PrintToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuPrintBC4Files})
    Me.PrintToolStripMenuItem.Name = "PrintToolStripMenuItem"
    Me.PrintToolStripMenuItem.Size = New System.Drawing.Size(134, 22)
    Me.PrintToolStripMenuItem.Text = "Print"
    '
    'mnuPrintBC4Files
    '
    Me.mnuPrintBC4Files.Name = "mnuPrintBC4Files"
    Me.mnuPrintBC4Files.Size = New System.Drawing.Size(154, 22)
    Me.mnuPrintBC4Files.Text = "Print Barcodes"
    '
    'mnuExit
    '
    Me.mnuExit.Name = "mnuExit"
    Me.mnuExit.Size = New System.Drawing.Size(134, 22)
    Me.mnuExit.Text = "E&xit"
    '
    'EditToolStripMenuItem
    '
    Me.EditToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.menuEditPrograms, Me.toolStripSeparator3, Me.CutToolStripMenuItem, Me.CopyToolStripMenuItem, Me.PasteToolStripMenuItem, Me.toolStripSeparator4, Me.SelectAllToolStripMenuItem})
    Me.EditToolStripMenuItem.Name = "EditToolStripMenuItem"
    Me.EditToolStripMenuItem.Size = New System.Drawing.Size(37, 20)
    Me.EditToolStripMenuItem.Text = "&Edit"
    '
    'menuEditPrograms
    '
    Me.menuEditPrograms.Name = "menuEditPrograms"
    Me.menuEditPrograms.Size = New System.Drawing.Size(202, 22)
    Me.menuEditPrograms.Text = "View / Edit Program Files"
    '
    'toolStripSeparator3
    '
    Me.toolStripSeparator3.Name = "toolStripSeparator3"
    Me.toolStripSeparator3.Size = New System.Drawing.Size(199, 6)
    '
    'CutToolStripMenuItem
    '
    Me.CutToolStripMenuItem.Image = CType(resources.GetObject("CutToolStripMenuItem.Image"), System.Drawing.Image)
    Me.CutToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Magenta
    Me.CutToolStripMenuItem.Name = "CutToolStripMenuItem"
    Me.CutToolStripMenuItem.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.X), System.Windows.Forms.Keys)
    Me.CutToolStripMenuItem.Size = New System.Drawing.Size(202, 22)
    Me.CutToolStripMenuItem.Text = "Cu&t"
    '
    'CopyToolStripMenuItem
    '
    Me.CopyToolStripMenuItem.Image = CType(resources.GetObject("CopyToolStripMenuItem.Image"), System.Drawing.Image)
    Me.CopyToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Magenta
    Me.CopyToolStripMenuItem.Name = "CopyToolStripMenuItem"
    Me.CopyToolStripMenuItem.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.C), System.Windows.Forms.Keys)
    Me.CopyToolStripMenuItem.Size = New System.Drawing.Size(202, 22)
    Me.CopyToolStripMenuItem.Text = "&Copy"
    '
    'PasteToolStripMenuItem
    '
    Me.PasteToolStripMenuItem.Image = CType(resources.GetObject("PasteToolStripMenuItem.Image"), System.Drawing.Image)
    Me.PasteToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Magenta
    Me.PasteToolStripMenuItem.Name = "PasteToolStripMenuItem"
    Me.PasteToolStripMenuItem.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.V), System.Windows.Forms.Keys)
    Me.PasteToolStripMenuItem.Size = New System.Drawing.Size(202, 22)
    Me.PasteToolStripMenuItem.Text = "&Paste"
    '
    'toolStripSeparator4
    '
    Me.toolStripSeparator4.Name = "toolStripSeparator4"
    Me.toolStripSeparator4.Size = New System.Drawing.Size(199, 6)
    '
    'SelectAllToolStripMenuItem
    '
    Me.SelectAllToolStripMenuItem.Name = "SelectAllToolStripMenuItem"
    Me.SelectAllToolStripMenuItem.Size = New System.Drawing.Size(202, 22)
    Me.SelectAllToolStripMenuItem.Text = "Select &All"
    '
    'ToolStripMenuItem1
    '
    Me.ToolStripMenuItem1.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuBarcodePartReport, Me.mnuMateralRequirements})
    Me.ToolStripMenuItem1.Name = "ToolStripMenuItem1"
    Me.ToolStripMenuItem1.Size = New System.Drawing.Size(57, 20)
    Me.ToolStripMenuItem1.Text = "&Reports"
    '
    'mnuBarcodePartReport
    '
    Me.mnuBarcodePartReport.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuPrintBarcodeReport, Me.mnuPrintBarcodes2})
    Me.mnuBarcodePartReport.Name = "mnuBarcodePartReport"
    Me.mnuBarcodePartReport.Size = New System.Drawing.Size(192, 22)
    Me.mnuBarcodePartReport.Text = "Program Barcode"
    '
    'mnuPrintBarcodeReport
    '
    Me.mnuPrintBarcodeReport.Name = "mnuPrintBarcodeReport"
    Me.mnuPrintBarcodeReport.Size = New System.Drawing.Size(171, 22)
    Me.mnuPrintBarcodeReport.Text = "Barcode Parts List"
    '
    'mnuPrintBarcodes2
    '
    Me.mnuPrintBarcodes2.Name = "mnuPrintBarcodes2"
    Me.mnuPrintBarcodes2.Size = New System.Drawing.Size(171, 22)
    Me.mnuPrintBarcodes2.Text = "Print Barcodes"
    '
    'mnuMateralRequirements
    '
    Me.mnuMateralRequirements.Name = "mnuMateralRequirements"
    Me.mnuMateralRequirements.Size = New System.Drawing.Size(192, 22)
    Me.mnuMateralRequirements.Text = "Material Requirements"
    '
    'toolsToolStripMenuItem
    '
    Me.toolsToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.menuPurgeOldData, Me.menuOptions, Me.mnuUpdateFiles})
    Me.toolsToolStripMenuItem.Name = "toolsToolStripMenuItem"
    Me.toolsToolStripMenuItem.Size = New System.Drawing.Size(44, 20)
    Me.toolsToolStripMenuItem.Text = "&Tools"
    '
    'menuPurgeOldData
    '
    Me.menuPurgeOldData.Name = "menuPurgeOldData"
    Me.menuPurgeOldData.Size = New System.Drawing.Size(163, 22)
    Me.menuPurgeOldData.Text = "Purge Old Data"
    '
    'menuOptions
    '
    Me.menuOptions.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.menuEditSetupData})
    Me.menuOptions.Name = "menuOptions"
    Me.menuOptions.Size = New System.Drawing.Size(163, 22)
    Me.menuOptions.Text = "&Options"
    '
    'menuEditSetupData
    '
    Me.menuEditSetupData.Name = "menuEditSetupData"
    Me.menuEditSetupData.Size = New System.Drawing.Size(176, 22)
    Me.menuEditSetupData.Text = "Edit Style Features"
    '
    'mnuUpdateFiles
    '
    Me.mnuUpdateFiles.Name = "mnuUpdateFiles"
    Me.mnuUpdateFiles.Size = New System.Drawing.Size(163, 22)
    Me.mnuUpdateFiles.Text = "Update Program"
    '
    'mnuAbout
    '
    Me.mnuAbout.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.AboutToolStripMenuItem})
    Me.mnuAbout.Name = "mnuAbout"
    Me.mnuAbout.Size = New System.Drawing.Size(40, 20)
    Me.mnuAbout.Text = "&Help"
    '
    'AboutToolStripMenuItem
    '
    Me.AboutToolStripMenuItem.Name = "AboutToolStripMenuItem"
    Me.AboutToolStripMenuItem.Size = New System.Drawing.Size(114, 22)
    Me.AboutToolStripMenuItem.Text = "About"
    '
    'ToolStrip1
    '
    Me.ToolStrip1.AllowMerge = False
    Me.ToolStrip1.Dock = System.Windows.Forms.DockStyle.None
    Me.ToolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden
    Me.ToolStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.labelJobNumberKey, Me.labelStatusJobNo, Me.ToolStripSeparator1, Me.lblSelectItem, Me.comboItems, Me.ToolStripSeparator2, Me.toolBtnCabInfo, Me.toolBtnJobdetails, Me.toolBtnCatalog, Me.toolBtnPrintBarcodeReport, Me.ToolStripSeparator5, Me.toolBtnNavHome, Me.toolBtnNavBack, Me.toolBtnNavForward, Me.toolBtnPrintBrowser})
    Me.ToolStrip1.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow
    Me.ToolStrip1.Location = New System.Drawing.Point(3, 24)
    Me.ToolStrip1.Name = "ToolStrip1"
    Me.ToolStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional
    Me.ToolStrip1.Size = New System.Drawing.Size(824, 25)
    Me.ToolStrip1.TabIndex = 1
    '
    'labelJobNumberKey
    '
    Me.labelJobNumberKey.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.labelJobNumberKey.Name = "labelJobNumberKey"
    Me.labelJobNumberKey.Size = New System.Drawing.Size(77, 13)
    Me.labelJobNumberKey.Text = "Job Number:"
    '
    'labelStatusJobNo
    '
    Me.labelStatusJobNo.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text
    Me.labelStatusJobNo.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.labelStatusJobNo.LinkBehavior = System.Windows.Forms.LinkBehavior.AlwaysUnderline
    Me.labelStatusJobNo.Name = "labelStatusJobNo"
    Me.labelStatusJobNo.Size = New System.Drawing.Size(102, 13)
    Me.labelStatusJobNo.Text = "No Job Selected! "
    '
    'ToolStripSeparator1
    '
    Me.ToolStripSeparator1.AutoSize = False
    Me.ToolStripSeparator1.Name = "ToolStripSeparator1"
    Me.ToolStripSeparator1.Size = New System.Drawing.Size(16, 25)
    '
    'lblSelectItem
    '
    Me.lblSelectItem.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
    Me.lblSelectItem.Name = "lblSelectItem"
    Me.lblSelectItem.Size = New System.Drawing.Size(76, 13)
    Me.lblSelectItem.Text = "Select Item:"
    '
    'comboItems
    '
    Me.comboItems.Name = "comboItems"
    Me.comboItems.Size = New System.Drawing.Size(88, 21)
    Me.comboItems.Text = " "
    '
    'ToolStripSeparator2
    '
    Me.ToolStripSeparator2.AutoSize = False
    Me.ToolStripSeparator2.Name = "ToolStripSeparator2"
    Me.ToolStripSeparator2.Size = New System.Drawing.Size(36, 23)
    '
    'toolBtnCabInfo
    '
    Me.toolBtnCabInfo.CheckOnClick = True
    Me.toolBtnCabInfo.ImageTransparentColor = System.Drawing.Color.Magenta
    Me.toolBtnCabInfo.Name = "toolBtnCabInfo"
    Me.toolBtnCabInfo.Size = New System.Drawing.Size(107, 17)
    Me.toolBtnCabInfo.Text = "Cabinet Information"
    Me.toolBtnCabInfo.ToolTipText = "Launch cabinet sketch in Internet Explorer"
    '
    'toolBtnJobdetails
    '
    Me.toolBtnJobdetails.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text
    Me.toolBtnJobdetails.Image = CType(resources.GetObject("toolBtnJobdetails.Image"), System.Drawing.Image)
    Me.toolBtnJobdetails.ImageTransparentColor = System.Drawing.Color.Magenta
    Me.toolBtnJobdetails.Name = "toolBtnJobdetails"
    Me.toolBtnJobdetails.Size = New System.Drawing.Size(83, 17)
    Me.toolBtnJobdetails.Text = "Get Job Details"
    Me.toolBtnJobdetails.ToolTipText = "Launch Job Info in Internet Explorer"
    '
    'toolBtnCatalog
    '
    Me.toolBtnCatalog.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text
    Me.toolBtnCatalog.Image = CType(resources.GetObject("toolBtnCatalog.Image"), System.Drawing.Image)
    Me.toolBtnCatalog.ImageTransparentColor = System.Drawing.Color.Magenta
    Me.toolBtnCatalog.Name = "toolBtnCatalog"
    Me.toolBtnCatalog.Size = New System.Drawing.Size(54, 17)
    Me.toolBtnCatalog.Text = "eCatalog"
    Me.toolBtnCatalog.ToolTipText = "Browse the eCatalog"
    '
    'toolBtnPrintBarcodeReport
    '
    Me.toolBtnPrintBarcodeReport.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text
    Me.toolBtnPrintBarcodeReport.Image = CType(resources.GetObject("toolBtnPrintBarcodeReport.Image"), System.Drawing.Image)
    Me.toolBtnPrintBarcodeReport.ImageTransparentColor = System.Drawing.Color.Magenta
    Me.toolBtnPrintBarcodeReport.Name = "toolBtnPrintBarcodeReport"
    Me.toolBtnPrintBarcodeReport.Size = New System.Drawing.Size(80, 17)
    Me.toolBtnPrintBarcodeReport.Text = "Print Barcodes"
    '
    'ToolStripSeparator5
    '
    Me.ToolStripSeparator5.AutoSize = False
    Me.ToolStripSeparator5.Name = "ToolStripSeparator5"
    Me.ToolStripSeparator5.Size = New System.Drawing.Size(27, 23)
    '
    'toolBtnNavHome
    '
    Me.toolBtnNavHome.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text
    Me.toolBtnNavHome.Image = CType(resources.GetObject("toolBtnNavHome.Image"), System.Drawing.Image)
    Me.toolBtnNavHome.ImageTransparentColor = System.Drawing.Color.Magenta
    Me.toolBtnNavHome.Name = "toolBtnNavHome"
    Me.toolBtnNavHome.Size = New System.Drawing.Size(38, 17)
    Me.toolBtnNavHome.Text = "Home"
    Me.toolBtnNavHome.ToolTipText = "Navigate to top job page."
    Me.toolBtnNavHome.Visible = False
    '
    'toolBtnNavBack
    '
    Me.toolBtnNavBack.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text
    Me.toolBtnNavBack.Enabled = False
    Me.toolBtnNavBack.Image = CType(resources.GetObject("toolBtnNavBack.Image"), System.Drawing.Image)
    Me.toolBtnNavBack.ImageTransparentColor = System.Drawing.Color.Magenta
    Me.toolBtnNavBack.Name = "toolBtnNavBack"
    Me.toolBtnNavBack.Size = New System.Drawing.Size(33, 17)
    Me.toolBtnNavBack.Text = "Back"
    Me.toolBtnNavBack.ToolTipText = "Navigate backwards through page history."
    Me.toolBtnNavBack.Visible = False
    '
    'toolBtnNavForward
    '
    Me.toolBtnNavForward.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text
    Me.toolBtnNavForward.Enabled = False
    Me.toolBtnNavForward.Image = CType(resources.GetObject("toolBtnNavForward.Image"), System.Drawing.Image)
    Me.toolBtnNavForward.ImageTransparentColor = System.Drawing.Color.Magenta
    Me.toolBtnNavForward.Name = "toolBtnNavForward"
    Me.toolBtnNavForward.Size = New System.Drawing.Size(51, 17)
    Me.toolBtnNavForward.Text = "Forward"
    Me.toolBtnNavForward.ToolTipText = "Navigate forward through page history."
    Me.toolBtnNavForward.Visible = False
    '
    'toolBtnPrintBrowser
    '
    Me.toolBtnPrintBrowser.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text
    Me.toolBtnPrintBrowser.Image = CType(resources.GetObject("toolBtnPrintBrowser.Image"), System.Drawing.Image)
    Me.toolBtnPrintBrowser.ImageTransparentColor = System.Drawing.Color.Magenta
    Me.toolBtnPrintBrowser.Name = "toolBtnPrintBrowser"
    Me.toolBtnPrintBrowser.Size = New System.Drawing.Size(75, 17)
    Me.toolBtnPrintBrowser.Text = "Print Browser"
    Me.toolBtnPrintBrowser.ToolTipText = "Print Web Browser Contents"
    '
    'popupCabList
    '
    Me.popupCabList.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuMove2List})
    Me.popupCabList.Name = "popupCabList"
    Me.popupCabList.Size = New System.Drawing.Size(187, 26)
    '
    'mnuMove2List
    '
    Me.mnuMove2List.Name = "mnuMove2List"
    Me.mnuMove2List.Size = New System.Drawing.Size(186, 22)
    Me.mnuMove2List.Text = "Move to Program List"
    '
    'popupParts
    '
    Me.popupParts.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuAddEditPartComment})
    Me.popupParts.Name = "popupParts"
    Me.popupParts.Size = New System.Drawing.Size(175, 26)
    '
    'mnuAddEditPartComment
    '
    Me.mnuAddEditPartComment.Name = "mnuAddEditPartComment"
    Me.mnuAddEditPartComment.Size = New System.Drawing.Size(174, 22)
    Me.mnuAddEditPartComment.Text = "Add/Edit Comment"
    '
    'MainForm
    '
    Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
    Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
    Me.ClientSize = New System.Drawing.Size(915, 702)
    Me.Controls.Add(Me.ToolStripContainer1)
    Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
    Me.MainMenuStrip = Me.MenuStrip1
    Me.Name = "MainForm"
    Me.Text = "Accu-System Program Configuration Manager"
    Me.ToolStripContainer1.ContentPanel.ResumeLayout(False)
    Me.ToolStripContainer1.ContentPanel.PerformLayout()
    Me.ToolStripContainer1.TopToolStripPanel.ResumeLayout(False)
    Me.ToolStripContainer1.TopToolStripPanel.PerformLayout()
    Me.ToolStripContainer1.ResumeLayout(False)
    Me.ToolStripContainer1.PerformLayout()
    Me.TabControl1.ResumeLayout(False)
    Me.tpNewStart.ResumeLayout(False)
    Me.SplitContainer3.Panel1.ResumeLayout(False)
    Me.SplitContainer3.Panel2.ResumeLayout(False)
    Me.SplitContainer3.ResumeLayout(False)
    Me.pnlFilterBase.ResumeLayout(False)
    Me.pnlDateRangeFilter.ResumeLayout(False)
    Me.pnlDateRangeFilter.PerformLayout()
    Me.tpSelect.ResumeLayout(False)
    Me.SplitContainer2.Panel1.ResumeLayout(False)
    Me.SplitContainer2.Panel2.ResumeLayout(False)
    Me.SplitContainer2.ResumeLayout(False)
    Me.TabControl2.ResumeLayout(False)
    Me.tpStyles.ResumeLayout(False)
    Me.FlowLayoutPanel2.ResumeLayout(False)
    Me.pnlStyleLabel.ResumeLayout(False)
    Me.pnlStyleLabel.PerformLayout()
    Me.Panel6.ResumeLayout(False)
    Me.Panel6.PerformLayout()
    Me.groupRadioCSV.ResumeLayout(False)
    Me.groupRadioCSV.PerformLayout()
    Me.groupExistingData.ResumeLayout(False)
    Me.groupExistingData.PerformLayout()
    Me.pnlSourceLabel.ResumeLayout(False)
    Me.pnlSourceLabel.PerformLayout()
    Me.tpProgList.ResumeLayout(False)
    Me.SplitContainer1.Panel1.ResumeLayout(False)
    Me.SplitContainer1.Panel2.ResumeLayout(False)
    Me.SplitContainer1.ResumeLayout(False)
    Me.popupTvProg.ResumeLayout(False)
    Me.Panel3.ResumeLayout(False)
    Me.Panel2.ResumeLayout(False)
    Me.tpPartsLayout.ResumeLayout(False)
    Me.TableLayoutPanel1.ResumeLayout(False)
    Me.TableLayoutPanel1.PerformLayout()
    Me.pnlCurrentPart.ResumeLayout(False)
    Me.pnlCurrentPart.PerformLayout()
    Me.Panel4.ResumeLayout(False)
    Me.Panel5.ResumeLayout(False)
    Me.Panel5.PerformLayout()
    Me.Panel8.ResumeLayout(False)
    Me.pnlCabinetInfo.ResumeLayout(False)
    Me.Panel9.ResumeLayout(False)
    Me.Panel9.PerformLayout()
    Me.Panel7.ResumeLayout(False)
    Me.popupMenus.ResumeLayout(False)
    Me.tpJobDetail.ResumeLayout(False)
    Me.tpCatalog.ResumeLayout(False)
    Me.tpStart.ResumeLayout(False)
    Me.FlowLayoutPanel1.ResumeLayout(False)
    Me.Panel1.ResumeLayout(False)
    Me.pnlTpStart.ResumeLayout(False)
    CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
    Me.StatusStrip1.ResumeLayout(False)
    Me.StatusStrip1.PerformLayout()
    Me.MenuStrip1.ResumeLayout(False)
    Me.MenuStrip1.PerformLayout()
    Me.ToolStrip1.ResumeLayout(False)
    Me.ToolStrip1.PerformLayout()
    Me.popupCabList.ResumeLayout(False)
    Me.popupParts.ResumeLayout(False)
    Me.ResumeLayout(False)

  End Sub
  Friend WithEvents ToolStripContainer1 As System.Windows.Forms.ToolStripContainer
  Friend WithEvents TabControl1 As System.Windows.Forms.TabControl
  Friend WithEvents tpStart As System.Windows.Forms.TabPage
  Friend WithEvents tpProgList As System.Windows.Forms.TabPage
  Friend WithEvents StatusStrip1 As System.Windows.Forms.StatusStrip
  Friend WithEvents pnlTpStart As System.Windows.Forms.Panel
  Friend WithEvents groupThicknessold As System.Windows.Forms.GroupBox
  Friend WithEvents groupHingeStyleold As System.Windows.Forms.GroupBox
  Friend WithEvents groupFrameStyleold As System.Windows.Forms.GroupBox
  Friend WithEvents FlowLayoutPanel1 As System.Windows.Forms.FlowLayoutPanel
  Friend WithEvents btnManualCreateFrame As System.Windows.Forms.Button
  Friend WithEvents btnJob As System.Windows.Forms.Button
  Friend WithEvents MenuStrip1 As System.Windows.Forms.MenuStrip
  Friend WithEvents FileToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
  Friend WithEvents PrintToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
  Friend WithEvents mnuPrintBC4Files As System.Windows.Forms.ToolStripMenuItem
  Friend WithEvents mnuAbout As System.Windows.Forms.ToolStripMenuItem
  Friend WithEvents mnuExit As System.Windows.Forms.ToolStripMenuItem
  Friend WithEvents AboutToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
  Friend WithEvents ToolStrip1 As System.Windows.Forms.ToolStrip
  Friend WithEvents labelJobNumberKey As System.Windows.Forms.ToolStripLabel
  Friend WithEvents labelStatusJobNo As System.Windows.Forms.ToolStripLabel
  Friend WithEvents ToolStripSeparator1 As System.Windows.Forms.ToolStripSeparator
  Friend WithEvents lblSelectItem As System.Windows.Forms.ToolStripLabel
  Friend WithEvents comboItems As System.Windows.Forms.ToolStripComboBox
  Friend WithEvents pnlCabinetInfo As System.Windows.Forms.Panel
  Friend WithEvents ImageList1 As System.Windows.Forms.ImageList
  Friend WithEvents tpPartsLayout As System.Windows.Forms.TabPage
  Friend WithEvents tpCatalog As System.Windows.Forms.TabPage
  Friend WithEvents browserCatalog As System.Windows.Forms.WebBrowser
  Friend WithEvents tpJobDetail As System.Windows.Forms.TabPage
  Friend WithEvents browserJobDetail As System.Windows.Forms.WebBrowser
  Friend WithEvents sbPanelMessage As System.Windows.Forms.ToolStripStatusLabel
  Friend WithEvents sbPanelVersion As System.Windows.Forms.ToolStripStatusLabel
  Friend WithEvents PictureBox1 As System.Windows.Forms.PictureBox
  Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
  Friend WithEvents tvProgList As System.Windows.Forms.TreeView
  Friend WithEvents Panel3 As System.Windows.Forms.Panel
  Friend WithEvents lbCabList As System.Windows.Forms.ListBox
  Friend WithEvents popupTvProg As System.Windows.Forms.ContextMenuStrip
  Friend WithEvents mnuAddPartComment As System.Windows.Forms.ToolStripMenuItem
  Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
  Friend WithEvents popupCabList As System.Windows.Forms.ContextMenuStrip
  Friend WithEvents mnuMove2List As System.Windows.Forms.ToolStripMenuItem
  Friend WithEvents popupParts As System.Windows.Forms.ContextMenuStrip
  Friend WithEvents mnuAddEditPartComment As System.Windows.Forms.ToolStripMenuItem
  Friend WithEvents popupMenus As System.Windows.Forms.ContextMenuStrip
  Friend WithEvents puMnuItemDelSelFile As System.Windows.Forms.ToolStripMenuItem
  Friend WithEvents puMnuItemEditSelFile As System.Windows.Forms.ToolStripMenuItem
  Friend WithEvents puMnuShowPartsSelFile As System.Windows.Forms.ToolStripMenuItem
  Friend WithEvents btnTvUp As System.Windows.Forms.Button
  Friend WithEvents btnTvDown As System.Windows.Forms.Button
  Friend WithEvents btnRemoveProg As System.Windows.Forms.Button
  Friend WithEvents cbxPartName As System.Windows.Forms.ComboBox
  Friend WithEvents pnlCurrentPart As System.Windows.Forms.Panel
  Friend WithEvents Label1 As System.Windows.Forms.Label
  Friend WithEvents txtPartName As System.Windows.Forms.TextBox
  Friend WithEvents Label7 As System.Windows.Forms.Label
  Friend WithEvents Label6 As System.Windows.Forms.Label
  Friend WithEvents Label5 As System.Windows.Forms.Label
  Friend WithEvents Label4 As System.Windows.Forms.Label
  Friend WithEvents Label3 As System.Windows.Forms.Label
  Friend WithEvents Label2 As System.Windows.Forms.Label
  Friend WithEvents txtPartCode As System.Windows.Forms.TextBox
  Friend WithEvents comboPartType As System.Windows.Forms.ComboBox
  Friend WithEvents txtPartHght As System.Windows.Forms.TextBox
  Friend WithEvents txtPartWdth As System.Windows.Forms.TextBox
  Friend WithEvents txtPartItem As System.Windows.Forms.TextBox
  Friend WithEvents txtPartThick As System.Windows.Forms.TextBox
  Friend WithEvents Label9 As System.Windows.Forms.Label
  Friend WithEvents Label8 As System.Windows.Forms.Label
  Friend WithEvents lbAdjoinParts As System.Windows.Forms.ListBox
  Friend WithEvents Label10 As System.Windows.Forms.Label
  Friend WithEvents btnProcessPart As System.Windows.Forms.Button
  Friend WithEvents btnClearOperations As System.Windows.Forms.Button
  Friend WithEvents btnPartsClearForm As System.Windows.Forms.Button
  Friend WithEvents btnRemove As System.Windows.Forms.Button
  Friend WithEvents btnDown As System.Windows.Forms.Button
  Friend WithEvents btnUp As System.Windows.Forms.Button
  Friend WithEvents lbSavedFiles As System.Windows.Forms.ListBox
  Friend WithEvents Label12 As System.Windows.Forms.Label
  Friend WithEvents FramePartImagePanel1 As FramePartImagePanel.FramePartImagePanel
  Friend WithEvents lbStilesRails As System.Windows.Forms.ListBox
  Friend WithEvents Label13 As System.Windows.Forms.Label
  Friend WithEvents btnTransAdjoinOpg As System.Windows.Forms.Button
  Friend WithEvents btnTransAdjoinPart As System.Windows.Forms.Button
  Friend WithEvents btnTransCurrPart As System.Windows.Forms.Button
  Friend WithEvents TableLayoutPanel1 As System.Windows.Forms.TableLayoutPanel
  Friend WithEvents Panel4 As System.Windows.Forms.Panel
  Friend WithEvents Panel5 As System.Windows.Forms.Panel
  Friend WithEvents Panel7 As System.Windows.Forms.Panel
  Friend WithEvents Panel8 As System.Windows.Forms.Panel
  Friend WithEvents Panel9 As System.Windows.Forms.Panel
  Friend WithEvents lbOpenings As System.Windows.Forms.ListBox
  Friend WithEvents lblCurrOpening As System.Windows.Forms.Label
  Friend WithEvents checkHingeR As System.Windows.Forms.CheckBox
  Friend WithEvents checkHingeL As System.Windows.Forms.CheckBox
  Friend WithEvents Label11 As System.Windows.Forms.Label
  Friend WithEvents txtLibPath As System.Windows.Forms.TextBox
  Friend WithEvents btnEditOpg As System.Windows.Forms.Button
  Friend WithEvents btnCustOpg As System.Windows.Forms.Button
  Friend WithEvents btnEditPart As System.Windows.Forms.Button
  Friend WithEvents btnNewCustPart As System.Windows.Forms.Button
  Friend WithEvents Panel1 As System.Windows.Forms.Panel
  Friend WithEvents Panel2 As System.Windows.Forms.Panel
  Friend WithEvents btnNextPage As System.Windows.Forms.Button
  Friend WithEvents btnTransNode As System.Windows.Forms.Button
  Friend WithEvents EditToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
  Friend WithEvents menuEditPrograms As System.Windows.Forms.ToolStripMenuItem
  Friend WithEvents toolStripSeparator3 As System.Windows.Forms.ToolStripSeparator
  Friend WithEvents CutToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
  Friend WithEvents CopyToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
  Friend WithEvents PasteToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
  Friend WithEvents toolStripSeparator4 As System.Windows.Forms.ToolStripSeparator
  Friend WithEvents SelectAllToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
  Friend WithEvents toolsToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
  Friend WithEvents menuPurgeOldData As System.Windows.Forms.ToolStripMenuItem
  Friend WithEvents menuOptions As System.Windows.Forms.ToolStripMenuItem
  Friend WithEvents menuEditSetupData As System.Windows.Forms.ToolStripMenuItem
  Friend WithEvents mnuUpdateFiles As System.Windows.Forms.ToolStripMenuItem
  Friend WithEvents mnuSelectJob As System.Windows.Forms.ToolStripMenuItem
  Friend WithEvents ToolStripMenuItem1 As System.Windows.Forms.ToolStripMenuItem
  Friend WithEvents mnuBarcodePartReport As System.Windows.Forms.ToolStripMenuItem
  Friend WithEvents mnuMateralRequirements As System.Windows.Forms.ToolStripMenuItem
  Friend WithEvents mnuPrintBarcodeReport As System.Windows.Forms.ToolStripMenuItem
  Friend WithEvents txtCabinetInfo As System.Windows.Forms.RichTextBox
  Friend WithEvents tpSelect As System.Windows.Forms.TabPage
  Friend WithEvents SplitContainer2 As System.Windows.Forms.SplitContainer
  Friend WithEvents TabControl2 As System.Windows.Forms.TabControl
  Friend WithEvents tpStyles As System.Windows.Forms.TabPage
  Friend WithEvents FlowLayoutPanel2 As System.Windows.Forms.FlowLayoutPanel
  Friend WithEvents groupFrameStyle As System.Windows.Forms.GroupBox
  Friend WithEvents Panel6 As System.Windows.Forms.Panel
  Friend WithEvents browserJobHeader As System.Windows.Forms.WebBrowser
  Friend WithEvents groupHingeStyle As System.Windows.Forms.GroupBox
  Friend WithEvents groupThickness As System.Windows.Forms.GroupBox
  Friend WithEvents radioUseExistingData As System.Windows.Forms.RadioButton
  Friend WithEvents radioImportData As System.Windows.Forms.RadioButton
  Friend WithEvents groupExistingData As System.Windows.Forms.GroupBox
  Friend WithEvents radioGetExistingData As System.Windows.Forms.RadioButton
  Friend WithEvents btnLoadJob As System.Windows.Forms.Button
  Friend WithEvents ContextMenuBlank As System.Windows.Forms.ContextMenuStrip
  Friend WithEvents toolBtnCabInfo As System.Windows.Forms.ToolStripButton
  Friend WithEvents toolBtnJobdetails As System.Windows.Forms.ToolStripButton
  Friend WithEvents ToolStripSeparator2 As System.Windows.Forms.ToolStripSeparator
  Friend WithEvents toolBtnCatalog As System.Windows.Forms.ToolStripButton
  Friend WithEvents toolBtnPrintBarcodeReport As System.Windows.Forms.ToolStripButton
  Friend WithEvents ToolStripSeparator5 As System.Windows.Forms.ToolStripSeparator
  Friend WithEvents toolBtnNavHome As System.Windows.Forms.ToolStripButton
  Friend WithEvents toolBtnNavBack As System.Windows.Forms.ToolStripButton
  Friend WithEvents toolBtnNavForward As System.Windows.Forms.ToolStripButton
  Friend WithEvents tpNewStart As System.Windows.Forms.TabPage
  Friend WithEvents SplitContainer3 As System.Windows.Forms.SplitContainer
  Friend WithEvents browserSchedule As System.Windows.Forms.WebBrowser
  Friend WithEvents pnlFilterBase As System.Windows.Forms.Panel
  Friend WithEvents pnlDateRangeFilter As System.Windows.Forms.Panel
  Friend WithEvents btnRefreshSchedule As System.Windows.Forms.Button
  Friend WithEvents Label15 As System.Windows.Forms.Label
  Friend WithEvents Label16 As System.Windows.Forms.Label
  Friend WithEvents datePickFilterEnd As System.Windows.Forms.DateTimePicker
  Friend WithEvents datePickFilterStart As System.Windows.Forms.DateTimePicker
  Friend WithEvents btnSelectJobNumber As System.Windows.Forms.Button
  Friend WithEvents Label17 As System.Windows.Forms.Label
  Friend WithEvents toolBtnPrintBrowser As System.Windows.Forms.ToolStripButton
  Friend WithEvents btnSaveProgList As System.Windows.Forms.Button
  Friend WithEvents btnImportItem As System.Windows.Forms.Button
  Friend WithEvents btnNewFrame As System.Windows.Forms.Button
  Friend WithEvents groupRadioCSV As System.Windows.Forms.GroupBox
  Friend WithEvents radioImportTemplateCSV As System.Windows.Forms.RadioButton
  Friend WithEvents radioImportCSV As System.Windows.Forms.RadioButton
  Friend WithEvents radioGenerateCSV As System.Windows.Forms.RadioButton
  Friend WithEvents mnuPrintBarcodes2 As System.Windows.Forms.ToolStripMenuItem
  Friend WithEvents pnlStyleLabel As System.Windows.Forms.Panel
  Friend WithEvents pnlSourceLabel As System.Windows.Forms.Panel
  Friend WithEvents Label14 As System.Windows.Forms.Label
  Friend WithEvents Label18 As System.Windows.Forms.Label

End Class
