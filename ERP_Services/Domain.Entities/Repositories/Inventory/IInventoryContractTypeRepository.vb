'***********************************************************************
' Assembly         : Domain.Inventory
' Author           : Henry Alejandro Vargas Polania
' Created          : 26-12-2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Base
Imports Domain.Entities

Public Interface IInventoryContractTypeRepository
    Inherits IRepository(Of InventoryContractType)

    ''' <summary>
    ''' Obtiene un InventoryContracType por codigo
    ''' </summary>
    ''' <param name="code"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetInventoryContractType(code As String) As InventoryContractType

    ''' <summary>
    ''' Obtiene un InventoryContracType por id
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetInventoryContractTypeById(id As Integer) As InventoryContractType

End Interface
