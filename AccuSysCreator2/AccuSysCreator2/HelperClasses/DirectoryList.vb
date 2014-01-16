Imports System.Xml

''' <summary>
''' <file>File: DirectoryList.vb</file>
''' <author>Author: Galen Newswanger</author>
''' 
''' This class is a container for the various directories used by the application.  
''' This data is stored in the shared file, "\\qccfile\accusysfiles\common\SetupParms.xml"
''' </summary>
''' <remarks>
''' This class currently is used by the ParmSetup form class.
''' </remarks>
Public Class DirectoryList
    Inherits Object

    Private _xmlFilename As String
    Public dataStore As String
    Public dataOutput As String
    Public dataExecutable As String
    Public accuSysLibComputer As String
    Public accuSysLib As String
    Public accuSysTenonLib As String

    Public Sub New(ByVal xmlname As String)
        MyBase.New()
        Me._xmlFilename = xmlname
        ReadFromXml()
    End Sub

    Public Sub New(ByVal dList As DirectoryList)
        MyBase.New()
        Me._xmlFilename = dList._xmlFilename
        Me.dataStore = dList.dataStore
        Me.dataOutput = dList.dataOutput
        Me.dataExecutable = dList.dataExecutable
        Me.accuSysLibComputer = dList.accuSysLibComputer
        Me.accuSysLib = dList.accuSysLib
        Me.accuSysTenonLib = dList.accuSysTenonLib
    End Sub

    Private Sub ReadFromXml()
        If System.IO.File.Exists(Me._xmlFilename) Then
            Dim xd As XmlDocument = New XmlDocument
            Dim xtr As XmlTextReader = New XmlTextReader(Me._xmlFilename)
            Try
                xd.Load(xtr)
                dataStore = xd.SelectSingleNode("/SetupParameters/Directories/Directory[@name='DataStore']").InnerText
                dataOutput = xd.SelectSingleNode("/SetupParameters/Directories/Directory[@name='DataOutput']").InnerText
                dataExecutable = xd.SelectSingleNode("/SetupParameters/Directories/Directory[@name='DataExecutable']").InnerText
                accuSysLibComputer = xd.SelectSingleNode("/SetupParameters/Directories/Directory[@name='AccuSysLibComputer']").InnerText
                accuSysLib = xd.SelectSingleNode("/SetupParameters/Directories/Directory[@name='AccuSysLib']").InnerText
                accuSysTenonLib = xd.SelectSingleNode("/SetupParameters/Directories/Directory[@name='AccuSysTenonLib']").InnerText
            Catch ex As XmlException
                MessageBox.Show(Me._xmlFilename & vbCr & ex.Message & vbCr & ex.LineNumber & vbCr & ex.LinePosition)
            Finally
                xtr.Close()
            End Try
        End If

    End Sub

    Private Sub WriteToXml()
        If System.IO.File.Exists(Me._xmlFilename) Then
            Dim xd As XmlDocument = New XmlDocument
            Dim xtr As XmlTextReader = New XmlTextReader(Me._xmlFilename)
            Try
                xd.Load(xtr)
            Catch ex As XmlException
                MessageBox.Show(Me._xmlFilename & vbCr & ex.Message & vbCr & ex.LineNumber & vbCr & ex.LinePosition)
            Finally
                xtr.Close()
            End Try
            xd.SelectSingleNode("/SetupParameters/Directories/Directory[@name=DataStore]").InnerText = dataStore
            xd.SelectSingleNode("/SetupParameters/Directories/Directory[@name=DataOutput]").InnerText = dataOutput
            xd.SelectSingleNode("/SetupParameters/Directories/Directory[@name=DataExecutable]").InnerText = dataExecutable
            xd.SelectSingleNode("/SetupParameters/Directories/Directory[@name=AccuSysLibComputer]").InnerText = accuSysLibComputer
            xd.SelectSingleNode("/SetupParameters/Directories/Directory[@name=AccuSysLib]").InnerText = accuSysLib
            xd.SelectSingleNode("/SetupParameters/Directories/Directory[@name=AccuSysTenonLib]").InnerText = accuSysTenonLib
            xd.Save(Me._xmlFilename)
        End If
    End Sub

End Class
