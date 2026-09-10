'***********************************************************************
' Assembly         : DistributedServices.InteropCost
' Author           : Diego Andrés Roldán lozano
' Created          : 22-12-2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities
Imports System.ServiceModel
Imports Application.InteropCost
Imports Microsoft.Practices.Unity

Partial Class InteropCostService
    Implements IInteropCostServiceGeneralExpenseCategory

    Public Function DeleteGeneralExpenseCategory(GeneralExpenseCategory As GeneralExpenseCategory, audit As AuditMessage) As ActionResult Implements IInteropCostServiceGeneralExpenseCategory.DeleteGeneralExpenseCategory
        Using service As IGeneralExpenseCategoryAdminService = DistributedServices.InteropCost.Container.Current.Resolve(Of IGeneralExpenseCategoryAdminService)()
            Return service.DeleteGeneralExpenseCategory(GeneralExpenseCategory, audit)
        End Using
        'Return _generalExpenseCategoryAdminService.DeleteGeneralExpenseCategory(GeneralExpenseCategory, audit)
    End Function

    Public Function GetGeneralExpenseCategory(code As String, audit As AuditMessage) As ActionResult(Of GeneralExpenseCategory) Implements IInteropCostServiceGeneralExpenseCategory.GetGeneralExpenseCategory
        Using service As IGeneralExpenseCategoryAdminService = DistributedServices.InteropCost.Container.Current.Resolve(Of IGeneralExpenseCategoryAdminService)()
            Return service.GetGeneralExpenseCategory(code, audit)
        End Using
        'Return _generalExpenseCategoryAdminService.GetGeneralExpenseCategory(code, audit)
    End Function

    Public Function GetGeneralExpenseCategoryById(id As Integer, audit As AuditMessage) As ActionResult(Of GeneralExpenseCategory) Implements IInteropCostServiceGeneralExpenseCategory.GetGeneralExpenseCategoryById
        Using service As IGeneralExpenseCategoryAdminService = DistributedServices.InteropCost.Container.Current.Resolve(Of IGeneralExpenseCategoryAdminService)()
            Return service.GetGeneralExpenseCategoryById(id, audit)
        End Using
        'Return _generalExpenseCategoryAdminService.GetGeneralExpenseCategoryById(id, audit)
    End Function

    Public Function SaveGeneralExpenseCategory(GeneralExpenseCategory As GeneralExpenseCategory, audit As AuditMessage) As ActionResult(Of GeneralExpenseCategory) Implements IInteropCostServiceGeneralExpenseCategory.SaveGeneralExpenseCategory
        Using service As IGeneralExpenseCategoryAdminService = DistributedServices.InteropCost.Container.Current.Resolve(Of IGeneralExpenseCategoryAdminService)()
            Return service.SaveGeneralExpenseCategory(GeneralExpenseCategory, audit)
        End Using
        'Return _generalExpenseCategoryAdminService.SaveGeneralExpenseCategory(GeneralExpenseCategory, audit)
    End Function

    Public Function UpdateStateGeneralExpenseCategory(code As String, state As Boolean, audit As AuditMessage) As ActionResult(Of GeneralExpenseCategory) Implements IInteropCostServiceGeneralExpenseCategory.UpdateStateGeneralExpenseCategory
        Using service As IGeneralExpenseCategoryAdminService = DistributedServices.InteropCost.Container.Current.Resolve(Of IGeneralExpenseCategoryAdminService)()
            Return service.UpdateState(code, state, audit)
        End Using
        'Return _generalExpenseCategoryAdminService.UpdateState(code, state, audit)
    End Function

End Class