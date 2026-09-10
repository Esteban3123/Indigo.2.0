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

Public Class CancellationReasonsAdminService
    Implements ICancellationReasonsAdminService

    ''' <summary>
    ''' repositorio de las secuencias
    ''' </summary>
    Private _secuenseDRepository As ISequenceAuthorizationDRepository
    Private _cancellationReasonsRepository As ICancellationReasonsRepository
    Private _IUserAdminService As IUserAdminService

    Public Sub New(secuenceDRepository As ISequenceAuthorizationDRepository, cancellationReasonsRepository As ICancellationReasonsRepository, IUserAdminService As IUserAdminService)
        If secuenceDRepository Is Nothing Then
            Throw New ArgumentNullException("secuenceDRepository")
        End If
        If cancellationReasonsRepository Is Nothing Then
            Throw New ArgumentNullException("cancellationReasonsRepository")
        End If
        If IUserAdminService Is Nothing Then
            Throw New ArgumentNullException("IUserAdminService vacío")
        End If
        _secuenseDRepository = secuenceDRepository
        _cancellationReasonsRepository = cancellationReasonsRepository
        Me._IUserAdminService = IUserAdminService
    End Sub

    Public Function ChangeStateCancellationReasons(code As String, state As Boolean, audit As AuditMessage) As ActionResult(Of CancellationReasons) Implements ICancellationReasonsAdminService.ChangeStateCancellationReasons
        Dim CancellationReasons As CancellationReasons = _cancellationReasonsRepository.GetCancellationReasonsByCode(code)
        CancellationReasons.Status = state
        Return SaveCancellationReasons(CancellationReasons, audit)
    End Function

    Public Function DeleteCancellationReasons(CancellationReasons As CancellationReasons, audit As AuditMessage) As ActionResult Implements ICancellationReasonsAdminService.DeleteCancellationReasons
        If CancellationReasons Is Nothing Then
            Throw New ArgumentNullException("CancellationReasons")
        End If
        Dim unitOfWork As IUnitWork = Me._cancellationReasonsRepository.UnitWork
        Try
            Dim auditProcess As IndigoAuditSimpleEntity(Of CancellationReasons)
            auditProcess = New IndigoAuditSimpleEntity(Of CancellationReasons)(CancellationReasons, audit, Infrastructure.CrossCutting.Audit.Actions.Delete)

            While CancellationReasons.CancellationReasonsUser.Count > 0
                CancellationReasons.CancellationReasonsUser(CancellationReasons.CancellationReasonsUser.Count - 1).MarkAsDeleted()
            End While
            CancellationReasons.MarkAsDeleted()

            Me._cancellationReasonsRepository.SaveEntity(CancellationReasons)
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

    Public Function GetCancellationReasons(code As String, audit As AuditMessage) As CancellationReasons Implements ICancellationReasonsAdminService.GetCancellationReasons
        If String.IsNullOrEmpty(code) Then
            Throw New ArgumentNullException("code")
        End If
        If audit Is Nothing Then
            Throw New ArgumentNullException("audit")
        End If
        Try
            Dim CancellationReasons As CancellationReasons = Me._cancellationReasonsRepository.GetCancellationReasonsByCode(code.Trim())
            If CancellationReasons IsNot Nothing AndAlso CancellationReasons.Id > 0 Then
                Dim auditObject As New IndigoAuditSimpleEntity(Of CancellationReasons)(CancellationReasons, audit, Infrastructure.CrossCutting.Audit.Actions.Print)
                auditObject.Execute()
            End If

            'Se saca el listado de ids de usuario para enviar
            Dim ListUsers As New List(Of Domain.Security.Entities.User)
            'Se consulta los usuarios por id para agregarles la descripcion
            Dim ListUserIds As New List(Of Integer)
            If CancellationReasons IsNot Nothing AndAlso CancellationReasons.Id > 0 AndAlso CancellationReasons.CancellationReasonsUser IsNot Nothing AndAlso CancellationReasons.CancellationReasonsUser.Count > 0 Then
                'Se recorre el listado de usuarios que trae la autorizacion para sacar los ids
                For Each u In CancellationReasons.CancellationReasonsUser
                    ListUserIds.Add(u.UserId)
                Next

                'Se obtiene el listado de usuarios
                ListUsers = _IUserAdminService.ListUsersByIds(ListUserIds)
                If ListUsers IsNot Nothing AndAlso ListUsers.Count > 0 Then
                    'Se asigna el nombre completo del usuario al listado que se va a asignar al datasource de la rejilla
                    For Each bu In CancellationReasons.CancellationReasonsUser
                        Dim user = (From e In ListUsers Where e.Id = bu.UserId Select e).FirstOrDefault
                        If user IsNot Nothing AndAlso user.Id > 0 AndAlso user.Person IsNot Nothing Then
                            bu.FullNameUser = user.Person.Fullname
                        End If
                    Next
                End If
            End If

            Return CancellationReasons
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New CancellationReasons
        End Try
    End Function

    Public Function GetCancellationReasonsById(id As Integer) As CancellationReasons Implements ICancellationReasonsAdminService.GetCancellationReasonsById
        Try
            Return _cancellationReasonsRepository.GetCancellationReasonsById(id)
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New CancellationReasons
        End Try
    End Function

    Public Function SaveCancellationReasons(CancellationReasons As CancellationReasons, audit As AuditMessage, Optional idSequense As Long = 0) As ActionResult(Of CancellationReasons) Implements ICancellationReasonsAdminService.SaveCancellationReasons
        If CancellationReasons Is Nothing Then
            Throw New ArgumentNullException("CancellationReasons")
        End If
        Dim unitOfWork As IUnitWork = Me._cancellationReasonsRepository.UnitWork
        Dim sequenseUnitOfWork As IUnitWork = Me._secuenseDRepository.UnitWork
        Try

            Using scope As New TransactionScope(TransactionScopeOption.Required, New TransactionOptions() With {.Timeout = TransactionManager.MaximumTimeout, .IsolationLevel = IsolationLevel.ReadCommitted})

                Dim seq As AuthorizationSequenceDetail = Nothing
                If CancellationReasons.Code Is Nothing OrElse CancellationReasons.Code.Trim().Equals(String.Empty) Then
                    seq = Me._secuenseDRepository.GetSequenseDById(idSequense)
                    If seq IsNot Nothing AndAlso seq.Id > 0 AndAlso seq.AuthorizationSequence.Sequential Then
                        Dim res = Infrastructure.CrossCutting.Base.Sequense.GetSequense(seq.Sequense.Pattern, seq.Next)
                        If res IsNot Nothing AndAlso Not res.Equals(Infrastructure.CrossCutting.Base.Sequense.ERROR_MAXVALUE) Then
                            CancellationReasons.Code = res
                            seq.Next += 1
                            Me._secuenseDRepository.SaveEntity(seq)
                        Else
                            scope.Dispose()
                            Return New ActionResult(Of CancellationReasons) With {.StatusCode = eStatusResult.WARNING, .StateResult = False, .Message = ResourceManager.GetString("SequenceNotFound")}
                        End If
                    Else
                        scope.Dispose()
                        Return New ActionResult(Of CancellationReasons) With {.StatusCode = eStatusResult.WARNING, .StateResult = False, .Message = ResourceManager.GetString("SequenceNotFound")}
                    End If
                End If

                Dim auxCancellationReasons As CancellationReasons = Nothing
                Dim auditProcess As IndigoAuditSimpleEntity(Of CancellationReasons)
                Dim status As Integer

                If CancellationReasons.ChangeTracker.State = Domain.Base.Entities.ObjectState.Added Then
                    CancellationReasons.CreationUser = audit.CodeUser
                    CancellationReasons.CreationDate = DateTime.Now
                    status = Infrastructure.CrossCutting.Audit.Actions.Insert
                Else
                    auxCancellationReasons = CancellationReasons.OriginalValue
                    CancellationReasons.ModificationUser = audit.CodeUser
                    CancellationReasons.ModificationDate = DateTime.Now
                    status = Infrastructure.CrossCutting.Audit.Actions.Update
                End If

                Me._cancellationReasonsRepository.SaveEntity(CancellationReasons)
                unitOfWork.Commit()
                sequenseUnitOfWork.Commit()
                auditProcess = New IndigoAuditSimpleEntity(Of CancellationReasons)(CancellationReasons, audit, status, auxCancellationReasons)
                auditProcess.Execute()

                'Se marca la entidad como sin cambios
                CancellationReasons.MarkAsUnchanged()
                scope.Complete()
                Return New ActionResult(Of CancellationReasons) With {.StateResult = True, .ObjectEmbbeded = CancellationReasons}
            End Using

        Catch ex As OptimisticConcurrencyException
            unitOfWork.RollbackChanges()
            Return New ActionResult(Of CancellationReasons) With {.StateResult = False, .MessageResult = {"-999"}.ToList()}
        Catch ex As Exception
            unitOfWork.RollbackChanges()
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of CancellationReasons) With {.StateResult = False, .MessageResult = {ex.Message}.ToList}
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
            _cancellationReasonsRepository = Nothing
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
