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
    ''' Obtiene una dependencia
    ''' </summary>
    ''' <param name="code"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetBudgetDependency(code As String, validityId As Integer, audit As AuditMessage) As Dependency Implements IBudgetServiceBudgetDependency.GetBudgetDependency
        Using service As IBudgetDependencyAdminService = Container.Current.Resolve(Of IBudgetDependencyAdminService)()
            Return service.GetBudgetDependency(code.Trim(), validityId, audit)
        End Using
    End Function

    ''' <summary>
    ''' Obtiene una dependencia by validity
    ''' </summary>
    ''' <param name="code"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetBudgetDependencyByValidity(code As String, ValidityId As String, audit As AuditMessage) As Dependency Implements IBudgetServiceBudgetDependency.GetBudgetDependencyByValidity
        Using service As IBudgetDependencyAdminService = Container.Current.Resolve(Of IBudgetDependencyAdminService)()
            Return service.GetBudgetDependencyByValidity(code.Trim(), ValidityId, audit)
        End Using
    End Function

    ''' <summary>
    ''' Elimina una dependencia
    ''' </summary>
    ''' <param name="Dependency">La entidad</param>
    ''' <returns></returns>
    ''' <exception cref="System.ArgumentNullException">company Vacio</exception>
    Function DeleteBudgetDependency(Dependency As Dependency, audit As AuditMessage) As ActionResult Implements IBudgetServiceBudgetDependency.DeleteBudgetDependency
        Using service As IBudgetDependencyAdminService = Container.Current.Resolve(Of IBudgetDependencyAdminService)()
            Return service.DeleteBudgetDependency(Dependency, audit)
        End Using
    End Function

    ''' <summary>
    ''' Guarda o Actualiza una dependencia
    ''' </summary>
    ''' <param name="Dependency">la entidad</param>
    ''' <returns></returns>
    ''' <exception cref="System.ArgumentNullException">entidad Vacio</exception>
    Function SaveBudgetDependency(Dependency As Dependency, idSequense As Int64, audit As AuditMessage) As ActionResult(Of Dependency) Implements IBudgetServiceBudgetDependency.SaveBudgetDependency
        Using service As IBudgetDependencyAdminService = Container.Current.Resolve(Of IBudgetDependencyAdminService)()
            Return service.SaveBudgetDependency(Dependency, audit, idSequense)
        End Using
    End Function

    ''' <summary>
    ''' cambia el estado de la entidad.
    ''' </summary>
    ''' <param name="code">The code.</param>
    ''' <param name="state">if set to <c>true</c> [state].</param>
    ''' <returns></returns>
    Public Function ChangeStateBudgetDependency(code As String, validityId As Integer, state As Boolean, audit As AuditMessage) As Domain.Base.Entities.ActionResult(Of Domain.Entities.Dependency) Implements IBudgetServiceBudgetDependency.ChangeStateBudgetDependency
        Using service As IBudgetDependencyAdminService = Container.Current.Resolve(Of IBudgetDependencyAdminService)()
            Return service.ChangeStateBudgetDependency(code, validityId, state, audit)
        End Using
    End Function

    ''' <summary>
    ''' Obtiene una dependencia por Id
    ''' </summary>
    ''' <param name="id"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetBudgetDependencyById(id As Integer, audit As AuditMessage) As Dependency Implements IBudgetServiceBudgetDependency.GetBudgetDependencyById
        Using service As IBudgetDependencyAdminService = Container.Current.Resolve(Of IBudgetDependencyAdminService)()
            Return service.GetBudgetDependencyById(id, audit)
        End Using
    End Function

End Class