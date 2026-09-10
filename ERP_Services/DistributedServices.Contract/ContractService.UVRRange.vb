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
Imports Application.Contract
Imports Microsoft.Practices.Unity

Partial Class ContractService

    Public Function ChangeStateUVRRange(code As String, state As Boolean, audit As AuditMessage) As Domain.Base.Entities.ActionResult(Of Domain.Entities.UVRRange) Implements IContractUVRRange.ChangeStateUVRRange
        Using service As IUVRRangeAdminService = Container.Current.Resolve(Of IUVRRangeAdminService)()
            Return service.ChangeStateUVRRange(code, state, audit)
        End Using
        'Return Me._uvrRangeAdminService.ChangeStateUVRRange(code, state, audit)
    End Function

    Public Function DeleteUVRRange(UVRRange As Domain.Entities.UVRRange, audit As AuditMessage) As Domain.Base.Entities.ActionResult Implements IContractUVRRange.DeleteUVRRange
        Using service As IUVRRangeAdminService = Container.Current.Resolve(Of IUVRRangeAdminService)()
            Return service.DeleteUVRRange(UVRRange, audit)
        End Using
        'Return Me._uvrRangeAdminService.DeleteUVRRange(UVRRange, audit)
    End Function

    Public Function GetUVRRange(code As String, audit As AuditMessage) As Domain.Base.Entities.ActionResult(Of Domain.Entities.UVRRange) Implements IContractUVRRange.GetUVRRange
        Using service As IUVRRangeAdminService = Container.Current.Resolve(Of IUVRRangeAdminService)()
            Return service.GetUVRRange(code, audit)
        End Using
        'Return Me._uvrRangeAdminService.GetUVRRange(code, audit)
    End Function

    Public Function GetUVRRangeById(id As Integer, audit As AuditMessage) As Domain.Base.Entities.ActionResult(Of Domain.Entities.UVRRange) Implements IContractUVRRange.GetUVRRangeById
        Using service As IUVRRangeAdminService = Container.Current.Resolve(Of IUVRRangeAdminService)()
            Return service.GetUVRRangeById(id, audit)
        End Using
        'Return Me._uvrRangeAdminService.GetUVRRangeById(id, audit)
    End Function

    Public Function SaveUVRRange(UVRRange As Domain.Entities.UVRRange, idSequense As Int64, audit As AuditMessage) As Domain.Base.Entities.ActionResult(Of Domain.Entities.UVRRange) Implements IContractUVRRange.SaveUVRRange
        Using service As IUVRRangeAdminService = Container.Current.Resolve(Of IUVRRangeAdminService)()
            Return service.SaveUVRRange(UVRRange, audit, idSequense)
        End Using
        'Return Me._uvrRangeAdminService.SaveUVRRange(UVRRange, audit, idSequense)
    End Function

End Class
