''' <summary>
''' File: FormUserInput.vb
''' Author: Galen Newswanger
''' 
''' This class is a subclass of Windows Form and provides a UI for the user to
''' input a job number.  The Sub New() takes a Point for its location and an Integer
''' for validating the length of the user's input.
''' </summary>
''' <remarks>
''' The private variable _jobNumberLength represents the length of a valid job number 
''' and is used for validating the user's input in the click event of btnOk.
''' </remarks>
Public Class FormUserInput

    Private _jobNumberLength As Integer
    Private _location As Point

    Public Sub New(ByVal loc As Point, ByVal inputLength As Integer)

        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        Me._jobNumberLength = inputLength
        If Me._jobNumberLength > 0 Then
            lblPrompt.Text = "Enter " + Me._jobNumberLength.ToString + " Digit Job Number:"
        Else
            lblPrompt.Text = "Enter a Folder Name to store programs:"
        End If
        Me._location = loc
    End Sub

    Public Property NumDigitsRequired() As Integer
        Get
            Return Me._jobNumberLength
        End Get
        Set(ByVal Value As Integer)
            Me._jobNumberLength = Value
        End Set
    End Property

    Public Property Prompt() As String
        Get
            Return lblPrompt.Text
        End Get
        Set(ByVal Value As String)
            lblPrompt.Text = Value
        End Set
    End Property

    Private Sub Button1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button1.Click
        If txtJobNumber.Text.Length <> Me._jobNumberLength And Me._jobNumberLength > 0 Then
            DialogResult = DialogResult.None
            MessageBox.Show("A job number must contain " + NumDigitsRequired.ToString + " digits.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button1)
            txtJobNumber.Focus()
        End If
    End Sub

    Private Sub FormUserInput_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Activated
        Me.Location = Me._location
    End Sub
End Class