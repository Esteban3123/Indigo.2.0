'***********************************************************************
' Assembly         : Domain.MixingStation
' Author           : Carlos Mario Arias Rubiano
' Created          : 09/02/2021
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Base

Public Interface IRequestMixingStationDetailRepository
    Inherits IRepository(Of RequestMixingStationDetail)

    ''' <summary>
    ''' Obtiene un registro por id
    ''' </summary>
    ''' <param name="id">The identifier.</param>
    ''' <returns></returns>
    Function GetRequestMixingStationDetailById(id As String, Optional tracking As Boolean = True) As RequestMixingStationDetail

    ''' <summary>
    ''' Obtiene un registro por list of  CampaignDetailId
    ''' </summary>
    ''' <param name="CampaignDetailId">The identifier.</param>
    ''' <returns></returns>
    Function GetRequestMixingStationDetailByCampaignDetailId(CampaignDetailId As List(Of Integer)) As List(Of RequestMixingStationDetail)

    ''' <summary>
    ''' Obtiene un registro por list of Ids
    ''' </summary>
    ''' <param name="ListIds"></param>
    ''' <returns></returns>
    Function GetRequestMixingStationDetailByListIds(listIds As List(Of Integer)) As List(Of RequestMixingStationDetail)

    Function GetRequestMixingStationDetailByIdAsNoTracking(Id As Integer) As RequestMixingStationDetail





End Interface
