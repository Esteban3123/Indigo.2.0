'***********************************************************************
' Assembly         : DistributedServices.Payroll
' Author           : Daniel Eduardo Arévalo Bonilla
' Created          : 11-10-2018
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

    Implements IPayrollFileForeclousure

    Public Function GenerateFileForeclousure(PayrollDateLiquidated As Date, CompanyId As Integer, session As SessionValues) As ActionMessageResult(Of StringBuilder) Implements IPayrollFileForeclousure.GenerateFileForeclousure
        Using FileForeclousureAdminService As IFileForeclousureAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of IFileForeclousureAdminService)()
            Return FileForeclousureAdminService.GenerateFileForeclousure(PayrollDateLiquidated, CompanyId, session)
        End Using
    End Function
End Class
