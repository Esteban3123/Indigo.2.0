'***********************************************************************
' Assembly         : Domain.InteropCost
' Author           : Diego Andrés Roldán Lozano
' Created          : 21-04-2016
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************
Imports Domain.Base
Imports Domain.Entities

Public Interface IEstimateCostNativeRepository
    Inherits IRepository(Of CostEstimationNative)


    Function GetCostEstimationByYearMonth(year As Integer, month As Integer) As List(Of CostEstimationNative)

    Function GetCostEstimationById(id As Integer) As CostEstimationNative

    Function GetSpEstimateCostNative(Year As Integer, Month As Integer, distributionType As Byte, OnlySimulate As Boolean, containerPayrollModule As Boolean, PreviusDataXml As String, usercode As String) As List(Of SP_EstimateCostNative_Result)

    Function SP_ReverseEstimateCostNative(Year As Integer, Month As Integer, Usercode As String) As SP_ReverseEstimateCostNative_Result

    Function ListrptEstimatingPrimaryGeneral(ByVal InitialMonth As Integer, ByVal InitialYear As Integer, ByVal LastMonth As Integer, ByVal LastYear As Integer, ByVal CenterType As Integer, ByVal CodePCenterIni As String, ByVal CodePCenterFin As String, ByVal Status As Integer) As List(Of SP_CostReportGeneralProfitabilityTotalCostCx_Result)
End Interface