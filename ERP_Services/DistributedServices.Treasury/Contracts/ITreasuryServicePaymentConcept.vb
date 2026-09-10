'***********************************************************************
' Assembly         : DistributedServices.Treasury
' Author           : Diego Andrés Roldán Lozano
' Created          : 13-05-2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities
Imports System.ServiceModel

<ServiceContract()>
Public Interface ITreasuryServicePaymentConcept

    ''' <summary>
    ''' Saves the payment concept.
    ''' </summary>
    ''' <returns></returns>
    <OperationContract()>
    Function SavePaymentConcept(paymentConcept As Domain.Entities.TreasuryPaymentConcepts, audit As AuditMessage, idSequence As Int64) As Domain.Base.Entities.ActionResult(Of Domain.Entities.TreasuryPaymentConcepts)

    ''' <summary>
    ''' Updates the state payment concept.
    ''' </summary>
    ''' <param name="code">The code.</param>
    ''' <param name="state">if set to <c>true</c> [state].</param>
    ''' <returns></returns>
    <OperationContract()>
    Function UpdateStatePaymentConcept(code As String, state As Boolean, audit As AuditMessage) As Domain.Base.Entities.ActionResult(Of Domain.Entities.TreasuryPaymentConcepts)

    ''' <summary>
    ''' Deletes the payment concept.
    ''' </summary>
    ''' <returns></returns>
    <OperationContract()>
    Function DeletePaymentConcept(paymentConcept As Domain.Entities.TreasuryPaymentConcepts, audit As AuditMessage) As Domain.Base.Entities.ActionResult

    ''' <summary>
    ''' Gets the payment concept.
    ''' </summary>
    ''' <param name="code">The code.</param>
    ''' <returns></returns>
    <OperationContract()>
    Function GetPaymentConcept(code As String, audit As AuditMessage) As ActionResult(Of TreasuryPaymentConcepts)

End Interface
