Imports System.Xml
Imports System.Data.SqlClient
Imports System.Text.RegularExpressions

Public Delegate Sub ProgressHandler(ByVal sender As Object, ByVal e As ProgressEventArgs)

''' <summary>
''' <file>File: DataClass.vb</file>
''' <author>Author: Galen Newswanger</author>
''' 
''' DataClass is a class intended to contain all calls to the database dealing with frame objects.
''' The AccuSysCreator database tables are located in MS SQL Server "QCCSQL". All calls access the
''' database via stored procedures.
''' </summary>
''' <remarks></remarks>
Public Class DataClass

    Public Shared Event DeletionProgress As ProgressHandler

#Region "Class Instance Variables"

    Private _conn As SqlConnection
    Private _xmlFilename As String
#End Region

#Region "Constructor and Initialization Methods"

    Public Sub New(ByVal filename As String)
        Me._xmlFilename = filename
        Me._conn = New SqlConnection(GetConnStrXml())
    End Sub

    Private Function GetConnStrXml() As String
        If System.IO.File.Exists(Me._xmlFilename) Then
            Dim xd As XmlDocument = New XmlDocument
            Dim xtr As XmlTextReader = New XmlTextReader(Me._xmlFilename)
            Try
                xd.Load(xtr)
                Return xd.SelectSingleNode("/SetupParameters/DbConnections/ConnectionString").InnerText
            Catch ex As XmlException
                MessageBox.Show(Me._xmlFilename & vbCr & ex.Message & vbCr & ex.LineNumber & vbCr & ex.LinePosition)
            Finally
                xtr.Close()
            End Try
        End If
        Return Nothing
    End Function
#End Region

#Region "Class Instance Deletion Methods"

    Public Function DeleteCompleteJob(ByVal jobNo As String) As Integer 'Number rows effected"
        Dim retVal As Integer = 0
        If Not Me._conn.State = ConnectionState.Open Then
            Me._conn.Open()
        End If
        retVal += DeleteJobFrontFrame(jobNo)
        RaiseEvent DeletionProgress(Me, New ProgressEventArgs(20))
        retVal += DeleteJobFrameParts(jobNo)
        RaiseEvent DeletionProgress(Me, New ProgressEventArgs(40))
        retVal += DeleteJobEdgeOperations(jobNo)
        RaiseEvent DeletionProgress(Me, New ProgressEventArgs(60))
        retVal += DeleteJobAdjoiningParts(jobNo)
        RaiseEvent DeletionProgress(Me, New ProgressEventArgs(80))
        retVal += DeleteJobPartEdges(jobNo)
        RaiseEvent DeletionProgress(Me, New ProgressEventArgs(100))
        Return retVal
    End Function

    Public Function PurgeJobDataByDate(ByVal dt As DateTime) As Integer 'Number rows effected"
        Dim retVal As Integer = 0
        If Not Me._conn.State = ConnectionState.Open Then
            Me._conn.Open()
        End If
        Dim list As ArrayList = GetJobNumberListByLastupdate(dt)
        For i As Integer = 0 To list.Count - 1
            retVal += DeleteJobFrontFrame(CStr(list(i)))
            RaiseEvent DeletionProgress(Me, New ProgressEventArgs(20))
            retVal += DeleteJobFrameParts(CStr(list(i)))
            RaiseEvent DeletionProgress(Me, New ProgressEventArgs(40))
            retVal += DeleteJobEdgeOperations(CStr(list(i)))
            RaiseEvent DeletionProgress(Me, New ProgressEventArgs(60))
            retVal += DeleteJobAdjoiningParts(CStr(list(i)))
            RaiseEvent DeletionProgress(Me, New ProgressEventArgs(80))
            retVal += DeleteJobPartEdges(CStr(list(i)))
            RaiseEvent DeletionProgress(Me, New ProgressEventArgs(100))
        Next
        Me._conn.Close()
        Return retVal
    End Function
#End Region

#Region "Front Frame Methods"

    Public Function GetJobItemNoList(ByVal jobNo As String) As String() 'of String
        Dim sqlCmd As New SqlCommand("EXECUTE spGetJobItemNoList @JobNumber = '" & jobNo & "'", Me._conn)
        If Not Me._conn.State = ConnectionState.Open Then
            Me._conn.Open()
        End If
        Dim rdr As SqlDataReader = Nothing
        Try
            rdr = sqlCmd.ExecuteReader
            Dim retStrs() As String = Nothing
            Dim index As Integer = 0
            While rdr.Read
                ReDim Preserve retStrs(index)
                retStrs(index) = Trim(rdr.GetString(0))
                index += 1
            End While
            Return retStrs
        Catch ex As Exception
            MsgBox("GetJobItemNoList SqlDataReader failed. " + ex.Message)
        Finally
            rdr.Close()
            Me._conn.Close()
        End Try
        Return Nothing
    End Function

    Public Function GetJobNumberListByLastupdate(ByVal dt As DateTime) As ArrayList 'of String
        Dim sqlCmd As New SqlCommand("EXECUTE spGetJobNoListByUpdate @LastUpdate = '" & dt & "'", Me._conn)
        Dim rdr As SqlDataReader = Nothing
        If Not Me._conn.State = ConnectionState.Open Then
            Me._conn.Open()
        End If
        Try
            rdr = sqlCmd.ExecuteReader
            Dim retList As New ArrayList
            While rdr.Read
                retList.Add(Trim(rdr.GetString(0)))
            End While
            Return retList
        Catch ex As Exception
            MsgBox("GetJobNumberListByLastupdate SqlDataReader failed. " + ex.Message)
        Finally
            rdr.Close()
            Me._conn.Close()
        End Try
        Return Nothing
    End Function

    Public Function JobDataExists(ByVal jobNo As String, Optional ByVal leaveConnOpen As Boolean = True) As Boolean
        Dim sqlCmd As New SqlCommand("Execute  spJobDataExists @JobNumber = '" & jobNo & "'", Me._conn)
        If Not Me._conn.State = ConnectionState.Open Then
            Me._conn.Open()
        End If
        Try
            If IsNothing(sqlCmd.ExecuteScalar) Then
                Return False
            Else
                Return True
            End If
        Catch ex As Exception
            MsgBox("JobDataExists ExecuteScalar failed. " + ex.Message)
        Finally
            If Not leaveConnOpen Then
                Me._conn.Close()
            End If
        End Try
    End Function

    Public Function FrontFrameExists(ByVal ffName As String, Optional ByVal leaveConnOpen As Boolean = True) As Boolean
        Dim sqlCmd As New SqlCommand("Execute  spFrontFrameExists @FrameName = '" & ffName & "'", Me._conn)
        If Not Me._conn.State = ConnectionState.Open Then
            Me._conn.Open()
        End If
        Try
            If IsNothing(sqlCmd.ExecuteScalar) Then
                Return False
            Else
                Return True
            End If
        Catch ex As Exception
            MsgBox("FrontFrameExists ExecuteScalar failed. " + ex.Message)
        Finally
            If Not leaveConnOpen Then
                Me._conn.Close()
            End If
        End Try
    End Function

    Public Function SetFrontFrame(ByVal ff As FrontFrame, Optional ByVal leaveConnOpen As Boolean = True) As dbTransactionTypes
        Dim retVal As dbTransactionTypes = dbTransactionTypes.dbFail
        Dim sqlCmd As New SqlCommand
        sqlCmd.CommandType = CommandType.StoredProcedure
        sqlCmd.CommandText = "spSetFrontFrame"
        sqlCmd.Connection = Me._conn
        If Not Me._conn.State = ConnectionState.Open Then
            Me._conn.Open()
        End If
        Try
            sqlCmd.Parameters.Add("@FrameName", SqlDbType.VarChar)
            sqlCmd.Parameters.Item("@FrameName").Direction = ParameterDirection.Input
            sqlCmd.Parameters.Item("@FrameName").Value = ff.Name
            sqlCmd.Parameters.Add("@JobNumber", SqlDbType.VarChar)
            sqlCmd.Parameters.Item("@JobNumber").Direction = ParameterDirection.Input
            sqlCmd.Parameters.Item("@JobNumber").Value = ff.JobNo
            sqlCmd.Parameters.Add("@ItemNumber", SqlDbType.VarChar)
            sqlCmd.Parameters.Item("@ItemNumber").Direction = ParameterDirection.Input
            sqlCmd.Parameters.Item("@ItemNumber").Value = ff.ItemNo
            sqlCmd.Parameters.Add("@FrameSizeW", SqlDbType.Float)
            sqlCmd.Parameters.Item("@FrameSizeW").Direction = ParameterDirection.Input
            sqlCmd.Parameters.Item("@FrameSizeW").Value = ff.FrontframeSize.Width
            sqlCmd.Parameters.Add("@FrameSizeH", SqlDbType.Float)
            sqlCmd.Parameters.Item("@FrameSizeH").Direction = ParameterDirection.Input
            sqlCmd.Parameters.Item("@FrameSizeH").Value = ff.FrontframeSize.Height
            sqlCmd.Parameters.Add("@FrameStyle", SqlDbType.VarChar)
            sqlCmd.Parameters.Item("@FrameStyle").Direction = ParameterDirection.Input
            sqlCmd.Parameters.Item("@FrameStyle").Value = ""
            sqlCmd.Parameters.Add("@FrameThickness", SqlDbType.VarChar)
            sqlCmd.Parameters.Item("@FrameThickness").Direction = ParameterDirection.Input
            sqlCmd.Parameters.Item("@FrameThickness").Value = ""
            sqlCmd.Parameters.Add("@FrameHinge", SqlDbType.VarChar)
            sqlCmd.Parameters.Item("@FrameHinge").Direction = ParameterDirection.Input
            sqlCmd.Parameters.Item("@FrameHinge").Value = ""
            sqlCmd.Parameters.Add("@WoodSpecie", SqlDbType.VarChar)
            sqlCmd.Parameters.Item("@WoodSpecie").Direction = ParameterDirection.Input
            sqlCmd.Parameters.Item("@WoodSpecie").Value = ff.WoodSpecie
            sqlCmd.Parameters.Add("@RetVal", SqlDbType.Int)
            sqlCmd.Parameters.Item("@RetVal").Direction = ParameterDirection.ReturnValue

            sqlCmd.Prepare()
            sqlCmd.ExecuteNonQuery()
            retVal = CType(sqlCmd.Parameters.Item("@RetVal").Value, dbTransactionTypes)
            Return retVal
        Catch ex As Exception
            MsgBox("spSetFrontFrame ExecuteNonQuery failed. " + ex.Message)
        Finally
            If Not leaveConnOpen Then
                Me._conn.Close()
            End If
        End Try
        Return retVal
    End Function

    Public Function GetJobFramesList(ByVal jobNo As String, Optional ByVal leaveConnOpen As Boolean = True) As Generic.List(Of FrontFrame)
        Dim sqlCmd As New SqlCommand("EXECUTE spGetJobFramesList @JobNumber = '" & jobNo & "'", Me._conn)
        If Not Me._conn.State = ConnectionState.Open Then
            Me._conn.Open()
        End If
        Try
            Dim frame As FrontFrame
            Dim retList As New Generic.List(Of FrontFrame)
            Dim ds As New DataSet
            Dim da As New SqlDataAdapter
            da.SelectCommand = sqlCmd
            da.Fill(ds)
            'FrameName 0, JobNumber 1, ItemNumber 2, FrameSizeW 3
            'FrameSizeH 4, FrameStyle 5, FrameThickness 6, FrameHinge 7, WoodSpecie 8

            For i As Integer = 0 To ds.Tables(0).Rows.Count - 1
                frame = New FrontFrame(Me, CStr(ds.Tables(0).Rows(i)(1)), CStr(ds.Tables(0).Rows(i)(2)), _
                     Trim(CStr(ds.Tables(0).Rows(i)(8))))
                frame.FrontframeSize = New SizeF(CSng((ds.Tables(0).Rows(i)(3))), CSng(ds.Tables(0).Rows(i)(4)))
                retList.Add(frame)
            Next
            For i As Integer = 0 To retList.Count - 1
                CType(retList(i), FrontFrame).PartsList = GetFramePartList(CType(retList(i), FrontFrame), True)
                CType(retList(i), FrontFrame).Openinglist = GetFrameOpeningList(CType(retList(i), FrontFrame), True)
            Next
            Return retList
        Catch ex As Exception
            MsgBox("GetJobFramesList SqlDataReader failed. " + ex.Message)
        Finally
            If Not leaveConnOpen Then
                Me._conn.Close()
            End If
        End Try
        Return Nothing
    End Function

    Public Function GetSingleFrame(ByVal fname As String) As FrontFrame
        Dim sqlCmd As New SqlCommand("EXECUTE spGetSingleFrame @FrameName = '" & fname & "'", Me._conn)
        If Not Me._conn.State = ConnectionState.Open Then
            Me._conn.Open()
        End If
        Try
            Dim rdr As SqlDataReader
            rdr = sqlCmd.ExecuteReader
            Dim retClass As FrontFrame = Nothing
            Try
                If rdr.Read Then
                    'FrameName 0, JobNumber 1, ItemNumber 2, FrameSizeW 3
                    'FrameSizeH 4, FrameStyle 5, FrameThickness 6, FrameHinge 7, WoodSpecie 8
                    retClass = New FrontFrame(Me, rdr.GetString(1), rdr.GetString(2), Trim(rdr.GetString(8)))
                    retClass.FrontframeSize = New SizeF(CSng(rdr.GetValue(3)), CSng(rdr.GetValue(4)))
                End If
            Finally
                rdr.Close()
            End Try
            retClass.PartsList = GetFramePartList(retClass, True)
            retClass.Openinglist = GetFrameOpeningList(retClass, True)
            Return retClass
        Catch ex As Exception
            MsgBox("GetSingleFrame SqlDataReader failed. " + ex.Message)
        Finally
            Me._conn.Close()
        End Try
        Return Nothing
    End Function

    Private Function DeleteJobFrontFrame(ByVal jobNo As String) As Integer
        Dim sqlCmd As New SqlCommand
        sqlCmd.CommandType = CommandType.StoredProcedure
        sqlCmd.CommandText = "spDeleteJobFrontFrames"
        sqlCmd.Connection = Me._conn
        If Not Me._conn.State = ConnectionState.Open Then
            Me._conn.Open()
        End If
        Try
            sqlCmd.Parameters.Add("@JobNumber", SqlDbType.VarChar)
            sqlCmd.Parameters.Item("@JobNumber").Direction = ParameterDirection.Input
            sqlCmd.Parameters.Item("@JobNumber").Value = jobNo
            sqlCmd.Parameters.Add("@RetVal", SqlDbType.Int)
            sqlCmd.Parameters.Item("@RetVal").Direction = ParameterDirection.ReturnValue

            sqlCmd.Prepare()
            sqlCmd.ExecuteNonQuery()
            Dim retVal As Integer
            retVal = CInt(sqlCmd.Parameters.Item("@RetVal").Value)
            Return retVal
        Catch ex As Exception
            MsgBox("DeleteJobFrontFrame SqlDataReader failed. " + ex.Message)
        Finally
            Me._conn.Close()
        End Try
    End Function

#End Region

#Region "Frame Part Methods"

    Public Function FramePartExists(ByVal fpName As String) As Boolean
        Dim sqlCmd As New SqlCommand("Execute  spFramePartExists @PartName = '" & fpName & "'", Me._conn)
        If Not Me._conn.State = ConnectionState.Open Then
            Me._conn.Open()
        End If
        Try
            If IsNothing(sqlCmd.ExecuteScalar) Then
                Return False
            Else
                Return True
            End If
        Catch ex As Exception
            MsgBox("FramePartExists ExecuteScalar failed. " + ex.Message)
        Finally
            Me._conn.Close()
        End Try
    End Function

    Public Function SetFramePart(ByVal fp As FramePart, ByVal framename As String, Optional ByVal leaveConnOpen As Boolean = True) As dbTransactionTypes
        Dim retVal As dbTransactionTypes = dbTransactionTypes.dbFail
        Dim sqlCmd As New SqlCommand
        sqlCmd.CommandType = CommandType.StoredProcedure
        sqlCmd.CommandText = "spSetFramePart4"
        sqlCmd.Connection = Me._conn
        If Not Me._conn.State = ConnectionState.Open Then
            Me._conn.Open()
        End If
        Try
            sqlCmd.Parameters.Add("@PartName", SqlDbType.VarChar)
            sqlCmd.Parameters.Item("@PartName").Direction = ParameterDirection.Input
            sqlCmd.Parameters.Item("@PartName").Value = Trim(fp.Name)

            sqlCmd.Parameters.Add("@PartDesc", SqlDbType.VarChar)
            sqlCmd.Parameters.Item("@PartDesc").Direction = ParameterDirection.Input
            sqlCmd.Parameters.Item("@PartDesc").Value = Trim(fp.Description)

            sqlCmd.Parameters.Add("@FrameName", SqlDbType.VarChar)
            sqlCmd.Parameters.Item("@FrameName").Direction = ParameterDirection.Input
            sqlCmd.Parameters.Item("@FrameName").Value = framename

            sqlCmd.Parameters.Add("@PartSizeItem", SqlDbType.VarChar)
            sqlCmd.Parameters.Item("@PartSizeItem").Direction = ParameterDirection.Input
            sqlCmd.Parameters.Item("@PartSizeItem").Value = Trim(fp.ItemNo)

            sqlCmd.Parameters.Add("@PartSizeCode", SqlDbType.VarChar)
            sqlCmd.Parameters.Item("@PartSizeCode").Direction = ParameterDirection.Input
            sqlCmd.Parameters.Item("@PartSizeCode").Value = Trim(fp.Code)

            sqlCmd.Parameters.Add("@PartSizeW", SqlDbType.Decimal)
            sqlCmd.Parameters.Item("@PartSizeW").Direction = ParameterDirection.Input
            sqlCmd.Parameters.Item("@PartSizeW").Value = Convert.ToDecimal(fp.Width)

            sqlCmd.Parameters.Add("@PartSizeLen", SqlDbType.Decimal)
            sqlCmd.Parameters.Item("@PartSizeLen").Direction = ParameterDirection.Input
            sqlCmd.Parameters.Item("@PartSizeLen").Value = Convert.ToDecimal(fp.Length)

            sqlCmd.Parameters.Add("@PartSizeThick", SqlDbType.Decimal)
            sqlCmd.Parameters.Item("@PartSizeThick").Direction = ParameterDirection.Input
            sqlCmd.Parameters.Item("@PartSizeThick").Value = Convert.ToDecimal(fp.Thickness)

            sqlCmd.Parameters.Add("@PartType", SqlDbType.NVarChar)
            sqlCmd.Parameters.Item("@PartType").Direction = ParameterDirection.Input
            sqlCmd.Parameters.Item("@PartType").Value = fp.PartType

            sqlCmd.Parameters.Add("@HingePlace", SqlDbType.Int)
            sqlCmd.Parameters.Item("@HingePlace").Direction = ParameterDirection.Input
            sqlCmd.Parameters.Item("@HingePlace").Value = CInt(fp.Hinging)

            sqlCmd.Parameters.Add("@HingeStyle", SqlDbType.VarChar)
            sqlCmd.Parameters.Item("@HingeStyle").Direction = ParameterDirection.Input
            sqlCmd.Parameters.Item("@HingeStyle").Value = fp.HingeStyle.Name

            sqlCmd.Parameters.Add("@FrameStyle", SqlDbType.VarChar)
            sqlCmd.Parameters.Item("@FrameStyle").Direction = ParameterDirection.Input
            sqlCmd.Parameters.Item("@FrameStyle").Value = fp.FrontFrameStyle.Name

            sqlCmd.Parameters.Add("@ThickStyle", SqlDbType.VarChar)
            sqlCmd.Parameters.Item("@ThickStyle").Direction = ParameterDirection.Input
            sqlCmd.Parameters.Item("@ThickStyle").Value = fp.ThicknessStyle.Name

            sqlCmd.Parameters.Add("@DoMortise", SqlDbType.Bit)
            sqlCmd.Parameters.Item("@DoMortise").Direction = ParameterDirection.Input
            sqlCmd.Parameters.Item("@DoMortise").Value = fp.DoMortise

            sqlCmd.Parameters.Add("@DoHaunch", SqlDbType.Bit)
            sqlCmd.Parameters.Item("@DoHaunch").Direction = ParameterDirection.Input
            sqlCmd.Parameters.Item("@DoHaunch").Value = fp.DoHaunch

            sqlCmd.Parameters.Add("@NoHaunchZero", SqlDbType.Bit)
            sqlCmd.Parameters.Item("@NoHaunchZero").Direction = ParameterDirection.Input
            sqlCmd.Parameters.Item("@NoHaunchZero").Value = fp.noHaunchAtZero

            sqlCmd.Parameters.Add("@NoHaunchFarend", SqlDbType.Bit)
            sqlCmd.Parameters.Item("@NoHaunchFarend").Direction = ParameterDirection.Input
            sqlCmd.Parameters.Item("@NoHaunchFarend").Value = fp.noHaunchAtFarend

            sqlCmd.Parameters.Add("@TenonLengthNear", SqlDbType.Decimal)
            sqlCmd.Parameters.Item("@TenonLengthNear").Direction = ParameterDirection.Input
            sqlCmd.Parameters.Item("@TenonLengthNear").Value = Convert.ToDecimal(fp.tenonLengthAtZero)

            sqlCmd.Parameters.Add("@TenonLengthFar", SqlDbType.Decimal)
            sqlCmd.Parameters.Item("@TenonLengthFar").Direction = ParameterDirection.Input
            sqlCmd.Parameters.Item("@TenonLengthFar").Value = Convert.ToDecimal(fp.tenonLengthAtFar)

            sqlCmd.Parameters.Add("@PartComment", SqlDbType.VarChar)
            sqlCmd.Parameters.Item("@PartComment").Direction = ParameterDirection.Input
            sqlCmd.Parameters.Item("@PartComment").Value = fp.TSComments

            sqlCmd.Parameters.Add("@CutList", SqlDbType.Bit)
            sqlCmd.Parameters.Item("@CutList").Direction = ParameterDirection.Input
            sqlCmd.Parameters.Item("@CutList").Value = fp.IncludeInCutlist

            sqlCmd.Parameters.Add("@WoodGrade", SqlDbType.Char)
            sqlCmd.Parameters.Item("@WoodGrade").Direction = ParameterDirection.Input
            sqlCmd.Parameters.Item("@WoodGrade").Value = fp.Grade

            sqlCmd.Parameters.Add("@RetVal", SqlDbType.Int)
            sqlCmd.Parameters.Item("@RetVal").Direction = ParameterDirection.ReturnValue

            sqlCmd.Prepare()
            sqlCmd.ExecuteNonQuery()
            retVal = CType(sqlCmd.Parameters.Item("@RetVal").Value, dbTransactionTypes)
            Return retVal
        Catch ex As Exception
            MsgBox("InsertFramePart ExecuteNonQuery failed. " + ex.Message)
        Finally
            If Not leaveConnOpen Then
                Me._conn.Close()
            End If
        End Try
        Return retVal
    End Function

    Public Function GetFramePartPartsize(ByVal ptName As String) As FramePartBaseClass
        Dim sqlSelect As String = "Execute  spGetFramePart4 @ptName = '" & ptName & "'"
        Dim sqlCmd As New SqlCommand(sqlSelect, Me._conn)
        If Not Me._conn.State = ConnectionState.Open Then
            Me._conn.Open()
        End If
        Try
            Dim rdr As SqlDataReader
            rdr = sqlCmd.ExecuteReader
            Dim ptSze As FramePartBaseClass = Nothing
            Try
                If rdr.Read Then
                    'PartName 0, PartDesc 1, PartSizeItem 2, PartSizeCode 3, PartSizeW 4, PartSizeLen 5, 
                    'PartSizeThick 6, PartType 7, HingePlace 8, HingeStyle 9, FrameStyle 10, ThickStyle 11
                    'DoMortise(12), DoHaunch(13), NoTenonZero 14, NoTenonFarend 15, NoHaunchZero 16,
                    'NoHaunchFarend 17, PartComment (18), CutList(19) WoodGrade(20)

                    ptSze = New FramePartBaseClass(Trim(rdr.GetString(0)))
                    ptSze.Width = CSng(rdr.GetValue(4))
                    ptSze.Length = CSng(rdr.GetValue(5))
                    ptSze.Thickness = CSng(rdr.GetValue(6))
                    If Not IsDBNull(rdr.GetValue(7)) Then
                        ptSze.PartType = CType([Enum].Parse(GetType(FrontFrameEventClasses.PartEdgeTypes), rdr.GetString(7)), FrontFrameEventClasses.PartEdgeTypes)
                    End If
                End If
            Finally
                rdr.Close()
            End Try
            Return ptSze
        Catch ex As Exception
            MsgBox("GetFramePart SqlDataReader failed. " + ex.Message)
        Finally
            Me._conn.Close()
        End Try
        Return Nothing
    End Function

    'GetFramePart
    Public Function GetFramePart(ByVal ptName As String) As FramePart
        Dim sqlSelect As String = "Execute  [spGetFramePart4] @ptName = '" & ptName & "'"
        Dim sqlCmd As New SqlCommand(sqlSelect, Me._conn)
        If Not Me._conn.State = ConnectionState.Open Then
            Me._conn.Open()
        End If
        Dim rdr As SqlDataReader
        Try
            rdr = sqlCmd.ExecuteReader
            Dim pt As FramePart = Nothing
            Dim values(21) As Object
            Try
                If rdr.Read Then
                    'PartName 0, PartDesc 1, PartSizeItem 2, PartSizeCode 3, PartSizeW 4, PartSizeLen 5, PartSizeThick 6, 
                    'PartType 7, HingePlace 8, HingeStyle 9, FrameStyle 10, ThickStyle 11, DoMortise 12, DoHaunch 13, NoHaunchZero 14, 
                    'NoHaunchFarend 15, TenonLengthNear 16, TenonLengthFar 17, PartComment 18, CutList 19, WoodGrade 20

                    rdr.GetValues(CType(values, Object()))
                Else
                    MsgBox("Part " & ptName & " was not found in the database")
                End If
            Catch ex1 As Exception
                MsgBox("GetFramePart [spGetFramePart4] rdr.read " & ex1.Message)
            Finally
                rdr.Close()
            End Try
            Dim frame As FrontFrame = Me.GetSingleFrame(Regex.Matches(CStr(values(0)).Trim, "^[A-Za-z0-9]{9}.\d{2,3}[A-Z]?")(0).Value.ToString)
            Dim retPart As FramePart = frame.GetSinglePart(CStr(values(0)).Trim)
            Return retPart
        Catch ex As Exception
            MsgBox("GetFramePart, spGetFramePart4, SqlDataReader failed. " + ex.Message)
            Return Nothing
        Finally
            Me._conn.Close()
        End Try
    End Function

    Private Function GetFramePartList(ByRef ownerFrame As FrontFrame, Optional ByVal leaveConnOpen As Boolean = True) As Generic.List(Of FramePart)
        Dim opParms As OperationParms = Me.GetOperationParmsClass(ownerFrame.WoodSpecie)
        Dim sqlSelect As String = "Execute  [spGetFramePartList4] @frName = '" & ownerFrame.Name.Trim & "'"
        Dim sqlCmd As New SqlCommand(sqlSelect, Me._conn)
        If Not Me._conn.State = ConnectionState.Open Then
            Me._conn.Open()
        End If
        Dim rdr As SqlDataReader
        Try
            rdr = sqlCmd.ExecuteReader
            Dim pt As FramePart = Nothing
            Dim retList As New Generic.List(Of FramePart)
            Try
                While rdr.Read()
                    'PartName 0, PartDesc 1, PartSizeItem 2, PartSizeCode 3, PartSizeW 4, PartSizeLen 5, PartSizeThick 6, 
                    'PartType 7, HingePlace 8, HingeStyle 9, FrameStyle 10, ThickStyle 11, DoMortise 12, DoHaunch 13, NoHaunchZero 14, 
                    'NoHaunchFarend 15, TenonLengthNear 16, TenonLengthFar 17, PartComment 18, CutList 19, WoodGrade 20

                    Dim ptSze As New FramePartBaseClass(Trim(rdr.GetString(0)))
                    ptSze.Width = CSng(rdr.GetValue(4))
                    ptSze.Length = CSng(rdr.GetValue(5))
                    ptSze.Thickness = CSng(rdr.GetValue(6))
                    If Not IsDBNull(rdr.GetValue(7)) Then
                        ptSze.PartType = CType([Enum].Parse(GetType(FrontFrameEventClasses.PartEdgeTypes), rdr.GetString(7)), FrontFrameEventClasses.PartEdgeTypes)
                    End If
                    Dim hngeLoc As HingePlacement = CType([Enum].ToObject(GetType(HingePlacement), rdr.GetValue(8)), HingePlacement)
                    pt = New FramePart(ownerFrame, ptSze, hngeLoc, opParms)
                    pt.noHaunchAtZero = rdr.GetBoolean(14)
                    pt.noHaunchAtFarend = rdr.GetBoolean(15)
                    pt.tenonLengthAtZero = CSng(rdr.GetValue(16))
                    pt.tenonLengthAtFar = CSng(rdr.GetValue(17))
                    pt.TSComments = rdr.GetString(18)
                    pt.IncludeInCutlist = rdr.GetBoolean(19)
                    pt.Grade = CChar(rdr.GetValue(20))
                    retList.Add(pt)
                End While
            Catch ex1 As Exception
                MsgBox("spGetFramePartList4 rdr.read " & ex1.Message)
            Finally
                rdr.Close()
            End Try
            Dim retPt As FramePart
            For Each retPt In retList
                Dim edgeList As New ArrayList
                retPt.PartEdges = GetPartEdgeList(retPt, retPt.Name, True)
                For i As Integer = 0 To retPt.PartEdges.GetUpperBound(0)
                    If Not IsNothing(retPt.PartEdges(i)) Then
                        retPt.PartEdges(i).Operationlist = GetEdgeOperationList(retPt.PartEdges(i).Name, True)
                        retPt.PartEdges(i).AdjoiningPartNameList = GetAdjoiningPartList(retPt.PartEdges(i).Name, True)
                    End If
                Next
            Next
            Return retList
        Catch ex As Exception
            MsgBox("GetFramePartList SqlDataReader failed. " + ex.Message)
            Return Nothing
        Finally
            If Not leaveConnOpen Then
                Me._conn.Close()
            End If
        End Try
    End Function

    Private Function GetFrameOpeningList(ByRef ownerFrame As FrontFrame, ByVal leaveConnOpen As Boolean) As Generic.List(Of FramePart)
        Dim opParms As OperationParms = Me.GetOperationParmsClass(ownerFrame.WoodSpecie)
        Dim sqlSelect As String = "Execute  spGetFrameOpeningList5 @frName = '" & ownerFrame.Name & "'"
        Dim sqlCmd As New SqlCommand(sqlSelect, Me._conn)
        If Not Me._conn.State = ConnectionState.Open Then
            Me._conn.Open()
        End If
        Dim rdr As SqlDataReader = sqlCmd.ExecuteReader
        Try
            Dim pt As FramePart = Nothing
            Dim values(21) As Object
            Dim retList As New Generic.List(Of FramePart)
            While rdr.Read
                'PartName 0, PartDesc 1, PartSizeItem 2, PartSizeCode 3, PartSizeW 4, PartSizeLen 5, PartSizeThick 6, 
                'PartType 7, HingePlace 8, PartComment 9

                Dim ptSze As New FramePartBaseClass(rdr.GetString(0).Trim)
                ptSze.Width = CSng(rdr.GetValue(4))
                ptSze.Length = CSng(rdr.GetValue(5))
                ptSze.Thickness = 0.0
                If Not IsDBNull(rdr.GetValue(7)) Then
                    ptSze.PartType = CType([Enum].Parse(GetType(FrontFrameEventClasses.PartEdgeTypes), rdr.GetString(7)), FrontFrameEventClasses.PartEdgeTypes)
                End If
                pt = New FramePart(ownerFrame, ptSze, CType([Enum].Parse(GetType(HingePlacement), CStr(rdr.GetValue(8))), HingePlacement), opParms)
                pt.TSComments = rdr.GetString(9).Trim
                retList.Add(pt)
            End While
            Return retList
        Catch ex As Exception
            MsgBox("spGetFrameOpeningList5 SqlDataReader failed. " + ex.Message)
        Finally
            rdr.Close()
            If Not leaveConnOpen Then
                Me._conn.Close()
            End If
        End Try
        Return Nothing
    End Function

    Private Function DeleteJobFrameParts(ByVal jobNo As String) As Integer
        Dim sqlCmd As New SqlCommand
        sqlCmd.CommandType = CommandType.StoredProcedure
        sqlCmd.CommandText = "spDeleteJobFrameParts"
        sqlCmd.Connection = Me._conn
        If Not Me._conn.State = ConnectionState.Open Then
            Me._conn.Open()
        End If
        Try
            sqlCmd.Parameters.Add("@JobNumber", SqlDbType.VarChar)
            sqlCmd.Parameters.Item("@JobNumber").Direction = ParameterDirection.Input
            sqlCmd.Parameters.Item("@JobNumber").Value = jobNo
            sqlCmd.Parameters.Add("@RetVal", SqlDbType.Int)
            sqlCmd.Parameters.Item("@RetVal").Direction = ParameterDirection.ReturnValue

            sqlCmd.Prepare()
            sqlCmd.ExecuteNonQuery()
            Dim retVal As Integer
            retVal = CInt(sqlCmd.Parameters.Item("@RetVal").Value)
            Return retVal
        Catch ex As Exception
            MsgBox("DeleteJobFrameParts SqlDataReader failed. " + ex.Message)
        Finally
            Me._conn.Close()
        End Try
    End Function

#End Region

#Region "Edge Operation Methods"

    Public Function PartEdgeExists(ByVal edName As String) As Boolean
        Dim sqlCmd As New SqlCommand("EXECUTE spPartEdgeExists " _
            & " @EdgeName = '" & edName & "'", Me._conn)
        If Not Me._conn.State = ConnectionState.Open Then
            Me._conn.Open()
        End If
        Try
            If IsNothing(sqlCmd.ExecuteScalar) Then
                Return False
            Else
                Return True
            End If
        Catch ex As Exception
            MsgBox("PartEdgeExists ExecuteScalar failed. " + ex.Message)
        Finally
            Me._conn.Close()
        End Try
    End Function

    Public Function SetPartEdge(ByVal edge As PartEdge, ByVal leaveConnOpen As Boolean) As dbTransactionTypes
        Dim retVal As dbTransactionTypes = dbTransactionTypes.dbFail
        Dim sqlCmd As New SqlCommand
        sqlCmd.CommandType = CommandType.StoredProcedure
        sqlCmd.CommandText = "spSetPartEdge"
        sqlCmd.Connection = Me._conn
        If Not Me._conn.State = ConnectionState.Open Then
            Me._conn.Open()
        End If
        Try
            sqlCmd.Parameters.Add("@EdgeName", SqlDbType.VarChar)
            sqlCmd.Parameters.Item("@EdgeName").Direction = ParameterDirection.Input
            sqlCmd.Parameters.Item("@EdgeName").Value = Trim(edge.Name)
            sqlCmd.Parameters.Add("@PartEdgeType", SqlDbType.VarChar)
            sqlCmd.Parameters.Item("@PartEdgeType").Direction = ParameterDirection.Input
            sqlCmd.Parameters.Item("@PartEdgeType").Value = edge.PeType
            sqlCmd.Parameters.Add("@ProgramName", SqlDbType.VarChar)
            sqlCmd.Parameters.Item("@ProgramName").Direction = ParameterDirection.Input
            sqlCmd.Parameters.Item("@ProgramName").Value = edge.ProgramFilename
            sqlCmd.Parameters.Add("@ProgComment", SqlDbType.VarChar)
            sqlCmd.Parameters.Item("@ProgComment").Direction = ParameterDirection.Input
            sqlCmd.Parameters.Item("@ProgComment").Value = edge.Comment
            sqlCmd.Parameters.Add("@RetVal", SqlDbType.Int)
            sqlCmd.Parameters.Item("@RetVal").Direction = ParameterDirection.ReturnValue

            sqlCmd.Prepare()
            sqlCmd.ExecuteNonQuery()
            retVal = CType(sqlCmd.Parameters.Item("@RetVal").Value, dbTransactionTypes)
            Return retVal
        Catch ex As Exception
            MsgBox("SetPartEdge ExecuteNonQuery failed. " + ex.Message)
        Finally
            If Not leaveConnOpen Then
                Me._conn.Close()
            End If
        End Try
        Return retVal
    End Function

    Public Function GetPartEdgeList(ByRef ownerPart As FramePart, ByVal ptname As String, ByVal leaveConnOpen As Boolean) As PartEdge()
        Dim sqlCmd As New SqlCommand("EXECUTE spGetLikePartEdge " _
            & "@PartName = '" & ptname & "'", Me._conn)
        Dim rdr As SqlDataReader = Nothing
        If Not Me._conn.State = ConnectionState.Open Then
            Me._conn.Open()
        End If
        Try
            rdr = sqlCmd.ExecuteReader
            Dim retList(0) As PartEdge
            While rdr.Read
                'EdgeName 0, PartEdgeType 1, ProgramName 2, ProgComment 3
                retList(retList.GetUpperBound(0)) = New PartEdge(ownerPart, Trim(rdr.GetString(0)), _
                    CType(System.Enum.Parse(GetType(FrontFrameEventClasses.PartEdgeTypes), Trim(rdr.GetString(1))), FrontFrameEventClasses.PartEdgeTypes), _
                    Trim(rdr.GetString(2)), Trim(rdr.GetString(3)))
                ReDim Preserve retList(retList.GetUpperBound(0) + 1)
            End While
            If IsNothing(retList(retList.GetUpperBound(0))) Then
                ReDim Preserve retList(retList.GetUpperBound(0) - 1)
            End If
            Return retList
        Catch ex As Exception
            MsgBox("GetPartEdgeList SqlDataReader failed. " + ex.Message)
        Finally
            rdr.Close()
            If Not leaveConnOpen Then
                Me._conn.Close()
            End If
        End Try
        Return Nothing
    End Function

    Public Function SetPartEdgeProgname(ByVal edgeName As String, ByVal prgName As String) As dbTransactionTypes
        Dim retVal As dbTransactionTypes = dbTransactionTypes.dbFail
        Dim sqlCmd As New SqlCommand
        sqlCmd.CommandType = CommandType.StoredProcedure
        sqlCmd.CommandText = "spSetPartEdgeProgname"
        sqlCmd.Connection = Me._conn
        If Not Me._conn.State = ConnectionState.Open Then
            Me._conn.Open()
        End If
        Try
            sqlCmd.Parameters.Add("@EdgeName", SqlDbType.VarChar)
            sqlCmd.Parameters.Item("@EdgeName").Direction = ParameterDirection.Input
            sqlCmd.Parameters.Item("@EdgeName").Value = Trim(edgeName)
            sqlCmd.Parameters.Add("@ProgramName", SqlDbType.VarChar)
            sqlCmd.Parameters.Item("@ProgramName").Direction = ParameterDirection.Input
            sqlCmd.Parameters.Item("@ProgramName").Value = prgName
            sqlCmd.Parameters.Add("@RetVal", SqlDbType.Int)
            sqlCmd.Parameters.Item("@RetVal").Direction = ParameterDirection.ReturnValue

            sqlCmd.Prepare()
            sqlCmd.ExecuteNonQuery()
            retVal = CType(sqlCmd.Parameters.Item("@RetVal").Value, dbTransactionTypes)
            Return retVal
        Catch ex As Exception
            MsgBox("SetPartEdgeProgname ExecuteNonQuery failed. " + ex.Message)
        Finally
            Me._conn.Close()
        End Try
        Return retVal
    End Function

    Public Function GetPartEdgeProgname(ByVal edName As String) As String
        Dim sqlCmd As New SqlCommand("EXECUTE spGetPartEdgeProgname " _
            & "@EdgeName = '" & edName & "'", Me._conn)
        Dim rdr As SqlDataReader = Nothing
        If Not Me._conn.State = ConnectionState.Open Then
            Me._conn.Open()
        End If
        Try
            rdr = sqlCmd.ExecuteReader
            Dim retVal As String = String.Empty
            If rdr.Read Then
                'ProgramName 0
                retVal = Trim(rdr.GetString(0))
            End If
            Return retVal
        Catch ex As Exception
            MsgBox("GetPartEdgeProgname SqlDataReader failed. " + ex.Message)
        Finally
            rdr.Close()
            Me._conn.Close()
        End Try
        Return Nothing
    End Function

    Private Function DeleteJobPartEdges(ByVal jobNo As String) As Integer
        Dim sqlCmd As New SqlCommand
        sqlCmd.CommandType = CommandType.StoredProcedure
        sqlCmd.CommandText = "spDeleteJobPartEdges"
        sqlCmd.Connection = Me._conn
        If Not Me._conn.State = ConnectionState.Open Then
            Me._conn.Open()
        End If
        Try
            sqlCmd.Parameters.Add("@JobNumber", SqlDbType.VarChar)
            sqlCmd.Parameters.Item("@JobNumber").Direction = ParameterDirection.Input
            sqlCmd.Parameters.Item("@JobNumber").Value = jobNo
            sqlCmd.Parameters.Add("@RetVal", SqlDbType.Int)
            sqlCmd.Parameters.Item("@RetVal").Direction = ParameterDirection.ReturnValue

            sqlCmd.Prepare()
            sqlCmd.ExecuteNonQuery()
            Dim retVal As Integer
            retVal = CInt(sqlCmd.Parameters.Item("@RetVal").Value)
            Return retVal
        Catch ex As Exception
            MsgBox("DeleteJobAdjoiningParts SqlDataReader failed. " + ex.Message)
        Finally
            Me._conn.Close()
        End Try
    End Function

    Public Function EdgeOperationExists(ByVal edName As String, ByVal opName As String) As Boolean
        Dim sqlCmd As New SqlCommand("EXECUTE spEdgeOperationExists " _
            & "@OperationName = '" & opName & "', @EdgeName = '" & edName & "'", Me._conn)
        If Not Me._conn.State = ConnectionState.Open Then
            Me._conn.Open()
        End If
        Try
            If IsNothing(sqlCmd.ExecuteScalar) Then
                Return False
            Else
                Return True
            End If
        Catch ex As Exception
            MsgBox("EdgeOperationExists ExecuteScalar failed. " + ex.Message)
        Finally
            Me._conn.Close()
        End Try
    End Function

    Public Function SetEdgeOperation(ByVal edge As PartEdge, ByVal op As AccuOperation, ByVal leaveConnOpen As Boolean) As dbTransactionTypes
        Dim retVal As dbTransactionTypes = dbTransactionTypes.dbFail
        Dim sqlCmd As New SqlCommand
        sqlCmd.CommandType = CommandType.StoredProcedure
        sqlCmd.CommandText = "spSetEdgeOperation"
        sqlCmd.Connection = Me._conn
        If Not Me._conn.State = ConnectionState.Open Then
            Me._conn.Open()
        End If
        DeleteEdgeOperations(edge.Name, True)
        Try
            sqlCmd.Parameters.Add("@OperationName", SqlDbType.VarChar)
            sqlCmd.Parameters.Item("@OperationName").Direction = ParameterDirection.Input
            sqlCmd.Parameters.Item("@OperationName").Value = Trim(op.Name)
            sqlCmd.Parameters.Add("@OperationNo", SqlDbType.VarChar)
            sqlCmd.Parameters.Item("@OperationNo").Direction = ParameterDirection.Input
            sqlCmd.Parameters.Item("@OperationNo").Value = Trim(op.Number)
            sqlCmd.Parameters.Add("@opPosition", SqlDbType.Float)
            sqlCmd.Parameters.Item("@opPosition").Direction = ParameterDirection.Input
            sqlCmd.Parameters.Item("@opPosition").Value = op.Position
            sqlCmd.Parameters.Add("@doHaunch", SqlDbType.Bit)
            sqlCmd.Parameters.Item("@doHaunch").Direction = ParameterDirection.Input
            sqlCmd.Parameters.Item("@doHaunch").Value = op.Haunch.GetHashCode
            sqlCmd.Parameters.Add("@EdgeName", SqlDbType.VarChar)
            sqlCmd.Parameters.Item("@EdgeName").Direction = ParameterDirection.Input
            sqlCmd.Parameters.Item("@EdgeName").Value = Trim(edge.Name)
            sqlCmd.Parameters.Add("@HaunchDepth", SqlDbType.Float)
            sqlCmd.Parameters.Item("@HaunchDepth").Direction = ParameterDirection.Input
            sqlCmd.Parameters.Item("@HaunchDepth").Value = op.HaunchDepth
            sqlCmd.Parameters.Add("@doMortise", SqlDbType.Bit)
            sqlCmd.Parameters.Item("@doMortise").Direction = ParameterDirection.Input
            sqlCmd.Parameters.Item("@doMortise").Value = op.Mortise.GetHashCode
            sqlCmd.Parameters.Add("@HaunchLen", SqlDbType.Float)
            sqlCmd.Parameters.Item("@HaunchLen").Direction = ParameterDirection.Input
            sqlCmd.Parameters.Item("@HaunchLen").Value = op.HaunchLength
            sqlCmd.Parameters.Add("@MortiseDepth", SqlDbType.Float)
            sqlCmd.Parameters.Item("@MortiseDepth").Direction = ParameterDirection.Input
            sqlCmd.Parameters.Item("@MortiseDepth").Value = op.MortiseDepth
            sqlCmd.Parameters.Add("@MortiseLen", SqlDbType.Float)
            sqlCmd.Parameters.Item("@MortiseLen").Direction = ParameterDirection.Input
            sqlCmd.Parameters.Item("@MortiseLen").Value = op.MortiseLength
            sqlCmd.Parameters.Add("@MortiseOffset", SqlDbType.Float)
            sqlCmd.Parameters.Item("@MortiseOffset").Direction = ParameterDirection.Input
            sqlCmd.Parameters.Item("@MortiseOffset").Value = op.MortiseOffset
            sqlCmd.Parameters.Add("@doPilot", SqlDbType.Bit)
            sqlCmd.Parameters.Item("@doPilot").Direction = ParameterDirection.Input
            sqlCmd.Parameters.Item("@doPilot").Value = op.Pilot.GetHashCode
            sqlCmd.Parameters.Add("@PilotDepth", SqlDbType.Float)
            sqlCmd.Parameters.Item("@PilotDepth").Direction = ParameterDirection.Input
            sqlCmd.Parameters.Item("@PilotDepth").Value = op.PilotDepth
            sqlCmd.Parameters.Add("@PilotLen", SqlDbType.Float)
            sqlCmd.Parameters.Item("@PilotLen").Direction = ParameterDirection.Input
            sqlCmd.Parameters.Item("@PilotLen").Value = op.PilotDiameter
            sqlCmd.Parameters.Add("@BetwnPilots", SqlDbType.Float)
            sqlCmd.Parameters.Item("@BetwnPilots").Direction = ParameterDirection.Input
            sqlCmd.Parameters.Item("@BetwnPilots").Value = op.Pilot2Pilot
            sqlCmd.Parameters.Add("@RetVal", SqlDbType.Int)
            sqlCmd.Parameters.Item("@RetVal").Direction = ParameterDirection.ReturnValue

            sqlCmd.Prepare()
            sqlCmd.ExecuteNonQuery()
            retVal = CType(sqlCmd.Parameters.Item("@RetVal").Value, dbTransactionTypes)
            Return retVal
        Catch ex As Exception
            MsgBox("SetEdgeOperation ExecuteNonQuery failed. " + ex.Message)
        Finally
            If Not leaveConnOpen Then
                Me._conn.Close()
            End If
        End Try
        Return retVal
    End Function

    Private Function GetEdgeOperation(ByVal opName As String) As AccuOperation
        Dim sqlCmd As New SqlCommand("EXECUTE spGetEdgeOperation " _
            & "@OperationName = '" & opName & "'", Me._conn)
        Dim rdr As SqlDataReader = Nothing
        If Not Me._conn.State = ConnectionState.Open Then
            Me._conn.Open()
        End If
        Try
            rdr = sqlCmd.ExecuteReader
            Dim retClass As AccuOperation = Nothing
            If rdr.Read Then
                'OperationName 0, OperationNo 1, opPosition 2, EdgeName 3, doHaunch 4, HaunchDepth 5,
                'doMortise 6, HaunchLen 7, MortiseDepth 8, MortiseLen 9, MortiseOffset 10, doPilot 11,
                'PilotDepth 12, PilotLen 13, BetwnPilots 14
                retClass = New AccuOperation(Trim(rdr.GetString(0)), CInt(rdr.GetValue(1)), _
                    CSng(rdr.GetValue(2)), rdr.GetBoolean(4), CSng(rdr.GetValue(5)), CSng(rdr.GetValue(7)), _
                    rdr.GetBoolean(6), CSng(rdr.GetValue(8)), CSng(rdr.GetValue(9)), CSng(rdr.GetValue(10)), _
                    rdr.GetBoolean(11), CSng(rdr.GetValue(12)), CSng(rdr.GetValue(13)), CSng(rdr.GetValue(14)))
            End If
            Return retClass
        Catch ex As Exception
            MsgBox("GetEdgeOperation SqlDataReader failed. " + ex.Message)
        Finally
            rdr.Close()
            Me._conn.Close()
        End Try
        Return Nothing
    End Function

    Private Function GetEdgeOperationList(ByVal edName As String, ByVal leaveConnOpen As Boolean) As ArrayList 'of Operation
        Dim sqlCmd As New SqlCommand("EXECUTE spGetEdgeOperationList " _
            & "@EdgeName = '" & edName & "'", Me._conn)
        If Not Me._conn.State = ConnectionState.Open Then
            Me._conn.Open()
        End If
        Try
            Dim rdr As SqlDataReader
            rdr = sqlCmd.ExecuteReader
            Dim retList As New ArrayList
            Dim op As AccuOperation
            Try
                While rdr.Read
                    'OperationName 0, OperationNo 1, opPosition 2, EdgeName 3, doHaunch 4, HaunchDepth 5,
                    'doMortise 6, HaunchLen 7, MortiseDepth 8, MortiseLen 9, MortiseOffset 10, doPilot 11,
                    'PilotDepth 12, PilotLen 13, BetwnPilots 14
                    op = New AccuOperation(Trim(rdr.GetString(0)), CInt(rdr.GetValue(1)), _
                        CSng(rdr.GetValue(2)), rdr.GetBoolean(4), CSng(rdr.GetValue(5)), CSng(rdr.GetValue(7)), _
                        rdr.GetBoolean(6), CSng(rdr.GetValue(8)), CSng(rdr.GetValue(9)), CSng(rdr.GetValue(10)), _
                        rdr.GetBoolean(11), CSng(rdr.GetValue(12)), CSng(rdr.GetValue(13)), CSng(rdr.GetValue(14)))
                    retList.Add(op)
                End While
            Finally
                rdr.Close()
            End Try
            Return retList
        Catch ex As Exception
            MsgBox("GetEdgeOperationList SqlDataReader failed. " + ex.Message)
        Finally
            If Not leaveConnOpen Then
                Me._conn.Close()
            End If
        End Try
        Return Nothing
    End Function

    Private Function DeleteEdgeOperations(ByVal edgeName As String, ByVal leaveConnOpen As Boolean) As Integer
        Dim sqlCmd As New SqlCommand
        sqlCmd.CommandType = CommandType.StoredProcedure
        sqlCmd.CommandText = "spDeleteEdgeOperations"
        sqlCmd.Connection = Me._conn
        If Not Me._conn.State = ConnectionState.Open Then
            Me._conn.Open()
        End If
        Try
            sqlCmd.Parameters.Add("@EdgeName", SqlDbType.VarChar)
            sqlCmd.Parameters.Item("@EdgeName").Direction = ParameterDirection.Input
            sqlCmd.Parameters.Item("@EdgeName").Value = edgeName
            sqlCmd.Parameters.Add("@RetVal", SqlDbType.Int)
            sqlCmd.Parameters.Item("@RetVal").Direction = ParameterDirection.ReturnValue

            sqlCmd.Prepare()
            sqlCmd.ExecuteNonQuery()
            Dim retVal As Integer
            retVal = CInt(sqlCmd.Parameters.Item("@RetVal").Value)
            Return retVal
        Catch ex As Exception
            MsgBox("DeleteEdgeOperations SqlDataReader failed. " + ex.Message)
        Finally
            If Not leaveConnOpen Then
                Me._conn.Close()
            End If
        End Try
    End Function

    Private Function DeleteJobEdgeOperations(ByVal jobNo As String) As Integer
        Dim sqlCmd As New SqlCommand
        sqlCmd.CommandType = CommandType.StoredProcedure
        sqlCmd.CommandText = "spDeleteJobEdgeOperations"
        sqlCmd.Connection = Me._conn
        If Not Me._conn.State = ConnectionState.Open Then
            Me._conn.Open()
        End If
        Try
            sqlCmd.Parameters.Add("@JobNumber", SqlDbType.VarChar)
            sqlCmd.Parameters.Item("@JobNumber").Direction = ParameterDirection.Input
            sqlCmd.Parameters.Item("@JobNumber").Value = jobNo
            sqlCmd.Parameters.Add("@RetVal", SqlDbType.Int)
            sqlCmd.Parameters.Item("@RetVal").Direction = ParameterDirection.ReturnValue

            sqlCmd.Prepare()
            sqlCmd.ExecuteNonQuery()
            Dim retVal As Integer
            retVal = CInt(sqlCmd.Parameters.Item("@RetVal").Value)
            Return retVal
        Catch ex As Exception
            MsgBox("DeleteJobEdgeOperations SqlDataReader failed. " + ex.Message)
        Finally
            Me._conn.Close()
        End Try
    End Function

#End Region

#Region "AdjoiningParts Methods"

    Public Function AdjoiningPartExists(ByVal edName As String, ByVal ptName As String) As Boolean
        Dim sqlCmd As New SqlCommand("EXECUTE spAdjoiningPartExists " _
             & "@EdgeName = '" & edName & "', @PartName = '" & ptName & "'", Me._conn)
        If Not Me._conn.State = ConnectionState.Open Then
            Me._conn.Open()
        End If
        Try
            If IsNothing(sqlCmd.ExecuteScalar) Then
                Return False
            Else
                Return True
            End If
        Catch ex As Exception
            MsgBox("AdjoiningPartExists ExecuteScalar failed. " + ex.Message)
        Finally
            Me._conn.Close()
        End Try
    End Function

    Public Function SetAdjoiningPart(ByVal edName As String, ByVal ptName As String, ByVal seq As Integer, ByVal leaveConnOpen As Boolean) As dbTransactionTypes
        Dim sqlCmd As New SqlCommand
        sqlCmd.CommandType = CommandType.StoredProcedure
        sqlCmd.CommandText = "spSetAdjoiningPart"
        sqlCmd.Connection = Me._conn
        If Not Me._conn.State = ConnectionState.Open Then
            Me._conn.Open()
        End If
        Try
            Dim retVal As dbTransactionTypes = dbTransactionTypes.dbFail
            sqlCmd.Parameters.Add("@PartName", SqlDbType.VarChar)
            sqlCmd.Parameters.Item("@PartName").Direction = ParameterDirection.Input
            sqlCmd.Parameters.Item("@PartName").Value = Trim(ptName)

            sqlCmd.Parameters.Add("@EdgeName", SqlDbType.VarChar)
            sqlCmd.Parameters.Item("@EdgeName").Direction = ParameterDirection.Input
            sqlCmd.Parameters.Item("@EdgeName").Value = Trim(edName)

            sqlCmd.Parameters.Add("@PartSequence", SqlDbType.SmallInt)
            sqlCmd.Parameters.Item("@PartSequence").Direction = ParameterDirection.Input
            sqlCmd.Parameters.Item("@PartSequence").Value = seq

            sqlCmd.Parameters.Add("@RetVal", SqlDbType.Int)
            sqlCmd.Parameters.Item("@RetVal").Direction = ParameterDirection.ReturnValue
            sqlCmd.Prepare()
            sqlCmd.ExecuteNonQuery()
            Dim status As Integer = CInt(sqlCmd.Parameters.Item("@RetVal").Value)
            retVal = CType(status, dbTransactionTypes)
            Return retVal
        Catch ex As Exception
            MsgBox("InsertAdjoiningPart ExecuteNonQuery failed. " + ex.Message)
        Finally
            If Not leaveConnOpen Then
                Me._conn.Close()
            End If
        End Try
    End Function

    Private Function GetAdjoiningPartList(ByVal edName As String, ByVal leaveConnOpen As Boolean) As ArrayList 'of FramePart Names
        Dim sqlCmd As New SqlCommand("EXECUTE spGetAdjoiningPartList " _
            & "@EdgeName = '" & edName & "'", Me._conn)
        If Not Me._conn.State = ConnectionState.Open Then
            Me._conn.Open()
        End If
        Dim rdr As SqlDataReader = sqlCmd.ExecuteReader
        Try
            Dim retList As New ArrayList
            Dim nameList As New ArrayList
            Try
                While rdr.Read
                    nameList.Add(Trim(rdr.GetString(1)))
                End While
            Catch ex As Exception
                MsgBox("GetAdjoiningPartlist rdr.Read failed. " + ex.Message)
            Finally
                rdr.Close()
            End Try

            'Dim str As String
            'For Each str In nameList
            '    retList.Add(GetFramePart(str))
            'Next
            'Return retList

            Return nameList

        Catch ex As Exception
            MsgBox("GetAdjoiningPartlist SqlDataReader failed. " + ex.Message)
        Finally
            If Not leaveConnOpen Then
                Me._conn.Close()
            End If
        End Try
        Return Nothing
    End Function

    Private Function DeleteJobAdjoiningParts(ByVal jobNo As String) As Integer
        Dim sqlCmd As New SqlCommand
        sqlCmd.CommandType = CommandType.StoredProcedure
        sqlCmd.CommandText = "spDeleteJobAdjoiningParts"
        sqlCmd.Connection = Me._conn
        If Not Me._conn.State = ConnectionState.Open Then
            Me._conn.Open()
        End If
        Try
            sqlCmd.Parameters.Add("@JobNumber", SqlDbType.VarChar)
            sqlCmd.Parameters.Item("@JobNumber").Direction = ParameterDirection.Input
            sqlCmd.Parameters.Item("@JobNumber").Value = jobNo
            sqlCmd.Parameters.Add("@RetVal", SqlDbType.Int)
            sqlCmd.Parameters.Item("@RetVal").Direction = ParameterDirection.ReturnValue

            sqlCmd.Prepare()
            sqlCmd.ExecuteNonQuery()
            Dim retVal As Integer
            retVal = CInt(sqlCmd.Parameters.Item("@RetVal").Value)
            Return retVal
        Catch ex As Exception
            MsgBox("DeleteJobAdjoiningParts SqlDataReader failed. " + ex.Message)
        Finally
            Me._conn.Close()
        End Try
    End Function

    Public Function DeleteEdgeAdjoiningParts(ByVal name As String) As Integer
        Dim sqlCmd As New SqlCommand
        sqlCmd.CommandType = CommandType.StoredProcedure
        sqlCmd.CommandText = "spDeleteEdgeAdjoiningParts"
        sqlCmd.Connection = Me._conn
        If Not Me._conn.State = ConnectionState.Open Then
            Me._conn.Open()
        End If
        Try
            sqlCmd.Parameters.Add("@EdgeName", SqlDbType.VarChar)
            sqlCmd.Parameters.Item("@EdgeName").Direction = ParameterDirection.Input
            sqlCmd.Parameters.Item("@EdgeName").Value = name
            sqlCmd.Parameters.Add("@RetVal", SqlDbType.Int)
            sqlCmd.Parameters.Item("@RetVal").Direction = ParameterDirection.ReturnValue

            sqlCmd.Prepare()
            sqlCmd.ExecuteNonQuery()
            Dim retVal As Integer
            retVal = CInt(sqlCmd.Parameters.Item("@RetVal").Value)
            Return retVal
        Catch ex As Exception
            MsgBox("DeleteEdgeAdjoiningParts SqlDataReader failed. " + ex.Message)
        Finally
            Me._conn.Close()
        End Try
    End Function

#End Region

#Region "New Get Job Frame List methods"


    Public Function NewGetJobFramesList(ByVal jobNo As String) As List(Of FrontFrame)
        Dim sqlCmd As New SqlCommand("EXECUTE spGetJobFramesList @JobNumber = '" & jobNo & "'", Me._conn)
        If Not Me._conn.State = ConnectionState.Open Then
            Me._conn.Open()
        End If
        Try
            Dim frame As FrontFrame
            Dim retList As New Generic.List(Of FrontFrame)
            Dim rdr As SqlDataReader
            rdr = sqlCmd.ExecuteReader
            Try
                While rdr.Read
                    'FrameName 0, JobNumber 1, ItemNumber 2, FrameSizeW 3
                    'FrameSizeH 4, FrameStyle 5, FrameThickness 6, FrameHinge 7, WoodSpecie 8
                    frame = New FrontFrame(Me, rdr.GetString(1), rdr.GetString(2), Trim(rdr.GetString(8)))
                    frame.FrontframeSize = New SizeF(CSng((rdr.GetValue(3))), CSng(rdr.GetValue(4)))
                    retList.Add(frame)
                End While
            Finally
                rdr.Close()
            End Try
            For i As Integer = 0 To retList.Count - 1
                CType(retList(i), FrontFrame).PartsList = GetFramePartList(CType(retList(i), FrontFrame), True)
                CType(retList(i), FrontFrame).Openinglist = GetFrameOpeningList(CType(retList(i), FrontFrame), True)
            Next
            Return retList
        Catch ex As Exception
            MsgBox("GetJobFramesList SqlDataReader failed. " + ex.Message)
        Finally
            Me._conn.Close()
        End Try
        Return Nothing
    End Function
    'spGetFramePartOpeningList

#End Region

#Region "Tool Feedrate Methods"

    Public Function GetToolFeedrateSpecieNames() As List(Of String)
        Dim retList As New List(Of String)
        Dim sqlCmd As New SqlCommand("Execute  spGetToolFeedrateNames", Me._conn)
        If Not Me._conn.State = ConnectionState.Open Then
            Me._conn.Open()
        End If
        Dim rdr As SqlDataReader = sqlCmd.ExecuteReader
        Try
            While rdr.Read
                retList.Add(rdr.GetString(0))
            End While
        Catch ex As Exception
            MsgBox("GetAllToolFeedrateSpecies SqlDataReader failed. " + ex.Message)
        Finally
            rdr.Close()
            Me._conn.Close()
        End Try
        Return retList
    End Function

    Public Function GetOperationParmsClass(specie As String) As OperationParms
        Dim retClass As New OperationParms(Module1.JobOperationParms)
        Dim sqlcmd As New SqlCommand("Execute spGetToolFeedrate @Specie ='" & specie & "'", Me._conn)
        If Not Me._conn.State = ConnectionState.Open Then
            Me._conn.Open()
        End If
        Dim rdr As SqlDataReader = sqlcmd.ExecuteReader
        Try
            While rdr.Read
                retClass.haunchFeedrate = rdr.GetInt32(1)
                retClass.mortiseFeedrate = rdr.GetInt32(2)
                retClass.tenonFeedrate = rdr.GetInt32(3)
                retClass.drillFeedrate = rdr.GetInt32(4)
            End While
        Catch ex As Exception
            MsgBox("GetOperationParmsClass SqlDataReader failed. " + ex.Message)
        Finally
            rdr.Close()
            Me._conn.Close()
        End Try
        Return retClass
    End Function

    Public Function SetToolFeedrate(specie As String, parms As OperationParms, Optional ByVal leaveConnOpen As Boolean = False) As Integer
        Dim sqlCmd As New SqlCommand
        sqlCmd.CommandType = CommandType.StoredProcedure
        sqlCmd.CommandText = "spSetToolFeedrate"
        sqlCmd.Connection = Me._conn
        If Not Me._conn.State = ConnectionState.Open Then
            Me._conn.Open()
        End If
        Try
            Dim retVal As Integer = 0
            sqlCmd.Parameters.Add("@Specie", SqlDbType.VarChar)
            sqlCmd.Parameters.Item("@Specie").Direction = ParameterDirection.Input
            sqlCmd.Parameters.Item("@Specie").Value = Trim(specie)

            sqlCmd.Parameters.Add("@HaunchRate", SqlDbType.Int)
            sqlCmd.Parameters.Item("@HaunchRate").Direction = ParameterDirection.Input
            sqlCmd.Parameters.Item("@HaunchRate").Value = parms.haunchFeedrate

            sqlCmd.Parameters.Add("@MortiseRate", SqlDbType.Int)
            sqlCmd.Parameters.Item("@MortiseRate").Direction = ParameterDirection.Input
            sqlCmd.Parameters.Item("@MortiseRate").Value = parms.mortiseFeedrate

            sqlCmd.Parameters.Add("@TennonRate", SqlDbType.Int)
            sqlCmd.Parameters.Item("@TennonRate").Direction = ParameterDirection.Input
            sqlCmd.Parameters.Item("@TennonRate").Value = parms.mortiseFeedrate

            sqlCmd.Parameters.Add("@DrillRate", SqlDbType.Int)
            sqlCmd.Parameters.Item("@DrillRate").Direction = ParameterDirection.Input
            sqlCmd.Parameters.Item("@DrillRate").Value = parms.drillFeedrate

            sqlCmd.Parameters.Add("@RetVal", SqlDbType.Int)
            sqlCmd.Parameters.Item("@RetVal").Direction = ParameterDirection.ReturnValue
            sqlCmd.Prepare()
            sqlCmd.ExecuteNonQuery()
            Dim status As Integer = CInt(sqlCmd.Parameters.Item("@RetVal").Value)
            retVal = CType(status, dbTransactionTypes)
            Return retVal
        Catch ex As Exception
            MsgBox("SetToolFeedrate ExecuteNonQuery failed. " + ex.Message)
        Finally
            If Not leaveConnOpen Then
                Me._conn.Close()
            End If
        End Try
    End Function

    Public Function DeleteToolFeedrate(specie As String) As Integer
        Dim sqlCmd As New SqlCommand("EXECUTE spDeleteToolFeedrate @Specie = '" & specie & "'", Me._conn)
        If Not Me._conn.State = ConnectionState.Open Then
            Me._conn.Open()
        End If
        Try
            sqlCmd.ExecuteNonQuery()
        Catch ex As Exception
            MsgBox("DeleteToolFeedrate ExecuteNonQuery failed. " + ex.Message)
        Finally
            Me._conn.Close()
        End Try

    End Function
#End Region

End Class

