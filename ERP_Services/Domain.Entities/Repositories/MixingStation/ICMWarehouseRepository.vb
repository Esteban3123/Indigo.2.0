'***********************************************************************
' Assembly         : Domain.MixingStation
' Author           : Diego A. Roldan
' Created          : 2021-09-10
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Base

Public Interface ICMWarehouseRepository
    Inherits IRepository(Of CMWarehouse)

    ''' <summary>
    ''' Obtiene el almacén
    ''' </summary>
    ''' <param name="mixingStationId"></param>
    ''' <param name="wareHouseType"></param>
    ''' <returns></returns>
    Function GetWareHouseByType(mixingStationId As Integer, wareHouseType As Byte, Optional tracking As Boolean = True) As Warehouse

    ''' <summary>
    ''' consulta por tipo y campaña
    ''' </summary>
    ''' <param name="campaignDetailId"></param>
    ''' <param name="wareHouseType"></param>
    ''' <param name="tracking"></param>
    ''' <returns></returns>
    Function GetByTypeAndCampaignDetailId(campaignDetailId As Integer, wareHouseType As Byte, Optional tracking As Boolean = True) As CMWarehouse
End Interface
