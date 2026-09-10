#Region "Librerias Importadas"
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.Data.Xpo.TreasuryRepository
Imports Infrastructure.Data.Xpo
Imports DevExpress.XtraReports.UI
Imports System.Globalization

#End Region

Public Class rptAdvancesList
    Implements IReport

    ''' <summary>
    ''' Variable para inicializar los valores de sesion
    ''' </summary>
    Dim IndigoSessionValues As SessionValues = SessionValues.Instance
    Dim listReport As List(Of TreasuryVReportAdvancesListXpo)
    Private CurrencyAbbreviation As String = SessionValues.Instance.CurrencyISO4217

    Public Sub CargarDataSource() Implements IReport.CargarDataSource
        Dim filtroConsulta As String = "GetDate(MovesDate) >= #" & Format(ParametrosReporte(0), "yyyy-MM-dd") & "# AND GetDate(MovesDate) <= #" & Format(ParametrosReporte(1), "yyyy-MM-dd") & "#"

        'Se filtra por Proveedores
        If ParametrosReporte(3) IsNot Nothing And ParametrosReporte(4) IsNot Nothing Then
            filtroConsulta &= " AND ThirdPartyNit >= '" & ParametrosReporte(3) & "' AND ThirdPartyNit <= '" & ParametrosReporte(4) & "'"
        End If

        'Se filtra por Anticipos
        If ParametrosReporte(5) IsNot Nothing And ParametrosReporte(6) IsNot Nothing Then
            filtroConsulta &= " AND AdvanceCode >= '" & ParametrosReporte(5) & "' AND AdvanceCode <= '" & ParametrosReporte(6) & "'"
        End If

        'Se filtra por Saldo
        If ParametrosReporte(2) <> 3 Then
            If ParametrosReporte(2) = 1 Then
                filtroConsulta &= " AND BillCurrentBalance > 0"
            Else
                filtroConsulta &= " AND BillCurrentBalance = 0"
            End If
        End If

        listReport = XpoServiceEx.Instance(IndigoSessionValues.TransactionalContainer).PaymentsService.GetCollection(Of TreasuryVReportAdvancesListXpo)(Nothing, filtroConsulta)
        Me.DataSource = listReport
    End Sub

    Public Sub CargarImagenes() Implements IReport.CargarImagenes

    End Sub

    Public ReadOnly Property NameReport As String Implements IReport.NameReport
        Get
            Return ""
        End Get
    End Property

    Public Property ParametrosReporte As Object() Implements IReport.ParametrosReporte

    Private Sub rptAdvancesList_BeforePrint(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles MyBase.BeforePrint
        INDLblCompany.Text = IndigoSessionValues.IndigoCompanyName
        INDLblNitCompany.Text = "Nit:" & IndigoSessionValues.IndigoCompanyNit
        INDLblUserPrint.Text = "Usuario Impresión : " & IndigoSessionValues.UserIndigo & " - " & IndigoSessionValues.UserIndigoName
        Me.INDLBlSubtitle.Text = "Informe comprendido entre " & CDate(ParametrosReporte(0)).ToString("dd De MMMM Del yyyy") & " " & CDate(ParametrosReporte(1)).ToString("A dd De MMMM Del yyyy")

        Dim _culture As CultureInfo = CultureInfo.CurrentCulture.Clone()
        If listReport IsNot Nothing AndAlso listReport?.Any() Then
            CurrencyAbbreviation = TryCast(Me.DataSource, List(Of TreasuryVReportAdvancesListXpo))?.FirstOrDefault?.Abbreviation
            _culture.NumberFormat = CurrencyAbbreviation.GetNumberFormat
            ApplyLocalization(_culture)
        End If

    End Sub

    Private Sub XrTableCell20_BeforePrint(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles XrTableCell20.BeforePrint
        Dim row As TreasuryVReportAdvancesListXpo = GetCurrentRow()
        If row IsNot Nothing AndAlso String.IsNullOrEmpty(row.Abbreviation) Then
            XrTableCell15.Text = Utils.GetMoneyWithISO4217(row.MovesDebit, row.Abbreviation)
        End If
    End Sub

    Private Sub XrTableCell21_BeforePrint(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles XrTableCell21.BeforePrint
        Dim row As TreasuryVReportAdvancesListXpo = GetCurrentRow()
        If row IsNot Nothing AndAlso String.IsNullOrEmpty(row.Abbreviation) Then
            XrTableCell15.Text = Utils.GetMoneyWithISO4217(row.MovesCredit, row.Abbreviation)
        End If
    End Sub

    Private Sub XrTableCell15_BeforePrint(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles XrTableCell15.BeforePrint
        Dim row As TreasuryVReportAdvancesListXpo = GetCurrentRow()
        If row IsNot Nothing AndAlso String.IsNullOrEmpty(row.Abbreviation) Then
            XrTableCell15.Text = Utils.GetMoneyWithISO4217(row.BillValueInitial, row.Abbreviation)
        End If
    End Sub

    Private Sub XrTableCell26_BeforePrint(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles XrTableCell26.BeforePrint
        Dim row As TreasuryVReportAdvancesListXpo = GetCurrentRow()
        If row IsNot Nothing AndAlso String.IsNullOrEmpty(row.Abbreviation) Then
            XrTableCell15.Text = Utils.GetMoneyWithISO4217(row.BillCurrentBalance, row.Abbreviation)
        End If
    End Sub

    Private Sub XrTableCell24_SummaryGetResult(sender As Object, e As SummaryGetResultEventArgs) Handles XrTableCell24.SummaryGetResult, XrTableCell25.SummaryGetResult
        Dim row = GetCurrentRow()
        e.Result = Utils.GetMoneyWithISO4217(e.CalculatedValues.ToEntityList(Of Decimal).Sum(), If(String.IsNullOrEmpty(row?.Abbreviation),
                                             IndigoSessionValues.CurrencyISO4217, row?.Abbreviation))
        e.Handled = True
    End Sub
End Class