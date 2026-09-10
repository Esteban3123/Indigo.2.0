'***********************************************************************
' Assembly         : Domain.MixingStation
' Author           : Carlos Mario Arias Rubiano
' Created          : 09/02/2021
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Base

Public Interface IRequestUnitDoseInventoryRepository
    Inherits IRepository(Of RequestUnitDoseInventory)

    ''' <summary>
    ''' Obtiene un registro por código
    ''' </summary>
    ''' <param name="code">The code.</param>
    ''' <returns></returns>
    Function GetRequestUnitDoseInventory(code As String, Optional tracking As Boolean = True) As RequestUnitDoseInventory

    ''' <summary>
    ''' Obtiene un registro por id
    ''' </summary>
    ''' <param name="id">The identifier.</param>
    ''' <returns></returns>
    Function GetRequestUnitDoseInventoryById(id As String, Optional tracking As Boolean = True) As RequestUnitDoseInventory

    ''' <summary>
    ''' Sp que guarda las solicitudes
    ''' </summary>
    ''' <param name="xml"></param>
    ''' <param name="userCode"></param>
    ''' <returns></returns>
    Function SP_SaveRequestUnitDoseInventory(xml As String, userCode As String) As SP_SaveRequestUnitDoseInventory_Result

End Interface
