-- =============================================
-- Author:		Miguel Angel Fonseca Castro
-- Create date: 2020-11-06
-- Description:	Procedimiento que se encarga de el Copy & Paste de los detalles del control de inventarios
-- =============================================
CREATE PROCEDURE [Inventory].[SP_CopyAndPasteInventoryControlDetail]
	@WarehouseId INT,
	@XmlObject XML,
	@UserCode VARCHAR(20)
AS
BEGIN
	SET NOCOUNT ON

	/************************************* VARIABLES ************************************/
	
	DECLARE @DefaultDate DATE
	SET @DefaultDate = DATEFROMPARTS(1900, 1, 1)

	--Tabla para almacenar los items del listado que viene en el xml y los resultados a devolver
	DECLARE @TableXmlObject TABLE
	(
		ProductId INT,
		ProductCode VARCHAR(500),
		ProductName VARCHAR(500),
		HandlesBatch BIT DEFAULT(0),
		BatchSerialId INT,
		BatchSerialCode VARCHAR(500),
		BatchSerialExpirationDate DATE,
		BatchSerialExpirationDateString VARCHAR(500),		
		Quantity INT,
		QuantityString VARCHAR(500),
		InventoryQuantity INT,
		--------------------------------
		StatusField INT DEFAULT(0), 
		MessageField VARCHAR(MAX)
	)

	BEGIN TRY

		INSERT INTO @TableXmlObject
			(ProductCode, BatchSerialCode, BatchSerialExpirationDateString, QuantityString)
			SELECT	t.x.value('ProductCode[1]','varchar(500)') as ProductCode,
					t.x.value('BatchSerialCode[1]','varchar(500)') as BatchSerialCode,
					t.x.value('BatchSerialExpirationDate[1]','varchar(500)') as BatchSerialExpirationDate,
					t.x.value('Quantity[1]','varchar(500)') as QuantityString
			FROM @XmlObject.nodes('/Data') t(x)

			-- VALIDA QUE EL PRODUCTO A IMPORTAR ESTE ACTIVO
			IF EXISTS 
			(
				SELECT 1
				FROM @TableXmlObject txo
				LEFT JOIN Inventory.InventoryProduct ip WITH (NOLOCK) ON ISNULL(txo.ProductCode, '') = ISNULL(ip.Code, '')
				where ip.Status = 0			
			)
			BEGIN
				UPDATE tXml
					SET tXml.StatusField =  999,
						tXml.MessageField = CONCAT('El Producto con Código ',ISNULL(tXml.ProductCode, ''),' Esta Inactivo, quitelo de la lista')
				FROM @TableXmlObject tXml
				LEFT JOIN Inventory.InventoryProduct ip WITH (NOLOCK) ON ISNULL(tXml.ProductCode, '') = ISNULL(ip.Code, '')
				WHERE ip.Status = 0
			END
			------------------------------------------------------------------------------------------------------------------------------------------

		UPDATE tXml
			SET tXml.StatusField = IIF(ip.Id IS NULL, 999, tXml.StatusField),
				tXml.MessageField = IIF(ip.Id IS NULL, 'El Producto con Código ' + ISNULL(tXml.ProductCode, '') + ' no existe', tXml.MessageField),
				-------------------------------------------------------------------------------------------------------
				tXml.ProductId = ip.Id,
				tXml.ProductCode = ip.Code,
				tXml.ProductName = ip.Name,
				tXml.HandlesBatch = ISNULL(psg.HandlesBatch, 0)
		FROM @TableXmlObject tXml
		LEFT JOIN Inventory.InventoryProduct ip WITH (NOLOCK) ON ISNULL(tXml.ProductCode, '') = ip.Code
		LEFT JOIN Inventory.ProductSubGroup psg WITH (NOLOCK) ON ip.ProductSubGroupId = psg.Id
		WHERE tXml.StatusField = 0

		UPDATE tXml
			SET tXml.StatusField = 999,
				tXml.MessageField = 'El codigo del lote ' + ISNULL(tXml.BatchSerialCode, '') + ' tiene una longitud mayor a 50'
		FROM @TableXmlObject tXml
		WHERE tXml.StatusField = 0 AND tXml.HandlesBatch = 1
			AND LEN(ISNULL(tXml.BatchSerialCode, '')) > 50

		UPDATE tXml
			SET tXml.BatchSerialExpirationDateString = IIF(LEN(LTRIM(RTRIM(ISNULL(tXml.BatchSerialExpirationDateString, '')))) = 0, NULL, tXml.BatchSerialExpirationDateString)
		FROM @TableXmlObject tXml
		WHERE tXml.StatusField = 0 AND tXml.HandlesBatch = 1

		UPDATE tXml
			SET tXml.StatusField = IIF(tXml.BatchSerialExpirationDateString IS NULL OR ISDATE(tXml.BatchSerialExpirationDateString) = 1, tXml.StatusField, 999),
				tXml.MessageField = IIF(tXml.BatchSerialExpirationDateString IS NULL OR ISDATE(tXml.BatchSerialExpirationDateString) = 1, tXml.MessageField, 'La fecha de vencimiento ' + tXml.BatchSerialExpirationDateString + ' no es válida'),
				-------------------------------------------------------------------------------------------------------
				tXml.BatchSerialExpirationDate = IIF(tXml.BatchSerialExpirationDateString IS NULL OR ISDATE(tXml.BatchSerialExpirationDateString) = 1, CAST(tXml.BatchSerialExpirationDateString AS DATE), NULL)
		FROM @TableXmlObject tXml
		WHERE tXml.StatusField = 0 AND tXml.HandlesBatch = 1

		INSERT INTO Inventory.BatchSerial
			(ProductId, Type, BatchCode, ExpirationDate, CreationDate, CreationUser)
			SELECT	tXml.ProductId, 2, tXml.BatchSerialCode, tXml.BatchSerialExpirationDate, Common.GETDATE(), @UserCode
			FROM
			(
				SELECT tXml.ProductId, tXml.BatchSerialCode, ISNULL(tXml.BatchSerialExpirationDate, @DefaultDate) BatchSerialExpirationDate
				FROM @TableXmlObject tXml
				WHERE tXml.StatusField = 0 AND tXml.HandlesBatch = 1
				GROUP BY tXml.ProductId, tXml.BatchSerialCode, ISNULL(tXml.BatchSerialExpirationDate, @DefaultDate)
			) tXml
			LEFT JOIN Inventory.BatchSerial bs WITH (NOLOCK) ON tXml.ProductId = bs.ProductId 
				AND tXml.BatchSerialCode = bs.BatchCode 
				AND tXml.BatchSerialExpirationDate = ISNULL(bs.ExpirationDate, @DefaultDate)
			WHERE bs.Id IS NULL

		UPDATE tXml
			SET tXml.BatchSerialId = bs.Id,
				tXml.BatchSerialCode = bs.BatchCode,
				tXml.BatchSerialExpirationDate = bs.ExpirationDate
		FROM @TableXmlObject tXml
		JOIN Inventory.BatchSerial bs WITH (NOLOCK)
			ON tXml.ProductId = bs.ProductId 
				AND tXml.BatchSerialCode = bs.BatchCode 
				AND ISNULL(tXml.BatchSerialExpirationDate, @DefaultDate) = ISNULL(bs.ExpirationDate, @DefaultDate)
		WHERE tXml.StatusField = 0 AND tXml.HandlesBatch = 1

		UPDATE tXml
			SET tXml.StatusField = IIF(ISNUMERIC(tXml.QuantityString) = 1 AND CAST(tXml.QuantityString AS INT) >= 0, tXml.StatusField, 999),
				tXml.MessageField = IIF(ISNUMERIC(tXml.QuantityString) = 1 AND CAST(tXml.QuantityString AS INT) >= 0, tXml.MessageField, 'El Producto con Código ' + ISNULL(tXml.ProductCode, '') + IIF(tXml.HandlesBatch = 0, '', ' y Lote ' + ISNULL(tXml.BatchSerialCode, '')) + ' no tiene una cantidad válida'),
				-------------------------------------------------------------------------------------------------------
				tXml.Quantity = IIF(ISNUMERIC(tXml.QuantityString) = 1, CAST(tXml.QuantityString AS INT), NULL)
		FROM @TableXmlObject tXml
		WHERE tXml.StatusField = 0

		UPDATE tXml
			SET tXml.InventoryQuantity = ISNULL(phy.Quantity, 0)
		FROM @TableXmlObject tXml
		LEFT JOIN Inventory.PhysicalInventory phy WITH (NOLOCK)
			ON tXml.ProductId = phy.ProductId
				AND @WarehouseId = phy.WarehouseId
				AND ISNULL(tXml.BatchSerialId, 0) = ISNULL(phy.BatchSerialId, 0)
		WHERE tXml.StatusField = 0
	END TRY
	BEGIN CATCH
		DELETE FROM  @TableXmlObject

		INSERT INTO @TableXmlObject(StatusField, MessageField)
		VALUES (999, ERROR_MESSAGE() + ' Linea: ' + CAST(ERROR_LINE() AS VARCHAR(5)))
	END CATCH

	SELECT *
	FROM @TableXmlObject
END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Procedimiento que permite copiar y pegar el detalle de un control de inventario a partir de un listado de productos enviado en formato XML. Valida que cada producto exista y esté activo en el catálogo maestro de inventario, verifica que los códigos de lote y fechas de vencimiento sean válidos (creando lotes nuevos si no existen en BatchSerial), y confirma que las cantidades ingresadas sean numéricas y no negativas. Retorna el resultado de cada línea indicando si fue procesada correctamente o el motivo del error, facilitando la carga masiva de conteos o movimientos de inventario en bodegas.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'PROCEDURE', @level1name = N'SP_CopyAndPasteInventoryControlDetail';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'PROCEDURE', @level1name = N'SP_CopyAndPasteInventoryControlDetail';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Valida y prepara una lista de ítems (producto, lote, fecha de vencimiento y cantidad) recibida en XML para pegarla en un control de inventarios, marcando errores por ítem y creando los lotes inexistentes, devolviendo además la existencia física por bodega.', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_CopyAndPasteInventoryControlDetail';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El XML de entrada debe tener nodos /Data con ProductCode, BatchSerialCode, BatchSerialExpirationDate y Quantity; Debe proporcionarse una bodega válida para consultar el inventario físico; Debe proporcionarse un usuario que será registrado como creador de los lotes nuevos', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_CopyAndPasteInventoryControlDetail';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Cada validación posterior solo se aplica sobre ítems con StatusField = 0 (no marcados previamente como erróneos); Solo se insertan lotes nuevos en Inventory.BatchSerial cuando no exista ya uno con misma combinación ProductId + BatchCode + ExpirationDate; Los lotes creados por este proceso se registran con Type = 2; Las fechas de vencimiento nulas se normalizan a 1900-01-01 al comparar/insertar lotes; El código de lote no puede exceder 50 caracteres cuando el producto maneja lote; La cantidad debe ser numérica y mayor o igual a cero; InventoryQuantity refleja la existencia física en la bodega indicada para el producto y lote (0 si no hay registro)', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_CopyAndPasteInventoryControlDetail';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'producto de inventario; lote; fecha de vencimiento; bodega; inventario físico; control de inventarios; subgrupo de producto', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_CopyAndPasteInventoryControlDetail';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[INSERT] Inventory.BatchSerial: Cuando un ítem es válido (StatusField=0) y el producto maneja lote (HandlesBatch=1), se inserta un nuevo lote con Type=2 si no existe ya un BatchSerial con mismo ProductId, BatchCode y ExpirationDate (normalizando NULL a 1900-01-01); [RETURN_RESULT] Inventory.BatchSerial: Al final retorna el contenido de la tabla temporal con los ítems validados, sus IDs resueltos, mensajes de error y la cantidad en inventario físico; [RETURN_RESULT] @TableXmlObject: Si ocurre una excepción, se vacía la tabla temporal y se devuelve una única fila con StatusField=999 y el mensaje y línea del error', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_CopyAndPasteInventoryControlDetail';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si Existe al menos un ítem cuyo ProductCode corresponde a un producto con Status = 0 (inactivo) → Marca esos ítems con StatusField=999 y mensaje indicando que el producto está inactivo; si El producto del ítem maneja lote (HandlesBatch = 1) → Aplica validaciones adicionales: longitud de BatchSerialCode ≤ 50, fecha de vencimiento válida (ISDATE) o nula, y registra/recupera el lote en Inventory.BatchSerial else Omite validaciones de lote y no inserta en BatchSerial; si QuantityString no es numérico o es negativo → Marca StatusField=999 con mensaje de cantidad no válida (incluyendo lote si HandlesBatch=1); si Ocurre cualquier error en el TRY → Vacía la tabla de resultados e inserta una sola fila con StatusField=999 y el ERROR_MESSAGE + línea', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_CopyAndPasteInventoryControlDetail';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Inventory.InventoryProduct; Inventory.ProductSubGroup; Inventory.BatchSerial; Inventory.PhysicalInventory', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_CopyAndPasteInventoryControlDetail';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_CopyAndPasteInventoryControlDetail';
-- GO
