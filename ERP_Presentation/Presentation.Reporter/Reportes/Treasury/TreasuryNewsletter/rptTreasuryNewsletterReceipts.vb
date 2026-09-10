#Region "Librerias Improtadas"
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.Data.Xpo
Imports Infrastructure.Data.Xpo.AccountingRepository
Imports Domain.Entities
Imports DevExpress.XtraReports.UI
Imports System.Drawing.Printing
Imports System.Globalization
#End Region

Public Class rptTreasuryNewsletterReceipts
    Implements IReport

    Public Shared ValueDebit As String = "0"

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
            ValueDebit = e.Value
        Else
            ValueDebit = 0
        End If
    End Sub

    Private Sub XrTableCell4_BeforePrint(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles XrTableCell4.BeforePrint
        Dim row = GetCurrentRow()
        XrTableCell4.Text = Utils.GetMoneyWithISO4217(row.Value, If(String.IsNullOrEmpty(row?.CurrencyAbbreviation),
                                             IndigoSessionValues.CurrencyISO4217, row?.CurrencyAbbreviation))
    End Sub

    Private Sub XrTableCell30_SummaryGetResult(sender As Object, e As SummaryGetResultEventArgs) Handles XrTableCell30.SummaryGetResult, XrTableCell31.SummaryGetResult, XrTableCell25.SummaryGetResult
        Dim row = GetCurrentRow()
        'Asignacion del formato de moneda
        e.Result = Utils.GetMoneyWithISO4217(e.CalculatedValues.ToEntityList(Of Decimal).Sum(), If(String.IsNullOrEmpty(row?.CurrencyAbbreviation),
                                             IndigoSessionValues.CurrencyISO4217, row?.CurrencyAbbreviation))
        e.Handled = True
    End Sub
End Class