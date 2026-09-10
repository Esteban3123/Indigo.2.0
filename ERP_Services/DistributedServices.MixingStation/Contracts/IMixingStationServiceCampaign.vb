'***********************************************************************
' Assembly         : DistributedServices.MixinStation
' Author           : Carlos Mario Arias Rubiano
' Created          : 09/02/2021
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports System.ServiceModel
Imports Domain.Base.Entities
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base

<ServiceContract()>
Public Interface IMixingStationServiceCampaign

    ''' <summary>
    ''' Guarda o Actualiza un grupo
    ''' </summary>
    ''' <param name="audit">The audit.</param>
    ''' <returns></returns>
    <OperationContract()>
    Function SaveCampaign(objParams As String, audit As AuditMessage) As ActionResult(Of SP_SaveCampaign_Result)

    ''' <summary>
    ''' Guarda la campaña y genera los items RequestMixingStationDetail
    ''' </summary>
    ''' <param name="objParams"></param>
    ''' <param name="listPackageDetail"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    <OperationContract()>
    Function SaveCampaignAndMixingStationDetailAsync(objParams As String, listPackageDetail As List(Of RequestMixingStationDetail), audit As AuditMessage) As Task(Of ActionResult(Of SP_SaveCampaign_Result))

    ''' <summary>
    ''' Obtiene una Campana por id
    ''' </summary>
    ''' <param name="id"></param>
    ''' <returns></returns>
    <OperationContract()>
    Function GetCampaignById(ByVal id As Integer) As ActionResult(Of Campaign)

    ''' <summary>
    ''' Obtiene un registro de la tabla campaingDetail por Id
    ''' </summary>
    ''' <returns></returns>
    <OperationContract()>
    Function GetCampaignDetailById(ByVal id As Integer, Optional ByVal ValidateProcessRawMaterial As Boolean = False) As ActionResult(Of CampaignDetail)

    ''' <summary>
    ''' Elimina la campaña
    ''' </summary>
    ''' <param name="audit">The audit.</param>
    ''' <returns></returns>
    <OperationContract()>
    Function DeleteCampaign(campaignDetailId As Integer, TransactionalContainer As String, ByVal audit As AuditMessage) As ActionResult

    ''' <summary>
    ''' Anula los pacientes
    ''' </summary>
    ''' <param name="objParams"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    <OperationContract()>
    Function AnnulatePatients(objParams As String, audit As AuditMessage) As ActionResult(Of SP_ProcessMixingStation_Result)

    ''' <summary>
    ''' Guardo los paquetes segun la cantidad solicitada
    ''' </summary>
    ''' <param name="listPackageDetail"></param>
    ''' <returns></returns>
    <OperationContract()>
    Function SavePackageDetailStatus(ByVal audit As AuditMessage, Optional ByVal listPackageDetail As List(Of RequestMixingStationDetail) = Nothing, Optional ByVal PackageDetailStatus As List(Of RequestPackageDetailStatus) = Nothing) As Task(Of ActionResult)

    ''' <summary>
    ''' Lista el picking por detalle de la campaña
    ''' </summary>
    ''' <param name="campaignDetailId"></param>
    ''' <returns></returns>
    <OperationContract()>
    Function GetCampaignDetailPickingByCampaignDetailId(campaignDetailId As Integer) As ActionResult(Of List(Of CampaignDetailPicking))

    ''' <summary>
    ''' Lista lotes valdiados por detalle de la campaña
    ''' </summary>
    ''' <param name="campaignDetailId"></param>
    ''' <returns></returns>
    <OperationContract()>
    Function GetCampaignDetailValidationByCampaignDetailId(campaignDetailId As Integer) As ActionResult(Of List(Of CampaignDetailValidation))

    <OperationContract()>
    Function GetCampaignDetailValidationForDevolution(campaignDetailId As Integer) As ActionResult(Of List(Of CampaignDetailValidation))


    ''' <summary>
    ''' Guarda un listado de detalle de picking
    ''' </summary>
    ''' <param name="campaignDetailPicking"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    <OperationContract()>
    Function SaveCampaignDetailPickingList(campaignDetailItem As CampaignDetailItems, campaignDetailPicking As List(Of CampaignDetailPicking), stockWareHouseId As Integer, audit As AuditMessage) As ActionResult

    ''' <summary>
    ''' Confirma el etiquetado de los items
    ''' </summary>
    ''' <param name="campaignDetailId"></param>
    ''' <param name="mixingLabelItems"></param>
    ''' <returns></returns>
    <OperationContract()>
    Function ConfirmLabelItems(campaignDetailId As Integer, mixingLabelItems As List(Of MixingLabelModel), audit As AuditMessage) As ActionResult

    ''' <summary>
    ''' Guarda o actualiza validacion de lotes
    ''' </summary>
    ''' <param name="campaignDetailItem"></param>
    ''' <param name="campaignDetailValdiation"></param>
    ''' <param name="stockWareHouseId"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    <OperationContract()>
    Function SaveCampaignDetailValidationList(campaignDetailItem As CampaignDetailItems, campaignDetailValdiation As List(Of CampaignDetailValidation), stockWareHouseId As Integer, audit As AuditMessage) As ActionResult

    ''' <summary>
    ''' Lista los Detalles del Paquete segun la cantidad Seleccionada
    ''' </summary>
    ''' <param name="Data"></param>
    ''' <returns></returns>
    <OperationContract()>
    Function GetProductDetailByCampaignId(packageDetailStatusIds As List(Of Integer), Data As Tuple(Of Integer, Integer)) As ActionResult(Of ManageRawMaterialModel)

    ''' <summary>
    ''' Finaliza una campaña
    ''' </summary>
    ''' <param name="campaignDetailId"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    <OperationContract()>
    Function EndCampaign(campaignDetailId As Integer, observations As String, operativeUnitId As Integer, companyNit As String, audit As AuditMessage) As Task(Of ActionResult)

    ''' <summary>
    ''' Valida la campaña para terminacion
    ''' </summary>
    ''' <param name="campaignDetailid"></param>
    ''' <returns></returns>
    <OperationContract()>
    Function ValidateCampaignToEnd(campaignDetailid As Integer) As ActionResult

    ''' <summary>
    ''' Valida los almacenes para No afectar el SP y devolver mensajes especificos de Central de Mezclas
    ''' </summary>
    ''' <param name="objParams"></param>
    ''' <returns></returns>
    <OperationContract()>
    Function ValidateWarehouseMS(objParams As String) As ActionResult

    ''' <summary>
    ''' Guardo la Gestion de Materia Prima para una solitud de Campaña
    ''' </summary>
    ''' <param name="myListCampaignRawMaterial"></param>
    ''' <param name="productPackageIds"></param>
    ''' <param name="RequestMixingStationDetailId"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    <OperationContract()>
    Function SaveManageCampaignRawMaterialsAsync(myListCampaignRawMaterial As List(Of ManageRawMaterialModel), productPackageIds As List(Of Integer), RequestMixingStationDetailId As Integer, audit As AuditMessage) As Task(Of ActionResult)

    ''' <summary>
    ''' Liberación de línea
    ''' </summary>
    ''' <param name="releaseLine"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    <OperationContract()>
    Function SaveReleaseLine(releaseLine As ReleaseLine, audit As AuditMessage) As ActionResult

    ''' <summary>
    ''' Obtiene un release line
    ''' </summary>
    ''' <param name="campaignDetailId"></param>
    ''' <returns></returns>
    <OperationContract>
    Function GetReleaseLine(campaignDetailId As Integer) As ReleaseLine
    ''' <summary>
    ''' Obtiene un listado de status
    ''' </summary>
    ''' <param name="ids"></param>
    ''' <returns></returns>
    <OperationContract>
    Function GetRequestMixingDetailStatusByIds(ids As List(Of Integer)) As List(Of RequestPackageDetailStatus)
    <OperationContract>
    Function ProcessFinishedProductAsync(requestMixingStationDetailIds As List(Of Integer), audit As AuditMessage) As Task(Of ActionResult(Of List(Of Tuple(Of String, Integer))))
    <OperationContract>
    Function GetLastReleaseLineUsedByWorkingAreaId(releaseLineId As Integer, workingAreaId As Integer) As ReleaseLine
    <OperationContract>
    Function GetCampaignDetailWitnessFile(campaignDetailId As Integer) As ActionResult(Of List(Of CampaignDetailWitnessFile))
    <OperationContract>
    Function SaveCampaignDetailWitnessFile(files As List(Of CampaignDetailWitnessFile), audit As AuditMessage) As ActionResult

    ''' <summary>
    ''' Registra el motivo de anulacion de una adecuación NPT
    ''' </summary>
    ''' <returns></returns>
    <OperationContract>
    Function CancellationadjustmentsNPT(MixingstationDetailId As Integer, arguments As String, audit As AuditMessage) As ActionResult

End Interface
