'***********************************************************************
' Author           : Juan Pablo Daza Medina
' Created          : 27-10-2022
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************
Imports Application.Payroll
Imports Infrastructure.CrossCutting.IOC
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities
Imports Domain.Entities
Partial Class PayrollService

    Public Function ListAllMinimumsalary(session As SessionValues) As List(Of Domain.Entities.MinimumSalary) Implements IPayrollMinimumSalary.ListAllMinimumsalary
        Using minimumSalaryAdminService As IMinimumSalaryAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of IMinimumSalaryAdminService)()
            Return minimumSalaryAdminService.ListAllMinimumSalary()
        End Using

    End Function

    Public Function DeleteMinimunSalary(minimumSalary As Domain.Entities.MinimumSalary, sesion As SessionValues) As ActionMessageResult(Of MinimumSalary) Implements IPayrollMinimumSalary.DeleteMinimunSalary
        Using minimumSalaryAdminService As IMinimumSalaryAdminService = IocFactory.Instance(sesion.TransactionalContainer).CurrentContainer.Resolve(Of IMinimumSalaryAdminService)()
            Return minimumSalaryAdminService.DeleteMinimunSalary(minimumSalary, sesion.AuditMessageWcf)
        End Using

    End Function

    Public Function SaveMinimunSalary(minimumSalary As Domain.Entities.MinimumSalary, session As SessionValues, idSequense As Int64, audit As AuditMessage) As ActionResult(Of Domain.Entities.MinimumSalary) Implements IPayrollMinimumSalary.SaveMinimunSalary
        Using minimumSalaryAdminService As IMinimumSalaryAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of IMinimumSalaryAdminService)()
            Return minimumSalaryAdminService.SaveMinimunSalary(minimumSalary, audit, idSequense)
        End Using

    End Function

    Public Function UpdateMinimunSalary(year As String, state As Boolean, session As SessionValues) As ActionResult(Of MinimumSalary) Implements IPayrollMinimumSalary.UpdateMinimunSalary
        Using minimumSalaryAdminService As IMinimumSalaryAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of IMinimumSalaryAdminService)()
            Return minimumSalaryAdminService.UpdateMinimunSalary(year, state, session.AuditMessageWcf)
        End Using

    End Function

    Public Function GetMinimunSalary(year As String, session As SessionValues) As Domain.Entities.MinimumSalary Implements IPayrollMinimumSalary.GetMinimunSalary
        Using minimumSalaryAdminService As IMinimumSalaryAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of IMinimumSalaryAdminService)()
            Return minimumSalaryAdminService.GetMinimunSalary(year, session.AuditMessageWcf)
        End Using

    End Function

    Public Function GetMinimumSalaryById(Id As Integer, session As SessionValues) As MinimumSalary Implements IPayrollMinimumSalary.GetMinimumSalaryById
        Using minimumSalaryAdminService As IMinimumSalaryAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of IMinimumSalaryAdminService)()
            Return minimumSalaryAdminService.GetMinimumSalaryById(Id)
        End Using

    End Function

End Class
