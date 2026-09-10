Imports Domain.AccountManagement.Model
Imports Domain.Base.Entities
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base

Public Interface IFolioTransferAdminService
    Inherits IDisposable
    ''' <summary>
    ''' Permite realizar la solicitud de traslado de folios
    ''' </summary>
    ''' <param name="folioTransferRequest">lista de los folios a trasladar</param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    Function RequestFolioTransferToUser(folioTransferRequest As List(Of FolioTransfer), audit As AuditMessage) As ActionResult

    ''' <summary>
    ''' Permite suspender (resolver) una alerta creada anteriormente
    ''' </summary>
    ''' <param name="folioAlert">Alerta a suspender (resolver)</param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    Function SuspendFolioAlert(folioAlert As FolioAlert, audit As AuditMessage) As ActionResult

    ''' <summary>
    ''' Permite crear una nueva alerta asociada a un folio
    ''' </summary>
    ''' <param name="folioAlert">Alerta a crear</param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    Function SaveFolioAlert(folioAlert As FolioAlert, audit As AuditMessage) As ActionResult(Of FolioAlert)

    ''' <summary>
    ''' Obtiene todas las alertas por traslados de folios
    ''' </summary>
    ''' <returns></returns>
    Function GetAllAlerts() As ActionResult(Of List(Of FolioAlert))

    ''' <summary>
    ''' Obtiene una lista de todos los eventos de folios y también permite filtrar por numero admission, codigo de paciente o centro de atención
    ''' </summary>
    ''' <param name="admissionNumber">Número de ingreso</param>
    ''' <param name="patientCode">Código del paciente</param>
    ''' <param name="attentionCenter">Centro de atención</param>
    ''' <returns></returns>
    Function ListFolioEvents(attentionCenter As String, Optional admissionNumber As String = Nothing, Optional patientCode As String = Nothing, Optional userCode As String = Nothing) As ActionResult(Of List(Of VFolioTraceabilityProperties))

    ''' <summary>
    '''  Obtiene una lista de traslados de folios de acuerdo a un centro de atención y un código de usuario
    ''' </summary>
    ''' <param name="attentionCenter"></param>
    ''' <param name="userCode"></param>
    ''' <returns></returns>
    Function ListFolioTransferRequests(attentionCenter As String, managementAreaCode As String, userCode As String) As ActionResult(Of List(Of VDashboardProperties))

    ''' <summary>
    ''' Procesa la aceptación o rechazo de una solicitud de traslado de folio
    ''' </summary>
    ''' <param name="folioTransfer">PObjeto del traslado</param>
    ''' <param name="newState">Nuevo estado de la solictud. 3 - Aceptado, 4 - Rechazado</param>
    ''' <param name="operation">Descripción de la operación: "aceptar", "rechazar"</param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    Function ProcessTransferRequest(folioTransfer As FolioTransfer, newState As Byte, operation As String, audit As AuditMessage) As ActionResult(Of FolioTransfer)

    ''' <summary>
    ''' Obtiene los folios asociados a un usuario a partir del SP AccountManagement_SP_GetUserFolios
    ''' </summary>
    ''' <param name="userCode"></param>
    ''' <returns></returns>
    Function GetUserFolios(userCode As String) As ActionResult(Of List(Of GetUserFolios))
End Interface
