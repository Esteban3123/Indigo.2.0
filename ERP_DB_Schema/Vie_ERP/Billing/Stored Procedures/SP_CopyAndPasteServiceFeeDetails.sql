

-- =============================================
-- Author:		Andres Alarcon
-- Create date: 2023-05-26
-- Description:	Procedimiento que se encarga de el copyPaste de la rejilla de servicios en el formulario de tarifas de productos y servicios
-- =============================================
CREATE PROCEDURE [Billing].[SP_CopyAndPasteServiceFeeDetails]
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
		Service VARCHAR(500),
		InitialDate VARCHAR(500),
		FinalDate VARCHAR(MAX),
		SalePrice VARCHAR(500),
		Observations VARCHAR(500),
		
		--------------------------------
		ServiceId INT,
		InitialDateOut Datetime,
		FinalDateOut Datetime,
		SalePriceOut DECIMAL(18, 2),
		--------------------------------
		StatusField INT DEFAULT(0), --Estado pendiente de validación
		MessageField VARCHAR(MAX)
	)

	BEGIN TRY

		INSERT INTO @TableXmlObject
		(
			RowIndex, RowColumns, 
			Service,InitialDate, FinalDate,
			SalePrice, Observations
		)
		SELECT	t.x.value('RowIndex[1]','int') as RowIndex,
				t.x.value('RowColumns[1]','int') as RowColumns,
				t.x.value('Service[1]','varchar(500)') as Service,
				t.x.value('InitialDate[1]','varchar(500)') as InitialDate,
				t.x.value('FinalDate[1]','varchar(max)') as FinalDate,
				t.x.value('SalePrice[1]','varchar(500)') as SalePrice,
				t.x.value('Observations[1]','varchar(500)') as Observations
				
		FROM @XmlObject.nodes('/Data/Row') t(x)

		/***********************************************  VALIDACIONES ***********************************************/

		-- Validar el numero de columnas de acuerdo al tipo de documento
		UPDATE tx
			SET tx.StatusField = 999,
				tx.MessageField = CONCAT('El registro ', tx.RowIndex, ' no tiene la estructura válida')
		FROM @TableXmlObject tx
		WHERE tx.StatusField = 0 AND tx.RowColumns < 5

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

		-- Validar Servicio
		UPDATE tx
			SET tx.StatusField = IIF(txd.MessageField IS NOT NULL, 999, tx.StatusField),
				tx.MessageField = txd.MessageField,
				---------------------------------------------------------------
				tx.Service = ISNULL(txd.Service,tx.Service),
				tx.ServiceId = txd.ServiceId
		FROM @TableXmlObject tx
		JOIN
		(
			SELECT	tx.RowIndex,
					CONCAT('El Servicio ', tx.Service, ' del registro ', tx.RowIndex, ' ') + CASE 
						WHEN bc.Id IS NULL THEN 'no existe'
						WHEN bc.Status = 0 THEN 'se encuentra inactivo'
					END MessageField,
					IIF(bc.Code IS NOT NULL OR bc.Name IS NOT NULL,CONCAT(bc.Code, ' - ', bc.Name),NULL) Service,
					bc.Id ServiceId
			FROM @TableXmlObject tx
			LEFT JOIN Billing.BillingConcept bc on bc.Code = tx.Service
			WHERE tx.StatusField = 0
		) txd ON tx.RowIndex = txd.RowIndex
		
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

	SELECT	Service,
			ServiceId,
			Observations,
			InitialDateOut,
			FinalDateOut,
			SalePriceOut,
			--------------------------------
			StatusField, 
			MessageField
	FROM @TableXmlObject
END