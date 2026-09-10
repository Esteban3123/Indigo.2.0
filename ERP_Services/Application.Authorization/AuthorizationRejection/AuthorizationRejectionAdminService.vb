'***********************************************************************
' Assembly         : Application.Contract
' Author           : Carlos Mario Arias Rubiano
' Created          : 21/07/2020
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Entities
Imports Domain.Base
Imports Infrastructure.CrossCutting.Exceptions
Imports Application.Base
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities
Imports Infrastructure.CrossCutting.Resources
Imports System.Transactions
Imports Application.Security
Imports Application.Authorization

Public Class AuthorizationRejectionAdminService
    Implements IAuthorizationRejectionAdminService

    ''' <summary>
    ''' repositorio de las secuencias
    ''' </summary>
    Private _secuenseDRepository As ISequenceAuthorizationDRepository
    Private _authorizationRejectionRepository As IAuthorizationRejectionRepository
    Private _IUserAdminService As IUserAdminService

    Public Sub New(secuenceDRepository As ISequenceAuthorizationDRepository, authorizationRejectionRepository As IAuthorizationRejectionRepository, IUserAdminService As IUserAdminService)
        If secuenceDRepository Is Nothing Then
            Throw New ArgumentNullException("secuenceDRepository")
        End If
        If authorizationRejectionRepository Is Nothing Then
            Throw New ArgumentNullException("authorizationRejectionRepository")
        End If
        If IUserAdminService Is Nothing Then
            Throw New ArgumentNullException("IUserAdminService vacío")
        End If
        _secuenseDRepository = secuenceDRepository
        _authorizationRejectionRepository = authorizationRejectionRepository
        Me._IUserAdminService = IUserAdminService
    End Sub

    Public Function SaveAuthorizationRejection(AuthorizationRejection As AuthorizationRejection, audit As AuditMessage, Optional idSequense As Long = 0) As ActionResult(Of AuthorizationRejection) Implements IAuthorizationRejectionAdminService.SaveAuthorizationRejection
        If AuthorizationRejection Is Nothing Then
            Throw New ArgumentNullException("AuthorizationRejection")
        End If
        Dim unitOfWork As IUnitWork = Me._authorizationRejectionRepository.UnitWork
        Dim sequenseUnitOfWork As IUnitWork = Me._secuenseDRepository.UnitWork
        Try

            Using scope As New TransactionScope(TransactionScopeOption.Required, New TransactionOptions() With {.Timeout = TransactionManager.MaximumTimeout, .IsolationLevel = IsolationLevel.ReadCommitted})

                Dim seq As AuthorizationSequenceDetail = Nothing
                If AuthorizationRejection.Code Is Nothing OrElse AuthorizationRejection.Code.Trim().Equals(String.Empty) Then
                    seq = Me._secuenseDRepository.GetSequenseDById(idSequense)
                    If seq IsNot Nothing AndAlso seq.Id > 0 AndAlso seq.AuthorizationSequence.Sequential Then
                        Dim res = Infrastructure.CrossCutting.Base.Sequense.GetSequense(seq.Sequense.Pattern, seq.Next)
                        If res IsNot Nothing AndAlso Not res.Equals(Infrastructure.CrossCutting.Base.Sequense.ERROR_MAXVALUE) Then
                            AuthorizationRejection.Code = res
                            seq.Next += 1
                            Me._secuenseDRepository.SaveEntity(seq)
                        Else
                            scope.Dispose()
                            Return New ActionResult(Of AuthorizationRejection) With {.StatusCode = eStatusResult.WARNING, .StateResult = False, .Message = ResourceManager.GetString("SequenceNotFound")}
                        End If
                    Else
                        scope.Dispose()
                        Return New ActionResult(Of AuthorizationRejection) With {.StatusCode = eStatusResult.WARNING, .StateResult = False, .Message = ResourceManager.GetString("SequenceNotFound")}
                    End If
                End If

                Dim auxAuthorizationRejection As AuthorizationRejection = Nothing
                Dim auditProcess As IndigoAuditSimpleEntity(Of AuthorizationRejection)
                Dim status As Integer

                If AuthorizationRejection.ChangeTracker.State = Domain.Base.Entities.ObjectState.Added Then
                    AuthorizationRejection.CreationUser = audit.CodeUser
                    AuthorizationRejection.CreationDate = DateTime.Now
                    status = Infrastructure.CrossCutting.Audit.Actions.Insert
                Else
                    auxAuthorizationRejection = AuthorizationRejection.OriginalValue
                    AuthorizationRejection.ModificationUser = audit.CodeUser
                    AuthorizationRejection.ModificationDate = DateTime.Now
                    status = Infrastructure.CrossCutting.Audit.Actions.Update
                End If

                Me._authorizationRejectionRepository.SaveEntity(AuthorizationRejection)
                unitOfWork.Commit()
                sequenseUnitOfWork.Commit()
                auditProcess = New IndigoAuditSimpleEntity(Of AuthorizationRejection)(AuthorizationRejection, audit, status, auxAuthorizationRejection)
                auditProcess.Execute()

                'Se marca la entidad como sin cambios
                AuthorizationRejection.MarkAsUnchanged()
                scope.Complete()
                Return New ActionResult(Of AuthorizationRejection) With {.StateResult = True, .ObjectEmbbeded = AuthorizationRejection}
            End Using

        Catch ex As OptimisticConcurrencyException
            unitOfWork.RollbackChanges()
            Return New ActionResult(Of AuthorizationRejection) With {.StateResult = False, .MessageResult = {"-999"}.ToList()}
        Catch ex As Exception
            unitOfWork.RollbackChanges()
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of AuthorizationRejection) With {.StateResult = False, .MessageResult = {ex.Message}.ToList}
        End Try
    End Function

    Public Function DeleteAuthorizationRejection(AuthorizationRejection As AuthorizationRejection, audit As AuditMessage) As ActionResult Implements IAuthorizationRejectionAdminService.DeleteAuthorizationRejection
        If AuthorizationRejection Is Nothing Then
            Throw New ArgumentNullException("AuthorizationRejection")
        End If
        Dim unitOfWork As IUnitWork = Me._authorizationRejectionRepository.UnitWork
        Try
            Dim auditProcess As IndigoAuditSimpleEntity(Of AuthorizationRejection)
            auditProcess = New IndigoAuditSimpleEntity(Of AuthorizationRejection)(AuthorizationRejection, audit, Infrastructure.CrossCutting.Audit.Actions.Delete)

            While AuthorizationRejection.AuthorizationRejectionUser.Count > 0
                AuthorizationRejection.AuthorizationRejectionUser(AuthorizationRejection.AuthorizationRejectionUser.Count - 1).MarkAsDeleted()
            End While
            AuthorizationRejection.MarkAsDeleted()

            Me._authorizationRejectionRepository.SaveEntity(AuthorizationRejection)
            unitOfWork.Commit()
            auditProcess.Execute()
            Return New ActionResult With {.StateResult = True}
        Catch ex As OptimisticConcurrencyException
            unitOfWork.RollbackChanges()
            Return New ActionResult With {.StateResult = False, .MessageResult = New List(Of String)({"-999"})}
        Catch ex As UpdateException
            unitOfWork.RollbackChanges()
            Return New ActionResult With {.StateResult = False, .MessageResult = New List(Of String)({"-000"})}
        Catch ex As Exception
            unitOfWork.RollbackChanges()
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult With {.StateResult = False, .MessageResult = {ex.Message}.ToList}
        End Try
    End Function

    Public Function GetAuthorizationRejection(code As String, audit As AuditMessage) As AuthorizationRejection Implements IAuthorizationRejectionAdminService.GetAuthorizationRejection
        If String.IsNullOrEmpty(code) Then
            Throw New ArgumentNullException("code")
        End If
        If audit Is Nothing Then
            Throw New ArgumentNullException("audit")
        End If
        Try
            Dim AuthorizationRejection As AuthorizationRejection = Me._authorizationRejectionRepository.GetAuthorizationRejectionByCode(code.Trim())
            If AuthorizationRejection IsNot Nothing AndAlso AuthorizationRejection.Id > 0 Then
                Dim auditObject As New IndigoAuditSimpleEntity(Of AuthorizationRejection)(AuthorizationRejection, audit, Infrastructure.CrossCutting.Audit.Actions.Print)
                auditObject.Execute()
            End If

            'Se saca el listado de ids de usuario para enviar
            Dim ListUsers As New List(Of Domain.Security.Entities.User)
            'Se consulta los usuarios por id para agregarles la descripcion
            Dim ListUserIds As New List(Of Integer)
            If AuthorizationRejection IsNot Nothing AndAlso AuthorizationRejection.Id > 0 AndAlso AuthorizationRejection.AuthorizationRejectionUser IsNot Nothing AndAlso AuthorizationRejection.AuthorizationRejectionUser.Count > 0 Then
                'Se recorre el listado de usuarios que trae la autorizacion para sacar los ids
                For Each u In AuthorizationRejection.AuthorizationRejectionUser
                    ListUserIds.Add(u.UserId)
                Next

                'Se obtiene el listado de usuarios
                ListUsers = _IUserAdminService.ListUsersByIds(ListUserIds)
                If ListUsers IsNot Nothing AndAlso ListUsers.Count > 0 Then
                    'Se asigna el nombre completo del usuario al listado que se va a asignar al datasource de la rejilla
                    For Each bu In AuthorizationRejection.AuthorizationRejectionUser
                        Dim user = (From e In ListUsers Where e.Id = bu.UserId Select e).FirstOrDefault
                        If user IsNot Nothing AndAlso user.Id > 0 AndAlso user.Person IsNot Nothing Then
                            bu.FullNameUser = user.Person.Fullname
                        End If
                    Next
                End If
            End If

            Return AuthorizationRejection
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New AuthorizationRejection
        End Try
    End Function

    Public Function GetAuthorizationRejectionById(id As Integer) As AuthorizationRejection Implements IAuthorizationRejectionAdminService.GetAuthorizationRejectionById
        Try
            Return _authorizationRejectionRepository.GetAuthorizationRejectionById(id)
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New AuthorizationRejection
        End Try
    End Function

    Public Function ChangeStateAuthorizationRejection(code As String, state As Boolean, audit As AuditMessage) As ActionResult(Of AuthorizationRejection) Implements IAuthorizationRejectionAdminService.ChangeStateAuthorizationRejection
        Dim AuthorizationRejection As AuthorizationRejection = _authorizationRejectionRepository.GetAuthorizationRejectionByCode(code)
        AuthorizationRejection.Status = state
        Return SaveAuthorizationRejection(AuthorizationRejection, audit)
    End Function

#Region "IDisposable Support"
    Private disposedValue As Boolean ' Para detectar llamadas redundantes

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not disposedValue Then
            If disposing Then
                ' TODO: elimine el estado administrado (objetos administrados).
            End If
            _secuenseDRepository = Nothing
            _authorizationRejectionRepository = Nothing
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
