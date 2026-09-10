'***********************************************************************
' Assembly         : DistributedServices.Payroll
' Author           : Faiber Mora
' Created          : 07-01-2015
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Payroll.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities

<ServiceContract()> _
Public Interface IPayrollVacationRequest

    ''' <summary>
    ''' Funcion para obtener las solicitudes de vacaciones de un empleado
    ''' </summary>
    ''' <param name="employeeId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <OperationContract()> _
    Function GetVacationRequestByEmployee(employeeId As Integer, session As SessionValues) As List(Of VacationRequest)

    ''' <summary>
    ''' Guarda la solicitud de vacaciones hecha por el empleado
    ''' </summary>
    ''' <param name="vacationRequest"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <OperationContract()> _
    Function PostVacationRequest(ByVal vacationRequest As VacationRequest, session As SessionValues) As Boolean

    ''' <summary>
    ''' Eliminar la solicitud de vacaciones
    ''' </summary>
    ''' <param name="vacationRequest"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <OperationContract()> _
    Function DeleteVacationRequest(ByVal vacationRequest As VacationRequest, session As SessionValues) As Boolean

End Interface
