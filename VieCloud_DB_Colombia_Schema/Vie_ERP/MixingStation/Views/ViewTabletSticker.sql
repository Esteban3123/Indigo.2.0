

CREATE VIEW [MixingStation].[ViewTabletSticker]
AS
	WITH CTE_Concentration AS(

	SELECT	a.Id,
			IIF(a.ConcentrationQuantity = 0, 
				IIF(a.Concentration = '0', CONCAT(a.ConcentrationQuantity, ' ', mu.Abbreviation), a.Concentration),
				CONCAT(a.ConcentrationQuantity, ' ', mu.Abbreviation)
			) Concentration,
			pf.Name PharmaceuticalForm
	FROM Inventory.ATC a (NOLOCK)
	JOIN Inventory.PharmaceuticalForm pf (NOLOCK) ON pf.Id = a.PharmaceuticalFormId
	JOIN Inventory.InventoryMeasurementUnit mu (NOLOCK) ON a.ConcentrationMeasureUnitId = mu.Id), 
		
	CTE_ProductsCampaignDetail AS 
	(
	SELECT cd.Id CampaignDetailId
		, ip.Id ProductId
		, ip.Code ProductCode
		, ip.Name ProductName
		, ip.HealthRegistration
		, bs.BatchCode ProductBatchCode
		, bs.ExpirationDate
		, a.AbbreviationName ProductAbbreviation
		, a.Id AtcId
		, cdv.ItemType
	FROM MixingStation.CampaignDetailValidation cdv
	JOIN MixingStation.CampaignDetail cd on cd.Id = cdv.CampaignDetailId
	JOIN Inventory.InventoryProduct ip (NOLOCK) ON cdv.ProductId = ip.Id
	JOIN Inventory.ATC a ON a.Id = ip.ATCId
	JOIN Inventory.BatchSerial bs (NOLOCK) ON bs.Id = cdv.BatchSerialId)

SELECT  CONCAT(rpds.Id, '-', cte.ProductId) Id
	, rpds.Id AS RequestPackageDetailStatusId
	, rmsd.CampaignDetailId
	, cte.ProductId
	, cte.ProductCode
	, cte.ProductName
	, cte.ProductAbbreviation
	, c.Concentration AS Dosis
	, 'VIA ORAL' AS AdministrationRoute
	, c.PharmaceuticalForm AS PharmaceuticalForm
	, rpds.BatchCode AS InternalBatchCode
	, cte.ProductBatchCode
	, cd.CreationDate AS CampaignDate
	, rpds.BatchExpirationDate   as ExpirationDate
	, cte.HealthRegistration
	, ud.MSClass as UnitDoseTypeClass
	, cduPA.Nombre AS CreatedBy
	, cduQC.Nombre AS VerifiedBy
FROM  MixingStation.RequestPackageDetailStatus rpds
JOIN MixingStation.RequestMixingStationDetail rmsd ON rmsd.Id = rpds.RequestMixingStationDetailId
JOIN MixingStation.CampaignDetail cd on	 cd.Id = rmsd.CampaignDetailId
JOIN MixingStation.UnitDoseType ud ON ud.Id = cd.UnitDoseTypeId
JOIN CTE_ProductsCampaignDetail cte on cte.CampaignDetailId = cd.Id AND cte.AtcId = rmsd.ATCId
JOIN CTE_Concentration c ON cte.AtcId = c.Id
LEFT JOIN [MixingStation].[ViewUsersRolsCM] cduPA (NOLOCK) ON cd.Id = cduPA.CampaignDetailId AND cduPA.UserRole = 3 -- Auxiliar de producción
LEFT JOIN [MixingStation].[ViewUsersRolsCM] cduQC (NOLOCK) ON cd.Id = cduQC.CampaignDetailId AND cduQC.UserRole = 1 -- QF Calidad 
WHERE rmsd.LabelType = 4 AND cte.ItemType = 1
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Vista que genera la información necesaria para imprimir las etiquetas (stickers) de tabletas preparadas en la estación de mezclas farmacéuticas. Combina los datos del producto (código, nombre, abreviatura, registro sanitario, lote y fecha de vencimiento), la concentración y forma farmacéutica del catálogo ATC, la dosis calculada y la vía de administración (oral), junto con el lote interno asignado y la fecha de la campaña de preparación. Incluye además el nombre del auxiliar de producción que elaboró la dosis unitaria y el químico farmacéutico de calidad que la verificó, filtrando únicamente los paquetes de tipo etiqueta para tabletas (LabelType = 4, ItemType = 1). Esta vista es la fuente de datos para el sistema de etiquetado y trazabilidad de dosis unitarias orales sólidas dispensadas desde la central de mezclas.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'VIEW', @level1name = N'ViewTabletSticker';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'VIEW', @level1name = N'ViewTabletSticker';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Provee la información consolidada necesaria para imprimir el sticker/etiqueta de tabletas preparadas en la estación de mezclas, combinando datos del paquete, producto, lote, concentración, forma farmacéutica y los responsables de producción y control de calidad.', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'VIEW', @level1name=N'ViewTabletSticker';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Debe existir un RequestPackageDetailStatus ligado a un RequestMixingStationDetail con LabelType = 4.; El CampaignDetail asociado debe tener al menos una CampaignDetailValidation con ItemType = 1 cuyo ATC coincida con el ATCId del RequestMixingStationDetail.; El producto debe estar parametrizado con ATC, PharmaceuticalForm e InventoryMeasurementUnit válidos para que aparezca en la vista (INNER JOINs).; Debe existir un BatchSerial ligado a la validación de campaña para obtener el lote y la fecha de vencimiento del producto.', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'VIEW', @level1name=N'ViewTabletSticker';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'La vía de administración siempre se reporta como ''VIA ORAL'' (literal fijo en el SELECT).; Solo se exponen paquetes cuyo detalle de solicitud tiene LabelType = 4 (etiqueta tipo tableta).; Solo se incluyen validaciones de campaña cuyo ItemType = 1 (producto principal, no insumo/empaque).; El Id de la fila es la concatenación ''RequestPackageDetailStatusId-ProductId'', garantizando unicidad por paquete-producto.; La concentración se construye siempre con la abreviatura de la unidad de medida del ATC, salvo cuando ConcentrationQuantity = 0 y existe texto en Concentration.; El producto reportado debe pertenecer al mismo ATC que el detalle de mezcla (cte.AtcId = rmsd.ATCId).; CreatedBy y VerifiedBy pueden ser NULL si no existe usuario con rol 3 o 1 asociado a la campaña (LEFT JOIN).', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'VIEW', @level1name=N'ViewTabletSticker';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'sticker/etiqueta de tableta; campaña de preparación farmacéutica; estación de mezclas; dosis unitaria; concentración del medicamento; forma farmacéutica; lote interno y lote del producto; registro sanitario (HealthRegistration); ATC; vía de administración oral; auxiliar de producción; QF de calidad', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'VIEW', @level1name=N'ViewTabletSticker';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] MixingStation.ViewTabletSticker: Devuelve una fila por paquete (RequestPackageDetailStatus) y producto, solo cuando rmsd.LabelType = 4 y la validación de campaña tiene ItemType = 1.', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'VIEW', @level1name=N'ViewTabletSticker';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si ConcentrationQuantity = 0 AND Concentration = ''0'' → Muestra ''0 <abreviatura unidad>'' como concentración else Si ConcentrationQuantity = 0 pero Concentration <> ''0'', usa el texto libre Concentration; en cualquier otro caso concatena ConcentrationQuantity con la abreviatura de la unidad; si cduPA.UserRole = 3 → Se considera el usuario como ''Auxiliar de producción'' (CreatedBy); si cduQC.UserRole = 1 → Se considera el usuario como ''QF Calidad'' (VerifiedBy)', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'VIEW', @level1name=N'ViewTabletSticker';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Inventory.ATC; Inventory.PharmaceuticalForm; Inventory.InventoryMeasurementUnit; MixingStation.CampaignDetailValidation; MixingStation.CampaignDetail; Inventory.InventoryProduct; Inventory.BatchSerial; MixingStation.RequestPackageDetailStatus; MixingStation.RequestMixingStationDetail; MixingStation.UnitDoseType; MixingStation.ViewUsersRolsCM', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'VIEW', @level1name=N'ViewTabletSticker';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'VIEW', @level1name=N'ViewTabletSticker';
GO
