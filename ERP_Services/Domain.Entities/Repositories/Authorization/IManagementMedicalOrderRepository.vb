Imports Domain.Base

Public Interface IManagementMedicalOrderRepository
    Inherits IRepository(Of ManagementMedicalOrder)

    Function GetManagementMedicalOrderById(id As Integer) As ManagementMedicalOrder

    Function GetManagementMedicalOrderByEntity(ByVal entityName As String, ByVal entityId As Integer) As ManagementMedicalOrder

    Function SP_SaveManagementMedicalOrder(ByVal listManagementMedicalOrderXml As String, ByVal codeUser As String) As SP_SaveManagementMedicalOrder_Result

End Interface
