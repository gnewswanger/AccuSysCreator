''' <summary>
''' File: TenonPathEventArgs.vb
''' Author: Galen Newswanger
''' 
''' This class is a subclass of EventArgs and provides a means for passing
''' arguments to called methods.
''' </summary>
''' <remarks>FrontFrameEventArgClasses are created in a separate dll to allow sharing among multiple projects.</remarks>
Public Class TenonPathEventArgs
  Inherits EventArgs

  Public PathMode As OperationPathMode = OperationPathMode.doHaunch
  Public Position As Single


End Class
