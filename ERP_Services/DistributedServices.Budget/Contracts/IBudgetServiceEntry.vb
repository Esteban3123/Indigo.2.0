
'***********************************************************************
' Assembly         : DistributedServices.Budget
' Author           : Oscar Ivan Sierra  
' Created          : 21-07-2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"

Imports Infrastructure.CrossCutting.Base
Imports System.ServiceModel
Imports Domain.Entities
Imports Domain.Base.Entities

#End Region

<ServiceContract()> _
Public Interface IBudgetServiceEntry


    ''' <summary>
    ''' obtiene la lista de categorias teniendo en cuenta el Id de la vigencia
    ''' </summary>
    ''' <param name="ValidityId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <OperationContract()>
    Function GetBudgetCategory(ValidityId As Integer, TypeRevenue As String, audit As AuditMessage) As List(Of Domain.Entities.BudgetEntry)

    ''' <summary>
    ''' Guarda o Actualiza el presupuesto inicial
    ''' </summary>
    ''' <param name="BudgetHeader">la entidad</param>
    ''' <param name="state">The audit.</param>
    ''' <returns></returns>
    ''' <exception cref="System.ArgumentNullException">entidad Vacio</exception>
    <OperationContract()>
    Function SaveBudget(BudgetHeader As BudgetHeader, state As Integer, audit As AuditMessage) As Domain.Base.Entities.ActionResult(Of BudgetHeader)

    <OperationContract()>
    Function GetBudgetBudget(ValidityId As Integer, itemType As Integer, audit As AuditMessage) As List(Of Domain.Entities.Budget)

    <OperationContract()>
    Function GetBudgetBudgetInitialValueZero(ValidityId As Integer, itemType As Integer, FlagInitialValue As Boolean, audit As AuditMessage) As List(Of Domain.Entities.Budget)

    ''' <summary>
    ''' Obtiene la cabecera con sus detalles del presupuesto inicial
    ''' </summary>
    ''' <param name="budgetaryValidityId"></param>
    ''' <param name="type"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <OperationContract()>
    Function GetBudgetHeader(budgetaryValidityId As Integer, type As Integer, audit As AuditMessage) As Domain.Base.Entities.ActionResult(Of Domain.Entities.BudgetHeader)

End Interface
