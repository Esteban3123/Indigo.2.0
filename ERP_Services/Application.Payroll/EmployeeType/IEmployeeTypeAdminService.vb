'***********************************************************************
' Assembly         : Application.Payroll
' Author           : Cristhian Mauricio Salazar
' Created          : 26-09-2013
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Payroll.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities

Public Interface IEmployeeTypeAdminService
    Inherits IDisposable

    ''' <summary>
    ''' Elimina un tipo de empleado
    ''' </summary>
    ''' <param name="employeeType">Tipo de empleado</param>
    ''' <returns></returns>
    Function DeleteEmployeeType(ByVal employeeType As EmployeeType, ByVal audit As AuditMessage) As ActionMessageResult(Of EmployeeType)

    ''' <summary>
    ''' Guarda o edita un Empleado y todos sus agregados
    ''' </summary>
    ''' <param name="employeeType">Empleado</param>
    ''' <returns></returns>
    Function SaveEmployeeType(ByVal employeeType As EmployeeType, ByVal audit As AuditMessage) As Boolean

    ''' <summary>
    ''' Obtiene un tipo de empleado por codigo
    ''' </summary>
    ''' <param name="code">codigo tipo empleado</param>
    ''' <returns> Employee</returns>
    Function GetEmployeeType(ByVal code As String) As EmployeeType

End Interface
