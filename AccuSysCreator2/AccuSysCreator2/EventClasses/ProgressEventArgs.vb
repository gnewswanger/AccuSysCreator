''' <summary>
''' <file>File: ProgressEventArgs.vb</file>
''' <author>Author: Galen Newswanger</author>
''' 
''' ProgressEventArgs is a subclass of EventArgs and is intended for passing a value to methods 
''' displaying some sort of feedback such as a progress bar.
''' </summary>
''' <remarks></remarks>
Public Class ProgressEventArgs
    Inherits EventArgs

    Private _progressValue As Integer

    Public Sub New(ByVal Value As Integer)
        Me._progressValue = Value
    End Sub

    Public Function GetProgress() As Integer
        Return Me._progressValue
    End Function
End Class
