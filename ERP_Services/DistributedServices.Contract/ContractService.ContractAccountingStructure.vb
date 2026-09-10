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

    Public Function ChangeStateContractAccountingStructure(code As String, state As Boolean, audit As AuditMessage) As Domain.Base.Entities.ActionResult(Of Domain.Entities.ContractAccountingStructure) Implements IContractContractAccountingStructure.ChangeStateContractAccountingStructure
        Using service As IContractAccountingStructureAdminService = Container.Current.Resolve(Of IContractAccountingStructureAdminService)()
            Return service.ChangeStateContractAccountingStructure(code, state, audit)
        End Using
        'Return Me._contractAccountingStructureAdminService.ChangeStateContractAccountingStructure(code, state, audit)
    End Function

    Public Function DeleteContractAccountingStructure(ContractAccountingStructure As Domain.Entities.ContractAccountingStructure, audit As AuditMessage) As Domain.Base.Entities.ActionResult Implements IContractContractAccountingStructure.DeleteContractAccountingStructure
        Using service As IContractAccountingStructureAdminService = Container.Current.Resolve(Of IContractAccountingStructureAdminService)()
            Return service.DeleteContractAccountingStructure(ContractAccountingStructure, audit)
        End Using
        'Return Me._contractAccountingStructureAdminService.DeleteContractAccountingStructure(ContractAccountingStructure, audit)
    End Function

    Public Function GetContractAccountingStructure(code As String, audit As AuditMessage) As Domain.Base.Entities.ActionResult(Of Domain.Entities.ContractAccountingStructure) Implements IContractContractAccountingStructure.GetContractAccountingStructure
        Using service As IContractAccountingStructureAdminService = Container.Current.Resolve(Of IContractAccountingStructureAdminService)()
            Return service.GetContractAccountingStructure(code, audit)
        End Using
        'Return Me._contractAccountingStructureAdminService.GetContractAccountingStructure(code, audit)
    End Function

    Public Function GetContractAccountingStructureById(id As Integer, audit As AuditMessage) As Domain.Base.Entities.ActionResult(Of Domain.Entities.ContractAccountingStructure) Implements IContractContractAccountingStructure.GetContractAccountingStructureById
        Using service As IContractAccountingStructureAdminService = Container.Current.Resolve(Of IContractAccountingStructureAdminService)()
            Return service.GetContractAccountingStructureById(id, audit)
        End Using
        'Return Me._contractAccountingStructureAdminService.GetContractAccountingStructureById(id, audit)
    End Function

    Public Function SaveContractAccountingStructure(ContractAccountingStructure As Domain.Entities.ContractAccountingStructure, idSequense As Int64, audit As AuditMessage) As Domain.Base.Entities.ActionResult(Of Domain.Entities.ContractAccountingStructure) Implements IContractContractAccountingStructure.SaveContractAccountingStructure
        Using service As IContractAccountingStructureAdminService = Container.Current.Resolve(Of IContractAccountingStructureAdminService)()
            Return service.SaveContractAccountingStructure(ContractAccountingStructure, audit, idSequense)
        End Using
        'Return Me._contractAccountingStructureAdminService.SaveContractAccountingStructure(ContractAccountingStructure, audit, idSequense)
    End Function

    Public Function GetActiveListContractAccountingStructure(audit As AuditMessage) As Domain.Base.Entities.ActionResult(Of List(Of Domain.Entities.ContractAccountingStructure)) Implements IContractContractAccountingStructure.GetActiveListContractAccountingStructure
        Using service As IContractAccountingStructureAdminService = Container.Current.Resolve(Of IContractAccountingStructureAdminService)()
            Return service.GetActiveList(audit)
        End Using
    End Function

    Public Function GetListByCodesContractAccountingStructure(codes As List(Of String), audit As AuditMessage) As Domain.Base.Entities.ActionResult(Of List(Of Domain.Entities.ContractAccountingStructure)) Implements IContractContractAccountingStructure.GetListByCodesContractAccountingStructure
        Using service As IContractAccountingStructureAdminService = Container.Current.Resolve(Of IContractAccountingStructureAdminService)()
            Return service.GetListByCodes(codes, audit)
        End Using
    End Function

End Class
