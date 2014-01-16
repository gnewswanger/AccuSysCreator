Imports System.Drawing

''' <summary>
''' File: MortiseEventArgs.vb
''' Author: Galen Newswanger
''' 
''' This class is a subclass of EventArgs and provides a means for passing
''' arguments to called methods.
''' </summary>
''' <remarks>FrontFrameEventArgClasses are created in a separate dll to allow sharing among multiple projects.</remarks>
Public Class MortiseEventArgs
    Inherits EventArgs

    Public PathMode As OperationPathMode = OperationPathMode.doMortise
    Public Position As PointF
    Public MortiseDepth As Single
    Public MortiseLength As Single
    Public MortiseEndOffset As Single
    Public IsHaunched As Boolean = False
    Public HaunchDepth As Single
    Public TenonLenght As Single
End Class
