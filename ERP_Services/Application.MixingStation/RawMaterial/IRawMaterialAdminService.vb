'***********************************************************************
' Assembly         : Application.MixingStation
' Author           : 
' Created          : 13-07-2021
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Base.Entities
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base


Public Interface IRawMaterialAdminService
    Inherits IDisposable

    ''' <summary>
    ''' Listar medicamentos en el formulario de Materia Prima
    ''' </summary>
    ''' <param name="CampaigmId"></param>
    ''' <returns></returns>
    Function ListProductRawMaterialAsync(CampaigmId As Integer, StockId As Integer, warehouseId As Integer, Type As Integer, session As SessionValues) As Task(Of List(Of ProductMixingStation))
    ''' <summary>
    ''' Guardar los componentes de la canasta que seleccionan en el formulario de materia prima
    ''' </summary>
    ''' <returns></returns>
    Function SaveBasketsMateriaRawAsync(CampaignDetailId As Integer, campaignId As Integer, ProductionBasketsId As Integer, audit As AuditMessage, session As SessionValues) As Task(Of ActionResult)

    ''' <summary>
    ''' Funcion Calcular y Guardar Procesos Almacenes Materia Prima
    ''' </summary>
    ''' <returns></returns>
    Function CalculatePickingAsync(ByVal productsMixing As List(Of ProductMixingStation), campaignDetailId As Integer, stockWarehouseId As Integer, warehouseId As Integer?, maquilaId As Integer?, RemnantWarehouseId As Integer, audit As AuditMessage, session As SessionValues) As Task(Of ActionResult(Of CampaignDetailItems))

    ''' <summary>
    ''' Lista los Medicamentos en Picking 
    ''' </summary>
    ''' <param name="campaignDetailId">Id Campaña Detalle</param>
    ''' <param name="session">Session Values</param>
    ''' <returns></returns>
    Function ListRawMaterialItems(campaignDetailId As Integer, session As SessionValues) As List(Of CampaignDetailItems)

    ''' <summary>
    ''' Lista los Medicamentos en Validation 
    ''' </summary>
    ''' <param name="campaignDetailId">Id Campaña Detalle</param>
    ''' <param name="session">Session Values</param>
    ''' <returns></returns>
    Function ListRawMaterialItemsValidation(campaignDetailId As Integer, session As SessionValues) As List(Of CampaignDetailItems)

    ''' <summary>
    ''' Función que Envia la Orden de Traslado 
    ''' </summary>
    ''' <param name="transferOrderlist"></param>
    ''' <param name="CMConfiguration"></param>
    ''' <param name="campaignDetailId"></param>
    ''' <param name="CampaignNumber"></param>
    ''' <param name="audit"></param>
    ''' <param name="session"></param>
    ''' <returns></returns>
    Function OrdertransferAsync(transferOrderlist As TransferOrder, CMConfiguration As Integer, campaignDetailId As Integer, CampaignNumber As Integer, audit As AuditMessage, session As SessionValues) As Task(Of ActionResult(Of TransferOrder))

    ''' <summary>
    ''' Crea y guarda la Solicitud Traslado de Inventario
    ''' </summary>
    ''' <param name="inventoryRequest"></param>
    ''' <param name="audit"></param>
    ''' <param name="session"></param>
    ''' <returns></returns>
    Function SaveInventoryRequestAsync(inventoryRequest As InventoryRequest, audit As AuditMessage, session As SessionValues) As Task(Of ActionResult)

    ''' <summary>
    ''' Actualiza estado de la campaña 
    ''' </summary>
    ''' <param name="campaignDetailId"></param>
    ''' <param name="statuscampaign"></param>
    ''' <param name="session"></param>
    ''' <returns></returns>
    Function UpdateStatusCampaignDetailAsync(campaignDetailId As Integer, statuscampaign As Byte, session As SessionValues) As Task(Of ActionResult(Of CampaignDetail))
    ''' <summary>
    ''' Listar Boton de C.U.M
    ''' </summary>
    ''' <returns></returns>
    Function ListPhysicalInventoryByCode(ATCNumber As String, StockId As Integer, warehouseId As Integer, MaquilaId As Integer?, session As SessionValues) As List(Of PhysicalInventory)

    ''' <summary>
    ''' Guardo la materia prima adicional a la campaña
    ''' </summary>
    ''' <param name="productCampaignDetail"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    Function SaveProductCampaignDetailAsync(productCampaignDetail As CampaignDetailItems, audit As AuditMessage) As Task(Of ActionResult)

    ''' <summary>
    ''' Se encarga de eliminar la materia prima adicional de la campaña
    ''' </summary>
    ''' <param name="productCampaignDetail"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
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
    Function ListPhysicalInventoryByATCSupplyProduct(campaignDetailId As Integer, atcid As Integer?, supplyId As Integer?, productId As Integer?, warehouseId As Integer, stockId As Integer, maquilaId As Integer?, session As SessionValues) As List(Of PhysicalInventory)

    ''' <summary>
    ''' Entrega manual
    ''' </summary>
    ''' <param name="campaignDetailItems"></param>
    ''' <param name="session"></param>
    ''' <returns></returns>
    Function ManualDeliveryAsync(campaignDetailItems As List(Of CampaignDetailItems), session As SessionValues) As Task(Of ActionResult)
End Interface
