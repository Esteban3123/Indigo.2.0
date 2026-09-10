'***********************************************************************
' Assembly         : Domain.MixingStation
' Author           : Duván Albeiro Mejia Cortes
' Created          : 2021-12-08
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Base

Public Interface ICampaignReportsRepository
    Inherits IRepository(Of CampaignReports), Inject

    ''' <summary>
    ''' Obtiene una lista, filtando por CampaignDetail
    ''' </summary>
    ''' <param name="Data"></param>
    ''' <returns></returns>
    Function GetListCampaignReports(Data As Tuple(Of Integer, Integer)) As List(Of CampaignReports)

End Interface
