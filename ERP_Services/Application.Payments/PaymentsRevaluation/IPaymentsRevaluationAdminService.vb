Imports Domain.Entities

Public Interface IPaymentsRevaluationAdminService
    Inherits IDisposable

    ''' <summary>
    ''' Ejecuta el metodo de revaluación de CxP
    ''' </summary>
    ''' <param name="Month"></param>
    ''' <param name="Year"></param>
    ''' <param name="Status"></param>
    ''' <param name="UserCode"></param>
    ''' <returns></returns>
    Function CalculateRevaluation(ByVal Month As Integer, ByVal Year As Integer, ByVal Status As Integer, ByVal UserCode As String) As List(Of RevaluationResult)

End Interface
