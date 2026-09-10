'***********************************************************************
' Assembly         : Application.MixingStation
' Author           : Diego A. Roldan
' Created          : 2021-09-16
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Base.Entities
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base

Public Interface IMixingStationSettingAdminService
    Inherits IDisposable

    ''' <summary>
    ''' Obtiene un parámetro de mezclas
    ''' </summary>
    ''' <returns></returns>
    Function GetMixingStationSettingByOperativeUnitId(operativeUnitId As Integer, audit As AuditMessage) As ActionResult(Of MixingStationSetting)

    ''' <summary>
    ''' Guarda un parámetro de mezclas
    ''' </summary>
    ''' <param name="mixingStationSetting"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    Function SaveMixingStationSetting(mixingStationSetting As MixingStationSetting, attentionCenters As List(Of MixingStationSettingAttentionCenter), audit As AuditMessage) As ActionResult(Of MixingStationSetting)
    ''' <summary>
    ''' Lista los centros de atencion
    ''' </summary>
    ''' <returns></returns>
    Function ListAttentionCenters() As List(Of MixingStationSettingAttentionCenter)
End Interface
