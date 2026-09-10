Imports Infrastructure.CrossCutting.Base
Imports Microsoft.Practices.Unity
Imports Application.FixedAsset

Partial Public Class FixedAssetService

    Public Function GetFixedAssetReclassificationById(Id As Integer, audit As AuditMessage) As Domain.Base.Entities.ActionResult(Of Domain.Entities.FixedAssetReclassification) Implements IFixedAssetReclassificationService.GetFixedAssetReclassificationById
        Using service As IFixedAssetReclassificationAdminService = Container.Current.Resolve(Of IFixedAssetReclassificationAdminService)()
            Return service.GetFixedAssetReclassificationById(Id, audit)
        End Using
    End Function

    Public Function GetFixedAssetReclassificationByCode(Code As String, audit As AuditMessage) As Domain.Base.Entities.ActionResult(Of Domain.Entities.FixedAssetReclassification) Implements IFixedAssetReclassificationService.GetFixedAssetReclassificationByCode
        Using service As IFixedAssetReclassificationAdminService = Container.Current.Resolve(Of IFixedAssetReclassificationAdminService)()
            Return service.GetFixedAssetReclassificationByCode(Code, audit)
        End Using
    End Function

    Public Function SaveFixedAssetReclassification(FixedAssetReclassification As Domain.Entities.FixedAssetReclassification, idSequense As Int64, audit As AuditMessage) As Domain.Base.Entities.ActionResult(Of Domain.Entities.FixedAssetReclassification) Implements IFixedAssetReclassificationService.SaveFixedAssetReclassification
        Using service As IFixedAssetReclassificationAdminService = Container.Current.Resolve(Of IFixedAssetReclassificationAdminService)()
            Return service.SaveFixedAssetReclassification(FixedAssetReclassification, audit, idSequense)
        End Using
    End Function

    Public Function ConfirmFixedAssetReclassification(FixedAssetReclassification As Domain.Entities.FixedAssetReclassification, idSequense As Int64, audit As AuditMessage) As Domain.Base.Entities.ActionResult(Of Domain.Entities.FixedAssetReclassification) Implements IFixedAssetReclassificationService.ConfirmFixedAssetReclassification
        Using service As IFixedAssetReclassificationAdminService = Container.Current.Resolve(Of IFixedAssetReclassificationAdminService)()
            Return service.ConfirmFixedAssetReclassification(FixedAssetReclassification, audit, idSequense)
        End Using
    End Function

End Class
