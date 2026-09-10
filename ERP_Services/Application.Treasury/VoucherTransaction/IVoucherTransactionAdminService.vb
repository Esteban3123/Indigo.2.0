'***********************************************************************
' Assembly         : Application.Treasury
' Author           : Diego Andrés Roldán Lozano
' Created          : 13-06-2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities

Public Interface IVoucherTransactionAdminService
    Inherits IDisposable

    Function ListVoucherTransactionFinalDateNotRefundExpenseType(CashRegisterId As Integer, FinalDate As Date, ExpenseType As Byte, audit As AuditMessage) As ActionResult(Of List(Of VoucherTransaction))

    ''' <summary>
    ''' Guarda un comprobante de egreso
    ''' </summary>
    ''' <returns></returns>
    Function SaveVoucherTransaction(ByVal voucherTransaction As VoucherTransaction, ByVal audit As AuditMessage, ByVal withConfirm As Boolean, Optional ByVal idSequence As Int64 = 0, Optional ByVal sequenceC As TreasurySequence = Nothing, Optional ByVal returnWithTracking As Boolean = True) As ActionResult(Of VoucherTransaction)

    ''' <summary>
    ''' Confirma un comprobante de egreso
    ''' </summary>
    ''' <returns></returns>
    Function ConfirmVoucherTransaction(ByVal IdvoucherTransaction As Integer, ByVal audit As AuditMessage, Optional ByVal voucherTransaction As VoucherTransaction = Nothing, Optional ByVal idSequence As Int64 = 0) As ActionResult(Of String)

    ''' <summary>
    ''' Hace reversión del comprobante de egreso
    ''' </summary>
    ''' <returns></returns>
    Function DisconfirmVoucherTransaction(treasuryNote As TreasuryNote, audit As AuditMessage) As ActionResult(Of String)

    ''' <summary>
    ''' Obtiene un comprobante de egreso por codigo
    ''' </summary>
    Function GetVoucherTransaction(ByVal code As String, ByVal audit As AuditMessage) As ActionResult(Of VoucherTransaction)

    ''' <summary>
    ''' Obtiene un comprobante de egreso por id
    ''' </summary>
    ''' <param name="Id">The identifier.</param>
    ''' <returns></returns>
    Function GetVoucherTransactionById(ByVal Id As Integer) As ActionResult(Of VoucherTransaction)

    ''' <summary>
    ''' Obtiene un avance de tesoreria por Id
    ''' </summary>
    ''' <param name="Id">The identifier.</param>
    ''' <returns></returns>
    Function GetTreasuryAdvanceById(ByVal Id As Integer) As ActionResult(Of TreasuryAdvances)

    ''' <summary>
    ''' Obtiene un avance de tesoreria por IdVoucherTransactionDetail
    ''' </summary>
    ''' <param name="IdVoucherTransactionDetail">The identifier voucher transaction detail.</param>
    ''' <returns></returns>
    Function GetTreasuryAdvanceByIdVoucherTransactionDetail(ByVal IdVoucherTransactionDetail As Integer) As ActionResult(Of TreasuryAdvances)

    ''' <summary>
    ''' Obtiene un listado de comprobantes de egreso que esten dentro de un rango de fecha, tipo de egreso y que no esten reembolsados 
    ''' </summary>
    ''' <returns></returns>
    Function ListVoucherTransactionBetweenDateNotRefundExpenseType(ByVal CashRegisterId As Integer, ByVal InitialDate As DateTime, ByVal FinalDate As DateTime, ByVal ExpenseType As Byte, ByVal audit As AuditMessage) As ActionResult(Of List(Of VoucherTransaction))

    ''' <summary>
    ''' obtiene un listado de comprobantes de egreso por el id del reembolso
    ''' </summary>
    ''' <param name="IdRefund">The identifier refund.</param>
    ''' <returns></returns>
    Function ListVoucherTransactionByIdRefund(ByVal IdRefund As Integer, ByVal audit As AuditMessage) As ActionResult(Of List(Of VoucherTransaction))

    ''' <summary>
    ''' Obtiene un comprobante de egreso por número de cheque
    ''' </summary>
    Function GetVoucherTransactionByCheckNumber(ByVal checkNumber As Long) As ActionResult(Of VoucherTransaction)

    ''' <summary>
    ''' Gets the account payable in note and advance.
    ''' </summary>
    ''' <param name="listAccountPayableShareId">The list account payable share identifier.</param>
    ''' <returns></returns>
    Function GetAccountPayableInNoteAndAdvance(listAccountPayableShareId As List(Of Integer), voucherTransactionCode As String) As ActionResult(Of List(Of String))

    Function GetCheckNumber(entitybanckAccountId As Integer, OperatingUnitId As Integer, UserCode As String) As SP_GetCheckNumber_Result

End Interface
