'***********************************************************************
' Assembly         : Domain.Cost
' Author           : Diego Andrés Roldán Lozano
' Created          : 29-12-2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************
Imports Domain.Base
Imports Domain.Entities

Public Interface ICostSettingRepository
    Inherits IRepository(Of CostSetting)

    ''' <summary>
    ''' Obtiene el parámetro actual de costos
    ''' </summary>
    Function GetCostSetting() As CostSetting

    ''' <summary>
    ''' Obtiene el parámetro actual de costos por id
    ''' </summary>
    Function GetCostSettingById(id As Integer) As CostSetting

End Interface
