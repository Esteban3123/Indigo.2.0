'***********************************************************************
' Assembly         : Application.Budget
' Author           : Jhossept Kevin Garay Rodriguez
' Created          : 02-04-2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities

Public Interface IFinancialSourceAdminService
    Inherits IDisposable

    ''' <summary>
    ''' Obtiene una Fuente financiera
    ''' </summary>
    ''' <param name="code">The code.</param>
    ''' <returns></returns>
    ''' <exception cref="System.ArgumentNullException">Code Vacio</exception>
    Function GetFinancialSource(code As String, validityId As Integer, audit As AuditMessage) As FinancialSource

    ''' <summary>
    ''' Elimina una fuente de financiación
    ''' </summary>
    ''' <param name="financialSource">La entidad</param>
    ''' <param name="audit">The audit.</param>
    ''' <returns></returns>
    ''' <exception cref="System.ArgumentNullException">company Vacio</exception>
    Function DeleteFinancialSource(financialSource As FinancialSource, audit As AuditMessage) As ActionResult

    ''' <summary>
    ''' Guarda o Actualiza una fuente de financiacion
    ''' </summary>
    ''' <param name="financialSource">la entidad</param>
    ''' <param name="audit">The audit.</param>
    ''' <returns></returns>
    ''' <exception cref="System.ArgumentNullException">entidad Vacio</exception>
    Function SaveFinancialSource(financialSource As FinancialSource, audit As AuditMessage, Optional ByVal idSecuense As Int64 = 0) As ActionResult(Of FinancialSource)

    ''' <summary>
    ''' metodo para cambiar el estado a le entidad
    ''' </summary>
    ''' <param name="code">The code.</param>
    ''' <param name="state">if set to <c>true</c> [state].</param>
    ''' <param name="audit">The audit.</param>
    ''' <returns></returns>
    Function ChangeStateFinancialSource(code As String, validityId As Integer, state As Boolean, audit As AuditMessage) As ActionResult(Of FinancialSource)
End Interface
