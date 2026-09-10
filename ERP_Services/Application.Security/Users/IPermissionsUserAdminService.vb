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
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities
Imports Domain.Crystal.Entities

#End Region

''' <summary>
''' 	
''' </summary>
Public Interface IPermissionsUserAdminService
    Inherits IDisposable

    ''' <summary>
    ''' Graba los Permisos para el Usuario.
    ''' </summary>
    ''' <param name="user">el usuario</param>
    ''' <returns></returns>
    Function SavePermissionsUser(ByVal user As User, ByVal SessionValues As SessionValues, ByVal audit As AuditMessage) As ActionResult

    ''' <summary>
    ''' elimina los Permisos para el Rol.
    ''' </summary>
    ''' <param name="user">el usuario</param>
    ''' <returns></returns>
    Function DeleteUsers(ByVal user As User, session As SessionValues, ByVal audit As AuditMessage) As ActionResult

#Region "EHR"
    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="idMenu"></param>
    ''' <param name="codigoUsuario"></param>
    ''' <returns></returns>
    Function GetPermisoUsuario(idMenu As String, codigoUsuario As String) As ActionResult(Of SEGpermiu)
#End Region


End Interface