'***********************************************************************
' Assembly         : Infrastructure.Data.TreasuryRepositiry
' Author           : Diego Andrés Roldán Lozano
' Created          : 11-12-2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Infrastructure.Data.Base
Imports Domain.Entities

Public Class ProductionCenterRepository
    Inherits GenericRepository(Of ProductionCenter)
    Implements IProductionCenterRepository


    ''' <summary>
    ''' The _context
    ''' </summary>
    Private _context As IGlobalModelUnitOfWork

    Public Sub New(ByVal context As IGlobalModelUnitOfWork)
        MyBase.New(context)
        _context = context
    End Sub

    ''' <summary>
    ''' Obtiene un centro de produccion por codigo
    ''' </summary>
    ''' <param name="code"></param>
    ''' <returns></returns>
    ''' <exception cref="System.ArgumentNullException">Code</exception>
    Public Function GetProductionCenter(code As String) As ProductionCenter Implements IProductionCenterRepository.GetProductionCenter
        If String.IsNullOrEmpty(code) Then
            Throw New ArgumentNullException("Code")
        End If
        Dim query = (From o In _context.ProductionCenter.Include("ProductionCenterHomologation").Include("ProductionCenterCostCenter") Where o.Code.Equals(code) Select o).FirstOrDefault()
        If query IsNot Nothing AndAlso query.Id > 0 Then
            query.OriginalValue = (From o In _context.ProductionCenter.AsNoTracking() Where o.Code.Equals(code) Select o).FirstOrDefault()
            query.NullTextOrganizationalStructure = (From o In _context.OrganizationalStructureOfCosts Where o.Id = query.OrganizationalStructureOfCostId Select String.Concat(o.Code, " - ", o.Name)).FirstOrDefault()

            Return query
        Else
            Return New ProductionCenter()
        End If
    End Function

    ''' <summary>
    ''' Obtiene centro de produccion por id
    ''' </summary>
    ''' <param name="id"></param>
    ''' <returns></returns>
    ''' <exception cref="System.ArgumentNullException">Id</exception>
    Public Function GetProductionCenterById(id As Integer) As ProductionCenter Implements IProductionCenterRepository.GetProductionCenterById
        If id = 0 Then
            Throw New ArgumentNullException("Id")
        End If
        Dim query = (From o In _context.ProductionCenter Where o.Id = id Select o).FirstOrDefault()
        If query IsNot Nothing AndAlso query.Id > 0 Then
            query.OriginalValue = (From o In _context.ProductionCenter.AsNoTracking() Where o.Id = id Select o).FirstOrDefault()
            Return query
        Else
            Return New ProductionCenter()
        End If
    End Function

    ''' <summary>
    ''' Lista el Reporte Resumido de las Operaciones
    ''' </summary>
    ''' <param name="InitialMonth"></param>
    ''' <param name="EndMonth"></param>
    ''' <param name="Year"></param>
    ''' <param name="InitialCodeCenter"></param>
    ''' <param name="EndCodeCenter"></param>
    ''' <param name="Container"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListrptResultProductionCostsExpenses(InitialMonth As Integer, EndMonth As Integer, Year As Integer, InitialCodeCenter As String, EndCodeCenter As String, Container As String) As List(Of SP_ReportResultProductionCostsExpenses_Result) Implements IProductionCenterRepository.ListrptResultProductionCostsExpenses
        Dim result = _context.SP_ReportResultProductionCostsExpenses(InitialMonth, EndMonth, Year, InitialCodeCenter, EndCodeCenter, Container).ToList()
        Return result
    End Function

    ''' <summary>
    ''' Lista el Reporte Detallado de las Operaciones
    ''' </summary>
    ''' <param name="DateStart"></param>
    ''' <param name="DateEnd"></param>
    ''' <param name="InitialCodeCenter"></param>
    ''' <param name="EndCodeCenter"></param>
    ''' <param name="Container"></param>
    ''' <param name="DetailType"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListrptResultProductionCostsExpensesDetail(DateStart As Date, DateEnd As Date, InitialCodeCenter As String, EndCodeCenter As String, Container As String, DetailType As Integer) As List(Of SP_ReportResultProductionCostsExpensesDetail_Result) Implements IProductionCenterRepository.ListrptResultProductionCostsExpensesDetail
        Dim result = _context.SP_ReportResultProductionCostsExpensesDetail(DateStart.ToString("yyyy-MM-dd"), DateEnd.ToString("yyyy-MM-dd"), InitialCodeCenter, EndCodeCenter, Container, DetailType).ToList()
        Return result
    End Function

    ''' <summary>
    ''' Lista todos los centros de producción
    ''' </summary>
    ''' <returns></returns>
    Public Function ListProductionCenter() As List(Of ProductionCenter) Implements IProductionCenterRepository.ListProductionCenter
        Return (From pc In _context.ProductionCenter Select pc).ToList()
    End Function

    Public Function GetProductionCenterByCostCenterOid(costCenterOid As Integer) As ProductionCenter Implements IProductionCenterRepository.GetProductionCenterByCostCenterOid
        Return (From pc In _context.ProductionCenter.AsNoTracking() Where pc.ProductionCenterCostCenter.Any(Function(x) x.CostCenterId = costCenterOid) Select pc).FirstOrDefault()
    End Function

    Public Function GetProductionCenterByCostCenterOidList(list As List(Of Integer)) As List(Of ProductionCenter) Implements IProductionCenterRepository.GetProductionCenterByCostCenterOidList
        Return (From p In _context.ProductionCenterCostCenter.AsNoTracking()
                Where list.Contains(p.CostCenterId) Select p.ProductionCenter).ToList()
    End Function

    Public Function ListReportOperatingResult(InitialMonth As Integer, EndMonth As Integer, Year As Integer, Container As String, CodePCenterIni As String, CodePCenterFin As String, StructureOfCostId As Integer) As List(Of SP_ReportOperatingResult_Result) Implements IProductionCenterRepository.ListReportOperatingResult
        Dim result = _context.SP_ReportOperatingResult(InitialMonth, EndMonth, Year, Container, CodePCenterIni, CodePCenterFin, StructureOfCostId).ToList()
        Return result
    End Function

    Public Function ListReportOperatingResultsByOrganizationalStructure(InitialMonth As Integer, EndMonth As Integer, Year As Integer, Container As String, CodePCenterIni As String, CodePCenterFin As String, StructureOfCostId As Integer) As List(Of SP_ReportOperatingResultsByOrganizationalStructure_Result) Implements IProductionCenterRepository.ListReportOperatingResultsByOrganizationalStructure
        Dim result = _context.SP_ReportOperatingResultsByOrganizationalStructure(InitialMonth, EndMonth, Year, Container, CodePCenterIni, CodePCenterFin, StructureOfCostId).ToList()
        Return result
    End Function

    Public Function ListReportOperatingResultsByOrganizationalStatisticalGraphics(InitialMonth As Integer, EndMonth As Integer, Year As Integer, Container As String, CodePCenterIni As String, CodePCenterFin As String, StructureOfCostId As Integer) As List(Of SP_ReportOperatingResultsByOrganizationalStatisticalGraphics_Result) Implements IProductionCenterRepository.ListReportOperatingResultsByOrganizationalStatisticalGraphics
        Dim result = _context.SP_ReportOperatingResultsByOrganizationalStatisticalGraphics(InitialMonth, EndMonth, Year, Container, CodePCenterIni, CodePCenterFin, StructureOfCostId).ToList()
        Return result
    End Function
End Class