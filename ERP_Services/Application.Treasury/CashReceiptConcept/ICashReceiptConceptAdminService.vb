'***********************************************************************
' Assembly         : Application.Treasury
' Author           : Diego Andrés Roldán Lozano
' Created          : 03-04-2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities

Public Interface ICashReceiptConceptAdminService
    Inherits IDisposable

    ''' <summary>
    ''' Guarda un concepto de recibo de caja
    ''' </summary>
    ''' <param name="cashReceiptConcept">The cash receipt concept.</param>
    ''' <param name="audit">The audit.</param>
    ''' <returns></returns>
    Function SaveCashReceiptConcept(ByVal cashReceiptConcept As CashReceiptConcepts, ByVal audit As AuditMessage, Optional ByVal idSequence As Int64 = 0) As ActionResult(Of CashReceiptConcepts)

    ''' <summary>
    ''' Actualiza el estado del registro
    ''' </summary>
    ''' <param name="code">The code.</param>
    ''' <param name="state">if set to <c>true</c> [state].</param>
    ''' <param name="audit">The audit.</param>
    ''' <returns></returns>
    Function UpdateStateCashReceiptConcept(ByVal code As String, ByVal state As Boolean, ByVal audit As AuditMessage) As ActionResult(Of CashReceiptConcepts)

    ''' <summary>
    ''' Elimina un concepto de recibo de caja
    ''' </summary>
    ''' <param name="cashReceiptConcept">The cash receipt concept.</param>
    ''' <param name="audit">The audit.</param>
    ''' <returns></returns>
    Function DeleteCashReceiptConcept(ByVal cashReceiptConcept As CashReceiptConcepts, ByVal audit As AuditMessage) As ActionResult

    ''' <summary>
    ''' Obtiene un concepto de recibo de caja
    ''' </summary>
    ''' <param name="code">The code.</param>
    ''' <returns></returns>
    Function GetCashReceiptConcept(ByVal code As String, ByVal audit As AuditMessage) As CashReceiptConcepts

    ''' <summary>
    ''' obtiene un concepto de recibo de caja por id
    ''' </summary>
    ''' <param name="id">The identifier.</param>
    ''' <returns></returns>
    Function GetCashReceiptConceptById(ByVal id) As CashReceiptConcepts

    ''' <summary>
    ''' Obtiene los conceptos de recibo de caja que estan relacionados a un concepto de flujo de caja
    ''' </summary>
    ''' <param name="id"></param>
    ''' <returns></returns>
    Function GetCashReceiptConceptByFlowConcept(id As Integer) As List(Of CashReceiptConcepts)
End Interface
