'***********************************************************************
' Assembly         : DistributedServices.Payroll
' Author           : Cristhian Mauricio Salazar
' Created          : 24-10-2013
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Application.Payroll
Imports Infrastructure.CrossCutting.IOC
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities

Partial Class PayrollService
    Implements IPayrollResumptionHoliday

    Public Function GetResumptionHoliday(EmployeeId As Integer, session As SessionValues) As ActionResult(Of List(Of Domain.Payroll.Entities.ResumptionHoliday)) Implements IPayrollResumptionHoliday.GetResumptionHoliday
        Using adminService As IResumptionHolidayAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of IResumptionHolidayAdminService)()
            Return adminService.GetResumptionHoliday(EmployeeId)
        End Using
    End Function

    Public Function SaveResumptionHoliday(ResumptionHoliday As Domain.Payroll.Entities.ResumptionHoliday, session As SessionValues) As ActionResult(Of Domain.Payroll.Entities.ResumptionHoliday) Implements IPayrollResumptionHoliday.SaveResumptionHoliday
        Using adminService As IResumptionHolidayAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of IResumptionHolidayAdminService)()
            Return adminService.SaveResumptionHoliday(ResumptionHoliday, session.AuditMessageWcf)
        End Using
    End Function

End Class
