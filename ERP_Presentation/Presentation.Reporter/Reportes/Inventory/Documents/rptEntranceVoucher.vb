#Region "Librerias Improtadas"
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.Data.Xpo
Imports Infrastructure.Data.Xpo.InventoryRepository
Imports Domain.Entities
Imports DevExpress.XtraReports.UI
Imports System.Drawing.Printing
Imports DevExpress.XtraReports.Parameters
Imports Infrastructure.Data.Xpo.SecurityRepository
Imports System.Globalization

#End Region

Public Class rptEntranceVoucher
    Implements IReport

    ''' <summary>
    ''' Variable para inicializar los valores de sesion
    ''' </summary>
    Dim IndigoSessionValues As SessionValues = SessionValues.Instance
    Private INDList As List(Of InventoryEntranceVoucherReportXpo)
    Private CurrencyAbbreviation As String = SessionValues.Instance.CurrencyISO4217
    Private CurrencyName As String
    Dim CurrencyDecimal As String
    Public Sub CargarDataSource() Implements IReport.CargarDataSource
        Dim filtroConsulta As String = "Id = " & ParametrosReporte(0)

        INDList = XpoServiceEx.Instance(IndigoSessionValues.TransactionalContainer).BillingService.GetCollection(Of InventoryEntranceVoucherReportXpo)(Nothing, filtroConsulta)
        If INDList.Count > 0 Then

            CurrencyName = UCase(CType(INDList(0), InventoryEntranceVoucherReportXpo)?.Currency.ISO4217Xpo.CurrencyName)
            CurrencyDecimal = UCase(CType(INDList(0), InventoryEntranceVoucherReportXpo)?.Currency.ISO4217Xpo.CodeAbbreviation)
            Dim INDNameUser = CType(INDList(0), InventoryEntranceVoucherReportXpo).CreationUser.Trim()

            Dim INDListUser = XpoServiceEx.Instance(IndigoSessionValues.SecurityContainer).SecurityService.GetCollection(Of UserXpo)(Nothing, "UserCode = '" & INDNameUser & "'")

            If INDListUser IsNot Nothing AndAlso INDListUser.Count() > 0 Then
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

    Private Sub rptEntranceVoucher_BeforePrint(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles MyBase.BeforePrint
        If Me.Parameters.Count > 0 And Me.Parameters(0).Value > 0 Then
            Dim ParametrosFilter As ParameterCollection = Me.Parameters
            ParametrosReporte = New Object() {ParametrosFilter("EntranceVoucherId").Value}
            CargarDataSource()
        End If

        Dim _culture As CultureInfo = CultureInfo.CurrentCulture.Clone()
        If INDList IsNot Nothing AndAlso INDList?.Any() Then
            CurrencyAbbreviation = If(String.IsNullOrEmpty(INDList?.FirstOrDefault?.CurrencyAbbreviation),
                                        CurrencyAbbreviation, INDList?.FirstOrDefault?.CurrencyAbbreviation)
            _culture.NumberFormat = CurrencyAbbreviation.GetNumberFormat
            ApplyLocalization(_culture)
        End If

        'obtiene la parte escrita del valor
        'Se obtiene el nombre de la moneda desde la tabla de iso4217
        Dim _integerPart As Integer = Int(Convert.ToDecimal(GetCurrentColumnValue("TotalValue")))
        Dim _decimalPart As Integer = Strings.Right(Format(Convert.ToDecimal(GetCurrentColumnValue("TotalValue")) - _integerPart, "0.00"), 2)

        Me.XrTableCell60.Text = String.Format("{0} {1}{2}",
                                                  Utils.Num2Text(_integerPart).ToString,
                                                  CurrencyName,
                                                  If(_decimalPart > 0, $", CON {Utils.Num2Text(_decimalPart).ToString } {Utils.ListDecimalCurrency(CurrencyDecimal)}", ""))

        INDLblCompany.Text = IndigoSessionValues.IndigoCompanyName
        INDLblNitCompany.Text = "Nit:" & IndigoSessionValues.IndigoCompanyNit
        Me.INDUserImp.Text = "Usuario Impresión : " & IndigoSessionValues.UserIndigo & " - " & IndigoSessionValues.UserIndigoName
    End Sub
End Class