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
    Implements IAuthorizationServiceTraceabilityPaperwork

    Public Function GetTraceabilityPaperworkById(id As Integer) As ActionResult(Of TraceabilityPaperwork) Implements IAuthorizationServiceTraceabilityPaperwork.GetTraceabilityPaperworkById
        Using service As ITraceabilityPaperworkAdminService = Container.Current.Resolve(Of ITraceabilityPaperworkAdminService)()
            Return service.GetTraceabilityPaperworkById(id)
        End Using
    End Function

    Public Function AssignTraceabilityPaperwork(ListTraceabilityPaperwork As List(Of TraceabilityPaperwork), audit As AuditMessage) As ActionResult(Of List(Of TraceabilityPaperwork)) Implements IAuthorizationServiceTraceabilityPaperwork.AssignTraceabilityPaperwork
        Using service As ITraceabilityPaperworkAdminService = Container.Current.Resolve(Of ITraceabilityPaperworkAdminService)()
            Return service.AssignTraceabilityPaperwork(ListTraceabilityPaperwork, audit)
        End Using
    End Function

    Public Function SaveTraceabilityPaperwork(ListTraceabilityPaperwork As List(Of TraceabilityPaperwork), audit As AuditMessage) As ActionResult(Of List(Of TraceabilityPaperwork)) Implements IAuthorizationServiceTraceabilityPaperwork.SaveTraceabilityPaperwork
        Using service As ITraceabilityPaperworkAdminService = Container.Current.Resolve(Of ITraceabilityPaperworkAdminService)()
            Return service.SaveTraceabilityPaperwork(ListTraceabilityPaperwork, audit)
        End Using
    End Function

    Public Function SP_SaveAcceptanceAuthorization(listTuple As List(Of Tuple(Of Integer, Integer, String, Integer)), audit As AuditMessage) As ActionResult Implements IAuthorizationServiceTraceabilityPaperwork.SP_SaveAcceptanceAuthorization
        Using service As ITraceabilityPaperworkAdminService = Container.Current.Resolve(Of ITraceabilityPaperworkAdminService)()
            Return service.SP_SaveAcceptanceAuthorization(listTuple, audit)
        End Using
    End Function

    Public Function DeleteTraceabilityPaperwork(TraceabilityPaperwork As TraceabilityPaperwork, TransactionalContainer As String, audit As AuditMessage) As ActionResult Implements IAuthorizationServiceTraceabilityPaperwork.DeleteTraceabilityPaperwork
        Using service As ITraceabilityPaperworkAdminService = Container.Current.Resolve(Of ITraceabilityPaperworkAdminService)()
            Return service.DeleteTraceabilityPaperwork(TraceabilityPaperwork, TransactionalContainer, audit)
        End Using
    End Function

End Class
