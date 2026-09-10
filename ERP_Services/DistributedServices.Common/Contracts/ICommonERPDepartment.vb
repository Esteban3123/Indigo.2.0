Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities

<ServiceContract()> _
Public Interface ICommonERPDepartment

#Region "Department"

    ''' <summary>
    ''' Obtiene todos los Departamentos
    ''' </summary>
    ''' <returns>Lista de Departamentos</returns>
    <OperationContract()>
    Function ListAllDepartment(session As SessionValues) As List(Of Department)

    ''' <summary>
    ''' Obtiene un Departamento especifico
    ''' </summary>
    ''' <param name="code">Codigo del Departmento</param>
    ''' <returns>Departmento</returns>
    <OperationContract()>
    Function GetDepartment(ByVal code As String, ByVal idCountry As String, session As SessionValues) As Department

    ''' <summary>
    ''' Obtiene una lista Departamentos por pais
    ''' </summary>
    ''' <param name="IdCountry">Codigo del Departmento</param>
    ''' <returns>Departmento</returns>
    <OperationContract()>
    Function GetDepartments(ByVal idCountry As String, session As SessionValues) As List(Of Department)

    ''' <summary>
    ''' Graba un Departmento
    ''' </summary>
    ''' <param name="department">Departmento a grabar</param>
    ''' <param name="audit">Objeto para la Auditoria</param>
    ''' <returns>Retorna un boleano: 1. Si es exitoso, 0. si no lo fue</returns>
    <OperationContract()>
    Function SaveDepartment(ByVal department As Department, session As SessionValues) As ActionResult(Of Department)

    ''' <summary>
    ''' Elimina un Departmento
    ''' </summary>
    ''' <param name="department">Departmento que se desa eliminar</param>
    ''' <param name="audit">Objeto para la Auditoria</param>
    ''' <returns>Retorna un boleano: 1. Si es exitoso el borrado, 0. si no lo fue</returns>
    <OperationContract()>
    Function DeleteDepartment(ByVal department As Department, session As SessionValues) As ActionMessageResult(Of Department)

    <OperationContract()>
    Function GetDepartmentById(ByVal idDepartment As Integer, session As SessionValues) As Department
    ''' <summary>
    ''' Metodo que cambia el estado del registro
    ''' </summary>
    ''' <param name="code">Codigo Ciudad</param>
    ''' <returns>booleano</returns>
    ''' <remarks></remarks>
    <OperationContract()>
    Function ChangeStateDepartment(code As String, IdCountry As String, state As Boolean, session As Infrastructure.CrossCutting.Base.SessionValues) As ActionResult(Of Department)

#End Region

End Interface
