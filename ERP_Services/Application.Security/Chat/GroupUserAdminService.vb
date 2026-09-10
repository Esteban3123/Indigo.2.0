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
Public Class GroupUserAdminService
    Implements IGroupUserAdminService

    Private _groupUserRepository As IGroupUserRepository

    ''' <summary>
    ''' Initializa una nueva instancia de la clase <see cref="GroupAdminService" />.
    ''' </summary>
    ''' <param name="groupUserRepository">Repositorio de grupos de usuario en el chat</param>
    Public Sub New(ByVal groupUserRepository As IGroupUserRepository)
        If groupUserRepository Is Nothing Then
            Throw New ArgumentNullException("groupUserRepository Empty")
        End If
        Me._groupUserRepository = groupUserRepository
    End Sub

#Region "GroupUser"

    ''' <summary>
    ''' Elimina un grupo del chat del usuario
    ''' </summary>
    ''' <param name="groupUser">Grupo a eliminar</param>
    ''' <returns>Resultado de la acción</returns>
    Public Function DeleteGroupUser(groupUser As GroupUser, audit As AuditMessage) As ActionResult Implements IGroupUserAdminService.DeleteGroupUser
        If groupUser Is Nothing Then
            Throw New ArgumentNullException("groupUser")
        End If
        Dim unitOfWork As IUnitWork = Me._groupUserRepository.UnitWork
        Try
            Me._groupUserRepository.DeleteEntity(groupUser)
            unitOfWork.Commit()
            Return New ActionResult() With {.StateResult = True}
        Catch ex As Exception
            unitOfWork.RollbackChanges()
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult() With {.StateResult = False}
        End Try
    End Function

    ''' <summary>
    ''' Guarda un grupo del chat del usuario
    ''' </summary>
    ''' <param name="groupUser">Grupo a guardar</param>
    ''' <returns>Resultado de la acción</returns>
    Public Function SaveGroupUser(groupUser As GroupUser, audit As AuditMessage) As ActionResult(Of GroupUser) Implements IGroupUserAdminService.SaveGroupUser
        If groupUser Is Nothing Then
            Throw New ArgumentNullException("groupUser")
        End If
        Dim unitOfWork As IUnitWork = Me._groupUserRepository.UnitWork
        Try
            Me._groupUserRepository.SaveEntity(groupUser)
            unitOfWork.Commit()
            Return New ActionResult(Of GroupUser) With {.ObjectEmbbeded = groupUser, .StateResult = True}
        Catch ex As Exception
            unitOfWork.RollbackChanges()
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of GroupUser) With {.ObjectEmbbeded = groupUser, .StateResult = False}
        End Try
    End Function

    ''' <summary>
    ''' Obtiene un grupo por su id
    ''' </summary>
    ''' <param name="id">Id del grupo</param>
    ''' <returns>El grupo buscado</returns>
    Public Function GetGroupById(id As Integer) As GroupUser Implements IGroupUserAdminService.GetGroupById
        Try
            Return Me._groupUserRepository.GetGroupById(id)
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return Nothing
        End Try
    End Function

    ''' <summary>
    ''' Lista los grupos del usuario friltrando por su id
    ''' </summary>
    ''' <param name="userId">Id del usuario</param>
    ''' <returns>Lista de grupos</returns>
    Public Function ListByUserId(userId As Integer) As List(Of GroupUser) Implements IGroupUserAdminService.ListByUserId
        Try
            Return Me._groupUserRepository.ListByUserId(userId)
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return Nothing
        End Try
    End Function

    ''' <summary>
    ''' Lista los grupos del usuario friltrando por su codigo
    ''' </summary>
    ''' <param name="userCode">Codigo del usuario</param>
    ''' <returns>Lista de grupos</returns>
    Public Function ListByUsercode(userCode As String) As List(Of GroupUser) Implements IGroupUserAdminService.ListByUsercode
        If String.IsNullOrEmpty(userCode.Trim()) Then
            Throw New ArgumentNullException("userCode Empty")
        End If
        Try
            Return Me._groupUserRepository.ListByUsercode(userCode.Trim())
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
            _groupUserRepository = Nothing
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
