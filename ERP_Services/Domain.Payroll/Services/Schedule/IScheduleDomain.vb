Imports Domain.Payroll.Entities
Imports Domain.Base.Entities
Imports Infrastructure.CrossCutting.Base

'***********************************************************************
' Assembly         : Application.Payroll
' Author           : Kevin Garay Rodriguez
' Created          : 17-09-2013
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Public Interface IScheduleDomain
    Inherits IDisposable

    ''' <summary>
    ''' Agrupa los detalles por empleado y los devuelve en un diccionario
    ''' </summary>
    ''' <param name="listDetail"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GroupScheduleDetailByEmployee(listDetail As List(Of ScheduleDetail)) As Dictionary(Of Integer, List(Of ScheduleDetail))

    ''' <summary>
    ''' Marca como eliminados los detalles y actualiza las horas del schedule
    ''' </summary>
    ''' <param name="schedule"></param>
    ''' <param name="listDetail"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function DeleteScheduleDetail(schedule As Schedule, listDetail As List(Of ScheduleDetail))

    ''' <summary>
    ''' Funcion para validar un nuevo registro de turno(Schedule Detail) que se va a ingresar
    ''' </summary>
    ''' <param name="_schedule">El schedule al que se le registrara el schedule detail</param>
    ''' <param name="day">el dia a regsitrar</param>
    ''' <param name="Include_Holidays">Indica si se tienen en cuenta los domingos y feriados</param>
    ''' <param name="_validContract">El contrato valido</param>
    ''' <param name="detHours">listado de detalle de horas que tiene el turno</param>
    ''' <param name="ScheduleInPeriod">Listado de scheduledetails que tenga el empleado</param>
    ''' <param name="List_Holiday">Listado de festivos para este periodo</param>
    ''' <returns>El messege result</returns>
    ''' <remarks></remarks>
    Function ValidateScheduleDetailInSchedule(ByVal _schedule As Schedule, ByVal day As Date, ByVal Include_Holidays As Boolean, _validContract As Domain.Payroll.Entities.Contract, ByVal detHours As Domain.Base.Entities.TrackableCollection(Of ScheduleDetailHour), ScheduleInPeriod As List(Of ScheduleDetail), List_Holiday As List(Of Domain.Entities.Holiday), audit As AuditMessage, Optional EditFlag As Boolean = False) As Domain.Base.Entities.MessageResult

    ''' <summary>
    ''' Funcion que retorna un schedule detail armado, cuando se quieren cortar por ser eventos
    ''' </summary>
    ''' <param name="detailHours">detalle de horas general</param>
    ''' <param name="DtInitial">Fecha Hora, Inicial</param>
    ''' <param name="DtEnding">Fecha Hora, Fin</param>
    ''' <param name="_List_Holidays">Listado de festivos para este periodo</param>
    ''' <returns>El schedule detail hora</returns>
    Function GetSchDetHourInEvents(detailHours As ScheduleDetailHour, DtInitial As DateTime, DtEnding As DateTime, _List_Holidays As List(Of Domain.Entities.Holiday)) As Domain.Payroll.Entities.ScheduleDetailHour

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
    Function SaveScheduleDetailMasive(_scheduleDetail As ScheduleDetail, _employeesCheked As List(Of Integer), _dayToSave As List(Of Date), newFunctionalUnit As FunctionalUnit, Include_Holiday As Boolean, currentFunctionalUnit As FunctionalUnit, ScheduleDatasource As List(Of Schedule), Period As String, _List_Holidays As List(Of Domain.Entities.Holiday), audit As AuditMessage, Optional EditFlag As Boolean = False) As ActionMessageResult(Of List(Of Schedule))
End Interface
