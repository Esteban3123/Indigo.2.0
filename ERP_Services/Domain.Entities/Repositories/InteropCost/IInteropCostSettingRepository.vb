'***********************************************************************
' Assembly         : Domain.InteropCost
' Author           : Diego Andrés Roldán Lozano
' Created          : 29-12-2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************
Imports Domain.Base
Imports Domain.Entities

Public Interface IInteropCostSettingRepository
    Inherits IRepository(Of InteropCostSetting)

    ''' <summary>
    ''' Obtiene el parámetro actual de costos
    ''' </summary>
    Function GetInteropCostSetting() As InteropCostSetting

    ''' <summary>
    ''' Obtiene el parámetro actual de costos por id
    ''' </summary>
    Function GetInteropCostSettingById(id As Integer) As InteropCostSetting

End Interface