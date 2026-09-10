'***********************************************************************
' Assembly         : Application.Treasury
' Author           : Diego Andrés Roldán Lozano
' Created          : 18-07-2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities

Public Interface IRefundAdminService
    Inherits IDisposable

    ''' <summary>
    ''' Guarda un reembolso
    ''' </summary>
    ''' <param name="refund">The refund.</param>
    ''' <param name="audit">The audit.</param>
    ''' <param name="idSequence">The identifier sequence.</param>
    ''' <returns></returns>
    Function SaveRefund(ByVal refund As Refunds, ByVal audit As AuditMessage, ByVal withConfirm As Boolean, Optional ByVal idSequence As Int64 = 0, Optional ValidateRefund As Boolean = True) As ActionResult(Of Refunds)

    ''' <summary>
    ''' Confirms the specified identifier refund.
    ''' </summary>
    ''' <param name="IdRefund">The identifier refund.</param>
    ''' <param name="audit">The audit.</param>
    ''' <param name="idSequence">The identifier sequence.</param>
    ''' <returns></returns>
    Function ConfirmRefund(ByVal IdRefund As Integer, ByVal audit As AuditMessage, Optional ByVal idSequence As Int64 = 0, Optional refund As Refunds = Nothing) As ActionResult(Of String)

    ''' <summary>
    ''' Elimina un reembolso
    ''' </summary>
    ''' <param name="refund">The refund.</param>
    ''' <param name="audit">The audit.</param>
    ''' <returns></returns>
    Function DeleteRefund(ByVal refund As Refunds, ByVal audit As AuditMessage) As ActionResult

    ''' <summary>
    ''' obtiene un reembolso por codigo
    ''' </summary>
    ''' <param name="code">The code.</param>
    ''' <returns></returns>
    Function GetRefund(ByVal code As String, ByVal audit As AuditMessage) As ActionResult(Of Refunds)

    ''' <summary>
    ''' Gets the refund detail by voucher transaction identifier.
    ''' </summary>
    Function GetRefundDetailByVoucherTransactionId(voucherTransactionId As Integer) As RefundDetail

    ''' <summary>
    ''' Obtiene un reembolso por id
    ''' </summary>
    ''' <param name="id">The identifier.</param>
    ''' <returns></returns>
    Function GetRefundById(id As Integer) As Refunds

    ''' <summary>
    ''' lista los reembolsos asociados a una cuenta contable
    ''' </summary>
    ''' <param name="IdMainAccount">The identifier main account.</param>
    ''' <returns></returns>
    Function ListRefundByAccount(ByVal IdMainAccount As Integer, ByVal audit As AuditMessage) As ActionResult(Of List(Of Refunds))

    ''' <summary>
    ''' Lista los reembolsos hechos a una caja
    ''' </summary>
    Function ListRefundByCashRegisterId(ByVal CashRegisterId As Integer, Optional getRefundWithRefunded As Boolean = False) As ActionResult(Of List(Of Refunds))

End Interface