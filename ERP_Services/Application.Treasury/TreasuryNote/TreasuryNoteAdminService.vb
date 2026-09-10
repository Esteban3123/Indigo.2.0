'***********************************************************************
' Assembly         : Application.Treasury
' Author           : Diego Andrés Roldán Lozano
' Created          : 02-04-2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************
#Region "Libraries"
Imports Domain.Entities
Imports Domain.Base
Imports Infrastructure.CrossCutting.Exceptions
Imports Application.Base
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities
Imports System.Data.Entity.Infrastructure
Imports System.Data.Entity.Core
Imports System.Transactions
Imports Infrastructure.CrossCutting.Resources
Imports Domain.Entities.Service
Imports Application.Accounting

#End Region

Public Class TreasuryNoteAdminService
    Implements ITreasuryNoteAdminService, Inject

#Region "Fields"

    ''' <summary>
    ''' Repositorio de notas de tesoreria
    ''' </summary>
    Private _treasuryNoteRepository As ITreasuryNoteRepository

    ''' <summary>
    ''' repositorio de las secuencias
    ''' </summary>
    Private _secuenseDRepository As ISequenseTreasuryDRepository

    ''' <summary>
    ''' repositorio de cuentas contables
    ''' </summary>
    Private _mainAccountRepository As IPUCRepository

    ''' <summary>
    ''' repositorio de cuentas bancarias
    ''' </summary>
    Private _entityBankAccountRepository As IEntityBankAccountRepository

    ''' <summary>
    ''' Servicios de Aplicacion de cuentas bancarias
    ''' </summary>
    Private _entityBankAccountAdminService As IEntityBankAccountAdminService

    ''' <summary>
    ''' repositorio de cajas
    ''' </summary>
    Private _cashRegisterRepository As ICashRegisterRepository

    ''' <summary>
    ''' Servicios de Aplicacion de cajas
    ''' </summary>
    Private _cashRegisterAdminService As ICashRegisterAdminService

    ''' <summary>
    ''' servicios de aplicacion de contabilidad
    ''' </summary>
    Private _accountingAdminService As IAccountingDocumentAdminService

    ''' <summary>
    ''' Variable tipo repositorio para parametros de pago
    ''' </summary>
    ''' <remarks></remarks>
    Private _settingsTreasuryRepository As ISettingsTreasuryRepository

    ''' <summary>
    ''' Servicios de Aplicacion de comprobantes de egreso
    ''' </summary>
    Private _voucherTransactionAdminService As IVoucherTransactionAdminService

    Private _documentTypeRepostiry As IDocumentTypeRepository

    Private _voucherTransactionRepository As IVoucherTransactionRepository

    Private _refundRepository As IRefundRepository

    Private treasuryService As ITreasuryServices

    Private _treasuryControlAdminService As ITreasuryControlAdminService

    Private _cashReceiptAdminService As ICashReceiptsAdminService

    Private _consignmentAdminService As IConsignmentTransferAdminService

    Private _crossingAccountAdminService As ICrossingAccountAdminService


#End Region

#Region "Methods"

    Public Sub New(ByVal treasuryNoteRepository As ITreasuryNoteRepository, ByVal secuenseDRepository As ISequenseTreasuryDRepository, ByVal mainAccountRepository As IPUCRepository,
                   ByVal entityBankAccountRepository As IEntityBankAccountRepository, ByVal cashRegisterRepository As ICashRegisterRepository, ByVal entityBankAccountAdminService As IEntityBankAccountAdminService,
                   ByVal cashRegisterAdminService As ICashRegisterAdminService, ByVal accountingAdminService As IAccountingDocumentAdminService, ByVal settingsTreasuryRepository As ISettingsTreasuryRepository,
                   ByVal voucherTransactionAdminService As IVoucherTransactionAdminService, documentTypeRepostiry As IDocumentTypeRepository, voucherTransactionRepository As IVoucherTransactionRepository,
                   refundRepository As IRefundRepository, _treasuryService As ITreasuryServices, treasuryControlAdminService As ITreasuryControlAdminService, cashReceiptAdminService As ICashReceiptsAdminService,
                   consignmentAdminService As IConsignmentTransferAdminService, crossingAccountAdminService As ICrossingAccountAdminService)
        If treasuryNoteRepository Is Nothing Then
            Throw New ArgumentNullException("treasuryNoteRepository")
        End If
        If secuenseDRepository Is Nothing Then
            Throw New ArgumentNullException("secuenseDRepository")
        End If
        If mainAccountRepository Is Nothing Then
            Throw New ArgumentNullException("mainAccountRepository")
        End If
        If entityBankAccountRepository Is Nothing Then
            Throw New ArgumentNullException("entityBankAccountRepository")
        End If
        If cashRegisterRepository Is Nothing Then
            Throw New ArgumentNullException("cashRegisterRepository")
        End If
        If entityBankAccountAdminService Is Nothing Then
            Throw New ArgumentNullException("entityBankAccountAdminService")
        End If
        If cashRegisterAdminService Is Nothing Then
            Throw New ArgumentNullException("cashRegisterAdminService")
        End If
        If accountingAdminService Is Nothing Then
            Throw New ArgumentNullException("accountingAdminService")
        End If
        If settingsTreasuryRepository Is Nothing Then
            Throw New ArgumentNullException("settingsTreasuryRepository")
        End If
        If voucherTransactionAdminService Is Nothing Then
            Throw New ArgumentNullException("voucherTransactionAdminService")
        End If
        If crossingAccountAdminService Is Nothing Then
            Throw New ArgumentNullException("crossingAccountAdminService")
        End If
        _treasuryNoteRepository = treasuryNoteRepository
        treasuryService = _treasuryService
        _secuenseDRepository = secuenseDRepository
        _mainAccountRepository = mainAccountRepository
        _entityBankAccountRepository = entityBankAccountRepository
        _cashRegisterRepository = cashRegisterRepository
        _entityBankAccountAdminService = entityBankAccountAdminService
        _cashRegisterAdminService = cashRegisterAdminService
        _accountingAdminService = accountingAdminService
        _settingsTreasuryRepository = settingsTreasuryRepository
        _voucherTransactionAdminService = voucherTransactionAdminService
        _documentTypeRepostiry = documentTypeRepostiry
        _voucherTransactionRepository = voucherTransactionRepository
        _refundRepository = refundRepository
        _treasuryControlAdminService = treasuryControlAdminService
        _cashReceiptAdminService = cashReceiptAdminService
        _consignmentAdminService = consignmentAdminService
        _crossingAccountAdminService = crossingAccountAdminService
    End Sub

    ''' <summary>
    ''' obtiene una nota de tesoreria por codigo
    ''' </summary>
    Public Function GetTreasuryNote(code As String, audit As AuditMessage) As ActionResult(Of TreasuryNote) Implements ITreasuryNoteAdminService.GetTreasuryNote
        If String.IsNullOrEmpty(code) Then
            Throw New ArgumentNullException("code")
        End If
        If audit Is Nothing Then
            Throw New ArgumentNullException("audit")
        End If
        Try
            Dim treasuryNote As TreasuryNote = Me._treasuryNoteRepository.GetTreasuryNote(code.Trim())
            If treasuryNote IsNot Nothing AndAlso treasuryNote.Id > 0 Then
                Dim status As Integer = Infrastructure.CrossCutting.Audit.Actions.Print
                Dim auditProcess As New IndigoAuditSimpleEntity(Of TreasuryNote)(treasuryNote, audit, status)
                auditProcess.Execute()
            End If
            Return New ActionResult(Of TreasuryNote) With {.StateResult = True, .ObjectEmbbeded = treasuryNote}
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of TreasuryNote) With {.StateResult = False}
        End Try
    End Function

    ''' <summary>
    ''' Obtiene una nota de tesoreria por id
    ''' </summary>
    ''' <param name="Id"></param>
    ''' <returns></returns>
    Public Function GetTreasuryNoteById(Id As Integer) As TreasuryNote Implements ITreasuryNoteAdminService.GetTreasuryNoteById
        Try
            Return _treasuryNoteRepository.GetTreasuryNoteById(Id)
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return Nothing
        End Try
    End Function

    ''' <summary>
    ''' Guarda una nota de tesoreria
    ''' </summary>
    Public Function SaveTreasuryNote(treasuryNote As TreasuryNote, audit As AuditMessage, withConfirm As Boolean, Optional idSequence As Long = 0) As ActionResult(Of TreasuryNote) Implements ITreasuryNoteAdminService.SaveTreasuryNote
        If treasuryNote Is Nothing Then
            Throw New ArgumentNullException("treasuryNote")
        End If

        Dim unitOfWork As IUnitWork = Me._treasuryNoteRepository.UnitWork
        Dim unitWorkSequence As IUnitWork = Me._secuenseDRepository.UnitWork

        Try
            Dim resultValidateVoucher As ActionResult = treasuryService.ValidateTreasuryNoteSave(treasuryNote)
            If Not resultValidateVoucher.StateResult Then
                Return New ActionResult(Of TreasuryNote) With {.StateResult = False, .Message = resultValidateVoucher.Message}
            End If

            Using scope As New TransactionScope(TransactionScopeOption.Required, New TransactionOptions() With {.Timeout = TransactionManager.MaximumTimeout, .IsolationLevel = IsolationLevel.ReadCommitted})
                If String.IsNullOrEmpty(treasuryNote.Code) Then
                    Dim seq As TreasurySequenceDetail = _secuenseDRepository.GetSequenseDById(idSequence)
                    If seq IsNot Nothing AndAlso seq.Id > 0 AndAlso seq.TreasurySequence.Sequential Then
                        Dim res = Infrastructure.CrossCutting.Base.Sequense.GetSequense(seq.Sequense.Pattern, seq.Next)
                        If res IsNot Nothing AndAlso Not res.Equals(Infrastructure.CrossCutting.Base.Sequense.ERROR_MAXVALUE) Then
                            treasuryNote.Code = res
                            seq.Next += 1
                            Me._secuenseDRepository.SaveEntity(seq)
                        Else
                            scope.Dispose()
                            Return New ActionResult(Of TreasuryNote) With {.StateResult = False, .Message = ResourceManager.GetString("SequenceNotFound")}
                        End If
                    Else
                        scope.Dispose()
                        Return New ActionResult(Of TreasuryNote) With {.StateResult = False, .Message = ResourceManager.GetString("SequenceNotFound")}
                    End If
                End If

                Dim auxTreasuryNote As TreasuryNote = Nothing
                Dim status As Integer

                If treasuryNote.CashReceiptId.HasValue Then
                    Dim cashRegister = _cashReceiptAdminService.GetCashReceiptsById(treasuryNote.CashReceiptId)
                    If cashRegister.CurrencyId <> treasuryNote.CurrencyId Then
                        Throw New Exception("La moneda del movimiento origen no coincide con la moneda actual de la transacción")
                    End If
                End If

                If treasuryNote.ChangeTracker.State = Domain.Base.Entities.ObjectState.Added Then
                    treasuryNote.CreationDate = Date.Now
                    treasuryNote.CreationUser = audit.CodeUser
                    status = Infrastructure.CrossCutting.Audit.Actions.Insert

                    Dim treasuryControl As New TreasuryControl() With {.DocumentNumber = treasuryNote.Code, .DocumentType = 3, .DocumentUser = audit.CodeUser, .DocumentDate = treasuryNote.NoteDate}
                    Dim resultSaveControl = _treasuryControlAdminService.SaveTreasuryControl(treasuryControl, audit)
                    If resultSaveControl.StateResult = False Then
                        Throw New Exception(ResourceManager.GetString("SaveTreasuryControlError", "Treasury"))
                    End If
                Else
                    treasuryNote.ModificationDate = Date.Now
                    treasuryNote.ModificationUser = audit.CodeUser
                    status = Infrastructure.CrossCutting.Audit.Actions.Update
                    auxTreasuryNote = treasuryNote.OriginalValue
                    If treasuryNote.Status = 3 Then 'si se va a anular
                        status = Infrastructure.CrossCutting.Audit.Actions.Annular
                        treasuryNote.AnnulmentDate = Date.Now
                        treasuryNote.AnnulmentUser = audit.CodeUser

                        Dim treasuryControl As TreasuryControl = _treasuryControlAdminService.GetTreasuryControlByDocumentNumber(treasuryNote.Code, 3) '3-notass
                        If treasuryControl IsNot Nothing AndAlso treasuryControl.Id > 0 Then
                            treasuryControl.MarkAsDeleted()
                            Dim resultSaveControl = _treasuryControlAdminService.DeleteTreasuryControl(treasuryControl, audit)
                            If resultSaveControl.StateResult = False Then
                                Throw New Exception(ResourceManager.GetString("DeleteTreasuryControlError", "Treasury"))
                            End If
                        End If
                    End If
                End If
                Me._treasuryNoteRepository.SaveEntity(treasuryNote)
                unitOfWork.Commit()
                scope.Complete()
                Dim auditProcess As New IndigoAuditSimpleEntity(Of TreasuryNote)(treasuryNote, audit, status, auxTreasuryNote)
                auditProcess.Execute()
            End Using

            If withConfirm Then
                Dim resultConfirm = ConfirmTreasuryNote(treasuryNote.Id, audit, treasuryNote)
                If resultConfirm.StateResult = False Then
                    unitOfWork.RollbackChangesUnitOfWork()
                    Return New ActionResult(Of TreasuryNote) With {.StateResult = True, .StateResultAux = False, .ObjectEmbbeded = treasuryNote, .MessageResult = resultConfirm.MessageResult, .Message = resultConfirm.Message}
                End If
                Dim messageResult As New List(Of String)
                messageResult.Add(resultConfirm.ObjectEmbbeded)
                If resultConfirm.MessageResult.Count > 0 Then
                    messageResult.Add(resultConfirm.MessageResult(0))
                End If
                Return New ActionResult(Of TreasuryNote) With {.StateResult = True, .StateResultAux = True, .ObjectEmbbeded = treasuryNote, .MessageResult = messageResult}
            Else
                Return New ActionResult(Of TreasuryNote) With {.StateResult = True, .StateResultAux = False, .ObjectEmbbeded = treasuryNote}
            End If

        Catch ex As OptimisticConcurrencyException
            unitOfWork.RollbackChanges()
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of TreasuryNote) With {.StateResult = False, .MessageResult = {"-999"}.ToList()}
        Catch ex As Exception
            unitOfWork.RollbackChanges()
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of TreasuryNote) With {.StateResult = False, .Message = ex.Message}
        End Try
    End Function

    ''' <summary>
    ''' Confirma una nota de tesoreria
    ''' </summary>
    Public Function ConfirmTreasuryNote(IdTreasuryNote As Integer, audit As AuditMessage, Optional treasuryNote As TreasuryNote = Nothing, Optional idSequence As Long = 0) As ActionResult(Of String) Implements ITreasuryNoteAdminService.ConfirmTreasuryNote
        If IdTreasuryNote = 0 Then
            Throw New ArgumentNullException("IdTreasuryNote")
        End If
        If audit Is Nothing Then
            Throw New ArgumentNullException("audit")
        End If

        Dim unitOfWork As IUnitWork = Me._treasuryNoteRepository.UnitWork
        Try

            If treasuryNote Is Nothing Then
                treasuryNote = _treasuryNoteRepository.GetTreasuryNoteById(IdTreasuryNote)
            End If
            Dim resultValidateVoucher As ActionResult = treasuryService.ValidateTreasuryNoteConfirm(treasuryNote)
            If Not resultValidateVoucher.StateResult Then
                Return New ActionResult(Of String) With {.StateResult = False, .Message = resultValidateVoucher.Message}
            End If

            Dim entityBankAccount As EntityBankAccounts
            Dim cashRegister As CashRegisters
            Dim mainAccounts As MainAccounts = Nothing

            Dim treasuryControl As TreasuryControl = _treasuryControlAdminService.GetTreasuryControlByDocumentNumber(treasuryNote.Code, 3)

            Using scope As New TransactionScope(TransactionScopeOption.Required, New TransactionOptions() With {.Timeout = TransactionManager.MaximumTimeout, .IsolationLevel = IsolationLevel.ReadCommitted})

                If treasuryControl IsNot Nothing AndAlso treasuryControl.Id > 0 Then
                    treasuryControl.MarkAsDeleted()
                    Dim resultSaveControl = _treasuryControlAdminService.DeleteTreasuryControl(treasuryControl, audit, False)
                    If resultSaveControl.StateResult = False Then
                        Return New ActionResult(Of String) With {.StateResult = False, .Message = ResourceManager.GetString("DeleteTreasuryControlError", "Treasury")}
                    End If
                End If

                Dim result As ActionResult = Nothing
                Dim resultDisconfirm As ActionResult(Of String) = Nothing
                Dim disconfirm As Boolean = False
                Dim consecutive As String = String.Empty

                Select Case treasuryNote.NoteType
                    Case eNoteType.EntityBankAccount, eNoteType.CardReconciliationExpenses
                        result = NoteEntityBankAccount(treasuryNote, audit)
                    Case eNoteType.CashRegister
                        result = NoteCashRegister(treasuryNote, audit)
                    Case eNoteType.NoteTypeUnConfirmVoucherTransaction
                        disconfirm = True
                        resultDisconfirm = NoteUnConfirmVoucherTransaction(treasuryNote, audit)
                    Case eNoteType.NoteTypeUnConfirmCashReceipt, eNoteType.DevolutionCashReceipt
                        disconfirm = True
                        resultDisconfirm = NoteUnConfirmCashReceipt(treasuryNote, audit)
                    Case eNoteType.NoteTypeUnConfirmConsignment
                        disconfirm = True
                        resultDisconfirm = NoteUnConfirmConsignment(treasuryNote, audit)
                    Case eNoteType.ReverseCrossingAccount
                        disconfirm = True
                        resultDisconfirm = NoteUnConfirmCrossAccount(treasuryNote, audit)
                    Case Else
                        result = New ActionResult With {.StateResult = False, .Message = "Opción no válida"}
                End Select
                If disconfirm Then
                    If Not resultDisconfirm.StateResult Then
                        unitOfWork.RollbackChanges()
                        scope.Dispose()
                        Return New ActionResult(Of String) With {.StateResult = False, .Message = resultDisconfirm.Message}
                    End If
                    consecutive = resultDisconfirm.ObjectEmbbeded
                Else
                    If Not result.StateResult Then
                        unitOfWork.RollbackChanges()
                        scope.Dispose()
                        Return New ActionResult(Of String) With {.StateResult = False, .Message = result.Message}
                    End If
                    'consecutive = resultDisconfirm.ObjectEmbbeded
                End If
                treasuryNote.Status = 2
                Dim settingTreasury As SettingsTreasury = Nothing

                If Not disconfirm Then
                    If (treasuryNote.EntityBankAccountId IsNot Nothing AndAlso treasuryNote.EntityBankAccountId <> 0) OrElse (treasuryNote.CashRegisterId IsNot Nothing AndAlso treasuryNote.CashRegisterId <> 0) Then
                        'Comprobante contable
                        settingTreasury = _settingsTreasuryRepository.GetSettingsTreasuryByIdUnitOperative(treasuryNote.OperatingUnitId)

                        If settingTreasury.Id > 0 Then
                            Dim accounting As New Domain.Entities.JournalVouchers()
                            With accounting
                                .IdJournalVoucher = settingTreasury.JournalVoucherTypeTreasuryNotes
                                .VoucherDate = treasuryNote.NoteDate
                                .Status = treasuryNote.Status
                                .Detail = treasuryNote.Description
                                .EntityCode = treasuryNote.Code
                                .EntityId = treasuryNote.Id
                                .EntityName = GetType(TreasuryNote).Name
                                .IsClosedYear = False
                                .BookCurrencyId = treasuryNote.CurrencyId
                                For Each detail As TreasuryNoteDetail In treasuryNote.TreasuryNoteDetail
                                    Dim accountDetail As New JournalVoucherDetails
                                    With accountDetail
                                        .IdMainAccount = detail.MainAccountId
                                        mainAccounts = _mainAccountRepository.GetAccountById(.IdMainAccount, False)
                                        .IdThirdParty = IIf(mainAccounts.HandlesThirdParty, detail.ThirdPartyId, Nothing)
                                        .IdCostCenter = IIf(mainAccounts.HandlesCostCenter, detail.CostCenterId, Nothing)
                                        If detail.Nature = eNature.Debit Then
                                            .DebitValue = detail.Value
                                        Else
                                            .CreditValue = detail.Value
                                        End If
                                        .Detail = ""
                                        .IdRetention = Nothing
                                        .RetentionRate = Nothing
                                    End With
                                    accounting.JournalVoucherDetails.Add(accountDetail)
                                Next

                                If treasuryNote.Value <> 0 Then
                                    Dim accountDetail As New JournalVoucherDetails
                                    With accountDetail
                                        .IdMainAccount = treasuryNote.MainAccountId
                                        mainAccounts = _mainAccountRepository.GetAccountById(.IdMainAccount, False)

                                        If treasuryNote.NoteType = eNoteType.EntityBankAccount OrElse treasuryNote.NoteType = eNoteType.CardReconciliationExpenses Then
                                            entityBankAccount = _entityBankAccountRepository.GetEntityBankAccountById(treasuryNote.EntityBankAccountId)
                                            .IdThirdParty = IIf(mainAccounts.HandlesThirdParty, entityBankAccount.ThirdPartyId, Nothing)
                                            .IdCostCenter = IIf(mainAccounts.HandlesCostCenter, treasuryNote.CostCenterId, Nothing)
                                        ElseIf treasuryNote.NoteType = eNoteType.CashRegister Then
                                            cashRegister = _cashRegisterRepository.GetCashRegisterById(treasuryNote.CashRegisterId)
                                            .IdThirdParty = IIf(mainAccounts.HandlesThirdParty, cashRegister.ThirdPartyId, Nothing)
                                            .IdCostCenter = IIf(mainAccounts.HandlesCostCenter, treasuryNote.CostCenterId, Nothing)
                                        End If
                                        If treasuryNote.Nature = eNature.Debit Then
                                            .DebitValue = treasuryNote.Value
                                        Else
                                            .CreditValue = treasuryNote.Value
                                        End If
                                        .Detail = treasuryNote.Description
                                        .IdRetention = Nothing
                                    End With
                                    accounting.JournalVoucherDetails.Add(accountDetail)
                                End If
                                Dim resultAccounting As ActionMessageResult(Of Domain.Entities.JournalVouchers)
                                resultAccounting = _accountingAdminService.SaveAccountingDocument(accounting, audit, False)
                                If resultAccounting.StateResult = False Then
                                    scope.Dispose()
                                    Return New ActionResult(Of String) With {.StateResult = False, .Message = resultAccounting.Message}
                                Else
                                    consecutive = resultAccounting.ObjectEmbbeded.Consecutive
                                End If
                            End With
                        Else
                            unitOfWork.RollbackChangesUnitOfWork()
                            scope.Dispose()
                            Return New ActionResult(Of String) With {.StateResult = False, .MessageResult = {"CE0006"}.ToList(), .Message = ResourceManager.GetString("SettingTreasuryNotExist", "Treasury")}
                        End If
                    End If
                End If

                treasuryNote.ModificationDate = Date.Now
                treasuryNote.ModificationUser = audit.CodeUser
                treasuryNote.ConfirmationDate = Date.Now
                treasuryNote.ConfirmationUser = audit.CodeUser

                Me._treasuryNoteRepository.SaveEntity(treasuryNote)
                unitOfWork.Commit()

                Dim status As Integer = Infrastructure.CrossCutting.Audit.Actions.Confirm
                Dim auditProcess As New IndigoAuditSimpleEntity(Of TreasuryNote)(treasuryNote, audit, status, treasuryNote.OriginalValue)
                auditProcess.Execute()
                scope.Complete()

                Dim messageResult As New List(Of String)
                If disconfirm Then
                    messageResult.Add(resultDisconfirm.Message)
                Else
                    messageResult.Add(settingTreasury.JournalVoucherTypeTreasuryNotes.ToString())
                End If
                Return New ActionResult(Of String) With {.StateResult = True, .ObjectEmbbeded = consecutive, .MessageResult = messageResult}

            End Using
        Catch ex As OptimisticConcurrencyException
            unitOfWork.RollbackChanges()
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of String) With {.StateResult = False, .MessageResult = {"-999"}.ToList(), .Message = IndigoManagementExceptions.GetExceptionDetails(ex)}
        Catch ex As Exception
            unitOfWork.RollbackChanges()
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of String) With {.StateResult = False, .MessageResult = {"-000"}.ToList(), .Message = IndigoManagementExceptions.GetExceptionDetails(ex)}
        End Try

    End Function

    ''' <summary>
    ''' Nota de tesoreria para cuentas bancarias
    ''' </summary>
    ''' <param name="treasuryNote">The treasury note.</param>
    Private Function NoteEntityBankAccount(ByVal treasuryNote As TreasuryNote, ByVal audit As AuditMessage) As ActionResult
        Dim _entityBankAccount As EntityBankAccounts = _entityBankAccountRepository.GetEntityBankAccountById(treasuryNote.EntityBankAccountId)
        Dim _treasuryBalance As New TreasuryBalance()
        With _entityBankAccount
            With _treasuryBalance
                .DocumentNumber = treasuryNote.Code
                .DocumentDate = treasuryNote.NoteDate
                .DocumentType = 4 'Notas
                .PreviousBalance = _entityBankAccount.CurrentBalance
                .ValueMovement = treasuryNote.Value
                .CreationDate = Date.Now
            End With
            If treasuryNote.Nature = eNature.Debit Then
                _treasuryBalance.Nature = 1
                .CurrentBalance += treasuryNote.Value
            Else
                _treasuryBalance.Nature = 2
                .CurrentBalance -= treasuryNote.Value
            End If
            .TreasuryBalance.Add(_treasuryBalance)
        End With
        Dim resultEntityAccount = _entityBankAccountAdminService.SaveEntityBankAccount(_entityBankAccount, audit, 0, False)
        If resultEntityAccount.StateResult Then
            Return New ActionResult With {.StateResult = True}
        Else
            Return New ActionResult With {.StateResult = False, .Message = ResourceManager.GetString("BalanceUpdateErrorEntityAccount", "Treasury")}
        End If
        '_entityBankAccountRepository.SaveEntity(_entityBankAccount)
    End Function

    ''' <summary>
    ''' Nota de tesoreria para cajas
    ''' </summary>
    ''' <param name="treasuryNote">The treasury note.</param>
    ''' <param name="audit">The audit.</param>
    ''' <returns></returns>
    Private Function NoteCashRegister(treasuryNote As TreasuryNote, audit As AuditMessage) As ActionResult
        Dim _cashRegister As CashRegisters = _cashRegisterRepository.GetCashRegisterById(treasuryNote.CashRegisterId)
        Dim _treasuryBalance As New TreasuryBalance()
        With _cashRegister
            With _treasuryBalance
                .DocumentNumber = treasuryNote.Code
                .DocumentDate = treasuryNote.NoteDate
                .DocumentType = 4 'Notas
                .PreviousBalance = _cashRegister.CurrentBalance
                .ValueMovement = treasuryNote.Value
                .CreationDate = Date.Now
            End With
            If treasuryNote.Nature = eNature.Debit Then
                _treasuryBalance.Nature = 1
                .CurrentBalance += treasuryNote.Value
            Else
                _treasuryBalance.Nature = 2
                .CurrentBalance -= treasuryNote.Value
            End If
            .TreasuryBalance.Add(_treasuryBalance)
        End With
        Dim resultCashRegister = _cashRegisterAdminService.SaveCashRegister(_cashRegister, audit, 0, False)
        If resultCashRegister.StateResult Then
            Return New ActionResult With {.StateResult = True}
        Else
            Return New ActionResult With {.StateResult = False, .Message = String.Concat(ResourceManager.GetString("BalanceUpdateErrorCashRegister", "Treasury"), _cashRegister.Code)}
        End If
    End Function

    ''' <summary>
    ''' Nota de tesoreria para reversar comprobante de egreso
    ''' </summary>
    ''' <param name="treasuryNote">The treasury note.</param>
    ''' <param name="audit">The audit.</param>
    Private Function NoteUnConfirmVoucherTransaction(treasuryNote As TreasuryNote, audit As AuditMessage) As ActionResult(Of String)
        Return _voucherTransactionAdminService.DisconfirmVoucherTransaction(treasuryNote, audit)
    End Function

    ''' <summary>
    ''' Notes the un confirm cash receipt.
    ''' </summary>
    ''' <param name="treasuryNote">The treasury note.</param>
    ''' <param name="audit">The audit.</param>
    ''' <returns></returns>
    Private Function NoteUnConfirmCashReceipt(treasuryNote As TreasuryNote, audit As AuditMessage) As ActionResult(Of String)
        Return _cashReceiptAdminService.ReverseCashReceipt(treasuryNote.CashReceiptId, audit, treasuryNote)
    End Function

    Private Function NoteUnConfirmConsignment(treasuryNote As TreasuryNote, audit As AuditMessage) As ActionResult(Of String)
        Return _consignmentAdminService.ReverseConsignment(treasuryNote, audit)
    End Function

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="treasuryNote"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    Private Function NoteUnConfirmCrossAccount(treasuryNote As TreasuryNote, audit As AuditMessage) As ActionResult(Of String)
        Return _crossingAccountAdminService.ReverseCrossingAccount(treasuryNote, audit)
    End Function

#End Region

#Region "Enums"
    ''' <summary>
    ''' enumeracion de tipo de nota
    ''' </summary>
    Public Enum eNoteType
        EntityBankAccount = 1
        CashRegister = 2
        NoteTypeUnConfirmVoucherTransaction = 3
        NoteTypeUnConfirmCashReceipt = 4
        NoteTypeUnConfirmConsignment = 5
        ReverseCrossingAccount = 6
        DevolutionCashReceipt = 7
        CardReconciliationExpenses = 8
    End Enum

    ''' <summary>
    ''' enumeración de naturaleza
    ''' </summary>
    Public Enum eNature
        Debit = 1
        Credit = 2
    End Enum
#End Region

#Region "IDisposable Support"
    Private disposedValue As Boolean ' Para detectar llamadas redundantes

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not disposedValue Then
            If disposing Then
                treasuryService.Dispose()
                _entityBankAccountAdminService.Dispose()
                _cashRegisterAdminService.Dispose()
                _accountingAdminService.Dispose()
                _voucherTransactionAdminService.Dispose()
                _treasuryControlAdminService.Dispose()
                _cashReceiptAdminService.Dispose()
            End If
            _treasuryNoteRepository = Nothing
            treasuryService = Nothing
            _secuenseDRepository = Nothing
            _mainAccountRepository = Nothing
            _entityBankAccountRepository = Nothing
            _cashRegisterRepository = Nothing
            _entityBankAccountAdminService = Nothing
            _cashRegisterAdminService = Nothing
            _accountingAdminService = Nothing
            _settingsTreasuryRepository = Nothing
            _voucherTransactionAdminService = Nothing
            _documentTypeRepostiry = Nothing
            _voucherTransactionRepository = Nothing
            _refundRepository = Nothing
            _treasuryControlAdminService = Nothing
            _cashReceiptAdminService = Nothing
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
