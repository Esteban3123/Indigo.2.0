'***********************************************************************
' Assembly         : DistributedServices.FixedAsset
' Author           : Andres Alarcon
' Created          : 10/06/2024
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"

Imports Infrastructure.CrossCutting.Base
Imports Application.FixedAsset
Imports Microsoft.Practices.Unity
Imports Domain.Base.Entities
Imports Domain.Entities

#End Region

Partial Public Class FixedAssetService

    Public Function GetFixedAssetCatalogOfPropertyandServicesByCode(code As String, audit As AuditMessage) As ActionResult(Of FixedAssetCatalogOfPropertyandServices) Implements IFixedAssetCatalogOfPropertyandServicesService.GetFixedAssetCatalogOfPropertyandServicesByCode
        Using service As IFixedAssetCatalogOfPropertyandServicesAdminService = Container.Current.Resolve(Of IFixedAssetCatalogOfPropertyandServicesAdminService)()
            Return service.GetFixedAssetCatalogOfPropertyandServicesByCode(code, audit)
        End Using
    End Function

    Public Function SaveFixedAssetCatalogOfPropertyandServices(FixedAssetCatalogOfPropertyandServices As FixedAssetCatalogOfPropertyandServices, idSequense As Int64, audit As AuditMessage) As ActionResult(Of FixedAssetCatalogOfPropertyandServices) Implements IFixedAssetCatalogOfPropertyandServicesService.SaveFixedAssetCatalogOfPropertyandServices
        Using service As IFixedAssetCatalogOfPropertyandServicesAdminService = Container.Current.Resolve(Of IFixedAssetCatalogOfPropertyandServicesAdminService)()
            Return service.SaveFixedAssetCatalogOfPropertyandServices(FixedAssetCatalogOfPropertyandServices, audit, idSequense)
        End Using
    End Function

    Public Function DeleteFixedAssetCatalogOfPropertyandServices(FixedAssetCatalogOfPropertyandServices As FixedAssetCatalogOfPropertyandServices, audit As AuditMessage) As ActionResult Implements IFixedAssetCatalogOfPropertyandServicesService.DeleteFixedAssetCatalogOfPropertyandServices
        Using service As IFixedAssetCatalogOfPropertyandServicesAdminService = Container.Current.Resolve(Of IFixedAssetCatalogOfPropertyandServicesAdminService)()
            Return service.DeleteCatalogOfPropertyandServices(FixedAssetCatalogOfPropertyandServices, audit)
        End Using
    End Function

    Public Function ChangeFixedAssetCatalogOfPropertyandServicesStatus(Code As String, State As Boolean, audit As AuditMessage) As ActionResult(Of FixedAssetCatalogOfPropertyandServices) Implements IFixedAssetCatalogOfPropertyandServicesService.ChangeFixedAssetCatalogOfPropertyandServicesStatus
        Using service As IFixedAssetCatalogOfPropertyandServicesAdminService = Container.Current.Resolve(Of IFixedAssetCatalogOfPropertyandServicesAdminService)()
            Return service.ChangeCatalogOfPropertyandServicesStatus(Code, State, audit)
        End Using
    End Function

End Class
