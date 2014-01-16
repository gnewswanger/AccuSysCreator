''' <summary>
''' File: formPurgeDatabase.vb
''' Author: Galen Newswanger
''' 
''' This class is a subclass of Windows Form and provides a UI for the user to
''' select a timespan for records being preserved during purge of database.
''' </summary>
''' <remarks>All records with a modification date prior to selected date will be deleted</remarks>
Public Class formPurgeDatabase

    Public ReadOnly Property Days() As Integer
        Get

        End Get
    End Property

    Public Sub New()

        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        Me.radio90Days.Tag = 90
        Me.radio180Days.Tag = 180
        Me.radio270Days.Tag = 270
        Me.DateTimePicker1.MaxDate = Today.AddDays(-90)
        Me.DateTimePicker1.Value = Today.AddDays(-CInt(Me.radio180Days.Tag))
        Me.SetWarning()
    End Sub

    Private Function GetNumberOfDays() As Integer
        Return CType(Today - Me.DateTimePicker1.Value, TimeSpan).Days
    End Function

    Private Sub SetWarning()
        txtWarning.Text = "You are about to permanently delete all data that has not been updated in the past " _
        & Me.GetNumberOfDays.ToString & " days!" & vbCr & vbCr & "Do you wish to continue?"
    End Sub

    Private Sub radio180Days_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles radio180Days.CheckedChanged, radio90Days.CheckedChanged, radio270Days.CheckedChanged
        If CType(sender, RadioButton).Checked Then
            Me.DateTimePicker1.Value = Today.AddDays(-CInt(CType(sender, RadioButton).Tag))
            Me.SetWarning()
        End If
    End Sub

    Private Sub DateTimePicker1_CloseUp(ByVal sender As Object, ByVal e As System.EventArgs) Handles DateTimePicker1.CloseUp
        Me.radioCustomDays.Checked = True
        Me.SetWarning()
    End Sub

End Class