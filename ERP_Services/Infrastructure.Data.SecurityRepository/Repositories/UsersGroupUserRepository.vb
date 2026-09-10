'***********************************************************************
' Assembly         : Infrastructure.Data.SecurityRepository
' Author           : Juan F. Tamayo
' Created          : 2013-10-30
'
' Last Modified By : Juan F. Tamayo
' Last Modified On : 2013-10-30
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"
Imports Domain.Base
Imports Domain.Security.Entities
Imports Domain.Security
Imports Infrastructure.Data.Base
#End Region

''' <summary>
''' Repositorio de la entidad Usuarios del grupo del usuario en el chat
''' </summary>
Public Class UsersGroupUserRepository
    Inherits GenericRepository(Of UsersGroupUser)
    Implements IUsersGroupUserRepository
    'Devuelve el contexto en este repositorio 
    Private _context As ISeguridadUnitOfWork

    ''' <summary>
    '''inicializa la neva instancia de <see cref="UsersGroupUserRepository" /> clase.
    ''' </summary>
    ''' <param name="contex">el contexto.</param>
    Public Sub New(ByVal contex As ISeguridadUnitOfWork)
        MyBase.New(contex)
        _context = contex
    End Sub

    ''' <summary>
    ''' Lista los usuarios pertenecientes al grupo filtrandolos por el id del grupo
    ''' </summary>
    ''' <param name="id">Id del grupo</param>
    ''' <returns>Lista de usuario del grupo</returns>
    Public Function ListByGroupId(id As Integer) As List(Of UsersGroupUser) Implements IUsersGroupUserRepository.ListByGroupId
        Dim result = (From u In Me._context.UsersGroupUser.Include("User").Include("GroupUser") Where u.IdGroupUser = id Select u).ToList()
        If result IsNot Nothing AndAlso result.Count > 0 Then
            Return result
        Else
            Return New List(Of UsersGroupUser)()
        End If
    End Function

    ''' <summary>
    ''' Lista los usuarios pertenecientes a un usuario filtrados por su Codigo
    ''' </summary>
    ''' <param name="userCode">Codigo del usuario propietario</param>
    ''' <returns>Lista de usuarios de los grupos del usuario</returns>
    Public Function ListByUserCode(userCode As String) As List(Of UsersGroupUser) Implements IUsersGroupUserRepository.ListByUserCode
        Dim result = (From u In Me._context.UsersGroupUser.Include("User").Include("GroupUser") Where u.GroupUser.User.UserCode = userCode.Trim() Select u).ToList()
        If result IsNot Nothing AndAlso result.Count > 0 Then
            Return result
        Else
            Return New List(Of UsersGroupUser)()
        End If
    End Function

    ''' <summary>
    ''' Lista los usuarios pertenecientes a un usuario filtrados por su Id
    ''' </summary>
    ''' <param name="userId">Id del usuario propietario</param>
    ''' <returns>Lista de usuarios de los grupos del usuario</returns>
    Public Function ListByUserId(userId As Integer) As List(Of UsersGroupUser) Implements IUsersGroupUserRepository.ListByUserId
        Dim result = (From u In Me._context.UsersGroupUser.Include("User").Include("GroupUser") Where u.GroupUser.User.Id = userId Select u).ToList()
        If result IsNot Nothing AndAlso result.Count > 0 Then
            Return result
        Else
            Return New List(Of UsersGroupUser)()
        End If
    End Function

    ''' <summary>
    ''' Lista los grupos de otros usuarios a los cuales pertenece el usuario consultado
    ''' </summary>
    ''' <param name="userId">Id del usuario</param>
    ''' <returns>Lista de usuarios de los grupos del usuario</returns>
    Public Function ListUserInGroupsById(userId As Integer) As List(Of UsersGroupUser) Implements IUsersGroupUserRepository.ListUserInGroupsById
        Dim result = (From u In Me._context.UsersGroupUser Where u.IdUser = userId Select u).ToList()
        If result IsNot Nothing AndAlso result.Count > 0 Then
            Return result
        Else
            Return New List(Of UsersGroupUser)()
        End If
    End Function
End Class
