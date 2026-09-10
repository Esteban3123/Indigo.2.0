'***********************************************************************
' Assembly         : DistributedServices.Treasury
' Author           : Diego Andrés Roldán Lozano
' Created          : 03-04-2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities
Imports System.ServiceModel

<ServiceContract()>
Public Interface ITreasuryServiceCashReceiptConcept

    ''' <summary>
    ''' Guarda un concepto de recibo de caja
    ''' </summary>
    ''' <param name="cashReceiptConcept">The cash receipt concept.</param>
    ''' <returns></returns>
    <OperationContract()>
    Function SaveCashReceiptConcept(cashReceiptConcept As CashReceiptConcepts, idSequence As Int64, audit As AuditMessage) As ActionResult(Of CashReceiptConcepts)

    ''' <summary>
    ''' Actualiza el estado del registro
    ''' </summary>
    ''' <param name="code">The code.</param>
    ''' <param name="state">if set to <c>true</c> [state].</param>
    ''' <returns></returns>
    <OperationContract()>
    Function UpdateStateCashReceiptConcept(code As String, state As Boolean, audit As AuditMessage) As Domain.Base.Entities.ActionResult(Of Domain.Entities.CashReceiptConcepts)

    ''' <summary>
    ''' Elimina un concepto de recibo de caja
    ''' </summary>
    ''' <param name="cashReceiptConcept">The cash receipt concept.</param>
    ''' <returns></returns>
    <OperationContract()>
    Function DeleteCashReceiptConcept(cashReceiptConcept As CashReceiptConcepts, audit As AuditMessage) As ActionResult

    ''' <summary>
    ''' Obtiene un concepto de recibo de caja
    ''' </summary>
    ''' <param name="code">The code.</param>
    ''' <returns></returns>
    <OperationContract()>
    Function GetCashReceiptConcept(code As String, audit As AuditMessage) As CashReceiptConcepts

    ''' <summary>
    ''' Obtiene un concepto de recibo de caja por id
    ''' </summary>
    ''' <param name="code">The code.</param>
    ''' <returns></returns>
    <OperationContract()>
    Function GetCashReceiptConceptById(ByVal id As Integer) As CashReceiptConcepts

End Interface
