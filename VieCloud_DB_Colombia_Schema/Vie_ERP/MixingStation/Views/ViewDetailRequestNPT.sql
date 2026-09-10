

CREATE VIEW [MixingStation].[ViewDetailRequestNPT]
AS

with CTE_edad as (SELECT
					it.IPCODPACI,
					CASE 
						WHEN MONTH(it.IPFECNACI) < MONTH(GETDATE()) OR 
							 (MONTH(it.IPFECNACI) = MONTH(GETDATE()) AND DAY(it.IPFECNACI) <= DAY(GETDATE()))
						THEN DATEDIFF(YEAR, it.IPFECNACI, GETDATE())
						ELSE DATEDIFF(YEAR, it.IPFECNACI, GETDATE()) - 1
					END AS Edad,
					CAST(DATEDIFF(DAY, it.IPFECNACI, GETDATE()) AS INT) AS DiasDeNacido
					FROM INPACIENT it)

	SELECT DISTINCT
		  pd.Id
		, CAST(phd.GroupingCodeDose As Varchar(36)) GroupingCodeDose
		, dp.Id as RequestMixingStationDetailPatientsId
		, pac.IPNOMCOMP AS PatientName
		, pac.IPCODPACI AS PatientCode
		, ISNULL(cma.NUMCAMHOS, '') AS Bed
		, pa.PESOPACIE AS PatientWeight
		, pa.VIADMIN AS AdministrationRoute
		, CASE pa.VIADMIN WHEN 1 THEN 'Línea central' WHEN 2 THEN 'Línea periférica' ELSE '' END AS AdministrationRouteDescription
		, pa.TEMPOADMIN AS InfusionTime
		, pa.VOLUTOTAL AS TotalVolume
		, IIF(tn.NutritionVolumeAlert = 1, tn.MinimumValue, NULL ) MinValueVolume
		, IIF(tn.NutritionVolumeAlert = 1, tn.MaximumValue, NULL ) MaxValueVolume
		, pa.VELINFUSION AS InfusionVelocity
		, pa.AGUAESTERIL AS Water
		, pa.AGUAESTERIL AS WaterOsmolarity
		, pd.NAMENUT AS NutritionName
		, TRY_CAST(ISNULL(pd.APORTENUT, 0) AS DECIMAL(18,2)) AS Dose
		, ISNULL(pd.VOLUMENCAL, 0) AS Volume
		, TRY_CAST(CASE 
				WHEN cte.Edad = 0 THEN 
					CASE 
						WHEN cte.DiasDeNacido BETWEEN 0 AND 28 
							THEN REPLACE(JSON_VALUE(hcp.NewbornNutrientAlert, '$.ValorMinimo'), ',', '.') -- Neonato
						ELSE REPLACE(JSON_VALUE(hcp.BreastfeedingNutrientAlert, '$.ValorMinimo'), ',', '.') -- Lactante
					END
				WHEN cte.Edad BETWEEN 1 AND 17 
					THEN REPLACE(JSON_VALUE(hcp.PediatricNutrientAlert, '$.ValorMinimo'), ',', '.') -- Pediátrico
				ELSE REPLACE(JSON_VALUE(hcp.AdultNutrientAlert, '$.ValorMinimo'), ',', '.') -- Adulto
			END AS DECIMAL(7, 2)) AS MinValueNutrition
		, TRY_CAST(CASE 
				WHEN cte.Edad = 0 THEN 
					CASE 
						WHEN cte.DiasDeNacido BETWEEN 0 AND 28 
							THEN REPLACE(JSON_VALUE(hcp.NewbornNutrientAlert, '$.ValorMaximo'), ',', '.') -- Neonato
						ELSE REPLACE(JSON_VALUE(hcp.BreastfeedingNutrientAlert, '$.ValorMaximo'), ',', '.') -- Lactante
					END
				WHEN cte.Edad BETWEEN 1 AND 17 
					THEN REPLACE(JSON_VALUE(hcp.PediatricNutrientAlert, '$.ValorMaximo'), ',', '.') -- Pediátrico
				ELSE REPLACE(JSON_VALUE(hcp.AdultNutrientAlert, '$.ValorMaximo'), ',', '.') -- Adulto
			END AS DECIMAL(7, 2)) AS MaxValueNutrition
		, ISNULL(a.Osmolarity, 0) AS Osmolarity
		, pd.CODPRODUC AS ProductCode
		, pd.ID AS NutritionId
		, dp.CampaignDetailId
		, dp.RequestMixingStationDetailId
		, CONCAT(ps.FunctionalUnitCode, ' - ', fu.Name) AS FunctionalUnitCodeName
		, tn.NAME TypeNutrition
		, rpds.BatchCode NumberLotAdequacy
		, pa.Justificacion JustificationPrescription
		, pd.APORTENUT as Request
	FROM MedicalHistory.ProductSusceptibleMixingStation ps
	JOIN MedicalHistory.PharmaDose phd ON ps.CodeSusceptibleMixingStation = phd.CodeSusceptibleMixingStation
	JOIN HCNUTPAREC pa ON ps.IdOrigin = pa.ID
	JOIN HCPARNUTC tn ON pa.IDHCPARNUTC = tn.ID
	JOIN HCNUTPAREND pd ON pd.IDHCNUTPAREC = pa.ID
	JOIN INPACIENT pac ON pa.IPCODPACI = pac.IPCODPACI
	JOIN CTE_edad cte on cte.IPCODPACI = pac.IPCODPACI
	JOIN Inventory.ATC a ON pd.CODPRODUC = a.Code
	JOIN Payroll.FunctionalUnit fu ON ps.FunctionalUnitCode = fu.Code
	JOIN HCPARNUTD hcp on hcp.HCPARNUTCID = tn.ID AND pd.IDHCPARNUTD = hcp.ID
	LEFT JOIN MixingStation.RequestMixingStationDetailPatients dp ON dp.EntityId = ps.Id
	LEFT JOIN MixingStation.RequestPackageDetailStatus rpds ON rpds.RequestMixingStationDetailId = dp.RequestMixingStationDetailId 
	LEFT JOIN ADINGRESO ing ON pa.NUMINGRES = ing.NUMINGRES
	LEFT JOIN CHCAMASHO cma WITH(NOLOCK) ON ISNULL(CASE WHEN ing.CODCAMACT = 0 THEN '' ELSE Cast(ing.CODCAMACT As VARCHAR(15)) END, '') = cma.CODICAMAS
	LEFT JOIN MixingStation.ConfirmationUnitDose cud WITH(NOLOCK) ON phd.GroupingCodeDose = cud.GroupingCodeDose 
	WHERE cud.RequestMixingStationDetailId IS NULL OR (dp.EntityName = 'ProductSusceptibleMixingStation' And dp.[Status] <> 3 and rpds.Status NOT IN (5,6))
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Vista que consolida el detalle de solicitudes de Nutrición Parenteral Total (NPT) pendientes o en proceso para la estación de mezclas. Integra la prescripción nutricional del paciente (pauta, componentes, volúmenes, vía de administración, velocidad de infusión) con los datos demográficos del paciente (nombre, cédula, edad calculada en años y días, cama hospitalaria, peso), los rangos de alerta por nutriente según grupo etario (neonato, lactante, pediátrico o adulto), la osmolaridad del producto ATC, la unidad funcional responsable y el estado del lote de adecuación. Sirve como fuente principal para el módulo de estación de mezclas, permitiendo visualizar qué solicitudes de NPT aún no han sido confirmadas o procesadas completamente, filtrando aquellas ya despachadas o anuladas. Es utilizada para reportería operativa de farmacia clínica, preparación de mezclas intravenosas y trazabilidad de nutrición parenteral por paciente.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'VIEW', @level1name = N'ViewDetailRequestNPT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'VIEW', @level1name = N'ViewDetailRequestNPT';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Expone el detalle de solicitudes de Nutrición Parenteral Total (NPT) susceptibles de mezcla, consolidando datos del paciente, prescripción, nutrientes, ubicación, lote y rangos de alerta según grupo etario.', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'VIEW', @level1name=N'ViewDetailRequestNPT';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El producto debe estar registrado como susceptible de mezcla en MedicalHistory.ProductSusceptibleMixingStation con un IdOrigin que corresponda a una prescripción nutricional en HCNUTPAREC.; Debe existir relación entre PharmaDose y ProductSusceptibleMixingStation por CodeSusceptibleMixingStation.; El paciente referenciado en la prescripción nutricional (IPCODPACI) debe existir en INPACIENT.; El producto del detalle nutricional (CODPRODUC) debe existir en Inventory.ATC.; La unidad funcional asociada al producto susceptible debe existir en Payroll.FunctionalUnit.; Debe existir el parámetro nutricional detalle (HCPARNUTD) que vincula el tipo de nutriente (HCPARNUTC) con el detalle de la prescripción.', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'VIEW', @level1name=N'ViewDetailRequestNPT';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'La clasificación etaria es excluyente: Neonato (0-28 días), Lactante (>28 días y <1 año), Pediátrico (1-17 años), Adulto (>=18 años).; Los valores numéricos JSON de alerta nutricional se normalizan reemplazando coma decimal por punto antes de castear a DECIMAL(7,2).; Dose y Volume nunca son NULL: se aplica ISNULL con 0 por defecto.; Los registros con paquete en estado 5 o 6 (rpds.Status) o con paciente en estado 3 (dp.Status) son excluidos cuando ya existe confirmación de dosis.; Sólo se consideran productos cuyo origen (IdOrigin) corresponda a una receta nutricional (HCNUTPAREC), garantizando que la vista representa exclusivamente NPT.; La descripción de la vía de administración sólo soporta dos códigos válidos (1 y 2); cualquier otro valor produce cadena vacía.', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'VIEW', @level1name=N'ViewDetailRequestNPT';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Nutrición Parenteral Total (NPT); Prescripción nutricional; Estación de mezclas (Mixing Station); Vía de administración (línea central/periférica); Clasificación etaria (Neonato, Lactante, Pediátrico, Adulto); Alertas de nutrientes por grupo etario; Osmolaridad; Lote de adecuación (BatchCode); Unidad funcional; Cama hospitalaria; Peso del paciente; Volumen de infusión; Tiempo y velocidad de infusión; Dosis unitaria', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'VIEW', @level1name=N'ViewDetailRequestNPT';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] Resultset: Devuelve sólo registros donde no existe ConfirmationUnitDose para el GroupingCodeDose, o bien el detalle del paciente está vinculado a ''ProductSusceptibleMixingStation'' con Status<>3 y el estado del paquete (rpds.Status) no esté en (5,6).', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'VIEW', @level1name=N'ViewDetailRequestNPT';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si MES de IPFECNACI < MES actual, o (mismo mes y DÍA de nacimiento <= día actual) → Edad = DATEDIFF(YEAR, fecha_nac, hoy) else Edad = DATEDIFF(YEAR, fecha_nac, hoy) - 1 (aún no ha cumplido años en el año actual); si cte.Edad = 0 AND DiasDeNacido BETWEEN 0 AND 28 → Usa rangos de alerta de NewbornNutrientAlert (Neonato) else Si Edad=0 pero DiasDeNacido>28, usa BreastfeedingNutrientAlert (Lactante); si cte.Edad BETWEEN 1 AND 17 → Usa rangos de alerta PediatricNutrientAlert (Pediátrico) else Si Edad >= 18, usa AdultNutrientAlert (Adulto); si tn.NutritionVolumeAlert = 1 → Expone MinValueVolume y MaxValueVolume desde tn.MinimumValue/MaximumValue else MinValueVolume y MaxValueVolume = NULL; si pa.VIADMIN = 1 → AdministrationRouteDescription = ''Línea central'' else Si VIADMIN=2 → ''Línea periférica''; en otro caso cadena vacía; si ing.CODCAMACT = 0 o NULL → Bed se busca con cadena vacía (no se enlaza cama) else Bed se obtiene de CHCAMASHO via CODICAMAS = CODCAMACT casteado a varchar; si cud.RequestMixingStationDetailId IS NULL → Incluye el registro (no hay confirmación de dosis previa) else Sólo lo incluye si dp.EntityName=''ProductSusceptibleMixingStation'' AND dp.Status<>3 AND rpds.Status NOT IN (5,6)', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'VIEW', @level1name=N'ViewDetailRequestNPT';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'INPACIENT; MedicalHistory.ProductSusceptibleMixingStation; MedicalHistory.PharmaDose; HCNUTPAREC; HCPARNUTC; HCNUTPAREND; Inventory.ATC; Payroll.FunctionalUnit; HCPARNUTD; MixingStation.RequestMixingStationDetailPatients; MixingStation.RequestPackageDetailStatus; ADINGRESO; CHCAMASHO; MixingStation.ConfirmationUnitDose', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'VIEW', @level1name=N'ViewDetailRequestNPT';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'VIEW', @level1name=N'ViewDetailRequestNPT';
GO
