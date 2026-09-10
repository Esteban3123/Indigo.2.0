'***********************************************************************
' Assembly         : Domain.InteropCost
' Author           : Diego Andrés Roldán Lozano
' Created          : 11-12-2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************
Imports Domain.Base
Imports Domain.Entities

Public Interface ICostProductionCenterRepository
    Inherits IRepository(Of CostProductionCenter)

    ''' <summary>
    ''' Obtiene un centro de produccion por codigo
    ''' </summary>
    ''' <returns></returns>
    Function GetCostProductionCenter(ByVal code As String) As CostProductionCenter

    ''' <summary>
    ''' Obtiene centro de produccion por id
    ''' </summary>
    ''' <returns></returns>
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
    ''' Declara la interfaz para Listar la información de costo por Unidad de Medida
    ''' </summary>
    ''' <param name="initialMonth"></param>
    ''' <param name="initialYear"></param>
    ''' <param name="endMonth"></param>
    ''' <param name="endYear"></param>
    ''' <param name="initialMeasurementUnitCode"></param>
    ''' <param name="endMeasurementUnitCode"></param>
    ''' <param name="ProductionCenterId"></param>
    ''' <returns></returns>
    Function ListReportCostCostMeasurementUnit(initialMonth As Integer, initialYear As Integer, endMonth As Integer, endYear As Integer, initialMeasurementUnitCode As String, endMeasurementUnitCode As String, ByVal ProductionCenterId As Integer) As List(Of spCostReportCostMeasurementUnit_Result)

    ''' <summary>
    ''' Valida que los centros de costo que tenga la entidad no existan en otro centro de producción
    ''' </summary>
    ''' <param name="ListInfo"></param>
    ''' <returns></returns>
    Function ValidateCostCenterIds(ListInfo As List(Of CostProductionCenterCostCenter), IdCurrent As String) As String

End Interface