'***********************************************************************
' Assembly         : DistributedServices.Cost
' Author           : Diego Andrés Roldán lozano
' Created          : 25-06-2025
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Application.Cost
Imports Domain.Base.Entities
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Microsoft.Practices.Unity

Partial Public Class CostService
    Implements ICostServiceCostIntermediateDistribution

    Public Function SaveIntermediateDistribution(intermediateDistribution As CostIntermediateDistribution, idSequence As Long, audit As AuditMessage) As ActionResult(Of CostIntermediateDistribution) Implements ICostServiceCostIntermediateDistribution.SaveIntermediateDistribution
        Using service As ICostIntermediateDistributionAdminService = Container.Current.Resolve(Of ICostIntermediateDistributionAdminService)()
            Return service.SaveIntermediateDistribution(intermediateDistribution, audit, idSequence)
        End Using
    End Function

    Public Function DeleteIntermediateDistribution(intermediateDistribution As CostIntermediateDistribution, audit As AuditMessage) As ActionResult Implements ICostServiceCostIntermediateDistribution.DeleteIntermediateDistribution
        Using service As ICostIntermediateDistributionAdminService = Container.Current.Resolve(Of ICostIntermediateDistributionAdminService)()
            Return service.DeleteIntermediateDistribution(intermediateDistribution, audit)
        End Using
    End Function

    Public Function UpdateStateIntermediateDistribution(code As String, state As Boolean, audit As AuditMessage) As ActionResult(Of CostIntermediateDistribution) Implements ICostServiceCostIntermediateDistribution.UpdateStateIntermediateDistribution
        Using service As ICostIntermediateDistributionAdminService = Container.Current.Resolve(Of ICostIntermediateDistributionAdminService)()
            Return service.UpdateStateIntermediateDistribution(code, state, audit)
        End Using
    End Function

    Public Function GetIntermediateDistribution(code As String, audit As AuditMessage) As ActionResult(Of CostIntermediateDistribution) Implements ICostServiceCostIntermediateDistribution.GetIntermediateDistribution
        Using service As ICostIntermediateDistributionAdminService = Container.Current.Resolve(Of ICostIntermediateDistributionAdminService)()
            Return service.GetIntermediateDistribution(code, audit)
        End Using
    End Function

    Public Function GetIntermediateDistributionById(id As Integer) As CostIntermediateDistribution Implements ICostServiceCostIntermediateDistribution.GetIntermediateDistributionById
        Using service As ICostIntermediateDistributionAdminService = Container.Current.Resolve(Of ICostIntermediateDistributionAdminService)()
            Return service.GetIntermediateDistributionById(id)
        End Using
    End Function

    Public Function SP_CopyPasteCostIntermediateDistribution(data As List(Of List(Of String)), intermediateDistributionId As Integer, initialDistribution As Decimal) As ActionResult(Of List(Of CostIntermediateDistributionBaseDetail), List(Of Tuple(Of String, Integer))) Implements ICostServiceCostIntermediateDistribution.SP_CopyPasteCostIntermediateDistribution
        Using service As ICostIntermediateDistributionAdminService = Container.Current.Resolve(Of ICostIntermediateDistributionAdminService)()
            Return service.SP_CopyPasteCostIntermediateDistribution(data, intermediateDistributionId, initialDistribution)
        End Using
    End Function

    Public Function ImportDetailsToCostIntermediateDistributionBase(DistributionType As Byte, MeasurementUnit As Byte, ListIntermediateDistributionBaseDetail As List(Of CostIntermediateDistributionBaseDetail), Data As List(Of List(Of String))) As ActionResult(Of List(Of CostIntermediateDistributionBaseDetail)) Implements ICostServiceCostIntermediateDistribution.ImportDetailsToCostIntermediateDistributionBase
        Using service As ICostIntermediateDistributionAdminService = Container.Current.Resolve(Of ICostIntermediateDistributionAdminService)()
            Return service.ImportDetailsToCostIntermediateDistributionBase(DistributionType, MeasurementUnit, ListIntermediateDistributionBaseDetail, Data)
        End Using
    End Function
End Class
