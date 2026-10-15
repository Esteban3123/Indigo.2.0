CREATE VIEW [Maintenance].[ViewMaintenanceResponsibleItemCatalogs]
AS
SELECT	mr.Id,
		tp.Nit ThirdPartyNit,
		tp.Name ThirdPartyName,
		mr.ResponsibleRole,
		CONCAT(',',rcoa.ItemCatalogIds, ',') ItemCatalogIds
FROM Common.ThirdParty tp WITH (NOLOCK)
JOIN Maintenance.MaintenanceResponsible mr WITH (NOLOCK) ON tp.Id = mr.ThirdPartyId
JOIN 
(
	SELECT rcoa.ResponsibleId, STRING_AGG(rcoa.ItemCatalogId, ',') WITHIN GROUP (ORDER BY rcoa.ItemCatalogId) ItemCatalogIds
	FROM Maintenance.ResponsibleCatalogOfArticles rcoa WITH (NOLOCK) 
	GROUP BY rcoa.ResponsibleId
) rcoa ON mr.Id = rcoa.ResponsibleId
WHERE mr.Status = 1