''' <summary>
''' File: AbstractStyleClass.vb
''' Author: Galen Newswanger
''' 
''' This class is an abstract (MustInherit) class providing a common base class for 
''' all style classes.
''' </summary>
''' <remarks>The style classes with names beginning with "Style" are intended to replace
''' the classes in the folder "ParamClasses". Implementation is still pending.  There are 
''' only a few references to these classes in use currently.</remarks>
Public MustInherit Class AbstractStyleClass

    Protected _edgeName As String
    Protected _styleName As String

    Public Sub New(ByVal edgeName As String, ByVal styleName As String)
        MyBase.New()
        Me._edgeName = edgeName
        Me._styleName = styleName
    End Sub


End Class
