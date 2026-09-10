'***********************************************************************
' Assembly         : DistributedServices.Budget
' Author           : Jeisson Herrera Peña
' Created          : 25-08-2015
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"

Imports Application.Budget
Imports Infrastructure.CrossCutting.Base
Imports Microsoft.Practices.Unity

#End Region

Partial Class BudgetService

#Region "Methods"

    ''' <summary>
    ''' Obtiene el presupuesto por Id
    ''' </summary>
    ''' <param name="Id"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetBudgetById(Id As Integer, audit As AuditMessage) As Domain.Base.Entities.ActionResult(Of Domain.Entities.Budget) Implements IBudgetServiceBudget.GetBudgetById
        Using service As IBudgetAdminService = Container.Current.Resolve(Of IBudgetAdminService)()
            Return service.GetBudgetById(Id, audit)
        End Using
    End Function

    ''' <summary>
    ''' Obtiene el presupuesto por Id
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetBalanceBudgetByCategoryIdAndRevenueTypeId(categoryId As Integer, revenueTypeId As Integer, audit As AuditMessage) As Decimal Implements IBudgetServiceBudget.GetBalanceBudgetByCategoryIdAndRevenueTypeId
        Using service As IBudgetAdminService = Container.Current.Resolve(Of IBudgetAdminService)()
            Return service.GetBalanceBudgetByCategoryIdAndRevenueTypeId(categoryId, revenueTypeId)
        End Using
    End Function

#End Region

End Class