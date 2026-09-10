#Region "Imports"

Imports Application.Payments
Imports Domain.Entities
Imports Microsoft.Practices.Unity

#End Region

Partial Public Class PaymentsService

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="Month"></param>
    ''' <param name="Year"></param>
    ''' <param name="Status"></param>
    ''' <param name="UserCode"></param>
    ''' <returns></returns>
    Public Function CalculateRevaluation(ByVal Month As Integer, ByVal Year As Integer, ByVal Status As Integer, ByVal UserCode As String) As List(Of RevaluationResult) Implements IPaymentsServiceRevaluation.CalculateRevaluation
        Using service As IPaymentsRevaluationAdminService = Container.Current.Resolve(Of IPaymentsRevaluationAdminService)()
            Return service.CalculateRevaluation(Month, Year, Status, UserCode)
        End Using
    End Function


End Class
