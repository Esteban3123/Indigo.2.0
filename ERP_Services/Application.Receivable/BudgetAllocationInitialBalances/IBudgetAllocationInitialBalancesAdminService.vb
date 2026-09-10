'***********************************************************************
' Assembly         : Application.Portfolio
' Author           : Carlos Ernesto Cordoba
' Created          : 19-10-20
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities

Public Interface IBudgetAllocationInitialBalancesAdminService
    Inherits IDisposable
    ''' <summary>
    ''' asigana un presupuesto a las facturas de saldos iniciales
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function SaveBudgetAllocationInitialBalances(listItems As List(Of Tuple(Of Integer, Integer)), initialBalanceId As Integer, audit As AuditMessage) As ActionResult


End Interface
