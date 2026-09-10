CREATE VIEW [FixedAsset].[ViewListFixedAssetEntryItemDetail]
AS
	SELECT
		ROW_NUMBER() OVER (ORDER BY FixedAssetEntryId DESC) AS Id
	   ,*
	FROM (SELECT
			CONCAT(fai.Code, ' - ', fai.Description) AS CodeNameItem
		   ,faeid.Plate
		   ,faeid.Serie
		   ,fae.Id AS FixedAssetEntryId
		   ,faei.Id AS FixedAssetEntryItemId
		   ,faeid.Id AS FixedAssetEntryItemDetailId
		   ,0 AS FixedAssetEntryDevolutionDetailId
		   ,SelectOption = 1
		   ,faei.Quantity
		   ,CAST(ROUND(faei.UnitValue, 2) AS DECIMAL(18, 2)) AS UnitValue
		   ,CAST(ROUND((faei.SubTotalValue / faei.Quantity), 2) AS DECIMAL(18, 2)) AS SubTotalValue
		   ,faei.IvaPercentage
		   ,CAST(ROUND((faei.IvaValue / faei.Quantity), 2) AS DECIMAL(18, 2)) AS IvaValue
		   ,faei.DiscountPercentage
		   ,CAST(ROUND((faei.DiscountValue / faei.Quantity), 2) AS DECIMAL(18, 2)) AS DiscountValue
		   ,CAST(ROUND((faei.TotalValue / faei.Quantity), 2) AS DECIMAL(18, 2)) AS TotalValue
		   ,faei.RTFPercentage
		   ,CAST(ROUND((faei.RTFValue / faei.Quantity), 2) + ISNULL(adjustment.diffRTFValue, 0) AS DECIMAL(18, 2)) AS RTFValue
		   ,CAST(ROUND((fae.WithholdingTax / fat.Quantity), 2) + ISNULL(adjustment.diffWithholdingTax, 0) AS DECIMAL(18, 2)) AS WithholdingTax
		   ,CAST(ROUND((fae.WithholdingICA / fat.Quantity), 2) AS DECIMAL(18, 2)) AS WithholdingICA
		   --,CAST(ROUND((fae.RetentionSource / fat.Quantity), 0) + ISNULL(adjustment.diffRetentionSource, 0) AS DECIMAL(18, 2)) AS RetentionSource
		   ,CAST(adjustment2.sumRTFValue + ISNULL(adjustment.diffRTFValue, 2) AS DECIMAL(18, 2)) AS RetentionSource
		   ,CAST(ROUND(faei.SubTotalValue, 2) AS DECIMAL(18, 2)) AS SubTotalValueSource
		   ,CAST(ROUND(faei.IvaValue, 2) AS DECIMAL(18, 2)) AS IvaValueSource
		   ,CAST(ROUND(faei.DiscountValue, 2) AS DECIMAL(18, 2)) AS DiscountValueSource
		   ,CAST(ROUND(faei.TotalValue, 2) AS DECIMAL(18, 2)) AS TotalValueSource
		FROM FixedAsset.FixedAssetEntryItemDetail faeid
		INNER JOIN fixedasset.FixedAssetEntryItem faei ON faeid.FixedAssetEntryItemId = faei.Id
		INNER JOIN FixedAsset.FixedAssetItem fai ON faei.ItemId = fai.Id
		INNER JOIN FixedAsset.FixedAssetEntry fae ON faei.FixedAssetEntryId = fae.Id
		INNER JOIN 
		(
			SELECT faei.FixedAssetEntryId, SUM(faei.Quantity) Quantity 
			FROM FixedAsset.FixedAssetEntryItem faei 
			GROUP BY faei.FixedAssetEntryId
		) fat ON fae.Id = fat.FixedAssetEntryId
		JOIN
		(
			SELECT faei.Id,
				SUM(ROUND((faei.RTFValue / faei.Quantity), 2)) sumRTFValue
			FROM FixedAsset.FixedAssetEntryItemDetail faeid
			INNER JOIN fixedasset.FixedAssetEntryItem faei ON faeid.FixedAssetEntryItemId = faei.Id
			GROUP BY faei.Id
		) adjustment2 ON faei.Id = adjustment2.Id
		LEFT JOIN
		(
			SELECT MIN(faeid.Id) Id,
				fae.RetentionSource - SUM(ROUND((faei.RTFValue / faei.Quantity), 2)) diffRTFValue,
				fae.WithholdingTax - SUM(ROUND((fae.WithholdingTax / fat.Quantity), 2)) diffWithholdingTax
			FROM FixedAsset.FixedAssetEntryItemDetail faeid
			INNER JOIN fixedasset.FixedAssetEntryItem faei ON faeid.FixedAssetEntryItemId = faei.Id
			INNER JOIN FixedAsset.FixedAssetEntry fae ON faei.FixedAssetEntryId = fae.Id
			INNER JOIN 
			(
				SELECT faei.FixedAssetEntryId, SUM(faei.Quantity) Quantity 
				FROM FixedAsset.FixedAssetEntryItem faei 
				GROUP BY faei.FixedAssetEntryId
			) fat ON fae.Id = fat.FixedAssetEntryId
			GROUP BY fae.Id, fae.RetentionSource, fae.WithholdingTax
		) adjustment ON faeid.Id = adjustment.Id		
		WHERE fae.Status = 2 --and faeid.Refund = 0
	) AS datos
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Vista que muestra el detalle individual de cada activo fijo registrado en una entrada de activos (compra o adquisición), desglosando la información por ítem y por unidad física identificada con placa y serie. Integra la entrada de activo (proveedor, factura, impuestos), el ítem del catálogo de activos fijos (código y descripción del bien) y el detalle específico de cada unidad (placa, serie, responsable, ubicación, condiciones de depreciación y garantía). Calcula y distribuye proporcionalmente por unidad los valores monetarios de cada bien: valor unitario, subtotal, IVA, descuento, valor total, porcentaje y valor de RTF (retefuente), retención en la fuente, retención ICA y retención por fuente, aplicando ajustes de redondeo para que los totales distribuidos cuadren exactamente con los totales de la entrada. Sirve para reportería y consulta de activos fijos activos (estado 2) a nivel de cada unidad física adquirida, útil en auditoría de inventario, contabilidad de activos y control de bienes por placa o serie.', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'VIEW', @level1name = N'ViewListFixedAssetEntryItemDetail';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'VIEW', @level1name = N'ViewListFixedAssetEntryItemDetail';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Lista el detalle unitario (por placa/serie) de los ítems de entradas de activos fijos confirmadas, prorrateando valores monetarios por unidad y ajustando diferencias de redondeo en retenciones.', @level0type=N'SCHEMA', @level0name=N'FixedAsset', @level1type=N'VIEW', @level1name=N'ViewListFixedAssetEntryItemDetail';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'La entrada de activo fijo debe estar en Status = 2 (confirmada/aprobada) para ser incluida.; Cada ítem debe tener Quantity > 0, ya que se utiliza como divisor en los cálculos unitarios.; Cada detalle debe estar vinculado a un ítem de entrada (FixedAssetEntryItemId) y este a una entrada y a un artículo del catálogo.', @level0type=N'SCHEMA', @level0name=N'FixedAsset', @level1type=N'VIEW', @level1name=N'ViewListFixedAssetEntryItemDetail';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Los valores monetarios del ítem (subtotal, IVA, descuento, total, RTF) se exponen siempre como valor unitario (dividido entre la cantidad del ítem) y redondeados a 2 decimales.; Las diferencias por redondeo entre el total registrado en la entrada (RetentionSource, WithholdingTax) y la suma de los prorrateos se concentran en un único detalle (el de menor Id) para garantizar conciliación.; La vista nunca muestra entradas en estado distinto de 2.; Cada fila representa una unidad física (placa/serie) y no una línea agregada del ítem.; FixedAssetEntryDevolutionDetailId es constante 0 (la vista no maneja devoluciones).', @level0type=N'SCHEMA', @level0name=N'FixedAsset', @level1type=N'VIEW', @level1name=N'ViewListFixedAssetEntryItemDetail';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Activo fijo; Entrada/ingreso de activo fijo; Ítem de entrada; Detalle por placa y serie; IVA; Descuento; Retención en la fuente (RetentionSource/RTF); Retención de IVA (WithholdingTax); Retención de ICA (WithholdingICA); Prorrateo unitario; Ajuste por diferencia de redondeo', @level0type=N'SCHEMA', @level0name=N'FixedAsset', @level1type=N'VIEW', @level1name=N'ViewListFixedAssetEntryItemDetail';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] FixedAsset.ViewListFixedAssetEntryItemDetail: Solo devuelve filas cuando fae.Status = 2; los detalles de entradas con otro estado no aparecen.; [RETURN_RESULT] FixedAsset.ViewListFixedAssetEntryItemDetail: Calcula valores unitarios dividiendo SubTotalValue/IvaValue/DiscountValue/TotalValue/RTFValue del ítem entre su Quantity y redondea a 2 decimales.; [RETURN_RESULT] FixedAsset.ViewListFixedAssetEntryItemDetail: WithholdingTax por unidad se calcula como fae.WithholdingTax / suma de cantidades de la entrada (fat.Quantity), sumando un ajuste por diferencia de redondeo (diffWithholdingTax) solo en el detalle con MIN(Id).; [RETURN_RESULT] FixedAsset.ViewListFixedAssetEntryItemDetail: RTFValue unitario se ajusta sumando diffRTFValue (= fae.RetentionSource - suma de RTFValue prorrateados) únicamente en el detalle con MIN(Id) de la entrada.; [RETURN_RESULT] FixedAsset.ViewListFixedAssetEntryItemDetail: RetentionSource se expone como sumRTFValue del ítem + diffRTFValue (con ISNULL=2 cuando no hay ajuste, lo que constituye un valor por defecto particular).; [RETURN_RESULT] FixedAsset.ViewListFixedAssetEntryItemDetail: FixedAssetEntryDevolutionDetailId siempre se retorna como 0 y SelectOption siempre como 1, marcando que es un detalle de entrada (no devolución) seleccionable por defecto.; [RETURN_RESULT] FixedAsset.ViewListFixedAssetEntryItemDetail: Se asigna un Id secuencial mediante ROW_NUMBER ordenado por FixedAssetEntryId DESC, mostrando primero las entradas más recientes.; [RETURN_RESULT] FixedAsset.ViewListFixedAssetEntryItemDetail: CodeNameItem concatena código y descripción del artículo del catálogo separados por '' - ''.', @level0type=N'SCHEMA', @level0name=N'FixedAsset', @level1type=N'VIEW', @level1name=N'ViewListFixedAssetEntryItemDetail';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si fae.Status = 2 → Se incluye el detalle del ítem de entrada en el resultado else Se excluye (no aparece en la vista); si faeid.Id = MIN(faeid.Id) por entrada (subconsulta adjustment) → Se aplica el ajuste de diferencia de redondeo (diffRTFValue, diffWithholdingTax) sobre ese detalle else Se usa ISNULL = 0 para RTFValue/WithholdingTax (sin ajuste); para RetentionSource se usa ISNULL = 2', @level0type=N'SCHEMA', @level0name=N'FixedAsset', @level1type=N'VIEW', @level1name=N'ViewListFixedAssetEntryItemDetail';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'FixedAsset.FixedAssetEntryItemDetail; FixedAsset.FixedAssetEntryItem; FixedAsset.FixedAssetItem; FixedAsset.FixedAssetEntry', @level0type=N'SCHEMA', @level0name=N'FixedAsset', @level1type=N'VIEW', @level1name=N'ViewListFixedAssetEntryItemDetail';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'FixedAsset', @level1type=N'VIEW', @level1name=N'ViewListFixedAssetEntryItemDetail';
GO
