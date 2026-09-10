'***********************************************************************
' Assembly         : Application.Budget
' Author           : Jhossept Kevin Garay Rodriguez
' Created          : 10-04-2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities

Public Interface IBudgetConceptAdminService
    Inherits IDisposable

    ''' <summary>
    ''' Obtiene un concepto
    ''' </summary>
    ''' <param name="code"></param>
    ''' <param name="tracking"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetBudgetConcept(code As String, validityId As Integer, audit As AuditMessage) As Concept

    ''' <summary>
    ''' Obtiene un concepto by validity
    ''' </summary>
    ''' <param name="code"></param>
    ''' <param name="tracking"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetBudgetConceptByValidity(code As String, ValidityId As String, audit As AuditMessage) As Concept

    ''' <summary>
    ''' Elimina un concepto
    ''' </summary>
    ''' <param name="Concept">La entidad</param>
    ''' <param name="audit">The audit.</param>
    ''' <returns></returns>
    ''' <exception cref="System.ArgumentNullException">company Vacio</exception>
    Function DeleteBudgetConcept(Concept As Concept, audit As AuditMessage) As ActionResult

    ''' <summary>
    ''' Guarda o Actualiza un concepto
    ''' </summary>
    ''' <param name="Concept">la entidad</param>
    ''' <param name="audit">The audit.</param>
    ''' <returns></returns>
    ''' <exception cref="System.ArgumentNullException">entidad Vacio</exception>
    Function SaveBudgetConcept(Concept As Concept, audit As AuditMessage, Optional ByVal idSequense As Int64 = 0) As ActionResult(Of Concept)

    ''' <summary>
    ''' metodo para cambiar el estado de la entidad
    ''' </summary>
    ''' <param name="code">The code.</param>
    ''' <param name="state">if set to <c>true</c> [state].</param>
    ''' <param name="audit">The audit.</param>
    ''' <returns></returns>
    Function ChangeStateBudgetConcept(ByVal code As String, validityId As Integer, ByVal state As Boolean, ByVal audit As AuditMessage) As ActionResult(Of Concept)
End Interface
