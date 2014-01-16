Imports System.Xml

''' <summary>
''' <file>File: MachineTool.vb</file>
''' <author>Author: Galen Newswanger</author>
''' 
''' This class contains the attributes of the actual machine tool, i.e. mortise bit, haunch bit, 
''' drill bit, used on the MTH machine. 
''' </summary>
''' <remarks></remarks>
Public Class MachineTool
    Inherits Object

    Private _xmlFilename As String
    Private _haunchBottom As Single
    Private _mToolDia As Single
    Private _pilotDia As Single

    Public Property HaunchToolBottom() As Single
        Get
            Return Me._haunchBottom
        End Get
        Set(value As Single)
            Me._haunchBottom = value
        End Set
    End Property
    Public Property MortiseToolDiameter() As Single
        Get
            Return Me._mToolDia
        End Get
        Set(value As Single)
            Me._mToolDia = value
        End Set
    End Property

    Public Property PilotToolDiameter() As Single
        Get
            Return Me._pilotDia
        End Get
        Set(value As Single)
            Me._pilotDia = value
        End Set
    End Property

    Public Sub New(ByVal xmlname As String)
        MyBase.New()
        Me._xmlFilename = xmlname
        ReadFromXml()
    End Sub

    Private Sub ReadFromXml()
        If System.IO.File.Exists(Me._xmlFilename) Then
            Dim xd As XmlDocument = New XmlDocument
            Dim xtr As XmlTextReader = New XmlTextReader(Me._xmlFilename)
            Try
                xd.Load(xtr)
                Me._haunchBottom = CSng(xd.SelectSingleNode("/SetupParameters/ToolGeometry/MachineTool[@name='HaunchTool']/ToolParm[@name='HaunchToolBottom']").InnerText)
                Me._mToolDia = CSng(xd.SelectSingleNode("/SetupParameters/ToolGeometry/MachineTool[@name='MortiseTool']/ToolParm[@name='MToolDiameter']").InnerText)
                Me._pilotDia = CSng(xd.SelectSingleNode("/SetupParameters/ToolGeometry/MachineTool[@name='HingeDrill']/ToolParm[@name='PilotDia']").InnerText)
            Catch ex As XmlException
                MessageBox.Show(Me._xmlFilename & vbCr & ex.Message & vbCr & ex.LineNumber & vbCr & ex.LinePosition)
            Finally
                xtr.Close()
            End Try
        End If
    End Sub

    Public Sub SaveToXml()
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
            xd.SelectSingleNode("/SetupParameters/ToolGeometry/MachineTool[@name='HaunchTool']/ToolParm[@name='HaunchToolBottom']").InnerText = Me._haunchBottom.ToString
            xd.SelectSingleNode("/SetupParameters/ToolGeometry/MachineTool[@name='MortiseTool']/ToolParm[@name='MToolDiameter']").InnerText = Me._mToolDia.ToString
            xd.SelectSingleNode("/SetupParameters/ToolGeometry/MachineTool[@name='HingeDrill']/ToolParm[@name='PilotDia']").InnerText = Me._pilotDia.ToString
            xd.Save(Me._xmlFilename)
        End If
    End Sub

End Class
