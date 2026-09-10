'***********************************************************************
' Assembly         : DistributedServices.InteropCost
' Author           : Diego Andrés Roldán lozano
' Created          : 22-12-2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities
Imports System.ServiceModel
Imports Application.InteropCost
Imports Microsoft.Practices.Unity

Partial Class InteropCostService
    Implements IInteropCostServiceInteropCostSetting

    ''' <summary>
    ''' Elimina un parámetro de costos
    ''' </summary>
    ''' <param name="interopCostSetting"></param>
    ''' <returns></returns>
    Public Function DeleteInteropCostSetting(interopCostSetting As InteropCostSetting, audit As AuditMessage) As ActionResult Implements IInteropCostServiceInteropCostSetting.DeleteInteropCostSetting
        Using service As IInteropCostSettingAdminService = DistributedServices.InteropCost.Container.Current.Resolve(Of IInteropCostSettingAdminService)()
            Return service.DeleteInteropCostSetting(interopCostSetting, audit)
        End Using
        'Return Me._interopCostSettingAdminService.DeleteInteropCostSetting(interopCostSetting, audit)
    End Function

    ''' <summary>
    ''' Obtiene el parámetro actual de costos
    ''' </summary>
    ''' <returns></returns>
    Public Function GetInteropCostSetting() As InteropCostSetting Implements IInteropCostServiceInteropCostSetting.GetInteropCostSetting
        Using service As IInteropCostSettingAdminService = DistributedServices.InteropCost.Container.Current.Resolve(Of IInteropCostSettingAdminService)()
            Return service.GetInteropCostSetting()
        End Using
        'Return Me._interopCostSettingAdminService.GetInteropCostSetting()
    End Function

    ''' <summary>
    ''' Obtiene el parámetro actual de costos por id
    ''' </summary>
    ''' <param name="id"></param>
    ''' <returns></returns>
    Public Function GetInteropCostSettingById(id As Integer) As InteropCostSetting Implements IInteropCostServiceInteropCostSetting.GetInteropCostSettingById
        Using service As IInteropCostSettingAdminService = DistributedServices.InteropCost.Container.Current.Resolve(Of IInteropCostSettingAdminService)()
            Return service.GetInteropCostSettingById(id)
        End Using
        'Return Me._interopCostSettingAdminService.GetInteropCostSettingById(id)
    End Function

    ''' <summary>
    ''' Guarda un parámetro de costos
    ''' </summary>
    ''' <param name="interopCostSetting"></param>
    ''' <returns></returns>
    Public Function SaveInteropCostSetting(interopCostSetting As InteropCostSetting, audit As AuditMessage) As ActionResult(Of InteropCostSetting) Implements IInteropCostServiceInteropCostSetting.SaveInteropCostSetting
        Using service As IInteropCostSettingAdminService = DistributedServices.InteropCost.Container.Current.Resolve(Of IInteropCostSettingAdminService)()
            Return service.SaveInteropCostSetting(interopCostSetting, audit)
        End Using
        'Return Me._interopCostSettingAdminService.SaveInteropCostSetting(interopCostSetting, audit)
    End Function
End Class