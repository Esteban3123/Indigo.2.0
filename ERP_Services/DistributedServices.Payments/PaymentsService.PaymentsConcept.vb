'***********************************************************************
' Assembly         : DistributedServices.Payments
' Author           : Carlos Mario Arias Rubiano
' Created          : 02/04/2014
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

    ''' <summary>
    ''' Elimina un concepto de pago
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function DeletePaymentConcept(paymentConcept As AccountPayableConcepts, audit As AuditMessage) As ActionResult Implements IPaymentsPaymentsConcept.DeletePaymentConcept
        Using service As IPaymentsConceptAdminService = Container.Current.Resolve(Of IPaymentsConceptAdminService)()
            Return service.DeletePaymentConcept(paymentConcept, audit)
        End Using
        'Return Me._PaymentsConceptAdminService.DeletePaymentConcept(paymentConcept, audit)
    End Function

    ''' <summary>
    ''' Obtiene un determinado concepto de pago
    ''' </summary>
    ''' <param name="code"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetPaymentConcept(code As String, audit As AuditMessage) As ActionResult(Of AccountPayableConcepts) Implements IPaymentsPaymentsConcept.GetPaymentConcept
        Using service As IPaymentsConceptAdminService = Container.Current.Resolve(Of IPaymentsConceptAdminService)()
            Return service.GetPaymentConcept(code, audit)
        End Using
        'Return Me._PaymentsConceptAdminService.GetPaymentConcept(code, audit)
    End Function

    ''' <summary>
    ''' Lista todos los conceptos de pagos
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListAllPaymentConcept(session As SessionValues, audit As AuditMessage) As List(Of AccountPayableConcepts) Implements IPaymentsPaymentsConcept.ListAllPaymentConcept
        Using service As IPaymentsConceptAdminService = Container.Current.Resolve(Of IPaymentsConceptAdminService)()
            Return service.ListAllPaymentConcept(audit)
        End Using
        'Return Me._PaymentsConceptAdminService.ListAllPaymentConcept(audit)
    End Function

    ''' <summary>
    ''' Guarda o actualiza un concepto de pago
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SavePaymentConcept(paymentConcept As AccountPayableConcepts, idSequense As Int64, audit As AuditMessage) As ActionResult(Of AccountPayableConcepts) Implements IPaymentsPaymentsConcept.SavePaymentConcept
        Using service As IPaymentsConceptAdminService = Container.Current.Resolve(Of IPaymentsConceptAdminService)()
            Return service.SavePaymentConcept(paymentConcept, audit, idSequense)
        End Using
        'Return Me._PaymentsConceptAdminService.SavePaymentConcept(paymentConcept, audit, idSequense)
    End Function

    Public Function ChangeStatePaymentConcept(code As String, state As Boolean, audit As AuditMessage) As Domain.Base.Entities.ActionResult(Of Domain.Entities.AccountPayableConcepts) Implements IPaymentsPaymentsConcept.ChangeStatePaymentConcept
        Using service As IPaymentsConceptAdminService = Container.Current.Resolve(Of IPaymentsConceptAdminService)()
            Return service.ChangeState(code, state, audit)
        End Using
        'Return Me._PaymentsConceptAdminService.ChangeState(code, state, audit)
    End Function

    ''' <summary>
    ''' Concepto por id
    ''' </summary>
    ''' <param name="id"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetPaymentConceptById(id As Integer, audit As AuditMessage) As ActionResult(Of Domain.Entities.AccountPayableConcepts) Implements IPaymentsPaymentsConcept.GetPaymentConceptById
        Using service As IPaymentsConceptAdminService = Container.Current.Resolve(Of IPaymentsConceptAdminService)()
            Return service.GetPaymentConceptById(id, audit)
        End Using
        'Return Me._PaymentsConceptAdminService.GetPaymentConceptById(id, audit)
    End Function

End Class
