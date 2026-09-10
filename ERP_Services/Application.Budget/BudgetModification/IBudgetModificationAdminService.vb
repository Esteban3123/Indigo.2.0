'***********************************************************************
' Assembly         : Application.Budget
' Author           : Jhossept Kevin Garay Rodriguez
' Created          : 04-08-2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities

Public Interface IBudgetModificationAdminService
    Inherits IDisposable
    ''' <summary>
    ''' Obtener una modificacion de presupuesto por codigo
    ''' </summary>
    ''' <param name="code"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetBudgetModification(code As String, type As Integer, budgetaryValidityId As Integer, audit As AuditMessage) As BudgetModification

    ''' <summary>
    ''' Obtener una modificacion de presupuesto por codigo
    ''' </summary>
    ''' <param name="id"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetBudgetModificationById(id As Integer) As BudgetModification

    ''' <summary>
    ''' Guarda una modificacion de presupuesto
    ''' </summary>
    ''' <param name="budgetModification"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function SaveBudgetMofication(BudgetModification As BudgetModification, listBudgetModificationDetailDelete As List(Of Integer), audit As AuditMessage) As ActionResult(Of BudgetModification)

    ''' <summary>
    ''' elimina las modificaciones de presupuesto
    ''' </summary>
    ''' <param name="budgetModification"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function DeleteBudgetModification(budgetModification As BudgetModification, audit As AuditMessage) As ActionResult

    ''' <summary>
    ''' metodo para cambiar el estado de la entidad
    ''' </summary>
    ''' <param name="code">The code.</param>
    ''' <param name="state">if set to <c>true</c> [state].</param>
    ''' <param name="audit">The audit.</param>
    ''' <returns></returns>
    Function ChangeStateBudgetModification(code As String, validity As Integer, state As Integer, budgetaryValidityId As Integer, audit As AuditMessage) As ActionResult(Of BudgetModification)
End Interface
