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

Public Class EstimateCostRepository
    Inherits GenericRepository(Of CostEstimation)
    Implements IEstimateCostRepository

    ''' <summary>
    ''' The _context
    ''' </summary>
    Private _context As IGlobalModelUnitOfWork

    Public Sub New(ByVal context As IGlobalModelUnitOfWork)
        MyBase.New(context)
        _context = context
    End Sub

    Public Function GetCostEstimationById(id As Integer) As CostEstimation Implements IEstimateCostRepository.GetCostEstimationById
        Return (From e In _context.CostEstimation Where e.Id = id Select e).FirstOrDefault()
    End Function

    Public Function GetCostEstimationByYearMonth(year As Integer, month As Integer) As List(Of CostEstimation) Implements IEstimateCostRepository.GetCostEstimationByYearMonth
        Return (From e In _context.CostEstimation Where e.Year = year AndAlso e.Month = month Select e).ToList()
    End Function

    Public Function GetSpEstimateCost(distributionType As Byte, containerDGH As String, containerPayrollModule As Boolean, OnlySimulate As Boolean, PreviusDataXml As String, usercode As String) As List(Of SP_EstimateCost_Result) Implements IEstimateCostRepository.GetSpEstimateCost
        CType(_context, IObjectContextAdapter).ObjectContext.CommandTimeout = 3600
        Return _context.SP_EstimateCost(distributionType, containerDGH, containerPayrollModule, OnlySimulate, PreviusDataXml, usercode).ToList()
    End Function

    Public Function HasMonthClosed() As Boolean Implements IEstimateCostRepository.HasMonthClosed
        Dim result As Int32 = (From ce As CostEstimation In _context.CostEstimation Select ce).Count()
        Return If(result > 0, True, False)
    End Function

    Public Sub AddEntity1(item As CostEstimation) Implements Domain.Base.IRepository(Of CostEstimation).AddEntity

    End Sub

    Public Sub AttachEntity1(item As CostEstimation) Implements Domain.Base.IRepository(Of CostEstimation).AttachEntity

    End Sub

    Public Sub DeleteEntity1(item As CostEstimation) Implements Domain.Base.IRepository(Of CostEstimation).DeleteEntity

    End Sub

    Public Sub DeleteVirtual1(item As CostEstimation) Implements Domain.Base.IRepository(Of CostEstimation).DeleteVirtual

    End Sub

    Public Function GetAll1() As IEnumerable(Of CostEstimation) Implements Domain.Base.IRepository(Of CostEstimation).GetAll

    End Function


    Public Sub SaveEntity1(item As CostEstimation) Implements Domain.Base.IRepository(Of CostEstimation).SaveEntity

    End Sub

    Public ReadOnly Property UnitWork1 As Domain.Base.IUnitWork Implements Domain.Base.IRepository(Of CostEstimation).UnitWork
        Get

        End Get
    End Property

    Public Sub UpdateEntity1(item As CostEstimation) Implements Domain.Base.IRepository(Of CostEstimation).UpdateEntity

    End Sub

    Public Function ListrptEstimatingPrimaryGeneral(InitialMonth As Integer, InitialYear As Integer, LastMonth As Integer, LastYear As Integer, CenterType As Integer, CodePCenterIni As String, CodePCenterFin As String, Status As Integer) As List(Of SP_ReportGeneralProfitabilityTotalCostCx_Result) Implements IEstimateCostRepository.ListrptEstimatingPrimaryGeneral
        Dim result = _context.SP_ReportGeneralProfitabilityTotalCostCx(InitialMonth, InitialYear, LastMonth, LastYear, CenterType, CodePCenterIni, CodePCenterFin, Status).ToList()
        Return result
    End Function
End Class
