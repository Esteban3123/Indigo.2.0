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
Imports System.Configuration
Imports System.Web.Caching
#End Region

''' <summary>
''' 	
''' </summary>
Public Class RoleAdminService
    Implements IRoleAdminService

    Dim _rolesRepository As IRoleRepository
    Dim _formRepository As IFormRepository
    Dim _moduleRepository As IModuleRepository

    ''' <summary>
    ''' Initializa una nueva instancia de la clase <see cref="RoleAdminService" />.
    ''' </summary>
    ''' <param name="rolesRepository">el repositorio para el manejo de los usuarios.</param>
    Public Sub New(ByVal rolesRepository As IRoleRepository, ByVal formRepository As IFormRepository, moduleRepository As IModuleRepository)
        If rolesRepository Is Nothing Then
            Throw New ArgumentNullException("rolesRepository Vacio")
        End If
        If formRepository Is Nothing Then
            Throw New ArgumentNullException("formRepository Vacio")
        End If
        If moduleRepository Is Nothing Then
            Throw New ArgumentNullException("moduleRepository Vacio")
        End If
        _rolesRepository = rolesRepository
        _formRepository = formRepository
        _moduleRepository = moduleRepository
    End Sub

    ''' <summary>
    ''' Gets the permissions role.	
    ''' </summary>
    ''' <param name="codeRole">The code role.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetPermissionsRole(codeRole As String, companyCode As String) As List(Of PermissionRoll) Implements IRoleAdminService.GetPermissionsRole
        If String.IsNullOrEmpty(codeRole) = True Then
            Throw New ArgumentNullException("codeRole Vacio")
        End If
        If String.IsNullOrEmpty(companyCode) = True Then
            Throw New ArgumentNullException("companyCode Vacio")
        End If
        Try
            Return _rolesRepository.GetPermissionsRole(codeRole, companyCode)
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "UIPolicy")
            Return Nothing
        End Try
    End Function

    ''' <summary>
    ''' Gets the permissions role.	
    ''' </summary>
    ''' <param name="codeRole">The code role.</param>
    ''' <param name="codeMenu">The code menu.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetPermissionsRole(codeRole As String, codeMenu As String, companyCode As String) As List(Of PermissionRoll) Implements IRoleAdminService.GetPermissionsRole
        If String.IsNullOrEmpty(codeRole) = True Then
            Throw New ArgumentNullException("codeRole Vacio")
        End If
        If String.IsNullOrEmpty(codeMenu) = True Then
            Throw New ArgumentNullException("codeMenu Vacio")
        End If
        If String.IsNullOrEmpty(companyCode) = True Then
            Throw New ArgumentNullException("companyCode Vacio")
        End If
        Try
            Return _rolesRepository.GetPermissionsRole(codeRole, codeMenu)
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "UIPolicy")
            Return Nothing
        End Try
    End Function

    ''' <summary>
    ''' Gets the permissions role.	
    ''' </summary>
    ''' <param name="codeRole">The code role.</param>
    ''' <param name="codeMenu">The code menu.</param>
    ''' <param name="authorized">The authorized.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetPermissionsRole(codeRole As String, codeMenu As String, authorized As Boolean, companyCode As String) As List(Of PermissionRoll) Implements IRoleAdminService.GetPermissionsRole
        If String.IsNullOrEmpty(codeRole) = True Then
            Throw New ArgumentNullException("codeRole Vacio")
        End If
        If String.IsNullOrEmpty(codeMenu) = True Then
            Throw New ArgumentNullException("codeMenu Vacio")
        End If
        If String.IsNullOrEmpty(companyCode) = True Then
            Throw New ArgumentNullException("companyCode Vacio")
        End If
        Try
            Return _rolesRepository.GetPermissionsRole(codeRole, codeMenu, authorized)
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "UIPolicy")
            Return Nothing
        End Try
    End Function

    ''' <summary>
    ''' Gets the role.	
    ''' </summary>
    ''' <param name="codeRole">The code role.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetRole(codeRole As String, ByVal CompanyCode As String) As Roll Implements IRoleAdminService.GetRole
        If String.IsNullOrEmpty(codeRole) = True Then
            Throw New ArgumentNullException("codeRole Vacio")
        End If
        Try
            Return _rolesRepository.GetRole(codeRole)
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "UIPolicy")
            Return Nothing
        End Try
    End Function

    ''' <summary>
    ''' Lists the roles.	
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListRoles() As IEnumerable(Of Roll) Implements IRoleAdminService.ListRoles
        Try
            '  Return _rolesRepository.GetByFilter(Function(e) e.State = False)
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "UIPolicy")
            Return Nothing
        End Try
    End Function

    Public Function ListRolAll() As List(Of RolAll) Implements IRoleAdminService.ListRolAll
        Try
            Return _rolesRepository.ListRolAll
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "UIPolicy")
            Return Nothing
        End Try
    End Function

    ''' <summary>
    ''' obtiene un rol y sus permisos dependiendo del tag del formulario
    ''' </summary>
    ''' <param name="rolId"></param>
    ''' <param name="IdForm"></param>
    ''' <returns></returns>
    Public Function GetRolByIdAndPermissionRollByIdForm(rolId As Integer, IdForm As String) As Roll Implements IRoleAdminService.GetRolByIdAndPermissionRollByIdForm
        Try
            Return _rolesRepository.GetRolByIdAndPermissionRollByIdForm(rolId, IdForm)
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "UIPolicy")
            Return New Roll
        End Try
    End Function

    ''' <summary>
    ''' Gets the list of forms and actions.	
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListForms() As List(Of VieForm) Implements IRoleAdminService.ListForms
        Try
            Dim cache As New Cache
            Dim contenedorKey = ConfigurationManager.AppSettings.Get("containerSecurity")
            Dim forms As List(Of VieForm)

            If cache(contenedorKey) IsNot Nothing Then
                forms = cache(contenedorKey)
            Else
                ' Si no se encuentra en caché, llama a la función ListFormModule() para obtener la lista de formularios.
                forms = _formRepository.ListFormModule()

                cache.Remove(contenedorKey)
                ' Guarda la lista de formularios en caché con una duración de caché de X, el tiempo se pone en el web.config.
                cache.Add(
                          contenedorKey,
                          forms,
                          Nothing,
                          Date.Now.AddMinutes(CInt(ConfigurationManager.AppSettings.Get("WebCachingDurationMinutes"))),
                          Cache.NoSlidingExpiration,
                          CacheItemPriority.High,
                          Nothing)
            End If

            Return forms
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "UIPolicy")
            Return Nothing
        End Try
    End Function

    Public Function ListModules() As List(Of VieModule) Implements IRoleAdminService.ListModules
        Try
            Return _moduleRepository.ListVieModule()
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "UIPolicy")
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