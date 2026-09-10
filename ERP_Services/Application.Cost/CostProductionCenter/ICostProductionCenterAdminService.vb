'***********************************************************************
' Assembly         : Application.InteropCost
' Author           : Diego Andrés Roldán Lozano
' Created          : 11-12-2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************
Imports Domain.Base
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities

Public Interface ICostProductionCenterAdminService
    Inherits IDisposable

    ''' <summary>
    ''' Guarda un centro de produccion
    ''' </summary>
    Function SaveCostProductionCenter(ByVal productionCenter As CostProductionCenter, ByVal audit As AuditMessage, Optional ByVal idSequence As Int64 = 0) As ActionResult(Of CostProductionCenter)

    ''' <summary>
    ''' Elimina un centro de produccion
    ''' </summary>
    Function DeleteCostProductionCenter(ByVal productionCenter As CostProductionCenter, ByVal audit As AuditMessage) As ActionResult

    ''' <summary>
    ''' Updates the state.
    ''' </summary>
    Function UpdateStateCostProductionCenter(ByVal code As String, ByVal state As Boolean, ByVal audit As AuditMessage) As ActionResult(Of CostProductionCenter)

    ''' <summary>
    ''' Obtiene un centro de produccion por codigo
    ''' </summary>
    Function GetCostProductionCenter(ByVal code As String, ByVal audit As AuditMessage) As ActionResult(Of CostProductionCenter)

    ''' <summary>
    ''' Obtiene centro de produccion por id
    ''' </summary>
    Function GetCostProductionCenterById(id As Integer) As CostProductionCenter

    ''' <summary>
    ''' Lista todos los centros de producción
    ''' </summary>
    ''' <returns></returns>
    Function ListCostProductionCenter() As List(Of CostProductionCenter)

    Function GetProductionCenterByCostCenterId(costCenterId As Integer) As CostProductionCenter

    ''' <summary>
    ''' Lista todos los resultados de la operacion de REPORTE
    ''' </summary>
    ''' <returns></returns>
    Function ListReportOperatingResult(InitialMonth As Integer, ByVal EndMonth As Integer, ByVal Year As Integer, ByVal CodePCenterIni As String, ByVal CodePCenterFin As String, ByVal StructureOfCostId As Integer) As List(Of SP_CostReportOperatingResult_Result)

    ''' <summary>
    ''' Lista todos los resultados de la operacion de REPORTE
    ''' </summary>
    ''' <returns></returns>
    Function ListReportOperatingResultsByOrganizationalStructure(InitialMonth As Integer, ByVal EndMonth As Integer, ByVal Year As Integer, ByVal Container As String, ByVal CodePCenterIni As String, ByVal CodePCenterFin As String, ByVal StructureOfCostId As Integer) As List(Of SP_CostReportOperatingResultsByOrganizationalStructure_Result)

    ''' <summary>
    ''' Lista todos los resultados de la operacion de REPORTE
    ''' </summary>
    ''' <returns></returns>
    Function ListReportOperatingResultsByOrganizationalStatisticalGraphics(InitialMonth As Integer, ByVal EndMonth As Integer, ByVal Year As Integer, ByVal Container As String, ByVal CodePCenterIni As String, ByVal CodePCenterFin As String, ByVal StructureOfCostId As Integer) As List(Of SP_CostReportOperatingResultsByOrganizationalStatisticalGraphics_Result)

    ''' <summary>
    ''' Lista la informacion de costo por Unidad de Medida
    ''' </summary>
    ''' <param name="initialMonth"></param>
    ''' <param name="initialYear"></param>
    ''' <param name="endMonth"></param>
    ''' <param name="endYear"></param>
    ''' <param name="initialMeasurementUnitCode"></param>
    ''' <param name="endMeasurementUnitCode"></param>
    ''' <param name="ProductionCenterId"></param>
    ''' <returns></returns>
    Function ListReportCostCostMeasurementUnit(initialMonth As Integer, initialYear As Integer, endMonth As Integer, endYear As Integer, initialMeasurementUnitCode As String, endMeasurementUnitCode As String, ProductionCenterId As Integer) As List(Of spCostReportCostMeasurementUnit_Result)

    ''' <summary>
    ''' Metodo que realiza el llamado al stored Procedure SP_CostReportOperatingResultProductionCenter realizado para cargar los datos del reporte de Estructura Organizacional ProductionCenter de costo nativo
    ''' </summary>
    ''' <param name="InitialMonth"></param>
    ''' <param name="EndMonth"></param>
    ''' <param name="Year"></param>
    ''' <param name="CodePCenterIni"></param>
    ''' <param name="CodePCenterFin"></param>
    ''' <param name="OrderBy"></param>
    ''' <param name="Session"></param>
    ''' <returns></returns>
    Function GetCostListReportOperatingProductionCenter(InitialMonth As Integer, EndMonth As Integer, Year As Integer, CodePCenterIni As String, CodePCenterFin As String, OrderBy As Integer, Session As SessionValues) As DataSet

End Interface
