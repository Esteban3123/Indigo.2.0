'***********************************************************************
' Assembly         : Domain.Payroll
' Author           : Cristhian Mauricio Salazar
' Created          : 21-06-2013
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports System.Threading.Tasks
Imports Domain.Base
Imports Domain.Payroll.Entities


Public Interface IEmployeeRepository
    Inherits IRepository(Of Employee)
    ''' <summary>
    ''' Lista todos los empleados y sus agregados
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function ListAllEmployee() As List(Of Employee)

    ''' <summary>
    ''' Obtiene un empleado y todos sus agregados atraves del nit del tercero (ASYNC)
    ''' </summary>
    ''' <param name="nit">nit del tercero</param>
    ''' <returns>Task con el Empleado</returns>
    ''' <remarks></remarks>
    Function GetEmployeeAsync(ByVal nit As String) As Task(Of Employee)

    Function GetEmployeeWithGroup(ByVal employeeId As Integer) As Employee

    ''' <summary>
    ''' Lista todos los empleados que tiene una unidad funcional
    ''' </summary>
    ''' <param name="functionalUnitId">Id unidad funcional</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetEmployeesByFunctionalUnit(functionalUnitId As Integer) As List(Of Employee)

    ''' <summary>
    ''' Obtiene un empleado y los agregaos de contratos y fondos de contratos atraves del nit del tercero
    ''' </summary>
    ''' <param name="nit">nit del tercero</param>
    ''' <returns>Empleado</returns>
    ''' <remarks></remarks>
    Function GetEmployeeBasicContract(ByVal nit As String) As Employee

    ''' <summary>
    ''' Guarda o actualiza un empleado y todos sus agregados
    ''' </summary>
    ''' <param name="employee">Empleado</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function SaveEmployee(ByVal employee As Employee) As Boolean

    ''' <summary>
    ''' Obtiene un empleado y todos sus agregados atraves del id
    ''' </summary>
    ''' <param name="id">id del empleado</param>
    ''' <returns>Empleado</returns>
    ''' <remarks></remarks>
    Function GetEmployeeById(ByVal id As Integer, Optional tracking As Boolean = True) As Employee

    ''' <summary>
    ''' Obtiene un empleado por id si sus agregados
    ''' </summary>
    ''' <param name="Id"></param>
    ''' <returns></returns>
    Function GetEmployeeSimpleById(Id As Integer) As Employee

    ''' <summary>
    ''' Obtiene un empleado y los agregados requeridos para ser mostrados de manera informativa en el frontal de liquidacion de contrato
    ''' </summary>
    ''' <param name="id">Id del empleado</param>
    ''' <returns>Empleado</returns>
    Function GetEmployeeByIdForContractLiquidation(ByVal id As Integer) As Employee

    ''' <summary>
    ''' Obtiene los empleados pertenecientes a un grupo
    ''' </summary>
    ''' <param name="groupId">id del grupo</param>
    ''' <param name="agregates">para saber si se envian mas agregados o solo el tercero y el contrato</param>
    ''' <param name="SpecificEmployeeId">Id del empleado específico (opcional, para filtrar por un empleado en particular)</param>
    ''' <returns>Lista de Empleado</returns>
    ''' <remarks></remarks>
    Function GetEmployeeByGroup(ByVal groupId As Integer, Optional agregates As Boolean = True, Optional InitialDate As Date? = Nothing, Optional EndingDate As Date? = Nothing, Optional SpecificEmployeeId As Integer = 0) As List(Of Employee)

    ''' <summary>
    ''' Funcion para obtener el contrato actual del empleado
    ''' </summary>
    ''' <param name="employeeId">Id del empleado</param>
    ''' <returns>Contrato</returns>
    ''' <remarks></remarks>
    Function GetContractValidByEmployee(ByVal employeeId As Integer) As Contract

    Function GetEmployeeByIdContractLiquidation(id As Integer, Optional tracking As Boolean = True) As Employee

    ''' <summary>
    ''' Función que devuelve el Empleado y su Grupo Familiar
    ''' </summary>
    ''' <param name="Id">Id del Empleado</param>
    ''' <returns>Employee</returns>
    ''' <remarks></remarks>
    Function GetEmployeeRelationshipByEmployeeId(Id As Integer) As Employee

    Function GetEmployeePensionary() As List(Of Employee)

    Function GetContractByIncreaseSalary(groupId As Integer, Optional FunctionalUnitId As Integer = 0, Optional PositionId As Integer = 0) As List(Of Contract)

    ''' <summary>
    ''' Obtiene el id del tercero a partir del id del empleado
    ''' </summary>
    ''' <param name="id">id del empleado</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetThirdPartyIdByEmployeeId(ByVal employeeId As Integer) As Integer
End Interface
