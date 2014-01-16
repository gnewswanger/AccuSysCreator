Imports System
Imports System.Drawing
Imports System.IO
Imports System.ComponentModel
Imports System.Xml
Imports System.Data.OleDb
Imports System.Text.RegularExpressions
Imports System.Threading

''' <summary>
''' <file>File: MainForm.vb</file>
''' <author>Author: Galen Newswanger</author>
''' 
''' This class is a subclass of Windows Form and is the main UI for the AccuSysCreator2 
''' application. 
''' The FrameListClass contains a hierarchy of data objects that represent the items in 
''' the job's BOM (see comments in file FrameListClass.vb).
''' Execution for the applicaton starts in sub main() located in Module1.vb.
''' The logic in this file mostly manages the UI. The currOpeningList and currStileRailsList are 
''' used to keep track of movement of objects between listboxes on the tabpage tpPartsLayout.
''' </summary>
''' <remarks></remarks>
Public Class MainForm

    Public Delegate Sub MainFormShowingHandler(ByVal sender As Object, ByVal e As EventArgs)
    Public Shared Event MainFormShowing As MainFormShowingHandler

#Region "Class MainForm Instance Variables ====="

    Friend WithEvents Abcnet282 As Abcnet28Lib.Abcnet28

    Public _framesDb As DataClass
    Public frames As FrameListClass

    Private _jobNumber As String    'FrameListClass has it's own variable for job number. be certain to sync the two when changing jobs.
    Private _jobFileName As String

    Private _currOpeningList As New Generic.List(Of FramePart)
    Private _currStilesRailsList As New Generic.List(Of FramePart)

    Private _myPages As New List(Of TabPage)
    Private _progViewChanged As Boolean
    Private _BOM_Exists As Boolean
    Private _dragBoxFromMouseDown As Rectangle
    Private _tvNodeToDrag As TreeNode
    Private Const CSC_NAVIGATEFORWARD As Integer = 1
    Private Const CSC_NAVIGATEBACK As Integer = 2
#End Region 'Class MainForm Instance Variables

#Region "MainForm Class Properties"

    Public WriteOnly Property StyleRadioButtonGroup() As System.Collections.Generic.List(Of StyleClassMetaData)
        Set(ByVal value As System.Collections.Generic.List(Of StyleClassMetaData))
            Me.SetStyleRadioButtons(value)
        End Set
    End Property

    Public Property SourceOfJobData() As SourceOfData
        Get
            If Me.radioUseExistingData.Checked Then
                Return SourceOfData.dsExistingData
            ElseIf Me.radioImportData.Checked And Me.radioGenerateCSV.Checked Then
                Return SourceOfData.dsNewCSV
            ElseIf Me.radioImportData.Checked And Me.radioImportCSV.Checked Then
                Return SourceOfData.dsExistingCSV
            ElseIf Me.radioImportData.Checked And Me.radioImportTemplateCSV.Checked Then
                Return SourceOfData.dsTemplateCSV
            Else
                Return SourceOfData.dsNone
            End If
        End Get
        Set(ByVal value As SourceOfData)
            If value = SourceOfData.dsExistingData Then
                Me.radioUseExistingData.Checked = True
                Me.radioGetExistingData.Checked = True
                Me.radioGenerateCSV.Checked = True
            ElseIf value = SourceOfData.dsNewCSV Then
                Me.radioImportData.Checked = True
                Me.radioGenerateCSV.Checked = True
            ElseIf value = SourceOfData.dsExistingCSV Then
                Me.radioImportData.Checked = True
                Me.radioImportCSV.Checked = True
            ElseIf value = SourceOfData.dsTemplateCSV Then
                Me.radioImportData.Checked = True
                Me.radioImportTemplateCSV.Checked = True
            End If
        End Set
    End Property

#End Region 'MainForm Class Properties

#Region "Construction and Initialization Methods"

    Public Sub New()

        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        Me.Abcnet282 = New Abcnet28Lib.Abcnet28(Me.components)
        '
        'Abcnet282 barcode component (DLSoft Abcnet28Lib.dll). This prints code 128 barcodes.
        '
        Me.Abcnet282.AutoCheckdigit = False
        Me.Abcnet282.BackColor = System.Drawing.Color.White
        Me.Abcnet282.BarcodeHeight = 16.0!
        Me.Abcnet282.BarcodeWidth = 36.0!
        Me.Abcnet282.BarRatio = 2.5!
        Me.Abcnet282.BearerSize = 0.0!
        Me.Abcnet282.BorderWidth = 0.0!
        Me.Abcnet282.BothBearers = False
        Me.Abcnet282.Caption = "1234"
        Me.Abcnet282.CodeType = Abcnet28Lib.Abcnet28.bCode.Code_128
        Me.Abcnet282.ErrorCode = 0
        Me.Abcnet282.ExtendBearers = False
        Me.Abcnet282.Extra1 = False
        Me.Abcnet282.Extra2 = False
        Me.Abcnet282.Font = New System.Drawing.Font("Arial", 10.0!)
        Me.Abcnet282.ForeColor = System.Drawing.Color.Black
        Me.Abcnet282.ImageHeight = 16.0!
        Me.Abcnet282.ImageWidth = 36.0!
        Me.Abcnet282.Indicators = False
        Me.Abcnet282.MarginSize = 0.0!
        Me.Abcnet282.Orientation = 0
        Me.Abcnet282.Reduction = 0
        Me.Abcnet282.ShowCheckdigit = False
        Me.Abcnet282.ShowText = True
        Me.Abcnet282.Status = ""
        Me.Abcnet282.String2 = ""
        Me.Abcnet282.TextAlign = System.Drawing.StringAlignment.Center
        Me.Abcnet282.Unit = System.Drawing.GraphicsUnit.Millimeter
        Me.Abcnet282.Xunit = 0.0!

    End Sub

    Private Sub MainForm_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'The following line is required to be called before any calls to the barcode component are made (other than the above initialization).
        Abcnet282.SerialNo = "A2D37914562CS1"
        Me._framesDb = New DataClass(ParmFilename)

        For Each tp As TabPage In TabControl1.TabPages
            Me._myPages.Add(tp)
        Next
        Me.InitializeTabPages2Start()
        TabControl1.SelectedIndex = 0
        Me.datePickFilterStart.Value = Today.AddDays(Module1.SchedSearchStartOffset)
        Me.datePickFilterEnd.Value = Today.AddDays(Module1.SchedSearchEndOffset)
        Me.GetSchedule()
        Me.comboPartType.DataSource = System.Enum.GetValues(GetType(FrontFrameEventClasses.PartEdgeTypes))
        sbPanelVersion.Text = GetVersionString()
        sbPanelVersion.ToolTipText = sbPanelVersion.Text
        SetVisibleControls()
    End Sub

    Private Sub InitializeTabPages2Start()
        For Each tp As TabPage In TabControl1.TabPages
            'If Not tp Is tpStart Then
            If Not tp Is tpNewStart Then
                TabControl1.Controls.Remove(tp)
            End If
        Next
        TabControl1.SelectedIndex = 0
    End Sub

#End Region 'Costruction and Initialization Methods

#Region "Set control visibility and enabling"

    Private Sub SetVisibleControls()
        Me.mnuMateralRequirements.Visible = (Not Me.frames Is Nothing) AndAlso (Me.frames.JobNumber.Length = 9)
        Me.comboItems.Visible = TabControl1.SelectedTab Is tpPartsLayout
        Me.lblSelectItem.Visible = Me.comboItems.Visible
        Me.toolBtnJobdetails.Visible = (Not Me._jobNumber Is Nothing) AndAlso (Me._jobNumber.Length = 9) _
                        AndAlso (Not TabControl1.SelectedTab Is tpJobDetail)
        Me.toolBtnCatalog.Visible = Not TabControl1.SelectedTab Is tpCatalog
        Dim browserTabIsActive As Boolean = Me.TabControl1.SelectedTab Is tpJobDetail _
          Or Me.TabControl1.SelectedTab Is tpCatalog _
          Or Me.TabControl1.SelectedTab Is tpNewStart _
          Or Me.TabControl1.SelectedTab Is tpSelect
        Me.toolBtnNavHome.Visible = browserTabIsActive
        Me.toolBtnNavBack.Visible = browserTabIsActive
        Me.toolBtnNavForward.Visible = browserTabIsActive
        Me.toolBtnPrintBrowser.Visible = browserTabIsActive
        Me.toolBtnCabInfo.Visible = TabControl1.SelectedTab Is tpPartsLayout AndAlso (Not Me._jobNumber Is Nothing) AndAlso (Me._jobNumber.Length = 9)
        Me.pnlCabinetInfo.Visible = (Me.toolBtnCabInfo.Visible And toolBtnCabInfo.Checked)
        Me.mnuPrintBarcodeReport.Visible = TabControl1.SelectedTab Is tpPartsLayout Or TabControl1.SelectedTab Is tpProgList
        Me.toolBtnPrintBarcodeReport.Visible = TabControl1.SelectedTab Is tpPartsLayout Or TabControl1.SelectedTab Is tpProgList
    End Sub

    Private Sub SetRadioGetJobDataEnabled(ByVal dbExists As Boolean, ByVal csvExists As Boolean)
        If dbExists Then
            Me.groupExistingData.Enabled = True
            Me.radioUseExistingData.Enabled = True
            Me.radioGetExistingData.Enabled = True
        Else
            Me.groupExistingData.Enabled = False
            Me.radioUseExistingData.Enabled = False
        End If
        If csvExists Then
            Me.radioImportCSV.Enabled = True
            Me.radioGenerateCSV.Enabled = True
        Else
            Me.radioImportCSV.Enabled = False
        End If
        Me.radioGenerateCSV.Enabled = Me._BOM_Exists
        Me.radioImportTemplateCSV.Enabled = Not Me._BOM_Exists
    End Sub

#End Region 'Set control visibiltity and enabling

#Region "New Set Styles Methods"
    Private Sub SetStyleRadioButtons(ByVal args As List(Of StyleClassMetaData))
        Me.SuspendLayout()
        Me.groupFrameStyle.Controls.Clear()
        Me.groupHingeStyle.Controls.Clear()
        Me.groupThickness.Controls.Clear()
        For Each item As StyleClassMetaData In args
            Dim rBtn As New RadioButton()
            rBtn.Name = item.StyleName
            rBtn.Text = item.StyleDesc
            Select Case item.StyleGroup
                Case StyleTypes.rgFrameEdgeStyle
                    rBtn.Width = groupFrameStyle.Width - 20
                    rBtn.Location = New Point(12, 15 + groupFrameStyle.Controls.Count * 24)
                    AddHandler rBtn.CheckedChanged, AddressOf radioGroupFrameStyle_CheckedChanged
                    If groupFrameStyle.Controls.Count = 0 Then
                        rBtn.Checked = True
                    End If
                    groupFrameStyle.Controls.Add(rBtn)
                    groupFrameStyle.Height = rBtn.Location.Y + 32
                Case StyleTypes.rgHingeStyle
                    rBtn.Width = groupHingeStyle.Width - 20
                    rBtn.Location = New Point(12, 15 + groupHingeStyle.Controls.Count * 24)
                    AddHandler rBtn.CheckedChanged, AddressOf radioGroupHingeStyle_CheckedChanged
                    If groupHingeStyle.Controls.Count = 0 Then
                        rBtn.Checked = True
                    End If
                    groupHingeStyle.Controls.Add(rBtn)
                    groupHingeStyle.Height = rBtn.Location.Y + 32
                Case StyleTypes.rgThickStyle
                    rBtn.Width = groupThickness.Width - 20
                    rBtn.Location = New Point(12, 15 + groupThickness.Controls.Count * 24)
                    AddHandler rBtn.CheckedChanged, AddressOf radioGroupThicknessStyle_CheckedChanged
                    If groupThickness.Controls.Count = 0 Then
                        rBtn.Checked = True
                    End If
                    groupThickness.Controls.Add(rBtn)
                    groupThickness.Height = rBtn.Location.Y + 32
            End Select
        Next
        If groupFrameStyle.Controls.Count > 0 Then
            CType(groupFrameStyle.Controls(0), RadioButton).Checked = True
        End If
        If groupThickness.Controls.Count > 0 Then
            CType(groupThickness.Controls(0), RadioButton).Checked = True
        End If
        If groupHingeStyle.Controls.Count > 0 Then
            CType(groupHingeStyle.Controls(0), RadioButton).Checked = True
        End If
        Me.ResumeLayout(False)
    End Sub

    Private Function SelectedStyleRadioButtons() As List(Of StyleClassMetaData)
        Dim retVal As New List(Of StyleClassMetaData)
        retVal.Add(SelectedFrameStyleButton)
        retVal.Add(SelectedHingeStyleButton)
        retVal.Add(SelectedThickStyleButton)
        Return retVal
    End Function

    Private Function SelectedFrameStyleButton() As StyleClassMetaData
        Dim evStyle As New StyleClassMetaData
        For Each ctl As Control In Me.groupFrameStyle.Controls
            If TypeOf ctl Is RadioButton Then
                If CType(ctl, RadioButton).Checked Then
                    evStyle.StyleName = CType(ctl, RadioButton).Name
                    evStyle.StyleDesc = CType(ctl, RadioButton).Text
                    evStyle.StyleGroup = StyleTypes.rgFrameEdgeStyle
                    evStyle.StyleSelected = CType(ctl, RadioButton).Checked
                    Return evStyle
                End If
            End If
        Next
        Return Nothing
    End Function

    Private Function SelectedHingeStyleButton() As StyleClassMetaData
        Dim evStyle As New StyleClassMetaData
        For Each ctl As Control In Me.groupHingeStyle.Controls
            If TypeOf ctl Is RadioButton Then
                If CType(ctl, RadioButton).Checked Then
                    evStyle.StyleName = CType(ctl, RadioButton).Name
                    evStyle.StyleDesc = CType(ctl, RadioButton).Text
                    evStyle.StyleGroup = StyleTypes.rgHingeStyle
                    evStyle.StyleSelected = CType(ctl, RadioButton).Checked
                    Return evStyle
                End If
            End If
        Next
        Return Nothing
    End Function

    Private Function SelectedThickStyleButton() As StyleClassMetaData
        Dim evStyle As New StyleClassMetaData
        For Each ctl As Control In Me.groupThickness.Controls
            If TypeOf ctl Is RadioButton Then
                If CType(ctl, RadioButton).Checked Then
                    evStyle.StyleName = CType(ctl, RadioButton).Name
                    evStyle.StyleDesc = CType(ctl, RadioButton).Text
                    evStyle.StyleGroup = StyleTypes.rgThickStyle
                    evStyle.StyleSelected = CType(ctl, RadioButton).Checked
                    Return evStyle
                End If
            End If
        Next
        Return Nothing
    End Function

    Private Sub SetSelectedStyleRadioButtons(ByVal list As List(Of StyleClassMetaData))
        Me.SuspendLayout()
        For Each item As StyleClassMetaData In list
            Select Case item.StyleGroup
                Case StyleTypes.rgFrameEdgeStyle
                    For Each ctl As Control In Me.groupFrameStyle.Controls
                        If TypeOf ctl Is RadioButton AndAlso ctl.Name = item.StyleName Then
                            CType(ctl, RadioButton).Checked = item.StyleSelected
                        End If
                    Next
                Case StyleTypes.rgHingeStyle
                    For Each ctl As Control In Me.groupHingeStyle.Controls
                        If TypeOf ctl Is RadioButton AndAlso ctl.Name = item.StyleName Then
                            CType(ctl, RadioButton).Checked = item.StyleSelected
                        End If
                    Next
                Case StyleTypes.rgThickStyle
                    For Each ctl As Control In Me.groupThickness.Controls
                        If TypeOf ctl Is RadioButton AndAlso ctl.Name = item.StyleName Then
                            CType(ctl, RadioButton).Checked = item.StyleSelected
                        End If
                    Next
            End Select
        Next
        Me.ResumeLayout(False)
    End Sub

    Private Function GetHingeStylename() As String
        Dim ctl As Control
        Dim style As String = String.Empty
        For Each ctl In Me.groupHingeStyle.Controls
            If TypeOf ctl Is RadioButton Then
                If CType(ctl, RadioButton).Checked Then
                    style = CType(ctl, RadioButton).Name
                End If
            End If
        Next
        Return style
    End Function

    Private Function GetFrameStylename() As String
        Dim ctl As Control
        Dim style As String = String.Empty
        For Each ctl In Me.groupFrameStyle.Controls
            If TypeOf ctl Is RadioButton Then
                If CType(ctl, RadioButton).Checked Then
                    style = CType(ctl, RadioButton).Name
                End If
            End If
        Next
        Return style
    End Function

    Private Function GetThickStylename() As String
        Dim ctl As Control
        Dim style As String = String.Empty
        For Each ctl In Me.groupThickness.Controls
            If TypeOf ctl Is RadioButton Then
                If CType(ctl, RadioButton).Checked Then
                    style = CType(ctl, RadioButton).Name
                End If
            End If
        Next
        Return style
    End Function

    Private Sub radioGroupFrameStyle_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Module1.JobFrameStyle = New FrameStyle(Trim(GetFrameStylename), ParmFilename)
    End Sub

    Private Sub radioGroupThicknessStyle_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Module1.JobThickness = New Thickness(Trim(GetThickStylename), ParmFilename)
    End Sub

    Private Sub radioGroupHingeStyle_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Module1.JobHingeStyle = New Hinge(Trim(GetHingeStylename), ParmFilename)
    End Sub

    Private Sub pnlStyleLabel_Paint(ByVal sender As System.Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles pnlStyleLabel.Paint
        Dim font As New Font(Me.Font.FontFamily, 11.0, FontStyle.Regular, GraphicsUnit.Point)
        Dim mytext As String = "Select Job Style Settings"
        Dim stringSize As New SizeF
        stringSize = e.Graphics.MeasureString(mytext, font)
        e.Graphics.TranslateTransform(5, stringSize.Width + 15)
        e.Graphics.RotateTransform(270)
        e.Graphics.DrawString(mytext, font, New SolidBrush(Color.Black), 0, 0)
    End Sub

    Private Sub pnlSourceLabel_Paint(ByVal sender As Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles pnlSourceLabel.Paint
        Dim font As New Font(Me.Font.FontFamily, 11.0, FontStyle.Regular, GraphicsUnit.Point)
        Dim mytext As String = "Select Source for Job Data"
        Dim stringSize As New SizeF
        stringSize = e.Graphics.MeasureString(mytext, font)
        e.Graphics.TranslateTransform(5, stringSize.Width + 15)
        e.Graphics.RotateTransform(270)
        e.Graphics.DrawString(mytext, font, New SolidBrush(Color.Black), 0, 0)
    End Sub

#End Region  'Set Styles Methods

#Region "UI Data Changed Events ======"

    Private Sub comboItems_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles comboItems.SelectedIndexChanged
        Me.FramePartImagePanel1.ClearDisplay()
        If Not IsNothing(Me.frames) Then
            Me.frames.SetCurrentFrameByItem(CStr(comboItems.SelectedItem))
            'txtCabinetInfo.Text = "Frame Size: " + Me.frames.CurrentFrame.FrontframeSize.ToString _
            '    + vbCr + "Hinging: " + vbCr + "Cabinet type: "
            GetFrameSketch()
        End If
        Me.ResetPartLayoutForm()
        Me.btnImportItem.Enabled = Me.comboItems.SelectedIndex > -1
        Me.btnNewCustPart.Enabled = Me.comboItems.SelectedIndex > -1
        Me.btnCustOpg.Enabled = Me.comboItems.SelectedIndex > -1
    End Sub
    Private Sub GetFrameSketch()
        '"W:\2006\*F.dwf"
        Dim yr As Integer = Now.Year
        Dim fInfo As New FileInfo(Me._jobFileName)
        yr = fInfo.CreationTime.Year()
        If File.Exists(Module1.DwfSourceDirectory + yr.ToString + "\" + Me._jobNumber + CStr(comboItems.SelectedItem) + "F.dwf") Then

        ElseIf File.Exists(Module1.DwfSourceDirectory + (yr - 1).ToString + "\" + Me._jobNumber + CStr(comboItems.SelectedItem) + "F.dwf") Then
            yr -= 1
        ElseIf File.Exists(Module1.DwfSourceDirectory + (yr + 1).ToString + "\" + Me._jobNumber + CStr(comboItems.SelectedItem) + "F.dwf") Then
            yr += 1
        Else
            yr = 0
        End If
        If (yr > 0) Then
            'AxWebBrowser1.Navigate2("http://qccnt1/webdrawings/" + Me._jobNumber + CStr(comboItems.SelectedItem) + "F.dwf?papervisible=false")
            'AxWebBrowser1.Navigate2("http://qccnt1/qccasp/cad.asp?lcname=21352000013F.DWF&mjobno=213520000&mitem=13&mjcustno=0005&mtime=current&myear=2009")

            'http://qccnt1/qccasp/cad.asp?lcname=21352000013F.DWF&mjobno=213520000&mitem=13&mjcustno=0005&mtime=current&myear=2009
            'AxCExpressViewerControl1.SourcePath = Module1.DwfSourceDirectory + yr.ToString + "\" + Me._jobNumber + comboItems.SelectedItem + "F.dwf"
            toolBtnCabInfo.ImageIndex = 0
        Else
            'AxCExpressViewerControl1.SourcePath = Me._dwfNotFoundFile
            toolBtnCabInfo.ImageIndex = -1
        End If
    End Sub

    Private Sub TabControl1_Deselected(ByVal sender As Object, ByVal e As System.Windows.Forms.TabControlEventArgs) Handles TabControl1.Deselected
        If e.TabPage Is tpProgList Then
            Me.WriteProgList2Xml()
        End If
    End Sub

    Private Sub TabControl1_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TabControl1.SelectedIndexChanged
        If Me._progViewChanged Then
            cbxPartName.Items.Clear()
            cbxPartName.Text = ""
            For i As Integer = 0 To tvProgList.GetNodeCount(False) - 1
                If Not Me.ListboxSavedfilesContains(tvProgList.Nodes(i).Text.Replace(CChar("."), "_")) Then
                    cbxPartName.Items.Add(Trim(tvProgList.Nodes(i).Text))
                End If
            Next
            'WriteProgList2Xml()
            Me._progViewChanged = Not UpdateProgramNames()
        End If
        If TabControl1.SelectedTab Is tpProgList Then
            Me.sbPanelMessage.Text = "Style: " & Me.GetFrameStylename & "    Hinge: " & Me.GetHingeStylename _
                                    & "    Thickness: " & Me.GetThickStylename

        ElseIf TabControl1.SelectedTab Is tpNewStart Then
            If Me.browserSchedule.Url Is Nothing Then
                Me.datePickFilterStart.Value = Today.AddDays(Module1.SchedSearchStartOffset)
                Me.datePickFilterEnd.Value = Today.AddDays(Module1.SchedSearchEndOffset)
                Me.GetSchedule()
            End If
            Me.browser_CanGoChanged(Me.browserSchedule, e)
        ElseIf TabControl1.SelectedTab Is tpSelect Then
            Me.browser_CanGoChanged(Me.browserJobHeader, e)
        ElseIf TabControl1.SelectedTab Is tpPartsLayout Then
            Me.sbPanelMessage.Text = "Style: " & Me.GetFrameStylename & "    Hinge: " & Me.GetHingeStylename _
                                    & "    Thickness: " & Me.GetThickStylename
        ElseIf TabControl1.SelectedTab Is tpJobDetail Then
            Me.browser_CanGoChanged(Me.browserJobDetail, e)
        ElseIf TabControl1.SelectedTab Is tpCatalog Then
            Me.browser_CanGoChanged(Me.browserCatalog, e)
        End If
        SetVisibleControls()
    End Sub

    Private Function ListboxSavedfilesContains(ByVal name As String) As Boolean
        Dim retVal As Boolean = False
        For Each item As String In Me.lbSavedFiles.Items
            If item.Split(CChar("."))(0) = name.Trim Then
                retVal = True
            End If
        Next
        Return retVal
    End Function

    Private Sub txtPartCode_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtPartCode.TextChanged
        If txtPartCode.Text <> "" Then
            Select Case txtPartCode.Text.Substring(0, 2)
                Case "SL"
                    comboPartType.SelectedIndex = comboPartType.Items.IndexOf(FrontFrameEventClasses.PartEdgeTypes.StileLeft)
                Case "SR"
                    comboPartType.SelectedIndex = comboPartType.Items.IndexOf(FrontFrameEventClasses.PartEdgeTypes.StileRight)
                Case "SC"
                    If cbxPartName.Text.EndsWith("R") Then
                        comboPartType.SelectedIndex = comboPartType.Items.IndexOf(FrontFrameEventClasses.PartEdgeTypes.StileCenterR)
                    Else
                        comboPartType.SelectedIndex = comboPartType.Items.IndexOf(FrontFrameEventClasses.PartEdgeTypes.StileCenterL)
                    End If
                Case "RT"
                    comboPartType.SelectedIndex = comboPartType.Items.IndexOf(FrontFrameEventClasses.PartEdgeTypes.TopRail)
                Case "RB"
                    comboPartType.SelectedIndex = comboPartType.Items.IndexOf(FrontFrameEventClasses.PartEdgeTypes.BotRail)
                Case "RC"
                    If cbxPartName.Text.EndsWith("B") Then
                        comboPartType.SelectedIndex = comboPartType.Items.IndexOf(FrontFrameEventClasses.PartEdgeTypes.RailCenterB)
                    Else
                        comboPartType.SelectedIndex = comboPartType.Items.IndexOf(FrontFrameEventClasses.PartEdgeTypes.RailCenterT)
                    End If
                Case "GI"
                    comboPartType.SelectedIndex = comboPartType.Items.IndexOf(FrontFrameEventClasses.PartEdgeTypes.GlueInStrip)
                Case Else
                    comboPartType.SelectedIndex = comboPartType.Items.IndexOf(FrontFrameEventClasses.PartEdgeTypes.Opening)
            End Select
            Me.FramePartImagePanel1.DisplayGraphic(Me.frames.CurrentPart.EndTenonArgs)
        End If
    End Sub

    Private Sub txtPartName_DragEnter(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DragEventArgs) Handles txtPartName.DragEnter
        If e.Data.GetDataPresent(GetType(FramePart)) Then
            e.Effect = DragDropEffects.Copy Or DragDropEffects.Move
        End If
    End Sub

    Private Sub txtPartName_DragDrop(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DragEventArgs) Handles txtPartName.DragDrop
        If e.Data.GetDataPresent(GetType(FramePart)) Then
            Dim dragobj As New FramePart(CType(e.Data.GetData(GetType(FramePart)), FramePart))
            Me.frames.SetCurrentPartByName(dragobj.Name)
        End If
        txtPartWdth.Text = Me.frames.CurrentPart.Width.ToString
        txtPartHght.Text = Me.frames.CurrentPart.Length.ToString
        txtPartThick.Text = Me.frames.CurrentPart.Thickness.ToString
        txtPartCode.Text = Me.frames.CurrentPart.Code
        txtPartItem.Text = Me.frames.CurrentPart.ItemNo
        Dim txt As String = Me.frames.CurrentPart.Name
        txt = txt.Substring(txt.IndexOf(".") + 1)
        txtPartName.Text = txt
        cbxPartName.Text = ""
        ClearPartOperations()
        Me.FramePartImagePanel1.ClearDisplay()
        RefreshPartsLists()
        Me.frames.CurrentPart.SetOperations(Me.frames.CurrentFrame.CabinetType, Me.frames.CurrentPart.PartEdges(0).PeType)
        Me.FramePartImagePanel1.DisplayGraphic(Me.frames.CurrentPart.EndTenonArgs, True)
    End Sub

    Private Sub txtPartName_MouseMove(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles txtPartName.MouseMove
        If Me.txtPartName.Text <> "" Then
            Dim fp As FramePart = Me.frames.GetSinglePartByPartedgename(Me._jobNumber & "." & Me.txtPartName.Text.Trim)
            If Not fp Is Nothing Then
                Dim tipText As String = fp.PartEdges(0).Comment
                If fp.TSComments <> "" Then
                    tipText += " | TS Comment: " & fp.TSComments
                End If
                Me.ToolTip1.SetToolTip(Me.txtPartName, tipText)
            End If
        Else
            Me.ToolTip1.SetToolTip(Me.txtPartName, "")
        End If
    End Sub

    Private Sub txtPartName_MouseUp(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles txtPartName.MouseUp
        If e.Button = MouseButtons.Right Then
            If Me.txtPartName.Text <> "" Then
                Dim fp As FramePart = Me.frames.GetSinglePartByPartedgename(Me._jobNumber & "." & Me.txtPartName.Text.Trim)
                If Not fp Is Nothing Then
                    Dim dlg As New UserInputBox(0)
                    Me.ToolTip1.Hide(CType(sender, Control))
                    dlg.Text = "Add comment for " & fp.Name
                    dlg.Prompt = "Enter or modify " & fp.Name & " comments:"
                    dlg.txtJobNumber.Text = fp.PartEdges(0).Comment
                    If dlg.ShowDialog() = DialogResult.OK Then
                        fp.PartEdges(0).Comment = dlg.txtJobNumber.Text
                    End If
                End If
            End If
        End If
    End Sub

    Private Sub btnNextPage_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNextPage.Click
        If TabControl1.TabPages.Contains(tpPartsLayout) Then
            TabControl1.SelectedTab = tpPartsLayout
        End If
    End Sub

    Private Sub SetTabControlPages(Optional ByVal gotoPage As Boolean = True)
        If TabControl1.TabPages.IndexOf(tpSelect) = -1 Then
            TabControl1.TabPages.Add(Me._myPages(Me._myPages.IndexOf(tpSelect)))
        End If
        If TabControl1.TabPages.IndexOf(tpProgList) = -1 Then
            TabControl1.TabPages.Add(Me._myPages(Me._myPages.IndexOf(tpProgList)))
        End If
        If TabControl1.TabPages.IndexOf(tpPartsLayout) = -1 Then
            TabControl1.TabPages.Add(Me._myPages(Me._myPages.IndexOf(tpPartsLayout)))
        End If
        'If TabControl1.TabPages.IndexOf(tpNewStart) = -1 Then
        '  TabControl1.TabPages.Add(Me._myPages(Me._myPages.IndexOf(tpNewStart)))
        'End If
        If gotoPage Then
            TabControl1.SelectedIndex = TabControl1.TabPages.IndexOf(tpProgList)
        End If
    End Sub

#End Region 'UI Data Changed Events

#Region "Clear Methods"

    Private Sub ClearFramelist()
        Me.frames.ClearFrameList()
        comboItems.Items.Clear()
        Me.labelStatusJobNo.Text = "No Job Selected! "
    End Sub

    Private Sub btnClearOperations_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClearOperations.Click
        If Me.ClearPartOperations() Then
            Me.FramePartImagePanel1.ClearDisplay()
        End If
    End Sub

    Private Sub btnPartsClearForm_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPartsClearForm.Click
        Me.ResetPartLayoutForm()
    End Sub
    Private Function ResetPartLayoutForm() As Boolean
        Me.txtPartName.Text = ""
        Me.txtPartCode.Text = ""
        Me.txtPartWdth.Text = ""
        Me.txtPartHght.Text = ""
        Me.txtPartThick.Text = ""
        Me.txtPartItem.Text = ""
        Me.FramePartImagePanel1.ClearDisplay()
        Me.lbAdjoinParts.Items.Clear()
        Me.loadFramePartsLists()
        Return True
    End Function

    Private Sub ResetPartForm()
        txtPartName.Text = ""
        If txtPartCode.Text <> "" Then
            txtPartCode.Text = ""
        End If
        txtPartWdth.Text = ""
        txtPartHght.Text = ""
        txtPartThick.Text = ""
        txtPartItem.Text = ""
        Me._currOpeningList.Clear()
        Me.FramePartImagePanel1.ClearDisplay()
        UpdateOpeningPartListbox()
        UpdateAdjoinPartsList(Nothing)
        If Not IsNothing(Me.frames) Then
            Me.frames.CurrentFrameIndex = comboItems.SelectedIndex
            Me.frames.CurrentPartIndex = 0
        End If
    End Sub

    Private Function ClearPartOperations() As Boolean
        If Me.frames.CurrentFrameIndex > -1 AndAlso Me.frames.CurrentPartIndex > -1 Then
            Dim fp As FramePart = CType(Me.frames.CurrentPart, FramePart)
            If Not IsNothing(fp) Then
                'TODO:
                fp.ClearOperations(CType(System.Enum.Parse(GetType(FrontFrameEventClasses.PartEdgeTypes), CStr(comboPartType.SelectedItem)), FrontFrameEventClasses.PartEdgeTypes))
                Return True
            End If
        Else
            Return False
        End If
    End Function

    Private Function ClearFramePartAssoc() As Boolean
        If Me.frames.CurrentPartIndex > -1 Then
            If MessageBox.Show("This will remove all part associations from the current part.  Do you want to continue?", "Delete Adjoining Parts", MessageBoxButtons.OKCancel) = DialogResult.OK Then
                If Me.frames.CurrentFrameIndex > -1 AndAlso Me.frames.CurrentPartIndex > -1 Then
                    Dim fp As FramePart = CType(Me.frames.CurrentPart, FramePart)
                    If Not IsNothing(fp) Then
                        fp.ClearAll(CType(System.Enum.Parse(GetType(FrontFrameEventClasses.PartEdgeTypes), CStr(comboPartType.SelectedItem)), FrontFrameEventClasses.PartEdgeTypes))
                        Return True
                    End If
                Else
                    Return False
                End If
            End If
        End If
    End Function
#End Region 'Clear Methods

#Region "Update Listboxes ======"

    Private Sub loadFramePartsLists()

        LoadStilesRailsList()
        LoadOpeningPartList()
    End Sub

    Private Sub RefreshPartsLists()
        If Me.frames.CurrentFrameIndex > -1 Then
            If Me.frames.CurrentPartIndex > -1 Then
                ''Dim pt As FrontFrameEventClasses.PartEdgeTypes
                'UpdateAdjoinPartsList(Me.frames.CurrentPart.AdjoiningPartNameList(CType([Enum].Parse(GetType(FrontFrameEventClasses.PartEdgeTypes), comboPartType.SelectedItem), FrontFrameEventClasses.PartEdgeTypes)))
                UpdateAdjoinPartsList(Me.frames.CurrentPart.AdjoiningPartNameList(CType(comboPartType.SelectedItem, FrontFrameEventClasses.PartEdgeTypes)))
                Dim opList As New Generic.List(Of FramePart)
                opList.AddRange(Me.frames.CurrentFrame.Openinglist.ToArray)
                For i As Integer = 0 To lbAdjoinParts.Items.Count - 1
                    For j As Integer = opList.Count - 1 To 0 Step -1
                        If Me.frames.GetSinglePartByDescription(CStr(lbAdjoinParts.Items(i))).Name = CType(opList(j), FramePart).Name Then
                            opList.RemoveAt(j)
                            Exit For
                        End If
                    Next
                Next
                Me._currOpeningList.Clear()
                Me._currOpeningList.AddRange(opList.ToArray)
                Dim ptList As New Generic.List(Of FramePart)
                ptList.AddRange(Me.frames.CurrentFrame.PartsList.ToArray)
                ptList.RemoveAt(Me.frames.CurrentPartIndex)
                For i As Integer = 0 To lbAdjoinParts.Items.Count - 1
                    For j As Integer = ptList.Count - 1 To 0 Step -1
                        If (Me.frames.GetSinglePartByDescription(CStr(lbAdjoinParts.Items(i))).Name = CType(ptList(j), FramePart).Name) Then
                            ptList.RemoveAt(j)
                            Exit For
                        End If
                    Next
                Next
                Me._currStilesRailsList.Clear()
                Me._currStilesRailsList.AddRange(ptList)
                UpdateOpeningPartListbox()
                UpdateStilesRailsListbox()
            Else
                loadFramePartsLists()
            End If
        End If
    End Sub

    Private Sub UpdateAdjoinPartsList(ByVal items As ArrayList)
        lbAdjoinParts.Items.Clear()
        'lbAdjoinParts.DisplayMember = "Description"
        If Not items Is Nothing Then
            For i As Integer = 0 To items.Count - 1
                Dim str As String = Me.frames.GetSinglePart(CStr(items(i))).Description
                lbAdjoinParts.Items.Add(str)
            Next
        End If
        btnUp.Enabled = lbAdjoinParts.SelectedIndex > 0 And lbAdjoinParts.Items.Count > 1
        btnDown.Enabled = lbAdjoinParts.SelectedIndex > -1 And lbAdjoinParts.SelectedIndex < lbAdjoinParts.Items.Count - 1 And lbAdjoinParts.Items.Count > 1
        btnRemove.Enabled = lbAdjoinParts.SelectedIndex > -1
    End Sub

    Private Sub LoadOpeningPartList()
        Me._currOpeningList.Clear()
        If comboItems.SelectedIndex > -1 AndAlso Me.frames.CurrentFrameIndex > -1 Then
            For Each item As FramePart In Me.frames.CurrentFrame.Openinglist
                Me._currOpeningList.Add(item)
            Next
        End If
        UpdateOpeningPartListbox()
    End Sub

    Private Sub UpdateOpeningPartListbox()
        Dim itemIndex As Integer = lbOpenings.SelectedIndex
        lbOpenings.Items.Clear()
        lbOpenings.ValueMember = "Description"

        lbOpenings.Items.AddRange(Me._currOpeningList.ToArray)

        If itemIndex = -1 Then
            If lbOpenings.Items.Count = 0 Then
                lbOpenings.SelectedIndex = -1
            Else
                lbOpenings.SelectedIndex = 0
            End If
        Else
            If lbOpenings.Items.Count > itemIndex Then
                lbOpenings.SelectedIndex = itemIndex
            Else
                lbOpenings.SelectedIndex = -1
            End If
        End If
        lbOpenings_SelectedIndexChanged(Me, Nothing)
        btnEditOpg.Enabled = lbOpenings.SelectedIndex > -1
        btnTransAdjoinOpg.Enabled = (Not IsNothing(Me.frames)) AndAlso Me.frames.CurrentPartIndex > -1
    End Sub

    Private Sub LoadStilesRailsList()
        Me._currStilesRailsList.Clear()
        If comboItems.SelectedIndex > -1 AndAlso Me.frames.CurrentFrameIndex > -1 Then
            For Each item As FramePart In Me.frames.CurrentFrame.PartsList
                Me._currStilesRailsList.Add(item)
            Next
        End If
        UpdateStilesRailsListbox()
    End Sub

    Private Sub UpdateStilesRailsListbox()
        Dim itemIndex As Integer = lbStilesRails.SelectedIndex
        lbStilesRails.Items.Clear()
        lbStilesRails.ValueMember = "Description"
        lbStilesRails.Items.AddRange(Me._currStilesRailsList.ToArray)
        If itemIndex = -1 Then
            If lbStilesRails.Items.Count = 0 Then
                lbStilesRails.SelectedIndex = -1
            Else
                lbStilesRails.SelectedIndex = 0
            End If
        Else
            If lbStilesRails.Items.Count > itemIndex Then
                lbStilesRails.SelectedIndex = itemIndex
            Else
                lbStilesRails.SelectedIndex = -1
            End If
        End If
        lbStilesRails_SelectedIndexChanged(Me, Nothing)
        btnTransAdjoinPart.Enabled = (Not IsNothing(Me.frames)) AndAlso (Me.frames.CurrentFrameIndex > -1)
    End Sub


#End Region 'Update Listboxes

#Region "Process Job buttonclick methods"


    Private Sub btnImportItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnImportItem.Click
        If MsgBox("You are about to import parts for item " & Me.frames.CurrentFrame.ItemNo & ". This will remove any changes you have made to this frame. Do you wish to continue?", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
            Me.Cursor = Cursors.WaitCursor
            Dim retriever As New JobRetrieverClass
            retriever.LoadJob(Me._jobNumber, Module1.DataSourceDirectory, Module1.BarcodeDirectory, Module1.SourceExecutableFilePath)
            retriever.ImportFrameFromCSVFile(Me.frames, Me.frames.CurrentFrame.ItemNo, Module1.DataSourceDirectory)
            Me.ResetPartLayoutForm()
            Me.Cursor = Cursors.Default
        End If
    End Sub

    Private Sub btnSelectJobNumber_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSelectJobNumber.Click, mnuSelectJob.Click
        SetTabControlPages(False)
        SelectJobNumber()
    End Sub

    Private Sub btnLoadJob_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnLoadJob.Click
        lbSavedFiles.Items.Clear()
        comboItems.Items.Clear()
        If Not Me.frames Is Nothing Then
            Me.frames.ClearFrameList()
        End If
        lbCabList.Items.Clear()
        tvProgList.Nodes.Clear()
        Me._jobFileName = (Module1.DataSourceDirectory & Me._jobNumber & ".csv")
        txtLibPath.Text = Module1.ProgramOutputDirectory() & Me._jobNumber & "\"
        Me._progViewChanged = False
        Module1.JobFrameStyle = New FrameStyle(Trim(GetFrameStylename), ParmFilename)
        Module1.JobHingeStyle = New Hinge(GetHingeStylename, ParmFilename)
        Module1.JobHingeRule = New HingeRule(Module1.JobHingeStyle.placementRule, ParmFilename)
        Module1.JobThickness = New Thickness(Trim(GetThickStylename), ParmFilename)
        CreateFrames()
    End Sub

    Private Function CreateFrames() As Boolean
        'If File.Exists(Me._jobFileName) AndAlso Me._db.JobDataExists(Me._jobNumber) AndAlso Not useExistingCSV _
        'AndAlso MsgBox("There is existing data for this job. Do you want to use the old data?", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
        Dim retriever As New JobRetrieverClass
        Select Case Me.SourceOfJobData
            Case SourceOfData.dsExistingData
                Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
                Me.frames = New FrameListClass(Me._framesDb, Me._jobNumber)
                comboItems.Items.Clear()
                comboItems.Items.AddRange(Me._framesDb.GetJobItemNoList(Me._jobNumber))
                lbCabList.Items.AddRange(retriever.GetCabinetPartList(Me.frames).ToArray)
                If Not Directory.Exists(Module1.ProgramOutputDirectory & Me._jobNumber & "\") Then
                    Directory.CreateDirectory(Module1.ProgramOutputDirectory & Me._jobNumber & "\")
                End If
                loadLbSavedFiles()
                comboItems.SelectedIndex = 0
                SetTabControlPages()
                Me._progViewChanged = False
                LoadProgramlist()
                Cursor.Current = System.Windows.Forms.Cursors.Default
                Return True
            Case SourceOfData.dsExistingCSV
                Me.frames = retriever.ImportCSVFile(Me._jobNumber, Module1.DataSourceDirectory)
            Case SourceOfData.dsTemplateCSV
                Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
                Me.frames = New FrameListClass(Me._framesDb, Me._jobNumber)
                comboItems.Items.Clear()
                retriever.ImportFromTemplateFile(Me.frames, Module1.DataSourceDirectory)
            Case Else
                If Not retriever.LoadJob(Me._jobNumber, Module1.DataSourceDirectory, Module1.BarcodeDirectory, Module1.SourceExecutableFilePath) Then
                    Return False
                End If
                Me.frames = retriever.ImportCSVFile(Me._jobNumber, Module1.DataSourceDirectory)
        End Select
        Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        If Not Directory.Exists(Module1.DataSourceDirectory) Then
            Directory.CreateDirectory(Module1.DataSourceDirectory)
        End If
        If Not Directory.Exists(Module1.ProgramOutputDirectory & Me._jobNumber & "\") Then
            Directory.CreateDirectory(Module1.ProgramOutputDirectory & Me._jobNumber & "\")
        End If
        Me._progViewChanged = False
        LoadProgramlist()
        Cursor.Current = System.Windows.Forms.Cursors.Default
        lbCabList.Items.AddRange(retriever.GetCabinetPartList(Me.frames).ToArray)
        comboItems.Items.Clear()
        For index As Integer = 0 To frames.FrameCount - 1
            comboItems.Items.Add(frames.GetFrameByIndex(index).ItemNo)
            frames.GetFrameByIndex(index).SaveToDb(False)
            If comboItems.Items.Count > 0 Then
                comboItems.SelectedIndex = 0
            End If
        Next
        loadLbSavedFiles()
        SetTabControlPages()
        Return True
    End Function

    Private Sub SelectJobNumber(Optional ByVal jobNum As String = Nothing)
        Dim dlg As New FormUserInput(PointToScreen(Me.ToolStripContainer1.Location), 9)
        If jobNum IsNot Nothing Then
            dlg.txtJobNumber.Text = jobNum.Replace("-", "")
        End If
        If dlg.ShowDialog(Me) = DialogResult.OK Then
            Me.labelStatusJobNo.Text = Regex.Replace(dlg.txtJobNumber.Text.Trim, "([A-Za-z0-9]{5})([A-Za-z0-9]{2})([A-Za-z0-9]{2})", "$1-$2-$3")
            Dim url As String = Module1.JobofficeAspUrl & dlg.txtJobNumber.Text.Trim & "&mjobkey=*&mtype=jobhdr&mtime=current"
            'Dim url As String = My.Settings.JobofficeAsp1 & dlg.txtJobNumber.Text.Trim & My.Settings.JobofficeAsp2
            Me.browserJobHeader.Navigate(url)
            Me.ActivateSelectTabpage(dlg.txtJobNumber.Text.Trim)
        End If
    End Sub

    Private Sub ActivateSelectTabpage(ByVal jobno As String)
        If Me.frames IsNot Nothing Then
            Me.frames = Nothing
        End If
        Me._jobNumber = jobno.Trim
        Dim dataExists As Boolean = Me._framesDb.JobDataExists(jobno.Trim)
        Dim csvExists As Boolean = File.Exists(My.Settings.CsvFileDirectory & "\" & jobno.Trim & ".csv")
        Me.SetRadioGetJobDataEnabled(dataExists, csvExists)
        If dataExists Then
            Me.SourceOfJobData = SourceOfData.dsExistingData
        ElseIf csvExists Then
            Me.SourceOfJobData = SourceOfData.dsExistingCSV
        ElseIf Me._BOM_Exists Then
            Me.SourceOfJobData = SourceOfData.dsNewCSV
        Else
            Me.SourceOfJobData = SourceOfData.dsTemplateCSV
        End If
        Me.StyleRadioButtonGroup = StyleClassMetaData.GetRadioButtonGroupDataFromXML.StyleMeta
        If TabControl1.TabPages.IndexOf(tpSelect) = -1 Then
            TabControl1.TabPages.Add(Me._myPages(Me._myPages.IndexOf(tpSelect)))
        End If
        Me.TabControl1.SelectedTab = tpSelect
    End Sub

#End Region 'Process Job buttonclick methods"

#Region "Process part methods"

    Private Sub btnProcessPart_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnProcessPart.Click
        Dim pt As FrontFrameEventClasses.PartEdgeTypes = CType(System.Enum.Parse(GetType(FrontFrameEventClasses.PartEdgeTypes), CStr(comboPartType.SelectedItem)), FrontFrameEventClasses.PartEdgeTypes)
        If lbAdjoinParts.Items.Count > 0 Then
            If Trim(txtPartName.Text) <> "" Then
                ClearPartOperations()
                Me.FramePartImagePanel1.ClearDisplay()
                cbxPartName.Items.Remove(Trim(cbxPartName.Text))
                Me.frames.CurrentPart.SetOperations(Me.frames.CurrentFrame.CabinetType, pt)
                'BuildPartPath()
                'DrawPartOperations(pt, System.Drawing.SystemColors.ControlLightLight)
            Else
                sbPanelMessage.Text = "You must enter a part into the current part box."
                Exit Sub
            End If
        Else
            sbPanelMessage.Text = "You must add at least one part to the adjoining parts listbox."
            Exit Sub
        End If
        'picPart.Invalidate()
        Me.FramePartImagePanel1.DisplayGraphic(Me.frames.CurrentPart.EndTenonArgs)
        Me.frames.CurrentPart.WriteProgramFile(pt, Module1.ProgramOutputDirectory & Me._jobNumber & "\")
        loadLbSavedFiles()
        Me.frames.CurrentFrame.SaveToDb(False)
    End Sub

#End Region '"Process part methods"

#Region "Parts Layout DragDrop ====="

    Private Sub btnUp_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUp.Click
        Dim item As Object
        Dim index As Integer
        If lbAdjoinParts.SelectedIndex > 0 Then
            item = Me.frames.GetSinglePartByDescription(CStr(lbAdjoinParts.SelectedItem)).Name
            index = lbAdjoinParts.SelectedIndex
            Dim pt As FrontFrameEventClasses.PartEdgeTypes = CType(System.Enum.Parse(GetType(FrontFrameEventClasses.PartEdgeTypes), CStr(comboPartType.SelectedItem)), FrontFrameEventClasses.PartEdgeTypes)
            'CType(frameList(currFrame).Partslist(currFramePart), FramePart).AdjoiningPartlist(pt).Insert(index - 1, item)
            'CType(frameList(currFrame).Partslist(currFramePart), FramePart).AdjoiningPartlist(pt).RemoveAt(index + 1)
            Me.frames.CurrentPart.AdjoiningPartNameList(pt).Insert(index - 1, item)
            Me.frames.CurrentPart.AdjoiningPartNameList(pt).RemoveAt(index + 1)
            UpdateAdjoinPartsList(Me.frames.CurrentPart.AdjoiningPartNameList(pt))
            lbAdjoinParts.SelectedIndex = index - 1
        End If
    End Sub

    Private Sub btnDown_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDown.Click
        Dim item As Object
        Dim index As Integer
        If (lbAdjoinParts.SelectedIndex > -1) And (lbAdjoinParts.SelectedIndex < lbAdjoinParts.Items.Count - 1) Then
            'item = lbAdjoinParts.SelectedItem
            item = Me.frames.GetSinglePartByDescription(CStr(lbAdjoinParts.SelectedItem)).Name
            index = lbAdjoinParts.SelectedIndex
            Dim pt As FrontFrameEventClasses.PartEdgeTypes = CType(System.Enum.Parse(GetType(FrontFrameEventClasses.PartEdgeTypes), CStr(comboPartType.SelectedItem)), FrontFrameEventClasses.PartEdgeTypes)
            Me.frames.CurrentPart.AdjoiningPartNameList(pt).Insert(index + 2, item)
            Me.frames.CurrentPart.AdjoiningPartNameList(pt).RemoveAt(index)
            UpdateAdjoinPartsList(Me.frames.CurrentPart.AdjoiningPartNameList(pt))
            lbAdjoinParts.SelectedIndex = index + 1
        End If
    End Sub

    Private Sub btnRemove_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRemove.Click
        If (lbAdjoinParts.SelectedIndex > -1) Then
            Dim list As New ArrayList
            Dim pt As FrontFrameEventClasses.PartEdgeTypes = CType(System.Enum.Parse(GetType(FrontFrameEventClasses.PartEdgeTypes), CStr(comboPartType.SelectedItem)), FrontFrameEventClasses.PartEdgeTypes)
            list = Me.frames.CurrentPart.AdjoiningPartNameList(pt)
            For i As Integer = 0 To list.Count - 1
                'If CType(list(i), FramePart).Name = CType(lbAdjoinParts.SelectedItem, FramePart).Name Then
                If Me.frames.GetSinglePart(CStr(list(i))).Name = Me.frames.GetSinglePartByDescription(CStr(lbAdjoinParts.SelectedItem)).Name Then
                    Me.frames.CurrentPart.AdjoiningPartNameList(pt).RemoveAt(i)
                    Exit For
                End If
            Next
            RefreshPartsLists()
        End If
    End Sub

    Private Sub lbAdjoinParts_DragEnter(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DragEventArgs) Handles lbAdjoinParts.DragEnter
        If e.Data.GetDataPresent(GetType(FramePart)) AndAlso Me.frames.CurrentFrameIndex > -1 Then
            e.Effect = DragDropEffects.Copy Or DragDropEffects.Move
        End If
    End Sub

    Private Sub lbAdjoinParts_DragDrop(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DragEventArgs) Handles lbAdjoinParts.DragDrop
        If Me.frames.CurrentPart Is Nothing Then
            MsgBox("Current part must be selected before adding adjoining parts.")
        Else
            If e.Data.GetDataPresent(GetType(FramePart)) Then
                Dim item As New FramePart(CType(e.Data.GetData(GetType(FramePart)), FramePart))
                'Me.frames.CurrentPart.AdjoiningPartNameList(CType(System.Enum.Parse(GetType(FrontFrameEventClasses.PartEdgeTypes), CStr(comboPartType.SelectedItem)), FrontFrameEventClasses.PartEdgeTypes)).Add(item.Name)
                Me.frames.CurrentPart.PartEdges(Me.frames.CurrentPart.EdgeIndexOf(CType(System.Enum.Parse(GetType(FrontFrameEventClasses.PartEdgeTypes), CStr(comboPartType.SelectedItem)), FrontFrameEventClasses.PartEdgeTypes))).AddAdjoiningPart(item.Name)
                RefreshPartsLists()
            End If
        End If
    End Sub

    Private Sub lbAdjoinParts_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lbAdjoinParts.SelectedIndexChanged
        btnUp.Enabled = lbAdjoinParts.SelectedIndex > 0 And lbAdjoinParts.Items.Count > 1
        btnDown.Enabled = lbAdjoinParts.SelectedIndex > -1 And lbAdjoinParts.SelectedIndex < lbAdjoinParts.Items.Count - 1 And lbAdjoinParts.Items.Count > 1
        btnRemove.Enabled = lbAdjoinParts.SelectedIndex > -1
    End Sub

    Private Sub lbStilesRails_MouseMove(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles lbStilesRails.MouseMove
        If ((e.Button And MouseButtons.Left) = MouseButtons.Left) Then
            Try
                Dim indexOfItemUnderMouseToDrag As Integer = lbStilesRails.IndexFromPoint(e.X, e.Y)
                If (indexOfItemUnderMouseToDrag <> ListBox.NoMatches) Then
                    Dim dragItem As FramePart = (CType(lbStilesRails.Items(indexOfItemUnderMouseToDrag), FramePart))
                    Dim myObject As New DataObject
                    myObject.SetData(dragItem)
                    lbStilesRails.DoDragDrop(myObject, DragDropEffects.Move)
                Else
                    lbStilesRails.Select()
                End If
            Catch ex As Exception
            End Try
        Else
            Dim indexOfItemUnderMouseToDrag As Integer = lbStilesRails.IndexFromPoint(e.X, e.Y)
            If (indexOfItemUnderMouseToDrag <> ListBox.NoMatches) Then
                Dim fp As FramePart = (CType(lbStilesRails.Items(indexOfItemUnderMouseToDrag), FramePart))
                Dim tipText As String = fp.PartEdges(0).Comment
                If fp.TSComments <> "" Then
                    tipText += " | TS Comment: " & fp.TSComments
                End If
                Me.ToolTip1.SetToolTip(Me.lbStilesRails, tipText)
            End If
        End If
    End Sub

    Private Sub lbStilesRails_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles lbStilesRails.MouseUp
        Dim indexOfItemUnderMouseToDrag As Integer = Me.lbStilesRails.IndexFromPoint(e.X, e.Y)
        Me.lbStilesRails.SelectedIndex = indexOfItemUnderMouseToDrag
        If e.Button = MouseButtons.Right Then
            If (indexOfItemUnderMouseToDrag <> ListBox.NoMatches) Then
                Dim fp As FramePart = (CType(Me.lbStilesRails.Items(indexOfItemUnderMouseToDrag), FramePart))
                If Not fp Is Nothing Then
                    Dim dlg As New UserInputBox(0)
                    Me.ToolTip1.Hide(Me.lbStilesRails)
                    dlg.Text = "Add comment for " & fp.Name
                    dlg.Prompt = "Enter or modify " & fp.Name & " comments:"
                    dlg.txtJobNumber.Text = fp.PartEdges(0).Comment
                    If dlg.ShowDialog() = DialogResult.OK Then
                        fp.PartEdges(0).Comment = dlg.txtJobNumber.Text
                    End If
                End If
            End If
        End If
    End Sub

    Private Sub lbOpenings_MouseMove(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles lbOpenings.MouseMove
        Dim xy As New Point(e.X, e.Y)
        If e.Button = MouseButtons.Left Then
            Try
                Dim dragItem As FramePart = CType(lbOpenings.Items(lbOpenings.IndexFromPoint(xy)), FramePart)
                Dim myObject As New DataObject
                myObject.SetData(dragItem)
                lbOpenings.DoDragDrop(myObject, DragDropEffects.Move)
            Catch ex As Exception
                lbOpenings.Select()
            End Try
        End If
    End Sub

    Private Sub lbStilesRails_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lbStilesRails.Enter
        lbStilesRails.Select()
    End Sub

    Private Sub lbStilesRails_MouseEnter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lbStilesRails.MouseEnter
        lbStilesRails.Select()
    End Sub

    Private Sub lbOpenings_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lbOpenings.Enter
        lbOpenings.Select()
    End Sub

    Private Sub lbOpenings_MouseEnter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lbOpenings.MouseEnter
        lbOpenings.Select()
    End Sub

    Private Sub lbOpenings_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lbOpenings.SelectedIndexChanged
        Me.checkHingeL.Enabled = lbOpenings.SelectedIndex > -1
        Me.checkHingeR.Enabled = lbOpenings.SelectedIndex > -1
        Me.btnEditOpg.Enabled = lbOpenings.SelectedIndex > -1
        Me.btnTransAdjoinOpg.Enabled = lbOpenings.SelectedIndex > -1
        If lbOpenings.SelectedIndex > -1 Then
            lblCurrOpening.Text = CType(lbOpenings.SelectedItem, FramePart).Description
        End If
    End Sub

    Private Sub lbStilesRails_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lbStilesRails.SelectedIndexChanged
        Me.btnTransAdjoinPart.Enabled = lbStilesRails.SelectedIndex > -1
        Me.btnTransAdjoinPart.Enabled = lbStilesRails.SelectedIndex > -1
        Me.btnEditPart.Enabled = lbStilesRails.SelectedIndex > -1
    End Sub

    Private Sub checkHingeL_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles checkHingeL.CheckedChanged
        If lbOpenings.SelectedIndex > -1 Then
            Dim hp As HingePlacement = CType(lbOpenings.SelectedItem, FramePart).Hinging
            If checkHingeL.Checked Then
                If Not (hp And HingePlacement.L) = HingePlacement.L Then
                    hp = hp Or HingePlacement.L
                End If
            Else
                If (hp And HingePlacement.L) = HingePlacement.L Then
                    hp = hp Xor HingePlacement.L
                End If
            End If
            UpdateHingePlacement(hp)
            lbOpenings.Focus()
        End If
    End Sub

    Private Sub checkHingeR_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles checkHingeR.CheckedChanged
        If lbOpenings.SelectedIndex > -1 Then
            Dim hp As HingePlacement = CType(lbOpenings.SelectedItem, FramePart).Hinging
            If checkHingeR.Checked Then
                If Not (hp And HingePlacement.R) = HingePlacement.R Then
                    hp = hp Or HingePlacement.R
                End If
            Else
                If (hp And HingePlacement.R) = HingePlacement.R Then
                    hp = hp Xor HingePlacement.R
                End If
            End If
            UpdateHingePlacement(hp)
            lbOpenings.Focus()
        End If
    End Sub

    Private Sub checkHinge_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles checkHingeL.Click, checkHingeR.Click
        If lbOpenings.SelectedIndex = -1 Then
            MessageBox.Show("You must select an item from the Opening List before changing hinge settings")
        End If
    End Sub

    Private Sub UpdateHingePlacement(ByVal hp As HingePlacement)
        Dim ptCount As Integer = Me.frames.CurrentFrame.Openinglist.Count
        For i As Integer = 0 To ptCount - 1
            If CType(Me.frames.CurrentFrame.Openinglist.Item(i), FramePart).Name = CType(lbOpenings.SelectedItem, FramePart).Name Then
                CType(Me.frames.CurrentFrame.Openinglist.Item(i), FramePart).Hinging = hp
                Exit For
            End If
        Next
        RefreshPartsLists()
    End Sub

    Private Sub lblCurrOpening_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lblCurrOpening.TextChanged
        If Not lbOpenings.SelectedItem Is Nothing Then
            Dim currAdjoinPart As FramePart = (CType(lbOpenings.SelectedItem, FramePart))
            checkHingeL.Checked = (currAdjoinPart.Hinging And HingePlacement.L) = HingePlacement.L
            checkHingeR.Checked = (currAdjoinPart.Hinging And HingePlacement.R) = HingePlacement.R
        End If
    End Sub

    Private Sub loadLbSavedFiles()
        If Directory.Exists(txtLibPath.Text) Then
            Dim dirs As String() = Directory.GetFiles(txtLibPath.Text, "*.acc")
            lbSavedFiles.Items.Clear()
            Dim width As Integer
            For i As Integer = 0 To dirs.Length - 1
                Dim strs() As String = dirs(i).Split(CChar("\"))
                lbSavedFiles.Items.Add(strs(strs.GetUpperBound(0)))
                width = CInt(lbSavedFiles.CreateGraphics().MeasureString(lbSavedFiles.Items(lbSavedFiles.Items.Count - 1).ToString(), _
                    lbSavedFiles.Font).Width)
                ' Set the column width based on the width of each item in the list.
                If width > lbSavedFiles.ColumnWidth Then
                    lbSavedFiles.ColumnWidth = width
                End If
            Next
        End If
    End Sub

    Private Sub lbSavedFiles_DoubleClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lbSavedFiles.DoubleClick
        puMnuItemEditSelFile_Click(sender, e)
    End Sub

    Private Sub lbSavedFiles_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles lbSavedFiles.MouseDown
        Me.lbSavedFiles.SelectedIndex = Me.lbSavedFiles.IndexFromPoint(e.X, e.Y)
    End Sub

    Private Sub btnTransCurrPart_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnTransCurrPart.Click
        If lbStilesRails.SelectedIndex > -1 Then
            Dim dragItem As New FramePart(CType(lbStilesRails.Items(lbStilesRails.SelectedIndex), FramePart))
            Dim myObject As New DataObject
            myObject.SetData(dragItem)
            Dim ev As New System.Windows.Forms.DragEventArgs(myObject, Nothing, 0, 0, DragDropEffects.Copy, DragDropEffects.Copy)
            txtPartName_DragDrop(sender, ev)
        End If
    End Sub

    Private Sub btnTransAdjoinPart_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnTransAdjoinPart.Click
        If lbStilesRails.SelectedIndex > -1 Then
            Dim dragItem As FramePart = (CType(lbStilesRails.Items(lbStilesRails.SelectedIndex), FramePart))
            Dim myObject As New DataObject
            myObject.SetData(dragItem)
            Dim ev As New System.Windows.Forms.DragEventArgs(myObject, Nothing, 0, 0, DragDropEffects.Copy Or DragDropEffects.Move, DragDropEffects.Move)
            lbAdjoinParts_DragDrop(sender, ev)
        End If
    End Sub

    Private Sub btnTransAdjoinOpg_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnTransAdjoinOpg.Click
        If lbOpenings.SelectedIndex > -1 Then
            Dim dragItem As FramePart = (CType(lbOpenings.Items(lbOpenings.SelectedIndex), FramePart))
            Dim myObject As New DataObject
            myObject.SetData(dragItem)
            Dim ev As New System.Windows.Forms.DragEventArgs(myObject, Nothing, 0, 0, DragDropEffects.Copy Or DragDropEffects.Move, DragDropEffects.Move)
            lbAdjoinParts_DragDrop(sender, ev)
        End If
    End Sub

    Private Sub txtLibPath_MouseHover(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtLibPath.MouseHover
        ToolTip1.SetToolTip(txtLibPath, txtLibPath.Text)
    End Sub

#End Region '"Parts Layout DragDrop ====="

#Region "Menu Events ======"

    Private Sub mnuAbout_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AboutToolStripMenuItem.Click
        Dim dlg As New frmSplash
        dlg.lblOneMoment.Visible = False
        dlg.btnOk.Visible = True
        dlg.ShowDialog()
    End Sub

    Private Sub mnuExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuExit.Click
        Close()
    End Sub

    Private Sub puMnuItemDelSelFile_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles puMnuItemDelSelFile.Click
        If lbSavedFiles.SelectedIndex > -1 Then
            If MessageBox.Show("This will permanently delete the program for this part.  Do you want to continue?", "Delete File", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2) = DialogResult.Yes Then
                Dim fileToDelete As String = txtLibPath.Text & CStr(lbSavedFiles.SelectedItem)
                If File.Exists(fileToDelete) Then
                    File.Delete(fileToDelete)
                    loadLbSavedFiles()
                Else
                    MessageBox.Show("File " & fileToDelete & " was not found.")
                End If
            End If
        Else
            MsgBox("An item must be selected first")
        End If
    End Sub

    Private Sub puMnuItemEditSelFile_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles puMnuItemEditSelFile.Click
        If lbSavedFiles.SelectedIndex > -1 Then
            If File.Exists(txtLibPath.Text & CStr(lbSavedFiles.SelectedItem)) Then
                Dim fi As New FileInfo(txtLibPath.Text & CStr(lbSavedFiles.SelectedItem))
                Dim ed As New frmEditProgramText(fi, Nothing, Me._framesDb)
                ed.ShowDialog()
            End If
        Else
            MsgBox("An item must be selected first")
        End If
    End Sub

    Private Sub puMnuShowPartsSelFile_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles puMnuShowPartsSelFile.Click
        If lbSavedFiles.SelectedIndex > -1 Then
            Dim pn As String = CStr(lbSavedFiles.SelectedItem)
            Dim strs() As String = pn.Split(CChar("_"), CChar("."))
            'Dim n As char() = pn.ToCharArray
            'For i As Integer = 0 To n.GetUpperBound(0) - 1
            '    If Char.IsLetter(n(i)) Then
            '        pn = pn.Substring(0, i) + "." + pn.Substring(i)
            '        Exit For
            '    End If
            'Next
            pn = strs(0) + "." + strs(1)
            If cbxPartName.Items.IndexOf(pn) = -1 Then
                cbxPartName.Items.Add(pn)
            End If
            cbxPartName.SelectedIndex = cbxPartName.Items.IndexOf(pn)
        Else
            MsgBox("An item must be selected first")
        End If
    End Sub

    Private Sub mnuEditSetupData_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles menuEditSetupData.Click
        If Not IsNothing(Me.frames) Then
            If MsgBox("The current job must be closed to edit setup data.  Do you want to continue?", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
                InitializeTabPages2Start()
                ClearFramelist()
            Else
                Exit Sub
            End If
        End If
        Dim dlg As New ParmSetup
        If dlg.ShowDialog() = DialogResult.OK Then

        End If
    End Sub

    Private Sub mnuPrintBC4Files_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuPrintBC4Files.Click, mnuPrintBarcodes2.Click
        Dim dlg As New BarMain
        dlg.ShowDialog()
    End Sub

    Private Sub mnuAddEditPartComment_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuAddEditPartComment.Click
        'Dim ed As New frmEditProgramText(CType(frameList(currFrame).Partslist(currFramePart), FramePart).CNCPrgComment)
        Dim dlg As New UserInputBox(0)
        Me.ToolTip1.Hide(CType(sender, Control))
        dlg.Text = "Add comment"
        dlg.Prompt = "Enter or modify part comments:"
        dlg.txtJobNumber.Text = CType(Me.lbStilesRails.SelectedItem, FramePart).PartEdges(0).Comment
        If dlg.ShowDialog() = DialogResult.OK Then
            CType(Me.lbStilesRails.SelectedItem, FramePart).PartEdges(0).Comment = dlg.txtJobNumber.Text
        End If

    End Sub

    Private Sub mnuPurgeOldData_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles menuPurgeOldData.Click
        Dim dlg As New formPurgeDatabase
        If dlg.ShowDialog() = Windows.Forms.DialogResult.Yes Then
            Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
            Dim num As Integer = Me._framesDb.PurgeJobDataByDate(dlg.DateTimePicker1.Value)
            Cursor.Current = System.Windows.Forms.Cursors.Default
            MsgBox("Purged " & num & " records from database")
        End If
    End Sub

    Private Sub mnuAddPartComment_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuAddPartComment.Click
        Dim dlg As New UserInputBox(0)
        Me.ToolTip1.Hide(CType(sender, Control))
        dlg.Text = "Add comment"
        dlg.Prompt = "Enter or modify part comments:"
        Dim fp As FramePart = Me.frames.GetSinglePartByPartedgename(Me._jobNumber & "." & Trim(tvProgList.SelectedNode.Text))
        dlg.txtJobNumber.Text = fp.PartEdges(0).Comment
        If dlg.ShowDialog() = DialogResult.OK Then
            fp.PartEdges(0).Comment = dlg.txtJobNumber.Text
            Me._progViewChanged = True
            Me.tvProgList.SelectedNode.ToolTipText = fp.PartEdges(0).Comment & " | TS Comment: " & fp.TSComments
        End If

    End Sub

    Private Sub mnuMove2List_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuMove2List.Click
        If lbCabList.SelectedIndex > -1 Then
            Dim str As String = CType(lbCabList.SelectedItem, String)
            str = str.Substring(str.LastIndexOf(":") + 1)
            str = str.Substring(0, str.LastIndexOf(vbTab))
            Dim dragItem As TreeNode = New TreeNode(str)
            Dim myObject As New DataObject
            myObject.SetData(dragItem)
            Dim ev As New System.Windows.Forms.DragEventArgs(myObject, Nothing, 0, 0, DragDropEffects.Copy Or DragDropEffects.Move, DragDropEffects.Copy)
            tvProgList_DragDrop(sender, ev)
            lbCabList.Focus()
        End If
    End Sub

    Private Sub menuViewEditProgramFiles_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles menuEditPrograms.Click
        Dim ed As New frmEditProgramText(Me._framesDb)
        ed.ShowDialog()
    End Sub

    Private Sub toolBtnCabInfo_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles toolBtnCabInfo.Click
        pnlCabinetInfo.Visible = Me.toolBtnCabInfo.Checked

        If pnlCabinetInfo.Visible Then
            Dim yr As Integer = Now.Year
            Dim fInfo As New FileInfo(Me._jobFileName)
            yr = fInfo.CreationTime.Year()
            Dim fname As String = Me._jobNumber + CStr(comboItems.SelectedItem) + "F.dwf"
            If File.Exists(Module1.DwfSourceDirectory + yr.ToString + "\" + fname) Then

            ElseIf File.Exists(Module1.DwfSourceDirectory + (yr - 1).ToString + "\" + fname) Then
                yr -= 1
            ElseIf File.Exists(Module1.DwfSourceDirectory + (yr + 1).ToString + "\" + fname) Then
                yr += 1
            Else
                yr = 0
            End If
            If (yr > 0) Then
                Dim url As String = Module1.CadAspUrl & "?lcname=" & fname & "&mjobno=" & Me._jobNumber & "&mitem=" & CStr(comboItems.SelectedItem) & "&mjcustno=0005&mtime=current&myear=" & yr.ToString
                Process.Start(url)
            End If
        End If
    End Sub

    Private Sub toolBtnPrintBarcodeReport_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles toolBtnPrintBarcodeReport.Click
        Me.New_btnPrintBarcode_Click(sender, e)
    End Sub

#End Region '"Menu Events ======"

#Region "Treeview Part Program List ======"

    Private Sub lbCabList_DragOver(ByVal sender As Object, ByVal e As System.Windows.Forms.DragEventArgs) Handles lbCabList.DragOver
        e.Effect = DragDropEffects.All
    End Sub
    Private Sub lbCabList_MouseMove(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles lbCabList.MouseMove
        If e.Button = MouseButtons.Left Then
            Try
                Dim indexOfItemUnderMouseToDrag As Integer = lbCabList.IndexFromPoint(e.X, e.Y)
                Dim str As String = CType(lbCabList.Items(indexOfItemUnderMouseToDrag), String)
                str = str.Substring(str.LastIndexOf(":") + 1)
                str = str.Substring(0, str.LastIndexOf(vbTab))
                Dim dragItem As TreeNode = New TreeNode(Trim(str))
                Dim myObject As New DataObject
                myObject.SetData(dragItem)
                lbCabList.DoDragDrop(myObject, DragDropEffects.Copy)
            Catch ex As Exception
                MsgBox(ex.Message)
                lbCabList.Select()
            End Try
        End If
    End Sub

    Private Sub lbCabList_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles lbCabList.MouseDown
        Dim indexOfItemUnderMouse As Integer = Me.lbCabList.IndexFromPoint(e.X, e.Y)
        If CStr(Me.lbCabList.Items(indexOfItemUnderMouse)).Trim.StartsWith("Part") Then
            Me.lbCabList.SelectedIndex = indexOfItemUnderMouse
            If e.Button = MouseButtons.Right Then
                Me.popupCabList.Show(Me.lbCabList, New Point(e.X, e.Y))
            End If
        End If
    End Sub

    Private Function NodeFound(ByVal nodes As TreeNodeCollection, ByVal node As TreeNode, ByVal includeChildnodes As Boolean) As Point
        Dim retVal As New Point(-1, -1)
        Dim nd As TreeNode
        For i As Integer = 0 To nodes.Count - 1
            If nodes(i).Text.Trim = node.Text.Trim Then
                retVal.X = nodes(i).Index
                retVal.Y = -1
                Return retVal
            End If
            For Each nd In nodes(i).Nodes
                If nd.Text.Trim = node.Text.Trim Then
                    retVal.X = nodes(i).Index
                    retVal.Y = nd.Index
                    Return retVal
                End If

            Next
        Next
        Return retVal
    End Function

    Public Function GetNodeLevel(ByVal node As TreeNode) As Integer
        Dim level As Integer = 0
        While Not node Is Nothing
            node = node.Parent
            level = level + 1
        End While
        Return level
    End Function

    Public Function SelectFoundNode(ByVal node As TreeNode) As Boolean
        Dim Loc As Point = NodeFound(tvProgList.Nodes, node, True)
        If IsNothing(Loc) OrElse (Loc.X = -1 And Loc.Y = -1) Then
            Return False
        ElseIf Loc.Y = -1 Then
            tvProgList.SelectedNode = tvProgList.Nodes(Loc.X)
        Else
            tvProgList.SelectedNode = tvProgList.Nodes(Loc.X).Nodes(Loc.Y)
        End If
        tvProgList.Nodes(Loc.X).Expand()
        tvProgList.Focus()
        Return True
    End Function

    Private Sub tvProgList_DragDrop(ByVal sender As Object, ByVal e As System.Windows.Forms.DragEventArgs) Handles tvProgList.DragDrop
        Dim node As TreeNode = New TreeNode()
        If e.Data.GetDataPresent(GetType(TreeNode)) Then
            node = CType(e.Data.GetData(GetType(TreeNode)), TreeNode)
            Dim fp As FramePart = Me.frames.GetSinglePartByPartedgename(Me._jobNumber & "." & Trim(node.Text))
            node.ToolTipText = fp.PartEdges(0).Comment & " | TS Comment: " & fp.TSComments
            node.Tag = fp
        End If
        Me._progViewChanged = True
        Dim rootNode As TreeNode = tvProgList.GetNodeAt(tvProgList.PointToClient(New Point(e.X, e.Y)))
        If GetNodeLevel(rootNode) > 1 Then
            rootNode = rootNode.Parent
        End If
        'Dim strs() As String = node.Text.Split(CChar("."))
        If IsNothing(rootNode) Then
            If SelectFoundNode(node) Then
                node.TreeView.Nodes.Remove(node)
                tvProgList.Nodes.Add(node)
            Else
                If (CType(node.Tag, FramePart).PartType = FrontFrameEventClasses.PartEdgeTypes.CenterStile) Then
                    Dim nd As New TreeNode(node.Text + "L")
                    nd.ToolTipText = node.ToolTipText
                    nd.Tag = node.Tag
                    If Not SelectFoundNode(nd) Then
                        tvProgList.Nodes.Add(nd)
                    End If
                    node.Text += "R"
                    If Not SelectFoundNode(node) Then
                        tvProgList.Nodes.Add(node)
                    End If
                ElseIf (CType(node.Tag, FramePart).PartType = FrontFrameEventClasses.PartEdgeTypes.CenterRail) Then
                    Dim nd As New TreeNode(node.Text + "T")
                    nd.ToolTipText = node.ToolTipText
                    nd.Tag = node.Tag
                    If Not SelectFoundNode(nd) Then
                        tvProgList.Nodes.Add(nd)
                    End If
                    node.Text += "B"
                    If Not SelectFoundNode(node) Then
                        tvProgList.Nodes.Add(node)
                    End If
                Else
                    tvProgList.Nodes.Add(node)
                End If
            End If
        Else
            If SelectFoundNode(node) Then
                node.TreeView.Nodes.Remove(node)
                rootNode.Nodes.Add(node)
            Else
                If (CType(node.Tag, FramePart).PartType = FrontFrameEventClasses.PartEdgeTypes.CenterStile) Then
                    Dim nd As New TreeNode(node.Text + "L")
                    nd.ToolTipText = node.ToolTipText
                    nd.Tag = node.Tag
                    If Not SelectFoundNode(nd) Then
                        rootNode.Nodes.Add(nd)
                    End If
                    node.Text += "R"
                    If Not SelectFoundNode(node) Then
                        rootNode.Nodes.Add(node)
                    End If
                ElseIf (CType(node.Tag, FramePart).PartType = FrontFrameEventClasses.PartEdgeTypes.CenterRail) Then
                    Dim nd As New TreeNode(node.Text + "T")
                    nd.ToolTipText = node.ToolTipText
                    nd.Tag = node.Tag
                    If Not SelectFoundNode(nd) Then
                        rootNode.Nodes.Add(nd)
                    End If
                    node.Text += "B"
                    If Not SelectFoundNode(node) Then
                        rootNode.Nodes.Add(node)
                    End If
                Else
                    If node.GetNodeCount(False) > 0 Then
                        For i As Integer = node.GetNodeCount(False) - 1 To 0 Step -1
                            rootNode.Nodes.Add(New TreeNode(node.Nodes(i).Text))
                        Next
                    End If
                    node.Nodes.Clear()
                    rootNode.Nodes.Add(node)
                End If
            End If
            SelectFoundNode(node)
        End If
        tvProgList.Refresh()

        Me.WriteProgList2Xml()

    End Sub

    Private Sub tvProgList_ItemDrag(ByVal sender As Object, ByVal e As System.Windows.Forms.ItemDragEventArgs) Handles tvProgList.ItemDrag
        If e.Button = MouseButtons.Left Then
            DoDragDrop(e.Item, DragDropEffects.All)
        End If
    End Sub

    Private Sub tvProgList_DragEnter(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DragEventArgs) Handles tvProgList.DragEnter
        e.Effect = DragDropEffects.All
    End Sub

    Private Sub tvProgList_DragOver(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DragEventArgs) Handles tvProgList.DragOver
        tvProgList.Focus()
        Dim nodeUnderMouse As TreeNode = tvProgList.GetNodeAt(tvProgList.PointToClient(New Point(e.X, e.Y)))
        tvProgList.SelectedNode = tvProgList.GetNodeAt(tvProgList.PointToClient(New Point(e.X, e.Y)))
    End Sub

    Private Sub btnTransNode_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnTransNode.Click
        If lbCabList.SelectedIndex > -1 Then
            Dim str As String = CType(lbCabList.SelectedItem, String)
            str = str.Substring(str.LastIndexOf(":") + 1)
            str = str.Substring(0, str.LastIndexOf(vbTab))
            Dim dragItem As TreeNode = New TreeNode(str)
            Dim myObject As New DataObject
            myObject.SetData(dragItem)
            Dim ev As New System.Windows.Forms.DragEventArgs(myObject, Nothing, 0, 0, DragDropEffects.Copy Or DragDropEffects.Move, DragDropEffects.Copy)
            tvProgList_DragDrop(sender, ev)
            lbCabList.Focus()
        End If
    End Sub

    Private Sub lbCabList_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lbCabList.SelectedIndexChanged
        btnTransNode.Enabled = CStr(lbCabList.SelectedItem).Trim.StartsWith("Part")
    End Sub

    Private Sub tvProgList_AfterSelect(ByVal sender As System.Object, ByVal e As System.Windows.Forms.TreeViewEventArgs) Handles tvProgList.AfterSelect
        btnRemoveProg.Enabled = Not IsNothing(tvProgList.SelectedNode)
        btnTvDown.Enabled = Not IsNothing(tvProgList.SelectedNode)
        btnTvUp.Enabled = Not IsNothing(tvProgList.SelectedNode)
    End Sub

    Private Sub btnRemoveProg_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRemoveProg.Click
        If Not IsNothing(tvProgList.SelectedNode) AndAlso tvProgList.SelectedNode.GetNodeCount(False) = 0 Then
            Me._progViewChanged = True
            tvProgList.Nodes.Remove(tvProgList.SelectedNode)
        Else
            MsgBox("Node selected for deletion has child nodes. Remove child nodes first.")
        End If
    End Sub

    Private Sub btnTvUp_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnTvUp.Click
        Dim index As Integer
        Dim tvNd As TreeNode = tvProgList.SelectedNode
        Dim nodes As TreeNodeCollection
        If IsNothing(tvProgList.SelectedNode.Parent) Then
            nodes = tvProgList.Nodes
        Else
            nodes = tvProgList.SelectedNode.Parent.Nodes
        End If
        index = tvNd.Index
        nodes.RemoveAt(index)
        nodes.Insert(index - 1, tvNd)
        tvProgList.Focus()
        tvProgList.SelectedNode = tvNd
    End Sub

    Private Sub btnTvDown_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnTvDown.Click
        Dim index As Integer
        Dim tvNd As TreeNode = tvProgList.SelectedNode
        Dim nodes As TreeNodeCollection
        If IsNothing(tvProgList.SelectedNode.Parent) Then
            nodes = tvProgList.Nodes
        Else
            nodes = tvProgList.SelectedNode.Parent.Nodes
        End If
        index = tvNd.Index
        nodes.RemoveAt(index)
        nodes.Insert(index + 1, tvNd)
        tvProgList.Focus()
        tvProgList.SelectedNode = tvNd
    End Sub

    Private Sub tvProgList_GotFocus(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tvProgList.GotFocus
        btnRemoveProg.Enabled = Not tvProgList.SelectedNode Is Nothing
    End Sub

    Private Sub cbxPartName_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbxPartName.SelectedIndexChanged
        Me.ResetPartLayoutForm()

        Dim str As String = CStr(cbxPartName.SelectedItem).Trim
        Dim strs() As String = str.Split(CChar("_"), CChar("."))
        comboItems.SelectedIndex = comboItems.Items.IndexOf(Trim(strs(0)))
        If strs(1).StartsWith("SC") And str.LastIndexOf("L") = str.Length - 1 Then
            str = str.Remove(str.LastIndexOf("L"), 1)
        ElseIf strs(1).StartsWith("SC") And str.LastIndexOf("R") = str.Length - 1 Then
            str = str.Remove(str.LastIndexOf("R"), 1)
        ElseIf strs(1).StartsWith("RC") And str.LastIndexOf("T") = str.Length - 1 Then
            str = str.Remove(str.LastIndexOf("T"), 1)
        ElseIf strs(1).StartsWith("RC") And str.LastIndexOf("B") = str.Length - 1 Then
            str = str.Remove(str.LastIndexOf("B"), 1)
        End If
        For i As Integer = 0 To lbStilesRails.Items.Count - 1
            If CType(lbStilesRails.Items(i), FramePart).Name = Me._jobNumber & "." & Trim(str) Then
                Dim dragItem As FramePart = (CType(lbStilesRails.Items(i), FramePart))
                Dim myObject As New DataObject
                myObject.SetData(dragItem)
                Dim ev As New System.Windows.Forms.DragEventArgs(myObject, 1, 0, 0, DragDropEffects.Copy, DragDropEffects.Copy)
                txtPartName_DragDrop(sender, ev)
                Exit Sub
            End If
        Next
        MsgBox(Me._jobNumber & "." & Trim(str) & " was not found in Stile and Rail list.")
    End Sub

    Private Sub tvProgList_MouseDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles tvProgList.MouseDown
        If e.Button = MouseButtons.Left Then
            Me._tvNodeToDrag = (tvProgList.GetNodeAt(New Point(e.X, e.Y)))
            If Not IsNothing(Me._tvNodeToDrag) Then
                Dim dragSize As Size = SystemInformation.DragSize
                Me._dragBoxFromMouseDown = New Rectangle(New Point(e.X - CInt(dragSize.Width / 2), e.Y - CInt(dragSize.Height / 2)), dragSize)
            Else
                Me._dragBoxFromMouseDown = Rectangle.Empty
            End If
        End If
    End Sub

    Private Sub tvProgList_MouseUp(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles tvProgList.MouseUp
        Me._dragBoxFromMouseDown = Rectangle.Empty
        Me._tvNodeToDrag = Nothing
    End Sub

    Private Sub tvProgList_MouseMove(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles tvProgList.MouseMove
        If ((e.Button And MouseButtons.Left) = MouseButtons.Left) Then
            If (Rectangle.op_Inequality(Me._dragBoxFromMouseDown, Rectangle.Empty) And _
                Not Me._dragBoxFromMouseDown.Contains(e.X, e.Y)) Then
                tvProgList.Nodes.Remove(Me._tvNodeToDrag)
                Dim myObject As New DataObject
                myObject.SetData(Me._tvNodeToDrag)
                tvProgList.DoDragDrop(myObject, DragDropEffects.Move)
            End If
        End If
    End Sub

    Private Function UpdateProgramNames() As Boolean
        For i As Integer = 0 To tvProgList.GetNodeCount(False) - 1
            Dim pe As PartEdge
            pe = Me.frames.FindPartEdge(Me._jobNumber & "." & Trim(tvProgList.Nodes(i).Text))
            If IsNothing(pe) Then
                MsgBox("Part " & Trim(tvProgList.Nodes(i).Text) & " was not found in the database. " _
                & vbCr & "If the name has changed, delete the old name from the left list and drag in the new name.")
                TabControl1.TabIndex = TabControl1.TabPages.IndexOf(tpProgList)
                Return False
                Exit Function
            Else
                pe.ResetProgramName(Me._jobNumber & "." & Trim(tvProgList.Nodes(i).Text), Me._framesDb)
                If tvProgList.Nodes(i).Nodes.Count > 0 Then
                    Dim nd As TreeNode
                    For Each nd In tvProgList.Nodes(i).Nodes
                        pe = Me.frames.FindPartEdge(Me._jobNumber & "." & Trim(nd.Text))
                        pe.ResetProgramName(Me._jobNumber & "." & Trim(tvProgList.Nodes(i).Text), Me._framesDb)
                    Next
                End If
            End If
        Next
        Dim outFile As String = Module1.RazorGageOutputDirectory & Me._jobNumber & ".csv"
        Me.frames.SaveToCsv(outFile, Module1.BarcodeDirectory)
        Return True
    End Function

    Private Function WriteProgList2Xml() As Boolean
        If System.IO.File.Exists(Windows.Forms.Application.StartupPath & "\ProgListTempl.xml") Then
            'TODO: alert user if file not found.
            Dim xtr As XmlTextReader = New XmlTextReader(Windows.Forms.Application.StartupPath & "\ProgListTempl.xml")
            Dim xd As XmlDocument = New XmlDocument
            Try
                xd.Load(xtr)
            Finally
                xtr.Close()
            End Try

            Dim xnodRoot As Xml.XmlNode
            xnodRoot = xd.DocumentElement '.Item("ProgramList")
            Dim xElem As XmlElement
            For i As Integer = 0 To tvProgList.GetNodeCount(False) - 1
                xElem = xd.CreateElement("ListItem")
                xElem.SetAttribute("name", tvProgList.Nodes(i).Text)
                If tvProgList.Nodes(i).Nodes.Count > 0 Then
                    For Each nd As TreeNode In tvProgList.Nodes(i).Nodes
                        Dim subNd As XmlElement = xd.CreateElement("SubItem")
                        subNd.InnerText = nd.Text
                        xElem.AppendChild(subNd)
                    Next
                End If
                xnodRoot.InsertAfter(xElem, xnodRoot.LastChild)
            Next
            Dim xmlfilename As String = Module1.ProgramOutputDirectory & Me._jobNumber & "\ProgramList.xml"
            'If System.IO.File.Exists(xmlfilename) Then
            '  File.Delete(xmlfilename)
            'End If
            Try
                xd.Save(xmlfilename)
            Catch ex As Exception
                MsgBox("Save xml file, " & xmlfilename & ", failed")
            End Try
            Return True
        Else
            Return False
        End If
    End Function

    Private Sub LoadProgramlist()
        Dim xmlfilename As String = Module1.ProgramOutputDirectory & Me._jobNumber & "\ProgramList.xml"
        If System.IO.File.Exists(xmlfilename) Then
            Dim xtr As XmlTextReader = New XmlTextReader(xmlfilename)
            Dim xd As XmlDocument = New XmlDocument
            Try
                xd.Load(xtr)
            Finally
                xtr.Close()
            End Try
            Dim xnodRoot As XmlNode = xd.DocumentElement
            If xnodRoot.ChildNodes.Count > 0 Then
                Dim nd As XmlNode
                Dim tvNd As TreeNode
                For Each nd In xnodRoot.ChildNodes
                    tvNd = New TreeNode(nd.Attributes("name").Value.ToString)
                    Dim fp As FramePart = Me.frames.GetSinglePartByPartedgename(Me._jobNumber & "." & Trim(tvNd.Text))
                    If fp IsNot Nothing Then
                        tvNd.ToolTipText = fp.PartEdges(0).Comment & " | TS Comment: " & fp.TSComments
                    End If
                    If nd.HasChildNodes Then
                        Dim cnd As XmlNode
                        For Each cnd In nd.ChildNodes
                            Dim tvchild As New TreeNode(cnd.InnerText)
                            fp = Me.frames.GetSinglePartByPartedgename(Me._jobNumber & "." & Trim(tvchild.Text))
                            If fp IsNot Nothing Then
                                tvchild.ToolTipText = fp.PartEdges(0).Comment & " | TS Comment: " & fp.TSComments
                            End If
                            tvNd.Nodes.Add(tvchild)
                        Next
                    End If
                    tvProgList.Nodes.Add(tvNd)
                Next
            End If
            tvProgList.ExpandAll()
            Me._progViewChanged = True
        End If
    End Sub

    Private Sub btnSaveProgList_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSaveProgList.Click
        WriteProgList2Xml()
        UpdateProgramNames()
    End Sub

#End Region '"Treeview Part Program List ======"

#Region "Print Barcodes ======"

    Private Function GetProgramName(ByVal nodename As String) As String
        Dim fn As String
        Dim strs() As String
        strs = nodename.Split(CChar("."))
        fn = Me._framesDb.GetPartEdgeProgname(Me._jobNumber & "." & strs(0) & "." & strs(1))
        Return Module1.BarcodeDirectory & Me._jobNumber & "\" & fn
    End Function

    Private Sub New_btnPrintBarcode_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuPrintBarcodeReport.Click
        If tvProgList.GetNodeCount(False) > 0 Then
            WriteProgList2Xml()
            'Me._progViewChanged = Not 
            UpdateProgramNames()
            Dim reportList As New ArrayList
            For i As Integer = 0 To tvProgList.GetNodeCount(False) - 1
                Dim repNodeObj As New ReportNodeData
                repNodeObj.name = Trim(tvProgList.Nodes(i).Text)
                repNodeObj.program = GetProgramName(Trim(tvProgList.Nodes(i).Text))
                If tvProgList.Nodes(i).Nodes.Count > 0 Then
                    repNodeObj.Childnodes = New ArrayList
                    Dim nd As TreeNode
                    For Each nd In tvProgList.Nodes(i).Nodes
                        repNodeObj.Childnodes.Add(Trim(nd.Text))
                    Next
                End If
                reportList.Add(repNodeObj)
            Next
            Dim repJobObj As New ReportJobData
            repJobObj.jobNo = Me._jobNumber
            repJobObj.repTitle = "Job Number: " & Me._jobNumber
            repJobObj.repSubtitle = "Style: " & GetFrameStylename() & vbTab & "   Hinge: " & GetHingeStylename() & vbTab & "   Thichness: " & GetThickStylename()
            Dim pc As New PrintClass
            pc.PrintBarcodeReport(repJobObj, reportList)

        Else
            MsgBox("There needs to be at least one item in the list to print.")
        End If
    End Sub

    Private Sub mnuPrintBarcodeReport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Me.New_btnPrintBarcode_Click(sender, e)
    End Sub

#End Region 'Print Barcodes ======"

#Region "Custom Events and Delegates ======"

    Public Sub OnMainFormShowing(ByVal Sender As Object, ByVal e As EventArgs)
        RaiseEvent MainFormShowing(Me, e)
    End Sub

    Private Sub MainForm_Closing(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles MyBase.Closing
        If Me._progViewChanged AndAlso tvProgList.GetNodeCount(True) > 0 Then
            WriteProgList2Xml()
            If Not UpdateProgramNames() Then
                e.Cancel = True
            End If
            Me._progViewChanged = False
        End If
    End Sub

    Private Sub MainForm_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Activated
        OnMainFormShowing(Me, e)
    End Sub
#End Region  'Custom Events and Delegates

#Region "Custom Frame Opening and Parts ======"

    Private Sub btnCustOpg_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCustOpg.Click
        Dim dlg As New dlgCustomOpening(New HingeRule(Module1.JobHingeRule))

        If dlg.ShowDialog() = DialogResult.OK Then
            Dim ps As New FramePartBaseClass(Me.frames.CurrentFrame.Name & ".OP-CUST")
            'ps.ItemNo = Trim(CStr(comboItems.SelectedItem))
            ps.Width = CDec(dlg.txtOpgWdth.Text)
            ps.Length = CDec(dlg.txtOpgHght.Text)
            ps.Thickness = 0
            Dim fp As FramePart = Me.frames.CurrentFrame.CreatePart(ps, HingePlacement.N)
            fp.HingeStyleRules = dlg.HingeRules
            If dlg.checkSpecRules.Checked Then
                fp.HingeStyleRules = dlg.HingeRules
                fp.Hinging = dlg.HingePlace(fp.Hinging)
            End If
            RefreshPartsLists()
        End If
    End Sub

    Private Sub btnEditOpg_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEditOpg.Click
        Dim dlg As dlgCustomOpening
        Dim fp As FramePart = Nothing
        If lbOpenings.SelectedIndex > -1 Then
            Dim ptCount As Integer = Me.frames.CurrentFrame.Openinglist.Count
            For i As Integer = 0 To ptCount - 1
                If CType(Me.frames.CurrentFrame.Openinglist.Item(i), FramePart).Name = CType(lbOpenings.SelectedItem, FramePart).Name Then
                    fp = CType(Me.frames.CurrentFrame.Openinglist(i), FramePart)
                    Exit For
                End If
            Next
            dlg = New dlgCustomOpening(fp.HingeStyleRules)
            Dim psz As FrameSize = fp.FramePartSize
            dlg.txtOpgHght.Text = psz.Length.ToString
            dlg.txtOpgWdth.Text = psz.Width.ToString
            Dim hr As New HingeRule(fp.HingeStyleRules)
            dlg.HingeRules = hr
            dlg.HingePlace(Nothing) = fp.Hinging

            If dlg.ShowDialog() = DialogResult.OK Then
                psz.Width = CDec(dlg.txtOpgWdth.Text)
                psz.Length = CDec(dlg.txtOpgHght.Text)
                'fp.Description = psz.name & " " & psz.width & " X " & psz.length
                'Me.ptSize.name & " " & ptSize.width & " X " & ptSize.length
                fp.FramePartSize = psz
                If dlg.checkSpecRules.Checked Then
                    fp.HingeStyleRules = dlg.HingeRules
                    fp.Hinging = dlg.HingePlace(fp.Hinging)
                End If
                If Me.frames.CurrentPartIndex > -1 Then
                    Dim pt As FrontFrameEventClasses.PartEdgeTypes = CType(System.Enum.Parse(GetType(FrontFrameEventClasses.PartEdgeTypes), _
                                                                                          CStr(comboPartType.SelectedItem)),  _
                                                                                          FrontFrameEventClasses.PartEdgeTypes)
                    ptCount = Me.frames.CurrentPart.AdjoiningPartNameList(pt).Count
                    For i As Integer = 0 To ptCount - 1
                        If Me.frames.GetSinglePart(CStr(Me.frames.CurrentPart.AdjoiningPartNameList(pt)(i))).Name = fp.Name Then
                            'CType(CType(frameList(currFrame).Partslist(currFramePart), FramePart).AdjoiningPartlist(pt)(i), FramePart).ModifyPart(fp)
                            Me.frames.GetSinglePart(CStr(Me.frames.CurrentPart.AdjoiningPartNameList(pt)(i))).ModifyPart(fp)
                            Exit For
                        End If
                    Next
                End If
                RefreshPartsLists()
            End If
        End If

    End Sub

    Private Sub btnEditPart_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEditPart.Click
        EditCustomPart(False)

    End Sub

    Private Sub btnNewCustPart_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNewCustPart.Click
        EditCustomPart(True)
    End Sub

    Private Sub btnNewFrame_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNewFrame.Click
        Dim itemNo As String = (comboItems.Items.Count + 1).ToString("#00")
        comboItems.Items.Add(itemNo)
        Me.frames.AddFrame2List(New FrontFrame(Me._framesDb, Me._jobNumber, itemNo, Me.frames.CurrentFrame.WoodSpecie))
        comboItems.SelectedIndex = comboItems.Items.Count - 1
    End Sub

    Private Function EditCustomPart(ByVal newPart As Boolean) As FramePart
        Dim dlg As dlgEditRailParts = Nothing
        Dim ptCount As Integer = Me.frames.CurrentFrame.PartsList.Count

        If newPart Then
            dlg = New dlgEditRailParts(Me.frames.CurrentFrame, Me.frames.CurrentFrame.ItemNo)
        Else
            If lbStilesRails.SelectedIndex > -1 Then
                dlg = New dlgEditRailParts(CType(lbStilesRails.SelectedItem, FramePart))
            End If
        End If

        Dim fp As FramePart = Nothing
        If dlg.ShowDialog() = DialogResult.OK Then
            fp = dlg.Part
            fp.SaveToDB(False)
            RefreshPartsLists()
            Dim outFile As String = Module1.RazorGageOutputDirectory & Me._jobNumber & ".csv"
            Me.frames.SaveToCsv(outFile, Module1.BarcodeDirectory)
            Me._progViewChanged = True
            If newPart Then
                Dim retriever As New JobRetrieverClass
                Me.lbCabList.Items.Clear()
                Me.lbCabList.Items.AddRange(retriever.GetCabinetPartList(Me.frames).ToArray)
                Me.SetTabControlPages(False)
            End If
        End If
        Return fp
    End Function

#End Region '"Custom Frame Opening and Parts ======"

#Region "WebBrowsers Methods ====="

    Private Sub toolBtnCatalog_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles toolBtnCatalog.Click
        If TabControl1.TabPages.IndexOf(tpCatalog) = -1 Then
            TabControl1.TabPages.Add(Me._myPages(Me._myPages.IndexOf(tpCatalog)))
        End If
        NavigateToTopPage(Me.browserCatalog)
        TabControl1.SelectedIndex = TabControl1.TabPages.IndexOf(tpCatalog)
    End Sub

    Private Sub toolBtnJobdetails_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles toolBtnJobdetails.Click
        'http://qccnt1/qccasp/joboffice.asp?mtext=220400000
        'http://qccnt1/qccasp/joboffice.asp?mjobno=213520000&mjobkey=275907&mtype=jobdtl&mtime=current
        'Dim jobofficeasp As String = ReadFromXml(ParmFilename, "/SetupParameters/Directories/Directory[@name='Joboffice.aspUrl']")
        'Dim url As String = jobofficeasp & "?mtext=" & Me.JobNumber & "&mtype=job&mtime=current"
        'Process.Start(url)

        If (Me._jobNumber.Length = 9) AndAlso TabControl1.TabPages.IndexOf(tpJobDetail) = -1 Then
            TabControl1.TabPages.Add(Me._myPages(Me._myPages.IndexOf(tpJobDetail)))
        End If
        NavigateToTopPage(Me.browserJobDetail)
        TabControl1.SelectedIndex = TabControl1.TabPages.IndexOf(tpJobDetail)
    End Sub

    Private Sub toolBtnNavBack_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles toolBtnNavBack.Click
        Select Case TabControl1.SelectedIndex
            Case TabControl1.TabPages.IndexOf(tpNewStart)
                If Me.browserSchedule.CanGoBack Then
                    Me.browserSchedule.GoBack()
                End If
            Case TabControl1.TabPages.IndexOf(tpJobDetail)
                If Me.browserJobDetail.CanGoBack Then
                    Me.browserJobDetail.GoBack()
                End If
            Case TabControl1.TabPages.IndexOf(tpCatalog)
                If Me.browserCatalog.CanGoBack Then
                    Me.browserCatalog.GoBack()
                End If
            Case TabControl1.TabPages.IndexOf(tpSelect)
                If Me.browserJobHeader.CanGoBack Then
                    Me.browserJobHeader.GoBack()
                End If
        End Select
    End Sub

    Private Sub toolBtnNavForward_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles toolBtnNavForward.Click
        Select Case TabControl1.SelectedIndex
            Case TabControl1.TabPages.IndexOf(tpNewStart)
                If Me.browserSchedule.CanGoForward Then
                    Me.browserSchedule.GoForward()
                End If
            Case TabControl1.TabPages.IndexOf(tpJobDetail)
                If Me.browserJobDetail.CanGoForward Then
                    Me.browserJobDetail.GoForward()
                End If
            Case TabControl1.TabPages.IndexOf(tpCatalog)
                If Me.browserCatalog.CanGoForward Then
                    Me.browserCatalog.GoForward()
                End If
            Case TabControl1.TabPages.IndexOf(tpSelect)
                If Me.browserJobHeader.CanGoForward Then
                    Me.browserJobHeader.GoForward()
                End If
        End Select
    End Sub

    Private Sub toolBtnNavHome_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles toolBtnNavHome.Click
        Select Case TabControl1.SelectedIndex
            Case TabControl1.TabPages.IndexOf(tpNewStart)
                NavigateToTopPage(Me.browserSchedule)
            Case TabControl1.TabPages.IndexOf(tpJobDetail)
                NavigateToTopPage(Me.browserJobDetail)
            Case TabControl1.TabPages.IndexOf(tpCatalog)
                NavigateToTopPage(Me.browserCatalog)
            Case TabControl1.TabPages.IndexOf(tpSelect)
                NavigateToTopPage(Me.browserJobHeader)
        End Select
    End Sub

    'jobofficeasp arguments:
    '   "mjobno" must be followed by &mjobkey although &mjobkey can be assigned a wildcard "*"
    'http://ecatalog.qcc.com/Collections.aspx?ProductLineId=1

    Private Sub NavigateToTopPage(ByVal sender As WebBrowser)
        Dim url As String = String.Empty
        If sender Is Me.browserJobDetail Then
            url = Module1.JobofficeAspUrl & Me._jobNumber & "&mjobkey=*&mtype=jobdtl&mtime=current"
        ElseIf sender Is Me.browserCatalog Then
            url = Module1.eCatalogUrl
            '"http://ecatalog.qcc.com/Collections.aspx?ProductLineId=1"
        ElseIf sender Is Me.browserSchedule Then
            Me.GetSchedule()
            Exit Sub
        End If
        sender.Navigate(url)
    End Sub

    Private Sub browser_CanGoChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles browserJobDetail.CanGoBackChanged, browserJobDetail.CanGoForwardChanged _
                                                                                                          , browserCatalog.CanGoBackChanged, browserCatalog.CanGoForwardChanged _
                                                                                                          , browserJobHeader.CanGoBackChanged, browserJobHeader.CanGoForwardChanged _
                                                                                                          , browserSchedule.CanGoBackChanged, browserSchedule.CanGoForwardChanged
        Dim bwsr As WebBrowser = CType(sender, WebBrowser)
        Me.toolBtnNavBack.Enabled = bwsr.CanGoBack
        Me.toolBtnNavForward.Enabled = bwsr.CanGoForward
        If bwsr.Url Is Nothing Then
            Me.sbPanelMessage.Text = ""
            Me.sbPanelMessage.ToolTipText = Me.sbPanelMessage.Text
        Else
            Me.sbPanelMessage.Text = bwsr.Url.ToString
            Me.sbPanelMessage.ToolTipText = Me.sbPanelMessage.Text
        End If
    End Sub

    Private Sub browser_Navigated(ByVal sender As Object, ByVal e As System.Windows.Forms.WebBrowserNavigatedEventArgs) Handles browserJobDetail.Navigated, browserCatalog.Navigated, browserJobHeader.Navigated, browserSchedule.Navigated
        Me.sbPanelMessage.Text = CType(sender, WebBrowser).Url.ToString
    End Sub

    Private Sub toolBtnPrintBrowser_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles toolBtnPrintBrowser.Click
        Select Case TabControl1.SelectedIndex
            Case TabControl1.TabPages.IndexOf(tpNewStart)
                Me.browserSchedule.Print()
            Case TabControl1.TabPages.IndexOf(tpJobDetail)
                Me.browserCatalog.Print()
            Case TabControl1.TabPages.IndexOf(tpCatalog)
                Me.browserCatalog.Print()
            Case TabControl1.TabPages.IndexOf(tpSelect)
                Me.browserJobHeader.Print()
        End Select
    End Sub

    Private Sub browserSchedule_Navigating(ByVal sender As System.Object, ByVal e As System.Windows.Forms.WebBrowserNavigatingEventArgs) Handles browserSchedule.Navigating
        Dim hrefJobno As String = Regex.Match(e.Url.ToString, "(?!mjobno=)([A-Za-z0-9]{9})(?=&mjobkey)").Value
        If hrefJobno IsNot Nothing AndAlso hrefJobno <> String.Empty Then
            e.Cancel = True
            Thread.Sleep(500)
            GetJobInformation(hrefJobno)
        End If
    End Sub

    Private Sub GetJobInformation(ByVal jobNum As String)
        Me.labelStatusJobNo.Text = Regex.Replace(jobNum, "([A-Za-z0-9]{5})([A-Za-z0-9]{2})([A-Za-z0-9]{2})", "$1-$2-$3")
        Dim url As String = Module1.JobofficeAspUrl & jobNum & "&mjobkey=*&mtype=jobhdr&mtime=current"
        Me.browserJobHeader.Navigate(url)
        Me.ActivateSelectTabpage(jobNum)
    End Sub

    Private Sub browserJobHeader_DocumentCompleted(ByVal sender As Object, ByVal e As System.Windows.Forms.WebBrowserDocumentCompletedEventArgs) Handles browserJobHeader.DocumentCompleted
        Dim elem As HtmlElement = Me.browserJobHeader.Document.Body
        If Regex.IsMatch(elem.InnerText, "(NOT FOUND!!!)") Then
            Me._BOM_Exists = False
        Else
            Me._BOM_Exists = True
        End If
        Me.radioGenerateCSV.Enabled = Me._BOM_Exists
        Me.radioImportTemplateCSV.Enabled = Not Me._BOM_Exists
    End Sub

#End Region   '"WebBrowsers Methods ====="

#Region "Department Schedule Methods"

    Private Sub GetSchedule()
        '(?!lcdatefrom=)\d{2}.{12}(?=&lcdatethru)
        Dim urltextSchedule As String = Regex.Replace("http://qccnt1/shopasp/shipshop.asp?lcdatefrom=03%2F15%2F2012&lcdatethru=04%2F12%2F2012&lcwc=FT&mtype=shopsch&I1.x=28&I1.y=9", _
                                              "(?!lcdatefrom=)\d{2}.{12}(?=&lcdatethru)", _
                                              Me.datePickFilterStart.Value.ToShortDateString)
        '(?!&lcdatethru)\d{2}.{12}(?=&lcwc)
        urltextSchedule = Regex.Replace(urltextSchedule, "(?!&lcdatethru)\d{2}.{12}(?=&lcwc)", Me.datePickFilterEnd.Value.ToShortDateString)
        Me.browserSchedule.Navigate(New Uri(urltextSchedule))
    End Sub

    Private Sub btnRefreshSchedule_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRefreshSchedule.Click
        Me.GetSchedule()
    End Sub

#End Region   '"Department Schedule Methods")

#Region "Estimated Materials Methods"

    Private Sub mnuMaterialRequirements_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuMateralRequirements.Click
        With New FormMaterialReportWasteFactor(Me.frames.MaterialDimensionList)
            .trackWasteFactor.Value = My.Settings.MaterialWasteFactor
            If .ShowDialog = Windows.Forms.DialogResult.OK Then
                My.Settings.MaterialWasteFactor = .trackWasteFactor.Value
                My.Settings.Save()
                Dim register As New FrameMaterialRegister(.ReportMaterialList)
                register.Calculate(Me.frames)
                register.ReportTitle = "Job #" & Me._jobNumber
                register.ReportSubtitle = "Front Frame Material:  " & Me.frames.CurrentFrame.WoodSpecie
                Dim report As New MaterialRequirementsReport(register, CSng(.trackWasteFactor.Value / 100))
                report.PrintMaterialRequirements()
            End If
        End With
    End Sub

    Private Function MaterialRegister_ExistsAt(ByVal name As String, ByVal list As List(Of FrameMaterialClass)) As Integer
        For index As Integer = 0 To list.Count - 1
            If list.Item(index).MaterialName = name Then
                Return index
            End If
        Next
        Return -1
    End Function
#End Region ' "Estimated Materials Methods"

End Class
