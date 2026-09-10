'***********************************************************************
' Assembly         : DistributedServices.Budget
' Author           : Jhossept Kevin Garay Rodriguez
' Created          : 10-04-2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"

Imports Application.Budget
Imports Domain.Base.Entities
Imports Infrastructure.CrossCutting.Base
Imports Microsoft.Practices.Unity

#End Region

Partial Public Class BudgetService

    ''' <summary>
    ''' Obtierne la conficuracion de presupuesto
    ''' </summary>
    ''' <returns></returns>
    Public Function GetSettingBudgetById(id As Integer, audit As AuditMessage) As ActionResult(Of Domain.Entities.SettingsBudget) Implements IButgetServiceSettingBudget.GetSettingBudgetById
        Using service As ISettingBudgetAdminService = Container.Current.Resolve(Of ISettingBudgetAdminService)()
            Return service.GetSettingBudgetById(id, audit)
        End Using
    End Function

    ''' <summary>
    ''' guarda la configuracion de presupuesto
    ''' </summary>
    ''' <param name="settingBudget">The setting budget.</param>
    ''' <returns></returns>
    Public Function SaveSettingBudget(settingBudget As Domain.Entities.SettingsBudget, audit As AuditMessage) As Domain.Base.Entities.ActionResult(Of Domain.Entities.SettingsBudget) Implements IButgetServiceSettingBudget.SaveSettingBudget
        Using service As ISettingBudgetAdminService = Container.Current.Resolve(Of ISettingBudgetAdminService)()
            Return service.SaveSettingBudget(settingBudget, audit)
        End Using
    End Function

    ''' <summary>
    ''' Cambia de estado la entidad
    ''' </summary>
    ''' <param name="id"></param>
    ''' <param name="state"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ChangeState(id As Integer, state As Boolean) As Domain.Base.Entities.ActionResult(Of Domain.Entities.SettingsBudget) Implements IButgetServiceSettingBudget.ChangeState

    End Function

    ''' <summary>
    ''' Elimina un parametro de Presupuesto
    ''' </summary>
    ''' <param name="settingBudget"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function DeleteSettingBudget(settingBudget As Domain.Entities.SettingsBudget, audit As AuditMessage) As Domain.Base.Entities.ActionResult Implements IButgetServiceSettingBudget.DeleteSettingBudget
        Using service As ISettingBudgetAdminService = Container.Current.Resolve(Of ISettingBudgetAdminService)()
            Return service.DeleteSettingBudget(settingBudget, audit)
        End Using
    End Function

End Class