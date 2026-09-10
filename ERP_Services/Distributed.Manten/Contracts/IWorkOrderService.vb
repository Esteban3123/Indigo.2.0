Imports System.ServiceModel
Imports Domain.Base.Entities
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base

<ServiceContract()>
Public Interface IWorkOrderService

    <OperationContract()>
    Function GetWorkOrderById(id As Integer) As WorkOrder

    <OperationContract()>
    Function GetWorkOrderByCode(code As String) As WorkOrder

    <OperationContract()>
    Function SaveWorkOrder(listWorkOrder As List(Of WorkOrder), audit As AuditMessage) As ActionResult(Of List(Of WorkOrder))

End Interface
