'***********************************************************************
' Assembly         : DistributedServices.Payroll
' Author           : Daniel Eduardo Arévalo Bonilla
' Created          : 16-08-2017
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Base.Entities
Imports Application.Payroll
Imports Infrastructure.CrossCutting.IOC
Imports Infrastructure.CrossCutting.Base
Imports System.Text
Imports Domain.Payroll.Entities
Imports DistributedServices.Payroll

Partial Class PayrollService

    Implements IPayrollBlockSchedule

    Public Function ListAllBlockSchedule(session As SessionValues) As Tuple(Of BlockScheduleC, List(Of BlockSchedule)) Implements IPayrollBlockSchedule.ListAllBlockSchedule
        Using BlockScheduleAdminService As IBlockScheduleAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of IBlockScheduleAdminService)()
            Return BlockScheduleAdminService.ListAllBlockSchedule()
        End Using
    End Function

    Public Function SaveBlockSchedule(ByVal BlockScheduleC As BlockScheduleC, ByVal ListBlockSchedule As List(Of BlockSchedule), session As SessionValues) As ActionResult(Of Tuple(Of BlockScheduleC, List(Of BlockSchedule))) Implements IPayrollBlockSchedule.SaveBlockSchedule
        Using BlockScheduleAdminService As IBlockScheduleAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of IBlockScheduleAdminService)()
            Return BlockScheduleAdminService.SaveBlockSchedule(BlockScheduleC, ListBlockSchedule, session.AuditMessageWcf)
        End Using
    End Function
End Class
