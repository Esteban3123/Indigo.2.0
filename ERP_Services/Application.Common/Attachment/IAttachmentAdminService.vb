#Region "Imports"

Imports Domain.Base.Entities
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base

#End Region

Public Interface IAttachmentAdminService
    Inherits IDisposable

    ''' <summary>
    ''' Obtiene el documento adjunto por Id
    ''' </summary>
    ''' <param name="id"></param>
    ''' <returns></returns>
    Function GetAttachmentById(ByVal id As Integer) As ActionResult(Of Attachment)

    ''' <summary>
    ''' Obtiene los documentos por formulario y entidad
    ''' </summary>
    ''' <param name="formId"></param>
    ''' <param name="entityName"></param>
    ''' <param name="entityId"></param>
    ''' <returns></returns>
    Function GetAttachmentsByFormAndEntity(ByVal formId As Integer, ByVal entityName As String, ByVal entityId As Integer, ByVal withTop As Boolean) As ActionResult(Of List(Of Attachment))

    ''' <summary>
    ''' Graba un archivo adjunto
    ''' </summary>
    ''' <param name="attachment">Archivo adjunto</param>
    ''' <returns></returns>
    Function SaveAttachment(ByVal attachment As Attachment, ByVal session As SessionValues) As ActionResult(Of Attachment)

    ''' <summary>
    ''' Elimina un archivo adjunto
    ''' </summary>
    ''' <param name="attachment">Archivo adjunto</param>
    ''' <returns></returns>
    Function DeleteAttachment(ByVal attachment As Attachment, ByVal session As SessionValues) As ActionResult(Of Attachment)

End Interface
