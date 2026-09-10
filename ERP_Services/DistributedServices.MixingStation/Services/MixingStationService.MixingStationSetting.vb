Imports Application.MixingStation
Imports Domain.Base.Entities
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Microsoft.Practices.Unity

Partial Public Class MixingStationService
    Implements IMixingStationSettingService

    ''' <summary>
    ''' Obtiene un parámetro de mezclas
    ''' </summary>
    ''' <returns></returns>
    Public Function GetMixingStationSettingByOperativeUnitId(operativeUnitId As Integer, audit As AuditMessage) As ActionResult(Of MixingStationSetting) Implements IMixingStationSettingService.GetMixingStationSettingByOperativeUnitId
        Using service As IMixingStationSettingAdminService = Container.Current.Resolve(Of IMixingStationSettingAdminService)()
            Return service.GetMixingStationSettingByOperativeUnitId(operativeUnitId, audit)
        End Using
    End Function

    ''' <summary>
    ''' Guarda un parámetro de mezclas
    ''' </summary>
    ''' <param name="mixingStationSetting"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    Public Function SaveMixingStationSetting(mixingStationSetting As MixingStationSetting, attentionCenters As List(Of MixingStationSettingAttentionCenter), audit As AuditMessage) As ActionResult(Of MixingStationSetting) Implements IMixingStationSettingService.SaveMixingStationSetting
        Using service As IMixingStationSettingAdminService = Container.Current.Resolve(Of IMixingStationSettingAdminService)()
            Return service.SaveMixingStationSetting(mixingStationSetting, attentionCenters, audit)
        End Using
    End Function

    ''' <summary>
    ''' Lista los centros de atencion
    ''' </summary>
    ''' <returns></returns>
    Public Function ListAttentionCenters() As List(Of MixingStationSettingAttentionCenter) Implements IMixingStationSettingService.ListAttentionCenters
        Using service As IMixingStationSettingAdminService = Container.Current.Resolve(Of IMixingStationSettingAdminService)()
            Return service.ListAttentionCenters()
        End Using
    End Function
End Class
