'***********************************************************************
' Assembly         : DistributedService.MixingStation
' Author           : Carlos Mario Arias Rubiano
' Created          : 09/02/2021
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"
Imports Application.MixingStation
Imports DistributedServices.MixingStation
Imports Domain.Base.Entities
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Microsoft.Practices.Unity
Imports System.ServiceModel
#End Region

Partial Class MixingStationService
    Implements IMixingStationServiceRequestMixingStation

    Public Function GetRequestMixingStation(code As String, audit As AuditMessage) As ActionResult(Of RequestMixingStation) Implements IMixingStationServiceRequestMixingStation.GetRequestMixingStation
        Using service As IRequestMixingStationAdminService = Container.Current.Resolve(Of IRequestMixingStationAdminService)()
            Return service.GetRequestMixingStation(code, audit)
        End Using
    End Function

    Public Function GetRequestMixingStationById(id As Integer) As ActionResult(Of RequestMixingStation) Implements IMixingStationServiceRequestMixingStation.GetRequestMixingStationById
        Using service As IRequestMixingStationAdminService = Container.Current.Resolve(Of IRequestMixingStationAdminService)()
            Return service.GetRequestMixingStationById(id)
        End Using
    End Function

    Public Function SP_ProcessMixingStation(objParams As String, audit As AuditMessage) As ActionResult(Of SP_ProcessMixingStation_Result) Implements IMixingStationServiceRequestMixingStation.SP_ProcessMixingStation
        Using service As IRequestMixingStationAdminService = Container.Current.Resolve(Of IRequestMixingStationAdminService)()
            Return service.SP_ProcessMixingStation(objParams, audit)
        End Using
    End Function

    Public Function SaveRequestMixingStation(data As Tuple(Of Integer, Integer)) As ActionResult(Of RequestMixingStation) Implements IMixingStationServiceRequestMixingStation.SaveRequestMixingStation
        Using service As IRequestMixingStationAdminService = Container.Current.Resolve(Of IRequestMixingStationAdminService)()
            Return service.SaveRequestMixingStation(data)
        End Using
    End Function

    ''' <summary>
    ''' Procesa la Solicitud de Central de Mezclas
    ''' </summary>
    ''' <param name="requestIds"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    Public Function ProcessRequestMixingStation(ByVal requestIds As List(Of Integer), audit As AuditMessage) As ActionResult Implements IMixingStationServiceRequestMixingStation.ProcessRequestMixingStation
        Using service As IRequestMixingStationAdminService = Container.Current.Resolve(Of IRequestMixingStationAdminService)()
            Return service.ProcessRequestMixingStation(requestIds, audit)
        End Using
    End Function

    Public Function AnnulateRequests(requestMixingStationDetailIds As List(Of Integer)) As ActionResult Implements IMixingStationServiceRequestMixingStation.AnnulateRequests
        Using service As IRequestMixingStationAdminService = Container.Current.Resolve(Of IRequestMixingStationAdminService)()
            Return service.AnnulateRequests(requestMixingStationDetailIds)
        End Using
    End Function

    ''' <summary>
    ''' vincula readecuacion con detalle de solicitud
    ''' </summary>
    ''' <param name="RequestMSDetailId"></param>
    ''' <param name="Readjustment"></param>
    ''' <returns></returns>
    Public Function AssignReadjustmentToRequestMSDetail(RequestMSDetailId As Integer, Readjustment As Readjustments) As ActionResult Implements IMixingStationServiceRequestMixingStation.AssignReadjustmentToRequestMSDetail
        Using service As IRequestMixingStationAdminService = Container.Current.Resolve(Of IRequestMixingStationAdminService)()
            Return service.AssignReadjustmentToRequestMSDetail(RequestMSDetailId, Readjustment)
        End Using
    End Function
End Class
