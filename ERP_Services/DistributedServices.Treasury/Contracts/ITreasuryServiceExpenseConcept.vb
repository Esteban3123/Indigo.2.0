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
Public Interface ITreasuryServiceExpenseConcept

    ''' <summary>
    ''' Almacena un concepto de egreso
    ''' </summary>
    ''' <param name="expenseConcept">The expense concept.</param>
    ''' <returns></returns>
    <OperationContract()>
    Function SaveExpenseConcept(expenseConcept As ExpenseConcepts, idSequence As Int64, audit As AuditMessage) As ActionResult(Of ExpenseConcepts)

    ''' <summary>
    ''' Updates the state card.
    ''' </summary>
    ''' <param name="code">The code.</param>
    ''' <param name="state">if set to <c>true</c> [state].</param>
    ''' <returns></returns>
    <OperationContract()>
    Function UpdateStateExpenseConcept(code As String, state As Boolean, audit As AuditMessage) As ActionResult(Of ExpenseConcepts)

    ''' <summary>
    ''' Elimina un concepto de egreso
    ''' </summary>
    ''' <param name="expenseConcept">The expense concept.</param>
    ''' <returns></returns>
    <OperationContract()>
    Function DeleteExpenseConcept(expenseConcept As ExpenseConcepts, audit As AuditMessage) As ActionResult

    ''' <summary>
    ''' Obtiene un concepto de egreso
    ''' </summary>
    ''' <param name="code">The code.</param>
    ''' <returns></returns>
    <OperationContract()>
    Function GetExpenseConcept(code As String, audit As AuditMessage) As ActionResult(Of ExpenseConcepts)

    ''' <summary>
    ''' Obtiene un concepto de egreso por id
    ''' </summary>
    ''' <param name="Id">The identifier.</param>
    ''' <returns></returns>
    <OperationContract()>
    Function GetExpenseConceptById(Id As Integer, audit As AuditMessage) As Domain.Entities.ExpenseConcepts

End Interface
