'***********************************************************************
' Assembly         : DistributedServices.Payroll
' Author           : Cristhian Mauricio Salazar
' Created          : 24-10-2013
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Application.Payroll
Imports Infrastructure.CrossCutting.IOC
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities

Partial Class PayrollService
    Implements IPayrollVacationPeriod

    ''' <summary>
    ''' Calcula las fechas fin e ingreso de vacaciones
    ''' </summary>
    ''' <param name="EmployeeId"></param>
    ''' <param name="RequestDay"></param>
    ''' <param name="InitialDate"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function CalculateDatesResumption(EmployeeId As Integer, RequestDay As Integer, InitialDate As Date, session As SessionValues) As Domain.Base.Entities.ActionResult(Of Tuple(Of Date, Date)) Implements IPayrollVacationPeriod.CalculateDatesResumption
        Using VacationAdminService As IVacationPeriodAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of IVacationPeriodAdminService)()
            Return VacationAdminService.CalculateDatesResumption(EmployeeId, RequestDay, InitialDate)
        End Using
    End Function

    ''' <summary>
    ''' Elimina una vacacion
    ''' </summary>
    ''' <param name="vacation">Vacacion</param>
    ''' <returns></returns>
    Public Function DeleteVacationPeriod(vacation As Domain.Payroll.Entities.VacationPeriod, session As SessionValues) As Boolean Implements IPayrollVacationPeriod.DeleteVacationPeriod
        Using VacationAdminService As IVacationPeriodAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of IVacationPeriodAdminService)()
            Return VacationAdminService.DeleteVacationPeriod(vacation, session.AuditMessageWcf)
        End Using
    End Function

    ''' <summary>
    ''' Obtiene las vacaciones que tenga solicitadas o pagas un empleado
    ''' </summary>
    ''' <param name="employeeId">Id del empleado</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetVacationPeriodByEmployee(employeeId As Integer, session As SessionValues) As List(Of Domain.Payroll.Entities.VacationPeriod) Implements IPayrollVacationPeriod.GetVacationPeriodByEmployee
        Using VacationAdminService As IVacationPeriodAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of IVacationPeriodAdminService)()
            Return VacationAdminService.GetVacationPeriodByEmployee(employeeId)
        End Using
    End Function

    ''' <summary>
    ''' Guarda o edita una vacacion
    ''' </summary>
    ''' <param name="vacation">vacacion</param>
    ''' <returns></returns>
    Public Function SaveVacationPeriod(vacation As Domain.Payroll.Entities.VacationPeriod, session As SessionValues) As Boolean Implements IPayrollVacationPeriod.SaveVacationPeriod
        Using VacationAdminService As IVacationPeriodAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of IVacationPeriodAdminService)()
            Return VacationAdminService.SaveVacationPeriod(vacation, session.AuditMessageWcf)
        End Using
    End Function

    ''' <summary>
    ''' Lista todas la vacaciones que tiene el empleado y le agrega los periodos que se le debe
    ''' </summary>
    ''' <param name="choice">opcion de filtro 1 - Empleado 2 - Grupo</param>
    ''' <param name="value">Valor del filtro</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListVacationPeriodByFilter(choice As Byte, value As String, session As SessionValues) As List(Of Domain.Payroll.Entities.Employee) Implements IPayrollVacationPeriod.ListVacationPeriodByFilter
        Using vacationAdmin As IVacationPeriodAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of IVacationPeriodAdminService)()
            Return vacationAdmin.ListVacationPeriodByFilter(choice, value)
        End Using
    End Function

    ''' <summary>
    ''' Funcion para solicitar las vacaciones de uno o varios empleados
    ''' </summary>
    ''' <param name="employees">Lista de empleados</param>
    ''' <param name="requestDays">Dias Solicitados</param>
    ''' <param name="typeCalculate">Tipo de calculo 1 - Promedio 2 - Sueldo Basico</param>
    ''' <param name="typeVacation">Tipo de vacaciones 1 - Liquuidar 2 - Disfrutar</param>
    ''' <param name="typePayment">Tipo de pago  1 - Inmedito 2 - Proxima Nomina</param>
    ''' <param name="initialDateVacation">Fecha Inicial Vacaciones</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function RequestVacationEmployees(employees As List(Of Domain.Payroll.Entities.Employee), requestDays As Integer, typeCalculate As Byte, typeVacation As Byte, typePayment As Nullable(Of Byte), initialDateVacation As Date, session As SessionValues) As Domain.Base.Entities.ActionMessageResult(Of List(Of Domain.Payroll.Entities.Employee)) Implements IPayrollVacationPeriod.RequestVacationEmployees
        Using vacationAdmin As IVacationPeriodAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of IVacationPeriodAdminService)()
            Return vacationAdmin.RequestVacationEmployees(employees, requestDays, typeCalculate, typeVacation, typePayment, initialDateVacation, session)
        End Using
    End Function

    ''' <summary>
    ''' Guarda o edita una vacacion desde el agregado de empleado
    ''' </summary>
    ''' <param name="listEmployee">Lista de empleados</param>
    ''' <param name="typeVacation">Tipo de vacacion 1 liquidar 2 Disfrutar</param>
    ''' <param name="audit">Auditoria</param>
    ''' <returns>boolean</returns>
    ''' <remarks></remarks>
    Public Function SaveVacationEmployee(listEmployee As List(Of Domain.Payroll.Entities.Employee), typeVacation As Byte, initialDate As Nullable(Of Date), endDate As Nullable(Of Date), session As SessionValues) As ActionMessageResult Implements IPayrollVacationPeriod.SaveVacationEmployee
        Using vacationAdmin As IVacationPeriodAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of IVacationPeriodAdminService)()
            Return vacationAdmin.SaveVacationEmployee(listEmployee, typeVacation, initialDate, endDate, session.AuditMessageWcf)
        End Using
    End Function

    ''' <summary>
    ''' Cancelar la solicitud de vacaciones que aun no esta pagada
    ''' </summary>
    ''' <param name="vacation"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function CancelRequest(vacation As Domain.Payroll.Entities.Vacation, session As SessionValues) As Domain.Base.Entities.ActionMessageResult(Of Domain.Payroll.Entities.Vacation) Implements IPayrollVacationPeriod.CancelRequest
        Using vacationAdmin As IVacationPeriodAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of IVacationPeriodAdminService)()
            Return vacationAdmin.CancelRequest(vacation, session.AuditMessageWcf)
        End Using
    End Function

    ''' <summary>
    ''' Funcion para el realizar el ingreso forzoso
    ''' </summary>
    ''' <param name="vacation">Solicitud de vacacion</param>
    ''' <param name="dateEntry">Fecha ingreso</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ForceEntry(vacation As Domain.Payroll.Entities.Vacation, dateEntry As Date, session As SessionValues) As Domain.Base.Entities.ActionMessageResult(Of Domain.Payroll.Entities.Vacation) Implements IPayrollVacationPeriod.ForceEntry
        Using vacationAdmin As IVacationPeriodAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of IVacationPeriodAdminService)()
            Return vacationAdmin.ForceEntry(vacation, dateEntry, session.AuditMessageWcf)
        End Using
    End Function

    ''' <summary>
    ''' Funcion la cual obtiene las vacaciones de una fecha de liquidacion especifica
    ''' </summary>
    ''' <param name="dateLiquidation">Fecha de liquidacion de nomina</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetVacationLiquidationDate(dateLiquidation As Date, state As Byte, session As SessionValues) As List(Of Domain.Payroll.Entities.Vacation) Implements IPayrollVacationPeriod.GetVacationLiquidationDate
        Using vacationAdmin As IVacationPeriodAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of IVacationPeriodAdminService)()
            Return vacationAdmin.GetVacationLiquidationDate(dateLiquidation, state, session.AuditMessageWcf)
        End Using
    End Function

    ''' <summary>
    ''' Funcion para listar todas las vacaciones de tengan cierto estado de incorporacion
    ''' </summary>
    ''' <param name="stateIncorporation">Estado de incorporacion</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetVacationStateIncorporation(stateIncorporation As Byte, session As SessionValues) As List(Of Domain.Payroll.Entities.Vacation) Implements IPayrollVacationPeriod.GetVacationStateIncorporation
        Using vacationAdmin As IVacationPeriodAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of IVacationPeriodAdminService)()
            Return vacationAdmin.GetVacationStateIncorporation(stateIncorporation)
        End Using
    End Function

    ''' <summary>
    ''' Obtiene los detalles de calendario de varios empleados en un rango de fechas
    ''' </summary>
    ''' <param name="listIdEmpleoyee">Lista de ids de empleados</param>
    ''' <param name="dateInitial">Fecha Inicial</param>
    ''' <param name="dateEnd">Fecha Final</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetScheduleDetailByListEmployeeBetweenDate(listIdEmpleoyee As List(Of Integer), dateInitial As Date, dateEnd As Date, session As SessionValues) As List(Of Domain.Payroll.Entities.ScheduleDetail) Implements IPayrollVacationPeriod.GetScheduleDetailByListEmployeeBetweenDate
        Using vacationAdmin As IVacationPeriodAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of IVacationPeriodAdminService)()
            Return vacationAdmin.GetScheduleDetailByListEmployeeBetweenDate(listIdEmpleoyee, dateInitial, dateEnd)
        End Using
    End Function

    ''' <summary>
    ''' Obtiene la lista de novedades que estan dentro de un rango de fechas a excepcion de la que se envia como parametro
    ''' </summary>
    ''' <param name="listEmployeeId">Lista de id de empleados</param>
    ''' <param name="initialDate">Fecha de inicio</param>
    ''' <param name="endDate">Fecha Fin</param>
    ''' <returns>Lista de novedades</returns>
    ''' <remarks></remarks>
    Public Function GetNoveltyByListIdEmployeeBetweenDate(listEmployeeId As List(Of Integer), initialDate As Date, endDate As Date, session As SessionValues) As List(Of Domain.Payroll.Entities.Novelty) Implements IPayrollVacationPeriod.GetNoveltyByListIdEmployeeBetweenDate
        Using vacationAdmin As IVacationPeriodAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of IVacationPeriodAdminService)()
            Return vacationAdmin.GetNoveltyByListIdEmployeeBetweenDate(listEmployeeId, initialDate, endDate)
        End Using
    End Function

    ''' <summary>
    ''' Funcion para listar las vaciones que estan en cierto rango de fechas
    ''' </summary>
    ''' <param name="initialDate">Fecha Inicial</param>
    ''' <param name="endDate">Fecha Final</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetVacationBetweenDate(employeeId As Integer, initialDate As Date, endDate As Date, session As SessionValues) As List(Of Domain.Payroll.Entities.Vacation) Implements IPayrollVacationPeriod.GetVacationBetweenDate
        Using vacationAdmin As IVacationPeriodAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of IVacationPeriodAdminService)()
            Return vacationAdmin.GetVacationBetweenDate(employeeId, initialDate, endDate)
        End Using
    End Function

    Public Function GetVacationPeriodWithDetailByEmployee(employeeId As Integer, session As Infrastructure.CrossCutting.Base.SessionValues) As List(Of Domain.Payroll.Entities.VacationPeriod) Implements IPayrollVacationPeriod.GetVacationPeriodWithDetailByEmployee
        Using vacationAdmin As IVacationPeriodAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of IVacationPeriodAdminService)()
            Return vacationAdmin.GetVacationPeriodWithDetailByEmployee(employeeId)
        End Using
    End Function

    Public Function IsValidDay(listHoliday As List(Of Domain.Entities.Holiday), saturdayBusinessDay As Boolean, sundayBusinessDay As Boolean, dateValidation As Date, session As Infrastructure.CrossCutting.Base.SessionValues) As Boolean Implements IPayrollVacationPeriod.IsValidDay
        Using vacationAdmin As IVacationPeriodAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of IVacationPeriodAdminService)()
            Return vacationAdmin.IsValidDay(listHoliday, saturdayBusinessDay, sundayBusinessDay, dateValidation)
        End Using
    End Function

    ''' <summary>
    ''' Función para Confirmar las Vacaciones de Tipo Pago Inmediato. Genera Comprobante Contable y Comprobante de Egreso
    ''' </summary>
    ''' <param name="listVacation"> Lista de Vacaciones</param>
    ''' <param name="audit"></param>
    ''' <returns>ActionResult</returns>
    Public Function ConfirmVacation(listVacation As List(Of Domain.Payroll.Entities.Vacation), session As Infrastructure.CrossCutting.Base.SessionValues) As ActionResult Implements IPayrollVacationPeriod.ConfirmVacation
        Using vacationAdmin As IVacationPeriodAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of IVacationPeriodAdminService)()
            Return vacationAdmin.ConfirmVacation(listVacation, session)
        End Using
    End Function

End Class
