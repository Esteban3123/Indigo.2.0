Imports Infrastructure.CrossCutting.Base
Imports Domain.Entities
Imports Domain.Base.Entities
Imports Application.FixedAsset
Imports System.ServiceModel
Imports Microsoft.Practices.Unity

Partial Class FixedAssetService

    Public Function DeleteFixedAssetInventoryType(FixedAssetInventoryType As FixedAssetInventoryType, audit As AuditMessage) As ActionResult Implements IFixedAssetInventoryTypeService.DeleteFixedAssetInventoryType
        Using service As IFixedAssetInventoryTypeAdminService = Container.Current.Resolve(Of IFixedAssetInventoryTypeAdminService)()
            Return service.DeleteFixedAssetInventoryType(FixedAssetInventoryType, audit)
        End Using
        'Return _fixedAssetInventoryTypeAdminService.DeleteFixedAssetInventoryType(FixedAssetInventoryType, audit)
    End Function

    Public Function GetFixedAssetInventoryType(code As String) As FixedAssetInventoryType Implements IFixedAssetInventoryTypeService.GetFixedAssetInventoryType
        Using service As IFixedAssetInventoryTypeAdminService = Container.Current.Resolve(Of IFixedAssetInventoryTypeAdminService)()
            Return service.GetFixedAssetInventoryType(code)
        End Using
        'Return _fixedAssetInventoryTypeAdminService.GetFixedAssetInventoryType(code)
    End Function

    Function ListAllFixedAssetInventoryType() As List(Of FixedAssetInventoryType) Implements IFixedAssetInventoryTypeService.ListAllFixedAssetInventoryType
        Using service As IFixedAssetInventoryTypeAdminService = Container.Current.Resolve(Of IFixedAssetInventoryTypeAdminService)()
            Return service.ListAllFixedAssetInventoryType()
        End Using
        'Return _fixedAssetInventoryTypeAdminService.ListAllFixedAssetInventoryType()
    End Function

    Function SaveFixedAssetInventoryType(FixedAssetInventoryType As FixedAssetInventoryType, idSequense As Int64, audit As AuditMessage) As ActionResult(Of FixedAssetInventoryType) Implements IFixedAssetInventoryTypeService.SaveFixedAssetInventoryType
        Using service As IFixedAssetInventoryTypeAdminService = Container.Current.Resolve(Of IFixedAssetInventoryTypeAdminService)()
            Return service.SaveFixedAssetInventoryType(FixedAssetInventoryType, audit, idSequense)
        End Using
        'Return _fixedAssetInventoryTypeAdminService.SaveFixedAssetInventoryType(FixedAssetInventoryType, audit, idSequense)
    End Function

    Function ChangeFixedAssetInventoryTypeStatus(code As String, state As Boolean, audit As AuditMessage) As ActionResult(Of FixedAssetInventoryType) Implements IFixedAssetInventoryTypeService.ChangeFixedAssetInventoryTypeStatus
        Using service As IFixedAssetInventoryTypeAdminService = Container.Current.Resolve(Of IFixedAssetInventoryTypeAdminService)()
            Return service.ChangeFixedAssetInventoryTypeStatus(code, state, audit)
        End Using
        'Return _fixedAssetInventoryTypeAdminService.ChangeFixedAssetInventoryTypeStatus(code, state, audit)
    End Function

End Class
