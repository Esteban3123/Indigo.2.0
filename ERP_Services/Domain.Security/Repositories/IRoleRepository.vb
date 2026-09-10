'***********************************************************************
' Assembly         : Domain.Security
' Author           : WalterSierra
' Created          : 11-03-2011
'
' Last Modified By : Juan F. Tamayo
' Last Modified On : 2013-02-28
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"
Imports Domain.Base
Imports Domain.Security.Entities
#End Region

''' <summary>
''' 	
''' </summary>
Public Interface IRoleRepository
    Inherits IRepository(Of Roll)

    Function GetRolByIdAndPermissionRollByIdForm(rolId As Integer, IdForm As String) As Roll

    ''' <summary>
    ''' Lists the groups all.
    ''' </summary>
    ''' <returns></returns>
    Function ListRolAll() As List(Of RolAll)

    ''' <summary>
    ''' Obtiene el rol por Id.
    ''' </summary>
    ''' <param name="id">el id del rol</param>
    ''' <returns></returns>
    Function GetRoleId(ByVal id As Integer) As Roll

    ''' <summary>
    ''' Lista el  Nombre del rol.
    ''' </summary>
    ''' <param name="codeRole">el codigo del rol</param>
    ''' <returns></returns>
    Function GetRole(ByVal codeRole As String) As Roll

    ''' <summary>
    ''' Lista todos Los Permisos del Rol por menu especificando si los tiene o no autorizados
    ''' </summary>
    ''' <param name="codeRole">el codigo del rol</param>
    ''' <param name="codeMenu">el codigo del menu</param>
    ''' <param name="authorized">True= Si, False= No</param>
    ''' <returns></returns>
    Function GetPermissionsRole(ByVal codeRole As String, codeMenu As String, ByVal authorized As Boolean) As List(Of PermissionRoll)

    ''' <summary>
    ''' Lista todos Los Permisos del Rol por menu.
    ''' </summary>
    ''' <param name="codeMenu">el codigo del menu</param>
    ''' <param name="codeRole">el codigo del rol</param>
    ''' <returns></returns>
    Function GetPermissionsRole(ByVal codeRole As String, codeMenu As String) As List(Of PermissionRoll)

    ''' <summary>
    ''' Lista todos los permisos del rol.
    ''' </summary>
    ''' <param name="codeRole">el codigo del rol</param>
    ''' <returns></returns>
    Function GetPermissionsRole(codeRole As String) As List(Of PermissionRoll)

    ''' <summary>
    ''' Listar permisos por rol.	
    ''' </summary>
    ''' <param name="codeRole">El código del rol.</param>
    ''' <param name="authorized">autorizado.</param>
    ''' <returns>Lista de permisos por rol</returns>
    Function ListPermissionsRolAuthorized(codeRole As String, authorized As Boolean) As List(Of PermissionRoll)

End Interface
