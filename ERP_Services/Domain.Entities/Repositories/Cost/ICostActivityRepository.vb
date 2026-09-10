#Region "Imports"

Imports Domain.Base

#End Region

Public Interface ICostActivityRepository
    Inherits IRepository(Of CostActivity)

    Function GetCostActivityById(id As Integer) As CostActivity

    Function GetCostActivityByIdWithAggregates(id As Integer) As CostActivity

    Function GetCostActivityByCode(code As String) As CostActivity

    Function SP_SaveCostActivity(costActivityXml As String, listCostActivityProductionCenterXml As String, listCostActivityStepXml As String, listCostActivityStepFixedAssetXml As String, listCostActivityStepPayrollXml As String, listCostActivityStepInventoryXml As String, listCostActivityStepAddictionalCostXml As String, codeUser As String) As List(Of SP_SaveCostActivity_Result)

    Function ValidateBeforeActive(costActivityId As Integer, CUPSEntityId As Integer) As String

#Region "Copy & Paste"

    Function SP_CostActivityCopyAndPasteProductionCenter(XmlObject As String) As List(Of SP_CostActivityCopyAndPasteProductionCenter_Result)

    Function SP_CostActivityCopyAndPasteFixedAsset(XmlObject As String) As List(Of SP_CostActivityCopyAndPasteFixedAsset_Result)

    Function SP_CostActivityCopyAndPastePayroll(XmlObject As String) As List(Of SP_CostActivityCopyAndPastePayroll_Result)

    Function SP_CostActivityCopyAndPasteInventory(XmlObject As String) As List(Of SP_CostActivityCopyAndPasteInventory_Result)

#End Region

End Interface
