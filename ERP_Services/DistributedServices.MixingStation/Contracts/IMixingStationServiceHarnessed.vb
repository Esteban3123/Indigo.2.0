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
Public Interface IMixingStationServiceHarnessed

    ''' <summary>
    ''' Guarda  o actualiza registro en la tabla Aprovechamiento
    ''' </summary>
    ''' <param name="ListHarnessed">The identifier.</param>
    ''' <param name="audit">The identifier.</param>
    <OperationContract()>
    Function SaveHarnessed(ByVal ListHarnessed As List(Of Harnessed), audit As AuditMessage) As ActionResult

    ''' <summary>
    ''' registra los aprovechamientos
    ''' </summary>
    ''' <returns></returns>
    <OperationContract()>
    Function RegisterHarnessed(ByVal ListQuantityRemaining As List(Of QuantityRemaining), ByVal CampaignDetailId As Integer, ByVal audit As AuditMessage) As ActionResult


End Interface
