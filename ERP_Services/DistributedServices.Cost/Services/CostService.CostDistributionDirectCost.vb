Imports Infrastructure.CrossCutting.Base
Imports System.ServiceModel
Imports Application.Cost
Imports Microsoft.Practices.Unity
Imports DistributedServices.Cost
Imports Domain.Base.Entities
Imports Domain.Entities

Partial Public Class CostService
    Implements ICostServiceCostDistributionDirectCost

    Public Function DeleteDistributionDirectCost(distributionDirectCost As Domain.Entities.CostDistributionDirectCost, audit As AuditMessage) As Domain.Base.Entities.ActionResult Implements ICostServiceCostDistributionDirectCost.DeleteDistributionDirectCost
        Using service As ICostDistributionDirectCostAdminService = Container.Current.Resolve(Of ICostDistributionDirectCostAdminService)()
            Return service.DeleteDistributionDirectCost(distributionDirectCost, audit)
        End Using
        'Return _costDistributionDirectCostAdminService.DeleteDistributionDirectCost(distributionDirectCost, audit)
    End Function

    Public Function GetDistributionDirectCost(code As String, audit As AuditMessage) As Domain.Base.Entities.ActionResult(Of Domain.Entities.CostDistributionDirectCost) Implements ICostServiceCostDistributionDirectCost.GetDistributionDirectCost
        Using service As ICostDistributionDirectCostAdminService = Container.Current.Resolve(Of ICostDistributionDirectCostAdminService)()
            Return service.GetDistributionDirectCost(code, audit)
        End Using
        'Return _costDistributionDirectCostAdminService.GetDistributionDirectCost(code, audit)
    End Function

    Public Function GetDistributionDirectCostById(id As Integer) As Domain.Entities.CostDistributionDirectCost Implements ICostServiceCostDistributionDirectCost.GetDistributionDirectCostById
        Using service As ICostDistributionDirectCostAdminService = Container.Current.Resolve(Of ICostDistributionDirectCostAdminService)()
            Return service.GetDistributionDirectCostById(id)
        End Using
        'Return _costDistributionDirectCostAdminService.GetDistributionDirectCostById(id)
    End Function

    Public Function GetMainAccountValueByNumberAccountYearAndMotn(mainAccountId As Integer, year As String, month As Integer) As Decimal Implements ICostServiceCostDistributionDirectCost.GetMainAccountValueByNumberAccountYearAndMotn
        Using service As ICostDistributionDirectCostAdminService = Container.Current.Resolve(Of ICostDistributionDirectCostAdminService)()
            Return service.GetMainAccountValueByNumberAccountYearAndMotn(mainAccountId, year, month)
        End Using
        'Return _costDistributionDirectCostAdminService.GetMainAccountValueByNumberAccountYearAndMotn(mainAccountId, year, month)
    End Function
    Public Function ListDistributionDirectCostByYearMonth(year As Integer, month As Integer) As List(Of Domain.Entities.CostDistributionDirectCost) Implements ICostServiceCostDistributionDirectCost.ListDistributionDirectCostByYearMonth
        Using service As ICostDistributionDirectCostAdminService = Container.Current.Resolve(Of ICostDistributionDirectCostAdminService)()
            Return service.ListDistributionDirectCostByYearMonth(year, month)
        End Using
        'Return _costDistributionDirectCostAdminService.ListDistributionDirectCostByYearMonth(year, month)
    End Function

    Public Function SaveDistributionDirectCost(distributionDirectCost As Domain.Entities.CostDistributionDirectCost, audit As AuditMessage, idSequence As Int64) As Domain.Base.Entities.ActionResult(Of Domain.Entities.CostDistributionDirectCost) Implements ICostServiceCostDistributionDirectCost.SaveDistributionDirectCost
        Using service As ICostDistributionDirectCostAdminService = Container.Current.Resolve(Of ICostDistributionDirectCostAdminService)()
            Return service.SaveDistributionDirectCost(distributionDirectCost, audit, idSequence)
        End Using
        'Return _costDistributionDirectCostAdminService.SaveDistributionDirectCost(distributionDirectCost, audit, idSequence)
    End Function

    Public Function ReverseProvisionDocument(distributionDirectCostId As Integer, audit As AuditMessage) As Domain.Base.Entities.ActionResult(Of Domain.Entities.CostDistributionDirectCost) Implements ICostServiceCostDistributionDirectCost.ReverseProvisionDocument
        Using service As ICostDistributionDirectCostAdminService = Container.Current.Resolve(Of ICostDistributionDirectCostAdminService)()
            Return service.ReverseProvisionDocument(distributionDirectCostId, audit)
        End Using
    End Function

    Public Function UpdateStateDistributionDirectCost(code As String, state As Boolean, audit As AuditMessage) As Domain.Base.Entities.ActionResult(Of Domain.Entities.CostDistributionDirectCost) Implements ICostServiceCostDistributionDirectCost.UpdateStateDistributionDirectCost
        Using service As ICostDistributionDirectCostAdminService = Container.Current.Resolve(Of ICostDistributionDirectCostAdminService)()
            Return service.UpdateStateDistributionDirectCost(code, state, audit)
        End Using
        'Return _costDistributionDirectCostAdminService.UpdateStateDistributionDirectCost(code, state, audit)
    End Function

    Public Function CalculateCostDistribution(CostGeneralExpenseId As Integer, value As Decimal, year As Integer, month As Integer, containerName As String) As Domain.Base.Entities.ActionResult(Of List(Of Domain.Entities.CostDistributionDirectCostDetail)) Implements ICostServiceCostDistributionDirectCost.CalculateCostDistribution
        Using service As ICostDistributionDirectCostAdminService = Container.Current.Resolve(Of ICostDistributionDirectCostAdminService)()
            Return service.CalculateCostDistribution(CostGeneralExpenseId, value, year, month, containerName)
        End Using
        'Return Me._costDistributionDirectCostAdminService.CalculateCostDistribution(CostGeneralExpenseId, value, year, month, containerName)
    End Function

    Public Function DisconfirmDistributionDirectCost(distributionDirectCost As Domain.Entities.CostDistributionDirectCost, audit As AuditMessage) As Domain.Base.Entities.ActionResult(Of Domain.Entities.CostDistributionDirectCost) Implements ICostServiceCostDistributionDirectCost.DisconfirmDistributionDirectCost
        Using service As ICostDistributionDirectCostAdminService = Container.Current.Resolve(Of ICostDistributionDirectCostAdminService)()
            Return service.DisconfirmDistributionDirectCost(distributionDirectCost, audit)
        End Using
        'Return _costDistributionDirectCostAdminService.DisconfirmDistributionDirectCost(distributionDirectCost, audit)
    End Function

    Public Function CopyAndPasteCostDistributionDirectCostDetail(GeneralExpenseId As Integer, data As List(Of List(Of String))) As ActionResult(Of List(Of CostDistributionDirectCostDetail)) Implements ICostServiceCostDistributionDirectCost.CopyAndPasteCostDistributionDirectCostDetail
        Using service As ICostDistributionDirectCostAdminService = Container.Current.Resolve(Of ICostDistributionDirectCostAdminService)()
            Return service.CopyAndPasteCostDistributionDirectCostDetail(GeneralExpenseId, data)
        End Using
    End Function
End Class
