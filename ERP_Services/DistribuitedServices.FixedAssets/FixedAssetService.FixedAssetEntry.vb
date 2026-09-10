Imports System.ServiceModel
Imports Infrastructure.CrossCutting.Base
Imports Domain.Entities
Imports Domain.Base.Entities
Imports Application.FixedAsset
Imports Microsoft.Practices.Unity

Partial Public Class FixedAssetService

    Public Function ConfirmFixedAssetEntry(FixedAssetEntry As Domain.Entities.FixedAssetEntry, ListDeleteFixedAssetEntryItemDetailPartBook As List(Of Domain.Entities.FixedAssetEntryItemDetailPartBook), ListDeleteFixedAssetEntryItemDetailPart As List(Of Domain.Entities.FixedAssetEntryItemDetailPart), ListDeleteFixedAssetEntryItemDetailBook As List(Of Domain.Entities.FixedAssetEntryItemDetailBook), ListDeleteFixedAssetEntryItemDetail As List(Of Domain.Entities.FixedAssetEntryItemDetail), ListDeleteFixedAssetEntryItem As List(Of Domain.Entities.FixedAssetEntryItem), idSequense As Int64, audit As AuditMessage) As Domain.Base.Entities.ActionResult(Of Domain.Entities.FixedAssetEntry) Implements IFixedAssetEntryService.ConfirmFixedAssetEntry
        Using service As IFixedAssetEntryAdminService = Container.Current.Resolve(Of IFixedAssetEntryAdminService)()
            Return service.ConfirmFixedAssetEntry(FixedAssetEntry, ListDeleteFixedAssetEntryItemDetailPartBook, ListDeleteFixedAssetEntryItemDetailPart, ListDeleteFixedAssetEntryItemDetailBook, ListDeleteFixedAssetEntryItemDetail, ListDeleteFixedAssetEntryItem, audit, idSequense)
        End Using
        'Return Me._FixedAssetEntryAdminService.ConfirmFixedAssetEntry(FixedAssetEntry, ListDeleteFixedAssetEntryItemDetailPartBook, ListDeleteFixedAssetEntryItemDetailPart, ListDeleteFixedAssetEntryItemDetailBook, ListDeleteFixedAssetEntryItemDetail, ListDeleteFixedAssetEntryItem, audit, idSequense)
    End Function

    Public Function GetFixedAssetEntry(code As String, audit As AuditMessage) As Domain.Base.Entities.ActionResult(Of Domain.Entities.FixedAssetEntry) Implements IFixedAssetEntryService.GetFixedAssetEntry
        Using service As IFixedAssetEntryAdminService = Container.Current.Resolve(Of IFixedAssetEntryAdminService)()
            Return service.GetFixedAssetEntry(code, audit)
        End Using
        'Return Me._FixedAssetEntryAdminService.GetFixedAssetEntry(code, audit)
    End Function

    Public Function GetFixedAssetEntryById(Id As Integer, audit As AuditMessage) As Domain.Base.Entities.ActionResult(Of Domain.Entities.FixedAssetEntry) Implements IFixedAssetEntryService.GetFixedAssetEntryById
        Using service As IFixedAssetEntryAdminService = Container.Current.Resolve(Of IFixedAssetEntryAdminService)()
            Return service.GetFixedAssetEntryById(Id, audit)
        End Using
        'Return Me._FixedAssetEntryAdminService.GetFixedAssetEntryById(Id, audit)
    End Function

    Public Function SaveFixedAssetEntry(FixedAssetEntry As Domain.Entities.FixedAssetEntry, ListDeleteFixedAssetEntryItemDetailPartBook As List(Of Domain.Entities.FixedAssetEntryItemDetailPartBook), ListDeleteFixedAssetEntryItemDetailPart As List(Of Domain.Entities.FixedAssetEntryItemDetailPart), ListDeleteFixedAssetEntryItemDetailBook As List(Of Domain.Entities.FixedAssetEntryItemDetailBook), ListDeleteFixedAssetEntryItemDetail As List(Of Domain.Entities.FixedAssetEntryItemDetail), ListDeleteFixedAssetEntryItem As List(Of Domain.Entities.FixedAssetEntryItem), idSequense As Int64, audit As AuditMessage) As Domain.Base.Entities.ActionResult(Of Domain.Entities.FixedAssetEntry) Implements IFixedAssetEntryService.SaveFixedAssetEntry
        Using service As IFixedAssetEntryAdminService = Container.Current.Resolve(Of IFixedAssetEntryAdminService)()
            Return service.SaveFixedAssetEntry(FixedAssetEntry, ListDeleteFixedAssetEntryItemDetailPartBook, ListDeleteFixedAssetEntryItemDetailPart, ListDeleteFixedAssetEntryItemDetailBook, ListDeleteFixedAssetEntryItemDetail, ListDeleteFixedAssetEntryItem, audit, idSequense)
        End Using
        'Return Me._FixedAssetEntryAdminService.SaveFixedAssetEntry(FixedAssetEntry, ListDeleteFixedAssetEntryItemDetailPartBook, ListDeleteFixedAssetEntryItemDetailPart, ListDeleteFixedAssetEntryItemDetailBook, ListDeleteFixedAssetEntryItemDetail, ListDeleteFixedAssetEntryItem, audit, idSequense)
    End Function

    Public Function GetFixedAssetItemCatalogByItemId(ItemId As Integer) As Domain.Entities.FixedAssetItemCatalog Implements IFixedAssetEntryService.GetFixedAssetItemCatalogByItemId
        Using service As IFixedAssetEntryAdminService = Container.Current.Resolve(Of IFixedAssetEntryAdminService)()
            Return service.GetFixedAssetItemCatalogByItemId(ItemId)
        End Using
        'Return Me._FixedAssetEntryAdminService.GetFixedAssetItemCatalogByItemId(ItemId)
    End Function
    ''' <summary>
    ''' Obtiene la información necesaria para el  informe de Hoja de Vida del Activo
    ''' </summary>
    ''' <param name="AdquisitionDateStart">The adquisition date start.</param>
    ''' <param name="AdquisitionDateEnd">The adquisition date end.</param>
    ''' <param name="PlateStart">The plate start.</param>
    ''' <param name="PlateEnd">The plate end.</param>
    ''' <param name="session">The session.</param>
    ''' <returns></returns>
    Function GetListFixedAssetPhysicalAsset(AdquisitionDateStart As Integer, AdquisitionDateEnd As Integer, PlateStart As Integer, PlateEnd As Integer, ItemStart As Integer, ItemEnd As Integer, ItemTypeStart As Integer, ItemTypeEnd As Integer, LocationStart As Integer, LocationEnd As Integer, Responsible As String, ItemCatalog As String, StatusAsset As String, session As SessionValues) As DataSet Implements IFixedAssetEntryService.GetListFixedAssetPhysicalAsset
        Using service As IFixedAssetEntryAdminService = Container.Current.Resolve(Of IFixedAssetEntryAdminService)()
            Return service.GetListFixedAssetPhysicalAsset(AdquisitionDateStart, AdquisitionDateEnd, PlateStart, PlateEnd, ItemStart, ItemEnd, ItemTypeStart, ItemTypeEnd, LocationStart, LocationEnd, Responsible, ItemCatalog, StatusAsset, session)
        End Using
        'Return Me._FixedAssetEntryAdminService.GetListFixedAssetPhysicalAsset(AdquisitionDateStart, AdquisitionDateEnd, PlateStart, PlateEnd, ItemStart, ItemEnd, ItemTypeStart, ItemTypeEnd, LocationStart, LocationEnd, Responsible, ItemCatalog, StatusAsset, session)
    End Function

End Class
