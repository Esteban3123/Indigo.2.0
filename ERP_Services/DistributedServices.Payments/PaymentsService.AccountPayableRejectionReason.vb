'***********************************************************************
' Assembly         : DistributedServices.Payments
' Author           : Carlos Mario Arias Rubiano
' Created          : 03/04/2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Application.Payments
Imports Infrastructure.CrossCutting.Base
Imports Microsoft.Practices.Unity

Partial Public Class PaymentsService

    ''' <summary>
    ''' Elimina un rechazo de factura
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function DeleteAccountPayableRejectionReason(AccountPayableRejectionReason As Domain.Entities.AccountPayableRejectionReason, audit As AuditMessage) As Domain.Base.Entities.ActionResult Implements IPaymentsAccountPayableRejectionReason.DeleteAccountPayableRejectionReason
        Using service As IAccountPayableRejectionReasonAdminService = Container.Current.Resolve(Of IAccountPayableRejectionReasonAdminService)()
            Return service.DeleteAccountPayableRejectionReason(AccountPayableRejectionReason, audit)
        End Using
        'Return Me._accountPayableRejectionReasonAdminService.DeleteAccountPayableRejectionReason(AccountPayableRejectionReason, audit)
    End Function

    ''' <summary>
    ''' Obtiene un rechazo de factura
    ''' </summary>
    ''' <param name="code"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetAccountPayableRejectionReason(code As String, audit As AuditMessage) As Domain.Base.Entities.ActionResult(Of Domain.Entities.AccountPayableRejectionReason) Implements IPaymentsAccountPayableRejectionReason.GetAccountPayableRejectionReason
        Using service As IAccountPayableRejectionReasonAdminService = Container.Current.Resolve(Of IAccountPayableRejectionReasonAdminService)()
            Return service.GetAccountPayableRejectionReason(code, audit)
        End Using
        'Return Me._accountPayableRejectionReasonAdminService.GetAccountPayableRejectionReason(code, audit)
    End Function

    ''' <summary>
    ''' Guarda o actualiza un rechazo de factura
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SaveAccountPayableRejectionReason(AccountPayableRejectionReason As Domain.Entities.AccountPayableRejectionReason, audit As AuditMessage, idSequense As Int64) As Domain.Base.Entities.ActionResult(Of Domain.Entities.AccountPayableRejectionReason) Implements IPaymentsAccountPayableRejectionReason.SaveAccountPayableRejectionReason
        Using service As IAccountPayableRejectionReasonAdminService = Container.Current.Resolve(Of IAccountPayableRejectionReasonAdminService)()
            Return service.SaveAccountPayableRejectionReason(AccountPayableRejectionReason, audit, idSequense)
        End Using
        'Return Me._accountPayableRejectionReasonAdminService.SaveAccountPayableRejectionReason(AccountPayableRejectionReason, audit, idSequense)
    End Function

    ''' <summary>
    ''' Cambia el estado de la entidad
    ''' </summary>
    ''' <param name="code"></param>
    ''' <param name="state"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ChangeStateAccountPayableRejectionReason(code As String, state As Boolean, audit As AuditMessage) As Domain.Base.Entities.ActionResult(Of Domain.Entities.AccountPayableRejectionReason) Implements IPaymentsAccountPayableRejectionReason.ChangeStateAccountPayableRejectionReason
        Using service As IAccountPayableRejectionReasonAdminService = Container.Current.Resolve(Of IAccountPayableRejectionReasonAdminService)()
            Return service.ChangeState(code, state, audit)
        End Using
        'Return Me._accountPayableRejectionReasonAdminService.ChangeState(code, state, audit)
    End Function

    ''' <summary>
    ''' Consulta el rechazo de factura
    ''' </summary>
    ''' <param name="id"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetAccountPayableRejectionReasonById(id As String, audit As AuditMessage) As Domain.Base.Entities.ActionResult(Of Domain.Entities.AccountPayableRejectionReason) Implements IPaymentsAccountPayableRejectionReason.GetAccountPayableRejectionReasonById
        Using service As IAccountPayableRejectionReasonAdminService = Container.Current.Resolve(Of IAccountPayableRejectionReasonAdminService)()
            Return service.GetAccountPayableRejectionReasonById(id, audit)
        End Using
        'Return Me._accountPayableRejectionReasonAdminService.GetAccountPayableRejectionReasonById(id, audit)
    End Function
    ''' <summary>
    ''' Lista las razones de rechazo
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListRejectionReason(audit As AuditMessage) As List(Of Domain.Entities.AccountPayableRejectionReason) Implements IPaymentsAccountPayableRejectionReason.ListRejectionReason
        Using service As IAccountPayableRejectionReasonAdminService = Container.Current.Resolve(Of IAccountPayableRejectionReasonAdminService)()
            Return service.ListRejectionReason()
        End Using
        'Return Me._accountPayableRejectionReasonAdminService.ListRejectionReason()
    End Function
End Class
