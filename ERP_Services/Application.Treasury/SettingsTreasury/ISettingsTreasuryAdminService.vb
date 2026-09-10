'***********************************************************************
' Assembly         : Application.Treasury
' Author           : Diego Andrés Roldán Lozano
' Created          : 02-04-2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities

Public Interface ISettingsTreasuryAdminService
    Inherits IDisposable

    ''' <summary>
    ''' Guarda un parámetro de Tesorería
    ''' </summary>
    ''' <param name="settingsTreasury">The settings treasury.</param>
    ''' <param name="audit">The audit.</param>
    ''' <param name="idSequence">The identifier sequence.</param>
    ''' <returns></returns>
    Function SaveSettingsTreasury(ByVal settingsTreasury As SettingsTreasury, ByVal audit As AuditMessage) As ActionResult(Of SettingsTreasury)

    ''' <summary>
    ''' Elimina un parámetro de tesoreria
    ''' </summary>
    ''' <param name="settingsTreasury">The settings treasury.</param>
    ''' <param name="audit">The audit.</param>
    ''' <returns></returns>
    Function DeleteSettingsTreasury(ByVal settingsTreasury As SettingsTreasury, ByVal audit As AuditMessage) As ActionResult

    ''' <summary>
    ''' Obtiene un registro de parámetros por Id
    ''' </summary>
    ''' <param name="id">The identifier.</param>
    ''' <returns></returns>
    Function GetSettingsTreasuryById(id As Integer, ByVal audit As AuditMessage) As SettingsTreasury

    ''' <summary>
    ''' Obtiene un resgistro de parámetros por el id de la unidad operativa
    ''' </summary>
    ''' <param name="IdUnitOperative">The identifier unit operative.</param>
    ''' <returns></returns>
    Function GetSettingsTreasuryByIdUnitOperative(IdUnitOperative As Integer, ByVal audit As AuditMessage) As ActionResult(Of SettingsTreasury)

End Interface