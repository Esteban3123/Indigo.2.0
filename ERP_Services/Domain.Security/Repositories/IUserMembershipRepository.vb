'***********************************************************************
' Assembly         : Domain.Security
' Author           : WalterSierra
' Created          : 11-03-2011
'
' Last Modified By : Juan F. Tamayo
' Last Modified On : 2013-02-25
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************
#Region "Imports"
Imports System.Web.Security
#End Region
''' <summary>
''' Metodos y Funciones necesarias para el manejo de los usuarios en membership
''' </summary>
Public Interface IUserMembershipRepository
    ''' <summary>
    ''' Funcion para restaurar la contraseña de un usuario
    ''' </summary>
    ''' <param name="UserCode"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function ResetPassword(ByVal UserCode As String) As String

    ''' <summary>
    ''' Crear un usuario nuevo.
    ''' </summary>
    ''' <param name="codeUser">el codigo del usuario</param>
    ''' <param name="passwd">la contraseña encriptada en HASH.</param>
    ''' <param name="email">el email.</param>
    ''' <returns></returns>
    Function CreateUser(ByVal codeUser As String, ByVal passwd As String, ByVal email As String) As Boolean

    ''' <summary>
    ''' Eliminar un usuario
    ''' </summary>
    ''' <param name="codeUser">el codigo del usuario.</param>
    ''' <returns></returns>
    Function DeleteUser(ByVal codeUser As String) As Boolean

    ''' <summary>
    ''' Listar el usuario
    ''' </summary>
    ''' <param name="codeUser">el codigo del usuario</param>
    ''' <returns></returns>
    Function GetUser(ByVal codeUser As String) As MembershipUser

    ''' <summary>
    ''' Listar todos los usuarios
    ''' </summary>
    ''' <returns></returns>
    Function GetAllUsers() As MembershipUserCollection

    ''' <summary>
    ''' Listar los usuarios Online
    ''' </summary>
    ''' <returns></returns>
    Function GetTotalUsersOnline() As Integer

    ''' <summary>
    ''' Buscars los usuarios por Nombre
    ''' </summary>
    ''' <param name="name">el nombre del usuario.</param>
    ''' <returns></returns>
    Function FindUserByName(ByVal name As String) As MembershipUserCollection

    ''' <summary>
    ''' Buscar usuarios por email
    ''' </summary>
    ''' <param name="email">el email.</param>
    ''' <returns></returns>
    Function FindUserByEmail(ByVal email As String) As MembershipUserCollection

    ''' <summary>
    ''' Cambiar la contraseña
    ''' </summary>
    ''' <param name="codeUser">el codigo del usuario.</param>
    ''' <param name="lastPasswd">la contraseña anterior.</param>
    ''' <param name="newPasswd">la contraseña nueva.</param>
    ''' <returns></returns>
    Function ChangePassword(ByVal codeUser As String, ByVal lastPasswd As String, ByVal newPasswd As String) As Boolean

    ''' <summary>
    ''' Bloquear el usuario
    ''' </summary>
    ''' <param name="codeUser">el codigo del usuario.</param>
    ''' <returns></returns>
    Function UnlockUser(ByVal codeUser As String) As Boolean

    ''' <summary>
    ''' Validar el usuario
    ''' </summary>
    ''' <param name="codeUser">el codigo del usuario.</param>
    ''' <param name="passwd">la contraseña.</param>
    ''' <returns></returns>
    Function ValidateUser(ByVal codeUser As String, ByVal passwd As String) As Boolean

    ''' <summary>
    ''' Actualizar un usuario
    ''' </summary>
    ''' <param name="codeUser">el codigo del usuario.</param>
    ''' <param name="email">el email.</param>
    ''' <returns></returns>
    Function UpdateUser(ByVal codeUser As String, ByVal email As String) As Boolean

    ''' <summary>
    ''' Esta funcion Retorna el email del usuario.	
    ''' </summary>
    ''' <param name="codeUser">el codigo del usuario.</param>
    ''' <returns>retorna el email del usuario</returns>
    ''' <remarks></remarks>
    Function GetEmailUser(ByVal codeUser As String) As String

    ''' <summary>
    ''' Lista la contraseña del Usuario.
    ''' </summary>
    ''' <param name="codeUser">el codigo del usuario.</param>
    ''' <returns></returns>
    Function GetPasswordUser(ByVal codeUser As String) As String

End Interface
