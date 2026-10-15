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