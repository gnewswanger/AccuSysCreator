Imports FramePartImagePanel

''' <summary>
''' <file>OperationsGraphicClass.vb</file>
''' <author>Galen Newswanger</author>
''' 
''' This class, containing only shared methods and so never instantiated, provides
''' calculations for the FramePartImagePanel class.
''' </summary>
''' <remarks></remarks>
Public Class OperationsGraphicClass

  Public Shared Function GetOpPath(ByVal opMode As FrontFrameEventClasses.OperationPathMode, ByVal e As EventArgs) As Drawing2D.GraphicsPath
    Select Case opMode
      Case FrontFrameEventClasses.OperationPathMode.doHaunch
        Return GetHaunchPath(CType(e, FrontFrameEventClasses.HaunchEventArgs))
      Case FrontFrameEventClasses.OperationPathMode.doMortise
        Return GetMortisePath(CType(e, FrontFrameEventClasses.MortiseEventArgs))
      Case FrontFrameEventClasses.OperationPathMode.doPilot
        Return GetPilotPath(CType(e, FrontFrameEventClasses.PilotEventArgs))
    End Select
    Return Nothing
  End Function

  Private Shared Function GetHaunchPath(ByVal e As FrontFrameEventClasses.HaunchEventArgs) As Drawing2D.GraphicsPath
    Dim position As PointF = e.Position
    Dim hLen As Single = e.HaunchLength
    Dim hDepth As Single = e.HaunchDepth
    Dim thisPath As New Drawing2D.GraphicsPath
    Dim pts(3) As PointF
    pts(0) = New PointF(position.X - ((hLen / 2) + hDepth), position.Y)
    pts(1) = New PointF(position.X - (hLen / 2), hDepth)
    pts(2) = New PointF(position.X + (hLen / 2), hDepth)
    pts(3) = New PointF(position.X + (hLen / 2) + hDepth, position.Y)
    thisPath.AddLines(pts)
    Return thisPath
  End Function

  Private Shared Function GetMortisePath(ByVal e As FrontFrameEventClasses.MortiseEventArgs) As Drawing2D.GraphicsPath
    Dim position As PointF = e.Position
    Dim mLen As Single = e.MortiseLength
    Dim mDepth As Single = e.MortiseDepth
    Dim mOffset As Single = e.MortiseEndOffset
    Dim hDepth As Single = 0.0
    If e.IsHaunched Then
      hDepth = e.HaunchDepth
    End If
    Dim thisPath As New Drawing2D.GraphicsPath
    Dim pts(3) As PointF
    pts(0) = New PointF(position.X - (mLen / 2) + mOffset, hDepth)
    pts(1) = New PointF(position.X - (mLen / 2) + mOffset, mDepth + hDepth)
    pts(2) = New PointF(position.X + (mLen / 2) + mOffset, mDepth + hDepth)
    pts(3) = New PointF(position.X + (mLen / 2) + mOffset, hDepth)
    thisPath.AddLines(pts)
    Return thisPath
  End Function

  Private Shared Function GetPilotPath(ByVal e As FrontFrameEventClasses.PilotEventArgs) As Drawing2D.GraphicsPath
    Dim position As PointF = e.Position
    Dim pLen As Single = e.PilotLength
    Dim pDepth As Single = e.PilotDepth
    Dim hDepth As Single = e.HaunchDepth
    Dim thisPath As New Drawing2D.GraphicsPath
    Dim pts(3) As PointF
    pts(0) = New PointF(position.X - (pLen / 2), hDepth)
    pts(1) = New PointF(position.X - (pLen / 2), pDepth + hDepth)
    pts(2) = New PointF(position.X + (pLen / 2), pDepth + hDepth)
    pts(3) = New PointF(position.X + (pLen / 2), hDepth)
    thisPath.AddLines(pts)
    Return thisPath
  End Function

  Public Shared Function GetEndTenonPath(ByVal e As FrontFrameEventClasses.EndTenonEventArgs) As Drawing2D.GraphicsPath
    Dim thisPath As New Drawing2D.GraphicsPath
    thisPath.AddRectangle(e.PartRect)
    If e.TenonLengthAtNearend > 0.0 And e.PartType <> FrontFrameEventClasses.PartEdgeTypes.Stile Then
      Dim pts(5) As PointF
      Dim tenonPath As New Drawing2D.GraphicsPath
      Dim position As PointF = e.PartRect.Location
      pts(0) = New PointF(position.X + e.TenonLengthAtNearend + e.HaunchDepthFarend, 0.0)
      pts(1) = New PointF(position.X + e.TenonLengthAtNearend, e.MortiseShoulder)
      pts(2) = New PointF(position.X, e.MortiseShoulder)
      pts(3) = New PointF(position.X, e.PartRect.Height - e.MortiseShoulder)
      pts(4) = New PointF(position.X + e.TenonLengthAtNearend, e.PartRect.Height - e.MortiseShoulder)
      If e.IsCenterPart Then
        pts(5) = New PointF(position.X + e.TenonLengthAtNearend + e.HaunchDepthFarend, e.PartRect.Height)
      Else
        pts(5) = New PointF(position.X + e.TenonLengthAtNearend, e.PartRect.Height)
      End If
      tenonPath.AddLines(pts)
      tenonPath.AddLine(pts(4), pts(1))
      thisPath.AddPath(tenonPath, False)
    End If
    If e.TenonLengthAtFarend > 0.0 And e.PartType <> FrontFrameEventClasses.PartEdgeTypes.Stile Then
      Dim pts(5) As PointF
      Dim tenonPath As New Drawing2D.GraphicsPath
      Dim position As New PointF(e.PartRect.X + e.PartRect.Width, 0.0)
      pts(0) = New PointF(position.X - (e.TenonLengthAtFarend + e.HaunchDepthFarend), 0.0)
      pts(1) = New PointF(position.X - e.TenonLengthAtFarend, e.MortiseShoulder)
      pts(2) = New PointF(position.X, e.MortiseShoulder)
      pts(3) = New PointF(position.X, e.PartRect.Height - e.MortiseShoulder)
      pts(4) = New PointF(position.X - e.TenonLengthAtFarend, e.PartRect.Height - e.MortiseShoulder)
      If e.IsCenterPart Then
        pts(5) = New PointF(position.X - (e.TenonLengthAtFarend + e.HaunchDepthFarend), e.PartRect.Height)
      Else
        pts(5) = New PointF(position.X - e.TenonLengthAtFarend, e.PartRect.Height)
      End If
      tenonPath.AddLines(pts)
      tenonPath.AddLine(pts(4), pts(1))
      thisPath.AddPath(tenonPath, False)
    End If
    Return thisPath
  End Function

  Public Shared Function GetEndTenonArgs(ByVal partName As String, ByVal ptSize As SizeF, ByVal isHaunched As Boolean) As FrontFrameEventClasses.EndTenonEventArgs
    Dim partCode As String = StripLeadingNumeric(partName)
    Dim args As New FrontFrameEventClasses.EndTenonEventArgs
    If partCode.StartsWith("R") OrElse partCode.StartsWith("SC") Then
      args.TenonLengthAtFarend = 0.75
      args.TenonLengthAtNearend = 0.75
      If isHaunched = True Then
        args.HaunchDepthFarend = 0.25
      Else
        args.HaunchDepthFarend = 0.0
      End If
      args.MortiseShoulder = 0.25
    Else
      args.TenonLengthAtFarend = 0.0
      args.TenonLengthAtNearend = 0.0
      args.HaunchDepthFarend = 0.0
      args.MortiseShoulder = 0.0
    End If
    If partCode.StartsWith("RC") OrElse partCode.StartsWith("SC") Then
      args.IsCenterPart = True
    Else
      args.IsCenterPart = False
    End If
    args.PartRect = New RectangleF(New PointF(0.0, 0.0), ptSize)
    Return args
  End Function

  Private Shared Function StripLeadingNumeric(ByVal name As String) As String
    Dim retval As String = name
    For i As Integer = 0 To name.Length - 1
      If IsNumeric(name(i)) Then
        retval = name.Substring(i + 1)
      Else
        Return retval
      End If
    Next
    Return retval
  End Function

End Class
