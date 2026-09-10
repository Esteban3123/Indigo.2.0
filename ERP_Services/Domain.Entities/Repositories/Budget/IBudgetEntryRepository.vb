'***********************************************************************
' Assembly         : Domain.Budget
' Author           : Oscar Ivan Sierra Jaramillo
' Created          : 21-07-2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************
Imports Domain.Base
Imports Domain.Entities
Public Interface IBudgetEntryRepository
    Inherits IRepository(Of Budget)



    ''' <summary>
    ''' Obtiene una categoria teniendo en cuenta el Id de la vigencia
    ''' </summary>
    ''' <param name="ValidityId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetBudgetCategory(ValidityId As Integer, RevenueType As String) As List(Of BudgetEntry)

    ''' <summary>
    ''' Obtener todos los registros de presupuesto inicial por vigencia y tipo
    ''' </summary>
    ''' <param name="ValidityId"></param>
    ''' <param name="itemType"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetBudgetBudget(ValidityId As Integer, itemType As Integer) As List(Of Budget)

    ''' <summary>
    ''' Obtener todos los registros de presupuesto inicial por vigencia y tipo
    ''' </summary>
    ''' <param name="ValidityId"></param>
    ''' <param name="itemType"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetBudgetBudgetInitialValueZero(ValidityId As Integer, itemType As Integer, FlagInitialValue As Boolean) As List(Of Budget)

    ''' <summary>
    ''' Obtiene una una vigencia
    ''' </summary>
    ''' <param name="ValidityId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetBudgetValidity(ValidityId As Integer) As BudgetaryValidity

    ''' <summary>
    ''' Guardo una vigencia
    ''' </summary>
    ''' <param name="ValidityId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function SaveBudgetValidity(ValidityId As BudgetaryValidity) As Boolean

    ''' <summary>
    ''' Obtiene la cabecera con sus detalles del presupuesto inicial
    ''' </summary>
    ''' <param name="budgetaryValidityId">Id de la vigencia</param>
    ''' <param name="type">tipo: Ingreso o Gasto</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetBudgetHeader(budgetaryValidityId As Integer, type As Integer) As BudgetHeader

End Interface
