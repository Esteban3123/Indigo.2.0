
Imports Application.FixedAsset
Imports Infrastructure.CrossCutting.Base
Imports System.ServiceModel
Imports Domain.Base.Entities
Imports Microsoft.Practices.Unity

Partial Class FixedAssetService


    Public Function DeletePoliza(Empresa As String, Poliza As Domain.Entities.FixedAssetPolicy, audit As Infrastructure.CrossCutting.Base.AuditMessage) As ActionResult Implements IFixedAssetPolicyService.DeletePoliza
        Using service As IFixedAssetPolicyAdminService = Container.Current.Resolve(Of IFixedAssetPolicyAdminService)()
            Return service.DeletePoliza(Poliza, audit)
        End Using
        'Return _FixedAssetPolicyAdminService.DeletePoliza(Poliza, audit)
    End Function

    Public Function GetPoliza(Empresa As String, codePoliza As String, audit As Infrastructure.CrossCutting.Base.AuditMessage) As Domain.Entities.FixedAssetPolicy Implements IFixedAssetPolicyService.GetPoliza
        Using service As IFixedAssetPolicyAdminService = Container.Current.Resolve(Of IFixedAssetPolicyAdminService)()
            Return service.GetPoliza(codePoliza, audit)
        End Using
        'Return _FixedAssetPolicyAdminService.GetPoliza(codePoliza, audit)
    End Function

    Public Function ListAllPoliza(Empresa As String) As List(Of Domain.Entities.FixedAssetPolicy) Implements IFixedAssetPolicyService.ListAllPoliza
        Using service As IFixedAssetPolicyAdminService = Container.Current.Resolve(Of IFixedAssetPolicyAdminService)()
            Return service.ListAllPoliza()
        End Using
        'Return _FixedAssetPolicyAdminService.ListAllPoliza()
    End Function

    Public Function SavePoliza(Empresa As String, Poliza As Domain.Entities.FixedAssetPolicy, audit As Infrastructure.CrossCutting.Base.AuditMessage, idSequense As Int64) As ActionResult(Of Domain.Entities.FixedAssetPolicy) Implements IFixedAssetPolicyService.SavePoliza
        Using service As IFixedAssetPolicyAdminService = Container.Current.Resolve(Of IFixedAssetPolicyAdminService)()
            Return service.SavePoliza(Poliza, audit, idSequense)
        End Using
        'Return _FixedAssetPolicyAdminService.SavePoliza(Poliza, audit, idSequense)
    End Function

    Public Function ChangeStatePoliza(Empresa As String, code As String, state As Boolean, audit As Infrastructure.CrossCutting.Base.AuditMessage) As Domain.Base.Entities.ActionResult(Of Domain.Entities.FixedAssetPolicy) Implements IFixedAssetPolicyService.ChangeStatePoliza
        Using service As IFixedAssetPolicyAdminService = Container.Current.Resolve(Of IFixedAssetPolicyAdminService)()
            Return service.ChangeStatePoliza(code, state, audit)
        End Using
        'Return _FixedAssetPolicyAdminService.ChangeStatePoliza(code, state, audit)
    End Function
End Class
