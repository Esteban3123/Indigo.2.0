'***********************************************************************
' Assembly         : DistributedServices.Treasury
' Author           : Diego Andrés Roldán lozano
' Created          : 02-04-2014
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
    Implements ITreasuryServiceRefund

    ''' <summary>
    ''' Elimina un reembolso
    ''' </summary>
    ''' <param name="refund"></param>
    ''' <returns></returns>
    Public Function DeleteRefund(refund As Refunds, audit As AuditMessage) As ActionResult Implements ITreasuryServiceRefund.DeleteRefund
        Using service As IRefundAdminService = Container.Current.Resolve(Of IRefundAdminService)()
            Return service.DeleteRefund(refund, audit)
        End Using
        'Return Me._refundAdminService.DeleteRefund(refund, audit)
    End Function

    ''' <summary>
    ''' obtiene un reembolso por codigo
    ''' </summary>
    ''' <param name="code">The code.</param>
    ''' <returns></returns>
    Public Function GetRefund(code As String, audit As AuditMessage) As ActionResult(Of Refunds) Implements ITreasuryServiceRefund.GetRefund
        Using service As IRefundAdminService = Container.Current.Resolve(Of IRefundAdminService)()
            Return service.GetRefund(code, audit)
        End Using
        'Return Me._refundAdminService.GetRefund(code, audit)
    End Function

    ''' <summary>
    ''' Obtiene un reembolso por id
    ''' </summary>
    ''' <param name="id">The identifier.</param>
    ''' <returns></returns>
    Public Function GetRefundById(id As Integer) As Refunds Implements ITreasuryServiceRefund.GetRefundById
        Using service As IRefundAdminService = Container.Current.Resolve(Of IRefundAdminService)()
            Return service.GetRefundById(id)
        End Using
        'Return Me._refundAdminService.GetRefundById(id)
    End Function

    ''' <summary>
    ''' Guarda un reembolso
    ''' </summary>
    ''' <param name="refund">The refund.</param>
    ''' <returns></returns>
    Public Function SaveRefund(refund As Refunds, withConfirm As Boolean, audit As AuditMessage, idSequence As Int64) As ActionResult(Of Refunds) Implements ITreasuryServiceRefund.SaveRefund
        Using service As IRefundAdminService = Container.Current.Resolve(Of IRefundAdminService)()
            Return service.SaveRefund(refund, audit, withConfirm, idSequence)
        End Using
        'Return Me._refundAdminService.SaveRefund(refund, audit, withConfirm, idSequence)
    End Function

    ''' <summary>
    ''' Confirms the specified identifier refund.
    ''' </summary>
    ''' <param name="IdRefund">The identifier refund.</param>
    ''' <returns></returns>
    Public Function ConfirmRefund(IdRefund As Integer, audit As AuditMessage, idSequence As Int64) As ActionResult(Of String) Implements ITreasuryServiceRefund.ConfirmRefund
        Using service As IRefundAdminService = Container.Current.Resolve(Of IRefundAdminService)()
            Return service.ConfirmRefund(IdRefund, audit, idSequence)
        End Using
        'Return Me._refundAdminService.ConfirmRefund(IdRefund, audit, idSequence)
    End Function

    ''' <summary>
    ''' lista los reembolsos asociados a una cuenta contable
    ''' </summary>
    ''' <param name="IdMainAccount">The identifier main account.</param>
    ''' <returns></returns>
    Public Function ListRefundByAccount(IdMainAccount As Integer, audit As AuditMessage) As ActionResult(Of List(Of Refunds)) Implements ITreasuryServiceRefund.ListRefundByAccount
        Using service As IRefundAdminService = Container.Current.Resolve(Of IRefundAdminService)()
            Return service.ListRefundByAccount(IdMainAccount, audit)
        End Using
        'Return Me._refundAdminService.ListRefundByAccount(IdMainAccount, audit)
    End Function

    ''' <summary>
    ''' Lista los reembolsos hechos a una caja
    ''' </summary>
    ''' <param name="CashRegisterId"></param>
    ''' <returns></returns>
    Public Function ListRefundByCashRegisterId(CashRegisterId As Integer) As ActionResult(Of List(Of Refunds)) Implements ITreasuryServiceRefund.ListRefundByCashRegisterId
        Using service As IRefundAdminService = Container.Current.Resolve(Of IRefundAdminService)()
            Return service.ListRefundByCashRegisterId(CashRegisterId)
        End Using
        'Return Me._refundAdminService.ListRefundByCashRegisterId(CashRegisterId)
    End Function

    ''' <summary>
    ''' Gets the refund detail by voucher transaction identifier.
    ''' </summary>
    ''' <param name="voucherTransactionId"></param>
    ''' <returns></returns>
    Public Function GetRefundDetailByVoucherTransactionId(voucherTransactionId As Integer) As RefundDetail Implements ITreasuryServiceRefund.GetRefundDetailByVoucherTransactionId
        Using service As IRefundAdminService = Container.Current.Resolve(Of IRefundAdminService)()
            Return service.GetRefundDetailByVoucherTransactionId(voucherTransactionId)
        End Using
        'Return Me._refundAdminService.GetRefundDetailByVoucherTransactionId(voucherTransactionId)
    End Function
End Class
