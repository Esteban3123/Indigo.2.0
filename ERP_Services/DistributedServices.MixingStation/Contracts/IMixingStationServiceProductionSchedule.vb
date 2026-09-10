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
Public Interface IMixingStationServiceProductionSchedule

    ''' <summary>
    ''' Guarda orden de produccion
    ''' </summary>
    ''' <param name="objParams"></param>
    ''' <param name="authorizeUserslist"></param>
    ''' <param name="campaignDetail"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    <OperationContract()>
    Function SaveProductionScheduleAsync(objParams As String, authorizeUserslist As List(Of CampaignDetailUsers), campaignDetail As CampaignDetail, audit As AuditMessage) As Task(Of ActionResult(Of SP_SaveProductionSchedule_Result))


    ''' <summary>
    ''' Obtiene un grupo por codigo
    ''' </summary>
    ''' <param name="code">The code.</param>
    ''' <returns></returns>
    <OperationContract()>
    Function GetProductionSchedule(ByVal code As String, ByVal audit As AuditMessage) As ActionResult(Of ProductionSchedule)

    ''' <summary>
    ''' Obtiene un grupo uvr por id
    ''' </summary>
    ''' <returns></returns>
    <OperationContract()>
    Function GetProductionScheduleById(ByVal id As Integer) As ActionResult(Of ProductionSchedule)

    ''' <summary>
    ''' Cambia de estado la campaña
    ''' </summary>
    ''' <returns></returns>
    <OperationContract()>
    Function CampaingDetailStatusChangeAsync(ByVal CampaingDetailId As List(Of Integer), ByVal Action As Byte, audit As AuditMessage) As Task(Of ActionResult)

    ''' <summary>
    ''' Obtiene los detalles de los paquetes por campaña
    ''' </summary>
    ''' <param name="campaignDetailId"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    <OperationContract()>
    Function GetItemsCampaigns(ByVal campaignDetailId As Integer, ByVal audit As AuditMessage) As ActionResult(Of SP_ListViewItemsCampaigns_Result)

    ''' <summary>
    ''' Guarda los usuarios autorizados
    ''' </summary>
    ''' <param name="AuthorizeUserslist"></param>
    ''' <param name="ProcessingDate"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    <OperationContract()>
    Function SaveAuthorizeUsersAsync(AuthorizeUserslist As List(Of CampaignDetailUsers), campaignDetail As CampaignDetail, audit As AuditMessage) As Task(Of ActionResult(Of CampaignDetailUsers))

    ''' <summary>
    ''' Validaciones de las campañas al momento de procesar
    ''' </summary>
    ''' <param name="campaignDetailIds"></param>
    ''' <returns></returns>
    <OperationContract>
    Function ValidateProductionSchedule(campaignDetailIds As List(Of Integer)) As ActionResult
    <OperationContract>
    Function GetCampaignDetailIdsByBatchCode(cmConfigurationId As Integer, productionLineId As Integer, batchCode As String) As List(Of Integer)
End Interface
