#Region "Imports"

Imports Domain.Entities
Imports Domain.Base
Imports Infrastructure.CrossCutting.Exceptions
Imports Application.Base
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities

#End Region

Public Interface IElectronicSupportDocumentAdminService
    Inherits IDisposable
    ''' <summary>
    ''' obtiene documento soporte por id
    ''' </summary>
    ''' <param name="id"></param>
    ''' <returns></returns>
    Function GetElectronicSupportDocumentById(id As Integer) As ActionResult(Of ElectronicSupportDocument)
    ''' <summary>
    ''' obtiene un documento soporte por codigo
    ''' </summary>
    ''' <param name="code"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    Function GetElectronicSupportDocumentByCode(code As String, audit As AuditMessage) As ActionResult(Of ElectronicSupportDocument)
    ''' <summary>
    ''' guarda y confirma
    ''' </summary>
    ''' <param name="ElectronicSupportDocument"></param>
    ''' <param name="audit"></param>
    ''' <param name="idSequence"></param>
    ''' <returns></returns>
    Function SaveConfirmElectronicSupportDocument(ElectronicSupportDocument As ElectronicSupportDocument, audit As AuditMessage, Optional idSequence As Long = 0) As ActionResult

    ''' <summary>
    ''' Actualiza el estado de un documento para ser reenviado
    ''' </summary>
    ''' <param name="Ids"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    Function UpdateStateElectronicSupportDocuments(Ids As List(Of Integer), audit As AuditMessage) As ActionResult(Of ElectronicSupportDocument)
End Interface
