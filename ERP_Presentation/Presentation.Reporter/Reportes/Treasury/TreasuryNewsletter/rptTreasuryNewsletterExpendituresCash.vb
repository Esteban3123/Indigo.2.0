#Region "Librerias Improtadas"
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.Data.Xpo
Imports Infrastructure.Data.Xpo.AccountingRepository
Imports Domain.Entities
Imports DevExpress.XtraReports.UI
Imports System.Drawing.Printing
#End Region

Public Class rptTreasuryNewsletterExpendituresCash
    Implements IReport

    Public Shared ValueCreditCash As String = "0"
    Public Shared ValueCreditCashDecimal As Decimal = 0
    ''' <summary>
    ''' Variable para inicializar los valores de sesion
    ''' </summary>
    Dim IndigoSessionValues As SessionValues = SessionValues.Instance
    Public Sub CargarDataSource() Implements IReport.CargarDataSource

    End Sub

    Public Sub CargarImagenes() Implements IReport.CargarImagenes

    End Sub

    Public ReadOnly Property NameReport As String Implements IReport.NameReport
        Get
            Return ""
        End Get
    End Property

    Public Property ParametrosReporte As Object() Implements IReport.ParametrosReporte

    Private Sub XrTableCell31_SummaryCalculated(sender As Object, e As TextFormatEventArgs) Handles XrTableCell31.SummaryCalculated
        If e.Value IsNot Nothing Then
            ValueCreditCash = e.Value
        Else
            ValueCreditCash = 0
        End If
    End Sub

    Private Sub XrTableCell4_BeforePrint(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles XrTableCell4.BeforePrint
        Dim row = GetCurrentRow()
        XrTableCell4.Text = Utils.GetMoneyWithISO4217(row.Value, If(String.IsNullOrEmpty(row.CurrencyAbbreviation),
                                             IndigoSessionValues.CurrencyISO4217, row.CurrencyAbbreviation))
    End Sub

    Private Sub XrTableCell30_SummaryGetResult(sender As Object, e As SummaryGetResultEventArgs) Handles XrTableCell30.SummaryGetResult, XrTableCell33.SummaryGetResult, XrTableCell31.SummaryGetResult
        Dim row = GetCurrentRow()
        'Asignacion de la abreviacion a los campos
        If e.CalculatedValues.ToEntityList(Of Decimal)?.Any() Then
            Dim sumValue = e.CalculatedValues.ToEntityList(Of Decimal).Sum()

            'Guardar valor decimal para XrTableCell31
            If sender Is XrTableCell31 Then
                ValueCreditCashDecimal = sumValue
            End If

            e.Result = Utils.GetMoneyWithISO4217(sumValue, If(String.IsNullOrEmpty(row.CurrencyAbbreviation),
                                            IndigoSessionValues.CurrencyISO4217, row.CurrencyAbbreviation))
            e.Handled = True
        End If
    End Sub
End Class