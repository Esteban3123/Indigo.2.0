'***********************************************************************
' Assembly         : DistributedServices.Payroll
' Author           : Daniel Eduardo Arévalo Bonilla
' Created          : 05-03-2015
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Infrastructure.CrossCutting.IOC
Imports Application.Maintenance
Imports Domain.Base.Entities
Imports Infrastructure.CrossCutting.Base
Imports Microsoft.Practices.Unity

Partial Class MaintanceService
    Implements IMaintenanceParameterService

    ''' <summary>
    ''' Función para Eliminar los parámetros de Mantenimiento
    ''' </summary>
    ''' <param name="MaintenanceParameter">Objeto MaintenanceParameter</param>
    ''' <returns>Boolean</returns>
    ''' <remarks></remarks>
    Public Function DeleteMaintenanceParameter(MaintenanceParameter As Domain.Entities.MaintenanceParameter, session As SessionValues) As Boolean Implements IMaintenanceParameterService.DeleteMaintenanceParameter
        Dim MaintenanceParameterAdmin As IMaintenanceParameterAdminService = Container.Current.Resolve(Of IMaintenanceParameterAdminService)()
        Return MaintenanceParameterAdmin.DeleteMaintenanceParameter(MaintenanceParameter, session.AuditMessageWcf)
        'End Using
    End Function

    ''' <summary>
    ''' Función que obtiene los parámetros de Mantenimiento
    ''' </summary>
    ''' <returns>MaintenanceParameter</returns>
    ''' <remarks></remarks>
    Public Function ListMaintenanceParameter(session As SessionValues) As Domain.Entities.MaintenanceParameter Implements IMaintenanceParameterService.ListMaintenanceParameter
        Dim MaintenanceParameterAdmin As IMaintenanceParameterAdminService = Container.Current.Resolve(Of IMaintenanceParameterAdminService)()
        Return MaintenanceParameterAdmin.ListMaintenanceParameter()
        'End Using
    End Function

    ''' <summary>
    ''' Función para Almacenar los Parámetros de Mantenimiento
    ''' </summary>
    ''' <param name="MaintenanceParameter">Objeto MaintenanceParameter</param>
    ''' <returns>Boolean</returns>
    ''' <remarks></remarks>
    Public Function SaveMaintenanceParameter(MaintenanceParameter As Domain.Entities.MaintenanceParameter, session As SessionValues) As Boolean Implements IMaintenanceParameterService.SaveMaintenanceParameter
        Dim MaintenanceParameterAdmin As IMaintenanceParameterAdminService = Container.Current.Resolve(Of IMaintenanceParameterAdminService)()
        Return MaintenanceParameterAdmin.SaveMaintenanceParameter(MaintenanceParameter, session.AuditMessageWcf)
        'End Using
    End Function
End Class
