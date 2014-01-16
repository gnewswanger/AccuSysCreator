Imports System.Drawing

<Assembly: CLSCompliant(True)> 
''' <summary>
''' File: EndTenonEventArgs.vb
''' Author: Galen Newswanger
''' 
''' This class is a subclass of EventArgs and provides a means for passing
''' arguments to called methods.
''' </summary>
''' <remarks>FrontFrameEventArgClasses are created in a separate dll to allow sharing among multiple projects.</remarks>
Public Class EndTenonEventArgs
  Inherits EventArgs

  Public PathMode As OperationPathMode
  Public IsCenterPart As Boolean
  Public TenonLengthAtNearend As Single
  Public TenonLengthAtFarend As Single
  Public HaunchDepthNearend As Single
  Public HaunchDepthFarend As Single
  Public MortiseShoulder As Single
  Public PartRect As RectangleF
  Public PartType As FrontFrameEventClasses.PartEdgeTypes
  Public OperartionList1 As List(Of String)
  Public OperartionList2 As List(Of String)
  Public DisplayGrayed As Boolean
End Class
