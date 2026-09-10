'***********************************************************************
' Assembly         : DistributedServices.Maintenance
' Author           : Daniel Eduardo Arévalo
' Created          : 5-03-2015
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"
Imports System.ServiceModel
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities
#End Region

<ServiceContract()> _
Public Interface IMaintenanceParameterService

    ''' <summary>
    ''' Función para Eliminar los parámetros de Mantenimiento
    ''' </summary>
    ''' <param name="MaintenanceParameter">Objeto MaintenanceParameter</param>
    ''' <param name="audit"></param>
    ''' <returns>Boolean</returns>
    ''' <remarks></remarks>
    <OperationContract()> _
    Function DeleteMaintenanceParameter(MaintenanceParameter As MaintenanceParameter, session As SessionValues) As Boolean

    ''' <summary>
    ''' Función que obtiene los parámetros de Mantenimiento
    ''' </summary>
    ''' <returns>MaintenanceParameter</returns>
    ''' <remarks></remarks>
    <OperationContract()> _
    Function ListMaintenanceParameter(session As SessionValues) As MaintenanceParameter

    ''' <summary>
    ''' Función para Almacenar los Parámetros de Mantenimiento
    ''' </summary>
    ''' <param name="MaintenanceParameter">Objeto MaintenanceParameter</param>
    ''' <param name="audit"></param>
    ''' <returns>Boolean</returns>
    ''' <remarks></remarks>
    <OperationContract()> _
    Function SaveMaintenanceParameter(MaintenanceParameter As MaintenanceParameter, session As SessionValues) As Boolean

End Interface
