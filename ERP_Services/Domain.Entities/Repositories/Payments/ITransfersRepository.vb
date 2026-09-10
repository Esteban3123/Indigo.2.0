'***********************************************************************
' Assembly         : Domain.Payments
' Author           : Carlos Mario Arias Rubiano
' Created          : 21-06-2013
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Base
Imports Domain.Entities

Public Interface ITransfersRepository
    Inherits IRepository(Of PaymentTransfer)

    ''' <summary>
    ''' Valida el CopyPaste de facturas del form de cruce de anticipos vs Cuentas por Pagar
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function SP_ImportBillsToPaymentTransfer(XmlObject As String, XmlParameters As String) As List(Of SP_ImportBillsToPaymentTransfer_Result)

    Function ListPaymentTransferMassiveConfirm(listDocuments As List(Of String)) As List(Of PaymentTransfer)

    ''' <summary>
    ''' Obtiene un traslado por codigo
    ''' </summary>
    ''' <param name="code"></param>
    ''' <param name="tracking"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetPaymentsTransfer(ByVal code As String, Optional tracking As Boolean = True) As PaymentTransfer

    ''' <summary>
    ''' Obtiene un traslado por id
    ''' </summary>
    ''' <param name="id"></param>
    ''' <param name="tracking"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetPaymentsTransferById(ByVal id As Integer, Optional tracking As Boolean = True) As PaymentTransfer

    ''' <summary>
    ''' Obtiene un listado de validaciones si el accountPayable esta en paymentTransfer
    ''' </summary>
    ''' <param name="listAccountPayableId">The list account payable identifier.</param>
    ''' <returns></returns>
    Function GetPaymentTransferByAccountPayableListId(listAccountPayableId As List(Of Integer)) As List(Of String)

    ''' <summary>
    ''' Confirma un cruce de anticipo vs cxp
    ''' </summary>
    ''' <param name="paymentTransferId"></param>
    ''' <param name="codeUser"></param>
    ''' <returns></returns>
    Function SP_ConfirmPaymentTransfer(paymentTransferId As Integer, codeUser As String) As SP_ConfirmPaymentTransfer_Result

End Interface
