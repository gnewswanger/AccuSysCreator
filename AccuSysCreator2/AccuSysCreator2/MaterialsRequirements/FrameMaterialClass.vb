Imports System.Globalization

''' <summary>
''' <file>File: FrameMaterialClass.vb</file>
''' <author>Author: Galen Newswanger</author>
''' 
''' This class is used to encapsulate the atributes of a class of framing material
''' (name + width + thickness)for the purpose of calculating length requirements 
''' when ordering special specie etc.
''' </summary>
''' <remarks></remarks>
Public Class FrameMaterialClass

    Private _materialName As String
    Private _materialWidth As Single
    Private _materialThickness As Single
    Private _lengthRegister As Single
    Private _includeOnReport As Boolean = True

    Public Property MaterialName() As String
        Get
            Return Me._materialName
        End Get
        Set(ByVal Value As String)
            Me._materialName = Value
        End Set
    End Property
    Public ReadOnly Property MaterialDescript() As String
        Get
            Return String.Format("{0,10:##0.0##}", Me._materialThickness) & """  x " & String.Format("{0,10:##0.0##}", Me._materialWidth) & """"
        End Get
    End Property
    Public Property MaterialWidth() As Single
        Get
            Return Me._materialWidth
        End Get
        Set(ByVal Value As Single)
            Me._materialWidth = Value
        End Set
    End Property
    Public Property MaterialThickness() As Single
        Get
            Return Me._materialThickness
        End Get
        Set(ByVal value As Single)
            Me._materialThickness = value
        End Set
    End Property
    Public ReadOnly Property MaterialLength() As Single
        Get
            Return Me._lengthRegister
        End Get
    End Property
    Public Property IncludeInReport() As Boolean
        Get
            Return Me._includeOnReport
        End Get
        Set(ByVal value As Boolean)
            Me._includeOnReport = value
        End Set
    End Property

    Public Sub New(ByVal width As Single, ByVal thickness As Single)
        Me.MaterialName = width.ToString
        Me.MaterialWidth = width
        Me.MaterialThickness = thickness
    End Sub

    Public Function AddLength(ByVal len As Single) As Single
        Me._lengthRegister += len
        Return Me.MaterialLength
    End Function

    Public Function ResetLength() As Single
        If MsgBox("The register is about to be reset to 0.  Do you want to continue?", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
            Me._lengthRegister = 0
        End If
        Return Me.MaterialLength
    End Function

End Class