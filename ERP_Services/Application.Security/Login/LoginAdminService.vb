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
Imports System.Configuration
Imports Domain.Base
Imports Domain.Base.Entities
Imports Domain.Security
Imports Domain.Security.Entities
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.CrossCutting.Exceptions

#End Region

''' <summary>
''' 	
''' </summary>
Public Class LoginAdminService
    Implements ILoginAdminService

    Private _loginRepository As ILoginRepository
    Private _userRepository As IUserRepository
    'Private _companyIndigoRepository As ICompanyIndigoRepository
    Private _containersRepository As IContainersRepository
    Private _endPointsRepository As IEndpointsRepository


    ''' <summary>
    ''' Initializa una nueva instancia de la clase <see cref="LoginAdminService" />.
    ''' </summary>
    ''' <param name="loginRepository">el repositorio para el manejo de los usuarios.</param>
    Public Sub New(ByVal loginRepository As ILoginRepository,
                   ByVal userRepository As IUserRepository,
                   ByVal containerRepository As IContainersRepository,
                   endPointsRepository As IEndpointsRepository)
        If loginRepository Is Nothing Then
            Throw New ArgumentNullException("loginRepository Vacio")
        End If
        If userRepository Is Nothing Then
            Throw New ArgumentNullException("userRepository Vacio")
        End If
        'If companyRepository Is Nothing Then
        '    Throw New ArgumentNullException("companyRepository Vacio")
        'End If
        If containerRepository Is Nothing Then
            Throw New ArgumentNullException("containerRepository Vacio")
        End If

        _loginRepository = loginRepository
        _userRepository = userRepository
        '_companyIndigoRepository = companyRepository
        _containersRepository = containerRepository
        _endPointsRepository = endPointsRepository

    End Sub

    ''' <summary>
    ''' Gets the finger print.	
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetFingerPrint() As List(Of Domain.Security.Entities.Person) Implements ILoginAdminService.GetFingerPrint
        Return _loginRepository.GetFingerPrint()
    End Function

    ''' <summary>
    ''' Gets the user role.	
    ''' </summary>
    ''' <param name="codeUser">The code user.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetUserRole(codeUser As String) As String Implements ILoginAdminService.GetUserRole
        If String.IsNullOrEmpty(codeUser) = True Then
            Throw New ArgumentNullException("codeUser Vacio")
        End If
        Return _loginRepository.GetRolUser(codeUser)
    End Function

    ''' <summary>
    ''' Saves the login location.	
    ''' </summary>
    ''' <param name="codeUser">The code user.</param>
    ''' <param name="role">The role.</param>
    ''' <param name="CareCenter">The health care.</param>
    ''' <param name="unitFunctional">The unit functional.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SaveLoginLocation(codeUser As String, role As Integer, CareCenter As String, unitFunctional As String) As Boolean Implements ILoginAdminService.SaveLoginLocation
        If String.IsNullOrEmpty(codeUser) = True Then
            Throw New ArgumentNullException("codeUser Vacio")
        End If
        Dim unitOfWork As IUnitWork = _userRepository.UnitWork
        Try
            'guardo las preferencias
            _userRepository.SaveLoginLocation(codeUser, role, CareCenter, unitFunctional)
            'confirmo la unidad de trabajo
            unitOfWork.Commit()
            Return True
        Catch ex As Exception
            unitOfWork.RollbackChanges()
            Return False
        End Try
    End Function

    ''' <summary>
    ''' Función para obtener una lista de contenedores
    ''' </summary>
    ''' <returns></returns>
    Public Function getContainers() As List(Of Containers) Implements ILoginAdminService.getContainers
        Return _containersRepository.getContainers
    End Function

    Public Function ListCompanies() As ActionResult(Of List(Of Company)) Implements ILoginAdminService.ListCompanies
        Try
            If ConfigurationManager.AppSettings.Get("ContainersAllow") Is Nothing Then
                Return New ActionResult(Of List(Of Company)) With {.StateResult = True, .ObjectEmbbeded = _containersRepository.ListCompanies()}
            End If

            Dim containers = ConfigurationManager.AppSettings.Get("ContainersAllow")
            Return New ActionResult(Of List(Of Company)) With {.StateResult = True, .ObjectEmbbeded = _containersRepository.ListCompaniesByContainers(containers)}
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of List(Of Company)) With {.StateResult = False, .Message = ex.Message}
        End Try
    End Function

    ''' <summary>
    ''' Lista las compañias que tiene permiso el usuario
    ''' </summary>
    ''' <param name="idUser"></param>
    ''' <returns></returns>
    Public Function getCompaniesByUser(ByVal idUser As Integer) As ActionResult(Of List(Of Company)) Implements ILoginAdminService.getCompaniesByUser
        Try
            Dim res As List(Of Company) = New List(Of Company)()
            res = _containersRepository.getCompaniesByUser(idUser)
            Return New ActionResult(Of List(Of Company)) With {.StateResult = True, .ObjectEmbbeded = res}
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of List(Of Company)) With {.StateResult = False, .Message = ex.Message}
        End Try
    End Function

    ''' <summary>
    ''' Consulta el usuario por el correo electronico
    ''' </summary>
    ''' <param name="email"></param>
    ''' <returns></returns>
    Public Function GetUserByEmail(email As String) As User Implements ILoginAdminService.GetUserByEmail
        If String.IsNullOrEmpty(email) = True Then
            Throw New ArgumentNullException("email vacío")
        End If
        Return _userRepository.GetUserByEmail(email)
    End Function

    ''' <summary>
    ''' Obtiene el nombre del contenedor de seguridad
    ''' </summary>
    ''' <returns>Nombre del contenedor de seguridad</returns>
    Public Function getSecurityContainerName() As String Implements ILoginAdminService.getSecurityContainerName
        Return _containersRepository.getSecurityContainerName()
    End Function

    ''' <summary>
    ''' Función para obtener un contenedores
    ''' </summary>
    ''' <param name="Name">Nombre del Contenedor</param>
    ''' <returns></returns>
    Public Function getContainersByName(Name As String) As Containers Implements ILoginAdminService.getContainersByName
        If String.IsNullOrEmpty(Name) = True Then
            Throw New ArgumentNullException("Nombre del contenedor vacío")
        End If
        Return _containersRepository.getContainersByName(Name)
    End Function

    Public Function getContainersByCode(Code As String) As Containers Implements ILoginAdminService.getContainersByCode
        If String.IsNullOrEmpty(Code) = True Then
            Throw New ArgumentNullException("Codigo del contenedor vacío")
        End If
        Return _containersRepository.getContainersByCode(Code)
    End Function

    ' ''' <summary>
    ' ''' Obtiene el nombre del contenedor de la interaccion con costos
    ' ''' </summary>
    ' ''' <returns>Nombre del contenedor de HIS</returns>
    'Public Function getInteropCostContainerName() As String Implements ILoginAdminService.getInteropCostContainerName
    '    Return _containersRepository.getInteropCostContainerName()
    'End Function

    Public Function GetEndpointsByIdContainer(idContainer As Integer) As IEnumerable(Of Endpoints) Implements ILoginAdminService.GetEndpointsByIdContainer
        Return _endPointsRepository.GetEndpointsByContainer(idContainer)
    End Function

    ''' <summary>
    ''' Función para obtener una lista de zonas horarias
    ''' </summary>
    ''' <returns></returns>
    Public Function getTimezone() As List(Of Timezone) Implements ILoginAdminService.getTimezone
        Return _containersRepository.getTimezone
    End Function

    ''' <summary>
    ''' Función para obtener una zona horaria
    ''' </summary>
    ''' <returns></returns>
    Public Function getTimezoneById(IdTimeZone As Integer) As Timezone Implements ILoginAdminService.getTimezoneById
        Return _containersRepository.getTimezoneById(IdTimeZone)
    End Function



#Region "IDisposable Support"
    Private disposedValue As Boolean ' Para detectar llamadas redundantes

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not disposedValue Then
            If disposing Then

            End If
            _loginRepository = Nothing
            _userRepository = Nothing
            _containersRepository = Nothing
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
