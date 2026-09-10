'***********************************************************************
' Assembly         : DistributedService.MixingStation
' Author           : Unknown
' Created          : 13/07/2021
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"
Imports Application.MixingStation
Imports DistributedServices.MixingStation
Imports Domain.Base.Entities
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.CrossCutting.IOC
Imports Microsoft.Practices.Unity
#End Region

Partial Class MixingStationService
    Implements IMixingStationServiceRawMaterial

    ''' <summary>
    ''' Servicio distribuidos para listar productos y cantidad de materia prima.
    ''' </summary>
    ''' <param name="IdCampaing"></param>
    ''' <param name="StockId"></param>
    ''' <param name="warehouseId"></param>
    ''' <param name="Session"></param>
    ''' <returns></returns>
    Public Async Function ListProductRawMaterial(IdCampaing As Integer, StockId As Integer, warehouseId As Integer, Type As Integer, Session As SessionValues) As Task(Of List(Of ProductMixingStation)) Implements IMixingStationServiceRawMaterial.ListProductRawMaterial
        Using service As IRawMaterialAdminService = Container.Current.Resolve(Of IRawMaterialAdminService)()
            Return Await service.ListProductRawMaterialAsync(IdCampaing, StockId, warehouseId, Type, Session)
        End Using
    End Function
    ''' <summary>
    ''' Guardar los componentes de la canasta que se asocian a la materia prima
    ''' </summary>
    ''' <param name="CampaignDetailId"></param>
    ''' <param name="campaignId"></param>
    ''' <param name="ProductionBasketsId"></param>
    ''' <param name="Session"></param>
    ''' <returns></returns>
    Public Async Function SaveBasketsMateriaRawAsync(CampaignDetailId As Integer, campaignId As Integer, ProductionBasketsId As Integer, audit As AuditMessage, Session As SessionValues) As Task(Of ActionResult) Implements IMixingStationServiceRawMaterial.SaveBasketsMateriaRawAsync
        Using service As IRawMaterialAdminService = Container.Current.Resolve(Of IRawMaterialAdminService)()
            Return Await service.SaveBasketsMateriaRawAsync(CampaignDetailId, campaignId, ProductionBasketsId, audit, Session)
        End Using
    End Function

    ''' <summary>
    ''' Calculo del Picking asociado a la Materia Prima 
    ''' </summary>
    ''' <param name="productsMixing"></param>
    ''' <param name="stockWarehouseId"></param>
    ''' <param name="warehouseId"></param>
    ''' <param name="maquilaId"></param>    
    ''' <param name="Session"></param>
    ''' <returns></returns>
    Async Function CalculatePickingAsync(ByVal productsMixing As List(Of ProductMixingStation), campaignDetailId As Integer, stockWarehouseId As Integer, warehouseId As Integer?, maquilaId As Integer?, RemnantWarehouseId As Integer, audit As AuditMessage, Session As SessionValues) As Task(Of ActionResult(Of CampaignDetailItems)) Implements IMixingStationServiceRawMaterial.CalculatePickingAsync
        Using service As IRawMaterialAdminService = Container.Current.Resolve(Of IRawMaterialAdminService)()
            Return Await service.CalculatePickingAsync(productsMixing, campaignDetailId, stockWarehouseId, warehouseId, maquilaId, RemnantWarehouseId, audit, Session)
        End Using
    End Function

    ''' <summary>
    ''' Listar Medicamentos en Estado Picking 
    ''' </summary>
    ''' <param name="campaignDetailId">Id de la CampaignDetail</param>
    ''' <param name="session"></param>
    ''' <returns></returns>
    Function ListRawMaterialItems(campaignDetailId As Integer, session As SessionValues) As List(Of CampaignDetailItems) Implements IMixingStationServiceRawMaterial.ListRawMaterialItems
        Using service As IRawMaterialAdminService = Container.Current.Resolve(Of IRawMaterialAdminService)()
            Return service.ListRawMaterialItems(campaignDetailId, session)
        End Using
    End Function

    ''' <summary>
    ''' Listar Medicamentos en Estado Validation 
    ''' </summary>
    ''' <param name="campaignDetailId">Id de la CampaignDetail</param>
    ''' <param name="session"></param>
    ''' <returns></returns>
    Function ListRawMaterialItemsValidation(campaignDetailId As Integer, session As SessionValues) As List(Of CampaignDetailItems) Implements IMixingStationServiceRawMaterial.ListRawMaterialItemsValidation
        Using service As IRawMaterialAdminService = Container.Current.Resolve(Of IRawMaterialAdminService)()
            Return service.ListRawMaterialItemsValidation(campaignDetailId, session)
        End Using
    End Function

    ''' <summary>
    ''' Guardar la Solicitud orden de Traslado 
    ''' </summary>
    ''' <param name="transferOrderlist">Entidad Principal TransferOrder </param>
    ''' <param name="CMConfiguration">Id de la Central de Mezcla</param>
    ''' <param name="campaignDetailId">Id de la CampaignDetail</param>
    ''' <param name="CampaignNumber">Numero de la Campaña</param>
    ''' <param name="audit"></param>
    ''' <param name="session"></param>
    ''' <returns></returns>
    Async Function Ordertransfer(transferOrderlist As TransferOrder, CMConfiguration As Integer, campaignDetailId As Integer, CampaignNumber As Integer, audit As AuditMessage, session As SessionValues) As Task(Of ActionResult(Of TransferOrder)) Implements IMixingStationServiceRawMaterial.OrdertransferAsync
        Using service As IRawMaterialAdminService = Container.Current.Resolve(Of IRawMaterialAdminService)()
            Return Await service.OrdertransferAsync(transferOrderlist, CMConfiguration, campaignDetailId, CampaignNumber, audit, session)
        End Using
    End Function

    ''' <summary>
    ''' Guardar Solicitud traslado de inventario
    ''' </summary>
    ''' <param name="inventoryRequest">Entidad Principal InventoryRequest</param>
    ''' <param name="audit"></param>
    ''' <param name="session"></param>
    ''' <returns></returns>
    Function SaveInventoryRequest(inventoryRequest As InventoryRequest, audit As AuditMessage, session As SessionValues) As ActionResult Implements IMixingStationServiceRawMaterial.SaveInventoryRequest
        Using service As IRawMaterialAdminService = Container.Current.Resolve(Of IRawMaterialAdminService)()
            Return service.SaveInventoryRequest(inventoryRequest, audit, session)
        End Using
    End Function

    ''' <summary>
    ''' Guardo la materia prima adicional a la campaña
    ''' </summary>
    ''' <param name="productCampaignDetail"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    Async Function SaveProductCampaignDetailAsync(productCampaignDetail As CampaignDetailItems, audit As AuditMessage) As Task(Of ActionResult) Implements IMixingStationServiceRawMaterial.SaveProductCampaignDetailAsync
        Using service As IRawMaterialAdminService = Container.Current.Resolve(Of IRawMaterialAdminService)()
            Return Await service.SaveProductCampaignDetailAsync(productCampaignDetail, audit)
        End Using
    End Function

    ''' <summary>
    ''' Se encarga de eliminar la materia prima adicional de la campaña
    ''' </summary>
    ''' <param name="productCampaignDetail"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    Async Function DeleteProductCampaignDetailAsync(productCampaignDetail As CampaignDetailItems, audit As AuditMessage) As Task(Of ActionResult) Implements IMixingStationServiceRawMaterial.DeleteProductCampaignDetailAsync
        Using service As IRawMaterialAdminService = Container.Current.Resolve(Of IRawMaterialAdminService)()
            Return Await service.DeleteProductCampaignDetailAsync(productCampaignDetail, audit)
        End Using
    End Function

    ''' <summary>
    ''' Actualiza estado de la campaña
    ''' </summary>
    ''' <param name="campaignDetailId">Id de la CampaignDetail</param>
    ''' <param name="statuscampaign"></param>
    ''' <param name="session"></param>
    ''' <returns></returns>
    Async Function UpdateStatusCampaignDetailAsync(campaignDetailId As Integer, statuscampaign As Byte, session As SessionValues) As Task(Of ActionResult(Of CampaignDetail)) Implements IMixingStationServiceRawMaterial.UpdateStatusCampaignDetailAsync
        Using service As IRawMaterialAdminService = Container.Current.Resolve(Of IRawMaterialAdminService)()
            Return Await service.UpdateStatusCampaignDetailAsync(campaignDetailId, statuscampaign, session)
        End Using
    End Function

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
    Public Function ListPhysicalInventoryByATCSupplyProduct(campaignDetailId As Integer, atcid As Integer?, supplyId As Integer?, productId As Integer?, warehouseId As Integer, stockId As Integer, maquilaId As Integer?, session As SessionValues) As List(Of PhysicalInventory) Implements IMixingStationServiceRawMaterial.ListPhysicalInventoryByATCSupplyProduct
        Using service As IRawMaterialAdminService = Container.Current.Resolve(Of IRawMaterialAdminService)()
            Return service.ListPhysicalInventoryByATCSupplyProduct(campaignDetailId, atcid, supplyId, productId, warehouseId, stockId, maquilaId, session)
        End Using
    End Function

    ''' <summary>
    ''' Entrega Manual
    ''' </summary>
    ''' <param name="campaignDetailItems"></param>
    ''' <param name="session"></param>
    ''' <returns></returns>
    Public Async Function ManualDeliveryAsync(campaignDetailItems As List(Of CampaignDetailItems), session As SessionValues) As Task(Of ActionResult) Implements IMixingStationServiceRawMaterial.ManualDeliveryAsync
        Using service As IRawMaterialAdminService = Container.Current.Resolve(Of IRawMaterialAdminService)()
            Return Await service.ManualDeliveryAsync(campaignDetailItems, session)
        End Using
    End Function

    ''' <summary>
    ''' Función para listar Productos a partir de un 
    ''' </summary>
    ''' <param name="warehouseId"></param>
    ''' <param name="StockId"></param>
    ''' <param name="session"></param>
    ''' <returns></returns>
    Private Function ListPhysicalInventoryByCode(ATCNumber As String, warehouseId As Integer, StockId As Integer, MaquilaId As Integer?, session As SessionValues) As List(Of PhysicalInventory) Implements IMixingStationServiceRawMaterial.ListPhysicalInventoryByCode
        Using service As IRawMaterialAdminService = Container.Current.Resolve(Of IRawMaterialAdminService)()
            Return service.ListPhysicalInventoryByCode(ATCNumber, warehouseId, StockId, MaquilaId, session)
        End Using
    End Function

End Class