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
    Implements IAuthorizationServiceAuthorizationSchedule

    Public Function SaveAuthorizationSchedule(AuthorizationSchedule As AuthorizationSchedule, ListDays As List(Of Integer), audit As AuditMessage) As ActionResult(Of AuthorizationSchedule) Implements IAuthorizationServiceAuthorizationSchedule.SaveAuthorizationSchedule
        Using service As IAuthorizationScheduleAdminService = Container.Current.Resolve(Of IAuthorizationScheduleAdminService)()
            Return service.SaveAuthorizationSchedule(AuthorizationSchedule, ListDays, audit)
        End Using
    End Function

    Public Function DeleteAuthorizationSchedule(AuthorizationSchedule As AuthorizationSchedule, TransactionalContainer As String, audit As AuditMessage) As ActionResult Implements IAuthorizationServiceAuthorizationSchedule.DeleteAuthorizationSchedule
        Using service As IAuthorizationScheduleAdminService = Container.Current.Resolve(Of IAuthorizationScheduleAdminService)()
            Return service.DeleteAuthorizationSchedule(AuthorizationSchedule, TransactionalContainer, audit)
        End Using
    End Function

    Public Function GetAuthorizationScheduleById(id As Integer) As ActionResult(Of AuthorizationSchedule) Implements IAuthorizationServiceAuthorizationSchedule.GetAuthorizationScheduleById
        Using service As IAuthorizationScheduleAdminService = Container.Current.Resolve(Of IAuthorizationScheduleAdminService)()
            Return service.GetAuthorizationScheduleById(id)
        End Using
    End Function

End Class
