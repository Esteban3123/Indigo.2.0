'***********************************************************************
' Assembly         : Domain.MixingStation
' Author           : Giovanny Plazas L
' Created          : 16/09/2021
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Base

Public Interface IQuantityRemainingRepository
    Inherits IRepository(Of QuantityRemaining)
    ''' <summary>
    ''' Obtiene Lista de sobrantes filtrados por campaignId, productId y batchserialId
    ''' </summary>
    ''' <param name="CampaignDetailId"></param>
    ''' <param name="ProductId"></param>
    ''' <param name="BatchSerialId"></param>
    ''' <returns></returns>
    Function GetListQuantityRemaining(CampaignDetailId As Integer, ProductId As Integer, BatchSerialId As Integer?) As List(Of QuantityRemaining)

    ''' <summary>
    ''' consulta todos los registros de la tabla
    ''' </summary>
    ''' <returns></returns>
    Function GetAllQuantityRemaining() As List(Of QuantityRemaining)

    ''' <summary>
    ''' funcion para consultar la tabla de remanentes por almacen de remanente el cual se parametriza por central de mezclas
    ''' </summary>
    ''' <param name="_cMConfigurationId"></param>
    ''' <returns></returns>
    Function GetQuantityRemainingByMixingStation(_cMConfigurationId As Integer) As List(Of QuantityRemaining)

End Interface
