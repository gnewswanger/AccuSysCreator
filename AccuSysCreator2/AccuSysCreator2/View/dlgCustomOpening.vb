''' <summary>
''' <file>dlgCustomOpening.vb</file>
''' <author>Galen Newswanger</author>
''' 
''' This class is a subclass of Windows Form, intended for editing cabinet opening attributes (FramePart class)
''' and customizing the instance's HingeRule.
''' </summary>
''' <remarks></remarks>
Public Class dlgCustomOpening

    Private _hRules As HingeRule

    Public Property HingeRules() As HingeRule
        Get
            Return GetHingeRules()
        End Get
        Set(ByVal Value As HingeRule)
            SetHingeRules(Value)
        End Set
    End Property

    Public Property HingePlace(ByVal hp As HingePlacement) As HingePlacement
        Get
            Return GetHingePlacement(hp)
        End Get
        Set(ByVal Value As HingePlacement)
            SetHingePlacement(Value)
        End Set
    End Property

    Private Sub checkSpecRules_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles checkSpecRules.CheckedChanged
        Panel2.Enabled = checkSpecRules.Checked
    End Sub

    Private Sub SetHingeRules(ByVal rules As HingeRule)
        Me._hRules = rules
        txtHghtRange2Lower.Text = rules.hghtRange42Lower.ToString
        txtHghtRange2Upper.Text = rules.hghtRange42Upper.ToString
        txtHghtRange3Upper.Text = rules.hghtRange43Upper.ToString
        txtHghtRange4Upper.Text = rules.hghtRange44Upper.ToString
        If rules.overrideNumbeHinges > 0 Then
            txtHingeCntOverride.Value = rules.overrideNumbeHinges
            chkbxHingeCntOverride.Checked = True
            checkSpecRules.Checked = True
        End If
    End Sub

    Private Function GetHingeRules() As HingeRule
        Me._hRules.hghtRange42Lower = CSng(txtHghtRange2Lower.Text)
        Me._hRules.hghtRange42Upper = CSng(txtHghtRange2Upper.Text)
        Me._hRules.hghtRange43Upper = CSng(txtHghtRange3Upper.Text)
        Me._hRules.hghtRange44Upper = CSng(txtHghtRange4Upper.Text)
        If Me.chkbxHingeCntOverride.Checked Then
            Me._hRules.overrideNumbeHinges = CInt(txtHingeCntOverride.Value)
        End If
        Return Me._hRules
    End Function

    Private Sub CustomPartsDlg_Closing(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles MyBase.Closing
        If CDec(txtHghtRange2Upper.Text) < CDec(txtHghtRange2Lower.Text) Then
            DialogResult = DialogResult.None
            MessageBox.Show("Upper limit must be equal to or greater than lower.")
            txtHghtRange2Lower.Focus()
        ElseIf CDec(txtHghtRange3Upper.Text) < CDec(txtHghtRange2Upper.Text) Then
            DialogResult = DialogResult.None
            MessageBox.Show("Upper limit must be equal to or greater than lower.")
            txtHghtRange3Upper.Focus()
        ElseIf CDec(txtHghtRange4Upper.Text) < CDec(txtHghtRange3Upper.Text) Then
            DialogResult = DialogResult.None
            MessageBox.Show("Upper limit must be equal to or greater than lower.")
            txtHghtRange4Upper.Focus()
        End If
    End Sub

    Private Function GetHingePlacement(ByVal hp As HingePlacement) As HingePlacement
        If checkHingeTop.Checked And (Not (hp And HingePlacement.T) = HingePlacement.T) Then
            hp = hp Or HingePlacement.T
        ElseIf (hp And HingePlacement.T) = HingePlacement.T Then
            hp = hp And Not HingePlacement.T
        End If

        If checkHingeBot.Checked And (Not (hp And HingePlacement.B) = HingePlacement.B) Then
            hp = hp Or HingePlacement.B
        ElseIf (hp And HingePlacement.B) = HingePlacement.B Then
            hp = hp And Not HingePlacement.B
        End If
        Return hp
    End Function

    Private Sub SetHingePlacement(ByVal hp As HingePlacement)
        checkHingeBot.Checked = (hp And HingePlacement.B) = HingePlacement.B
        checkHingeTop.Checked = (hp And HingePlacement.T) = HingePlacement.T

    End Sub

    Public Sub New(rule As HingeRule)

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        Me.SetHingeRules(rule)
    End Sub
End Class