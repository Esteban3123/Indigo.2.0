'***********************************************************************
' Assembly         : Infrastructure.Data.PayrollRepository
' Author           : Cristhian Mauricio Salazar
' Created          : 24-10-2013
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Infrastructure.Data.Base
Imports Domain.Payroll.Entities
Imports Domain.Payroll
Public Class VacationPeriodRepository
    Inherits GenericRepository(Of VacationPeriod)
    Implements IVacationPeriodRepository

    ''' <summary>
    ''' Contexto de payrrol
    ''' </summary>
    Private _context As IPayrollUnitOfWork

    ''' <summary>
    ''' Inicia el contexto de payrrol
    ''' </summary>
    ''' <param name="context">Contexto</param>
    ''' <remarks></remarks>
    Public Sub New(ByVal context As IPayrollUnitOfWork)
        MyBase.New(context)
        _context = context
    End Sub

    ''' <summary>
    ''' Obtiene las vacaciones que tenga solicitadas o pagas un empleado
    ''' </summary>
    ''' <param name="employeeId">Id del empleado</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetVacationPeriodByEmployee(employeeId As Integer) As List(Of VacationPeriod) Implements IVacationPeriodRepository.GetVacationPeriodByEmployee
        Dim query = From e In _context.VacationPeriod
                    Where e.EmployeeId = employeeId
                    Select e
        Return query.ToList()
    End Function

    ''' <summary>
    ''' Obtiene las vacaciones que tenga solicitadas o pagas un empleado
    ''' </summary>
    ''' <param name="employeeId">Id del empleado</param>
    ''' <returns>Lista de periodos de vacaciones</returns>
    ''' <remarks></remarks>
    Public Function GetVacationPeriodWithDetailByEmployee(employeeId As Integer) As List(Of VacationPeriod) Implements IVacationPeriodRepository.GetVacationPeriodWithDetailByEmployee
        Dim query = From e In _context.VacationPeriod.Include("Vacation").Include("Contract")
                    Where e.EmployeeId = employeeId And e.Contract.Status <> 2
                    Select e

        Dim r = query.ToList

        Return r

    End Function

End Class
