'***********************************************************************
' Assembly         : DistributedServices.InteropCost
' Author           : Diego Andrés Roldán lozano
' Created          : 22-12-2014
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
    Implements IInteropCostServiceDistributionFixedAsset

    ''' <summary>
    ''' Elimina una distribución de activos fijos
    ''' </summary>
    Public Function DeleteDistributionFixedAsset(distributionFixedAsset As DistributionFixedAsset, audit As AuditMessage) As ActionResult Implements IInteropCostServiceDistributionFixedAsset.DeleteDistributionFixedAsset
        Using service As IDistributionFixedAssetAdminService = DistributedServices.InteropCost.Container.Current.Resolve(Of IDistributionFixedAssetAdminService)()
            Return service.DeleteDistributionFixedAsset(distributionFixedAsset, audit)
        End Using
        'Return Me._distributionFixedAssetAdminService.DeleteDistributionFixedAsset(distributionFixedAsset, audit)
    End Function

    ''' <summary>
    ''' Obtiene una distribucion de activos fijos por codigo
    ''' </summary>
    Public Function GetDistributionFixedAsset(code As String, audit As AuditMessage) As ActionResult(Of DistributionFixedAsset) Implements IInteropCostServiceDistributionFixedAsset.GetDistributionFixedAsset
        Using service As IDistributionFixedAssetAdminService = DistributedServices.InteropCost.Container.Current.Resolve(Of IDistributionFixedAssetAdminService)()
            Return service.GetDistributionFixedAsset(code, audit)
        End Using
        'Return Me._distributionFixedAssetAdminService.GetDistributionFixedAsset(code, audit)
    End Function

    ''' <summary>
    ''' Obtiene una distribucion de activos fijos por id
    ''' </summary>
    Public Function GetDistributionFixedAssetById(id As Integer) As DistributionFixedAsset Implements IInteropCostServiceDistributionFixedAsset.GetDistributionFixedAssetById
        Using service As IDistributionFixedAssetAdminService = DistributedServices.InteropCost.Container.Current.Resolve(Of IDistributionFixedAssetAdminService)()
            Return service.GetDistributionFixedAssetById(id)
        End Using
        'Return Me._distributionFixedAssetAdminService.GetDistributionFixedAssetById(id)
    End Function

    ''' <summary>
    ''' Lista las distribuciones de activos fijos por año y mes
    ''' </summary>
    Public Function ListDistributionFixedAssetByYearMonth(year As Integer, month As Integer) As List(Of DistributionFixedAsset) Implements IInteropCostServiceDistributionFixedAsset.ListDistributionFixedAssetByYearMonth
        Using service As IDistributionFixedAssetAdminService = DistributedServices.InteropCost.Container.Current.Resolve(Of IDistributionFixedAssetAdminService)()
            Return service.ListDistributionFixedAssetByYearMonth(year, month)
        End Using
        'Return Me._distributionFixedAssetAdminService.ListDistributionFixedAssetByYearMonth(year, month)
    End Function

    ''' <summary>
    ''' Guarda una distribución de activos fijos
    ''' </summary>
    Public Function SaveDistributionFixedAsset(distributionFixedAsset As DistributionFixedAsset, idSequence As Int64, audit As AuditMessage) As ActionResult(Of DistributionFixedAsset) Implements IInteropCostServiceDistributionFixedAsset.SaveDistributionFixedAsset
        Using service As IDistributionFixedAssetAdminService = DistributedServices.InteropCost.Container.Current.Resolve(Of IDistributionFixedAssetAdminService)()
            Return service.SaveDistributionFixedAsset(distributionFixedAsset, audit, idSequence)
        End Using
        'Return Me._distributionFixedAssetAdminService.SaveDistributionFixedAsset(distributionFixedAsset, audit, idSequence)
    End Function

    ''' <summary>
    ''' Updates the state distribution fixed asset.
    ''' </summary>
    Public Function UpdateStateDistributionFixedAsset(code As String, state As Boolean, audit As AuditMessage) As ActionResult(Of DistributionFixedAsset) Implements IInteropCostServiceDistributionFixedAsset.UpdateStateDistributionFixedAsset
        Using service As IDistributionFixedAssetAdminService = DistributedServices.InteropCost.Container.Current.Resolve(Of IDistributionFixedAssetAdminService)()
            Return service.UpdateStateDistributionFixedAsset(code, state, audit)
        End Using
        'Return Me._distributionFixedAssetAdminService.UpdateStateDistributionFixedAsset(code, state, audit)
    End Function

    ''' <summary>
    ''' Lists the period with data by maximum period1.
    ''' </summary>
    Public Function ListPeriodWithDataByMaximumPeriodFixedAsset(year As Integer, month As Integer) As List(Of String) Implements IInteropCostServiceDistributionFixedAsset.ListPeriodWithDataByMaximumPeriodFixedAsset
        Using service As IDistributionFixedAssetAdminService = DistributedServices.InteropCost.Container.Current.Resolve(Of IDistributionFixedAssetAdminService)()
            Return service.ListPeriodWithDataByMaximumPeriod(year, month)
        End Using
        'Return Me._distributionFixedAssetAdminService.ListPeriodWithDataByMaximumPeriod(year, month)
    End Function

    Public Function GetAFNDEPRECIByOidYearMonth(oid As Integer, year As Integer, month As Integer) As Domain.InteropCost.Entities.AFNDEPRECI Implements IInteropCostServiceDistributionFixedAsset.GetAFNDEPRECIByOidYearMonth
        Using service As IDistributionFixedAssetAdminService = DistributedServices.InteropCost.Container.Current.Resolve(Of IDistributionFixedAssetAdminService)()
            Return service.GetAFNDEPRECIByOidYearMonth(oid, year, month)
        End Using
        'Return Me._distributionFixedAssetAdminService.GetAFNDEPRECIByOidYearMonth(oid, year, month)
    End Function

    Public Function GetDeprecationValue(oidAfnActivo As Integer, year As Integer, month As Integer) As Decimal Implements IInteropCostServiceDistributionFixedAsset.GetDeprecationValue
        Using service As IDistributionFixedAssetAdminService = DistributedServices.InteropCost.Container.Current.Resolve(Of IDistributionFixedAssetAdminService)()
            Return service.GetDeprecationValue(oidAfnActivo, year, month)
        End Using
        'Return Me._distributionFixedAssetAdminService.GetDeprecationValue(oidAfnActivo, year, month)
    End Function

    Public Function GetDistributionFixedAssetByActivoAndYearMonth(activoOid As Integer, year As Integer, month As Integer) As ActionResult(Of DistributionFixedAsset) Implements IInteropCostServiceDistributionFixedAsset.GetDistributionFixedAssetByActivoAndYearMonth
        Using service As IDistributionFixedAssetAdminService = DistributedServices.InteropCost.Container.Current.Resolve(Of IDistributionFixedAssetAdminService)()
            Return service.GetDistributionFixedAssetByActivoAndYearMonth(activoOid, year, month)
        End Using
        'Return Me._distributionFixedAssetAdminService.GetDistributionFixedAssetByActivoAndYearMonth(activoOid, year, month)
    End Function

    Public Function SP_ConfirmMasiveDistributionFixedAsset(Container As String, Year As Integer, Month As Integer, audit As AuditMessage) As ActionResult Implements IInteropCostServiceDistributionFixedAsset.SP_ConfirmMasiveDistributionFixedAsset
        Using service As IDistributionFixedAssetAdminService = DistributedServices.InteropCost.Container.Current.Resolve(Of IDistributionFixedAssetAdminService)()
            Return service.SP_ConfirmMasiveDistributionFixedAsset(Container, Year, Month, audit)
        End Using
        'Return Me._distributionFixedAssetAdminService.SP_ConfirmMasiveDistributionFixedAsset(Container, Year, Month, audit)
    End Function

    Public Function SP_ExportExcelDistributionFixedAsset(Container As String, Year As Integer, Month As Integer) As ActionResult(Of List(Of SP_ExportExcelDistributionFixedAsset_Result)) Implements IInteropCostServiceDistributionFixedAsset.SP_ExportExcelDistributionFixedAsset
        Using service As IDistributionFixedAssetAdminService = DistributedServices.InteropCost.Container.Current.Resolve(Of IDistributionFixedAssetAdminService)()
            Return service.SP_ExportExcelDistributionFixedAsset(Container, Year, Month)
        End Using
        'Return Me._distributionFixedAssetAdminService.SP_ExportExcelDistributionFixedAsset(Container, Year, Month)
    End Function

End Class
