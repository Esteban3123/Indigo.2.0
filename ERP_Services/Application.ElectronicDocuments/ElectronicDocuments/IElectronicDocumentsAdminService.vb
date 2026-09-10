Imports Domain.Base.Entities
Imports Domain.Entities

Public Interface IElectronicDocumentsAdminService
    Inherits IDisposable

    Function GetBillingAuthorizationResolution(operatingUnitId As Integer, billingAuthorization As BillingAuthorization) As ActionResult(Of BillingAuthorization)

    Function ExecuteProcess() As Task(Of ActionResult(Of String))

    Function ExecuteSendMailProcess() As Task(Of ActionResult(Of String))

End Interface