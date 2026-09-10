#Region "Imports"

Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities
Imports System.ServiceModel

#End Region

<ServiceContract()>
Public Interface ICostServiceCostActivity

    <OperationContract()>
    Function GetCostActivityById(id As Integer) As ActionResult(Of CostActivity)

    <OperationContract()>
    Function GetCostActivityByCode(code As String, audit As AuditMessage) As ActionResult(Of CostActivity)

    <OperationContract()>
    Function SaveCostActivity(costActivity As CostActivity, 
                                listCostActivityProductionCenter As List(Of CostActivityProductionCenter), listCostActivityStep As List(Of CostActivityStep), 
                                listCostActivityStepFixedAsset As List(Of CostActivityStepFixedAsset), listCostActivityStepPayroll As List(Of CostActivityStepPayroll),
                                listCostActivityStepInventory As List(Of CostActivityStepInventory), listCostActivityStepAddictionalCost As List(Of CostActivityStepAddictionalCost),
                                audit As AuditMessage) As ActionResult(Of CostActivity)

    <OperationContract()>
    Function ChangeStateCostActivity(costActivity As CostActivity, audit As AuditMessage) As ActionResult(Of CostActivity)

#Region "Copy & Paste"

    <OperationContract()>
    Function CostActivityCopyAndPasteProductionCenter(data As List(Of List(Of String))) As ActionResult(Of List(Of CostActivityProductionCenter))

    <OperationContract()>
    Function CostActivityCopyAndPasteFixedAsset(data As List(Of List(Of String))) As ActionResult(Of List(Of CostActivityStepFixedAsset))

    <OperationContract()>
    Function CostActivityCopyAndPastePayroll(data As List(Of List(Of String))) As ActionResult(Of List(Of CostActivityStepPayroll))

    <OperationContract()>
    Function CostActivityCopyAndPasteInventory(data As List(Of List(Of String))) As ActionResult(Of List(Of CostActivityStepInventory))

#End Region

End Interface
