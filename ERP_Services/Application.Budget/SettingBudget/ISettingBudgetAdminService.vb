'***********************************************************************
' Assembly         : Application.Budget
' Author           : Jhossept Kevin Garay Rodriguez
' Created          : 04-04-2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************
'***********************************************************************
' Assembly         : Application.Budget
' Author           : Jeisson Herrera Peña
' Created          : 19-09-2015
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities
#End Region

Public Interface ISettingBudgetAdminService
    Inherits IDisposable

#Region "Mehods"
    ''' <summary>
    ''' Obtiene la configuracion de presupuesto
    ''' </summary>
    ''' <returns></returns>
    Function GetSettingBudgetById(id As Integer, audit As AuditMessage) As ActionResult(Of SettingsBudget)

    ''' <summary>
    ''' metodo para guardar y actualizar la configuracion
    ''' </summary>
    ''' <param name="settingBudget">The setting budget.</param>
    ''' <returns></returns>
    Function SaveSettingBudget(ByVal settingBudget As SettingsBudget, audit As AuditMessage, Optional ByVal idSequense As Int64 = 0) As ActionResult(Of SettingsBudget)

    ''' <summary>
    ''' Elimina un parametro de Presupuesto
    ''' </summary>
    ''' <param name="audit">The audit.</param>
    ''' <returns></returns>
    Function DeleteSettingBudget(ByVal settingBudget As SettingsBudget, ByVal audit As AuditMessage) As ActionResult

    ''' <summary>
    ''' Cambia el estado de la entidad
    ''' </summary>
    ''' <param name="state"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function ChangeState(ByVal id As Integer, ByVal state As Boolean, ByVal audit As AuditMessage) As ActionResult(Of SettingsBudget)

#End Region

End Interface
