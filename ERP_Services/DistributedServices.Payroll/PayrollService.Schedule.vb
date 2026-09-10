'***********************************************************************
' Assembly         : DistributedServices.Payroll
' Author           : Cristhian Mauricio Salazar
' Created          : 06-08-2013
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Application.Payroll
Imports Infrastructure.CrossCutting.IOC
Imports Domain.Payroll.Entities
Imports Infrastructure.CrossCutting.Base

Partial Class PayrollService

    ''' <summary>
    ''' Elimina un horario
    ''' </summary>
    ''' <param name="schedule">Horario a eliminar</param>
    ''' <param name="audit">Objeto auditoria</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function DeleteSchedule(schedule As Domain.Payroll.Entities.Schedule, session As SessionValues) As Boolean Implements IPayrollSchedule.DeleteSchedule
        Using scheduleAdmin As IScheduleAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of IScheduleAdminService)()
            Return scheduleAdmin.DeleteSchedule(schedule, session.AuditMessageWcf)
        End Using
    End Function

    ''' <summary>
    ''' Obtiene un horario especifico
    ''' </summary>
    ''' <param name="functionalUnitId">Codigo unidad funcional</param>
    ''' <param name="period">Peridodo del horario</param>
    ''' <returns>Horario</returns>
    ''' <remarks></remarks>
    Public Function GetSchedule(functionalUnitId As String, period As String, session As SessionValues) As List(Of Domain.Payroll.Entities.Schedule) Implements IPayrollSchedule.GetSchedule
        Using scheduleAdmin As IScheduleAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of IScheduleAdminService)()
            Return scheduleAdmin.GetSchedule(functionalUnitId, period)
        End Using
    End Function

    ''' <summary>
    ''' Lista todos los horarios
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListAllSchedule(session As SessionValues) As List(Of Domain.Payroll.Entities.Schedule) Implements IPayrollSchedule.ListAllSchedule
        Using scheduleAdmin As IScheduleAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of IScheduleAdminService)()
            Return scheduleAdmin.ListAllSchedule()
        End Using
    End Function

    ''' <summary>
    ''' Guarda un Horario
    ''' </summary>
    ''' <param name="schedule">Horario</param>
    ''' <param name="audit">Objeto auditoria</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SaveSchedule(schedule As Domain.Payroll.Entities.Schedule, session As SessionValues) As Boolean Implements IPayrollSchedule.SaveSchedule
        Using scheduleAdmin As IScheduleAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of IScheduleAdminService)()
            Return scheduleAdmin.SaveSchedule(schedule, session.AuditMessageWcf)
        End Using
    End Function

    ''' <summary>
    ''' funcion para obtener todos lo turnos de un empleado en cierto rango de un periodo
    ''' </summary>
    ''' <param name="idEmployee">id del empleado</param>
    ''' <param name="period">periodo del calendario</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetScheduleByEmployeePeriod(idEmployee As Integer, period As String, diaMin As Integer, diaMax As Integer, session As SessionValues) As List(Of Domain.Payroll.Entities.Schedule) Implements IPayrollSchedule.GetScheduleByEmployeePeriod
        Using scheduleAdmin As IScheduleAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of IScheduleAdminService)()
            Return scheduleAdmin.GetScheduleByEmployeePeriod(idEmployee, period, diaMin, diaMax)
        End Using
    End Function

    ''' <summary>
    ''' Funcion para obtener todos los turno que hay en un periodo y que tenga plantilla en un dia especifico
    ''' </summary>
    ''' <param name="period">periodo del calendario</param>
    ''' <param name="day">dia que debe tener plantilla</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetScheduleByPeriodDay(period As String, day As Integer, functionalUnitId As Integer, session As SessionValues) As List(Of Domain.Payroll.Entities.Schedule) Implements IPayrollSchedule.GetScheduleByPeriodDay
        Using scheduleAdmin As IScheduleAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of IScheduleAdminService)()
            Return scheduleAdmin.GetScheduleByPeriodDay(period, day, functionalUnitId)
        End Using
    End Function

    ''' <summary>
    ''' funcion la cual obtiene un turno del empleado en un periodo especifico y en una unidad funcional especifica
    ''' </summary>
    ''' <param name="idEmployee">empleado</param>
    ''' <param name="period">Periodo</param>
    ''' <param name="functionalUnitId">unidad funcional</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetScheduleByEmployeePeriodFunctionalUnit(idEmployee As Integer, period As String, functionalUnitId As Integer, session As SessionValues) As Domain.Payroll.Entities.Schedule Implements IPayrollSchedule.GetScheduleByEmployeePeriodFunctionalUnit
        Using scheduleAdmin As IScheduleAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of IScheduleAdminService)()
            Return scheduleAdmin.GetScheduleByEmployeePeriodFunctionalUnit(idEmployee, period, functionalUnitId)
        End Using
    End Function

    ''' <summary>
    ''' funcion para obtener todos lo turnos de un empleado en cierto rango de un periodo y en una unidad funcional especifica
    ''' </summary>
    ''' <param name="idEmployee">id del empleado</param>
    ''' <param name="period">periodo del calendario</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetScheduleByEmployeePeriodFunctionalUnitRangeDays(idEmployee As Integer, period As String, functionalUnitId As Integer, diaMin As Integer, diaMax As Integer, session As SessionValues) As Domain.Payroll.Entities.Schedule Implements IPayrollSchedule.GetScheduleByEmployeePeriodFunctionalUnitRangeDays
        Using scheduleAdmin As IScheduleAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of IScheduleAdminService)()
            Return scheduleAdmin.GetScheduleByEmployeePeriodFunctionalUnitRangeDays(idEmployee, period, functionalUnitId, diaMin, diaMax)
        End Using
    End Function

    Public Function GetScheduleDetailByEmployeeBetweenDate(employeeId As Integer, dateInitial As Date, dateEnd As Date, session As SessionValues) As List(Of Domain.Payroll.Entities.ScheduleDetail) Implements IPayrollSchedule.GetScheduleDetailByEmployeeBetweenDate
        Using scheduleAdmin As IScheduleAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of IScheduleAdminService)()
            Return scheduleAdmin.GetScheduleDetailByEmployeeBetweenDate(employeeId, dateInitial, dateEnd)
        End Using
    End Function

    ''' <summary>
    ''' Funcion que elimina masivamente una lista de detalles
    ''' </summary>
    ''' <param name="listScheduleDetail">Lista de detalles de un calendario</param>
    ''' <returns>Boolean</returns>
    ''' <remarks></remarks>
    Function DeleteScheduleDetailMasive(listScheduleDetail As List(Of ScheduleDetail), session As SessionValues) As Boolean Implements IPayrollSchedule.DeleteScheduleDetailMasive
        Using scheduleAdmin As IScheduleAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of IScheduleAdminService)()
            Return scheduleAdmin.DeleteScheduleDetailMasive(listScheduleDetail, session.AuditMessageWcf)
        End Using
    End Function

    ''' <summary>
    ''' Guarda varios schedule a varios empleados de diferentes dias
    ''' </summary>
    ''' <param name="_scheduleDetail">El schedule detail (turno) que se replicará</param>
    ''' <param name="_employeesCheked">Listado de Id's de los empleados que se les registrara el turno</param>
    ''' <param name="_dayToSave">Listado de días que se van a registrar</param>
    ''' <param name="newFunctionalUnit">Objeto tipo FunctionalUnit, para saber si se registrar en una unidad funcional diferente</param>
    ''' <param name="Include_Holiday">Indica si se registran los dias feriados</param>
    ''' <param name="currentFunctionalUnit">Objeto que contiene la unidad funcional actual</param>
    ''' <param name="ScheduleDatasource">Listado de schedule que tiene el datasource actual</param>
    ''' <param name="Period">Periodo del schedule inicial en el formulario</param>
    ''' <param name="_List_Holidays">Listado de festivos para ese periodo</param>
    ''' <returns>Si se realizo o no la operación</returns>
    ''' <remarks></remarks>
    Public Function SaveScheduleDetailMasive(_scheduleDetail As ScheduleDetail, _employeesCheked As List(Of Integer), _dayToSave As List(Of Date), newFunctionalUnit As FunctionalUnit, Include_Holiday As Boolean, currentFunctionalUnit As FunctionalUnit, ScheduleDatasource As List(Of Schedule), Period As String, _List_Holidays As List(Of Domain.Entities.Holiday), session As SessionValues, Optional EditFlag As Boolean = False) As Domain.Base.Entities.ActionMessageResult(Of List(Of Schedule)) Implements IPayrollSchedule.SaveScheduleDetailMasive
        Using scheduleAdmin As IScheduleAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of IScheduleAdminService)()
            Return scheduleAdmin.SaveScheduleDetailMasive(_scheduleDetail, _employeesCheked, _dayToSave, newFunctionalUnit, Include_Holiday, currentFunctionalUnit, ScheduleDatasource, Period, _List_Holidays, session.AuditMessageWcf, EditFlag)
        End Using
    End Function

    Public Function AnalasisScheduleEmployee(InitialDate As Date, EndDate As Date, IdFunctionalUnit As Integer, IdPosition As Integer, IdEmployee As Integer, session As SessionValues) As List(Of SP_AnalisEmployeeSchedule_Result) Implements IPayrollSchedule.AnalisisEmployeeSchedule
        Using scheduleAdmin As IScheduleAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of IScheduleAdminService)()
            Return scheduleAdmin.AnalisisEmployeeSchedule(InitialDate, EndDate, IdFunctionalUnit, IdPosition, IdEmployee)
        End Using
    End Function

    Public Function SaveExtraHours(ObjListAnalisisSchedule As List(Of SP_AnalisEmployeeSchedule_Result), session As SessionValues) As SP_SaveExtraHours_Result Implements IPayrollSchedule.SaveExtraHours
        Using scheduleAdmin As IScheduleAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of IScheduleAdminService)()
            Return scheduleAdmin.SaveExtraHours(ObjListAnalisisSchedule)
        End Using
    End Function
End Class
