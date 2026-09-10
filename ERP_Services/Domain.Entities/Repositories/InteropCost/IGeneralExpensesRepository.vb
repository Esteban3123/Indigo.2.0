'***********************************************************************
' Assembly         : Domain.InteropCost
' Author           : Diego Andrés Roldán Lozano
' Created          : 22-12-2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************
Imports Domain.Base
Imports Domain.Entities

Public Interface IGeneralExpensesRepository
    Inherits IRepository(Of GeneralExpense)

    ''' <summary>
    ''' Obtiene un gasto general
    ''' </summary>
    ''' <param name="code">The code.</param>
    ''' <returns></returns>
    Function GetGeneralExpense(ByVal code As String) As GeneralExpense

    ''' <summary>
    ''' Obtiene un gasto general por id
    ''' </summary>
    ''' <param name="id">The identifier.</param>
    ''' <returns></returns>
    Function GetGeneralExpenseById(id As Integer) As GeneralExpense

    Function ListGeneralExpenseByStatus(ByVal status As Boolean) As List(Of GeneralExpense)

    ''' <summary>
    ''' Lista los gastos generales por id de cuenta contable
    ''' </summary>
    Function GetGeneralExpenseByMainAccountId(MainAccountId As Integer) As GeneralExpense

End Interface