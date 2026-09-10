'***********************************************************************
' Assembly         : Domain.Payroll
' Author           : Cristhian Mauricio Salazar
' Created          : 05-08-2013
'
' Modified Last By : Cristhian Salazar
' Modified Last On : 06-09-2013
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Base
Imports Domain.Payroll.Entities
Public Interface IScheduleRepository
    Inherits IRepository(Of Schedule)

    ''' <summary>
    ''' Lista todos los horarios
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function ListAllSchedule() As List(Of Schedule)

    ''' <summary>
    ''' Devuelve el schedule por id desatachado
    ''' </summary>
    ''' <param name="id">id del schedule</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetScheduleByIdAsNoTracking(id As Integer) As Schedule

    ''' <summary>
    ''' Obtiene un horario especifico
    ''' </summary>
    ''' <param name="functionalUnitCode">Codigo unidad funcional</param>
    ''' <param name="period">Peridodo del horario</param>
    ''' <returns>Horario</returns>
    ''' <remarks></remarks>
    Function GetSchedule(functionalUnitCode As String, period As String) As List(Of Schedule)
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
    Function GetScheduleByEmployeePeriodFunctionalUnit(idEmployee As Integer, period As String, functionalUnitId As Integer, Optional IncludeDetails As Boolean = True) As Schedule

    ''' <summary>
    ''' funcion para obtener todos lo turnos de un empleado en cierto rango de un periodo y en una unidad funcional especifica
    ''' </summary>
    ''' <param name="idEmployee">id del empleado</param>
    ''' <param name="period">periodo del calendario</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetScheduleByEmployeePeriodFunctionalUnitRangeDays(idEmployee As Integer, period As String, functionalUnitId As Integer, diaMin As Integer, diaMax As Integer) As Schedule

    ''' <summary>
    ''' Función que dispara el SP para Análisis de Turnos de los Empleados
    ''' </summary>
    ''' <param name="InitialDate"></param>
    ''' <param name="EndDate"></param>
    ''' <returns></returns>
    Function AnalisisEmployeeSchedule(InitialDate As Date, EndDate As Date, ByVal IdFunctionalUnit As Integer, ByVal IdPosition As Integer, IdEmployee As Integer) As List(Of SP_AnalisEmployeeSchedule_Result)

    ''' <summary>
    ''' función para almacenar las horas extras al cuadro de turnos
    ''' </summary>
    ''' <param name="ObjXml"></param>
    ''' <returns></returns>
    Function ExecuteSaveExtraHours(ObjXml As String) As SP_SaveExtraHours_Result

End Interface
