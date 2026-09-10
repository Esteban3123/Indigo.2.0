'***********************************************************************
' Assembly         : DistributedServices.Cost
' Author           : Diego Andrés Roldán Lozano
' Created          : 23-02-2016
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities
Imports System.ServiceModel

<ServiceContract()>
Public Interface ICostServiceCostEstimationNative

    <OperationContract()> _
    Function GetCostEstimationByYearMonth(year As Integer, month As Integer) As List(Of CostEstimationNative)
    <OperationContract()> _
    Function GetCostEstimationById(id As Integer) As CostEstimationNative
    <OperationContract()>
    Function GetSpEstimateCostNative(Year As Integer, Month As Integer, distributionType As Byte, OnlySimulate As Boolean, containerPayrollModule As Boolean, PreviusDataXml As String, usercode As String) As List(Of SP_EstimateCostNative_Result)
    <OperationContract()>
    Function ReverseEstimateCostNative(Year As Integer, Month As Integer, Usercode As String) As ActionResult
    <OperationContract()>
    Function ListrptEstimatingPrimaryGeneral(ByVal InitialMonth As Integer, ByVal InitialYear As Integer, ByVal LastMonth As Integer, ByVal LastYear As Integer, ByVal CenterType As Integer, ByVal CodePCenterIni As String, ByVal CodePCenterFin As String, ByVal Status As Integer) As List(Of SP_CostReportGeneralProfitabilityTotalCostCx_Result)
End Interface