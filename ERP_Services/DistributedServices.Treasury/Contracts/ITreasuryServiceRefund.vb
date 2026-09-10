'***********************************************************************
' Assembly         : DistributedServices.Treasury
' Author           : Diego Andrés Roldán Lozano
' Created          : 18-07-2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities
Imports System.ServiceModel

<ServiceContract()>
Public Interface ITreasuryServiceRefund

    ''' <summary>
    ''' Guarda un reembolso
    ''' </summary>
    ''' <param name="refund">The refund.</param>
    ''' <returns></returns>
    <OperationContract()>
    Function SaveRefund(refund As Refunds, withConfirm As Boolean, audit As AuditMessage, idSequence As Int64) As ActionResult(Of Refunds)

    ''' <summary>
    ''' Confirms the specified identifier refund.
    ''' </summary>
    ''' <param name="IdRefund">The identifier refund.</param>
    ''' <returns></returns>
    <OperationContract()>
    Function ConfirmRefund(IdRefund As Integer, audit As AuditMessage, idSequence As Int64) As ActionResult(Of String)

    ''' <summary>
    ''' Elimina un reembolso
    ''' </summary>
    ''' <returns></returns>
    <OperationContract()>
    Function DeleteRefund(refund As Refunds, audit As AuditMessage) As ActionResult

    ''' <summary>
    ''' obtiene un reembolso por codigo
    ''' </summary>
    ''' <param name="code">The code.</param>
    ''' <returns></returns>
    <OperationContract()>
    Function GetRefund(code As String, audit As AuditMessage) As ActionResult(Of Refunds)

    ''' <summary>
    ''' Obtiene un reembolso por id
    ''' </summary>
    ''' <param name="id">The identifier.</param>
    ''' <returns></returns>
    <OperationContract()>
    Function GetRefundById(id As Integer) As Refunds

    ''' <summary>
    ''' Gets the refund detail by voucher transaction identifier.
    ''' </summary>
    <OperationContract()>
    Function GetRefundDetailByVoucherTransactionId(voucherTransactionId As Integer) As RefundDetail

    ''' <summary>
    ''' lista los reembolsos asociados a una cuenta contable
    ''' </summary>
    ''' <param name="IdMainAccount">The identifier main account.</param>
    ''' <returns></returns>
    <OperationContract()>
    Function ListRefundByAccount(IdMainAccount As Integer, audit As AuditMessage) As ActionResult(Of List(Of Refunds))

    ''' <summary>
    ''' Lista los reembolsos hechos a una caja
    ''' </summary>
    <OperationContract()>
    Function ListRefundByCashRegisterId(ByVal CashRegisterId As Integer) As ActionResult(Of List(Of Refunds))

End Interface