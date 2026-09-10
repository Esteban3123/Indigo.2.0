Imports Domain.Base

Public Interface IWorkOrderRepository
    Inherits IRepository(Of WorkOrder)

    Function GetWorkOrderById(id As Integer) As WorkOrder

    Function GetWorkOrderByCode(code As String) As WorkOrder

    Function SP_SaveWorkOrder(listWorkOrderXml As String, userCode As String) As SP_SaveWorkOrder_Result

End Interface
