Imports Infrastructure.CrossCutting.Base
Imports System.ServiceModel
Imports Application.Cost
Imports Microsoft.Practices.Unity

Partial Public Class CostService
    Implements ICostServiceCostSetting

    Public Function DeleteCostSetting(costSetting As Domain.Entities.CostSetting, audit As AuditMessage) As Domain.Base.Entities.ActionResult Implements ICostServiceCostSetting.DeleteCostSetting
        Using service As ICostSettingAdminService = Container.Current.Resolve(Of ICostSettingAdminService)()
            Return service.DeleteCostSetting(costSetting, audit)
        End Using
        'Return _costSettingAdminService.DeleteCostSetting(costSetting, audit)
    End Function

    Public Function GetCostSetting() As Domain.Entities.CostSetting Implements ICostServiceCostSetting.GetCostSetting
        Using service As ICostSettingAdminService = Container.Current.Resolve(Of ICostSettingAdminService)()
            Return service.GetCostSetting()
        End Using
        'Return _costSettingAdminService.GetCostSetting()
    End Function

    Public Function GetCostSettingById(id As Integer) As Domain.Entities.CostSetting Implements ICostServiceCostSetting.GetCostSettingById
        Using service As ICostSettingAdminService = Container.Current.Resolve(Of ICostSettingAdminService)()
            Return service.GetCostSettingById(id)
        End Using
        'Return _costSettingAdminService.GetCostSettingById(id)
    End Function

    Public Function SaveCostSetting(costSetting As Domain.Entities.CostSetting, audit As AuditMessage) As Domain.Base.Entities.ActionResult(Of Domain.Entities.CostSetting) Implements ICostServiceCostSetting.SaveCostSetting
        Using service As ICostSettingAdminService = Container.Current.Resolve(Of ICostSettingAdminService)()
            Return service.SaveCostSetting(costSetting, audit)
        End Using
        'Return _costSettingAdminService.SaveCostSetting(costSetting, audit)
    End Function
End Class
