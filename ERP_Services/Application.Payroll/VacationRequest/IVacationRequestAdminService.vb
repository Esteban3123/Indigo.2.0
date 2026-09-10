'***********************************************************************
' Assembly         : Application.Payroll
' Author           : Faiber Mora
' Created          : 07-01-2015
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************
Imports Domain.Payroll.Entities
Imports Domain.Base.Entities
Imports Infrastructure.CrossCutting.Base
Public Interface IVacationRequestAdminService
    Inherits IDisposable

    ''' <summary>
    ''' Funcion para obtener las solicitudes de vacaciones de un empleado
    ''' </summary>
    ''' <param name="employeeId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetVacationRequestByEmployee(employeeId As Integer) As List(Of VacationRequest)

    ''' <summary>
    ''' Guarda la solicitud de vacaciones hecha por el empleado
    ''' </summary>
    ''' <param name="vacationRequest"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function PostVacationRequest(ByVal vacationRequest As VacationRequest)

    ''' <summary>
    ''' Elimina la solicitud, solo si esta en estado registrado (Status=1)
    ''' </summary>
    ''' <param name="vacationRequest"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function DeleteVacationRequest(ByVal vacationRequest As VacationRequest)


End Interface
