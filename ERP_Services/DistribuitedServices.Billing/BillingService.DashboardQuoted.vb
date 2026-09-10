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
Imports Application.Billing

Partial Class BillingService

    Public Function SP_SaveDashboardQuoted(listDashboardQuoted As List(Of DashboardQuoted), audit As AuditMessage) As ActionResult(Of SP_SaveDashboardQuoted_Result) Implements IBillingServiceDashboardQuoted.SP_SaveDashboardQuoted
        Using service As IDashboardQuotedAdminService = Container.Current.Resolve(Of IDashboardQuotedAdminService)()
            Return service.SP_SaveDashboardQuoted(listDashboardQuoted, audit)
        End Using
    End Function

End Class
