'***********************************************************************
' Assembly         : Application.MixingStation
' Author           : Giovanny Plazas L
' Created          : 2022-05-24
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Base.Entities
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base

Public Interface IBatchSerialSettingAdminService
    Inherits IDisposable


    ''' <summary>
    ''' Guarda  parámetro de Lotes
    ''' </summary>
    ''' <param name="BatchSerialSetting"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    Function SaveBatchSerialSetting(BatchSerialSetting As BatchSerialSetting, audit As AuditMessage) As ActionResult

    ''' <summary>
    ''' consulta el parametro de configuracion de lote
    ''' </summary>
    ''' <returns></returns>
    Function GetBatchSerialSettings() As ActionResult(Of BatchSerialSetting)
End Interface
