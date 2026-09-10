'***********************************************************************
' Assembly         : Infrastructure.Data.PayrollRepository
' Author           : Carlos Mario Arias Rubiano
' Created          : 05/07/2016
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Infrastructure.Data.Base
Imports Domain.Payroll.Entities
Imports Domain.Payroll
Public Class ResumptionHolidayRepository
    Inherits GenericRepository(Of ResumptionHoliday)
    Implements IResumptionHolidayRepository

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
    ''' Obtiene la entidad
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetResumptionHoliday(EmployeeId As Integer) As List(Of ResumptionHoliday) Implements IResumptionHolidayRepository.GetResumptionHoliday
        If EmployeeId = 0 Then
            Throw New ArgumentNullException("EmployeeId")
        End If
        Dim res = (From d In Me._context.ResumptionHoliday Where d.EmployeeId = EmployeeId Select d).ToList
        If res IsNot Nothing AndAlso res.Count > 0 Then
            Return res
        Else
            Return Nothing
        End If
    End Function

    ''' <summary>
    ''' Obtiene las vacaciones del empleado
    ''' </summary>
    ''' <param name="EmployeeId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetVacationsOfEmployee(EmployeeId As Integer) As List(Of Vacation) Implements IResumptionHolidayRepository.GetVacationsOfEmployee
        If EmployeeId = 0 Then
            Throw New ArgumentNullException("EmployeeId")
        End If
        Return (From vp In _context.VacationPeriod
                   Join v In _context.Vacation On v.VacationPeriodId Equals vp.Id
                   Where vp.EmployeeId = EmployeeId
                   Select v).ToList
    End Function

End Class
