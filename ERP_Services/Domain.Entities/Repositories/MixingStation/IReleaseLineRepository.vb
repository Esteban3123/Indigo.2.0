'************************************************************
' Assembly         : Domain.Authorization
' Author           : Diego A. Roldan
' Created          : 2021-11-08
'
' Copyright        : (c) . All rights reserved.
'************************************************************

#Region "Imports"
Imports Domain.Base
#End Region

Public Interface IReleaseLineRepository
    Inherits IRepository(Of ReleaseLine)

    Function GetReleaseLineByCampaignDetailId(campaignDetailId As Integer, Optional tracking As Boolean = True) As ReleaseLine
End Interface