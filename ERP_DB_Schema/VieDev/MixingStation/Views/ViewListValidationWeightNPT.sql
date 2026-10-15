



CREATE VIEW [MixingStation].[ViewListValidationWeightNPT]
AS


SELECT	rpds.Id RequestPackageDetailStatusId
		, K.PatientCode
		, TRY_CAST(SUM(K.TheoreticalWeightItems) AS DECIMAL(18,2)) TheoreticalWeight
		, TRY_CAST(ISNULL(ip.WeightParenteralNutritionSupply,0) AS DECIMAL(18,2)) InputsWeight
		, TRY_CAST((SUM(k.TheoreticalWeightItems) + ISNULL(ip.WeightParenteralNutritionSupply,0)) AS DECIMAL(18,2)) TheoreticalAndInputsWeight
		, TRY_CAST((SUM(k.TheoreticalWeightItems) - (SUM(k.TheoreticalWeightItems) * 0.05)) AS DECIMAL(18,2)) MinimunWeight
		, TRY_CAST((SUM(k.TheoreticalWeightItems) + (SUM(k.TheoreticalWeightItems) * 0.05)) AS DECIMAL(18,2)) MaximunWeight
FROM (
	SELECT
		pd.GroupingCodeDose
		, htd.CODPRODUC ProductCode
		, htd.CORRPURGACAL * a.Density TheoreticalWeightItems
		, htc.IPCODPACI PatientCode
	FROM MedicalHistory.PharmaDose pd
	JOIN HCFARMEPD hcd ON pd.IDHCFARMEPC = hcd.CODCONCEC AND hcd.SourceTable = 'HCNUTPAREC'
	JOIN HCNUTPAREC htc ON htc.ID = hcd.IdSourceTable 
	JOIN HCNUTPAREND htd ON htd.IDHCNUTPAREC = htc.ID
	JOIN Inventory.ATC a ON a.Code = htd.CODPRODUC
	GROUP BY pd.GroupingCodeDose, htd.CODPRODUC, htd.CORRPURGACAL, a.Density, htc.IPCODPACI
	) K
JOIN MixingStation.RequestPackageDetailStatus rpds ON rpds.GroupingCodeDose = k.GroupingCodeDose
JOIN MixingStation.RequestMixingStationDetail rmsd ON rmsd.Id = rpds.RequestMixingStationDetailId
LEFT JOIN (
    SELECT ppd.PackagePersonalizedId, SUM(ip.WeightParenteralNutritionSupply) AS WeightParenteralNutritionSupply
    FROM MixingStation.PackagePersonalizedDetail ppd
    JOIN Inventory.InventoryProduct ip ON ip.Id = ppd.ProductId
    WHERE ppd.ComponentType = 4 AND ppd.ProductId IS NOT NULL
    GROUP BY ppd.PackagePersonalizedId
) ip ON ip.PackagePersonalizedId = rmsd.PackagePersonalizedId
GROUP BY k.PatientCode, rpds.Id, rmsd.PackagePersonalizedId, ip.WeightParenteralNutritionSupply