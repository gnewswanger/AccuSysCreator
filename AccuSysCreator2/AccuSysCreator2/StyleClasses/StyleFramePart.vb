''' <summary>
''' File: StyleFramePart.vb
''' Author: Galen Newswanger
''' 
''' This class is a subclass of AbstractStyleClass and encapsulates the attributes
''' of a frame's part.
''' </summary>
''' <remarks>The style classes with names beginning with "Style" are intended to replace
''' the classes in the folder "ParamClasses". Implementation is still pending.  There are 
''' only a few references to these classes in use currently.</remarks>
Public Class StyleFramePart
    Inherits AbstractStyleClass

    Private _styleDescription As String
    Private _middleRail As Boolean
    Private _tenonLengthNear As Single
    Private _tenonLenghtFar As Single
    Private _tenonHaunchNear As Single
    Private _tenonHaunchFar As Single
    Private _tenonValanceWidthLimit As Single
    Private _tenonPartWidthLimit As Single
    Private _singleTenonLimit As Single
    Private _dblTenonSeparation As Single
    Private _glueAllowance As Single

#Region "Public Class Properties"

    Public ReadOnly Property PartName() As String
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
            Return Me._styleDescription
        End Get
        Set(ByVal Value As String)
            Me._styleDescription = Value
        End Set
    End Property

    Public Property IsMiddleRail() As Boolean
        Get
            Return Me._middleRail
        End Get
        Set(ByVal value As Boolean)
            Me._middleRail = value
        End Set
    End Property

    Public ReadOnly Property HasEndTenonNear() As Boolean
        Get
            Return Me.TenonLengthNear > 0
        End Get
    End Property

    Public Property TenonLengthNear() As Single
        Get
            Return Me._tenonLengthNear
        End Get
        Set(ByVal value As Single)
            Me._tenonLengthNear = value
        End Set
    End Property

    Public Property TenonLengthFar() As Single
        Get
            Return Me._tenonLenghtFar
        End Get
        Set(ByVal value As Single)
            Me._tenonLenghtFar = value
        End Set
    End Property

    Public Property TenonHaunchNear() As Single
        Get
            Return Me._tenonHaunchNear
        End Get
        Set(ByVal value As Single)
            Me._tenonHaunchNear = value
        End Set
    End Property

    Public Property TenonHaunchFar() As Single
        Get
            Return Me._tenonHaunchFar
        End Get
        Set(ByVal value As Single)
            Me._tenonHaunchFar = value
        End Set
    End Property

    Public Property GlueAllowance() As Single
        Get
            Return Me._glueAllowance
        End Get
        Set(ByVal value As Single)
            Me._glueAllowance = value
        End Set
    End Property

    Public Property SingleTenonLimit() As Single
        Get
            Return Me._singleTenonLimit
        End Get
        Set(ByVal value As Single)
            Me._singleTenonLimit = value
        End Set
    End Property

    Public Property DblTenonSeparation() As Single
        Get
            Return Me._dblTenonSeparation
        End Get
        Set(ByVal value As Single)
            Me._dblTenonSeparation = value
        End Set
    End Property

    Public Property TenonPartwidthLimit() As Single
        Get
            Return Me._tenonPartWidthLimit
        End Get
        Set(ByVal value As Single)
            Me._tenonPartWidthLimit = value
        End Set
    End Property

    Public Property TenonValancewidthLimit() As Single
        Get
            Return Me._tenonValanceWidthLimit
        End Get
        Set(ByVal value As Single)
            Me._tenonValanceWidthLimit = value
        End Set
    End Property

#End Region

#Region "Contructor and Initialization"
    Public Sub New(ByVal edgeName As String, ByVal styleName As String)
        MyBase.New(edgeName, styleName)
    End Sub

    Public Sub New(ByVal edgeName As String, ByVal style As StyleFramePart)
        MyBase.New(edgeName, style.StyleName)

    End Sub

#End Region

End Class
