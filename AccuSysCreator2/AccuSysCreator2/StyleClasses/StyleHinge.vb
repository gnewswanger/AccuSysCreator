Imports System.Xml

''' <summary>
''' File: StyleHinge.vb
''' Author: Galen Newswanger
''' 
''' This class is a subclass of AbstractStyleClass and encapsulates the attributes
''' of a hinge's mortise and drilling pattern.
''' </summary>
''' <remarks>The style classes with names beginning with "Style" are intended to replace
''' the classes in the folder "ParamClasses". Implementation is still pending.  There are 
''' only a few references to these classes in use currently.</remarks>
Public Class StyleHinge
    Inherits AbstractStyleClass

    Private _hingeDesc As String
    Private _isMortised As Boolean
    Private _mortiseWidth As Single
    Private _mortiseDepth As Single
    Private _isPredrilled As Boolean
    Private _distBetweenOuter As Single
    Private _pilotVertCenter As Single
    Private _pilotDepth As Single
    Private _pilotDia As Single
    Private _HingeRule As String

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
            Return Me._hingeDesc
        End Get
        Set(ByVal Value As String)
            Me._hingeDesc = Value
        End Set
    End Property

    Public ReadOnly Property IsMortised() As Boolean
        Get
            Return Me._mortiseDepth > 0
        End Get
    End Property

    Public Property MortiseWidth() As Single
        Get
            Return Me._mortiseWidth
        End Get
        Set(ByVal value As Single)
            Me._mortiseWidth = value
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

    Public ReadOnly Property IsPredrilled() As Boolean
        Get
            Return Me._pilotDepth > 0.0
        End Get
    End Property

    Public Property DistBetweenOuter() As Single
        Get
            Return Me._distBetweenOuter
        End Get
        Set(ByVal value As Single)
            Me._distBetweenOuter = value
        End Set
    End Property

    Public Property VCenterOuter() As Single
        Get
            Return Me._pilotVertCenter
        End Get
        Set(ByVal value As Single)
            Me._pilotVertCenter = value
        End Set
    End Property

    Public Property PilotDepth() As Single
        Get
            Return Me._pilotDepth
        End Get
        Set(ByVal value As Single)
            Me._pilotDepth = value
        End Set
    End Property

    Public Property PilotDia() As Single
        Get
            Return Me._pilotDia
        End Get
        Set(ByVal value As Single)
            Me._pilotDia = value
        End Set
    End Property

    Public Property HingeRule() As String
        Get
            Return Me._HingeRule
        End Get
        Set(ByVal value As String)
            Me._HingeRule = value
        End Set
    End Property

#End Region

    Public Sub New(ByVal edgeName As String, ByVal styleName As String)
        MyBase.New(edgeName, styleName)
    End Sub

    Public Sub New(ByVal edgeName As String, ByVal style2Dupe As StyleHinge)
        MyBase.New(edgeName, style2Dupe.StyleName)

        Me.MortiseWidth = style2Dupe.MortiseWidth
        Me.MortiseDepth = style2Dupe.MortiseDepth
        Me.DistBetweenOuter = style2Dupe.DistBetweenOuter
        Me.VCenterOuter = style2Dupe.VCenterOuter
        Me.PilotDepth = style2Dupe.PilotDepth
        Me.PilotDia = style2Dupe.PilotDia
        Me.HingeRule = style2Dupe.HingeRule
    End Sub


End Class
