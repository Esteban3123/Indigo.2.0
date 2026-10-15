-- =============================================
-- Author:		Miguel Angel Fonseca Castro
-- Create date: 2020-03-22
-- Description:	Procedimiento que se encarga de el Copy & Paste de los detalles de la notas de cartera
-- =============================================
CREATE PROCEDURE [Portfolio].[SP_CopyAndPastePortfolioNoteAccountReceivableAdvance] 
	@XmlParameters as XML,
	@XmlObject as Xml
AS
BEGIN
	SET NOCOUNT ON

	/************************************* VARIABLES ************************************/
	
	--Parametros
	DECLARE @CompanyType INT,
			@NoteType INT,
			@ThirdPartyId INT,
			@Nature INT,
			@CurrencyId INT,
			@OfficialCurrencyId INT

	--Tabla para almacenar los items del listado que viene en el xml y los resultados a devolver
	DECLARE @TableXmlObject TABLE
	(
		RowIndex INT,
		RowColumns INT,
		PortfolioAdvanceCode VARCHAR(500),
		InvoiceNumber VARCHAR(500),		
		Number VARCHAR,
		SpecificPortfolioStatus TINYINT,
		StringValue VARCHAR(500),
		FEConcept TINYINT,
		--------------------------------
		[AccountReceivableId] [int] NULL,
		[AccountReceivableShareId] [int] NULL,
		[MainAccountId] [int] NULL,
		MainAccountNumberName VARCHAR(500) NULL,
		[AccountReceivableAccountingId] [int] NULL,
		[PortfolioAdvanceId] [int] NULL,
		[AdjusmentValue] [numeric](20, 2) NOT NULL DEFAULT(0),
		[Value] [numeric](20, 2) NOT NULL DEFAULT(0),
		[Balance] [numeric](20, 2) NOT NULL DEFAULT(0),
		--------------------------------
		StatusField INT DEFAULT(99), --Estado pendiente de validación
		MessageField VARCHAR(MAX)
	)
	
	BEGIN TRY

		SELECT	@CompanyType = t.x.value('CompanyType[1]','int'),
				@NoteType = t.x.value('NoteType[1]','int'),
				@ThirdPartyId = t.x.value('ThirdPartyId[1]','int'),
				@Nature = t.x.value('Nature[1]','int'),
				@CurrencyId = IIF(t.x.value('CurrencyId[1]','int') ='',NULL,t.x.value('CurrencyId[1]','int'))
		FROM @XmlParameters.nodes('/Data/Row') t(x)

		INSERT INTO @TableXmlObject
			(RowIndex, RowColumns, PortfolioAdvanceCode, InvoiceNumber, Number, SpecificPortfolioStatus, StringValue,FEConcept)
			SELECT	t.x.value('RowIndex[1]','int') as RowIndex,
					t.x.value('RowColumns[1]','int') as RowColumns,
					t.x.value('PortfolioAdvanceCode[1]','varchar(500)') as PortfolioAdvanceCode,
					t.x.value('InvoiceNumber[1]','varchar(500)') as InvoiceNumber,
					t.x.value('ShareNumber[1]','varchar') as ShareNumber,
					IIF(@NoteType IN (1) AND @CompanyType = 1, t.x.value('SpecificPortfolioStatus[1]','TINYINT'), NULL) as SpecificPortfolioStatus,
					t.x.value('Value[1]','varchar(500)') as Value,
					t.x.value('FEConcept[1]','TINYINT') as FEConcept
			FROM @XmlObject.nodes('/Data/Row') t(x)

		-- Validar el numero de columnas de acuerdo al tipo de documento
		UPDATE tx
			SET tx.StatusField = 999,
				tx.MessageField = CONCAT('El registro ', tx.RowIndex, ' no tiene la estructura válida')
		FROM @TableXmlObject tx
		WHERE 
		(
			(
				@NoteType = 1 AND
				(
					(@CompanyType <> 1 AND NOT tx.RowColumns >= 2) 
					OR 
					(@CompanyType = 1 AND NOT tx.RowColumns >= 3)
				)
			)
			OR
			(@NoteType = 2 AND NOT (tx.RowColumns >= 2))
			OR
			(@NoteType = 3 AND NOT (tx.RowColumns >= 2))
		)

		-- Validar que no existan registros duplicados
		UPDATE tx
			SET tx.StatusField = 999,
				tx.MessageField = CONCAT('El registro ', tx.RowIndex, ' se encuentra duplicado')
		FROM @TableXmlObject tx
		JOIN
		(
			SELECT	IIF(@NoteType IN (3), tx.PortfolioAdvanceCode, NULL) PortfolioAdvanceCode,
					IIF(@NoteType IN (1, 2), tx.InvoiceNumber, NULL) InvoiceNumber,
					IIF(@NoteType IN (2), tx.Number, NULL) ShareNumber,
					IIF(@NoteType IN (1) AND @CompanyType = 1, tx.SpecificPortfolioStatus, NULL) SpecificPortfolioStatus
			FROM @TableXmlObject tx
			WHERE tx.StatusField = 99
			GROUP BY	IIF(@NoteType IN (3), tx.PortfolioAdvanceCode, NULL),
						IIF(@NoteType IN (1, 2), tx.InvoiceNumber, NULL),
						IIF(@NoteType IN (2), tx.Number, NULL),
						IIF(@NoteType IN (1) AND @CompanyType = 1, tx.SpecificPortfolioStatus, NULL)
			HAVING COUNT(1) > 1
		) txd ON ISNULL(tx.PortfolioAdvanceCode, '') = ISNULL(txd.PortfolioAdvanceCode, '')
			AND ISNULL(tx.InvoiceNumber, '') = ISNULL(txd.InvoiceNumber, '')
			AND ISNULL(tx.Number, '') = ISNULL(txd.ShareNumber, '')
			AND ISNULL(tx.SpecificPortfolioStatus, '') = ISNULL(txd.SpecificPortfolioStatus, '')
		WHERE tx.StatusField = 99

		-- Validar que el valor ingresado sea un número
		UPDATE tx
			SET tx.StatusField = 999,
				tx.MessageField = CONCAT('El registro ', tx.RowIndex, ' no tiene un valor válido')
		FROM @TableXmlObject tx
		WHERE tx.StatusField = 99 AND 
			(
				ISNUMERIC(tx.StringValue) = 0
				OR
				NOT (CAST(tx.StringValue AS NUMERIC(20, 2)) > 0)
			)

		-- Actualizamos el valor ajustado
		UPDATE tx
			SET tx.AdjusmentValue = CAST(tx.StringValue AS NUMERIC(20, 2))
		FROM @TableXmlObject tx
		WHERE tx.StatusField = 99

		SELECT TOP 1 @OfficialCurrencyId= OfficialCurrencyId FROM GeneralLedger.CompanySettings

		----------------------------------- VALIDACION POR TIPO -----------------------------------

		IF @NoteType = 1 -- Factura total
		BEGIN
		-- Validar la factura ingresada
			UPDATE tx
				SET tx.StatusField = 999,
					tx.MessageField =CASE 
                    					WHEN varabpn.AccountReceivableId IS null THEN CONCAT('La factura del registro ', tx.RowIndex, ' no existe o no esta asignada al tercero seleccionado')
                    					WHEN @Nature = 2 AND tx.AdjusmentValue > ISNULL(varabpn.Balance, 0) THEN CONCAT('El valor del ajuste del registro ', tx.RowIndex, ' es mayor al saldo de la factura')
                    					--WHEN @Nature = 1 AND tx.AdjusmentValue > (ISNULL(varabpn.Value - varabpn.Balance, 0)) THEN CONCAT('El valor del ajuste del registro ', tx.RowIndex, ' supera el valor inicial de la factura')
										WHEN @CurrencyId <> varabpn.CurrencyId THEN CONCAT('La factura del registro ',tx.RowIndex,' está en una moneda diferente a la del documento')
									  END 					
			FROM @TableXmlObject tx
			LEFT JOIN Portfolio.ViewAccountReceivableAccountingByPortfolioNote varabpn 
					ON	@ThirdPartyId = varabpn.ThirdPartyId 
						AND tx.InvoiceNumber = varabpn.InvoiceNumber
						AND (ISNULL(tx.SpecificPortfolioStatus, IIF(@CompanyType = 1, '', varabpn.SpecificPortfolioStatus)) = varabpn.SpecificPortfolioStatus)
			WHERE tx.StatusField = 99 AND 
				(
					varabpn.AccountReceivableId IS NULL
					--OR
					--(@Nature = 1 AND tx.AdjusmentValue > (ISNULL(varabpn.Value - varabpn.Balance, 0)) AND varabpn.InvoiceDocumentType <> 4 AND varabpn.OpeningBalance = 0)
					OR
					(@Nature = 2 AND varabpn.Balance > 0 AND tx.AdjusmentValue > ISNULL(varabpn.Balance, 0)
					OR
					(@CurrencyId <> ISNULL(varabpn.CurrencyId,@OfficialCurrencyId)))
				)

			-- Actualizar la información del registro
			UPDATE tx
				SET tx.StatusField = 0,
					-------------------------------------------------------------------------------
					tx.AccountReceivableId = varabpn.AccountReceivableId,
					tx.AccountReceivableShareId = ars.Id,
					tx.MainAccountId = varabpn.MainAccountId,
					tx.AccountReceivableAccountingId = varabpn.AccountReceivableAccountingId,
					tx.[Value] = varabpn.Value,
					tx.Balance = varabpn.Balance,
					tx.MainAccountNumberName = varabpn.MainAccountNumberName,
					-------------------------------------------------------------------------------
					tx.InvoiceNumber = varabpn.InvoiceNumber,
					tx.SpecificPortfolioStatus = varabpn.SpecificPortfolioStatus
			FROM @TableXmlObject tx
			JOIN Portfolio.ViewAccountReceivableAccountingByPortfolioNote varabpn 
				ON @ThirdPartyId = varabpn.ThirdPartyId 
					AND tx.InvoiceNumber = varabpn.InvoiceNumber
					AND (ISNULL(tx.SpecificPortfolioStatus, IIF(@CompanyType = 1, '', varabpn.SpecificPortfolioStatus)) = varabpn.SpecificPortfolioStatus)
			LEFT JOIN Portfolio.AccountReceivableShare ars ON varabpn.AccountReceivableId = ars.AccountReceivableId
			WHERE tx.StatusField = 99
			AND 
				(
					varabpn.AccountReceivableId IS NULL
					OR
					(@Nature = 1 AND tx.AdjusmentValue > 0)
					OR
					(@Nature = 2 AND varabpn.Balance > 0 )
				)
		END
		ELSE IF @NoteType = 2 -- Factura cuotas
		BEGIN
			-- Validar la factura ingresada
			UPDATE tx
				SET tx.StatusField = 999,
					tx.MessageField = CASE 
                                      	WHEN ar.Id IS NULL THEN CONCAT('La factura del registro ', tx.RowIndex, ' no existe o no esta asignada al tercero seleccionado')
                                      	WHEN ars.Id IS NULL THEN CONCAT('La factura del registro ', tx.RowIndex, ' debe manejar mas de una cuota, o el numero de cuota no existe')
                                      	WHEN @Nature = 2 AND tx.AdjusmentValue > ISNULL(ar.Balance, 0) THEN CONCAT('El valor del ajuste del registro ', tx.RowIndex, ' es mayor al saldo de la cuota de la factura')
										WHEN @Nature = 1 AND tx.AdjusmentValue > (ISNULL(ars.Value - ars.Balance, 0)) THEN CONCAT('El valor del ajuste del registro ', tx.RowIndex, ' supera el valor inicial de la cuota de la factura')
										WHEN @CurrencyId <> ISNULL(ar.CurrencyId,@OfficialCurrencyId) THEN CONCAT('La moneda del registro ',tx.RowIndex,' es diferente a la del documento')
									  END
			FROM @TableXmlObject tx
			LEFT JOIN Portfolio.AccountReceivable ar ON @ThirdPartyId = ar.ThirdPartyId AND tx.InvoiceNumber = ar.InvoiceNumber AND ar.Status = 2
			LEFT JOIN Portfolio.AccountReceivableShare ars ON ar.Id = ars.AccountReceivableId AND ar.NumberShares > 1 AND tx.Number = ars.Number
			WHERE tx.StatusField = 99 AND 
				(
					(ar.Id IS NULL OR ars.Id IS NULL)
					OR
					(@Nature = 1 AND tx.AdjusmentValue > (ISNULL(ars.Value - ars.Balance, 0)))
					OR
					(@Nature = 2 AND tx.AdjusmentValue > ISNULL(ars.Balance, 0))
					OR 
					(@CurrencyId <> ISNULL(ar.CurrencyId,@OfficialCurrencyId))
				)

			-- Actualizar la información del registro
			UPDATE tx
				SET tx.StatusField = 0,
					-------------------------------------------------------------------------------
					tx.AccountReceivableId = ar.Id,
					tx.AccountReceivableShareId = ars.Id,
					tx.AccountReceivableAccountingId = ara.Id,
					tx.[Value] = ars.Value,
					tx.Balance = ars.Balance,
					-------------------------------------------------------------------------------
					tx.InvoiceNumber = ar.InvoiceNumber,
					tx.Number = ars.Number
			FROM @TableXmlObject tx
			JOIN Portfolio.AccountReceivable ar ON @ThirdPartyId = ar.ThirdPartyId AND tx.InvoiceNumber = ar.InvoiceNumber AND ar.Status = 2
			JOIN Portfolio.AccountReceivableShare ars ON ar.Id = ars.AccountReceivableId
			JOIN Portfolio.AccountReceivableAccounting ara ON ar.Id = ara.AccountReceivableId
			WHERE tx.StatusField = 99
		END
		ELSE IF @NoteType = 3 -- Anticipo
		BEGIN
			-- Validar el anticipo ingresado
			UPDATE tx
				SET tx.StatusField = 999,
					tx.MessageField = CASE 
                                      	WHEN pa.Id IS NULL THEN CONCAT('El anticipo del registro ', tx.RowIndex, ' no existe o no esta asignada al tercero seleccionado')
                                      	WHEN NOT (@Nature = ma.Nature OR ISNULL(pa.Balance, 0) > 0) THEN CONCAT('El valor del ajuste del registro ', tx.RowIndex, ' supera el saldo del anticipo')
                                      	WHEN (@CurrencyId <> ISNULL(pa.CurrencyId,@OfficialCurrencyId)) THEN CONCAT('La moneda del registro ',tx.RowIndex,' es diferente a la del documento')
                                      END
			FROM @TableXmlObject tx
			LEFT JOIN Portfolio.PortfolioAdvance pa ON @ThirdPartyId = pa.ThirdPartyId AND tx.PortfolioAdvanceCode = pa.Code AND pa.Status = 2
			LEFT JOIN GeneralLedger.MainAccounts ma ON pa.MainAccountId = ma.Id
			WHERE tx.StatusField = 99 AND 
				(
					(pa.Id IS NULL)
					OR
					NOT (@Nature = ma.Nature OR ISNULL(pa.Balance, 0) > 0)
					OR
					(@CurrencyId <> ISNULL(pa.CurrencyId,@OfficialCurrencyId))
				)

			-- Actualizar la información del registro
			UPDATE tx
				SET tx.StatusField = 0,
					-------------------------------------------------------------------------------
					tx.PortfolioAdvanceId = pa.Id,					
					tx.[Value] = pa.Value,
					tx.Balance = pa.Balance,
					-------------------------------------------------------------------------------
					tx.PortfolioAdvanceCode = pa.Code
			FROM @TableXmlObject tx
			JOIN Portfolio.PortfolioAdvance pa ON @ThirdPartyId = pa.ThirdPartyId AND tx.PortfolioAdvanceCode = pa.Code AND pa.Status = 2
			WHERE tx.StatusField = 99
		END
		----------------------------------- ACTUALIZACION CONCEPTO SALDOS INICIALES-----------------------------------
		UPDATE tx
			SET 
				----------------
				tx.FEConcept = NULL
				----------------
		FROM @TableXmlObject tx
		JOIN Portfolio.AccountReceivable ac ON tx.AccountReceivableId = ac.Id
		WHERE ac.Id = tx.AccountReceivableId AND ac.OpeningBalance = 1 
	END TRY
	BEGIN CATCH
		INSERT INTO @TableXmlObject(StatusField, MessageField)
		VALUES (999, ERROR_MESSAGE() + ' - Linea: ' + CAST(ERROR_LINE() AS VARCHAR(5)))
	END CATCH

	SELECT	AccountReceivableId,
			AccountReceivableShareId,
			MainAccountId,
			AccountReceivableAccountingId,
			PortfolioAdvanceId,			
			AdjusmentValue,
			[Value],
			Balance,
			--------------------------------
			PortfolioAdvanceCode,
			InvoiceNumber,
			Number,
			MainAccountNumberName,
			SpecificPortfolioStatus,
			CASE SpecificPortfolioStatus
			WHEN 1 THEN '1-Sin Radicar'
			WHEN 3 THEN '3-Radicada Entidad'
			WHEN 4 THEN '4-Glosada sin Conciliar'
			WHEN 12 THEN '12-Glosada Conciliada'
			WHEN 15 THEN '15-Cuenta de Dificil Recaudo'
			WHEN 16 THEN '16-Cobro Jurídico'
			WHEN 0 THEN 'N/A'
			END PortfolioStatusName,
			IIF(FEConcept = 0, NULL, FEConcept) FEConcept,
			--------------------------------
			StatusField, 
			MessageField
	FROM @TableXmlObject
	WHERE StatusField <> 99
END