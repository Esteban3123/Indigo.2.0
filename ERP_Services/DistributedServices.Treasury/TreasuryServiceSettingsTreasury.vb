'***********************************************************************
' Assembly         : DistributedServices.Treasury
' Author           : Diego Andrés Roldán lozano
' Created          : 02-04-2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Entities
Imports Application.Treasury
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities
Imports System.ServiceModel
Imports Microsoft.Practices.Unity

Partial Class TreasuryService

    ''' <summary>
    ''' Elimina un parámetro de tesoreria
    ''' </summary>
    ''' <param name="settingsTreasury">The settings treasury.</param>
    ''' <returns></returns>
    Public Function DeleteSettingsTreasury(settingsTreasury As SettingsTreasury, audit As AuditMessage) As ActionResult Implements ITreasuryServiceSettingsTreasury.DeleteSettingsTreasury
        Using service As ISettingsTreasuryAdminService = Container.Current.Resolve(Of ISettingsTreasuryAdminService)()
            Return service.DeleteSettingsTreasury(settingsTreasury, audit)
        End Using
        'Return Me._settingsTreasury.DeleteSettingsTreasury(settingsTreasury, audit)
    End Function

    ''' <summary>
    ''' Obtiene un registro de parámetros por Id
    ''' </summary>
    ''' <param name="id">The identifier.</param>
    ''' <returns></returns>
    Public Function GetSettingsTreasuryById(id As Integer, audit As AuditMessage) As SettingsTreasury Implements ITreasuryServiceSettingsTreasury.GetSettingsTreasuryById
        Using service As ISettingsTreasuryAdminService = Container.Current.Resolve(Of ISettingsTreasuryAdminService)()
            Return service.GetSettingsTreasuryById(id, audit)
        End Using
        'Return Me._settingsTreasury.GetSettingsTreasuryById(id, audit)
    End Function

    ''' <summary>
    ''' Saves the settings treasury.
    ''' </summary>
    ''' <param name="settingsTreasury">The settings treasury.</param>
    ''' <returns></returns>
    Public Function SaveSettingsTreasury(settingsTreasury As SettingsTreasury, audit As AuditMessage) As ActionResult(Of SettingsTreasury) Implements ITreasuryServiceSettingsTreasury.SaveSettingsTreasury
        Using service As ISettingsTreasuryAdminService = Container.Current.Resolve(Of ISettingsTreasuryAdminService)()
            Return service.SaveSettingsTreasury(settingsTreasury, audit)
        End Using
        'Return Me._settingsTreasury.SaveSettingsTreasury(settingsTreasury, audit)
    End Function

    ''' <summary>
    ''' Obtiene un resgistro de parámetros por el id de la unidad operativa
    ''' </summary>
    ''' <param name="IdUnitOperative">The identifier unit operative.</param>
    ''' <returns></returns>
    Public Function GetSettingsTreasuryByIdUnitOperative(IdUnitOperative As Integer, audit As AuditMessage) As ActionResult(Of SettingsTreasury) Implements ITreasuryServiceSettingsTreasury.GetSettingsTreasuryByIdUnitOperative
        Using service As ISettingsTreasuryAdminService = Container.Current.Resolve(Of ISettingsTreasuryAdminService)()
            Return service.GetSettingsTreasuryByIdUnitOperative(IdUnitOperative, audit)
        End Using
        'Return Me._settingsTreasury.GetSettingsTreasuryByIdUnitOperative(IdUnitOperative, audit)
    End Function

End Class
