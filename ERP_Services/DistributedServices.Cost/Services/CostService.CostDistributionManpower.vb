Imports Infrastructure.CrossCutting.Base
Imports Application.Cost
Imports Microsoft.Practices.Unity

Partial Public Class CostService
    Implements ICostServiceCostDistributionManpower

    Public Function GetDistributionManpowerById(id As Integer) As Domain.Entities.CostDistributionManpower Implements ICostServiceCostDistributionManpower.GetDistributionManpowerById
        Using service As ICostDistributionManpowerAdminService = Container.Current.Resolve(Of ICostDistributionManpowerAdminService)()
            Return service.GetDistributionManpowerById(id)
        End Using
    End Function

    Public Function GetDistributionManpower(code As String, audit As AuditMessage) As Domain.Base.Entities.ActionResult(Of Domain.Entities.CostDistributionManpower) Implements ICostServiceCostDistributionManpower.GetDistributionManpower
        Using service As ICostDistributionManpowerAdminService = Container.Current.Resolve(Of ICostDistributionManpowerAdminService)()
            Return service.GetDistributionManpower(code, audit)
        End Using
    End Function

    Public Function GetDistributionManpowerByYearMonth(ByVal Year As Integer, ByVal Month As Integer, Optional ByVal ManpowerType? As Integer = Nothing, Optional ByVal EntityId? As Integer = Nothing) As Domain.Base.Entities.ActionResult(Of List(Of Domain.Entities.CostDistributionManpower)) Implements ICostServiceCostDistributionManpower.GetDistributionManpowerByYearMonth
        Using service As ICostDistributionManpowerAdminService = Container.Current.Resolve(Of ICostDistributionManpowerAdminService)()
            Return service.GetDistributionManpowerByYearMonth(Year, Month, ManpowerType, EntityId)
        End Using
    End Function

    Public Function GetDistributionManpowerByYearMonthManpowerTypeAndEntityId(ByVal Year As Integer, ByVal Month As Integer, Optional ByVal ManpowerType? As Integer = Nothing, Optional ByVal EntityId? As Integer = Nothing) As Domain.Base.Entities.ActionResult(Of Domain.Entities.CostDistributionManpower) Implements ICostServiceCostDistributionManpower.GetDistributionManpowerByYearMonthManpowerTypeAndEntityId
        Using service As ICostDistributionManpowerAdminService = Container.Current.Resolve(Of ICostDistributionManpowerAdminService)()
            Return service.GetDistributionManpowerByYearMonthManpowerTypeAndEntityId(Year, Month, ManpowerType, EntityId)
        End Using
    End Function

    Public Function ListPeriodWithDataByMaximumPeriod(year As Integer, month As Integer) As List(Of String) Implements ICostServiceCostDistributionManpower.ListPeriodWithDataByMaximumPeriod
        Using service As ICostDistributionManpowerAdminService = Container.Current.Resolve(Of ICostDistributionManpowerAdminService)()
            Return service.ListPeriodWithDataByMaximumPeriod(year, month)
        End Using
    End Function

    Public Function ListDistributionManpowerByYearMonth(year As Integer, month As Integer) As List(Of Domain.Entities.CostDistributionManpower) Implements ICostServiceCostDistributionManpower.ListDistributionManpowerByYearMonth
        Using service As ICostDistributionManpowerAdminService = Container.Current.Resolve(Of ICostDistributionManpowerAdminService)()
            Return service.ListDistributionManpowerByYearMonth(year, month)
        End Using
    End Function

    Public Function SP_ExportExcelCostDistributionManPower(Year As Integer, Month As Integer) As Domain.Base.Entities.ActionResult(Of List(Of Domain.Entities.SP_ExportExcelCostDistributionManPower_Result)) Implements ICostServiceCostDistributionManpower.SP_ExportExcelCostDistributionManPower
        Using service As ICostDistributionManpowerAdminService = Container.Current.Resolve(Of ICostDistributionManpowerAdminService)()
            Return service.SP_ExportExcelCostDistributionManPower(Year, Month)
        End Using
    End Function

    Public Function ImportCostDistributionManpower(Year As Integer, Month As Integer, OperatingUnitId As Integer, ImportIds As List(Of Integer), audit As AuditMessage) As Domain.Base.Entities.ActionResult Implements ICostServiceCostDistributionManpower.ImportCostDistributionManpower
        Using service As ICostDistributionManpowerAdminService = Container.Current.Resolve(Of ICostDistributionManpowerAdminService)()
            Return service.ImportCostDistributionManpower(Year, Month, OperatingUnitId, ImportIds, audit)
        End Using
    End Function

    Public Function SaveDistributionManpower(distributionManpower As Domain.Entities.CostDistributionManpower, audit As AuditMessage, idSequence As Int64) As Domain.Base.Entities.ActionResult(Of Domain.Entities.CostDistributionManpower) Implements ICostServiceCostDistributionManpower.SaveDistributionManpower
        Using service As ICostDistributionManpowerAdminService = Container.Current.Resolve(Of ICostDistributionManpowerAdminService)()
            Return service.SaveDistributionManpower(distributionManpower, audit, idSequence)
        End Using
    End Function

    Public Function ConfirmMasive(Year As Integer, Month As Integer, OperatingUnitId As Integer, audit As AuditMessage) As Domain.Base.Entities.ActionResult Implements ICostServiceCostDistributionManpower.ConfirmMasive
        Using service As ICostDistributionManpowerAdminService = Container.Current.Resolve(Of ICostDistributionManpowerAdminService)()
            Return service.ConfirmMasive(Year, Month, OperatingUnitId, audit)
        End Using
    End Function

    Public Function DeleteDistributionManpower(distributionManpower As Domain.Entities.CostDistributionManpower, audit As AuditMessage) As Domain.Base.Entities.ActionResult Implements ICostServiceCostDistributionManpower.DeleteDistributionManpower
        Using service As ICostDistributionManpowerAdminService = Container.Current.Resolve(Of ICostDistributionManpowerAdminService)()
            Return service.DeleteDistributionManpower(distributionManpower, audit)
        End Using
    End Function

End Class
