'***********************************************************************
' Assembly         : Domain.Security
' Author           : Juan Diego Diaz M.
' Created          : 12-11-2013
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"
Imports Domain.Security.Entities
Imports Domain.Base
#End Region
Public Interface IContainersRepository

    ''' <summary>
    ''' Función que obtiene una lista de Contenedores
    ''' </summary>
    ''' <returns>Lista de Contenedores</returns>
    Function getContainers() As List(Of Containers)

    Function ListCompanies() As List(Of Company)

    ''' <summary>
    ''' Obtiene el nombre del contenedor de seguridad
    ''' </summary>
    ''' <returns>Nombre del contenedor de seguridad</returns>
    Function getSecurityContainerName() As String

    ' ''' <summary>
    ' ''' Obtiene el nombre del contenedor de la interaccion con costos
    ' ''' </summary>
    ' ''' <returns>Nombre del contenedor de Indigo Vie Cloud Platform</returns>
    Function getInteropCostContainerName() As String

    ''' <summary>
    ''' Obtiene el nombre del contenedor de la interaccion con costos
    ''' </summary>
    ''' <returns>Nombre del contenedor de Indigo Vie Cloud Platform</returns>
    Function getIndigoConnectionString() As String

    ''' <summary>
    ''' Función para obtener un contenedor
    ''' </summary>
    ''' <returns>Objeto Contenedor</returns>
    Function getContainersByName(Name As String) As Containers

    ''' <summary>
    ''' Obtiene los contenedores para el codigo de empresa dado
    ''' </summary>
    ''' <param name="code">Codigo de empresa a consultar</param>
    ''' <returns>Contenedores</returns>
    Function getContainersByCode(code As String) As Containers

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="idCompany"></param>
    ''' <returns></returns>
    Function getContainersById(ByVal idCompany As Integer) As Containers

    ''' <summary>
    ''' Lista las compañias que tiene permiso el usuario
    ''' </summary>
    ''' <param name="idUser"></param>
    ''' <returns></returns>
    Function getCompaniesByUser(ByVal idUser As Integer) As List(Of Company)

    Function ListCompaniesByContainers(containers As String) As List(Of Company)

    ''' <summary>
    ''' Lista Zonas horarias
    ''' </summary>
    ''' <returns></returns>
    Function getTimezone() As List(Of Domain.Security.Entities.Timezone)

    ''' <summary>
    ''' Lista una zona horarias
    ''' </summary>
    ''' <returns></returns>
    Function getTimezoneById(idTimezone As Integer) As Domain.Security.Entities.Timezone

    ''' <summary>
    ''' Consulta el Container por el TransactionalContainer
    ''' </summary>
    ''' <param name="transactionalContainer"></param>
    ''' <returns></returns>
    Function getContainersByTransactionalContainer(ByVal transactionalContainer As String) As Containers

End Interface

