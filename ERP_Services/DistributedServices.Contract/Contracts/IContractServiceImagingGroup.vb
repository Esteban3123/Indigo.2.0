'***********************************************************************
' Assembly         : DistributedServices.Contract
' Author           : Hector Rodriguez
' Created          : 14/03/2019
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports System.ServiceModel
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities
Imports Domain.Crystal.Entities

<ServiceContract()> _
Public Interface IContractServiceImagingGroup
    ''' <summary>
    ''' Obtiene un determinado grupo de imagenologia por id
    ''' </summary>
    ''' <returns></returns>
    <OperationContract()>
    Function GetImagingGroupById(id As Integer, audit As AuditMessage) As ActionResult(Of RISGRIMAGE)
    
    ''' <summary>
    ''' Obtiene grupos de imagenologia activos
    ''' </summary>
    ''' <returns></returns>
    <OperationContract()>
    Function GetImagingGroupActive() As ActionResult(Of List(Of RISGRIMAGE))
End Interface
