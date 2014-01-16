Imports System.Xml

''' <summary>
''' <file>File: Hinge.vb</file>
''' <author>Author: Galen Newswanger</author>
''' 
''' This class encapsulates the attributes of a hinge style, i.e. 852, 504, etc. 
''' The data for each instance is stored in the SetupParms.xml file.
''' </summary>
''' <remarks>This class still exists extensively throughout the application
''' although plans are to replace it with StyleHinge class which store instance 
''' data in Sql Server.</remarks>
Public Class Hinge
    Inherits Object

    Private _xmlFilename As String
    Private _hingeName As String
    Private _hingeDesc As String
    Private _isActiveStyle As Boolean
    Public isMortised As Boolean
    Public mortiseWidth As Single
    Public mortiseDepth As Single
    Public isPredrilled As Boolean
    Public distBetweenOuter As Single
    Public vCenterOuter As Single
    Public vCenterInner As Single
    Public pilotDepth As Single
    Public pilotDia As Single
    Public placementRule As String

    Public ReadOnly Property Name() As String
        Get
            Return Me._hingeName
        End Get
    End Property

    Public Property Description() As String
        Get
            Return Me._hingeDesc
        End Get
        Set(ByVal Value As String)
            Me._hingeDesc = Value
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

    Public Sub New(ByVal hnge As Hinge)
        MyBase.New()
        Me._xmlFilename = hnge._xmlFilename
        Me._hingeName = hnge._hingeName
        Me._hingeDesc = hnge._hingeDesc
        Me.isMortised = hnge.isMortised
        Me.mortiseWidth = hnge.mortiseWidth
        Me.mortiseDepth = hnge.mortiseDepth
        Me.isPredrilled = hnge.isPredrilled
        Me.distBetweenOuter = hnge.distBetweenOuter
        Me.vCenterOuter = hnge.vCenterOuter
        Me.vCenterInner = hnge.vCenterInner
        Me.pilotDepth = hnge.pilotDepth
        Me.pilotDia = hnge.pilotDia
        Me.placementRule = hnge.placementRule
        Me._isActiveStyle = hnge.ActiveStyle
    End Sub

    Public Sub New(ByVal desc As String, ByVal xmlname As String)
        MyBase.New()
        Me._xmlFilename = xmlname
        Me._hingeName = desc
        Me._isActiveStyle = True
        ReadFromXml()
    End Sub

    Public Sub SaveToXml()
        WriteUpdateToXml()
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
                        If xnodWorking.Name.Equals("HingeType") Then
                            InitializeHingeData(xnodWorking)
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

    Private Sub InitializeHingeData(ByVal xnod As XmlNode)
        Dim parmNode As XmlNode
        For i As Integer = 0 To xnod.ChildNodes.Count - 1
            If xnod.ChildNodes(i).Attributes("name").Value = Me._hingeName Then
                Me._hingeDesc = xnod.ChildNodes(i).Attributes("description").Value
                Me._isActiveStyle = CBool(xnod.ChildNodes(i).Attributes("active").Value)
                parmNode = xnod.ChildNodes(i).Item("HingeParm")
                While Not IsNothing(parmNode)
                    Select Case parmNode.Attributes("name").Value
                        Case "IsMortised"
                            isMortised = CBool(parmNode.InnerText)
                        Case "MortiseWidth"
                            mortiseWidth = CDec(parmNode.InnerText)
                        Case "MortiseDepth"
                            mortiseDepth = CDec(parmNode.InnerText)
                        Case "IsPredrilled"
                            isPredrilled = CBool(parmNode.InnerText)
                        Case "BetweenPilots"
                            distBetweenOuter = CDec(parmNode.InnerText)
                        Case "PilotCenterline"
                            vCenterOuter = CDec(parmNode.InnerText)
                        Case "PilotDepth"
                            pilotDepth = CDec(parmNode.InnerText)
                        Case "PilotDia"
                            pilotDia = CDec(parmNode.InnerText)
                        Case "HingeRule"
                            placementRule = parmNode.InnerText
                        Case "active"
                            Me._isActiveStyle = CBool(parmNode.InnerText)
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
        xnodRoot = xd.DocumentElement.Item("HingeType")

        Dim xElem As XmlElement = xd.CreateElement("Hinge")
        xElem.SetAttribute("name", Me._hingeName)
        xElem.SetAttribute("description", Me._hingeDesc)
        xElem.SetAttribute("active", Me.ActiveStyle.ToString)

        Dim child As XmlElement = xd.CreateElement("HingeParm")
        child.SetAttribute("name", "IsMortised")
        child.InnerText = isMortised.GetHashCode.ToString
        xElem.AppendChild(child)

        child = xd.CreateElement("HingeParm")
        child.SetAttribute("name", "MortiseWidth")
        child.InnerText = mortiseWidth.ToString
        xElem.AppendChild(child)

        child = xd.CreateElement("HingeParm")
        child.SetAttribute("name", "MortiseDepth")
        child.InnerText = mortiseDepth.ToString
        xElem.AppendChild(child)

        child = xd.CreateElement("HingeParm")
        child.SetAttribute("name", "IsPredrilled")
        child.InnerText = isPredrilled.GetHashCode.ToString
        xElem.AppendChild(child)

        child = xd.CreateElement("HingeParm")
        child.SetAttribute("name", "BetweenPilots")
        child.InnerText = distBetweenOuter.ToString
        xElem.AppendChild(child)

        child = xd.CreateElement("HingeParm")
        child.SetAttribute("name", "PilotCenterline")
        child.InnerText = vCenterOuter.ToString
        xElem.AppendChild(child)

        child = xd.CreateElement("HingeParm")
        child.SetAttribute("name", "PilotDepth")
        child.InnerText = pilotDepth.ToString
        xElem.AppendChild(child)

        child = xd.CreateElement("HingeParm")
        child.SetAttribute("name", "PilotDia")
        child.InnerText = pilotDia.ToString
        xElem.AppendChild(child)

        child = xd.CreateElement("HingeParm")
        child.SetAttribute("name", "HingeRule")
        child.InnerText = placementRule
        xElem.AppendChild(child)

        xnodRoot.InsertAfter(xElem, xnodRoot.LastChild)
        xd.Save(Me._xmlFilename)

    End Sub

    Private Sub WriteUpdateToXml()
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
            Dim found As Boolean = False
            Dim xnodRoot As XmlNode
            xnodRoot = xd.DocumentElement.Item("HingeType")
            Dim xnodWorking As XmlNode
            If xnodRoot.HasChildNodes Then
                For i As Integer = 0 To xnodRoot.ChildNodes.Count - 1
                    If xnodRoot.ChildNodes(i).Attributes("name").Value = Me._hingeName Then
                        xnodRoot.ChildNodes(i).Attributes("description").Value = Me._hingeDesc
                        xnodRoot.ChildNodes(i).Attributes("active").Value = Me.ActiveStyle.ToString
                        xnodWorking = xnodRoot.ChildNodes(i).FirstChild
                        found = True
                        While Not IsNothing(xnodWorking)
                            Select Case xnodWorking.Attributes("name").Value
                                Case "IsMortised"
                                    xnodWorking.InnerText = isMortised.GetHashCode.ToString
                                Case "MortiseWidth"
                                    xnodWorking.InnerText = mortiseWidth.ToString
                                Case "MortiseDepth"
                                    xnodWorking.InnerText = mortiseDepth.ToString
                                Case "IsPredrilled"
                                    xnodWorking.InnerText = isPredrilled.GetHashCode.ToString
                                Case "BetweenPilots"
                                    xnodWorking.InnerText = distBetweenOuter.ToString
                                Case "PilotCenterline"
                                    xnodWorking.InnerText = vCenterOuter.ToString
                                Case "PilotDepth"
                                    xnodWorking.InnerText = pilotDepth.ToString
                                Case "PilotDia"
                                    xnodWorking.InnerText = pilotDia.ToString
                                Case "HingeRule"
                                    xnodWorking.InnerText = placementRule
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
            node = xd.SelectSingleNode("SetupParameters/HingeType/Hinge[@name='" + Me._hingeName + "']")
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
