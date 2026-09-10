'***********************************************************************
' Assembly         : DistributedServices.Payroll
' Author           : Carlos Mario Arias Rubiano
' Created          : 04/05/2017
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Application.Payroll
Imports Infrastructure.CrossCutting.IOC
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities
Imports Domain.Payroll.Entities
Imports Domain.Entities

Partial Class PayrollService

    ''' <summary>
    ''' Importa el archivo excel y valida la info
    ''' </summary>
    ''' <param name="data"></param>
    ''' <returns></returns>
    Public Function SP_ImportFileInitialBalancePayroll(data As List(Of ImportFileRow), session As SessionValues) As ActionResult(Of List(Of SP_ImportFileInitialBalancePayroll_Result)) Implements IPayrollInitialBalancePayroll.SP_ImportFileInitialBalancePayroll
        Using adminService As IInitialBalancePayrollAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of IInitialBalancePayrollAdminService)()
            Return adminService.SP_ImportFileInitialBalancePayroll(data)
        End Using
    End Function

    Public Function SP_SaveInitialBalancePayroll(ListInfo As List(Of SP_ImportFileInitialBalancePayroll_Result), session As SessionValues) As ActionResult(Of List(Of Tuple(Of String, Integer))) Implements IPayrollInitialBalancePayroll.SP_SaveInitialBalancePayroll
        Using adminService As IInitialBalancePayrollAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of IInitialBalancePayrollAdminService)()
            Return adminService.SP_SaveInitialBalancePayroll(ListInfo, session.AuditMessageWcf)
        End Using
    End Function

End Class
