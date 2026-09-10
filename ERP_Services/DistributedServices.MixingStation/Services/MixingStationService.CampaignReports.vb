'***********************************************************************
' Assembly         : DistributedService.MixingStation
' Author           : Duván Albeiro Mejia Cortes
' Created          : 2021-12-09
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"
Imports Application.MixingStation
Imports Domain.Base.Entities
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Microsoft.Practices.Unity
Imports System.ServiceModel
#End Region

Partial Class MixingStationService
    Implements IMixingStationServiceCampaignReports

    ''' <summary>
    ''' Obtener una Lista Movimientos por CampaignDetail y tipo reporte
    ''' </summary>
    ''' <param name="Data"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    Public Function GetListCampaignReportsByAction(Data As Tuple(Of Integer, Integer), audit As AuditMessage) As List(Of CampaignReports) Implements IMixingStationServiceCampaignReports.GetListCampaignReportsByAction
        Using service As ICampaignReportsAdminService = Container.Current.Resolve(Of ICampaignReportsAdminService)()
            Return service.GetListCampaignReportsByAction(Data, audit)
        End Using
    End Function

End Class
