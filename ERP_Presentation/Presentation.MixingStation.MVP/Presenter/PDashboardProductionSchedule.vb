'***********************************************************************
' Assembly         : Presentacion.MixingStation.MVP
' Author           : Carlos Mario Arias Rubiano
' Created          : 11/03/2021
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"
Imports Presentation.Base
Imports Infrastructure.CrossCutting.Base
Imports Presentation.CloudAgent.IndigoReference.MixingStation
Imports Presentation.Controls
Imports Presentation.Controls.MVP
Imports DevExpress.Xpo
Imports Infrastructure.Data.Xpo
Imports Infrastructure.Data.Xpo.MixingStationRepository
Imports System.Globalization
Imports Infrastructure.Data.Xpo.CrystalRepository
Imports DevExpress.Data.PLinq

#End Region

''' <summary>
''' Este presentador captura toda la logica aplicada en el frontal 
''' </summary>
Public Class PDashboardProductionSchedule

#Region "Variables"

    ''' <summary>
    ''' Se utiliza para instanciar la clase singleton
    ''' </summary>
    Dim _sessionValues As SessionValues

#End Region

#Region "Builder"

    Public Sub New()
        Me._sessionValues = SessionValues.Instance
    End Sub

#End Region

#Region "Methods"

    ''' <summary>
    ''' Datasource del search principal
    ''' </summary>
    ''' <returns></returns>
    Public Function InitializeCMProductionLine() As XPInstantFeedbackSource
        Dim filter As String = "UserCode = '" & _sessionValues.UserIndigo & "'"
        Return XpoServiceEx.Instance(_sessionValues.TransactionalContainer).MixingStationService.ListXPInstantFeedbackSource(Of ViewListCMProductionLineXpo)(filter, sortField:="CMConfigurationCodeName ASC; ProductionLineCodeName")
    End Function

    ''' <summary>
    ''' Carga la información de las solicitudes
    ''' </summary>
    ''' <returns></returns>
    Public Function ListViewListDashboardProductionSchedule(cmConfigurationId As Integer, productionLineId As Integer) As List(Of ViewListDashboardProductionScheduleXpo)
        Dim filter As String = "CMConfigurationId = " & cmConfigurationId & " and ProductionLineId = " & productionLineId
        Return XpoServiceEx.Instance(_sessionValues.TransactionalContainer).MixingStationService.GetCollection(Of ViewListDashboardProductionScheduleXpo)(Nothing, filter).ToList()
    End Function

    ''' <summary>
    ''' Carga la información de las solicitudes
    ''' </summary>
    ''' <returns></returns>
    Public Function ListViewListDashboardProductionScheduleXPInstantFeedbackSource(cmConfigurationId As Integer, productionLineId As Integer) As XPInstantFeedbackSource
        Return XpoServiceEx.Instance(_sessionValues.TransactionalContainer).MixingStationService.ListViewListDashboardProductionSchedule(cmConfigurationId, productionLineId)
    End Function

    ''' <summary>
    ''' Carga la información de la pestaña de ordenes de producción
    ''' </summary>
    ''' <returns></returns>
    Public Function ListViewProductionOrder(cmConfigurationId As Integer, productionLineId As Integer) As XPInstantFeedbackSource
        Return XpoServiceEx.Instance(_sessionValues.TransactionalContainer).MixingStationService.ListViewProductionOrder(cmConfigurationId, productionLineId)
    End Function

    ''' <summary>
    ''' Carga la información de la pestaña de Historico campañas
    ''' </summary>
    ''' <returns></returns>
    Public Function ListViewHistoricCampaign(cmConfigurationId As Integer, productionLineId As Integer) As XPInstantFeedbackSource
        Return XpoServiceEx.Instance(_sessionValues.TransactionalContainer).MixingStationService.ListViewHistoricCampaign(cmConfigurationId, productionLineId)
    End Function

    ''' <summary>
    ''' Carga los usuarios autorizados 
    ''' </summary>
    ''' <returns></returns>
    Public Function ListViewListAuthorizeUsers(CampaignDetailId As Integer) As List(Of ViewListAuthorizeUsersXpo)
        Dim filter As String = "CampaignDetailId = " & CampaignDetailId
        Return XpoServiceEx.Instance(_sessionValues.TransactionalContainer).MixingStationService.GetCollection(Of ViewListAuthorizeUsersXpo)(Nothing, filter).ToList()
    End Function

    Public Function ListUsersCampaing(Id As Integer) As XPInstantFeedbackSource
        Return XpoServiceEx.Instance(_sessionValues.TransactionalContainer).MixingStationService.ListUsersCampaing(Id)
    End Function

    Public Function ListRequestDetailStatus(Id As Integer) As XPInstantFeedbackSource
        Return XpoServiceEx.Instance(_sessionValues.TransactionalContainer).MixingStationService.ListViewRequestDetailStatus(Id)
    End Function

    Public Function GetTechnicalDirectorByCampaign(CampaignDetailId As Integer) As ViewTechnicalDirectorByCampaignXpo
        Dim filter As String = "CampaignDetailId = " & CampaignDetailId
        Return XpoServiceEx.Instance(_sessionValues.TransactionalContainer).MixingStationService.GetCollection(Of ViewTechnicalDirectorByCampaignXpo)(Nothing, filter).FirstOrDefault()
    End Function


    Public Function GetCampaignDetailUsersByIdCampaign(ByVal campaignDetailId As Integer) As List(Of CampaignDetailUsersXpo)
        Return XpoServiceEx.Instance(_sessionValues.TransactionalContainer).MixingStationService.GetCollection(Of CampaignDetailUsersXpo)(Nothing, $"CampaignDetailId={campaignDetailId}").ToList()
    End Function


    Public Function GetUserById(ByVal Id As Integer) As Infrastructure.Data.Xpo.SecurityRepository.UserXpo
        Return XpoServiceEx.Instance(_sessionValues.SecurityContainer).SecurityService.GetXPOObject(Of SecurityRepository.UserXpo)($"Id = {Id}")
    End Function

#End Region

End Class
