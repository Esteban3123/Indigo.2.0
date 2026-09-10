Imports Domain.Base.Entities
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base

Public Interface IWorkOrderAdminService
    Inherits IDisposable

    Function GetWorkOrderById(id As Integer) As WorkOrder

    Function GetWorkOrderByCode(code As String) As WorkOrder

    Function SaveWorkOrder(listWorkOrder As List(Of WorkOrder), audit As AuditMessage) As ActionResult(Of List(Of WorkOrder))

    Sub SendWorkOrderNotification(listWorkOrder As List(Of WorkOrder))

End Interface
