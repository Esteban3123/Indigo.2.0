'***********************************************************************
' Assembly         : Domain.MixingStation
' Author           : Diego A. Roldán
' Created          : 2021-08-23
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Base

Public Interface ICampaignDetailPickingRepository
    Inherits IRepository(Of CampaignDetailPicking)

    Function GetCampaignDetailPickingByCampaignDetailId(campaignDetailId As Integer, Optional tracking As Boolean = True) As List(Of CampaignDetailPicking)

End Interface
