'***********************************************************************
' Assembly         : Application.Budget
' Author           : Oscar Ivan Sierra Jaramillo
' Created          : 21-07-2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities

Public Interface IBudgetEntryAdminService
    Inherits IDisposable


    ''' <summary>
    ''' Obtiene una categoria teniendo en cuenta el id de la vigencia
    ''' </summary>
    ''' <param name="ValidityId">The code.</param>
    ''' <param name="audit">objeto de auditoria</param>
    ''' <returns></returns>
    Function GetBudgetCategory(ValidityId As Integer, audit As AuditMessage, RevenueType As String) As List(Of BudgetEntry)

    ''' <summary>
    ''' Obtener los rubros presupuestales de una vigencia
    ''' </summary>
    ''' <param name="ValidityId"></param>
    ''' <param name="itemType"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetBudgetBudget(ValidityId As Integer, itemType As Integer, audit As AuditMessage) As List(Of Domain.Entities.Budget)

    ''' <summary>
    ''' Obtener los rubros presupuestales de una vigencia
    ''' </summary>
    ''' <param name="ValidityId"></param>
    ''' <param name="itemType"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetBudgetBudgetInitialValueZero(ValidityId As Integer, itemType As Integer, audit As AuditMessage, FlagInitialValue As Boolean) As List(Of Domain.Entities.Budget)

    ''' <summary>
    ''' Guarda o Actualiza el presupuesto inicial
    ''' </summary>
    ''' <param name="BudgetHeader">la entidad</param>
    ''' <param name="audit">The audit.</param>
    ''' <returns></returns>
    ''' <exception cref="System.ArgumentNullException">entidad Vacio</exception>
    Function SaveBudget(BudgetHeader As BudgetHeader, state As Integer, audit As AuditMessage) As ActionResult(Of BudgetHeader)

    ''' <summary>
    ''' Obtiene la cebecera con sus detalles del presupuesto inicial
    ''' </summary>
    ''' <param name="budgetaryValidityId"></param>
    ''' <param name="type"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetBudgetHeader(budgetaryValidityId As Integer, type As Integer, audit As AuditMessage) As ActionResult(Of BudgetHeader)

End Interface
