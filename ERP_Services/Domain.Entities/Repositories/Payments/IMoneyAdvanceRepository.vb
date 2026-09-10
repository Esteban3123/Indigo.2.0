'***********************************************************************
' Assembly         : Domain.Payments
' Author           : Carlos Mario Arias Rubiano
' Created          : 21-06-2013
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Base
Imports Domain.Entities

Public Interface IMoneyAdvanceRepository
    Inherits IRepository(Of AdvancePayments)

    Function ListMoneyAdvanceById(listId As List(Of Integer?)) As List(Of AdvancePayments)

    ''' <summary>
    ''' Obtiene un anticipo
    ''' </summary>
    ''' <param name="code"></param>
    ''' <param name="tracking"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetMoneyAdvance(ByVal code As String, Optional tracking As Boolean = True) As AdvancePayments

    ''' <summary>
    ''' Obtiene un anticipo por id
    ''' </summary>
    ''' <param name="id"></param>
    ''' <param name="tracking"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetMoneyAdvanceById(ByVal id As Integer, Optional tracking As Boolean = True) As AdvancePayments

    ''' <summary>
    ''' Lista todos los avances por tercero
    ''' </summary>
    ''' <param name="ThirdId">The third identifier.</param>
    ''' <returns></returns>
    Function ListAdvancePaymentByThirdId(ByVal ThirdId As Integer) As List(Of AdvancePayments)

    ''' <summary>
    ''' Obtiene un anticipo por código
    ''' </summary>
    ''' <param name="code"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetAdvanceByCode(ByVal code As String) As AdvancePayments

    Function SaveMoneyAdvanceList(moneyAdvanceList As List(Of AdvancePayments)) As IEnumerable(Of AdvancePayments)

End Interface
