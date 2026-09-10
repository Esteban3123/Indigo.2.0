'***********************************************************************
' Assembly         : DistributedServices.Payroll
' Author           : Cristhian Mauricio Salazar
' Created          : 26-09-2013
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Payroll.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities

<ServiceContract()> _
Public Interface IPayrollEmployeeType

    ''' <summary>
    ''' Elimina un tipo de empleado
    ''' </summary>
    ''' <param name="employeeType">Tipo de empleado</param>
    ''' <returns></returns>
    <OperationContract()> _
    Function DeleteEmployeeType(ByVal employeeType As EmployeeType, session As SessionValues) As ActionMessageResult(Of EmployeeType)

    ''' <summary>
    ''' Guarda o edita un Empleado y todos sus agregados
    ''' </summary>
    ''' <param name="employeeType">Empleado</param>
    ''' <returns></returns>
    <OperationContract()> _
    Function SaveEmployeeType(ByVal employeeType As EmployeeType, session As SessionValues) As Boolean

    ''' <summary>
    ''' Obtiene un tipo de empleado por codigo
    ''' </summary>
    ''' <param name="code">codigo tipo empleado</param>
    ''' <returns> Employee</returns>
    <OperationContract()> _
    Function GetEmployeeType(ByVal code As String, session As SessionValues) As EmployeeType

End Interface
