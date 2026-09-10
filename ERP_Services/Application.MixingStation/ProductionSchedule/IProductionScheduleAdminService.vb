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


Public Interface IProductionScheduleAdminService
    Inherits IDisposable

    ''' <summary>
    ''' Procesa la orden de produccion
    ''' </summary>
    ''' <param name="objParams"></param>
    ''' <param name="authorizeUserslist"></param>
    ''' <param name="campaignDetail"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    Function SaveProductionScheduleAsync(objParams As String, authorizeUserslist As List(Of CampaignDetailUsers), campaignDetail As CampaignDetail, audit As AuditMessage) As Task(Of ActionResult(Of SP_SaveProductionSchedule_Result))

    ''' <summary>
    ''' Obtiene un grupo por codigo
    ''' </summary>
    ''' <param name="code">The code.</param>
    ''' <returns></returns>
    Function GetProductionSchedule(ByVal code As String, ByVal audit As AuditMessage) As ActionResult(Of ProductionSchedule)

    ''' <summary>
    ''' Obtiene un grupo uvr por id
    ''' </summary>
    ''' <returns></returns>
    Function GetProductionScheduleById(ByVal id As Integer) As ActionResult(Of ProductionSchedule)

    ''' <summary>
    ''' Ejecuta la accion de cambio de estado  
    ''' </summary>
    ''' <returns></returns>
    Function CampaingDetailStatusChangeAsync(ByVal CampaingDetailId As List(Of Integer), ByVal Action As Byte, audit As AuditMessage) As Task(Of ActionResult)

    ''' <summary>
    ''' Obtiene los detalles de los paquetes por campaña
    ''' </summary>
    ''' <param name="campaignDetailId"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    Function GetItemsCampaigns(ByVal campaignDetailId As Integer, ByVal audit As AuditMessage) As ActionResult(Of SP_ListViewItemsCampaigns_Result)


    ''' <summary>
    ''' Guarda los usuarios autorizados en campaña
    ''' </summary>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    Function SaveAuthorizeUsersAsync(AuthorizeUserslist As List(Of CampaignDetailUsers), campaignDetail As CampaignDetail, audit As AuditMessage) As Task(Of ActionResult(Of CampaignDetailUsers))

    ''' <summary>
    ''' Validaciones de las campañas al momento de procesar
    ''' </summary>
    ''' <param name="campaignDetailIds"></param>
    ''' <returns></returns>
    Function ValidateProductionSchedule(campaignDetailIds As List(Of Integer)) As ActionResult
    Function GetCampaignDetailIdsByBatchCode(cmConfigurationId As Integer, productionLineId As Integer, batchCode As String) As List(Of Integer)
End Interface
