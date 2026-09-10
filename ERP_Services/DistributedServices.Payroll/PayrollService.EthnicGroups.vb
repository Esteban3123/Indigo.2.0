Imports Application.Payroll
Imports Infrastructure.CrossCutting.IOC
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities

Partial Class PayrollService

    Public Function DeleteEthnicGroups(pEthnicGroups As Domain.Payroll.Entities.EthnicGroups, session As SessionValues) As ActionResult Implements IPayrollEthnicGroups.DeleteEthnicGroups
        Using ethnicGroupsAdminService As IEthnicGroupsAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of IEthnicGroupsAdminService)()
            Return ethnicGroupsAdminService.DeleteEthnicGroups(pEthnicGroups, session.AuditMessageWcf)
        End Using
    End Function

    Public Function GetEthnicGroups(code As String, tracking As Boolean, session As SessionValues) As ActionResult(Of Domain.Payroll.Entities.EthnicGroups) Implements IPayrollEthnicGroups.GetEthnicGroups
        Using ethnicGroupsAdminService As IEthnicGroupsAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of IEthnicGroupsAdminService)()
            Return ethnicGroupsAdminService.GetEthnicGroups(code, tracking, session.AuditMessageWcf)
        End Using
    End Function

    Public Function SaveEthnicGroups(pEthnicGroups As Domain.Payroll.Entities.EthnicGroups, session As SessionValues, idSequense As Int64) As ActionResult(Of Domain.Payroll.Entities.EthnicGroups) Implements IPayrollEthnicGroups.SaveEthnicGroups
        Using ethnicGroupsAdminService As IEthnicGroupsAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of IEthnicGroupsAdminService)()
            Return ethnicGroupsAdminService.SaveEthnicGroups(pEthnicGroups, session.AuditMessageWcf, idSequense)
        End Using
    End Function

    Public Function GetEthnicGroupsById(ID As String, tracking As Boolean, session As SessionValues) As ActionResult(Of Domain.Payroll.Entities.EthnicGroups) Implements IPayrollEthnicGroups.GetEthnicGroupsById
        Using ethnicGroupsAdminService As IEthnicGroupsAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of IEthnicGroupsAdminService)()
            Return ethnicGroupsAdminService.GetEthnicGroupsById(ID, tracking, session.AuditMessageWcf)
        End Using
    End Function

    Public Function ChangeStateEthnicGroups(code As String, state As Boolean, session As SessionValues) As Domain.Base.Entities.ActionResult(Of Domain.Payroll.Entities.EthnicGroups) Implements IPayrollEthnicGroups.ChangeStateEthnicGroups
        Using ethnicGroupsAdminService As IEthnicGroupsAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of IEthnicGroupsAdminService)()
            Return ethnicGroupsAdminService.ChangeState(code, state, session.AuditMessageWcf)
        End Using
    End Function

End Class
