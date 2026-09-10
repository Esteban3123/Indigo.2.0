#Region "Imports"
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
#End Region
Public Interface IMaintenanceManufacturersAdminService
    Inherits IDisposable

    ''' <summary>
    ''' Función que obtiene todas las Fabricantes 
    ''' </summary>
    ''' <returns>Lista de Fabricantes</returns>
    ''' <remarks></remarks>
    Function ListAllMaintenanceManufacturers() As List(Of MaintenanceManufacturers)

    ''' <summary>
    ''' Función que obtiene una Fabricantes por Código
    ''' </summary>
    ''' <param name="Code">Código de la Fabricantes</param>
    ''' <returns>MaintenanceManufacturers</returns>
    ''' <remarks></remarks>
    Function GetMaintenanceManufacturersByCode(Code As String) As MaintenanceManufacturers

    ''' <summary>
    ''' Función para Almacenar un Fabricante
    ''' </summary>
    ''' <param name="MaintenanceManufacturers">Objeto MaintenanceManufacturers</param>
    ''' <param name="audit"></param>
    ''' <returns>Boolean</returns>
    ''' <remarks></remarks>
    Function SaveMaintenanceManufacturers(MaintenanceManufacturers As MaintenanceManufacturers, audit As AuditMessage, Optional idSequense As Long = 0) As Domain.Base.Entities.ActionResult(Of Domain.Entities.MaintenanceManufacturers)

    ''' <summary>
    ''' Función para Eliminar las Fabricantes
    ''' </summary>
    ''' <param name="MaintenanceManufacturers">Objeto MaintenanceManufacturers</param>
    ''' <param name="audit"></param>
    ''' <returns>Boolean</returns>
    ''' <remarks></remarks>
    Function DeleteMaintenanceManufacturers(MaintenanceManufacturers As MaintenanceManufacturers, audit As AuditMessage) As Domain.Base.Entities.ActionResult


    ''' <summary>
    ''' Cambiar el Estado del Estado de fabricante de Activos Fijos
    ''' </summary>
    ''' <param name="code">Code</param>
    ''' <param name="state">State</param>
    ''' <param name="audit">Audit</param>
    ''' <returns>Boolean</returns>
    ''' <remarks></remarks>
    Function ChangeMaintenanceManufacturersStatus(code As String, state As Boolean, audit As Infrastructure.CrossCutting.Base.AuditMessage) As Domain.Base.Entities.ActionResult(Of Domain.Entities.MaintenanceManufacturers)
End Interface
