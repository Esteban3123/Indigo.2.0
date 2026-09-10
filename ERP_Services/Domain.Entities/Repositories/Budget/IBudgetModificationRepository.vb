'***********************************************************************
' Assembly         : Domain.Budget
' Author           : Jhossept Kevin Garay
' Created          : 21-07-2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************
Imports Domain.Base
Imports Domain.Entities
Public Interface IBudgetModificationRepository
    Inherits IRepository(Of BudgetModification)

    ''' <summary>
    ''' Obtener una modificacion de presupuesto por codigo
    ''' </summary>
    ''' <param name="code"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetBudgetModification(code As String, type As Integer, budgetaryValidityId As Integer) As BudgetModification

    ''' <summary>
    ''' Obtener una modificacion de presupuesto por codigo
    ''' </summary>
    ''' <param name="id"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetBudgetModificationById(id As Integer) As BudgetModification

    ''' <summary>
    ''' Guarda la modificacion
    ''' </summary>
    ''' <param name="BudgetModificationXml"></param>
    ''' <param name="BudgetModificationDetailForDeleteXml"></param>
    ''' <param name="CodeUser"></param>
    ''' <returns></returns>
    Function SP_SaveBudgetModification(BudgetModificationXml As String, BudgetModificationDetailForDeleteXml As String, CodeUser As String) As SP_SaveBudgetModification_Result

End Interface
