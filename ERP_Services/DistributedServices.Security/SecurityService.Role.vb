'***********************************************************************
' Assembly         : DistributedService.Security
' Author           : WalterSierra
' Created          : 11-03-2011
'
' Last Modified By : Juan F. Tamayo
' Last Modified On : 2013-03-05
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"
Imports Domain.Security.Entities
Imports Domain.Security
Imports Infrastructure.CrossCutting.IOC
Imports Application.Security
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities
Imports Domain.Crystal.Entities
#End Region

Partial Public Class SecurityService
    ''' <summary>
    ''' obtiene un rol y sus permisos dependiendo del tag del formulario
    ''' </summary>
    ''' <param name="rolId"></param>
    ''' <param name="IdForm"></param>
    ''' <returns></returns>
    Public Function GetRolByIdAndPermissionRollByIdForm(rolId As Integer, IdForm As String) As Roll Implements ISecurityService.GetRolByIdAndPermissionRollByIdForm
        Using rolesAdmin As IRoleAdminService = IocFactory.Instance().CurrentContainer.Resolve(Of IRoleAdminService)()
            Return rolesAdmin.GetRolByIdAndPermissionRollByIdForm(rolId, IdForm)
        End Using
    End Function

    ''' <summary>
    ''' Lists the rol all.
    ''' </summary>
    ''' <returns></returns>
    Public Function ListRolAll(session As SessionValues) As List(Of RolAll) Implements ISecurityService.ListRolAll
        Using rolesAdmin As IRoleAdminService = IocFactory.Instance().CurrentContainer.Resolve(Of IRoleAdminService)()
            Return rolesAdmin.ListRolAll
        End Using
    End Function

    ''' <summary>
    ''' Lists the roles.	
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListRoles(session As SessionValues) As IEnumerable(Of Roll) Implements ISecurityService.ListRoles
        Using rolesAdmin As IRoleAdminService = IocFactory.Instance().CurrentContainer.Resolve(Of IRoleAdminService)()
            Return rolesAdmin.ListRoles()
        End Using
    End Function

    ''' <summary>
    ''' Gets the role.	
    ''' </summary>
    ''' <param name="codeRole">The code role.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetRole(codeRole As String, session As SessionValues) As Roll Implements ISecurityService.GetRoleByCodeRole
        Using rolesAdmin As IRoleAdminService = IocFactory.Instance().CurrentContainer.Resolve(Of IRoleAdminService)()
            Return rolesAdmin.GetRole(codeRole, session.IndigoCompany)
        End Using
    End Function

    ''' <summary>
    ''' Gets the permissions role.	
    ''' </summary>
    ''' <param name="codeRole">The code role.</param>
    ''' <param name="codeMenu">The code menu.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetPermissionsRole(codeRole As String, codeMenu As String, session As SessionValues) As List(Of PermissionRoll) Implements ISecurityService.ListPermissionsRoleByRoleAndMenu
        Using rolesAdmin As IRoleAdminService = IocFactory.Instance().CurrentContainer.Resolve(Of IRoleAdminService)()
            Return rolesAdmin.GetPermissionsRole(codeRole, codeMenu)
        End Using
    End Function

    ''' <summary>
    ''' Saves the permissions role.	
    ''' </summary>
    ''' <param name="permissionsRole">The permissions role.</param>
    ''' <param name="role">The role.</param>
    ''' <param name="session">The session.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SavePermissionsRole(role As Roll, session As SessionValues) As Boolean Implements ISecurityService.SavePermissionsRole
        Using rolesAdmin As IPermissionsRoleAdminService = IocFactory.Instance().CurrentContainer.Resolve(Of IPermissionsRoleAdminService)()
            Return rolesAdmin.SavePermissionsRole(role, session.AuditMessageWcf)
        End Using
    End Function

    ''' <summary>
    ''' Deletes the role.	
    ''' </summary>
    ''' <param name="role">The role.</param>
    ''' <param name="session">The session.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function DeleteRole(role As Roll, session As SessionValues) As Boolean Implements ISecurityService.DeleteRole
        Using rolesAdmin As IPermissionsRoleAdminService = IocFactory.Instance().CurrentContainer.Resolve(Of IPermissionsRoleAdminService)()
            Return rolesAdmin.DeleteRole(role, session.AuditMessageWcf)
        End Using
    End Function

    ''' <summary>
    ''' Gets the permissions role.	
    ''' </summary>
    ''' <param name="codeRole">The code role.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetPermissionsRole(codeRole As String, session As SessionValues) As List(Of PermissionRoll) Implements ISecurityService.ListPermissionsRoleByCodeRole
        Using rolesAdmin As IRoleAdminService = IocFactory.Instance().CurrentContainer.Resolve(Of IRoleAdminService)()
            Return rolesAdmin.GetPermissionsRole(codeRole, session.IndigoCompany)
        End Using
    End Function

    ''' <summary>
    ''' Gets the permissions role authorized.	
    ''' </summary>
    ''' <param name="codeRole">The code role.</param>
    ''' <param name="codeMenu">The code menu.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetPermissionsRoleAuthorized(codeRole As String, codeMenu As String, session As SessionValues) As List(Of PermissionRoll) Implements ISecurityService.GetPermissionsRoleAuthorized
        Using rolesAdmin As IRoleAdminService = IocFactory.Instance().CurrentContainer.Resolve(Of IRoleAdminService)()
            Return rolesAdmin.GetPermissionsRole(codeRole, codeMenu, True)
        End Using
    End Function

    ''' <summary>
    ''' Gets the permissions role no authorized.	
    ''' </summary>
    ''' <param name="codeRole">The code role.</param>
    ''' <param name="codeMenu">The code menu.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetPermissionsRoleNoAuthorized(codeRole As String, codeMenu As String, session As SessionValues) As List(Of PermissionRoll) Implements ISecurityService.GetPermissionsRoleNoAuthorized
        Using rolesAdmin As IRoleAdminService = IocFactory.Instance().CurrentContainer.Resolve(Of IRoleAdminService)()
            Return rolesAdmin.GetPermissionsRole(codeRole, codeMenu, False)
        End Using
    End Function

    ''' <summary>
    ''' Gets the list of forms and actions.	
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListForms(session As SessionValues) As List(Of VieForm) Implements ISecurityService.ListForms
        Using rolesAdmin As IRoleAdminService = IocFactory.Instance().CurrentContainer.Resolve(Of IRoleAdminService)()
            Return rolesAdmin.ListForms()
        End Using
    End Function

    Public Function ListModules(session As SessionValues) As List(Of VieModule) Implements ISecurityService.ListModules
        Using rolesAdmin As IRoleAdminService = IocFactory.Instance().CurrentContainer.Resolve(Of IRoleAdminService)()
            Return rolesAdmin.ListModules()
        End Using
    End Function

#Region "EHR"
    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="idMenu"></param>
    ''' <param name="codigoRol"></param>
    ''' <returns></returns>
    Public Function GetPermisoRol(idMenu As String, codigoRol As String) As ActionResult(Of SEGpermir) Implements ISecurityService.GetPermisoRol
        Using rolesAdmin As IPermissionsRoleAdminService = IocFactory.Instance().CurrentContainer.Resolve(Of IPermissionsRoleAdminService)()
            Return rolesAdmin.GetPermisoRol(idMenu, codigoRol)
        End Using
    End Function
#End Region


End Class
