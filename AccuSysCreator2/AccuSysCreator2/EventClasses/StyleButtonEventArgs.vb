''' <summary>
''' <file>File: StyleButtonEventArgs.vb</file>
''' <author>Author: Galen Newswanger</author>
''' 
''' StyleButtonEventArgs is a subclass of EventArgs intended for passing at list of 
''' parameters of type StyleClassMetaData to called methods. 
''' </summary>
''' <remarks></remarks>
Public Class StyleButtonEventArgs
    Inherits EventArgs

    Public StyleMeta As New List(Of StyleClassMetaData)
    Public csvFileExists As Boolean

    Public ReadOnly Property FirstStyle() As StyleClassMetaData
        Get
            If StyleMeta.Count > 0 Then
                Return StyleMeta.Item(0)
            Else
                Return Nothing
            End If
        End Get
    End Property

    Public Sub New()
        MyBase.New()
    End Sub

    Public Sub New(ByVal style As StyleClassMetaData)
        MyBase.New()
        StyleMeta.Add(style)
    End Sub

    Public Sub New(ByVal styleList As List(Of StyleClassMetaData))
        MyBase.New()
        StyleMeta = styleList
    End Sub

End Class
