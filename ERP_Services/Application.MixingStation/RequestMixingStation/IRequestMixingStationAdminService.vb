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
    ''' Vincula la readecaucion con el detalle de la solicitud de la campaña
    ''' </summary>
    ''' <param name="RequestMSDetailId"></param>
    ''' <param name="Readjustment"></param>
    ''' <returns></returns>
    Function AssignReadjustmentToRequestMSDetail(RequestMSDetailId As Integer, Readjustment As Readjustments) As ActionResult

End Interface
