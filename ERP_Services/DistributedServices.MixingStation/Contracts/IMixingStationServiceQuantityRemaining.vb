'***********************************************************************
' Assembly         : DistributedServices.MixinStation
' Author           : Yoe Andres Cardenas
' Created          : 06/06/2019
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports System.ServiceModel
Imports Domain.Base.Entities
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base

<ServiceContract()>
Public Interface IMixingStationServiceQuantityRemaining

    ''' <summary>
    ''' Guarda  o actualiza registro en la tabla sobrantes
    ''' </summary>
    ''' <param name="ListQuantityRemaining">The identifier.</param>
    ''' <param name="audit">The identifier.</param>
    <OperationContract()>
    Function SaveQuantityRemaining(ByVal ListQuantityRemaining As List(Of QuantityRemaining), audit As AuditMessage) As ActionResult

    ''' <summary>
    ''' consulta todos los registros de la tabla
    ''' </summary>
    ''' <returns></returns>
    <OperationContract()>
    Function GetAllQuantityRemaining() As ActionResult(Of List(Of QuantityRemaining))

    ''' <summary>
    ''' funcion para consultar la tabla de remanentes por almacen de remanente el cual se parametriza por central de mezclas
    ''' </summary>
    ''' <param name="_cMConfigurationId"></param>
    ''' <returns></returns>
    <OperationContract()>
    Function GetQuantityRemainingByMixingStation(_cMConfigurationId As Integer) As ActionResult(Of List(Of QuantityRemaining))


End Interface
