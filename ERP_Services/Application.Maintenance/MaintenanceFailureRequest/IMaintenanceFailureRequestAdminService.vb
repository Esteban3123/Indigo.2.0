#Region "Imports"

Imports Domain.Base.Entities
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base

#End Region

Public Interface IMaintenanceFailureRequestAdminService
    Inherits IDisposable

    ''' <summary>
    ''' Función que obtiene una falla por Código
    ''' </summary>
    ''' <param name="Code">Código del Responsible</param>
    ''' <returns>Trademark</returns>
    ''' <remarks></remarks>
    Function GetMaintenanceFailureRequestByCode(Code As String) As MaintenanceFailureRequest

    ''' <summary>
    ''' Función que obtiene todas fallas de mantenimiento
    ''' </summary>
    ''' <returns>Lista de Responsible</returns>
    ''' <remarks></remarks>
    Function ListAllMaintenanceFailureRequest() As List(Of MaintenanceFailureRequest)

    ''' <summary>
    ''' Función para Almacenar una Fallas y solicitudes
    ''' </summary>
    ''' <param name="MaintenanceFailureRequest">Objeto MaintenanceFailureRequest</param>
    ''' <param name="audit"></param>
    ''' <returns>Boolean</returns>
    ''' <remarks></remarks>
    Function SaveMaintenanceFailureRequest(MaintenanceFailureRequest As MaintenanceFailureRequest, audit As AuditMessage, Optional idSequense As Long = 0) As ActionResult(Of MaintenanceFailureRequest)

    ''' <summary>
    ''' Confirma una falla
    ''' </summary>s
    ''' <param name="MaintenanceFailureRequest"></param>        
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function ConfirmMaintenanceFailureRequest(MaintenanceFailureRequest As MaintenanceFailureRequest, audit As AuditMessage, Optional idSequense As Long = 0) As ActionResult(Of MaintenanceFailureRequest)

    ''' <summary>
    ''' Envía las notificaciones asociadas a la falla
    ''' </summary>
    ''' <param name="listMaintenanceFailureRequest"></param>
    ''' <remarks></remarks>
    Sub SendMaintenanceFailureRequestNotification(listMaintenanceFailureRequest As List(Of MaintenanceFailureRequest))

End Interface
