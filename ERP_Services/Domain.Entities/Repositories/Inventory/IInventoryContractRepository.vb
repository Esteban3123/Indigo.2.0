'***********************************************************************
' Assembly         : Domain.Inventory
' Author           : Henry Alejandro Vargas Polania
' Created          : 07-01-2015
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Base
Imports Domain.Entities

Public Interface IInventoryContractRepository
    Inherits IRepository(Of InventoryContract)

    ''' <summary>
    ''' Obtiene un InventoryContrac por codigo
    ''' </summary>
    ''' <param name="code"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetInventoryContract(code As String) As InventoryContract

    ''' <summary>
    ''' Obtiene un InventoryContrac por id
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetInventoryContractById(id As Integer) As InventoryContract

    ''' <summary>
    ''' Guarda el contrato de inventarios
    ''' </summary>
    ''' <param name="Xml"></param>
    ''' <param name="UserCode"></param>
    ''' <returns></returns>
    Function SP_SaveInventoryContract(Xml As String, UserCode As String) As SP_SaveInventoryContract_Result

End Interface
