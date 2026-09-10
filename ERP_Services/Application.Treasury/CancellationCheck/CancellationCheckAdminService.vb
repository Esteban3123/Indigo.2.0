'***********************************************************************
' Assembly         : Application.Treasury
' Author           : Diego Andrés Roldán Lozano
' Created          : 02-04-2014
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

Public Class CancellationCheckAdminService
    Implements ICancellationCheckAdminService

#Region "Fields"

    ''' <summary>
    ''' Repositorio de cancelacion de cheques
    ''' </summary>
    Private _cancellationCheckRepository As ICancellationCheckRepository

    ''' <summary>
    ''' servicios de aplicacion de chequeras
    ''' </summary>
    Private _checkAdminService As ICheckAdminService
    Private _outstandingChecksRepository As IOutstandingChecksRepository

#End Region

    Public Sub New(ByVal cancellationCheckRepository As ICancellationCheckRepository, ByVal checkAdminService As ICheckAdminService, outstandingChecksRepository As IOutstandingChecksRepository)
        If cancellationCheckRepository Is Nothing Then
            Throw New ArgumentNullException("cancellationCheckRepository")
        End If
        If checkAdminService Is Nothing Then
            Throw New ArgumentNullException("checkAdminService")
        End If
        _cancellationCheckRepository = cancellationCheckRepository
        _checkAdminService = checkAdminService
        _outstandingChecksRepository = outstandingChecksRepository
    End Sub

    ''' <summary>
    ''' Obtener un registro de cheque cancelado
    ''' </summary>
    ''' <param name="IdEntityAccount">The identifier entity account.</param>
    ''' <param name="CheckNumber">The check number.</param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    Public Function GetCancellationCheckByEntityAccountAndCheckNumber(IdEntityAccount As Integer, CheckNumber As String, audit As AuditMessage) As ActionResult(Of CancellationChecks) Implements ICancellationCheckAdminService.GetCancellationCheckByEntityAccountAndCheckNumber
        If String.IsNullOrEmpty(CheckNumber) Then
            Throw New ArgumentNullException("CheckNumber")
        End If
        If audit Is Nothing Then
            Throw New ArgumentNullException("audit")
        End If
        Try
            Dim cancellationCheck As CancellationChecks = Me._cancellationCheckRepository.GetCancellationCheckByEntityAccountAndCheckNumber(IdEntityAccount, CheckNumber)
            Dim auditProcess As New IndigoAuditSimpleEntity(Of CancellationChecks)(cancellationCheck, audit, Infrastructure.CrossCutting.Audit.Actions.Delete)
            auditProcess.Execute()
            Return New ActionResult(Of CancellationChecks) With {.StateResult = True, .ObjectEmbbeded = cancellationCheck}
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of CancellationChecks) With {.StateResult = False}
        End Try
    End Function

    ''' <summary>
    ''' Saves the cancellation check.
    ''' </summary>
    ''' <param name="cancellationCheck">The cancellation check.</param>
    ''' <param name="audit">The audit.</param>
    ''' <returns></returns>
    ''' <exception cref="System.ArgumentNullException">cancellationCheck</exception>
    Public Function SaveCancellationCheck(cancellationCheck As CancellationChecks, audit As AuditMessage, Optional ByVal withCommint As Boolean = True) As ActionResult(Of CancellationChecks) Implements ICancellationCheckAdminService.SaveCancellationCheck
        If cancellationCheck Is Nothing Then
            Throw New ArgumentNullException("cancellationCheck")
        End If

        Dim unitOfWork As IUnitWork = Me._cancellationCheckRepository.UnitWork
        Try
            Using scope As New TransactionScope(TransactionScopeOption.Required, New TransactionOptions() With {.Timeout = TransactionManager.MaximumTimeout, .IsolationLevel = IsolationLevel.ReadCommitted})
                Dim checkBook = _checkAdminService.GetCheckByIdEntityBankAccountAndStatus(cancellationCheck.IdEntityAccount, 1, audit) ' 1: Estado activo
                If checkBook.StateResult Then
                    '1-Revisar que el cheque se encuentre dentro del rango de los cheques activos
                    'If Convert.ToInt64(cancellationCheck.CheckNumber) < checkBook.ObjectEmbbeded.CurrentNumber OrElse Convert.ToInt64(cancellationCheck.CheckNumber) > checkBook.ObjectEmbbeded.EndNumber Then
                    '    unitOfWork.RollbackChangesUnitOfWork()
                    '    scope.Dispose()
                    '    Return New ActionResult(Of CancellationChecks) With {.StateResult = False, .Message = String.Format("El cheque {0} no se encuentra dentro del rango activo de la chequera", cancellationCheck.CheckNumber)}
                    'End If
                    '1 - Busco dentro de los cheques pendientes, si está allí lo elimino
                    Dim outstandingCheck As OutstandingChecks = _outstandingChecksRepository.GetOutstandingCheckByCheckBookIdAndCheckNumber(checkBook.ObjectEmbbeded.Id, CType(cancellationCheck.CheckNumber, Long))
                    If outstandingCheck IsNot Nothing AndAlso outstandingCheck.Id > 0 Then
                        _outstandingChecksRepository.DeleteEntity(outstandingCheck)
                        _outstandingChecksRepository.UnitWork.Commit()
                    End If
                    '2-Buscamos si el cheque ya se encuentra anulado
                    Dim _cancellationCheck As CancellationChecks = _cancellationCheckRepository.GetCancellationCheckByCheckBookIdAndCheckNumber(checkBook.ObjectEmbbeded.Id, Convert.ToInt64(cancellationCheck.CheckNumber))
                    If _cancellationCheck IsNot Nothing AndAlso _cancellationCheck.Id > 0 Then
                        unitOfWork.RollbackChangesUnitOfWork()
                        scope.Dispose()
                        Return New ActionResult(Of CancellationChecks) With {.StateResult = False, .Message = String.Format("El cheque {0} ya se encuentra anulado, por favor seleccione otro", cancellationCheck.CheckNumber)}
                    End If
                    '3-solo si el cheque a anular es el mismo actual de la chequera entonces lo aumento
                    If cancellationCheck.CheckNumber.Equals(checkBook.ObjectEmbbeded.CurrentNumber.ToString()) Then
                        If checkBook.ObjectEmbbeded IsNot Nothing Or checkBook.ObjectEmbbeded.Id > 0 Then
                            checkBook.ObjectEmbbeded.CurrentNumber = checkBook.ObjectEmbbeded.CurrentNumber + 1
                            Dim resultOp = _checkAdminService.SaveCheck(checkBook.ObjectEmbbeded, audit)
                            If resultOp.StateResult = False Then
                                unitOfWork.RollbackChangesUnitOfWork()
                                scope.Dispose()
                                Return New ActionResult(Of CancellationChecks) With {.StateResult = False, .MessageResult = {"CC0002"}.ToList(), .Message = ResourceManager.GetString("SaveCheckBookError", "Treasury")}
                            End If
                        Else
                            unitOfWork.RollbackChangesUnitOfWork()
                            scope.Dispose()
                            Return New ActionResult(Of CancellationChecks) With {.StateResult = False, .MessageResult = {"CC0001"}.ToList(), .Message = ResourceManager.GetString("CheckActiveNoExist", "Treasury")}
                        End If
                    End If
                    Dim auxCancellationCheck As CancellationChecks = Nothing
                    Dim status As Integer
                    If cancellationCheck.ChangeTracker.State = ObjectState.Added Then
                        cancellationCheck.CreationDate = Date.Now
                        cancellationCheck.CreationUser = audit.CodeUser
                        status = Infrastructure.CrossCutting.Audit.Actions.Insert
                    Else
                        cancellationCheck.ModificationDate = Date.Now
                        cancellationCheck.ModificationUser = audit.CodeUser
                        status = Infrastructure.CrossCutting.Audit.Actions.Update
                        auxCancellationCheck = cancellationCheck.OriginalValue
                    End If

                    Dim auditProcess As New IndigoAuditSimpleEntity(Of CancellationChecks)(cancellationCheck, audit, status, auxCancellationCheck)
                    Me._cancellationCheckRepository.SaveEntity(cancellationCheck)
                    auditProcess.Execute()
                    If withCommint Then
                        unitOfWork.Commit()
                    End If
                    scope.Complete()
                    Return New ActionResult(Of CancellationChecks) With {.StateResult = True, .ObjectEmbbeded = cancellationCheck}
                Else
                    scope.Dispose()
                    Return New ActionResult(Of CancellationChecks) With {.StateResult = False, .MessageResult = checkBook.MessageResult}
                End If
            End Using
        Catch ex As OptimisticConcurrencyException
            unitOfWork.RollbackChanges()
            Return New ActionResult(Of CancellationChecks) With {.StateResult = False, .MessageResult = {"-999"}.ToList()}
        Catch ex As Exception
            unitOfWork.RollbackChanges()
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of CancellationChecks) With {.StateResult = False, .Message = ResourceManager.GetString("ErrorUnknown")}
        End Try
    End Function

    ''' <summary>
    ''' Obtener un registro de cheque cancelado por id de chequera y numero de cheque
    ''' </summary>
    Public Function GetCancellationCheckByCheckBookIdAndCheckNumber(checkBookId As Integer, CheckNumber As Long) As CancellationChecks Implements ICancellationCheckAdminService.GetCancellationCheckByCheckBookIdAndCheckNumber
        If checkBookId = 0 Then
            Throw New ArgumentNullException("checkBookId")
        End If
        If CheckNumber = 0 Then
            Throw New ArgumentNullException("CheckNumber")
        End If
        Try
            Return Me._cancellationCheckRepository.GetCancellationCheckByCheckBookIdAndCheckNumber(checkBookId, CheckNumber)
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return Nothing
        End Try
    End Function

    ''' <summary>
    ''' Obtener un registro de cheque cancelado por id 
    ''' </summary>
    Public Function GetCancellationCheckById(ByVal Id As Integer) As CancellationChecks Implements ICancellationCheckAdminService.GetCancellationCheckById
        If Id = 0 Then
            Return Nothing
        End If
        Try
            Return Me._cancellationCheckRepository.GetCancellationCheckById(Id)
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return Nothing
        End Try
    End Function

#Region "IDisposable Support"
    Private disposedValue As Boolean ' Para detectar llamadas redundantes

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not disposedValue Then
            If disposing Then
                _checkAdminService.Dispose()
            End If
            _cancellationCheckRepository = Nothing
            _checkAdminService = Nothing
            _outstandingChecksRepository = Nothing
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
