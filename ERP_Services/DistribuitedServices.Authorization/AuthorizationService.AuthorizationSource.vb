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
Imports DistribuitedServices.Authorization

Partial Class AuthorizationService
    Implements IAuthorizationServiceAuthorizationSource

    Public Function SaveAuthorizationSource(AuthorizationSource As AuthorizationSource, idSequense As Long, audit As AuditMessage) As ActionResult(Of AuthorizationSource) Implements IAuthorizationServiceAuthorizationSource.SaveAuthorizationSource
        Using service As IAuthorizationSourceAdminService = Container.Current.Resolve(Of IAuthorizationSourceAdminService)()
            Return service.SaveAuthorizationSource(AuthorizationSource, audit, idSequense)
        End Using
    End Function

    Public Function DeleteAuthorizationSource(AuthorizationSource As AuthorizationSource, audit As AuditMessage) As ActionResult Implements IAuthorizationServiceAuthorizationSource.DeleteAuthorizationSource
        Using service As IAuthorizationSourceAdminService = Container.Current.Resolve(Of IAuthorizationSourceAdminService)()
            Return service.DeleteAuthorizationSource(AuthorizationSource, audit)
        End Using
    End Function

    Public Function GetAuthorizationSource(code As String, audit As AuditMessage) As AuthorizationSource Implements IAuthorizationServiceAuthorizationSource.GetAuthorizationSource
        Using service As IAuthorizationSourceAdminService = Container.Current.Resolve(Of IAuthorizationSourceAdminService)()
            Return service.GetAuthorizationSource(code, audit)
        End Using
    End Function

    Public Function GetAuthorizationSourceById(id As Integer, audit As AuditMessage) As AuthorizationSource Implements IAuthorizationServiceAuthorizationSource.GetAuthorizationSourceById
        Using service As IAuthorizationSourceAdminService = Container.Current.Resolve(Of IAuthorizationSourceAdminService)()
            Return service.GetAuthorizationSourceById(id)
        End Using
    End Function

    Public Function ChangeStateAuthorizationSource(code As String, state As Boolean, audit As AuditMessage) As ActionResult(Of AuthorizationSource) Implements IAuthorizationServiceAuthorizationSource.ChangeStateAuthorizationSource
        Using service As IAuthorizationSourceAdminService = Container.Current.Resolve(Of IAuthorizationSourceAdminService)()
            Return service.ChangeStateAuthorizationSource(code, state, audit)
        End Using
    End Function

End Class
