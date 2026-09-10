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
    Implements IAuthorizationServiceCancellationReasons
    Public Function ChangeStateCancellationReasons(code As String, state As Boolean, audit As AuditMessage) As Domain.Base.Entities.ActionResult(Of Domain.Entities.CancellationReasons) Implements IAuthorizationServiceCancellationReasons.ChangeStateCancellationReasons
        Using service As ICancellationReasonsAdminService = Container.Current.Resolve(Of ICancellationReasonsAdminService)()
            Return service.ChangeStateCancellationReasons(code, state, audit)
        End Using
    End Function

    Public Function DeleteCancellationReasons(CancellationReasons As Domain.Entities.CancellationReasons, audit As AuditMessage) As Domain.Base.Entities.ActionResult Implements IAuthorizationServiceCancellationReasons.DeleteCancellationReasons
        Using service As ICancellationReasonsAdminService = Container.Current.Resolve(Of ICancellationReasonsAdminService)()
            Return service.DeleteCancellationReasons(CancellationReasons, audit)
        End Using
    End Function

    Public Function GetCancellationReasons(code As String, audit As AuditMessage) As Domain.Entities.CancellationReasons Implements IAuthorizationServiceCancellationReasons.GetCancellationReasons
        Using service As ICancellationReasonsAdminService = Container.Current.Resolve(Of ICancellationReasonsAdminService)()
            Return service.GetCancellationReasons(code, audit)
        End Using
    End Function

    Public Function GetCancellationReasonsById(id As Integer, audit As AuditMessage) As Domain.Entities.CancellationReasons Implements IAuthorizationServiceCancellationReasons.GetCancellationReasonsById
        Using service As ICancellationReasonsAdminService = Container.Current.Resolve(Of ICancellationReasonsAdminService)()
            Return service.GetCancellationReasonsById(id)
        End Using
    End Function

    Public Function SaveCancellationReasons(CancellationReasons As Domain.Entities.CancellationReasons, idSequense As Int64, audit As AuditMessage) As Domain.Base.Entities.ActionResult(Of Domain.Entities.CancellationReasons) Implements IAuthorizationServiceCancellationReasons.SaveCancellationReasons
        Using service As ICancellationReasonsAdminService = Container.Current.Resolve(Of ICancellationReasonsAdminService)()
            Return service.SaveCancellationReasons(CancellationReasons, audit, idSequense)
        End Using
    End Function
End Class
