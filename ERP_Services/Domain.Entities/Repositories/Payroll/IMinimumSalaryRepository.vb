'***********************************************************************
' Assembly         : Domain.Payroll
' Author           : Juan Pablo Daza
' Created          : 26-10-2022
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************
Imports Domain.Base
Imports Domain.Entities
Public Interface IMinimumSalaryRepository
    Inherits IRepository(Of MinimumSalary)

    ''' <summary>
    ''' Lista todos los salarios minimos 
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function ListAllMinimumSalary() As List(Of MinimumSalary)

    ''' <summary>
    ''' Obtiene el año
    ''' </summary>
    ''' <param name="Year">año </param>
    ''' <returns>Year</returns>
    ''' <remarks></remarks>
    Function GetMinimunSalaryByYear(ByVal Year As String, Optional tracking As Boolean = True) As MinimumSalary

    ''' <summary>
    ''' Obtiene un Salario minimo por el identificador
    ''' </summary>
    ''' <param name="Id">The identifier.</param>
    ''' <returns></returns>
    Function GetMinimunSalaryById(ByVal Id As Integer, Optional tracking As Boolean = True) As MinimumSalary

End Interface
