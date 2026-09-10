'***********************************************************************
' Assembly         : Domain.MixingStation
' Author           : Carlos Mario Arias Rubiano
' Created          : 15/03/2021
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Base

Public Interface ICampaignRepository
    Inherits IRepository(Of Campaign)

    ''' <summary>
    ''' Obtiene un registro por id
    ''' </summary>
    ''' <param name="id">The identifier.</param>
    ''' <returns></returns>
    Function GetCampaignById(id As String, Optional tracking As Boolean = True) As Campaign

    ''' <summary>
    ''' Sp que genera la campaña
    ''' </summary>
    ''' <param name="xml"></param>
    ''' <param name="userCode"></param>
    ''' <returns></returns>
    Function SP_SaveCampaign(xml As String, userCode As String) As SP_SaveCampaign_Result

    ''' <summary>
    ''' Sp que anula los pacientes
    ''' </summary>
    ''' <param name="xml"></param>
    ''' <param name="userCode"></param>
    ''' <returns></returns>
    Function SP_ProcessMixingStation(xml As String, userCode As String) As SP_ProcessMixingStation_Result

End Interface
