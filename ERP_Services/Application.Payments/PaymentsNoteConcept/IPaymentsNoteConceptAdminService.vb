'***********************************************************************
' Assembly         : Application.Payments
' Author           : Carlos Mario Arias Rubiano
' Created          : 03/04/2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities

Public Interface IPaymentsNoteConceptAdminService
    Inherits IDisposable

    ''' <summary>
    ''' Guarda o Actualiza un concepto de nota
    ''' </summary>
    ''' <param name="audit">The audit.</param>
    ''' <returns></returns>
    Function SavePaymentNoteConcept(ByVal paymentNoteConcept As AccountPayableConceptNotes, ByVal audit As AuditMessage, Optional ByVal idSequense As Int64 = 0) As ActionResult(Of AccountPayableConceptNotes)

    ''' <summary>
    ''' Elimina un concepto de nota
    ''' </summary>
    ''' <param name="audit">The audit.</param>
    ''' <returns></returns>
    Function DeletePaymentNoteConcept(ByVal paymentNoteConcept As AccountPayableConceptNotes, ByVal audit As AuditMessage) As ActionResult

    ''' <summary>
    ''' Obtiene todos los conceptos de notas
    ''' </summary>
    ''' <returns>Lista de dependencias</returns>
    Function ListAllPaymentNoteConcept(ByVal audit As AuditMessage) As List(Of AccountPayableConceptNotes)

    ''' <summary>
    ''' Obtiene un determinado concepto de nota 
    ''' </summary>
    ''' <param name="code">The code.</param>
    ''' <returns></returns>
    Function GetPaymentNoteConcept(ByVal code As String, ByVal audit As AuditMessage) As ActionResult(Of AccountPayableConceptNotes)

    ''' <summary>
    ''' Cambia el estado de la entidad
    ''' </summary>
    ''' <param name="code"></param>
    ''' <param name="state"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function ChangeState(ByVal code As String, ByVal state As Boolean, ByVal audit As AuditMessage) As ActionResult(Of AccountPayableConceptNotes)

    ''' <summary>
    ''' Obtiene un concepto de pago
    ''' </summary>
    ''' <param name="id"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetPaymentNoteConceptById(id As String, ByVal audit As AuditMessage) As ActionResult(Of AccountPayableConceptNotes)

End Interface
