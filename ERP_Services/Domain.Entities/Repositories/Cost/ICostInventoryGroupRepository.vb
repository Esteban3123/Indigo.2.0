#Region "Imports"

Imports Domain.Base

#End Region

Public Interface ICostInventoryGroupRepository
    Inherits IRepository(Of CostInventoryGroup)

    Function GetCostInventoryGroupById(id As Integer) As CostInventoryGroup

    Function GetCostInventoryGroupByCode(code As String) As CostInventoryGroup

    Function SP_SaveCostInventoryGroup(CostInventoryGroupXml As String, listCostInventoryGroupDetailXml As String, codeUser As String) As List(Of SP_SaveCostInventoryGroup_Result)

    Function ValidateBeforeActive(CostInventoryGroupId As Integer) As String

#Region "Copy & Paste"

    Function SP_CopyAndPasteCostInventoryGroupDetail(XmlObject As String) As List(Of SP_CopyAndPasteCostInventoryGroupDetail_Result)

#End Region

End Interface
