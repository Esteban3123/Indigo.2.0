CREATE PROCEDURE [MixingStation].[SP_ProductListCampaingDetails]
(
    @ATCEntityId INT,
	@StockId INT,
	@warehouseId INT,
	@PharmaceuticalFormId INT,
	@RequestMeasurementUnitCode VARCHAR(20),
	@AtcId INT,
	@Type INT,
	@MSClass INT
)
AS
BEGIN
    SET NOCOUNT ON
	-- Usa UnitType de InventoryMeasurementUnit (1=Peso, 2=Volumen) para evitar códigos hardcodeados
	SELECT DISTINCT atc.Id, 
		atc.Code AS ProductCode, 
		atc.NAME AS ProductName,
        atc.AbbreviationName AS ProductAbbreviationName,
		CAST((CASE 
				WHEN mu.UnitType = 1 THEN atc.Weight
				WHEN mu.UnitType = 2 THEN COALESCE(atc.Volume, atc.Weight, 1)
				WHEN atc.FormulationType IN (1, 3) AND @Type = 1 AND @MSClass <> 2 THEN atc.Weight  -- compatibilidad si no hay unidad en catálogo
				ELSE COALESCE(atc.Volume, atc.Weight, 1)
			END) AS DECIMAL(18,2)
			) AS Concentration,
		MixingStation.CalculationQuantityMaterialRaw(
			FormulationType, 
			atc.Code, 
			(CASE WHEN atc.FormulationType = 2 THEN atc.Volume ELSE atc.Weight END), 
			@RequestMeasurementUnitCode
		) AS Dosis
		FROM  Inventory.ATC atc
		JOIN Inventory.InventoryProduct B WITH(NOLOCK) ON B.Status = 1 AND B.ATCId = atc.Id
		JOIN Inventory.ProductType pt WITH(NOLOCK) ON b.ProductTypeId = pt.Id
		JOIN Inventory.PhysicalInventory phy WITH(NOLOCK) ON phy.ProductId = B.Id
		JOIN Inventory.Warehouse E WITH(NOLOCK) ON phy.WarehouseId = E.Id
		LEFT JOIN Inventory.InventoryMeasurementUnit mu WITH(NOLOCK) ON mu.Code = @RequestMeasurementUnitCode AND mu.Status = 1
	WHERE ((@AtcId IS NOT NULL AND atc.Id = @AtcId) OR (@AtcId IS NULL AND atc.ATCEntityId = @ATCEntityId))
		AND atc.PharmaceuticalFormId = @PharmaceuticalFormId 
		AND E.Id IN (@StockId, @warehouseId) AND pt.Class <> 5 AND phy.Quantity > 0
	ORDER BY Concentration DESC

END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Lista los productos (medicamentos o insumos) disponibles en bodega para una campaña o preparación en la estación de mezclas, filtrando por clasificación ATC, forma farmacéutica, bodega y tipo de producto. Cruza el catálogo ATC con el inventario de productos, el stock físico en bodega y las unidades de medida para calcular la concentración (peso o volumen según el tipo de unidad solicitada) y la dosis de cada material. Excluye productos sin stock disponible y ciertos tipos de clase de producto, ordenando los resultados de mayor a menor concentración. Se usa para identificar qué materias primas o medicamentos están disponibles para formular una mezcla o preparación magistral en el módulo de estación de mezclas.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'PROCEDURE', @level1name = N'SP_ProductListCampaingDetails';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'PROCEDURE', @level1name = N'SP_ProductListCampaingDetails';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Lista los productos (medicamentos/insumos) candidatos para una campaña de mezclas en central de mezclas, calculando su concentración y dosis según forma farmacéutica y existencia física en bodegas indicadas.', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'PROCEDURE', @level1name=N'SP_ProductListCampaingDetails';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Debe proporcionarse @PharmaceuticalFormId para filtrar por forma farmacéutica.; Debe proporcionarse al menos uno entre @AtcId o @ATCEntityId (si @AtcId es NULL, se usa @ATCEntityId).; Las bodegas @StockId y/o @warehouseId deben existir en Inventory.Warehouse.; @RequestMeasurementUnitCode requerido por la función CalculationQuantityMaterialRaw para conversión de unidades.', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'PROCEDURE', @level1name=N'SP_ProductListCampaingDetails';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se consideran productos de inventario activos (InventoryProduct.Status = 1).; Se excluyen productos cuyo ProductType.Class = 5.; Solo se consideran productos con existencia física positiva (PhysicalInventory.Quantity > 0).; Las bodegas consultadas se restringen estrictamente a @StockId y @warehouseId.; La concentración nunca es nula: si no hay Volume ni Weight, se asume 1.; Los resultados son únicos por producto (DISTINCT) y siempre ordenados por concentración descendente.', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'PROCEDURE', @level1name=N'SP_ProductListCampaingDetails';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Central de mezclas (MixingStation); Clasificación ATC de medicamentos; Forma farmacéutica; Tipo de formulación (sólido/líquido); Concentración (peso/volumen); Dosis; Inventario físico por bodega; Unidad de medida de solicitud', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'PROCEDURE', @level1name=N'SP_ProductListCampaingDetails';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] Resultset: Devuelve productos DISTINCT con Id, ProductCode, ProductName, ProductAbbreviationName, Concentration y Dosis ordenados por Concentration DESC, filtrando solo productos con InventoryProduct.Status=1, ProductType.Class<>5 y PhysicalInventory.Quantity>0 en las bodegas @StockId o @warehouseId.', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'PROCEDURE', @level1name=N'SP_ProductListCampaingDetails';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si atc.FormulationType IN (1,3) AND @Type = 1 AND @MSClass <> 2 → Concentration = atc.Weight (se prioriza el peso del producto). else Concentration = COALESCE(atc.Volume, atc.Weight, 1) (se toma volumen, en su defecto peso, o 1).; si atc.FormulationType = 2 → Para el cálculo de Dosis se envía atc.Volume a CalculationQuantityMaterialRaw. else Para el cálculo de Dosis se envía atc.Weight a CalculationQuantityMaterialRaw.; si @AtcId IS NOT NULL → Se filtra por atc.Id = @AtcId (un producto específico). else Se filtra por atc.ATCEntityId = @ATCEntityId (todos los productos de la entidad ATC).', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'PROCEDURE', @level1name=N'SP_ProductListCampaingDetails';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'MixingStation.CalculationQuantityMaterialRaw', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'PROCEDURE', @level1name=N'SP_ProductListCampaingDetails';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Inventory.ATC; Inventory.InventoryProduct; Inventory.ProductType; Inventory.PhysicalInventory; Inventory.Warehouse', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'PROCEDURE', @level1name=N'SP_ProductListCampaingDetails';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'PROCEDURE', @level1name=N'SP_ProductListCampaingDetails';
-- GO
