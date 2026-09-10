'***********************************************************************
' Assembly         : DistributedServices.MixinStation
' Author           : Carlos Mario Arias Rubiano
' Created          : 09/02/2021
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports System.ServiceModel
Imports Domain.Base.Entities
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base

<ServiceContract()>
Public Interface IMixingStationServiceRequestMixingStation

    ''' <summary>
    ''' Guarda o Actualiza un grupo
    ''' </summary>
    ''' <param name="audit">The audit.</param>
    ''' <returns></returns>
    <OperationContract()>
    Function SP_ProcessMixingStation(objParams As String, audit As AuditMessage) As ActionResult(Of SP_ProcessMixingStation_Result)

    ''' <summary>
    ''' Guarda la central de mezclas
    ''' </summary>
    <OperationContract()>
    Function SaveRequestMixingStation(data As Tuple(Of Integer, Integer)) As ActionResult(Of RequestMixingStation)

    ''' <summary>
    ''' Obtiene un grupo por codigo
    ''' </summary>
    ''' <param name="code">The code.</param>
    ''' <returns></returns>
    <OperationContract()>
    Function GetRequestMixingStation(ByVal code As String, ByVal audit As AuditMessage) As ActionResult(Of RequestMixingStation)

    ''' <summary>
    ''' Obtiene un grupo uvr por id
    ''' </summary>
    ''' <returns></returns>
    <OperationContract()>
    Function GetRequestMixingStationById(ByVal id As Integer) As ActionResult(Of RequestMixingStation)

    ''' <summary>
    ''' Procesa la Solicitud de Central de Mezclas
    ''' </summary>
    ''' <param name="requestIds"></param>
    ''' <returns></returns>
    <OperationContract()>
    Function ProcessRequestMixingStation(ByVal requestIds As List(Of Integer), ByVal audit As AuditMessage) As ActionResult

    <OperationContract()>
    Function AnnulateRequests(requestMixingStationDetailIds As List(Of Integer)) As ActionResult

    ''' <summary>
    ''' Vincula la readecaucion con el detalle de la solicitud de la campaña
    ''' </summary>
    ''' <param name="RequestMSDetailId"></param>
    ''' <param name="Readjustment"></param>
    ''' <returns></returns>
    <OperationContract()>
    Function AssignReadjustmentToRequestMSDetail(RequestMSDetailId As Integer, Readjustment As Readjustments) As ActionResult

End Interface
