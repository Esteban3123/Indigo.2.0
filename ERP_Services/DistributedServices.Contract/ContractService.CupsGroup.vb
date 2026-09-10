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

    Public Function ChangeStateCupsGroup(code As String, state As Boolean, audit As AuditMessage) As Domain.Base.Entities.ActionResult(Of Domain.Entities.CupsGroup) Implements IContractCupsGroup.ChangeStateCupsGroup
        Using service As ICupsGroupAdminService = Container.Current.Resolve(Of ICupsGroupAdminService)()
            Return service.ChangeStateCupsGroup(code, state, audit)
        End Using
        'Return Me._cupsGroupAdminService.ChangeStateCupsGroup(code, state, audit)
    End Function

    Public Function DeleteCupsGroup(CupsGroup As Domain.Entities.CupsGroup, audit As AuditMessage) As Domain.Base.Entities.ActionResult Implements IContractCupsGroup.DeleteCupsGroup
        Using service As ICupsGroupAdminService = Container.Current.Resolve(Of ICupsGroupAdminService)()
            Return service.DeleteCupsGroup(CupsGroup, audit)
        End Using
        'Return Me._cupsGroupAdminService.DeleteCupsGroup(CupsGroup, audit)
    End Function

    Public Function GetCupsGroup(code As String, audit As AuditMessage) As Domain.Base.Entities.ActionResult(Of Domain.Entities.CupsGroup) Implements IContractCupsGroup.GetCupsGroup
        Using service As ICupsGroupAdminService = Container.Current.Resolve(Of ICupsGroupAdminService)()
            Return service.GetCupsGroup(code, audit)
        End Using
        'Return Me._cupsGroupAdminService.GetCupsGroup(code, audit)
    End Function

    Public Function GetCupsGroupById(id As Integer, audit As AuditMessage) As Domain.Base.Entities.ActionResult(Of Domain.Entities.CupsGroup) Implements IContractCupsGroup.GetCupsGroupById
        Using service As ICupsGroupAdminService = Container.Current.Resolve(Of ICupsGroupAdminService)()
            Return service.GetCupsGroupById(id, audit)
        End Using
        'Return Me._cupsGroupAdminService.GetCupsGroupById(id, audit)
    End Function

    Public Function SaveCupsGroup(CupsGroup As Domain.Entities.CupsGroup, idSequense As Int64, audit As AuditMessage) As Domain.Base.Entities.ActionResult(Of Domain.Entities.CupsGroup) Implements IContractCupsGroup.SaveCupsGroup
        Using service As ICupsGroupAdminService = Container.Current.Resolve(Of ICupsGroupAdminService)()
            Return service.SaveCupsGroup(CupsGroup, audit, idSequense)
        End Using
        'Return Me._cupsGroupAdminService.SaveCupsGroup(CupsGroup, audit, idSequense)
    End Function

End Class
