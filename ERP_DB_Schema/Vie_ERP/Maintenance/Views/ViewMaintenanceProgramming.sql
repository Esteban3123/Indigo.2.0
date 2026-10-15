CREATE VIEW [Maintenance].[ViewMaintenanceProgramming]
AS
	SELECT	CONCAT(fapa.Id, '-', mpp.Id, '-', 1) AS Id,
			fapa.Id AS PhysetAssetId,
			fapa.Plate,
			fapa.Serie,
			fapa.Model,
			pai.Id AS ItemId,
			pai.ItemTypeId AS ItemTypeId,
			pai.Code AS ArticleCode,
			pai.[Description] AS ArticleName,
			pait.InventoryTypeId AS InventoryTypeId,
			fait.Type InventoryType,
			fal.Id AS LocationId,
			fal.Code AS LocationCode,
			fal.[Name] AS LocationName,
			CONCAT(fal.code, ' - ', fal.[Name]) AS LocationCodeName,
			CONCAT(fat.Code, ' - ', fat.[Name]) AS TrademarkCodeName,
			mpm.Id AS ProgramingId,
			IIF(mpm.Id IS NULL, 'NO', 'SI') AS Programado,
			CASE
				WHEN (mpm.EquipmentFunction + mpm.RegisterApplication + mpm.MaintenanceRequirement + mpm.Backgrounds) < 15 THEN '12 meses'
				WHEN (mpm.EquipmentFunction + mpm.RegisterApplication + mpm.MaintenanceRequirement + mpm.Backgrounds) <= 19 THEN '6 meses'
				WHEN (mpm.EquipmentFunction + mpm.RegisterApplication + mpm.MaintenanceRequirement + mpm.Backgrounds) <= 22 THEN '4 meses'
				WHEN (mpm.EquipmentFunction + mpm.RegisterApplication + mpm.MaintenanceRequirement + mpm.Backgrounds) <= 25 THEN '3 meses'
				WHEN (mpm.EquipmentFunction + mpm.RegisterApplication + mpm.MaintenanceRequirement + mpm.Backgrounds) > 25 THEN '2 meses'
			END AS Periodicidad,
			mp.Color AS ProtocolColor,
			mp.Code AS ProtocolCode,
			mp.[Name] AS ProtocolName,
			CONCAT(mp.Code, ' - ', mp.[Name]) AS ProtocolCodeName,
			mr.Id AS ResponsibleId,
			mr.ReponsibleTypeId AS ReponsibleTypeId,
			tp.Nit AS ResponsibleNit,
			tp.[Name] AS ResponsibleName,
			CONCAT(tp.Nit, ' - ', tp.[Name]) AS ResponsibleCodeName,
			mpp.Id AS ProgramatedDateId,
			(
				SELECT TOP 1 DateProgramated 
				FROM Maintenance.MaintenancePlanProgramated WITH (NOLOCK)
				WHERE MaintenancePlanAndMetrologyId = mpm.Id AND FixedAssetPhysicalId = fapa.Id AND DateProgramated < Common.GETDATE() AND ProgramType = 1
				ORDER BY DateProgramated DESC
			) AS UltimoMantenimiento,
			ppd.DateProgramated AS ProximoMantenimiento,
			'Mantenimiento' AS TypeName
	FROM Maintenance.EquipmentRegistration er WITH (NOLOCK)
	JOIN FixedAsset.FixedAssetPhysicalAsset fapa WITH (NOLOCK) ON er.FixedAssetPhysicalAssetId = fapa.Id
	JOIN FixedAsset.FixedAssetItem pai WITH (NOLOCK) ON fapa.ItemId = pai.Id
	JOIN FixedAsset.FixedAssetItemType pait WITH (NOLOCK) ON pai.[ItemTypeId] = pait.Id
	JOIN FixedAsset.FixedAssetInventoryType fait WITH (NOLOCK) ON pait.InventoryTypeId = fait.Id
	JOIN FixedAsset.FixedAssetLocation fal WITH (NOLOCK) ON fapa.LocationId = fal.Id
	JOIN FixedAsset.FixedAssetTrademark fat WITH (NOLOCK) ON fapa.TrademarkId = fat.Id
	LEFT JOIN
	(
		SELECT MIN(pp.DateProgramated) AS DateProgramated, pp.FixedAssetPhysicalId, pp.MaintenancePlanAndMetrologyId 
		FROM Maintenance.MaintenancePlanProgramated pp WITH (NOLOCK)
		WHERE pp.DateProgramated >= Common.GETDATE() and pp.ProgramType = 1
		GROUP BY FixedAssetPhysicalId, MaintenancePlanAndMetrologyId
	) ppd ON ppd.FixedAssetPhysicalId = fapa.Id
	LEFT JOIN Maintenance.MaintenancePlanAndMetrology mpm WITH (NOLOCK) ON ppd.MaintenancePlanAndMetrologyId = mpm.Id
	LEFT JOIN Maintenance.MaintenanceProtocol mp WITH (NOLOCK) ON mpm.ProtocolMaintenanceId = mp.Id
	LEFT JOIN Maintenance.MaintenanceResponsible mr WITH (NOLOCK) ON mpm.ResponsibleMaintenanceId = mr.Id
	LEFT JOIN Common.ThirdParty tp WITH (NOLOCK) ON mr.ThirdPartyId = tp.Id
	LEFT JOIN Maintenance.MaintenancePlanProgramated mpp WITH (NOLOCK) ON ppd.DateProgramated = mpp.DateProgramated and mpp.FixedAssetPhysicalId = fapa.Id
UNION ALL
	SELECT	CONCAT(fapa.Id, '-', mpp.Id, '-', 2) AS Id,
			fapa.Id AS PhysetAssetId,
			fapa.Plate,
			fapa.Serie,
			fapa.Model,
			pai.Id AS ItemId,
			pai.ItemTypeId AS ItemTypeId,
			pai.Code AS ArticleCode,
			pai.[Description] AS ArticleName,
			pait.InventoryTypeId AS InventoryTypeId,
			fait.Type InventoryType,
			fal.Id AS LocationId,
			fal.Code AS LocationCode,
			fal.[Name] AS LocationName,
			CONCAT(fal.code, ' - ', fal.[Name]) AS LocationCodeName,
			CONCAT(fat.Code, ' - ', fat.[Name]) AS TrademarkCodeName,
			mpm.Id AS ProgramingId,
			IIF(mpm.Id IS NULL, 'NO', 'SI') AS Programado,
			CONCAT(mpm.FrequenceMetrologyValue, ' ', case mpm.FrequenceMetrologyUnit when 1 then 'Días' when 2 then 'Meses' when 3 then 'Años' else '' end) AS Periodicidad,
			mp.Color AS ProtocolColor,
			mp.Code AS ProtocolCode,
			mp.[Name] AS ProtocolName,
			CONCAT(mp.Code, ' - ', mp.[Name]) AS ProtocolCodeName,
			mr.Id AS ResponsibleId,
			mr.ReponsibleTypeId AS ReponsibleTypeId,
			tp.Nit AS ResponsibleNit,
			tp.[Name] AS ResponsibleName,
			CONCAT(tp.Nit, ' - ', tp.[Name]) AS ResponsibleCodeName,
			mpp.Id AS ProgramatedDateId,
			(
				SELECT TOP 1 DateProgramated 
				FROM Maintenance.MaintenancePlanProgramated WITH (NOLOCK)
				WHERE MaintenancePlanAndMetrologyId = mpm.Id AND FixedAssetPhysicalId = fapa.Id AND DateProgramated < Common.GETDATE() AND ProgramType = 2
				ORDER BY DateProgramated DESC
			) AS UltimoMantenimiento,
			ppd.DateProgramated AS ProximoMantenimiento,
			'Metrología' AS TypeName
	FROM Maintenance.EquipmentRegistration er WITH (NOLOCK)
	JOIN FixedAsset.FixedAssetPhysicalAsset fapa WITH (NOLOCK) ON er.FixedAssetPhysicalAssetId = fapa.Id
	JOIN FixedAsset.FixedAssetItem pai WITH (NOLOCK) ON fapa.ItemId = pai.Id
	JOIN FixedAsset.FixedAssetItemType pait WITH (NOLOCK) ON pai.[ItemTypeId] = pait.Id
	JOIN FixedAsset.FixedAssetInventoryType fait WITH (NOLOCK) ON pait.InventoryTypeId = fait.Id
	JOIN FixedAsset.FixedAssetLocation fal WITH (NOLOCK) ON fapa.LocationId = fal.Id
	JOIN FixedAsset.FixedAssetTrademark fat WITH (NOLOCK) ON fapa.TrademarkId = fat.Id
	JOIN
	(
		SELECT MIN(pp.DateProgramated) AS DateProgramated, pp.FixedAssetPhysicalId, pp.MaintenancePlanAndMetrologyId
		FROM Maintenance.MaintenancePlanProgramated pp WITH (NOLOCK)
		WHERE pp.DateProgramated >= Common.GETDATE() AND pp.ProgramType = 2
		GROUP BY FixedAssetPhysicalId, MaintenancePlanAndMetrologyId
	) ppd ON ppd.FixedAssetPhysicalId = fapa.Id
	LEFT JOIN Maintenance.MaintenancePlanAndMetrology mpm WITH (NOLOCK) ON ppd.MaintenancePlanAndMetrologyId = mpm.Id
	LEFT JOIN Maintenance.MaintenanceProtocol mp WITH (NOLOCK) ON mpm.ProtocolMetrologyId = mp.Id
	LEFT JOIN Maintenance.MaintenanceResponsible mr WITH (NOLOCK) ON mpm.ResponsibleMetrologyId = mr.Id
	LEFT JOIN Common.ThirdParty tp WITH (NOLOCK) ON mr.ThirdPartyId = tp.Id
	LEFT JOIN Maintenance.MaintenancePlanProgramated mpp WITH (NOLOCK) ON ppd.DateProgramated = mpp.DateProgramated and mpp.FixedAssetPhysicalId = fapa.Id