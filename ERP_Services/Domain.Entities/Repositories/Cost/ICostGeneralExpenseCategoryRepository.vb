'***********************************************************************
' Assembly         : Domain.Cost
' Author           : Carlos Mario Arias Rubiano
' Created          : 21/11/2016
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************
Imports Domain.Base
Imports Domain.Entities

Public Interface ICostGeneralExpenseCategoryRepository
    Inherits IRepository(Of CostGeneralExpenseCategory)

    ''' <summary>
    ''' Obtiene la entidad por codigo
    ''' </summary>
    ''' <returns></returns>
    Function GetCostGeneralExpenseCategory(ByVal code As String) As CostGeneralExpenseCategory

    ''' <summary>
    ''' Obtiene la entidad por id
    ''' </summary>
    ''' <returns></returns>
    Function GetCostGeneralExpenseCategoryById(id As Integer) As CostGeneralExpenseCategory

End Interface