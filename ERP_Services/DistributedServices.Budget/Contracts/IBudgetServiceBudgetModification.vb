
'***********************************************************************
' Assembly         : DistributedServices.Budget
' Author           : Jhossept Kevin Garay  
' Created          : 04-08-2014
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
Public Interface IBudgetServiceBudgetModification
    ''' <summary>
    ''' Obtener una modificacion de presupuesto por codigo
    ''' </summary>
    ''' <param name="code"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <OperationContract()>
    Function GetBudgetModification(code As String, type As Integer, budgetaryValidityId As Integer, audit As AuditMessage) As BudgetModification

    ''' <summary>
    ''' Obtener una modificacion de presupuesto por codigo
    ''' </summary>
    ''' <param name="id"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <OperationContract()>
    Function GetBudgetModificationById(id As Integer, audit As AuditMessage) As BudgetModification

    ''' <summary>
    ''' Guarda una modificacion de presupuesto
    ''' </summary>
    ''' <param name="budgetModification"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <OperationContract()>
    Function SaveBudgetMofication(BudgetModification As BudgetModification, listBudgetModificationDetailDelete As List(Of Integer), audit As AuditMessage) As ActionResult(Of BudgetModification)

    ''' <summary>
    ''' elimina las modificaciones de presupuesto
    ''' </summary>
    ''' <param name="budgetModification"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <OperationContract()>
    Function DeleteBudgetModification(budgetModification As BudgetModification, audit As AuditMessage) As ActionResult

    ''' <summary>
    ''' metodo para cambiar el estado de la entidad
    ''' </summary>
    ''' <param name="code">The code.</param>
    ''' <param name="state">if set to <c>true</c> [state].</param>
    ''' <param name="audit">The audit.</param>
    ''' <returns></returns>
    <OperationContract()>
    Function ChangeStateBudgetModification(code As String, validity As Integer, state As Integer, budgetaryValidityId As Integer, audit As AuditMessage) As ActionResult(Of BudgetModification)
End Interface
