'***********************************************************************
' Assembly         : DistributedServices.Treasury
' Author           : Diego Andrés Roldán lozano
' Created          : 07-05-2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Entities
Imports Application.Treasury
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities
Imports System.ServiceModel
Imports Microsoft.Practices.Unity

Partial Class TreasuryService

    ''' <summary>
    ''' Gets the cash register user by identifier.
    ''' </summary>
    ''' <param name="Id">The identifier.</param>
    ''' <returns></returns>
    Public Function GetCashRegisterUserById(Id As Integer, audit As AuditMessage) As CashRegisterUser Implements ITreasuryServiceCashRegisterUser.GetCashRegisterUserById
        Using service As ICashRegisterUserAdminService = Container.Current.Resolve(Of ICashRegisterUserAdminService)()
            Return service.GetCashRegisterUserById(Id, audit)
        End Using
        'Return Me._cashRegisterUserAdminService.GetCashRegisterUserById(Id, audit)
    End Function

    ''' <summary>
    ''' Lists the cash register user by identifier cash register.
    ''' </summary>
    ''' <param name="IdCashRegister">The identifier cash register.</param>
    ''' <returns></returns>
    Public Function ListCashRegisterUserByIdCashRegister(IdCashRegister As Integer, audit As AuditMessage) As List(Of CashRegisterUser) Implements ITreasuryServiceCashRegisterUser.ListCashRegisterUserByIdCashRegister
        Using service As ICashRegisterUserAdminService = Container.Current.Resolve(Of ICashRegisterUserAdminService)()
            Return service.ListCashRegisterUserByIdCashRegister(IdCashRegister, audit)
        End Using
        'Return Me._cashRegisterUserAdminService.ListCashRegisterUserByIdCashRegister(IdCashRegister, audit)
    End Function

    Public Function ListUsersByCashRegisterId(CashRegisterId As Integer, audit As AuditMessage) As Domain.Base.Entities.ActionResult(Of List(Of Domain.Security.Entities.User)) Implements ITreasuryServiceCashRegisterUser.ListUsersByCashRegisterId
        Using service As ICashRegisterUserAdminService = Container.Current.Resolve(Of ICashRegisterUserAdminService)()
            Return service.ListUsersByCashRegisterId(CashRegisterId, audit)
        End Using
        'Return Me._cashRegisterUserAdminService.ListUsersByCashRegisterId(CashRegisterId, audit)
    End Function

End Class
