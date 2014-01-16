''' <summary>
''' <file>File: FrameSize.vb</file>
''' <author>Author: Galen Newswanger</author>
''' 
''' This class contains the parameters that describe the size of 
''' a FrontFrame and exists as a convenient means for passing these
''' parameters.
''' </summary>
''' <remarks></remarks>
Public Class FrameSize

    Private _partName As String
    Private _thickness As Single = 0.0
    Private _width As Single = 0.0
    Private _length As Single = 0.0

    Public Property Name() As String
        Get
            Return Me._partName
        End Get
        Set(ByVal value As String)
            Me._partName = value
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
    Public Property Width() As Single
        Get
            Return Me._width
        End Get
        Set(ByVal value As Single)
            Me._width = value
        End Set
    End Property
    Public Property Length() As Single
        Get
            Return Me._length
        End Get
        Set(ByVal value As Single)
            Me._length = value
        End Set
    End Property

    Public Sub New(ByVal wdth As Single, ByVal len As Single, Optional ByVal ptname As String = "")
        Me._width = wdth
        Me._length = len
        Me._partName = ptname
    End Sub

End Class
