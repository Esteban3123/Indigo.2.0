Imports System.ServiceModel
Imports Infrastructure.CrossCutting.Base
Imports Domain.Entities
Imports Domain.Base.Entities
Imports Application.FixedAsset
Imports Microsoft.Practices.Unity

Partial Public Class FixedAssetService

    Public Function ConfirmFixedAssetTransfer(FixedAssetTransfer As Domain.Entities.FixedAssetTransfer, idSequense As Int64, audit As AuditMessage) As Domain.Base.Entities.ActionResult(Of Domain.Entities.FixedAssetTransfer) Implements IFixedAssetTransferService.ConfirmFixedAssetTransfer
        Using service As IFixedAssetTransferAdminService = Container.Current.Resolve(Of IFixedAssetTransferAdminService)()
            Return service.ConfirmFixedAssetTransfer(FixedAssetTransfer, audit, idSequense)
        End Using
        'Return Me._fixedAssetTransferAdminService.ConfirmFixedAssetTransfer(FixedAssetTransfer, audit, idSequense)
    End Function

    Public Function GetFixedAssetTransfer(code As String, audit As AuditMessage) As Domain.Base.Entities.ActionResult(Of Domain.Entities.FixedAssetTransfer) Implements IFixedAssetTransferService.GetFixedAssetTransfer
        Using service As IFixedAssetTransferAdminService = Container.Current.Resolve(Of IFixedAssetTransferAdminService)()
            Return service.GetFixedAssetTransfer(code, audit)
        End Using
        'Return Me._fixedAssetTransferAdminService.GetFixedAssetTransfer(code, audit)
    End Function

    Public Function GetFixedAssetTransferById(Id As Integer, audit As AuditMessage) As Domain.Base.Entities.ActionResult(Of Domain.Entities.FixedAssetTransfer) Implements IFixedAssetTransferService.GetFixedAssetTransferById
        Using service As IFixedAssetTransferAdminService = Container.Current.Resolve(Of IFixedAssetTransferAdminService)()
            Return service.GetFixedAssetTransferById(Id, audit)
        End Using
        'Return Me._fixedAssetTransferAdminService.GetFixedAssetTransferById(Id, audit)
    End Function

    Public Function SaveFixedAssetTransfer(FixedAssetTransfer As Domain.Entities.FixedAssetTransfer, idSequense As Int64, audit As AuditMessage) As Domain.Base.Entities.ActionResult(Of Domain.Entities.FixedAssetTransfer) Implements IFixedAssetTransferService.SaveFixedAssetTransfer
        Using service As IFixedAssetTransferAdminService = Container.Current.Resolve(Of IFixedAssetTransferAdminService)()
            Return service.SaveFixedAssetTransfer(FixedAssetTransfer, audit, idSequense)
        End Using
        'Return Me._fixedAssetTransferAdminService.SaveFixedAssetTransfer(FixedAssetTransfer, audit, idSequense)
    End Function

End Class
