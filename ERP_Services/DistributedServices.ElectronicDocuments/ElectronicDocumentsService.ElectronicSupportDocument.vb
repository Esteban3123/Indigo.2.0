Imports Application.ElectronicDocuments
Imports Domain.Base.Entities
Imports Domain.Entities
Imports Microsoft.Practices.Unity

Partial Class ElectronicDocumentsService
    Implements IElectronicSupportDocument

    Public Async Function ExecuteProcessDocumentSupport() As Task(Of ActionResult(Of String)) Implements IElectronicSupportDocument.ExecuteProcessDocumentSupport
        Using service As IElectronicDocumentSupportAdminService = Container.Current.Resolve(Of IElectronicDocumentSupportAdminService)()
            Return Await service.ExecuteProcessElectronicSupportDocument()
        End Using
    End Function
End Class
