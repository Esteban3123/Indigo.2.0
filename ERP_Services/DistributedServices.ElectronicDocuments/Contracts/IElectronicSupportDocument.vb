Imports System.ServiceModel
Imports Domain.Base.Entities

<ServiceModel.ServiceContract()>
Public Interface IElectronicSupportDocument
    <OperationContract()>
    Function ExecuteProcessDocumentSupport() As Task(Of ActionResult(Of String))
End Interface
