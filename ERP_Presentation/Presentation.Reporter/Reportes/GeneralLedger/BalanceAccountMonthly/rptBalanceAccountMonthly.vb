#Region "Librerias Improtadas"
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.Data.Xpo
Imports Infrastructure.Data.Xpo.AccountingRepository
Imports Domain.Entities
Imports DevExpress.XtraReports.UI
Imports System.Drawing.Printing
#End Region

Public Class rptBalanceAccountMonthly
    Implements IReport

    ''' <summary>
    ''' Variable para inicializar los valores de sesion
    ''' </summary>
    Dim IndigoSessionValues As SessionValues = SessionValues.Instance

    Dim AccumulatedBalance As Decimal = 0
    Dim Balance As Decimal = 0
    Dim PreviousBalance As Decimal = 0

    Public Sub CargarDataSource() Implements IReport.CargarDataSource

        Dim filtroConsulta As String = Nothing

        filtroConsulta = "GetDate('01/' + ToStr(Month) + '/' + ToStr(Year)) >= #" & Format(ParametrosReporte(1), "yyyy-MM-dd") & "# AND Month <> 13 AND IdMainAccount.Number = '" & ParametrosReporte(0) & "' AND IdMainAccount.LegalBookId.Id = " & ParametrosReporte(6)

        If ParametrosReporte(2) IsNot Nothing And ParametrosReporte(3) IsNot Nothing Then
            filtroConsulta &= " AND IdThirdParty.Nit >= '" & ParametrosReporte(2) & "' AND IdThirdParty.Nit <= '" & ParametrosReporte(3) & "'"
        End If

        If ParametrosReporte(4) IsNot Nothing And ParametrosReporte(5) Then
            filtroConsulta &= " AND IdCostCenter.Code >= '" & ParametrosReporte(4) & "' AND IdCostCenter.Code <= '" & ParametrosReporte(5) & "'"
        End If

        Me.DataSource = XpoServiceEx.Instance(IndigoSessionValues.TransactionalContainer).AccountingService.GetCollection(Of GeneralLedgerBalanceXpo)(Nothing, filtroConsulta)

    End Sub

    Public Sub CargarImagenes() Implements IReport.CargarImagenes

    End Sub

    Public ReadOnly Property NameReport As String Implements IReport.NameReport
        Get
            Return ""
        End Get
    End Property

    Public Property ParametrosReporte As Object() Implements IReport.ParametrosReporte

    Private Sub XrTableCell10_SummaryGetResult(sender As Object, e As SummaryGetResultEventArgs) Handles XrTableCell10.SummaryGetResult
        AccumulatedBalance += Balance
        e.Result = AccumulatedBalance
        e.Handled = True
    End Sub

    Private Sub XrTableCell10_SummaryRowChanged(sender As Object, e As EventArgs) Handles XrTableCell10.SummaryRowChanged
        Balance += Convert.ToDouble(GetCurrentColumnValue("Saldo"))
    End Sub

    Private Sub XrTableCell10_SummaryReset(sender As Object, e As EventArgs) Handles XrTableCell10.SummaryReset
        Balance = 0
    End Sub

    Private Sub rptBalanceAccountMonthly_BeforePrint(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles MyBase.BeforePrint
        INDLblCompany.Text = IndigoSessionValues.IndigoCompanyName
        INDLblNitCompany.Text = "Nit:" & IndigoSessionValues.IndigoCompanyNit
        Me.INDLblUserPrint.Text = "Usuario Impresión : " & IndigoSessionValues.UserIndigo & " - " & IndigoSessionValues.UserIndigoName
        INDLblAccount.Text = ParametrosReporte(0)
    End Sub

    Private Sub XrTableCell17_SummaryGetResult(sender As Object, e As SummaryGetResultEventArgs) Handles XrTableCell17.SummaryGetResult
        PreviousBalance = AccumulatedBalance
        e.Result = PreviousBalance
        e.Handled = True
    End Sub

End Class