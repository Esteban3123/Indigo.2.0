CREATE VIEW [Maintenance].[ViewWorkOrderScheduledMaintenance]
AS
	SELECT	CONCAT(mpam.Id, '-', mpp.Id, '-', 2) AS Id,
			mpam.Id AS ProgramingId,
			CASE mpp.ProgramType
				WHEN 1 THEN mpam.ObservationMaintenance
				WHEN 2 THEN mpam.ObservationsMetrology
			END Observation,
			mpp.Id AS MaintenancePlanProgramatedId,
			CASE mpp.ProgramType
				WHEN 1 THEN 'Mantenimiento' 
				WHEN 2 THEN 'Metrología'
			END AS MaintenanceType,
			mpp.State ProgramState,
			CASE mpp.State
				WHEN 1 THEN 'Programado' 
				WHEN 2 THEN 'Con Orden de Trabajo'
				WHEN 3 THEN 'Anulado'
			END AS ProgramStateName,
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
			ISNULL(wo.RequestDate, mpp.DateProgramated) AS RequestDate,
			ISNULL(wo.ProgramDate, mpp.DateProgramated) AS ProgramDate,
			ISNULL(wo.State, 1) WorkOrderState,
			mp.Id AS ProtocolId,
			CONCAT(mp.Code, ' - ', mp.Name) AS ProtocolCodeName,
			mr.Id AS MaintenanceResponsibleId,
			mr.ReponsibleTypeId,
			CONCAT(tp.Nit, ' - ', tp.Name) AS ResponsibleCodeName,
			mr.ResponsibleRole
	FROM Maintenance.MaintenancePlanAndMetrology mpam WITH (NOLOCK)
	JOIN Maintenance.MaintenancePlanProgramated mpp WITH (NOLOCK) ON mpam.Id = mpp.MaintenancePlanAndMetrologyId
	JOIN FixedAsset.FixedAssetPhysicalAsset fapa WITH (NOLOCK) ON mpp.FixedAssetPhysicalId = fapa.Id
	JOIN FixedAsset.FixedAssetItem fai WITH (NOLOCK) ON fapa.ItemId = fai.Id
	JOIN FixedAsset.FixedAssetItemType fait WITH (NOLOCK) ON fai.ItemTypeId = fait.Id
	JOIN FixedAsset.FixedAssetLocation fal WITH (NOLOCK) ON fapa.LocationId = fal.Id
	JOIN Payroll.FunctionalUnit fu WITH (NOLOCK) ON fal.FunctionalUnitId = fu.Id
	JOIN Payroll.BranchOffice bo WITH (NOLOCK) ON fu.BranchOfficeId = bo.Id
	JOIN FixedAsset.FixedAssetTrademark fat WITH (NOLOCK) ON fapa.TrademarkId = fat.Id
	JOIN Maintenance.MaintenanceProtocol mp WITH (NOLOCK) ON IIF(mpp.ProgramType = 1, mpam.ProtocolMaintenanceId, mpam.ProtocolMetrologyId) = mp.Id
	JOIN Maintenance.MaintenanceResponsible mr WITH (NOLOCK) ON IIF(mpp.ProgramType = 1, mpam.ResponsibleMaintenanceId, mpam.ResponsibleMetrologyId) = mr.Id
	JOIN Common.ThirdParty tp WITH (NOLOCK) ON mr.ThirdPartyId = tp.Id
	LEFT JOIN Maintenance.WorkOrder wo WITH (NOLOCK) ON 'MaintenancePlanProgramated' = wo.EntityName AND mpp.Id = wo.EntityId