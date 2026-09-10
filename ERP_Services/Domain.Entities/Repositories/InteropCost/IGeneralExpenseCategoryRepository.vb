'***********************************************************************
' Assembly         : Domain.InteropCost
' Author           : Carlos Mario Arias Rubiano
' Created          : 22/11/2016
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************
Imports Domain.Base
Imports Domain.Entities

Public Interface IGeneralExpenseCategoryRepository
    Inherits IRepository(Of GeneralExpenseCategory)

     ''' <summary>
    ''' Obtiene la entidad por codigo
    ''' </summary>
    ''' <returns></returns>
    Function GetGeneralExpenseCategory(ByVal code As String) As GeneralExpenseCategory

    ''' <summary>
    ''' Obtiene la entidad por id
    ''' </summary>
    ''' <returns></returns>
    Function GetGeneralExpenseCategoryById(id As Integer) As GeneralExpenseCategory

End Interface