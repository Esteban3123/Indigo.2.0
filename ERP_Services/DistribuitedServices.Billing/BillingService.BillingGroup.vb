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
    Public Function ChangeStateBillingGroup(code As String, state As Boolean, audit As AuditMessage) As Domain.Base.Entities.ActionResult(Of Domain.Entities.BillingGroup) Implements IBillingServiceBillingGroup.ChangeStateBillingGroup
        Using service As IBillingGroupAdminService = Container.Current.Resolve(Of IBillingGroupAdminService)()
            Return service.ChangeStateBillingGroup(code, state, audit)
        End Using
        'Return _billingGroupAdminService.ChangeStateBillingGroup(code, state, audit)
    End Function

    Public Function DeleteBillingGroup(BillingGroup As Domain.Entities.BillingGroup, audit As AuditMessage) As Domain.Base.Entities.ActionResult Implements IBillingServiceBillingGroup.DeleteBillingGroup
        Using service As IBillingGroupAdminService = Container.Current.Resolve(Of IBillingGroupAdminService)()
            Return service.DeleteBillingGroup(BillingGroup, audit)
        End Using
        'Return _billingGroupAdminService.DeleteBillingGroup(BillingGroup, audit)
    End Function

    Public Function GetBillingGroup(code As String, audit As AuditMessage) As Domain.Entities.BillingGroup Implements IBillingServiceBillingGroup.GetBillingGroup
        Using service As IBillingGroupAdminService = Container.Current.Resolve(Of IBillingGroupAdminService)()
            Return service.GetBillingGroup(code, audit)
        End Using
        'Return _billingGroupAdminService.GetBillingGroup(code, audit)
    End Function

    Public Function GetBillingGroupById(id As Integer, audit As AuditMessage) As Domain.Entities.BillingGroup Implements IBillingServiceBillingGroup.GetBillingGroupById
        Using service As IBillingGroupAdminService = Container.Current.Resolve(Of IBillingGroupAdminService)()
            Return service.GetBillingGroupById(id)
        End Using
        'Return _billingGroupAdminService.GetBillingGroupById(id)
    End Function

    Public Function SaveBillingGroup(BillingGroup As Domain.Entities.BillingGroup, idSequense As Int64, audit As AuditMessage) As Domain.Base.Entities.ActionResult(Of Domain.Entities.BillingGroup) Implements IBillingServiceBillingGroup.SaveBillingGroup
        Using service As IBillingGroupAdminService = Container.Current.Resolve(Of IBillingGroupAdminService)()
            Return service.SaveBillingGroup(BillingGroup, audit, idSequense)
        End Using
        'Return _billingGroupAdminService.SaveBillingGroup(BillingGroup, audit, idSequense)
    End Function
End Class
