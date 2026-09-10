Imports Domain.Base
Imports Domain.Payroll.Entities

Public Interface IGroupRepository

    Inherits IRepository(Of Group)

    ''' <summary>
    ''' Lista Todos los Grupos
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function ListAllGroups() As List(Of Group)

    ''' <summary>
    ''' Obtiene un Grupo en Específico
    ''' </summary>
    ''' <param name="code">Código del Grupo</param>
    ''' <returns>Grupo</returns>
    ''' <remarks></remarks>
    Function GetGroup(ByVal code As String, Optional tracking As Boolean = True) As Group

    ''' <summary>
    ''' Obtiene un grupo en específico por Id
    ''' </summary>
    ''' <param name="groupId">Id del Grupo</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetGroupById(ByVal groupId As String) As Group

    ''' <summary>
    ''' Obtiene un grupo en específico por Id
    ''' </summary>
    ''' <param name="groupId">Id del Grupo</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetGroupSimpleById(ByVal groupId As Integer) As Group

    ''' <summary>
    ''' Obtiene unos grupos filtrado por empresa
    ''' </summary>
    ''' <param name="companyId">Id de la empresa</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetGroupsByCompanyId(ByVal companyId As String) As List(Of Group)

    ''' <summary>
    ''' Grupo y sus correspondientes liquidaciones
    ''' </summary>
    ''' <param name="GroupId">Id del Grupo</param>
    ''' <returns>Grupo</returns>
    ''' <remarks></remarks>
    Function GetGroupLiquidationById(ByVal GroupId As String) As List(Of Group)

    ''' <summary>
    ''' Lista todos los Grupos por Estado
    ''' </summary>
    ''' <param name="Status"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function ListGroupsByStatus(Status As Boolean) As List(Of Group)
End Interface
