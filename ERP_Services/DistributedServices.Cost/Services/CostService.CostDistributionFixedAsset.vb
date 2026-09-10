Imports Infrastructure.CrossCutting.Base
Imports System.ServiceModel
Imports Application.Cost
Imports Microsoft.Practices.Unity
Imports DistributedServices.Cost
Imports Domain.Base.Entities

Partial Public Class CostService
    Implements ICostServiceCostDistributionFixedAsset

    Public Function GetCostDistributionFixedAssetById(id As Integer) As Domain.Base.Entities.ActionResult(Of Domain.Entities.CostDistributionFixedAsset) Implements ICostServiceCostDistributionFixedAsset.GetCostDistributionFixedAssetById
        Using service As ICostDistributionFixedAssetAdminService = Container.Current.Resolve(Of ICostDistributionFixedAssetAdminService)()
            Return service.GetCostDistributionFixedAssetById(id)
        End Using
    End Function

    Public Function GetCostDistributionFixedAsset(ByVal code As String, audit As AuditMessage) As Domain.Base.Entities.ActionResult(Of Domain.Entities.CostDistributionFixedAsset) Implements ICostServiceCostDistributionFixedAsset.GetCostDistributionFixedAsset
        Using service As ICostDistributionFixedAssetAdminService = Container.Current.Resolve(Of ICostDistributionFixedAssetAdminService)()
            Return service.GetCostDistributionFixedAsset(code)
        End Using
    End Function

    Public Function GetCostDistributionFixedAssetDataByPhysicalIdAndYearMonth(year As Integer, month As Integer, Optional physicalId As Integer = Nothing) As Domain.Base.Entities.ActionResult(Of List(Of Domain.Entities.CostDistributionFixedAsset)) Implements ICostServiceCostDistributionFixedAsset.GetCostDistributionFixedAssetDataByPhysicalIdAndYearMonth
        Using service As ICostDistributionFixedAssetAdminService = Container.Current.Resolve(Of ICostDistributionFixedAssetAdminService)()
            Return service.GetCostDistributionFixedAssetDataByPhysicalIdAndYearMonth(year, month, physicalId)
        End Using
    End Function

    Public Function GetCostDistributionFixedAssetByPhysicalIdAndYearMonth(physicalId As Integer, year As Integer, month As Integer) As Domain.Base.Entities.ActionResult(Of Domain.Entities.CostDistributionFixedAsset) Implements ICostServiceCostDistributionFixedAsset.GetCostDistributionFixedAssetByPhysicalIdAndYearMonth
        Using service As ICostDistributionFixedAssetAdminService = Container.Current.Resolve(Of ICostDistributionFixedAssetAdminService)()
            Return service.GetCostDistributionFixedAssetByPhysicalIdAndYearMonth(physicalId, year, month)
        End Using
    End Function

    Public Function ListPeriodWithDistributionFixedDataByMaximumPeriod(year As Integer, month As Integer) As List(Of String) Implements ICostServiceCostDistributionFixedAsset.ListPeriodWithDistributionFixedDataByMaximumPeriod
        Using service As ICostDistributionFixedAssetAdminService = Container.Current.Resolve(Of ICostDistributionFixedAssetAdminService)()
            Return service.ListPeriodWithDistributionFixedDataByMaximumPeriod(year, month)
        End Using
    End Function

    Public Function ListDistributionFixedAssetByYearMonth(year As Integer, month As Integer) As List(Of Domain.Entities.CostDistributionFixedAsset) Implements ICostServiceCostDistributionFixedAsset.ListDistributionFixedAssetByYearMonth
        Using service As ICostDistributionFixedAssetAdminService = Container.Current.Resolve(Of ICostDistributionFixedAssetAdminService)()
            Return service.ListDistributionFixedAssetByYearMonth(year, month)
        End Using
    End Function

    Public Function SP_ExportExcelCostDistributionFixedAsset(Year As Integer, Month As Integer) As Domain.Base.Entities.ActionResult(Of List(Of Domain.Entities.SP_ExportExcelCostDistributionFixedAsset_Result)) Implements ICostServiceCostDistributionFixedAsset.SP_ExportExcelCostDistributionFixedAsset
        Using service As ICostDistributionFixedAssetAdminService = Container.Current.Resolve(Of ICostDistributionFixedAssetAdminService)()
            Return service.SP_ExportExcelCostDistributionFixedAsset(Year, Month)
        End Using
    End Function

    Public Function ImportCostDistributionFixedAsset(Year As Integer, Month As Integer, OperatingUnitId As Integer, ImportIds As List(Of Integer), audit As AuditMessage) As Domain.Base.Entities.ActionResult Implements ICostServiceCostDistributionFixedAsset.ImportCostDistributionFixedAsset
        Using service As ICostDistributionFixedAssetAdminService = Container.Current.Resolve(Of ICostDistributionFixedAssetAdminService)()
            Return service.ImportCostDistributionFixedAsset(Year, Month, OperatingUnitId, ImportIds, audit)
        End Using
    End Function

    Public Function SaveCostDistributionFixedAsset(CostDistributionFixedAsset As Domain.Entities.CostDistributionFixedAsset, audit As AuditMessage, idSequence As Int64) As Domain.Base.Entities.ActionResult(Of Domain.Entities.CostDistributionFixedAsset) Implements ICostServiceCostDistributionFixedAsset.SaveCostDistributionFixedAsset
        Using service As ICostDistributionFixedAssetAdminService = Container.Current.Resolve(Of ICostDistributionFixedAssetAdminService)()
            Return service.SaveCostDistributionFixedAsset(CostDistributionFixedAsset, audit, idSequence)
        End Using
    End Function

    Public Function SP_ConfirmMasiveCostDistributionFixedAsset(Year As Integer, Month As Integer, OperatingUnitId As Integer, audit As AuditMessage) As Domain.Base.Entities.ActionResult Implements ICostServiceCostDistributionFixedAsset.SP_ConfirmMasiveCostDistributionFixedAsset
        Using service As ICostDistributionFixedAssetAdminService = Container.Current.Resolve(Of ICostDistributionFixedAssetAdminService)()
            Return service.SP_ConfirmMasiveCostDistributionFixedAsset(Year, Month, OperatingUnitId, audit)
        End Using
    End Function

    Public Function DeleteCostDistributionFixedAsset(CostDistributionFixedAsset As Domain.Entities.CostDistributionFixedAsset, audit As AuditMessage) As Domain.Base.Entities.ActionResult Implements ICostServiceCostDistributionFixedAsset.DeleteCostDistributionFixedAsset
        Using service As ICostDistributionFixedAssetAdminService = Container.Current.Resolve(Of ICostDistributionFixedAssetAdminService)()
            Return service.DeleteCostDistributionFixedAsset(CostDistributionFixedAsset, audit)
        End Using
    End Function
End Class
