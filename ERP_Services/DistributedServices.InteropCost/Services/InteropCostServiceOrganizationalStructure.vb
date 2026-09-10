'***********************************************************************
' Assembly         : DistributedServices.InteropCost
' Author           : Diego Andrés Roldán lozano
' Created          : 09-12-2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities
Imports System.ServiceModel
Imports Application.InteropCost
Imports Microsoft.Practices.Unity

Partial Class InteropCostService
    Implements IInteropCostServiceOrganizationalStructure

    ''' <summary>
    ''' Elimina una estructura Organizacional
    ''' </summary>
    Public Function DeleteOrganizationalStructure(organizationalStructure As Domain.Entities.OrganizationalStructureOfCosts, audit As AuditMessage) As Domain.Base.Entities.ActionResult Implements IInteropCostServiceOrganizationalStructure.DeleteOrganizationalStructure
        Using service As IOrganizationalStructureAdminService = Container.Current.Resolve(Of IOrganizationalStructureAdminService)()
            Return service.DeleteOrganizationalStructure(organizationalStructure, audit)
        End Using
        'Return Me._organizationalStructure.DeleteOrganizationalStructure(organizationalStructure, audit)
    End Function

    ''' <summary>
    ''' Obtiene una estructura organizacional por codigo
    ''' </summary>
    ''' <param name="code"></param>
    ''' <returns></returns>
    Public Function GetOrganizationalStructure(code As String, audit As AuditMessage) As Domain.Base.Entities.ActionResult(Of Domain.Entities.OrganizationalStructureOfCosts) Implements IInteropCostServiceOrganizationalStructure.GetOrganizationalStructure
        Using service As IOrganizationalStructureAdminService = Container.Current.Resolve(Of IOrganizationalStructureAdminService)()
            Return service.GetOrganizationalStructure(code, audit)
        End Using
        'Return Me._organizationalStructure.GetOrganizationalStructure(code, audit)
    End Function

    ''' <summary>
    ''' Obtiene una estructura organizacional por id
    ''' </summary>
    ''' <param name="id"></param>
    ''' <returns></returns>
    Public Function GetOrganizationalStructureById(id As Integer) As Domain.Entities.OrganizationalStructureOfCosts Implements IInteropCostServiceOrganizationalStructure.GetOrganizationalStructureById
        Using service As IOrganizationalStructureAdminService = Container.Current.Resolve(Of IOrganizationalStructureAdminService)()
            Return service.GetOrganizationalStructureById(id)
        End Using
        'Return Me._organizationalStructure.GetOrganizationalStructureById(id)
    End Function

    ''' <summary>
    ''' Guarda una estructura Organizacional
    ''' </summary>
    Public Function SaveOrganizationalStructure(organizationalStructure As Domain.Entities.OrganizationalStructureOfCosts, audit As AuditMessage) As Domain.Base.Entities.ActionResult(Of Domain.Entities.OrganizationalStructureOfCosts) Implements IInteropCostServiceOrganizationalStructure.SaveOrganizationalStructure
        Using service As IOrganizationalStructureAdminService = Container.Current.Resolve(Of IOrganizationalStructureAdminService)()
            Return service.SaveOrganizationalStructure(organizationalStructure, audit)
        End Using
        'Return Me._organizationalStructure.SaveOrganizationalStructure(organizationalStructure, audit)
    End Function

    ''' <summary>
    ''' Updates the state.
    ''' </summary>
    ''' <param name="code"></param>
    ''' <param name="state"></param>
    ''' <returns></returns>
    Public Function UpdateState(code As String, state As Boolean, audit As AuditMessage) As ActionResult(Of OrganizationalStructureOfCosts) Implements IInteropCostServiceOrganizationalStructure.UpdateState
        Using service As IOrganizationalStructureAdminService = Container.Current.Resolve(Of IOrganizationalStructureAdminService)()
            Return service.UpdateState(code, state, audit)
        End Using
        'Return Me._organizationalStructure.UpdateState(code, state, audit)
    End Function

End Class