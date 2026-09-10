Imports System.ServiceModel
Imports Infrastructure.CrossCutting.Base
Imports Domain.Entities
Imports Domain.Base.Entities
Imports Microsoft.Practices.Unity
Imports Application.FixedAsset

Partial Public Class FixedAssetService

    Public Function ConfirmDepreciation(DepreciationMonth As Integer, DepreciationYear As Integer, OperatingUnitId As Integer, audit As AuditMessage) As Domain.Base.Entities.ActionResult(Of Domain.Entities.FixedAssetDepreciation) Implements IFixedAssetDepreciationService.ConfirmDepreciation
        Using service As IFixedAssetDepreciationAdminService = Container.Current.Resolve(Of IFixedAssetDepreciationAdminService)()
            Return service.ConfirmDepreciation(DepreciationMonth, DepreciationYear, OperatingUnitId, audit)
        End Using
        'Return Me._fixedAssetDepreciationAdminService.ConfirmDepreciation(DepreciationMonth, DepreciationYear, OperatingUnitId, audit)
    End Function

    Public Function GetFixedAssetDepreciation(code As String, audit As AuditMessage) As Domain.Base.Entities.ActionResult(Of Domain.Entities.FixedAssetDepreciation) Implements IFixedAssetDepreciationService.GetFixedAssetDepreciation
        Using service As IFixedAssetDepreciationAdminService = Container.Current.Resolve(Of IFixedAssetDepreciationAdminService)()
            Return service.GetFixedAssetDepreciation(code, audit)
        End Using
        'Return Me._fixedAssetDepreciationAdminService.GetFixedAssetDepreciation(code, audit)
    End Function

    Public Function GetFixedAssetDepreciationById(Id As Integer, audit As AuditMessage) As Domain.Base.Entities.ActionResult(Of Domain.Entities.FixedAssetDepreciation) Implements IFixedAssetDepreciationService.GetFixedAssetDepreciationById
        Using service As IFixedAssetDepreciationAdminService = Container.Current.Resolve(Of IFixedAssetDepreciationAdminService)()
            Return service.GetFixedAssetDepreciationById(Id, audit)
        End Using
        'Return Me._fixedAssetDepreciationAdminService.GetFixedAssetDepreciationById(Id, audit)
    End Function

    Public Function SaveDepreciation(DepreciationMonth As Integer, DepreciationYear As Integer, OperatingUnitId As Integer, ModeConfirm As Boolean, audit As AuditMessage) As Domain.Base.Entities.ActionResult(Of Domain.Entities.FixedAssetDepreciation) Implements IFixedAssetDepreciationService.SaveDepreciation
        Using service As IFixedAssetDepreciationAdminService = Container.Current.Resolve(Of IFixedAssetDepreciationAdminService)()
            Return service.SaveDepreciation(DepreciationMonth, DepreciationYear, OperatingUnitId, audit, ModeConfirm)
        End Using
        'Return Me._fixedAssetDepreciationAdminService.SaveDepreciation(DepreciationMonth, DepreciationYear, OperatingUnitId, audit, ModeConfirm)
    End Function

End Class
