Imports Application.Cost
Imports Microsoft.Practices.Unity

Partial Public Class CostService
    Implements ICostServiceBlockRecordCost

    Public Function DeleteBlockRecordCost(blockRecordCost As Domain.Entities.BlockRecordCost) As Domain.Base.Entities.ActionResult Implements ICostServiceBlockRecordCost.DeleteBlockRecordCost
        Using service As ICostBlockRecordAdminService = Container.Current.Resolve(Of ICostBlockRecordAdminService)()
            Return service.DeleteBlockRecordCost(blockRecordCost)
        End Using
        'Return _costBlockRecordAdminService.DeleteBlockRecordCost(blockRecordCost)
    End Function

    Public Function GetBlockRecordCostByIdformAndIdRecord(IdForm As String, IdRecord As String, Optional tracking As Boolean = True) As Domain.Entities.BlockRecordCost Implements ICostServiceBlockRecordCost.GetBlockRecordCostByIdformAndIdRecord
        Using service As ICostBlockRecordAdminService = Container.Current.Resolve(Of ICostBlockRecordAdminService)()
            Return service.GetBlockRecordCostByIdformAndIdRecord(IdForm, IdRecord, tracking)
        End Using
        'Return _costBlockRecordAdminService.GetBlockRecordCostByIdformAndIdRecord(IdForm, IdRecord, tracking)
    End Function

    Public Function SaveBlockRecordCost(blockRecordCost As Domain.Entities.BlockRecordCost) As Domain.Base.Entities.ActionResult(Of Domain.Entities.BlockRecordCost) Implements ICostServiceBlockRecordCost.SaveBlockRecordCost
        Using service As ICostBlockRecordAdminService = Container.Current.Resolve(Of ICostBlockRecordAdminService)()
            Return service.SaveBlockRecordCost(blockRecordCost)
        End Using
        'Return _costBlockRecordAdminService.SaveBlockRecordCost(blockRecordCost)
    End Function
End Class
