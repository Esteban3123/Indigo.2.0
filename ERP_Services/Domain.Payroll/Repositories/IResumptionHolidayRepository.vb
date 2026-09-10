'***********************************************************************
' Assembly         : Domain.Payroll
' Author           : Carlos Mario Arias Rubiano
' Created          : 05/07/2016
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Base
Imports Domain.Payroll.Entities
Public Interface IResumptionHolidayRepository
    Inherits IRepository(Of ResumptionHoliday)

    ''' <summary>
    ''' Obtiene la entidad
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetResumptionHoliday(EmployeeId As Integer) As List(Of ResumptionHoliday)

    ''' <summary>
    ''' Obtiene las vacaciones del empleado
    ''' </summary>
    ''' <param name="EmployeeId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetVacationsOfEmployee(EmployeeId As Integer) As List(Of Vacation)

End Interface
