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
''' Repositorio de la entidad Grupos de usuario en el chat
''' </summary>
Public Class GroupUserRepository
    Inherits GenericRepository(Of GroupUser)
    Implements IGroupUserRepository

    'Devuelve el contexto en este repositorio 
    Private _context As ISeguridadUnitOfWork

    ''' <summary>
    '''inicializa la neva instancia de <see cref="GroupUserRepository" /> clase.
    ''' </summary>
    ''' <param name="contex">el contexto.</param>
    Public Sub New(ByVal contex As ISeguridadUnitOfWork)
        MyBase.New(contex)
        _context = contex
    End Sub

    ''' <summary>
    ''' Obtiene un grupo por su id
    ''' </summary>
    ''' <param name="id">Id del grupo</param>
    ''' <returns>El grupo buscado</returns>
    Public Function GetGroupById(id As Integer) As GroupUser Implements IGroupUserRepository.GetGroupById
        Dim result = (From g In Me._context.GroupUser.Include("User.Person").Include("UsersGroupUser.User.Person") Where g.Id = id Select g).ToList()
        If result IsNot Nothing AndAlso result.Count > 0 Then
            Return result(0)
        Else
            Return New GroupUser()
        End If
    End Function

    ''' <summary>
    ''' Lista los grupos del usuario friltrando por su codigo
    ''' </summary>
    ''' <param name="userCode">Codigo del usuario</param>
    ''' <returns>Lista de grupos</returns>
    Public Function ListByUsercode(userCode As String) As List(Of GroupUser) Implements IGroupUserRepository.ListByUsercode
        Dim result = (From g In Me._context.GroupUser.Include("User.Person").Include("UsersGroupUser.User.Person") Where g.User.UserCode = userCode.Trim() Select g).ToList()
        If result IsNot Nothing AndAlso result.Count > 0 Then
            Return result
        Else
            Return New List(Of GroupUser)()
        End If
    End Function

    ''' <summary>
    ''' Lista los grupos del usuario friltrando por su id
    ''' </summary>
    ''' <param name="userId">Id del usuario</param>
    ''' <returns>Lista de grupos</returns>
    Public Function ListByUserId(userId As Integer) As List(Of GroupUser) Implements IGroupUserRepository.ListByUserId
        Dim result = (From g In Me._context.GroupUser.Include("User.Person").Include("UsersGroupUser.User.Person") Where g.User.Id = userId Select g).ToList()
        If result IsNot Nothing AndAlso result.Count > 0 Then
            Return result
        Else
            Return New List(Of GroupUser)()
        End If
    End Function
End Class
