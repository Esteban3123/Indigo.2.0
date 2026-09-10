'***********************************************************************
' Assembly         : Domain.MixingStation
' Author           : Andres Felipe Aros
' Created          : 2021-09-02
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Base

Public Interface ICampaignDetailValidationRepository
    Inherits IRepository(Of CampaignDetailValidation)

    Function GetCampaignDetailValidationByCampaignDetailId(campaignDetailId As Integer, Optional tracking As Boolean = True) As List(Of CampaignDetailValidation)

    Function GetCampaignDetailValidationForDevolution(campaignDetailId As Integer) As List(Of CampaignDetailValidation)

End Interface
