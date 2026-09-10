'***********************************************************************
' Assembly         : Domain.Treasury
' Author           : Diego Andrés Roldán Lozano
' Created          : 18-07-2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************
Imports Domain.Base
Imports Domain.Entities

Public Interface IRefundRepository
    Inherits IRepository(Of Refunds)

    Function ListRefundMassiveConfirm(listDocuments As List(Of String)) As List(Of Refunds)

    ''' <summary>
    ''' obtiene un reembolso por codigo
    ''' </summary>
    ''' <param name="code">The code.</param>
    ''' <returns></returns>
    Function GetRefund(ByVal code As String) As Refunds
    
    ''' <summary>
    ''' Obtiene un reembolso por id
    ''' </summary>
    ''' <param name="id">The identifier.</param>
    ''' <returns></returns>
    Function GetRefundById(id As Integer) As Refunds

    ''' <summary>
    ''' Valida si el reembolso esta con estado anulado
    ''' </summary>
    ''' <param name="id">The identifier.</param>
    ''' <returns></returns>
    Function ValidateRefundById(id As Integer) As Refunds

    ''' <summary>
    ''' Gets the refund detail by voucher transaction identifier.
    ''' </summary>
    Function GetRefundDetailByVoucherTransactionId(voucherTransactionId As Integer) As RefundDetail

    ''' <summary>
    ''' lista los reembolsos asociados a una cuenta contable
    ''' </summary>
    ''' <param name="IdMainAccount">The identifier main account.</param>
    ''' <returns></returns>
    Function ListRefundByAccount(ByVal IdMainAccount As Integer) As List(Of Refunds)

    ''' <summary>
    ''' Lista los reembolsos hechos a una caja
    ''' </summary>
    Function ListRefundByCashRegisterId(ByVal CashRegisterId As Integer, Optional getRefundWithRefunded As Boolean = False) As List(Of Refunds)

    ''' <summary>
    ''' Valida si hay reembolso con la misma caja sin confirmar
    ''' </summary>
    ''' <param name="CashRegisterId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function ValidateRefundRegister(CashRegisterId As Integer) As Boolean

    ''' <summary>
    ''' Valida si hay reembolso con la misma caja confirmado y con refunded en false
    ''' </summary>
    ''' <param name="CashRegisterId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function ValidateRefundConfirmed(CashRegisterId As Integer) As Boolean

End Interface