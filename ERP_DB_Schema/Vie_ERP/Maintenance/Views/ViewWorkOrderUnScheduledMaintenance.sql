CREATE VIEW [Maintenance].[ViewWorkOrderUnScheduledMaintenance]
AS
	SELECT	CONCAT(mfr.Id, '-', mfrd.Id, '-', 2) AS Id,
			mfr.Id AS FailureRequestId,
			mfr.Code AS FailureRequestCode,
			ISNULL(mfrd.Description, mfr.Observation) Observation,
			CASE mfr.TypeRequest
				WHEN 1 THEN 'Falla General' 
				WHEN 2 THEN 'Revisión'
				WHEN 3 THEN 'Otro'
			END AS MaintenanceType,
			CASE 
				WHEN mfr.Status = 1 THEN 0
				WHEN mfr.Status = 2 AND wo.Id IS NULL THEN 1
				WHEN mfr.Status = 2 AND wo.Id IS NOT NULL THEN 2
				WHEN mfr.Status = 3 THEN 3
			END ProgramState,
			CASE 
				WHEN mfr.Status = 1 THEN 'Registrado'
				WHEN mfr.Status = 2 AND wo.Id IS NULL THEN 'Confirmado'
				WHEN mfr.Status = 2 AND wo.Id IS NOT NULL THEN 'Con Orden de Trabajo'
				WHEN mfr.Status = 3 THEN 'Anulado'
			END AS ProgramStateName,
			mfrd.Id AS FailureRequestDetailId,
			'' PartCodeDescription,
			fapa.Id AS PhysicalAssetId,
			fapa.Plate,
			fapa.Serie,
			fapa.Model,
			fai.Id AS ItemId,
			fai.ItemTypeId,
			CONCAT(fai.Code, ' - ', fai.Description) AS ItemCodeName,
			fait.InventoryTypeId,
			fal.Id AS LocationId,
			CONCAT(fal.code, ' - ', fal.Name) AS LocationCodeName,
			bo.Id AS BranchOfficeId,
			CONCAT(bo.Code, ' - ', bo.Name) AS BranchOfficeCodeName,
			CONCAT(fat.Code, ' - ', fat.Name) AS TrademarkCodeName,
			wo.Id AS WorkOrderId,
			wo.Consecutive AS WorkOrderCode,
			ISNULL(wo.RequestDate, mfr.DateFailure) AS RequestDate,
			ISNULL(wo.ProgramDate, mfr.DateFailure) AS ProgramDate,
			ISNULL(wo.State, 1) WorkOrderState,
			mp.Id AS ProtocolId,
			CONCAT(mp.Code, ' - ', mp.Name) AS ProtocolCodeName,
			mr.Id MaintenanceResponsibleId,
			mr.ReponsibleTypeId,
			CONCAT(tp.Nit, ' - ', tp.Name) AS ResponsibleCodeName,
			mr.ResponsibleRole
	FROM Maintenance.MaintenanceFailureRequest mfr WITH (NOLOCK)
	JOIN Maintenance.MaintenanceFailureRequestDetail mfrd WITH (NOLOCK) ON mfr.Id = mfrd.MaintenanceFailureRequestId
	JOIN FixedAsset.FixedAssetPhysicalAsset fapa WITH (NOLOCK) ON mfrd.PhysicalAssetId = fapa.Id
	JOIN FixedAsset.FixedAssetItem fai WITH (NOLOCK) ON fapa.ItemId = fai.Id
	JOIN FixedAsset.FixedAssetItemType fait WITH (NOLOCK) ON fai.ItemTypeId = fait.Id
	JOIN FixedAsset.FixedAssetLocation fal WITH (NOLOCK) ON fapa.LocationId = fal.Id
	JOIN Payroll.FunctionalUnit fu WITH (NOLOCK) ON fal.FunctionalUnitId = fu.Id
	JOIN Payroll.BranchOffice bo WITH (NOLOCK) ON fu.BranchOfficeId = bo.Id
	JOIN FixedAsset.FixedAssetTrademark fat WITH (NOLOCK) ON fapa.TrademarkId = fat.Id
	LEFT JOIN Maintenance.WorkOrder wo WITH (NOLOCK) ON 'MaintenanceFailureRequest' = wo.EntityName AND mfrd.Id = wo.EntityId
	LEFT JOIN Maintenance.MaintenanceProtocol mp WITH (NOLOCK) ON wo.ProtocolId = mp.Id
	LEFT JOIN Maintenance.MaintenanceResponsible mr WITH (NOLOCK) ON wo.MaintenanceResponsibleId = mr.Id
	LEFT JOIN Common.ThirdParty tp WITH (NOLOCK) ON mr.ThirdPartyId = tp.Id
	WHERE mfrd.TransactionClass = 1
UNION ALL
	SELECT	CONCAT(mfr.Id, '-', mfrd.Id, '-', 2) AS Id,
			mfr.Id AS FailureRequestId,
			mfr.Code AS FailureRequestCode,
			ISNULL(mfrd.Description, mfr.Observation) Observation,
			CASE mfr.TypeRequest
				WHEN 1 THEN 'Falla General' 
				WHEN 2 THEN 'Revisión'
				WHEN 3 THEN 'Otro'
			END AS MaintenanceType,
			CASE 
				WHEN mfr.Status = 1 THEN 0
				WHEN mfr.Status = 2 AND wo.Id IS NULL THEN 1
				WHEN mfr.Status = 2 AND wo.Id IS NOT NULL THEN 2
				WHEN mfr.Status = 3 THEN 3
			END ProgramState,
			CASE 
				WHEN mfr.Status = 1 THEN 'Registrado'
				WHEN mfr.Status = 2 AND wo.Id IS NULL THEN 'Confirmado'
				WHEN mfr.Status = 2 AND wo.Id IS NOT NULL THEN 'Con Orden de Trabajo'
				WHEN mfr.Status = 3 THEN 'Anulado'
			END AS ProgramStateName,
			mfrd.Id AS FailureRequestDetailId,
			CONCAT(fapac.Code, ' - ', fapac.Name) PartCodeDescription,
			fapa.Id AS PhysicalAssetId,
			fapa.Plate,
			fapa.Serie,
			fapa.Model,
			fai.Id AS ItemId,
			fai.ItemTypeId,
			CONCAT(fai.Code, ' - ', fai.Description) AS ItemCodeName,
			fait.InventoryTypeId,
			fal.Id AS LocationId,
			CONCAT(fal.code, ' - ', fal.Name) AS LocationCodeName,
			bo.Id AS BranchOfficeId,
			CONCAT(bo.Code, ' - ', bo.Name) AS BranchOfficeCodeName,
			CONCAT(fat.Code, ' - ', fat.Name) AS TrademarkCodeName,
			wo.Id AS WorkOrderId,
			wo.Consecutive AS WorkOrderCode,
			ISNULL(wo.RequestDate, mfr.DateFailure) AS RequestDate,
			ISNULL(wo.ProgramDate, mfr.DateFailure) AS ProgramDate,
			ISNULL(wo.State, 1) WorkOrderState,
			mp.Id AS ProtocolId,
			CONCAT(mp.Code, ' - ', mp.Name) AS ProtocolCodeName,
			mr.Id MaintenanceResponsibleId,
			mr.ReponsibleTypeId,
			CONCAT(tp.Nit, ' - ', tp.Name) AS ResponsibleCodeName,
			mr.ResponsibleRole
	FROM Maintenance.MaintenanceFailureRequest mfr WITH (NOLOCK)
	JOIN Maintenance.MaintenanceFailureRequestDetail mfrd WITH (NOLOCK) ON mfr.Id = mfrd.MaintenanceFailureRequestId
	JOIN FixedAsset.FixedAssetPhysicalAssetParts fapap WITH (NOLOCK) ON mfrd.PhysicalAssetPartsId = fapap.Id
	JOIN FixedAsset.FixedAssetPartsAccesoriesConsumables fapac WITH (NOLOCK) ON fapap.PartAccesoriesConsumiblesId = fapac.Id
	JOIN FixedAsset.FixedAssetPhysicalAsset fapa WITH (NOLOCK) ON fapap.PhysicalAssetId = fapa.Id
	JOIN FixedAsset.FixedAssetItem fai WITH (NOLOCK) ON fapa.ItemId = fai.Id
	JOIN FixedAsset.FixedAssetItemType fait WITH (NOLOCK) ON fai.ItemTypeId = fait.Id
	JOIN FixedAsset.FixedAssetLocation fal WITH (NOLOCK) ON fapa.LocationId = fal.Id
	JOIN Payroll.FunctionalUnit fu WITH (NOLOCK) ON fal.FunctionalUnitId = fu.Id
	JOIN Payroll.BranchOffice bo WITH (NOLOCK) ON fu.BranchOfficeId = bo.Id
	JOIN FixedAsset.FixedAssetTrademark fat WITH (NOLOCK) ON fapa.TrademarkId = fat.Id
	LEFT JOIN Maintenance.WorkOrder wo WITH (NOLOCK) ON 'MaintenanceFailureRequest' = wo.EntityName AND mfrd.Id = wo.EntityId
	LEFT JOIN Maintenance.MaintenanceProtocol mp WITH (NOLOCK) ON wo.ProtocolId = mp.Id
	LEFT JOIN Maintenance.MaintenanceResponsible mr WITH (NOLOCK) ON wo.MaintenanceResponsibleId = mr.Id
	LEFT JOIN Common.ThirdParty tp WITH (NOLOCK) ON mr.ThirdPartyId = tp.Id
	WHERE mfrd.TransactionClass = 2