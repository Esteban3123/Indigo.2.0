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
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Procedimiento que procesa el copiado y pegado masivo de productos en la grilla del formulario de lista de costos de consignación. Recibe un XML con filas de datos (código de producto y costo nuevo), valida cada fila contra el catálogo maestro de productos de inventario (InventoryProduct) y su tipo de producto (ProductType), verificando existencia, estado activo, unicidad y que el costo sea un valor numérico positivo. Devuelve el resultado fila por fila indicando si cada registro fue válido o el mensaje de error correspondiente, permitiendo al usuario corregir inconsistencias antes de confirmar los costos de consignación.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'PROCEDURE', @level1name = N'SP_CopyAndPasteConsignmentCostListDetail';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'PROCEDURE', @level1name = N'SP_CopyAndPasteConsignmentCostListDetail';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Valida y normaliza filas pegadas (XML) desde una grilla de productos para una lista de costos de consignación, devolviendo cada fila con su producto, costo nuevo y estado/mensaje de validación.', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_CopyAndPasteConsignmentCostListDetail';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El XML debe respetar la estructura /Data/Row con nodos RowIndex, RowColumns, Product y Cost.; Cada fila debe traer al menos 2 columnas (RowColumns >= 2) para considerarse estructura válida.; El código de producto recibido debe coincidir con InventoryProduct.Code para poder resolverse.', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_CopyAndPasteConsignmentCostListDetail';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Las validaciones posteriores solo se aplican a filas con StatusField = 0 (las ya marcadas como erróneas no se re-evalúan).; StatusField=0 representa pendiente de validación; StatusField=999 representa error.; El procedimiento nunca persiste cambios en tablas físicas: solo lee catálogos y devuelve un result set sobre tabla en memoria.; Cuando el producto se resuelve, Product se reemplaza por la concatenación ''Code - Name'' del catálogo.; ProductType devuelto corresponde al campo Class de Inventory.ProductType, no al Id.; Ante cualquier excepción, el resultado se reduce a una única fila con el error capturado.', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_CopyAndPasteConsignmentCostListDetail';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Lista de costos de consignación; Producto de inventario; Tipo/Clase de producto; Costo nuevo; Carga masiva por copy & paste (XML)', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_CopyAndPasteConsignmentCostListDetail';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[INSERT] @TableXmlObject: Por cada nodo /Data/Row del XML se inserta una fila con RowIndex, RowColumns, Product y Cost.; [UPDATE] @TableXmlObject: Si RowColumns < 2 entonces StatusField=999 y mensaje ''El registro X no tiene la estructura válida''.; [UPDATE] @TableXmlObject: Si un mismo RowIndex aparece más de una vez, marca StatusField=999 con mensaje ''El registro X se encuentra duplicado''.; [UPDATE] @TableXmlObject: Si un mismo Product aparece más de una vez, marca StatusField=999 con mensaje ''El producto X está duplicado''.; [UPDATE] @TableXmlObject: Si InventoryProduct.Id es NULL para el código → mensaje ''no existe'' y StatusField=999; si Status=0 → mensaje ''se encuentra inactivo'' y StatusField=999. En cualquier caso resuelve ProductId, ProductType (ProductType.Class) y reformatea Product como ''Code - Name''.; [UPDATE] @TableXmlObject: Si Cost no es numérico (ISNUMERIC=0) marca StatusField=999 con ''no es numerico''; si CAST a DECIMAL(12,2) <= 0 marca ''es negativo o cero''. En caso válido carga CostNew con el valor convertido.; [DELETE] @TableXmlObject: En CATCH se vacía la tabla y se inserta una única fila con RowIndex=0, StatusField=999 y mensaje con ERROR_MESSAGE() + línea.; [RETURN_RESULT] @TableXmlObject: Devuelve Product, ProductId, Cost, CostNew, ProductType, StatusField y MessageField de todas las filas procesadas.', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_CopyAndPasteConsignmentCostListDetail';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si RowColumns < 2 → Marca la fila como inválida (StatusField=999) por estructura.; si RowIndex duplicado entre filas → Marca todas las ocurrencias como duplicadas.; si Product duplicado entre filas → Marca las filas con código repetido como duplicadas.; si Producto no encontrado en InventoryProduct (Id IS NULL) → Mensaje ''no existe'' y StatusField=999.; si Producto encontrado pero Status=0 → Mensaje ''se encuentra inactivo'' y StatusField=999.; si Cost no numérico → Mensaje ''no es numerico'' y StatusField=999. else Si numérico y <=0 → ''es negativo o cero''; en otro caso carga CostNew.', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_CopyAndPasteConsignmentCostListDetail';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Inventory.InventoryProduct; Inventory.ProductType', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_CopyAndPasteConsignmentCostListDetail';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_CopyAndPasteConsignmentCostListDetail';
-- GO
