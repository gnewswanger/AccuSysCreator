Imports System.Text.RegularExpressions

''' <summary>
''' <file>File: FramePartBaseClass.vb</file>
''' <author>Author: Galen Newswanger</author>
''' 
''' This class is a base class (super class) to FramePart and provides
''' the basic attributes available during import of the "csv" file from 
''' the BOM.   
''' </summary>
''' <remarks>
''' The intention was to eventually create separate sub classes, FramePart 
''' to represent stile and rails and FrameOpening to represent cabinet openings.
''' Name is the full name, a concatenation of job number, item number, and part 
''' code with an additional digit in the case of duplication.  I use regular 
''' expressions to parse the name for ShortName, ItemNo, and Code.
''' </remarks>
Public Class FramePartBaseClass

    Private _partName As String
    Private _width As Single
    Private _length As Single
    Private _thick As Single
    Private _partType As FrontFrameEventClasses.PartEdgeTypes
    Protected _thickStyle As Thickness

#Region "Class Properties"
    ' Name is the full name, a concatenation of ob number, item number, and part 
    ' code with an additional digit in the case of duplication.
    Public Property Name() As String
        Get
            Return Me._partName
        End Get
        Set(ByVal Value As String)
            Me._partName = Value
        End Set
    End Property

    Public ReadOnly Property Description() As String
        Get
            Return Me.ShortName & " " & Me.Width & " X " & Me.Length
        End Get
    End Property

    'ShorName is parsed using regular expressions to be a concatenation of item number and code 
    Public ReadOnly Property ShortName() As String
        Get
            Return Regex.Match(Me._partName, "\d{2,3}[A-Z]?(-|_|.)[A-Z-]+[1-9]{0,2}").Value
        End Get
    End Property

    Public ReadOnly Property ItemNo() As String  '\b[0-9]{2,3}[A-Z]?(?!\d)
        Get
            Return Regex.Match(Me._partName, "\b[0-9]{2,3}[A-Z]?(?!\d)").Value
        End Get
    End Property

    Public ReadOnly Property Code() As String
        Get
            Return Regex.Match(Me._partName, "(?!(\d{7,9}\.\d{2,3}{A-Z]?\.))[A-Z-]{2,8}[1-9]{0,2}").Value
        End Get
    End Property

    Public Property Width() As Single
        Get
            Return Me._width
        End Get
        Set(ByVal Value As Single)
            Me._width = Value
        End Set
    End Property

    Public Property Length() As Single
        Get
            Return Me._length
        End Get
        Set(ByVal Value As Single)
            Me._length = Value
        End Set
    End Property

    Public Property Thickness() As Single
        Get
            If Me._thick = 0.0 AndAlso Not (Me.Code.StartsWith("OP") Or Me.Code.StartsWith("CUT")) Then
                Return Me.ThicknessStyle.BoardHeight
            Else
                Return Me._thick
            End If
        End Get
        Set(ByVal Value As Single)
            Me._thick = Value
        End Set
    End Property

    Public ReadOnly Property ThicknessStyle() As Thickness
        Get
            If Me._thickStyle Is Nothing Then
                Return Module1.JobThickness
            Else
                Return Me._thickStyle
            End If
        End Get
    End Property

    Public Property PartType() As FrontFrameEventClasses.PartEdgeTypes
        Get
            Return Me._partType
        End Get
        Set(ByVal Value As FrontFrameEventClasses.PartEdgeTypes)
            Me._partType = Value
        End Set
    End Property

    Public ReadOnly Property JobNo() As String
        Get
            Return Regex.Match(Me._partName, "^\d{9}").Value
        End Get
    End Property

    Public Property FramePartSize() As FrameSize
        Get
            Dim retClass As New FrameSize(Me.Width, Me.Length, Me.Name)
            retClass.Thickness = Me.Thickness
            Return retClass
        End Get
        Set(ByVal value As FrameSize)
            Me.Width = value.Width
            Me.Length = value.Length
            Me.Name = value.Name
            Me.Thickness = value.Thickness
        End Set
    End Property
#End Region

    Public Sub New(ByVal fullname As String)  '^\d{9}.\d{2,4}[A-Z]?.[A-Z0-9]+
        'Checks the pattern of fullname to fit the concatenation of job number + item number + code
        If Regex.IsMatch(fullname, "^[A-Za-z0-9]{9}.\d{2,4}[A-Z]?.[A-Z0-9]+") Then
            Me._partName = fullname
        Else
            MsgBox("Part name, " & fullname & ", is not valid.")
        End If
        If Me.PartType = Nothing Then
            Me.PartType = Me.GetPartTypeFromCode(Regex.Match(Me._partName, "(?!(\d{7,9}\.\d{2,3}\.))[A-Z]{2}(?=(-|\d))?").Value.ToString)
        End If
    End Sub

    Protected Function GetPartTypeFromCode(ByVal code As String) As FrontFrameEventClasses.PartEdgeTypes
        If Not code Is Nothing Then
            Select Case code
                Case "SL", "SR"
                    Return FrontFrameEventClasses.PartEdgeTypes.Stile
                Case "SC"
                    Return FrontFrameEventClasses.PartEdgeTypes.CenterStile
                Case "RT"
                    Return FrontFrameEventClasses.PartEdgeTypes.TopRail
                Case "RB"
                    Return FrontFrameEventClasses.PartEdgeTypes.BotRail
                Case "RC"
                    Return FrontFrameEventClasses.PartEdgeTypes.CenterRail
                Case "OP", "CU"
                    Return FrontFrameEventClasses.PartEdgeTypes.Opening
                Case "GI"
                    Return FrontFrameEventClasses.PartEdgeTypes.GlueInStrip
                Case Else
                    MsgBox("Unable to find PartType for " & code)
                    Return Nothing
            End Select
        End If
    End Function

End Class
