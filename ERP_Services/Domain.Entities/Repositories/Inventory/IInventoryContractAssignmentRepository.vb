Imports Domain.Base

Public Interface IInventoryContractAssignmentRepository
    Inherits IRepository(Of InventoryContractAssignment)

    ''' <summary>
    ''' Obtiene una cesion de contrato por codigo
    ''' </summary>
    ''' <param name="code"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetInventoryContractAssignment(code As String) As InventoryContractAssignment

    ''' <summary>
    ''' Obtieneuna cesion de contrato por id
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetInventoryContractAssignmentById(id As Integer) As InventoryContractAssignment

    ''' <summary>
    ''' Guarda una cesion de contrato
    ''' </summary>
    ''' <param name="inventoryContractAssignmentXML"></param>
    ''' <param name="userCode"></param>
    ''' <returns></returns>
    Function SP_SaveInventoryContractAssignment(inventoryContractAssignmentXML As String, userCode As String) As SP_SaveInventoryContractAssignment_Result

End Interface
