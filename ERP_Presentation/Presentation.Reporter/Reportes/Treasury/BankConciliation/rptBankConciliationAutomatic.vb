#Region "Librerias Importadas"
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.Data.Xpo.TreasuryRepository
Imports Infrastructure.Data.Xpo
Imports DevExpress.XtraReports.UI
Imports System.Globalization

#End Region

Public Class rptBankConciliationAutomatic
    Implements IReport

#Region "Globals"
    ''' <summary>
    ''' Variable para inicializar los valores de sesion
    ''' </summary>
    Public IndigoSessionValues As SessionValues = SessionValues.Instance
    ''' <summary>
    ''' Variable que almacena el DataSource del reporte
    ''' </summary>
    Dim listReport As List(Of BankReconciliationAutomaticXpo)
    ''' <summary>
    ''' Variable que almacena el objeto principal del reporte
    ''' </summary>
    Dim dataReport As BankReconciliationAutomaticXpo
    ''' <summary>
    ''' Abreviación de la moneda
    ''' </summary>
    Private _currencyAbbreviation As String = SessionValues.Instance.CurrencyISO4217
    ''' <summary>
    ''' Parámetros del reporte
    ''' </summary>
    ''' <returns></returns>
    Public Property ParametrosReporte As Object() Implements IReport.ParametrosReporte
#End Region


    ''' <summary>
    ''' Método que asigna el DataSource al reporte
    ''' </summary>
    Public Sub CargarDataSource() Implements IReport.CargarDataSource
        Dim filtroConsulta As String = "Id = " & ParametrosReporte(0)

        listReport = XpoServiceEx.Instance(IndigoSessionValues.TransactionalContainer).PaymentsService.GetCollection(Of BankReconciliationAutomaticXpo)(Nothing, filtroConsulta)
        dataReport = listReport(0)
        Me.DataSource = listReport
    End Sub

    Public Sub CargarImagenes() Implements IReport.CargarImagenes

    End Sub

    Public ReadOnly Property NameReport As String Implements IReport.NameReport
        Get
            Return ""
        End Get
    End Property

    Private Sub rptBankConciliationAutomatic_BeforePrint(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles MyBase.BeforePrint
        INDLblCompany.Text = IndigoSessionValues.IndigoCompanyName
        INDLblUserPrint.Text = "Usuario Impresión : " & IndigoSessionValues.UserIndigo & " - " & IndigoSessionValues.UserIndigoName
        Me.INDLBlSubtitle.Text = MonthName(Month(dataReport.DocumentDate)) & " " & Year(dataReport.DocumentDate) & " - " & " CUENTA BANCARIA " & dataReport.EntityBankAccountId.IdBank.Name & " - " & dataReport.EntityBankAccountId.Number

        Dim _culture As CultureInfo = CultureInfo.CurrentCulture.Clone()
        If dataReport IsNot Nothing Then
            _currencyAbbreviation = TryCast(Me.DataSource, List(Of BankReconciliationAutomaticXpo))?.FirstOrDefault.EntityBankAccountId.CurrencyAbbreviation
            _culture.NumberFormat = _currencyAbbreviation.GetNumberFormat
            ApplyLocalization(_culture)
        End If

    End Sub

#Region "BeforePrint"
    ''' <summary>
    ''' Evento que obtiene el valor de Transacciones pendientes de registro bancario
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub XrLabel19_BeforePrint(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles XrLabel19.BeforePrint
        If dataReport.EntityBankAccountValue > dataReport.ExtractValue Then
            dataReport.PendingBankRecordTransactions = dataReport.BankReconciliationAutomaticDetailXpo.ToList().
                                                        Where(Function(x) x.Nature = 2 And Not x.Reconciled).
                                                        Sum(Function(s) s.Value)
        Else
            dataReport.PendingBankRecordTransactions = dataReport.BankReconciliationAutomaticDetailXpo.ToList().
                                                        Where(Function(x) x.Nature = 1 And Not x.Reconciled).
                                                        Sum(Function(s) s.Value)
        End If
        XrLabel19.Text = Utils.GetMoneyWithISO4217(dataReport.PendingBankRecordTransactions, _currencyAbbreviation)
    End Sub
    ''' <summary>
    ''' Evento que obtiene el valor de Consignaciones no registradas en extracto
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub XrLabel20_BeforePrint(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles XrLabel20.BeforePrint
        If dataReport.EntityBankAccountValue > dataReport.ExtractValue Then
            dataReport.ConsignmentsNotRecordedInExtract = dataReport.BankReconciliationAutomaticDetailXpo.ToList().
                                                        Where(Function(x) x.Nature = 1 And Not x.Reconciled).
                                                        Sum(Function(s) s.Value)
        Else
            dataReport.ConsignmentsNotRecordedInExtract = dataReport.BankReconciliationAutomaticDetailXpo.ToList().
                                                        Where(Function(x) x.Nature = 2 And Not x.Reconciled).
                                                        Sum(Function(s) s.Value)
        End If
        XrLabel20.Text = Utils.GetMoneyWithISO4217(dataReport.ConsignmentsNotRecordedInExtract, _currencyAbbreviation)
    End Sub
    ''' <summary>
    ''' Evento que obtiene el valor de los Documentos/Notas que suman
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub XrLabel21_BeforePrint(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles XrLabel21.BeforePrint
        If dataReport.EntityBankAccountValue > dataReport.ExtractValue Then
            dataReport.PositiveDocumentsPending = dataReport.BankReconciliationAutomaticExtractDetailXpo.ToList().
                                                        Where(Function(x) x.Nature = 1 And Not x.Reconciled).
                                                        Sum(Function(s) s.Value)
        Else
            dataReport.PositiveDocumentsPending = dataReport.BankReconciliationAutomaticExtractDetailXpo.ToList().
                                                        Where(Function(x) x.Nature = 2 And Not x.Reconciled).
                                                        Sum(Function(s) s.Value)
        End If
        XrLabel21.Text = Utils.GetMoneyWithISO4217(dataReport.PositiveDocumentsPending, _currencyAbbreviation)
    End Sub
    ''' <summary>
    ''' Evento que obtiene el valor de los Documentos/Notas que restan
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub XrLabel22_BeforePrint(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles XrLabel22.BeforePrint
        If dataReport.EntityBankAccountValue > dataReport.ExtractValue Then
            dataReport.NegativeDocumentsPending = dataReport.BankReconciliationAutomaticExtractDetailXpo.ToList().
                                                        Where(Function(x) x.Nature = 2 And Not x.Reconciled).
                                                        Sum(Function(s) s.Value)
        Else
            dataReport.NegativeDocumentsPending = dataReport.BankReconciliationAutomaticExtractDetailXpo.ToList().
                                                        Where(Function(x) x.Nature = 1 And Not x.Reconciled).
                                                        Sum(Function(s) s.Value)
        End If
        XrLabel22.Text = Utils.GetMoneyWithISO4217(dataReport.NegativeDocumentsPending, _currencyAbbreviation)
    End Sub

#End Region
End Class