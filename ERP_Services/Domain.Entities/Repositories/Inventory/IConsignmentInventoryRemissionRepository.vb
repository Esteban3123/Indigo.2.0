'***********************************************************************
' Assembly         : Domain.Inventory
' Author           : Miguel Angel Fonseca
' Created          : 2017-12-12
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Base

Public Interface IConsignmentInventoryRemissionRepository
    Inherits IRepository(Of ConsignmentInventoryRemission)
    Inherits IRepositoryRollbackStrategy

    ''' <summary>
    ''' obtiene una remision por id
    ''' </summary>
    ''' <param name="id"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetConsignmentInventoryRemissionById(id As Integer) As ConsignmentInventoryRemission

    ''' <summary>
    ''' obtiene una remision por codigo
    ''' </summary>
    ''' <param name="code"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetConsignmentInventoryRemissionByCode(code As String) As ConsignmentInventoryRemission

    ''' <summary>
    ''' lista todos los documentos para confirmarlos masivamente
    ''' </summary>
    ''' <param name="listDocuments"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function ListConsignmentInventoryRemissionMassiveConfirm(listDocuments As List(Of String)) As List(Of ConsignmentInventoryRemission)

    ''' <summary>
    ''' Genera el comprobante contable para remision de inventario en consignación
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function SP_GenerateJournalVoucherByConsignmentInventoryRemission(Id As Integer, CodeUser As String) As SP_GenerateJournalVoucherByConsignmentInventoryRemission_Result

    ''' <summary>
    ''' Obtiene las cantidades en el almacen de consignación
    ''' </summary>
    ''' <param name="warehouseId"></param>
    ''' <param name="productId"></param>
    ''' <returns></returns>
    Function GetConsignmentInventoryQuantities(warehouseId As Integer, productId As Integer) As ConsignmentMovementInventory
End Interface