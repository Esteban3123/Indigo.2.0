'***********************************************************************
' Assembly         : Application.Security
' Author           : Juan F. Tamayo
' Created          : 2013-10-30
'
' Last Modified By : Juan F. Tamayo
' Last Modified On : 2013-10-30
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"
Imports Domain.Base.Entities
Imports Domain.Security.Entities
Imports Infrastructure.CrossCutting.Base
#End Region

''' <summary>
''' Interface que define los metodos del servicio del chat
''' </summary>
Public Interface IUsersGroupUserAdminService
    Inherits IDisposable

#Region "UsersGroupUser"

    ''' <summary>
    ''' Guarda un usuario relacionado a un grupo en el chat
    ''' </summary>
    ''' <param name="userGroupUser">Usuario relacionado</param>
    ''' <returns>Resultado de la acción</returns>
    Function SaveUsersGroupUser(ByVal userGroupUser As UsersGroupUser, ByVal audit As AuditMessage) As ActionResult(Of UsersGroupUser)

    ''' <summary>
    ''' Elimina un usuario relacionado a un grupo
    ''' </summary>
    ''' <param name="userGroupUser">Usuario relacionado</param>
    ''' <returns>Resultado de la acción</returns>
    Function DeleteUsersGroupUser(ByVal userGroupUser As UsersGroupUser, ByVal audit As AuditMessage) As ActionResult

    ''' <summary>
    ''' Lista los usuarios pertenecientes al grupo filtrandolos por el id del grupo
    ''' </summary>
    ''' <param name="id">Id del grupo</param>
    ''' <returns>Lista de usuario del grupo</returns>
    Function ListByGroupId(id As Integer) As List(Of UsersGroupUser)

    ''' <summary>
    ''' Lista los usuarios pertenecientes a un usuario filtrados por su Codigo
    ''' </summary>
    ''' <param name="userCode">Codigo del usuario propietario</param>
    ''' <returns>Lista de usuarios de los grupos del usuario</returns>
    Function ListByUserCode(userCode As String) As List(Of UsersGroupUser)

    ''' <summary>
    ''' Lista los usuarios pertenecientes a un usuario filtrados por su Id
    ''' </summary>
    ''' <param name="userId">Id del usuario propietario</param>
    ''' <returns>Lista de usuarios de los grupos del usuario</returns>
    Function ListByUserId(userId As Integer) As List(Of UsersGroupUser)

    ''' <summary>
    ''' Lista los grupos de otros usuarios a los cuales pertenece el usuario consultado
    ''' </summary>
    ''' <param name="userId">Id del usuario</param>
    ''' <returns>Lista de usuarios de los grupos del usuario</returns>
    Function ListUserInGroupsById(ByVal userId As Integer) As List(Of UsersGroupUser)

#End Region

End Interface
