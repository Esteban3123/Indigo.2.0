#Region "Imports"
Imports System.ServiceModel
Imports Domain.Base.Entities
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
#End Region

<ServiceContract()>
Public Interface IPayrollMinimumSalary
#Region "MinimumSalary"

    ''' <summary>
    ''' Lista todos los Salarios Minimos
    ''' </summary>
    ''' <returns>Lista todos los Salarios Minimos</returns>
    ''' <remarks></remarks>
    <OperationContract()>
    Function ListAllMinimumsalary(ByVal session As SessionValues) As List(Of MinimumSalary)
    ''' <summary>
    ''' Elimina un salario minimo
    ''' </summary>
    ''' <param name="minimumSalary">salario minio</param>
    ''' <returns></returns>
    <OperationContract()>
    Function DeleteMinimunSalary(ByVal minimumSalary As MinimumSalary, session As SessionValues) As ActionMessageResult(Of MinimumSalary)
    ''' <summary>
    ''' Guarda o edita un salario minimo 
    ''' </summary>
    ''' <param name="minimumSalary">Banco</param>
    ''' <returns></returns>
    <OperationContract()>
    Function SaveMinimunSalary(ByVal minimumSalary As Domain.Entities.MinimumSalary, ByVal session As SessionValues, idSequense As Int64, audit As AuditMessage) As ActionResult(Of MinimumSalary)
    ''' <summary>
    ''' Actualiza un salario minimo
    ''' </summary>
    ''' <param name="year">año del salario minimo</param>
    ''' <returns> Salario minimo</returns>
    <OperationContract()>
    Function UpdateMinimunSalary(year As String, state As Boolean, session As SessionValues) As ActionResult(Of MinimumSalary)
    ''' <summary>
    ''' Obtiene un salario minimo por año
    ''' </summary>
    ''' <param name="year">Año</param>
    ''' <returns></returns>
    <OperationContract()>
    Function GetMinimunSalary(ByVal year As String, session As SessionValues) As MinimumSalary
    ''' <summary>
    ''' Obtiene un salario minimo por Id
    ''' </summary>
    ''' <param name="Id">The identifier.</param>
    ''' <returns></returns>
    <OperationContract()>
    Function GetMinimumSalaryById(ByVal Id As Integer, session As SessionValues) As MinimumSalary
#End Region

End Interface
