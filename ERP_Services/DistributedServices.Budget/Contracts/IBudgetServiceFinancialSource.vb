'***********************************************************************
' Assembly         : DistributedServices.Budget
' Author           : Jhossept Kevin Garay
' Created          : 02-04-2014
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
Public Interface IBudgetServiceFinancialSource
    ''' <summary>
    ''' Obtiene una fuente de financiacion
    ''' </summary>
    ''' <returns></returns>
    <OperationContract()>
    Function GetFinancialSource(ByVal code As String, validityId As Integer, audit As AuditMessage) As FinancialSource

    ''' <summary>
    ''' Elimina una fuente de financiación
    ''' </summary>
    ''' <param name="financialSource">La entidad</param>
    ''' <param name="audit">The audit.</param>
    ''' <returns></returns>
    ''' <exception cref="System.ArgumentNullException">company Vacio</exception>
    <OperationContract()>
    Function DeleteFinancialSource(financialSource As FinancialSource, audit As AuditMessage) As ActionResult

    ''' <summary>
    ''' Guarda o Actualiza una fuente de financiacion
    ''' </summary>
    ''' <param name="financialSource">la entidad</param>
    ''' <param name="audit">The audit.</param>
    ''' <returns></returns>
    ''' <exception cref="System.ArgumentNullException">entidad Vacio</exception>
    <OperationContract()>
    Function SaveFinancialSource(financialSource As FinancialSource, idSequense As Int64, audit As AuditMessage) As ActionResult(Of FinancialSource)

    ''' <summary>
    ''' metodo para cambiar estado a la entidad
    ''' </summary>
    ''' <param name="code">The code.</param>
    ''' <param name="state">if set to <c>true</c> [state].</param>
    ''' <returns></returns>
    <OperationContract()>
    Function ChangeStateFinancialSource(code As String, validityId As Integer, state As Boolean, audit As AuditMessage) As Domain.Base.Entities.ActionResult(Of Domain.Entities.FinancialSource)
End Interface

