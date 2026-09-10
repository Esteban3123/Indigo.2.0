'***********************************************************************
' Assembly         : Application.Payroll
' Author           : Oscar Stiven Astudillo
' Created          : 2025-11-12
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Base
Imports Domain.Payroll
Imports Domain.Payroll.Entities
Imports Infrastructure.Data.Base
Imports Infrastructure.Data.PayrollRepository

Public Class VacationSummaryAdminService
    Implements IVacationSummaryAdminService

    ''' <summary>
    ''' Repositorio de resumen de vacaciones
    ''' </summary>
    Private _vacationSummaryRepository As IVacationSummaryRepository


    'Devuelve el contexto en este repositorio 
    Private _context As IPayrollUnitOfWork

    ''' <summary>
    ''' Constructor del servicio
    ''' </summary>
    ''' <param name="vacationSummaryRepository">Repositorio de resumen de vacaciones</param>
    Public Sub New(ByVal contex As IPayrollUnitOfWork, vacationSummaryRepository As IVacationSummaryRepository)
        _context = contex
        _vacationSummaryRepository = vacationSummaryRepository
    End Sub

    ''' <summary>
    ''' Obtiene el resumen de vacaciones de un empleado en un periodo específico
    ''' </summary>
    ''' <param name="yearClosed">Año del periodo de liquidación</param>
    ''' <param name="monthClosed">Mes del periodo de liquidación</param>
    ''' <param name="employeeId">Id del empleado (nullable)</param>
    ''' <returns>Lista de SP_SummaryVacationLiquidation_Result</returns>
    Public Function GetVacationSummary(yearClosed As Integer, monthClosed As Integer, employeeId As Integer?) As List(Of SP_SummaryVacationLiquidation_Result) Implements IVacationSummaryAdminService.GetVacationSummary
        Try
            Return _vacationSummaryRepository.GetVacationSummary(yearClosed, monthClosed, employeeId)
        Catch ex As Exception
            Throw New Exception("Error al obtener el resumen de vacaciones: " & ex.Message, ex)
        End Try
    End Function

#Region "IDisposable Support"
    Private disposedValue As Boolean

    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not disposedValue Then
            _context = Nothing
        End If
        disposedValue = True
    End Sub

    Public Sub Dispose() Implements IDisposable.Dispose
        Dispose(True)
        GC.SuppressFinalize(Me)
    End Sub
#End Region

End Class

