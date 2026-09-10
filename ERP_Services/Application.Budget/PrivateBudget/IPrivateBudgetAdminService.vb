'***********************************************************************
' Assembly         : Application.Budget
' Author           : Daniel Eduardo Arévalo Bonilla
' Created          : 29/01/2018
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities

Public Interface IPrivateBudgetAdminService
    Inherits IDisposable

    ''' <summary>
    ''' Guarda o Actualiza el registro
    ''' </summary>
    ''' <param name="audit">The audit.</param>
    ''' <returns></returns>
    Function SavePrivateBudget(ByVal PrivateBudget As List(Of SP_ListPrivateBudget_Result), ByVal audit As AuditMessage, Optional ByVal idSequense As Int64 = 0) As ActionResult(Of List(Of SP_ListPrivateBudget_Result))

    ''' <summary>
    ''' Elimina el registro
    ''' </summary>
    ''' <param name="audit">The audit.</param>
    ''' <returns></returns>
    Function DeletePrivateBudget(ByVal PrivateBudget As PrivateBudget, ByVal audit As AuditMessage) As ActionResult

    ''' <summary>
    ''' Obtiene el registro por codigo
    ''' </summary>
    ''' <returns></returns>
    Function GetListPrivateBudget(ByVal audit As AuditMessage) As ActionResult(Of List(Of SP_ListPrivateBudget_Result))

End Interface
