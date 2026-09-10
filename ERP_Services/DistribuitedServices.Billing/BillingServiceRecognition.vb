Imports Application.Billing
Imports DistribuitedServices.Billing
Imports Domain.Base.Entities
Imports Microsoft.Practices.Unity

Partial Public Class BillingService
    Implements IBillingServiceRecognition

    Public Function GenerateRecognitionByCareGroup(careGroupId As Integer, careGroupTotal As Decimal, operatingUnitId As Integer, recognitionDate As Date, userCode As String) As ActionResult Implements IBillingServiceRecognition.GenerateRecognitionByCareGroup
        Using service As IRevenueRecognitionAdminService = Container.Current.Resolve(Of IRevenueRecognitionAdminService)()
            Return service.GenerateRecognitionByCareGroup(careGroupId, careGroupTotal, operatingUnitId, recognitionDate, userCode)
        End Using
        'Return _recognitionAdminService.GenerateRecognitionByCareGroup(careGroupId, careGroupTotal, operatingUnitId, recognitionDate, userCode)
    End Function

    Public Function GenerateRevenueRecognition(careGroupXml As String, operatingUnit As Integer, recognitionDate As Date, userCode As String) As ActionResult Implements IBillingServiceRecognition.GenerateRevenueRecognition
        Using service As IRevenueRecognitionAdminService = Container.Current.Resolve(Of IRevenueRecognitionAdminService)()
            Return service.GenerateRecognition(careGroupXml, operatingUnit, recognitionDate, userCode)
        End Using
        'Return _recognitionAdminService.GenerateRecognition(careGroupXml, operatingUnit, recognitionDate, userCode)
    End Function

    Public Function ReverseRecognition(revenueRecognitionId As Integer, userCode As String) As ActionResult Implements IBillingServiceRecognition.ReverseRecognition
        Using service As IRevenueRecognitionAdminService = Container.Current.Resolve(Of IRevenueRecognitionAdminService)()
            Return service.ReverseRecognition(revenueRecognitionId, userCode)
        End Using
        'Return _recognitionAdminService.ReverseRecognition(revenueRecognitionId, userCode)
    End Function
End Class
