'***********************************************************************
' Assembly         : Application.Contract
' Author           : Carlos Mario Arias Rubiano
' Created          : 07/10/2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports System.Transactions
Imports Application.Base
Imports Application.Security
Imports Domain.Base
Imports Domain.Base.Entities
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.CrossCutting.Exceptions
Imports Infrastructure.CrossCutting.Resources

Public Class AuthorizationGroupAdminService
    Implements IAuthorizationGroupAdminService

    ''' <summary>
    ''' repositorio de las secuencias
    ''' </summary>
    Private _secuenseDRepository As ISequenceAuthorizationDRepository
    Private _authorizationGroupRepository As IAuthorizationGroupRepository
    Private _IUserAdminService As IUserAdminService

    Public Sub New(secuenceDRepository As ISequenceAuthorizationDRepository, authorizationGroupRepository As IAuthorizationGroupRepository, IUserAdminService As IUserAdminService)
        If secuenceDRepository Is Nothing Then
            Throw New ArgumentNullException("secuenceDRepository")
        End If
        If authorizationGroupRepository Is Nothing Then
            Throw New ArgumentNullException("authorizationGroupRepository")
        End If
        If IUserAdminService Is Nothing Then
            Throw New ArgumentNullException("IUserAdminService vacío")
        End If
        _secuenseDRepository = secuenceDRepository
        _authorizationGroupRepository = authorizationGroupRepository
        Me._IUserAdminService = IUserAdminService
    End Sub

    Public Function ChangeStateAuthorizationGroup(code As String, state As Boolean, audit As AuditMessage) As ActionResult(Of AuthorizationGroup) Implements IAuthorizationGroupAdminService.ChangeStateAuthorizationGroup
        Dim AuthorizationGroup As AuthorizationGroup = _authorizationGroupRepository.GetAuthorizationGroupByCode(code)
        AuthorizationGroup.Status = state
        Return SaveAuthorizationGroup(AuthorizationGroup, audit)
    End Function

    Public Function DeleteAuthorizationGroup(AuthorizationGroup As AuthorizationGroup, audit As AuditMessage) As ActionResult Implements IAuthorizationGroupAdminService.DeleteAuthorizationGroup
        If AuthorizationGroup Is Nothing Then
            Throw New ArgumentNullException("AuthorizationGroup")
        End If
        Dim unitOfWork As IUnitWork = Me._authorizationGroupRepository.UnitWork
        Try
            Dim auditProcess As IndigoAuditSimpleEntity(Of AuthorizationGroup)
            auditProcess = New IndigoAuditSimpleEntity(Of AuthorizationGroup)(AuthorizationGroup, audit, Infrastructure.CrossCutting.Audit.Actions.Delete)

            While AuthorizationGroup.AuthorizationGroupUser.Count > 0
                AuthorizationGroup.AuthorizationGroupUser(AuthorizationGroup.AuthorizationGroupUser.Count - 1).MarkAsDeleted()
            End While
            AuthorizationGroup.MarkAsDeleted()

            Me._authorizationGroupRepository.SaveEntity(AuthorizationGroup)
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

    Public Function GetAuthorizationGroup(code As String, audit As AuditMessage) As AuthorizationGroup Implements IAuthorizationGroupAdminService.GetAuthorizationGroup
        If String.IsNullOrEmpty(code) Then
            Throw New ArgumentNullException("code")
        End If
        If audit Is Nothing Then
            Throw New ArgumentNullException("audit")
        End If
        Try
            Dim AuthorizationGroup As AuthorizationGroup = Me._authorizationGroupRepository.GetAuthorizationGroupByCode(code.Trim())
            If AuthorizationGroup IsNot Nothing AndAlso AuthorizationGroup.Id > 0 Then
                Dim auditObject As New IndigoAuditSimpleEntity(Of AuthorizationGroup)(AuthorizationGroup, audit, Infrastructure.CrossCutting.Audit.Actions.Print)
                auditObject.Execute()
            End If

            'Se saca el listado de ids de usuario para enviar
            Dim ListUsers As New List(Of Domain.Security.Entities.User)
            'Se consulta los usuarios por id para agregarles la descripcion
            Dim ListUserIds As New List(Of Integer)
            If AuthorizationGroup IsNot Nothing AndAlso AuthorizationGroup.Id > 0 AndAlso AuthorizationGroup.AuthorizationGroupUser IsNot Nothing AndAlso AuthorizationGroup.AuthorizationGroupUser.Count > 0 Then
                'Se recorre el listado de usuarios que trae la autorizacion para sacar los ids
                For Each u In AuthorizationGroup.AuthorizationGroupUser
                    ListUserIds.Add(u.UserId)
                Next

                'Se obtiene el listado de usuarios
                ListUsers = _IUserAdminService.ListUsersByIds(ListUserIds)
                If ListUsers IsNot Nothing AndAlso ListUsers.Count > 0 Then
                    'Se asigna el nombre completo del usuario al listado que se va a asignar al datasource de la rejilla
                    For Each bu In AuthorizationGroup.AuthorizationGroupUser
                        Dim user = (From e In ListUsers Where e.Id = bu.UserId Select e).FirstOrDefault
                        If user IsNot Nothing AndAlso user.Id > 0 AndAlso user.Person IsNot Nothing Then
                            bu.FullNameUser = user.Person.Fullname
                        End If
                    Next
                End If
            End If

            Return AuthorizationGroup
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New AuthorizationGroup
        End Try
    End Function

    Public Function GetAuthorizationGroupById(id As Integer) As AuthorizationGroup Implements IAuthorizationGroupAdminService.GetAuthorizationGroupById
        Try
            Return _authorizationGroupRepository.GetAuthorizationGroupById(id)
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New AuthorizationGroup
        End Try
    End Function

    Public Function SaveAuthorizationGroup(AuthorizationGroup As AuthorizationGroup, audit As AuditMessage, Optional idSequense As Long = 0) As ActionResult(Of AuthorizationGroup) Implements IAuthorizationGroupAdminService.SaveAuthorizationGroup
        If AuthorizationGroup Is Nothing Then
            Throw New ArgumentNullException("AuthorizationGroup")
        End If
        Dim unitOfWork As IUnitWork = Me._authorizationGroupRepository.UnitWork
        Dim sequenseUnitOfWork As IUnitWork = Me._secuenseDRepository.UnitWork
        Try

            Using scope As New TransactionScope(TransactionScopeOption.Required, New TransactionOptions() With {.Timeout = TransactionManager.MaximumTimeout, .IsolationLevel = IsolationLevel.ReadCommitted})

                Dim seq As AuthorizationSequenceDetail = Nothing
                If AuthorizationGroup.Code Is Nothing OrElse AuthorizationGroup.Code.Trim().Equals(String.Empty) Then
                    seq = Me._secuenseDRepository.GetSequenseDById(idSequense)
                    If seq IsNot Nothing AndAlso seq.Id > 0 AndAlso seq.AuthorizationSequence.Sequential Then
                        Dim res = Infrastructure.CrossCutting.Base.Sequense.GetSequense(seq.Sequense.Pattern, seq.Next)
                        If res IsNot Nothing AndAlso Not res.Equals(Infrastructure.CrossCutting.Base.Sequense.ERROR_MAXVALUE) Then
                            AuthorizationGroup.Code = res
                            seq.Next += 1
                            Me._secuenseDRepository.SaveEntity(seq)
                        Else
                            scope.Dispose()
                            Return New ActionResult(Of AuthorizationGroup) With {.StatusCode = eStatusResult.WARNING, .StateResult = False, .Message = ResourceManager.GetString("SequenceNotFound")}
                        End If
                    Else
                        scope.Dispose()
                        Return New ActionResult(Of AuthorizationGroup) With {.StatusCode = eStatusResult.WARNING, .StateResult = False, .Message = ResourceManager.GetString("SequenceNotFound")}
                    End If
                End If

                Dim auxAuthorizationGroup As AuthorizationGroup = Nothing
                Dim auditProcess As IndigoAuditSimpleEntity(Of AuthorizationGroup)
                Dim status As Integer

                If AuthorizationGroup.ChangeTracker.State = Domain.Base.Entities.ObjectState.Added Then
                    AuthorizationGroup.CreationUser = audit.CodeUser
                    AuthorizationGroup.CreationDate = DateTime.Now
                    status = Infrastructure.CrossCutting.Audit.Actions.Insert
                Else
                    auxAuthorizationGroup = AuthorizationGroup.OriginalValue
                    AuthorizationGroup.ModificationUser = audit.CodeUser
                    AuthorizationGroup.ModificationDate = DateTime.Now
                    status = Infrastructure.CrossCutting.Audit.Actions.Update
                End If

                Me._authorizationGroupRepository.SaveEntity(AuthorizationGroup)
                unitOfWork.Commit()
                sequenseUnitOfWork.Commit()
                auditProcess = New IndigoAuditSimpleEntity(Of AuthorizationGroup)(AuthorizationGroup, audit, status, auxAuthorizationGroup)
                auditProcess.Execute()

                'Se marca la entidad como sin cambios
                AuthorizationGroup.MarkAsUnchanged()
                scope.Complete()
                Return New ActionResult(Of AuthorizationGroup) With {.StateResult = True, .ObjectEmbbeded = AuthorizationGroup}
            End Using

        Catch ex As OptimisticConcurrencyException
            unitOfWork.RollbackChanges()
            Return New ActionResult(Of AuthorizationGroup) With {.StateResult = False, .MessageResult = {"-999"}.ToList()}
        Catch ex As Exception
            unitOfWork.RollbackChanges()
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of AuthorizationGroup) With {.StateResult = False, .MessageResult = {ex.Message}.ToList}
        End Try
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
            _authorizationGroupRepository = Nothing
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
