'***********************************************************************
' Assembly         : Application.InteropCost
' Author           : Diego Andrés Roldán Lozano
' Created          : 23-02-2016
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************
Imports Domain.Base
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities

Public Interface ICostEstimationAdminService
    Inherits IDisposable
    Function GetCostEstimationByYearMonth(year As Integer, month As Integer) As List(Of CostEstimation)

    Function GetCostEstimationById(id As Integer) As CostEstimation

    Function GetSpEstimateCost(distributionType As Byte, containerDGH As String, containerPayrollModule As Boolean, OnlySimulate As Boolean, PreviusDataXml As String, usercode As String) As List(Of SP_EstimateCost_Result)

    Function HasMonthClosed() As Boolean
    Function ListrptEstimatingPrimaryGeneral(InitialMonth As Integer, ByVal InitialYear As Integer, ByVal LastMonth As Integer, ByVal LastYear As Integer, ByVal CenterType As Integer, ByVal CodePCenterIni As String, ByVal CodePCenterFin As String, ByVal Status As Integer) As List(Of SP_ReportGeneralProfitabilityTotalCostCx_Result)

End Interface
