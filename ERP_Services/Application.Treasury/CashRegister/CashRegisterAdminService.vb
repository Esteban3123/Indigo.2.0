'***********************************************************************
' Assembly         : Application.Treasury
' Author           : Diego Andrés Roldán Lozano
' Created          : 31-03-2014
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
Imports Infrastructure.CrossCutting.Resources
Imports System.Transactions
Imports Microsoft.Extensions.Caching.Memory

Public Class CashRegisterAdminService
    Implements ICashRegisterAdminService

#Region "Fields"

    ''' <summary>
    ''' Repositorio de la entidad ciudades bancarias
    ''' </summary>
    Private _cashRegisterRepository As ICashRegisterRepository

    ''' <summary>
    ''' Repositorio de secuencias numericas
    ''' </summary>
    Private _sequenceDRepository As ISequenseTreasuryDRepository

    Private Const FORM_NAME As String = "Cajas"

    Private ReadOnly _cache As IMemoryCache

#End Region

#Region "Methods"

    Public Sub New(ByVal cashRegisterRepository As ICashRegisterRepository, ByVal sequenceDRepository As ISequenseTreasuryDRepository, cache As IMemoryCache)
        If cashRegisterRepository Is Nothing Then
            Throw New ArgumentNullException("repository")
        End If
        If sequenceDRepository Is Nothing Then
            Throw New ArgumentNullException("sequenceDRepository")
        End If
        _cashRegisterRepository = cashRegisterRepository
        _sequenceDRepository = sequenceDRepository
        _cache = cache
    End Sub

    ''' <summary>
    ''' Deletes the cash register.
    ''' </summary>
    ''' <param name="cashRegister">The cash register.</param>
    ''' <param name="audit">The audit.</param>
    ''' <returns></returns>
    ''' <exception cref="System.ArgumentNullException">cashRegister</exception>
    Public Function DeleteCashRegister(cashRegister As CashRegisters, audit As AuditMessage) As ActionResult Implements ICashRegisterAdminService.DeleteCashRegister
        If cashRegister Is Nothing Then
            Throw New ArgumentNullException("cashRegister")
        End If
        Dim unitOfWork As IUnitWork = Me._cashRegisterRepository.UnitWork
        Try
            Using scope As New TransactionScope(TransactionScopeOption.Required, New TransactionOptions() With {.Timeout = TransactionManager.MaximumTimeout, .IsolationLevel = IsolationLevel.ReadCommitted})
                cashRegister.ModificationUser = audit.CodeUser
                cashRegister.ModificationDate = Date.Now
                Dim status As Integer = Infrastructure.CrossCutting.Audit.Actions.Delete
                Dim auditProcess As New IndigoAuditSimpleEntity(Of CashRegisters)(cashRegister, audit, status)

                While cashRegister.CashRegisterUser.Count > 0
                    cashRegister.CashRegisterUser.Item(cashRegister.CashRegisterUser.Count() - 1).MarkAsDeleted()
                End While
                cashRegister.MarkAsDeleted()
                Me._cashRegisterRepository.SaveEntity(cashRegister)
                unitOfWork.Commit()
                auditProcess.Execute()
                scope.Complete()
                Return New ActionResult With {.StateResult = True, .StatusCode = eStatusResult.SUCCESS, .Message = ResourceManager.GetString("RecordDeleted")}
            End Using
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
    ''' actualiza el estado del registro
    ''' </summary>
    ''' <param name="code">The code.</param>
    ''' <param name="state">if set to <c>true</c> [state].</param>
    ''' <param name="audit">The audit.</param>
    ''' <returns></returns>
    Public Function UpdateStateCashRegister(code As String, state As Boolean, audit As AuditMessage) As ActionResult(Of CashRegisters) Implements ICashRegisterAdminService.UpdateStateCashRegister
        If String.IsNullOrEmpty(code) Then
            Throw New ArgumentNullException("code")
        End If
        If String.IsNullOrEmpty(state) Then
            Throw New ArgumentNullException("state")
        End If
        If audit Is Nothing Then
            Throw New ArgumentNullException("audit")
        End If
        Try
            Dim cashRegister As CashRegisters = Me._cashRegisterRepository.GetCashRegister(code.Trim())
            If cashRegister IsNot Nothing AndAlso cashRegister.Id > 0 Then
                cashRegister.Status = state
            End If
            Dim result = Me.SaveCashRegister(cashRegister, audit)
            If result.StatusCode = eStatusResult.SUCCESS Then
                result.Message = ResourceManager.GetString("UpdateState")
            End If
            Return result
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of CashRegisters) With {.StateResult = False, .StatusCode = eStatusResult.EXCEPTION, .Message = IndigoManagementExceptions.GetExceptionDetails(ex)}
        End Try
    End Function

    ''' <summary>
    ''' Gets the cash register.
    ''' </summary>
    ''' <param name="code">The code.</param>
    ''' <param name="audit">The audit.</param>
    ''' <returns></returns>
    ''' <exception cref="System.ArgumentNullException">
    ''' code
    ''' or
    ''' audit
    ''' </exception>
    Public Function GetCashRegister(code As String, audit As AuditMessage) As ActionResult(Of CashRegisters) Implements ICashRegisterAdminService.GetCashRegister
        If String.IsNullOrEmpty(code) Then
            Throw New ArgumentNullException("code")
        End If
        If audit Is Nothing Then
            Throw New ArgumentNullException("audit")
        End If
        Try
            Dim cashRegister As CashRegisters = Me._cashRegisterRepository.GetCashRegister(code.Trim())
            If cashRegister IsNot Nothing AndAlso cashRegister.Id > 0 Then
                Dim status As Integer = Infrastructure.CrossCutting.Audit.Actions.Print
                Dim auditProcess As New IndigoAuditSimpleEntity(Of CashRegisters)(cashRegister, audit, status)
                auditProcess.Execute()
            End If
            Return New ActionResult(Of CashRegisters) With {.StateResult = True, .ObjectEmbbeded = cashRegister}
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of CashRegisters) With {.StateResult = False}
        End Try
    End Function

    ''' <summary>
    ''' Obtiene una caja por Id
    ''' </summary>
    ''' <param name="Id">The identifier.</param>
    ''' <returns></returns>
    Public Function GetCashRegisterById(Id As Integer) As CashRegisters Implements ICashRegisterAdminService.GetCashRegisterById
        If String.IsNullOrEmpty(Id) Then
            Throw New ArgumentNullException("Id")
        End If
        Try
            Dim CashRegister = _cache.GetOrCreate(Of CashRegisters)(Id, Function(x)
                                                                            x.AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(10)
                                                                            Return Me._cashRegisterRepository.GetCashRegisterById(Id)
                                                                        End Function)
            Return CashRegister

        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return Nothing
        End Try
    End Function

    ''' <summary>
    ''' Lista todos los registros de caja
    ''' </summary>
    ''' <returns></returns>
    Public Function ListCashRegister() As List(Of CashRegisters) Implements ICashRegisterAdminService.ListCashRegister
        Try
            Return Me._cashRegisterRepository.ListCashRegister()
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return Nothing
        End Try
    End Function

    ''' <summary>
    ''' Saves the cash register.
    ''' </summary>
    ''' <param name="cashRegister">The cash register.</param>
    ''' <param name="audit">The audit.</param>
    ''' <returns></returns>
    ''' <exception cref="System.ArgumentNullException">cashRegister</exception>
    Public Function SaveCashRegister(cashRegister As CashRegisters, audit As AuditMessage, Optional ByVal idSequence As Int64 = 0, Optional withCommit As Boolean = True) As ActionResult(Of CashRegisters) Implements ICashRegisterAdminService.SaveCashRegister
        If cashRegister Is Nothing Then
            Throw New ArgumentNullException("cashRegister")
        End If
        Dim unitOfWork As IUnitWork = Me._cashRegisterRepository.UnitWork
        Dim sequenceUnitOfWork As IUnitWork = Me._sequenceDRepository.UnitWork
        Try
            Using scope As New TransactionScope(TransactionScopeOption.Required, New TransactionOptions() With {.Timeout = TransactionManager.MaximumTimeout, .IsolationLevel = IsolationLevel.ReadCommitted})
                Dim MessageResult As String = String.Empty

                If String.IsNullOrEmpty(cashRegister.Code) Then
                    Dim seq As TreasurySequenceDetail = Me._sequenceDRepository.GetSequenseDById(idSequence)
                    If seq IsNot Nothing AndAlso seq.Id > 0 AndAlso seq.TreasurySequence.Sequential Then
                        Dim res = Infrastructure.CrossCutting.Base.Sequense.GetSequense(seq.Sequense.Pattern, seq.Next)
                        If res IsNot Nothing AndAlso Not res.Equals(Infrastructure.CrossCutting.Base.Sequense.ERROR_MAXVALUE) Then
                            cashRegister.Code = res
                            seq.Next += 1
                            Me._sequenceDRepository.SaveEntity(seq)
                        Else
                            scope.Dispose()
                            Return New ActionResult(Of CashRegisters) With {.StatusCode = eStatusResult.WARNING, .StateResult = False, .MessageResult = {"_Seq01_"}.ToList(), .Message = String.Format(ResourceManager.GetString("SequenceFormNotFound"), FORM_NAME)}
                        End If
                        MessageResult = If(seq.TreasurySequence.Sequential, String.Format(ResourceManager.GetString("SavedWithCode"), cashRegister.Code), ResourceManager.GetString("SaveMessage"))
                    Else
                        scope.Dispose()
                        Return New ActionResult(Of CashRegisters) With {.StatusCode = eStatusResult.WARNING, .StateResult = False, .MessageResult = {"_Seq01_"}.ToList(), .Message = String.Format(ResourceManager.GetString("SequenceFormNotFound"), FORM_NAME)}
                    End If
                Else
                    MessageResult = ResourceManager.GetString("SaveMessage")
                End If

                Dim auxCashRegister As CashRegisters = Nothing
                Dim status As Integer
                If cashRegister.ChangeTracker.State = ObjectState.Added Then
                    cashRegister.CreationDate = DateTime.Now
                    cashRegister.CreationUser = audit.CodeUser
                    status = Infrastructure.CrossCutting.Audit.Actions.Insert
                Else
                    MessageResult = ResourceManager.GetString("UpdateMessage")
                    cashRegister.ModificationDate = DateTime.Now
                    cashRegister.ModificationUser = audit.CodeUser
                    status = Infrastructure.CrossCutting.Audit.Actions.Update
                    auxCashRegister = cashRegister.OriginalValue
                End If

                Me._cashRegisterRepository.SaveEntity(cashRegister)
                If withCommit Then
                    unitOfWork.Commit()
                    sequenceUnitOfWork.Commit()
                End If

                Dim auditProcess As New IndigoAuditSimpleEntity(Of CashRegisters)(cashRegister, audit, status, auxCashRegister)
                auditProcess.Execute()
                'Se marca la entidad como sin cambios
                cashRegister.MarkAsUnchanged()
                scope.Complete()
                Return New ActionResult(Of CashRegisters) With {.StateResult = True, .StatusCode = eStatusResult.SUCCESS, .ObjectEmbbeded = cashRegister, .Message = MessageResult}
            End Using
        Catch ex As DbUpdateException
            unitOfWork.RollbackChanges()
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of CashRegisters) With {.StateResult = False, .StatusCode = eStatusResult.WARNING, .MessageResult = {"-888"}.ToList(), .Message = ResourceManager.GetString("ErrorConcurrence")}
        Catch ex As OptimisticConcurrencyException
            unitOfWork.RollbackChanges()
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of CashRegisters) With {.StateResult = False, .StatusCode = eStatusResult.WARNING, .MessageResult = {"-999"}.ToList(), .Message = ResourceManager.GetString("ErrorConcurrence")}
        Catch ex As Exception
            unitOfWork.RollbackChanges()
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of CashRegisters) With {.StateResult = False, .StatusCode = eStatusResult.EXCEPTION, .MessageResult = {ex.Message}.ToList, .Message = IndigoManagementExceptions.GetExceptionDetails(ex)}
        End Try
    End Function

    ''' <summary>
    ''' Lista todos los prefijos registrados en la tabla
    ''' </summary>
    ''' <returns>Lista de prefijos</returns>
    Public Function ListPrefixs() As List(Of String) Implements ICashRegisterAdminService.ListPrefixs
        Try
            Return Me._cashRegisterRepository.ListPrefixs()
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return Nothing
        End Try
    End Function

    Public Function GetFirstCashbyUserId(UserId As Integer, Optional currencyId As Integer? = Nothing) As CashRegisters Implements ICashRegisterAdminService.GetFirstCashbyUserId
        Try
            Return Me._cashRegisterRepository.GetFirstCashbyUserId(UserId, currencyId)
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return Nothing
        End Try
    End Function

#End Region

#Region "IDisposable Support"
    Private disposedValue As Boolean ' Para detectar llamadas redundantes

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not disposedValue Then
            If disposing Then

            End If
            _cashRegisterRepository = Nothing
            _sequenceDRepository = Nothing
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
