'***********************************************************************
' Assembly         : DistributedServices.InteropCost
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
Public Interface IInteropCostServiceCostEstimation

    <OperationContract()> _
    Function GetCostEstimationByYearMonth(year As Integer, month As Integer) As List(Of CostEstimation)

    <OperationContract()> _
    Function GetCostEstimationById(id As Integer) As CostEstimation

    <OperationContract()> _
    Function GetSpEstimateCost(distributionType As Byte, containerDGH As String, containerPayrollModule As Boolean, OnlySimulate As Boolean, PreviusDataXml As String, usercode As String) As List(Of SP_EstimateCost_Result)

    <OperationContract()> _
    Function HasMonthClosed() As Boolean

    <OperationContract()>
    Function ListrptEstimatingPrimaryGeneral(ByVal InitialMonth As Integer, ByVal InitialYear As Integer, ByVal LastMonth As Integer, ByVal LastYear As Integer, ByVal CenterType As Integer, ByVal CodePCenterIni As String, ByVal CodePCenterFin As String, ByVal Status As Integer) As List(Of SP_ReportGeneralProfitabilityTotalCostCx_Result)
End Interface