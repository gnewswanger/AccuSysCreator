''' <summary>
''' <file>File: FrameMaterialRegister.vb</file>
''' <author>Author: Galen Newswanger</author>
''' 
''' This class provides methods for calculating and accumulating the total length
''' of all parts of each width required. The sum is returned in total inches
''' </summary>
''' <remarks></remarks>
Public Class FrameMaterialRegister

    Private _specie As String
    Private _jobNo As String
    Private _reportTitle As String
    Private _reportSubtitle As String
    Private _materialList As Generic.List(Of FrameMaterialClass)

    Public ReadOnly Property MaterialList() As Generic.List(Of FrameMaterialClass)
        Get
            Return Me._materialList
        End Get
    End Property
    Public Property ReportTitle() As String
        Get
            Return Me._reportTitle
        End Get
        Set(ByVal value As String)
            Me._reportTitle = value
        End Set
    End Property
    Public Property ReportSubtitle() As String
        Get
            Return Me._reportSubtitle
        End Get
        Set(ByVal value As String)
            Me._reportSubtitle = value
        End Set
    End Property
    Public Property WoodSpecie() As String
        Get
            Return Me._specie
        End Get
        Set(ByVal value As String)
            Me._specie = value
        End Set
    End Property

    Public Sub New(ByVal material As Generic.List(Of FrameMaterialClass))
        Me._materialList = material
    End Sub

    Public Sub Calculate(ByVal frames As FrameListClass)
        For Each item As FrameMaterialClass In Me._materialList
            item.AddLength(frames.GetMaterialLength(item.MaterialWidth))
        Next
    End Sub

End Class