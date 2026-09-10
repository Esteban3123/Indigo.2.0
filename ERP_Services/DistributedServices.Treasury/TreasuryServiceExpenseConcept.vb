'***********************************************************************
' Assembly         : DistributedServices.Treasury
' Author           : Diego Andrés Roldán lozano
' Created          : 03-04-2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Entities
Imports Application.Treasury
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities
Imports System.ServiceModel
Imports Microsoft.Practices.Unity

Partial Class TreasuryService

    ''' <summary>
    ''' Elimina un concepto de egreso
    ''' </summary>
    ''' <param name="expenseConcept">The expense concept.</param>
    ''' <returns></returns>
    Public Function DeleteExpenseConcept(expenseConcept As ExpenseConcepts, audit As AuditMessage) As ActionResult Implements ITreasuryServiceExpenseConcept.DeleteExpenseConcept
        Using service As IExpenseConceptAdminService = Container.Current.Resolve(Of IExpenseConceptAdminService)()
            Return service.DeleteExpenseConcept(expenseConcept, audit)
        End Using
        'Return Me._expenseConceptAdminService.DeleteExpenseConcept(expenseConcept, audit)
    End Function

    ''' <summary>
    ''' Obtiene un concepto de egreso
    ''' </summary>
    ''' <param name="code">The code.</param>
    ''' <returns></returns>
    Public Function GetExpenseConcept(code As String, audit As AuditMessage) As ActionResult(Of ExpenseConcepts) Implements ITreasuryServiceExpenseConcept.GetExpenseConcept
        Using service As IExpenseConceptAdminService = Container.Current.Resolve(Of IExpenseConceptAdminService)()
            Return service.GetExpenseConcept(code, audit)
        End Using
        'Return Me._expenseConceptAdminService.GetExpenseConcept(code, audit)
    End Function

    ''' <summary>
    ''' Almacena un concepto de egreso
    ''' </summary>
    ''' <param name="expenseConcept">The expense concept.</param>
    ''' <returns></returns>
    Public Function SaveExpenseConcept(expenseConcept As ExpenseConcepts, idSequence As Int64, audit As AuditMessage) As ActionResult(Of ExpenseConcepts) Implements ITreasuryServiceExpenseConcept.SaveExpenseConcept
        Using service As IExpenseConceptAdminService = Container.Current.Resolve(Of IExpenseConceptAdminService)()
            Return service.SaveExpenseConcept(expenseConcept, audit, idSequence)
        End Using
        'Return Me._expenseConceptAdminService.SaveExpenseConcept(expenseConcept, audit, idSequence)
    End Function

    ''' <summary>
    ''' Updates the state card.
    ''' </summary>
    ''' <param name="code">The code.</param>
    ''' <param name="state">if set to <c>true</c> [state].</param>
    ''' <returns></returns>
    Public Function UpdateStateExpenseConcept(code As String, state As Boolean, audit As AuditMessage) As ActionResult(Of ExpenseConcepts) Implements ITreasuryServiceExpenseConcept.UpdateStateExpenseConcept
        Using service As IExpenseConceptAdminService = Container.Current.Resolve(Of IExpenseConceptAdminService)()
            Return service.UpdateStateExpenseConcept(code, state, audit)
        End Using
        'Return Me._expenseConceptAdminService.UpdateStateExpenseConcept(code, state, audit)
    End Function

    ''' <summary>
    ''' Obtiene un concepto de egreso por id
    ''' </summary>
    ''' <param name="Id">The identifier.</param>
    ''' <returns></returns>
    Public Function GetExpenseConceptById(Id As Integer, audit As AuditMessage) As Domain.Entities.ExpenseConcepts Implements ITreasuryServiceExpenseConcept.GetExpenseConceptById
        Using service As IExpenseConceptAdminService = Container.Current.Resolve(Of IExpenseConceptAdminService)()
            Return service.GetExpenseConceptById(Id, audit)
        End Using
        'Return Me._expenseConceptAdminService.GetExpenseConceptById(Id, audit)
    End Function

End Class