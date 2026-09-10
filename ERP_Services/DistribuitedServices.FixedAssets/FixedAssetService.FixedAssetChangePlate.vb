Imports System.ServiceModel
Imports Infrastructure.CrossCutting.Base
Imports Domain.Entities
Imports Domain.Base.Entities
Imports Microsoft.Practices.Unity
Imports Application.FixedAsset

Partial Public Class FixedAssetService

    Public Function ConfirmFixedAssetChangePlate(FixedAssetChangePlate As Domain.Entities.FixedAssetChangePlate, idSequense As Int64, audit As AuditMessage) As Domain.Base.Entities.ActionResult(Of Domain.Entities.FixedAssetChangePlate) Implements IFixedAssetChangePlateService.ConfirmFixedAssetChangePlate
        Using service As IFixedAssetChangePlateAdminService = Container.Current.Resolve(Of IFixedAssetChangePlateAdminService)()
            Return service.ConfirmFixedAssetChangePlate(FixedAssetChangePlate, audit, idSequense)
        End Using
        'Return Me._fixedAssetChangePlateAdminService.ConfirmFixedAssetChangePlate(FixedAssetChangePlate, audit, idSequense)
    End Function

    Public Function GetFixedAssetChangePlate(code As String, audit As AuditMessage) As Domain.Base.Entities.ActionResult(Of Domain.Entities.FixedAssetChangePlate) Implements IFixedAssetChangePlateService.GetFixedAssetChangePlate
        Using service As IFixedAssetChangePlateAdminService = Container.Current.Resolve(Of IFixedAssetChangePlateAdminService)()
            Return service.GetFixedAssetChangePlate(code, audit)
        End Using
        'Return Me._fixedAssetChangePlateAdminService.GetFixedAssetChangePlate(code, audit)
    End Function

    Public Function GetFixedAssetChangePlateById(Id As Integer, audit As AuditMessage) As Domain.Base.Entities.ActionResult(Of Domain.Entities.FixedAssetChangePlate) Implements IFixedAssetChangePlateService.GetFixedAssetChangePlateById
        Using service As IFixedAssetChangePlateAdminService = Container.Current.Resolve(Of IFixedAssetChangePlateAdminService)()
            Return service.GetFixedAssetChangePlateById(Id, audit)
        End Using
        'Return Me._fixedAssetChangePlateAdminService.GetFixedAssetChangePlateById(Id, audit)
    End Function

    Public Function SaveFixedAssetChangePlate(FixedAssetChangePlate As Domain.Entities.FixedAssetChangePlate, idSequense As Int64, audit As AuditMessage) As Domain.Base.Entities.ActionResult(Of Domain.Entities.FixedAssetChangePlate) Implements IFixedAssetChangePlateService.SaveFixedAssetChangePlate
        Using service As IFixedAssetChangePlateAdminService = Container.Current.Resolve(Of IFixedAssetChangePlateAdminService)()
            Return service.SaveFixedAssetChangePlate(FixedAssetChangePlate, audit, idSequense)
        End Using
        'Return Me._fixedAssetChangePlateAdminService.SaveFixedAssetChangePlate(FixedAssetChangePlate, audit, idSequense)
    End Function

End Class
