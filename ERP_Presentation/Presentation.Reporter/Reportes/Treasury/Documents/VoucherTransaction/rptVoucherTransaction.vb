#Region "Imports"
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.Data.Xpo
Imports Infrastructure.Data.Xpo.TreasuryRepository
Imports DevExpress.XtraReports.UI
Imports DevExpress.XtraReports.Parameters
Imports Infrastructure.Data.Xpo.SecurityRepository
Imports System.Globalization
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Resources
Imports Infrastructure.Data.Xpo.AccountingRepository

#End Region

Public Class rptVoucherTransaction
    Implements IReport

    ''' <summary>
    ''' Variable para inicializar los valores de sesion.
    ''' </summary>
    Dim IndigoSessionValues As SessionValues = SessionValues.Instance
    Private INDUser As List(Of UserXpo)
    Private INDSettingTreasury As List(Of TreasurySettingsTreasuryXpo)

    ''' <summary>
    ''' propiedad que almacena los tipo de registro del IVA  de companysettings
    ''' </summary>
    ''' <returns>1- IVA Costo (Control Fiscal)
    '''             2- IVA Descontable
    '''             3- IVA Mixto
    '''             4- IVA Costo
    '''             NULL - no parametrizado</returns>
    Private Property CompanyTaxRegistration As Byte?

    Public Sub CargarDataSource() Implements IReport.CargarDataSource

        Dim filtroConsulta As String = "Id = " & ParametrosReporte(0)
        Dim INDList = XpoServiceEx.Instance(IndigoSessionValues.TransactionalContainer).TreasuryService.GetCollection(Of TreasuryVoucherTransactionXpo)(Nothing, filtroConsulta)
        Dim INDCodeUser = CType(INDList(0), TreasuryVoucherTransactionXpo).CreationUser.Trim()

        INDUser = XpoServiceEx.Instance(IndigoSessionValues.SecurityContainer).TreasuryService.GetCollection(Of Infrastructure.Data.Xpo.SecurityRepository.UserXpo)(Nothing, "UserCode = '" & INDCodeUser & "'")
        CompanyTaxRegistration = XpoServiceEx.Instance(IndigoSessionValues.TransactionalContainer).AccountingService.GetXPOObject(Of CompanySettingsXpo)("TaxRegistration > 0")?.TaxRegistration

        'Ocultar el Beneficiario si llega vacia
        'fila de beneficiario tercero
        Dim table1 As XRTable = CType(XrTable3, XRTable)
        'fila de beneficiario
        Dim table2 As XRTable = CType(XrTable24, XRTable)

        Dim tableCashRegisters As XRTable = CType(XrTable19, XRTable)
        Dim tableEntityBankAccounts As XRTable = CType(XrTable21, XRTable)

        If (CType(INDList(0), TreasuryVoucherTransactionXpo).VoucherClass = 3) Then
            table1.Rows.Remove(XrTableRow3)
            table2.Rows.Remove(XrTableRow25)
        Else
            If (CType(INDList(0), TreasuryVoucherTransactionXpo).Beneficiary Is Nothing OrElse CType(INDList(0), TreasuryVoucherTransactionXpo).Beneficiary.Trim() = "") Then
                XrTable24.Visible = False
            Else
                XrTable3.Visible = False
            End If
        End If

        'Tercero del movimiento según la parametrización en Tesoreria
        If (CType(INDList(0), TreasuryVoucherTransactionXpo).VoucherClass = 1) Then
            INDSettingTreasury = XpoServiceEx.Instance(IndigoSessionValues.TransactionalContainer).TreasuryService.GetCollection(Of TreasurySettingsTreasuryXpo)(Nothing, "IdOperatingUnit = " & IndigoSessionValues.IndigoOperatingUnitId)

            If (CType(INDList(0), TreasuryVoucherTransactionXpo).IdCashRegister IsNot Nothing) Then
                If (CType(INDSettingTreasury(0), TreasurySettingsTreasuryXpo).GetThirdPartyCashRegister = 2) Then
                    XrTableCell31.Visible = False
                    XrLabel1.Visible = True
                Else
                    XrTableCell31.Visible = True
                    XrLabel1.Visible = False
                End If
            Else
                If (CType(INDSettingTreasury(0), TreasurySettingsTreasuryXpo).GetThirdPartyBank = 2) Then
                    XrTableCell66.Visible = False
                    XrLabel1.Visible = True
                Else
                    XrTableCell66.Visible = True
                    XrLabel1.Visible = False
                End If
            End If
        End If

        Me.DataSource = INDList
        Dim ivadetail = New List(Of VoucherTransactionDetailAccountInfo)
        For Each Itemc In INDList

            Dim GroupConcept = (From x In Itemc?.TreasuryVoucherTransactionDetailsXpo.Where(Function(s) s.discountableIVA.HasValue AndAlso s.ValueIVA.HasValue)
                                Group By Key = New With {Key .IdMainAccountNumber = x.IdMainAccount?.Number,
                                                   Key .IdThirdPartyNit = x.IdThirdParty?.Nit,
                                                   Key .discountableIVA = x.discountableIVA,
                                                   Key .IdExpenseConceptCode = x.IdExpenseConcept?.Code,
                                                   Key .IdExpenseConceptNature = x?.IdExpenseConcept?.Nature,
                                                   Key .IdExpenseConceptCodeDescription = x?.IdExpenseConcept?.CodeDescription,
                                                   Key .GeneralLedgerIVAName = x.GeneralLedgerIVAXpo?.Name,
                                                   Key .AccountPurchaseServiceNumber = x?.GeneralLedgerIVAXpo?.AccountPurchaseService?.Number,
                                                   Key .AccountDebitControlFiscalName = x?.GeneralLedgerIVAXpo?.AccountDebitControlFiscal?.Name,
                                                   Key .AccountDebitControlFiscalNumber = x?.GeneralLedgerIVAXpo?.AccountDebitControlFiscal?.Number,
                                                   Key .AccountCreditControlFiscalNumber = x?.GeneralLedgerIVAXpo?.AccountCreditControlFiscal?.Number,
                                                   Key .AccountCreditControlFiscalName = x?.GeneralLedgerIVAXpo?.AccountCreditControlFiscal?.Name,
                                                   Key .TaxRegistration = x?.TaxRegistration
                                                     } Into Group
                                Select New With {.Key = Key, .ValueIVA = Group.Sum(Function(h) h.ValueIVA)})

            GroupConcept.ToList()?.ForEach(Sub(item)
                                               If item.Key.TaxRegistration = 2 Then
                                                   If item.ValueIVA > 0 Then
                                                       ivadetail.Add(New VoucherTransactionDetailAccountInfo With {
                                                                                                                    .ConceptName = $"{item?.Key.IdExpenseConceptCode} - {item?.Key.GeneralLedgerIVAName}",
                                                                                                                    .MainAccountCodeName = item?.Key.AccountPurchaseServiceNumber,
                                                                                                                    .CostCenterCodeName = "",
                                                                                                                    .NatureName = If(item?.Key.IdExpenseConceptNature = 1, "Débito", "Crédito"),
                                                                                                                    .ValueTotalConcept = item.ValueIVA,
                                                                                                                    .Nature = item?.Key.IdExpenseConceptNature,
                                                                                                                    .DebitValue = If(.Nature = 1, .ValueTotalConcept, 0),
                                                                                                                    .CreditValue = If(.Nature = 2, .ValueTotalConcept, 0),
                                                                                                                    .ThirdPartyNitName = item.Key.IdThirdPartyNit
                                                                                                                })
                                                   End If

                                               ElseIf item.Key.TaxRegistration = 1 Then
                                                   If item.ValueIVA > 0 Then
                                                       ivadetail.Add(New VoucherTransactionDetailAccountInfo With {
                                                                                                                    .ConceptName = $"{item?.Key.IdExpenseConceptCode} - {item?.Key.AccountDebitControlFiscalName}",
                                                                                                                    .MainAccountCodeName = item?.Key.AccountDebitControlFiscalNumber,
                                                                                                                    .CostCenterCodeName = "",
                                                                                                                    .NatureName = ResourceManager.GetString("AccountNatureDebit"),
                                                                                                                    .DebitValue = item.ValueIVA,
                                                                                                                    .Nature = 1,
                                                                                                                    .ThirdPartyNitName = item.Key.IdThirdPartyNit
                                                                                                                })

                                                       ivadetail.Add(New VoucherTransactionDetailAccountInfo With {
                                                                                                                    .ConceptName = $"{item?.Key.IdExpenseConceptCode} - { item?.Key.AccountCreditControlFiscalName}",
                                                                                                                    .MainAccountCodeName = item?.Key.AccountCreditControlFiscalNumber,
                                                                                                                    .CostCenterCodeName = "",
                                                                                                                    .NatureName = ResourceManager.GetString("AccountNatureCredit"),
                                                                                                                    .CreditValue = item.ValueIVA,
                                                                                                                    .Nature = 2,
                                                                                                                    .ThirdPartyNitName = item.Key.IdThirdPartyNit
                                                                                                                    })
                                                   End If
                                               ElseIf item.Key.TaxRegistration = 4 Then
                                                   If item.ValueIVA > 0 Then
                                                       ivadetail.Add(New VoucherTransactionDetailAccountInfo With {
                                                                                                                    .ConceptName = $"{item?.Key.IdExpenseConceptCodeDescription}",
                                                                                                                    .MainAccountCodeName = item?.Key.IdMainAccountNumber,
                                                                                                                    .CostCenterCodeName = "",
                                                                                                                    .NatureName = If(item?.Key.IdExpenseConceptNature = 1, "Débito", "Crédito"),
                                                                                                                    .ValueTotalConcept = item.ValueIVA,
                                                                                                                    .Nature = item?.Key.IdExpenseConceptNature,
                                                                                                                    .DebitValue = If(.Nature = 1, .ValueTotalConcept, 0),
                                                                                                                    .CreditValue = If(.Nature = 2, .ValueTotalConcept, 0),
                                                                                                                    .ThirdPartyNitName = item?.Key.IdThirdPartyNit
                                                                                                                })
                                                   End If
                                               End If
                                           End Sub)
        Next
        'Filtrar items con valores en 0 para no mostrarlos en el reporte
        ivadetail = ivadetail.Where(Function(x) x.DebitValue <> 0 OrElse x.CreditValue <> 0).ToList()
        Me.DetailReport4.DataSource = ivadetail
    End Sub

    Public Sub CargarImagenes() Implements IReport.CargarImagenes

    End Sub

    Public ReadOnly Property NameReport As String Implements IReport.NameReport
        Get
            Return ""
        End Get
    End Property

    Public Property ParametrosReporte As Object() Implements IReport.ParametrosReporte

    Private Sub rptVoucherTransaction_BeforePrint(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles MyBase.BeforePrint
        'Dim filtroConsulta As String = "Id = " & ParametrosReporte(0)
        If Me.Parameters.Count > 0 And Me.Parameters(0).Value > 0 Then
            Dim ParametrosFilter As ParameterCollection = Me.Parameters
            ParametrosReporte = New Object() {ParametrosFilter("INDIdScheduleSubreport").Value}
            CargarDataSource()
        End If

        Me.Parameters("INDTaxRegistration").Value = Me.CompanyTaxRegistration

        '---Se establece el numbert fortmat al reporte dependiendo de la moneda
        Dim _culture As CultureInfo = CultureInfo.CurrentCulture.Clone()
        Dim CurrencyAbbreviation As String = TryCast(Me.DataSource, List(Of TreasuryVoucherTransactionXpo))?.FirstOrDefault?.CurrencyAbbreviation
        Dim currencyName As String = TryCast(Me.DataSource, List(Of TreasuryVoucherTransactionXpo))?.FirstOrDefault?.CommonCurrency?.ISO4217Xpo?.CurrencyName
        _culture.NumberFormat = CurrencyAbbreviation.GetNumberFormat
        ApplyLocalization(_culture)

        Me.INDLblUserPrint.Text = "Usuario Impresión : " & IndigoSessionValues.UserIndigo & " - " & IndigoSessionValues.UserIndigoName
        Me.INDLblCompany.Text = IndigoSessionValues.IndigoCompanyName
        Me.INDLblNitCompany.Text = "Nit : " & IndigoSessionValues.IndigoCompanyNit

        If INDUser.Count() > 0 Then
            INDLblCreationUser.Text = INDUser(0).CodeName
        End If

        Dim integerPart As Long = Int(Convert.ToDecimal(GetCurrentColumnValue("Value")))
        Dim decimalPart As Integer = Strings.Right(Format(Convert.ToDecimal(GetCurrentColumnValue("Value")) - integerPart, "0.00"), 2)

        INDLblNumLetters.Text = String.Format("{0} {1}{2}", Utils.Num2Text(integerPart).ToString,
                                                            If(String.IsNullOrEmpty(currencyName), CurrencyAbbreviation, currencyName.ToUpper),
                                                            If(decimalPart > 0, $", CON {Utils.Num2Text(decimalPart).ToString } {Utils.ListDecimalCurrency(CurrencyAbbreviation)}", ""))

        Dim treasurySequence = XpoServiceEx.Instance(IndigoSessionValues.TransactionalContainer).TreasuryService.GetCollection(Of TreasurySequenceXpo)(Nothing, "IdForm = '636'")
        If treasurySequence IsNot Nothing Then
            INDPrScope.Value = treasurySequence(0).Scope
        End If

    End Sub

    Private Sub XrTableCell68_BeforePrint(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles XrTableCell68.BeforePrint
        Dim currencyAbbreviation = TryCast(Me.GetCurrentRow, TreasuryVoucherTransactionXpo)?.CurrencyAbbreviation
        XrTableCell68.Text = Utils.GetMoneyWithISO4217(0.00, currencyAbbreviation)
    End Sub

    Private Sub XrTableCell33_BeforePrint(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles XrTableCell33.BeforePrint
        Dim currencyAbbreviation = TryCast(Me.GetCurrentRow, TreasuryVoucherTransactionXpo)?.CurrencyAbbreviation
        XrTableCell33.Text = Utils.GetMoneyWithISO4217(0.00, currencyAbbreviation)
    End Sub

    ''' <summary>
    ''' evento que se ejecuta antes de pintar lo datos para determinar si se debe mostrar o no
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub DetailReport4_BeforePrint(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles DetailReport4.BeforePrint
        Dim datasourcelistIva = TryCast(Me.DetailReport4.DataSource, List(Of VoucherTransactionDetailAccountInfo))
        If datasourcelistIva Is Nothing OrElse Not datasourcelistIva?.Any() Then
            e.Cancel = True
        End If
    End Sub
End Class