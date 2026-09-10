#Region "Librerias Improtadas"
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.Data.Xpo
Imports Infrastructure.Data.Xpo.FixedAssetRepository
Imports Domain.Entities
Imports DevExpress.XtraReports.UI
Imports System.Drawing.Printing
Imports DevExpress.XtraReports.Parameters
Imports Presentation.Base
Imports Infrastructure.Data.Xpo.SecurityRepository
Imports Presentation.Reporter


#End Region

Public Class rptAssetTransactions
    Implements IReport

    ''' <summary>
    ''' Variable para inicializar los valores de sesion
    ''' </summary>
    Dim IndigoSessionValues As SessionValues = SessionValues.Instance


    Private INDList As List(Of FixedAssetVReportFixedTransactionReportXpo)
    Public Sub CargarDataSource() Implements IReport.CargarDataSource
        Try
            Dim filtroConsulta As String = "Id = " & ParametrosReporte(0)
            INDList = XpoServiceEx.Instance(IndigoSessionValues.TransactionalContainer).FixedAsset.GetCollection(Of FixedAssetVReportFixedTransactionReportXpo)(Nothing, filtroConsulta)
            Me.DataSource = INDList
        Catch ex As Exception
            MessageIndigo.Show(GetExceptionDetails(ex), MessageType.Errores, Me.Text, Botones.Aceptar, "")
        End Try
    End Sub
    Public Function GetExceptionDetails(exception As Exception) As String
        Dim properties = exception.[GetType]().GetProperties()
        Dim fields = properties.[Select](Function([property]) New With {
            Key .Name = [property].Name,
            Key .Value = [property].GetValue(exception, Nothing)
        }).[Select](Function(x) [String].Format("{0} : {1}", x.Name, If(x.Value IsNot Nothing, x.Value.ToString(), [String].Empty)))
        Return [String].Join(vbLf, fields)
    End Function

    Public Sub CargarImagenes() Implements IReport.CargarImagenes

    End Sub

    Public ReadOnly Property NameReport As String Implements IReport.NameReport
        Get
            Return ""
        End Get
    End Property

    Public Property ParametrosReporte As Object() Implements IReport.ParametrosReporte

    Private Sub rptFixedAssetTransfer_BeforePrint(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles MyBase.BeforePrint
        INDLblCompany.Text = IndigoSessionValues.IndigoCompanyName
        INDLblNitCompany.Text = "Nit:" & IndigoSessionValues.IndigoCompanyNit
        INDUserImp.Text = "Usuario Impresión : " & IndigoSessionValues.UserIndigo & " - " & IndigoSessionValues.UserIndigoName
    End Sub

    ''' <summary>
    '''  Funcion que centraliza de obtención de abreviaturas de moneda
    ''' </summary>
    ''' <param name="generateAccountPayable"></param>
    ''' <param name="currencyId"></param>
    ''' <param name="operatingUnitId"></param>
    ''' <returns></returns>
    Private Function GetCurrencyAbbreviation(generateAccountPayable As Boolean, currencyId As Integer, operatingUnitId As Integer) As String
        If generateAccountPayable Then
            Dim filterString As String = "Id = " & currencyId
            Dim currrencyXpo As CommonCurrencyXpo = XpoServiceEx.Instance(IndigoSessionValues.TransactionalContainer).FixedAsset.GetCollection(Of CommonCurrencyXpo)(Nothing, filterString).FirstOrDefault()
            Return If(String.IsNullOrEmpty(currrencyXpo?.Abbreviation), currrencyXpo?.ISO4217Xpo.CodeAbbreviation, currrencyXpo?.Abbreviation)
        Else
            Dim filterString As String = "OperatingUnitId = " & operatingUnitId
            Dim fixedAssetSetting As SettingFixedAssetXpo = XpoServiceEx.Instance(IndigoSessionValues.TransactionalContainer).FixedAsset.GetCollection(Of SettingFixedAssetXpo)(Nothing, filterString).FirstOrDefault()
            Return If(String.IsNullOrEmpty(fixedAssetSetting?.CurrencyId.Abbreviation), fixedAssetSetting?.CurrencyId.ISO4217Xpo.CodeAbbreviation, fixedAssetSetting?.CurrencyId.Abbreviation)
        End If
    End Function
    Private Sub XrTableCell16_BeforePrint(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles XrTableCell16.BeforePrint
        Dim currentRow As FixedAssetVReportFixedTransactionReportXpo = GetCurrentRow()
        Dim currencyAbbreviation As String = GetCurrencyAbbreviation(currentRow.GenerateAccountPayable, currentRow.CurrencyId, currentRow.OperatingUnitId)
        XrTableCell16.Text = Utils.GetMoneyWithISO4217(currentRow.Neto, currencyAbbreviation)
    End Sub
    Private Sub XrTableCell30_SummaryGetResult(sender As Object, e As SummaryGetResultEventArgs) Handles XrTableCell30.SummaryGetResult
        Dim currentRow As FixedAssetVReportFixedTransactionReportXpo = GetCurrentRow()
        Dim currencyAbbreviation As String = GetCurrencyAbbreviation(currentRow.GenerateAccountPayable, currentRow.CurrencyId, currentRow.OperatingUnitId)
        e.Result = Utils.GetMoneyWithISO4217(e.CalculatedValues.ToEntityList(Of Decimal).Sum(), currencyAbbreviation)
        e.Handled = True
    End Sub
    Private Sub XrTableCell44_BeforePrint(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles XrTableCell44.BeforePrint
        Dim currentRow As FixedAssetVReportFixedTransactionReportXpo = GetCurrentRow()
        Dim currencyAbbreviation As String = GetCurrencyAbbreviation(currentRow.GenerateAccountPayable, currentRow.CurrencyId, currentRow.OperatingUnitId)
        XrTableCell44.Text = Utils.GetMoneyWithISO4217(currentRow.ValueDiscount, currencyAbbreviation)
    End Sub
    Private Sub XrTableCell42_BeforePrint(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles XrTableCell42.BeforePrint
        Dim currentRow As FixedAssetVReportFixedTransactionReportXpo = GetCurrentRow()
        Dim currencyAbbreviation As String = GetCurrencyAbbreviation(currentRow.GenerateAccountPayable, currentRow.CurrencyId, currentRow.OperatingUnitId)
        XrTableCell42.Text = Utils.GetMoneyWithISO4217(currentRow.IVA, currencyAbbreviation)
    End Sub
    Private Sub XrTableCell50_BeforePrint(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles XrTableCell50.BeforePrint
        Dim totalNeto As Decimal = INDList.Sum(Function(x) x.Neto)
        Dim currentRow As FixedAssetVReportFixedTransactionReportXpo = GetCurrentRow()
        Dim currencyAbbreviation As String = GetCurrencyAbbreviation(currentRow.GenerateAccountPayable, currentRow.CurrencyId, currentRow.OperatingUnitId)
        Dim Total As Decimal = totalNeto - currentRow.ValueDiscount + currentRow.IVA
        XrTableCell50.Text = Utils.GetMoneyWithISO4217(Total, currencyAbbreviation)
    End Sub
    Private Sub XrTableCell40_BeforePrint(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles XrTableCell40.BeforePrint
        Dim currentRow As FixedAssetVReportFixedTransactionReportXpo = GetCurrentRow()
        Dim currencyAbbreviation As String = GetCurrencyAbbreviation(currentRow.GenerateAccountPayable, currentRow.CurrencyId, currentRow.OperatingUnitId)
        XrTableCell40.Text = Utils.GetMoneyWithISO4217(currentRow.RetentionSource, currencyAbbreviation)
    End Sub

    Private Sub XrTableCell38_BeforePrint(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles XrTableCell38.BeforePrint
        Dim currentRow As FixedAssetVReportFixedTransactionReportXpo = GetCurrentRow()
        Dim currencyAbbreviation As String = GetCurrencyAbbreviation(currentRow.GenerateAccountPayable, currentRow.CurrencyId, currentRow.OperatingUnitId)
        XrTableCell38.Text = Utils.GetMoneyWithISO4217(currentRow.WithholdingICA, currencyAbbreviation)
    End Sub

    Private Sub XrTableCell36_BeforePrint(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles XrTableCell36.BeforePrint
        Dim currentRow As FixedAssetVReportFixedTransactionReportXpo = GetCurrentRow()
        Dim currencyAbbreviation As String = GetCurrencyAbbreviation(currentRow.GenerateAccountPayable, currentRow.CurrencyId, currentRow.OperatingUnitId)
        XrTableCell36.Text = Utils.GetMoneyWithISO4217(currentRow.WithholdingTax, currencyAbbreviation)
    End Sub

    Private Sub XrTableCell34_BeforePrint(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles XrTableCell34.BeforePrint
        Dim currentRow As FixedAssetVReportFixedTransactionReportXpo = GetCurrentRow()
        Dim currencyAbbreviation As String = GetCurrencyAbbreviation(currentRow.GenerateAccountPayable, currentRow.CurrencyId, currentRow.OperatingUnitId)
        XrTableCell34.Text = Utils.GetMoneyWithISO4217(currentRow.RetentionOther, currencyAbbreviation)
    End Sub

    Private Sub XrTableCell32_BeforePrint(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles XrTableCell32.BeforePrint
        Dim currentRow As FixedAssetVReportFixedTransactionReportXpo = GetCurrentRow()
        Dim currencyAbbreviation As String = GetCurrencyAbbreviation(currentRow.GenerateAccountPayable, currentRow.CurrencyId, currentRow.OperatingUnitId)
        XrTableCell32.Text = Utils.GetMoneyWithISO4217(currentRow.DeductionOther, currencyAbbreviation)
    End Sub
    Private Sub XrTableCell46_BeforePrint(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles XrTableCell46.BeforePrint
        Dim totalNeto As Decimal = INDList.Sum(Function(x) x.Neto)
        Dim currentRow As FixedAssetVReportFixedTransactionReportXpo = GetCurrentRow()
        Dim currencyAbbreviation As String = GetCurrencyAbbreviation(currentRow.GenerateAccountPayable, currentRow.CurrencyId, currentRow.OperatingUnitId)
        Dim Total As Decimal = totalNeto - currentRow.ValueDiscount + currentRow.IVA - currentRow.RetentionSource - currentRow.WithholdingICA - currentRow.WithholdingTax - currentRow.RetentionOther - currentRow.DeductionOther
        XrTableCell46.Text = Utils.GetMoneyWithISO4217(Total, currencyAbbreviation)
    End Sub


End Class