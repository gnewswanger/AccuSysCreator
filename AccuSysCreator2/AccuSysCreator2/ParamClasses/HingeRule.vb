Imports System.Xml

''' <summary>
''' <file>File: HingeRule.vb</file>
''' <author>Author: Galen Newswanger</author>
''' 
''' This class contains the rules for placement of hinges, i.e. the number of
''' hinges, location of pilot holes,etc. The data for each instance is stored 
''' in the SetupParms.xml file.
''' </summary>
''' <remarks>This class still exists extensively throughout the application
''' although plans are to replace it with HingeStyleRule class which store instance 
''' data in Sql Server.</remarks>
Public Class HingeRule
    Inherits Object

    Private _xmlFilename As String
    Private _ruleName As String
    Private _isActiveStyle As Boolean
    Public mortiseOffset As Single
    Public smallOpgMortOffset As Single
    Public minHght42Hinges As Single
    Public hghtRange42Lower As Single
    Public hghtRange42Upper As Single
    Public hghtRange43Upper As Single
    Public hghtRange44Upper As Single
    Public overrideNumbeHinges As Integer = 0

    Public ReadOnly Property Name() As String
        Get
            Return Me._ruleName
        End Get
    End Property

    Public Property ActiveStyle() As Boolean
        Get
            Return Me._isActiveStyle
        End Get
        Set(ByVal value As Boolean)
            Me._isActiveStyle = value
        End Set
    End Property

    Public Sub New(ByVal hr As HingeRule)
        MyBase.New()
        Me._xmlFilename = hr._xmlFilename
        Me._ruleName = hr._ruleName
        Me.mortiseOffset = hr.mortiseOffset
        Me.smallOpgMortOffset = hr.smallOpgMortOffset
        Me.minHght42Hinges = hr.minHght42Hinges
        Me.hghtRange42Lower = hr.hghtRange42Lower
        Me.hghtRange42Upper = hr.hghtRange42Upper
        Me.hghtRange43Upper = hr.hghtRange43Upper
        Me.hghtRange44Upper = hr.hghtRange44Upper
        Me.ActiveStyle = hr.ActiveStyle
    End Sub

    Public Sub New(ByVal name As String, ByVal xmlname As String)
        MyBase.New()
        Me._xmlFilename = xmlname
        Me._ruleName = name
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
                xnodRoot = xd.DocumentElement.Item("HingePlacementRules")
                Dim xnodWorking As XmlNode
                If xnodRoot.HasChildNodes Then
                    For i As Integer = 0 To xnodRoot.ChildNodes.Count - 1
                        If xnodRoot.ChildNodes(i).Attributes("name").Value = Me._ruleName Then
                            Me._isActiveStyle = CBool(xnodRoot.ChildNodes(i).Attributes("active").Value)
                            xnodWorking = xnodRoot.ChildNodes(i).FirstChild
                            While Not IsNothing(xnodWorking)
                                Select Case xnodWorking.Attributes("name").Value
                                    Case "MortiseOffset"
                                        mortiseOffset = CDec(xnodWorking.InnerText)
                                    Case "SmallOpgMortOffset"
                                        smallOpgMortOffset = CDec(xnodWorking.InnerText)
                                    Case "MinHght42Hinges"
                                        minHght42Hinges = CDec(xnodWorking.InnerText)
                                    Case "HghtRange42Lower"
                                        hghtRange42Lower = CDec(xnodWorking.InnerText)
                                    Case "HghtRange42Upper"
                                        hghtRange42Upper = CDec(xnodWorking.InnerText)
                                    Case "HghtRange43Upper"
                                        hghtRange43Upper = CDec(xnodWorking.InnerText)
                                    Case "HghtRange44Upper"
                                        hghtRange44Upper = CDec(xnodWorking.InnerText)
                                End Select
                                xnodWorking = xnodWorking.NextSibling
                            End While
                        End If
                    Next
                End If
            Catch ex As XmlException
                MessageBox.Show(Me._xmlFilename & vbCr & ex.Message & vbCr & ex.LineNumber & vbCr & ex.LinePosition)
            Finally
                xtr.Close()
            End Try
        End If

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
        xnodRoot = xd.DocumentElement.Item("HingePlacementRules")

        Dim xElem As XmlElement = xd.CreateElement("HingeRule")
        xElem.SetAttribute("name", Me._ruleName)
        xElem.SetAttribute("active", Me.ActiveStyle.ToString)

        Dim child As XmlElement = xd.CreateElement("HingeRuleParm")
        child.SetAttribute("name", "MortiseOffset")
        child.InnerText = mortiseOffset.ToString
        xElem.AppendChild(child)

        child = xd.CreateElement("HingeRuleParm")
        child.SetAttribute("name", "SmallOpgMortOffset")
        child.InnerText = smallOpgMortOffset.ToString
        xElem.AppendChild(child)

        child = xd.CreateElement("HingeRuleParm")
        child.SetAttribute("name", "MinHght42Hinges")
        child.InnerText = minHght42Hinges.ToString
        xElem.AppendChild(child)

        child = xd.CreateElement("HingeRuleParm")
        child.SetAttribute("name", "HghtRange42Lower")
        child.InnerText = hghtRange42Lower.ToString
        xElem.AppendChild(child)

        child = xd.CreateElement("HingeRuleParm")
        child.SetAttribute("name", "HghtRange42Upper")
        child.InnerText = hghtRange42Upper.ToString
        xElem.AppendChild(child)

        child = xd.CreateElement("HingeRuleParm")
        child.SetAttribute("name", "HghtRange43Upper")
        child.InnerText = hghtRange43Upper.ToString
        xElem.AppendChild(child)

        child = xd.CreateElement("HingeRuleParm")
        child.SetAttribute("name", "HghtRange44Upper")
        child.InnerText = hghtRange44Upper.ToString
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
            xnodRoot = xd.DocumentElement.Item("HingePlacementRules")
            Dim xnodWorking As XmlNode
            If xnodRoot.HasChildNodes Then
                For i As Integer = 0 To xnodRoot.ChildNodes.Count - 1
                    If xnodRoot.ChildNodes(i).Attributes("name").Value = Me._ruleName Then
                        found = True
                        xnodRoot.ChildNodes(i).Attributes("active").Value = Me.ActiveStyle.ToString
                        xnodWorking = xnodRoot.ChildNodes(i).FirstChild
                        While Not IsNothing(xnodWorking)
                            Select Case xnodWorking.Attributes("name").Value
                                Case "MortiseOffset"
                                    xnodWorking.InnerText = mortiseOffset.ToString
                                Case "SmallOpgMortOffset"
                                    xnodWorking.InnerText = smallOpgMortOffset.ToString
                                Case "MinHght42Hinges"
                                    xnodWorking.InnerText = minHght42Hinges.ToString
                                Case "HghtRange42Lower"
                                    xnodWorking.InnerText = hghtRange42Lower.ToString
                                Case "HghtRange42Upper"
                                    xnodWorking.InnerText = hghtRange42Upper.ToString
                                Case "HghtRange43Upper"
                                    xnodWorking.InnerText = hghtRange43Upper.ToString
                                Case "HghtRange44Upper"
                                    xnodWorking.InnerText = hghtRange44Upper.ToString
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
            node = xd.SelectSingleNode("SetupParameters/HingePlacementRules/HingeRule[@name='" + Me._ruleName + "']")
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
