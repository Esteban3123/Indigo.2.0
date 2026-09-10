'***********************************************************************
' Assembly         : DistributedServices.Payroll
' Author           : Faiber Mora
' Created          : 07-01-2015
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Application.Payroll
Imports Infrastructure.CrossCutting.IOC
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities
Partial Class PayrollService
    Implements IPayrollVacationRequest

    ''' <summary>
    ''' Funcion para obtener las solicitudes de vacaciones de un empleado
    ''' </summary>
    ''' <param name="employeeId"></param>
    ''' <param name="session"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetVacationRequestByEmployee(employeeId As Integer, session As SessionValues) As List(Of Domain.Payroll.Entities.VacationRequest) Implements IPayrollVacationRequest.GetVacationRequestByEmployee
        Using VacationRequestAdminService As IVacationRequestAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of IVacationRequestAdminService)()
            Return VacationRequestAdminService.GetVacationRequestByEmployee(employeeId)
        End Using
    End Function
    ''' <summary>
    ''' Guarda la solicitud de vacaciones que hace el empleado
    ''' </summary>
    ''' <param name="vacationRequest"></param>
    ''' <param name="session"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function PostVacationRequest(vacationRequest As Domain.Payroll.Entities.VacationRequest, session As Infrastructure.CrossCutting.Base.SessionValues) As Boolean Implements IPayrollVacationRequest.PostVacationRequest
        Using VacationRequestAdminService As IVacationRequestAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of IVacationRequestAdminService)()
            Return VacationRequestAdminService.PostVacationRequest(vacationRequest)
        End Using
    End Function

    ''' <summary>
    ''' Elimina la solicitud de vacaciones si el estado es Registrado
    ''' </summary>
    ''' <param name="vacationRequest"></param>
    ''' <param name="session"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function DeleteVacationRequest(vacationRequest As Domain.Payroll.Entities.VacationRequest, session As Infrastructure.CrossCutting.Base.SessionValues) As Boolean Implements IPayrollVacationRequest.DeleteVacationRequest
        Using VacationRequestAdminService As IVacationRequestAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of IVacationRequestAdminService)()
            Return VacationRequestAdminService.DeleteVacationRequest(vacationRequest)
        End Using
    End Function
End Class
