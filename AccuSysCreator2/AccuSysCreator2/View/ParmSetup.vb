Imports System.IO
Imports System.Xml
Imports System.DirectoryServices

''' <summary>
''' File: ParmSetup.vb
''' Author: Galen Newswanger
''' 
''' This class is a subclass of Windows Form and provides a UI for adding and editing 
''' instances of the style classes, paths to file locations, and other parameters
''' the application consumes.
''' </summary>
''' <remarks></remarks>
Public Class ParmSetup

#Region "Private Instance Variables"
    Private _isModified As Boolean
    Private _isNew As Boolean
    Private _panelAddNew As PanelAddNewClass
    Private _tcpages(7) As TabPage
    Private _modifiedPage As TabPage
    Private _modifiedPageIndex As Integer
    Private _dirList As DirectoryList
    Private _db As DataClass
    '#End Region

    '#Region "Public Instance Variables"
    Private allowModifyDefaults As Boolean
    Private opParms As OperationParms
    Private toolGeomery As MachineTool
    Private feedrates As OperationParms
    Private styleFS As FrameStyle
    Private styleHng As Hinge
    Private ruleHng As HingeRule
    Private styleThk As Thickness
#End Region

#Region "Constructor and Initialization"

    Public Sub New()
        ' This call is required by the designer.
        InitializeComponent()
        ' Add any initialization after the InitializeComponent() call.
        Me._db = New DataClass(ParmFilename)
    End Sub

    Private Sub ParmSetup_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'parmFilename = parmFilename
        For i As Integer = 0 To TabControl1.TabCount - 1
            Me._tcpages(i) = TabControl1.TabPages(i)
        Next
        LoadComboLists()
        CreatePanelAddNew()
        TabControl1_SelectedIndexChanged(Nothing, Nothing)
        Me._isNew = False
        Me._isModified = False
    End Sub

    Private Sub LoadComboLists()
        comboPlaceRule.Items.Clear()
        comboPlaceRule.Items.AddRange(GetElementAttrList("HingePlacementRules", "name").ToArray)
        comboHingeStyle.Items.Clear()
        comboHingeStyle.Items.AddRange(GetElementAttrList("HingeType", "name").ToArray)
        comboHingePlacement.Items.Clear()
        comboHingePlacement.Items.AddRange(GetElementAttrList("HingePlacementRules", "name").ToArray)
        comboFrameStyle.Items.Clear()
        comboFrameStyle.Items.AddRange(GetElementAttrList("FrameStyles", "name").ToArray)
        comboThicknessStyle.Items.Clear()
        comboThicknessStyle.Items.AddRange(GetElementAttrList("ThicknessParms", "name").ToArray)
        comboLibComputer.Items.Clear()
        comboLibComputer.Items.AddRange(GetActiveComputerList.ToArray)
        Me.comboFeedrateSpecie.Items.Clear()
        Me.comboFeedrateSpecie.Items.AddRange(Me._db.GetToolFeedrateSpecieNames.ToArray)
    End Sub

    Private Function GetElementAttrList(ByVal elementName As String, ByVal attrName As String) As ArrayList
        Dim retList As New ArrayList
        Dim xtr As XmlTextReader = New XmlTextReader(ParmFilename)
        Try
            Dim xd As XmlDocument = New XmlDocument
            xd.Load(xtr)
            Dim xnodRoot As XmlNode = xd.DocumentElement.Item(elementName)
            Dim xnodWorking As XmlNode
            If xnodRoot.HasChildNodes Then
                xnodWorking = xnodRoot.FirstChild
                While Not IsNothing(xnodWorking)
                    retList.Add(xnodWorking.Attributes(attrName).Value)
                    xnodWorking = xnodWorking.NextSibling
                End While
            End If
            Return retList
        Catch ex As XmlException
            MessageBox.Show(ParmFilename & vbCr & ex.Message & vbCr & ex.LineNumber & vbCr & ex.LinePosition)
        Finally
            xtr.Close()
        End Try
        Return Nothing
    End Function

    Private Function GetActiveComputerList() As ArrayList
        Dim cList As New ArrayList
        Dim enTry As DirectoryEntry = New DirectoryEntry("LDAP://Office")
        Dim mySearcher As DirectorySearcher = New DirectorySearcher(enTry)
        mySearcher.Filter = ("(objectClass=computer)")
        Dim resEnt As SearchResult
        For Each resEnt In mySearcher.FindAll()
            Dim cmptr As New System.DirectoryServices.DirectoryEntry(resEnt.Path)
            cList.Add(cmptr.Properties("cn").Item(0))
            cmptr = Nothing
        Next
        cList.Sort()
        Return cList
    End Function
#End Region

    Private Sub btnOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOK.Click
        If Me._isModified Then
            If TabControl1.SelectedTab.Contains(Me._panelAddNew) Then
                TabControl1.SelectedTab.Controls.Remove(Me._panelAddNew)
            End If
            ProcessChanges(Me._isNew)
        End If
    End Sub

    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        If Me._isModified Then
            Me._isNew = False
            Me._isModified = False
            TabControl1.SelectedTab.Controls.Remove(Me._panelAddNew)
            TabControl1.TabPages.RemoveAt(TabControl1.SelectedIndex)
            UpdateTabs()
        Else
            Me.DialogResult = DialogResult.Cancel
            Me.Close()
        End If
        EnableTabpageEdit(False)
        SetPageData()
        Panel1.Invalidate()
    End Sub

    Private Sub btnApply_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnApply.Click
        If Me._isModified Then
            If TabControl1.SelectedTab.Contains(Me._panelAddNew) Then
                TabControl1.SelectedTab.Controls.Remove(Me._panelAddNew)
            End If
            ProcessChanges(Me._isNew)
            LoadComboLists()
        End If
        Me._isNew = False
        Me._isModified = False
        UpdateTabs()
        EnableTabpageEdit(False)
    End Sub

    Private Sub buttonAddEdit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEdit.Click, btnAddNew.Click
        Me._isNew = (CType(sender, Button).Name = "btnAddNew")
        SetEditMode()
    End Sub

    Private Sub SetEditMode()
        If Not TabControl1.SelectedTab Is tpOperationParms And Not TabControl1.SelectedTab Is tpLibDir Then
            TabControl1.SelectedTab.Controls.Add(Me._panelAddNew)
            Select Case TabControl1.SelectedIndex
                Case TabControl1.TabPages.IndexOf(tpHingeStyle)
                    Me._panelAddNew.Size = New System.Drawing.Size(600, 36)
                    Me._panelAddNew.txtNewItemName.Text = CStr(comboHingeStyle.SelectedItem)
                    Me._panelAddNew.txtNewDesc.Text = txtStyleDesc.Text
                Case TabControl1.TabPages.IndexOf(tpHingePlacement)
                    Me._panelAddNew.Size = New System.Drawing.Size(235, 36)
                    Me._panelAddNew.txtNewItemName.Text = CStr(comboHingePlacement.SelectedItem)
                    Me._panelAddNew.txtNewDesc.Text = ""
                Case TabControl1.TabPages.IndexOf(tpFrameStyle)
                    Me._panelAddNew.Size = New System.Drawing.Size(600, 36)
                    Me._panelAddNew.txtNewItemName.Text = CStr(comboFrameStyle.SelectedItem)
                    Me._panelAddNew.txtNewDesc.Text = txtFrameStyleDesc.Text
                Case TabControl1.TabPages.IndexOf(tpThicknessStyle)
                    Me._panelAddNew.Size = New System.Drawing.Size(600, 36)
                    Me._panelAddNew.txtNewItemName.Text = CStr(comboThicknessStyle.SelectedItem)
                    Me._panelAddNew.txtNewDesc.Text = txtThickStyleDesc.Text
                Case TabControl1.TabPages.IndexOf(tpToolFeedrates)
                    Me._panelAddNew.Size = New System.Drawing.Size(600, 36)
                    Me._panelAddNew.txtNewItemName.Text = CStr(comboFeedrateSpecie.SelectedItem)
                    Me._panelAddNew.txtNewDesc.Text = textFeedrateSpecieDesc.Text
            End Select
            If Me._isNew Then
                Me._panelAddNew.TextReadOnly = False
                Me._panelAddNew.txtNewItemName.Text = ""
                Me._panelAddNew.txtNewDesc.Text = ""
            Else
                Me._panelAddNew.TextReadOnly = True
            End If
            Me._panelAddNew.BringToFront()
        End If
        btnApply.Visible = True
        Me._isModified = True
        UpdateTabs()
        EnableTabpageEdit(True)
    End Sub

    Private Sub CreatePanelAddNew()
        Me._panelAddNew = New PanelAddNewClass
    End Sub

    Private Sub TabControl1_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles TabControl1.SelectedIndexChanged
        Panel1.Invalidate()
        SetPageData()
        If TabControl1.SelectedTab Is tpOperationParms Then
            SetTpOperationParms()
        ElseIf TabControl1.SelectedTab Is tpLibDir Then
            SetTpLibDir()
        End If
    End Sub

    Private Sub EnableTabpageEdit(ByVal enableEdit As Boolean)
        Dim pnl As Panel = Nothing
        For i As Integer = 0 To TabControl1.SelectedTab.Controls.Count - 1
            If TabControl1.SelectedTab.Controls(i).Name.StartsWith("pnlControls") Then
                pnl = CType(TabControl1.SelectedTab.Controls(i), Panel)
                Exit For
            End If
        Next
        If Not pnl Is Nothing Then
            For i As Integer = 0 To pnl.Controls.Count - 1
                If pnl.Controls(i).GetType Is GetType(TextBox) Then
                    CType(pnl.Controls(i), TextBox).ReadOnly = Not enableEdit
                ElseIf pnl.Controls(i).GetType Is GetType(ComboBox) Then
                    If enableEdit Then
                        CType(pnl.Controls(i), ComboBox).Enabled = True
                    Else
                        CType(pnl.Controls(i), ComboBox).Enabled = False
                        CType(pnl.Controls(i), ComboBox).ForeColor = Drawing.SystemColors.WindowText
                    End If
                ElseIf pnl.Controls(i).GetType Is GetType(Button) Or pnl.Controls(i).GetType Is GetType(CheckBox) Then
                    CType(pnl.Controls(i), Control).Enabled = enableEdit
                End If
            Next
        End If
    End Sub

    Private Sub UpdateTabs()
        If Me._isModified Then
            TabControl1.SuspendLayout()
            Me._modifiedPageIndex = TabControl1.SelectedIndex
            Me._modifiedPage = TabControl1.SelectedTab
            TabControl1.TabPages.Clear()
            TabControl1.TabPages.Add(Me._tcpages(Me._modifiedPageIndex))

            TabControl1.ResumeLayout()
        Else
            TabControl1.SuspendLayout()
            TabControl1.TabPages.Clear()
            For i As Integer = 0 To Me._tcpages.GetUpperBound(0)
                If Not TabControl1.TabPages.Contains(Me._tcpages(i)) Then
                    TabControl1.TabPages.Add(Me._tcpages(i))
                End If
            Next
            TabControl1.SelectedIndex = Me._modifiedPageIndex
            TabControl1.ResumeLayout()
        End If

    End Sub

    Private Sub SetTpOperationParms()
        If IsNothing(opParms) Then
            opParms = Me._db.GetOperationParmsClass("Default")
            comboTenonMode.SelectedIndex = opParms.tenonMode.GetHashCode
            comboMiddleRail.SelectedIndex = opParms.middleRail.GetHashCode
            txtHDepthAdj.Text = opParms.haunchDepthAdj.ToString
            txtTDepthAdj.Text = opParms.tenonDepthAdj.ToString
            txtNumClamps.Text = opParms.numClamps.ToString
            txtMaxLength2Clamps.Text = opParms.maxLen2Clamps.ToString
            txtMinMortMove.Text = opParms.minMortiseToolMove.ToString
            txtMinMortShoulder.Text = opParms.minMortiseShoulder.ToString
            Me.toolGeomery = New MachineTool(ParmFilename)
            txtHaunchToolBottom.Text = Me.toolGeomery.HaunchToolBottom.ToString
            txtMortiseToolDia.Text = Me.toolGeomery.MortiseToolDiameter.ToString
            txtDrillDiameter.Text = Me.toolGeomery.PilotToolDiameter.ToString
        End If
    End Sub

    Private Sub SetTpLibDir()
        If IsNothing(Me._dirList) Then
            Me._dirList = New DirectoryList(ParmFilename)
            txtDataStore.Text = Me._dirList.dataStore
            txtDataOutput.Text = Me._dirList.dataOutput
            txtDataExecutable.Text = Me._dirList.dataExecutable
            comboLibComputer.SelectedIndex = comboLibComputer.Items.IndexOf(Me._dirList.accuSysLibComputer.ToUpper)
            txtAccuSysLib.Text = Me._dirList.accuSysLib
            txtAccuSysTenonLib.Text = Me._dirList.accuSysTenonLib
        End If
    End Sub

    Private Sub Panel1_Paint(ByVal sender As Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles Panel1.Paint
        btnAddNew.Visible = (TabControl1.SelectedTab Is tpFrameStyle _
                Or TabControl1.SelectedTab Is tpToolFeedrates _
                Or TabControl1.SelectedTab Is tpHingePlacement Or TabControl1.SelectedTab Is tpHingeStyle _
                Or TabControl1.SelectedTab Is tpFrameStyle Or TabControl1.SelectedTab Is tpHingePlacement _
                Or TabControl1.SelectedTab Is tpThicknessStyle) And (Not Me._isModified)
        btnEdit.Visible = ((TabControl1.SelectedTab Is tpFrameStyle AndAlso Me.styleFS IsNot Nothing) _
                Or (TabControl1.SelectedTab Is tpToolFeedrates AndAlso Me.comboFeedrateSpecie.Text <> "") _
                Or (TabControl1.SelectedTab Is tpHingePlacement AndAlso Me.ruleHng IsNot Nothing) _
                Or (TabControl1.SelectedTab Is tpHingeStyle AndAlso Me.styleHng IsNot Nothing) _
                Or (TabControl1.SelectedTab Is tpThicknessStyle AndAlso Me.styleThk IsNot Nothing) _
                Or (TabControl1.SelectedTab Is tpOperationParms AndAlso Me.opParms IsNot Nothing) _
                Or TabControl1.SelectedTab Is tpLibDir)
        btnDelete.Visible = btnEdit.Visible AndAlso Not (TabControl1.SelectedTab Is tpLibDir Or TabControl1.SelectedTab Is tpOperationParms)
        btnApply.Visible = Me._isModified
    End Sub

    Private Sub SetPageData()
        Select Case TabControl1.SelectedIndex
            Case TabControl1.TabPages.IndexOf(tpGeneral)
                SetGeneralData()
            Case TabControl1.TabPages.IndexOf(tpHingeStyle)
                SetHingeStyleData()
            Case TabControl1.TabPages.IndexOf(tpHingePlacement)
                SetHingePlaceData()
            Case TabControl1.TabPages.IndexOf(tpFrameStyle)
                SetFrameStyleData()
            Case TabControl1.TabPages.IndexOf(tpThicknessStyle)
                SetThicknessData()
            Case TabControl1.TabPages.IndexOf(tpOperationParms)

            Case TabControl1.TabPages.IndexOf(tpToolFeedrates)
                SetFeedrateData()

            Case TabControl1.TabPages.IndexOf(tpLibDir)

        End Select

    End Sub

    Private Sub comboHingeStyle_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles comboHingeStyle.SelectedIndexChanged
        SetHingeStyleData()
    End Sub

    Private Sub SetHingeStyleData()
        If comboHingeStyle.SelectedIndex > -1 Then
            styleHng = New Hinge(CType(comboHingeStyle.SelectedItem, String), ParmFilename)
            txtStyleDesc.Text = styleHng.Description
            comboIsHingeMortised.SelectedIndex = styleHng.isMortised.GetHashCode
            txtHingeMWidth.Text = styleHng.mortiseWidth.ToString
            txtHingeMDepth.Text = styleHng.mortiseDepth.ToString
            comboPlaceRule.SelectedIndex = comboPlaceRule.Items.IndexOf(styleHng.placementRule)
            comboIsPredrilled.SelectedIndex = styleHng.isPredrilled.GetHashCode
            txtBetweenPilots.Text = styleHng.distBetweenOuter.ToString
            txtPilotCenter.Text = styleHng.vCenterOuter.ToString
            txtPilotDepthDef.Text = styleHng.pilotDepth.ToString
            txtPilotDiaDef.Text = styleHng.pilotDia.ToString
            chkbxStyleHingeActive.Checked = styleHng.ActiveStyle
        Else
            txtStyleDesc.Text = ""
            comboIsHingeMortised.Text = ""
            txtHingeMWidth.Text = ""
            txtHingeMDepth.Text = ""
            comboPlaceRule.Text = ""
            comboIsPredrilled.Text = ""
            txtBetweenPilots.Text = ""
            txtPilotCenter.Text = ""
            txtPilotDepthDef.Text = ""
            txtPilotDiaDef.Text = ""
            chkbxStyleHingeActive.Checked = False
        End If
        Me.Panel1.Invalidate()
    End Sub

    Private Sub comboHingePlacement_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles comboHingePlacement.SelectedIndexChanged
        SetHingePlaceData()
    End Sub

    Private Sub SetHingePlaceData()
        If comboHingePlacement.SelectedIndex > -1 Then
            ruleHng = New HingeRule(CType(comboHingePlacement.SelectedItem, String), ParmFilename)
            txtHingeMOffset.Text = ruleHng.mortiseOffset.ToString
            txtSmallOpgOffset.Text = ruleHng.smallOpgMortOffset.ToString
            txtMinHght42Hinges.Text = ruleHng.minHght42Hinges.ToString
            txtHghtRange2Lower.Text = ruleHng.hghtRange42Lower.ToString
            txtHghtRange2Upper.Text = ruleHng.hghtRange42Upper.ToString
            txtHghtRange3Upper.Text = ruleHng.hghtRange43Upper.ToString
            txtHghtRange4Upper.Text = ruleHng.hghtRange44Upper.ToString
            chkbxHingeRuleActive.Checked = ruleHng.ActiveStyle
        Else
            txtHingeMOffset.Text = ""
            txtSmallOpgOffset.Text = ""
            txtMinHght42Hinges.Text = ""
            txtHghtRange2Lower.Text = ""
            txtHghtRange2Upper.Text = ""
            txtHghtRange3Upper.Text = ""
            txtHghtRange4Upper.Text = ""
            chkbxHingeRuleActive.Checked = False
        End If
        Me.Panel1.Invalidate()
    End Sub

    Private Sub comboFrameStyle_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles comboFrameStyle.SelectedIndexChanged
        SetFrameStyleData()
    End Sub

    Private Sub SetFrameStyleData()
        If comboFrameStyle.SelectedIndex > -1 Then
            styleFS = New FrameStyle(CType(comboFrameStyle.SelectedItem, String), ParmFilename)
            txtFrameStyleDesc.Text = styleFS.Description
            comboDoHaunch.SelectedIndex = styleFS.isHaunched.GetHashCode
            comboDoMortise.SelectedIndex = styleFS.isMortised.GetHashCode
            txtHaunchEndAdj.Text = styleFS.haunchEndAdj.ToString
            txtMortiseEndAdj.Text = styleFS.mortiseEndAdj.ToString
            txtHDepthDef.Text = styleFS.haunchDepth.ToString
            txtMDepthDef.Text = styleFS.mortiseDepth.ToString
            txtTenonHWidth.Text = styleFS.mortiseShoulder.ToString
            comboHaunchOnTenon.SelectedIndex = styleFS.haunchOnTenon.GetHashCode
            txtTenonLength.Text = styleFS.tenonDepth.ToString
            txtPilotCenterAdj.Text = styleFS.pilotCenterAdj.ToString
            chkbxStyleFrameActive.Checked = styleFS.ActiveStyle
        Else
            txtFrameStyleDesc.Text = ""
            comboDoHaunch.Text = ""
            comboDoMortise.Text = ""
            txtHaunchEndAdj.Text = ""
            txtMortiseEndAdj.Text = ""
            txtHDepthDef.Text = ""
            txtMDepthDef.Text = ""
            txtTenonHWidth.Text = ""
            comboHaunchOnTenon.Text = ""
            txtTenonLength.Text = ""
            txtPilotCenterAdj.Text = ""
            chkbxStyleFrameActive.Checked = False
        End If
        Me.Panel1.Invalidate()
    End Sub

    Private Sub comboThicknessStyle_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles comboThicknessStyle.SelectedIndexChanged
        SetThicknessData()
    End Sub

    Private Sub SetThicknessData()
        If comboThicknessStyle.SelectedIndex > -1 Then
            styleThk = New Thickness(CType(comboThicknessStyle.SelectedItem, String), ParmFilename)
            txtThickStyleDesc.Text = styleThk.Description
            txtBdHeight.Text = styleThk.BoardHeight.ToString
            txtToolCenter.Text = styleThk.toolLine.ToString
            txtRouteTopW.Text = styleThk.routeTopWidth.ToString
            txtRouteMidW.Text = styleThk.routeMiddleWidth.ToString
            txtRouteBotW.Text = styleThk.routeBottomWidth.ToString
            chkbxStyleThickActive.Checked = styleThk.ActiveStyle
        Else
            txtThickStyleDesc.Text = ""
            txtBdHeight.Text = ""
            txtToolCenter.Text = ""
            txtRouteTopW.Text = ""
            txtRouteMidW.Text = ""
            txtRouteBotW.Text = ""
            chkbxStyleThickActive.Checked = False
        End If
        Me.Panel1.Invalidate()
    End Sub

    Private Sub comboFeedrateSpecie_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles comboFeedrateSpecie.SelectedIndexChanged
        Me.SetFeedrateData()
    End Sub

    Private Sub SetFeedrateData()
        If Me.comboFeedrateSpecie.SelectedIndex > -1 Then
            Me.feedrates = Me._db.GetOperationParmsClass(Me.comboFeedrateSpecie.Text)
            Me.textFeedrateSpecieDesc.Text = Me.comboFeedrateSpecie.Text
            Me.textHaunchFeed.Text = Me.feedrates.haunchFeedrate.ToString
            Me.textMortiseFeed.Text = Me.feedrates.mortiseFeedrate.ToString
            Me.textTenonFeed.Text = Me.feedrates.tenonFeedrate.ToString
            Me.textDrillFeed.Text = Me.feedrates.drillFeedrate.ToString
        Else
            Me.SetEmptyTextboxToZero(Me.pnlControlsFeedrates)
        End If
        Me.Panel1.Invalidate()
    End Sub

    Private Sub SetGeneralData()
        txtSchedStartOffset.Value = Module1.SchedSearchStartOffset
        txtSchedEndOffset.Value = Module1.SchedSearchEndOffset
    End Sub

    Private Sub ProcessChanges(ByVal newRec As Boolean)
        Select Case TabControl1.SelectedTab.Name
            Case "tpHingePlacement"
                Dim hpr As HingeRule
                If newRec Then
                    'Dim ctlIndex = TabControl1.SelectedTab.Controls.GetChildIndex(Me._panelAddNew)
                    Dim objName As String
                    objName = Me._panelAddNew.txtNewItemName.Text
                    'objName = CType(CType(TabControl1.SelectedTab.Controls(ctlIndex), Panel).Controls(1), TextBox).Text
                    If objName = "" Then
                        MessageBox.Show("You must give the new record a unique name.", "Record Name", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Exit Select
                    Else
                        hpr = New HingeRule(objName, ParmFilename)
                    End If
                Else
                    If comboHingePlacement.SelectedIndex > -1 Then
                        hpr = New HingeRule(CType(comboHingePlacement.SelectedItem, String), ParmFilename)
                    Else
                        MessageBox.Show("You must select style first.", "Record Name", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Exit Select
                    End If
                End If
                Me.SetEmptyTextboxToZero(Me.pnlControls3)
                hpr.mortiseOffset = CSng(txtHingeMOffset.Text)
                hpr.smallOpgMortOffset = CSng(txtSmallOpgOffset.Text)
                hpr.minHght42Hinges = CSng(txtMinHght42Hinges.Text)
                hpr.hghtRange42Lower = CSng(txtHghtRange2Lower.Text)
                hpr.hghtRange42Upper = CSng(txtHghtRange2Upper.Text)
                hpr.hghtRange43Upper = CSng(txtHghtRange3Upper.Text)
                hpr.hghtRange44Upper = CSng(txtHghtRange4Upper.Text)
                hpr.ActiveStyle = CBool(chkbxHingeRuleActive.Checked)
                hpr.SaveToXml()
                comboPlaceRule.Items.Clear()
                comboPlaceRule.Items.AddRange(GetElementAttrList("HingePlacementRules", "name").ToArray)
                comboPlaceRule.SelectedIndex = comboPlaceRule.Items.IndexOf(hpr.Name)

            Case "tpHingeStyle"
                Dim hng As Hinge
                If newRec Then
                    'Dim ctlIndex = TabControl1.SelectedTab.Controls.GetChildIndex(Me._panelAddNew)
                    Dim objName As String
                    objName = Me._panelAddNew.txtNewItemName.Text
                    txtStyleDesc.Text = Me._panelAddNew.txtNewDesc.Text
                    'CType(CType(TabControl1.SelectedTab.Controls(ctlIndex), Panel).Controls(1), TextBox).Text()
                    If objName = "" Then
                        MessageBox.Show("You must give the new record a unique name.", "Record Name", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Exit Select
                    Else
                        hng = New Hinge(objName, ParmFilename)
                    End If
                Else
                    If comboHingeStyle.SelectedIndex > -1 Then
                        hng = New Hinge(CType(comboHingeStyle.SelectedItem, String), ParmFilename)
                    Else
                        MessageBox.Show("You must select style first.", "Record Name", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Exit Select
                    End If
                End If
                Me.SetEmptyTextboxToZero(Me.pnlControls1)
                hng.Description = Me._panelAddNew.txtNewDesc.Text
                hng.isMortised = CBool(comboIsHingeMortised.SelectedItem)
                hng.mortiseWidth = CSng(txtHingeMWidth.Text)
                hng.mortiseDepth = CSng(txtHingeMDepth.Text)
                hng.placementRule = CType(comboPlaceRule.SelectedItem, String)
                hng.isPredrilled = CBool(comboIsPredrilled.SelectedItem)
                hng.distBetweenOuter = CSng(txtBetweenPilots.Text)
                hng.vCenterOuter = CSng(txtPilotCenter.Text)
                hng.pilotDepth = CSng(txtPilotDepthDef.Text)
                hng.pilotDia = CSng(txtPilotDiaDef.Text)
                hng.ActiveStyle = CBool(chkbxStyleHingeActive.Checked)
                hng.SaveToXml()
                comboHingeStyle.Items.Clear()
                comboHingeStyle.Items.AddRange(GetElementAttrList("HingeType", "name").ToArray)
                comboHingeStyle.SelectedIndex = comboHingeStyle.Items.IndexOf(hng.Name)

            Case "tpFrameStyle"
                Dim fs As FrameStyle
                If newRec Then
                    'Dim ctlIndex = TabControl1.SelectedTab.Controls.GetChildIndex(Me._panelAddNew)
                    Dim objName As String
                    objName = Me._panelAddNew.txtNewItemName.Text
                    'CType(CType(TabControl1.SelectedTab.Controls(ctlIndex), Panel).Controls(1), TextBox).Text()
                    If objName = "" Then
                        MessageBox.Show("You must give the new record a unique name.", "Record Name", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Exit Select
                    Else
                        fs = New FrameStyle(objName, ParmFilename)
                    End If
                Else
                    If comboFrameStyle.SelectedIndex > -1 Then
                        fs = New FrameStyle(CType(comboFrameStyle.SelectedItem, String), ParmFilename)
                    Else
                        MessageBox.Show("You must select style first.", "Record Name", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Exit Select
                    End If
                End If
                fs.Description = Me._panelAddNew.txtNewDesc.Text
                fs.isMortised = CBool(comboDoMortise.SelectedItem)
                fs.isHaunched = CBool(comboDoHaunch.SelectedItem)
                Me.SetEmptyTextboxToZero(Me.pnlControls2)
                fs.haunchEndAdj = CSng(txtHaunchEndAdj.Text)
                fs.mortiseEndAdj = CSng(txtMortiseEndAdj.Text)
                fs.haunchDepth = CSng(txtHDepthDef.Text)
                fs.mortiseDepth = CSng(txtMDepthDef.Text)
                fs.mortiseShoulder = CSng(txtTenonHWidth.Text)
                fs.haunchOnTenon = CBool(comboHaunchOnTenon.SelectedItem)
                fs.tenonDepth = CSng(txtTenonLength.Text)
                fs.pilotCenterAdj = CSng(txtPilotCenterAdj.Text)
                fs.ActiveStyle = Me.chkbxStyleFrameActive.Checked
                fs.SaveToXml()
                comboFrameStyle.Items.Clear()
                comboFrameStyle.Items.AddRange(GetElementAttrList("FrameStyles", "name").ToArray)
                comboFrameStyle.SelectedIndex = comboFrameStyle.Items.IndexOf(fs.Name)

            Case "tpThicknessStyle"
                Dim tk As Thickness
                If newRec Then
                    'Dim ctlIndex = TabControl1.SelectedTab.Controls.GetChildIndex(Me._panelAddNew)
                    Dim objName As String
                    objName = Me._panelAddNew.txtNewItemName.Text
                    'CType(CType(TabControl1.SelectedTab.Controls(ctlIndex), Panel).Controls(1), TextBox).Text()
                    If objName = "" Then
                        MessageBox.Show("You must give the new record a unique name.", "Record Name", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Exit Select
                    Else
                        tk = New Thickness(objName, ParmFilename)
                    End If
                Else
                    If comboThicknessStyle.SelectedIndex > -1 Then
                        tk = New Thickness(CType(comboThicknessStyle.SelectedItem, String), ParmFilename)
                    Else
                        MessageBox.Show("You must select style first.", "Record Name", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Exit Select
                    End If
                End If
                tk.Description = Me._panelAddNew.txtNewDesc.Text
                Me.SetEmptyTextboxToZero(Me.pnlControls5)
                tk.BoardHeight = CSng(txtBdHeight.Text)
                tk.toolLine = CSng(txtToolCenter.Text)
                tk.routeTopWidth = CSng(txtRouteTopW.Text)
                tk.routeMiddleWidth = CSng(txtRouteMidW.Text)
                tk.routeBottomWidth = CSng(txtRouteBotW.Text)
                tk.ActiveStyle = chkbxStyleThickActive.Checked
                tk.SaveToXml()
                comboThicknessStyle.Items.Clear()
                comboThicknessStyle.Items.AddRange(GetElementAttrList("ThicknessParms", "name").ToArray)
                comboThicknessStyle.SelectedIndex = comboThicknessStyle.Items.IndexOf(tk.Name)

            Case "tpOperationParms"
                Me.SetEmptyTextboxToZero(Me.pnlControlsOpParms)
                opParms.tenonMode = [Boolean].Parse(CStr(comboTenonMode.SelectedItem))
                opParms.middleRail = [Boolean].Parse(CStr(comboMiddleRail.SelectedItem))
                opParms.haunchDepthAdj = CDec(txtHDepthAdj.Text)
                opParms.tenonDepthAdj = CDec(txtTDepthAdj.Text)
                opParms.numClamps = CInt(txtNumClamps.Text)
                opParms.maxLen2Clamps = CDec(txtMaxLength2Clamps.Text)
                opParms.minMortiseShoulder = CDec(txtMinMortShoulder.Text)
                opParms.minMortiseToolMove = CDec(txtMinMortMove.Text)
                opParms.SaveToXml()
                toolGeomery.HaunchToolBottom = CDec(txtHaunchToolBottom.Text)
                toolGeomery.MortiseToolDiameter = CDec(txtMortiseToolDia.Text)
                toolGeomery.PilotToolDiameter = CDec(txtDrillDiameter.Text)
                toolGeomery.SaveToXml()

            Case "tpToolFeedrates"
                If Me.feedrates Is Nothing Then
                    Me.feedrates = Me._db.GetOperationParmsClass("Default")
                End If
                Me.SetEmptyTextboxToZero(Me.pnlControlsFeedrates)
                Me.feedrates.haunchFeedrate = CInt(Me.textHaunchFeed.Text)
                Me.feedrates.mortiseFeedrate = CInt(Me.textMortiseFeed.Text)
                Me.feedrates.tenonFeedrate = CInt(Me.textTenonFeed.Text)
                Me.feedrates.drillFeedrate = CDec(Me.textDrillFeed.Text)
                If newRec Then
                    Me._db.SetToolFeedrate(Me._panelAddNew.txtNewItemName.Text, feedrates)
                Else
                    Me._db.SetToolFeedrate(Me.comboFeedrateSpecie.SelectedItem.ToString, feedrates)
                End If

            Case "tpLibDir"

            Case "tpGeneral"
                My.Settings.ScheduleSearchStartOffset = CInt(txtSchedStartOffset.Value)
                My.Settings.ScheduleSearchEndOffset = CInt(txtSchedEndOffset.Value)
                My.Settings.Save()
                'If System.IO.File.Exists(Module1.ParmFilename) Then
                '  Dim xd As XmlDocument = New XmlDocument
                '  Dim xtr As XmlTextReader = New XmlTextReader(Module1.ParmFilename)
                '  Try
                '    xd.Load(xtr)
                '  Catch ex As XmlException
                '    MessageBox.Show(Module1.ParmFilename & vbCr & ex.Message & vbCr & ex.LineNumber & vbCr & ex.LinePosition)
                '  Finally
                '    xtr.Close()
                '  End Try
                '  xd.SelectSingleNode("/SetupParameters/General/ScheduleSearch[@name='startOffset']").InnerText = Me.txtSchedStartOffset.Value.ToString
                '  xd.SelectSingleNode("/SetupParameters/General/ScheduleSearch[@name='endOffset']").InnerText = Me.txtSchedEndOffset.Value.ToString
                '  xd.Save(Module1.ParmFilename)
                'End If

        End Select

    End Sub

    Private Sub SetEmptyTextboxToZero(ByVal ctrl As Control)
        For Each child As Control In ctrl.Controls
            If TypeOf child Is TextBox AndAlso child.Text = String.Empty Then
                child.Text = CStr(0.0)
            End If
        Next
    End Sub

    Private Sub btnDataStoreDir_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDataStoreDir.Click
        If txtDataStore.Text <> "" AndAlso Directory.Exists(Trim(txtDataStore.Text)) Then
            FolderBrowserDialog1.SelectedPath = Trim(txtDataStore.Text)
        End If
        If FolderBrowserDialog1.ShowDialog = DialogResult.OK Then
            txtDataStore.Text = FolderBrowserDialog1.SelectedPath
        End If
    End Sub

    Private Sub btnDataOutput_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDataOutput.Click
        If txtDataOutput.Text <> "" AndAlso Directory.Exists(Trim(txtDataOutput.Text)) Then
            FolderBrowserDialog1.SelectedPath = Trim(txtDataOutput.Text)
        End If
        If FolderBrowserDialog1.ShowDialog = DialogResult.OK Then
            txtDataOutput.Text = FolderBrowserDialog1.SelectedPath
        End If
    End Sub

    Private Sub btnAccuSysLib_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAccuSysLib.Click
        If txtAccuSysLib.Text <> "" AndAlso Directory.Exists(Trim(txtAccuSysLib.Text)) Then
            FolderBrowserDialog1.SelectedPath = Trim(txtAccuSysLib.Text)
        End If
        If FolderBrowserDialog1.ShowDialog = DialogResult.OK Then
            txtAccuSysLib.Text = FolderBrowserDialog1.SelectedPath
        End If
    End Sub

    Private Sub btnAccuSysTenonLib_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAccuSysTenonLib.Click
        If txtAccuSysTenonLib.Text <> "" AndAlso Directory.Exists(Trim(txtAccuSysTenonLib.Text)) Then
            FolderBrowserDialog1.SelectedPath = Trim(txtAccuSysTenonLib.Text)
        End If
        If FolderBrowserDialog1.ShowDialog = DialogResult.OK Then
            txtAccuSysTenonLib.Text = FolderBrowserDialog1.SelectedPath
        End If
    End Sub

    Private Sub btnDataExec_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDataExec.Click
        If txtDataExecutable.Text <> "" Then
            OpenFileDialog1.DefaultExt = "exe"
            OpenFileDialog1.FileName = txtDataExecutable.Text
            If OpenFileDialog1.CheckFileExists() Then
                OpenFileDialog1.InitialDirectory = txtDataExecutable.Text
            End If
        End If
        If OpenFileDialog1.ShowDialog = DialogResult.OK Then
            txtDataExecutable.Text = OpenFileDialog1.FileName
        End If
    End Sub

    Private Sub btnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        If Not TabControl1.SelectedTab Is tpOperationParms And Not TabControl1.SelectedTab Is tpLibDir Then
            If MsgBox("You are about to permanently delete the displayed style. Do you wish to continue?", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
                Select Case TabControl1.SelectedIndex
                    Case TabControl1.TabPages.IndexOf(tpHingeStyle)
                        Me.styleHng.DeleteRuleFromXml()
                        Me.styleHng = Nothing
                        Me.comboHingeStyle.SelectedIndex = -1
                        Me.SetHingeStyleData()
                    Case TabControl1.TabPages.IndexOf(tpHingePlacement)
                        Me.ruleHng.DeleteRuleFromXml()
                        Me.ruleHng = Nothing
                        Me.comboHingePlacement.SelectedIndex = -1
                        Me.SetHingePlaceData()
                    Case TabControl1.TabPages.IndexOf(tpFrameStyle)
                        Me.styleFS.DeleteRuleFromXml()
                        Me.styleFS = Nothing
                        Me.comboFrameStyle.SelectedIndex = -1
                        Me.SetFrameStyleData()
                    Case TabControl1.TabPages.IndexOf(tpThicknessStyle)
                        Me.styleThk.DeleteRuleFromXml()
                        Me.styleThk = Nothing
                        Me.comboThicknessStyle.SelectedIndex = -1
                        Me.SetThicknessData()
                    Case TabControl1.TabPages.IndexOf(tpToolFeedrates)
                        Me._db.DeleteToolFeedrate(Me.comboFeedrateSpecie.SelectedItem.ToString)
                        Me.comboFeedrateSpecie.SelectedIndex = -1
                        Me.SetFeedrateData()

                End Select
                Me.LoadComboLists()
            End If
        End If
        btnApply.Visible = True
        Me._isModified = True
        UpdateTabs()
        EnableTabpageEdit(True)
    End Sub

    Private Sub txtSchedStartOffset_ValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtSchedStartOffset.ValueChanged, txtSchedEndOffset.ValueChanged
        Me._isModified = True
    End Sub

End Class
