'***********************************************************************
' Assembly         : DistributedServices.Billing
' Author           : Cristian Camilo Bahamon
' Created          : 05-02-2023
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"

Imports Application.Billing
Imports Infrastructure.CrossCutting.Base
Imports System.ServiceModel
Imports Domain.Base.Entities
Imports Microsoft.Practices.Unity
Imports Domain.Entities
Imports Infrastructure.CrossCutting.IOC
Imports Domain.Payroll.Entities
Imports Application.Payroll


#End Region

Partial Class PayrollService

    Function ListAllContributorSubtype(ByVal session As SessionValues) As List(Of ContributorSubtype) Implements IPayrollContributorSubtype.ListAllContributorSubtype
        Using service As IContributorSubtypeAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of IContributorSubtypeAdminService)()
            Return service.ListAllContributorSubtype()
        End Using
    End Function

    Public Function DeleteContributorSubtype(ByVal contributorSubtype As ContributorSubtype, ByVal audit As AuditMessage, session As SessionValues) As ActionResult Implements IPayrollContributorSubtype.DeleteContributorSubtype
        Using service As IContributorSubtypeAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of IContributorSubtypeAdminService)()
            Return service.DeleteContributorSubtype(contributorSubtype, audit)
        End Using
    End Function

    Public Function SaveContributorSubtype(ByVal contributorSubtype As ContributorSubtype, ByVal audit As AuditMessage, session As SessionValues, Optional idSequense As Long = 0) As ActionResult(Of ContributorSubtype) Implements IPayrollContributorSubtype.SaveContributorSubtype
        Using service As IContributorSubtypeAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of IContributorSubtypeAdminService)()
            Return service.SaveContributorSubtype(contributorSubtype, audit, idSequense)
        End Using
    End Function

    Public Function GetContributorSubtypeByCode(ByVal code As String, ByVal audit As AuditMessage, session As SessionValues) As ActionResult(Of ContributorSubtype) Implements IPayrollContributorSubtype.GetContributorSubtypeByCode
        Using service As IContributorSubtypeAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of IContributorSubtypeAdminService)()
            Return service.GetContributorSubtypeByCode(code, audit)
        End Using
    End Function

    Public Function GetContributorSubtypeById(ByVal id As Integer, session As SessionValues) As ActionResult(Of ContributorSubtype) Implements IPayrollContributorSubtype.GetContributorSubtypeById
        Using service As IContributorSubtypeAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of IContributorSubtypeAdminService)()
            Return service.GetContributorSubtypeById(id)
        End Using
    End Function

End Class
