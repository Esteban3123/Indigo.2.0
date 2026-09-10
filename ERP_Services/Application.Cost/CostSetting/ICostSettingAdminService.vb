'***********************************************************************
' Assembly         : Application.Cost
' Author           : Diego Andrés Roldán Lozano
' Created          : 26-02-2016
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************
Imports Domain.Base
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities

Public Interface ICostSettingAdminService
    Inherits IDisposable

    ''' <summary>
    ''' Guarda un parámetro de costos
    ''' </summary>
    Function SaveCostSetting(ByVal costSetting As CostSetting, ByVal audit As AuditMessage) As ActionResult(Of CostSetting)

    ''' <summary>
    ''' Elimina un parámetro de costos
    ''' </summary>
    Function DeleteCostSetting(ByVal costSetting As CostSetting, ByVal audit As AuditMessage) As ActionResult

    ''' <summary>
    ''' Obtiene el parámetro actual de costos
    ''' </summary>
    Function GetCostSetting() As CostSetting

    ''' <summary>
    ''' Obtiene el parámetro actual de costos por id
    ''' </summary>
    Function GetCostSettingById(id As Integer) As CostSetting

End Interface