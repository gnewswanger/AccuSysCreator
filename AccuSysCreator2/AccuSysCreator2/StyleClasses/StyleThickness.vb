Imports System.Xml

''' <summary>
''' File: StyleThickness.vb
''' Author: Galen Newswanger
''' 
''' This class is a subclass of AbstractStyleClass and encapsulates the attributes
''' of a frame's thickness.
''' </summary>
''' <remarks>The style classes with names beginning with "Style" are intended to replace
''' the classes in the folder "ParamClasses". Implementation is still pending.  There are 
''' only a few references to these classes in use currently.</remarks>
Public Class StyleThickness
    Inherits AbstractStyleClass

    Private _tkDesc As String
    Private _thickness As Single
    Private _boardHeight As Single
    Private _toolLine As Single
    Private _routeTopWidth As Single
    Private _routeMiddleWidth As Single
    Private _routeBottomWidth As Single

#Region "Public Class Properties"

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
            Return Me._tkDesc
        End Get
        Set(ByVal Value As String)
            Me._tkDesc = Value
        End Set
    End Property

    Public Property Thickness() As Single
        Get
            Return Me._thickness
        End Get
        Set(ByVal value As Single)
            Me._thickness = value
        End Set
    End Property

    Public Property BoardHeight() As Single
        Get
            Return Me._boardHeight
        End Get
        Set(ByVal value As Single)
            Me._boardHeight = value
        End Set
    End Property

    Public Property ToolLine() As Single
        Get
            Return Me._toolLine
        End Get
        Set(ByVal value As Single)
            Me._toolLine = value
        End Set
    End Property

    Public Property RouteTopWidth() As Single
        Get
            Return Me._routeTopWidth
        End Get
        Set(ByVal value As Single)
            Me._routeTopWidth = value
        End Set
    End Property

    Public Property RouteMiddleWidth() As Single
        Get
            Return Me._routeMiddleWidth
        End Get
        Set(ByVal value As Single)
            Me._routeMiddleWidth = value
        End Set
    End Property

    Public Property RouteBottomWidth() As Single
        Get
            Return Me._routeBottomWidth
        End Get
        Set(ByVal value As Single)
            Me._routeBottomWidth = value
        End Set
    End Property

#End Region

    Public Sub New(ByVal edgeName As String, ByVal styleName As String)
        MyBase.New(edgeName, styleName)

    End Sub


End Class