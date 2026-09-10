'***********************************************************************
' Assembly         : Presentacion.MixingStation.MVP
' Author           : Carlos Mario Arias Rubiano
' Created          : 17/03/2021
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************
#Region "Imports"
Imports Infrastructure.CrossCutting.Base
Imports Presentation.CloudAgent
Imports Domain.Entities
Imports Domain.Base.Entities
Imports DevExpress.Data.PLinq
Imports Infrastructure.Data.Xpo
Imports DevExpress.Xpo
Imports Infrastructure.Data.Xpo.MixingStationRepository
Imports Presentation.MixingStation.MVP.PCampaigns
Imports Infrastructure.Data.Xpo.InventoryRepository

#End Region

Public Class MCampaign
    Implements IDisposable

#Region "Fields"


    ''' <summary>
    ''' Variable que contiene la instancia de la clase singleton
    ''' </summary>
    Dim _sessionValues As SessionValues

    ''' <summary>
    ''' Tag del formulario
    ''' </summary>
    Private _tagForm As String

#End Region

#Region "Builder"

    ''' <summary>
    ''' Contructor
    ''' </summary>
    ''' <param name="tag">tag del form</param>
    ''' <remarks></remarks>
    Public Sub New(tag As String)
        Me._tagForm = tag
        _sessionValues = SessionValues.Instance
        Me._sessionValues.AuditMessageWcf.Functional = Me._tagForm
    End Sub

#End Region

#Region "Methods"

    ''' <summary>
    ''' lista los inventarios fisicos por el numero ATC del producto con información adicional
    ''' </summary>
    ''' <param name="ATCNumber"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function ListPhysicalInventoryByATCNumberWithAdditionalInformation(ATCNumber As String, warehouseId As Integer, StockId As Integer, MaquilaId As Integer?) As Task(Of List(Of PhysicalInventory))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoMixingStation.ListPhysicalInventoryByCodeAsync(ATCNumber, warehouseId, StockId, MaquilaId, _sessionValues)
    End Function

    Public Async Function CancellationadjustmentsNPT(MixingstationDetailId As Integer, Data As Object) As Task(Of ActionResult)
        Dim parameter As String = Utils.SerializeObjectToJson(Data)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoMixingStation.CancellationadjustmentsNPTAsync(MixingstationDetailId, parameter, _sessionValues.AuditMessageWcf)
    End Function

    Public Async Function ListPhysicalInventoryByATCSupplyProductAsync(CampaignDetailId As Integer, Atcid As Integer?, SupplyId As Integer?, ProductId As Integer?, warehouseId As Integer, StockId As Integer, MaquilaId As Integer?) As Task(Of List(Of PhysicalInventory))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoMixingStation.ListPhysicalInventoryByATCSupplyProductAsync(CampaignDetailId, Atcid, SupplyId, ProductId, warehouseId, StockId, MaquilaId, _sessionValues)
    End Function

    Public Async Function GetCampaignDetailPickingByCampaignDetailId(campaignDetailId As Integer) As Task(Of ActionResult(Of List(Of CampaignDetailPicking)))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoMixingStation.GetCampaignDetailPickingByCampaignDetailIdAsync(campaignDetailId)
    End Function

    Public Async Function GetCampaignDetailValidationByCampaignDetailId(campaignDetailId As Integer) As Task(Of ActionResult(Of List(Of CampaignDetailValidation)))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoMixingStation.GetCampaignDetailValidationByCampaignDetailIdAsync(campaignDetailId)
    End Function

    Public Async Function AnnulatePatients(args As Object) As Task(Of ActionResult(Of SP_ProcessMixingStation_Result))
        Dim parameter As String = Utils.SerializeObjectToJson(args)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoMixingStation.AnnulatePatientsAsync(parameter, Me._sessionValues.AuditMessageWcf)
    End Function

    Public Function GetAllCampaignDetailWitnessFilesAsync(campaignDetailId As Integer) As Task(Of ActionResult(Of List(Of CampaignDetailWitnessFile)))
        Return IndigoConecta.Instancia.CurrentCloud.IndigoMixingStation.GetCampaignDetailWitnessFileAsync(campaignDetailId)
    End Function

    Public Function SaveCampaignDetailWitnessFileAsync(files As List(Of CampaignDetailWitnessFile)) As Task(Of ActionResult)
        Return IndigoConecta.Instancia.CurrentCloud.IndigoMixingStation.SaveCampaignDetailWitnessFileAsync(files, _sessionValues.AuditMessageWcf)
    End Function

    Public Async Function GetCampaignById(ByVal id As Integer) As Task(Of ActionResult(Of Campaign))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoMixingStation.GetCampaignByIdAsync(id)
    End Function

    Public Async Function GetCampaignDetailById(ByVal id As Integer, Optional Validation As Boolean = False) As Task(Of ActionResult(Of CampaignDetail))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoMixingStation.GetCampaignDetailByIdAsync(id, Validation)
    End Function

    Public Async Function ProcessFinishedProductAsync(requestMixingStationDetailIds As List(Of Integer)) As Task(Of ActionResult(Of List(Of Tuple(Of String, Integer))))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoMixingStation.ProcessFinishedProductAsync(requestMixingStationDetailIds, Me._sessionValues.AuditMessageWcf)
    End Function

    Public Function GetCampaignDetailXpoById(ByVal id As Integer) As CampaignDetailXpo
        Return XpoServiceEx.Instance(_sessionValues.TransactionalContainer).MixingStationService.GetXPOObject(Of CampaignDetailXpo)($"Id={id}")
    End Function

    Public Async Function SaveCampaign(args As Object) As Task(Of ActionResult(Of SP_SaveCampaign_Result))
        Dim parameter As String = Utils.SerializeObjectToJson(args)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoMixingStation.SaveCampaignAsync(parameter, Me._sessionValues.AuditMessageWcf)
    End Function

    ''' <summary>
    ''' Guarda la campaña y los items RequestMixingStationDetail
    ''' </summary>
    ''' <param name="args"></param>
    ''' <param name="listMixingStationDetail"></param>
    ''' <returns></returns>
    Public Async Function SaveCampaignAndMixingStationDetailAsync(args As Object, listMixingStationDetail As List(Of RequestMixingStationDetail)) As Task(Of ActionResult(Of SP_SaveCampaign_Result))
        Dim parameter As String = Utils.SerializeObjectToJson(args)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoMixingStation.SaveCampaignAndMixingStationDetailAsync(parameter, listMixingStationDetail, Me._sessionValues.AuditMessageWcf)
    End Function

    Public Function SaveReleaseLine(releaseLine As ReleaseLine) As Task(Of ActionResult)
        Return IndigoConecta.Instancia.CurrentCloud.IndigoMixingStation.SaveReleaseLineAsync(releaseLine, Me._sessionValues.AuditMessageWcf)
    End Function

    Public Function GetLastReleaseLineUsedByWorkingAreaIdAsync(releaseLineId As Integer, workingAreaId As Integer) As Task(Of ReleaseLine)
        Return IndigoConecta.Instancia.CurrentCloud.IndigoMixingStation.GetLastReleaseLineUsedByWorkingAreaIdAsync(releaseLineId, workingAreaId)
    End Function

    Public Async Function DeleteCampaign(campaignDetailId As Integer) As Task(Of ActionResult)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoMixingStation.DeleteCampaignAsync(campaignDetailId, _sessionValues.TransactionalContainer, _sessionValues.AuditMessageWcf)
    End Function

    ''' <summary>
    ''' Guardar Canasta a la materia prima.
    ''' </summary>
    ''' <returns></returns>
    Public Async Function SaveBasketsMateriaRaw(ByVal CampaignDetailId As Integer, ByVal campaignId As Integer, ByVal ProductionBasketsId As Integer) As Task(Of ActionResult)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoMixingStation.SaveBasketsMateriaRawAsync(CampaignDetailId, campaignId, ProductionBasketsId, Me._sessionValues.AuditMessageWcf, _sessionValues)
    End Function

    ''' <summary>
    ''' get release line
    ''' </summary>
    ''' <param name="campaignDetailId"></param>
    ''' <returns></returns>
    Public Function GetReleaseLineByCampaignDetailId(campaignDetailId As Integer) As Task(Of ReleaseLine)
        Return IndigoConecta.Instancia.CurrentCloud.IndigoMixingStation.GetReleaseLineAsync(campaignDetailId)
    End Function

    ''' <summary>
    ''' Guardar Canasta a la materia prima.
    ''' </summary>
    ''' <returns></returns>
    Public Async Function ConfirmLabelItems(campaignDetailId As Integer, mixingLabelItems As List(Of MixingLabelModel)) As Task(Of ActionResult)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoMixingStation.ConfirmLabelItemsAsync(campaignDetailId, mixingLabelItems, Me._sessionValues.AuditMessageWcf)
    End Function

    ''' <summary>
    ''' Lista las solicitudes asociadas a la campaña
    ''' </summary>
    ''' <param name="campaignDetailId">Id de la campaña</param>
    Public Function ListCampaignDetailWithRequests(campaignDetailId As Integer) As PLinqServerModeSource
        Return XpoServiceEx.Instance(_sessionValues.TransactionalContainer).MixingStationService.ListCampaignDetailWithRequests(campaignDetailId)
    End Function

    ''' <summary>
    ''' Datasource lineas de producción
    ''' </summary>
    ''' <returns></returns>
    Public Function InitializeProductionLine() As XPInstantFeedbackSource
        Return XpoServiceEx.Instance(_sessionValues.TransactionalContainer).MixingStationService.ListProductionLineByStatus(True)
    End Function

    ''' <summary>
    ''' Datasource tipo de dosis unitarias
    ''' </summary>
    ''' <returns></returns>
    Public Function InitializeUnitDoseType() As XPInstantFeedbackSource
        Return XpoServiceEx.Instance(_sessionValues.TransactionalContainer).MixingStationService.ListUnitDoseTypeByStatus(True)
    End Function

    ''' <summary>
    ''' Datasource de Materia Prima.
    ''' </summary>
    ''' <returns></returns>
    Public Async Function ListProductRawMaterial(IdCampaing As Integer, StockId As Integer, warehouseId As Integer) As Task(Of List(Of ProductMixingStation))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoMixingStation.ListProductRawMaterialAsync(IdCampaing, StockId, warehouseId, 1, _sessionValues)
    End Function

    ''' <summary>
    ''' Lista las canastas
    ''' </summary>
    ''' <returns></returns>
    Public Function ListBaskets(ByVal UnitDoseTypeId As Integer) As XPInstantFeedbackSource
        Return XpoServiceEx.Instance(_sessionValues.TransactionalContainer).MixingStationService.ListProductionBaskets(UnitDoseTypeId)
    End Function

    ''' <summary>
    ''' Proceso Picking
    ''' </summary>
    ''' <param name="productsMixing"></param>
    ''' <param name="campaignDetailId"></param>
    ''' <param name="stockWarehouseId"></param>
    ''' <param name="warehouseId"></param>
    ''' <param name="maquilaId"></param>
    ''' <returns></returns>
    Public Async Function CalculatePicking(ByVal productsMixing As List(Of ProductMixingStation), campaignDetailId As Integer, stockWarehouseId As Integer, warehouseId As Integer?, maquilaId As Integer?, RemnantWarehouseId As Integer) As Task(Of ActionResult(Of CampaignDetailItems))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoMixingStation.CalculatePickingAsync(productsMixing, campaignDetailId, stockWarehouseId, warehouseId, maquilaId, RemnantWarehouseId, _sessionValues.AuditMessageWcf, _sessionValues)
    End Function

    ''' <summary>
    ''' Valida la campaña para finalizarla
    ''' </summary>
    ''' <param name="campaignDetailId"></param>
    ''' <returns></returns>
    Public Function ValidateCampaignToEnd(campaignDetailId As Integer) As Task(Of ActionResult)
        Return IndigoConecta.Instancia.CurrentCloud.IndigoMixingStation.ValidateCampaignToEndAsync(campaignDetailId)
    End Function

    ''' <summary>
    ''' Finaliza una campaña
    ''' </summary>
    ''' <param name="campaignDetailId"></param>
    ''' <returns></returns>
    Public Function EndCampaign(campaignDetailId As Integer, observations As String) As Task(Of ActionResult)
        Return IndigoConecta.Instancia.CurrentCloud.IndigoMixingStation.EndCampaignAsync(campaignDetailId, observations, _sessionValues.IndigoOperatingUnitId, _sessionValues.IndigoCompanyNit, _sessionValues.AuditMessageWcf)
    End Function

    ''' <summary>
    ''' DataSource Materia Prima Estado: Picking
    ''' </summary>
    ''' <param name="campaignDetailId">Id campaña detalle</param>
    ''' <returns></returns>
    Public Async Function ListRawMaterialItems(campaignDetailId As Integer) As Task(Of List(Of CampaignDetailItems))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoMixingStation.ListRawMaterialItemsAsync(campaignDetailId, _sessionValues)
    End Function

    ''' <summary>
    ''' DataSource Materia Prima Estado: Validacion
    ''' </summary>
    ''' <param name="campaignDetailId">Id campaña detalle</param>
    ''' <returns></returns>
    Public Async Function ListRawMaterialItemsValidation(campaignDetailId As Integer) As Task(Of List(Of CampaignDetailItems))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoMixingStation.ListRawMaterialItemsValidationAsync(campaignDetailId, _sessionValues)
    End Function

    ''' <summary>
    ''' Obtengo el Tipo de Reporte por Campaña y entidad
    ''' </summary>
    ''' <param name="Data"></param>
    ''' <returns></returns>
    Public Async Function GetListCampaignReports(Data As Tuple(Of Integer, Integer)) As Task(Of List(Of CampaignReports))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoMixingStation.GetListCampaignReportsByActionAsync(Data, Me._sessionValues.AuditMessageWcf)
    End Function

    ''' <summary>
    '''  Guarda la Orden de Traslado
    ''' </summary>
    ''' <param name="transferOrderlist"></param>
    ''' <param name="CMConfiguration"></param>
    ''' <param name="campaignDetailId"></param>
    ''' <param name="CampaignNumber"></param>
    ''' <returns></returns>
    Public Async Function OrderTranfer(transferOrderlist As TransferOrder, CMConfiguration As Integer, campaignDetailId As Integer, CampaignNumber As Integer) As Task(Of ActionResult(Of TransferOrder))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoMixingStation.OrdertransferAsync(transferOrderlist, CMConfiguration, campaignDetailId, CampaignNumber, _sessionValues.AuditMessageWcf, _sessionValues)
    End Function

    ''' <summary>
    ''' Guarda el tralados de Inventario
    ''' </summary>
    ''' <param name="inventoryRequest"></param>
    ''' <returns></returns>
    Public Async Function SaveInventoryRequest(inventoryRequest As InventoryRequest) As Task(Of ActionResult)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoMixingStation.SaveInventoryRequestAsync(inventoryRequest, _sessionValues.AuditMessageWcf, _sessionValues)
    End Function

    ''' <summary>
    ''' Guarda los paquete según la cantidad requerida con sus difentes estados. 
    ''' </summary>
    ''' <param name="myPackageDetail"></param>
    ''' <returns></returns>
    Public Async Function SavePackageDetailStatus(Optional myPackageDetail As List(Of RequestMixingStationDetail) = Nothing, Optional ListRequestPackageDetailStatus As List(Of RequestPackageDetailStatus) = Nothing) As Task(Of ActionResult)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoMixingStation.SavePackageDetailStatusAsync(_sessionValues.AuditMessageWcf, myPackageDetail, ListRequestPackageDetailStatus)
    End Function

    ''' <summary>
    ''' Guarda o actualiza un detalle de campaña
    ''' </summary>
    ''' <param name="rowDetail"></param>
    ''' <returns></returns>
    Public Async Function SaveCampaignDetailItem(rowDetail As CampaignDetailItems, pickingList As List(Of CampaignDetailPicking), stockWareHouseId As Integer) As Task(Of ActionResult)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoMixingStation.SaveCampaignDetailPickingListAsync(rowDetail, pickingList, stockWareHouseId, _sessionValues.AuditMessageWcf)
    End Function

    ''' <summary>
    ''' Actualiza el estado de la campaña
    ''' </summary>
    ''' <param name="campaignDetailId"></param>
    ''' <param name="statuscampaign"></param>
    ''' <returns></returns>
    Public Async Function UpdateStatusCampaignDetail(campaignDetailId As Integer, statuscampaign As Integer) As Task(Of ActionResult(Of CampaignDetail))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoMixingStation.UpdateStatusCampaignDetailAsync(campaignDetailId, statuscampaign, _sessionValues)
    End Function

    ''' <summary>
    ''' Guardo la materia prima adicional a la campaña
    ''' </summary>
    ''' <param name="productItemDetail">Producto Adicional</param>
    ''' <returns></returns>
    Public Async Function SaveProductItemCampaignDetail(productItemDetail As CampaignDetailItems) As Task(Of ActionResult)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoMixingStation.SaveProductCampaignDetailAsync(productItemDetail, _sessionValues.AuditMessageWcf)
    End Function

    ''' <summary>
    ''' Elimino la materia prima adicional de la campaña
    ''' </summary>
    ''' <param name="productItemDetail">Producto Adicional</param>
    ''' <returns></returns>
    Public Async Function DeleteProductItemCampaignDetail(productItemDetail As CampaignDetailItems) As Task(Of ActionResult)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoMixingStation.DeleteProductCampaignDetailAsync(productItemDetail, _sessionValues.AuditMessageWcf)
    End Function

    ''' <summary>
    ''' Guarda o actualiza validacion de lotes
    ''' </summary>
    ''' <param name="rowDetail"></param>
    ''' <param name="ValidationList"></param>
    ''' <param name="stockWareHouseId"></param>
    ''' <returns></returns>
    Public Async Function SaveCampaignDetailValidation(rowDetail As CampaignDetailItems, ValidationList As List(Of CampaignDetailValidation), stockWareHouseId As Integer) As Task(Of ActionResult)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoMixingStation.SaveCampaignDetailValidationListAsync(rowDetail, ValidationList, stockWareHouseId, _sessionValues.AuditMessageWcf)
    End Function

    ''' <summary>
    ''' Guarda o actualiza validacion de lotes
    ''' </summary>
    ''' <returns></returns>
    Public Function ManualDeliveryAsync(items As List(Of CampaignDetailItems)) As Task(Of ActionResult)
        Return IndigoConecta.Instancia.CurrentCloud.IndigoMixingStation.ManualDeliveryAsync(items, _sessionValues)
    End Function

    ''' <summary>
    ''' metodo para obtener un inventario fisico cuando se hace por codigo de barras
    ''' </summary>
    ''' <param name="productCode"></param>
    ''' <param name="batchCode"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetPhysicalInventoryBarCode(productCode As String, batchCode As String) As ActionResult(Of List(Of PhysicalInventory))
        Return IndigoConecta.Instancia.CurrentCloud.IndigoInventory.GetPhysicalInventoryBarCode(productCode, batchCode, _sessionValues.UserIndigoId)
    End Function

    ''' <summary>
    ''' Obtiene los detalles de los paquetes por campaña
    ''' </summary>
    ''' <returns></returns>
    Public Function GetItemsByCampaigns(campaignDetailId As Integer) As ActionResult(Of SP_ListViewItemsCampaigns_Result)
        Return IndigoConecta.Instancia.CurrentCloud.IndigoMixingStation.GetItemsCampaigns(campaignDetailId, Me._sessionValues.AuditMessageWcf)
    End Function

    ''' <summary>
    ''' funcion para guardar o actualizar un registro en la tabla de soibrantes
    ''' </summary>
    ''' <param name="ListQuantityRemaining"></param>
    ''' <returns></returns>
    Public Async Function SaveQuantityRemaining(ListQuantityRemaining As List(Of QuantityRemaining)) As Task(Of ActionResult)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoMixingStation.SaveQuantityRemainingAsync(ListQuantityRemaining, Me._sessionValues.AuditMessageWcf)
    End Function

    ''' <summary>
    ''' consulta a la tabla sobrantes
    ''' </summary>
    ''' <returns></returns>
    Public Async Function GetQuantityRemainingByMixingStation(_cMConfigurationId As Integer) As Task(Of ActionResult(Of List(Of QuantityRemaining)))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoMixingStation.GetQuantityRemainingByMixingStationAsync(_cMConfigurationId)
    End Function

    ''' <summary>
    ''' funcion para registrar aprovechamientos
    ''' </summary>
    ''' <param name="ListQuantityRemaining"></param>
    ''' <returns></returns>
    Public Async Function RegisterHarnessed(ListQuantityRemaining As List(Of QuantityRemaining), FromCampaignDetailId As Integer) As Task(Of ActionResult)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoMixingStation.RegisterHarnessedAsync(ListQuantityRemaining, FromCampaignDetailId, Me._sessionValues.AuditMessageWcf)
    End Function

    ''' <summary>
    ''' funcion para registrar materia prima Indirecta
    ''' </summary>
    ''' <param name="ListIndirectMPQuantity"></param>
    ''' <returns></returns>
    Public Async Function RegisterIndirectMPQuantityAsync(ListIndirectMPQuantity As List(Of IndirectMPQuantity)) As Task(Of ActionResult)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoMixingStation.RegisterIndirectMPQuantityAsync(ListIndirectMPQuantity, Me._sessionValues.AuditMessageWcf)
    End Function

    Public Async Function GetRequestMixingDetailStatusByIdsAsync(ids As List(Of Integer)) As Task(Of List(Of RequestPackageDetailStatus))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoMixingStation.GetRequestMixingDetailStatusByIdsAsync(ids)
    End Function

    ''' <summary>
    ''' Lista los Productos del Paquete segun la cantidad Seleccionada
    ''' </summary>
    ''' <param name="Data"></param>
    ''' <returns></returns>
    Public Async Function getProductDetail(packageDetailStatusIds As List(Of Integer), Data As Tuple(Of Integer, Integer)) As Task(Of ActionResult(Of ManageRawMaterialModel))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoMixingStation.GetProductDetailByCampaignIdAsync(packageDetailStatusIds, Data)
    End Function

    ''' <summary>
    ''' Obtengo el almacén por el tipo de paramterizacion de la Central de mezclas
    ''' </summary>
    ''' <returns></returns>
    Public Function ListViewMixingStationWarehouse(cmConfigurationId As Integer, type As Integer) As ViewListWarehouseTypeXpo
        Dim filter As String = String.Format("IdMixingStation = {0} and WarehouseType = {1}", cmConfigurationId, type)
        Return XpoServiceEx.Instance(_sessionValues.TransactionalContainer).MixingStationService.GetCollection(Of ViewListWarehouseTypeXpo)(Nothing, filter).FirstOrDefault()
    End Function

    Public Function ListWorkingAreas(state As Boolean, CMConfigurationId As Integer) As XPInstantFeedbackSource
        Return XpoServiceEx.Instance(_sessionValues.TransactionalContainer).MixingStationService.ListXPInstantFeedbackSource(Of WorkingAreaXpo)($"Status={state} And CMConfigurationId = {CMConfigurationId}")
    End Function

    ''' <summary>
    ''' Guarda la gestion de la Materia Prima
    ''' </summary>
    ''' <param name="myListCampaignRawMaterial"></param>
    ''' <param name="_packageIds"></param>
    ''' <param name="RequestMixingStationDetailId"></param>
    ''' <returns></returns>
    Public Async Function SaveManageCampaignRawMaterialsAsync(myListCampaignRawMaterial As List(Of ManageRawMaterialModel), _packageIds As List(Of Integer), RequestMixingStationDetailId As Integer) As Task(Of ActionResult)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoMixingStation.SaveManageCampaignRawMaterialsAsync(myListCampaignRawMaterial, _packageIds, RequestMixingStationDetailId, Me._sessionValues.AuditMessageWcf)
    End Function

    ''' <summary>
    ''' Valida los almacenes cuando se hace ordenes con el fin de devolver mensajes especificos de Central de Mezclas
    ''' </summary>
    ''' <param name="args"></param>
    ''' <returns></returns>
    Public Function ValidateWarehouses(args As Object) As Task(Of ActionResult)
        Dim parameter As String = Utils.SerializeObjectToJson(args)
        Return IndigoConecta.Instancia.CurrentCloud.IndigoMixingStation.ValidateWarehouseMSAsync(parameter)
    End Function
#End Region

#Region "IDisposable Support"
    Private disposedValue As Boolean ' To detect redundant calls

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not Me.disposedValue Then
            If disposing Then
                ' TODO: dispose managed state (managed objects).
            End If

            ' TODO: free unmanaged resources (unmanaged objects) and override Finalize() below.
            ' TODO: set large fields to null.
        End If
        Me.disposedValue = True
    End Sub

    ' This code added by Visual Basic to correctly implement the disposable pattern.
    Public Sub Dispose() Implements IDisposable.Dispose
        ' Do not change this code.  Put cleanup code in Dispose(disposing As Boolean) above.
        Dispose(True)
        GC.SuppressFinalize(Me)
    End Sub
#End Region

End Class
