'***********************************************************************
' Assembly         : Application.Payments
' Author           : Carlos Mario Arias Rubiano
' Created          : 02/04/2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities

Public Interface IPaymentsConceptAdminService
    Inherits IDisposable

    ''' <summary>
    ''' Guarda o Actualiza un concepto de pago
    ''' </summary>
    ''' <param name="audit">The audit.</param>
    ''' <returns></returns>
    Function SavePaymentConcept(ByVal paymentConcept As AccountPayableConcepts, ByVal audit As AuditMessage, Optional ByVal idSequense As Int64 = 0) As ActionResult(Of AccountPayableConcepts)

    ''' <summary>
    ''' Elimina un concepto de pago
    ''' </summary>
    ''' <param name="audit">The audit.</param>
    ''' <returns></returns>
    Function DeletePaymentConcept(ByVal paymentConcept As AccountPayableConcepts, ByVal audit As AuditMessage) As ActionResult

    ''' <summary>
    ''' Obtiene todos los conceptos de pagos
    ''' </summary>
    ''' <returns>Lista de dependencias</returns>
    Function ListAllPaymentConcept(ByVal audit As AuditMessage) As List(Of AccountPayableConcepts)

    ''' <summary>
    ''' Obtiene un determinado concepto de pago 
    ''' </summary>
    ''' <param name="code">The code.</param>
    ''' <returns></returns>
    Function GetPaymentConcept(ByVal code As String, ByVal audit As AuditMessage) As ActionResult(Of AccountPayableConcepts)

    ''' <summary>
    ''' Obtiene un determinado concepto de pago 
    ''' </summary>
    ''' <returns></returns>
    Function GetPaymentConceptById(ByVal id As Integer, ByVal audit As AuditMessage) As ActionResult(Of AccountPayableConcepts)

    ''' <summary>
    ''' Cambia el estado de la entidad
    ''' </summary>
    ''' <param name="code"></param>
    ''' <param name="state"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function ChangeState(ByVal code As String, ByVal state As Boolean, ByVal audit As AuditMessage) As ActionResult(Of AccountPayableConcepts)

End Interface
