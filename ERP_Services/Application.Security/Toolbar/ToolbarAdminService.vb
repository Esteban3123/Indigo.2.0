'***********************************************************************
' Assembly         : Application.Security
' Author           : WalterSierra
' Created          : 11-03-2011
'
' Last Modified By : Juan F. Tamayo
' Last Modified On : 2013-03-04
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"
Imports Domain.Security.Entities
Imports System.Transactions
Imports Domain.Security
Imports Domain.Base
Imports Domain.Base.Entities
Imports Infrastructure.CrossCutting.Exceptions
Imports Infrastructure.CrossCutting.Base
#End Region

''' <summary>
''' 	
''' </summary>
Public Class ToolbarAdminService
    Implements IToolbarAdminService

    Private _listPermissionsUser As List(Of PermissionUser)
    Private _listPermissionsRole As List(Of PermissionRoll)
    Private _listPermissionsUserToolbar As List(Of PermissionUserToolbar)
    Private _listPermissionsFormsActive As List(Of PermissionsFormsActive)

    Private _toolbarRepository As IToolbarRepository
    Private _rolesRepository As IRoleRepository
    Private _usersRepository As IUserRepository

    ''' <summary>
    ''' Initializa una nueva instancia de la clase <see cref="ToolbarAdminService" />.
    ''' </summary>
    ''' <param name="rolesRepository">el repositorio para el manejo de los usuarios.</param>
    Public Sub New(ByVal rolesRepository As IRoleRepository, usersRepository As IUserRepository, toolbarRepository As IToolbarRepository)
        If rolesRepository Is Nothing Then
            Throw New ArgumentNullException("rolesRepository Vacio")
        End If
        If toolbarRepository Is Nothing Then
            Throw New ArgumentNullException("toolbarRepository Vacio")
        End If
        If usersRepository Is Nothing Then
            Throw New ArgumentNullException("usersRepository Vacio")
        End If
        _rolesRepository = rolesRepository
        _usersRepository = usersRepository
        _toolbarRepository = toolbarRepository
    End Sub

    ''' <summary>
    ''' Lists the permissions user toolbar.	
    ''' </summary>
    ''' <param name="codeUser">The code user.</param>
    ''' <param name="codeRole">The code role.</param>
    ''' <param name="codeMenu">The code menu.</param>
    ''' <param name="tenantId">id de tenant.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListPermissionsUserToolbar(codeUser As String, codeRole As String, codeMenu As String, codeCompany As String, tenantId As Short) As List(Of PermissionUserToolbar) Implements IToolbarAdminService.ListPermissionsUserToolbar
        If String.IsNullOrEmpty(codeUser) = True Then
            Throw New ArgumentNullException("codeUser Vacio")
        End If
        If String.IsNullOrEmpty(codeRole) = True Then
            Throw New ArgumentNullException("codeRole Vacio")
        End If
        If String.IsNullOrEmpty(codeMenu) = True Then
            Throw New ArgumentNullException("codeMenu Vacio")
        End If

        Dim permissionsUserToolbar As PermissionUserToolbar
        Dim hasReg As Boolean = False

        Try
            'consulta permisos para el rol (solo autorizados)
            _listPermissionsRole = _rolesRepository.GetPermissionsRole(codeRole, codeMenu, True)
            'consulta los permisos del usuario
            _listPermissionsUser = _usersRepository.ListPermissionsUser(codeUser, codeMenu, True, tenantId)
            'crea la lista a devolver
            _listPermissionsUserToolbar = New List(Of PermissionUserToolbar)
            For usu As Integer = 0 To _listPermissionsUser.Count - 1
                'cargo en la lista primero lo permisos del rol
                For role As Integer = 0 To _listPermissionsRole.Count - 1
                    If _listPermissionsUser.Item(usu).Action.ToString.Trim = _listPermissionsRole.Item(role).Action.ToString.Trim Then
                        hasReg = True
                        Exit For
                    Else
                        hasReg = False
                    End If
                Next
                'cuando el rol y el usuario no tienen el mismo permiso, 
                'lo adiciono a la lista de permisos de la barra de usuarios
                If hasReg = False Then
                    permissionsUserToolbar = New PermissionUserToolbar
                    permissionsUserToolbar.TagButton = CInt(_listPermissionsUser.Item(usu).Action)
                    _listPermissionsUserToolbar.Add(permissionsUserToolbar)
                End If
            Next
            'adicionar los pemrisos encontrados en el rol
            ' a la lista de permisos de la barra
            For role As Integer = 0 To _listPermissionsRole.Count - 1
                permissionsUserToolbar = New PermissionUserToolbar
                permissionsUserToolbar.TagButton = CInt(_listPermissionsRole.Item(role).Action)
                _listPermissionsUserToolbar.Add(permissionsUserToolbar)
            Next
            Return _listPermissionsUserToolbar
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "UIPolicy")
            Return Nothing
        End Try
    End Function

    ''' <summary>
    ''' Lists the permissions user toolbar.	
    ''' </summary>
    ''' <param name="codeUser">The code user.</param>
    ''' <param name="codeRole">The code role.</param>
    ''' <returns>Lists of permissionsFormsActive</returns>
    Public Function ListPermissionsFormsActive(codeUser As String, codeRole As String, codeCompany As String) As List(Of PermissionsFormsActive) Implements IToolbarAdminService.ListPermissionsFormsActive

        If String.IsNullOrEmpty(codeUser) = True Then
            Throw New ArgumentNullException("codeUser Vacio")
        End If
        If String.IsNullOrEmpty(codeRole) = True Then
            Throw New ArgumentNullException("codeRole Vacio")
        End If
        If String.IsNullOrEmpty(codeCompany) = True Then
            Throw New ArgumentNullException("codeCompany Vacio")
        End If

        Dim permissionsFormsActive As PermissionsFormsActive
        Dim hasReg As Boolean = False

        Try
            'consulta permisos para el rol (solo autorizados)
            _listPermissionsRole = _rolesRepository.ListPermissionsRolAuthorized(codeRole, True)
            'consulta los permisos del usuario
            _listPermissionsUser = _usersRepository.ListPermissionsUserAuthorized(codeUser, True)
            'crea la lista a devolver
            _listPermissionsFormsActive = New List(Of PermissionsFormsActive)
            'adicionar los permisos encontrados en el rol
            ' a la lista de permisos de la barra
            For role As Integer = 0 To _listPermissionsRole.Count - 1
                'If CInt(_listPermissionsRole.Item(role).Action) = 41 Then 'si tiene el permiso de visualizar
                'If CInt(_listPermissionsRole.Item(role).Action) = 40 Then
                '   permissionsFormsActive.EnableEmbedded = True
                'Else
                '   permissionsFormsActive.EnableEmbedded = False
                'End If
                Dim permissionRole = _listPermissionsRole(role)
                Dim queryPermissionForm = From e In _listPermissionsFormsActive
                                          Where e.FormTag = permissionRole.IdForm
                                          Select e
                If queryPermissionForm.Count > 0 Then
                    Dim permissionFormActive = queryPermissionForm.SingleOrDefault()
                    permissionFormActive.ListActions.Add(permissionRole.Action)
                Else
                    permissionsFormsActive = New PermissionsFormsActive
                    permissionsFormsActive.ListActions = New List(Of String)()
                    permissionsFormsActive.ListActions.Add(permissionRole.Action)
                    permissionsFormsActive.FormTag = permissionRole.IdForm
                    _listPermissionsFormsActive.Add(permissionsFormsActive)
                End If
                'End If
            Next
            '*** Agrego los permisos de los usuarios
            For usu As Integer = 0 To _listPermissionsUser.Count - 1
                Dim permissionUser = _listPermissionsUser.Item(usu)
                Dim queryPermissionForm = From e In _listPermissionsFormsActive
                                          Where e.FormTag = permissionUser.IdForm
                                          Select e
                If queryPermissionForm.Count > 0 Then
                    Dim permissionFormActive = queryPermissionForm.SingleOrDefault()
                    permissionFormActive.ListActions.Add(permissionUser.Action)
                Else
                    permissionsFormsActive = New PermissionsFormsActive
                    permissionsFormsActive.ListActions = New List(Of String)()
                    permissionsFormsActive.ListActions.Add(permissionUser.Action)
                    permissionsFormsActive.FormTag = permissionUser.IdForm
                    _listPermissionsFormsActive.Add(permissionsFormsActive)
                End If
            Next
            Return _listPermissionsFormsActive
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return Nothing
        End Try
    End Function

#Region "IDisposable Support"
    Private disposedValue As Boolean ' Para detectar llamadas redundantes

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not disposedValue Then
            If disposing Then

            End If
            _rolesRepository = Nothing
            _usersRepository = Nothing
            _toolbarRepository = Nothing
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
