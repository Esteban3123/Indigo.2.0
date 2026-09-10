'***********************************************************************
' Assembly         : DistributedServices.Payments
' Author           : Carlos Mario Arias Rubiano
' Created          : 01-04-2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Entities
Imports Application.Payments
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities
Imports System.ServiceModel
Imports Microsoft.Practices.Unity

Partial Public Class PaymentsService

    ''' <summary>
    ''' Elimina un saldo inicial
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function DeleteOpeningBalance(openingBalance As Domain.Entities.InitialBalance, audit As AuditMessage) As Domain.Base.Entities.ActionResult Implements IPaymentsOpeningBalance.DeleteOpeningBalance
        Using service As IOpeningBalanceAdminService = Container.Current.Resolve(Of IOpeningBalanceAdminService)()
            Return service.DeleteOpeningBalance(openingBalance, audit)
        End Using
        'Return Me._openingBalanceAdminService.DeleteOpeningBalance(openingBalance, audit)
    End Function

    ''' <summary>
    ''' Obtiene un saldo inicial
    ''' </summary>
    ''' <param name="code"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetOpeningBalance(code As String, audit As AuditMessage) As ActionResult(Of Domain.Entities.InitialBalance) Implements IPaymentsOpeningBalance.GetOpeningBalance
        Using service As IOpeningBalanceAdminService = Container.Current.Resolve(Of IOpeningBalanceAdminService)()
            Return service.GetOpeningBalance(code, audit)
        End Using
        'Return Me._openingBalanceAdminService.GetOpeningBalance(code, audit)
    End Function


    ''' <summary>
    ''' Guarda o actualiza un saldo inicial
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SaveOpeningBalance(openingBalance As Domain.Entities.InitialBalance, idSequense As Int64, audit As AuditMessage) As Domain.Base.Entities.ActionResult(Of Domain.Entities.InitialBalance) Implements IPaymentsOpeningBalance.SaveOpeningBalance
        Using service As IOpeningBalanceAdminService = Container.Current.Resolve(Of IOpeningBalanceAdminService)()
            Return service.SaveOpeningBalance(openingBalance, audit, idSequense)
        End Using
        'Return Me._openingBalanceAdminService.SaveOpeningBalance(openingBalance, audit, idSequense)
    End Function

    Public Function ConfirmOpeningBalance(openingBalance As Domain.Entities.InitialBalance, modeSaveAndConfirm As Boolean, _idOperativeUnit As Int32, idSequense As Int64, audit As AuditMessage) As Domain.Base.Entities.ActionResult(Of Domain.Entities.InitialBalance) Implements IPaymentsOpeningBalance.ConfirmOpeningBalance
        Using service As IOpeningBalanceAdminService = Container.Current.Resolve(Of IOpeningBalanceAdminService)()
            Return service.ConfirmOpeningBalance(openingBalance, modeSaveAndConfirm, audit, idSequense, _idOperativeUnit)
        End Using
        'Return Me._openingBalanceAdminService.ConfirmOpeningBalance(openingBalance, modeSaveAndConfirm, audit, idSequense, _idOperativeUnit)
    End Function

    ''' <summary>
    ''' Cambia el estado de la entidad
    ''' </summary>
    ''' <param name="code"></param>
    ''' <param name="state"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ChangeStateOpeningBalance(code As String, state As Boolean, audit As AuditMessage) As Domain.Base.Entities.ActionResult(Of Domain.Entities.InitialBalance) Implements IPaymentsOpeningBalance.ChangeStateOpeningBalance
        Using service As IOpeningBalanceAdminService = Container.Current.Resolve(Of IOpeningBalanceAdminService)()
            Return service.ChangeState(code, state, audit)
        End Using
        'Return Me._openingBalanceAdminService.ChangeState(code, state, audit)
    End Function

    ''' <summary>
    ''' Valida los campos del copyPaste de anticipos
    ''' </summary>
    ''' <param name="data"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SetAdvanceInitialBalance(data As List(Of List(Of String))) As Domain.Base.Entities.ActionResult(Of List(Of Domain.Entities.InitialBalanceAdvance)) Implements IPaymentsOpeningBalance.SetAdvanceInitialBalance
        Using service As IOpeningBalanceAdminService = Container.Current.Resolve(Of IOpeningBalanceAdminService)()
            Return service.SetAdvanceInitialBalance(data)
        End Using
        'Return _openingBalanceAdminService.SetAdvanceInitialBalance(data)
    End Function

    ''' <summary>
    ''' Valida los campos del copyPaste de facturas
    ''' </summary>
    ''' <param name="data"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SetBillsInitialBalance(data As List(Of List(Of String))) As Domain.Base.Entities.ActionResult(Of List(Of Domain.Entities.InitialBalanceAccountPayable)) Implements IPaymentsOpeningBalance.SetBillsInitialBalance
        Using service As IOpeningBalanceAdminService = Container.Current.Resolve(Of IOpeningBalanceAdminService)()
            Return service.SetBillsInitialBalance(data)
        End Using
        'Return _openingBalanceAdminService.SetBillsInitialBalance(data)
    End Function

End Class
