Imports Infrastructure.CrossCutting.IOC
Imports Application.Payroll
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities

Partial Class PayrollService

    Public Function GetPermissionRoll(CodeRoll As String, RolId As Integer, session As SessionValues) As List(Of Domain.Payroll.Entities.PositionRoll) Implements IPayrollPermissionSchedule.GetPermissionRoll
        Using PermissionRolAdminService As IPermissionScheduleAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of IPermissionScheduleAdminService)()
            Return PermissionRolAdminService.GetPermissionRoll(CodeRoll, RolId)
        End Using
    End Function

    Public Function SavePermissionRol(ListPermissionRol As List(Of Domain.Payroll.Entities.PositionRoll), ListPermissionRolDelete As List(Of Domain.Payroll.Entities.PositionRoll), session As SessionValues) As ActionResult(Of List(Of Domain.Payroll.Entities.PositionRoll)) Implements IPayrollPermissionSchedule.SavePermissionRol
        Using PermissionRolAdminService As IPermissionScheduleAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of IPermissionScheduleAdminService)()
            Return PermissionRolAdminService.SavePermissionRol(ListPermissionRol, ListPermissionRolDelete, session.AuditMessageWcf)
        End Using
    End Function

    Public Function GetPositionUser(UserId As Integer, session As SessionValues) As List(Of Domain.Payroll.Entities.PositionUser) Implements IPayrollPermissionSchedule.GetPositionUser
        Using PermissionRolAdminService As IPermissionScheduleAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of IPermissionScheduleAdminService)()
            Return PermissionRolAdminService.GetPositionUser(UserId)
        End Using
    End Function

    Public Function GetFunctionalUnitResponsible(UserId As Integer, session As SessionValues) As List(Of Domain.Payroll.Entities.FunctionalUnitResponsible) Implements IPayrollPermissionSchedule.GetFunctionalUnitResponsible
        Using PermissionRolAdminService As IPermissionScheduleAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of IPermissionScheduleAdminService)()
            Return PermissionRolAdminService.GetFunctionalUnitResponsible(UserId)
        End Using
    End Function

    Public Function SavePermissionUser(ListPermissionUser As List(Of Domain.Payroll.Entities.PositionUser), ListDeletePermissionUser As List(Of Domain.Payroll.Entities.PositionUser), ListPermissionFunctionalUnit As List(Of Domain.Payroll.Entities.FunctionalUnitResponsible), ListDeletePermissionFunctionalUnit As List(Of Domain.Payroll.Entities.FunctionalUnitResponsible), session As SessionValues) As ActionResult(Of String) Implements IPayrollPermissionSchedule.SavePermissionUser
        Using PermissionRolAdminService As IPermissionScheduleAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of IPermissionScheduleAdminService)()
            Return PermissionRolAdminService.SavePermissionUser(ListPermissionUser, ListDeletePermissionUser, ListPermissionFunctionalUnit, ListDeletePermissionFunctionalUnit, session.AuditMessageWcf)
        End Using
    End Function
End Class
