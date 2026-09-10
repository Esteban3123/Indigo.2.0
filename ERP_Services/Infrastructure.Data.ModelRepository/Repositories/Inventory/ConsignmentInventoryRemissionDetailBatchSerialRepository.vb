'***********************************************************************
' Assembly         : Infrastructure.Data.InventoryRepository
' Author           : Miguel Angel Fonseca
' Created          : 2017-12-12
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports System.Data.Entity.Core.Objects
Imports Domain.Entities
Imports Infrastructure.Data.Base

Public Class ConsignmentInventoryRemissionDetailBatchSerialRepository
    Inherits GenericRepository(Of ConsignmentInventoryRemissionDetailBatchSerial)
    Implements IConsignmentInventoryRemissionDetailBatchSerialRepository

    ''' <summary>
    ''' Contexto
    ''' </summary>
    ''' <remarks></remarks>
    Private _context As IGlobalModelUnitOfWork

    ''' <summary>
    ''' Inicia el contexto de payments
    ''' </summary>
    ''' <param name="context">Contexto</param>
    ''' <remarks></remarks>
    Public Sub New(ByVal context As IGlobalModelUnitOfWork)
        MyBase.New(context)
        _context = context
    End Sub

    Public Function GetConsignmentInventoryRemissionToReposition(warehouseId As Integer, productId As Integer, batchSerialId As Integer?) As List(Of ConsignmentInventoryRemissionDetailBatchSerial) Implements IConsignmentInventoryRemissionDetailBatchSerialRepository.GetConsignmentInventoryRemissionToReposition
        Dim query = (From bs In _context.ConsignmentInventoryRemissionDetailBatchSerial
                     Join cd In _context.ConsignmentInventoryRemissionDetail On bs.ConsignmentInventoryRemissionDetailId Equals cd.Id
                     Join c In _context.ConsignmentInventoryRemission On cd.ConsignmentInventoryRemissionId Equals c.Id
                     Where c.WarehouseId = warehouseId AndAlso cd.ProductId = productId AndAlso bs.BatchSerialId = batchSerialId AndAlso (bs.UsedQuantity - bs.ReplacementQuantity) > 0
                     Select bs).ToList() '.Sum(Function(m) m.UsedQuantity - m.ReplacementQuantity)

        Return query
    End Function

    ''' <summary>
    ''' Obtiene los detalles de un alamce de IC para decrementar el limite máximo
    ''' </summary>
    ''' <param name="warehouseId"></param>
    ''' <param name="productId"></param>
    ''' <returns></returns>
    Public Function GetCosignmentInventoryRemissionDetailBatchSerialToDecreaseMaximum(warehouseId As Integer, productId As Integer) As List(Of ConsignmentInventoryRemissionDetailBatchSerial) Implements IConsignmentInventoryRemissionDetailBatchSerialRepository.GetCosignmentInventoryRemissionDetailBatchSerialToDecreaseMaximum
        Dim query = (From bs In _context.ConsignmentInventoryRemissionDetailBatchSerial
                     Join cd In _context.ConsignmentInventoryRemissionDetail On bs.ConsignmentInventoryRemissionDetailId Equals cd.Id
                     Join c In _context.ConsignmentInventoryRemission On cd.ConsignmentInventoryRemissionId Equals c.Id
                     Where c.WarehouseId = warehouseId AndAlso cd.ProductId = productId AndAlso {1, 2}.Contains(c.MovementType)
                     Select bs)?.ToList()

        Return query
    End Function

    ''' <summary>
    ''' Obtenemos los batchserial de los cuales se puede usar cantidades
    ''' </summary>
    ''' <param name="wareHouseId"></param>
    ''' <param name="productId"></param>
    ''' <returns></returns>
    Public Function GetCosignmentInventoryRemissionDetailBatchSerialByWarehouseAndProduct(wareHouseId As Integer, productId As Integer, batchSerialId As Integer?) As List(Of ConsignmentInventoryRemissionDetailBatchSerial) Implements IConsignmentInventoryRemissionDetailBatchSerialRepository.GetCosignmentInventoryRemissionDetailBatchSerialByWarehouseAndProduct
        Dim query = (From bs In _context.ConsignmentInventoryRemissionDetailBatchSerial
                     Join cd In _context.ConsignmentInventoryRemissionDetail On bs.ConsignmentInventoryRemissionDetailId Equals cd.Id
                     Join c In _context.ConsignmentInventoryRemission On cd.ConsignmentInventoryRemissionId Equals c.Id
                     Where c.WarehouseId = wareHouseId AndAlso cd.ProductId = productId AndAlso bs.BatchSerialId = batchSerialId AndAlso bs.OutstandingQuantity > 0
                     Select bs).ToList()

        Return query
    End Function

    ''' <summary>
    ''' obtiene el detalle del detalle de la remision por id
    ''' </summary>
    ''' <param name="Id"></param>
    ''' <returns></returns>
    Public Function GetConsignmentInventoryRemissionDetailBatchSerialById(Id As Integer) As ConsignmentInventoryRemissionDetailBatchSerial Implements IConsignmentInventoryRemissionDetailBatchSerialRepository.GetConsignmentInventoryRemissionDetailBatchSerialById
        Return (From redb In _context.ConsignmentInventoryRemissionDetailBatchSerial Where redb.Id = Id Select redb).FirstOrDefault()
    End Function

    ''' <summary>
    ''' lista los detalles del detalle de la remision de inventario en consignacion 
    ''' </summary>
    ''' <param name="ConsignmentInventoryRemissionId"></param>
    ''' <returns></returns>
    Public Function ListConsignmentInventoryRemissionDetailBatchSerialByConsignmentInventoryRemissionId(ConsignmentInventoryRemissionId As Integer) As List(Of ConsignmentInventoryRemissionDetailBatchSerial) Implements IConsignmentInventoryRemissionDetailBatchSerialRepository.ListConsignmentInventoryRemissionDetailBatchSerialByConsignmentInventoryRemissionId
        Dim res = (From redbs In _context.ConsignmentInventoryRemissionDetailBatchSerial
                   Join red In _context.ConsignmentInventoryRemissionDetail On redbs.ConsignmentInventoryRemissionDetailId Equals red.Id
                   Join re In _context.ConsignmentInventoryRemission On red.ConsignmentInventoryRemissionId Equals re.Id
                   Where re.Id = ConsignmentInventoryRemissionId Select redbs).ToList()

        For Each item In res
            Dim remissionDetail = (From rd In _context.ConsignmentInventoryRemissionDetail.AsNoTracking() Where rd.Id = item.ConsignmentInventoryRemissionDetailId Select rd).FirstOrDefault()
            Dim product = (From p In _context.InventoryProduct.AsNoTracking() Where p.Id = remissionDetail.ProductId Select p).FirstOrDefault()
            item.CodeNameProduct = product.Code + " - " + product.Name
            item.ProductId = product.Id
            item.QuantityDeliver = item.OutstandingQuantity
            If item.BatchSerialId IsNot Nothing Then
                item.CodeBatchSerial = (From bs In _context.BatchSerial.AsNoTracking() Where bs.Id = item.BatchSerialId Select bs.BatchCode).FirstOrDefault()
            End If
        Next
        Return res
    End Function

    ''' <summary>
    ''' lista los detalles del detalle de la remision de inventario en consignacion con cantidades pendientes
    ''' </summary>
    ''' <param name="ConsignmentInventoryRemissionId"></param>
    ''' <returns></returns>
    Public Function ListConsignmentInventoryRemissionDetailBatchSerialByConsignmentInventoryRemissionIdWithOutstandingQuantity(ConsignmentInventoryRemissionId As Integer) As List(Of ConsignmentInventoryRemissionDetailBatchSerial) Implements IConsignmentInventoryRemissionDetailBatchSerialRepository.ListConsignmentInventoryRemissionDetailBatchSerialByConsignmentInventoryRemissionIdWithOutstandingQuantity
        Dim res = (From redbs In _context.ConsignmentInventoryRemissionDetailBatchSerial
                   Join red In _context.ConsignmentInventoryRemissionDetail On redbs.ConsignmentInventoryRemissionDetailId Equals red.Id
                   Join re In _context.ConsignmentInventoryRemission On red.ConsignmentInventoryRemissionId Equals re.Id
                   Where re.Id = ConsignmentInventoryRemissionId And redbs.OutstandingQuantity > 0 Select redbs).ToList()

        For Each item In res
            Dim remissionDetail = (From rd In _context.ConsignmentInventoryRemissionDetail.AsNoTracking() Where rd.Id = item.ConsignmentInventoryRemissionDetailId Select rd).FirstOrDefault()
            Dim product = (From p In _context.InventoryProduct.AsNoTracking() Where p.Id = remissionDetail.ProductId Select p).FirstOrDefault()
            item.CodeNameProduct = product.Code + " - " + product.Name
            item.ProductId = product.Id
            item.QuantityDeliver = item.OutstandingQuantity
            If item.BatchSerialId IsNot Nothing Then
                item.CodeBatchSerial = (From bs In _context.BatchSerial.AsNoTracking() Where bs.Id = item.BatchSerialId Select bs.BatchCode).FirstOrDefault()
            End If
        Next
        Return res
    End Function

    ''' <summary>
    ''' Lista los productos de la remision por lote
    ''' </summary>
    ''' <param name="idSupplier"></param>
    ''' <param name="idSupplierDistributionLine"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListConsignmentInventoryRemissionDetailBatchSerialBySupplierAndSupplierDistributionLine(idSupplier As Integer, idSupplierDistributionLine As Integer) As List(Of ConsignmentInventoryRemissionDetailBatchSerial) Implements IConsignmentInventoryRemissionDetailBatchSerialRepository.ListConsignmentInventoryRemissionDetailBatchSerialBySupplierAndSupplierDistributionLine
        Dim status As Integer = 2
        Dim res = (From redb In _context.ConsignmentInventoryRemissionDetailBatchSerial
                   Join red In _context.ConsignmentInventoryRemissionDetail On redb.ConsignmentInventoryRemissionDetailId Equals red.Id
                   Join re In _context.ConsignmentInventoryRemission On red.ConsignmentInventoryRemissionId Equals re.Id
                   Where re.SupplierId = idSupplier And re.SupplierDistributionLineId = idSupplierDistributionLine And re.Status = status And redb.OutstandingQuantity > 0 Select redb).ToList()

        If res.Count > 0 Then
            For Each item In res
                Dim remissionEntranceDetail = (From red In _context.ConsignmentInventoryRemissionDetail.AsNoTracking() Where red.Id = item.ConsignmentInventoryRemissionDetailId Select red).FirstOrDefault()
                Dim remissionEntrance = (From re In _context.ConsignmentInventoryRemission.AsNoTracking() Where re.Id = remissionEntranceDetail.ConsignmentInventoryRemissionId Select re).FirstOrDefault()
                Dim product = (From p In _context.InventoryProduct.AsNoTracking() Where p.Id = remissionEntranceDetail.ProductId Select p).FirstOrDefault()
                Dim batch = IIf(item.BatchSerialId IsNot Nothing, (From b In _context.BatchSerial.AsNoTracking() Where b.Id = item.BatchSerialId Select b).FirstOrDefault(), Nothing)

                item.Code = remissionEntrance.Code
                item.CodeNameProduct = product.Code + " - " + product.Name
                item.ProductId = product.Id
                If batch IsNot Nothing Then
                    item.CodeBatchSerial = batch.BatchCode
                End If
            Next
        End If
        Return res
    End Function

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
    Public Function UpdateTheQuantityProductUsedInConsignmentInventoryRemission(xml As String, EntityId As Integer, EntityCode As String, EntityName As String, User As String) As String Implements IConsignmentInventoryRemissionDetailBatchSerialRepository.UpdateTheQuantityProductUsedInConsignmentInventoryRemission
        Dim Output As ObjectParameter = New ObjectParameter("MessageReturn", GetType(String))
        _context.SP_UpdateTheQuantityProductUsedInConsignmentInventoryRemission(xml, EntityId, EntityCode, EntityName, User, Output).FirstOrDefault()
        Return Output.Value.ToString()
    End Function

    ''' <summary>
    ''' lista los detalles de la remision de consignacion sin legalizar
    ''' </summary>
    ''' <param name="code"></param>
    ''' <param name="warehouseId"></param>
    ''' <returns></returns>
    Public Function ListConsignmentInventoryRemissionWithoutLegalize(code As String, warehouseId As Integer) As List(Of ViewConsignmentInventoryRemissionWithoutLegalize) Implements IConsignmentInventoryRemissionDetailBatchSerialRepository.ListConsignmentInventoryRemissionWithoutLegalize
        Dim quantity As Integer = 0
        Dim res = (From consigrwl In _context.ViewConsignmentInventoryRemissionWithoutLegalize
                   Where consigrwl.WarehouseId = warehouseId AndAlso consigrwl.OutstandingQuantity > quantity AndAlso consigrwl.Code.Equals(code) Select consigrwl).ToList()

        Return res
    End Function

End Class