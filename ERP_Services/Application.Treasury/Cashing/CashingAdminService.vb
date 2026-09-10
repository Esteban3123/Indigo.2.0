'***********************************************************************
' Assembly         : Application.Treasury
' Author           : Diego Andrés Roldán Lozano
' Created          : 28-10-2014
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
Imports System.Transactions
Imports Infrastructure.CrossCutting.Resources

Public Class CashingAdminService
    Implements ICashingAdminService

#Region "Fields"

    Private Const FORM_NAME As String = "Cambio de Cheques"


    ''' <summary>
    ''' Repositorio de chequeras
    ''' </summary>
    Private _cashingRepository As ICashingRepository

    ''' <summary>
    ''' repositorio de cancelacion de cheques
    ''' </summary>
    Private _cancellationCheckRepository As ICancellationCheckRepository

    ''' <summary>
    ''' The _cancellation check admin service
    ''' </summary>
    Private _cancellationCheckAdminService As ICancellationCheckAdminService

    ''' <summary>
    ''' The _entity bank account repository
    ''' </summary>
    Dim _entityBankAccountRepository As IEntityBankAccountRepository

    ''' <summary>
    ''' The _voucher transactionrepository
    ''' </summary>
    Private _voucherTransactionrepository As IVoucherTransactionRepository

    ''' <summary>
    ''' repositorio de las secuencias numericas
    ''' </summary>
    Private _sequenceDRepository As ISequenseTreasuryDRepository

    ''' <summary>
    ''' servicios de aplicacion de chequeras
    ''' </summary>
    Private _checkBookAdminService As ICheckAdminService

    Private _checkBlockRepository As ICheckBlockRepository

    Private _outstandingCheckRepository As IOutstandingChecksRepository
#End Region

#Region "Methods"

    Public Sub New(ByVal cashingRepository As ICashingRepository, ByVal sequenceDRepository As ISequenseTreasuryDRepository, ByVal cancellationCheckRepository As ICancellationCheckRepository,
                   ByVal cancellationCheckAdminService As ICancellationCheckAdminService, ByVal voucherTransactionrepository As IVoucherTransactionRepository,
                   ByVal entityBankAccountRepository As IEntityBankAccountRepository, ByVal checkBookAdminService As ICheckAdminService, ByVal checkBlockRepository As ICheckBlockRepository,
                   ByVal outstandingCheckRepository As IOutstandingChecksRepository)
        If cashingRepository Is Nothing Then
            Throw New ArgumentNullException("cashingRepository")
        End If
        If entityBankAccountRepository Is Nothing Then
            Throw New ArgumentNullException("entityBankAccountRepository")
        End If
        If voucherTransactionrepository Is Nothing Then
            Throw New ArgumentNullException("voucherTransactionrepository")
        End If
        If cancellationCheckAdminService Is Nothing Then
            Throw New ArgumentNullException("cancellationCheckAdminService")
        End If
        If cancellationCheckRepository Is Nothing Then
            Throw New ArgumentNullException("cancellationCheckRepository")
        End If
        If sequenceDRepository Is Nothing Then
            Throw New ArgumentNullException("sequenceDRepository")
        End If
        _cashingRepository = cashingRepository
        _sequenceDRepository = sequenceDRepository
        _cancellationCheckRepository = cancellationCheckRepository
        _cancellationCheckAdminService = cancellationCheckAdminService
        _voucherTransactionrepository = voucherTransactionrepository
        _entityBankAccountRepository = entityBankAccountRepository
        _checkBookAdminService = checkBookAdminService
        _checkBlockRepository = checkBlockRepository
        _outstandingCheckRepository = outstandingCheckRepository
    End Sub

    ''' <summary>
    ''' Elimina un registro de cambio de cheque
    ''' </summary>
    Public Function DeleteCashing(checkCashing As CheckCashing, audit As AuditMessage) As ActionResult Implements ICashingAdminService.DeleteCashing
        If checkCashing Is Nothing Then
            Throw New ArgumentNullException("checkCashing")
        End If
        Dim unitOfWork As IUnitWork = Me._cashingRepository.UnitWork
        Try
            Using scope As New TransactionScope(TransactionScopeOption.Required, New TransactionOptions() With {.Timeout = TransactionManager.MaximumTimeout, .IsolationLevel = IsolationLevel.ReadCommitted})
                checkCashing.ModificationUser = audit.CodeUser
                checkCashing.ModificationDate = Date.Now
                Dim status As Integer = Infrastructure.CrossCutting.Audit.Actions.Delete
                Dim auditProcess As New IndigoAuditSimpleEntity(Of CheckCashing)(checkCashing, audit, status)
                checkCashing.MarkAsDeleted()
                Me._cashingRepository.SaveEntity(checkCashing)
                unitOfWork.Commit()
                auditProcess.Execute()
                scope.Complete()
                Return New ActionResult With {.StateResult = True, .StatusCode = eStatusResult.SUCCESS, .Message = ResourceManager.GetString("RecordDeleted")}
            End Using
            'If checkCashing.ChangeTracker.State = ObjectState.Deleted Then
            '    Dim status As Integer = Infrastructure.CrossCutting.Audit.Actions.Delete
            '    Dim auditProcess As New IndigoAuditSimpleEntity(Of CheckCashing)(checkCashing, audit, status)
            '    Me._cashingRepository.DeleteEntity(checkCashing)
            '    unitOfWork.Commit()
            '    auditProcess.Execute()
            '    Return New ActionResult With {.StateResult = True}
            'Else
            '    Return New ActionResult With {.StateResult = False, .MessageResult = New List(Of String)({"-000"})}
            'End If
        Catch ex As OptimisticConcurrencyException
            unitOfWork.RollbackChanges()
            Return New ActionResult With {.StateResult = False, .StatusCode = eStatusResult.WARNING, .MessageResult = New List(Of String)({"-999"}), .Message = ResourceManager.GetString("ErrorConcurrence")}
        Catch ex As UpdateException
            unitOfWork.RollbackChanges()
            Return New ActionResult With {.StateResult = False, .StatusCode = eStatusResult.WARNING, .MessageResult = New List(Of String)({"-000"}), .Message = ResourceManager.GetString("ErrorDependence")}
        Catch ex As DbUpdateException
            unitOfWork.RollbackChanges()
            Return New ActionResult With {.StateResult = False, .StatusCode = eStatusResult.WARNING, .MessageResult = New List(Of String)({"-000"}), .Message = ResourceManager.GetString("ErrorDependence")}
        Catch ex As Exception
            unitOfWork.RollbackChanges()
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult With {.StateResult = False, .StatusCode = eStatusResult.EXCEPTION, .Message = IndigoManagementExceptions.GetExceptionDetails(ex)}
        End Try
    End Function

    ''' <summary>
    ''' Obtiene un registro de cambio de cheque por codigo
    ''' </summary>
    Public Function GetCashing(code As String, audit As AuditMessage) As ActionResult(Of CheckCashing) Implements ICashingAdminService.GetCashing
        If String.IsNullOrEmpty(code) Then
            Throw New ArgumentNullException("code")
        End If
        Try
            Dim cashing As CheckCashing = Me._cashingRepository.GetCashing(code.Trim())
            If cashing IsNot Nothing AndAlso cashing.Id > 0 Then
                Dim status As Integer = Infrastructure.CrossCutting.Audit.Actions.Print
                Dim auditProcess As New IndigoAuditSimpleEntity(Of CheckCashing)(cashing, audit, status)
                auditProcess.Execute()
            End If
            Return New ActionResult(Of CheckCashing) With {.StateResult = True, .ObjectEmbbeded = cashing}
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of CheckCashing) With {.StateResult = False, .Message = ""}
        End Try
    End Function

    ''' <summary>
    ''' Obtiene un registro de cambio de cheque por id
    ''' </summary>
    Public Function GetCashingById(id As Integer, tracking As Boolean) As CheckCashing Implements ICashingAdminService.GetCashingById
        If id = 0 Then
            Throw New ArgumentNullException("id")
        End If
        Try
            Return Me._cashingRepository.GetCashingById(id, tracking)
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return Nothing
        End Try
    End Function

    ''' <summary>
    ''' Guarda un registro de cambio de cheque
    ''' </summary>
    Public Function SaveCashing(checkCashing As CheckCashing, audit As AuditMessage, Optional ByVal idSequence As Int64 = 0) As ActionResult(Of CheckCashing) Implements ICashingAdminService.SaveCashing
        If checkCashing Is Nothing Then
            Throw New ArgumentNullException("checkCashing")
        End If
        Dim unitOfWork As IUnitWork = Me._cashingRepository.UnitWork
        Dim unitOfWorkCancellation As IUnitWork = Me._cancellationCheckRepository.UnitWork
        Try
            Using scope As New TransactionScope(TransactionScopeOption.Required, New TransactionOptions() With {.Timeout = TransactionManager.MaximumTimeout, .IsolationLevel = IsolationLevel.ReadCommitted})
                Dim cancellationCheck As CancellationChecks = checkCashing.CancellationChecks
                cancellationCheck.CancellationDate = Date.Now
                Dim _result = _cancellationCheckAdminService.SaveCancellationCheck(cancellationCheck, audit, False)
                If _result.StateResult Then
                    Dim previusCheckNumber As Long
                    Dim _voucherTransaction As VoucherTransaction = _voucherTransactionrepository.GetVoucherTransactionById(checkCashing.VoucherTransactionId)
                    previusCheckNumber = _voucherTransaction.CheckNumber
                    _voucherTransaction.CheckNumber = checkCashing.NextCheckNumber
                    _voucherTransaction.ModificationDate = Date.Now
                    _voucherTransaction.ModificationUser = audit.CodeUser
                    _voucherTransactionrepository.SaveEntity(_voucherTransaction)

                    ''''''''''''''''''''''
                    '''' Validaciones'''''
                    ''''''''''''''''''''''
                    Dim checkBook = _checkBookAdminService.GetCheckByIdEntityBankAccountAndStatus(cancellationCheck.IdEntityAccount, 1, audit).ObjectEmbbeded '1: Estado activo

                    '1-Revisar que el cheque se encuentre dentro del rango de los cheques activos
                    If checkCashing.NextCheckNumber < checkBook.CurrentNumber OrElse checkCashing.NextCheckNumber > checkBook.EndNumber Then
                        unitOfWork.RollbackChangesUnitOfWork()
                        scope.Dispose()
                        Return New ActionResult(Of CheckCashing) With {.StateResult = False, .StatusCode = eStatusResult.WARNING, .Message = String.Format("El cheque {0} no se encuentra dentro del rango activo de la chequera", checkCashing.NextCheckNumber)}
                    End If

                    Dim _AnnulatedCheck As CancellationChecks = _cancellationCheckRepository.GetCancellationCheckByCheckBookIdAndCheckNumber(checkBook.Id, checkCashing.NextCheckNumber)
                    If _AnnulatedCheck IsNot Nothing AndAlso _AnnulatedCheck.Id > 0 Then
                        unitOfWork.RollbackChangesUnitOfWork()
                        scope.Dispose()
                        Return New ActionResult(Of CheckCashing) With {.StateResult = False, .StatusCode = eStatusResult.WARNING, .Message = String.Format(ResourceManager.GetString("CheckAnnulated", "Treasury"), checkCashing.NextCheckNumber)}
                    End If

                    Dim _oustanding As OutstandingChecks = _outstandingCheckRepository.GetOutstandingCheckByCheckBookIdAndCheckNumber(checkBook.Id, checkCashing.NextCheckNumber)
                    If _oustanding IsNot Nothing AndAlso _oustanding.Id > 0 Then
                        _outstandingCheckRepository.DeleteEntity(_oustanding)
                    End If

                    Dim _checkBlock As CheckBlock = _checkBlockRepository.GetCheckBlockByIdCheckBookAndNumber(checkBook.Id, checkCashing.NextCheckNumber)
                    If _checkBlock IsNot Nothing AndAlso _checkBlock.Id > 0 Then
                        unitOfWork.RollbackChangesUnitOfWork()
                        scope.Dispose()
                        Return New ActionResult(Of CheckCashing) With {.StateResult = False, .StatusCode = eStatusResult.WARNING, .Message = String.Format("El cheque ({0}) se encuentra bloqueado, seleccione otro", checkCashing.NextCheckNumber)}
                    End If
                    If _voucherTransaction.Status = 1 Then
                        _checkBlock = New CheckBlock() With {.IdCheckbook = checkBook.Id, .CodUser = audit.CodeUser, .CheckNumber = checkCashing.NextCheckNumber}
                        _checkBlockRepository.SaveEntity(_checkBlock)
                    End If
                    _checkBlock = _checkBlockRepository.GetCheckBlockByIdCheckBookAndNumber(checkBook.Id, previusCheckNumber)
                    If _checkBlock IsNot Nothing AndAlso _checkBlock.Id > 0 Then
                        _checkBlockRepository.DeleteEntity(_checkBlock)
                    End If

                    If checkCashing.NextCheckNumber = checkBook.EndNumber Then
                        'Cambiar estado de la chequera a 3 - Finalizada
                        checkBook.Status = 3
                    Else
                        'aumentar hasta q encuentre un cheque que no este bloqueado ni anulado
                        For numberCheck As Integer = checkCashing.NextCheckNumber + 1 To checkBook.EndNumber Step 1
                            Dim _checkBlock2 As CheckBlock = _checkBlockRepository.GetCheckBlockByIdCheckBookAndNumber(checkBook.Id, numberCheck)
                            Dim _annulatedCheck2 As CancellationChecks = _cancellationCheckRepository.GetCancellationCheckByCheckBookIdAndCheckNumber(checkBook.Id, Convert.ToInt64(numberCheck))
                            Dim _oustanding2 As OutstandingChecks = _outstandingCheckRepository.GetOutstandingCheckByCheckBookIdAndCheckNumber(checkBook.Id, numberCheck)
                            If (_checkBlock2 Is Nothing OrElse _checkBlock2.Id = 0) AndAlso (_annulatedCheck2 Is Nothing OrElse _annulatedCheck2.Id = 0) AndAlso (_oustanding2 Is Nothing OrElse _oustanding2.Id = 0) Then
                                checkBook.CurrentNumber = numberCheck
                                Exit For
                            End If
                        Next
                    End If

                    Dim MessageResult As String = String.Empty

                    If String.IsNullOrEmpty(checkCashing.Code) Then
                        Dim seq As TreasurySequenceDetail = Me._sequenceDRepository.GetSequenseDById(idSequence)
                        If seq IsNot Nothing AndAlso seq.Id > 0 AndAlso seq.TreasurySequence.Sequential Then
                            Dim res = Infrastructure.CrossCutting.Base.Sequense.GetSequense(seq.Sequense.Pattern, seq.Next)
                            If res IsNot Nothing AndAlso Not res.Equals(Infrastructure.CrossCutting.Base.Sequense.ERROR_MAXVALUE) Then
                                checkCashing.Code = res
                                seq.Next += 1
                                Me._sequenceDRepository.SaveEntity(seq)
                            Else
                                scope.Dispose()
                                Return New ActionResult(Of CheckCashing) With {.StatusCode = eStatusResult.WARNING, .StateResult = False, .MessageResult = {"_Seq02_"}.ToList(), .Message = String.Format(ResourceManager.GetString("SequenceFormNotFound"), FORM_NAME)}
                            End If
                            MessageResult = If(seq.TreasurySequence.Sequential, String.Format(ResourceManager.GetString("SavedWithCode"), checkCashing.Code), ResourceManager.GetString("SaveMessage"))
                        Else
                            scope.Dispose()
                            Return New ActionResult(Of CheckCashing) With {.StatusCode = eStatusResult.WARNING, .StateResult = False, .MessageResult = {"_Seq02_"}.ToList(), .Message = String.Format(ResourceManager.GetString("SequenceFormNotFound"), FORM_NAME)}
                        End If
                    Else
                        MessageResult = ResourceManager.GetString("SaveMessage")
                    End If

                    Dim auxCashing As CheckCashing = Nothing
                    Dim status As Integer
                    If checkCashing.ChangeTracker.State = Domain.Base.Entities.ObjectState.Added Then
                        checkCashing.CreationDate = Date.Now
                        checkCashing.CreationUser = audit.CodeUser
                        status = Infrastructure.CrossCutting.Audit.Actions.Insert
                    Else
                        MessageResult = ResourceManager.GetString("UpdateMessage")
                        checkCashing.ModificationDate = Date.Now
                        checkCashing.ModificationUser = audit.CodeUser
                        status = Infrastructure.CrossCutting.Audit.Actions.Update
                        auxCashing = checkCashing.OriginalValue
                    End If
                    checkCashing.CancellationCheckId = _result.ObjectEmbbeded.Id
                    Me._cashingRepository.SaveEntity(checkCashing)
                    unitOfWork.Commit()
                    Dim auditProcess As New IndigoAuditSimpleEntity(Of CheckCashing)(checkCashing, audit, status, auxCashing)
                    auditProcess.Execute()
                    scope.Complete()
                    Return New ActionResult(Of CheckCashing) With {.StatusCode = eStatusResult.SUCCESS, .StateResult = True, .ObjectEmbbeded = checkCashing, .Message = MessageResult}
                Else
                    scope.Dispose()
                    Return New ActionResult(Of CheckCashing) With {.StateResult = False, .StatusCode = eStatusResult.WARNING, .Message = _result.Message}
                End If
            End Using

        Catch ex As OptimisticConcurrencyException
            unitOfWork.RollbackChanges()
            Return New ActionResult(Of CheckCashing) With {.StateResult = False, .StatusCode = eStatusResult.WARNING, .MessageResult = {"-999"}.ToList(), .Message = ResourceManager.GetString("ErrorConcurrence")}
        Catch ex As Exception
            unitOfWork.RollbackChanges()
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of CheckCashing) With {.StateResult = False, .StatusCode = eStatusResult.EXCEPTION, .MessageResult = {ex.Message}.ToList, .Message = IndigoManagementExceptions.GetExceptionDetails(ex)}
        End Try
    End Function

#End Region

#Region "IDisposable Support"
    Private disposedValue As Boolean ' Para detectar llamadas redundantes

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not disposedValue Then
            If disposing Then
                _cancellationCheckAdminService.Dispose()
                _checkBookAdminService.Dispose()
            End If
            _cashingRepository = Nothing
            _sequenceDRepository = Nothing
            _cancellationCheckRepository = Nothing
            _cancellationCheckAdminService = Nothing
            _voucherTransactionrepository = Nothing
            _entityBankAccountRepository = Nothing
            _checkBookAdminService = Nothing
            _checkBlockRepository = Nothing
            _outstandingCheckRepository = Nothing
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