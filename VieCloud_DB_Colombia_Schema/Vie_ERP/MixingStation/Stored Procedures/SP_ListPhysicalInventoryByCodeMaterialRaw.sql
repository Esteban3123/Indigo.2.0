-- =============================================
-- Author:		Juan David Patiño Cabrera
-- Create date: 2021-07-26
-- Description:	Procedimiento que se encarga de listar el inventario físico de acuerdo a un medicamento
-- =============================================
CREATE PROCEDURE [MixingStation].[SP_ListPhysicalInventoryByCodeMaterialRaw] 
	@CodeProducto AS varchar(20),
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
		WarehouseCodeName VARCHAR(500),
		ProductId INT NOT NULL,
		ProductCodeName VARCHAR(500),
		BatchSerialId INT, 
		BatchSerialCode VARCHAR(500),
		BatchSerialExpirationDate DATE, 
		Quantity INT NOT NULL,
		Covered BIT DEFAULT(0) NOT NULL,
		QuantityDelivered INT
	)

	BEGIN TRY

		/*****************************************  ATC - Productos - Insumos  *************************************************************/

--- Atc
		INSERT INTO @TableResult 
		(
			Id, Code, WarehouseId, ProductId, ProductCodeName, BatchSerialId, Quantity
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
			 JOIN Inventory.ATC atc WITH (NOLOCK) ON ip.ATCId = atc.Id
			 WHERE ip.Atcid = @CodeProducto and phy.Quantity > 0   AND phy.WarehouseId IN (@StockId, @warehouseId, @MaquilaId) 

--- Insumos
		INSERT INTO @TableResult 
		(
			Id, Code, WarehouseId, ProductId, ProductCodeName, BatchSerialId, Quantity
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
			 JOIN Inventory.InventorySupplie ins WITH (NOLOCK) ON ip.SupplieId = ins.Id
			 LEFT JOIN @TableResult tr ON phy.Id = tr.Id
			 WHERE ip.SupplieId = @CodeProducto and phy.Quantity > 0 AND tr.Id IS NULL AND phy.WarehouseId IN (@StockId, @warehouseId, @MaquilaId) 

-- Productos
		INSERT INTO @TableResult 
		(
			Id, Code, WarehouseId, ProductId, ProductCodeName, BatchSerialId, Quantity
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
			 LEFT JOIN @TableResult tr ON phy.Id = tr.Id
			 WHERE  ip.Code  = @CodeProducto and phy.Quantity > 0 AND tr.Id IS NULL AND phy.WarehouseId IN (@StockId, @warehouseId, @MaquilaId) 

---
		INSERT INTO @TableResult 
		(
			Id, Code, WarehouseId, ProductId, ProductCodeName, Quantity
		)
			SELECT	0 Id,
					ip.Code,
					w.Id WarehouseId,
					ip.ProductId,
					ip.ProductCodeName,
					9999999 Quantity
			FROM
			(
				SELECT	w.Id
				FROM Inventory.WarehouseUser wu WITH (NOLOCK)
				JOIN Inventory.Warehouse w WITH (NOLOCK) ON wu.WarehouseId = w.Id
				WHERE w.VirtualStore = 1
			) w
			JOIN
			(
				SELECT tr.Code, tr.ProductId, tr.ProductCodeName
				FROM @TableResult tr
				GROUP BY tr.Code, tr.ProductId, tr.ProductCodeName
			) ip ON 1 = 1
			LEFT JOIN @TableResult tr ON w.Id = tr.WarehouseId AND ip.ProductId = tr.ProductId
			WHERE tr.Id IS NULL

		/*******************************************************************************************/
		
		UPDATE tr
			SET tr.WarehouseCodeName = CONCAT(w.Code, ' - ', w.Name)
		FROM @TableResult tr
		JOIN Inventory.Warehouse w WITH (NOLOCK) ON tr.WarehouseId = w.Id

		UPDATE tr
			SET tr.BatchSerialCode = bs.BatchCode,
				tr.BatchSerialExpirationDate = bs.ExpirationDate
		FROM @TableResult tr
		JOIN Inventory.BatchSerial bs WITH (NOLOCK) ON tr.BatchSerialId = bs.Id

		UPDATE tr
			SET tr.Covered = 1
		FROM @TableResult tr
		JOIN Inventory.ProductRateDetail prd WITH (NOLOCK) ON tr.ProductId = prd.ProductId
		JOIN Contract.CareGroup cg WITH (NOLOCK) ON prd.ProductRateId = cg.ProductRateId
		WHERE Common.GETDATE() BETWEEN prd.InitialDate AND prd.EndDate

	END TRY
	BEGIN CATCH
		PRINT ERROR_MESSAGE() + ' Linea: ' + cast(ERROR_LINE() AS VARCHAR(20))
	END CATCH

	--Se retorna la tabla con los resultados
	SELECT	*
	FROM @TableResult
END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Lista el inventario físico disponible (cantidad mayor a cero) de un material o producto específico en una estación de mezclas, filtrando por bodegas de stock, bodega principal y bodega de maquila. Busca el producto en tres categorías: medicamentos con clasificación ATC, insumos y productos generales, y además incluye bodegas virtuales donde el producto aún no tenga registro. Enriquece los resultados con el nombre y código de la bodega, información de lote y fecha de vencimiento, e indica si el producto está cubierto por un contrato o tarifa vigente. Se utiliza en el proceso de preparación y dispensación de mezclas farmacéuticas para conocer la disponibilidad real de materias primas antes de iniciar la producción.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'PROCEDURE', @level1name = N'SP_ListPhysicalInventoryByCodeMaterialRaw';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'PROCEDURE', @level1name = N'SP_ListPhysicalInventoryByCodeMaterialRaw';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Lista el inventario físico disponible para un medicamento/insumo/producto en bodegas específicas (stock, bodega y maquila), enriqueciendo con lote, vencimiento y marca de cobertura por tarifa de grupo de atención.', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'PROCEDURE', @level1name=N'SP_ListPhysicalInventoryByCodeMaterialRaw';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El código recibido debe corresponder a un ATCId, SupplieId o Code de InventoryProduct existente.; Las bodegas indicadas (StockId, WarehouseId, MaquilaId) deben existir en Inventory.Warehouse.; Para que aplique la marca Covered debe existir una ProductRateDetail vigente (GETDATE entre InitialDate y EndDate) ligada a un CareGroup.', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'PROCEDURE', @level1name=N'SP_ListPhysicalInventoryByCodeMaterialRaw';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se incluyen filas con Quantity > 0 provenientes de PhysicalInventory.; Solo se consideran bodegas dentro del conjunto (@StockId, @WarehouseId, @MaquilaId) salvo las bodegas virtuales que se agregan al final.; Un mismo registro de PhysicalInventory.Id no se duplica entre ATC, Insumo y Producto (controlado vía LEFT JOIN @TableResult … IS NULL).; Las bodegas con VirtualStore=1 siempre aparecen con cantidad 9999999 si el producto no está en ellas.; Covered se setea solo cuando hay tarifa vigente vinculada a un CareGroup; nunca se desmarca a 0 explícitamente.; Los errores se silencian (PRINT) y el SP retorna lo procesado hasta el fallo.', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'PROCEDURE', @level1name=N'SP_ListPhysicalInventoryByCodeMaterialRaw';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Inventario físico; Medicamento (ATC); Insumo médico; Producto; Bodega/Almacén; Bodega virtual; Maquila; Lote y fecha de vencimiento; Tarifa de producto vigente; Grupo de atención (CareGroup); Cobertura del producto', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'PROCEDURE', @level1name=N'SP_ListPhysicalInventoryByCodeMaterialRaw';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[INSERT] @TableResult: Inserta inventario físico cuando ip.ATCId = @CodeProducto, phy.Quantity > 0 y la bodega es una de (@StockId, @WarehouseId, @MaquilaId).; [INSERT] @TableResult: Inserta inventario por insumo cuando ip.SupplieId = @CodeProducto, phy.Quantity > 0, la bodega está en el conjunto permitido y el registro no fue ya cargado por ATC.; [INSERT] @TableResult: Inserta inventario por producto cuando ip.Code = @CodeProducto, phy.Quantity > 0, la bodega está en el conjunto permitido y no fue cargado previamente.; [INSERT] @TableResult: Para cada producto ya cargado, agrega filas ficticias (Id=0, Quantity=9999999) por cada bodega marcada como VirtualStore=1 que no tenga aún el producto, simulando disponibilidad ilimitada en almacenes virtuales.; [UPDATE] @TableResult: Actualiza WarehouseCodeName concatenando Code y Name de Inventory.Warehouse para cada fila.; [UPDATE] @TableResult: Actualiza BatchSerialCode y BatchSerialExpirationDate desde Inventory.BatchSerial cuando hay BatchSerialId asociado.; [UPDATE] @TableResult: Marca Covered=1 cuando el producto tiene una ProductRateDetail vigente (Common.GETDATE() entre InitialDate y EndDate) asociada a un CareGroup mediante ProductRateId.; [RETURN_RESULT] (resultset): Retorna SELECT * FROM @TableResult con todas las filas consolidadas.; [RAISERROR] (consola): En CATCH imprime ERROR_MESSAGE() + línea, sin relanzar la excepción (silencia errores).', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'PROCEDURE', @level1name=N'SP_ListPhysicalInventoryByCodeMaterialRaw';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si ip.ATCId = @CodeProducto → Carga inventario clasificándolo por código ATC.; si ip.SupplieId = @CodeProducto y el registro no se cargó por ATC → Carga inventario clasificándolo por código de insumo.; si ip.Code = @CodeProducto y el registro no se cargó previamente → Carga inventario por código directo del producto.; si Bodega tiene VirtualStore = 1 y no existe fila para ese producto en esa bodega → Inserta fila con Id=0 y Quantity=9999999 (stock virtual ilimitado).; si Existe ProductRateDetail vigente (GETDATE entre InitialDate y EndDate) ligada a CareGroup → Marca el producto como Covered=1. else Covered queda en 0 (default).', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'PROCEDURE', @level1name=N'SP_ListPhysicalInventoryByCodeMaterialRaw';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'Common.GETDATE', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'PROCEDURE', @level1name=N'SP_ListPhysicalInventoryByCodeMaterialRaw';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Inventory.PhysicalInventory; Inventory.Warehouse; Inventory.InventoryProduct; Inventory.ATC; Inventory.InventorySupplie; Inventory.WarehouseUser; Inventory.BatchSerial; Inventory.ProductRateDetail; Contract.CareGroup', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'PROCEDURE', @level1name=N'SP_ListPhysicalInventoryByCodeMaterialRaw';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'PROCEDURE', @level1name=N'SP_ListPhysicalInventoryByCodeMaterialRaw';
-- GO
