'***********************************************************************
' Assembly         : DistributedServices.Payments
' Author           : Carlos Mario Arias Rubiano
' Created          : 02/04/2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities
Imports System.ServiceModel

<ServiceContract()> _
Public Interface IPaymentsPaymentNotesAccountPayableAdvance

    ''' <summary>
    ''' Guarda o Actualiza la relacion entre las notas y que facturas o anticipos modifico
    ''' </summary>
    ''' <returns></returns>
    <OperationContract()>
    Function SavePaymentNotesAccountPayableAdvance(paymentNotesAccountPayableAdvance As Domain.Entities.PaymentNotesAccountPayableAdvance, audit As AuditMessage) As Domain.Base.Entities.ActionResult(Of Domain.Entities.PaymentNotesAccountPayableAdvance)

    ''' <summary>
    ''' Elimina un concepto de pago
    ''' </summary>
    ''' <returns></returns>
    <OperationContract()>
    Function DeletePaymentNotesAccountPayableAdvance(paymentNotesAccountPayableAdvance As Domain.Entities.PaymentNotesAccountPayableAdvance, audit As AuditMessage) As Domain.Base.Entities.ActionResult

    ''' <summary>
    ''' Obtiene un concepto de pago
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <OperationContract()>
    Function GetPaymentNotesAccountPayableAdvanceById(id As String, audit As AuditMessage) As Domain.Entities.PaymentNotesAccountPayableAdvance

    ''' <summary>
    ''' Obtiene un listado de cxp por id de la tabla PaymentNotesAccountPayableAdvance
    ''' </summary>
    ''' <param name="id"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <OperationContract()>
    Function GetPaymentNotesAccountPayableAdvanceByIdAccountPayableOrAdvance(id As Integer, type As Integer, audit As AuditMessage) As List(Of Domain.Entities.PaymentNotesAccountPayableAdvance)

End Interface
