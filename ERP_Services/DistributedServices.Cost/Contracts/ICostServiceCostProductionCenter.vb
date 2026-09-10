Imports System.ServiceModel
Imports Domain.Entities
Imports Domain.Base.Entities
Imports Infrastructure.CrossCutting.Base

<ServiceContract()> _
Public Interface ICostServiceCostProductionCenter

    ''' <summary>
    ''' Guarda un centro de produccion
    ''' </summary>
    <OperationContract()>
    Function SaveCostProductionCenter(productionCenter As Domain.Entities.CostProductionCenter, audit As AuditMessage, idSequence As Int64) As Domain.Base.Entities.ActionResult(Of Domain.Entities.CostProductionCenter)

    ''' <summary>
    ''' Elimina un centro de produccion
    ''' </summary>
    <OperationContract()>
    Function DeleteCostProductionCenter(productionCenter As Domain.Entities.CostProductionCenter, audit As AuditMessage) As Domain.Base.Entities.ActionResult

    ''' <summary>
    ''' Updates the state.
    ''' </summary>
    <OperationContract()>
    Function UpdateStateCostProductionCenter(code As String, state As Boolean, audit As AuditMessage) As Domain.Base.Entities.ActionResult(Of Domain.Entities.CostProductionCenter)

    ''' <summary>
    ''' Obtiene un centro de produccion por codigo
    ''' </summary>
    <OperationContract()>
    Function GetCostProductionCenter(code As String, audit As AuditMessage) As Domain.Base.Entities.ActionResult(Of Domain.Entities.CostProductionCenter)

    ''' <summary>
    ''' Obtiene centro de produccion por id
    ''' </summary>
    <OperationContract()> _
    Function GetCostProductionCenterById(id As Integer) As CostProductionCenter

    ''' <summary>
    ''' Lista todos los centros de producción
    ''' </summary>
    ''' <returns></returns>
    <OperationContract()> _
    Function ListCostProductionCenter() As List(Of CostProductionCenter)

    <OperationContract()>
    Function GetProductionCenterByCostCenterId(costCenterId As Integer) As CostProductionCenter

    <OperationContract()>
    Function ListReportOperatingResult(InitialMonth As Integer, ByVal EndMonth As Integer, ByVal Year As Integer, ByVal CodePCenterIni As String, ByVal CodePCenterFin As String, ByVal StructureOfCostId As Integer) As List(Of SP_CostReportOperatingResult_Result)

    <OperationContract()>
    Function ListReportOperatingResultsByOrganizationalStructure(InitialMonth As Integer, ByVal EndMonth As Integer, ByVal Year As Integer, ByVal Container As String, ByVal CodePCenterIni As String, ByVal CodePCenterFin As String, ByVal StructureOfCostId As Integer) As List(Of SP_CostReportOperatingResultsByOrganizationalStructure_Result)

    <OperationContract()>
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
    <OperationContract()>
    Function ListReportCostCostMeasurementUnit(initialMonth As Integer, initialYear As Integer, endMonth As Integer, endYear As Integer, initialMeasurementUnitCode As String, endMeasurementUnitCode As String, ByVal ProductionCenterId As Integer) As List(Of spCostReportCostMeasurementUnit_Result)

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
    <OperationContract()>
    Function GetCostListReportOperatingProductionCenter(InitialMonth As Integer, EndMonth As Integer, Year As Integer, CodePCenterIni As String, CodePCenterFin As String, OrderBy As Integer, Session As SessionValues) As DataSet

End Interface
