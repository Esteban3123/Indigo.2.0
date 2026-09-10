'***********************************************************************
' Assembly         : DistributedServices.InteropCost
' Author           : Diego Andrés Roldán Lozano
' Created          : 11-12-2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities
Imports System.ServiceModel

<ServiceContract()>
Public Interface IInteropCostSeriveProductionCenter

    ''' <summary>
    ''' Guarda un centro de produccion
    ''' </summary>
    <OperationContract()>
    Function SaveProductionCenter(productionCenter As ProductionCenter, idSequence As Int64, audit As AuditMessage) As ActionResult(Of ProductionCenter)

    ''' <summary>
    ''' Elimina un centro de produccion
    ''' </summary>
    <OperationContract()>
    Function DeleteProductionCenter(productionCenter As ProductionCenter, audit As AuditMessage) As ActionResult

    ''' <summary>
    ''' Updates the state.
    ''' </summary>
    <OperationContract()>
    Function UpdateStateProductionCenter(code As String, state As Boolean, audit As AuditMessage) As ActionResult(Of ProductionCenter)

    ''' <summary>
    ''' Obtiene un centro de produccion por codigo
    ''' </summary>
    <OperationContract()>
    Function GetProductionCenter(code As String, audit As AuditMessage) As ActionResult(Of ProductionCenter)

    ''' <summary>
    ''' Obtiene centro de produccion por id
    ''' </summary>
    <OperationContract()>
    Function GetProductionCenterById(id As Integer) As ProductionCenter

    ''' <summary>
    ''' Lista todos los centros de producción
    ''' </summary>
    ''' <returns></returns>
    <OperationContract()>
    Function ListProductionCenter() As List(Of ProductionCenter)

    <OperationContract()> _
    Function GetProductionCenterByCostCenterOid(costCenterOid As Integer) As ProductionCenter

    ''' <summary>
    ''' Lista Reporte Resumido para las operaciones
    ''' </summary>
    ''' <param name="InitialMonth"></param>
    ''' <param name="EndMonth"></param>
    ''' <param name="Year"></param>
    ''' <param name="InitialCodeCenter"></param>
    ''' <param name="EndCodeCenter"></param>
    ''' <param name="Container"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <OperationContract()>
    Function ListrptResultProductionCostsExpenses(InitialMonth As Integer, ByVal EndMonth As Integer, ByVal Year As Integer, InitialCodeCenter As String, EndCodeCenter As String, ByVal Container As String) As List(Of SP_ReportResultProductionCostsExpenses_Result)

    <OperationContract()>
    Function ListrptResultProductionCostsExpensesDetail(DateStart As Date, ByVal DateEnd As Date, InitialCodeCenter As String, EndCodeCenter As String, ByVal Container As String, ByVal DetailType As Integer) As List(Of SP_ReportResultProductionCostsExpensesDetail_Result)

    <OperationContract()>
    Function ListReportOperatingResult(InitialMonth As Integer, ByVal EndMonth As Integer, ByVal Year As Integer, ByVal Container As String, ByVal CodePCenterIni As String, ByVal CodePCenterFin As String, ByVal StructureOfCostId As Integer) As List(Of SP_ReportOperatingResult_Result)

    <OperationContract()>
    Function ListReportOperatingResultsByOrganizationalStructure(InitialMonth As Integer, ByVal EndMonth As Integer, ByVal Year As Integer, ByVal Container As String, ByVal CodePCenterIni As String, ByVal CodePCenterFin As String, ByVal StructureOfCostId As Integer) As List(Of SP_ReportOperatingResultsByOrganizationalStructure_Result)

    <OperationContract()>
    Function ListReportOperatingResultsByOrganizationalStatisticalGraphics(InitialMonth As Integer, ByVal EndMonth As Integer, ByVal Year As Integer, ByVal Container As String, ByVal CodePCenterIni As String, ByVal CodePCenterFin As String, ByVal StructureOfCostId As Integer) As List(Of SP_ReportOperatingResultsByOrganizationalStatisticalGraphics_Result)
End Interface