Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.Data.Xpo
Imports Infrastructure.Data.Xpo.AccountingRepository
Imports DevExpress.XtraReports.UI
Imports System.Globalization

#Region "Imports"

#End Region


Public Class rptDailyLedger
    Implements IReport

    ''' <summary>
    ''' Variable para inicializar los valores de sesion
    ''' </summary>
    Dim IndigoSessionValues As SessionValues = SessionValues.Instance

    Dim AccumulatedBalance As Decimal = 0
    Dim Balance As Decimal = 0
    Dim a As Integer

    Public Sub CargarDataSource() Implements IReport.CargarDataSource

    End Sub

    Public Sub CargarImagenes() Implements IReport.CargarImagenes

    End Sub

    ''' <summary>
    ''' se ejecuta para cargar los datasources de los subreportes
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function FillListDailyLedger()
        a = ParametrosReporte(6)
        Dim filtroConsulta As String = Nothing
        filtroConsulta = "IdAccounting.VoucherDate >= #" & Format(Me.ParametrosReporte(0), "yyyy-MM-dd") & "# AND IdAccounting.VoucherDate <= #" & Format(Me.ParametrosReporte(1), "yyyy-MM-dd") & "# AND IdAccounting.LegalBookId = " & ParametrosReporte(4) & " AND IdAccounting.Status = 2"
        Dim lista = XpoServiceEx.Instance(IndigoSessionValues.TransactionalContainer).AccountingService.GetCollection(Of JournalVoucherDetailsXpo)(Nothing, filtroConsulta)
        Return lista
    End Function

    Public ReadOnly Property NameReport As String Implements IReport.NameReport
        Get
            Return ""
        End Get
    End Property

    Public Property ParametrosReporte As Object() Implements IReport.ParametrosReporte

    Private Sub rpt_BeforePrint(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles MyBase.BeforePrint
        Me.INDLblCompany.Text = IndigoSessionValues.IndigoCompanyName
        Me.INDLblNitCompany.Text = "Nit : " & IndigoSessionValues.IndigoCompanyNit
        Me.INDLblDate.Text = "Para el Mes De " & CDate(Me.ParametrosReporte(0)).ToString("MMMM Del yyyy")
        Me.INDLblUserPrint.Text = "Usuario Impresión : " & IndigoSessionValues.UserIndigo & " - " & IndigoSessionValues.UserIndigoName

        If Me.ParametrosReporte(2) = 1 Then
            INDSrGroupJournalAccount.Visible = True
            INDSrGroupAccountJournal.Visible = False
        Else
            INDSrGroupJournalAccount.Visible = False
            INDSrGroupAccountJournal.Visible = True

            'Group Header Band Del Subreport 
            Dim INDGroupHeaderCuentaDebit As GroupHeaderBand = INDSrGroupAccountJournal.ReportSource.FindControl("INDGhAccount", True)
            Dim INDGroupHeaderSubCuentaDebit As GroupHeaderBand = INDSrGroupAccountJournal.ReportSource.FindControl("INDGhSubAccount", True)
            Dim INDGroupHeaderAuxiliarDebit As GroupHeaderBand = INDSrGroupAccountJournal.ReportSource.FindControl("INDGhAuxiliary", True)
            Dim INDGroupHeaderSubAuxiliarDebit As GroupHeaderBand = INDSrGroupAccountJournal.ReportSource.FindControl("INDGhSubAuxiliar", True)

            Dim INDGroupFooterCuentaDebit As GroupFooterBand = INDSrGroupAccountJournal.ReportSource.FindControl("INDGfAccount", True)
            Dim INDGroupFooterSubCuentaDebit As GroupFooterBand = INDSrGroupAccountJournal.ReportSource.FindControl("INDGfSubAccount", True)
            Dim INDGroupFooterAuxiliarDebit As GroupFooterBand = INDSrGroupAccountJournal.ReportSource.FindControl("INDGfAuxiliary", True)
            Dim INDGroupFooterSubAuxiliarDebit As GroupFooterBand = INDSrGroupAccountJournal.ReportSource.FindControl("INDGfSubAuxiliar", True)

            ' si tipo de reporte es igual a cuenta ocultar la subcuenta y auxiliar
            If Me.ParametrosReporte(2) = 2 Then
                INDGroupHeaderCuentaDebit.Visible = True
                INDGroupHeaderSubCuentaDebit.Visible = False
                INDGroupHeaderAuxiliarDebit.Visible = False
                INDGroupHeaderSubAuxiliarDebit.Visible = False

                INDGroupFooterCuentaDebit.Visible = True
                INDGroupFooterSubCuentaDebit.Visible = False
                INDGroupFooterAuxiliarDebit.Visible = False
                INDGroupFooterSubAuxiliarDebit.Visible = False

                INDSrGroupAccountJournal.ReportSource.FindControl("INDLblTiTleNumber", True).Text = "CUENTA"
                INDSrGroupAccountJournal.ReportSource.FindControl("INDLblTitleName", True).Text = "NOMBRE DE LA CUENTA"

                'si tipo de reporte es igual a subcuenta ocultar la cuenta y auxiliar
            ElseIf Me.ParametrosReporte(2) = 3 Then
                INDGroupHeaderCuentaDebit.Visible = False
                INDGroupHeaderSubCuentaDebit.Visible = True
                INDGroupHeaderAuxiliarDebit.Visible = False
                INDGroupHeaderSubAuxiliarDebit.Visible = False

                INDGroupFooterCuentaDebit.Visible = False
                INDGroupFooterSubCuentaDebit.Visible = True
                INDGroupFooterAuxiliarDebit.Visible = False
                INDGroupFooterSubAuxiliarDebit.Visible = False

                INDSrGroupAccountJournal.ReportSource.FindControl("INDLblTiTleNumber", True).Text = "SUBCUENTA"
                INDSrGroupAccountJournal.ReportSource.FindControl("INDLblTitleName", True).Text = "NOMBRE DE LA SUBCUENTA"

                'si tipo de reporte es igual a auxiliar ocultar la cuenta y subcuenta
            ElseIf Me.ParametrosReporte(2) = 4 Then
                INDGroupHeaderCuentaDebit.Visible = False
                INDGroupHeaderSubCuentaDebit.Visible = False
                INDGroupHeaderAuxiliarDebit.Visible = True
                INDGroupHeaderSubAuxiliarDebit.Visible = True

                INDGroupFooterCuentaDebit.Visible = False
                INDGroupFooterSubCuentaDebit.Visible = False
                INDGroupFooterAuxiliarDebit.Visible = True
                INDGroupFooterSubAuxiliarDebit.Visible = True

                INDSrGroupAccountJournal.ReportSource.FindControl("INDLblTiTleNumber", True).Text = "AUXILIAR"
                INDSrGroupAccountJournal.ReportSource.FindControl("INDLblTitleName", True).Text = "NOMBRE DE LA CUENTA AUXILIAR"
            End If
        End If

    End Sub

    Private Sub PageHeader_BeforePrint(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles PageHeader.BeforePrint
        If ParametrosReporte(3) = 2 Then
            XrPageInfo1.Visible = True
            XrPageInfo1.Format = ParametrosReporte(5) & a
            a = a + 1
        End If

    End Sub
End Class