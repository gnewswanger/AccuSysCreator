Imports System.Xml

''' <summary>
''' <file>File: OperationParms.vb</file>
''' <author>Author: Galen Newswanger</author>
''' 
''' This class simply holds the general parameters to be used when creating
''' the ".acc" file for export to the MTH machine.
''' It should be instantiated in the DataClass class by function GetOperationParms,
''' getting its data from the xml file, "//qccfile/accusysfiles/common/SetupParms.xml" 
''' and the feedrates from the AccuSysCreator database.
''' </summary>
''' <remarks></remarks>
Public Class OperationParms

    Private _xmlFilename As String
    Public drillFeedrate As Single
    Public tenonFeedrate As Single
    Public haunchFeedrate As Single
    Public mortiseFeedrate As Single
    Public tenonMode As Boolean
    Public middleRail As Boolean
    Public haunchDepthAdj As Single
    Public tenonDepthAdj As Single
    Public numClamps As Int32
    Public maxLen2Clamps As Single
    Public minMortiseToolMove As Single
    Public minMortiseShoulder As Single

    Public Sub New(ByVal xmlFile As String)
        Me._xmlFilename = xmlFile
        ReadFromXml()
    End Sub

    Public Sub New(ByVal op As OperationParms)
        CopyFrom(op)
    End Sub

    Private Sub CopyFrom(ByVal op As OperationParms)
        Me._xmlFilename = op._xmlFilename
        Me.drillFeedrate = op.drillFeedrate
        Me.tenonFeedrate = op.tenonFeedrate
        Me.haunchFeedrate = op.haunchFeedrate
        Me.mortiseFeedrate = op.mortiseFeedrate
        Me.tenonMode = op.tenonMode
        Me.middleRail = op.middleRail
        Me.haunchDepthAdj = op.haunchDepthAdj
        Me.tenonDepthAdj = op.tenonDepthAdj
        Me.numClamps = op.numClamps
        Me.maxLen2Clamps = op.maxLen2Clamps
        Me.minMortiseToolMove = op.minMortiseToolMove
        Me.minMortiseShoulder = op.minMortiseShoulder
    End Sub

    Private Sub ReadFromXml()
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
            drillFeedrate = CSng(xd.SelectSingleNode("/SetupParameters/OperationParms/OpParm[@name='DrillFeedrate']").InnerText)
            tenonFeedrate = CSng(xd.SelectSingleNode("/SetupParameters/OperationParms/OpParm[@name='TenonFeedrate']").InnerText)
            haunchFeedrate = CSng(xd.SelectSingleNode("/SetupParameters/OperationParms/OpParm[@name='HaunchFeedrate']").InnerText)
            mortiseFeedrate = CSng(xd.SelectSingleNode("/SetupParameters/OperationParms/OpParm[@name='MortiseFeedrate']").InnerText)
            tenonMode = CBool(xd.SelectSingleNode("/SetupParameters/OperationParms/OpParm[@name='TenonMode']").InnerText)
            middleRail = CBool(xd.SelectSingleNode("/SetupParameters/OperationParms/OpParm[@name='MiddleRail']").InnerText)
            haunchDepthAdj = CSng(xd.SelectSingleNode("/SetupParameters/OperationParms/OpParm[@name='HaunchDepthAdj']").InnerText)
            tenonDepthAdj = CSng(xd.SelectSingleNode("/SetupParameters/OperationParms/OpParm[@name='TenonDepthAdj']").InnerText)
            numClamps = CInt(xd.SelectSingleNode("/SetupParameters/OperationParms/OpParm[@name='NumClamps']").InnerText)
            maxLen2Clamps = CSng(xd.SelectSingleNode("/SetupParameters/OperationParms/OpParm[@name='MaxLength2Clamps']").InnerText)
            minMortiseToolMove = CSng(xd.SelectSingleNode("/SetupParameters/OperationParms/OpParm[@name='MinMortiseToolMove']").InnerText)
            minMortiseShoulder = CSng(xd.SelectSingleNode("/SetupParameters/OperationParms/OpParm[@name='MinMortiseShoulder']").InnerText)
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
            'xd.SelectSingleNode("/SetupParameters/OperationParms/OpParm[@name='DrillFeedrate']").InnerText = drillFeedrate.ToString
            'xd.SelectSingleNode("/SetupParameters/OperationParms/OpParm[@name='TenonFeedrate']").InnerText = tenonFeedrate.ToString
            'xd.SelectSingleNode("/SetupParameters/OperationParms/OpParm[@name='HaunchFeedrate']").InnerText = haunchFeedrate.ToString
            'xd.SelectSingleNode("/SetupParameters/OperationParms/OpParm[@name='MortiseFeedrate']").InnerText = mortiseFeedrate.ToString
            xd.SelectSingleNode("/SetupParameters/OperationParms/OpParm[@name='TenonMode']").InnerText = tenonMode.GetHashCode.ToString
            xd.SelectSingleNode("/SetupParameters/OperationParms/OpParm[@name='MiddleRail']").InnerText = middleRail.GetHashCode.ToString
            xd.SelectSingleNode("/SetupParameters/OperationParms/OpParm[@name='HaunchDepthAdj']").InnerText = haunchDepthAdj.ToString
            xd.SelectSingleNode("/SetupParameters/OperationParms/OpParm[@name='TenonDepthAdj']").InnerText = tenonDepthAdj.ToString
            xd.SelectSingleNode("/SetupParameters/OperationParms/OpParm[@name='NumClamps']").InnerText = numClamps.ToString
            xd.SelectSingleNode("/SetupParameters/OperationParms/OpParm[@name='MaxLength2Clamps']").InnerText = maxLen2Clamps.ToString
            xd.SelectSingleNode("/SetupParameters/OperationParms/OpParm[@name='MinMortiseToolMove']").InnerText = minMortiseToolMove.ToString
            xd.SelectSingleNode("/SetupParameters/OperationParms/OpParm[@name='MinMortiseShoulder']").InnerText = minMortiseShoulder.ToString
            xd.Save(Me._xmlFilename)
        End If
    End Sub

End Class
