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
#End Region

Partial Public Class SecurityService

    ''' <summary>
    ''' Lists the permissions user toolbar.	
    ''' </summary>
    ''' <param name="codeUser">The code user.</param>
    ''' <param name="codeRole">The code role.</param>
    ''' <param name="codeMenu">The code menu.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListPermissionsUserToolbar(codeUser As String, codeRole As String, codeMenu As String, session As SessionValues) As List(Of PermissionUserToolbar) Implements ISecurityService.ListPermissionsUserToolbar
        Using toolbarAdmin As IToolbarAdminService = IocFactory.Instance().CurrentContainer.Resolve(Of IToolbarAdminService)()
            Return toolbarAdmin.ListPermissionsUserToolbar(codeUser, codeRole, codeMenu, session.IndigoCompany, session.TenantId)
        End Using
    End Function

    ''' <summary>
    ''' Lists the permissions user toolbar.	
    ''' </summary>
    ''' <param name="codeUser">The code user.</param>
    ''' <param name="codeRole">The code role.</param>
    ''' <returns>List of PermissionsFormsActive</returns>
    Public Function ListPermissionsFormsActive(codeUser As String, codeRole As String, session As Infrastructure.CrossCutting.Base.SessionValues) As List(Of PermissionsFormsActive) Implements ISecurityService.ListPermissionsFormsActive
        Using toolbarAdmin As IToolbarAdminService = IocFactory.Instance().CurrentContainer.Resolve(Of IToolbarAdminService)()
            Return toolbarAdmin.ListPermissionsFormsActive(codeUser, codeRole, session.IndigoCompany)
        End Using
    End Function

End Class
