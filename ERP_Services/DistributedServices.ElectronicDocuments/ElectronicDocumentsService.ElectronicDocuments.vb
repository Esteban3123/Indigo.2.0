Imports Application.ElectronicDocuments
Imports Domain.Base.Entities
Imports Domain.Entities
Imports Microsoft.Practices.Unity

Partial Class ElectronicDocumentsService
    Implements IElectronicDocuments

    Public Function GetBillingAuthorizationResolution(operatingUnitId As Integer, billingAuthorization As BillingAuthorization) As ActionResult(Of BillingAuthorization) Implements IElectronicDocuments.GetBillingAuthorizationResolution
        Using service As IElectronicDocumentsAdminService = Container.Current.Resolve(Of IElectronicDocumentsAdminService)()
            Return service.GetBillingAuthorizationResolution(operatingUnitId, billingAuthorization)
        End Using
    End Function

    Public Async Function ExecuteProcess() As Task(Of ActionResult(Of String)) Implements IElectronicDocuments.ExecuteProcess
        Using service As IElectronicDocumentsAdminService = Container.Current.Resolve(Of IElectronicDocumentsAdminService)()
            Return Await service.ExecuteProcess()
        End Using
    End Function

    Public Async Function ExecuteSendMailProcess() As Task(Of ActionResult(Of String)) Implements IElectronicDocuments.ExecuteSendMailProcess
        Using service As IElectronicDocumentsAdminService = Container.Current.Resolve(Of IElectronicDocumentsAdminService)()
            Return Await service.ExecuteSendMailProcess()
        End Using
    End Function

End Class