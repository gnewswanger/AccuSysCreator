Imports System.Xml

''' <summary>
''' <file>File: FrameStyle.vb</file>
''' <author>Author: Galen Newswanger</author>
''' 
''' This class encapsulates the attributes of a frame style, i.e. Flush Inset, Beaded Inset, etc. 
''' The data for each instance is stored in the SetupParms.xml file.
''' </summary>
''' <remarks>This class still exists extensively throughout the application
''' although plans are to replace it with StyleFramePart and StyleFrameEdge classes
''' which store instance data in Sql Server.</remarks>
Public Class FrameStyle
    Inherits Object

    Private _xmlFilename As String
    Private _fsName As String
    Private _fsDesc As String
    Public isMortised As Boolean
    Public isHaunched As Boolean
    Public haunchEndAdj As Single
    Public mortiseEndAdj As Single
    Public haunchDepth As Single
    Public mortiseDepth As Single
    Public mortiseShoulder As Single
    Public haunchOnTenon As Boolean
    Public tenonDepth As Single
    Public pilotCenterAdj As Single
    Public singleTenonWidthMax As Single
    Private _isActiveStyle As Boolean

    Public ReadOnly Property Name() As String
        Get
            Return Me._fsName
        End Get
    End Property

    Public Property Description() As String
        Get
            Return Me._fsDesc
        End Get
        Set(ByVal Value As String)
            Me._fsDesc = Value
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

    Public Sub New(ByVal styleName As String, ByVal filename As String)
        MyBase.New()
        Me._xmlFilename = filename
        Me._fsName = styleName
        Me._isActiveStyle = True
        ReadFromXml()
    End Sub

    Private Sub ReadFromXml()
        If System.IO.File.Exists(Me._xmlFilename) Then
            Dim xd As XmlDocument = New XmlDocument
            Dim xtr As XmlTextReader = New XmlTextReader(Me._xmlFilename)
            Try
                xd.Load(xtr)
                Dim xnodRoot As Xml.XmlNode
                xnodRoot = xd.DocumentElement
                Dim xnodWorking As XmlNode
                If xnodRoot.HasChildNodes Then
                    xnodWorking = xnodRoot.FirstChild
                    While Not IsNothing(xnodWorking)
                        If xnodWorking.Name.Equals("FrameStyles") Then
                            InitializeFrameStyleData(xnodWorking)
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

    Private Sub InitializeFrameStyleData(ByVal xnod As XmlNode)
        Dim parmNode As XmlNode
        For i As Integer = 0 To xnod.ChildNodes.Count - 1
            If xnod.ChildNodes(i).Attributes("name").Value = Me._fsName Then
                Me._fsDesc = xnod.ChildNodes(i).Attributes("description").Value
                Me._isActiveStyle = CBool(xnod.ChildNodes(i).Attributes("active").Value)
                parmNode = xnod.ChildNodes(i).Item("FParm")
                While Not IsNothing(parmNode)
                    Select Case parmNode.Attributes("name").Value
                        Case "IsMortised"
                            isMortised = CBool(parmNode.InnerText)
                        Case "IsHaunched"
                            isHaunched = CBool(parmNode.InnerText)
                        Case "HaunchEndAdj"
                            haunchEndAdj = CDec(parmNode.InnerText)
                        Case "MortiseEndAdj"
                            mortiseEndAdj = CDec(parmNode.InnerText)
                        Case "HaunchDepth"
                            haunchDepth = CDec(parmNode.InnerText)
                        Case "MortiseDepth"
                            mortiseDepth = CDec(parmNode.InnerText)
                        Case "MortiseShoulder"
                            mortiseShoulder = CDec(parmNode.InnerText)
                        Case "HaunchOnTenon"
                            haunchOnTenon = CBool(parmNode.InnerText)
                        Case "TenonDepth"
                            tenonDepth = CDec(parmNode.InnerText)
                        Case "PilotCenterAdj"
                            pilotCenterAdj = CDec(parmNode.InnerText)
                        Case "SingleTenonMax"
                            singleTenonWidthMax = CDec(parmNode.InnerText)
                    End Select
                    parmNode = parmNode.NextSibling
                End While
            End If
        Next
    End Sub

    Public Sub SaveToXml()
        WriteUpdateToXml()
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
        xnodRoot = xd.DocumentElement.Item("FrameStyles")

        Dim xElem As XmlElement = xd.CreateElement("FStyle")
        xElem.SetAttribute("name", Me._fsName)
        xElem.SetAttribute("description", Me._fsDesc)
        xElem.SetAttribute("active", Me._isActiveStyle.ToString)

        Dim child As XmlElement = xd.CreateElement("FParm")
        child.SetAttribute("name", "IsMortised")
        child.InnerText = isMortised.GetHashCode.ToString
        xElem.AppendChild(child)

        child = xd.CreateElement("FParm")
        child.SetAttribute("name", "IsHaunched")
        child.InnerText = isHaunched.GetHashCode.ToString
        xElem.AppendChild(child)

        child = xd.CreateElement("FParm")
        child.SetAttribute("name", "HaunchEndAdj")
        child.InnerText = haunchEndAdj.ToString
        xElem.AppendChild(child)

        child = xd.CreateElement("FParm")
        child.SetAttribute("name", "MortiseEndAdj")
        child.InnerText = mortiseEndAdj.ToString
        xElem.AppendChild(child)

        child = xd.CreateElement("FParm")
        child.SetAttribute("name", "HaunchDepth")
        child.InnerText = haunchDepth.ToString
        xElem.AppendChild(child)

        child = xd.CreateElement("FParm")
        child.SetAttribute("name", "MortiseDepth")
        child.InnerText = mortiseDepth.ToString
        xElem.AppendChild(child)

        child = xd.CreateElement("FParm")
        child.SetAttribute("name", "MortiseShoulder")
        child.InnerText = mortiseShoulder.ToString
        xElem.AppendChild(child)

        child = xd.CreateElement("FParm")
        child.SetAttribute("name", "HaunchOnTenon")
        child.InnerText = haunchOnTenon.GetHashCode.ToString
        xElem.AppendChild(child)

        child = xd.CreateElement("FParm")
        child.SetAttribute("name", "TenonDepth")
        child.InnerText = tenonDepth.ToString
        xElem.AppendChild(child)

        child = xd.CreateElement("FParm")
        child.SetAttribute("name", "PilotCenterAdj")
        child.InnerText = pilotCenterAdj.ToString
        xElem.AppendChild(child)

        xnodRoot.InsertAfter(xElem, xnodRoot.LastChild)
        xd.Save(Me._xmlFilename)

    End Sub

    Private Sub WriteUpdateToXml()
        Dim found As Boolean = False
        Dim xd As XmlDocument = Me.GetXmlDocument
        Dim xnodRoot As Xml.XmlNode
        xnodRoot = xd.DocumentElement.Item("FrameStyles")
        Dim xnodWorking As XmlNode
        If xnodRoot.HasChildNodes Then
            For i As Integer = 0 To xnodRoot.ChildNodes.Count - 1
                If xnodRoot.ChildNodes(i).Attributes("name").Value = Me._fsName Then
                    xnodRoot.ChildNodes(i).Attributes("description").Value = Me._fsDesc
                    xnodRoot.ChildNodes(i).Attributes("active").Value = Me.ActiveStyle.ToString
                    xnodWorking = xnodRoot.ChildNodes(i).FirstChild
                    found = True
                    While Not IsNothing(xnodWorking)
                        Select Case xnodWorking.Attributes("name").Value
                            Case "IsMortised"
                                xnodWorking.InnerText = isMortised.GetHashCode.ToString
                            Case "IsHaunched"
                                xnodWorking.InnerText = isHaunched.GetHashCode.ToString
                            Case "HaunchEndAdj"
                                xnodWorking.InnerText = haunchEndAdj.ToString
                            Case "MortiseEndAdj"
                                xnodWorking.InnerText = mortiseEndAdj.ToString
                            Case "HaunchDepth"
                                xnodWorking.InnerText = haunchDepth.ToString
                            Case "MortiseDepth"
                                xnodWorking.InnerText = mortiseDepth.ToString
                            Case "MortiseShoulder"
                                xnodWorking.InnerText = mortiseShoulder.ToString
                            Case "HaunchOnTenon"
                                xnodWorking.InnerText = haunchOnTenon.GetHashCode.ToString
                            Case "TenonDepth"
                                xnodWorking.InnerText = tenonDepth.ToString
                            Case "PilotCenterAdj"
                                xnodWorking.InnerText = pilotCenterAdj.ToString
                            Case "SingleTenonMax"
                                xnodWorking.InnerText = singleTenonWidthMax.ToString
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
        '        End If
    End Sub

    Public Sub DeleteRuleFromXml()
        Dim xd As XmlDocument = Me.GetXmlDocument
        If Not xd Is Nothing Then
            Dim node As Xml.XmlNode
            node = xd.SelectSingleNode("SetupParameters/FrameStyles/FStyle[@name='" + Me._fsName + "']")
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
