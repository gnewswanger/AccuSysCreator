Imports System.Drawing

''' <summary>
''' File: PilotEventArgs.vb
''' Author: Galen Newswanger
''' 
''' This class is a subclass of EventArgs and provides a means for passing
''' arguments to called methods.
''' </summary>
''' <remarks>FrontFrameEventArgClasses are created in a separate dll to allow sharing among multiple projects.</remarks>
Public Class PilotEventArgs
    Inherits EventArgs

    Public PathMode As OperationPathMode = OperationPathMode.doPilot
    Public Position As PointF
    Public PilotDepth As Single
    Public PilotLength As Single
    Public HaunchDepth As Single

End Class
