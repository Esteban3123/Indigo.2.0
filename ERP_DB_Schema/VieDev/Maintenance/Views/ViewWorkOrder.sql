CREATE VIEW [Maintenance].[ViewWorkOrder]
AS
	SELECT	wo.Id,
			wo.Consecutive AS WorkOrderCode,
			wo.RequestDate AS RequestDate,
			wo.ProgramDate AS ProgramDate,
			wo.State WorkOrderState,
			CASE wo.State
				WHEN 1 THEN 'Registrado'
				WHEN 2 THEN 'Confirmado'
				WHEN 3 THEN 'Anulado'
				WHEN 4 THEN 'Aprobado'
				WHEN 5 THEN 'Rechazado'
			END AS WorkOrderStateName,
			mp.Id AS ProtocolId,
			CONCAT(mp.Code, ' - ', mp.Name) AS ProtocolCodeName,
			mr.Id AS MaintenanceResponsibleId,
			mr.ReponsibleTypeId,
			CONCAT(mtp.Nit, ' - ', mtp.Name) AS MaintenanceResponsibleCodeName,
			mr.ResponsibleRole,
			-------------------------------------------------------------------
			fapa.Id AS PhysicalAssetId,
			fapa.Plate,
			fapa.Serie,
			fapa.Model,
			fai.Id AS ItemId,
			fai.ItemTypeId,
			CONCAT(fai.Code, ' - ', fai.Description) AS ItemCodeName,
			CONCAT(fapac.Code, ' - ', fapac.Name) PartCodeDescription,
			fait.InventoryTypeId,
			fal.Id AS LocationId,
			CONCAT(fal.code, ' - ', fal.Name) AS LocationCodeName,
			bo.Id AS BranchOfficeId,
			CONCAT(bo.Code, ' - ', bo.Name) AS BranchOfficeCodeName,
			CONCAT(fat.Code, ' - ', fat.Name) AS TrademarkCodeName,
			far.Id AS FixedAssetResponsibleId,
			CONCAT(ftp.Nit, ' - ', ftp.Name) AS FixedAssetResponsibleCodeName,
			u.UserCode FixedAssetUserCode
	FROM Maintenance.WorkOrder wo WITH (NOLOCK)
	JOIN Maintenance.MaintenanceProtocol mp WITH (NOLOCK) ON wo.ProtocolId = mp.Id
	JOIN Maintenance.MaintenanceResponsible mr WITH (NOLOCK) ON wo.MaintenanceResponsibleId = mr.Id
	JOIN Common.ThirdParty mtp WITH (NOLOCK) ON mr.ThirdPartyId = mtp.Id
	---------------------------------------------------------------------------
	JOIN FixedAsset.FixedAssetPhysicalAsset fapa WITH (NOLOCK) ON wo.PhysicalAssetId = fapa.Id
	JOIN FixedAsset.FixedAssetItem fai WITH (NOLOCK) ON fapa.ItemId = fai.Id
	JOIN FixedAsset.FixedAssetItemType fait WITH (NOLOCK) ON fai.ItemTypeId = fait.Id
	JOIN FixedAsset.FixedAssetLocation fal WITH (NOLOCK) ON fapa.LocationId = fal.Id
	JOIN Payroll.FunctionalUnit fu WITH (NOLOCK) ON fal.FunctionalUnitId = fu.Id
	JOIN Payroll.BranchOffice bo WITH (NOLOCK) ON fu.BranchOfficeId = bo.Id
	JOIN FixedAsset.FixedAssetTrademark fat WITH (NOLOCK) ON fapa.TrademarkId = fat.Id
	JOIN FixedAsset.FixedAssetResponsible far WITH (NOLOCK) ON fapa.ResponsibleId = far.Id
	JOIN Common.ThirdParty ftp WITH (NOLOCK) ON far.ThirdPartyId = ftp.Id
	---------------------------------------------------------------------------
	LEFT JOIN Security.Person sp ON ftp.Nit = sp.Identification
	LEFT JOIN Security.[User] u ON sp.Id = u.IdPerson
	---------------------------------------------------------------------------
	LEFT JOIN Maintenance.MaintenanceFailureRequestDetail mfrd WITH (NOLOCK) ON wo.EntityId = mfrd.Id AND wo.EntityName = 'MaintenanceFailureRequest'
	LEFT JOIN FixedAsset.FixedAssetPhysicalAssetParts fapap WITH (NOLOCK) ON mfrd.PhysicalAssetPartsId = fapap.Id
	LEFT JOIN FixedAsset.FixedAssetPartsAccesoriesConsumables fapac WITH (NOLOCK) ON fapap.PartAccesoriesConsumiblesId = fapac.Id