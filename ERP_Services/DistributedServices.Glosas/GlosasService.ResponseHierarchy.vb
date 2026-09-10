'***********************************************************************
' Assembly         : DistributedService.Glosas
' Author           : Rafael Eduardo Patiño Cabrera
' Created          : 10-06-2015
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************
#Region "Imports"

Imports Domain.Entities

Imports Infrastructure.CrossCutting.IOC
Imports Application.Glosas
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities

#End Region
Partial Class GlosasService

    Public Function DeleteGlosasResponseHierarchy(GlosasResponseHierarchy As GlosasResponseHierarchy, session As SessionValues) As Domain.Base.Entities.ActionResult Implements IGlosasResponseHierarchy.DeleteGlosasResponseHierarchy
        Using ResponsibleAdmin As IResponseHierarchyAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of IResponseHierarchyAdminService)()
            Return ResponsibleAdmin.DeleteGlosasResponseHierarchy(GlosasResponseHierarchy, session.AuditMessageWcf)
        End Using
    End Function

    Public Function GetResponseHierarchy(Code As String, session As SessionValues) As GlosasResponseHierarchy Implements IGlosasResponseHierarchy.GetResponseHierarchy
        Using ResponsibleAdmin As IResponseHierarchyAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of IResponseHierarchyAdminService)()
            Return ResponsibleAdmin.GetResponseHierarchy(Code, session.AuditMessageWcf)
        End Using
    End Function

    Public Function GetResponseHierarchyById(id As Integer, session As SessionValues) As GlosasResponseHierarchy Implements IGlosasResponseHierarchy.GetResponseHierarchyById
        Using ResponsibleAdmin As IResponseHierarchyAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of IResponseHierarchyAdminService)()
            Return ResponsibleAdmin.GetResponseHierarchyById(id, session.AuditMessageWcf)
        End Using
    End Function

    Public Function ListResponseHierarchy(session As SessionValues) As List(Of GlosasResponseHierarchy) Implements IGlosasResponseHierarchy.ListResponseHierarchy
        Using ResponsibleAdmin As IResponseHierarchyAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of IResponseHierarchyAdminService)()
            Return ResponsibleAdmin.ListResponseHierarchy(session.AuditMessageWcf)
        End Using
    End Function

    Public Function SaveGlosasResponseHierarchy(GlosasResponseHierarchy As GlosasResponseHierarchy, session As SessionValues) As Domain.Base.Entities.ActionResult(Of GlosasResponseHierarchy) Implements IGlosasResponseHierarchy.SaveGlosasResponseHierarchy
        Using ResponsibleAdmin As IResponseHierarchyAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of IResponseHierarchyAdminService)()
            Return ResponsibleAdmin.SaveGlosasResponseHierarchy(GlosasResponseHierarchy, session.AuditMessageWcf)
        End Using
    End Function

End Class
