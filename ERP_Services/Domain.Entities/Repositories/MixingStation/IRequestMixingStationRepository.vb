'***********************************************************************
' Assembly         : Domain.MixingStation
' Author           : Carlos Mario Arias Rubiano
' Created          : 09/02/2021
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Base

Public Interface IRequestMixingStationRepository
    Inherits IRepository(Of RequestMixingStation)

    ''' <summary>
    ''' Obtiene un registro por código
    ''' </summary>
    ''' <param name="code">The code.</param>
    ''' <returns></returns>
    Function GetRequestMixingStation(code As String, Optional tracking As Boolean = True) As RequestMixingStation

    ''' <summary>
    ''' Obtiene un registro por id
    ''' </summary>
    ''' <param name="id">The identifier.</param>
    ''' <returns></returns>
    Function GetRequestMixingStationById(id As String, Optional tracking As Boolean = True) As RequestMixingStation

    ''' <summary>
    ''' Sp que guarda las solicitudes
    ''' </summary>
    ''' <param name="xml"></param>
    ''' <param name="userCode"></param>
    ''' <returns></returns>
    Function SP_ProcessMixingStation(xml As String, userCode As String) As SP_ProcessMixingStation_Result

End Interface
