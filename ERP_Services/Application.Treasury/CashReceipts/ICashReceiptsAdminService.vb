'***********************************************************************
' Assembly         : Application.Treasury
' Author           : Carlos Ernesto Cordoba
' Created          : 04-07-2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities

Public Interface ICashReceiptsAdminService
    Inherits IDisposable

    Function ReverseCashReceipt(cashReceiptId As Integer, audit As AuditMessage, treasuryNote As TreasuryNote) As ActionResult(Of String)

    ''' <summary>
    ''' metodo para obtener el recibo de caja por codigo
    ''' </summary>
    ''' <param name="code">The code.</param>
    ''' <param name="audit">The audit.</param>
    ''' <returns></returns>
    Function GetCashReceiptsByCode(code As String, audit As AuditMessage) As ActionResult(Of CashReceipts)

    ''' <summary>
    ''' metodo para obtener el recibo de caja por id
    ''' </summary>
    ''' <param name="id">The identifier.</param>
    ''' <returns></returns>
    Function GetCashReceiptsById(id As Integer) As CashReceipts

    ''' <summary>
    ''' metodo para obtener los detalles del los recibos de caja
    ''' </summary>
    ''' <param name="idCashReceipt"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetCashReceiptDetailsByIdCashReceipt(idCashReceipt As Integer) As List(Of CashReceiptDetails)
    ''' <summary>
    ''' metodo para obtener los metodos de pago del recibo de caja
    ''' </summary>
    ''' <param name="idCashReceipt"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetPaymentMethodsByIdCashReceip(idCashReceipt As Integer) As List(Of PaymentMethods)

    ''' <summary>
    ''' metodo para guardar un recibo de caja
    ''' </summary>
    ''' <param name="CashReceipts">The cash receipts.</param>
    ''' <param name="audit">The audit.</param>
    ''' <param name="idSequence">The identifier secuence.</param>
    ''' <returns></returns>
    Function SaveCashReceipts(CashReceipts As CashReceipts, audit As AuditMessage, Optional idSequence As Integer = 0, Optional ByVal sequenceC As TreasurySequence = Nothing) As ActionResult(Of CashReceipts)
    ''' <summary>
    ''' metodo para confirmar un recibo de caja
    ''' </summary>   
    ''' <returns></returns>
    Function ConfirmCashReceipts(idCashReceipts As Integer, audit As AuditMessage, IndigoSessionValues As SessionValues, Optional CashReceipts As CashReceipts = Nothing) As ActionResult(Of String)
    ''' <summary>
    ''' metodo para guardar y confirmar el registro
    ''' </summary>
    ''' <param name="CashReceipts"></param>
    ''' <param name="audit"></param>
    ''' <param name="idSequence"></param>
    ''' <returns></returns>
    Function SaveAndConfirm(CashReceipts As CashReceipts, audit As AuditMessage, IndigoSessionValues As SessionValues, Optional idSequence As Integer = 0, Optional ByVal sequenceC As TreasurySequence = Nothing) As ActionResult(Of CashReceipts)
    ''' <summary>
    ''' metodo para eliminar un recibo de caja
    ''' </summary>
    ''' <param name="CashReceipts">The cash receipts.</param>
    ''' <param name="audit">The audit.</param>
    ''' <returns></returns>
    Function DeleteCashReceipts(CashReceipts As CashReceipts, audit As AuditMessage) As ActionResult

    Function ValidateCostcenterPaymentMethod(PaymentMethods As PaymentMethods, operatingUnitId As Integer) As ActionResult
    ''' <summary>
    ''' metodo para obtener las facturas del tercero por los datos que se pegaron en la rejilla
    ''' </summary>
    ''' <param name="data"></param>
    ''' <param name="idThirdPaty"></param>
    ''' <param name="idMainAccount"></param>
    ''' <param name="idCostCenter"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function SetBillsCashReceipts(data As List(Of List(Of String)), idThirdPaty As Integer, idMainAccount As Integer, idCostCenter As Integer, idOperatingUnit As Integer) As ActionResult(Of List(Of AccountReceivable))

    ''' <summary>
    ''' Meodo para crear y confirmar un recibo de caja
    ''' </summary>
    ''' <param name="invoiceNumber"></param>
    ''' <param name="value"></param>
    ''' <param name="documentDate"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GenerateCashReceiptsREST(invoiceNumber As String, value As Decimal, documentDate As DateTime) As ActionResult

End Interface
