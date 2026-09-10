'***********************************************************************
' Assembly         : Infrastructure.Data.MixinStationRepository
' Author           : Duván Mejía Cortes
' Created          : 22/07/2021
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Infrastructure.Data.Base
Imports Domain.Entities
Imports Domain.Base
Imports System.Data.Entity

Public Class PickingRepository
    Inherits GenericRepository(Of CampaignDetailPicking)
    Implements IPickingRepository, Inject

    ''' <summary>
    ''' Contexto de Configuración de Central de Mezclas
    ''' </summary>
    Private _context As IGlobalModelUnitOfWork
    ''' <summary>
    ''' Inicia el contexto de Configuración de Central de Mezclas
    ''' </summary>
    ''' <param name="context">Contexto</param>
    ''' <remarks></remarks>
    Public Sub New(ByVal context As IGlobalModelUnitOfWork)
        MyBase.New(context)
        _context = context
    End Sub

    ''' <summary>
    ''' Listar los productos en el Almacén
    ''' </summary>
    ''' <param name="atcId"></param>
    ''' <param name="productId"></param>
    ''' <param name="supplyId"></param>
    ''' <param name="warehouseId"></param>
    ''' <returns></returns>
    Public Async Function GetProductsByAtcAndWarehouseAsync(atcId As Integer?, supplyId As Integer?, productId As Integer?, warehouseId As Integer?) As Task(Of List(Of PhysicalInventory)) Implements IPickingRepository.GetProductsByAtcAndWarehouseAsync
        If atcId IsNot Nothing Then
            Return Await (From a In _context.PhysicalInventory.Include("InventoryProduct.ProductType").Include("BatchSerial").OrderBy(Function(t) t.BatchSerial.ExpirationDate)
                          Where a.WarehouseId = warehouseId And a.InventoryProduct.ATCId = atcId And a.Quantity > 0 And a.InventoryProduct.ProductType.Class <> 5 And a.InventoryProduct.Status = True
                          Select a).ToListAsync()
        End If
        If supplyId IsNot Nothing Then
            Return Await (From a In _context.PhysicalInventory.Include("InventoryProduct.ProductType").Include("BatchSerial").OrderBy(Function(t) t.BatchSerial.ExpirationDate)
                          Where a.WarehouseId = warehouseId And a.InventoryProduct.SupplieId = supplyId And a.Quantity > 0 And a.InventoryProduct.ProductType.Class <> 5 And a.InventoryProduct.Status = True
                          Select a).ToListAsync()
        End If
        If productId IsNot Nothing Then
            Return Await (From a In _context.PhysicalInventory.Include("InventoryProduct.ProductType").Include("BatchSerial").OrderBy(Function(t) t.BatchSerial.ExpirationDate)
                          Where a.WarehouseId = warehouseId And a.InventoryProduct.Id = productId And a.Quantity > 0 And a.InventoryProduct.ProductType.Class <> 5 And a.InventoryProduct.Status = True
                          Select a).ToListAsync()
        End If
        Return Nothing
    End Function

    ''' <summary>
    ''' Lista la Materia prima en Estado: Picking 
    ''' </summary>
    ''' <param name="campaignDetailId"></param>
    ''' <returns></returns>
    Public Function GetCampaignPickingDetail(campaignDetailId As Integer) As List(Of CampaignDetailPicking) Implements IPickingRepository.GetCampaignPickingDetail
        Return (From a In _context.CampaignDetailPicking.Include("CampaignDetail").Include("InventoryProduct").Include("Warehouse").Include("BatchSerial")
                Where Not a.Warehouse.ControlStore AndAlso a.CampaignDetailId = campaignDetailId
                Select a).ToList()
    End Function

    ''' <summary>
    ''' Lista la Cantidad de Productos en el Almacen por Batchserial
    ''' </summary>
    ''' <param name="productId"></param>
    ''' <param name="batchSerialId"></param>
    ''' <param name="WarehouseId"></param>
    ''' <returns></returns>
    Public Function GetQuantityByWarehouseProductAndBatchSerial(productId As Integer, batchSerialId As Integer, WarehouseId As Integer) As PhysicalInventory Implements IPickingRepository.GetQuantityByWarehouseProductAndBatchSerial
        Return (From a In _context.PhysicalInventory.Include("InventoryProduct") Where a.ProductId = productId And a.BatchSerialId = batchSerialId And a.WarehouseId = WarehouseId Select a).FirstOrDefault()
    End Function

End Class