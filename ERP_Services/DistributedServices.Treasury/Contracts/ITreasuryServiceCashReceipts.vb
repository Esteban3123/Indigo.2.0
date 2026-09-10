'***********************************************************************
' Assembly         : DistributedServices.Treasury
' Author           : Carlos Ernesto Cordoba
' Created          : 04-07-2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities
Imports System.ServiceModel

<ServiceContract()>
Public Interface ITreasuryServiceCashReceipts

    ''' <summary>
    ''' obtiene el recibo de caja por codigo
    ''' </summary>
    ''' <param name="code">The code.</param>
    ''' <returns></returns>
    <OperationContract()>
    Function GetCashReceiptsByCode(code As String, audit As AuditMessage) As ActionResult(Of CashReceipts)

    ''' <summary>
    ''' obtiene el recibo de caja por id
    ''' </summary>
    ''' <param name="id">The identifier.</param>
    ''' <returns></returns>
    <OperationContract()>
    Function GetCashReceiptsById(id As Integer) As CashReceipts

    ''' <summary>
    ''' guarda el recibo de caja
    ''' </summary>
    ''' <param name="CashReceipts">The cash receipts.</param>
    ''' <returns></returns>
    <OperationContract()>
    Function SaveCashReceipts(CashReceipts As CashReceipts, idSequence As Int64, audit As AuditMessage, sequenceC As Domain.Entities.TreasurySequence) As ActionResult(Of CashReceipts)

    ''' <summary>
    ''' Saves the and confirm.
    ''' </summary>
    ''' <param name="CashReceipts">The cash receipts.</param>
    ''' <returns></returns>
    <OperationContract()>
    Function SaveAndConfirm(CashReceipts As Domain.Entities.CashReceipts, IndigoSessionValues As SessionValues, idSequence As Int64, audit As AuditMessage, sequenceC As Domain.Entities.TreasurySequence) As ActionResult(Of CashReceipts)
    ''' <summary>
    ''' confirma el recibo de caja
    ''' </summary>
    ''' <param name="idCashReceipt">The cash receipts.</param>
    ''' <returns></returns>
    <OperationContract()>
    Function ConfirmCashReceipts(idCashReceipt As Integer, IndigoSessionValues As SessionValues, audit As AuditMessage) As Domain.Base.Entities.ActionResult(Of String)

    ''' <summary>
    ''' elimina el recibo de caja
    ''' </summary>
    ''' <param name="CashReceipts">The cash receipts.</param>
    ''' <returns></returns>
    <OperationContract()>
    Function DeleteCashReceipts(CashReceipts As CashReceipts, audit As AuditMessage) As ActionResult

    ''' <summary>
    ''' metodo para obtener los detalles del recibo de caja
    ''' </summary>
    ''' <param name="idCashReceipt">The identifier cash receipt.</param>
    ''' <returns></returns>
    <OperationContract()>
    Function GetCashReceiptDetailsByIdCashReceipt(idCashReceipt As Integer) As List(Of CashReceiptDetails)

    ''' <summary>
    ''' metodo para obtener los metodos de pago de los recibos de caja
    ''' </summary>
    ''' <param name="idCashRegister">The identifier cash register.</param>
    ''' <returns></returns>
    <OperationContract()>
    Function GetPaymentMethodsByIdCashReceipts(idCashRegister As Integer) As List(Of PaymentMethods)
    <OperationContract()>
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
    <OperationContract()>
    Function SetBillsCashReceipts(data As List(Of List(Of String)), idThirdPaty As Integer, idMainAccount As Integer, idCostCenter As Integer, idOperatingUnit As Integer) As ActionResult(Of List(Of AccountReceivable))

    ''' <summary>
    ''' Metodo para realizar un recibo de caja
    ''' </summary>
    ''' <param name="invoiceNumber"></param>
    ''' <param name="value"></param>
    ''' <param name="documentDate"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <OperationContract()>
    Function GenerateCashReceiptsREST(invoiceNumber As String, value As Decimal, documentDate As DateTime) As ActionResult

End Interface
