
Imports Application.FixedAsset
Imports Domain.Base.Entities
Imports Infrastructure.CrossCutting.Base
Imports System.ServiceModel
Imports Microsoft.Practices.Unity

Partial Class FixedAssetService

    Public Function DeletePolizaType(Empresa As String, PolizaType As Domain.Entities.FixedAssetPolicyType, audit As Infrastructure.CrossCutting.Base.AuditMessage) As actionResult Implements IFixedAssetPolicyTypeService.DeletePolizaType
        Using service As IFixedAssetPolicyTypeAdminService = Container.Current.Resolve(Of IFixedAssetPolicyTypeAdminService)()
            Return service.DeletePolizaType(PolizaType, audit)
        End Using
        'Return _FixedAssetPolicyTypeAdminService.DeletePolizaType(PolizaType, audit)
    End Function

    Public Function GetPolizaType(Empresa As String, codePolizaType As String, audit As Infrastructure.CrossCutting.Base.AuditMessage) As Domain.Entities.FixedAssetPolicyType Implements IFixedAssetPolicyTypeService.GetPolizaType
        Using service As IFixedAssetPolicyTypeAdminService = Container.Current.Resolve(Of IFixedAssetPolicyTypeAdminService)()
            Return service.GetPolizaType(codePolizaType, audit)
        End Using
        'Return _FixedAssetPolicyTypeAdminService.GetPolizaType(codePolizaType, audit)
    End Function

    Public Function ListAllPolizaType(Empresa As String) As List(Of Domain.Entities.FixedAssetPolicyType) Implements IFixedAssetPolicyTypeService.ListAllPolizaType
        Using service As IFixedAssetPolicyTypeAdminService = Container.Current.Resolve(Of IFixedAssetPolicyTypeAdminService)()
            Return service.ListAllPolizaType()
        End Using
        'Return _FixedAssetPolicyTypeAdminService.ListAllPolizaType
    End Function

    Public Function SavePolizaType(Empresa As String, PolizaType As Domain.Entities.FixedAssetPolicyType, audit As Infrastructure.CrossCutting.Base.AuditMessage, idSequense As Int64) As ActionResult(Of Domain.Entities.FixedAssetPolicyType) Implements IFixedAssetPolicyTypeService.SavePolizaType
        Using service As IFixedAssetPolicyTypeAdminService = Container.Current.Resolve(Of IFixedAssetPolicyTypeAdminService)()
            Return service.SavePolizaType(PolizaType, audit, idSequense)
        End Using
        'Return _FixedAssetPolicyTypeAdminService.SavePolizaType(PolizaType, audit, idSequense)
    End Function

    Public Function ChangeStatePolizaType(Empresa As String, code As String, state As Boolean, audit As Infrastructure.CrossCutting.Base.AuditMessage) As Domain.Base.Entities.ActionResult(Of Domain.Entities.FixedAssetPolicyType) Implements IFixedAssetPolicyTypeService.ChangeStatePolizaType
        Using service As IFixedAssetPolicyTypeAdminService = Container.Current.Resolve(Of IFixedAssetPolicyTypeAdminService)()
            Return service.ChangeStatePoliza(code, state, audit)
        End Using
        'Return _FixedAssetPolicyTypeAdminService.ChangeStatePoliza(code, state, audit)
    End Function

End Class
