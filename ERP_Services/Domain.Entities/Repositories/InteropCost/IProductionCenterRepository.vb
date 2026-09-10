'***********************************************************************
' Assembly         : Domain.InteropCost
' Author           : Diego Andrés Roldán Lozano
' Created          : 11-12-2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************
Imports Domain.Base
Imports Domain.Entities

Public Interface IProductionCenterRepository
    Inherits IRepository(Of ProductionCenter)

    ''' <summary>
    ''' Obtiene un centro de produccion por codigo
    ''' </summary>
    ''' <returns></returns>
    Function GetProductionCenter(ByVal code As String) As ProductionCenter

    ''' <summary>
    ''' Obtiene centro de produccion por id
    ''' </summary>
    ''' <returns></returns>
    Function GetProductionCenterById(id As Integer) As ProductionCenter

    ''' <summary>
    ''' Lista todos los centros de producción
    ''' </summary>
    ''' <returns></returns>
    Function ListProductionCenter() As List(Of ProductionCenter)

    Function GetProductionCenterByCostCenterOid(costCenterOid As Integer) As ProductionCenter

    Function GetProductionCenterByCostCenterOidList(list As List(Of Integer)) As List(Of ProductionCenter)

    ''' <summary>
    ''' Lista todos los resultados de la operacion de REPORTES
    ''' </summary>
    ''' <returns></returns>
    Function ListrptResultProductionCostsExpenses(InitialMonth As Integer, ByVal EndMonth As Integer, ByVal Year As Integer, InitialCodeCenter As String, EndCodeCenter As String, ByVal Container As String) As List(Of SP_ReportResultProductionCostsExpenses_Result)

    ''' <summary>
    ''' Lista todos los resultados de la operacion de REPORTES Detallado
    ''' </summary>
    ''' <returns></returns>
    Function ListrptResultProductionCostsExpensesDetail(DateStart As Date, ByVal DateEnd As Date, InitialCodeCenter As String, EndCodeCenter As String, ByVal Container As String, ByVal DetailType As Integer) As List(Of SP_ReportResultProductionCostsExpensesDetail_Result)

    ''' <summary>
    ''' Lista todos los resultados de la operacion de REPORTE
    ''' </summary>
    ''' <returns></returns>
    Function ListReportOperatingResult(InitialMonth As Integer, ByVal EndMonth As Integer, ByVal Year As Integer, ByVal Container As String, ByVal CodePCenterIni As String, ByVal CodePCenterFin As String, ByVal StructureOfCostId As Integer) As List(Of SP_ReportOperatingResult_Result)

    ''' <summary>
    ''' Lista todos los resultados de la operacion de REPORTE
    ''' </summary>
    ''' <returns></returns>
    Function ListReportOperatingResultsByOrganizationalStructure(InitialMonth As Integer, ByVal EndMonth As Integer, ByVal Year As Integer, ByVal Container As String, ByVal CodePCenterIni As String, ByVal CodePCenterFin As String, ByVal StructureOfCostId As Integer) As List(Of SP_ReportOperatingResultsByOrganizationalStructure_Result)

    ''' <summary>
    ''' Lista todos los resultados de la operacion de REPORTE
    ''' </summary>
    ''' <returns></returns>
    Function ListReportOperatingResultsByOrganizationalStatisticalGraphics(InitialMonth As Integer, ByVal EndMonth As Integer, ByVal Year As Integer, ByVal Container As String, ByVal CodePCenterIni As String, ByVal CodePCenterFin As String, ByVal StructureOfCostId As Integer) As List(Of SP_ReportOperatingResultsByOrganizationalStatisticalGraphics_Result)
End Interface