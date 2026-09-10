#Region "Imports"

Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities

#End Region

Public Interface ICostActivityAdminService
    Inherits IDisposable

    Function GetCostActivityById(id As Integer) As ActionResult(Of CostActivity)

    Function GetCostActivityByCode(code As String, audit As AuditMessage) As ActionResult(Of CostActivity)

    Function SaveCostActivity(costActivity As CostActivity, 
                                listCostActivityProductionCenter As List(Of CostActivityProductionCenter), listCostActivityStep As List(Of CostActivityStep), 
                                listCostActivityStepFixedAsset As List(Of CostActivityStepFixedAsset), listCostActivityStepPayroll As List(Of CostActivityStepPayroll),
                                listCostActivityStepInventory As List(Of CostActivityStepInventory), listCostActivityStepAddictionalCost As List(Of CostActivityStepAddictionalCost),
                                audit As AuditMessage) As ActionResult(Of CostActivity)

    Function ChangeStateCostActivity(costActivity As CostActivity, audit As AuditMessage) As ActionResult(Of CostActivity)

#Region "Copy & Paste"

    Function CopyAndPasteProductionCenter(data As List(Of List(Of String))) As ActionResult(Of List(Of CostActivityProductionCenter))

    Function CopyAndPasteFixedAsset(data As List(Of List(Of String))) As ActionResult(Of List(Of CostActivityStepFixedAsset))

    Function CopyAndPastePayroll(data As List(Of List(Of String))) As ActionResult(Of List(Of CostActivityStepPayroll))

    Function CopyAndPasteInventory(data As List(Of List(Of String))) As ActionResult(Of List(Of CostActivityStepInventory))

#End Region

End Interface
