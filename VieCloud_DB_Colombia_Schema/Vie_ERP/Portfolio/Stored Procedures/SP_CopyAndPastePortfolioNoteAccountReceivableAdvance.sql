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
			@OfficialCurrencyId INT,
			@RoundTolerance DECIMAL(18,2) = 0.01

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

		-- Obtener tolerancia de redondeo basada en la moneda del documento
		SELECT @RoundTolerance = Common.GetRoundTolerance(c.RoundingType)
		FROM Common.Currency c 
		WHERE c.Id = ISNULL(@CurrencyId, @OfficialCurrencyId)
		SET @RoundTolerance = ISNULL(@RoundTolerance, 0.01)

		----------------------------------- VALIDACION POR TIPO -----------------------------------

		IF @NoteType = 1 -- Factura total
		BEGIN
		-- Validar la factura ingresada
			UPDATE tx
				SET tx.StatusField = 999,
					tx.MessageField =CASE 
                    					WHEN varabpn.AccountReceivableId IS null THEN CONCAT('La factura del registro ', tx.RowIndex, ' no existe o no esta asignada al tercero seleccionado')
                    					WHEN @Nature = 2 AND (tx.AdjusmentValue - ISNULL(varabpn.Balance, 0)) > @RoundTolerance THEN CONCAT('El valor del ajuste del registro ', tx.RowIndex, ' es mayor al saldo de la factura')
                    					--WHEN @Nature = 1 AND tx.AdjusmentValue > (ISNULL(varabpn.Value - varabpn.Balance, 0)) THEN CONCAT('El valor del ajuste del registro ', tx.RowIndex, ' supera el valor inicial de la factura')
										WHEN @CurrencyId <> varabpn.CurrencyId THEN CONCAT('La factura del registro ',tx.RowIndex,' está en una moneda diferente a la del documento')
									  END 					
			FROM @TableXmlObject tx
			LEFT JOIN Portfolio.ViewAccountReceivableAccountingByPortfolioNote varabpn 
					ON	@ThirdPartyId = varabpn.ThirdPartyId 
						AND tx.InvoiceNumber = varabpn.InvoiceNumber
					AND (
						(@CompanyType = 1 AND ISNULL(tx.SpecificPortfolioStatus, '') = varabpn.SpecificPortfolioStatus)
						OR
						(@CompanyType <> 1 AND varabpn.SpecificPortfolioStatus =
							CASE WHEN varabpn.InvoicePortfolioStatus IN (1, 3, 4, 12, 15, 16)
								THEN varabpn.InvoicePortfolioStatus
								ELSE varabpn.SpecificPortfolioStatus END)
					)
			WHERE tx.StatusField = 99 AND 
				(
					varabpn.AccountReceivableId IS NULL
					--OR
					--(@Nature = 1 AND tx.AdjusmentValue > (ISNULL(varabpn.Value - varabpn.Balance, 0)) AND varabpn.InvoiceDocumentType <> 4 AND varabpn.OpeningBalance = 0)
					OR
					(@Nature = 2 AND varabpn.Balance > 0 AND (tx.AdjusmentValue - ISNULL(varabpn.Balance, 0)) > @RoundTolerance
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
					AND (
						(@CompanyType = 1 AND ISNULL(tx.SpecificPortfolioStatus, '') = varabpn.SpecificPortfolioStatus)
						OR
						(@CompanyType <> 1 AND varabpn.SpecificPortfolioStatus =
							CASE WHEN varabpn.InvoicePortfolioStatus IN (1, 3, 4, 12, 15, 16)
								THEN varabpn.InvoicePortfolioStatus
								ELSE varabpn.SpecificPortfolioStatus END)
					)
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
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Procedimiento que permite copiar y pegar masivamente los detalles de notas de cartera (débito o crédito) sobre cuentas por cobrar, anticipos y cuotas, recibiendo los datos en formato XML. Valida la estructura, duplicados, valores numéricos y consistencia de cada registro según el tipo de nota (nota a factura total, nota a cuota/share, o nota a anticipo) y el tipo de empresa, consultando la configuración de moneda oficial en contabilidad (CompanySettings) y la vista de contabilización de notas de cartera (ViewAccountReceivableAccountingByPortfolioNote). Se usa en el módulo de cartera para aplicar ajustes en bloque sobre facturas, anticipos y cuotas de cuentas por cobrar, garantizando que cada ítem pertenezca al tercero correcto, esté en la moneda del documento y no supere el saldo disponible.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'PROCEDURE', @level1name = N'SP_CopyAndPastePortfolioNoteAccountReceivableAdvance';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'PROCEDURE', @level1name = N'SP_CopyAndPastePortfolioNoteAccountReceivableAdvance';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Valida y enriquece, a partir de un XML pegado por el usuario, los detalles de notas de cartera (factura total, factura por cuotas o anticipo) verificando existencia, tercero, saldo, moneda y duplicados, y devuelve cada fila con su estado y mensaje.', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'PROCEDURE', @level1name=N'SP_CopyAndPastePortfolioNoteAccountReceivableAdvance';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'@XmlParameters debe contener un nodo /Data/Row con CompanyType, NoteType, ThirdPartyId, Nature y CurrencyId.; @XmlObject debe contener filas /Data/Row con RowIndex, RowColumns, PortfolioAdvanceCode/InvoiceNumber/ShareNumber/Value/FEConcept según el tipo de nota.; @NoteType debe ser 1 (factura total), 2 (factura por cuotas) o 3 (anticipo) para que se ejecute lógica de validación específica.; Debe existir un registro en GeneralLedger.CompanySettings que aporte la moneda oficial (OfficialCurrencyId).; El tercero (@ThirdPartyId) debe tener asignada la factura o el anticipo referenciado para que pase la validación.', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'PROCEDURE', @level1name=N'SP_CopyAndPastePortfolioNoteAccountReceivableAdvance';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se devuelven registros con StatusField distinto de 99 (validados, sea OK=0 o error=999).; Los registros marcados con error (StatusField=999) en una validación previa no se reevalúan en validaciones posteriores (todas usan WHERE StatusField=99).; El SpecificPortfolioStatus solo se considera cuando NoteType=1 y CompanyType=1; en cualquier otro caso se ignora.; La moneda del registro debe coincidir con la moneda del documento (@CurrencyId); si la entidad no tiene moneda se asume la moneda oficial de CompanySettings.; Para cuotas (NoteType=2) la factura debe estar en Status=2 y tener NumberShares>1.; Para anticipos (NoteType=3) el anticipo debe estar en Status=2.; Si la cuenta por cobrar es saldo inicial (OpeningBalance=1), el FEConcept se descarta (queda NULL).; AdjusmentValue debe ser numérico y estrictamente mayor que cero.', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'PROCEDURE', @level1name=N'SP_CopyAndPastePortfolioNoteAccountReceivableAdvance';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Cartera; Notas de cartera; Factura; Cuotas de factura; Anticipos de cartera; Tercero; Saldo; Naturaleza contable (débito/crédito); Moneda oficial; Estado específico de cartera (radicada, glosada, cobro jurídico, etc.); Saldo inicial (OpeningBalance); Concepto de facturación electrónica (FEConcept)', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'PROCEDURE', @level1name=N'SP_CopyAndPastePortfolioNoteAccountReceivableAdvance';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si @NoteType = 1 (Factura total) → Valida factura contra ViewAccountReceivableAccountingByPortfolioNote por tercero, número de factura y SpecificPortfolioStatus (cuando CompanyType=1); verifica saldo, moneda y completa AccountReceivableId, MainAccountId, AccountReceivableAccountingId, Value y Balance.; si @NoteType = 2 (Factura por cuotas) → Valida que la factura exista (Status=2) y tenga más de una cuota (NumberShares>1) coincidente con el número de cuota; verifica saldo de la cuota, moneda; completa AccountReceivableId, AccountReceivableShareId, AccountReceivableAccountingId, Value y Balance.; si @NoteType = 3 (Anticipo) → Valida anticipo en Portfolio.PortfolioAdvance por tercero y código (Status=2); verifica que la naturaleza coincida con la de la cuenta principal o que tenga saldo, y la moneda; completa PortfolioAdvanceId, Value y Balance.; si @NoteType=1 y @CompanyType=1 → Se exige RowColumns >= 3 y se utiliza SpecificPortfolioStatus como discriminador adicional para evitar duplicados y para emparejar la factura. else Para otros NoteType/CompanyType solo se exige RowColumns >= 2 y se ignora SpecificPortfolioStatus.; si @Nature = 2 y AdjusmentValue > Balance → Se marca el registro con error ''el valor del ajuste es mayor al saldo''.; si @Nature = 1 y AdjusmentValue > (Value - Balance) (en cuotas) → Se marca con error ''el ajuste supera el valor inicial de la cuota''.; si Cuenta por cobrar marcada como saldo inicial (AccountReceivable.OpeningBalance = 1) → Se anula (NULL) el FEConcept del registro, omitiendo el concepto de facturación electrónica para saldos iniciales.', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'PROCEDURE', @level1name=N'SP_CopyAndPastePortfolioNoteAccountReceivableAdvance';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'GeneralLedger.CompanySettings; Portfolio.ViewAccountReceivableAccountingByPortfolioNote; Portfolio.AccountReceivableShare; Portfolio.AccountReceivable; Portfolio.AccountReceivableAccounting; Portfolio.PortfolioAdvance; GeneralLedger.MainAccounts', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'PROCEDURE', @level1name=N'SP_CopyAndPastePortfolioNoteAccountReceivableAdvance';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'PROCEDURE', @level1name=N'SP_CopyAndPastePortfolioNoteAccountReceivableAdvance';
-- GO
