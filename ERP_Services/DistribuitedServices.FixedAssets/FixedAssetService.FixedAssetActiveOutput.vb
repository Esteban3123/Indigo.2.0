Imports System.ServiceModel
Imports Infrastructure.CrossCutting.Base
Imports Domain.Entities
Imports Domain.Base.Entities
Imports Microsoft.Practices.Unity
Imports Application.FixedAsset

Partial Public Class FixedAssetService

    Public Function ConfirmFixedAssetActiveOutput(FixedAssetActiveOutput As Domain.Entities.FixedAssetActiveOutput, idSequense As Int64, audit As AuditMessage) As Domain.Base.Entities.ActionResult(Of Domain.Entities.FixedAssetActiveOutput) Implements IFixedAssetActiveOutputService.ConfirmFixedAssetActiveOutput
        Using service As IFixedAssetActiveOutputAdminService = Container.Current.Resolve(Of IFixedAssetActiveOutputAdminService)()
            Return service.ConfirmFixedAssetActiveOutput(FixedAssetActiveOutput, audit, idSequense)
        End Using
        'Return Me._fixedAssetActiveOutputAdminService.ConfirmFixedAssetActiveOutput(FixedAssetActiveOutput, audit, idSequense)
    End Function

    Public Function GetFixedAssetActiveOutput(code As String, audit As AuditMessage) As Domain.Base.Entities.ActionResult(Of Domain.Entities.FixedAssetActiveOutput) Implements IFixedAssetActiveOutputService.GetFixedAssetActiveOutput
        Using service As IFixedAssetActiveOutputAdminService = Container.Current.Resolve(Of IFixedAssetActiveOutputAdminService)()
            Return service.GetFixedAssetActiveOutput(code, audit)
        End Using
        'Return Me._fixedAssetActiveOutputAdminService.GetFixedAssetActiveOutput(code, audit)
    End Function

    Public Function GetFixedAssetActiveOutputById(Id As Integer, audit As AuditMessage) As Domain.Base.Entities.ActionResult(Of Domain.Entities.FixedAssetActiveOutput) Implements IFixedAssetActiveOutputService.GetFixedAssetActiveOutputById
        Using service As IFixedAssetActiveOutputAdminService = Container.Current.Resolve(Of IFixedAssetActiveOutputAdminService)()
            Return service.GetFixedAssetActiveOutputById(Id, audit)
        End Using
        'Return Me._fixedAssetActiveOutputAdminService.GetFixedAssetActiveOutputById(Id, audit)
    End Function

    Public Function SaveFixedAssetActiveOutput(FixedAssetActiveOutput As Domain.Entities.FixedAssetActiveOutput, idSequense As Int64, audit As AuditMessage) As Domain.Base.Entities.ActionResult(Of Domain.Entities.FixedAssetActiveOutput) Implements IFixedAssetActiveOutputService.SaveFixedAssetActiveOutput
        Using service As IFixedAssetActiveOutputAdminService = Container.Current.Resolve(Of IFixedAssetActiveOutputAdminService)()
            Return service.SaveFixedAssetActiveOutput(FixedAssetActiveOutput, audit, idSequense)
        End Using
        'Return Me._fixedAssetActiveOutputAdminService.SaveFixedAssetActiveOutput(FixedAssetActiveOutput, audit, idSequense)
    End Function

End Class
