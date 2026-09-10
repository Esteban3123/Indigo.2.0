
Imports Domain.Base
Imports Domain.Entities
Imports Domain.Payroll.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities
Public Interface IMinimumSalaryAdminService
    Inherits IDisposable

    ''' <summary>
    ''' Lista todos los salarios minimos 
    ''' </summary>
    ''' <returns>Lista de salarios minimos </returns>
    Function ListAllMinimumSalary() As List(Of MinimumSalary)

    ''' <summary>
    ''' Elimina un salario minimo
    ''' </summary>
    ''' <param name="minimumSalary">salario minio</param>
    ''' <returns></returns>
    Function DeleteMinimunSalary(ByVal minimumSalary As MinimumSalary, ByVal audit As AuditMessage) As ActionMessageResult(Of MinimumSalary)

    ''' <summary>
    ''' Guarda o edita un salario minimo 
    ''' </summary>
    ''' <param name="minimumSalary">Banco</param>
    ''' <returns></returns>
    Function SaveMinimunSalary(ByVal minimumSalary As MinimumSalary, audit As AuditMessage, Optional idSequence As Int64 = 0) As ActionResult(Of MinimumSalary)
    ''' <summary>
    ''' Obtiene un actualiza un salario minimo
    ''' </summary>
    ''' <param name="year">año del salario minimo</param>
    ''' <returns> Salario minimo</returns>
    Function UpdateMinimunSalary(ByVal year As String, state As Boolean, audit As AuditMessage) As ActionResult(Of MinimumSalary)

    ''' <summary>
    ''' Obtiene un salario minimo por año
    ''' </summary>
    ''' <param name="year">Año</param>
    ''' <returns></returns>
    Function GetMinimunSalary(ByVal year As String, ByVal audit As AuditMessage) As MinimumSalary

    ''' <summary>
    ''' Obtiene un salario minimo por Id
    ''' </summary>
    ''' <param name="Id">The identifier.</param>
    ''' <returns></returns>
    Function GetMinimumSalaryById(ByVal Id As Integer) As MinimumSalary

End Interface
