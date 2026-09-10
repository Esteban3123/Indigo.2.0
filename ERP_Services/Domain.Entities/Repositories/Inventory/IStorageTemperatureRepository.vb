'***********************************************************************
' Assembly         : Domain.Inventory
' Author           : Andres Alarcon
' Created          : 29-11-2024
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Base

Public Interface IStorageTemperatureRepository
    Inherits IRepository(Of StorageTemperature)

    ''' <summary>
    ''' Obtiene un rango de temperatura por codigo
    ''' </summary>
    ''' <param name="code">The code.</param>
    ''' <returns></returns>
    Function GetStorageTemperature(ByVal code As String) As StorageTemperature

    ''' <summary>
    ''' Obtiene un rando de temperatura por id
    ''' </summary>
    ''' <param name="id">The identifier.</param>
    ''' <returns></returns>
    Function GetStorageTemperatureById(id As Integer, Optional tracking As Boolean = True) As StorageTemperature

End Interface