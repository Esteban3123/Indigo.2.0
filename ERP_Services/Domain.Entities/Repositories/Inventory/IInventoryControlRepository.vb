'***********************************************************************
' Assembly         : Domain.Inventory
' Author           : Henry Alejandro Vargas Polania
' Created          : 08-04-2015
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Base
Imports Domain.Entities

Public Interface IInventoryControlRepository
    Inherits IRepository(Of InventoryControl)
    ''' <summary>
    ''' lista los detalles del control de inventario
    ''' </summary>
    ''' <param name="inventoryControlId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetInventoryControlDetailByInventoryControlId(inventoryControlId As Integer) As List(Of InventoryControlDetail)

    ''' <summary>
    ''' metodo para obtener el control de inventario sin agregados, solo con el original value
    ''' </summary>
    ''' <param name="code"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetInventoryControlByCodeNoAdded(code As String) As InventoryControl

    ''' <summary>
    ''' Obtiene un InventoryControl por codigo
    ''' </summary>
    ''' <param name="code"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetInventoryControl(code As String) As InventoryControl

    ''' <summary>
    ''' Obtiene un InventoryControl por id
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetInventoryControlById(id As Integer) As InventoryControl

    ''' <summary>
    ''' Obtiene un InventoryControlDetail por id
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetInventoryControlDetailByInventoryControlDetailBatchSerialId(InventoryControlDetailBatchSerialId As Integer) As InventoryControlDetail
    ''' <summary>
    ''' obtyiene el control de inventarios por id sin agregado solo con el original value
    ''' </summary>
    ''' <param name="id"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetInventoryControlByIdNoAdded(id As Integer) As InventoryControl

    ''' <summary>
    ''' Obtiene un InventoryControl por id
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetInventoryControlByIdTask(id As Integer, Optional status As Byte? = Nothing) As InventoryControl

    ''' <summary>
    ''' '
    ''' </summary>
    ''' <param name="inventoryAdjustmentId"></param>
    ''' <returns></returns>
    Function GetInventoryControlByInventoryAdjustmentId(inventoryAdjustmentId As Integer) As List(Of InventoryControlDetailBatchSerial)
End Interface
