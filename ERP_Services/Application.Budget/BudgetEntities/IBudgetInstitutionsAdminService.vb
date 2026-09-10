'***********************************************************************
' Assembly         : Application.Budget
' Author           : Jhossept Kevin Garay Rodriguez
' Created          : 04-04-2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities

Public Interface IBudgetInstitutionAdminService
    Inherits IDisposable

    ''' <summary>
    ''' Obtiene una entidad presupuestal por codigo
    ''' </summary>
    ''' <param name="code">The code.</param>
    ''' <param name="audit">objeto de auditoria</param>
    ''' <returns></returns>
    Function GetBudgetInstitution(code As String, audit As AuditMessage) As BudgetaryEntity

    ''' <summary>
    ''' Guarda o Actualiza una Entidad Presupuestal
    ''' </summary>
    ''' <param name="BudgetEntity">la entidad</param>
    ''' <param name="audit">The audit.</param>
    ''' <returns></returns>
    ''' <exception cref="System.ArgumentNullException">entidad Vacio</exception>
    Function SaveBudgetInstitution(BudgetBudgetInstitution As BudgetaryEntity, audit As AuditMessage) As ActionResult(Of BudgetaryEntity)

    ''' <summary>
    ''' Elimina una entidad presupuestal
    ''' </summary>
    ''' <param name="BudgetEntity">La entidad</param>
    ''' <param name="audit">The audit.</param>
    ''' <returns></returns>
    ''' <exception cref="System.ArgumentNullException">company Vacio</exception>
    Function DeleteBudgetInstitution(BudgetaryEntity As BudgetaryEntity, audit As AuditMessage) As ActionResult

    ''' <summary>
    ''' metodo para cambiar el estado de la entidad
    ''' </summary>
    ''' <param name="code">The code.</param>
    ''' <param name="state">if set to <c>true</c> [state].</param>
    ''' <param name="audit">The audit.</param>
    ''' <returns></returns>
    Function ChangeStateBudgetInstitution(code As String, state As Boolean, audit As AuditMessage) As ActionResult(Of BudgetaryEntity)
End Interface
