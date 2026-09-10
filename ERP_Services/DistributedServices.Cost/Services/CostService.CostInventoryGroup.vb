#Region "Imports"

Imports Infrastructure.CrossCutting.Base
Imports Application.Cost
Imports Microsoft.Practices.Unity
Imports Domain.Entities
Imports Domain.Base.Entities
Imports DistributedServices.Cost

#End Region

Partial Public Class CostService
    Implements ICostServiceCostInventoryGroup

    Public Function GetCostInventoryGroupByCode(code As String, audit As AuditMessage) As ActionResult(Of CostInventoryGroup) Implements ICostServiceCostInventoryGroup.GetCostInventoryGroupByCode
        Using service As ICostInventoryGroupAdminService = Container.Current.Resolve(Of ICostInventoryGroupAdminService)()
            Return service.GetCostInventoryGroupByCode(code, audit)
        End Using
    End Function

    Public Function SaveCostInventoryGroup(costInventoryGroup As CostInventoryGroup, listCostInventoryGroupDetail As List(Of CostInventoryGroupDetail), audit As AuditMessage) As ActionResult(Of CostInventoryGroup) Implements ICostServiceCostInventoryGroup.SaveCostInventoryGroup
        Using service As ICostInventoryGroupAdminService = Container.Current.Resolve(Of ICostInventoryGroupAdminService)()
            Return service.SaveCostInventoryGroup(CostInventoryGroup, listCostInventoryGroupDetail, audit)
        End Using
    End Function

    Public Function ChangeStateCostInventoryGroup(costInventoryGroup As CostInventoryGroup, audit As AuditMessage) As ActionResult(Of CostInventoryGroup) Implements ICostServiceCostInventoryGroup.ChangeStateCostInventoryGroup
        Using service As ICostInventoryGroupAdminService = Container.Current.Resolve(Of ICostInventoryGroupAdminService)()
            Return service.ChangeStateCostInventoryGroup(CostInventoryGroup, audit)
        End Using
    End Function

#Region "Copy & Paste"

    Public Function CopyAndPasteCostInventoryGroup(data As List(Of List(Of String))) As ActionResult(Of List(Of CostInventoryGroupDetail)) Implements ICostServiceCostInventoryGroup.CopyAndPasteCostInventoryGroup
        Using service As ICostInventoryGroupAdminService = Container.Current.Resolve(Of ICostInventoryGroupAdminService)()
            Return service.CopyAndPasteCostInventoryGroupDetail(Data)
        End Using
    End Function

#End Region

End Class
