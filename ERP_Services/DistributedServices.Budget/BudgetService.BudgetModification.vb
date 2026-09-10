'***********************************************************************
' Assembly         : DistributedServices.Budget
' Author           : Jhossept Kevin Garay Rodriguez
' Created          : 04-08-2014
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
    ''' Obtiene una modificación de presupuesto por codigo
    ''' </summary>
    ''' <param name="code"></param>
    ''' <param name="type"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetBudgetModification(code As String, type As Integer, budgetaryValidityId As Integer, audit As AuditMessage) As BudgetModification Implements IBudgetServiceBudgetModification.GetBudgetModification
        Using service As IBudgetModificationAdminService = Container.Current.Resolve(Of IBudgetModificationAdminService)()
            Return service.GetBudgetModification(code.Trim(), type, budgetaryValidityId, audit)
        End Using
    End Function

    ''' <summary>
    ''' Obtiene una modificación de presupuesto por id
    ''' </summary>
    ''' <param name="id"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetBudgetModificationById(id As Integer, audit As AuditMessage) As BudgetModification Implements IBudgetServiceBudgetModification.GetBudgetModificationById
        Using service As IBudgetModificationAdminService = Container.Current.Resolve(Of IBudgetModificationAdminService)()
            Return service.GetBudgetModificationById(id)
        End Using
    End Function

    ''' <summary>
    ''' Guarda una modificacion de presupuesto
    ''' </summary>
    ''' <param name="budgetModification"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SaveBudgetMofication(BudgetModification As BudgetModification, listBudgetModificationDetailDelete As List(Of Integer), audit As AuditMessage) As ActionResult(Of BudgetModification) Implements IBudgetServiceBudgetModification.SaveBudgetMofication
        Using service As IBudgetModificationAdminService = Container.Current.Resolve(Of IBudgetModificationAdminService)()
            Return service.SaveBudgetMofication(BudgetModification, listBudgetModificationDetailDelete, audit)
        End Using
    End Function

    ''' <summary>
    ''' elimina las modificaciones de presupuesto
    ''' </summary>
    ''' <param name="budgetModification"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function DeleteBudgetModification(budgetModification As BudgetModification, audit As AuditMessage) As ActionResult Implements IBudgetServiceBudgetModification.DeleteBudgetModification
        Using service As IBudgetModificationAdminService = Container.Current.Resolve(Of IBudgetModificationAdminService)()
            Return service.DeleteBudgetModification(budgetModification, audit)
        End Using
    End Function

    ''' <summary>
    ''' metodo para cambiar el estado de la entidad
    ''' </summary>
    ''' <param name="code">The code.</param>
    ''' <param name="state">if set to <c>true</c> [state].</param>
    ''' <param name="audit">The audit.</param>
    ''' <returns></returns>
    Public Function ChangeStateBudgetModification(code As String, validity As Integer, state As Integer, budgetaryValidityId As Integer, audit As AuditMessage) As ActionResult(Of BudgetModification) Implements IBudgetServiceBudgetModification.ChangeStateBudgetModification
        Using service As IBudgetModificationAdminService = Container.Current.Resolve(Of IBudgetModificationAdminService)()
            Return service.ChangeStateBudgetModification(code, validity, state, budgetaryValidityId, audit)
        End Using
    End Function

End Class