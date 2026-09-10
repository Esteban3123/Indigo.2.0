'***********************************************************************
' Assembly         : Application.Treasury
' Author           : Diego Andrés Roldán Lozano
' Created          : 07-10-2014
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
Imports System.Text
Imports System.Data.Entity.Validation
Imports Application.Payments
Imports Application.Portfolio
Imports Application.Accounting

#End Region

Public Class ConsignmentTransferAdminService
    Implements IConsignmentTransferAdminService

#Region "Fields"

    Private Const MODULE_NAME As String = "Treasury"
    ''' <summary>
    ''' Repositorio de consignaciones / traslados
    ''' </summary>
    Private _consignmentTransferRepository As IConsignmentTransferRepository
    ''' <summary>
    ''' repositorio de las secuencias
    ''' </summary>
    Private _secuenseDRepository As ISequenseTreasuryDRepository
    ''' <summary>
    ''' repositorio de Cajas
    ''' </summary>
    Private _cashRegisterRepository As ICashRegisterRepository
    ''' <summary>
    ''' Servicios de aplicacion de cajas
    ''' </summary>
    Private _cashRegisterAdminService As ICashRegisterAdminService
    ''' <summary>
    ''' Servicios de aplicacion de cuentas bancarias
    ''' </summary>
    Private _entityAccountAdminService As IEntityBankAccountAdminService
    ''' <summary>
    ''' repositorio de parametros de tesoreria
    ''' </summary>
    Private _settingsTreasuryRepository As ISettingsTreasuryRepository
    ''' <summary>
    ''' servicios de aplicacion de contabilidad
    ''' </summary>
    Private _accountingAdminService As IAccountingDocumentAdminService

    Private treasuryService As ITreasuryServices

    Private _mainAccountsRepository As IPUCRepository

    Private _entityAccountsRepository As IEntityBankAccountRepository

    Private _treasuryControlAdminService As ITreasuryControlAdminService

#End Region

#Region "Methods"
    Public Sub New(ByVal secuenseDRepository As ISequenseTreasuryDRepository, ByVal consignmentTransferRepository As IConsignmentTransferRepository,
                   ByVal cashRegisterRepository As ICashRegisterRepository, ByVal cashRegisterAdminService As ICashRegisterAdminService,
                   ByVal entityAccountAdminService As IEntityBankAccountAdminService, ByVal settingsTreasuryRepository As ISettingsTreasuryRepository,
                   ByVal accountingAdminService As IAccountingDocumentAdminService, _treasuryService As ITreasuryServices, mainAccountsRepository As IPUCRepository,
                   entityAccountsRepository As IEntityBankAccountRepository, treasuryControlAdminService As ITreasuryControlAdminService)
        If secuenseDRepository Is Nothing Then
            Throw New ArgumentNullException("secuenseDRepository")
        End If
        If accountingAdminService Is Nothing Then
            Throw New ArgumentNullException("accountingAdminService")
        End If
        If settingsTreasuryRepository Is Nothing Then
            Throw New ArgumentNullException("settingsTreasuryRepository")
        End If
        If entityAccountAdminService Is Nothing Then
            Throw New ArgumentNullException("entityAccountAdminService")
        End If
        If cashRegisterAdminService Is Nothing Then
            Throw New ArgumentNullException("cashRegisterAdminService")
        End If
        If cashRegisterRepository Is Nothing Then
            Throw New ArgumentNullException("cashRegisterRepository")
        End If
        If consignmentTransferRepository Is Nothing Then
            Throw New ArgumentNullException("consignmentTransferRepository")
        End If
        _secuenseDRepository = secuenseDRepository
        _consignmentTransferRepository = consignmentTransferRepository
        _cashRegisterRepository = cashRegisterRepository
        _cashRegisterAdminService = cashRegisterAdminService
        _entityAccountAdminService = entityAccountAdminService
        _settingsTreasuryRepository = settingsTreasuryRepository
        _accountingAdminService = accountingAdminService
        treasuryService = _treasuryService
        _mainAccountsRepository = mainAccountsRepository
        _entityAccountsRepository = entityAccountsRepository
        _treasuryControlAdminService = treasuryControlAdminService
    End Sub

    ''' <summary>
    ''' Confirms the consignment transfer.
    ''' </summary>
    Public Function ConfirmConsignmentTransfer(consignmentId As Integer, audit As AuditMessage, Optional consignment As Consignment = Nothing) As ActionResult(Of String) Implements IConsignmentTransferAdminService.ConfirmConsignmentTransfer
        If consignmentId = 0 Then
            Throw New ArgumentNullException("consignmentId")
        End If

        Dim unitOfWork As IUnitWork = Me._consignmentTransferRepository.UnitWork
        Dim mainAccounts As MainAccounts = Nothing
        Dim cashRegister As CashRegisters = Nothing
        Try
            Dim errorList As New StringBuilder()
            If consignment Is Nothing Then
                consignment = Me.GetConsignmentTransferById(consignmentId, True)
            End If

            Dim resultValidateObject = treasuryService.ValidateConsignmentConfirm(consignment)
            If Not resultValidateObject.StateResult Then
                Return New ActionResult(Of String) With {.StateResult = False, .Message = resultValidateObject.Message}
            End If

            Dim treasuryControl As TreasuryControl = _treasuryControlAdminService.GetTreasuryControlByDocumentNumber(consignment.Code, 4)

            Using scope As New TransactionScope(TransactionScopeOption.Required, New TransactionOptions() With {.Timeout = TransactionManager.MaximumTimeout, .IsolationLevel = IsolationLevel.ReadCommitted})

                If treasuryControl IsNot Nothing AndAlso treasuryControl.Id > 0 Then
                    treasuryControl.MarkAsDeleted()
                    Dim resultSaveControl = _treasuryControlAdminService.DeleteTreasuryControl(treasuryControl, audit, False)
                    If resultSaveControl.StateResult = False Then
                        Return New ActionResult(Of String) With {.StateResult = False, .Message = ResourceManager.GetString("DeleteTreasuryControlError", "Treasury")}
                    End If
                End If

                consignment.Status = 2
                Dim consecutive As String = String.Empty
                'Afecta los saldos de la cuenta bancaria y la caja
                For Each consignmentDetail As ConsignmentDetail In consignment.ConsignmentDetail
                    cashRegister = _cashRegisterRepository.GetCashRegisterById(consignmentDetail.CashRegisterId)
                    Dim _treasuryBalance As New TreasuryBalance()
                    With cashRegister
                        With _treasuryBalance
                            .DocumentNumber = consignment.Code
                            .DocumentDate = consignment.DocumentDate
                            .DocumentType = 3 'Consignaciones
                            .Nature = 2 'Credito
                            .PreviousBalance = cashRegister.CurrentBalance
                            .ValueMovement = consignmentDetail.Value
                            .CreationDate = DateTime.Now
                        End With
                        .TreasuryBalance.Add(_treasuryBalance)
                        cashRegister.CurrentBalance -= consignmentDetail.Value
                    End With
                    Dim resultAffectCash As ActionResult(Of CashRegisters) = _cashRegisterAdminService.SaveCashRegister(cashRegister, audit)
                    If Not resultAffectCash.StateResult Then
                        unitOfWork.RollbackChangesUnitOfWork()
                        scope.Dispose()
                        Return New ActionResult(Of String) With {.StateResult = False, .Message = resultAffectCash.Message}
                    End If
                Next

                Dim entityBankAccount As EntityBankAccounts = _entityAccountAdminService.GetEntityBankAccountById(consignment.EntityBankAccountId, Nothing)
                Dim _treasuryBalanceEntity As New TreasuryBalance()
                With entityBankAccount
                    With _treasuryBalanceEntity
                        .DocumentNumber = consignment.Code
                        .DocumentDate = consignment.DocumentDate
                        .DocumentType = 3 'Consignaciones
                        .Nature = 1 'Debito
                        .PreviousBalance = entityBankAccount.CurrentBalance
                        .ValueMovement = consignment.Value
                        .CreationDate = DateTime.Now
                    End With
                    .TreasuryBalance.Add(_treasuryBalanceEntity)
                    .CurrentBalance += consignment.ConsignmentDetail.Sum(Function(x) x.ValueInCurrencyHeader)
                End With

                Dim resultAffectEntityAccount As ActionResult(Of EntityBankAccounts) = _entityAccountAdminService.SaveEntityBankAccount(entityBankAccount, audit)
                If Not resultAffectEntityAccount.StateResult Then
                    unitOfWork.RollbackChangesUnitOfWork()
                    scope.Dispose()
                    Return New ActionResult(Of String) With {.StateResult = False, .Message = resultAffectEntityAccount.Message}
                End If

                'Comprobante contable
                Dim settingTreasury As SettingsTreasury = _settingsTreasuryRepository.GetSettingsTreasuryByIdUnitOperative(consignment.OperativeUnitId)
                If settingTreasury.Id > 0 Then
                    Dim accounting As New Domain.Entities.JournalVouchers()
                    With accounting
                        .IdJournalVoucher = settingTreasury.JournalVoucherTypeBankAppropriations
                        .VoucherDate = consignment.DocumentDate
                        .Status = consignment.Status
                        .Detail = consignment.Description
                        .EntityCode = consignment.Code
                        .EntityId = consignment.Id
                        .EntityName = GetType(Consignment).Name
                        .IsClosedYear = False
                        .BookCurrencyId = consignment?.CurrencyId

                        Dim accountDetail As JournalVoucherDetails
                        For Each consignmentDetail As ConsignmentDetail In consignment.ConsignmentDetail
                            cashRegister = _cashRegisterRepository.GetCashRegisterById(consignmentDetail.CashRegisterId)

                            accountDetail = New JournalVoucherDetails()
                            With accountDetail
                                .IdMainAccount = consignmentDetail.MainAccountId
                                mainAccounts = _mainAccountsRepository.GetAccountById(.IdMainAccount, False)
                                .IdThirdParty = IIf(mainAccounts.HandlesThirdParty, cashRegister.ThirdPartyId, Nothing)
                                .IdCostCenter = IIf(mainAccounts.HandlesCostCenter, consignmentDetail.CostCenterId, Nothing)
                                .CreditValue = consignmentDetail.ValueInCurrencyHeader
                                .DebitValue = 0
                                .Detail = "Detalle de consignación valor " & .CreditValue
                                .IdRetention = Nothing
                                .RetentionRate = Nothing
                            End With
                            accounting.JournalVoucherDetails.Add(accountDetail)
                        Next

                        accountDetail = New JournalVoucherDetails()
                        With accountDetail
                            .IdMainAccount = consignment.MainAccountId
                            Dim entityAccount As EntityBankAccounts = _entityAccountsRepository.GetEntityBankAccountById(consignment.EntityBankAccountId)
                            mainAccounts = _mainAccountsRepository.GetAccountById(.IdMainAccount, False)
                            .IdThirdParty = IIf(mainAccounts.HandlesThirdParty, entityAccount.ThirdPartyId, Nothing)
                            .IdCostCenter = IIf(mainAccounts.HandlesCostCenter, consignment.CostCenterId, Nothing)
                            .CreditValue = 0
                            .DebitValue = consignment.Value
                            .Detail = consignment.Description
                            .IdRetention = Nothing
                        End With
                        accounting.JournalVoucherDetails.Add(accountDetail)

                        Dim resultAccounting As ActionMessageResult(Of Domain.Entities.JournalVouchers)
                        resultAccounting = _accountingAdminService.SaveAccountingDocument(accounting, audit)
                        If resultAccounting.StateResult = False Then
                            scope.Dispose()
                            'capturar el mensaje de error de contabilidad
                            Return New ActionResult(Of String) With {.StateResult = False, .Message = resultAccounting.Message}
                        Else
                            consecutive = resultAccounting.ObjectEmbbeded.Consecutive
                        End If
                    End With
                Else
                    scope.Dispose()
                    Return New ActionResult(Of String) With {.StateResult = False, .MessageResult = {"CE0006"}.ToList(), .Message = ResourceManager.GetString("SettingTreasuryNotExist", MODULE_NAME)}
                End If
                consignment.ModificationDate = Date.Now
                consignment.ModificationUser = audit.CodeUser
                consignment.ConfirmationDate = Date.Now
                consignment.ConfirmationUser = audit.CodeUser
                Dim status As Integer = Infrastructure.CrossCutting.Audit.Actions.Confirm
                Dim auditProcess As New IndigoAuditSimpleEntity(Of Consignment)(consignment, audit, status, consignment.OriginalValue)
                auditProcess.Execute()
                consignment.MarkAsUnchanged()
                scope.Complete()

                Dim messageResult As New List(Of String)
                messageResult.Add(settingTreasury.JournalVoucherTypeBankAppropriations.ToString())
                Return New ActionResult(Of String) With {.StateResult = True, .ObjectEmbbeded = consecutive, .MessageResult = messageResult}
            End Using
        Catch ex As OptimisticConcurrencyException
            unitOfWork.RollbackChangesUnitOfWork()
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of String) With {.StateResult = False, .MessageResult = {"-999"}.ToList()}
        Catch ex As DbEntityValidationException
            unitOfWork.RollbackChangesUnitOfWork()
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of String) With {.StateResult = False, .Message = ResourceManager.GetString("ErrorUnknown")}
        Catch ex As Exception
            unitOfWork.RollbackChangesUnitOfWork()
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of String) With {.StateResult = False, .Message = ResourceManager.GetString("ErrorUnknown")}
        End Try
    End Function

    ''' <summary>
    ''' Obtiene un registro de consignacion o traslado por codigo
    ''' </summary>
    Public Function GetConsignmentTransfer(code As String, audit As AuditMessage, Optional tracking As Boolean = False) As ActionResult(Of Consignment) Implements IConsignmentTransferAdminService.GetConsignmentTransfer
        If String.IsNullOrEmpty(code) Then
            Throw New ArgumentNullException("code")
        End If
        If audit Is Nothing Then
            Throw New ArgumentNullException("audit")
        End If
        Try
            Dim consignmentTransfer As Consignment = Me._consignmentTransferRepository.GetConsignmentTransfer(code.Trim(), tracking)
            If consignmentTransfer IsNot Nothing AndAlso consignmentTransfer.Id > 0 Then
                Dim status As Integer = Infrastructure.CrossCutting.Audit.Actions.Print
                Dim auditProcess As New IndigoAuditSimpleEntity(Of Consignment)(consignmentTransfer, audit, status)
                auditProcess.Execute()
            End If
            Return New ActionResult(Of Consignment) With {.StateResult = True, .ObjectEmbbeded = consignmentTransfer}
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of Consignment) With {.StateResult = False}
        End Try
    End Function

    ''' <summary>
    ''' Obtiene un registro de consignacion o traslado por id
    ''' </summary>
    Public Function GetConsignmentTransferById(Id As Integer, Optional tracking As Boolean = False) As Consignment Implements IConsignmentTransferAdminService.GetConsignmentTransferById
        Try
            Return _consignmentTransferRepository.GetConsignmentTransferById(Id, tracking)
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return Nothing
        End Try
    End Function

    ''' <summary>
    ''' Saves the consignment transfer.
    ''' </summary>
    Public Function SaveConsignmentTransfer(consignment As Consignment, audit As AuditMessage, withConfirm As Boolean, Optional idSequence As Long = 0) As ActionResult(Of Consignment) Implements IConsignmentTransferAdminService.SaveConsignmentTransfer
        If consignment Is Nothing Then
            Throw New ArgumentNullException("consignmentTransfer")
        End If
        Dim unitOfWork As IUnitWork = Me._consignmentTransferRepository.UnitWork
        Dim unitWorkSequence As IUnitWork = Me._secuenseDRepository.UnitWork

        Dim resultValidateVoucher As ActionResult(Of String) = treasuryService.ValidateConsignmentSave(consignment)
        If resultValidateVoucher.StateResult = False Then
            Return New ActionResult(Of Consignment) With {.StateResult = False, .Message = resultValidateVoucher.Message}
        End If

        Try
            Using scope As New TransactionScope(TransactionScopeOption.Required, New TransactionOptions() With {.Timeout = TransactionManager.MaximumTimeout, .IsolationLevel = IsolationLevel.ReadCommitted})
                If String.IsNullOrEmpty(consignment.Code) Then
                    Dim seq As TreasurySequenceDetail = _secuenseDRepository.GetSequenseDById(idSequence)
                    If seq IsNot Nothing AndAlso seq.Id > 0 AndAlso seq.TreasurySequence.Sequential Then
                        Dim res = Infrastructure.CrossCutting.Base.Sequense.GetSequense(seq.Sequense.Pattern, seq.Next)
                        If res IsNot Nothing AndAlso Not res.Equals(Infrastructure.CrossCutting.Base.Sequense.ERROR_MAXVALUE) Then
                            consignment.Code = res
                            seq.Next += 1
                            Me._secuenseDRepository.SaveEntity(seq)
                        Else
                            Return New ActionResult(Of Consignment) With {.StateResult = False, .Message = ResourceManager.GetString("SequenceNotFound")}
                        End If
                    Else
                        Return New ActionResult(Of Consignment) With {.StateResult = False, .Message = ResourceManager.GetString("SequenceNotFound")}
                    End If
                End If

                Dim auxConsignmentTransfer As Consignment = Nothing
                Dim state As Integer
                If consignment.ChangeTracker.State = ObjectState.Added Then
                    consignment.CreationDate = Date.Now
                    consignment.CreationUser = audit.CodeUser
                    state = Infrastructure.CrossCutting.Audit.Actions.Insert

                    Dim treasuryControl As New TreasuryControl() With {.DocumentNumber = consignment.Code, .DocumentType = 4, .DocumentUser = audit.CodeUser, .DocumentDate = consignment.DocumentDate}
                    Dim resultSaveControl = _treasuryControlAdminService.SaveTreasuryControl(treasuryControl, audit)
                    If resultSaveControl.StateResult = False Then
                        Return New ActionResult(Of Consignment) With {.StateResult = False, .Message = ResourceManager.GetString("SaveTreasuryControlError", "Treasury")}
                    End If

                ElseIf consignment.ChangeTracker.State = ObjectState.Modified Then
                    consignment.ModificationDate = Date.Now
                    consignment.ModificationUser = audit.CodeUser
                    state = Infrastructure.CrossCutting.Audit.Actions.Update
                    auxConsignmentTransfer = consignment.OriginalValue
                    If consignment.Status = 3 Then
                        state = Infrastructure.CrossCutting.Audit.Actions.Annular
                        consignment.AnnulmentDate = Date.Now
                        consignment.AnnulmentUser = audit.CodeUser

                        Dim treasuryControl As TreasuryControl = _treasuryControlAdminService.GetTreasuryControlByDocumentNumber(consignment.Code, 4)
                        If treasuryControl IsNot Nothing AndAlso treasuryControl.Id > 0 Then
                            treasuryControl.MarkAsDeleted()
                            Dim resultSaveControl = _treasuryControlAdminService.DeleteTreasuryControl(treasuryControl, audit)
                            If resultSaveControl.StateResult = False Then
                                Return New ActionResult(Of Consignment) With {.StateResult = False, .Message = ResourceManager.GetString("DeleteTreasuryControlError", "Treasury")}
                            End If
                        End If

                    End If
                End If

                Me._consignmentTransferRepository.SaveEntity(consignment)
                unitOfWork.Commit()
                unitWorkSequence.Commit()
                scope.Complete()
                Dim auditProcess As New IndigoAuditSimpleEntity(Of Consignment)(consignment, audit, state, auxConsignmentTransfer)
                auditProcess.Execute()
            End Using

            If withConfirm Then
                Dim resultConfirm = ConfirmConsignmentTransfer(consignment.Id, audit, consignment)
                If resultConfirm.StateResult = False Then
                    unitOfWork.RollbackChangesUnitOfWork()
                    Return New ActionResult(Of Consignment) With {.StateResult = True, .ObjectEmbbeded = consignment, .MessageResult = resultConfirm.MessageResult, .Message = resultConfirm.Message}
                End If
                Dim messageResult As New List(Of String)
                messageResult.Add(resultConfirm.ObjectEmbbeded)
                messageResult.Add(resultConfirm.MessageResult(0))
                Return New ActionResult(Of Consignment) With {.StateResult = True, .ObjectEmbbeded = consignment, .MessageResult = messageResult}
            Else
                Return New ActionResult(Of Consignment) With {.StateResult = True, .ObjectEmbbeded = consignment}
            End If

        Catch ex As OptimisticConcurrencyException
            unitOfWork.RollbackChangesUnitOfWork()
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of Consignment) With {.StateResult = False, .MessageResult = {"-999"}.ToList(), .Message = ResourceManager.GetString("ErrorUnknown")}
        Catch ex As DbUpdateException
            unitOfWork.RollbackChangesUnitOfWork()
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Dim MessageResult = ex.InnerException.InnerException
            Return New ActionResult(Of Consignment) With {.StateResult = False, .Message = MessageResult.Message}
        Catch ex As Exception
            unitOfWork.RollbackChangesUnitOfWork()
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of Consignment) With {.StateResult = False, .Message = ResourceManager.GetString("ErrorUnknown")}
        End Try
    End Function

    Private Function ReverseConsignment(treasuryNote As TreasuryNote, audit As AuditMessage) As ActionResult(Of String) Implements IConsignmentTransferAdminService.ReverseConsignment
        Dim txSettings As New TransactionOptions()
        txSettings.Timeout = TransactionManager.MaximumTimeout
        txSettings.IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted
        Using transaction As New TransactionScope(TransactionScopeOption.Required, txSettings)
            Try
                CType(_consignmentTransferRepository.UnitWork, IObjectContextAdapter).ObjectContext.CommandTimeout = 3600
                Dim result = _consignmentTransferRepository.SP_ReverseConsignment(treasuryNote.Id, audit.CodeUser).ToList().ElementAt(0)
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
#End Region

#Region "IDisposable Support"
    Private disposedValue As Boolean ' Para detectar llamadas redundantes

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not disposedValue Then
            If disposing Then
                _cashRegisterAdminService.Dispose()
                _entityAccountAdminService.Dispose()
                _accountingAdminService.Dispose()
                treasuryService.Dispose()
                _treasuryControlAdminService.Dispose()
            End If
            _secuenseDRepository = Nothing
            _consignmentTransferRepository = Nothing
            _cashRegisterRepository = Nothing
            _cashRegisterAdminService = Nothing
            _entityAccountAdminService = Nothing
            _settingsTreasuryRepository = Nothing
            _accountingAdminService = Nothing
            treasuryService = Nothing
            _mainAccountsRepository = Nothing
            _entityAccountsRepository = Nothing
            _treasuryControlAdminService = Nothing
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