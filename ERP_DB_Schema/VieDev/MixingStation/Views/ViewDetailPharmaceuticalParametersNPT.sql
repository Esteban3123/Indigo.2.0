



CREATE VIEW [MixingStation].[ViewDetailPharmaceuticalParametersNPT]
AS
	select
			  ROW_NUMBER() OVER(ORDER BY (SELECT NULL)) Id
			, pac.IPNOMCOMP PatientName
			, pac.IPCODPACI PatientCode
			, ISNULL(cma.NUMCAMHOS, '') AS Bed
			, CONCAT(ps.FunctionalUnitCode, ' - ', fu.Name) FunctionalUnitCodeName
			, pa.PESOPACIE PatientWeight
			, pa.VIADMIN AdministrationRoute
			, case pa.VIADMIN when 1 then 'Línea central' when 2 then 'Línea periférica' else '' end AdministrationRouteDescription
			, pa.TEMPOADMIN InfusionTime
			, pa.VOLUTOTAL TotalVolume
			, pa.VELINFUSION InfusionVelocity
			, pdd.NAMEFORM ParameterName
			, pdd.RESULT Result
			, pdd.COLOR Color
			, CAST(phd.GroupingCodeDose As Varchar(36)) GroupingCodeDose
	FROM MedicalHistory.ProductSusceptibleMixingStation ps
	JOIN MedicalHistory.PharmaDose phd WITH(NOLOCK) ON ps.CodeSusceptibleMixingStation = phd.CodeSusceptibleMixingStation
	JOIN HCNUTPAREC pa on ps.IdOrigin = pa.ID
	JOIN HCPARNUTC ha ON ha.ID = pa.IDHCPARNUTC AND ha.FinishedProductCode = ps.MainDrugCode
	JOIN HCNUTPAREFD pdd on	pdd.IDHCNUTPAREC = pa.ID
	JOIN INPACIENT pac on pa.IPCODPACI = pac.IPCODPACI
	JOIN Payroll.FunctionalUnit fu on ps.FunctionalUnitCode = fu.Code
	LEFT JOIN ADINGRESO ing ON pa.NUMINGRES = ing.NUMINGRES
	LEFT JOIN CHCAMASHO cma WITH(NOLOCK) ON ISNULL(CASE WHEN ing.CODCAMACT = 0 THEN '' ELSE Cast(ing.CODCAMACT As VARCHAR(15)) END, '') = cma.CODICAMAS