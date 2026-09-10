'***********************************************************************
' Assembly         : DistributedServices.MixinStation
' Author           : Duván Albeiro Mejia Cortes
' Created          : 2021-12-09
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports System.ServiceModel
Imports Domain.Base.Entities
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base

<ServiceContract()>
Public Interface IMixingStationServiceCampaignReports

    ''' <summary>
    ''' Obtener una Lista Movimientos por CampaignDetail y tipo reporte
    ''' </summary>
    ''' <param name="Data"></param>
    ''' <param name="Audit"></param>
    ''' <returns></returns>
    <OperationContract()>
    Function GetListCampaignReportsByAction(Data As Tuple(Of Integer, Integer), Audit As AuditMessage) As List(Of CampaignReports)
End Interface
