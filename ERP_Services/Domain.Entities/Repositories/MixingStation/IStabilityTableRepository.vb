'***********************************************************************
' Assembly         : Domain.MixingStation
' Author           : Carlos Mario Arias Rubiano
' Created          : 06/11/2020
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Base

Public Interface IStabilityTableRepository
    Inherits IRepository(Of StabilityTable)

    ''' <summary>
    ''' Obtiene un registro por código
    ''' </summary>
    ''' <param name="code">The code.</param>
    ''' <returns></returns>
    Function GetStabilityTable(code As String, Optional tracking As Boolean = True) As StabilityTable

    ''' <summary>
    ''' Obtiene un registro por id
    ''' </summary>
    ''' <param name="id">The identifier.</param>
    ''' <returns></returns>
    Function GetStabilityTableById(id As String, Optional tracking As Boolean = True) As StabilityTable

    ''' <summary>
    ''' Sp que guarda la tabla de estabilidad
    ''' </summary>
    ''' <param name="xml"></param>
    ''' <param name="userCode"></param>
    ''' <returns></returns>
    Function SP_SaveStabilityTable(xml As String, userCode As String) As SP_SaveStabilityTable_Result

End Interface
