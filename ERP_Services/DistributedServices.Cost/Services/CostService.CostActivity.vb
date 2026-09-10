#Region "Imports"

Imports Infrastructure.CrossCutting.Base
Imports Application.Cost
Imports Microsoft.Practices.Unity
Imports Domain.Entities
Imports Domain.Base.Entities

#End Region

Partial Public Class CostService
    Implements ICostServiceCostActivity

    Public Function GetCostActivityById(id As Integer) As ActionResult(Of CostActivity) Implements ICostServiceCostActivity.GetCostActivityById
        Using service As ICostActivityAdminService = Container.Current.Resolve(Of ICostActivityAdminService)()
            Return service.GetCostActivityById(id)
        End Using
    End Function

    Function GetCostActivityByCode(code As String, audit As AuditMessage) As ActionResult(Of CostActivity) Implements ICostServiceCostActivity.GetCostActivityByCode
        Using service As ICostActivityAdminService = Container.Current.Resolve(Of ICostActivityAdminService)()
            Return service.GetCostActivityByCode(code, audit)
        End Using
    End Function

    Function SaveCostActivity(costActivity As CostActivity, 
                                listCostActivityProductionCenter As List(Of CostActivityProductionCenter), listCostActivityStep As List(Of CostActivityStep), 
                                listCostActivityStepFixedAsset As List(Of CostActivityStepFixedAsset), listCostActivityStepPayroll As List(Of CostActivityStepPayroll),
                                listCostActivityStepInventory As List(Of CostActivityStepInventory), listCostActivityStepAddictionalCost As List(Of CostActivityStepAddictionalCost),
                                audit As AuditMessage) As ActionResult(Of CostActivity) Implements ICostServiceCostActivity.SaveCostActivity
        Using service As ICostActivityAdminService = Container.Current.Resolve(Of ICostActivityAdminService)()
            Return service.SaveCostActivity(costActivity, listCostActivityProductionCenter, listCostActivityStep, listCostActivityStepFixedAsset, listCostActivityStepPayroll, listCostActivityStepInventory, listCostActivityStepAddictionalCost, audit)
        End Using
    End Function

    Function ChangeStateCostActivity(costActivity As CostActivity, audit As AuditMessage) As ActionResult(Of CostActivity) Implements ICostServiceCostActivity.ChangeStateCostActivity
        Using service As ICostActivityAdminService = Container.Current.Resolve(Of ICostActivityAdminService)()
            Return service.ChangeStateCostActivity(costActivity, audit)
        End Using
    End Function

#Region "Copy & Paste"

    Function CostActivityCopyAndPasteProductionCenter(data As List(Of List(Of String))) As ActionResult(Of List(Of CostActivityProductionCenter))  Implements ICostServiceCostActivity.CostActivityCopyAndPasteProductionCenter
        Using service As ICostActivityAdminService = Container.Current.Resolve(Of ICostActivityAdminService)()
            Return service.CopyAndPasteProductionCenter(data)
        End Using
    End Function

    Function CostActivityCopyAndPasteFixedAsset(Data As List(Of List(Of String))) As ActionResult(Of List(Of CostActivityStepFixedAsset)) Implements ICostServiceCostActivity.CostActivityCopyAndPasteFixedAsset
        Using service As ICostActivityAdminService = Container.Current.Resolve(Of ICostActivityAdminService)()
            Return service.CopyAndPasteFixedAsset(data)
        End Using
    End Function

    Function CostActivityCopyAndPastePayroll(Data As List(Of List(Of String))) As ActionResult(Of List(Of CostActivityStepPayroll)) Implements ICostServiceCostActivity.CostActivityCopyAndPastePayroll
        Using service As ICostActivityAdminService = Container.Current.Resolve(Of ICostActivityAdminService)()
            Return service.CopyAndPastePayroll(data)
        End Using
    End Function

    Function CostActivityCopyAndPasteInventory(Data As List(Of List(Of String))) As ActionResult(Of List(Of CostActivityStepInventory)) Implements ICostServiceCostActivity.CostActivityCopyAndPasteInventory
        Using service As ICostActivityAdminService = Container.Current.Resolve(Of ICostActivityAdminService)()
            Return service.CopyAndPasteInventory(data)
        End Using
    End Function

#End Region

End Class
