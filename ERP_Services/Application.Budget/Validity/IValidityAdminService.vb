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

Public Interface IValidityAdminService
    Inherits IDisposable

    ''' <summary>
    ''' Obtiene las validaciones de una entidad presupuestal
    ''' </summary>
    ''' <param name="BudgetEntityId">Id de la entidad presupuestal.</param>
    ''' <returns></returns>
    Function GetValidityByBudgetEntity(BudgetEntityId As String, audit As AuditMessage) As Object

    ''' <summary>
    ''' Obtiene una validacion por Id
    ''' </summary>
    ''' <param name="Id">Id de la validacion.</param>
    ''' <returns></returns>
    Function GetValidity(Id As String, audit As AuditMessage) As BudgetaryValidity

    ''' <summary>
    ''' Guarda o Actualiza una Vigencia
    ''' </summary>
    ''' <param name="BudgetaryValidity">la entidad</param>
    ''' <param name="audit">The audit.</param>
    ''' <returns></returns>
    ''' <exception cref="System.ArgumentNullException">entidad Vacio</exception>
    Function SaveValidity(BudgetaryValidity As BudgetaryValidity, audit As AuditMessage) As ActionResult(Of BudgetaryValidity)

    ''' <summary>
    ''' Cierra una Vigencia
    ''' </summary>
    ''' <param name="BudgetaryValidity">la entidad</param>
    ''' <param name="type">Tipo 1. Ingresos 2. Gastos</param>
    ''' <param name="audit">The audit.</param>
    ''' <returns></returns>
    ''' <exception cref="System.ArgumentNullException">entidad Vacio</exception>
    Function CloseValidity(BudgetaryValidity As BudgetaryValidity, type As Integer, audit As AuditMessage) As ActionResult(Of BudgetaryValidity)

    ''' <summary>
    ''' Recalcula los valores de una vigencia
    ''' </summary>
    ''' <param name="BudgetaryValidity">la entidad</param>
    ''' <param name="type">Tipo 1. Ingresos 2. Gastos</param>
    ''' <param name="audit">The audit.</param>
    ''' <returns></returns>
    ''' <exception cref="System.ArgumentNullException">entidad Vacio</exception>
    Function RecalculateBalances(BudgetaryValidity As BudgetaryValidity, type As Integer, audit As AuditMessage) As ActionResult(Of BudgetaryValidity)

    ''' <summary>
    ''' Elimina una vigencia
    ''' </summary>
    ''' <param name="BudgetaryValidity">La entidad</param>
    ''' <param name="audit">The audit.</param>
    ''' <returns></returns>
    ''' <exception cref="System.ArgumentNullException">company Vacio</exception>
    Function DeleteValidity(BudgetaryValidity As BudgetaryValidity, audit As AuditMessage) As ActionResult
End Interface
