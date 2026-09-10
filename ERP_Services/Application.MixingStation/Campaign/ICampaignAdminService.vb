'***********************************************************************
' Assembly         : Application.MixingStation
' Author           : Carlos Mario Arias Rubiano
' Created          : 09/02/2021
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities

Public Interface ICampaignAdminService
    Inherits IDisposable

    ''' <summary>
    ''' Procesa la central de mezclas
    ''' </summary>
    ''' <param name="objParams"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    Function SaveCampaign(objParams As String, audit As AuditMessage) As ActionResult(Of SP_SaveCampaign_Result)

    ''' <summary>
    ''' Obtiene un grupo uvr por id
    ''' </summary>
    ''' <returns></returns>
    Function GetCampaignById(ByVal id As Integer) As ActionResult(Of Campaign)

    ''' <summary>
    ''' Obtiene un registro de la tabla CampaingDetail por Id
    ''' </summary>
    ''' <returns></returns>
    Function GetCampaignDetailById(ByVal id As Integer, Optional ByVal ValidateProcessRawMaterial As Boolean = False) As ActionResult(Of CampaignDetail)

    ''' <summary>
    ''' Elimina la campaña
    ''' </summary>
    ''' <param name="audit">The audit.</param>
    ''' <returns></returns>
    Function DeleteCampaign(campaignDetailId As Integer, TransactionalContainer As String, ByVal audit As AuditMessage) As ActionResult

    ''' <summary>
    ''' Anula los pacientes
    ''' </summary>
    ''' <param name="objParams"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    Function AnnulatePatients(objParams As String, audit As AuditMessage) As ActionResult(Of SP_ProcessMixingStation_Result)

    ''' <summary>
    ''' Guardo los paquetes segun la cantidad solicitada 
    ''' </summary>
    ''' <param name="listPackageDetail"></param>
    ''' <returns></returns>
    Function SavePackageDetailStatusAsync(ByVal audit As AuditMessage, Optional ByVal listPackageDetail As List(Of RequestMixingStationDetail) = Nothing, Optional ByVal PackageDetailStatus As List(Of RequestPackageDetailStatus) = Nothing) As Task(Of ActionResult)

    ''' <summary>
    ''' Lista el picking por detalle de la campaña
    ''' </summary>
    ''' <param name="campaignDetailId"></param>
    ''' <returns></returns>
    Function GetCampaignDetailPickingByCampaignDetailId(campaignDetailId As Integer) As ActionResult(Of List(Of CampaignDetailPicking))

    ''' <summary>
    ''' Lista de lotes validados por detalle de la campaña
    ''' </summary>
    ''' <param name="campaignDetailId"></param>
    ''' <returns></returns>
    Function GetCampaignDetailValidationByCampaignDetailId(campaignDetailId As Integer) As ActionResult(Of List(Of CampaignDetailValidation))

    Function GetCampaignDetailValidationForDevolution(campaignDetailId As Integer) As ActionResult(Of List(Of CampaignDetailValidation))

    ''' <summary>
    ''' guarda un listado de detalles de picking
    ''' </summary>
    ''' <returns></returns>
    Function SaveCampaignDetailPickingList(campaignDetailItem As CampaignDetailItems, campaignDetailPicking As List(Of CampaignDetailPicking), stockWareHouseId As Integer, audit As AuditMessage) As ActionResult

    ''' <summary>
    ''' Guarda la campaña y genera los items RequestMixingStationDetail
    ''' </summary>
    ''' <param name="objParams"></param>
    ''' <param name="listPackageDetail"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    Function SaveCampaignAndMixingStationDetailAsync(objParams As String, listPackageDetail As List(Of RequestMixingStationDetail), audit As AuditMessage) As Task(Of ActionResult(Of SP_SaveCampaign_Result))

    ''' <summary>
    ''' Confirma el etiquetado de los items
    ''' </summary>
    ''' <param name="campaignDetailId"></param>
    ''' <param name="mixingLabelItems"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    Function ConfirmLabelItems(campaignDetailId As Integer, mixingLabelItems As List(Of MixingLabelModel), audit As AuditMessage) As ActionResult

    ''' <summary>
    ''' guarda un listado de detalles de validacion de lotes
    ''' </summary>
    ''' <returns></returns>
    Function SaveCampaignDetailValidationList(campaignDetailItem As CampaignDetailItems, campaignDetailValdiation As List(Of CampaignDetailValidation), stockWareHouseId As Integer, audit As AuditMessage) As ActionResult

    ''' <summary>
    ''' Lista los Detalles del Paquete segun la cantidad Seleccionada
    ''' </summary>
    ''' <param name="Data"></param>
    ''' <returns></returns>
    Function GetProductDetailByCampaignId(packageDetailStatusIds As List(Of Integer), Data As Tuple(Of Integer, Integer)) As ActionResult(Of ManageRawMaterialModel)

    ''' <summary>
    ''' Finaliza una campaña
    ''' </summary>
    ''' <param name="campaignDetailId"></param>
    ''' <returns></returns>
    Function EndCampaignAsync(campaignDetailId As Integer, observations As String, operativeUnitId As Integer, companyNit As String, audit As AuditMessage) As Task(Of ActionResult)

    ''' <summary>
    ''' Valida la campaña para terminacion
    ''' </summary>
    ''' <param name="campaignDetailid"></param>
    ''' <returns></returns>
    Function ValidateCampaignToEnd(campaignDetailid As Integer) As ActionResult

    ''' <summary>
    ''' Valida los almacenes para procesar solicitudes externas a central de Mezclas
    ''' </summary>
    ''' <param name="objParams"></param>
    ''' <returns></returns>
    Function ValidateWarehouseMS(objParams As String) As ActionResult

    ''' <summary>
    ''' Guarda la gestion de la Materia Prima
    ''' </summary>
    ''' <param name="myListCampaignRawMaterial"></param>
    ''' <param name="productPackageIds"></param>
    ''' <param name="RequestMixingStationDetailId"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    Function SaveManageCampaignRawMaterialsAsync(myListCampaignRawMaterial As List(Of ManageRawMaterialModel), productPackageIds As List(Of Integer), RequestMixingStationDetailId As Integer, audit As AuditMessage) As Task(Of ActionResult)

    ''' <summary>
    ''' Liberación de línea
    ''' </summary>
    ''' <param name="releaseLine"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    Function SaveReleaseLine(releaseLine As ReleaseLine, audit As AuditMessage) As ActionResult
    ''' <summary>
    ''' Obtiene un release line
    ''' </summary>
    ''' <param name="campaignDetailId"></param>
    ''' <returns></returns>
    Function GetReleaseLine(campaignDetailId As Integer) As ReleaseLine
    Function GetRequestMixingDetailStatusByIds(ids As List(Of Integer)) As List(Of RequestPackageDetailStatus)
    Function ProcessFinishedProductAsync(requestMixingStationDetailId As List(Of Integer), audit As AuditMessage) As Task(Of ActionResult(Of List(Of Tuple(Of String, Integer))))
    Function GetLastReleaseLineUsedByWorkingAreaId(releaseLineId As Integer, workingAreaId As Integer) As ReleaseLine
    Function GetCampaignDetailWitnessFile(campaignDetailId As Integer) As ActionResult(Of List(Of CampaignDetailWitnessFile))
    Function SaveCampaignDetailWitnessFile(files As List(Of CampaignDetailWitnessFile), audit As AuditMessage) As ActionResult

    ''' <summary>
    ''' Registra el motivo de anulacion de una adecuación NPT
    ''' </summary>
    ''' <returns></returns>
    Function CancellationadjustmentsNPT(MixingstationDetailId As Integer, arguments As String, audit As AuditMessage) As ActionResult
End Interface


