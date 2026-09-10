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
Imports Domain.Security.Entities
Imports System.Transactions
Imports Domain.Security
Imports Domain.Base
Imports Domain.Base.Entities
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.CrossCutting.Exceptions
#End Region

''' <summary>
''' Servicio de chat
''' </summary>
Public Class UsersGroupUserAdminService
    Implements IUsersGroupUserAdminService

    Private _usersGroupUserRepository As IUsersGroupUserRepository

    ''' <summary>
    ''' Initializa una nueva instancia de la clase <see cref="GroupAdminService" />.
    ''' </summary>
    ''' <param name="usersGroupUserRepository">Repositorio de grupos de usuario en el chat</param>
    Public Sub New(ByVal usersGroupUserRepository As IUsersGroupUserRepository)
        If usersGroupUserRepository Is Nothing Then
            Throw New ArgumentNullException("usersGroupUserRepository Empty")
        End If
        Me._usersGroupUserRepository = usersGroupUserRepository
    End Sub

#Region "UsersGroupUser"

    ''' <summary>
    ''' Elimina un usuario relacionado a un grupo
    ''' </summary>
    ''' <param name="userGroupUser">Usuario relacionado</param>
    ''' <returns>Resultado de la acción</returns>
    Public Function DeleteUsersGroupUser(userGroupUser As UsersGroupUser, audit As AuditMessage) As ActionResult Implements IUsersGroupUserAdminService.DeleteUsersGroupUser
        If userGroupUser Is Nothing Then
            Throw New ArgumentNullException("userGroupUser")
        End If
        Dim unitOfWork As IUnitWork = Me._usersGroupUserRepository.UnitWork
        Try
            Me._usersGroupUserRepository.DeleteEntity(userGroupUser)
            unitOfWork.Commit()
            Return New ActionResult() With {.StateResult = True}
        Catch ex As Exception
            unitOfWork.RollbackChanges()
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult() With {.StateResult = False}
        End Try
    End Function

    ''' <summary>
    ''' Guarda un usuario relacionado a un grupo en el chat
    ''' </summary>
    ''' <param name="userGroupUser">Usuario relacionado</param>
    ''' <returns>Resultado de la acción</returns>
    Public Function SaveUsersGroupUser(userGroupUser As UsersGroupUser, audit As AuditMessage) As ActionResult(Of UsersGroupUser) Implements IUsersGroupUserAdminService.SaveUsersGroupUser
        If userGroupUser Is Nothing Then
            Throw New ArgumentNullException("userGroupUser")
        End If
        Dim unitOfWork As IUnitWork = Me._usersGroupUserRepository.UnitWork
        Try
            Me._usersGroupUserRepository.SaveEntity(userGroupUser)
            unitOfWork.Commit()
            Return New ActionResult(Of UsersGroupUser) With {.ObjectEmbbeded = userGroupUser, .StateResult = True}
        Catch ex As Exception
            unitOfWork.RollbackChanges()
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of UsersGroupUser) With {.ObjectEmbbeded = userGroupUser, .StateResult = False}
        End Try
    End Function

    ''' <summary>
    ''' Lista los usuarios pertenecientes al grupo filtrandolos por el id del grupo
    ''' </summary>
    ''' <param name="id">Id del grupo</param>
    ''' <returns>Lista de usuario del grupo</returns>
    Public Function ListByGroupId(id As Integer) As List(Of UsersGroupUser) Implements IUsersGroupUserAdminService.ListByGroupId
        Try
            Return Me._usersGroupUserRepository.ListByGroupId(id)
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return Nothing
        End Try
    End Function

    ''' <summary>
    ''' Lista los usuarios pertenecientes a un usuario filtrados por su Codigo
    ''' </summary>
    ''' <param name="userCode">Codigo del usuario propietario</param>
    ''' <returns>Lista de usuarios de los grupos del usuario</returns>
    Public Function ListByUserCode(userCode As String) As List(Of UsersGroupUser) Implements IUsersGroupUserAdminService.ListByUserCode
        If String.IsNullOrEmpty(userCode.Trim()) Then
            Throw New ArgumentNullException("userCode")
        End If
        Try
            Return Me._usersGroupUserRepository.ListByUserCode(userCode)
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return Nothing
        End Try
    End Function

    ''' <summary>
    ''' Lista los usuarios pertenecientes a un usuario filtrados por su Id
    ''' </summary>
    ''' <param name="userId">Id del usuario propietario</param>
    ''' <returns>Lista de usuarios de los grupos del usuario</returns>
    Public Function ListByUserId(userId As Integer) As List(Of UsersGroupUser) Implements IUsersGroupUserAdminService.ListByUserId
        Try
            Return Me._usersGroupUserRepository.ListByUserId(userId)
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return Nothing
        End Try
    End Function

    ''' <summary>
    ''' Lista los grupos de otros usuarios a los cuales pertenece el usuario consultado
    ''' </summary>
    ''' <param name="userId">Id del usuario</param>
    ''' <returns>Lista de usuarios de los grupos del usuario</returns>
    Public Function ListUserInGroupsById(ByVal userId As Integer) As List(Of UsersGroupUser) Implements IUsersGroupUserAdminService.ListUserInGroupsById
        Try
            Return Me._usersGroupUserRepository.ListUserInGroupsById(userId)
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return Nothing
        End Try
    End Function

#End Region

#Region "IDisposable Support"
    Private disposedValue As Boolean ' Para detectar llamadas redundantes

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not disposedValue Then
            If disposing Then

            End If
            _usersGroupUserRepository = Nothing
            IndigoGC.Execute()
        End If
        disposedValue = True
    End Sub

    ' Visual Basic agrega este código para implementar correctamente el patrón descartable.
    Public Sub Dispose() Implements IDisposable.Dispose
        Dispose(True)
        GC.SuppressFinalize(Me)
    End Sub
#End Region

End Class
