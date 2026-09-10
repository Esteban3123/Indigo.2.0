

CREATE VIEW [MixingStation].[ViewListValidationWeightNPT]
AS

WITH WeightItems AS (
	SELECT
		rpds.Id RequestPackageDetailStatusId
		, rmsd.PackagePersonalizedId
		, htc.IPCODPACI PatientCode
		, htd.CODPRODUC ProductCode
		, htd.CORRPURGACAL * a.Density TheoreticalWeightItems
	FROM MixingStation.RequestPackageDetailStatus rpds
	JOIN MixingStation.RequestMixingStationDetail rmsd ON rmsd.Id = rpds.RequestMixingStationDetailId
	JOIN MedicalHistory.PharmaDose pd ON pd.GroupingCodeDose = rpds.GroupingCodeDose
	JOIN HCFARMEPD hcd ON pd.IDHCFARMEPC = hcd.CODCONCEC AND hcd.SourceTable = 'HCNUTPAREC'
	JOIN HCNUTPAREC htc ON htc.ID = hcd.IdSourceTable 
	JOIN HCNUTPAREND htd ON htd.IDHCNUTPAREC = htc.ID
	JOIN Inventory.ATC a ON a.Code = htd.CODPRODUC
	GROUP BY rpds.Id, rmsd.PackagePersonalizedId, htc.IPCODPACI, htd.CODPRODUC, htd.CORRPURGACAL, a.Density
),
Weights AS (
	SELECT
		RequestPackageDetailStatusId
		, PackagePersonalizedId
		, PatientCode
		, SUM(TheoreticalWeightItems) TheoreticalWeightItems
	FROM WeightItems
	GROUP BY RequestPackageDetailStatusId, PackagePersonalizedId, PatientCode
)
SELECT	weights.RequestPackageDetailStatusId
		, weights.PatientCode
		, TRY_CAST(weights.TheoreticalWeightItems AS DECIMAL(18,2)) TheoreticalWeight
		, TRY_CAST(ISNULL(inputs.WeightParenteralNutritionSupply,0) AS DECIMAL(18,2)) InputsWeight
		, TRY_CAST((weights.TheoreticalWeightItems + ISNULL(inputs.WeightParenteralNutritionSupply,0)) AS DECIMAL(18,2)) TheoreticalAndInputsWeight
		, TRY_CAST((weights.TheoreticalWeightItems - (weights.TheoreticalWeightItems * 0.05)) AS DECIMAL(18,2)) MinimunWeight
		, TRY_CAST((weights.TheoreticalWeightItems + (weights.TheoreticalWeightItems * 0.05)) AS DECIMAL(18,2)) MaximunWeight
FROM Weights weights
OUTER APPLY (
	SELECT SUM(ip.WeightParenteralNutritionSupply) WeightParenteralNutritionSupply
	FROM MixingStation.PackagePersonalizedDetail ppd
	JOIN Inventory.InventoryProduct ip ON ip.Id = ppd.ProductId
	WHERE ppd.PackagePersonalizedId = weights.PackagePersonalizedId
		AND ppd.ComponentType = 4
		AND ppd.ProductId IS NOT NULL
) inputs

GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Vista de validación de pesos teóricos para bolsas de Nutrición Parenteral Total (NPT) preparadas en la estación de mezclas. Para cada paquete/bolsa generado, calcula el peso teórico sumando el volumen purgado de cada componente nutricional (aminoácidos, lípidos, glucosa, electrolitos, etc.) multiplicado por su densidad según el catálogo ATC, y le añade el peso de los insumos adicionales personalizados (ComponentType=4) registrados en el detalle del paquete. A partir del peso teórico total, expone los límites mínimo y máximo permitidos con una tolerancia del ±5%, junto con el código del paciente, para que el operador de la estación de mezclas pueda validar que el peso real de la bolsa preparada se encuentra dentro del rango aceptable de calidad antes de la dispensación.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'VIEW', @level1name = N'ViewListValidationWeightNPT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'VIEW', @level1name = N'ViewListValidationWeightNPT';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Calcula, para cada solicitud de mezcla de Nutrición Parenteral Total, el peso teórico de la fórmula, el peso de los insumos y los límites de tolerancia (±5%) usados para validar el pesaje real de la bolsa.', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'VIEW', @level1name=N'ViewListValidationWeightNPT';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Las dosis en MedicalHistory.PharmaDose deben enlazar con HCFARMEPD mediante IDHCFARMEPC=CODCONCEC y SourceTable=''HCNUTPAREC''.; Los productos referenciados en HCNUTPAREND.CODPRODUC deben existir en Inventory.ATC para obtener su Density.; Debe existir un RequestPackageDetailStatus cuyo GroupingCodeDose coincida con el de la PharmaDose para que la fila aparezca en la vista.; El RequestMixingStationDetail asociado debe estar vinculado al RequestPackageDetailStatus.', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'VIEW', @level1name=N'ViewListValidationWeightNPT';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'El peso teórico de cada ítem se calcula como CORRPURGACAL * Density del producto en Inventory.ATC.; El peso mínimo aceptable es el peso teórico menos el 5% y el peso máximo es el peso teórico más el 5%, definiendo una tolerancia simétrica del ±5% para validar el pesaje.; Solo se consideran dosis farmacéuticas cuyo origen (HCFARMEPD.SourceTable) sea ''HCNUTPAREC'', es decir, prescripciones de nutrición parenteral.; Para el peso de insumos solo se suman componentes del paquete personalizado con ComponentType = 4 y ProductId no nulo.; El peso total esperado (TheoreticalAndInputsWeight) combina el peso teórico de los ítems de la fórmula NPT con el peso del insumo de nutrición parenteral del producto de inventario.; Si no existe insumo asociado al paquete personalizado, el peso de insumos se asume 0 (ISNULL).; Las conversiones a DECIMAL(18,2) se realizan con TRY_CAST, devolviendo NULL si la conversión falla en lugar de error.', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'VIEW', @level1name=N'ViewListValidationWeightNPT';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Nutrición Parenteral Total (NPT); Paciente; Dosis farmacéutica; Densidad de producto; Peso teórico de mezcla; Peso de insumos; Tolerancia de pesaje (±5%); Estación de mezclas; Paquete personalizado', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'VIEW', @level1name=N'ViewListValidationWeightNPT';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] MixingStation.ViewListValidationWeightNPT: Devuelve por cada RequestPackageDetailStatus de NPT: peso teórico (suma de CORRPURGACAL*Density), peso de insumos (WeightParenteralNutritionSupply de InventoryProduct con ComponentType=4), peso total esperado y rango aceptable [teórico-5%, teórico+5%].', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'VIEW', @level1name=N'ViewListValidationWeightNPT';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'MedicalHistory.PharmaDose; dbo.HCFARMEPD; dbo.HCNUTPAREC; dbo.HCNUTPAREND; Inventory.ATC; MixingStation.RequestPackageDetailStatus; MixingStation.RequestMixingStationDetail; MixingStation.PackagePersonalizedDetail; Inventory.InventoryProduct', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'VIEW', @level1name=N'ViewListValidationWeightNPT';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'VIEW', @level1name=N'ViewListValidationWeightNPT';
GO
