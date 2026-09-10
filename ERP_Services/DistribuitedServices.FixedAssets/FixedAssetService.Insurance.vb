
Imports Application.FixedAsset
Imports Domain.Entities
Imports Domain.Base.Entities
Imports Infrastructure.CrossCutting.Base
Imports System.ServiceModel
Imports Microsoft.Practices.Unity

Partial Class FixedAssetService

    Public Function DeleteInsurance(Insurance As FixedAssetInsurance, audit As AuditMessage) As ActionResult Implements IFixedAssetInsuranceService.DeleteInsurance
        Using service As IFixedAssetInsuranceAdminService = Container.Current.Resolve(Of IFixedAssetInsuranceAdminService)()
            Return service.DeleteInsurance(Insurance, audit)
        End Using
        'Return _fixedAssetInsuranceAdminService.DeleteInsurance(Insurance, audit)
    End Function

    Public Function GetInsurance(Empresa As String, codeInsurance As String) As FixedAssetInsurance Implements IFixedAssetInsuranceService.GetInsurance
        Using service As IFixedAssetInsuranceAdminService = Container.Current.Resolve(Of IFixedAssetInsuranceAdminService)()
            Return service.GetInsurance(codeInsurance)
        End Using
        'Return _fixedAssetInsuranceAdminService.GetInsurance(codeInsurance)
    End Function

    Public Function ListAllInsurance(Empresa As String) As List(Of FixedAssetInsurance) Implements IFixedAssetInsuranceService.ListAllInsurance
        Using service As IFixedAssetInsuranceAdminService = Container.Current.Resolve(Of IFixedAssetInsuranceAdminService)()
            Return service.ListAllInsurance()
        End Using
        'Return _fixedAssetInsuranceAdminService.ListAllInsurance()
    End Function
    Public Function SaveInsurance(Insurance As FixedAssetInsurance, idSequense As Int64, audit As AuditMessage) As ActionResult(Of FixedAssetInsurance) Implements IFixedAssetInsuranceService.SaveInsurance
        Using service As IFixedAssetInsuranceAdminService = Container.Current.Resolve(Of IFixedAssetInsuranceAdminService)()
            Return service.SaveInsurance(Insurance, audit, idSequense)
        End Using
        'Return _fixedAssetInsuranceAdminService.SaveInsurance(Insurance, audit, idSequense)
    End Function

    Public Function Change_StateInsurance(code As String, state As Boolean, audit As AuditMessage) As ActionResult(Of FixedAssetInsurance) Implements IFixedAssetInsuranceService.Change_StateInsurance
        Using service As IFixedAssetInsuranceAdminService = Container.Current.Resolve(Of IFixedAssetInsuranceAdminService)()
            Return service.ChangeState(code, state, audit)
        End Using
        'Return _fixedAssetInsuranceAdminService.ChangeState(code, state, audit)
    End Function
End Class
