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

    Public Function ChangeStateCupsSubgroup(code As String, state As Boolean, audit As AuditMessage) As Domain.Base.Entities.ActionResult(Of Domain.Entities.CupsSubgroup) Implements IContractCupsSubGroup.ChangeStateCupsSubgroup
        Using service As ICupsSubGroupAdminService = Container.Current.Resolve(Of ICupsSubGroupAdminService)()
            Return service.ChangeStateCupsSubGroup(code, state, audit)
        End Using
        'Return Me._cupsSubGroupAdminService.ChangeStateCupsSubGroup(code, state, audit)
    End Function

    Public Function DeleteCupsSubgroup(CupsSubgroup As Domain.Entities.CupsSubgroup, audit As AuditMessage) As Domain.Base.Entities.ActionResult Implements IContractCupsSubGroup.DeleteCupsSubgroup
        Using service As ICupsSubGroupAdminService = Container.Current.Resolve(Of ICupsSubGroupAdminService)()
            Return service.DeleteCupsSubGroup(CupsSubgroup, audit)
        End Using
        'Return Me._cupsSubGroupAdminService.DeleteCupsSubGroup(CupsSubgroup, audit)
    End Function

    Public Function GetCupsSubgroup(code As String, audit As AuditMessage) As Domain.Base.Entities.ActionResult(Of Domain.Entities.CupsSubgroup) Implements IContractCupsSubGroup.GetCupsSubgroup
        Using service As ICupsSubGroupAdminService = Container.Current.Resolve(Of ICupsSubGroupAdminService)()
            Return service.GetCupsSubGroup(code, audit)
        End Using
        'Return Me._cupsSubGroupAdminService.GetCupsSubGroup(code, audit)
    End Function

    Public Function GetCupsSubgroupById(id As Integer, audit As AuditMessage) As Domain.Base.Entities.ActionResult(Of Domain.Entities.CupsSubgroup) Implements IContractCupsSubGroup.GetCupsSubgroupById
        Using service As ICupsSubGroupAdminService = Container.Current.Resolve(Of ICupsSubGroupAdminService)()
            Return service.GetCupsSubGroupById(id, audit)
        End Using
        'Return Me._cupsSubGroupAdminService.GetCupsSubGroupById(id, audit)
    End Function

    Public Function SaveCupsSubgroup(CupsSubgroup As Domain.Entities.CupsSubgroup, idSequense As Int64, audit As AuditMessage) As Domain.Base.Entities.ActionResult(Of Domain.Entities.CupsSubgroup) Implements IContractCupsSubGroup.SaveCupsSubgroup
        Using service As ICupsSubGroupAdminService = Container.Current.Resolve(Of ICupsSubGroupAdminService)()
            Return service.SaveCupsSubGroup(CupsSubgroup, audit, idSequense)
        End Using
        'Return Me._cupsSubGroupAdminService.SaveCupsSubGroup(CupsSubgroup, audit, idSequense)
    End Function

End Class
