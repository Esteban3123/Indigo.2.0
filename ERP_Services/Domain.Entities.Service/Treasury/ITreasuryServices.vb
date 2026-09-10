Imports Domain.Base.Entities

Public Interface ITreasuryServices
    Inherits IDisposable

#Region "METHODS"

    Function SetDocumentsCrossingImportFile(data As List(Of ImportFileRow), idThirdPaty As Integer, crossingType As Integer, processType As Integer) As ActionResult(Of List(Of CrossingAccountDetailCxP), List(Of CrossingAccountDetailCxC))
    ''' <summary>
    '''  establece las cuentas por cobrar del copiar y pegar
    ''' </summary>
    ''' <param name="data">Listado que se va a procesar</param>
    ''' <param name="idThirdPaty"></param>
    ''' <param name="crossingType">1-Mismo Tercero, 2-Diferente Tercero</param>
    ''' <param name="processType">1 - CxP, 2 - CxC </param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function SetDocumentsCrossingCopyPaste(data As List(Of List(Of String)), idThirdPaty As Integer, crossingType As Integer, processType As Integer) As ActionResult(Of List(Of CrossingAccountDetailCxP), List(Of CrossingAccountDetailCxC))

    ''' <summary>
    ''' Validates the cash receipts.
    ''' </summary>
    ''' <param name="cashReceipt">The cash receipt.</param>
    ''' <param name="confirm">if set to <c>true</c> [confirm].</param>
    ''' <returns></returns>
    Function ValidateCashReceipts(cashReceipt As CashReceipts, userId As Integer, codeUser As String, Optional confirm As Boolean = False) As ActionResult(Of String)

    ''' <summary>
    ''' Sets the bills cash receipts.
    ''' </summary>
    ''' <param name="data">The data.</param>
    ''' <param name="idThirdPaty">The identifier third paty.</param>
    ''' <param name="idMainAccount">The identifier main account.</param>
    ''' <param name="idCostCenter">The identifier cost center.</param>
    ''' <param name="idOperatingUnit">The identifier operating unit.</param>
    ''' <returns></returns>
    Function SetBillsCashReceipts(data As List(Of List(Of String)), idThirdPaty As Integer, idMainAccount As Integer, idCostCenter As Integer, idOperatingUnit As Integer) As ActionResult(Of List(Of AccountReceivable))

    ''' <summary>
    ''' Validates the voucher transaction.
    ''' </summary>
    ''' <param name="voucherTransaction">The voucher transaction.</param>
    ''' <returns></returns>
    Function ValidateVoucherTransaction(ByVal voucherTransaction As VoucherTransaction) As ActionResult(Of String)

    ''' <summary>
    ''' Validates the voucher transaction save.
    ''' </summary>
    ''' <param name="voucherTransaction">The voucher transaction.</param>
    ''' <returns></returns>
    Function ValidateVoucherTransactionSave(ByVal voucherTransaction As VoucherTransaction) As ActionResult(Of String)

    ''' <summary>
    ''' Validates the crossing account save.
    ''' </summary>
    ''' <param name="crossingAccount">The crossing account.</param>
    ''' <returns></returns>
    Function ValidateCrossingAccountSave(ByVal crossingAccount As CrossingAccount) As ActionResult(Of String)

    ''' <summary>
    ''' Validates the crossing account confirm.
    ''' </summary>
    ''' <param name="crossingAccount">The crossing account.</param>
    ''' <returns></returns>
    Function ValidateCrossingAccountConfirm(ByVal crossingAccount As CrossingAccount) As ActionResult(Of CrossingAccount)

    ''' <summary>
    ''' Validates the schedule payment.
    ''' </summary>
    ''' <param name="schedulePayment">The schedule payment.</param>
    ''' <returns></returns>
    Function ValidateSchedulePayment(ByVal schedulePayment As List(Of SP_SchedulePayment_Result)) As ActionResult(Of String)

    Function ValidateSchedulePayment(ByVal schedulePaymentDetail As List(Of SchedulePaymentDetail)) As ActionResult(Of String)

    ''' <summary>
    ''' Validates the consignment save.
    ''' </summary>
    ''' <param name="consignment">The consignment.</param>
    ''' <returns></returns>
    Function ValidateConsignmentSave(consignment As Consignment) As ActionResult(Of String)

    ''' <summary>
    ''' Validates the consignment confirm.
    ''' </summary>
    ''' <param name="consigment">The consigment.</param>
    ''' <returns></returns>
    Function ValidateConsignmentConfirm(consigment As Consignment) As ActionResult(Of Consignment)

    ''' <summary>
    ''' Validates the treasury note save.
    ''' </summary>
    ''' <param name="treasuryNote">The treasury note.</param>
    ''' <returns></returns>
    Function ValidateTreasuryNoteSave(ByVal treasuryNote As TreasuryNote) As ActionResult

    ''' <summary>
    ''' Validates the treasury note confirm.
    ''' </summary>
    ''' <param name="treasuryNote">The treasury note.</param>
    ''' <returns></returns>
    Function ValidateTreasuryNoteConfirm(ByVal treasuryNote As TreasuryNote) As ActionResult
    ''' <summary>
    ''' metodo para generar el archivo para pagos en bancos
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GenerateBankFile(SchedulePayment As SchedulePayment, bankId As Integer, companyNIT As String, companyName As String, Optional optionalParameters As List(Of String) = Nothing) As ActionResult(Of String)
#End Region

End Interface
