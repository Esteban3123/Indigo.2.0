'***********************************************************************
' Assembly         : DistributedServices.Payroll
' Author           : Daniel Eduardo Arévalo
' Created          : 03-07-2015
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************


Imports Application.Payroll
Imports Infrastructure.CrossCutting.IOC
Imports Infrastructure.CrossCutting.Base
Imports Domain.Payroll.Entities
Imports Domain.Base.Entities
Imports System.Threading.Tasks

Partial Class PayrollService

    ''' <summary>
    ''' Obtiene el resumen de vacaciones de un empleado en un periodo específico
    ''' </summary>
    ''' <param name="yearClosed">Año del periodo de liquidación</param>
    ''' <param name="monthClosed">Mes del periodo de liquidación</param>
    ''' <param name="employeeId">Id del empleado (nullable)</param>
    ''' <param name="session">Sesión del usuario</param>
    ''' <returns>Lista de SP_SummaryVacationLiquidation_Result</returns>
    Public Function GetVacationSummary(yearClosed As Integer, monthClosed As Integer, employeeId As Integer?, session As SessionValues) As List(Of Domain.Payroll.Entities.SP_SummaryVacationLiquidation_Result) Implements IPayrollIVacationSummary.GetVacationSummary
        Using vacationSummaryRepo As IVacationSummaryAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of IVacationSummaryAdminService)()
            Return vacationSummaryRepo.GetVacationSummary(yearClosed, monthClosed, employeeId)
        End Using
    End Function

End Class
