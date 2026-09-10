Imports Domain.Base.Entities
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base

Public Interface IElectronicSupportDocumentAdjustmentNoteAdminService
    Inherits IDisposable

    ''' <summary>
    ''' Consulta por código
    ''' </summary>
    ''' <param name="code"></param>
    ''' <returns></returns>
    Function GetElectronicSupportDocumentAdjustmentNoteByCode(code As String, audit As AuditMessage) As ActionResult(Of ElectronicSupportDocumentAdjustmentNote)

    ''' <summary>
    ''' Consulta por id
    ''' </summary>
    ''' <param name="id"></param>
    ''' <returns></returns>
    Function GetElectronicSupportDocumentAdjustmentNoteById(id As Integer) As ElectronicSupportDocumentAdjustmentNote

    ''' <summary>
    ''' Guarda un documento de soporte
    ''' </summary>
    ''' <param name="electronicDocumentNote"></param>
    ''' <param name="audit"></param>
    ''' <param name="idSequence"></param>
    ''' <returns></returns>
    Function SaveElectronicSupportDocumentAdjusmentNote(electronicDocumentNote As ElectronicSupportDocumentAdjustmentNote, audit As AuditMessage, Optional idSequence As Long = 0) As ActionResult(Of ElectronicSupportDocumentAdjustmentNote)

    ''' <summary>
    ''' Confirma el documento
    ''' </summary>
    ''' <param name="electronicDocumentNote"></param>
    ''' <param name="audit"></param>
    ''' <param name="idSequence"></param>
    ''' <returns></returns>
    Function ConfirmElectronicSupportDocumentAdjusmentNote(electronicDocumentNote As ElectronicSupportDocumentAdjustmentNote, audit As AuditMessage, Optional idSequence As Long = 0) As ActionResult(Of ElectronicSupportDocumentAdjustmentNote)

    ''' <summary>
    ''' Anula el documento
    ''' </summary>
    ''' <param name="electronicDocumentNote"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    Function AnulateElectronicSupportDocumentAdjusmentNote(electronicDocumentNote As ElectronicSupportDocumentAdjustmentNote, audit As AuditMessage) As ActionResult(Of ElectronicSupportDocumentAdjustmentNote)

    ''' <summary>
    ''' Actualiza el estado para ser reenviado
    ''' </summary>
    ''' <param name="Ids"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    Function UpdateStateElectronicSupportDocumentsAdjusmentNote(Ids As List(Of Integer), audit As AuditMessage) As ActionResult(Of ElectronicSupportDocumentAdjustmentNote)

End Interface
