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
Public Interface IToolbarAdminService
    Inherits IDisposable

    ''' <summary>
    ''' Lista Permisos Barra Usuario.
    ''' </summary>
    ''' <param name="codeUser">el codigo del usuario.</param>
    ''' <param name="codeRole">el codigo del rol.</param>
    ''' <param name="codeMenu">el codigo del menu.</param>
    ''' <param name="tenantId">id de tenant.</param>
    ''' <returns></returns>
    Function ListPermissionsUserToolbar(ByVal codeUser As String, codeRole As String, codeMenu As String, codeCompany As String, tenantId As Short) As List(Of PermissionUserToolbar)

    ''' <summary>
    ''' Lists the permissions user toolbar.	
    ''' </summary>
    ''' <param name="codeUser">The code user.</param>
    ''' <param name="codeRole">The code role.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function ListPermissionsFormsActive(codeUser As String, codeRole As String, codeCompany As String) As List(Of PermissionsFormsActive)

End Interface
