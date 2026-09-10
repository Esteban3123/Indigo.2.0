

CREATE VIEW [MixingStation].[ViewMainInformationParenteralNutritionLabel]
AS
WITH Cte_ParenteralNutritionParameters AS (
	SELECT IDHCNUTPAREC,
		[2]  AS TotalCalories, -- [Calorías totales kg/día],
		[11] AS Carbohydrates, --[Carbohidratos (%)],
		[12] AS ProteinConcentration, --[Concentración de proteína (%)],
		[13] AS LipidConcentration, --[Concentración de lípidos (%)],
		[14] AS Osmolarity --[Osmolaridad (mOsm/L)]
	FROM (
		SELECT 
			d.IDHCNUTPAREC,
			f.CODNAME,
			d.RESULT
		FROM HCNUTPAREFD d
		JOIN  HCPARNUTFORD f ON d.IDHCPARNUTFORD = f.ID
		WHERE TRY_CAST(f.CODNAME AS INT) IN (2, 11, 12, 13, 14)
	) AS SourceTable 
	PIVOT (
		MAX(RESULT)
		FOR CODNAME IN ([2], [11], [12], [13], [14])
	) AS tp_ParenteralNutritionParameters
)
SELECT 
	npc.Id  AS Id,
	cd.Id CampaignDetailId,
	RTRIM(ie.INDNOMEMP) AS Institution,
	RTRIM(ca.NOMCENATE) AS CareCenter,
	RTRIM(fu.Name) AS FunctionalUnit,
	cd.ProcessingDate,
	rmsdp.Bed AS PatientBed ,
	pac.IPNOMCOMP AS PatientName,
	pac.IPCODPACI AS PatientIdentification,
	pac.IPCODPACI AS HistoryNumber, --Numero de historia
	npc.PESOPACIE AS PatientWeight,  --En kg
	dbo.Edad(pac.IPFECNACI, Common.GETDATE()) AS PatientAge,
	-----
	ISNULL(cte_pnp.TotalCalories, 0) AS TotalCalories,
	ISNULL(cte_pnp.Carbohydrates, 0) AS Carbohydrates,
	ISNULL(cte_pnp.LipidConcentration, 0) AS LipidConcentration,
	ISNULL(cte_pnp.ProteinConcentration, 0) AS ProteinConcentration,
	ISNULL(cte_pnp.Osmolarity, 0) AS Osmolarity,
	-----
	npc.VOLUTOTAL AS TotalVolume, --Volumen total
	SUM(ViewDetailsTmp.Volume) AS SterileWater,--Agua esteril
	CASE npc.VIADMIN 
		WHEN 1 THEN 'Línea central' 
		WHEN 2 THEN 'Línea periférica' 
		ELSE '' 
	END AdministrationRouteDescription,
	npc.VELINFUSION AS InfusionRate, --velocidad de infusión
	npc.TEMPOADMIN AS InfusionTime,
	cduPC.Nombre AS ProductionChemist, --Elaborado por
	cduQF.Nombre AS Supervisor, --verificado por
	rpds.BatchExpirationDate AS Stability, --Estabilidad
	st.Description AS StorageConditions,--Almacenamiento
	ps.NOMMEDICO AS NutritionistName, --Nutricionista
	rpds.BatchCode AS InternalBatchCode--Lote
FROM MixingStation.CampaignDetail cd
JOIN MixingStation.RequestMixingStationDetail rmsd ON cd.Id = rmsd.CampaignDetailId
JOIN MixingStation.RequestMixingStationDetailPatients rmsdp ON rmsd.Id = rmsdp.RequestMixingStationDetailId
JOIN MedicalHistory.ProductSusceptibleMixingStation psms ON rmsdp.EntityId = psms.Id AND psms.Origin = 'HCNUTPAREC' 
JOIN HCNUTPAREC npc ON psms.IdOrigin = npc.ID
JOIN MixingStation.PackagePersonalized pp ON rmsd.PackagePersonalizedId = pp.Id
JOIN INPACIENT pac ON rmsdp.PatientCode = pac.IPCODPACI
JOIN INPROFSAL ps ON npc.CODPROSAL = ps.CODPROSAL
JOIN ADCENATEN ca ON psms.CenterAttentionCode = ca.CODCENATE
JOIN Payroll.FunctionalUnit fu ON psms.FunctionalUnitCode = fu.Code
CROSS JOIN (SELECT TOP 1 INDNOMEMP FROM INEMPRESU) ie
JOIN MixingStation.RequestPackageDetailStatus rpds ON rmsdp.RequestMixingStationDetailId = rpds.RequestMixingStationDetailId
LEFT JOIN Inventory.StorageTemperature st ON pp.Storage = st.Id
LEFT JOIN Cte_ParenteralNutritionParameters cte_pnp ON npc.Id = cte_pnp.IDHCNUTPAREC
LEFT JOIN MixingStation.ViewDetailsParenteralNutritionLabel ViewDetailsTmp ON ViewDetailsTmp.ParenteralNutritionId = npc.ID AND ViewDetailsTmp.AbbreviationAtc = 'AGUA ESTERIL'
--Químicos 
LEFT JOIN [MixingStation].[ViewUsersRolsCM] cduPC ON cd.Id = cduPC.CampaignDetailId AND cduPC.UserRole = 2 --QF Producción 
LEFT JOIN [MixingStation].[ViewUsersRolsCM] cduQF ON cd.Id = cduQF.CampaignDetailId AND cduQF.UserRole = 1 --QF Calidad
GROUP BY 
		npc.ID,
		cd.Id,
		ie.INDNOMEMP,
		ca.NOMCENATE,
		fu.Name,
		cd.ProcessingDate,
		rmsdp.Bed,
		pac.IPNOMCOMP,
		pac.IPCODPACI,
		npc.PESOPACIE,
		pac.IPFECNACI,
		cte_pnp.TotalCalories,
		cte_pnp.Carbohydrates,
		cte_pnp.LipidConcentration,
		cte_pnp.ProteinConcentration,
		cte_pnp.Osmolarity,
		npc.VOLUTOTAL,
		npc.VIADMIN,
		npc.VELINFUSION,
		npc.TEMPOADMIN,
		cduPC.Nombre,
		cduQF.Nombre,
		rpds.BatchExpirationDate,
		st.Description,
		ps.NOMMEDICO,
		rpds.BatchCode
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Vista que consolida la información principal para la etiqueta de nutrición parenteral preparada en la estación de mezclas farmacéuticas. Integra datos del paciente (nombre, cédula/identificación, cama, peso, edad), la institución y centro de atención, la unidad funcional, y los parámetros clínicos calculados de la evaluación nutricional: calorías totales, carbohidratos, concentración de proteínas, concentración de lípidos y osmolaridad. Combina además el detalle de la campaña de preparación (lote interno, fecha de procesamiento, estabilidad/vencimiento del lote, condiciones de almacenamiento, velocidad y tiempo de infusión, vía de administración central o periférica, volumen total y agua estéril), junto con el nombre del químico farmacéutico elaborador, el supervisor de calidad y el nombre del nutricionista responsable. Sirve como fuente de datos para imprimir o generar la etiqueta oficial de bolsas de nutrición parenteral individualizada por paciente.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'VIEW', @level1name = N'ViewMainInformationParenteralNutritionLabel';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'VIEW', @level1name = N'ViewMainInformationParenteralNutritionLabel';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Consolida la información requerida para imprimir la etiqueta de una bolsa de nutrición parenteral preparada en la estación de mezclas, integrando datos del paciente, parámetros nutricionales, responsables, lote y condiciones de almacenamiento.', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'VIEW', @level1name=N'ViewMainInformationParenteralNutritionLabel';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Existen registros en MedicalHistory.ProductSusceptibleMixingStation con Origin=''HCNUTPAREC'' enlazados a la campaña; La tabla INEMPRESU contiene al menos un registro con el nombre de la empresa; Los parámetros nutricionales en HCPARNUTFORD usan CODNAME numéricos convertibles a entero (códigos 2, 11, 12, 13, 14); Existe relación entre el paciente del detalle de mezcla (PatientCode) y la tabla INPACIENT por IPCODPACI', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'VIEW', @level1name=N'ViewMainInformationParenteralNutritionLabel';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Sólo se incluyen mezclas cuyo origen en ProductSusceptibleMixingStation es ''HCNUTPAREC'' (nutrición parenteral); Los parámetros nutricionales (calorías, carbohidratos, lípidos, proteína, osmolaridad) nunca son NULL: si no hay valor se sustituyen por 0 vía ISNULL; La institución mostrada proviene siempre del primer registro (TOP 1) de INEMPRESU; La estabilidad de la mezcla se reporta como la fecha de vencimiento del lote (BatchExpirationDate) y el lote interno como BatchCode de RequestPackageDetailStatus; La edad del paciente se calcula con dbo.Edad sobre la fecha actual (Common.GETDATE()) y la fecha de nacimiento IPFECNACI; El número de historia y la identificación del paciente coinciden (ambos toman IPCODPACI); La vía de administración se traduce a texto: 1=''Línea central'', 2=''Línea periférica'', otro=''''', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'VIEW', @level1name=N'ViewMainInformationParenteralNutritionLabel';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Nutrición parenteral; Etiqueta de mezcla; Calorías totales; Carbohidratos; Concentración de proteína; Concentración de lípidos; Osmolaridad; Volumen total; Agua estéril; Vía de administración (línea central/periférica); Velocidad de infusión; Tiempo de infusión; Químico farmacéutico de producción; Director técnico; Nutricionista; Lote interno; Estabilidad; Condiciones de almacenamiento; Paciente / cama / unidad funcional; Centro de atención', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'VIEW', @level1name=N'ViewMainInformationParenteralNutritionLabel';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] MixingStation.ViewMainInformationParenteralNutritionLabel: Devuelve una fila por paciente/bolsa de nutrición parenteral incluida en una campaña de mezcla, sólo cuando el producto susceptible tiene Origin=''HCNUTPAREC''.', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'VIEW', @level1name=N'ViewMainInformationParenteralNutritionLabel';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si npc.VIADMIN = 1 → AdministrationRouteDescription = ''Línea central'' else Si VIADMIN=2 → ''Línea periférica''; cualquier otro valor → cadena vacía; si f.CODNAME (convertido a INT) IN (2,11,12,13,14) → Se pivotean los resultados como TotalCalories, Carbohydrates, ProteinConcentration, LipidConcentration y Osmolarity respectivamente else El resto de parámetros nutricionales se descartan; si cduPC.UserRole = 2 → El usuario se reporta como ''ProductionChemist'' (QF de Producción / Elaborado por)', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'VIEW', @level1name=N'ViewMainInformationParenteralNutritionLabel';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'HCNUTPAREFD; HCPARNUTFORD; MixingStation.CampaignDetail; MixingStation.RequestMixingStationDetail; MixingStation.RequestMixingStationDetailPatients; MedicalHistory.ProductSusceptibleMixingStation; HCNUTPAREC; MixingStation.PackagePersonalized; INPACIENT; INPROFSAL; ADCENATEN; Payroll.FunctionalUnit; INEMPRESU; MixingStation.RequestPackageDetailStatus; Inventory.StorageTemperature; MixingStation.ViewUsersRolsCM; MixingStation.ViewTechnicalDirectorByCampaign', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'VIEW', @level1name=N'ViewMainInformationParenteralNutritionLabel';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'VIEW', @level1name=N'ViewMainInformationParenteralNutritionLabel';
GO
