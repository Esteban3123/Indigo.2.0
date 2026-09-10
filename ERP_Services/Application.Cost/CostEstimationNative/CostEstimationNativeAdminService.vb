'***********************************************************************
' Assembly         : Application.InteropCost
' Author           : Diego Andrés Roldán Lozano
' Created          : 23-02-2016
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Entities
Imports Domain.Base
Imports Infrastructure.CrossCutting.Exceptions
Imports Application.Base
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities
Imports System.Data.Entity.Infrastructure
Imports System.Data.Entity.Core
Imports Infrastructure.CrossCutting.Resources
Imports System.Transactions
Imports Application.Cost

Public Class CostEstimationNativeAdminService
    Implements ICostEstimationNativeAdminService

#Region "Fields"

    ''' <summary>
    ''' Repositorio de gastos generales
    ''' </summary>
    Private _costEstimationRepository As IEstimateCostNativeRepository

#End Region

#Region "Methods"

    Public Sub New(costEstimationRepository As IEstimateCostNativeRepository)
        If costEstimationRepository Is Nothing Then
            Throw New ArgumentNullException("costEstimationRepository")
        End If
        _costEstimationRepository = costEstimationRepository
    End Sub

#End Region

    Public Function GetCostEstimationById(id As Integer) As CostEstimationNative Implements ICostEstimationNativeAdminService.GetCostEstimationById
        Try
            Return _costEstimationRepository.GetCostEstimationById(id)
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return Nothing
        End Try
    End Function

    Public Function GetCostEstimationByYearMonth(year As Integer, month As Integer) As List(Of CostEstimationNative) Implements ICostEstimationNativeAdminService.GetCostEstimationByYearMonth
        Try
            Return _costEstimationRepository.GetCostEstimationByYearMonth(year, month)
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return Nothing
        End Try
    End Function

    Public Function GetSpEstimateCostNative(Year As Integer, Month As Integer, distributionType As Byte, OnlySimulate As Boolean, containerPayrollModule As Boolean, PreviusDataXml As String, usercode As String) As List(Of SP_EstimateCostNative_Result) Implements ICostEstimationNativeAdminService.GetSpEstimateCostNative
        Try
            Return _costEstimationRepository.GetSpEstimateCostNative(Year, Month, distributionType, OnlySimulate, containerPayrollModule, PreviusDataXml, usercode)
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return Nothing
        End Try
    End Function

    Public Function ReverseEstimateCostNative(Year As Integer, Month As Integer, Usercode As String) As ActionResult Implements ICostEstimationNativeAdminService.ReverseEstimateCostNative
        Dim unitOfWork As IUnitWork = Me._costEstimationRepository.UnitWork

        Dim txSettings As New TransactionOptions()
        txSettings.Timeout = TransactionManager.MaximumTimeout
        txSettings.IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted
        Using transaction As New TransactionScope(TransactionScopeOption.Required, txSettings)
            Try
                Dim resultStore = Me._costEstimationRepository.SP_ReverseEstimateCostNative(Year, Month, Usercode)
                If resultStore.CodeMessage <> 0 Then
                    unitOfWork.RollbackChanges()
                    transaction.Dispose()
                    Return New ActionResult With {.StateResult = False, .Message = resultStore.Message}
                End If

                transaction.Complete()
                Return New ActionResult With {.StateResult = True, .Message = resultStore.Message}
            Catch ex As OptimisticConcurrencyException
                unitOfWork.RollbackChanges()
                transaction.Dispose()
                Return New ActionResult With {.StateResult = False, .MessageResult = {"-999"}.ToList()}
            Catch ex As Exception
                unitOfWork.RollbackChanges()
                transaction.Dispose()
                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
                Return New ActionResult With {.StateResult = False, .Message = Utils.GetInnerExceptionMessageToString(ex)}
            End Try
        End Using
    End Function

    Public Function ListrptEstimatingPrimaryGeneral(InitialMonth As Integer, InitialYear As Integer, LastMonth As Integer, LastYear As Integer, CenterType As Integer, CodePCenterIni As String, CodePCenterFin As String, Status As Integer) As List(Of SP_CostReportGeneralProfitabilityTotalCostCx_Result) Implements ICostEstimationNativeAdminService.ListrptEstimatingPrimaryGeneral
        Try
            Return _costEstimationRepository.ListrptEstimatingPrimaryGeneral(InitialMonth, InitialYear, LastMonth, LastYear, CenterType, CodePCenterIni, CodePCenterFin, Status)
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New List(Of SP_CostReportGeneralProfitabilityTotalCostCx_Result)()
        End Try
    End Function

#Region "IDisposable Support"
    Private disposedValue As Boolean ' Para detectar llamadas redundantes

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not disposedValue Then
            If disposing Then

            End If
            _costEstimationRepository = Nothing
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