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
Imports Domain.Base.Entities
Imports Domain.Crystal.Entities
Imports Domain.Security.Entities
Imports Infrastructure.CrossCutting.Base
#End Region

''' <summary>
''' 	
''' </summary>
Public Interface IPermissionsRoleAdminService
    Inherits IDisposable

    ''' <summary>
    ''' Graba los Permisos para el Rol.
    ''' </summary>
    ''' <param name="role">el rol.</param>
    ''' <returns></returns>
    Function SavePermissionsRole(ByVal role As Roll, ByVal audit As AuditMessage) As Boolean

    ''' <summary>
    ''' elimina los Permisos para el Rol.
    ''' </summary>
    ''' <param name="role">el rol</param>
    ''' <returns></returns>
    Function DeleteRole(ByVal role As Roll, ByVal audit As AuditMessage) As Boolean

    ''' <summary>
    ''' Lista todos los permisos del rol.
    ''' </summary>
    ''' <returns></returns>
    Function ListPermissionsRole() As IEnumerable(Of PermissionRoll)

#Region "EHR"
    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="idMenu"></param>
    ''' <param name="codigoRol"></param>
    ''' <returns></returns>
    Function GetPermisoRol(idMenu As String, codigoRol As String) As ActionResult(Of SEGpermir)
#End Region


End Interface
