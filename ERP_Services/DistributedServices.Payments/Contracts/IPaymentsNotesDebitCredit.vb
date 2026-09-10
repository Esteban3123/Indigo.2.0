'***********************************************************************
' Assembly         : DistributedServices.Payments
' Author           : Carlos Mario Arias Rubiano
' Created          : 01-04-2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities
Imports System.ServiceModel

<ServiceContract()> _
Public Interface IPaymentsNotesDebitCredit

    ''' <summary>
    ''' Importa facturas a la nota de cuentas por pagar
    ''' </summary>
    ''' <param name="data"></param>
    ''' <param name="parameters"></param>
    ''' <returns></returns>
    <OperationContract()>
    Function ImportBillsToPortfolioNote(data As List(Of List(Of String)), ParamArray parameters As Object()) As ActionResult(Of List(Of AccountPayable))

    ''' <summary>
    ''' Importa anticipos a la nota de cuentas por pagar
    ''' </summary>
    ''' <param name="data"></param>
    ''' <param name="parameters"></param>
    ''' <returns></returns>
    <OperationContract()>
    Function ImportAdvancesToPortfolioNote(data As List(Of List(Of String)), ParamArray parameters As Object()) As ActionResult(Of List(Of AdvancePayments))

    ''' <summary>
    ''' Guarda o Actualiza una nota debito/credito
    ''' </summary>
    ''' <returns></returns>
    <OperationContract()>
    Function SavePaymentsNote(paymentsNote As Domain.Entities.PaymentNotes, idSequense As Int64, audit As AuditMessage) As Domain.Base.Entities.ActionResult(Of Domain.Entities.PaymentNotes)

    ''' <summary>
    ''' Elimina una nota debito/credito
    ''' </summary>
    ''' <returns></returns>
    <OperationContract()>
    Function DeletePaymentsNote(paymentsNote As Domain.Entities.PaymentNotes, audit As AuditMessage) As Domain.Base.Entities.ActionResult

    ''' <summary>
    ''' Obtiene una determinada nota debito/credito
    ''' </summary>
    ''' <param name="code">The code.</param>
    ''' <returns></returns>
    <OperationContract()>
    Function GetPaymentsNote(code As String, audit As AuditMessage) As ActionResult(Of Domain.Entities.PaymentNotes)

    ''' <summary>
    ''' Cambia el estado de la entidad
    ''' </summary>
    ''' <param name="code"></param>
    ''' <param name="state"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <OperationContract()>
    Function ChangeStatePaymentsNote(code As String, state As Boolean, audit As AuditMessage) As Domain.Base.Entities.ActionResult(Of Domain.Entities.PaymentNotes)

    ''' <summary>
    ''' Guarda las notas de manera completa: modificando los balances
    ''' </summary>
    ''' <param name="paymentNotes"></param>
    ''' <param name="listAccountPayable"></param>
    ''' <param name="listAdvancePayments"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <OperationContract()>
    Function SavePaymentNotesComplete(paymentNotes As Domain.Entities.PaymentNotes, listAccountPayable As List(Of Domain.Entities.AccountPayable), listAdvancePayments As List(Of Domain.Entities.AdvancePayments), modeConfirm As Boolean, idSequense As Int64, audit As AuditMessage) As Domain.Base.Entities.ActionResult(Of Domain.Entities.PaymentNotes)

    ''' <summary>
    ''' Confirma la nota debito
    ''' </summary>
    ''' <param name="paymentNotes"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <OperationContract()>
    Function ConfirmPaymentNotes(paymentNotes As Domain.Entities.PaymentNotes, listAccountPayable As List(Of Domain.Entities.AccountPayable), listAdvancePayments As List(Of Domain.Entities.AdvancePayments), idSequense As Int64, audit As AuditMessage) As Domain.Base.Entities.ActionResult(Of Domain.Entities.PaymentNotes)

End Interface
