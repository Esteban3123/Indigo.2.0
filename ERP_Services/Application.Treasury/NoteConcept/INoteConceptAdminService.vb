'***********************************************************************
' Assembly         : Application.Treasury
' Author           : Diego Andrés Roldán Lozano
' Created          : 03-04-2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities

Public Interface INoteConceptAdminService
    Inherits IDisposable

    ''' <summary>
    ''' Saves the note concept.
    ''' </summary>
    ''' <param name="noteConcept">The note concept.</param>
    ''' <param name="audit">The audit.</param>
    ''' <returns></returns>
    Function SaveNoteConcept(ByVal noteConcept As NoteConcepts, ByVal audit As AuditMessage, Optional ByVal idSequence As Int64 = 0) As ActionResult(Of NoteConcepts)

    ''' <summary>
    ''' actualiza el estado del registro
    ''' </summary>
    ''' <param name="code">The code.</param>
    ''' <param name="state">if set to <c>true</c> [state].</param>
    ''' <param name="audit">The audit.</param>
    ''' <returns></returns>
    Function UpdateStateNoteConcept(ByVal code As String, ByVal state As Boolean, ByVal audit As AuditMessage) As ActionResult(Of NoteConcepts)

    ''' <summary>
    ''' Deletes the note concept.
    ''' </summary>
    ''' <param name="noteConcept">The note concept.</param>
    ''' <param name="audit">The audit.</param>
    ''' <returns></returns>
    Function DeleteNoteConcept(ByVal noteConcept As NoteConcepts, ByVal audit As AuditMessage) As ActionResult

    ''' <summary>
    ''' Obtiene un concepto de nota
    ''' </summary>
    ''' <param name="code">The code.</param>
    ''' <returns></returns>
    Function GetNoteConcept(ByVal code As String, ByVal audit As AuditMessage) As ActionResult(Of NoteConcepts)

    ''' <summary>
    ''' Obtiene un concepto de nota por id
    ''' </summary>
    ''' <param name="Id">The identifier.</param>
    ''' <returns></returns>
    Function GetNoteConceptById(ByVal Id As Integer, ByVal audit As AuditMessage) As NoteConcepts
    ''' <summary>
    ''' Se valida para importar el excel
    ''' </summary>
    ''' <param name="DataImport"></param>
    ''' <returns></returns>
    Function ValidateNoteConcept(DataImport As List(Of ImportFileRow)) As ActionResult(Of List(Of TreasuryNoteDetail))
    ''' <summary>
    ''' Funcion para copiar y pegar las notas de tesoreria
    ''' </summary>
    ''' <param name="Data"></param>
    ''' <returns></returns>
    Function CopyPasteNoteConceptDetail(Data As List(Of List(Of String))) As ActionResult(Of List(Of TreasuryNoteDetail))

End Interface
