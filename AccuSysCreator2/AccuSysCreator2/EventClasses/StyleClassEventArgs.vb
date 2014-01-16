Imports FrontFrameEventClasses

''' <summary>
''' <file>File: StyleClassEventArgs.vb</file>
''' <author>Author: Galen Newswanger</author>
''' 
''' FrontFrameEventClasses is a subclass of EventArgs intended for passing style 
''' parameters to called methods.
''' </summary>
''' <remarks></remarks>
Public Class StyleClassEventArgs
    Inherits EventArgs

    Private _style As AbstractStyleClass

    Public Property StyleClass() As AbstractStyleClass
        Get
            Return Me._style
        End Get
        Set(ByVal value As AbstractStyleClass)
            Me._style = value
        End Set
    End Property

    Public Sub New(ByVal evName As EventName)
        MyBase.New()
    End Sub

    Public Sub New(ByVal style As AbstractStyleClass, ByVal evName As EventName)
        MyBase.New()
        Me._style = style
    End Sub

End Class
