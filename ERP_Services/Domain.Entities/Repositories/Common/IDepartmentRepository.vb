Imports Domain.Base
Imports Domain.Common.Entities

Public Interface IDepartmentRepository
    Inherits IRepository(Of Department)

    ''' <summary>
    ''' funcion la cual retorna todos los departamentos
    ''' </summary>
    ''' <returns>Lista de departamento</returns>
    ''' <remarks></remarks>
    Function ListAllDepartment() As List(Of Department)

    ''' <summary>
    '''  funcion el cual trae un departamento en especifico
    ''' </summary>
    ''' <param name="code">Codigo del departamento</param>
    ''' <returns>Departamento</returns>
    ''' <remarks></remarks>
    Function GetDepartment(ByVal code As String, ByVal IdCountry As String) As Department

    Function GetDepartmentById(ByVal idDepartment As Integer, Optional traking As Boolean = True) As Department

    Function GetDepartments(ByVal IdCountry As String) As List(Of Department)

End Interface
