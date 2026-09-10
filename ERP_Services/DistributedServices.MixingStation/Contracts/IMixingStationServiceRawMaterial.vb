'***********************************************************************
' Assembly         : DistributedServices.MixinStation
' Author           : Unknown
' Created          : 13/07/2021
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports System.ServiceModel
Imports Domain.Base.Entities
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base

<ServiceContract()>
Public Interface IMixingStationServiceRawMaterial

    ''' <summary>
    ''' Permite la importación de medicamentos para producción
    ''' </summary>
    ''' <returns></returns>
    <OperationContract()>
    Function ListProductRawMaterial(CampaigmId As Integer, StockId As Integer, warehouseId As Integer, Type As Integer, Session As SessionValues) As Task(Of List(Of ProductMixingStation))

    ''' <summary>
    ''' Guardar los componentes de la canasta que se asocian a la materia prima
    ''' </summary>
    ''' <param name="CampaignDetailId"></param>
    ''' <param name="campaignId"></param>
    ''' <param name="ProductionBasketsId"></param>
    ''' <param name="Session"></param>
    ''' <returns></returns>
    <OperationContract()>
    Function SaveBasketsMateriaRawAsync(CampaignDetailId As Integer, campaignId As Integer, ProductionBasketsId As Integer, audit As AuditMessage, Session As SessionValues) As Task(Of ActionResult)


    ''' <summary>
    ''' Calculo del Picking asociado a la Materia Prima 
    ''' </summary>
    ''' <param name="productsMixing"></param>
    ''' <param name="stockWarehouseId"></param>
    ''' <param name="warehouseId"></param>
    ''' <param name="maquilaId"></param>    
    ''' <param name="Session"></param>
    ''' <returns></returns>
    <OperationContract()>
    Function CalculatePickingAsync(ByVal productsMixing As List(Of ProductMixingStation), campaignDetailId As Integer, stockWarehouseId As Integer, warehouseId As Integer?, maquilaId As Integer?, RemnantWarehouseId As Integer, audit As AuditMessage, Session As SessionValues) As Task(Of ActionResult(Of CampaignDetailItems))

    ''' <summary>
    ''' Listar la Informacion de los Productos en Estado Picking
    ''' </summary>
    ''' <param name="campaignDetailId"></param>
    ''' <param name="session"></param>
    ''' <returns></returns>
    <OperationContract()>
    Function ListRawMaterialItems(campaignDetailId As Integer, session As SessionValues) As List(Of CampaignDetailItems)


    ''' <summary>
    ''' Listar la Informacion de los Productos en Estado Validation
    ''' </summary>
    ''' <param name="campaignDetailId"></param>
    ''' <param name="session"></param>
    ''' <returns></returns>
    <OperationContract()>
    Function ListRawMaterialItemsValidation(campaignDetailId As Integer, session As SessionValues) As List(Of CampaignDetailItems)

    ''' <summary>
    ''' Guardar la Solicitud orden de Traslado 
    ''' </summary>
    ''' <param name="transferOrderlist"></param>
    ''' <param name="CMConfiguration"></param>
    ''' <param name="campaignDetailId"></param>
    ''' <param name="CampaignNumber"></param>
    ''' <param name="audit"></param>
    ''' <param name="session"></param>
    ''' <returns></returns>
    <OperationContract()>
    Function OrdertransferAsync(transferOrderlist As TransferOrder, CMConfiguration As Integer, campaignDetailId As Integer, CampaignNumber As Integer, audit As AuditMessage, session As SessionValues) As Task(Of ActionResult(Of TransferOrder))

    ''' <summary>
    '''  Guardar Solicitud traslado de inventario
    ''' </summary>
    ''' <param name="inventoryRequest"></param>
    ''' <param name="audit"></param>
    ''' <param name="session"></param>
    ''' <returns></returns>
    <OperationContract()>
    Function SaveInventoryRequest(inventoryRequest As InventoryRequest, audit As AuditMessage, session As SessionValues) As Task(Of ActionResult)

    ''' <summary>
    ''' GActualiza estado de la campaña
    ''' </summary>
    ''' <param name="campaignDetailId"></param>
    ''' <param name="statuscampaign"></param>
    ''' <param name="session"></param>
    ''' <returns></returns>
    <OperationContract()>
    Function UpdateStatusCampaignDetailAsync(campaignDetailId As Integer, statuscampaign As Byte, session As SessionValues) As Task(Of ActionResult(Of CampaignDetail))

    ''' <summary>
    ''' Función para listar Productos a partir de un codigo de medicamento
    ''' </summary>
    ''' <param name="warehouseId"></param>
    ''' <param name="StockId"></param>
    ''' <param name="session"></param>
    ''' <returns></returns>
    <OperationContract()>
    Function ListPhysicalInventoryByCode(ATCNumber As String, warehouseId As Integer, StockId As Integer, MaquilaId As Integer?, session As SessionValues) As List(Of PhysicalInventory)

    ''' <summary>
    ''' Guardo la materia prima adicional a la campaña
    ''' </summary>
    ''' <param name="productCampaignDetail"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    <OperationContract()>
    Function SaveProductCampaignDetailAsync(productCampaignDetail As CampaignDetailItems, audit As AuditMessage) As Task(Of ActionResult)

    ''' <summary>
    ''' Se encarga de eliminar la materia prima adicional de la campaña
    ''' </summary>
    ''' <param name="productCampaignDetail"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    <OperationContract()>
    Function DeleteProductCampaignDetailAsync(productCampaignDetail As CampaignDetailItems, audit As AuditMessage) As Task(Of ActionResult)

    ''' <summary>
    ''' ListPhysicalInventoryByATCSupplyProduct
    ''' </summary>
    ''' <param name="campaignDetailId"></param>
    ''' <param name="atcid"></param>
    ''' <param name="supplyId"></param>
    ''' <param name="productId"></param>
    ''' <param name="warehouseId"></param>
    ''' <param name="stockId"></param>
    ''' <param name="maquilaId"></param>
    ''' <param name="session"></param>
    ''' <returns></returns>
    <OperationContract()>
    Function ListPhysicalInventoryByATCSupplyProduct(campaignDetailId As Integer, atcid As Integer?, supplyId As Integer?, productId As Integer?, warehouseId As Integer, stockId As Integer, maquilaId As Integer?, session As SessionValues) As List(Of PhysicalInventory)

    ''' <summary>
    ''' Entrega manual
    ''' </summary>
    ''' <param name="campaignDetailItems"></param>
    ''' <param name="session"></param>
    ''' <returns></returns>
    <OperationContract>
    Function ManualDeliveryAsync(campaignDetailItems As List(Of CampaignDetailItems), session As SessionValues) As Task(Of ActionResult)
End Interface
