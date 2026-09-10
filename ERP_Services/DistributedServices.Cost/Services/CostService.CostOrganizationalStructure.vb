Imports Infrastructure.CrossCutting.Base
Imports System.ServiceModel
Imports Application.Cost
Imports Microsoft.Practices.Unity

Partial Public Class CostService
    Implements ICostServiceCostOrganizationalStructure

    Public Function DeleteCostOrganizationalStructure(organizationalStructure As Domain.Entities.CostOrganizationalStructureOfCosts, audit As AuditMessage) As Domain.Base.Entities.ActionResult Implements ICostServiceCostOrganizationalStructure.DeleteCostOrganizationalStructure
        Using service As ICostOrganizationalStructureAdminService = Container.Current.Resolve(Of ICostOrganizationalStructureAdminService)()
            Return service.DeleteCostOrganizationalStructure(organizationalStructure, audit)
        End Using
        'Return _costOrganizationalStructureAdminService.DeleteCostOrganizationalStructure(organizationalStructure, audit)
    End Function

    Public Function GetCostOrganizationalStructure(code As String, audit As AuditMessage) As Domain.Base.Entities.ActionResult(Of Domain.Entities.CostOrganizationalStructureOfCosts) Implements ICostServiceCostOrganizationalStructure.GetCostOrganizationalStructure
        Using service As ICostOrganizationalStructureAdminService = Container.Current.Resolve(Of ICostOrganizationalStructureAdminService)()
            Return service.GetCostOrganizationalStructure(code, audit)
        End Using
        'Return _costOrganizationalStructureAdminService.GetCostOrganizationalStructure(code, audit)
    End Function

    Public Function GetCostOrganizationalStructureById(id As Integer) As Domain.Entities.CostOrganizationalStructureOfCosts Implements ICostServiceCostOrganizationalStructure.GetCostOrganizationalStructureById
        Using service As ICostOrganizationalStructureAdminService = Container.Current.Resolve(Of ICostOrganizationalStructureAdminService)()
            Return service.GetCostOrganizationalStructureById(id)
        End Using
        'Return _costOrganizationalStructureAdminService.GetCostOrganizationalStructureById(id)
    End Function

    Public Function SaveCostOrganizationalStructure(costOrganizationalStructure As Domain.Entities.CostOrganizationalStructureOfCosts, audit As AuditMessage) As Domain.Base.Entities.ActionResult(Of Domain.Entities.CostOrganizationalStructureOfCosts) Implements ICostServiceCostOrganizationalStructure.SaveCostOrganizationalStructure
        Using service As ICostOrganizationalStructureAdminService = Container.Current.Resolve(Of ICostOrganizationalStructureAdminService)()
            Return service.SaveCostOrganizationalStructure(costOrganizationalStructure, audit)
        End Using
        'Return _costOrganizationalStructureAdminService.SaveCostOrganizationalStructure(costOrganizationalStructure, audit)
    End Function

    Public Function UpdateState(code As String, state As Boolean, audit As AuditMessage) As Domain.Base.Entities.ActionResult(Of Domain.Entities.CostOrganizationalStructureOfCosts) Implements ICostServiceCostOrganizationalStructure.UpdateState
        Using service As ICostOrganizationalStructureAdminService = Container.Current.Resolve(Of ICostOrganizationalStructureAdminService)()
            Return service.UpdateState(code, state, audit)
        End Using
        'Return _costOrganizationalStructureAdminService.UpdateState(code, state, audit)
    End Function
End Class
