'***********************************************************************
' Assembly         : DistributedServices.Budget
' Author           : Jhossept Kevin Garay Rodriguez
' Created          : 04-04-2014
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
    ''' Obtiene una entidad presupuestal por codigo
    ''' </summary>
    ''' <param name="code">The code.</param>
    ''' <param name="audit">objeto de auditoria</param>
    ''' <returns></returns>
    Function GetBudgetInstitution(code As String, audit As AuditMessage) As BudgetaryEntity Implements IBudgetServiceBudgetInstitution.GetBudgetInstitution
        Using service As IBudgetInstitutionAdminService = Container.Current.Resolve(Of IBudgetInstitutionAdminService)()
            Return service.GetBudgetInstitution(code.Trim(), audit)
        End Using
    End Function

    ''' <summary>
    ''' Guarda o Actualiza una Entidad Presupuestal
    ''' </summary>
    ''' <param name="BudgetaryEntity">la entidad</param>
    ''' <param name="audit">The audit.</param>
    ''' <returns></returns>
    ''' <exception cref="System.ArgumentNullException">entidad Vacio</exception>
    Function SaveBudgetInstitution(BudgetaryEntity As BudgetaryEntity, audit As AuditMessage) As ActionResult(Of BudgetaryEntity) Implements IBudgetServiceBudgetInstitution.SaveBudgetInstitution
        Using service As IBudgetInstitutionAdminService = Container.Current.Resolve(Of IBudgetInstitutionAdminService)()
            Return service.SaveBudgetInstitution(BudgetaryEntity, audit)
        End Using
    End Function

    ''' <summary>
    ''' Elimina una entidad presupuestal
    ''' </summary>
    ''' <param name="BudgetaryEntity">La entidad</param>
    ''' <param name="audit">The audit.</param>
    ''' <returns></returns>
    ''' <exception cref="System.ArgumentNullException">company Vacio</exception>
    Function DeleteBudgetInstitution(BudgetaryEntity As BudgetaryEntity, audit As AuditMessage) As ActionResult Implements IBudgetServiceBudgetInstitution.DeleteBudgetInstitution
        Using service As IBudgetInstitutionAdminService = Container.Current.Resolve(Of IBudgetInstitutionAdminService)()
            Return service.DeleteBudgetInstitution(BudgetaryEntity, audit)
        End Using
    End Function

    ''' <summary>
    ''' metodo para cambiar el estado de la entidad
    ''' </summary>
    ''' <param name="code">The code.</param>
    ''' <param name="state">if set to <c>true</c> [state].</param>
    ''' <returns></returns>
    Public Function ChangeStateBudgetInstitution(code As String, state As Boolean, audit As AuditMessage) As ActionResult(Of BudgetaryEntity) Implements IBudgetServiceBudgetInstitution.ChangeStateBudgetInstitution
        Using service As IBudgetInstitutionAdminService = Container.Current.Resolve(Of IBudgetInstitutionAdminService)()
            Return service.ChangeStateBudgetInstitution(code, state, audit)
        End Using
    End Function

End Class