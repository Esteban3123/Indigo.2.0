'***********************************************************************
' Assembly         : Application.Treasury
' Author           : Diego Andrés Roldán Lozano
' Created          : 02-04-2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities

Public Interface ICashRegisterAdminService
    Inherits IDisposable

    ''' <summary>
    ''' Saves the cash register.
    ''' </summary>
    ''' <param name="cashRegister">The cash register.</param>
    ''' <param name="audit">The audit.</param>
    ''' <returns></returns>
    Function SaveCashRegister(ByVal cashRegister As CashRegisters, ByVal audit As AuditMessage, Optional ByVal idSequence As Int64 = 0, Optional withCommit As Boolean = True) As ActionResult(Of CashRegisters)

    ''' <summary>
    ''' actualiza el estado del registro
    ''' </summary>
    ''' <param name="code">The code.</param>
    ''' <param name="state">if set to <c>true</c> [state].</param>
    ''' <param name="audit">The audit.</param>
    ''' <returns></returns>
    Function UpdateStateCashRegister(ByVal code As String, ByVal state As Boolean, ByVal audit As AuditMessage) As ActionResult(Of CashRegisters)

    ''' <summary>
    ''' Deletes the cash register.
    ''' </summary>
    ''' <param name="cashRegister">The cash register.</param>
    ''' <param name="audit">The audit.</param>
    ''' <returns></returns>
    Function DeleteCashRegister(ByVal cashRegister As CashRegisters, ByVal audit As AuditMessage) As ActionResult

    ''' <summary>
    ''' Gets the cash register.
    ''' </summary>
    ''' <param name="code">The code.</param>
    ''' <param name="audit">The audit.</param>
    ''' <returns></returns>
    Function GetCashRegister(ByVal code As String, ByVal audit As AuditMessage) As ActionResult(Of CashRegisters)

    ''' <summary>
    ''' Obtiene una caja por Id
    ''' </summary>
    ''' <param name="Id">The identifier.</param>
    ''' <returns></returns>
    Function GetCashRegisterById(ByVal Id As Integer) As CashRegisters

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

End Interface
