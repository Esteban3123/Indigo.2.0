'***********************************************************************
' Assembly         : Application.Payroll
' Author           : Oscar Stiven Astudillo
' Created          : 2025-11-12
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Payroll.Entities

Public Interface IVacationSummaryAdminService
    Inherits IDisposable

    ''' <summary>
    ''' Obtiene el resumen de vacaciones de un empleado en un periodo específico
    ''' </summary>
    ''' <param name="yearClosed">Año del periodo de liquidación</param>
    ''' <param name="monthClosed">Mes del periodo de liquidación</param>
    ''' <param name="employeeId">Id del empleado (nullable)</param>
    ''' <returns>Lista de SP_SummaryVacationLiquidation_Result</returns>
    Function GetVacationSummary(yearClosed As Integer, monthClosed As Integer, employeeId As Integer?) As List(Of SP_SummaryVacationLiquidation_Result)

End Interface

