#Region "Imports"

Imports System.Text
Imports System.Transactions
Imports Domain.Base
Imports Domain.Base.Entities
Imports Domain.Entities
Imports Domain.Notification
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.CrossCutting.Exceptions
Imports Infrastructure.CrossCutting.Resources

#End Region

Public Class WorkOrderAdminService
    Implements IWorkOrderAdminService

#Region "Builder"

    Private _workOrderRepository As IWorkOrderRepository
    Private _maintenanceLogRepository As IMaintenanceLogRepository
    Private _workOrderNotificationRepository As IWorkOrderNotificationRepository

    Public Sub New(workOrderRepository As IWorkOrderRepository,
                   maintenanceLogRepository As IMaintenanceLogRepository,
                   workOrderNotificationRepository As IWorkOrderNotificationRepository)
        If (workOrderRepository Is Nothing) Then
            Throw New ArgumentNullException("Repositorio de WorkOrderRespository")
        End If

        Me._workOrderRepository = workOrderRepository
        Me._maintenanceLogRepository = maintenanceLogRepository
        Me._workOrderNotificationRepository = workOrderNotificationRepository
    End Sub

#End Region

#Region "Methods"

    Public Function GetWorkOrderById(id As Integer) As WorkOrder Implements IWorkOrderAdminService.GetWorkOrderById
        If String.IsNullOrEmpty(id) Then
            Throw New ArgumentNullException("id vacio")
        End If
        Try
            Return _workOrderRepository.GetWorkOrderById(id)
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return Nothing
        End Try
    End Function

    Public Function GetWorkOrderByCode(code As String) As WorkOrder Implements IWorkOrderAdminService.GetWorkOrderByCode
        If String.IsNullOrEmpty(code) Then
            Throw New ArgumentNullException("Codigo vacio")
        End If
        Try
            Return _workOrderRepository.GetWorkOrderByCode(code)
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return Nothing
        End Try
    End Function

    Public Function SaveWorkOrder(listWorkOrder As List(Of WorkOrder), audit As AuditMessage) As ActionResult(Of List(Of WorkOrder)) Implements IWorkOrderAdminService.SaveWorkOrder
        If listWorkOrder Is Nothing Then
            Throw New ArgumentNullException("listWorkOrder")
        End If

        Dim txSettings As New TransactionOptions()
        txSettings.Timeout = TransactionManager.MaximumTimeout
        txSettings.IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted
        Using scope As New TransactionScope(TransactionScopeOption.Required, txSettings)
            Try
                Dim listWorkOrderXml = ConvertListWorkOrderToXml(listWorkOrder)

                Dim result = Me._workOrderRepository.SP_SaveWorkOrder(listWorkOrderXml, audit.CodeUser)
                If result.CodeResult <> 0 Then
                    scope.Dispose()
                    Return New ActionResult(Of List(Of WorkOrder)) With {.StateResult = False, .Message = result.MessageResult}
                End If

                Dim workOrder = listWorkOrder.FirstOrDefault()
                workOrder.Id = result.Id
                workOrder.Consecutive = result.Code
                workOrder.MarkAsUnchanged()

                scope.Complete()
                Return New ActionResult(Of List(Of WorkOrder)) With {.StateResult = True, .ObjectEmbbeded = listWorkOrder, .Message = result.MessageResult}
            Catch ex As OptimisticConcurrencyException
                scope.Dispose()
                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
                Return New ActionResult(Of List(Of WorkOrder)) With {.StateResult = False, .Message = Infrastructure.CrossCutting.Resources.ResourceManager.GetString("ErrorConcurrence")}
            Catch ex As System.Data.Entity.Infrastructure.DbUpdateException
                scope.Dispose()
                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
                Return New ActionResult(Of List(Of WorkOrder)) With {.StateResult = False, .Message = Utils.GetInnerExceptionMessageToString(ex)}
            Catch ex As Exception
                scope.Dispose()
                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
                Return New ActionResult(Of List(Of WorkOrder)) With {.StateResult = False, .Message = Utils.GetInnerExceptionMessageToString(ex)}
            End Try
        End Using
    End Function

    Public Sub SendWorkOrderNotification(listWorkOrder As List(Of WorkOrder)) Implements IWorkOrderAdminService.SendWorkOrderNotification
        For Each workOrder In listWorkOrder
            workOrder = _workOrderRepository.GetWorkOrderById(workOrder.Id)
            For Each notification In workOrder.WorkOrderNotification.Where(Function(n) n.Status = False)
                Try
                    Dim message As New MessageNotification With
                    {
                        .From = Utils.GetAppSettingValueByKey("FromEmailNotification"),
                        .To = notification.Email,
                        .Subject = notification.GetSubject(),
                        .Message = notification.GetBody()
                    }

                    Using manager As New MessageManager(Utils.GetAppSettingValueByKey("FxGetUrlNotification"), Utils.GetAppSettingValueByKey("FxEmailNotification"))
                        Dim response = Task.Run(Function() manager.Send(message)).Result
                        If Not response.StateResult Then
                            Throw New Exception(If(String.IsNullOrEmpty(response.Message), "Error al enviar el correo electrónico", response.Message))
                        End If

                        notification.Status = True
                        notification.ShippingDate = DateTime.Now

                        Me._workOrderNotificationRepository.SaveEntity(notification)
                        Me._workOrderNotificationRepository.UnitWork.Commit()
                    End Using
                Catch ex As Exception
                    Me._maintenanceLogRepository.SaveEntity(New Log With
                    {
                        .EntityId = workOrder.Id,
                        .EntityCode = workOrder.Consecutive,
                        .EntityName = workOrder.GetType().Name,
                        .CreationDate = DateTime.Now,
                        .Status = False,
                        .Observation = Utils.GetInnerExceptionMessageToString(ex)
                    })
                    Me._maintenanceLogRepository.UnitWork.Commit()
                End Try
            Next
        Next
    End Sub

#End Region

#Region "Private Methods"

    Private Function ConvertListWorkOrderToXml(listWorkOrder As List(Of WorkOrder)) As String
        Dim rowId = 1
        Dim builder As New StringBuilder

        For Each workOrder In listWorkOrder
            workOrder.RowId = rowId
            builder.Append(workOrder.ToXML())

            rowId = rowId + 1
        Next

        Return builder.ToString()
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

            ' TODO: libere los recursos no administrados (objetos no administrados) y reemplace Finalize() a continuación.
            ' TODO: configure los campos grandes en nulos.
        End If
        disposedValue = True
    End Sub

    ' TODO: reemplace Finalize() solo si el anterior Dispose(disposing As Boolean) tiene código para liberar recursos no administrados.
    'Protected Overrides Sub Finalize()
    '    ' No cambie este código. Coloque el código de limpieza en el anterior Dispose(disposing As Boolean).
    '    Dispose(False)
    '    MyBase.Finalize()
    'End Sub

    ' Visual Basic agrega este código para implementar correctamente el patrón descartable.
    Public Sub Dispose() Implements IDisposable.Dispose
        ' No cambie este código. Coloque el código de limpieza en el anterior Dispose(disposing As Boolean).
        Dispose(True)
        ' TODO: quite la marca de comentario de la siguiente línea si Finalize() se ha reemplazado antes.
        ' GC.SuppressFinalize(Me)
    End Sub
#End Region

End Class
