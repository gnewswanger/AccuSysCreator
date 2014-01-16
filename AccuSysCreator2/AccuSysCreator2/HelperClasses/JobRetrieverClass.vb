Imports System.IO
Imports System.Text.RegularExpressions

''' <summary>
''' <file>File: JobRetrieverClass.vb</file>
''' <author>Author: Galen Newswanger</author>
''' 
''' This class provides methods for accessing data from csv files.  The initial csv file is 
''' generated in Function LoadJob().  
''' </summary>
''' <remarks></remarks>
Public Class JobRetrieverClass

    Private _db As New DataClass(Module1.ParmFilename)

    Public Function LoadJob(ByVal jobNumber As String, ByVal dataStoreDir As String, ByVal bcDir As String, ByVal inputDataExec As String) As Boolean
        Dim eCode As Integer
        Dim eTime As String
        Dim myProcess As New Process
        Try
            'myProcess.EnableRaisingEvents = True
            myProcess.StartInfo.Arguments = jobNumber & " " & dataStoreDir & " " & bcDir
            myProcess.StartInfo.FileName = inputDataExec
            myProcess.StartInfo.CreateNoWindow = True
            myProcess.StartInfo.UseShellExecute = False
#If DEBUG Then
            AddHandler myProcess.Exited, AddressOf Me.ProcessExited
#End If
            myProcess.Start()
            myProcess.WaitForExit()
            eCode = myProcess.ExitCode
            eTime = myProcess.ExitTime.ToString
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            myProcess.Close()
        End Try
        Dim prgbar As New ProgressBar

        If eCode = 0 Then
            Dim fn As String = dataStoreDir & jobNumber + ".csv"
            If File.Exists(fn) Then
                Dim fs As FileStream = File.OpenRead(fn)
                Try
                    If fs.Length = 0 Then
                        MsgBox("No parts found for job #" & jobNumber, MsgBoxStyle.Exclamation, "Nothing Found")
                        Return False
                    Else
                        fs.Close()
                    End If
                Finally
                    fs.Close()
                End Try
            Else
                MsgBox("Job #" & jobNumber & " was not found. Check for entry error.", MsgBoxStyle.Exclamation, "Nothing Found")
                Return False
            End If
        ElseIf eCode = 1 Then
            MessageBox.Show("The job number, " & jobNumber & ", was not found " _
            & "or it has no bill of materials available yet.  Confirm that the " _
            & " number was entered correctly.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        ElseIf eCode = 2 Then
            MessageBox.Show("File access was denied", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End If
        Return True
    End Function

    Friend Sub ProcessExited(ByVal sender As Object, _
       ByVal e As System.EventArgs)
        Dim aProcess As Process = DirectCast( _
          sender, Process)
        MessageBox.Show("The process exited, raising " & _
          "the Exited event at: " & aProcess.ExitTime & _
           "." & System.Environment.NewLine & _
           "Exit Code: " & aProcess.ExitCode)
    End Sub

    Public Function ImportCSVFile(ByVal num As String, ByVal dataStoreDir As String) As FrameListClass
        Dim jobNum As String = Trim(num)
        If Me._db.JobDataExists(jobNum) Then
            Me._db.DeleteCompleteJob(jobNum)
        End If
        Dim framelist As New FrameListClass(Me._db, jobNum)

        Dim sr As StreamReader = New StreamReader(dataStoreDir + jobNum + ".csv")

        Dim frame As FrontFrame
        Try
            Do While sr.Peek() >= 0
                Dim line As String = sr.ReadLine
                Dim strs() As String = line.Split(CChar(","))
                Dim code As String = strs(4)
                Dim item As String = strs(3).Trim(CChar(""""))
                PadCodeItemFields(code, item)

                frame = framelist.GetFrameByName(jobNum & "." & item)
                If IsNothing(frame) Then
                    frame = New FrontFrame(Me._db, jobNum, item, strs(0))
                    frame.Code = strs(4)
                    framelist.AddFrame2List(frame)
                End If

                If strs(4).Trim.StartsWith("FR") OrElse strs(4).Trim.StartsWith("SFR") Then
                    frame.FrontframeSize = New SizeF(CSng(strs(5)), CSng(strs(6)))
                Else
                    Dim ps As New FramePartBaseClass(jobNum & "." & item & "." & code)
                    'ps.ItemNo = item
                    ps.Width = CSng(strs(5))
                    ps.Length = CSng(strs(6))
                    ps.Thickness = CSng(strs(7))
                    If ps.Code.StartsWith("OP") Or ps.Code.StartsWith("CUT") Then
                        ps.Thickness = 0
                        ps.PartType = FrontFrameEventClasses.PartEdgeTypes.Opening
                        If Not IsNumeric(ps.Name.Substring(ps.Name.Length - 1)) Then
                            ps.Name = ps.Name & frame.Openinglist.Count + 1
                        End If
                    ElseIf ps.Code.StartsWith("SC") AndAlso Not IsNumeric(ps.Name.Substring(ps.Name.Length - 1)) Then
                        ps.Name = ps.Name & frame.PartsList.Count + 1
                    ElseIf ps.Code.StartsWith("GIS") Then
                        ps.Name = ps.Name & frame.PartsList.Count + 1
                        If ps.Thickness = 0 Then
                            ps.Thickness = Module1.JobThickness.BoardHeight
                        End If
                    End If

                    Dim opParms As OperationParms = Me._db.GetOperationParmsClass(strs(1))
                    Dim fp As New FramePart(frame, ps, HingePlacement.N, opParms)
                    fp.Grade = CChar(strs(1))
                    fp.TSComments = CStr(strs(9))
                    frame.AppendFramePart(fp)
                End If
            Loop
            Return framelist

        Finally
            sr.Close()
        End Try
    End Function

    Private Sub PadCodeItemFields(ByRef code As String, ByRef item As String)
        If Trim(code).EndsWith("-L") AndAlso Trim(code).LastIndexOf("BEP") < 0 Then
            If Trim(item).EndsWith("L") Then
                item = Trim(item).PadLeft(3, CChar("0")) '& "L"
            Else
                item = Trim(item).PadLeft(2, CChar("0"))
            End If
            code = Trim(code).Substring(0, code.LastIndexOf("-"))
        ElseIf Trim(code).EndsWith("-R") AndAlso Trim(code).LastIndexOf("BEP") < 0 Then
            If Trim(item).EndsWith("R") Then
                item = Trim(item).PadLeft(3, CChar("0")) '& "R"
            Else
                item = Trim(item).PadLeft(2, CChar("0"))
            End If
            code = Trim(code).Substring(0, code.LastIndexOf("-"))
        ElseIf Trim(code).EndsWith("-B") AndAlso Trim(code).LastIndexOf("BEP") < 0 Then
            If Trim(item).EndsWith("B") Then
                item = Trim(item).PadLeft(3, CChar("0")) '& "B"
            Else
                item = Trim(item).PadLeft(2, CChar("0"))
            End If
            code = Trim(code).Substring(0, code.LastIndexOf("-"))
        ElseIf Trim(item).EndsWith("A") Or Trim(item).EndsWith("B") Then
            item = Trim(item).PadLeft(3, CChar("0")) '
        Else
            item = Trim(item).PadLeft(2, CChar("0"))
        End If
    End Sub

    Public Function GetCabinetPartList(ByVal frmList As FrameListClass) As ArrayList
        Dim list As New ArrayList
        For index1 As Integer = 0 To frmList.FrameCount - 1
            list.Add("Job#.Item#: " & frmList.GetFrameByIndex(index1).Name & vbTab & frmList.GetFrameByIndex(index1).Code & "  " _
            & "Cabinet Size: " & frmList.GetFrameByIndex(index1).FrontframeSize.Width.ToString & " X " & frmList.GetFrameByIndex(index1).FrontframeSize.Height)
            For index2 As Integer = 0 To frmList.GetFrameByIndex(index1).PartsList.Count - 1
                'lbCabList.Items.Add(vbTab & "Part Name: " & pt.Name.Substring(pt.Name.IndexOf(".") + 1) & vbTab & "(" & ps.width & " X " & ps.length & ")")
                Dim fp As FramePart = CType(frmList.GetFrameByIndex(index1).PartsList(index2), FramePart)
                If ((fp.PartType And FrontFrameEventClasses.PartEdgeTypes.Opening) = 0) Then
                    list.Add(vbTab & "Part Name: " & fp.Name.Substring(fp.Name.IndexOf(".") + 1) & vbTab & "(" & fp.Width & " X " & fp.Length & ")")
                End If
            Next
        Next
        Return list
    End Function

    Public Function ImportFrameFromCSVFile(ByRef list As FrameListClass, ByVal itemNo As String, ByVal dataStoreDir As String) As FrontFrame
        Dim sr As StreamReader = New StreamReader(dataStoreDir + list.JobNumber + ".csv")
        Dim frame As FrontFrame = list.GetFrameByName(list.JobNumber & "." & itemNo)
        Try
            If frame IsNot Nothing Then
                list.DeleteFramePartsForItemNo(frame.ItemNo)
                Do While sr.Peek() >= 0
                    Dim line As String = sr.ReadLine
                    Dim strs() As String = line.Split(CChar(","))
                    Dim item As String = strs(3).Trim(CChar(""""))
                    If frame.ItemNo = item.PadLeft(2, CChar("0")) Or frame.ItemNo = item.PadLeft(3, CChar("0")) Then
                        Dim code As String = strs(4)
                        PadCodeItemFields(code, item)

                        If strs(4).Trim.StartsWith("FR") OrElse strs(4).Trim.StartsWith("SFR") Then
                            frame.FrontframeSize = New SizeF(CSng(strs(5)), CSng(strs(6)))
                        Else
                            Dim ps As New FramePartBaseClass(list.JobNumber & "." & item & "." & code)
                            ps.Width = CSng(strs(5))
                            ps.Length = CSng(strs(6))
                            ps.Thickness = CSng(strs(7))
                            If ps.Code.StartsWith("OP") Or ps.Code.StartsWith("CUT") Then
                                ps.Thickness = 0
                                ps.PartType = FrontFrameEventClasses.PartEdgeTypes.Opening
                                If Not IsNumeric(ps.Name.Substring(ps.Name.Length - 1)) Then
                                    ps.Name = ps.Name & frame.Openinglist.Count + 1
                                End If
                            ElseIf ps.Code.StartsWith("SC") AndAlso Not IsNumeric(ps.Name.Substring(ps.Name.Length - 1)) Then
                                ps.Name = ps.Name & frame.PartsList.Count + 1
                            ElseIf ps.Code.StartsWith("GIS") Then
                                ps.Name = ps.Name & frame.PartsList.Count + 1
                                If ps.Thickness = 0 Then
                                    ps.Thickness = Module1.JobThickness.BoardHeight
                                End If
                            End If

                            Dim opParms As OperationParms = Me._db.GetOperationParmsClass(strs(1))
                            Dim fp As New FramePart(frame, ps, HingePlacement.N, opParms)
                            fp.Grade = CChar(strs(1))
                            fp.TSComments = CStr(strs(9))
                            frame.AppendFramePart(fp)
                        End If
                    End If
                Loop
                Return frame
            End If
            Return Nothing
        Finally
            sr.Close()
        End Try
    End Function

    Public Sub ImportFromTemplateFile(ByRef frameList As FrameListClass, ByVal dataStoreDir As String)
        Dim dlg As New OpenFileDialog()
        dlg.DefaultExt = "csv"
        dlg.Filter = "Comma Separated Value Files (*.csv)|*.csv|All files (*.*)|*.*"
        dlg.InitialDirectory = My.Settings.CommonTemplateDirectory
        If dlg.ShowDialog = DialogResult.OK Then
            If dlg.CheckFileExists Then
                File.Copy(dlg.FileName, Module1.DataSourceDirectory & frameList.JobNumber & ".csv", True)
                Getlist(frameList, New StreamReader(Module1.DataSourceDirectory & frameList.JobNumber & ".csv"))
            End If
        End If
    End Sub

    Private Sub Getlist(ByRef framelist As FrameListClass, ByVal sr As StreamReader)
        Dim frame As FrontFrame = Nothing
        Try
            Do While sr.Peek() >= 0
                Dim line As String = sr.ReadLine
                Dim strs() As String = line.Split(CChar(","))
                Dim code As String = strs(4)
                Dim item As String = strs(3).Trim(CChar(""""))
                PadCodeItemFields(code, item)

                frame = framelist.GetFrameByName(framelist.JobNumber & "." & item)
                If IsNothing(frame) Then
                    frame = New FrontFrame(framelist.DataRef, framelist.JobNumber, item, strs(0))
                    frame.Code = strs(4)
                    framelist.AddFrame2List(frame)
                End If

                If strs(4).Trim.StartsWith("FR") OrElse strs(4).Trim.StartsWith("SFR") Then
                    frame.FrontframeSize = New SizeF(CSng(strs(5)), CSng(strs(6)))
                    frame.SaveToDb(True)
                Else
                    Dim ptBase As New FramePartBaseClass(framelist.JobNumber & "." & item & "." & code)
                    ptBase.Width = CSng(strs(5))
                    ptBase.Length = CSng(strs(6))
                    ptBase.Thickness = CSng(strs(7))
                    If ptBase.PartType = FrontFrameEventClasses.PartEdgeTypes.Opening Then
                        If Not IsNumeric(ptBase.Name.Substring(ptBase.Name.Length - 1)) Then
                            ptBase.Name = ptBase.Name & frame.Openinglist.Count + 1
                        End If
                    ElseIf (ptBase.PartType And FrontFrameEventClasses.PartEdgeTypes.Center) = ptBase.PartType AndAlso Not IsNumeric(ptBase.Name.Substring(ptBase.Name.Length - 1)) Then
                        ptBase.Name = ptBase.Name & frame.PartsList.Count + 1
                    ElseIf (ptBase.PartType And FrontFrameEventClasses.PartEdgeTypes.GlueInStrip) = ptBase.PartType Then
                        ptBase.Name = ptBase.Name & frame.PartsList.Count + 1
                        If ptBase.Thickness = 0 Then
                            ptBase.Thickness = Module1.JobThickness.BoardHeight
                        End If
                    End If

                    Dim opParms As OperationParms = Me._db.GetOperationParmsClass(strs(1))
                    Dim fp As New FramePart(frame, ptBase, HingePlacement.N, opParms)
                    fp.Grade = CChar(strs(1))
                    fp.TSComments = CStr(strs(9))
                    frame.AppendFramePart(fp)
                End If
            Loop
            'Return framelist

        Finally
            sr.Close()
        End Try
    End Sub

End Class
