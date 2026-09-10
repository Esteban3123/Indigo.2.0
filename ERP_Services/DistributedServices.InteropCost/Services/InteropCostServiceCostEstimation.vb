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
Imports Application.InteropCost
Imports Microsoft.Practices.Unity

Partial Class InteropCostService
    Implements IInteropCostServiceCostEstimation

    Public Function GetCostEstimationById(id As Integer) As CostEstimation Implements IInteropCostServiceCostEstimation.GetCostEstimationById
        Using service As ICostEstimationAdminService = DistributedServices.InteropCost.Container.Current.Resolve(Of ICostEstimationAdminService)()
            Return service.GetCostEstimationById(id)
        End Using
        'Return _costEstimationAdminService.GetCostEstimationById(id)
    End Function

    Public Function GetCostEstimationByYearMonth(year As Integer, month As Integer) As List(Of CostEstimation) Implements IInteropCostServiceCostEstimation.GetCostEstimationByYearMonth
        Using service As ICostEstimationAdminService = DistributedServices.InteropCost.Container.Current.Resolve(Of ICostEstimationAdminService)()
            Return service.GetCostEstimationByYearMonth(year, month)
        End Using
        'Return _costEstimationAdminService.GetCostEstimationByYearMonth(year, month)
    End Function

    Public Function GetSpEstimateCost(distributionType As Byte, containerDGH As String, containPayrollModule As Boolean, OnlySimulate As Boolean, PreviusDataXml As String, usercode As String) As List(Of SP_EstimateCost_Result) Implements IInteropCostServiceCostEstimation.GetSpEstimateCost
        Using service As ICostEstimationAdminService = DistributedServices.InteropCost.Container.Current.Resolve(Of ICostEstimationAdminService)()
            Return service.GetSpEstimateCost(distributionType, containerDGH, containPayrollModule, OnlySimulate, PreviusDataXml, usercode)
        End Using
        'Return _costEstimationAdminService.GetSpEstimateCost(distributionType, containerDGH, containPayrollModule, OnlySimulate, PreviusDataXml, usercode)
    End Function

    Public Function HasMonthClosed() As Boolean Implements IInteropCostServiceCostEstimation.HasMonthClosed
        Using service As ICostEstimationAdminService = DistributedServices.InteropCost.Container.Current.Resolve(Of ICostEstimationAdminService)()
            Return service.HasMonthClosed()
        End Using
        'Return _costEstimationAdminService.HasMonthClosed()
    End Function

    Public Function ListrptEstimatingPrimaryGeneral(InitialMonth As Integer, InitialYear As Integer, LastMonth As Integer, LastYear As Integer, CenterType As Integer, CodePCenterIni As String, CodePCenterFin As String, Status As Integer) As List(Of SP_ReportGeneralProfitabilityTotalCostCx_Result) Implements IInteropCostServiceCostEstimation.ListrptEstimatingPrimaryGeneral
        Using service As ICostEstimationAdminService = DistributedServices.InteropCost.Container.Current.Resolve(Of ICostEstimationAdminService)()
            Return service.ListrptEstimatingPrimaryGeneral(InitialMonth, InitialYear, LastMonth, LastYear, CenterType, CodePCenterIni, CodePCenterFin, Status)
        End Using
        'Return Me._costEstimationAdminService.ListrptEstimatingPrimaryGeneral(InitialMonth, InitialYear, LastMonth, LastYear, CenterType, CodePCenterIni, CodePCenterFin, Status)
    End Function

End Class