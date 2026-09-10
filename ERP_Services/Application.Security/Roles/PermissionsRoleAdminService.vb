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
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.CrossCutting.Exceptions
Imports Domain.Crystal
Imports Domain.Crystal.Entities
#End Region

''' <summary>
''' 	
''' </summary>
Public Class PermissionsRoleAdminService
    Implements IPermissionsRoleAdminService

    Private _TenantRollRepository As IRepository(Of TenantRoll)
    Private _permissionsRoleRepository As IRepository(Of PermissionRoll)
    Private _rolesRepository As IRoleRepository

    'EHR
    Dim _SEGpermirRepository As ISEGpermirRepository

    ''' <summary>
    ''' Initializa una nueva instancia de la clase <see cref="PermissionsRoleAdminService" />.
    ''' </summary>
    ''' <param name="permissionsRoleRepository"></param>
    ''' <param name="rolesRepository"></param>
    ''' <param name="SEGpermirRepository"></param>
    ''' <param name="tenantRollRepository"></param>
    Public Sub New(ByVal permissionsRoleRepository As IRepository(Of PermissionRoll), ByVal rolesRepository As IRoleRepository, SEGpermirRepository As ISEGpermirRepository,
                    ByVal tenantRollRepository As IRepository(Of TenantRoll))
        If permissionsRoleRepository Is Nothing Then
            Throw New ArgumentNullException("permissionsRoleRepository Vacio")
        End If
        If rolesRepository Is Nothing Then
            Throw New ArgumentNullException("rolesRepository Vacio")
        End If
        _rolesRepository = rolesRepository
        _permissionsRoleRepository = permissionsRoleRepository
        _SEGpermirRepository = SEGpermirRepository
        _TenantRollRepository = tenantRollRepository
    End Sub

    ''' <summary>
    ''' Deletes the role.	
    ''' </summary>
    ''' <param name="role">The role.</param>
    ''' <param name="audit">The audit.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function DeleteRole(role As Roll, audit As AuditMessage) As Boolean Implements IPermissionsRoleAdminService.DeleteRole
        If role Is Nothing Then
            Throw New ArgumentNullException("Role Nothing")
        End If
        If role.PermissionRoll Is Nothing Then
            Throw New ArgumentNullException("Permission Roll Nothing")
        End If
        'creo la unidad de trabajo para el manejo del rol
        Dim unitWorkRoles As IUnitWork = TryCast(_rolesRepository.UnitWork, IUnitWork)

        Try
            Using scope As New TransactionScope(TransactionScopeOption.Required)
                _TenantRollRepository.DeleteList(role.TenantRoll.ToList)
                _permissionsRoleRepository.DeleteList(role.PermissionRoll.ToList)
                role.ChangeTracker.State = ObjectState.Deleted
                _rolesRepository.DeleteEntity(role)
                unitWorkRoles.Commit()
                scope.Complete()
                Return True
            End Using

        Catch ex As Exception
            'descarto los permisos en el rol
            unitWorkRoles.RollbackChanges()
            IndigoManagementExceptions.HandleException(ex, "UIPolicy")
            Return False
        End Try
    End Function

    ''' <summary>
    ''' Lists the permissions role.	
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListPermissionsRole() As IEnumerable(Of PermissionRoll) Implements IPermissionsRoleAdminService.ListPermissionsRole
        Try
            '  Return _permissionsRoleRepository.GetByFilter(Function(e) e.State = False)
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "UIPolicy")
            Return Nothing
        End Try
    End Function

    ''' <summary>
    ''' Saves the permissions role.	
    ''' </summary>
    ''' <param name="role">The rol.</param>
    ''' <param name="audit">The audit.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SavePermissionsRole(role As Roll, audit As AuditMessage) As Boolean Implements IPermissionsRoleAdminService.SavePermissionsRole
        If role.PermissionRoll Is Nothing Then
            Throw New ArgumentNullException("permissionsRole Vacio")
        End If
        If role Is Nothing Then
            Throw New ArgumentNullException("role Vacio")
        End If

        'creo la unidad de trabajo para los permisos del rol
        Dim unitWorkPermissionsRole As IUnitWork = TryCast(_permissionsRoleRepository.UnitWork, IUnitWork)
        'creo la unidad de trabajo para el manejo del rol
        Dim unitWorkRoles As IUnitWork = TryCast(_rolesRepository.UnitWork, IUnitWork)

        Try
            Using scope As New TransactionScope(TransactionScopeOption.Required)
                _rolesRepository.SaveEntity(role)
                unitWorkRoles.Commit()
                If role.ChangeTracker.State = ObjectState.Added Then
                    role.RollCode = role.Id
                    _rolesRepository.SaveEntity(role)
                    unitWorkRoles.Commit()
                End If
                'confirmo la transaccion
                scope.Complete()
                Return True

            End Using

        Catch ex As Exception
            'descarto la unidad de trabajo de los permisos
            unitWorkPermissionsRole.RollbackChanges()
            'descarto la unidad de trabajo de los roles
            unitWorkRoles.RollbackChanges()
            IndigoManagementExceptions.HandleException(ex, "UIPolicy")
            Return False
        End Try
    End Function

#Region "EHR"
    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="idMenu"></param>
    ''' <param name="codigoRol"></param>
    ''' <returns></returns>
    Public Function GetPermisoRol(idMenu As String, codigoRol As String) As ActionResult(Of SEGpermir) Implements IPermissionsRoleAdminService.GetPermisoRol
        Try
            Return _SEGpermirRepository.GetPermisoRol(idMenu, codigoRol)
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of SEGpermir) With {.StateResult = False, .ObjectEmbbeded = Nothing, .Message = ex.Message}
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
            _rolesRepository = Nothing
            _permissionsRoleRepository = Nothing
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
