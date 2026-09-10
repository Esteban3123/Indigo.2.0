#Region "Imports"

Imports Application.Common
Imports Domain.Base.Entities
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.CrossCutting.IOC

#End Region

Partial Public Class CommonERPService
    Implements ICommonERPAttachment

    Public Function GetAttachmentById(id As Integer, session As SessionValues) As ActionResult(Of Attachment) Implements ICommonERPAttachment.GetAttachmentById
        Using attachmentAdminService As IAttachmentAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of IAttachmentAdminService)()
            Return attachmentAdminService.GetAttachmentById(id)
        End Using
    End Function

    Public Function GetAttachmentsByFormAndEntity(formId As Integer, entityName As String, entityId As Integer, ByVal withTop As Boolean, session As SessionValues) As ActionResult(Of List(Of Attachment)) Implements ICommonERPAttachment.GetAttachmentsByFormAndEntity
        Using attachmentAdminService As IAttachmentAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of IAttachmentAdminService)()
            Return attachmentAdminService.GetAttachmentsByFormAndEntity(formId, entityName, entityId, withTop)
        End Using
    End Function

    Public Function SaveAttachment(attachment As Attachment, session As SessionValues) As ActionResult(Of Attachment) Implements ICommonERPAttachment.SaveAttachment
        Using attachmentAdminService As IAttachmentAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of IAttachmentAdminService)()
            Return attachmentAdminService.SaveAttachment(attachment, session)
        End Using
    End Function

    Public Function DeleteAttachment(attachment As Attachment, session As SessionValues) As ActionResult(Of Attachment) Implements ICommonERPAttachment.DeleteAttachment
        Using attachmentAdminService As IAttachmentAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of IAttachmentAdminService)()
            Return attachmentAdminService.DeleteAttachment(attachment, session)
        End Using
    End Function

End Class
