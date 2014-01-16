Imports System.Xml

''' <summary>
''' <file>File: Thickness.vb</file>
''' <author>Author: Galen Newswanger</author>
''' 
''' This class encapsulates the attributes of the thickness style for the frame. 
''' The data for each instance is stored in the SetupParms.xml file.
''' </summary>
''' <remarks>This class still exists extensively throughout the application
''' although plans are to replace it with StyleThickness class which store instance 
''' data in Sql Server.</remarks>
Public Class Thickness
    Inherits Object

    Private _xmlFilename As String
    Private _tkName As String
    Private _tkDesc As String
    Private _boardHeight As Single
    Private _isActiveStyle As Boolean
    Public toolLine As Single
    Public routeTopWidth As Single
    Public routeMiddleWidth As Single
    Public routeBottomWidth As Single

    Public ReadOnly Property Name() As String
        Get
            Return Me._tkName
        End Get
    End Property

    Public Property Description() As String
        Get
            Return Me._tkDesc
        End Get
        Set(ByVal Value As String)
            Me._tkDesc = Value
        End Set
    End Property

    Public Property BoardHeight() As Single
        Get
            Return Me._boardHeight
        End Get
        Set(ByVal Value As Single)
            Me._boardHeight = Value
        End Set
    End Property

    Public Property ActiveStyle() As Boolean
        Get
            Return Me._isActiveStyle
        End Get
        Set(ByVal value As Boolean)
            Me._isActiveStyle = value
        End Set
    End Property

    Public Sub New(ByVal name As String, ByVal xmlname As String)
        MyBase.New()
        Me._tkName = name
        Me._xmlFilename = xmlname
        Me._isActiveStyle = True
        ReadFromXml()
    End Sub

    Public Sub SaveToXml()
        WriteUpdateToXml()
    End Sub

    Private Sub ReadFromXml()
        If System.IO.File.Exists(Me._xmlFilename) Then
            Dim xd As XmlDocument = New XmlDocument
            Dim xtr As XmlTextReader = Nothing
            Try
                xtr = New XmlTextReader(Me._xmlFilename)
                xd.Load(xtr)
                Dim xnodRoot As Xml.XmlNode
                xnodRoot = xd.DocumentElement
                Dim xnodWorking As XmlNode
                If xnodRoot.HasChildNodes Then
                    xnodWorking = xnodRoot.FirstChild
                    While Not IsNothing(xnodWorking)
                        If xnodWorking.Name.Equals("ThicknessParms") Then
                            InitializeThicknessData(xnodWorking)
                        End If
                        xnodWorking = xnodWorking.NextSibling
                    End While
                End If
            Catch ex As XmlException
                MessageBox.Show(Me._xmlFilename & vbCr & ex.Message & vbCr & ex.LineNumber & vbCr & ex.LinePosition)
            Finally
                xtr.Close()
            End Try
        End If

    End Sub

    Private Sub InitializeThicknessData(ByVal xnod As XmlNode)
        Dim parmNode As XmlNode
        For i As Integer = 0 To xnod.ChildNodes.Count - 1
            If xnod.ChildNodes(i).Attributes("name").Value = Me._tkName Then
                Me._tkDesc = xnod.ChildNodes(i).Attributes("description").Value
                Me._isActiveStyle = CBool(xnod.ChildNodes(i).Attributes("active").Value)
                parmNode = xnod.ChildNodes(i).Item("ThkParm")
                While Not IsNothing(parmNode)
                    Select Case parmNode.Attributes("name").Value
                        Case "BdHeight"
                            Me._boardHeight = CDec(parmNode.InnerText)
                        Case "ToolLine"
                            toolLine = CDec(parmNode.InnerText)
                        Case "RouteTopWidth"
                            routeTopWidth = CDec(parmNode.InnerText)
                        Case "RouteMiddleWidth"
                            routeMiddleWidth = CDec(parmNode.InnerText)
                        Case "RouteBottomWidth"
                            routeBottomWidth = CDec(parmNode.InnerText)
                    End Select
                    parmNode = parmNode.NextSibling
                End While
            End If
        Next
    End Sub

    Private Sub WriteNewRuleToXml()
        Dim xtr As XmlTextReader = New XmlTextReader(Me._xmlFilename)
        Dim xd As XmlDocument = New XmlDocument
        Try
            xd.Load(xtr)
        Finally
            xtr.Close()
        End Try

        Dim xnodRoot As Xml.XmlNode
        xnodRoot = xd.DocumentElement.Item("ThicknessParms")

        Dim xElem As XmlElement = xd.CreateElement("Thickness")
        xElem.SetAttribute("name", Me._tkName)
        xElem.SetAttribute("description", Me._tkDesc)
        xElem.SetAttribute("active", Me.ActiveStyle.ToString)

        Dim child As XmlElement = xd.CreateElement("ThkParm")
        child.SetAttribute("name", "BdHeight")
        child.InnerText = Me._boardHeight.ToString
        xElem.AppendChild(child)

        child = xd.CreateElement("ThkParm")
        child.SetAttribute("name", "ToolLine")
        child.InnerText = toolLine.ToString
        xElem.AppendChild(child)

        child = xd.CreateElement("ThkParm")
        child.SetAttribute("name", "RouteTopWidth")
        child.InnerText = routeTopWidth.ToString
        xElem.AppendChild(child)

        child = xd.CreateElement("ThkParm")
        child.SetAttribute("name", "RouteMiddleWidth")
        child.InnerText = routeMiddleWidth.ToString
        xElem.AppendChild(child)

        child = xd.CreateElement("ThkParm")
        child.SetAttribute("name", "RouteBottomWidth")
        child.InnerText = routeBottomWidth.ToString
        xElem.AppendChild(child)

        xnodRoot.InsertAfter(xElem, xnodRoot.LastChild)
        xd.Save(Me._xmlFilename)

    End Sub

    Private Sub WriteUpdateToXml()
        If System.IO.File.Exists(Me._xmlFilename) Then
            Dim xd As XmlDocument = New XmlDocument
            Dim xtr As XmlTextReader = New XmlTextReader(Me._xmlFilename)
            Dim found As Boolean = False
            Try
                xd.Load(xtr)
            Catch ex As XmlException
                MessageBox.Show(Me._xmlFilename & vbCr & ex.Message & vbCr & ex.LineNumber & vbCr & ex.LinePosition)
            Finally
                xtr.Close()
            End Try
            Dim xnodRoot As Xml.XmlNode
            xnodRoot = xd.DocumentElement.Item("ThicknessParms")
            Dim xnodWorking As XmlNode
            If xnodRoot.HasChildNodes Then
                For i As Integer = 0 To xnodRoot.ChildNodes.Count - 1
                    If xnodRoot.ChildNodes(i).Attributes("name").Value = Me._tkName Then
                        xnodRoot.ChildNodes(i).Attributes("description").Value = Me._tkDesc
                        xnodRoot.ChildNodes(i).Attributes("active").Value = Me.ActiveStyle.ToString
                        xnodWorking = xnodRoot.ChildNodes(i).FirstChild
                        found = True
                        While Not IsNothing(xnodWorking)
                            Select Case xnodWorking.Attributes("name").Value
                                Case "BdHeight"
                                    xnodWorking.InnerText = Me._boardHeight.ToString
                                Case "ToolLine"
                                    xnodWorking.InnerText = toolLine.ToString
                                Case "RouteTopWidth"
                                    xnodWorking.InnerText = routeTopWidth.ToString
                                Case "RouteMiddleWidth"
                                    xnodWorking.InnerText = routeMiddleWidth.ToString
                                Case "RouteBottomWidth"
                                    xnodWorking.InnerText = routeBottomWidth.ToString
                            End Select
                            xnodWorking = xnodWorking.NextSibling
                        End While
                    End If
                Next
            End If
            If found = True Then
                xd.Save(Me._xmlFilename)
            ElseIf found = False Then
                WriteNewRuleToXml()
            End If
        End If
    End Sub

    Public Sub DeleteRuleFromXml()
        Dim xd As XmlDocument = Me.GetXmlDocument
        If xd IsNot Nothing Then
            Dim node As Xml.XmlNode
            node = xd.SelectSingleNode("SetupParameters/ThicknessParms/Thickness[@name='" + Me._tkName + "']")
            If node IsNot Nothing Then
                node.ParentNode.RemoveChild(node)
                xd.Save(Me._xmlFilename)
            End If
        End If
    End Sub

    Private Function GetXmlDocument() As XmlDocument
        If System.IO.File.Exists(Me._xmlFilename) Then
            Dim xd As XmlDocument = New XmlDocument
            Dim xtr As XmlTextReader = New XmlTextReader(Me._xmlFilename)
            Dim found As Boolean = False
            Try
                xd.Load(xtr)
            Catch ex As XmlException
                MessageBox.Show(Me._xmlFilename & vbCr & ex.Message & vbCr & ex.LineNumber & vbCr & ex.LinePosition)
            Finally
                xtr.Close()
            End Try
            Return xd
        End If
        Return Nothing
    End Function

End Class
