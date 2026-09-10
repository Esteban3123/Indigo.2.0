Imports System.ServiceModel
Imports Infrastructure.CrossCutting.Base
Imports Domain.Entities
Imports Domain.Base.Entities
Imports Microsoft.Practices.Unity
Imports Application.FixedAsset

Partial Public Class FixedAssetService

    Public Function GetFixedAssetEntryDevolution(code As String, audit As AuditMessage) As Domain.Base.Entities.ActionResult(Of Domain.Entities.FixedAssetEntryDevolution) Implements IFixedAssetEntryDevolutionService.GetFixedAssetEntryDevolution
        Using service As IFixedAssetEntryDevolutionAdminService = Container.Current.Resolve(Of IFixedAssetEntryDevolutionAdminService)()
            Return service.GetFixedAssetEntryDevolution(code, audit)
        End Using
        'Return Me._fixedAssetEntryDevolutionAdminService.GetFixedAssetEntryDevolution(code, audit)
    End Function

    Public Function GetFixedAssetEntryDevolutionById(Id As Integer, audit As AuditMessage) As Domain.Base.Entities.ActionResult(Of Domain.Entities.FixedAssetEntryDevolution) Implements IFixedAssetEntryDevolutionService.GetFixedAssetEntryDevolutionById
        Using service As IFixedAssetEntryDevolutionAdminService = Container.Current.Resolve(Of IFixedAssetEntryDevolutionAdminService)()
            Return service.GetFixedAssetEntryDevolutionById(Id, audit)
        End Using
        'Return Me._fixedAssetEntryDevolutionAdminService.GetFixedAssetEntryDevolutionById(Id, audit)
    End Function

    Public Function SaveFixedAssetEntryDevolution(FixedAssetEntryDevolution As Domain.Entities.FixedAssetEntryDevolution, idSequense As Int64, audit As AuditMessage) As Domain.Base.Entities.ActionResult(Of Domain.Entities.FixedAssetEntryDevolution) Implements IFixedAssetEntryDevolutionService.SaveFixedAssetEntryDevolution
        Using service As IFixedAssetEntryDevolutionAdminService = Container.Current.Resolve(Of IFixedAssetEntryDevolutionAdminService)()
            Return service.SaveFixedAssetEntryDevolution(FixedAssetEntryDevolution, audit, idSequense)
        End Using
        'Return Me._fixedAssetEntryDevolutionAdminService.SaveFixedAssetEntryDevolution(FixedAssetEntryDevolution, audit, idSequense)
    End Function

    Public Function ConfirmFixedAssetEntryDevolution(FixedAssetEntryDevolution As Domain.Entities.FixedAssetEntryDevolution, idSequense As Int64, audit As AuditMessage) As Domain.Base.Entities.ActionResult(Of Domain.Entities.FixedAssetEntryDevolution) Implements IFixedAssetEntryDevolutionService.ConfirmFixedAssetEntryDevolution
        Using service As IFixedAssetEntryDevolutionAdminService = Container.Current.Resolve(Of IFixedAssetEntryDevolutionAdminService)()
            Return service.ConfirmFixedAssetEntryDevolution(FixedAssetEntryDevolution, audit, idSequense)
        End Using
        'Return Me._fixedAssetEntryDevolutionAdminService.ConfirmFixedAssetEntryDevolution(FixedAssetEntryDevolution, audit, idSequense)
    End Function

End Class
