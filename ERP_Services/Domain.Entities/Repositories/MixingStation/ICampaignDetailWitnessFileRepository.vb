'***********************************************************************
' Assembly         : Domain.MixingStation
' Author           : Diego A. Roldán
' Created          : 2022-10-26
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Base
Imports Domain.Entities

Public Interface ICampaignDetailWitnessFileRepository
    Inherits IRepository(Of CampaignDetailWitnessFile)

End Interface