'***********************************************************************
' Assembly         : Domain.MixingStation
' Author           : Carlos Mario Arias Rubiano
' Created          : 11/02/2021
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Base

Public Interface IRequestUnitDoseExternalCareCenterRepository
    Inherits IRepository(Of RequestUnitDoseExternalCareCenter)

    ''' <summary>
    ''' Obtiene un registro por código
    ''' </summary>
    ''' <param name="code">The code.</param>
    ''' <returns></returns>
    Function GetRequestUnitDoseExternalCareCenter(code As String, Optional tracking As Boolean = True) As RequestUnitDoseExternalCareCenter

    ''' <summary>
    ''' Obtiene un registro por id
    ''' </summary>
    ''' <param name="id">The identifier.</param>
    ''' <returns></returns>
    Function GetRequestUnitDoseExternalCareCenterById(id As String, Optional tracking As Boolean = True) As RequestUnitDoseExternalCareCenter

    ''' <summary>
    ''' Sp que guarda las solicitudes
    ''' </summary>
    ''' <param name="xml"></param>
    ''' <param name="userCode"></param>
    ''' <returns></returns>
    Function SP_SaveRequestUnitDoseExternalCareCenter(xml As String, userCode As String) As SP_SaveRequestUnitDoseExternalCareCenter_Result

End Interface
