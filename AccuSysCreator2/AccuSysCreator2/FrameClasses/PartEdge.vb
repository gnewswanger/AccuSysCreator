Imports System.IO
Imports System.Xml
Imports FrontFrameEventClasses

''' <summary>
''' <file>File: PartEdge.vb</file>
''' <author>Author: Galen Newswanger</author>
''' 
''' This class represents an edge of a FramePart.  It contains a list of
''' the adjoining FrameParts and a list of operations to accommodate each
''' part.
''' </summary>
''' <remarks></remarks>
Public Class PartEdge

    Private _mypart As FramePart
    Private _edgeName As String
    Private _partType As PartEdgeTypes
    Private _myStyle As StyleFramePart
    Private _frStyle As FrameStyle
    Private _operations As New ArrayList
    Private _adjoiningPartLst As New ArrayList   'List of part names
    Private _progName As String = String.Empty
    Private _progComment As String = String.Empty

    Private _currAdjPart As FramePart
    Private _mortShoulder As Single

#Region "PartEdge Class Properties"

    Public ReadOnly Property Name() As String
        Get
            Return Me._edgeName
        End Get
    End Property

    Public ReadOnly Property PeType() As FrontFrameEventClasses.PartEdgeTypes
        Get
            Return Me._partType
        End Get
    End Property

    Public ReadOnly Property OperationText() As Generic.List(Of String)
        Get
            Dim retList As New Generic.List(Of String)
            For Each oper As AccuOperation In Me._operations
                retList.Add(oper.OperationText.ToString)
            Next
            Return retList
        End Get
    End Property
    Public Property AdjoiningPartNameList() As ArrayList
        Get
            Return Me._adjoiningPartLst
        End Get
        Set(ByVal Value As ArrayList)
            Me._adjoiningPartLst = Value
        End Set
    End Property
    Public Property Operationlist() As ArrayList
        Get
            Return Me._operations
        End Get
        Set(ByVal Value As ArrayList)
            Me._operations = Value
        End Set
    End Property
    Public ReadOnly Property ProgramFilename() As String
        Get
            Return Me._progName
        End Get
    End Property
    Public Property Comment() As String
        Get
            Return Me._progComment
        End Get
        Set(ByVal Value As String)
            Me._progComment = Value
            Me._mypart.SaveToDB(False)
        End Set
    End Property
    Public ReadOnly Property FrontFrameStyle() As FrameStyle
        Get
            If Me._frStyle Is Nothing Then
                Return Module1.JobFrameStyle
            Else
                Return Me._frStyle
            End If
        End Get
    End Property
    Public ReadOnly Property DoHaunch() As Boolean
        Get
            Return Me.FrontFrameStyle.isHaunched
        End Get
    End Property
    Public ReadOnly Property DoMortise() As Boolean
        Get
            Return Me.FrontFrameStyle.isMortised
        End Get
    End Property

#End Region

    Public Sub New(ByVal fp As PartEdge)
        Me._mypart = fp._mypart
        Me._edgeName = fp._edgeName
        Me._partType = fp._partType
        Me._frStyle = fp._frStyle
        Me._operations = fp._operations
        Me._adjoiningPartLst = fp._adjoiningPartLst
        Me._progName = fp._progName
        Me._progComment = fp._progComment
        Me._currAdjPart = fp._currAdjPart
    End Sub

    Public Sub New(ByRef ownerPart As FramePart, ByVal ptname As String, ByVal ptType As PartEdgeTypes, ByVal prgName As String, ByVal prgComment As String)
        Me._mypart = ownerPart
        Me._edgeName = SetName(ptname, ptType)
        Me._partType = ptType
        Me._frStyle = Module1.JobFrameStyle
        Me._progName = prgName
        Me._progComment = prgComment
        If Me._adjoiningPartLst.Count > 0 Then
            Me._currAdjPart = GetAdjoiningPartByIndex(0)
        Else
            Me._currAdjPart = Nothing
        End If
    End Sub

    Public Sub New(ByRef ownerPart As FramePart, ByVal ptname As String, ByVal ptType As PartEdgeTypes, _
                    Optional ByVal prgComment As String = "")
        Me._mypart = ownerPart
        Me._edgeName = SetName(ptname, ptType)
        Me._partType = ptType
        Me._frStyle = Module1.JobFrameStyle
        Me._progComment = prgComment
        Me._currAdjPart = Nothing
    End Sub

    Private Function SetName(ByVal ptname As String, ByVal pt As PartEdgeTypes) As String
        Select Case pt
            Case FrontFrameEventClasses.PartEdgeTypes.RailCenterB
                If Not ptname.EndsWith("B") Then
                    ptname += "B"
                End If
            Case FrontFrameEventClasses.PartEdgeTypes.RailCenterT
                If Not ptname.EndsWith("T") Then
                    ptname += "T"
                End If
            Case FrontFrameEventClasses.PartEdgeTypes.StileCenterL
                If Not ptname.EndsWith("L") Then
                    ptname += "L"
                End If
            Case FrontFrameEventClasses.PartEdgeTypes.StileCenterR
                If Not ptname.EndsWith("R") Then
                    ptname += "R"
                End If
        End Select
        Return ptname
    End Function

    Private Function AdjustProgramName(ByVal peName As String) As String
        'me._partName = "[item number].[item code]"
        Dim retVal As String
        Dim strs() As String = peName.Split(CChar("."))
        Dim ofAny As Char() = (" -").ToCharArray
        While strs(2).LastIndexOfAny(ofAny) > -1
            strs(2) = strs(2).Remove(strs(2).LastIndexOfAny(ofAny), 1)
        End While
        retVal = strs(1) & "_" & strs(2)
        If retVal.Length > 8 Then
            retVal = retVal.Substring(0, 8)
        End If
        If Not retVal.EndsWith(".acc") Then
            retVal += ".acc"
        End If
        Return retVal

    End Function

    Public Function GetAdjoiningPartByName(ByVal ptName As String) As FramePart
        For i As Integer = 0 To Me._adjoiningPartLst.Count - 1
            If CType(Me._adjoiningPartLst(i), FramePart).Name = ptName Then
                Return Me._mypart.MyFrame.GetSinglePart(CStr(Me._adjoiningPartLst(i)))
            End If
        Next
        Return Nothing
    End Function

    Public Function GetAdjoiningPartByIndex(ByVal idx As Integer) As FramePart
        Return Me._mypart.MyFrame.GetSinglePart(CStr(Me._adjoiningPartLst(idx)))
    End Function

    Public Function AddOperation(ByVal op As AccuOperation) As Integer
        Try
            Return Me._operations.Add(op)
        Catch ex As Exception
            MsgBox("Add Operation failed. " & ex.Message)
        End Try
    End Function

    Public Function AddAdjoiningPart(ByVal ptName As String) As Integer
        Try
            Return Me._adjoiningPartLst.Add(ptName)
        Catch ex As Exception
            MsgBox("Add Adjoining Part failed. " & ex.Message)
        End Try
    End Function

    Private Function GetIsMortised() As Boolean
        If Me._partType = FrontFrameEventClasses.PartEdgeTypes.StileLeft OrElse _
        (Me._partType = FrontFrameEventClasses.PartEdgeTypes.TopRail AndAlso Me._mypart.MyFrame.CabinetType <> CabinetTypes.Wall) OrElse _
        (Me._partType = FrontFrameEventClasses.PartEdgeTypes.BotRail AndAlso Me._mypart.MyFrame.CabinetType = CabinetTypes.Wall) Then
            If Me._currAdjPart.noTenonAtZero Then
                Return False
            Else
                Return Me._frStyle.isMortised
            End If
        Else    '(If Me._partType = FrontFrameEventClasses.PartEdgeTypes.StileRight Then)
            If Me._currAdjPart.noTenonAtFarend Then
                Return False
            Else
                Return Me._frStyle.isMortised
            End If
        End If
    End Function

    Private Function GetIsHaunched() As Boolean
        If Me._partType = FrontFrameEventClasses.PartEdgeTypes.StileLeft OrElse _
        Me._partType = FrontFrameEventClasses.PartEdgeTypes.TopRail OrElse _
        Me._partType = FrontFrameEventClasses.PartEdgeTypes.StileCenterR OrElse _
        Me._partType = FrontFrameEventClasses.PartEdgeTypes.RailCenterB Then

            If Me._currAdjPart.noHaunchAtZero Then
                Return False
            Else
                Return Me._frStyle.isHaunched
            End If
        Else    '(If Me._partType = FrontFrameEventClasses.PartEdgeTypes.StileRight Then)
            If Me._currAdjPart.noHaunchAtFarend Then
                Return False
            Else
                Return Me._frStyle.isHaunched
            End If
        End If
    End Function

    'The position of an operation is the center of the haunch rout. To prevent a triangular point at the end of the piece,
    'the position for the haunch is adjusted toward the end 1/2 the depth of the haunch (1/2 width of the profile) and the length
    'of the haunch is increased by the depth of the haunch (the adjustment x 2). The center of the haunch and the center of 
    'the mortise are no longer the same and an adjustment must be made before running the mortise.  Since the hanuch at the point zero
    'end of the part is less than the center of the mortise, the adjustment must be added. The center of the haunch at the far end 
    'is greater than the mortise so the adjustment must be negative (subtracted).
    Private Function GetEndOffset(ByVal currPos As Single, ByRef refHaunchLen As Single, ByVal ptSize As FrameSize, _
     ByVal noTenonAtZero As Boolean, ByVal noTenonAtFarend As Boolean) As Single
        Me._mortShoulder = Me._frStyle.mortiseShoulder
        Dim retVal As Single
        If noTenonAtZero AndAlso currPos = 0 Then
            retVal = +Me._frStyle.haunchEndAdj
            If (Me._currAdjPart.Width / 2) < MinPosition(Me._mortShoulder) Then
                retVal = ((Module1.JobMachineTool.HaunchToolBottom / 2) + Me.FrontFrameStyle.haunchDepth) - (Me._currAdjPart.Width / 2)
                Me._mortShoulder = Me.FrontFrameStyle.mortiseShoulder - (MinPosition(Me._mortShoulder) - (Me._currAdjPart.Width / 2))
            End If
            refHaunchLen = (Me._currAdjPart.Width - (Me._frStyle.haunchDepth * 2)) + (retVal * 2)
        ElseIf noTenonAtFarend AndAlso currPos >= ptSize.Length - (Me._currAdjPart.Width + 0.015) Then
            retVal = -Me._frStyle.haunchEndAdj
            If (ptSize.Length - (currPos + (Me._currAdjPart.Width / 2)) < MinPosition(Me._mortShoulder)) Then
                retVal = -(((Module1.JobMachineTool.HaunchToolBottom / 2) + Me.FrontFrameStyle.haunchDepth) - (Me._currAdjPart.Width / 2))
                Me._mortShoulder = Me.FrontFrameStyle.mortiseShoulder - (MinPosition(Me._mortShoulder) - (Me._currAdjPart.Width / 2))
            End If
            refHaunchLen = (Me._currAdjPart.Width - (Me._frStyle.haunchDepth * 2)) + (-retVal * 2)
        Else
            retVal = 0
        End If
        Return retVal
    End Function

    Private Function MinPosition(ByVal mortiseShoulder As Single) As Single
        Return mortiseShoulder + ((Module1.JobMachineTool.MortiseToolDiameter + Me._mypart.OperationParmeters.minMortiseToolMove) / 2)
    End Function

    Private Sub Reset_Mortised_Haunched(ByRef args As OperationEventArgs)
        If ((Me._partType And (RightEdge Or BotEdge)) = Me._partType) Then
            args.Mortised = Not Me._currAdjPart.noTenonAtZero
            args.Haunched = Not Me._currAdjPart.noHaunchAtZero
        ElseIf ((Me._partType And (LeftEdge Or TopEdge)) = Me._partType) Then
            args.Mortised = Not Me._currAdjPart.noTenonAtFarend
            args.Haunched = Not Me._currAdjPart.noHaunchAtFarend
        End If
    End Sub

    'This is where the operation parameter args for the operation objects are generated.
    Public Sub SetOperations(ByVal cabType As CabinetTypes, ByVal currPart As FramePart)
        'Private _num As Integer
        'Private _mortiseDepth As Single
        'Private _mortiseOffset As Single

        Dim opArgs As New OperationEventArgs
        opArgs.Name = Me._edgeName
        opArgs.HaunchDepth = Me._frStyle.haunchDepth
        opArgs.Predrilled = False
        opArgs.DrillDepth = currPart.HingeStyle.pilotDepth
        opArgs.DrillDia = currPart.HingeStyle.pilotDia
        opArgs.DistTweenDrill = currPart.HingeStyle.distBetweenOuter

        opArgs.Haunched = Me._frStyle.isHaunched
        opArgs.Mortised = Me._frStyle.isMortised
        opArgs.CurrPos = 0.0
        Dim absPos As Single = 0.0

        Me.Operationlist.Clear()
        If Not currPart.noHaunchAtZero Then
            absPos += Me._frStyle.haunchDepth
        End If
        If Not currPart.noTenonAtZero Then
            absPos += Me._frStyle.tenonDepth
        End If
        Try
            For i As Integer = 0 To Me._adjoiningPartLst.Count - 1
                Me._currAdjPart = GetAdjoiningPartByIndex(i)

                If (Me._currAdjPart.noHaunchAtZero AndAlso (Me._partType And Module1.BotEdge) = Me._partType) _
                    OrElse (Me._currAdjPart.noHaunchAtFarend AndAlso (Me._partType And Module1.TopEdge) = Me._partType) Then
                    opArgs.HaunchLength = Me._currAdjPart.Width
                Else
                    opArgs.HaunchLength = Me._currAdjPart.Width - (Me._frStyle.haunchDepth * 2)
                End If

                opArgs.MortiseLength = Me._currAdjPart.Width - (Me._frStyle.mortiseShoulder * 2)
                opArgs.MortiseOffset = GetEndOffset(absPos, opArgs.HaunchLength, currPart.FramePartSize, currPart.noTenonAtZero, currPart.noTenonAtFarend)
                Dim mortTravel As Single = opArgs.MortiseLength - Module1.JobMachineTool.MortiseToolDiameter

                If Me._currAdjPart.Code.StartsWith("OP") Or Me._currAdjPart.Code.StartsWith("CUT") Then
                    Select Case Me._partType
                        Case (Me._partType And RightEdge)
                            If ((Me._currAdjPart.Hinging And HingePlacement.L) = HingePlacement.L) Then
                                SetHingeOperations(absPos, Me._currAdjPart.Length, Me._currAdjPart.HingeStyleRules, currPart.HingeStyle, opArgs.MortiseOffset)
                            Else
                                absPos += Me._currAdjPart.Length
                            End If
                        Case (Me._partType And LeftEdge)
                            If ((Me._currAdjPart.Hinging And HingePlacement.R) = HingePlacement.R) Then
                                SetHingeOperations(absPos, Me._currAdjPart.Length, Me._currAdjPart.HingeStyleRules, currPart.HingeStyle, opArgs.MortiseOffset)
                            Else
                                absPos += Me._currAdjPart.Length
                            End If
                        Case (Me._partType And TopEdge)
                            If ((Me._currAdjPart.Hinging And HingePlacement.B) = HingePlacement.B) Then
                                SetHingeOperations(absPos, Me._currAdjPart.Width, Me._currAdjPart.HingeStyleRules, currPart.HingeStyle, opArgs.MortiseOffset)
                            Else
                                absPos += Me._currAdjPart.Width
                            End If
                        Case (Me._partType And BotEdge)
                            If ((Me._currAdjPart.Hinging And HingePlacement.T) = HingePlacement.T) Then
                                SetHingeOperations(absPos, Me._currAdjPart.Width, Me._currAdjPart.HingeStyleRules, currPart.HingeStyle, opArgs.MortiseOffset)
                            Else
                                absPos += Me._currAdjPart.Width
                            End If
                    End Select
                Else
                    '==== Establish the end offset ============================

                    'Minimum mortise travel = 0.218"
                    'Minimum haunch length = 0.655"
                    'Minimum Position = (min mort len + mort tool dia) / 2 + mort shoulder ((0.218+0.312)/2+0.25=0.515)

                    Dim minPos As Single = Me._mortShoulder + ((Module1.JobMachineTool.MortiseToolDiameter + Me._mypart.OperationParmeters.minMortiseToolMove) / 2)
                    If mortTravel < Me._mypart.OperationParmeters.minMortiseToolMove Then
                        mortTravel = Me._mypart.OperationParmeters.minMortiseToolMove
                    End If

                    Me._mortShoulder = Me._frStyle.mortiseShoulder

                    '=========================================
                    If absPos = 0.0 And currPart.noTenonAtZero Then
                        If (Me._currAdjPart.Width / 2) < minPos Then
                            opArgs.MortiseOffset += (Me._currAdjPart.Width / 2) - ((Module1.JobMachineTool.HaunchToolBottom / 2) + Me._frStyle.haunchEndAdj)
                            Me._mortShoulder = Me._frStyle.mortiseShoulder - (minPos - (Me._currAdjPart.Width / 2))
                        End If
                    ElseIf (absPos >= currPart.Length - (Me._currAdjPart.Width + 0.015)) And currPart.noTenonAtFarend Then
                        If currPart.Length - (absPos + (Me._currAdjPart.Width / 2) + minPos) < 0 Then
                            opArgs.MortiseOffset += ((Module1.JobMachineTool.HaunchToolBottom / 2) + Me._frStyle.haunchEndAdj) - (Me._currAdjPart.Width / 2)
                            Me._mortShoulder = Me._frStyle.mortiseShoulder - (minPos - (Me._currAdjPart.Width / 2))
                        End If
                    Else
                        opArgs.MortiseOffset = 0
                    End If

                    Reset_Mortised_Haunched(opArgs)
                    '=========================================
                    Dim adjMortDepth As Single = 0.0

                    If mortTravel >= Me._mypart.OperationParmeters.minMortiseToolMove AndAlso (opArgs.HaunchLength >= Module1.JobMachineTool.HaunchToolBottom) Then
                        If ((Me._partType And (LeftEdge Or TopEdge)) = Me._partType) Then
                            If Me._currAdjPart.noTenonAtFarend Then
                                adjMortDepth = 0.0
                            Else
                                adjMortDepth = Me._currAdjPart.tenonLengthAtFar + (Me.FrontFrameStyle.mortiseDepth - Me.FrontFrameStyle.tenonDepth)
                            End If
                        Else
                            If Me._currAdjPart.noTenonAtZero Then
                                adjMortDepth = 0.0
                            Else
                                adjMortDepth = Me._currAdjPart.tenonLengthAtZero + (Me.FrontFrameStyle.mortiseDepth - Me.FrontFrameStyle.tenonDepth)
                            End If
                        End If
                        If adjMortDepth + Me._frStyle.haunchDepth >= currPart.Width Then
                            If MsgBox("Mortise depth (" + Me._frStyle.mortiseDepth.ToString + ") will penetrate through stile. " + vbCr _
                            + " Do you want to continue or cancel and edit parts?", MsgBoxStyle.OkCancel) = MsgBoxResult.Cancel Then
                                Exit Sub
                            End If
                        End If


                        If (Me._currAdjPart.Width > 14.5) Then
                            absPos += (Me._currAdjPart.Width) / 2
                            opArgs.CurrPos = absPos - opArgs.MortiseOffset
                            opArgs.Number = Operationlist.Count + 1
                            opArgs.Mortised = False
                            opArgs.MortiseLength = mortTravel
                            opArgs.MortiseDepth = adjMortDepth
                            Operationlist.Add(New AccuOperation(opArgs))
                            absPos += (Me._currAdjPart.Width / 2)

                        ElseIf Me._currAdjPart.Width > 6.0 Then
                            Dim mPos As Single = absPos
                            Dim mortiseLen As Single = (Me._currAdjPart.Width / 2) - (1 + Me._frStyle.mortiseShoulder)
                            mPos += (mortiseLen / 2) + Me._frStyle.mortiseShoulder
                            opArgs.Number = Operationlist.Count + 1
                            opArgs.Haunched = False
                            opArgs.MortiseLength = mortiseLen - Module1.JobMachineTool.MortiseToolDiameter
                            opArgs.MortiseDepth = adjMortDepth + opArgs.HaunchDepth
                            opArgs.MortiseOffset = 0.0
                            opArgs.CurrPos = mPos

                            Operationlist.Add(New AccuOperation(opArgs))

                            opArgs.MortiseOffset = GetEndOffset(absPos, opArgs.HaunchLength, currPart.FramePartSize, currPart.noTenonAtZero, currPart.noTenonAtFarend)
                            absPos += (Me._currAdjPart.Width) / 2
                            opArgs.Number = Operationlist.Count + 1
                            Reset_Mortised_Haunched(opArgs)
                            opArgs.Mortised = False
                            opArgs.MortiseLength = mortiseLen - Module1.JobMachineTool.MortiseToolDiameter
                            opArgs.MortiseDepth = adjMortDepth + opArgs.HaunchDepth
                            opArgs.CurrPos = absPos - opArgs.MortiseOffset
                            opArgs.MortiseOffset = 0.0

                            Operationlist.Add(New AccuOperation(opArgs))
                            mPos += mortiseLen + 2

                            opArgs.Number = Operationlist.Count + 1
                            Reset_Mortised_Haunched(opArgs)
                            opArgs.Haunched = False
                            opArgs.MortiseLength = mortiseLen - Module1.JobMachineTool.MortiseToolDiameter
                            opArgs.MortiseDepth = adjMortDepth + opArgs.HaunchDepth
                            opArgs.MortiseOffset = 0.0
                            opArgs.CurrPos = mPos

                            Operationlist.Add(New AccuOperation(opArgs))
                            absPos += (Me._currAdjPart.Width / 2)
                        Else
                            absPos += (Me._currAdjPart.Width) / 2
                            opArgs.Number = Operationlist.Count + 1
                            opArgs.MortiseLength = mortTravel
                            opArgs.MortiseDepth = adjMortDepth
                            opArgs.CurrPos = absPos - opArgs.MortiseOffset

                            Operationlist.Add(New AccuOperation(opArgs))
                            absPos += (Me._currAdjPart.Width / 2)
                        End If
                    Else
                        MsgBox("One of the following errors has occurred." + vbCr _
                            + "Mortise tool travel distance(" + mortTravel.ToString + ") is less than the minimum (" + Me._mypart.OperationParmeters.minMortiseToolMove.ToString + ")." _
                            + "Haunch length (" + (opArgs.HaunchLength + opArgs.MortiseOffset).ToString + ") is less than the tool bottom (" + Module1.JobMachineTool.HaunchToolBottom.ToString + ").")
                    End If

                End If
            Next
            Dim haunch As Single
            If currPart.noTenonAtFarend Then
                haunch = 0
            Else
                haunch = currPart.FrontFrameStyle.haunchDepth
            End If
            If Me.Operationlist.Count > 0 Then
                Dim diff As Single = currPart.Length - (absPos + currPart.tenonLengthAtFar + haunch)
                If diff > 0.01 Or diff < -0.01 Then
                    MsgBox("Adjoining parts do not match length of current part. There is a difference of " & diff.ToString & " inch(es).")
                End If
                Me._mypart.SaveToDB(True)
            End If
        Catch ex As Exception
            MsgBox("PartEdge.SetOperations failed. " & ex.Message)
        End Try
    End Sub

    'This sub is called by SetOperations() to generate the hinge mortise and predrilling parameters.
    Private Sub SetHingeOperations(ByRef currPos As Single, ByVal adjoinOpgHght As Single, ByVal hingeRules As HingeRule, ByVal hngStyle As Hinge, ByVal endOffset As Single)

        Dim hingeOffset As Single = (hingeRules.mortiseOffset + (hngStyle.mortiseWidth / 2))
        Dim pilotOffcenter As Single = hngStyle.distBetweenOuter / 2
        If (adjoinOpgHght < Me._currAdjPart.HingeStyleRules.minHght42Hinges) Then
            MessageBox.Show("This opening height is less than the minimum allowable for two hinges (" & Me._currAdjPart.HingeStyleRules.minHght42Hinges & " inches)")
        ElseIf hingeRules.overrideNumbeHinges > 0 Then
            Dim hingeSpacing As Single = (adjoinOpgHght - (hingeOffset * 2)) / (hingeRules.overrideNumbeHinges - 1)
            For i As Integer = 0 To hingeRules.overrideNumbeHinges - 1
                MortiseNPredrill4Hinge(currPos + hingeOffset + (hingeSpacing * i), pilotOffcenter, hngStyle, endOffset)
            Next
            currPos += adjoinOpgHght
        Else
            If (adjoinOpgHght >= Me._currAdjPart.HingeStyleRules.hghtRange42Lower) And _
            (adjoinOpgHght < Me._currAdjPart.HingeStyleRules.hghtRange42Upper) Then
                MortiseNPredrill4Hinge(currPos + hingeOffset, pilotOffcenter, hngStyle, endOffset)
                currPos += adjoinOpgHght
                MortiseNPredrill4Hinge(currPos - hingeOffset, pilotOffcenter, hngStyle, endOffset)
            ElseIf (adjoinOpgHght >= Me._currAdjPart.HingeStyleRules.hghtRange42Upper) _
            And (adjoinOpgHght < Me._currAdjPart.HingeStyleRules.hghtRange43Upper) Then
                MortiseNPredrill4Hinge(currPos + hingeOffset, pilotOffcenter, hngStyle, endOffset)
                currPos += (adjoinOpgHght / 2)
                MortiseNPredrill4Hinge(currPos, pilotOffcenter, hngStyle, endOffset)
                currPos += (adjoinOpgHght / 2)
                MortiseNPredrill4Hinge(currPos - hingeOffset, pilotOffcenter, hngStyle, endOffset)
            ElseIf (adjoinOpgHght >= Me._currAdjPart.HingeStyleRules.hghtRange43Upper) _
            And (adjoinOpgHght < Me._currAdjPart.HingeStyleRules.hghtRange44Upper) Then
                currPos += hingeOffset
                Dim hingeSpacing As Single = (adjoinOpgHght - (hingeOffset * 2)) / 3
                MortiseNPredrill4Hinge(currPos, pilotOffcenter, hngStyle, endOffset)
                MortiseNPredrill4Hinge(currPos + hingeSpacing, pilotOffcenter, hngStyle, endOffset)
                MortiseNPredrill4Hinge(currPos + (hingeSpacing * 2), pilotOffcenter, hngStyle, endOffset)
                MortiseNPredrill4Hinge(currPos + (hingeSpacing * 3), pilotOffcenter, hngStyle, endOffset)
                currPos += (adjoinOpgHght - hingeOffset)
            ElseIf (adjoinOpgHght >= Me._currAdjPart.HingeStyleRules.hghtRange44Upper) Then
                currPos += hingeOffset
                Dim hingeSpacing As Single = (adjoinOpgHght - (hingeOffset * 2)) / 4
                MortiseNPredrill4Hinge(currPos, pilotOffcenter, hngStyle, endOffset)
                MortiseNPredrill4Hinge(currPos + hingeSpacing, pilotOffcenter, hngStyle, endOffset)
                MortiseNPredrill4Hinge(currPos + (hingeSpacing * 2), pilotOffcenter, hngStyle, endOffset)
                MortiseNPredrill4Hinge(currPos + (hingeSpacing * 3), pilotOffcenter, hngStyle, endOffset)
                MortiseNPredrill4Hinge(currPos + (hingeSpacing * 4), pilotOffcenter, hngStyle, endOffset)
                currPos += (adjoinOpgHght - hingeOffset)

            ElseIf (adjoinOpgHght >= Me._currAdjPart.HingeStyleRules.minHght42Hinges) _
            And (adjoinOpgHght < Me._currAdjPart.HingeStyleRules.hghtRange42Lower) Then
                hingeOffset = (hingeRules.smallOpgMortOffset + (hngStyle.mortiseWidth / 2))
                MortiseNPredrill4Hinge(currPos + hingeOffset, pilotOffcenter, hngStyle, endOffset)
                currPos += adjoinOpgHght
                MortiseNPredrill4Hinge(currPos - hingeOffset, pilotOffcenter, hngStyle, endOffset)
            End If
        End If
    End Sub

    Private Sub MortiseNPredrill4Hinge(ByVal pos As Single, ByVal pilotOffCenter As Single, ByVal hngStyle As Hinge, ByVal endOffset As Single)
        Dim op As AccuOperation
        If hngStyle.isMortised Then
            op = New AccuOperation(Me._edgeName, Operationlist.Count + 1, pos, hngStyle.isMortised, _
            hngStyle.mortiseDepth, hngStyle.mortiseWidth, False, hngStyle.mortiseDepth, 0.0, _
            -endOffset, False)
            Operationlist.Add(op)
        End If
        If hngStyle.isPredrilled Then
            op = New AccuOperation(Me._edgeName, Operationlist.Count + 1, pos - pilotOffCenter, False, _
            hngStyle.mortiseDepth, 0.0, False, hngStyle.mortiseDepth, 0.0, _
            -endOffset, True, hngStyle.pilotDepth, hngStyle.pilotDia, hngStyle.distBetweenOuter)
            Operationlist.Add(op)
            If pilotOffCenter <> 0 Then
                op = New AccuOperation(Me._edgeName, Operationlist.Count + 1, pos + pilotOffCenter, False, _
                hngStyle.mortiseDepth, 0.0, False, hngStyle.mortiseDepth, 0.0, _
                -endOffset, True, hngStyle.pilotDepth, hngStyle.pilotDia, hngStyle.distBetweenOuter)
                Operationlist.Add(op)
            End If
        End If
    End Sub

    Public Sub WriteToFile(ByRef fp As FramePart, ByVal lPath As String)
        If IsNothing(Me._progName) OrElse Me._progName = "" Then
            Me._progName = AdjustProgramName(Me._edgeName)
        End If

        If File.Exists(lPath & Me._progName) Then
            File.Delete(lPath & Me._progName)
        End If
        Dim sw As StreamWriter = File.CreateText(lPath & Me._progName)
        Dim xmlFilename As String = Application.StartupPath & "\accTemplate.xml"
        Try
            If System.IO.File.Exists(xmlFilename) Then
                Dim reader As XmlTextReader = New XmlTextReader(xmlFilename)
                Try
                    reader.NameTable.Add("Header")
                    Do While reader.Read
                        If reader.Name.Equals("ProgLine") Then
                            reader.MoveToContent()
                            Dim ndText As String
                            Select Case reader.GetAttribute("name")
                                Case "ProgID"
                                    reader.Read()
                                    ndText = reader.ReadElementString("Header")
                                    sw.WriteLine(ndText)
                                Case "Ver"
                                    reader.Read()
                                    ndText = reader.ReadElementString("Header")
                                    sw.WriteLine(ndText)
                                    sw.WriteLine("100")
                                Case "BdHeight"
                                    reader.Read()
                                    ndText = reader.ReadElementString("Header")
                                    sw.WriteLine(ndText)
                                    ndText = fp.ThicknessStyle.BoardHeight.ToString("##0.000")
                                    sw.WriteLine(ndText)
                                Case "BdWidth"
                                    reader.Read()
                                    ndText = reader.ReadElementString("Header")
                                    sw.WriteLine(ndText)
                                    ndText = fp.Width.ToString("##0.000")
                                    sw.WriteLine(ndText)
                                Case "ToolCL"
                                    reader.Read()
                                    ndText = reader.ReadElementString("Header")
                                    sw.WriteLine(ndText)
                                    ndText = fp.ThicknessStyle.toolLine.ToString("##0.000")
                                    sw.WriteLine(ndText)
                                Case "DrillFeed"
                                    reader.Read()
                                    ndText = reader.ReadElementString("Header")
                                    sw.WriteLine(ndText)
                                    ndText = fp.OperationParmeters.drillFeedrate.ToString("##0.000")
                                    sw.WriteLine(ndText)
                                Case "TenonFeed"
                                    reader.Read()
                                    ndText = reader.ReadElementString("Header")
                                    sw.WriteLine(ndText)
                                    ndText = fp.OperationParmeters.tenonFeedrate.ToString("##0.000")
                                    sw.WriteLine(ndText)
                                Case "HaunchFeed"
                                    reader.Read()
                                    ndText = reader.ReadElementString("Header")
                                    sw.WriteLine(ndText)
                                    ndText = fp.OperationParmeters.haunchFeedrate.ToString("##0.000")
                                    sw.WriteLine(ndText)
                                Case "MortiseFeed"
                                    reader.Read()
                                    ndText = reader.ReadElementString("Header")
                                    sw.WriteLine(ndText)
                                    ndText = fp.OperationParmeters.mortiseFeedrate.ToString("##0.000")
                                    sw.WriteLine(ndText)
                                Case "TenonMode"
                                    reader.Read()
                                    ndText = reader.ReadElementString("Header")
                                    sw.WriteLine(ndText)
                                    ndText = fp.OperationParmeters.tenonMode.GetHashCode.ToString
                                    sw.WriteLine(ndText)
                                Case "CenterRail"
                                    reader.Read()
                                    ndText = reader.ReadElementString("Header")
                                    sw.WriteLine(ndText)
                                    ndText = fp.OperationParmeters.middleRail.GetHashCode.ToString
                                    sw.WriteLine(ndText)
                                Case "NumHoleOps"
                                    reader.Read()
                                    ndText = reader.ReadElementString("Header")
                                    sw.WriteLine(ndText)
                                    sw.WriteLine(OperationText.Count.ToString)
                                Case "Operations"
                                    reader.Read()
                                    ndText = reader.ReadElementString("Header")
                                    sw.WriteLine(String.Join(vbTab, ndText.Split(CChar(","))))
                                    For i As Integer = 0 To OperationText.Count - 1
                                        sw.WriteLine(OperationText.Item(i))
                                    Next
                                Case "RoutTop"
                                    reader.Read()
                                    ndText = reader.ReadElementString("Header")
                                    sw.WriteLine(ndText)
                                    ndText = fp.ThicknessStyle.routeTopWidth.ToString("##0.000")
                                    sw.WriteLine(ndText)
                                Case "RoutMiddle"
                                    reader.Read()
                                    ndText = reader.ReadElementString("Header")
                                    sw.WriteLine(ndText)
                                    ndText = fp.ThicknessStyle.routeMiddleWidth.ToString("##0.000")
                                    sw.WriteLine(ndText)
                                Case "RoutBottom"
                                    reader.Read()
                                    ndText = reader.ReadElementString("Header")
                                    sw.WriteLine(ndText)
                                    ndText = fp.ThicknessStyle.routeBottomWidth.ToString("##0.000")
                                    sw.WriteLine(ndText)
                                Case "HaunchToolLength"
                                    reader.Read()
                                    ndText = reader.ReadElementString("Header")
                                    sw.WriteLine(ndText)
                                    ndText = fp.OperationParmeters.haunchDepthAdj.ToString("##0.000")
                                    sw.WriteLine(ndText)
                                Case "TenonDepth"
                                    reader.Read()
                                    ndText = reader.ReadElementString("Header")
                                    sw.WriteLine(ndText)
                                    ndText = fp.FrontFrameStyle.tenonDepth.ToString("##0.000")
                                    sw.WriteLine(ndText)
                                Case "BtnLabel"
                                    reader.Read()
                                    ndText = reader.ReadElementString("Header")
                                    sw.WriteLine(ndText)
                                    'If fp.JobNumber.Length > 7 Then
                                    '  ndText = fp.JobNumber.Substring(0, 7)
                                    'Else
                                    ndText = fp.Name
                                    'End If
                                    sw.WriteLine(ndText)
                                Case "PilotCenter"
                                    reader.Read()
                                    ndText = reader.ReadElementString("Header")
                                    sw.WriteLine(ndText)
                                    Dim cntr As Single
                                    cntr = fp.HingeStyle.vCenterOuter + fp.FrontFrameStyle.pilotCenterAdj
                                    ndText = cntr.ToString("##0.000")
                                    sw.WriteLine(ndText)
                                Case "NumClamps"
                                    reader.Read()
                                    ndText = reader.ReadElementString("Header")
                                    sw.WriteLine(ndText)
                                    If fp.Length < fp.OperationParmeters.maxLen2Clamps Then
                                        ndText = fp.OperationParmeters.numClamps.ToString
                                    Else
                                        ndText = (fp.OperationParmeters.numClamps * 2).ToString
                                    End If
                                    sw.WriteLine(ndText)
                                Case "TenonHaunchWidth"
                                    reader.Read()
                                    ndText = reader.ReadElementString("Header")
                                    sw.WriteLine(ndText)
                                    ndText = fp.FrontFrameStyle.haunchDepth.ToString("##0.000")
                                    sw.WriteLine(ndText)
                                Case "HaunchOnTenon"
                                    reader.Read()
                                    ndText = reader.ReadElementString("Header")
                                    sw.WriteLine(ndText)
                                    ndText = fp.FrontFrameStyle.haunchOnTenon.GetHashCode.ToString
                                    sw.WriteLine(ndText)
                                Case "PilotDepth"
                                    reader.Read()
                                    ndText = reader.ReadElementString("Header")
                                    sw.WriteLine(ndText)
                                    ndText = fp.HingeStyle.pilotDepth.ToString("##0.000")
                                    sw.WriteLine(ndText)
                            End Select
                        End If
                    Loop

                Catch ex As Exception
                    MessageBox.Show(ex.Message)
                Finally
                    If Not reader Is Nothing Then
                        reader.Close()
                    End If
                End Try
            Else
                MsgBox("Template file, '" & xmlFilename & "' was not found. Write program file '" & Me._progName & "' was aborted.")
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            sw.Close()
        End Try
    End Sub

    Public Sub ResetProgramName(ByVal name As String, ByRef db As DataClass)
        Me._progName = AdjustProgramName(name)
        db.SetPartEdgeProgname(Me.Name, Me._progName)
    End Sub

    Public Sub SaveToDB(ByVal ptName As String, ByRef db As DataClass, ByVal leaveConnOpen As Boolean)
        If Me._operations.Count > 0 Then
            For i As Integer = 0 To Me._operations.Count - 1
                db.SetEdgeOperation(Me, CType(Me._operations(i), AccuOperation), True)
            Next
            db.DeleteEdgeAdjoiningParts(Me._edgeName)
            For i As Integer = 0 To Me._adjoiningPartLst.Count - 1
                db.SetAdjoiningPart(Me.Name, CStr(Me._adjoiningPartLst(i)), i, True)
            Next
        End If
        db.SetPartEdge(Me, leaveConnOpen)
    End Sub
End Class
