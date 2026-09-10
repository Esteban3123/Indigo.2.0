'***********************************************************************
' Assembly         : DistributedServices.Payments
' Author           : Carlos Mario Arias Rubiano
' Created          : 03/04/2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities
Imports System.ServiceModel

<ServiceContract()> _
Public Interface IPaymentsPaymentsNoteConcept

    ''' <summary>
    ''' Guarda o Actualiza un concepto de nota
    ''' </summary>
    ''' <returns></returns>
    <OperationContract()>
    Function SavePaymentNoteConcept(paymentNoteConcept As AccountPayableConceptNotes, idSequense As Int64, audit As AuditMessage) As ActionResult(Of AccountPayableConceptNotes)

    ''' <summary>
    ''' Elimina un concepto de nota
    ''' </summary>
    ''' <returns></returns>
    <OperationContract()>
    Function DeletePaymentNoteConcept(paymentNoteConcept As AccountPayableConceptNotes, audit As AuditMessage) As ActionResult

    ''' <summary>
    ''' Obtiene todos los conceptos de notas
    ''' </summary>
    ''' <returns>Lista de dependencias</returns>
    <OperationContract()>
    Function ListAllPaymentNoteConcept(session As SessionValues, audit As AuditMessage) As List(Of AccountPayableConceptNotes)

    ''' <summary>
    ''' Obtiene un determinado concepto de nota 
    ''' </summary>
    ''' <param name="code">The code.</param>
    ''' <returns></returns>
    <OperationContract()>
    Function GetPaymentNoteConcept(code As String, audit As AuditMessage) As ActionResult(Of AccountPayableConceptNotes)

    ''' <summary>
    ''' Cambia el estado de la entidad
    ''' </summary>
    ''' <param name="code"></param>
    ''' <param name="state"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <OperationContract()>
    Function ChangeStatePaymentNoteConcept(code As String, state As Boolean, audit As AuditMessage) As Domain.Base.Entities.ActionResult(Of Domain.Entities.AccountPayableConceptNotes)

    ''' <summary>
    ''' Obtiene un concepto de pago
    ''' </summary>
    ''' <param name="id"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <OperationContract()>
    Function GetPaymentNoteConceptById(id As String, audit As AuditMessage) As Domain.Base.Entities.ActionResult(Of Domain.Entities.AccountPayableConceptNotes)

End Interface
