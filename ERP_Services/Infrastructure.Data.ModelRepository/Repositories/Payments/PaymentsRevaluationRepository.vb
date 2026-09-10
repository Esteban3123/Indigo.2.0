Imports Domain.Entities
Imports Infrastructure.Data.Base

Public Class PaymentsRevaluationRepository
    Inherits BaseRepository
    Implements IPaymentsRevaluationRepository

#Region "Builder"

    'contexto de cartera
    Private _context As IGlobalModelUnitOfWork

    Public Sub New(ByVal contex As IGlobalModelUnitOfWork)
        MyBase.New(contex)
        _context = contex
    End Sub

#End Region
    ''' <summary>
    ''' Ejecuta el metodo de revaluación de CxP
    ''' </summary>
    ''' <param name="Month"></param>
    ''' <param name="Year"></param>
    ''' <param name="Status"></param>
    ''' <param name="UserCode"></param>
    ''' <returns></returns>
    Public Function CalculatePortfolioRevaluation(Month As Integer, Year As Integer, Status As Integer, UserCode As String) As List(Of RevaluationResult) Implements IPaymentsRevaluationRepository.CalculatePortfolioRevaluation
        Dim parameters As IEnumerable(Of (String, Object)) = {
                    ("@month", Month), ("@year", Year), ("@status", Status), ("@userCode", UserCode)
                }

        Dim result = ExecuteStoredProcedure(Of RevaluationResult)("[Payments].[SP_CurrencyRevaluation]", parameters)

        Return result
    End Function
End Class
