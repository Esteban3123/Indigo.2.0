'***********************************************************************
' Assembly         : DistributedServices.Budget
' Author           : Jhossept Kevin Garay Rodriguez
' Created          : 10-04-2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"

Imports Application.Budget
Imports Domain.Base.Entities
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Microsoft.Practices.Unity

#End Region

Partial Class BudgetService

    ''' <summary>
    ''' Obtiene un concepto
    ''' </summary>
    ''' <param name="code"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetBudgetConcept(code As String, validityId As Integer, audit As AuditMessage) As Concept Implements IBudgetServiceBudgetConcept.GetBudgetConcept
        Using service As IBudgetConceptAdminService = Container.Current.Resolve(Of IBudgetConceptAdminService)()
            Return service.GetBudgetConcept(code.Trim(), validityId, audit)
        End Using
    End Function

    ''' <summary>
    ''' Obtiene un concepto by validity
    ''' </summary>
    ''' <param name="code"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetBudgetConceptByValidity(code As String, ValidityId As String, audit As AuditMessage) As Concept Implements IBudgetServiceBudgetConcept.GetBudgetConceptByValidity
        Using service As IBudgetConceptAdminService = Container.Current.Resolve(Of IBudgetConceptAdminService)()
            Return service.GetBudgetConceptByValidity(code.Trim(), ValidityId, audit)
        End Using
    End Function

    ''' <summary>
    ''' Elimina un concepto
    ''' </summary>
    ''' <param name="Concept">La entidad</param>
    ''' <returns></returns>
    ''' <exception cref="System.ArgumentNullException">company Vacio</exception>
    Function DeleteBudgetConcept(Concept As Concept, audit As AuditMessage) As ActionResult Implements IBudgetServiceBudgetConcept.DeleteBudgetConcept
        Using service As IBudgetConceptAdminService = Container.Current.Resolve(Of IBudgetConceptAdminService)()
            Return service.DeleteBudgetConcept(Concept, audit)
        End Using
    End Function

    ''' <summary>
    ''' Guarda o Actualiza un concepto
    ''' </summary>
    ''' <param name="Concept">la entidad</param>
    ''' <returns></returns>
    ''' <exception cref="System.ArgumentNullException">entidad Vacio</exception>
    Function SaveBudgetConcept(Concept As Concept, idSequense As Int64, audit As AuditMessage) As ActionResult(Of Concept) Implements IBudgetServiceBudgetConcept.SaveBudgetConcept
        Using service As IBudgetConceptAdminService = Container.Current.Resolve(Of IBudgetConceptAdminService)()
            Return service.SaveBudgetConcept(Concept, audit, idSequense)
        End Using
    End Function

    ''' <summary>
    ''' cambia el estado de la entidad.
    ''' </summary>
    ''' <param name="code">The code.</param>
    ''' <param name="state">if set to <c>true</c> [state].</param>
    ''' <returns></returns>
    Public Function ChangeStateBudgetConcept(code As String, validityId As Integer, state As Boolean, audit As AuditMessage) As Domain.Base.Entities.ActionResult(Of Domain.Entities.Concept) Implements IBudgetServiceBudgetConcept.ChangeStateBudgetConcept
        Using service As IBudgetConceptAdminService = Container.Current.Resolve(Of IBudgetConceptAdminService)()
            Return service.ChangeStateBudgetConcept(code, validityId, state, audit)
        End Using
    End Function

End Class