'***********************************************************************
' Assembly         : DistributedServices.InteropCost
' Author           : Diego Andrés Roldán lozano
' Created          : 23-02-2016
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities
Imports System.ServiceModel
Imports Application.Cost
Imports Microsoft.Practices.Unity
Imports DistributedServices.Cost

Partial Class CostService
    Implements ICostServiceCostEstimationNative

    Public Function GetCostEstimationById(id As Integer) As CostEstimationNative Implements ICostServiceCostEstimationNative.GetCostEstimationById
        Using service As ICostEstimationNativeAdminService = Container.Current.Resolve(Of ICostEstimationNativeAdminService)()
            Return service.GetCostEstimationById(id)
        End Using
        'Return _costEstimationNativeAdminService.GetCostEstimationById(id)
    End Function

    Public Function GetCostEstimationByYearMonth(year As Integer, month As Integer) As List(Of CostEstimationNative) Implements ICostServiceCostEstimationNative.GetCostEstimationByYearMonth
        Using service As ICostEstimationNativeAdminService = Container.Current.Resolve(Of ICostEstimationNativeAdminService)()
            Return service.GetCostEstimationByYearMonth(year, month)
        End Using
        'Return _costEstimationNativeAdminService.GetCostEstimationByYearMonth(year, month)
    End Function

    Public Function GetSpEstimateCostNative(Year As Integer, Month As Integer, distributionType As Byte, OnlySimulate As Boolean, containerPayrollModule As Boolean, PreviusDataXml As String, usercode As String) As List(Of SP_EstimateCostNative_Result) Implements ICostServiceCostEstimationNative.GetSpEstimateCostNative
        Using service As ICostEstimationNativeAdminService = Container.Current.Resolve(Of ICostEstimationNativeAdminService)()
            Return service.GetSpEstimateCostNative(Year, Month, distributionType, OnlySimulate, containerPayrollModule, PreviusDataXml, usercode)
        End Using
        'Return _costEstimationNativeAdminService.GetSpEstimateCostNative(distributionType, containPayrollModule, OnlySimulate, PreviusDataXml, usercode)
    End Function

    Public Function ReverseEstimateCostNative(Year As Integer, Month As Integer, Usercode As String) As ActionResult Implements ICostServiceCostEstimationNative.ReverseEstimateCostNative
        Using service As ICostEstimationNativeAdminService = Container.Current.Resolve(Of ICostEstimationNativeAdminService)()
            Return service.ReverseEstimateCostNative(Year, Month, Usercode)
        End Using
    End Function

    Public Function ListrptEstimatingPrimaryGeneral(InitialMonth As Integer, InitialYear As Integer, LastMonth As Integer, LastYear As Integer, CenterType As Integer, CodePCenterIni As String, CodePCenterFin As String, Status As Integer) As List(Of SP_CostReportGeneralProfitabilityTotalCostCx_Result) Implements ICostServiceCostEstimationNative.ListrptEstimatingPrimaryGeneral
        Using service As ICostEstimationNativeAdminService = Container.Current.Resolve(Of ICostEstimationNativeAdminService)()
            Return service.ListrptEstimatingPrimaryGeneral(InitialMonth, InitialYear, LastMonth, LastYear, CenterType, CodePCenterIni, CodePCenterFin, Status)
        End Using
        'Return Me._costEstimationNativeAdminService.ListrptEstimatingPrimaryGeneral(InitialMonth, InitialYear, LastMonth, LastYear, CenterType, CodePCenterIni, CodePCenterFin, Status)
    End Function

End Class