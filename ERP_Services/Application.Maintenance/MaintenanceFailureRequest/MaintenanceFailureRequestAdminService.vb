#Region "Imports"

Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities
Imports Infrastructure.CrossCutting.Exceptions
Imports Domain.Base
Imports Application.Base
Imports System.Data.Entity.Core
Imports Infrastructure.CrossCutting.Resources
Imports System.Transactions
Imports System.Data.Entity.Infrastructure
Imports System.Text
Imports Domain.Notification

#End Region

Public Class MaintenanceFailureRequestAdminService
    Implements IMaintenanceFailureRequestAdminService

#Region "Builder"

    Private Const FORM_NAME As String = "FrmMaintenanceFailureRequest"

    'Repositorio de tipo de ubicacion
    Private _MaintenanceRespository As IMaintenanceFailureRequestRepository

    '' <summary>
    '' repositorio de las secuencias
    '' </summary>
    Private _secuenseDRepository As IMaintenanceSequenceDetailRepository
    Private _maintenanceLogRepository As IMaintenanceLogRepository
    Private _maintenanceFailureRequestDetailNotificationRepository As IMaintenanceFailureRequestDetailNotificationRepository

    '' <summary>
    '' inicia el repositorio de ubicacion
    '' </summary>
    '' <param name="MaintenanceRepository">Repositorio de Responsable</param>
    '' <remarks></remarks>
    Public Sub New(secuenceDRepository As IMaintenanceSequenceDetailRepository,
                   MaintenanceRepository As IMaintenanceFailureRequestRepository,
                   maintenanceLogRepository As IMaintenanceLogRepository,
                   maintenanceFailureRequestDetailNotificationRepository As IMaintenanceFailureRequestDetailNotificationRepository)
        If (MaintenanceRepository Is Nothing) Then
            Throw New ArgumentNullException("Repositorio de MaintenanceRespository")
        End If

        Me._secuenseDRepository = secuenceDRepository
        Me._MaintenanceRespository = MaintenanceRepository
        Me._maintenanceLogRepository = maintenanceLogRepository
        Me._maintenanceFailureRequestDetailNotificationRepository = maintenanceFailureRequestDetailNotificationRepository
    End Sub

#End Region

#Region "Methods"

    Public Function MaintenanceFailureRequest(Code As String) As MaintenanceFailureRequest Implements IMaintenanceFailureRequestAdminService.GetMaintenanceFailureRequestByCode
        If String.IsNullOrEmpty(Code) Then
            Throw New ArgumentNullException("Codigo vacio")
        End If
        Try
            Return _MaintenanceRespository.GetMaintenanceFailureRequestByCode(Code)
        Catch ex As Exception
            IndigoManagementExceptions.HandleExceptionUI(ex, "ApplicationPolicy")
            Return New MaintenanceFailureRequest()
        End Try
    End Function

    Public Function ListAllMaintenanceFailureRequest() As List(Of MaintenanceFailureRequest) Implements IMaintenanceFailureRequestAdminService.ListAllMaintenanceFailureRequest
        Try
            Return _MaintenanceRespository.ListAllMaintenanceFailureRequest()
        Catch ex As Exception
            IndigoManagementExceptions.HandleExceptionUI(ex, "ApplicationPolicy")
            Return Nothing
        End Try
    End Function

    Public Function SaveMaintenanceFailureRequest(MaintenanceFailure As MaintenanceFailureRequest, audit As AuditMessage, Optional idSequense As Long = 0) As ActionResult(Of MaintenanceFailureRequest) Implements IMaintenanceFailureRequestAdminService.SaveMaintenanceFailureRequest
        If MaintenanceFailure Is Nothing Then
            Throw New ArgumentNullException("ObjEntity")
        End If
        Dim unitOfWork As IUnitWork = Me._MaintenanceRespository.UnitWork
        Dim sequenseUnitOfWork As IUnitWork = Me._secuenseDRepository.UnitWork
        Try
            Using scope As New TransactionScope(TransactionScopeOption.Required, New TransactionOptions() With {.Timeout = TransactionManager.MaximumTimeout, .IsolationLevel = IsolationLevel.ReadCommitted})
                Dim MessageResult As String = String.Empty

                If String.IsNullOrEmpty(MaintenanceFailure.Code) Then
                    Dim seq As MaintenanceSequenceDetail = Me._secuenseDRepository.GetSequenseDetailUpdatedById(idSequense)
                    If seq IsNot Nothing AndAlso seq.Id > 0 AndAlso seq.MaintenanceSequence.Sequential Then
                        Dim res = Infrastructure.CrossCutting.Base.Sequense.GetSequense(seq.Sequense.Pattern, seq.Next)
                        If res IsNot Nothing AndAlso Not res.Equals(Infrastructure.CrossCutting.Base.Sequense.ERROR_MAXVALUE) Then
                            MaintenanceFailure.Code = res
                            seq.Next += 1
                            Me._secuenseDRepository.SaveEntity(seq)
                        Else
                            scope.Dispose()
                            Return New ActionResult(Of MaintenanceFailureRequest) With {.StatusCode = eStatusResult.WARNING, .StateResult = False, .MessageResult = {"_Seq02_"}.ToList(), .Message = String.Format(ResourceManager.GetString("SequenceFormNotFound"), FORM_NAME)}
                        End If
                        MessageResult = If(seq.MaintenanceSequence.Sequential, String.Format(ResourceManager.GetString("SavedWithCode"), MaintenanceFailure.Code), ResourceManager.GetString("SaveMessage"))
                    Else
                        scope.Dispose()
                        Return New ActionResult(Of MaintenanceFailureRequest) With {.StatusCode = eStatusResult.WARNING, .StateResult = False, .MessageResult = {"_Seq02_"}.ToList(), .Message = String.Format(ResourceManager.GetString("SequenceFormNotFound"), FORM_NAME)}
                    End If
                Else
                    MessageResult = ResourceManager.GetString("SaveMessage")
                End If

                Dim auxObjEntity As MaintenanceFailureRequest = Nothing
                Dim auditProcess As IndigoAuditSimpleEntity(Of MaintenanceFailureRequest)
                Dim status As Integer

                If MaintenanceFailure.ChangeTracker.State = Domain.Base.Entities.ObjectState.Added Then
                    MaintenanceFailure.CreationUser = audit.CodeUser
                    MaintenanceFailure.CreationDate = DateTime.Now
                    status = Infrastructure.CrossCutting.Audit.Actions.Insert
                Else
                    MessageResult = ResourceManager.GetString("UpdateMessage")
                    auxObjEntity = MaintenanceFailure.OriginalValue
                    MaintenanceFailure.ModificationUser = audit.CodeUser
                    MaintenanceFailure.ModificationDate = DateTime.Now
                    status = Infrastructure.CrossCutting.Audit.Actions.Update
                End If

                Me._MaintenanceRespository.SaveEntity(MaintenanceFailure)
                unitOfWork.Commit()
                sequenseUnitOfWork.Commit()
                auditProcess = New IndigoAuditSimpleEntity(Of MaintenanceFailureRequest)(MaintenanceFailure, audit, status, auxObjEntity)
                auditProcess.Execute()

                'Se marca la entidad como sin cambios
                MaintenanceFailure.MarkAsUnchanged()
                scope.Complete()
                Return New ActionResult(Of MaintenanceFailureRequest) With {.StateResult = True, .StatusCode = eStatusResult.SUCCESS, .ObjectEmbbeded = MaintenanceFailure, .Message = MessageResult}
            End Using
        Catch ex As OptimisticConcurrencyException
            unitOfWork.RollbackChanges()
            Return New ActionResult(Of MaintenanceFailureRequest) With {.StateResult = False, .StatusCode = eStatusResult.WARNING, .MessageResult = {"-999"}.ToList(), .Message = ResourceManager.GetString("ErrorConcurrence")}
        Catch ex As Exception
            unitOfWork.RollbackChanges()
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of MaintenanceFailureRequest) With {.StateResult = False, .StatusCode = eStatusResult.EXCEPTION, .MessageResult = {ex.Message}.ToList, .Message = IndigoManagementExceptions.GetExceptionDetails(ex)}
        End Try
    End Function

    Public Function ConfirmMaintenanceFailureRequest(MaintenanceFailure As MaintenanceFailureRequest, audit As AuditMessage, Optional idSequense As Long = 0) As ActionResult(Of MaintenanceFailureRequest) Implements IMaintenanceFailureRequestAdminService.ConfirmMaintenanceFailureRequest
        Dim unitOfWork As IUnitWork = Me._MaintenanceRespository.UnitWork

        Dim txSettings As New TransactionOptions()
        txSettings.Timeout = TransactionManager.MaximumTimeout
        txSettings.IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted
        Using transaction As New TransactionScope(TransactionScopeOption.Required, txSettings)
            Try

                Dim resultEntry As ActionResult(Of MaintenanceFailureRequest) = SaveMaintenanceFailureRequest(MaintenanceFailure, audit, idSequense)
                If resultEntry.StateResult = False Then
                    transaction.Dispose()
                    Return New ActionResult(Of MaintenanceFailureRequest) With {.StateResult = False, .StatusCode = eStatusResult.WARNING, .Message = resultEntry.MessageResult(0).ToString}
                End If

                Dim message As New Text.StringBuilder
                message.AppendLine("El registro se guardó con código: " + resultEntry.ObjectEmbbeded.Code)

                transaction.Complete()
                Return New ActionResult(Of MaintenanceFailureRequest) With {.StateResult = True, .StatusCode = eStatusResult.SUCCESS, .ObjectEmbbeded = MaintenanceFailure, .Message = message.ToString}
            Catch ex As Exception
                transaction.Dispose()
                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
                Return New ActionResult(Of MaintenanceFailureRequest) With {.StateResult = False, .StatusCode = eStatusResult.EXCEPTION, .Message = Utils.GetInnerExceptionMessageToString(ex)}
            End Try
        End Using
    End Function

    Public Sub SendMaintenanceFailureRequestNotification(listMaintenanceFailureRequest As List(Of MaintenanceFailureRequest)) Implements IMaintenanceFailureRequestAdminService.SendMaintenanceFailureRequestNotification
        For Each failure In listMaintenanceFailureRequest
            failure = _MaintenanceRespository.GetMaintenanceFailureRequestById(failure.Id)
            For Each detail In failure.MaintenanceFailureRequestDetail
                For Each notification In detail.MaintenanceFailureRequestDetailNotification.Where(Function(n) n.Status = False)
                    Dim message As New MessageNotification With
                    {
                        .From = Utils.GetAppSettingValueByKey("FromEmailNotification"),
                        .To = notification.Email,
                        .Subject = notification.GetSubject(),
                        .Message = notification.GetBody()
                    }

                    Try
                        Using manager As New MessageManager(Utils.GetAppSettingValueByKey("FxGetUrlNotification"), Utils.GetAppSettingValueByKey("FxEmailNotification"))
                            Dim response = Task.Run(Function() manager.Send(message)).Result
                            If Not response.StateResult Then
                                Throw New Exception(If(String.IsNullOrEmpty(response.Message), "Error al enviar el correo electrónico", response.Message))
                            End If

                            notification.Status = True
                            notification.ShippingDate = DateTime.Now

                            Me._maintenanceFailureRequestDetailNotificationRepository.SaveEntity(notification)
                            Me._maintenanceFailureRequestDetailNotificationRepository.UnitWork.Commit()
                        End Using
                    Catch ex As Exception
                        Me._maintenanceLogRepository.SaveEntity(New Log With
                        {
                            .EntityId = detail.Id,
                            .EntityCode = failure.Code,
                            .EntityName = detail.GetType().Name,
                            .CreationDate = DateTime.Now,
                            .Status = False,
                            .Observation = Utils.GetInnerExceptionMessageToString(ex)
                        })
                        Me._maintenanceLogRepository.UnitWork.Commit()
                    End Try
                Next
            Next
        Next
    End Sub

#End Region

#Region "IDisposable Support"
    Private disposedValue As Boolean ' Para detectar llamadas redundantes

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not disposedValue Then
            If disposing Then

            End If
            _MaintenanceRespository = Nothing
            _secuenseDRepository = Nothing
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
