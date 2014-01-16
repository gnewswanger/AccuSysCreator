''' <summary>
''' <file>File: UserInputBox.vb</file>
''' <author>Author: Galen Newswanger</author>
''' 
''' This class is a subclass of Windows Form and provides a UI for the user to
''' input a job number. 
''' </summary>
''' <remarks>
''' The private variable _jobNumberLength represents the length of a valid job number 
''' and is used for validating the user's input in the click event of btnOk.
''' </remarks>
Public Class UserInputBox

    Private _jobNumberLength As Integer

    Public Sub New(ByVal inputLength As Integer)

        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        Me._jobNumberLength = inputLength
        If Me._jobNumberLength > 0 Then
            lblPrompt.Text = "Enter " + Me._jobNumberLength.ToString + " Digit Job Number:"
        Else
            lblPrompt.Text = "Enter a Folder Name to store programs:"
        End If

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

    Private Sub Button1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnOk.Click
        If txtJobNumber.Text.Length <> Me._jobNumberLength And Me._jobNumberLength > 0 Then
            DialogResult = DialogResult.None
            MessageBox.Show("A job number must contain " + NumDigitsRequired.ToString + " digits.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button1)
            txtJobNumber.Focus()
        End If
    End Sub

End Class