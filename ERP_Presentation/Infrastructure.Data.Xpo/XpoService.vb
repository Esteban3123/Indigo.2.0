'***********************************************************************
' Assembly         : Infrastructure.Data.Xpo
' Author           : WalterSierra
' Created          : 27-03-2011
'
' Last Modified By : Juan F. Tamayo
' Last Modified On : 2013-02-28
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"

Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.Data.Xpo.SecurityRepository
Imports Infrastructure.Data.Xpo.GlosasRepository
Imports Infrastructure.Data.Xpo.PayrollRepository
Imports Infrastructure.Data.Xpo.CommonRepository
Imports Infrastructure.Data.Xpo.MaintenanceRepository
Imports Infrastructure.Data.Xpo.DocumentalSystemRepository
Imports Infrastructure.Data.Xpo.AuditRepository
Imports DevExpress.Xpo
Imports DevExpress.Data.Linq
Imports Infrastructure.Data.Xpo.AccountingRepository
Imports Infrastructure.Data.Xpo.TreasuryRepository
Imports Infrastructure.Data.Xpo.FixedAssetRepository
Imports Infrastructure.Data.Xpo.PortfolioRepository
Imports Infrastructure.Data.Xpo.BudgetRepository
Imports Infrastructure.Data.Xpo.PaymentsRepository
Imports Infrastructure.Data.Xpo.InventoryRepository
Imports DevExpress.Data.PLinq
Imports Infrastructure.Data.Xpo.ContractRepository
Imports Infrastructure.Data.Xpo.BillingRepository
Imports Infrastructure.Data.Xpo.CrystalRepository
Imports Infrastructure.Data.Xpo.InteropCostRepository
Imports System.Dynamic
Imports Infrastructure.Data.Xpo.MedicalFeesRepository
Imports Infrastructure.Data.Xpo.TaxesRepository

#End Region

''' <summary>
''' Clase Puente para los servicios de consultas XPO
''' instancia los repositorios por demanda
''' </summary>
<Obsolete("Ésta clase fue reemplazada por XpoServiceEx", True)>
Public NotInheritable Class XpoService

#Region "Treasury"

    ''' <summary>
    ''' Lista los conceptos 
    ''' </summary>
    ''' <param name="company">Empresa a la que se encuentra conectado</param>
    ''' <returns>Lista de tipos de documento</returns>
    Public Shared Function GetExpenseConceptById(ExpenseConceptId As Integer, ByVal company As String) As XPCollection
        Dim service As New TreasuryServiceXpo(company)
        Return service.GetExpenseConceptById(ExpenseConceptId)
    End Function

    ''' <summary>
    ''' lista todos los proveedores
    ''' </summary>
    ''' <param name="company">The company.</param>
    ''' <returns></returns>
    Public Shared Function ListSupplierReportTreasury(ByVal company As String) As XPInstantFeedbackSource
        Dim service As New TreasuryServiceXpo(company)
        Return service.ListSupplierReportTreasury()
    End Function

    Public Shared Function ListVoucherTransactionByPaymentMethod(company As String, paymentMethod As Byte) As XPInstantFeedbackSource
        Dim service As New TreasuryServiceXpo(company)
        Return service.ListVoucherTransactionByPaymentMethod(paymentMethod)
    End Function

    ''' <summary>
    ''' lista todos los comprobantes de egreso que se paguen con cheques por filtro
    ''' </summary>
    ''' <param name="company">The company.</param>
    ''' <returns></returns>
    Public Shared Function ListVoucherTranscationCheckReportTreasuryByFilter(ByVal company As String, ByVal filtro As String) As XPInstantFeedbackSource
        Dim service As New TreasuryServiceXpo(company)
        Return service.ListVoucherTranscationCheckReportTreasuryByFilter(filtro)
    End Function

    ''' <summary>
    ''' lista todas los reembolsos de tesoreria por filtro
    ''' </summary>
    ''' <param name="company">The company.</param>
    ''' <returns></returns>
    Public Shared Function ListRefundsReportTreasuryByFilter(ByVal company As String, ByVal filtro As String) As XPInstantFeedbackSource
        Dim service As New TreasuryServiceXpo(company)
        Return service.ListRefundsReportTreasuryByFilter(filtro)
    End Function

    ''' <summary>
    ''' lista todas las notas de tesoreria por filtro
    ''' </summary>
    ''' <param name="company">The company.</param>
    ''' <returns></returns>
    Public Shared Function ListNoteReportTreasuryByFilter(ByVal company As String, ByVal filtro As String) As XPInstantFeedbackSource
        Dim service As New TreasuryServiceXpo(company)
        Return service.ListNoteReportTreasuryByFilter(filtro)
    End Function

    ''' <summary>
    ''' lista todos las consignaciones y traslados de tesoreria por filtro
    ''' </summary>
    ''' <param name="company">The company.</param>
    ''' <returns></returns>
    Public Shared Function ListVReportConsignmentTransferTreasuryByFilter(ByVal company As String, ByVal filtro As String) As XPInstantFeedbackSource
        Dim service As New TreasuryServiceXpo(company)
        Return service.ListVReportConsignmentTransferTreasuryByFilter(filtro)
    End Function

    ''' <summary>
    ''' lista todos los anticipos de pago
    ''' </summary>
    ''' <param name="company">The company.</param>
    ''' <returns></returns>
    Public Shared Function ListPaymentsAdvancesReportTreasury(ByVal company As String, ByVal filtro As String) As XPInstantFeedbackSource
        Dim service As New TreasuryServiceXpo(company)
        Return service.ListPaymentsAdvancesReportTreasury(filtro)
    End Function

    ''' <summary>
    ''' lista todos las cuentas de cruce
    ''' </summary>
    ''' <param name="company">filtro</param>
    ''' <returns></returns>
    Public Shared Function ListCrossingAccountReportTreasuryFilter(ByVal company As String, ByVal filtro As String) As XPInstantFeedbackSource
        Dim service As New TreasuryServiceXpo(company)
        Return service.ListCrossingAccountReportTreasuryFilter(filtro)
    End Function

    ''' <summary>
    ''' lista todos los comprobantes de transacción
    ''' </summary>
    ''' <param name="company">filtro</param>
    ''' <returns></returns>
    Public Shared Function ListVoucherTranscationReportTreasuryFilter(ByVal company As String, ByVal filtro As String) As XPInstantFeedbackSource
        Dim service As New TreasuryServiceXpo(company)
        Return service.ListVoucherTranscationReportTreasuryFilter(filtro)
    End Function

    ''' <summary>
    ''' lista todas las cuentas bancarias
    ''' </summary>
    ''' <param name="company">The company.</param>
    ''' <returns></returns>
    Public Shared Function ListEntityBankReportTreasury(ByVal company As String) As XPInstantFeedbackSource
        Dim service As New TreasuryServiceXpo(company)
        Return service.ListEntityBankReportTreasury()
    End Function

    ''' <summary>
    ''' Lista todos los comprobantes de egreso que se pagaron con cheque
    ''' </summary>
    ''' <param name="company">The company.</param>
    ''' <returns></returns>
    Public Shared Function ListVoucherTranscationCheckReportTreasury(ByVal company As String) As XPInstantFeedbackSource
        Dim service As New TreasuryServiceXpo(company)
        Return service.ListVoucherTranscationCheckReportTreasury()
    End Function

    ''' <summary>
    ''' data source para listar todos los conceptos de los recibos de caja
    ''' </summary>
    ''' <param name="company">The company.</param>
    ''' <returns></returns>
    Public Shared Function ListReceiptConceptsReport(ByVal company As String) As XPInstantFeedbackSource
        Dim service As New TreasuryServiceXpo(company)
        Return service.ListReceiptConceptsReport()
    End Function

    ''' <summary>
    ''' data source para listar todos los conceptos de los comprobantes de egreso
    ''' </summary>
    ''' <param name="company">The company.</param>
    ''' <returns></returns>
    Public Shared Function ListExpensesConceptsReport(ByVal company As String) As XPInstantFeedbackSource
        Dim service As New TreasuryServiceXpo(company)
        Return service.ListExpensesConceptsReport()
    End Function

    ''' <summary>
    ''' data source para listar todos los cheques cancelados de tesoreria
    ''' </summary>
    ''' <param name="company">The company.</param>
    ''' <returns></returns>
    Public Shared Function ListCancellationChecksReportTreasury(ByVal company As String) As XPInstantFeedbackSource
        Dim service As New TreasuryServiceXpo(company)
        Return service.ListCancellationChecksReportTreasury()
    End Function

    ''' <summary>
    ''' Lista todos los reembolsos de tesoreria
    ''' </summary>
    ''' <param name="company">The company.</param>
    ''' <returns></returns>
    Public Shared Function ListRefundsReportTreasury(ByVal company As String) As XPInstantFeedbackSource
        Dim service As New TreasuryServiceXpo(company)
        Return service.ListRefundsReportTreasury()
    End Function

    ''' <summary>
    ''' Lista todas las consignaciones y transferencia de tesoreria
    ''' </summary>
    ''' <param name="company">The company.</param>
    ''' <returns></returns>
    Public Shared Function ListVReportConsignmentTransferTreasury(ByVal company As String) As XPInstantFeedbackSource
        Dim service As New TreasuryServiceXpo(company)
        Return service.ListVReportConsignmentTransferTreasury()
    End Function

    ''' <summary>
    ''' Lista todas las Notas debito y credito de tesoreria
    ''' </summary>
    ''' <param name="company">The company.</param>
    ''' <returns></returns>
    Public Shared Function ListNoteReportTreasury(ByVal company As String) As XPInstantFeedbackSource
        Dim service As New TreasuryServiceXpo(company)
        Return service.ListNoteReportTreasury()
    End Function

    ''' <summary>
    ''' Lista todos terceros
    ''' </summary>
    ''' <param name="company">The company.</param>
    ''' <returns></returns>
    Public Shared Function ListThirdPartyReportTreasury(ByVal company As String) As XPInstantFeedbackSource
        Dim service As New TreasuryServiceXpo(company)
        Return service.ListThirdPartyReportTreasury()
    End Function

    ''' <summary>
    ''' Lista todas las cuentas de cruce
    ''' </summary>
    ''' <param name="company">The company.</param>
    ''' <returns></returns>
    Public Shared Function ListCrossingAccountReportTreasury(ByVal company As String) As XPInstantFeedbackSource
        Dim service As New TreasuryServiceXpo(company)
        Return service.ListCrossingAccountReportTreasury()
    End Function

    ''' <summary>
    ''' Lista todos los comprobantes de transacción
    ''' </summary>
    ''' <param name="company">The company.</param>
    ''' <returns></returns>
    Public Shared Function ListVoucherTransactionReportTreasury(ByVal company As String) As XPInstantFeedbackSource
        Dim service As New TreasuryServiceXpo(company)
        Return service.ListVoucherTransactionReportTreasury()
    End Function

    '''' <summary>
    '''' SE RECOMIENDA USAR XPO SEGURIDAD
    '''' Lista todos los usuarios de creación
    '''' </summary>
    '''' <param name="company">The company.</param>
    '''' <returns></returns>
    'Public Shared Function ListCreationUsersReportTreasury(ByVal company As String) As XPInstantFeedbackSource
    '    Dim service As New TreasuryServiceXpo(company)
    '    Return service.ListCreationUsersReportTreasury()
    'End Function

    ''' <summary>
    ''' Lista todas las cuentas bancarias
    ''' </summary>
    ''' <param name="company">The company.</param>
    ''' <returns></returns>
    Public Shared Function ListCashReceiptReport(ByVal company As String, ByVal Filtro As String) As XPInstantFeedbackSource
        Dim service As New TreasuryServiceXpo(company)
        Return service.ListCashReceiptReport(Filtro)
    End Function

    ''' <summary>
    ''' Lista todas las cuentas bancarias
    ''' </summary>
    ''' <param name="company">The company.</param>
    ''' <returns></returns>
    Public Shared Function ListEntityBankAccountsReport(ByVal company As String) As XPInstantFeedbackSource
        Dim service As New TreasuryServiceXpo(company)
        Return service.ListEntityBankAccountsReport()
    End Function

    ''' <summary>
    ''' Lists the voucher transaction advance.
    ''' </summary>
    ''' <param name="company">The company.</param>
    ''' <returns></returns>
    Public Shared Function ListVoucherTransactionAdvance(ByVal company As String) As XPCollection
        Dim service As New TreasuryServiceXpo(company)
        Return service.ListVoucherTransactionAdvance()
    End Function

    Public Shared Function GetFirstCash(company As String) As XPCollection(Of CashRegisterXpo)
        Dim service As New TreasuryServiceXpo(company)
        Return service.GetFirstCash()
    End Function

    ''' <summary>
    ''' Lists the voucher transaction advance by voucher transaction detail identifier.
    ''' </summary>
    ''' <param name="company">The company.</param>
    ''' <param name="voucherTransactionDetailId">The voucher transaction detail identifier.</param>
    ''' <returns></returns>
    Public Shared Function ListVoucherTransactionAdvanceByVoucherTransactionDetailId(ByVal company As String, voucherTransactionDetailId As Integer) As XPCollection(Of VoucherTransactionAdvanceXpo)
        Dim service As New TreasuryServiceXpo(company)
        Return service.ListVoucherTransactionAdvanceByVoucherTransactionDetailId(voucherTransactionDetailId)
    End Function

    ''' <summary>
    ''' Lista los cambios de cheque
    ''' </summary>
    ''' <param name="company">The company.</param>
    ''' <returns></returns>
    Public Shared Function ListCheckCashing(ByVal company As String) As XPInstantFeedbackSource
        Dim service As New TreasuryServiceXpo(company)
        Return service.ListCheckCashing()
    End Function

    ''' <summary>
    ''' Lista todos las Cajas
    ''' </summary>
    ''' <param name="company">The company.</param>
    ''' <returns></returns>
    Public Shared Function ListCashRegistersEntity(ByVal company As String) As XPInstantFeedbackSource
        Dim service As New TreasuryServiceXpo(company)
        Return service.ListCashRegistersEntity()
    End Function

    ''' <summary>
    ''' Lists the expense concept endorsement.
    ''' </summary>
    ''' <param name="company">The company.</param>
    ''' <param name="Behavior">The behavior.</param>
    ''' <returns></returns>
    Public Shared Function ListExpenseConceptsByBehavior(ByVal company As String, Behavior As Integer) As XPInstantFeedbackSource
        Dim service As New TreasuryServiceXpo(company)
        Return service.ListExpenseConceptsByBehavior(Behavior)
    End Function

    ''' <summary>
    ''' Lista las notas de tesoreria
    ''' </summary>
    Public Shared Function ListTreasuryNote(ByVal company As String) As XPInstantFeedbackSource
        Dim service As New TreasuryServiceXpo(company)
        Return service.ListTreasuryNote()
    End Function

    ''' <summary>
    ''' Lists the consignment.
    ''' </summary>
    ''' <returns></returns>
    Public Shared Function ListConsignment(ByVal company As String) As XPInstantFeedbackSource
        Dim service As New TreasuryServiceXpo(company)
        Return service.ListConsignment()
    End Function

    ''' <summary>
    ''' Lista todos los cruces de cuentas
    ''' </summary>
    ''' <param name="company">The company.</param>
    ''' <returns></returns>
    Public Shared Function ListCrossingAccount(ByVal company As String) As XPInstantFeedbackSource
        Dim service As New TreasuryServiceXpo(company)
        Return service.ListCrossingAccount()
    End Function

    ''' <summary>
    ''' Lists the expense concept endorsement.
    ''' </summary>
    ''' <param name="company">The company.</param>
    ''' <param name="Behavior">The behavior.</param>
    ''' <returns></returns>
    Public Shared Function ListExpenseConceptsByBehaviorAndIdMainAccount(ByVal company As String, Behavior As Integer, IdMainAccount As Integer) As XPInstantFeedbackSource
        Dim service As New TreasuryServiceXpo(company)
        Return service.ListExpenseConceptsByBehaviorAndIdMainAccount(Behavior, IdMainAccount)
    End Function

    ''' <summary>
    ''' Lists the schedule payment confirm.
    ''' </summary>
    ''' <param name="company">The company.</param>
    ''' <returns></returns>
    Public Shared Function ListSchedulePaymentConfirm(ByVal company As String) As XPInstantFeedbackSource
        Dim service As New TreasuryServiceXpo(company)
        Return service.ListSchedulePaymentConfirm()
    End Function

    ''' <summary>
    ''' Lista todos los reembolsos
    ''' </summary>
    ''' <param name="company">The company.</param>
    ''' <returns></returns>
    Public Shared Function ListCashReceiptConceptsByRetentionType(ByVal company As String, retentionType As Integer, status As Boolean) As LinqInstantFeedbackSource
        Dim service As New TreasuryServiceXpo(company)
        Return service.ListCashReceiptConceptsByRetentionType(retentionType, status)
    End Function
    ''' <summary>
    ''' Lista todos los detalles de egresos por id del detalle del comprobante de egreso
    ''' </summary>
    ''' <returns></returns>
    Public Shared Function ListSchedulePayment(ByVal company As String) As XPInstantFeedbackSource
        Dim service As New TreasuryServiceXpo(company)
        Return service.ListSchedulePayment()
    End Function
    ''' <summary>
    ''' Lista todos los detalles de egresos por id del detalle del comprobante de egreso
    ''' </summary>
    ''' <param name="IdVoucherTransactionD">The identifier voucher transaction d.</param>
    ''' <returns></returns>
    Public Shared Function ListDischargeBillByIdVoucherTransactionDXpo(ByVal company As String, ByVal IdVoucherTransactionD As Integer) As XPCollection
        Dim service As New TreasuryServiceXpo(company)
        Return service.ListDischargeBillByIdVoucherTransactionDXpo(IdVoucherTransactionD)
    End Function
    ''' <summary>
    ''' Lists the expense concept endorsement.
    ''' </summary>
    ''' <param name="company">The company.</param>
    ''' <param name="Behavior">The behavior.</param>
    ''' <returns></returns>
    Public Shared Function ListExpenseConceptEndorsement(ByVal company As String, Behavior As Integer) As LinqInstantFeedbackSource
        Dim service As New TreasuryServiceXpo(company)
        Return service.ListExpenseConceptEndorsement(Behavior)
    End Function
    ''' <summary>
    ''' Lista todos los reembolsos
    ''' </summary>
    ''' <param name="company">The company.</param>
    ''' <returns></returns>
    Public Shared Function ListRefund(ByVal company As String) As XPInstantFeedbackSource
        Dim service As New TreasuryServiceXpo(company)
        Return service.ListRefund()
    End Function

    ''' <summary>
    ''' Lista todos los recibos de caja
    ''' </summary>
    ''' <param name="company">The company.</param>
    ''' <returns></returns>
    Public Shared Function ListCashReceipts(ByVal company As String) As LinqInstantFeedbackSource
        Dim service As New TreasuryServiceXpo(company)
        Return service.ListCashReceipts()
    End Function

    ''' <summary>
    ''' Lists the entity bank account by user.
    ''' </summary>
    Public Shared Function ListEntityBankAccountByUser(ByVal company As String, ByVal codUser As String, Optional ByVal status As Boolean = True) As LinqInstantFeedbackSource
        Dim service As New TreasuryServiceXpo(company)
        Return service.ListEntityBankAccountByUser(codUser, status)
    End Function

    ''' <summary>
    ''' Lista todas las tarjetas
    ''' </summary>
    ''' <param name="company">The company.</param>
    ''' <returns></returns>
    Public Shared Function ListCard(ByVal company As String, Optional ByVal status As Boolean = True) As XPInstantFeedbackSource
        Dim service As New TreasuryServiceXpo(company)
        Return service.ListCard(status)
    End Function

    ''' <summary>
    ''' Lista todos los conceptos de recibos de caja
    ''' </summary>
    ''' <param name="company">The company.</param>
    ''' <returns></returns>
    Public Shared Function ListCashReceiptConcept(ByVal company As String, Optional ByVal status As Boolean = True) As XPInstantFeedbackSource
        Dim service As New TreasuryServiceXpo(company)
        Return service.ListCashReceiptConcept(status)
    End Function

    Public Shared Function ListCashReceiptConceptCollection(ByVal company As String, Optional ByVal status As Boolean = True) As XPCollection(Of CashReceiptConceptXpo)
        Dim service As New TreasuryServiceXpo(company)
        Return service.ListCashReceiptConceptCollection(status)
    End Function


    ''' <summary>
    ''' Lists the cash receipt concept by affectation.
    ''' </summary>
    ''' <param name="company">The company.</param>
    ''' <param name="affectation">The affectation.</param>
    ''' <param name="status">if set to <c>true</c> [status].</param>
    ''' <returns></returns>
    Public Shared Function ListCashReceiptConceptByAffectation(ByVal company As String, affectation As Byte, Optional ByVal status As Boolean = True) As XPInstantFeedbackSource
        Dim service As New TreasuryServiceXpo(company)
        Return service.ListCashReceiptConceptByAffectation(status, affectation)
    End Function

    Public Shared Function ListCashReceiptConceptWithOutCostCenter(ByVal company As String, handlesCostCenter As Boolean, status As Boolean) As LinqInstantFeedbackSource
        Dim service As New TreasuryServiceXpo(company)
        Return service.ListCashReceiptConceptWithOutCostCenter(handlesCostCenter, status)
    End Function

    ''' <summary>
    ''' Lista todos los recibos de caja
    ''' </summary>
    ''' <param name="company">The company.</param>
    ''' <returns></returns>
    Public Shared Function ListCashRegister(ByVal company As String, Optional ByVal status As Boolean = True) As XPInstantFeedbackSource
        Dim service As New TreasuryServiceXpo(company)
        Return service.ListCashRegister(status)
    End Function

    ''' <summary>
    ''' Lists the type of the cash register by.
    ''' </summary>
    ''' <param name="Type">The type.</param>
    ''' <param name="company">The company.</param>
    ''' <returns></returns>
    Public Shared Function ListCashRegisterByType(ByVal Type As Integer, ByVal company As String) As XPInstantFeedbackSource
        Dim service As New TreasuryServiceXpo(company)
        Return service.ListCashRegisterByType(Type)
    End Function

    ''' <summary>
    ''' Lista todos los conceptos de egresos
    ''' </summary>
    ''' <param name="company">The company.</param>
    ''' <returns></returns>
    Public Shared Function ListExpenseConcept(ByVal company As String, Optional ByVal status As Boolean = True) As XPInstantFeedbackSource
        Dim service As New TreasuryServiceXpo(company)
        Return service.ListExpenseConcept(status)
    End Function

    ''' <summary>
    ''' Lista todos los conceptos de nota
    ''' </summary>
    ''' <param name="company">The company.</param>
    ''' <returns></returns>
    Public Shared Function ListNoteConcept(ByVal company As String, Optional ByVal status As Boolean = True) As XPInstantFeedbackSource
        Dim service As New TreasuryServiceXpo(company)
        Return service.ListNoteConcept(status)
    End Function

    ''' <summary>
    ''' Lista todas las cuentas bancarias de terceros
    ''' </summary>
    ''' <param name="company">The company.</param>
    ''' <returns></returns>
    Public Shared Function ListThridPartyBankAccount(ByVal company As String, Optional ByVal status As Boolean = True) As XPInstantFeedbackSource
        Dim service As New TreasuryServiceXpo(company)
        Return service.ListThridPartyBankAccount(status)
    End Function

    ''' <summary>
    ''' Lista todas las cuentas bancarias de entidades
    ''' </summary>
    ''' <param name="company">The company.</param>
    ''' <returns></returns>
    Public Shared Function ListEntityBankAccountByBank(ByVal company As String, Optional ByVal status As Boolean = True, Optional ByVal bankId As Integer = 0) As XPInstantFeedbackSource
        Dim service As New TreasuryServiceXpo(company)
        Return service.ListEntityBankAccountByBank(status, bankId)
    End Function

    ''' <summary>
    ''' Lista todas las cuentas bancarias de entidades
    ''' </summary>
    ''' <param name="company">The company.</param>
    ''' <returns></returns>
    Public Shared Function ListEntityBankAccount(ByVal company As String, Optional ByVal status As Boolean = True) As XPInstantFeedbackSource
        Dim service As New TreasuryServiceXpo(company)
        Return service.ListEntityBankAccount(status)
    End Function

    ''' <summary>
    ''' Lista todos los bancos
    ''' </summary>
    ''' <param name="company">The company.</param>
    ''' <returns></returns>
    Public Shared Function ListAllBank(ByVal company As String, Optional ByVal status As Boolean = True) As XPInstantFeedbackSource
        Dim service As New TreasuryServiceXpo(company)
        Return service.ListAllBank(status)
    End Function

    ''' <summary>
    ''' Lista todos los conceptos de pago
    ''' </summary>
    ''' <param name="company">The company.</param>
    ''' <returns></returns>
    Public Shared Function ListAllPaymentConcept(ByVal company As String, Optional ByVal state As Boolean = True) As LinqInstantFeedbackSource
        Dim service As New TreasuryServiceXpo(company)
        Return service.ListPaymentConcept()
    End Function

    ''' <summary>
    ''' Lista todos los conceptos de egreso por id de caja
    ''' </summary>
    ''' <param name="IdEntity">The identifier entity.</param>
    ''' <param name="company">The company.</param>
    ''' <returns></returns>
    Public Shared Function ListExpenseConceptCashRegisterByCash(ByVal IdEntity As Integer, ByVal company As String) As XPInstantFeedbackSource
        Dim service As New TreasuryServiceXpo(company)
        Return service.ListExpenseConceptCashRegisterByCash(IdEntity)
    End Function

    ''' <summary>
    ''' Lista los conceptos que tenga permitido una caja
    ''' </summary>
    ''' <param name="IdEntity">The identifier entity.</param>
    ''' <param name="company">The company.</param>
    ''' <returns></returns>
    Public Shared Function ListExpenseConceptByCash(ByVal IdEntity As String, ByVal company As String) As XPInstantFeedbackSource
        Dim service As New TreasuryServiceXpo(company)
        Return service.ListExpenseConceptByCash1(IdEntity)
    End Function

    ''' <summary>
    ''' Lists the expense concept by major cash.
    ''' </summary>
    ''' <param name="company">The company.</param>
    ''' <returns></returns>
    Public Shared Function ListExpenseConceptByMajorCash(ByVal company As String) As XPInstantFeedbackSource
        Dim service As New TreasuryServiceXpo(company)
        Return service.ListExpenseConceptByMajorCash()
    End Function

    ''' <summary>
    ''' Lista todos los conceptos distintos a la caja elegida
    ''' </summary>
    ''' <param name="IdEntity">The identifier entity.</param>
    ''' <param name="company">The company.</param>
    ''' <returns></returns>
    Public Shared Function ListExpenseConceptCashRegisterByNotCash(ByVal IdEntity As Integer, ByVal company As String) As XPInstantFeedbackSource
        Dim service As New TreasuryServiceXpo(company)
        Return service.ListExpenseConceptCashRegisterByNotCash(IdEntity)
    End Function

    ''' <summary>
    ''' Lista todos los cheques cancelados
    ''' </summary>
    ''' <param name="company">The company.</param>
    ''' <returns></returns>
    Public Shared Function ListAllCancellationCheck(ByVal company As String) As XPInstantFeedbackSource
        Dim service As New TreasuryServiceXpo(company)
        Return service.ListAllCancellationCheck()
    End Function

    ''' <summary>
    ''' Lista todos los conceptos de egreso que tengan comportamiento distinto a cajas
    ''' </summary>
    ''' <param name="company">The company.</param>
    ''' <returns></returns>
    Public Shared Function ListExpenseConceptByNotCash(ByVal company As String) As XPInstantFeedbackSource
        Dim service As New TreasuryServiceXpo(company)
        Return service.ListExpenseConceptByNotCash1()
    End Function

    ''' <summary>
    ''' lista las cajas que tiene asignadas el usuario
    ''' </summary>
    ''' <param name="company">The company.</param>
    ''' <returns></returns>
    Public Shared Function ListCashRegisterByUser(ByVal company As String, ByVal idUser As Integer, type As Integer, status As Boolean) As LinqInstantFeedbackSource
        Dim service As New TreasuryServiceXpo(company)
        Return service.ListCashRegisterByUser(idUser, type, status)
    End Function

    ''' <summary>
    ''' Lista todos los conceptos de egreso
    ''' </summary>
    ''' <param name="company">The company.</param>
    ''' <returns></returns>
    Public Shared Function ListAllVoucherTransaction(ByVal company As String) As XPInstantFeedbackSource
        Dim service As New TreasuryServiceXpo(company)
        Return service.ListAllVoucherTransaction()
    End Function

    ''' <summary>
    ''' Lista todos los conceptos de egreso que se pagan con cheque
    ''' </summary>
    ''' <returns></returns>
    Public Shared Function ListAllVoucherTransactionWithVoucherTypeCheck(ByVal company As String) As XPInstantFeedbackSource
        Dim service As New TreasuryServiceXpo(company)
        Return service.ListAllVoucherTransactionWithVoucherTypeCheck()
    End Function

    ''' <summary>
    ''' Lista todos los conceptos de egreso por estado
    ''' </summary>
    ''' <returns></returns>
    Public Shared Function ListAllVoucherTransactionByStatus(ByVal company As String, ByVal state As Byte) As XPInstantFeedbackSource
        Dim service As New TreasuryServiceXpo(company)
        Return service.ListAllVoucherTransactionByStatus(state)
    End Function

    ''' <summary>
    ''' Lists all cash receipt by status.
    ''' </summary>
    ''' <param name="company">The company.</param>
    ''' <param name="state">The state.</param>
    ''' <returns></returns>
    Public Shared Function ListAllCashReceiptByStatus(company As String, state As Integer) As XPInstantFeedbackSource
        Dim service As New TreasuryServiceXpo(company)
        Return service.ListAllCashReceiptByStatus(state)
    End Function
    ''' <summary>
    ''' lista los documentos de control por tipo de documento
    ''' </summary>
    ''' <param name="company"></param>
    ''' <param name="documentType"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function ListTreasuryControlByDocumentType(company As String, documentType As Integer) As XPCollection(Of TreasuryControlXpo)
        Dim service As New TreasuryServiceXpo(company)
        Return service.ListTreasuryControlByDocumentType(documentType)
    End Function



    ''' <summary>
    ''' Obtiene los datos para la programacion de pagos
    ''' </summary>
    ''' <param name="company">The company.</param>
    ''' <returns></returns>
    Public Shared Function GetSchedulePayment(ByVal company As String) As LinqInstantFeedbackSource
        Dim service As New TreasuryServiceXpo(company)
        Return service.GetSchedulePayment()
    End Function

#Region "Reports"

    ''' <summary>
    ''' Función para obtener una lista de datos de una entidad XPO
    ''' </summary>
    ''' <typeparam name="T">Objeto XPO</typeparam>
    ''' <param name="Company">Compañia Actual</param>
    ''' <param name="Fun">Objeto tipo Función opcional para filtrar los datos necesarios</param>
    ''' <returns>Objeto</returns>
    Public Shared Function ListCollectionTreasuryReport(Of T)(ByVal Company As String, Optional Fun As Func(Of T, Boolean) = Nothing, Optional criteria As String = Nothing) As Object
        Dim Treasury As New TreasuryServiceXpo(Company)
        Return Treasury.GetCollection(Of T)(Fun, criteria)
    End Function

#Region "Reporte diario de Caja"

    ''' <summary>
    ''' Función para obtener una lista de datos de una entidad XPO
    ''' </summary>
    ''' <param name="Company">Compañia Actual</param>
    ''' <returns>Objeto</returns>
    Public Shared Function ListCollectionReportCashJournal(ByVal Company As String, ByVal INDDateStart As Date, ByVal INDDateEnd As Date, ByVal INDStatus As Integer, ByVal INDPaymentType As Integer, ByVal INDCashStart As String, ByVal INDCashEnd As String) As List(Of TreasuryVReportCashBookXpo)
        Dim Treasury As New TreasuryServiceXpo(Company)
        Return Treasury.GetCollectionReportCashJournal(INDDateStart, INDDateEnd, INDStatus, INDPaymentType, INDCashStart, INDCashEnd)
    End Function

#End Region

#Region "Reporte Libro de Caja"

    ''' <summary>
    ''' Función para obtener una lista de datos de una entidad XPO
    ''' </summary>
    ''' <param name="Company">Compañia Actual</param>
    ''' <returns>Objeto</returns>
    Public Shared Function ListCollectionReportCashBook(ByVal Company As String, ByVal INDDateStart As Date, ByVal INDDateEnd As Date, ByVal INDStatus As String, ByVal INDPaymentType As Integer, ByVal INDCashStart As String, ByVal INDCashEnd As String, ByVal INDUserStart As String, ByVal INDUserEnd As String) As List(Of TreasuryVReportCashBookXpo)
        Dim Treasury As New TreasuryServiceXpo(Company)
        Return Treasury.GetCollectionReportCashBook(INDDateStart, INDDateEnd, INDStatus, INDPaymentType, INDCashStart, INDCashEnd, INDUserStart, INDUserEnd)
    End Function

#End Region

#Region "TreasuryNewsletterEntityBank"

    ''' <summary>
    ''' Función para obtener una lista de datos de una entidad XPO
    ''' </summary>
    ''' <param name="Company">Compañia Actual</param>
    ''' <returns>Objeto</returns>
    Public Shared Function ListCollectionTreasuryNewsletterEntityBank(ByVal Company As String, ByVal INDFechaIni As Date, ByVal INDFechaEnd As Date, ByVal INDStatus As String, ByVal INDCurrentAccountSavingsStart As String, ByVal INDCurrentAccountSavingsEnd As String, ByVal INDTypeReport As Integer, ByVal INDFormat As Integer, ByVal INDTypeVoucher As Integer) As List(Of TreasuryVReportTreasuryNewsletterEntityBankAccount)
        Dim Treasury As New TreasuryServiceXpo(Company)
        Return Treasury.GetCollectionTreasuryNewsletterEntityBank(INDFechaIni, INDFechaEnd, INDStatus, INDCurrentAccountSavingsStart, INDCurrentAccountSavingsEnd, INDTypeReport, INDFormat, INDTypeVoucher)
    End Function

#End Region

#Region "TreasuryNewsletterEntityCash"

    ''' <summary>
    ''' Función para obtener una lista de datos de una entidad XPO
    ''' </summary>
    ''' <param name="Company">Compañia Actual</param>
    ''' <returns>Objeto</returns>
    Public Shared Function ListCollectionTreasuryNewsletterEntityCash(ByVal Company As String, ByVal INDFechaIni As Date, ByVal INDFechaEnd As Date, ByVal INDStatus As String, ByVal INDCashStart As String, ByVal INDCashEnd As String, ByVal INDTypeReport As Integer, ByVal INDFormat As Integer, ByVal INDTypeVoucher As Integer) As List(Of TreasuryVReportTreasuryNewsletterCash)
        Dim Treasury As New TreasuryServiceXpo(Company)
        Return Treasury.GetCollectionTreasuryNewsletterEntityCash(INDFechaIni, INDFechaEnd, INDStatus, INDCashStart, INDCashEnd, INDTypeReport, INDFormat, INDTypeVoucher)
    End Function

#End Region

#Region "TreasuryNewsletterSummaryNewsletter"

    ''' <summary>
    ''' Función para obtener una lista de datos de una entidad XPO
    ''' </summary>
    ''' <param name="Company">Compañia Actual</param>
    ''' <returns>Objeto</returns>
    Public Shared Function ListCollectionSummaryNewsletter(ByVal Company As String, ByVal INDFechaIni As Date, ByVal INDFechaEnd As Date, ByVal INDStatus As String, ByVal INDCurrentAccountSavingsStart As String, ByVal INDCurrentAccountSavingsEnd As String, ByVal INDCashStart As String, ByVal INDCashEnd As String, ByVal INDTypeReport As Integer, ByVal INDFormat As Integer) As List(Of TreasuryVReportTreasuryNewsletterSummary)
        Dim Treasury As New TreasuryServiceXpo(Company)
        Return Treasury.ListCollectionSummaryNewsletter(INDFechaIni, INDFechaEnd, INDStatus, INDCurrentAccountSavingsStart, INDCurrentAccountSavingsEnd, INDCashStart, INDCashEnd, INDTypeReport, INDFormat)
    End Function

#End Region

#Region "ValuePreviosBalance Reporte Boletin de Tesoreria"

    ''' <summary>
    ''' Función para obtener una lista de datos de una entidad XPO
    ''' </summary>
    ''' <param name="Company">Compañia Actual</param>
    ''' <returns>Objeto</returns>
    Public Shared Function ValuePreviousBalance(ByVal Company As String, ByVal INDFechaIni As Date, ByVal INDFechaEnd As Date, ByVal INDCurrentAccountSavingsStart As String, ByVal INDCurrentAccountSavingsEnd As String, ByVal INDCashStart As String, ByVal INDCashEnd As String, ByVal INDPreviousBalanceType As Integer) As Long
        Dim Treasury As New TreasuryServiceXpo(Company)
        Return Treasury.ValuePreviousBalance(INDFechaIni, INDFechaEnd, INDCurrentAccountSavingsStart, INDCurrentAccountSavingsEnd, INDCashStart, INDCashEnd, INDPreviousBalanceType)
    End Function

#End Region

#Region "StatusEfecty"

    ''' <summary>
    ''' Función para obtener una lista de datos de una entidad XPO
    ''' </summary>
    ''' <param name="Company">Compañia Actual</param>
    ''' <returns>Objeto</returns>
    Public Shared Function ListReportStatusEfecty(ByVal Company As String, ByVal INDDateStart As Date, ByVal INDDateEnd As Date) As List(Of TreasuryTreasuryBalance)
        Dim Treasury As New TreasuryServiceXpo(Company)
        Return Treasury.ListCollectionReportStatusEfecty(INDDateStart, INDDateEnd)
    End Function

#End Region

#End Region

#End Region

#Region "Accounting"
    ''' <summary>
    ''' lista los detalles del comprobante contable
    ''' </summary>
    ''' <param name="company"></param>
    ''' <param name="journalVoucherId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function ListJournalVourcherHomologations(company As String, AccountingMovementId As Integer, journalVoucherId As Integer) As XPCollection(Of JournalVouchersXpo)
        Dim service As New AccountingServiceXpo(company)
        Return service.ListJournalVourcherHomologations(AccountingMovementId, journalVoucherId)
    End Function

    ''' <summary>
    ''' lista los coprobantes que no estan confirmados
    ''' </summary>
    ''' <param name="company"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function ListJournalVourcherMassiveConfirm(company As String) As XPCollection(Of JournalVouchersMassiveConfirmXpo)
        Dim service As New AccountingServiceXpo(company)
        Return service.ListJournalVourcherMassiveConfirm()
    End Function

    ''' <summary>
    ''' lista los detalles del comprobante contable
    ''' </summary>
    ''' <param name="company"></param>
    ''' <param name="journalVoucherId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function ListJournalVourcherDetailsByJournalVoucherId(company As String, journalVoucherId As Integer) As XPCollection(Of JournalVoucherDetailsXpo)
        Dim service As New AccountingServiceXpo(company)
        Return service.ListJournalVourcherDetailsByJournalVoucherId(journalVoucherId)
    End Function

    ''' <summary>
    ''' lista los detalles del comprobante contable
    ''' </summary>
    ''' <param name="company"></param>
    ''' <param name="journalVoucherId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function ListJournalVourcherDetailsImportedByJournalVoucherId(company As String, journalVoucherId As Integer) As XPInstantFeedbackSource
        Dim service As New AccountingServiceXpo(company)
        Return service.ListJournalVourcherDetailsImportedByJournalVoucherId(journalVoucherId)
    End Function

    ''' <summary>
    ''' lista todos los comprobantes por filtro
    ''' </summary>
    ''' <param name="company">criteria</param>
    ''' <returns></returns>
    Public Shared Function ListVoucherReportFilter(ByVal company As String, ByVal filtro As String) As XPInstantFeedbackSource
        Dim service As New AccountingServiceXpo(company)
        Return service.ListVoucherReportFilter(filtro)
    End Function

    ''' <summary>
    ''' Retornar La ultima fecha de cierre contable
    ''' </summary>
    ''' <param name="company">The company.</param>
    ''' <returns></returns>
    Public Shared Function GetLastClosingDate(ByVal company As String) As Date
        Dim service As New AccountingServiceXpo(company)
        Return service.GetLastClosingDate()
    End Function

    ''' <summary>
    ''' Lista todos los terceros
    ''' </summary>
    ''' <param name="company">The company.</param>
    ''' <returns></returns>
    Public Shared Function ListThirdPartyReport(ByVal company As String) As XPInstantFeedbackSource
        Dim service As New AccountingServiceXpo(company)
        Return service.ListThirdPartyReport()
    End Function

    ''' <summary>
    ''' Lista todos los centros de costos
    ''' </summary>
    ''' <param name="company">The company.</param>
    ''' <returns></returns>
    Public Shared Function ListCostcenterReport(ByVal company As String) As XPInstantFeedbackSource
        Dim service As New AccountingServiceXpo(company)
        Return service.ListCostcenterReport()
    End Function

    ''' <summary>
    ''' Lista todos las cuentas del nivel 5
    ''' </summary>
    ''' <param name="company">The company.</param>
    ''' <returns></returns>
    Public Shared Function ListMainAccountsByStatusAndBookId(ByVal company As String, ByVal status As Boolean, ByVal bookId As Integer) As XPInstantFeedbackSource
        Dim service As New AccountingServiceXpo(company)
        Return service.ListMainAccountsByStatusAndBookId(status, bookId)
    End Function

    ''' <summary>
    ''' Lista todos las cuentas del nivel 5
    ''' </summary>
    ''' <param name="company">The company.</param>
    ''' <returns></returns>
    Public Shared Function ListAccountsReport(ByVal company As String) As XPCollection(Of Infrastructure.Data.Xpo.AccountingRepository.MainAccountsXpo)
        Dim service As New AccountingServiceXpo(company)
        Return service.ListAccountsReport()
    End Function

    ''' <summary>
    ''' Lista todas las cuentas
    ''' </summary>
    ''' <param name="company">The company.</param>
    ''' <returns></returns>
    Public Shared Function ListAccountsForSearch(LegalBookId As Integer, ByVal company As String) As XPInstantFeedbackSource
        Dim service As New AccountingServiceXpo(company)
        Return service.ListAccountsForSearch(LegalBookId)
    End Function

    ''' <summary>
    ''' Lista todos los tipos de comprobante
    ''' </summary>
    ''' <param name="company">The company.</param>
    ''' <returns></returns>
    Public Shared Function ListTypeVoucherRepor(ByVal company As String) As XPInstantFeedbackSource
        Dim service As New AccountingServiceXpo(company)
        Return service.ListTypeVoucherRepor()
    End Function

    ''' <summary>
    ''' Lista todos los comprobantes
    ''' </summary>
    ''' <param name="company">The company.</param>
    ''' <returns></returns>
    Public Shared Function ListVoucherReport(ByVal company As String) As XPInstantFeedbackSource
        Dim service As New AccountingServiceXpo(company)
        Return service.ListVoucherReport()
    End Function

    ''' <summary>
    ''' Lista todo el plan unico de cuentas
    ''' </summary>
    ''' <param name="company">The company.</param>
    ''' <returns></returns>
    Public Shared Function ListAccountsByLevelReport(ByVal company As String) As XPInstantFeedbackSource
        Dim service As New AccountingServiceXpo(company)
        Return service.ListAccountsByLevelReport()
    End Function

    ''' <summary>
    ''' Lista todo el plan unico de cuentas por level y que manejen terceros
    ''' </summary>
    ''' <param name="company">The company.</param>
    ''' <returns></returns>
    Public Shared Function ListAccountsByLevelHandlesThirdReport(ByVal company As String) As XPInstantFeedbackSource
        Dim service As New AccountingServiceXpo(company)
        Return service.ListAccountsByLevelHandlesThirdReport()
    End Function

    ''' <summary>
    ''' Lista todo el plan unico de cuentas por level y de patrimonio
    ''' </summary>
    ''' <param name="company">The company.</param>
    ''' <returns></returns>
    Public Shared Function ListAccountsByLevelPatrimonial(ByVal company As String) As XPInstantFeedbackSource
        Dim service As New AccountingServiceXpo(company)
        Return service.ListAccountsByLevelPatrimonial()
    End Function
    ''' <summary>
    ''' lista las cuentas contables con o sin centro de costo
    ''' </summary>
    ''' <param name="company"></param>
    ''' <param name="handlesCostCenter"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function ListAccountsCostCenter(ByVal company As String, handlesCostCenter As Boolean) As XPInstantFeedbackSource
        Dim service As New AccountingServiceXpo(company)
        Return service.ListAccountsCostCenter(handlesCostCenter)
    End Function

    ''' <summary>
    ''' Lista todos los tipos de documento
    ''' </summary>
    ''' <param name="company">Empresa a la que se encuentra conectado</param>
    ''' <returns>Lista de tipos de documento</returns>
    Public Shared Function ListMainAccountsHandlesCostCenterAndRetention(ByVal company As String, level As Integer, retentionType As Integer, handlesCostCenter As Boolean, status As Boolean) As LinqInstantFeedbackSource
        Dim service As New AccountingServiceXpo(company)
        Return service.ListMainAccountsHandlesCostCenterAndRetention(level, retentionType, handlesCostCenter, status)
    End Function

    ''' <summary>
    ''' Lista todos los tipos de documento
    ''' </summary>
    ''' <param name="company">Empresa a la que se encuentra conectado</param>
    ''' <returns>Lista de tipos de documento</returns>
    Public Shared Function ListDocumentTypes(ByVal company As String, Optional ByVal state As Boolean = True) As XPInstantFeedbackSource
        Dim service As New AccountingServiceXpo(company)
        Return service.ListDocumentTypes(state)
    End Function

    ''' <summary>
    ''' Lista todos los tipos de documento
    ''' </summary>
    ''' <param name="company">Empresa a la que se encuentra conectado</param>
    ''' <returns>Lista de tipos de documento</returns>
    Public Shared Function ListBook(ByVal company As String) As XPInstantFeedbackSource
        Dim service As New AccountingServiceXpo(company)
        Return service.ListBook()
    End Function

    ''' <summary>
    ''' Lista todos los libros oficiales por estado
    ''' </summary>
    ''' <param name="company">Empresa a la que se encuentra conectado</param>
    ''' <returns>Lista de tipos de documento</returns>
    Public Shared Function ListBookByStatus(ByVal company As String, ByVal status As Boolean) As XPInstantFeedbackSource
        Dim service As New AccountingServiceXpo(company)
        Return service.ListBookByStatus(status)
    End Function

    ''' <summary>
    ''' Lista todos los libros oficiales por estado
    ''' </summary>
    ''' <param name="company">Empresa a la que se encuentra conectado</param>
    ''' <returns>Lista de tipos de documento</returns>
    Public Shared Function ListBookByStatusAndOfficialBook(ByVal company As String, ByVal status As Boolean, OfficialBook As Boolean) As XPInstantFeedbackSource
        Dim service As New AccountingServiceXpo(company)
        Return service.ListBookByStatusAndOfficialBook(status, OfficialBook)
    End Function

    ''' <summary>
    ''' Lista todos los libros oficiales por estado
    ''' </summary>
    ''' <param name="company">Empresa a la que se encuentra conectado</param>
    ''' <returns>Lista de tipos de documento</returns>
    Public Shared Function ListBookByStatusXpCollection(ByVal company As String, ByVal status As Boolean) As XPCollection
        Dim service As New AccountingServiceXpo(company)
        Return service.ListBookByStatusXpCollection(status)
    End Function

    ''' <summary>
    ''' Lista todos los libros oficiales por estado
    ''' </summary>
    ''' <param name="company">Empresa a la que se encuentra conectado</param>
    ''' <returns>Lista de tipos de documento</returns>
    Public Shared Function ListBookJournalVoucherHomologations(ByVal company As String, BookId As Integer) As XPCollection
        Dim service As New AccountingServiceXpo(company)
        Return service.ListBookJournalVoucherHomologations(BookId)
    End Function

    ''' <summary>
    ''' Lista todos los libros oficiales por estado
    ''' </summary>
    ''' <param name="company">Empresa a la que se encuentra conectado</param>
    ''' <returns>Lista de tipos de documento</returns>
    Public Shared Function ListVieBotByForm(Form As String, ByVal company As String) As XPCollection
        Dim service As New AccountingServiceXpo(company)
        Return service.ListVieBotByForm(Form)
    End Function

    ''' <summary>
    ''' Lista todos los tipos de documento
    ''' </summary>
    ''' <param name="company">Empresa a la que se encuentra conectado</param>
    ''' <returns>Lista de tipos de documento</returns>
    Public Shared Function ListBookByStatus(status As Boolean, ByVal company As String) As XPInstantFeedbackSource
        Dim service As New AccountingServiceXpo(company)
        Return service.ListBookByStatus(status)
    End Function

    ''' <summary>
    ''' Lista todos los tipos de documento
    ''' </summary>
    ''' <param name="company">Empresa a la que se encuentra conectado</param>
    ''' <returns>Lista de tipos de documento</returns>
    Public Shared Function ListAccountClass(ByVal company As String) As XPInstantFeedbackSource
        Dim service As New AccountingServiceXpo(company)
        Return service.ListAccountClass()
    End Function

    ''' <summary>
    ''' Lista todos los tipos de documento
    ''' </summary>
    ''' <param name="company">Empresa a la que se encuentra conectado</param>
    ''' <returns>Lista de tipos de documento</returns>
    Public Shared Function ListAccountLevel(ByVal company As String) As XPInstantFeedbackSource
        Dim service As New AccountingServiceXpo(company)
        Return service.ListAccountLevel()
    End Function

    ''' <summary>
    ''' Lista todos los tipos de documento
    ''' </summary>
    ''' <param name="company">Empresa a la que se encuentra conectado</param>
    ''' <returns>Lista de tipos de documento</returns>
    Public Shared Function ListRetentionConcept(ByVal company As String) As XPInstantFeedbackSource
        Dim service As New AccountingServiceXpo(company)
        Return service.ListRetentionConcept()
    End Function


    ''' <summary>
    ''' lista todos los conceptos de retencion por tipo de retentcion
    ''' </summary>
    ''' <param name="company">Empresa a la que se encuentra conectado</param>
    ''' <returns>Lista de tipos de documento</returns>
    Public Shared Function ListRetentionConceptByTypeRetention(ByVal company As String, retentionType As Integer, status As Boolean) As XPInstantFeedbackSource
        Dim service As New AccountingServiceXpo(company)
        Return service.ListRetentionConceptByTypeRetention(retentionType, status)
    End Function
    ''' <summary>
    ''' Lista todos los tipos de documento
    ''' </summary>
    ''' <param name="company">Empresa a la que se encuentra conectado</param>
    ''' <returns>Lista de tipos de documento</returns>
    Public Shared Function ListStatementFolio(ByVal company As String) As XPInstantFeedbackSource
        Dim service As New AccountingServiceXpo(company)
        Return service.ListStatementFolio()
    End Function

    ''' <summary>
    ''' Lista todas las participaciones patrimoniales
    ''' </summary>
    ''' <param name="company">The company.</param>
    ''' <returns></returns>
    Public Shared Function ListPatrimonialPart(ByVal company As String) As XPInstantFeedbackSource
        Dim service As New AccountingServiceXpo(company)
        Return service.ListPatrimonialPart()
    End Function

    ''' <summary>
    ''' Lista todas las cuentas contables por id del libro contable
    ''' </summary>
    ''' <param name="company">The company.</param>
    ''' <returns></returns>
    Public Shared Function ListAccountsByLegalBookId(ByVal company As String, legalBookId As Integer) As XPInstantFeedbackSource
        Dim service As New AccountingServiceXpo(company)
        Return service.ListAccountsByLegalBookId(legalBookId)
    End Function

    ''' <summary>
    ''' Lista todas las participaciones patrimoniales
    ''' </summary>
    ''' <param name="company">The company.</param>
    ''' <returns></returns>
    Public Shared Function ListAccounts(LegalBookId As Integer, ByVal company As String) As XPServerCollectionSource
        Dim service As New AccountingServiceXpo(company)
        Return service.ListAccounts(LegalBookId)
    End Function

    ''' <summary>
    ''' Lista todas las participaciones patrimoniales
    ''' </summary>
    ''' <param name="company">The company.</param>
    ''' <returns></returns>
    Public Shared Function ListAccountsByLegalBookAndAllowMovementAndStatus(LegalBookId As Integer, AllowMovement As Boolean, Status As Boolean, company As String) As XPCollection
        Dim service As New AccountingServiceXpo(company)
        Return service.ListAccountsByLegalBookAndAllowMovementAndStatus(LegalBookId, AllowMovement, Status)
    End Function

    ''' <summary>
    ''' Lista todas las participaciones patrimoniales
    ''' </summary>
    ''' <param name="company">The company.</param>
    ''' <returns></returns>
    Public Shared Function ListViewHomologationAccountByLegalBookId(LegalBookId As Integer, company As String) As XPCollection
        Dim service As New AccountingServiceXpo(company)
        Return service.ListViewHomologationAccountByLegalBookId(LegalBookId)
    End Function

    ''' <summary>
    ''' Lista todas las participaciones patrimoniales
    ''' </summary>
    ''' <param name="company">The company.</param>
    ''' <returns></returns>
    Public Shared Function ListViewHomologationAccountByLegalBookIdAndHomologationLegalBookId(LegalBookId As Integer, HomologationLegalBookId As Integer, company As String) As XPCollection
        Dim service As New AccountingServiceXpo(company)
        Return service.ListViewHomologationAccountByLegalBookIdAndHomologationLegalBookId(LegalBookId, HomologationLegalBookId)
    End Function

    ''' <summary>
    ''' Lista todas las participaciones patrimoniales
    ''' </summary>
    ''' <param name="company">The company.</param>
    ''' <returns></returns>
    Public Shared Function GetAccountById(Id As Integer, company As String) As XPCollection
        Dim service As New AccountingServiceXpo(company)
        Return service.GetAccountById(Id)
    End Function

    ''' <summary>
    ''' Lista todas las participaciones patrimoniales
    ''' </summary>
    ''' <param name="company">The company.</param>
    ''' <returns></returns>
    Public Shared Function ListAccountsByLegalBookAndAllowMovementAndStatusForSearch(LegalBookId As Integer, AllowMovement As Boolean, Status As Boolean, company As String) As XPInstantFeedbackSource
        Dim service As New AccountingServiceXpo(company)
        Return service.ListAccountsByLegalBookAndAllowMovementAndStatusForSearch(LegalBookId, AllowMovement, Status)
    End Function

    ''' <summary>
    ''' Lists the accounts by level.
    ''' </summary>
    ''' <param name="level">The level.</param>
    ''' <param name="company">The company.</param>
    ''' <returns></returns>
    Public Shared Function ListAccountsByLevel(ByVal level As Integer, ByVal Status As Boolean, ByVal company As String, Optional legalBookId As Integer = 0) As XPInstantFeedbackSource
        Dim service As New AccountingServiceXpo(company)
        Return service.ListAccountsByLevel(level, Status, legalBookId)
    End Function

    ''' <summary>
    ''' Lists the accounts by level.
    ''' </summary>
    ''' <param name="level">The level.</param>
    ''' <param name="company">The company.</param>
    ''' <returns></returns>
    Public Shared Function ListAccountsByRetentionAndFreelancerCategory(ByVal TypeRetention As Integer, ByVal FreelancerCategory As Boolean, ByVal Status As Boolean, ByVal company As String) As XPInstantFeedbackSource
        Dim service As New AccountingServiceXpo(company)
        Return service.ListAccountsByRetentionAndFreelancerCategory(TypeRetention, FreelancerCategory, Status)
    End Function

    ''' <summary>
    ''' Lists the accounts by retention.
    ''' </summary>
    ''' <param name="retention">if set to <c>true</c> [retention].</param>
    ''' <param name="Status">if set to <c>true</c> [status].</param>
    ''' <param name="company">The company.</param>
    ''' <returns></returns>
    Public Shared Function ListAccountsByRetention(ByVal retention As Boolean, ByVal Status As Boolean, ByVal company As String) As XPInstantFeedbackSource
        Dim service As New AccountingServiceXpo(company)
        Return service.ListAccountsByRetention(retention, Status)
    End Function

    ''' <summary>
    ''' Lists the state of the retention concept by.
    ''' </summary>
    ''' <param name="status">if set to <c>true</c> [status].</param>
    ''' <param name="company">The company.</param>
    ''' <returns></returns>
    Public Shared Function ListRetentionConceptByState(ByVal status As Boolean, ByVal company As String) As XPInstantFeedbackSource
        Dim service As New AccountingServiceXpo(company)
        Return service.ListRetentionConceptByState(status)
    End Function

    ''' <summary>
    ''' funcion para consultar los tipos de documento contables  por estado
    ''' </summary>
    ''' <param name="status">if set to <c>true</c> [status].</param>
    ''' <param name="Company">The company.</param>
    ''' <returns></returns>
    Public Shared Function ListJournalVoucherByState(ByVal status As Boolean, ByVal Company As String) As XPInstantFeedbackSource
        Dim service As New AccountingServiceXpo(Company)
        Return service.ListJournalVoucherByState(status)
    End Function

    ''' <summary>
    ''' funcion para consultar los tipos de documento contables  por estado
    ''' </summary>
    ''' <param name="status">if set to <c>true</c> [status].</param>
    ''' <param name="Company">The company.</param>
    ''' <returns></returns>
    Public Shared Function ListJournalVouchers(ByVal Company As String, legalBookId As Integer) As XPInstantFeedbackSource
        Dim service As New AccountingServiceXpo(Company)
        Return service.ListJournalVourchers(legalBookId)
    End Function

    ''' <summary>
    ''' funcion para consultar todos los meses
    ''' </summary>
    ''' <param name="Month"></param>
    ''' <param name="Year"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function ListAllMonth(ByVal Year As Integer, ByVal Company As String) As XPInstantFeedbackSource
        Dim service As New AccountingServiceXpo(Company)
        Return service.ListMonth(Year)
    End Function

#Region "Reports"

    ''' <summary>
    ''' Función para obtener una lista de datos de una entidad XPO
    ''' </summary>
    ''' <typeparam name="T">Objeto XPO</typeparam>
    ''' <param name="Company">Compañia Actual</param>
    ''' <param name="Fun">Objeto tipo Función opcional para filtrar los datos necesarios</param>
    ''' <returns>Objeto</returns>
    Public Shared Function ListCollectionAccountingReport(Of T)(ByVal Company As String, Optional Fun As Func(Of T, Boolean) = Nothing, Optional criteria As String = Nothing) As Object
        Dim Accounting As New AccountingServiceXpo(Company)
        Return Accounting.GetCollection(Of T)(Fun, criteria)
    End Function

#Region "Auxiliary"
    ''' <summary>
    ''' Función para obtener una lista de datos de una entidad XPO
    ''' </summary>
    ''' <param name="Company">Compañia Actual</param>
    ''' <returns>Objeto</returns>
    Public Shared Function ListCollectionAccountingThird(ByVal Company As String, ByVal INDStatus As Integer, ByVal INDFechaIni As Date, ByVal INDFechaEnd As Date, ByVal INDAccountIni As String, ByVal INDAccountEnd As String, ByVal INDThirdPartyStart As String, ByVal INDThirdPartyEnd As String, ByVal INDCostCenterStart As String, ByVal INDCostCenterEnd As String, ByVal INDBookId As Integer) As Object
        Dim Accounting As New AccountingServiceXpo(Company)
        Return Accounting.GetCollectionAccountThird(INDStatus, INDFechaIni, INDFechaEnd, INDAccountIni, INDAccountEnd, INDThirdPartyStart, INDThirdPartyEnd, INDCostCenterStart, INDCostCenterEnd, INDBookId)
    End Function

    ''' <summary>
    ''' Función para obtener una lista de datos de una entidad XPO
    ''' </summary>
    ''' <param name="Company">Compañia Actual</param>
    ''' <returns>Objeto</returns>
    Public Shared Function ListCollectionAccountingCostCenter(ByVal Company As String, ByVal INDStatus As Integer, ByVal INDFechaIni As Date, ByVal INDFechaEnd As Date, ByVal INDAccountIni As String, ByVal INDAccountEnd As String, ByVal INDThirdPartyStart As String, ByVal INDThirdPartyEnd As String, ByVal INDCostCenterStart As String, ByVal INDCostCenterEnd As String, ByVal INDBookId As Integer) As Object
        Dim Accounting As New AccountingServiceXpo(Company)
        Return Accounting.GetCollectionAccountCostCenter(INDStatus, INDFechaIni, INDFechaEnd, INDAccountIni, INDAccountEnd, INDThirdPartyStart, INDThirdPartyEnd, INDCostCenterStart, INDCostCenterEnd, INDBookId)
    End Function

    ''' <summary>
    ''' Función para obtener una lista de datos de una entidad XPO
    ''' </summary>
    ''' <param name="Company">Compañia Actual</param>
    ''' <returns>Objeto</returns>
    Public Shared Function ListCollectionThirdPartyCostCenter(ByVal Company As String, ByVal INDStatus As Integer, ByVal INDFechaIni As Date, ByVal INDFechaEnd As Date, ByVal INDAccountIni As String, ByVal INDAccountEnd As String, ByVal INDThirdPartyStart As String, ByVal INDThirdPartyEnd As String, ByVal INDCostCenterStart As String, ByVal INDCostCenterEnd As String, ByVal INDBookId As Integer) As Object
        Dim Accounting As New AccountingServiceXpo(Company)
        Return Accounting.GetCollectionThirdPartyCostCenter(INDStatus, INDFechaIni, INDFechaEnd, INDAccountIni, INDAccountEnd, INDThirdPartyStart, INDThirdPartyEnd, INDCostCenterStart, INDCostCenterEnd, INDBookId)
    End Function

    ''' <summary>
    ''' Función para obtener una lista de datos de una entidad XPO
    ''' </summary>
    ''' <param name="Company">Compañia Actual</param>
    ''' <returns>Objeto</returns>
    Public Shared Function ListCollectionAccountingDate(ByVal Company As String, ByVal INDStatus As Integer, ByVal INDFechaIni As Date, ByVal INDFechaEnd As Date, ByVal INDAccountIni As String, ByVal INDAccountEnd As String, ByVal INDThirdPartyStart As String, ByVal INDThirdPartyEnd As String, ByVal INDCostCenterStart As String, ByVal INDCostCenterEnd As String, ByVal INDBookId As Integer) As Object
        Dim Accounting As New AccountingServiceXpo(Company)
        Return Accounting.GetCollectionAccountingDate(INDStatus, INDFechaIni, INDFechaEnd, INDAccountIni, INDAccountEnd, INDThirdPartyStart, INDThirdPartyEnd, INDCostCenterStart, INDCostCenterEnd, INDBookId)
    End Function
#End Region

#Region "TrialBalance"

    ''' <summary>
    ''' Función para obtener una lista de datos de una entidad XPO
    ''' </summary>
    ''' <param name="Company">Compañia Actual</param>
    ''' <returns>Objeto</returns>
    Public Shared Function ListCollectionTrialBalance(ByVal Company As String, ByVal INDFechaIni As Date, ByVal INDFechaEnd As Date, ByVal INDAccountIni As String, ByVal INDAccountEnd As String, ByVal INDThirdPartyStart As String, ByVal INDThirdPartyEnd As String, ByVal INDCostCenterStart As String, ByVal INDCostCenterEnd As String, ByVal INDAccountingZero As Integer, ByVal INDDetailingThirdParty As Boolean, ByVal INDDetailingCostCenter As Boolean, ByVal INDBookId As Integer) As Object
        Dim Accounting As New AccountingServiceXpo(Company)
        Return Accounting.GetCollectionTrialBalance(INDFechaIni, INDFechaEnd, INDAccountIni, INDAccountEnd, INDThirdPartyStart, INDThirdPartyEnd, INDCostCenterStart, INDCostCenterEnd, INDAccountingZero, INDDetailingThirdParty, INDDetailingCostCenter, INDBookId)
    End Function

#End Region

#Region "GeneralBalanceDebit"

    ''' <summary>
    ''' Función para obtener una lista de datos de una entidad XPO
    ''' </summary>
    ''' <param name="Company">Compañia Actual</param>
    ''' <returns>Objeto</returns>
    Public Shared Function ListCollectionBalanceGeneral(ByVal Company As String, ByVal INDDateStart As Date, ByVal INDDateEnd As Date, ByVal INDCostCenterStart As String, ByVal INDCostCenterEnd As String, ByVal INDNatureAccount As String, ByVal INDMemorandumAccounts As String, ByVal INDAccountingZero As Integer, ByVal INDBookId As Integer) As Object
        Dim Accounting As New AccountingServiceXpo(Company)
        Return Accounting.GetCollectionGeneralBalance(INDDateStart, INDDateEnd, INDCostCenterStart, INDCostCenterEnd, INDNatureAccount, INDMemorandumAccounts, INDAccountingZero, INDBookId)
    End Function

#End Region

#Region "Result Status"

    ''' <summary>
    ''' Función para obtener una lista de datos de una entidad XPO
    ''' </summary>
    ''' <param name="Company">Compañia Actual</param>
    ''' <returns>Objeto</returns>
    Public Shared Function ListCollectionResultStatus(ByVal Company As String, ByVal INDDateStart As Date, ByVal INDDateEnd As Date, ByVal INDCostCenterStart As String, ByVal INDCostCenterEnd As String, ByVal INDNatureAccount As String, ByVal INDAccountsCostCenter As Integer, ByVal INDAccountingZero As Integer, ByVal INDVisualization As Integer, ByVal INDBookId As Integer) As Object
        Dim Accounting As New AccountingServiceXpo(Company)
        Return Accounting.GetCollectionResultStatus(INDDateStart, INDDateEnd, INDCostCenterStart, INDCostCenterEnd, INDNatureAccount, INDAccountsCostCenter, INDAccountingZero, INDVisualization, INDBookId)
    End Function

#End Region

#Region "ThirdBalance"
    ''' <summary>
    ''' Función para obtener una lista de datos de una entidad XPO
    ''' </summary>
    ''' <param name="Company">Compañia Actual</param>
    ''' <returns>Objeto</returns>
    Public Shared Function ListCollectionThirdBalance(ByVal Company As String, ByVal INDperiod As Date, ByVal INDAccountStart As String, ByVal INDAccountEnd As String, ByVal INDThirdPartyStart As String, INDThirdPartyEnd As String, ByVal INDBookId As Integer) As Object
        Dim Accounting As New AccountingServiceXpo(Company)
        Return Accounting.GetCollectionThirdBalance(INDperiod, INDAccountStart, INDAccountEnd, INDThirdPartyStart, INDThirdPartyEnd, INDBookId)
    End Function
#End Region

#Region "ThirdBalance"
    ''' <summary>
    ''' Función para obtener una lista de datos de una entidad XPO
    ''' </summary>
    ''' <param name="Company">Compañia Actual</param>
    ''' <returns>Objeto</returns>
    Public Shared Function ListCollectionPatrimonialChanges(ByVal Company As String, ByVal INDAccountStart As String, ByVal INDAccountEnd As String, ByVal INDLastClosingDate As Date) As Object
        Dim Accounting As New AccountingServiceXpo(Company)
        Return Accounting.GetCollectionPatrimonialChanges(INDAccountStart, INDAccountEnd, INDLastClosingDate)
    End Function
#End Region

#Region "LedgerAndBalance"
    ''' <summary>
    ''' Función para obtener una lista de datos de una entidad XPO
    ''' </summary>
    ''' <param name="Company">Compañia Actual</param>
    ''' <returns>Objeto</returns>
    Public Shared Function ListCollectionLedgerAndBalance(ByVal Company As String, ByVal INDPeriod As Date, ByVal INDAccountZero As Boolean, ByVal INDBookId As Integer) As Object
        Dim Accounting As New AccountingServiceXpo(Company)
        Return Accounting.GetCollectionLedgerAndBalance(INDPeriod, INDAccountZero, INDBookId)
    End Function
#End Region

#Region "InventoryAndBalance"
    ''' <summary>
    ''' Función para obtener una lista de datos de una entidad XPO
    ''' </summary>
    ''' <param name="Company">Compañia Actual</param>
    ''' <returns>Objeto</returns>
    Public Shared Function ListCollectionInventoryAndBalance(ByVal Company As String, ByVal INDPeriod As Date, ByVal INDAccountZero As Boolean, ByVal INDBookId As Integer) As Object
        Dim Accounting As New AccountingServiceXpo(Company)
        Return Accounting.GetCollectionInventoryAndBalance(INDPeriod, INDAccountZero, INDBookId)
    End Function
#End Region

#End Region

#End Region

#Region "Budget"

    ''' <summary>
    ''' lista los ingresos de presupuesto
    ''' </summary>
    ''' <param name="filtro"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function ListBudgetIncome(Company As String, filtro As String) As XPCollection(Of VReportBudgetIncomeXpo)
        Dim service As New BudgetServicesXpo(Company)
        Return service.ListBudgetIncome(filtro)
    End Function

    ''' <summary>
    ''' lista los ingresos de presupuesto
    ''' </summary>
    ''' <param name="filtro"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function ListBudgetExpense(Company As String, filtro As String) As XPCollection(Of VReportBudgetExpenseXpo)
        Dim service As New BudgetServicesXpo(Company)
        Return service.ListBudgetExpense(filtro)
    End Function

    ''' <summary>
    ''' lista los documentos de presupuesto que no estan confirmados de ingresos
    ''' </summary>
    ''' <param name="documentType"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function ListBudgetDocumentsNotConfirmed(company As String, documentType As EBudgetDocumentType, Optional validityId As Integer = 0) As XPCollection
        Dim service As New BudgetServicesXpo(company)
        Return service.ListBudgetDocumentsNotConfirmed(documentType)
    End Function

    ''' <summary>
    ''' lista los documentos de presupuesto que no estan confirmados de gastos
    ''' </summary>
    ''' <param name="documentType"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function ListBudgetDocumentsNotConfirmedExpenses(company As String, documentType As EBudgetDocumentTypeExpense, Optional validityId As Integer = 0) As XPCollection
        Dim service As New BudgetServicesXpo(company)
        Return service.ListBudgetDocumentsNotConfirmedExpenses(documentType)
    End Function

    ''' <summary>
    ''' lista las modificaciones del PAC
    ''' </summary>
    Public Shared Function ListPACModification(ByVal company As String, Validity As Integer, type As Integer) As XPInstantFeedbackSource
        Dim service As New BudgetServicesXpo(company)
        Return service.ListPACModification(Validity, type)
    End Function

    ''' <summary>
    ''' Lista las obligaciones confirmadas
    ''' </summary>
    Public Shared Function ListObligationByStatusConfirmedAndValidityId(ValidityId As Integer, Status As Integer, ByVal company As String) As XPInstantFeedbackSource
        Dim service As New BudgetServicesXpo(company)
        Return service.ListObligationByStatusConfirmedAndValidityId(ValidityId, Status)
    End Function

    ''' <summary>
    ''' lista los recaudos
    ''' </summary>
    Public Shared Function ListCollection(ByVal company As String, Validity As Integer) As XPInstantFeedbackSource
        Dim service As New BudgetServicesXpo(company)
        Return service.ListCollection(Validity)
    End Function

    ''' <summary>
    ''' lista las obligaciones
    ''' </summary>
    Public Shared Function ListObligation(ByVal company As String, Validity As Integer) As XPInstantFeedbackSource
        Dim service As New BudgetServicesXpo(company)
        Return service.ListObligation(Validity)
    End Function

    ''' <summary>
    ''' lista los recaudos por estado
    ''' </summary>
    Public Shared Function ListCollectionByStatus(ByVal company As String, Validity As Integer, status As Integer) As XPInstantFeedbackSource
        Dim service As New BudgetServicesXpo(company)
        Return service.ListCollectionByStatus(Validity, status)
    End Function

    ''' <summary>
    ''' lista las modificaciones de recaudos
    ''' </summary>
    ''' <param name="company"></param>
    ''' <param name="validaty"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function ListCollectionModification(company As String, validaty As Integer) As XPInstantFeedbackSource
        Dim service As New BudgetServicesXpo(company)
        Return service.ListCollectionModification(validaty)
    End Function

    ''' <summary>
    ''' Lists Financial Source by Entity and Validity.
    ''' </summary>
    ''' <param name="entity">The level.</param>
    ''' <param name="validity">The company.</param>
    ''' <returns></returns>
    Public Shared Function ListFinancialSource(ByVal Entity As Integer, ByVal Validity As Integer, ByVal company As String) As XPInstantFeedbackSource
        Dim service As New BudgetServicesXpo(company)
        Return service.ListFinancialSource(Entity, Validity)
    End Function

    ''' <summary>
    ''' Lists Financial Source by Entity and Validity.
    ''' </summary>
    ''' <param name="entity">The level.</param>
    ''' <param name="validity">The company.</param>
    ''' <returns></returns>
    Public Shared Function ListEarningsType(ByVal Entity As Integer, ByVal Validity As Integer, ByVal company As String) As XPInstantFeedbackSource
        Dim service As New BudgetServicesXpo(company)
        Return service.ListEarningsType(Entity, Validity)
    End Function

    ''' <summary>
    ''' Lists Financial Source by Entity and Validity.
    ''' </summary>
    ''' <param name="entity">The level.</param>
    ''' <param name="validity">The company.</param>
    ''' <returns></returns>
    Public Shared Function ListExpenseType(ByVal Entity As Integer, ByVal Validity As Integer, ByVal company As String) As XPInstantFeedbackSource
        Dim service As New BudgetServicesXpo(company)
        Return service.ListExpenseType(Entity, Validity)
    End Function

    ''' <summary>
    ''' Lists Financial Source by Entity and Validity.
    ''' </summary>
    ''' <param name="entity">The level.</param>
    ''' <param name="validity">The company.</param>
    ''' <returns></returns>
    Public Shared Function ListBudgetDependency(ByVal Entity As Integer, ByVal Validity As Integer, ByVal company As String) As XPInstantFeedbackSource
        Dim service As New BudgetServicesXpo(company)
        Return service.ListBudgetDependency(Entity, Validity)
    End Function

    ''' <summary>
    ''' Lists Financial Source by Entity and Validity.
    ''' </summary>
    ''' <returns></returns>
    Public Shared Function ListBudgetTransferByType(type As Integer, budgetValidityId As Integer, ByVal company As String) As XPInstantFeedbackSource
        Dim service As New BudgetServicesXpo(company)
        Return service.ListBudgetTransferByType(type, budgetValidityId)
    End Function

    ''' <summary>
    ''' Lists Financial Source by Entity and Validity.
    ''' </summary>
    ''' <returns></returns>
    Public Shared Function ListObligationModificationByValidityId(budgetValidityId As Integer, ByVal company As String) As XPInstantFeedbackSource
        Dim service As New BudgetServicesXpo(company)
        Return service.ListObligationModificationByValidityId(budgetValidityId)
    End Function

    ''' <summary>
    ''' Lists  Recognition by Validity.
    ''' </summary>
    ''' <returns></returns>
    Public Shared Function ListRecognition(budgetValidityId As Integer, ByVal company As String) As XPInstantFeedbackSource
        Dim service As New BudgetServicesXpo(company)
        Return service.ListRecognition(budgetValidityId)
    End Function

    ''' <summary>
    ''' Lists  Availability by Validity.
    ''' </summary>
    ''' <returns></returns>
    Public Shared Function ListAvailability(budgetValidityId As Integer, ByVal company As String) As XPInstantFeedbackSource
        Dim service As New BudgetServicesXpo(company)
        Return service.ListAvailability(budgetValidityId)
    End Function

    ''' <summary>
    ''' Lists Financial Source by Entity and Validity.
    ''' </summary>
    ''' <param name="entity">The level.</param>
    ''' <param name="validity">The company.</param>
    ''' <returns></returns>
    Public Shared Function ListBudgetConcept(ByVal Entity As Integer, ByVal Validity As Integer, ByVal company As String) As XPInstantFeedbackSource
        Dim service As New BudgetServicesXpo(company)
        Return service.ListBudgetConcept(Entity, Validity)
    End Function

    ''' <summary>
    ''' Gets all portfolio SuretyFile.
    ''' </summary>
    ''' <param name="Company">The company.</param>
    ''' <returns></returns>
    Public Shared Function GetAllBudgetEntities(Company As String, filtro As String) As XPCollection(Of BudgetBudgetaryValidityReportXpo)
        Dim service As New BudgetServicesXpo(Company)
        Return service.GetAllBudgetEntities(filtro)
    End Function

    ''' <summary>
    ''' Gets all portfolio SuretyFile.
    ''' </summary>
    ''' <param name="Company">The company.</param>
    ''' <returns></returns>
    Public Shared Function GetAllBudgetRevenueType(Company As String, filtro As String) As XPCollection(Of BudgetRevenueTypeReportXpo)
        Dim service As New BudgetServicesXpo(Company)
        Return service.GetAllBudgetRevenueType(filtro)
    End Function

    ''' <summary>
    ''' Lista todos las fuentes de financiacion
    ''' </summary>
    ''' <param name="company">Empresa a la que se encuentra conectado</param>
    ''' <returns>Lista dde fuentes de financiacion</returns>
    Public Shared Function ListFinancialSource(ByVal company As String, Optional ValidityId As String = "") As XPInstantFeedbackSource
        Dim service As New BudgetServicesXpo(company)
        Return service.GetFinancialSource(ValidityId)
    End Function

    ''' <summary>
    ''' Lista todos las entidades presupuestales server collection
    ''' </summary>
    ''' <param name="company">Empresa a la que se encuentra conectado</param>
    ''' <returns>Lista de entidades presupuestales</returns>
    Public Shared Function ListBudgetEntitiesXPSCS(ByVal company As String) As LinqInstantFeedbackSource
        Dim service As New BudgetServicesXpo(company)
        Return service.ListBudgetEntity()
    End Function

    ''' <summary>
    ''' Lista todos las entidades presupuestales xpinstantfeedbacksource
    ''' </summary>
    ''' <param name="company">Empresa a la que se encuentra conectado</param>
    ''' <returns>Lista de entidades presupuestales</returns>
    Public Shared Function ListBudgetEntitiesXPIFS(ByVal company As String) As XPInstantFeedbackSource
        Dim service As New BudgetServicesXpo(company)
        Return service.GetBudgetEntitiesXPIFS()
    End Function

    ''' <summary>
    ''' Lista los conceptos de pagos por estado y si maneja retencion
    ''' </summary>
    ''' <param name="company">Empresa a la que se encuentra conectado</param>
    ''' <returns>Lista de tipos de documento</returns>
    Public Shared Function ListValidityByBudgetEntityId(ByVal company As String, ByVal BudgetaryEntityId As Integer) As XPCollection
        Dim service As New BudgetServicesXpo(company)
        Return service.ListValidityByBudgetEntityId(BudgetaryEntityId)
    End Function

    ''' <summary>
    ''' Lista los detalles de la obligacion
    ''' </summary>
    ''' <param name="company">Empresa a la que se encuentra conectado</param>
    ''' <returns>Lista de tipos de documento</returns>
    Public Shared Function ListObligationDetailByObligationId(ByVal ObligationId As Integer, ByVal company As String) As XPCollection
        Dim service As New BudgetServicesXpo(company)
        Return service.ListObligationDetailByObligationId(ObligationId)
    End Function

    ''' <summary>
    ''' Lista todos los tipos de ingreso
    ''' </summary>
    ''' <param name="company">Empresa a la que se encuentra conectado</param>
    ''' <returns>Lista dde fuentes de financiacion</returns>
    Public Shared Function ListEarningsType(ByVal company As String) As XPInstantFeedbackSource
        Dim service As New BudgetServicesXpo(company)
        Return service.GetBudgetEarningsType()
    End Function

    ''' <summary>
    ''' Lista todos los tipos de gasto
    ''' </summary>
    ''' <param name="company">Empresa a la que se encuentra conectado</param>
    ''' <returns>Lista dde fuentes de financiacion</returns>
    Public Shared Function ListExpenseType(ByVal company As String) As XPInstantFeedbackSource
        Dim service As New BudgetServicesXpo(company)
        Return service.GetBudgetExpenseType()
    End Function

    ''' <summary>
    ''' Lista todos las dependencias de presupuesto
    ''' </summary>
    ''' <param name="company">Empresa a la que se encuentra conectado</param>
    ''' <returns>Lista dde fuentes de financiacion</returns>
    Public Shared Function ListBudgetDependency(ByVal company As String) As XPInstantFeedbackSource
        Dim service As New BudgetServicesXpo(company)
        Return service.GetBudgetBudgetDependencyType()
    End Function

    ''' <summary>
    ''' Lista todos los conceptos de presupuesto
    ''' </summary>
    ''' <param name="company">Empresa a la que se encuentra conectado</param>
    ''' <returns>Lista dde fuentes de financiacion</returns>
    Public Shared Function ListBudgetConcept(ByVal company As String) As XPInstantFeedbackSource
        Dim service As New BudgetServicesXpo(company)
        Return service.GetBudgetBudgetConceptType()
    End Function

    ''' <summary>
    ''' lista los rubros por estado, vigencia y tipo 
    ''' </summary>
    ''' <param name="company">Empresa a la que se encuentra conectado</param>
    ''' <returns>Lista dde fuentes de financiacion</returns>
    Public Shared Function ListBudgetCategoryByStatuAndValidityIdAndItemTypeAndFinancialSourceId(Status As Boolean, BudgetaryValidityId As Integer, ItemType As Integer, ByVal company As String) As XPInstantFeedbackSource
        Dim service As New BudgetServicesXpo(company)
        Return service.ListBudgetCategoryByStatuAndValidityIdAndItemTypeAndFinancialSourceId(Status, BudgetaryValidityId, ItemType)
    End Function

    ''' <summary>
    ''' lista los rubros por estado, vigencia y tipo 
    ''' </summary>
    ''' <param name="company">Empresa a la que se encuentra conectado</param>
    ''' <returns>Lista dde fuentes de financiacion</returns>
    Public Shared Function ListBudgetCategoryByStatuAndValidityIdAndItemTypeAndFinancialSourceIdValue(Status As Boolean, BudgetaryValidityId As Integer, ItemType As Integer, FinancialSourceId As Integer, ByVal company As String) As XPInstantFeedbackSource
        Dim service As New BudgetServicesXpo(company)
        Return service.ListBudgetCategoryByStatuAndValidityIdAndItemTypeAndFinancialSourceId(Status, BudgetaryValidityId, ItemType)
    End Function

    ''' <summary>
    ''' lista los rubros por filtro
    ''' </summary>
    ''' <param name="company">Empresa a la que se encuentra conectado</param>
    ''' <returns>Lista dde fuentes de financiacion</returns>
    Public Shared Function ListCategoryByFilter(ByVal company As String, ByVal filter As String) As XPInstantFeedbackSource
        Dim service As New BudgetServicesXpo(company)
        Return service.ListCategoryByFilter(filter)
    End Function

    ''' <summary>
    ''' Lista todos los rubros de presupuesto
    ''' </summary>
    ''' <param name="company">Empresa a la que se encuentra conectado</param>
    ''' <returns>Lista dde fuentes de financiacion</returns>
    Public Shared Function ListBudgetCategoryByStatusAndBudgetaryValidityIdAndItemType(Status As Boolean, BudgetaryValidityId As Integer, ItemType As Integer, ByVal company As String) As XPInstantFeedbackSource
        Dim service As New BudgetServicesXpo(company)
        Return service.ListBudgetCategoryByStatusAndBudgetaryValidityIdAndItemType(Status, BudgetaryValidityId, ItemType)
    End Function

    ''' <summary>
    ''' Lista todos los rubros de presupuesto
    ''' </summary>
    ''' <param name="company">Empresa a la que se encuentra conectado</param>
    ''' <returns>Lista dde fuentes de financiacion</returns>
    Public Shared Function ListRevenueTypeByBudgetValidityIdAndTypeAndStatus(budgetValidityId As Integer, itemType As Integer, status As Boolean, company As String) As XPInstantFeedbackSource
        Dim service As New BudgetServicesXpo(company)
        Return service.ListRevenueTypeByBudgetValidityIdAndTypeAndStatus(budgetValidityId, itemType, status)
    End Function

    ''' <summary>
    ''' Consulta el presupuesto inicial con la vigencia seleccionada
    ''' </summary>
    ''' <param name="validityId">Id de la Vigencia</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function ListViewListAnnualizedCashFlow(validityId As Integer, type As Integer, company As String) As XPInstantFeedbackSource
        Dim service As New BudgetServicesXpo(company)
        Return service.ListViewListAnnualizedCashFlow(validityId, type)
    End Function

    ''' <summary>
    ''' Consulta los reconocimientos por vigencia y estado
    ''' </summary>
    ''' <param name="validityId">Id de la Vigencia</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function ListRecognitionByValidityIdAndStatus(validityId As Integer, status As Integer, company As String) As XPInstantFeedbackSource
        Dim service As New BudgetServicesXpo(company)
        Return service.ListRecognitionByValidityIdAndStatus(validityId, status)
    End Function

    ''' <summary>
    ''' Consulta los compromisos por vigencia y estado
    ''' </summary>
    ''' <param name="validityId">Id de la Vigencia</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function ListCommitmentByValidityIdAndStatus(validityId As Integer, status As Integer, company As String) As XPInstantFeedbackSource
        Dim service As New BudgetServicesXpo(company)
        Return service.ListCommitmentByValidityIdAndStatus(validityId, status)
    End Function

    ''' <summary>
    ''' Consulta las suspenciones de presupuesto por vigencia y estado
    ''' </summary>
    ''' <param name="validityId">Id de la Vigencia</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function ListSuspensionByValidityIdAndStatus(validityId As Integer, status As Integer, company As String) As XPInstantFeedbackSource
        Dim service As New BudgetServicesXpo(company)
        Return service.ListSuspensionByValidityIdAndStatus(validityId, status)
    End Function

    ''' <summary>
    ''' Consulta las ordenes de pago por vigencia y estado
    ''' </summary>
    ''' <param name="validityId">Id de la Vigencia</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function ListPaymentOrderByValidityIdAndStatus(validityId As Integer, status As Integer, company As String) As XPInstantFeedbackSource
        Dim service As New BudgetServicesXpo(company)
        Return service.ListPaymentOrderByValidityIdAndStatus(validityId, status)
    End Function

    ''' <summary>
    ''' Consulta los compromisos por vigencia y estado
    ''' </summary>
    ''' <param name="validityId">Id de la Vigencia</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function ListCommitmentByValidityId(validityId As Integer, company As String) As XPInstantFeedbackSource
        Dim service As New BudgetServicesXpo(company)
        Return service.ListCommitmentByValidityId(validityId)
    End Function

    ''' <summary>
    ''' Consulta las ordenes de pago por id de la vigencia
    ''' </summary>
    ''' <param name="validityId">Id de la Vigencia</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function ListPaymentOrderByValidityId(validityId As Integer, company As String) As XPInstantFeedbackSource
        Dim service As New BudgetServicesXpo(company)
        Return service.ListPaymentOrderByValidityId(validityId)
    End Function

    ''' <summary>
    ''' Consulta los levantamientos de suspenciones de presupuesto por id de la vigencia
    ''' </summary>
    ''' <param name="validityId">Id de la Vigencia</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function ListSuspensionCancellationByValidityId(validityId As Integer, company As String) As XPInstantFeedbackSource
        Dim service As New BudgetServicesXpo(company)
        Return service.ListSuspensionCancellationByValidityId(validityId)
    End Function

    ''' <summary>
    ''' Consulta las suspenciones de presupuesto por id de la vigencia
    ''' </summary>
    ''' <param name="validityId">Id de la Vigencia</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function ListSuspensionByValidityId(validityId As Integer, company As String) As XPInstantFeedbackSource
        Dim service As New BudgetServicesXpo(company)
        Return service.ListSuspensionByValidityId(validityId)
    End Function

    ''' <summary>
    ''' Consulta los reintegros por id de la vigencia
    ''' </summary>
    ''' <param name="validityId">Id de la Vigencia</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function ListReimbursementResourceByValidityId(validityId As Integer, company As String) As XPInstantFeedbackSource
        Dim service As New BudgetServicesXpo(company)
        Return service.ListReimbursementResourceByValidityId(validityId)
    End Function

    ''' <summary>
    ''' Consulta las disponibilidades por vigencia y estado
    ''' </summary>
    ''' <param name="validityId">Id de la Vigencia</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function ListAvailabilityByValidityIdAndStatus(validityId As Integer, status As Integer, company As String) As XPInstantFeedbackSource
        Dim service As New BudgetServicesXpo(company)
        Return service.ListAvailabilityByValidityIdAndStatus(validityId, status)
    End Function

    ''' <summary>
    ''' Consulta las disponibilidades por vigencia , estado , saldo y fecha de vencimiento
    ''' </summary>
    ''' <param name="validityId">Id de la Vigencia</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function ListViewAvailabilityBalance(validityId As Integer, status As Integer, dateServer As Date, company As String) As XPInstantFeedbackSource
        Dim service As New BudgetServicesXpo(company)
        Return service.ListViewAvailabilityBalance(validityId, status, dateServer)
    End Function

    ''' <summary>
    ''' Consulta el presupuesto inicial con el valor Programado
    ''' </summary>
    ''' <param name="validityId">Id de la Vigencia</param>
    ''' <param name="withBalance">Permite saber si se consulta con saldo mayor a cero o no</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function ListBudgetByBudgetValidityId(validityId As Integer, status As Integer, type As Integer, withBalance As Boolean, company As String) As XPCollection
        Dim service As New BudgetServicesXpo(company)
        Return service.ListBudgetByBudgetValidityId(validityId, status, type, withBalance)
    End Function
    ''' <summary>
    ''' lista los presupuestos por tipo de rubro
    ''' </summary>
    ''' <param name="ItemType"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function ListBudgetByCategoryItemType(ItemType As Integer, company As String) As XPInstantFeedbackSource
        Dim service As New BudgetServicesXpo(company)
        Return service.ListBudgetByCategoryItemType(ItemType)
    End Function

    ''' <summary>
    ''' Consulta el detalle de disponibilidad
    ''' </summary>
    ''' <param name="validityId">Id de la Vigencia</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function ListAvailabilityDetailByValidityIdAndStatus(validityId As Integer, status As Integer, company As String) As XPCollection
        Dim service As New BudgetServicesXpo(company)
        Return service.ListAvailabilityDetailByValidityIdAndStatus(validityId, status)
    End Function

    ''' <summary>
    ''' Consulta el detalle de obligacion
    ''' </summary>
    ''' <param name="validityId">Id de la Vigencia</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function ListObligationDetailByValidityIdAndStatus(validityId As Integer, status As Integer, thirdpartyId As Integer, company As String) As XPCollection
        Dim service As New BudgetServicesXpo(company)
        Return service.ListObligationDetailByValidityIdAndStatus(validityId, status, thirdpartyId)
    End Function

    ''' <summary>
    ''' Consulta el presupuesto inicial con el valor Programado
    ''' </summary>
    ''' <param name="RecognitionId">Id del reconocimiento</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function ListRecognitionDetailByRecognitionId(RecognitionId As Integer, company As String) As XPCollection
        Dim service As New BudgetServicesXpo(company)
        Return service.ListRecognitionDetailByRecognitionId(RecognitionId)
    End Function
    ''' <summary>
    ''' metodo para listar los detalles del compromiso para usarlos en el popup de obligaiones
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function ListCommitmentDetailObligation(company As String, validityId As Integer, thirdPartyId As Integer, documentDate As DateTime) As XPCollection
        Dim service As New BudgetServicesXpo(company)
        Return service.ListCommitmentDetailObligation(validityId, thirdPartyId, documentDate)
    End Function
    ''' <summary>
    ''' Consulta los detalles de suspencion presupuestal por id de la suspencion
    ''' </summary>
    ''' <param name="SuspensionId">Id de la suspencion presupuestal</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function ListSuspensionDetailBySuspensionId(SuspensionId As Integer, company As String) As XPCollection
        Dim service As New BudgetServicesXpo(company)
        Return service.ListSuspensionDetailBySuspensionId(SuspensionId)
    End Function
    ''' <summary>
    ''' Consulta el presupuesto inicial con el valor Programado
    ''' </summary>
    ''' <param name="CommitmentId">Id del compromiso</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function ListCommitmentDetailByCommitmentId(CommitmentId As Integer, company As String) As XPCollection
        Dim service As New BudgetServicesXpo(company)
        Return service.ListCommitmentDetailByCommitmentId(CommitmentId)
    End Function

    ''' <summary>
    ''' Consulta los detalles de la orden de pago por id de la orden de pago
    ''' </summary>
    ''' <param name="PaymentOrderId">Id de la orden de pago</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function ListPaymentOrderDetailByPaymentOrderId(PaymentOrderId As Integer, company As String) As XPCollection
        Dim service As New BudgetServicesXpo(company)
        Return service.ListPaymentOrderDetailByPaymentOrderId(PaymentOrderId)
    End Function

    ''' <summary>
    ''' Consulta los detalles de la disponibilidad por id de disponibilidad
    ''' </summary>
    ''' <param name="AvailabilityId">Id de la disponibilidad</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function ListAvailabilityDetailByAvailabilityId(AvailabilityId As Integer, company As String) As XPCollection
        Dim service As New BudgetServicesXpo(company)
        Return service.ListAvailabilityDetailByAvailabilityId(AvailabilityId)
    End Function

    ''' <summary>
    ''' Consulta el presupuesto inicial con el valor Programado
    ''' </summary>
    ''' <param name="validityId">Id de la Vigencia</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function ListAnnualizedCashFlowValidityId(validityId As Integer, status As Integer, type As Integer, company As String) As XPCollection
        Dim service As New BudgetServicesXpo(company)
        Return service.ListAnnualizedCashFlowValidityId(validityId, status, type)
    End Function
    ''' <summary>
    ''' lista los reconocimienntos por vigencia , tercero y estado
    ''' </summary>
    ''' <param name="validityId"></param>
    ''' <param name="thirdPartyId"></param>
    ''' <param name="status"></param>
    ''' <param name="company"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function ListRecognitionDetailByValidatyIdAndThirdPartyId(validityId As Integer, thirdPartyId As Integer, status As Integer, company As String) As XPCollection
        Dim service As New BudgetServicesXpo(company)
        Return service.ListRecognitionDetailByValidatyIdAndThirdPartyId(validityId, thirdPartyId, status)
    End Function

    ''' <summary>
    ''' lista los recaudos para hacer modificaciones
    ''' </summary>
    ''' <param name="collectionId"></param>
    ''' <param name="validatyId"></param>
    ''' <param name="status"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function ListCollectionDetailByCollectionIdAndValidatyId(collectionId As Integer, validatyId As Integer, status As Integer, company As String) As XPCollection
        Dim service As New BudgetServicesXpo(company)
        Return service.ListCollectionDetailByCollectionIdAndValidatyId(collectionId, validatyId, status)
    End Function

    ''' <summary>
    ''' Consulta las modificaciones del reconocimiento por vigencia
    ''' </summary>
    ''' <param name="validityId">Id de la Vigencia</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function ListRecognitionModificationByValidityId(validityId As Integer, company As String) As XPInstantFeedbackSource
        Dim service As New BudgetServicesXpo(company)
        Return service.ListRecognitionModificationByValidityId(validityId)
    End Function

    ''' <summary>
    ''' Consulta las modificaciones de las disponibilidades por vigencia
    ''' </summary>
    ''' <param name="validityId">Id de la Vigencia</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function ListAvailabilityModificationByValidityId(validityId As Integer, company As String) As XPInstantFeedbackSource
        Dim service As New BudgetServicesXpo(company)
        Return service.ListAvailabilityModificationByValidityId(validityId)
    End Function

    ''' <summary>
    ''' Consulta las modificaciones de los compromisos por vigencia
    ''' </summary>
    ''' <param name="validityId">Id de la Vigencia</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function ListCommitmentModificationByValidityId(validityId As Integer, company As String) As XPInstantFeedbackSource
        Dim service As New BudgetServicesXpo(company)
        Return service.ListCommitmentModificationByValidityId(validityId)
    End Function

    ''' <summary>
    ''' Consulta los traslados pac por vigencia
    ''' </summary>
    ''' <param name="validityId">Id de la Vigencia</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function ListAnnualizedCashFlowTransferByValidityId(validityId As Integer, type As Integer, company As String) As XPInstantFeedbackSource
        Dim service As New BudgetServicesXpo(company)
        Return service.ListAnnualizedCashFlowTransferByValidityId(validityId, type)
    End Function

    ''' <summary>
    ''' Lista todos los rubros de presupuesto
    ''' </summary>
    ''' <param name="company">Empresa a la que se encuentra conectado</param>
    ''' <returns>Lista dde fuentes de financiacion</returns>
    Public Shared Function ListCategoryByBudgetaryValidityIdAndItemType(filtro As String, ByVal company As String) As XPInstantFeedbackSource
        Dim service As New BudgetServicesXpo(company)
        Return service.ListCategoryByBudgetaryValidityIdAndItemType(filtro)
    End Function

    ''' <summary>
    ''' Lista todos los rubros de presupuesto
    ''' </summary>
    ''' <param name="company">Empresa a la que se encuentra conectado</param>
    ''' <returns>Lista dde fuentes de financiacion</returns>
    Public Shared Function ListCategoryByBudgetaryValidityIdAndItemTypeForTreeList(budgetaryValidityId As Integer, itemType As Integer, ByVal company As String) As XPCollection
        Dim service As New BudgetServicesXpo(company)
        Return service.ListCategoryByBudgetaryValidityIdAndItemTypeForTreeList(budgetaryValidityId, itemType)
    End Function

    ''' <summary>
    ''' Lista todos los rubros de presupuesto
    ''' </summary>
    ''' <param name="company">Empresa a la que se encuentra conectado</param>
    ''' <returns>Lista dde fuentes de financiacion</returns>
    Public Shared Function ListRevenueTypeByBudgetValidityIdAndType(budgetaryValidityId As Integer, itemType As Integer, ByVal company As String) As XPCollection
        Dim service As New BudgetServicesXpo(company)
        Return service.ListRevenueTypeByBudgetValidityIdAndType(budgetaryValidityId, itemType)
    End Function

    ''' <summary>
    ''' Lista las modificaciones de presupuesto
    ''' </summary>
    ''' <param name="company"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function ListGetBudgetModifications(budgetaryValidityId As Integer, itemType As Integer, ByVal company As String) As XPInstantFeedbackSource
        Dim service As New BudgetServicesXpo(company)
        Return service.GetBudgetModifications(budgetaryValidityId, itemType)
    End Function

    ''' <summary>
    ''' Lista los conceptos de pagos por estado y si maneja retencion
    ''' </summary>
    ''' <param name="company">Empresa a la que se encuentra conectado</param>
    ''' <returns>Lista de tipos de documento</returns>
    Public Shared Function ListBudgetEntityByStatus(ByVal status As Boolean, ByVal company As String) As XPInstantFeedbackSource
        Dim service As New BudgetServicesXpo(company)
        Return service.ListBudgetEntityByStatus(status)
    End Function

    ''' <summary>
    ''' Lista los conceptos de pagos por estado y si maneja retencion
    ''' </summary>
    ''' <param name="company">Empresa a la que se encuentra conectado</param>
    ''' <returns>Lista de tipos de documento</returns>
    Public Shared Function ListFinancialSourceByStatus(ByVal status As Boolean, ByVal validityId As Integer, ByVal company As String) As XPInstantFeedbackSource
        Dim service As New BudgetServicesXpo(company)
        Return service.ListFinancialSourceByStatus(status, validityId)
    End Function

#End Region

#Region "Others"

    ''' <summary>
    ''' Lists the user entities.	
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function ListUserEntities(ByVal container As String) As XPInstantFeedbackSource
        Dim security As New SecurityServicesXpo(container)
        Return security.GetUsers()
    End Function


    ''' <summary>
    ''' Lists the user entities sync.	
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function ListUserEntitiesSync(ByVal container As String) As XPServerCollectionSource
        Dim security As New SecurityServicesXpo(container, True)
        Return security.GetUsersSync()
    End Function

    ''' <summary>
    ''' Lists the user entities sync.	
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function ListAllUser(ByVal container As String) As XPInstantFeedbackSource
        Dim security As New SecurityServicesXpo(container, True)
        Return security.GetAllUser()
    End Function

    ''' <summary>
    ''' Lists the roles entities.	
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function ListRolesEntities(ByVal container As String) As XPInstantFeedbackSource
        Dim security As New SecurityServicesXpo(container, True)
        Return security.GetRoles()
    End Function

    ''' <summary>
    ''' Lists the roles entities sync.	
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function ListRolesEntitiesSync(ByVal container As String) As XPServerCollectionSource
        Dim security As New SecurityServicesXpo(container, True)
        Return security.GetRolesSync()
    End Function

    ''' <summary>
    ''' Lists the groups entities.	
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function ListGroupsEntities(ByVal container As String) As XPInstantFeedbackSource
        Dim security As New SecurityServicesXpo(container, True)
        Return security.GetGroups()
    End Function

    ''' <summary>
    ''' Lists the groups entities sync.	
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function ListGroupsEntitiesSync(ByVal container As String) As XPServerCollectionSource
        Dim security As New SecurityServicesXpo(container, True)
        Return security.GetGroupsSync()
    End Function


    ''' <summary>
    ''' Lists detail Concept
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function ListDetailedConcept(ByVal Company As String) As XPInstantFeedbackSource
        Dim Glosas As New GlosasServicesXpo(Company)
        Return Glosas.GetDetailedConcepts
    End Function

    ''' <summary>
    ''' Lists general concept	
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function ListGeneralConcept(ByVal Company As String) As XPInstantFeedbackSource
        Dim Glosas As New GlosasServicesXpo(Company)
        Return Glosas.GetGeneralConcepts
    End Function
    ''' <summary>
    ''' Lists general concept	
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function ListResponsible(ByVal Company As String) As XPInstantFeedbackSource
        Dim Glosas As New GlosasServicesXpo(Company)
        Return Glosas.GetResponsibles
    End Function
    ''' <summary>
    ''' Lists general concept	
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function ListResponseHierarchy(ByVal Company As String) As XPInstantFeedbackSource
        Dim Glosas As New GlosasServicesXpo(Company)
        Return Glosas.ListResponseHierarchy
    End Function
    ''' <summary>
    ''' Lists general concept	
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function ListSpecificConcept(ByVal Company As String) As XPInstantFeedbackSource
        Dim Glosas As New GlosasServicesXpo(Company)
        Return Glosas.GetSpecificConcepts
    End Function
    ''' <summary>
    ''' Lists general concept	
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function ListCustomers(ByVal Company As String) As XPInstantFeedbackSource
        Dim Glosas As New GlosasServicesXpo(Company)
        Return Glosas.GetCustomers
    End Function

    ''' <summary>
    ''' List Corporation
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function ListCompany(ByVal Company As String) As XPInstantFeedbackSource
        Dim Glosas As New GlosasServicesXpo(Company)
        Return Glosas.GetCompany
    End Function

    ''' <summary>
    '''  List Position Level
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function ListPositionLevel(ByVal Company As String) As XPInstantFeedbackSource
        Dim Payroll As New PayrollServicesXpo(Company)
        Return Payroll.GetPositionLevel()
    End Function

    ''' <summary>
    ''' List Corporation
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function ListCorporation(ByVal Company As String) As XPInstantFeedbackSource
        Dim Payroll As New PayrollServicesXpo(Company)
        Return Payroll.GetCorporation()
    End Function

    ''' <summary>
    ''' List GetIdObjectionsReception
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function GetObjectionsReception(ByVal Company As String) As XPInstantFeedbackSource
        Dim Glosas As New GlosasServicesXpo(Company)
        Return Glosas.GetObjectionsReception()
    End Function

    ''' <summary>
    ''' Lista todas los Países 
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function ListCountry(ByVal Company As String) As XPInstantFeedbackSource
        Dim country As New CommonServicesXpo(Company)
        Return country.GetCountry()
    End Function

    ''' <summary>
    ''' Lista todos los Departamentos
    ''' </summary>
    ''' <returns></returns>
    Public Shared Function ListDepartment(ByVal idPais As String, ByVal Company As String) As XPInstantFeedbackSource
        Dim department As New CommonServicesXpo(Company)
        Return department.GetDepartment(idPais)
    End Function


    ''' <summary>
    ''' Lista todas las ciudades de un departamento
    ''' </summary>
    ''' <param name="IdDepartamento">id Departamento</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function ListCity(ByVal IdDepartamento As Integer, ByVal Company As String) As XPInstantFeedbackSource
        Dim city As New CommonServicesXpo(Company)
        Return city.GetCities(IdDepartamento)
    End Function

    ''' <summary>
    ''' Lista todos los centros de atención
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function ListHealthCenter(ByVal Company As String) As XPInstantFeedbackSource
        Dim healthCenter As New CommonServicesXpo(Company)
        Return healthCenter.GetHealthCenter
    End Function


    ''' <summary>
    ''' Lista todos los niveles de educacion
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function ListEducationLevels(ByVal Company As String) As XPInstantFeedbackSource
        Dim EducationLevels As New PayrollServicesXpo(Company)
        Return EducationLevels.GetEducationLevels()
    End Function

    ''' <summary>
    ''' Lista todas las profesiones de un nivel de educacion
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function ListProfessions(ByVal Company As String) As XPInstantFeedbackSource
        Dim Professions As New PayrollServicesXpo(Company)
        Return Professions.GetProfessions()
    End Function

    ''' <summary>
    ''' Lista todas los usuarios.
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function ListUsers(ByVal Company As String) As XPInstantFeedbackSource
        Dim Users As New CommonServicesXpo(Company)
        Return Users.GetUsers
    End Function

    Public Shared Function ListRetirementReason(ByVal Company As String) As XPInstantFeedbackSource
        Dim reason As New PayrollServicesXpo(Company)
        Return reason.GetRetirementReason()
    End Function

    ''' <summary>
    ''' Lista las unidades de negocio
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function ListBusinessUnit(ByVal Company As String) As XPInstantFeedbackSource
        Dim business As New PayrollServicesXpo(Company)
        Return business.GetBusinessUnit()
    End Function

    ''' <summary>
    ''' Lista las cabeceras de conciliaciones.
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function ListConciliationC(ByVal Company As String) As LinqInstantFeedbackSource
        Dim conciliationC As New GlosasServicesXpo(Company)
        Return conciliationC.GetConciliationCList
    End Function

    ''' <summary>
    ''' Lista las cabeceras de devoluciones.
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function ListDevolutionC(ByVal Company As String) As LinqInstantFeedbackSource 'XPInstantFeedbackSource
        Dim devolutionC As New GlosasServicesXpo(Company)
        Return devolutionC.GetDevolutionCList
    End Function

    ''' <summary>
    ''' Lista todos los idiomas
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function ListLanguage(ByVal Company As String) As XPInstantFeedbackSource
        Dim language As New PayrollServicesXpo(Company)
        Return language.GetLanguage()
    End Function

    ''' <summary>
    ''' Lista Todas las Compañías de Nómina
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function ListCompanyPayroll(ByVal CompanyAux As String) As XPInstantFeedbackSource
        Dim company As New PayrollServicesXpo(CompanyAux)
        Return company.GetCompany()
    End Function

    ''' <summary>
    ''' Lista los usuarios por su nombre
    ''' </summary>
    ''' <param name="personName">Nombre de la persona</param>
    ''' <returns>Lista de usuarios</returns>
    Public Shared Function ListUserByPersonName(ByVal personName As String, ByVal UserCodeExcluid As String, ByVal container As String) As LinqInstantFeedbackSource
        Dim users As New SecurityServicesXpo(container)
        Return users.ListUserByPersonName(personName.Trim(), UserCodeExcluid)
    End Function

    ''' <summary>
    ''' Lista todos os parentescos
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function ListKinship(ByVal Company As String) As XPInstantFeedbackSource
        Dim kinship As New PayrollServicesXpo(Company)
        Return kinship.GetKinship()
    End Function
    ''' <summary>
    ''' Lista todos los fondos
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function ListFunds(ByVal Company As String) As XPInstantFeedbackSource
        Dim funds As New PayrollServicesXpo(Company)
        Return funds.GetFunds()
    End Function
    ''' <summary>
    ''' Lista todos los tipos de telefono
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function ListPhoneType(ByVal Company As String) As XPInstantFeedbackSource
        Dim phonetype As New CommonServicesXpo(Company)
        Return phonetype.GetPhoneType()
    End Function

    Public Shared Function ListGroup(ByVal Company As String) As XPInstantFeedbackSource
        Dim Group As New PayrollServicesXpo(Company)
        Return Group.GetGroup()
    End Function

    Public Shared Function ListHumanTalent(ByVal Company As String) As XPInstantFeedbackSource
        Dim humanTalent As New PayrollServicesXpo(Company)
        Return humanTalent.GetHumanTalent()
    End Function

    ''' <summary>
    ''' Lista los objetos Cartera Glosa.
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function ListXpoInvoicesByPartialPayments(ByVal Nit As String, ByVal Company As String) As XPInstantFeedbackSource
        Dim portfolioGlosa As New GlosasServicesXpo(Company)
        Return portfolioGlosa.ListXpoInvoicesByPartialPayments(Nit)
    End Function


    ''' <summary>
    ''' Obtiene los objetos de tipos Cartera Glosa.
    ''' </summary>
    ''' <returns></returns>
    Public Shared Function GetXPOPortfolioGlosa(ByVal Nit As String, ByVal Company As String) As XPInstantFeedbackSource
        Dim portfolioGlosa As New GlosasServicesXpo(Company)
        Return portfolioGlosa.GetXPOPortfolioGlosa(Nit)
    End Function

    ''' <summary>
    ''' Lista los objetos Cartera Glosa.
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function ListPortfolio_AccountReceivable(ByVal Nit As String, ByVal Company As String) As XPInstantFeedbackSource
        Dim portfolioGlosa As New GlosasServicesXpo(Company)
        Return portfolioGlosa.ListPortfolio_AccountReceivable(Nit)
    End Function


    ''' <summary>
    ''' Lista info. facturas  a exportar a excel
    ''' </summary>
    ''' <param name="ListInvocie"></param>
    ''' <param name="Company"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function ListGetInvoiceExportExcel(ByVal ListInvocie As List(Of String), ByVal OnlyMovements As Boolean, ByVal Company As String) As PLinqServerModeSource
        Dim portfolioGlosa As New GlosasServicesXpo(Company)
        Return portfolioGlosa.GetInvoiceExportExcel(ListInvocie, OnlyMovements)
    End Function

    ''' <summary>
    ''' Lista info. facturas  a exportar a excel
    ''' </summary>
    ''' <param name="ListInvocie"></param>
    ''' <param name="Company"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function ListGetInvoiceExportExcelGlosa(ByVal ListInvocie As List(Of String), ByVal Company As String) As PLinqServerModeSource
        Dim portfolioGlosa As New GlosasServicesXpo(Company)
        Return portfolioGlosa.ListGetInvoiceExportExcelGlosa(ListInvocie)
    End Function

    ''' <summary>
    ''' Lista de detalles de factura en coordinacion
    ''' </summary>
    ''' <param name="IdObjc"></param>
    ''' <param name="Company"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function ListGlosasObjectionD(ByVal IdObjc As Integer, ByVal Company As String) As PLinqServerModeSource
        Dim portfolioGlosa As New GlosasServicesXpo(Company)
        Return portfolioGlosa.ListGlosasObjectionD(IdObjc)
    End Function


    ''' <summary>
    ''' Lista todas las sucursales
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function ListBranchOffice(ByVal Company As String) As XPInstantFeedbackSource
        Dim branchOffice As New PayrollServicesXpo(Company)
        Return branchOffice.GetBranchOffice()
    End Function

    ''' <summary>
    ''' Lista todas las unidades funcionales
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function ListFunctionalUnit(ByVal Company As String) As XPInstantFeedbackSource
        Dim functionalUnit As New PayrollServicesXpo(Company)
        Return functionalUnit.GetFunctionalUnit()
    End Function

    Public Shared Function ListFunctionalUnitXpCollection(ByVal status As Boolean, ByVal Company As String) As XPCollection
        Dim service As New PayrollServicesXpo(Company)
        Return service.ListFunctionalUnitXpCollection(status)
    End Function

    ''' <summary>
    ''' Lista los centros de costos
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function ListCostCenter(ByVal Company As String) As XPInstantFeedbackSource
        Dim costCenter As New PayrollServicesXpo(Company)
        Return costCenter.GetCostCenter()
    End Function
    ''' <summary>
    ''' obtiene los centros de costo por estado
    ''' </summary>
    ''' <param name="status"></param>
    ''' <param name="Company"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function GetCostCenterByState(ByVal status As Boolean, ByVal Company As String) As XPInstantFeedbackSource
        Dim service As New PayrollServicesXpo(Company)
        Return service.GetCostCenterByState(status)
    End Function


    ''' <summary>
    ''' Lista todos los bancos
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function ListBank(ByVal Company As String) As XPInstantFeedbackSource
        Dim bank As New PayrollServicesXpo(Company)
        Return bank.GetBank()
    End Function

    ''' <summary>
    ''' Lista todos los bancos
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function ListBankByStatus(ByVal status As Boolean, ByVal Company As String) As XPInstantFeedbackSource
        Dim bank As New PayrollServicesXpo(Company)
        Return bank.GetBankByStatus(status)
    End Function

    ''' <summary>
    ''' Lista Todas los Tipos de Pensionados
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function ListPensionaryType(ByVal Company As String) As XPInstantFeedbackSource
        Dim pensionarytype As New PayrollServicesXpo(Company)
        Return pensionarytype.GetPensionaryType()
    End Function

    ''' <summary>
    ''' Lista Todas los Centros de Trabajo
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function ListWorkCenter(ByVal Company As String) As XPInstantFeedbackSource
        Dim workCenter As New PayrollServicesXpo(Company)
        Return workCenter.GetWorkCenter()
    End Function

    ''' <summary>
    ''' Lista todos los tipos de estudio
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function ListStudyType(ByVal Company As String) As XPInstantFeedbackSource
        Dim studyType As New PayrollServicesXpo(Company)
        Return studyType.GetStudyType()
    End Function

    ''' <summary>
    ''' Lista todos los terceros por estado
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function ListThirdParty(ByVal Company As String) As XPInstantFeedbackSource
        Dim thirdParty As New CommonServicesXpo(Company)
        Return thirdParty.GetThirdParty()
    End Function

    ''' <summary>
    ''' Lista todas las dependencias
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function ListDependency(ByVal Company As String, validity As Integer) As XPInstantFeedbackSource
        Dim dependency As New BudgetServicesXpo(Company)
        Return dependency.GetDependency(validity)
    End Function

    ''' <summary>
    ''' Lista todos los terceros
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function ListAllThirdParty(ByVal Company As String) As XPInstantFeedbackSource
        Dim thirdParty As New CommonServicesXpo(Company)
        Return thirdParty.ListAllThirdParty()
    End Function
    ''' <summary>
    ''' Lista tercero mediante un lInq
    ''' </summary>
    ''' <param name="Company"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function ListThird(ByVal Company As String) As LinqInstantFeedbackSource
        Dim thirdParty As New CommonServicesXpo(Company)
        Return thirdParty.ListThird()
    End Function


    ''' <summary>
    ''' Lista todos las ciudades con datos del departamento y ciudad
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function ListCity(ByVal Company As String) As XPInstantFeedbackSource
        Dim city As New PayrollServicesXpo(Company)
        Return city.GetCity()
    End Function


    ''' <summary>
    ''' Lista todos los Riesgos Profesionales
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function ListProfessionalRisk(ByVal Company As String) As XPInstantFeedbackSource
        Dim professionalRisk As New PayrollServicesXpo(Company)
        Return professionalRisk.GetProfessionalRisk()
    End Function

    ''' <summary>
    ''' Lista todos los grupos de contratos
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function ListContractGroup(ByVal Company As String) As XPInstantFeedbackSource
        Dim contractGroup As New PayrollServicesXpo(Company)
        Return contractGroup.GetContractGroup()
    End Function

    ''' <summary>
    ''' Lista todas las discapacidades
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function ListDisability(ByVal Company As String) As XPInstantFeedbackSource
        Dim disability As New CommonServicesXpo(Company)
        Return disability.GetDisability()
    End Function

    ''' <summary>
    ''' Lista Todos los Centros de Estudio
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function ListStudyCenter(ByVal Company As String) As XPInstantFeedbackSource
        Dim studyCenter As New PayrollServicesXpo(Company)
        Return studyCenter.GetStudyCenter()
    End Function
    ''' <summary>
    ''' Lista todas las discapacidades
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function ListThirdPartyByPersonId(personId As Integer, ByVal Company As String) As XPInstantFeedbackSource
        Dim thirdParty As New CommonServicesXpo(Company)
        Return thirdParty.GetThirdPartyByPersonId(personId)
    End Function

    ''' <summary>
    ''' Lista de Tipos de Contrato
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function ListContractType(ByVal Company As String) As XPInstantFeedbackSource
        Dim contractType As New PayrollServicesXpo(Company)
        Return contractType.GetContractType()
    End Function

    ''' <summary>
    ''' Lista todos los tipos de contribuyentes
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function ListContributorType(ByVal Company As String) As XPInstantFeedbackSource
        Dim contributor As New PayrollServicesXpo(Company)
        Return contributor.GetContributorType()
    End Function

    ''' <summary>
    ''' Lista Todas las Plantillas de Contrato
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function ListContractTemplate(ByVal Company As String) As XPInstantFeedbackSource
        Dim contractTemplate As New PayrollServicesXpo(Company)
        Return contractTemplate.GetContractTemplate()
    End Function

    ''' <summary>
    ''' Lista las Retenciones
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function ListRetention(ByVal Company As String) As XPInstantFeedbackSource
        Dim retention As New PayrollServicesXpo(Company)
        Return retention.GetRetention()
    End Function
    ''' <summary>
    ''' Lista Todos los cargos
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function ListPosition(ByVal Company As String) As XPInstantFeedbackSource
        Dim position As New PayrollServicesXpo(Company)
        Return position.GetPosition()
    End Function

    ''' <summary>
    ''' Lista Todas las Incapacidades
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function ListInability(ByVal Company As String) As XPInstantFeedbackSource
        Dim inability As New PayrollServicesXpo(Company)
        Return inability.GetInability()
    End Function

    ''' <summary>
    ''' Lista Todas las personas
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function ListPerson(ByVal Company As String) As XPInstantFeedbackSource
        Dim person As New CommonServicesXpo(Company)
        Return person.GetPerson()
    End Function

    ''' <summary>
    ''' Lista Todos los Conceptos
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function ListConcept(ByVal Company As String) As XPInstantFeedbackSource
        Dim concept As New PayrollServicesXpo(Company)
        Return concept.GetConcept()
    End Function

    ''' <summary>
    ''' Lista Todos los Tipos de Vinculación Laboral
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function ListJobBondingType(ByVal Company As String) As XPInstantFeedbackSource
        Dim jobBondingType As New PayrollServicesXpo(Company)
        Return jobBondingType.GetJobBondingType()
    End Function

    ''' <summary>
    ''' Lista las Unidades de Tiempo
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function ListTimeUnit(ByVal Company As String) As XPInstantFeedbackSource
        Dim timeUnit As New CommonServicesXpo(Company)
        Return timeUnit.GetTimeUnit()
    End Function

    ''' <summary>
    ''' Lista todos los grupos de una empresa
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function ListGroupByCompany(companyId As Integer, ByVal Company As String) As XPInstantFeedbackSource
        Dim group As New PayrollServicesXpo(Company)
        Return group.GetGroupByCompany(companyId)
    End Function

    ''' <summary>
    ''' Lista todas las sucursales por empresa
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function ListBranchOfficeByCompany(companyId As Integer, ByVal Company As String) As XPInstantFeedbackSource
        Dim branchOffice As New PayrollServicesXpo(Company)
        Return branchOffice.GetBranchOfficeByCompany(companyId)
    End Function

    ''' <summary>
    ''' Lista todas las unidades funcionales por sucursales
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function ListFunctionalUnitByBranchOffice(branchOfficeId As Integer, ByVal Company As String) As XPInstantFeedbackSource
        Dim functionalUnit As New PayrollServicesXpo(Company)
        Return functionalUnit.GetFunctionalUnitByBranchOffice(branchOfficeId)
    End Function

    ''' <summary>
    ''' Lista todas las plantillas de turnos
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function ListScheduleTemplate(ByVal Company As String) As XPInstantFeedbackSource
        Dim schedule As New PayrollServicesXpo(Company)
        Return schedule.GetScheduleTemplate()
    End Function

    ''' <summary>
    ''' Lista todas las cabeceras de recepcion de objeciones que estan en estado de
    ''' 2-Confirmada sin evaluar y 5-Confirmada evaluada
    ''' </summary>
    ''' <returns></returns>
    Public Shared Function ListObjectionReceptionCByState(ByVal Company As String) As XPInstantFeedbackSource
        Dim objectionC As New GlosasServicesXpo(Company)
        Return objectionC.ListObjectionReceptionCByState()
    End Function

    ''' <summary>
    ''' Lists the justification templates entities.	
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function ListJustificationTemplates(ByVal Company As String) As XPInstantFeedbackSource
        Dim glosas As New GlosasServicesXpo(Company)
        Return glosas.GetTemplateJustification
    End Function

    ''' <summary>
    ''' Función para obtener una lista de datos de una entidad XPO
    ''' </summary>
    ''' <typeparam name="T">Objeto XPO</typeparam>
    ''' <param name="Company">Compañia Actual</param>
    ''' <param name="Fun">Objeto tipo Función opcional para filtrar los datos necesarios</param>
    ''' <returns>Objeto</returns>
    Public Shared Function ListCollection(Of T)(ByVal Company As String, Optional Fun As Func(Of T, Boolean) = Nothing, Optional criteria As String = Nothing) As Object
        Dim glosas As New GlosasServicesXpo(Company)
        Return glosas.GetCollection(Of T)(Fun, criteria)
    End Function

    ''' <summary>
    ''' Función para obtener una lista de datos de una entidad XPO
    ''' </summary>
    ''' <typeparam name="T">Objeto XPO</typeparam>
    ''' <param name="Company">Compañia Actual</param>
    ''' <param name="Fun">Objeto tipo Función opcional para filtrar los datos necesarios</param>
    ''' <returns>Objeto</returns>
    Public Shared Function ListCollectionGlosas(Of T)(ByVal Company As String, Optional Fun As Func(Of T, Boolean) = Nothing, Optional criteria As String = Nothing) As Object
        Dim Glosas As New GlosasServicesXpo(Company)
        Return Glosas.GetCollection(Of T)(Fun, criteria)
    End Function

    ''' <summary>
    ''' Función para obtener una lista de datos de una entidad XPO
    ''' </summary>
    ''' <typeparam name="T">Objeto XPO</typeparam>
    ''' <param name="Company">Compañia Actual</param>
    ''' <param name="Fun">Objeto tipo Función opcional para filtrar los datos necesarios</param>
    ''' <returns>Objeto</returns>
    Public Shared Function ListCollectionAccounting(Of T)(ByVal Company As String, Optional Fun As Func(Of T, Boolean) = Nothing, Optional criteria As String = Nothing) As Object
        Dim Accounting As New AccountingServiceXpo(Company)
        Return Accounting.GetCollection(Of T)(Fun, criteria)
    End Function

#Region "Listar ObjectionReceptionD con PLinqServerModeSource (Pruebas Reporte)"

    'Public Shared Function ListGetGeneralBalance(ByVal IdReceptionC As Integer, ByVal Company As String) As PLinqServerModeSource
    'Dim Accounting As New AccountingServiceXpo(Company)
    ' Return Accounting.ListGetGeneralBalance(IdReceptionC)
    'End Function

#End Region

    ''' <summary>
    ''' Función para obtener una lista de datos de una entidad XPO
    ''' </summary>
    ''' <typeparam name="T">Objeto XPO</typeparam>
    ''' <param name="Company">Compañia Actual</param>
    ''' <param name="Fun">Objeto tipo Función opcional para filtrar los datos necesarios</param>
    ''' <returns>Objeto</returns>
    Public Shared Function ListCollectionPayRoll(Of T)(ByVal Company As String, Optional Fun As Func(Of T, Boolean) = Nothing, Optional criteria As String = Nothing) As Object
        Dim Payroll As New PayrollServicesXpo(Company)
        Return Payroll.GetCollection(Of T)(Fun, criteria)
    End Function

    ''' <summary>
    ''' Función para obtener una lista de datos de una entidad XPO
    ''' </summary>
    ''' <typeparam name="T">Objeto XPO</typeparam>
    ''' <param name="Company">Compañia Actual</param>
    ''' <param name="Fun">Objeto tipo Función opcional para filtrar los datos necesarios</param>
    ''' <returns>Objeto</returns>
    'Public Shared Function ListCollectionMovements(Of T)(ByVal Company As String, Optional Fun As Func(Of T, Boolean) = Nothing, Optional criteria As String = Nothing) As Object
    '    Dim glosas As New GlosasServicesXpo(Company)
    '    Return glosas.GetCollectionMovements(Of T)(Fun, criteria)
    'End Function

    ''' <summary>
    ''' Función para obtener una lista de datos de una entidad XPO
    ''' </summary>
    ''' <typeparam name="T">Objeto XPO</typeparam>
    ''' <param name="Company">Compañia Actual</param>
    ''' <param name="Fun">Objeto tipo Función opcional para filtrar los datos necesarios</param>
    ''' <returns>Objeto</returns>
    Public Shared Function ListCollectionConciliation(Of T)(ByVal Company As String, Optional Fun As Func(Of T, Boolean) = Nothing, Optional criteria As String = Nothing) As Object
        Dim glosas As New GlosasServicesXpo(Company)
        Return glosas.GetCollectionConciliation(Of T)(Fun, criteria, Company)
    End Function

    Public Shared Function ListView(Of T)(ByVal Company As String) As XPView
        Dim glosas As New GlosasServicesXpo(Company)
        Return glosas.GetCollectionView(Of T)()
    End Function

    ''' <summary>
    ''' Lista De Facturas y Informacionde la cabecera de un oficio
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function ListObjectionsReceptionD(ByVal Company As String) As LinqInstantFeedbackSource
        Dim ObjectionD As New GlosasServicesXpo(Company)
        Return ObjectionD.ListObjectionsReceptionD()
    End Function

    ''' <summary>
    ''' Lista de facturas Radicadas sin confirmar
    ''' </summary>
    ''' <param name="Company"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function ListInvoiceRadicate(ByVal Company As String) As LinqInstantFeedbackSource
        Dim ObjectionD As New GlosasServicesXpo(Company)
        Return ObjectionD.ListInvoiceRadicate()
    End Function

    ''' <summary>
    ''' Lista de facturas Radicadas sin confirmar
    ''' </summary>
    ''' <param name="Company"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function ListInvoiceRadicateConfirm(ByVal Company As String) As LinqInstantFeedbackSource
        Dim ObjectionD As New GlosasServicesXpo(Company)
        Return ObjectionD.ListInvoiceRadicateConfirm()
    End Function


    ''' <summary>
    ''' Lista de facturas de pagos parciales
    ''' </summary>
    ''' <param name="Company"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function ListInvoicePartialPayments(ByVal Company As String) As LinqInstantFeedbackSource
        Dim ObjectionD As New GlosasServicesXpo(Company)
        Return ObjectionD.ListInvoicePartialPayments()
    End Function

    ''' <summary>
    ''' Lista todas la facturas por evaluar por el codigo de responsable
    ''' </summary>
    ''' <param name="Company">Empresa</param>
    ''' <param name="responsibleCode">Código del responsable</param>
    ''' <returns>Lista de facturas por evaluar</returns>
    Public Shared Function ListEvaluationInvoiceByResponsible(ByVal Company As String, ByVal responsibleCode As String) As LinqInstantFeedbackSource
        Dim ObjectionD As New GlosasServicesXpo(Company)
        Return ObjectionD.ListEvaluationInvoiceByResponsible(responsibleCode)
    End Function

    ''' <summary>
    ''' Lista de compañias
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function ListBranch(ByVal Company As String) As XPInstantFeedbackSource
        Dim ObjectionD As New MaintenanceServicesXpo(Company)
        Return ObjectionD.GetBranch()
    End Function



    ''' <summary>
    ''' Lista de compañias
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function ListBrand(ByVal Company As String) As XPInstantFeedbackSource
        Dim ObjectionD As New MaintenanceServicesXpo(Company)
        Return ObjectionD.GetBrand()
    End Function
    ''' <summary>
    ''' Lista los tipos de equipo
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function ListEquipmentType(ByVal Company As String) As XPInstantFeedbackSource
        Dim ObjectionD As New MaintenanceServicesXpo(Company)
        Return ObjectionD.GetEquipmentType()
    End Function


    ''' <summary>
    ''' Lista los tipos de equipo
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function ListInsurance(ByVal Company As String) As XPInstantFeedbackSource
        Dim ObjectionD As New MaintenanceServicesXpo(Company)
        Return ObjectionD.GetInsurance()
    End Function



    ''' <summary>
    ''' Lista los tipos de equipo
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function ListInventoryType(ByVal CompanyAux As String) As XPInstantFeedbackSource
        Dim ObjectionD As New MaintenanceServicesXpo(CompanyAux)
        Return ObjectionD.GetInventoryType()
    End Function



    ''' <summary>
    ''' Lista los tipos de proveedores
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function ListSupplier(ByVal Company As String) As XPInstantFeedbackSource
        Dim ObjectionD As New MaintenanceServicesXpo(Company)
        Return ObjectionD.GetSupplier()
    End Function

    ''' <summary>
    ''' Lista los tipos de proveedores
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function ListSupplierByStatus(status As Boolean, ByVal Company As String) As XPInstantFeedbackSource
        Dim ObjectionD As New MaintenanceServicesXpo(Company)
        Return ObjectionD.ListSupplierByStatus(status)
    End Function

    ''' <summary>
    ''' Lista los tipos de poliza
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function ListPoliza(ByVal Company As String) As XPInstantFeedbackSource
        Dim ObjectionD As New MaintenanceServicesXpo(Company)
        Return ObjectionD.GetPoliza()
    End Function



    ''' <summary>
    ''' Lista los tipos de poliza
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function ListPolizaType(ByVal Company As String) As XPInstantFeedbackSource
        Dim ObjectionD As New MaintenanceServicesXpo(Company)
        Return ObjectionD.GetPolizaType()
    End Function

    ''' <summary>
    ''' Lista todos los empleados
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function ListEmployee(ByVal Company As String) As LinqInstantFeedbackSource
        Dim objEmployee As New PayrollServicesXpo(Company)
        Return objEmployee.GetEmployee()
    End Function

    ''' <summary>
    ''' Lista todos los empleados
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function ListEmployeeByFunctionalUnitIdOrGroupId(ByVal Company As String, Optional functionalUnitId As Integer = 0, Optional groupId As Integer = 0) As LinqInstantFeedbackSource
        Dim objEmployee As New PayrollServicesXpo(Company)
        Return objEmployee.GetEmployeeByFunctionalUnitIdOrGroupId(functionalUnitId, groupId)
    End Function

    ''' <summary>
    ''' Lista las incapacidades de un empleado y filtra si esta liquidadas o no
    ''' </summary>
    ''' <param name="employeeId">id del empleado</param>
    ''' <param name="status">Estado de la novedad</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function ListInabilityLiquidate(employeeId As Integer, status As Integer, ByVal Company As String) As XPInstantFeedbackSource
        Dim objEmployee As New PayrollServicesXpo(Company)
        Return objEmployee.GetInabilityLiquidate(employeeId, status)
    End Function



    ''' <summary>
    ''' Lista los accesorios
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function ListAccesory(ByVal Company As String) As XPInstantFeedbackSource
        Dim objEmployee As New MaintenanceServicesXpo(Company)
        Return objEmployee.GetAccesory()
    End Function


    ''' <summary>
    ''' Lista los consumibles
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function ListConsumible(ByVal Company As String) As XPInstantFeedbackSource
        Dim objEmployee As New MaintenanceServicesXpo(Company)
        Return objEmployee.GetConsumible()
    End Function


    ''' <summary>
    ''' Lista las torres
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function ListTower(ByVal Company As String) As XPInstantFeedbackSource
        Dim objEmployee As New MaintenanceServicesXpo(Company)
        Return objEmployee.GetTower()
    End Function

    ''' <summary>
    ''' Lista los pisos
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function ListFloor(ByVal Company As String) As XPInstantFeedbackSource
        Dim objEmployee As New MaintenanceServicesXpo(Company)
        Return objEmployee.GetFloor()
    End Function


    ''' <summary>
    ''' Lista las areas
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function ListArea(ByVal Company As String) As XPInstantFeedbackSource
        Dim objEmployee As New MaintenanceServicesXpo(Company)
        Return objEmployee.GetArea()
    End Function



    ''' <summary>
    ''' Lista las habitaciones
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function ListRoom(ByVal Company As String) As XPInstantFeedbackSource
        Dim objEmployee As New MaintenanceServicesXpo(Company)
        Return objEmployee.GetRoom()
    End Function



    ''' <summary>
    ''' Lista las equipo
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function ListEquipment(ByVal Company As String) As XPInstantFeedbackSource
        Dim objEmployee As New MaintenanceServicesXpo(Company)
        Return objEmployee.GetEquipment()
    End Function



    ''' <summary>
    ''' Lista las partes
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function ListPart(ByVal Company As String) As XPInstantFeedbackSource
        Dim objEmployee As New MaintenanceServicesXpo(Company)
        Return objEmployee.GetPart()
    End Function


    ''' <summary>
    ''' Lista las recepciones e los equipos
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function ListEquipmentReception(ByVal Company As String) As XPInstantFeedbackSource
        Dim objEmployee As New MaintenanceServicesXpo(Company)
        Return objEmployee.GetEquipmentReception()
    End Function



    ''' <summary>
    ''' lista las unidades de medida
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function ListMeasurementUnit(ByVal Company As String) As XPInstantFeedbackSource
        Dim objEmployee As New MaintenanceServicesXpo(Company)
        Return objEmployee.GetMeasurementUnit()
    End Function

    ''' <summary>
    ''' lista los registros tecnicos
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function ListTechnicalLog(ByVal Company As String) As XPInstantFeedbackSource
        Dim objEmployee As New MaintenanceServicesXpo(Company)
        Return objEmployee.GetTechnicalLog()
    End Function


    ''' <summary>
    ''' lista los registros responsables
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function ListResponsibleMaintenance(ByVal Company As String) As XPInstantFeedbackSource
        Dim objEmployee As New MaintenanceServicesXpo(Company)
        Return objEmployee.GetResponsible()
    End Function

    ''' <summary>
    ''' lista los registros responsables
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function ListTemplateEquipmentType(ByVal Company As String) As XPInstantFeedbackSource
        Dim objEmployee As New MaintenanceServicesXpo(Company)
        Return objEmployee.GetTemplateEquipmentType()
    End Function

    ''' <summary>
    ''' Lista las novedades de un empleado y filtra por tipo de novedad
    ''' </summary>
    ''' <param name="employeeId"></param>
    ''' <param name="typeNovelty"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function ListNoveltyLiquidateByType(employeeId As Integer, typeNovelty As Byte, ByVal Company As String) As XPInstantFeedbackSource
        Dim objNovelty As New PayrollServicesXpo(Company)
        Return objNovelty.GetNoveltyLiquidateByType(employeeId, typeNovelty)
    End Function

    ''' <summary>
    ''' Lista los registros de equipo
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function ListRegisterEquipment(ByVal Company As String) As XPInstantFeedbackSource
        Dim Maintenance As New MaintenanceServicesXpo(Company)
        Return Maintenance.GetRegisterEquipment()
    End Function

    ''' <summary>
    ''' Lista todos los tipos de empleado
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function ListEmployeeType(ByVal Company As String) As XPInstantFeedbackSource
        Dim payroll As New PayrollServicesXpo(Company)
        Return payroll.GetEmployeeType()
    End Function

    ''' <summary>
    ''' Lista todos las razones de retiro
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function ListContractModificationReason(ByVal Company As String) As XPInstantFeedbackSource
        Dim payroll As New PayrollServicesXpo(Company)
        Return payroll.GetContractModificationReason()
    End Function

    ''' <summary>
    ''' Lisa los grupos filtrado por la clase de contrato
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function ListGroupByContractClass(contractClass As Byte, ByVal Company As String) As XPInstantFeedbackSource
        Dim payroll As New PayrollServicesXpo(Company)
        Return payroll.GetGroupByContractClass(contractClass)
    End Function

    ''' <summary>
    ''' Lisa las unidades funcionales por empresa
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function ListFunctionalUnitByCompany(companyId As Integer, ByVal CompanyAux As String) As XPInstantFeedbackSource
        Dim payroll As New PayrollServicesXpo(CompanyAux)
        Return payroll.GetFunctionalUnitByCompany(companyId)
    End Function

    ''' <summary>
    ''' Lista Todas las Estructuras Contables de Nómina
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function ListAccountingStructure(ByVal Company As String) As XPInstantFeedbackSource
        Dim accountingStructure As New PayrollServicesXpo(Company)
        Return accountingStructure.GetAccountingStructure()
    End Function
#Region "DocumentalSystem"

    ''' <summary>
    ''' Lista los contenedores de archivos
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function ListFileContainers(ByVal Company As String) As XPInstantFeedbackSource
        Dim fileContainer As New DocumentalSystemServicesXpo(Company)
        Return fileContainer.GetFileContainer
    End Function

#End Region

    ''' <summary>
    ''' Lisa los tipos de contratos por clase de contrato
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function ListContractTypeByContractClass(Company As String, ParamArray classContract() As String) As XPInstantFeedbackSource
        Dim payroll As New PayrollServicesXpo(Company)
        Return payroll.GetContractTypeByContractClass(classContract)
    End Function

    ''' <summary>
    ''' Lista todos los empleados sin importar si esta inactivo o activo el contrato
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function ListAllEmployees(Company As String) As LinqInstantFeedbackSource
        Dim payroll As New PayrollServicesXpo(Company)
        Return payroll.GetAllEmployees()
    End Function

    ''' <summary>
    ''' Lista todos los empleados que tengan contrato activo para liquidar
    ''' </summary>
    ''' <param name="company"></param>
    Public Shared Function ListAllEmployeesWithContractToLiquidate(company As String) As LinqInstantFeedbackSource
        Dim payroll As New PayrollServicesXpo(company)
        Return payroll.GetAllEmployeesWithContratToLiquidate
    End Function

    ''' <summary>
    ''' Lista todos los empleados sin importar si esta inactivo o activo el contrato
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function GetTransferJuridicalCList(Company As String) As LinqInstantFeedbackSource
        Dim glosas = New GlosasServicesXpo(Company)
        Return glosas.GetTransferJuridicalCList
    End Function

    ''' <summary>
    ''' Lista todos los registros de un detalle de auditoria
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function GetAuditList(IdAudit As String, entityName As String, Company As String, ByVal container As String) As XPServerCollectionSource
        Dim audit = New SecurityServicesXpo(container)
        Return audit.GetAuditList(IdAudit, entityName, Company)
    End Function

    ''' <summary>
    ''' Lista todos los registros de auditoria cabecera según parametros.
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function GetAuditC(entityName As String, Company As String, IdForm As String, IdEntity As String, ByVal container As String) As XPInstantFeedbackSource
        Dim audit = New SecurityServicesXpo(container)
        Return audit.GetAuditC(entityName, Company, IdForm, IdEntity)
    End Function

    ''' <summary>
    ''' Lista todos los registros eliminados de auditoria cabecera según parametros.
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function GetAuditCDelete(entityName As String, Company As String, IdForm As String, ByVal container As String) As XPInstantFeedbackSource
        Dim audit = New SecurityServicesXpo(container)
        Return audit.GetAuditCDelete(entityName, Company, IdForm)
    End Function

    ''' <summary>
    ''' Lista la auditoria basica según formulario y id registro
    ''' </summary>
    ''' <param name="Month"></param>
    ''' <param name="Year"></param>
    ''' <param name="IdForm"></param>
    ''' <param name="IdEntity"></param>
    ''' <param name="Company"></param>
    ''' <returns></returns>
    Public Shared Function listAuditBasic(Month As String, Year As String, IdForm As String, IdEntity As String, Company As String, ByVal container As String) As XPInstantFeedbackSource
        Dim audit = New SecurityServicesXpo(container)
        Return audit.ListBasicAudit(Month, Year, IdForm, IdEntity, Company)
    End Function

    ''' <summary>
    ''' Lista la auditoria basica de los reportes según formulario y id registro
    ''' </summary>
    ''' <param name="Month"></param>
    ''' <param name="Year"></param>
    ''' <param name="IdForm"></param>
    ''' <param name="IdEntity"></param>
    ''' <param name="Company"></param>
    ''' <returns></returns>
    Public Shared Function listAuditBasicReports(Month As String, Year As String, IdForm As String, IdEntity As String, Company As String, ByVal container As String) As XPInstantFeedbackSource
        Dim audit = New SecurityServicesXpo(container)
        Return audit.ListBasicAuditReports(Month, Year, IdForm, IdEntity, Company)
    End Function

    ''' <summary>
    ''' Lista la auditoria basica eliminada según formulario 
    ''' </summary>
    ''' <param name="Month"></param>
    ''' <param name="Year"></param>
    ''' <param name="IdForm"></param>
    ''' <param name="Company"></param>
    ''' <returns></returns>
    Public Shared Function listAuditBasicDelete(Month As String, Year As String, IdForm As String, Company As String, ByVal container As String) As XPInstantFeedbackSource
        Dim audit = New SecurityServicesXpo(container)
        Return audit.ListBasicAuditDelete(Month, Year, IdForm, Company)
    End Function

    ''' <summary>
    ''' Lista todas las clases de convenios
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function ListKindsAgreements(Company As String) As XPInstantFeedbackSource
        Dim payroll As New PayrollServicesXpo(Company)
        Return payroll.ListKindsAgreements()
    End Function

    ''' <summary>
    ''' Lista los convenios
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function ListAgreements(Company As String) As XPInstantFeedbackSource
        Dim payroll As New PayrollServicesXpo(Company)
        Return payroll.ListAgreements()
    End Function

    ''' <summary>
    ''' Lista los conceptos manuales
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function ListManualConcepts(Company As String) As XPInstantFeedbackSource
        Dim payroll As New PayrollServicesXpo(Company)
        Return payroll.ListManualConcepts()
    End Function
    ''' <summary>
    ''' Lista de Parametros interfaces
    ''' </summary>
    ''' <param name="Company"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function GetParameterInterface(Company As String) As XPInstantFeedbackSource
        Dim Glosas As New GlosasServicesXpo(Company)
        Return Glosas.GetParameterInterface()
    End Function

    ''' <summary>
    ''' Lista las ubicaciones de mantenimiento
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function ListLocationMaintenance(Company As String) As XPInstantFeedbackSource
        Dim maint As New MaintenanceServicesXpo(Company)
        Return maint.GetLocation
    End Function

    ''' <summary>
    ''' Lista los tipos de equipos
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function ListEquipmentTypeMaintenance(Company As String) As XPInstantFeedbackSource
        Dim maint As New MaintenanceServicesXpo(Company)
        Return maint.GetEquipmentType
    End Function

    ''' <summary>
    ''' Obtiene una unidad funcional por id
    ''' </summary>
    ''' <param name="Company"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function GetFunctionalUnitById(functionalUnitId As Integer, Company As String) As XPCollection(Of PayrollFunctionalUnit)
        Dim payroll As New PayrollServicesXpo(Company)
        Return payroll.GetFunctionalUnitById(functionalUnitId)
    End Function

    ''' <summary>
    ''' Obtiene una unidad funcional por id
    ''' </summary>
    ''' <param name="Company"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function GetLiquidationPayroll(Company As String, ByVal IdGroup As Integer, ByVal PayrollDateLiquidated As Date) As XPCollection
        Dim payroll As New PayrollServicesXpo(Company)
        Return payroll.GetLiquidationPayroll(IdGroup, PayrollDateLiquidated)
    End Function

#End Region

#Region "Payroll"

    ''' <summary>
    ''' Lista los sindicatos para el formulario
    ''' </summary>
    ''' <param name="company">Empresa a la que se encuentra conectado</param>
    Public Shared Function ListTradeUnion(ByVal company As String) As XPInstantFeedbackSource
        Dim service As New PayrollServicesXpo(company)
        Return service.ListTradeUnion()
    End Function

    ''' <summary>
    ''' Lista los sindicatos por estado
    ''' </summary>
    ''' <param name="company">Empresa a la que se encuentra conectado</param>
    Public Shared Function ListTradeUnionByStatus(ByVal company As String, ByVal status As Boolean) As XPInstantFeedbackSource
        Dim service As New PayrollServicesXpo(company)
        Return service.ListTradeUnionByStatus(status)
    End Function

    ''' <summary>
    ''' Lista las liquidaciones por periodo
    ''' </summary>
    ''' <param name="company">Empresa a la que se encuentra conectado</param>
    Public Shared Function ListLiquidationByYearMont(ByVal company As String, ByVal year As Integer, ByVal month As Integer) As XPInstantFeedbackSource
        Dim service As New PayrollServicesXpo(company)
        Return service.ListLiquidationByYearMont(year, month)
    End Function

    ''' <summary>
    ''' Lista los grupos atencion por estado
    ''' </summary>
    ''' <param name="company">Empresa a la que se encuentra conectado</param>
    Public Shared Function ListFunctionalUnit(ByVal company As String, ByVal status As Boolean) As XPInstantFeedbackSource
        Dim service As New PayrollServicesXpo(company)
        Return service.ListFunctionalUnit(status)
    End Function

    ''' <summary>
    ''' Lista los grupos atencion por estado
    ''' </summary>
    ''' <param name="company">Empresa a la que se encuentra conectado</param>
    Public Shared Function ListFunctionalUnitUserAuthorized(ByVal company As String, userCode As String) As XPInstantFeedbackSource
        Dim service As New PayrollServicesXpo(company)
        Return service.ListFunctionalUnitUserAuthorized(userCode)
    End Function

    ''' <summary>
    ''' Lista los conceptos por estado y clase
    ''' </summary>
    ''' <param name="company">Empresa a la que se encuentra conectado</param>
    Public Shared Function ListConceptsPayroll(ByVal company As String, ByVal status As Boolean, ByVal conceptClass As String) As XPInstantFeedbackSource
        Dim service As New PayrollServicesXpo(company)
        Return service.ListConceptsPayroll(status, conceptClass)
    End Function

    Public Shared Function ListFunctionalUnitByUnitTypeAndUserAuthorized(company As String, unitTypes As String, userCode As String) As XPInstantFeedbackSource
        Dim service As New PayrollServicesXpo(company)
        Return service.ListFunctionalUnitByUnitTypeAndUserAuthorized(unitTypes, userCode)
    End Function
    ''' <summary>
    ''' Lista los grupos atencion por estado
    ''' </summary>
    ''' <param name="company">Empresa a la que se encuentra conectado</param>
    Public Shared Function ListLiquidationPayroll(ByVal company As String, ByVal GroupId As String, ByVal PayrollDateLiquidated As Date) As XPCollection
        Dim service As New PayrollServicesXpo(company)
        Return service.GetLiquidationPayroll(GroupId, PayrollDateLiquidated)
    End Function

    ''' <summary>
    ''' Lista todos los empleados con el contrato activo
    ''' </summary>
    Public Shared Function ListEmployeeWithActiveContract(ByVal company As String) As XPInstantFeedbackSource
        Dim service As New PayrollServicesXpo(company)
        Return service.ListEmployeeWithActiveContract()
    End Function

#End Region

#Region "Portfolio"
    ''' <summary>
    ''' obtiene una factura por id
    ''' </summary>
    ''' <param name="id"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function GetPortfolioAccountReceivableXpo(Company As String, id As Integer) As Infrastructure.Data.Xpo.PortfolioRepository.PortfolioAccountReceivableXpo
        Dim service As New PortfolioServicesXpo(Company)
        Return service.GetPortfolioAccountReceivableXpo(id)
    End Function

    ''' <summary>
    ''' lista los detalles del saldo inicial
    ''' </summary>
    ''' <param name="PortfolioInitialBalanceId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function ListViewBudgetAllocationInitialBalances(Company As String, PortfolioInitialBalanceId As Integer) As XPCollection
        Dim service As New PortfolioServicesXpo(Company)
        Return service.ListViewBudgetAllocationInitialBalances(PortfolioInitialBalanceId)
    End Function

    ''' <summary>
    ''' Lista los detalles del traslado por el id de la cabecera del traslado
    ''' </summary>
    ''' <param name="PortfolioTransferId">Id de la cabecera del traslado</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function ListPortfolioTransferDetailByPortfolioTransferId(Company As String, PortfolioTransferId As Integer) As XPCollection
        Dim service As New PortfolioServicesXpo(Company)
        Return service.ListPortfolioTransferDetailByPortfolioTransferId(PortfolioTransferId)
    End Function

#Region "Reports"

    ''' <summary>
    ''' Función para obtener una lista de datos de una entidad XPO
    ''' </summary>
    ''' <typeparam name="T">Objeto XPO</typeparam>
    ''' <param name="Company">Compañia Actual</param>
    ''' <param name="Fun">Objeto tipo Función opcional para filtrar los datos necesarios</param>
    ''' <returns>Objeto</returns>
    Public Shared Function ListCollectionPortfolioReport(Of T)(ByVal Company As String, Optional Fun As Func(Of T, Boolean) = Nothing, Optional criteria As String = Nothing) As Object
        Dim Portfolio As New PortfolioServicesXpo(Company)
        Return Portfolio.GetCollection(Of T)(Fun, criteria)
    End Function

#End Region

    ''' <summary>
    ''' lista las facturas radicadas
    ''' </summary>
    ''' <param name="company">Empresa a la que se encuentra conectado</param>
    ''' <returns>Lista de tipos de documento</returns>
    Public Shared Function ListVReportListRadicatedInvoice(ByVal company As String, filtro As String) As XPCollection(Of PortfolioVReportListRadicatedInvoiceXpo)
        Dim service As New PortfolioServicesXpo(company)
        Return service.ListVReportListRadicatedInvoice(filtro)
    End Function

    ''' <summary>
    ''' lista las facturas para la cartera por edades
    ''' </summary>
    ''' <param name="company">Empresa a la que se encuentra conectado</param>
    ''' <returns>Lista de tipos de documento</returns>
    Public Shared Function ListVReportPortfolioByAge(ByVal company As String, filtro As String) As XPCollection(Of VReportPortfolioByAge)
        Dim service As New PortfolioServicesXpo(company)
        Return service.ListVReportPortfolioByAge(filtro)
    End Function

    ''' <summary>
    ''' Función para obtener una lista de datos de una entidad XPO
    ''' </summary>
    ''' <param name="Company">Compañia Actual</param>
    ''' <returns>Objeto</returns>
    Public Shared Function ListCollectionCashReceipts(ByVal Company As String, ByVal INDFechaIni As Date, ByVal INDFechaEnd As Date, ByVal INDThirdPartyStart As String, ByVal INDThirdPartyEnd As String, ByVal INDBankAccountStart As String, ByVal INDBankAccountEnd As String) As Object
        Dim service As New PortfolioServicesXpo(Company)
        Return service.GetCollectionCashReceipts(INDFechaIni, INDFechaEnd, INDThirdPartyStart, INDThirdPartyEnd, INDBankAccountStart, INDBankAccountEnd)
    End Function

    ''' <summary>
    ''' Gets all portfolio SuretyFile.
    ''' </summary>
    ''' <param name="Company">The company.</param>
    ''' <returns></returns>
    Public Shared Function GetAllVReportSuretyFileXpo(Company As String, filtro As String) As XPCollection(Of VReportSuretyFileXpo)
        Dim Portfolio As New PortfolioServicesXpo(Company)
        Return Portfolio.GetAllVReportSuretyFileXpo(filtro)
    End Function

    ''' <summary>
    ''' Lista todas las notas de cartera por filtro
    ''' </summary>
    ''' <param name="company">The company.</param>
    ''' <returns></returns>
    Public Shared Function ListNoteReportPortfolioByFilter(ByVal company As String, ByVal Filtro As String) As XPInstantFeedbackSource
        Dim service As New PortfolioServicesXpo(company)
        Return service.ListNoteReportPortfolioByFilter(Filtro)
    End Function

    ''' <summary>
    ''' Lista los radicados de portfolio
    ''' </summary>
    ''' <param name="company">The company.</param>
    ''' <returns></returns>
    Public Shared Function ListRadicateInvoice(ByVal company As String, ByVal Filtro As String) As XPInstantFeedbackSource
        Dim service As New PortfolioServicesXpo(company)
        Return service.ListRadicateInvoice(Filtro)
    End Function

    ''' <summary>
    ''' Lista todas los grupos de atención
    ''' </summary>
    ''' <param name="company">The company.</param>
    ''' <returns></returns>
    Public Shared Function ListCareGroupReportPortfolio(ByVal company As String) As XPInstantFeedbackSource
        Dim service As New PortfolioServicesXpo(company)
        Return service.ListCareGroupReportPortfolio()
    End Function

    ''' <summary>
    ''' Lista todas las cuentas contables
    ''' </summary>
    ''' <param name="company">The company.</param>
    ''' <returns></returns>
    Public Shared Function ListMainAccountsReportPortfolio(ByVal company As String) As XPInstantFeedbackSource
        Dim service As New PortfolioServicesXpo(company)
        Return service.ListMainAccountsReportPortfolio()
    End Function

    ''' <summary>
    ''' Lista todas las notas de cartera
    ''' </summary>
    ''' <param name="company">The company.</param>
    ''' <returns></returns>
    Public Shared Function ListNoteReportPortfolio(ByVal company As String) As XPInstantFeedbackSource
        Dim service As New PortfolioServicesXpo(company)
        Return service.ListNoteReportPortfolio()
    End Function

    ''' <summary>
    ''' Lista todas las notas
    ''' </summary>
    ''' <param name="company">The company.</param>
    ''' <returns></returns>
    Public Shared Function ListThirdPartyReportPortfolio(ByVal company As String) As XPInstantFeedbackSource
        Dim service As New PortfolioServicesXpo(company)
        Return service.ListThirdPartyReportPortfolio()
    End Function

    ''' <summary>
    ''' Lista todas las Cuentas Bancarias
    ''' </summary>
    ''' <param name="company">The company.</param>
    ''' <returns></returns>
    Public Shared Function ListAccountBankReportPortfolio(ByVal company As String) As XPInstantFeedbackSource
        Dim service As New PortfolioServicesXpo(company)
        Return service.ListAccountBankReportPortfolio()
    End Function

    ''' <summary>
    ''' Lista todos los Clientes
    ''' </summary>
    ''' <param name="company">The company.</param>
    ''' <returns></returns>
    Public Shared Function ListCustomerReportPortfolio(ByVal company As String) As XPInstantFeedbackSource
        Dim service As New PortfolioServicesXpo(company)
        Return service.ListCustomerReportPortfolio()
    End Function

    ''' <summary>
    ''' Lista todos los Vendedores
    ''' </summary>
    ''' <param name="company">The company.</param>
    ''' <returns></returns>
    Public Shared Function ListSellerReportPortfolio(ByVal company As String) As XPInstantFeedbackSource
        Dim service As New PortfolioServicesXpo(company)
        Return service.ListSellerReportPortfolio()
    End Function

    ''' <summary>
    ''' Lista todos los Facturas
    ''' </summary>
    ''' <param name="company">The company.</param>
    ''' <returns></returns>
    Public Shared Function ListInvoiceReportPortfolio(ByVal company As String) As XPInstantFeedbackSource
        Dim service As New PortfolioServicesXpo(company)
        Return service.ListInvoiceReportPortfolio()
    End Function

    ''' <summary>
    ''' lista los detalles del salfo inicial de las cuentas
    ''' </summary>
    ''' <param name="company"></param>
    ''' <param name="portfolioInitialBalanceAccountReceivableId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function ListPortfolioInitialBalanceAccountReceivableAccounting(company As String, portfolioInitialBalanceAccountReceivableId As Integer) As XPInstantFeedbackSource
        Dim service As New PortfolioServicesXpo(company)
        Return service.ListPortfolioInitialBalanceAccountReceivableAccounting(portfolioInitialBalanceAccountReceivableId)
    End Function
    ''' <summary>
    ''' lista los detalles de las cuentas donde esta la factura
    ''' </summary>
    Public Shared Function ListAccountReceivableAccounting(company As String, AccountReceivableId As Integer) As XPInstantFeedbackSource
        Dim service As New PortfolioServicesXpo(company)
        Return service.ListAccountReceivableAccounting(AccountReceivableId)
    End Function

    ''' <summary>
    ''' lista los detalles del salfo inicial de las facturas
    ''' </summary>
    ''' <param name="company"></param>
    ''' <param name="portfolioInitialBalanceAccountReceivableId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function ListPortfolioInitialBalanceAccountReceivableShare(company As String, portfolioInitialBalanceAccountReceivableId As Integer) As XPInstantFeedbackSource
        Dim service As New PortfolioServicesXpo(company)
        Return service.ListPortfolioInitialBalanceAccountReceivableShare(portfolioInitialBalanceAccountReceivableId)
    End Function

    ''' <summary>
    ''' lista los detalles del salfo inicial
    ''' </summary>
    ''' <param name="company"></param>
    ''' <param name="portfolioInitialBalanceId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function ListPortfolioInitialBalanceAccountReceivable(company As String, portfolioInitialBalanceId As Integer) As XPInstantFeedbackSource
        Dim service As New PortfolioServicesXpo(company)
        Return service.ListPortfolioInitialBalanceAccountReceivable(portfolioInitialBalanceId)
    End Function

    ''' <summary>
    ''' Lista todos los traslados
    ''' </summary>
    ''' <param name="company">The company.</param>
    ''' <returns></returns>
    Public Shared Function ListPortfolioTransferReport(ByVal company As String) As XPInstantFeedbackSource
        Dim service As New PortfolioServicesXpo(company)
        Return service.ListPortfolioTransferReport()
    End Function

    ''' <summary>
    ''' Gets the portfolio advance by third party identifier and admission.
    ''' </summary>
    ''' <param name="company">The company.</param>
    ''' <param name="thirdId">The third identifier.</param>
    ''' <param name="admission">The admission.</param>
    ''' <returns></returns>
    Public Shared Function GetPortfolioAdvanceByThirdPartyIdAndAdmission(ByVal company As String, thirdId As Integer, admission As String) As XPInstantFeedbackSource
        Dim service As New PortfolioServicesXpo(company)
        Return service.GetPortfolioAdvanceByThirdPartyIdAndAdmission(thirdId, admission)
    End Function

    ''' <summary>
    ''' obtiene todos los documentos de cuentas x cobrar
    ''' </summary>
    ''' <param name="company"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function GetAllAccountReceivableDocument(company As String) As XPInstantFeedbackSource
        Dim portfolio As New PortfolioServicesXpo(company)
        Return portfolio.GetAllAccountReceivableDocument()
    End Function
    ''' <summary>
    ''' obtiene un listado de los documentos reclasificados
    ''' </summary>
    ''' <param name="company"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function ListPortfolioReclassification(company As String) As XPInstantFeedbackSource
        Dim portfolio As New PortfolioServicesXpo(company)
        Return portfolio.ListPortfolioReclassification()
    End Function

    ''' <summary>
    ''' obtiene un listado de los documentos reclasificados
    ''' </summary>
    ''' <param name="company"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function ListPortfolioReclassificationDetail(company As String, code As String) As XPInstantFeedbackSource
        Dim portfolio As New PortfolioServicesXpo(company)
        Return portfolio.ListPortfolioReclassificationDetail(code)
    End Function
    ''' <summary>
    ''' obtiene todos los traslados
    ''' </summary>
    ''' <param name="company"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function ListPortfolioTransfers(company As String) As XPInstantFeedbackSource
        Dim portfolio As New PortfolioServicesXpo(company)
        Return portfolio.ListPortfolioTransfers()
    End Function
    ''' <summary>
    ''' lista los anticipos del tercero para traslados
    ''' </summary>
    ''' <param name="company"></param>
    ''' <param name="ThirdPartyId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function ListAdvanceTransfers(company As String, ThirdPartyId As Integer) As XPInstantFeedbackSource
        Dim portfolio As New PortfolioServicesXpo(company)
        Return portfolio.ListAdvanceTransfers(ThirdPartyId)
    End Function
    ''' <summary>
    ''' lista las facturas del tercero para traslados
    ''' </summary>
    ''' <param name="company"></param>
    ''' <param name="ThirdPartyId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function ListBillsTransfers(company As String, ThirdPartyId As Integer) As XPInstantFeedbackSource
        Dim portfolio As New PortfolioServicesXpo(company)
        Return portfolio.ListBillsTransfers(ThirdPartyId)
    End Function
    ''' <summary>
    ''' lista las facturas del cliente para traslados
    ''' </summary>
    ''' <param name="company"></param>
    ''' <param name="Filtro"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function ListInvoiceReportPortfolioFilter(company As String, Filtro As String) As XPInstantFeedbackSource
        Dim portfolio As New PortfolioServicesXpo(company)
        Return portfolio.ListInvoiceReportPortfolioFilter(Filtro)
    End Function
    ''' <summary>
    ''' lista los anticipos del tercero para extracto de cartera
    ''' </summary>
    ''' <param name="company"></param>
    ''' <param name="Filtro"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function ListAdvancesReportPortfolioFilter(company As String, Filtro As String) As XPInstantFeedbackSource
        Dim portfolio As New PortfolioServicesXpo(company)
        Return portfolio.ListAdvancesReportPortfolioFilter(Filtro)
    End Function
    ''' <summary>
    ''' lista los traslados por filtro
    ''' </summary>
    ''' <param name="company"></param>
    ''' <param name="Filtro"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function ListPortfolioTransferReportFilter(company As String, Filtro As String) As XPInstantFeedbackSource
        Dim portfolio As New PortfolioServicesXpo(company)
        Return portfolio.ListPortfolioTransferReportFilter(Filtro)
    End Function
    ''' <summary>
    ''' Lists the bills transfers.
    ''' </summary>
    ''' <returns></returns>
    Public Shared Function ListAccountRecivableAccountByThirdIdState(company As String, thirdId As Integer, state As Byte) As LinqInstantFeedbackSource
        Dim portfolio As New PortfolioServicesXpo(company)
        Return portfolio.ListAccountRecivableAccountByThirdIdState(thirdId, state)
    End Function
    ''' <summary>
    ''' metodo para listar las facturas que se utilizaran en recibos de caja
    ''' </summary>
    ''' <param name="company"></param>
    ''' <param name="idThirdParty"></param>
    ''' <param name="idMainAccount"></param>
    ''' <param name="idCostCenter"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function ListPortfolioAccountReceivableByCashReceipt(company As String, idThirdParty As Integer, idMainAccount As Integer, idCostCenter As Integer) As XPInstantFeedbackSource
        Dim portfolio As New PortfolioServicesXpo(company)
        Return portfolio.ListPortfolioAccountReceivableByCashReceipt(idThirdParty, idMainAccount, idCostCenter)
    End Function
    ''' <summary>
    ''' Lists the state of the account recivable account by.
    ''' </summary>
    ''' <returns></returns>
    ''' <exception cref="System.NotImplementedException"></exception>
    Public Shared Function ListAccountRecivableAccountByState(company As String, state As Integer) As LinqInstantFeedbackSource
        Dim portfolio As New PortfolioServicesXpo(company)
        Return portfolio.ListAccountRecivableAccountByState(state)
    End Function
    ''' <summary>
    ''' obtiene todos los saldos iniciales
    ''' </summary>
    ''' <param name="Company"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function GetAllInitialBalance(Company As String) As XPInstantFeedbackSource
        Dim Portfolio As New PortfolioServicesXpo(Company)
        Return Portfolio.GetAllInitialBalance()
    End Function

    ''' <summary>
    ''' Metodo para obtener todos los saldos iniciales por estado
    ''' </summary>
    ''' <returns></returns>
    Public Shared Function GetInitialBalanceByStatus(Company As String, status As Integer) As XPInstantFeedbackSource
        Dim Portfolio As New PortfolioServicesXpo(Company)
        Return Portfolio.GetInitialBalanceByStatus(status)
    End Function
    ''' <summary>
    ''' Lista de indicadores economicos
    ''' </summary>
    ''' <param name="Company"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function GetAllEconomicIndicator(Company As String) As XPInstantFeedbackSource
        Dim Portfolio As New PortfolioServicesXpo(Company)
        Return Portfolio.GetAllEconomicIndicator()
    End Function

    ''' <summary>
    ''' Gets all portfolio advance by third identifier.
    ''' </summary>
    ''' <param name="ThirdId">The third identifier.</param>
    ''' <param name="Company">The company.</param>
    ''' <returns></returns>
    Public Shared Function GetAllPortfolioAdvanceByThirdId(Company As String, ThirdId As Integer) As XPCollection
        Dim Portfolio As New PortfolioServicesXpo(Company)
        Return Portfolio.GetAllPortfolioAdvanceByThirdId(ThirdId)
    End Function

    ''' <summary>
    ''' Gets all portfolio Extract.
    ''' </summary>
    ''' <param name="Company">The company.</param>
    ''' <returns></returns>
    Public Shared Function GetAllVReportExtractAccountReceivableXpo(Company As String, filtro As String) As XPCollection(Of VReportExtractAccountReceivableXpo)
        Dim Portfolio As New PortfolioServicesXpo(Company)
        Return Portfolio.GetAllVReportExtractAccountReceivableXpo(filtro)
    End Function

    ''' <summary>
    ''' Lista de conceptos de cuentas por pagar
    ''' </summary>
    ''' <param name="Company"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function GetAllPortfolioConcept(Company As String) As XPInstantFeedbackSource
        Dim Portfolio As New PortfolioServicesXpo(Company)
        Return Portfolio.GetAllPortfolioConcept()
    End Function

    ''' <summary>
    ''' Lista de conceptos de cuentas por pagar
    ''' </summary>
    ''' <param name="Company"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function GetAllPortfolioNoteConcept(Company As String) As XPInstantFeedbackSource
        Dim Portfolio As New PortfolioServicesXpo(Company)
        Return Portfolio.GetAllPortfolioNoteConcept()
    End Function
    ''' <summary>
    ''' Lista de rangos de provision
    ''' </summary>
    ''' <param name="Company"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function GetAllProvisionRanges(Company As String) As XPInstantFeedbackSource
        Dim Portfolio As New PortfolioServicesXpo(Company)
        Return Portfolio.GetAllProvisionRanges()
    End Function

    ''' <summary>
    ''' Lista todas las facutas con un in facturas
    ''' </summary>
    ''' <param name="Company"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function ListPortfolioAccountReceivableValidation(invoiceNumbers As List(Of String), Company As String) As XPCollection
        Dim Portfolio As New PortfolioServicesXpo(Company)
        Return Portfolio.ListPortfolioAccountReceivableValidation(invoiceNumbers)
    End Function

    ''' <summary>
    ''' metodo para listar las facturas para las notas de cuentas por cobrar
    ''' </summary>
    ''' <param name="company"></param>
    ''' <param name="customerId"></param>
    ''' <param name="noteType"></param>
    ''' <param name="nature"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function ListBillsPortfolioNote(company As String, customerId As Integer, noteType As Integer, nature As Integer) As XPInstantFeedbackSource
        Dim Portfolio As New PortfolioServicesXpo(company)
        Return Portfolio.ListBillsPortfolioNote(customerId, noteType, nature)
    End Function
    ''' <summary>
    ''' metodo para listar las facturas para las notas de cuentas por cobrar filtradas por tercero
    ''' </summary>
    ''' <param name="company"></param>
    ''' <param name="thirdPartyId"></param>
    ''' <param name="noteType"></param>
    ''' <param name="nature"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function ListBillsPortfolioNoteByThridParty(company As String, thirdPartyId As Integer, noteType As Integer, nature As Integer) As XPInstantFeedbackSource
        Dim Portfolio As New PortfolioServicesXpo(company)
        Return Portfolio.ListBillsPortfolioNoteByThridParty(thirdPartyId, noteType, nature)
    End Function

    ''' <summary>
    ''' metodo para listar los anticipos para las notas de cuentas por cobrar
    ''' </summary>
    Public Shared Function ListPortfolioAdvancePortfolioNote(company As String, customerId As Integer, nature As Integer) As XPInstantFeedbackSource
        Dim Portfolio As New PortfolioServicesXpo(company)
        Return Portfolio.ListPortfolioAdvancePortfolioNote(customerId, nature)
    End Function
    ''' <summary>
    ''' lista los conceptos de notas por estado
    ''' </summary>
    ''' <param name="company"></param>
    ''' <param name="status"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function GetAllPortfolioNoteConceptByStatus(company As String, status As Boolean) As XPInstantFeedbackSource
        Dim Portfolio As New PortfolioServicesXpo(company)
        Return Portfolio.GetAllPortfolioNoteConceptByStatus(status)
    End Function
    ''' <summary>
    ''' lista las notas
    ''' </summary>
    ''' <param name="company"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function ListPortfolioNote(company As String) As XPInstantFeedbackSource
        Dim Portfolio As New PortfolioServicesXpo(company)
        Return Portfolio.ListPortfolioNote()
    End Function
    ''' <summary>
    ''' Lista los conceptos de cuentas x cobrar por estado
    ''' </summary>
    ''' <param name="company"></param>
    ''' <param name="status"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function GetAllAccountReceivableConceptByStatus(company As String, status As Integer) As XPInstantFeedbackSource
        Dim Portfolio As New PortfolioServicesXpo(company)
        Return Portfolio.GetAllAccountReceivableConceptByStatus(status)
    End Function

    ''' <summary>
    ''' lista los grupos de atencion por tipo de liquidacion
    ''' </summary>
    ''' <param name="noteType"></param>
    ''' <param name="company"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function ListPortfolioNoteConceptByNoteType(noteType As Integer, status As Boolean, company As String) As XPInstantFeedbackSource
        Dim service As New PortfolioServicesXpo(company)
        Return service.ListPortfolioNoteConceptByNoteType(noteType, status)
    End Function
    ''' <summary>
    ''' lista las facturas en cartera por estado
    ''' </summary>
    ''' <param name="company"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function ListAccountReceivableByStatus(company As String, status As Integer) As XPInstantFeedbackSource
        Dim service As New PortfolioServicesXpo(company)
        Return service.ListAccountReceivableByStatus(status)
    End Function

    ''' <summary>
    ''' lista las facturas en cartera
    ''' </summary>
    ''' <param name="company"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function ListAccountReceivable(company As String) As XPInstantFeedbackSource
        Dim service As New PortfolioServicesXpo(company)
        Return service.ListAccountReceivable()
    End Function

#End Region

#Region "Payments"

#Region "Reports"

    ''' <summary>
    ''' Función para obtener una lista de datos de una entidad XPO
    ''' </summary>
    ''' <typeparam name="T">Objeto XPO</typeparam>
    ''' <param name="Company">Compañia Actual</param>
    ''' <param name="Fun">Objeto tipo Función opcional para filtrar los datos necesarios</param>
    ''' <returns>Objeto</returns>
    Public Shared Function ListCollectionPaymentsReport(Of T)(ByVal Company As String, Optional Fun As Func(Of T, Boolean) = Nothing, Optional criteria As String = Nothing) As Object
        Dim Payments As New PaymentsServiceXpo(Company)
        Return Payments.GetCollection(Of T)(Fun, criteria)
    End Function

    ''' <summary>
    ''' Función para obtener una lista de datos de una entidad XPO para el reporte Extractos de cuentas por pagar 
    ''' </summary>
    ''' <param name="Company">Compañia Actual</param>
    ''' <returns>Objeto</returns>
    Public Shared Function ListCollectionReportExtractsAccountsByPay(ByVal Company As String, ByVal INDFechaIni As Date, ByVal INDFechaEnd As Date, ByVal INDSupplierStart As String, ByVal INDSupplierEnd As String, ByVal INDInvoiceStart As String, ByVal INDInvoiceEnd As String) As Object
        Dim Payments As New PaymentsServiceXpo(Company)
        Return Payments.GetCollectionReportExtractsAccountsByPay(INDFechaIni, INDFechaEnd, INDSupplierStart, INDSupplierEnd, INDInvoiceStart, INDInvoiceEnd)
    End Function

#End Region

    ''' <summary>
    ''' lista todas las notas de pago
    ''' </summary>
    ''' <param name="company">filtro</param>
    ''' <returns></returns>
    Public Shared Function ListPaymentsNotesReportFilter(ByVal company As String, ByVal filtro As String) As XPInstantFeedbackSource
        Dim service As New PaymentsServiceXpo(company)
        Return service.ListPaymentsNotesReportFilter(filtro)
    End Function

    ''' <summary>
    ''' lista todos los traslados
    ''' </summary>
    ''' <param name="company">filtro</param>
    ''' <returns></returns>
    Public Shared Function ListTransfersReportPaymentsFilter(ByVal company As String, ByVal filtro As String) As XPInstantFeedbackSource
        Dim service As New PaymentsServiceXpo(company)
        Return service.ListTransfersReportPaymentsFilter(filtro)
    End Function

    ''' <summary>
    ''' Lista todos los fabricantes
    ''' </summary>
    ''' <param name="company">The company.</param>
    ''' <returns></returns>
    Public Shared Function ListSupplierReport(ByVal company As String) As XPInstantFeedbackSource
        Dim service As New PaymentsServiceXpo(company)
        Return service.ListSupplierReport()
    End Function

    ''' <summary>
    ''' Lista todos las facturas por filtro
    ''' </summary>
    ''' <param name="company">The company.</param>
    ''' <returns></returns>
    Public Shared Function ListPaymentsAccountPayableReportByFilter(ByVal company As String, ByVal Filtro As String) As XPInstantFeedbackSource
        Dim service As New PaymentsServiceXpo(company)
        Return service.ListPaymentsAccountPayableReportByFilter(Filtro)
    End Function

    ''' <summary>
    ''' Lista todos las facturas
    ''' </summary>
    ''' <param name="company">The company.</param>
    ''' <returns></returns>
    Public Shared Function ListPaymentsAccountPayableReport(ByVal company As String) As XPInstantFeedbackSource
        Dim service As New PaymentsServiceXpo(company)
        Return service.ListPaymentsAccountPayableReport()
    End Function

    ''' <summary>
    ''' Lista todos los Terceros
    ''' </summary>
    ''' <param name="company">The company.</param>
    ''' <returns></returns>
    Public Shared Function ListThirdPartyReportPayments(ByVal company As String) As XPCollection(Of Infrastructure.Data.Xpo.PaymentsRepository.CommonThirdPartyXpo)
        Dim service As New PaymentsServiceXpo(company)
        Return service.ListThirdPartyReportPayments()
    End Function

    ''' <summary>
    ''' Lista todos los Traslados
    ''' </summary>
    ''' <param name="company">The company.</param>
    ''' <returns></returns>
    Public Shared Function ListTransfersReportPayments(ByVal company As String) As XPInstantFeedbackSource
        Dim service As New PaymentsServiceXpo(company)
        Return service.ListTransfersReportPayments()
    End Function

    ''' <summary>
    ''' Lista todos los Traslados de facturas
    ''' </summary>
    ''' <param name="company">The company.</param>
    ''' <returns></returns>
    Public Shared Function ListAccountPayableTransfer(ByVal company As String) As XPInstantFeedbackSource
        Dim service As New PaymentsServiceXpo(company)
        Return service.ListAccountPayableTransfer()
    End Function

    ''' <summary>
    ''' Lista todas las notas
    ''' </summary>
    ''' <param name="company">The company.</param>
    ''' <returns></returns>
    Public Shared Function ListPaymentsNotesReport(ByVal company As String) As XPInstantFeedbackSource
        Dim service As New PaymentsServiceXpo(company)
        Return service.ListPaymentsNotesReport()
    End Function

    ''' <summary>
    ''' Lista todas las dependencias
    ''' </summary>
    ''' <param name="company">The company.</param>
    ''' <returns></returns>
    Public Shared Function ListDependencies(ByVal company As String) As XPInstantFeedbackSource
        Dim service As New PaymentsServiceXpo(company)
        Return service.ListDepencies()
    End Function

    ''' <summary>
    ''' Lista los traslados
    ''' </summary>
    ''' <param name="company">The company.</param>
    ''' <returns></returns>
    Public Shared Function ListPaymentTransfer(ByVal company As String) As XPInstantFeedbackSource
        Dim service As New PaymentsServiceXpo(company)
        Return service.ListPaymentTransfer()
    End Function

    ''' <summary>
    ''' Lists the account payable by bill number.
    ''' </summary>
    ''' <param name="company">The company.</param>
    ''' <param name="billNumber">The bill number.</param>
    ''' <returns></returns>
    Public Shared Function ListAccountPayableByBillNumber(ByVal company As String, ByVal billNumber As List(Of String)) As XPCollection
        Dim service As New PaymentsServiceXpo(company)
        Return service.ListAccountPayableByBillNumber(billNumber)
    End Function

    ''' <summary>
    ''' Lista todas las dependencias
    ''' </summary>
    ''' <param name="company">The company.</param>
    ''' <returns></returns>
    Public Shared Function ListInitialBalance(ByVal company As String) As XPInstantFeedbackSource
        Dim service As New PaymentsServiceXpo(company)
        Return service.ListInitialBalance()
    End Function

    Public Shared Function ListAccountPayableByStateAndNature(company As String, state As Integer, nature As Integer) As LinqInstantFeedbackSource
        Dim service As New PaymentsServiceXpo(company)
        Return service.ListAccountPayableByStateAndNature(state, nature)
    End Function

    ''' <summary>
    ''' Lista todas las dependencias
    ''' </summary>
    ''' <param name="company">The company.</param>
    ''' <returns></returns>
    Public Shared Function ListAdvancePayments(ByVal idSupplier As Integer, ByVal Status As Byte, ByVal Nature As Byte, ByVal company As String) As LinqInstantFeedbackSource
        Dim service As New PaymentsServiceXpo(company)
        Return service.ListAdvancePayments(idSupplier, Status, Nature)
    End Function

    ''' <summary>
    ''' Gets the account payable xpo by identifier.
    ''' </summary>
    ''' <param name="Id">The identifier.</param>
    ''' <param name="company">The company.</param>
    ''' <returns></returns>
    Public Shared Function GetAccountPayableXpoById(ByVal Id As Integer, ByVal company As String) As LinqInstantFeedbackSource
        Dim service As New PaymentsServiceXpo(company)
        Return service.GetAccountPayableXpoById(Id)
    End Function
    ''' <summary>
    ''' Lista todas las cuentas por pagar
    ''' </summary>
    ''' <param name="company">The company.</param>
    ''' <returns></returns>
    Public Shared Function GetPaymentsTrazability(company As String, AccountPayableCode As String, BillNumber As String) As XPCollection
        Dim service As New PaymentsServiceXpo(company)
        Return service.GetPaymentsTrazability(AccountPayableCode, BillNumber)
    End Function

    ''' <summary>
    ''' lista la trazabilidad de pagos
    ''' </summary>
    ''' <param name="company"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function ListAccountPayablebysupplier(ByVal company As String, ByVal IdSupplier As Integer) As XPInstantFeedbackSource
        Dim service As New PaymentsServiceXpo(company)
        Return service.ListAccountPayablebysupplier(IdSupplier)
    End Function

    ''' <summary>
    ''' lista la trazabilidad de pagos
    ''' </summary>
    ''' <param name="company"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function ListAccountPayable(ByVal company As String) As XPInstantFeedbackSource
        Dim service As New PaymentsServiceXpo(company)
        Return service.ListAccountPayable()
    End Function

    ''' <summary>
    ''' Lista todas las cuentas por pagar
    ''' </summary>
    ''' <param name="company">The company.</param>
    ''' <returns></returns>
    Public Shared Function ListPaymentNotes(ByVal company As String) As XPInstantFeedbackSource
        Dim service As New PaymentsServiceXpo(company)
        Return service.ListPaymentNotes()
    End Function

    ''' <summary>
    ''' Lista todos los conceptos de pagos
    ''' </summary>
    ''' <param name="company"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function ListConceptsAccountsPayable(ByVal company As String) As XPInstantFeedbackSource
        Dim service As New PaymentsServiceXpo(company)
        Return service.ListConceptsAccountsPayable()
    End Function

    ''' <summary>
    ''' Lista los conceptos de pagos por estado
    ''' </summary>
    ''' <param name="company">Empresa a la que se encuentra conectado</param>
    ''' <returns>Lista de tipos de documento</returns>
    Public Shared Function ListConceptsAccountsPayableByStatus(ByVal status As Boolean, ByVal company As String) As XPInstantFeedbackSource
        Dim service As New PaymentsServiceXpo(company)
        Return service.ListConceptsAccountsPayableByStatus(status)
    End Function

    ''' <summary>
    ''' Lista los conceptos de pagos por estado y si maneja retencion
    ''' </summary>
    ''' <param name="company">Empresa a la que se encuentra conectado</param>
    ''' <returns>Lista de tipos de documento</returns>
    Public Shared Function ListConceptsAccountsPayableByStatusAndHandlesRetention(ByVal status As Boolean, ByVal handlesRetention As Boolean, ByVal company As String, Optional session As DevExpress.Xpo.Session = Nothing) As XPCollection
        Dim service As New PaymentsServiceXpo(company)
        Return service.ListConceptsAccountsPayableByStatusAndHandlesRetention(status, handlesRetention, session)
    End Function

    ''' <summary>
    ''' Lista los conceptos de pagos por estado y si maneja retencion
    ''' </summary>
    ''' <param name="company">Empresa a la que se encuentra conectado</param>
    ''' <returns>Lista de tipos de documento</returns>
    Public Shared Function ListAccountPayableByFilingUnitId(filingUnitId As Integer, ByVal company As String) As XPCollection
        Dim service As New PaymentsServiceXpo(company)
        Return service.ListAccountPayableByFilingUnitId(filingUnitId)
    End Function

    ''' <summary>
    ''' Lista todos las unidades de radicacion
    ''' </summary>
    ''' <param name="company"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function ListFilingUnit(ByVal company As String) As XPInstantFeedbackSource
        Dim service As New PaymentsServiceXpo(company)
        Return service.ListFilingUnit()
    End Function

    ''' <summary>
    ''' Lista las unidades de radicacion por estado
    ''' </summary>
    ''' <param name="company">Empresa a la que se encuentra conectado</param>
    ''' <returns>Lista de tipos de documento</returns>
    Public Shared Function ListFilingUnitByStatus(ByVal status As Boolean, ByVal company As String) As XPInstantFeedbackSource
        Dim service As New PaymentsServiceXpo(company)
        Return service.ListFilingUnitByStatus(status)
    End Function

    ''' <summary>
    ''' Lista las unidades de radicacion por estado
    ''' </summary>
    ''' <param name="company">Empresa a la que se encuentra conectado</param>
    ''' <returns>Lista de tipos de documento</returns>
    Public Shared Function ListFilingUnitByStatusCollection(ByVal status As Boolean, ByVal company As String) As XPCollection
        Dim service As New PaymentsServiceXpo(company)
        Return service.ListFilingUnitByStatusCollection(status)
    End Function

    ''' <summary>
    ''' Lista las unidades de radicacion por estado
    ''' </summary>
    ''' <param name="company">Empresa a la que se encuentra conectado</param>
    ''' <returns>Lista de tipos de documento</returns>
    Public Shared Function ListFilingUnitData(ByVal company As String) As XPCollection
        Dim service As New PaymentsServiceXpo(company)
        Return service.ListFilingUnitData()
    End Function

    ''' <summary>
    ''' Lista los conceptos de pagos por estado
    ''' </summary>
    ''' <param name="company">Empresa a la que se encuentra conectado</param>
    ''' <returns>Lista de tipos de documento</returns>
    Public Shared Function ListAccountPayableConceptByHandlesRetention(ByVal handlesRetention As Boolean, ByVal company As String) As XPInstantFeedbackSource
        Dim service As New PaymentsServiceXpo(company)
        Return service.ListAccountPayableConceptByHandlesRetention(handlesRetention)
    End Function

    ''' <summary>
    ''' Lista los conceptos de pagos por estado
    ''' </summary>
    ''' <param name="company">Empresa a la que se encuentra conectado</param>
    ''' <returns>Lista de tipos de documento</returns>
    Public Shared Function ListAccountPayableConceptByHandlesRetentionAndConceptType(ByVal status As Boolean, ByVal handlesRetention As Boolean, ByVal conceptType As Integer, ByVal company As String) As XPInstantFeedbackSource
        Dim service As New PaymentsServiceXpo(company)
        Return service.ListAccountPayableConceptByHandlesRetentionAndConceptType(status, handlesRetention, conceptType)
    End Function

    ''' <summary>
    ''' Lista los conceptos de pagos por estado
    ''' </summary>
    ''' <param name="company">Empresa a la que se encuentra conectado</param>
    ''' <returns>Lista de tipos de documento</returns>
    Public Shared Function ListAccountPayableConceptByConceptType(ByVal status As Boolean, ByVal conceptType As Integer, ByVal company As String) As XPInstantFeedbackSource
        Dim service As New PaymentsServiceXpo(company)
        Return service.ListAccountPayableConceptByConceptType(status, conceptType)
    End Function

    ''' <summary>
    ''' Lista todos los conceptos de notas
    ''' </summary>
    ''' <param name="company"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function ListConceptsNotes(ByVal company As String) As XPInstantFeedbackSource
        Dim service As New PaymentsServiceXpo(company)
        Return service.ListConceptsNotes()
    End Function

    ''' <summary>
    ''' Lista todos los conceptos de notas
    ''' </summary>
    ''' <param name="company"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function ListAccountPayableRejectionReason(ByVal company As String) As XPInstantFeedbackSource
        Dim service As New PaymentsServiceXpo(company)
        Return service.ListAccountPayableRejectionReason()
    End Function

    ''' <summary>
    ''' Lista los conceptos de notas por estado
    ''' </summary>
    ''' <param name="company">Empresa a la que se encuentra conectado</param>
    ''' <returns>Lista de tipos de documento</returns>
    Public Shared Function ListConceptsNotesByStatus(ByVal status As Boolean, ByVal company As String) As XPInstantFeedbackSource
        Dim service As New PaymentsServiceXpo(company)
        Return service.ListConceptsNotesByStatus(status)
    End Function

    ''' <summary>
    ''' Lista los conceptos de notas por estado y tipo de concepto
    ''' </summary>
    ''' <param name="company">Empresa a la que se encuentra conectado</param>
    ''' <returns>Lista de tipos de documento</returns>
    Public Shared Function ListConceptsNotesByConcepType(ByVal status As Boolean, ByVal concepType As Integer, ByVal company As String) As XPInstantFeedbackSource
        Dim service As New PaymentsServiceXpo(company)
        Return service.ListConceptsNotesByConcepType(status, concepType)
    End Function

    ''' <summary>
    ''' Lista todas las rutas
    ''' </summary>
    ''' <param name="company"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function ListRoutes(ByVal company As String) As XPInstantFeedbackSource
        Dim service As New PaymentsServiceXpo(company)
        Return service.ListRoutes()
    End Function

    ''' <summary>
    ''' Lista todas las cuotas de las facturas
    ''' </summary>
    ''' <param name="company">The company.</param>
    ''' <returns></returns>
    Public Shared Function ListAccountPayableSharesByIdThirdIdAccountAndState(ByVal company As String, ByVal IdThird As Integer, ByVal IdAccount As Integer, ByVal Status As Byte) As LinqInstantFeedbackSource
        Dim service As New PaymentsServiceXpo(company)
        Return service.ListAccountPayableSharesByIdThirdIdAccountAndState(IdThird, IdAccount, Status)
    End Function

    ''' <summary>
    ''' Lista las facturas por el id del proveedor y el estado
    ''' </summary>
    ''' <param name="company">The company.</param>
    ''' <returns></returns>
    Public Shared Function ListAccountPayableByIdSupplierAndState(ByVal company As String, ByVal IdSupplier As Integer, ByVal Status As Byte, ByVal Nature As Byte) As LinqInstantFeedbackSource
        Dim service As New PaymentsServiceXpo(company)
        Return service.ListAccountPayableByIdSupplierAndState(IdSupplier, Status, Nature)
    End Function

    ''' <summary>
    ''' Lista las facturas por el id del proveedor y el estado
    ''' </summary>
    ''' <param name="company">The company.</param>
    ''' <returns></returns>
    Public Shared Function ListAccountPayableByIdSupplierAndStateWithDeferredCausation(ByVal company As String, ByVal IdSupplier As Integer, ByVal Status As Byte) As LinqInstantFeedbackSource
        Dim service As New PaymentsServiceXpo(company)
        Return service.ListAccountPayableByIdSupplierAndStateWithDeferredCausation(IdSupplier, Status)
    End Function

    ''' <summary>
    ''' Lista todas los oficios de translado que tiene el usuario que tiene un usuario 
    ''' </summary>
    ''' <param name="company">The company.</param>
    ''' <returns></returns>
    Public Shared Function ListAccountPayableTransferAcceptence(ByVal company As String, listFillingUnitId As List(Of Integer)) As PLinqServerModeSource
        Dim service As New PaymentsServiceXpo(company)
        Return service.ListAccountPayableTransferAcceptence(listFillingUnitId)
    End Function
    ''' <summary>
    ''' Lista el detalle de una translado de facturas
    ''' </summary>
    ''' <param name="company"></param>
    ''' <param name="_AccountPayableTransferId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function ListPaymentsAccountPayableTransferDetail(ByVal company As String, ByVal _AccountPayableTransferId As Integer) As PLinqServerModeSource
        Dim service As New PaymentsServiceXpo(company)
        Return service.ListPaymentsAccountPayableTransferDetail(_AccountPayableTransferId)
    End Function
    ''' <summary>
    ''' lista los anticipos para utilizarlos en el recibo de caja
    ''' </summary>
    ''' <param name="company"></param>
    ''' <param name="idThitdParty"></param>
    ''' <param name="idMainAccount"></param>
    ''' <param name="idCostCenter"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function ListAdvancePaymentsCashReceipt(company As String, idThitdParty As Integer, idMainAccount As Integer, idCostCenter As Integer)
        Dim service As New PaymentsServiceXpo(company)
        Return service.ListAdvancePaymentsCashReceipt(idThitdParty, idMainAccount, idCostCenter)
    End Function

    ''' <summary>
    ''' lista los anticipos para utilizarlos en el recibo de caja
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>

    Public Shared Function ListSharesWithAccountPayable(company As String, IdSupplier As Integer, Status As Byte, TransferType As Integer)
        Dim service As New PaymentsServiceXpo(company)
        Return service.ListSharesWithAccountPayable(IdSupplier, Status, TransferType)
    End Function

    ''' <summary>
    ''' Lista las cuotas de las facturas por el id del proveedor y el estado
    ''' </summary>
    ''' <param name="company">The company.</param>
    ''' <returns></returns>
    Public Shared Function ListAccountPayableSharesByIdSupplierAndState(ByVal company As String, ByVal IdSupplier As Integer, ByVal Status As Byte) As LinqInstantFeedbackSource
        Dim service As New PaymentsServiceXpo(company)
        Return service.ListAccountPayableSharesByIdSupplierAndState(IdSupplier, Status)
    End Function

    ''' <summary>
    ''' Lista las cuotas de las facturas por el id del proveedor y el estado
    ''' </summary>
    ''' <param name="company">The company.</param>
    ''' <returns></returns>
    Public Shared Function ListAccountPayableSharesBySupplierIdAndState(ByVal company As String, ByVal IdSupplier As Integer, ByVal Status As Byte) As XPCollection
        Dim service As New PaymentsServiceXpo(company)
        Return service.ListAccountPayableSharesBySupplierIdAndState(IdSupplier, Status)
    End Function

    ''' <summary>
    ''' Lista las cxp por id proveedor y estado para el form de traslado de facturas
    ''' </summary>
    Public Shared Function ListAccountPayableBySupplierIdAndStatusForTransfer(ByVal supplierId As Integer, ByVal filingUnitId As Integer, ByVal company As String) As PLinqServerModeSource
        Dim service As New PaymentsServiceXpo(company)
        Return service.ListAccountPayableBySupplierIdAndStatusForTransfer(supplierId, filingUnitId)
    End Function

    ''' <summary>
    ''' lista el detalle AccountPayableDetailConceptLiquidation para el reporte de Cuentas por pagar y obtener datos de la calculadora
    ''' </summary>
    ''' <param name="company">Empresa a la que se encuentra conectado</param>
    ''' <returns>Lista de tipos de documento</returns>
    Public Shared Function ListAccountPayableDetailConceptLiquidationByAccountPayableDetailConceptId(ByVal filter As String, ByVal company As String) As XPCollection(Of PaymentsAccountPayableConceptLiquidationXpo)
        Dim service As New PaymentsServiceXpo(company)
        Return service.ListAccountPayableDetailConceptLiquidationByAccountPayableDetailConceptId(filter)
    End Function
#End Region

#Region "FixedAssets"

    ''' <summary>
    ''' Lista las Ubicaciones
    ''' </summary>
    ''' <param name="company">Empresa a la que se encuentra conectado</param>
    ''' <returns>Lista de tipos de documento</returns>
    Public Shared Function ListFixedAssetLocation(ByVal company As String) As XPCollection
        Dim service As New FixedAssetServiceXpo(company)
        Return service.ListFixedAssetLocation()
    End Function

    ''' <summary>
    ''' Lista todos las unidades de radicacion
    ''' </summary>
    ''' <param name="company"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function ListFixedAssetLocationData(ByVal company As String) As XPInstantFeedbackSource
        Dim service As New FixedAssetServiceXpo(company)
        Return service.ListFixedAssetLocationData()
    End Function

    ''' <summary>
    ''' Lista todos las unidades de radicacion
    ''' </summary>
    ''' <param name="company"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function ListFixedAssetLocationType(ByVal company As String) As XPInstantFeedbackSource
        Dim service As New FixedAssetServiceXpo(company)
        Return service.ListFixedAssetLocationType()
    End Function

    ''' <summary>
    ''' Lista todos las unidades de radicacion
    ''' </summary>
    ''' <param name="company"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function ListFixedAssetCostCenter(ByVal company As String) As XPInstantFeedbackSource
        Dim service As New FixedAssetServiceXpo(company)
        Return service.ListFixedAssetCostCenter()
    End Function

    ''' <summary>
    ''' Lista todos las unidades de radicacion
    ''' </summary>
    ''' <param name="company"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function ListFixedAssetMainAccount(ByVal company As String) As XPInstantFeedbackSource
        Dim service As New FixedAssetServiceXpo(company)
        Return service.ListFixedAssetMainAccount()
    End Function


    ''' <summary>
    ''' Lista todas las dependencias
    ''' </summary>
    ''' <param name="company">The company.</param>
    ''' <returns></returns>
    Public Shared Function ListAddres(ByVal company As String) As XPInstantFeedbackSource
        Dim service As New FixedAssetServiceXpo(company)
        Return service.GetAllAddress()
    End Function

    ''' <summary>
    ''' Lista todas las dependencias
    ''' </summary>
    ''' <param name="company">The company.</param>
    ''' <returns></returns>
    Public Shared Function ListAreas(ByVal company As String) As XPInstantFeedbackSource
        Dim service As New FixedAssetServiceXpo(company)
        Return service.GetAllArea()
    End Function

    ''' <summary>
    ''' Lista todas las deducciones
    ''' </summary>
    ''' <param name="company">The company.</param>
    ''' <returns></returns>
    Public Shared Function ListDeduction(ByVal company As String) As XPInstantFeedbackSource
        Dim service As New FixedAssetServiceXpo(company)
        Return service.GetAllDeduction()
    End Function

    ''' <summary>
    ''' Lista todas las deducciones
    ''' </summary>
    ''' <param name="company">The company.</param>
    ''' <returns></returns>
    Public Shared Function ListIva(ByVal company As String) As XPInstantFeedbackSource
        Dim service As New FixedAssetServiceXpo(company)
        Return service.GetAllIva()
    End Function

    ''' <summary>
    ''' Lista todos los conceptos de traslados
    ''' </summary>
    ''' <param name="company">The company.</param>
    ''' <returns></returns>
    Public Shared Function ListConceptShuttle(ByVal company As String) As XPInstantFeedbackSource
        Dim service As New FixedAssetServiceXpo(company)
        Return service.ListConceptShuttle()
    End Function

    ''' <summary>
    ''' Lista todos los ajustes de inflacion
    ''' </summary>
    ''' <param name="company">The company.</param>
    ''' <returns></returns>
    Public Shared Function GetAllInflationAdjustment(ByVal company As String) As XPInstantFeedbackSource
        Dim service As New FixedAssetServiceXpo(company)
        Return service.GetAllInflationAdjustment()
    End Function

    ''' <summary>
    ''' Lista todos los ajustes de inflacion
    ''' </summary>
    ''' <param name="company">The company.</param>
    ''' <returns></returns>
    Public Shared Function GetAllGroupFixedAsset(ByVal company As String) As XPInstantFeedbackSource
        Dim service As New FixedAssetServiceXpo(company)
        Return service.GetAllGroup()
    End Function

    ''' <summary>
    ''' Lista todos los productos
    ''' </summary>
    ''' <param name="company">The company.</param>
    ''' <returns></returns>
    Public Shared Function GetAllProductFixedAsset(ByVal company As String) As XPInstantFeedbackSource
        Dim service As New FixedAssetServiceXpo(company)
        Return service.GetAllProduct()
    End Function

    ''' <summary>
    ''' Gets all group by state fixed asset.
    ''' </summary>
    ''' <param name="company">The company.</param>
    ''' <param name="status">if set to <c>true</c> [status].</param>
    ''' <returns></returns>
    Public Shared Function GetAllGroupByStateFixedAsset(ByVal company As String, ByVal status As Boolean) As XPInstantFeedbackSource
        Dim service As New FixedAssetServiceXpo(company)
        Return service.GetAllGroupByState(status)
    End Function

    ''' <summary>
    ''' Lista todas los tipos de ubicación mayores a los del padre(ubicación)
    ''' </summary>
    ''' <param name="company">The company.</param>
    ''' <param name="filtro"></param>
    ''' <returns></returns>
    Public Shared Function ListFixedAssetLocationTypeParent(ByVal company As String, ByVal filtro As String) As XPInstantFeedbackSource
        Dim service As New FixedAssetServiceXpo(company)
        Return service.ListFixedAssetLocationTypeParent(filtro)
    End Function

    ''' <summary>
    ''' Lista todos los productos
    ''' </summary>
    ''' <param name="company">The company.</param>
    ''' <returns></returns>
    Public Shared Function GetAllClassificationFixedAsset(ByVal company As String) As XPInstantFeedbackSource
        Dim service As New FixedAssetServiceXpo(company)
        Return service.GetAllClassification()
    End Function

    ''' <summary>
    ''' Gets all group by state fixed asset.
    ''' </summary>
    ''' <param name="company">The company.</param>
    ''' <param name="status">if set to <c>true</c> [status].</param>
    ''' <returns></returns>
    Public Shared Function GetAllClassficationByStateFixedAsset(ByVal company As String, ByVal status As Boolean) As XPInstantFeedbackSource
        Dim service As New FixedAssetServiceXpo(company)
        Return service.GetAllClassificationByState(status)
    End Function

    ''' <summary>
    ''' Lista todas las deducciones
    ''' </summary>
    ''' <param name="company">The company.</param>
    ''' <returns></returns>
    Public Shared Function ListFixedAssetTrademark(ByVal company As String) As XPInstantFeedbackSource
        Dim service As New FixedAssetServiceXpo(company)
        Return service.ListTrademark()
    End Function

    ''' <summary>
    ''' Lista todas los Tipos de Poliza
    ''' </summary>
    ''' <param name="company">The company.</param>
    ''' <returns></returns>
    Public Shared Function ListFixedAssetPolizaType(ByVal company As String) As XPInstantFeedbackSource
        Dim service As New FixedAssetServiceXpo(company)
        Return service.ListFixedAssetPolizaType()
    End Function

    ''' <summary>
    ''' Lista todas las Polizas
    ''' </summary>
    ''' <param name="company">The company.</param>
    ''' <returns></returns>
    Public Shared Function ListFixedAssetPoliza(ByVal company As String) As XPInstantFeedbackSource
        Dim service As New FixedAssetServiceXpo(company)
        Return service.ListFixedAssetPoliza()
    End Function

    ''' <summary>
    ''' Lista todas las Polizas
    ''' </summary>
    ''' <param name="company">The company.</param>
    ''' <returns></returns>
    Public Shared Function ListFixedAssetEquipmentCatalog(ByVal company As String) As XPInstantFeedbackSource
        Dim service As New FixedAssetServiceXpo(company)
        Return service.ListFixedEquipmentCatalog()
    End Function

    ''' <summary>
    ''' Lista todas las Polizas
    ''' </summary>
    ''' <param name="company">The company.</param>
    ''' <returns></returns>
    Public Shared Function ListFixedAssetInsurance(ByVal company As String) As XPInstantFeedbackSource
        Dim service As New FixedAssetServiceXpo(company)
        Return service.ListFixedInsurance()
    End Function

    Public Shared Function ListFixedAssetInventoryType(ByVal company As String) As XPInstantFeedbackSource
        Dim service As New FixedAssetServiceXpo(company)
        Return service.ListFixedAssetInventoryType()
    End Function

    Public Shared Function ListFixedAssetResponsibleType(ByVal company As String) As XPInstantFeedbackSource
        Dim service As New FixedAssetServiceXpo(company)
        Return service.ListFixedAssetResponsibleType()
    End Function

    Public Shared Function ListFixedAssetVinculationType(ByVal company As String) As XPInstantFeedbackSource
        Dim service As New FixedAssetServiceXpo(company)
        Return service.ListFixedAssetVinculationType()
    End Function

    Public Shared Function ListFixedAssetResponsible(ByVal company As String) As XPInstantFeedbackSource
        Dim service As New FixedAssetServiceXpo(company)
        Return service.ListFixedAssetResponsible()
    End Function

    Public Shared Function ListFixedAssetEquipment(ByVal company As String) As XPInstantFeedbackSource
        Dim service As New FixedAssetServiceXpo(company)
        Return service.ListFixedAssetEquipment()
    End Function

    Public Shared Function ListFixedAssetEquipmentType(ByVal company As String) As XPInstantFeedbackSource
        Dim service As New FixedAssetServiceXpo(company)
        Return service.ListFixedAssetEquipmentType()
    End Function

    Public Shared Function ListFixedAssetInputRemission(ByVal company As String) As XPInstantFeedbackSource
        Dim service As New FixedAssetServiceXpo(company)
        Return service.ListFixedAssetInputRemission()
    End Function

    Public Shared Function ListFixedAssetPartsAccesoriesConsumibles(ByVal company As String) As XPInstantFeedbackSource
        Dim service As New FixedAssetServiceXpo(company)
        Return service.ListFixedAssetPartsAccesoriesConsumibles()
    End Function

#End Region

#Region "Common"
    ''' <summary>
    ''' lista los clientes por estado
    ''' </summary>
    ''' <param name="company"></param>
    ''' <param name="status"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function ListCustomerByStatus(company As String, status As Boolean) As XPInstantFeedbackSource
        Dim service As New CommonServicesXpo(company)
        Return service.ListCustomerByStatus(status)
    End Function
    ''' <summary>
    ''' Lista todas las cuentas por pagar
    ''' </summary>
    ''' <param name="company">The company.</param>
    ''' <returns></returns>
    Public Shared Function ListDistributionLine(ByVal company As String) As XPInstantFeedbackSource
        Dim service As New CommonServicesXpo(company)
        Return service.ListDistributionLine()
    End Function

    ''' <summary>
    ''' Lista todas las cuentas por pagar
    ''' </summary>
    ''' <param name="company">The company.</param>
    ''' <returns></returns>
    Public Shared Function ListSuppliersDistributionLines(ByVal company As String) As XPInstantFeedbackSource
        Dim service As New CommonServicesXpo(company)
        Return service.ListSuppliersDistributionLines()
    End Function

    ''' <summary>
    ''' Lista todas las cuentas por pagar
    ''' </summary>
    ''' <param name="company">The company.</param>
    ''' <returns></returns>
    Public Shared Function ListSuppliersDistributionLinesById(ByVal id As Integer, ByVal company As String) As XPCollection
        Dim service As New CommonServicesXpo(company)
        Return service.ListSuppliersDistributionLinesById(id)
    End Function

    ''' <summary>
    ''' lista todas las actividades economicas
    ''' </summary>
    ''' <param name="company"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function ListEconomicActivity(ByVal company As String) As XPInstantFeedbackSource
        Dim service As New CommonServicesXpo(company)
        Return service.ListEconomicActivity()
    End Function

    ''' <summary>
    ''' Lista las actividades economicas por estado
    ''' </summary>
    ''' <param name="company">Empresa a la que se encuentra conectado</param>
    Public Shared Function ListEconomicActivityByStatus(ByVal status As Boolean, ByVal company As String) As XPInstantFeedbackSource
        Dim service As New CommonServicesXpo(company)
        Return service.ListEconomicActivityByStatus(status)
    End Function

    ''' <summary>
    ''' lista todas las ciudades por estado
    ''' </summary>
    ''' <param name="company"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function ListAllCities(ByVal status As Boolean, ByVal company As String) As XPInstantFeedbackSource
        Dim service As New CommonServicesXpo(company)
        Return service.ListAllCities(status)
    End Function

    ''' <summary>
    ''' lista todas las unidades operativas XPCollection
    ''' </summary>
    ''' <param name="company"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function ListOperatingUnitTreeList(ByVal company As String) As XPCollection
        Dim service As New CommonServicesXpo(company)
        Return service.ListOperatingUnitTreeList()
    End Function

    ''' <summary>
    ''' lista todas las unidades operativas 
    ''' </summary>
    ''' <param name="company"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function ListOperatingUnit(ByVal company As String) As XPInstantFeedbackSource
        Dim service As New CommonServicesXpo(company)
        Return service.ListOperatingUnit()
    End Function

    ''' <summary>
    ''' lista todas las unidades operativas por id
    ''' </summary>
    ''' <param name="company"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function ListOperatingUnitById(ByVal company As String, ByVal operatingUnitId As Integer) As XPCollection
        Dim service As New CommonServicesXpo(company)
        Return service.ListOperatingUnitById(operatingUnitId)
    End Function


    ''' <summary>
    ''' Lista los conceptos de pagos por estado y si maneja retencion
    ''' </summary>
    ''' <param name="company">Empresa a la que se encuentra conectado</param>
    ''' <returns>Lista de tipos de documento</returns>
    Public Shared Function ListConceptsAccountsPayableByStatusAndHandlesRetentionOfCommon(ByVal status As Boolean, ByVal handlesRetention As Boolean, ByVal company As String) As XPCollection
        Dim service As New CommonServicesXpo(company)
        Return service.ListConceptsAccountsPayableByStatusAndHandlesRetentionOfCommon(status, handlesRetention)
    End Function

#End Region

#Region "MedicalFees"

    ''' <summary>
    ''' lista todos los grupos de atencion
    ''' </summary>
    ''' <param name="company"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function ListMedicalFeesContract(ByVal company As String) As XPInstantFeedbackSource
        Dim service As New MedicalFeesServiceXpo(company)
        Return service.ListMedicalFeesContract()
    End Function

    ''' <summary>
    ''' Consulta un contrato por id
    ''' </summary>
    ''' <param name="company"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function GetMedicalFeesContractById(id As Integer, ByVal company As String) As XPCollection
        Dim service As New MedicalFeesServiceXpo(company)
        Return service.GetMedicalFeesContractById(id)
    End Function

    ''' <summary>
    ''' Consulta un contrato por id
    ''' </summary>
    ''' <param name="company"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function ListViewMedicalFeesLiquidationDetailByMedicalFeesLiquidationId(MedicalFeesLiquidationId As Integer, ByVal company As String) As XPCollection
        Dim service As New MedicalFeesServiceXpo(company)
        Return service.ListViewMedicalFeesLiquidationDetailByMedicalFeesLiquidationId(MedicalFeesLiquidationId)
    End Function

    ''' <summary>
    ''' Consulta un contrato por id
    ''' </summary>
    ''' <param name="company"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function ListHealthProfessionalContractByHealthProfessionalCode(code As String, ByVal company As String) As XPCollection
        Dim service As New MedicalFeesServiceXpo(company)
        Return service.ListHealthProfessionalContractByHealthProfessionalCode(code)
    End Function

    ''' <summary>
    ''' Consulta un contrato por id
    ''' </summary>
    ''' <param name="company"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function ListHealthProfessionalContractByMedicalFeesContractId(medicalFeesContractId As Integer, ByVal company As String) As XPCollection
        Dim service As New MedicalFeesServiceXpo(company)
        Return service.ListHealthProfessionalContractByMedicalFeesContractId(medicalFeesContractId)
    End Function

    ''' <summary>
    ''' lista todas las liquidaciones de honorarios medicos
    ''' </summary>
    ''' <param name="company"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function ListMedicalFeesLiquidation(ByVal company As String) As XPInstantFeedbackSource
        Dim service As New MedicalFeesServiceXpo(company)
        Return service.ListMedicalFeesLiquidation()
    End Function

    ''' <summary>
    ''' Lista los grupos atencion por estado
    ''' </summary>
    ''' <param name="company">Empresa a la que se encuentra conectado</param>
    Public Shared Function ListMedicalFeesContractByStatus(ByVal status As Boolean, ByVal company As String) As XPInstantFeedbackSource
        Dim service As New MedicalFeesServiceXpo(company)
        Return service.ListMedicalFeesContractByStatus(status)
    End Function

    ''' <summary>
    ''' Lista los grupos atencion por estado
    ''' </summary>
    ''' <param name="company">Empresa a la que se encuentra conectado</param>
    Public Shared Function ListMedicalFeesContractByStatusAndContractType(ByVal status As Boolean, ByVal contractType As Integer, ByVal company As String) As XPInstantFeedbackSource
        Dim service As New MedicalFeesServiceXpo(company)
        Return service.ListMedicalFeesContractByStatusAndContractType(status, contractType)
    End Function

    ''' <summary>
    ''' Lista las causaciones por contrato
    ''' </summary>
    ''' <param name="company">Empresa a la que se encuentra conectado</param>
    Public Shared Function ListMedicalFeesCausationByMedicalFeesContractId(ByVal medicalFeesContractId As Integer, ByVal company As String) As XPInstantFeedbackSource
        Dim service As New MedicalFeesServiceXpo(company)
        Return service.ListMedicalFeesCausationByMedicalFeesContractId(medicalFeesContractId)
    End Function

    ''' <summary>
    ''' Lista las causaciones por contrato
    ''' </summary>
    ''' <param name="company">Empresa a la que se encuentra conectado</param>
    Public Shared Function ListCausationByMedicalFeesContractIdViewXpo(listMedicalFeesContractId As List(Of Integer), ByVal medicalFeesContractId As Integer?, ByVal initialDate As DateTime, ByVal endDate As DateTime, ByVal healthProfessionalCode As String, ByVal company As String) As XPCollection
        Dim service As New MedicalFeesServiceXpo(company)
        Return service.ListCausationByMedicalFeesContractIdViewXpo(listMedicalFeesContractId, medicalFeesContractId, initialDate, endDate, healthProfessionalCode)
    End Function

    ''' <summary>
    ''' Lista las causaciones por contrato
    ''' </summary>
    ''' <param name="company">Empresa a la que se encuentra conectado</param>
    Public Shared Function ListCausationByMedicalFeesContractId(listMedicalFeesContractId As List(Of Integer), ByVal medicalFeesContractId As Integer?, ByVal initialDate As DateTime, ByVal endDate As DateTime, ByVal healthProfessionalCode As String, ByVal company As String) As XPCollection
        Dim service As New MedicalFeesServiceXpo(company)
        Return service.ListCausationByMedicalFeesContractId(listMedicalFeesContractId, medicalFeesContractId, initialDate, endDate, healthProfessionalCode)
    End Function

    ''' <summary>
    ''' Lista las causaciones por contrato para deducciones
    ''' </summary>
    ''' <param name="company">Empresa a la que se encuentra conectado</param>
    Public Shared Function ListCausationByMedicalFeesContractIdForDeductions(listMedicalFeesContractId As List(Of Integer), ByVal medicalFeesContractId As Integer?, healthProfessionalCode As String, ByVal company As String) As XPCollection
        Dim service As New MedicalFeesServiceXpo(company)
        Return service.ListCausationByMedicalFeesContractIdForDeductions(listMedicalFeesContractId, medicalFeesContractId, healthProfessionalCode)
    End Function

    ''' <summary>
    ''' Lista las causaciones por contrato para glosas
    ''' </summary>
    ''' <param name="company">Empresa a la que se encuentra conectado</param>
    Public Shared Function ListCausationByMedicalFeesContractIdForGlosas(listMedicalFeesContractId As List(Of Integer), ByVal medicalFeesContractId As Integer?, healthProfessionalCode As String, ByVal company As String) As XPCollection
        Dim service As New MedicalFeesServiceXpo(company)
        Return service.ListCausationByMedicalFeesContractIdForGlosas(listMedicalFeesContractId, medicalFeesContractId, healthProfessionalCode)
    End Function

#End Region

#Region "Inventory"
    ''' <summary>
    ''' lista productos por admission (frmAdmissionProduct)
    ''' </summary>
    ''' <param name="company"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function ListViewAdmissionProduct(company As String, admissionNumber As String) As XPCollection(Of InventoryViewAdmissionProductXpo)
        Dim service As New InventoryServiceXpo(company)
        Return service.ListViewAdmissionProduct(admissionNumber)
    End Function

    ''' <summary>
    ''' lista los detalles de productos por admission (frmAdmissionProduct)
    ''' </summary>
    ''' <param name="company"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function ListViewAdmissionProductDetail(company As String, admissionNumber As String, functionalUnitId As Integer, productId As Integer) As XPCollection(Of InventoryViewAdmissionProductDetailXpo)
        Dim service As New InventoryServiceXpo(company)
        Return service.ListViewAdmissionProductDetail(admissionNumber, functionalUnitId, productId)
    End Function
    ''' <summary>
    ''' lista las facturas de productos
    ''' </summary>
    ''' <param name="company"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function ListDocumentInvoiceProductSales(company As String) As XPInstantFeedbackSource
        Dim service As New InventoryServiceXpo(company)
        Return service.ListDocumentInvoiceProductSales()
    End Function
    ''' <summary>
    ''' lista los productos con cantidades para devolver
    ''' </summary>
    ''' <param name="company"></param>
    ''' <param name="admissionNumber"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function ListPharmaceuticalDispensingDetailBatchSerialByAdmissionNumber(company As String, admissionNumber As String) As XPInstantFeedbackSource
        Dim service As New InventoryServiceXpo(company)
        Return service.ListPharmaceuticalDispensingDetailBatchSerialByAdmissionNumber(admissionNumber)
    End Function

    ''' <summary>
    ''' lista los productos con cantidades para devolver
    ''' </summary>
    ''' <param name="company"></param>
    ''' <param name="admissionNumber"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function ListPharmaceuticalDispensingDetailBatchSerialByAdmissionNumberXpcollection(company As String, admissionNumber As String) As XPCollection(Of PharmaceuticalDispensingDetailBatchSerialXpo)
        Dim service As New InventoryServiceXpo(company)
        Return service.ListPharmaceuticalDispensingDetailBatchSerialByAdmissionNumberXpcollection(admissionNumber)
    End Function

    ''' <summary>
    ''' Lista los detalles del control de inventario
    ''' </summary>
    ''' <param name="inventoryControlId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function ListInventoryControlDetailByInventoryControlId(company As String, inventoryControlId As Integer) As XPInstantFeedbackSource
        Dim service As New InventoryServiceXpo(company)
        Return service.ListInventoryControlDetailByInventoryControlId(inventoryControlId)
    End Function

    ''' <summary>
    ''' Lista los detalles de los detalles del control de inventario
    ''' </summary>
    ''' <param name="inventoryControlDetailId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function ListInventoryControlDetailBatchSerialByInventoryControlDetailId(company As String, inventoryControlDetailId As Integer) As XPInstantFeedbackSource
        Dim service As New InventoryServiceXpo(company)
        Return service.ListInventoryControlDetailBatchSerialByInventoryControlDetailId(inventoryControlDetailId)
    End Function

    ''' <summary>
    ''' Lista todas las devoluciones de ordenes de traslado
    ''' </summary>
    ''' <param name="company">Empresa a la que se encuentra conectado</param>
    ''' <returns>Lista de tipos de documento</returns>
    Public Shared Function ListTransferOrderDevolution(ByVal company As String) As XPInstantFeedbackSource
        Dim service As New InventoryServiceXpo(company)
        Return service.ListTransferOrderDevolution()
    End Function

    ''' <summary>
    ''' Lista todas las ordenes de traslado por estado
    ''' </summary>
    ''' <param name="company">Empresa a la que se encuentra conectado</param>
    ''' <returns>Lista de tipos de documento</returns>
    Public Shared Function ListTransferOrderByStatus(ByVal company As String, ByVal status As Integer) As XPInstantFeedbackSource
        Dim service As New InventoryServiceXpo(company)
        Return service.ListTransferOrderByStatus(status)
    End Function

    ''' <summary>
    ''' Lista todas las ordenes de traslado
    ''' </summary>
    ''' <param name="company">Empresa a la que se encuentra conectado</param>
    ''' <returns>Lista de tipos de documento</returns>
    Public Shared Function ListTransferOrder(ByVal company As String) As XPInstantFeedbackSource
        Dim service As New InventoryServiceXpo(company)
        Return service.ListTransferOrder()
    End Function

    ''' <summary>
    ''' Lista todas las solicitudes
    ''' </summary>
    ''' <param name="company">Empresa a la que se encuentra conectado</param>
    ''' <returns>Lista de tipos de documento</returns>
    Public Shared Function ListInventoryRequest(ByVal company As String) As XPInstantFeedbackSource
        Dim service As New InventoryServiceXpo(company)
        Return service.ListInventoryRequest()
    End Function

    ''' <summary>
    ''' Lista todas las remisiones de entrada por filtro
    ''' </summary>
    ''' <returns>Lista de remisiones de entrada</returns>
    Public Shared Function ListPurchaseOrderReportByFilter(ByVal company As String, ByVal Filtro As String) As XPInstantFeedbackSource
        Dim service As New InventoryServiceXpo(company)
        Return service.ListPurchaseOrderReportByFilter(Filtro)
    End Function

    ''' <summary>
    ''' Lista todas las remisiones de entrada por filtro
    ''' </summary>
    ''' <returns>Lista de remisiones de entrada</returns>
    Public Shared Function ListRemissionEntranceReportFilter(ByVal company As String, ByVal Filtro As String) As XPInstantFeedbackSource
        Dim service As New InventoryServiceXpo(company)
        Return service.ListRemissionEntranceReportFilter(Filtro)
    End Function

    ''' <summary>
    ''' lista todos comprobantes de entrada inventario por filtro
    ''' </summary>
    ''' <returns>Lista de tipos de documento</returns>
    Public Shared Function ListEntranceVoucherReportByFilter(ByVal company As String, ByVal Filtro As String) As XPInstantFeedbackSource
        Dim service As New InventoryServiceXpo(company)
        Return service.ListEntranceVoucherReportByFilter(Filtro)
    End Function

    ''' <summary>
    ''' Lista las ordenes de traslado por filtro
    ''' </summary>
    ''' <returns>Lista las devoluciones de remision por tipo</returns>
    Public Shared Function ListTransferOrderByFilter(ByVal company As String, ByVal Filtro As String) As XPInstantFeedbackSource
        Dim service As New InventoryServiceXpo(company)
        Return service.ListTransferOrderByFilter(Filtro)
    End Function

    ''' <summary>
    ''' Lista las devoluciones de remision por tipo
    ''' </summary>
    ''' <param name="company">Tipo de devolucion</param>
    ''' <returns>Lista las devoluciones de remision por tipo</returns>
    Public Shared Function ListRemissionDevolutionByTypeReport(ByVal company As String, ByVal Type As String) As XPInstantFeedbackSource
        Dim service As New InventoryServiceXpo(company)
        Return service.ListRemissionDevolutionByTypeReport(Type)
    End Function

    ''' <summary>
    ''' lista todos los comprobantes de entrada
    ''' </summary>
    ''' <param name="company">The company.</param>
    ''' <returns></returns>
    Public Shared Function ListEntranceVoucherReport(ByVal company As String) As XPInstantFeedbackSource
        Dim service As New InventoryServiceXpo(company)
        Return service.ListEntranceVoucherReport()
    End Function

    ''' <summary>
    ''' lista todas las ordenes de servicio
    ''' </summary>
    ''' <param name="company">The company.</param>
    ''' <returns></returns>
    Public Shared Function ListPurchaseOrderReport(ByVal company As String) As XPInstantFeedbackSource
        Dim service As New InventoryServiceXpo(company)
        Return service.ListPurchaseOrderReport()
    End Function

    ''' <summary>
    ''' lista todas las remissiones de Entrada
    ''' </summary>
    ''' <param name="company">The company.</param>
    ''' <returns></returns>
    Public Shared Function ListRemissionEntranceReport(ByVal company As String) As XPInstantFeedbackSource
        Dim service As New InventoryServiceXpo(company)
        Return service.ListRemissionEntranceReport
    End Function

    ''' <summary>
    ''' lista todos los proveedores
    ''' </summary>
    ''' <param name="company">The company.</param>
    ''' <returns></returns>
    Public Shared Function ListSupplierInventoryReport(ByVal company As String) As XPInstantFeedbackSource
        Dim service As New InventoryServiceXpo(company)
        Return service.ListSupplierInventoryReport()
    End Function

    ''' <summary>
    ''' lista todos los Clientes
    ''' </summary>
    ''' <param name="company">The company.</param>
    ''' <returns></returns>
    Public Shared Function ListCustomerInventoryReport(ByVal company As String) As XPInstantFeedbackSource
        Dim service As New InventoryServiceXpo(company)
        Return service.ListCustomerInventoryReport()
    End Function

    ''' <summary>
    ''' lista todos los productos
    ''' </summary>
    ''' <param name="company">The company.</param>
    ''' <returns></returns>
    Public Shared Function ListProductsReport(ByVal company As String) As XPInstantFeedbackSource
        Dim service As New InventoryServiceXpo(company)
        Return service.ListProductsReport()
    End Function

    ''' <summary>
    ''' lista todos los grupos de clase producto
    ''' </summary>
    ''' <param name="company">The company.</param>
    ''' <returns></returns>
    Public Shared Function ListGroupReport(ByVal company As String) As XPInstantFeedbackSource
        Dim service As New InventoryServiceXpo(company)
        Return service.ListGroupReport()
    End Function

    ''' <summary>
    ''' lista todos los subgrupos
    ''' </summary>
    ''' <param name="company">The company.</param>
    ''' <returns></returns>
    Public Shared Function ListSubGroupReport(ByVal company As String) As XPInstantFeedbackSource
        Dim service As New InventoryServiceXpo(company)
        Return service.ListSubGroupReport()
    End Function

    ''' <summary>
    ''' lista todos los almacenes
    ''' </summary>
    ''' <param name="company">The company.</param>
    ''' <returns></returns>
    Public Shared Function ListWarehouseReport(ByVal company As String) As XPInstantFeedbackSource
        Dim service As New InventoryServiceXpo(company)
        Return service.ListWarehouseReport()
    End Function

    ''' <summary>
    ''' lista todos los lotes
    ''' </summary>
    ''' <param name="company">The company.</param>
    ''' <returns></returns>
    Public Shared Function ListBatchSerialReport(ByVal company As String) As XPInstantFeedbackSource
        Dim service As New InventoryServiceXpo(company)
        Return service.ListBatchSerialReport()
    End Function

    ''' <summary>
    ''' lista las devoluciones de las remisiones
    ''' </summary>
    ''' <param name="company"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function ListAllRemissionDevolution(company As String) As XPInstantFeedbackSource
        Dim service As New InventoryServiceXpo(company)
        Return service.ListAllRemissionDevolution()
    End Function

    ''' <summary>
    ''' lista las devoluciones de suministros
    ''' </summary>
    ''' <param name="company"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function ListAllPharmaceuticalDispensingDevolution(company As String) As XPInstantFeedbackSource
        Dim service As New InventoryServiceXpo(company)
        Return service.ListAllPharmaceuticalDispensingDevolution()
    End Function
    ''' <summary>
    ''' lista las remisiones de salida por unidad operativa
    ''' </summary>
    ''' <param name="company"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function ListAllRemissionOutput(company As String) As XPInstantFeedbackSource
        Dim service As New InventoryServiceXpo(company)
        Return service.ListAllRemissionOutput()
    End Function
    ''' <summary>
    ''' lista las remisiodnes de salida por estado
    ''' </summary>
    ''' <param name="company"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function ListRemissionOutputByStatus(company As String, Optional status As Integer = 2) As XPInstantFeedbackSource
        Dim service As New InventoryServiceXpo(company)
        Return service.ListRemissionOutputByStatus(status)
    End Function

    ''' <summary>
    ''' Lista todas las dispensaciones farmaceuticas
    ''' </summary>
    ''' <param name="company">The company.</param>
    ''' <returns></returns>
    Public Shared Function ListPharmaceuticalDispensing(company As String) As XPInstantFeedbackSource
        Dim service As New InventoryServiceXpo(company)
        Return service.ListPharmaceuticalDispensing()
    End Function

    ''' <summary>
    ''' lista las remisiones de entrada por unidad operativa
    ''' </summary>
    ''' <param name="company"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function ListAllRemissionEntrance(company As String) As XPInstantFeedbackSource
        Dim service As New InventoryServiceXpo(company)
        Return service.ListAllRemissionEntrance()
    End Function

    ''' <summary>
    ''' lista las remisiones de entrada por unidad operativa
    ''' </summary>
    ''' <param name="company"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function ListAllLoanMerchandiseByWareHouseIdAndConfirm(company As String, IdStores As Integer) As XPInstantFeedbackSource
        Dim service As New InventoryServiceXpo(company)
        Return service.ListAllLoanMerchandiseByWareHouseIdAndConfirm(IdStores)
    End Function

    ''' <summary>
    ''' lista de solicitudes de prestamo
    ''' </summary>
    ''' <param name="company"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function ListAllLoanMerchandise(company As String) As XPInstantFeedbackSource
        Dim service As New InventoryServiceXpo(company)
        Return service.ListAllLoanMerchandise()
    End Function

    ''' <summary>
    ''' lista evolucion de prestamo
    ''' </summary>
    ''' <param name="company"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function ListAllLoanMerchandiseDevolutions(company As String) As XPInstantFeedbackSource
        Dim service As New InventoryServiceXpo(company)
        Return service.ListAllLoanMerchandiseDevolutions()
    End Function

    ''' <summary>
    ''' lista evolucion de prestamo
    ''' </summary>
    ''' <param name="company"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function ListInventoryProductFrmStock(company As String) As XPCollection(Of Infrastructure.Data.Xpo.InventoryRepository.InventoryProductXpo)
        Dim service As New InventoryServiceXpo(company)
        Return service.ListInventoryProductFrmStock()
    End Function

    Public Shared Function ListInventoryProductByATCCode(company As String, atcCode As String) As XPInstantFeedbackSource
        Dim service As New InventoryServiceXpo(company)
        Return service.ListInventoryProductByATCCode(atcCode)
    End Function

    ''' <summary>
    ''' lista las remisiones de entrada por estado
    ''' </summary>
    ''' <param name="company"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function ListRemissionEntranceByStatus(company As String, Optional status As Integer = 2) As XPInstantFeedbackSource
        Dim service As New InventoryServiceXpo(company)
        Return service.ListRemissionEntranceByStatus(status)
    End Function

    ''' <summary>
    ''' lista los lotes por id del producto
    ''' </summary>
    ''' <param name="company"></param>
    ''' <param name="ProductId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function ListBatchSerialByProductId(company As String, ProductId As Integer) As XPInstantFeedbackSource
        Dim service As New InventoryServiceXpo(company)
        Return service.ListBatchSerialByProductId(ProductId)
    End Function

    ''' <summary>
    ''' lista los almacenes por id del producto
    ''' </summary>
    ''' <param name="company"></param>
    ''' <param name="ProductId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function ListPhysicalInventoryByProductId(company As String, ProductId As Integer) As XPCollection(Of PhysicalInventoryXpo)
        Dim service As New InventoryServiceXpo(company)
        Return service.ListPhysicalInventoryByProductId(ProductId)
    End Function

    ''' <summary>
    ''' lista todos los subdetalles de la remision de entrada por proveedor y linea de distribuccion
    ''' </summary>
    ''' <param name="company"></param>
    ''' <param name="SupplierId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function ListRemissionEntranceDetailBatchSerialBySupplierIdAndSupplierDistributionLineId(company As String, SupplierId As Integer, SupplierDistributionLineId As Integer) As XPCollection
        Dim service As New InventoryServiceXpo(company)
        Return service.ListRemissionEntranceDetailBatchSerialBySupplierIdAndSupplierDistributionLineId(SupplierId, SupplierDistributionLineId)
    End Function

    ''' <summary>
    ''' lista todos los detalles del contrato por proveedor y linea de distribuccion
    ''' </summary>
    ''' <param name="company"></param>
    ''' <param name="SupplierId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function ListInventoryContractDetailBySupplierIdAndSupplierDistributionLineId(company As String, SupplierId As Integer, SupplierDistributionLineId As Integer) As XPCollection
        Dim service As New InventoryServiceXpo(company)
        Return service.ListInventoryContractDetailBySupplierIdAndSupplierDistributionLineId(SupplierId, SupplierDistributionLineId)
    End Function

    ''' <summary>
    ''' lista todos los detalles de la orden de compra por proveedor y linea de distribuccion
    ''' </summary>
    ''' <param name="company"></param>
    ''' <param name="SupplierId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function ListPurchaseOrderDetailBySupplierIdAndSupplierDistributionLineId(company As String, SupplierId As Integer, SupplierDistributionLineId As Integer) As XPCollection
        Dim service As New InventoryServiceXpo(company)
        Return service.ListPurchaseOrderDetailBySupplierIdAndSupplierDistributionLineId(SupplierId, SupplierDistributionLineId)
    End Function

    ''' <summary>
    ''' lista todos los detalles de las solicitudes por tipo de orden, despachado a y almacen o unidad funcional dependiendo a cual se despacho
    ''' </summary>
    ''' <param name="company"></param>
    ''' <param name="filterFunctionalUnitWarehouse"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function ListRequestDetailByFilterFunctionalUnitWarehouseAndOrderTypeAndDispatchTo(company As String, filterFunctionalUnitWarehouse As Integer, orderType As Byte, dispatchTo As Byte) As XPCollection
        Dim service As New InventoryServiceXpo(company)
        Return service.ListRequestDetailByFilterFunctionalUnitWarehouseAndOrderTypeAndDispatchTo(filterFunctionalUnitWarehouse, orderType, dispatchTo)
    End Function
    ''' <summary>
    ''' lista los inventarios fisicios filtrados por id del almacen
    ''' </summary>
    ''' <param name="company"></param>
    ''' <param name="WarehouseId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function ListPhysicalInventoryByWarehouseIdFrmStock(company As String, WarehouseId As Integer) As XPCollection(Of PhysicalInventoryXpo)
        Dim service As New InventoryServiceXpo(company)
        Return service.ListPhysicalInventoryByWarehouseIdFrmStock(WarehouseId)
    End Function

    ''' <summary>
    ''' lista los almacenes por id del producto
    ''' </summary>
    ''' <param name="company"></param>
    ''' <param name="WarehouseId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function ListPhysicalInventoryByWarehouseId(company As String, WarehouseId As Integer) As XPInstantFeedbackSource
        Dim service As New InventoryServiceXpo(company)
        Return service.ListPhysicalInventoryByWarehouseId(WarehouseId)
    End Function

    ''' <summary>
    ''' Lista los cubrimientos de productos
    ''' </summary>
    ''' <param name="company">The company.</param>
    ''' <returns></returns>
    Public Shared Function ListProductTemplateInventory(ByVal company As String) As XPInstantFeedbackSource
        Dim service As New InventoryServiceXpo(company)
        Return service.ListProductTemplate()
    End Function

    ''' <summary>
    ''' Lista los productos por clase de tipo de producto
    ''' </summary>
    ''' <param name="company">The company.</param>
    ''' <returns></returns>
    Public Shared Function ListDiagnostic(ByVal company As String) As XPInstantFeedbackSource
        Dim service As New InventoryServiceXpo(company)
        Return service.ListDiagnostic()
    End Function


    ''' <summary>
    ''' diagnostico por id
    ''' </summary>
    ''' <param name="company">The company.</param>
    ''' <returns></returns>
    Public Shared Function ListDiagnosticById(ByVal company As String, id As Integer) As XPCollection(Of DiagnosticXpo)
        Dim service As New InventoryServiceXpo(company)
        Return service.ListDiagnosticById(id)
    End Function

    ''' <summary>
    ''' Lista los productos por clase de tipo de producto
    ''' </summary>
    ''' <param name="company">The company.</param>
    ''' <returns></returns>
    Public Shared Function LostPOSPathologies(ByVal company As String) As XPInstantFeedbackSource
        Dim service As New InventoryServiceXpo(company)
        Return service.LostPOSPathologies()
    End Function

    ''' <summary>
    ''' Lista los productos por clase de tipo de producto
    ''' </summary>
    ''' <param name="company">The company.</param>
    ''' <returns></returns>
    Public Shared Function ListInventoryProductByNoClassType(ByVal company As String, ByVal ClassProductType As Integer) As XPInstantFeedbackSource
        Dim service As New InventoryServiceXpo(company)
        Return service.ListInventoryProductByNoClassType(ClassProductType)
    End Function

    ''' <summary>
    ''' Lista los productos por clase de tipo de producto
    ''' </summary>
    ''' <param name="company">The company.</param>
    ''' <returns></returns>
    Public Shared Function ListInventoryProductByClassTypeAndHandlesBatch(ByVal company As String, status As Boolean, ByVal ClassProductType As Integer) As XPInstantFeedbackSource
        Dim service As New InventoryServiceXpo(company)
        Return service.ListInventoryProductByClassTypeAndHandlesBatch(status, ClassProductType)
    End Function

    ''' <summary>
    ''' Lista los productos por clase de tipo de producto
    ''' </summary>
    ''' <param name="company">The company.</param>
    ''' <returns></returns>
    Public Shared Function ListInventoryProductByStatusByNoClassType(ByVal company As String, status As Boolean, ByVal ClassProductType As Integer) As XPInstantFeedbackSource
        Dim service As New InventoryServiceXpo(company)
        Return service.ListInventoryProductByStatusByNoClassType(status, ClassProductType)
    End Function

    ''' <summary>
    ''' Lista los productos por clase de tipo de producto
    ''' </summary>
    ''' <param name="company">The company.</param>
    ''' <returns></returns>
    Public Shared Function ListInventoryProductByProductType(ByVal company As String, ByVal ClassProductType As Integer) As XPInstantFeedbackSource
        Dim service As New InventoryServiceXpo(company)
        Return service.ListInventoryProductByProductType(ClassProductType)
    End Function

    Public Shared Function ListInventoryProductByProductTypeClasses(company As String, productTypeClasses As List(Of String)) As XPInstantFeedbackSource
        Dim service As New InventoryServiceXpo(company)
        Return service.ListInventoryProductByProductTypeClasses(productTypeClasses)
    End Function

    ''' <summary>
    ''' Lista todos los productos
    ''' </summary>
    ''' <param name="company">The company.</param>
    ''' <returns></returns>
    Public Shared Function ListInventoryProduct(ByVal company As String) As XPInstantFeedbackSource
        Dim service As New InventoryServiceXpo(company)
        Return service.ListInventoryProduct()
    End Function

    ''' <summary>
    ''' Lists the atc.
    ''' </summary>
    ''' <param name="company">The company.</param>
    ''' <returns></returns>
    Public Shared Function ListATC(ByVal company As String) As XPInstantFeedbackSource
        Dim service As New InventoryServiceXpo(company)
        Return service.ListATC()
    End Function

    ''' <summary>
    ''' Lists the general ledger iva.
    ''' </summary>
    ''' <param name="company">The company.</param>
    ''' <returns></returns>
    Public Shared Function ListGeneralLedgerIva(ByVal company As String) As XPInstantFeedbackSource
        Dim service As New InventoryServiceXpo(company)
        Return service.ListGeneralLedgerIva()
    End Function

    ''' <summary>
    ''' Lists the packaging unit.
    ''' </summary>
    ''' <param name="company">The company.</param>
    ''' <returns></returns>
    Public Shared Function ListPackagingUnit(ByVal company As String) As XPInstantFeedbackSource
        Dim service As New InventoryServiceXpo(company)
        Return service.ListPackagingUnit()
    End Function

    ''' <summary>
    ''' Lista todos los grupos
    ''' </summary>
    ''' <param name="company">Empresa a la que se encuentra conectado</param>
    ''' <returns>Lista de tipos de documento</returns>
    Public Shared Function ListProductGroup(ByVal company As String) As XPInstantFeedbackSource
        Dim service As New InventoryServiceXpo(company)
        Return service.ListProductGroup()
    End Function

    ''' <summary>
    ''' Lista todos los subgrupos
    ''' </summary>
    ''' <param name="company">Empresa a la que se encuentra conectado</param>
    ''' <returns>Lista de tipos de documento</returns>
    Public Shared Function ListProductSubGroup(ByVal company As String) As XPInstantFeedbackSource
        Dim service As New InventoryServiceXpo(company)
        Return service.ListProductSubGroup()
    End Function

    ''' <summary>
    ''' Lista todos las unidades de medida
    ''' </summary>
    ''' <param name="company">Empresa a la que se encuentra conectado</param>
    ''' <returns>Lista de tipos de documento</returns>
    Public Shared Function ListMeasureUnitByType(ByVal company As String, ByVal unitType As Byte) As XPInstantFeedbackSource
        Dim service As New InventoryServiceXpo(company)
        Return service.ListMeasureUnitByType(unitType)
    End Function

    ''' <summary>
    ''' Lista todos las unidades de medida
    ''' </summary>
    ''' <param name="company">Empresa a la que se encuentra conectado</param>
    ''' <returns>Lista de tipos de documento</returns>
    Public Shared Function ListMeasureUnit(ByVal company As String) As XPInstantFeedbackSource
        Dim service As New InventoryServiceXpo(company)
        Return service.ListMeasureUnit()
    End Function

    ''' <summary>
    ''' Lista todos los almacenes
    ''' </summary>
    ''' <param name="company">Empresa a la que se encuentra conectado</param>
    ''' <returns>Lista de tipos de documento</returns>
    Public Shared Function ListWarehouse(ByVal company As String) As XPInstantFeedbackSource
        Dim service As New InventoryServiceXpo(company)
        Return service.ListWarehouse()
    End Function

    ''' <summary>
    ''' Lista todos los almacenes
    ''' </summary>
    ''' <param name="company">Empresa a la que se encuentra conectado</param>
    ''' <returns>Lista de tipos de documento</returns>
    Public Shared Function ListWarehouseWithoutVirtualStore(ByVal company As String) As XPInstantFeedbackSource
        Dim service As New InventoryServiceXpo(company)
        Return service.ListWarehouseWithoutVirtualStore()
    End Function

    ''' <summary>
    ''' Lista los almacenes por estado y por usuario
    ''' </summary>
    ''' <param name="company">Empresa a la que se encuentra conectado</param>
    ''' <returns>Lista de tipos de documento</returns>
    Public Shared Function ListWarehouseByStatus(ByVal company As String, status As Boolean) As XPInstantFeedbackSource
        Dim service As New InventoryServiceXpo(company)
        Return service.ListWarehouseByStatus(status)
    End Function

    ''' <summary>
    ''' Lista los almacenes por estado y por usuario
    ''' </summary>
    ''' <param name="company">Empresa a la que se encuentra conectado</param>
    ''' <returns>Lista de tipos de documento</returns>
    Public Shared Function ListWarehouseByStatusAndUser(ByVal company As String, status As Boolean, codeUser As String) As XPInstantFeedbackSource
        Dim service As New InventoryServiceXpo(company)
        Return service.ListWarehouseByStatusAndUser(status, codeUser)
    End Function

    ''' <summary>
    ''' Lista los almacenes por estado y por usuario
    ''' </summary>
    ''' <param name="company">Empresa a la que se encuentra conectado</param>
    ''' <returns>Lista de tipos de documento</returns>
    Public Shared Function ListWarehouseByStatusAndUserXpcollection(ByVal company As String, status As Boolean, codeUser As String) As XPCollection(Of WarehouseXpo)
        Dim service As New InventoryServiceXpo(company)
        Return service.ListWarehouseByStatusAndUserXpCollection(status, codeUser)
    End Function

    ''' <summary>
    ''' Lista todos los grupos farmacologicos
    ''' </summary>
    ''' <param name="company">Empresa a la que se encuentra conectado</param>
    ''' <returns>Lista de tipos de documento</returns>
    Public Shared Function ListPharmacologicalGroup(ByVal company As String) As XPInstantFeedbackSource
        Dim service As New InventoryServiceXpo(company)
        Return service.ListPharmacologicalGroup()
    End Function

    ''' <summary>
    ''' Lista todos los conceptos de ajuste
    ''' </summary>
    ''' <param name="company">Empresa a la que se encuentra conectado</param>
    ''' <returns>Lista de tipos de documento</returns>
    Public Shared Function ListAdjustmentConcept(ByVal company As String) As XPInstantFeedbackSource
        Dim service As New InventoryServiceXpo(company)
        Return service.ListAdjustmentConcept()
    End Function

    ''' <summary>
    ''' Lista todos los conceptos de ajuste por tipo
    ''' </summary>
    ''' <param name="Type"></param>
    ''' <param name="company"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function ListAdjustmentConceptByType(ByVal Type As Byte, ByVal company As String) As XPInstantFeedbackSource
        Dim service As New InventoryServiceXpo(company)
        Return service.ListAdjustmentConceptByType(Type)
    End Function

    ''' <summary>
    ''' Lista todos los conceptos de ajuste por tipo
    ''' </summary>
    ''' <param name="Type"></param>
    ''' <param name="company"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function ListAdjustmentConceptByTypeAndMovement(ByVal Type As Byte, ByVal Movement As Byte, ByVal company As String) As XPInstantFeedbackSource
        Dim service As New InventoryServiceXpo(company)
        Return service.ListAdjustmentConceptByConceptTypeAndMovement(Type, Movement, True)
    End Function

    ''' <summary>
    ''' Lista todos los fabricantes
    ''' </summary>
    ''' <param name="company">Empresa a la que se encuentra conectado</param>
    ''' <returns>Lista de tipos de documento</returns>
    Public Shared Function ListManufacturers(ByVal company As String) As XPInstantFeedbackSource
        Dim service As New InventoryServiceXpo(company)
        Return service.ListManufacturers()
    End Function

    ''' <summary>
    ''' Lista otras deducciones o retenciones
    ''' </summary>
    ''' <param name="company">Empresa a la que se encuentra conectado</param>
    ''' <returns>Lista de tipos de documento</returns>
    Public Shared Function ListOtherWitholdingDeductions(ByVal company As String) As XPInstantFeedbackSource
        Dim service As New InventoryServiceXpo(company)
        Return service.ListOtherWitholdingDeductions()
    End Function

    ''' <summary>
    ''' Lista los tipos de productos
    ''' </summary>
    ''' <param name="company">Empresa a la que se encuentra conectado</param>
    ''' <returns>Lista de tipos de documento</returns>
    Public Shared Function ListProductType(ByVal company As String) As XPInstantFeedbackSource
        Dim service As New InventoryServiceXpo(company)
        Return service.ListProductType()
    End Function

    ''' <summary>
    ''' Lista los conceptos de movimiento de inventario por tipo
    ''' </summary>
    ''' <param name="company">Empresa a la que se encuentra conectado</param>
    ''' <returns>Lista de conceptos de movimiento</returns>
    Public Shared Function ListAdjustmentConceptByConceptType(ByVal movementClass As Integer, ByVal concepType As Integer, ByVal status As Boolean, ByVal company As String) As XPInstantFeedbackSource
        Dim service As New InventoryServiceXpo(company)
        Return service.ListAdjustmentConceptByConceptTypeAndMovement(concepType, movementClass, status)
    End Function

    ''' <summary>
    ''' Lista los tipos de productos
    ''' </summary>
    ''' <param name="company">Empresa a la que se encuentra conectado</param>
    ''' <returns>Lista de tipos de documento</returns>
    Public Shared Function ListProductTypeByStatus(ByVal status As Boolean, ByVal company As String) As XPInstantFeedbackSource
        Dim service As New InventoryServiceXpo(company)
        Return service.ListProductTypeByStatus(status)
    End Function

    ''' <summary>
    ''' Lista los atributos de tipos de productos
    ''' </summary>
    ''' <param name="company">Empresa a la que se encuentra conectado</param>
    ''' <returns>Lista de tipos de documento</returns>
    Public Shared Function ListAttributeProductType(ByVal company As String) As XPInstantFeedbackSource
        Dim service As New InventoryServiceXpo(company)
        Return service.ListAttributeProductType()
    End Function

    ''' <summary>
    ''' Lista todos los DCI
    ''' </summary>
    ''' <param name="company">Empresa a la que se encuentra conectado</param>
    ''' <returns>Lista de tipos de documento</returns>
    Public Shared Function ListDCI(ByVal company As String) As XPInstantFeedbackSource
        Dim service As New InventoryServiceXpo(company)
        Return service.ListDCI()
    End Function

    ''' <summary>
    ''' Lista los DCI por estado
    ''' </summary>
    ''' <param name="company">Empresa a la que se encuentra conectado</param>
    ''' <returns>Lista de tipos de documento</returns>
    Public Shared Function ListDCIByStatus(ByVal status As Boolean, ByVal company As String) As XPInstantFeedbackSource
        Dim service As New InventoryServiceXpo(company)
        Return service.ListDCIByStatus(status)
    End Function

    ''' <summary>
    ''' Lista todas las formas farmaceutica
    ''' </summary>
    ''' <param name="company">Empresa a la que se encuentra conectado</param>
    ''' <returns>Lista de tipos de documento</returns>
    Public Shared Function ListPharmaceuticalForm(ByVal company As String) As XPInstantFeedbackSource
        Dim service As New InventoryServiceXpo(company)
        Return service.ListPharmaceuticalForm()
    End Function

    ''' <summary>
    ''' Lista las formas farmaceuticas por estado
    ''' </summary>
    ''' <param name="company">Empresa a la que se encuentra conectado</param>
    ''' <returns>Lista de tipos de documento</returns>
    Public Shared Function ListPharmaceuticalFormByStatus(ByVal status As Boolean, ByVal company As String) As XPInstantFeedbackSource
        Dim service As New InventoryServiceXpo(company)
        Return service.ListPharmaceuticalFormByStatus(status)
    End Function

    ''' <summary>
    ''' Lista todas las vias de administracion
    ''' </summary>
    ''' <param name="company">Empresa a la que se encuentra conectado</param>
    ''' <returns>Lista de tipos de documento</returns>
    Public Shared Function ListAdministrationRoute(ByVal company As String) As XPInstantFeedbackSource
        Dim service As New InventoryServiceXpo(company)
        Return service.ListAdministrationRoute()
    End Function

    ''' <summary>
    ''' Lista todos los niveles de riesgo
    ''' </summary>
    ''' <returns>Lista de tipos de documento</returns>
    Public Shared Function ListInventoryRiskLevel(ByVal company As String) As XPInstantFeedbackSource
        Dim service As New InventoryServiceXpo(company)
        Return service.ListRiskLevel()
    End Function

    ''' <summary>
    ''' Lista todos los DCI
    ''' </summary>
    ''' <returns>Lista de tipos de documento</returns>
    Public Shared Function ListDCI(ByVal company As String, ByVal DCIId As String) As XPInstantFeedbackSource
        Dim service As New InventoryServiceXpo(company)
        Return service.ListDCI(DCIId)
    End Function

    ''' <summary>
    ''' Lista todos los Contract Type
    ''' </summary>
    ''' <returns>Lista de tipos de contrato</returns>
    Public Shared Function ListInventoryContractType(ByVal company As String) As XPInstantFeedbackSource
        Dim service As New InventoryServiceXpo(company)
        Return service.ListInventoryContractType()
    End Function

    ''' <summary>
    ''' Lista todos los Contract Type
    ''' </summary>
    ''' <returns>Lista de tipos de contrato</returns>
    Public Shared Function ListInventoryContractTypeByStatus(ByVal company As String, ByVal Status As Boolean) As XPInstantFeedbackSource
        Dim service As New InventoryServiceXpo(company)
        Return service.ListInventoryContractTypeByStatus(Status)
    End Function

    ''' <summary>
    ''' Lista todos los Contract
    ''' </summary>
    ''' <returns>Lista de tipos de contrato</returns>
    Public Shared Function ListInventoryContract(ByVal company As String) As XPInstantFeedbackSource
        Dim service As New InventoryServiceXpo(company)
        Return service.ListAllInventoryContract()
    End Function

    ''' <summary>
    ''' Lista todos los Contract
    ''' </summary>
    ''' <returns>Lista de tipos de contrato</returns>
    Public Shared Function ListInventoryContractByStatus(ByVal company As String, ByVal Status As Byte) As XPInstantFeedbackSource
        Dim service As New InventoryServiceXpo(company)
        Return service.ListInventoryContractByStatus(Status)
    End Function

    ''' <summary>
    ''' Lista todos los Contract
    ''' </summary>
    ''' <returns>Lista de tipos de contrato</returns>
    Public Shared Function ListEntranceVoucher(ByVal company As String) As XPInstantFeedbackSource
        Dim service As New InventoryServiceXpo(company)
        Return service.ListEntranceVoucher()
    End Function

    ''' <summary>
    ''' Lista todos los Contract
    ''' </summary>
    ''' <returns>Lista de tipos de contrato</returns>
    Public Shared Function ListEntranceVoucherByStatus(ByVal company As String, ByVal Status As Byte) As XPInstantFeedbackSource
        Dim service As New InventoryServiceXpo(company)
        Return service.ListEntranceVoucherByStatus(Status)
    End Function

    ''' <summary>
    ''' Lista todos los Contract
    ''' </summary>
    ''' <returns>Lista de tipos de contrato</returns>
    Public Shared Function ListEntranceVoucherDevolution(ByVal company As String) As XPInstantFeedbackSource
        Dim service As New InventoryServiceXpo(company)
        Return service.ListEntranceVoucherDevolution()
    End Function

    ''' <summary>
    ''' Lista todos los Contract
    ''' </summary>
    ''' <returns>Lista de tipos de contrato</returns>
    Public Shared Function ListEntranceVoucherDevolutionByStatus(ByVal company As String, ByVal Status As Byte) As XPInstantFeedbackSource
        Dim service As New InventoryServiceXpo(company)
        Return service.ListEntranceVoucherDevolutionByStatus(Status)
    End Function

    ''' <summary>
    ''' Lista todos los Contract
    ''' </summary>
    ''' <returns>Lista de tipos de contrato</returns>
    Public Shared Function ListInventoryControl(ByVal company As String) As XPInstantFeedbackSource
        Dim service As New InventoryServiceXpo(company)
        Return service.ListInventoryControl()
    End Function

    ''' <summary>
    ''' Lista todos los Contract
    ''' </summary>
    ''' <returns>Lista de tipos de contrato</returns>
    Public Shared Function ListInventoryControlByStatus(ByVal company As String, ByVal Status As Byte) As XPInstantFeedbackSource
        Dim service As New InventoryServiceXpo(company)
        Return service.ListInventoryControlByStatus(Status)
    End Function

    ''' <summary>
    ''' Lista todos los inventory Adjustment
    ''' </summary>
    ''' <returns>Lista de tipos de contrato</returns>
    Public Shared Function ListInventoryAdjustment(ByVal company As String) As XPInstantFeedbackSource
        Dim service As New InventoryServiceXpo(company)
        Return service.ListInventoryAdjustment()
    End Function

    ''' <summary>
    ''' Lista todos los inventory Adjustment
    ''' </summary>
    ''' <returns>Lista de tipos de contrato</returns>
    Public Shared Function ListInventoryAdjustmentByStatus(ByVal company As String, ByVal Status As Byte) As XPInstantFeedbackSource
        Dim service As New InventoryServiceXpo(company)
        Return service.ListInventoryAdjustmentByStatus(Status)
    End Function

    ''' <summary>
    ''' Lista todos los Contract
    ''' </summary>
    ''' <returns>Lista de tipos de contrato</returns>
    Public Shared Function ListInventoryControlByDocumentType(ByVal company As String, ByVal DocumentType As Byte) As XPInstantFeedbackSource
        Dim service As New InventoryServiceXpo(company)
        Return service.ListInventoryControlByDocumentType(DocumentType)
    End Function

    ''' <summary>
    ''' Lista todos los Contract
    ''' </summary>
    ''' <returns>Lista de tipos de contrato</returns>
    Public Shared Function ListAllInventoryPurchaserOrder(ByVal company As String) As XPInstantFeedbackSource
        Dim service As New InventoryServiceXpo(company)
        Return service.ListAllInventoryPurchaserOrder()
    End Function

    ''' <summary>
    ''' Lists the general ledger voucher types
    ''' </summary>
    ''' <param name="company">The company.</param>
    ''' <returns></returns>
    Public Shared Function ListAllInventoryGeneralLedgerJournalVoucherTypes(ByVal company) As XPInstantFeedbackSource
        Dim service As New InventoryServiceXpo(company)
        Return service.ListGeneralLedgerJournalVoucherTypes()
    End Function
    'Public Shared Function ListAllInventoryGeneralLedgerJournalVoucherTypes(ByVal company As String) As XPCollection(Of GeneralLedgerJournalVoucherTypesXpo)
    '    Dim service As New InventoryServiceXpo(company)
    '    Return service.ListGeneralLedgerJournalVoucherTypes()
    'End Function

    ''' <summary>
    ''' Lista los detalles de productRate
    ''' </summary>
    ''' <param name="company">Empresa a la que se encuentra conectado</param>
    Public Shared Function ListProductRateDetailByProductRateId(productRateId As Integer, ByVal company As String) As XPCollection
        Dim service As New InventoryServiceXpo(company)
        Return service.ListProductRateDetailByProductRateId(productRateId)
    End Function

    ''' <summary>
    ''' lista reporte de DashboardPharmacy por filtros---------------------------
    ''' </summary>
    ''' <param name="company">Empresa a la que se encuentra conectado</param>
    Public Shared Function ListViewDashboardPharmacyFilters(ByVal Filter As String, ByVal company As String) As XPCollection(Of InventoryDashboardPharmacyReportXpo)
        Dim service As New InventoryServiceXpo(company)
        Return service.ListViewDashboardPharmacyFilters(Filter)
    End Function

    ''' <summary>
    ''' lista reporte de PharmaceuticalDispensingDevolutionDeytail por filtros---------------------------
    ''' </summary>
    ''' <param name="company">Empresa a la que se encuentra conectado</param>
    Public Shared Function ListViewPharmaceuticalDispensingDevolutionDetailFilters(ByVal Filter As String, ByVal company As String) As XPCollection(Of InventoryPharmaceuticalDispensingDevolutionDetailReportXpo)
        Dim service As New InventoryServiceXpo(company)
        Return service.ListViewPharmaceuticalDispensingDevolutionDetailFilters(Filter)
    End Function

    ''' <summary>
    ''' lista reporte de PharmaceuticalDispensingDevolution por filtros---------------------------
    ''' </summary>
    ''' <param name="company">Empresa a la que se encuentra conectado</param>
    Public Shared Function ListViewPharmaceuticalDispensingDevolutionFilters(ByVal Filter As String, ByVal company As String) As XPCollection(Of InventoryPharmaceuticalViewDispensingDevolutionReportXpo)
        Dim service As New InventoryServiceXpo(company)
        Return service.ListViewPharmaceuticalDispensingDevolutionFilters(Filter)
    End Function

    ''' <summary>
    ''' lista reporte de PharmaceuticalDispensing por filtros---------------------------
    ''' </summary>
    ''' <param name="company">Empresa a la que se encuentra conectado</param>
    Public Shared Function ListViewPharmaceuticalDispensingFilters(ByVal Filter As String, ByVal company As String) As XPCollection(Of InventoryPharmaceuticalViewDispensingReportXpo)
        Dim service As New InventoryServiceXpo(company)
        Return service.ListViewPharmaceuticalDispensingFilters(Filter)
    End Function

    ''' <summary>
    ''' lista los documentos de control por tipo de documento
    ''' </summary>
    ''' <param name="company"></param>
    ''' <param name="documentType"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function ListInventoryControlDocumentByDocumentType(company As String, documentType As Integer) As XPCollection(Of InventoryControlDocumentXpo)
        Dim service As New InventoryServiceXpo(company)
        Return service.ListInventoryControlDocumentByDocumentType(documentType)
    End Function
#End Region

#Region "InteropCost"

    ''' <summary>
    ''' Lista las cuentas contables
    ''' </summary>
    ''' <returns></returns>
    Public Shared Function ListMainAccountLabor(ByVal companyPayroll As String, ByVal companyCost As String) As XPInstantFeedbackSource
        Dim service As New PayrollServicesXpo(companyPayroll)
        Dim _listConceptAccountingStructure As XPCollection(Of Infrastructure.Data.Xpo.PayrollRepository.ConceptAccountingStructureXpo) = service.ListConceptAccountingStructure()
        Dim mainAccountAccrued As List(Of String) = (From o In _listConceptAccountingStructure Where o.AccruedAccount IsNot Nothing Select o.AccruedAccount).Distinct().ToList()
        Dim mainAccountDeducted As List(Of String) = (From o In _listConceptAccountingStructure Where o.DeductedAccount IsNot Nothing Select o.DeductedAccount).Distinct().ToList()
        'Dim mainAccountAccrued As List(Of String) = _listConceptAccountingStructure.ToList().Where(Function(x) x.AccruedAccount IsNot Nothing).Select(Function(y) y.AccruedAccount).Distinct().ToList()
        'Dim mainAccountDeducted As List(Of String) = _listConceptAccountingStructure.ToList().Where(Function(x) x.DeductedAccount IsNot Nothing).Select(Function(y) y.DeductedAccount).Distinct().ToList()
        Dim service1 As New InteropCostServiceXpo(companyCost)
        Return service1.ListMainAccountByNumberAccountList(mainAccountAccrued.Union(mainAccountDeducted).ToList().Distinct().ToList())
    End Function

    ''' <summary>
    ''' Lists the main account deprecation.
    ''' </summary>
    Public Shared Function ListMainAccountDeprecation(ByVal company As String, ByVal costCenterId As Integer) As XPInstantFeedbackSource
        Dim service1 As New InteropCostServiceXpo(company)
        Dim _responsible As XPCollection(Of Infrastructure.Data.Xpo.InteropCostRepository.AFNRESPONXpo) = service1.ListResponsibleByCostCenterId(costCenterId)
        Dim _listMainAccountsId As List(Of Integer) = (From r In _responsible Select r).Select(Function(x) x.CTNCUENTA).Distinct().ToList()
        Dim service2 As New InteropCostServiceXpo(company)
        Return service2.ListMainAccountByOIDAccountList(_listMainAccountsId)
    End Function

    ''' <summary>
    ''' Lists the main account supply.
    ''' </summary>
    Public Shared Function ListMainAccountSupply(ByVal company As String, ByVal listCuentaServiceArea As List(Of Integer)) As XPInstantFeedbackSource
        Dim service2 As New InteropCostServiceXpo(company)
        Return service2.ListMainAccountByOIDAccountList(listCuentaServiceArea.Distinct().ToList())
    End Function

    ''' <summary>
    ''' Lists the main account erp.
    ''' </summary>
    Public Shared Function ListDistributionSecondaryByYearMonth(ByVal company As String, year As Integer, month As Integer) As XPInstantFeedbackSource
        Dim service As New InteropCostServiceXpo(company)
        Return service.ListDistributionSecondaryByYearMonth(year, month)
    End Function

    ''' <summary>
    ''' Lists the main account erp.
    ''' </summary>
    Public Shared Function ListMainAccountErp(ByVal company As String) As XPInstantFeedbackSource
        Dim service As New InteropCostServiceXpo(company)
        Return service.ListMainAccountErp()
    End Function

    ''' <summary>
    ''' Lists the main account erp.
    ''' </summary>
    Public Shared Function ListDistributionSecondary(ByVal company As String) As XPInstantFeedbackSource
        Dim service As New InteropCostServiceXpo(company)
        Return service.ListDistributionSecondary()
    End Function

    ''' <summary>
    ''' Lista los activos fijos del erp con que se hace interfaz
    ''' </summary>
    Public Shared Function ListFixedAssetInterfaceErp(ByVal company As String) As XPInstantFeedbackSource
        Dim service As New InteropCostServiceXpo(company)
        Return service.ListFixedAssetInterfaceErp()
    End Function

    ''' <summary>
    ''' Lista la distribucion de activos fijos
    ''' </summary>
    ''' <param name="company">The company.</param>
    ''' <returns></returns>
    Public Shared Function ListDistributionFixedAsset(ByVal company As String) As XPInstantFeedbackSource
        Dim service As New InteropCostServiceXpo(company)
        Return service.ListDistributionFixedAsset()
    End Function

    ''' <summary>
    ''' Lists the distribution fixed asset by year month.
    ''' </summary>
    Public Shared Function ListDistributionFixedAssetByYearMonth(ByVal company As String, ByVal year As Integer, ByVal month As Integer) As XPInstantFeedbackSource
        Dim service As New InteropCostServiceXpo(company)
        Return service.ListDistributionFixedAssetByYearMonth(year, month)
    End Function

    ''' <summary>
    ''' Lists the distribution intermediate by year month.
    ''' </summary>
    Public Shared Function ListDistributionIntermediateByYearMonth(ByVal company As String, ByVal year As Integer, ByVal month As Integer) As XPInstantFeedbackSource
        Dim service As New InteropCostServiceXpo(company)
        Return service.ListDistributionIntermediateByYearMonth(year, month)
    End Function

    ''' <summary>
    ''' Lista los gastos generales
    ''' </summary>
    ''' <param name="company">The company.</param>
    ''' <returns></returns>
    Public Shared Function ListGeneralExpenses(ByVal company As String) As XPInstantFeedbackSource
        Dim service As New InteropCostServiceXpo(company)
        Return service.ListGeneralExpenses()
    End Function

    ''' <summary>
    ''' Lista todos los registros de distribucion de gastos directos
    ''' </summary>
    Public Shared Function ListDistributionDirectCost(ByVal company As String) As XPInstantFeedbackSource
        Dim service As New InteropCostServiceXpo(company)
        Return service.ListDistributionDirectCost()
    End Function

    ''' <summary>
    ''' Lista los gastos generales de distribucion directa
    ''' </summary>
    Public Shared Function ListGeneralExpensesWithDirectDistribution(ByVal company As String) As XPInstantFeedbackSource
        Dim service As New InteropCostServiceXpo(company)
        Return service.ListGeneralExpensesWithDirectDistribution()
    End Function

    ''' <summary>
    ''' Lista los gastos generales de distribucion directa por año y mes
    ''' </summary>
    Public Shared Function ListGeneralExpensesWithDirectDistributionByYearMonth(ByVal company As String, ByVal year As Integer, ByVal month As Integer) As XPInstantFeedbackSource
        Dim service As New InteropCostServiceXpo(company)
        Return service.ListGeneralExpensesWithDirectDistributionByYearMonth(year, month)
    End Function

    ''' <summary>
    ''' Lista las cuentas contables del erp con que se interfaza por clase
    ''' </summary>
    Public Shared Function ListMainAccountErpByClass(ByVal company As String, ByVal MAclass As List(Of Integer)) As XPInstantFeedbackSource
        Dim service As New InteropCostServiceXpo(company)
        Return service.ListMainAccountErpByClasses(MAclass)
    End Function

    ''' <summary>
    ''' Lista los numeros de cuenta
    ''' </summary>
    Public Shared Function ListMainAccountNumber(ByVal company As String) As List(Of String)
        'Dim service As New PayrollServicesXpo(company)
        'Dim _listConceptAccountingStructure As XPCollection(Of Infrastructure.Data.Xpo.PayrollRepository.ConceptAccountingStructureXpo) = service.ListConceptAccountingStructure()
        'Dim mainAccountAccrued As List(Of String) = _listConceptAccountingStructure.ToList().Where(Function(x) x.AccruedAccount IsNot Nothing).Select(Function(y) y.AccruedAccount).Distinct().ToList()
        'Dim mainAccountDeducted As List(Of String) = _listConceptAccountingStructure.ToList().Where(Function(x) x.DeductedAccount IsNot Nothing).Select(Function(y) y.DeductedAccount).Distinct().ToList()
        'Return mainAccountAccrued.Union(mainAccountDeducted).ToList().Distinct().ToList()
    End Function

    ''' <summary>
    ''' Lista los centros de produccion
    ''' </summary>
    Public Shared Function ListCostCenterDinamic(ByVal company As String) As XPInstantFeedbackSource
        Dim service As New InteropCostServiceXpo(company)
        Return service.ListCostCenterDinamic()
    End Function
    ''' <summary>
    ''' Lista los centros de produccion
    ''' </summary>
    Public Shared Function ListServiceArea(ByVal company As String) As XPInstantFeedbackSource
        Dim service As New InteropCostServiceXpo(company)
        Return service.ListServiceArea()
    End Function
    Public Shared Function ListServiceAreaByCostCenter(company As String, oidCostCenter As Integer) As XPInstantFeedbackSource
        Dim service As New InteropCostServiceXpo(company)
        Return service.ListServiceAreaByCostCenter(oidCostCenter)
    End Function
    ''' <summary>
    ''' Lista los centros de produccion
    ''' </summary>
    Public Shared Function ListProductionCenter(ByVal company As String) As XPInstantFeedbackSource
        Dim service As New InteropCostServiceXpo(company)
        Return service.ListProductionCenter()
    End Function

    ''' <summary>
    ''' Lista los centros de produccion
    ''' </summary>
    Public Shared Function ListProductionCenterByStatus(ByVal company As String, ByVal status As Boolean) As XPInstantFeedbackSource
        Dim service As New InteropCostServiceXpo(company)
        Return service.ListProductionCenterByStatus(status)
    End Function

    ''' <summary>
    ''' Lista la estructura organizacional
    ''' </summary>
    Public Shared Function ListOrganizationalStructureData(ByVal company As String) As XPCollection
        Dim service As New InteropCostServiceXpo(company)
        Return service.ListOrganizationalStructureData()
    End Function

    ''' <summary>
    ''' Lista la estructura organizacional
    ''' </summary>
    Public Shared Function InitializeOrganizationalStructureWithOut(ByVal company As String, ByVal code As String) As XPInstantFeedbackSource
        Dim service As New InteropCostServiceXpo(company)
        Return service.InitializeOrganizationalStructureWithOut(code)
    End Function

    ''' <summary>
    ''' Lista la estructura organizacional
    ''' </summary>
    Public Shared Function ListOrganizationalStructure(ByVal company As String) As XPInstantFeedbackSource
        Dim service As New InteropCostServiceXpo(company)
        Return service.ListOrganizationalStructure()
    End Function

    ''' <summary>
    ''' Lista la distribución por mano de obra
    ''' </summary>
    Public Shared Function ListDistributionManpower(ByVal company As String) As XPInstantFeedbackSource
        Dim service As New InteropCostServiceXpo(company)
        Return service.ListDistributionManpower()
    End Function

#End Region

#Region "Contract"

    ''' <summary>
    ''' lista todos los grupos de atencion
    ''' </summary>
    ''' <param name="company"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function ListCareGroup(ByVal company As String) As XPInstantFeedbackSource
        Dim service As New ContractServiceXpo(company)
        Return service.ListCareGroup()
    End Function

    ''' <summary>
    ''' lista todas las definiciones de tarifa
    ''' </summary>
    ''' <param name="company"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function ListDefinitionRate(ByVal company As String) As XPInstantFeedbackSource
        Dim service As New ContractServiceXpo(company)
        Return service.ListDefinitionRate()
    End Function

    ''' <summary>
    ''' lista las definiciones de tarifa por estado
    ''' </summary>
    ''' <param name="company">Empresa a la que se encuentra conectado</param>
    Public Shared Function ListDefinitionRateByStatus(ByVal status As Boolean, ByVal company As String) As XPInstantFeedbackSource
        Dim service As New ContractServiceXpo(company)
        Return service.ListDefinitionRateByStatus(status)
    End Function

    ''' <summary>
    ''' lista las definiciones de tarifa por estado
    ''' </summary>
    ''' <param name="company">Empresa a la que se encuentra conectado</param>
    Public Shared Function ListAllUserXpCollection(ByVal company As String) As XPCollection
        Dim service As New SecurityServicesXpo(company)
        Return service.ListAllUserXpCollection()
    End Function

    ''' <summary>
    ''' lista las definiciones de tarifa por estado
    ''' </summary>
    ''' <param name="company">Empresa a la que se encuentra conectado</param>
    Public Shared Function ListDefinitionRateXpCollection(ByVal company As String) As XPCollection
        Dim service As New ContractServiceXpo(company)
        Return service.ListDefinitionRateXpCollection()
    End Function

    ''' <summary>
    ''' Lista los grupos atencion por estado
    ''' </summary>
    ''' <param name="company">Empresa a la que se encuentra conectado</param>
    Public Shared Function ListCareGroupByStatus(ByVal status As Boolean, ByVal company As String) As XPInstantFeedbackSource
        Dim service As New ContractServiceXpo(company)
        Return service.ListCareGroupByStatus(status)
    End Function

    ''' <summary>
    ''' Lista los grupos atencion por estado con xpcollection
    ''' </summary>
    ''' <param name="company">Empresa a la que se encuentra conectado</param>
    Public Shared Function ListCareGroupByStatusXpCollection(ByVal status As Boolean, ByVal company As String) As XPCollection
        Dim service As New ContractServiceXpo(company)
        Return service.ListCareGroupByStatusXpCollection(status)
    End Function

    ''' <summary>
    ''' Lista los detalles de la definición de tarifa
    ''' </summary>
    ''' <param name="company">Empresa a la que se encuentra conectado</param>
    Public Shared Function ListDefinitionRateDetailByDefinitionRateId(definitionRateId As Integer, ByVal company As String) As XPCollection
        Dim service As New ContractServiceXpo(company)
        Return service.ListDefinitionRateDetailByDefinitionRateId(definitionRateId)
    End Function

    ''' <summary>
    ''' lista los grupos de atencion por tipo de liquidacion
    ''' </summary>
    ''' <param name="LiquidationType"></param>
    ''' <param name="company"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function ListCareGroupByLiquidationType(LiquidationType As Integer, company As String) As XPInstantFeedbackSource
        Dim service As New ContractServiceXpo(company)
        Return service.ListCareGroupByLiquidationType(LiquidationType)
    End Function
    ''' <summary>
    ''' lista todos los grupos de servicios IPS 
    ''' </summary>
    ''' <param name="company"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function ListBillingConcept(ByVal company As String) As XPInstantFeedbackSource
        Dim service As New ContractServiceXpo(company)
        Return service.ListBillingConcept()
    End Function

    ''' <summary>
    ''' Lista los grupos de servicios ips por estado
    ''' </summary>
    ''' <param name="company">Empresa a la que se encuentra conectado</param>
    Public Shared Function ListIPSServicesGroupByStatus(ByVal status As Boolean, ByVal company As String) As XPInstantFeedbackSource
        Dim service As New ContractServiceXpo(company)
        Return service.ListIPSServicesGroupByStatus(status)
    End Function
    Public Shared Function ListBillingConceptByTypeAndStatus(type As Integer, status As Boolean, company As String) As XPInstantFeedbackSource
        Dim service As New ContractServiceXpo(company)
        Return service.ListBillingConceptByTypeAndStatus(type, status)
    End Function
    ''' <summary>
    ''' lista todos los servicios IPS 
    ''' </summary>
    ''' <param name="company"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function ListIPSServices(ByVal company As String) As XPInstantFeedbackSource
        Dim service As New ContractServiceXpo(company)
        Return service.ListIPSServices()
    End Function

    ''' <summary>
    ''' Lista los servicios ips por estado
    ''' </summary>
    ''' <param name="company">Empresa a la que se encuentra conectado</param>
    ''' <returns>Lista de tipos de documento</returns>
    Public Shared Function ListIPSServicesByStatus(ByVal status As Boolean, ByVal company As String) As XPInstantFeedbackSource
        Dim service As New ContractServiceXpo(company)
        Return service.ListIPSServicesByStatus(status)
    End Function

    ''' <summary>
    ''' lista todas las plantillas de producto 
    ''' </summary>
    ''' <param name="company"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function ListProductTemplate(ByVal company As String) As XPInstantFeedbackSource
        Dim service As New ContractServiceXpo(company)
        Return service.ListProductTemplate()
    End Function

    ''' <summary>
    ''' Lista las plantillas de producto por estado
    ''' </summary>
    ''' <param name="company">Empresa a la que se encuentra conectado</param>
    ''' <returns>Lista de tipos de documento</returns>
    Public Shared Function ListProductTemplateByStatus(ByVal status As Boolean, ByVal company As String) As XPInstantFeedbackSource
        Dim service As New ContractServiceXpo(company)
        Return service.ListProductTemplateByStatus(status)
    End Function

    ''' <summary>
    ''' Lista los contratos por estdo
    ''' </summary>
    ''' <param name="company">Empresa a la que se encuentra conectado</param>
    ''' <returns>Lista de tipos de documento</returns>
    Public Shared Function ListContractByStatus(ByVal status As Integer, ByVal company As String) As XPInstantFeedbackSource
        Dim service As New ContractServiceXpo(company)
        Return service.ListContractByStatus(status)
    End Function

    ''' <summary>
    ''' lista los servicios IPS no quirurgicos
    ''' </summary>
    ''' <param name="company"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function ListIPSServicesNoSurgical(ByVal manualType As Integer, ByVal company As String) As XPInstantFeedbackSource
        Dim service As New ContractServiceXpo(company)
        Return service.ListIPSServicesNoSurgical(manualType)
    End Function

    ''' <summary>
    ''' Lista los grupos cups por estado
    ''' </summary>
    ''' <param name="company">Empresa a la que se encuentra conectado</param>
    ''' <returns>Lista de tipos de documento</returns>
    Public Shared Function ListIPSServicesByService(ByVal status As Boolean, ByVal serviceClass As Integer, ByVal company As String) As XPInstantFeedbackSource
        Dim service As New ContractServiceXpo(company)
        Return service.ListIPSServicesByService(status, serviceClass)
    End Function

    ''' <summary>
    ''' Lista los grupos cups por estado
    ''' </summary>
    ''' <param name="company">Empresa a la que se encuentra conectado</param>
    ''' <returns>Lista de tipos de documento</returns>
    Public Shared Function ListIPSServicesByServiceAndPresentation(ByVal status As Boolean, ByVal serviceClass As Integer, ByVal presentation As Integer, ByVal company As String) As XPInstantFeedbackSource
        Dim service As New ContractServiceXpo(company)
        Return service.ListIPSServicesByServiceAndPresentation(status, serviceClass, presentation)
    End Function

    ''' <summary>
    ''' Lista todos los grupos cups
    ''' </summary>
    ''' <param name="company">Empresa a la que se encuentra conectado</param>
    ''' <returns>Lista de tipos de documento</returns>
    Public Shared Function ListCupsGroup(ByVal company As String) As XPInstantFeedbackSource
        Dim service As New ContractServiceXpo(company)
        Return service.ListCupsGroup()
    End Function

    ''' <summary>
    ''' Lista todas las entidades administradoras de salud
    ''' </summary>
    ''' <param name="company">Empresa a la que se encuentra conectado</param>
    ''' <returns>Lista de tipos de documento</returns>
    Public Shared Function ListHealthAdministrator(ByVal company As String) As XPInstantFeedbackSource
        Dim service As New ContractServiceXpo(company)
        Return service.ListHealthAdministrator()
    End Function

    ''' <summary>
    ''' Lista los grupos cups por estado
    ''' </summary>
    ''' <param name="company">Empresa a la que se encuentra conectado</param>
    ''' <returns>Lista de tipos de documento</returns>
    Public Shared Function ListHealthAdministratorByStatus(ByVal status As Boolean, ByVal company As String) As XPInstantFeedbackSource
        Dim service As New ContractServiceXpo(company)
        Return service.ListHealthAdministratorByStatus(status)
    End Function

    ''' <summary>
    ''' Lista los grupos cups por estado
    ''' </summary>
    ''' <param name="company">Empresa a la que se encuentra conectado</param>
    ''' <returns>Lista de tipos de documento</returns>
    Public Shared Function ListHealthAdministratorByType(ByVal type As Byte, ByVal company As String) As XPInstantFeedbackSource
        Dim service As New ContractServiceXpo(company)
        Return service.ListHealthAdministratorByType(type)
    End Function

    ''' <summary>
    ''' Lista las entidades administradoras de salud exepto por un tipo
    ''' </summary>
    ''' <param name="type">The type.</param>
    ''' <param name="company">The company.</param>
    ''' <returns></returns>
    Public Shared Function ListHealthAdministratorByNotType(type As Integer, company As String) As XPInstantFeedbackSource
        Dim service As New ContractServiceXpo(company)
        Return service.ListHealthAdministratorByNotType(type)
    End Function

    ''' <summary>
    ''' Lista todos los subgrupos cups
    ''' </summary>
    ''' <param name="company">Empresa a la que se encuentra conectado</param>
    ''' <returns>Lista de tipos de documento</returns>
    Public Shared Function ListCupsSubGroup(ByVal company As String) As XPInstantFeedbackSource
        Dim service As New ContractServiceXpo(company)
        Return service.ListCupsSubGroup()
    End Function

    ''' <summary>
    ''' Lista todos las entidades CUPS
    ''' </summary>
    ''' <param name="company">Empresa a la que se encuentra conectado</param>
    ''' <returns>Lista de tipos de documento</returns>
    Public Shared Function ListCupsEntity(ByVal company As String) As XPInstantFeedbackSource
        Dim service As New ContractServiceXpo(company)
        Return service.ListCupsEntity()
    End Function

    ''' <summary>
    ''' Lista todos las entidades CUPS
    ''' </summary>
    ''' <param name="company">Empresa a la que se encuentra conectado</param>
    ''' <returns>Lista de tipos de documento</returns>
    Public Shared Function ListMarketingUnit(ByVal company As String) As XPInstantFeedbackSource
        Dim service As New ContractServiceXpo(company)
        Return service.ListMarketingUnit()
    End Function

    ''' <summary>
    ''' Lista los grupos cups por estado
    ''' </summary>
    ''' <param name="company">Empresa a la que se encuentra conectado</param>
    ''' <returns>Lista de tipos de documento</returns>
    Public Shared Function ListCupsGroupByStatus(ByVal status As Boolean, ByVal company As String) As XPInstantFeedbackSource
        Dim service As New ContractServiceXpo(company)
        Return service.ListCupsGroupByStatus(status)
    End Function

    ''' <summary>
    ''' Lista los grupos cups por estado
    ''' </summary>
    ''' <param name="company">Empresa a la que se encuentra conectado</param>
    ''' <returns>Lista de tipos de documento</returns>
    Public Shared Function ListCupsGroupByStatusXpCollection(ByVal status As Boolean, ByVal company As String) As XPCollection
        Dim service As New ContractServiceXpo(company)
        Return service.ListCupsGroupByStatusXpCollection(status)
    End Function

    ''' <summary>
    ''' Lista los grupos cups por estado
    ''' </summary>
    ''' <param name="company">Empresa a la que se encuentra conectado</param>
    ''' <returns>Lista de tipos de documento</returns>
    Public Shared Function ListCupsEntityByStatus(ByVal status As Boolean, ByVal company As String) As XPInstantFeedbackSource
        Dim service As New ContractServiceXpo(company)
        Return service.ListCupsEntityByStatus(status)
    End Function

    ''' <summary>
    ''' Lista las entidades cups con xpCollection
    ''' </summary>
    ''' <param name="company">Empresa a la que se encuentra conectado</param>
    ''' <returns>Lista de tipos de documento</returns>
    Public Shared Function ListCupsEntityByStatusXpCollection(ByVal status As Boolean, ByVal company As String) As XPCollection
        Dim service As New ContractServiceXpo(company)
        Return service.ListCupsEntityByStatusXpCollection(status)
    End Function

    ''' <summary>
    ''' Lista los serivicios ips con xpCollection
    ''' </summary>
    ''' <param name="company">Empresa a la que se encuentra conectado</param>
    ''' <returns>Lista de tipos de documento</returns>
    Public Shared Function ListIPSServicesByStatusXpCollection(ByVal status As Boolean, ByVal company As String) As XPCollection
        Dim service As New ContractServiceXpo(company)
        Return service.ListIPSServicesByStatusXpCollection(status)
    End Function

    ''' <summary>
    ''' Lista los detalles del cubrimiento
    ''' </summary>
    ''' <param name="company">Empresa a la que se encuentra conectado</param>
    ''' <returns>Lista de tipos de documento</returns>
    Public Shared Function ListProcedureCupsByProcedureTemplateId(procedureTemplateId As Integer, ByVal company As String) As XPCollection
        Dim service As New ContractServiceXpo(company)
        Return service.ListProcedureCupsByProcedureTemplateId(procedureTemplateId)
    End Function

    ''' <summary>
    ''' Lista los subgrupos cups por estado
    ''' </summary>
    ''' <param name="company">Empresa a la que se encuentra conectado</param>
    ''' <returns>Lista de tipos de documento</returns>
    Public Shared Function ListCupsSubGroupByStatus(ByVal status As Boolean, ByVal company As String) As XPInstantFeedbackSource
        Dim service As New ContractServiceXpo(company)
        Return service.ListCupsSubGroupByStatus(status)
    End Function

    ''' <summary>
    ''' Lista los subgrupos cups por estado
    ''' </summary>
    ''' <param name="company">Empresa a la que se encuentra conectado</param>
    ''' <returns>Lista de tipos de documento</returns>
    Public Shared Function ListCupsSubGroupByStatusXpCollection(ByVal status As Boolean, ByVal company As String) As XPCollection
        Dim service As New ContractServiceXpo(company)
        Return service.ListCupsSubGroupByStatusXpCollection(status)
    End Function

    ''' <summary>
    ''' Lista todos los contratos
    ''' </summary>
    ''' <param name="company">Empresa a la que se encuentra conectado</param>
    ''' <returns>Lista de tipos de documento</returns>
    Public Shared Function ListContract(ByVal company As String) As XPInstantFeedbackSource
        Dim service As New ContractServiceXpo(company)
        Return service.ListContract()
    End Function

    ''' <summary>
    ''' Lista todos los rangos uvr
    ''' </summary>
    ''' <param name="company">Empresa a la que se encuentra conectado</param>
    ''' <returns>Lista de tipos de documento</returns>
    Public Shared Function ListUVRRange(ByVal company As String) As XPInstantFeedbackSource
        Dim service As New ContractServiceXpo(company)
        Return service.ListUVRRange()
    End Function

    ''' <summary>
    ''' Lista todos los rangos uvr por estado
    ''' </summary>
    ''' <param name="company">Empresa a la que se encuentra conectado</param>
    ''' <returns>Lista de tipos de documento</returns>
    Public Shared Function ListUVRRangeByStatus(ByVal status As Boolean, ByVal company As String) As XPInstantFeedbackSource
        Dim service As New ContractServiceXpo(company)
        Return service.ListUVRRangeByStatus(status)
    End Function

    ''' <summary>
    ''' Lista todas las plantillas de procedimiento
    ''' </summary>
    ''' <param name="company">Empresa a la que se encuentra conectado</param>
    ''' <returns>Lista de tipos de documento</returns>
    Public Shared Function ListProcedureTemplate(ByVal company As String) As XPInstantFeedbackSource
        Dim service As New ContractServiceXpo(company)
        Return service.ListProcedureTemplate()
    End Function

    ''' <summary>
    ''' Lista las plantillas de procedimiento por estado
    ''' </summary>
    ''' <param name="company">Empresa a la que se encuentra conectado</param>
    ''' <returns>Lista de tipos de documento</returns>
    Public Shared Function ListProcedureTemplateByStatus(ByVal status As Boolean, ByVal company As String) As XPInstantFeedbackSource
        Dim service As New ContractServiceXpo(company)
        Return service.ListProcedureTemplateByStatus(status)
    End Function
    ''' <summary>
    ''' Lista todas las plantillas de requerimientos
    ''' </summary>
    ''' <param name="company">Empresa a la que se encuentra conectado</param>
    ''' <returns>Lista de tipos de documento</returns>
    Public Shared Function ListRequirementTemplate(ByVal company As String) As XPInstantFeedbackSource
        Dim service As New ContractServiceXpo(company)
        Return service.ListRequirementTemplate()
    End Function

    ''' <summary>
    ''' Lista las plantillas de requerimientos por estado
    ''' </summary>
    ''' <param name="company">Empresa a la que se encuentra conectado</param>
    ''' <returns>Lista de tipos de documento</returns>
    Public Shared Function ListRequirementTemplateByStatus(ByVal status As Boolean, ByVal company As String) As XPInstantFeedbackSource
        Dim service As New ContractServiceXpo(company)
        Return service.ListRequirementTemplateByStatus(status)
    End Function

    ''' <summary>
    ''' Lista todos los salarios minimos
    ''' </summary>
    ''' <param name="company">Empresa a la que se encuentra conectado</param>
    ''' <returns>Lista de tipos de documento</returns>
    Public Shared Function ListContractMinimumWage(ByVal company As String) As XPInstantFeedbackSource
        Dim service As New ContractServiceXpo(company)
        Return service.ListContractMinimumWage()
    End Function

    ''' <summary>
    ''' Lista los los salarios minimos por estado
    ''' </summary>
    ''' <param name="company">Empresa a la que se encuentra conectado</param>
    ''' <returns>Lista de tipos de documento</returns>
    Public Shared Function ListContractMinimumWageByStatus(ByVal status As Boolean, ByVal company As String) As XPInstantFeedbackSource
        Dim service As New ContractServiceXpo(company)
        Return service.ListContractMinimumWageByStatus(status)
    End Function

    ''' <summary>
    ''' Lista todos los manuales tarifarios
    ''' </summary>
    ''' <param name="company">Empresa a la que se encuentra conectado</param>
    ''' <returns>Lista de tipos de documento</returns>
    Public Shared Function ListRateManual(ByVal company As String) As XPInstantFeedbackSource
        Dim service As New ContractServiceXpo(company)
        Return service.ListRateManual()
    End Function

    ''' <summary>
    ''' Lista los manuales tarifarios por estado
    ''' </summary>
    ''' <param name="company">Empresa a la que se encuentra conectado</param>
    ''' <returns>Lista de tipos de documento</returns>
    Public Shared Function ListRateManualByStatus(ByVal status As Boolean, ByVal company As String) As XPInstantFeedbackSource
        Dim service As New ContractServiceXpo(company)
        Return service.ListRateManualByStatus(status)
    End Function

    ''' <summary>
    ''' Consulta el manual tarifario por id
    ''' </summary>
    ''' <param name="company">Empresa a la que se encuentra conectado</param>
    ''' <returns>Lista de tipos de documento</returns>
    Public Shared Function RateManualById(ByVal id As Integer, ByVal company As String) As XPCollection
        Dim service As New ContractServiceXpo(company)
        Return service.RateManualById(id)
    End Function

    ''' <summary>
    ''' Lista todos los grupos quirurgicos
    ''' </summary>
    ''' <param name="company">Empresa a la que se encuentra conectado</param>
    ''' <returns>Lista de tipos de documento</returns>
    Public Shared Function ListSurgicalGroup(ByVal company As String) As XPInstantFeedbackSource
        Dim service As New ContractServiceXpo(company)
        Return service.ListSurgicalGroup()
    End Function

    ''' <summary>
    ''' Lista los grupos quirurgicos por estado
    ''' </summary>
    ''' <param name="company">Empresa a la que se encuentra conectado</param>
    ''' <returns>Lista de tipos de documento</returns>
    Public Shared Function ListSurgicalGroupByStatus(ByVal status As Boolean, ByVal company As String) As XPInstantFeedbackSource
        Dim service As New ContractServiceXpo(company)
        Return service.ListSurgicalGroupByStatus(status)
    End Function

    ''' <summary>
    ''' Lista todas las entidades de contratos
    ''' </summary>
    ''' <param name="company">Empresa a la que se encuentra conectado</param>
    ''' <returns>Lista de tipos de documento</returns>
    Public Shared Function ListContractEntity(ByVal company As String) As XPInstantFeedbackSource
        Dim service As New ContractServiceXpo(company)
        Return service.ListContractEntity()
    End Function

    ''' <summary>
    ''' Lista las entidades de contrato por estado
    ''' </summary>
    ''' <param name="company">Empresa a la que se encuentra conectado</param>
    ''' <returns>Lista de tipos de documento</returns>
    Public Shared Function ListContractEntityByStatus(ByVal status As Boolean, ByVal company As String) As XPInstantFeedbackSource
        Dim service As New ContractServiceXpo(company)
        Return service.ListContractEntityByStatus(status)
    End Function

    ''' <summary>
    ''' Lista las entidades de contrato por estado
    ''' </summary>
    ''' <param name="company">Empresa a la que se encuentra conectado</param>
    ''' <returns>Lista de tipos de documento</returns>
    Public Shared Function ListContractEntityByStatusXpCollection(ByVal status As Boolean, ByVal company As String) As XPCollection
        Dim service As New ContractServiceXpo(company)
        Return service.ListContractEntityByStatusXpCollection(status)
    End Function

    ''' <summary>
    ''' Lista todos los manuales de servicio
    ''' </summary>
    ''' <param name="company">Empresa a la que se encuentra conectado</param>
    ''' <returns>Lista de tipos de documento</returns>
    Public Shared Function ListRateManualDetail(ByVal company As String) As XPInstantFeedbackSource
        Dim service As New ContractServiceXpo(company)
        Return service.ListRateManualDetail()
    End Function

    ''' <summary>
    ''' Lista los manuales de servicio por estado
    ''' </summary>
    ''' <param name="company">Empresa a la que se encuentra conectado</param>
    ''' <returns>Lista de tipos de documento</returns>
    Public Shared Function ListRateManualDetailByStatus(ByVal status As Boolean, ByVal company As String) As XPInstantFeedbackSource
        Dim service As New ContractServiceXpo(company)
        Return service.ListRateManualDetailByStatus(status)
    End Function

    ''' <summary>
    ''' Lista los manuales de servicio por id de manual tarifario
    ''' </summary>
    ''' <param name="company">Empresa a la que se encuentra conectado</param>
    ''' <returns>Lista de tipos de documento</returns>
    Public Shared Function ListRateManualDetailByRateManualId(ByVal idRateManual As Integer, ByVal company As String) As XPCollection
        Dim service As New ContractServiceXpo(company)
        Return service.ListRateManualDetailByRateManualId(idRateManual)
    End Function

    ''' <summary>
    ''' Lista la asociacion entre procedureCups y marketingUnitCups
    ''' </summary>
    ''' <param name="company">Empresa a la que se encuentra conectado</param>
    ''' <returns>Lista de tipos de documento</returns>
    Public Shared Function ListVProcedureMarketingUnitCUPS(ByVal idProcedureTemplate As Integer, ByVal company As String) As XPCollection
        Dim service As New ContractServiceXpo(company)
        Return service.ListVProcedureMarketingUnitCUPS(idProcedureTemplate)
    End Function

    ''' <summary>
    ''' Lista el detalle del grupo de atencion
    ''' </summary>
    ''' <param name="company">Empresa a la que se encuentra conectado</param>
    ''' <returns>Lista de tipos de documento</returns>
    Public Shared Function ListCareGroupRateCollection(ByVal careGroupId As Integer, ByVal company As String) As XPCollection
        Dim service As New ContractServiceXpo(company)
        Return service.ListCareGroupRateCollection(careGroupId)
    End Function

    ''' <summary>
    ''' Obtiene el grupo de atencion
    ''' </summary>
    ''' <param name="company">Empresa a la que se encuentra conectado</param>
    ''' <returns>Lista de tipos de documento</returns>
    Public Shared Function GetCareGroupById(Id As Integer, ByVal company As String) As XPCollection
        Dim service As New ContractServiceXpo(company)
        Return service.GetCareGroupById(Id)
    End Function

    ''' <summary>
    ''' lista los servicios ISS por grupo de atencion
    ''' </summary>
    Public Shared Function ListIssServicesByCareGroup(ByVal company As String, idCareGroup As Integer) As XPInstantFeedbackSource
        Dim service As New ContractServiceXpo(company)
        Return service.ListIssServicesByCareGroup(idCareGroup)
    End Function

    Public Shared Function ListIssServicesByCareGroupNotQx(company As String, caregroupId As Integer) As XPInstantFeedbackSource
        Dim service As New ContractServiceXpo(company)
        Return service.ListIssServicesByCareGroupNotQx(caregroupId)
    End Function
    Shared Function ListSoatByCareGroupNotQx(company As String, idCareGroup As Integer) As XPInstantFeedbackSource
        Dim service As New ContractServiceXpo(company)
        Return service.ListSoatByCareGroupNotQx(idCareGroup)
    End Function
    ''' <summary>
    ''' lista los servicios ISS por grupo de atencion y presentación
    ''' </summary>
    Public Shared Function ListIssServicesByCareGroupAndPresentation(company As String, idCareGroup As Integer, presentation As Integer) As XPInstantFeedbackSource
        Dim service As New ContractServiceXpo(company)
        Return service.ListIssServicesByCareGroupAndPresentation(idCareGroup, presentation)
    End Function
    ''' <summary>
    ''' lista los servicios SOAT por grupo de atencion
    ''' </summary>
    Public Shared Function ListSoatByCareGroup(ByVal company As String, idCareGroup As Integer) As XPInstantFeedbackSource
        Dim service As New ContractServiceXpo(company)
        Return service.ListSoatByCareGroup(idCareGroup)
    End Function
    ''' <summary>
    ''' lista los servicios SOAT por grupo de atencion y presentacion
    ''' </summary>
    Public Shared Function ListSoatByCareGroupAndPresentation(company As String, caregroupId As Object, presentation As Integer) As XPInstantFeedbackSource
        Dim service As New ContractServiceXpo(company)
        Return service.ListSoatByCareGroupAndPresentation(caregroupId, presentation)
    End Function
    ''' <summary>
    ''' lista los CUPS para ordenes de servicio
    ''' </summary>
    Public Shared Function ListCUPS(ByVal company As String, idCareGroup As Integer) As LinqInstantFeedbackSource
        Dim service As New ContractServiceXpo(company)
        Return service.ListCUPS(idCareGroup)
    End Function

    ''' <summary>
    ''' Lista los servicios IPS por presentacion y clase servicio
    ''' </summary>
    ''' <param name="company">Empresa a la que se encuentra conectado</param>
    ''' <returns>Lista de tipos de documento</returns>
    Public Shared Function ListIPSServicesByPresentationAndServiceClass(ByVal status As Boolean, ByVal serviceClass As Integer, ByVal presentation As Integer, ByVal options As Integer, ByVal typeManual As Integer, ByVal company As String) As XPInstantFeedbackSource
        Dim service As New ContractServiceXpo(company)
        Return service.ListIPSServicesByPresentationAndServiceClass(status, serviceClass, presentation, options, typeManual)
    End Function

    ''' <summary>
    ''' Lista los servicios IPS por presentacion y clase servicio
    ''' </summary>
    ''' <param name="company">Empresa a la que se encuentra conectado</param>
    ''' <returns>Lista de tipos de documento</returns>
    Public Shared Function ListIPSServicesByServiceManualAndServiceClass(ByVal status As Boolean, ByVal serviceClass As Integer, ByVal typeManual As Integer, ByVal company As String) As XPInstantFeedbackSource
        Dim service As New ContractServiceXpo(company)
        Return service.ListIPSServicesByServiceManualAndServiceClass(status, serviceClass, typeManual)
    End Function

    ''' <summary>
    ''' Lista los servicios IPS por presentacion y clase servicio
    ''' </summary>
    ''' <param name="company">Empresa a la que se encuentra conectado</param>
    ''' <returns>Lista de tipos de documento</returns>
    Public Shared Function ListIPSServiceByServiceClassAndServiceManualAndPresentation(ByVal status As Boolean, ByVal serviceClass As Integer, ByVal typeManual As Integer, presentation As Integer, ByVal company As String) As XPInstantFeedbackSource
        Dim service As New ContractServiceXpo(company)
        Return service.ListIPSServiceByServiceClassAndServiceManualAndPresentation(status, serviceClass, typeManual, presentation)
    End Function

#End Region

#Region "Taxes"
    ''' <summary>
    ''' lista todas las cuentas  contables por filtro
    ''' </summary>
    ''' <param name="company">The company.</param>
    ''' <returns></returns>
    Public Shared Function ListMainAccountsReportByFilter(ByVal company As String, ByVal filtro As String) As XPInstantFeedbackSource
        Dim service As New PortfolioServicesXpo(company)
        Return service.ListMainAccountsReportByFilter(filtro)
    End Function

    ''' <summary>
    ''' Lista todas las Cuentas Bancarias
    ''' </summary>
    ''' <param name="company">The company.</param>
    ' ''' <returns></returns>
    'Public Shared Function ListLowTaxesLiquidation(ByVal company As String) As XPInstantFeedbackSource
    '    Dim service As New TaxesServiceXpoEx()
    '    Return service.ListLowTaxesLiquidation()
    'End Function

#End Region

#Region "Xpcollection RadicatedD Glosas"
    ''' <summary>
    ''' Lista de radicatedd
    ''' </summary>
    ''' <param name="company">Empresa a la que se encuentra conectado</param>
    ''' <returns>Lista de tipos de documento</returns>
    Public Shared Function ListViewRadicateInvoiceDetail(ByVal radicateinvoiceCid As Integer?, ByVal company As String, Optional ByVal InvalidateOffice As Boolean = False) As XPCollection
        Dim service As New GlosasServicesXpo(company)
        Return service.ListViewRadicateInvoiceDetail(radicateinvoiceCid, InvalidateOffice)
    End Function
    ''' <summary>
    ''' Lista de radicatedd
    ''' </summary>
    ''' <param name="company">Empresa a la que se encuentra conectado</param>
    ''' <returns>Lista de tipos de documento</returns>
    Public Shared Function ListXPCollectionRadicateInvoiceD(ByVal radicateinvoiceCid As Integer?, ByVal company As String, Optional ByVal InvalidateOffice As Boolean = False) As XPCollection
        Dim service As New GlosasServicesXpo(company)
        Return service.ListXPCollectionRadicateInvoiceD(radicateinvoiceCid, InvalidateOffice)
    End Function
    ''' <summary>
    ''' Lista de radicatedD vacio para la primer instancia
    ''' </summary>
    ''' <param name="company">Empresa a la que se encuentra conectado</param>
    ''' <returns>Lista de tipos de documento</returns>
    Public Shared Function ListXPCollectionRadicateInvoiceD(ByVal company As String) As XPCollection
        Dim service As New GlosasServicesXpo(company)
        Return service.ListXPCollectionRadicateInvoiceD()
    End Function
#End Region

#Region "Listar ObjectionReceptionD con PLinqServerModeSource (Pruebas Reporte)"

    Public Shared Function ListGetD(ByVal IdReceptionC As String, ByVal Company As String) As PLinqServerModeSource
        Dim ServiceceGlosaXpo As New GlosasServicesXpo(Company)
        Return ServiceceGlosaXpo.ListGetD(IdReceptionC)
    End Function

#End Region

#Region "Mantenimiento"
    ''' <summary>
    ''' Lista las Marcas de los Equipos
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function GetTrademark(Company As String) As XPInstantFeedbackSource
        Dim service As New MaintenanceServicesXpo(Company)
        Return service.GetTrademark()
    End Function

    ''' <summary>
    ''' Lista las Marcas de los Equipos
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function GetPartsAccesoriesConsumables(Company As String) As XPInstantFeedbackSource
        Dim service As New MaintenanceServicesXpo(Company)
        Return service.GetListPartsAccesoriesConsumables()
    End Function

    ''' <summary>
    ''' Lista las Marcas de los Equipos
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function GetCostCenterMaintenance(Company As String) As XPInstantFeedbackSource
        Dim service As New MaintenanceServicesXpo(Company)
        Return service.GetCostCenter()
    End Function

    ''' <summary>
    ''' Lista los tipos de proveedores
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function ListSupplierMaintenance(ByVal Company As String) As XPInstantFeedbackSource
        Dim ObjectionD As New MaintenanceServicesXpo(Company)
        Return ObjectionD.GetSupplierMaintenance()
    End Function

    ''' <summary>
    ''' Lista todos los tipos de proveedores
    ''' </summary>
    ''' <param name="company">Empresa a la que se encuentra conectado</param>
    ''' <returns>Lista de tipos de documento</returns>
    Public Shared Function ListSupplierType(ByVal company As String) As XPInstantFeedbackSource
        Dim service As New MaintenanceServicesXpo(company)
        Return service.ListSupplierType()
    End Function

    ''' <summary>
    ''' Lista todos los Planes de Mantenimiento
    ''' </summary>
    ''' <param name="company">Empresa a la que se encuentra conectado</param>
    ''' <returns>Lista de tipos de documento</returns>
    Public Shared Function ListMaintenancePlan(ByVal company As String) As XPInstantFeedbackSource
        Dim service As New MaintenanceServicesXpo(company)
        Return service.GetListMaintenancePlan()
    End Function

    ''' <summary>
    ''' Lista los grupos cups por estado
    ''' </summary>
    ''' <param name="company">Empresa a la que se encuentra conectado</param>
    ''' <returns>Lista de tipos de documento</returns>
    Public Shared Function ListSupplierTypeByStatus(ByVal status As Boolean, ByVal company As String) As XPInstantFeedbackSource
        Dim service As New MaintenanceServicesXpo(company)
        Return service.ListSupplierTypeByStatus(status)
    End Function

    ''' <summary>
    ''' Lista los grupos cups por estado
    ''' </summary>
    ''' <param name="company">Empresa a la que se encuentra conectado</param>
    ''' <returns>Lista de tipos de documento</returns>
    Public Shared Function ListAllSupplierTypeByStatus(ByVal status As Boolean, ByVal company As String) As XPInstantFeedbackSource
        Dim service As New MaintenanceServicesXpo(company)
        Return service.ListAllSupplierTypeByStatus(status)
    End Function

    ''' <summary>
    ''' Lista los grupos cups por estado
    ''' </summary>
    ''' <param name="company">Empresa a la que se encuentra conectado</param>
    ''' <returns>Lista de tipos de documento</returns>
    Public Shared Function ListSupplierTypeByStatusTreeList(ByVal status As Boolean, ByVal company As String) As XPCollection
        Dim service As New MaintenanceServicesXpo(company)
        Return service.ListSupplierTypeByStatusTreeList(status)
    End Function

    ''' <summary>
    ''' Lista los grupos cups por estado
    ''' </summary>
    ''' <param name="company">Empresa a la que se encuentra conectado</param>
    ''' <returns>Lista de tipos de documento</returns>
    Public Shared Function ListSupplierTypeData(ByVal company As String) As XPCollection
        Dim service As New MaintenanceServicesXpo(company)
        Return service.ListSupplierTypeData()
    End Function

    ''' <summary>
    ''' Lista los grupos cups por estado
    ''' </summary>
    ''' <param name="company">Empresa a la que se encuentra conectado</param>
    ''' <returns>Lista de tipos de documento</returns>
    Public Shared Function ListSupplierTypeByStatusTreeList2(ByVal supplierId As Integer, ByVal company As String) As XPCollection
        Dim service As New MaintenanceServicesXpo(company)
        Return service.ListSupplierTypeByStatusTreeList2(supplierId)
    End Function

#End Region

#Region "Facturación"
    ''' <summary>
    ''' lista todas las ordenes de servicio
    ''' </summary>
    ''' <param name="company"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function ListAllServiceOrder(company As String) As XPInstantFeedbackSource
        Dim service As New BillingServiceXpo(company)
        Return service.ListAllServiceOrder()
    End Function

    Public Shared Function ListHealthAdministratoyByCode(company As String, code As String) As XPInstantFeedbackSource
        Dim service As New ContractServiceXpo(company)
        Return service.ListHealthAdministratoyByCode(code)
    End Function

    ''' <summary>
    ''' Lists the invoice categories.
    ''' </summary>
    ''' <param name="company">The company.</param>
    ''' <returns></returns>
    Public Shared Function ListInvoiceCategories(company As String) As XPInstantFeedbackSource
        Dim service As New BillingServiceXpo(company)
        Return service.ListInvoiceCategories()
    End Function

    ''' <summary>
    ''' Lists the invoice.
    ''' </summary>
    ''' <param name="company">The company.</param>
    ''' <returns></returns>
    Public Shared Function ListInvoice(company As String) As XPInstantFeedbackSource
        Dim service As New BillingServiceXpo(company)
        Return service.ListInvoice()
    End Function

    ''' <summary>
    ''' Lists the invoice.
    ''' </summary>
    ''' <param name="company">The company.</param>
    ''' <returns></returns>
    Public Shared Function ListViewListInvoiceAndPatient(company As String) As XPInstantFeedbackSource
        Dim service As New BillingServiceXpo(company)
        Return service.ListViewListInvoiceAndPatient()
    End Function

    ''' <summary>
    ''' Gets the categories by status and user.
    ''' </summary>
    ''' <param name="company">The company.</param>
    ''' <param name="status">if set to <c>true</c> [status].</param>
    ''' <param name="UserId">The user identifier.</param>
    ''' <returns></returns>
    Public Shared Function GetCategoriesByStatusAndUser(company As String, status As Boolean, UserId As Integer) As XPInstantFeedbackSource
        Dim service As New BillingServiceXpo(company)
        Return service.GetCategoriesByStatusAndUser(status, UserId)
    End Function

    ''' <summary>
    ''' Lists the service order detail by admission number and not list service order detail identifier.
    ''' </summary>
    ''' <param name="company">The company.</param>
    ''' <param name="admissionNumber">The admission number.</param>
    ''' <param name="listId">The list identifier.</param>
    ''' <returns></returns>
    Public Shared Function ListServiceOrderDetailByAdmissionNumberAndNotListServiceOrderDetailId(company As String, admissionNumber As String, listId As List(Of Integer)) As XPInstantFeedbackSource
        Dim service As New BillingServiceXpo(company)
        Return service.ListServiceOrderDetailByAdmissionNumberAndNotListServiceOrderDetailId(admissionNumber, listId)
    End Function

    ''' <summary>
    ''' Lists the invoice entity capitated.
    ''' </summary>
    ''' <param name="company">The company.</param>
    ''' <returns></returns>
    Public Shared Function ListInvoiceEntityCapitated(company As String) As XPInstantFeedbackSource
        Dim service As New BillingServiceXpo(company)
        Return service.ListInvoiceEntityCapitated()
    End Function

    ''' <summary>
    ''' Lista los motivos de anulacion
    ''' </summary>
    ''' <param name="company">The company.</param>
    ''' <returns></returns>
    Public Shared Function ListAnnulmentReason(company As String) As XPInstantFeedbackSource
        Dim service As New BillingServiceXpo(company)
        Return service.ListAnnulmentReason()
    End Function

    ''' <summary>
    ''' Lista todas las autorizaciones de facturación por código de usuario
    ''' </summary>
    ''' <returns></returns>
    Public Shared Function ListAllBillingAuthorizationByUserCode(company As String, usercode As String) As XPInstantFeedbackSource
        Dim service As New BillingServiceXpo(company)
        Return service.ListAllBillingAuthorizationByUserCode(usercode)
    End Function

    ''' <summary>
    ''' Lists the type of the service order detail by admission number record.
    ''' </summary>
    Public Shared Function ListServiceOrderDetailByAdmissionNumberRecordType(company As String, admissionNumber As String, recordType As Integer) As XPInstantFeedbackSource
        Dim service As New BillingServiceXpo(company)
        Return service.ListServiceOrderDetailByAdmissionNumberRecordType(admissionNumber, recordType)
    End Function

    ''' <summary>
    ''' lista todos las autorizaciones de facturación
    ''' </summary>
    ''' <param name="company"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function ListBillingAuthotization(ByVal company As String) As XPInstantFeedbackSource
        Dim service As New BillingServiceXpo(company)
        Return service.ListBillingAuthotization()
    End Function

    ''' <summary>
    ''' lista todos los grupos de facturacion
    ''' </summary>
    ''' <param name="company"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function ListBillingGroup(ByVal company As String) As XPInstantFeedbackSource
        Dim service As New BillingServiceXpo(company)
        Return service.ListBillingGroup()
    End Function

    ''' <summary>
    ''' lista todas las boletas de salida
    ''' </summary>
    ''' <param name="company"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function ListSlipOut(ByVal company As String) As XPInstantFeedbackSource
        Dim service As New BillingServiceXpo(company)
        Return service.ListSlipOut()
    End Function

    ''' <summary>
    ''' Lista los grupos de facturacion por estado
    ''' </summary>
    ''' <param name="company">Empresa a la que se encuentra conectado</param>
    Public Shared Function ListBillingGroupByStatus(ByVal status As Boolean, ByVal company As String) As XPInstantFeedbackSource
        Dim service As New BillingServiceXpo(company)
        Return service.ListBillingGroupByStatus(status)
    End Function

    ''' <summary>
    ''' Lista las facturas por estado
    ''' </summary>
    ''' <param name="company">Empresa a la que se encuentra conectado</param>
    Public Shared Function ListInvoiceByAdmissionStatus(ByVal status As Integer, ByVal admissionNumber As String, ByVal company As String) As XPInstantFeedbackSource
        Dim service As New BillingServiceXpo(company)
        Return service.ListInvoiceByAdmissionStatus(status, admissionNumber)
    End Function

    Public Shared Function ListInvoiceByStatus(ByVal company As String, ByVal status As Byte) As XPInstantFeedbackSource
        Dim service As New BillingServiceXpo(company)
        Return service.ListInvoiceByStatus(status)
    End Function

    Public Shared Function ListInvoiceByStatusAndPatientCode(company As String, status As Integer, PatientCode As String) As XPInstantFeedbackSource
        Dim service As New BillingServiceXpo(company)
        Return service.ListInvoiceByStatusAndPatientCode(status, PatientCode)
    End Function

    ''' <summary>
    ''' Lista las facturas por id
    ''' </summary>
    ''' <param name="company">Empresa a la que se encuentra conectado</param>
    ''' <returns>Lista de tipos de documento</returns>
    Public Shared Function ListViewNoSurgicalByInvoiceId(ByVal InvoiceId As Integer, ByVal company As String) As XPCollection
        Dim service As New BillingServiceXpo(company)
        Return service.ListViewNoSurgicalByInvoiceId(InvoiceId)
    End Function

    ''' <summary>
    ''' lista el detalle para el reporte de facturas
    ''' </summary>
    ''' <param name="company">Empresa a la que se encuentra conectado</param>
    ''' <returns>Lista de tipos de documento</returns>
    Public Shared Function ListViewInvoiceDetailByInvoiceId(ByVal InvoiceId As Integer, ByVal company As String) As XPCollection(Of BillingVReportInvoiceDetail)
        Dim service As New BillingServiceXpo(company)
        Return service.ListViewInvoiceDetailByInvoiceId(InvoiceId)
    End Function

    ''' <summary>
    ''' lista reporte de facturas por filtros---------------------------
    ''' </summary>
    ''' <param name="company">Empresa a la que se encuentra conectado</param>
    ''' <returns>Lista de tipos de documento</returns>
    Public Shared Function ListViewInvoiceFilters(ByVal Filter As String, ByVal company As String) As XPCollection(Of InvoicesXpo)
        Dim service As New BillingServiceXpo(company)
        Return service.ListViewInvoiceFilters(Filter)
    End Function

    ''' <summary>
    ''' lista el header para el reporte de facturas
    ''' </summary>
    ''' <param name="company">Empresa a la que se encuentra conectado</param>
    ''' <returns>Lista de tipos de documento</returns>
    Public Shared Function ListViewInvoiceByInvoiceId(ByVal InvoiceId As Integer, ByVal company As String) As XPCollection
        Dim service As New BillingServiceXpo(company)
        Return service.ListViewInvoiceByInvoiceId(InvoiceId)
    End Function

    ''' <summary>
    ''' Lista las facturas por id
    ''' </summary>
    ''' <param name="company">Empresa a la que se encuentra conectado</param>
    ''' <returns>Lista de tipos de documento</returns>
    Public Shared Function ListServiceOrderDetailSurgicalByServiceOrderDetailId(ByVal company As String, serviceOrderDetailId As Integer) As XPInstantFeedbackSource
        Dim service As New BillingServiceXpo(company)
        Return service.ListServiceOrderDetailSurgicalByServiceOrderDetailId(serviceOrderDetailId)
    End Function

    Public Shared Function ListServiceOrderDetailBySurgeryNumberAndServiceOrderId(company As String, surgeryNumber As Byte, serviceOrderId As Integer) As XPCollection(Of Infrastructure.Data.Xpo.BillingRepository.ServiceOrderDetailXpo)
        Dim service As New BillingServiceXpo(company)
        Return service.ListServiceOrderDetailBySurgeryNumberAndServiceOrderId(surgeryNumber, serviceOrderId)
    End Function
    ''' <summary>
    ''' lista los detalles de la orden
    ''' </summary>
    ''' <param name="company"></param>
    ''' <param name="serviceOrderId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function ListServiceOrderDetailByServiceOrderId(company As String, serviceOrderId As Integer) As XPCollection(Of Infrastructure.Data.Xpo.BillingRepository.ServiceOrderDetailXpo)
        Dim service As New BillingServiceXpo(company)
        Return service.ListServiceOrderDetailByServiceOrderId(serviceOrderId)
    End Function



    ''' <summary>
    ''' Lista las facturas por id
    ''' </summary>
    ''' <param name="company">Empresa a la que se encuentra conectado</param>
    ''' <returns>Lista de tipos de documento</returns>
    Public Shared Function ListViewSurgicalAndPackageByInvoiceId(ByVal InvoiceId As Integer, ByVal company As String) As XPCollection
        Dim service As New BillingServiceXpo(company)
        Return service.ListViewSurgicalAndPackageByInvoiceId(InvoiceId)
    End Function

    ''' <summary>
    ''' Lista los detalles de un folio que aún no se ha facturado
    ''' </summary>
    ''' <param name="id">Id del RevenueControlDetail</param>
    Public Shared Function ListRevenueControl(ByVal id As Integer, ByVal company As String) As PLinqServerModeSource
        Dim service As New BillingServiceXpo(company)
        Return service.ListRevenueControl(id)
    End Function

    Public Shared Function ListRevenueControlCollection(ByVal id As Integer, ByVal company As String) As XPCollection
        Dim service As New BillingServiceXpo(company)
        Return service.ListRevenueControlCollection(id)
    End Function

    ''' <summary>
    ''' Lista los detalles de un folio que fue anulado
    ''' </summary>
    ''' <returns></returns>
    Public Shared Function ListRevenueControlForAnnullateInvoice(ByVal company As String, invoiceId As Integer) As PLinqServerModeSource
        Dim service As New BillingServiceXpo(company)
        Return service.ListRevenueControlForAnnullateInvoice(invoiceId)
    End Function

    ''' <summary>
    ''' Función para obtener una lista de datos de una entidad XPO
    ''' </summary>
    ''' <typeparam name="T">Objeto XPO</typeparam>
    ''' <param name="Company">Compañia Actual</param>
    ''' <param name="Fun">Objeto tipo Función opcional para filtrar los datos necesarios</param>
    ''' <returns>Objeto</returns>
    Public Shared Function ListCollectionBillingReport(Of T)(ByVal Company As String, Optional Fun As Func(Of T, Boolean) = Nothing, Optional criteria As String = Nothing) As Object
        Dim Billing As New BillingServiceXpo(Company)
        Return Billing.GetCollection(Of T)(Fun, criteria)
    End Function

#End Region

#Region "Indigo Crystal HIS"

    Public Shared Function GetAdmissionObjectByNumIngres(company As String, admissionnumber As String) As XPCollection(Of ViewAdmissionsToLiquidation)
        Dim service As New CrystalServiceXpo(company)
        Return service.GetAdmissionObjectByNumIngres(admissionnumber)
    End Function

    ''' <summary>
    ''' Lists the medicine supplier aggregates by patient code ingreso.
    ''' </summary>
    ''' <param name="company">The company.</param>
    ''' <param name="patientCode">The patient code.</param>
    ''' <param name="admissionNumber">The admission number.</param>
    ''' <returns></returns>
    Public Shared Function ListMedicineSupplierAggregatesByPatientCodeIngreso(company As String, patientCode As String, admissionNumber As String) As XPCollection(Of ViewMedicinesSupplies)
        Dim service As New CrystalServiceXpo(company)
        Dim listMedicineSupplier As XPCollection(Of ViewMedicinesSupplies) = service.ListMedicineSupplierByPatientCodeIngreso(patientCode, admissionNumber)
        Dim listKardex As XPCollection(Of ViewKardexMedicineSupplier) = service.ListKardexMedicineSupplierByPatientCodeIngreso(patientCode, admissionNumber)

        'If listMedicineSupplier IsNot Nothing AndAlso listMedicineSupplier.Count > 0 Then
        '    For Each medicine In listMedicineSupplier
        '        'Dim kardex As XPCollection(Of ViewKardexMedicineSupplier) = service.ListKardexMedicineSupplierByCodePatientCodeIngreso(medicine.Codigo, patientCode, admissionNumber)
        '        Dim kardexList = DirectCast((From e In listKardex Where e.Codigo = medicine.Codigo Select e), XPCollection(Of ViewKardexMedicineSupplier))
        '        medicine.KardexMedicineSupplier = kardexList
        '        medicine.Entregada = kardexList.Where(Function(o) o.Origen > 0 AndAlso o.Origen < 4).Sum(Function(o) o.Cantidad)
        '        medicine.Aplicada = kardexList.Where(Function(o) o.Origen > 10 AndAlso o.Origen < 16).Sum(Function(o) o.Cantidad)
        '        medicine.Prestamo = kardexList.Where(Function(o) o.Origen = 4).Sum(Function(o) o.Cantidad)
        '        medicine.Devolutivo = kardexList.Where(Function(o) o.Origen = 16).Sum(Function(o) o.Cantidad)
        '        medicine.Alerta = IIf(medicine.Prestamo - medicine.Devolutivo = 0, 0, 1)
        '        medicine.Fisico = medicine.Entregada - medicine.Aplicada
        '    Next
        'End If

        Dim listxxx = listKardex.ToList()
        Dim list2 = listMedicineSupplier.ToList()
        If list2.Count > 0 AndAlso listxxx.Count > 0 Then
            For Each medicine In listMedicineSupplier
                'Dim kardex As XPCollection(Of ViewKardexMedicineSupplier) = service.ListKardexMedicineSupplierByCodePatientCodeIngreso(medicine.Codigo, patientCode, admissionNumber)
                Dim kardexList As List(Of ViewKardexMedicineSupplier) = (From e In listxxx Where e.Codigo = medicine.Codigo Select e).ToList()
                medicine.KardexMedicineSupplier = kardexList
                medicine.Entregada = kardexList.Where(Function(o) o.Origen > 0 AndAlso o.Origen < 4).Sum(Function(o) o.Cantidad)
                medicine.Aplicada = kardexList.Where(Function(o) o.Origen > 10 AndAlso o.Origen < 16).Sum(Function(o) o.Cantidad)
                'medicine.Prestamo = kardexList.Where(Function(o) o.Origen = 4).Sum(Function(o) o.Cantidad)
                'medicine.Devolutivo = kardexList.Where(Function(o) o.Origen = 16).Sum(Function(o) o.Cantidad)
                'medicine.Alerta = IIf(medicine.Prestamo - medicine.Devolutivo = 0, 0, 1)
                Dim devolutivos As Integer = kardexList.Where(Function(o) o.Origen = 18).Sum(Function(o) o.Cantidad)
                medicine.Fisico = medicine.Entregada - medicine.Aplicada - kardexList.Where(Function(o) o.Origen = 18).Sum(Function(o) o.Cantidad)
            Next
        End If
        Return listMedicineSupplier
    End Function

    ''' <summary>
    ''' Lists the laboratories.
    ''' </summary>
    ''' <param name="company">The company.</param>
    ''' <param name="patientCode">The patient code.</param>
    ''' <param name="admissionNumber">The admission number.</param>
    ''' <returns></returns>
    Public Shared Function ListLaboratoriesByPatientCodeIngreso(company As String, patientCode As String, admissionNumber As String) As XPCollection(Of ViewLaboratories)
        Dim service As New CrystalServiceXpo(company)
        Return service.ListLaboratories(patientCode, admissionNumber)
    End Function

    ''' <summary>
    ''' Lists the pathologies by patient code ingreso.
    ''' </summary>
    ''' <param name="company">The company.</param>
    ''' <param name="patientCode">The patient code.</param>
    ''' <param name="admissionNumber">The admission number.</param>
    ''' <returns></returns>
    Public Shared Function ListPathologiesByPatientCodeIngreso(company As String, patientCode As String, admissionNumber As String) As XPCollection(Of ViewPathologies)
        Dim service As New CrystalServiceXpo(company)
        Return service.ListPathologiesByPatientCodeIngreso(patientCode, admissionNumber)
    End Function

    Public Shared Function ListReviewsByPatientCodeIngreso(company As String, patientCode As String, admissionNumber As String) As XPCollection(Of ViewReviews)
        Dim service As New CrystalServiceXpo(company)
        Return service.ListReviewsByadmissionCodeIngreso(patientCode, admissionNumber)
    End Function

    ''' <summary>
    ''' Lists the images qx by patient code ingreso.
    ''' </summary>
    ''' <param name="company">The company.</param>
    ''' <param name="patientCode">The patient code.</param>
    ''' <param name="admissionNumber">The admission number.</param>
    ''' <returns></returns>
    Public Shared Function ListImagesDxByPatientCodeIngreso(company As String, patientCode As String, admissionNumber As String) As XPCollection(Of ViewImagesDX)
        Dim service As New CrystalServiceXpo(company)
        Return service.ListImagesDxByPatientCodeIngreso(patientCode, admissionNumber)
    End Function

    ''' <summary>
    ''' Lists the procedures no qx by patient code ingreso.
    ''' </summary>
    ''' <returns></returns>
    Public Shared Function ListProceduresNoQxByPatientCodeIngreso(company As String, patientCode As String, admissionNumber As String) As XPCollection(Of ViewProceduresNoQx)
        Dim service As New CrystalServiceXpo(company)
        Return service.ListProceduresNoQxByPatientCodeIngreso(patientCode, admissionNumber)
    End Function

    ''' <summary>
    ''' Lists the procedures qx by patient code ingreso.
    ''' </summary>
    ''' <param name="company">The company.</param>
    ''' <param name="patientCode">The patient code.</param>
    ''' <param name="admissionNumber">The admission number.</param>
    ''' <returns></returns>
    Public Shared Function ListProceduresQxByPatientCodeIngreso(company As String, patientCode As String, admissionNumber As String) As XPCollection(Of ViewProceduresQx)
        Dim service As New CrystalServiceXpo(company)
        Return service.ListProceduresQxByPatientCodeIngreso(patientCode, admissionNumber)
    End Function

    ''' <summary>
    ''' Lists the services procedures byadmission code ingreso.
    ''' </summary>
    ''' <param name="company">The company.</param>
    ''' <param name="patientCode">The patient code.</param>
    ''' <param name="admissionNumber">The admission number.</param>
    ''' <returns></returns>
    Public Shared Function ListServicesProceduresLaboratoriesByadmissionCodeIngreso(company As String, patientCode As String, admissionNumber As String) As XPCollection(Of ViewServicesProceduresLaboratories)
        Dim service As New CrystalServiceXpo(company)
        Return service.ListServicesProceduresLaboratoriesByadmissionCodeIngreso(patientCode, admissionNumber)
    End Function

    ''' <summary>
    ''' Lists the services procedures images dx byadmission code ingreso.
    ''' </summary>
    ''' <param name="company">The company.</param>
    ''' <param name="patientCode">The patient code.</param>
    ''' <param name="admissionNumber">The admission number.</param>
    ''' <returns></returns>
    Public Shared Function ListServicesProceduresImagesDxByadmissionCodeIngreso(company As String, patientCode As String, admissionNumber As String) As XPCollection(Of ViewServicesProceduresImagesDx)
        Dim service As New CrystalServiceXpo(company)
        Return service.ListServicesProceduresImagesDxByadmissionCodeIngreso(patientCode, admissionNumber)
    End Function

    ''' <summary>
    ''' Lists the services procedures pathologies byadmission code ingreso.
    ''' </summary>
    ''' <param name="company">The company.</param>
    ''' <param name="patientCode">The patient code.</param>
    ''' <param name="admissionNumber">The admission number.</param>
    ''' <returns></returns>
    Public Shared Function ListServicesProceduresPathologiesByadmissionCodeIngreso(company As String, patientCode As String, admissionNumber As String) As XPCollection(Of ViewServicesProceduresPathologies)
        Dim service As New CrystalServiceXpo(company)
        Return service.ListServicesProceduresPathologiesByadmissionCodeIngreso(patientCode, admissionNumber)
    End Function

    ''' <summary>
    ''' Lists the services procedures qx byadmission code ingreso.
    ''' </summary>
    ''' <param name="company">The company.</param>
    ''' <param name="patientCode">The patient code.</param>
    ''' <param name="admissionNumber">The admission number.</param>
    ''' <returns></returns>
    Public Shared Function ListServicesProceduresQxByadmissionCodeIngreso(company As String, patientCode As String, admissionNumber As String) As XPCollection(Of ViewServicesProceduresQx)
        Dim service As New CrystalServiceXpo(company)
        Return service.ListServicesProceduresQxByadmissionCodeIngreso(patientCode, admissionNumber)
    End Function

    ''' <summary>
    ''' Lists the services procedures report qx byadmission code ingreso.
    ''' </summary>
    ''' <param name="company">The company.</param>
    ''' <param name="patientCode">The patient code.</param>
    ''' <param name="admissionNumber">The admission number.</param>
    ''' <returns></returns>
    Public Shared Function ListServicesProceduresReportQxByadmissionCodeIngreso(company As String, patientCode As String, admissionNumber As String) As XPCollection(Of ViewServicesProceduresReportQx)
        Dim service As New CrystalServiceXpo(company)
        Return service.ListServicesProceduresReportQxByadmissionCodeIngreso(patientCode, admissionNumber)
    End Function

    ''' <summary>
    ''' Lists the services procedures no qx byadmission code ingreso.
    ''' </summary>
    ''' <param name="company">The company.</param>
    ''' <param name="patientCode">The patient code.</param>
    ''' <param name="admissionNumber">The admission number.</param>
    ''' <returns></returns>
    Public Shared Function ListServicesProceduresNoQxByadmissionCodeIngreso(company As String, patientCode As String, admissionNumber As String) As XPCollection(Of ViewServicesProceduresNoQx)
        Dim service As New CrystalServiceXpo(company)
        Return service.ListServicesProceduresNoQxByadmissionCodeIngreso(patientCode, admissionNumber)
    End Function

    ''' <summary>
    ''' Lists the consultation by patient code ingreso.
    ''' </summary>
    ''' <param name="company">The company.</param>
    ''' <param name="patientCode">The patient code.</param>
    ''' <param name="admissionNumber">The admission number.</param>
    ''' <returns></returns>
    Public Shared Function ListConsultationByPatientCodeIngreso(company As String, patientCode As String, admissionNumber As String) As XPCollection(Of ViewConsultation)
        Dim service As New CrystalServiceXpo(company)
        Return service.ListConsultationByPatientCodeIngreso(patientCode, admissionNumber)
    End Function

    ''' <summary>
    ''' Lists the services procedures consultation byadmission code ingreso.
    ''' </summary>
    ''' <param name="company">The company.</param>
    ''' <param name="patientCode">The patient code.</param>
    ''' <param name="admissionNumber">The admission number.</param>
    ''' <returns></returns>
    Public Shared Function ListServicesProceduresConsultationByadmissionCodeIngreso(company As String, patientCode As String, admissionNumber As String) As XPCollection(Of ViewServicesProceduresConsultation)
        Dim service As New CrystalServiceXpo(company)
        Return service.ListServicesProceduresConsultationByadmissionCodeIngreso(patientCode, admissionNumber)
    End Function

    ''' <summary>
    ''' Lists the therapy by patient code ingreso.
    ''' </summary>
    ''' <param name="company">The company.</param>
    ''' <param name="patientCode">The patient code.</param>
    ''' <param name="admissionNumber">The admission number.</param>
    ''' <returns></returns>
    Public Shared Function ListTherapyByPatientCodeIngreso(company As String, patientCode As String, admissionNumber As String) As XPCollection(Of ViewTherapy)
        Dim service As New CrystalServiceXpo(company)
        Return service.ListTherapyByPatientCodeIngreso(patientCode, admissionNumber)
    End Function

    ''' <summary>
    ''' Lists the services procedures therapy byadmission code ingreso.
    ''' </summary>
    ''' <param name="company">The company.</param>
    ''' <param name="patientCode">The patient code.</param>
    ''' <param name="admissionNumber">The admission number.</param>
    ''' <returns></returns>
    Public Shared Function ListServicesProceduresTherapyByadmissionCodeIngreso(company As String, patientCode As String, admissionNumber As String) As XPCollection(Of ViewServicesProceduresTherapy)
        Dim service As New CrystalServiceXpo(company)
        Return service.ListServicesProceduresTherapyByadmissionCodeIngreso(patientCode, admissionNumber)
    End Function

    ''' <summary>
    ''' Lists the oxygen consumption byadmission code ingreso.
    ''' </summary>
    ''' <param name="company">The company.</param>
    ''' <param name="patientCode">The patient code.</param>
    ''' <param name="admissionNumber">The admission number.</param>
    ''' <returns></returns>
    Public Shared Function ListOxygenConsumptionByadmissionCodeIngreso(company As String, patientCode As String, admissionNumber As String) As XPCollection(Of ViewOxygenConsumption)
        Dim service As New CrystalServiceXpo(company)
        Return service.ListOxygenConsumptionByadmissionCodeIngreso(patientCode, admissionNumber)
    End Function

    ''' <summary>
    ''' Lists the services nursing procedures byadmission code ingreso atention center.
    ''' </summary>
    ''' <param name="company">The company.</param>
    ''' <param name="patientCode">The patient code.</param>
    ''' <param name="admissionNumber">The admission number.</param>
    ''' <param name="atentionCenter">The atention center.</param>
    ''' <returns></returns>
    Public Shared Function ListServicesNursingProceduresByadmissionCodeIngresoAtentionCenter(company As String, patientCode As String, admissionNumber As String, atentionCenter As String) As XPCollection(Of ViewServiceProceduresNursingProcedure)
        Dim service As New CrystalServiceXpo(company)
        Return service.ListServicesNursingProceduresDetailByadmissionCodeIngresoCodCenAte(patientCode, admissionNumber, atentionCenter)
    End Function

    ''' <summary>
    ''' Lists the nursing procedures byadmission code ingreso cod cen ate.
    ''' </summary>
    ''' <param name="company">The company.</param>
    ''' <param name="patientCode">The patient code.</param>
    ''' <param name="admissionNumber">The admission number.</param>
    ''' <param name="atentionCenter">The atention center.</param>
    ''' <returns></returns>
    Public Shared Function ListNursingProceduresByadmissionCodeIngresoCodCenAte(company As String, patientCode As String, admissionNumber As String, atentionCenter As String) As XPCollection(Of ViewNursingProcedures)
        Dim service As New CrystalServiceXpo(company)
        Dim master As XPCollection(Of ViewNursingProcedures) = service.ListNursingProceduresByadmissionCodeIngresoCodCenAte(patientCode, admissionNumber, atentionCenter)
        'Dim detail As XPCollection(Of ViewNursingProceduresDetail) = service.ListNursingProceduresDetailByadmissionCodeIngresoCodCenAte(patientCode, admissionNumber, atentionCenter)
        'Dim masterList = master.ToList()
        'Dim detailList = detail.ToList()
        'If masterList.Count > 0 AndAlso detailList.Count > 0 Then
        '    For Each mast In master
        '        Dim procDetailList As List(Of ViewNursingProceduresDetail) = (From e In detailList Where e.Llave = mast.Llave Select e).ToList()
        '        mast.ListNursingProcedureDetail = procDetailList
        '    Next
        'End If
        Return master
    End Function

    ''' <summary>
    ''' Lists the nursing procedures detail byadmission code ingreso cod cen ate.
    ''' </summary>
    ''' <param name="company">The company.</param>
    ''' <param name="patientCode">The patient code.</param>
    ''' <param name="admissionNumber">The admission number.</param>
    ''' <param name="atentionCenter">The atention center.</param>
    ''' <returns></returns>
    Public Shared Function ListNursingProceduresDetailByadmissionCodeIngresoCodCenAte(company As String, patientCode As String, admissionNumber As String, atentionCenter As String) As XPCollection(Of ViewNursingProceduresDetail)
        Dim service As New CrystalServiceXpo(company)
        Return service.ListNursingProceduresDetailByadmissionCodeIngresoCodCenAte(patientCode, admissionNumber, atentionCenter)
    End Function

    ''' <summary>
    ''' lista las solicitudes en el dashboard de farmacia
    ''' </summary>
    ''' <param name="company"></param>
    ''' <param name="codeCareCenter"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function ListDashBoardPharmacy(company As String, codeCareCenter As String) As XPCollection(Of ViewDashBoardPharmacy)
        Dim service As New CrystalServiceXpo(company)
        Return service.ListDashBoardPharmacy(codeCareCenter)
    End Function

    ''' <summary>
    ''' lista de admission en Reporte
    ''' </summary>
    ''' <param name="company"></param>
    ''' <param name="NumAdmission"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function ListAdmissionReport(company As String, NumAdmission As String) As XPCollection(Of VAdmission)
        Dim service As New CrystalServiceXpo(company)
        Return service.ListAdmissionsToReport(NumAdmission)
    End Function

    ''' <summary>
    ''' Gets the bed rate by bed identifier.
    ''' </summary>
    ''' <returns></returns>
    Public Shared Function GetBedRateByBedId(company As String, bedId As Integer) As XPInstantFeedbackSource
        Dim service As New CrystalServiceXpo(company)
        Return service.GetBedRateByBedId(bedId)
    End Function

    ''' <summary>
    ''' Lista los tipos de estancia
    ''' </summary>
    ''' <returns></returns>
    Public Shared Function GetAllStayType(company As String) As XPInstantFeedbackSource
        Dim service As New CrystalServiceXpo(company)
        Return service.GetAllStayType()
    End Function

    ''' <summary>
    ''' Lista todos los centros de atencion
    ''' </summary>
    ''' <returns></returns>
    Public Shared Function GetAllCareCenter(company As String) As XPInstantFeedbackSource
        Dim service As New CrystalServiceXpo(company)
        Return service.GetAllCareCenter()
    End Function

    ''' <summary>
    ''' Lista todas las unidades funcionales
    ''' </summary>
    ''' <returns></returns>
    Public Shared Function GetAllFunctionalUnit(company As String) As XPInstantFeedbackSource
        Dim service As New CrystalServiceXpo(company)
        Return service.GetAllFunctionalUnit()
    End Function
    ''' <summary>
    ''' Lists all beds.
    ''' </summary>
    ''' <param name="company">The company.</param>
    ''' <returns></returns>
    Public Shared Function ListAllBeds(company As String) As XPInstantFeedbackSource
        Dim service As New CrystalServiceXpo(company)
        Return service.ListAllBeds()
    End Function
    ''' <summary>
    ''' lista las devoluciones en el dashboard de farmacia
    ''' </summary>
    ''' <param name="company"></param>
    ''' <param name="codeCareCenter"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function ListDashBoardPharmacyDevolution(company As String, codeCareCenter As String) As XPCollection(Of ViewDashBoardPharmacyDevolution)
        Dim service As New CrystalServiceXpo(company)
        Return service.ListDashBoardPharmacyDevolution(codeCareCenter)
    End Function

    ''' <summary>
    ''' Lista todos los ingresos para el frontal de liquidación
    ''' </summary>
    ''' <param name="company">Nombre del contenedor HIS</param>
    Public Shared Function ListAdmissionsToLiquidation(ByVal company As String) As XPInstantFeedbackSource
        Dim service As New CrystalServiceXpo(company)
        Return service.ListAdmissionsToLiquidation()
    End Function

    ''' <summary>
    ''' Lista todos los ingresos para el frontal de listado de facturas
    ''' </summary>
    ''' <param name="company">Nombre del contenedor HIS</param>
    Public Shared Function ListAdmissionsToReport(ByVal company As String) As XPInstantFeedbackSource
        Dim service As New CrystalServiceXpo(company)
        Return service.ListAdmissionsToReport()
    End Function

    ''' <summary>
    ''' Lista todos los ingresos para el frontal de boleta de salida
    ''' </summary>
    ''' <param name="company">Nombre del contenedor HIS</param>
    Public Shared Function ListAdmissionsToReportSlipOut(ByVal company As String) As XPInstantFeedbackSource
        Dim service As New CrystalServiceXpo(company)
        Return service.ListAdmissionsToReportSlipOut()
    End Function

    ''' <summary>
    ''' lista el ingreso por codigo
    ''' </summary>
    ''' <param name="company"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function ListAdmissionsToReportSlipOutById(ByVal company As String, ByVal AdmissionCode As String) As XPCollection(Of ViewAdmissionsToReportSlipOut)
        Dim service As New CrystalServiceXpo(company)
        Return service.ListAdmissionsToReportSlipOutById(AdmissionCode)
    End Function

    ''' <summary>
    ''' lista el ingreso por codigo
    ''' </summary>
    ''' <param name="company"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function ListAdmissionsToReportById(ByVal company As String, ByVal AdmissionCode As String) As XPCollection(Of ViewAdmissionsToReport)
        Dim service As New CrystalServiceXpo(company)
        Return service.ListAdmissionsToReportById(AdmissionCode)
    End Function

    Public Shared Function ListAdmissionsToLiquidationConfirm(company As String) As XPInstantFeedbackSource
        Dim service As New CrystalServiceXpo(company)
        Return service.ListAdmissionsToLiquidationConfirm()
    End Function

    ''' <summary>
    ''' Lista las unidades funcionales por permiso de usuario
    ''' </summary>
    Public Shared Function ListViewListFuncionalUnitAuthorization(ByVal company As String, ByVal CodeGroup As String, ByVal CodeUsers As String) As XPInstantFeedbackSource
        Dim service As New CrystalServiceXpo(company)
        Return service.ListViewListFuncionalUnitAuthorization(CodeGroup, CodeUsers)
    End Function

    ''' <summary>
    ''' Lista todos los ingresos para el frontal de liquidación
    ''' </summary>
    ''' <param name="company">Nombre del contenedor HIS</param>
    Public Shared Function ListAdmissionsLiquidation(ByVal company As String) As LinqInstantFeedbackSource
        Dim service As New CrystalServiceXpo(company)
        Return service.ListAdmissionsLiquidation()
    End Function


    Public Shared Function ListAdmissionsByPatientCode(company As String, patientCode As String) As XPInstantFeedbackSource
        Dim service As New CrystalServiceXpo(company)
        Return service.ListAdmissionsByPatientCode(patientCode)
    End Function

    ''' <summary>
    ''' Lista todos los ingresos que no estan anulados ni cancelados
    ''' </summary>
    ''' <param name="company">Nombre del contenedor HIS</param>
    Public Shared Function ListAdmissions(ByVal company As String) As LinqInstantFeedbackSource
        Dim service As New CrystalServiceXpo(company)
        Return service.ListAdmissions()
    End Function

    ''' <summary>
    ''' Lista todos los ingresos que no estan anulados ni cancelados
    ''' </summary>
    ''' <param name="company">Nombre del contenedor HIS</param>
    Public Shared Function ListAllAdmissions(ByVal company As String) As LinqInstantFeedbackSource
        Dim service As New CrystalServiceXpo(company)
        Return service.ListAllAdmissions()
    End Function

    ''' <summary>
    ''' Lista todos los ingresos por el nit del terceo
    ''' </summary>
    ''' <param name="company">Nombre del contenedor HIS</param>
    Public Shared Function ListAdmissionsPatientCode(ByVal company As String, patientCode As String) As LinqInstantFeedbackSource
        Dim service As New CrystalServiceXpo(company)
        Return service.ListAdmissionsPatientCode(patientCode)
    End Function
    ''' <summary>
    ''' Lists the admissions patient code status.
    ''' </summary>
    ''' <param name="company">The company.</param>
    ''' <param name="patientCode">The patient code.</param>
    ''' <param name="status">The status.</param>
    ''' <returns></returns>
    Public Shared Function ListAdmissionsPatientCodeStatus(company As String, patientCode As String, status As String) As LinqInstantFeedbackSource
        Dim service As New CrystalServiceXpo(company)
        Return service.ListAdmissionsPatientCodeStatus(patientCode, status)
    End Function

    Public Shared Function ListAdmissionsByPatientCodeStatus(company As String, patientCode As String, status As String) As LinqInstantFeedbackSource
        Dim service As New CrystalServiceXpo(company)
        Return service.ListAdmissionsByPatientCodeStatus(patientCode, status)
    End Function
    ''' <summary>
    ''' lista los profesionales de la salud
    ''' </summary>
    ''' <param name="company"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function ListHealthCareProfessionalAll(ByVal company As String) As XPInstantFeedbackSource
        Dim service As New CrystalServiceXpo(company)
        Return service.ListHealthCareProfessionalAll()
    End Function
    ''' <summary>
    ''' lista los profesionales de la salud activos
    ''' </summary>
    ''' <param name="company"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function ListHealthCareProfessional(ByVal company As String) As XPInstantFeedbackSource
        Dim service As New CrystalServiceXpo(company)
        Return service.ListHealthCareProfessional()
    End Function
    ''' <summary>
    ''' Lists the health care professional by profile.
    ''' </summary>
    ''' <param name="p1">The p1.</param>
    ''' <param name="profile">The profile.</param>
    ''' <returns></returns>
    Public Shared Function ListHealthCareProfessionalByProfile(company As String, profile As List(Of Integer)) As XPInstantFeedbackSource
        Dim service As New CrystalServiceXpo(company)
        Return service.ListHealthCareProfessionalByProfile(profile)
    End Function
    ''' <summary>
    ''' lista los profesionales de la salud con xpCollection
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function ListHealthCareProfessionalXpCollection(ByVal company As String) As XPCollection(Of HealthCareProfessionalXpo)
        Dim service As New CrystalServiceXpo(company)
        Return service.ListHealthCareProfessionalXpCollection()
    End Function

    ''' <summary>
    ''' lista los profesionales de la salud con xpCollection filtrado por la especialidad
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function ListHealthCareProfessionalSpecialtyXpCollection(ByVal company As String) As XPCollection(Of HealthCareProfessionalXpo)
        Dim service As New CrystalServiceXpo(company)
        Return service.ListHealthCareProfessionalSpecialtyXpCollection()
    End Function

    ''' <summary>
    ''' lista los profesionales de la salud
    ''' </summary>
    ''' <param name="company"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function ListAllPatients(ByVal company As String) As XPInstantFeedbackSource
        Dim service As New CrystalServiceXpo(company)
        Return service.ListAllPatients()
    End Function

    ''' <summary>
    ''' lista los profesionales de la salud por contrato
    ''' </summary>
    ''' <param name="company"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function ListHealthProfessionalCodeByMedicalFeesContractId(medicalFeesContractId As Integer, ByVal company As String) As XPInstantFeedbackSource
        Dim service As New CrystalServiceXpo(company)
        Return service.ListHealthProfessionalCodeByMedicalFeesContractId(medicalFeesContractId)
    End Function

    ''' <summary>
    ''' lista un profesional por codigo
    ''' </summary>
    ''' <param name="company"></param>
    ''' <param name="code"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function GetCareProfessionalByCode(ByVal company As String, code As String) As XPCollection(Of HealthCareProfessionalXpo)
        Dim service As New CrystalServiceXpo(company)
        Return service.GetCareProfessionalByCode(code)
    End Function

    ''' <summary>
    ''' lista las especialidades por estado
    ''' </summary>
    ''' <param name="company"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function ListSpecialty(ByVal company As String, ByVal status As Boolean) As XPInstantFeedbackSource
        Dim service As New CrystalServiceXpo(company)
        Return service.ListSpecialty(status)
    End Function

    Public Shared Function ListSpecialtyXpCollection(ByVal status As Boolean, ByVal company As String) As XPCollection
        Dim service As New CrystalServiceXpo(company)
        Return service.ListSpecialtyXpCollection(status)
    End Function

    ''' <summary>
    ''' lista las especialidades por estado
    ''' </summary>
    ''' <param name="company"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function ListSpecialties(ByVal company As String, ByVal status As Boolean) As XPInstantFeedbackSource
        Dim service As New CrystalServiceXpo(company)
        Return service.ListSpecialties(status)
    End Function

    ''' <summary>
    ''' Función para obtener una lista de datos de una entidad XPO
    ''' </summary>
    ''' <typeparam name="T">Objeto XPO</typeparam>
    ''' <param name="Company">Compañia Actual</param>
    ''' <param name="Fun">Objeto tipo Función opcional para filtrar los datos necesarios</param>
    ''' <returns>Objeto</returns>
    Public Shared Function ListCollectionCrystalReport(Of T)(ByVal Company As String, Optional Fun As Func(Of T, Boolean) = Nothing, Optional criteria As String = Nothing) As Object
        Dim Crystal As New CrystalServiceXpo(Company)
        Return Crystal.GetCollection(Of T)(Fun, criteria)
    End Function

    ''' <summary>
    ''' Funcion para obtener las actividades
    ''' </summary>
    ''' <param name="Company">Compañia Actual</param>
    ''' <returns>Objeto</returns>
    Public Shared Function ListActivitiesHIS(ByVal company As String) As XPInstantFeedbackSource
        Dim service As New CrystalServiceXpo(company)
        Return service.ListActivities()
    End Function

    ''' <summary>
    ''' Funcion para obtener las empresas
    ''' </summary>
    ''' <param name="Company">Compañia Actual</param>
    ''' <returns>Objeto</returns>
    Public Shared Function ListCompanyHIS(ByVal company As String) As XPInstantFeedbackSource
        Dim service As New CrystalServiceXpo(company)
        Return service.ListCompany()
    End Function

    ''' <summary>
    ''' Funcion para obtener las ubicaciones
    ''' </summary>
    ''' <param name="Company">Compañia Actual</param>
    ''' <returns>Objeto</returns>
    Public Shared Function ListLocationHIS(ByVal company As String) As XPInstantFeedbackSource
        Dim service As New CrystalServiceXpo(company)
        Return service.ListLocations()
    End Function

    ''' <summary>
    ''' Funcion para obtener los grupos étnicos
    ''' </summary>
    ''' <param name="Company">Compañia Actual</param>
    ''' <returns>Objeto</returns>
    Public Shared Function ListEthnicGroupHIS(ByVal company As String) As XPInstantFeedbackSource
        Dim service As New CrystalServiceXpo(company)
        Return service.ListEthnicGroup()
    End Function

    ''' <summary>
    ''' Funcion para obtener los Niveles
    ''' </summary>
    ''' <param name="Company">Compañia Actual</param>
    ''' <returns>Objeto</returns>
    Public Shared Function ListLevelsSHIS(ByVal company As String) As XPInstantFeedbackSource
        Dim service As New CrystalServiceXpo(company)
        Return service.ListLevels()
    End Function

    ''' <summary>
    ''' Funcion para obtener los Niveles de educación
    ''' </summary>
    ''' <param name="Company">Compañia Actual</param>
    ''' <returns>Objeto</returns>
    Public Shared Function ListEducationLevelsHIS(ByVal company As String) As XPInstantFeedbackSource
        Dim service As New CrystalServiceXpo(company)
        Return service.ListEducationLevels
    End Function

    ''' <summary>
    ''' Funcion para obtener los Lenguajes
    ''' </summary>
    ''' <param name="Company">Compañia Actual</param>
    ''' <returns>Objeto</returns>
    Public Shared Function ListLanguageHIS(ByVal company As String) As XPInstantFeedbackSource
        Dim service As New CrystalServiceXpo(company)
        Return service.ListLanguage
    End Function

    ''' <summary>
    ''' Funcion para obtener las creencias
    ''' </summary>
    ''' <param name="Company">Compañia Actual</param>
    ''' <returns>Objeto</returns>
    Public Shared Function ListBeliefHIS(ByVal company As String) As XPInstantFeedbackSource
        Dim service As New CrystalServiceXpo(company)
        Return service.ListBelief
    End Function

    ''' <summary>
    ''' Funcion para obtener las creencias
    ''' </summary>
    ''' <param name="Company">Compañia Actual</param>
    ''' <returns>Objeto</returns>
    Public Shared Function ListDisabilityHIS(ByVal company As String) As XPInstantFeedbackSource
        Dim service As New CrystalServiceXpo(company)
        Return service.ListDisability
    End Function

    ''' <summary>
    ''' Funcion para obtener los grupos especiales
    ''' </summary>
    ''' <param name="Company">Compañia Actual</param>
    ''' <returns>Objeto</returns>
    Public Shared Function ListSpecialGroupsHIS(ByVal company As String) As XPInstantFeedbackSource
        Dim service As New CrystalServiceXpo(company)
        Return service.ListSpecialGroups
    End Function

    ''' <summary>
    ''' Funcion para obtener los centros de atención
    ''' </summary>
    ''' <param name="Company">Compañia Actual</param>
    ''' <returns>Objeto</returns>
    Public Shared Function ListCentersHIS(ByVal company As String) As XPInstantFeedbackSource
        Dim service As New CrystalServiceXpo(company)
        Return service.ListCenters
    End Function

    ''' <summary>
    ''' Lista los municipios
    ''' </summary>
    ''' <param name="company"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function ListTown(ByVal company As String) As XPInstantFeedbackSource
        Dim service As New CrystalServiceXpo(company)
        Return service.ListTown
    End Function
    ''' <summary>
    ''' Lista las IPS
    ''' </summary>
    ''' <param name="company"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function ListIPS(ByVal company As String) As XPInstantFeedbackSource
        Dim service As New CrystalServiceXpo(company)
        Return service.ListIPS
    End Function
    ''' <summary>
    ''' lista las IPS por estado
    ''' </summary>
    ''' <param name="company"></param>
    ''' <param name="status"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function ListIPSByStatus(company As String, status As Boolean) As XPInstantFeedbackSource
        Dim service As New CrystalServiceXpo(company)
        Return service.ListIPSByStatus(status)
    End Function
    ''' <summary>
    ''' lista los CUPS por estado
    ''' </summary>
    ''' <param name="company"></param>
    ''' <param name="status"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function ListCUPSByStatus(company As String, status As Boolean) As XPInstantFeedbackSource
        Dim service As New CrystalServiceXpo(company)
        Return service.ListCUPSByStatus(status)
    End Function

    Public Shared Function ListCUPSCrystalByStatusType(company As String, status As Boolean, type As Integer) As XPInstantFeedbackSource
        Dim service As New CrystalServiceXpo(company)
        Return service.ListCUPSCrystalByStatusType(status, type)
    End Function

    ''' <summary>
    ''' Lista las Unidades Funcionales
    ''' </summary>
    ''' <param name="company"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function ListUF(ByVal company As String) As XPInstantFeedbackSource
        Dim service As New CrystalServiceXpo(company)
        Return service.ListUF
    End Function

    ''' <summary>
    ''' Lista los salarios minimos
    ''' </summary>
    ''' <param name="company"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function ListMinWage(ByVal company As String) As XPInstantFeedbackSource
        Dim service As New CrystalServiceXpo(company)
        Return service.ListMinWage
    End Function


    ''' <summary>
    ''' Lista las camas 
    ''' </summary>
    ''' <param name="company"></param>
    ''' <param name="center"></param>
    ''' <param name="uf"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function ListHospitalizationBeds(company As String, center As String, uf As String)
        Dim service As New CrystalServiceXpo(company)
        Return service.ListBeds(center, uf)
    End Function

    ''' <summary>
    ''' Lista los ingresos para ordenes de servicio
    ''' </summary>
    ''' <param name="company"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function GetViewAdmissionServiceOrder(ByVal company As String) As XPInstantFeedbackSource
        Dim service As New CrystalServiceXpo(company)
        Return service.GetViewAdmissionServiceOrder
    End Function
#End Region

#Region "Seguridad"
    ''' <summary>
    ''' lista todos los roles
    ''' </summary>
    ''' <param name="company"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function GetRoles(company As String) As XPInstantFeedbackSource
        Dim service As New SecurityServicesXpo(company)
        Return service.GetRoles()
    End Function
#End Region

End Class
