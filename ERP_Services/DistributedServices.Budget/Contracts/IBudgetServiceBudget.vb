'***********************************************************************
' Assembly         : DistributedServices.Budget
' Author           : Jeisson Herrera Peña
' Created          : 25/08/2015
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"

Imports Infrastructure.CrossCutting.Base
Imports System.ServiceModel
Imports Domain.Entities
Imports Domain.Base.Entities

#End Region

<ServiceContract()>
Public Interface IBudgetServiceBudget

    ''' <summary>
    ''' Obtiene un presupuesto por id
    ''' </summary>
    '''<param name="Id">Id del presupuesto</param>
    ''' <returns></returns>
    <OperationContract()>
    Function GetBudgetById(Id As Integer, audit As AuditMessage) As Domain.Base.Entities.ActionResult(Of Domain.Entities.Budget)
    ''' <summary>
    ''' Obtiene el saldo del presupuesto por id del rubro y el tipo
    ''' </summary>
    ''' <param name="categoryId"></param>
    ''' <param name="revenueTypeId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <OperationContract()>
    Function GetBalanceBudgetByCategoryIdAndRevenueTypeId(categoryId As Integer, revenueTypeId As Integer, audit As AuditMessage) As Decimal

End Interface
