'***********************************************************************
' Assembly         : Domain.Payroll
' Author           : Faiber Mora
' Created          : 07/01/2015
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************
Imports Domain.Base
Imports Domain.Payroll.Entities
Public Interface IVacationRequestRepository
    Inherits IRepository(Of VacationRequest)

    ''' <summary>
    ''' Funcion para obtener las solicitudes de vacaciones de un empleado
    ''' </summary>
    ''' <param name="employeeId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetVacationRequestByEmployee(employeeId As Integer) As List(Of VacationRequest)

End Interface
