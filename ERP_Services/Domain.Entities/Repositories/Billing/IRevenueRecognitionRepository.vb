Imports Domain.Base
Imports Domain.Entities

Public Interface IRevenueRecognitionRepository
    Inherits IRepository(Of RevenueControl)
    Function SP_GenerateRecognition(careGroupId As Integer, careGroupTotal As Decimal, operatingUnitId As Integer, recognitionDate As DateTime, userCode As String) As SP_GenerateRecognition_Result
    Function SP_ReverseRecognition(revenueRecognitionId As Integer, userCode As String) As SP_ReverseRecognition_Result
End Interface
