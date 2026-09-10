'***********************************************************************
' Assembly         : DistributedServices.Treasury
' Author           : Diego Andrés Roldán Lozano
' Created          : 13-06-2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities
Imports System.ServiceModel

<ServiceContract()>
Public Interface ITreasuryServiceVoucherTransaction

    ''' <summary>
    ''' Guarda un comprobante de egreso
    ''' </summary>
    ''' <returns></returns>
    <OperationContract()>
    Function SaveVoucherTransaction(voucherTransaction As Domain.Entities.VoucherTransaction, withConfirm As Boolean, idSequence As Int64, sequenceC As Domain.Entities.TreasurySequence, audit As AuditMessage) As Domain.Base.Entities.ActionResult(Of Domain.Entities.VoucherTransaction)

    ''' <summary>
    ''' Confirma un comprobante de egreso
    ''' </summary>
    ''' <returns></returns>
    <OperationContract()>
    Function ConfirmVoucherTransaction(idVoucherTransaction As Integer, idSequence As Int64, audit As AuditMessage) As ActionResult(Of String)

    ''' <summary>
    ''' Obtiene un comprobante de egreso por codigo
    ''' </summary>
    <OperationContract()>
    Function GetVoucherTransaction(code As String, audit As AuditMessage) As ActionResult(Of VoucherTransaction)

    ''' <summary>
    ''' Obtiene un comprobante de egreso por id
    ''' </summary>
    ''' <param name="Id">The identifier.</param>
    ''' <returns></returns>
    <OperationContract()>
    Function GetVoucherTransactionById(ByVal Id As Integer) As ActionResult(Of VoucherTransaction)

    ''' <summary>
    ''' Obtiene un avance de tesoreria por Id
    ''' </summary>
    ''' <param name="Id">The identifier.</param>
    ''' <returns></returns>
    <OperationContract()>
    Function GetTreasuryAdvanceById(ByVal Id As Integer) As ActionResult(Of TreasuryAdvances)

    ''' <summary>
    ''' Obtiene un avance de tesoreria por IdVoucherTransactionDetail
    ''' </summary>
    ''' <param name="IdVoucherTransactionDetail">The identifier voucher transaction detail.</param>
    ''' <returns></returns>
    <OperationContract()>
    Function GetTreasuryAdvanceByIdVoucherTransactionDetail(ByVal IdVoucherTransactionDetail As Integer) As ActionResult(Of TreasuryAdvances)

    ''' <summary>
    ''' Obtiene un listado de comprobantes de egreso que esten dentro de un rango de fecha y que no esten reembolsados 
    ''' </summary>
    ''' <returns></returns>
    <OperationContract()>
    Function ListVoucherTransactionBetweenDateNotRefundExpenseType(CashRegisterId As Integer, InitialDate As Date, FinalDate As Date, ExpenseType As Byte, audit As AuditMessage) As ActionResult(Of List(Of VoucherTransaction))

    ''' <summary>
    ''' Lists the type of the voucher transaction final date not refund expense.
    ''' </summary>
    ''' <param name="CashRegisterId">The cash register identifier.</param>
    ''' <param name="FinalDate">The final date.</param>
    ''' <param name="ExpenseType">Type of the expense.</param>
    ''' <returns></returns>
    <OperationContract()>
    Function ListVoucherTransactionFinalDateNotRefundExpenseType(CashRegisterId As Integer, FinalDate As Date, ExpenseType As Byte, audit As AuditMessage) As ActionResult(Of List(Of VoucherTransaction))

    ''' <summary>
    ''' Obtiene un comprobante de egreso por número de cheque
    ''' </summary>
    ''' <param name="Id">checkNumber</param>
    ''' <returns></returns>
    <OperationContract()>
    Function GetVoucherTransactionByCheckNumber(ByVal checkNumber As Long) As ActionResult(Of VoucherTransaction)

    ''' <summary>
    ''' Gets the account payable in note and advance.
    ''' </summary>
    ''' <param name="listAccountPayableShareId">The list account payable share identifier.</param>
    ''' <returns></returns>
    <OperationContract()>
    Function GetAccountPayableInNoteAndAdvance(listAccountPayableShareId As List(Of Integer), voucherTransactionCode As String) As ActionResult(Of List(Of String))

    <OperationContract()> _
    Function GetCheckNumber(entitybanckAccountId As Integer, OperatingUnitId As Integer, UserCode As String) As SP_GetCheckNumber_Result

End Interface
