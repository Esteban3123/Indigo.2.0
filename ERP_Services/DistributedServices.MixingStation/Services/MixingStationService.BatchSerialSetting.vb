Imports Application.MixingStation
Imports Domain.Base.Entities
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Microsoft.Practices.Unity

Partial Public Class MixingStationService
    Implements IBatchSerialSettingService

    ''' <summary>
    ''' Obtiene un parámetro de lotes
    ''' </summary>
    ''' <returns></returns>
    Public Function GetBatchSerialSetting() As ActionResult(Of BatchSerialSetting) Implements IBatchSerialSettingService.GetBatchSerialSettings
        Using service As IBatchSerialSettingAdminService = Container.Current.Resolve(Of IBatchSerialSettingAdminService)()
            Return service.GetBatchSerialSettings()
        End Using
    End Function

    ''' <summary>
    ''' Guarda un parámetro de mezclas
    ''' </summary>
    ''' <param name="BatchSerialSetting"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    Public Function SaveBatchSerialSetting(BatchSerialSetting As BatchSerialSetting, audit As AuditMessage) As ActionResult Implements IBatchSerialSettingService.SaveBatchSerialSetting
        Using service As IBatchSerialSettingAdminService = Container.Current.Resolve(Of IBatchSerialSettingAdminService)()
            Return service.SaveBatchSerialSetting(BatchSerialSetting, audit)
        End Using
    End Function
End Class
