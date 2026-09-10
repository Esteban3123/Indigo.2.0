'***********************************************************************
' Assembly         : Application.InteropCost
' Author           : Diego Andrés Roldán Lozano
' Created          : 22-12-2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************
Imports Domain.Base
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities

Public Interface IInteropCostSettingAdminService
    Inherits IDisposable

    ''' <summary>
    ''' Guarda un parámetro de costos
    ''' </summary>
    Function SaveInteropCostSetting(ByVal interopCostSetting As InteropCostSetting, ByVal audit As AuditMessage) As ActionResult(Of InteropCostSetting)

    ''' <summary>
    ''' Elimina un parámetro de costos
    ''' </summary>
    Function DeleteInteropCostSetting(ByVal interopCostSetting As InteropCostSetting, ByVal audit As AuditMessage) As ActionResult

    ''' <summary>
    ''' Obtiene el parámetro actual de costos
    ''' </summary>
    Function GetInteropCostSetting() As InteropCostSetting

    ''' <summary>
    ''' Obtiene el parámetro actual de costos por id
    ''' </summary>
    Function GetInteropCostSettingById(id As Integer) As InteropCostSetting

End Interface