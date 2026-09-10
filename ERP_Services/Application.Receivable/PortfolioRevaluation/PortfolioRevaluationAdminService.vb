'***********************************************************************
' Assembly         : Application.Treasury
' Author           : Carlos Ernesto Cordoba
' Created          : 04-07-2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "imports"
Imports Domain.Entities
Imports Domain.Base
Imports Infrastructure.CrossCutting.Exceptions
Imports Application.Base
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities
Imports System.Data.Entity.Infrastructure
Imports System.Data.Entity.Core
Imports System.Transactions
Imports Application.Accounting
Imports System.Data.Entity.Validation
Imports Domain.Entities.Service
Imports Infrastructure.CrossCutting.Resources
Imports Application.Portfolio
Imports Application.Payments

Imports System.Text

#End Region

Public Class PortfolioRevaluationAdminService
    Implements IPortfolioRevaluationAdminService

#Region "Fileds"

    Private _portfolioRevaluationRepository As IPortfolioRevaluationRepository

#End Region

#Region "Builder"

    Public Sub New(portfolioRevaluationRepository As IPortfolioRevaluationRepository)

        If portfolioRevaluationRepository Is Nothing Then
            Throw New ArgumentNullException("portfolioRevaluationRepository")
        End If

        _portfolioRevaluationRepository = portfolioRevaluationRepository
    End Sub

#End Region

#Region "Methods"

    Public Function CalculateRevaluation(Month As Integer, Year As Integer, Status As Integer, UserCode As String) As List(Of RevaluationResult) Implements IPortfolioRevaluationAdminService.CalculateRevaluation
        Dim result As List(Of RevaluationResult)
        Dim txSettings As New TransactionOptions()
        txSettings.Timeout = TransactionManager.MaximumTimeout
        txSettings.IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted
        Using transaction As New TransactionScope(TransactionScopeOption.Required, txSettings)
            Try
                result = _portfolioRevaluationRepository.CalculatePortfolioRevaluation(Month, Year, Status, UserCode)

                If result.Exists(Function(x) x.MessageCode <> 0) Then
                    transaction.Dispose()
                    Return result.FindAll(Function(x) x.MessageCode <> 0)
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

            _portfolioRevaluationRepository = Nothing
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
