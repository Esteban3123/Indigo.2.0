'***********************************************************************
' Assembly         : DistributedServices.Common
' Author           : Cristhian Mauricio Salazar
' Created          : 11-04-2011
'
' Last Modified By : Cristhian Mauricio Salazar
' Last Modified On : 16-04-2013
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Application.Common
Imports Domain.Base.Entities
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.CrossCutting.IOC

Partial Class CommonERPService

    ''' <summary>
    ''' Elimina una ciudad
    ''' </summary>
    ''' <param name="city">Ciudad</param>
    ''' <param name="audit">Auditoria</param>
    ''' <returns>True o False</returns>
    ''' <remarks></remarks>
    Public Function DeleteCity(city As Domain.Entities.City, session As SessionValues) As ActionMessageResult(Of Domain.Entities.City) Implements ICommonERPService.DeleteCity
        Using CityService As ICityAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of ICityAdminService)()
            Return CityService.DeleteCity(city, session.AuditMessageWcf)
        End Using
    End Function

    ''' <summary>
    ''' Obtiene una ciudad en especifico
    ''' </summary>
    ''' <param name="code">Codigo Ciudad</param>
    ''' <returns>Ciudad</returns>
    ''' <remarks></remarks>
    Public Function GetCity(code As String, session As SessionValues) As Domain.Entities.City Implements ICommonERPService.GetCity
        Using CityService As ICityAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of ICityAdminService)()
            Return CityService.GetCity(code)
        End Using
    End Function

    ''' <summary>
    ''' Lista todas las ciudades
    ''' </summary>
    ''' <returns>Lista Ciuades</returns>
    ''' <remarks></remarks>
    Public Function ListAllCity(session As SessionValues) As List(Of Domain.Entities.City) Implements ICommonERPService.ListAllCity
        Using CityService As ICityAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of ICityAdminService)()
            Return CityService.ListAllCity()
        End Using
    End Function

    ''' <summary>
    ''' Graba o actualiza una ciudad
    ''' </summary>
    ''' <param name="city">Ciudad</param>
    ''' <param name="audit">Objeto Auditoria</param>
    ''' <returns>True o False</returns>
    ''' <remarks></remarks>
    Public Function SaveCity(city As Domain.Entities.City, session As SessionValues) As ActionResult(Of Domain.Entities.City) Implements ICommonERPService.SaveCity
        Using CityService As ICityAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of ICityAdminService)()
            Return CityService.SaveCity(city, session.AuditMessageWcf)
        End Using
    End Function

    ''' <summary>
    ''' Retorna una ciudad especifica
    ''' </summary>
    ''' <param name="code">Codigo Ciudad</param>
    ''' <param name="idDepartamento">Id del departamento</param>
    ''' <returns>Ciudad</returns>
    ''' <remarks></remarks>
    Public Function GetCityByDepartment(code As String, idDepartamento As Integer, session As SessionValues) As Domain.Entities.City Implements ICommonERPService.GetCityByDepartment
        Using CityService As ICityAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of ICityAdminService)()
            Return CityService.GetCity(code, idDepartamento, session.AuditMessageWcf)
        End Using
    End Function

    ''' <summary>
    ''' Obtiene una ciudad especifica
    ''' </summary>
    ''' <param name="idCity">Id de la ciudad</param>
    ''' <returns>Ciudad</returns>
    ''' <remarks></remarks>
    Public Function GetCityById(idCity As Integer, session As SessionValues) As Domain.Entities.City Implements ICommonERPService.GetCityById
        Using CityService As ICityAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of ICityAdminService)()
            Return CityService.GetCityById(idCity)
        End Using
    End Function

    ''' <summary>
    ''' Metodo que lista todas las ciudades dependiendo del departanmento
    ''' </summary>
    ''' <param name="IdDepartment">Id del departamento</param>
    ''' <returns>Lista de ciudades</returns>
    ''' <remarks></remarks>
    Public Function ListAllCitiesByIdDepartment(idDepartment As Integer, session As SessionValues) As List(Of Domain.Entities.City) Implements ICommonERPService.ListAllCitiesByIdDepartment
        Using CityService As ICityAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of ICityAdminService)()
            Return CityService.ListAllCitiesByIdDepartment(idDepartment)
        End Using
    End Function

    ''' <summary>
    ''' Metodo que cambia el estado del registro
    ''' </summary>
    ''' <param name="code">Codigo Ciudad</param>
    ''' <returns>booleano</returns>
    ''' <remarks></remarks>
    Public Function ChangeStateCity(code As String, state As Boolean, session As Infrastructure.CrossCutting.Base.SessionValues) As ActionResult(Of Domain.Entities.City) Implements ICommonERPService.ChangeStateCity
        Using CityService As ICityAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of ICityAdminService)()
            Return CityService.ChangeStateCity(code, state, session.AuditMessageWcf)
        End Using
    End Function
End Class
