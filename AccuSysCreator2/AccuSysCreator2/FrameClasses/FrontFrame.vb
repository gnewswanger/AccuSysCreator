''' <summary>
''' <file>File: FrontFrame.vb</file>
''' <author>Author: Galen Newswanger</author>
''' 
''' This class, as its name suggests, represents the front (face) frame of the cabinet. 
''' It contains a list of FramePart representing its stiles and rails, and a list
''' of FramePart representing the cabinet openings.
''' </summary>
''' <remarks></remarks>
Public Class FrontFrame
    Inherits Object

    Private _frameCode As String
    Private _jobNumber As String
    Private _itemNumber As String
    Private _frameSize As SizeF
    Private _cabType As CabinetTypes
    Private _parts As New List(Of FramePart)
    Private _openings As New List(Of FramePart)
    Private _wood As String
    Private _db As DataClass

#Region "Class Properties"
    Public ReadOnly Property Name() As String
        Get
            Return Me._jobNumber & "." & Me._itemNumber
        End Get
    End Property
    Public Property Code() As String
        Get
            Return Trim(Me._frameCode)
        End Get
        Set(ByVal Value As String)
            Me._frameCode = Value
        End Set
    End Property
    Public Property FrontframeSize() As SizeF
        Get
            Return Me._frameSize
        End Get
        Set(ByVal Value As SizeF)
            Me._frameSize = Value
        End Set
    End Property

    Public ReadOnly Property ItemNo() As String
        Get
            Return Trim(Me._itemNumber)
        End Get
    End Property

    Public ReadOnly Property JobNo() As String
        Get
            Return Me._jobNumber
        End Get
    End Property

    Public Property CabinetType() As CabinetTypes
        Get
            Return Me._cabType
        End Get
        Set(ByVal Value As CabinetTypes)
            Me._cabType = Value
        End Set
    End Property

    Public Property PartsList() As Generic.List(Of FramePart)
        Get
            Return Me._parts
        End Get
        Set(ByVal Value As Generic.List(Of FramePart))
            Me._parts = Value
        End Set
    End Property

    Public Property Openinglist() As Generic.List(Of FramePart)
        Get
            Return Me._openings
        End Get
        Set(ByVal Value As Generic.List(Of FramePart))
            Me._openings = Value
        End Set
    End Property

    Public Property WoodSpecie() As String
        Get
            Return Me._wood
        End Get
        Set(ByVal Value As String)
            Me._wood = Value
        End Set
    End Property

    Public ReadOnly Property Data() As DataClass
        Get
            Return Me._db
        End Get
    End Property

#End Region

    Public Sub New(ByVal fr As FrontFrame)
        MyBase.New()
        Me._jobNumber = fr._jobNumber
        Me._itemNumber = fr._itemNumber
        'Me._frameName = fr._frameName
        Me._frameSize = fr._frameSize
        Me._cabType = fr._cabType
        Me._parts = fr._parts
        Me._openings = fr._openings
        Me._wood = fr._wood
        Me._db = fr._db
    End Sub

    Public Sub New(ByRef data As DataClass, ByVal jobNum As String, ByVal itemNum As String, ByVal specie As String)
        MyBase.New()
        Me._jobNumber = jobNum
        Me._itemNumber = itemNum
        'Me._frameName = Trim(jobNum) & "." & Trim(itemNum)
        Me._wood = specie
        Me._db = data
    End Sub

    Public Function CreatePart(ByVal part As FramePartBaseClass, ByVal hingePlace As HingePlacement) As FramePart
        Dim opParms As OperationParms = Me._db.GetOperationParmsClass(Me.WoodSpecie)
        Dim thisPart As FramePart
        If part.Code.StartsWith("OP") Or part.Code.StartsWith("CUT") Then
            If Not IsNumeric(part.Name.Substring(part.Name.Length - 1)) Then
                part.Name = part.Name & Me._openings.Count + 1
            End If
            thisPart = New FramePart(Me, part, hingePlace, opParms)
            Me._openings.Add(thisPart)
        Else
            thisPart = New FramePart(Me, part, hingePlace, opParms)
            Me._parts.Add(thisPart)
        End If
        Return thisPart
    End Function

    Public Sub AppendFramePart(ByVal fPart As FramePart)
        If fPart.Code.StartsWith("OP") Or fPart.Code.StartsWith("CUT") Then
            Me._openings.Add(fPart)
        Else
            Me._parts.Add(fPart)
        End If
        fPart.SaveToDB(True)
        'Me.SaveToDb(True)
    End Sub

    Public Function GetSinglePart(ByVal ptName As String) As FramePart
        For i As Integer = 0 To Me._parts.Count - 1
            If ptName = Me._parts(i).Name Then
                Return Me._parts(i)
            End If
        Next
        For i As Integer = 0 To Me._openings.Count - 1
            If ptName = Me._openings(i).Name Then
                Return Me._openings(i)
            End If
        Next
        Return Nothing
    End Function

    Public Function GetSinglePartByDescription(ByVal desc As String) As FramePart
        For i As Integer = 0 To Me._parts.Count - 1
            If Me._parts(i).Description = desc Then
                Return Me._parts(i)
            End If
        Next
        For i As Integer = 0 To Me._openings.Count - 1
            If Me._openings(i).Description = desc Then
                Return Me._openings(i)
            End If
        Next
        Return Nothing
    End Function

    Public Function DeletePart(ByVal fp As FramePart) As Boolean
        If fp.Code.StartsWith("O") Or fp.Code.StartsWith("C") Then
            For i As Integer = 0 To Me._openings.Count - 1
                If (Me._parts(i).Name = fp.Name) Then
                    Me._parts.RemoveAt(i)
                    Return True
                End If
            Next
        Else
            For i As Integer = 0 To Me._parts.Count - 1
                If (Me._parts(i).Name = fp.Name) Then
                    Me._parts.RemoveAt(i)
                    Return True
                End If
            Next
        End If
        Return False
    End Function

    Public Sub SaveToDb(ByVal leaveConnOpen As Boolean)
        For i As Integer = 0 To Me._parts.Count - 1
            Dim pt As FramePart = Me._parts(i)
            pt.SaveToDB(True)
        Next
        For i As Integer = 0 To _openings.Count - 1
            Dim pt As FramePart = Me._openings(i)
            pt.SaveToDB(True)
        Next
        Me.Data.SetFrontFrame(Me)
    End Sub
End Class

