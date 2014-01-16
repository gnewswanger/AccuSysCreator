Imports System.IO
Imports System.Xml

''' <summary>
''' <file>File: AutoUpdateClass.vb</file>
''' <author>Author: Galen Newswanger</author>
''' 
''' This class compares the current assembly's build number with the build number 
''' recorded in "AutoUpdate.xml" for a more recent update. If found it calls "autoupdate.exe"
''' to copy the files listed in "AutoUpdate.xml" to the program directory and relaunch 
''' AccuSysCreator2.exe.
''' </summary>
''' <remarks>This auto update process does not work with terminal server clients since they are remote
''' to the server and Windows will not allow them to modify or delete files located in ".\Program Files"
''' directory.</remarks>
Public Class AutoUpdateClass

    Public Function AutoUpdate(ByRef CommandLine As String, ByVal RemotePath As String) As Boolean
        Dim Key As String = "&**#@!" ' any unique sequence of characters
        ' the file with the update information
        Dim sfile As String = "AutoUpdate.xml"
        ' the Assembly name 
        Dim AssemblyName As String = _
                System.Reflection.Assembly.GetEntryAssembly.GetName.Name
        ' where are the files for a specific system
        Dim RemoteUri As String = RemotePath & "\" & AssemblyName & "\"
        ' clean up the command line getting rid of the key
        CommandLine = Replace(Microsoft.VisualBasic.Command(), Key, "")
        ' Verify if was called by the autoupdate
        If InStr(Microsoft.VisualBasic.Command(), Key) > 0 Then
            Try
                ' try to delete the AutoUpdate program, 
                ' since it is not needed anymore
                System.IO.File.Delete(Windows.Forms.Application.StartupPath & "\autoupdate.exe")
            Catch ex As Exception
            End Try
            ' return false means that no update is needed
            Return False
        Else
            ' was called by the user
            Dim ret As Boolean = False ' Default - no update needed
            Dim contents As New XmlDocument
            Dim xtr As XmlTextReader
            Try
                xtr = New XmlTextReader(Path.Combine(RemoteUri, sfile))
                contents.Load(xtr)
                xtr.Close()
                If contents.InnerText <> "" Then
                    Dim xnode As XmlNode
                    xnode = contents.SelectSingleNode("/AutoUpdateParms/Version[@name='ProdVersion']")
                    If xnode.InnerText.Substring(1) > Application.ProductVersion Then
                        Dim list As Xml.XmlNodeList = contents.SelectNodes("/AutoUpdateParms/FileList/File")
                        Dim fileListItem As String = String.Empty
                        For Each node As XmlNode In list
                            fileListItem = fileListItem & node.InnerText & "?"
                        Next
                        fileListItem = fileListItem.Trim(CChar("?"))
                        Dim arg As String = Application.ExecutablePath & "|" & _
                                    RemoteUri & "|" & fileListItem & "|" & Key & "|" & _
                                    Microsoft.VisualBasic.Command()
                        ' Download the auto update program to the application 
                        ' path, so you always have the last version runing
                        Dim fFile1 As New FileInfo(Path.Combine(RemotePath, "AutoUpdateExe.exe"))
                        If File.Exists(fFile1.FullName) Then
                            fFile1.CopyTo(Path.Combine(Windows.Forms.Application.StartupPath, "autoupdate.exe"), True)
                        End If
                        ' Call the auto update program with all the parameters
                        System.Diagnostics.Process.Start( _
                            Windows.Forms.Application.StartupPath & "\autoupdate.exe", arg)
                        ' return true - auto update in progress
                        ret = True
                    End If
                End If
            Catch ex As Exception
                ' if there is an error return true, 
                ' what means that the application
                ' should be closed
                ret = True
                ' something went wrong... 
                MsgBox("There was a problem runing the Auto Update." & vbCr & _
                    "Please Contact [contact info]" & vbCr & ex.Message, _
                    MsgBoxStyle.Critical)
            End Try
            Return ret
        End If
    End Function

End Class
