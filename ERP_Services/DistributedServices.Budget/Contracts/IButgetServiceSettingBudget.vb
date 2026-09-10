'***********************************************************************
' Assembly         : DistributedServices.Budget
' Author           : Jhossept Kevin Garay
' Created          : 10-04-2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************
'***********************************************************************
' Assembly         : DistributedServices.Budget
' Author           : Jeisson Herrera
' Created          : 19-09-2015
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"

Imports Infrastructure.CrossCutting.Base
Imports System.ServiceModel
Imports Domain.Entities
Imports Domain.Base.Entities

#End Region

<ServiceContract()>
Public Interface IButgetServiceSettingBudget

    ''' <summary>
    ''' Obtiene la configuracion de presupuesto
    ''' </summary>
    ''' <returns></returns>
    <OperationContract()>
    Function GetSettingBudgetById(id As Integer, audit As AuditMessage) As ActionResult(Of Domain.Entities.SettingsBudget)

    ''' <summary>
    ''' guarda la configuracion de presupuesto
    ''' </summary>
    ''' <param name="settingBudget">The setting budget.</param>
    ''' <returns></returns>
    <OperationContract()>
    Function SaveSettingBudget(settingBudget As Domain.Entities.SettingsBudget, audit As AuditMessage) As Domain.Base.Entities.ActionResult(Of Domain.Entities.SettingsBudget)

    ''' <summary>
    ''' Cambia el estado de la entidad
    ''' </summary>
    ''' <param name="state"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <OperationContract()> _
    Function ChangeState(ByVal id As Integer, ByVal state As Boolean) As ActionResult(Of SettingsBudget)

    ''' <summary>
    ''' Elimina un parametro de Presupuesto
    ''' </summary>
    ''' <returns></returns>
    <OperationContract()>
    Function DeleteSettingBudget(settingBudget As Domain.Entities.SettingsBudget, audit As AuditMessage) As Domain.Base.Entities.ActionResult

End Interface
