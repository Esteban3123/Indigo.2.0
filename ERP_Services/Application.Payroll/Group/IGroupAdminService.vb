Imports Domain.Payroll.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities
Public Interface IGroupAdminService
    Inherits IDisposable

    ''' <summary>
    ''' Lista de Grupos
    ''' </summary>
    ''' <returns>Lista de Grupos</returns>
    Function ListAllGroups() As List(Of Group)

    ''' <summary>
    ''' Elimina un Grupo
    ''' </summary>
    ''' <param name="Group">Grupo</param>
    ''' <param name="audit">Objeto Auditoria</param>
    ''' <returns></returns>
    Function DeleteGroup(ByVal Group As Group, ByVal audit As AuditMessage) As ActionMessageResult(Of Group)

    ''' <summary>
    ''' Almacena o Edita un Grupo
    ''' </summary>
    ''' <param name="Group">Grupo</param>
    ''' <param name="audit">Objeto Auditoria</param>
    ''' <returns></returns>
    Function SaveGroup(ByVal Group As Group, ByVal audit As AuditMessage)

    ''' <summary>
    ''' Obtiene un Grupo
    ''' </summary>
    ''' <param name="code">Código del Grupo</param>
    ''' <returns></returns>
    Function GetGroup(ByVal code As String) As Group

    ''' <summary>
    ''' Obtiene un Grupo por Id
    ''' </summary>
    ''' <param name="groupId">Id del Grupo</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetGroupById(ByVal groupId As String) As Group

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

    Function GroupChangeState(code As String, state As Boolean, audit As Infrastructure.CrossCutting.Base.AuditMessage) As Boolean

    Function ListGroupsByStatus(Status As Boolean) As List(Of Group)
End Interface
