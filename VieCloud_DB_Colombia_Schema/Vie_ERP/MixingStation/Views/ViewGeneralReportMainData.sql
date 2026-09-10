CREATE VIEW [MixingStation].[ViewGeneralReportMainData] 
AS
	WITH CampaignQuantityTemp AS
	(
		SELECT data.CampaignDetailId, SUM(data.Quantity) Quantity
		FROM
		(
			SELECT rd.CampaignDetailId, SUM(rd.Quantity) Quantity
			FROM MixingStation.RequestMixingStationDetail rd WITH (NOLOCK)
			GROUP BY rd.CampaignDetailId
		) data 
		GROUP BY data.CampaignDetailId
	)

	SELECT 
		CONCAT(c.Id, ' - ', cd.Id ) KeyView,
		cd.Id CampaignDetailId,
		cc.Name MixingStationName,
		cd.CampaignNumber,
		CONCAT (ud.Code, ' - ', ud.Description) UnitDoseType,
		CONCAT (pl.Code, ' - ', pl.Name) ProductionLine,
		wa.Description WorkArea,
		'Terminada' CampaignStatus,
		cd.ProcessingDate,
		cd.FinishDate,
		CONCAT(
			IIF(DATEDIFF(DAY, cd.ProcessingDate, cd.FinishDate) > 0, 
				CONCAT(DATEDIFF(DAY, cd.ProcessingDate, cd.FinishDate), ' días con '), ''
			), 
			CONVERT(varchar, DATEADD(second, 
					DATEDIFF(SECOND, cd.ProcessingDate, cd.FinishDate), 0), 108)
		) Duration,
		cqt.Quantity,
		cduTD.Usuario TechnicalDirector,
		cduQC.Usuario QualityChemical,
		cduPC.Usuario ProductionChemical,
		ISNULL(cduPA.Usuario, '') ProductionAssistant
	FROM MixingStation.CMConfiguration cc
	JOIN MixingStation.Campaign c (NOLOCK) ON cc.Id = c.CMConfigurationId
	JOIN MixingStation.CampaignDetail cd (NOLOCK) ON c.Id = cd.CampaignId
	JOIN MixingStation.UnitDoseType ud (NOLOCK) ON cd.UnitDoseTypeId = ud.Id
	JOIN MixingStation.ProductionLine pl (NOLOCK) ON cd.ProductionLineId = pl.Id
	JOIN MixingStation.ReleaseLine rl (NOLOCK) ON cd.Id = rl.CampaignDetailId
	JOIN MixingStation.WorkingArea wa (NOLOCK) ON rl.WorkingAreaId = wa.Id
	JOIN CampaignQuantityTemp cqt (NOLOCK) ON cd.Id = cqt.CampaignDetailId
	LEFT JOIN [MixingStation].[ViewUsersRolsCM] cduQC (NOLOCK) ON cd.Id = cduQC.CampaignDetailId AND cduQC.UserRole = 1 --QF Calidad
	LEFT JOIN [MixingStation].[ViewUsersRolsCM] cduPC (NOLOCK) ON cd.Id = cduPC.CampaignDetailId AND cduPC.UserRole = 2 --QF Producción 
	LEFT JOIN [MixingStation].[ViewUsersRolsCM] cduPA (NOLOCK) ON cd.Id = cduPA.CampaignDetailId AND cduPA.UserRole = 3 --Auxiliar de Central de Mezclas
	LEFT JOIN [MixingStation].[ViewUsersRolsCM] cduTD (NOLOCK) ON cd.Id = cduTD.CampaignDetailId AND cduTD.UserRole = 4 --Director técnico
	WHERE cd.CampaignStatus = 6
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Vista que consolida los datos principales del reporte general de campañas farmacéuticas finalizadas en la estación de mezclas (central de mezclas). Integra información de la configuración de la estación, el número de campaña, el tipo de dosis unitaria, la línea de producción, el área de trabajo y la cantidad total de unidades preparadas por lote. Calcula la duración del proceso (en días y horas) entre la fecha de inicio y la fecha de finalización de cada campaña, y complementa el reporte con los usuarios responsables por rol: Director Técnico, Químico Farmacéutico de Calidad, Químico Farmacéutico de Producción y Auxiliar de Central de Mezclas. Sirve como fuente principal para reportes de trazabilidad y control de producción farmacéutica, filtrando únicamente las campañas en estado ''Terminada''.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'VIEW', @level1name = N'ViewGeneralReportMainData';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'VIEW', @level1name = N'ViewGeneralReportMainData';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Consolida la información principal de campañas de mezcla finalizadas para alimentar el reporte general, incluyendo duración, cantidad producida y usuarios responsables por rol.', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'VIEW', @level1name=N'ViewGeneralReportMainData';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'La campaña debe estar en estado finalizado (CampaignStatus = 6).; El detalle de campaña debe tener al menos una línea de liberación (ReleaseLine) asociada.; Debe existir al menos un detalle en RequestMixingStationDetail para el CampaignDetailId, ya que CampaignQuantityTemp se une con JOIN interno.; Las relaciones a CMConfiguration, Campaign, UnitDoseType y ProductionLine deben existir.', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'VIEW', @level1name=N'ViewGeneralReportMainData';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se exponen campañas terminadas (CampaignStatus = 6).; La cantidad reportada (Quantity) es la suma agregada de todas las cantidades de RequestMixingStationDetail por CampaignDetailId.; La clave de vista (KeyView) se compone como ''CampaignId - CampaignDetailId''.; Los códigos de UnitDoseType y ProductionLine se muestran concatenados como ''Code - Descripción/Nombre''.; Los roles de usuario están codificados: 1=QF Calidad, 2=QF Producción, 3=Auxiliar Central de Mezclas, 4=Director Técnico.; Si no hay Auxiliar de Central de Mezclas asignado, se muestra cadena vacía en lugar de NULL; los demás roles pueden quedar NULL.', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'VIEW', @level1name=N'ViewGeneralReportMainData';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Campaña de mezcla; Central de mezclas; Dosis unitaria; Línea de producción; Área de trabajo; Línea de liberación; Director técnico; Químico farmacéutico de calidad; Químico farmacéutico de producción; Auxiliar de central de mezclas; Estado de campaña terminada', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'VIEW', @level1name=N'ViewGeneralReportMainData';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] MixingStation.ViewGeneralReportMainData: Devuelve solo campañas con CampaignStatus = 6, etiquetadas literalmente como ''Terminada''.', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'VIEW', @level1name=N'ViewGeneralReportMainData';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si DATEDIFF(DAY, ProcessingDate, FinishDate) > 0 → La duración se prefija con ''N días con '' antes del componente horario. else Solo se muestra el componente horario (HH:MM:SS) sin prefijo de días.; si ViewUsersRolsCM.UserRole = 1 → Se asigna como Químico de Calidad (QualityChemical).; si ViewUsersRolsCM.UserRole = 2 → Se asigna como Químico de Producción (ProductionChemical).; si ViewUsersRolsCM.UserRole = 3 → Se asigna como Auxiliar de Central de Mezclas (ProductionAssistant); si no existe se devuelve cadena vacía.; si ViewUsersRolsCM.UserRole = 4 → Se asigna como Director Técnico (TechnicalDirector).', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'VIEW', @level1name=N'ViewGeneralReportMainData';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'MixingStation.RequestMixingStationDetail; MixingStation.CMConfiguration; MixingStation.Campaign; MixingStation.CampaignDetail; MixingStation.UnitDoseType; MixingStation.ProductionLine; MixingStation.ReleaseLine; MixingStation.WorkingArea; MixingStation.ViewUsersRolsCM', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'VIEW', @level1name=N'ViewGeneralReportMainData';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'VIEW', @level1name=N'ViewGeneralReportMainData';
GO
