
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

Public Interface IExpenseTypeAdminService
    Inherits IDisposable

    ''' <summary>
    ''' Obtiene un tipo de gasto
    ''' </summary>
    ''' <param name="code"></param>
    ''' <param name="tracking"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetExpenseType(code As String, validityId As Integer, type As Integer, audit As AuditMessage) As RevenueType

    ''' <summary>
    ''' Obtiene un tipo de gasto by validity
    ''' </summary>
    ''' <param name="code"></param>
    ''' <param name="tracking"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetExpenseTypeByValidity(code As String, ValidityId As String, audit As AuditMessage) As RevenueType

    ''' <summary>
    ''' Elimina un tipo de gasto
    ''' </summary>
    ''' <param name="RevenueType">La entidad</param>
    ''' <param name="audit">The audit.</param>
    ''' <returns></returns>
    ''' <exception cref="System.ArgumentNullException">company Vacio</exception>
    Function DeleteExpenseType(RevenueType As RevenueType, audit As AuditMessage) As ActionResult

    ''' <summary>
    ''' Guarda o Actualiza un tipo de gasto
    ''' </summary>
    ''' <param name="RevenueType">la entidad</param>
    ''' <param name="audit">The audit.</param>
    ''' <returns></returns>
    ''' <exception cref="System.ArgumentNullException">entidad Vacio</exception>
    Function SaveExpenseType(RevenueType As RevenueType, audit As AuditMessage, Optional ByVal idSequense As Int64 = 0) As ActionResult(Of RevenueType)

    ''' <summary>
    ''' metodo para cambiar el estado a la entidad
    ''' </summary>
    ''' <param name="code">The code.</param>
    ''' <param name="state">if set to <c>true</c> [state].</param>
    ''' <param name="audit">The audit.</param>
    ''' <returns></returns>
    Function ChangeStateExpenseType(ByVal code As String, validityId As Integer, type As Integer, ByVal state As Boolean, ByVal audit As AuditMessage) As ActionResult(Of RevenueType)
End Interface
