'***********************************************************************
' Assembly         : DistributedServices.Payroll
' Author           : Daniel Eduardo Arévalo
' Created          : 03-07-2015
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Payroll.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities
Imports System.Threading.Tasks

<ServiceContract()>
Public Interface IPayrollIVacationSummary


    ''' <summary>
    ''' Obtiene el resumen de vacaciones de un empleado en un periodo específico
    ''' </summary>
    ''' <param name="yearClosed">Año del periodo de liquidación</param>
    ''' <param name="monthClosed">Mes del periodo de liquidación</param>
    ''' <param name="employeeId">Id del empleado (nullable)</param>
    ''' <param name="session">Sesión del usuario</param>
    ''' <returns>Lista de SP_SummaryVacationLiquidation_Result</returns>
    <OperationContract()>
    Function GetVacationSummary(yearClosed As Integer, monthClosed As Integer, employeeId As Integer?, session As SessionValues) As List(Of SP_SummaryVacationLiquidation_Result)

End Interface
