#Region "Imports"

Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities
Imports System.ServiceModel

#End Region

<ServiceContract()>
Public Interface ICostServiceCostInventoryGroup

    <OperationContract()>
    Function GetCostInventoryGroupByCode(code As String, audit As AuditMessage) As ActionResult(Of CostInventoryGroup)

    <OperationContract()>
    Function SaveCostInventoryGroup(costInventoryGroup As CostInventoryGroup, listCostInventoryGroupDetail As List(Of CostInventoryGroupDetail), audit As AuditMessage) As ActionResult(Of CostInventoryGroup)

    <OperationContract()>
    Function ChangeStateCostInventoryGroup(costInventoryGroup As CostInventoryGroup, audit As AuditMessage) As ActionResult(Of CostInventoryGroup)

#Region "Copy & Paste"

    <OperationContract()>
    Function CopyAndPasteCostInventoryGroup(data As List(Of List(Of String))) As ActionResult(Of List(Of CostInventoryGroupDetail))

#End Region

End Interface
