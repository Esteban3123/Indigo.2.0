
CREATE view [MixingStation].[ViewReleaseLineToReport]
As

WITH last_release_line(CampaignDetailId, ReleaseLineId, UnitDoseCodeName, WorkingAreaId, BatchesProcessed) as (
	select top 1 lcd.Id
		, lrl.Id as ReleaseLineId
		, concat(lud.Code, ' - ', lud.Description) as UnitDoseCodeName		
		, lrl.WorkingAreaId
		, COUNT(rpds.Id) BatchesProcessed
	from MixingStation.CampaignDetail lcd WITH(NOLOCK)
	INNER join MixingStation.ReleaseLine lrl WITH(NOLOCK) ON lrl.CampaignDetailId = lcd.Id
	INNER join MixingStation.UnitDoseType lud WITH(NOLOCK) ON lcd.UnitDoseTypeId = lud.Id
	INNER JOIN MixingStation.RequestMixingStationDetail rmsd ON rmsd.CampaignDetailId = lcd.Id
	INNER JOIN MixingStation.RequestPackageDetailStatus rpds ON rpds.RequestMixingStationDetailId = rmsd.Id AND rpds.Status = 3
	GROUP BY lcd.Id, lrl.Id, lud.Code, lud.Description, lrl.WorkingAreaId, lcd.ModificationDate
	ORDER BY lcd.ModificationDate DESC
)
SELECT rl.Id
		, rl.CampaignDetailId
		, rl.WorkingAreaId
		, rl.AirIgnitionTime
		, rl.CPIIgnitionTime
		, rl.EntryMixingStationTime
		, rl.PreparationStartTime
		, rl.IsSterile
		, rl.AdequacyItem1
		, rl.AdequacyItem2
		, rl.AdequacyItem3
		, rl.AdequacyItem4
		, rl.AdequacyItem5
		, rl.AdequacyItem6
		, rl.AdequacyItem7
		, rl.AdequacyItem8
		, rl.ConditioningItem1
		, rl.ConditioningItem2
		, rl.ConditioningItem3
		, rl.ConditioningItem4
		, rl.ConditioningItem5
		, rl.ConditioningItem6
		, rl.ConditioningItem7
		, rl.ConditioningItem8
		, rl.ApplyItem9
		, rl.ApplyItem11
		, rl.ValueItem9
		, rl.ValueItem11
		, rl.Observation
		, COUNT(rpds.Id) BatchesProcess
		, cd.ProductionLineId
		, cd.UnitDoseTypeId
		, cd.CampaignStatus
		, pl.Code as ProductionLineCode
		, pl.Name as ProductionLineName
		, ud.Code as UnitDoseCode
		, ud.Description as UnitDoseName
		, wa.Code as WorkingAreaCode
		, wa.Description as WorkingAreaName
		, rl.CreationUser as CreationUser
		, rl.CreationDate
		, lrl.UnitDoseCodeName as LastUnitDoseCodeName
		, lrl.BatchesProcessed LastCampaignDetailBatchesProcessed
		, cdu.UserCode As UserSupervisor
from MixingStation.ReleaseLine rl with(nolock)
INNER JOIN MixingStation.CampaignDetail cd with(nolock) on rl.campaignDetailid = cd.Id
INNER JOIN MixingStation.UnitDoseType ud with(nolock) on cd.UnitDoseTypeId = ud.Id
INNER JOIN MixingStation.ProductionLine pl with(nolock) on cd.ProductionLineId = pl.Id
INNER JOIN MixingStation.WorkingArea wa with(nolock) on rl.WorkingAreaId = wa.Id
INNER JOIN MixingStation.RequestMixingStationDetail rmsd ON rmsd.CampaignDetailId = cd.Id
INNER JOIN MixingStation.RequestPackageDetailStatus rpds ON rpds.RequestMixingStationDetailId = rmsd.Id AND rpds.Status = 3

LEFT JOIN MixingStation.CampaignDetailUsers cdu with(nolock) on cdu.CampaignDetailId = cd.Id and cdu.UserRole = 1
LEFT JOIN last_release_line lrl with(nolock) on lrl.WorkingAreaId = rl.WorkingAreaId And lrl.CampaignDetailId <> rl.CampaignDetailId
GROUP BY rl.Id, rl.CampaignDetailId, rl.WorkingAreaId, rl.AirIgnitionTime, rl.CPIIgnitionTime, rl.EntryMixingStationTime,rl.PreparationStartTime,
rl.IsSterile, rl.AdequacyItem1, rl.AdequacyItem2, rl.AdequacyItem3, rl.AdequacyItem4, rl.AdequacyItem5, rl.AdequacyItem6, rl.AdequacyItem7,
rl.AdequacyItem8, rl.ConditioningItem1, rl.ConditioningItem2, rl.ConditioningItem3, rl.ConditioningItem4, rl.ConditioningItem5, rl.ConditioningItem6,
rl.ConditioningItem7, rl.ConditioningItem8, rl.ApplyItem9, rl.ApplyItem11, rl.ValueItem9, rl.ValueItem11, rl.Observation, cd.ProductionLineId,
cd.UnitDoseTypeId, cd.CampaignStatus, pl.Code, pl.Name, ud.Code, ud.Description, wa.Code, wa.Description, rl.CreationUser, rl.CreationDate, lrl.UnitDoseCodeName,
lrl.BatchesProcessed, cdu.UserCode
GO
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Vista de reporte para las líneas de liberación (release lines) de campañas de preparación farmacéutica en la estación de mezclas. Consolida, por cada línea de liberación, los datos operativos del proceso: tiempos de encendido de aire y CPI, hora de ingreso a la estación, inicio de preparación, condición estéril, ítems de adecuación y acondicionamiento verificados, observaciones, y la cantidad de lotes (bolsas/envases) procesados con estado aprobado (status 3). Integra información de la campaña farmacéutica (CampaignDetail), el tipo de dosis unitaria, la línea de producción, el área de trabajo y el supervisor asignado al lote. Adicionalmente, incorpora mediante un CTE el contexto de la última campaña procesada en el mismo área de trabajo (tipo de dosis y cantidad de lotes previos), permitiendo comparar el estado actual con el historial reciente para reportería de control de calidad y trazabilidad del proceso de mezclas.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'VIEW', @level1name = N'ViewReleaseLineToReport';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'VIEW', @level1name = N'ViewReleaseLineToReport';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Vista que consolida información de líneas de liberación de la estación de mezclas con datos de campaña, línea de producción, dosis unitaria, área de trabajo, supervisor y métricas de lotes procesados, incluyendo referencia a la última liberación previa en la misma área.', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'VIEW', @level1name=N'ViewReleaseLineToReport';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Deben existir paquetes de la campaña con estado 3 (RequestPackageDetailStatus.Status=3) para que la línea de liberación aparezca en la vista (INNER JOIN obliga su presencia).; Cada ReleaseLine debe tener CampaignDetail, UnitDoseType, ProductionLine y WorkingArea referenciados válidos (INNER JOIN).', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'VIEW', @level1name=N'ViewReleaseLineToReport';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'BatchesProcess refleja únicamente paquetes con Status=3 (procesados/liberados).; La CTE last_release_line selecciona TOP 1 ordenado por CampaignDetail.ModificationDate DESC, representando la campaña más recientemente modificada distinta de la actual en la misma WorkingArea.; UnitDoseCodeName se construye como concatenación ''Code - Description'' del tipo de dosis unitaria.; Solo se incluyen líneas de liberación cuya campaña tiene al menos un detalle de solicitud con paquetes en Status=3 (filtrado natural por INNER JOIN).', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'VIEW', @level1name=N'ViewReleaseLineToReport';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Línea de liberación; Estación de mezclas; Campaña de preparación; Dosis unitaria; Línea de producción; Área de trabajo; Lote/paquete procesado; Supervisor de campaña; Acondicionamiento; Adecuación; Preparación estéril', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'VIEW', @level1name=N'ViewReleaseLineToReport';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] MixingStation.ReleaseLine: Devuelve una fila por ReleaseLine agrupada, contando paquetes con Status=3 como BatchesProcess y enriqueciendo con datos de campaña, línea de producción, dosis unitaria, área de trabajo y supervisor.', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'VIEW', @level1name=N'ViewReleaseLineToReport';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si RequestPackageDetailStatus.Status = 3 → Solo se cuentan/incluyen paquetes con estado 3 (paquetes procesados) tanto en la métrica BatchesProcess como en la CTE last_release_line.BatchesProcessed.; si CampaignDetailUsers.UserRole = 1 → Se toma el usuario con rol 1 como UserSupervisor (LEFT JOIN, opcional).; si last_release_line.WorkingAreaId = rl.WorkingAreaId AND last_release_line.CampaignDetailId <> rl.CampaignDetailId → Se asocia como ''última liberación previa'' (LastUnitDoseCodeName, LastCampaignDetailBatchesProcessed) la línea de otra campaña en la misma área de trabajo.', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'VIEW', @level1name=N'ViewReleaseLineToReport';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'MixingStation.CampaignDetail; MixingStation.ReleaseLine; MixingStation.UnitDoseType; MixingStation.RequestMixingStationDetail; MixingStation.RequestPackageDetailStatus; MixingStation.ProductionLine; MixingStation.WorkingArea; MixingStation.CampaignDetailUsers', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'VIEW', @level1name=N'ViewReleaseLineToReport';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'VIEW', @level1name=N'ViewReleaseLineToReport';
GO
