'***********************************************************************
' Assembly         : DistributedService.MixingStation
' Author           : Carlos Mario Arias Rubiano
' Created          : 09/02/2021
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"
Imports Application.MixingStation
Imports Domain.Base.Entities
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Microsoft.Practices.Unity
#End Region

Partial Class MixingStationService
    Implements IMixingStationServiceCampaign

    ''' <summary>
    ''' Obtener una Campañar por ID 
    ''' </summary>
    ''' <param name="id"></param>
    ''' <returns></returns>
    Public Function GetCampaignById(id As Integer) As ActionResult(Of Campaign) Implements IMixingStationServiceCampaign.GetCampaignById
        'Using service As ICampaignAdminService = Container.Current.BeginLifetimeScope().Resolve(Of ICampaignAdminService)()
        '    Return service.GetCampaignById(id)
        'End Using
        Using service As ICampaignAdminService = Container.Current.Resolve(Of ICampaignAdminService)()
            Return service.GetCampaignById(id)
        End Using
    End Function

    Public Function GetCampaignDetailById(id As Integer, Optional ValidateProcessRawMaterial As Boolean = False) As ActionResult(Of CampaignDetail) Implements IMixingStationServiceCampaign.GetCampaignDetailById
        Using service As ICampaignAdminService = Container.Current.Resolve(Of ICampaignAdminService)()
            Return service.GetCampaignDetailById(id, ValidateProcessRawMaterial)
        End Using
    End Function

    ''' <summary>
    ''' Guarda y Actualiza una Campaña
    ''' </summary>
    ''' <param name="objParams"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    Public Async Function SaveCampaignAndMixingStationDetailAsync(objParams As String, listPackageDetail As List(Of RequestMixingStationDetail), audit As AuditMessage) As Task(Of ActionResult(Of SP_SaveCampaign_Result)) Implements IMixingStationServiceCampaign.SaveCampaignAndMixingStationDetailAsync
        Using service As ICampaignAdminService = Container.Current.Resolve(Of ICampaignAdminService)()
            Return Await service.SaveCampaignAndMixingStationDetailAsync(objParams, listPackageDetail, audit)
        End Using
    End Function

    ''' <summary>
    ''' Guarda y Actualiza una Campaña
    ''' </summary>
    ''' <param name="objParams"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    Public Function SaveCampaign(objParams As String, audit As AuditMessage) As ActionResult(Of SP_SaveCampaign_Result) Implements IMixingStationServiceCampaign.SaveCampaign
        Using service As ICampaignAdminService = Container.Current.Resolve(Of ICampaignAdminService)()
            Return service.SaveCampaign(objParams, audit)
        End Using
    End Function

    ''' <summary>
    ''' Elimina la campaña
    ''' </summary>
    ''' <param name="campaignDetailId"></param>
    ''' <param name="TransactionalContainer"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    Public Function DeleteCampaign(campaignDetailId As Integer, TransactionalContainer As String, audit As AuditMessage) As ActionResult Implements IMixingStationServiceCampaign.DeleteCampaign
        Using service As ICampaignAdminService = Container.Current.Resolve(Of ICampaignAdminService)()
            Return service.DeleteCampaign(campaignDetailId, TransactionalContainer, audit)
        End Using
    End Function

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="objParams"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    Public Function AnnulatePatients(objParams As String, audit As AuditMessage) As ActionResult(Of SP_ProcessMixingStation_Result) Implements IMixingStationServiceCampaign.AnnulatePatients
        Using service As ICampaignAdminService = Container.Current.Resolve(Of ICampaignAdminService)()
            Return service.AnnulatePatients(objParams, audit)
        End Using
    End Function

    ''' <summary>
    ''' Guardo el paquete segun las cantidades Solicitadas
    ''' </summary>
    ''' <param name="listPackageDetail"></param>
    ''' <returns></returns>
    Public Async Function SavePackageDetailStatusAsync(audit As AuditMessage, Optional listPackageDetail As List(Of RequestMixingStationDetail) = Nothing, Optional PackageDetailStatus As List(Of RequestPackageDetailStatus) = Nothing) As Task(Of ActionResult) Implements IMixingStationServiceCampaign.SavePackageDetailStatus
        Using service As ICampaignAdminService = Container.Current.Resolve(Of ICampaignAdminService)()
            Return Await service.SavePackageDetailStatusAsync(audit, listPackageDetail, PackageDetailStatus)
        End Using
    End Function

    Public Function GetCampaignDetailPickingByCampaignDetailId(campaignDetailId As Integer) As ActionResult(Of List(Of CampaignDetailPicking)) Implements IMixingStationServiceCampaign.GetCampaignDetailPickingByCampaignDetailId
        Using service As ICampaignAdminService = Container.Current.Resolve(Of ICampaignAdminService)()
            Return service.GetCampaignDetailPickingByCampaignDetailId(campaignDetailId)
        End Using
    End Function

    Public Function GetCampaignDetailValidationByCampaignDetailId(campaignDetailId As Integer) As ActionResult(Of List(Of CampaignDetailValidation)) Implements IMixingStationServiceCampaign.GetCampaignDetailValidationByCampaignDetailId
        Using service As ICampaignAdminService = Container.Current.Resolve(Of ICampaignAdminService)()
            Return service.GetCampaignDetailValidationByCampaignDetailId(campaignDetailId)
        End Using
    End Function

    Public Function SaveCampaignDetailPickingList(campaignDetailItem As CampaignDetailItems, campaignDetailPicking As List(Of CampaignDetailPicking), stockWareHouseId As Integer, audit As AuditMessage) As ActionResult Implements IMixingStationServiceCampaign.SaveCampaignDetailPickingList
        Using service As ICampaignAdminService = Container.Current.Resolve(Of ICampaignAdminService)()
            Return service.SaveCampaignDetailPickingList(campaignDetailItem, campaignDetailPicking, stockWareHouseId, audit)
        End Using
    End Function

    Public Function SaveCampaignDetailValidationList(campaignDetailItem As CampaignDetailItems, campaignDetailValdiation As List(Of CampaignDetailValidation), stockWareHouseId As Integer, audit As AuditMessage) As ActionResult Implements IMixingStationServiceCampaign.SaveCampaignDetailValidationList
        Using service As ICampaignAdminService = Container.Current.Resolve(Of ICampaignAdminService)()
            Return service.SaveCampaignDetailValidationList(campaignDetailItem, campaignDetailValdiation, stockWareHouseId, audit)
        End Using
    End Function

    ''' <summary>
    ''' Confirma el etiquetado de los items
    ''' </summary>
    ''' <param name="campaignDetailId"></param>
    ''' <param name="mixingLabelItems"></param>
    ''' <returns></returns>
    Public Function ConfirmLabelItems(campaignDetailId As Integer, mixingLabelItems As List(Of MixingLabelModel), audit As AuditMessage) As ActionResult Implements IMixingStationServiceCampaign.ConfirmLabelItems
        Using service As ICampaignAdminService = Container.Current.Resolve(Of ICampaignAdminService)()
            Return service.ConfirmLabelItems(campaignDetailId, mixingLabelItems, audit)
        End Using
    End Function

    ''' <summary>
    ''' Lista los Detalles del Paquete segun la cantidad Seleccionada
    ''' </summary>
    ''' <param name="Data"></param>
    ''' <returns></returns>
    Public Function GetProductDetailByCampaignId(packageDetailStatusIds As List(Of Integer), Data As Tuple(Of Integer, Integer)) As ActionResult(Of ManageRawMaterialModel) Implements IMixingStationServiceCampaign.GetProductDetailByCampaignId
        Using service As ICampaignAdminService = Container.Current.Resolve(Of ICampaignAdminService)()
            Return service.GetProductDetailByCampaignId(packageDetailStatusIds, Data)
        End Using
    End Function

    ''' <summary>
    ''' Finaliza una campaña
    ''' </summary>
    ''' <param name="campaignDetailId"></param>
    ''' <returns></returns>
    Public Async Function EndCampaignAsync(campaignDetailId As Integer, observations As String, operativeUnitId As Integer, companyNit As String, audit As AuditMessage) As Task(Of ActionResult) Implements IMixingStationServiceCampaign.EndCampaign
        Using service As ICampaignAdminService = Container.Current.Resolve(Of ICampaignAdminService)()
            Return Await service.EndCampaignAsync(campaignDetailId, observations, operativeUnitId, companyNit, audit)
        End Using
    End Function

    ''' <summary>
    ''' Valida la campaña para terminacion
    ''' </summary>
    ''' <param name="campaignDetailid"></param>
    ''' <returns></returns>
    Public Function ValidateCampaignToEnd(campaignDetailid As Integer) As ActionResult Implements IMixingStationServiceCampaign.ValidateCampaignToEnd
        Using service As ICampaignAdminService = Container.Current.Resolve(Of ICampaignAdminService)()
            Return service.ValidateCampaignToEnd(campaignDetailid)
        End Using
    End Function

    ''' <summary>
    ''' Valida los almacenes para mostrar mensajes especificos de Central de Mezclas
    ''' </summary>
    ''' <param name="objParams"></param>
    ''' <returns></returns>
    Function ValidateWarehouseMS(objParams As String) As ActionResult Implements IMixingStationServiceCampaign.ValidateWarehouseMS
        Using Service As ICampaignAdminService = Container.Current.Resolve(Of ICampaignAdminService)()
            Return Service.ValidateWarehouseMS(objParams)
        End Using
    End Function

    ''' <summary>
    ''' Guarda la gestion de la Materia Prima
    ''' </summary>
    ''' <param name="myListCampaignRawMaterial"></param>
    ''' <param name="productPackageIds"></param>
    ''' <param name="RequestMixingStationDetailId"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    Public Async Function SaveManageCampaignRawMaterials(myListCampaignRawMaterial As List(Of ManageRawMaterialModel), productPackageIds As List(Of Integer), RequestMixingStationDetailId As Integer, audit As AuditMessage) As Task(Of ActionResult) Implements IMixingStationServiceCampaign.SaveManageCampaignRawMaterialsAsync
        Using service As ICampaignAdminService = Container.Current.Resolve(Of ICampaignAdminService)()
            Return Await service.SaveManageCampaignRawMaterialsAsync(myListCampaignRawMaterial, productPackageIds, RequestMixingStationDetailId, audit)
        End Using
    End Function

    ''' <summary>
    ''' Liberación de línea
    ''' </summary>
    ''' <param name="releaseLine"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    Public Function SaveReleaseLine(releaseLine As ReleaseLine, audit As AuditMessage) As ActionResult Implements IMixingStationServiceCampaign.SaveReleaseLine
        Using service As ICampaignAdminService = Container.Current.Resolve(Of ICampaignAdminService)()
            Return service.SaveReleaseLine(releaseLine, audit)
        End Using
    End Function

    ''' <summary>
    ''' Obtiene un release line
    ''' </summary>
    ''' <param name="campaignDetailId"></param>
    ''' <returns></returns>
    Public Function GetReleaseLine(campaignDetailId As Integer) As ReleaseLine Implements IMixingStationServiceCampaign.GetReleaseLine
        Using service As ICampaignAdminService = Container.Current.Resolve(Of ICampaignAdminService)()
            Return service.GetReleaseLine(campaignDetailId)
        End Using
    End Function

    ''' <summary>
    ''' Lista status
    ''' </summary>
    ''' <param name="ids"></param>
    ''' <returns></returns>
    Public Function GetRequestMixingDetailStatusByIds(ids As List(Of Integer)) As List(Of RequestPackageDetailStatus) Implements IMixingStationServiceCampaign.GetRequestMixingDetailStatusByIds
        Using service As ICampaignAdminService = Container.Current.Resolve(Of ICampaignAdminService)()
            Return service.GetRequestMixingDetailStatusByIds(ids)
        End Using
    End Function

    Public Async Function ProcessFinishedProductAsync(requestMixingStationDetailIds As List(Of Integer), audit As AuditMessage) As Task(Of ActionResult(Of List(Of Tuple(Of String, Integer)))) Implements IMixingStationServiceCampaign.ProcessFinishedProductAsync
        Using service As ICampaignAdminService = Container.Current.Resolve(Of ICampaignAdminService)()
            Return Await service.ProcessFinishedProductAsync(requestMixingStationDetailIds, audit)
        End Using
    End Function

    Public Function GetLastReleaseLineUsedByWorkingAreaId(releaseLineId As Integer, workingAreaId As Integer) As ReleaseLine Implements IMixingStationServiceCampaign.GetLastReleaseLineUsedByWorkingAreaId
        Using service As ICampaignAdminService = Container.Current.Resolve(Of ICampaignAdminService)()
            Return service.GetLastReleaseLineUsedByWorkingAreaId(releaseLineId, workingAreaId)
        End Using
    End Function

    Public Function GetCampaignDetailWitnessFile(campaignDetailId As Integer) As ActionResult(Of List(Of CampaignDetailWitnessFile)) Implements IMixingStationServiceCampaign.GetCampaignDetailWitnessFile
        Using service As ICampaignAdminService = Container.Current.Resolve(Of ICampaignAdminService)()
            Return service.GetCampaignDetailWitnessFile(campaignDetailId)
        End Using
    End Function

    Public Function SaveCampaignDetailWitnessFile(files As List(Of CampaignDetailWitnessFile), audit As AuditMessage) As ActionResult Implements IMixingStationServiceCampaign.SaveCampaignDetailWitnessFile
        Using service As ICampaignAdminService = Container.Current.Resolve(Of ICampaignAdminService)()
            Return service.SaveCampaignDetailWitnessFile(files, audit)
        End Using
    End Function

    ''' <summary>
    ''' Registra el motivo de anulacion de una adecuación NPT
    ''' </summary>
    ''' <returns></returns>
    Public Function CancellationadjustmentsNPT(MixingstationDetailId As Integer, arguments As String, audit As AuditMessage) As ActionResult Implements IMixingStationServiceCampaign.CancellationadjustmentsNPT
        Using service As ICampaignAdminService = Container.Current.Resolve(Of ICampaignAdminService)()
            Return service.CancellationadjustmentsNPT(MixingstationDetailId, arguments, audit)
        End Using
    End Function
End Class
