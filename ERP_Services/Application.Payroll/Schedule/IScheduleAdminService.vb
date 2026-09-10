'***********************************************************************
' Assembly         : Application.Payroll
' Author           : Cristhian Mauricio Salazar
' Created          : 05-08-2013
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Payroll.Entities
Imports Infrastructure.CrossCutting.Base
Public Interface IScheduleAdminService
    Inherits IDisposable

    ''' <summary>
    ''' Lista todos los horarios
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function ListAllSchedule() As List(Of Schedule)

    ''' <summary>
    ''' Obtiene un horario especifico
    ''' </summary>
    ''' <param name="functionalUnitCode">Codigo unidad funcional</param>
    ''' <param name="period">Peridodo del horario</param>
    ''' <returns>Horario</returns>
    ''' <remarks></remarks>
    Function GetSchedule(functionalUnitCode As String, period As String) As List(Of Schedule)

    ''' <summary>
    ''' Guarda un Horario
    ''' </summary>
    ''' <param name="schedule">Horario</param>
    ''' <param name="audit">Objeto auditoria</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function SaveSchedule(schedule As Schedule, audit As AuditMessage) As Boolean

    ''' <summary>
    ''' Elimina un horario
    ''' </summary>
    ''' <param name="schedule">Horario a eliminar</param>
    ''' <param name="audit">Objeto auditoria</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function DeleteSchedule(schedule As Schedule, audit As AuditMessage) As Boolean

    ''' <summary>
    ''' funcion para obtener todos lo turnos de un empleado en cierto rango de un periodo
    ''' </summary>
    ''' <param name="idEmployee">id del empleado</param>
    ''' <param name="period">periodo del calendario</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetScheduleByEmployeePeriod(idEmployee As Integer, period As String, diaMin As Integer, diaMax As Integer) As List(Of Schedule)
    ''' <summary>
    ''' Funcion para obtener todos los turno que hay en un periodo y que tenga plantilla en un dia especifico
    ''' </summary>
    ''' <param name="period">periodo del calendario</param>
    ''' <param name="day">dia que debe tener plantilla</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetScheduleByPeriodDay(period As String, day As Integer, functionalUnitId As Integer) As List(Of Schedule)

    ''' <summary>
    ''' funcion la cual obtiene un turno del empleado en un periodo especifico y en una unidad funcional especifica
    ''' </summary>
    ''' <param name="idEmployee">empleado</param>
    ''' <param name="period">Periodo</param>
    ''' <param name="functionalUnitId">unidad funcional</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetScheduleByEmployeePeriodFunctionalUnit(idEmployee As Integer, period As String, functionalUnitId As Integer) As Schedule

    ''' <summary>
    ''' funcion para obtener todos lo turnos de un empleado en cierto rango de un periodo y en una unidad funcional especifica
    ''' </summary>
    ''' <param name="idEmployee">id del empleado</param>
    ''' <param name="period">periodo del calendario</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetScheduleByEmployeePeriodFunctionalUnitRangeDays(idEmployee As Integer, period As String, functionalUnitId As Integer, diaMin As Integer, diaMax As Integer) As Schedule

    ''' <summary>
    ''' Funcion que retorna lista de turnos de empleado para un rango de fechas
    ''' </summary>
    ''' <param name="employeeId">Id del empleado</param>
    ''' <param name="dateInitial">Fecha Inicio</param>
    ''' <param name="dateEnd">Fecha Fin</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetScheduleDetailByEmployeeBetweenDate(employeeId As Integer, dateInitial As Date, dateEnd As Date) As List(Of ScheduleDetail)

    ''' <summary>
    ''' Funcion que elimina masivamente una lista de detalles
    ''' </summary>
    ''' <param name="listScheduleDetail">Lista de detalles de un calendario</param>
    ''' <returns>Boolean</returns>
    ''' <remarks></remarks>
    Function DeleteScheduleDetailMasive(listScheduleDetail As List(Of ScheduleDetail), audit As AuditMessage) As Boolean


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
    Function SaveScheduleDetailMasive(_scheduleDetail As ScheduleDetail, _employeesCheked As List(Of Integer), _dayToSave As List(Of Date), newFunctionalUnit As FunctionalUnit, Include_Holiday As Boolean, currentFunctionalUnit As FunctionalUnit, ScheduleDatasource As List(Of Schedule), Period As String, _List_Holidays As List(Of Domain.Entities.Holiday), audit As AuditMessage, Optional EditFlag As Boolean = False) As Domain.Base.Entities.ActionMessageResult(Of List(Of Schedule))

    ''' <summary>
    ''' Función para el análisis de Turnos del Empleado
    ''' </summary>
    ''' <param name="InitialDate"></param>
    ''' <param name="EndDate"></param>
    ''' <returns></returns>
    Function AnalisisEmployeeSchedule(InitialDate As Date, EndDate As Date, IdFunctionalUnit As Integer, IdPosition As Integer, IdEmployee As Integer) As List(Of SP_AnalisEmployeeSchedule_Result)

    Function SaveExtraHours(ListSpAnalisisSchedule As List(Of SP_AnalisEmployeeSchedule_Result)) As SP_SaveExtraHours_Result


End Interface
