Imports System.ServiceModel
Imports Domain.Base.Entities
Imports Domain.Entities

<ServiceContract()>
Public Interface IElectronicDocuments

    <OperationContract()>
    Function GetBillingAuthorizationResolution(operatingUnitId As Integer, billingAuthorization As BillingAuthorization) As ActionResult(Of BillingAuthorization)

    <OperationContract()>
    Function ExecuteProcess() As Task(Of ActionResult(Of String))

    <OperationContract()>
    Function ExecuteSendMailProcess() As Task(Of ActionResult(Of String))

End Interface