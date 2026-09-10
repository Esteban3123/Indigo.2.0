'***********************************************************************
' Assembly         : DistributedServices.Billing
' Author           : Diego Andrés Roldán Lozano
' Created          : 13-11-2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"

Imports Application.Billing
Imports Infrastructure.CrossCutting.Base
Imports System.ServiceModel
Imports Domain.Base.Entities
Imports Microsoft.Practices.Unity
Imports Domain.Entities

#End Region

Partial Class BillingService
    Implements IBillingServiceSettingBilling

    ''' <summary>
    ''' Obtiene un registro de parámetros por Id
    ''' </summary>
    ''' <param name="id"></param>
    ''' <param name="tracking"></param>
    ''' <returns></returns>
    Public Function GetSettingsBillingById(id As Integer, tracking As Boolean) As Domain.Entities.SettingsBilling Implements IBillingServiceSettingBilling.GetSettingsBillingById
        Using service As ISettingBillingAdminService = Container.Current.Resolve(Of ISettingBillingAdminService)()
            Return service.GetSettingsBillingById(id, tracking)
        End Using
        'Return _settingBillingAdminService.GetSettingsBillingById(id, tracking)
    End Function

    ''' <summary>
    ''' Obtiene un resgistro de parámetros por el id de la unidad operativa
    ''' </summary>
    ''' <param name="IdUnitOperative"></param>
    ''' <param name="tracking"></param>
    ''' <returns></returns>
    Public Function GetSettingsBillingByIdUnitOperative(IdUnitOperative As Integer, tracking As Boolean) As Domain.Entities.SettingsBilling Implements IBillingServiceSettingBilling.GetSettingsBillingByIdUnitOperative
        Using service As ISettingBillingAdminService = Container.Current.Resolve(Of ISettingBillingAdminService)()
            Return service.GetSettingsBillingByIdUnitOperative(IdUnitOperative, tracking)
        End Using
        'Return _settingBillingAdminService.GetSettingsBillingByIdUnitOperative(IdUnitOperative, tracking)
    End Function


    ''' <summary>
    ''' Obtiene un resgistro de parámetros por el id de la unidad operativa
    ''' </summary>
    ''' <param name="IdUnitOperative"></param>
    ''' <param name="tracking"></param>
    ''' <returns></returns>
    Public Function GetCustomTRM(IdUnitOperative As Integer, tracking As Boolean) As ActionResult(Of List(Of CustomTRM)) Implements IBillingServiceSettingBilling.GetCustomTRM
        Using service As ISettingBillingAdminService = Container.Current.Resolve(Of ISettingBillingAdminService)()
            Return service.GetCustomTRM(IdUnitOperative, tracking)
        End Using
        'Return _settingBillingAdminService.GetSettingsBillingByIdUnitOperative(IdUnitOperative, tracking)
    End Function

    ''' <summary>
    ''' Obtiene un resgistro de parámetros por el id de la unidad operativa para cargar los datos del formulario
    ''' </summary>
    ''' <param name="IdUnitOperative"></param>
    ''' <returns></returns>
    Public Function GetSettingBillingByUnitOperativeForm(IdUnitOperative As Integer) As ActionResult(Of Domain.Entities.SettingsBilling) Implements IBillingServiceSettingBilling.GetSettingBillingByUnitOperativeForm
        Using service As ISettingBillingAdminService = Container.Current.Resolve(Of ISettingBillingAdminService)()
            Return service.GetSettingBillingByUnitOperativeForm(IdUnitOperative)
        End Using
        'Return _settingBillingAdminService.GetSettingBillingByUnitOperativeForm(IdUnitOperative)
    End Function

    ''' <summary>
    ''' guarda o actualiza un registro de parametros de facturacion
    ''' </summary>
    ''' <returns></returns>
    Public Function SaveSettingsBilling(settingBilling As Domain.Entities.SettingsBilling, audit As AuditMessage) As ActionResult(Of Domain.Entities.SettingsBilling) Implements IBillingServiceSettingBilling.SaveSettingsBilling
        Using service As ISettingBillingAdminService = Container.Current.Resolve(Of ISettingBillingAdminService)()
            Return service.SaveSettingsBilling(settingBilling, audit)
        End Using
        'Return _settingBillingAdminService.SaveSettingsBilling(settingBilling, audit)
    End Function

    ''' <summary>
    ''' guarda o actualiza un registro de parametros de facturacion
    ''' </summary>
    ''' <returns></returns>
    Public Function SaveCustomTRM(customTRM As List(Of Domain.Entities.CustomTRM), audit As AuditMessage) As ActionResult(Of Domain.Entities.CustomTRM) Implements IBillingServiceSettingBilling.SaveCustomTRM
        Using service As ISettingBillingAdminService = Container.Current.Resolve(Of ISettingBillingAdminService)()
            Return service.SaveCustomTrm(customTRM, audit)
        End Using
    End Function

    ''' <summary>
    ''' Obtiene la cantidad de registros de facturas de tipo de documento 5
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function CountBillingInvoice() As Integer Implements IBillingServiceSettingBilling.CountBillingInvoice
        Using service As ISettingBillingAdminService = Container.Current.Resolve(Of ISettingBillingAdminService)()
            Return service.CountBillingInvoice()
        End Using
        'Return _settingBillingAdminService.CountBillingInvoice()
    End Function

End Class
