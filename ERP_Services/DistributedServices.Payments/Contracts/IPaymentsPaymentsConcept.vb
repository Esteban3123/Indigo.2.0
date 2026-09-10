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
Public Interface IPaymentsPaymentsConcept

    ''' <summary>
    ''' Guarda o Actualiza un concepto de pago
    ''' </summary>
    ''' <returns></returns>
    <OperationContract()>
    Function SavePaymentConcept(paymentConcept As AccountPayableConcepts, idSequense As Int64, audit As AuditMessage) As ActionResult(Of AccountPayableConcepts)

    ''' <summary>
    ''' Elimina un concepto de pago
    ''' </summary>
    ''' <returns></returns>
    <OperationContract()>
    Function DeletePaymentConcept(paymentConcept As AccountPayableConcepts, audit As AuditMessage) As ActionResult

    ''' <summary>
    ''' Obtiene todos los conceptos de pagos
    ''' </summary>
    ''' <returns>Lista de dependencias</returns>
    <OperationContract()>
    Function ListAllPaymentConcept(session As SessionValues, audit As AuditMessage) As List(Of AccountPayableConcepts)

    ''' <summary>
    ''' Obtiene un determinado concepto de pago 
    ''' </summary>
    ''' <param name="code">The code.</param>
    ''' <returns></returns>
    <OperationContract()>
    Function GetPaymentConcept(code As String, audit As AuditMessage) As ActionResult(Of AccountPayableConcepts)

    ''' <summary>
    ''' Obtiene un determinado concepto de pago 
    ''' </summary>
    ''' <returns></returns>
    <OperationContract()>
    Function GetPaymentConceptById(id As Integer, audit As AuditMessage) As ActionResult(Of Domain.Entities.AccountPayableConcepts)

    ''' <summary>
    ''' Cambia el estado de la entidad
    ''' </summary>
    ''' <param name="code"></param>
    ''' <param name="state"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <OperationContract()>
    Function ChangeStatePaymentConcept(code As String, state As Boolean, audit As AuditMessage) As Domain.Base.Entities.ActionResult(Of Domain.Entities.AccountPayableConcepts)

End Interface
