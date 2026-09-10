Public Interface IPaymentsRevaluationRepository

    ''' <summary>
    ''' Ejecuta el metodo de revaluación de CxP
    ''' </summary>
    ''' <param name="Month"></param>
    ''' <param name="Year"></param>
    ''' <param name="Status"></param>
    ''' <param name="UserCode"></param>
    ''' <returns></returns>
    Function CalculatePortfolioRevaluation(Month As Integer, Year As Integer, Status As Integer, UserCode As String) As List(Of RevaluationResult)

End Interface
