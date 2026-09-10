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
    ''' Elimina un anticipo
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function DeleteMoneyAdvance(moneyAdvance As Domain.Entities.AdvancePayments, audit As AuditMessage) As Domain.Base.Entities.ActionResult Implements IPaymentsMoneyAdvance.DeleteMoneyAdvance
        Using service As IMoneyAdvanceAdminService = Container.Current.Resolve(Of IMoneyAdvanceAdminService)()
            Return service.DeleteMoneyAdvance(moneyAdvance, audit)
        End Using
        'Return Me._moneyAdvanceAdminService.DeleteMoneyAdvance(moneyAdvance, audit)
    End Function

    ''' <summary>
    ''' Obtiene un anticipo
    ''' </summary>
    ''' <param name="code"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetMoneyAdvance(code As String, audit As AuditMessage) As Domain.Entities.AdvancePayments Implements IPaymentsMoneyAdvance.GetMoneyAdvance
        Using service As IMoneyAdvanceAdminService = Container.Current.Resolve(Of IMoneyAdvanceAdminService)()
            Return service.GetMoneyAdvance(code, audit)
        End Using
        'Return Me._moneyAdvanceAdminService.GetMoneyAdvance(code, audit)
    End Function


    ''' <summary>
    ''' Guarda o actualiza un anticipo
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SaveMoneyAdavance(moneyAdvance As Domain.Entities.AdvancePayments, idSequense As Int64, audit As AuditMessage) As Domain.Base.Entities.ActionResult(Of Domain.Entities.AdvancePayments) Implements IPaymentsMoneyAdvance.SaveMoneyAdavance
        Using service As IMoneyAdvanceAdminService = Container.Current.Resolve(Of IMoneyAdvanceAdminService)()
            Return service.SaveMoneyAdvance(moneyAdvance, audit, idSequense)
        End Using
        'Return Me._moneyAdvanceAdminService.SaveMoneyAdvance(moneyAdvance, audit, idSequense)
    End Function

    ''' <summary>
    ''' Cambia el estado de la entidad
    ''' </summary>
    ''' <param name="code"></param>
    ''' <param name="state"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ChangeStateMoneyAdvance(code As String, state As Boolean, audit As AuditMessage) As Domain.Base.Entities.ActionResult(Of Domain.Entities.AdvancePayments) Implements IPaymentsMoneyAdvance.ChangeStateMoneyAdvance
        Using service As IMoneyAdvanceAdminService = Container.Current.Resolve(Of IMoneyAdvanceAdminService)()
            Return service.ChangeState(code, state, audit)
        End Using
        'Return Me._moneyAdvanceAdminService.ChangeState(code, state, audit)
    End Function

    ''' <summary>
    ''' Obtiene un anticipo por id
    ''' </summary>
    ''' <param name="id"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetMoneyAdvanceById(id As Integer, audit As AuditMessage) As Domain.Entities.AdvancePayments Implements IPaymentsMoneyAdvance.GetMoneyAdvanceById
        Using service As IMoneyAdvanceAdminService = Container.Current.Resolve(Of IMoneyAdvanceAdminService)()
            Return service.GetMoneyAdvanceById(id, audit)
        End Using
        'Return Me._moneyAdvanceAdminService.GetMoneyAdvanceById(id, audit)
    End Function

    ''' <summary>
    ''' Lista todos los avances por tercero
    ''' </summary>
    ''' <param name="ThirdId">The third identifier.</param>
    ''' <returns></returns>
    Public Function ListAdvancePaymentByThirdId(ThirdId As Integer) As ActionResult(Of List(Of AdvancePayments)) Implements IPaymentsMoneyAdvance.ListAdvancePaymentByThirdId
        Using service As IMoneyAdvanceAdminService = Container.Current.Resolve(Of IMoneyAdvanceAdminService)()
            Return service.ListAdvancePaymentByThirdId(ThirdId)
        End Using
        'Return Me._moneyAdvanceAdminService.ListAdvancePaymentByThirdId(ThirdId)
    End Function

    ''' <summary>
    ''' Obtiene un anticipo por código
    ''' </summary>
    ''' <param name="code"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetAdvanceByCode(ByVal code As String) As Domain.Entities.AdvancePayments Implements IPaymentsMoneyAdvance.GetAdvanceByCode
        Using service As IMoneyAdvanceAdminService = Container.Current.Resolve(Of IMoneyAdvanceAdminService)()
            Return service.GetAdvanceByCode(code)
        End Using
        'Return Me._moneyAdvanceAdminService.GetAdvanceByCode(code)
    End Function
End Class
