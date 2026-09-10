'***********************************************************************
' Assembly         : Domain.MixingStation
' Author           : Diego A. Roldan
' Created          : 2021-09-16
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Base

Public Interface IMixingStationSettingRepository
    Inherits IRepository(Of MixingStationSetting)

    ''' <summary>
    ''' Obtiene un parámetro de mezclas
    ''' </summary>
    ''' <returns></returns>
    Function GetMixingStationSettingByOperativeUnitId(operativeUnitId As Integer, Optional tracking As Boolean = True) As MixingStationSetting
End Interface
