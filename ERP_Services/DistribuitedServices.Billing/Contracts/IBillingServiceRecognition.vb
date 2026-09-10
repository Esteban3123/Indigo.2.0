Imports System.ServiceModel
Imports Domain.Base.Entities

<ServiceContract()>
Public Interface IBillingServiceRecognition

    <OperationContract()>
    Function GenerateRevenueRecognition(careGroupXml As String, operatingUnit As Integer, recognitionDate As DateTime, userCode As String) As ActionResult

    <OperationContract()>
    Function GenerateRecognitionByCareGroup(careGroupId As Integer, careGroupTotal As Decimal, operatingUnitId As Integer, recognitionDate As DateTime, userCode As String) As ActionResult
    <OperationContract()>
    Function ReverseRecognition(revenueRecognitionId As Integer, userCode As String) As ActionResult
End Interface
