'***********************************************************************
' Assembly         : Domain.MixingStation
' Author           : Duván Albeiro Mejia Cortes
' Created          : 23/07/2021
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Base

Public Interface ICampaignDetailRepository
    Inherits IRepository(Of CampaignDetail)

    ''' <summary>
    ''' Obtener una CampañaDetail 
    ''' </summary>
    ''' <param name="id">Campaign Detail.</param>
    ''' <returns></returns>
    Function GetCampaignDetailById(id As Integer) As CampaignDetail

    ''' <summary>
    ''' Obtiene una lista de detalles de la campaña por Ids
    ''' </summary>
    ''' <param name="ids">The identifier.</param>
    ''' <returns></returns>
    Function GetCampaignsDetailByIds(ids As List(Of Integer), Optional tracking As Boolean = True) As List(Of CampaignDetail)
End Interface