'***********************************************************************
' Assembly         : Application.Payroll
' Author           : Carlos Mario Arias Rubiano
' Created          : 05/07/2016
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************
Imports Domain.Payroll.Entities
Imports Domain.Base.Entities
Imports Infrastructure.CrossCutting.Base

Public Interface IResumptionHolidayAdminService
    Inherits IDisposable

    ''' <summary>
    ''' Obtiene la entidad
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetResumptionHoliday(EmployeeId As Integer) As ActionResult(Of List(Of ResumptionHoliday))

    ''' <summary>
    ''' Guarda una entidad
    ''' </summary>
    ''' <returns></returns>
    Function SaveResumptionHoliday(ResumptionHoliday As ResumptionHoliday, audit As AuditMessage) As ActionResult(Of ResumptionHoliday)

End Interface
