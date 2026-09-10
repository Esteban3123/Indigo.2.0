'***********************************************************************
' Assembly         : DistributedServices.Portfolio
' Author           : Carlos Ernesto Cordoba
' Created          : 04-04-2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"
Imports Domain.Entities
Imports Infrastructure.CrossCutting.IOC
Imports Application.Portfolio
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities
Imports System.ServiceModel
Imports Microsoft.Practices.Unity
#End Region

Partial Public Class PortfolioService
    Public Function SaveBudgetAllocationInitialBalances(listItems As List(Of Tuple(Of Integer, Integer)), initialBalanceId As Integer, audit As AuditMessage) As Domain.Base.Entities.ActionResult Implements IPortfolioServiceBudgetAllocationInitialBalances.SaveBudgetAllocationInitialBalances
        Using service As IBudgetAllocationInitialBalancesAdminService = Container.Current.Resolve(Of IBudgetAllocationInitialBalancesAdminService)()
            Return service.SaveBudgetAllocationInitialBalances(listItems, initialBalanceId, audit)
        End Using
        'Return _BudgetAllocationInitialBalancesAdminService.SaveBudgetAllocationInitialBalances(listItems, initialBalanceId, audit)
    End Function
End Class
