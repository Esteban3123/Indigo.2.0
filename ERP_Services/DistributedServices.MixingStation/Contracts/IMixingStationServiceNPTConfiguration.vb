'***********************************************************************
' Assembly         : DistributedServices.MixinStation
' Author           : Judy Andrea Díaz Reyes
' Created          : 21/05/2019
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports System.ServiceModel
Imports Domain.Base.Entities
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base

<ServiceContract()>
Public Interface IMixingStationServiceNPTConfiguration
    ''' <summary>
    ''' Lista todo
    ''' </summary>
    ''' <returns>Lista de tipos de dosis unitaria</returns>
    <OperationContract()>
    Function ListAllNPTConfiguration(audit As AuditMessage) As List(Of NPTConfiguration)

    ''' <summary>
    ''' Guarda 
    ''' </summary>
    ''' <param name="NPTConfiguration">The identifier.</param>
    ''' <param name="audit">The identifier.</param>
    <OperationContract()>
    Function SaveNPTConfiguration(ByVal NPTConfiguration As NPTConfiguration, audit As AuditMessage) As ActionResult(Of List(Of NPTConfiguration))


    ''' <summary>
    ''' elimina 
    ''' </summary>
    ''' <param name="ListNPTConfiguration"></param>
    ''' <param name="audit">The identifier.</param>
    <OperationContract()>
    Function DeleteNPTConfiguration(ByVal ListNPTConfiguration As List(Of NPTConfiguration), audit As AuditMessage) As ActionResult(Of List(Of NPTConfiguration))

End Interface
