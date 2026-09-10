'***********************************************************************
' Assembly         : Application.Treasury
' Author           : Carlos Ernesto Cordoba
' Created          : 04-07-2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "imports"
Imports System.Transactions
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base

#End Region

Public Class TreasuryRevaluationAdminService
    Implements ITreasuryRevaluationAdminService

#Region "Fileds"

    Private _TreasuryRevaluationRepository As ITreasuryRevaluationRepository

#End Region

#Region "Builder"

    Public Sub New(TreasuryRevaluationRepository As ITreasuryRevaluationRepository)

        If TreasuryRevaluationRepository Is Nothing Then
            Throw New ArgumentNullException("TreasuryRevaluationRepository")
        End If

        _TreasuryRevaluationRepository = TreasuryRevaluationRepository
    End Sub

#End Region

#Region "Methods"

    ''' <summary>
    ''' Metodo para calcular la revaluacion en el modulo de tesoreria
    ''' </summary>
    ''' <param name="Month"></param>
    ''' <param name="Year"></param>
    ''' <param name="Status"></param>
    ''' <param name="UserCode"></param>
    ''' <returns></returns>
    Public Function CalculateRevaluation(Month As Integer, Year As Integer, Status As Integer, UserCode As String) As List(Of RevaluationResult) Implements ITreasuryRevaluationAdminService.CalculateRevaluation
        Dim result As List(Of RevaluationResult)
        Dim txSettings As New TransactionOptions()
        txSettings.Timeout = TransactionManager.MaximumTimeout
        txSettings.IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted
        Using transaction As New TransactionScope(TransactionScopeOption.Required, txSettings)
            Try
                result = _TreasuryRevaluationRepository.CalculateTreasuryRevaluation(Month, Year, Status, UserCode)
                If result Is Nothing OrElse Not result?.Any() OrElse result?.Any(Function(d) d.MessageCode = "999") Then
                    transaction.Dispose()
                Else
                    transaction.Complete()
                End If
                Return result
            Catch ex As Exception
                transaction.Dispose()
                Return New List(Of RevaluationResult)(New RevaluationResult With {.MessageCode = "999", .MessageVoucher = ex.Message})
            End Try
        End Using
    End Function

#End Region

#Region "IDisposable Support"
    Private disposedValue As Boolean ' Para detectar llamadas redundantes

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not disposedValue Then
            If disposing Then
            End If

            _TreasuryRevaluationRepository = Nothing
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
