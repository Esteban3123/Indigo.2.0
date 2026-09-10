#Region "Imports"
Imports System.ServiceModel
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities
#End Region

<ServiceContract()>
Public Interface IMaintenanceFailureRequestService

    ''' <summary>
    ''' Función que obtiene una falla por Código
    ''' </summary>
    ''' <param name="Code">Código de la Marca</param>
    ''' <returns>Trademark</returns>
    ''' <remarks></remarks>
    <OperationContract()>
    Function GetMaintenanceFailureRequestByCode(Code As String) As MaintenanceFailureRequest

    ''' <summary>
    ''' Función que obtiene todas las falla par alos Equipos
    ''' </summary>
    ''' <returns>Lista de falla</returns>
    ''' <remarks></remarks>
    <OperationContract()>
    Function ListAllMaintenanceFailureRequest(audit As AuditMessage) As List(Of Domain.Entities.MaintenanceFailureRequest)

    ''' <summary>
    ''' Función para Almacenar una flla
    ''' </summary>
    ''' <param name="MaintenanceFailureRequest">Objeto MaintenanceFailureRequest</param>    
    ''' <remarks></remarks>
    <OperationContract()>
    Function SaveMaintenanceFailureRequest(MaintenanceFailureRequest As Domain.Entities.MaintenanceFailureRequest, idSequense As Int64, audit As AuditMessage) As ActionResult(Of MaintenanceFailureRequest)

    ''' <summary>
    ''' Confirma
    ''' </summary>
    ''' <param name="MaintenanceFailureRequest"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <OperationContract()>
    Function ConfirmMaintenanceFailureRequest(MaintenanceFailureRequest As Domain.Entities.MaintenanceFailureRequest, idSequense As Int64, audit As AuditMessage) As ActionResult(Of MaintenanceFailureRequest)

End Interface
