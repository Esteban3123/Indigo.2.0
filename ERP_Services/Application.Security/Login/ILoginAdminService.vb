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
Imports Domain.Security.Entities

#End Region

''' <summary>
''' 	
''' </summary>
Public Interface ILoginAdminService
    Inherits IDisposable

    ''' <summary>
    ''' Guardar las preferencias del usuario en el funcional de login
    ''' </summary>
    ''' <param name="codeUser">el codigo del usuario.</param>
    ''' <param name="role">el perfil (1. administrativo, 2. asistencial).</param>
    ''' <param name="CareCenter">el codigo del centro de atencion.</param>
    ''' <param name="unitFunctional">el codigo de la unidad funcional.</param>
    ''' <returns></returns>
    Function SaveLoginLocation(ByVal codeUser As String, ByVal role As Integer, ByVal CareCenter As String, ByVal unitFunctional As String) As Boolean

    ''' <summary>
    ''' Consulta el perfil del usuario
    ''' </summary>
    ''' <param name="codeUser">el codigo del usuario.</param>
    ''' <returns></returns>
    Function GetUserRole(ByVal codeUser As String) As String

    ''' <summary>
    ''' Lista huella digital del usuario logeado en genesis
    ''' </summary>
    ''' <returns></returns>
    Function GetFingerPrint() As List(Of Domain.Security.Entities.Person)

    ''' <summary>
    ''' Función para obtener una lista de contenedores
    ''' </summary>
    ''' <returns></returns>
    Function getContainers() As List(Of Containers)

    Function ListCompanies() As ActionResult(Of List(Of Company))

    ''' <summary>
    ''' Lista las compañias que tiene permiso el usuario
    ''' </summary>
    ''' <param name="idUser"></param>
    ''' <returns></returns>
    Function getCompaniesByUser(ByVal idUser As Integer) As ActionResult(Of List(Of Company))

    ''' <summary>
    ''' Obtiene el nombre del contenedor de seguridad
    ''' </summary>
    ''' <returns>Nombre del contenedor de seguridad</returns>
    Function getSecurityContainerName() As String

    ' ''' <summary>
    ' ''' Obtiene el nombre del contenedor de la interaccion con costos
    ' ''' </summary>
    ' ''' <returns>Nombre del contenedor de HIS</returns>
    'Function getInteropCostContainerName() As String

    ''' <summary>
    ''' Obtiene el nombre del contenedor de la interaccion con costos
    ''' </summary>
    ''' <returns>Nombre del contenedor de HIS</returns>
    Function getIndigoConnectionString() As String

    ''' <summary>
    ''' Función para obtener un contenedores
    ''' </summary>
    ''' <param name="Name">Nombre del Contenedor</param>
    ''' <returns></returns>
    Function getContainersByName(Name As String) As Containers

    ''' <summary>
    ''' Función para obtener un contenedores
    ''' </summary>
    ''' <param name="Code">Nombre del Contenedor</param>
    ''' <returns></returns>
    Function getContainersByCode(Code As String) As Containers

    ''' <summary>
    ''' Consulta el usuario por el correo electronico
    ''' </summary>
    ''' <param name="email"></param>
    ''' <returns></returns>
    Function GetUserByEmail(email As String) As User

    Function GetEndpointsByIdContainer(idContainer As Integer) As IEnumerable(Of Endpoints)

    ''' <summary>
    ''' Cargar las zonas horarias
    ''' </summary>
    ''' <returns></returns>
    Function getTimezone() As List(Of Timezone)

    Function getTimezoneById(IdTimeZone As Integer) As Timezone

End Interface
