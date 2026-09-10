'***********************************************************************
' Assembly         : Application.Payroll
' Author           : Daniel Eduardo Arévalo Bonilla
' Created          : 07-07-2013
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Payroll
Imports Domain.Payroll.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities

Public Interface INoveltyAdminService
    Inherits IDisposable

    Function CalculateTwoFirstDays(employeeId As Integer, session As SessionValues) As ActionResult(Of Decimal)
    ''' <summary>
    ''' Obtiene una Incapacidad
    ''' </summary>
    ''' <param name="code">Código de la Incapacidad</param>
    ''' <returns>Incapacidad</returns>
    ''' <remarks></remarks>
    Function GetNovelty(ByVal code As String) As Novelty

    ''' <summary>
    ''' Lista todas las Incapacidades por Empleado
    ''' </summary>
    ''' <param name="employeeId">Id del Empleado</param>
    ''' <returns>Incapacidades por Empleado</returns>
    ''' <remarks></remarks>
    Function GetEmployeeNovelty(ByVal employeeId As Integer) As List(Of Novelty)

    ''' <summary>
    ''' Graba o actualiza una Incapacidad
    ''' </summary>
    ''' <param name="novelty">noevdad</param>
    ''' <param name="audit">Objeto auditoria</param>
    ''' <returns>Boolean</returns>
    ''' <remarks></remarks>
    Function SaveNovelty(ByVal novelty As Novelty, ByVal audit As AuditMessage, Optional saveEntity As Boolean = True) As ActionMessageResult(Of Novelty)

    ''' <summary>
    ''' elimina un objeto Incapacidad
    ''' </summary>
    ''' <param name="novelty">Novedad</param>
    ''' <param name="audit">Objeto auditoria</param>
    ''' <returns>Boolean</returns>
    ''' <remarks></remarks>
    Function DeleteNovelty(ByVal novelty As Novelty, ByVal audit As AuditMessage) As Boolean

    ''' <summary>
    ''' Lista las incapacidades de un empleados que esten liquidadas o no 
    ''' </summary>
    ''' <param name="employeeId">Id del empleado</param>
    ''' <param name="liquidate">Si esta liquidado</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetNoveltyLiquidate(ByVal employeeId As Integer, ByVal liquidate As Boolean) As List(Of Novelty)

    ''' <summary>
    ''' Lista las Novedades de un empleado y filtra por tipo de novedad (Sancion, Licencia o Incapacidad)
    ''' </summary>
    ''' <param name="employeeId">Id del empleado</param>
    ''' <param name="typeNovelty">Tipo de la novedad</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetNoveltyByTypeNovelty(ByVal employeeId As Integer, ByVal typeNovelty As Byte) As List(Of Novelty)

    ''' <summary>
    ''' Funcion que lista las novedades de un empleado en un rango establecido
    ''' </summary>
    ''' <param name="employeeId">id empleado</param>
    ''' <param name="fechaInicio">Fecha Inicio</param>
    ''' <param name="fechaFin">Fecha Fin</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetNoveltyByEmployeeDateInitialEnd(employeeId As Integer, fechaInicio As Date, fechaFin As Date) As List(Of Novelty)

    ''' <summary>
    ''' Obtiene una novedad por id
    ''' </summary>
    ''' <param name="id">id de la Incapacidad</param>
    ''' <returns>Novedad</returns>
    ''' <remarks></remarks>
    Function GetNoveltyById(ByVal id As Integer) As Novelty

    ''' <summary>
    ''' obtiene un listado de los detalles que estan dentro de un rango de fechas
    ''' </summary>
    ''' <param name="employeeId">Id del empleado</param>
    ''' <param name="dateInitial">Fecha Inicial</param>
    ''' <param name="dateEnd">Fecha Final</param>
    ''' <returns>Lista de detalles de un calendario</returns>
    ''' <remarks></remarks>
    Function GetScheduleDetailByEmployeeBetweenDate(employeeId As Integer, dateInitial As Date, dateEnd As Date) As List(Of ScheduleDetail)

    ''' <summary>
    ''' Obtiene la lista de novedades que estan dentro de un rango de fechas a excepcion de la que se envia como parametro
    ''' </summary>
    ''' <param name="noveltyId">id de la novedad que se va a exonerar</param>
    ''' <param name="initialDate">Fecha de inicio</param>
    ''' <param name="endDate">Fecha Fin</param>
    ''' <returns>Lista de novedades</returns>
    ''' <remarks></remarks>
    Function GetNoveltyDistinctNoveltyBetweenDate(noveltyId As Integer, employeeId As Integer, initialDate As Date, endDate As Date) As List(Of Novelty)

    ''' <summary>
    ''' Obtiene la novedad que tenga la fecha mas alta del mismo codigo consecutivo
    ''' </summary>
    ''' <param name="consecutive">Consecutivo a buscar</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetUltimateNoveltyConsecutive(consecutive As Integer) As Novelty

    ''' <summary>
    ''' Obtiene un promedio del ibc del empleado en los ultimos meses
    ''' </summary>
    ''' <param name="contractId">Id del contrato</param>
    ''' <param name="numberLastMonth">Meses que se quiere calcular</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetAverageIBCLiquidationLastMonth(contractId As Integer, numberLastMonth As Integer) As Decimal

    ''' <summary>
    ''' Obtiene la Lista de Novedades por consecutivo
    ''' </summary>
    ''' <param name="Consecutive">Consecutivo</param>
    ''' <returns>List(Of Novelty)</returns>
    ''' <remarks></remarks>
    Function GetListNoveltyByConsecutive(Consecutive As Integer) As List(Of Novelty)
    ''' <summary>
    ''' Calcula  el valor del concepto 
    ''' </summary>
    ''' <param name="contract"></param>
    ''' <param name="noveltyDays"></param>
    ''' <param name="session"></param>
    ''' <returns></returns>
    Function CalculateNoveltyConcept(contract As Contract, noveltyDays As Integer, session As SessionValues) As ActionResult(Of Decimal)


End Interface
