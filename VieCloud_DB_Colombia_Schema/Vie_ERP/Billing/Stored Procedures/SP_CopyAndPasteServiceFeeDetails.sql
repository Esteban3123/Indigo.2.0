

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
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Procedimiento que procesa una operación de copiar y pegar filas en la grilla de tarifas de servicios, dentro del formulario de configuración de tarifas de productos y servicios de facturación. Recibe un objeto XML con múltiples filas, cada una con código de servicio, fechas de vigencia (inicial y final), precio de venta y observaciones, y ejecuta validaciones de estructura, duplicidad de índices, existencia y estado activo del concepto de facturación (consultando Billing.BillingConcept por código), formato de fechas, validez de observaciones y precio de venta numérico y positivo. Retorna el listado procesado con el identificador del concepto de facturación resuelto, las fechas y precio convertidos a sus tipos nativos, y para cada fila un estado y mensaje indicando si la validación fue exitosa o contiene errores, permitiendo al front-end informar al usuario qué registros pegados son válidos y cuáles deben corregirse antes de guardar la tarifa.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'PROCEDURE', @level1name = N'SP_CopyAndPasteServiceFeeDetails';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'PROCEDURE', @level1name = N'SP_CopyAndPasteServiceFeeDetails';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Validar y normalizar filas pegadas (copy/paste) de tarifas de servicios recibidas en XML, resolviendo el servicio contra el catálogo de conceptos de facturación y devolviendo cada fila con su estado y mensaje de error.', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'SP_CopyAndPasteServiceFeeDetails';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El parámetro XML debe seguir la estructura /Data/Row con nodos RowIndex, RowColumns, Service, InitialDate, FinalDate, SalePrice y Observations; Las fechas deben venir en formato convertible mediante CONVERT(DATETIME, ..., 103) (dd/mm/yyyy); El campo Service del XML debe corresponder al Code de Billing.BillingConcept para poder resolverse', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'SP_CopyAndPasteServiceFeeDetails';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'No se persisten cambios en tablas físicas: solo valida y devuelve resultados sobre una tabla temporal; StatusField=0 representa pendiente de validación; StatusField=999 indica error en la fila; Las fechas se interpretan en formato británico/europeo (style 103, dd/mm/yyyy) al convertirse a DATETIME; Solo se aplican validaciones subsiguientes a filas con StatusField=0 (no se sobrescriben errores previos en validaciones posteriores que filtran por StatusField=0); El servicio se resuelve cruzando BillingConcept.Code = Service del XML; si existe, el campo Service se normaliza a ''Code - Name''; Ante cualquier excepción, se descartan los registros y se devuelve una única fila con RowIndex=0 y el mensaje de error junto con la línea', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'SP_CopyAndPasteServiceFeeDetails';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Concepto de facturación / servicio facturable; Tarifa de servicios; Precio de venta; Vigencia de tarifa (fecha inicial y final)', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'SP_CopyAndPasteServiceFeeDetails';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] @TableXmlObject: Devuelve por cada fila del XML: Service normalizado, ServiceId, Observations, InitialDateOut, FinalDateOut, SalePriceOut, StatusField y MessageField; [RAISERROR] @TableXmlObject: En el CATCH se inserta una fila única con RowIndex=0, StatusField=999 y MessageField = ERROR_MESSAGE() + '' - Linea: '' + ERROR_LINE()', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'SP_CopyAndPasteServiceFeeDetails';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si RowColumns < 5 → Marca el registro como inválido (StatusField=999) por estructura no válida; si Existen múltiples filas con el mismo RowIndex → Marca el registro como duplicado (StatusField=999); si BillingConcept.Id IS NULL al cruzar por Code → Marca el servicio como ''no existe'' else Si BillingConcept.Status = 0 marca como ''se encuentra inactivo''; si Observations vacío o longitud > 500 → Marca observación inválida (StatusField=999); si ISDATE(InitialDate) IS NULL OR ISDATE(FinalDate) IS NULL → Marca formato de fecha inválido (StatusField=999); si SalePrice no es numérico → Mensaje ''no es numerico'' else Si CAST(SalePrice AS DECIMAL) <= 0 mensaje ''es negativo o cero''', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'SP_CopyAndPasteServiceFeeDetails';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Billing.BillingConcept', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'SP_CopyAndPasteServiceFeeDetails';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'SP_CopyAndPasteServiceFeeDetails';
-- GO
