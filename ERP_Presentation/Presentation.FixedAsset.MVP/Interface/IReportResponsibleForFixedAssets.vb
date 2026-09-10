Imports DevExpress.Xpo

Public Interface IReportResponsibleForFixedAssets

#Region "XPO"

    ''' <summary>
    ''' Establece el datasource de los responsables
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property ResponsibleXpo As XPCollection(Of Infrastructure.Data.Xpo.FixedAssetRepository.FixedAssetResponsibleReportXpo)

    ''' <summary>
    ''' Establece el datasource de los catálogos
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property ItemCatalogXpo As XPCollection(Of Infrastructure.Data.Xpo.FixedAssetRepository.FixedAssetItemCatalogReportXpo)

    ''' <summary>
    ''' Establece el datasource de los artículos
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property ItemXpo As XPCollection(Of Infrastructure.Data.Xpo.FixedAssetRepository.FixedAssetItemReportXpo)

    ''' <summary>
    ''' Establece el datasource de los tipos de activos
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property ItemTypeXpo As XPCollection(Of Infrastructure.Data.Xpo.FixedAssetRepository.FixedAssetItemTypeReportXpo)

    ''' <summary>
    ''' Establece el datasource de las ubicaciones
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property LocationXpo As XPCollection(Of Infrastructure.Data.Xpo.FixedAssetRepository.FixedAssetLocationReportXpo)

    ''' <summary>
    ''' Establece el datasource de los activos
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property PhysicalAssetXpo As XPCollection(Of Infrastructure.Data.Xpo.FixedAssetRepository.FixedAssetPhysicalAssetReportXpo)

#End Region

End Interface
