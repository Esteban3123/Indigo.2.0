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

Public Class AuthorizationScheduleTemplateAdminService
    Implements IAuthorizationScheduleTemplateAdminService

    ''' <summary>
    ''' repositorio de las secuencias
    ''' </summary>
    Private _secuenseDRepository As ISequenceAuthorizationDRepository
    Private _authorizationScheduleTemplateRepository As IAuthorizationScheduleTemplateRepository
    Private _IUserAdminService As IUserAdminService

    Public Sub New(secuenceDRepository As ISequenceAuthorizationDRepository, authorizationScheduleTemplateRepository As IAuthorizationScheduleTemplateRepository, IUserAdminService As IUserAdminService)
        If secuenceDRepository Is Nothing Then
            Throw New ArgumentNullException("secuenceDRepository")
        End If
        If authorizationScheduleTemplateRepository Is Nothing Then
            Throw New ArgumentNullException("authorizationScheduleTemplateRepository")
        End If
        If IUserAdminService Is Nothing Then
            Throw New ArgumentNullException("IUserAdminService vacío")
        End If
        _secuenseDRepository = secuenceDRepository
        _authorizationScheduleTemplateRepository = authorizationScheduleTemplateRepository
        Me._IUserAdminService = IUserAdminService
    End Sub

    Public Function ChangeStatAuthorizationScheduleTemplate(code As String, state As Boolean, audit As AuditMessage) As ActionResult(Of AuthorizationScheduleTemplate) Implements IAuthorizationScheduleTemplateAdminService.ChangeStateAuthorizationScheduleTemplate
        Dim AuthorizationScheduleTemplate As AuthorizationScheduleTemplate = _authorizationScheduleTemplateRepository.GetAuthorizationScheduleTemplateByCode(code)
        AuthorizationScheduleTemplate.Status = state
        Return SaveAuthorizationScheduleTemplate(AuthorizationScheduleTemplate, audit)
    End Function

    Public Function DeleteAuthorizationScheduleTemplate(AuthorizationScheduleTemplate As AuthorizationScheduleTemplate, audit As AuditMessage) As ActionResult Implements IAuthorizationScheduleTemplateAdminService.DeleteAuthorizationScheduleTemplate
        If AuthorizationScheduleTemplate Is Nothing Then
            Throw New ArgumentNullException("AuthorizationScheduleTemplate")
        End If
        Dim unitOfWork As IUnitWork = Me._authorizationScheduleTemplateRepository.UnitWork
        Try
            Dim auditProcess As IndigoAuditSimpleEntity(Of AuthorizationScheduleTemplate)
            auditProcess = New IndigoAuditSimpleEntity(Of AuthorizationScheduleTemplate)(AuthorizationScheduleTemplate, audit, Infrastructure.CrossCutting.Audit.Actions.Delete)

            While AuthorizationScheduleTemplate.AuthorizationScheduleTemplateUsers.Count > 0
                AuthorizationScheduleTemplate.AuthorizationScheduleTemplateUsers(AuthorizationScheduleTemplate.AuthorizationScheduleTemplateUsers.Count - 1).MarkAsDeleted()
            End While

            While AuthorizationScheduleTemplate.AuthorizationScheduleTemplateSchedule.Count > 0
                AuthorizationScheduleTemplate.AuthorizationScheduleTemplateSchedule(AuthorizationScheduleTemplate.AuthorizationScheduleTemplateSchedule.Count - 1).MarkAsDeleted()
            End While

            AuthorizationScheduleTemplate.MarkAsDeleted()

            Me._authorizationScheduleTemplateRepository.SaveEntity(AuthorizationScheduleTemplate)
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

    Public Function GetAuthorizationScheduleTemplate(code As String, audit As AuditMessage) As AuthorizationScheduleTemplate Implements IAuthorizationScheduleTemplateAdminService.GetAuthorizationScheduleTemplate
        If String.IsNullOrEmpty(code) Then
            Throw New ArgumentNullException("code")
        End If
        If audit Is Nothing Then
            Throw New ArgumentNullException("audit")
        End If
        Try
            Dim AuthorizationScheduleTemplate As AuthorizationScheduleTemplate = Me._authorizationScheduleTemplateRepository.GetAuthorizationScheduleTemplateByCode(code.Trim())
            If AuthorizationScheduleTemplate IsNot Nothing AndAlso AuthorizationScheduleTemplate.Id > 0 Then
                Dim auditObject As New IndigoAuditSimpleEntity(Of AuthorizationScheduleTemplate)(AuthorizationScheduleTemplate, audit, Infrastructure.CrossCutting.Audit.Actions.Print)
                auditObject.Execute()
            End If

            'Se saca el listado de ids de usuario para enviar
            Dim ListUsers As New List(Of Domain.Security.Entities.User)
            'Se consulta los usuarios por id para agregarles la descripcion
            Dim ListUserIds As New List(Of Integer)
            If AuthorizationScheduleTemplate IsNot Nothing AndAlso AuthorizationScheduleTemplate.Id > 0 AndAlso AuthorizationScheduleTemplate.AuthorizationScheduleTemplateUsers IsNot Nothing AndAlso AuthorizationScheduleTemplate.AuthorizationScheduleTemplateUsers.Count > 0 Then
                'Se recorre el listado de usuarios que trae la autorizacion para sacar los ids
                For Each u In AuthorizationScheduleTemplate.AuthorizationScheduleTemplateUsers
                    ListUserIds.Add(u.UserId)
                Next

                'Se obtiene el listado de usuarios
                ListUsers = _IUserAdminService.ListUsersByIds(ListUserIds)
                If ListUsers IsNot Nothing AndAlso ListUsers.Count > 0 Then
                    'Se asigna el nombre completo del usuario al listado que se va a asignar al datasource de la rejilla
                    For Each bu In AuthorizationScheduleTemplate.AuthorizationScheduleTemplateUsers
                        Dim user = (From e In ListUsers Where e.Id = bu.UserId Select e).FirstOrDefault
                        If user IsNot Nothing AndAlso user.Id > 0 AndAlso user.Person IsNot Nothing Then
                            bu.FullNameUser = user.Person.Fullname
                        End If
                    Next
                End If
            End If

            Return AuthorizationScheduleTemplate
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New AuthorizationScheduleTemplate
        End Try
    End Function

    Public Function GetAuthorizationScheduleTemplateById(id As Integer) As AuthorizationScheduleTemplate Implements IAuthorizationScheduleTemplateAdminService.GetAuthorizationScheduleTemplateById
        Try
            Return _authorizationScheduleTemplateRepository.GetAuthorizationScheduleTemplateById(id)
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New AuthorizationScheduleTemplate
        End Try
    End Function

    Public Function SaveAuthorizationScheduleTemplate(AuthorizationScheduleTemplate As AuthorizationScheduleTemplate, audit As AuditMessage, Optional idSequense As Long = 0) As ActionResult(Of AuthorizationScheduleTemplate) Implements IAuthorizationScheduleTemplateAdminService.SaveAuthorizationScheduleTemplate
        If AuthorizationScheduleTemplate Is Nothing Then
            Throw New ArgumentNullException("AuthorizationScheduleTemplate")
        End If
        Dim unitOfWork As IUnitWork = Me._authorizationScheduleTemplateRepository.UnitWork
        Dim sequenseUnitOfWork As IUnitWork = Me._secuenseDRepository.UnitWork
        Try

            Using scope As New TransactionScope(TransactionScopeOption.Required, New TransactionOptions() With {.Timeout = TransactionManager.MaximumTimeout, .IsolationLevel = IsolationLevel.ReadCommitted})

                Dim seq As AuthorizationSequenceDetail = Nothing
                If AuthorizationScheduleTemplate.Code Is Nothing OrElse AuthorizationScheduleTemplate.Code.Trim().Equals(String.Empty) Then
                    seq = Me._secuenseDRepository.GetSequenseDById(idSequense)
                    If seq IsNot Nothing AndAlso seq.Id > 0 AndAlso seq.AuthorizationSequence.Sequential Then
                        Dim res = Infrastructure.CrossCutting.Base.Sequense.GetSequense(seq.Sequense.Pattern, seq.Next)
                        If res IsNot Nothing AndAlso Not res.Equals(Infrastructure.CrossCutting.Base.Sequense.ERROR_MAXVALUE) Then
                            AuthorizationScheduleTemplate.Code = res
                            seq.Next += 1
                            Me._secuenseDRepository.SaveEntity(seq)
                        Else
                            scope.Dispose()
                            Return New ActionResult(Of AuthorizationScheduleTemplate) With {.StatusCode = eStatusResult.WARNING, .StateResult = False, .Message = ResourceManager.GetString("SequenceNotFound")}
                        End If
                    Else
                        scope.Dispose()
                        Return New ActionResult(Of AuthorizationScheduleTemplate) With {.StatusCode = eStatusResult.WARNING, .StateResult = False, .Message = ResourceManager.GetString("SequenceNotFound")}
                    End If
                End If

                Dim auxAuthorizationScheduleTemplate As AuthorizationScheduleTemplate = Nothing
                Dim auditProcess As IndigoAuditSimpleEntity(Of AuthorizationScheduleTemplate)
                Dim status As Integer

                If AuthorizationScheduleTemplate.ChangeTracker.State = Domain.Base.Entities.ObjectState.Added Then
                    AuthorizationScheduleTemplate.CreationUser = audit.CodeUser
                    AuthorizationScheduleTemplate.CreationDate = DateTime.Now
                    status = Infrastructure.CrossCutting.Audit.Actions.Insert
                Else
                    auxAuthorizationScheduleTemplate = AuthorizationScheduleTemplate.OriginalValue
                    AuthorizationScheduleTemplate.ModificationUser = audit.CodeUser
                    AuthorizationScheduleTemplate.ModificationDate = DateTime.Now
                    status = Infrastructure.CrossCutting.Audit.Actions.Update
                End If

                Me._authorizationScheduleTemplateRepository.SaveEntity(AuthorizationScheduleTemplate)
                unitOfWork.Commit()
                sequenseUnitOfWork.Commit()
                auditProcess = New IndigoAuditSimpleEntity(Of AuthorizationScheduleTemplate)(AuthorizationScheduleTemplate, audit, status, auxAuthorizationScheduleTemplate)
                auditProcess.Execute()

                'Se marca la entidad como sin cambios
                AuthorizationScheduleTemplate.MarkAsUnchanged()
                scope.Complete()
                Return New ActionResult(Of AuthorizationScheduleTemplate) With {.StateResult = True, .ObjectEmbbeded = AuthorizationScheduleTemplate}
            End Using

        Catch ex As OptimisticConcurrencyException
            unitOfWork.RollbackChanges()
            Return New ActionResult(Of AuthorizationScheduleTemplate) With {.StateResult = False, .MessageResult = {"-999"}.ToList()}
        Catch ex As Exception
            unitOfWork.RollbackChanges()
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of AuthorizationScheduleTemplate) With {.StateResult = False, .MessageResult = {ex.Message}.ToList}
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
            _authorizationScheduleTemplateRepository = Nothing
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
