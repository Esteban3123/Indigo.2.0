-- =============================================
-- Author:		Miguel Angel Fonseca Castro
-- Create date: 2020-04-05
-- Description:	Procedimiento que se encarga del Copy & Paste de los detalles de traslados a cobro jurídico
-- =============================================
CREATE PROCEDURE [Glosas].[SP_CopyAndPasteTransferJuridicalDebtCollectionDetail] 
	@XmlParameter AS XML,
	@XmlObject AS XML
AS
BEGIN
	SET NOCOUNT ON

	/************************************* VARIABLES ************************************/
	
	--Parametros
	DECLARE @OperatingUnitId INT,
			@CustomerId INT,
			@TransferJuridicalDebtCollectionCId INT,
			@Type INT, /* 1. Copy & Paste - 2. Import - 3. SearchLookupEdit */
			---------------------------
			@UnReconciledInvoice BIT = 0,
			@CustomerNit VARCHAR(20)

	--Tabla para almacenar los items del listado que viene en el xml y los resultados a devolver
	DECLARE @TableXmlObject TABLE
	(
		RowIndex INT,
		RowColumns INT,
		--------------------------------
		PortfolioGlosaId INT,
		AccountReceivableId INT,
		LegalTransferValue MONEY,
		InvoiceNumber VARCHAR(50),
		AccountReceivableDate DATETIME,
		--------------------------------
		StatusField INT DEFAULT(99), 
		MessageField VARCHAR(MAX)
	)

	BEGIN TRY

		SELECT	@OperatingUnitId = t.x.value('OperatingUnitId[1]','int'),
				@CustomerId = t.x.value('CustomerId[1]','int'),
				@TransferJuridicalDebtCollectionCId = t.x.value('TransferJuridicalDebtCollectionCId[1]','int'),
				@Type = t.x.value('Type[1]','int')
		FROM @XmlParameter.nodes('/Data/Row') t(x)

		INSERT INTO @TableXmlObject
			(RowIndex, RowColumns, InvoiceNumber)
			SELECT	t.x.value('RowIndex[1]','int') as RowIndex,
					t.x.value('RowColumns[1]','int') as RowColumns,
					t.x.value('InvoiceNumber[1]','varchar(500)') as InvoiceNumber
			FROM @XmlObject.nodes('/Data/Row') t(x)

		--------------------------------  OBTENGO DATOS NECESARIOS --------------------------------

		-- Se obtiene parametro que indica si se puede trasladar facturas con glosas
		SELECT @UnReconciledInvoice = sp.UnReconciledInvoice
		FROM Portfolio.SettingPortfolio sp
		WHERE sp.OperatingUnitId = @OperatingUnitId

		-- Se obtiene parametro que indica si se puede trasladar facturas con glosas
		SELECT @CustomerNit = c.Nit
		FROM Common.Customer c
		WHERE c.Id = @CustomerId

		--------------------------------------  VALIDACIONES --------------------------------------

		-- Validar el numero de columnas de acuerdo al tipo de documento
		UPDATE tx
			SET tx.StatusField = 999,
				tx.MessageField = CONCAT('El registro ', tx.RowIndex, ' no tiene la estructura válida')
		FROM @TableXmlObject tx
		WHERE NOT (tx.RowColumns >= 1)

		-- Validar que no existan registros duplicados
		UPDATE tx
			SET tx.StatusField = 999,
				tx.MessageField = CONCAT('El registro ', tx.RowIndex, ' se encuentra duplicado')
		FROM @TableXmlObject tx
		JOIN
		(
			SELECT tx.InvoiceNumber
			FROM @TableXmlObject tx
			WHERE tx.StatusField = 99
			GROUP BY tx.InvoiceNumber
			HAVING COUNT(1) > 1
		) txd ON ISNULL(tx.InvoiceNumber, '') = ISNULL(txd.InvoiceNumber, '')
		WHERE tx.StatusField = 99

		-- Validar la factura ingresada
		UPDATE tx
			SET tx.StatusField = 999,
				tx.MessageField = IIF
					(
						ar.Id IS NULL, 
						CONCAT(IIF(@Type IN (3, 4), CONCAT('La factura ', tx.InvoiceNumber), CONCAT('La factura del registro ', tx.RowIndex)), ' no existe o no esta asignada al tercero seleccionado'), 
						IIF
						(
							NOT (ar.Balance > 0),
							CONCAT(IIF(@Type IN (3, 4), CONCAT('La factura ', tx.InvoiceNumber), CONCAT('La factura del registro ', tx.RowIndex)), ' no tiene saldo'),
							CONCAT(IIF(@Type IN (3, 4), CONCAT('La factura ', tx.InvoiceNumber), CONCAT('La factura del registro ', tx.RowIndex)), ' se encuentra ', CASE ar.PortfolioStatus 
																								WHEN 1 THEN 'sin radicar'
																								WHEN 2 THEN 'radicada sin confirmar'
																								WHEN 4 THEN 'radicada sin confirmar'
																								WHEN 16 THEN 'trasladada a cobro jurídico'
																							  END)
						)
					)
		FROM @TableXmlObject tx
		LEFT JOIN Portfolio.AccountReceivable ar ON @CustomerId = ar.CustomerId AND tx.InvoiceNumber = ar.InvoiceNumber AND ar.Balance > 0
		WHERE tx.StatusField = 99 
			AND 
			(
				ar.Id IS NULL
				OR ar.PortfolioStatus IN (1, 2, 4, 16)
			)

		-- Validar las facturas glosadas
		UPDATE tx
			SET tx.StatusField = 999,
				tx.MessageField = CONCAT(IIF(@Type IN (3, 4), CONCAT('La factura ', tx.InvoiceNumber), CONCAT('La factura del registro ', tx.RowIndex)), ' se encuentra ', CASE gpg.State
																										WHEN 1 THEN 'pendiente de confirmar glosa'																										
																										WHEN 4 THEN 'pendiente de confirmar reiteración'
																										WHEN 7 THEN 'pendiente confirmar conciliacón'
																										WHEN 8 THEN 'conciliada'
																										WHEN 9 THEN 'en conciliación parcial'
																										WHEN 13 THEN 'pendiente confirmar pago parcial'
																									END)
		FROM @TableXmlObject tx
		JOIN Glosas.GlosaPortfolioGlosada gpg ON tx.InvoiceNumber = gpg.InvoiceNumber
		WHERE tx.StatusField = 99 
			AND 
			(
				(@UnReconciledInvoice = 0 AND gpg.State IN (1, 4, 7, 8, 9, 13))
				OR
				(@UnReconciledInvoice = 1 AND gpg.State IN (13))
			)

		-- Validar si la factura ya esta agregada en un traslado a cobro jurídico
		UPDATE tx
			SET tx.StatusField = 999,
				tx.MessageField = CONCAT(IIF(@Type IN (3, 4), CONCAT('La factura ', tx.InvoiceNumber), CONCAT('La factura del registro ', tx.RowIndex)), ' ya esta en el traslado a cobro jurídico: ', tjdc.JuridicalTransferConsecutive)
		FROM @TableXmlObject tx
		JOIN Glosas.TransferJuridicalDebtCollectionD tjdcd ON tx.InvoiceNumber = tjdcd.InvoiceNumber
		JOIN Glosas.TransferJuridicalDebtCollectionC tjdc ON tjdcd.TransferJuridicalDebtCollectionCId = tjdc.Id
		WHERE tx.StatusField = 99 AND
			(
				(
					(@Type <> 4 OR tjdc.Id <> @TransferJuridicalDebtCollectionCId)
					AND
					(tjdc.State IN (1, 2) OR (tjdc.State IN (4) AND tjdcd.ReversedUser IS NULL))
				)
			)

		------------------------------ ACTUALIZAR INFORMACIóN DEL REGISTRO ------------------------------

		UPDATE tx
			SET tx.StatusField = 0,
				tx.PortfolioGlosaId = gpg.Id,
				tx.AccountReceivableId = ar.Id,
				tx.LegalTransferValue = 0,
				tx.InvoiceNumber = ar.InvoiceNumber,
				tx.AccountReceivableDate = ar.AccountReceivableDate
		FROM @TableXmlObject tx
		JOIN Portfolio.AccountReceivable ar ON @CustomerId = ar.CustomerId AND tx.InvoiceNumber = ar.InvoiceNumber
		LEFT JOIN Glosas.GlosaPortfolioGlosada gpg 
			ON ar.InvoiceNumber = gpg.InvoiceNumber AND @CustomerNit = gpg.Nit
				AND
				(
					(@UnReconciledInvoice = 0 AND gpg.State IN (1, 4, 7, 8, 10, 13))
					OR
					(@UnReconciledInvoice = 1 AND gpg.State IN (10, 13))
				)
		WHERE tx.StatusField = 99

	END TRY
	BEGIN CATCH
		INSERT INTO @TableXmlObject(StatusField, MessageField)
		VALUES (999, ERROR_MESSAGE() + ' Linea: ' + CAST(ERROR_LINE() AS VARCHAR(5)))
	END CATCH

	SELECT	PortfolioGlosaId,
			AccountReceivableId,
			LegalTransferValue,
			InvoiceNumber,
			AccountReceivableDate,
			--------------------------------
			StatusField, 
			MessageField
	FROM @TableXmlObject
END