-- =============================================
-- Author:		Oscar Astudillo Reyes
-- Create date: 2024-02-20
-- Description:	Procedimiento que se encarga de el copyPaste de la rejilla de productos en el formulario de lista de costos consignacion
-- =============================================

CREATE PROCEDURE [Inventory].[SP_CopyAndPasteConsignmentCostListDetail]			  
	@XmlObject XML
AS
BEGIN
	SET NOCOUNT ON

	/*************************************************** VARIABLES ***************************************************/
	
	--Tabla para almacenar los items del listado que viene en el xml y los resultados a devolver
	DECLARE @TableXmlObject TABLE
	(
		------ Campos que vienen en el Xml
		RowIndex INT NOT NULL,
		RowColumns INT NOT NULL,
		Product VARCHAR(500),
		Cost VARCHAR(500),

		-------------------------------- Campos a  devolver con valores
		ProductId INT,
		CostNew DECIMAL(12, 2),
		ProductType TINYINT,
		--------------------------------
		StatusField INT DEFAULT(0), --Estado pendiente de validación
		MessageField VARCHAR(MAX)
	)
	BEGIN TRY

		INSERT INTO @TableXmlObject
		(
			RowIndex, RowColumns, 
			Product,Cost
		)
		SELECT	t.x.value('RowIndex[1]','int') as RowIndex,
				t.x.value('RowColumns[1]','int') as RowColumns,
				t.x.value('Product[1]','varchar(500)') as Product,
				t.x.value('Cost[1]','varchar(500)') as Cost
		FROM @XmlObject.nodes('/Data/Row') t(x)

		/***********************************************  VALIDACIONES ***********************************************/

		-- Validar el numero de columnas del archivo
		UPDATE tx
			SET tx.StatusField = 999,
				tx.MessageField = CONCAT('El registro ', tx.RowIndex, ' no tiene la estructura válida')
		FROM @TableXmlObject tx
		WHERE tx.StatusField = 0 AND tx.RowColumns < 2

		-- Validar que no exista dos registros con el mismo indice (numero de fila)
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

		--Valida duplicidad de codigo del producto
		UPDATE tx
		SET tx.StatusField = 999,
			tx.MessageField = 'El producto ' + tx.Product + ' está duplicado'
		FROM @TableXmlObject tx
		JOIN (
			SELECT Product
			FROM @TableXmlObject
			GROUP BY Product
			HAVING COUNT(Product) > 1
		) txd ON tx.Product = txd.Product
		WHERE tx.StatusField = 0

		-- Validar Producto
		UPDATE tx
			SET tx.StatusField = IIF(txd.MessageField IS NOT NULL, 999, tx.StatusField),
				tx.MessageField = txd.MessageField,
				---------------------------------------------------------------
				tx.Product = ISNULL(txd.Product,tx.Product),
				tx.ProductId = txd.ProductId,
				tx.ProductType = txd.ProductType
		FROM @TableXmlObject tx
		JOIN
		(
			SELECT	tx.RowIndex,
					CONCAT('El producto ', tx.Product, ' del registro ', tx.RowIndex, ' ') + CASE 
						WHEN ip.Id IS NULL THEN 'no existe'
						WHEN ip.Status = 0 THEN 'se encuentra inactivo'
					END MessageField,
					IIF(ip.Code IS NOT NULL OR ip.Name IS NOT NULL,CONCAT(ip.Code, ' - ', ip.Name),NULL) Product,
					pt.Class ProductType,
					ip.Id ProductId
			FROM @TableXmlObject tx
			LEFT JOIN Inventory.InventoryProduct ip ON ip.Code = tx.Product
			LEFT JOIN Inventory.ProductType pt ON pt.Id = ip.ProductTypeId
			WHERE tx.StatusField = 0
		) txd ON tx.RowIndex = txd.RowIndex

		-- Validar el costo nuevo
		UPDATE tx
			SET tx.StatusField = IIF(txd.MessageField IS NOT NULL, 999, tx.StatusField),
				tx.MessageField = txd.MessageField,
				---------------------------------------------------------------
				tx.CostNew = txd.CostNew
		FROM @TableXmlObject tx
		JOIN
		(
			SELECT	tx.RowIndex,
					CONCAT('El costo nuevo', tx.Cost, ' del registro ', tx.RowIndex, ' ')  + CASE
						WHEN tx.Cost IS NULL THEN NULL
						WHEN ISNUMERIC(tx.Cost) = 0 THEN 'no es numerico'
						WHEN CAST(tx.Cost AS DECIMAL(12,2)) <= 0 THEN 'es negativo o cero'
					END MessageField,
					IIF(tx.Cost IS NOT NULL AND ISNUMERIC(tx.Cost) = 1, CAST(tx.Cost AS DECIMAL(12,2)), NULL) CostNew
			FROM @TableXmlObject tx
			WHERE tx.StatusField = 0
		) txd ON tx.RowIndex = txd.RowIndex
		END TRY

	BEGIN CATCH
		DELETE FROM @TableXmlObject
		INSERT INTO @TableXmlObject(RowIndex, RowColumns,StatusField, MessageField)
		VALUES (0, 0, 999, ERROR_MESSAGE() + ' - Linea: ' + CAST(ERROR_LINE() AS VARCHAR(5)))
	END CATCH

	SELECT	Product,
			ProductId,
			Cost,
			CostNew,
			ProductType,
			--------------------------------
			StatusField, 
			MessageField
	FROM @TableXmlObject
END