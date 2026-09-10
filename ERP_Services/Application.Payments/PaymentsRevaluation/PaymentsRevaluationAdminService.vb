Imports Domain.Entities
Imports System.Transactions
Imports Infrastructure.CrossCutting.Base
Imports System.Text
Public Class PaymentsRevaluationAdminService
    Implements IPaymentsRevaluationAdminService

#Region "Variables"
    Private _paymentsRevaluationRepository As IPaymentsRevaluationRepository
#End Region

#Region "Builder"
    Public Sub New(paymentsRevaluationRepository As IPaymentsRevaluationRepository)

        If paymentsRevaluationRepository Is Nothing Then
            Throw New ArgumentNullException("portfolioRevaluationRepository")
        End If

        _paymentsRevaluationRepository = paymentsRevaluationRepository
    End Sub
#End Region

#Region "Methods"
    ''' <summary>
    ''' Ejecuta el metodo de revaluación de CxP
    ''' </summary>
    ''' <param name="Month"></param>
    ''' <param name="Year"></param>
    ''' <param name="Status"></param>
    ''' <param name="UserCode"></param>
    ''' <returns></returns>
    Public Function CalculateRevaluation(Month As Integer, Year As Integer, Status As Integer, UserCode As String) As List(Of RevaluationResult) Implements IPaymentsRevaluationAdminService.CalculateRevaluation
        Dim result As List(Of RevaluationResult)
        Dim txSettings As New TransactionOptions()
        txSettings.Timeout = TransactionManager.MaximumTimeout
        txSettings.IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted
        Using transaction As New TransactionScope(TransactionScopeOption.Required, txSettings)
            Try
                result = _paymentsRevaluationRepository.CalculatePortfolioRevaluation(Month, Year, Status, UserCode)

                If result.Count() > 0 And result.Any(Function(x) x.MessageCode <> 0) Then
                    Return result
                End If

                transaction.Complete()
            Catch ex As Exception
                transaction.Dispose()
                Throw ex
            End Try
        End Using

        Return result
    End Function
#End Region

#Region "IDisposable Support"
    Private disposedValue As Boolean ' Para detectar llamadas redundantes

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not disposedValue Then
            If disposing Then
            End If

            _paymentsRevaluationRepository = Nothing
            IndigoGC.Execute()
        End If
        disposedValue = True
    End Sub

    ' Visual Basic agrega este código para implementar correctamente el patrón descartable.
    Public Sub Dispose() Implements IDisposable.Dispose
        Dispose(True)
        GC.SuppressFinalize(Me)
    End Sub

#End Region
End Class
