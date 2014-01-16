Imports System.IO

''' <summary>
''' <file>OperationsFile.vb</file>
''' <author>Galen Newswanger</author>
''' 
''' This class reads operations from an "acc" file or returns a list of operations
''' from the file's contents (lines) passed in as a List(of String).
''' </summary>
''' <remarks></remarks>
Public Class OperationsFile

    Public Function UpdateOperationsList(ByVal lines As List(Of String)) As List(Of String())
        If lines.Count > 0 AndAlso Not lines.First = String.Empty Then
            Dim opList As New List(Of String())
            Dim isOperation As Boolean
            If lines(0).StartsWith("HPMHTP") Then
                isOperation = False
            ElseIf IsNumeric(lines(0)(0)) Then
                isOperation = True
            End If
            opList.Clear()
            For i As Integer = 0 To lines.Count - 1
                Dim strs() As String = lines(i).Split(vbTab)
                If Not IsNothing(strs) AndAlso strs(0) = "Num" Then
                    isOperation = True
                    Continue For
                ElseIf Not IsNothing(strs) AndAlso Not IsNumeric(strs(0)) Then
                    isOperation = False
                End If
                If isOperation Then
                    opList.Add(strs)
                End If
            Next
            Return opList
        Else
            Return Nothing
        End If
    End Function

    Public Function ReadOperationsFile(ByVal filename As String) As String
        Dim fileText As String = String.Empty
        Dim sr As New StreamReader(filename, False)
        Try
            Do While sr.Peek >= 0
                Dim line As String = sr.ReadLine
                fileText += line & vbCr
            Loop
            Return fileText
        Catch ex As Exception
            MsgBox("ReadOperationsFile failed. " & filename & "; " & ex.Message)
        Finally
            sr.Close()
        End Try
        Return Nothing
    End Function

End Class
