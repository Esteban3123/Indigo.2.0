#Region "Imports"

Imports Domain.Base

#End Region

Public Interface IAttachmentRepository
    Inherits IRepository(Of Attachment)

    ''' <summary>
    ''' Obtiene el documento adjunto por Id
    ''' </summary>
    ''' <param name="Id"></param>
    ''' <returns></returns>
    Function GetAttachmentById(ByVal Id As Integer) As Attachment

    ''' <summary>
    ''' Obtiene los documentos por formulario y entidad
    ''' </summary>
    ''' <param name="FormId"></param>
    ''' <param name="EntityName"></param>
    ''' <param name="EntityId"></param>
    ''' <returns></returns>
    Function GetAttachmentsByFormAndEntity(ByVal FormId As Integer, ByVal EntityName As String, ByVal EntityId As Integer, ByVal withTop As Boolean) As List(Of Attachment)

End Interface
