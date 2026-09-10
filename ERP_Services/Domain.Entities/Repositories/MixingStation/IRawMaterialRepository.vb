'***********************************************************************
' Assembly         : Domain.MixingStation
' Author           : 
' Created          : 13-07-2021
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Base

Public Interface IRawMaterialRepository
    Inherits IRepository(Of CampaignDetailBasketDetail)

    ''' <summary>
    ''' Obtiene un paquete por id
    ''' </summary>
    ''' <param name="id">The identifier.</param>
    ''' <returns></returns>
    Function GetCampaignDetailBasketDetailById(id As String, Optional tracking As Boolean = True) As CampaignDetailBasketDetail
    ''' <summary>
    ''' Obtiene un paquete por id
    ''' </summary>
    ''' <returns></returns>
    Function GetCampaignDetailBasketDetailBycampaignDetailId(campaignDetailId As String) As List(Of CampaignDetailBasketDetail)
    ''' <summary>
    ''' lista los inventarios fisicos por un listado de codigo de productos
    ''' </summary>
    ''' <returns></returns>
    Function SP_ListPhysicalInventoryByCodeMaterialRaw(ATCNumber As String, warehouseId As Integer, StockId As Integer, MaquilaId As Integer?) As List(Of PhysicalInventory)
End Interface
