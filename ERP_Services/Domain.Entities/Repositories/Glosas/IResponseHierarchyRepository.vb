Imports Domain.Entities
Imports Domain.Base
Public Interface IResponseHierarchyRepository
    Inherits IRepository(Of GlosasResponseHierarchy)

    ''' <summary>
    ''' Lists the GlosasResponseHierarchy
    ''' </summary>
    ''' <returns></returns>
    Function ListResponseHierarchy() As List(Of GlosasResponseHierarchy)
    ''' <summary>
    ''' consulta un Responsible especifico
    ''' </summary>
    ''' <param name="Code">el codigo del Responsible</param>
    ''' <returns></returns>
    Function GetResponseHierarchy(ByVal Code As String) As GlosasResponseHierarchy
    ''' <summary>
    ''' consulta una jerarquía de respuesta especifico
    ''' </summary>
    ''' <param name="id">Id</param>
    ''' <returns></returns>
    Function GetResponseHierarchyById(ByVal id As Integer) As GlosasResponseHierarchy

End Interface
