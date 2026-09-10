'***********************************************************************
' Assembly         : Domain.Treasury
' Author           : Diego Andrés Roldán Lozano
' Created          : 02-04-2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************
Imports Domain.Base
Imports Domain.Entities

Public Interface ICashReceiptsRepository
    Inherits IRepository(Of CashReceipts)

#Region "Methods"

    Function GetConcept(mainAccountId As Integer) As CashReceiptConcepts

    Function GetAccountReceivableByInvoiceNumberCashReceipts(invoiceNumber As String) As AccountReceivable

    Function GetFirstAccount() As EntityBankAccounts

    Function GetThird(id As Integer) As ThirdParty

    Function ReverseCashReceiptSP(CashReceiptId As Integer, UserCode As String, TreasuryNoteCode As String) As Entity.Core.Objects.ObjectResult(Of SP_ReverseCashReceipt_Result)

    Function GenerateCashReceiptSP(cashReceiptXml As String, UserCode As String, companyType As Integer) As Entity.Core.Objects.ObjectResult(Of SP_SaveCashReceipts_Result)

    Function ListCashReceiptsMassiveConfirm(listDocuments As List(Of String)) As List(Of CashReceipts)

    Function GetCashReceiptsDetailByIdCashReceiptIncludes(idCashReceipts As Integer) As List(Of CashReceiptDetails)

    ''' <summary>
    ''' metodo para obtener un recibo de caja por codigo
    ''' </summary>
    ''' <param name="code">The code.</param>
    ''' <returns></returns>
    Function GetCashReceiptsByCode(code As String) As CashReceipts
    ''' <summary>
    ''' ''' metodo para obtener un recibo de caja por id
    ''' </summary>
    ''' <param name="id"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetCashReceiptsById(id As Integer) As CashReceipts
    ''' <summary>
    ''' metodo para traer los detalles del recibo de caja
    ''' </summary>
    ''' <param name="idCashReceipts"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetCashReceiptsDetailByIdCashReceipt(idCashReceipts As Integer) As List(Of CashReceiptDetails)
    ''' <summary>
    ''' metodo para traer los metodos de pago del  recibo de caja
    ''' </summary>
    ''' <param name="idCashReceipt"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetPaymentMethodsByIdCashReceipt(idCashReceipt As Integer) As List(Of PaymentMethods)

    Function ValidateCostcenterPaymentMethod(PaymentMethods As PaymentMethods, OperatingUnitId As Integer) As Base.Entities.ActionResult

    Function GetCashReceiptsByIdAccountReceivable(accountReceivableId As Integer) As CashReceipts

    ''' <summary>
    ''' Sp que relaiza la devolucion de recibo de caja
    ''' </summary>
    ''' <param name="DevolutionCashReceipt"></param>
    ''' <returns></returns>
    Function SP_DevolutionCashReceipt(DevolutionCashReceipt As String) As Entity.Core.Objects.ObjectResult(Of SP_DevolutionCashReceipt_Result)


#End Region

End Interface
