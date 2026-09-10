'***********************************************************************
' Assembly         : Domain.Payroll
' Author           : Cristhian Mauricio Salazar
' Created          : 24-10-2013
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************
Imports Domain.Base
Imports Domain.Payroll.Entities
Public Interface IVacationPeriodRepository
    Inherits IRepository(Of VacationPeriod)

    ''' <summary>
    ''' Obtiene las vacaciones que tenga solicitadas o pagas un empleado
    ''' </summary>
    ''' <param name="employeeId">Id del empleado</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetVacationPeriodByEmployee(employeeId As Integer) As List(Of VacationPeriod)


    ''' <summary>
    ''' Obtiene las vacaciones que tenga solicitadas o pagas un empleado
    ''' </summary>
    ''' <param name="employeeId">Id del empleado</param>
    ''' <returns>lista de periodos de vacaciones</returns>
    ''' <remarks></remarks>
    Function GetVacationPeriodWithDetailByEmployee(employeeId As Integer) As List(Of VacationPeriod)

End Interface
