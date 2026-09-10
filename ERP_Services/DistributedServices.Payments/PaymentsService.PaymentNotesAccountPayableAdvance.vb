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
    ''' Elimina la relacion de notas con facturas o anticipo
    ''' </summary>
    ''' <param name="paymentNotesAccountPayableAdvance"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function DeletePaymentNotesAccountPayableAdvance(paymentNotesAccountPayableAdvance As Domain.Entities.PaymentNotesAccountPayableAdvance, audit As AuditMessage) As Domain.Base.Entities.ActionResult Implements IPaymentsPaymentNotesAccountPayableAdvance.DeletePaymentNotesAccountPayableAdvance
        Using service As IPaymentNotesAccountPayableAdvanceAdminService = Container.Current.Resolve(Of IPaymentNotesAccountPayableAdvanceAdminService)()
            Return service.DeletePaymentNotesAccountPayableAdvance(paymentNotesAccountPayableAdvance, audit)
        End Using
        'Return Me._paymentNotesAccountPayableAdvanceAdminService.DeletePaymentNotesAccountPayableAdvance(paymentNotesAccountPayableAdvance, audit)
    End Function

    ''' <summary>
    ''' Obtiene la relacion de notas con facturas o anticipo por id
    ''' </summary>
    ''' <param name="id"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetPaymentNotesAccountPayableAdvanceById(id As String, audit As AuditMessage) As Domain.Entities.PaymentNotesAccountPayableAdvance Implements IPaymentsPaymentNotesAccountPayableAdvance.GetPaymentNotesAccountPayableAdvanceById
        Using service As IPaymentNotesAccountPayableAdvanceAdminService = Container.Current.Resolve(Of IPaymentNotesAccountPayableAdvanceAdminService)()
            Return service.GetPaymentNotesAccountPayableAdvanceById(id)
        End Using
        'Return Me._paymentNotesAccountPayableAdvanceAdminService.GetPaymentNotesAccountPayableAdvanceById(id)
    End Function

    ''' <summary>
    ''' Obtiene el listado de facturas o anticipos que fueron afectadas por la nota
    ''' </summary>
    ''' <param name="id"></param>
    ''' <param name="type"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetPaymentNotesAccountPayableAdvanceByIdAccountPayableOrAdvance(id As Integer, type As Integer, audit As AuditMessage) As List(Of Domain.Entities.PaymentNotesAccountPayableAdvance) Implements IPaymentsPaymentNotesAccountPayableAdvance.GetPaymentNotesAccountPayableAdvanceByIdAccountPayableOrAdvance
        Using service As IPaymentNotesAccountPayableAdvanceAdminService = Container.Current.Resolve(Of IPaymentNotesAccountPayableAdvanceAdminService)()
            Return service.GetPaymentNotesAccountPayableAdvanceByIdAccountPayableOrAdvance(id, type)
        End Using
        'Return Me._paymentNotesAccountPayableAdvanceAdminService.GetPaymentNotesAccountPayableAdvanceByIdAccountPayableOrAdvance(id, type)
    End Function

    ''' <summary>
    ''' Guarda la relacion que hay entre la nota y las facturas o anticipo
    ''' </summary>
    ''' <param name="paymentNotesAccountPayableAdvance"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SavePaymentNotesAccountPayableAdvance(paymentNotesAccountPayableAdvance As Domain.Entities.PaymentNotesAccountPayableAdvance, audit As AuditMessage) As Domain.Base.Entities.ActionResult(Of Domain.Entities.PaymentNotesAccountPayableAdvance) Implements IPaymentsPaymentNotesAccountPayableAdvance.SavePaymentNotesAccountPayableAdvance
        Using service As IPaymentNotesAccountPayableAdvanceAdminService = Container.Current.Resolve(Of IPaymentNotesAccountPayableAdvanceAdminService)()
            Return service.SavePaymentNotesAccountPayableAdvance(paymentNotesAccountPayableAdvance, audit)
        End Using
        'Return Me._paymentNotesAccountPayableAdvanceAdminService.SavePaymentNotesAccountPayableAdvance(paymentNotesAccountPayableAdvance, audit)
    End Function


End Class
