
Imports Infrastructure.CrossCutting.IOC
Imports Application.Maintenance
Imports System.ServiceModel
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities
Imports Microsoft.Practices.Unity
Imports Domain.Entities

Partial Public Class MaintanceService

    ''' <summary>
    ''' Función para Eliminar las Fabricantes
    ''' </summary>
    ''' <param name="MaintenanceManufacturers">Objeto MaintenanceManufacturers</param>    
    ''' <returns>Boolean</returns>
    ''' <remarks></remarks>
    Public Function DeleteMaintenanceManufacturers(MaintenanceManufacturers As Domain.Entities.MaintenanceManufacturers, audit As AuditMessage) As ActionResult Implements IMaintenanceManufacturersService.DeleteMaintenanceManufacturers
        Using service As IMaintenanceManufacturersAdminService = Container.Current.Resolve(Of IMaintenanceManufacturersAdminService)()
            Return service.DeleteMaintenanceManufacturers(MaintenanceManufacturers, audit)
        End Using
    End Function

    ''' <summary>
    ''' Función que obtiene una marca por Código
    ''' </summary>
    ''' <param name="Code">Código de la Marca</param>
    ''' <returns>MaintenanceManufacturers</returns>
    ''' <remarks></remarks>
    Public Function GetMaintenanceManufacturersByCode(Code As String, session As SessionValues) As Domain.Entities.MaintenanceManufacturers Implements IMaintenanceManufacturersService.GetMaintenanceManufacturersByCode
        Using service As IMaintenanceManufacturersAdminService = Container.Current.Resolve(Of IMaintenanceManufacturersAdminService)()
            Return service.GetMaintenanceManufacturersByCode(Code)
        End Using
    End Function

    ''' <summary>
    ''' Función que obtiene todas las Fabricantes par alos Equipos
    ''' </summary>
    ''' <returns>Lista de Fabricantes</returns>
    ''' <remarks></remarks>
    Public Function ListAllMaintenanceManufacturers(session As Infrastructure.CrossCutting.Base.SessionValues, audit As AuditMessage) As List(Of Domain.Entities.MaintenanceManufacturers) Implements IMaintenanceManufacturersService.ListAllMaintenanceManufacturers
        Using service As IMaintenanceManufacturersAdminService = Container.Current.Resolve(Of IMaintenanceManufacturersAdminService)()
            Return service.ListAllMaintenanceManufacturers()
        End Using
    End Function

    ''' <summary>
    ''' Función para Almacenar una Marca
    ''' </summary>
    ''' <param name="MaintenanceManufacturers">Objeto MaintenanceManufacturers</param>
    ''' <returns>Boolean</returns>
    ''' <remarks></remarks>
    Public Function SaveMaintenanceManufacturers(MaintenanceManufacturers As Domain.Entities.MaintenanceManufacturers, idSequense As Int64, audit As AuditMessage) As ActionResult(Of MaintenanceManufacturers) Implements IMaintenanceManufacturersService.SaveMaintenanceManufacturers
        Using service As IMaintenanceManufacturersAdminService = Container.Current.Resolve(Of IMaintenanceManufacturersAdminService)()
            Return service.SaveMaintenanceManufacturers(MaintenanceManufacturers, audit, idSequense)
        End Using
        'Return _MaintenanceManufacturersAdminService.SaveMaintenanceManufacturers(MaintenanceManufacturers, audit, idSequense)
    End Function

    Public Function ChangeMaintenanceManufacturersStatus(Code As String, state As Boolean, audit As AuditMessage) As ActionResult(Of MaintenanceManufacturers) Implements IMaintenanceManufacturersService.ChangeMaintenanceManufacturersStatus
        Using service As IMaintenanceManufacturersAdminService = Container.Current.Resolve(Of IMaintenanceManufacturersAdminService)()
            Return service.ChangeMaintenanceManufacturersStatus(Code, state, audit)
        End Using
    End Function

End Class
