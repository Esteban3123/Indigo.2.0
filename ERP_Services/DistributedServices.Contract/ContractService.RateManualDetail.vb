'***********************************************************************
' Assembly         : DistributedServices.Payments
' Author           : Carlos Mario Arias Rubiano
' Created          : 17/10/2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities
Imports System.ServiceModel
Imports Application.Contract
Imports Microsoft.Practices.Unity

Partial Class ContractService

    Public Function ChangeStateRateManualDetail(id As Integer, state As Boolean, audit As AuditMessage) As Domain.Base.Entities.ActionResult(Of Domain.Entities.RateManualDetail) Implements IContractRateManualDetail.ChangeStateRateManualDetail
        Using service As IRateManualDetailAdminService = Container.Current.Resolve(Of IRateManualDetailAdminService)()
            Return service.ChangeStateRateManualDetail(id, state, audit)
        End Using
        'Return Me._rateManualDetailasAdminService.ChangeStateRateManualDetail(id, state, audit)
    End Function

    Public Function DeleteRateManualDetail(RateManualDetail As Domain.Entities.RateManualDetail, audit As AuditMessage) As Domain.Base.Entities.ActionResult Implements IContractRateManualDetail.DeleteRateManualDetail
        Using service As IRateManualDetailAdminService = Container.Current.Resolve(Of IRateManualDetailAdminService)()
            Return service.DeleteRateManualDetail(RateManualDetail, audit)
        End Using
        'Return Me._rateManualDetailasAdminService.DeleteRateManualDetail(RateManualDetail, audit)
    End Function

    Public Function GetRateManualDetailById(id As Integer, audit As AuditMessage) As Domain.Base.Entities.ActionResult(Of Domain.Entities.RateManualDetail) Implements IContractRateManualDetail.GetRateManualDetailById
        Using service As IRateManualDetailAdminService = Container.Current.Resolve(Of IRateManualDetailAdminService)()
            Return service.GetRateManualDetailById(id, audit)
        End Using
        'Return Me._rateManualDetailasAdminService.GetRateManualDetailById(id, audit)
    End Function

    Public Function SaveRateManualDetail(RateManualDetail As Domain.Entities.RateManualDetail, audit As AuditMessage) As Domain.Base.Entities.ActionResult(Of Domain.Entities.RateManualDetail) Implements IContractRateManualDetail.SaveRateManualDetail
        Using service As IRateManualDetailAdminService = Container.Current.Resolve(Of IRateManualDetailAdminService)()
            Return service.SaveRateManualDetail(RateManualDetail, audit)
        End Using
        'Return Me._rateManualDetailasAdminService.SaveRateManualDetail(RateManualDetail, audit)
    End Function

    Public Function SaveListRateManualDetail(ListRateManualDetail As List(Of Domain.Entities.RateManualDetail), ListDeleteRateManualDetail As List(Of RateManualDetail), audit As AuditMessage) As Domain.Base.Entities.ActionResult(Of List(Of Domain.Entities.RateManualDetail)) Implements IContractRateManualDetail.SaveListRateManualDetail
        Using service As IRateManualDetailAdminService = Container.Current.Resolve(Of IRateManualDetailAdminService)()
            Return service.SaveListRateManualDetail(ListRateManualDetail, ListDeleteRateManualDetail, audit)
        End Using
        'Return Me._rateManualDetailasAdminService.SaveListRateManualDetail(ListRateManualDetail, ListDeleteRateManualDetail, audit)
    End Function

End Class
