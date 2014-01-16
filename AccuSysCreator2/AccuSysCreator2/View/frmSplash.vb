''' <summary>
''' File: frmSplash.vb
''' Author: Galen Newswanger
''' 
''' This class is a subclass of Windows form and UI splash screen that can be displayed
''' while application is launching or as an About dialog that responds to a click on the Help menu
''' </summary>
''' <remarks>
''' The use as a splash screen was disabled since the application launches quickly and 
''' the splash screen does not stay visible long enough to view.
''' </remarks>
Public Class frmSplash

    Public Sub OnMainFormShowing(ByVal Sender As Object, ByVal e As EventArgs)
        Close()
    End Sub

    Private Sub btnOk_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOk.Click
        Close()
    End Sub

    Private Sub frmSplash_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        lblVersion.Text = "Version: " & Environment.Version.ToString
        lblOpSystem.Text = "OS: " & Environment.OSVersion.ToString
        lblNoProcessors.Text = "# Processors: " & Environment.ProcessorCount.ToString
        lblUser.Text = "User Name: " & Environment.UserName
        lblDomain.Text = "Domain: " & Environment.UserDomainName
        lblMemory.Text = "System Memory Used: " & (Environment.WorkingSet / 1024).ToString("N00") & "K bytes"
        lblMachine.Text = "Machine Name: " & Environment.MachineName
    End Sub
End Class