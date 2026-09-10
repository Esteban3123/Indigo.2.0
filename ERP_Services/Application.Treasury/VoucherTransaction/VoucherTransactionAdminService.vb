'***********************************************************************
' Assembly         : Application.Treasury
' Author           : Diego Andrés Roldán Lozano
' Created          : 13-06-2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Entities
Imports Domain.Base
Imports Infrastructure.CrossCutting.Exceptions
Imports Application.Base
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities
Imports System.Data.Entity.Infrastructure
Imports System.Data.Entity.Core
Imports System.Data.Entity.Validation
Imports Application.Payments
Imports Application.Accounting
Imports System.Transactions
Imports Domain.Entities.Service
Imports System.Text
Imports Infrastructure.CrossCutting.Resources
Imports Application.Portfolio
Imports Application.EventHandlers
Imports Application.EventHandlers.Model
Imports Application.EventHandlers.Enums.Enums

Public Class VoucherTransactionAdminService
    Implements IVoucherTransactionAdminService

#Region "Fields"

    ''' <summary>
    ''' nombre del esquema de tesorería
    ''' </summary>
    Private Const TREASURY As String = "Treasury"

    Private treasuryService As ITreasuryServices

    ''' <summary>
    ''' Repositorio de comprobantes de egreso
    ''' </summary>
    Private _voucherRepository As IVoucherTransactionRepository

    ''' <summary>
    ''' Repositorio para los parametros de empresa
    ''' </summary>
    ''' <remarks></remarks>
    Private _companySettingsRepository As ICompanySettingsRepository

    ''' <summary>
    ''' repositorio de las secuencias
    ''' </summary>
    Private _secuenseDRepository As ISequenseTreasuryDRepository
    ''' <summary>
    ''' repositorio de las chequeras
    ''' </summary>
    Private _checkBookRepository As ICheckRepository
    ''' <summary>
    ''' repositorio de cheques bloqueados
    ''' </summary>
    Private _checkBlockRepository As ICheckBlockRepository
    ''' <summary>
    ''' The _account payable
    ''' </summary>
    Private _accountPayable As IAccountPayableAdminService
    ''' <summary>
    ''' repositorio de cuentas por pagar
    ''' </summary>
    Private _accountPayableRepository As IAccountPayableRepository
    ''' <summary>
    ''' Variable tipo repositorio para parametros de pago
    ''' </summary>
    ''' <remarks></remarks>
    Private _settingsTreasuryRepository As ISettingsTreasuryRepository
    ''' <summary>
    ''' repositorio de conceptos de egreso
    ''' </summary>
    Private _expenseConceptRepository As IExpenseConceptRepository
    ''' <summary>
    ''' servicio de aplicacion de cuentas bancarias
    ''' </summary>
    Private _entityAccountAdminService As IEntityBankAccountAdminService
    ''' <summary>
    ''' Repositorio de cuentas bancarias
    ''' </summary>
    Private _entityBankAccountRepository As IEntityBankAccountRepository
    ''' <summary>
    ''' servicios de aplicacion de contabilidad
    ''' </summary>
    Private _accountingAdminService As IAccountingDocumentAdminService
    ''' <summary>
    ''' repositorio de avances de tesoreria
    ''' </summary>
    Private _treasuryAdvanceRepository As ITreasuryAdvanceRepository
    ''' <summary>
    ''' servicios de aplicacion de movimientos de cuentas por pagar
    ''' </summary>
    Private _movementAccountPayableAdminService As IMovementAccountPayableAdminService
    ''' <summary>
    ''' Servicios de aplicacion de anticipos de pagos
    ''' </summary>
    Private _moneyAdvanceAdminService As IMoneyAdvanceAdminService
    ''' <summary>
    ''' Repositorios de anticipos de pagos
    ''' </summary>
    Private _moneyAdvanceRepository As IMoneyAdvanceRepository
    ''' <summary>
    ''' Servicios de aplicacion de Proveedor
    ''' </summary>
    Private _supplierAdminService As Application.Common.ISupplierAdminService
    ''' <summary>
    ''' repositorio de proveedores
    ''' </summary>
    Private _supplierRepository As Domain.Entities.ISupplierRepository
    ''' <summary>
    ''' servicios de aplicacion de cajas
    ''' </summary>
    Private _cashAdminService As ICashRegisterAdminService
    ''' <summary>
    ''' repositorio de cajas
    ''' </summary>
    Private _cashRegisterRepository As ICashRegisterRepository
    ''' <summary>
    ''' servicios de aplicacion de pagos de facturas
    ''' </summary>
    Private _dischargeBillAdminService As IDischargeBillAdminService
    ''' <summary>
    ''' repositorio de detalle de egresos
    ''' </summary>
    Private _dischargeBillRepository As IDischargeBillRepository
    ''' <summary>
    ''' servicios de aplicacion de reembolsos
    ''' </summary>
    Private _refundAdminService As IRefundAdminService
    ''' <summary>
    ''' repositorio de reembolsos
    ''' </summary>
    Private _refundRepository As IRefundRepository
    ''' <summary>
    ''' servicios de aplicacion de secuencias
    ''' </summary>
    Private _sequencePaymentsAdminService As IPaymentsSequenseAdminService
    ''' <summary>
    ''' servicios de aplicacion de anticipos de cartera
    ''' </summary>
    Private _portfolioAdvanceAdminService As IPortfolioAdvanceAdminService
    ''' <summary>
    ''' repositorio de anticipos de cartera
    ''' </summary>
    Private _portfolioAdvanceRepository As IPortfolioAdvanceRepository
    ''' <summary>
    ''' servicios de aplicacion de control de tesoreria
    ''' </summary>
    Private _treasuryControlAdminService As ITreasuryControlAdminService
    ''' <summary>
    ''' Repositorio de meses cerrados
    ''' </summary>
    Private _closeMonthRepository As ICloseMonthRepository
    ''' <summary>
    ''' The _puc repository
    ''' </summary>
    Private _pucRepository As IPUCRepository
    ''' <summary>
    ''' repositorio de cheques anulados
    ''' </summary>
    Private _cancellationCheckRepository As ICancellationCheckRepository
    ''' <summary>
    ''' Repositorio de cheques en espera
    ''' </summary>
    Private _outstandingCheckRepository As IOutstandingChecksRepository
    Private _paymentNotesAccountPayableAdvanceRepository As IPaymentNotesAccountPayableAdvanceRepository
    Private _paymentTransferRepository As ITransfersRepository
    Private _documentTypeRepository As IDocumentTypeRepository
    Private _crossingAccountRepository As ICrossingAccountRepository

    ''' <summary>
    ''' repositorio de documento soporte electronico
    ''' </summary>
    Private _electronicSupportDocumentRepository As IElectronicSupportDocumentRepository

    ''' <summary>
    ''' Repositorio parametro cuentas contables
    ''' </summary>
    Private _settingsAccountRepository As ISettingsAccountRepository

    ''' <summary>
    ''' Event Publisher
    ''' </summary>
    Private ReadOnly _eventProxy As IEventProxy

#End Region

#Region "Functions"
    Public Sub New(ByVal voucherRepository As IVoucherTransactionRepository, ByVal secuenseDRepository As ISequenseTreasuryDRepository, ByVal checkBookRepository As ICheckRepository,
                   ByVal checkBlockRepository As ICheckBlockRepository, ByVal accountPayable As IAccountPayableAdminService, ByVal settingsTreasuryRepository As ISettingsTreasuryRepository,
                   ByVal expenseConceptRepository As IExpenseConceptRepository, ByVal entityAccountAdminService As IEntityBankAccountAdminService, ByVal accountingAdminService As IAccountingDocumentAdminService,
                   ByVal treasuryAdvanceRepository As ITreasuryAdvanceRepository, ByVal movementAccountPayableAdminService As IMovementAccountPayableAdminService,
                   ByVal moneyAdvanceAdminService As IMoneyAdvanceAdminService, ByVal supplierAdminService As Application.Common.ISupplierAdminService, ByVal cashAdminService As ICashRegisterAdminService,
                   ByVal dischargeBillAdminService As IDischargeBillAdminService, ByVal refundAdminService As IRefundAdminService, ByVal sequencePaymentsAdminService As IPaymentsSequenseAdminService,
                   ByVal portfolioAdvanceAdminService As IPortfolioAdvanceAdminService, ByVal entityBankAccountRepository As IEntityBankAccountRepository, ByVal cashRegisterRepository As ICashRegisterRepository,
                   ByVal dischargeBillRepository As IDischargeBillRepository, ByVal accountPayableRepository As IAccountPayableRepository, ByVal portfolioAdvanceRepository As IPortfolioAdvanceRepository,
                   ByVal refundRepository As IRefundRepository, ByVal treasuryControlAdminService As ITreasuryControlAdminService, ByVal closeMonthRepository As ICloseMonthRepository,
                   ByVal pucRepository As IPUCRepository, ByVal cancellationCheckRepository As ICancellationCheckRepository, ByVal supplierRepository As Domain.Entities.ISupplierRepository,
                   ByVal outstandingCheckRepository As IOutstandingChecksRepository, ByVal moneyAdvanceRepository As IMoneyAdvanceRepository, _treasuryService As ITreasuryServices,
                   paymentNotesAccountPayableAdvanceRepository As IPaymentNotesAccountPayableAdvanceRepository, paymentTransferRepository As ITransfersRepository, documentTypeRepository As IDocumentTypeRepository,
                   crossingAccountRepository As ICrossingAccountRepository, ByVal ElectronicSupportDocumentRepository As IElectronicSupportDocumentRepository, ByVal SettingsAccountRepository As ISettingsAccountRepository, CompanySettingsRepository As ICompanySettingsRepository,
                   eventProxy As IEventProxy)
        If voucherRepository Is Nothing Then
            Throw New ArgumentNullException("TreasuryRepository")
        End If
        If supplierRepository Is Nothing Then
            Throw New ArgumentNullException("supplierRepository")
        End If
        If cancellationCheckRepository Is Nothing Then
            Throw New ArgumentNullException("cancellationCheckRepository")
        End If
        If pucRepository Is Nothing Then
            Throw New ArgumentNullException("pucRepository")
        End If
        If closeMonthRepository Is Nothing Then
            Throw New ArgumentNullException("closeMonthRepository")
        End If
        If treasuryControlAdminService Is Nothing Then
            Throw New ArgumentNullException("treasuryControlAdminService")
        End If
        If refundRepository Is Nothing Then
            Throw New ArgumentNullException("refundRepository")
        End If
        If portfolioAdvanceRepository Is Nothing Then
            Throw New ArgumentNullException("portfolioAdvanceRepository")
        End If
        If accountPayableRepository Is Nothing Then
            Throw New ArgumentNullException("accountPayableRepository")
        End If
        If dischargeBillRepository Is Nothing Then
            Throw New ArgumentNullException("dischargeBillRepository")
        End If
        If cashRegisterRepository Is Nothing Then
            Throw New ArgumentNullException("cashRegisterRepository")
        End If
        If entityBankAccountRepository Is Nothing Then
            Throw New ArgumentNullException("entityBankAccountRepository")
        End If
        If portfolioAdvanceAdminService Is Nothing Then
            Throw New ArgumentNullException("portfolioAdvanceAdminService")
        End If
        If dischargeBillAdminService Is Nothing Then
            Throw New ArgumentNullException("dischargeBillAdminService")
        End If
        If sequencePaymentsAdminService Is Nothing Then
            Throw New ArgumentNullException("sequencePayments")
        End If
        If refundAdminService Is Nothing Then
            Throw New ArgumentNullException("refundAdminService")
        End If
        If cashAdminService Is Nothing Then
            Throw New ArgumentNullException("cashAdminService")
        End If
        If secuenseDRepository Is Nothing Then
            Throw New ArgumentNullException("secuenseDRepository")
        End If
        If checkBookRepository Is Nothing Then
            Throw New ArgumentNullException("checkRepository")
        End If
        If checkBlockRepository Is Nothing Then
            Throw New ArgumentNullException("checkBlockRepository")
        End If
        If settingsTreasuryRepository Is Nothing Then
            Throw New ArgumentNullException("settingsTreasuryRepository")
        End If
        If expenseConceptRepository Is Nothing Then
            Throw New ArgumentNullException("expenseConceptRepository")
        End If
        If entityAccountAdminService Is Nothing Then
            Throw New ArgumentNullException("entityAccountAdminService")
        End If
        If accountingAdminService Is Nothing Then
            Throw New ArgumentNullException("accountingAdminService")
        End If
        If treasuryAdvanceRepository Is Nothing Then
            Throw New ArgumentNullException("treasuryAdvanceRepository")
        End If
        If movementAccountPayableAdminService Is Nothing Then
            Throw New ArgumentNullException("movementAccountPayableAdminService")
        End If
        If moneyAdvanceAdminService Is Nothing Then
            Throw New ArgumentNullException("moneyAdvanceAdminService")
        End If
        If supplierAdminService Is Nothing Then
            Throw New ArgumentNullException("supplierAdminService")
        End If
        If moneyAdvanceRepository Is Nothing Then
            Throw New ArgumentNullException("moneyAdvanceRepository")
        End If
        _documentTypeRepository = documentTypeRepository
        Me._outstandingCheckRepository = outstandingCheckRepository
        Me._voucherRepository = voucherRepository
        Me._secuenseDRepository = secuenseDRepository
        Me._checkBookRepository = checkBookRepository
        Me._checkBlockRepository = checkBlockRepository
        Me._accountPayable = accountPayable
        Me._settingsTreasuryRepository = settingsTreasuryRepository
        Me._expenseConceptRepository = expenseConceptRepository
        Me._entityAccountAdminService = entityAccountAdminService
        Me._accountingAdminService = accountingAdminService
        treasuryService = _treasuryService
        Me._treasuryAdvanceRepository = treasuryAdvanceRepository
        Me._movementAccountPayableAdminService = movementAccountPayableAdminService
        Me._moneyAdvanceAdminService = moneyAdvanceAdminService
        Me._supplierAdminService = supplierAdminService
        Me._cashAdminService = cashAdminService
        Me._dischargeBillAdminService = dischargeBillAdminService
        Me._refundAdminService = refundAdminService
        Me._sequencePaymentsAdminService = sequencePaymentsAdminService
        Me._portfolioAdvanceAdminService = portfolioAdvanceAdminService
        Me._entityBankAccountRepository = entityBankAccountRepository
        Me._cashRegisterRepository = cashRegisterRepository
        Me._dischargeBillRepository = dischargeBillRepository
        Me._accountPayableRepository = accountPayableRepository
        Me._portfolioAdvanceRepository = portfolioAdvanceRepository
        Me._refundRepository = refundRepository
        Me._treasuryControlAdminService = treasuryControlAdminService
        Me._pucRepository = pucRepository
        Me._closeMonthRepository = closeMonthRepository
        Me._cancellationCheckRepository = cancellationCheckRepository
        Me._supplierRepository = supplierRepository
        Me._moneyAdvanceRepository = moneyAdvanceRepository
        _paymentNotesAccountPayableAdvanceRepository = paymentNotesAccountPayableAdvanceRepository
        _paymentTransferRepository = paymentTransferRepository
        _crossingAccountRepository = crossingAccountRepository
        Me._electronicSupportDocumentRepository = ElectronicSupportDocumentRepository
        Me._settingsAccountRepository = SettingsAccountRepository
        Me._companySettingsRepository = CompanySettingsRepository
        _eventProxy = eventProxy
    End Sub

    Public Function GetCheckNumber(entitybanckAccountId As Integer, OperatingUnitId As Integer, UserCode As String) As SP_GetCheckNumber_Result Implements IVoucherTransactionAdminService.GetCheckNumber
        Try
            Using scope As New TransactionScope(TransactionScopeOption.Required, New TransactionOptions() With {.Timeout = TransactionManager.MaximumTimeout, .IsolationLevel = IsolationLevel.ReadCommitted})
                Dim res As SP_GetCheckNumber_Result = _voucherRepository.GetCheckNumber(entitybanckAccountId, OperatingUnitId, UserCode)
                If res.StatusResult.Equals("000") Then
                    scope.Complete()
                Else
                    scope.Dispose()
                End If
                Return res
            End Using
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return Nothing
        End Try
    End Function

    ''' <summary>
    ''' Obtiene un comprobante de egreso por codigo
    ''' </summary>
    Public Function GetVoucherTransaction(code As String, audit As AuditMessage) As ActionResult(Of VoucherTransaction) Implements IVoucherTransactionAdminService.GetVoucherTransaction
        If String.IsNullOrEmpty(code) Then
            Throw New ArgumentNullException("code")
        End If
        If audit Is Nothing Then
            Throw New ArgumentNullException("audit")
        End If
        Try
            Dim voucherTransaction As VoucherTransaction = Me._voucherRepository.GetVoucherTransaction(code.Trim())
            If voucherTransaction IsNot Nothing AndAlso voucherTransaction.Id > 0 Then
                Dim status As Integer = Infrastructure.CrossCutting.Audit.Actions.Print
                Dim auditProcess As New IndigoAuditSimpleEntity(Of VoucherTransaction)(voucherTransaction, audit, status)
                auditProcess.Execute()
            End If
            Return New ActionResult(Of VoucherTransaction) With {.StateResult = True, .ObjectEmbbeded = voucherTransaction}
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of VoucherTransaction) With {.StateResult = False}
        End Try
    End Function

    ''' <summary>
    ''' Gets the account payable in note and advance.
    ''' </summary>
    ''' <param name="listAccountPayableId">The list account payable identifier.</param>
    ''' <returns></returns>
    ''' <exception cref="System.ArgumentNullException">listAccountPayableShareId</exception>
    Public Function GetAccountPayableInNoteAndAdvance(listAccountPayableId As List(Of Integer), voucherTransactionCode As String) As ActionResult(Of List(Of String)) Implements IVoucherTransactionAdminService.GetAccountPayableInNoteAndAdvance
        If listAccountPayableId Is Nothing OrElse listAccountPayableId.Count = 0 Then
            Throw New ArgumentNullException("listAccountPayableShareId")
        End If
        Try
            Dim resultNote = _paymentNotesAccountPayableAdvanceRepository.GetPaymentNotesAccountPayableAdvanceByAccountPayableListId(listAccountPayableId)
            Dim response As New List(Of String)()
            If resultNote IsNot Nothing AndAlso resultNote.Count > 0 Then
                For Each item In resultNote
                    response.Add(String.Format("La factura {0} esta contenida en la nota {1} la cual se encuentra sin confirmar", item.Split(";")(0), item.Split(";")(1)))
                Next
            End If
            Dim resultTransfer = _paymentTransferRepository.GetPaymentTransferByAccountPayableListId(listAccountPayableId)
            If resultTransfer IsNot Nothing AndAlso resultTransfer.Count > 0 Then
                For Each item In resultTransfer
                    response.Add(String.Format("La factura {0} esta contenida en el anticipo {1} el cual se encuentra sin confirmar", item.Split(";")(0), item.Split(";")(1)))
                Next
            End If
            Dim resultCrossing = _crossingAccountRepository.GetCrossingAccountByAccountPayableListId(listAccountPayableId)
            If resultCrossing IsNot Nothing AndAlso resultCrossing.Count > 0 Then
                For Each item In resultCrossing
                    response.Add(String.Format("La factura {0} esta contenida en el cruce de cuentas {1} el cual se encuentra sin confirmar", item.Split(";")(0), item.Split(";")(1)))
                Next
            End If
            Dim resultVoucherTransaction = _voucherRepository.GetVoucherTransactionByAccountPayableListId(listAccountPayableId)
            If resultVoucherTransaction IsNot Nothing AndAlso resultVoucherTransaction.Count > 0 Then
                For Each item In resultVoucherTransaction
                    If Not item.Split(";")(1).Equals(voucherTransactionCode) Then
                        response.Add(String.Format("La factura {0} esta contenida en el Comprobante de Egreso {1} el cual se encuentra sin confirmar", item.Split(";")(0), item.Split(";")(1)))
                    End If
                Next
            End If

            Return New ActionResult(Of List(Of String)) With {.StateResult = True, .ObjectEmbbeded = response}
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of List(Of String)) With {.StateResult = False}
        End Try
    End Function

    ''' <summary>
    ''' Obtiene un comprobante de egreso por id
    ''' </summary>
    ''' <param name="Id">The identifier.</param>
    ''' <returns></returns>
    ''' <exception cref="System.ArgumentNullException">Id</exception>
    Public Function GetVoucherTransactionById(Id As Integer) As ActionResult(Of VoucherTransaction) Implements IVoucherTransactionAdminService.GetVoucherTransactionById
        If Id = 0 Then
            Throw New ArgumentNullException("Id")
        End If
        Try
            Dim voucherTransaction As VoucherTransaction = Me._voucherRepository.GetVoucherTransactionById(Id)
            Return New ActionResult(Of VoucherTransaction) With {.StateResult = True, .ObjectEmbbeded = voucherTransaction}
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of VoucherTransaction) With {.StateResult = False}
        End Try
    End Function

    ''' <summary>
    ''' Guarda un comprobante de egreso
    ''' </summary>
    Public Function SaveVoucherTransaction(voucherTransaction As VoucherTransaction, audit As AuditMessage, withConfirm As Boolean, Optional idSequence As Long = 0, Optional ByVal sequenceC As TreasurySequence = Nothing, Optional ByVal returnWithTracking As Boolean = True) As ActionResult(Of VoucherTransaction) Implements IVoucherTransactionAdminService.SaveVoucherTransaction
        If voucherTransaction Is Nothing Then
            Throw New ArgumentNullException("voucherTransaction")
        End If

        'Se valida que cuando el comprobante sea de clase traslado y tipo caja o cuenta bancaria, se valide que la caja o la cuenta bancaria no exista en los detalles
        If voucherTransaction.VoucherClass = 3 AndAlso (voucherTransaction.ExpenseType = 3 OrElse voucherTransaction.ExpenseType = 1) AndAlso voucherTransaction.VoucherTransactionDetails IsNot Nothing AndAlso voucherTransaction.VoucherTransactionDetails.Count > 0 Then
            If voucherTransaction.ExpenseType = 3 Then 'Si el tipo de comprobante es caja
                If (From x In voucherTransaction.VoucherTransactionDetails Where x.CashRegisterId = voucherTransaction.IdCashRegister AndAlso x.ChangeTracker.State <> ObjectState.Deleted Select x).Count > 0 Then 'Se valida que la caja no sea igual de los detalles al de la cabecera
                    Return New ActionResult(Of VoucherTransaction) With {.StateResult = False, .StatusCode = eStatusResult.WARNING, .Message = "Existen cajas en el detalle que son iguales a la caja de la cabecera"}
                End If
            ElseIf voucherTransaction.ExpenseType = 1 Then 'Si el tipo de comprobante es cuenta bancaria
                If (From x In voucherTransaction.VoucherTransactionDetails Where x.IdEntityBankAccount = voucherTransaction.IdEntityBankAccount AndAlso x.ChangeTracker.State <> ObjectState.Deleted Select x).Count > 0 Then 'Se valida que la cuenta bancaria no sea igual de los detalles al de la cabecera
                    Return New ActionResult(Of VoucherTransaction) With {.StateResult = False, .StatusCode = eStatusResult.WARNING, .Message = "Existen cuentas bancarias en el detalle que son iguales a la cuenta bancaria de la cabecera"}
                End If
                If (From x In voucherTransaction.VoucherTransactionDetails Where x.IdMainAccount = voucherTransaction.IdMainAccount AndAlso x.ChangeTracker.State <> ObjectState.Deleted Select x).Count > 0 Then 'Se valida que la cuenta contable de la cabecera no este en los detalles
                    Return New ActionResult(Of VoucherTransaction) With {.StateResult = False, .StatusCode = eStatusResult.WARNING, .Message = "Existen cuentas contables en el detalle que son iguales a la cuenta contable de la cabecera"}
                End If
            End If
        End If

        If withConfirm Then
            voucherTransaction.Status = 2
            voucherTransaction.ModificationDate = Date.Now
            voucherTransaction.ModificationUser = audit.CodeUser
            voucherTransaction.ConfirmationDate = Date.Now
            voucherTransaction.ConfirmationUser = audit.CodeUser
        End If

        Dim txSettings As New TransactionOptions()
        txSettings.Timeout = TransactionManager.MaximumTimeout
        txSettings.IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted
        Using transaction As New TransactionScope(TransactionScopeOption.Required, txSettings)
            Try
                ''Validacion para actualizar iva descontable en caso de que el parámetro haya sido modificado
                Dim message As String = String.Empty
                Dim _companySettings = _companySettingsRepository.GetCompanySettings(True)
                If _companySettings IsNot Nothing Then
                    Dim taxValidation As Boolean
                    Dim parameter As Boolean = False
                    Select Case _companySettings.TaxRegistration
                        Case 1, 4
                            parameter = False
                            taxValidation = (From l In voucherTransaction.VoucherTransactionDetails
                                             Where (l.discountableIVA.HasValue AndAlso l.discountableIVA)).Any()
                        Case 2
                            parameter = True
                            taxValidation = (From l In voucherTransaction.VoucherTransactionDetails
                                             Where (l.discountableIVA.HasValue AndAlso Not l.discountableIVA)).Any()
                    End Select
                    If taxValidation Then
                        message += "Debido a una diferencia en el parámetro de Registro IVA, el iva descontable ha sido modificado"
                        Parallel.ForEach(voucherTransaction.VoucherTransactionDetails.ToList().FindAll(Function(s) s.ChangeTracker.State <> ObjectState.Deleted), Sub(x)
                                                                                                                                                                      x.discountableIVA = parameter
                                                                                                                                                                  End Sub)
                    End If
                End If
                Dim xml = voucherTransaction.ToXML()
                'Return New ActionResult(Of VoucherTransaction)
                Dim result = _voucherRepository.GenerateVoucherTransactionSP(xml, audit.CodeUser).ToList().ElementAt(0)
                If result.CodeMessage = "999" Then
                    _voucherRepository.UnitWork.RollbackChangesUnitOfWork()
                    message += result.Message
                    transaction.Dispose()
                    Return New ActionResult(Of VoucherTransaction) With {.StateResult = False, .StatusCode = eStatusResult.WARNING, .Message = message}
                End If

                Dim ResultGEDS As ActionResult(Of List(Of Tuple(Of String, String))) = Nothing
                Dim voucher = _voucherRepository.GetVoucherTransactionById(result.IdVoucherTransaction, returnWithTracking)

                If voucher.HandlesDocumentSupport AndAlso voucher.Status = 2 AndAlso voucher.VoucherClass = 1 Then
                    'genero y confirmo el documento soporte electronico
                    voucher.IndigoCompanyNit = voucherTransaction.IndigoCompanyNit
                    ResultGEDS = GenerateElectronicSupportDocument({voucher}.ToList(), audit, voucherTransaction.AuthorizationResolutionId)
                    If ResultGEDS.StateResult = False Then
                        _voucherRepository.UnitWork.RollbackChangesUnitOfWork()
                        transaction.Dispose()
                        Return New ActionResult(Of VoucherTransaction) With {.StateResult = False, .StatusCode = eStatusResult.WARNING, .Message = String.Join(" ; ", ResultGEDS.ObjectEmbbeded.Select(Function(x) x.Item2).ToList())}
                    End If
                    result.Message = $"{result.Message} Documento Soporte Electronico : {String.Join(" ; ", ResultGEDS.ObjectEmbbeded.Select(Function(x) x.Item2).ToList())}"

                    Dim resultPublish = PublishElectronicSupporDocumentMessages({voucher}.ToList(), audit)
                    If resultPublish.StateResult = False Then
                        _voucherRepository.UnitWork.RollbackChangesUnitOfWork()
                        transaction.Dispose()

                        Return New ActionResult(Of VoucherTransaction) With {.StateResult = False, .StatusCode = eStatusResult.WARNING, .Message = resultPublish.Message}
                    End If
                End If

                _voucherRepository.UnitWork.RollbackChangesUnitOfWork()
                transaction.Complete()
                Return New ActionResult(Of VoucherTransaction) With {.StateResult = True, .StatusCode = eStatusResult.SUCCESS, .Message = result.Message, .ObjectEmbbeded = voucher}
            Catch ex As OptimisticConcurrencyException
                transaction.Dispose()
                Return New ActionResult(Of VoucherTransaction) With {.StateResult = False, .StatusCode = eStatusResult.EXCEPTION, .Message = ResourceManager.GetString("ErrorConcurrence")}
            Catch ex As InvalidOperationException
                transaction.Dispose()
                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
                Return New ActionResult(Of VoucherTransaction) With {.StateResult = False, .StatusCode = eStatusResult.EXCEPTION, .Message = ResourceManager.GetString("ErrorUnknown")}
            Catch ex As Exception
                transaction.Dispose()
                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
                Return New ActionResult(Of VoucherTransaction) With {.StateResult = False, .StatusCode = eStatusResult.EXCEPTION, .Message = IndigoManagementExceptions.GetExceptionDetails(ex)}
            End Try
        End Using
    End Function


    ''' <summary>
    ''' Validates the check.
    ''' </summary>
    ''' <returns></returns>
    Public Function ValidateCheck(ByVal checkBookId As Integer, ByVal checkNumber As Long, ByVal operativeUnitId As Integer) As ActionResult
        Dim _AnnulatedCheck As CancellationChecks = _cancellationCheckRepository.GetCancellationCheckByCheckBookIdAndCheckNumber(checkBookId, checkNumber)
        If _AnnulatedCheck IsNot Nothing AndAlso _AnnulatedCheck.Id > 0 Then
            Return New ActionResult With {.StateResult = False, .MessageResult = {"00"}.ToList(), .Message = String.Format(ResourceManager.GetString("CheckAnnulated", TREASURY), checkNumber)}
        End If
        Dim _settings As SettingsTreasury = _settingsTreasuryRepository.GetSettingsTreasuryByIdUnitOperative(operativeUnitId)
        Dim _checkBlock As CheckBlock = _checkBlockRepository.GetCheckBlockByIdCheckBookAndNumber(checkBookId, checkNumber)
        If (_checkBlock IsNot Nothing AndAlso _checkBlock.Id > 0) AndAlso Not _settings.CheckBookControl Then
            Return New ActionResult With {.StateResult = False, .MessageResult = {"01"}.ToList(), .Message = String.Format("El cheque {0} se encuentra bloqueado", checkNumber)}
        End If
        Return New ActionResult With {.StateResult = True}
    End Function

    ''' <summary>
    ''' Confirma un comprobante de egreso
    ''' </summary>
    Public Function ConfirmVoucherTransaction(IdvoucherTransaction As Integer, audit As AuditMessage, Optional voucherTransaction As VoucherTransaction = Nothing, Optional idSequence As Long = 0) As ActionResult(Of String) Implements IVoucherTransactionAdminService.ConfirmVoucherTransaction
        If IdvoucherTransaction = 0 Then
            Throw New ArgumentNullException("voucherTransaction")
        End If
        Try
            Dim unitOfWork As IUnitWork = Me._voucherRepository.UnitWork
            If voucherTransaction Is Nothing Then
                voucherTransaction = _voucherRepository.GetVoucherTransactionById(IdvoucherTransaction)
            End If
            voucherTransaction.StartTracking()
            Dim resultValidateObject = treasuryService.ValidateVoucherTransaction(voucherTransaction)
            If Not resultValidateObject.StateResult Then
                Return New ActionResult(Of String) With {.StateResult = False, .Message = resultValidateObject.Message}
            End If
            Dim treasuryControl As TreasuryControl = _treasuryControlAdminService.GetTreasuryControlByDocumentNumber(voucherTransaction.Code, 2)
            Using scope As New TransactionScope(TransactionScopeOption.Required, New TransactionOptions() With {.Timeout = TransactionManager.MaximumTimeout, .IsolationLevel = IsolationLevel.ReadCommitted})
                If treasuryControl IsNot Nothing AndAlso treasuryControl.Id > 0 Then
                    treasuryControl.MarkAsDeleted()
                    Dim resultSaveControl = _treasuryControlAdminService.DeleteTreasuryControl(treasuryControl, audit, False)
                    If resultSaveControl.StateResult = False Then
                        Return New ActionResult(Of String) With {.StateResult = False, .Message = ResourceManager.GetString("DeleteTreasuryControlError", TREASURY)}
                    End If
                End If
                Dim _treasuryBalance As New TreasuryBalance()
                Dim _movementValue As Decimal = IIf(voucherTransaction.TaxByMil, voucherTransaction.Value + voucherTransaction.TaxByMilValue, voucherTransaction.Value)
                Dim FMGCounterpartMainAccountId As Integer = 0
                Dim FMGExpenseMainAccountId As Integer = 0
                Select Case voucherTransaction.ExpenseType
                    Case 1 'Cuenta Bancaria
                        'Egreso de cuenta bancaria
                        Dim _entityAccount As EntityBankAccounts = _entityBankAccountRepository.GetEntityBankAccountById(voucherTransaction.IdEntityBankAccount)
                        If voucherTransaction.ExpenseType = 1 AndAlso voucherTransaction.PaymentMethod = 1 AndAlso voucherTransaction.IdChecks IsNot Nothing Then
                            Dim checkBlock As CheckBlock = _checkBlockRepository.GetCheckBlockByIdCheckBookAndNumber(voucherTransaction.IdChecks, voucherTransaction.CheckNumber)
                            If checkBlock IsNot Nothing AndAlso checkBlock.Id > 0 Then
                                _checkBlockRepository.DeleteEntity(checkBlock)
                            End If
                            Dim checkbook As Checkbooks = _checkBookRepository.GetCheckByIdEntityBankAccountAndStatus(voucherTransaction.IdEntityBankAccount, 1)
                            If checkbook IsNot Nothing AndAlso checkbook.Id > 0 Then
                                Dim checkBlocks As List(Of CheckBlock) = _checkBlockRepository.ListCheckByCheckbookId(voucherTransaction.IdChecks).Where(Function(o) o.Id <> checkBlock.Id).ToList()
                                Dim pendingChecks As List(Of OutstandingChecks) = _outstandingCheckRepository.ListOutstandingChecksByIdCheckBook(voucherTransaction.IdChecks)
                                If checkbook.CurrentNumber = checkbook.EndNumber AndAlso (checkBlocks Is Nothing OrElse Not checkBlocks.Any()) AndAlso (pendingChecks Is Nothing OrElse Not pendingChecks.Any()) Then
                                    checkbook.Status = 3
                                    _checkBookRepository.SaveEntity(checkbook)
                                End If
                            End If
                        End If

                        FMGCounterpartMainAccountId = _entityAccount.FMGCounterpartMainAccountId
                        FMGExpenseMainAccountId = _entityAccount.FMGExpenseMainAccountId
                        With _entityAccount
                            With _treasuryBalance
                                .DocumentNumber = voucherTransaction.Code
                                .DocumentDate = voucherTransaction.DocumentDate
                                .DocumentType = 2 'Comprobante de egreso
                                .Nature = 2 'Credito
                                .PreviousBalance = _entityAccount.CurrentBalance
                                .ValueMovement = _movementValue
                                .CreationDate = Date.Now
                            End With
                            _entityAccount.CurrentBalance -= _movementValue
                            .TreasuryBalance.Add(_treasuryBalance)
                        End With
                        Dim resultEntityAccount = _entityAccountAdminService.SaveEntityBankAccount(_entityAccount, audit, 0, False)
                        If resultEntityAccount.StateResult = False Then
                            unitOfWork.RollbackChangesUnitOfWork()
                            scope.Dispose()
                            Return New ActionResult(Of String) With {.StateResult = False, .Message = ResourceManager.GetString("BalanceUpdateErrorEntityAccount", TREASURY)}
                        End If
                    Case 2, 3 'Caja Menor, Caja Mayor
                        'Egreso de caja
                        Dim _cashRegister As CashRegisters = _cashRegisterRepository.GetCashRegisterById(voucherTransaction.IdCashRegister)
                        With _cashRegister
                            With _treasuryBalance
                                .DocumentNumber = voucherTransaction.Code
                                .DocumentDate = voucherTransaction.DocumentDate
                                .DocumentType = 2 'Comprobante de egreso
                                .Nature = 2 'Credito
                                .PreviousBalance = _cashRegister.CurrentBalance
                                .ValueMovement = _movementValue
                                .CreationDate = DateTime.Now
                            End With
                            .CurrentBalance -= _movementValue  'Tambien avisar que si la caja esta por debajo del monto minimo entonces necesita reembolsar
                            .IsMovement = True
                            .TreasuryBalance.Add(_treasuryBalance)
                        End With
                        Dim resultCash = _cashAdminService.SaveCashRegister(_cashRegister, audit, idSequence)
                        If resultCash.StateResult = False Then
                            unitOfWork.RollbackChangesUnitOfWork()
                            scope.Dispose()
                            Return New ActionResult(Of String) With {.StateResult = False, .Message = String.Concat(ResourceManager.GetString("BalanceUpdateErrorCashRegister", TREASURY), _cashRegister.Code)}
                        End If
                End Select

                Dim consecutive As String = String.Empty
                For Each vouDetail As VoucherTransactionDetails In voucherTransaction.VoucherTransactionDetails
                    Dim result As ActionResult(Of String) = Nothing
                    Select Case voucherTransaction.VoucherClass
                        Case 1 'Pagos
                            Dim expenseConcept As ExpenseConcepts = _expenseConceptRepository.GetExpenseConceptById(vouDetail.IdExpenseConcept)
                            If expenseConcept IsNot Nothing AndAlso expenseConcept.Id > 0 Then
                                'Comportamiento: 1 - Traslado entre bancos; 2 - Caja Menor; 3 - Pago/Anticipo de Facturas CxP; 4 - Devolutivos de Anticipos RC; 5 - Reembolso de Caja Menor; 6 - Ninguno
                                Select Case expenseConcept.Behavior
                                    'Case 1
                                    '    result = BehaviorTransferBetweenBanks(voucherTransaction, vouDetail, audit)
                                    Case 2
                                        result = BehaviorCashMinor(voucherTransaction, vouDetail, audit, idSequence)
                                    Case 3
                                        result = BehaviorPaymentAdvancePaymentInvoices(voucherTransaction, vouDetail, audit)
                                    Case 4
                                        result = BehaviorReturningImprestRC(voucherTransaction, vouDetail, audit)
                                        'Case 5
                                        '    result = BehaviorRefundCashMinor(voucherTransaction, vouDetail, audit, idSequence)
                                    Case 6
                                        result = New ActionResult(Of String) With {.StateResult = True}
                                End Select
                            End If
                        Case 2 'Reembolsos
                            result = RefundCashMinor(voucherTransaction, vouDetail, audit, idSequence)
                        Case 3 'Traslados
                            result = Transfers(voucherTransaction, vouDetail, audit)
                    End Select
                    If result.StateResult = False Then
                        unitOfWork.RollbackChangesUnitOfWork()
                        scope.Dispose()
                        Return New ActionResult(Of String) With {.StateResult = False, .Message = result.Message}
                    End If
                Next
                voucherTransaction.Status = 2


                'Comprobante contable
                Dim settingTreasury As SettingsTreasury = _settingsTreasuryRepository.GetSettingsTreasuryByIdUnitOperative(voucherTransaction.IdUnitOperative)
                If settingTreasury.Id > 0 Then
                    Dim generateJournalVoucher As ActionResult(Of JournalVouchers) = Generate(settingTreasury.JournalVoucherTypeVoucherTransaction, FMGCounterpartMainAccountId, FMGExpenseMainAccountId, voucherTransaction, False) 'settingTreasury.FMGMainAccountExpenses, voucherTransaction, False)
                    If Not generateJournalVoucher.StateResult Then
                        'unitOfWork.RollbackChangesUnitOfWork()
                        scope.Dispose()
                        Return New ActionResult(Of String) With {.StateResult = False, .Message = "Error al Generar el comprobante contable"}
                    End If
                    Dim accounting As JournalVouchers = generateJournalVoucher.ObjectEmbbeded
                    Dim resultAccounting As ActionMessageResult(Of Domain.Entities.JournalVouchers)
                    resultAccounting = _accountingAdminService.SaveAccountingDocument(accounting, audit, False)
                    If resultAccounting.StateResult = False Then
                        'unitOfWork.RollbackChangesUnitOfWork()
                        scope.Dispose()
                        Return New ActionResult(Of String) With {.StateResult = False, .Message = resultAccounting.Message}
                    Else
                        consecutive = resultAccounting.ObjectEmbbeded.Consecutive
                    End If
                Else
                    unitOfWork.RollbackChangesUnitOfWork()
                    scope.Dispose()
                    Return New ActionResult(Of String) With {.StateResult = False, .MessageResult = {"CE0006"}.ToList(), .Message = ResourceManager.GetString("SettingTreasuryNotExist", TREASURY)}
                End If
                voucherTransaction.ModificationDate = Date.Now
                voucherTransaction.ModificationUser = audit.CodeUser
                voucherTransaction.ConfirmationDate = Date.Now
                voucherTransaction.ConfirmationUser = audit.CodeUser

                Me._voucherRepository.SaveEntity(voucherTransaction)
                unitOfWork.Commit()

                Dim status As Integer = Infrastructure.CrossCutting.Audit.Actions.Confirm
                Dim auditProcess As New IndigoAuditSimpleEntity(Of VoucherTransaction)(voucherTransaction, audit, status, voucherTransaction.OriginalValue)
                auditProcess.Execute()
                scope.Complete()
                Dim messageResult As New List(Of String)
                messageResult.Add(settingTreasury.JournalVoucherTypeVoucherTransaction.ToString())
                Return New ActionResult(Of String) With {.StateResult = True, .ObjectEmbbeded = consecutive, .MessageResult = messageResult}
            End Using

        Catch ex As AccessViolationException
            Return New ActionResult(Of String) With {.StateResult = False, .Message = ex.Message}
        Catch ex As OptimisticConcurrencyException
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of String) With {.StateResult = False, .MessageResult = {"-999"}.ToList()}
        Catch ex As DbEntityValidationException
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of String) With {.StateResult = False, .Message = ResourceManager.GetString("ErrorUnknown")}
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of String) With {.StateResult = False, .Message = ResourceManager.GetString("ErrorUnknown")}
        End Try

    End Function

    ''' <summary>
    ''' Hace reversión del comprobante de egreso
    ''' </summary>
    Public Function DisconfirmVoucherTransaction(treasuryNote As TreasuryNote, audit As AuditMessage) As ActionResult(Of String) Implements IVoucherTransactionAdminService.DisconfirmVoucherTransaction
        Dim txSettings As New TransactionOptions()
        txSettings.Timeout = TransactionManager.MaximumTimeout
        txSettings.IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted
        Using transaction As New TransactionScope(TransactionScopeOption.Required, txSettings)
            Try
                CType(_voucherRepository.UnitWork, IObjectContextAdapter).ObjectContext.CommandTimeout = 3600
                Dim result = _voucherRepository.SP_ReverseVoucherTransaction(treasuryNote.Id, audit.CodeUser).ToList().ElementAt(0)
                If result.CodeMessage = 999 Then
                    Return New ActionResult(Of String) With {.StateResult = False, .Message = result.Message}
                Else
                    transaction.Complete()
                    Return New ActionResult(Of String) With {.StateResult = True, .ObjectEmbbeded = result.IdJournalVoucher, .Message = result.Message}
                End If
            Catch ex As OptimisticConcurrencyException
                transaction.Dispose()
                Return New ActionResult(Of String) With {.StateResult = False, .Message = ResourceManager.GetString("ErrorConcurrence")}
            Catch ex As DbEntityValidationException
                transaction.Dispose()
                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
                Return New ActionResult(Of String) With {.StateResult = False, .Message = ResourceManager.GetString("ErrorUnknown")}
            Catch ex As Exception
                transaction.Dispose()
                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
                Return New ActionResult(Of String) With {.StateResult = False, .Message = ResourceManager.GetString("ErrorUnknown")}
            End Try
        End Using
    End Function

    ''' <summary>
    ''' Generates the specified journal voucher type identifier.
    ''' </summary>
    ''' <param name="JournalVoucherTypeId">The journal voucher type identifier.</param>
    ''' <param name="FMGCounterpartMainAccountId">The FMG counterpart main account identifier.</param>
    ''' <param name="FMGExpenseMainAccountId">The FMG expense main account identifier.</param>
    ''' <param name="voucher">The voucher.</param>
    ''' <param name="disconfirm">if set to <c>true</c> [disconfirm].</param>
    ''' <param name="treasuryNote">The treasury note.</param>
    ''' <returns></returns>
    Public Function Generate(ByVal JournalVoucherTypeId As Integer, ByVal FMGCounterpartMainAccountId As Integer, FMGExpenseMainAccountId As Integer, ByVal voucher As VoucherTransaction, ByVal disconfirm As Boolean, Optional ByVal treasuryNote As TreasuryNote = Nothing) As ActionResult(Of JournalVouchers)
        Try
            Dim mainAccounts As MainAccounts = Nothing
            Dim accounting As New Domain.Entities.JournalVouchers()
            Dim cashregister As CashRegisters = Nothing
            Dim entityAccount As EntityBankAccounts = Nothing
            Dim dictionaryMainAccount As New Dictionary(Of Integer, MainAccounts)


            With accounting
                .IdJournalVoucher = JournalVoucherTypeId
                .Status = 2
                If disconfirm Then
                    .VoucherDate = treasuryNote.NoteDate
                    .Detail = treasuryNote.Description
                    .EntityCode = treasuryNote.Code
                    .EntityId = treasuryNote.Id
                    .EntityName = GetType(TreasuryNote).Name
                Else
                    .VoucherDate = voucher.DocumentDate
                    .Detail = voucher.Detail
                    .EntityCode = voucher.Code
                    .EntityId = voucher.Id
                    .EntityName = GetType(VoucherTransaction).Name
                End If
                .IsClosedYear = False
                For Each detail As VoucherTransactionDetails In voucher.VoucherTransactionDetails
                    Dim accountDetail As New JournalVoucherDetails
                    With accountDetail
                        .IdMainAccount = detail.IdMainAccount

                        'se busca en el diccionario para no consultar
                        If dictionaryMainAccount.ContainsKey(.IdMainAccount) Then
                            mainAccounts = dictionaryMainAccount(.IdMainAccount)
                        Else
                            mainAccounts = _pucRepository.GetAccountById(.IdMainAccount, False)
                            dictionaryMainAccount.Add(.IdMainAccount, mainAccounts)
                        End If



                        If voucher.VoucherClass = eVoucherClass.Refund Then
                            cashregister = _cashRegisterRepository.GetCashRegisterById(detail.CashRegisterId)
                        ElseIf voucher.VoucherClass = eVoucherClass.Transfer Then
                            If voucher.ExpenseType = eExpenseType.BankAccount Then
                                entityAccount = _entityBankAccountRepository.GetEntityBankAccountById(detail.IdEntityBankAccount)
                            Else
                                cashregister = _cashRegisterRepository.GetCashRegisterById(detail.CashRegisterId)
                            End If
                        End If
                        If mainAccounts.HandlesThirdParty Then
                            If voucher.VoucherClass = eVoucherClass.Refund Then
                                .IdThirdParty = cashregister.ThirdPartyId
                            ElseIf voucher.VoucherClass = eVoucherClass.Payment Then
                                .IdThirdParty = detail.IdThirdParty 'voucher.IdThirdParty
                            ElseIf voucher.ExpenseType = eExpenseType.BankAccount Then
                                .IdThirdParty = entityAccount.ThirdPartyId.Value
                            Else
                                .IdThirdParty = cashregister.ThirdPartyId
                            End If
                        Else
                            .IdThirdParty = Nothing
                        End If
                        '.IdThirdParty = IIf(mainAccounts.HandlesThirdParty, IIf(voucher.VoucherClass = eVoucherClass.Refund, cashregister.ThirdPartyId, _
                        '                                                        IIf(voucher.VoucherClass = eVoucherClass.Payment, detail.IdThirdParty, _
                        '                                                            IIf(voucher.ExpenseType = eExpenseType.BankAccount, entityAccount.ThirdPartyId.Value, cashregister.ThirdPartyId.Value))), Nothing)
                        .IdCostCenter = IIf(mainAccounts.HandlesCostCenter, detail.IdCostCenter, Nothing)

                        .CreditValue = IIf(disconfirm, IIf(detail.Nature = 1, detail.Value, 0), IIf(detail.Nature = 1, 0, detail.Value))
                        .DebitValue = IIf(disconfirm, IIf(detail.Nature = 1, 0, detail.Value), IIf(detail.Nature = 1, detail.Value, 0))
                        .Detail = detail.Observation
                        .IdRetention = detail.IdRetentionConcept
                        .RetentionRate = detail.PercentRetention
                        If .IdRetention IsNot Nothing Then
                            .BaseValue = detail.BaseValue
                            .BillingValue = detail.BillingValue
                        End If
                    End With
                    accounting.JournalVoucherDetails.Add(accountDetail)
                Next
                If voucher.Value <> 0 Then

                    Dim settingTreasury = _settingsTreasuryRepository.GetSettingsTreasuryByIdUnitOperative(voucher.IdUnitOperative)
                    If settingTreasury.Id = 0 Then
                        Return New ActionResult(Of JournalVouchers) With {.StateResult = False, .Message = ResourceManager.GetString("SettingTreasuryNotExist", "Treasury")}
                    End If


                    Dim accountDetail As New JournalVoucherDetails
                    With accountDetail
                        .IdMainAccount = voucher.IdMainAccount
                        'se busca en el diccionario para no consultar
                        If dictionaryMainAccount.ContainsKey(.IdMainAccount) Then
                            mainAccounts = dictionaryMainAccount(.IdMainAccount)
                        Else
                            mainAccounts = _pucRepository.GetAccountById(.IdMainAccount, False)
                            dictionaryMainAccount.Add(.IdMainAccount, mainAccounts)
                        End If

                        If voucher.ExpenseType = eExpenseType.BankAccount Then
                            entityAccount = _entityBankAccountRepository.GetEntityBankAccountById(voucher.IdEntityBankAccount)
                        Else
                            cashregister = _cashRegisterRepository.GetCashRegisterById(voucher.IdCashRegister)
                        End If

                        If mainAccounts.HandlesThirdParty Then
                            If voucher.VoucherClass = eVoucherClass.Payment Then
                                If voucher.ExpenseType = eExpenseType.BankAccount Then
                                    If settingTreasury.GetThirdPartyBank = 1 Then
                                        .IdThirdParty = entityAccount.ThirdPartyId
                                    Else
                                        .IdThirdParty = voucher.IdThirdParty
                                    End If

                                Else
                                    If settingTreasury.GetThirdPartyCashRegister = 1 Then
                                        .IdThirdParty = cashregister.ThirdPartyId
                                    Else
                                        .IdThirdParty = voucher.IdThirdParty
                                    End If

                                End If
                            Else
                                If voucher.ExpenseType = eExpenseType.BankAccount Then
                                    .IdThirdParty = entityAccount.ThirdPartyId
                                Else
                                    .IdThirdParty = cashregister.ThirdPartyId
                                End If
                            End If

                        Else
                            .IdThirdParty = Nothing
                        End If
                        '.IdThirdParty = IIf(mainAccounts.HandlesThirdParty, IIf(voucher.VoucherClass = eVoucherClass.Refund OrElse voucher.VoucherClass = eVoucherClass.Transfer, _
                        '                                                        IIf(voucher.ExpenseType = eExpenseType.BankAccount, entityAccount.ThirdPartyId, cashregister.ThirdPartyId), voucher.IdThirdParty), Nothing)
                        .IdCostCenter = IIf(mainAccounts.HandlesCostCenter, voucher.IdCostCenter, Nothing)

                        .DebitValue = IIf(disconfirm, voucher.Value + voucher.TaxByMilValue, 0)
                        .CreditValue = IIf(disconfirm, 0, voucher.Value + voucher.TaxByMilValue)
                        Dim detailVoucher As String = "Comprobante de Egreso : " & voucher.Code
                        If voucher.PaymentMethod IsNot Nothing AndAlso voucher.PaymentMethod = 1 Then
                            detailVoucher &= ", Cheque # " & voucher.CheckNumber
                        ElseIf voucher.PaymentMethod IsNot Nothing AndAlso voucher.PaymentMethod = 2 Then
                            detailVoucher &= ", Nota debito # " & voucher.NoteNumber
                        End If
                        If Not String.IsNullOrEmpty(voucher.Detail) Then
                            detailVoucher &= ", " & voucher.Detail
                        End If
                        .Detail = detailVoucher
                        .IdRetention = Nothing
                    End With
                    accounting.JournalVoucherDetails.Add(accountDetail)
                    'si tiene activado la tasa por mil
                    If voucher.TaxByMil Then

                        accountDetail = New JournalVoucherDetails
                        With accountDetail
                            .IdMainAccount = FMGCounterpartMainAccountId
                            'se busca en el diccionario para no consultar
                            If dictionaryMainAccount.ContainsKey(.IdMainAccount) Then
                                mainAccounts = dictionaryMainAccount(.IdMainAccount)
                            Else
                                mainAccounts = _pucRepository.GetAccountById(.IdMainAccount, False)
                                dictionaryMainAccount.Add(.IdMainAccount, mainAccounts)
                            End If

                            If mainAccounts.HandlesThirdParty Then
                                .IdThirdParty = entityAccount.FMGCounterpartThirdPartyId
                            Else
                                .IdThirdParty = Nothing
                            End If
                            If mainAccounts.HandlesCostCenter Then
                                .IdCostCenter = entityAccount.FMGCounterpartCostCenterId
                            Else
                                .IdCostCenter = Nothing
                            End If
                            .DebitValue = IIf(disconfirm, voucher.TaxByMilValue, 0)
                            .CreditValue = IIf(disconfirm, 0, voucher.TaxByMilValue)
                            .Detail = voucher.Detail
                            .IdRetention = Nothing
                        End With
                        accounting.JournalVoucherDetails.Add(accountDetail)

                        accountDetail = New JournalVoucherDetails
                        With accountDetail
                            .IdMainAccount = FMGExpenseMainAccountId
                            'se busca en el diccionario para no consultar
                            If dictionaryMainAccount.ContainsKey(.IdMainAccount) Then
                                mainAccounts = dictionaryMainAccount(.IdMainAccount)
                            Else
                                mainAccounts = _pucRepository.GetAccountById(.IdMainAccount, False)
                                dictionaryMainAccount.Add(.IdMainAccount, mainAccounts)
                            End If

                            If mainAccounts.HandlesThirdParty Then
                                .IdThirdParty = entityAccount.FMGExpenseThirdPartyId
                            Else
                                .IdThirdParty = Nothing
                            End If
                            If mainAccounts.HandlesCostCenter Then
                                .IdCostCenter = entityAccount.FMGExpenseCostCenterId
                            Else
                                .IdCostCenter = Nothing
                            End If
                            .DebitValue = IIf(disconfirm, 0, voucher.TaxByMilValue)
                            .CreditValue = IIf(disconfirm, voucher.TaxByMilValue, 0)
                            .Detail = voucher.Detail
                            .IdRetention = Nothing
                        End With
                        accounting.JournalVoucherDetails.Add(accountDetail)
                    End If
                End If
            End With
            Return New ActionResult(Of JournalVouchers) With {.StateResult = True, .ObjectEmbbeded = accounting}
        Catch ex As Exception
            Return New ActionResult(Of JournalVouchers) With {.StateResult = False, .Message = ex.Message.ToString()}
        End Try
    End Function

    Public Enum eVoucherClass
        Payment = 1
        Refund = 2
        Transfer = 3
    End Enum

    ''' <summary>
    ''' Obtiene un avance de tesoreria por Id
    ''' </summary>
    ''' <param name="Id">The identifier.</param>
    ''' <returns></returns>
    ''' <exception cref="System.ArgumentNullException">Id</exception>
    Public Function GetTreasuryAdvanceById(Id As Integer) As ActionResult(Of TreasuryAdvances) Implements IVoucherTransactionAdminService.GetTreasuryAdvanceById
        If Id = 0 Then
            Throw New ArgumentNullException("Id")
        End If
        Try
            Dim treasuryAdvances As TreasuryAdvances = Me._treasuryAdvanceRepository.GetTreasuryAdvanceById(Id)
            Return New ActionResult(Of TreasuryAdvances) With {.StateResult = True, .ObjectEmbbeded = treasuryAdvances}
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of TreasuryAdvances) With {.StateResult = False}
        End Try
    End Function

    ''' <summary>
    ''' Obtiene un avance de tesoreria por IdVoucherTransactionDetail
    ''' </summary>
    ''' <param name="IdVoucherTransactionDetail">The identifier voucher transaction detail.</param>
    ''' <returns></returns>
    ''' <exception cref="System.ArgumentNullException">IdVoucherTransactionDetail</exception>
    Public Function GetTreasuryAdvanceByIdVoucherTransactionDetail(IdVoucherTransactionDetail As Integer) As ActionResult(Of TreasuryAdvances) Implements IVoucherTransactionAdminService.GetTreasuryAdvanceByIdVoucherTransactionDetail
        If IdVoucherTransactionDetail = 0 Then
            Throw New ArgumentNullException("IdVoucherTransactionDetail")
        End If
        Try
            Dim treasuryAdvances As TreasuryAdvances = Me._treasuryAdvanceRepository.GetTreasuryAdvanceByIdVoucherTransactionDetail(IdVoucherTransactionDetail)
            Return New ActionResult(Of TreasuryAdvances) With {.StateResult = True, .ObjectEmbbeded = treasuryAdvances}
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of TreasuryAdvances) With {.StateResult = False, .Message = ResourceManager.GetString("ErrorUnknown")}
        End Try
    End Function

    ''' <summary>
    ''' Obtiene un listado de comprobantes de egreso que esten dentro de un rango de fecha, tipo de egreso y que no esten reembolsados
    ''' </summary>
    Public Function ListVoucherTransactionBetweenDateNotRefundExpenseType(CashRegisterId As Integer, InitialDate As Date, FinalDate As Date, ExpenseType As Byte, ByVal audit As AuditMessage) As ActionResult(Of List(Of VoucherTransaction)) Implements IVoucherTransactionAdminService.ListVoucherTransactionBetweenDateNotRefundExpenseType

        If audit Is Nothing Then
            Throw New ArgumentNullException("audit")
        End If
        Try
            Dim listVoucherTransaction As List(Of VoucherTransaction) = Me._voucherRepository.ListVoucherTransactionBetweenDateNotRefundExpenseType(CashRegisterId, InitialDate, FinalDate, ExpenseType)
            Return New ActionResult(Of List(Of VoucherTransaction)) With {.StateResult = True, .ObjectEmbbeded = listVoucherTransaction}
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of List(Of VoucherTransaction)) With {.StateResult = False}
        End Try
    End Function

    ''' <summary>
    ''' Lists the type of the voucher transaction final date not refund expense.
    ''' </summary>
    ''' <param name="CashRegisterId">The cash register identifier.</param>
    ''' <param name="FinalDate">The final date.</param>
    ''' <param name="ExpenseType">Type of the expense.</param>
    ''' <param name="audit">The audit.</param>
    ''' <returns></returns>
    ''' <exception cref="System.ArgumentNullException">audit</exception>
    Public Function ListVoucherTransactionFinalDateNotRefundExpenseType(CashRegisterId As Integer, FinalDate As Date, ExpenseType As Byte, ByVal audit As AuditMessage) As ActionResult(Of List(Of VoucherTransaction)) Implements IVoucherTransactionAdminService.ListVoucherTransactionFinalDateNotRefundExpenseType
        If audit Is Nothing Then
            Throw New ArgumentNullException("audit")
        End If
        Try
            Dim listVoucherTransaction As List(Of VoucherTransaction) = Me._voucherRepository.ListVoucherTransactionFinalDateNotRefundExpenseType(CashRegisterId, FinalDate, ExpenseType)
            Return New ActionResult(Of List(Of VoucherTransaction)) With {.StateResult = True, .ObjectEmbbeded = listVoucherTransaction}
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of List(Of VoucherTransaction)) With {.StateResult = False}
        End Try
    End Function

    ''' <summary>
    ''' obtiene un listado de comprobantes de egreso por el id del reembolso
    ''' </summary>
    ''' <param name="IdRefund">The identifier refund.</param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    ''' <exception cref="System.ArgumentNullException">
    ''' IdRefund
    ''' or
    ''' audit
    ''' </exception>
    Public Function ListVoucherTransactionByIdRefund(IdRefund As Integer, audit As AuditMessage) As ActionResult(Of List(Of VoucherTransaction)) Implements IVoucherTransactionAdminService.ListVoucherTransactionByIdRefund
        If IdRefund = 0 Then
            Throw New ArgumentNullException("IdRefund")
        End If
        If audit Is Nothing Then
            Throw New ArgumentNullException("audit")
        End If
        Try
            Dim listVoucherTransaction As List(Of VoucherTransaction) = Me._voucherRepository.ListVoucherTransactionByIdRefund(IdRefund)
            Return New ActionResult(Of List(Of VoucherTransaction)) With {.StateResult = True, .ObjectEmbbeded = listVoucherTransaction}
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of List(Of VoucherTransaction)) With {.StateResult = False}
        End Try
    End Function

    ''' <summary>
    ''' Obtiene un comprobante de egreso por número de cheque
    ''' </summary>
    ''' <param name="Id">checkNumber</param>
    ''' <returns></returns>
    ''' <exception cref="System.ArgumentNullException">Id</exception>
    Public Function GetVoucherTransactionByCheckNumber(checkNumber As Long) As ActionResult(Of VoucherTransaction) Implements IVoucherTransactionAdminService.GetVoucherTransactionByCheckNumber
        If checkNumber = 0 Then
            Return New ActionResult(Of VoucherTransaction) With {.StateResult = False}
        End If
        Try
            Dim voucherTransaction As VoucherTransaction = Me._voucherRepository.GetVoucherTransactionByCheckNumber(checkNumber)
            Return New ActionResult(Of VoucherTransaction) With {.StateResult = True, .ObjectEmbbeded = voucherTransaction}
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of VoucherTransaction) With {.StateResult = False}
        End Try
    End Function

    ''' <summary>
    ''' Genera documento soporte electronico a comprobante de egreso
    ''' </summary>
    ''' <param name="listVoucherTransaction"></param>
    ''' <param name="audit"></param>
    ''' <param name="authorizationResolutionId"></param>
    ''' <returns></returns>
    Public Function GenerateElectronicSupportDocument(listVoucherTransaction As List(Of VoucherTransaction), audit As AuditMessage, Optional authorizationResolutionId As Integer? = Nothing) As ActionResult(Of List(Of Tuple(Of String, String)))
        If listVoucherTransaction Is Nothing Then
            Throw New Exception("Comprobantes de egresos vacio")
        End If

        Dim UnitOfWork As IUnitWork = _electronicSupportDocumentRepository.UnitWork
        Dim txSettings As New TransactionOptions()
        txSettings.Timeout = TransactionManager.MaximumTimeout
        txSettings.IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted
        Using transaction As New TransactionScope(TransactionScopeOption.Required, txSettings)
            Try
                Dim message As String = String.Empty
                Dim listCodes As New List(Of Tuple(Of String, String))

                Dim _Xml As String = ConvertToXml(listVoucherTransaction, audit)

                Dim resultStore = Me._electronicSupportDocumentRepository.SP_GenerateElectronicSupportDocument(_Xml)

                If resultStore.Any(Function(r) r.Code = "999") Then
                    UnitOfWork.RollbackChanges()
                    transaction.Dispose()
                    resultStore.ToList().ForEach(Sub(r)
                                                     listCodes.Add(New Tuple(Of String, String)(r.Code, r.Message))
                                                 End Sub)

                    Return New ActionResult(Of List(Of Tuple(Of String, String))) With {.StateResult = False, .ObjectEmbbeded = listCodes, .Message = message}

                End If

                Dim IdsElectronicDS = resultStore.FindAll(Function(s) s.Code = "002").Select(Function(d) d.IdDS).ToList()

                resultStore.ForEach(Sub(r)
                                        listCodes.Add(New Tuple(Of String, String)(r.Code, r.Message))
                                    End Sub)

                If IdsElectronicDS.Any() Then

                    Dim _electronicDocumentSupport = Me._electronicSupportDocumentRepository.GetByFilter(Function(x) IdsElectronicDS.Contains(x.Id)).ToList()

                    If Not _electronicDocumentSupport.Any() Then
                        UnitOfWork.RollbackChanges()
                        transaction.Dispose()
                        Return New ActionResult(Of List(Of Tuple(Of String, String))) With {.StateResult = False, .ObjectEmbbeded = {New Tuple(Of String, String)("999", "Error Consultando los documentos soporte electronicos")}.ToList()}
                    End If

                    For Each Item In _electronicDocumentSupport
                        Dim settingsAccount = Me._settingsAccountRepository.GetSettingAccountSimple(Item.OperativeUnitId)

                        If settingsAccount Is Nothing OrElse settingsAccount.Id = 0 OrElse String.IsNullOrEmpty(settingsAccount.SoftwarePin) Then
                            Return New ActionResult(Of List(Of Tuple(Of String, String))) With {.StateResult = False, .ObjectEmbbeded = {New Tuple(Of String, String)("999", "No hay parametros de cuentas contables confirgurado para la unidad operativa")}.ToList()}
                        End If

                        Item.SoftwarePin = settingsAccount.SoftwarePin
                        Item.Environment = IIf(settingsAccount.SupportDocumentEnvironment, 1, 2)
                        Item.CUDS = Item.GetCUDSCode()
                        Me._electronicSupportDocumentRepository.SaveEntity(Item)
                    Next
                Else
                    UnitOfWork.RollbackChanges()
                    transaction.Dispose()
                    Return New ActionResult(Of List(Of Tuple(Of String, String))) With {.StateResult = False, .ObjectEmbbeded = listCodes, .Message = message}
                End If

                UnitOfWork.Commit()
                transaction.Complete()

                resultStore.ForEach(Sub(r) message = message + r.Message + vbCrLf)

                Return New ActionResult(Of List(Of Tuple(Of String, String))) With {.StateResult = True, .ObjectEmbbeded = listCodes, .Message = message}
            Catch ex As DbUpdateException
                transaction.Dispose()
                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
                Return New ActionResult(Of List(Of Tuple(Of String, String))) With {.StateResult = False, .Message = Utils.GetInnerExceptionMessageToString(ex), .MessageResult = {Utils.GetInnerExceptionMessageToString(ex)}.ToList}
            Catch ex As Exception
                transaction.Dispose()
                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
                Return New ActionResult(Of List(Of Tuple(Of String, String))) With {.StateResult = False, .Message = Utils.GetInnerExceptionMessageToString(ex), .MessageResult = {Utils.GetInnerExceptionMessageToString(ex)}.ToList}
            End Try
        End Using
    End Function

    ''' <summary>
    ''' Funcion para publicar el documento soporte a partir del comprobante de egreso
    ''' </summary>
    ''' <param name="listVoucherTransaction"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    Private Function PublishElectronicSupporDocumentMessages(listVoucherTransaction As List(Of VoucherTransaction), audit As AuditMessage) As ActionResult(Of List(Of String))
        If listVoucherTransaction Is Nothing Then
            Throw New ArgumentNullException("listAccountPayable")
        End If

        Try
            Dim data As New List(Of SupportDocumentEvent)

            data = listVoucherTransaction.Select(Function(v) New SupportDocumentEvent With {
                .EntityId = v.Id,
                .EntityCode = v.Code,
                .EntityName = "VoucherTransaction",
                .DocumentDate = v.DocumentDate}).ToList()

            _eventProxy.Publish(New EventData(data, NameOf(EventType.SupportDocument), NameOf(EventAction.added), audit.Company, audit.CodeUser, DateTime.Now().GetTimestamp))

            Return New ActionResult(Of List(Of String)) With {.StateResult = True}
        Catch ex As Exception
            Return New ActionResult(Of List(Of String)) With {.StateResult = False, .Message = Utils.GetInnerExceptionMessageToString(ex)}
        End Try
    End Function

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="listVoucherTransaction"></param>
    ''' <param name="Audit"></param>
    ''' <returns></returns>
    Private Function ConvertToXml(listVoucherTransaction As List(Of VoucherTransaction), Audit As AuditMessage) As String
        Dim builder As StringBuilder = New StringBuilder()
        builder.AppendLine("<Data>")
        builder.AppendLine("<CodeUser>" & Audit.CodeUser & "</CodeUser>")
        builder.AppendLine("<CompanyNit>" & listVoucherTransaction.FirstOrDefault().IndigoCompanyNit & "</CompanyNit>")
        For Each item In listVoucherTransaction
            builder.AppendLine("<VoucherTransactionIds>")
            builder.AppendLine("<Id>" & item.Id & "</Id>")
            builder.AppendLine("</VoucherTransactionIds>")
        Next
        builder.AppendLine("</Data>")
        Return builder.ToString()
    End Function

#Region "Private Methods"
    ''' <summary>
    ''' Método de comportamiento de traslado entre bancos
    ''' </summary>
    ''' <param name="voucherTransaction">The voucher transaction.</param>
    ''' <param name="vouDetail">The vou detail.</param>
    ''' <param name="audit">The audit.</param>
    ''' <returns></returns>
    Private Function Transfers(voucherTransaction As VoucherTransaction, vouDetail As VoucherTransactionDetails, audit As AuditMessage, Optional ByVal disconfirm As Boolean = False, Optional ByVal noteTreasury As TreasuryNote = Nothing) As ActionResult(Of String)
        Try
            Dim errorList As New StringBuilder()
            If voucherTransaction.ExpenseType = 1 Then 'Entre cuentas bancarias
                Dim entityAccountD As EntityBankAccounts = _entityAccountAdminService.GetEntityBankAccountById(vouDetail.IdEntityBankAccount, audit)
                With entityAccountD
                    Dim _treasuryBalance As New TreasuryBalance()
                    With _treasuryBalance
                        If disconfirm Then
                            .DocumentDate = noteTreasury.NoteDate
                            .DocumentNumber = noteTreasury.Code
                            .DocumentType = 4 'Notas
                            .Nature = 2 'Credito
                        Else
                            .DocumentDate = voucherTransaction.DocumentDate
                            .DocumentNumber = voucherTransaction.Code
                            .DocumentType = 2 'Comprobante de egreso
                            .Nature = 1 'Debito
                        End If
                        .PreviousBalance = entityAccountD.CurrentBalance
                        .ValueMovement = vouDetail.Value
                        .CreationDate = Date.Now
                    End With
                    .TreasuryBalance.Add(_treasuryBalance)
                    If disconfirm Then
                        entityAccountD.CurrentBalance -= vouDetail.Value
                    Else
                        entityAccountD.CurrentBalance += vouDetail.Value
                    End If
                End With
                Dim resultEntityAccountD = _entityAccountAdminService.SaveEntityBankAccount(entityAccountD, audit)
                If resultEntityAccountD.StateResult = False Then
                    errorList.AppendLine(resultEntityAccountD.Message)
                End If

            ElseIf voucherTransaction.ExpenseType = 3 Then 'Entre cajas
                Dim cashRegisterD As CashRegisters = _cashAdminService.GetCashRegisterById(vouDetail.CashRegisterId)
                With cashRegisterD
                    Dim _treasuryBalance As New TreasuryBalance()
                    With _treasuryBalance
                        If disconfirm Then
                            .DocumentNumber = noteTreasury.Code
                            .DocumentDate = noteTreasury.NoteDate
                            .DocumentType = 4 'Notas
                            .Nature = 2 'Credito
                        Else
                            .DocumentNumber = voucherTransaction.Code
                            .DocumentDate = voucherTransaction.DocumentDate
                            .DocumentType = 2 'Comprobante de egreso
                            .Nature = 1 'Debito
                        End If
                        .PreviousBalance = cashRegisterD.CurrentBalance
                        .ValueMovement = vouDetail.Value
                        .CreationDate = Date.Now
                    End With
                    .TreasuryBalance.Add(_treasuryBalance)
                    If disconfirm Then
                        cashRegisterD.CurrentBalance -= vouDetail.Value
                    Else
                        cashRegisterD.CurrentBalance += vouDetail.Value
                    End If
                End With
                Dim resultCashRegisterD = _cashAdminService.SaveCashRegister(cashRegisterD, audit)
                If resultCashRegisterD.StateResult = False Then
                    errorList.AppendLine(resultCashRegisterD.Message)
                End If
            End If
            If errorList.Length > 0 Then
                Return New ActionResult(Of String) With {.StateResult = False, .Message = errorList.ToString()}
            End If
            Return New ActionResult(Of String) With {.StateResult = True}
        Catch ex As Exception
            Return New ActionResult(Of String) With {.StateResult = False, .Message = ResourceManager.GetString("ErrorUnknown")}
        End Try
    End Function

    ''' <summary>
    ''' Método de comportamiento de egreso de caja menor
    ''' </summary>
    ''' <param name="voucherTransaction">The voucher transaction.</param>
    ''' <param name="vouDetail">The vou detail.</param>
    ''' <param name="audit">The audit.</param>
    ''' <returns></returns>
    Private Function BehaviorCashMinor(voucherTransaction As VoucherTransaction, vouDetail As VoucherTransactionDetails, audit As AuditMessage, idSequence As Long, Optional disconfirm As Boolean = False) As ActionResult(Of String)
        Dim errorList As New StringBuilder()
        Dim resultDischarge = _dischargeBillAdminService.ListDischargeBillByIdVoucherTransactionD(vouDetail.Id, audit)
        If resultDischarge.StateResult = False Then
            errorList.AppendLine(resultDischarge.Message)
        End If
        '2.	Actualizamos los saldos de las cuentas por pagar
        If resultDischarge.ObjectEmbbeded IsNot Nothing AndAlso resultDischarge.ObjectEmbbeded.Count > 0 Then
            For Each dischargeBill As DischargeBill In resultDischarge.ObjectEmbbeded
                Dim accountPayable As AccountPayable = _accountPayable.GetAccountPayableById(dischargeBill.IdAccountPayable, audit)
                Dim accountPayableShare As AccountPayableShares = accountPayable.AccountPayableShares.Where(Function(x) x.Id = dischargeBill.IdAccountPayableShare).Cast(Of AccountPayableShares).FirstOrDefault()
                If disconfirm Then
                    accountPayableShare.Balance += dischargeBill.AdvancedValue
                    accountPayable.Balance += dischargeBill.AdvancedValue
                    'este campo es el de abono
                    accountPayableShare.PaymentValue -= dischargeBill.AdvancedValue
                Else
                    accountPayableShare.Balance -= dischargeBill.AdvancedValue
                    accountPayable.Balance -= dischargeBill.AdvancedValue
                    'este campo es el de abono
                    accountPayableShare.PaymentValue += dischargeBill.AdvancedValue
                End If
                Dim resultAccountPayale = _accountPayable.SaveAccountPayable(accountPayable, audit)
                If resultAccountPayale.StateResult = False Then
                    errorList.AppendLine(ResourceManager.GetString("BalanceUpdateErrorInvoice", TREASURY))
                End If
                'Guardar un registro en el movimiento de pagos (MovementAccountPayables) (No se si esta bien, los campos de entity son de la entidad principal? o del detalle?)
                Dim movement As New MovementAccountPayables() With {.IdAccountPayable = dischargeBill.IdAccountPayable, .IdAccountPayableShare = dischargeBill.IdAccountPayableShare, .EntityId = voucherTransaction.Id, _
                                                                    .EntityCode = voucherTransaction.Code, .EntityName = GetType(VoucherTransaction).Name, .MovementDate = DateTime.Now, .Value = dischargeBill.AdvancedValue}
                'Guardar el movimiento
                Dim resultMovement = _movementAccountPayableAdminService.SaveMovementAccountPayable(movement, audit)
                If resultMovement.StateResult = False Then
                    errorList.AppendLine(ResourceManager.GetString("CreateMovementError", TREASURY))
                End If
            Next
        End If
        If errorList.Length > 0 Then
            Return New ActionResult(Of String) With {.StateResult = False, .Message = errorList.ToString()}
        End If
        Return New ActionResult(Of String) With {.StateResult = True}
    End Function

    ''' <summary>
    ''' Método de comportamiento de Pagos/Anticipos de facturas
    ''' </summary>
    ''' <returns></returns>
    Private Function BehaviorPaymentAdvancePaymentInvoices(voucherTransaction As VoucherTransaction, vouDetail As VoucherTransactionDetails, audit As AuditMessage, Optional ByVal disconfirm As Boolean = False) As ActionResult(Of String)
        Try
            Dim errorList As New StringBuilder()
            '2.	Actualizamos los saldos de las cuentas por pagar
            Dim resultSearchDischargeBill = _dischargeBillAdminService.ListDischargeBillByIdVoucherTransactionD(vouDetail.Id, audit)
            If resultSearchDischargeBill.StateResult = False Then
                errorList.AppendLine(ResourceManager.GetString("InvoiceConfirmError", TREASURY))
            End If
            '3.	Si hay anticipo, guardamos el anticipo en Pagos
            Dim advanceTreasury As ActionResult(Of TreasuryAdvances) = Me.GetTreasuryAdvanceByIdVoucherTransactionDetail(vouDetail.Id)
            If advanceTreasury.StateResult = False Then
                errorList.AppendLine(advanceTreasury.Message)
            Else
                If resultSearchDischargeBill.ObjectEmbbeded IsNot Nothing AndAlso resultSearchDischargeBill.ObjectEmbbeded.Count > 0 Then
                    'Dim ObservationVoucherTransaction As New StringBuilder()
                    For Each dischargeBill As DischargeBill In resultSearchDischargeBill.ObjectEmbbeded

                        Dim accountPayable As AccountPayable = _accountPayable.GetAccountPayableById(dischargeBill.IdAccountPayable, audit)
                        Dim accountPayableShare As AccountPayableShares = accountPayable.AccountPayableShares.Where(Function(x) x.Id = dischargeBill.IdAccountPayableShare).Cast(Of AccountPayableShares).FirstOrDefault()
                        'validacion para que halla saldo en la couta
                        If disconfirm Then
                            accountPayableShare.Balance += dischargeBill.AdvancedValue
                            accountPayable.Balance += dischargeBill.AdvancedValue
                            accountPayableShare.PaymentValue -= dischargeBill.AdvancedValue
                        Else
                            accountPayableShare.Balance -= dischargeBill.AdvancedValue
                            accountPayable.Balance -= dischargeBill.AdvancedValue
                            accountPayableShare.PaymentValue += dischargeBill.AdvancedValue
                        End If
                        Dim resultAccountPayale = _accountPayable.SaveAccountPayable(accountPayable, audit, 0, False)
                        If resultAccountPayale.StateResult = False Then
                            errorList.AppendLine(ResourceManager.GetString("BalanceUpdateErrorInvoice", TREASURY))
                        End If
                        'Guardar un registro en el movimiento de pagos (MovementAccountPayables) (No se si esta bien, los campos de entity son de la entidad principal? o del detalle?)
                        Dim movement As New MovementAccountPayables() With {.IdAccountPayable = dischargeBill.IdAccountPayable, .IdAccountPayableShare = dischargeBill.IdAccountPayableShare, .EntityId = voucherTransaction.Id, _
                                                                            .EntityCode = voucherTransaction.Code, .EntityName = GetType(VoucherTransaction).Name, .MovementDate = DateTime.Now, .Value = dischargeBill.AdvancedValue}
                        'Guardar el movimiento
                        Dim resultMovement = _movementAccountPayableAdminService.SaveMovementAccountPayable(movement, audit, False)
                        If resultMovement.StateResult = False Then
                            errorList.AppendLine(ResourceManager.GetString("CreateMovementError", TREASURY))
                        End If
                        'If voucherTransaction.IsDispersionFundGenerated AndAlso accountPayable.Coments IsNot Nothing AndAlso Not accountPayable.Coments.ToString.Trim.Equals("") Then
                        '    'ObservationVoucherTransaction.AppendLine(accountPayable.Coments)
                        'End If
                    Next
                    'If ObservationVoucherTransaction.Length > 0 Then
                    '    'voucherTransaction.Detail = ObservationVoucherTransaction.ToString()
                    'End If
                ElseIf advanceTreasury.ObjectEmbbeded.Value < 0 Then
                    errorList.AppendLine(ResourceManager.GetString("InvoiceConfirmError", TREASURY))
                End If

                If advanceTreasury.ObjectEmbbeded IsNot Nothing AndAlso advanceTreasury.ObjectEmbbeded.Id > 0 Then
                    Dim supplier As Supplier = _supplierAdminService.GetSupplierByIdThirdParty(vouDetail.IdThirdParty)
                    If supplier IsNot Nothing AndAlso supplier.Id > 0 Then
                        If disconfirm Then
                            'si estoy reversando entonces consulto el anticipo de pagos que se guardó y se elimima
                            Dim moneyAdvance As AdvancePayments = _moneyAdvanceRepository.GetMoneyAdvance(voucherTransaction.Code)

                            'Se valida que el anticipo no haya sido afectado por otro proceso, esto se hace porque cuando el anticipo ya fue afectado por otro proceso
                            'como se debe eliminar saldría error por referencia
                            If moneyAdvance.Value <> moneyAdvance.Balance Then 'Si son diferentes es porque se afecto el anticipo por otro proceso
                                errorList.AppendLine("No se puede confirmar porque el anticipo con código " + moneyAdvance.Code + " ya fue afectado por otro proceso")
                            Else 'Si no se ha afectado el anticipo por otro proceso
                                If moneyAdvance IsNot Nothing AndAlso moneyAdvance.Id > 0 Then
                                    _moneyAdvanceRepository.DeleteEntity(moneyAdvance)
                                End If
                            End If
                        Else
                            Dim moneyAdvance As New AdvancePayments() With {.Code = voucherTransaction.Code, .IdSupplier = supplier.Id, .IdAccount = vouDetail.IdMainAccount, .IdCostCenter = vouDetail.IdCostCenter, .IdThirdParty = vouDetail.IdThirdParty, _
                                                                    .StateAdvancePayments = 1, .DocumentDate = voucherTransaction.DocumentDate, .Comments = advanceTreasury.ObjectEmbbeded.Detail, .Value = advanceTreasury.ObjectEmbbeded.Value, _
                                                                    .Balance = advanceTreasury.ObjectEmbbeded.Value, .EntityCode = voucherTransaction.Code, .EntityId = voucherTransaction.Id, .EntityName = GetType(VoucherTransaction).Name, _
                                                                    .Status = 2, .CreationUser = audit.CodeUser, .CreationDate = DateTime.Now, .ModificationUser = audit.CodeUser, .ModificationDate = DateTime.Now, .ConfirmationUser = audit.CodeUser, _
                                                                    .ConfirmationDate = DateTime.Now, .AnnulmentUser = audit.CodeUser, .AnnulmentDate = DateTime.Now}
                            'Consulto el id de la secuencia para el frontal de anticipos de pagos
                            Dim resultMoneyAdvance = _moneyAdvanceAdminService.SaveMoneyAdvance(moneyAdvance, audit, 0, False)
                            If resultMoneyAdvance.StateResult = False Then
                                errorList.AppendLine("Error al intentar actualizar los saldos de las cuentas")
                            End If
                        End If
                    Else
                        Dim thirParty As ThirdParty = _supplierRepository.GetThirdPartyById(vouDetail.IdThirdParty)
                        errorList.AppendLine(String.Format("Para poder generar un anticipo al tercero {0} este debe estar creado como proveedor", String.Concat(thirParty.Nit, " - ", thirParty.Name)))
                    End If
                End If
            End If
            If errorList.Length > 0 Then
                Return New ActionResult(Of String) With {.StateResult = False, .Message = errorList.ToString()}
            End If
            Return New ActionResult(Of String) With {.StateResult = True}
        Catch ex As Exception
            Return New ActionResult(Of String) With {.StateResult = False, .Message = ResourceManager.GetString("ErrorUnknown")}
        End Try
    End Function

    ''' <summary>
    ''' Método de comportamiento de reembolso de caja menor
    ''' </summary>
    ''' <param name="voucherTransaction">The voucher transaction.</param>
    ''' <param name="vouDetail">The vou detail.</param>
    ''' <param name="audit">The audit.</param>
    ''' <param name="idSequence">The identifier sequence.</param>
    ''' <returns></returns>
    Private Function RefundCashMinor(voucherTransaction As VoucherTransaction, vouDetail As VoucherTransactionDetails, audit As AuditMessage, idSequence As Long, Optional ByVal disconfirm As Boolean = False) As ActionResult(Of String)
        Dim errorList As New StringBuilder()
        '2.	Actualizamos estado en la cabecera de la tabla de reembolsos
        Dim _listRefund As ActionResult(Of List(Of Refunds)) = _refundAdminService.ListRefundByCashRegisterId(vouDetail.CashRegisterId)
        If _listRefund.StateResult = False Then
            errorList.AppendLine(ResourceManager.GetString("RefundSearchError", TREASURY))
        End If
        For Each Refund As Refunds In _listRefund.ObjectEmbbeded
            If Not Refund.Refunded Then
                With Refund
                    .Refunded = True
                End With
                Dim resultRefund = _refundAdminService.SaveRefund(Refund, audit, False, idSequence, False)
                If resultRefund.StateResult = False Then
                    errorList.AppendLine(ResourceManager.GetString("RefundUpdateError", TREASURY))
                ElseIf resultRefund.StateResult AndAlso (resultRefund.Message IsNot Nothing AndAlso Not resultRefund.Message.Equals("")) Then
                    errorList.AppendLine(String.Format("Error al confirmar el reembolso por : {0}{1}", vbCrLf, resultRefund.Message))
                End If
            End If
            'consultamos la caja a reembolsar
            Dim _cashRefund As CashRegisters = _cashAdminService.GetCashRegisterById(Refund.IdCashRegister)
            If _cashRefund Is Nothing OrElse _cashRefund.Id = 0 Then
                errorList.AppendLine(ResourceManager.GetString("CashRefundNotFound", TREASURY))
            Else
                Dim _treasuryBalance As New TreasuryBalance()
                With _treasuryBalance
                    .DocumentNumber = voucherTransaction.Code
                    .DocumentDate = voucherTransaction.DocumentDate
                    If disconfirm Then
                        .DocumentType = 4 'Notas
                        .Nature = 2 'Debito
                    Else
                        .DocumentType = 2 'Comprobante de egreso
                        .Nature = 1 'Credito
                    End If
                    .PreviousBalance = _cashRefund.CurrentBalance
                    .ValueMovement = Refund.Value
                    .CreationDate = Date.Now
                End With
                _cashRefund.TreasuryBalance.Add(_treasuryBalance)
                If disconfirm Then
                    _cashRefund.CurrentBalance -= Refund.Value
                Else
                    _cashRefund.CurrentBalance += Refund.Value
                End If
                Dim saveResultCash = _cashAdminService.SaveCashRegister(_cashRefund, audit)
                If saveResultCash.StateResult = False Then
                    errorList.AppendLine(ResourceManager.GetString("RefundUpdateError", TREASURY))
                End If
                '3.	Actualizamos bandera en la cabecera de los comprobantes de caja menor para saber que ya fueron reembolsados
                Dim _resultListVoucherForRefund = Me.ListVoucherTransactionByIdRefund(Refund.Id, audit)
                If _resultListVoucherForRefund.StateResult = False Then
                    errorList.AppendLine(ResourceManager.GetString("RefundSearchError", TREASURY))
                End If
                Dim _listVoucherRefund As List(Of VoucherTransaction) = _resultListVoucherForRefund.ObjectEmbbeded
                If _listVoucherRefund IsNot Nothing Then
                    For Each vt As VoucherTransaction In _listVoucherRefund
                        If Not vt.RefundCashRegisterExpense Then
                            vt.RefundCashRegisterExpense = True
                            Dim _resultsUpdateVoucher = SaveVoucherTransaction(vt, audit, False)
                            If _resultsUpdateVoucher.StateResult = False Then
                                errorList.AppendLine(_resultsUpdateVoucher.Message)
                            End If
                        End If
                    Next
                End If
            End If
        Next
        If errorList.Length > 0 Then
            Return New ActionResult(Of String) With {.StateResult = False, .Message = errorList.ToString()}
        End If
        Return New ActionResult(Of String) With {.StateResult = True}
    End Function

    ''' <summary>
    ''' Método de comportamiento ninguno
    ''' </summary>
    ''' <returns></returns>
    Private Function BehaviorNone(voucherTransaction As VoucherTransaction, vouDetail As VoucherTransactionDetails, audit As AuditMessage, idSequence As Long) As ActionResult(Of String)
        Dim errorList As New StringBuilder()
        If errorList.Length > 0 Then
            Return New ActionResult(Of String) With {.StateResult = False, .Message = errorList.ToString()}
        End If
        Return New ActionResult(Of String) With {.StateResult = True}
    End Function

    ''' <summary>
    ''' Behaviors the returning imprest rc.
    ''' </summary>
    ''' <returns></returns>
    Private Function BehaviorReturningImprestRC(voucherTransaction As VoucherTransaction, vouDetail As VoucherTransactionDetails, audit As AuditMessage, Optional ByVal disconfirm As Boolean = False) As ActionResult(Of String)
        Dim errorList As New StringBuilder()
        For Each item As VoucherTransactionAdvance In vouDetail.VoucherTransactionAdvance
            Dim _portfolioAdvance As PortfolioAdvance = _portfolioAdvanceRepository.GetPortfolioAdvanceByIdSimple(item.PortfolioAdvanceId)
            If _portfolioAdvance IsNot Nothing AndAlso _portfolioAdvance.Id > 0 Then
                If disconfirm Then
                    _portfolioAdvance.Balance += item.Value
                Else
                    _portfolioAdvance.Balance -= item.Value
                End If
                _portfolioAdvance.MarkAsModified()
                _portfolioAdvanceRepository.SaveEntity(_portfolioAdvance)
                _portfolioAdvanceRepository.UnitWork.Commit()
                'Dim resultSaveAdvance = _portfolioAdvanceAdminService.SavePortfolioAdvance(_portfolioAdvance, audit, 0, False)
                'If resultSaveAdvance.StateResult = False Then
                '    errorList.AppendLine(String.Format(ResourceManager.GetString("AdvanceReturnError", TREASURY), _portfolioAdvance.Code))
                'End If
            End If
        Next
        If errorList.Length > 0 Then
            Return New ActionResult(Of String) With {.StateResult = False, .Message = errorList.ToString()}
        End If
        Return New ActionResult(Of String) With {.StateResult = True}
    End Function

#End Region

#End Region

#Region "IDisposable Support"
    Private disposedValue As Boolean ' Para detectar llamadas redundantes

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not disposedValue Then
            If disposing Then
                _accountPayable.Dispose()
                _entityAccountAdminService.Dispose()
                _accountingAdminService.Dispose()
                treasuryService.Dispose()
                _movementAccountPayableAdminService.Dispose()
                _moneyAdvanceAdminService.Dispose()
                _supplierAdminService.Dispose()
                _cashAdminService.Dispose()
                _dischargeBillAdminService.Dispose()
                _refundAdminService.Dispose()
                _sequencePaymentsAdminService.Dispose()
                _portfolioAdvanceAdminService.Dispose()
                _treasuryControlAdminService.Dispose()
            End If
            _documentTypeRepository = Nothing
            _outstandingCheckRepository = Nothing
            _voucherRepository = Nothing
            _secuenseDRepository = Nothing
            _checkBookRepository = Nothing
            _checkBlockRepository = Nothing
            _accountPayable = Nothing
            _settingsTreasuryRepository = Nothing
            _expenseConceptRepository = Nothing
            _entityAccountAdminService = Nothing
            _accountingAdminService = Nothing
            treasuryService = Nothing
            _treasuryAdvanceRepository = Nothing
            _movementAccountPayableAdminService = Nothing
            _moneyAdvanceAdminService = Nothing
            _supplierAdminService = Nothing
            _cashAdminService = Nothing
            _dischargeBillAdminService = Nothing
            _refundAdminService = Nothing
            _sequencePaymentsAdminService = Nothing
            _portfolioAdvanceAdminService = Nothing
            _entityBankAccountRepository = Nothing
            _cashRegisterRepository = Nothing
            _dischargeBillRepository = Nothing
            _accountPayableRepository = Nothing
            _portfolioAdvanceRepository = Nothing
            _refundRepository = Nothing
            _treasuryControlAdminService = Nothing
            _pucRepository = Nothing
            _closeMonthRepository = Nothing
            _cancellationCheckRepository = Nothing
            _supplierRepository = Nothing
            _moneyAdvanceRepository = Nothing
            _paymentNotesAccountPayableAdvanceRepository = Nothing
            _paymentTransferRepository = Nothing
            _crossingAccountRepository = Nothing
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
