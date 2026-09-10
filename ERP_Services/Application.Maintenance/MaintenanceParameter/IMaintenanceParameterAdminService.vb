'***********************************************************************
' Assembly         : Application.Maintenance
' Author           : Daniel Eduardo Arévalo Bonilla
' Created          : 05-03-2015
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
#End Region

Public Interface IMaintenanceParameterAdminService
    Inherits IDisposable

    ''' <summary>
    ''' Función que obtiene los parámetros de Mantenimiento
    ''' </summary>
    ''' <returns>MaintenanceParameter</returns>
    ''' <remarks></remarks>
    Function ListMaintenanceParameter() As MaintenanceParameter

    ''' <summary>
    ''' Función para Almacenar los Parámetros de Mantenimiento
    ''' </summary>
    ''' <param name="MaintenanceParameter">Objeto MaintenanceParameter</param>
    ''' <param name="audit"></param>
    ''' <returns>Boolean</returns>
    ''' <remarks></remarks>
    Function SaveMaintenanceParameter(MaintenanceParameter As MaintenanceParameter, audit As AuditMessage) As Boolean

    ''' <summary>
    ''' Función para Eliminar los parámetros de Mantenimiento
    ''' </summary>
    ''' <param name="MaintenanceParameter">Objeto MaintenanceParameter</param>
    ''' <param name="audit"></param>
    ''' <returns>Boolean</returns>
    ''' <remarks></remarks>
    Function DeleteMaintenanceParameter(MaintenanceParameter As MaintenanceParameter, audit As AuditMessage) As Boolean

End Interface
