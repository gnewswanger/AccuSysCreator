Imports System.Xml
Imports System.IO

''' <summary>
''' File: StyleClassMetaData.vb
''' Author: Galen Newswanger
''' 
''' This is a class for encapsulating the general attributes of a style class for passing 
''' as arguments in method calls.
''' </summary>
''' <remarks>The style classes with names beginning with "Style" are intended to replace
''' the classes in the folder "ParamClasses". Implementation is still pending.  There are 
''' only a few references to these classes in use currently.</remarks>
Public Class StyleClassMetaData

    Private _styleName As String
    Private _styleDesc As String
    Private _styleSelected As Boolean
    Private _styleGroup As StyleTypes

    Public Property StyleName() As String
        Get
            Return Me._styleName
        End Get
        Set(ByVal value As String)
            Me._styleName = value
        End Set
    End Property

    Public Property StyleDesc() As String
        Get
            Return Me._styleDesc
        End Get
        Set(ByVal value As String)
            Me._styleDesc = value
        End Set
    End Property

    Public Property StyleSelected() As Boolean
        Get
            Return Me._styleSelected
        End Get
        Set(ByVal value As Boolean)
            Me._styleSelected = value
        End Set
    End Property

    Public Property StyleGroup() As StyleTypes
        Get
            Return Me._styleGroup
        End Get
        Set(ByVal value As StyleTypes)
            Me._styleGroup = value
        End Set
    End Property

    Public Sub New()

    End Sub

    Public Sub New(ByVal name As String, ByVal desc As String, ByVal group As StyleTypes)
        Me.StyleName = name
        Me.StyleDesc = desc
        Me.StyleGroup = group
    End Sub

#Region "Setup Data From XML ======"

    Public Shared Function GetRadioButtonGroupDataFromXML() As StyleButtonEventArgs
        Dim retGroup As New StyleButtonEventArgs()
        'Dim xmlFilename As String = Application.StartupPath & "\SetupParms.xml"
        Dim xtr As XmlTextReader = New XmlTextReader(My.Settings.ParmsXmlFile)
        Try
            Dim xd As XmlDocument = New XmlDocument
            xd.Load(xtr)
            Dim xnodRoot As XmlNode = xd.DocumentElement
            Dim xnodWorking As XmlNode
            If xnodRoot.HasChildNodes Then
                xnodWorking = xnodRoot.FirstChild
                While Not IsNothing(xnodWorking)
                    If xnodWorking.Name.Equals("FrameStyles") Then
                        retGroup.StyleMeta.AddRange(GetFrameStyleRadioButtonGroup(xnodWorking))
                    ElseIf xnodWorking.Name.Equals("ThicknessParms") Then
                        retGroup.StyleMeta.AddRange(GetThicknessStyleRadioButtonGroup(xnodWorking))
                    ElseIf xnodWorking.Name.Equals("HingeType") Then
                        retGroup.StyleMeta.AddRange(GetHingeStyleRadioButtonGroup(xnodWorking))
                    End If
                    xnodWorking = xnodWorking.NextSibling
                End While
            End If
        Catch ex As XmlException
            MessageBox.Show("GetRadioButtonGroupDataFromXML" & vbCr & ex.Message & vbCr & ex.LineNumber & vbCr & ex.LinePosition)
        Finally
            xtr.Close()
        End Try
        Return retGroup
    End Function

    Private Shared Function GetFrameStyleRadioButtonGroup(ByVal xnod As XmlNode) As List(Of StyleClassMetaData)
        Dim retList As New List(Of StyleClassMetaData)
        Dim parmNode As XmlNode
        parmNode = xnod.Item("FStyle")
        While Not IsNothing(parmNode)
            If parmNode.Name.Equals("FStyle") Then
                Dim item As New StyleClassMetaData
                item.StyleGroup = StyleTypes.rgFrameEdgeStyle
                item.StyleName = parmNode.Attributes("name").Value
                item.StyleDesc = parmNode.Attributes("description").Value
                item.StyleSelected = False
                retList.Add(item)
            End If
            parmNode = parmNode.NextSibling
        End While
        Dim style As StyleClassMetaData = retList(0)
        style.StyleSelected = True
        Return retList
    End Function

    Private Shared Function GetThicknessStyleRadioButtonGroup(ByVal xnod As XmlNode) As List(Of StyleClassMetaData)
        Dim retList As New List(Of StyleClassMetaData)
        Dim parmNode As XmlNode
        parmNode = xnod.Item("Thickness")
        While Not IsNothing(parmNode)
            If parmNode.Name.Equals("Thickness") Then
                Dim item As New StyleClassMetaData
                item.StyleGroup = StyleTypes.rgThickStyle
                item.StyleName = parmNode.Attributes("name").Value
                item.StyleDesc = parmNode.Attributes("description").Value
                item.StyleSelected = False
                retList.Add(item)
            End If
            parmNode = parmNode.NextSibling
        End While
        Dim style As StyleClassMetaData = retList(0)
        style.StyleSelected = True
        Return retList
    End Function

    Private Shared Function GetHingeStyleRadioButtonGroup(ByVal xnod As XmlNode) As List(Of StyleClassMetaData)
        Dim retList As New List(Of StyleClassMetaData)
        Dim parmNode As XmlNode
        parmNode = xnod.Item("Hinge")
        While Not IsNothing(parmNode)
            If parmNode.Name.Equals("Hinge") Then
                Dim item As New StyleClassMetaData
                item.StyleGroup = StyleTypes.rgHingeStyle
                item.StyleName = parmNode.Attributes("name").Value
                item.StyleDesc = parmNode.Attributes("description").Value
                item.StyleSelected = False
                retList.Add(item)
            End If
            parmNode = parmNode.NextSibling
        End While
        Dim style As StyleClassMetaData = retList(0)
        style.StyleSelected = True
        Return retList
    End Function

#End Region

End Class
