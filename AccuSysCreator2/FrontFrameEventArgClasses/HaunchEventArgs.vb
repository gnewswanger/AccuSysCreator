Imports System.Drawing

''' <summary>
''' File: HaunchEventArgs.vb
''' Author: Galen Newswanger
''' 
''' This class is a subclass of EventArgs and provides a means for passing
''' arguments to called methods.
''' </summary>
''' <remarks>FrontFrameEventArgClasses are created in a separate dll to allow sharing among multiple projects.</remarks>
Public Class HaunchEventArgs
    Inherits EventArgs

    Public PathMode As OperationPathMode = OperationPathMode.doHaunch
    Public Position As PointF
    Public HaunchDepth As Single
    Public HaunchLength As Single
    Public TenonLength As Single
End Class
