#Region "Libraries"
Imports Infrastructure.CrossCutting
Imports Domain.Base
Imports Domain.Base.Entities
Imports System.Text
Imports Infrastructure.CrossCutting.Resources
Imports Infrastructure.CrossCutting.Base
Imports Microsoft.SqlServer.Server
Imports Domain.Base.Entities.Enums.ElectronicDocuments.v1_6
Imports System.Text.RegularExpressions


#End Region

Public Class TreasuryServices
    Implements ITreasuryServices


#Region "Fields"
    ''' <summary>
    ''' constante con el nombre del modulo
    ''' </summary>
    Private Const MODULE_NAME = "Treasury"
    Private _cashRegisterRepository As ICashRegisterRepository
    ''' <summary>
    ''' repositorio de banco
    ''' </summary>
    ''' <remarks></remarks>
    Private _entityBankAccountRepository As IEntityBankAccountRepository
    ''' <summary>
    ''' repositorio de cuotas de cuentas por cobrar
    ''' </summary>
    ''' <remarks></remarks>
    Private _accountReceivableRepository As IAccountReceivableRepository
    ''' <summary>
    ''' repositrio de anticipo
    ''' </summary>
    ''' <remarks></remarks>
    Private _advancePaymentRepository As IMoneyAdvanceRepository
    ''' <summary>
    ''' repositorio de conceptos de egreso
    ''' </summary>
    Private _expenseConceptRepository As IExpenseConceptRepository
    ''' <summary>
    ''' repositorio de detalle de egresos
    ''' </summary>
    Private _dischargeBillRepository As IDischargeBillRepository
    ''' <summary>
    ''' repositorio de cuentas por pagar
    ''' </summary>
    Private _accountPayableRepository As IAccountPayableRepository
    ''' <summary>
    ''' repositorio de avances de cartera
    ''' </summary>
    Private _portfolioAdvanceRepository As IPortfolioAdvanceRepository
    ''' <summary>
    ''' repositorio de reembolsos
    ''' </summary>
    Private _refundRepository As IRefundRepository
    ''' <summary>
    ''' The _close month repository
    ''' </summary>
    Private _closeMonthRepository As ICloseMonthRepository
    ''' <summary>
    ''' The _main account repository
    ''' </summary>
    Private _mainAccountRepository As IPUCRepository
    ''' <summary>
    ''' The _supplier repository
    ''' </summary>
    Private _supplierRepository As Domain.Entities.ISupplierRepository
    ''' <summary>
    ''' repositorio de cruce de cuentas CxP
    ''' </summary>
    Private _crossingAccountDetailCxPRepository As ICrossingAccountDetailCxPRepository
    ''' <summary>
    ''' repositorio de cruce de cuentas CxC
    ''' </summary>
    Private _crossingAccountDetailCxCRepository As ICrossingAccountDetailCxCRepository
    Private _crossingAccountDetailOtherConceptsRepository As ICrossingAccountDetailOtherConceptsRepository
    ''' <summary>
    ''' The _voucher transaction repository
    ''' </summary>
    Private _voucherTransactionRepository As IVoucherTransactionRepository
    Private _cashRecepitRepository As ICashReceiptsRepository
    Private _crossingAccountRepository As ICrossingAccountRepository
    Private _bankRepository As IBankRepository
    Private _accountReceivableAccountingRepository As IAccountReceivableAccountingRepository
    Private _cashFlowConceptRepository As ICashFlowConceptRepository
    Private _thirdPartyRepository As IThirdPartyRepository
    Private _currencyRepository As ICurrencyRepository
    Private _companySettingRepository As ICompanySettingsRepository
    Private _settingsAccountRepository As ISettingsAccountRepository
#End Region

#Region "Builder"
    Public Sub New(cashRegisterRepository As ICashRegisterRepository, entityBankAccountRepository As IEntityBankAccountRepository, accountReceivableRepository As IAccountReceivableRepository,
                   advancePaymentRepository As IMoneyAdvanceRepository, expenseConceptRepository As IExpenseConceptRepository, dischargeBillRepository As IDischargeBillRepository,
                   accountPayableRepository As IAccountPayableRepository, portfolioAdvanceRepository As IPortfolioAdvanceRepository, refundRepository As IRefundRepository,
                   closeMonthRepository As ICloseMonthRepository, mainAccountRepository As IPUCRepository, supplierRepository As Domain.Entities.ISupplierRepository,
                   crossingAccountDetailCxPRepository As ICrossingAccountDetailCxPRepository, crossingAccountDetailCxCRepository As ICrossingAccountDetailCxCRepository,
                   voucherTransactionRepository As IVoucherTransactionRepository, cashRecepitRepository As ICashReceiptsRepository, bankRepository As IBankRepository,
                   crossingAccountDetailOtherConceptsRepository As ICrossingAccountDetailOtherConceptsRepository, accountReceivableAccountingRepository As IAccountReceivableAccountingRepository,
                   crossingAccountRepository As ICrossingAccountRepository, cashFlowConceptRepository As ICashFlowConceptRepository, thirdPartyRepository As IThirdPartyRepository,
                   currencyRepository As ICurrencyRepository, companySettingsRepository As ICompanySettingsRepository, settingsAccountRepository As ISettingsAccountRepository)
        _cashRegisterRepository = cashRegisterRepository
        _entityBankAccountRepository = entityBankAccountRepository
        _accountReceivableRepository = accountReceivableRepository
        _advancePaymentRepository = advancePaymentRepository
        _expenseConceptRepository = expenseConceptRepository
        _dischargeBillRepository = dischargeBillRepository
        _accountPayableRepository = accountPayableRepository
        _portfolioAdvanceRepository = portfolioAdvanceRepository
        _refundRepository = refundRepository
        _closeMonthRepository = closeMonthRepository
        _mainAccountRepository = mainAccountRepository
        _supplierRepository = supplierRepository
        _crossingAccountDetailCxPRepository = crossingAccountDetailCxPRepository
        _crossingAccountDetailCxCRepository = crossingAccountDetailCxCRepository
        _voucherTransactionRepository = voucherTransactionRepository
        _cashRecepitRepository = cashRecepitRepository
        _bankRepository = bankRepository
        _crossingAccountDetailOtherConceptsRepository = crossingAccountDetailOtherConceptsRepository
        _accountReceivableAccountingRepository = accountReceivableAccountingRepository
        _crossingAccountRepository = crossingAccountRepository
        _cashFlowConceptRepository = cashFlowConceptRepository
        _thirdPartyRepository = thirdPartyRepository
        _currencyRepository = currencyRepository
        Me._companySettingRepository = companySettingsRepository
        Me._settingsAccountRepository = settingsAccountRepository
    End Sub
#End Region

#Region "Dynamic Methods"
    ''' <summary>
    '''  establece las facturas cuando se importa el archivo
    ''' </summary>
    ''' <param name="data">Listado que se va a procesar</param>
    ''' <param name="idThirdPaty"></param>
    ''' <param name="crossingType">1-Mismo Tercero, 2-Diferente Tercero</param>
    ''' <param name="processType">1 - CxP, 2 - CxC </param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SetDocumentsCrossingImportFile(data As List(Of ImportFileRow), idThirdPaty As Integer, crossingType As Integer, processType As Integer) As ActionResult(Of List(Of CrossingAccountDetailCxP), List(Of CrossingAccountDetailCxC)) Implements ITreasuryServices.SetDocumentsCrossingImportFile
        Dim listErrors As New List(Of String)
        Dim _cashFlowConcept As CashFlowConcept = Nothing
        If processType = 1 Then 'cuenta por pagar 
            Dim listResult As New List(Of CrossingAccountDetailCxP)
            For Each row In data
                Dim indexRow = row.IndexRow
                'valido que el valor sea numerico
                If Not IsNumeric(row.Row.Item(1)) Then
                    listErrors.Add("El valor del item " + (indexRow).ToString() + " no es numerico")
                    Continue For
                End If
                Dim accountPayable As AccountPayable

                Dim billNumber As String
                If IsNumeric(row.Row.Item(0)) Then
                    billNumber = CInt(row.Row.Item(0)).ToString()
                Else
                    billNumber = row.Row.Item(0)
                End If

                If crossingType = 1 Then 'mismo tercero
                    Dim supplier = _supplierRepository.GetSupplierByThirdPartyIdSimple(idThirdPaty)
                    If supplier Is Nothing Then
                        listErrors.Add("El tercero seleccionado no esta asosciado a un proveedor")
                        Continue For
                    End If

                    'obtengo la cuenta por pagar asociada al tercero
                    accountPayable = _accountPayableRepository.GetAccountPayableByBillNumber(billNumber, supplier.Id)
                    If accountPayable.Id = 0 Then
                        listErrors.Add("La cuenta por pagar del item " & (indexRow).ToString() & " no existe o no esta asociada al tercero " & String.Concat(supplier.ThirdParty.Nit, " - ", supplier.ThirdParty.Name))
                        Continue For
                    End If

                Else 'diferente tercero
                    'busco la cuenta por pagar
                    accountPayable = _accountPayableRepository.GetAccountsPayableByBillNumber(billNumber)
                    If accountPayable.Id = 0 Then
                        listErrors.Add("La cuenta por pagar del item " & (indexRow).ToString() & " no existe")
                        Continue For
                    End If
                End If
                If accountPayable.Balance < CDec(row.Row.Item(1)) Then
                    listErrors.Add("El saldo de la fatura del item " & (indexRow).ToString() & " es menor al valor a cruzar")
                    Continue For
                End If

                If row.Row.Item(2) IsNot Nothing Then
                    _cashFlowConcept = _cashFlowConceptRepository.GetCashFlowConceptByCode(CStr(row.Row.Item(2)))
                    If _cashFlowConcept Is Nothing OrElse _cashFlowConcept.Id = 0 Then
                        listErrors.Add("El concepto de flujo de efectivo del item " & (indexRow).ToString() & " no existe")
                        Continue For
                    ElseIf _cashFlowConcept.TypeConcept <> 2 Then
                        listErrors.Add("El tipo del concepto de flujo de efectivo del item " & (indexRow).ToString() & " debe ser egreso")
                        Continue For
                    ElseIf Not _cashFlowConcept.StatusConcept Then
                        listErrors.Add("El concepto de flujo de efectivo del item " & (indexRow).ToString() & " esta inactivo")
                        Continue For
                    End If
                End If

                Dim mainAccount = _mainAccountRepository.GetAccountById(accountPayable.IdAccount, False)
                Dim CrossingAccountDetailCxP As New CrossingAccountDetailCxP
                With CrossingAccountDetailCxP
                    .AccountPayableId = accountPayable.Id
                    .MainAccountId = accountPayable.IdAccount
                    .BillNumber = accountPayable.BillNumber
                    .MainAccountDescription = mainAccount.Number & " - " & mainAccount.Name
                    .Value = accountPayable.Value
                    .Balance = accountPayable.Balance
                    .CrossingValue = CDec(row.Row.Item(1))
                    .InvoiceCurrencyId = accountPayable.CurrencyId
                    .Detail = "Nota de Tesoreria : {0}, Factura CxP : " & accountPayable.BillNumber
                    If _cashFlowConcept IsNot Nothing Then
                        .IdCashFlowConcept = _cashFlowConcept.Id
                        .CodeNameCashFlowConcept = String.Format("{0} - {1}", _cashFlowConcept.Code, _cashFlowConcept.NameConcept)
                    End If
                End With
                listResult.Add(CrossingAccountDetailCxP)


            Next
            Return New ActionResult(Of List(Of CrossingAccountDetailCxP), List(Of CrossingAccountDetailCxC)) With {.StatusCode = eStatusResult.SUCCESS, .ObjectEmbbeded = listResult, .ObjectEmbbededAux = Nothing, .MessageResult = listErrors}
        Else 'cuenta por cobrar
            Dim listResult As New List(Of CrossingAccountDetailCxC)
            For Each row In data
                Dim indexRow = row.IndexRow
                If Not IsNumeric(row.Row.Item(2)) Then
                    listErrors.Add("El valor del item " + (indexRow).ToString() + " no es numerico")
                    Continue For
                End If

                Dim accountReceivableAccounting As AccountReceivableAccounting

                'valido la cuenta contable

                Dim accountNumber As String
                If IsNumeric(row.Row.Item(1)) Then
                    accountNumber = row.Row.Item(1).ToString()
                Else
                    accountNumber = row.Row.Item(1)
                End If

                Dim mainAccount = _mainAccountRepository.GetAccountByCode(accountNumber, False)
                If mainAccount.Id = 0 Then
                    listErrors.Add("La cuenta contable del item " & (indexRow).ToString() & " no existe")
                    Continue For
                End If
                If mainAccount.AllowsMovement = False Then
                    listErrors.Add("La cuenta contable del item " & (indexRow).ToString() & " no maneja movimiento")
                    Continue For
                End If

                Dim billNumber As String
                If IsNumeric(row.Row.Item(0)) Then
                    billNumber = CInt(row.Row.Item(0)).ToString()
                Else
                    billNumber = row.Row.Item(0)
                End If

                If crossingType = 1 Then 'mismo tercero

                    accountReceivableAccounting = _accountReceivableAccountingRepository.GetAccountReceivableAccountingByInvoiceNumberAndMainAccountId(billNumber, mainAccount.Id, idThirdPaty)
                    If accountReceivableAccounting Is Nothing Then
                        listErrors.Add("La factura del item " & (indexRow).ToString() & " en la cuenta contable " & mainAccount.Number & " para el tercero seleccionado no existe")
                        Continue For
                    End If
                Else 'diferente tercero
                    accountReceivableAccounting = _accountReceivableAccountingRepository.GetAccountReceivableAccountingByInvoiceNumberAndMainAccountId(billNumber, mainAccount.Id, 0)
                    If accountReceivableAccounting Is Nothing Then
                        listErrors.Add("La factura del item " & (indexRow).ToString() & " en la cuenta contable " & mainAccount.Number & " no existe")
                        Continue For
                    End If
                End If

                'Valido que la factura este radicada y también que sea de tipo 2
                Dim TupleInfo As Tuple(Of Boolean, String) = Nothing
                If crossingType = 1 Then 'mismo tercero
                    TupleInfo = _accountReceivableAccountingRepository.ValidateAccountReceivable(billNumber, idThirdPaty, indexRow)
                    If TupleInfo.Item1 = False Then
                        listErrors.Add(TupleInfo.Item2)
                        Continue For
                    End If
                Else 'diferente tercero
                    TupleInfo = _accountReceivableAccountingRepository.ValidateAccountReceivable(billNumber, 0, indexRow)
                    If TupleInfo.Item1 = False Then
                        listErrors.Add(TupleInfo.Item2)
                        Continue For
                    End If
                End If

                'valido el saldo
                If accountReceivableAccounting.Balance < CDec(row.Row.Item(2)) Then
                    listErrors.Add("La factura del item " & (indexRow).ToString() & " en la cuenta contable " & mainAccount.Number & " tiene un saldo(" & accountReceivableAccounting.Balance.ToString("c0") & ") menor al valor del cruce")
                    Continue For
                End If

                If row.Row.Item(3) IsNot Nothing Then
                    _cashFlowConcept = _cashFlowConceptRepository.GetCashFlowConceptByCode(CStr(row.Row.Item(3)))
                    If _cashFlowConcept Is Nothing OrElse _cashFlowConcept.Id = 0 Then
                        listErrors.Add("El concepto de flujo de efectivo del item " & (indexRow).ToString() & " no existe")
                        Continue For
                    ElseIf _cashFlowConcept.TypeConcept <> 1 Then
                        listErrors.Add("El tipo del concepto de flujo de efectivo del item " & (indexRow).ToString() & " debe ser ingreso")
                        Continue For
                    ElseIf Not _cashFlowConcept.StatusConcept Then
                        listErrors.Add("El concepto de flujo de efectivo del item " & (indexRow).ToString() & " esta inactivo")
                        Continue For
                    End If
                End If

                ''obtenemos info de la cxc
                accountReceivableAccounting.AccountReceivable = _accountReceivableRepository.GetAccountReceivableById(accountReceivableAccounting.AccountReceivableId)

                Dim CrossingAccountDetailCxC As New CrossingAccountDetailCxC
                With CrossingAccountDetailCxC
                    .AccountReceivableId = accountReceivableAccounting.AccountReceivableId
                    .AccountReceivableAccountingId = accountReceivableAccounting.Id
                    .MainAccountId = mainAccount.Id
                    .CrossingValue = CDec(row.Row.Item(2))
                    .Detail = "Nota de Tesoreria : {0}, Factura CxC : " & billNumber
                    .BillNumber = billNumber
                    .MainAccountDescription = mainAccount.Number & " - " & mainAccount.Name
                    .Value = accountReceivableAccounting.Value
                    .Balance = accountReceivableAccounting.Balance
                    .InvoiceCurrencyId = accountReceivableAccounting.AccountReceivable?.CurrencyId
                    If _cashFlowConcept IsNot Nothing Then
                        .IdCashFlowConcept = _cashFlowConcept.Id
                        .CodeNameCashFlowConcept = String.Format("{0} - {1}", _cashFlowConcept.Code, _cashFlowConcept.NameConcept)
                    End If
                End With

                listResult.Add(CrossingAccountDetailCxC)

            Next
            Return New ActionResult(Of List(Of CrossingAccountDetailCxP), List(Of CrossingAccountDetailCxC)) With {.StatusCode = eStatusResult.SUCCESS, .ObjectEmbbeded = Nothing, .ObjectEmbbededAux = listResult, .MessageResult = listErrors}
        End If
    End Function

    ''' <summary>
    '''  establece las cuentas por cobrar del copiar y pegar
    ''' </summary>
    ''' <param name="data">Listado que se va a procesar</param>
    ''' <param name="idThirdPaty"></param>
    ''' <param name="crossingType">1-Mismo Tercero, 2-Diferente Tercero</param>
    ''' <param name="processType">1 - CxP, 2 - CxC </param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SetDocumentsCrossingCopyPaste(data As List(Of List(Of String)), idThirdPaty As Integer, crossingType As Integer, processType As Integer) As ActionResult(Of List(Of CrossingAccountDetailCxP), List(Of CrossingAccountDetailCxC)) Implements ITreasuryServices.SetDocumentsCrossingCopyPaste
        Dim listErrors As New List(Of String)
        If processType = 1 Then 'cuenta por pagar 
            Dim listResult As New List(Of CrossingAccountDetailCxP)

            For i As Integer = 0 To data.Count - 1 Step 1
                'valido la estructura
                If data.Item(i).Count <> 3 Then
                    listErrors.Add(String.Format(ResourceManager.GetString("IncorrectStructure", MODULE_NAME), (i + 1).ToString()))
                    Continue For
                End If
                'valido que el valor sea numerico
                If Not IsNumeric(data.Item(i).Item(1)) Then
                    listErrors.Add("El valor del item " & (i + 1).ToString() & " no es numerico")
                    Continue For
                End If

                Dim accountPayable As AccountPayable
                If crossingType = 1 Then 'mismo tercero
                    Dim supplier = _supplierRepository.GetSupplierByThirdPartyIdSimple(idThirdPaty)
                    If supplier Is Nothing Then
                        listErrors.Add("El tercero seleccionado no esta asosciado a un proveedor")
                        Continue For
                    End If
                    'obtengo la cuenta por pagar asociada al tercero
                    accountPayable = _accountPayableRepository.GetAccountPayableByBillNumber(data.Item(i).Item(0), supplier.Id)
                    If accountPayable.Id = 0 Then
                        listErrors.Add("La cuenta por pagar del item " & (i + 1).ToString() & " no existe o no esta asociada al tercero " & String.Concat(supplier.ThirdParty.Nit, " - ", supplier.ThirdParty.Name))
                        Continue For
                    End If

                Else 'diferente tercero
                    'busco la cuenta por pagar
                    accountPayable = _accountPayableRepository.GetAccountsPayableByBillNumber(data.Item(i).Item(0))
                    If accountPayable.Id = 0 Then
                        listErrors.Add("La cuenta por pagar del item " & (i + 1).ToString() & " no existe")
                        Continue For
                    End If
                End If
                'valido que la cuenta por pagar no este repetida
                If listResult.Count > 0 Then
                    Dim accountPayableAdded = listResult.Find(Function(x) x.AccountPayableId = accountPayable.Id)
                    If accountPayableAdded IsNot Nothing Then
                        listErrors.Add("La cuenta por pagar del item " & (i + 1).ToString() & " esta repetida")
                        Continue For
                    End If
                End If


                If accountPayable.Balance < CDec(data.Item(i).Item(1)) Then
                    listErrors.Add("El saldo de la fatura del item " & (i + 1).ToString() & " es menor al valor a cruzar")
                    Continue For
                End If

                Dim mainAccount = _mainAccountRepository.GetAccountById(accountPayable.IdAccount, False)
                Dim CrossingAccountDetailCxP As New CrossingAccountDetailCxP
                With CrossingAccountDetailCxP
                    .AccountPayableId = accountPayable.Id
                    .MainAccountId = accountPayable.IdAccount
                    .BillNumber = accountPayable.BillNumber
                    .MainAccountDescription = mainAccount.Number & " - " & mainAccount.Name
                    .Value = accountPayable.Value
                    .Balance = accountPayable.Balance
                    .CrossingValue = CDec(data.Item(i).Item(1))
                    .InvoiceCurrencyId = accountPayable.CurrencyId
                    .Detail = "Nota de Tesoreria : {0}, Factura CxP : " & accountPayable.BillNumber
                End With
                listResult.Add(CrossingAccountDetailCxP)
            Next

            Return New ActionResult(Of List(Of CrossingAccountDetailCxP), List(Of CrossingAccountDetailCxC)) With {.StatusCode = eStatusResult.SUCCESS, .ObjectEmbbeded = listResult, .ObjectEmbbededAux = Nothing, .MessageResult = listErrors}
        Else 'cuenta por cobrar
            Dim listResult As New List(Of CrossingAccountDetailCxC)
            For i As Integer = 0 To data.Count - 1 Step 1
                If data.Item(i).Count <> 4 Then
                    listErrors.Add(String.Format(ResourceManager.GetString("IncorrectStructure", MODULE_NAME), (i + 1).ToString()))
                    Continue For
                End If

                If Not IsNumeric(data.Item(i).Item(2)) Then
                    listErrors.Add("El valor del item " & (i + 1).ToString() & " no es numerico")
                    Continue For
                End If

                Dim accountReceivableAccounting As AccountReceivableAccounting

                'valido la cuenta contable
                Dim mainAccount = _mainAccountRepository.GetAccountByCode(data.Item(i).Item(1), False)
                If mainAccount.Id = 0 Then
                    listErrors.Add("La cuenta contable del item " & (i + 1).ToString() & " no existe")
                    Continue For
                End If
                If mainAccount.AllowsMovement = False Then
                    listErrors.Add("La cuenta contable del item " & (i + 1).ToString() & " no maneja movimiento")
                    Continue For
                End If

                If crossingType = 1 Then 'mismo tercero

                    accountReceivableAccounting = _accountReceivableAccountingRepository.GetAccountReceivableAccountingByInvoiceNumberAndMainAccountId(data.Item(i).Item(0), mainAccount.Id, idThirdPaty)
                    If accountReceivableAccounting Is Nothing Then
                        listErrors.Add("La factura del item " & (i + 1).ToString() & " en la cuenta contable " & mainAccount.Number & " para el tercero seleccionado no existe")
                        Continue For
                    End If
                Else 'diferente tercero
                    accountReceivableAccounting = _accountReceivableAccountingRepository.GetAccountReceivableAccountingByInvoiceNumberAndMainAccountId(data.Item(i).Item(0), mainAccount.Id, 0)
                    If accountReceivableAccounting Is Nothing Then
                        listErrors.Add("La factura del item " & (i + 1).ToString() & " en la cuenta contable " & mainAccount.Number & " no existe")
                        Continue For
                    End If
                End If

                'Valido que la factura este radicada y también que sea de tipo 2
                Dim TupleInfo As Tuple(Of Boolean, String) = Nothing
                If crossingType = 1 Then 'mismo tercero
                    TupleInfo = _accountReceivableAccountingRepository.ValidateAccountReceivable(data.Item(i).Item(0), idThirdPaty, i + 1)
                    If TupleInfo.Item1 = False Then
                        listErrors.Add(TupleInfo.Item2)
                        Continue For
                    End If
                Else 'diferente tercero
                    TupleInfo = _accountReceivableAccountingRepository.ValidateAccountReceivable(data.Item(i).Item(0), 0, i + 1)
                    If TupleInfo.Item1 = False Then
                        listErrors.Add(TupleInfo.Item2)
                        Continue For
                    End If
                End If

                'valido el saldo
                If accountReceivableAccounting.Balance < CDec(data.Item(i).Item(2)) Then
                    listErrors.Add("La factura del item " & (i + 1).ToString() & " en la cuenta contable " & mainAccount.Number & " tiene un saldo(" & accountReceivableAccounting.Balance.ToString("c0") & ") menor al valor del cruce")
                    Continue For
                End If

                ''obtenemos info de la cxc
                accountReceivableAccounting.AccountReceivable = _accountReceivableRepository.GetAccountReceivableById(accountReceivableAccounting.AccountReceivableId)

                Dim CrossingAccountDetailCxC As New CrossingAccountDetailCxC
                With CrossingAccountDetailCxC
                    .AccountReceivableId = accountReceivableAccounting.AccountReceivableId
                    .AccountReceivableAccountingId = accountReceivableAccounting.Id
                    .MainAccountId = mainAccount.Id
                    .CrossingValue = CDec(data.Item(i).Item(2))
                    .Detail = "Nota de Tesoreria : {0}, Factura CxC : " & data.Item(i).Item(0)
                    .BillNumber = data.Item(i).Item(0)
                    .MainAccountDescription = mainAccount.Number & " - " & mainAccount.Name
                    .Value = accountReceivableAccounting.Value
                    .InvoiceCurrencyId = accountReceivableAccounting.AccountReceivable.CurrencyId
                    .Balance = accountReceivableAccounting.Balance
                End With

                listResult.Add(CrossingAccountDetailCxC)
            Next
            Return New ActionResult(Of List(Of CrossingAccountDetailCxP), List(Of CrossingAccountDetailCxC)) With {.StatusCode = eStatusResult.SUCCESS, .ObjectEmbbeded = Nothing, .ObjectEmbbededAux = listResult, .MessageResult = listErrors}
        End If
    End Function

    Public Function ValidateCashReceipts(cashReceipt As CashReceipts, userId As Integer, codeUser As String, Optional confirm As Boolean = False) As ActionResult(Of String) Implements ITreasuryServices.ValidateCashReceipts
        Dim debit As Decimal = 0
        Dim credit As Decimal = 0
        Dim result As New StringBuilder
        Dim accountingServices As New AccountingServices(_mainAccountRepository, _closeMonthRepository)
        Dim validatePeriod = accountingServices.ValidatePeriodAndReturnOpendMonths(cashReceipt.DocumentDate)
        If validatePeriod.StateResult = True Then
            If cashReceipt.CashReceiptDetails.Count = 0 Then
                result.AppendLine("- " + ResourceManager.GetString("CashReceiptDetailsList", MODULE_NAME))
            End If

            If cashReceipt.PaymentMethods.Count = 0 Then
                result.AppendLine("- " + ResourceManager.GetString("PaymentMethodsList", MODULE_NAME))
            End If

            debit += cashReceipt.CashReceiptDetails.Where(Function(x) x.Nature = 1 And x.ChangeTracker.State <> ObjectState.Deleted).Sum(Function(y) y.Value)
            debit += cashReceipt.PaymentMethods.Where(Function(x) x.ChangeTracker.State <> ObjectState.Deleted).Sum(Function(y) y.Value)
            credit += cashReceipt.CashReceiptDetails.Where(Function(x) x.Nature = 2 And x.ChangeTracker.State <> ObjectState.Deleted).Sum(Function(y) y.Value)

            If debit = 0 Or credit = 0 Then
                result.AppendLine("- " + ResourceManager.GetString("DebitOrCreditVoid", MODULE_NAME))
            End If

            If debit > 0 And credit > 0 And debit <> credit Then
                result.AppendLine("- " + ResourceManager.GetString("DifferenceBetweenDebitAndCredit"))
            End If

            If confirm = True Then
                If cashReceipt.CollectType = 1 Then
                    Dim listCashUser = _cashRegisterRepository.ListCashRegisterUserByCashRegisterIdAndUser(cashReceipt.IdCashRegister, userId)
                    If listCashUser.Count = 0 Then
                        result.AppendLine("- " + "El usuario no tiene permiso para la caja seleccionada")
                    End If
                Else
                    Dim listAccountUser = _entityBankAccountRepository.ListEntityBankAccountUserByIdEntityBankiAccountAndUserCode(cashReceipt.IdBankAccount, codeUser)
                    If listAccountUser.Count = 0 Then
                        result.AppendLine("- " + "El usuario no tiene permiso para la cuenta bancaria seleccionada")
                    End If
                End If
                Dim conceptDetail As CashReceiptDetails
                conceptDetail = cashReceipt.CashReceiptDetails.Where(Function(x) x.CashReceiptConceptAffectation = 2 And x.ChangeTracker.State <> ObjectState.Deleted).FirstOrDefault()
                If conceptDetail IsNot Nothing AndAlso conceptDetail.CashReceiptAccountReceivable.Count > 0 Then
                    For Each item In conceptDetail.CashReceiptAccountReceivable
                        Dim invoice = _accountReceivableRepository.GetAccountReceivableById(item.AccountReceivableId)
                        Dim account = invoice.AccountReceivableAccounting.Where(Function(x) x.AccountReceivableId = item.AccountReceivableId And x.MainAccountId = conceptDetail.IdMainAccount).FirstOrDefault()
                        If account.Balance < item.Value Then
                            result.AppendLine("- " + String.Format(ResourceManager.GetString("BalanceBill", "Portfolio"), invoice.InvoiceNumber))
                        End If
                    Next
                End If
                conceptDetail = cashReceipt.CashReceiptDetails.Where(Function(x) x.CashReceiptConceptAffectation = 3 And x.ChangeTracker.State <> ObjectState.Deleted).FirstOrDefault()
                If conceptDetail IsNot Nothing AndAlso conceptDetail.CashReceiptAdvancePayment.Count > 0 Then
                    For Each item In conceptDetail.CashReceiptAdvancePayment
                        Dim advance = _advancePaymentRepository.GetMoneyAdvanceById(item.AdvancePaymentId)
                        If advance.Balance < item.PaymentValue Then
                            result.AppendLine("-" + String.Format(ResourceManager.GetString("LowerBalanceAdvance", MODULE_NAME), advance.Code))
                        End If
                    Next
                End If
            End If
            If result.Length = 0 Then
                Return New ActionResult(Of String) With {.StateResult = True, .ObjectEmbbeded = String.Empty}
            Else
                Return New ActionResult(Of String) With {.StateResult = False, .Message = result.ToString(), .ObjectEmbbeded = String.Empty}
            End If
        Else
            Return New ActionResult(Of String) With {.StateResult = False, .Message = validatePeriod.Message, .ObjectEmbbeded = "Periods"}
        End If
    End Function

    Public Function SetBillsCashReceipts(data As List(Of List(Of String)), idThirdPaty As Integer, idMainAccount As Integer, idCostCenter As Integer, idOperatingUnit As Integer) As ActionResult(Of List(Of AccountReceivable)) Implements ITreasuryServices.SetBillsCashReceipts
        Dim listInvoice As New List(Of AccountReceivable)
        Dim listErrors As New List(Of String)
        If idThirdPaty = 0 Then
            listErrors.Add(ResourceManager.GetString("ThirdPartyNotSelected", MODULE_NAME))
        End If
        If idMainAccount = 0 Then
            listErrors.Add(ResourceManager.GetString("AccountNotSelected", MODULE_NAME))
        End If
        If listErrors.Count = 0 Then
            For i As Integer = 0 To data.Count - 1 Step 1
                If data.Item(i).Count <> 2 Then
                    listErrors.Add(String.Format(ResourceManager.GetString("IncorrectStructure", MODULE_NAME), (i + 1).ToString()))
                    Continue For
                End If
                If listInvoice.Count > 0 Then
                    Dim invoiceAdded = listInvoice.Where(Function(x) x.InvoiceNumber = data(i).Item(0)).FirstOrDefault()
                    If invoiceAdded IsNot Nothing Then
                        listErrors.Add(String.Format(ResourceManager.GetString("InvoiceListExist", MODULE_NAME), data(i).Item(0)))
                        Exit For
                    End If
                End If
                Dim result = _accountReceivableRepository.GetAccountReceivableByInvoiceNumber(data.Item(i).Item(0).ToUpper(), idThirdPaty, idMainAccount, idCostCenter, idOperatingUnit)
                If result.Count > 0 Then
                    For Each item In result
                        If item.Balance < CDec(data(i).Item(1)) Then
                            listErrors.Add(String.Format(ResourceManager.GetString("ValuePayGreaterParameter", MODULE_NAME), data(i).Item(0)))
                            Continue For
                        End If
                        If CDec(data(i).Item(1)) <= 0 Then
                            listErrors.Add(String.Format(ResourceManager.GetString("PaymentValueZero", MODULE_NAME), data(i).Item(0)))
                            Continue For
                        End If
                        If IsNumeric(data(i).Item(1)) = False Then
                            listErrors.Add(String.Format(ResourceManager.GetString("PayValueNotNumeric", MODULE_NAME), data(i).Item(1)))
                            Continue For
                        End If
                        item.PaymentValue = data(i).Item(1)
                        listInvoice.Add(item)
                    Next
                Else
                    listErrors.Add(String.Format(ResourceManager.GetString("ThirdPartyNoInvoiceAssigned", MODULE_NAME), data(i).Item(0)))
                    Continue For
                End If
            Next
        End If
        Return New ActionResult(Of List(Of AccountReceivable)) With {.ObjectEmbbeded = listInvoice, .MessageResult = listErrors}
    End Function

    ''' <summary>
    ''' valida el comprobante de egreso para confirmacion
    ''' </summary>
    Public Function ValidateVoucherTransaction(ByVal voucherTransaction As VoucherTransaction) As ActionResult(Of String) Implements ITreasuryServices.ValidateVoucherTransaction
        Dim resultValidateSave As ActionResult(Of String) = Me.ValidateVoucherTransactionSave(voucherTransaction)
        If resultValidateSave.StateResult = False Then
            Return New ActionResult(Of String) With {.StateResult = False, .Message = resultValidateSave.Message}
        End If
        Dim errorList As New StringBuilder()
        If voucherTransaction.Value <= 0 Then
            errorList.AppendLine(ResourceManager.GetString("voucherTransactionValueMoreZero", MODULE_NAME))
            Return New ActionResult(Of String) With {.StateResult = False, .Message = errorList.ToString()}
        End If
        Dim accountingServices As New AccountingServices(_mainAccountRepository, _closeMonthRepository)
        Dim validatePeriod = accountingServices.ValidatePeriodAndReturnOpendMonths(voucherTransaction.DocumentDate)
        If validatePeriod.StateResult = False Then
            errorList.AppendLine(validatePeriod.Message)
            Return New ActionResult(Of String) With {.StateResult = False, .Message = errorList.ToString()}
        End If
        'If voucherTransaction.Value <= 0 Then
        '    errorList.AppendLine(ResourceManager.GetString("VoucherTransactionValueError", MODULE_NAME))
        'End If


        Dim valueVoucer As Decimal = IIf(voucherTransaction.TaxByMil, voucherTransaction.Value + voucherTransaction.TaxByMilValue, voucherTransaction.Value)
        Dim direfenceValue As Decimal = Math.Abs(voucherTransaction.VoucherTransactionDetails.Where(Function(o) o.Nature = 1).Sum(Function(x) x.Value).Value - voucherTransaction.VoucherTransactionDetails.Where(Function(o) o.Nature = 2).Sum(Function(x) x.Value).Value)
        If valueVoucer <> direfenceValue Then
            errorList.AppendLine("El valor del comprobante no coincide con la suma de los valores de los conceptos")
        End If
        If voucherTransaction.IdEntityBankAccount IsNot Nothing Then
            Dim entityAccount As EntityBankAccounts = _entityBankAccountRepository.GetEntityBankAccountById(voucherTransaction.IdEntityBankAccount)
            If entityAccount.CurrentBalance < voucherTransaction.Value Then
                errorList.AppendLine(String.Format(ResourceManager.GetString("EntityAccountInsufficientBalanceParameter", MODULE_NAME), entityAccount.Code))
            End If
        ElseIf voucherTransaction.IdCashRegister IsNot Nothing Then
            Dim cashRegister As CashRegisters = _cashRegisterRepository.GetCashRegisterById(voucherTransaction.IdCashRegister)
            If cashRegister.CurrentBalance < voucherTransaction.Value Then
                errorList.AppendLine(ResourceManager.GetString("CashInsufficientBalance", MODULE_NAME))
            End If
            If cashRegister.Type = 1 And cashRegister.AmountMax < voucherTransaction.Value Then
                errorList.AppendLine(String.Format("El valor a pagar no debe ser mayor a la cuantía máxima de la caja ({0})", String.Concat(cashRegister.Code, " - ", cashRegister.Name)))
            End If
        Else
            errorList.AppendLine(ResourceManager.GetString("CashOrBankAccountNecessary", MODULE_NAME))
        End If
        For Each voucherDetail As VoucherTransactionDetails In voucherTransaction.VoucherTransactionDetails

            Select Case voucherTransaction.VoucherClass
                Case 1 'Payment
                    Dim expenseConcept As ExpenseConcepts = _expenseConceptRepository.GetExpenseConceptById(voucherDetail.IdExpenseConcept)
                    If expenseConcept IsNot Nothing AndAlso expenseConcept.Id > 0 Then
                        'Comportamiento: 1 - Traslado entre bancos; 2 - Caja Menor; 3 - Pago/Anticipo de Facturas CxP; 4 - Devolutivos de Anticipos RC; 5 - Reembolso de Caja Menor; 6 - Ninguno
                        Select Case expenseConcept.Behavior
                            'Case 1
                            '    Dim entityAccountD As EntityBankAccounts = _entityBankAccountRepository.GetEntityBankAccountById(voucherDetail.IdEntityBankAccount)
                            '    If entityAccountD.CurrentBalance < voucherTransaction.Value Then
                            '        errorList.AppendLine(String.Format(ResourceManager.GetString("EntityAccountInsufficientBalanceParameter", MODULE_NAME), entityAccountD.Code))
                            '    End If
                            Case 2
                                'VALIDACION DE TERCERO Y CUENTA CONTABLE PARA LAS FACTURAS
                                Dim countInvoiceByThirdAndMainAccount As Integer = _accountPayableRepository.GetCountAccountPayableShareByAccountPayIdThirdIdAccountAndState(voucherTransaction.IdThirdParty, voucherDetail.IdMainAccount, CInt(eStatusAccountPayable.Confirmado))
                                If countInvoiceByThirdAndMainAccount = 0 Then
                                    Dim _third As Domain.Entities.ThirdParty = _supplierRepository.GetThirdPartyById(voucherTransaction.IdThirdParty)
                                    Dim _mainAccount As MainAccounts = _mainAccountRepository.GetAccountById(voucherDetail.IdMainAccount, False)
                                    errorList.AppendLine(String.Format(ResourceManager.GetString("NoInvoiceThird", "Payments"), String.Concat(_third.Nit, " - ", _third.Name), String.Concat(_mainAccount.Number, " - ", _mainAccount.Name)))
                                End If
                                'END
                                Dim listDischargeBill = _dischargeBillRepository.ListDischargeBillByIdVoucherTransactionD(voucherDetail.Id)
                                For Each dischargeBill As DischargeBill In listDischargeBill
                                    Dim accountPayable As AccountPayable = _accountPayableRepository.GetAccountPayableById(dischargeBill.IdAccountPayable)
                                    Dim accountPayableShare As AccountPayableShares = accountPayable.AccountPayableShares.Where(Function(x) x.Id = dischargeBill.IdAccountPayableShare).Cast(Of AccountPayableShares).FirstOrDefault()
                                    'validacion para que halla saldo en la couta
                                    If accountPayableShare.Balance < dischargeBill.AdvancedValue Then
                                        errorList.AppendLine(String.Format(ResourceManager.GetString("AccountPayableBalanceError", MODULE_NAME), accountPayableShare.Share, accountPayable.Code))
                                    End If
                                Next
                            Case 3
                                'VALIDACION DE TERCERO Y CUENTA CONTABLE PARA LAS FACTURAS
                                If voucherDetail.AdvanceValue = 0 Then
                                    Dim countInvoiceByThirdAndMainAccount As Integer = _accountPayableRepository.GetCountAccountPayableShareByAccountPayIdThirdIdAccountAndState(voucherTransaction.IdThirdParty, voucherDetail.IdMainAccount, CInt(eStatusAccountPayable.Confirmado))
                                    If countInvoiceByThirdAndMainAccount = 0 Then
                                        Dim _third As Domain.Entities.ThirdParty = _supplierRepository.GetThirdPartyById(voucherTransaction.IdThirdParty)
                                        Dim _mainAccount As MainAccounts = _mainAccountRepository.GetAccountById(voucherDetail.IdMainAccount, False)
                                        errorList.AppendLine(String.Format(ResourceManager.GetString("NoInvoiceThird", "Payments"), String.Concat(_third.Nit, " - ", _third.Name), String.Concat(_mainAccount.Number, " - ", _mainAccount.Name)))
                                    End If
                                    'END
                                    Dim listDischargeBill = _dischargeBillRepository.ListDischargeBillByIdVoucherTransactionD(voucherDetail.Id)
                                    For Each dischargeBill As DischargeBill In listDischargeBill
                                        Dim accountPayable As AccountPayable = _accountPayableRepository.GetAccountPayableById(dischargeBill.IdAccountPayable)
                                        Dim accountPayableShare As AccountPayableShares = accountPayable.AccountPayableShares.Where(Function(x) x.Id = dischargeBill.IdAccountPayableShare).Cast(Of AccountPayableShares).FirstOrDefault()
                                        'validacion para que halla saldo en la couta
                                        If accountPayableShare.Balance < dischargeBill.AdvancedValue Then
                                            errorList.AppendLine(String.Format(ResourceManager.GetString("AccountPayableBalanceError", MODULE_NAME), accountPayableShare.Share, accountPayable.Code))
                                        End If
                                    Next
                                End If
                            Case 4
                                Dim _portfAdvanceResult = _portfolioAdvanceRepository.ListPorfolioAdvanceByThirdId(voucherDetail.IdThirdParty)
                                If _portfAdvanceResult Is Nothing OrElse _portfAdvanceResult.Count = 0 Then
                                    errorList.AppendLine(ResourceManager.GetString("AdvanceBackNotFound", MODULE_NAME))
                                End If
                                'Case 5
                                '    Dim _listRefund = _refundRepository.ListRefundByAccount(voucherDetail.IdMainAccount)
                                '    If _listRefund IsNot Nothing AndAlso _listRefund.Count > 0 Then
                                '        For Each Refund As Refunds In _listRefund
                                '            If Refund.Refunded = True Then
                                '                errorList.AppendLine(String.Format(ResourceManager.GetString("RefundRefunded", MODULE_NAME), Refund.Code))
                                '            End If
                                '            Dim _cashRefund As CashRegisters = _cashRegisterRepository.GetCashRegisterById(Refund.IdCashRegister)
                                '            If _listRefund.Sum(Function(x) x.Value) > _cashRefund.AmountMax Then
                                '                errorList.AppendLine(String.Format(ResourceManager.GetString("AmountMaxExceeded", MODULE_NAME), _cashRefund.Code))
                                '            End If
                                '        Next
                                '    Else
                                '        errorList.AppendLine(ResourceManager.GetString("AffectRefundNotFound", MODULE_NAME))
                                '    End If
                            Case 6
                        End Select
                    End If
                Case 2 'Refund
                    Dim _listRefund = _refundRepository.ListRefundByCashRegisterId(voucherDetail.CashRegisterId)
                    If _listRefund IsNot Nothing AndAlso _listRefund.Count > 0 Then
                        For Each Refund As Refunds In _listRefund
                            If Refund.Refunded = True Then
                                errorList.AppendLine(String.Format(ResourceManager.GetString("RefundRefunded", MODULE_NAME), Refund.Code))
                            End If
                            Dim _cashRefund As CashRegisters = _cashRegisterRepository.GetCashRegisterById(Refund.IdCashRegister)
                            'If _listRefund.Sum(Function(x) x.Value) > _cashRefund.AmountMax Then
                            '    errorList.AppendLine(String.Format(ResourceManager.GetString("AmountMaxExceeded", MODULE_NAME), _cashRefund.Code))
                            'End If
                        Next
                    Else
                        errorList.AppendLine(ResourceManager.GetString("AffectRefundNotFound", MODULE_NAME))
                    End If
                Case 3 'Transfer
                    If voucherTransaction.ExpenseType = 1 Then
                        'Dim entityAccountD As EntityBankAccounts = _entityBankAccountRepository.GetEntityBankAccountById(voucherDetail.IdEntityBankAccount)
                        'If entityAccountD.CurrentBalance < voucherTransaction.Value Then
                        '    errorList.AppendLine(String.Format(ResourceManager.GetString("EntityAccountInsufficientBalanceParameter", MODULE_NAME), entityAccountD.Code))
                        'End If
                    ElseIf voucherTransaction.ExpenseType = 3 Then
                        Dim cashRegisterD As CashRegisters = _cashRegisterRepository.GetCashRegisterById(voucherDetail.CashRegisterId)
                        ' Esto no se valida ya que esto se saca de la caja
                        'If cashRegisterD.CurrentBalance < voucherTransaction.Value Then
                        '    errorList.AppendLine(String.Format(ResourceManager.GetString("CashRegisterInsufficientBalanceParameter", MODULE_NAME), cashRegisterD.Code))
                        'End If
                    End If
            End Select
        Next
        If errorList.Length > 0 Then
            Return New ActionResult(Of String) With {.StateResult = False, .Message = errorList.ToString()}
        End If
        Return New ActionResult(Of String) With {.StateResult = True}
    End Function

    ''' <summary>
    ''' valida el comprobante de egreso antes de enviar a guardar
    ''' </summary>
    ''' <returns></returns>
    Public Function ValidateVoucherTransactionSave(ByVal voucherTransaction As VoucherTransaction) As ActionResult(Of String) Implements ITreasuryServices.ValidateVoucherTransactionSave
        If voucherTransaction Is Nothing Then
            Return New ActionResult(Of String) With {.StateResult = False, .Message = String.Format(ResourceManager.GetString("EntityNothing"), GetType(VoucherTransaction).Name)}
        End If
        Dim errorList As New StringBuilder()
        'FieldEmpty

        Dim accountingServices As New AccountingServices(_mainAccountRepository, _closeMonthRepository)
        Dim validatePeriod = accountingServices.ValidatePeriodAndReturnOpendMonths(voucherTransaction.DocumentDate)
        If validatePeriod.StateResult = False Then
            errorList.AppendLine(validatePeriod.Message)
        End If
        If voucherTransaction.Value <= 0 Then
            errorList.AppendLine(ResourceManager.GetString("voucherTransactionValueMoreZero", MODULE_NAME))
        End If
        With voucherTransaction

            'Campos Obligatorios
            If .IdMainAccount = 0 Then
                errorList.AppendLine(String.Format(ResourceManager.GetString("FieldEmptyName"), ResourceManager.GetString("ValidateMainAccount", MODULE_NAME)))
            Else
                Dim _mainAccount As MainAccounts = _mainAccountRepository.GetAccountById(.IdMainAccount, False)
                If _mainAccount.HandlesCostCenter AndAlso (.IdCostCenter Is Nothing OrElse .IdCostCenter = 0) Then
                    errorList.AppendLine(String.Format(ResourceManager.GetString("FieldEmptyName"), ResourceManager.GetString("ValidateCostCenter", MODULE_NAME)))
                End If
            End If
            If .ExpenseType = 0 Then
                errorList.AppendLine(String.Format(ResourceManager.GetString("FieldEmptyName"), ResourceManager.GetString("ValidateExpenseType", MODULE_NAME)))
            Else
                Select Case .ExpenseType
                    Case eExpenseType.BankAccount
                        If .IdEntityBankAccount Is Nothing OrElse .IdEntityBankAccount = 0 Then
                            errorList.AppendLine(String.Format(ResourceManager.GetString("FieldEmptyName"), ResourceManager.GetString("ValidateEntityAccount", MODULE_NAME)))
                        Else
                            If .PaymentMethod Is Nothing OrElse .PaymentMethod = 0 Then
                                errorList.AppendLine(String.Format(ResourceManager.GetString("FieldEmptyName"), ResourceManager.GetString("ValidatePaymentMethod", MODULE_NAME)))
                            Else
                                Select Case .PaymentMethod
                                    Case ePaymentMethod.Check
                                        If .IdChecks Is Nothing OrElse .IdChecks = 0 Then
                                            errorList.AppendLine(String.Format(ResourceManager.GetString("FieldEmptyName"), ResourceManager.GetString("ValidateCheckBook", MODULE_NAME)))
                                        End If
                                    Case ePaymentMethod.DebitNote
                                        If String.IsNullOrEmpty(.NoteNumber) Then
                                            errorList.AppendLine(String.Format(ResourceManager.GetString("FieldEmptyName"), ResourceManager.GetString("ValidateDebitNote", MODULE_NAME)))
                                        End If
                                    Case Else
                                        errorList.AppendLine(ResourceManager.GetString("MethodPaymentNotExist", MODULE_NAME))
                                End Select
                            End If
                        End If
                    Case eExpenseType.MinorCash, eExpenseType.MajorCash
                        If .IdCashRegister Is Nothing OrElse .IdCashRegister = 0 Then
                            errorList.AppendLine(String.Format(ResourceManager.GetString("FieldEmptyName"), ResourceManager.GetString("ValidateCashRegister", MODULE_NAME)))
                        End If
                    Case Else
                        errorList.AppendLine(ResourceManager.GetString("ExpenseTypeNotExist", MODULE_NAME))
                End Select
            End If
            If String.IsNullOrEmpty(.DocumentDate) Then
                errorList.AppendLine(String.Format(ResourceManager.GetString("FieldEmptyName"), ResourceManager.GetString("ValidateDocumentDate", MODULE_NAME)))
            End If
            If .Value = 0 Then
                errorList.AppendLine(ResourceManager.GetString("VoucherTransactionValueError", MODULE_NAME))
            End If
            If .IdUnitOperative = 0 Then
                errorList.AppendLine(String.Format(ResourceManager.GetString("FieldEmptyName"), ResourceManager.GetString("ValidateOperatingUnit", MODULE_NAME)))
            End If
            If .Status = 0 Then
                errorList.AppendLine(String.Format(ResourceManager.GetString("PropertyError"), ResourceManager.GetString("ValidateState", MODULE_NAME), .Status))
            End If
            If .TaxByMil AndAlso .TaxByMilValue Is Nothing Then
                errorList.AppendLine(String.Format(ResourceManager.GetString("FieldEmptyName"), ResourceManager.GetString("ValidateTaxByMil", MODULE_NAME)))
            End If

            Select Case .VoucherClass
                Case 1 'Payment
                    If .IdThirdParty = 0 Then
                        errorList.AppendLine(String.Format(ResourceManager.GetString("FieldEmptyName"), ResourceManager.GetString("ValidateThirdParty", MODULE_NAME)))
                    End If
                Case 2 'Refund
                Case 3 'Transfer
            End Select

        End With

        If voucherTransaction.VoucherTransactionDetails Is Nothing OrElse voucherTransaction.VoucherTransactionDetails.Count = 0 Then
            errorList.AppendLine(String.Format(ResourceManager.GetString("VoucherTransactionConceptNotFound", MODULE_NAME)))
        End If

        If errorList.Length > 0 Then
            Return New ActionResult(Of String) With {.StateResult = False, .Message = errorList.ToString()}
        Else
            Return New ActionResult(Of String) With {.StateResult = True}
        End If
    End Function

    ''' <summary>
    ''' Valida el guardar de los cruces de cuentas CxP yCxC
    ''' </summary>
    ''' <returns></returns>
    Public Function ValidateCrossingAccountSave(ByVal crossingAccount As CrossingAccount) As ActionResult(Of String) Implements ITreasuryServices.ValidateCrossingAccountSave
        If crossingAccount Is Nothing Then
            Return New ActionResult(Of String) With {.StateResult = False, .Message = String.Format(ResourceManager.GetString("EntityNothing"), GetType(CrossingAccount).Name)}
        End If
        Dim errorList As New StringBuilder()
        If String.IsNullOrEmpty(crossingAccount.DocumentDate) Then
            Dim accountingServices As New AccountingServices(_mainAccountRepository, _closeMonthRepository)
            Dim validatePeriod = accountingServices.ValidatePeriodAndReturnOpendMonths(crossingAccount.DocumentDate)
            If validatePeriod.StateResult = False Then
                errorList.AppendLine(validatePeriod.Message)
            End If
        End If
        'Campos Obligatorios
        With crossingAccount
            If String.IsNullOrEmpty(.Description) Then
                errorList.AppendLine(String.Format(ResourceManager.GetString("FieldEmptyName"), "Descripción"))
            End If
            If .ThirdPartyId = 0 Then
                errorList.AppendLine(String.Format(ResourceManager.GetString("FieldEmptyName"), "Tercero"))
            End If
            If Not (.Status = 1 OrElse .Status = 3) Then
                errorList.AppendLine(String.Format(ResourceManager.GetString("PropertyError"), "Estado", .Status))
            End If
            If .OperatingUnitId = 0 Then
                errorList.AppendLine(String.Format(ResourceManager.GetString("FieldEmptyName"), "Unidad Operativa"))
            End If
        End With

        If crossingAccount.CrossingType = 1 Then 'Mismo tercero
            Dim IsCurrentThird As Boolean = False
            Dim lstAccReceivable As List(Of Integer) = crossingAccount.CrossingAccountDetailCxC.Select(Function(o) o.AccountReceivableId).ToList()
            For Each acrId As Integer In lstAccReceivable
                Dim accrec As AccountReceivable = _accountReceivableRepository.GetAccountReceivableById(acrId)
                If accrec.ThirdPartyId = crossingAccount.ThirdPartyId Then
                    IsCurrentThird = True
                    Exit For
                End If
            Next

            If IsCurrentThird = False Then
                Dim lstAccPayable As List(Of Integer) = crossingAccount.CrossingAccountDetailCxP.Select(Function(o) o.AccountPayableId).ToList()
                For Each apId As Integer In lstAccPayable
                    Dim accountPay As AccountPayable = _accountPayableRepository.GetAccountPayableById(apId)
                    If accountPay.IdThirdParty = crossingAccount.ThirdPartyId Then
                        IsCurrentThird = True
                        Exit For
                    End If
                Next
            End If

            If Not IsCurrentThird Then
                errorList.AppendLine("El tercero debe tener relacionado al menos una factura CxC y CxP")
            End If
        End If

        If errorList.Length > 0 Then
            Return New ActionResult(Of String) With {.StateResult = False, .Message = errorList.ToString()}
        Else
            Return New ActionResult(Of String) With {.StateResult = True}
        End If
    End Function

    ''' <summary>
    ''' valida el confirmar de el cruce de cuentas
    ''' </summary>
    Public Function ValidateCrossingAccountConfirm(ByVal crossingAccount As CrossingAccount) As ActionResult(Of CrossingAccount) Implements ITreasuryServices.ValidateCrossingAccountConfirm
        Dim errorList As New StringBuilder()

        Dim accountingServices As New AccountingServices(_mainAccountRepository, _closeMonthRepository)
        Dim validatePeriod = accountingServices.ValidatePeriodAndReturnOpendMonths(crossingAccount.DocumentDate)
        If validatePeriod.StateResult = False Then
            errorList.AppendLine(validatePeriod.Message)
            Return New ActionResult(Of CrossingAccount) With {.StateResult = False, .Message = errorList.ToString()}
        End If

        Dim _listCrossingCxP As List(Of CrossingAccountDetailCxP) = _crossingAccountDetailCxPRepository.ListCrossingAccountDetailCxPByCrossingAccountId(crossingAccount.Id, True)
        Dim _listCrossingCxC As List(Of CrossingAccountDetailCxC) = _crossingAccountDetailCxCRepository.ListCrossingAccountDetailCxCByCrossingAccountId(crossingAccount.Id, True)
        Dim _listCrossingConcepts As List(Of CrossingAccountDetailOtherConcept) = _crossingAccountDetailOtherConceptsRepository.ListCrossingAccountDetailOtherConceptByCrossingAccountId(crossingAccount.Id, True)

        If (_listCrossingCxP Is Nothing OrElse _listCrossingCxP.Count = 0) AndAlso (_listCrossingCxC Is Nothing OrElse _listCrossingCxC.Count = 0) Then
            errorList.AppendLine("No hay Facturas de CxP, ni Facturas CxC para cruzar")
        End If

        If _listCrossingCxP IsNot Nothing Then
            _listCrossingCxP.ForEach(Sub(crossinfCxP)
                                         crossingAccount.CrossingAccountDetailCxP.Add(crossinfCxP)
                                     End Sub)
        End If
        If _listCrossingCxC IsNot Nothing Then
            _listCrossingCxC.ForEach(Sub(crossingCxC)
                                         crossingAccount.CrossingAccountDetailCxC.Add(crossingCxC)
                                     End Sub)
        End If
        If _listCrossingConcepts IsNot Nothing Then
            _listCrossingConcepts.ForEach(Sub(crossingConcepts)
                                              crossingAccount.CrossingAccountDetailOtherConcept.Add(crossingConcepts)
                                          End Sub)
        End If

        Dim resultValidateSave As ActionResult(Of String) = Me.ValidateCrossingAccountSave(crossingAccount)
        If resultValidateSave.StateResult = False Then
            Return New ActionResult(Of CrossingAccount) With {.StateResult = False, .Message = resultValidateSave.Message}
        End If

        Dim difference As Integer = Math.Abs((_listCrossingCxP.Sum(Function(x) x.CrossingValue) + _listCrossingConcepts.Where(Function(x) x.Nature = 1).Sum(Function(x) x.Value)) - (_listCrossingCxC.Sum(Function(x) x.CrossingValue) + _listCrossingConcepts.Where(Function(x) x.Nature = 2).Sum(Function(x) x.Value)))
        If difference <> 0 Then
            errorList.AppendLine("La diferencia entre los valores de CxP y CxC es diferente de cero")
            Return New ActionResult(Of CrossingAccount) With {.StateResult = False, .Message = errorList.ToString()}
        End If

        For Each crossingCxP As CrossingAccountDetailCxP In _listCrossingCxP
            If crossingCxP.CrossingAccountId = 0 Then
                errorList.AppendLine(String.Format(ResourceManager.GetString("FieldEmptyName"), "CrossingAccountId"))
            End If
            If crossingCxP.AccountPayableId = 0 Then
                errorList.AppendLine(String.Format(ResourceManager.GetString("FieldEmptyName"), "Id de Cuentas por Pagar"))
            End If
            If crossingCxP.MainAccountId = 0 Then
                errorList.AppendLine(String.Format(ResourceManager.GetString("FieldEmptyName"), "Cuenta Contable CxP"))
            End If
            If crossingCxP.CrossingValue = 0 Then
                errorList.AppendLine(String.Format(ResourceManager.GetString("FieldEmptyName"), "Valor de Cruce"))
            End If
            Dim accountPayable As AccountPayable = _accountPayableRepository.GetAccountPayableById(crossingCxP.AccountPayableId)
            If crossingCxP.ValueInCurrencyInvoice > accountPayable.Balance Then
                errorList.AppendLine(String.Format("El valor a cruzar de la factura CxP {0} no debe superar el valor del saldo", accountPayable.BillNumber))
            End If
        Next

        For Each crossingCxC As CrossingAccountDetailCxC In _listCrossingCxC
            If crossingCxC.CrossingAccountId = 0 Then
                errorList.AppendLine(String.Format(ResourceManager.GetString("FieldEmptyName"), "CrossingAccountId"))
            End If
            If crossingCxC.AccountReceivableId = 0 Then
                errorList.AppendLine(String.Format(ResourceManager.GetString("FieldEmptyName"), "Id de Recibos de Caja"))
            End If
            If crossingCxC.MainAccountId = 0 Then
                errorList.AppendLine(String.Format(ResourceManager.GetString("FieldEmptyName"), "Cuenta Contable CxP"))
            End If
            If crossingCxC.CrossingValue = 0 Then
                errorList.AppendLine(String.Format(ResourceManager.GetString("FieldEmptyName"), "Valor de Cruce"))
            End If
            Dim accountReceivable As AccountReceivable = _accountReceivableRepository.GetAccountReceivableById(crossingCxC.AccountReceivableId)
            If crossingCxC.ValueInCurrencyInvoice > accountReceivable.AccountReceivableAccounting.Where(Function(x) x.Id = crossingCxC.AccountReceivableAccountingId).Cast(Of AccountReceivableAccounting).FirstOrDefault().Balance Then
                errorList.AppendLine(String.Format("El valor a cruzar de la factura CxC {0} no debe superar el valor del saldo", accountReceivable.InvoiceNumber))
            End If
        Next

        If errorList.Length > 0 Then
            Return New ActionResult(Of CrossingAccount) With {.StateResult = False, .Message = errorList.ToString()}
        Else
            Return New ActionResult(Of CrossingAccount) With {.StateResult = True, .ObjectEmbbeded = crossingAccount}
        End If

        'validar que el tercero tenga las facturas asociadas
    End Function

    ''' <summary>
    ''' valida los saldos de las facturas contra el valor pagado en la programacion de pagos
    ''' </summary>
    ''' <param name="schedulePayment">The schedule payment.</param>
    ''' <returns></returns>
    Public Function ValidateSchedulePayment(ByVal schedulePayment As List(Of SP_SchedulePayment_Result)) As ActionResult(Of String) Implements ITreasuryServices.ValidateSchedulePayment
        Dim listError As New StringBuilder()
        Dim _schedulePayment = schedulePayment.Where(Function(x) x.PayValue <> 0).Cast(Of SP_SchedulePayment_Result).ToList()

        For Each sp As SP_SchedulePayment_Result In _schedulePayment
            If sp.PayValue > sp.BalanceShare Then
                listError.AppendLine(String.Format(ResourceManager.GetString("InvoiceValueBalanceError2", MODULE_NAME), sp.Invoice, sp.Share, String.Concat(sp.SupplierCode, " - ", sp.SupplierName)))
            End If
        Next

        If listError.Length > 0 Then
            Return New ActionResult(Of String) With {.StateResult = False, .Message = listError.ToString()}
        Else
            Return New ActionResult(Of String) With {.StateResult = True}
        End If
    End Function

    Public Function ValidateSchedulePayment(ByVal schedulePayment As List(Of SchedulePaymentDetail)) As ActionResult(Of String) Implements ITreasuryServices.ValidateSchedulePayment
        Dim listError As New StringBuilder()
        For Each sp As SchedulePaymentDetail In schedulePayment
            If sp.AmountPaid > sp.BalanceShare Then
                listError.AppendLine(String.Format(ResourceManager.GetString("InvoiceValueBalanceError2", MODULE_NAME), sp.Invoice, sp.Share, String.Concat(sp.SupplierCode, " - ", sp.SupplierName)))
            End If
        Next

        If listError.Length > 0 Then
            Return New ActionResult(Of String) With {.StateResult = False, .Message = listError.ToString()}
        Else
            Return New ActionResult(Of String) With {.StateResult = True}
        End If
    End Function

    ''' <summary>
    ''' Valida el guardar de las consignaciones
    ''' </summary>
    Public Function ValidateConsignmentSave(consignment As Consignment) As ActionResult(Of String) Implements ITreasuryServices.ValidateConsignmentSave
        Dim errorList As New StringBuilder()
        With consignment
            If .DocumentDate = Nothing Then
                errorList.AppendLine(String.Format(ResourceManager.GetString("FieldEmptyName"), "Fecha del Documento"))
            End If
            If .EntityBankAccountId = 0 Then
                errorList.AppendLine(String.Format(ResourceManager.GetString("FieldEmptyName"), "Cuenta Bancaria"))
            End If
            If .MainAccountId = 0 Then
                errorList.AppendLine(String.Format(ResourceManager.GetString("FieldEmptyName"), "Cuenta Contable"))
            End If
            If .Value = 0 Then
                errorList.AppendLine(String.Format(ResourceManager.GetString("FieldEmptyName"), "Valor Consignar"))
            End If
            If .Status = 0 Then
                errorList.AppendLine(String.Format(ResourceManager.GetString("FieldEmptyName"), "Estado"))
            End If
            If .OperativeUnitId = 0 Then
                errorList.AppendLine(String.Format(ResourceManager.GetString("FieldEmptyName"), "Unidad Operativa"))
            End If

            If .ConsignmentDetail Is Nothing OrElse .ConsignmentDetail.Count = 0 Then
                errorList.AppendLine("No se encontraron cajas para efectuar la consignación")
            Else
                If .Value = 0 Then
                    errorList.AppendLine("El valor de consignación no puede ser 0")
                End If

                Dim consignmentValue = .ConsignmentDetail.Sum(Function(d) d.ValueInCurrencyHeader)
                If .Value <> consignmentValue Then
                    errorList.AppendLine("El valor de consignación no corresponde con la suma de sus detalles")
                End If

                For Each detail As ConsignmentDetail In .ConsignmentDetail
                    Dim cashRegister As CashRegisters = _cashRegisterRepository.GetCashRegisterById(detail.CashRegisterId)
                    If cashRegister.Type = 1 Then
                        errorList.AppendLine(String.Format("La caja {0} debe ser de tipo mayor", cashRegister.Code))
                    End If
                    If detail.CashRegisterId = 0 Then
                        errorList.AppendLine(String.Format(ResourceManager.GetString("FieldEmptyName"), "Caja detalle"))
                    End If
                    If detail.MainAccountId = 0 Then
                        errorList.AppendLine(String.Format(ResourceManager.GetString("FieldEmptyName"), "Cuenta contable detalle"))
                    End If
                    If detail.Value = 0 Then
                        errorList.AppendLine(String.Format(ResourceManager.GetString("FieldEmptyName"), "Valor detalle"))
                    End If
                Next
            End If

        End With
        If errorList.Length <> 0 Then
            Return New ActionResult(Of String) With {.StateResult = False, .Message = errorList.ToString()}
        End If
        Return New ActionResult(Of String) With {.StateResult = True}
    End Function

    ''' <summary>
    ''' Valida el Confirmar de las consignaciones
    ''' </summary>
    Public Function ValidateConsignmentConfirm(consigment As Consignment) As ActionResult(Of Consignment) Implements ITreasuryServices.ValidateConsignmentConfirm
        Dim errorList As New StringBuilder()
        Dim validateSave As ActionResult(Of String) = Me.ValidateConsignmentSave(consigment)
        If Not validateSave.StateResult Then
            Return New ActionResult(Of Consignment) With {.StateResult = False, .Message = validateSave.Message}
        End If
        For Each detail As ConsignmentDetail In consigment.ConsignmentDetail
            Dim cashRegister As CashRegisters = _cashRegisterRepository.GetCashRegisterById(detail.CashRegisterId)
            If cashRegister.CurrentBalance < detail.Value Then
                errorList.AppendLine(String.Format("El saldo de la caja {0} es menor al valor a consignar", cashRegister.Code))
            End If
        Next
        If errorList.Length <> 0 Then
            Return New ActionResult(Of Consignment) With {.StateResult = False, .Message = errorList.ToString()}
        End If
        Return New ActionResult(Of Consignment) With {.StateResult = True}
    End Function

    Public Sub ValidateCheck(ByVal checkBookId As Integer, ByVal checkNumber As Long)
        Dim errorList As New StringBuilder()
        ''2-Revisar que el cheque por el que se va a hacer el cambio no este bloqueado
        'Dim _checkBlock As CheckBlock = _checkBlockRepository.GetCheckBlockByIdCheckBookAndNumber(checkBook.ObjectEmbbeded.Id, CheckCashing.NextCheckNumber)
        'If _checkBlock IsNot Nothing AndAlso _checkBlock.Id > 0 Then
        '    errorList.AppendLine("El cheque a reemplazar no se encuentra activo, por favor seleccione otro")
        'End If

        ''3-Revisar que el cheque no se encuentre anulado


        ''4-Si el cheque a reemplazar se encuentra en espera entonces lo eliminamos de la tabla de espera
        'Dim _outstandingCheck As OutstandingChecks = _outstandingCheckRepository.GetOutstandingCheckByCheckBookIdAndCheckNumber(checkBook.ObjectEmbbeded.Id, CheckCashing.NextCheckNumber)
        'If _outstandingCheck IsNot Nothing AndAlso _outstandingCheck.Id > 0 Then
        '    _outstandingCheck.MarkAsDeleted()
        '    _outstandingCheckRepository.DeleteEntity(_outstandingCheck)
        'End If
    End Sub

#Region "Notas de Tesoreria"

#Region "Métodos"
    ''' <summary>
    ''' Validates the treasury note.
    ''' </summary>
    Public Function ValidateTreasuryNoteSave(ByVal treasuryNote As TreasuryNote) As ActionResult Implements ITreasuryServices.ValidateTreasuryNoteSave
        Dim errorList As New StringBuilder()
        If String.IsNullOrEmpty(treasuryNote.NoteDate) Then
            errorList.AppendLine(String.Format(ResourceManager.GetString("FieldEmptyName"), "Fecha del Documento"))
        End If
        If treasuryNote.NoteType = 0 Then
            errorList.AppendLine(String.Format(ResourceManager.GetString("FieldEmptyName"), "Tipo de Nota"))
        End If
        If treasuryNote.Description = String.Empty Then
            errorList.AppendLine(String.Format(ResourceManager.GetString("FieldEmptyName"), "Descripción"))
        End If
        If treasuryNote.Status = 0 Then
            errorList.AppendLine(String.Format(ResourceManager.GetString("FieldEmptyName"), "Estado"))
        End If
        If (treasuryNote.EntityBankAccountId IsNot Nothing AndAlso treasuryNote.EntityBankAccountId <> 0) OrElse (treasuryNote.CashRegisterId IsNot Nothing AndAlso treasuryNote.CashRegisterId <> 0) Then
            If treasuryNote.MainAccountId Is Nothing OrElse treasuryNote.MainAccountId = 0 Then
                errorList.AppendLine(String.Format(ResourceManager.GetString("FieldEmptyName"), "Cuenta Contable"))
            Else
                Dim mainAccount As MainAccounts = _mainAccountRepository.GetAccountById(treasuryNote.MainAccountId, False)
                If mainAccount.HandlesCostCenter AndAlso (treasuryNote.CostCenterId Is Nothing OrElse treasuryNote.CostCenterId = 0) Then
                    errorList.AppendLine(String.Format(ResourceManager.GetString("FieldEmptyName"), "Centro de Costo"))
                ElseIf Not mainAccount.HandlesCostCenter AndAlso treasuryNote.CostCenterId IsNot Nothing Then
                    errorList.AppendLine("El centro de costo debe ser nulo")
                End If
            End If
        End If

        If treasuryNote.NoteType = 3 Then
            'Se cambia esta consulta porque se verifico para que mas utilizaban el objeto y solo lo utilizan para realizar validaciones lo cual estaba generando error en pitalito bug No. 8018 Att: Carlos Mario
            'Dim voucherTransaction As VoucherTransaction = _voucherTransactionRepository.GetVoucherTransactionById(treasuryNote.VoucherTransactionId, False)

            Dim voucherTransaction As VoucherTransaction = _voucherTransactionRepository.GetVoucherTransactionSimpleById(treasuryNote.VoucherTransactionId)
            If voucherTransaction.IdRefund IsNot Nothing AndAlso voucherTransaction.IdRefund <> 0 Then
                Dim refund As Refunds = _refundRepository.ValidateRefundById(voucherTransaction.IdRefund)
                If refund IsNot Nothing AndAlso refund.Id > 0 Then
                    errorList.AppendLine(String.Format("El comprobante de egreso seleccionado ya está preparado para hacer un reembolso. Ver reembolso : {0}", refund.Code))
                End If
            End If
            If treasuryNote.NoteDate < voucherTransaction.DocumentDate.Date Then
                errorList.AppendLine("la fecha de la nota no debe ser inferior a la fecha del comprobante de egreso")
            End If
        End If
        If {4, 7}.Contains(treasuryNote.NoteType) Then
            Dim cashReceipt As CashReceipts = Nothing
            If treasuryNote.CashReceipts Is Nothing Then
                cashReceipt = _cashRecepitRepository.GetCashReceiptsById(treasuryNote.CashReceiptId)
            Else
                cashReceipt = treasuryNote.CashReceipts
            End If
            If treasuryNote.NoteDate < cashReceipt.DocumentDate.Date Then
                errorList.AppendLine("la fecha de la nota no debe ser inferior a la fecha del recibo de caja")
            End If
        End If
        If treasuryNote.NoteType = 6 Then
            Dim crossAccount As CrossingAccount = Nothing
            If treasuryNote.CrossingAccount Is Nothing Then
                crossAccount = _crossingAccountRepository.GetCrossingAccountById(treasuryNote.CrossingAccountId)
            Else
                crossAccount = treasuryNote.CrossingAccount
            End If
            If treasuryNote.NoteDate < crossAccount.DocumentDate.Date Then
                errorList.AppendLine("la fecha de la nota no debe ser inferior a la fecha del cruce CxC vs CxP")
            End If
        End If

        'If treasuryNote.Value IsNot Nothing AndAlso treasuryNote.Value = 0 Then
        '    errorList.AppendLine(ResourceManager.GetString("InvalidNoteValue", MODULE_NAME))
        'End If
        If errorList.Length > 0 Then
            Return New ActionResult With {.StateResult = False, .Message = errorList.ToString()}
        Else
            Return New ActionResult With {.StateResult = True}
        End If
    End Function

    ''' <summary>
    ''' Validates the treasury note.
    ''' </summary>
    Public Function ValidateTreasuryNoteConfirm(ByVal treasuryNote As TreasuryNote) As ActionResult Implements ITreasuryServices.ValidateTreasuryNoteConfirm
        Dim errorList As New StringBuilder()
        Dim validateSave As ActionResult = Me.ValidateTreasuryNoteSave(treasuryNote)
        If Not validateSave.StateResult Then
            Return New ActionResult With {.StateResult = False, .Message = validateSave.Message}
        End If
        '1-Que si se va a sacar plata de la cuenta bancaria de la cabecera entonces que tenga el saldo suficiente para cubrir ese gasto igual con la caja

        If (treasuryNote.EntityBankAccountId IsNot Nothing AndAlso treasuryNote.EntityBankAccountId <> 0 AndAlso treasuryNote.Nature = eNature.Credit) AndAlso treasuryNote.NoteType <> 7 Then
            Dim _entityBankAccount As EntityBankAccounts = _entityBankAccountRepository.GetEntityBankAccountById(treasuryNote.EntityBankAccountId)
            If _entityBankAccount IsNot Nothing AndAlso _entityBankAccount.Id > 0 Then
                If _entityBankAccount.CurrentBalance + _entityBankAccount.Quota < treasuryNote.Value Then
                    errorList.AppendLine(String.Format(ResourceManager.GetString("EntityAccountInsufficientBalanceParameter", MODULE_NAME), _entityBankAccount.Code))
                End If
            End If
        End If

        If (treasuryNote.CashRegisterId IsNot Nothing AndAlso treasuryNote.CashRegisterId <> 0 AndAlso treasuryNote.Nature = eNature.Credit) AndAlso treasuryNote.NoteType <> 7 Then
            Dim _cashRegister As CashRegisters = _cashRegisterRepository.GetCashRegisterById(treasuryNote.CashRegisterId)
            If _cashRegister IsNot Nothing AndAlso _cashRegister.Id > 0 Then
                If _cashRegister.CurrentBalance < treasuryNote.Value Then
                    errorList.AppendLine(ResourceManager.GetString("CashInsufficientBalance", MODULE_NAME))
                End If
            End If
        End If

        '2-Verificar que el valor de la cabecera sea equivalente a la suma de los valores de los detalles y que almenos haya un detalle
        If ((treasuryNote.EntityBankAccountId IsNot Nothing AndAlso treasuryNote.EntityBankAccountId <> 0) OrElse (treasuryNote.CashRegisterId IsNot Nothing AndAlso treasuryNote.CashRegisterId <> 0)) AndAlso treasuryNote.NoteType <> 7 Then
            If treasuryNote.TreasuryNoteDetail IsNot Nothing AndAlso treasuryNote.TreasuryNoteDetail.Count > 0 Then
                If treasuryNote.Value <> treasuryNote.GetValueNoteDetail() Then
                    errorList.AppendLine("El valor de la nota no coincide con la suma de los valores de los conceptos")
                End If
            Else
                errorList.AppendLine("La nota debe contener al menos un detalle")
            End If
        End If

        If errorList.Length > 0 Then
            Return New ActionResult With {.StateResult = False, .Message = errorList.ToString()}
        Else
            Return New ActionResult With {.StateResult = True}
        End If
    End Function
#End Region

#End Region

#Region "Generate Bank File"

    ''' <summary>
    ''' metodo para generar el archivo para pagos en bancos
    ''' </summary>
    ''' <param name="SchedulePayment"></param>
    ''' <param name="bankId"></param>
    ''' <returns></returns>
    Public Function GenerateBankFile(SchedulePayment As SchedulePayment, bankId As Integer, companyNIT As String, companyName As String, Optional optionalParameters As List(Of String) = Nothing) As ActionResult(Of String) Implements ITreasuryServices.GenerateBankFile
        Dim bank = _bankRepository.GetBankById(bankId, False)
        Select Case bank.BankFileCode
            Case "001" 'Bancolombia
                Dim result = GenerateBancolombiaFile(SchedulePayment, companyNIT, companyName)
                If result.StateResult = False Then
                    Return New ActionResult(Of String) With {.StateResult = False, .MessageResult = result.MessageResult}
                End If
                Return New ActionResult(Of String) With {.StateResult = True, .Message = result.Message, .ObjectEmbbeded = result.ObjectEmbbeded}
            Case "002" 'Av Villas
                Dim result = GenerateAvVillasFile(SchedulePayment, companyNIT, companyName, optionalParameters)
                If result.StateResult = False Then
                    Return New ActionResult(Of String) With {.StateResult = False, .MessageResult = result.MessageResult}
                End If
                Return New ActionResult(Of String) With {.StateResult = True, .Message = result.Message, .ObjectEmbbeded = result.ObjectEmbbeded}
            Case "003" ' Banco Popular
                Dim result = GeneratePopularFile(SchedulePayment, companyNIT, companyName)
                If result.StateResult = False Then
                    Return New ActionResult(Of String) With {.StateResult = False, .MessageResult = result.MessageResult}
                End If
                Return New ActionResult(Of String) With {.StateResult = True, .Message = result.Message, .ObjectEmbbeded = result.ObjectEmbbeded}
            Case "004" ' Banco Occidente
                Dim result = GenerateOccidenteFile(SchedulePayment, companyNIT, companyName)
                If result.StateResult = False Then
                    Return New ActionResult(Of String) With {.StateResult = False, .MessageResult = result.MessageResult}
                End If
                Return New ActionResult(Of String) With {.StateResult = True, .Message = result.Message, .ObjectEmbbeded = result.ObjectEmbbeded}
            Case "005" ' Banco BBVA
                Dim result = GenerateBBVAFile(SchedulePayment, companyNIT, companyName)
                If result.StateResult = False Then
                    Return New ActionResult(Of String) With {.StateResult = False, .MessageResult = result.MessageResult}
                End If
                Return New ActionResult(Of String) With {.StateResult = True, .Message = result.Message, .ObjectEmbbeded = result.ObjectEmbbeded}
            Case "006" ' Banco Davivienda
                Dim result = GenerateDaviviendaFile(SchedulePayment, companyNIT, companyName, optionalParameters)
                If result.StateResult = False Then
                    Return New ActionResult(Of String) With {.StateResult = False, .MessageResult = result.MessageResult}
                End If
                Return New ActionResult(Of String) With {.StateResult = True, .Message = result.Message, .ObjectEmbbeded = result.ObjectEmbbeded}
            Case "007" ' Banco Caja Social
                Dim result = GenerateCajaSocialFile(SchedulePayment, companyNIT, companyName)
                If result.StateResult = False Then
                    Return New ActionResult(Of String) With {.StateResult = False, .MessageResult = result.MessageResult}
                End If
                Return New ActionResult(Of String) With {.StateResult = True, .Message = result.Message, .ObjectEmbbeded = result.ObjectEmbbeded}
            Case "008" ' Banco de Bogotá
                Dim result = GenerateBancoBogotaFile(SchedulePayment, companyNIT, companyName)
                If result.StateResult = False Then
                    Return New ActionResult(Of String) With {.StateResult = False, .MessageResult = result.MessageResult}
                End If
                Return New ActionResult(Of String) With {.StateResult = True, .Message = result.Message, .ObjectEmbbeded = result.ObjectEmbbeded}
            Case "009" 'Bancolombia-SAP
                Dim result = GenerateBancolombiaFileSap(SchedulePayment, companyNIT, companyName)
                If result.StateResult = False Then
                    Return New ActionResult(Of String) With {.StateResult = False, .MessageResult = result.MessageResult}
                End If
                Return New ActionResult(Of String) With {.StateResult = True, .Message = result.Message, .ObjectEmbbeded = result.ObjectEmbbeded}
            Case "010" 'Banco Itaú
                Dim result = GenerateItauFile(SchedulePayment, companyNIT, companyName)
                If result.StateResult = False Then
                    Return New ActionResult(Of String) With {.StateResult = False, .MessageResult = result.MessageResult}
                End If
                Return New ActionResult(Of String) With {.StateResult = True, .Message = result.Message, .ObjectEmbbeded = result.ObjectEmbbeded}
            Case "011" 'Banco Cooperativo Coopcentral
                Dim result = GenerateCoopcentralFile(SchedulePayment, companyNIT, companyName)
                If result.StateResult = False Then
                    Return New ActionResult(Of String) With {.StateResult = False, .MessageResult = result.MessageResult}
                End If
                Return New ActionResult(Of String) With {.StateResult = True, .Message = result.Message, .ObjectEmbbeded = result.ObjectEmbbeded}
            Case "012" 'Scotiabank CRC
                Dim result = GenerateScotiabankFile(SchedulePayment)
                If Not result.StateResult Then
                    Return New ActionResult(Of String) With {.StateResult = False, .MessageResult = result?.MessageResult}
                End If
                Return New ActionResult(Of String) With {.StateResult = True, .Message = result.Message, .ObjectEmbbeded = result.ObjectEmbbeded}
            Case "013" 'Banco GNV Sudameris
                Dim result = GenerateBancoGNVSudameris(SchedulePayment, companyNIT, companyName)
                If Not result.StateResult Then
                    Return New ActionResult(Of String) With {.StateResult = False, .MessageResult = result?.MessageResult}
                End If
                Return New ActionResult(Of String) With {.StateResult = True, .Message = result.Message, .ObjectEmbbeded = result.ObjectEmbbeded}
            Case "014" 'Davivienda CRC
                Dim result = GenerateDaviviendaCRCFile(SchedulePayment, bankId)
                If Not result.StateResult Then
                    Return New ActionResult(Of String) With {.StateResult = False, .MessageResult = result?.MessageResult}
                End If
                Return New ActionResult(Of String) With {.StateResult = True, .Message = result.Message, .ObjectEmbbeded = result.ObjectEmbbeded}
            Case "015" 'Archivo plano BCT
                Dim result = GenerateBCTFile(SchedulePayment, bankId, companyNIT, companyName)
                If Not result.StateResult Then
                    Return New ActionResult(Of String) With {.StateResult = False, .MessageResult = result?.MessageResult}
                End If
                Return New ActionResult(Of String) With {.StateResult = True, .Message = result.Message, .ObjectEmbbeded = result.ObjectEmbbeded}
            Case Else
                Return New ActionResult(Of String) With {.StateResult = False, .Message = "No se encontro estructura de archivo para el banco " + bank.Code + " - " + bank.Name}
        End Select
    End Function

    ''' <summary>
    ''' metodo para generar archivo plano para bancolombia
    ''' </summary>
    ''' <param name="SchedulePayment"></param>
    ''' <param name="companyNIT"></param>
    ''' <param name="companyName"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function GenerateBancolombiaFile(SchedulePayment As SchedulePayment, companyNIT As String, companyName As String) As ActionResult(Of String)
        Try
            Dim result As New StringBuilder()
            Dim DateTransaction = Date.Now
            Dim DateNow As String = DateTransaction.ToString("yyyyMMdd")

            Dim listErrorsDetails As New List(Of String)

            If SchedulePayment IsNot Nothing Then
                Dim entityAccount = _entityBankAccountRepository.GetEntityBankAccountById(SchedulePayment.EntityBankAccountId)
                Dim listDetailSupplier = (From e In SchedulePayment.SchedulePaymentDetail Select e.SupplierId).Distinct().ToList()
                Dim totalItems = (From e In SchedulePayment.SchedulePaymentDetail Select e.SupplierId).Distinct().ToList().Count

                Dim lineHead As String = Utils.StringPad(1, 1, 0, Utils.PadType.STR_PAD_LEFT)
                lineHead &= Utils.StringPad(companyNIT, 15, 0, Utils.PadType.STR_PAD_LEFT)
                lineHead &= Utils.StringPad("I", 1, 0, Utils.PadType.STR_PAD_LEFT)
                lineHead &= Utils.StringPad("", 15, " ", Utils.PadType.STR_PAD_LEFT)
                lineHead &= Utils.StringPad(225, 3, 0, Utils.PadType.STR_PAD_LEFT)
                lineHead &= Utils.StringPad("PAGO PROVEEDORES", 10, 0, Utils.PadType.STR_PAD_LEFT)
                lineHead &= Utils.StringPad(DateNow, 8, 0, Utils.PadType.STR_PAD_LEFT)
                lineHead &= Utils.StringPad("A", 2, " ", Utils.PadType.STR_PAD_RIGHT)
                lineHead &= Utils.StringPad(DateNow, 8, 0, Utils.PadType.STR_PAD_LEFT)
                lineHead &= Utils.StringPad(totalItems, 6, 0, Utils.PadType.STR_PAD_LEFT)
                lineHead &= Utils.StringPad(0, 17, 0, Utils.PadType.STR_PAD_LEFT)
                lineHead &= Utils.StringPad(CDbl(SchedulePayment.SchedulePaymentDetail.Sum(Function(y) y.AmountPaid)), 17, 0, Utils.PadType.STR_PAD_LEFT)
                lineHead &= Utils.StringPad(entityAccount.Number.Trim.Replace("-", ""), 11, " ", Utils.PadType.STR_PAD_LEFT)
                lineHead &= Utils.StringPad(If(entityAccount.Type = 1, "S", "D"), 1, "", Utils.PadType.STR_PAD_LEFT)
                result.Append(lineHead)

                For i As Integer = 0 To listDetailSupplier.Count - 1 Step 1
                    'nit del beneficiario
                    Dim supplier = _supplierRepository.GetSupplierByIdWithSupplierBankAccount(listDetailSupplier(i))
                    Dim resultValidateSupplierAccount As ActionResult(Of SupplierBankAccount)

                    If supplier Is Nothing Then
                        Throw New ArgumentNullException("supplier")
                    End If

                    Dim schedulePaymentBankAccount = SchedulePayment?.SchedulePaymentBankAccount?.ToList().Find(Function(x) x.SupplierId = supplier?.Id)

                    'se busca si se guardaron datos en la nueva tabla de informacion bancaria para programacion de pago, de lo contrario sigue como se hacia.
                    If schedulePaymentBankAccount IsNot Nothing Then
                        Dim queryBank = supplier.SupplierBankAccount.ToList().Find(Function(x) x.Id = schedulePaymentBankAccount?.SupplierBankAccountId)
                        resultValidateSupplierAccount = New ActionResult(Of SupplierBankAccount) With {.StateResult = queryBank IsNot Nothing, .ObjectEmbbeded = queryBank}
                    Else
                        '    Valido las cuentas del proveedor
                        resultValidateSupplierAccount = ValidateSupplierAccount(supplier, entityAccount.IdBank)
                    End If

                    If resultValidateSupplierAccount.StateResult = False Then
                        listErrorsDetails.Add(resultValidateSupplierAccount.Message)
                        Continue For
                    End If

                    Dim account As SupplierBankAccount = resultValidateSupplierAccount.ObjectEmbbeded
                    Dim bank As Bank = _bankRepository.GetBankById(account.BankId)
                    If String.IsNullOrEmpty(bank.AchCode) Then
                        listErrorsDetails.Add("El banco " + bank.Code + " - " + bank.Name + " no tiene código CENIT")
                        Continue For
                    End If
                    Dim AchCode As Integer
                    If Not Integer.TryParse(bank.AchCode, AchCode) Then
                        listErrorsDetails.Add("El banco " + bank.Code + " - " + bank.Name + " no tiene código CENIT numérico")
                        Continue For
                    End If

                    Dim lineDet As String = vbCrLf

                    lineDet &= Utils.StringPad(6, 1, " ", Utils.PadType.STR_PAD_LEFT)
                    lineDet &= Utils.StringPad(supplier.ThirdParty.Nit, 15, " ", Utils.PadType.STR_PAD_RIGHT)
                    lineDet &= Utils.StringPad(supplier.ThirdParty.Name.ToUpper().ChangeCharacters, 30, " ", Utils.PadType.STR_PAD_RIGHT)
                    lineDet &= Utils.StringPad(bank.AchCode, 9, " ", Utils.PadType.STR_PAD_LEFT)
                    lineDet &= Utils.StringPad(account.Number.Trim().Replace("-", ""), 17, " ", Utils.PadType.STR_PAD_RIGHT)
                    lineDet &= Utils.StringPad(" ", 1, " ", Utils.PadType.STR_PAD_LEFT)
                    lineDet &= Utils.StringPad(37, 2, " ", Utils.PadType.STR_PAD_LEFT)
                    lineDet &= Utils.StringPad(CDbl(SchedulePayment.SchedulePaymentDetail.Where(Function(x) x.SupplierId = listDetailSupplier(i)).ToList().Sum(Function(y) y.AmountPaid)), 17, 0, Utils.PadType.STR_PAD_LEFT)
                    lineDet &= Utils.StringPad(0, 8, 0, Utils.PadType.STR_PAD_LEFT)
                    lineDet &= Utils.StringPad("PROVEEDOR" & supplier.ThirdParty.Name.ToUpper().ChangeCharacters, 21, " ", Utils.PadType.STR_PAD_RIGHT)
                    lineDet &= Utils.StringPad(" ", 6, " ", Utils.PadType.STR_PAD_LEFT)
                    result.Append(lineDet)
                Next
            End If

            If listErrorsDetails.Count > 0 Then
                Return New ActionResult(Of String) With {.StateResult = False, .MessageResult = listErrorsDetails}
            End If
            Return New ActionResult(Of String) With {.StateResult = True, .Message = "DF_" + DateTime.Now.ToString("ddMMyyyy") + "_Bancolombia", .ObjectEmbbeded = result.ToString()}
        Catch ex As Exception
            Return New ActionResult(Of String) With {.StateResult = False, .Message = ex.Message}
        End Try
    End Function

    ''' <summary>
    ''' metodo para generar archivo plano para Av villas
    ''' </summary>
    ''' <param name="SchedulePayment"></param>
    ''' <param name="companyNIT"></param>
    ''' <param name="companyName"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function GenerateAvVillasFile(SchedulePayment As SchedulePayment, companyNIT As String, companyName As String, optionalParameters As List(Of String)) As ActionResult(Of String)
        Try
            Dim result As New StringBuilder
            Dim entityAccount = _entityBankAccountRepository.GetEntityBankAccountById(SchedulePayment.EntityBankAccountId)

            'creo la cabecera del archivo
            'tipo de registro por defecto es 1
            Dim lineHead As String = Utils.StringPad(1, 1, 0, Utils.PadType.STR_PAD_LEFT)
            'cuenta cliente a debitar
            lineHead += Utils.StringPad(entityAccount.Number.Trim.Replace("-", ""), 17, " ", Utils.PadType.STR_PAD_RIGHT)
            'tipo de cuenta 1 = ahorros , 0 = corriente
            lineHead += Utils.StringPad(If(entityAccount.Type = 1, 1, 0), 1, "", Utils.PadType.STR_PAD_LEFT)
            'codigo del producto por defecto PP pago a proveedores
            lineHead += Utils.StringPad("PP", 2, "", Utils.PadType.STR_PAD_LEFT)
            'fecha
            lineHead += Utils.StringPad(DateTime.Now.ToString("yyyyMMdd"), 8, 0, Utils.PadType.STR_PAD_LEFT)
            'nit de la empresa
            lineHead += Utils.StringPad(companyNIT, 15, 0, Utils.PadType.STR_PAD_LEFT)
            'tipo de identificacion  nit = 03
            lineHead += Utils.StringPad("03", 2, "", Utils.PadType.STR_PAD_LEFT)
            'nombre de la empresa
            lineHead += Utils.StringPad(companyName.ToUpper(), 16, " ", Utils.PadType.STR_PAD_RIGHT)
            'plaza 
            lineHead += Utils.StringPad(optionalParameters.ElementAt(1), 4, 0, Utils.PadType.STR_PAD_RIGHT)
            'tipo registro = PPD
            lineHead += Utils.StringPad("PPD", 3, 0, Utils.PadType.STR_PAD_RIGHT)
            'seccion cliente
            lineHead += Utils.StringPad("", 6, 0, Utils.PadType.STR_PAD_RIGHT)
            'canal
            lineHead += Utils.StringPad(4, 1, 0, Utils.PadType.STR_PAD_RIGHT)

            result.AppendLine(lineHead)

            Dim listErrorsDetails As New List(Of String)
            Dim listDetailSupplier = (From e In SchedulePayment.SchedulePaymentDetail Select e.SupplierId).Distinct().ToList()
            For i As Integer = 0 To listDetailSupplier.Count - 1 Step 1

                Dim supplier = _supplierRepository.GetSupplierByIdWithSupplierBankAccount(listDetailSupplier(i))

                Dim resultValidateSupplierAccount As ActionResult(Of SupplierBankAccount)

                If supplier Is Nothing Then
                    Throw New ArgumentNullException("supplier")
                End If

                Dim schedulePaymentBankAccount = SchedulePayment?.SchedulePaymentBankAccount?.ToList().Find(Function(x) x.SupplierId = supplier?.Id)

                'se busca si se guardaron datos en la nueva tabla de informacion bancaria para programacion de pago, de lo contrario sigue como se hacia.
                If schedulePaymentBankAccount IsNot Nothing Then
                    Dim queryBank = supplier.SupplierBankAccount.ToList().Find(Function(x) x.Id = schedulePaymentBankAccount?.SupplierBankAccountId)
                    resultValidateSupplierAccount = New ActionResult(Of SupplierBankAccount) With {.StateResult = queryBank IsNot Nothing, .ObjectEmbbeded = queryBank}
                Else
                    '    Valido las cuentas del proveedor
                    resultValidateSupplierAccount = ValidateSupplierAccount(supplier, entityAccount.IdBank)
                End If

                If resultValidateSupplierAccount.StateResult = False Then
                    listErrorsDetails.Add(resultValidateSupplierAccount.Message)
                    Continue For
                End If

                Dim account As SupplierBankAccount = resultValidateSupplierAccount.ObjectEmbbeded

                'tipo de registro por defecto esta en 2
                Dim lineDetail As String = Utils.StringPad(2, 1, 0, Utils.PadType.STR_PAD_LEFT)
                'tipo de transaccion =======================================================================> averiguar como se obtiene
                lineDetail += Utils.StringPad(If(account.Type = 1, 32, 22), 2, "", Utils.PadType.STR_PAD_LEFT)

                Dim bank As Bank = Nothing
                bank = _bankRepository.GetBankById(account.BankId)
                If bank.CenitCode Is String.Empty Then
                    listErrorsDetails.Add("El banco " + bank.Code + " - " + bank.Name + " no tiene código CENIT")
                    Continue For
                End If
                'banco cuenta del beneficiario
                lineDetail += Utils.StringPad(bank.CenitCode, 4, 0, Utils.PadType.STR_PAD_LEFT)
                'plaza destino 
                lineDetail += Utils.StringPad(optionalParameters.ElementAt(2), 4, 0, Utils.PadType.STR_PAD_LEFT)

                'nit del beneficiario
                lineDetail += Utils.StringPad(supplier.ThirdParty.Nit, 15, " ", Utils.PadType.STR_PAD_RIGHT)
                'tipo de documento
                Dim documentType As String = "03"
                Select Case supplier.ThirdParty.Person.IdentificationType
                    Case 0 'cedula 
                        documentType = "01"
                    Case 1 ' cedula de extranjeria
                        documentType = "02"
                    Case 2 'tarjeta de identidad
                        documentType = "04"
                    Case 3 'registro civil
                        documentType = "05"
                    Case 4 'pasaporte
                        documentType = "10"
                    Case 7 'nit
                        documentType = "03"
                End Select
                lineDetail += Utils.StringPad(documentType, 2, "", Utils.PadType.STR_PAD_RIGHT)
                'numero cuenta beneficiario
                lineDetail += Utils.StringPad(account.Number.Trim.Replace("-", ""), 17, " ", Utils.PadType.STR_PAD_RIGHT)
                'tipo de cuenta 1 = ahorros , 0 = corriente
                lineDetail += Utils.StringPad(If(account.Type = 1, 1, 0), 1, "", Utils.PadType.STR_PAD_LEFT)
                'nombre del beneficiario
                lineDetail += Utils.StringPad(supplier.ThirdParty.Name.ToUpper().ChangeCharacters, 22, " ", Utils.PadType.STR_PAD_RIGHT)
                '
                lineDetail += Utils.StringPad(0, 1, "", Utils.PadType.STR_PAD_RIGHT)
                'valor de la transaccion
                lineDetail += Utils.StringPad(CDbl(SchedulePayment.SchedulePaymentDetail.Where(Function(x) x.SupplierId = listDetailSupplier(i)).ToList().Sum(Function(y) y.AmountPaid)), 18, 0, Utils.PadType.STR_PAD_LEFT)
                'bandera por defecto 1
                lineDetail += Utils.StringPad(1, 1, "", Utils.PadType.STR_PAD_RIGHT)
                'referencia ===========================================================================================================
                lineDetail += Utils.StringPad("", 16, " ", Utils.PadType.STR_PAD_RIGHT)
                result.AppendLine(lineDetail)
            Next
            'totales
            Dim lineTotal As String = Utils.StringPad(4, 1, "", Utils.PadType.STR_PAD_RIGHT)
            'numero de registros
            Dim totalItems = (From e In SchedulePayment.SchedulePaymentDetail Select e.SupplierId).Distinct().ToList().Count
            lineTotal += Utils.StringPad(totalItems, 8, 0, Utils.PadType.STR_PAD_LEFT)
            'valor total
            lineTotal += Utils.StringPad(SchedulePayment.SchedulePaymentDetail.Sum(Function(y) y.AmountPaid), 18, 0, Utils.PadType.STR_PAD_LEFT)

            result.AppendLine(lineTotal)

            If listErrorsDetails.Count > 0 Then
                Return New ActionResult(Of String) With {.StateResult = False, .MessageResult = listErrorsDetails}
            End If
            Return New ActionResult(Of String) With {.StateResult = True, .Message = "DF_" + DateTime.Now.ToString("ddMMyyyy") + "_Banco_AvVillas", .ObjectEmbbeded = result.ToString()}
        Catch ex As Exception
            Return New ActionResult(Of String) With {.StateResult = False, .Message = ex.Message}
        End Try
    End Function

    ''' <summary>
    ''' metodo para generar archivo plano para el banco popular
    ''' </summary>
    ''' <param name="SchedulePayment"></param>
    ''' <param name="companyNIT"></param>
    ''' <param name="companyName"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function GeneratePopularFile(SchedulePayment As SchedulePayment, companyNIT As String, companyName As String) As ActionResult(Of String)
        Try
            Dim result As New StringBuilder
            Dim entityAccount = _entityBankAccountRepository.GetEntityBankAccountById(SchedulePayment.EntityBankAccountId)
            'creo la cabecera del archivo
            'tipo de registro por defecto es 01 por defecto
            Dim lineHead As String = Utils.StringPad("01", 2, 0, Utils.PadType.STR_PAD_LEFT)
            'Fecha transmisión 
            lineHead += Utils.StringPad(DateTime.Now.ToString("yyyyddMM"), 8, 0, Utils.PadType.STR_PAD_LEFT)
            'Nombre cliente
            lineHead += Utils.StringPad(companyName.ToUpper(), 16, " ", Utils.PadType.STR_PAD_RIGHT)
            'cuenta cliente
            lineHead += Utils.StringPad(entityAccount.Number.Trim.Replace("-", ""), 12, " ", Utils.PadType.STR_PAD_RIGHT)
            'nit
            lineHead += Utils.StringPad(companyNIT, 10, " ", Utils.PadType.STR_PAD_RIGHT)
            'numero de registros
            Dim totalItems = (From e In SchedulePayment.SchedulePaymentDetail Select e.SupplierId).Distinct().ToList().Count
            lineHead += Utils.StringPad(totalItems, 6, 0, Utils.PadType.STR_PAD_LEFT)
            'valor total
            lineHead += Utils.StringPad(CDbl(SchedulePayment.SchedulePaymentDetail.Sum(Function(y) y.AmountPaid)), 18, 0, Utils.PadType.STR_PAD_LEFT)
            'nombre archivo
            lineHead += Utils.StringPad("", 12, " ", Utils.PadType.STR_PAD_RIGHT)
            'descripcion lote
            lineHead += Utils.StringPad("PROVEEDOR", 10, " ", Utils.PadType.STR_PAD_RIGHT)
            'campo vacio de 70 espacios
            lineHead += Utils.StringPad("", 70, " ", Utils.PadType.STR_PAD_RIGHT)
            'campo se llena con una V
            lineHead += Utils.StringPad("V", 2, " ", Utils.PadType.STR_PAD_LEFT)
            'campo vacio de 41 espacios
            lineHead += Utils.StringPad("", 41, " ", Utils.PadType.STR_PAD_RIGHT)
            result.AppendLine(lineHead)

            Dim listErrorsDetails As New List(Of String)
            Dim listDetailSupplier = (From e In SchedulePayment.SchedulePaymentDetail Select e.SupplierId).Distinct().ToList()

            For i As Integer = 0 To listDetailSupplier.Count - 1 Step 1
                'tipo de registro por defecto esta en 02
                Dim lineDetail As String = Utils.StringPad("02", 2, 0, Utils.PadType.STR_PAD_LEFT)
                'nit del beneficiario
                Dim supplier = _supplierRepository.GetSupplierByIdWithSupplierBankAccount(listDetailSupplier(i))

                Dim resultValidateSupplierAccount As ActionResult(Of SupplierBankAccount)

                If supplier Is Nothing Then
                    Throw New ArgumentNullException("supplier")
                End If

                lineDetail += Utils.StringPad(supplier.ThirdParty.Nit, 15, " ", Utils.PadType.STR_PAD_RIGHT)
                'valor de la transaccion
                lineDetail += Utils.StringPad(CDbl(SchedulePayment.SchedulePaymentDetail.Where(Function(x) x.SupplierId = listDetailSupplier(i)).ToList().Sum(Function(y) y.AmountPaid)), 18, 0, Utils.PadType.STR_PAD_LEFT)
                'nombre del beneficiario
                lineDetail += Utils.StringPad(supplier.ThirdParty.Name.ToUpper(), 22, " ", Utils.PadType.STR_PAD_RIGHT)

                Dim schedulePaymentBankAccount = SchedulePayment?.SchedulePaymentBankAccount?.ToList().Find(Function(x) x.SupplierId = supplier?.Id)

                'se busca si se guardaron datos en la nueva tabla de informacion bancaria para programacion de pago, de lo contrario sigue como se hacia.
                If schedulePaymentBankAccount IsNot Nothing Then
                    Dim queryBank = supplier.SupplierBankAccount.ToList().Find(Function(x) x.Id = schedulePaymentBankAccount?.SupplierBankAccountId)
                    resultValidateSupplierAccount = New ActionResult(Of SupplierBankAccount) With {.StateResult = queryBank IsNot Nothing, .ObjectEmbbeded = queryBank}
                Else
                    '    Valido las cuentas del proveedor
                    resultValidateSupplierAccount = ValidateSupplierAccount(supplier, entityAccount.IdBank)
                End If

                If resultValidateSupplierAccount.StateResult = False Then
                    listErrorsDetails.Add(resultValidateSupplierAccount.Message)
                    Continue For
                End If

                Dim account As SupplierBankAccount = resultValidateSupplierAccount.ObjectEmbbeded

                Dim bank As Bank = Nothing
                bank = _bankRepository.GetBankById(account.BankId)
                'banco cuenta del beneficiario
                lineDetail += Utils.StringPad(bank.AchCode, 9, 0, Utils.PadType.STR_PAD_LEFT)
                'tipo de cuenta 32 = ahorros , 22 = corriente
                lineDetail += Utils.StringPad(If(account.Type = 1, 32, 22), 2, 0, Utils.PadType.STR_PAD_LEFT)
                'numero cuenta beneficiario
                lineDetail += Utils.StringPad(account.Number.Trim.Replace("-", ""), 17, " ", Utils.PadType.STR_PAD_RIGHT)
                'nit de la empresa que origina
                lineDetail += Utils.StringPad(companyNIT, 15, " ", Utils.PadType.STR_PAD_RIGHT)
                'reservado
                lineDetail += Utils.StringPad("", 2, " ", Utils.PadType.STR_PAD_RIGHT)
                'descripcion lote
                lineDetail += Utils.StringPad("PROVEEDOR", 10, " ", Utils.PadType.STR_PAD_RIGHT)
                'referencia = 0
                lineDetail += Utils.StringPad(0, 53, " ", Utils.PadType.STR_PAD_RIGHT)
                'campo se llena con una V
                lineDetail += Utils.StringPad("V", 2, " ", Utils.PadType.STR_PAD_RIGHT)
                'respuesta
                lineDetail += Utils.StringPad("", 40, " ", Utils.PadType.STR_PAD_LEFT)
                result.AppendLine(lineDetail)
            Next

            If listErrorsDetails.Count > 0 Then
                Return New ActionResult(Of String) With {.StateResult = False, .MessageResult = listErrorsDetails}
            End If
            Return New ActionResult(Of String) With {.StateResult = True, .Message = "DF_" + DateTime.Now.ToString("ddMMyyyy") + "_Banco_Popular", .ObjectEmbbeded = result.ToString()}
        Catch ex As Exception
            Return New ActionResult(Of String) With {.StateResult = False, .Message = ex.Message}
        End Try
    End Function

    ''' <summary>
    ''' metodo para generar archivo plano para el banco occidente
    ''' </summary>
    ''' <param name="SchedulePayment"></param>
    ''' <param name="companyNIT"></param>
    ''' <param name="companyName"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function GenerateOccidenteFile(SchedulePayment As SchedulePayment, companyNIT As String, companyName As String) As ActionResult(Of String)
        Try
            Dim result As New StringBuilder
            Dim entityAccount = _entityBankAccountRepository.GetEntityBankAccountById(SchedulePayment.EntityBankAccountId)
            'creo la cabecera del archivo
            'tipo de registro por defecto es 10000 por defecto
            Dim lineHead As String = Utils.StringPad(1, 5, 0, Utils.PadType.STR_PAD_RIGHT)
            'fecha
            lineHead += Utils.StringPad(DateTime.Now.ToString("yyyyMMdd"), 8, 0, Utils.PadType.STR_PAD_LEFT)
            'numero de registros
            Dim totalItems = (From e In SchedulePayment.SchedulePaymentDetail Select e.SupplierId).Distinct().ToList().Count
            lineHead += Utils.StringPad(totalItems, 4, 0, Utils.PadType.STR_PAD_LEFT)
            'valor total
            lineHead += Utils.StringPad(CDbl((SchedulePayment.SchedulePaymentDetail.Sum(Function(y) y.AmountPaid) * 100)), 18, 0, Utils.PadType.STR_PAD_LEFT)
            'cuenta cliente
            lineHead += Utils.StringPad(entityAccount.Number.Trim.Replace("-", ""), 16, 0, Utils.PadType.STR_PAD_LEFT)
            'relleno
            lineHead += Utils.StringPad("", 148, 0, Utils.PadType.STR_PAD_LEFT)

            result.AppendLine(lineHead)

            Dim listErrorsDetails As New List(Of String)
            Dim listDetailSupplier As List(Of Integer) = (From e In SchedulePayment.SchedulePaymentDetail Select e.SupplierId).Distinct().ToList()

            For i As Integer = 0 To listDetailSupplier.Count - 1 Step 1
                'tipo de registro por defecto esta en 2
                Dim lineDetail As String = Utils.StringPad(2, 1, 0, Utils.PadType.STR_PAD_LEFT)
                'orden de los items
                lineDetail += Utils.StringPad(i + 1, 4, 0, Utils.PadType.STR_PAD_LEFT)
                'nit del beneficiario
                Dim supplier = _supplierRepository.GetSupplierByIdWithSupplierBankAccount(listDetailSupplier(i))

                Dim resultValidateSupplierAccount As ActionResult(Of SupplierBankAccount)

                If supplier Is Nothing Then
                    Throw New ArgumentNullException("supplier")
                End If

                Dim schedulePaymentBankAccount = SchedulePayment?.SchedulePaymentBankAccount?.ToList().Find(Function(x) x.SupplierId = supplier?.Id)

                'se busca si se guardaron datos en la nueva tabla de informacion bancaria para programacion de pago, de lo contrario sigue como se hacia.
                If schedulePaymentBankAccount IsNot Nothing Then
                    Dim queryBank = supplier.SupplierBankAccount.ToList().Find(Function(x) x.Id = schedulePaymentBankAccount?.SupplierBankAccountId)
                    resultValidateSupplierAccount = New ActionResult(Of SupplierBankAccount) With {.StateResult = queryBank IsNot Nothing, .ObjectEmbbeded = queryBank}
                Else
                    '    Valido las cuentas del proveedor
                    resultValidateSupplierAccount = ValidateSupplierAccount(supplier, entityAccount.IdBank)
                End If

                If resultValidateSupplierAccount.StateResult = False Then
                    listErrorsDetails.Add(resultValidateSupplierAccount.Message)
                    Continue For
                End If
                Dim account As SupplierBankAccount = resultValidateSupplierAccount.ObjectEmbbeded
                'numero cuenta origen
                lineDetail += Utils.StringPad(entityAccount.Number.Trim.Replace("-", ""), 16, 0, Utils.PadType.STR_PAD_LEFT)
                'nombre del beneficiario
                lineDetail += Utils.StringPad(supplier.ThirdParty.Name.CleanSpecialChars().ToUpper(), 30, " ", Utils.PadType.STR_PAD_RIGHT)
                Dim bank As Bank = Nothing
                bank = _bankRepository.GetBankById(account.BankId)
                'If bank.CenitCode Is String.Empty Then
                '    listErrorsDetails.Add("El banco " + bank.Code + " - " + bank.Name + " no tiene código CENIT")
                '    Continue For
                'End If

                If supplier.ThirdParty.PersonType = 1 Then
                    'nit del beneficiario Natural
                    lineDetail += Utils.StringPad(supplier.ThirdParty.Nit, 11, 0, Utils.PadType.STR_PAD_LEFT)
                ElseIf supplier.ThirdParty.PersonType = 2 Then
                    'nit del beneficiario Juridico
                    lineDetail += Utils.StringPad(String.Concat(supplier.ThirdParty.Nit, supplier.ThirdParty.DigitVerification), 11, 0, Utils.PadType.STR_PAD_LEFT)
                Else
                    listErrorsDetails.Add("El Proveedor " & supplier.Code & " - " & supplier.Name & " no tiene especificado un Tipo de persona correcto: 1-Natural 2-Juridico")
                End If
                'banco cuenta del beneficiario
                lineDetail += Utils.StringPad(bank.CenitCode, 4, 0, Utils.PadType.STR_PAD_LEFT)
                'fecha
                lineDetail += Utils.StringPad(DateTime.Now.ToString("yyyyMMdd"), 8, 0, Utils.PadType.STR_PAD_LEFT)
                'codigo forma de pago
                If account.BankId = entityAccount.IdBank Then
                    lineDetail += Utils.StringPad(2, 1, 0, Utils.PadType.STR_PAD_LEFT)
                Else
                    lineDetail += Utils.StringPad(3, 1, 0, Utils.PadType.STR_PAD_LEFT)
                End If
                'valor de la transaccion
                lineDetail += Utils.StringPad(CDbl((SchedulePayment.SchedulePaymentDetail.Where(Function(x) x.SupplierId = listDetailSupplier(i)).ToList().Sum(Function(y) y.AmountPaid) * 100)), 15, 0, Utils.PadType.STR_PAD_LEFT)
                'numero cuenta beneficiario
                lineDetail += Utils.StringPad(account.Number, 16, " ", Utils.PadType.STR_PAD_RIGHT)
                'comprobante
                lineDetail += Utils.StringPad(0, 12, " ", Utils.PadType.STR_PAD_LEFT)
                'tipo de cuenta A = ahorros , C = corriente
                lineDetail += Utils.StringPad(If(account.Type = 1, "A", "C"), 1, 0, Utils.PadType.STR_PAD_LEFT)
                'informacion adicional
                lineDetail += Utils.StringPad(0, 80, " ", Utils.PadType.STR_PAD_RIGHT)

                result.AppendLine(lineDetail)
            Next



            'totales
            Dim lineTotal As String = Utils.StringPad(39999, 5, "", Utils.PadType.STR_PAD_RIGHT)
            'total de items
            lineTotal += Utils.StringPad(totalItems, 4, 0, Utils.PadType.STR_PAD_LEFT)
            'valor total
            lineTotal += Utils.StringPad(CDbl((SchedulePayment.SchedulePaymentDetail.Sum(Function(y) y.AmountPaid) * 100)), 18, 0, Utils.PadType.STR_PAD_LEFT)
            'informacion adicional
            lineTotal += Utils.StringPad("", 172, 0, Utils.PadType.STR_PAD_RIGHT)
            result.AppendLine(lineTotal)

            If listErrorsDetails.Count > 0 Then
                Return New ActionResult(Of String) With {.StateResult = False, .MessageResult = listErrorsDetails}
            End If

            Dim enc As Encoding = New UTF8Encoding(True, True)
            Dim bytes() As Byte = enc.GetBytes(result.ToString())


            Return New ActionResult(Of String) With {.StateResult = True, .Message = "DF_" + DateTime.Now.ToString("ddMMyyyy") + "_Banco_Occidente", .ObjectEmbbeded = enc.GetString(bytes)}
        Catch ex As Exception
            Return New ActionResult(Of String) With {.StateResult = False, .Message = ex.Message}
        End Try
    End Function

    ''' <summary>
    ''' metodo para generar plano para el banco BBVA
    ''' </summary>
    ''' <param name="SchedulePayment"></param>
    ''' <param name="companyNIT"></param>
    ''' <param name="companyName"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function GenerateBBVAFile(SchedulePayment As SchedulePayment, companyNIT As String, companyName As String) As ActionResult(Of String)
        Try
            Dim result As New StringBuilder
            Dim listErrorsDetails As New List(Of String)
            Dim listDetailSupplier = (From e In SchedulePayment.SchedulePaymentDetail Select e.SupplierId).Distinct().ToList()

            Dim entityAccount = _entityBankAccountRepository.GetEntityBankAccountById(SchedulePayment.EntityBankAccountId)

            For i As Integer = 0 To listDetailSupplier.Count - 1 Step 1
                Dim supplier = _supplierRepository.GetSupplierByIdWithSupplierBankAccount(listDetailSupplier(i))

                Dim resultValidateSupplierAccount As ActionResult(Of SupplierBankAccount)

                If supplier Is Nothing Then
                    Throw New ArgumentNullException("supplier")
                End If

                Dim schedulePaymentBankAccount = SchedulePayment?.SchedulePaymentBankAccount?.ToList().Find(Function(x) x.SupplierId = supplier?.Id)

                'se busca si se guardaron datos en la nueva tabla de informacion bancaria para programacion de pago, de lo contrario sigue como se hacia.
                If schedulePaymentBankAccount IsNot Nothing Then
                    Dim queryBank = supplier.SupplierBankAccount.ToList().Find(Function(x) x.Id = schedulePaymentBankAccount?.SupplierBankAccountId)
                    resultValidateSupplierAccount = New ActionResult(Of SupplierBankAccount) With {.StateResult = queryBank IsNot Nothing, .ObjectEmbbeded = queryBank}
                Else
                    '    Valido las cuentas del proveedor
                    resultValidateSupplierAccount = ValidateSupplierAccount(supplier, entityAccount.IdBank)
                End If

                If resultValidateSupplierAccount.StateResult = False Then
                    listErrorsDetails.Add(resultValidateSupplierAccount.Message)
                    Continue For
                End If

                Dim account As SupplierBankAccount = resultValidateSupplierAccount.ObjectEmbbeded

                'tipo de documento
                Dim documentType As String = "03"
                Select Case supplier.ThirdParty.Person.IdentificationType
                    Case 0 'cedula 
                        documentType = "01"
                    Case 1 ' cedula de extranjeria
                        documentType = "02"
                    Case 2 'tarjeta de identidad
                        documentType = "04"
                    Case 4 'pasaporte
                        documentType = "05"
                    Case 7 'nit
                        documentType = "03"
                End Select
                Dim lineDetail As String
                'numero de documento
                If documentType = "03" Then
                    If supplier.ThirdParty.PersonType = 1 Then 'natural
                        documentType = documentType.Replace("3", "1")
                        lineDetail = Utils.StringPad(documentType, 2, "", Utils.PadType.STR_PAD_RIGHT)
                        lineDetail += Utils.StringPad(String.Concat(supplier.ThirdParty.Nit, 0), 16, 0, Utils.PadType.STR_PAD_LEFT)
                    Else 'juridico
                        lineDetail = Utils.StringPad(documentType, 2, "", Utils.PadType.STR_PAD_RIGHT)
                        lineDetail += Utils.StringPad(String.Concat(supplier.ThirdParty.Nit, supplier.ThirdParty.DigitVerification), 16, 0, Utils.PadType.STR_PAD_LEFT)
                    End If
                Else
                    lineDetail = Utils.StringPad(documentType, 2, "", Utils.PadType.STR_PAD_RIGHT)
                    lineDetail += Utils.StringPad(String.Concat(supplier.ThirdParty.Nit, 0), 16, 0, Utils.PadType.STR_PAD_LEFT)
                End If
                'forma de pago
                lineDetail += Utils.StringPad(1, 1, 0, Utils.PadType.STR_PAD_LEFT)

                Dim bank As Bank = Nothing
                bank = _bankRepository.GetBankById(account.BankId)
                If bank.CenitCode Is String.Empty Then
                    listErrorsDetails.Add("El banco " + bank.Code + " - " + bank.Name + " no tiene código CENIT")
                    Continue For
                End If
                'banco cuenta del beneficiario
                Dim safeCodeCenit As String = Utils.StringPad(bank.CenitCode, 4, 0, Utils.PadType.STR_PAD_LEFT)
                lineDetail += safeCodeCenit

                Dim accountNumber As String = String.Empty
                Dim accountType As String = String.Empty
                Dim supplierAccountNumber As String = String.Empty

                If safeCodeCenit = "0013" Then
                    accountType = Utils.StringPad(If(account.Type = 1, "02", "01"), 4, 0, Utils.PadType.STR_PAD_RIGHT)
                    Dim office = "0" + Left(account.Number.Trim.Replace("-", ""), 3)
                    accountNumber = office + "00" + accountType + Right(account.Number.Trim.Replace("-", ""), 6)
                    accountType = "00"
                    supplierAccountNumber = "00000000000000000"
                Else
                    accountNumber = "0000000000000000"
                    accountType = Utils.StringPad(If(account.Type = 1, "02", "01"), 2, 0, Utils.PadType.STR_PAD_RIGHT)
                    supplierAccountNumber = account.Number.Trim.Replace("-", "")
                End If
                'numero cuenta BBVA ==============================================================porque va en 0
                lineDetail += Utils.StringPad(accountNumber, 16, 0, Utils.PadType.STR_PAD_LEFT)
                'tipo de cuenta 02 = ahorros , 01 = corriente
                lineDetail += Utils.StringPad(accountType, 2, "", Utils.PadType.STR_PAD_LEFT)
                'numero cuenta beneficiario
                lineDetail += Utils.StringPad(supplierAccountNumber.Trim.Replace("-", ""), 17, " ", Utils.PadType.STR_PAD_LEFT)
                'valor de la transaccion parte entera
                Dim Part As String = CDbl(SchedulePayment.SchedulePaymentDetail.Where(Function(x) x.SupplierId = listDetailSupplier(i)).ToList().Sum(Function(y) y.AmountPaid))
                Dim tempInt() As String
                tempInt = Split(Part, ",")
                lineDetail += Utils.StringPad(tempInt(0).ToString, 13, 0, Utils.PadType.STR_PAD_LEFT)
                'valor de la transaccion parte decimal
                lineDetail += Utils.StringPad(If(tempInt.Length >= 2, StrConv(tempInt(1).ToString, 0, 2), 0), 2, 0, Utils.PadType.STR_PAD_RIGHT)
                'fecha - 00000000 se hizo este cambio porque la documentacion enviada lo decia asi
                lineDetail += Utils.StringPad("", 8, 0, Utils.PadType.STR_PAD_LEFT)
                'codigo oficna pagadora
                lineDetail += Utils.StringPad("9999", 4, 0, Utils.PadType.STR_PAD_RIGHT)
                'nombre del beneficiario
                lineDetail += Utils.StringPad(supplier.ThirdParty.Name.ChangeCharacters, 36, " ", Utils.PadType.STR_PAD_LEFT)
                'direccion
                lineDetail += Utils.StringPad("NEIVA", 36, " ", Utils.PadType.STR_PAD_LEFT)
                'direccion 2
                lineDetail += Utils.StringPad("", 36, " ", Utils.PadType.STR_PAD_LEFT)
                'email
                lineDetail += Utils.StringPad("", 48, " ", Utils.PadType.STR_PAD_LEFT)
                'concepto
                lineDetail += Utils.StringPad("PAGO A PROVEEDORES", 40, " ", Utils.PadType.STR_PAD_LEFT)
                result.AppendLine(lineDetail)
            Next
            If listErrorsDetails.Count > 0 Then
                Return New ActionResult(Of String) With {.StateResult = False, .MessageResult = listErrorsDetails}
            End If
            Return New ActionResult(Of String) With {.StateResult = True, .Message = "DF_" + DateTime.Now.ToString("ddMMyyyy") + "_Banco_BBVA", .ObjectEmbbeded = result.ToString()}
        Catch ex As Exception
            Return New ActionResult(Of String) With {.StateResult = False, .Message = ex.Message}
        End Try
    End Function

    ''' <summary>
    ''' metodo para generar el archivo plano del banco davivienda
    ''' </summary>
    ''' <param name="SchedulePayment"></param>
    ''' <param name="companyNIT"></param>
    ''' <param name="companyName"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function GenerateDaviviendaFile(SchedulePayment As SchedulePayment, companyNIT As String, companyName As String, optionalParameters As List(Of String)) As ActionResult(Of String)
        Try
            Dim verificationDigit = String.Empty
            If optionalParameters IsNot Nothing AndAlso optionalParameters.Any Then
                verificationDigit = optionalParameters(0)
            End If
            Dim result As New StringBuilder
            Dim entityAccount = _entityBankAccountRepository.GetEntityBankAccountById(SchedulePayment.EntityBankAccountId)
            'creo la cabecera del archivo
            'tipo de registro por defecto es RC por defecto
            Dim lineHead As String = Utils.StringPad("RC", 2, 0, Utils.PadType.STR_PAD_LEFT)
            'nit de la empresa
            lineHead += Utils.StringPad(String.Concat(companyNIT, verificationDigit), 16, 0, Utils.PadType.STR_PAD_LEFT)
            'codigo del servicio
            lineHead += Utils.StringPad("PROV", 4, 0, Utils.PadType.STR_PAD_LEFT)
            'codigo del subservicio
            lineHead += Utils.StringPad("PROV", 4, 0, Utils.PadType.STR_PAD_LEFT)
            'cuenta cliente
            lineHead += Utils.StringPad(entityAccount.Number.Trim.Replace("-", ""), 16, 0, Utils.PadType.STR_PAD_LEFT)
            'tipo de cuenta CA = ahorros , CC = corriente
            lineHead += Utils.StringPad(If(entityAccount.Type = 1, "CA", "CC"), 2, 0, Utils.PadType.STR_PAD_LEFT)
            'codigo del banco
            Dim bank As Bank = Nothing
            bank = _bankRepository.GetBankById(entityAccount.IdBank)
            lineHead += Utils.StringPad(bank.CenitCode, 6, 0, Utils.PadType.STR_PAD_LEFT)
            'valor total
            lineHead += Utils.StringPad((CDbl(SchedulePayment.SchedulePaymentDetail.Sum(Function(y) y.AmountPaid) * 100)), 18, 0, Utils.PadType.STR_PAD_LEFT)
            'numero de registros
            Dim totalItems = (From e In SchedulePayment.SchedulePaymentDetail Select e.SupplierId).Distinct().ToList().Count
            lineHead += Utils.StringPad(totalItems, 6, 0, Utils.PadType.STR_PAD_LEFT)
            'Fecha transmisión 
            lineHead += Utils.StringPad(DateTime.Now.ToString("yyyyMMdd"), 8, 0, Utils.PadType.STR_PAD_LEFT)
            'hora de la transaccion
            lineHead += Utils.StringPad(DateTime.Now.ToString("hhmmss"), 6, 0, Utils.PadType.STR_PAD_LEFT)
            'codigo del operador
            lineHead += Utils.StringPad("", 4, 0, Utils.PadType.STR_PAD_LEFT)
            'codigo no procesado
            lineHead += Utils.StringPad(9999, 4, 0, Utils.PadType.STR_PAD_LEFT)
            'fecha generacion
            lineHead += Utils.StringPad("", 8, 0, Utils.PadType.STR_PAD_LEFT)
            'hora del proceso
            lineHead += Utils.StringPad("", 6, 0, Utils.PadType.STR_PAD_LEFT)
            'indicador de inscripcion
            lineHead += Utils.StringPad("", 2, 0, Utils.PadType.STR_PAD_LEFT)
            'tipo de identificacion
            lineHead += Utils.StringPad("01", 2, 0, Utils.PadType.STR_PAD_LEFT)
            'numero cliente asignado davivienda
            lineHead += Utils.StringPad("", 12, 0, Utils.PadType.STR_PAD_LEFT)
            'oficina de recaudo
            lineHead += Utils.StringPad("", 4, 0, Utils.PadType.STR_PAD_LEFT)
            'campo futuro
            lineHead += Utils.StringPad("", 40, 0, Utils.PadType.STR_PAD_LEFT)
            result.AppendLine(lineHead)
            Dim listErrorsDetails As New List(Of String)
            Dim listDetailSupplier = (From e In SchedulePayment.SchedulePaymentDetail Select e.SupplierId).Distinct().ToList()

            For i As Integer = 0 To listDetailSupplier.Count - 1 Step 1
                Dim supplier = _supplierRepository.GetSupplierByIdWithSupplierBankAccount(listDetailSupplier(i))
                Dim resultValidateSupplierAccount As ActionResult(Of SupplierBankAccount)

                If supplier Is Nothing Then
                    Throw New ArgumentNullException("supplier")
                End If

                Dim schedulePaymentBankAccount = SchedulePayment?.SchedulePaymentBankAccount?.ToList().Find(Function(x) x.SupplierId = supplier?.Id)

                'se busca si se guardaron datos en la nueva tabla de informacion bancaria para programacion de pago, de lo contrario sigue como se hacia.
                If schedulePaymentBankAccount IsNot Nothing Then
                    Dim queryBank = supplier.SupplierBankAccount.ToList().Find(Function(x) x.Id = schedulePaymentBankAccount?.SupplierBankAccountId)
                    resultValidateSupplierAccount = New ActionResult(Of SupplierBankAccount) With {.StateResult = queryBank IsNot Nothing, .ObjectEmbbeded = queryBank}
                Else
                    '    Valido las cuentas del proveedor
                    resultValidateSupplierAccount = ValidateSupplierAccount(supplier, entityAccount.IdBank)
                End If

                If resultValidateSupplierAccount.StateResult = False Then
                    listErrorsDetails.Add(resultValidateSupplierAccount.Message)
                    Continue For
                End If

                Dim account As SupplierBankAccount = resultValidateSupplierAccount.ObjectEmbbeded
                'tipo de registro por defecto esta en TR
                Dim lineDetail As String = Utils.StringPad("TR", 2, 0, Utils.PadType.STR_PAD_LEFT)
                'nit del beneficiario y digito de verificacion cuando es NIT Empresa
                lineDetail += Utils.StringPad(If(supplier.ThirdParty.PersonType = 1, supplier.ThirdParty.Nit, String.Concat(supplier.ThirdParty.Nit, supplier.ThirdParty.DigitVerification)), 16, 0, Utils.PadType.STR_PAD_LEFT)
                'referencia
                lineDetail += Utils.StringPad("", 16, 0, Utils.PadType.STR_PAD_LEFT)
                'numero cuenta beneficiario
                lineDetail += Utils.StringPad(account.Number.Trim.Replace("-", ""), 16, 0, Utils.PadType.STR_PAD_LEFT)
                'tipo de cuenta CA = ahorros , CC = corriente
                lineDetail += Utils.StringPad(If(account.Type = 1, "CA", "CC"), 2, 0, Utils.PadType.STR_PAD_LEFT)

                bank = _bankRepository.GetBankById(account.BankId)
                'banco cuenta del beneficiario
                lineDetail += Utils.StringPad(bank.CenitCode, 6, 0, Utils.PadType.STR_PAD_LEFT)
                'valor de la transaccion
                lineDetail += Utils.StringPad((CDbl(SchedulePayment.SchedulePaymentDetail.Where(Function(x) x.SupplierId = listDetailSupplier(i)).ToList().Sum(Function(y) y.AmountPaid) * 100)), 18, 0, Utils.PadType.STR_PAD_LEFT)
                'talon
                lineDetail += Utils.StringPad("", 6, 0, Utils.PadType.STR_PAD_LEFT)
                'tipo de documento
                Dim documentType As String = "01"
                Select Case supplier.ThirdParty.Person.IdentificationType
                    Case 0 'cedula 
                        documentType = "02"
                    Case 1 ' cedula de extranjeria
                        documentType = "04"
                    Case 2 'tarjeta de identidad
                        documentType = "03"
                    Case 4 'pasaporte
                        documentType = "05"
                    Case 7 'nit
                        documentType = "01"
                End Select
                lineDetail += Utils.StringPad(documentType, 2, "", Utils.PadType.STR_PAD_RIGHT)
                'validadar traslados
                lineDetail += Utils.StringPad(1, 1, 0, Utils.PadType.STR_PAD_LEFT)
                'Resultado del proceso que asume los valores (Enviar campo con 9999)
                lineDetail += Utils.StringPad(9999, 4, 0, Utils.PadType.STR_PAD_LEFT)
                'mensaje de respuesta
                lineDetail += Utils.StringPad("", 40, 0, Utils.PadType.STR_PAD_LEFT)
                'valor acumulado del cobro
                lineDetail += Utils.StringPad("", 18, 0, Utils.PadType.STR_PAD_LEFT)
                'fecha de aplicacion
                lineDetail += Utils.StringPad("", 8, 0, Utils.PadType.STR_PAD_LEFT)
                'oficina de recaudo
                lineDetail += Utils.StringPad("", 4, 0, Utils.PadType.STR_PAD_LEFT)
                'motivo
                lineDetail += Utils.StringPad("", 4, 0, Utils.PadType.STR_PAD_LEFT)
                '
                lineDetail += Utils.StringPad("", 7, 0, Utils.PadType.STR_PAD_LEFT)

                result.AppendLine(lineDetail)

            Next
            If listErrorsDetails.Count > 0 Then
                Return New ActionResult(Of String) With {.StateResult = False, .MessageResult = listErrorsDetails}
            End If
            Return New ActionResult(Of String) With {.StateResult = True, .Message = "DF_" + DateTime.Now.ToString("ddMMyyyy") + "_Banco_Davivienda", .ObjectEmbbeded = result.ToString()}
        Catch ex As Exception
            Return New ActionResult(Of String) With {.StateResult = False, .Message = ex.Message}
        End Try
    End Function

    ''' <summary>
    ''' metodo para generar plano para el banco caja social
    ''' </summary>
    ''' <param name="SchedulePayment"></param>
    ''' <param name="companyNIT"></param>
    ''' <param name="companyName"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function GenerateCajaSocialFile(SchedulePayment As SchedulePayment, companyNIT As String, companyName As String) As ActionResult(Of String)
        Try
            Dim result As New StringBuilder
            Dim listErrorsDetails As New List(Of String)
            Dim listDetailSupplier = (From e In SchedulePayment.SchedulePaymentDetail Select e.SupplierId).Distinct().ToList()

            Dim entityAccount = _entityBankAccountRepository.GetEntityBankAccountById(SchedulePayment.EntityBankAccountId)

            For i As Integer = 0 To listDetailSupplier.Count - 1 Step 1
                Dim supplier = _supplierRepository.GetSupplierByIdWithSupplierBankAccount(listDetailSupplier(i))
                Dim resultValidateSupplierAccount As ActionResult(Of SupplierBankAccount)

                If supplier Is Nothing Then
                    Throw New ArgumentNullException("supplier")
                End If

                Dim schedulePaymentBankAccount = SchedulePayment?.SchedulePaymentBankAccount?.ToList().Find(Function(x) x.SupplierId = supplier?.Id)

                'se busca si se guardaron datos en la nueva tabla de informacion bancaria para programacion de pago, de lo contrario sigue como se hacia.
                If schedulePaymentBankAccount IsNot Nothing Then
                    Dim queryBank = supplier.SupplierBankAccount.ToList().Find(Function(x) x.Id = schedulePaymentBankAccount?.SupplierBankAccountId)
                    resultValidateSupplierAccount = New ActionResult(Of SupplierBankAccount) With {.StateResult = queryBank IsNot Nothing, .ObjectEmbbeded = queryBank}
                Else
                    ' Valido las cuentas del proveedor
                    resultValidateSupplierAccount = ValidateSupplierAccount(supplier, entityAccount.IdBank)
                End If

                If resultValidateSupplierAccount.StateResult = False Then
                    listErrorsDetails.Add(resultValidateSupplierAccount.Message)
                    Continue For
                End If

                Dim account As SupplierBankAccount = resultValidateSupplierAccount.ObjectEmbbeded
                'tipo de registro por defecto 1
                Dim lineDetail As String = Utils.StringPad(6, 1, "", Utils.PadType.STR_PAD_RIGHT)
                'tipo de cuenta 32 = ahorros , 22 = corriente
                lineDetail += Utils.StringPad(If(account.Type = 1, 32, 22), 2, "", Utils.PadType.STR_PAD_LEFT)
                'valor de la transaccion
                lineDetail += Utils.StringPad(CDbl((SchedulePayment.SchedulePaymentDetail.Where(Function(x) x.SupplierId = listDetailSupplier(i)).ToList().Sum(Function(y) y.AmountPaid) * 100)), 12, 0, Utils.PadType.STR_PAD_LEFT)
                'numero cuenta beneficiario
                lineDetail += Utils.StringPad(account.Number.Trim.Replace("-", ""), 17, " ", Utils.PadType.STR_PAD_RIGHT)
                'banco cuenta del beneficiario
                Dim bank = _bankRepository.GetBankById(account.BankId)
                If bank.CenitCode Is String.Empty Then
                    listErrorsDetails.Add("El banco " + bank.Code + " - " + bank.Name + " no tiene código CENIT")
                    Continue For
                End If

                If bank.CenitVerification Is String.Empty Then
                    listErrorsDetails.Add("El banco " + bank.Code + " - " + bank.Name + " no tiene digito de verificación")
                    Continue For
                End If

                lineDetail += Utils.StringPad(bank.CenitCode + bank.CenitVerification, 9, 0, Utils.PadType.STR_PAD_LEFT)
                'nit del beneficiario
                lineDetail += Utils.StringPad(supplier.ThirdParty.Nit, 15, " ", Utils.PadType.STR_PAD_RIGHT)
                'nombre del beneficiario
                lineDetail += Utils.StringPad(supplier.ThirdParty.Name.ToUpper().ChangeCharacters, 22, " ", Utils.PadType.STR_PAD_RIGHT)
                'valida la identificacion enviada en el archivo con la identificacion registrada en la institucion finanaciera
                lineDetail += Utils.StringPad("V", 2, " ", Utils.PadType.STR_PAD_RIGHT)
                'espacios en blanco
                lineDetail += Utils.StringPad("", 13, " ", Utils.PadType.STR_PAD_LEFT)
                'descripcion
                lineDetail += Utils.StringPad("Proveedor", 10, " ", Utils.PadType.STR_PAD_RIGHT)
                'espacios en blanco
                lineDetail += Utils.StringPad("", 30, " ", Utils.PadType.STR_PAD_RIGHT)
                'espacios en blanco
                lineDetail += Utils.StringPad("", 27, " ", Utils.PadType.STR_PAD_LEFT)

                result.AppendLine(lineDetail)
            Next

            If listErrorsDetails.Count > 0 Then
                Return New ActionResult(Of String) With {.StateResult = False, .MessageResult = listErrorsDetails}
            End If
            Return New ActionResult(Of String) With {.StateResult = True, .Message = "DF_" + DateTime.Now.ToString("ddMMyyyy") + "_Banco_Caja_Social", .ObjectEmbbeded = result.ToString()}
        Catch ex As Exception
            Return New ActionResult(Of String) With {.StateResult = False, .Message = ex.Message}
        End Try
    End Function

    ''' <summary>
    ''' metodo para generar el archivo plano del banco de Bogotá
    ''' </summary>
    ''' <param name="SchedulePayment"></param>
    ''' <param name="companyNIT"></param>
    ''' <param name="companyName"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function GenerateBancoBogotaFile(SchedulePayment As SchedulePayment, companyNIT As String, companyName As String) As ActionResult(Of String)
        Try
            Dim result As New StringBuilder
            Dim DateTransaction = Date.Now
            Dim DateNow As String = DateTransaction.ToString("yyyyMMdd")
            Dim BankTypeAccount As Char
            Dim entityAccount = _entityBankAccountRepository.GetEntityBankAccountById(SchedulePayment.EntityBankAccountId)
            Dim companyThirdParty = _thirdPartyRepository.GetThirdPartyByNit(companyNIT, False)

            If entityAccount.Type = 1 Then '1. Ahorros
                BankTypeAccount = "2"
            Else ' 2. Corriente
                BankTypeAccount = "1"
            End If

            'creo la cabecera del archivo
            'tipo de registro por defecto es RC por defecto
            Dim lineHead As String = Utils.StringPad(1, 1, 0, Utils.PadType.STR_PAD_LEFT)
            lineHead &= Utils.StringPad(DateNow, 8, 0, Utils.PadType.STR_PAD_LEFT)
            lineHead &= Utils.StringPad(0, 23, 0, Utils.PadType.STR_PAD_LEFT)
            lineHead &= Utils.StringPad(BankTypeAccount, 2, 0, Utils.PadType.STR_PAD_LEFT)
            lineHead &= Utils.StringPad(0, 6, 0, Utils.PadType.STR_PAD_LEFT)
            lineHead &= Utils.StringPad(entityAccount.Number, 11, 0, Utils.PadType.STR_PAD_LEFT)
            lineHead &= Utils.StringPad(companyName, 40, " ", Utils.PadType.STR_PAD_RIGHT)
            lineHead &= Utils.StringPad(String.Format("{0}{1}", companyNIT, companyThirdParty.DigitVerification), 11, 0, Utils.PadType.STR_PAD_LEFT)
            lineHead &= Utils.StringPad("002", 3, 0, Utils.PadType.STR_PAD_LEFT)
            lineHead &= Utils.StringPad("0001", 4, 0, Utils.PadType.STR_PAD_LEFT)
            lineHead &= Utils.StringPad(DateNow, 8, 0, Utils.PadType.STR_PAD_LEFT)
            lineHead &= Utils.StringPad(entityAccount.Number, 3, 0, Utils.PadType.STR_PAD_LEFT)
            lineHead &= Utils.StringPad("N", 1, 0, Utils.PadType.STR_PAD_LEFT)
            lineHead &= Utils.StringPad(" ", 48, " ", Utils.PadType.STR_PAD_LEFT)
            lineHead &= Utils.StringPad(" ", 1, " ", Utils.PadType.STR_PAD_LEFT)
            lineHead &= Utils.StringPad(" ", 80, " ", Utils.PadType.STR_PAD_LEFT)

            result.AppendLine(lineHead)
            Dim listErrorsDetails As New List(Of String)
            Dim listDetailSupplier = (From e In SchedulePayment.SchedulePaymentDetail Select e.SupplierId).Distinct().ToList()

            For i As Integer = 0 To listDetailSupplier.Count - 1 Step 1
                Dim supplier = _supplierRepository.GetSupplierByIdWithSupplierBankAccount(listDetailSupplier(i))

                Dim resultValidateSupplierAccount As ActionResult(Of SupplierBankAccount)

                If supplier Is Nothing Then
                    Throw New ArgumentNullException("supplier")
                End If

                Dim schedulePaymentBankAccount = SchedulePayment?.SchedulePaymentBankAccount?.ToList().Find(Function(x) x.SupplierId = supplier?.Id)

                'se busca si se guardaron datos en la nueva tabla de informacion bancaria para programacion de pago, de lo contrario sigue como se hacia.
                If schedulePaymentBankAccount IsNot Nothing Then
                    Dim queryBank = supplier.SupplierBankAccount.ToList().Find(Function(x) x.Id = schedulePaymentBankAccount?.SupplierBankAccountId)
                    resultValidateSupplierAccount = New ActionResult(Of SupplierBankAccount) With {.StateResult = queryBank IsNot Nothing, .ObjectEmbbeded = queryBank}
                Else
                    '    Valido las cuentas del proveedor
                    resultValidateSupplierAccount = ValidateSupplierAccount(supplier, entityAccount.IdBank)
                End If

                If resultValidateSupplierAccount.StateResult = False Then
                    listErrorsDetails.Add(resultValidateSupplierAccount.Message)
                    Continue For
                End If

                Dim lineDetail As String = Utils.StringPad("2", 1, " ", Utils.PadType.STR_PAD_LEFT)

                'tipo de documento
                Dim documentType As String = "C"

                Select Case supplier.ThirdParty.Person.IdentificationType
                    Case 0 'cedula 
                        documentType = "C"
                    Case 1 ' cedula de extranjeria
                        documentType = "E"
                    Case 2 'tarjeta de identidad
                        documentType = "T"
                    Case 7 'nit
                        documentType = "N"
                End Select

                Dim account As SupplierBankAccount = resultValidateSupplierAccount.ObjectEmbbeded
                Dim bank = _bankRepository.GetBankById(account.BankId)

                Dim SupplierBankTypeAccount As Char

                If account.Type = 1 Then '1. Ahorros
                    SupplierBankTypeAccount = "2"
                Else ' 2. Corriente
                    SupplierBankTypeAccount = "1"
                End If


                lineDetail &= Utils.StringPad(documentType, 1, " ", Utils.PadType.STR_PAD_LEFT)
                If supplier.ThirdParty.PersonType = 1 Then
                    lineDetail &= Utils.StringPad(supplier.ThirdParty.Nit, 11, "0", Utils.PadType.STR_PAD_LEFT)
                Else
                    lineDetail &= Utils.StringPad(String.Format("{0}{1}", supplier.ThirdParty.Nit, supplier.ThirdParty.DigitVerification), 11, "0", Utils.PadType.STR_PAD_LEFT)
                End If
                lineDetail &= Utils.StringPad(Trim(supplier.ThirdParty.Name.ChangeCharacters), 40, " ", Utils.PadType.STR_PAD_LEFT)
                'lineDetail &= Utils.StringPad(0, 1, 0, Utils.PadType.STR_PAD_LEFT)
                lineDetail &= Utils.StringPad(LTrim(SupplierBankTypeAccount), 2, 0, Utils.PadType.STR_PAD_LEFT)
                lineDetail &= Utils.StringPad(account.Number.Trim.Replace("-", ""), 17, " ", Utils.PadType.STR_PAD_RIGHT)
                lineDetail &= Utils.StringPad(CDbl((SchedulePayment.SchedulePaymentDetail.Where(Function(x) x.SupplierId = listDetailSupplier(i)).ToList().Sum(Function(y) y.AmountPaid) * 100)), 18, "0", Utils.PadType.STR_PAD_LEFT)
                lineDetail &= Utils.StringPad("A", 1, " ", Utils.PadType.STR_PAD_LEFT)
                lineDetail &= Utils.StringPad("000", 3, " ", Utils.PadType.STR_PAD_LEFT)
                'Toma los últimos 3 dígitos del código Cenit
                If bank.CenitCode IsNot Nothing AndAlso bank.CenitCode.Length >= 3 Then
                    lineDetail &= bank.CenitCode.Substring(bank.CenitCode.Length - 3)
                End If
                lineDetail &= Utils.StringPad("0001", 4, " ", Utils.PadType.STR_PAD_LEFT)
                'lineDetail &= Utils.StringPad(" ", 9, " ", Utils.PadType.STR_PAD_LEFT)
                'lineDetail &= Utils.StringPad(" ", 1, " ", Utils.PadType.STR_PAD_LEFT)
                lineDetail &= Utils.StringPad("PAGO DE PROVEEDORES", 80, " ", Utils.PadType.STR_PAD_LEFT)
                lineDetail &= Utils.StringPad("0", 1, " ", Utils.PadType.STR_PAD_LEFT)
                lineDetail &= Utils.StringPad("0", 10, "0", Utils.PadType.STR_PAD_LEFT)
                lineDetail &= Utils.StringPad("N", 1, " ", Utils.PadType.STR_PAD_LEFT)
                'lineDetail &= Utils.StringPad(" ", 8, " ", Utils.PadType.STR_PAD_LEFT)
                'lineDetail &= Utils.StringPad(" ", 16, " ", Utils.PadType.STR_PAD_LEFT)
                lineDetail &= Utils.StringPad(" ", 48, " ", Utils.PadType.STR_PAD_LEFT)
                lineDetail &= Utils.StringPad("N", 1, " ", Utils.PadType.STR_PAD_LEFT)
                lineDetail &= Utils.StringPad(" ", 8, " ", Utils.PadType.STR_PAD_LEFT)

                result.AppendLine(lineDetail)
            Next

            If listErrorsDetails.Count > 0 Then
                Return New ActionResult(Of String) With {.StateResult = False, .MessageResult = listErrorsDetails}
            End If
            Return New ActionResult(Of String) With {.StateResult = True, .Message = "DF_" + DateTime.Now.ToString("ddMMyyyy") + "_Banco_De_Bogota", .ObjectEmbbeded = result.ToString()}

        Catch ex As Exception
            Return New ActionResult(Of String) With {.StateResult = False, .Message = ex.Message}
        End Try
    End Function

    ''' <summary>
    ''' metodo para generar archivo plano para bancolombia pagos SAP
    ''' </summary>
    ''' <param name="SchedulePayment"></param>
    ''' <param name="companyNIT"></param>
    ''' <param name="companyName"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function GenerateBancolombiaFileSap(SchedulePayment As SchedulePayment, companyNIT As String, companyName As String) As ActionResult(Of String)
        Try
            Dim result As New StringBuilder()
            Dim DateTransaction = Date.Now
            Dim DateNow As String = DateTransaction.ToString("yyMMdd")


            Dim listErrorsDetails As New List(Of String)


            If SchedulePayment IsNot Nothing Then

                Dim entityAccount = _entityBankAccountRepository.GetEntityBankAccountById(SchedulePayment.EntityBankAccountId)
                Dim listDetailSupplier = (From e In SchedulePayment.SchedulePaymentDetail Select e.SupplierId).Distinct().ToList()
                Dim totalItems = (From e In SchedulePayment.SchedulePaymentDetail Select e.SupplierId).Distinct().ToList().Count

                Dim lineHead As String = Utils.StringPad(1, 1, 0, Utils.PadType.STR_PAD_LEFT)
                lineHead &= Utils.StringPad(companyNIT, 10, 0, Utils.PadType.STR_PAD_LEFT)
                lineHead &= Utils.StringPad(companyName, 16, " ", Utils.PadType.STR_PAD_LEFT)
                lineHead &= Utils.StringPad(220, 3, 0, Utils.PadType.STR_PAD_LEFT)
                lineHead &= Utils.StringPad("PAGO PROVEEDORES", 10, 0, Utils.PadType.STR_PAD_LEFT)
                lineHead &= Utils.StringPad(DateNow, 6, 0, Utils.PadType.STR_PAD_LEFT)
                lineHead &= Utils.StringPad("A", 1, " ", Utils.PadType.STR_PAD_LEFT)
                lineHead &= Utils.StringPad(DateNow, 6, 0, Utils.PadType.STR_PAD_LEFT)
                lineHead &= Utils.StringPad(totalItems, 6, 0, Utils.PadType.STR_PAD_LEFT)
                lineHead &= Utils.StringPad(0, 12, 0, Utils.PadType.STR_PAD_LEFT)
                lineHead &= Utils.StringPad(CDbl(SchedulePayment.SchedulePaymentDetail.Sum(Function(y) y.AmountPaid)), 12, 0, Utils.PadType.STR_PAD_LEFT)
                lineHead &= Utils.StringPad(entityAccount.Number.Trim.Replace("-", ""), 11, 0, Utils.PadType.STR_PAD_LEFT)
                lineHead &= Utils.StringPad(If(entityAccount.Type = 1, "S", "D"), 1, "", Utils.PadType.STR_PAD_LEFT)
                result.Append(lineHead)

                For i As Integer = 0 To listDetailSupplier.Count - 1 Step 1

                    'nit del beneficiario
                    Dim supplier = _supplierRepository.GetSupplierByIdWithSupplierBankAccount(listDetailSupplier(i))
                    Dim resultValidateSupplierAccount As ActionResult(Of SupplierBankAccount)

                    If supplier Is Nothing Then
                        Throw New ArgumentNullException("supplier")
                    End If

                    Dim schedulePaymentBankAccount = SchedulePayment?.SchedulePaymentBankAccount?.ToList().Find(Function(x) x.SupplierId = supplier?.Id)

                    'se busca si se guardaron datos en la nueva tabla de informacion bancaria para programacion de pago, de lo contrario sigue como se hacia.
                    If schedulePaymentBankAccount IsNot Nothing Then
                        Dim queryBank = supplier.SupplierBankAccount.ToList().Find(Function(x) x.Id = schedulePaymentBankAccount?.SupplierBankAccountId)
                        resultValidateSupplierAccount = New ActionResult(Of SupplierBankAccount) With {.StateResult = queryBank IsNot Nothing, .ObjectEmbbeded = queryBank}
                    Else
                        '    Valido las cuentas del proveedor
                        resultValidateSupplierAccount = ValidateSupplierAccount(supplier, entityAccount.IdBank)
                    End If

                    If resultValidateSupplierAccount.StateResult = False Then
                        listErrorsDetails.Add(resultValidateSupplierAccount.Message)
                        Continue For
                    End If

                    Dim account As SupplierBankAccount = resultValidateSupplierAccount.ObjectEmbbeded

                    Dim bank As Bank = _bankRepository.GetBankById(account.BankId)
                    If String.IsNullOrEmpty(bank.AchCode) Then
                        listErrorsDetails.Add("El banco " + bank.Code + " - " + bank.Name + " no tiene código ACH")
                        Continue For
                    End If
                    Dim AchCode As Integer
                    If Not Integer.TryParse(bank.AchCode, AchCode) Then
                        listErrorsDetails.Add("El banco " + bank.Code + " - " + bank.Name + " no tiene código ACH numérico")
                        Continue For
                    End If

                    Dim lineDet As String = vbCrLf

                    lineDet &= Utils.StringPad(6, 1, " ", Utils.PadType.STR_PAD_LEFT)
                    lineDet &= Utils.StringPad(supplier.ThirdParty.Nit, 15, 0, Utils.PadType.STR_PAD_LEFT)
                    lineDet &= Utils.StringPad(supplier.ThirdParty.Name.ToUpper().ChangeCharacters, 18, " ", Utils.PadType.STR_PAD_RIGHT)
                    lineDet &= Utils.StringPad(bank.AchCode, 9, 0, Utils.PadType.STR_PAD_LEFT)
                    lineDet &= Utils.StringPad(account.Number.Trim().Replace("-", ""), 17, 0, Utils.PadType.STR_PAD_LEFT)
                    lineDet &= Utils.StringPad("S", 1, " ", Utils.PadType.STR_PAD_LEFT)
                    lineDet &= Utils.StringPad(If(account.Type = 1, 37, 27), 2, " ", Utils.PadType.STR_PAD_LEFT)
                    lineDet &= Utils.StringPad(CDbl(SchedulePayment.SchedulePaymentDetail.Where(Function(x) x.SupplierId = listDetailSupplier(i)).ToList().Sum(Function(y) y.AmountPaid)), 10, 0, Utils.PadType.STR_PAD_LEFT)
                    lineDet &= Utils.StringPad(0, 9, 0, Utils.PadType.STR_PAD_LEFT)
                    lineDet &= Utils.StringPad("PROV" & supplier.ThirdParty.Name.ToUpper().ChangeCharacters, 12, " ", Utils.PadType.STR_PAD_RIGHT)
                    lineDet &= Utils.StringPad(" ", 1, " ", Utils.PadType.STR_PAD_LEFT)
                    result.Append(lineDet)


                Next

            End If

            If listErrorsDetails.Count > 0 Then
                Return New ActionResult(Of String) With {.StateResult = False, .MessageResult = listErrorsDetails}
            End If
            Return New ActionResult(Of String) With {.StateResult = True, .Message = "DF_" + DateTime.Now.ToString("ddMMyyyy") + "_Bancolombia-SAP", .ObjectEmbbeded = result.ToString()}
        Catch ex As Exception
            Return New ActionResult(Of String) With {.StateResult = False, .Message = ex.Message}
        End Try
    End Function

    ''' <summary>
    ''' metodo para generar archivo plano para el banco itau
    ''' </summary>
    ''' <param name="SchedulePayment"></param>
    ''' <param name="companyNIT"></param>
    ''' <param name="companyName"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function GenerateItauFile(SchedulePayment As SchedulePayment, companyNIT As String, companyName As String) As ActionResult(Of String)
        Try
            Dim DateTransaction = Date.Now
            Dim DateNow As String = DateTransaction.ToString("MMddyy")

            Dim result As New StringBuilder()
            Dim listErrorsDetails As New List(Of String)

            If SchedulePayment IsNot Nothing Then
                Dim CompanyThirdParty = _thirdPartyRepository.GetThirdPartyByNit(companyNIT)
                If CompanyThirdParty Is Nothing OrElse CompanyThirdParty.Id = 0 Then
                    listErrorsDetails.Add("La empresa " + companyNIT + " no esta creada como tercero")
                    Return New ActionResult(Of String) With {.StateResult = False, .MessageResult = listErrorsDetails}
                End If

                Dim entityAccount = _entityBankAccountRepository.GetEntityBankAccountById(SchedulePayment.EntityBankAccountId)
                Dim listDetailSupplier = (From e In SchedulePayment.SchedulePaymentDetail Select e.SupplierId).Distinct().ToList()

                For i As Integer = 0 To listDetailSupplier.Count - 1 Step 1
                    'nit del beneficiario
                    Dim supplier = _supplierRepository.GetSupplierByIdWithSupplierBankAccount(listDetailSupplier(i))
                    Dim resultValidateSupplierAccount As ActionResult(Of SupplierBankAccount)

                    If supplier Is Nothing Then
                        Throw New ArgumentNullException("supplier")
                    End If

                    Dim schedulePaymentBankAccount = SchedulePayment?.SchedulePaymentBankAccount?.ToList().Find(Function(x) x.SupplierId = supplier?.Id)

                    'se busca si se guardaron datos en la nueva tabla de informacion bancaria para programacion de pago, de lo contrario sigue como se hacia.
                    If schedulePaymentBankAccount IsNot Nothing Then
                        Dim queryBank = supplier.SupplierBankAccount.ToList().Find(Function(x) x.Id = schedulePaymentBankAccount?.SupplierBankAccountId)
                        resultValidateSupplierAccount = New ActionResult(Of SupplierBankAccount) With {.StateResult = queryBank IsNot Nothing, .ObjectEmbbeded = queryBank}
                    Else
                        '    Valido las cuentas del proveedor
                        resultValidateSupplierAccount = ValidateSupplierAccount(supplier, entityAccount.IdBank)
                    End If

                    If resultValidateSupplierAccount.StateResult = False Then
                        listErrorsDetails.Add(resultValidateSupplierAccount.Message)
                        Continue For
                    End If

                    Dim account As SupplierBankAccount = resultValidateSupplierAccount.ObjectEmbbeded
                    Dim bank As Bank = _bankRepository.GetBankById(account.BankId)
                    If String.IsNullOrEmpty(bank.CenitCode) Then
                        listErrorsDetails.Add("El banco " + bank.Code + " - " + bank.Name + " no tiene código CENIT")
                        Continue For
                    End If
                    Dim cenitCode As Integer
                    If Not Integer.TryParse(bank.CenitCode, cenitCode) Then
                        listErrorsDetails.Add("El banco " + bank.Code + " - " + bank.Name + " no tiene código CENIT numérico")
                        Continue For
                    End If

                    'Me aseguro de rellenar el codigo con 0 a la izquierda para completar 4 caracteres si el codigo no los tiene
                    Dim safeCenitCode As String = Utils.StringPad(bank.CenitCode, 4, "0", Utils.PadType.STR_PAD_LEFT)

                    Dim lineDet As String = If(result.Length > 0, vbCrLf, String.Empty)
                    lineDet &= Utils.StringPad((i + 1), 5, 0, Utils.PadType.STR_PAD_LEFT) 'Secuencia
                    lineDet &= Utils.StringPad((i + 1), 5, 0, Utils.PadType.STR_PAD_LEFT) 'Secuencia
                    lineDet &= Utils.StringPad(DateNow, 6, 0, Utils.PadType.STR_PAD_LEFT) 'Fecha del movimiento
                    lineDet &= Utils.StringPad(supplier.ThirdParty.Nit, 15, 0, Utils.PadType.STR_PAD_LEFT) 'Numero de identificacion del proveedor
                    lineDet &= Utils.StringPad(supplier.ThirdParty.Name.ToUpper().ChangeCharacters, 22, " ", Utils.PadType.STR_PAD_RIGHT) 'Nombre del proveedor
                    lineDet &= safeCenitCode.Substring(2, 2) 'Codigo del banco del proveedor
                    lineDet += Utils.StringPad(If(account.Type = 1, "CA", "CC"), 2, "", Utils.PadType.STR_PAD_LEFT) 'Tipo de cuenta del proveedor
                    lineDet &= Utils.StringPad(account.Number.Trim().Replace("-", ""), 17, " ", Utils.PadType.STR_PAD_RIGHT) 'Numero de cuenta del proveedor
                    lineDet &= Utils.StringPad("CR", 2, " ", Utils.PadType.STR_PAD_RIGHT) 'Tipo de transaccion
                    lineDet &= Utils.StringPad(CDbl(SchedulePayment.SchedulePaymentDetail.Where(Function(x) x.SupplierId = listDetailSupplier(i)).ToList().Sum(Function(y) y.AmountPaid)), 10, 0, Utils.PadType.STR_PAD_LEFT) 'Valor
                    lineDet &= Utils.StringPad(String.Concat("CANCELACION FACTURAS ", String.Join(", ", SchedulePayment.SchedulePaymentDetail.Where(Function(x) x.SupplierId = listDetailSupplier(i) AndAlso Not String.IsNullOrEmpty(x.Invoice)).Select(Function(d) d.Invoice).ToArray())), 65, " ", Utils.PadType.STR_PAD_RIGHT) 'Observacion
                    lineDet &= Utils.StringPad(1, 2, 0, Utils.PadType.STR_PAD_LEFT) 'Identicador de registro
                    lineDet &= Utils.StringPad(String.Concat(CompanyThirdParty.Nit, CompanyThirdParty.DigitVerification), 15, 0, Utils.PadType.STR_PAD_LEFT) 'Numero de identificacion del cliente
                    lineDet &= Utils.StringPad(entityAccount.Number.Trim().Replace("-", ""), 17, " ", Utils.PadType.STR_PAD_RIGHT) 'Numero de cuenta del cliente
                    lineDet &= Utils.StringPad("CCDB", 4, " ", Utils.PadType.STR_PAD_RIGHT) 'Adicional
                    result.Append(lineDet)
                Next
            End If

            If listErrorsDetails.Count > 0 Then
                Return New ActionResult(Of String) With {.StateResult = False, .MessageResult = listErrorsDetails}
            End If
            Return New ActionResult(Of String) With {.StateResult = True, .Message = "DF_" + DateTime.Now.ToString("ddMMyyyy") + "_Itau", .ObjectEmbbeded = result.ToString()}
        Catch ex As Exception
            Return New ActionResult(Of String) With {.StateResult = False, .Message = ex.Message}
        End Try
    End Function

    ''' <summary>
    ''' metodo para generar archivo plano para el Banco Cooperativo Coopcentral
    ''' </summary>
    ''' <param name="SchedulePayment"></param>
    ''' <param name="companyNIT"></param>
    ''' <param name="companyName"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function GenerateCoopcentralFile(SchedulePayment As SchedulePayment, companyNIT As String, companyName As String) As ActionResult(Of String)
        Try
            Dim DateTransaction = Date.Now
            Dim DateNow As String = DateTransaction.ToString("MMddyy")

            Dim result As New StringBuilder()
            Dim listErrorsDetails As New List(Of String)

            If SchedulePayment IsNot Nothing Then
                Dim CompanyThirdParty = _thirdPartyRepository.GetThirdPartyByNit(companyNIT)
                If CompanyThirdParty Is Nothing OrElse CompanyThirdParty.Id = 0 Then
                    listErrorsDetails.Add("La empresa " + companyNIT + " no esta creada como tercero")
                    Return New ActionResult(Of String) With {.StateResult = False, .MessageResult = listErrorsDetails}
                End If

                Dim entityAccount = _entityBankAccountRepository.GetEntityBankAccountById(SchedulePayment.EntityBankAccountId)
                Dim listDetailSupplier = (From e In SchedulePayment.SchedulePaymentDetail Select e.SupplierId).Distinct().ToList()

                For i As Integer = 0 To listDetailSupplier.Count - 1 Step 1
                    'nit del beneficiario
                    Dim supplier = _supplierRepository.GetSupplierByIdWithSupplierBankAccount(listDetailSupplier(i))
                    Dim resultValidateSupplierAccount As ActionResult(Of SupplierBankAccount)

                    If supplier Is Nothing Then
                        Throw New ArgumentNullException("supplier")
                    End If

                    Dim schedulePaymentBankAccount = SchedulePayment?.SchedulePaymentBankAccount?.ToList().Find(Function(x) x.SupplierId = supplier?.Id)

                    'se busca si se guardaron datos en la nueva tabla de informacion bancaria para programacion de pago, de lo contrario sigue como se hacia.
                    If schedulePaymentBankAccount IsNot Nothing Then
                        Dim queryBank = supplier.SupplierBankAccount.ToList().Find(Function(x) x.Id = schedulePaymentBankAccount?.SupplierBankAccountId)
                        resultValidateSupplierAccount = New ActionResult(Of SupplierBankAccount) With {.StateResult = queryBank IsNot Nothing, .ObjectEmbbeded = queryBank}
                    Else
                        '    Valido las cuentas del proveedor
                        resultValidateSupplierAccount = ValidateSupplierAccount(supplier, entityAccount.IdBank)
                    End If

                    If resultValidateSupplierAccount.StateResult = False Then
                        listErrorsDetails.Add(resultValidateSupplierAccount.Message)
                        Continue For
                    End If

                    Dim account As SupplierBankAccount = resultValidateSupplierAccount.ObjectEmbbeded
                    Dim bank As Bank = _bankRepository.GetBankById(account.BankId)
                    If String.IsNullOrEmpty(bank.CenitCode) Then
                        listErrorsDetails.Add("El banco " + bank.Code + " - " + bank.Name + " no tiene código CENIT")
                        Continue For
                    End If
                    Dim cenitCode As Integer
                    If Not Integer.TryParse(bank.CenitCode, cenitCode) Then
                        listErrorsDetails.Add("El banco " + bank.Code + " - " + bank.Name + " no tiene código CENIT numérico")
                        Continue For
                    End If

                    Dim lineDet As String = If(result.Length > 0, vbCrLf, String.Empty)
                    lineDet &= Utils.StringPad(supplier.ThirdParty.Nit, 15, " ", Utils.PadType.STR_PAD_RIGHT) 'identificador cliente                    
                    lineDet += Utils.StringPad(CDbl(SchedulePayment.SchedulePaymentDetail.Where(Function(x) x.SupplierId = listDetailSupplier(i)).ToList().Sum(Function(y) y.AmountPaid)), 16, 0, Utils.PadType.STR_PAD_LEFT) 'valor de la transaccion parte entera
                    lineDet += Utils.StringPad("", 2, 0, Utils.PadType.STR_PAD_RIGHT) 'valor de la transaccion parte decimal
                    lineDet &= Utils.StringPad(supplier.ThirdParty.Name.ToUpper().ChangeCharacters, 22, " ", Utils.PadType.STR_PAD_RIGHT) 'Nombre del proveedor
                    lineDet &= Utils.StringPad("00001", 5, " ", Utils.PadType.STR_PAD_RIGHT) 'Código de Ruta - Por defecto es 00001
                    lineDet &= Utils.StringPad(cenitCode, 3, 0, Utils.PadType.STR_PAD_LEFT) 'Codigo cenit del banco del proveedor
                    lineDet &= Utils.StringPad(bank.CenitVerification, 1, 0, Utils.PadType.STR_PAD_LEFT) 'Codigo cenit verificacion del banco del proveedor
                    lineDet += Utils.StringPad(If(account.Type = 1, 32, 22), 2, "", Utils.PadType.STR_PAD_LEFT) 'tipo de transaccion
                    lineDet &= Utils.StringPad(account.Number.Trim().Replace("-", ""), 17, " ", Utils.PadType.STR_PAD_RIGHT) 'Numero de cuenta del proveedor
                    lineDet += Utils.StringPad("PAGO A PROVEEDORES", 24, " ", Utils.PadType.STR_PAD_LEFT) 'concepto                    
                    lineDet &= Utils.StringPad(String.Concat(companyNIT, supplier.ThirdParty.DigitVerification), 24, " ", Utils.PadType.STR_PAD_RIGHT) 'nit originador     
                    lineDet += Utils.StringPad("", 32, " ", Utils.PadType.STR_PAD_LEFT) 'espacios en blanco                                        
                    lineDet += Utils.StringPad("V", 1, " ", Utils.PadType.STR_PAD_LEFT) 'campo se llena con una V
                    result.Append(lineDet)
                Next
            End If

            If listErrorsDetails.Count > 0 Then
                Return New ActionResult(Of String) With {.StateResult = False, .MessageResult = listErrorsDetails}
            End If
            Return New ActionResult(Of String) With {.StateResult = True, .Message = "DF_" + DateTime.Now.ToString("ddMMyyyy") + "_Coopcentral", .ObjectEmbbeded = result.ToString()}
        Catch ex As Exception
            Return New ActionResult(Of String) With {.StateResult = False, .Message = ex.Message}
        End Try
    End Function

    ''' <summary>
    ''' Genera el archivo plano de Scotiabank
    ''' </summary>
    ''' <param name="SchedulePayment"></param>
    ''' <returns></returns>
    Public Function GenerateScotiabankFile(SchedulePayment As SchedulePayment) As ActionResult(Of String)
        Try
            Dim result As New StringBuilder()
            Dim lineHead As String = "CD TEF" 'Primea linea o cabecera del documento
            Dim officialCurrencyId = _companySettingRepository.FirstOrDefault(Function(x) True, False)?.OfficialCurrencyId
            If SchedulePayment IsNot Nothing Then
                ' Obtener el nombre del tercero desde GeneralLedgerSettings
                Dim thirdPartyName As String = GetThirdPartyNameFromGeneralLedgerSettings(SchedulePayment.OperativeUnitId)

                Dim delimiterCharacter As String = "#..#"
                Dim listErrorsDetails As New List(Of String)
                Dim entityAccount = _entityBankAccountRepository.GetEntityBankAccountById(SchedulePayment.EntityBankAccountId)
                Dim listDetailSupplier = (From e In SchedulePayment.SchedulePaymentDetail Select e.SupplierId).Distinct().ToList()
                result.AppendLine(lineHead)
                For Each supplierId In listDetailSupplier
                    'nit del beneficiario
                    Dim supplier = _supplierRepository.GetSupplierByIdWithSupplierBankAccount(supplierId)

                    If supplier Is Nothing Then
                        Throw New ArgumentNullException("supplier")
                    End If

                    'se busca si se guardaron datos en la nueva tabla de informacion bancaria para programacion de pago, de lo contrario sigue como se hacia.
                    Dim suplierBank As ActionResult(Of SupplierBankAccount)
                    Dim schedulePaymentBankAccount = SchedulePayment?.SchedulePaymentBankAccount?.ToList()?.Find(Function(x) x.SupplierId = supplierId)

                    If schedulePaymentBankAccount IsNot Nothing Then
                        Dim queryBank = supplier.SupplierBankAccount.ToList().Find(Function(x) x.Id = schedulePaymentBankAccount?.SupplierBankAccountId)
                        suplierBank = New ActionResult(Of SupplierBankAccount) With {.StateResult = queryBank IsNot Nothing, .ObjectEmbbeded = queryBank}
                    Else
                        suplierBank = ValidateSupplierAccount(supplier, entityAccount.IdBank)
                    End If

                    Dim accountBank As SupplierBankAccount
                    If Not suplierBank.StateResult Then
                        listErrorsDetails.Add(suplierBank.Message)
                        Continue For
                    Else
                        accountBank = suplierBank.ObjectEmbbeded
                    End If

                    Dim listCurrency = SchedulePayment?.SchedulePaymentDetail?.
                                                        Where(Function(w) w.SupplierId = supplierId)?.
                                                        Select(Function(s) If(s.AccountPayable?.CurrencyId Is Nothing,
                                                                                officialCurrencyId, s.AccountPayable?.CurrencyId))?.ToList()
                    Dim lineDetail As String = String.Empty
                    lineDetail += accountBank.Number

                    For Each itemCurrency In listCurrency
                        lineDetail += Utils.StringPad(GetCurrencyIdentification(itemCurrency), 1, "")
                        Dim Amount = SchedulePayment.SchedulePaymentDetail.Where(Function(w) w.SupplierId = supplierId AndAlso w.AccountPayable.CurrencyId = itemCurrency).Sum(Function(s) s.AmountPaid)
                        lineDetail += Utils.StringPad(Amount, 16, delimiterCharacter)
                    Next

                    lineDetail += Utils.StringPad(supplier.Code, 20, delimiterCharacter)
                    lineDetail += Utils.StringPad(If(String.IsNullOrWhiteSpace(thirdPartyName), String.Empty, $"PAGO {thirdPartyName}"), 30, delimiterCharacter)
                    lineDetail += Utils.StringPad(supplier.supplierEmail, 50, delimiterCharacter)
                    lineDetail += Utils.StringPad("TRANSFERENCIA REALIZADA", 50, delimiterCharacter)
                    result.AppendLine(lineDetail)
                Next
                If listErrorsDetails.Count > 0 Then
                    Return New ActionResult(Of String) With {.StateResult = False, .MessageResult = listErrorsDetails}
                End If
                Return New ActionResult(Of String) With {.StateResult = True, .Message = String.Format("SRC-{0}", SchedulePayment.Code), .ObjectEmbbeded = result.ToString()}
            End If
        Catch ex As Exception
            Return New ActionResult(Of String) With {.StateResult = False, .Message = Utils.GetInnerExceptionMessageToString(ex)}
        End Try
    End Function

    ''' <summary>
    ''' Genera el archivo plano de GNV Sudameris
    ''' </summary>
    ''' <param name="SchedulePayment"></param>
    ''' <returns></returns>
    Public Function GenerateBancoGNVSudameris(SchedulePayment As SchedulePayment, companyNIT As String, companyName As String) As ActionResult(Of String)
        Try
            Dim result As New StringBuilder()
            Dim listErrorsDetails As New List(Of String)

            If SchedulePayment IsNot Nothing Then
                Dim CompanyThirdParty = _thirdPartyRepository.GetThirdPartyByNit(companyNIT)
                If CompanyThirdParty Is Nothing OrElse CompanyThirdParty.Id = 0 Then
                    listErrorsDetails.Add("La empresa " + companyNIT + " no esta creada como tercero")
                    Return New ActionResult(Of String) With {.StateResult = False, .MessageResult = listErrorsDetails}
                End If

                Dim entityAccount = _entityBankAccountRepository.GetEntityBankAccountById(SchedulePayment.EntityBankAccountId)
                Dim listDetailSupplier = (From e In SchedulePayment.SchedulePaymentDetail Select e.SupplierId).Distinct().ToList()

                If listDetailSupplier.Any() Then
                    For Each item In listDetailSupplier
                        'Nit del beneficiario
                        Dim supplier = _supplierRepository.GetSupplierByIdWithSupplierBankAccount(item)
                        Dim resultValidateSupplierAccount As ActionResult(Of SupplierBankAccount)

                        If supplier Is Nothing Then
                            Throw New ArgumentNullException("supplier")
                        End If

                        Dim schedulePaymentBankAccount = SchedulePayment?.SchedulePaymentBankAccount?.ToList().Find(Function(x) x.SupplierId = supplier?.Id)

                        'se busca si se guardaron datos en la nueva tabla de informacion bancaria para programacion de pago, de lo contrario sigue como se hacia.
                        If schedulePaymentBankAccount IsNot Nothing Then
                            Dim queryBank = supplier.SupplierBankAccount.ToList().Find(Function(x) x.Id = schedulePaymentBankAccount?.SupplierBankAccountId)
                            resultValidateSupplierAccount = New ActionResult(Of SupplierBankAccount) With {.StateResult = queryBank IsNot Nothing, .ObjectEmbbeded = queryBank}
                        Else
                            'Valido las cuentas del proveedor
                            resultValidateSupplierAccount = ValidateSupplierAccount(supplier, entityAccount.IdBank)
                        End If

                        If resultValidateSupplierAccount.StateResult = False Then
                            listErrorsDetails.Add(resultValidateSupplierAccount.Message)
                            Continue For
                        End If

                        Dim account As SupplierBankAccount = resultValidateSupplierAccount.ObjectEmbbeded
                        Dim bank As Bank = _bankRepository.GetBankById(account.BankId)
                        If String.IsNullOrEmpty(bank.CenitCode) Then
                            listErrorsDetails.Add("El banco " + bank.Code + " - " + bank.Name + " no tiene código CENIT")
                            Continue For
                        End If

                        Dim cenitCode As Integer
                        If Not Integer.TryParse(bank.CenitCode, cenitCode) Then
                            listErrorsDetails.Add("El banco " + bank.Code + " - " + bank.Name + " no tiene código CENIT numérico")
                            Continue For
                        End If
                        ''se separa el valor 
                        Dim valor = CDbl(SchedulePayment.SchedulePaymentDetail.Where(Function(x) x.SupplierId = item).ToList().Sum(Function(y) y.AmountPaid)).ToString().Split(",")
                        Dim entero = valor(0)
                        Dim decimalPart = 0
                        If valor.Count > 1 Then
                            decimalPart = valor(1)
                        End If

                        Dim lineDet As String = If(result.Length > 0, vbCrLf, String.Empty)
                        lineDet += Utils.StringPad("  ", 6, " ", Utils.PadType.STR_PAD_LEFT)
                        lineDet += Utils.StringPad(entityAccount.Number.Trim().Replace("-", ""), 16, " ", Utils.PadType.STR_PAD_RIGHT) 'CUENTA A DEBITAR
                        lineDet += Utils.StringPad(If(entityAccount.Type = 1, 21, 20), 2, " ", Utils.PadType.STR_PAD_RIGHT) 'TIPO DE CUENTA A DEBITAR
                        lineDet += Utils.StringPad(bank.CenitCode, 6, " ", Utils.PadType.STR_PAD_RIGHT) 'CODIGO CENIT DEL BANCO RECEPTOR
                        lineDet += Utils.StringPad(If(account.Type = 1, 21, 20), 2, " ", Utils.PadType.STR_PAD_RIGHT) 'TIPO DE CUENTA RECEPTORA
                        lineDet += Utils.StringPad(account.Number.Trim().Replace("-", " "), 16, " ", Utils.PadType.STR_PAD_RIGHT) 'NUMERO DE CUENTA RECEPTORA
                        lineDet += Utils.StringPad(supplier.ThirdParty?.Nit, 12, " ", Utils.PadType.STR_PAD_RIGHT) 'IDENTIFICACION DEL PARTICIPANTE
                        lineDet += Utils.StringPad(supplier.ThirdParty?.Name.ToUpper().ChangeCharacters, 22, " ", Utils.PadType.STR_PAD_RIGHT) 'NOMBRE DEL PARTICIPANTE
                        lineDet += Utils.StringPad("Cancelacion facturas No", 80, " ", Utils.PadType.STR_PAD_RIGHT) 'DESCRIPCION DEL PAGO
                        lineDet += Utils.StringPad(entero, 13, 0, Utils.PadType.STR_PAD_LEFT) 'VALOR
                        lineDet += Utils.StringPad(decimalPart, 2, 0, Utils.PadType.STR_PAD_RIGHT) 'PARTE DECIMAL DEL VALOR
                        result.Append(lineDet)
                    Next
                End If
            End If

            If listErrorsDetails.Count > 0 Then
                Return New ActionResult(Of String) With {.StateResult = False, .MessageResult = listErrorsDetails}
            End If

            Return New ActionResult(Of String) With {.StateResult = True, .Message = "PRN_" + DateTime.Now.ToString("ddMMyyyy") + "GNV_Sudameris", .ObjectEmbbeded = result.ToString()}
        Catch ex As Exception
            Return New ActionResult(Of String) With {.StateResult = False, .Message = ex.Message}
        End Try
    End Function

    ''' <summary>
    ''' Genera el archivo plano de Davivienda  CRC
    ''' </summary>
    ''' <param name="SchedulePayment"></param>
    ''' <returns></returns>
    Public Function GenerateDaviviendaCRCFile(SchedulePayment As SchedulePayment, Optional idBank As Integer? = Nothing) As ActionResult(Of String)
        Try
            If SchedulePayment Is Nothing Then
                Return New ActionResult(Of String) With {.StateResult = False, .MessageResult = {"No hay datos Para construir el archivo plano"}.ToList()}
            End If

            Dim result As New StringBuilder()
            Const delimiterCharacter As String = ""
            Dim listErrorsDetails As New List(Of String)
            Dim listSuppliers As List(Of Supplier)
            Dim listSuppliersIds As List(Of Integer)

            ' Obtener el nombre del tercero desde GeneralLedgerSettings
            Dim thirdPartyName As String = GetThirdPartyNameFromGeneralLedgerSettings(SchedulePayment.OperativeUnitId)

            If idBank Is Nothing Then
                Dim entityAccount = _entityBankAccountRepository.GetEntityBankAccountById(SchedulePayment.EntityBankAccountId)
                idBank = entityAccount.IdBank
            End If

            listSuppliersIds = (From e In SchedulePayment.SchedulePaymentDetail Select e.SupplierId).Distinct().ToList()
            If listSuppliersIds Is Nothing OrElse Not listSuppliersIds?.Any() Then
                Return New ActionResult(Of String) With {.StateResult = False, .MessageResult = {"La programación de pagos no tiene información de proveedores"}.ToList()}
            End If
            'se consulta masivamente los proveedores
            listSuppliers = _supplierRepository.GetByFilter(Function(x) listSuppliersIds.Contains(x.Id), True, {"ThirdParty.Person", "SupplierBankAccount"})
            If listSuppliers Is Nothing OrElse Not listSuppliers?.Any() Then
                Return New ActionResult(Of String) With {.StateResult = False, .MessageResult = {"No se encontrarón datos de los proveedores"}.ToList()}
            End If

            For Each supplierId In listSuppliersIds
                'nit del beneficiario
                Dim supplier = listSuppliers.FirstOrDefault(Function(x) x.Id = supplierId)

                If supplier Is Nothing Then
                    Throw New ArgumentNullException("supplier")
                End If

                'se busca si se guardaron datos en la nueva tabla de informacion bancaria para programacion de pago, de lo contrario sigue como se hacia.
                Dim suplierBank As ActionResult(Of SupplierBankAccount)
                Dim schedulePaymentBankAccount = SchedulePayment?.SchedulePaymentBankAccount?.ToList()?.Find(Function(x) x.SupplierId = supplierId)

                If schedulePaymentBankAccount IsNot Nothing Then
                    Dim queryBank = supplier.SupplierBankAccount.ToList().Find(Function(x) x.Id = schedulePaymentBankAccount?.SupplierBankAccountId)
                    suplierBank = New ActionResult(Of SupplierBankAccount) With {.StateResult = queryBank IsNot Nothing, .ObjectEmbbeded = queryBank}
                Else
                    suplierBank = ValidateSupplierAccount(supplier, idBank)
                End If

                Dim accountBank As SupplierBankAccount
                If Not suplierBank.StateResult Then
                    listErrorsDetails.Add(suplierBank.Message)
                    Continue For
                Else
                    accountBank = suplierBank.ObjectEmbbeded
                End If

                Dim lineDetail As String = String.Empty
                lineDetail += Utils.StringMaxLenth(accountBank.Number, 22, ",")
                lineDetail += Utils.StringMaxLenth(supplier.Name.CleanSpecialChars(".").Replace("_", ""), 500, ",")
                lineDetail += Utils.StringMaxLenth(supplier.Code, 20, ",")
                Dim Amount = SchedulePayment.SchedulePaymentDetail.Where(Function(w) w.SupplierId = supplierId).Sum(Function(s) s.AmountPaid).ToString.Replace(",", ".")
                lineDetail += Utils.StringMaxLenth(Amount, 17, ",")
                lineDetail += Utils.StringMaxLenth(If(String.IsNullOrWhiteSpace(thirdPartyName), String.Empty, $"PAGO {thirdPartyName}"), 200, ",")
                Dim MessageNotification = String.Join(" ", SchedulePayment?.SchedulePaymentDetail?. _
                                            Where(Function(w) w.SupplierId = supplierId)?.Select(Function(x) x.Invoice).ToList())
                lineDetail += Utils.StringMaxLenth($"Pago Facturas {MessageNotification}", 255, ",")
                lineDetail += Utils.StringMaxLenth(If(String.IsNullOrWhiteSpace(thirdPartyName), String.Empty, $"Pago Facturas {thirdPartyName}"), 50)
                result.AppendLine(lineDetail)
            Next
            If listErrorsDetails.Count > 0 Then
                Return New ActionResult(Of String) With {.StateResult = False, .MessageResult = listErrorsDetails}
            End If
            Return New ActionResult(Of String) With {.StateResult = True, .Message = String.Format("PAGO-PROV-{0}", SchedulePayment.Code), .ObjectEmbbeded = result.ToString()}
        Catch ex As Exception
            Return New ActionResult(Of String) With {.StateResult = False, .MessageResult = {Utils.GetInnerExceptionMessageToString(ex)}.ToList()}
        End Try
    End Function
    ''' <summary>
    ''' Generacion del archivo plano para BCT
    ''' </summary>
    ''' <param name="schedulePayment"></param>
    ''' <param name="idBank"></param>
    ''' <param name="CompanyNIT"></param>
    ''' <param name="companyName"></param>
    ''' <returns></returns>
    Public Function GenerateBCTFile(schedulePayment As SchedulePayment, Optional idBank As Integer? = Nothing, Optional CompanyNIT As String = Nothing, Optional companyName As String = Nothing) As ActionResult(Of String)
        Try
            Dim result As New StringBuilder()
            Dim listErrorsDetails As New List(Of String)

            If schedulePayment IsNot Nothing Then
                ' Obtener el nombre del tercero desde GeneralLedgerSettings
                Dim thirdPartyName As String = GetThirdPartyNameFromGeneralLedgerSettings(schedulePayment.OperativeUnitId)

                Dim CompanyThirdParty = _thirdPartyRepository.GetThirdPartyByNit(CompanyNIT)
                If CompanyThirdParty Is Nothing OrElse CompanyThirdParty.Id = 0 Then
                    listErrorsDetails.Add("La empresa " + CompanyNIT + " no esta creada como tercero")
                    Return New ActionResult(Of String) With {.StateResult = False, .MessageResult = listErrorsDetails}
                End If

                Dim entityAccount = _entityBankAccountRepository.GetEntityBankAccountById(schedulePayment.EntityBankAccountId)
                Dim listDetailSupplier = (From e In schedulePayment.SchedulePaymentDetail Select e.SupplierId).Distinct().ToList()


                If listDetailSupplier.Any() Then
                    For Each item In listDetailSupplier
                        'Nit del beneficiario
                        Dim supplier = _supplierRepository.GetSupplierByIdWithSupplierBankAccount(item)
                        Dim resultValidateSupplierAccount As ActionResult(Of SupplierBankAccount)

                        If supplier Is Nothing Then
                            Throw New ArgumentNullException("supplier")
                        End If

                        Dim schedulePaymentBankAccount = schedulePayment?.SchedulePaymentBankAccount?.ToList().Find(Function(x) x.SupplierId = supplier?.Id)

                        'se busca si se guardaron datos en la nueva tabla de informacion bancaria para programacion de pago, de lo contrario sigue como se hacia.
                        If schedulePaymentBankAccount IsNot Nothing Then
                            Dim queryBank = supplier.SupplierBankAccount.ToList().Find(Function(x) x.Id = schedulePaymentBankAccount?.SupplierBankAccountId)
                            resultValidateSupplierAccount = New ActionResult(Of SupplierBankAccount) With {.StateResult = queryBank IsNot Nothing, .ObjectEmbbeded = queryBank}
                        Else
                            'Valido las cuentas del proveedor
                            resultValidateSupplierAccount = ValidateSupplierAccount(supplier, entityAccount.IdBank)
                        End If

                        If resultValidateSupplierAccount.StateResult = False Then
                            listErrorsDetails.Add(resultValidateSupplierAccount.Message)
                            Continue For
                        End If

                        Dim account As SupplierBankAccount = resultValidateSupplierAccount.ObjectEmbbeded
                        ' Dim bank As Bank = _bankRepository.GetBankById(account.BankId)
                        Dim lineDet As String = If(result.Length > 0, vbCrLf, String.Empty)
                        Dim documentType As String
                        Select Case supplier.ThirdParty.Person.ADTIPOIDENTIFICA.SIGLA
                            Case "PA" 'PASAPORTE
                                documentType = String.Empty
                            Case "CF" ' Cedula Fisica
                                documentType = "0"
                            Case "CJ" 'Cedula Juridica
                                documentType = "3"
                            Case "DM" 'DIMEX
                                documentType = "1"
                            Case "NI" 'NITE
                                documentType = String.Empty
                            Case "SI" 'DIDI
                                documentType = "1"
                            Case "GO" 'Gobierno
                                documentType = "2"
                            Case "NC" 'No Contribuyente
                                documentType = "2"
                            Case "IA" '	Institución Autónoma
                                documentType = "4"
                            Case Else
                                documentType = String.Empty
                        End Select


                        lineDet += documentType & "," 'TIPO DE DOCUMENTO
                        lineDet += FormatNit(supplier.ThirdParty?.Nit, supplier.ThirdParty.Person.ADTIPOIDENTIFICA.SIGLA) & "," 'IDENTIFICACION
                        lineDet += Utils.StringMaxLenth(CleanName(supplier.ThirdParty?.Name), 100, ",") 'NOMBRE DE LA CUENTA BENEFICIARIA
                        If supplier.SupplierBankAccount.Any(Function(t) t.PaymentDefault = True AndAlso Not String.IsNullOrWhiteSpace(t.Number)) Then
                            Dim ibanBankAccount = supplier.SupplierBankAccount.Where(Function(s) s.PaymentDefault).Select(Function(s) s.Number).FirstOrDefault()
                            lineDet += Utils.StringMaxLenth(ibanBankAccount, 22, ",") 'CUENTAPOR DEFECTO PARA PAGO AUTOMATICO
                        End If
                        Dim Amount = schedulePayment.SchedulePaymentDetail.Where(Function(w) w.SupplierId = item).Sum(Function(s) s.AmountPaid).ToString.Replace(",", ".") 'MONTO A TRANSFERIR
                        lineDet += Amount & ","

                        If account.Currency Is Nothing Then
                            listErrorsDetails.Add("El proveedor " + account.Supplier.Code + " - " + account.Supplier.Name + " no tiene parametrizado la moneda en la cuenta bancaria:  " + account.Number)
                        End If

                        Dim currency As String
                        Select Case account.Currency?.Abbreviation
                            Case "CRC"
                                currency = "1"
                            Case "USD"
                                currency = "2"
                            Case Else
                                currency = String.Empty
                        End Select
                        lineDet += Utils.StringMaxLenth(currency, 1, ",") 'MONEDA
                        lineDet += Utils.StringMaxLenth(If(String.IsNullOrWhiteSpace(thirdPartyName), String.Empty, $"Pago Facturas {thirdPartyName}"), 35, ",") 'CONCEPTO
                        lineDet += "O" & "," 'ORIGEN 
                        Dim MessageNotification As String = String.Empty

                        'Concatenamos los números de facturas para ser mostradas en el campo de observaciones del archivo plano
                        Dim listA = schedulePayment?.SchedulePaymentDetail?. _
                                          Where(Function(w) w.SupplierId = item)?.Select(Function(x) x.Invoice).ToList()

                        'Limpiamos la lista de campos vacíos
                        If listA.Count > 0 Then listA.RemoveAll(Function(x) String.IsNullOrEmpty(x))

                        'Si la lista viene vacía buscamos la relación del comprobante con la cuenta por pagar que tenga
                        If listA Is Nothing Or Not listA.Count > 0 Then
                            'Traemos todos los IDs de los detalles de comprobantes de egreso
                            Dim idVoucherTransactionD = schedulePayment?.SchedulePaymentDetail?.Select(Function(x) x.Id).ToList()

                            'Inicializamos la lista que almacenará las facturas a mostrar
                            Dim billNumbers As New List(Of String)

                            'Relacionamos los detalles de comprobantes de egreso para así consultar las facturas asociadas de las cuentas por pagar
                            For Each detailId In idVoucherTransactionD
                                Dim dischargeBills = _dischargeBillRepository.ListDischargeBillByIdVoucherTransactionD(detailId)
                                If dischargeBills IsNot Nothing Then
                                    For Each db In dischargeBills
                                        Dim ap = _accountPayableRepository.GetAccountPayableById(db.IdAccountPayable)
                                        If ap IsNot Nothing Then
                                            billNumbers.Add(ap.BillNumber)
                                        End If
                                    Next
                                End If
                            Next

                            'Sobre escribimos la lista inicial para mostrar los valores consultados
                            listA = billNumbers
                        End If

                        MessageNotification = String.Join("-", listA.Select(Function(i) CleanInvoiceText(i)))

                        lineDet += Utils.StringMaxLenth(supplier.supplierEmail, 100, ",")
                        lineDet += Utils.StringMaxLenth($"Dispersion de fondos - {MessageNotification}", 200) & "," 'OBSERVACION
                        result.Append(lineDet)
                    Next
                End If
            End If

            If listErrorsDetails.Count > 0 Then
                Return New ActionResult(Of String) With {.StateResult = False, .MessageResult = listErrorsDetails}
            End If

            Return New ActionResult(Of String) With {.StateResult = True, .Message = "PRN_" + DateTime.Now.ToString("ddMMyyyy") + "BancoBCT_Archivo_Plano", .ObjectEmbbeded = result.ToString()}
        Catch ex As Exception
            Return New ActionResult(Of String) With {.StateResult = False, .Message = ex.Message}
        End Try
    End Function
    ''' <summary>
    ''' Obtiene el nombre del tercero asociado al IdDian desde GeneralLedgerSettings
    ''' </summary>
    ''' <param name="operativeUnitId">Id de la unidad operativa</param>
    ''' <returns>Nombre del tercero o String.Empty si no se encuentra</returns>
    Private Function GetThirdPartyNameFromGeneralLedgerSettings(operativeUnitId As Integer) As String
        Try
            Dim generalLedgerSettings = _settingsAccountRepository.GetSettingAccountSimple(operativeUnitId)
            If generalLedgerSettings IsNot Nothing AndAlso generalLedgerSettings.IdDian > 0 Then
                Dim thirdParty = _thirdPartyRepository.GetThirdPartyById(generalLedgerSettings.IdDian)
                If thirdParty IsNot Nothing AndAlso Not String.IsNullOrWhiteSpace(thirdParty.Name) Then
                    Return thirdParty.Name
                End If
            End If
        Catch ex As Exception
            Return String.Empty
        End Try
        Return String.Empty
    End Function

    ''' <summary>
    ''' metodo para validar las cuentas de los proveedores
    ''' </summary>
    ''' <param name="supplier"></param>
    ''' <param name="bankId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function ValidateSupplierAccount(supplier As Supplier, bankId As Integer) As ActionResult(Of SupplierBankAccount)
        Dim account As SupplierBankAccount = Nothing
        If supplier.SupplierBankAccount.Count = 0 Then
            Return New ActionResult(Of SupplierBankAccount) With {.StateResult = False, .Message = "El proveerdor " + supplier.Name + " no tiene cuentas parametrizadas"}
        End If
        If supplier.PrioritizeBankAccount Then
            ''si el parametro de priorizar cuentas del mismo banco esta activo tomo la cuenta que pertenece al mismo banco 
            account = supplier.SupplierBankAccount.Where(Function(x) x.BankId = bankId).FirstOrDefault()
            If account Is Nothing Then
                'si el proveedor no tiene cuentas del mismo banco tomo la que esta por defecto
                account = supplier.SupplierBankAccount.Where(Function(x) x.PaymentDefault).FirstOrDefault()
                If account Is Nothing Then
                    'si no tiene cuenta por defecto para el pago agrego un error al listado
                    Return New ActionResult(Of SupplierBankAccount) With {.StateResult = False, .Message = "El proveedor " + supplier.Name + " no tiene una cuenta por defecto para realizar el pago"}
                End If
            End If
        Else
            'si el parametro de pririzar no esta activo tomo la cuenta por defecto 
            account = supplier.SupplierBankAccount.Where(Function(x) x.PaymentDefault).FirstOrDefault()
            If account Is Nothing Then
                'si no tiene cuenta por defecto para el pago agrego un error al listado
                Return New ActionResult(Of SupplierBankAccount) With {.StateResult = False, .Message = "El proveedor " + supplier.Name + " no tiene una cuenta por defecto para realizar el pago"}
            End If
        End If
        Return New ActionResult(Of SupplierBankAccount) With {.StateResult = True, .ObjectEmbbeded = account}
    End Function

    Public Function GetCurrencyIdentification(CurrencyId As Integer?) As Byte
        Dim currency As Currency = Nothing
        Dim dictionaryCurrency As New Dictionary(Of Integer, Currency)()

        If CurrencyId Is Nothing Then
            CurrencyId = _companySettingRepository.FirstOrDefault(Function(x) True, False)?.OfficialCurrencyId
        End If

        If Not dictionaryCurrency.ContainsKey(CurrencyId) Then
            currency = _currencyRepository.FirstOrDefault(Function(g) g.Id = CurrencyId)
            dictionaryCurrency.Add(CurrencyId, currency)

        ElseIf currency Is Nothing Then
            currency = dictionaryCurrency(CurrencyId)
        End If

        Dim CurrencyAbbreviation = currency.Abbreviation
        Select Case CurrencyAbbreviation
            Case "CRC"
                Return 1
            Case "USD"
                Return 2
            Case Else
                Return 0
        End Select
    End Function
    ''' <summary>
    ''' Limpia el nombre
    ''' </summary>
    ''' <param name="input"></param>
    ''' <returns></returns>
    Public Function CleanName(input As String) As String
        If String.IsNullOrEmpty(input) Then Return ""

        input = input.ToUpper().ChangeCharacters()

        input = input.Replace("Ñ", "").Replace("ñ", "")
        input = New String(input.Where(Function(c) Char.IsLetterOrDigit(c) OrElse Char.IsWhiteSpace(c)).ToArray())

        If input.Length > 100 Then
            input = input.Substring(0, 100)
        End If

        Return input.Trim()
    End Function
    ''' <summary>
    ''' Formatea el Nit
    ''' </summary>
    ''' <param name="nit"></param>
    ''' <param name="sigla"></param>
    ''' <returns></returns>
    Public Function FormatNit(nit As String, sigla As String) As String
        If String.IsNullOrWhiteSpace(nit) OrElse String.IsNullOrWhiteSpace(sigla) Then
            Return nit
        End If

        ' Eliminar cualquier carácter que no sea dígito
        nit = Regex.Replace(nit, "[^0-9]", "")
        Dim number As Integer = nit.Length
        Select Case sigla
            Case "CF" ' Físico Nacional: #-####-####
                If number >= 8 Then
                    Dim formattedPart = $"{nit(0)}-{nit.Substring(1, 4)}-{nit.Substring(5)}"

                    Return formattedPart
                End If

            Case "CJ", "GO" ' Jurídico Nacional / Gobierno: #-###-######
                If number >= 8 Then
                    Dim formattedPart = $"{nit(0)}-{nit.Substring(1, 3)}-{nit.Substring(4, number - 4)}"

                    Return formattedPart
                End If

            Case "IA" ' Institución Autónoma: #-000-########
                If number >= 8 Then
                    Dim formattedPart = $"{nit.Substring(0, 1)}-{nit.Substring(1, 3)}-{nit.Substring(4)}"

                    Return formattedPart
                End If
        End Select

        ' Si no aplica formato especial o no cumple longitud mínima, devolver limpio
        Return nit
    End Function
    ''' <summary>
    ''' Limpia el texto de cada factura
    ''' </summary>
    ''' <param name="text"></param>
    ''' <returns></returns>
    Public Function CleanInvoiceText(text As String) As String
        If String.IsNullOrWhiteSpace(text) Then Return ""

        ' 1. Convertir a mayúsculas
        'text = text.ToUpperInvariant()

        ' 2. Reemplazar vocales acentuadas por su equivalente sin tilde
        Dim accents As Dictionary(Of String, String) = New Dictionary(Of String, String) From {
        {"Á", "A"}, {"É", "E"}, {"Í", "I"}, {"Ó", "O"}, {"Ú", "U"}
        }
        For Each pair In accents
            text = text.Replace(pair.Key, pair.Value)
        Next

        ' 3. Eliminar Ñ y ñ
        text = text.Replace("Ñ", "").Replace("ñ", "")

        ' 4. Eliminar comas
        text = text.Replace(",", "")

        ' 5. Eliminar cualquier carácter que no sea letra, número o espacio
        text = Regex.Replace(text, "[^A-Z0-9\s]", "")

        Return text.Trim()
    End Function



#End Region

#End Region

#Region "IDisposable Support"
    Private disposedValue As Boolean ' Para detectar llamadas redundantes

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not disposedValue Then
            If disposing Then

            End If
            _cashRegisterRepository = Nothing
            _entityBankAccountRepository = Nothing
            _accountReceivableRepository = Nothing
            _advancePaymentRepository = Nothing
            _expenseConceptRepository = Nothing
            _dischargeBillRepository = Nothing
            _accountPayableRepository = Nothing
            _portfolioAdvanceRepository = Nothing
            _refundRepository = Nothing
            _closeMonthRepository = Nothing
            _mainAccountRepository = Nothing
            _supplierRepository = Nothing
            _crossingAccountDetailCxPRepository = Nothing
            _crossingAccountDetailCxCRepository = Nothing
            _voucherTransactionRepository = Nothing
            _cashRecepitRepository = Nothing
            _bankRepository = Nothing
            _crossingAccountDetailOtherConceptsRepository = Nothing
            _accountReceivableAccountingRepository = Nothing
            _currencyRepository = Nothing
            IndigoGC.Execute()
        End If
        disposedValue = True
    End Sub

    ' Visual Basic agrega este código para implementar correctamente el patrón descartable.
    Public Sub Dispose() Implements IDisposable.Dispose
        Dispose(True)
        GC.SuppressFinalize(Me)
    End Sub
#End Region

End Class

#Region "Enums"
Public Enum eExpenseType
    BankAccount = 1
    MinorCash = 2
    MajorCash = 3
End Enum

Public Enum ePaymentMethod
    Check = 1
    DebitNote = 2
    Pse = 3
End Enum

Public Enum eNature
    Debit = 1
    Credit = 2
End Enum

Public Enum eStatusAccountPayable
    Registrado = 1
    Confirmado = 2
    Anulado = 3
End Enum

#End Region