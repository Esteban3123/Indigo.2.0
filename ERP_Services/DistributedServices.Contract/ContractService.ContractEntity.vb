'***********************************************************************
' Assembly         : DistributedServices.Payments
' Author           : Carlos Mario Arias Rubiano
' Created          : 21/10/2014
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

    Public Function ChangeStateContractEntity(code As String, state As Boolean, audit As AuditMessage) As Domain.Base.Entities.ActionResult(Of Domain.Entities.ContractEntity) Implements IContractContractEntity.ChangeStateContractEntity
        Using service As IContractEntityAdminService = Container.Current.Resolve(Of IContractEntityAdminService)()
            Return service.ChangeStateContractEntity(code, state, audit)
        End Using
        'Return Me._contractEntityAdminService.ChangeStateContractEntity(code, state, audit)
    End Function

    Public Function DeleteContractEntity(ContractEntity As Domain.Entities.ContractEntity, audit As AuditMessage) As Domain.Base.Entities.ActionResult Implements IContractContractEntity.DeleteContractEntity
        Using service As IContractEntityAdminService = Container.Current.Resolve(Of IContractEntityAdminService)()
            Return service.DeleteContractEntity(ContractEntity, audit)
        End Using
        'Return Me._contractEntityAdminService.DeleteContractEntity(ContractEntity, audit)
    End Function

    Public Function GetContractEntity(code As String, audit As AuditMessage) As Domain.Base.Entities.ActionResult(Of Domain.Entities.ContractEntity) Implements IContractContractEntity.GetContractEntity
        Using service As IContractEntityAdminService = Container.Current.Resolve(Of IContractEntityAdminService)()
            Return service.GetContractEntity(code, audit)
        End Using
        'Return Me._contractEntityAdminService.GetContractEntity(code, audit)
    End Function

    Public Function GetContractEntityById(id As Integer, audit As AuditMessage) As Domain.Base.Entities.ActionResult(Of Domain.Entities.ContractEntity) Implements IContractContractEntity.GetContractEntityById
        Using service As IContractEntityAdminService = Container.Current.Resolve(Of IContractEntityAdminService)()
            Return service.GetContractEntityById(id, audit)
        End Using
        'Return Me._contractEntityAdminService.GetContractEntityById(id, audit)
    End Function

    Public Function SaveContractEntity(ContractEntity As Domain.Entities.ContractEntity, idSequense As Int64, audit As AuditMessage) As Domain.Base.Entities.ActionResult(Of Domain.Entities.ContractEntity) Implements IContractContractEntity.SaveContractEntity
        Using service As IContractEntityAdminService = Container.Current.Resolve(Of IContractEntityAdminService)()
            Return service.SaveContractEntity(ContractEntity, audit, idSequense)
        End Using
        'Return Me._contractEntityAdminService.SaveContractEntity(ContractEntity, audit, idSequense)
    End Function

End Class
