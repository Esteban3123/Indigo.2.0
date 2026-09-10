Imports Domain.Base.Entities
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base

Public Interface ICostIntermediateDistributionAdminService
    Inherits IDisposable

    Function DeleteIntermediateDistribution(intermediateDistribution As CostIntermediateDistribution, audit As AuditMessage) As ActionResult
    Function GetIntermediateDistribution(code As String, audit As AuditMessage) As ActionResult(Of CostIntermediateDistribution)
    Function GetIntermediateDistributionById(id As Integer) As CostIntermediateDistribution
    Function SaveIntermediateDistribution(intermediateDistribution As CostIntermediateDistribution, audit As AuditMessage, Optional idSequence As Long = Nothing) As ActionResult(Of CostIntermediateDistribution)
    Function UpdateStateIntermediateDistribution(code As String, state As Boolean, audit As AuditMessage) As ActionResult(Of CostIntermediateDistribution)
    Function SP_CopyPasteCostIntermediateDistribution(data As List(Of List(Of String)), intermediateDistributionId As Integer, initialDistribution As Decimal) As ActionResult(Of List(Of CostIntermediateDistributionBaseDetail), List(Of Tuple(Of String, Integer)))
    Function ImportDetailsToCostIntermediateDistributionBase(DistributionType As Byte, MeasurementUnit As Byte, listIntermediateDistributionBaseDetail As List(Of CostIntermediateDistributionBaseDetail), Data As List(Of List(Of String))) As ActionResult(Of List(Of CostIntermediateDistributionBaseDetail))
End Interface
