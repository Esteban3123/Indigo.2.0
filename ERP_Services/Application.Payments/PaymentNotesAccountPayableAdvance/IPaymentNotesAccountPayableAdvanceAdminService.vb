'***********************************************************************
' Assembly         : Application.Payments
' Author           : Carlos Mario Arias Rubiano
' Created          : 23/07/2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities

Public Interface IPaymentNotesAccountPayableAdvanceAdminService
    Inherits IDisposable

    ''' <summary>
    ''' Guarda o Actualiza la relacion entre las notas y que facturas o anticipos modifico
    ''' </summary>
    ''' <param name="audit">The audit.</param>
    ''' <returns></returns>
    Function SavePaymentNotesAccountPayableAdvance(ByVal paymentNotesAccountPayableAdvance As PaymentNotesAccountPayableAdvance, ByVal audit As AuditMessage, Optional ByVal idSequense As Int64 = 0) As ActionResult(Of PaymentNotesAccountPayableAdvance)

    ''' <summary>
    ''' Elimina un concepto de pago
    ''' </summary>
    ''' <param name="audit">The audit.</param>
    ''' <returns></returns>
    Function DeletePaymentNotesAccountPayableAdvance(ByVal paymentNotesAccountPayableAdvance As PaymentNotesAccountPayableAdvance, ByVal audit As AuditMessage) As ActionResult

    ''' <summary>
    ''' Obtiene un concepto de pago
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetPaymentNotesAccountPayableAdvanceById(id As String) As PaymentNotesAccountPayableAdvance

    ''' <summary>
    ''' Obtiene un listado de cxp por id de la tabla PaymentNotesAccountPayableAdvance
    ''' </summary>
    ''' <param name="id"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetPaymentNotesAccountPayableAdvanceByIdAccountPayableOrAdvance(id As Integer, type As Integer) As List(Of PaymentNotesAccountPayableAdvance)

End Interface
