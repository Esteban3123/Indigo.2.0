'***********************************************************************
' Assembly         : Application.Treasury
' Author           : Diego Andrés Roldán Lozano
' Created          : 13-05-2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities

Public Interface IPaymentConceptAdminService
    Inherits IDisposable

    ''' <summary>
    ''' Guarda un concepto de pago
    ''' </summary>
    ''' <param name="paymentConcept">The payment concept.</param>
    ''' <param name="audit">The audit.</param>
    ''' <param name="idSequence">The identifier sequence.</param>
    ''' <returns></returns>
    Function SavePaymentConcept(ByVal paymentConcept As TreasuryPaymentConcepts, ByVal audit As AuditMessage, Optional ByVal idSequence As Int64 = 0) As ActionResult(Of TreasuryPaymentConcepts)

    ''' <summary>
    ''' Actualiza el estado del registro
    ''' </summary>
    ''' <param name="code">The code.</param>
    ''' <param name="state">if set to <c>true</c> [state].</param>
    ''' <param name="audit">The audit.</param>
    ''' <returns></returns>
    Function UpdateStatePaymentConcept(ByVal code As String, ByVal state As Boolean, ByVal audit As AuditMessage) As ActionResult(Of TreasuryPaymentConcepts)

    ''' <summary>
    ''' Elimina un concepto de pago
    ''' </summary>
    ''' <param name="paymentConcept">The payment concept.</param>
    ''' <param name="audit">The audit.</param>
    ''' <returns></returns>
    Function DeletePaymentConcept(ByVal paymentConcept As TreasuryPaymentConcepts, ByVal audit As AuditMessage) As ActionResult

    ''' <summary>
    ''' Obtiene un concepto de pago
    ''' </summary>
    ''' <param name="code">The code.</param>
    ''' <param name="audit">The audit.</param>
    ''' <returns></returns>
    Function GetPaymentConcept(ByVal code As String, ByVal audit As AuditMessage) As ActionResult(Of TreasuryPaymentConcepts)

End Interface
