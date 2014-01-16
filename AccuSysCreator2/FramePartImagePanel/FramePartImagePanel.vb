Imports System.Collections.Generic

''' <summary>
''' <file>FramePartImagePanel.vb</file>
''' <author>Galen Newswanger</author>
''' 
''' This class is a custom user control that contains a PictureBox for displaying a 
''' visual conception of the defined machining operations.  The definitions of these 
''' operations can be inputed in several formats through the overloaded public Sub 
''' DisplayGraphic and public Sub DisplayGraphicFromFile. 
''' </summary>
''' <remarks></remarks>
Public Class FramePartImagePanel

#Region "Private Variables"
  Private _currentFilename As String = String.Empty
  Private _thePath As New Drawing2D.GraphicsPath
  Private _partPath As Drawing2D.GraphicsPath
  Private _partSize As SizeF
  Private _opList As New List(Of String())
  Private _fileText As String = String.Empty
  Private _imageBackColor As System.Drawing.Color = SystemColors.ControlDarkDark
  Private _imageFill As Color = Color.Bisque
  Private _grayed As Boolean = False
  Private moveStartPoint As Point
  Private isZoomed As Boolean = False
#End Region

#Region "Public Properties"
  Public Property GraphicDisplay_BackColor() As Drawing.Color
    Get
      Return picBxGraphicDisplay.BackColor
    End Get
    Set(ByVal value As Drawing.Color)
      Me._imageBackColor = value
      Panel1.BackColor = Me._imageBackColor
      picBxGraphicDisplay.Refresh()
    End Set
  End Property

  Public Property FileText() As String
    Get
      Return Me._fileText
    End Get
    Set(ByVal value As String)
      Me._fileText = value
    End Set
  End Property

  Public Property PartSize() As SizeF
    Get
      Return Me._partSize
    End Get
    Set(ByVal value As SizeF)
      Me._partSize = value
    End Set
  End Property

#End Region

#Region "Constructors and Iniatializers"
  Public Sub New()

    ' This call is required by the Windows Form Designer.
    InitializeComponent()

    ' Add any initialization after the InitializeComponent() call.
    _thePath = New Drawing2D.GraphicsPath
  End Sub

#End Region

#Region "Public Methods"

  Public Sub ClearDisplay()
    picBxGraphicDisplay.Image = Nothing
    Me._thePath.Reset()
    If Me._partPath IsNot Nothing Then
      Me._partPath.Reset()
    End If
    Me._opList.Clear()
    Me.isZoomed = False
    ZoomPix(Me.isZoomed, Panel1, picBxGraphicDisplay)
    picBxGraphicDisplay.Invalidate()
  End Sub

  Public Sub DisplayGraphicFromFile(ByVal filename As System.IO.FileInfo, ByVal et As FrontFrameEventClasses.EndTenonEventArgs, Optional ByVal grayed As Boolean = False)
    Me.ClearDisplay()
    Me._grayed = grayed
    Me._currentFilename = filename.FullName
    Dim operFile As New OperationsFile
    Me._fileText = operFile.ReadOperationsFile(filename.FullName)
    Me._opList = operFile.UpdateOperationsList(Me._fileText.Split(vbCrLf).ToList)
    Me.PartSize = New SizeF(et.PartRect.Width, et.PartRect.Height)
    If IsNothing(PartSize) OrElse PartSize = New SizeF(0.0, 0.0) Then
      PartSize = New SizeF(EstimatePartLength(Me._opList.Last), 1.5)
    End If
    'TODO: Get part name from directory and file name and find in database. Use framepart to get endtenonargs.
    Dim args As FrontFrameEventClasses.EndTenonEventArgs
    If et Is Nothing Then
      args = OperationsGraphicClass.GetEndTenonArgs(Me._currentFilename.Substring(Me._currentFilename.LastIndexOf(CChar("\")) + 1), Me.PartSize, IsHaunched)
    Else
      args = et
    End If
    Dim inverted As Boolean = False
    If et.OperartionList1.Count > 0 Then
      Me._opList = operFile.UpdateOperationsList(et.OperartionList1)
      inverted = False
      Me.SetPathPoints(et, inverted)
    Else
      Me.SetPathPoints(et, inverted)
    End If
    If Not et.OperartionList2 Is Nothing AndAlso et.OperartionList2.Count > 0 Then
      Me._opList = operFile.UpdateOperationsList(et.OperartionList2)
      inverted = True
      Me.SetPathPoints(et, inverted)
    End If
  End Sub

  Public Sub DisplayGraphic(ByVal et As FrontFrameEventClasses.EndTenonEventArgs, Optional ByVal grayed As Boolean = False)
    ClearDisplay()
    Me._grayed = grayed
    Dim operFile As New OperationsFile
    Dim inverted As Boolean = False
    If et.OperartionList1.Count > 0 Then
      Me._opList = operFile.UpdateOperationsList(et.OperartionList1)
      inverted = False
      Me.SetPathPoints(et, inverted)
    Else
      Me.SetPathPoints(et, inverted)
    End If
    If Not et.OperartionList2 Is Nothing AndAlso et.OperartionList2.Count > 0 Then
      Me._opList = operFile.UpdateOperationsList(et.OperartionList2)
      inverted = True
      Me.SetPathPoints(et, inverted)
    End If
  End Sub

  Public Sub DisplayGraphic(ByVal et As FrontFrameEventClasses.EndTenonEventArgs, ByVal operationsText As String)
    ClearDisplay()
    If operationsText.Count > 0 Then
      Me._grayed = False
      Me._fileText = operationsText
      Dim operFile As New OperationsFile
      Me._opList = operFile.UpdateOperationsList(operationsText.Split(CChar(vbLf)).ToList)
      Me.SetPathPoints(et)
    End If
  End Sub
#End Region

#Region "Private Methods"

  Private Function GetStartPosition(ByVal et As FrontFrameEventClasses.EndTenonEventArgs) As Single
    Dim retVal As Single
    'If ((et.PartType And FrontFrameEventClasses.PartEdgeTypes.Stile) = et.PartType) Then
    retVal = 0.0
    'Else
    'retVal = et.TenonLengthAtNearend + et.HaunchDepthNearend
    'End If
    Return retVal
  End Function

  Private Sub SetPathPoints(ByVal et As FrontFrameEventClasses.EndTenonEventArgs, Optional ByVal invert As Boolean = False)
    If et Is Nothing Then
      et = OperationsGraphicClass.GetEndTenonArgs(Me._currentFilename.Substring(Me._currentFilename.LastIndexOf(CChar("\")) + 1), Me.PartSize, IsHaunched)
    End If
    If IsNothing(Me.PartSize) Then
      Me.PartSize = New SizeF(EstimatePartLength(Me._opList.Last), 1.5)
    End If
    Me._partPath = New Drawing2D.GraphicsPath
    Me._partPath.AddPath(OperationsGraphicClass.GetEndTenonPath(et), False)
    Me._thePath.AddPath(RenderGraphicDisplay(Me.GetStartPosition(et), invert), False)
    picBxGraphicDisplay.Refresh()
    ZoomPix(False, Panel1, picBxGraphicDisplay)
  End Sub

  Private Function RenderGraphicDisplay(ByVal startPos As Single, ByVal invert As Boolean) As Drawing2D.GraphicsPath
    Dim retPath As New Drawing2D.GraphicsPath
    If Not IsNothing(Me._opList) AndAlso Me._opList.Count > 0 Then
      For i As Integer = 0 To Me._opList.Count - 1
        Dim strs() As String = Me._opList(i)
        If CInt(strs(3)) = 1 Then
          Dim e As New FrontFrameEventClasses.HaunchEventArgs
          e.Position = New PointF(startPos + CSng(strs(1)), 0.0)
          e.HaunchDepth = CSng(strs(2))
          e.HaunchLength = CSng(strs(4))
          retPath.AddPath(OperationsGraphicClass.GetOpPath(FrontFrameEventClasses.OperationPathMode.doHaunch, e), False)
        End If
        If CInt(strs(5)) = 1 Then
          Dim e As New FrontFrameEventClasses.MortiseEventArgs
          e.Position = New PointF(startPos + CSng(strs(1)), 0.0)
          e.MortiseLength = CSng(strs(6))
          e.MortiseDepth = CSng(strs(7))
          e.MortiseEndOffset = CSng(strs(9))
          e.IsHaunched = CBool(strs(3))
          e.HaunchDepth = CSng(strs(2))
          retPath.AddPath(OperationsGraphicClass.GetOpPath(FrontFrameEventClasses.OperationPathMode.doMortise, e), False)
        End If
        If CInt(strs(8)) = 1 Then
          Dim e As New FrontFrameEventClasses.PilotEventArgs
          e.Position = New PointF(startPos + CSng(strs(1)), 0.0)
          e.HaunchDepth = CSng(strs(2))
          e.PilotDepth = 0.7
          e.PilotLength = 0.187
          retPath.AddPath(OperationsGraphicClass.GetOpPath(FrontFrameEventClasses.OperationPathMode.doPilot, e), False)
        End If
      Next
    End If
    If invert Then
      Return Me.GetInvertedPath(retPath)
    Else
      retPath.AddPath(Me._partPath, False)
      Return retPath
    End If
  End Function

  Private Function GetInvertedPath(ByVal aPath As Drawing2D.GraphicsPath) As Drawing2D.GraphicsPath
    Dim rect As RectangleF = Me._partPath.GetBounds
    Dim plts() As PointF = {New PointF(rect.Left, rect.Bottom), New PointF(rect.Right, rect.Bottom), New PointF(rect.Left, rect.Top)}
    Dim matrix As New Drawing2D.Matrix(rect, plts)
    aPath.Transform(matrix)
    If aPath.PointCount = 0 Then
      Return Nothing
    Else
      Return (aPath)
    End If
  End Function

  Private Sub picBxGraphicDisplay_MouseDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles picBxGraphicDisplay.MouseDoubleClick
    isZoomed = Not isZoomed
    If isZoomed Then
      ZoomPix(isZoomed, Panel1, picBxGraphicDisplay, e.X)
    Else
      ZoomPix(isZoomed, Panel1, picBxGraphicDisplay)
    End If
  End Sub

  Private Sub picBxGraphicDisplay_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles picBxGraphicDisplay.MouseDown
    If (e.Button = Windows.Forms.MouseButtons.Left) Then
      moveStartPoint = picBxGraphicDisplay.PointToScreen(New Point(e.X, e.Y))
    End If
  End Sub

  Private Sub picBxGraphicDisplay_MouseMove(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles picBxGraphicDisplay.MouseMove
    If (e.Button = Windows.Forms.MouseButtons.Left) Then
      Dim currentPoint As Point = picBxGraphicDisplay.PointToScreen(New Point(e.X, e.Y))
      picBxGraphicDisplay.Location = New Point(picBxGraphicDisplay.Location.X - (moveStartPoint.X - currentPoint.X), picBxGraphicDisplay.Location.Y - (moveStartPoint.Y - currentPoint.Y))
      moveStartPoint = currentPoint
    End If
  End Sub

  Private Sub picBxGraphicDisplay_Paint(ByVal sender As Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles picBxGraphicDisplay.Paint
    e.Graphics.Clear(Me._imageBackColor)
    If Not Me._thePath Is Nothing Then
      Dim tempPath As Drawing2D.GraphicsPath
      tempPath = CType(Me._thePath.Clone(), Drawing2D.GraphicsPath)
      If (tempPath.GetBounds.Width / tempPath.GetBounds.Height) <= (picBxGraphicDisplay.Width / picBxGraphicDisplay.Height) Then
        Dim scaleMatrix As New Drawing2D.Matrix
        scaleMatrix.Scale((picBxGraphicDisplay.Height - 6) / tempPath.GetBounds.Height, (picBxGraphicDisplay.Height - 6) / tempPath.GetBounds.Height, Drawing2D.MatrixOrder.Prepend)
        tempPath.Transform(scaleMatrix)
      Else
        Dim scaleMatrix As New Drawing2D.Matrix
        scaleMatrix.Scale((picBxGraphicDisplay.Width - 6) / tempPath.GetBounds.Width, (picBxGraphicDisplay.Width - 6) / tempPath.GetBounds.Width, Drawing2D.MatrixOrder.Append)
        tempPath.Transform(scaleMatrix)
      End If
      Dim forecolor As Color = Color.WhiteSmoke
      If Me._grayed Then
        forecolor = Color.Coral
      End If
      e.Graphics.DrawPath(New Pen(forecolor, 1), tempPath)
    End If

  End Sub

  Private Function IsHaunched() As Boolean
    Dim retVal As Boolean = False
    Dim strs() As String = Me._fileText.Split(vbCr)
    For i As Integer = 0 To strs.Count - 1
      If strs(i) = "Haunch used on Tenon" Then
        retVal = CBool(strs(i + 1))
      End If
    Next
    Return retVal
  End Function

  Private Function EstimatePartLength(ByVal len() As String) As Single
    Dim retVal As Single = CSng(len(1))
    retVal += CSng(len(4) / 2)
    Return retVal
  End Function

  Private Sub ZoomPix(ByVal plus As Boolean, ByVal aPanel As Panel, ByRef aPix As PictureBox, Optional ByVal xloc As Integer = 0)
    Dim sz As New Size(aPix.Size.Width, aPix.Size.Height)
    Dim propX As Single = (xloc / aPix.Width)
    Try
      If plus Then
        sz.Height = CInt(sz.Height + sz.Height)
        sz.Width = CInt(sz.Width + sz.Width)
        aPix.Size = sz
      Else
        aPix.Size = New Size(aPanel.Width - aPanel.Margin.Left - aPanel.Margin.Right, aPanel.Height - aPanel.Margin.Top - aPanel.Margin.Bottom)
      End If
      If xloc = 0 Then
        aPix.Location = New Point(aPanel.Margin.Left, aPanel.Margin.Top)
      Else
        Dim currLoc As Point = aPix.PointToScreen(aPix.Location) - aPanel.PointToScreen(aPanel.Location)
        Dim newLeft As Integer = CInt((aPix.Width * propX) - (aPanel.Width / 2))
        aPix.Location = New Point(-newLeft, currLoc.Y)
        'aPix.Location = New Point(((aPix.Left - aPanel.Margin.Left) + xloc) + (aPanel.Width / 2), aPanel.Margin.Top)
      End If
      aPix.Refresh()
    Catch ex As Exception
      MsgBox("ZOOM failed. " & ex.Message)
    End Try
  End Sub

  Private Sub FramePartImagePanel_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
    Panel1.BackColor = Me._imageBackColor
    ZoomPix(False, Panel1, picBxGraphicDisplay)
    picBxGraphicDisplay.Refresh()
  End Sub

#End Region

End Class
