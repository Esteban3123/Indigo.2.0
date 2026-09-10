Imports Infrastructure.CrossCutting.Base
Imports System.ServiceModel
Imports Application.Cost
Imports Microsoft.Practices.Unity

Partial Public Class CostService
    Implements ICostServiceCostGeneralExpenseCategory

    Public Function DeleteCostGeneralExpenseCategory(CostGeneralExpenseCategory As Domain.Entities.CostGeneralExpenseCategory, audit As AuditMessage) As Domain.Base.Entities.ActionResult Implements ICostServiceCostGeneralExpenseCategory.DeleteCostGeneralExpenseCategory
        Using service As ICostGeneralExpenseCategoryAdminService = Container.Current.Resolve(Of ICostGeneralExpenseCategoryAdminService)()
            Return service.DeleteCostGeneralExpenseCategory(CostGeneralExpenseCategory, audit)
        End Using
        'Return _costGeneralExpenseCategoryAdminService.DeleteCostGeneralExpenseCategory(CostGeneralExpenseCategory, audit)
    End Function

    Public Function GetCostGeneralExpenseCategory(code As String, audit As AuditMessage) As Domain.Base.Entities.ActionResult(Of Domain.Entities.CostGeneralExpenseCategory) Implements ICostServiceCostGeneralExpenseCategory.GetCostGeneralExpenseCategory
        Using service As ICostGeneralExpenseCategoryAdminService = Container.Current.Resolve(Of ICostGeneralExpenseCategoryAdminService)()
            Return service.GetCostGeneralExpenseCategory(code, audit)
        End Using
        'Return _costGeneralExpenseCategoryAdminService.GetCostGeneralExpenseCategory(code, audit)
    End Function

    Public Function GetCostGeneralExpenseCategoryById(id As Integer, audit As AuditMessage) As Domain.Base.Entities.ActionResult(Of Domain.Entities.CostGeneralExpenseCategory) Implements ICostServiceCostGeneralExpenseCategory.GetCostGeneralExpenseCategoryById
        Using service As ICostGeneralExpenseCategoryAdminService = Container.Current.Resolve(Of ICostGeneralExpenseCategoryAdminService)()
            Return service.GetCostGeneralExpenseCategoryById(id, audit)
        End Using
        'Return _costGeneralExpenseCategoryAdminService.GetCostGeneralExpenseCategoryById(id, audit)
    End Function

    Public Function SaveCostGeneralExpenseCategory(CostGeneralExpenseCategory As Domain.Entities.CostGeneralExpenseCategory, audit As AuditMessage) As Domain.Base.Entities.ActionResult(Of Domain.Entities.CostGeneralExpenseCategory) Implements ICostServiceCostGeneralExpenseCategory.SaveCostGeneralExpenseCategory
        Using service As ICostGeneralExpenseCategoryAdminService = Container.Current.Resolve(Of ICostGeneralExpenseCategoryAdminService)()
            Return service.SaveCostGeneralExpenseCategory(CostGeneralExpenseCategory, audit)
        End Using
        'Return _costGeneralExpenseCategoryAdminService.SaveCostGeneralExpenseCategory(CostGeneralExpenseCategory, audit)
    End Function

    Public Function UpdateStateCostGeneralExpenseCategory(code As String, state As Boolean, audit As AuditMessage) As Domain.Base.Entities.ActionResult(Of Domain.Entities.CostGeneralExpenseCategory) Implements ICostServiceCostGeneralExpenseCategory.UpdateStateCostGeneralExpenseCategory
        Using service As ICostGeneralExpenseCategoryAdminService = Container.Current.Resolve(Of ICostGeneralExpenseCategoryAdminService)()
            Return service.UpdateState(code, state, audit)
        End Using
        'Return _costGeneralExpenseCategoryAdminService.UpdateState(code, state, audit)
    End Function
End Class
