''' <summary>
''' <file>File: PanelAddNewClass.vb</file>
''' <author>Author: Galen Newswanger</author>
''' 
''' This class is a subclass of Panel and provides a custom view for 
''' the ParmSetup form.
''' </summary>
''' <remarks></remarks>
Public Class PanelAddNewClass
    Inherits System.Windows.Forms.Panel

#Region "Initialization and construction"
    Private Sub InitializeComponent()
        Me.Label1 = New System.Windows.Forms.Label
        Me.txtNewItemName = New System.Windows.Forms.TextBox
        Me.txtNewDesc = New System.Windows.Forms.TextBox
        Me.Label2 = New System.Windows.Forms.Label
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(2, 20)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(38, 13)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Name:"
        '
        'txtNewItemName
        '
        Me.txtNewItemName.Location = New System.Drawing.Point(107, 17)
        Me.txtNewItemName.Name = "txtNewItemName"
        Me.txtNewItemName.Size = New System.Drawing.Size(140, 20)
        Me.txtNewItemName.TabIndex = 0
        '
        'txtNewDesc
        '
        Me.txtNewDesc.Location = New System.Drawing.Point(320, 16)
        Me.txtNewDesc.Name = "txtNewDesc"
        Me.txtNewDesc.Size = New System.Drawing.Size(200, 20)
        Me.txtNewDesc.TabIndex = 0
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(262, 17)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(35, 13)
        Me.Label2.TabIndex = 0
        Me.Label2.Text = "Desc:"
        '
        'PanelAddNewClass
        '
        Me.Controls.Add(Me.txtNewItemName)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.txtNewDesc)
        Me.Controls.Add(Me.Label2)
        Me.Size = New System.Drawing.Size(600, 36)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Public Sub New()
        InitializeComponent()

    End Sub

    Friend WithEvents txtNewItemName As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents txtNewDesc As System.Windows.Forms.TextBox

#End Region

    Public Property TextReadOnly() As Boolean
        Get
            Return txtNewItemName.ReadOnly
        End Get
        Set(ByVal Value As Boolean)
            txtNewItemName.ReadOnly = Value
        End Set
    End Property

    Public Property ItemName() As String
        Get
            Return txtNewItemName.Text
        End Get
        Set(ByVal Value As String)
            txtNewItemName.Text = Value
        End Set
    End Property

    Public Property ItemDesc() As String
        Get
            Return txtNewDesc.Text
        End Get
        Set(ByVal Value As String)
            txtNewDesc.Text = Value
        End Set
    End Property

End Class
