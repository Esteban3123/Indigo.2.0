'***********************************************************************
' Assembly         : DistributedServices.Cost
' Author           : Diego Andrés Roldán Lozano
' Created          : 25-06-2025
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities
Imports System.ServiceModel

<ServiceContract()>
Public Interface ICostServiceCostIntermediateDistribution

    ''' <summary>
    ''' Guarda una distribucion intermedia
    ''' </summary>
    <OperationContract()>
    Function SaveIntermediateDistribution(IntermediateDistribution As CostIntermediateDistribution, idSequence As Int64, audit As AuditMessage) As ActionResult(Of CostIntermediateDistribution)

    ''' <summary>
    ''' Elimina una distribucion intermedia
    ''' </summary>
    <OperationContract()>
    Function DeleteIntermediateDistribution(IntermediateDistribution As CostIntermediateDistribution, audit As AuditMessage) As ActionResult

    ''' <summary>
    ''' Actualiza una distribucion intermedia
    ''' </summary>
    <OperationContract()>
    Function UpdateStateIntermediateDistribution(code As String, state As Boolean, audit As AuditMessage) As ActionResult(Of CostIntermediateDistribution)

    ''' <summary>
    ''' Obtiene una distribucion intermedia por codigo
    ''' </summary>
    ''' <param name="code">The code.</param>
    ''' <returns></returns>
    <OperationContract()>
    Function GetIntermediateDistribution(code As String, audit As AuditMessage) As ActionResult(Of CostIntermediateDistribution)

    ''' <summary>
    ''' Obtiene una distribucion intermedia por id
    ''' </summary>
    ''' <param name="id">The identifier.</param>
    ''' <returns></returns>
    <OperationContract()>
    Function GetIntermediateDistributionById(id As Integer) As CostIntermediateDistribution

    <OperationContract()>
    Function SP_CopyPasteCostIntermediateDistribution(data As List(Of List(Of String)), intermediateDistributionId As Integer, initialDistribution As Decimal) As ActionResult(Of List(Of CostIntermediateDistributionBaseDetail), List(Of Tuple(Of String, Integer)))

    ''' <summary>
    ''' Importa detalles al elemento de costo de distribución intermedia
    ''' </summary>
    ''' <param name="DistributionType"></param>
    ''' <param name="MeasurementUnit"></param>
    ''' <param name="ListIntermediateDistributionBaseDetail"></param>
    ''' <param name="Data"></param>
    ''' <returns></returns>
    <OperationContract()>
    Function ImportDetailsToCostIntermediateDistributionBase(DistributionType As Byte, MeasurementUnit As Byte, ListIntermediateDistributionBaseDetail As List(Of CostIntermediateDistributionBaseDetail), Data As List(Of List(Of String))) As ActionResult(Of List(Of CostIntermediateDistributionBaseDetail))

End Interface