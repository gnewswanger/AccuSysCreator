<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class BarMain
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
    Me.components = New System.ComponentModel.Container
    Me.txtSearch = New System.Windows.Forms.TextBox
    Me.Label2 = New System.Windows.Forms.Label
    Me.Button1 = New System.Windows.Forms.Button
    Me.ckbxPrintAll = New System.Windows.Forms.CheckBox
    Me.Label1 = New System.Windows.Forms.Label
    Me.lblStatus = New System.Windows.Forms.Label
    Me.txtTest = New System.Windows.Forms.TextBox
    Me.txtString2 = New System.Windows.Forms.TextBox
    Me.txtStatus = New System.Windows.Forms.TextBox
    Me.btnProperties = New System.Windows.Forms.Button
    Me.btnPrint = New System.Windows.Forms.Button
    Me.pictBarcode = New System.Windows.Forms.PictureBox
    Me.lbSavedJobs = New System.Windows.Forms.ListBox
    Me.lbSavedFiles = New System.Windows.Forms.ListBox
    Me.PrintDocument1 = New System.Drawing.Printing.PrintDocument
    Me.Abcnet282 = New Abcnet28Lib.Abcnet28(Me.components)
    CType(Me.pictBarcode, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.SuspendLayout()
    '
    'txtSearch
    '
    Me.txtSearch.Location = New System.Drawing.Point(184, 9)
    Me.txtSearch.Name = "txtSearch"
    Me.txtSearch.Size = New System.Drawing.Size(248, 20)
    Me.txtSearch.TabIndex = 27
    '
    'Label2
    '
    Me.Label2.AutoSize = True
    Me.Label2.Location = New System.Drawing.Point(24, 9)
    Me.Label2.Name = "Label2"
    Me.Label2.Size = New System.Drawing.Size(157, 13)
    Me.Label2.TabIndex = 26
    Me.Label2.Text = "Search by Job Number/Name >"
    '
    'Button1
    '
    Me.Button1.DialogResult = System.Windows.Forms.DialogResult.OK
    Me.Button1.Location = New System.Drawing.Point(360, 417)
    Me.Button1.Name = "Button1"
    Me.Button1.Size = New System.Drawing.Size(75, 23)
    Me.Button1.TabIndex = 25
    Me.Button1.Text = "E&xit"
    '
    'ckbxPrintAll
    '
    Me.ckbxPrintAll.Location = New System.Drawing.Point(128, 417)
    Me.ckbxPrintAll.Name = "ckbxPrintAll"
    Me.ckbxPrintAll.Size = New System.Drawing.Size(104, 24)
    Me.ckbxPrintAll.TabIndex = 24
    Me.ckbxPrintAll.Text = "Print Entire List"
    '
    'Label1
    '
    Me.Label1.AutoSize = True
    Me.Label1.Location = New System.Drawing.Point(24, 361)
    Me.Label1.Name = "Label1"
    Me.Label1.Size = New System.Drawing.Size(42, 13)
    Me.Label1.TabIndex = 23
    Me.Label1.Text = "Output:"
    '
    'lblStatus
    '
    Me.lblStatus.AutoSize = True
    Me.lblStatus.Location = New System.Drawing.Point(24, 337)
    Me.lblStatus.Name = "lblStatus"
    Me.lblStatus.Size = New System.Drawing.Size(40, 13)
    Me.lblStatus.TabIndex = 22
    Me.lblStatus.Text = "Status:"
    '
    'txtTest
    '
    Me.txtTest.Location = New System.Drawing.Point(72, 385)
    Me.txtTest.Name = "txtTest"
    Me.txtTest.Size = New System.Drawing.Size(360, 20)
    Me.txtTest.TabIndex = 21
    Me.txtTest.Text = "This is a test string"
    '
    'txtString2
    '
    Me.txtString2.Location = New System.Drawing.Point(72, 361)
    Me.txtString2.Name = "txtString2"
    Me.txtString2.Size = New System.Drawing.Size(360, 20)
    Me.txtString2.TabIndex = 18
    Me.txtString2.Text = "TextBox2"
    '
    'txtStatus
    '
    Me.txtStatus.Location = New System.Drawing.Point(72, 337)
    Me.txtStatus.Name = "txtStatus"
    Me.txtStatus.Size = New System.Drawing.Size(360, 20)
    Me.txtStatus.TabIndex = 17
    Me.txtStatus.Text = "TextBox1"
    '
    'btnProperties
    '
    Me.btnProperties.Location = New System.Drawing.Point(272, 417)
    Me.btnProperties.Name = "btnProperties"
    Me.btnProperties.Size = New System.Drawing.Size(75, 23)
    Me.btnProperties.TabIndex = 20
    Me.btnProperties.Text = "Properties"
    '
    'btnPrint
    '
    Me.btnPrint.Location = New System.Drawing.Point(48, 417)
    Me.btnPrint.Name = "btnPrint"
    Me.btnPrint.Size = New System.Drawing.Size(75, 23)
    Me.btnPrint.TabIndex = 19
    Me.btnPrint.Text = "Print"
    '
    'pictBarcode
    '
    Me.pictBarcode.Location = New System.Drawing.Point(32, 249)
    Me.pictBarcode.Name = "pictBarcode"
    Me.pictBarcode.Size = New System.Drawing.Size(400, 80)
    Me.pictBarcode.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize
    Me.pictBarcode.TabIndex = 16
    Me.pictBarcode.TabStop = False
    '
    'lbSavedJobs
    '
    Me.lbSavedJobs.Location = New System.Drawing.Point(32, 33)
    Me.lbSavedJobs.Name = "lbSavedJobs"
    Me.lbSavedJobs.Size = New System.Drawing.Size(400, 69)
    Me.lbSavedJobs.TabIndex = 15
    '
    'lbSavedFiles
    '
    Me.lbSavedFiles.Location = New System.Drawing.Point(32, 121)
    Me.lbSavedFiles.MultiColumn = True
    Me.lbSavedFiles.Name = "lbSavedFiles"
    Me.lbSavedFiles.Size = New System.Drawing.Size(400, 121)
    Me.lbSavedFiles.TabIndex = 14
    '
    'PrintDocument1
    '
    '
    'Abcnet282
    '
    Me.Abcnet282.AutoCheckdigit = False
    Me.Abcnet282.BackColor = System.Drawing.Color.White
    Me.Abcnet282.BarcodeHeight = 16.0!
    Me.Abcnet282.BarcodeWidth = 36.0!
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
    Me.Abcnet282.Unit = System.Drawing.GraphicsUnit.Millimeter
    Me.Abcnet282.Xunit = 0.0!
    '
    'BarMain
    '
    Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
    Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
    Me.ClientSize = New System.Drawing.Size(460, 456)
    Me.Controls.Add(Me.txtSearch)
    Me.Controls.Add(Me.Label2)
    Me.Controls.Add(Me.Button1)
    Me.Controls.Add(Me.ckbxPrintAll)
    Me.Controls.Add(Me.Label1)
    Me.Controls.Add(Me.lblStatus)
    Me.Controls.Add(Me.txtTest)
    Me.Controls.Add(Me.txtString2)
    Me.Controls.Add(Me.txtStatus)
    Me.Controls.Add(Me.btnProperties)
    Me.Controls.Add(Me.btnPrint)
    Me.Controls.Add(Me.pictBarcode)
    Me.Controls.Add(Me.lbSavedJobs)
    Me.Controls.Add(Me.lbSavedFiles)
    Me.Name = "BarMain"
    Me.Text = "BarMain"
    CType(Me.pictBarcode, System.ComponentModel.ISupportInitialize).EndInit()
    Me.ResumeLayout(False)
    Me.PerformLayout()

  End Sub
    Friend WithEvents txtSearch As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents ckbxPrintAll As System.Windows.Forms.CheckBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents lblStatus As System.Windows.Forms.Label
    Friend WithEvents txtTest As System.Windows.Forms.TextBox
    Friend WithEvents txtString2 As System.Windows.Forms.TextBox
    Friend WithEvents txtStatus As System.Windows.Forms.TextBox
    Friend WithEvents btnProperties As System.Windows.Forms.Button
    Friend WithEvents btnPrint As System.Windows.Forms.Button
    Friend WithEvents pictBarcode As System.Windows.Forms.PictureBox
    Friend WithEvents lbSavedJobs As System.Windows.Forms.ListBox
    Friend WithEvents lbSavedFiles As System.Windows.Forms.ListBox
  Friend WithEvents PrintDocument1 As System.Drawing.Printing.PrintDocument
  Friend WithEvents Abcnet282 As Abcnet28Lib.Abcnet28
End Class
