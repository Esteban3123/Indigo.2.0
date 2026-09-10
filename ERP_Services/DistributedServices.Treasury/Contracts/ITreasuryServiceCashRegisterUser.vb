'***********************************************************************
' Assembly         : DistributedServices.Treasury
' Author           : Diego Andrés Roldán Lozano
' Created          : 07-05-2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities
Imports System.ServiceModel

<ServiceContract()>
Public Interface ITreasuryServiceCashRegisterUser

    ''' <summary>
    ''' Gets the cash register user by identifier.
    ''' </summary>
    ''' <param name="Id">The identifier.</param>
    ''' <returns></returns>
    <OperationContract()>
    Function GetCashRegisterUserById(Id As Integer, audit As AuditMessage) As CashRegisterUser

    ''' <summary>
    ''' Lists the cash register user by identifier cash register.
    ''' </summary>
    ''' <param name="IdCashRegister">The identifier cash register.</param>
    ''' <returns></returns>
    <OperationContract()>
    Function ListCashRegisterUserByIdCashRegister(IdCashRegister As Integer, audit As AuditMessage) As List(Of CashRegisterUser)

    ''' <summary>
    ''' Lista los usuarios permitidos para una caja
    ''' </summary>
    ''' <param name="CashRegisterId">The identifier entity bank account.</param>
    ''' <returns></returns>
    <OperationContract()>
    Function ListUsersByCashRegisterId(CashRegisterId As Integer, audit As AuditMessage) As Domain.Base.Entities.ActionResult(Of List(Of Domain.Security.Entities.User))

End Interface
