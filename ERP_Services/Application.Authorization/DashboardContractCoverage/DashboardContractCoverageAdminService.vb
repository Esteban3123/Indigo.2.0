'***********************************************************************
' Assembly         : Application.Contract
' Author           : Carlos Mario Arias Rubiano
' Created          : 01/07/2020
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Entities
Imports Domain.Base
Imports Infrastructure.CrossCutting.Exceptions
Imports Application.Base
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities
Imports Infrastructure.CrossCutting.Resources
Imports System.Transactions
Imports Application.Security
Imports Application.Authorization
Imports System.Text

Public Class DashboardContractCoverageAdminService
    Implements IDashboardContractCoverageAdminService

    Private _dashboardContractCoverageRepository As IDashboardContractCoverageRepository

    Public Sub New(dashboardContractCoverageRepository As IDashboardContractCoverageRepository)
        If dashboardContractCoverageRepository Is Nothing Then
            Throw New ArgumentNullException("dashboardContractCoverageRepository")
        End If
        _dashboardContractCoverageRepository = dashboardContractCoverageRepository
    End Sub

    Public Function SP_SaveContractCoverage(listTuple As List(Of Tuple(Of Integer, Integer, String)), audit As AuditMessage) As ActionResult(Of SP_SaveContractCoverage_Result) Implements IDashboardContractCoverageAdminService.SP_SaveContractCoverage
        If listTuple Is Nothing OrElse listTuple.Count = 0 Then
            Throw New ArgumentNullException("listTuple")
        End If
        Using scope As New TransactionScope(TransactionScopeOption.Required, New TransactionOptions() With {.Timeout = TransactionManager.MaximumTimeout, .IsolationLevel = IsolationLevel.ReadCommitted})
            Try
                Dim xml = ConvertEntityToXml(listTuple, audit)

                Dim result = _dashboardContractCoverageRepository.SP_SaveContractCoverage(xml)
                If result.CodeResult <> 0 Then
                    scope.Dispose()
                    Return New ActionResult(Of SP_SaveContractCoverage_Result) With {.StateResult = False, .Message = result.MessageResult}
                End If

                scope.Complete()
                Return New ActionResult(Of SP_SaveContractCoverage_Result) With {.StateResult = True, .ObjectEmbbeded = result, .Message = result.MessageResult}
            Catch ex As Exception
                scope.Dispose()
                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
                Return New ActionResult(Of SP_SaveContractCoverage_Result) With {.StateResult = False, .Message = Utils.GetInnerExceptionMessageToString(ex)}
            End Try
        End Using
    End Function

    Private Function ConvertEntityToXml(listTuple As List(Of Tuple(Of Integer, Integer, String)), audit As AuditMessage) As String
        Dim builder As New StringBuilder

        For Each item In listTuple
            builder.Append("<TableDetail>")
            builder.Append("<ServiceOrderDetailId>" & item.Item1 & "</ServiceOrderDetailId>")
            builder.Append("<ContractCoverageStatus>" & item.Item2 & "</ContractCoverageStatus>")
            builder.Append("<ContractCoverageObservations>" & item.Item3 & "</ContractCoverageObservations>")
            builder.Append("</TableDetail>")
        Next

        Return builder.ToString()
    End Function

#Region "IDisposable Support"
    Private disposedValue As Boolean ' Para detectar llamadas redundantes

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not disposedValue Then
            If disposing Then
                ' TODO: elimine el estado administrado (objetos administrados).
            End If
            _dashboardContractCoverageRepository = Nothing
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
