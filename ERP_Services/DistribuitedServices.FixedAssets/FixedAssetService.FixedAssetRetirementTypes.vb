Imports System.ServiceModel
Imports Infrastructure.CrossCutting.Base
Imports Domain.Entities
Imports Domain.Base.Entities
Imports Application.FixedAsset
Imports Microsoft.Practices.Unity

Partial Public Class FixedAssetService

    Public Function DeleteFixedAssetRetirementTypes(FixedAssetRetirementTypes As FixedAssetRetirementTypes, audit As AuditMessage) As ActionResult Implements IFixedAssetRetirementTypesService.DeleteFixedAssetRetirementTypes
        Using service As IFixedAssetRetirementTypesAdminService = Container.Current.Resolve(Of IFixedAssetRetirementTypesAdminService)()
            Return service.DeleteFixedAssetRetirementTypes(FixedAssetRetirementTypes, audit)
        End Using
    End Function

    Public Function GetAllFixedAssetRetirementTypes(audit As AuditMessage) As List(Of FixedAssetRetirementTypes) Implements IFixedAssetRetirementTypesService.GetAllFixedAssetRetirementTypes
        Using service As IFixedAssetRetirementTypesAdminService = Container.Current.Resolve(Of IFixedAssetRetirementTypesAdminService)()
            Return service.GetAllFixedAssetRetirementTypes(audit)
        End Using
    End Function

    Public Function GetFixedAssetRetirementTypesByCode(code As String, audit As AuditMessage) As ActionResult(Of FixedAssetRetirementTypes) Implements IFixedAssetRetirementTypesService.GetFixedAssetRetirementTypesByCode
        Using service As IFixedAssetRetirementTypesAdminService = Container.Current.Resolve(Of IFixedAssetRetirementTypesAdminService)()
            Return service.GetFixedAssetRetirementTypesByCode(code, audit)
        End Using
    End Function

    Public Function GetFixedAssetRetirementTypesById(id As Integer, audit As AuditMessage) As ActionResult(Of FixedAssetRetirementTypes) Implements IFixedAssetRetirementTypesService.GetFixedAssetRetirementTypesById
        Using service As IFixedAssetRetirementTypesAdminService = Container.Current.Resolve(Of IFixedAssetRetirementTypesAdminService)()
            Return service.GetFixedAssetRetirementTypesById(id, audit)
        End Using
    End Function

    Public Function SaveFixedAssetRetirementTypes(FixedAssetRetirementTypes As FixedAssetRetirementTypes, idSequense As Int64, audit As AuditMessage) As ActionResult(Of FixedAssetRetirementTypes) Implements IFixedAssetRetirementTypesService.SaveFixedAssetRetirementTypes
        Using service As IFixedAssetRetirementTypesAdminService = Container.Current.Resolve(Of IFixedAssetRetirementTypesAdminService)()
            Return service.SaveFixedAssetRetirementTypes(FixedAssetRetirementTypes, audit, idSequense)
        End Using
    End Function

End Class
