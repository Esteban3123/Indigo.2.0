Imports Domain.Base

Public Interface IInventoryContractModificationRepository
    Inherits IRepository(Of InventoryContractModification)

    ''' <summary>
    ''' Obtiene un otro si por codigo
    ''' </summary>
    ''' <param name="code"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetInventoryContractModification(code As String) As InventoryContractModification

    ''' <summary>
    ''' Obtiene un otro si por id
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetInventoryContractModificationById(id As Integer) As InventoryContractModification

    ''' <summary>
    ''' Guarda un otro si
    ''' </summary>
    ''' <param name="inventoryContractModificationXML"></param>
    ''' <param name="userCode"></param>
    ''' <returns></returns>
    Function SP_SaveInventoryContractModification(inventoryContractModificationXML As String, userCode As String) As SP_SaveInventoryContractModification_Result

End Interface
