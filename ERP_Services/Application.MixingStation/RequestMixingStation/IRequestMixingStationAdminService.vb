'***********************************************************************
' Assembly         : Application.MixingStation
' Author           : Carlos Mario Arias Rubiano
' Created          : 09/02/2021
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities


Public Interface IRequestMixingStationAdminService
    Inherits IDisposable

    ''' <summary>
    ''' Procesa la central de mezclas
    ''' </summary>
    ''' <param name="objParams"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    Function SP_ProcessMixingStation(objParams As String, audit As AuditMessage) As ActionResult(Of SP_ProcessMixingStation_Result)

    ''' <summary>
    ''' Guarda la central de mezclas
    ''' </summary>
    Function SaveRequestMixingStation(data As Tuple(Of Integer, Integer)) As ActionResult(Of RequestMixingStation)

    ''' <summary>
    ''' Obtiene un grupo por codigo
    ''' </summary>
    ''' <param name="code">The code.</param>
    ''' <returns></returns>
    Function GetRequestMixingStation(ByVal code As String, ByVal audit As AuditMessage) As ActionResult(Of RequestMixingStation)

    ''' <summary>
    ''' Obtiene un grupo uvr por id
    ''' </summary>
    ''' <returns></returns>
    Function GetRequestMixingStationById(ByVal id As Integer) As ActionResult(Of RequestMixingStation)

    ''' <summary>
    ''' Procesa la Solicitud de Central de Mezclas
    ''' </summary>
    ''' <param name="requestIds"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    Function ProcessRequestMixingStation(ByVal requestIds As List(Of Integer), ByVal audit As AuditMessage) As ActionResult

    ''' <summary>
    ''' anula las solicitudes de central de mezclas
    ''' </summary>
    ''' <param name="requestMixingStationDetailIds"></param>
    ''' <returns></returns>
    Function AnnulateRequests(requestMixingStationDetailIds As List(Of Integer)) As ActionResult

    ''' <summary>
    ''' Reversa solicitudes activas al dashboard de confirmación de dosis unitaria.
    ''' </summary>
    ''' <param name="requestMixingStationDetailIds">Identificadores de los detalles de solicitud de central de mezclas a reversar.</param>
    ''' <param name="audit">Información de auditoría del usuario que ejecuta la acción.</param>
    ''' <returns>Resultado de la operación de reversa.</returns>
    Function ReverseRequestsToConfirmationUnitDose(requestMixingStationDetailIds As List(Of Integer), audit As AuditMessage) As ActionResult

    ''' <summary>
    ''' Devuelve solicitudes activas al flujo del servicio farmacéutico.
    ''' </summary>
    ''' <param name="requestMixingStationDetailIds">Identificadores de los detalles de solicitud de central de mezclas a devolver.</param>
    ''' <param name="audit">Información de auditoría del usuario que ejecuta la acción.</param>
    ''' <returns>Resultado de la operación de devolución al servicio farmacéutico.</returns>
    Function ReturnRequestsToPharmacy(requestMixingStationDetailIds As List(Of Integer), audit As AuditMessage) As ActionResult

    ''' <summary>
    ''' Vincula la readecaucion con el detalle de la solicitud de la campaña
    ''' </summary>
    ''' <param name="RequestMSDetailId"></param>
    ''' <param name="Readjustment"></param>
    ''' <returns></returns>
    Function AssignReadjustmentToRequestMSDetail(RequestMSDetailId As Integer, Readjustment As Readjustments) As ActionResult

End Interface
