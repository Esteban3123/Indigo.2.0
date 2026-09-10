Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities

Public Interface IDepartmentAdminService
    Inherits IDisposable

    ''' <summary>
    ''' Lista todos los Departamentos.
    ''' </summary>
    ''' <returns></returns>
    Function ListAllDepartment() As List(Of Department)

    ''' <summary>
    ''' Elimina un Departamento
    ''' </summary>
    ''' <param name="department">el Departamento</param>
    ''' <returns></returns>
    Function DeleteDepartment(ByVal department As Department, ByVal audit As AuditMessage) As ActionMessageResult(Of Department)
    ''' <summary>
    ''' graba un Departamento
    ''' </summary>
    ''' <param name="department">el Departamento</param>
    ''' <returns></returns>
    Function SaveDepartment(ByVal department As Department, ByVal audit As AuditMessage) As ActionResult(Of Department)
    ''' <summary>
    ''' consulta un Departamento
    ''' </summary>
    ''' <param name="code">el codigo del Departamento</param>
    ''' <returns></returns>
    Function GetDepartment(ByVal code As String, ByVal IdCountry As String) As Department

    ''' <summary>
    ''' Devuelve un departamento por id
    ''' </summary>
    ''' <param name="idDepartment">id de departamento</param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetDepartmentById(ByVal idDepartment As Integer, audit As Infrastructure.CrossCutting.Base.AuditMessage) As Department

    ''' <summary>
    ''' consulta lista de Departamentos
    ''' </summary>
    ''' <param name="code">el codigo del Departamento</param>
    ''' <returns></returns>
    Function GetDepartments(ByVal IdCountry As String) As List(Of Department)

    ''' <summary>
    ''' Metodo que cambia el estado del registro
    ''' </summary>
    ''' <param name="code">Codigo Ciudad</param>
    ''' <returns>booleano</returns>
    ''' <remarks></remarks>
    Function ChangeStateDepartment(code As String, ByVal IdCountry As String, state As Boolean, audit As AuditMessage) As ActionResult(Of Department)

End Interface
