-- =============================================
-- Author:		Mariana Gonzalez
-- Create date: 02/12/2025
-- Description:	Valida datos importados (Excel/Copy-Paste) para Traslado en Consignacion.
--              Valida: estructura, almacen destino, producto, cantidad, observaciones y lotes.
--              Retorna: datos validados con costos, lotes (FEFO(First Expired, First Out – primero en vencer, primero en salir)) y existencias.
-- =============================================
CREATE PROCEDURE [Inventory].[SP_SetConsignmentTransferDetailFromFile]
	@XmlObject XML
AS
BEGIN
	SET NOCOUNT ON
	
	DECLARE @SourceWarehouseId INT
	
	-- Tabla temporal para procesar registros importados
	DECLARE @TableXmlObject TABLE
	(
		-- Campos del XML (entrada de usuario)
		RowIndex INT NOT NULL,
		RowColumns INT NOT NULL,
		TargetWarehouseId VARCHAR(500),  
		Product VARCHAR(500),             
		Quantity VARCHAR(500),            
		Description VARCHAR(500),         
		
		-- Campos procesados y validados
		WarehouseId INT,
		ProductId INT,
		QuantityValidated INT,
		HandlesBatch BIT,
		TargetWarehouseCodeName VARCHAR(500),
		ProductCodeName VARCHAR(500),
		ProductCost DECIMAL(18,2),
		BatchSerialId INT,                 -- Lote con mayor stock (FEFO)
		BatchSerialCode VARCHAR(500),
		PhysicalInventoryId INT,
		PhysicalQuantity INT,              -- Total existencias disponibles
		
		-- Control de validacion
		StatusField INT DEFAULT(0),        -- 0=OK, 999=Error
		MessageField VARCHAR(MAX)
	)

	BEGIN TRY
		
		-- Obtener almacen de origen del XML
		SELECT @SourceWarehouseId = t.x.value('SourceWarehouseId[1]','int')
		FROM @XmlObject.nodes('/Data') t(x)
		
		-- Validar que el almacen de origen exista y este activo
		IF NOT EXISTS (SELECT 1 FROM Inventory.Warehouse WHERE Id = @SourceWarehouseId AND Status = 1)
		BEGIN
			INSERT INTO @TableXmlObject(RowIndex, RowColumns, StatusField, MessageField)
			VALUES (0, 0, 999, 'El almacén de origen no existe o se encuentra inactivo')
			
			SELECT	WarehouseId AS TargetWarehouseId,
					ProductId,
					QuantityValidated AS Quantity,
					ProductCodeName,
					TargetWarehouseCodeName AS WarehouseCodeName,
					ProductCost,
					BatchSerialId,
					BatchSerialCode,  
					PhysicalInventoryId,
					PhysicalQuantity,
					Description,
					StatusField,
					MessageField
			FROM @TableXmlObject
			RETURN
		END

		-- Cargar datos del XML a la tabla temporal
		INSERT INTO @TableXmlObject (RowIndex, RowColumns, TargetWarehouseId, Product, Quantity, Description)
		SELECT	t.x.value('RowIndex[1]','int') as RowIndex,
				t.x.value('RowColumns[1]','int') as RowColumns,
				LTRIM(RTRIM(t.x.value('TargetWarehouseId[1]','varchar(500)'))) as TargetWarehouseId,
				LTRIM(RTRIM(t.x.value('Product[1]','varchar(500)'))) as Product,
				LTRIM(RTRIM(t.x.value('Quantity[1]','varchar(500)'))) as Quantity,
				LTRIM(RTRIM(ISNULL(t.x.value('Description[1]','varchar(500)'), ''))) as Description
		FROM @XmlObject.nodes('/Data/Row') t(x)

		-- ============================================
		-- VALIDACION 1: Estructura del archivo (4 columnas obligatorias)
		-- ============================================
		UPDATE tx
			SET tx.StatusField = 999,
				tx.MessageField = CONCAT('El registro ', tx.RowIndex, ' no tiene la estructura válida. Debe tener 4 columnas: Almacén Destino, Producto, Cantidad, Observación')
		FROM @TableXmlObject tx
		WHERE tx.StatusField = 0 AND tx.RowColumns < 4

		-- ============================================
		-- VALIDACION 2: Registros duplicados
		-- ============================================
		UPDATE tx
			SET tx.StatusField = 999,
				tx.MessageField = CONCAT('El registro ', tx.RowIndex, ' se encuentra duplicado')
		FROM @TableXmlObject tx
		JOIN
		(
			SELECT tx.RowIndex
			FROM @TableXmlObject tx
			GROUP BY tx.RowIndex
			HAVING COUNT(1) > 1
		) txd ON tx.RowIndex = txd.RowIndex
		WHERE tx.StatusField = 0

		-- ============================================
		-- VALIDACION 3: Almacen destino (debe ser consignacion activo)
		-- ============================================
		UPDATE tx
			SET tx.StatusField = IIF(txd.MessageField IS NOT NULL, 999, tx.StatusField),
				tx.MessageField = txd.MessageField,
				tx.WarehouseId = txd.WarehouseId,
				tx.TargetWarehouseCodeName = txd.TargetWarehouseCodeName
		FROM @TableXmlObject tx
		JOIN
		(
			SELECT	tx.RowIndex,
					CONCAT('El almacén destino ', tx.TargetWarehouseId, ' del registro ', tx.RowIndex, ' ') + CASE 
						WHEN tx.TargetWarehouseId IS NULL OR LTRIM(RTRIM(tx.TargetWarehouseId)) = '' THEN 'está vacío'
						WHEN w.Id IS NULL THEN 'no existe'
						WHEN w.Status = 0 THEN 'se encuentra inactivo'
						WHEN w.WarehouseConsignment = 0 THEN 'no es un almacén de tipo consignación'
						WHEN w.VirtualStore = 1 THEN 'es un almacén virtual'
						WHEN w.CustodyStore = 1 THEN 'es un almacén de custodia'
						WHEN w.TransitStore = 1 THEN 'es un almacén de tránsito'
					END MessageField,
					w.Id WarehouseId,
					IIF(w.Code IS NOT NULL OR w.Name IS NOT NULL, CONCAT(w.Code, ' - ', w.Name), NULL) TargetWarehouseCodeName
			FROM @TableXmlObject tx
			LEFT JOIN Inventory.Warehouse w ON w.Code = LTRIM(RTRIM(tx.TargetWarehouseId))
			WHERE tx.StatusField = 0
		) txd ON tx.RowIndex = txd.RowIndex

		-- ============================================
		-- VALIDACION 4: Producto (existencias, lotes FEFO, costo, cantidad disponible)
		-- ============================================
		UPDATE tx
			SET tx.StatusField = IIF(txd.MessageField IS NOT NULL, 999, tx.StatusField),
				tx.MessageField = IIF(tx.MessageField IS NOT NULL AND txd.MessageField IS NOT NULL, 
					tx.MessageField + ' | ' + txd.MessageField, 
					IIF(txd.MessageField IS NOT NULL, txd.MessageField, tx.MessageField)),
				tx.ProductId = ISNULL(txd.ProductId, tx.ProductId),
				tx.HandlesBatch = ISNULL(txd.HandlesBatch, tx.HandlesBatch),
				tx.ProductCodeName = ISNULL(txd.ProductCodeName, tx.ProductCodeName),
				tx.ProductCost = ISNULL(txd.ProductCost, tx.ProductCost),        
				tx.BatchSerialId = ISNULL(txd.BatchSerialId, tx.BatchSerialId),
				tx.BatchSerialCode = ISNULL(txd.BatchSerialCode, tx.BatchSerialCode),
				tx.PhysicalInventoryId = ISNULL(txd.PhysicalInventoryId, tx.PhysicalInventoryId),
				tx.PhysicalQuantity = ISNULL(txd.PhysicalQuantity, tx.PhysicalQuantity)
		FROM @TableXmlObject tx
		JOIN
		(
			SELECT	tx.RowIndex,
					CONCAT('El producto ', tx.Product, ' del registro ', tx.RowIndex, ' ') + CASE 
						WHEN tx.Product IS NULL OR LTRIM(RTRIM(tx.Product)) = '' THEN 'está vacío'
						WHEN ip.Id IS NULL THEN 'no existe'
						WHEN ip.Status = 0 THEN 'se encuentra inactivo'
						WHEN NOT EXISTS (
							SELECT 1 
							FROM Inventory.PhysicalInventory pi 
							WHERE pi.ProductId = ip.Id 
								AND pi.WarehouseId = @SourceWarehouseId 
								AND pi.Quantity > 0
						) THEN 'no tiene existencias en el almacén origen'
						WHEN psg.HandlesBatch = 1 AND NOT EXISTS (
							SELECT 1 
							FROM Inventory.PhysicalInventory pi 
							INNER JOIN Inventory.BatchSerial bs ON pi.BatchSerialId = bs.Id
							WHERE pi.ProductId = ip.Id 
								AND pi.WarehouseId = @SourceWarehouseId 
								AND pi.Quantity > 0
								AND bs.ExpirationDate >= CAST(GETDATE() AS DATE)
						) THEN 'maneja lotes pero no tiene lotes disponibles con fecha de vencimiento válida en el almacén origen'
					END MessageField,
					ip.Id ProductId,
					ISNULL(psg.HandlesBatch, 0) HandlesBatch,
					IIF(ip.Code IS NOT NULL OR ip.Name IS NOT NULL, CONCAT(ip.Code, ' - ', ip.Name), NULL) ProductCodeName,
					ISNULL(ip.ProductCost, 0) ProductCost, 
					
					-- Seleccionar lote con MAYOR stock (FEFO)
					CASE 
						WHEN ISNULL(psg.HandlesBatch, 0) = 1 THEN (
							SELECT TOP 1 pi.BatchSerialId
							FROM Inventory.PhysicalInventory pi
							INNER JOIN Inventory.BatchSerial bs ON pi.BatchSerialId = bs.Id
							WHERE pi.ProductId = ip.Id 
								AND pi.WarehouseId = @SourceWarehouseId 
								AND pi.Quantity > 0
								AND bs.ExpirationDate >= CAST(GETDATE() AS DATE)
							ORDER BY pi.Quantity DESC, bs.ExpirationDate ASC 
						)
						ELSE NULL 
					END BatchSerialId,
					
					CASE 
						WHEN ISNULL(psg.HandlesBatch, 0) = 1 THEN (
							SELECT TOP 1 bs.BatchCode
							FROM Inventory.PhysicalInventory pi
							INNER JOIN Inventory.BatchSerial bs ON pi.BatchSerialId = bs.Id
							WHERE pi.ProductId = ip.Id 
								AND pi.WarehouseId = @SourceWarehouseId 
								AND pi.Quantity > 0
								AND bs.ExpirationDate >= CAST(GETDATE() AS DATE)
							ORDER BY pi.Quantity DESC, bs.ExpirationDate ASC
						)
						ELSE NULL
					END BatchSerialCode,

					CASE 
						WHEN ISNULL(psg.HandlesBatch, 0) = 1 THEN (
							SELECT TOP 1 pi.Id
							FROM Inventory.PhysicalInventory pi
							INNER JOIN Inventory.BatchSerial bs ON pi.BatchSerialId = bs.Id
							WHERE pi.ProductId = ip.Id 
								AND pi.WarehouseId = @SourceWarehouseId 
								AND pi.Quantity > 0
								AND bs.ExpirationDate >= CAST(GETDATE() AS DATE)
							ORDER BY pi.Quantity DESC, bs.ExpirationDate ASC
						)
						ELSE NULL
					END PhysicalInventoryId,

					-- Total existencias disponibles
					CASE 
						WHEN ISNULL(psg.HandlesBatch, 0) = 1 THEN (
							SELECT ISNULL(SUM(pi.Quantity), 0)
							FROM Inventory.PhysicalInventory pi
							INNER JOIN Inventory.BatchSerial bs ON pi.BatchSerialId = bs.Id
							WHERE pi.ProductId = ip.Id 
								AND pi.WarehouseId = @SourceWarehouseId 
								AND pi.Quantity > 0
								AND bs.ExpirationDate >= CAST(GETDATE() AS DATE)
						)
						ELSE (
							SELECT ISNULL(SUM(pi.Quantity), 0)
							FROM Inventory.PhysicalInventory pi
							WHERE pi.ProductId = ip.Id 
								AND pi.WarehouseId = @SourceWarehouseId
								AND pi.Quantity > 0
						)
					END PhysicalQuantity

			FROM @TableXmlObject tx
			LEFT JOIN Inventory.InventoryProduct ip ON ip.Code = LTRIM(RTRIM(tx.Product))
			LEFT JOIN Inventory.ProductSubGroup psg ON psg.Id = ip.ProductSubGroupId
		) txd ON tx.RowIndex = txd.RowIndex

		-- ============================================
		-- VALIDACION 5: Cantidad (numerica sin decimales, vs existencias)
		-- ============================================
		UPDATE tx
			SET tx.StatusField = IIF(txd.MessageField IS NOT NULL, 999, tx.StatusField),
				tx.MessageField = IIF(tx.MessageField IS NOT NULL AND txd.MessageField IS NOT NULL, 
					tx.MessageField + ' | ' + txd.MessageField, 
					IIF(txd.MessageField IS NOT NULL, txd.MessageField, tx.MessageField)),
				tx.QuantityValidated = ISNULL(txd.QuantityValidated, tx.QuantityValidated)
		FROM @TableXmlObject tx
		JOIN
		(
			SELECT	tx.RowIndex,
					CONCAT('La cantidad ', tx.Quantity, ' del registro ', tx.RowIndex, ' ') + CASE
						WHEN tx.Quantity IS NULL OR LTRIM(RTRIM(tx.Quantity)) = '' THEN 'está vacía'
						WHEN ISNUMERIC(tx.Quantity) = 0 THEN 'no es numérica'
						WHEN CAST(tx.Quantity AS DECIMAL(18,2)) <> CAST(tx.Quantity AS INT) THEN 'tiene decimales (debe ser un número entero sin decimales)'
						WHEN CAST(tx.Quantity AS INT) <= 0 THEN 'debe ser mayor a cero'
						WHEN tx.ProductId IS NOT NULL AND (
							CASE 
								WHEN tx.HandlesBatch = 1 THEN (
									SELECT ISNULL(SUM(pi.Quantity), 0)
									FROM Inventory.PhysicalInventory pi
									INNER JOIN Inventory.BatchSerial bs ON pi.BatchSerialId = bs.Id
									WHERE pi.ProductId = tx.ProductId 
										AND pi.WarehouseId = @SourceWarehouseId
										AND pi.Quantity > 0
										AND bs.ExpirationDate >= CAST(GETDATE() AS DATE)
								)
								ELSE (
									SELECT ISNULL(SUM(pi.Quantity), 0)
									FROM Inventory.PhysicalInventory pi
									WHERE pi.ProductId = tx.ProductId 
										AND pi.WarehouseId = @SourceWarehouseId
										AND pi.Quantity > 0
								)
							END
						) < CAST(tx.Quantity AS INT) THEN CONCAT('excede las existencias disponibles en el almacén origen (Disponible: ', 
							CAST((
								CASE 
									WHEN tx.HandlesBatch = 1 THEN (
										SELECT ISNULL(SUM(pi.Quantity), 0)
										FROM Inventory.PhysicalInventory pi
										INNER JOIN Inventory.BatchSerial bs ON pi.BatchSerialId = bs.Id
										WHERE pi.ProductId = tx.ProductId 
											AND pi.WarehouseId = @SourceWarehouseId
											AND pi.Quantity > 0
											AND bs.ExpirationDate >= CAST(GETDATE() AS DATE)
									)
									ELSE (
										SELECT ISNULL(SUM(pi.Quantity), 0)
										FROM Inventory.PhysicalInventory pi
										WHERE pi.ProductId = tx.ProductId 
											AND pi.WarehouseId = @SourceWarehouseId
											AND pi.Quantity > 0
									)
								END
							) AS VARCHAR(10)), ')')
					END MessageField,
					IIF(tx.Quantity IS NOT NULL 
						AND ISNUMERIC(tx.Quantity) = 1 
						AND CAST(tx.Quantity AS DECIMAL(18,2)) = CAST(tx.Quantity AS INT)
						AND CAST(tx.Quantity AS INT) > 0
						AND (tx.ProductId IS NULL OR (
							CASE 
								WHEN tx.HandlesBatch = 1 THEN (
									SELECT ISNULL(SUM(pi.Quantity), 0)
									FROM Inventory.PhysicalInventory pi
									INNER JOIN Inventory.BatchSerial bs ON pi.BatchSerialId = bs.Id
									WHERE pi.ProductId = tx.ProductId 
										AND pi.WarehouseId = @SourceWarehouseId
										AND pi.Quantity > 0
										AND bs.ExpirationDate >= CAST(GETDATE() AS DATE)
								)
								ELSE (
									SELECT ISNULL(SUM(pi.Quantity), 0)
									FROM Inventory.PhysicalInventory pi
									WHERE pi.ProductId = tx.ProductId 
										AND pi.WarehouseId = @SourceWarehouseId
										AND pi.Quantity > 0
								)
							END
						) >= CAST(tx.Quantity AS INT)), 
						CAST(tx.Quantity AS INT), NULL) QuantityValidated
			FROM @TableXmlObject tx
		) txd ON tx.RowIndex = txd.RowIndex

		-- ============================================
		-- VALIDACION 6: Observacion (maximo 200 caracteres)
		-- ============================================
		UPDATE tx
			SET tx.StatusField = IIF(txd.MessageField IS NOT NULL, 999, tx.StatusField),
				tx.MessageField = IIF(tx.MessageField IS NOT NULL AND txd.MessageField IS NOT NULL, 
					tx.MessageField + ' | ' + txd.MessageField, 
					IIF(txd.MessageField IS NOT NULL, txd.MessageField, tx.MessageField))
		FROM @TableXmlObject tx
		JOIN
		(
			SELECT	tx.RowIndex,
					CONCAT('La observación del registro ', tx.RowIndex, ' ') + CASE
						WHEN tx.Description IS NOT NULL AND LEN(LTRIM(RTRIM(tx.Description))) > 200 THEN 'excede los 200 caracteres permitidos (máximo 200 caracteres permitidos)'
					END MessageField
			FROM @TableXmlObject tx
			WHERE tx.Description IS NOT NULL 
				AND LEN(LTRIM(RTRIM(tx.Description))) > 200
		) txd ON tx.RowIndex = txd.RowIndex

	END TRY
	BEGIN CATCH
		DELETE FROM @TableXmlObject
		INSERT INTO @TableXmlObject(RowIndex, RowColumns, StatusField, MessageField)
		VALUES (0, 0, 999, ERROR_MESSAGE() + ' - Linea: ' + CAST(ERROR_LINE() AS VARCHAR(5)))
	END CATCH

	-- Retornar resultados validados
	SELECT	WarehouseId AS TargetWarehouseId,
			ProductId,
			QuantityValidated AS Quantity,
			ProductCodeName,
			TargetWarehouseCodeName AS WarehouseCodeName,
			ProductCost,
			BatchSerialId,
			BatchSerialCode, 
			PhysicalInventoryId,
			PhysicalQuantity,
			Description,
			StatusField,
			MessageField
	FROM @TableXmlObject
	ORDER BY RowIndex
END