Imports System.IO

''' <summary>
''' <file>File: FrameListClass.vb</file>
''' <author>Author: Galen Newswanger</author>
''' 
''' FrameListClass is the top class in a hierarchy of classes that represent the various 
''' objects that make up the frame of a cabinet job.
''' It contains a Generic List of class FrontFrame. 
''' </summary>
''' <remarks>
''' Class FrontFrame contains 2 lists of class FramePart, one that contains stiles and rails objects
''' and another to contain opening objects.
''' Class FramePart contains an array of PartEdge objects, one for each edge to be operated on.
''' Class PartEdge contains a list of AccuOperation objects.
''' Class AccuOperation represents an operation line in the parameter file ("acc") that is 
''' generated for input into the MTH machine.
''' </remarks>
Public Class FrameListClass

    Private _myDbRef As DataClass   'This is a reference to the instance of DataClass located in the MainForm class.
    Private _jobNumber As String
    Private _frameList As New Generic.List(Of FrontFrame)
    Private _currFrameIndex As Integer
    Private _currPartIndex As Integer
    Private _programList As New ArrayList 'of ProgramInfoClass

#Region "Class Properties"

    Public Property JobNumber() As String
        Get
            Return Me._jobNumber
        End Get
        Set(ByVal value As String)
            Me._jobNumber = value
        End Set
    End Property

    Public ReadOnly Property DataRef() As DataClass
        Get
            Return Me._myDbRef
        End Get
    End Property

    Public ReadOnly Property FrameCount() As Integer
        Get
            Return Me._frameList.Count
        End Get
    End Property

    Public Property CurrentFrameIndex() As Integer
        Get
            Return Me._currFrameIndex
        End Get
        Set(ByVal Value As Integer)
            SetCurrentFrameByIndex(Value)
        End Set
    End Property

    Public Property CurrentPartIndex() As Integer
        Get
            Return Me._currPartIndex
        End Get
        Set(ByVal Value As Integer)
            SetCurrentPartByIndex(Value)
        End Set
    End Property

    Public ReadOnly Property CurrentFrame() As FrontFrame
        Get
            Return GetCurrentFrame()
        End Get
    End Property

    Public ReadOnly Property CurrentPart() As FramePart
        Get
            Return GetCurrentPart()
        End Get
    End Property

    Public ReadOnly Property MaterialDimensionList() As Generic.List(Of FrameMaterialClass)
        Get
            Return Me.GetFrameMaterialList()
        End Get
    End Property


#End Region

    Public Sub New(ByRef db As DataClass, ByVal jobNo As String)
        Me._myDbRef = db
        Me._jobNumber = jobNo
        If Not IsNothing(jobNo) Then
            Me._frameList = Me._myDbRef.GetJobFramesList(jobNo)
        End If
        SetCurrentFrameByIndex(0)
        SetCurrentPartByIndex(0)
    End Sub

    Public Sub New(ByRef db As DataClass, ByVal jobNo As String, ByVal list As Generic.List(Of FrontFrame))
        Me._myDbRef = db
        Me._jobNumber = jobNo
        Me._frameList = list
        SetCurrentFrameByIndex(0)
        SetCurrentPartByIndex(0)
    End Sub

    Public Sub AddFrame2List(ByVal fr As FrontFrame)
        Me._frameList.Add(fr)
        If Me._frameList.Count = 1 Then
            SetCurrentFrameByIndex(0)
            SetCurrentPartByIndex(0)
        End If
    End Sub

    Private Function GetCurrentFrame() As FrontFrame
        If Me._currFrameIndex > -1 AndAlso Me._currFrameIndex < Me._frameList.Count _
        AndAlso Me._currPartIndex < Me._frameList(Me._currFrameIndex).PartsList.Count Then
            Return Me._frameList(Me._currFrameIndex)
        End If
        Return Nothing
    End Function

    Private Function GetCurrentPart() As FramePart
        If Me.CurrentPartIndex > -1 Then
            If Me.CurrentFrameIndex > -1 AndAlso Me.CurrentFrameIndex < Me._frameList.Count _
             AndAlso Me.CurrentPartIndex < Me._frameList(Me.CurrentFrameIndex).PartsList.Count Then
                Return Me._frameList(Me.CurrentFrameIndex).PartsList(Me.CurrentPartIndex)
            End If
        End If
        Return Nothing
    End Function

    Public Function GetSinglePart(ByVal ptName As String) As FramePart
        Dim strs() As String = ptName.Split(CChar("."))
        Return GetFrameByName(strs(0) + "." + strs(1)).GetSinglePart(ptName)
    End Function

    Public Function GetSinglePartByDescription(ByVal desc As String) As FramePart
        If desc = String.Empty Then
            Return Nothing
        Else
            Dim strs() As String = desc.Split(CChar("."))
            Dim frObj As FrontFrame = GetFrameByName(Me._jobNumber + "." + strs(0))
            Return frObj.GetSinglePartByDescription(desc)
        End If
    End Function

    Public Function GetSinglePartByPartedgename(ByVal edgeName As String) As FramePart
        Dim strs() As String = edgeName.Split(CChar("."))
        Return GetFrameByName(strs(0) + "." + strs(1)).GetSinglePart(Me.PartNameFromEdgeName(edgeName))
    End Function

    Public Function GetFrameByIndex(ByVal num As Integer) As FrontFrame
        If Not IsNothing(Me._frameList(num)) Then
            Return Me._frameList(num)
        End If
        Return Nothing
    End Function

    Public Function GetFrameByName(ByVal frname As String) As FrontFrame
        For i As Integer = 0 To Me._frameList.Count - 1
            If Me._frameList(i).Name.ToUpper = frname.ToUpper Then
                Dim retVal As FrontFrame = Me._frameList(i)
                Return retVal
            End If
        Next
        Return Nothing
    End Function

    Public Sub SetFrameList(Optional ByVal jobNo As String = Nothing)
        If Not IsNothing(jobNo) Then
            Me._jobNumber = jobNo
        End If
        Me._frameList = Me._myDbRef.GetJobFramesList(Me._jobNumber)
        If IsNothing(CurrentFrame) Then
            SetCurrentFrameByIndex(0)
            SetCurrentPartByIndex(0)
        End If
    End Sub

    Public Sub SetCurrentFrameByItem(ByVal itemNo As String)
        If Me._frameList.Count > 0 Then
            For i As Integer = 0 To Me._frameList.Count - 1
                If Me._frameList(i).ItemNo = itemNo Then
                    Me._currFrameIndex = i
                    Me._currPartIndex = -1
                    Exit For
                End If
            Next
        Else
            Me._currFrameIndex = -1
            Me._currPartIndex = -1
            MsgBox("Item number " & itemNo & " was not found.")
        End If
    End Sub

    Private Sub SetCurrentFrameByIndex(ByVal num As Integer)
        If Me._frameList.Count > num Then
            Me._currFrameIndex = num
        Else
            If Me._frameList.Count > 0 Then
                Me._currFrameIndex = 0
            Else
                Me._currFrameIndex = -1
                Me._currPartIndex = -1
            End If
            'MsgBox(num & " is greater the the frame list count.")
        End If
    End Sub

    Private Sub SetCurrentPartByIndex(ByVal num As Integer)
        If Me._currFrameIndex > -1 AndAlso Me._frameList.Count > Me._currFrameIndex AndAlso Me._frameList(Me._currFrameIndex).PartsList.Count > num Then
            Me._currPartIndex = num
        Else
            If Not IsNothing(CurrentFrame) AndAlso CurrentFrame.PartsList.Count > 0 Then
                Me._currPartIndex = 0
            Else
                Me._currPartIndex = -1
            End If
            'MsgBox(num & " is greater the the part list count.")
        End If
    End Sub

    Public Sub SetCurrentPartByName(ByVal name As String)
        If Me._currFrameIndex > -1 AndAlso Me._frameList.Count > Me._currFrameIndex Then
            For i As Integer = 0 To Me._frameList(Me._currFrameIndex).PartsList.Count - 1
                If Me._frameList(Me._currFrameIndex).PartsList(i).Name = name Then
                    Me._currPartIndex = i
                    Exit Sub
                End If
            Next
        End If
        If Not IsNothing(CurrentFrame) AndAlso CurrentFrame.PartsList.Count > 0 Then
            Me._currPartIndex = 0
        Else
            Me._currPartIndex = -1
        End If
        MsgBox("Part name " & name & " was not found.")

    End Sub

    Public Sub DeleteFramePartsForItemNo(ByVal itemNo As String)
        For i As Integer = 0 To Me._frameList.Count - 1
            If Me._frameList(i).ItemNo = itemNo Then
                Me._frameList(i).PartsList.Clear()
                Me._frameList(i).Openinglist.Clear()
                Exit Sub
            End If
        Next
        MsgBox("Item number " & itemNo & " was not found.")
    End Sub

    Public Sub ClearFrameList()
        Me._frameList.Clear()
        Me._frameList.TrimExcess()
    End Sub

    Private Function FrontFrameAsCSVRecord(ByVal fr As FrontFrame) As String
        Dim retVal As String
        retVal = fr.WoodSpecie & ",B," & fr.Name.Split(CChar("."))(0) & "," & fr.Name.Split(CChar("."))(1) & ",FR," & fr.FrontframeSize.Width & "," & fr.FrontframeSize.Height & ", 0.000,,,,"
        Return retVal
    End Function

    Public Sub SaveToCsv(ByVal jobFileName As String, ByVal bcDir As String)
        Dim fs As StreamWriter = New StreamWriter(New FileStream(jobFileName, FileMode.Create))
        Try
            For i As Integer = 0 To Me._frameList.Count - 1
                fs.WriteLine(FrontFrameAsCSVRecord(Me._frameList(i)))
                Dim pList As Generic.List(Of FramePart) = Me._frameList(i).PartsList
                Dim fp As FramePart
                For Each fp In pList
                    If fp.IncludeInCutlist Then
                        fs.WriteLine(ProduceCSVRecord(bcDir, fp, Me._frameList(i).WoodSpecie))
                    End If
                Next
                pList = Me._frameList(i).Openinglist
                For Each fp In pList
                    fs.WriteLine(ProduceCSVRecord(bcDir, fp, Me._frameList(i).WoodSpecie))
                Next
            Next

        Catch ex As Exception
            MsgBox("SaveToCsv failed: " & ex.Message)
        Finally
            fs.Close()
        End Try
    End Sub
    Private Function ProduceCSVRecord(ByVal bcDir As String, ByVal fp As FramePart, ByVal wood As String) As String
        Dim retVal As String
        retVal = wood & "," & fp.Grade & "," & _
            fp.Name.Split(CChar("."))(0) & "," & fp.Name.Split(CChar("."))(1) & "," & fp.Name.Split(CChar("."))(2) _
            & "," & fp.Width & "," & fp.Length & "," & fp.Thickness & ","
        If Not fp.PartEdges(0).ProgramFilename = "" Then
            retVal += bcDir & Me._jobNumber & "\" & fp.PartEdges(0).ProgramFilename
        End If
        retVal += "," & Trim(fp.TSComments) & "," & Trim(fp.PartEdges(0).Comment) & ","
        If fp.PartEdges.GetUpperBound(0) = 1 AndAlso Not IsNothing(fp.PartEdges(1)) AndAlso Not fp.PartEdges(1).ProgramFilename = "" Then
            retVal += bcDir & Me._jobNumber & "\" & fp.PartEdges(1).ProgramFilename
        End If
        Return retVal
    End Function

    Private Function GetWriteableValue(ByVal obj As Object) As String
        If (obj Is Convert.DBNull OrElse obj Is Nothing) Then
            Return "0"
        ElseIf (obj.ToString().IndexOf(",") > -1) Then
            obj.ToString().Replace(",", "|")
            Return obj.ToString()
        Else
            Return obj.ToString()
        End If
    End Function

    Public Function FindPartEdge(ByVal peName As String) As PartEdge
        Dim strs() As String
        strs = peName.Split(CChar("."))
        Dim fn As String = Trim(strs(0)) & "." & Trim(strs(1))
        Dim fr As FrontFrame
        Dim fp As FramePart
        Dim pe As PartEdge = Nothing
        Dim enF As IEnumerator = Me._frameList.GetEnumerator
        While enF.MoveNext
            If fn = CType(enF.Current, FrontFrame).Name Then
                fr = CType(enF.Current, FrontFrame)
                Dim enP As System.Collections.IEnumerator = fr.PartsList.GetEnumerator
                While enP.MoveNext
                    If PartNameFromEdgeName(peName) = CType(enP.Current, FramePart).Name Then
                        fp = CType(enP.Current, FramePart)
                        For i As Integer = 0 To fp.PartEdges.GetUpperBound(0)
                            If Not IsNothing(fp.PartEdges(i)) AndAlso fp.PartEdges(i).Name = peName Then
                                pe = fp.PartEdges(i)
                                Exit While
                            End If
                        Next
                    End If
                End While
                Exit While
            End If
        End While
        Return pe
    End Function

    Private Function PartNameFromEdgeName(ByVal edgeName As String) As String
        Dim strs() As String = edgeName.Split(CChar("."))
        If strs(2).StartsWith("RC") OrElse strs(2).StartsWith("SC") Then
            If strs(2).EndsWith("T") OrElse strs(2).EndsWith("B") OrElse strs(2).EndsWith("L") _
            OrElse strs(2).EndsWith("R") Then
                strs(2) = strs(2).Remove(strs(2).Length - 1, 1)
            End If
        End If
        Return [String].Join(".", strs)
    End Function

    Private Function GetFrameMaterialList() As Generic.List(Of FrameMaterialClass)
        Dim retList As New Generic.List(Of FrameMaterialClass)
        For Each item As FrontFrame In Me._frameList
            For Each part As FramePart In item.PartsList
                If ListOfFrameMaterial_ExistsAt(part.Width, retList) = -1 Then
                    retList.Add(New FrameMaterialClass(part.Width, part.ThicknessStyle.BoardHeight))
                End If
            Next
        Next
        Return retList
    End Function

    Private Function ListOfFrameMaterial_ExistsAt(ByVal num As Single, ByVal list As Generic.List(Of FrameMaterialClass)) As Integer
        For index As Integer = 0 To list.Count - 1
            If CType(list.Item(index), FrameMaterialClass).MaterialWidth = num Then
                Return index
            End If
        Next
        Return -1
    End Function


    Public Function GetMaterialLength(ByVal materialWidth As Single) As Single
        Dim register As Single
        For Each item As FrontFrame In Me._frameList
            For Each part As FramePart In item.PartsList
                If part.Width = materialWidth Then
                    register += part.Length
                End If
            Next
        Next
        Return register
    End Function

End Class
