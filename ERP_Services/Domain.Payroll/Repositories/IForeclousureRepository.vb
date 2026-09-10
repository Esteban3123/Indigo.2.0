'***********************************************************************
' Assembly         : Domain.Payroll
' Author           : Daniel Eduardo Arévalo
' Created          : 19-09-2018
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Base
Imports Domain.Payroll.Entities

Public Interface IForeclousureRepository
    Inherits IRepository(Of Foreclousure)


    ''' <summary>
    ''' Obtiene un convenio por el consecutivo
    ''' </summary>
    ''' <param name="consecutive">consecutivo</param>
    ''' <param name="tracking">control de segumiento de cambios</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetForeclousure(ByVal consecutive As String, Optional tracking As Boolean = True) As Foreclousure


    ''' <summary>
    ''' Lista de convenios
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function ListForeclousure() As List(Of Foreclousure)

    ''' <summary>
    ''' Lista de Convenios por ID del Empleado, Fecha Inicio del Convenio y Concepto
    ''' </summary>
    ''' <param name="EmployeeId">Id del Empleado</param>
    ''' <param name="PayrollDate">Fecha inicio Nómina</param>
    ''' <param name="ConceptId">Id del Concepto</param>
    ''' <returns>Agreements</returns>
    ''' <remarks></remarks>
    Function LisForeclousureByEmployeeIdStarDate(EmployeeId As String, PayrollDate As Date, ConceptId As String) As List(Of Foreclousure)

    ''' <summary>
    ''' Obtiene los convenios que tenga el empleado
    ''' </summary>
    ''' <param name="employeeId">Id del empleado</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetForeclousureByEmployee(ByVal employeeId As Integer) As List(Of Foreclousure)

    ''' <summary>
    ''' Obtiene los convenios por ID
    ''' </summary>
    ''' <param name="Foreclousure">Id del Convenio</param>
    ''' <returns>AgreementsC</returns>
    ''' <remarks></remarks>
    Function GetForeclousureById(ForeclousureId As Integer) As Foreclousure

    ''' <summary>
    ''' Función creada para que busque el Id del Detalle del Convenio, para insertarlo en el detalle de la Liquidación de Nómina
    ''' </summary>
    ''' <param name="AgreementId">Id de la Cabecera del Convenio</param>
    ''' <param name="PayrollDate">Fecha de Nomina</param>
    ''' <returns>AgreementsD</returns>
    ''' <remarks></remarks>
    Function GetForeclousureDetailByForeclousureIdPayrollDate(ForeclousureID As Integer, PayrollDate As Date) As ForeclousureDetail

    ''' <summary>
    ''' Listado de Embargos en estado confirmado por Fecha
    ''' </summary>
    ''' <param name="PayrollDate"></param>
    ''' <returns></returns>
    Function ListForeclousureByDate(PayrollDate As Date) As List(Of Foreclousure)

    ''' <summary>
    ''' Lista de Embargos por Estado
    ''' </summary>
    ''' <param name="employeeId">ID del Empleado</param>
    ''' <param name="Status">Estado</param>
    ''' <returns>List(Of Foreclousure)</returns>
    Function GetForeclousureByEmployeeStatus(employeeId As Integer, Status As Integer) As List(Of Foreclousure)

    ''' <summary>
    ''' Lista de Embargos por Estado para pago por Vacaciones
    ''' </summary>
    ''' <param name="employeeId"></param>
    ''' <param name="Status"></param>
    ''' <returns></returns>
    Function GetForeclousureByEmployeeStatusVacation(employeeId As Integer, Status As Integer) As List(Of Foreclousure)

    ''' <summary>
    ''' Función para cargar el listado de Embargos sin tener en cuenta el estado
    ''' </summary>
    ''' <param name="PayrollDate"></param>
    ''' <returns></returns>
    Function ListForeclousureByDateNoStatus(PayrollDate As Date) As List(Of Foreclousure)

End Interface
