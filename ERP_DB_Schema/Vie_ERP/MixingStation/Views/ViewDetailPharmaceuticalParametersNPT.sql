

CREATE VIEW [MixingStation].[ViewDetailPharmaceuticalParametersNPT]
AS
SELECT
			  pdd.ID
			, pac.IPNOMCOMP PatientName
			, pac.IPCODPACI PatientCode
			, ISNULL(cma.NUMCAMHOS, '') AS Bed
			, CONCAT(psms.FunctionalUnitCode, ' - ', fu.Name) FunctionalUnitCodeName
			, pa.PESOPACIE PatientWeight
			, pa.VIADMIN AdministrationRoute
			, case pa.VIADMIN when 1 then 'Línea central' when 2 then 'Línea periférica' else '' end AdministrationRouteDescription
			, pa.TEMPOADMIN InfusionTime
			, pa.VOLUTOTAL TotalVolume
			, pa.VELINFUSION InfusionVelocity
			, pdd.NAMEFORM ParameterName
			, pdd.RESULT Result
			, pdd.COLOR Color
			, CAST(ph.GroupingCodeDose As Varchar(36)) GroupingCodeDose
	FROM HCNUTPAREFD pdd
	INNER JOIN HCNUTPAREC pa ON pa.ID = pdd.IDHCNUTPAREC
	INNER JOIN MedicalHistory.ProductSusceptibleMixingStation psms ON psms.Origin = 'HCNUTPAREC' AND psms.IdOrigin = pa.ID
	INNER JOIN INPACIENT pac on pa.IPCODPACI = pac.IPCODPACI
	INNER JOIN Payroll.FunctionalUnit fu on psms.FunctionalUnitCode = fu.Code
	LEFT JOIN ADINGRESO ing ON pa.NUMINGRES = ing.NUMINGRES
	LEFT JOIN CHCAMASHO cma ON ISNULL(CASE WHEN ing.CODCAMACT = 0 THEN '' ELSE Cast(ing.CODCAMACT As VARCHAR(15)) END, '') = cma.CODICAMAS
	OUTER APPLY (	SELECT 
					TOP 1 ph.GroupingCodeDose 
					FROM MedicalHistory.PharmaDose ph
					WHERE ph.CodeSusceptibleMixingStation = psms.CodeSusceptibleMixingStation) ph 