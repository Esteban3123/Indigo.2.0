'***********************************************************************
' Assembly         : DistributedServices.Payroll
' Author           : Cristhian Mauricio Salazar
' Created          : 09-07-2013
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Application.Payroll
Imports Infrastructure.CrossCutting.IOC
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities

Partial Class PayrollService


    ''' <summary>
    ''' Elimina un empleado
    ''' </summary>
    ''' <param name="employee">Empleado</param>
    ''' <returns></returns>
    Public Function DeleteEmployee(employee As Domain.Payroll.Entities.Employee, session As SessionValues) As ActionMessageResult Implements IPayrollEmployee.DeleteEmployee
        Using employeeAdmin As IEmployeeAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of IEmployeeAdminService)()
            Return employeeAdmin.DeleteEmployee(employee, session.AuditMessageWcf)
        End Using
    End Function

    ''' <summary>
    ''' Obtiene un empleado con todos sus agregados atraves del nit
    ''' </summary>
    ''' <param name="nit">nit del tercero</param>
    ''' <returns> Employee</returns>
    Public Async Function GetEmployee(nit As String, session As SessionValues) As Threading.Tasks.Task(Of Domain.Payroll.Entities.Employee) Implements IPayrollEmployee.GetEmployee
        Using employeeAdmin As IEmployeeAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of IEmployeeAdminService)()
            Return Await employeeAdmin.GetEmployeeAsync(nit)
        End Using
    End Function

    ''' <summary>
    ''' Lista todos los empleados con agregados de tercero y persona
    ''' </summary>
    ''' <returns>Lista de empleados</returns>
    Public Function ListAllEmployee(session As SessionValues) As List(Of Domain.Payroll.Entities.Employee) Implements IPayrollEmployee.ListAllEmployee
        Using employeeAdmin As IEmployeeAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of IEmployeeAdminService)()
            Return employeeAdmin.ListAllEmployee()
        End Using
    End Function

    ''' <summary>
    ''' Guarda o edita un Empleado y todos sus agregados
    ''' </summary>
    ''' <param name="employee">Empleado</param>
    ''' <returns></returns>
    Public Function SaveEmployee(employee As Domain.Payroll.Entities.Employee, session As SessionValues, Optional ByVal listExemptIncome As List(Of Domain.Entities.ExemptIncome) = Nothing, Optional ByVal listExemptIncomeToDelete As List(Of Domain.Entities.ExemptIncome) = Nothing) As ActionMessageResult(Of Domain.Payroll.Entities.Employee) Implements IPayrollEmployee.SaveEmployee
        Using employeeAdmin As IEmployeeAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of IEmployeeAdminService)()
            Return employeeAdmin.SaveEmployee(employee, session.AuditMessageWcf, listExemptIncome, listExemptIncomeToDelete)
        End Using
    End Function

    ''' <summary>
    ''' Obtiene un empleado y los agregaos de contratos y fondos de contratos atraves del nit del tercero
    ''' </summary>
    ''' <param name="nit">nit del tercero</param>
    ''' <returns>Empleado</returns>
    ''' <remarks></remarks>
    Public Function GetEmployeeBasicContract(nit As String, session As SessionValues) As Domain.Payroll.Entities.Employee Implements IPayrollEmployee.GetEmployeeBasicContract
        Using employeeAdmin As IEmployeeAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of IEmployeeAdminService)()
            Return employeeAdmin.GetEmployeeBasicContract(nit)
        End Using
    End Function

    ''' <summary>
    ''' Lista todos los empleados que tiene una unidad funcional
    ''' </summary>
    ''' <param name="functionalUnitId">Id unidad funcional</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetEmployeesByFunctionalUnit(functionalUnitId As Integer, session As SessionValues) As List(Of Domain.Payroll.Entities.Employee) Implements IPayrollEmployee.GetEmployeesByFunctionalUnit
        Using employeeAdmin As IEmployeeAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of IEmployeeAdminService)()
            Return employeeAdmin.GetEmployeesByFunctionalUnit(functionalUnitId)
        End Using
    End Function

    ''' <summary>
    ''' Lista todos los empleados que tiene una unidad funcional
    ''' </summary>
    ''' <param name="functionalUnitId">Id unidad funcional</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetEmployeesByGroupId(groupId As Integer, session As SessionValues) As List(Of Domain.Payroll.Entities.Employee) Implements IPayrollEmployee.GetEmployeesByGroupId
        Using employeeAdmin As IEmployeeAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of IEmployeeAdminService)()
            Return employeeAdmin.GetEmployeesByGroupId(groupId)
        End Using
    End Function


    ''' <summary>
    ''' Obtiene un empleado y todos sus agregados atraves del id
    ''' </summary>
    ''' <param name="id">id del empleado</param>
    ''' <returns>Empleado</returns>
    ''' <remarks></remarks>
    Public Function GetEmployeeById(id As Integer, session As SessionValues) As Domain.Payroll.Entities.Employee Implements IPayrollEmployee.GetEmployeeById
        Using employeeAdmin As IEmployeeAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of IEmployeeAdminService)()
            Return employeeAdmin.GetEmployeeById(id)
        End Using
    End Function


    ''' <summary>
    ''' Obtiene un empleado y todos sus agregados atraves del id
    ''' </summary>
    ''' <param name="id">id del empleado</param>
    ''' <returns>Empleado</returns>
    ''' <remarks></remarks>
    Public Function GetEmployeeByIdForContractLiquidation(id As Integer, session As SessionValues) As Domain.Payroll.Entities.Employee Implements IPayrollEmployee.GetEmployeeByIdForContractLiquidation
        Using employeeAdmin As IEmployeeAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of IEmployeeAdminService)()
            Return employeeAdmin.GetEmployeeByIdForContractLiquidation(id)
        End Using
    End Function

    ''' <summary>
    ''' Obtiene un empleado y todos sus agregados atraves del id
    ''' </summary>
    ''' <param name="id">id del empleado</param>
    ''' <returns>Empleado</returns>
    ''' <remarks></remarks>
    Public Function GetEmployeePensionary(session As SessionValues) As List(Of Domain.Payroll.Entities.Employee) Implements IPayrollEmployee.GetEmployeePensionary
        Using employeeAdmin As IEmployeeAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of IEmployeeAdminService)()
            Return employeeAdmin.GetEmployeePensionary()
        End Using
    End Function
    ''' <summary>
    ''' Guarda la renta
    ''' </summary>
    ''' <param name="exemptIncome"></param>
    ''' <param name="session"></param>
    ''' <returns></returns>
    Public Function SaveExemptIncome(exemptIncome As List(Of Domain.Entities.ExemptIncome), session As SessionValues) As ActionMessageResult(Of Domain.Entities.ExemptIncome) Implements IPayrollEmployee.SaveExemptIncome
        Using employeeAdmin As IEmployeeAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of IEmployeeAdminService)()
            Return employeeAdmin.SaveExemptIncome(exemptIncome, session.AuditMessageWcf)
        End Using
    End Function
    ''' <summary>
    ''' Elimina la renta exenta
    ''' </summary>
    ''' <param name="exemptIncome"></param>
    ''' <param name="session"></param>
    ''' <returns></returns>
    Public Function DeleteExemptIncome(exemptIncome As List(Of Domain.Entities.ExemptIncome), session As SessionValues) As ActionMessageResult(Of Domain.Entities.ExemptIncome) Implements IPayrollEmployee.DeleteExemptIncome
        Using employeeAdmin As IEmployeeAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of IEmployeeAdminService)()
            Return employeeAdmin.DeleteExemptIncome(exemptIncome, session.AuditMessageWcf)
        End Using
    End Function


End Class
