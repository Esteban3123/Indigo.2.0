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