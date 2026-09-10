'***********************************************************************
' Assembly         : Infrastructure.Data.PayrollRepository
' Author           : Oscar Stiven Astudillo
' Created          : 2025-11-12
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Infrastructure.Data.Base
Imports Domain.Payroll.Entities
Imports Domain.Payroll
Imports Infrastructure.CrossCutting.Base
Imports System.Data.Entity.Infrastructure

Public Class VacationSummaryRepository

    Inherits GenericRepository(Of Contract)
    Implements IVacationSummaryRepository

    ''' <summary>
    ''' Contexto de payroll
    ''' </summary>
    Private _context As IPayrollUnitOfWork

    ''' <summary>
    ''' Inicia el contexto de payroll
    ''' </summary>
    ''' <param name="context">Contexto</param>
    Public Sub New(ByVal context As IPayrollUnitOfWork)
        MyBase.New(context)
        _context = context
    End Sub



    ''' <summary>
    ''' Obtiene el resumen de vacaciones de un empleado en un periodo específico
    ''' </summary>
    ''' <param name="yearClosed">Año del periodo de liquidación</param>
    ''' <param name="monthClosed">Mes del periodo de liquidación</param>
    ''' <param name="employeeId">Id del empleado (nullable)</param>
    Private Function GetVacationSummary(yearClosed As Integer, monthClosed As Integer, employeeId As Integer?) As List(Of SP_SummaryVacationLiquidation_Result) Implements IVacationSummaryRepository.GetVacationSummary
        DirectCast(_context, IObjectContextAdapter).ObjectContext.CommandTimeout = 3600
        Dim dateLiquidation As Date = New Date(yearClosed, monthClosed, 1)
        Dim res = _context.SP_SummaryVacationLiquidation(dateLiquidation, employeeId).ToList()
        Return res
    End Function

End Class

