'***********************************************************************
' Assembly         : DistributedServices.Budget
' Author           : Jhossept Kevin Garay
' Created          : 10-04-2014
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
Public Interface IBudgetServiceBudgetConcept
    ''' <summary>
    ''' Obtiene un concepto
    ''' </summary>
    ''' <param name="code"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <OperationContract()>
    Function GetBudgetConcept(code As String, validityId As Integer, audit As AuditMessage) As Concept

    ''' <summary>
    ''' Obtiene un concepto by validity
    ''' </summary>
    ''' <param name="code"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <OperationContract()>
    Function GetBudgetConceptByValidity(code As String, ValidityId As String, audit As AuditMessage) As Concept

    ''' <summary>
    ''' Elimina un concepto
    ''' </summary>
    ''' <param name="Concept">La entidad</param>
    ''' <returns></returns>
    ''' <exception cref="System.ArgumentNullException">company Vacio</exception>
    <OperationContract()>
    Function DeleteBudgetConcept(Concept As Concept, audit As AuditMessage) As ActionResult

    ''' <summary>
    ''' Guarda o Actualiza un concepto
    ''' </summary>
    ''' <param name="Concept">la entidad</param>
    ''' <returns></returns>
    ''' <exception cref="System.ArgumentNullException">entidad Vacio</exception>
    <OperationContract()>
    Function SaveBudgetConcept(Concept As Concept, idSequense As Int64, audit As AuditMessage) As ActionResult(Of Concept)

    ''' <summary>
    ''' cambia el estado de la entidad.
    ''' </summary>
    ''' <param name="code">The code.</param>
    ''' <param name="state">if set to <c>true</c> [state].</param>
    ''' <returns></returns>
    <OperationContract()>
    Function ChangeStateBudgetConcept(code As String, validityId As Integer, state As Boolean, audit As AuditMessage) As Domain.Base.Entities.ActionResult(Of Domain.Entities.Concept)
End Interface

