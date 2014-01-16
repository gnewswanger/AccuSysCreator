Imports System.Xml
Imports System.IO

''' <summary>
''' FILE: AccuOperation.vb
''' AUTHOR: Galen Newswanger
''' This class contains the parameters required for generating operation data.
''' It also returns the various GraphicPath objects required for drawing itself
''' on the display screen.
''' </summary>
''' <remarks></remarks>
Public Class AccuOperation
    Inherits Object

    Private opName As String
    Private opNumber As String
    Private opPosition As Single
    Private doHaunch As Boolean
    Private hDepth As Single
    Private doMortise As Boolean
    Private hLen As Single
    Private mDepth As Single
    Private mLen As Single
    Private mOffset As Single
    Private doPilot As Boolean
    Private pDepth As Single
    Private pLen As Single
    Dim BetweenPilots As Single
    Private opPath As Drawing2D.GraphicsPath
    Private opText As String

    Public Property Name() As String
        Get
            Return opName
        End Get
        Set(ByVal Value As String)
            opName = Value
        End Set
    End Property

    Public ReadOnly Property Number() As String
        Get
            Return opNumber
        End Get
    End Property

    Public Property Position() As Single
        Get
            Return opPosition
        End Get
        Set(ByVal Value As Single)
            Dim op As Single = opPosition
            opPosition = Value
            If opPosition <> op Then
                Me.opPath.Reset()
                SetOpPath()
            End If
        End Set
    End Property

    Public Property HaunchDepth() As Single
        Get
            Return hDepth
        End Get
        Set(ByVal Value As Single)
            Dim hd As Single = hDepth
            hDepth = Value
            If hDepth <> hd Then
                Me.opPath.Reset()
                SetOpPath()
            End If
        End Set
    End Property

    Public Property HaunchLength() As Single
        Get
            Return hLen
        End Get
        Set(ByVal Value As Single)
            Dim hl As Single = hLen
            hLen = Value
            If hLen <> hl Then
                Me.opPath.Reset()
                SetOpPath()
            End If
        End Set
    End Property

    Public Property MortiseDepth() As Single
        Get
            Return mDepth
        End Get
        Set(ByVal Value As Single)
            Dim md As Single = mDepth
            mDepth = Value
            If mDepth <> md Then
                Me.opPath.Reset()
                SetOpPath()
            End If
        End Set
    End Property

    Public Property MortiseLength() As Single
        Get
            Return mLen
        End Get
        Set(ByVal Value As Single)
            Dim ml As Single = mLen
            mLen = Value
            If mLen <> ml Then
                Me.opPath.Reset()
                SetOpPath()
            End If
        End Set
    End Property

    Public Property Haunch() As Boolean
        Get
            Return doHaunch
        End Get
        Set(ByVal Value As Boolean)
            Dim dh As Boolean = doHaunch
            doHaunch = Value
            If Not doHaunch = dh Then
                Me.opPath.Reset()
                SetOpPath()
            End If
        End Set
    End Property

    Public Property Mortise() As Boolean
        Get
            Return doMortise
        End Get
        Set(ByVal Value As Boolean)
            Dim dm As Boolean = doMortise
            doMortise = Value
            If Not doMortise = dm Then
                Me.opPath.Reset()
                SetOpPath()
            End If
        End Set
    End Property

    Public Property MortiseOffset() As Single
        Get
            Return mOffset
        End Get
        Set(ByVal Value As Single)
            Dim mo As Single = mOffset
            mOffset = Value
            If mOffset <> mo Then
                Me.opPath.Reset()
                SetOpPath()
            End If
        End Set
    End Property

    Public Property Pilot() As Boolean
        Get
            Return doPilot
        End Get
        Set(ByVal Value As Boolean)
            Dim dp As Boolean = doPilot
            doPilot = Value
            If Not doPilot = dp Then
                Me.opPath.Reset()
                SetOpPath()
            End If
        End Set
    End Property

    Public Property PilotDepth() As Single
        Get
            Return pDepth
        End Get
        Set(ByVal Value As Single)
            Dim pd As Single = pDepth
            pDepth = Value
            If pDepth <> pd Then
                Me.opPath.Reset()
                SetOpPath()
            End If
        End Set
    End Property

    Public Property PilotDiameter() As Single
        Get
            Return pLen
        End Get
        Set(ByVal Value As Single)
            Dim pl As Single = pLen
            pLen = Value
            If pLen <> pl Then
                Me.opPath.Reset()
                SetOpPath()
            End If
        End Set
    End Property

    Public ReadOnly Property OperationGraphicPath() As Drawing2D.GraphicsPath
        Get
            If Me.opPath.PointCount = 0 Then
                Return Nothing
            Else
                Return Me.opPath
            End If
        End Get
    End Property

    Public ReadOnly Property OperationText() As String
        Get
            Return Me.opText
        End Get
    End Property

    Public ReadOnly Property Pilot2Pilot() As Single
        Get
            Return BetweenPilots
        End Get
    End Property

    Public Sub New(ByVal name As String, ByVal num As Integer, ByVal centerOfX As Single, _
    ByVal haunch As Boolean, ByVal haunchDepth As Single, ByVal haunchLength As Single, _
    ByVal mortise As Boolean, ByVal mortiseDepth As Single, ByVal mortiseLength As Single, ByVal mortiseOffset As Single, _
    ByVal predrill As Boolean, Optional ByVal drillDepth As Single = 0, Optional ByVal drillDia As Single = 0, _
    Optional ByVal betweenDrills As Single = 0) ', _

        opName = name & num
        opNumber = CStr(num)
        opPosition = centerOfX
        doHaunch = haunch
        hDepth = haunchDepth
        hLen = haunchLength
        doMortise = mortise
        mDepth = mortiseDepth
        mLen = mortiseLength
        mOffset = mortiseOffset
        doPilot = predrill
        pDepth = drillDepth
        pLen = drillDia
        BetweenPilots = betweenDrills
        'hmDepth = hingeMortiseDepth
        'hmlen = hingeMortiseLength
        Me.opPath = New Drawing2D.GraphicsPath
        SetOpText()
        SetOpPath()
    End Sub

    Public Sub New(ByVal opArgs As OperationEventArgs)
        opName = opArgs.Name & opArgs.Number
        opNumber = CStr(opArgs.Number)
        opPosition = opArgs.CurrPos
        doHaunch = opArgs.Haunched
        hDepth = opArgs.HaunchDepth
        hLen = opArgs.HaunchLength
        doMortise = opArgs.Mortised
        mDepth = opArgs.MortiseDepth
        mLen = opArgs.MortiseLength
        mOffset = opArgs.MortiseOffset
        doPilot = opArgs.Predrilled
        pDepth = opArgs.DrillDepth
        pLen = opArgs.DrillDia
        BetweenPilots = opArgs.DistTweenDrill
        Me.opPath = New Drawing2D.GraphicsPath
        SetOpText()
        SetOpPath()
    End Sub

    Private Sub SetOpPath()
        If doHaunch Then
            Me.opPath.AddPath(GetHaunchPath(New Point(Module1.InchesToPixels(opPosition), Module1.InchesToPixels(0))), False)
        End If
        If doMortise Then
            Me.opPath.AddPath(GetMortisePath(New Point(Module1.InchesToPixels(opPosition), Module1.InchesToPixels(0))), False)
        End If
        If doPilot Then
            Me.opPath.AddPath(GetPilotPath(New Point(Module1.InchesToPixels(opPosition), Module1.InchesToPixels(0))), False)
        End If
    End Sub
    Public Function GetHaunchPath(ByVal posAsPixels As Point) As Drawing2D.GraphicsPath
        Dim thisPath As New Drawing2D.GraphicsPath
        Dim pts(3) As Point
        pts(0) = New Point(posAsPixels.X - Module1.InchesToPixels((hLen / 2) + hDepth), posAsPixels.Y)
        pts(1) = New Point(posAsPixels.X - Module1.InchesToPixels(hLen / 2), Module1.InchesToPixels(hDepth))
        pts(2) = New Point(posAsPixels.X + Module1.InchesToPixels(hLen / 2), Module1.InchesToPixels(hDepth))
        pts(3) = New Point(posAsPixels.X + Module1.InchesToPixels((hLen / 2) + hDepth), posAsPixels.Y)
        thisPath.AddLines(pts)
        Return thisPath
    End Function

    Public Function GetMortisePath(ByVal posAsPixels As Point) As Drawing2D.GraphicsPath
        Dim thisPath As New Drawing2D.GraphicsPath
        Dim pts(3) As Point
        pts(0) = New Point(posAsPixels.X - Module1.InchesToPixels(mLen / 2) + Module1.InchesToPixels(mOffset), Module1.InchesToPixels(hDepth * doHaunch.GetHashCode))
        pts(1) = New Point(posAsPixels.X - Module1.InchesToPixels(mLen / 2) + Module1.InchesToPixels(mOffset), Module1.InchesToPixels(mDepth + (hDepth * doHaunch.GetHashCode)))
        pts(2) = New Point(posAsPixels.X + Module1.InchesToPixels(mLen / 2) + Module1.InchesToPixels(mOffset), Module1.InchesToPixels(mDepth + (hDepth * doHaunch.GetHashCode)))
        pts(3) = New Point(posAsPixels.X + Module1.InchesToPixels(mLen / 2) + Module1.InchesToPixels(mOffset), Module1.InchesToPixels((hDepth * doHaunch.GetHashCode)))
        thisPath.AddLines(pts)
        Return thisPath
    End Function

    Public Function GetPilotPath(ByVal posAsPixels As Point) As Drawing2D.GraphicsPath
        Dim thisPath As New Drawing2D.GraphicsPath
        Dim pts(3) As Point
        pts(0) = New Point(posAsPixels.X - Module1.InchesToPixels(pLen / 2), Module1.InchesToPixels(hDepth))
        pts(1) = New Point(posAsPixels.X - Module1.InchesToPixels(pLen / 2), Module1.InchesToPixels(pDepth + hDepth))
        pts(2) = New Point(posAsPixels.X + Module1.InchesToPixels(pLen / 2), Module1.InchesToPixels(pDepth + hDepth))
        pts(3) = New Point(posAsPixels.X + Module1.InchesToPixels(pLen / 2), Module1.InchesToPixels(hDepth))
        thisPath.AddLines(pts)
        Return thisPath
    End Function

    Private Function SetOpText() As String
        Me.opText = opNumber & vbTab & opPosition.ToString("##0.000") & vbTab & hDepth.ToString("##0.000") _
        & vbTab & doHaunch.GetHashCode & vbTab & hLen.ToString("##0.000") & vbTab & doMortise.GetHashCode _
        & vbTab & mLen.ToString("##0.000") & vbTab & mDepth.ToString("##0.000") & vbTab _
        & doPilot.GetHashCode & vbTab & mOffset.ToString("##0.000")
        Return Me.opText
    End Function

End Class
