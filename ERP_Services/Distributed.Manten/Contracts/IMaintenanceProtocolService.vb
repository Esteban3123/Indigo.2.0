Imports System.ServiceModel
Imports Domain.Base.Entities
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base

<ServiceContract()>
Public Interface IMaintenanceProtocolService

    ''' <summary>
    ''' Guarda o Actualiza un protocolo
    ''' </summary>
    ''' <param name="audit">The audit.</param>
    ''' <returns></returns>
    <OperationContract()>
    Function SaveMaintenanceProtocol(ByVal maintenanceProtocol As MaintenanceProtocol, ByVal audit As AuditMessage, Optional ByVal idSequense As Int64 = 0) As ActionResult(Of MaintenanceProtocol)

    ''' <summary>
    ''' Elimina un protocolo
    ''' </summary>
    ''' <param name="audit">The audit.</param>
    ''' <returns></returns>
    <OperationContract()>
    Function DeleteMaintenanceProtocol(ByVal maintenanceProtocol As MaintenanceProtocol, ByVal audit As AuditMessage) As ActionResult

    ''' <summary>
    ''' Obtiene un protocolo por codigo
    ''' </summary>
    ''' <param name="code">The code.</param>
    ''' <returns></returns>
    <OperationContract()>
    Function GetMaintenanceProtocol(ByVal code As String, ByVal audit As AuditMessage) As MaintenanceProtocol

    ''' <summary>
    ''' Obtiene un protocolo uvr por id
    ''' </summary>
    ''' <returns></returns>
    <OperationContract()>
    Function GetMaintenanceProtocolById(ByVal id As Integer) As MaintenanceProtocol

    ''' <summary>
    ''' Cambia el estado de la entidad
    ''' </summary>
    ''' <param name="code"></param>
    ''' <param name="state"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <OperationContract()>
    Function ChangeStateMaintenanceProtocol(ByVal code As String, ByVal state As Boolean, ByVal audit As AuditMessage) As ActionResult(Of MaintenanceProtocol)

End Interface
