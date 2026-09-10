'***********************************************************************
' Assembly         : Domain.Payroll
' Author           : Daniel Eduardo Arévalo Bonilla
' Created          : 07-07-2013
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Base
Imports Domain.Payroll.Entities

Public Interface INoveltyRepository
    Inherits IRepository(Of Novelty)


    ''' <summary>
    ''' Obtiene una Incapacidad
    ''' </summary>
    ''' <param name="code">Código de la Incapacidad</param>
    ''' <returns>Incapacidad</returns>
    ''' <remarks></remarks>
    Function GetNovelty(ByVal code As String) As Novelty

    ''' <summary>
    ''' Obtiene una Lista de Incapacidades por Empleado
    ''' </summary>
    ''' <param name="employeeId">Id del Empleado</param>
    ''' <returns>Incapacidades por Empleado</returns>
    ''' <remarks></remarks>
    Function GetEmployeeNovelty(ByVal employeeId As Integer) As List(Of Novelty)

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
    Function GetNoveltyById(ByVal id As Integer, Optional tracking As Boolean = True) As Novelty

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
    ''' Obtiene la lista de novedades que estan dentro de un rango de fechas a excepcion de la que se envia como parametro
    ''' </summary>
    ''' <param name="listEmployeeId">Lista de id de empleados</param>
    ''' <param name="initialDate">Fecha de inicio</param>
    ''' <param name="endDate">Fecha Fin</param>
    ''' <returns>Lista de novedades</returns>
    ''' <remarks></remarks>
    Function GetNoveltyByListIdEmployeeBetweenDate(listEmployeeId As List(Of Integer), initialDate As Date, endDate As Date) As List(Of Novelty)

    ''' <summary>
    ''' Obtiene el Listado de Novedades de un Empleado para liquidarlas por Liquidación de Nómina. Incapacidades NO pagadas o Liquidadas PARCIALMENTE
    ''' </summary>
    ''' <param name="EmployeeId">Id Empleado</param>
    ''' <returns>List(Of Novelty)</returns>
    ''' <remarks></remarks>
    Function GetNoveltyByEmployeeIdPayrollLiquidation(EmployeeId As Integer) As List(Of Novelty)

    Function GetNoveltyUnpaidLicenses(EmployeeId As Integer) As List(Of Novelty)

    Function GetNoveltySanctions(EmployeeId As Integer) As List(Of Novelty)

    Function GetListNoveltyByConsecutive(Consecutive As Integer) As List(Of Novelty)

End Interface
