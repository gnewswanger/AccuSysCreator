''' <summary>
''' <file>File: FormMaterialReportWasteFactor.vb</file>
''' <author>Author: Galen Newswanger</author>
''' 
''' This form prompts the user to set the waste factor desired.  
''' It also provides a filter on material widths to allow the user to select only the desired widths.
''' This form can be used to provide arguments required by the FrameMaterialRegister and  MaterialRequirementsReport.
''' </summary>
''' <remarks></remarks>
Public Class FormMaterialReportWasteFactor

    Private _materialList As Generic.List(Of FrameMaterialClass)

    Public ReadOnly Property ReportMaterialList() As Generic.List(Of FrameMaterialClass)
        Get
            Dim retList As New Generic.List(Of FrameMaterialClass)
            For Each material As FrameMaterialClass In Me._materialList
                If material.IncludeInReport Then
                    retList.Add(material)
                End If
            Next
            Return retList
        End Get
    End Property

    Private Sub trackWasteFactor_ValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles trackWasteFactor.ValueChanged
        Me.SetWasteFactor(Me.trackWasteFactor.Value)
    End Sub

    Private Sub frmMaterialReportWasteFactor_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Me.SetWasteFactor(Me.trackWasteFactor.Value)
        For Each material As FrameMaterialClass In Me._materialList
            Dim item As New ListViewItem(material.MaterialDescript.Trim)
            item.Tag = material
            Me.ListView1.Items.Add(item)
            item.Checked = True
        Next
    End Sub

    Private Sub SetWasteFactor(ByVal wastefactor As Single)
        Me.labelWasteFactor.Text = Me.trackWasteFactor.Value.ToString & "%"
    End Sub

    Public Sub New(ByVal list As Generic.List(Of FrameMaterialClass), Optional ByVal wastefactor As Integer = 20)

        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        Me._materialList = list
        Me.trackWasteFactor.Value = wastefactor
    End Sub

    Private Sub ListView1_ItemChecked(ByVal sender As Object, ByVal e As System.Windows.Forms.ItemCheckedEventArgs) Handles ListView1.ItemChecked
        CType(e.Item.Tag, FrameMaterialClass).IncludeInReport = e.Item.Checked
    End Sub

    Private Sub btnSelectAll_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSelectAll.Click
        Me.SelectDeselectAll(True)
    End Sub

    Private Sub SelectDeselectAll(ByVal isChecked As Boolean)
        For Each item As ListViewItem In Me.ListView1.Items
            item.Checked = isChecked
        Next
    End Sub

    Private Sub btnDeselectAll_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDeselectAll.Click
        Me.SelectDeselectAll(False)
    End Sub
End Class