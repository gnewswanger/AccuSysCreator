Imports System.Xml
Imports System.IO
Imports System.Diagnostics

''' <summary>
''' File: Module1.vb
''' Author: Galen Newswanger
''' This file contains global data and methods, available to any calls within the scope 
''' of the application.  
''' </summary>
''' <remarks>
''' The application's execution begins in Sub main().  The first call 
''' is to AutoUpdate.exe after a check to see that we are not running within the development 
''' environment. The AutoUpdate.exe checks the AutoUpdate.xml file to see if any files should 
''' be updated. If so AccuSysCreator2.exe is unloaded and the new files copied in, then 
''' AccuSysCreator2.exe is relaunched and AutoUpdate.exe is exited.  
''' The method Sub WriteToErrorLog is used for debugging purposes, to record status of problem 
''' areas at run time.
''' </remarks>
Module Module1

    Public CommandLine As String
    Public JobFrameStyle As FrameStyle
    Public JobHingeStyle As Hinge
    Public JobHingeRule As HingeRule
    Public JobThickness As Thickness
    Private _jobOperationParms As OperationParms
    Private _jobMachineTool As MachineTool
    Public LeftEdge As FrontFrameEventClasses.PartEdgeTypes = FrontFrameEventClasses.PartEdgeTypes.StileRight Or FrontFrameEventClasses.PartEdgeTypes.StileCenterL
    Public RightEdge As FrontFrameEventClasses.PartEdgeTypes = FrontFrameEventClasses.PartEdgeTypes.StileLeft Or FrontFrameEventClasses.PartEdgeTypes.StileCenterR
    Public TopEdge As FrontFrameEventClasses.PartEdgeTypes = FrontFrameEventClasses.PartEdgeTypes.BotRail Or FrontFrameEventClasses.PartEdgeTypes.RailCenterT
    Public BotEdge As FrontFrameEventClasses.PartEdgeTypes = FrontFrameEventClasses.PartEdgeTypes.TopRail Or FrontFrameEventClasses.PartEdgeTypes.RailCenterB

    Public Function JobOperationParms() As OperationParms
        If _jobOperationParms Is Nothing Then
            _jobOperationParms = New OperationParms(Module1.ParmFilename)
        End If
        Return _jobOperationParms
    End Function

    Public Function JobMachineTool() As MachineTool
        If _jobMachineTool Is Nothing Then
            _jobMachineTool = New MachineTool(Module1.ParmFilename)
        End If
        Return _jobMachineTool
    End Function

    Public Function SchedSearchStartOffset() As Integer
        'Return CInt(GetFromXml("/SetupParameters/General/ScheduleSearch[@name='startOffset']"))
        Return My.Settings.ScheduleSearchStartOffset
    End Function
    Public Function SchedSearchEndOffset() As Integer
        'Return CInt(GetFromXml("/SetupParameters/General/ScheduleSearch[@name='endOffset']"))
        Return My.Settings.ScheduleSearchEndOffset
    End Function

#Region "Directory Access"
    Public ParmFilename As String = My.Settings.ParmsXmlFile
    Public RemotePath As String = "\\Qccfile\accusysfiles\AutoUpdate"
    Public Function DataSourceDirectory() As String
        Return GetFromXml("/SetupParameters/Directories/Directory[@name='DataStore']")
    End Function
    Public Function RazorGageOutputDirectory() As String
        Return GetFromXml("/SetupParameters/Directories/Directory[@name='RazorDataOutput']")
    End Function
    Public Function ProgramOutputDirectory() As String
        Return GetFromXml("/SetupParameters/Directories/Directory[@name='DataOutput']")
    End Function
    Public Function DwfSourceDirectory() As String
        Return GetFromXml("/SetupParameters/Directories/Directory[@name='DwfStoreDir']")
    End Function
    Public Function DwfNotFoundFilePath() As String
        Return GetFromXml("/SetupParameters/Directories/Directory[@name='DwfNotFoundFile']")
    End Function
    Public Function SourceExecutableFilePath() As String
        Return GetFromXml("/SetupParameters/Directories/Directory[@name='DataExecutable']")
    End Function
    Public Function BarcodeDirectory() As String
        Return GetFromXml("/SetupParameters/Directories/Directory[@name='BarcodeDir']")
    End Function
    Public Function AutoUpdateDirectory() As String
        Return GetFromXml("/SetupParameters/Directories/Directory[@name='AutoUpdateDir']")
    End Function
    Public Function CadAspUrl() As String
        Return GetFromXml("/SetupParameters/Directories/Directory[@name='Cad.aspUrl']")
    End Function
    Public Function JobofficeAspUrl() As String
        Return GetFromXml("/SetupParameters/Directories/Directory[@name='Joboffice.aspUrl']")
    End Function
    Public Function eCatalogUrl() As String
        Return GetFromXml("/SetupParameters/Directories/Directory[@name='eCatalog']")
    End Function
    Private Function GetFromXml(ByVal path As String) As String
        If System.IO.File.Exists(My.Settings.ParmsXmlFile) Then
            Dim xd As XmlDocument = New XmlDocument
            Dim xtr As XmlTextReader
            xtr = New XmlTextReader(My.Settings.ParmsXmlFile)
            Try
                xd.Load(xtr)
                Return xd.SelectSingleNode(path).InnerText
            Catch ex As XmlException
                MessageBox.Show(My.Settings.ParmsXmlFile & vbCr & ex.Message & vbCr & ex.LineNumber & vbCr & ex.LinePosition)
            Finally
                xtr.Close()
            End Try
        End If
        Return Nothing
    End Function

#End Region

    Public Function InchesToPixels(ByVal inches As Single) As Int32
        Return CInt(inches * 72)
    End Function

    Public ReadOnly Property ComponentVersion() As String
        Get
            'Dim versionInfo As System.Version = Environment.Version
            Dim VersionInfo As Version = System.Reflection.Assembly.GetExecutingAssembly.GetName.Version
            Return VersionInfo.Major & "." & VersionInfo.Minor & "." & VersionInfo.Build & "." & VersionInfo.Revision
        End Get
    End Property

    Public Function GetVersionString() As String
        'Dim lastUpdate() As String = Application.ProductVersion.Split(CChar("."))
        Dim lastUpdate() As String = ComponentVersion.Split(CChar("."))
        Dim tm As DateTime = New DateTime(2000, 1, 1, 0, 0, 0)
        tm = tm.AddDays(CDbl(lastUpdate(2)))
        tm = tm.AddSeconds(CDbl(lastUpdate(3)))
        Dim retVal As String = "Version: " & ComponentVersion + "  (" + tm.ToString + ")"
        '        Dim retVal As String = "Version: " & Application.ProductVersion + "  (" + CStr(CDate(#1/1/2000#).AddDays(CDbl(lastUpdate(2)))) + " " _
        '                       + tm.ToShortTimeString + ")"
        Return retVal
    End Function

    Public Sub WriteToErrorLog(ByVal title As String, ByVal msg As String, ByVal excpt As String, Optional ByVal detail As String = "")
        If Not System.IO.Directory.Exists(Windows.Forms.Application.StartupPath & "\Log\") Then
            System.IO.Directory.CreateDirectory(Windows.Forms.Application.StartupPath & "\Log\")
        End If
        'check the file
        Dim fs As FileStream = New FileStream(Windows.Forms.Application.StartupPath & "\Log\Exceptions.log", FileMode.OpenOrCreate, FileAccess.ReadWrite)
        Dim s As StreamWriter = New StreamWriter(fs)
        s.Close()
        fs.Close()
        'log it
        Dim fs1 As FileStream = New FileStream(Windows.Forms.Application.StartupPath & "\Log\Exceptions.log", FileMode.Append, FileAccess.Write)
        Dim s1 As StreamWriter = New StreamWriter(fs1)
        s1.WriteLine(title & "," & msg & "," & excpt & "," & detail & "," & DateTime.Now.ToString)
        s1.Close()
        fs1.Close()
    End Sub

    <STAThread()> Sub Main()
        'Dim splashDlg As New frmSplash
        'splashDlg.Show()
        'If Not System.Diagnostics.Debugger.IsAttached() Then
        '  Dim MyAutoUpdate As New AutoUpdateClass
        '  If MyAutoUpdate.AutoUpdate(CommandLine, RemotePath) Then Exit Sub
        'End If
        Application.Run(New MainForm)
        'splashDlg.Close()
    End Sub

    'Build number will be equal to the number of days since January 1, 2000, local time. 
    'Revision number will be equal to the number of seconds since midnight local time, 
    'divided by 2 (86,400 seconds in a day 5 43,200 possible rev numbers per day). 

End Module

