'***********************************************************************
' Assembly         : DistributedServices.Payroll
' Author           : Cristhian Mauricio Salazar
' Created          : 21-06-2013
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Payroll.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities
Imports System.Threading.Tasks

<ServiceContract()>
Public Interface IPayrollEmployee

    ''' <summary>
    ''' Lista todos los empleados con agregados de tercero y persona
    ''' </summary>
    ''' <returns>Lista de empleados</returns>
    <OperationContract()>
    Function ListAllEmployee(session As SessionValues) As List(Of Employee)

    ''' <summary>
    ''' Elimina un empleado
    ''' </summary>
    ''' <param name="employee">Empleado</param>
    ''' <returns></returns>
    <OperationContract()>
    Function DeleteEmployee(ByVal employee As Employee, session As SessionValues) As ActionMessageResult

    ''' <summary>
    ''' Guarda o edita un Empleado y todos sus agregados
    ''' </summary>
    ''' <param name="employee">Empleado</param>
    ''' <returns></returns>
    <OperationContract()>
    Function SaveEmployee(ByVal employee As Employee, session As SessionValues, Optional ByVal listExemptIncome As List(Of Domain.Entities.ExemptIncome) = Nothing, Optional ByVal listExemptIncomeToDelete As List(Of Domain.Entities.ExemptIncome) = Nothing) As ActionMessageResult(Of Domain.Payroll.Entities.Employee)

    ''' <summary>
    ''' Obtiene un empleado con todos sus agregados atraves del nit
    ''' </summary>
    ''' <param name="nit">nit del tercero</param>
    ''' <returns> Employee</returns>
    <OperationContract()>
    Function GetEmployee(ByVal nit As String, session As SessionValues) As Task(Of Employee)

    ''' <summary>
    ''' Obtiene un empleado y los agregaos de contratos y fondos de contratos atraves del nit del tercero
    ''' </summary>
    ''' <param name="nit">nit del tercero</param>
    ''' <returns>Empleado</returns>
    ''' <remarks></remarks>
    <OperationContract()> _
    Function GetEmployeeBasicContract(ByVal nit As String, session As SessionValues) As Employee

    ''' <summary>
    ''' Lista todos los empleados que tiene una unidad funcional
    ''' </summary>
    ''' <param name="functionalUnitId">Id unidad funcional</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <OperationContract()> _
    Function GetEmployeesByFunctionalUnit(functionalUnitId As Integer, session As SessionValues) As List(Of Employee)

    ''' <summary>
    ''' Lista todos los empleados que tiene una unidad funcional
    ''' </summary>
    ''' <param name="functionalUnitId">Id unidad funcional</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <OperationContract()> _
    Function GetEmployeesByGroupId(groupId As Integer, session As SessionValues) As List(Of Employee)

    ''' <summary>
    ''' Obtiene un empleado y todos sus agregados atraves del id
    ''' </summary>
    ''' <param name="id">id del empleado</param>
    ''' <returns>Empleado</returns>
    ''' <remarks></remarks>
    <OperationContract()> _
    Function GetEmployeeById(ByVal id As Integer, session As SessionValues) As Employee


    ''' <summary>
    ''' Obtiene un empleado y todos sus agregados atraves del id
    ''' </summary>
    ''' <param name="id">id del empleado</param>
    ''' <returns>Empleado</returns>
    ''' <remarks></remarks>
    <OperationContract()> _
    Function GetEmployeeByIdForContractLiquidation(ByVal id As Integer, session As SessionValues) As Employee

    ''' <summary>
    ''' Obtiene un empleado y todos sus agregados atraves del id
    ''' </summary>
    ''' <param name="id">id del empleado</param>
    ''' <returns>Empleado</returns>
    ''' <remarks></remarks>
    <OperationContract()>
    Function GetEmployeePensionary(session As SessionValues) As List(Of Employee)
    ''' <summary>
    ''' Guarda la rneta exenta
    ''' </summary>
    ''' <param name="exemptIncome"></param>
    ''' <param name="session"></param>
    ''' <returns></returns>
    <OperationContract()>
    Function SaveExemptIncome(ByVal exemptIncome As List(Of Domain.Entities.ExemptIncome), session As SessionValues) As ActionMessageResult(Of Domain.Entities.ExemptIncome)
    ''' <summary>
    ''' Elimina la renta exenta
    ''' </summary>
    ''' <param name="exemptIncome"></param>
    ''' <param name="session"></param>
    ''' <returns></returns>
    <OperationContract()>
    Function DeleteExemptIncome(ByVal exemptIncome As List(Of Domain.Entities.ExemptIncome), session As SessionValues) As ActionMessageResult(Of Domain.Entities.ExemptIncome)





End Interface
