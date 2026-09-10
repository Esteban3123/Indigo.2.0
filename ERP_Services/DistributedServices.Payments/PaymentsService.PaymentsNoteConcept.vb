'***********************************************************************
' Assembly         : DistributedServices.Payments
' Author           : Carlos Mario Arias Rubiano
' Created          : 03/04/2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Application.Payments
Imports Domain.Base.Entities
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Microsoft.Practices.Unity

Partial Public Class PaymentsService

    ''' <summary>
    ''' Elimina un concepto de nota
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function DeletePaymentNoteConcept(paymentNoteConcept As AccountPayableConceptNotes, audit As AuditMessage) As ActionResult Implements IPaymentsPaymentsNoteConcept.DeletePaymentNoteConcept
        Using service As IPaymentsNoteConceptAdminService = Container.Current.Resolve(Of IPaymentsNoteConceptAdminService)()
            Return service.DeletePaymentNoteConcept(paymentNoteConcept, audit)
        End Using
        'Return Me._paymentsNoteConceptAdminService.DeletePaymentNoteConcept(paymentNoteConcept, audit)
    End Function

    ''' <summary>
    ''' Obtiene un determinado concepto de nota
    ''' </summary>
    ''' <param name="code"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetPaymentNoteConcept(code As String, audit As AuditMessage) As ActionResult(Of AccountPayableConceptNotes) Implements IPaymentsPaymentsNoteConcept.GetPaymentNoteConcept
        Using service As IPaymentsNoteConceptAdminService = Container.Current.Resolve(Of IPaymentsNoteConceptAdminService)()
            Return service.GetPaymentNoteConcept(code, audit)
        End Using
        'Return Me._paymentsNoteConceptAdminService.GetPaymentNoteConcept(code, audit)
    End Function

    ''' <summary>
    ''' Lista todos los conceptos de pago
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListAllPaymentNoteConcept(session As SessionValues, audit As AuditMessage) As List(Of AccountPayableConceptNotes) Implements IPaymentsPaymentsNoteConcept.ListAllPaymentNoteConcept
        Using service As IPaymentsNoteConceptAdminService = Container.Current.Resolve(Of IPaymentsNoteConceptAdminService)()
            Return service.ListAllPaymentNoteConcept(audit)
        End Using
        'Return Me._paymentsNoteConceptAdminService.ListAllPaymentNoteConcept(audit)
    End Function

    ''' <summary>
    ''' Guarda o actualiza un concepto de pago
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SavePaymentNoteConcept(paymentNoteConcept As AccountPayableConceptNotes, idSequense As Int64, audit As AuditMessage) As ActionResult(Of AccountPayableConceptNotes) Implements IPaymentsPaymentsNoteConcept.SavePaymentNoteConcept
        Using service As IPaymentsNoteConceptAdminService = Container.Current.Resolve(Of IPaymentsNoteConceptAdminService)()
            Return service.SavePaymentNoteConcept(paymentNoteConcept, audit, idSequense)
        End Using
        'Return Me._paymentsNoteConceptAdminService.SavePaymentNoteConcept(paymentNoteConcept, audit, idSequense)
    End Function

    ''' <summary>
    ''' Cambia el estado de la entidad
    ''' </summary>
    ''' <param name="code"></param>
    ''' <param name="state"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ChangeStatePaymentNoteConcept(code As String, state As Boolean, audit As AuditMessage) As Domain.Base.Entities.ActionResult(Of Domain.Entities.AccountPayableConceptNotes) Implements IPaymentsPaymentsNoteConcept.ChangeStatePaymentNoteConcept
        Using service As IPaymentsNoteConceptAdminService = Container.Current.Resolve(Of IPaymentsNoteConceptAdminService)()
            Return service.ChangeState(code, state, audit)
        End Using
        'Return Me._paymentsNoteConceptAdminService.ChangeState(code, state, audit)
    End Function

    ''' <summary>
    ''' Consulta el concepto de nota por id
    ''' </summary>
    ''' <param name="id"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetPaymentNoteConceptById(id As String, audit As AuditMessage) As Domain.Base.Entities.ActionResult(Of Domain.Entities.AccountPayableConceptNotes) Implements IPaymentsPaymentsNoteConcept.GetPaymentNoteConceptById
        Using service As IPaymentsNoteConceptAdminService = Container.Current.Resolve(Of IPaymentsNoteConceptAdminService)()
            Return service.GetPaymentNoteConceptById(id, audit)
        End Using
        'Return Me._paymentsNoteConceptAdminService.GetPaymentNoteConceptById(id, audit)
    End Function

End Class
