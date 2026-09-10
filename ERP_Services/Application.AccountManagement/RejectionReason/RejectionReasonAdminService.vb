'************************************************************
' Assembly         : Application
' Author           : Andrés Steven Rojas
' Created          : 07-01-2025
'
' Copyright        : (c) . All rights reserved.
'************************************************************
#Region "Imports"
Imports Domain.Entities
Imports Domain.Base
Imports Infrastructure.CrossCutting.Exceptions
Imports Application.Base
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities
Imports Infrastructure.CrossCutting.Resources
Imports System.Data.Entity.Core
Imports Application.Security
Imports System.Transactions
Imports System.Data.Entity.Infrastructure

#End Region

Public Class RejectionReasonAdminService
    Implements IRejectionReasonAdminService, Inject
    ''' <summary>
    ''' Nombre del formulario
    ''' </summary>
    Private Const FORM_NAME As String = "FrmRejectionReason"
    ''' <summary>
    ''' Repositorio de los Motivos de Rechazo
    ''' </summary>
    Private _rejectionReasonRepository As IRejectionReasonRepository
    ''' <summary>
    ''' Repositorio de la secuencia del módulo
    ''' </summary>
    Private _secuenceDRepository As IAccountManagementSequenceDetailRepository
    ''' <summary>
    ''' Repositorio de usuarios
    ''' </summary>
    Private _iUserAdminService As IUserAdminService

#Region "Builder"
    Public Sub New(rejectionReasonRepository As IRejectionReasonRepository, secuenceDRepository As IAccountManagementSequenceDetailRepository, iUserAdminService As IUserAdminService)
        If rejectionReasonRepository Is Nothing Then
            Throw New ArgumentNullException("rejectionReasonRepository vacío")
        End If
        If secuenceDRepository Is Nothing Then
            Throw New ArgumentNullException("secuenceDRepository vacío")
        End If
        If iUserAdminService Is Nothing Then
            Throw New ArgumentNullException("iUserAdminService vacío")
        End If

        _rejectionReasonRepository = rejectionReasonRepository
        _secuenceDRepository = secuenceDRepository
        _iUserAdminService = iUserAdminService
    End Sub
#End Region

    ''' <summary>
    ''' Lista los motivos de rechazo de acuerdo al código de usuario
    ''' </summary>
    ''' <param name="userCode"></param>
    ''' <returns></returns>
    Public Function ListRejectionReasonByUserCode(userCode As String) As List(Of RejectionReason) Implements IRejectionReasonAdminService.ListRejectionReasonByUserCode
        If String.IsNullOrEmpty(userCode) Then Throw New ArgumentNullException("userCode Vacio")

        Try
            Return _rejectionReasonRepository.ListRejectionReasonByUserCode(userCode)
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return Nothing
        End Try
    End Function
    ''' <summary>
    ''' Obtiene el Motivo de Rechazo por Id
    ''' </summary>
    ''' <param name="id"></param>
    ''' <returns></returns>
    Public Function GetRejectionReasonById(id As Integer) As ActionResult(Of RejectionReason) Implements IRejectionReasonAdminService.GetRejectionReasonById
        If id = 0 Then Throw New ArgumentNullException("id")

        Try
            Dim rejectionReason = _rejectionReasonRepository.GetRejectionReasonById(id)
            Return New ActionResult(Of RejectionReason) With {.StateResult = True, .ObjectEmbbeded = rejectionReason}
        Catch ex As Exception
            Return New ActionResult(Of RejectionReason) With {.StateResult = False, .Message = ex.Message}
        End Try
    End Function
    ''' <summary>
    ''' Obtiene el Motivo de Rezhado por Código
    ''' </summary>
    ''' <param name="code"></param>
    ''' <returns></returns>
    Public Function GetRejectionReasonByCode(code As String) As ActionResult(Of RejectionReason) Implements IRejectionReasonAdminService.GetRejectionReasonByCode
        If String.IsNullOrEmpty(code) Then Throw New ArgumentNullException("code")

        Try
            Dim rejectionReason = _rejectionReasonRepository.GetRejectionReasonByCode(code)

            'Se saca el listado de ids de usuario para enviar
            Dim ListUsers As New List(Of Domain.Security.Entities.User)
            'Se consulta los usuarios por id para agregarles la descripcion
            Dim ListUserIds As New List(Of Integer)
            If rejectionReason IsNot Nothing AndAlso rejectionReason.Id > 0 AndAlso rejectionReason.RejectionReasonUser IsNot Nothing AndAlso rejectionReason.RejectionReasonUser.Count > 0 Then
                'Se recorre el listado de usuarios que trae la autorizacion para sacar los ids
                For Each bu In rejectionReason.RejectionReasonUser
                    ListUserIds.Add(bu.UserId)
                Next

                'Se obtiene el listado de usuarios
                ListUsers = _iUserAdminService.ListUsersByIds(ListUserIds)
                If ListUsers IsNot Nothing AndAlso ListUsers.Count > 0 Then
                    'Se asigna el nombre completo del usuario al listado que se va a asignar al datasource de la rejilla
                    For Each ru In rejectionReason.RejectionReasonUser
                        Dim user = (From e In ListUsers Where e.Id = ru.UserId Select e).FirstOrDefault
                        If user IsNot Nothing AndAlso user.Id > 0 AndAlso user.Person IsNot Nothing Then
                            ru.FullNameUser = user.Person.Fullname
                        End If
                    Next
                End If
            End If

            Return New ActionResult(Of RejectionReason) With {.StateResult = True, .ObjectEmbbeded = rejectionReason}
        Catch ex As Exception
            Return New ActionResult(Of RejectionReason) With {.StateResult = False, .Message = ex.Message}
        End Try
    End Function
    ''' <summary>
    ''' Guarda o actualiza el Motivo de Rechazo
    ''' </summary>
    ''' <param name="rejectionReason"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    Public Function SaveRejectionReason(rejectionReason As RejectionReason, audit As AuditMessage, Optional idSecuence As Long = 0) As ActionResult(Of RejectionReason) Implements IRejectionReasonAdminService.SaveRejectionReason
        If rejectionReason Is Nothing Then Throw New ArgumentNullException("rejectionReason")
        If audit Is Nothing Then Throw New ArgumentNullException("audit")

        Dim unitOfWork As IUnitWork = Me._rejectionReasonRepository.UnitWork
        Dim secuenceUnitOfWork As IUnitWork = Me._secuenceDRepository.UnitWork

        Try
            Using scope As New TransactionScope(TransactionScopeOption.Required, New TransactionOptions() With {.Timeout = TransactionManager.MaximumTimeout, .IsolationLevel = IsolationLevel.ReadCommitted})

                Dim messageResult As String = String.Empty
                If String.IsNullOrEmpty(rejectionReason.Code) Then
                    Dim seq As AccountManagementSequenceDetail = Me._secuenceDRepository.GetSequenseDById(idSecuence)
                    If seq IsNot Nothing AndAlso seq.Id > 0 AndAlso seq.AccountManagementSequence.Sequential Then
                        Dim res = Infrastructure.CrossCutting.Base.Sequense.GetSequense(seq.Sequense.Pattern, seq.Next)
                        If res IsNot Nothing AndAlso Not res.Equals(Infrastructure.CrossCutting.Base.Sequense.ERROR_MAXVALUE) Then
                            rejectionReason.Code = res
                            seq.Next += 1
                            Me._secuenceDRepository.SaveEntity(seq)
                        Else
                            scope.Dispose()
                            Return New ActionResult(Of RejectionReason) With {.StatusCode = eStatusResult.WARNING, .StateResult = False, .MessageResult = {"_Seq02_"}.ToList(), .Message = String.Format(ResourceManager.GetString("SequenceFormNotFound"), FORM_NAME)}
                        End If
                        messageResult = If(seq.AccountManagementSequence.Sequential, String.Format(ResourceManager.GetString("SavedWithCode"), rejectionReason.Code), ResourceManager.GetString("SaveMessage"))
                    Else
                        scope.Dispose()
                        Return New ActionResult(Of RejectionReason) With {.StatusCode = eStatusResult.WARNING, .StateResult = False, .MessageResult = {"_Seq02_"}.ToList(), .Message = String.Format(ResourceManager.GetString("SequenceFormNotFound"), FORM_NAME)}
                    End If
                Else
                    messageResult = ResourceManager.GetString("SaveMessage")
                End If

                Dim auxObjEntity As RejectionReason = Nothing
                Dim auditProcess As IndigoAuditSimpleEntity(Of RejectionReason)
                Dim status As Integer

                If rejectionReason.ChangeTracker.State = ObjectState.Added Then
                    rejectionReason.CreationUser = audit.CodeUser
                    rejectionReason.CreationDate = DateTime.Now
                    status = Infrastructure.CrossCutting.Audit.Actions.Insert
                Else
                    messageResult = ResourceManager.GetString("UpdateMessage")
                    rejectionReason.ModificationUser = audit.CodeUser
                    rejectionReason.ModificationDate = DateTime.Now
                    auxObjEntity = rejectionReason.OriginalValue
                    status = Infrastructure.CrossCutting.Audit.Actions.Update
                End If

                Me._rejectionReasonRepository.SaveEntity(rejectionReason)
                unitOfWork.Commit()
                secuenceUnitOfWork.Commit()
                auditProcess = New IndigoAuditSimpleEntity(Of RejectionReason)(rejectionReason, audit, status, auxObjEntity)
                auditProcess.Execute()

                'Se marca la entidad sin cambios
                rejectionReason.MarkAsUnchanged()
                scope.Complete()
                Return New ActionResult(Of RejectionReason) With {.StateResult = True, .StatusCode = eStatusResult.SUCCESS, .ObjectEmbbeded = rejectionReason, .Message = messageResult}
            End Using
        Catch ex As Exception
            unitOfWork.RollbackChanges()
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of RejectionReason) With {.StateResult = False, .StatusCode = eStatusResult.EXCEPTION, .Message = IndigoManagementExceptions.GetExceptionDetails(ex)}
        End Try
    End Function
    ''' <summary>
    ''' Elimina el Motivo de Rechazo
    ''' </summary>
    ''' <param name="rejectionReason"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    Public Function DeleteRejectionReason(rejectionReason As RejectionReason, audit As AuditMessage) As ActionResult Implements IRejectionReasonAdminService.DeleteRejectionReason
        If rejectionReason Is Nothing Then Throw New ArgumentNullException("rejectionReason")

        Dim UnitOfWork As IUnitWork = Me._rejectionReasonRepository.UnitWork
        Try
            Using scope As New TransactionScope(TransactionScopeOption.Required, New TransactionOptions() With {.Timeout = TransactionManager.MaximumTimeout, .IsolationLevel = IsolationLevel.ReadCommitted})

                rejectionReason.ModificationUser = audit.CodeUser
                rejectionReason.ModificationDate = DateTime.Now
                Dim status As Integer = Infrastructure.CrossCutting.Audit.Actions.Delete
                Dim auditProcess As New IndigoAuditSimpleEntity(Of RejectionReason)(rejectionReason, audit, status)

                While rejectionReason.RejectionReasonUser.Count > 0
                    rejectionReason.RejectionReasonUser(rejectionReason.RejectionReasonUser.Count - 1).MarkAsDeleted
                End While

                rejectionReason.MarkAsDeleted()
                Me._rejectionReasonRepository.SaveEntity(rejectionReason)
                UnitOfWork.Commit()
                auditProcess.Execute()
                scope.Complete()
                Return New ActionResult With {.StateResult = False, .StatusCode = eStatusResult.SUCCESS, .Message = ResourceManager.GetString("RecordDeleted")}
            End Using
        Catch ex As Exception
            UnitOfWork.RollbackChanges()
            Return New ActionResult With {.StateResult = False, .StatusCode = eStatusResult.EXCEPTION, .Message = IndigoManagementExceptions.GetExceptionDetails(ex)}
        End Try
    End Function
    ''' <summary>
    ''' Activa o Inactiva el Motivo de Rechazo
    ''' </summary>
    ''' <param name="code"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    Public Function ChangeStateRejectionReason(code As String, audit As AuditMessage) As ActionResult(Of RejectionReason) Implements IRejectionReasonAdminService.ChangeStateRejectionReason
        If String.IsNullOrEmpty(code) Then Throw New ArgumentNullException("code")
        If audit Is Nothing Then Throw New ArgumentNullException("audit")

        Try
            Dim rejectionReason = _rejectionReasonRepository.GetRejectionReasonByCode(code)
            If rejectionReason IsNot Nothing Then
                rejectionReason.Status = Not rejectionReason.Status
            End If

            Dim result = SaveRejectionReason(rejectionReason, audit)
            If result.StatusCode = eStatusResult.SUCCESS Then
                result.Message = ResourceManager.GetString("UpdateState")
            End If

            Return result
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of RejectionReason) With {.StateResult = False, .StatusCode = eStatusResult.EXCEPTION, .Message = IndigoManagementExceptions.GetExceptionDetails(ex)}
        End Try
    End Function

    ''' <summary>
    ''' Obtiene todas las razones de rechazo activas
    ''' </summary>
    ''' <returns></returns>
    Public Function ListAllRejectionReasons() As ActionResult(Of List(Of RejectionReason)) Implements IRejectionReasonAdminService.ListAllRejectionReasons
        Dim rejectionReasonList = _rejectionReasonRepository.GetByFilter(Function(r) r.Status = True)
        If rejectionReasonList IsNot Nothing AndAlso rejectionReasonList.Count > 0 Then
            Return New ActionResult(Of List(Of RejectionReason)) With {.StateResult = True, .ObjectEmbbeded = rejectionReasonList}
        Else
            Return New ActionResult(Of List(Of RejectionReason)) With {.StateResult = False, .Message = "No se han encontrado razones de rechazo activas."}
        End If
    End Function

#Region "IDisposable Support"
    Private disposedValue As Boolean ' Para detectar llamadas redundantes

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not disposedValue Then
            If disposing Then
                _iUserAdminService.Dispose()
            End If
            _secuenceDRepository = Nothing
            _rejectionReasonRepository = Nothing
            _iUserAdminService = Nothing
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
