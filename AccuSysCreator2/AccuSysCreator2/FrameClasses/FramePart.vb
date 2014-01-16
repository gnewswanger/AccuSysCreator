Imports System.IO
Imports System.Xml
Imports System.Text.RegularExpressions

''' <summary>
''' <file>File: FramePart.vb</file>
''' <author>Author: Galen Newswanger</author>
''' 
''' This class is a subclass of FramePartBaseClass and extends the base class to contain 
''' all the attributes required for generating operations for one or both of its edges. 
''' Instances of this class represent stiles, rails, or cabinet openings. 
''' </summary>
''' <remarks></remarks>
Public Class FramePart
    Inherits FramePartBaseClass

    Private _myFrame As FrontFrame
    Private _edges(1) As PartEdge
    Private _hinged As HingePlacement
    Private _machineTools As MachineTool

    Private _opParms As OperationParms
    Private _hngStyle As Hinge
    Private _hingeRules As HingeRule

    Private _endOffset As Single
    Private _noHaunchZero As Boolean
    Private _noHaunchFarend As Boolean

    Private _tenonLengthNear As Single
    Private _tenonLengthFar As Single
    Private _includedInCutlist As Boolean
    Private _partComments As String
    Private _woodGrade As Char


#Region "FramePart Class Properties"
    Public ReadOnly Property MyFrame() As FrontFrame
        Get
            Return Me._myFrame
        End Get
    End Property
    Public ReadOnly Property JobNumber() As String
        Get
            Return Regex.Matches(Me.Name, "^\d{7,9}")(0).ToString
        End Get
    End Property

    Public Property Hinging() As HingePlacement
        Get
            Return Me._hinged
        End Get
        Set(ByVal Value As HingePlacement)
            Me._hinged = Value
            'UpdateProperty(Value, "Hinging", True)
        End Set
    End Property

    Public ReadOnly Property FrontFrameStyle() As FrameStyle
        Get
            Return Me._edges(0).FrontFrameStyle  'Me._frStyle
        End Get
    End Property

    Public Property PartBase() As FramePartBaseClass
        Get
            Dim retval As New FramePartBaseClass(Me.Name)
            retval.Length = Me.Length
            retval.PartType = Me.PartType
            retval.Thickness = Me.Thickness
            retval.Width = Me.Width
            Return retval
        End Get
        Set(ByVal value As FramePartBaseClass)
            Me.Length = value.Length
            Me.PartType = value.PartType
            Me.Thickness = value.Thickness
            Me.Width = value.Width
        End Set
    End Property

    Public ReadOnly Property HingeStyle() As Hinge
        Get
            Return Me._hngStyle
        End Get
    End Property

    Public Property HingeStyleRules() As HingeRule
        Get
            Return Me._hingeRules
        End Get
        Set(ByVal Value As HingeRule)
            Me._hingeRules = Value
            'UpdateProperty(Value, "HingeStyleRules", True)
        End Set
    End Property
    Public ReadOnly Property Tools() As MachineTool
        Get
            Return Me._machineTools
        End Get
    End Property

    Public ReadOnly Property OperationParmeters() As OperationParms
        Get
            Return Me._opParms
        End Get
    End Property

    Public Property PartEdges() As PartEdge()
        Get
            Return Me._edges
        End Get
        Set(ByVal Value As PartEdge())
            Me._edges = Value
        End Set
    End Property

    Public Property Operationlist(ByVal ptype As FrontFrameEventClasses.PartEdgeTypes) As ArrayList
        Get
            Dim index As Integer = EdgeIndexOf(ptype)
            If index > -1 Then
                Return Me._edges(index).Operationlist
            Else
                Return Nothing
            End If
        End Get
        Set(ByVal Value As ArrayList)
            Dim index As Integer = EdgeIndexOf(ptype)
            If index > -1 Then
                Me._edges(EdgeIndexOf(ptype)).Operationlist = Value
            Else
                MsgBox("No edge for this part type exists.", MsgBoxStyle.Critical)
            End If
        End Set
    End Property
    Public Property AdjoiningPartNameList(ByVal ptype As FrontFrameEventClasses.PartEdgeTypes) As ArrayList
        Get
            Dim index As Integer = EdgeIndexOf(ptype)
            If index > -1 Then
                Return Me._edges(index).AdjoiningPartNameList
            Else
                Return Nothing
            End If
        End Get
        Set(ByVal Value As ArrayList)
            Dim index As Integer = EdgeIndexOf(ptype)
            If index > -1 Then
                Me._edges(EdgeIndexOf(ptype)).AdjoiningPartNameList = Value
            Else
                MsgBox("No edge for this part type exists.", MsgBoxStyle.Critical)
            End If
        End Set
    End Property
    Public ReadOnly Property DoHaunch() As Boolean
        Get
            Return Me._edges(0).FrontFrameStyle.isHaunched
        End Get
    End Property
    Public ReadOnly Property DoMortise() As Boolean
        Get
            Return Me._edges(0).FrontFrameStyle.isMortised
        End Get
    End Property
    Public ReadOnly Property noTenonAtZero() As Boolean
        Get
            Return ((Me.PartType And FrontFrameEventClasses.PartEdgeTypes.Stile) = Me.PartType) OrElse (Me._tenonLengthNear = 0.0)
        End Get
    End Property
    Public ReadOnly Property noTenonAtFarend() As Boolean
        Get
            Return ((Me.PartType And FrontFrameEventClasses.PartEdgeTypes.Stile) = Me.PartType) OrElse (Me._tenonLengthFar = 0.0)
        End Get
    End Property

    Public Property noHaunchAtZero() As Boolean
        Get
            Return ((Me.PartType And FrontFrameEventClasses.PartEdgeTypes.Stile) = Me.PartType) OrElse Me._noHaunchZero
        End Get
        Set(ByVal Value As Boolean)
            Me._noHaunchZero = Value
        End Set
    End Property
    Public Property noHaunchAtFarend() As Boolean
        Get
            Return ((Me.PartType And FrontFrameEventClasses.PartEdgeTypes.Stile) = Me.PartType) OrElse Me._noHaunchFarend
        End Get
        Set(ByVal Value As Boolean)
            Me._noHaunchFarend = Value
        End Set
    End Property
    Public Property tenonLengthAtZero() As Single
        Get
            Return Me._tenonLengthNear
        End Get
        Set(ByVal value As Single)
            Me._tenonLengthNear = value
        End Set
    End Property
    Public Property tenonLengthAtFar() As Single
        Get
            Return Me._tenonLengthFar
        End Get
        Set(ByVal value As Single)
            Me._tenonLengthFar = value
        End Set
    End Property
    Public Property TSComments() As String
        Get
            Return Me._partComments
        End Get
        Set(ByVal value As String)
            Me._partComments = value
        End Set
    End Property
    Public Property Grade() As Char
        Get
            Return Me._woodGrade
        End Get
        Set(ByVal value As Char)
            Me._woodGrade = value
        End Set
    End Property
    Public Property IncludeInCutlist() As Boolean
        Get
            Return Me._includedInCutlist
        End Get
        Set(ByVal Value As Boolean)
            Me._includedInCutlist = Value
        End Set
    End Property
    Public ReadOnly Property IsCenter() As Boolean
        Get
            Return ((Me.PartType And FrontFrameEventClasses.PartEdgeTypes.Center) = Me.PartType)
        End Get
    End Property
    Public ReadOnly Property EndTenonArgs() As FrontFrameEventClasses.EndTenonEventArgs
        Get
            Dim retval As New FrontFrameEventClasses.EndTenonEventArgs()
            retval.PathMode = FrontFrameEventClasses.OperationPathMode.doTenon
            retval.IsCenterPart = Me.IsCenter
            retval.TenonLengthAtNearend = Me.tenonLengthAtZero
            retval.TenonLengthAtFarend = Me.tenonLengthAtFar
            retval.HaunchDepthNearend = Me.FrontFrameStyle.haunchDepth
            retval.HaunchDepthFarend = Me.FrontFrameStyle.haunchDepth
            retval.MortiseShoulder = Me.FrontFrameStyle.mortiseShoulder
            retval.PartRect = New RectangleF(0, 0, Me.Length, Me.Width)
            retval.PartType = Me.PartType
            retval.OperartionList1 = Me.PartEdges(0).OperationText
            If Me.PartEdges.Count > 1 AndAlso Not Me.PartEdges(1) Is Nothing AndAlso Me.PartEdges(1).OperationText.Count > 0 Then
                retval.OperartionList2 = Me.PartEdges(1).OperationText
            End If
            Return retval
        End Get
    End Property

#End Region

    Public Sub New(ByVal fp As FramePart)
        MyBase.New(fp.Name)
        Me._myFrame = fp._myFrame
        'Me.ItemNo = fp.ItemNo
        Me.Width = fp.Width
        Me.Length = fp.Length
        Me.Thickness = fp.Thickness
        Me.PartType = fp.PartType
        Me._hinged = fp._hinged
        Me._opParms = fp._opParms
        Me._hngStyle = fp._hngStyle
        Me._hingeRules = fp._hingeRules
        Me._thickStyle = fp._thickStyle
        Me._noHaunchZero = fp._noHaunchZero
        Me._noHaunchFarend = fp._noHaunchFarend
        Me._edges(0) = New PartEdge(fp._edges(0))
        If (Me._edges.GetLength(0) = 1) Then
            Me._edges(1) = New PartEdge(fp._edges(1))
        End If
        Me._machineTools = fp._machineTools
        Me._partComments = fp._partComments
        Me._tenonLengthFar = fp._tenonLengthFar
        Me._tenonLengthNear = fp._tenonLengthNear
        Me._includedInCutlist = fp._includedInCutlist
    End Sub

    Public Sub New(ByRef ownerFrame As FrontFrame, ByVal part As FramePartBaseClass, ByVal hingePlace As HingePlacement, opParms As OperationParms)
        MyBase.New(part.Name)
        If ownerFrame Is Nothing Then
            MsgBox("A parent frame has not been assigned.")
            'Me._myFrame = Me._db.GetSingleFrame(Regex.Matches(part.Name, "^\d{7,9}.\d{2,3}")(0).Value.ToString)
        Else
            Me._myFrame = ownerFrame
        End If
        Me.Width = part.Width
        Me.Length = part.Length
        Me._thickStyle = Module1.JobThickness
        Me.Thickness = part.Thickness
        Me.PartType = part.PartType
        Me._hinged = hingePlace
        'Me._frStyle = Module1.jobFrameStyle
        Me._hngStyle = Module1.JobHingeStyle
        Me._hingeRules = Module1.JobHingeRule
        Me._opParms = opParms
        Me._opParms.middleRail = Me.Code.StartsWith("SC") Or Me.Code.StartsWith("RC")
        Me._edges(0) = New PartEdge(Me, Me.Name, Me.GetPartEdgeTypeFromCode(0, part.Code), "", "")
        If ((Me.PartType And FrontFrameEventClasses.PartEdgeTypes.Center) = Me.PartType) Then
            Me._edges(1) = New PartEdge(Me, Me.Name, Me.GetPartEdgeTypeFromCode(1, part.Code), "", "")
        End If
        Me._machineTools = Module1.JobMachineTool()
        Me._partComments = ""
        Me._noHaunchZero = Not Me.DoHaunch
        Me._noHaunchFarend = Not Me.DoHaunch
        If ((Me.PartType And FrontFrameEventClasses.PartEdgeTypes.Stile) = Me.PartType) Then
            Me._tenonLengthNear = 0.0
            Me._tenonLengthFar = 0.0
        Else
            Me._tenonLengthNear = Module1.JobFrameStyle.tenonDepth
            Me._tenonLengthFar = Module1.JobFrameStyle.tenonDepth
        End If
        Me._includedInCutlist = True
    End Sub

    Public Sub ModifyPart(ByVal part As FramePart)
        Me._myFrame = part._myFrame
        Me.Name = part.Name
        Me.Width = part.Width
        Me.Length = part.Length
        Me._thickStyle = part._thickStyle
        Me.PartType = part.PartType
        Me._hinged = part._hinged
        Me._opParms = part._opParms
        Me._hngStyle = part._hngStyle
        Me._hingeRules = part._hingeRules
        Me._thickStyle = part._thickStyle
        Me._noHaunchZero = part._noHaunchZero
        Me._noHaunchFarend = part._noHaunchFarend
        Me.tenonLengthAtFar = part.tenonLengthAtFar
        Me.tenonLengthAtZero = part.tenonLengthAtZero
        Me._includedInCutlist = part._includedInCutlist
    End Sub

    Public Function EdgeIndexOf(ByVal pt As FrontFrameEventClasses.PartEdgeTypes) As Integer
        For i As Integer = Me._edges.GetLowerBound(0) To Me._edges.GetUpperBound(0)
            If Not IsNothing(Me._edges(i)) AndAlso Me._edges(i).PeType = pt Then
                Return i
            End If
        Next
        Return -1
    End Function


    Private Function GetPartEdgeTypeFromCode(ByVal edge As Integer, ByVal code As String) As FrontFrameEventClasses.PartEdgeTypes
        If Not code Is Nothing Then
            Select Case code.Substring(0, 2)
                Case "SL"
                    Return FrontFrameEventClasses.PartEdgeTypes.StileLeft
                Case "SR"
                    Return FrontFrameEventClasses.PartEdgeTypes.StileRight
                Case "SC"
                    If edge = 0 Then
                        Return FrontFrameEventClasses.PartEdgeTypes.StileCenterL
                    Else
                        Return FrontFrameEventClasses.PartEdgeTypes.StileCenterR
                    End If
                Case "RT"
                    Return FrontFrameEventClasses.PartEdgeTypes.TopRail
                Case "RB"
                    Return FrontFrameEventClasses.PartEdgeTypes.BotRail
                Case "RC"
                    If edge = 0 Then
                        Return FrontFrameEventClasses.PartEdgeTypes.RailCenterT
                    Else
                        Return FrontFrameEventClasses.PartEdgeTypes.RailCenterB
                    End If
                Case "OP", "CU"
                    Return FrontFrameEventClasses.PartEdgeTypes.Opening
                Case "GI"
                    Return FrontFrameEventClasses.PartEdgeTypes.GlueInStrip
                Case Else
                    MsgBox("Unable to find PartEdgeType for " & code)
                    Return Nothing
            End Select
        End If
    End Function

    Public Sub SetOperations(ByVal cabType As CabinetTypes, ByVal ptType As FrontFrameEventClasses.PartEdgeTypes)
        For i As Integer = 0 To _edges.GetUpperBound(0)
            If Not IsNothing(_edges(i)) Then
                _edges(i).SetOperations(cabType, Me)
            End If
        Next
    End Sub

    Private Sub GetPartEdges()
        Me._edges = Me.MyFrame.Data.GetPartEdgeList(Me, Me.Name, False)
    End Sub

    Public Sub ClearOperations(ByVal ptType As FrontFrameEventClasses.PartEdgeTypes)
        If Me._edges(EdgeIndexOf(ptType)).Operationlist.Count > 0 Then
            Me._edges(EdgeIndexOf(ptType)).Operationlist.Clear()
        End If
    End Sub

    Public Sub ClearAll(ByVal ptType As FrontFrameEventClasses.PartEdgeTypes)
        If Me._edges(EdgeIndexOf(ptType)).Operationlist.Count > 0 Then
            Me._edges(EdgeIndexOf(ptType)).Operationlist.Clear()
        End If
        If Me._edges(EdgeIndexOf(ptType)).AdjoiningPartNameList.Count > 0 Then
            Me._edges(EdgeIndexOf(ptType)).AdjoiningPartNameList.Clear()
        End If
    End Sub
    Public Sub WriteProgramFile(ByVal pttype As FrontFrameEventClasses.PartEdgeTypes, ByVal lPath As String)
        Me._edges(EdgeIndexOf(pttype)).WriteToFile(Me, lPath)
    End Sub

    Public Sub SaveToDB(ByVal leaveConnOpen As Boolean)
        For i As Integer = 0 To Me._edges.GetUpperBound(0)
            If Not IsNothing(Me._edges(i)) Then
                Me._edges(i).SaveToDB(Me.Name, Me.MyFrame.Data, True)
            End If
        Next
        Me.MyFrame.Data.SetFramePart(Me, Me._myFrame.Name)
    End Sub


End Class

