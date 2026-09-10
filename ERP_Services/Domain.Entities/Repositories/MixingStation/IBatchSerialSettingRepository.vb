'***********************************************************************
' Assembly         : Domain.MixingStation
' Author           : Giovanny Plazas L
' Created          : 2022-05-23
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Base

Public Interface IBatchSerialSettingRepository
    Inherits IRepository(Of BatchSerialSetting)

    ''' <summary>
    ''' Obtiene el registro parametro de lote
    ''' </summary>
    ''' <returns></returns>
    Function GetBatchSerialSetting() As BatchSerialSetting


End Interface
