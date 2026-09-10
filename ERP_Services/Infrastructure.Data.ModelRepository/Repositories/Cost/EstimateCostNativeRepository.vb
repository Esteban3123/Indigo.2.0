'***********************************************************************
' Assembly         : Infrastructure.Data.InteropCostRepositiry
' Author           : Diego Andrés Roldán Lozano
' Created          : 21-04-2016
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Infrastructure.Data.Base
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Resources
Imports System.Data.Entity.Infrastructure

Public Class EstimateCostNativeRepository
    Inherits GenericRepository(Of CostEstimationNative)
    Implements IEstimateCostNativeRepository

    ''' <summary>
    ''' The _context
    ''' </summary>
    Private _context As IGlobalModelUnitOfWork

    Public Sub New(ByVal context As IGlobalModelUnitOfWork)
        MyBase.New(context)
        _context = context
    End Sub

    Public Function GetCostEstimationById(id As Integer) As CostEstimationNative Implements IEstimateCostNativeRepository.GetCostEstimationById
        Return (From e In _context.CostEstimationNative Where e.Id = id Select e).FirstOrDefault()
    End Function

    Public Function GetCostEstimationByYearMonth(year As Integer, month As Integer) As List(Of CostEstimationNative) Implements IEstimateCostNativeRepository.GetCostEstimationByYearMonth
        Return (From e In _context.CostEstimationNative Where e.Year = year AndAlso e.Month = month Select e).ToList()
    End Function

    Public Function GetSpEstimateCostNative(Year As Integer, Month As Integer, distributionType As Byte, OnlySimulate As Boolean, containerPayrollModule As Boolean, PreviusDataXml As String, usercode As String) As List(Of SP_EstimateCostNative_Result) Implements IEstimateCostNativeRepository.GetSpEstimateCostNative
        CType(_context, IObjectContextAdapter).ObjectContext.CommandTimeout = 3600
        Return _context.SP_EstimateCostNative(Year, Month, distributionType, OnlySimulate, containerPayrollModule, PreviusDataXml, usercode).ToList()
    End Function

    Public Function SP_ReverseEstimateCostNative(Year As Integer, Month As Integer, Usercode As String) As SP_ReverseEstimateCostNative_Result Implements IEstimateCostNativeRepository.SP_ReverseEstimateCostNative
        DirectCast(_context, IObjectContextAdapter).ObjectContext.CommandTimeout = 3600
        Return _context.SP_ReverseEstimateCostNative(Year, Month, Usercode).SingleOrDefault
    End Function

    Public Function ListrptEstimatingPrimaryGeneral(InitialMonth As Integer, InitialYear As Integer, LastMonth As Integer, LastYear As Integer, CenterType As Integer, CodePCenterIni As String, CodePCenterFin As String, Status As Integer) As List(Of SP_CostReportGeneralProfitabilityTotalCostCx_Result) Implements IEstimateCostNativeRepository.ListrptEstimatingPrimaryGeneral
        Dim result = _context.SP_CostReportGeneralProfitabilityTotalCostCx(InitialMonth, InitialYear, LastMonth, LastYear, CenterType, CodePCenterIni, CodePCenterFin, Status).ToList()
        Return result
    End Function
End Class
