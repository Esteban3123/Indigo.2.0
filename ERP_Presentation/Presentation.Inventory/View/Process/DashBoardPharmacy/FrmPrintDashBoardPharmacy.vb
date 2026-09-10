Imports Presentation.Controls
Imports Presentation.Base

Public Class FrmPrintDashBoardPharmacy

    Public listFilter As Object

    ''' <summary>
    ''' Permite saber si se esta abriendo desde la pestaña de devolución
    ''' </summary>
    Public isDevolution As Boolean = False

    Private Sub SimpleButton1_Click_1(sender As Object, e As EventArgs) Handles SimpleButton1.Click
        Dim PrintingPage As Boolean = INDRbYes.Checked
        Dim Requests As Boolean = INDRbRequest.Checked

        If INDRbFuntionalUnit.Checked = True And INDRbHalfLetter.Checked = True Then
            If isDevolution = False Then
                Dim reportDef As New Reporter.rptDashboardPharmacy
                ReportHelper.ExecuteReport(reportDef, Me, Me.BarraBotones.PermissionsForm, listFilter, 1, Requests, PrintingPage)
            Else
                Dim reportDef As New Reporter.rptDashboardPharmacyDevolution
                ReportHelper.ExecuteReport(reportDef, Me, Me.BarraBotones.PermissionsForm, listFilter, 1, Requests, PrintingPage)
            End If
        End If

        If INDRbFuntionalUnit.Checked = True And INDRbNeckband.Checked = True Then
            If isDevolution = False Then
                Dim reportDef As New Reporter.rptDashboardPharmacyReduced
                ReportHelper.ExecuteReport(reportDef, Me, Me.BarraBotones.PermissionsForm, listFilter, 1, Requests, PrintingPage)
            Else
                Dim reportDef As New Reporter.rptDashboardPharmacyDevolutionReduced
                ReportHelper.ExecuteReport(reportDef, Me, Me.BarraBotones.PermissionsForm, listFilter, 1, Requests, PrintingPage)
            End If
        End If

        If INDRbPatient.Checked = True And INDRbHalfLetter.Checked = True Then
            If isDevolution = False Then
                Dim reportDef As New Reporter.rptDashboardPharmacy
                ReportHelper.ExecuteReport(reportDef, Me, Me.BarraBotones.PermissionsForm, listFilter, 2, Requests, PrintingPage)
            Else
                Dim reportDef As New Reporter.rptDashboardPharmacyDevolution
                ReportHelper.ExecuteReport(reportDef, Me, Me.BarraBotones.PermissionsForm, listFilter, 2, Requests, PrintingPage)
            End If
        End If

        If INDRbPatient.Checked = True And INDRbNeckband.Checked = True Then
            If isDevolution = False Then
                Dim reportDef As New Reporter.rptDashboardPharmacyReduced
                ReportHelper.ExecuteReport(reportDef, Me, Me.BarraBotones.PermissionsForm, listFilter, 2, Requests, PrintingPage)
            Else
                Dim reportDef As New Reporter.rptDashboardPharmacyDevolutionReduced
                ReportHelper.ExecuteReport(reportDef, Me, Me.BarraBotones.PermissionsForm, listFilter, 2, Requests, PrintingPage)
            End If
        End If
    End Sub

    Private Sub FrmPrintDashBoardPharmacy_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        INDRbPatient.Checked = True
        INDRbHalfLetter.Checked = True
        INDRbNo.Checked = True
        INDRbPending.Checked = True

        If isDevolution Then 'Si el form se abre desde devolución se cambia el nombre
            INDRbRequest.Text = "Devolución"
        End If
    End Sub
End Class