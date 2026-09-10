'***********************************************************************
' Assembly         : Domain.Inventory
' Author           : Miguel Angel Fonseca
' Created          : 2017-12-12
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Base

Public Interface IConsignmentInventoryRemissionDetailBatchSerialRepository
    Inherits IRepository(Of ConsignmentInventoryRemissionDetailBatchSerial)

    ''' <summary>
    ''' obtiene una detalla del detalle de la remision por id
    ''' </summary>
    ''' <param name="Id"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetConsignmentInventoryRemissionDetailBatchSerialById(Id As Integer) As ConsignmentInventoryRemissionDetailBatchSerial

    ''' <summary>
    ''' lista los detalles del detalle de la remision de inventario en consignacion
    ''' </summary>
    ''' <param name="ConsignmentInventoryRemissionId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function ListConsignmentInventoryRemissionDetailBatchSerialByConsignmentInventoryRemissionId(ConsignmentInventoryRemissionId As Integer) As List(Of ConsignmentInventoryRemissionDetailBatchSerial)

    ''' <summary>
    ''' lista los detalles del detalle de la remision de inventario en consignacion con cantidades pendientes
    ''' </summary>
    ''' <param name="ConsignmentInventoryRemissionId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function ListConsignmentInventoryRemissionDetailBatchSerialByConsignmentInventoryRemissionIdWithOutstandingQuantity(ConsignmentInventoryRemissionId As Integer) As List(Of ConsignmentInventoryRemissionDetailBatchSerial)

    ''' <summary>
    ''' Lista los productos de la remision por lote
    ''' </summary>
    ''' <param name="idSupplier"></param>
    ''' <param name="idSupplierDistributionLine"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function ListConsignmentInventoryRemissionDetailBatchSerialBySupplierAndSupplierDistributionLine(idSupplier As Integer, idSupplierDistributionLine As Integer) As List(Of ConsignmentInventoryRemissionDetailBatchSerial)

    ''' <summary>
    ''' Ejecuta el procedimiento almacenado que se encarga de actualizar la cantidad usada de determinado producto en la remisión de inventario en consignación
    ''' </summary>
    ''' <param name="xml"></param>
    ''' <param name="EntityId"></param>
    ''' <param name="EntityCode"></param>
    ''' <param name="EntityName"></param>
    ''' <param name="User"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function UpdateTheQuantityProductUsedInConsignmentInventoryRemission(xml As String, EntityId As Integer, EntityCode As String, EntityName As String, User As String) As String

    Function GetCosignmentInventoryRemissionDetailBatchSerialByWarehouseAndProduct(wareHouseId As Integer, productId As Integer, batchSerialId As Integer?) As List(Of ConsignmentInventoryRemissionDetailBatchSerial)
    Function GetConsignmentInventoryRemissionToReposition(warehouseId As Integer, productId As Integer, batchSerialId As Integer?) As List(Of ConsignmentInventoryRemissionDetailBatchSerial)

    Function GetCosignmentInventoryRemissionDetailBatchSerialToDecreaseMaximum(warehouseId As Integer, productId As Integer) As List(Of ConsignmentInventoryRemissionDetailBatchSerial)

    ''' <summary>
    ''' lista los detalles de la remision de consignacion sin legalizar
    ''' </summary>
    ''' <param name="code"></param>
    ''' <param name="warehouseId"></param>
    ''' <returns></returns>
    Function ListConsignmentInventoryRemissionWithoutLegalize(code As String, warehouseId As Integer) As List(Of ViewConsignmentInventoryRemissionWithoutLegalize)
End Interface