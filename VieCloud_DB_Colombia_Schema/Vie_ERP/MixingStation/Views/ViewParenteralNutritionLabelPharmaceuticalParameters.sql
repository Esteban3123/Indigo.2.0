CREATE VIEW [MixingStation].[ViewParenteralNutritionLabelPharmaceuticalParameters]
AS
	SELECT 
		npp.ID Id
		, npp.IDHCNUTPAREC ParenteralNutritionId
		--------------------------------------
		, npp.NAMEFORM ParameterName
		, npp.RESULT Result
	FROM HCNUTPAREFD npp
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Parámetros farmacéuticos calculados para la etiqueta de nutrición parenteral. Expone los valores numéricos de cada parámetro nutricional (como volumen, concentración, velocidad de infusión) asociados a una fórmula de nutrición parenteral específica, tomando los datos de resultados de evaluación nutricional registrados en HCNUTPAREFD. Se utiliza para imprimir o generar la etiqueta farmacéutica de la bolsa de nutrición parenteral en la estación de mezclas, mostrando el nombre del parámetro y su resultado calculado.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'VIEW', @level1name = N'ViewParenteralNutritionLabelPharmaceuticalParameters';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'VIEW', @level1name = N'ViewParenteralNutritionLabelPharmaceuticalParameters';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Expone los parámetros farmacéuticos asociados a una nutrición parenteral para su uso en el etiquetado de la mezcla en la central de mezclas.', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'VIEW', @level1name=N'ViewParenteralNutritionLabelPharmaceuticalParameters';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Cada parámetro farmacéutico expuesto está vinculado a una nutrición parenteral mediante IDHCNUTPAREC.; No aplica filtros: expone la totalidad de filas de HCNUTPAREFD.', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'VIEW', @level1name=N'ViewParenteralNutritionLabelPharmaceuticalParameters';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Nutrición parenteral; Parámetros farmacéuticos; Etiquetado de mezclas (central de mezclas)', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'VIEW', @level1name=N'ViewParenteralNutritionLabelPharmaceuticalParameters';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] HCNUTPAREFD: Devuelve, por cada registro de HCNUTPAREFD, el identificador del parámetro, el identificador de la nutrición parenteral asociada (IDHCNUTPAREC), el nombre del parámetro (NAMEFORM) y su resultado (RESULT).', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'VIEW', @level1name=N'ViewParenteralNutritionLabelPharmaceuticalParameters';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'HCNUTPAREFD', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'VIEW', @level1name=N'ViewParenteralNutritionLabelPharmaceuticalParameters';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'VIEW', @level1name=N'ViewParenteralNutritionLabelPharmaceuticalParameters';
GO
