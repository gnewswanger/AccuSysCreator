Imports System.Xml
Imports System.Data.SqlClient

''' <summary>
''' <file>File: Printclass.vb</file>
''' <author>Author: Galen Newswanger</author>
''' 
''' The PrintObject class represents a block of text with text and border attributes and 
''' location to be drawn by the PrintClass object.
''' </summary>
''' <remarks></remarks>
Public Class PrintObject
    Public rect As RectangleF
    Public drawBorder As Boolean
    Public text As String
    Public fnt As Font
    Public brsh As Brush
    Public xStart As Single
    Public xEnd As Single
    Public xPos As Single
    Public yPos As Single
    Public e As System.Drawing.Printing.PrintPageEventArgs

    Public Sub New()
        fnt = New Font("Courier New", 10, FontStyle.Regular)
    End Sub

    Public Sub New(ByVal printFont As System.Drawing.Font)
        fnt = printFont
    End Sub
End Class

Public Class ReportJobData
    Public jobNo As String
    Public repTitle As String
    Public repSubtitle As String
End Class

Public Class ReportNodeData
    Public name As String
    Public program As String
    Public Childnodes As ArrayList 'of strings

    Public Sub New()
        Childnodes = New ArrayList
    End Sub
End Class

''' <summary>
''' <file>File: Printclass.vb</file>
''' <author>Author: Galen Newswanger</author>
''' 
''' The PrintClass object encapsulates the print process, providing the PrintPage event
''' handler for the System.Drawing.Printing.PrintDocument class.
''' </summary>
''' <remarks></remarks>
Public Class PrintClass
    Inherits Object

    Private _prnFont As New Font("Courier New", 10, FontStyle.Regular)
    Private _titleFont As New Font("Courier New", 16, FontStyle.Bold)
    Private _repJobData As ReportJobData
    Private _repList As ArrayList
    Private WithEvents _prtDoc As System.Drawing.Printing.PrintDocument
    Private WithEvents _prtPrev As System.Windows.Forms.PrintPreviewDialog
    Private WithEvents _abcnet281 As Abcnet28Lib.Abcnet28
    Private _prtSet As PrintObject
    Private _index As Integer
    Private _firstPass As Boolean

    Public Sub New()
        Me._abcnet281 = New Abcnet28Lib.Abcnet28
        Me._abcnet281.SerialNo = "A2D37914562CS1"
        Me._prtDoc = New System.Drawing.Printing.PrintDocument
        Me._prtPrev = New System.Windows.Forms.PrintPreviewDialog
        Me._prtSet = New PrintObject
        DefaultPageSettings()
        Me._prtPrev.Document = Me._prtDoc
        Me._firstPass = True
    End Sub

    Private Sub DefaultPageSettings()
        Me._prtDoc.DefaultPageSettings.Margins.Top = 80
        Me._prtDoc.DefaultPageSettings.Margins.Bottom = 80
        Me._prtDoc.DefaultPageSettings.Margins.Left = 40
        Me._prtDoc.DefaultPageSettings.Margins.Right = 60
        Me._abcnet281.BarcodeHeight = 0.5
        Me._abcnet281.BarcodeWidth = 3.2
        Me._abcnet281.BarRatio = 2.5
        Me._abcnet281.TextAlign = StringAlignment.Center
        Me._abcnet281.Unit = GraphicsUnit.Inch
        Me._abcnet281.CodeType = Abcnet28Lib.Abcnet28.bCode.Code_128

    End Sub

    Public Sub PrintBarcodeReport(ByVal repData As ReportJobData, ByVal reportNodeList As ArrayList)
        DefaultPageSettings()
        Me._repJobData = repData
        Me._repList = reportNodeList
        Me._prtPrev.ShowDialog()
    End Sub

    Private Sub DrawStringCentered(ByVal ps As PrintObject)
        Dim xCenter As Single = ((ps.xEnd - ps.xStart - ps.e.Graphics.MeasureString(ps.text, ps.fnt).Width) / 2) + ps.xStart
        ps.e.Graphics.DrawString(ps.text, ps.fnt, Brushes.Black, xCenter, ps.yPos)
    End Sub

    Private Sub DrawTextbox(ByVal ps As PrintObject)
        If ps.drawBorder Then
            ps.e.Graphics.DrawRectangle(New Pen(ps.brsh), ps.rect.Left, ps.rect.Top, ps.rect.Width, ps.rect.Height)
        End If
        ps.e.Graphics.DrawString(ps.text, ps.fnt, Brushes.Black, ps.rect)
    End Sub

    Private Sub PrtDocPrintPage(ByVal sender As System.Object, ByVal e As System.Drawing.Printing.PrintPageEventArgs) Handles _prtDoc.PrintPage
        Me._prtSet.e = e
        Me._prtSet.xPos = e.MarginBounds.Left
        Me._prtSet.yPos = e.MarginBounds.Top
        Me._prtSet.brsh = Brushes.Black
        Dim imageheight As Integer = 100
        Dim lineHeight As Single = Me._prnFont.GetHeight(e.Graphics)
        Dim colGutter As Single = 20
        Dim colWidth As Single = (e.MarginBounds.Right - Me._prtSet.xPos - (Me._abcnet281.BarcodeWidth * 100) - colGutter)
        If Me._firstPass Then
            Me._index = 0
        End If
        Me._prtSet.fnt = Me._titleFont
        Me._prtSet.text = "Job Number: " + Me._repJobData.jobNo
        Me._prtSet.xStart = e.MarginBounds.Left
        Me._prtSet.xEnd = e.MarginBounds.Right
        DrawStringCentered(Me._prtSet)
        Me._prtSet.yPos += Me._titleFont.GetHeight(e.Graphics)
        Me._prtSet.fnt = Me._prnFont
        Me._prtSet.text = Me._repJobData.repSubtitle
        DrawStringCentered(Me._prtSet)

        Me._prtSet.yPos += CSng(Me._titleFont.GetHeight(e.Graphics) * 1.5)

        While (Me._prtSet.yPos < e.MarginBounds.Height) And (Me._index < Me._repList.Count)
            Dim str As String = Trim(CType(Me._repList(Me._index), ReportNodeData).name)
            Me._prtSet.drawBorder = False
            Me._prtSet.text = Trim(CType(Me._repList(Me._index), ReportNodeData).program)
            DrawBarcode(Me._prtSet)
            Me._prtSet.rect = New RectangleF(Me._prtSet.xPos + (Me._abcnet281.BarcodeWidth * 100) + colGutter, Me._prtSet.yPos, colWidth, imageheight)
            If CType(Me._repList(Me._index), ReportNodeData).Childnodes.Count > 0 Then
                For i As Integer = 0 To CType(Me._repList(Me._index), ReportNodeData).Childnodes.Count - 1
                    str += ", " & CStr(CType(Me._repList(Me._index), ReportNodeData).Childnodes(i))
                Next
                Me._prtSet.drawBorder = True
                Me._prtSet.fnt = Me._prnFont
                Me._prtSet.text = str
                DrawTextbox(Me._prtSet)
            Else
                Me._prtSet.drawBorder = True
                Me._prtSet.text = str
                Me._prtSet.fnt = Me._prnFont
                DrawTextbox(Me._prtSet)
            End If
            Me._prtSet.yPos += imageheight
            Me._index += 1
        End While
        If (Me._index < Me._repList.Count) Then
            Me._prtSet.fnt = Me._titleFont
            Me._prtSet.text = "Job Number: " + Me._repJobData.jobNo
            Me._prtSet.xStart = e.MarginBounds.Left
            Me._prtSet.xEnd = e.MarginBounds.Right
            Me._prtSet.yPos = e.MarginBounds.Bottom + lineHeight
            DrawStringCentered(Me._prtSet)
            e.HasMorePages = True
            Me._firstPass = False
        Else
            Me._prtSet.fnt = Me._titleFont
            Me._prtSet.text = "Job Number: " + Me._repJobData.jobNo
            Me._prtSet.xStart = e.MarginBounds.Left
            Me._prtSet.xEnd = e.MarginBounds.Right
            Me._prtSet.yPos = e.MarginBounds.Bottom + lineHeight
            DrawStringCentered(Me._prtSet)
            e.HasMorePages = False
            Me._firstPass = True
        End If
    End Sub

    Private Sub DrawBarcode(ByVal ps As PrintObject)
        Dim pictBC As New PictureBox
        Dim g As Graphics = pictBC.CreateGraphics()
        Me._abcnet281.Caption = ps.text
        pictBC.Image = Me._abcnet281.Barcode(g)
        ps.e.Graphics.DrawImage(Me._abcnet281.Barcode(ps.e.Graphics), ps.xPos, ps.yPos) ', Abcnet1.ImageWidth, Abcnet1.ImageHeight)
        g.Dispose()
    End Sub

End Class
