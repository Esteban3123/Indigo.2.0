
Imports Application.FixedAsset
Imports Domain.Entities
Imports Domain.Base.Entities
Imports Infrastructure.CrossCutting.Base
Imports Microsoft.Practices.Unity

Partial Class FixedAssetService

    Public Function DeleteSettingFixedAsset(ByVal SettingFixedAsset As SettingFixedAsset, ByVal audit As AuditMessage) As Boolean Implements ISettingFixedAssetService.DeleteSettingFixedAsset
        Using service As ISettingFixedAssetAdminService = Container.Current.Resolve(Of ISettingFixedAssetAdminService)()
            Return service.DeleteSettingFixedAsset(SettingFixedAsset, audit)
        End Using
        'Return _SettingFixedAssetAdminService.DeleteSettingFixedAsset(SettingFixedAsset, audit)
    End Function

    Public Function GetSettingFixedAssetByOperatingUnitId(OperatingUnitId As Integer, audit As Infrastructure.CrossCutting.Base.AuditMessage) As Domain.Entities.SettingFixedAsset Implements ISettingFixedAssetService.GetSettingFixedAssetByOperatingUnitId
        Using service As ISettingFixedAssetAdminService = Container.Current.Resolve(Of ISettingFixedAssetAdminService)()
            Return service.GetSettingFixedAssetByOperatingUnitId(OperatingUnitId)
        End Using
        'Return _SettingFixedAssetAdminService.GetSettingFixedAssetByOperatingUnitId(OperatingUnitId)
    End Function

    Public Function SaveSettingFixedAsset(ByVal SettingFixedAsset As SettingFixedAsset, ByVal audit As AuditMessage) As ActionResult(Of SettingFixedAsset) Implements ISettingFixedAssetService.SaveSettingFixedAsset
        Using service As ISettingFixedAssetAdminService = Container.Current.Resolve(Of ISettingFixedAssetAdminService)()
            Return service.SaveSettingFixedAsset(SettingFixedAsset, audit)
        End Using
        'Return _SettingFixedAssetAdminService.SaveSettingFixedAsset(SettingFixedAsset, audit)
    End Function
End Class
