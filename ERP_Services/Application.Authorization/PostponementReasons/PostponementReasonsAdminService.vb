#Region "Imports"

Imports System.Transactions
Imports Application.Base
Imports Application.Security
Imports Domain.Base
Imports Domain.Base.Entities
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.CrossCutting.Exceptions
Imports Infrastructure.CrossCutting.Resources

#End Region

Public Class PostponementReasonsAdminService
    Implements IPostponementReasonsAdminService

#Region "Builder"

    Private _secuenseDRepository As ISequenceAuthorizationDRepository
    Private _postponementReasonsRepository As IPostponementReasonsRepository
    Private _iUserAdminService As IUserAdminService

    Public Sub New(secuenceDRepository As ISequenceAuthorizationDRepository, PostponementReasonsRepository As IPostponementReasonsRepository, IUserAdminService As IUserAdminService)
        Me._secuenseDRepository = secuenceDRepository
        Me._postponementReasonsRepository = PostponementReasonsRepository
        Me._iUserAdminService = IUserAdminService
    End Sub

#End Region

#Region "Methods"

    Public Function GetPostponementReasonsById(id As Integer) As PostponementReasons Implements IPostponementReasonsAdminService.GetPostponementReasonsById
        Try
            Return _postponementReasonsRepository.GetPostponementReasonsById(id)
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New PostponementReasons
        End Try
    End Function

    Public Function GetPostponementReasons(code As String, audit As AuditMessage) As PostponementReasons Implements IPostponementReasonsAdminService.GetPostponementReasons
        Try
            Dim PostponementReasons As PostponementReasons = Me._postponementReasonsRepository.GetPostponementReasonsByCode(code.Trim())
            If PostponementReasons IsNot Nothing AndAlso PostponementReasons.Id > 0 Then
                Dim auditObject As New IndigoAuditSimpleEntity(Of PostponementReasons)(PostponementReasons, audit, Infrastructure.CrossCutting.Audit.Actions.Print)
                auditObject.Execute()
            End If

            'Se saca el listado de ids de usuario para enviar
            Dim ListUsers As New List(Of Domain.Security.Entities.User)
            'Se consulta los usuarios por id para agregarles la descripcion
            Dim ListUserIds As New List(Of Integer)
            If PostponementReasons IsNot Nothing AndAlso PostponementReasons.Id > 0 AndAlso PostponementReasons.PostponementReasonsUser IsNot Nothing AndAlso PostponementReasons.PostponementReasonsUser.Count > 0 Then
                'Se recorre el listado de usuarios que trae la autorizacion para sacar los ids
                For Each u In PostponementReasons.PostponementReasonsUser
                    ListUserIds.Add(u.UserId)
                Next

                'Se obtiene el listado de usuarios
                ListUsers = _iUserAdminService.ListUsersByIds(ListUserIds)
                If ListUsers IsNot Nothing AndAlso ListUsers.Count > 0 Then
                    'Se asigna el nombre completo del usuario al listado que se va a asignar al datasource de la rejilla
                    For Each bu In PostponementReasons.PostponementReasonsUser
                        Dim user = (From e In ListUsers Where e.Id = bu.UserId Select e).FirstOrDefault
                        If user IsNot Nothing AndAlso user.Id > 0 AndAlso user.Person IsNot Nothing Then
                            bu.FullNameUser = user.Person.Fullname
                        End If
                    Next
                End If
            End If

            Return PostponementReasons
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New PostponementReasons
        End Try
    End Function

    Public Function SavePostponementReasons(PostponementReasons As PostponementReasons, audit As AuditMessage, Optional idSequense As Long = 0) As ActionResult(Of PostponementReasons) Implements IPostponementReasonsAdminService.SavePostponementReasons
        If PostponementReasons Is Nothing Then
            Throw New ArgumentNullException("PostponementReasons")
        End If
        Dim unitOfWork As IUnitWork = Me._postponementReasonsRepository.UnitWork
        Dim sequenseUnitOfWork As IUnitWork = Me._secuenseDRepository.UnitWork
        Try
            Using scope As New TransactionScope(TransactionScopeOption.Required, New TransactionOptions() With {.Timeout = TransactionManager.MaximumTimeout, .IsolationLevel = IsolationLevel.ReadCommitted})
                Dim seq As AuthorizationSequenceDetail = Nothing
                If PostponementReasons.Code Is Nothing OrElse PostponementReasons.Code.Trim().Equals(String.Empty) Then
                    seq = Me._secuenseDRepository.GetSequenseDById(idSequense)
                    If seq IsNot Nothing AndAlso seq.Id > 0 AndAlso seq.AuthorizationSequence.Sequential Then
                        Dim res = Infrastructure.CrossCutting.Base.Sequense.GetSequense(seq.Sequense.Pattern, seq.Next)
                        If res IsNot Nothing AndAlso Not res.Equals(Infrastructure.CrossCutting.Base.Sequense.ERROR_MAXVALUE) Then
                            PostponementReasons.Code = res
                            seq.Next += 1
                            Me._secuenseDRepository.SaveEntity(seq)
                            sequenseUnitOfWork.Commit()
                        Else
                            scope.Dispose()
                            Return New ActionResult(Of PostponementReasons) With {.StatusCode = eStatusResult.WARNING, .StateResult = False, .Message = ResourceManager.GetString("SequenceNotFound")}
                        End If
                    Else
                        scope.Dispose()
                        Return New ActionResult(Of PostponementReasons) With {.StatusCode = eStatusResult.WARNING, .StateResult = False, .Message = ResourceManager.GetString("SequenceNotFound")}
                    End If
                End If

                Dim auxPostponementReasons As PostponementReasons = Nothing
                Dim auditProcess As IndigoAuditSimpleEntity(Of PostponementReasons)
                Dim status As Integer

                If PostponementReasons.ChangeTracker.State = Domain.Base.Entities.ObjectState.Added Then
                    PostponementReasons.CreationUser = audit.CodeUser
                    PostponementReasons.CreationDate = DateTime.Now
                    status = Infrastructure.CrossCutting.Audit.Actions.Insert
                Else
                    auxPostponementReasons = PostponementReasons.OriginalValue
                    PostponementReasons.ModificationUser = audit.CodeUser
                    PostponementReasons.ModificationDate = DateTime.Now
                    status = Infrastructure.CrossCutting.Audit.Actions.Update
                End If

                Me._postponementReasonsRepository.SaveEntity(PostponementReasons)
                unitOfWork.Commit()

                auditProcess = New IndigoAuditSimpleEntity(Of PostponementReasons)(PostponementReasons, audit, status, auxPostponementReasons)
                auditProcess.Execute()

                'Se marca la entidad como sin cambios
                PostponementReasons.MarkAsUnchanged()
                scope.Complete()
                Return New ActionResult(Of PostponementReasons) With {.StateResult = True, .ObjectEmbbeded = PostponementReasons}
            End Using
        Catch ex As OptimisticConcurrencyException
            unitOfWork.RollbackChanges()
            Return New ActionResult(Of PostponementReasons) With {.StateResult = False, .MessageResult = {"-999"}.ToList()}
        Catch ex As Exception
            unitOfWork.RollbackChanges()
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of PostponementReasons) With {.StateResult = False, .MessageResult = {ex.Message}.ToList}
        End Try
    End Function

    Public Function ChangeStatePostponementReasons(code As String, state As Boolean, audit As AuditMessage) As ActionResult(Of PostponementReasons) Implements IPostponementReasonsAdminService.ChangeStatePostponementReasons
        Dim PostponementReasons As PostponementReasons = _postponementReasonsRepository.GetPostponementReasonsByCode(code)
        PostponementReasons.Status = state
        Return SavePostponementReasons(PostponementReasons, audit)
    End Function

    Public Function DeletePostponementReasons(PostponementReasons As PostponementReasons, audit As AuditMessage) As ActionResult Implements IPostponementReasonsAdminService.DeletePostponementReasons
        If PostponementReasons Is Nothing Then
            Throw New ArgumentNullException("PostponementReasons")
        End If
        Dim unitOfWork As IUnitWork = Me._postponementReasonsRepository.UnitWork
        Try
            Dim auditProcess As IndigoAuditSimpleEntity(Of PostponementReasons)
            auditProcess = New IndigoAuditSimpleEntity(Of PostponementReasons)(PostponementReasons, audit, Infrastructure.CrossCutting.Audit.Actions.Delete)

            While PostponementReasons.PostponementReasonsUser.Count > 0
                PostponementReasons.PostponementReasonsUser(PostponementReasons.PostponementReasonsUser.Count - 1).MarkAsDeleted()
            End While
            PostponementReasons.MarkAsDeleted()

            Me._postponementReasonsRepository.SaveEntity(PostponementReasons)
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

#End Region

#Region "IDisposable Support"
    Private disposedValue As Boolean ' Para detectar llamadas redundantes

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not disposedValue Then
            If disposing Then
                ' TODO: elimine el estado administrado (objetos administrados).
            End If
            _secuenseDRepository = Nothing
            _postponementReasonsRepository = Nothing
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
