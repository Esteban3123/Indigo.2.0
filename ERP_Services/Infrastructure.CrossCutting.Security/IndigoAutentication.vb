'***********************************************************************
' Assembly         : Infrastructure.CrossCutting.Security
' Author           : OscarSierra
' Created          : 03-08-2011
'
' Last Modified By : Juan F. Tamayo
' Last Modified On : 2013-02-25
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************
#Region "Imports"
Imports System.Web.Security
Imports Domain.Security
#End Region
''' <summary>
''' Esta clase contiene los metodos y funciones correspondientes al tema de 
''' autenticacion de la aplicacion haciendo uso de membership.
''' De modo que se puede configurar de dos formas para los tipos de autentiacion
''' la primera seria contra LDAP y la segunda opcion seria contra las tablas 
''' propietarias creadas por membership.
''' </summary>
Public NotInheritable Class IndigoAutentication
    Implements IUserMembershipRepository

    ''' <summary>
    ''' Cambia la contraseña del usuario
    ''' </summary>
    ''' <param name="userName">Nombre del usuario</param>
    ''' <param name="lastPasswd">Ultima contraseña del usuario</param>
    ''' <param name="newPasswd">Nueva contraseña del usuario</param>
    ''' <returns>Un valor que indica si se cambio la contraseña</returns>
    Public Function ChangePassword(userName As String, lastPasswd As String, newPasswd As String) As Boolean Implements IUserMembershipRepository.ChangePassword
        If (Not userName Is Nothing AndAlso String.IsNullOrEmpty(userName)) Or (Not lastPasswd Is Nothing AndAlso String.IsNullOrEmpty(lastPasswd)) Or (Not newPasswd Is Nothing AndAlso String.IsNullOrEmpty(newPasswd)) Then
            Return False
        End If
            Dim user As MembershipUser = Membership.GetUser(userName)
            user.ChangePassword(lastPasswd, newPasswd)
            Return True
    End Function

    ''' <summary>
    ''' Crea un nuevo usuario
    ''' </summary>
    ''' <param name="userName">Name of the user.</param>
    ''' <param name="passwd">The passwd.</param>
    ''' <param name="email">The email.</param>
    ''' <returns>Valor que indica si se creo el usuario</returns>
    Public Function CreateUser(userName As String, passwd As String, email As String) As Boolean Implements IUserMembershipRepository.CreateUser
        If (Not userName Is Nothing AndAlso String.IsNullOrEmpty(userName)) Or (Not passwd Is Nothing AndAlso String.IsNullOrEmpty(passwd)) Or (Not email Is Nothing AndAlso String.IsNullOrEmpty(email)) Then
            Return False
            Exit Function
        End If
        Dim status As MembershipCreateStatus
        Try
            Dim resultado As Boolean = False
            Membership.CreateUser(userName, passwd, email)
            If status = MembershipCreateStatus.Success Then
                resultado = True
                Return resultado
            Else
                Return resultado
            End If
        Catch ex As MembershipCreateUserException
            Throw New MembershipPasswordException(ex.Message)
        End Try
    End Function

    ''' <summary>
    ''' Elimina un usuario
    ''' </summary>
    ''' <param name="userName">Nombre del usuario</param>
    ''' <returns>Un valor que indica si el usuario se elimino</returns>
    Public Function DeleteUser(userName As String) As Boolean Implements IUserMembershipRepository.DeleteUser
        If (Not userName Is Nothing AndAlso String.IsNullOrEmpty(userName)) Then
            Return False
            Exit Function
        End If
        Try
            Membership.DeleteUser(userName)
            Return True
        Catch ex As Exception
            Return False
            Exit Function
        End Try
    End Function

    ''' <summary>
    ''' Consulta un usuario por su email
    ''' </summary>
    ''' <param name="email">email del usuario</param>
    ''' <returns>El usuario</returns>
    Public Function FindUserByEmail(email As String) As MembershipUserCollection Implements IUserMembershipRepository.FindUserByEmail
        Try
            Dim user As MembershipUserCollection = Membership.FindUsersByEmail(email)
            Return user
        Catch ex As Exception
            Return Nothing
            Exit Function
        End Try
    End Function

    ''' <summary>
    ''' Consulta un usuario por el nombre
    ''' </summary>
    ''' <param name="userName">Nombre de usuario</param>
    ''' <returns>El usuario</returns>
    Public Function FindUserByName(userName As String) As MembershipUserCollection Implements IUserMembershipRepository.FindUserByName
        Try
            Dim user As MembershipUserCollection = Membership.FindUsersByName(userName)
            Return user
        Catch ex As Exception
            Return Nothing
            Exit Function
        End Try
    End Function

    ''' <summary>
    ''' Obtiene una coleccion de todos los usuario
    ''' </summary>
    ''' <returns>Coleccion de usuarios</returns>
    Public Function GetAllUsers() As MembershipUserCollection Implements IUserMembershipRepository.GetAllUsers
        Try
            Dim usuario As MembershipUserCollection = Membership.GetAllUsers
            Return usuario
        Catch ex As Exception
            Return Nothing
            Exit Function
        End Try
    End Function

    ''' <summary>
    ''' Obtiene el numero total de usuarios en linea
    ''' </summary>
    ''' <returns>Numero total de usuarios en linea</returns>
    Public Function GetTotalUsersOnline() As Integer Implements IUserMembershipRepository.GetTotalUsersOnline
        Try
            If Membership.Provider.Name = "SqlProvider" Then
                Dim usuariosConectados As Integer = Membership.GetNumberOfUsersOnline
                Return usuariosConectados
            Else
                Return 0
            End If
        Catch ex As Exception
            Return Nothing
            Exit Function
        End Try
    End Function

    ''' <summary>
    ''' Obtiene un usuario por su nombre
    ''' </summary>
    ''' <param name="userName">Nombre de usuario</param>
    ''' <returns>Usuario</returns>
    Public Function GetUser(userName As String) As MembershipUser Implements IUserMembershipRepository.GetUser
        If (Not userName Is Nothing AndAlso String.IsNullOrEmpty(userName)) Then
            Return Nothing
            Exit Function
        End If
        Try
            Dim user As MembershipUser = Membership.GetUser(userName)
            Return user
        Catch ex As Exception
            Return Nothing
            Exit Function
        End Try
    End Function

    ''' <summary>
    ''' Actualiza un usuario
    ''' </summary>
    ''' <param name="userName">Nombre de usuario</param>
    ''' <param name="email">Email del usuario</param>
    ''' <returns>Valor que indica si se actualizo</returns>
    Public Function UpdateUser(userName As String, email As String) As Boolean Implements IUserMembershipRepository.UpdateUser
        If (Not userName Is Nothing AndAlso String.IsNullOrEmpty(userName)) Then
            Return False
            Exit Function
        End If
        Dim user As MembershipUser = Membership.GetUser(userName)
        user.Email = email
        Try
            Membership.UpdateUser(user)
            Return True
        Catch ex As Exception
            Return False
            Exit Function
        End Try
    End Function

    ''' <summary>
    ''' Obtiene el email del usuario
    ''' </summary>
    ''' <param name="userName">Nombre del usuario</param>
    ''' <returns>Email del usuario</returns>
    Public Function GetUserEmail(userName As String) As String Implements IUserMembershipRepository.GetEmailUser
        Try
            Dim user As MembershipUser = Membership.GetUser(userName)
            Return user.Email
        Catch ex As Exception
            Return Nothing
            Exit Function
        End Try
    End Function

    ''' <summary>
    ''' Desbloquea un usuario
    ''' </summary>
    ''' <param name="userName">Nombre del usuario</param>
    ''' <returns>Un valor que indica si se desbloqueo</returns>
    Public Function UserUnlock(userName As String) As Boolean Implements IUserMembershipRepository.UnlockUser
        If (Not userName Is Nothing AndAlso String.IsNullOrEmpty(userName)) Then
            Return False
            Exit Function
        End If
        Try
            Dim user As MembershipUser = Membership.GetUser(userName)
            Dim success As Boolean = user.UnlockUser
            Return success
        Catch ex As Exception
            Return False
            Exit Function
        End Try
    End Function

    ''' <summary>
    ''' Obtiene la contraseña del usuario
    ''' </summary>
    ''' <param name="userName">Nombre de usuario</param>
    ''' <returns>Contraseña del usuario</returns>
    Public Function GetUserPassword(userName As String) As String Implements IUserMembershipRepository.GetPasswordUser
        If (Not userName Is Nothing AndAlso String.IsNullOrEmpty(userName)) Then
            Return Nothing
            Exit Function
        End If
        Try
            Dim user As MembershipUser = Membership.GetUser(userName)
            Return user.ResetPassword
        Catch ex As Exception
            Return Nothing
            Exit Function
        End Try
    End Function

    ''' <summary>
    ''' Valida el usuario
    ''' </summary>
    ''' <param name="userName">Nombre de usuario</param>
    ''' <param name="passwd">Contraseña de usuario</param>
    ''' <returns>Un valor que indica si se valido el usuario</returns>
    Public Function ValidateUser(userName As String, passwd As String) As Boolean Implements IUserMembershipRepository.ValidateUser
        If Membership.ValidateUser(userName, passwd) Then
            Return True
        Else
            Return False
        End If
    End Function

    ''' <summary>
    ''' Funcion para restaurar la contraseña
    ''' </summary>
    ''' <param name="UserCode"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ResetPassword(UserCode As String) As String Implements IUserMembershipRepository.ResetPassword
        Try
            Dim user As MembershipUser = Membership.GetUser(UserCode)
            Return user.ResetPassword()
        Catch ex As Exception
            Return Nothing
            Exit Function
        End Try
    End Function

End Class
