'***********************************************************************
' Assembly         : DistributedServices.Treasury
' Author           : Diego Andrés Roldán lozano
' Created          : 01-04-2014
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
    Implements ITreasuryServiceCashRegister

    ''' <summary>
    ''' Deletes the cash register.
    ''' </summary>
    ''' <param name="cashRegister">The cash register.</param>
    ''' <returns></returns>
    Public Function DeleteCashRegister(cashRegister As Domain.Entities.CashRegisters, audit As AuditMessage) As Domain.Base.Entities.ActionResult Implements ITreasuryServiceCashRegister.DeleteCashRegister
        Using service As ICashRegisterAdminService = Container.Current.Resolve(Of ICashRegisterAdminService)()
            Return service.DeleteCashRegister(cashRegister, audit)
        End Using
        'Return Me._cashAdminService.DeleteCashRegister(cashRegister, audit)
    End Function

    ''' <summary>
    ''' Gets the cash register.
    ''' </summary>
    ''' <param name="code">The code.</param>
    ''' <returns></returns>
    Public Function GetCashRegister(code As String, audit As AuditMessage) As ActionResult(Of CashRegisters) Implements ITreasuryServiceCashRegister.GetCashRegister
        Using service As ICashRegisterAdminService = Container.Current.Resolve(Of ICashRegisterAdminService)()
            Return service.GetCashRegister(code, audit)
        End Using
        'Return Me._cashAdminService.GetCashRegister(code, audit)
    End Function

    ''' <summary>
    ''' Saves the cash register.
    ''' </summary>
    ''' <param name="cashRegister">The cash register.</param>
    ''' <returns></returns>
    Public Function SaveCashRegister(cashRegister As Domain.Entities.CashRegisters, idSequence As Int64, audit As AuditMessage) As Domain.Base.Entities.ActionResult(Of Domain.Entities.CashRegisters) Implements ITreasuryServiceCashRegister.SaveCashRegister
        Using service As ICashRegisterAdminService = Container.Current.Resolve(Of ICashRegisterAdminService)()
            Return service.SaveCashRegister(cashRegister, audit, idSequence)
        End Using
        'Return Me._cashAdminService.SaveCashRegister(cashRegister, audit, idSequence)
    End Function

    ''' <summary>
    ''' Actualiza el estado del registro
    ''' </summary>
    ''' <param name="code">The code.</param>
    ''' <param name="state">if set to <c>true</c> [state].</param>
    ''' <returns></returns>
    Public Function UpdateStateCashRegister(code As String, state As Boolean, audit As AuditMessage) As ActionResult(Of CashRegisters) Implements ITreasuryServiceCashRegister.UpdateStateCashRegister
        Using service As ICashRegisterAdminService = Container.Current.Resolve(Of ICashRegisterAdminService)()
            Return service.UpdateStateCashRegister(code, state, audit)
        End Using
        'Return Me._cashAdminService.UpdateStateCashRegister(code, state, audit)
    End Function

    ''' <summary>
    ''' Lista todos los registros de caja
    ''' </summary>
    ''' <returns></returns>
    Public Function ListCashRegister() As List(Of CashRegisters) Implements ITreasuryServiceCashRegister.ListCashRegister
        Using service As ICashRegisterAdminService = Container.Current.Resolve(Of ICashRegisterAdminService)()
            Return service.ListCashRegister()
        End Using
        'Return Me._cashAdminService.ListCashRegister()
    End Function

    ''' <summary>
    ''' Obtiene una caja por Id
    ''' </summary>
    ''' <param name="Id">The identifier.</param>
    ''' <returns></returns>
    Public Function GetCashRegisterById(Id As Integer) As CashRegisters Implements ITreasuryServiceCashRegister.GetCashRegisterById
        Using service As ICashRegisterAdminService = Container.Current.Resolve(Of ICashRegisterAdminService)()
            Return service.GetCashRegisterById(Id)
        End Using
        'Return Me._cashAdminService.GetCashRegisterById(Id)
    End Function

    ''' <summary>
    ''' Lista todos los prefijos que existen en la tabla
    ''' </summary>
    ''' <returns>Lista de prefijos</returns>
    Public Function ListPrefixsCashRegister() As List(Of String) Implements ITreasuryServiceCashRegister.ListPrefixsCashRegister
        Using service As ICashRegisterAdminService = Container.Current.Resolve(Of ICashRegisterAdminService)()
            Return service.ListPrefixs()
        End Using
        'Return Me._cashAdminService.ListPrefixs()
    End Function

    Public Function GetFirstCashbyUserId(UserId As Integer, Optional currencyId As Integer? = Nothing) As CashRegisters Implements ITreasuryServiceCashRegister.GetFirstCashbyUserId
        Using service As ICashRegisterAdminService = Container.Current.Resolve(Of ICashRegisterAdminService)()
            Return service.GetFirstCashbyUserId(UserId, currencyId)
        End Using
        'Return Me._cashAdminService.GetFirstCashbyUserId(UserId)
    End Function

End Class
