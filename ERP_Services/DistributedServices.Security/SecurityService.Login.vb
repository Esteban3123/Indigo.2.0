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
Imports Application.Security
Imports Domain.Security.Entities
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.CrossCutting.IOC
#End Region

Partial Public Class SecurityService


    ''' <summary>
    ''' Saves the login location.	
    ''' </summary>
    ''' <param name="codeUser">The code user.</param>
    ''' <param name="role">The role.</param>
    ''' <param name="CareCenter">The health care.</param>
    ''' <param name="unitFunctional">The unit functional.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SaveLoginLocation(codeUser As String, role As Integer, CareCenter As String, unitFunctional As String, session As SessionValues) As Boolean Implements ISecurityService.SaveLoginLocation
        Using loginAdmin As ILoginAdminService = IocFactory.Instance().CurrentContainer.Resolve(Of ILoginAdminService)()
            Return loginAdmin.SaveLoginLocation(codeUser, role, CareCenter, unitFunctional)
        End Using
    End Function

    ''' <summary>
    ''' Gets the user role.	
    ''' </summary>
    ''' <param name="codeUser">The code user.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetUserRole(codeUser As String, session As SessionValues) As String Implements ISecurityService.GetUserRole
        Using loginAdmin As ILoginAdminService = IocFactory.Instance().CurrentContainer.Resolve(Of ILoginAdminService)()
            Return loginAdmin.GetUserRole(codeUser)
        End Using
    End Function

    ''' <summary>
    ''' Gets the finger print.	
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetFingerPrint(session As SessionValues) As List(Of Domain.Security.Entities.Person) Implements ISecurityService.GetFingerPrint
        Using loginAdmin As ILoginAdminService = IocFactory.Instance().CurrentContainer.Resolve(Of ILoginAdminService)()
            Return loginAdmin.GetFingerPrint()
        End Using
    End Function

    ''' <summary>
    ''' Función para obtener una lista de contenedores
    ''' </summary>
    ''' <param name="session">Objeto session</param>
    ''' <returns>Lista de Contenedores</returns>
    Public Function getContainers(session As SessionValues) As List(Of Containers) Implements ISecurityService.getContainers
        Using loginAdmin As ILoginAdminService = IocFactory.Instance().CurrentContainer.Resolve(Of ILoginAdminService)()
            Return loginAdmin.getContainers
        End Using
    End Function

    Public Function ListCompanies() As Domain.Base.Entities.ActionResult(Of List(Of Company)) Implements ISecurityService.ListCompanies
        Using loginAdmin As ILoginAdminService = IocFactory.Instance().CurrentContainer.Resolve(Of ILoginAdminService)()
            Return loginAdmin.ListCompanies
        End Using
    End Function

    ''' <summary>
    ''' Lista las compañias que tiene permiso el usuario
    ''' </summary>
    ''' <param name="idUser"></param>
    ''' <returns></returns>
    Public Function getCompaniesByUser(ByVal idUser As Integer) As Domain.Base.Entities.ActionResult(Of List(Of Company)) Implements ISecurityService.getCompaniesByUser
        Using loginAdmin As ILoginAdminService = IocFactory.Instance().CurrentContainer.Resolve(Of ILoginAdminService)()
            Return loginAdmin.getCompaniesByUser(idUser)
        End Using
    End Function

    ''' <summary>
    ''' Obtiene el nombre del contenedor de seguridad
    ''' </summary>
    ''' <returns>Nombre del contenedor de seguridad</returns>
    Public Function getSecurityContainerName() As String Implements ISecurityService.getSecurityContainerName
        Using loginAdmin As ILoginAdminService = IocFactory.Instance().CurrentContainer.Resolve(Of ILoginAdminService)()
            Return loginAdmin.getSecurityContainerName
        End Using
    End Function

    ' ''' <summary>
    ' ''' Obtiene el nombre del contenedor de Indigo Vie Cloud Platform
    ' ''' </summary>
    ' ''' <returns>Nombre del contenedor de Indigo Vie Cloud Platform</returns>
    'Public Function getInteropCostContainerName() As String Implements ISecurityService.getInteropCostContainerName
    '    Using loginAdmin As ILoginAdminService = IocFactory.Instance().CurrentContainer.Resolve(Of ILoginAdminService)()
    '    Return loginAdmin.getInteropCostContainerName()
    'End Function

    ''' <summary>
    ''' Obtiene el nombre del contenedor de Indigo Vie Cloud Platform
    ''' </summary>
    ''' <returns>Nombre del contenedor de Indigo Vie Cloud Platform</returns>
    Public Function getIndigoConnectionString() As String Implements ISecurityService.getIndigoConnectionString
        Using loginAdmin As ILoginAdminService = IocFactory.Instance().CurrentContainer.Resolve(Of ILoginAdminService)()
            Return loginAdmin.getIndigoConnectionString()
        End Using
    End Function

    ''' <summary>
    ''' Función para obtener un contenedor
    ''' </summary>
    ''' <param name="Name">Nombre del contenedor</param>
    ''' <param name="session">Objeto session</param>
    ''' <returns>Objeto Contenedor</returns>
    Public Function getContainersByName(Name As String, session As SessionValues) As Containers Implements ISecurityService.getContainersByName
        Using loginAdmin As ILoginAdminService = IocFactory.Instance().CurrentContainer.Resolve(Of ILoginAdminService)()
            Return loginAdmin.getContainersByName(Name)
        End Using
    End Function

    Public Function getContainersByCode(Code As String, session As Infrastructure.CrossCutting.Base.SessionValues) As Containers Implements ISecurityService.getContainersByCode
        Using loginAdmin As ILoginAdminService = IocFactory.Instance().CurrentContainer.Resolve(Of ILoginAdminService)()
            Return loginAdmin.getContainersByCode(Code)
        End Using
    End Function

    Public Function getEndpointsByIdContainer(idContainer As Integer, session As SessionValues) As IEnumerable(Of Endpoints) Implements ISecurityService.getEndpointsByIdContainer
        Using loginAdmin As ILoginAdminService = IocFactory.Instance().CurrentContainer.Resolve(Of ILoginAdminService)()
            Return loginAdmin.GetEndpointsByIdContainer(idContainer)
        End Using
    End Function

    ''' <summary>
    ''' Función para obtener una lista de zonas horarias
    ''' </summary>
    ''' <param name="session">Objeto session</param>
    ''' <returns>Lista de Contenedores</returns>
    Public Function getTimezone(session As SessionValues) As List(Of Timezone) Implements ISecurityService.getTimezone
        Using loginAdmin As ILoginAdminService = IocFactory.Instance().CurrentContainer.Resolve(Of ILoginAdminService)()
            Return loginAdmin.getTimezone
        End Using
    End Function

    ''' <summary>
    ''' Función para obtener zona horaria
    ''' </summary>    
    ''' <returns>Lista de Contenedores</returns>
    Public Function getTimezoneById(IdTimeZone As Integer) As Timezone Implements ISecurityService.getTimezoneById
        Using loginAdmin As ILoginAdminService = IocFactory.Instance().CurrentContainer.Resolve(Of ILoginAdminService)()
            Return loginAdmin.getTimezoneById(IdTimeZone)
        End Using
    End Function

End Class
