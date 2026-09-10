

CREATE VIEW [MixingStation].[ViewHistoricCampaing] 
AS
WITH dataTemp
AS
(SELECT data.CampaignDetailId
	   ,SUM(data.Quantity) Quantity
	FROM (SELECT
			rd.CampaignDetailId
		   ,SUM(rd.Quantity) Quantity
		FROM MixingStation.RequestMixingStationDetail rd WITH (NOLOCK)
		WHERE rd.CampaignDetailId IS NOT NULL
		GROUP BY rd.CampaignDetailId) data
	GROUP BY data.CampaignDetailId)

SELECT
	CONCAT(cd.Id, '') Id
   ,cd.Id CampaignDetailId
   ,c.Id CampaignId
   ,cd.CampaignNumber
   ,CONCAT('Campaña # ', cd.CampaignNumber) CampaignDescription
   ,cd.ProductionLineId
   ,c.CMConfigurationId
   ,dataTemp.Quantity
   ,ps.Code ProductionScheduleCode
   ,cd.CampaignStatus
   ,CASE cd.CampaignStatus
		WHEN 1 THEN 'Abierta'
		WHEN 2 THEN 'Cerrada'
		WHEN 3 THEN 'Bloqueada'
		WHEN 4 THEN 'Anulada'
		WHEN 5 THEN 'Procesada'
		WHEN 6 THEN 'Terminada'
	END CampaignStatusName
   ,cd.ProcessingDate
   ,cd.LabelConfirmationDate
   ,cd.CreationDate AS CampaignCreationDate
   ,rl.WorkingAreaId
   ,wa.Description AS WorkingAreaName
   ,(SELECT TOP 1 ce.IdWarehouse
		FROM MixingStation.CMWarehouse ce 
		LEFT JOIN MixingStation.CMConfiguration cn WITH (NOLOCK) ON c.CMConfigurationId = cn.Id
		WHERE ce.WarehouseType = 1 AND ce.IdMixingStation = cn.Id)
	AS MateriaPrimaStock
   ,(SELECT TOP 1 ce.IdWarehouse
		FROM MixingStation.CMWarehouse ce
		LEFT JOIN MixingStation.CMConfiguration cn WITH (NOLOCK) ON c.CMConfigurationId = cn.Id
		WHERE ce.WarehouseType = 2 AND ce.IdMixingStation = cn.Id)
	AS Almacen
   ,(SELECT TOP 1 ce.IdWarehouse
		FROM MixingStation.CMWarehouse ce
		LEFT JOIN MixingStation.CMConfiguration cn WITH (NOLOCK) ON c.CMConfigurationId = cn.Id
		WHERE ce.WarehouseType = 6 AND ce.IdMixingStation = cn.Id)
	AS Remanente
   ,ut.Description AS NameUnitDoseType
   , ISNULL((SELECT TOP 1  cr.EntityId 
			FROM MixingStation.CampaignReports cr
			WHERE cd.Id = cr.campaignDetailId)
			, 0) IdTransferOrder
	--, ISNULL(rmsd.LabelType, 0) LabelType
	, ut.MSClass
FROM MixingStation.Campaign c WITH (NOLOCK)
JOIN MixingStation.CampaignDetail cd WITH (NOLOCK) ON c.Id = cd.CampaignId
LEFT JOIN MixingStation.UnitDoseType ut WITH (NOLOCK) ON ut.Id = cd.UnitDoseTypeId
LEFT JOIN dataTemp WITH (NOLOCK) ON cd.Id = dataTemp.CampaignDetailId
LEFT JOIN MixingStation.ProductionScheduleDetail psd WITH (NOLOCK) ON cd.Id = psd.CampaignDetailId
LEFT JOIN MixingStation.ProductionSchedule ps WITH (NOLOCK) ON psd.ProductionScheduleId = ps.Id
LEFT JOIN MixingStation.ReleaseLine rl (NOLOCK) ON cd.Id = rl.CampaignDetailId
LEFT JOIN MixingStation.WorkingArea wa (NOLOCK)ON wa.Id = rl.WorkingAreaId
--LEFT JOIN MixingStation.CampaignReports cr (NOLOCK) cd.Id = cr.CampaignDetailId
--LEFT JOIN MixingStation.RequestMixingStationDetail rmsd (NOLOCK) ON cd.Id = rmsd.CampaignDetailId
WHERE cd.CampaignStatus = 6
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Vista histórica de campañas terminadas en la estación de mezclas farmacéuticas. Consolida únicamente los lotes (campañas) con estado ''Terminada'' (estado 6), combinando el encabezado de campaña, el detalle del lote, el tipo de dosis unitaria, la cantidad total de solicitudes despachadas, el programa de producción asignado, el área de trabajo donde se liberó la línea, y los almacenes de materia prima, producto terminado y remanente asociados a la configuración de la estación. Sirve como fuente de reportería e historial para auditar el ciclo completo de preparación de mezclas: desde la creación del lote hasta su terminación, incluyendo fechas de procesamiento y confirmación de etiquetas, número de campaña, estado descriptivo y la orden de transferencia generada si aplica.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'VIEW', @level1name = N'ViewHistoricCampaing';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'VIEW', @level1name = N'ViewHistoricCampaing';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Expone el histórico de campañas de preparación de mezclas en estado Terminada, consolidando cantidades solicitadas, almacenes asociados a la estación, área de trabajo, programación y orden de traslado generada.', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'VIEW', @level1name=N'ViewHistoricCampaing';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Existen filas en MixingStation.CampaignDetail con CampaignStatus = 6 (Terminada).; Cada CampaignDetail está asociada a una Campaign vía CampaignId.; La configuración de la estación (CMConfiguration) tiene almacenes registrados en CMWarehouse para que los campos de bodega devuelvan valor.', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'VIEW', @level1name=N'ViewHistoricCampaing';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'La vista solo muestra campañas Terminadas (CampaignStatus = 6).; Los tipos de almacén están codificados: 1=Materia Prima, 2=Almacén general, 6=Remanente.; El estado de campaña se mapea a un dominio cerrado de 6 valores; otros estados producen NULL en CampaignStatusName.; CampaignDescription siempre tiene el formato ''Campaña # '' + CampaignNumber.; Si no existe orden de traslado registrada en CampaignReports, IdTransferOrder es 0 y nunca NULL.', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'VIEW', @level1name=N'ViewHistoricCampaing';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Campaña de mezcla; Estación de mezcla (Mixing Station); Línea de producción; Programación de producción; Dosis unitaria; Almacén / bodega (Materia Prima, Almacén, Remanente); Área de trabajo; Orden de traslado (TransferOrder); Estado de campaña (Abierta/Cerrada/Bloqueada/Anulada/Procesada/Terminada)', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'VIEW', @level1name=N'ViewHistoricCampaing';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] MixingStation.ViewHistoricCampaing: Solo retorna detalles de campaña con CampaignStatus = 6 (Terminada), filtrado por WHERE cd.CampaignStatus = 6.; [RETURN_RESULT] MixingStation.ViewHistoricCampaing: La cantidad (Quantity) se obtiene sumando RequestMixingStationDetail.Quantity agrupado por CampaignDetailId, ignorando filas con CampaignDetailId NULL.; [RETURN_RESULT] MixingStation.ViewHistoricCampaing: IdTransferOrder se resuelve como el primer EntityId de CampaignReports asociado al CampaignDetail; si no existe, se devuelve 0 (ISNULL).', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'VIEW', @level1name=N'ViewHistoricCampaing';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si cd.CampaignStatus IN (1..6) → Se traduce a etiqueta legible: 1=Abierta, 2=Cerrada, 3=Bloqueada, 4=Anulada, 5=Procesada, 6=Terminada (CampaignStatusName). else NULL; si CMWarehouse.WarehouseType = 1 para la CMConfiguration de la campaña → Se expone como MateriaPrimaStock.; si CMWarehouse.WarehouseType = 2 para la CMConfiguration de la campaña → Se expone como Almacen.; si CMWarehouse.WarehouseType = 6 para la CMConfiguration de la campaña → Se expone como Remanente.', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'VIEW', @level1name=N'ViewHistoricCampaing';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'MixingStation.RequestMixingStationDetail; MixingStation.Campaign; MixingStation.CampaignDetail; MixingStation.UnitDoseType; MixingStation.ProductionScheduleDetail; MixingStation.ProductionSchedule; MixingStation.ReleaseLine; MixingStation.WorkingArea; MixingStation.CMWarehouse; MixingStation.CMConfiguration; MixingStation.CampaignReports', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'VIEW', @level1name=N'ViewHistoricCampaing';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'VIEW', @level1name=N'ViewHistoricCampaing';
GO
