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
    ''' Elimina un tipo de empleado
    ''' </summary>
    ''' <param name="employeeType">Tipo de empleado</param>
    ''' <returns></returns>
    Public Function DeleteEmployeeType(employeeType As Domain.Payroll.Entities.EmployeeType, session As SessionValues) As ActionMessageResult(Of Domain.Payroll.Entities.EmployeeType) Implements IPayrollEmployeeType.DeleteEmployeeType
        Using employeeTypeAdmin As IEmployeeTypeAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of IEmployeeTypeAdminService)()
            Return employeeTypeAdmin.DeleteEmployeeType(employeeType, session.AuditMessageWcf)
        End Using
    End Function

    ''' <summary>
    ''' Obtiene un tipo de empleado por codigo
    ''' </summary>
    ''' <param name="code">codigo tipo empleado</param>
    ''' <returns> Employee</returns>
    Public Function GetEmployeeType(code As String, session As SessionValues) As Domain.Payroll.Entities.EmployeeType Implements IPayrollEmployeeType.GetEmployeeType
        Using employeeTypeAdmin As IEmployeeTypeAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of IEmployeeTypeAdminService)()
            Return employeeTypeAdmin.GetEmployeeType(code)
        End Using
    End Function

    ''' <summary>
    ''' Guarda o edita un Empleado y todos sus agregados
    ''' </summary>
    ''' <param name="employeeType">Empleado</param>
    ''' <returns></returns>
    Public Function SaveEmployeeType(employeeType As Domain.Payroll.Entities.EmployeeType, session As SessionValues) As Boolean Implements IPayrollEmployeeType.SaveEmployeeType
        Using employeeTypeAdmin As IEmployeeTypeAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of IEmployeeTypeAdminService)()
            Return employeeTypeAdmin.SaveEmployeeType(employeeType, session.AuditMessageWcf)
        End Using
    End Function
End Class
