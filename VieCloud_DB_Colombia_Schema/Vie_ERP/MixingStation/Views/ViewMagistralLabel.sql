CREATE VIEW [MixingStation].[ViewMagistralLabel]
AS
WITH CTE_PharmaInfo AS (
    SELECT	pd.GroupingCodeDose,
			ar.DESVIAADM AS AdministrationName,
			h.ADMMEZLIQ	AS Indications
	FROM MedicalHistory.PharmaDose pd WITH (NOLOCK)
    JOIN MedicalHistory.ProductSusceptibleMixingStation psms WITH (NOLOCK) ON pd.CodeSusceptibleMixingStation = psms.CodeSusceptibleMixingStation AND psms.Origin = 'HCINFLIQA'
	JOIN HCINFLIQA h ON psms.IdOrigin = h.CONSECUTI
	LEFT JOIN dbo.HCINFLIQD  hc on hc.CODCONCEC = H.CODCONCEC
	LEFT JOIN HCVIAADMI ar on ar.CODVIAADM = hc.VIAADMDIL
	GROUP BY pd.GroupingCodeDose, ar.DESVIAADM, h.ADMMEZLIQ
)
SELECT	
		rpds.Id Id,
		rpds.Id RequestPackageDetailStatusId,
		cd.Id CampaignDetailId,
		ISNULL(pp.Id, p.Id) AS PackageId,
		p.[Name] PackageName,
		pac.IPNOMCOMP PatientName,
		rmsdp.PatientCode,
		rmsdp.Bed,
		pin.Indications,
		cd.ProcessingDate,
		rpds.BatchExpirationDate AS ExpirationDate,
		rpds.BatchCode AS InternalBatchCode,
		pin.AdministrationName AS AdministrationRoute,
		st.Description AS StorageConditions,
		cduPC.Nombre AS ProductionChemist,
		cduQC.Nombre AS Supervisor,
		'' AS Content,
		ISNULL(pp.SpecialConsiderations, p.SpecialConsiderations) AS SpecialConsiderations
FROM MixingStation.CampaignDetail cd WITH(NOLOCK)
JOIN MixingStation.RequestMixingStationDetail rmsd WITH(NOLOCK) ON cd.Id = rmsd.CampaignDetailId
JOIN MixingStation.RequestPackageDetailStatus rpds WITH(NOLOCK) ON rmsd.Id = rpds.RequestMixingStationDetailId
JOIN MixingStation.RequestMixingStationDetailPatients rmsdp WITH(NOLOCK) ON rmsd.Id = rmsdp.RequestMixingStationDetailId
JOIN MixingStation.Package p WITH(NOLOCK) ON p.Id = rmsd.PackageId
JOIN INPACIENT pac WITH(NOLOCK) ON rmsdp.PatientCode = pac.IPCODPACI
JOIN MixingStation.UnitDoseType udt WITH(NOLOCK) ON cd.UnitDoseTypeId = udt.Id
JOIN CTE_PharmaInfo pin ON rpds.GroupingCodeDose = pin.GroupingCodeDose 
LEFT JOIN MixingStation.PackagePersonalized pp WITH(NOLOCK) ON rmsd.PackagePersonalizedId = pp.Id
LEFT JOIN Inventory.StorageTemperature st WITH(NOLOCK) ON ISNULL(pp.Storage, p.Storage) = st.Id
LEFT JOIN [MixingStation].[ViewUsersRolsCM] cduPC ON cd.Id = cduPC.CampaignDetailId AND cduPC.UserRole = 2 --QF Producción 
LEFT JOIN [MixingStation].[ViewUsersRolsCM] cduQC ON cd.Id = cduQC.CampaignDetailId AND cduQC.UserRole = 1 --QF Calidad 
WHERE udt.MSClass = 9 
		AND rmsd.Source = 1 -- 1. Orden Médica  2. Solicitud Externa Paciente  3. Solicitud Externa Maquila  4. Solicitud Inventario
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Vista que genera la información completa para imprimir etiquetas de preparaciones magistrales (tipo dosis unitaria, clase MSClass=9) producidas en la estación de mezclas, exclusivamente a partir de órdenes médicas. Integra datos del paciente (nombre, cédula, cama), del paquete o envase del preparado (nombre, consideraciones especiales, condiciones de almacenamiento/temperatura), del lote de producción (campaña: fecha de procesamiento, código de lote interno, fecha de vencimiento), de la dosis farmacéutica (vía de administración, indicaciones de mezcla de líquidos obtenidas desde HCINFLIQA y HCINFLIQD), y de los profesionales responsables (químico farmacéutico de producción y supervisor de calidad). Sirve como fuente directa para el módulo de impresión de etiquetas magistrales, consolidando en un solo resultado toda la información regulatoria y clínica que debe aparecer en el rótulo del preparado entregado al paciente.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'VIEW', @level1name = N'ViewMagistralLabel';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'VIEW', @level1name = N'ViewMagistralLabel';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Consolida la información necesaria para imprimir el rótulo (label) de preparaciones magistrales elaboradas en la estación de mezclas a partir de órdenes médicas, integrando datos del paciente, paquete, lote, condiciones de almacenamiento e indicaciones de administración.', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'VIEW', @level1name=N'ViewMagistralLabel';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El tipo de dosis unitaria (UnitDoseType.MSClass) debe ser 9 (clase magistral).; La solicitud de mezcla (RequestMixingStationDetail.Source) debe ser 1 = Orden Médica.; Debe existir información farmacéutica en HCINFLIQA con origen ''HCINFLIQA'' en ProductSusceptibleMixingStation, ligada vía GroupingCodeDose con la dosis (PharmaDose).; Debe existir paciente válido en INPACIENT correspondiente al PatientCode de RequestMixingStationDetailPatients.', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'VIEW', @level1name=N'ViewMagistralLabel';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se exponen preparaciones de clase magistral (MSClass=9) originadas en orden médica (Source=1).; La información farmacéutica de indicaciones y vía de administración proviene exclusivamente de HCINFLIQA/HCINFLIQD/HCVIAADMI vinculadas mediante GroupingCodeDose.; Cuando existe paquete personalizado, sus atributos prevalecen sobre los del paquete estándar (ISNULL(pp.*, p.*)).; El rol 2 corresponde a Químico Farmacéutico de Producción y el rol 1 a Químico Farmacéutico de Calidad.; El campo Content siempre se devuelve como cadena vacía.', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'VIEW', @level1name=N'ViewMagistralLabel';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Preparación magistral; Estación de mezclas (farmacia); Orden médica; Paciente hospitalizado (cama); Lote y fecha de vencimiento; Vía de administración; Condiciones de almacenamiento; Químico farmacéutico de producción; Químico farmacéutico de calidad; Paquete personalizado; Dosis unitaria; Rótulo/etiqueta de preparación', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'VIEW', @level1name=N'ViewMagistralLabel';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] MixingStation.ViewMagistralLabel: Devuelve una fila por RequestPackageDetailStatus de mezclas magistrales (MSClass=9) originadas en orden médica (Source=1), con datos consolidados del paciente, paquete, lote, ruta de administración, condiciones de almacenamiento y químicos (Producción y Calidad).', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'VIEW', @level1name=N'ViewMagistralLabel';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si PackagePersonalized (pp) existe para la solicitud → Se usa pp.Id como PackageId y pp.SpecialConsiderations / pp.Storage como datos del paquete else Se usan los valores del Package estándar (p.Id, p.SpecialConsiderations, p.Storage); si ViewUsersRolsCM con UserRole = 2 → Se asigna como ProductionChemist (QF Producción); si ViewUsersRolsCM con UserRole = 1 → Se asigna como Supervisor (QF Calidad); si udt.MSClass = 9 AND rmsd.Source = 1 → La fila se incluye en el rótulo magistral else Se excluye del resultado', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'VIEW', @level1name=N'ViewMagistralLabel';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'MedicalHistory.PharmaDose; MedicalHistory.ProductSusceptibleMixingStation; dbo.HCINFLIQA; dbo.HCINFLIQD; dbo.HCVIAADMI; MixingStation.CampaignDetail; MixingStation.RequestMixingStationDetail; MixingStation.RequestPackageDetailStatus; MixingStation.RequestMixingStationDetailPatients; MixingStation.Package; dbo.INPACIENT; MixingStation.UnitDoseType; MixingStation.PackagePersonalized; Inventory.StorageTemperature; MixingStation.ViewUsersRolsCM', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'VIEW', @level1name=N'ViewMagistralLabel';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'VIEW', @level1name=N'ViewMagistralLabel';
GO
