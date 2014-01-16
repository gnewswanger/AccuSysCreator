Imports System.IO
Imports System.Text.RegularExpressions

''' <summary>
''' File: frmEditProgramText.vb
''' Author: Galen Newswanger
''' 
''' This class is a subclass of Windows Form and provides a UI for users to view and 
''' manually modify the "acc" file that was generated for import into the MTH machine.
''' </summary>
''' <remarks>
''' The form includes an image panel that displays a visual image of the effects of the
''' current operations.
''' </remarks>
Public Class frmEditProgramText

    Private _isDirty As Boolean
    Private _currentFileInfo As System.IO.FileInfo
    Private _db As DataClass
    Private _thePart As FramePart

    Protected Property ThePart() As FramePart
        Get
            Return Me._thePart
        End Get
        Set(ByVal value As FramePart)
            Me._thePart = value
            Me.UpdatePartSizeText(New SizeF(Me._thePart.Length, Me._thePart.Width))
        End Set
    End Property

#Region "Initialization / Finalization"

    Public Sub New(ByVal db As DataClass)

        ' This call is required by the Windows Form Designer.
        InitializeComponent()
        ' Add any initialization after the InitializeComponent() call.
        Me._db = db
    End Sub

    Public Sub New(ByVal fn As System.IO.FileInfo, ByVal fp As FramePart, ByVal db As DataClass)
        MyBase.New()
        InitializeComponent()
        Me._db = db
        Me._currentFileInfo = fn
        If fp IsNot Nothing Then
            Me.ThePart = fp
        End If
        FramePartImagePanel1.GraphicDisplay_BackColor = My.Settings.GraphicDisplayBackColor
        Me.LoadFromFile(fn)
    End Sub

    Private Sub frmEditProgramText_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Me._isDirty = False
        EnableToolButtons()
        FramePartImagePanel1.GraphicDisplay_BackColor = My.Settings.GraphicDisplayBackColor
    End Sub

    Private Sub frmEditProgramText_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        If Me._isDirty Then
            If MsgBox("You made changes to this program.  Do you want to save them?", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
                RichTextBox1.SaveFile(Me._currentFileInfo.FullName, RichTextBoxStreamType.PlainText)
            End If
        End If
    End Sub

#End Region

#Region "Menu / Button Event Handling"

    Private Sub EnableToolButtons()
        toolBtnSave.Enabled = Me._isDirty
        toolBtnRevert.Enabled = Me._isDirty
    End Sub

    Private Sub ExitProgram(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuExitProgram.Click, toolBtnClose.Click
        Close()
    End Sub

    Private Sub GraphicDisplayBackColor(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuGraphicBackColor.Click
        With New System.Windows.Forms.ColorDialog
            If .ShowDialog() = Windows.Forms.DialogResult.OK Then
                My.Settings.GraphicDisplayBackColor = .Color
                FramePartImagePanel1.GraphicDisplay_BackColor = .Color
            End If
        End With
    End Sub


    Private Sub OpenProgramFile(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OpenFileToolStripMenuItem.Click
        OpenFileDialog1.ShowDialog()
        Try
            If File.Exists(OpenFileDialog1.FileName) Then
                Me._currentFileInfo = New System.IO.FileInfo(OpenFileDialog1.FileName)
                LoadFromFile(Me._currentFileInfo)
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        Finally

        End Try
    End Sub

    Private Sub LoadFromFile(ByVal currFileInfo As FileInfo)
        Dim rdr As New StreamReader(currFileInfo.FullName)
        Me.RichTextBox1.Text = rdr.ReadToEnd
        rdr.Close()
        If Me.SetJobPart() Then
            Me.RefreshGraphicDisplay(Me, New EventArgs)
        Else
            MsgBox("Unable to find part in database.")
        End If
    End Sub

    Private Sub RefreshGraphicDisplay(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles toolBtnRefresh.Click
        If Me.ThePart IsNot Nothing OrElse Me.SetJobPart() Then
            FramePartImagePanel1.DisplayGraphic(Me.ThePart.EndTenonArgs, RichTextBox1.Text)
            DisplayList() '(FramePartImagePanel1.PartSize)
        End If
    End Sub

    Private Sub SaveProgramFile(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuSaveFile.Click, toolBtnSave.Click
        If MsgBox("Saving changes will overwrite existing data.  Do you wish to save changes?", MsgBoxStyle.YesNo, "Save File") = MsgBoxResult.Yes Then
            RichTextBox1.SaveFile(Me._currentFileInfo.FullName, RichTextBoxStreamType.PlainText)
            Me._isDirty = False
            EnableToolButtons()
        End If
    End Sub

    Private Sub RevertToLastSaved(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles toolBtnRevert.Click, RevertToLastSavedToolStripMenuItem.Click
        If Me._isDirty Then
            If MsgBox("You made changes to this program and reverting will lose them. Do you wish to continue?", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
                Dim rdr As New StreamReader(OpenFileDialog1.FileName)
                Me.RichTextBox1.Text = rdr.ReadToEnd
                If Me.SetJobPart() Then
                    Me.RefreshGraphicDisplay(sender, e)
                Else
                    MsgBox("Unable to find part in database.")
                End If
            End If
        End If
    End Sub

#End Region

    Private Sub UpdatePartSizeText(ByVal ptSize As SizeF)
        Me.txtPartLength.Text = ptSize.Width.ToString
        Me.txtPartWidth.Text = ptSize.Height.ToString
        FramePartImagePanel1.PartSize = ptSize
    End Sub

    Private Sub DisplayList()
        RichTextBox1.Text = FramePartImagePanel1.FileText
        RichTextBox1.SelectAll()
        RichTextBox1.SelectionTabs = New Integer() {50, 100, 150, 225, 300, 375, 450, 525, 600, 675}
        RichTextBox1.DeselectAll()
    End Sub

    Private Sub frmEditProgramText_ResizeEnd(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.ResizeEnd
        FramePartImagePanel1.DisplayGraphic(Me.ThePart.EndTenonArgs, RichTextBox1.Text)
    End Sub

    Private Sub PartWidth_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtPartWidth.TextChanged, txtPartLength.TextChanged
        toolBtnPartSize.Text = "Part Size: " & txtPartWidth.Text & ", " & txtPartLength.Text
    End Sub

    Private Sub RichTextBox1_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles RichTextBox1.KeyPress
        Me._isDirty = True
        EnableToolButtons()
    End Sub

    Private Sub toolBtnPartSize_DropDownClosed(ByVal sender As Object, ByVal e As System.EventArgs) Handles toolBtnPartSize.DropDownClosed
        If IsNumeric(txtPartWidth.Text) And IsNumeric(txtPartLength.Text) Then
            FramePartImagePanel1.PartSize = New SizeF(CSng(txtPartLength.Text), CSng(txtPartWidth.Text))
            RefreshGraphicDisplay(sender, e)
        End If
    End Sub
    Private Function SetJobPart() As Boolean
        Dim myMatch As Match = Regex.Match(Me.RichTextBox1.Text, "(?!(Button\ Label\n))[A-Za-z0-9]{9}(-|_|.)?\d{2,3}[A-Z]?(-|_|.)\w+")
        If Regex.IsMatch(myMatch.Value, "^[A-Za-z0-9]{9}.\d{2,3}[A-Z]?.\D+") Then
            Dim str As String = Regex.Replace(myMatch.Value, "(_|-|\.)", ".")
            Me.ThePart = Me._db.GetFramePart(str)
            Return (Me.ThePart IsNot Nothing)
        Else
            MsgBox("'" & myMatch.Value & "' is an invalid job number")
            Return False
        End If

    End Function

End Class