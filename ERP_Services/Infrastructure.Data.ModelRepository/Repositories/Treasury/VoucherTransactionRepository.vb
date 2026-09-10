'***********************************************************************
' Assembly         : Infrastructure.Data.TreasuryRepositiry
' Author           : Diego Andrés Roldán Lozano
' Created          : 13-06-2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Infrastructure.Data.Base
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Resources
Imports System.Data.Entity.Core.Objects
Imports System.Data.Entity.Infrastructure
Imports Domain.Payroll

Public Class VoucherTransactionRepository
    Inherits GenericRepository(Of VoucherTransaction)
    Implements IVoucherTransactionRepository

    'Contexto de payroll
    Private _context As IGlobalModelUnitOfWork

    Private _generalLedgerIVA As IGeneralLedgerIVARepository

    Private _mainAccounts As ICostDistributionsRepository

    Private _CostCenter As Domain.Payroll.ICostCenterRepository

    Private CostCenterDataCodeName As String

    Public Sub New(ByVal context As IGlobalModelUnitOfWork, ByVal generalLedgerIVA As IGeneralLedgerIVARepository, ByVal costCenter As Domain.Payroll.ICostCenterRepository, ByVal mainAccount As ICostDistributionsRepository)
        MyBase.New(context)
        _context = context

        If generalLedgerIVA Is Nothing Then
            Throw New ArgumentNullException("generalLedgerIVA")
        End If
        If costCenter Is Nothing Then
            Throw New ArgumentNullException("costCenter")
        End If
        _generalLedgerIVA = generalLedgerIVA

        _CostCenter = costCenter

        _mainAccounts = mainAccount
    End Sub

    ''' <summary>
    ''' lista todos los documnetos para confirmarlos masivamente
    ''' </summary>
    ''' <param name="listDocuments"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListVoucherTransactionMassiveConfirm(listDocuments As List(Of String)) As List(Of VoucherTransaction) Implements IVoucherTransactionRepository.ListVoucherTransactionMassiveConfirm
        Return (From vt In _context.VoucherTransaction.Include("VoucherTransactionDetails").Include("VoucherTransactionDetails.VoucherTransactionAdvance").Include("VoucherTransactionDetails.DischargeBill") Where listDocuments.Contains(vt.Code) Select vt).ToList()
    End Function


    ''' <summary>
    ''' Obtiene un comprobante de egreso por codigo
    ''' </summary>
    Public Function GetVoucherTransaction(code As String) As VoucherTransaction Implements IVoucherTransactionRepository.GetVoucherTransaction
        If code Is Nothing OrElse code.Trim().Equals(String.Empty) Then
            Throw New ArgumentNullException("code")
        End If
        Dim voucher As New VoucherTransaction
        Dim res = (From VTC As VoucherTransaction In Me._context.VoucherTransaction.
                                                     Include("EntityBankAccounts.Currency").AsNoTracking().
                                                     Include("EntityBankAccounts.Bank").AsNoTracking().
                                                     Include("Currency").AsNoTracking()
                   Where VTC.Code.Equals(code.Trim()) Select VTC).ToList()

        If res IsNot Nothing AndAlso res.Count > 0 Then

            Dim IdCashRegister As Integer? = res(0).IdCashRegister
            Dim IdThird As Integer? = res(0).IdThirdParty
            Dim IdCostCenter As Integer? = res(0).IdCostCenter
            Dim IdEntityBankAccount As Integer? = res(0).IdEntityBankAccount
            Dim IdVoucherTransaction As Integer = res(0).Id
            Dim IdMainAccount As Integer = res(0).IdMainAccount

            Dim varCashRegister = (From CR In _context.CashRegisters Where CR.Id = IdCashRegister Select New With {Key .Code = CR.Code, Key .Name = CR.Name}).FirstOrDefault()
            Dim varCostCenter = (From CC In _context.CostCenter Where CC.Id = IdCostCenter Select New With {Key .Code = CC.Code, Key .Name = CC.Name}).FirstOrDefault()
            Dim varEntityBankAccount = (From EA In _context.EntityBankAccounts
                                        Join B In _context.Bank On EA.IdBank Equals B.Id
                                        Where EA.Id = IdEntityBankAccount
                                        Select New With {Key .Code = EA.Code, Key .Name = B.Name, Key .Type = EA.Type}).FirstOrDefault()

            res(0).FullNameThird = If(IdThird IsNot Nothing, (From T In _context.ThirdParty Where T.Id = IdThird Select String.Concat(T.Nit, " - ", T.Name)).FirstOrDefault(), "")
            res(0).FullNameCashRegister = If(varCashRegister IsNot Nothing, String.Concat(varCashRegister.Code, " - ", varCashRegister.Name), String.Empty)
            res(0).FullNameCostCenter = If(varCostCenter IsNot Nothing, String.Concat(varCostCenter.Code, " - ", varCostCenter.Name), String.Empty)
            res(0).FullNameEntityBankAccount = If(varEntityBankAccount IsNot Nothing, String.Concat(varEntityBankAccount.Code, " - ", varEntityBankAccount.Name), String.Empty)
            res(0).EntityBankAccountsType = If(varEntityBankAccount IsNot Nothing, varEntityBankAccount.Type, Nothing)
            res(0).FullNameMainAccount = (From ma In _context.MainAccounts Where ma.Id = IdMainAccount Select String.Concat(ma.Number, " - ", ma.Name)).FirstOrDefault()
            res(0).OriginalValue = (From VTC As VoucherTransaction In Me._context.VoucherTransaction.AsNoTracking() Where VTC.Code.Equals(code.Trim()) Select VTC).SingleOrDefault()
            If res(0).HandlesDocumentSupport Then
                Dim IdVoucher = res(0).Id
                Dim EntityName = res(0).GetType().Name
                Dim supportDocument = (From esd In Me._context.ElectronicSupportDocument.Include("BillingAuthorization") Where esd.EntityId = IdVoucher And esd.EntityName = EntityName).FirstOrDefault()
                If supportDocument IsNot Nothing Then
                    res(0).AuthorizationResolutionId = supportDocument?.BillingAuthorization?.Id
                    res(0).AuthorizationResolutionName = supportDocument?.BillingAuthorization?.Name
                End If
            End If

            Dim companySettings = (From c In _context.CompanySettings.AsNoTracking().Include("Currency").AsNoTracking() Select c)?.FirstOrDefault

            If res(0)?.EntityBankAccounts IsNot Nothing Then
                If res(0)?.EntityBankAccounts?.CurrencyId Is Nothing Then
                    Dim _currency As Currency
                    _currency = companySettings?.Currency

                    res(0).EntityBankAccounts.CurrencyId = _currency?.Id
                    res(0).EntityBankAccounts.CurrencyAbbreviation = _currency?.Abbreviation
                Else
                    res(0).EntityBankAccounts.CurrencyAbbreviation = res(0)?.EntityBankAccounts?.Currency?.Abbreviation
                End If
            End If

            Dim ListVouDetail As List(Of VoucherTransactionDetails) = (From dtd In _context.VoucherTransactionDetails Where dtd.IdVoucherTransaction = IdVoucherTransaction Select dtd).ToList()
            Dim _cashFlowConcept As CashFlowConcept = Nothing
            If ListVouDetail IsNot Nothing AndAlso ListVouDetail.Count > 0 Then
                For Each vD As VoucherTransactionDetails In ListVouDetail
                    vD.FullNameMainAccount = (From ma In _context.MainAccounts Where ma.Id = vD.IdMainAccount Select String.Concat(ma.Number, " - ", ma.Name)).FirstOrDefault()

                    If vD.IdExpenseConcept IsNot Nothing Then
                        Dim expenseConcept As ExpenseConcepts = (From ec In _context.ExpenseConcepts Where ec.Id = vD.IdExpenseConcept Select ec).FirstOrDefault()
                        vD.ExpenseConceptCode = expenseConcept.Code
                        vD.ExpenseConceptName = expenseConcept.Description
                        vD.ExpenseConceptBehavior = expenseConcept.Behavior
                        If vD.Nature = 1 Then
                            vD.NatureName = ResourceManager.GetString("AccountNatureDebit")
                        Else
                            vD.NatureName = ResourceManager.GetString("AccountNatureCredit")
                        End If
                        _cashFlowConcept = (From cfc In _context.CashFlowConcept.AsNoTracking Where cfc.Id = vD.IdCashFlowConcept).FirstOrDefault
                        If _cashFlowConcept IsNot Nothing Then
                            vD.CodeNameCashFlowConcept = String.Format("{0} - {1}", _cashFlowConcept.Code, _cashFlowConcept.NameConcept)
                        End If
                    Else
                        vD.ExpenseConceptCode = String.Empty
                        vD.ExpenseConceptName = String.Empty
                        vD.NatureName = String.Empty
                    End If

                    If res(0).VoucherClass = 3 Then 'traslado
                        If res(0).ExpenseType = 1 Then 'Cuenta bancaria
                            Dim _varEntityBank = (From EA In _context.EntityBankAccounts
                                                  Join B In _context.Bank On EA.IdBank Equals B.Id
                                                  Where EA.Id = vD.IdEntityBankAccount
                                                  Select New With {Key .Code = EA.Code, Key .Name = B.Name}).FirstOrDefault()
                            vD.EntityName = _varEntityBank.Name
                            vD.EntityCode = _varEntityBank.Code
                        ElseIf res(0).ExpenseType = 3 Then 'Cajas
                            Dim _cash As CashRegisters = (From cr In _context.CashRegisters Where cr.Id = vD.CashRegisterId Select cr).FirstOrDefault()
                            vD.EntityName = _cash.Name
                            vD.EntityCode = _cash.Code
                        End If
                    ElseIf res(0).VoucherClass = 2 Then 'Reembolso
                        Dim _cash As CashRegisters = (From cr In _context.CashRegisters.Include("Currency").AsNoTracking() Where cr.Id = vD.CashRegisterId Select cr).FirstOrDefault()
                        vD.EntityName = _cash.Name
                        vD.EntityCode = _cash.Code
                        vD.CurrencyCashRegister = If(_cash.CurrencyId Is Nothing, companySettings?.Currency, _cash?.Currency)
                    End If

                    vD.FullNameThirdParty = (From tp In _context.ThirdParty Where tp.Id = vD.IdThirdParty Select String.Concat(tp.Nit, " - ", tp.Name)).FirstOrDefault()
                    vD.FullNameCostCenter = IIf(vD.IdCostCenter IsNot Nothing, (From cc In _context.CostCenter Where cc.Id = vD.IdCostCenter Select String.Concat(cc.Code, " - ", cc.Name)).FirstOrDefault(), String.Empty)

                    Dim treasuryAdvance = (From ta In _context.TreasuryAdvances Where ta.IdVoucherTransactionDetail = vD.Id Select ta).FirstOrDefault()
                    If treasuryAdvance IsNot Nothing AndAlso treasuryAdvance.Id > 0 Then
                        vD.AdvanceValue = treasuryAdvance.Value
                        vD.AdvanceDetail = treasuryAdvance.Detail
                        vD.ValueAdvanceInCurrencyHeader = treasuryAdvance.ValueInCurrencyHeader
                        vD.CurrencyAdvance = (From c In _context.Currency Where c.Id = treasuryAdvance.CurrencyId Select c).FirstOrDefault
                        vD.TRMValueAdvance = treasuryAdvance.TRMValue
                    End If

                    Dim _listAdvanceVoucher = (From va As VoucherTransactionAdvance In _context.VoucherTransactionAdvance Where va.IdVoucherTransactionD = vD.Id Select va).ToList()
                    For Each item As VoucherTransactionAdvance In _listAdvanceVoucher
                        vD.VoucherTransactionAdvance.Add(item)
                    Next

                    If vD.IdGeneralLedgerIVA.HasValue Then
                        Dim GeneralLedgerIVA As GeneralLedgerIVA = _generalLedgerIVA.GetGeneralLedgerIVAById(vD.IdGeneralLedgerIVA)
                        Dim MainAccountIVA As Domain.Payroll.Entities.MainAccounts = _mainAccounts.GetMainAccountById(GeneralLedgerIVA.IdAccountPurchaseService)

                        CostCenterDataCodeName = ""
                        If vD.IdCostCenter IsNot Nothing Then
                            Dim CostCenterData As Domain.Payroll.Entities.CostCenter = _CostCenter.GetCostCenterById(vD.IdCostCenter, False)
                            CostCenterDataCodeName = CostCenterData.Code + " - " + CostCenterData.Name
                        End If

                        Dim GeneralLedgerCodeName = MainAccountIVA.Number + " - " + GeneralLedgerIVA.Name

                        If vD.IdGeneralLedgerIVA.HasValue Then

                            If vD.discountableIVA Then

                                vD.VoucherTransactionDetailsAccountInfo.Add(New VoucherTransactionDetailAccountInfo With {
                                    .MainAccountCodeName = vD.FullNameMainAccount,
                                    .CostCenterCodeName = CostCenterDataCodeName,
                                    .NatureName = IIf(vD.ExpenseConcepts.Nature = 1, "Debito", "Credito"),
                                    .ValueTotalConcept = vD.Value
                                })

                                vD.VoucherTransactionDetailsAccountInfo.Add(New VoucherTransactionDetailAccountInfo With {
                                    .MainAccountCodeName = GeneralLedgerCodeName,
                                    .CostCenterCodeName = "",
                                    .NatureName = vD.NatureName,
                                    .ValueTotalConcept = vD.ValueIVA
                                })

                            ElseIf Not vD.discountableIVA AndAlso companySettings?.TaxRegistration = 1 Then

                                Dim acountDebit As Domain.Payroll.Entities.MainAccounts = _mainAccounts.GetMainAccountById(GeneralLedgerIVA.IdAccountDebitControlFiscal)
                                Dim creditDebit As Domain.Payroll.Entities.MainAccounts = _mainAccounts.GetMainAccountById(GeneralLedgerIVA.IdAccountCreditControlFiscal)

                                vD.VoucherTransactionDetailsAccountInfo.Add(New VoucherTransactionDetailAccountInfo With {
                                    .MainAccountCodeName = vD.FullNameMainAccount,
                                    .CostCenterCodeName = CostCenterDataCodeName,
                                    .NatureName = IIf(vD.ExpenseConcepts.Nature = 1, "Debito", "Credito"),
                                    .ValueTotalConcept = vD.Value + vD.ValueIVA
                                })

                                vD.VoucherTransactionDetailsAccountInfo.Add(New VoucherTransactionDetailAccountInfo With {
                                   .MainAccountCodeName = acountDebit.Number + " - " + acountDebit.Name,
                                   .CostCenterCodeName = "",
                                   .NatureName = ResourceManager.GetString("AccountNatureDebit"),
                                   .ValueTotalConcept = vD.ValueIVA
                                })

                                vD.VoucherTransactionDetailsAccountInfo.Add(New VoucherTransactionDetailAccountInfo With {
                                   .MainAccountCodeName = creditDebit.Number + " - " + creditDebit.Name,
                                   .CostCenterCodeName = "",
                                   .NatureName = ResourceManager.GetString("AccountNatureCredit"),
                                   .ValueTotalConcept = vD.ValueIVA
                                })
                            Else
                                vD.VoucherTransactionDetailsAccountInfo.Add(New VoucherTransactionDetailAccountInfo With {
                                                                                                                           .MainAccountCodeName = vD.FullNameMainAccount,
                                                                                                                           .CostCenterCodeName = CostCenterDataCodeName,
                                                                                                                           .NatureName = IIf(vD.ExpenseConcepts.Nature = 1, "Debito", "Credito"),
                                                                                                                           .ValueTotalConcept = vD.Value
                                                                                                                       })

                                vD.VoucherTransactionDetailsAccountInfo.Add(New VoucherTransactionDetailAccountInfo With {
                                                                                                                        .MainAccountCodeName = vD.FullNameMainAccount,
                                                                                                                        .CostCenterCodeName = "",
                                                                                                                        .NatureName = IIf(vD.ExpenseConcepts.Nature = 1, "Debito", "Credito"),
                                                                                                                        .ValueTotalConcept = vD.ValueIVA,
                                                                                                                        .Observation = $"IVA {GeneralLedgerIVA?.Code} % - {GeneralLedgerIVA?.Name}"
                                                                                                                    })
                            End If
                        End If
                    End If
                    If vD.SupplierBankAccountId IsNot Nothing Then
                        vD.SupplierBankAccount = (From sba In _context.SupplierBankAccount Where sba.Id = vD.SupplierBankAccountId Select sba).FirstOrDefault()
                    End If
                    res(0).VoucherTransactionDetails.Add(vD)
                Next
            End If

            Return res(0)
        Else
            Return New VoucherTransaction()
        End If
    End Function

    ''' <summary>
    ''' Obtiene un comprobante de egreso por id
    ''' </summary>
    ''' <param name="Id">The identifier.</param>
    ''' <returns></returns>
    ''' <exception cref="System.ArgumentNullException">Id</exception>
    Public Function GetVoucherTransactionById(Id As Integer, Optional tracking As Boolean = True) As VoucherTransaction Implements IVoucherTransactionRepository.GetVoucherTransactionById
        If Id = 0 Then
            Throw New ArgumentNullException("Id")
        End If
        Dim voucher As New VoucherTransaction
        Dim res As List(Of VoucherTransaction) = Nothing
        If tracking Then
            res = (From VTC As VoucherTransaction In Me._context.VoucherTransaction
                   Where VTC.Id = Id Select VTC).ToList()
        Else
            res = (From VTC As VoucherTransaction In Me._context.VoucherTransaction.AsNoTracking()
                   Where VTC.Id = Id Select VTC).ToList()
        End If


        '.Include("VoucherTransactionDetails").AsNoTracking()
        If res IsNot Nothing AndAlso res.Count > 0 Then

            Dim IdCashRegister As Integer? = res(0).IdCashRegister
            Dim IdThird As Integer? = res(0).IdThirdParty
            Dim IdCostCenter As Integer? = res(0).IdCostCenter
            Dim IdEntityBankAccount As Integer? = res(0).IdEntityBankAccount
            Dim IdVoucherTransaction As Integer = res(0).Id
            Dim IdMainAccount As Integer = res(0).IdMainAccount

            Dim varCashRegister = (From CR In _context.CashRegisters Where CR.Id = IdCashRegister Select New With {Key .Code = CR.Code, Key .Name = CR.Name}).FirstOrDefault()
            Dim varCostCenter = (From CC In _context.CostCenter Where CC.Id = IdCostCenter Select New With {Key .Code = CC.Code, Key .Name = CC.Name}).FirstOrDefault()
            Dim varEntityBankAccount = (From EA In _context.EntityBankAccounts
                                        Join B In _context.Bank On EA.IdBank Equals B.Id
                                        Where EA.Id = IdEntityBankAccount
                                        Select New With {Key .Code = EA.Code, Key .Name = B.Name}).FirstOrDefault()

            res(0).FullNameThird = If(IdThird IsNot Nothing, (From T In _context.ThirdParty Where T.Id = IdThird Select String.Concat(T.Nit, " - ", T.Name)).FirstOrDefault(), "")
            res(0).FullNameCashRegister = If(varCashRegister IsNot Nothing, String.Concat(varCashRegister.Code, " - ", varCashRegister.Name), String.Empty)
            res(0).FullNameCostCenter = If(varCostCenter IsNot Nothing, String.Concat(varCostCenter.Code, " - ", varCostCenter.Name), String.Empty)
            res(0).FullNameEntityBankAccount = If(varEntityBankAccount IsNot Nothing, String.Concat(varEntityBankAccount.Code, " - ", varEntityBankAccount.Name), String.Empty)
            res(0).FullNameMainAccount = (From ma In _context.MainAccounts Where ma.Id = IdMainAccount Select String.Concat(ma.Number, " - ", ma.Name)).FirstOrDefault()
            res(0).OriginalValue = (From VTC As VoucherTransaction In Me._context.VoucherTransaction.AsNoTracking() Where VTC.Id = Id Select VTC).SingleOrDefault()

            Dim ListVouDetail As List(Of VoucherTransactionDetails) = Nothing
            If tracking Then
                ListVouDetail = (From dtd In _context.VoucherTransactionDetails Where dtd.IdVoucherTransaction = IdVoucherTransaction Select dtd).ToList()
            Else
                ListVouDetail = (From dtd In _context.VoucherTransactionDetails.AsNoTracking() Where dtd.IdVoucherTransaction = IdVoucherTransaction Select dtd).ToList()
            End If
            If ListVouDetail IsNot Nothing AndAlso ListVouDetail.Count > 0 Then
                For Each vD As VoucherTransactionDetails In ListVouDetail
                    vD.FullNameMainAccount = (From ma In _context.MainAccounts Where ma.Id = vD.IdMainAccount Select String.Concat(ma.Number, " - ", ma.Name)).FirstOrDefault()
                    If vD.IdExpenseConcept IsNot Nothing Then
                        Dim expenseConcept As ExpenseConcepts = (From ec In _context.ExpenseConcepts Where ec.Id = vD.IdExpenseConcept Select ec).FirstOrDefault()
                        vD.ExpenseConceptCode = expenseConcept.Code
                        vD.ExpenseConceptName = expenseConcept.Description
                        vD.ExpenseConceptBehavior = expenseConcept.Behavior
                        'vD.Nature = expenseConcept.Nature
                        If vD.Nature = 1 Then
                            vD.NatureName = ResourceManager.GetString("AccountNatureDebit")
                        Else
                            vD.NatureName = ResourceManager.GetString("AccountNatureCredit")
                        End If
                    Else
                        vD.ExpenseConceptCode = String.Empty
                        vD.ExpenseConceptName = String.Empty
                        vD.NatureName = String.Empty
                    End If

                    If res(0).VoucherClass = 3 Then 'traslado
                        If res(0).ExpenseType = 1 Then 'Cuenta bancaria
                            Dim _varEntityBank = (From EA In _context.EntityBankAccounts
                                                  Join B In _context.Bank On EA.IdBank Equals B.Id
                                                  Where EA.Id = vD.IdEntityBankAccount
                                                  Select New With {Key .Code = EA.Code, Key .Name = B.Name}).FirstOrDefault()
                            vD.EntityName = _varEntityBank.Name
                            vD.EntityCode = _varEntityBank.Code
                        ElseIf res(0).ExpenseType = 3 Then 'Cajas
                            Dim _cash As CashRegisters = (From cr In _context.CashRegisters Where cr.Id = vD.CashRegisterId Select cr).FirstOrDefault()
                            vD.EntityName = _cash.Name
                            vD.EntityCode = _cash.Code
                        End If
                    ElseIf res(0).VoucherClass = 2 Then 'Reembolso
                        Dim _cash As CashRegisters = (From cr In _context.CashRegisters Where cr.Id = vD.CashRegisterId Select cr).FirstOrDefault()
                        vD.EntityName = _cash.Name
                        vD.EntityCode = _cash.Code
                    End If

                    vD.FullNameThirdParty = (From tp In _context.ThirdParty Where tp.Id = vD.IdThirdParty Select String.Concat(tp.Nit, " - ", tp.Name)).FirstOrDefault()
                    vD.FullNameCostCenter = IIf(vD.IdCostCenter IsNot Nothing, (From cc In _context.CostCenter Where cc.Id = vD.IdCostCenter Select String.Concat(cc.Code, " - ", cc.Name)).FirstOrDefault(), String.Empty)

                    Dim treasuryAdvance = (From ta In _context.TreasuryAdvances Where ta.IdVoucherTransactionDetail = vD.Id Select ta).FirstOrDefault()
                    If treasuryAdvance IsNot Nothing AndAlso treasuryAdvance.Id > 0 Then
                        vD.AdvanceValue = treasuryAdvance.Value
                        vD.AdvanceDetail = treasuryAdvance.Detail
                    End If
                    Dim _listAdvanceVoucher = (From va As VoucherTransactionAdvance In _context.VoucherTransactionAdvance Where va.IdVoucherTransactionD = vD.Id Select va).ToList()
                    For Each item As VoucherTransactionAdvance In _listAdvanceVoucher
                        'item.OriginalValue = (From va As VoucherTransactionAdvance In _context.VoucherTransactionAdvance Where va.Id = item.Id Select va).FirstOrDefault()
                        vD.VoucherTransactionAdvance.Add(item)
                    Next
                    res(0).VoucherTransactionDetails.Add(vD)
                Next
            End If
            Return res(0)
        Else
            Return New VoucherTransaction()
        End If
    End Function

    ''' <summary>
    ''' Obtiene un listado de comprobantes de egreso que esten dentro de un rango de fecha, tipo de egreso y que no esten reembolsados
    ''' </summary>
    ''' <param name="InitialDate"></param>
    ''' <param name="FinalDate"></param>
    ''' <param name="ExpenseType"></param>
    ''' <returns></returns>
    Public Function ListVoucherTransactionBetweenDateNotRefundExpenseType(CashRegisterId As Integer, InitialDate As Date, FinalDate As Date, ExpenseType As Byte) As List(Of VoucherTransaction) Implements IVoucherTransactionRepository.ListVoucherTransactionBetweenDateNotRefundExpenseType
        Dim voucher As New VoucherTransaction
        Dim Isrefund As Boolean = False
        Dim status As Byte = 2 ' estado confirmado
        Dim res = (From VTC As VoucherTransaction In Me._context.VoucherTransaction
                   Where VTC.DocumentDate >= InitialDate _
                   And VTC.DocumentDate <= FinalDate _
                   And VTC.RefundCashRegisterExpense = Isrefund _
                   And VTC.ExpenseType = ExpenseType _
                   And VTC.IdCashRegister = CashRegisterId
                   Select VTC).ToList()
        If res IsNot Nothing AndAlso res.Count > 0 Then
            For Each vt As VoucherTransaction In res
                vt.FullNameThird = (From T As ThirdParty In _context.ThirdParty Where T.Id = vt.IdThirdParty Select String.Concat(T.Nit, " - ", T.Name)).FirstOrDefault()
                vt.StatusName = ResourceManager.GetString("StatusName" + vt.Status.ToString, "Treasury")
                If vt.Status = 3 OrElse vt.Status = 4 Then 'Si el estado es anulado o reversado se iguala el valor a cero
                    vt.Value = 0
                End If
            Next
            Return res
        Else
            Return Nothing
        End If
    End Function

    ''' <summary>
    ''' Lists the type of the voucher transaction final date not refund expense.
    ''' </summary>
    ''' <param name="CashRegisterId">The cash register identifier.</param>
    ''' <param name="FinalDate">The final date.</param>
    ''' <param name="ExpenseType">Type of the expense.</param>
    ''' <returns></returns>
    Public Function ListVoucherTransactionFinalDateNotRefundExpenseType(CashRegisterId As Integer, FinalDate As Date, ExpenseType As Byte) As List(Of VoucherTransaction) Implements IVoucherTransactionRepository.ListVoucherTransactionFinalDateNotRefundExpenseType
        Dim voucher As New VoucherTransaction
        Dim Isrefund As Boolean = False
        Dim status As Byte = 2 ' estado confirmado
        Dim res = (From VTC As VoucherTransaction In Me._context.VoucherTransaction
                   Where VTC.DocumentDate <= FinalDate _
                   And VTC.RefundCashRegisterExpense = Isrefund _
                   And VTC.ExpenseType = ExpenseType _
                   And VTC.IdCashRegister = CashRegisterId
                   Select VTC).ToList()

        If res IsNot Nothing AndAlso res.Count > 0 Then
            For Each vt As VoucherTransaction In res
                vt.FullNameThird = (From T As ThirdParty In _context.ThirdParty Where T.Id = vt.IdThirdParty Select String.Concat(T.Nit, " - ", T.Name)).FirstOrDefault()
                vt.StatusName = ResourceManager.GetString("StatusName" + vt.Status.ToString, "Treasury")
                If vt.Status = 3 OrElse vt.Status = 4 Then 'Si el estado es anulado o reversado se iguala el valor a cero
                    vt.Value = 0
                End If
            Next
            Return res
        Else
            Return Nothing
        End If
    End Function

    ''' <summary>
    ''' obtiene un listado de comprobantes de egreso por el id del reembolso
    ''' </summary>
    ''' <param name="IdRefund">The identifier refund.</param>
    ''' <returns></returns>
    ''' <exception cref="System.ArgumentNullException">IdRefund</exception>
    Public Function ListVoucherTransactionByIdRefund(IdRefund As Integer) As List(Of VoucherTransaction) Implements IVoucherTransactionRepository.ListVoucherTransactionByIdRefund
        If IdRefund = 0 Then
            Throw New ArgumentNullException("IdRefund")
        End If
        Dim NotRefund As Boolean = False
        Dim res = (From VTC As VoucherTransaction In Me._context.VoucherTransaction
                   Where VTC.IdRefund = IdRefund And VTC.RefundCashRegisterExpense = NotRefund Select VTC).ToList()
        If res IsNot Nothing AndAlso res.Count > 0 Then
            For Each vt As VoucherTransaction In res
                vt.OriginalValue = (From VTC As VoucherTransaction In Me._context.VoucherTransaction Where VTC.IdRefund = IdRefund And VTC.RefundCashRegisterExpense = NotRefund Select VTC).FirstOrDefault()
                Dim IdVoucherTransaction As Integer = vt.Id
                Dim ListVouDetail As List(Of VoucherTransactionDetails) = (From dtd In _context.VoucherTransactionDetails Where dtd.IdVoucherTransaction = IdVoucherTransaction Select dtd).ToList()
                If ListVouDetail IsNot Nothing AndAlso ListVouDetail.Count > 0 Then
                    For Each vtd As VoucherTransactionDetails In ListVouDetail
                        vt.VoucherTransactionDetails.Add(vtd)
                    Next
                End If
            Next
            Return res
        Else
            Return Nothing
        End If
    End Function

    ''' <summary>
    ''' Obtiene un comprobante de egreso por codigo
    ''' </summary>
    Public Function GetVoucherTransactionByCheckNumber(ByVal checkNumber As Long) As VoucherTransaction Implements IVoucherTransactionRepository.GetVoucherTransactionByCheckNumber
        If checkNumber = 0 Then
            Throw New ArgumentNullException("checkNumber")
        End If
        Dim res = (From VTC As VoucherTransaction In Me._context.VoucherTransaction Where VTC.CheckNumber = checkNumber Select VTC).FirstOrDefault
        If res IsNot Nothing AndAlso res.Id > 0 Then
            res.OriginalValue = (From VTC As VoucherTransaction In Me._context.VoucherTransaction Where VTC.CheckNumber = checkNumber Select VTC).FirstOrDefault
            Return res
        Else
            Return New VoucherTransaction()
        End If

    End Function

    Public Function GetVoucherTransactionByAccountPayableListId(listAccountPayableId As List(Of Integer)) As List(Of String) Implements IVoucherTransactionRepository.GetVoucherTransactionByAccountPayableListId
        Dim Uno As Byte = 1
        Return (From vt In _context.VoucherTransaction.AsNoTracking()
                Join vtd In _context.VoucherTransactionDetails.AsNoTracking() On vtd.IdVoucherTransaction Equals vt.Id
                Join db In _context.DischargeBill.AsNoTracking() On db.IdVoucherTransactionD Equals vtd.Id
                Join ap In _context.AccountPayable On db.IdAccountPayable Equals ap.Id
                Where listAccountPayableId.Contains(db.IdAccountPayable) AndAlso vt.Status = Uno
                Select String.Concat(ap.BillNumber, ";", vt.Code)).ToList()
    End Function

    Public Function GetCheckNumber(entitybanckAccountId As Integer, OperatingUnitId As Integer, UserCode As String) As SP_GetCheckNumber_Result Implements IVoucherTransactionRepository.GetCheckNumber
        Return _context.SP_GetCheckNumber(entitybanckAccountId, OperatingUnitId, UserCode).FirstOrDefault()
    End Function


    Public Function GenerateVoucherTransactionSP(voucherTransactionXml As String, UserCode As String) As ObjectResult(Of SP_SaveVoucherTransaction_Result) Implements IVoucherTransactionRepository.GenerateVoucherTransactionSP
        DirectCast(_context, Entity.Infrastructure.IObjectContextAdapter).ObjectContext.CommandTimeout = 3600
        Return _context.SP_SaveVoucherTransaction(voucherTransactionXml, UserCode)
    End Function

    ''' <summary>
    ''' Obtiene un comprobante de egreso simple para poder realizar validaciones
    ''' </summary>
    ''' <param name="Id"></param>
    ''' <returns></returns>
    Public Function GetVoucherTransactionSimpleById(Id As Integer) As VoucherTransaction Implements IVoucherTransactionRepository.GetVoucherTransactionSimpleById
        If Id = 0 Then
            Throw New ArgumentNullException("Id")
        End If
        Return (From vt In Me._context.VoucherTransaction.AsNoTracking() Where vt.Id = Id Select vt).FirstOrDefault()
    End Function

    Public Function SP_ReverseVoucherTransaction(TreasuryNoteId As Integer, UserCode As String) As List(Of SP_ReverseVoucherTransaction_Result) Implements IVoucherTransactionRepository.SP_ReverseVoucherTransaction
        DirectCast(_context, IObjectContextAdapter).ObjectContext.CommandTimeout = 3600
        Return _context.SP_ReverseVoucherTransaction(TreasuryNoteId, UserCode).ToList()
    End Function

End Class
