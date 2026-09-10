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
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Programación de mantenimiento y metrología de equipos biomédicos y activos fijos. Consolida en una sola consulta dos tipos de actividades planificadas: mantenimiento preventivo (con periodicidad calculada según criticidad del equipo: 2, 3, 4, 6 o 12 meses) y metrología/calibración (con frecuencia expresada en días, meses o años). Para cada equipo registrado muestra la placa, serie, modelo, artículo, tipo de inventario, ubicación o sede, marca, protocolo de mantenimiento, responsable (con NIT), fecha del último mantenimiento ejecutado y fecha del próximo mantenimiento programado. Sirve como base para reportes de cumplimiento del plan de mantenimiento, seguimiento de equipos vencidos o próximos a intervenir, y gestión del programa de tecnovigilancia y metrología de la institución.', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'VIEW', @level1name = N'ViewMaintenanceProgramming';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'VIEW', @level1name = N'ViewMaintenanceProgramming';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Consolida en una sola vista la programación vigente de mantenimiento y metrología por equipo/activo fijo, mostrando próximo y último servicio, periodicidad, protocolo y responsable.', @level0type=N'SCHEMA', @level0name=N'Maintenance', @level1type=N'VIEW', @level1name=N'ViewMaintenanceProgramming';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El activo debe estar registrado como equipo en Maintenance.EquipmentRegistration y enlazado a FixedAsset.FixedAssetPhysicalAsset; Para que aparezca un próximo mantenimiento/metrología debe existir un registro en Maintenance.MaintenancePlanProgramated con DateProgramated >= fecha actual y ProgramType=1 (mantenimiento) o ProgramType=2 (metrología); El activo debe tener Item, ItemType, InventoryType, Location y Trademark válidos (JOIN obligatorio)', @level0type=N'SCHEMA', @level0name=N'Maintenance', @level1type=N'VIEW', @level1name=N'ViewMaintenanceProgramming';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'La periodicidad de mantenimiento se calcula a partir de la suma de cuatro criterios de criticidad (función, aplicación, requerimientos, antecedentes) usando rangos fijos que clasifican en 12/6/4/3/2 meses; Mantenimiento usa ProgramType=1 con ProtocolMaintenanceId y ResponsibleMaintenanceId; Metrología usa ProgramType=2 con ProtocolMetrologyId y ResponsibleMetrologyId; Próximo mantenimiento siempre es la fecha mínima futura (>= hoy); último mantenimiento siempre es la fecha máxima pasada (< hoy); La rama Metrología requiere INNER JOIN al subconjunto ppd, por lo que solo aparecen activos con metrología programada futura; la rama Mantenimiento usa LEFT JOIN y muestra activos aunque no tengan próximo mantenimiento; Todas las lecturas usan WITH (NOLOCK) — pueden producir lecturas sucias', @level0type=N'SCHEMA', @level0name=N'Maintenance', @level1type=N'VIEW', @level1name=N'ViewMaintenanceProgramming';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Mantenimiento preventivo; Metrología; Activo fijo físico; Equipo biomédico; Protocolo de mantenimiento; Responsable de mantenimiento; Periodicidad de mantenimiento; Criticidad del equipo (función, aplicación, requerimientos, antecedentes); Marca y ubicación del activo; Tercero responsable (NIT)', @level0type=N'SCHEMA', @level0name=N'Maintenance', @level1type=N'VIEW', @level1name=N'ViewMaintenanceProgramming';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] Maintenance.ViewMaintenanceProgramming: Retorna dos conjuntos unidos por UNION ALL: TypeName=''Mantenimiento'' (ProgramType=1) y TypeName=''Metrología'' (ProgramType=2), con Id compuesto CONCAT(PhysicalAssetId,''-'',ProgramatedId,''-'',1|2)', @level0type=N'SCHEMA', @level0name=N'Maintenance', @level1type=N'VIEW', @level1name=N'ViewMaintenanceProgramming';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si mpm.Id IS NULL → Programado=''NO'' else Programado=''SI''; si Suma (EquipmentFunction+RegisterApplication+MaintenanceRequirement+Backgrounds) < 15 (rama Mantenimiento) → Periodicidad=''12 meses''; si Suma de criterios entre 15 y 19 → Periodicidad=''6 meses''; si Suma de criterios entre 20 y 22 → Periodicidad=''4 meses''; si Suma de criterios entre 23 y 25 → Periodicidad=''3 meses''; si Suma de criterios > 25 → Periodicidad=''2 meses''; si Rama Metrología: FrequenceMetrologyUnit=1/2/3 → Periodicidad se expresa en ''Días''/''Meses''/''Años'' respectivamente concatenado con FrequenceMetrologyValue else cadena vacía; si Próximo mantenimiento (ppd) → Se toma MIN(DateProgramated) con DateProgramated >= Common.GETDATE() agrupado por activo y plan; si Último mantenimiento (subconsulta TOP 1) → Se toma el DateProgramated más reciente con DateProgramated < Common.GETDATE() para el mismo plan, activo y ProgramType correspondiente', @level0type=N'SCHEMA', @level0name=N'Maintenance', @level1type=N'VIEW', @level1name=N'ViewMaintenanceProgramming';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'Common.GETDATE', @level0type=N'SCHEMA', @level0name=N'Maintenance', @level1type=N'VIEW', @level1name=N'ViewMaintenanceProgramming';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Maintenance.EquipmentRegistration; FixedAsset.FixedAssetPhysicalAsset; FixedAsset.FixedAssetItem; FixedAsset.FixedAssetItemType; FixedAsset.FixedAssetInventoryType; FixedAsset.FixedAssetLocation; FixedAsset.FixedAssetTrademark; Maintenance.MaintenancePlanProgramated; Maintenance.MaintenancePlanAndMetrology; Maintenance.MaintenanceProtocol; Maintenance.MaintenanceResponsible; Common.ThirdParty', @level0type=N'SCHEMA', @level0name=N'Maintenance', @level1type=N'VIEW', @level1name=N'ViewMaintenanceProgramming';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Maintenance', @level1type=N'VIEW', @level1name=N'ViewMaintenanceProgramming';
GO
