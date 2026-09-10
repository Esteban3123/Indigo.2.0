'***********************************************************************
' Assembly         : DistributedServices.Payroll
' Author           : Daniel Eduardo Arévalo Bonilla
' Created          : 07-07-2013
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Payroll.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities

<ServiceContract()> _
Public Interface IPayrollNovelty

    <OperationContract>
    Function CalculateTwoFirstDays(employeeId As Integer, session As SessionValues) As ActionResult(Of Decimal)
    ''' <summary>
    ''' Elimina una Incapacidad
    ''' </summary>
    ''' <param name="novelty">Novedad</param>
    ''' <param name="audit">Objeto Auditoria</param>
    ''' <returns>Boolean</returns>
    ''' <remarks></remarks>
    <OperationContract()> _
    Function DeleteNovelty(ByVal novelty As Novelty, session As SessionValues) As Boolean

    ''' <summary>
    ''' Almacena o Actualiza una Incapacidad
    ''' </summary>
    ''' <param name="novelty">Novedad</param>
    ''' <param name="audit">Objeto Auditoria</param>
    ''' <returns>Boolean</returns>
    ''' <remarks></remarks>
    <OperationContract()> _
    Function SaveNovelty(ByVal novelty As Novelty, session As SessionValues) As ActionMessageResult(Of Novelty)

    ''' <summary>
    ''' Obtiene una Incapacidad
    ''' </summary>
    ''' <param name="code">Código de la Incapacidad</param>
    ''' <returns>Incapacidad</returns>
    ''' <remarks></remarks>
    <OperationContract()> _
    Function GetNovelty(ByVal code As String, session As SessionValues) As Novelty

    ''' <summary>
    ''' Obtiene el listado de Incapacidades por Empleado
    ''' </summary>
    ''' <param name="employeeId">Id del Empleado</param>
    ''' <returns>Incapacidades por Empleado</returns>
    ''' <remarks></remarks>
    <OperationContract()> _
    Function GetEmployeeNovelty(employeeId As Integer, session As SessionValues) As List(Of Novelty)

    ''' <summary>
    ''' Lista las incapacidades de un empleados que esten liquidadas o no 
    ''' </summary>
    ''' <param name="employeeId">Id del empleado</param>
    ''' <param name="liquidate">Si esta liquidado</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <OperationContract()> _
    Function GetNoveltyLiquidate(ByVal employeeId As Integer, ByVal liquidate As Boolean, session As SessionValues) As List(Of Novelty)

    ''' <summary>
    ''' Lista las Novedades de un empleado y filtra por tipo de novedad (Sancion, Licencia o Incapacidad)
    ''' </summary>
    ''' <param name="employeeId">Id del empleado</param>
    ''' <param name="typeNovelty">Tipo de la novedad</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <OperationContract()> _
    Function GetNoveltyByTypeNovelty(ByVal employeeId As Integer, ByVal typeNovelty As Byte, session As SessionValues) As List(Of Novelty)

    ''' <summary>
    ''' Funcion que lista las novedades de un empleado en un rango establecido
    ''' </summary>
    ''' <param name="employeeId">id empleado</param>
    ''' <param name="fechaInicio">Fecha Inicio</param>
    ''' <param name="fechaFin">Fecha Fin</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <OperationContract()> _
    Function GetNoveltyByEmployeeDateInitialEnd(employeeId As Integer, fechaInicio As Date, fechaFin As Date, session As SessionValues) As List(Of Novelty)

    ''' <summary>
    ''' Obtiene una novedad por id
    ''' </summary>
    ''' <param name="id">id de la Incapacidad</param>
    ''' <returns>Novedad</returns>
    ''' <remarks></remarks>
    <OperationContract()> _
    Function GetNoveltyById(ByVal id As Integer, session As SessionValues) As Novelty

    ''' <summary>
    ''' obtiene un listado de los detalles que estan dentro de un rango de fechas
    ''' </summary>
    ''' <param name="employeeId">Id del empleado</param>
    ''' <param name="dateInitial">Fecha Inicial</param>
    ''' <param name="dateEnd">Fecha Final</param>
    ''' <returns>Lista de detalles de un calendario</returns>
    ''' <remarks></remarks>
    <OperationContract()> _
    Function GetScheduleDetailByEmployeeBetweenDateFromNovelty(employeeId As Integer, dateInitial As Date, dateEnd As Date, session As SessionValues) As List(Of ScheduleDetail)

    ''' <summary>
    ''' Obtiene la lista de novedades que estan dentro de un rango de fechas a excepcion de la que se envia como parametro
    ''' </summary>
    ''' <param name="noveltyId">id de la novedad que se va a exonerar</param>
    ''' <param name="initialDate">Fecha de inicio</param>
    ''' <param name="endDate">Fecha Fin</param>
    ''' <returns>Lista de novedades</returns>
    ''' <remarks></remarks>
    <OperationContract()> _
    Function GetNoveltyDistinctNoveltyBetweenDate(noveltyId As Integer, employeeId As Integer, initialDate As Date, endDate As Date, session As SessionValues) As List(Of Novelty)

    ''' <summary>
    ''' Obtiene la novedad que tenga la fecha mas alta del mismo codigo consecutivo
    ''' </summary>
    ''' <param name="consecutive">Consecutivo a buscar</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <OperationContract()> _
    Function GetUltimateNoveltyConsecutive(consecutive As Integer, session As SessionValues) As Novelty

    ''' <summary>
    ''' Obtiene un promedio del ibc del empleado en los ultimos meses
    ''' </summary>
    ''' <param name="contractId">Id del contrato</param>
    ''' <param name="numberLastMonth">Meses que se quiere calcular</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <OperationContract()> _
    Function GetAverageIBCLiquidationLastMonth(contractId As Integer, numberLastMonth As Integer, session As SessionValues) As Decimal

    ''' <summary>
    ''' Función para Obtener un listado de Novedades por Consecutivo
    ''' </summary>
    ''' <param name="Consecutive">Consecutive</param>
    ''' <param name="session">session</param>
    ''' <returns>List(Of Novelty)</returns>
    ''' <remarks></remarks>
    <OperationContract()>
    Function GetListNoveltyByConsecutive(Consecutive As Integer, session As SessionValues) As List(Of Novelty)
    ''' <summary>
    ''' Funcion que calcula el valor del concepto para salario integral
    ''' </summary>
    ''' <param name="contract"></param>
    ''' <param name="noveltyDays"></param>
    ''' <param name="session"></param>
    ''' <returns></returns>
    <OperationContract()>
    Function CalculateNoveltyConcept(contract As Contract, noveltyDays As Integer, session As SessionValues) As ActionResult(Of Decimal)


End Interface
