'***********************************************************************
' Assembly         : Domain.MixingStation
' Author           : Diego A. Roldan
' Created          : 2021-12-02
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Base

Public Interface IContractExternalClientsDetailRepository
    Inherits IRepository(Of ContractExternalClientsDetail)

    ''' <summary>
    ''' Obtiene las excepciones para una campaña determinada
    ''' </summary>
    ''' <param name="campaignDetailId"></param>
    ''' <returns></returns>
    Function GetRawMaterialExceptionsByCampaignDetailId(campaignDetailId As Integer) As List(Of ContractExternalClientsDetail)
    Function GetRawMaterialExceptionByCampaignDetailIdAndItemId(campaignDetailId As Integer, atcId As Integer?, supplyId As Integer?, productId As Integer?) As ContractExternalClientsDetail
    Function GetContractExternalClientByCampaignDetailId(campaignDetailId As Integer) As ContractExternalClients
End Interface

