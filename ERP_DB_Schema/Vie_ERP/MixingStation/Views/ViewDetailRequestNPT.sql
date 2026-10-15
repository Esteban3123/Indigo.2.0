












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
	LEFT JOIN CHCAMASHO cma ON ISNULL(CASE WHEN ing.CODCAMACT = 0 THEN '' ELSE Cast(ing.CODCAMACT As VARCHAR(15)) END, '') = cma.CODICAMAS
	LEFT JOIN MixingStation.ConfirmationUnitDose cud ON phd.GroupingCodeDose = cud.GroupingCodeDose 
	WHERE cud.RequestMixingStationDetailId IS NULL OR (dp.EntityName = 'ProductSusceptibleMixingStation' And dp.[Status] <> 3 and rpds.Status NOT IN (5,6))