'***********************************************************************
' Assembly         : DistributedServices.Payments
' Author           : Carlos Mario Arias Rubiano
' Created          : 15/10/2014
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

    Public Function ChangeStateSurgicalGroup(code As String, state As Boolean, audit As AuditMessage) As Domain.Base.Entities.ActionResult(Of Domain.Entities.SurgicalGroup) Implements IContractSurgicalGroup.ChangeStateSurgicalGroup
        Using service As ISurgicalGroupAdminService = Container.Current.Resolve(Of ISurgicalGroupAdminService)()
            Return service.ChangeStateSurgicalGroup(code, state, audit)
        End Using
        'Return Me._surgicalGroupAdminService.ChangeStateSurgicalGroup(code, state, audit)
    End Function

    Public Function DeleteSurgicalGroup(SurgicalGroup As Domain.Entities.SurgicalGroup, audit As AuditMessage) As Domain.Base.Entities.ActionResult Implements IContractSurgicalGroup.DeleteSurgicalGroup
        Using service As ISurgicalGroupAdminService = Container.Current.Resolve(Of ISurgicalGroupAdminService)()
            Return service.DeleteSurgicalGroup(SurgicalGroup, audit)
        End Using
        'Return Me._surgicalGroupAdminService.DeleteSurgicalGroup(SurgicalGroup, audit)
    End Function

    Public Function GetSurgicalGroup(code As String, audit As AuditMessage) As Domain.Base.Entities.ActionResult(Of Domain.Entities.SurgicalGroup) Implements IContractSurgicalGroup.GetSurgicalGroup
        Using service As ISurgicalGroupAdminService = Container.Current.Resolve(Of ISurgicalGroupAdminService)()
            Return service.GetSurgicalGroup(code, audit)
        End Using
        'Return Me._surgicalGroupAdminService.GetSurgicalGroup(code, audit)
    End Function

    Public Function GetSurgicalGroupById(id As Integer, audit As AuditMessage) As Domain.Base.Entities.ActionResult(Of Domain.Entities.SurgicalGroup) Implements IContractSurgicalGroup.GetSurgicalGroupById
        Using service As ISurgicalGroupAdminService = Container.Current.Resolve(Of ISurgicalGroupAdminService)()
            Return service.GetSurgicalGroupById(id, audit)
        End Using
        'Return Me._surgicalGroupAdminService.GetSurgicalGroupById(id, audit)
    End Function

    Public Function SaveSurgicalGroup(SurgicalGroup As Domain.Entities.SurgicalGroup, idSequense As Int64, audit As AuditMessage) As Domain.Base.Entities.ActionResult(Of Domain.Entities.SurgicalGroup) Implements IContractSurgicalGroup.SaveSurgicalGroup
        Using service As ISurgicalGroupAdminService = Container.Current.Resolve(Of ISurgicalGroupAdminService)()
            Return service.SaveSurgicalGroup(SurgicalGroup, audit, idSequense)
        End Using
        'Return Me._surgicalGroupAdminService.SaveSurgicalGroup(SurgicalGroup, audit, idSequense)
    End Function

End Class
