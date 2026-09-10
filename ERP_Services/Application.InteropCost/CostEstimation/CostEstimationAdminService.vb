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
Imports Domain.InteropCost
Imports Domain.InteropCost.Entities
Imports System.Transactions

Public Class CostEstimationAdminService
    Implements ICostEstimationAdminService

#Region "Fields"

    ''' <summary>
    ''' Repositorio de gastos generales
    ''' </summary>
    Private _costEstimationRepository As IEstimateCostRepository

#End Region

#Region "Methods"

    Public Sub New(costEstimationRepository As IEstimateCostRepository)
        If costEstimationRepository Is Nothing Then
            Throw New ArgumentNullException("costEstimationRepository")
        End If
        _costEstimationRepository = costEstimationRepository
    End Sub

#End Region

    Public Function GetCostEstimationById(id As Integer) As CostEstimation Implements ICostEstimationAdminService.GetCostEstimationById
        Try
            Return _costEstimationRepository.GetCostEstimationById(id)
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return Nothing
        End Try
    End Function

    Public Function GetCostEstimationByYearMonth(year As Integer, month As Integer) As List(Of CostEstimation) Implements ICostEstimationAdminService.GetCostEstimationByYearMonth
        Try
            Return _costEstimationRepository.GetCostEstimationByYearMonth(year, month)
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return Nothing
        End Try
    End Function

    Public Function GetSpEstimateCost(distributionType As Byte, containerDGH As String, containPayrollModule As Boolean, OnlySimulate As Boolean, PreviusDataXml As String, usercode As String) As List(Of SP_EstimateCost_Result) Implements ICostEstimationAdminService.GetSpEstimateCost
        Try
            Return _costEstimationRepository.GetSpEstimateCost(distributionType, containerDGH, containPayrollModule, OnlySimulate, PreviusDataXml, usercode)
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return Nothing
        End Try
    End Function

    Public Function HasMonthClosed() As Boolean Implements ICostEstimationAdminService.HasMonthClosed
        Try
            Return _costEstimationRepository.HasMonthClosed()
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return False
        End Try
    End Function

    Public Function ListrptEstimatingPrimaryGeneral(InitialMonth As Integer, InitialYear As Integer, LastMonth As Integer, LastYear As Integer, CenterType As Integer, CodePCenterIni As String, CodePCenterFin As String, Status As Integer) As List(Of SP_ReportGeneralProfitabilityTotalCostCx_Result) Implements ICostEstimationAdminService.ListrptEstimatingPrimaryGeneral
        Try
            Return _costEstimationRepository.ListrptEstimatingPrimaryGeneral(InitialMonth, InitialYear, LastMonth, LastYear, CenterType, CodePCenterIni, CodePCenterFin, Status)
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New List(Of SP_ReportGeneralProfitabilityTotalCostCx_Result)()
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