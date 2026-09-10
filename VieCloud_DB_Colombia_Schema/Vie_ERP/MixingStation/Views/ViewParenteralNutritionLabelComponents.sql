

CREATE VIEW [MixingStation].[ViewParenteralNutritionLabelComponents]
AS
	SELECT DISTINCT	
		npd.ID Id
		, npd.IDHCNUTPAREC ParenteralNutritionId
		, CONCAT (RTRIM(npd.CODPRODUC), ' - ', RTRIM(atc.[Name])) AS ProductCode
		, npd.NAMENUT AS NutritionName
		, TRY_CAST(ISNULL(npd.APORTENUT, 0) AS DECIMAL(18,2)) Request
		, TRY_CAST(ISNULL(npd.VOLUMENCAL, 0) AS DECIMAL(18,2)) Volume
		, ppd.NPTItemOrder
	FROM HCNUTPAREND npd
	JOIN HCNUTPAREC npc ON npc.ID = npd.IDHCNUTPAREC
	JOIN MedicalHistory.ProductSusceptibleMixingStation psms ON psms.IdOrigin = npc.ID
	JOIN MixingStation.RequestMixingStationDetailPatients rmsdp ON psms.Id = rmsdp.EntityId AND rmsdp.PatientCode = npc.IPCODPACI
	JOIN MixingStation.RequestMixingStationDetail rmsd ON rmsd.Id = rmsdp.RequestMixingStationDetailId
	JOIN Inventory.ATC atc ON npd.CODPRODUC = atc.Code
	JOIN MixingStation.PackagePersonalizedDetail ppd ON ppd.PackagePersonalizedId = rmsd.PackagePersonalizedId AND atc.Id = ppd.AtcId
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Vista que muestra los componentes individuales (nutrientes y productos) de cada etiqueta de nutrición parenteral preparada en la estación de mezclas. Combina la prescripción de nutrición parenteral (HCNUTPAREC/HCNUTPAREND) con el catálogo de medicamentos ATC, la solicitud de mezcla del paciente y el detalle del paquete personalizado, para obtener por cada componente su código y nombre de producto, el nombre del nutriente, el aporte nutricional solicitado y el volumen calculado. Está diseñada para generar e imprimir etiquetas de bolsas de nutrición parenteral total (NPT) en farmacia, garantizando que cada ítem del paquete personalizado quede asociado correctamente al paciente y a la orden de preparación sin duplicados.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'VIEW', @level1name = N'ViewParenteralNutritionLabelComponents';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'VIEW', @level1name = N'ViewParenteralNutritionLabelComponents';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Expone los componentes (productos, aportes y volúmenes) de cada nutrición parenteral, enriquecidos con datos ATC y orden dentro del paquete personalizado, para imprimir etiquetas en la estación de mezclas.', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'VIEW', @level1name=N'ViewParenteralNutritionLabelComponents';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Debe existir la cabecera de nutrición parenteral (HCNUTPAREC) referenciada por cada detalle (HCNUTPAREND.IDHCNUTPAREC).; La cabecera de nutrición parenteral debe estar registrada en MedicalHistory.ProductSusceptibleMixingStation como origen de mezcla.; Debe existir una solicitud de mezcla (RequestMixingStationDetail) con su detalle de pacientes vinculado al producto susceptible y al mismo paciente de la nutrición parenteral.; El código de producto (CODPRODUC) del componente debe existir en Inventory.ATC.; El ATC del componente debe estar declarado dentro del paquete personalizado (PackagePersonalizedDetail) asociado al detalle de la solicitud de mezcla.', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'VIEW', @level1name=N'ViewParenteralNutritionLabelComponents';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se exponen componentes cuyo producto (CODPRODUC) existe en el catálogo Inventory.ATC, garantizando descripción ATC válida.; Solo se incluyen componentes cuya cabecera de nutrición parenteral (HCNUTPAREC) está registrada como producto susceptible de mezcla (ProductSusceptibleMixingStation con IdOrigin = ID de la cabecera).; El paciente del detalle de la solicitud de mezcla (RequestMixingStationDetailPatients.PatientCode) debe coincidir con el paciente de la cabecera de nutrición parenteral (HCNUTPAREC.IPCODPACI).; El componente solo aparece si el ATC del producto está incluido en el detalle del paquete personalizado (PackagePersonalizedDetail) asociado al detalle de la solicitud de mezcla.; Los valores de aporte (Request) y volumen (Volume) nulos se exponen como 0 y se truncan a DECIMAL(18,2); valores no numéricos producen NULL por TRY_CAST.; ProductCode se construye concatenando el código del producto y el nombre ATC separados por '' - '', con espacios derechos eliminados.; Se aplica DISTINCT: no se devuelven filas duplicadas para una misma combinación de componente y paquete.', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'VIEW', @level1name=N'ViewParenteralNutritionLabelComponents';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'nutrición parenteral; componentes de etiqueta; estación de mezclas; paquete personalizado; clasificación ATC; paciente; preparado magistral', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'VIEW', @level1name=N'ViewParenteralNutritionLabelComponents';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] MixingStation.ViewParenteralNutritionLabelComponents: Devuelve un conjunto de componentes de etiqueta de nutrición parenteral solo cuando existe trazabilidad completa entre el detalle de nutrición (HCNUTPAREND), su cabecera, el producto susceptible de mezcla, la solicitud de mezcla del mismo paciente y el detalle del paquete personalizado por ATC.', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'VIEW', @level1name=N'ViewParenteralNutritionLabelComponents';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.HCNUTPAREND; dbo.HCNUTPAREC; MedicalHistory.ProductSusceptibleMixingStation; MixingStation.RequestMixingStationDetailPatients; MixingStation.RequestMixingStationDetail; Inventory.ATC; MixingStation.PackagePersonalizedDetail', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'VIEW', @level1name=N'ViewParenteralNutritionLabelComponents';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'VIEW', @level1name=N'ViewParenteralNutritionLabelComponents';
GO
