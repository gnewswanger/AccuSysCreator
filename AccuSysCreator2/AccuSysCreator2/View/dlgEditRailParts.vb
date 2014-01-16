''' <summary>
''' File: dlgEditRailParts.vb
''' Author: Galen Newswanger
''' 
''' This class is a subclass of Windows Form and provides a UI for modifying the 
''' attributes of stiles and rails.
''' </summary>
''' <remarks>
''' Note: The text for labelTenonZero and labelTenonFar is changed to 'Extend Up (or Down)' 
''' for stiles.  This is intended for calculating the haunch on Bead Inset jobs, a process
''' currently done manually in the frmEditProgramText form. Implementation is still pending!
''' </remarks>
Public Class dlgEditRailParts

    Private _fp As FramePart
    Private _frame As FrontFrame
    Private _itemNo As String

    Public ReadOnly Property Part() As FramePart
        Get
            Return _fp
        End Get
    End Property

    Public Sub New(ByVal frame As FrontFrame, ByVal currItem As String)
        ' This call is required by the Windows Form Designer.
        InitializeComponent()
        ' Add any initialization after the InitializeComponent() call.
        Me._frame = frame
        Me._itemNo = currItem
        Me.InitializeForm()
    End Sub
    Public Sub New(ByVal fp As FramePart)
        ' This call is required by the Windows Form Designer.
        InitializeComponent()
        ' Add any initialization after the InitializeComponent() call.
        Me._fp = fp
        Me._frame = Me._fp.MyFrame
        Me.InitializeForm()
    End Sub

    Private Sub InitializeForm()
        Me.comboPartStyle.DataSource = [Enum].GetValues(GetType(FrontFrameEventClasses.PartEdgeTypes))
        If Me._fp IsNot Nothing Then
            Me.txtPartName.Text = Me._fp.Name.Split(CChar("."))(2)
            Me.txtPartName.Enabled = False
            Me.comboPartStyle.DropDownStyle = ComboBoxStyle.Simple
            Me.comboPartStyle.SelectedItem = Me._fp.PartEdges(0).PeType ' [Enum].GetName(GetType(FrontFrameEventClasses.PartEdgeTypes), Me._fp.PartEdges(0).PeType)
            Me.comboPartStyle.Enabled = False

            Me.ckNoHaunchatZero.Checked = Me._fp.noHaunchAtZero
            Me.ckNoHaunchatFarend.Checked = Me._fp.noHaunchAtFarend

            Me.txtTenonFar.Text = Me._fp.tenonLengthAtFar.ToString
            Me.txtTenonNear.Text = Me._fp.tenonLengthAtZero.ToString

            Me.txtPartWdth.Text = Me._fp.Width.ToString
            Me.txtPartLength.Text = Me._fp.Length.ToString
            Me.checkIncludeInCutlist.Checked = Me._fp.IncludeInCutlist

        ElseIf Me._frame IsNot Nothing Then
            Me.txtPartName.Enabled = True
            Me.comboPartStyle.DropDownStyle = ComboBoxStyle.DropDown
            Me.comboPartStyle.Enabled = True
        Else
            MsgBox("No frame part or front frame has been assigned", MsgBoxStyle.Critical)
            Me.Close()
        End If
    End Sub

    Private Sub dlgEditRailParts_FormClosing(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
        If Me.DialogResult = Windows.Forms.DialogResult.OK Then
            If Me._fp Is Nothing Then
                CreateNewPart()
                UpdateFramePart()
            Else
                UpdatePartSize()
                UpdateFramePart()
            End If
        End If
    End Sub

    Private Sub CreateNewPart()
        Dim ps As New FramePartBaseClass(Me._frame.Name & "." & Me.txtPartName.Text)
        'ps.ItemNo = Me._itemNo
        ps.Thickness = Module1.JobThickness.BoardHeight
        ps.Width = CDec(Me.txtPartWdth.Text)
        ps.Length = CDec(Me.txtPartLength.Text)
        Me._fp = Me._frame.CreatePart(ps, HingePlacement.N)
    End Sub
    Private Sub UpdatePartSize()
        Me._fp.Width = CDec(Me.txtPartWdth.Text)
        Me._fp.Length = CDec(Me.txtPartLength.Text)
        Me._fp.Name = Me._frame.Name & "." & Me.txtPartName.Text
    End Sub
    Private Sub UpdateFramePart()
        Me._fp.noHaunchAtZero = Me.ckNoHaunchatZero.Checked
        Me._fp.noHaunchAtFarend = Me.ckNoHaunchatFarend.Checked
        Me._fp.tenonLengthAtZero = CSng(Me.txtTenonNear.Text)
        Me._fp.tenonLengthAtFar = CSng(Me.txtTenonFar.Text)
        If Me._fp.Grade <> CChar("A") OrElse Me._fp.Grade = CChar("B") Then
            Me._fp.Grade = CChar("A")
        End If
        Me._fp.IncludeInCutlist = Me.checkIncludeInCutlist.Checked
    End Sub

    Private Sub comboPartStyle_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles comboPartStyle.SelectedIndexChanged
        If (CType(Me.comboPartStyle.SelectedItem, FrontFrameEventClasses.PartEdgeTypes) And FrontFrameEventClasses.PartEdgeTypes.Stile) = CType(Me.comboPartStyle.SelectedItem, FrontFrameEventClasses.PartEdgeTypes) Then
            Me.ckNoHaunchatFarend.Enabled = False
            Me.ckNoHaunchatZero.Enabled = False
            Me.labelTenonZero.Text = "Extend Stile UP:"
            Me.labelTenonFar.Text = "Extend Stile DOWN:"
        Else
            Me.ckNoHaunchatFarend.Enabled = Module1.JobFrameStyle.isHaunched
            Me.ckNoHaunchatZero.Enabled = Module1.JobFrameStyle.isHaunched
            Me.labelTenonZero.Text = "Adjusted Tenon Length at Point 0:"
            Me.labelTenonFar.Text = "Adjusted Tenon Length at Far End:"
        End If
    End Sub
End Class