'***********************************************************************
' Assembly         : Application.MixingStation
' Author           : Yoe Andres Cardenas
' Created          : 26-04-2019
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities

Public Interface IBlockRecordMixingStationAdminService
    Inherits IDisposable

    ''' <summary>
    ''' Obtiene el bloque del registro de central de mezclas por IdForm y registro del identificador
    ''' </summary>
    ''' <param name="IdForm">The identifier form.</param>
    ''' <param name="IdRecord">The identifier record.</param>
    ''' <returns></returns>
    Function GetBlockRecordMixingStationByIdformAndIdRecord(ByVal IdForm As String, ByVal IdRecord As String) As BlockRecordMixingStation

    ''' <summary>
    ''' Almacena o Actualiza registro bloqueado
    ''' </summary>
    ''' <returns>ActionResult</returns>
    Function SaveBlockRecordMixingStation(ByVal blockRecordMixingStation As BlockRecordMixingStation) As ActionResult(Of BlockRecordMixingStation)

    ''' <summary>
    ''' Elimina un registro bloqueado
    ''' </summary>
    ''' <returns>ActionResult</returns>
    Function DeleteBlockRecordMixingStation(ByVal blockRecordMixingStation As BlockRecordMixingStation) As ActionResult

End Interface
