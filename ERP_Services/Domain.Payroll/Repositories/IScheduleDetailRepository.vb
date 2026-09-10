'***********************************************************************
' Assembly         : Domain.Payroll
' Author           : Cristhian Mauricio Salazar
' Created          : 14-08-2013
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Base
Imports Domain.Payroll.Entities
Imports Infrastructure.CrossCutting.Base
Public Interface IScheduleDetailRepository
    Inherits IRepository(Of ScheduleDetail)

    ''' <summary>
    ''' obtiene un detalle del calendario
    ''' </summary>
    ''' <param name="id">id del detalle del calendario</param>
    ''' <returns>ScheduleDetail</returns>
    ''' <remarks></remarks>
    Function GetScheduleDetail(ByVal id As String, Optional tracking As Boolean = True) As ScheduleDetail

    ''' <summary>
    ''' obtiene un listado de los detalles que estan dentro de un rango de fechas
    ''' </summary>
    ''' <param name="dateInitial">Fecha Inicial</param>
    ''' <param name="dateEnd">Fecha Final</param>
    ''' <returns>Lista de detalles de un calendario</returns>
    ''' <remarks></remarks>
    Function GetScheduleDetailBetweenDate(dateInitial As Date, dateEnd As Date) As List(Of ScheduleDetail)

    ''' <summary>
    ''' obtiene el listado de los detalles que estan dentro de un rango de fechas, de un determinado grupo, y que estan marcados como eventos
    ''' </summary>
    ''' <param name="groupId">id del grupo</param>
    ''' <param name="dateInitial">fecha inicio</param>
    ''' <param name="dateEnd">fecha fin</param>
    ''' <returns>los schedule details</returns>
    ''' <remarks></remarks>
    Function GetScheduleDetailWithEvents(functionalunitId As Integer, dateInitial As Date, dateEnd As Date) As List(Of ScheduleDetail)

    ''' <summary>
    ''' obtiene un listado de los detalles de un empleado que estan dentro de un rango de fechas
    ''' </summary>
    ''' <param name="employeeId">Id del empleado</param>
    ''' <param name="dateInitial">Fecha Inicial</param>
    ''' <param name="dateEnd">Fecha Final</param>
    ''' <returns>Lista de detalles de un calendario</returns>
    ''' <remarks></remarks>
    Function GetScheduleDetailByEmployeeBetweenDate(employeeId As Integer, dateInitial As Date, dateEnd As Date) As List(Of ScheduleDetail)

    ''' <summary>
    ''' obtiene un listado de los detalles que estan dentro de un rango de fechas
    ''' </summary>
    ''' <param name="employeeId">Id del empleado</param>
    ''' <param name="functionalUnitId">Id de la unidad funcional</param>
    ''' <param name="dateInitial">Fecha Inicial</param>
    ''' <param name="dateEnd">Fecha Final</param>
    ''' <returns>Lista de detalles de un calendario</returns>
    ''' <remarks></remarks>
    Function GetScheduleDetailByEmployeeFuctionalUnitBetweenDate(employeeId As Integer, functionalUnitId As Integer, dateInitial As Date, dateEnd As Date) As List(Of ScheduleDetail)

    ''' <summary>
    ''' obtiene el listado de los detalles que están dentro de un rango de fechas SIN Novedades
    ''' </summary>
    ''' <param name="employeeId">Id del Empleado</param>
    ''' <param name="functionalUnitId">Id de la Unidad Funcional</param>
    ''' <param name="dateInitial">Fecha Inicial</param>
    ''' <param name="dateEnd">Fecha Final</param>
    ''' <returns>Lista de Detalles de un Calendario</returns>
    ''' <remarks></remarks>
    Function GetScheduleDetailByEmployeeFuctionalUnitBetweenDateWithoutNovelties(employeeId As Integer, functionalUnitId As Integer, dateInitial As Date, dateEnd As Date) As List(Of ScheduleDetail)

    ''' <summary>
    ''' Función para cargar el Listado de Detallas dentro de un Rango de Fechas
    ''' </summary>
    ''' <param name="employeeId">Id del Empleado</param>
    ''' <param name="dateInitial">Fecha Inicio</param>
    ''' <param name="dateEnd">fecha Fin</param>
    ''' <returns>List(Of ScheduleDetail)</returns>
    ''' <remarks></remarks>
    Function GetScheduleDetailByEmployeeBetweenDateWithoutNovelties(employeeId As Integer, dateInitial As Date, dateEnd As Date) As List(Of ScheduleDetail)

    ''' <summary>
    ''' obtiene un listado de los detalles de un empleado que este dentro de una novedad
    ''' </summary>
    ''' <param name="employeeId">Id del empleado</param>
    ''' <param name="noveltyId">Id de la novedad</param>
    ''' <returns>Lista de detalles de un calendario</returns>
    ''' <remarks></remarks>
    Function GetScheduleDetailTypeNoveltyByEmployeeNoveltyId(employeeId As Integer, noveltyId As Integer) As List(Of ScheduleDetail)

    ''' <summary>
    ''' Obtiene la lista de detalles que tiene un contrato especifico
    ''' </summary>
    ''' <param name="contractId">Id del Contrato</param>
    ''' <returns>Lista de ScheduleDetail</returns>
    ''' <remarks></remarks>
    Function GetScheduleDetailByContractId(contractId As Integer) As List(Of ScheduleDetail)

    ''' <summary>
    ''' Obtiene los detalles de calendario de varios empleados en un rango de fechas
    ''' </summary>
    ''' <param name="listIdEmpleoyee">Lista de ids de empleados</param>
    ''' <param name="dateInitial">Fecha Inicial</param>
    ''' <param name="dateEnd">Fecha Final</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetScheduleDetailByListEmployeeBetweenDate(listIdEmpleoyee As List(Of Integer), dateInitial As Date, dateEnd As Date) As List(Of ScheduleDetail)

    ''' <summary>
    ''' obtiene el numero de horas totales de un empleado en un determinado rango de fechas
    ''' </summary>
    ''' <param name="employeeId">Id del empleado</param>
    ''' <param name="dateInitial">Fecha Inicial</param>
    ''' <param name="dateEnd">Fecha Final</param>
    ''' <returns>Numero de horas que se tiene en general</returns>
    ''' <remarks></remarks>
    Function GetHoursNumber_ScheduleDetailByEmployeeBetweenDate(employeeId As Integer, dateInitial As Date, dateEnd As Date) As Integer

    ''' <summary>
    ''' obtiene un listado de los detalles de un empleado
    ''' </summary>
    ''' <param name="employeeId">Id del empleado</param>
    ''' <returns>Lista de detalles de un calendario</returns>
    ''' <remarks></remarks>
    Function GetScheduleDetailByEmployee(employeeId As Integer) As List(Of ScheduleDetail)

    ''' <summary>
    ''' Función para cargar el Listado de Detallas dentro de un Rango de Fechas
    ''' </summary>
    ''' <param name="employeeId">Id del Empleado</param>
    ''' <param name="dateInitial">Fecha Inicio</param>
    ''' <param name="dateEnd">fecha Fin</param>
    ''' <returns>List(Of ScheduleDetail)</returns>
    ''' <remarks></remarks>
    Function GetScheduleDetailForCostDistributions(employeeId As Integer, dateInitial As Date, dateEnd As Date) As List(Of CostDistributionCostCenter)

    Function GetScheduleDetailByEmployeeUnitBetweenDateWithoutNoveltiesLiquidation(employeeId As Integer, dateInitial As Date, dateEnd As Date) As List(Of ScheduleDetail)

End Interface

