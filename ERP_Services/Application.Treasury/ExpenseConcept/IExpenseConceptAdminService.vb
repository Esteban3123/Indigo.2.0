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

Public Interface IExpenseConceptAdminService
    Inherits IDisposable

    ''' <summary>
    ''' Almacena un concepto de egreso
    ''' </summary>
    ''' <param name="expenseConcept">The expense concept.</param>
    ''' <param name="audit">The audit.</param>
    ''' <returns></returns>
    Function SaveExpenseConcept(ByVal expenseConcept As ExpenseConcepts, ByVal audit As AuditMessage, Optional ByVal idSequence As Int64 = 0) As ActionResult(Of ExpenseConcepts)

    ''' <summary>
    ''' Actualiza el estado del registro
    ''' </summary>
    ''' <param name="code">The code.</param>
    ''' <param name="state">if set to <c>true</c> [state].</param>
    ''' <param name="audit">The audit.</param>
    ''' <returns></returns>
    Function UpdateStateExpenseConcept(ByVal code As String, ByVal state As Boolean, ByVal audit As AuditMessage) As ActionResult(Of ExpenseConcepts)

    ''' <summary>
    ''' Elimina un concepto de egreso
    ''' </summary>
    ''' <param name="expenseConcept">The expense concept.</param>
    ''' <param name="audit">The audit.</param>
    ''' <returns></returns>
    Function DeleteExpenseConcept(ByVal expenseConcept As ExpenseConcepts, ByVal audit As AuditMessage) As ActionResult

    ''' <summary>
    ''' Obtiene un concepto de egreso
    ''' </summary>
    ''' <param name="code">The code.</param>
    ''' <returns></returns>
    Function GetExpenseConcept(ByVal code As String, ByVal audit As AuditMessage) As ActionResult(Of ExpenseConcepts)

    ''' <summary>
    ''' Obtiene un concepto de egreso por id
    ''' </summary>
    ''' <param name="Id">The identifier.</param>
    ''' <returns></returns>
    Function GetExpenseConceptById(ByVal Id As Integer, ByVal audit As AuditMessage) As ExpenseConcepts

    ''' <summary>
    ''' Obtiene los conceptos de egreso que estan relacionados a un concepto de flujo de caja
    ''' </summary>
    ''' <param name="id"></param>
    ''' <returns></returns>
    Function GetExpenseConceptByFlowConcept(id As Integer) As List(Of ExpenseConcepts)
End Interface
