'***********************************************************************
' Assembly         : Application.Payments
' Author           : Carlos Mario Arias Rubiano
' Created          : 31-03-2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities

Public Interface INotesDebitCreditAdminService
    Inherits IDisposable

    ''' <summary>
    ''' Importa facturas a la nota de cuentas por pagar
    ''' </summary>
    ''' <param name="data"></param>
    ''' <param name="parameters"></param>
    ''' <returns></returns>
    Function ImportBillsToPortfolioNote(data As List(Of List(Of String)), ParamArray parameters As Object()) As ActionResult(Of List(Of AccountPayable))

    ''' <summary>
    ''' Importa anticipos a la nota de cuentas por pagar
    ''' </summary>
    ''' <param name="data"></param>
    ''' <param name="parameters"></param>
    ''' <returns></returns>
    Function ImportAdvancesToPortfolioNote(data As List(Of List(Of String)), ParamArray parameters As Object()) As ActionResult(Of List(Of AdvancePayments))

    ''' <summary>
    ''' Guarda o Actualiza una nota debito/credito
    ''' </summary>
    ''' <param name="audit">The audit.</param>
    ''' <returns></returns>
    Function SavePaymentsNote(ByVal paymentsNote As PaymentNotes, ByVal audit As AuditMessage, Optional ByVal idSequense As Int64 = 0) As ActionResult(Of PaymentNotes)

    ''' <summary>
    ''' Guarda las notas de manera completa: modificando los balances
    ''' </summary>
    ''' <param name="paymentNotes"></param>
    ''' <param name="listAccountPayable"></param>
    ''' <param name="listAdvancePayments"></param>
    ''' <param name="audit"></param>
    ''' <param name="idSequense"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function SavePaymentNotesComplete(ByVal paymentNotes As PaymentNotes, ByVal listAccountPayable As List(Of AccountPayable), ByVal listAdvancePayments As List(Of AdvancePayments), ByVal modeConfirm As Boolean, ByVal audit As AuditMessage, Optional ByVal idSequense As Int64 = 0,
                                      Optional ByVal FlagDispersion As Boolean = False) As ActionResult(Of PaymentNotes)

    ''' <summary>
    ''' Elimina una nota debito/credito
    ''' </summary>
    ''' <param name="audit">The audit.</param>
    ''' <returns></returns>
    Function DeletePaymentsNote(ByVal paymentsNote As PaymentNotes, ByVal audit As AuditMessage) As ActionResult

    ''' <summary>
    ''' Obtiene una determinada nota debito/credito
    ''' </summary>
    ''' <param name="code">The code.</param>
    ''' <returns></returns>
    Function GetPaymentsNote(ByVal code As String, ByVal audit As AuditMessage) As ActionResult(Of PaymentNotes)

    ''' <summary>
    ''' Cambia el estado de la entidad
    ''' </summary>
    ''' <param name="code"></param>
    ''' <param name="state"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function ChangeState(ByVal code As String, ByVal state As Boolean, ByVal audit As AuditMessage) As ActionResult(Of PaymentNotes)

    ''' <summary>
    ''' Confirma la nota debito
    ''' </summary>
    ''' <param name="paymentNotes"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function ConfirmPaymentNotes(ByVal paymentNotes As PaymentNotes, audit As AuditMessage, Optional isMassiveConfirm As Boolean = False) As ActionResult(Of PaymentNotes)

End Interface
