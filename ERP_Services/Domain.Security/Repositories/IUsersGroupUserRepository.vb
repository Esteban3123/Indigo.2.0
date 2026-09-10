'***********************************************************************
' Assembly         : Domain.Security
' Author           : Juan F. Tamayo
' Created          : 2013-10-30
'
' Last Modified By : Juan F. Tamayo
' Last Modified On : 2013-10-30
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"
Imports Domain.Security.Entities
Imports Domain.Base
#End Region

''' <summary>
''' Repositorio de los usuarios pertenecientes a un grupo en el chat de un usuario
''' </summary>
Public Interface IUsersGroupUserRepository
    Inherits IRepository(Of UsersGroupUser)

    ''' <summary>
    ''' Lista los usuarios pertenecientes al grupo filtrandolos por el id del grupo
    ''' </summary>
    ''' <param name="id">Id del grupo</param>
    ''' <returns>Lista de usuario del grupo</returns>
    Function ListByGroupId(ByVal id As Integer) As List(Of UsersGroupUser)

    ''' <summary>
    ''' Lista los usuarios pertenecientes a un usuario filtrados por su Codigo
    ''' </summary>
    ''' <param name="userCode">Codigo del usuario propietario</param>
    ''' <returns>Lista de usuarios de los grupos del usuario</returns>
    Function ListByUserCode(ByVal userCode As String) As List(Of UsersGroupUser)

    ''' <summary>
    ''' Lista los usuarios pertenecientes a un usuario filtrados por su Id
    ''' </summary>
    ''' <param name="userId">Id del usuario propietario</param>
    ''' <returns>Lista de usuarios de los grupos del usuario</returns>
    Function ListByUserId(ByVal userId As Integer) As List(Of UsersGroupUser)

    ''' <summary>
    ''' Lista los grupos de otros usuarios a los cuales pertenece el usuario consultado
    ''' </summary>
    ''' <param name="userId">Id del usuario</param>
    ''' <returns>Lista de usuarios de los grupos del usuario</returns>
    Function ListUserInGroupsById(ByVal userId As Integer) As List(Of UsersGroupUser)


End Interface
