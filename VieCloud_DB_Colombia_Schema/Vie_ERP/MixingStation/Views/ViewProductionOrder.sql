

CREATE VIEW [MixingStation].[ViewProductionOrder] 
AS
	WITH dataTemp AS
	(
		SELECT data.CampaignDetailId, SUM(data.Quantity) Quantity
		FROM
		(
			SELECT rd.CampaignDetailId, SUM(rd.Quantity) Quantity
			FROM MixingStation.RequestMixingStationDetail rd WITH (NOLOCK)
			WHERE rd.CampaignDetailId IS NOT NULL
			GROUP BY rd.CampaignDetailId
		) data 
		GROUP BY data.CampaignDetailId
	)

	SELECT	CONCAT(cd.Id, '') as Id, 
			cd.Id as CampaignDetailId, 
			c.Id as CampaignId,
			cd.CampaignNumber, 
			CONCAT('Campaña # ', cd.CampaignNumber) as CampaignDescription,
			cd.ProductionLineId, 
			c.CMConfigurationId, 
			dt.Quantity,
			ps.Code as ProductionScheduleCode,
			cd.CampaignStatus,
			CASE cd.CampaignStatus
			WHEN 1 THEN 'Abierta'
			WHEN 2 THEN 'Cerrada'
			WHEN 3 THEN 'Bloqueada'
			WHEN 4 THEN 'Anulada'
			WHEN 5 THEN 'Procesada'
			WHEN 6 THEN 'Terminada'
			END CampaignStatusName,
			cd.Status,
			cd.ProcessingDate,
			cd.LabelConfirmationDate,
			cd.CreationDate as CampaignCreationDate,
			rl.WorkingAreaId,
			wa.Description as WorkingAreaName,
			ud.MSClass as UnitDoseTypeClass,
			cast(Iif(cd.LabelConfirmationDate is not null, 1, 0) as bit) as IsManagedLabel
	FROM MixingStation.Campaign c (NOLOCK)
	JOIN MixingStation.CampaignDetail cd (NOLOCK) ON c.Id = cd.CampaignId
	JOIN MixingStation.UnitDoseType ud (NOLOCK) on cd.UnitDoseTypeId = ud.Id
	LEFT JOIN MixingStation.ReleaseLine rl (NOLOCK) on cd.Id = rl.CampaignDetailId
	LEFT JOIN MixingStation.WorkingArea wa (NOLOCK) on wa.Id = rl.WorkingAreaId
	LEFT JOIN dataTemp as dt (NOLOCK) ON cd.Id = dt.CampaignDetailId
	LEFT JOIN MixingStation.ProductionScheduleDetail psd (NOLOCK) ON cd.Id = psd.CampaignDetailId
	LEFT JOIN MixingStation.ProductionSchedule ps (NOLOCK) ON psd.ProductionScheduleId = ps.Id
	WHERE cd.CampaignStatus <> 6
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Órdenes de producción activas de la estación de mezclas farmacéuticas: consolida las campañas y sus detalles de lote (excluyendo las terminadas) junto con la cantidad total de dosis solicitadas, el estado de la campaña (abierta, cerrada, bloqueada, anulada, procesada), el área de trabajo asignada, el tipo de dosis unitaria, el código de programación de producción y si la etiqueta fue confirmada. Integra las solicitudes de mezcla, las líneas de liberación, las áreas de trabajo y el cronograma de producción para ofrecer una vista unificada del ciclo de vida de cada lote farmacéutico en curso.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'VIEW', @level1name = N'ViewProductionOrder';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'VIEW', @level1name = N'ViewProductionOrder';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Expone órdenes de producción (campañas de mezcla) activas con su estado descriptivo, programación, área de trabajo, tipo de dosis y cantidad solicitada agregada, excluyendo campañas terminadas.', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'VIEW', @level1name=N'ViewProductionOrder';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Cada CampaignDetail debe tener una Campaign asociada (JOIN obligatorio).; Cada CampaignDetail debe tener un UnitDoseType válido (JOIN obligatorio).', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'VIEW', @level1name=N'ViewProductionOrder';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Las campañas en estado ''Terminada'' (6) nunca aparecen en el resultado.; El Id textual siempre coincide con CampaignDetailId convertido a string.; La cantidad agregada solo considera detalles de solicitud con CampaignDetailId no nulo.; Toda fila pertenece a una pareja Campaign/CampaignDetail existente con UnitDoseType válido.', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'VIEW', @level1name=N'ViewProductionOrder';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Campaña de mezcla; Orden de producción; Estado de campaña; Línea de producción; Dosis unitaria; Área de trabajo; Cronograma de producción; Confirmación de etiqueta; Línea de liberación', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'VIEW', @level1name=N'ViewProductionOrder';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] : Devuelve únicamente CampaignDetail cuyo CampaignStatus <> 6 (excluye ''Terminada'').; [RETURN_RESULT] : Calcula la cantidad total (Quantity) sumando RequestMixingStationDetail.Quantity agrupado por CampaignDetailId, ignorando registros con CampaignDetailId NULL.; [RETURN_RESULT] : Marca IsManagedLabel = 1 cuando cd.LabelConfirmationDate IS NOT NULL, en caso contrario 0.; [RETURN_RESULT] : Traduce CampaignStatus numérico a nombre: 1=Abierta, 2=Cerrada, 3=Bloqueada, 4=Anulada, 5=Procesada, 6=Terminada.; [RETURN_RESULT] : Construye CampaignDescription como ''Campaña # '' concatenado con CampaignNumber.', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'VIEW', @level1name=N'ViewProductionOrder';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si cd.CampaignStatus IN (1..6) → Asigna nombre legible correspondiente (Abierta/Cerrada/Bloqueada/Anulada/Procesada/Terminada) else CampaignStatusName resulta NULL; si cd.LabelConfirmationDate IS NOT NULL → IsManagedLabel = 1 (etiqueta gestionada) else IsManagedLabel = 0; si CampaignDetail tiene ReleaseLine asociada → Se exponen WorkingAreaId y WorkingAreaName del área de trabajo else WorkingArea queda NULL (LEFT JOIN); si CampaignDetail tiene ProductionScheduleDetail asociado → Se expone ProductionScheduleCode del cronograma else ProductionScheduleCode queda NULL', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'VIEW', @level1name=N'ViewProductionOrder';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'MixingStation.RequestMixingStationDetail; MixingStation.Campaign; MixingStation.CampaignDetail; MixingStation.UnitDoseType; MixingStation.ReleaseLine; MixingStation.WorkingArea; MixingStation.ProductionScheduleDetail; MixingStation.ProductionSchedule', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'VIEW', @level1name=N'ViewProductionOrder';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'VIEW', @level1name=N'ViewProductionOrder';
GO
