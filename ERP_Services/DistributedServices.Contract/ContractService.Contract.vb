'***********************************************************************
' Assembly         : DistributedServices.Contracts
' Author           : Carlos Mario Arias Rubiano
' Created          : 01/10/2014
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

    Public Function ChangeStateContract(code As String, state As Integer, audit As AuditMessage) As Domain.Base.Entities.ActionResult(Of Domain.Entities.Contract) Implements IContractContract.ChangeStateContract
        Using service As IContractAdminService = Container.Current.Resolve(Of IContractAdminService)()
            Return service.ChangeStateContract(code, state, audit)
        End Using
        'Return Me._contractAdminService.ChangeStateContract(code, state, audit)
    End Function

    Public Function DeleteContract(Contract As Domain.Entities.Contract, audit As AuditMessage) As Domain.Base.Entities.ActionResult Implements IContractContract.DeleteContract
        Using service As IContractAdminService = Container.Current.Resolve(Of IContractAdminService)()
            Return service.DeleteContract(Contract, audit)
        End Using
        'Return Me._contractAdminService.DeleteContract(Contract, audit)
    End Function

    Public Function GetContract(code As String, audit As AuditMessage) As Domain.Base.Entities.ActionResult(Of Domain.Entities.Contract) Implements IContractContract.GetContract
        Using service As IContractAdminService = Container.Current.Resolve(Of IContractAdminService)()
            Return service.GetContract(code, audit)
        End Using
        'Return Me._contractAdminService.GetContract(code, audit)
    End Function

    Public Function GetContractById(id As Integer, audit As AuditMessage) As Domain.Base.Entities.ActionResult(Of Domain.Entities.Contract) Implements IContractContract.GetContractById
        Using service As IContractAdminService = Container.Current.Resolve(Of IContractAdminService)()
            Return service.GetContractById(id, audit)
        End Using
        'Return Me._contractAdminService.GetContractById(id, audit)
    End Function

    Public Function SaveContract(Contract As Domain.Entities.Contract, idSequense As Int64, audit As AuditMessage) As Domain.Base.Entities.ActionResult(Of Domain.Entities.Contract) Implements IContractContract.SaveContract
        Using service As IContractAdminService = Container.Current.Resolve(Of IContractAdminService)()
            Return service.SaveContract(Contract, audit, idSequense)
        End Using
        'Return Me._contractAdminService.SaveContract(Contract, audit, idSequense)
    End Function

    Public Function GetContractByIdWithAggregates(id As Integer, audit As AuditMessage) As Domain.Base.Entities.ActionResult(Of Domain.Entities.Contract) Implements IContractContract.GetContractByIdWithAggregates
        Using service As IContractAdminService = Container.Current.Resolve(Of IContractAdminService)()
            Return service.GetContractByIdWithAggregates(id, audit)
        End Using
        'Return Me._contractAdminService.GetContractByIdWithAggregates(id, audit)
    End Function

End Class
