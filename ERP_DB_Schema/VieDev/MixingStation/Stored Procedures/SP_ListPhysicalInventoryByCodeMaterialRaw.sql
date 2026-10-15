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