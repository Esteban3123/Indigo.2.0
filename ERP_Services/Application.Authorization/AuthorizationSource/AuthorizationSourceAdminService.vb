'***********************************************************************
' Assembly         : Application.Authorization
' Author           : Carlos Mario Arias Rubiano
' Created          : 04/03/2020
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports System.Transactions
Imports Application.Base
Imports Domain.Base
Imports Domain.Base.Entities
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.CrossCutting.Exceptions
Imports Infrastructure.CrossCutting.Resources

Public Class AuthorizationSourceAdminService
    Implements IAuthorizationSourceAdminService

    ''' <summary>
    ''' repositorio de las secuencias
    ''' </summary>
    Private _secuenseDRepository As ISequenceAuthorizationDRepository
    Private _authorizationSourceRepository As IAuthorizationSourceRepository

    Public Sub New(secuenceDRepository As ISequenceAuthorizationDRepository, authorizationSourceRepository As IAuthorizationSourceRepository)
        If secuenceDRepository Is Nothing Then
            Throw New ArgumentNullException("secuenceDRepository")
        End If
        If authorizationSourceRepository Is Nothing Then
            Throw New ArgumentNullException("authorizationSourceRepository")
        End If
        _secuenseDRepository = secuenceDRepository
        _authorizationSourceRepository = authorizationSourceRepository
    End Sub

    Public Function ChangeStateAuthorizationSource(code As String, state As Boolean, audit As AuditMessage) As ActionResult(Of AuthorizationSource) Implements IAuthorizationSourceAdminService.ChangeStateAuthorizationSource
        Dim AuthorizationSource As AuthorizationSource = _authorizationSourceRepository.GetAuthorizationSourceByCode(code)
        AuthorizationSource.Status = state
        Return SaveAuthorizationSource(AuthorizationSource, audit)
    End Function

    Public Function DeleteAuthorizationSource(AuthorizationSource As AuthorizationSource, audit As AuditMessage) As ActionResult Implements IAuthorizationSourceAdminService.DeleteAuthorizationSource
        If AuthorizationSource Is Nothing Then
            Throw New ArgumentNullException("AuthorizationGroup")
        End If
        Dim unitOfWork As IUnitWork = Me._authorizationSourceRepository.UnitWork
        Try
            Dim auditProcess As IndigoAuditSimpleEntity(Of AuthorizationSource)
            auditProcess = New IndigoAuditSimpleEntity(Of AuthorizationSource)(AuthorizationSource, audit, Infrastructure.CrossCutting.Audit.Actions.Delete)

            AuthorizationSource.MarkAsDeleted()

            Me._authorizationSourceRepository.SaveEntity(AuthorizationSource)
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

    Public Function GetAuthorizationSource(code As String, audit As AuditMessage) As AuthorizationSource Implements IAuthorizationSourceAdminService.GetAuthorizationSource
        If String.IsNullOrEmpty(code) Then
            Throw New ArgumentNullException("code")
        End If
        If audit Is Nothing Then
            Throw New ArgumentNullException("audit")
        End If
        Try
            Dim AuthorizationSource As AuthorizationSource = Me._authorizationSourceRepository.GetAuthorizationSourceByCode(code.Trim())
            If AuthorizationSource IsNot Nothing AndAlso AuthorizationSource.Id > 0 Then
                Dim auditObject As New IndigoAuditSimpleEntity(Of AuthorizationSource)(AuthorizationSource, audit, Infrastructure.CrossCutting.Audit.Actions.Print)
                auditObject.Execute()
            End If
            Return AuthorizationSource
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New AuthorizationSource
        End Try
    End Function

    Public Function GetAuthorizationSourceById(id As Integer) As AuthorizationSource Implements IAuthorizationSourceAdminService.GetAuthorizationSourceById
        Try
            Return _authorizationSourceRepository.GetAuthorizationSourceById(id)
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New AuthorizationSource
        End Try
    End Function

    Public Function SaveAuthorizationSource(AuthorizationSource As AuthorizationSource, audit As AuditMessage, Optional idSequense As Long = 0) As ActionResult(Of AuthorizationSource) Implements IAuthorizationSourceAdminService.SaveAuthorizationSource
        If AuthorizationSource Is Nothing Then
            Throw New ArgumentNullException("AuthorizationSource")
        End If
        Dim unitOfWork As IUnitWork = Me._authorizationSourceRepository.UnitWork
        Dim sequenseUnitOfWork As IUnitWork = Me._secuenseDRepository.UnitWork
        Try

            Using scope As New TransactionScope(TransactionScopeOption.Required, New TransactionOptions() With {.Timeout = TransactionManager.MaximumTimeout, .IsolationLevel = IsolationLevel.ReadCommitted})

                Dim seq As AuthorizationSequenceDetail = Nothing
                If AuthorizationSource.Code Is Nothing OrElse AuthorizationSource.Code.Trim().Equals(String.Empty) Then
                    seq = Me._secuenseDRepository.GetSequenseDById(idSequense)
                    If seq IsNot Nothing AndAlso seq.Id > 0 AndAlso seq.AuthorizationSequence.Sequential Then
                        Dim res = Infrastructure.CrossCutting.Base.Sequense.GetSequense(seq.Sequense.Pattern, seq.Next)
                        If res IsNot Nothing AndAlso Not res.Equals(Infrastructure.CrossCutting.Base.Sequense.ERROR_MAXVALUE) Then
                            AuthorizationSource.Code = res
                            seq.Next += 1
                            Me._secuenseDRepository.SaveEntity(seq)
                        Else
                            scope.Dispose()
                            Return New ActionResult(Of AuthorizationSource) With {.StatusCode = eStatusResult.WARNING, .StateResult = False, .Message = ResourceManager.GetString("SequenceNotFound")}
                        End If
                    Else
                        scope.Dispose()
                        Return New ActionResult(Of AuthorizationSource) With {.StatusCode = eStatusResult.WARNING, .StateResult = False, .Message = ResourceManager.GetString("SequenceNotFound")}
                    End If
                End If

                Dim auxAuthorizationSource As AuthorizationSource = Nothing
                Dim auditProcess As IndigoAuditSimpleEntity(Of AuthorizationSource)
                Dim status As Integer

                If AuthorizationSource.ChangeTracker.State = Domain.Base.Entities.ObjectState.Added Then
                    AuthorizationSource.CreationUser = audit.CodeUser
                    AuthorizationSource.CreationDate = DateTime.Now
                    status = Infrastructure.CrossCutting.Audit.Actions.Insert
                Else
                    auxAuthorizationSource = AuthorizationSource.OriginalValue
                    AuthorizationSource.ModificationUser = audit.CodeUser
                    AuthorizationSource.ModificationDate = DateTime.Now
                    status = Infrastructure.CrossCutting.Audit.Actions.Update
                End If

                Me._authorizationSourceRepository.SaveEntity(AuthorizationSource)
                unitOfWork.Commit()
                sequenseUnitOfWork.Commit()
                auditProcess = New IndigoAuditSimpleEntity(Of AuthorizationSource)(AuthorizationSource, audit, status, auxAuthorizationSource)
                auditProcess.Execute()

                'Se marca la entidad como sin cambios
                AuthorizationSource.MarkAsUnchanged()
                scope.Complete()
                Return New ActionResult(Of AuthorizationSource) With {.StateResult = True, .ObjectEmbbeded = AuthorizationSource}
            End Using

        Catch ex As OptimisticConcurrencyException
            unitOfWork.RollbackChanges()
            Return New ActionResult(Of AuthorizationSource) With {.StateResult = False, .MessageResult = {"-999"}.ToList()}
        Catch ex As Exception
            unitOfWork.RollbackChanges()
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of AuthorizationSource) With {.StateResult = False, .MessageResult = {ex.Message}.ToList}
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
            _authorizationSourceRepository = Nothing
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
