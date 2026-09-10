'***********************************************************************
' Assembly         : Application.Cost
' Author           : Carlos Mario Arias Rubiano
' Created          : 21/11/2016
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities

Public Interface ICostGeneralExpenseCategoryAdminService
    Inherits IDisposable

    ''' <summary>
    ''' Guarda una entidad
    ''' </summary>
    Function SaveCostGeneralExpenseCategory(ByVal CostGeneralExpenseCategory As CostGeneralExpenseCategory, ByVal audit As AuditMessage) As ActionResult(Of CostGeneralExpenseCategory)

    ''' <summary>
    ''' Elimina una entidad
    ''' </summary>
    Function DeleteCostGeneralExpenseCategory(ByVal CostGeneralExpenseCategory As CostGeneralExpenseCategory, ByVal audit As AuditMessage) As ActionResult

    ''' <summary>
    ''' Updates the state.
    ''' </summary>
    ''' <param name="audit">The audit.</param>
    ''' <returns></returns>
    Function UpdateState(ByVal code As String, ByVal state As Boolean, ByVal audit As AuditMessage) As ActionResult(Of CostGeneralExpenseCategory)

    ''' <summary>
    ''' Obtiene la entidad por codigo
    ''' </summary>
    ''' <returns></returns>
    Function GetCostGeneralExpenseCategory(ByVal code As String, ByVal audit As AuditMessage) As ActionResult(Of CostGeneralExpenseCategory)

    ''' <summary>
    ''' Obtiene la entidad por id
    ''' </summary>
    ''' <returns></returns>
    Function GetCostGeneralExpenseCategoryById(id As Integer, audit As AuditMessage) As ActionResult(Of CostGeneralExpenseCategory)

End Interface
