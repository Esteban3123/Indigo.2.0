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
    Implements IAuthorizationServiceAuthorizationGroup
    Public Function ChangeStateAuthorizationGroup(code As String, state As Boolean, audit As AuditMessage) As Domain.Base.Entities.ActionResult(Of Domain.Entities.AuthorizationGroup) Implements IAuthorizationServiceAuthorizationGroup.ChangeStateAuthorizationGroup
        Using service As IAuthorizationGroupAdminService = Container.Current.Resolve(Of IAuthorizationGroupAdminService)()
            Return service.ChangeStateAuthorizationGroup(code, state, audit)
        End Using
    End Function

    Public Function DeleteAuthorizationGroup(AuthorizationGroup As Domain.Entities.AuthorizationGroup, audit As AuditMessage) As Domain.Base.Entities.ActionResult Implements IAuthorizationServiceAuthorizationGroup.DeleteAuthorizationGroup
        Using service As IAuthorizationGroupAdminService = Container.Current.Resolve(Of IAuthorizationGroupAdminService)()
            Return service.DeleteAuthorizationGroup(AuthorizationGroup, audit)
        End Using
    End Function

    Public Function GetAuthorizationGroup(code As String, audit As AuditMessage) As Domain.Entities.AuthorizationGroup Implements IAuthorizationServiceAuthorizationGroup.GetAuthorizationGroup
        Using service As IAuthorizationGroupAdminService = Container.Current.Resolve(Of IAuthorizationGroupAdminService)()
            Return service.GetAuthorizationGroup(code, audit)
        End Using
    End Function

    Public Function GetAuthorizationGroupById(id As Integer, audit As AuditMessage) As Domain.Entities.AuthorizationGroup Implements IAuthorizationServiceAuthorizationGroup.GetAuthorizationGroupById
        Using service As IAuthorizationGroupAdminService = Container.Current.Resolve(Of IAuthorizationGroupAdminService)()
            Return service.GetAuthorizationGroupById(id)
        End Using
    End Function

    Public Function SaveAuthorizationGroup(AuthorizationGroup As Domain.Entities.AuthorizationGroup, idSequense As Int64, audit As AuditMessage) As Domain.Base.Entities.ActionResult(Of Domain.Entities.AuthorizationGroup) Implements IAuthorizationServiceAuthorizationGroup.SaveAuthorizationGroup
        Using service As IAuthorizationGroupAdminService = Container.Current.Resolve(Of IAuthorizationGroupAdminService)()
            Return service.SaveAuthorizationGroup(AuthorizationGroup, audit, idSequense)
        End Using
    End Function
End Class
