Imports Application.AccountManagement
Imports Domain.AccountManagement.Model
Imports Domain.Base
Imports Domain.Base.Entities
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Microsoft.Practices.Unity

Partial Public Class AccountManagementService
    Implements IAccountManagementServiceFolioTransfer

    ''' <summary>
    ''' Permite realizar la solicitud de traslado de folios
    ''' </summary>
    ''' <param name="folioTransferRequest"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    Public Function RequestFolioTransferToUser(folioTransferRequest As List(Of FolioTransfer), audit As AuditMessage) As ActionResult Implements IAccountManagementServiceFolioTransfer.RequestFolioTransferToUser
        Using service As IFolioTransferAdminService = Container.Current.Resolve(Of IFolioTransferAdminService)()
            Return service.RequestFolioTransferToUser(folioTransferRequest, audit)
        End Using
    End Function

    ''' <summary>
    ''' Permite suspender (resolver) una alerta creada anteriormente
    ''' </summary>
    ''' <param name="folioAlert"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    Public Function SuspendFolioAlert(folioAlert As FolioAlert, audit As AuditMessage) As ActionResult Implements IAccountManagementServiceFolioTransfer.SuspendFolioAlert
        Using service As IFolioTransferAdminService = Container.Current.Resolve(Of IFolioTransferAdminService)()
            Return service.SuspendFolioAlert(folioAlert, audit)
        End Using
    End Function

    ''' <summary>
    ''' Permite crear una nueva alerta asociada a un folio
    ''' </summary>
    ''' <param name="folioAlert"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    Public Function SaveFolioAlert(folioAlert As FolioAlert, audit As AuditMessage) As ActionResult(Of FolioAlert) Implements IAccountManagementServiceFolioTransfer.SaveFolioAlert
        Using service As IFolioTransferAdminService = Container.Current.Resolve(Of IFolioTransferAdminService)()
            Return service.SaveFolioAlert(folioAlert, audit)
        End Using
    End Function

    ''' <summary>
    ''' Obtiene todas las alertas de traslados de folios
    ''' </summary>
    ''' <returns></returns>
    Public Function GetAllAlerts() As ActionResult(Of List(Of FolioAlert)) Implements IAccountManagementServiceFolioTransfer.GetAllAlerts
        Using service As IFolioTransferAdminService = Container.Current.Resolve(Of IFolioTransferAdminService)()
            Return service.GetAllAlerts()
        End Using
    End Function

    ''' <summary>
    ''' Obtiene una lista de todos los eventos de folios y también permite filtrar por numero admission, codigo de paciente o centro de atención
    ''' </summary>
    ''' <param name="admissionNumber">Número de ingreso</param>
    ''' <param name="patientCode">Código del paciente</param>
    ''' <param name="attentionCenter">Centro de atención</param>
    ''' <returns></returns>
    Public Function ListFolioEvents(attentionCenter As String, Optional admissionNumber As String = Nothing, Optional patientCode As String = Nothing, Optional userCode As String = Nothing) As ActionResult(Of List(Of VFolioTraceabilityProperties)) Implements IAccountManagementServiceFolioTransfer.ListFolioEvents
        Using service As IFolioTransferAdminService = Container.Current.Resolve(Of IFolioTransferAdminService)()
            Return service.ListFolioEvents(attentionCenter, admissionNumber, patientCode, userCode)
        End Using
    End Function

    ''' <summary>
    ''' Obtiene una lista de traslados de folios de acuerdo a un centro de atención y un código de usuario
    ''' </summary>
    ''' <param name="attentionCenter">Código del centro de atención</param>
    ''' <param name="userCode">Código del usuario</param>
    ''' <returns></returns>
    Public Function ListFolioTransferRequests(attentionCenter As String, managementAreaCode As String, userCode As String) As ActionResult(Of List(Of VDashboardProperties)) Implements IAccountManagementServiceFolioTransfer.ListFolioTransferRequests
        Using service As IFolioTransferAdminService = Container.Current.Resolve(Of IFolioTransferAdminService)()
            Return service.ListFolioTransferRequests(attentionCenter, managementAreaCode, userCode)
        End Using
    End Function

    ''' <summary>
    ''' Procesa la aceptación o rechazo de una solicitud de traslado de folio
    ''' </summary>
    ''' <param name="folioTransfer">Objeto del traslado</param>
    ''' <param name="newState">Nuevo estado de la solictud. 3 - Aceptado, 4 - Rechazado</param>
    ''' <param name="operation">Descripción de la operación: "aceptar", "rechazar"</param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    Function ProcessTransferRequest(folioTransfer As FolioTransfer, newState As Byte, operation As String, audit As AuditMessage) As ActionResult(Of FolioTransfer) Implements IAccountManagementServiceFolioTransfer.ProcessTransferRequest
        Using service As IFolioTransferAdminService = Container.Current.Resolve(Of IFolioTransferAdminService)()
            Return service.ProcessTransferRequest(folioTransfer, newState, operation, audit)
        End Using
    End Function

    ''' <summary>
    ''' Obtiene los folios asociados a un usuario a partir del SP AccountManagement_SP_GetUserFolios
    ''' </summary>
    ''' <param name="userCode"></param>
    ''' <returns></returns>
    Function GetUserFolios(userCode As String) As ActionResult(Of List(Of GetUserFolios)) Implements IAccountManagementServiceFolioTransfer.GetUserFolios
        Using service As IFolioTransferAdminService = Container.Current.Resolve(Of IFolioTransferAdminService)()
            Return service.GetUserFolios(userCode)
        End Using
    End Function
End Class
