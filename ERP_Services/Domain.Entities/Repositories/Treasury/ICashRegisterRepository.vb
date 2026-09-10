'***********************************************************************
' Assembly         : Domain.Treasury
' Author           : Diego Andrés Roldán Lozano
' Created          : 02-04-2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************
Imports Domain.Base
Imports Domain.Entities


Public Interface ICashRegisterRepository
    Inherits IRepository(Of CashRegisters)

    ''' <summary>
    ''' Obtiene un registro de caja
    ''' </summary>
    ''' <param name="code">The code.</param>
    ''' <param name="tracking">if set to <c>true</c> [tracking].</param>
    ''' <returns></returns>
    Function GetCashRegister(ByVal code As String, Optional tracking As Boolean = True) As CashRegisters

    ''' <summary>
    ''' Obtiene una caja por Id
    ''' </summary>
    ''' <param name="Id">The identifier.</param>
    ''' <returns></returns>
    Function GetCashRegisterById(ByVal Id As Integer, Optional currencyflag As Boolean = True) As CashRegisters

    ''' <summary>
    ''' Lista todos los registros de caja
    ''' </summary>
    ''' <returns></returns>
    Function ListCashRegister() As List(Of CashRegisters)


    ''' <summary>
    ''' Lista todos los prefijos que existen en la tabla
    ''' </summary>
    ''' <returns>Lista de prefijos</returns>
    Function ListPrefixs() As List(Of String)

    Function GetFirstCashbyUserId(UserId As Integer, Optional currencyId As Integer? = Nothing) As CashRegisters

    Function ListCashRegisterUserByCashRegisterIdAndUser(cashId As Integer, userId As Integer) As List(Of CashRegisterUser)
End Interface
