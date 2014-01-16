''' <summary>
''' <file>File: MaterialRequirementsReport.vb</file>
''' <author>Author: Galen Newswanger</author>
''' 
''' This class is a subclass of GrnPrintingLibrary.PrintClassBase located in a separate project
''' included on the shared drive.
''' It extends a generic printer object by providing layout parameters and text objects
''' specific to the material requirement report.
''' </summary>
''' <remarks></remarks>
Public Class MaterialRequirementsReport
    Inherits GrnPrintingLibrary.PrintClassBase

    Private _colGutter As Single = 30
    Private _currDetail As FrameMaterialRegister
    Private _wasteFactor As Single

    Public Sub New(ByVal materialReg As FrameMaterialRegister, ByVal waste As Single)
        MyBase.New()
        rptBody.DefaultFont = New Font("Courier New", 12)
        Me._currDetail = materialReg
        Me._wasteFactor = waste
    End Sub

    Public Sub PrintMaterialRequirements()
        Dim param As New GrnPrintingLibrary.TextLineParams
        param.TextFont = New Font("Times New Roman", 18, FontStyle.Bold)
        param.Alignment = StringAlignment.Center
        Me.AddTitleTextLine(Me._currDetail.ReportTitle, , param, True)
        Me.AddTitleTextLine(Me._currDetail.ReportSubtitle, , param, True)
        Me.CreateColumn(0, prtDoc.DefaultPageSettings.Margins.Left, GetPageTextWidth)
        Me.CreateColumn(1, prtDoc.DefaultPageSettings.Margins.Left, 150)
        Me.CreateColumn(2, prtDoc.DefaultPageSettings.Margins.Left + 150, 300)
        Me.CreateColumn(3, prtDoc.DefaultPageSettings.Margins.Left + 450, 200)
        Dim str As String = String.Empty
        param.TextFont = New Font("Times New Roman", 12, FontStyle.Bold)
        param.Alignment = StringAlignment.Near

        If Me._currDetail.MaterialList(0).MaterialWidth <= 2.0 Then
            Me.AddHeaderTextLine("Thickness: " & Me._currDetail.MaterialList(0).MaterialThickness.ToString & "  (Rough: " _
                                 & String.Format("{0,8:##0.0##}", (Me._currDetail.MaterialList(0).MaterialThickness + 0.188)) & ")", 0, param, True)
        Else
            Me.AddHeaderTextLine("Thickness: " & Me._currDetail.MaterialList(0).MaterialThickness.ToString, 0, param, True)
        End If

        For Each material As FrameMaterialClass In Me._currDetail.MaterialList
            If material.IncludeInReport Then
                str = "Width: " & String.Format("{0,8:##0.0##}", material.MaterialWidth)
                Me.AddBodyTextLine(str, 1, param, False)
                If material.MaterialWidth < 1.5 Then
                    str = "  (Rough: " & String.Format("{0,8:##0.0##}", (1.5 + 0.188)) & ")"
                    Me.AddBodyTextLine(str, 2, param, False)
                ElseIf material.MaterialWidth >= 1.5 And material.MaterialWidth <= 2.0 Then
                    str = "  (Rough: " & String.Format("{0,8:##0.0##}", (material.MaterialWidth + 0.188)) & ")"
                    Me.AddBodyTextLine(str, 2, param, False)
                End If
                Dim lngth As Decimal = CDec((material.MaterialLength + (material.MaterialLength * Me._wasteFactor)) / 12)
                If lngth > Math.Truncate(lngth) Then
                    lngth = Math.Truncate(lngth) + 1
                End If
                str = lngth.ToString & " lin. ft."
                Me.AddBodyTextLine(str, 3, param, True)
                Me.AddBodyTextLine("", 0, param, True)
            End If
        Next
        Me.AddBodyTextLine("[Separator]", 0, param, True)
        Me.PrintGenericReport()

    End Sub


End Class
