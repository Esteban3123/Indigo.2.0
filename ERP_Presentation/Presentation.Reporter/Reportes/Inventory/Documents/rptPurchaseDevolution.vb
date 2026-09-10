#Region "Librerias Improtadas"
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.Data.Xpo
Imports Infrastructure.Data.Xpo.InventoryRepository
Imports Domain.Entities
Imports DevExpress.XtraReports.UI
Imports System.Drawing.Printing
Imports Infrastructure.Data.Xpo.SecurityRepository
Imports DevExpress.XtraReports.Parameters
Imports System.Globalization

#End Region

Public Class rptPurchaseDevolution
    Implements IReport

    ''' <summary>
    ''' Variable para inicializar los valores de sesion
    ''' </summary>
    Dim IndigoSessionValues As SessionValues = SessionValues.Instance
    Private INDList As List(Of InventoryEntranceVoucherDevolutionDetailReportXpo)
    Private CurrencyAbbreviation As String = SessionValues.Instance.CurrencyISO4217
    Dim CurrencyName As String
    Dim CurrencyDecimal As String

    Public Sub CargarDataSource() Implements IReport.CargarDataSource
        Dim filtroConsulta As String = "EntranceVoucherDevolutionId = " & ParametrosReporte(0) & " AND Quantity != 0"

        INDList = XpoServiceEx.Instance(IndigoSessionValues.TransactionalContainer).BillingService.GetCollection(Of InventoryEntranceVoucherDevolutionDetailReportXpo)(Nothing, filtroConsulta)

        If INDList.Count > 0 Then
            CurrencyName = UCase(CType(INDList(0), InventoryEntranceVoucherDevolutionDetailReportXpo)?.EntranceVoucherDevolutionId.EntranceVoucherId.Currency.ISO4217Xpo.CurrencyName) 'Currency.ISO4217Xpo.CurrencyName)
            CurrencyDecimal = UCase(CType(INDList(0), InventoryEntranceVoucherDevolutionDetailReportXpo)?.EntranceVoucherDevolutionId.EntranceVoucherId.Currency.ISO4217Xpo.CodeAbbreviation)
            Dim INDNameUser = CType(INDList(0), InventoryEntranceVoucherDevolutionDetailReportXpo).EntranceVoucherDevolutionId.CreationUser.Trim()
            Dim INDListUser = XpoServiceEx.Instance(IndigoSessionValues.SecurityContainer).SecurityService.GetCollection(Of UserXpo)(Nothing, "UserCode = '" & INDNameUser & "'")

            If INDListUser IsNot Nothing Then
                Dim INDCodName = CType(INDListUser(0), UserXpo).CodeName.Trim
                Me.INDUserCreate.Text = INDCodName
            End If
        End If
        Me.DataSource = INDList
    End Sub

    Public Sub CargarImagenes() Implements IReport.CargarImagenes

    End Sub

    Public ReadOnly Property NameReport As String Implements IReport.NameReport
        Get
            Return ""
        End Get
    End Property

    Public Property ParametrosReporte As Object() Implements IReport.ParametrosReporte

    Private Sub rptPurchaseDevolution_BeforePrint(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles MyBase.BeforePrint
        If Me.Parameters.Count > 0 And Me.Parameters(0).Value > 0 Then
            Dim ParametrosFilter As ParameterCollection = Me.Parameters
            ParametrosReporte = New Object() {ParametrosFilter("INDSubIdEntranceVoucherDevolution").Value}
            CargarDataSource()
        End If

        'Se obtiene la cultura para la moneda que se maneja en el reporte
        Dim _culture As CultureInfo = CultureInfo.CurrentCulture.Clone()

        If INDList IsNot Nothing AndAlso INDList?.Any() Then
            CurrencyAbbreviation = If(String.IsNullOrEmpty(INDList?.FirstOrDefault?.EntranceVoucherDevolutionId.EntranceVoucherId.CurrencyAbbreviation),
                                        CurrencyAbbreviation, INDList?.FirstOrDefault?.EntranceVoucherDevolutionId.EntranceVoucherId.CurrencyAbbreviation)
            _culture.NumberFormat = CurrencyAbbreviation.GetNumberFormat
        End If

        Dim CurrentValue As Decimal = Convert.ToDecimal(GetCurrentColumnValue("INDTotalValue"))
        Dim _integerPart As Int64 = Int(CurrentValue)
        Dim _decimalPart As Integer = Strings.Right(Format(CurrentValue - _integerPart, "0.00"), 2)

        INDLblCompany.Text = IndigoSessionValues.IndigoCompanyName
        INDLblNitCompany.Text = "Nit:" & IndigoSessionValues.IndigoCompanyNit
        INDUserImp.Text = "Usuario Impresión : " & IndigoSessionValues.UserIndigo & " - " & IndigoSessionValues.UserIndigoName
        XrTableCell60.Text = String.Format("{0} {1}{2}",
                                                  Utils.Num2Text(_integerPart).ToString,
                                                  CurrencyName,
                                                  If(_decimalPart > 0, $", CON {Utils.Num2Text(_decimalPart).ToString } {Utils.ListDecimalCurrency(CurrencyDecimal)}", ""))
    End Sub

    Private Sub XrTableCell67_BeforePrint(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles XrTableCell67.BeforePrint
        Dim row = GetCurrentRow()
        Dim currencyAbb = DirectCast(row, Infrastructure.Data.Xpo.InventoryRepository.InventoryEntranceVoucherDevolutionDetailReportXpo)?.EntranceVoucherDevolutionId?.EntranceVoucherId?.CurrencyAbbreviation
        XrTableCell67.Text = Utils.GetMoneyWithISO4217(XrTableCell67.Text, If(String.IsNullOrEmpty(currencyAbb),
                                             IndigoSessionValues.CurrencyISO4217, currencyAbb))
    End Sub

    Private Sub XrTableCell68_BeforePrint(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles XrTableCell68.BeforePrint
        Dim row = GetCurrentRow()
        Dim currencyAbb = DirectCast(row, Infrastructure.Data.Xpo.InventoryRepository.InventoryEntranceVoucherDevolutionDetailReportXpo)?.EntranceVoucherDevolutionId?.EntranceVoucherId?.CurrencyAbbreviation
        XrTableCell68.Text = Utils.GetMoneyWithISO4217(XrTableCell68.Text, If(String.IsNullOrEmpty(currencyAbb),
                                             IndigoSessionValues.CurrencyISO4217, currencyAbb))
    End Sub

    Private Sub XrTableCell40_BeforePrint(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles XrTableCell40.BeforePrint
        Dim value = INDList?.FirstOrDefault?.EntranceVoucherDevolutionId?.Value
        XrTableCell40.Text = Utils.GetMoneyWithISO4217(If(String.IsNullOrEmpty(value), "0.00", value), If(String.IsNullOrEmpty(CurrencyAbbreviation),
                                             IndigoSessionValues.CurrencyISO4217, CurrencyAbbreviation))
    End Sub

    Private Sub XrTableCell42_BeforePrint(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles XrTableCell42.BeforePrint
        Dim value = INDList?.FirstOrDefault?.EntranceVoucherDevolutionId?.ValueDiscount
        XrTableCell42.Text = Utils.GetMoneyWithISO4217(If(String.IsNullOrEmpty(value), "0.00", value), If(String.IsNullOrEmpty(CurrencyAbbreviation),
                                             IndigoSessionValues.CurrencyISO4217, CurrencyAbbreviation))
    End Sub

    Private Sub XrTableCell44_BeforePrint(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles XrTableCell44.BeforePrint
        Dim value = INDList?.FirstOrDefault?.EntranceVoucherDevolutionId?.ValueTax
        XrTableCell44.Text = Utils.GetMoneyWithISO4217(If(String.IsNullOrEmpty(value), "0.00", value), If(String.IsNullOrEmpty(CurrencyAbbreviation),
                                             IndigoSessionValues.CurrencyISO4217, CurrencyAbbreviation))
    End Sub

    Private Sub XrTableCell46_BeforePrint(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles XrTableCell46.BeforePrint
        Dim value = INDList?.FirstOrDefault?.EntranceVoucherDevolutionId?.WithholdingTax
        XrTableCell46.Text = Utils.GetMoneyWithISO4217(If(String.IsNullOrEmpty(value), "0.00", value), If(String.IsNullOrEmpty(CurrencyAbbreviation),
                                             IndigoSessionValues.CurrencyISO4217, CurrencyAbbreviation))
    End Sub

    Private Sub XrTableCell48_BeforePrint(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles XrTableCell48.BeforePrint
        Dim value = INDList?.FirstOrDefault?.EntranceVoucherDevolutionId?.WithholdingICA
        XrTableCell48.Text = Utils.GetMoneyWithISO4217(If(String.IsNullOrEmpty(value), "0.00", value), If(String.IsNullOrEmpty(CurrencyAbbreviation),
                                             IndigoSessionValues.CurrencyISO4217, CurrencyAbbreviation))
    End Sub

    Private Sub XrTableCell50_BeforePrint(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles XrTableCell50.BeforePrint
        Dim value = INDList?.FirstOrDefault?.EntranceVoucherDevolutionId?.RetentionSource
        XrTableCell50.Text = Utils.GetMoneyWithISO4217(If(String.IsNullOrEmpty(value), "0.00", value), If(String.IsNullOrEmpty(CurrencyAbbreviation),
                                             IndigoSessionValues.CurrencyISO4217, CurrencyAbbreviation))
    End Sub
    Private Sub XrTableCell52_BeforePrint(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles XrTableCell52.BeforePrint
        Dim value = INDList?.FirstOrDefault?.EntranceVoucherDevolutionId?.RetentionOther
        XrTableCell52.Text = Utils.GetMoneyWithISO4217(If(String.IsNullOrEmpty(value), "0.00", value), If(String.IsNullOrEmpty(CurrencyAbbreviation),
                                             IndigoSessionValues.CurrencyISO4217, CurrencyAbbreviation))
    End Sub

    Private Sub XrTableCell54_BeforePrint(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles XrTableCell54.BeforePrint
        Dim value = INDList?.FirstOrDefault?.EntranceVoucherDevolutionId?.DeductionOther
        XrTableCell54.Text = Utils.GetMoneyWithISO4217(If(String.IsNullOrEmpty(value), "0.00", value), If(String.IsNullOrEmpty(CurrencyAbbreviation),
                                             IndigoSessionValues.CurrencyISO4217, CurrencyAbbreviation))
    End Sub

    Private Sub XrTableCell56_BeforePrint(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles XrTableCell56.BeforePrint
        Dim value = INDList?.FirstOrDefault?.EntranceVoucherDevolutionId?.FreightValue
        XrTableCell56.Text = Utils.GetMoneyWithISO4217(If(String.IsNullOrEmpty(value), "0.00", value), If(String.IsNullOrEmpty(CurrencyAbbreviation),
                                             IndigoSessionValues.CurrencyISO4217, CurrencyAbbreviation))
    End Sub

    Private Sub XrTableCell58_BeforePrint(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles XrTableCell58.BeforePrint
        Dim value = INDList?.FirstOrDefault?.EntranceVoucherDevolutionId?.TotalValue
        XrTableCell58.Text = Utils.GetMoneyWithISO4217(If(String.IsNullOrEmpty(value), "0.00", value), If(String.IsNullOrEmpty(CurrencyAbbreviation),
                                             IndigoSessionValues.CurrencyISO4217, CurrencyAbbreviation))
    End Sub

    Private Sub XrTableCell62_BeforePrint(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles XrTableCell62.BeforePrint
        Dim value = INDList?.FirstOrDefault?.EntranceVoucherDevolutionId?.FreightIVAValue
        XrTableCell62.Text = Utils.GetMoneyWithISO4217(If(String.IsNullOrEmpty(value), "0.00", value), If(String.IsNullOrEmpty(CurrencyAbbreviation),
                                             IndigoSessionValues.CurrencyISO4217, CurrencyAbbreviation))
    End Sub
End Class