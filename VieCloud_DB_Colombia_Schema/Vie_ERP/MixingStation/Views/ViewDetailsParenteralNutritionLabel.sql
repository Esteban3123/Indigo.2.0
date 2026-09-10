

CREATE VIEW  [MixingStation].[ViewDetailsParenteralNutritionLabel]
AS
--Medicamentos de la plantilla de nutrición parenteral, se muestran los macro y micro nutrientes, con su orden en la etiqueta, el volumen a administrar y su tipo de componente
SELECT 
		ppd.Id Id,
		npd.IDHCNUTPAREC ParenteralNutritionId,
		atc.AbbreviationName AbbreviationAtc,
		npd.CORRPURGACAL Volume,
		ppd.NPTItemOrder,
		ISNULL(atc.ComponentType, 0) ComponentType --1- Macro --2 Micro
FROM HCNUTPAREND npd
JOIN HCNUTPAREC npc ON npc.ID = npd.IDHCNUTPAREC
JOIN MedicalHistory.ProductSusceptibleMixingStation psms ON psms.IdOrigin = npc.ID
JOIN MixingStation.RequestMixingStationDetailPatients rmsdp ON psms.Id = rmsdp.EntityId AND rmsdp.PatientCode = npc.IPCODPACI
JOIN MixingStation.RequestMixingStationDetail rmsd ON rmsd.Id = rmsdp.RequestMixingStationDetailId
JOIN Inventory.ATC atc ON npd.CODPRODUC = atc.Code
JOIN MixingStation.PackagePersonalizedDetail ppd ON ppd.PackagePersonalizedId = rmsd.PackagePersonalizedId AND atc.Id = ppd.AtcId AND npd.CORRPURGACAL = ppd.Quantity
WHERE ppd.ComponentType NOT IN (4,5)

UNION ALL

--Medicamentos adicionados en el paquete de referencia, identificados como 'Medicamento adicional NPT'
SELECT 
		ppd.Id Id,
		psms.IdOrigin ParenteralNutritionId,
		atc.AbbreviationName AbbreviationAtc,
		ppd.Quantity Volume,
		ppd.NPTItemOrder,
		ISNULL(atc.ComponentType, 0) ComponentType --1- Macro --2 Micro
FROM MixingStation.PackagePersonalizedDetail ppd
JOIN Inventory.ATC atc ON ppd.AtcId = atc.Id
JOIN MixingStation.RequestMixingStationDetail rmsd on rmsd.PackagePersonalizedId = ppd.PackagePersonalizedId
JOIN MixingStation.RequestMixingStationDetailPatients rmsdp on rmsdp.RequestMixingStationDetailId = rmsd.Id
JOIN MedicalHistory.ProductSusceptibleMixingStation psms on psms.Id = rmsdp.EntityId AND psms.Origin = 'HCNUTPAREC'
WHERE ppd.ComponentType IN (4,5)
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Vista que consolida el detalle de componentes para la etiqueta de nutrición parenteral en la estación de mezclas. Combina la prescripción de nutrición parenteral (cabecera y detalle de nutrientes) con el catálogo ATC de medicamentos para obtener la abreviación y tipo de componente (macronutriente o micronutriente), el volumen a preparar, y el orden del ítem en la bolsa NPT según el paquete personalizado asignado. Vincula la prescripción del paciente con la solicitud de mezcla en farmacia, asegurando que los datos del etiquetado correspondan exactamente al paciente, la unidad y la solicitud de preparación magistral correcta. Se utiliza para imprimir o generar la etiqueta de la bolsa de nutrición parenteral total (NPT) con los ingredientes, volúmenes y orden de adición de cada componente.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'VIEW', @level1name = N'ViewDetailsParenteralNutritionLabel';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'VIEW', @level1name = N'ViewDetailsParenteralNutritionLabel';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Expone los detalles de componentes de una nutrición parenteral, vinculando cada producto con su clasificación ATC, volumen y orden, para la generación de etiquetas en la estación de mezclas.', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'VIEW', @level1name=N'ViewDetailsParenteralNutritionLabel';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El detalle de nutrición parenteral (HCNUTPAREND) debe estar asociado a una cabecera (HCNUTPAREC) existente.; La cabecera de nutrición parenteral debe estar registrada como producto susceptible de mezcla (ProductSusceptibleMixingStation.IdOrigin = HCNUTPAREC.ID).; Debe existir una solicitud de mezcla con detalle por paciente cuyo PatientCode coincida con HCNUTPAREC.IPCODPACI.; El código de producto (CODPRODUC) debe existir en el catálogo Inventory.ATC.; El ATC del producto debe estar incluido en el detalle del paquete personalizado asociado al detalle de la solicitud de mezcla.', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'VIEW', @level1name=N'ViewDetailsParenteralNutritionLabel';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se incluyen componentes cuyo paciente en HCNUTPAREC coincide con el paciente del detalle de la solicitud de mezcla (npc.IPCODPACI = rmsdp.PatientCode).; Solo se incluyen componentes cuyo ATC está presente tanto en el detalle de nutrición parenteral como en el paquete personalizado de la solicitud (atc.Id = ppd.AtcId).; El ComponentType retornado nunca es NULL (se sustituye por 0).; ComponentType clasifica los componentes como 1=Macro, 2=Micro, 0=no clasificado.', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'VIEW', @level1name=N'ViewDetailsParenteralNutritionLabel';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Nutrición parenteral; Etiqueta de mezcla; Clasificación ATC; Componentes macro y micro; Estación de mezclas (farmacia); Paquete personalizado; Solicitud de mezcla por paciente', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'VIEW', @level1name=N'ViewDetailsParenteralNutritionLabel';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] MixingStation.ViewDetailsParenteralNutritionLabel: Devuelve filas DISTINCT con Id, ParenteralNutritionId, AbbreviationAtc, Volume, NPTItemOrder y ComponentType por cada componente de nutrición parenteral vinculado a una solicitud de mezcla y a un paquete personalizado.', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'VIEW', @level1name=N'ViewDetailsParenteralNutritionLabel';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si atc.ComponentType es NULL → ComponentType se expone como 0 (sin clasificación macro/micro) else Se expone el valor de atc.ComponentType (1=Macro, 2=Micro según comentario en SQL)', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'VIEW', @level1name=N'ViewDetailsParenteralNutritionLabel';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.HCNUTPAREND; dbo.HCNUTPAREC; MedicalHistory.ProductSusceptibleMixingStation; MixingStation.RequestMixingStationDetailPatients; MixingStation.RequestMixingStationDetail; Inventory.ATC; MixingStation.PackagePersonalizedDetail', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'VIEW', @level1name=N'ViewDetailsParenteralNutritionLabel';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'VIEW', @level1name=N'ViewDetailsParenteralNutritionLabel';
GO
