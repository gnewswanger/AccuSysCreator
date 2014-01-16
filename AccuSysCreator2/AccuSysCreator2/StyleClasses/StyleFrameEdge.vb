''' <summary>
''' File: StyleFrameEdge.vb
''' Author: Galen Newswanger
''' 
''' This class is a subclass of AbstractStyleClass and encapsulates the attributes
''' of a frame part's edge.
''' </summary>
''' <remarks>The style classes with names beginning with "Style" are intended to replace
''' the classes in the folder "ParamClasses". Implementation is still pending.  There are 
''' only a few references to these classes in use currently.</remarks>
Public Class StyleFrameEdge
    Inherits AbstractStyleClass

    Private _fsDesc As String
    Private _haunchEndAdj As Single
    Private _mortiseEndAdj As Single
    Private _haunchDepth As Single
    Private _mortiseDepth As Single
    Private _mortiseShoulder As Single
    Private _pilotCenterAdj As Single

#Region "Public Properties"

    Public ReadOnly Property EdgeName() As String
        Get
            Return MyBase._edgeName
        End Get
    End Property

    Public ReadOnly Property StyleName() As String
        Get
            Return MyBase._styleName
        End Get
    End Property

    Public Property Description() As String
        Get
            Return Me._fsDesc
        End Get
        Set(ByVal Value As String)
            Me._fsDesc = Value
        End Set
    End Property

    Public ReadOnly Property IsMortised() As Boolean
        Get
            Return Me._mortiseDepth > 0
        End Get
    End Property

    Public ReadOnly Property IsHaunched() As Boolean
        Get
            Return Me._haunchDepth > 0
        End Get
    End Property

    Public Property HaunchEndAdj() As Single
        Get
            Return Me._haunchEndAdj
        End Get
        Set(ByVal value As Single)
            Me._haunchEndAdj = value
        End Set
    End Property

    Public Property MortiseEndAdj() As Single
        Get
            Return Me._mortiseEndAdj
        End Get
        Set(ByVal value As Single)
            Me._mortiseEndAdj = value
        End Set
    End Property

    Public Property HaunchDepth() As Single
        Get
            Return Me._haunchDepth
        End Get
        Set(ByVal value As Single)
            Me._haunchDepth = value
        End Set
    End Property

    Public Property MortiseDepth() As Single
        Get
            Return Me._mortiseDepth
        End Get
        Set(ByVal value As Single)
            Me._mortiseDepth = value
        End Set
    End Property

    Public Property MortiseShoulder() As Single
        Get
            Return Me._mortiseShoulder
        End Get
        Set(ByVal value As Single)
            Me._mortiseShoulder = value
        End Set
    End Property

    Public Property PilotCenterAdj() As Single
        Get
            Return Me._pilotCenterAdj
        End Get
        Set(ByVal value As Single)
            Me._pilotCenterAdj = value
        End Set
    End Property

#End Region

#Region "Contructor and Initialization"

    Public Sub New(ByVal edgeName As String, ByVal styleName As String)
        MyBase.New(edgeName, styleName)
    End Sub

    Public Sub New(ByVal edgeName As String, ByVal style2Dupe As StyleFrameEdge)
        MyBase.New(edgeName, style2Dupe.StyleName)
        Me._fsDesc = style2Dupe.Description
        Me.HaunchEndAdj = style2Dupe.HaunchEndAdj
        Me.MortiseEndAdj = style2Dupe.MortiseEndAdj
        Me.HaunchDepth = style2Dupe.HaunchDepth
        Me.MortiseDepth = style2Dupe.MortiseDepth
        Me.MortiseShoulder = style2Dupe.MortiseShoulder
        Me.PilotCenterAdj = style2Dupe.PilotCenterAdj
    End Sub

#End Region


End Class
