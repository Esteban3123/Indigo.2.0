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
    Implements IAuthorizationServiceAuthorizationRejection

    Public Function SaveAuthorizationRejection(AuthorizationRejection As AuthorizationRejection, idSequense As Long, audit As AuditMessage) As ActionResult(Of AuthorizationRejection) Implements IAuthorizationServiceAuthorizationRejection.SaveAuthorizationRejection
        Using service As IAuthorizationRejectionAdminService = Container.Current.Resolve(Of IAuthorizationRejectionAdminService)()
            Return service.SaveAuthorizationRejection(AuthorizationRejection, audit, idSequense)
        End Using
    End Function

    Public Function DeleteAuthorizationRejection(AuthorizationRejection As AuthorizationRejection, audit As AuditMessage) As ActionResult Implements IAuthorizationServiceAuthorizationRejection.DeleteAuthorizationRejection
        Using service As IAuthorizationRejectionAdminService = Container.Current.Resolve(Of IAuthorizationRejectionAdminService)()
            Return service.DeleteAuthorizationRejection(AuthorizationRejection, audit)
        End Using
    End Function

    Public Function GetAuthorizationRejection(code As String, audit As AuditMessage) As AuthorizationRejection Implements IAuthorizationServiceAuthorizationRejection.GetAuthorizationRejection
        Using service As IAuthorizationRejectionAdminService = Container.Current.Resolve(Of IAuthorizationRejectionAdminService)()
            Return service.GetAuthorizationRejection(code, audit)
        End Using
    End Function

    Public Function GetAuthorizationRejectionById(id As Integer, audit As AuditMessage) As AuthorizationRejection Implements IAuthorizationServiceAuthorizationRejection.GetAuthorizationRejectionById
        Using service As IAuthorizationRejectionAdminService = Container.Current.Resolve(Of IAuthorizationRejectionAdminService)()
            Return service.GetAuthorizationRejectionById(id)
        End Using
    End Function

    Public Function ChangeStateAuthorizationRejection(code As String, state As Boolean, audit As AuditMessage) As ActionResult(Of AuthorizationRejection) Implements IAuthorizationServiceAuthorizationRejection.ChangeStateAuthorizationRejection
        Using service As IAuthorizationRejectionAdminService = Container.Current.Resolve(Of IAuthorizationRejectionAdminService)()
            Return service.ChangeStateAuthorizationRejection(code, state, audit)
        End Using
    End Function
End Class
