'***********************************************************************
' Assembly         : DistributedServices.Portfolio
' Author           : Diego Andrés Roldán Lozano
' Created          : 29-07-2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities
Imports System.ServiceModel

<ServiceContract()>
Public Interface IPortfolioServiceBudgetAllocationInitialBalances
    ''' <summary>
    ''' asigana un presupuesto a las facturas de saldos iniciales
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <OperationContract()>
    Function SaveBudgetAllocationInitialBalances(listItems As List(Of Tuple(Of Integer, Integer)), initialBalanceId As Integer, audit As AuditMessage) As Domain.Base.Entities.ActionResult
End Interface
