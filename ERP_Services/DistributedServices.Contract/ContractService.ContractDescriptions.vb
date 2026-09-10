'***********************************************************************
' Assembly         : DistributedServices.Payments
' Author           : Carlos Mario Arias Rubiano
' Created          : 07/04/2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Application.Contract
Imports Infrastructure.CrossCutting.Base
Imports Microsoft.Practices.Unity

Partial Class ContractService

    Public Function ChangeStateContractDescriptions(code As String, state As Boolean, audit As AuditMessage) As Domain.Base.Entities.ActionResult(Of Domain.Entities.ContractDescriptions) Implements IContractContractDescriptions.ChangeStateContractDescriptions
        Using service As IContractDescriptionsAdminService = Container.Current.Resolve(Of IContractDescriptionsAdminService)()
            Return service.ChangeStateContractDescriptions(code, state, audit)
        End Using
        'Return Me._cupsGroupAdminService.ChangeStateCupsGroup(code, state, audit)
    End Function

    Public Function DeleteContractDescriptions(ContractDescriptions As Domain.Entities.ContractDescriptions, audit As AuditMessage) As Domain.Base.Entities.ActionResult Implements IContractContractDescriptions.DeleteContractDescriptions
        Using service As IContractDescriptionsAdminService = Container.Current.Resolve(Of IContractDescriptionsAdminService)()
            Return service.DeleteContractDescriptions(ContractDescriptions, audit)
        End Using
        'Return Me._cupsGroupAdminService.DeleteCupsGroup(CupsGroup, audit)
    End Function

    Public Function GetContractDescriptions(code As String, audit As AuditMessage) As Domain.Base.Entities.ActionResult(Of Domain.Entities.ContractDescriptions) Implements IContractContractDescriptions.GetContractDescriptions
        Using service As IContractDescriptionsAdminService = Container.Current.Resolve(Of IContractDescriptionsAdminService)()
            Return service.GetContractDescriptions(code, audit)
        End Using
        'Return Me._cupsGroupAdminService.GetCupsGroup(code, audit)
    End Function

    Public Function GetContractDescriptionsById(id As Integer, audit As AuditMessage) As Domain.Base.Entities.ActionResult(Of Domain.Entities.ContractDescriptions) Implements IContractContractDescriptions.GetContractDescriptionsById
        Using service As IContractDescriptionsAdminService = Container.Current.Resolve(Of IContractDescriptionsAdminService)()
            Return service.GetContractDescriptionsById(id, audit)
        End Using
        'Return Me._cupsGroupAdminService.GetCupsGroupById(id, audit)
    End Function

    Public Function SaveContractDescriptions(ContractDescriptions As Domain.Entities.ContractDescriptions, idSequense As Int64, audit As AuditMessage) As Domain.Base.Entities.ActionResult(Of Domain.Entities.ContractDescriptions) Implements IContractContractDescriptions.SaveContractDescriptions
        Using service As IContractDescriptionsAdminService = Container.Current.Resolve(Of IContractDescriptionsAdminService)()
            Return service.SaveContractDescriptions(ContractDescriptions, audit, idSequense)
        End Using
        'Return Me._cupsGroupAdminService.SaveCupsGroup(CupsGroup, audit, idSequense)
    End Function

End Class
