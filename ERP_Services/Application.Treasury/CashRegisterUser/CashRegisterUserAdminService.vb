'***********************************************************************
' Assembly         : Application.Treasury
' Author           : Diego Andrés Roldán Lozano
' Created          : 07-05-2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Entities
Imports Domain.Base
Imports Infrastructure.CrossCutting.Exceptions
Imports Application.Base
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities
Imports System.Data.Entity.Infrastructure
Imports System.Data.Entity.Core
Imports Application.Security


Public Class CashRegisterUserAdminService
    Implements ICashRegisterUserAdminService

#Region "Fields"

    ''' <summary>
    ''' Repositorio de tarjetas
    ''' </summary>
    Private _cashRegisterUserRepository As ICashRegisterUserRepository

    ''' <summary>
    ''' Aplicación de usuarios
    ''' </summary>
    ''' <remarks></remarks>
    Private _IUserAdminService As IUserAdminService

#End Region

    Public Sub New(ByVal cashRegisterUserRepository As ICashRegisterUserRepository, IUserAdminService As IUserAdminService)
        If cashRegisterUserRepository Is Nothing Then
            Throw New ArgumentNullException("cashRegisterUserRepository")
        End If
        If IUserAdminService Is Nothing Then
            Throw New ArgumentNullException("IUserAdminService")
        End If
        _cashRegisterUserRepository = cashRegisterUserRepository
        _IUserAdminService = IUserAdminService
    End Sub

    ''' <summary>
    ''' Gets the cash register user by identifier.
    ''' </summary>
    ''' <param name="Id">The identifier.</param>
    ''' <param name="audit">The audit.</param>
    ''' <returns></returns>
    ''' <exception cref="System.ArgumentNullException">
    ''' Id
    ''' or
    ''' audit
    ''' </exception>
    Public Function GetCashRegisterUserById(Id As Integer, audit As AuditMessage) As CashRegisterUser Implements ICashRegisterUserAdminService.GetCashRegisterUserById
        If String.IsNullOrEmpty(Id) Then
            Throw New ArgumentNullException("Id")
        End If
        If audit Is Nothing Then
            Throw New ArgumentNullException("audit")
        End If
        Try
            Dim cashRegisterUser As CashRegisterUser = Me._cashRegisterUserRepository.GetCashRegisterUserById(Id)
            Return cashRegisterUser
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return Nothing
        End Try
    End Function

    ''' <summary>
    ''' Lists the cash register user by identifier cash register.
    ''' </summary>
    ''' <param name="IdCashRegister">The identifier cash register.</param>
    ''' <param name="audit">The audit.</param>
    ''' <returns></returns>
    ''' <exception cref="System.ArgumentNullException">
    ''' IdCashRegister
    ''' or
    ''' audit
    ''' </exception>
    Public Function ListCashRegisterUserByIdCashRegister(IdCashRegister As Integer, audit As AuditMessage) As List(Of CashRegisterUser) Implements ICashRegisterUserAdminService.ListCashRegisterUserByIdCashRegister
        If String.IsNullOrEmpty(IdCashRegister) Then
            Throw New ArgumentNullException("IdCashRegister")
        End If
        If audit Is Nothing Then
            Throw New ArgumentNullException("audit")
        End If
        Try
            Dim ListCashRegisterUser As List(Of CashRegisterUser) = Me._cashRegisterUserRepository.ListCashRegisterUserByIdCashRegister(IdCashRegister)
            Return ListCashRegisterUser
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return Nothing
        End Try
    End Function

    ''' <summary>
    ''' Lista los usuarios permitidos por una caja
    ''' </summary>
    ''' <param name="CashRegisterId"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListUsersByCashRegisterId(CashRegisterId As Integer, audit As AuditMessage) As ActionResult(Of List(Of Domain.Security.Entities.User)) Implements ICashRegisterUserAdminService.ListUsersByCashRegisterId
        If CashRegisterId = 0 Then
            Throw New ArgumentNullException("CashRegisterId")
        End If
        Try
            'Listado de usuarios que se va a retornar
            Dim ListUsers As New List(Of Domain.Security.Entities.User)

            'Se consulta el listado de usuarios al cual la caja tengan permiso
            Dim ListCashRegisterUser As List(Of CashRegisterUser) = Me._cashRegisterUserRepository.ListCashRegisterUserByIdCashRegister(CashRegisterId)
            If ListCashRegisterUser IsNot Nothing AndAlso ListCashRegisterUser.Count > 0 Then
                Dim listUserIds As New List(Of Integer)()
                For Each u In ListCashRegisterUser
                    listUserIds.Add(u.IdUser)
                Next

                Dim user = _IUserAdminService.ListUsersByIds(listUserIds)
                If user IsNot Nothing AndAlso user.Count > 0 Then
                    ListUsers.AddRange(user)
                End If
            End If
            Return New ActionResult(Of List(Of Domain.Security.Entities.User)) With {.StateResult = True, .ObjectEmbbeded = ListUsers}
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of List(Of Domain.Security.Entities.User)) With {.StateResult = False}
        End Try
    End Function

#Region "IDisposable Support"
    Private disposedValue As Boolean ' Para detectar llamadas redundantes

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not disposedValue Then
            If disposing Then

            End If
            _cashRegisterUserRepository = Nothing
            _IUserAdminService = Nothing
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
