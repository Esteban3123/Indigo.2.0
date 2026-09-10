'***********************************************************************
' Assembly         : DistributedServices.Payments
' Author           : Carlos Mario Arias Rubiano
' Created          : 07/10/2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities
Imports System.ServiceModel
Imports Microsoft.Practices.Unity
Imports Application.Authorization

Partial Class AuthorizationService
    Implements IAuthorizationServiceAuthorizationScheduleTemplate
    Public Function ChangeStatAuthorizationScheduleTemplate(code As String, state As Boolean, audit As AuditMessage) As Domain.Base.Entities.ActionResult(Of Domain.Entities.AuthorizationScheduleTemplate) Implements IAuthorizationServiceAuthorizationScheduleTemplate.ChangeStateAuthorizationScheduleTemplate
        Using service As IAuthorizationScheduleTemplateAdminService = Container.Current.Resolve(Of IAuthorizationScheduleTemplateAdminService)()
            Return service.ChangeStateAuthorizationScheduleTemplate(code, state, audit)
        End Using
    End Function

    Public Function DeleteAuthorizationScheduleTemplate(AuthorizationScheduleTemplate As Domain.Entities.AuthorizationScheduleTemplate, audit As AuditMessage) As Domain.Base.Entities.ActionResult Implements IAuthorizationServiceAuthorizationScheduleTemplate.DeleteAuthorizationScheduleTemplate
        Using service As IAuthorizationScheduleTemplateAdminService = Container.Current.Resolve(Of IAuthorizationScheduleTemplateAdminService)()
            Return service.DeleteAuthorizationScheduleTemplate(AuthorizationScheduleTemplate, audit)
        End Using
    End Function

    Public Function GetAuthorizationScheduleTemplate(code As String, audit As AuditMessage) As Domain.Entities.AuthorizationScheduleTemplate Implements IAuthorizationServiceAuthorizationScheduleTemplate.GetAuthorizationScheduleTemplate
        Using service As IAuthorizationScheduleTemplateAdminService = Container.Current.Resolve(Of IAuthorizationScheduleTemplateAdminService)()
            Return service.GetAuthorizationScheduleTemplate(code, audit)
        End Using
    End Function

    Public Function GetAuthorizationScheduleTemplateById(id As Integer, audit As AuditMessage) As Domain.Entities.AuthorizationScheduleTemplate Implements IAuthorizationServiceAuthorizationScheduleTemplate.GetAuthorizationScheduleTemplateById
        Using service As IAuthorizationScheduleTemplateAdminService = Container.Current.Resolve(Of IAuthorizationScheduleTemplateAdminService)()
            Return service.GetAuthorizationScheduleTemplateById(id)
        End Using
    End Function

    Public Function SaveAuthorizationScheduleTemplate(AuthorizationScheduleTemplate As Domain.Entities.AuthorizationScheduleTemplate, idSequense As Int64, audit As AuditMessage) As Domain.Base.Entities.ActionResult(Of Domain.Entities.AuthorizationScheduleTemplate) Implements IAuthorizationServiceAuthorizationScheduleTemplate.SaveAuthorizationScheduleTemplate
        Using service As IAuthorizationScheduleTemplateAdminService = Container.Current.Resolve(Of IAuthorizationScheduleTemplateAdminService)()
            Return service.SaveAuthorizationScheduleTemplate(AuthorizationScheduleTemplate, audit, idSequense)
        End Using
    End Function
End Class
