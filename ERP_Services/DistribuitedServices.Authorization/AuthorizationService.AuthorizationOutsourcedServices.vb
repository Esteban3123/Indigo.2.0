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
    Implements IAuthorizationServiceAuthorizationOutsourcedServices

    Public Function SaveAuthorizationOutsourcedServices(AuthorizationOutsourcedServices As AuthorizationOutsourcedServices, audit As AuditMessage, Optional idSequense As Long = 0) As ActionResult(Of AuthorizationOutsourcedServices) Implements IAuthorizationServiceAuthorizationOutsourcedServices.SaveAuthorizationOutsourcedServices
        Using service As IAuthorizationOutsourcedServicesAdminService = Container.Current.Resolve(Of IAuthorizationOutsourcedServicesAdminService)()
            Return service.SaveAuthorizationOutsourcedServices(AuthorizationOutsourcedServices, audit, idSequense)
        End Using
    End Function

    Public Function GetAuthorizationOutsourcedServices(code As String) As ActionResult(Of AuthorizationOutsourcedServices) Implements IAuthorizationServiceAuthorizationOutsourcedServices.GetAuthorizationOutsourcedServices
        Using service As IAuthorizationOutsourcedServicesAdminService = Container.Current.Resolve(Of IAuthorizationOutsourcedServicesAdminService)()
            Return service.GetAuthorizationOutsourcedServices(code)
        End Using
    End Function

    Public Function GetAuthorizationOutsourcedServicesById(id As Integer) As ActionResult(Of AuthorizationOutsourcedServices) Implements IAuthorizationServiceAuthorizationOutsourcedServices.GetAuthorizationOutsourcedServicesById
        Using service As IAuthorizationOutsourcedServicesAdminService = Container.Current.Resolve(Of IAuthorizationOutsourcedServicesAdminService)()
            Return service.GetAuthorizationOutsourcedServicesById(id)
        End Using
    End Function
End Class
