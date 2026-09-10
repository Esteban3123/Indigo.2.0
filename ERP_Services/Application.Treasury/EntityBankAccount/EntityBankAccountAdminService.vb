'***********************************************************************
' Assembly         : Application.Treasury
' Author           : Diego Andrés Roldán Lozano
' Created          : 10-04-2014
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
Imports Application.Security

Public Class EntityBankAccountAdminService
    Implements IEntityBankAccountAdminService

#Region "Fields"

    ''' <summary>
    ''' Repositorio de tarjetas
    ''' </summary>
    Private _entityBankAccountRepository As IEntityBankAccountRepository

    ''' <summary>
    ''' Repositorio de las secuencias numericas
    ''' </summary>
    Private _sequenceDRepository As ISequenseTreasuryDRepository

    ''' <summary>
    ''' Aplicación de usuarios
    ''' </summary>
    ''' <remarks></remarks>
    Private _IUserAdminService As IUserAdminService

    Private Const FORM_NAME As String = "Cuentas Bancarias a Entidades"

#End Region

    Public Sub New(ByVal entityBankAccountRepository As IEntityBankAccountRepository, ByVal sequenceDRepository As ISequenseTreasuryDRepository, IUserAdminService As IUserAdminService)
        If entityBankAccountRepository Is Nothing Then
            Throw New ArgumentNullException("entityBankAccountRepository")
        End If
        If sequenceDRepository Is Nothing Then
            Throw New ArgumentNullException("sequenceDRepository")
        End If
        If IUserAdminService Is Nothing Then
            Throw New ArgumentNullException("IUserAdminService")
        End If
        _entityBankAccountRepository = entityBankAccountRepository
        _sequenceDRepository = sequenceDRepository
        _IUserAdminService = IUserAdminService
    End Sub

    ''' <summary>
    ''' Deletes the entity bank account.
    ''' </summary>
    ''' <param name="entityBankAccount">The entity bank account.</param>
    ''' <param name="audit">The audit.</param>
    ''' <returns></returns>
    ''' <exception cref="System.ArgumentNullException">bankCity</exception>
    Public Function DeleteEntityBankAccount(entityBankAccount As EntityBankAccounts, audit As AuditMessage) As ActionResult Implements IEntityBankAccountAdminService.DeleteEntityBankAccount
        If entityBankAccount Is Nothing Then
            Throw New ArgumentNullException("bankCity")
        End If
        Dim unitOfWork As IUnitWork = Me._entityBankAccountRepository.UnitWork
        Try
            Using scope As New TransactionScope(TransactionScopeOption.Required, New TransactionOptions() With {.Timeout = TransactionManager.MaximumTimeout, .IsolationLevel = IsolationLevel.ReadCommitted})
                entityBankAccount.ModificationUser = audit.CodeUser
                entityBankAccount.ModificationDate = Date.Now
                Dim status As Integer = Infrastructure.CrossCutting.Audit.Actions.Delete
                Dim auditProcess As New IndigoAuditSimpleEntity(Of EntityBankAccounts)(entityBankAccount, audit, status)

                While entityBankAccount.Checkbooks.Count > 0
                    entityBankAccount.Checkbooks.Item(entityBankAccount.Checkbooks.Count() - 1).MarkAsDeleted()
                End While
                While entityBankAccount.EntityBankAccountUser.Count > 0
                    entityBankAccount.EntityBankAccountUser.Item(entityBankAccount.EntityBankAccountUser.Count() - 1).MarkAsDeleted()
                End While
                entityBankAccount.MarkAsDeleted()
                Me._entityBankAccountRepository.SaveEntity(entityBankAccount)
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
    ''' Actualiza el estado del registro
    ''' </summary>
    ''' <param name="code">The code.</param>
    ''' <param name="state">if set to <c>true</c> [state].</param>
    ''' <param name="audit">The audit.</param>
    ''' <returns></returns>
    Public Function UpdateStateEntityBankAccount(code As String, state As Boolean, audit As AuditMessage) As ActionResult(Of EntityBankAccounts) Implements IEntityBankAccountAdminService.UpdateStateEntityBankAccount
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
            Dim entityBankAccount As EntityBankAccounts = Me._entityBankAccountRepository.GetEntityBankAccount(code.Trim())
            If entityBankAccount IsNot Nothing AndAlso entityBankAccount.Id > 0 Then
                entityBankAccount.Status = state
            End If
            Dim result = Me.SaveEntityBankAccount(entityBankAccount, audit)
            If result.StatusCode = eStatusResult.SUCCESS Then
                result.Message = ResourceManager.GetString("UpdateState")
            End If
            Return result
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of EntityBankAccounts) With {.StateResult = False, .StatusCode = eStatusResult.EXCEPTION, .Message = IndigoManagementExceptions.GetExceptionDetails(ex)}
        End Try
    End Function

    ''' <summary>
    ''' Obtiene una cuenta de entidad por codigo
    ''' </summary>
    ''' <param name="code">The code.</param>
    ''' <param name="audit">The audit.</param>
    ''' <returns></returns>
    ''' <exception cref="System.ArgumentNullException">
    ''' code
    ''' or
    ''' audit
    ''' </exception>
    Public Function GetEntityBankAccount(code As String, audit As AuditMessage) As ActionResult(Of EntityBankAccounts) Implements IEntityBankAccountAdminService.GetEntityBankAccount
        If String.IsNullOrEmpty(code) Then
            Throw New ArgumentNullException("code")
        End If
        If audit Is Nothing Then
            Throw New ArgumentNullException("audit")
        End If
        Try
            Dim entityBankAccount As EntityBankAccounts = Me._entityBankAccountRepository.GetEntityBankAccount(code.Trim())
            If entityBankAccount IsNot Nothing AndAlso entityBankAccount.Id > 0 Then
                Dim status As Integer = Infrastructure.CrossCutting.Audit.Actions.Print
                Dim auditProcess As New IndigoAuditSimpleEntity(Of EntityBankAccounts)(entityBankAccount, audit, status)
                auditProcess.Execute()
            End If
            Return New ActionResult(Of EntityBankAccounts) With {.StateResult = True, .ObjectEmbbeded = entityBankAccount}
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of EntityBankAccounts) With {.StateResult = False}
        End Try
    End Function

    ''' <summary>
    ''' Gets the entity bank account by identifier.
    ''' </summary>
    ''' <param name="Id">The identifier.</param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    Public Function GetEntityBankAccountById(Id As Integer, audit As AuditMessage) As EntityBankAccounts Implements IEntityBankAccountAdminService.GetEntityBankAccountById
        If Id = 0 Then
            Throw New ArgumentNullException("Id")
        End If
        Try
            Dim entityBankAccount As EntityBankAccounts = Me._entityBankAccountRepository.GetEntityBankAccountById(Id)
            Return entityBankAccount
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return Nothing
        End Try
    End Function

    ''' <summary>
    ''' guarda una cuenta de entidad
    ''' </summary>
    ''' <param name="entityBankAccount">The entity bank account.</param>
    ''' <param name="audit">The audit.</param>
    ''' <returns></returns>
    ''' <exception cref="System.ArgumentNullException">bankCity</exception>
    Public Function SaveEntityBankAccount(entityBankAccount As EntityBankAccounts, audit As AuditMessage, Optional ByVal idSequence As Int64 = 0, Optional ByVal withCommit As Boolean = True) As ActionResult(Of EntityBankAccounts) Implements IEntityBankAccountAdminService.SaveEntityBankAccount
        If entityBankAccount Is Nothing Then
            Throw New ArgumentNullException("bankCity")
        End If
        Dim unitOfWork As IUnitWork = Me._entityBankAccountRepository.UnitWork
        Dim sequenceUnitOfWork As IUnitWork = Me._sequenceDRepository.UnitWork

        Try
            Using scope As New TransactionScope(TransactionScopeOption.Required, New TransactionOptions() With {.Timeout = TransactionManager.MaximumTimeout, .IsolationLevel = IsolationLevel.ReadCommitted})
                Dim MessageResult As String = String.Empty

                If String.IsNullOrEmpty(entityBankAccount.Code) Then
                    Dim seq As TreasurySequenceDetail = _sequenceDRepository.GetSequenseDById(idSequence)
                    If seq IsNot Nothing AndAlso seq.Id > 0 AndAlso seq.TreasurySequence.Sequential Then
                        Dim res = Infrastructure.CrossCutting.Base.Sequense.GetSequense(seq.Sequense.Pattern, seq.Next)
                        If res IsNot Nothing AndAlso Not res.Equals(Infrastructure.CrossCutting.Base.Sequense.ERROR_MAXVALUE) Then
                            entityBankAccount.Code = res
                            seq.Next += 1
                            Me._sequenceDRepository.SaveEntity(seq)
                        Else
                            scope.Dispose()
                            Return New ActionResult(Of EntityBankAccounts) With {.StatusCode = eStatusResult.WARNING, .StateResult = False, .MessageResult = {"_Seq02_"}.ToList(), .Message = String.Format(ResourceManager.GetString("SequenceFormNotFound"), FORM_NAME)}
                        End If
                        MessageResult = If(seq.TreasurySequence.Sequential, String.Format(ResourceManager.GetString("SavedWithCode"), entityBankAccount.Code), ResourceManager.GetString("SaveMessage"))
                    Else
                        scope.Dispose()
                        Return New ActionResult(Of EntityBankAccounts) With {.StatusCode = eStatusResult.WARNING, .StateResult = False, .MessageResult = {"_Seq02_"}.ToList(), .Message = String.Format(ResourceManager.GetString("SequenceFormNotFound"), FORM_NAME)}
                    End If
                Else
                    MessageResult = ResourceManager.GetString("SaveMessage")
                End If

                Dim auxEntityBankAccount As EntityBankAccounts = Nothing
                Dim status As Integer
                If entityBankAccount.ChangeTracker.State = ObjectState.Added Then
                    entityBankAccount.CreationDate = Date.Now
                    entityBankAccount.CreationUser = audit.CodeUser
                    status = Infrastructure.CrossCutting.Audit.Actions.Insert
                Else
                    MessageResult = ResourceManager.GetString("UpdateMessage")
                    entityBankAccount.ModificationDate = Date.Now
                    entityBankAccount.ModificationUser = audit.CodeUser
                    status = Convert.ToInt32(Infrastructure.CrossCutting.Audit.Actions.Update)
                    auxEntityBankAccount = entityBankAccount.OriginalValue
                End If

                Me._entityBankAccountRepository.SaveEntity(entityBankAccount)
                If withCommit Then
                    unitOfWork.Commit()
                    sequenceUnitOfWork.Commit()
                    entityBankAccount.MarkAsUnchanged()
                End If
                Dim auditProcess As New IndigoAuditSimpleEntity(Of EntityBankAccounts)(entityBankAccount, audit, status, auxEntityBankAccount)
                auditProcess.Execute()
                scope.Complete()
                Return New ActionResult(Of EntityBankAccounts) With {.StateResult = True, .StatusCode = eStatusResult.SUCCESS, .ObjectEmbbeded = entityBankAccount, .Message = MessageResult}
            End Using
        Catch ex As OptimisticConcurrencyException
            unitOfWork.RollbackChanges()
            Return New ActionResult(Of EntityBankAccounts) With {.StateResult = False, .StatusCode = eStatusResult.WARNING, .MessageResult = {"-999"}.ToList(), .Message = ResourceManager.GetString("ErrorConcurrence")}
        Catch ex As Exception
            unitOfWork.RollbackChanges()
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of EntityBankAccounts) With {.StateResult = False, .StatusCode = eStatusResult.EXCEPTION, .MessageResult = {ex.Message}.ToList, .Message = IndigoManagementExceptions.GetExceptionDetails(ex)}
        End Try
    End Function

    ''' <summary>
    ''' Lista los usuarios permitidos de una cuenta bancaria por el id de la cuenta bancaria
    ''' </summary>
    ''' <param name="IdEntityBankAccount">The identifier entity bank account.</param>
    Public Function ListEntityBankAccountUserByIdEntityBankiAccount(IdEntityBankAccount As Integer, audit As AuditMessage) As ActionResult(Of List(Of EntityBankAccountUser)) Implements IEntityBankAccountAdminService.ListEntityBankAccountUserByIdEntityBankiAccount
        If IdEntityBankAccount = 0 Then
            Throw New ArgumentNullException("IdEntityBankAccount")
        End If
        If audit Is Nothing Then
            Throw New ArgumentNullException("audit")
        End If
        Try
            Dim ListEntityBankAccountUser As List(Of EntityBankAccountUser) = Me._entityBankAccountRepository.ListEntityBankAccountUserByIdEntityBankiAccount(IdEntityBankAccount)
            Return New ActionResult(Of List(Of EntityBankAccountUser)) With {.StateResult = True, .ObjectEmbbeded = ListEntityBankAccountUser}
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of List(Of EntityBankAccountUser)) With {.StateResult = False}
        End Try
    End Function

    ''' <summary>
    ''' Lista todos los prefijos registrados en la tabla
    ''' </summary>
    ''' <returns>Lista de prefijos</returns>
    Public Function ListPrefixs() As List(Of String) Implements IEntityBankAccountAdminService.ListPrefixs
        Try
            Return Me._entityBankAccountRepository.ListPrefixs()
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return Nothing
        End Try
    End Function

    ''' <summary>
    ''' Consulta los usuarios que tienen permiso a cuentas bancarias a entidades
    ''' </summary>
    ''' <param name="EntityBankAccountId"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListUsersByEntityBankAccountId(EntityBankAccountId As Integer, audit As AuditMessage) As ActionResult(Of List(Of Domain.Security.Entities.User)) Implements IEntityBankAccountAdminService.ListUsersByEntityBankAccountId
        If EntityBankAccountId = 0 Then
            Throw New ArgumentNullException("EntityBankAccountId")
        End If
        Try
            'Listado de usuarios que se va a retornar
            Dim ListUsers As New List(Of Domain.Security.Entities.User)

            'Se consulta el listado de usuarios al cual la cuenta bancaria a entidades tengan permiso
            Dim ListEntityBankAccountUser As List(Of EntityBankAccountUser) = Me._entityBankAccountRepository.ListEntityBankAccountUserByIdEntityBankiAccount(EntityBankAccountId)
            If ListEntityBankAccountUser IsNot Nothing AndAlso ListEntityBankAccountUser.Count > 0 Then
                Dim listUserCodes As New List(Of String)()
                For Each u In ListEntityBankAccountUser
                    listUserCodes.Add(u.CodUser)
                Next
                'Se recorre el listado de usuarios para poder consultar al usuario por codigo y armar el listado de usuarios que se va a retornar
                'Se consulta el usuario por codigo para asignarselo al listado que se retorna
                Dim user = _IUserAdminService.ListUsersByCodes(listUserCodes)
                If user IsNot Nothing AndAlso user.Count > 0 Then
                    ListUsers.AddRange(user)
                End If
            End If
            Return New ActionResult(Of List(Of Domain.Security.Entities.User)) With {.StateResult = True, .ObjectEmbbeded = ListUsers}
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of List(Of Domain.Security.Entities.User)) With {.StateResult = False}
        End Try
    End Function

#Region "IDisposable Support"
    Private disposedValue As Boolean ' Para detectar llamadas redundantes

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not disposedValue Then
            If disposing Then
                _IUserAdminService.Dispose()
            End If
            _entityBankAccountRepository = Nothing
            _sequenceDRepository = Nothing
            _IUserAdminService = Nothing
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
