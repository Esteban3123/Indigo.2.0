'***********************************************************************
' Assembly         : DistributedServices.MixinStation
' Author           : Carlos Mario Arias Rubiano
' Created          : 09/12/2020
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports System.ServiceModel
Imports Domain.Base.Entities
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base

<ServiceContract()>
Public Interface IMixingStationServiceContractExternalClients

    ''' <summary>
    ''' Guarda o Actualiza un grupo
    ''' </summary>
    ''' <param name="audit">The audit.</param>
    ''' <returns></returns>
    <OperationContract()>
    Function SaveContractExternalClients(ByVal ContractExternalClients As ContractExternalClients, ByVal audit As AuditMessage, operatingUnitId As Integer, Optional ByVal idSequense As Int64 = 0) As ActionResult(Of ContractExternalClients)

    ''' <summary>
    ''' Elimina un grupo
    ''' </summary>
    ''' <param name="audit">The audit.</param>
    ''' <returns></returns>
    <OperationContract()>
    Function DeleteContractExternalClients(ByVal ContractExternalClients As ContractExternalClients, ByVal audit As AuditMessage, TransactionalContainer As String) As ActionResult

    ''' <summary>
    ''' Obtiene un grupo por codigo
    ''' </summary>
    ''' <param name="code">The code.</param>
    ''' <returns></returns>
    <OperationContract()>
    Function GetContractExternalClients(ByVal code As String, ByVal audit As AuditMessage) As ActionResult(Of ContractExternalClients)

    ''' <summary>
    ''' Obtiene un grupo uvr por id
    ''' </summary>
    ''' <returns></returns>
    <OperationContract()>
    Function GetContractExternalClientsById(ByVal id As Integer) As ActionResult(Of ContractExternalClients)

    ''' <summary>
    ''' Cambia el estado de la entidad
    ''' </summary>
    ''' <param name="code"></param>
    ''' <param name="state"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <OperationContract()>
    Function ChangeStateContractExternalClients(ByVal code As String, ByVal state As Boolean, ByVal audit As AuditMessage, operatingUnitId As Integer) As ActionResult(Of ContractExternalClients)

    ''' <summary>
    ''' Importa Excepciones Materia Prima
    ''' </summary>
    ''' <param name="data"></param>
    ''' <param name="parameters"></param>
    ''' <returns></returns>
    <OperationContract()>
    Function ImportExceptionsRawMaterial(data As List(Of List(Of String))) As ActionResult(Of List(Of ContractExternalClientsDetail))
End Interface
