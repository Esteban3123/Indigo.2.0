-- =============================================
-- Author:		Cristhian Salazar
-- Create date: 27/01/2016
-- Description:	Store para afectar el inventario fisico y registrar en el kardex, es decir que este store lo deben consumir todos los procesos que hagan movimientos de productos
-- =============================================
CREATE PROCEDURE [Inventory].[SP_SavePhysicalInventoryKardex_Output]
	@Kardex XML,
	@EntityId INT,
	@EntityCode VARCHAR(20),
	@EntityName VARCHAR(100),
	@User VARCHAR(20),
	@ControlCost BIT = 1,
	------------------------------------------------------
	@CodeResult INT OUTPUT,
	@MessageResult VARCHAR(MAX) OUTPUT
AS
BEGIN
	SET NOCOUNT ON

	/*************************************************** VARIABLES ***************************************************/

	DECLARE @Message VARCHAR(MAX)

	CREATE TABLE #TableKardex
	(
		Id INT identity(1,1) PRIMARY KEY,
		DocumentDate DATETIME,
		ThirdPartyId INT,
		ProductId INT,
		WarehouseId INT,
		BatchSerialId INT NULL,
		MovementType TINYINT,
		Quantity INT,
		PreviousAmountProduct INT DEFAULT(0),
		PreviousAmountWarehouse INT DEFAULT(0),
		PreviousAmountBatch INT DEFAULT(0),
		Value DECIMAL(20,2),
		PreviousCost DECIMAL(20,2) DEFAULT(0),
		AverageCost DECIMAL(20,4) DEFAULT(0),
		PreviousAverageCost DECIMAL(20,2) DEFAULT(0),
		ImportedEntityId INT,
		ImportedEntityCode VARCHAR(20),
		ImportedEntityName VARCHAR(250),
		AffectAverageCost BIT,
		LastPurchase DATETIME,
		LastSale DATETIME
	)

	-------------------------------------------------------------------------------------------------------------------

    BEGIN TRY

		INSERT INTO #TableKardex
		(
			DocumentDate, ThirdPartyId,
			ProductId, WarehouseId, BatchSerialId,
			MovementType, Quantity, Value,
			ImportedEntityId, ImportedEntityCode, ImportedEntityName,
			AffectAverageCost
		)
		SELECT	ISNULL(t.x.value('DocumentDate[1]','DATETIME'), [Common].[GETDATE]()),
				t.x.value('ThirdPartyId[1]','INT'),
				t.x.value('ProductId[1]','INT'),
				t.x.value('WarehouseId[1]','INT'),
				t.x.value('BatchSerialId[1]','INT'),
				t.x.value('MovementType[1]','TINYINT'),
				t.x.value('Quantity[1]','INT'),
				t.x.value('Value[1]','DECIMAL(20,4)'),
				t.x.value('ImportedEntityId[1]','INT'),
				t.x.value('ImportedEntityCode[1]','VARCHAR(20)'),
				t.x.value('ImportedEntityName[1]','VARCHAR(250)'),
				ISNULL(t.x.value('AffectAverageCost[1]','BIT'), 1)
		FROM @Kardex.nodes('/Kardex') t(x)

		CREATE NONCLUSTERED INDEX IX_TableKardex_Product_Id
			ON #TableKardex (ProductId, Id)
			INCLUDE (MovementType, Quantity, AffectAverageCost, PreviousAmountProduct, PreviousAverageCost, AverageCost, Value)

		CREATE NONCLUSTERED INDEX IX_TableKardex_Product_Warehouse_Id
			ON #TableKardex (ProductId, WarehouseId, Id)
			INCLUDE (MovementType, Quantity, AffectAverageCost, PreviousAmountWarehouse)

		CREATE NONCLUSTERED INDEX IX_TableKardex_Product_Warehouse_Batch_Id
			ON #TableKardex (ProductId, WarehouseId, BatchSerialId, Id)
			INCLUDE (MovementType, Quantity, AffectAverageCost, PreviousAmountBatch)

		CREATE NONCLUSTERED INDEX IX_TableKardex_MovementType
			ON #TableKardex (MovementType)
			INCLUDE (ProductId, WarehouseId, BatchSerialId, Quantity)

		/*************************************************************************************************************/

		IF @EntityName = 'EntranceVoucher'
		BEGIN
			UPDATE k
				SET k.DocumentDate = ev.DocumentDate,
					k.LastPurchase = ev.InvoiceDate
			FROM #TableKardex k
			JOIN Inventory.EntranceVoucher ev ON @EntityId = ev.Id
		END
		ELSE IF @EntityName = 'EntranceVoucherDevolution'
		BEGIN
			UPDATE k
				SET k.DocumentDate = evdev.DocumentDate
			FROM #TableKardex k
			JOIN Inventory.EntranceVoucherDevolution evdev ON @EntityId = evdev.Id
		END
		ELSE IF @EntityName = 'PharmaceuticalDispensing'
		BEGIN
			UPDATE k
				SET k.DocumentDate = pd.DocumentDate
			FROM #TableKardex k
			JOIN Inventory.PharmaceuticalDispensing pd ON @EntityId = pd.Id
		END
		ELSE IF @EntityName = 'PharmaceuticalDispensingDevolution'
		BEGIN
			UPDATE k
				SET k.DocumentDate = pddev.DocumentDate
			FROM #TableKardex k
			JOIN Inventory.PharmaceuticalDispensingDevolution pddev ON @EntityId = pddev.Id
		END
		ELSE IF @EntityName = 'InventoryAdjustment'
		BEGIN
			UPDATE k
				SET k.DocumentDate = ia.DocumentDate
			FROM #TableKardex k
			JOIN Inventory.InventoryAdjustment ia ON @EntityId = ia.Id
		END
		ELSE IF @EntityName = 'LoanMerchandise'
		BEGIN
			UPDATE k
				SET k.DocumentDate = lm.DocumentDate
			FROM #TableKardex k
			JOIN Inventory.LoanMerchandise lm ON @EntityId = lm.Id
		END
		ELSE IF @EntityName = 'LoanMerchandiseDevolution'
		BEGIN
			UPDATE k
				SET k.DocumentDate = lmdev.DocumentDate
			FROM #TableKardex k
			JOIN Inventory.LoanMerchandiseDevolution lmdev ON @EntityId = lmdev.Id
		END
		ELSE IF @EntityName = 'DocumentInvoiceProductSales'
		BEGIN
			UPDATE k
				SET k.LastSale = dips.DocumentDate
			FROM #TableKardex k
			JOIN Inventory.DocumentInvoiceProductSales dips ON @EntityId = dips.Id
		END
		ELSE IF @EntityName = 'BasicBilling'
		BEGIN
			UPDATE k
				SET k.DocumentDate = bb.DocumentDate
			FROM #TableKardex k
			JOIN Billing.BasicBilling bb ON @EntityId = bb.Id
		END
		ELSE IF @EntityName = 'BasicBillingDevolution'
		BEGIN
			UPDATE k
				SET k.DocumentDate = bb.DocumentDate
			FROM #TableKardex k
			JOIN Billing.BasicBilling bb ON @EntityId = bb.Id
		END
		ELSE IF @EntityName = 'TransferOrder'
		BEGIN
			UPDATE k
				SET k.DocumentDate = lmdev.DocumentDate
			FROM #TableKardex k
			JOIN Inventory.TransferOrder lmdev ON @EntityId = lmdev.Id
		END
		ELSE IF @EntityName = 'TransferOrderDevolution'
		BEGIN
			UPDATE k
				SET k.DocumentDate = lmdev.DocumentDate
			FROM #TableKardex k
			JOIN Inventory.TransferOrderDevolution lmdev ON @EntityId = lmdev.Id
		END

		/*************************************************************************************************************/

		-- Costo promedio
		UPDATE k
		SET Value = IIF(w.ControlStore = 1, p.ProductCost, k.Value),
		PreviousCost = p.FinalProductCost,
		PreviousAverageCost = p.ProductCost,
		AffectAverageCost = CASE
			WHEN @EntityName = 'EntranceVoucherDevolution' AND k.AffectAverageCost = 0 THEN 0
			WHEN @EntityName = 'EntranceVoucher' AND k.ImportedEntityName IN ('RemissionEntrance', 'ConsignmentInventoryRemission') THEN 0
			ELSE 1
		END,
		LastPurchase = IIF(ISNULL(k.LastPurchase, p.LastPurchase) > ISNULL(p.LastPurchase, '1900-01-01'),k.LastPurchase, p.LastPurchase),
		LastSale = IIF(ISNULL(k.LastSale, p.LastSale) > ISNULL(p.LastSale, '1900-01-01'),k.LastSale, p.LastSale)
		FROM #TableKardex k
		JOIN Inventory.InventoryProduct p ON k.ProductId = p.Id
		JOIN Inventory.Warehouse w ON k.WarehouseId = w.Id

		-- Cantidad anterior producto
		UPDATE k
			SET k.PreviousAmountProduct = phy.Quantity
		FROM
		(
			SELECT MIN(Id) Id
			FROM #TableKardex
			GROUP BY ProductId
		) km
		JOIN #TableKardex k ON km.Id = k.Id
		JOIN
		(
			SELECT ProductId, SUM(Quantity) Quantity
			FROM Inventory.PhysicalInventory
			GROUP BY ProductId
		) phy ON k.ProductId = phy.ProductId

		-- Cantidad anterior producto, almacen
		UPDATE k
			SET k.PreviousAmountWarehouse = phy.Quantity
		FROM
		(
			SELECT MIN(Id) Id
			FROM #TableKardex
			GROUP BY ProductId, WarehouseId
		) km
		JOIN #TableKardex k ON km.Id = k.Id
		JOIN
		(
			SELECT ProductId, WarehouseId, SUM(Quantity) Quantity
			FROM Inventory.PhysicalInventory
			GROUP BY ProductId, WarehouseId
		) phy ON k.ProductId = phy.ProductId AND k.WarehouseId = phy.WarehouseId

		-- Cantidad anterior producto, almacen, lote
		UPDATE k
			SET k.PreviousAmountBatch = phy.Quantity
		FROM
		(
			SELECT MIN(Id) Id
			FROM #TableKardex
			GROUP BY ProductId, WarehouseId, BatchSerialId
		) km
		JOIN #TableKardex k ON km.Id = k.Id
		JOIN
		(
			SELECT ProductId, WarehouseId, BatchSerialId, SUM(Quantity) Quantity
			FROM Inventory.PhysicalInventory
			GROUP BY ProductId, WarehouseId, BatchSerialId
		) phy ON k.ProductId = phy.ProductId AND k.WarehouseId = phy.WarehouseId AND k.BatchSerialId = phy.BatchSerialId

	/******************************************** VALIDACION DE PRODUCTOS HABILITADOS/ RESTRINGIDOS *******************************************/
	 --si existen almacenes dentro del kardex enviado que tenga habilitado restricciones
		IF EXISTS(SELECT 1 FROM #TableKardex TK
					JOIN Inventory.Warehouse W ON W.Id = TK.WarehouseId
					where W.HandleRestrictedProducts =1)
		BEGIN
			-------------------------PRODUCTOS RESTRINGIDOS------------------------------
			IF EXISTS
			(
				SELECT 1
				FROM #TableKardex k
				join Inventory.Warehouse w on w.Id = k.WarehouseId
				join Inventory.InventoryProduct ip on ip.Id = K.ProductId
				join Inventory.WarehouseRestrictedConditions wc on wc.WarehouseId = w.Id
				where W.HandleRestrictedProducts =1 and wc.RestrictionType=2 and (ip.Id = wc.ProductId or ip.ProductSubGroupId = wc.ProductSubgroupId or ip.ProductTypeId = wc.ProductTypeId
				or ip.ProductGroupId = wc.ProductGroupId)
			)
			BEGIN
				SELECT @Message = STUFF((
							SELECT DISTINCT CHAR(13) + CHAR(10) + ' - ' + ip.Code + ' - ' + ip.Name
							FROM #TableKardex k
							join Inventory.Warehouse w on w.Id = k.WarehouseId
							join Inventory.InventoryProduct ip on ip.Id = K.ProductId
							join Inventory.WarehouseRestrictedConditions wc on wc.WarehouseId = w.Id
							where W.HandleRestrictedProducts =1 and wc.RestrictionType=2 and (ip.Id = wc.ProductId or ip.ProductSubGroupId = wc.ProductSubgroupId or ip.ProductTypeId = wc.ProductTypeId
							or ip.ProductGroupId = wc.ProductGroupId)
							FOR XML PATH(N''), TYPE).value(N'.[1]', N'nvarchar(max)'), 1, 2, N'')

				SELECT	@CodeResult = 999,
						@MessageResult = 'No se logró afectar el Inventario debido a que los siguientes Productos están restringidos: ' + CHAR(13) + CHAR(10) + ISNULL(@Message, '')
				GOTO Cleanup
			END

			----------------------------------PRODUCTOS HABILITADOS---------------------

			IF EXISTS
			(
				SELECT 1
				FROM #TableKardex k
				JOIN Inventory.Warehouse w ON w.Id = k.WarehouseId
				JOIN Inventory.InventoryProduct ip ON ip.Id = k.ProductId
				WHERE w.HandleRestrictedProducts = 1
					AND NOT EXISTS
					(
						SELECT 1
						FROM Inventory.WarehouseRestrictedConditions wc
						WHERE wc.WarehouseId = w.Id
							AND wc.RestrictionType = 1
							AND
							(
								ip.Id = wc.ProductId
								OR ip.ProductSubGroupId = wc.ProductSubgroupId
								OR ip.ProductTypeId = wc.ProductTypeId
								OR ip.ProductGroupId = wc.ProductGroupId
							)
					)
			)
			BEGIN
				SELECT @Message = STUFF((
							SELECT DISTINCT CHAR(13) + CHAR(10) + ' - ' + ip.Code + ' - ' + ip.Name
							FROM #TableKardex k
							join Inventory.Warehouse w on w.Id = k.WarehouseId
							join Inventory.InventoryProduct ip on ip.Id = K.ProductId
							where W.HandleRestrictedProducts =1
							and not exists (
								SELECT 1
								FROM Inventory.WarehouseRestrictedConditions wc
								where wc.WarehouseId = w.Id and wc.RestrictionType=1 and
								(ip.Id = wc.ProductId
								OR ip.ProductSubGroupId= wc.ProductSubgroupId
								OR ip.ProductTypeId = wc.ProductTypeId
								OR ip.ProductGroupId = wc.ProductGroupId)
							)
							FOR XML PATH(N''), TYPE).value(N'.[1]', N'nvarchar(max)'), 1, 2, N'')

				SELECT	@CodeResult = 999,
						@MessageResult = 'No se logró afectar el Inventario debido a que los siguientes Productos no están habilitados: ' + CHAR(13) + CHAR(10) + ISNULL(@Message, '')
				GOTO Cleanup
			END
		END



		/******************************************** ACTUALIZAR REGISTROS *******************************************/

		DECLARE @Rows INT = 1,
				@Id INT = 0,
				@PreviousAmount INT = 0,
				@PreviousAverageCost DECIMAL(20,4) = 0,
				-----------------------
				@ProductRows INT = 1,
				@ProductId INT = 0,
				-----------------------
				@WarehouseRows INT = 1,
				@WarehouseId INT = 0,
				-----------------------
				@BatchserialRows INT = 1,
				@BatchserialId INT = 0

		-- Recorrer productos
		WHILE @ProductRows > 0
		BEGIN
			SELECT TOP 1
				@Rows = 1,
				@Id = Id,
				@PreviousAmount = PreviousAmountProduct + (CASE WHEN AffectAverageCost = 0 THEN 0 ELSE (Quantity * (CASE WHEN MovementType = 1 THEN 1 ELSE -1 END)) END),
				-----------------------
				@ProductId = ProductId,
				-----------------------
				@WarehouseRows = 1,
				@WarehouseId = 0
			FROM #TableKardex
			WHERE ProductId > @ProductId
			ORDER BY ProductId, Id

			SET @ProductRows = @@ROWCOUNT
			IF @ProductRows = 0
				BREAK

			---------------------------------------------  COSTO PROMEDIO ---------------------------------------------

			UPDATE #TableKardex
				SET @PreviousAverageCost = AverageCost = IIF
					(
						AffectAverageCost = 0,
						PreviousAverageCost,
						IIF
						(
							(PreviousAmountProduct + Quantity * (CASE WHEN MovementType = 1 THEN 1 ELSE -1 END)) = 0,
							Value,
							((PreviousAverageCost * PreviousAmountProduct) + (Value * Quantity * (CASE WHEN MovementType = 1 THEN 1 ELSE -1 END))) / (PreviousAmountProduct + Quantity * (CASE WHEN MovementType = 1 THEN 1 ELSE -1 END))
						)
					)
			WHERE Id = @Id

			------------------------------------  ACTUALIZAR REGISTRO POR PRODUCTO ------------------------------------

			WHILE @Rows > 0
			BEGIN
				SELECT TOP 1
					@Id = Id
				FROM #TableKardex
				WHERE ProductId = @ProductId
					AND Id > @Id
				ORDER BY Id

				SET @Rows = @@ROWCOUNT
				IF @Rows = 0
					BREAK

				-------------------------------------------------------------------------------------------------------

				UPDATE #TableKardex
					SET PreviousAmountProduct = @PreviousAmount,
						PreviousAverageCost = @PreviousAverageCost
				WHERE Id = @Id

				SELECT @PreviousAmount = PreviousAmountProduct  + (Quantity * (CASE WHEN MovementType = 1 THEN 1 ELSE -1 END))
				FROM #TableKardex
				WHERE Id = @Id AND AffectAverageCost = 1

				-------------------------------------------  COSTO PROMEDIO -------------------------------------------

				UPDATE #TableKardex
					SET @PreviousAverageCost = AverageCost = IIF
						(
							AffectAverageCost = 0,
							PreviousAverageCost,
							IIF
							(
								(PreviousAmountProduct + Quantity * (CASE WHEN MovementType = 1 THEN 1 ELSE -1 END)) = 0,
								Value,
								((PreviousAverageCost * PreviousAmountProduct) + (Value * Quantity * (CASE WHEN MovementType = 1 THEN 1 ELSE -1 END))) / (PreviousAmountProduct + Quantity * (CASE WHEN MovementType = 1 THEN 1 ELSE -1 END))
							)
						)
				WHERE Id = @Id
			END

			-------------------------------------------  RECORRER ALMACENES -------------------------------------------
			WHILE @WarehouseRows > 0

			BEGIN
				SELECT TOP 1
					@Rows = 1,
					@Id = Id,
					@PreviousAmount = PreviousAmountWarehouse + IIF(AffectAverageCost = 0, 0, (Quantity * (CASE WHEN MovementType = 1 THEN 1 ELSE -1 END))),
					-------------------
					@WarehouseId = WarehouseId,
					-------------------
					@BatchserialRows = 1,
					@BatchserialId = 0
				FROM #TableKardex
				WHERE ProductId = @ProductId
					AND WarehouseId > @WarehouseId
				ORDER BY WarehouseId, Id

				SET @WarehouseRows = @@ROWCOUNT
				IF @WarehouseRows = 0
					BREAK

				----------------------------------- ACTUALIZAR REGISTRO POR ALMACEN -----------------------------------

				WHILE @Rows > 0
				BEGIN
					SELECT TOP 1
						@Id = Id
					FROM #TableKardex
					WHERE ProductId = @ProductId
						AND WarehouseId = @WarehouseId
						AND Id > @Id
					ORDER BY Id

					SET @Rows = @@ROWCOUNT
					IF @Rows = 0
						BREAK

					-------------------------------------------------------------------------------------------------------

					UPDATE #TableKardex
						SET PreviousAmountWarehouse = @PreviousAmount
					WHERE Id = @Id

					SELECT @PreviousAmount = PreviousAmountWarehouse  + (Quantity * (CASE WHEN MovementType = 1 THEN 1 ELSE -1 END))
					FROM #TableKardex
					WHERE Id = @Id AND AffectAverageCost = 1
				END

				-------------------------------------------  RECORRER LOTES -------------------------------------------
				WHILE @BatchserialRows > 0
				BEGIN
					SELECT TOP 1
						@Rows = 1,
						@Id = Id,
						@PreviousAmount = PreviousAmountBatch + IIF(AffectAverageCost = 0, 0, (Quantity * (CASE WHEN MovementType = 1 THEN 1 ELSE -1 END))),
						---------------
						@BatchserialId = BatchSerialId
					FROM #TableKardex
					WHERE ProductId = @ProductId
						AND WarehouseId = @WarehouseId
						AND ISNULL(BatchSerialId, 0) > @BatchserialId
					ORDER BY BatchSerialId, Id

					SET @BatchserialRows = @@ROWCOUNT
					IF @BatchserialRows = 0
						BREAK

					----------------------------------  ACTUALIZAR REGISTRO POR LOTE ----------------------------------

					WHILE @Rows > 0
					BEGIN
						SELECT TOP 1
							@Id = Id
						FROM #TableKardex
						WHERE ProductId = @ProductId
							AND WarehouseId = @WarehouseId
							AND BatchSerialId = @BatchserialId
							AND Id > @Id
						ORDER BY Id

						SET @Rows = @@ROWCOUNT
						IF @Rows = 0
							BREAK

						-------------------------------------------------------------------------------------------------------

						UPDATE #TableKardex
							SET PreviousAmountBatch = @PreviousAmount
						WHERE Id = @Id

						SELECT @PreviousAmount = PreviousAmountBatch  + (Quantity * (CASE WHEN MovementType = 1 THEN 1 ELSE -1 END))
						FROM #TableKardex
						WHERE Id = @Id AND AffectAverageCost = 1
					END
				END
			END
		END

		/************************************************ VALIDACIONES ***********************************************/

		IF NOT EXISTS (SELECT 1 FROM @Kardex.nodes('/Kardex') t(x))
		BEGIN
			SELECT	@CodeResult = 999,
					@MessageResult = 'No se logro afectar el Inventario debido a que no se encontraron detalles.'
			GOTO Cleanup
		END

		IF EXISTS
		(
			SELECT 1
			FROM #TableKardex k
			JOIN Inventory.InventoryProduct p ON k.ProductId = p.Id
			WHERE FinalProductCost IS NULL
		)
		BEGIN
			SELECT @Message = STUFF((
						SELECT DISTINCT CHAR(13) + CHAR(10) + ' - ' + p.Code + ' - ' + p.Name
						FROM #TableKardex k
						JOIN Inventory.InventoryProduct p ON k.ProductId = p.Id
						WHERE FinalProductCost IS NULL
						FOR XML PATH(N''), TYPE).value(N'.[1]', N'nvarchar(max)'), 1, 2, N'')

			SELECT	@CodeResult = 999,
					@MessageResult = 'No se logro afectar el Inventario debido a que los siguientes Productos no poseen un valor en el ultimo costo: ' + CHAR(13) + CHAR(10) + ISNULL(@Message, '')
			GOTO Cleanup
		END

		IF EXISTS
		(
			SELECT 1
			FROM #TableKardex k
			JOIN Inventory.Warehouse w ON k.WarehouseId = w.Id
			WHERE w.VirtualStore = 1
		)
		BEGIN
			SELECT @Message = STUFF((
						SELECT DISTINCT CHAR(13) + CHAR(10) + ' - ' + w.Code + ' - ' + w.Name
						FROM #TableKardex k
						JOIN Inventory.Warehouse w ON k.WarehouseId = w.Id
						WHERE w.VirtualStore = 1
						FOR XML PATH(N''), TYPE).value(N'.[1]', N'nvarchar(max)'), 1, 2, N'')

			SELECT	@CodeResult = 999,
					@MessageResult = 'No se logro afectar el Inventario debido a que los siguientes almacenes son virtuales: ' + CHAR(13) + CHAR(10) + ISNULL(@Message, '')
			GOTO Cleanup
		END

		IF EXISTS
		(
			SELECT 1
			FROM #TableKardex k
			JOIN Inventory.Warehouse w ON k.WarehouseId = w.Id
			WHERE w.CustodyStore = 1
		)
		BEGIN
			SELECT @Message = STUFF((
						SELECT DISTINCT CHAR(13) + CHAR(10) + ' - ' + w.Code + ' - ' + w.Name
						FROM #TableKardex k
						JOIN Inventory.Warehouse w ON k.WarehouseId = w.Id
						WHERE w.CustodyStore = 1
						FOR XML PATH(N''), TYPE).value(N'.[1]', N'nvarchar(max)'), 1, 2, N'')

			SELECT	@CodeResult = 999,
					@MessageResult = 'No se logro afectar el Inventario debido a que los siguientes almacenes son de custodia: ' + CHAR(13) + CHAR(10) + ISNULL(@Message, '')
			GOTO Cleanup
		END

		IF EXISTS
		(
			SELECT 1
			FROM
			(
				SELECT ProductId, WarehouseId, BatchSerialId, SUM(Quantity * (CASE WHEN MovementType = 1 THEN 1 ELSE -1 END)) as Quantity
				FROM #TableKardex
				WHERE AffectAverageCost = 1
				GROUP BY ProductId, WarehouseId, BatchSerialId
			) k
			JOIN Inventory.PhysicalInventory ph  ON ph.ProductId = k.ProductId AND ph.WarehouseId = k.WarehouseId AND ISNULL(ph.BatchSerialId,0) = ISNULL(k.BatchSerialId,0)
			WHERE (ISNULL(ph.Quantity, 0) + k.Quantity) < 0
		)
		BEGIN
			SELECT @Message = STUFF((
						SELECT DISTINCT CHAR(13) + CHAR(10) + ' - ' + CONCAT('Almacén: ', w.Code, ' - ', w.Name) + CONCAT(' - Producto: ', ip.Code, ' - ', ip.Name) + (CASE WHEN bs.Id IS NULL THEN '' ELSE CONCAT(' - Lote: ', bs.BatchCode) END) + CONCAT('. Cantidad Actual: ', ISNULL(ph.Quantity, 0), ' - Cantidad Movimiento: ', ABS(k.Quantity))
						FROM
						(
							SELECT ProductId, WarehouseId, BatchSerialId, SUM(Quantity * (CASE WHEN MovementType = 1 THEN 1 ELSE -1 END)) as Quantity
							FROM #TableKardex
							WHERE AffectAverageCost = 1
							GROUP BY ProductId, WarehouseId, BatchSerialId
						) k
						LEFT JOIN Inventory.PhysicalInventory ph ON ph.ProductId = k.ProductId AND ph.WarehouseId = k.WarehouseId AND ISNULL(ph.BatchSerialId,0) = ISNULL(k.BatchSerialId,0)
						JOIN Inventory.Warehouse w ON k.WarehouseId = w.Id
						JOIN Inventory.InventoryProduct ip ON k.ProductId = ip.Id
						LEFT JOIN Inventory.BatchSerial bs ON k.BatchSerialId = bs.Id
						WHERE (ISNULL(ph.Quantity, 0) + k.Quantity) < 0
						FOR XML PATH(N''), TYPE).value(N'.[1]', N'nvarchar(max)'), 1, 2, N'')

			SELECT	@CodeResult = 999,
					@MessageResult = 'No se logro afectar el Inventario debido a que el inventario de los siguientes productos no pueden quedar negativo: ' + CHAR(13) + CHAR(10) + ISNULL(@Message, '')
			GOTO Cleanup
		END

		IF EXISTS
		(
			SELECT 1
			FROM #TableKardex tk
			WHERE (tk.PreviousAmountProduct + (CASE WHEN AffectAverageCost = 0 THEN 0 ELSE (Quantity * (CASE WHEN MovementType = 1 THEN 1 ELSE -1 END)) END)) < 0
		)
		BEGIN
			SELECT @Message = STUFF((
						SELECT DISTINCT CHAR(13) + CHAR(10) + ' - ' + CONCAT(' - Producto: ', ip.Code, ' - ', ip.Name)
						FROM #TableKardex k
						JOIN Inventory.InventoryProduct ip  ON k.ProductId = ip.Id
						WHERE (PreviousAmountProduct + (CASE WHEN AffectAverageCost = 0 THEN 0 ELSE (Quantity * (CASE WHEN MovementType = 1 THEN 1 ELSE -1 END)) END)) < 0
						FOR XML PATH(N''), TYPE).value(N'.[1]', N'nvarchar(max)'), 1, 2, N'')

			SELECT	@CodeResult = 999,
					@MessageResult = 'No se logro afectar el Inventario debido a que el inventario de los siguientes productos no pueden quedar negativo: ' + CHAR(13) + CHAR(10) + ISNULL(@Message, '')
			GOTO Cleanup
		END

		IF EXISTS
		(
			SELECT 1
			FROM #TableKardex tk
			WHERE (PreviousAmountWarehouse + (CASE WHEN AffectAverageCost = 0 THEN 0 ELSE (Quantity * (CASE WHEN MovementType = 1 THEN 1 ELSE -1 END)) END)) < 0
		)
		BEGIN
			SELECT @Message = STUFF((
						SELECT DISTINCT CHAR(13) + CHAR(10) + ' - ' + CONCAT('Almacén: ', w.Code, ' - ', w.Name) + CONCAT(' - Producto: ', ip.Code, ' - ', ip.Name)
						FROM #TableKardex k
						JOIN Inventory.Warehouse w  ON k.WarehouseId = w.Id
						JOIN Inventory.InventoryProduct ip  ON k.ProductId = ip.Id
						WHERE (PreviousAmountWarehouse + (CASE WHEN AffectAverageCost = 0 THEN 0 ELSE (Quantity * (CASE WHEN MovementType = 1 THEN 1 ELSE -1 END)) END)) < 0
						FOR XML PATH(N''), TYPE).value(N'.[1]', N'nvarchar(max)'), 1, 2, N'')

			SELECT	@CodeResult = 999,
					@MessageResult = 'No se logro afectar el Inventario debido a que el inventario de los siguientes productos no pueden quedar negativo: ' + CHAR(13) + CHAR(10) + ISNULL(@Message, '')
			GOTO Cleanup
		END

		IF EXISTS
		(
			SELECT 1
			FROM #TableKardex tk
			WHERE tk.BatchSerialId IS NOT NULL AND (tk.PreviousAmountBatch + (CASE WHEN AffectAverageCost = 0 THEN 0 ELSE (Quantity * (CASE WHEN MovementType = 1 THEN 1 ELSE -1 END)) END)) < 0
		)
		BEGIN
			SELECT @Message = STUFF((
						SELECT DISTINCT CHAR(13) + CHAR(10) + ' - ' + CONCAT('Almacén: ', w.Code, ' - ', w.Name) + CONCAT(' - Producto: ', ip.Code, ' - ', ip.Name) + (CASE WHEN bs.Id IS NULL THEN '' ELSE CONCAT(' - Lote: ', bs.BatchCode) END)
						FROM #TableKardex k
						JOIN Inventory.Warehouse w  ON k.WarehouseId = w.Id
						JOIN Inventory.InventoryProduct ip  ON k.ProductId = ip.Id
						LEFT JOIN Inventory.BatchSerial bs  ON k.BatchSerialId = bs.Id
						WHERE k.BatchSerialId IS NOT NULL AND (PreviousAmountBatch + (CASE WHEN AffectAverageCost = 0 THEN 0 ELSE (Quantity * (CASE WHEN MovementType = 1 THEN 1 ELSE -1 END)) END)) < 0
						FOR XML PATH(N''), TYPE).value(N'.[1]', N'nvarchar(max)'), 1, 2, N'')

			SELECT	@CodeResult = 999,
					@MessageResult = 'No se logro afectar el Inventario debido a que el inventario de los siguientes productos no pueden quedar negativo: ' + CHAR(13) + CHAR(10) + ISNULL(@Message, '')
			GOTO Cleanup
		END

		-- Valida salidas sin lote especifico (BatchSerialId IS NULL): stock en registro sin lote insuficiente
		IF EXISTS
		(
			SELECT 1
			FROM
			(
				SELECT ProductId, WarehouseId, SUM(Quantity * (CASE WHEN MovementType = 1 THEN 1 ELSE -1 END)) AS Quantity
				FROM #TableKardex
				WHERE AffectAverageCost = 1 AND BatchSerialId IS NULL
				GROUP BY ProductId, WarehouseId
			) k
			WHERE (
				ISNULL(
					(SELECT SUM(ph.Quantity) FROM Inventory.PhysicalInventory ph 
					 WHERE ph.ProductId = k.ProductId AND ph.WarehouseId = k.WarehouseId AND ph.BatchSerialId IS NULL),
					0
				) + k.Quantity
			) < 0
		)
		BEGIN
			SELECT @Message = STUFF((
						SELECT DISTINCT CHAR(13) + CHAR(10) + 'No se logró afectar el Inventario debido a que no existen cantidades suficientes para el Producto ' + ip.Code + ' - ' + ip.Name + ' sin lote especificado'
						FROM
						(
							SELECT ProductId, WarehouseId, SUM(Quantity * (CASE WHEN MovementType = 1 THEN 1 ELSE -1 END)) AS Quantity
							FROM #TableKardex
							WHERE AffectAverageCost = 1 AND BatchSerialId IS NULL
							GROUP BY ProductId, WarehouseId
						) k
						JOIN Inventory.InventoryProduct ip  ON k.ProductId = ip.Id
						WHERE (
							ISNULL(
								(SELECT SUM(ph.Quantity) FROM Inventory.PhysicalInventory ph 
								 WHERE ph.ProductId = k.ProductId AND ph.WarehouseId = k.WarehouseId AND ph.BatchSerialId IS NULL),
								0
							) + k.Quantity
						) < 0
						FOR XML PATH(N''), TYPE).value(N'.[1]', N'nvarchar(max)'), 1, 2, N'')

			SELECT	@CodeResult = 999,
					@MessageResult = ISNULL(@Message, '')
			GOTO Cleanup
		END

		IF EXISTS
		(
			SELECT 1
			FROM #TableKardex tk
			WHERE tk.AverageCost < 0
		)
		BEGIN
			SELECT @Message = STUFF((
						SELECT DISTINCT CHAR(13) + CHAR(10) + CONCAT(' - Producto: ', ip.Code, ' - ', ip.Name)
						FROM #TableKardex k
						JOIN Inventory.InventoryProduct ip  ON k.ProductId = ip.Id
						WHERE k.AverageCost < 0
						FOR XML PATH(N''), TYPE).value(N'.[1]', N'nvarchar(max)'), 1, 2, N'')

			SELECT	@CodeResult = 999,
					@MessageResult = 'No se logro afectar el Inventario debido a que el costo promedio de los siguientes productos no pueden quedar negativo: ' + CHAR(13) + CHAR(10) + ISNULL(@Message, '')
			GOTO Cleanup
		END

		/******************************************* VALIDACIONES POR TIPO *******************************************/

		-- Reversión de Factura de Compra
		IF
		(
			@EntityName = 'DocumentInvoiceProductSales'
			AND EXISTS (SELECT 1 FROM #TableKardex k WHERE k.MovementType = 1)
		)
		BEGIN
			IF EXISTS
			(
				SELECT 1
				FROM #TableKardex t
				LEFT JOIN
				(
					SELECT
						EntityId,
						EntityName,
						WarehouseId,
						ProductId,
						BatchSerialId,
						SUM(Quantity) Quantity
					FROM Inventory.Kardex k 
					GROUP BY EntityId, EntityName, WarehouseId, ProductId, BatchSerialId
				) k ON k.EntityName = @EntityName AND k.EntityId = @EntityId
					AND k.WarehouseId = t.WarehouseId AND k.ProductId = t.ProductId AND ISNULL(k.BatchSerialId, 0) = ISNULL(t.BatchSerialId, 0)
				WHERE t.Quantity <> ISNULL(k.Quantity, 0)
			)
			BEGIN
				SELECT @Message = STUFF((
							SELECT DISTINCT CHAR(13) + CHAR(10) + ' - ' + p.Code + ' - ' + p.Name
							FROM Inventory.InventoryProduct p
							JOIN
							(
								SELECT ISNULL(t.ProductId, k.ProductId) ProductId
								FROM #TableKardex t
								LEFT JOIN
								(
									SELECT
										EntityId,
										EntityName,
										WarehouseId,
										ProductId,
										BatchSerialId,
										SUM(Quantity) Quantity
									FROM Inventory.Kardex k 
									GROUP BY EntityId, EntityName, WarehouseId, ProductId, BatchSerialId
								) k ON k.EntityName = @EntityName AND k.EntityId = @EntityId
									AND k.WarehouseId = t.WarehouseId AND k.ProductId = t.ProductId AND ISNULL(k.BatchSerialId, 0) = ISNULL(t.BatchSerialId, 0)
								WHERE t.Quantity <> ISNULL(k.Quantity, 0)
							) k ON p.Id = k.ProductId
							FOR XML PATH(N''), TYPE).value(N'.[1]', N'nvarchar(max)'), 1, 2, N'')

				SELECT	@CodeResult = 999,
						@MessageResult = 'La cantidad de los siguientes productos es diferente de la facturada: ' + CHAR(13) + CHAR(10) + ISNULL(@Message, '')
				GOTO Cleanup
			END
		END

		/*********************************************  CONTROL DE COSTO *********************************************/

		IF @ControlCost = 1  AND EXISTS
		(
			SELECT 1
			FROM #TableKardex tk
			JOIN Inventory.InventoryProduct ip ON tk.ProductId = ip.Id
			WHERE tk.AffectAverageCost = 1 AND ISNULL(ip.ControlCostPercentage, 0) > 0
				AND IIF
				(
					tk.PreviousAverageCost = 0,
					0,
					(tk.AverageCost - tk.PreviousAverageCost) / tk.PreviousAverageCost
				) * 100 >= ISNULL(ip.ControlCostPercentage, 0)
		)
		BEGIN
			SELECT @Message = STUFF((
						SELECT DISTINCT CHAR(13) + CHAR(10) + ' - ' + ip.Code + ' - ' + ip.Name + ', nuevo costo promedio:  ' + FORMAT(tk.AverageCost, 'C4', 'es-CO')
						FROM #TableKardex tk
						JOIN Inventory.InventoryProduct ip ON tk.ProductId = ip.Id
						WHERE tk.AffectAverageCost = 1 AND ISNULL(ip.ControlCostPercentage, 0) > 0
							AND IIF
							(
								tk.PreviousAverageCost = 0,
								0,
								(tk.AverageCost - tk.PreviousAverageCost) / tk.PreviousAverageCost
							) * 100 >= ISNULL(ip.ControlCostPercentage, 0)
						FOR XML PATH(N''), TYPE).value(N'.[1]', N'nvarchar(max)'), 1, 2, N'')

			SELECT	@CodeResult = 999,
					@MessageResult = 'El costo promedio de los siguientes productos tendrá una variación mayor al establecido: ' + CHAR(13) + CHAR(10) + ISNULL(@Message, '')
			GOTO Cleanup
		END

		/**************************************************  KARDEX **************************************************/

		INSERT INTO [Inventory].[Kardex]
        (
			[DocumentDate],[ThirdPartyId],[ProductId],[WarehouseId],[BatchSerialId],[MovementType],
			[Quantity],[Value],[PreviousCost],[AverageCost],[PreviousAverageCost],[PreviousAmountProduct],[PreviousAmountWarehouse],[PreviousAmountBatch],
			[EntityId],[EntityCode],[EntityName],[ImportedEntityId],[ImportedEntityCode],[ImportedEntityName],
			[AffectInventory],[CreationUser],[CreationDate]
		)
		SELECT	k.DocumentDate,k.ThirdPartyId,k.ProductId,k.WarehouseId,k.BatchSerialId,k.MovementType,
				k.Quantity,k.Value,k.PreviousCost,k.AverageCost,k.PreviousAverageCost,k.PreviousAmountProduct,k.PreviousAmountWarehouse,k.PreviousAmountBatch,
				@EntityId,@EntityCode,@EntityName,k.ImportedEntityId,k.ImportedEntityCode,k.ImportedEntityName,
				k.AffectAverageCost,@User,[Common].[GETDATE]()
		FROM #TableKardex k
		JOIN Inventory.Warehouse w ON k.WarehouseId = w.Id
		WHERE w.ControlStore = 0
		ORDER BY k.Id

		/********************************************* KARDEX DE CONTROL *********************************************/

		INSERT INTO [Inventory].[KardexControl]
        (
			[DocumentDate],[ThirdPartyId],[ProductId],[WarehouseId],[BatchSerialId],[MovementType],
			[Quantity],[PreviousAmountProduct],[PreviousAmountWarehouse],[PreviousAmountBatch],
			[EntityId],[EntityCode],[EntityName],[CreationUser],[CreationDate]
		)
		SELECT	k.DocumentDate,k.ThirdPartyId,k.ProductId,k.WarehouseId,k.BatchSerialId,k.MovementType,
				k.Quantity,k.PreviousAmountProduct,k.PreviousAmountWarehouse,k.PreviousAmountBatch,
				@EntityId,@EntityCode,@EntityName,@User,[Common].[GETDATE]()
		FROM #TableKardex k
		JOIN Inventory.Warehouse w ON k.WarehouseId = w.Id
		WHERE w.ControlStore = 1
		ORDER BY k.Id

		/*************************************************  PRODUCTO *************************************************/

		UPDATE ip
			SET ip.ProductCost = k.AverageCost,
				ip.FinalProductCost = IIF(@EntityName IN ('EntranceVoucher'), k.Value, ip.FinalProductCost),
				ip.LastPurchase = IIF(k.LastPurchase > ip.LastPurchase, k.LastPurchase, ip.LastPurchase),
				ip.LastSale = IIF(k.LastSale > ip.LastSale, k.LastSale, ip.LastSale)
		FROM Inventory.InventoryProduct ip
		JOIN
		(
			SELECT ProductId, MAX(Id) Id
			FROM #TableKardex
			GROUP BY ProductId
		) km ON ip.Id = km.ProductId
		JOIN #TableKardex k ON km.Id = k.Id

		/********************************************* INVENTARIO FISICO *********************************************/

		INSERT INTO Inventory.PhysicalInventory
		(
			WarehouseId,ProductId,BatchSerialId,Quantity
		)
		SELECT DISTINCT k.WarehouseId, k.ProductId, k.BatchSerialId, 0
		FROM #TableKardex k
		LEFT JOIN Inventory.PhysicalInventory ph ON k.ProductId = ph.ProductId AND k.WarehouseId = ph.WarehouseId AND ISNULL(k.BatchSerialId,0) = ISNULL(ph.BatchSerialId,0)
		WHERE ph.Id IS NULL

		UPDATE ph
			SET ph.Quantity = ph.Quantity + k.Quantity
		FROM
		(
			SELECT ProductId, WarehouseId, BatchSerialId, SUM(Quantity * (CASE WHEN MovementType = 1 THEN 1 ELSE -1 END)) as Quantity
			FROM #TableKardex
			WHERE AffectAverageCost = 1
			GROUP BY ProductId, WarehouseId, BatchSerialId
		) k
		LEFT JOIN Inventory.PhysicalInventory ph ON ph.ProductId = k.ProductId AND ph.WarehouseId = k.WarehouseId AND ISNULL(ph.BatchSerialId,0) = ISNULL(k.BatchSerialId,0)

		/************************************************* RESULTADO *************************************************/

		SELECT	@CodeResult = 0,
				@MessageResult = 'Se Afecto correctamente el Kardex y el Inventario Fisico.'
	END TRY
	BEGIN CATCH
		SELECT	@CodeResult = 999,
				@MessageResult = 'SP_SavePhysicalInventoryKardex_Output: ' + ERROR_MESSAGE() + ' - Linea: ' + CAST(ERROR_LINE() AS VARCHAR(50))
	END CATCH
Cleanup:
	DROP TABLE IF EXISTS #TableKardex
	RETURN
END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Procedimiento central de movimientos de inventario físico y registro en kardex. Recibe un XML con los detalles del movimiento (producto, bodega, lote, tipo de movimiento, cantidad y valor), lo descompone en una tabla temporal y sincroniza la fecha del documento con la fuente real según el tipo de transacción: comprobante de entrada, devolución de compra, dispensación farmacéutica, devolución de dispensación, ajuste de inventario, préstamo de mercancía, orden de traslado, factura de venta o facturación básica. Calcula y actualiza el costo promedio ponderado del producto, las cantidades en bodega y por lote, y finalmente registra cada línea en el kardex de inventario. Es el punto de entrada obligatorio para cualquier proceso que genere un movimiento de productos en almacén, garantizando consistencia en valoración y trazabilidad de existencias.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'PROCEDURE', @level1name = N'SP_SavePhysicalInventoryKardex_Output';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'PROCEDURE', @level1name = N'SP_SavePhysicalInventoryKardex_Output';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Aplica movimientos de inventario al stock físico y registra en el kardex (y kardex de control), recalculando costo promedio, cantidades previas y validando restricciones, costos negativos y stocks resultantes.', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_SavePhysicalInventoryKardex_Output';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El XML de movimientos debe contener al menos un nodo /Kardex; en caso contrario se retorna error 999.; Los productos involucrados deben tener FinalProductCost no nulo en Inventory.InventoryProduct.; Los almacenes destino no pueden ser virtuales (VirtualStore=1) ni de custodia (CustodyStore=1).; Si el almacén tiene HandleRestrictedProducts=1, los productos no deben coincidir con condiciones de restricción tipo 2 y deben coincidir con condiciones de habilitación tipo 1 para todos los renglones.; El stock resultante (PhysicalInventory + movimiento) por producto/almacén/lote no puede quedar negativo, ni las cantidades previas (producto, almacén, lote) tras aplicar el movimiento.; El costo promedio resultante (AverageCost) no puede quedar negativo.; Cuando @EntityName=''DocumentInvoiceProductSales'' con MovementType=1 (reversión), las cantidades del kardex enviadas deben coincidir con las cantidades originalmente registradas en Inventory.Kardex para esa entidad.', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_SavePhysicalInventoryKardex_Output';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'El kardex se inserta en Inventory.Kardex sólo para almacenes con ControlStore=0; los de ControlStore=1 van exclusivamente a KardexControl.; El stock físico (PhysicalInventory) sólo se modifica para movimientos con AffectAverageCost=1.; MovementType=1 representa entrada (suma) y cualquier otro valor representa salida (resta).; El costo promedio se recalcula como ((PreviousAvgCost*PreviousAmount)+(Value*Quantity*signo))/(PreviousAmount+Quantity*signo); si el denominador es 0 toma Value; si AffectAverageCost=0 conserva el costo previo.; Nunca se permite dejar stock negativo a nivel producto, producto/almacén o producto/almacén/lote.; Nunca se permite dejar costo promedio negativo.; FinalProductCost del producto sólo se reemplaza con el Value del movimiento cuando la entidad es EntranceVoucher.; LastPurchase y LastSale del producto sólo avanzan; nunca retroceden.; Errores en TRY se capturan y devuelven 999 con el mensaje SQL y línea, sin propagar excepción.', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_SavePhysicalInventoryKardex_Output';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[INSERT] Inventory.Kardex: Inserta una fila por cada movimiento del kardex temporal cuyo almacén tenga ControlStore=0, registrando cantidades, costos previos, costo promedio y datos de la entidad.; [INSERT] Inventory.KardexControl: Inserta una fila por cada movimiento cuyo almacén tenga ControlStore=1 (kardex de control sin información de costos).; [INSERT] Inventory.PhysicalInventory: Crea con Quantity=0 las combinaciones (Warehouse, Product, BatchSerial) presentes en el kardex que aún no existan en el inventario físico.; [UPDATE] Inventory.PhysicalInventory: Suma al stock físico la cantidad neta (positiva si MovementType=1, negativa en otro caso) agrupada por producto/almacén/lote, solo para movimientos con AffectAverageCost=1.; [UPDATE] Inventory.InventoryProduct: Actualiza ProductCost con el AverageCost del último movimiento del producto; FinalProductCost se reemplaza por Value sólo cuando @EntityName=''EntranceVoucher''; LastPurchase y LastSale se actualizan si las nuevas fechas son mayores.; [UPDATE] #TableKardex: Cuando @EntityName=''EntranceVoucher'' y ImportedEntityName IN (''RemissionEntrance'',''ConsignmentInventoryRemission''), AffectAverageCost se fuerza a 0; cuando @EntityName=''EntranceVoucherDevolution'' con AffectAverageCost=0 entrante, se mantiene en 0.; [RETURN_RESULT] RESULT: Retorna @CodeResult=0 con mensaje de éxito; o 999 con mensaje describiendo la causa (XML vacío, sin costo final, almacén virtual/custodia, restricciones, stock/costo negativo, variación excesiva de costo, error en CATCH).', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_SavePhysicalInventoryKardex_Output';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si @EntityName identifica el tipo de documento (EntranceVoucher, EntranceVoucherDevolution, PharmaceuticalDispensing, PharmaceuticalDispensingDevolution, InventoryAdjustment, LoanMerchandise, LoanMerchandiseDevolution, DocumentInvoiceProductSales, BasicBilling, BasicBillingDevolution, TransferOrder, TransferOrderDevolution) → Actualiza DocumentDate (y LastPurchase/LastSale según corresponda) tomándolo de la tabla origen específica de la entidad.; si Almacén con w.ControlStore=1 → El movimiento no afecta costo (Value=ProductCost) y se registra en KardexControl en lugar de Kardex. else Se registra en Inventory.Kardex con cálculo completo de costo promedio.; si @EntityName=''EntranceVoucher'' y ImportedEntityName en (''RemissionEntrance'',''ConsignmentInventoryRemission'') → AffectAverageCost se establece en 0, por lo que el movimiento no altera el costo promedio ni el stock físico.; si Existe almacén con HandleRestrictedProducts=1 → Se valida productos restringidos (RestrictionType=2) y habilitados (RestrictionType=1); si falla cualquiera, retorna 999 listando los productos.; si @EntityName=''DocumentInvoiceProductSales'' y existe MovementType=1 (reversión) → Verifica que las cantidades coincidan con las originalmente facturadas en Inventory.Kardex; si no coinciden, retorna 999.; si @ControlCost=1 y la variación porcentual entre PreviousAverageCost y AverageCost supera ip.ControlCostPercentage → Bloquea la operación con código 999 indicando variación de costo excesiva.', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_SavePhysicalInventoryKardex_Output';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_SavePhysicalInventoryKardex_Output';
-- GO
