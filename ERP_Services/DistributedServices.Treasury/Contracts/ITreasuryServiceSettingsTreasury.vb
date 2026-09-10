'***********************************************************************
' Assembly         : DistributedServices.Treasury
' Author           : Diego Andrés Roldán Lozano
' Created          : 02-04-2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities
Imports System.ServiceModel

<ServiceContract()>
Public Interface ITreasuryServiceSettingsTreasury

    ''' <summary>
    ''' Saves the settings treasury.
    ''' </summary>
    ''' <param name="settingsTreasury">The settings treasury.</param>
    ''' <returns></returns>
    <OperationContract()>
    Function SaveSettingsTreasury(settingsTreasury As SettingsTreasury, audit As AuditMessage) As ActionResult(Of SettingsTreasury)

    ''' <summary>
    ''' Elimina un parámetro de tesoreria
    ''' </summary>
    ''' <param name="settingsTreasury">The settings treasury.</param>
    ''' <returns></returns>
    <OperationContract()>
    Function DeleteSettingsTreasury(settingsTreasury As SettingsTreasury, audit As AuditMessage) As ActionResult

    ''' <summary>
    ''' Obtiene un registro de parámetros por Id
    ''' </summary>
    ''' <param name="id">The identifier.</param>
    ''' <returns></returns>
    <OperationContract()>
    Function GetSettingsTreasuryById(id As Integer, audit As AuditMessage) As SettingsTreasury

    ''' <summary>
    ''' Obtiene un resgistro de parámetros por el id de la unidad operativa
    ''' </summary>
    ''' <param name="IdUnitOperative">The identifier unit operative.</param>
    ''' <returns></returns>
    <OperationContract()>
    Function GetSettingsTreasuryByIdUnitOperative(IdUnitOperative As Integer, audit As AuditMessage) As ActionResult(Of SettingsTreasury)

End Interface
