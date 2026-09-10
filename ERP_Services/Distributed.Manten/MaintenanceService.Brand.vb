Imports Infrastructure.CrossCutting.IOC
Imports Application.Maintenance
Imports Microsoft.Practices.Unity

Partial Class MaintanceService

    Public Function DeleteBrand(Empresa As String, Brand As Domain.Entities.Brand, audit As Infrastructure.CrossCutting.Base.AuditMessage) As Boolean Implements IBrandService.DeleteBrand
        Using BrandAdmin As IBrandAdminService = Container.Current.Resolve(Of IBrandAdminService)()
            Return BrandAdmin.DeleteBrand(Brand, audit)
        End Using
    End Function

    Public Function GetBrand(Empresa As String, codeBrand As String) As Domain.Entities.Brand Implements IBrandService.GetBrand
        Using BrandAdmin As IBrandAdminService = Container.Current.Resolve(Of IBrandAdminService)()
            Return BrandAdmin.GetBrand(codeBrand)
        End Using
    End Function

    Public Function ListAllBrand(Empresa As String) As List(Of Domain.Entities.Brand) Implements IBrandService.ListAllBrand
        Using BrandAdmin As IBrandAdminService = Container.Current.Resolve(Of IBrandAdminService)()
            Return BrandAdmin.ListAllBrand
        End Using
    End Function

    Public Function ListBrand(Empresa As String) As List(Of Domain.Entities.Brand) Implements IBrandService.ListBrand
        Using BrandAdmin As IBrandAdminService = Container.Current.Resolve(Of IBrandAdminService)()
            Return BrandAdmin.ListAllBrand
        End Using
    End Function

    Public Function SaveBrand(Empresa As String, Brand As Domain.Entities.Brand, audit As Infrastructure.CrossCutting.Base.AuditMessage) As Boolean Implements IBrandService.SaveBrand
        Using BrandAdmin As IBrandAdminService = Container.Current.Resolve(Of IBrandAdminService)()
            Return BrandAdmin.SaveBrand(Brand, audit)
        End Using
    End Function

End Class
