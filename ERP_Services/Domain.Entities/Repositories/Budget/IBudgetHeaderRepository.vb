'***********************************************************************
' Assembly         : Domain.Budget
' Author           : Carlos Mario Arias Rubiano
' Created          : 06/08/2015
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************
Imports Domain.Base
Imports Domain.Entities
Public Interface IBudgetHeaderRepository
    Inherits IRepository(Of BudgetHeader)

    ''' <summary>
    ''' Obtiene un presupuesto inicial
    ''' </summary>
    '''<param name="IdBudget">Id del presupuesto para obtener el budgetHeader</param>
    ''' <returns></returns>
    Function GetBudgetHeaderByIdForBudgetTransfer(IdBudget As Integer) As BudgetHeader

End Interface
