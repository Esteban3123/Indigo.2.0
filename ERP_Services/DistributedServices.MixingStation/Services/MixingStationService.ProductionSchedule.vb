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
    Implements IMixingStationServiceProductionSchedule

    Public Function GetProductionSchedule(code As String, audit As AuditMessage) As ActionResult(Of ProductionSchedule) Implements IMixingStationServiceProductionSchedule.GetProductionSchedule
        Using service As IProductionScheduleAdminService = Container.Current.Resolve(Of IProductionScheduleAdminService)()
            Return service.GetProductionSchedule(code, audit)
        End Using
    End Function

    Public Function GetProductionScheduleById(id As Integer) As ActionResult(Of ProductionSchedule) Implements IMixingStationServiceProductionSchedule.GetProductionScheduleById
        Using service As IProductionScheduleAdminService = Container.Current.Resolve(Of IProductionScheduleAdminService)()
            Return service.GetProductionScheduleById(id)
        End Using
    End Function

    Public Async Function SaveProductionScheduleAsync(objParams As String, authorizeUserslist As List(Of CampaignDetailUsers), campaignDetail As CampaignDetail, audit As AuditMessage) As Task(Of ActionResult(Of SP_SaveProductionSchedule_Result)) Implements IMixingStationServiceProductionSchedule.SaveProductionScheduleAsync
        Using service As IProductionScheduleAdminService = Container.Current.Resolve(Of IProductionScheduleAdminService)()
            Return Await service.SaveProductionScheduleAsync(objParams, authorizeUserslist, campaignDetail, audit)
        End Using
    End Function

    Public Async Function CampaingDetailStatusChangeAsync(CampaingDetailId As List(Of Integer), Action As Byte, audit As AuditMessage) As Task(Of ActionResult) Implements IMixingStationServiceProductionSchedule.CampaingDetailStatusChangeAsync
        Using service As IProductionScheduleAdminService = Container.Current.Resolve(Of IProductionScheduleAdminService)()
            Return Await service.CampaingDetailStatusChangeAsync(CampaingDetailId, Action, audit)
        End Using
    End Function
    ''' <summary>
    ''' Obtiene los detalles de los paquetes por campaña
    ''' </summary>
    ''' <param name="campaignDetailId"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    Public Function GetItemsCampaigns(campaignDetailId As Integer, audit As AuditMessage) As ActionResult(Of SP_ListViewItemsCampaigns_Result) Implements IMixingStationServiceProductionSchedule.GetItemsCampaigns
        Using service As IProductionScheduleAdminService = Container.Current.Resolve(Of IProductionScheduleAdminService)()
            Return service.GetItemsCampaigns(campaignDetailId, audit)
        End Using
    End Function

    ''' <summary>
    ''' Guarda los usuarios autorizados
    ''' </summary>
    ''' <param name="AuthorizeUserslist"></param>
    ''' <param name="campaignDetail"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    Public Async Function SaveAuthorizeUsersAsync(AuthorizeUserslist As List(Of CampaignDetailUsers), campaignDetail As CampaignDetail, audit As AuditMessage) As Task(Of ActionResult(Of CampaignDetailUsers)) Implements IMixingStationServiceProductionSchedule.SaveAuthorizeUsersAsync
        Using service As IProductionScheduleAdminService = Container.Current.Resolve(Of IProductionScheduleAdminService)()
            Return Await service.SaveAuthorizeUsersAsync(AuthorizeUserslist, campaignDetail, audit)
        End Using
    End Function

    ''' <summary>
    ''' Validaciones de las campañas al momento de procesar
    ''' </summary>
    ''' <param name="campaignDetailIds"></param>
    ''' <returns></returns>
    Public Function ValidateProductionSchedule(campaignDetailIds As List(Of Integer)) As ActionResult Implements IMixingStationServiceProductionSchedule.ValidateProductionSchedule
        Using service As IProductionScheduleAdminService = Container.Current.Resolve(Of IProductionScheduleAdminService)()
            Return service.ValidateProductionSchedule(campaignDetailIds)
        End Using
    End Function

    Public Function GetCampaignDetailIdsByBatchCode(cmConfigurationId As Integer, productionLineId As Integer, batchCode As String) As List(Of Integer) Implements IMixingStationServiceProductionSchedule.GetCampaignDetailIdsByBatchCode
        Using service As IProductionScheduleAdminService = Container.Current.Resolve(Of IProductionScheduleAdminService)()
            Return service.GetCampaignDetailIdsByBatchCode(cmConfigurationId, productionLineId, batchCode)
        End Using
    End Function
End Class
