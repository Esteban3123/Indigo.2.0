CREATE VIEW [Maintenance].[ViewMaintenanceResponsibleItemCatalog]
AS
SELECT	CONCAT(mr.Id, ' - ', rcoa.Id) RowId,
		faic.Id,
		faic.Code, 
		faic.Description,
		mr.Id ResponsibleId
FROM FixedAsset.FixedAssetItemCatalog faic WITH (NOLOCK)
JOIN Maintenance.ResponsibleCatalogOfArticles rcoa WITH (NOLOCK) ON faic.Id = rcoa.ItemCatalogId
JOIN Maintenance.MaintenanceResponsible mr WITH (NOLOCK) ON rcoa.ResponsibleId = mr.Id
WHERE mr.Status = 1