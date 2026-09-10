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
#End Region

''' <summary>
''' 	
''' </summary>
Public Interface IRoleAdminService
    Inherits IDisposable

    ''' <summary>
    ''' obtiene un rol y sus permisos dependiendo del tag del formulario
    ''' </summary>
    ''' <param name="rolId"></param>
    ''' <param name="IdForm"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetRolByIdAndPermissionRollByIdForm(rolId As Integer, IdForm As String) As Roll

    ''' <summary>
    ''' Lists the rol all.
    ''' </summary>
    ''' <returns></returns>
    Function ListRolAll() As List(Of RolAll)

    ''' <summary>
    ''' lista todos los roles
    ''' </summary>
    ''' <returns></returns>
    Function ListRoles() As IEnumerable(Of Roll)

    ''' <summary>
    ''' consulta un rol especifico
    ''' </summary>
    ''' <returns></returns>
    Function GetRole(ByVal codeRole As String, ByVal CompanyCode As String) As Roll

    ''' <summary>
    ''' Lista todos Los Permisos del Rol por menu especificando si los tiene o no autorizados
    ''' </summary>
    ''' <param name="codeMenu">el codigo del menu</param>
    ''' <param name="codeRole">el codigo del rol</param>
    ''' <param name="authorized">True= Si, False= No</param>
    ''' <returns></returns>
    Function GetPermissionsRole(ByVal codeRole As String, codeMenu As String, ByVal authorized As Boolean, companyCode As String) As List(Of PermissionRoll)

    ''' <summary>
    ''' Lista todos Los Permisos del Rol por menu.
    ''' </summary>
    ''' <param name="codeMenu">el codigo del menu</param>
    ''' <param name="codeRole">el codigo del rol</param>
    ''' <returns></returns>
    Function GetPermissionsRole(ByVal codeRole As String, codeMenu As String, companyCode As String) As List(Of PermissionRoll)

    ''' <summary>
    ''' Lista todos los permisos del rol.
    ''' </summary>
    ''' <param name="codeRole">el codigo del rol</param>
    ''' <returns></returns>
    Function GetPermissionsRole(codeRole As String, companyCode As String) As List(Of PermissionRoll)

    ''' <summary>
    ''' Lista los formularios para el treeview
    ''' </summary>
    ''' <returns></returns>
    Function ListForms() As List(Of VieForm)

    ''' <summary>
    ''' Lista los formularios para el treeview
    ''' </summary>
    ''' <returns></returns>
    Function ListModules() As List(Of VieModule)

End Interface
