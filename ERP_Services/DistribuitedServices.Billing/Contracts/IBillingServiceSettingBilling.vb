'***********************************************************************
' Assembly         : DistributedService.Billing
' Author           : Diego Andrés Roldán Lozano
' Created          : 13-11-2014
'
' Last Modified By : 
' Last Modified On : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"

Imports System.ServiceModel
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities
Imports Domain.Entities

#End Region

<ServiceContract()>
Public Interface IBillingServiceSettingBilling

    ''' <summary>
    ''' Obtiene un registro de parámetros por Id
    ''' </summary>
    <OperationContract()>
    Function GetSettingsBillingById(id As Integer, tracking As Boolean) As SettingsBilling

    ''' <summary>
    ''' Obtiene un resgistro de parámetros por el id de la unidad operativa
    ''' </summary>
    <OperationContract()>
    Function GetSettingsBillingByIdUnitOperative(IdUnitOperative As Integer, tracking As Boolean) As SettingsBilling

    ''' <summary>
    ''' Obtiene un resgistro de parámetros por el id de la unidad operativa para cargar los datos del form
    ''' </summary>
    <OperationContract()>
    Function GetCustomTRM(IdUnitOperative As Integer, tracking As Boolean) As ActionResult(Of List(Of CustomTRM))

    ''' <summary>
    ''' Obtiene un resgistro de parámetros por el id de la unidad operativa para cargar los datos del form
    ''' </summary>
    <OperationContract()>
    Function GetSettingBillingByUnitOperativeForm(IdUnitOperative As Integer) As ActionResult(Of SettingsBilling)

    ''' <summary>
    ''' guarda o actualiza un registro de parametros de facturacion
    ''' </summary>
    <OperationContract()>
    Function SaveSettingsBilling(settingBilling As Domain.Entities.SettingsBilling, audit As AuditMessage) As ActionResult(Of Domain.Entities.SettingsBilling)

    ''' <summary>
    ''' guarda los trm especificos
    ''' </summary>
    <OperationContract()>
    Function SaveCustomTRM(settingBilling As List(Of Domain.Entities.CustomTRM), audit As AuditMessage) As ActionResult(Of Domain.Entities.CustomTRM)

    ''' <summary>
    ''' Obtiene la cantidad de registros de facturas de tipo de documento 5
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <OperationContract()>
    Function CountBillingInvoice() As Integer

End Interface