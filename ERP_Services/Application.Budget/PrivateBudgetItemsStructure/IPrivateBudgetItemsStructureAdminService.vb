'***********************************************************************
' Assembly         : Application.Budget
' Author           : Carlos Mario Arias Rubiano
' Created          : 29/01/2018
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities

Public Interface IPrivateBudgetItemsStructureAdminService
    Inherits IDisposable


    ''' <summary>
    ''' Guarda o Actualiza el registro
    ''' </summary>
    ''' <param name="audit">The audit.</param>
    ''' <returns></returns>
    Function SavePrivateBudgetItemsStructure(ByVal PrivateBudgetItemsStructure As PrivateBudgetItemsStructure, ByVal audit As AuditMessage, Optional ByVal idSequense As Int64 = 0) As ActionResult(Of PrivateBudgetItemsStructure)

    ''' <summary>
    ''' Elimina el registro
    ''' </summary>
    ''' <param name="audit">The audit.</param>
    ''' <returns></returns>
    Function DeletePrivateBudgetItemsStructure(ByVal PrivateBudgetItemsStructure As PrivateBudgetItemsStructure, ByVal audit As AuditMessage) As ActionResult

    ''' <summary>
    ''' Obtiene el registro por codigo
    ''' </summary>
    ''' <param name="code">The code.</param>
    ''' <returns></returns>
    Function GetPrivateBudgetItemsStructure(ByVal code As String, ByVal audit As AuditMessage) As ActionResult(Of PrivateBudgetItemsStructure)

    ''' <summary>
    ''' Obtiene el registro por id
    ''' </summary>
    ''' <param name="id"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetPrivateBudgetItemsStructureById(id As String, ByVal audit As AuditMessage) As ActionResult(Of PrivateBudgetItemsStructure)

End Interface
