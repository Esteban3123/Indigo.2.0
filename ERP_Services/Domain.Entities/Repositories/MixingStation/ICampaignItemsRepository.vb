'***********************************************************************
' Assembly         : Domain.MixingStation
' Author           : Duván Mejía Cortes
' Created          : 22/07/2021
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Base

Public Interface ICampaignItemsRepository
    Inherits IRepository(Of CampaignDetailItems)

    ''' <summary>
    ''' Listar productos en Estado Picking 
    ''' </summary>
    ''' <param name="campaignDetailId"></param>
    ''' <returns></returns>
    Function GetProductsItems(campaignDetailId As Integer) As List(Of CampaignDetailItems)

    ''' <summary>
    ''' Listar productos en Estado Validacion 
    ''' </summary>
    ''' <param name="campaignDetailId"></param>
    ''' <returns></returns>
    Function GetProductsItemsValidation(campaignDetailId As Integer) As List(Of CampaignDetailItems)

End Interface
