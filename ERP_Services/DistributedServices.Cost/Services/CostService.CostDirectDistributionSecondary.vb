Imports Infrastructure.CrossCutting.Base
Imports System.ServiceModel
Imports Application.Cost
Imports Microsoft.Practices.Unity
Imports DistributedServices.Cost
Imports Domain.Base.Entities
Imports Domain.Entities

Partial Public Class CostService
    Implements ICostServiceCostDirectDistributionSecondary

    Public Function CalculateDistributionSecondary(CostDistributionSecondaryId As Integer, Value As Decimal, Year As Integer, Month As Integer) As ActionResult(Of List(Of CostDirectDistributionSecondaryDetail)) Implements ICostServiceCostDirectDistributionSecondary.CalculateDistributionSecondary
        Using service As ICostDirectDistributionSecondaryAdminService = Container.Current.Resolve(Of ICostDirectDistributionSecondaryAdminService)()
            Return service.CalculateDistributionSecondary(CostDistributionSecondaryId, Value, Year, Month)
        End Using
    End Function

    Public Function GetCostDirectDistributionSecondary(code As String, audit As AuditMessage) As Domain.Entities.CostDirectDistributionSecondary Implements ICostServiceCostDirectDistributionSecondary.GetCostDirectDistributionSecondary
        Using service As ICostDirectDistributionSecondaryAdminService = Container.Current.Resolve(Of ICostDirectDistributionSecondaryAdminService)()
            Return service.GetCostDirectDistributionSecondary(code, audit)
        End Using
    End Function

    Public Function GetCostDirectDistributionSecondaryById(id As Integer) As Domain.Entities.CostDirectDistributionSecondary Implements ICostServiceCostDirectDistributionSecondary.GetCostDirectDistributionSecondaryById
        Using service As ICostDirectDistributionSecondaryAdminService = Container.Current.Resolve(Of ICostDirectDistributionSecondaryAdminService)()
            Return service.GetCostDirectDistributionSecondaryById(id)
        End Using
    End Function

    Public Function SaveCostDirectDistributionSecondary(distributionSecondary As CostDirectDistributionSecondary, listDistributionSecondaryDetailForDelete As List(Of Integer), ListCostLogisticsProductionCenterDetail As List(Of Integer), audit As AuditMessage) As Domain.Base.Entities.ActionResult(Of Domain.Entities.CostDirectDistributionSecondary) Implements ICostServiceCostDirectDistributionSecondary.SaveCostDirectDistributionSecondary
        Using service As ICostDirectDistributionSecondaryAdminService = Container.Current.Resolve(Of ICostDirectDistributionSecondaryAdminService)()
            Return service.SaveCostDirectDistributionSecondary(distributionSecondary, listDistributionSecondaryDetailForDelete, ListCostLogisticsProductionCenterDetail, audit)
        End Using
    End Function

    Public Function GenerateDistributionSecondary(Year As Integer, Month As Integer, OperatingUnitId As Integer, audit As AuditMessage) As ActionResult Implements ICostServiceCostDirectDistributionSecondary.GenerateDistributionSecondary
        Using service As ICostDirectDistributionSecondaryAdminService = Container.Current.Resolve(Of ICostDirectDistributionSecondaryAdminService)()
            Return service.GenerateDistributionSecondary(Year, Month, OperatingUnitId, audit)
        End Using
    End Function

End Class
