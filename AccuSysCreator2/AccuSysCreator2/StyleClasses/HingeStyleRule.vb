Imports System.Xml

''' <summary>
''' File: HingeStyleRule.vb
''' Author: Galen Newswanger
''' 
''' This class is a subclass of AbstractStyleClass and encapsulates the attributes that
''' define the number of hinges and placement of hinges.
''' </summary>
''' <remarks>The style classes with names beginning with "Style" are intended to replace
''' the classes in the folder "ParamClasses". Implementation is still pending.  There are 
''' only a few references to these classes in use currently.</remarks>
Public Class HingeStyleRule
    Inherits AbstractStyleClass

    Private _mortiseOffset As Single
    Private _smallOpgMortOffset As Single
    Private _minHght42Hinges As Single
    Private _hghtRange42Lower As Single
    Private _hghtRange42Upper As Single
    Private _hghtRange43Upper As Single
    Private _hghtRange44Upper As Single

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

    Public Property MortiseOffsetStdOpg() As Single
        Get
            Return Me._mortiseOffset
        End Get
        Set(ByVal value As Single)
            Me._mortiseOffset = value
        End Set
    End Property

    Property MortiseOffsetSmallOpg() As Single
        Get
            Return Me._smallOpgMortOffset
        End Get
        Set(ByVal value As Single)
            Me._smallOpgMortOffset = value
        End Set
    End Property

    Public Property SmallOpgMinHght2Hinges() As Single
        Get
            Return Me._minHght42Hinges
        End Get
        Set(ByVal value As Single)
            Me._minHght42Hinges = value
        End Set
    End Property

    Public Property StdOpgMinHght2Hinges() As Single
        Get
            Return Me._hghtRange42Lower
        End Get
        Set(ByVal value As Single)
            Me._hghtRange42Lower = value
        End Set
    End Property

    Public Property StdOpgMaxHght2Hinges() As Single
        Get
            Return Me._hghtRange42Upper
        End Get
        Set(ByVal value As Single)
            Me._hghtRange42Upper = value
        End Set
    End Property

    Public Property StdOpgMaxHght3Hinges() As Single
        Get
            Return Me._hghtRange43Upper
        End Get
        Set(ByVal value As Single)
            Me._hghtRange43Upper = value
        End Set
    End Property

    Public Property StdOpgMaxHght4Hinges() As Single
        Get
            Return Me._hghtRange44Upper
        End Get
        Set(ByVal value As Single)
            Me._hghtRange44Upper = value
        End Set
    End Property

#End Region

    Public Sub New(ByVal edge As String, ByVal ruleName As String)
        MyBase.New(edge, ruleName)
    End Sub

    Public Sub New(ByVal edge As String, ByVal rule As HingeStyleRule)
        MyBase.New(edge, rule.StyleName)

        Me.MortiseOffsetStdOpg = rule.MortiseOffsetStdOpg
        Me.MortiseOffsetSmallOpg = rule.MortiseOffsetSmallOpg
        Me.SmallOpgMinHght2Hinges = rule.SmallOpgMinHght2Hinges
        Me.StdOpgMinHght2Hinges = rule.StdOpgMinHght2Hinges
        Me.StdOpgMaxHght2Hinges = rule.StdOpgMaxHght2Hinges
        Me.StdOpgMaxHght3Hinges = rule.StdOpgMaxHght3Hinges
        Me.StdOpgMaxHght4Hinges = rule.StdOpgMaxHght4Hinges
    End Sub

End Class

