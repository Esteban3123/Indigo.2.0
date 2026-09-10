'***********************************************************************
' Assembly         : Domain.Payments
' Author           : Carlos Mario Arias Rubiano
' Created          : 21-06-2013
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Base
Imports Domain.Entities

Public Interface INotesDebitCreditRepository
    Inherits IRepository(Of PaymentNotes)

    ''' <summary>
    ''' Valida el CopyPaste de facturas del form de notas de cuentas por pagar
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function SP_ImportBillsToPortfolioNote(XmlObject As String, XmlParameters As String) As List(Of SP_ImportBillsToPortfolioNote_Result)

    ''' <summary>
    ''' Valida el CopyPaste de anticipos del form de notas de cuentas por pagar
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function SP_ImportAdvancesToPortfolioNote(XmlObject As String, XmlParameters As String) As List(Of SP_ImportAdvancesToPortfolioNote_Result)

    ''' <summary>
    ''' Confirma una cuenta por pagar
    ''' </summary>
    ''' <param name="PaymentNotesXml"></param>
    ''' <param name="codeUser"></param>
    ''' <param name="isMassiveConfirm"></param>
    ''' <returns></returns>
    Function SP_ConfirmPaymentNotes(PaymentNotesXml As String, codeUser As String, isMassiveConfirm As Boolean) As List(Of SP_ConfirmPayableNote_Result)

    Function ListPaymentNotesMassiveConfirm(listDocuments As List(Of String)) As List(Of PaymentNotes)

    ''' <summary>
    ''' Obtiene una nota debito/credito por codigo
    ''' </summary>
    ''' <param name="code"></param>
    ''' <param name="tracking"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetPaymentsNote(ByVal code As String, Optional tracking As Boolean = True) As PaymentNotes

    ''' <summary>
    ''' Obtiene un traslado por id
    ''' </summary>
    ''' <param name="id"></param>
    ''' <param name="tracking"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetPaymentsNoteById(ByVal id As Integer, Optional tracking As Boolean = True) As PaymentNotes

    ''' <summary>
    ''' Valida que las cuentas por pagar agregadas a la rejilla no existan en una programacion de pagos confirmada
    ''' </summary>
    ''' <param name="ListAccountPayable"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function ValidateSchedulePaymentDetailContainsAccountPayable(ListAccountPayable As List(Of AccountPayable)) As List(Of AccountPayable)

    ''' <summary>
    ''' Valida que las cuentas por pagar agregadas a la rejilla no existan en un comprobantes de egreso confirmado
    ''' </summary>
    ''' <param name="ListAccountPayable"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function ValidateDischargeBillsContainsAccountPayable(ListAccountPayable As List(Of AccountPayable)) As List(Of AccountPayable)

End Interface
