'***********************************************************************
' Assembly         : Application.InteropCost
' Author           : Carlos Mario Arias Rubiano
' Created          : 22/11/2016
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities

Public Interface IGeneralExpenseCategoryAdminService
    Inherits IDisposable

    ''' <summary>
    ''' Guarda una entidad
    ''' </summary>
    Function SaveGeneralExpenseCategory(ByVal GeneralExpenseCategory As GeneralExpenseCategory, ByVal audit As AuditMessage) As ActionResult(Of GeneralExpenseCategory)

    ''' <summary>
    ''' Elimina una entidad
    ''' </summary>
    Function DeleteGeneralExpenseCategory(ByVal GeneralExpenseCategory As GeneralExpenseCategory, ByVal audit As AuditMessage) As ActionResult

    ''' <summary>
    ''' Updates the state.
    ''' </summary>
    ''' <param name="audit">The audit.</param>
    ''' <returns></returns>
    Function UpdateState(ByVal code As String, ByVal state As Boolean, ByVal audit As AuditMessage) As ActionResult(Of GeneralExpenseCategory)

    ''' <summary>
    ''' Obtiene la entidad por codigo
    ''' </summary>
    ''' <returns></returns>
    Function GetGeneralExpenseCategory(ByVal code As String, ByVal audit As AuditMessage) As ActionResult(Of GeneralExpenseCategory)

    ''' <summary>
    ''' Obtiene la entidad por id
    ''' </summary>
    ''' <returns></returns>
    Function GetGeneralExpenseCategoryById(id As Integer, audit As AuditMessage) As ActionResult(Of GeneralExpenseCategory)

End Interface
