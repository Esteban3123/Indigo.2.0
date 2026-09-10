'***********************************************************************
' Assembly         : DistributedServices.Payments
' Author           : Carlos Mario Arias Rubiano
' Created          : 01-04-2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Entities
Imports Application.Payments
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities
Imports System.ServiceModel
Imports Microsoft.Practices.Unity

Partial Public Class PaymentsService

    Public Function ImportBillsToPortfolioNote(data As List(Of List(Of String)), ParamArray parameters() As Object) As ActionResult(Of List(Of AccountPayable)) Implements IPaymentsNotesDebitCredit.ImportBillsToPortfolioNote
        Using service As INotesDebitCreditAdminService = Container.Current.Resolve(Of INotesDebitCreditAdminService)()
            Return service.ImportBillsToPortfolioNote(data, parameters)
        End Using
    End Function

    Public Function ImportAdvancesToPortfolioNote(data As List(Of List(Of String)), ParamArray parameters() As Object) As ActionResult(Of List(Of AdvancePayments)) Implements IPaymentsNotesDebitCredit.ImportAdvancesToPortfolioNote
        Using service As INotesDebitCreditAdminService = Container.Current.Resolve(Of INotesDebitCreditAdminService)()
            Return service.ImportAdvancesToPortfolioNote(data, parameters)
        End Using
    End Function

    ''' <summary>
    ''' Elimina una nota debito/credito
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function DeletePaymentsNote(paymentsNote As Domain.Entities.PaymentNotes, audit As AuditMessage) As Domain.Base.Entities.ActionResult Implements IPaymentsNotesDebitCredit.DeletePaymentsNote
        Using service As INotesDebitCreditAdminService = Container.Current.Resolve(Of INotesDebitCreditAdminService)()
            Return service.DeletePaymentsNote(paymentsNote, audit)
        End Using
        'Return Me._paymentsNoteAdminService.DeletePaymentsNote(paymentsNote, audit)
    End Function

    ''' <summary>
    ''' Obtiene una determinada nota debito/credito
    ''' </summary>
    ''' <param name="code"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetPaymentsNote(code As String, audit As AuditMessage) As ActionResult(Of Domain.Entities.PaymentNotes) Implements IPaymentsNotesDebitCredit.GetPaymentsNote
        Using service As INotesDebitCreditAdminService = Container.Current.Resolve(Of INotesDebitCreditAdminService)()
            Return service.GetPaymentsNote(code, audit)
        End Using
        'Return Me._paymentsNoteAdminService.GetPaymentsNote(code, audit)
    End Function

    ''' <summary>
    ''' Guarda o actualiza una nota debito/credito
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SavePaymentsNote(paymentsNote As Domain.Entities.PaymentNotes, idSequense As Int64, audit As AuditMessage) As Domain.Base.Entities.ActionResult(Of Domain.Entities.PaymentNotes) Implements IPaymentsNotesDebitCredit.SavePaymentsNote
        Using service As INotesDebitCreditAdminService = Container.Current.Resolve(Of INotesDebitCreditAdminService)()
            Return service.SavePaymentsNote(paymentsNote, audit, idSequense)
        End Using
        'Return Me._paymentsNoteAdminService.SavePaymentsNote(paymentsNote, audit, idSequense)
    End Function

    ''' <summary>
    ''' Cambia el estado de la entidad
    ''' </summary>
    ''' <param name="code"></param>
    ''' <param name="state"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ChangeStatePaymentsNote(code As String, state As Boolean, audit As AuditMessage) As Domain.Base.Entities.ActionResult(Of Domain.Entities.PaymentNotes) Implements IPaymentsNotesDebitCredit.ChangeStatePaymentsNote
        Using service As INotesDebitCreditAdminService = Container.Current.Resolve(Of INotesDebitCreditAdminService)()
            Return service.ChangeState(code, state, audit)
        End Using
        'Return Me._paymentsNoteAdminService.ChangeState(code, state, audit)
    End Function

    ''' <summary>
    ''' Guarda la nota de manera completa: modificando saldos
    ''' </summary>
    ''' <param name="paymentNotes"></param>
    ''' <param name="listAccountPayable"></param>
    ''' <param name="listAdvancePayments"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SavePaymentNotesComplete(paymentNotes As Domain.Entities.PaymentNotes, listAccountPayable As List(Of Domain.Entities.AccountPayable), listAdvancePayments As List(Of Domain.Entities.AdvancePayments), modeConfirm As Boolean, idSequense As Int64, audit As AuditMessage) As Domain.Base.Entities.ActionResult(Of Domain.Entities.PaymentNotes) Implements IPaymentsNotesDebitCredit.SavePaymentNotesComplete
        Using service As INotesDebitCreditAdminService = Container.Current.Resolve(Of INotesDebitCreditAdminService)()
            Return service.SavePaymentNotesComplete(paymentNotes, listAccountPayable, listAdvancePayments, modeConfirm, audit, idSequense)
        End Using
        'Return Me._paymentsNoteAdminService.SavePaymentNotesComplete(paymentNotes, listAccountPayable, listAdvancePayments, modeConfirm, audit, idSequense)
    End Function

    ''' <summary>
    ''' Confirma la nota debito/credito
    ''' </summary>
    ''' <param name="paymentNotes"></param>
    ''' <param name="listAccountPayable"></param>
    ''' <param name="listAdvancePayments"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ConfirmPaymentNotes(paymentNotes As Domain.Entities.PaymentNotes, listAccountPayable As List(Of Domain.Entities.AccountPayable), listAdvancePayments As List(Of Domain.Entities.AdvancePayments), idSequense As Int64, audit As AuditMessage) As Domain.Base.Entities.ActionResult(Of Domain.Entities.PaymentNotes) Implements IPaymentsNotesDebitCredit.ConfirmPaymentNotes
        Using service As INotesDebitCreditAdminService = Container.Current.Resolve(Of INotesDebitCreditAdminService)()
            Return service.ConfirmPaymentNotes(paymentNotes, audit)
        End Using
    End Function

End Class
