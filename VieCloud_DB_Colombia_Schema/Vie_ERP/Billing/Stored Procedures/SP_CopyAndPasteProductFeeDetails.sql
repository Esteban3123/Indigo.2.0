
-- =============================================
-- Author:		Andres Alarcon
-- Create date: 2023-05-26
-- Description:	Procedimiento que se encarga de el copyPaste de la rejilla de productos en el formulario de tarifas de productos y servicios
-- =============================================
CREATE PROCEDURE [Billing].[SP_CopyAndPasteProductFeeDetails]
	@XmlObject XML
AS
BEGIN
	SET NOCOUNT ON

	/*************************************************** VARIABLES ***************************************************/
	
	--Tabla para almacenar los items del listado que viene en el xml y los resultados a devolver
	DECLARE @TableXmlObject TABLE
	(
		RowIndex INT NOT NULL,
		RowColumns INT NOT NULL,
		Product VARCHAR(500),
		RateType VARCHAR(500),
		PercentageType VARCHAR(500),
		InitialDate VARCHAR(500),
		FinalDate VARCHAR(MAX),
		Percentage VARCHAR(500),
		SalePrice VARCHAR(500),
		Observations VARCHAR(500),
		
		--------------------------------
		ProductId INT,
		RateTypeOut BIT,
		PercentageTypeOut BIT,
		InitialDateOut Datetime,
		FinalDateOut Datetime,
		SalePriceOut DECIMAL(18, 2),
		PercentageOut DECIMAL(5, 2),
		--------------------------------
		StatusField INT DEFAULT(0), --Estado pendiente de validación
		MessageField VARCHAR(MAX)
	)

	BEGIN TRY

		INSERT INTO @TableXmlObject
		(
			RowIndex, RowColumns, 
			Product, RateType, PercentageType,
			InitialDate, FinalDate, Percentage,
			SalePrice, Observations
		)
		SELECT	t.x.value('RowIndex[1]','int') as RowIndex,
				t.x.value('RowColumns[1]','int') as RowColumns,
				t.x.value('Product[1]','varchar(500)') as Product,
				t.x.value('RateType[1]','varchar(500)') as RateType,
				t.x.value('PercentageType[1]','varchar(500)') as PercentageType,
				t.x.value('InitialDate[1]','varchar(500)') as InitialDate,
				t.x.value('FinalDate[1]','varchar(max)') as FinalDate,
				t.x.value('Percentage[1]','varchar(500)') as Percentage,
				t.x.value('SalePrice[1]','varchar(500)') as SalePrice,
				t.x.value('Observations[1]','varchar(500)') as Observations
				
		FROM @XmlObject.nodes('/Data/Row') t(x)

		/***********************************************  VALIDACIONES ***********************************************/

		-- Validar el numero de columnas de acuerdo al tipo de documento
		UPDATE tx
			SET tx.StatusField = 999,
				tx.MessageField = CONCAT('El registro ', tx.RowIndex, ' no tiene la estructura válida')
		FROM @TableXmlObject tx
		WHERE tx.StatusField = 0 AND tx.RowColumns < 6

		-- Validar que no exista dos registros con el mismo indice
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

		-- Validar Producto
		UPDATE tx
			SET tx.StatusField = IIF(txd.MessageField IS NOT NULL, 999, tx.StatusField),
				tx.MessageField = txd.MessageField,
				---------------------------------------------------------------
				tx.Product = ISNULL(txd.Product,tx.Product),
				tx.ProductId = txd.ProductId
		FROM @TableXmlObject tx
		JOIN
		(
			SELECT	tx.RowIndex,
					CONCAT('El producto ', tx.Product, ' del registro ', tx.RowIndex, ' ') + CASE 
						WHEN ip.Id IS NULL THEN 'no existe'
						WHEN ip.Status = 0 THEN 'se encuentra inactivo'
					END MessageField,
					IIF(ip.Code IS NOT NULL OR ip.Name IS NOT NULL,CONCAT(ip.Code, ' - ', ip.Name),NULL) Product,
					ip.Id ProductId
			FROM @TableXmlObject tx
			LEFT JOIN Inventory.InventoryProduct ip ON ip.Code = tx.Product
			WHERE tx.StatusField = 0
		) txd ON tx.RowIndex = txd.RowIndex
		
		-- Validar tipo de tarifa
		UPDATE tx
			SET tx.StatusField = IIF(txd.MessageField IS NOT NULL, 999, tx.StatusField),
				tx.MessageField = txd.MessageField,
				---------------------------------------------------------------
				tx.RateType = ISNULL(txd.RateTypeName,tx.RateType),
				tx.RateTypeOut = IIF(tx.RateType ='1',1,IIF(tx.RateType = '2', 0, NULL))
		FROM @TableXmlObject tx
		JOIN
		(
			SELECT	tx.RowIndex,
					CONCAT('El tipo de tarifa ', tx.RateType, ' del registro ', tx.RowIndex, ' ') + CASE 
						WHEN tx.RateType LIKE '' THEN 'no esta definido'
						WHEN tx.RateType NOT IN('1','2') THEN 'no existe'
					END MessageField,
					IIF(tx.RateType = '1', 'Tarifa fija', IIF(tx.RateType = '2', 'Porcentaje', '')) RateTypeName
			FROM @TableXmlObject tx
			WHERE tx.StatusField = 0
		) txd ON tx.RowIndex = txd.RowIndex

		----Se valida que solo cuando se escoja el tipo de tarifa porcentaje se procede con este trozo de codigo
		IF EXISTS 
		(SELECT 1 from @TableXmlObject tx
		 WHERE tx.RateTypeOut = 0)BEGIN

		---- Validar el tipo de porcentaje
		UPDATE tx
			SET tx.StatusField = IIF(txd.MessageField IS NOT NULL, 999, tx.StatusField),
				tx.MessageField = txd.MessageField,
				---------------------------------------------------------------
				tx.PercentageType = ISNULL(txd.PercentageTypeName,tx.PercentageType),
				tx.PercentageTypeOut = IIF(tx.PercentageType ='1',1,IIF(tx.PercentageType = '2', 0, NULL))
		FROM @TableXmlObject tx
		JOIN
		(
			SELECT	tx.RowIndex,
					CONCAT('El tipo de porcentaje ', tx.PercentageType, ' del registro ', tx.RowIndex, ' ') + CASE 
						WHEN tx.PercentageType LIKE '' THEN 'no esta definido'
						WHEN tx.PercentageType NOT IN('1','2') THEN 'no existe'
					END MessageField,
					IIF(tx.PercentageType = '1', 'Costo Promedio Ponderado ', IIF(tx.PercentageType = '2', 'Ultimo Costo', '')) PercentageTypeName
			FROM @TableXmlObject tx
			WHERE tx.StatusField = 0
		) txd ON tx.RowIndex = txd.RowIndex

		---- Valida el porcentaje
		
		UPDATE tx
			SET tx.StatusField = IIF(txd.MessageField IS NOT NULL, 999, tx.StatusField),
				tx.MessageField = txd.MessageField,
				---------------------------------------------------------------
				tx.PercentageOut = txd.PercentageOut
		FROM @TableXmlObject tx
		JOIN
		(
			SELECT	tx.RowIndex,
					CONCAT('El porcentaje ', tx.SalePrice, ' del registro ', tx.RowIndex, ' ')  + CASE
						WHEN tx.Percentage IS NULL THEN NULL
						WHEN ISNUMERIC(tx.Percentage) = 0 THEN 'no es numerico'
						WHEN CAST(tx.Percentage AS DECIMAL(5,2)) <= 0 THEN 'es negativo o cero'
					END MessageField,
					IIF(tx.Percentage IS NOT NULL AND ISNUMERIC(tx.Percentage) = 1, CAST(tx.Percentage AS DECIMAL(5,2)), NULL) PercentageOut
			FROM @TableXmlObject tx
			WHERE tx.StatusField = 0
		) txd ON tx.RowIndex = txd.RowIndex

		END
		
		-- Validar las observaciones
		UPDATE tx
			SET tx.StatusField = 999,
				tx.MessageField = CONCAT('Las observaciones ', tx.Observations, ' del registro ', tx.RowIndex, ' no es válida')
		FROM @TableXmlObject tx
		WHERE tx.StatusField = 0 AND ISNULL(tx.Observations, '') LIKE '' OR LEN(ISNULL(tx.Observations, '')) > 500

		--Validar las fechas
		UPDATE tx
			SET tx.StatusField = 999,
				tx.MessageField = CONCAT('El formato de fecha ', tx.InitialDate,' del registro ', tx.InitialDate, 'no es válida')
		FROM @TableXmlObject tx
		WHERE tx.StatusField = 0 AND ISDATE(tx.InitialDate) IS NULL OR ISDATE(tx.FinalDate) IS NULL

		UPDATE tx
			SET tx.InitialDateOut = CONVERT(DATETIME, tx.InitialDate, 103),
				tx.FinalDateOut = CONVERT(DATETIME, tx.FinalDate, 103)
		FROM @TableXmlObject tx
		
		-- Validar precio de venta
		UPDATE tx
			SET tx.StatusField = IIF(txd.MessageField IS NOT NULL, 999, tx.StatusField),
				tx.MessageField = txd.MessageField,
				---------------------------------------------------------------
				tx.SalePriceOut = txd.SalePriceOut
		FROM @TableXmlObject tx
		JOIN
		(
			SELECT	tx.RowIndex,
					CONCAT('El precio de venta ', tx.SalePrice, ' del registro ', tx.RowIndex, ' ')  + CASE
						WHEN tx.SalePrice IS NULL THEN NULL
						WHEN ISNUMERIC(tx.SalePrice) = 0 THEN 'no es numerico'
						WHEN CAST(tx.SalePrice AS DECIMAL(18,2)) <= 0 THEN 'es negativo o cero'
					END MessageField,
					IIF(tx.SalePrice IS NOT NULL AND ISNUMERIC(tx.SalePrice) = 1, CAST(tx.SalePrice AS DECIMAL(18,2)), NULL) SalePriceOut
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
			RateType,
			RateTypeOut,
			PercentageType,
			PercentageTypeOut,
			Observations,
			InitialDateOut,
			FinalDateOut,
			SalePriceOut,
			PercentageOut,
			--------------------------------
			StatusField, 
			MessageField
	FROM @TableXmlObject
END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Procedimiento que permite copiar y pegar en lote los detalles de tarifas de productos en el formulario de tarifas de productos y servicios de facturación. Recibe un XML con filas de una grilla (producto, tipo de tarifa, tipo de porcentaje, fechas de vigencia, precio de venta y observaciones), valida cada campo contra el catálogo maestro de productos del inventario (Inventory.InventoryProduct) y reglas de negocio —como existencia y estado activo del producto, tipo de tarifa válida (tarifa fija o porcentaje), tipo de porcentaje (costo promedio ponderado o último costo), fechas y precio de venta—, y retorna los registros con su estado de validación y mensajes de error por fila. Su propósito es agilizar la carga masiva de configuraciones de tarifas de productos, garantizando la integridad de los datos antes de persistirlos.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'PROCEDURE', @level1name = N'SP_CopyAndPasteProductFeeDetails';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'PROCEDURE', @level1name = N'SP_CopyAndPasteProductFeeDetails';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Valida y normaliza un lote de filas en XML proveniente del copy/paste de la rejilla de tarifas de productos, devolviendo cada fila enriquecida con identificadores, valores tipados y mensajes de error por registro, sin persistir datos.', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'SP_CopyAndPasteProductFeeDetails';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El XML de entrada debe seguir la estructura /Data/Row con nodos RowIndex, RowColumns, Product, RateType, PercentageType, InitialDate, FinalDate, Percentage, SalePrice, Observations; Cada fila debe tener al menos 6 columnas (RowColumns >= 6); RowIndex debe ser único por fila dentro del XML; Product debe corresponder al Code de un registro existente y activo en Inventory.InventoryProduct; RateType debe ser ''1'' (Tarifa fija) o ''2'' (Porcentaje); Si RateType = ''2'', PercentageType debe ser ''1'' (Costo Promedio Ponderado) o ''2'' (Último Costo) y Percentage debe ser numérico > 0; InitialDate y FinalDate deben ser fechas válidas en formato 103 (dd/mm/yyyy); SalePrice, cuando se provee, debe ser numérico y > 0; Observations es obligatorio y de longitud <= 500', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'SP_CopyAndPasteProductFeeDetails';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Las filas marcadas con StatusField <> 0 no son re-evaluadas por validaciones posteriores (cada UPDATE filtra por StatusField = 0); StatusField = 999 indica registro inválido; StatusField = 0 indica registro válido/pendiente; RateTypeOut solo puede ser 1 (Tarifa fija), 0 (Porcentaje) o NULL; PercentageTypeOut solo puede ser 1 (Costo Promedio Ponderado), 0 (Último Costo) o NULL; Las fechas se interpretan en formato británico/europeo dd/mm/yyyy (CONVERT con estilo 103); El procedimiento nunca persiste cambios: solo valida y devuelve un result set; Producto debe existir en Inventory.InventoryProduct (por Code) y estar activo (Status <> 0); Las observaciones no pueden estar vacías ni exceder 500 caracteres; Percentage y SalePrice deben ser numéricos y mayores a cero cuando se proveen', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'SP_CopyAndPasteProductFeeDetails';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Producto de inventario; Tarifa de productos y servicios; Tarifa fija; Porcentaje; Costo Promedio Ponderado; Último Costo; Precio de venta; Vigencia (fecha inicial/final)', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'SP_CopyAndPasteProductFeeDetails';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si Existe al menos una fila con RateTypeOut = 0 (tipo de tarifa = Porcentaje) → Se ejecutan validaciones adicionales sobre PercentageType (Costo Promedio Ponderado/Último Costo) y sobre el valor numérico de Percentage else Se omiten las validaciones de tipo de porcentaje y valor de porcentaje; si RateType = ''1'' → Se interpreta como ''Tarifa fija'' y RateTypeOut = 1; si RateType = ''2'' → Se interpreta como ''Porcentaje'' y RateTypeOut = 0; si PercentageType = ''1'' → Se interpreta como ''Costo Promedio Ponderado'' y PercentageTypeOut = 1; si PercentageType = ''2'' → Se interpreta como ''Último Costo'' y PercentageTypeOut = 0; si Ocurre una excepción durante el procesamiento (CATCH) → Se descartan todas las filas validadas y se devuelve una única fila con StatusField=999 y MessageField con ERROR_MESSAGE() y línea del error', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'SP_CopyAndPasteProductFeeDetails';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Inventory.InventoryProduct', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'SP_CopyAndPasteProductFeeDetails';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'SP_CopyAndPasteProductFeeDetails';
-- GO
