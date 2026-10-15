CREATE VIEW [Maintenance].[ViewListRequests]
AS
	SELECT	mfrd.Id,
			mfr.OperatingUnitId,
			mfr.Code,
			mfr.DateFailure,
			mfr.BranchOfficeId,
			'Activo' TypeDescription,
			fapa.Id PhysicalAssetId,
			fapa.Plate,
			fai.ItemCatalogId,
			CONCAT(faic.Code, ' - ', faic.Description) ItemCatalogCodeDescription,
			CONCAT(fai.Code, ' - ', fai.Description) ItemCodeDescription,
			CONCAT(fal.Code, ' - ', fal.Name) LocationCodeName,
			NULL PartCodeDescription,
			CONCAT(p.Identification, ' - ', p.Fullname) RequestUser,
			mr.Id MaintenanceResponsibleId,
			mr.ResponsibleRole,
			CONCAT(tp.Nit, ' - ', tp.Name) AssignUser,
			wo.Id WorkOrderId,
			wo.Consecutive WorkOrderCode,
			ISNULL(wo.ProgramDate, Common.GETDATE()) ProgramDate,
			COALESCE(wo.Description, mfrd.Description, mfr.Observation) Description,
			wo.State WorkOrderStatus
	FROM Maintenance.MaintenanceFailureRequest mfr WITH (NOLOCK)
	JOIN Maintenance.MaintenanceFailureRequestDetail mfrd WITH (NOLOCK) ON mfr.Id = mfrd.MaintenanceFailureRequestId
	JOIN FixedAsset.FixedAssetPhysicalAsset fapa WITH (NOLOCK) ON mfrd.PhysicalAssetId = fapa.Id
	JOIN FixedAsset.FixedAssetLocation fal WITH (NOLOCK) ON fapa.LocationId = fal.Id
	JOIN FixedAsset.FixedAssetItem fai WITH (NOLOCK) ON fapa.ItemId = fai.Id
	JOIN FixedAsset.FixedAssetItemCatalog faic WITH (NOLOCK) ON fai.ItemCatalogId = faic.Id
	-------------------------------------------------------------------------------------------------------------------
	LEFT JOIN Security.[User] u ON mfr.CreationUser = u.UserCode
	LEFT JOIN Security.Person p ON u.IdPerson = p.Id
	-------------------------------------------------------------------------------------------------------------------
	LEFT JOIN Maintenance.WorkOrder wo WITH (NOLOCK) ON mfrd.Id = wo.EntityId AND 'MaintenanceFailureRequest' = wo.EntityName
	LEFT JOIN Maintenance.MaintenanceResponsible mr WITH (NOLOCK) ON wo.MaintenanceResponsibleId = mr.Id
	LEFT JOIN Common.ThirdParty tp WITH (NOLOCK) ON mr.ThirdPartyId = tp.Id
	WHERE mfr.Status = 2 AND mfrd.TransactionClass = 1
UNION ALL
	SELECT	mfrd.Id,
			mfr.OperatingUnitId,
			mfr.Code,
			mfr.DateFailure,
			mfr.BranchOfficeId,
			'Parte de Activo' TypeDescription,
			fapa.Id PhysicalAssetId,
			fapa.Plate,
			fai.ItemCatalogId,
			CONCAT(faic.Code, ' - ', faic.Description) ItemCatalogCodeDescription,
			CONCAT(fai.Code, ' - ', fai.Description) ItemCodeDescription,
			CONCAT(fal.Code, ' - ', fal.Name) LocationCodeName,
			CONCAT(fapac.Code, ' - ', fapac.Name) PartCodeDescription,
			CONCAT(p.Identification, ' - ', p.Fullname) RequestUser,
			mr.Id MaintenanceResponsibleId,
			mr.ResponsibleRole,
			CONCAT(tp.Nit, ' - ', tp.Name) AssignUser,
			wo.Id WorkOrderId,
			wo.Consecutive WorkOrderCode,
			ISNULL(wo.ProgramDate, Common.GETDATE()) ProgramDate,
			COALESCE(wo.Description, mfrd.Description, mfr.Observation) Description,
			wo.State WorkOrderStatus
	FROM Maintenance.MaintenanceFailureRequest mfr WITH (NOLOCK)
	JOIN Maintenance.MaintenanceFailureRequestDetail mfrd WITH (NOLOCK) ON mfr.Id = mfrd.MaintenanceFailureRequestId
	JOIN FixedAsset.FixedAssetPhysicalAssetParts fapap WITH (NOLOCK) ON mfrd.PhysicalAssetPartsId = fapap.Id
	JOIN FixedAsset.FixedAssetPhysicalAsset fapa WITH (NOLOCK) ON fapap.PhysicalAssetId = fapa.Id
	JOIN FixedAsset.FixedAssetLocation fal WITH (NOLOCK) ON fapa.LocationId = fal.Id
	JOIN FixedAsset.FixedAssetItem fai WITH (NOLOCK) ON fapa.ItemId = fai.Id
	JOIN FixedAsset.FixedAssetItemCatalog faic WITH (NOLOCK) ON fai.ItemCatalogId = faic.Id
	JOIN FixedAsset.FixedAssetPartsAccesoriesConsumables fapac WITH (NOLOCK) ON fapap.PartAccesoriesConsumiblesId = fapac.Id
	-------------------------------------------------------------------------------------------------------------------
	LEFT JOIN Security.[User] u ON mfr.CreationUser = u.UserCode
	LEFT JOIN Security.Person p ON u.IdPerson = p.Id
	-------------------------------------------------------------------------------------------------------------------
	LEFT JOIN Maintenance.WorkOrder wo WITH (NOLOCK) ON mfrd.Id = wo.EntityId AND 'MaintenanceFailureRequest' = wo.EntityName
	LEFT JOIN Maintenance.MaintenanceResponsible mr WITH (NOLOCK) ON wo.MaintenanceResponsibleId = mr.Id
	LEFT JOIN Common.ThirdParty tp WITH (NOLOCK) ON mr.ThirdPartyId = tp.Id
	WHERE mfr.Status = 2 AND mfrd.TransactionClass = 2