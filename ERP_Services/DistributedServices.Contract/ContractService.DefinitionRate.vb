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

    Public Function ChangeStateDefinitionRate(code As String, state As Boolean, audit As AuditMessage) As Domain.Base.Entities.ActionResult(Of Domain.Entities.DefinitionRate) Implements IContractServiceDefinitionRate.ChangeStateDefinitionRate
        Using service As IDefinitionRateAdminService = Container.Current.Resolve(Of IDefinitionRateAdminService)()
            Return service.ChangeStateDefinitionRate(code, state, audit)
        End Using
        'Return Me._definitionRateAdminService.ChangeStateDefinitionRate(code, state, audit)
    End Function

    Public Function DeleteDefinitionRate(DefinitionRate As Domain.Entities.DefinitionRate, audit As AuditMessage) As Domain.Base.Entities.ActionResult Implements IContractServiceDefinitionRate.DeleteDefinitionRate
        Using service As IDefinitionRateAdminService = Container.Current.Resolve(Of IDefinitionRateAdminService)()
            Return service.DeleteDefinitionRate(DefinitionRate, audit)
        End Using
        'Return Me._definitionRateAdminService.DeleteDefinitionRate(DefinitionRate, audit)
    End Function

    Public Function GetDefinitionRate(code As String, audit As AuditMessage) As Domain.Base.Entities.ActionResult(Of Domain.Entities.DefinitionRate) Implements IContractServiceDefinitionRate.GetDefinitionRate
        Using service As IDefinitionRateAdminService = Container.Current.Resolve(Of IDefinitionRateAdminService)()
            Return service.GetDefinitionRate(code, audit)
        End Using
        'Return Me._definitionRateAdminService.GetDefinitionRate(code, audit)
    End Function

    Public Function GetDefinitionRateById(id As Integer, audit As AuditMessage) As Domain.Base.Entities.ActionResult(Of Domain.Entities.DefinitionRate) Implements IContractServiceDefinitionRate.GetDefinitionRateById
        Using service As IDefinitionRateAdminService = Container.Current.Resolve(Of IDefinitionRateAdminService)()
            Return service.GetDefinitionRateById(id, audit)
        End Using
        'Return Me._definitionRateAdminService.GetDefinitionRateById(id, audit)
    End Function

    Public Function SaveDefinitionRate(DefinitionRate As Domain.Entities.DefinitionRate, ListDeleteDefinitionRateDetail As List(Of DefinitionRateDetail), listDeleteDefinitionRateDetailCondition As List(Of DefinitionRateDetailCondition), Company As String, idSequense As Int64, audit As AuditMessage) As Domain.Base.Entities.ActionResult(Of Domain.Entities.DefinitionRate) Implements IContractServiceDefinitionRate.SaveDefinitionRate
        Using service As IDefinitionRateAdminService = Container.Current.Resolve(Of IDefinitionRateAdminService)()
            Return service.SaveDefinitionRate(DefinitionRate, ListDeleteDefinitionRateDetail, listDeleteDefinitionRateDetailCondition, Company, audit, idSequense)
        End Using
        'Return Me._definitionRateAdminService.SaveDefinitionRate(DefinitionRate, ListDeleteDefinitionRateDetail, Company, audit, idSequense)
    End Function

End Class
