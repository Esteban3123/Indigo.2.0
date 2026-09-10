'***********************************************************************
' Assembly         : DistributedServices.Treasury
' Author           : Diego Andrés Roldán Lozano
' Created          : 03-04-2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities
Imports System.ServiceModel

<ServiceContract()>
Public Interface ITreasuryServiceNoteConcept

    ''' <summary>
    ''' Saves the note concept.
    ''' </summary>
    ''' <param name="noteConcept">The note concept.</param>
    ''' <returns></returns>
    <OperationContract()>
    Function SaveNoteConcept(noteConcept As NoteConcepts, audit As AuditMessage, idSequence As Int64) As ActionResult(Of NoteConcepts)

    ''' <summary>
    ''' Updates the state card.
    ''' </summary>
    ''' <param name="code">The code.</param>
    ''' <param name="state">if set to <c>true</c> [state].</param>
    ''' <returns></returns>
    <OperationContract()>
    Function UpdateStateNoteConcept(code As String, state As Boolean, audit As AuditMessage) As ActionResult(Of NoteConcepts)

    ''' <summary>
    ''' Deletes the note concept.
    ''' </summary>
    ''' <param name="noteConcept">The note concept.</param>
    ''' <returns></returns>
    <OperationContract()>
    Function DeleteNoteConcept(noteConcept As NoteConcepts, audit As AuditMessage) As ActionResult

    ''' <summary>
    ''' Obtiene un concepto de nota
    ''' </summary>
    ''' <param name="code">The code.</param>
    ''' <returns></returns>
    <OperationContract()>
    Function GetNoteConcept(code As String, audit As AuditMessage) As ActionResult(Of NoteConcepts)

    ''' <summary>
    ''' Obtiene un concepto de nota por id
    ''' </summary>
    ''' <param name="Id">The identifier.</param>
    ''' <returns></returns>
    <OperationContract()>
    Function GetNoteConceptById(Id As Integer, audit As AuditMessage) As NoteConcepts
    ''' <summary>
    ''' Valida e importa los elementos de excel 
    ''' </summary>
    ''' <param name="DataImport"></param>
    ''' <returns></returns>
    <OperationContract>
    Function ValidateNoteConcept(DataImport As List(Of ImportFileRow)) As ActionResult(Of List(Of TreasuryNoteDetail))
    ''' <summary>
    ''' Funcion de copiar y pegar
    ''' </summary>
    ''' <param name="Data"></param>
    ''' <returns></returns>
    <OperationContract>
    Function CopyPasteNoteConceptDetail(Data As List(Of List(Of String))) As ActionResult(Of List(Of TreasuryNoteDetail))
End Interface
