'***********************************************************************
' Assembly         : Domain.Budget
' Author           : Jhossept Kevin Garay Rodriguez
' Created          : 10-04-2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************
Imports Domain.Base
Imports Domain.Entities

Public Interface IExpenseTypeRepository
    Inherits IRepository(Of RevenueType)

    ''' <summary>
    ''' Obtiene un tipo de gasto
    ''' </summary>
    ''' <param name="code"></param>
    ''' <param name="tracking"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetExpenseType(code As String, validityId As Integer, type As Integer, Optional tracking As Boolean = True) As RevenueType

    ''' <summary>
    ''' Obtiene un tipo de gasto by validity
    ''' </summary>
    ''' <param name="code"></param>
    ''' <param name="tracking"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetExpenseTypeByValidity(code As String, ValidityId As String, Optional tracking As Boolean = True) As RevenueType

    ''' <summary>
    ''' Obtiene un tipo por id
    ''' </summary>
    ''' <param name="id"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetRevenueTypeById(id As Integer) As RevenueType

End Interface