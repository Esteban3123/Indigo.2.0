'***********************************************************************
' Assembly         : Domain.MixingStation
' Author           : Carlos Mario Arias Rubiano
' Created          : 15/03/2021
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Base

Public Interface IProductionScheduleRepository
    Inherits IRepository(Of ProductionSchedule)

    ''' <summary>
    ''' Obtiene un registro por código
    ''' </summary>
    ''' <param name="code">The code.</param>
    ''' <returns></returns>
    Function GetProductionSchedule(code As String, Optional tracking As Boolean = True) As ProductionSchedule

    ''' <summary>
    ''' Obtiene un registro por id
    ''' </summary>
    ''' <param name="id">The identifier.</param>
    ''' <returns></returns>
    Function GetProductionScheduleById(id As String, Optional tracking As Boolean = True) As ProductionSchedule

    ''' <summary>
    ''' Sp que genera el cronograma
    ''' </summary>
    ''' <param name="xml"></param>
    ''' <param name="userCode"></param>
    ''' <returns></returns>
    Function SP_SaveProductionSchedule(xml As String, userCode As String) As SP_SaveProductionSchedule_Result

    ''' <summary>
    ''' Sp que Obtiene los detalles de los paquetes con sus diferentes estados
    ''' </summary>
    ''' <param name="campaignDetailId"></param>
    ''' <returns></returns>
    Function SP_ListViewItemsCampaigns(campaignDetailId As Integer) As List(Of SP_ListViewItemsCampaigns_Result)

End Interface
