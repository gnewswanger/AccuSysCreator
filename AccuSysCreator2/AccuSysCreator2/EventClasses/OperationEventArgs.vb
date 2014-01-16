''' <summary>
''' <file>File: OperationEventArgs.vb</file>
''' <author>Author: Galen Newswanger</author>
''' 
''' OperationEventArgs is a subclass of EventArgs intended for passing operation parameters to 
''' called methods.
''' </summary>
''' <remarks>
''' It currently has no methods and is basicly a struct.
''' </remarks>
Public Class OperationEventArgs
    Inherits EventArgs

    Private _name As String
    Private _num As Integer
    Private _centerOfX As Single
    Private _haunch As Boolean
    Private _haunchDepth As Single
    Private _haunchLength As Single
    Private _mortise As Boolean
    Private _mortiseDepth As Single
    Private _mortiseLength As Single
    Private _mortiseOffset As Single
    Private _predrill As Boolean = False
    Private _drillDepth As Single = 0.0
    Private _drillDia As Single = 0.0
    Private _betweenDrills As Single = 0.0

    Public Property Name() As String
        Get
            Return Me._name
        End Get
        Set(ByVal value As String)
            Me._name = value
        End Set
    End Property
    Public Property Number() As Integer
        Get
            Return Me._num
        End Get
        Set(ByVal value As Integer)
            Me._num = value
        End Set
    End Property
    Public Property CurrPos() As Single
        Get
            Return Me._centerOfX
        End Get
        Set(ByVal value As Single)
            Me._centerOfX = value
        End Set
    End Property
    Public Property Haunched() As Boolean
        Get
            Return Me._haunch
        End Get
        Set(ByVal value As Boolean)
            Me._haunch = value
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
    Public Property HaunchLength() As Single
        Get
            Return Me._haunchLength
        End Get
        Set(ByVal value As Single)
            Me._haunchLength = value
        End Set
    End Property
    Public Property Mortised() As Boolean
        Get
            Return Me._mortise
        End Get
        Set(ByVal value As Boolean)
            Me._mortise = value
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
    Public Property MortiseLength() As Single
        Get
            Return Me._mortiseLength
        End Get
        Set(ByVal value As Single)
            Me._mortiseLength = value
        End Set
    End Property
    Public Property MortiseOffset() As Single
        Get
            Return Me._mortiseOffset
        End Get
        Set(ByVal value As Single)
            Me._mortiseOffset = value
        End Set
    End Property
    Public Property Predrilled() As Boolean
        Get
            Return Me._predrill
        End Get
        Set(ByVal value As Boolean)
            Me._predrill = value
        End Set
    End Property
    Public Property DrillDepth() As Single
        Get
            Return Me._drillDepth
        End Get
        Set(ByVal value As Single)
            Me._drillDepth = value
        End Set
    End Property
    Public Property DrillDia() As Single
        Get
            Return Me._drillDia
        End Get
        Set(ByVal value As Single)
            Me._drillDia = value
        End Set
    End Property
    Public Property DistTweenDrill() As Single
        Get
            Return Me._betweenDrills
        End Get
        Set(ByVal value As Single)
            Me._betweenDrills = value
        End Set
    End Property

    Public Sub New()

    End Sub
End Class
