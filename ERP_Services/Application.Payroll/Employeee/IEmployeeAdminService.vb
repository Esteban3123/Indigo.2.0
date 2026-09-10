'***********************************************************************
' Assembly         : Application.Payroll
' Author           : Cristhian Mauricio Salazar
' Created          : 08-07-2013
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Payroll.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities

Public Interface IEmployeeAdminService
    Inherits IDisposable

    ''' <summary>
    ''' Lista todos los empleados con agregados de tercero y persona
    ''' </summary>
    ''' <returns>Lista de empleados</returns>
    Function ListAllEmployee() As List(Of Employee)

    ''' <summary>
    ''' Elimina un empleado
    ''' </summary>
    ''' <param name="employee">Empleado</param>
    ''' <returns></returns>
    Function DeleteEmployee(ByVal employee As Employee, ByVal audit As AuditMessage) As ActionMessageResult

    ''' <summary>
    ''' Guarda o edita un Empleado y todos sus agregados
    ''' </summary>
    ''' <param name="employee">Empleado</param>
    ''' <returns></returns>
    Function SaveEmployee(ByVal employee As Employee, ByVal audit As AuditMessage, Optional ByVal exempIncomeList As List(Of Domain.Entities.ExemptIncome) = Nothing, Optional ByVal exempIncomeListToDelete As List(Of Domain.Entities.ExemptIncome) = Nothing) As ActionMessageResult(Of Employee)

    ''' <summary>
    ''' Obtiene un empleado con todos sus agregados atraves del nit (ASYNC)
    ''' </summary>
    ''' <param name="nit">nit del tercero</param>
    ''' <returns>Task con el Employee</returns>
    Function GetEmployeeAsync(ByVal nit As String) As Task(Of Employee)

    ''' <summary>
    ''' Obtiene un empleado y los agregaos de contratos y fondos de contratos atraves del nit del tercero
    ''' </summary>
    ''' <param name="nit">nit del tercero</param>
    ''' <returns>Empleado</returns>
    ''' <remarks></remarks>
    Function GetEmployeeBasicContract(ByVal nit As String) As Employee

    ''' <summary>
    ''' Lista todos los empleados que tiene una unidad funcional
    ''' </summary>
    ''' <param name="functionalUnitId">Id unidad funcional</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetEmployeesByFunctionalUnit(functionalUnitId As Integer) As List(Of Employee)

    ''' <summary>
    ''' Obtiene un empleado y todos sus agregados atraves del id
    ''' </summary>
    ''' <param name="id">id del empleado</param>
    ''' <returns>Empleado</returns>
    ''' <remarks></remarks>
    Function GetEmployeeById(ByVal id As Integer) As Employee

    ''' <summary>
    ''' Obtiene un empleado y todos sus agregados atraves del id
    ''' </summary>
    ''' <param name="id">id del empleado</param>
    ''' <returns>Empleado</returns>
    ''' <remarks></remarks>
    Function GetEmployeeByIdForContractLiquidation(ByVal id As Integer) As Employee

    ''' <summary>
    ''' Obtiene los empleados por grupo
    ''' </summary>
    ''' <param name="groupId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetEmployeesByGroupId(groupId As Integer) As List(Of Employee)

    Function GetEmployeePensionary() As List(Of Employee)
    ''' <summary>
    ''' Guarda los detalles de rentas exentas
    ''' </summary>
    ''' <param name="exemptIncome"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    Function SaveExemptIncome(ByVal exemptIncome As List( Of Domain.Entities.ExemptIncome), ByVal audit As AuditMessage) As ActionMessageResult(Of Domain.Entities.ExemptIncome)
    ''' <summary>
    ''' Elimina el detalle de las rentas exentas
    ''' </summary>
    ''' <param name="exemptIncome"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    Function DeleteExemptIncome(ByVal exemptIncome As List(Of Domain.Entities.ExemptIncome), ByVal audit As AuditMessage) As ActionMessageResult(Of Domain.Entities.ExemptIncome)
End Interface
