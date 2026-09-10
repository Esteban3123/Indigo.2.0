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

    Public Function ChangeStateHealthAdministrator(code As String, state As Boolean, audit As AuditMessage) As Domain.Base.Entities.ActionResult(Of Domain.Entities.HealthAdministrator) Implements IContractHealthAdministrator.ChangeStateHealthAdministrator
        Using service As IHealthAdministratorAdminService = Container.Current.Resolve(Of IHealthAdministratorAdminService)()
            Return service.ChangeStateHealthAdministrator(code, state, audit)
        End Using
        'Return Me._healthAdministratorAdminService.ChangeStateHealthAdministrator(code, state, audit)
    End Function

    Public Function DeleteHealthAdministrator(HealthAdministrator As Domain.Entities.HealthAdministrator, audit As AuditMessage) As Domain.Base.Entities.ActionResult Implements IContractHealthAdministrator.DeleteHealthAdministrator
        Using service As IHealthAdministratorAdminService = Container.Current.Resolve(Of IHealthAdministratorAdminService)()
            Return service.DeleteHealthAdministrator(HealthAdministrator, audit)
        End Using
        'Return Me._healthAdministratorAdminService.DeleteHealthAdministrator(HealthAdministrator, audit)
    End Function

    Public Function GetHealthAdministrator(code As String, audit As AuditMessage) As Domain.Base.Entities.ActionResult(Of Domain.Entities.HealthAdministrator) Implements IContractHealthAdministrator.GetHealthAdministrator
        Using service As IHealthAdministratorAdminService = Container.Current.Resolve(Of IHealthAdministratorAdminService)()
            Return service.GetHealthAdministrator(code, audit)
        End Using
        'Return Me._healthAdministratorAdminService.GetHealthAdministrator(code, audit)
    End Function

    Public Function GetHealthAdministratorById(id As Integer, audit As AuditMessage) As Domain.Base.Entities.ActionResult(Of Domain.Entities.HealthAdministrator) Implements IContractHealthAdministrator.GetHealthAdministratorById
        Using service As IHealthAdministratorAdminService = Container.Current.Resolve(Of IHealthAdministratorAdminService)()
            Return service.GetHealthAdministratorById(id, audit)
        End Using
        'Return Me._healthAdministratorAdminService.GetHealthAdministratorById(id, audit)
    End Function

    Public Function SaveHealthAdministrator(HealthAdministrator As Domain.Entities.HealthAdministrator, idSequense As Int64, audit As AuditMessage) As Domain.Base.Entities.ActionResult(Of Domain.Entities.HealthAdministrator) Implements IContractHealthAdministrator.SaveHealthAdministrator
        Using service As IHealthAdministratorAdminService = Container.Current.Resolve(Of IHealthAdministratorAdminService)()
            Return service.SaveHealthAdministrator(HealthAdministrator, audit, idSequense)
        End Using
        'Return Me._healthAdministratorAdminService.SaveHealthAdministrator(HealthAdministrator, audit, idSequense)
    End Function

End Class
