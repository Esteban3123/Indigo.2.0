'***********************************************************************
' Assembly         : Application.Billing
' Author           : Diego Andrés Roldán Lozano
' Created          : 16-02-2015
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************
Imports Domain.Base
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities

Public Interface ISettingBillingAdminService
    Inherits IDisposable

    ''' <summary>
    ''' Obtiene un registro de parámetros por Id
    ''' </summary>
    Function GetSettingsBillingById(id As Integer, tracking As Boolean) As SettingsBilling

    ''' <summary>
    ''' Obtiene un resgistro de parámetros por el id de la unidad operativa
    ''' </summary>
    Function GetSettingsBillingByIdUnitOperative(IdUnitOperative As Integer, tracking As Boolean) As SettingsBilling

    ''' <summary>
    ''' Obtiene un resgistro de parámetros por el id de la unidad operativa para cargar los datos del formulario
    ''' </summary>
    Function GetSettingBillingByUnitOperativeForm(IdUnitOperative As Integer) As ActionResult(Of SettingsBilling)

    ''' <summary>
    ''' Obtiene un resgistro de parámetros por el id de la unidad operativa para cargar los datos del formulario
    ''' </summary>
    Function GetCustomTRM(OperatingUnitId As Integer, Tracking As Boolean) As ActionResult(Of List(Of CustomTRM))

    ''' <summary>
    ''' Saves the settings billing.
    ''' </summary>
    ''' <param name="settingBilling">The setting billing.</param>
    ''' <returns></returns>
    Function SaveSettingsBilling(settingBilling As SettingsBilling, audit As AuditMessage) As ActionResult(Of SettingsBilling)

    ''' <summary>
    ''' Saves the custom trm.
    ''' </summary>
    ''' <param name="CustomTRM">The setting billing.</param>
    ''' <returns></returns>
    Function SaveCustomTrm(CustomTRM As List(Of CustomTRM), audit As AuditMessage) As ActionResult(Of CustomTRM)

    ''' <summary>
    ''' Obtiene la cantidad de registros de facturas de tipo de documento 5
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function CountBillingInvoice() As Integer

End Interface