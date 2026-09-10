-- =============================================
-- Author:		Diego A. Roldán
-- Create date: 2021-12-07
-- Description:	Procedimiento que se encarga de listar el inventario físico de acuerdo a un medicamento
-- =============================================
CREATE PROCEDURE [MixingStation].[SP_ListPhysicalInventoryByATCSupplyProduct] 
	@ATCId AS integer,
	@SupplyId as integer,
	@ProductId as integer,
	@WarehouseId AS integer,
	@StockId AS integer,
	@MaquilaId As Integer
AS
BEGIN
	SET NOCOUNT ON;
	
	/************************************* VARIABLES *************************************/

	--Tabla para devolver los resultados
	DECLARE @TableResult TABLE
	(
		Id INT NOT NULL,
		Code VARCHAR(20) NOT NULL,
		WarehouseId INT NOT NULL, 
		CodeNameWarehouse VARCHAR(500),
		ProductId INT NOT NULL,
		CodeNameProduct VARCHAR(500),
		BatchSerialId INT, 
		CodeNameBatchSerial VARCHAR(500),
		BatchSerialExpiredDate DATE, 
		Quantity INT NOT NULL,
		Covered BIT DEFAULT(0) NOT NULL,
		QuantityDelivered INT
	)

	BEGIN TRY

		/*****************************************  ATC - Productos - Insumos  *************************************************************/

--- Atc
		INSERT INTO @TableResult 
		(
			Id, Code, WarehouseId, ProductId, CodeNameProduct, BatchSerialId, Quantity
		)
			 SELECT	phy.Id,
					atc.Code,
					phy.WarehouseId,
					phy.ProductId,
					CONCAT(ip.Code, ' - ', ip.Name) ProductCodeName,
					phy.BatchSerialId,
					phy.Quantity
			 FROM Inventory.PhysicalInventory phy WITH (NOLOCK)
			 JOIN Inventory.Warehouse wu WITH (NOLOCK) ON wu.Id = phy.WarehouseId
			 JOIN Inventory.InventoryProduct ip WITH (NOLOCK) ON phy.ProductId = ip.Id
			 join Inventory.ProductType pt with(nolock) on ip.ProductTypeId = pt.Id
			 JOIN Inventory.ATC atc WITH (NOLOCK) ON ip.ATCId = atc.Id
			 WHERE ip.Atcid = @ATCId and phy.Quantity > 0 AND phy.WarehouseId IN (@StockId, @warehouseId, @MaquilaId) And pt.Class <> 5

--- Insumos
		INSERT INTO @TableResult 
		(
			Id, Code, WarehouseId, ProductId, CodeNameProduct, BatchSerialId, Quantity
		)
			 SELECT	phy.Id,
					ins.Code,
					phy.WarehouseId,
					phy.ProductId,
					CONCAT(ip.Code, ' - ', ip.Name) ProductCodeName,
					phy.BatchSerialId,
					phy.Quantity
			 FROM Inventory.PhysicalInventory phy WITH (NOLOCK)
			 JOIN Inventory.Warehouse wu WITH (NOLOCK) ON wu.Id = phy.WarehouseId
			 JOIN Inventory.InventoryProduct ip WITH (NOLOCK) ON phy.ProductId = ip.Id
			 join Inventory.ProductType pt with(nolock) on ip.ProductTypeId = pt.Id
			 JOIN Inventory.InventorySupplie ins WITH (NOLOCK) ON ip.SupplieId = ins.Id
			 LEFT JOIN @TableResult tr ON phy.Id = tr.Id
			 WHERE ip.SupplieId = @SupplyId and phy.Quantity > 0 AND tr.Id IS NULL AND phy.WarehouseId IN (@StockId, @warehouseId, @MaquilaId) And pt.Class <> 5

-- Productos
		INSERT INTO @TableResult 
		(
			Id, Code, WarehouseId, ProductId, CodeNameProduct, BatchSerialId, Quantity
		)
			 SELECT	phy.Id,
					ip.Code,
					phy.WarehouseId,
					phy.ProductId,
					CONCAT(ip.Code, ' - ', ip.Name) ProductCodeName,
					phy.BatchSerialId,
					phy.Quantity
			 FROM Inventory.PhysicalInventory phy WITH (NOLOCK)
			 JOIN Inventory.Warehouse wu WITH (NOLOCK) ON wu.Id = phy.WarehouseId
			 JOIN Inventory.InventoryProduct ip WITH (NOLOCK) ON phy.ProductId = ip.Id
			 join Inventory.ProductType pt with(nolock) on ip.ProductTypeId = pt.Id
			 LEFT JOIN @TableResult tr ON phy.Id = tr.Id
			 --WHERE  ip.Code  = @CodeProducto and phy.Quantity > 0 AND tr.Id IS NULL AND phy.WarehouseId IN (@StockId, @warehouseId, @MaquilaId) 
			 WHERE  ip.Id  = @ProductId and phy.Quantity > 0 AND tr.Id IS NULL AND phy.WarehouseId IN (@StockId, @warehouseId, @MaquilaId) And pt.Class <> 5

		UPDATE tr
			SET tr.CodeNameWarehouse = CONCAT(w.Code, ' - ', w.Name)
		FROM @TableResult tr
		JOIN Inventory.Warehouse w WITH (NOLOCK) ON tr.WarehouseId = w.Id

		UPDATE tr
			SET tr.CodeNameBatchSerial = bs.BatchCode,
				tr.BatchSerialExpiredDate = bs.ExpirationDate
		FROM @TableResult tr
		JOIN Inventory.BatchSerial bs WITH (NOLOCK) ON tr.BatchSerialId = bs.Id

		UPDATE tr
			SET tr.Covered = 1
		FROM @TableResult tr
		JOIN Inventory.ProductRateDetail prd WITH (NOLOCK) ON tr.ProductId = prd.ProductId
		JOIN Contract.CareGroup cg WITH (NOLOCK) ON prd.ProductRateId = cg.ProductRateId
		WHERE GETDATE() BETWEEN prd.InitialDate AND prd.EndDate

	END TRY
	BEGIN CATCH
		PRINT ERROR_MESSAGE() + ' Linea: ' + cast(ERROR_LINE() AS VARCHAR(20))
	END CATCH

	--Se retorna la tabla con los resultados
	SELECT	*
	FROM @TableResult
END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Lista el inventario físico disponible (cantidad mayor a cero) en bodegas específicas (stock, bodega principal y maquila) filtrando por medicamento ATC, insumo o producto individual. Combina registros de inventario físico con el catálogo de productos, bodegas, lotes/series y tarifas contractuales para determinar si cada ítem está cubierto por un contrato vigente. Consolida en un único resultado las tres modalidades de búsqueda (por clasificación ATC, por insumo y por producto específico), evitando duplicados. Se usa en la estación de mezclas para conocer el stock real disponible de un medicamento, insumo o producto antes de preparar una orden, incluyendo código, nombre, bodega, lote, fecha de vencimiento, cantidad y cobertura contractual.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'PROCEDURE', @level1name = N'SP_ListPhysicalInventoryByATCSupplyProduct';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'PROCEDURE', @level1name = N'SP_ListPhysicalInventoryByATCSupplyProduct';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Lista el inventario físico disponible en bodegas específicas para un medicamento identificado por ATC, insumo o producto, indicando lote, vencimiento y si está cubierto por una tarifa contractual vigente.', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'PROCEDURE', @level1name=N'SP_ListPhysicalInventoryByATCSupplyProduct';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Las bodegas referenciadas (@StockId, @WarehouseId, @MaquilaId) deben existir en Inventory.Warehouse; Los productos deben tener registro en Inventory.InventoryProduct con ATC, Supplie o Id correspondiente', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'PROCEDURE', @level1name=N'SP_ListPhysicalInventoryByATCSupplyProduct';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo considera inventario con Quantity > 0; Excluye productos cuyo ProductType.Class = 5; Restringe la búsqueda a las bodegas @StockId, @WarehouseId y @MaquilaId; Evita duplicados de PhysicalInventory.Id entre las tres inserciones (ATC, Insumo, Producto) mediante anti-join con la tabla resultado; La cobertura (Covered) sólo se determina por tarifas vigentes a la fecha actual asociadas a un CareGroup; Los errores se silencian imprimiendo el mensaje, sin propagarse al consumidor', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'PROCEDURE', @level1name=N'SP_ListPhysicalInventoryByATCSupplyProduct';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Inventario físico; Medicamento; Insumo; Producto; Clasificación ATC; Bodega/Almacén; Lote y vencimiento; Tarifa de producto; Grupo de atención (CareGroup); Cobertura contractual; Maquila', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'PROCEDURE', @level1name=N'SP_ListPhysicalInventoryByATCSupplyProduct';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[INSERT] @TableResult: Inserta filas de PhysicalInventory donde InventoryProduct.ATCId = @ATCId, Quantity > 0, WarehouseId IN (@StockId,@WarehouseId,@MaquilaId) y ProductType.Class <> 5; [INSERT] @TableResult: Inserta registros adicionales por insumo donde InventoryProduct.SupplieId = @SupplyId, Quantity > 0, ProductType.Class <> 5 y el Id de PhysicalInventory aún no esté en la tabla resultado; [INSERT] @TableResult: Inserta registros por producto donde InventoryProduct.Id = @ProductId, Quantity > 0, ProductType.Class <> 5 y el Id no esté ya en la tabla resultado; [UPDATE] @TableResult: Actualiza CodeNameWarehouse con CONCAT(Warehouse.Code,'' - '',Warehouse.Name) por join sobre WarehouseId; [UPDATE] @TableResult: Asigna CodeNameBatchSerial=BatchSerial.BatchCode y BatchSerialExpiredDate=BatchSerial.ExpirationDate uniendo por BatchSerialId; [UPDATE] @TableResult: Marca Covered=1 cuando existe ProductRateDetail vigente (GETDATE() BETWEEN InitialDate AND EndDate) ligado a un CareGroup mediante ProductRateId; [RETURN_RESULT] @TableResult: Devuelve SELECT * de la tabla resultado al finalizar', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'PROCEDURE', @level1name=N'SP_ListPhysicalInventoryByATCSupplyProduct';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si InventoryProduct.ATCId = @ATCId (productos con clasificación ATC) → Inserta usando código ATC como Code; si InventoryProduct.SupplieId = @SupplyId y registro no insertado previamente → Inserta usando código del insumo (InventorySupplie.Code) como Code; si InventoryProduct.Id = @ProductId y registro no insertado previamente → Inserta usando código del producto (InventoryProduct.Code) como Code; si Existe ProductRateDetail vigente en la fecha actual unido a Contract.CareGroup para el producto → Se marca Covered=1 else Covered permanece en 0 (default)', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'PROCEDURE', @level1name=N'SP_ListPhysicalInventoryByATCSupplyProduct';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Inventory.PhysicalInventory; Inventory.Warehouse; Inventory.InventoryProduct; Inventory.ProductType; Inventory.ATC; Inventory.InventorySupplie; Inventory.BatchSerial; Inventory.ProductRateDetail; Contract.CareGroup', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'PROCEDURE', @level1name=N'SP_ListPhysicalInventoryByATCSupplyProduct';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'PROCEDURE', @level1name=N'SP_ListPhysicalInventoryByATCSupplyProduct';
-- GO
