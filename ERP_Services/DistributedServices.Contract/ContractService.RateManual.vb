'***********************************************************************
' Assembly         : DistributedServices.Payments
' Author           : Carlos Mario Arias Rubiano
' Created          : 07/04/2014
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

    Public Function ChangeStateRateManual(code As String, state As Boolean, audit As AuditMessage) As Domain.Base.Entities.ActionResult(Of Domain.Entities.RateManual) Implements IContractRateManual.ChangeStateRateManual
        Using service As IRateManualAdminService = Container.Current.Resolve(Of IRateManualAdminService)()
            Return service.ChangeStateRateManual(code, state, audit)
        End Using
        'Return Me._rateManualAdminService.ChangeStateRateManual(code, state, audit)
    End Function

    Public Function DeleteRateManual(RateManual As Domain.Entities.RateManual, audit As AuditMessage) As Domain.Base.Entities.ActionResult Implements IContractRateManual.DeleteRateManual
        Using service As IRateManualAdminService = Container.Current.Resolve(Of IRateManualAdminService)()
            Return service.DeleteRateManual(RateManual, audit)
        End Using
        'Return Me._rateManualAdminService.DeleteRateManual(RateManual, audit)
    End Function

    Public Function GetRateManual(code As String, audit As AuditMessage) As Domain.Base.Entities.ActionResult(Of Domain.Entities.RateManual) Implements IContractRateManual.GetRateManual
        Using service As IRateManualAdminService = Container.Current.Resolve(Of IRateManualAdminService)()
            Return service.GetRateManual(code, audit)
        End Using
        'Return Me._rateManualAdminService.GetRateManual(code, audit)
    End Function

    Public Function GetRateManualById(id As Integer, audit As AuditMessage) As Domain.Base.Entities.ActionResult(Of Domain.Entities.RateManual) Implements IContractRateManual.GetRateManualById
        Using service As IRateManualAdminService = Container.Current.Resolve(Of IRateManualAdminService)()
            Return service.GetRateManualById(id, audit)
        End Using
        'Return Me._rateManualAdminService.GetRateManualById(id, audit)
    End Function

    Public Function SaveRateManual(RateManual As Domain.Entities.RateManual, idSequense As Int64, audit As AuditMessage) As Domain.Base.Entities.ActionResult(Of Domain.Entities.RateManual) Implements IContractRateManual.SaveRateManual
        Using service As IRateManualAdminService = Container.Current.Resolve(Of IRateManualAdminService)()
            Return service.SaveRateManual(RateManual, audit, idSequense)
        End Using
        'Return Me._rateManualAdminService.SaveRateManual(RateManual, audit, idSequense)
    End Function

    Public Function CopyAndPasteRateManual(data As List(Of List(Of String)), ServiceManual As Integer) As Domain.Base.Entities.ActionResult(Of List(Of Domain.Entities.RateManualDetail), List(Of Tuple(Of String, Integer))) Implements IContractRateManual.CopyAndPasteRateManual
        Using service As IRateManualAdminService = Container.Current.Resolve(Of IRateManualAdminService)()
            Return service.CopyAndPasteRateManual(data, ServiceManual)
        End Using
        'Return Me._rateManualAdminService.CopyAndPasteRateManual(data, ServiceManual)
    End Function

    Public Function CopyAndPasteRateManualSurgical(data As List(Of List(Of String)), ServiceManual As Integer) As Domain.Base.Entities.ActionResult(Of List(Of Domain.Entities.RateManualDetailSurgical), List(Of Tuple(Of String, Integer))) Implements IContractRateManual.CopyAndPasteRateManualSurgical
        Using service As IRateManualAdminService = Container.Current.Resolve(Of IRateManualAdminService)()
            Return service.CopyAndPasteRateManualSurgical(data, ServiceManual)
        End Using
        'Return Me._rateManualAdminService.CopyAndPasteRateManualSurgical(data, ServiceManual)
    End Function

End Class
