Imports System.Xml
Imports System.Data.SqlClient

''' <summary>
''' <file>File: DataClassStyles.vb</file>
''' <author>Author: Galen Newswanger</author>
''' 
''' DataClassStyles is a class intended to contain all calls to the database dealing with style objects.
''' The AccuSysCreator database tables are located in MS SQL Server "QCCSQL". All calls use stored procedures 
''' to access data.
''' </summary>
''' <remarks>
''' Note: As of September, 2012, the current stable version of AccuSysCreator2 still stores style data in 
''' an xml file named "\\Qccfile\accusysfiles\Common\SetupParms.xml" and does not use this class.
''' </remarks>
Public Class DataClassStyles

#Region "Class Instance Variables"

    Private _conn As SqlConnection
#End Region

#Region "Constructor and Initialization"

    Public Sub New()
        Me._conn = New SqlConnection(GetConnStrXml())
    End Sub

    Private Function GetConnStrXml() As String
        If System.IO.File.Exists(My.Settings.ParmsXmlFile) Then
            Dim xd As XmlDocument = New XmlDocument
            Dim xtr As XmlTextReader
            xtr = New XmlTextReader(My.Settings.ParmsXmlFile)
            Try
                xd.Load(xtr)
                Return xd.SelectSingleNode("/SetupParameters/DbConnections/ConnectionString").InnerText
            Catch ex As XmlException
                MessageBox.Show(My.Settings.ParmsXmlFile & vbCr & ex.Message & vbCr & ex.LineNumber & vbCr & ex.LinePosition)
            Finally
                xtr.Close()
            End Try
        End If
        Return Nothing
    End Function
#End Region

#Region "Get List of Style Classes"

    Public Function GetAllStyleClassInfo() As List(Of StyleClassMetaData)
        Dim retList As New List(Of StyleClassMetaData)
        retList.AddRange(GetStyleDefaultsFrame_Edge(True))
        retList.AddRange(GetStyleDefaultsHinge(True))
        retList.AddRange(GetStyleDefaultsThickness(False))
        Return retList
    End Function

    Public Function GetJobInfoStyleDefaults(ByVal jobNo As String) As List(Of StyleClassMetaData)
        Dim retList As New List(Of StyleClassMetaData)
        Dim sqlCmd As New SqlCommand("EXECUTE [spGetJobInfo] @JobNumber = '" & jobNo & "'", Me._conn)
        Dim rdr As SqlDataReader
        If Not Me._conn.State = ConnectionState.Open Then
            Me._conn.Open()
        End If
        rdr = sqlCmd.ExecuteReader
        Try
            If rdr.Read = True Then
                retList.Add(New StyleClassMetaData(rdr.GetString(1), Nothing, StyleTypes.rgFrameEdgeStyle))
                retList.Add(New StyleClassMetaData(rdr.GetString(2), Nothing, StyleTypes.rgHingeStyle))
                retList.Add(New StyleClassMetaData(rdr.GetString(3), Nothing, StyleTypes.rgThickStyle))
            Else
                Return Nothing
            End If
        Catch ex As Exception
            MsgBox("GetJobItemNoList SqlDataReader failed. " + ex.Message)
        Finally
            rdr.Close()
            Me._conn.Close()
        End Try
        Return retList
    End Function

    Private Function GetStyleDefaultsFrame_Edge(ByVal leaveOpen As Boolean) As List(Of StyleClassMetaData)
        Dim retList As New List(Of StyleClassMetaData)
        If Not Me._conn.State = ConnectionState.Open Then
            Me._conn.Open()
        End If
        Dim sqlCmd As New SqlCommand("Execute  spGetStyleDefaultsFrame_Edge", Me._conn)
        Dim rdr As SqlDataReader = sqlCmd.ExecuteReader
        Try
            While rdr.Read()
                retList.Add(New StyleClassMetaData(rdr.GetString(1), rdr.GetString(2), StyleTypes.rgFrameEdgeStyle))
            End While
        Catch ex As Exception
            MsgBox("GetStyleDefaultsFrame_Edge SqlDataReader failed. " + ex.Message)
        Finally
            rdr.Close()
            If Not leaveOpen Then
                Me._conn.Close()
            End If
        End Try
        Return retList
    End Function

    Private Function GetStyleDefaultsFrame_Part(ByVal leaveOpen As Boolean) As List(Of StyleClassMetaData)
        Dim retList As New List(Of StyleClassMetaData)
        If Not Me._conn.State = ConnectionState.Open Then
            Me._conn.Open()
        End If
        Dim sqlCmd As New SqlCommand("Execute  spGetStyleDefaultsFrame_Part", Me._conn)
        Dim rdr As SqlDataReader = sqlCmd.ExecuteReader
        Try
            While rdr.Read()
                retList.Add(New StyleClassMetaData(rdr.GetString(1), rdr.GetString(2), StyleTypes.rgFrameEdgeStyle))
            End While
        Catch ex As Exception
            MsgBox("GetStyleDefaultsFrame_Edge SqlDataReader failed. " + ex.Message)
        Finally
            rdr.Close()
            If Not leaveOpen Then
                Me._conn.Close()
            End If
        End Try
        Return retList
    End Function

    Private Function GetStyleDefaultsHinge(ByVal leaveOpen As Boolean) As List(Of StyleClassMetaData)
        If Not Me._conn.State = ConnectionState.Open Then
            Me._conn.Open()
        End If
        Dim sqlCmd As New SqlCommand("Execute  spGetStyleDefaultsHinge", Me._conn)
        Dim rdr As SqlDataReader = sqlCmd.ExecuteReader
        Dim retList As New List(Of StyleClassMetaData)
        Try
            While rdr.Read()
                retList.Add(New StyleClassMetaData(rdr.GetString(1), rdr.GetString(2), StyleTypes.rgHingeStyle))
            End While
        Catch ex As Exception
            MsgBox("GetStyleDefaultsHinge SqlDataReader failed. " + ex.Message)
        Finally
            rdr.Close()
            If Not leaveOpen Then
                Me._conn.Close()
            End If
        End Try
        Return retList
    End Function

    Private Function GetStyleDefaultsThickness(ByVal leaveOpen As Boolean) As List(Of StyleClassMetaData)
        If Not Me._conn.State = ConnectionState.Open Then
            Me._conn.Open()
        End If
        Dim sqlCmd As New SqlCommand("Execute  spGetStyleDefaultsThickness", Me._conn)

        Dim rdr As SqlDataReader = sqlCmd.ExecuteReader
        Dim retList As New List(Of StyleClassMetaData)
        Try
            While rdr.Read()
                retList.Add(New StyleClassMetaData(rdr.GetString(1), rdr.GetString(2), StyleTypes.rgThickStyle))
            End While
        Catch ex As Exception
            MsgBox("GetStyleDefaultsThickness SqlDataReader failed. " + ex.Message)
        Finally
            rdr.Close()
            If Not leaveOpen Then
                Me._conn.Close()
            End If
        End Try
        Return retList
    End Function
#End Region

#Region "Frame Edge Style "

    Public Function GetStyleFrameEdge(ByVal edgeName As String, ByVal styleName As String) As StyleFrameEdge
        Dim retStyle As StyleFrameEdge = Nothing
        If Not Me._conn.State = ConnectionState.Open Then
            Me._conn.Open()
        End If
        If ExistsStyleFrameEdge(edgeName, styleName, True) Then
            Dim sqlSelect As String = "Execute  spGetStyleFrame_Edge @EdgeName = '" & edgeName & "', @StyleName = '" & styleName & "'"
            Dim sqlCmd As New SqlCommand(sqlSelect, Me._conn)
            Try
                Dim rdr As SqlDataReader
                rdr = sqlCmd.ExecuteReader
                If rdr.Read Then
                    retStyle = New StyleFrameEdge(rdr.GetString(0), rdr.GetString(1))
                    retStyle.Description = rdr.GetString(2)
                    retStyle.HaunchEndAdj = rdr.GetDecimal(3)
                    retStyle.MortiseEndAdj = rdr.GetDecimal(4)
                    retStyle.HaunchDepth = rdr.GetDecimal(5)
                    retStyle.MortiseDepth = rdr.GetDecimal(6)
                    retStyle.MortiseShoulder = rdr.GetDecimal(7)
                    retStyle.PilotCenterAdj = rdr.GetDecimal(8)
                End If
            Catch ex As Exception
                MsgBox("GetStyleFrameEdge SqlDataReader failed. " + ex.Message)
                Return Nothing
            Finally
                Me._conn.Close()
            End Try
        End If
        Return retStyle
    End Function

    Public Function SetStyleFrameEdge(ByVal style As StyleFrameEdge, ByVal leaveConnOpen As Boolean) As Integer
        Dim sqlCmd As New SqlCommand
        sqlCmd.CommandType = CommandType.StoredProcedure
        sqlCmd.CommandText = "[spSetStyleFrame_Edge]"
        sqlCmd.Connection = Me._conn
        If Not Me._conn.State = ConnectionState.Open Then
            Me._conn.Open()
        End If
        Try
            sqlCmd.Parameters.Add("@EdgeName", SqlDbType.NChar)
            sqlCmd.Parameters.Item("@EdgeName").Direction = ParameterDirection.Input
            sqlCmd.Parameters.Item("@EdgeName").Value = Trim(style.EdgeName)
            sqlCmd.Parameters.Add("@StyleName", SqlDbType.NVarChar)
            sqlCmd.Parameters.Item("@StyleName").Direction = ParameterDirection.Input
            sqlCmd.Parameters.Item("@StyleName").Value = Trim(style.StyleName)

            sqlCmd.Parameters.Add("@StyleDescription", SqlDbType.NVarChar)
            sqlCmd.Parameters.Item("@StyleDescription").Direction = ParameterDirection.Input
            sqlCmd.Parameters.Item("@StyleDescription").Value = Trim(style.Description)

            sqlCmd.Parameters.Add("@HaunchEndAdj", SqlDbType.Decimal)
            sqlCmd.Parameters.Item("@HaunchEndAdj").Direction = ParameterDirection.Input
            sqlCmd.Parameters.Item("@HaunchEndAdj").Value = style.HaunchEndAdj

            sqlCmd.Parameters.Add("@MortiseEndAdj", SqlDbType.Decimal)
            sqlCmd.Parameters.Item("@MortiseEndAdj").Direction = ParameterDirection.Input
            sqlCmd.Parameters.Item("@MortiseEndAdj").Value = style.MortiseEndAdj

            sqlCmd.Parameters.Add("@HaunchDepth", SqlDbType.Decimal)
            sqlCmd.Parameters.Item("@HaunchDepth").Direction = ParameterDirection.Input
            sqlCmd.Parameters.Item("@HaunchDepth").Value = style.HaunchDepth

            sqlCmd.Parameters.Add("@MortiseDepth", SqlDbType.Decimal)
            sqlCmd.Parameters.Item("@MortiseDepth").Direction = ParameterDirection.Input
            sqlCmd.Parameters.Item("@MortiseDepth").Value = style.MortiseDepth


            sqlCmd.Parameters.Add("@MortiseShoulder", SqlDbType.Decimal)
            sqlCmd.Parameters.Item("@MortiseShoulder").Direction = ParameterDirection.Input
            sqlCmd.Parameters.Item("@MortiseShoulder").Value = style.MortiseShoulder

            sqlCmd.Parameters.Add("@PilotCenterAdj", SqlDbType.Decimal)
            sqlCmd.Parameters.Item("@PilotCenterAdj").Direction = ParameterDirection.Input
            sqlCmd.Parameters.Item("@PilotCenterAdj").Value = style.PilotCenterAdj

            sqlCmd.Prepare()
            Dim retVal As Integer = 0
            retVal = sqlCmd.ExecuteNonQuery()
        Catch ex As Exception
            MsgBox("SetStyleFrameEdge ExecuteNonQuery failed. " + ex.Message)
        Finally
            If Not leaveConnOpen Then
                Me._conn.Close()
            End If
        End Try
        Return 0
    End Function

    Private Function ExistsStyleFrameEdge(ByVal edgeName As String, ByVal styleName As String, ByVal leaveOpen As Boolean) As Boolean
        Dim sqlCmd As New SqlCommand("Execute  spExistsStyleFrame_Edge @EdgeName = '" & edgeName & "', @StyleName = '" & styleName & "'", Me._conn)
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
            MsgBox("ExistsStyleFrameEdge ExecuteScalar failed. " + ex.Message)
        Finally
            If Not leaveOpen Then
                Me._conn.Close()
            End If
        End Try
    End Function
#End Region

#Region "Frame Part Style"
    Public Function GetStyleFramePart(ByVal partName As String, ByVal styleName As String) As StyleFramePart
        Dim retStyle As StyleFramePart = Nothing
        If Not Me._conn.State = ConnectionState.Open Then
            Me._conn.Open()
        End If
        If ExistsStyleFramePart(partName, styleName, True) Then
            Dim sqlSelect As String = "Execute  spGetStyleFrame_Part @PartName = '" & partName & "', @StyleName = '" & styleName & "'"
            Dim sqlCmd As New SqlCommand(sqlSelect, Me._conn)
            Try
                Dim rdr As SqlDataReader
                rdr = sqlCmd.ExecuteReader
                If rdr.Read Then
                    retStyle = New StyleFramePart(rdr.GetString(0), rdr.GetString(1))
                    retStyle.Description = rdr.GetString(2)
                    retStyle.IsMiddleRail = rdr.GetBoolean(3)
                    retStyle.TenonLengthNear = rdr.GetDecimal(4)
                    retStyle.TenonLengthFar = rdr.GetDecimal(5)
                    retStyle.TenonHaunchNear = rdr.GetDecimal(6)
                    retStyle.TenonHaunchFar = rdr.GetDecimal(7)
                    retStyle.SingleTenonLimit = rdr.GetDecimal(8)
                    retStyle.DblTenonSeparation = rdr.GetDecimal(9)
                    retStyle.GlueAllowance = rdr.GetDecimal(10)
                    retStyle.TenonPartwidthLimit = rdr.GetDecimal(11)
                    retStyle.TenonValancewidthLimit = rdr.GetDecimal(12)
                End If
            Catch ex As Exception
                MsgBox("GetStyleFramePart SqlDataReader failed. " + ex.Message)
                Return Nothing
            Finally
                Me._conn.Close()
            End Try
        End If
        Return retStyle
    End Function

    Public Function SetStyleFramePart(ByVal style As StyleFramePart, ByVal leaveConnOpen As Boolean) As Integer
        Dim sqlCmd As New SqlCommand
        sqlCmd.CommandType = CommandType.StoredProcedure
        sqlCmd.CommandText = "[spSetStyleFrame_Part]"
        sqlCmd.Connection = Me._conn
        If Not Me._conn.State = ConnectionState.Open Then
            Me._conn.Open()
        End If
        Try
            sqlCmd.Parameters.Add("@PartName", SqlDbType.NChar)
            sqlCmd.Parameters.Item("@PartName").Direction = ParameterDirection.Input
            sqlCmd.Parameters.Item("@PartName").Value = Trim(style.PartName)
            sqlCmd.Parameters.Add("@StyleName", SqlDbType.NVarChar)
            sqlCmd.Parameters.Item("@StyleName").Direction = ParameterDirection.Input
            sqlCmd.Parameters.Item("@StyleName").Value = Trim(style.StyleName)

            sqlCmd.Parameters.Add("@StyleDescription", SqlDbType.NVarChar)
            sqlCmd.Parameters.Item("@StyleDescription").Direction = ParameterDirection.Input
            sqlCmd.Parameters.Item("@StyleDescription").Value = Trim(style.Description)

            sqlCmd.Parameters.Add("@IsMiddleRail", SqlDbType.Bit)
            sqlCmd.Parameters.Item("@IsMiddleRail").Direction = ParameterDirection.Input
            sqlCmd.Parameters.Item("@IsMiddleRail").Value = style.IsMiddleRail

            sqlCmd.Parameters.Add("@TenonLengthNear", SqlDbType.Decimal)
            sqlCmd.Parameters.Item("@TenonLengthNear").Direction = ParameterDirection.Input
            sqlCmd.Parameters.Item("@TenonLengthNear").Value = style.TenonLengthNear

            sqlCmd.Parameters.Add("@TenonLengthFar", SqlDbType.Decimal)
            sqlCmd.Parameters.Item("@TenonLengthFar").Direction = ParameterDirection.Input
            sqlCmd.Parameters.Item("@TenonLengthFar").Value = style.TenonLengthFar

            sqlCmd.Parameters.Add("@TenonHaunchNear", SqlDbType.Decimal)
            sqlCmd.Parameters.Item("@TenonHaunchNear").Direction = ParameterDirection.Input
            sqlCmd.Parameters.Item("@TenonHaunchNear").Value = style.TenonHaunchNear


            sqlCmd.Parameters.Add("@TenonHaunchFar", SqlDbType.Decimal)
            sqlCmd.Parameters.Item("@TenonHaunchFar").Direction = ParameterDirection.Input
            sqlCmd.Parameters.Item("@TenonHaunchFar").Value = style.TenonHaunchFar

            sqlCmd.Parameters.Add("@SingleTenonLimit", SqlDbType.Decimal)
            sqlCmd.Parameters.Item("@SingleTenonLimit").Direction = ParameterDirection.Input
            sqlCmd.Parameters.Item("@SingleTenonLimit").Value = style.SingleTenonLimit

            sqlCmd.Parameters.Add("@DblTenonSeparation", SqlDbType.Decimal)
            sqlCmd.Parameters.Item("@DblTenonSeparation").Direction = ParameterDirection.Input
            sqlCmd.Parameters.Item("@DblTenonSeparation").Value = style.DblTenonSeparation

            sqlCmd.Parameters.Add("@GlueAllowance", SqlDbType.Decimal)
            sqlCmd.Parameters.Item("@GlueAllowance").Direction = ParameterDirection.Input
            sqlCmd.Parameters.Item("@GlueAllowance").Value = style.GlueAllowance

            sqlCmd.Parameters.Add("@MaxTenonPartWidth", SqlDbType.Decimal)
            sqlCmd.Parameters.Item("@MaxTenonPartWidth").Direction = ParameterDirection.Input
            sqlCmd.Parameters.Item("@MaxTenonPartWidth").Value = style.TenonPartwidthLimit

            sqlCmd.Parameters.Add("@MaxTenonValWidth", SqlDbType.Decimal)
            sqlCmd.Parameters.Item("@MaxTenonValWidth").Direction = ParameterDirection.Input
            sqlCmd.Parameters.Item("@MaxTenonValWidth").Value = style.TenonValancewidthLimit

            sqlCmd.Prepare()
            Dim retVal As Integer = 0
            retVal = sqlCmd.ExecuteNonQuery()
        Catch ex As Exception
            MsgBox("SetStyleFramePart ExecuteNonQuery failed. " + ex.Message)
        Finally
            If Not leaveConnOpen Then
                Me._conn.Close()
            End If
        End Try
        Return 0
    End Function

    Private Function ExistsStyleFramePart(ByVal partName As String, ByVal styleName As String, ByVal leaveOpen As Boolean) As Boolean
        Dim sqlCmd As New SqlCommand("Execute  spExistsStyleFrame_Part @PartName = '" & partName & "', @StyleName = '" & styleName & "'", Me._conn)
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
            MsgBox("ExistsStyleFramePart ExecuteScalar failed. " + ex.Message)
        Finally
            If Not leaveOpen Then
                Me._conn.Close()
            End If
        End Try
    End Function
#End Region

#Region "Hinge Style"


    Public Function GetStyleHinge(ByVal edgeName As String, ByVal styleName As String) As StyleHinge
        Dim retStyle As StyleHinge = Nothing
        If ExistsStyleHinge(edgeName, styleName, False) Then
            Dim sqlSelect As String = "Execute  spGetStyleHinge @EdgeName = '" & edgeName & "', @StyleName = '" & styleName & "'"
            Dim sqlCmd As New SqlCommand(sqlSelect, Me._conn)
            If Not Me._conn.State = ConnectionState.Open Then
                Me._conn.Open()
            End If
            Try
                Dim rdr As SqlDataReader
                rdr = sqlCmd.ExecuteReader
                If rdr.Read Then
                    retStyle = New StyleHinge(rdr.GetString(0), rdr.GetString(1))
                    retStyle.Description = rdr.GetString(2)
                    retStyle.MortiseWidth = rdr.GetDecimal(4)
                    retStyle.MortiseDepth = rdr.GetDecimal(5)
                    retStyle.PilotDepth = rdr.GetDecimal(6)
                    retStyle.PilotDia = rdr.GetDecimal(7)
                    retStyle.VCenterOuter = rdr.GetDecimal(8)
                    retStyle.DistBetweenOuter = rdr.GetDecimal(9)
                    retStyle.HingeRule = rdr.GetString(10)
                End If
            Catch ex As Exception
                MsgBox("GetStyleHinge SqlDataReader failed. " + ex.Message)
                Return Nothing
            Finally
                Me._conn.Close()
            End Try
        End If
        Return retStyle
    End Function

    Public Function SetStyleHinge(ByVal style As StyleHinge, ByVal leaveConnOpen As Boolean) As Integer
        Dim sqlCmd As New SqlCommand
        sqlCmd.CommandType = CommandType.StoredProcedure
        sqlCmd.CommandText = "[spSetStyleHinge]"
        sqlCmd.Connection = Me._conn
        If Not Me._conn.State = ConnectionState.Open Then
            Me._conn.Open()
        End If
        Try
            sqlCmd.Parameters.Add("@EdgeName", SqlDbType.NChar)
            sqlCmd.Parameters.Item("@EdgeName").Direction = ParameterDirection.Input
            sqlCmd.Parameters.Item("@EdgeName").Value = Trim(style.EdgeName)
            sqlCmd.Parameters.Add("@StyleName", SqlDbType.NVarChar)
            sqlCmd.Parameters.Item("@StyleName").Direction = ParameterDirection.Input
            sqlCmd.Parameters.Item("@StyleName").Value = Trim(style.StyleName)

            sqlCmd.Parameters.Add("@StyleDescription", SqlDbType.NVarChar)
            sqlCmd.Parameters.Item("@StyleDescription").Direction = ParameterDirection.Input
            sqlCmd.Parameters.Item("@StyleDescription").Value = Trim(style.Description)

            sqlCmd.Parameters.Add("@IsMortised", SqlDbType.Bit)
            sqlCmd.Parameters.Item("@IsMortised").Direction = ParameterDirection.Input
            sqlCmd.Parameters.Item("@IsMortised").Value = style.IsMortised

            sqlCmd.Parameters.Add("@MortiseWidth", SqlDbType.Decimal)
            sqlCmd.Parameters.Item("@MortiseWidth").Direction = ParameterDirection.Input
            sqlCmd.Parameters.Item("@MortiseWidth").Value = style.MortiseWidth

            sqlCmd.Parameters.Add("@MortiseDepth", SqlDbType.Decimal)
            sqlCmd.Parameters.Item("@MortiseDepth").Direction = ParameterDirection.Input
            sqlCmd.Parameters.Item("@MortiseDepth").Value = style.MortiseDepth

            sqlCmd.Parameters.Add("@PilotDepth", SqlDbType.Decimal)
            sqlCmd.Parameters.Item("@PilotDepth").Direction = ParameterDirection.Input
            sqlCmd.Parameters.Item("@PilotDepth").Value = style.PilotDepth

            sqlCmd.Parameters.Add("@PilotDia", SqlDbType.Decimal)
            sqlCmd.Parameters.Item("@PilotDia").Direction = ParameterDirection.Input
            sqlCmd.Parameters.Item("@PilotDia").Value = style.PilotDia

            sqlCmd.Parameters.Add("@PilotCenterline", SqlDbType.Decimal)
            sqlCmd.Parameters.Item("@PilotCenterline").Direction = ParameterDirection.Input
            sqlCmd.Parameters.Item("@PilotCenterline").Value = style.VCenterOuter

            sqlCmd.Parameters.Add("@BetweenPilots", SqlDbType.Decimal)
            sqlCmd.Parameters.Item("@BetweenPilots").Direction = ParameterDirection.Input
            sqlCmd.Parameters.Item("@BetweenPilots").Value = style.DistBetweenOuter

            sqlCmd.Parameters.Add("@HingeRule", SqlDbType.Decimal)
            sqlCmd.Parameters.Item("@HingeRule").Direction = ParameterDirection.Input
            sqlCmd.Parameters.Item("@HingeRule").Value = Trim(style.HingeRule)
            sqlCmd.Prepare()
            Dim retVal As Integer = 0
            retVal = sqlCmd.ExecuteNonQuery()
        Catch ex As Exception
            MsgBox("SetStyleHinge ExecuteNonQuery failed. " + ex.Message)
        Finally
            If Not leaveConnOpen Then
                Me._conn.Close()
            End If
        End Try
        Return 0
    End Function

    Private Function ExistsStyleHinge(ByVal edgeName As String, ByVal styleName As String, ByVal leaveOpen As Boolean) As Boolean
        Dim sqlCmd As New SqlCommand("Execute  spExistsStyleHinge @EdgeName = '" & edgeName & "', @StyleName = '" & styleName & "'", Me._conn)
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
            MsgBox("ExistsStyleHinge ExecuteScalar failed. " + ex.Message)
        Finally
            If Not leaveOpen Then
                Me._conn.Close()
            End If
        End Try
    End Function
#End Region

#Region "Thickness Style"

    Public Function GetStyleThickness(ByVal edgeName As String, ByVal styleName As String) As StyleThickness
        Dim retStyle As StyleThickness = Nothing
        If ExistsStyleThickness(edgeName, styleName, False) Then
            Dim sqlSelect As String = "Execute  spGetStyleThickness @EdgeName = '" & edgeName & "', @StyleName = '" & styleName & "'"
            Dim sqlCmd As New SqlCommand(sqlSelect, Me._conn)
            If Not Me._conn.State = ConnectionState.Open Then
                Me._conn.Open()
            End If
            Try
                Dim rdr As SqlDataReader
                rdr = sqlCmd.ExecuteReader
                If rdr.Read Then
                    retStyle = New StyleThickness(rdr.GetString(0), rdr.GetString(1))
                    retStyle.Description = rdr.GetString(2)
                    retStyle.Thickness = rdr.GetDecimal(3)
                    retStyle.BoardHeight = rdr.GetDecimal(4)
                    retStyle.ToolLine = rdr.GetDecimal(5)
                    retStyle.RouteTopWidth = rdr.GetDecimal(6)
                    retStyle.RouteMiddleWidth = rdr.GetDecimal(7)
                    retStyle.RouteBottomWidth = rdr.GetDecimal(8)
                End If
            Catch ex As Exception
                MsgBox("GetStyleThickness SqlDataReader failed. " + ex.Message)
                Return Nothing
            Finally
                Me._conn.Close()
            End Try
        End If
        Return retStyle
    End Function

    Public Function SetStyleThickness(ByVal style As StyleThickness, ByVal leaveConnOpen As Boolean) As Integer
        Dim sqlCmd As New SqlCommand
        sqlCmd.CommandType = CommandType.StoredProcedure
        sqlCmd.CommandText = "spSetStyleThickness"
        sqlCmd.Connection = Me._conn
        If Not Me._conn.State = ConnectionState.Open Then
            Me._conn.Open()
        End If
        Try
            sqlCmd.Parameters.Add("@EdgeName", SqlDbType.NChar)
            sqlCmd.Parameters.Item("@EdgeName").Direction = ParameterDirection.Input
            sqlCmd.Parameters.Item("@EdgeName").Value = Trim(style.EdgeName)

            sqlCmd.Parameters.Add("@StyleName", SqlDbType.NVarChar)
            sqlCmd.Parameters.Item("@StyleName").Direction = ParameterDirection.Input
            sqlCmd.Parameters.Item("@StyleName").Value = Trim(style.StyleName)

            sqlCmd.Parameters.Add("@StyleDescription", SqlDbType.NVarChar)
            sqlCmd.Parameters.Item("@StyleDescription").Direction = ParameterDirection.Input
            sqlCmd.Parameters.Item("@StyleDescription").Value = Trim(style.Description)

            sqlCmd.Parameters.Add("@Thickness", SqlDbType.Decimal)
            sqlCmd.Parameters.Item("@Thickness").Direction = ParameterDirection.Input
            sqlCmd.Parameters.Item("@Thickness").Value = style.Thickness

            sqlCmd.Parameters.Add("@BoardHght", SqlDbType.Decimal)
            sqlCmd.Parameters.Item("@BoardHght").Direction = ParameterDirection.Input
            sqlCmd.Parameters.Item("@BoardHght").Value = style.BoardHeight

            sqlCmd.Parameters.Add("@ToolLine", SqlDbType.Decimal)
            sqlCmd.Parameters.Item("@ToolLine").Direction = ParameterDirection.Input
            sqlCmd.Parameters.Item("@ToolLine").Value = style.ToolLine

            sqlCmd.Parameters.Add("@RouteTopWidth", SqlDbType.Decimal)
            sqlCmd.Parameters.Item("@RouteTopWidth").Direction = ParameterDirection.Input
            sqlCmd.Parameters.Item("@RouteTopWidth").Value = style.RouteTopWidth

            sqlCmd.Parameters.Add("@RouteMiddleWidth", SqlDbType.Decimal)
            sqlCmd.Parameters.Item("@RouteMiddleWidth").Direction = ParameterDirection.Input
            sqlCmd.Parameters.Item("@RouteMiddleWidth").Value = style.RouteMiddleWidth

            sqlCmd.Parameters.Add("@RouteBottomWidth", SqlDbType.Decimal)
            sqlCmd.Parameters.Item("@RouteBottomWidth").Direction = ParameterDirection.Input
            sqlCmd.Parameters.Item("@RouteBottomWidth").Value = style.RouteBottomWidth

            sqlCmd.Prepare()
            Dim retVal As Integer = 0
            retVal = sqlCmd.ExecuteNonQuery()
        Catch ex As Exception
            MsgBox("SetStyleThickness ExecuteNonQuery failed. " + ex.Message)
        Finally
            If Not leaveConnOpen Then
                Me._conn.Close()
            End If
        End Try
        Return 0
    End Function

    Private Function ExistsStyleThickness(ByVal edgeName As String, ByVal styleName As String, ByVal leaveOpen As Boolean) As Boolean
        Dim sqlCmd As New SqlCommand("Execute  spExistsStyleThickness @EdgeName = '" & edgeName & "', @StyleName = '" & styleName & "'", Me._conn)
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
            MsgBox("ExistsStyleThickness ExecuteScalar failed. " + ex.Message)
        Finally
            If Not leaveOpen Then
                Me._conn.Close()
            End If
        End Try
    End Function

#End Region

#Region "Hinge Style Rules"

    Public Function GetHingeStyleRule(ByVal edgeName As String, ByVal rule As String) As HingeStyleRule
        Dim retStyle As HingeStyleRule = Nothing
        If ExistsHingeStyleRule(edgeName, rule, False) Then
            Dim sqlSelect As String = "Execute  spGetHingeStyleRule @EdgeName = '" & edgeName & "', @RuleName = '" & rule & "'"
            Dim sqlCmd As New SqlCommand(sqlSelect, Me._conn)
            If Not Me._conn.State = ConnectionState.Open Then
                Me._conn.Open()
            End If
            Try
                Dim rdr As SqlDataReader
                rdr = sqlCmd.ExecuteReader
                If rdr.Read Then
                    retStyle = New HingeStyleRule(rdr.GetString(0), rdr.GetString(1))
                    retStyle.MortiseOffsetStdOpg = rdr.GetDecimal(2)
                    retStyle.MortiseOffsetSmallOpg = rdr.GetDecimal(3)
                    retStyle.SmallOpgMinHght2Hinges = rdr.GetDecimal(4)
                    retStyle.StdOpgMinHght2Hinges = rdr.GetDecimal(5)
                    retStyle.StdOpgMaxHght2Hinges = rdr.GetDecimal(6)
                    retStyle.StdOpgMaxHght3Hinges = rdr.GetDecimal(7)
                    retStyle.StdOpgMaxHght4Hinges = rdr.GetDecimal(8)
                End If
            Catch ex As Exception
                MsgBox("GetHingeStyleRule SqlDataReader failed. " + ex.Message)
                Return Nothing
            Finally
                Me._conn.Close()
            End Try
        End If
        Return retStyle
    End Function

    Public Function SetHingeStyleRule(ByVal style As HingeStyleRule, ByVal leaveConnOpen As Boolean) As Integer
        Dim retVal As Integer = 0
        Dim sqlCmd As New SqlCommand
        sqlCmd.CommandType = CommandType.StoredProcedure
        sqlCmd.CommandText = "spSetHingeStyleRule"
        sqlCmd.Connection = Me._conn
        If Not Me._conn.State = ConnectionState.Open Then
            Me._conn.Open()
        End If
        Try
            sqlCmd.Parameters.Add("@EdgeName", SqlDbType.NChar)
            sqlCmd.Parameters.Item("@EdgeName").Direction = ParameterDirection.Input
            sqlCmd.Parameters.Item("@EdgeName").Value = Trim(style.EdgeName)

            sqlCmd.Parameters.Add("@RuleName", SqlDbType.NChar)
            sqlCmd.Parameters.Item("@RuleName").Direction = ParameterDirection.Input
            sqlCmd.Parameters.Item("@RuleName").Value = Trim(style.StyleName)

            sqlCmd.Parameters.Add("@MortiseOffset", SqlDbType.Decimal)
            sqlCmd.Parameters.Item("@MortiseOffset").Direction = ParameterDirection.Input
            sqlCmd.Parameters.Item("@MortiseOffset").Value = style.MortiseOffsetStdOpg

            sqlCmd.Parameters.Add("@SmallOpgMortOffset", SqlDbType.Decimal)
            sqlCmd.Parameters.Item("@SmallOpgMortOffset").Direction = ParameterDirection.Input
            sqlCmd.Parameters.Item("@SmallOpgMortOffset").Value = style.MortiseOffsetSmallOpg

            sqlCmd.Parameters.Add("@MinHght42Hinges", SqlDbType.Decimal)
            sqlCmd.Parameters.Item("@MinHght42Hinges").Direction = ParameterDirection.Input
            sqlCmd.Parameters.Item("@MinHght42Hinges").Value = style.SmallOpgMinHght2Hinges

            sqlCmd.Parameters.Add("@HghtRange42Lower", SqlDbType.Decimal)
            sqlCmd.Parameters.Item("@HghtRange42Lower").Direction = ParameterDirection.Input
            sqlCmd.Parameters.Item("@HghtRange42Lower").Value = style.StdOpgMinHght2Hinges

            sqlCmd.Parameters.Add("@HghtRange42Upper", SqlDbType.Decimal)
            sqlCmd.Parameters.Item("@HghtRange42Upper").Direction = ParameterDirection.Input
            sqlCmd.Parameters.Item("@HghtRange42Upper").Value = style.StdOpgMaxHght2Hinges

            sqlCmd.Parameters.Add("@HghtRange43Upper", SqlDbType.Decimal)
            sqlCmd.Parameters.Item("@HghtRange43Upper").Direction = ParameterDirection.Input
            sqlCmd.Parameters.Item("@HghtRange43Upper").Value = style.StdOpgMaxHght3Hinges

            sqlCmd.Parameters.Add("@HghtRange44Upper", SqlDbType.Decimal)
            sqlCmd.Parameters.Item("@HghtRange44Upper").Direction = ParameterDirection.Input
            sqlCmd.Parameters.Item("@HghtRange44Upper").Value = style.StdOpgMaxHght4Hinges

            sqlCmd.Prepare()
            retVal = sqlCmd.ExecuteNonQuery()
        Catch ex As Exception
            MsgBox("SetHingeStyleRule ExecuteNonQuery failed. " + ex.Message)
        Finally
            If Not leaveConnOpen Then
                Me._conn.Close()
            End If
        End Try
        Return retVal
    End Function

    Private Function ExistsHingeStyleRule(ByVal edgeName As String, ByVal rule As String, ByVal leaveOpen As Boolean) As Boolean
        Dim sqlCmd As New SqlCommand("Execute  spExistsHingeStyleRule @EdgeName = '" & edgeName & "', @RuleName = '" & rule & "'", Me._conn)
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
            MsgBox("ExistsHingeStyleRule ExecuteScalar failed. " + ex.Message)
        Finally
            If Not leaveOpen Then
                Me._conn.Close()
            End If
        End Try
    End Function

#End Region

End Class
