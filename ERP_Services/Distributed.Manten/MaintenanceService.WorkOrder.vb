Imports Application.Maintenance
Imports Domain.Base.Entities
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Microsoft.Practices.Unity

Partial Public Class MaintanceService
    Implements IWorkOrderService

    Public Function GetWorkOrderById(id As Integer) As WorkOrder Implements IWorkOrderService.GetWorkOrderById
        Using service As IWorkOrderAdminService = Container.Current.Resolve(Of IWorkOrderAdminService)()
            Return service.GetWorkOrderById(id)
        End Using
    End Function

    Public Function GetWorkOrderByCode(code As String) As WorkOrder Implements IWorkOrderService.GetWorkOrderByCode
        Using service As IWorkOrderAdminService = Container.Current.Resolve(Of IWorkOrderAdminService)()
            Return service.GetWorkOrderByCode(code)
        End Using
    End Function

    Public Function SaveWorkOrder(listWorkOrder As List(Of WorkOrder), audit As AuditMessage) As ActionResult(Of List(Of WorkOrder)) Implements IWorkOrderService.SaveWorkOrder
        Using service As IWorkOrderAdminService = Container.Current.Resolve(Of IWorkOrderAdminService)()
            Dim result = service.SaveWorkOrder(listWorkOrder, audit)
            If result.StateResult Then
                service.SendWorkOrderNotification(listWorkOrder)
            End If
            Return result
        End Using
    End Function

End Class
