Imports Infrastructure.CrossCutting.Base
Imports System.ServiceModel
Imports Application.Cost
Imports Microsoft.Practices.Unity
Imports Domain.Entities
Imports Domain.Base.Entities

Partial Public Class CostService
    Implements ICostServiceCostGeneralExpense

    Public Function DeleteGeneralExpense(generalExpense As Domain.Entities.CostGeneralExpense, audit As AuditMessage) As Domain.Base.Entities.ActionResult Implements ICostServiceCostGeneralExpense.DeleteGeneralExpense
        Using service As ICostGeneralExpenseAdminService = Container.Current.Resolve(Of ICostGeneralExpenseAdminService)()
            Return service.DeleteGeneralExpense(generalExpense, audit)
        End Using
    End Function

    Public Function GetGeneralExpense(code As String, audit As AuditMessage) As Domain.Base.Entities.ActionResult(Of Domain.Entities.CostGeneralExpense) Implements ICostServiceCostGeneralExpense.GetGeneralExpense
        Using service As ICostGeneralExpenseAdminService = Container.Current.Resolve(Of ICostGeneralExpenseAdminService)()
            Return service.GetGeneralExpense(code, audit)
        End Using
    End Function

    Public Function GetGeneralExpenseById(id As Integer) As Domain.Entities.CostGeneralExpense Implements ICostServiceCostGeneralExpense.GetGeneralExpenseById
        Using service As ICostGeneralExpenseAdminService = Container.Current.Resolve(Of ICostGeneralExpenseAdminService)()
            Return service.GetGeneralExpenseById(id)
        End Using
    End Function

    Public Function GetGeneralExpenseByMainAccountId(MainAccountId As Integer) As Domain.Entities.CostGeneralExpense Implements ICostServiceCostGeneralExpense.GetGeneralExpenseByMainAccountId
        Using service As ICostGeneralExpenseAdminService = Container.Current.Resolve(Of ICostGeneralExpenseAdminService)()
            Return service.GetGeneralExpenseByMainAccountId(MainAccountId)
        End Using
    End Function

    Public Function SaveGeneralExpense(generalExpense As Domain.Entities.CostGeneralExpense, audit As AuditMessage, idSequence As Int64) As Domain.Base.Entities.ActionResult(Of Domain.Entities.CostGeneralExpense) Implements ICostServiceCostGeneralExpense.SaveGeneralExpense
        Using service As ICostGeneralExpenseAdminService = Container.Current.Resolve(Of ICostGeneralExpenseAdminService)()
            Return service.SaveGeneralExpense(generalExpense, audit, idSequence)
        End Using
    End Function

    Public Function UpdateStateCostGeneralExpense(code As String, state As Boolean, audit As AuditMessage) As Domain.Base.Entities.ActionResult(Of Domain.Entities.CostGeneralExpense) Implements ICostServiceCostGeneralExpense.UpdateStateCostGeneralExpense
        Using service As ICostGeneralExpenseAdminService = Container.Current.Resolve(Of ICostGeneralExpenseAdminService)()
            Return service.UpdateStateCostGeneralExpense(code, state, audit)
        End Using
    End Function

    Public Function ImportDetailsToCostDistributionBase(DistributionType As Byte, MeasurementUnit As Byte, ListDistributionBaseDetail As List(Of CostDistributionBaseDetail), Data As List(Of List(Of String))) As ActionResult(Of List(Of CostDistributionBaseDetail)) Implements ICostServiceCostGeneralExpense.ImportDetailsToCostDistributionBase
        Using service As ICostGeneralExpenseAdminService = Container.Current.Resolve(Of ICostGeneralExpenseAdminService)()
            Return service.ImportDetailsToCostDistributionBase(DistributionType, MeasurementUnit, ListDistributionBaseDetail, Data)
        End Using
    End Function

End Class
