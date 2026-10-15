


CREATE VIEW [MixingStation].[PendingRequestsCampaignForIngress]
AS

SELECT	ROW_NUMBER() OVER(ORDER BY (SELECT NULL)) Id
		,cd.CampaignNumber 
		,cd.CampaignStatus
		,CASE cd.CampaignStatus
			WHEN 1 THEN 'Abierta'
			WHEN 2 THEN 'Cerrada'
			WHEN 5 THEN 'Procesada'
		END CampaignStatusName
		,a.NUMINGRES 
		,a.IPCODPACI PatientCode
		FROM MixingStation.RequestPackageDetailStatus rpds
		JOIN MixingStation.RequestMixingStationDetail rmd ON rmd.Id = rpds.RequestMixingStationDetailId
		JOIN MixingStation.CampaignDetail cd ON cd.Id = rmd.CampaignDetailId
		JOIN MedicalHistory.PharmaDose phd ON phd.GroupingCodeDose = rpds.GroupingCodeDose
		JOIN MedicalHistory.ProductSusceptibleMixingStation psms on psms.CodeSusceptibleMixingStation = phd.CodeSusceptibleMixingStation and phd.ProductCode = psms.MainDrugCode
		JOIN HCFARMEPD hcf on psms.CodeSusceptibleMixingStation = hcf.CodeSusceptibleMixingStation and hcf.CODPRODUC = psms.MainDrugCode
		JOIN HCFARMEPC fpc on hcf.CODCONCEC = fpc.CODCONCEC
		JOIN ADINGRESO a ON a.NUMINGRES = fpc.NUMINGRES
		JOIN Contract.CareGroupMixLiquidation cgml ON cgml.CareGroupId = a.GENCAREGROUP AND cd.UnitDoseTypeId = cgml.UnitDoseTypeId
		WHERE cd.CampaignStatus IN (1,2,5) AND rmd.Source = 1 AND cgml.TypePayment = 2