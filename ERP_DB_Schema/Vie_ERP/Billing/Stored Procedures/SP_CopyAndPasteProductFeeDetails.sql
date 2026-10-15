
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