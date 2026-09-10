'***********************************************************************
' Assembly         : Domain.Budget
' Author           : Jhossept Kevin Garay Rodriguez
' Created          : 04-04-2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************
Imports Domain.Base
Imports Domain.Entities

Public Interface IValidityRepository
    Inherits IRepository(Of BudgetaryValidity)

    ''' <summary>
    ''' Obtiene las validaciones de una entidad presupuestal
    ''' </summary>
    ''' <param name="BudgetInstitutionsId">Id de la entidad presupuestal.</param>
    ''' <returns></returns>
    Function GetValidityByBudgetInstitutions(BudgetInstitutionsId As String) As List(Of BudgetaryValidity)

    ''' <summary>
    ''' Obtiene una validacion por Id
    ''' </summary>
    ''' <param name="Id">Id de la validacion.</param>
    ''' <param name="tracking">if set to <c>true</c> [tracking].</param>
    ''' <returns></returns>
    Function GetValidity(Id As String, Optional tracking As Boolean = True) As BudgetaryValidity

    ''' <summary>
    ''' Cerrar la vigencia de ingresos
    ''' </summary>
    ''' <param name="BudgetaryValidityId"></param>
    ''' <param name="CodeUser"></param>
    ''' <returns></returns>
    Function SP_ClosingValidityIncome(BudgetaryValidityId As Integer, CodeUser As String) As SP_ClosingValidityIncome_Result

    ''' <summary>
    ''' Cerrar la vigencia de gastos
    ''' </summary>
    ''' <param name="BudgetaryValidityId"></param>
    ''' <param name="CodeUser"></param>
    ''' <returns></returns>
    Function SP_ClosingValidityExpense(BudgetaryValidityId As Integer, CodeUser As String) As SP_ClosingValidityExpense_Result

    ''' <summary>
    ''' Recalcular los valores de la vigencia de gastos
    ''' </summary>
    ''' <param name="BudgetaryValidityId"></param>
    ''' <param name="CodeUser"></param>
    ''' <returns></returns>
    Function SP_RecalculateBalancesExpense(BudgetaryValidityId As Integer, CodeUser As String) As SP_RecalculateBalancesExpense_Result

    ''' <summary>
    ''' Recalcular los valores de la vigencia de ingresos
    ''' </summary>
    ''' <param name="BudgetaryValidityId"></param>
    ''' <param name="CodeUser"></param>
    ''' <returns></returns>
    Function SP_RecalculateBalancesIncome(BudgetaryValidityId As Integer, CodeUser As String) As SP_RecalculateBalancesIncome_Result

End Interface
