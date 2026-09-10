Imports Domain.Base.Entities

Public Interface IElectronicDocumentSupportAdminService
    Inherits IDisposable
    Function ExecuteProcessElectronicSupportDocument() As Task(Of ActionResult(Of String))
End Interface
