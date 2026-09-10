Imports System.ServiceModel
Imports Domain.Base.Entities
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base

<ServiceContract()>
Public Interface IMixingStationSettingService

    ''' <summary>
    ''' Obtiene un parámetro de mezclas
    ''' </summary>
    ''' <returns></returns>
    <OperationContract()>
    Function GetMixingStationSettingByOperativeUnitId(operativeUnitId As Integer, audit As AuditMessage) As ActionResult(Of MixingStationSetting)

    ''' <summary>
    ''' Guarda un parámetro de mezclas
    ''' </summary>
    ''' <param name="mixingStationSetting"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    <OperationContract()>
    Function SaveMixingStationSetting(mixingStationSetting As MixingStationSetting, attentionCenters As List(Of MixingStationSettingAttentionCenter), audit As AuditMessage) As ActionResult(Of MixingStationSetting)

    ''' <summary>
    ''' Lista los centros de atencion
    ''' </summary>
    ''' <returns></returns>
    <OperationContract>
    Function ListAttentionCenters() As List(Of MixingStationSettingAttentionCenter)
End Interface
