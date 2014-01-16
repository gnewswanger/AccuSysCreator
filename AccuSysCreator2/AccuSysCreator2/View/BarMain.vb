Imports Abcnet28Lib
Imports System.IO
Imports System.Xml

''' <summary>
''' File: BarMain.vb
''' Author: Galen Newswanger
''' 
''' This class is a subclass of Windows Form and provides a UI for searching for job programs (acc files) 
''' and printing the barcode list for the MTH machine input.
''' </summary>
''' <remarks></remarks>
Public Class BarMain

    Private printItemsCount As Integer
    Private barcodeList As ArrayList
    Private prnFont As New Font("Courier New", 10, FontStyle.Regular)
    Private titleFont As New Font("Courier New", 16, FontStyle.Bold)

    Private Sub BarMain_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Abcnet282.SerialNo = "A2D37914562CS1"
        loadLbSavedJobs()
    End Sub

    Private Sub loadLbSavedJobs()
        Dim dir As String = Module1.ProgramOutputDirectory
        If Directory.Exists(dir) Then
            Dim dirs As String() = Directory.GetDirectories(dir)
            lbSavedJobs.Items.Clear()
            For i As Integer = 0 To dirs.Length - 1
                lbSavedJobs.Items.Add(dirs(i))
            Next
        End If
    End Sub

    Private Sub loadLbSavedFiles(ByVal path As String)
        If Directory.Exists(path) Then
            Dim dirs As String() = Directory.GetFiles(path, "*.acc")
            lbSavedFiles.Items.Clear()
            Dim width As Integer
            For i As Integer = 0 To dirs.Length - 1
                Dim strs() As String = dirs(i).Split(CChar("\"))
                lbSavedFiles.Items.Add(strs(strs.GetUpperBound(0)))
                width = CInt(lbSavedFiles.CreateGraphics().MeasureString(lbSavedFiles.Items(lbSavedFiles.Items.Count - 1).ToString(), _
                    lbSavedFiles.Font).Width)
                ' Set the column width based on the width of each item in the list.
                If width > lbSavedFiles.ColumnWidth Then
                    lbSavedFiles.ColumnWidth = width
                End If
            Next
        End If
    End Sub

    Private Sub lbSavedJobs_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles lbSavedJobs.SelectedIndexChanged
        loadLbSavedFiles(lbSavedJobs.SelectedItem.ToString)
    End Sub

    Private Sub lbSavedFiles_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles lbSavedFiles.SelectedIndexChanged
        Dim str As String
        str = Module1.BarcodeDirectory
        Dim str2 As String = lbSavedJobs.SelectedItem.ToString
        str = str + str2.Substring(str2.LastIndexOf("\") + 1) + "\"
        doBarcode(str + lbSavedFiles.SelectedItem.ToString)
    End Sub

    Private Sub doBarcode(ByVal dataText As String)
        Dim g As Graphics = pictBarcode.CreateGraphics()
        Abcnet282.Caption = dataText
        pictBarcode.Image = Abcnet282.Barcode(g)
        g.Dispose()
        txtStatus.Text = Abcnet282.Status
        txtString2.Text = Abcnet282.String2
    End Sub

    Private Sub PrintDocument1_PrintPage(ByVal sender As Object, ByVal e As System.Drawing.Printing.PrintPageEventArgs) Handles PrintDocument1.PrintPage
        Dim x As Integer = 80
        Dim y As Integer = 100
        Dim yPos As Single = e.MarginBounds.Top
        Dim index As Integer = 0
        Dim str As String = lbSavedJobs.SelectedItem.ToString.Substring(lbSavedJobs.SelectedItem.ToString.LastIndexOf("\") + 1)
        DrawStringCentered(e, "Job Number: " + str, titleFont, Brushes.Black, e.MarginBounds.Left, e.MarginBounds.Right, yPos)
        yPos += titleFont.GetHeight(e.Graphics)

        While (yPos + y < e.PageBounds.Height) And (printItemsCount > 0)
            lbSavedFiles.SelectedIndex = lbSavedFiles.Items.Count - printItemsCount
            e.Graphics.DrawImage(Abcnet282.Barcode(e.Graphics), x, (yPos)) ', Abcnet1.ImageWidth, Abcnet1.ImageHeight)
            printItemsCount -= 1
            yPos += y
            index += 1
        End While
        If printItemsCount > 0 Then
            DrawStringCentered(e, "Job Number: " + str, titleFont, Brushes.Black, e.MarginBounds.Left, e.MarginBounds.Right, e.MarginBounds.Bottom + titleFont.GetHeight(e.Graphics))
            e.HasMorePages = True
        Else
            DrawStringCentered(e, "Job Number: " + str, titleFont, Brushes.Black, e.MarginBounds.Left, e.MarginBounds.Right, e.MarginBounds.Bottom + titleFont.GetHeight(e.Graphics))
            printItemsCount = lbSavedFiles.Items.Count
            e.HasMorePages = False
        End If
    End Sub

    Private Sub DrawStringCentered(ByRef e As System.Drawing.Printing.PrintPageEventArgs, ByVal text As String, ByVal fnt As Font, ByVal brsh As Brush, ByVal xStart As Single, ByVal xEnd As Single, ByVal ypos As Single)
        Dim xCenter As Single = ((xEnd - xStart - e.Graphics.MeasureString(text, fnt).Width) / 2) + xStart
        e.Graphics.DrawString(text, titleFont, Brushes.Black, xCenter, ypos)
    End Sub

    Private Sub btnPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPrint.Click
        Try
            printItemsCount = lbSavedFiles.Items.Count
            Dim PrintPreviewID As New PrintPreviewDialog
            PrintPreviewID.Document = PrintDocument1
            PrintDocument1.DefaultPageSettings.Margins.Top = 80
            PrintDocument1.DefaultPageSettings.Margins.Left = 80
            PrintDocument1.DefaultPageSettings.Margins.Right = 80
            PrintDocument1.DefaultPageSettings.Margins.Bottom = 80
            If PrintPreviewID.ShowDialog = DialogResult.OK Then
            End If
        Catch ex As Exception
            MessageBox.Show("An error occurred while printing", _
                ex.ToString())
        End Try
    End Sub

    Private Sub btnProperties_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnProperties.Click
        Abcnet282.Properties()
    End Sub

    Private Sub txtTest_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtTest.TextChanged
        doBarcode(txtTest.Text)
    End Sub

    Private Sub txtTest_DoubleClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtTest.DoubleClick
        doBarcode(txtTest.Text)
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Close()
    End Sub

    Private Sub txtSearch_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtSearch.TextChanged
        Dim ch As String
        Dim nInd As Integer
        ch = Module1.ProgramOutputDirectory & txtSearch.Text

        nInd = lbSavedJobs.FindString(ch, 0)
        If nInd >= 0 Then
            lbSavedJobs.SelectedIndex = nInd
        Else
            lbSavedJobs.SelectedIndex = -1
        End If

    End Sub

    Public Sub New()

        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        Me.Abcnet282 = New Abcnet28Lib.Abcnet28(Me.components)
        '
        'Abcnet282
        '
        Me.Abcnet282.AutoCheckdigit = False
        Me.Abcnet282.BackColor = System.Drawing.Color.White
        Me.Abcnet282.BarcodeHeight = 0.5!
        Me.Abcnet282.BarcodeWidth = 3.0!
        Me.Abcnet282.BarRatio = 2.5!
        Me.Abcnet282.BearerSize = 0.0!
        Me.Abcnet282.BorderWidth = 0.0!
        Me.Abcnet282.BothBearers = False
        Me.Abcnet282.Caption = "1234"
        Me.Abcnet282.CodeType = Abcnet28Lib.Abcnet28.bCode.Code_128
        Me.Abcnet282.ErrorCode = 0
        Me.Abcnet282.ExtendBearers = False
        Me.Abcnet282.Extra1 = False
        Me.Abcnet282.Extra2 = False
        Me.Abcnet282.Font = New System.Drawing.Font("Arial", 10.0!)
        Me.Abcnet282.ForeColor = System.Drawing.Color.Black
        Me.Abcnet282.ImageHeight = 16.0!
        Me.Abcnet282.ImageWidth = 36.0!
        Me.Abcnet282.Indicators = False
        Me.Abcnet282.MarginSize = 0.0!
        Me.Abcnet282.Orientation = 0
        Me.Abcnet282.Reduction = 0
        Me.Abcnet282.ShowCheckdigit = False
        Me.Abcnet282.ShowText = True
        Me.Abcnet282.Status = ""
        Me.Abcnet282.String2 = ""
        Me.Abcnet282.TextAlign = System.Drawing.StringAlignment.Center
        Me.Abcnet282.Unit = System.Drawing.GraphicsUnit.Inch
        Me.Abcnet282.Xunit = 0.0!

    End Sub
End Class