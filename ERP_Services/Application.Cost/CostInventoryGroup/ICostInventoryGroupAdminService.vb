#Region "Imports"

Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities

#End Region

Public Interface ICostInventoryGroupAdminService
    Inherits IDisposable

    Function GetCostInventoryGroupByCode(code As String, audit As AuditMessage) As ActionResult(Of CostInventoryGroup)

    Function SaveCostInventoryGroup(CostInventoryGroup As CostInventoryGroup, listCostInventoryGroupDetail As List(Of CostInventoryGroupDetail), audit As AuditMessage) As ActionResult(Of CostInventoryGroup)

    Function ChangeStateCostInventoryGroup(CostInventoryGroup As CostInventoryGroup, audit As AuditMessage) As ActionResult(Of CostInventoryGroup)

#Region "Copy & Paste"

    Function CopyAndPasteCostInventoryGroupDetail(data As List(Of List(Of String))) As ActionResult(Of List(Of CostInventoryGroupDetail))

#End Region

End Interface
