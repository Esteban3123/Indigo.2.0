#Region "Imports"
Imports System.ServiceModel
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities
#End Region

<ServiceContract()>
Public Interface IMaintenanceManufacturersService

    ''' <summary>
    ''' Función que obtiene todas los Fabricantess para los Equipos
    ''' </summary>
    ''' <returns>Lista de Fabricantess</returns>
    ''' <remarks></remarks>
    <OperationContract()>
    Function ListAllMaintenanceManufacturers(session As Infrastructure.CrossCutting.Base.SessionValues, audit As AuditMessage) As List(Of Domain.Entities.MaintenanceManufacturers)

    ''' <summary>
    ''' Función que obtiene Fabricantes por Código
    ''' </summary>
    ''' <param name="Code">Código de la Fabricantes</param>
    ''' <returns>MaintenanceManufacturers</returns>
    ''' <remarks></remarks>
    <OperationContract()>
    Function GetMaintenanceManufacturersByCode(Code As String, session As SessionValues) As MaintenanceManufacturers

    ''' <summary>
    ''' Función para Almacenar Fabricantes
    ''' </summary>
    ''' <param name="MaintenanceManufacturers">Objeto MaintenanceManufacturers</param>    
    ''' <returns>Boolean</returns>
    ''' <remarks></remarks>
    <OperationContract()>
    Function SaveMaintenanceManufacturers(MaintenanceManufacturers As Domain.Entities.MaintenanceManufacturers, idSequense As Int64, audit As AuditMessage) As ActionResult(Of MaintenanceManufacturers)

    ''' <summary>
    ''' Función para Eliminar Fabricantess
    ''' </summary>
    ''' <param name="MaintenanceManufacturers">Objeto MaintenanceManufacturers</param>    
    ''' <returns>Boolean</returns>
    ''' <remarks></remarks>
    <OperationContract()>
    Function DeleteMaintenanceManufacturers(MaintenanceManufacturers As Domain.Entities.MaintenanceManufacturers, audit As AuditMessage) As ActionResult

    ''' <summary>
    ''' Función para Actualizar estado
    ''' </summary>
    ''' <param name="Code">Código</param>    
    ''' <returns>Boolean</returns>
    ''' <remarks></remarks>
    <OperationContract()>
    Function ChangeMaintenanceManufacturersStatus(Code As String, state As Boolean, audit As AuditMessage) As ActionResult(Of MaintenanceManufacturers)

End Interface
