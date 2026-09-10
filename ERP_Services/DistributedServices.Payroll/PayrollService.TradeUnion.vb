'***********************************************************************
' Assembly         : DistributedServices.Payments
' Author           : Juan Carlos Bermudez
' Created          : 16/07/2015
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Payroll.Entities
Imports Application.Payroll
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities
Imports System.ServiceModel
Imports Infrastructure.CrossCutting.IOC

Partial Class PayrollService

    ''' <summary>
    ''' Elimina un sindicato
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function DeleteTradeUnion(tradeUnion As TradeUnion, session As SessionValues) As ActionResult Implements IPayrollTradeUnion.DeleteTradeUnion
        Using TradeUnionAdminService As ITradeUnionAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of ITradeUnionAdminService)()
            Return TradeUnionAdminService.DeleteTradeUnion(tradeUnion, session.AuditMessageWcf)
        End Using
    End Function

    ''' <summary>
    ''' Obtiene un determinado sindicato
    ''' </summary>
    ''' <param name="code"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetTradeUnionByCode(code As String, tracking As Boolean, session As SessionValues) As ActionResult(Of TradeUnion) Implements IPayrollTradeUnion.GetTradeUnionByCode
        Using TradeUnionAdminService As ITradeUnionAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of ITradeUnionAdminService)()
            Return TradeUnionAdminService.GetTradeUnionByCode(code, tracking, session.AuditMessageWcf)
        End Using
    End Function

    ''' <summary>
    ''' Guarda o actualiza un sindicato
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SaveTradeUnion(tradeUnion As TradeUnion, session As SessionValues, idSequense As Int64) As ActionResult(Of TradeUnion) Implements IPayrollTradeUnion.SaveTradeUnion
        Using TradeUnionAdminService As ITradeUnionAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of ITradeUnionAdminService)()
            Return TradeUnionAdminService.SaveTradeUnion(tradeUnion, session.AuditMessageWcf, idSequense)
        End Using
    End Function

    Public Function ChangeStateTradeUnion(code As String, state As Boolean, session As SessionValues) As Domain.Base.Entities.ActionResult(Of Domain.Payroll.Entities.TradeUnion) Implements IPayrollTradeUnion.ChangeStateTradeUnion
        Using TradeUnionAdminService As ITradeUnionAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of ITradeUnionAdminService)()
            Return TradeUnionAdminService.ChangeState(code, state, session.AuditMessageWcf)
        End Using
    End Function

    ''' <summary>
    ''' sindicato por id
    ''' </summary>
    ''' <param name="id"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetTradeUnionById(id As Integer, tracking As Boolean, session As SessionValues) As ActionResult(Of Domain.Payroll.Entities.TradeUnion) Implements IPayrollTradeUnion.GetTradeUnionById
        Using TradeUnionAdminService As ITradeUnionAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of ITradeUnionAdminService)()
            Return TradeUnionAdminService.GetTradeUnionById(id, tracking, session.AuditMessageWcf)
        End Using
    End Function


End Class
