Imports Domain.Base.Entities

Public Interface IRevenueRecognitionAdminService
    Inherits IDisposable

    Function GenerateRecognition(careGroupXml As String, operatingUnitId As Integer, recognitionDate As DateTime, userCode As String) As ActionResult

    Function GenerateRecognitionByCareGroup(careGroupId As Integer, careGroupTotal As Decimal, operatingUnitId As Integer, recognitionDate As DateTime, userCode As String) As ActionResult
    Function ReverseRecognition(revenueRecognitionId As Integer, userCode As String) As ActionResult
End Interface
