'***********************************************************************
' Assembly         : DistributedServices.Treasury
' Author           : Diego Andrés Roldán Lozano
' Created          : 02-04-2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities
Imports System.ServiceModel

<ServiceContract()>
Public Interface ITreasuryServiceCashRegister

    ''' <summary>
    ''' Saves the cash register.
    ''' </summary>
    ''' <param name="cashRegister">The cash register.</param>
    ''' <returns></returns>
    <OperationContract()>
    Function SaveCashRegister(cashRegister As Domain.Entities.CashRegisters, idSequence As Int64, audit As AuditMessage) As Domain.Base.Entities.ActionResult(Of Domain.Entities.CashRegisters)

    ''' <summary>
    ''' Actualiza el estado del registro
    ''' </summary>
    ''' <param name="code">The code.</param>
    ''' <param name="state">if set to <c>true</c> [state].</param>
    ''' <returns></returns>
    <OperationContract()>
    Function UpdateStateCashRegister(code As String, state As Boolean, audit As AuditMessage) As ActionResult(Of CashRegisters)

    ''' <summary>
    ''' Deletes the cash register.
    ''' </summary>
    ''' <param name="cashRegister">The cash register.</param>
    ''' <returns></returns>
    <OperationContract()>
    Function DeleteCashRegister(cashRegister As Domain.Entities.CashRegisters, audit As AuditMessage) As Domain.Base.Entities.ActionResult

    ''' <summary>
    ''' Gets the cash register.
    ''' </summary>
    ''' <param name="code">The code.</param>
    ''' <returns></returns>
    <OperationContract()>
    Function GetCashRegister(code As String, audit As AuditMessage) As ActionResult(Of CashRegisters)

    ''' <summary>
    ''' Obtiene una caja por Id
    ''' </summary>
    ''' <param name="Id">The identifier.</param>
    ''' <returns></returns>
    <OperationContract()>
    Function GetCashRegisterById(ByVal Id As Integer) As CashRegisters

    ''' <summary>
    ''' Lista todos los registros de caja
    ''' </summary>
    ''' <returns></returns>
    <OperationContract()>
    Function ListCashRegister() As List(Of CashRegisters)

    ''' <summary>
    ''' Lista todos los prefijos registrados en la tabla
    ''' </summary>
    ''' <returns>Lista de prefijos</returns>
    <OperationContract()>
    Function ListPrefixsCashRegister() As List(Of String)

    <OperationContract()>
    Function GetFirstCashbyUserId(UserId As Integer, Optional currencyId As Integer? = Nothing) As CashRegisters

End Interface
