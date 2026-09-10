'***********************************************************************
' Assembly         : Domain.Treasury
' Author           : Diego Andrés Roldán Lozano
' Created          : 13-06-2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************
Imports Domain.Base
Imports Domain.Entities

Public Interface IVoucherTransactionRepository
    Inherits IRepository(Of VoucherTransaction)

    Function GenerateVoucherTransactionSP(voucherTransactionXml As String, UserCode As String) As Entity.Core.Objects.ObjectResult(Of SP_SaveVoucherTransaction_Result)

    Function ListVoucherTransactionMassiveConfirm(listDocuments As List(Of String)) As List(Of VoucherTransaction)

    Function ListVoucherTransactionFinalDateNotRefundExpenseType(CashRegisterId As Integer, FinalDate As Date, ExpenseType As Byte) As List(Of VoucherTransaction)

    ''' <summary>
    ''' Obtiene un comprobante de egreso por codigo
    ''' </summary>
    Function GetVoucherTransaction(ByVal code As String) As VoucherTransaction

    ''' <summary>
    ''' Obtiene un comprobante de egreso por id
    ''' </summary>
    ''' <param name="Id">The identifier.</param>
    ''' <returns></returns>
    Function GetVoucherTransactionById(ByVal Id As Integer, Optional tracking As Boolean = True) As VoucherTransaction

    ''' <summary>
    ''' Obtiene el comprobante de egreso para realizar validaciones
    ''' </summary>
    ''' <param name="Id"></param>
    ''' <returns></returns>
    Function GetVoucherTransactionSimpleById(Id As Integer) As VoucherTransaction

    ''' <summary>
    ''' Obtiene un listado de comprobantes de egreso que esten dentro de un rango de fecha, tipo de egreso y que no esten reembolsados 
    ''' </summary>
    ''' <returns></returns>
    Function ListVoucherTransactionBetweenDateNotRefundExpenseType(ByVal CashRegisterId As Integer, ByVal InitialDate As DateTime, ByVal FinalDate As DateTime, ByVal ExpenseType As Byte) As List(Of VoucherTransaction)

    ''' <summary>
    ''' obtiene un listado de comprobantes de egreso por el id del reembolso
    ''' </summary>
    ''' <param name="IdRefund">The identifier refund.</param>
    ''' <returns></returns>
    Function ListVoucherTransactionByIdRefund(ByVal IdRefund As Integer) As List(Of VoucherTransaction)

    ''' <summary>
    ''' Obtiene un comprobante de egreso por número de cheque
    ''' </summary>
    Function GetVoucherTransactionByCheckNumber(ByVal checkNumber As Long) As VoucherTransaction

    ''' <summary>
    ''' Gets the voucher transaction by account payable list identifier.
    ''' </summary>
    ''' <param name="listAccountPayableId">The list account payable identifier.</param>
    ''' <returns></returns>
    Function GetVoucherTransactionByAccountPayableListId(listAccountPayableId As List(Of Integer)) As List(Of String)

    Function GetCheckNumber(entitybanckAccountId As Integer, OperatingUnitId As Integer, UserCode As String) As SP_GetCheckNumber_Result

    Function SP_ReverseVoucherTransaction(TreasuryNoteId As Integer, UserCode As String) As List(Of SP_ReverseVoucherTransaction_Result)

End Interface
