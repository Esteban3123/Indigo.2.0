'***********************************************************************
' Assembly         : DistributedServices.MixingStation
' Author           : Yoe Andres Cardenas
' Created          : 26-04-2019
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities
Imports System.ServiceModel

<ServiceContract()>
Public Interface IMixingStationBlockRecordMixingStation

    ''' <summary>
    ''' Obtiene el bloque del registro de central de mezclas por IdForm y registro del identificador
    ''' </summary>
    ''' <param name="IdForm">The identifier form.</param>
    ''' <param name="IdRecord">The identifier record.</param>
    ''' <returns></returns>
    <OperationContract()>
    Function GetBlockRecordMixingStationByIdformAndIdRecord(ByVal IdForm As String, ByVal IdRecord As String) As BlockRecordMixingStation

    ''' <summary>
    ''' Almacena o Actualiza registro bloqueado
    ''' </summary>
    ''' <param name="blockRecordMixingStation">Objeto Auditoria</param>
    ''' <returns>ActionResult</returns>
    <OperationContract()>
    Function SaveBlockRecordMixingStation(ByVal blockRecordMixingStation As BlockRecordMixingStation) As ActionResult(Of BlockRecordMixingStation)

    ''' <summary>
    ''' Elimina un registro bloqueado
    ''' </summary>
    ''' <param name="blockRecordMixingStation">Objeto Auditoría</param>
    ''' <returns>ActionResult</returns>
    <OperationContract()>
    Function DeleteBlockRecordMixingStation(ByVal blockRecordMixingStation As BlockRecordMixingStation) As ActionResult

End Interface
