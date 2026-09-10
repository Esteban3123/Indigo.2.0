'***********************************************************************
' Assembly         : Application.Treasury
' Author           : Diego Andrés Roldán Lozano
' Created          : 07-05-2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities

Public Interface ICashRegisterUserAdminService
    Inherits IDisposable

    ''' <summary>
    ''' Gets the cash register user by identifier.
    ''' </summary>
    ''' <param name="Id">The identifier.</param>
    ''' <param name="audit">The audit.</param>
    ''' <returns></returns>
    Function GetCashRegisterUserById(ByVal Id As Integer, ByVal audit As AuditMessage) As CashRegisterUser

    ''' <summary>
    ''' Lists the cash register user by identifier cash register.
    ''' </summary>
    ''' <param name="IdCashRegister">The identifier cash register.</param>
    ''' <param name="audit">The audit.</param>
    ''' <returns></returns>
    Function ListCashRegisterUserByIdCashRegister(ByVal IdCashRegister As Integer, ByVal audit As AuditMessage) As List(Of CashRegisterUser)

    ''' <summary>
    ''' Lista los usuarios permitidos para una caja
    ''' </summary>
    ''' <param name="CashRegisterId">The identifier entity bank account.</param>
    ''' <returns></returns>
    Function ListUsersByCashRegisterId(ByVal CashRegisterId As Integer, ByVal audit As AuditMessage) As ActionResult(Of List(Of Domain.Security.Entities.User))

End Interface
