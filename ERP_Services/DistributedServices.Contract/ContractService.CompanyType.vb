'***********************************************************************
' Assembly         : DistributedServices.Payments
' Author           : Carlos Mario Arias Rubiano
' Created          : 07/04/2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Application.Contract
Imports Infrastructure.CrossCutting.Base
Imports Microsoft.Practices.Unity

Partial Class ContractService

    Public Function ChangeStateCompanyType(code As String, state As Boolean, audit As AuditMessage) As Domain.Base.Entities.ActionResult(Of Domain.Entities.CompanyType) Implements IContractCompanyType.ChangeStateCompanyType
        Using service As ICompanyTypeAdminService = Container.Current.Resolve(Of ICompanyTypeAdminService)()
            Return service.ChangeStateCompanyType(code, state, audit)
        End Using
    End Function

    Public Function DeleteCompanyType(CompanyType As Domain.Entities.CompanyType, audit As AuditMessage) As Domain.Base.Entities.ActionResult Implements IContractCompanyType.DeleteCompanyType
        Using service As ICompanyTypeAdminService = Container.Current.Resolve(Of ICompanyTypeAdminService)()
            Return service.DeleteCompanyType(CompanyType, audit)
        End Using
    End Function

    Public Function GetAllCompanyType(audit As AuditMessage) As List(Of Domain.Entities.CompanyType) Implements IContractCompanyType.GetAllCompanyType
        Using service As ICompanyTypeAdminService = Container.Current.Resolve(Of ICompanyTypeAdminService)()
            Return service.GetAllCompanyType(audit)
        End Using
    End Function

    Public Function GetCompanyTypeByCode(code As String, audit As AuditMessage) As Domain.Base.Entities.ActionResult(Of Domain.Entities.CompanyType) Implements IContractCompanyType.GetCompanyTypeByCode
        Using service As ICompanyTypeAdminService = Container.Current.Resolve(Of ICompanyTypeAdminService)()
            Return service.GetCompanyTypeByCode(code, audit)
        End Using
    End Function

    Public Function GetCompanyTypeById(id As Integer, audit As AuditMessage) As Domain.Base.Entities.ActionResult(Of Domain.Entities.CompanyType) Implements IContractCompanyType.GetCompanyTypeById
        Using service As ICompanyTypeAdminService = Container.Current.Resolve(Of ICompanyTypeAdminService)()
            Return service.GetCompanyTypeById(id, audit)
        End Using
    End Function

    Public Function SaveCompanyType(CompanyType As Domain.Entities.CompanyType, idSequense As Int64, audit As AuditMessage) As Domain.Base.Entities.ActionResult(Of Domain.Entities.CompanyType) Implements IContractCompanyType.SaveCompanyType
        Using service As ICompanyTypeAdminService = Container.Current.Resolve(Of ICompanyTypeAdminService)()
            Return service.SaveCompanyType(CompanyType, audit, idSequense)
        End Using
    End Function

End Class