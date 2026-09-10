
-- =============================================
-- Author:		Carlos Ernesto Cordoba
-- Create date:	2016-07-11
-- Description:	Procedimiento para guardar, actualizar, confirmar o anular la nota
-- =============================================
CREATE PROCEDURE [Portfolio].[SP_SavePortfolioNote_Output]
    @PortfolioNoteXml AS XML,
    @User VARCHAR(20),
	------------------------------------------------------
	@CompanyType TINYINT,
	------------------------------------------------------
	@CodeResult INT OUTPUT,
	@MessageResult VARCHAR(MAX) OUTPUT,
	@Id INT OUTPUT,
	@Code VARCHAR(20) OUTPUT
AS
BEGIN
    SET NOCOUNT ON

	/*******************************************  DECLARACION DE VARIABLES *******************************************/
    --------------  Variables de la cabecera --------------
    DECLARE @OperatingUnitId INT,
			@NoteDate DATETIME,
			@CustomerId INT,
			@Observations VARCHAR(MAX),
			@Nature TINYINT,
			@NoteType TINYINT,			
			@Status TINYINT,
			@PortfolioAdvanceId INT,
			@PortfolioTransferId INT,
			@ChangeTracker VARCHAR(30),
			@CurrencyId INT,
			-----------------------------------------------
			@IsManual BIT,
			@IdForm INT = 686,
			@DocumentType INT = 1,
			-----------------------------------------------
			@Message VARCHAR(MAX),
			-----------------------------------------------
			@SubXml XML,
			@Code_Output INT,
			@Message_Output VARCHAR(MAX),
			@JournalVoucherId_Output INT,		
			@ReversalPreviousYearsGenericBillingMainAccountId INT,
			@EntityName VARCHAR(20),
            @EntityId int,
			@MessageJvRevaluation varchar(1000)

    ----------  Declaracion de tablas temporales ----------
	
	DECLARE @PortfolioNoteDetail TABLE 
	(	
		[Id] [INT] NOT NULL,
		[PortfolioNoteId] [INT] NOT NULL,
		[PortfolioNoteConceptId] [INT] NOT NULL,
		[MainAccountId] [INT] NOT NULL,
		[ThirdPartyId] [INT] NOT NULL,
		[CostCenterId] [INT] NULL,
		[Nature] [TINYINT] NOT NULL,
		[Value] [numeric](20, 2) NOT NULL,
		[RetentionConceptId] [INT] NULL,
		[BaseValue] [decimal](20, 2) NULL,
		[Percentage] [numeric](5, 3) NULL,
		[Observations] [VARCHAR](3000) NULL,
		[IdGeneralLedgerIVA] [INT] NULL,
		[IvaRate] [decimal](18, 2) NULL,
		[TotalConcept] [decimal](18, 2) NULL,
		[ChangeTracker] VARCHAR(30)
	)

    DECLARE @PortfolioNoteAccountReceivableAdvance TABLE
	(
		[PortfolioNoteAccountReceivableAdvanceIdTmp] [INT] NOT NULL,
		[Id] [INT]  NOT NULL,
		[PortfolioNoteId] [INT] NOT NULL,
		[AccountReceivableId] [INT] NULL,
		[AccountReceivableShareId] [INT] NULL,
		[MainAccountId] [INT] NULL,
		[AccountReceivableAccountingId] [INT] NULL,
		[PortfolioAdvanceId] [INT] NULL,
		[AdjusmentValue] [numeric](20, 2) NOT NULL,
		[PercentageValue] [numeric](5, 2) NOT NULL,
		[PreviousBalance] [numeric](20, 2) NOT NULL,
		[Balance] [numeric](20, 2) NOT NULL,
		[ConceptId] [INT] NULL,
		[InvoiceNumber] VARCHAR(30),
		[ChangeTracker] VARCHAR(30)
	)

	DECLARE @PortfolioNoteAccountReceivableDetail TABLE 
	(	
		[Id] [INT]  NOT NULL,
		[PortfolioNoteAccountReceivableAdvanceIdTmp] [INT] NOT NULL,
		[PortfolioNoteAccountReceivableId] [INT] NOT NULL,
		[EntityName] VARCHAR(250) NOT NULL,
		[EntityId] [INT] NOT NULL,
		[CodeName] VARCHAR(500)  NULL,
		[MainAccountId] [INT] NOT NULL,
		[CostCenterId] [INT] NULL,
		[Value] [numeric](18, 2) NOT NULL,
		[ChangeTracker] VARCHAR(30),
		[BaseValue] [numeric](20,2) NOT NULL,
		[TaxValue] [numeric](20,2) NOT NULL,
		[TaxPercentage] [NUMERIC](5,2) NULL,
		[TaxId] INT NULL,
		[ThirdPartyId] INT NULL
	)

    DECLARE @PortfolioNoteDistribution TABLE 
	(	
		[Id] [INT]  NOT NULL,
		[PortfolioNoteId] [INT] NOT NULL,
		[CustomerId] [INT] NOT NULL,
		[MainAccountId] [INT] NOT NULL,
		[CostCenterId] [INT] NULL,
		[Value] [numeric](18, 2) NOT NULL,
		[PortfolioAdvanceId] [INT] NULL,
		[ChangeTracker] VARCHAR(30)
	)

	--Tabla para ir almacenando los errores 
	DECLARE @TableErrors TABLE([Message] VARCHAR(300))

	DECLARE @JournalVoucher TABLE 
	(
		Id INT DEFAULT(0),
		Consecutive BIGINT DEFAULT(0),
		LegalBookId INT,
		IdJournalVoucher INT,
		VoucherDate DATETIME,
		Imported VARCHAR(5) DEFAULT('False'),
		Status TINYINT,
		Detail VARCHAR(MAX),
		EntityCode VARCHAR(20),
		EntityId INT,
		EntityName VARCHAR(250),
		OriginEntityName VARCHAR(250),
		IsClosedYear TINYINT DEFAULT(0),
		CurrencyId INT
	)
			
	DECLARE @JournalVoucherDetails TABLE 
	(
		Id INT DEFAULT(0),
		IdAccounting INT DEFAULT(0),
		IdMainAccount INT,
		IdThirdParty INT,
		IdCostCenter INT,
		DebitValue DECIMAL(18,2),
		CreditValue DECIMAL(18,2),
		Detail VARCHAR(500),
		IdRetention INT,
		RetentionRate DECIMAL(6,3),
		BaseValue DECIMAL(18,2),
		BillingValue DECIMAL(18,2)
	)

	--Tabla de resultados para la generacion de detalles de comprobante contable para provision y deterioro
	DECLARE @ResultProvisionAndDeterioration TABLE
	(
		Id INT, 
		[Status] INT, 
		[Message] VARCHAR(MAX),
		IdMainAccount INT, 
		IdThirdParty INT, 
		IdCostCenter INT, 
		DebitValue decimal(20, 4), 
		CreditValue decimal(20, 4)
	)
	
	--Tabla de resultado de los ajustes diferencial
	DECLARE @responseRevaluation TABLE
	(	code varchar(20),
		MessageResult varchar(max), JournalVoucherId int
	)

	--tabla temporal para almacenar el resultado deL movimiento contable
	declare @resultJournalVoucher table (code varchar(20),MessageResult varchar(max),IdJournalVoucher integer)

    BEGIN TRY
		/************************************  ASIGNACIONES DE VARIABLES CABECERA ************************************/
	
        SELECT	@Id = t.x.value('Id[1]', 'INT'),
				@Code = t.x.value('Code[1]', 'VARCHAR(20)'),
				@OperatingUnitId = t.x.value('OperatingUnitId[1]', 'INT'),
				@NoteDate = t.x.value('NoteDate[1]', 'DATETIME'),
				@CustomerId = t.x.value('CustomerId[1]', 'INT'),
				@Observations = t.x.value('Observations[1]', 'VARCHAR(MAX)'),
				@Nature = t.x.value('Nature[1]', 'TINYINT'),
				@NoteType = t.x.value('NoteType[1]', 'TINYINT'),
				@Status = t.x.value('Status[1]', 'TINYINT'),
				@PortfolioAdvanceId = t.x.value('PortfolioAdvanceId[1]', 'INT'),
				@PortfolioTransferId = t.x.value('PortfolioTransferId[1]', 'INT'),
				@ChangeTracker = t.x.value('ChangeTracker[1]', 'VARCHAR(30)'),
				@CurrencyId = IIF(t.x.value('CurrencyId[1]', 'INT') ='',NULL,t.x.value('CurrencyId[1]', 'INT')),
				@EntityName = t.x.value('EntityName[1]','Varchar(20)'),
                @EntityId = t.x.value('EntityId[1]', 'INT')
        FROM @PortfolioNoteXml.nodes('/PortfolioNote') t(x);

        /******************************************  VALIDACIONES GENERALES ******************************************/

		IF EXISTS (SELECT 1 FROM Portfolio.PortfolioNote  WHERE Id = @Id AND Status <> 1)
		BEGIN
			SELECT	@CodeResult = 999,
					@MessageResult = 'La nota de cartera se encuentra en estado: ' + IIF(Status = 2, 'Confirmado', 'Anulado'),
					@Id = 0,
					@Code = ''
			FROM Portfolio.PortfolioNote 
			WHERE Id = @Id AND Status <> 1
			RETURN
		END
		/*************************************************************************************************************/

		--Validacion Multi-Moneda

		DECLARE @OfficialCurrencyId INT = (SELECT TOP 1 cs.OfficialCurrencyId FROM GeneralLedger.CompanySettings cs WITH(NOLOCK))

		IF @CurrencyId IS NULL BEGIN
			SELECT TOP 1 @CurrencyId = @OfficialCurrencyId
		END

		-- Obtener tolerancia y precisión de redondeo basada en la moneda
		DECLARE @RoundTolerance DECIMAL(18,2) = 0.01
		DECLARE @RoundPrecision INT = 2  -- Precisión para ROUND de SQL (número de decimales)
		IF @CurrencyId IS NOT NULL AND @CurrencyId > 0
		BEGIN
			SELECT @RoundTolerance = Common.GetRoundTolerance(c.RoundingType),
				   @RoundPrecision = Common.GetRoundPrecision(c.RoundingType)
			FROM Common.Currency c 
			WHERE c.Id = @CurrencyId
		END
		SET @RoundTolerance = ISNULL(@RoundTolerance, 0.01)
		SET @RoundPrecision = ISNULL(@RoundPrecision, 2)

		--cta de reversion parametros fact para anulacion Fact-Basica
		SELECT top 1 @ReversalPreviousYearsGenericBillingMainAccountId = sb.ReversalPreviousYearsGenericBillingMainAccountId
		FROM Billing.SettingsBilling sb WITH(NOLOCK)
		where sb.IdOperatingUnit = @OperatingUnitId
		/*********************************************** ************* ***********************************************/

		IF @Status = 3
		BEGIN
			UPDATE [Portfolio].[PortfolioNote]
				SET [Status] = @Status,
					[ModificationUser] = @User,
					[ModificationDate] = [Common].[GETDATE](),
					[AnnulmentUser] = @User,
					[AnnulmentDate] = [Common].[GETDATE]()
			WHERE Id = @Id
		END
		ELSE
		BEGIN
			/***************************************  ASIGNACIONES DE DETALLES ***************************************/
			
			INSERT INTO @PortfolioNoteDetail 
				SELECT 
					t.x.value('Id[1]', 'INT') as Id,
					t.x.value('PortfolioNoteId[1]', 'INT') as PortfolioNoteId,
					t.x.value('PortfolioNoteConceptId[1]', 'INT') as PortfolioNoteConceptId,
					t.x.value('MainAccountId[1]', 'INT') as MainAccountId,
					t.x.value('ThirdPartyId[1]', 'INT') as ThirdPartyId,
					t.x.value('CostCenterId[1]', 'INT') as CostCenterId,
					t.x.value('Nature[1]', 'TINYINT')as Nature,
					t.x.value('Value[1]', 'decimal(20, 2)') AS Value,
					t.x.value('RetentionConceptId[1]', 'INT') as RetentionConceptId,
					t.x.value('BaseValue[1]', 'decimal(20, 2)') AS BaseValue,
					t.x.value('Percentage[1]', 'decimal(5, 3)') AS Percentage,
					t.x.value('Observations[1]', 'VARCHAR(3000)') as Observations,
					t.x.value('IdGeneralLedgerIVA[1]', 'INT') AS IdGeneralLedgerIVA,
					t.x.value('IvaRate[1]', 'decimal(18, 2)') AS IvaRate,
					t.x.value('TotalConcept[1]', 'decimal(18, 2)') AS TotalConcept,
					t.x.value('ChangeTracker[1]', 'VARCHAR(30)') AS ChangeTracker
				FROM @PortfolioNoteXml.nodes('/PortfolioNote/PortfolioNoteDetail') t(x);

			INSERT INTO @PortfolioNoteAccountReceivableAdvance
				SELECT 
					ISNULL(t.x.value('PortfolioNoteAccountReceivableAdvanceIdTmp[1]', 'INT'),0) as PortfolioNoteAccountReceivableAdvanceIdTmp,
					t.x.value('Id[1]', 'INT') as Id,
					t.x.value('PortfolioNoteId[1]', 'INT') as PortfolioNoteId,
					t.x.value('AccountReceivableId[1]', 'INT') as AccountReceivableId,
					t.x.value('AccountReceivableShareId[1]', 'INT') as AccountReceivableShareId,
					t.x.value('MainAccountId[1]', 'INT') as MainAccountId,
					t.x.value('AccountReceivableAccountingId[1]', 'INT') as AccountReceivableAccountingId,
					t.x.value('PortfolioAdvanceId[1]', 'INT') as PortfolioAdvanceId,
					t.x.value('AdjusmentValue[1]', 'decimal(20, 2)') AS AdjusmentValue,
					t.x.value('PercentageValue[1]', 'decimal(5, 2)') AS PercentageValue,
					t.x.value('PreviousBalance[1]', 'decimal(20, 2)') AS PreviousBalance,
					t.x.value('Balance[1]', 'decimal(20, 2)') AS Balance,
					t.x.value('ConceptId[1]', 'INT') as ConceptId,
					t.x.value('InvoiceNumber[1]', 'VARCHAR(30)') AS InvoiceNumber,
					t.x.value('ChangeTracker[1]', 'VARCHAR(30)') AS ChangeTracker
				FROM @PortfolioNoteXml.nodes('/PortfolioNote/PortfolioNoteAccountReceivableAdvance') t(x);

			INSERT INTO @PortfolioNoteAccountReceivableDetail
				SELECT 
					t.x.value('Id[1]', 'INT') as Id,
					t.x.value('PortfolioNoteAccountReceivableAdvanceIdTmp[1]', 'INT') as PortfolioNoteAccountReceivableAdvanceIdTmp,
					t.x.value('PortfolioNoteAccountReceivableId[1]', 'INT') as PortfolioNoteAccountReceivableId,
					t.x.value('EntityName[1]', 'VARCHAR(250)') as EntityName,
					t.x.value('EntityId[1]', 'INT') as EntityId,
					t.x.value('CodeName[1]', 'VARCHAR(250)') AS CodeName,
					t.x.value('MainAccountId[1]', 'INT') as MainAccountId,
					t.x.value('CostCenterId[1]', 'INT') as CostCenterId,
					t.x.value('Value[1]', 'decimal(18, 2)') AS Value,
					t.x.value('ChangeTracker[1]', 'VARCHAR(30)') AS ChangeTracker,
					COALESCE (t.x.value('BaseValue[1]', 'NUMERIC(20, 2)'),0) AS BaseValue,
					COALESCE (t.x.value('TaxValue[1]', 'NUMERIC(20, 2)'),0) AS TaxValue,
					t.x.value('TaxPercentage[1]', 'NUMERIC(18, 2)') AS TaxPercentage,
					t.x.value('TaxId[1]', 'INT') AS TaxId,
					t.x.value('ThirdPartyId[1]', 'INT') AS ThirdPartyId
				FROM @PortfolioNoteXml.nodes('/PortfolioNote/PortfolioNoteAccountReceivableAdvance/PortfolioNoteAccountReceivableDetail') t(x);

			INSERT INTO @PortfolioNoteDistribution
				SELECT 
					t.x.value('Id[1]', 'INT') as Id,
					t.x.value('PortfolioNoteId[1]', 'INT') as PortfolioNoteId,
					t.x.value('CustomerId[1]', 'INT') as CustomerId,
					t.x.value('MainAccountId[1]', 'INT') as MainAccountId,
					t.x.value('CostCenterId[1]', 'INT') as CostCenterId,
					t.x.value('Value[1]', 'decimal(18, 2)') AS Value,
					t.x.value('PortfolioAdvanceId[1]', 'INT') as PortfolioAdvanceId,
					t.x.value('ChangeTracker[1]', 'VARCHAR(30)') AS ChangeTracker
				FROM @PortfolioNoteXml.nodes('/PortfolioNote/PortfolioNoteDistribution') t(x);

			/**************************************** ELIMINACION DE DETALLES ****************************************/
			--elimino los detalles marcados para su eliminación
			DELETE pnd
			FROM @PortfolioNoteDetail pndTmp
			JOIN Portfolio.PortfolioNoteDetail pnd  ON pndTmp.Id = pnd.Id 
			WHERE @Id = pnd.PortfolioNoteId AND pndTmp.ChangeTracker = 'Deleted'

			DELETE @PortfolioNoteDetail WHERE ChangeTracker = 'Deleted'

			--elimino los detalles de las facturas marcados para su eliminación
			DELETE pnard
			FROM @PortfolioNoteAccountReceivableDetail pnardTmp
			JOIN Portfolio.PortfolioNoteAccountReceivableDetail pnard  ON pnardTmp.Id = pnard.Id 
			JOIN Portfolio.PortfolioNoteAccountReceivableAdvance pnara  ON pnard.PortfolioNoteAccountReceivableId = pnara.Id
			WHERE @Id = pnara.PortfolioNoteId AND pnardTmp.ChangeTracker = 'Deleted'

			DELETE @PortfolioNoteAccountReceivableDetail WHERE ChangeTracker = 'Deleted'

			--elimino las facturas / anticipos marcados para su eliminación (Pero primero elimino los subdetalles)
			DELETE pnard
			FROM @PortfolioNoteAccountReceivableAdvance pnaraTmp
			JOIN Portfolio.PortfolioNoteAccountReceivableAdvance pnara  ON pnaraTmp.Id = pnara.Id
			JOIN Portfolio.PortfolioNoteAccountReceivableDetail pnard  ON pnara.Id = pnard.PortfolioNoteAccountReceivableId
			WHERE @Id = pnara.PortfolioNoteId AND pnaraTmp.ChangeTracker = 'Deleted'

			DELETE pnardTmp
			FROM @PortfolioNoteAccountReceivableAdvance pnaraTmp
			JOIN @PortfolioNoteAccountReceivableDetail pnardTmp ON pnaraTmp.Id = pnardTmp.PortfolioNoteAccountReceivableId
			WHERE pnaraTmp.ChangeTracker = 'Deleted'

			DELETE pnara
			FROM @PortfolioNoteAccountReceivableAdvance pnaraTmp
			JOIN Portfolio.PortfolioNoteAccountReceivableAdvance pnara  ON pnaraTmp.Id = pnara.Id
			WHERE @Id = pnara.PortfolioNoteId AND pnaraTmp.ChangeTracker = 'Deleted'

			DELETE @PortfolioNoteAccountReceivableAdvance WHERE ChangeTracker = 'Deleted'

			--elimino los detalles de distribución marcados para su eliminación

			DELETE pnd
			FROM @PortfolioNoteDistribution pndTmp
			JOIN Portfolio.PortfolioNoteDistribution pnd  ON pndTmp.Id = pnd.Id
			WHERE @Id = pnd.PortfolioNoteId AND pndTmp.ChangeTracker = 'Deleted'

			DELETE @PortfolioNoteDistribution WHERE ChangeTracker = 'Deleted'

			/*********************************************  VALIDACIONES *********************************************/
			
			IF NOT EXISTS (SELECT 1 FROM Portfolio.SettingPortfolio  WHERE OperatingUnitId = @OperatingUnitId)
			BEGIN
				SELECT	@CodeResult = 999, 
						@MessageResult = 'No se encontraron parametros de cartera para la unidad operativa seleccionada', 
						@Id = 0, 
						@Code = ''
				RETURN
			END

			IF NOT EXISTS (SELECT 1 FROM GeneralLedger.ClosedMonth  WHERE [Year] = YEAR(@NoteDate) AND [Month] = MONTH(@NoteDate) AND [Status] = 1)
			BEGIN
				SELECT	@CodeResult = 999, 
						@MessageResult = 'El mes seleccionado en la fecha de la nota esta cerrado', 
						@Id = 0, 
						@Code = ''
				RETURN
			END

			IF @NoteType IN (1, 2, 3, 6)
			BEGIN
				-----------  Validación Detalles ----------
				IF NOT EXISTS (SELECT 1 FROM @PortfolioNoteDetail ) and @NoteType <> 6
				BEGIN 
					INSERT INTO @TableErrors 
						SELECT 'La Nota debe crear mínimo un detalle de la nota'
				END

				IF EXISTS (SELECT 1 FROM @PortfolioNoteDetail WHERE Value <= 0)
				BEGIN
					INSERT INTO @TableErrors 
						SELECT 'La Nota tiene detalles en 0 o negativos'
				END

				IF EXISTS 
				(
					SELECT 1 
					FROM Portfolio.PortfolioNoteDetail pnd 
					LEFT JOIN @PortfolioNoteDetail pndTmp ON pnd.Id = pndTmp.Id
					WHERE pnd.PortfolioNoteId = @Id AND pndTmp.Id IS NULL
				) 
				BEGIN
					INSERT INTO @TableErrors 
						SELECT 'Existen detalles de la Nota que no han sido procesados.'
				END

				-----  Validación Facturas / Anticipos ----
				IF NOT EXISTS (SELECT 1 FROM @PortfolioNoteAccountReceivableAdvance)
				BEGIN
					INSERT INTO @TableErrors 
						SELECT 'La Nota debe ajustar mínimo un anticipo o una factura'
				END

				IF EXISTS (SELECT 1 FROM @PortfolioNoteAccountReceivableAdvance WHERE AdjusmentValue <= 0)
				BEGIN
					INSERT INTO @TableErrors 
						SELECT 'La Nota tiene anticipos o facturas con valor a ajustar en 0 o negativos'
				END

				IF EXISTS 
				(
					SELECT 1 
					FROM Portfolio.PortfolioNoteAccountReceivableAdvance pnara 
					LEFT JOIN @PortfolioNoteAccountReceivableAdvance pnaraTmp ON pnara.Id = pnaraTmp.Id
					WHERE pnara.PortfolioNoteId = @Id AND pnaraTmp.Id IS NULL
				) 
				BEGIN
					INSERT INTO @TableErrors 
						SELECT 'Existen anticipos o facturas de la Nota que no han sido procesados.'
				END

				IF EXISTS 
				(
					SELECT 1 
					FROM @PortfolioNoteAccountReceivableAdvance
					GROUP BY AccountReceivableId, AccountReceivableShareId, MainAccountId, PortfolioAdvanceId
					HAVING COUNT(1) > 1
				) 
				BEGIN
					INSERT INTO @TableErrors 
						SELECT 'Existen anticipos o facturas de la Nota duplicados.'
				END

				IF @NoteType = 6
				BEGIN
				-----  Validación Detalle Facturas ----
				--=========================================================================================================================
					IF NOT EXISTS (SELECT 1 FROM @PortfolioNoteAccountReceivableDetail)
					BEGIN
						INSERT INTO @TableErrors 
							SELECT 'La Nota debe tener los detalles a ajustar de las facturas'
					END

					IF EXISTS (SELECT 1 FROM @PortfolioNoteAccountReceivableDetail WHERE Value <= 0)
					BEGIN
						INSERT INTO @TableErrors 
							SELECT 'Existen detalles de facturas con valor a ajustar en 0 o negativos'
					END

					IF EXISTS 
					(
						SELECT 1 
						FROM Portfolio.PortfolioNoteAccountReceivableAdvance pnara 
						JOIN Portfolio.PortfolioNoteAccountReceivableDetail pnard  ON pnara.Id = pnard.PortfolioNoteAccountReceivableId
						LEFT JOIN @PortfolioNoteAccountReceivableDetail pnardTmp ON pnard.Id = pnardTmp.Id
						WHERE pnara.PortfolioNoteId = @Id AND pnardTmp.Id IS NULL
					) 
					BEGIN
						INSERT INTO @TableErrors 
							SELECT 'Existen detalles de las facturas de la Nota que no han sido procesados.'
					END

					IF EXISTS 
					(
						SELECT 1 
						FROM @PortfolioNoteAccountReceivableDetail
						GROUP BY EntityName, EntityId
						HAVING COUNT(1) > 1
					) 
					BEGIN
						INSERT INTO @TableErrors 
							SELECT 'Existen detalles de las facturas de la Nota duplicados.'
					END
					
					IF EXISTS
					(
						SELECT 1 
						FROM @PortfolioNoteAccountReceivableAdvance pnaraTmp
						FULL JOIN 
						(
							SELECT PortfolioNoteAccountReceivableAdvanceIdTmp, SUM(Value) Value
							FROM @PortfolioNoteAccountReceivableDetail 
							GROUP BY PortfolioNoteAccountReceivableAdvanceIdTmp
						) pndTmp ON pnaraTmp.PortfolioNoteAccountReceivableAdvanceIdTmp = pndTmp.PortfolioNoteAccountReceivableAdvanceIdTmp
						WHERE ISNULL(pnaraTmp.AdjusmentValue, 0) > ISNULL(pndTmp.Value, 0)
					)
					BEGIN
						INSERT INTO @TableErrors 
							SELECT DISTINCT CONCAT('El valor ajustado de la factura ', ar.InvoiceNumber, ' no corresponde con el de sus detalles')
							FROM @PortfolioNoteAccountReceivableAdvance pnaraTmp
							FULL JOIN 
							(
								SELECT PortfolioNoteAccountReceivableAdvanceIdTmp, SUM(Value) Value
								FROM @PortfolioNoteAccountReceivableDetail 
								GROUP BY PortfolioNoteAccountReceivableAdvanceIdTmp
							) pndTmp ON pnaraTmp.PortfolioNoteAccountReceivableAdvanceIdTmp = pndTmp.PortfolioNoteAccountReceivableAdvanceIdTmp
							LEFT JOIN Portfolio.AccountReceivable ar  ON pnaraTmp.AccountReceivableId = ar.Id
							WHERE ISNULL(pnaraTmp.AdjusmentValue, 0) > ISNULL(pndTmp.Value, 0)
					END

					IF EXISTS
					(
						SELECT 1
						FROM @PortfolioNoteAccountReceivableDetail pnardTmp
						JOIN Billing.InvoiceDetailBasicInvoice idbi  ON pnardTmp.EntityName = 'InvoiceDetailBasicInvoice' AND pnardTmp.EntityId = idbi.Id
						WHERE
						(
							(@Nature = 1 AND pnardTmp.Value > (CASE WHEN @NoteType = 1 THEN idbi.TotalSalesPrice * 0.5 ELSE idbi.TotalSalesPrice - idbi.Balance END)) --BUG: NoteType=1 (Factura Total) tope acordado con QA = 50% del valor inicial, en lugar de (valor - saldo) que bloqueaba incrementos sobre facturas sin abonos
							OR
							(@Nature = 2 AND pnardTmp.Value > idbi.Balance)
						)
					)
					BEGIN
						INSERT INTO @TableErrors
							SELECT CONCAT('El valor del ajuste del detalle ', pnardTmp.CodeName, ' no puede ser mayor de su ', IIF(@Nature = 1, 'valor inicial', 'saldo'))
							FROM @PortfolioNoteAccountReceivableDetail pnardTmp
							JOIN Billing.InvoiceDetailBasicInvoice idbi  ON pnardTmp.EntityName = 'InvoiceDetailBasicInvoice' AND pnardTmp.EntityId = idbi.Id
							WHERE
							(
								(@Nature = 1 AND pnardTmp.Value > (CASE WHEN @NoteType = 1 THEN idbi.TotalSalesPrice * 0.5 ELSE idbi.TotalSalesPrice - idbi.Balance END)) --BUG: NoteType=1 (Factura Total) tope acordado con QA = 50% del valor inicial, en lugar de (valor - saldo) que bloqueaba incrementos sobre facturas sin abonos
								OR
								(@Nature = 2 AND pnardTmp.Value > idbi.Balance)
							)
					END

					IF EXISTS
					(
						SELECT 1
						FROM @PortfolioNoteAccountReceivableDetail pnardTmp
						JOIN Billing.InvoiceDetail id  ON pnardTmp.EntityName = 'InvoiceDetail' AND pnardTmp.EntityId = id.Id
						WHERE
						(
							(@Nature = 1 AND pnardTmp.Value > (CASE WHEN @NoteType = 1 THEN id.ThirdPartySalesPrice * 0.5 ELSE id.ThirdPartySalesPrice - id.Balance END)) --BUG: NoteType=1 (Factura Total) tope acordado con QA = 50% del valor inicial, en lugar de (valor - saldo) que bloqueaba incrementos sobre facturas sin abonos
							OR
							(@Nature = 2 AND pnardTmp.Value > id.Balance)
						)
					)
					BEGIN
						INSERT INTO @TableErrors
							SELECT CONCAT('El valor del ajuste del detalle ', pnardTmp.CodeName, ' no puede ser mayor de su ', IIF(@Nature = 1, 'valor inicial', 'saldo'))
							FROM @PortfolioNoteAccountReceivableDetail pnardTmp
							JOIN Billing.InvoiceDetail id  ON pnardTmp.EntityName = 'InvoiceDetail' AND pnardTmp.EntityId = id.Id
							WHERE
							(
								(@Nature = 1 AND pnardTmp.Value > (CASE WHEN @NoteType = 1 THEN id.ThirdPartySalesPrice * 0.5 ELSE id.ThirdPartySalesPrice - id.Balance END)) --BUG: NoteType=1 (Factura Total) tope acordado con QA = 50% del valor inicial, en lugar de (valor - saldo) que bloqueaba incrementos sobre facturas sin abonos
								OR
								(@Nature = 2 AND pnardTmp.Value > id.Balance)
							)
					END

					/*VALIDACION CAMBIO TARIFA IVA, se permite solo para movimiento donde el ajuste sea igual al valor inicial*/
					--***FACTURA SALUD***
					IF EXISTS(	SELECT 1
								FROM @PortfolioNoteAccountReceivableDetail pnardTmp
								JOIN Billing.InvoiceDetail id  ON pnardTmp.EntityName = 'InvoiceDetail' AND pnardTmp.EntityId = id.Id
								WHERE pnardTmp.TaxId <> id.TaxId and  id.ThirdPartySalesPrice <> pnardTmp.Value				
								) BEGIN

								INSERT INTO @TableErrors
								SELECT CONCAT('El cambio de tarifa de IVA del detalle ',pnardTmp.CodeName,'. Solo se puede llevar a cabo si el ajuste es total')
								FROM @PortfolioNoteAccountReceivableDetail pnardTmp
								JOIN Billing.InvoiceDetail id  ON pnardTmp.EntityName = 'InvoiceDetail' AND pnardTmp.EntityId = id.Id
								WHERE pnardTmp.TaxId <> id.TaxId and  id.ThirdPartySalesPrice <> pnardTmp.Value
					END

					IF EXISTS(	SELECT 1
								FROM @PortfolioNoteAccountReceivableDetail pnardTmp
								WHERE pnardTmp.Value <0 or pnardTmp.BaseValue < 0 or pnardTmp.TaxValue <0			
								) BEGIN

								INSERT INTO @TableErrors
								SELECT CONCAT('El detalle ',pnardTmp.CodeName,' no puede tener ajustes negativos')
								FROM @PortfolioNoteAccountReceivableDetail pnardTmp
								WHERE pnardTmp.Value <0 or pnardTmp.BaseValue < 0 or pnardTmp.TaxValue <0	
					END

					IF EXISTS
					(
						SELECT 1
						FROM @PortfolioNoteAccountReceivableDetail pnardTmp
						JOIN Billing.InvoiceDetailSurgical ids  ON pnardTmp.EntityName = 'InvoiceDetailSurgical' AND pnardTmp.EntityId = ids.Id
						WHERE
						(
							(@Nature = 1 AND pnardTmp.Value > (CASE WHEN @NoteType = 1 THEN ids.TotalSalesPrice * 0.5 ELSE ids.TotalSalesPrice - ids.Balance END)) --BUG: NoteType=1 (Factura Total) tope acordado con QA = 50% del valor inicial, en lugar de (valor - saldo) que bloqueaba incrementos sobre facturas sin abonos
							OR
							(@Nature = 2 AND pnardTmp.Value > ids.Balance)
						)
					)
					BEGIN
						INSERT INTO @TableErrors
							SELECT CONCAT('El valor del ajuste del detalle ', pnardTmp.CodeName, ' no puede ser mayor de su ', IIF(@Nature = 1, 'valor inicial', 'saldo'))
							FROM @PortfolioNoteAccountReceivableDetail pnardTmp
							JOIN Billing.InvoiceDetailSurgical ids  ON pnardTmp.EntityName = 'InvoiceDetailSurgical' AND pnardTmp.EntityId = ids.Id
							WHERE
							(
								(@Nature = 1 AND pnardTmp.Value > (CASE WHEN @NoteType = 1 THEN ids.TotalSalesPrice * 0.5 ELSE ids.TotalSalesPrice - ids.Balance END)) --BUG: NoteType=1 (Factura Total) tope acordado con QA = 50% del valor inicial, en lugar de (valor - saldo) que bloqueaba incrementos sobre facturas sin abonos
								OR
								(@Nature = 2 AND pnardTmp.Value > ids.Balance)
							)
					END

					IF EXISTS
					(
						SELECT 1
						FROM @PortfolioNoteAccountReceivableDetail pnardTmp
						JOIN Billing.InvoiceDetailProductSales idps  ON pnardTmp.EntityName = 'InvoiceDetailProductSales' AND pnardTmp.EntityId = idps.Id
						WHERE
						(
							(@Nature = 1 AND pnardTmp.Value > (CASE WHEN @NoteType = 1 THEN idps.TotalSalesPrice * 0.5 ELSE idps.TotalSalesPrice - idps.Balance END)) --BUG: NoteType=1 (Factura Total) tope acordado con QA = 50% del valor inicial, en lugar de (valor - saldo) que bloqueaba incrementos sobre facturas sin abonos
							OR
							(@Nature = 2 AND pnardTmp.Value > idps.Balance)
						)
					)
					BEGIN
						INSERT INTO @TableErrors
							SELECT CONCAT('El valor del ajuste del detalle ', pnardTmp.CodeName, ' no puede ser mayor de su ', IIF(@Nature = 1, 'valor inicial', 'saldo'))
							FROM @PortfolioNoteAccountReceivableDetail pnardTmp
							JOIN Billing.InvoiceDetailProductSales idps  ON pnardTmp.EntityName = 'InvoiceDetailProductSales' AND pnardTmp.EntityId = idps.Id
							WHERE
							(
								(@Nature = 1 AND pnardTmp.Value > (CASE WHEN @NoteType = 1 THEN idps.TotalSalesPrice * 0.5 ELSE idps.TotalSalesPrice - idps.Balance END)) --BUG: NoteType=1 (Factura Total) tope acordado con QA = 50% del valor inicial, en lugar de (valor - saldo) que bloqueaba incrementos sobre facturas sin abonos
								OR
								(@Nature = 2 AND pnardTmp.Value > idps.Balance)
							)
					END


					--Validacion Por existencia de EntityName
					--================InvoiceDetail=================

					IF EXISTS(SELECT 1
								FROM @PortfolioNoteAccountReceivableDetail pnardTmp
								LEFT JOIN Billing.InvoiceDetail id  ON pnardTmp.EntityId = id.Id
								WHERE pnardTmp.EntityName = 'InvoiceDetail' and id.Id is null) BEGIN

								INSERT into @TableErrors
								SELECT CONCAT('El Id del detalle de la factura : ( ',pnardTmp.EntityId,' ) No existe')
								FROM @PortfolioNoteAccountReceivableDetail pnardTmp
								LEFT JOIN Billing.InvoiceDetail id  ON pnardTmp.EntityId = id.Id
								WHERE  pnardTmp.EntityName = 'InvoiceDetail' and id.Id is null
					END

					--================InvoiceDetailSurgical=================

					IF EXISTS(SELECT 1
								FROM @PortfolioNoteAccountReceivableDetail pnardTmp
								LEFT JOIN Billing.InvoiceDetailSurgical id  ON pnardTmp.EntityId = id.Id
								WHERE  pnardTmp.EntityName = 'InvoiceDetailSurgical' and id.Id is null) BEGIN

								INSERT into @TableErrors
								SELECT CONCAT('El Id del detalle QX de la factura : ( ',pnardTmp.EntityId,' ) No existe')
								FROM @PortfolioNoteAccountReceivableDetail pnardTmp
								LEFT JOIN Billing.InvoiceDetailSurgical id  ON pnardTmp.EntityId = id.Id
								WHERE  pnardTmp.EntityName = 'InvoiceDetailSurgical' and id.Id is null
					END

					--================ServiceOrderDetailSurgical=================

					IF EXISTS(SELECT 1
								FROM @PortfolioNoteAccountReceivableDetail pnardTmp
								LEFT JOIN Billing.ServiceOrderDetailSurgical sods  ON pnardTmp.EntityId = sods.Id
								WHERE  pnardTmp.EntityName = 'ServiceOrderDetailSurgical' and sods.Id is null) BEGIN

								INSERT into @TableErrors
								SELECT CONCAT('El Id del detalle QX de la orden de servicio : ( ',pnardTmp.EntityId,' ) No existe')
								FROM @PortfolioNoteAccountReceivableDetail pnardTmp
								LEFT JOIN Billing.ServiceOrderDetailSurgical sods  ON pnardTmp.EntityId = sods.Id
								WHERE  pnardTmp.EntityName = 'ServiceOrderDetailSurgical' and sods.Id is null
					END
					-----  Validación Detalle Facturas ----
					--=========================================================================================================================
				END
				
				IF EXISTS 
				(
					SELECT 1
					FROM
					(
							SELECT @Id Id,
								IIF(@Nature = 1, AdjusmentValue, 0) DebitValue, 
								IIF(@Nature = 1, 0, AdjusmentValue) CreditValue
							FROM @PortfolioNoteAccountReceivableAdvance

						UNION ALL
							SELECT @Id Id,
								IIF(Nature = 1, ISNULL(TotalConcept, Value), 0) DebitValue, 
								IIF(Nature = 1, 0, ISNULL(TotalConcept, Value)) CreditValue
							FROM @PortfolioNoteDetail 

						UNION ALL

							SELECT @Id Id,
								IIF(@Nature = 2, ISNULL( pnard.Value,0), 0) DebitValue, 
								IIF(@Nature = 1, ISNULL(pnard.Value,0),0) CreditValue
							FROM @PortfolioNoteAccountReceivableDetail pnard 
							WHERE @NoteType=6

					) d
					GROUP BY Id
					HAVING SUM(d.DebitValue) <> SUM(d.CreditValue)
				)
				BEGIN
					INSERT INTO @TableErrors 
						SELECT 'La Nota tiene diferencia entre el débito y el crédito'
				END

				IF @NoteType = 1 OR @NoteType = 6
				BEGIN --Factura Total / Factura Detallada
					IF @Nature = 1 
					BEGIN
						INSERT INTO @TableErrors 
							SELECT 'El saldo de la factura ' + ISNULL(a.InvoiceNumber, '')  + ' es menor que el valor del ajuste'+ CHAR(13) + CHAR(10) 
							FROM @PortfolioNoteAccountReceivableAdvance pnara
							JOIN Portfolio.AccountReceivableAccounting ara  ON pnara.AccountReceivableAccountingId  = ara.Id  
							JOIN Portfolio.AccountReceivable a  ON a.Id = ara.AccountReceivableId  
							JOIN GeneralLedger.MainAccounts ma  ON ma.id  = ara.MainAccountId
							WHERE (pnara.AdjusmentValue - ara.Balance) > @RoundTolerance AND ma.Nature = 2

						INSERT INTO @TableErrors
							SELECT 'El valor del ajuste de la factura ' + ISNULL(a.InvoiceNumber, '')  + ' es mayor que el valor inicial registrado en la cuenta ' + ISNULL(ma.Number, '')  + CHAR(13) + CHAR(10)
							FROM @PortfolioNoteAccountReceivableAdvance pnara
							JOIN Portfolio.AccountReceivableAccounting ara  ON pnara.AccountReceivableAccountingId  = ara.Id
							JOIN Portfolio.AccountReceivable a  ON a.Id = ara.AccountReceivableId
							JOIN GeneralLedger.MainAccounts ma  ON ma.id  = ara.MainAccountId
							WHERE (pnara.AdjusmentValue - (CASE WHEN @NoteType = 1 THEN a.Value * 0.5 ELSE a.Value - a.Balance END)) > @RoundTolerance AND ma.Nature = 1 --BUG-42252/41983: NoteType=1 (Factura Total) tope acordado con QA = 50% del valor inicial, en lugar de (valor - saldo) que bloqueaba incrementos sobre facturas sin abonos
					END
					ELSE
					BEGIN                    
						INSERT INTO @TableErrors 
							SELECT 'El saldo de la factura ' + ISNULL( a.InvoiceNumber,'')  + ' es menor que el valor del ajuste'+ CHAR(13) + CHAR(10) 
							FROM @PortfolioNoteAccountReceivableAdvance pnara                         
							JOIN Portfolio.AccountReceivableAccounting ara  ON pnara.AccountReceivableAccountingId  = ara.Id  
							JOIN Portfolio.AccountReceivable a  ON a.Id = ara.AccountReceivableId  
							JOIN GeneralLedger.MainAccounts ma  ON ma.id  = ara.MainAccountId
							WHERE (pnara.AdjusmentValue - ara.Balance) > @RoundTolerance AND ma.Nature = 1

						INSERT INTO @TableErrors
							SELECT 'El valor del ajuste de la factura ' + ISNULL(a.InvoiceNumber, '')  + ' es mayor que el valor inicial registrado en la cuenta ' + ISNULL(ma.Number, '')  + CHAR(13) + CHAR(10)
							FROM @PortfolioNoteAccountReceivableAdvance pnara
							JOIN Portfolio.AccountReceivableAccounting ara  ON pnara.AccountReceivableAccountingId  = ara.Id
							JOIN Portfolio.AccountReceivable a  ON a.Id = ara.AccountReceivableId
							JOIN GeneralLedger.MainAccounts ma  ON ma.id  = ara.MainAccountId
							WHERE (pnara.AdjusmentValue - (a.Value - a.Balance)) > @RoundTolerance AND ma.Nature = 2
							 AND NOT (
									@EntityName = 'ReverseBasicBilling' -- anulación factura COPAGO
								AND @NoteType  = 1                    -- nota de tipo anulación total
								AND pnara.AdjusmentValue <= a.Value   -- no se pasa del valor de la factura
							  );
						
					END
				END
				ELSE IF @NoteType = 2
				BEGIN --Factura a Cuotas
					IF @Nature = 1
					BEGIN
						INSERT INTO @TableErrors 
							SELECT 'El saldo de la cuota ' + cast(ars.Number as varchar(10)) + ' de la factura '+ pnara.InvoiceNumber + ' es menor que el valor del ajuste'+ CHAR(13) + CHAR(10) 
							FROM @PortfolioNoteAccountReceivableAdvance  pnara                         
							JOIN Portfolio.AccountReceivableShare ars  ON pnara.AccountReceivableShareId   = ars.Id  
							JOIN Portfolio.AccountReceivableAccounting ara  ON ars.AccountReceivableId = ara.AccountReceivableId 
							JOIN GeneralLedger.MainAccounts ma  ON ara.MainAccountId = ma.Id
							WHERE (pnara.AdjusmentValue - ars.Balance) > @RoundTolerance AND ma.Nature = 2
					END
					ELSE
					BEGIN
						INSERT INTO @TableErrors
							SELECT 'El saldo de la cuota ' + cast(ars.Number as varchar(10)) + ' de la factura '+ pnara.InvoiceNumber + ' es menor que el valor del ajuste'+ CHAR(13) + CHAR(10)
							FROM @PortfolioNoteAccountReceivableAdvance  pnara
							JOIN Portfolio.AccountReceivableShare ars  ON pnara.AccountReceivableShareId   = ars.Id
							JOIN Portfolio.AccountReceivableAccounting ara  ON ars.AccountReceivableId = ara.AccountReceivableId
							JOIN GeneralLedger.MainAccounts ma  ON ara.MainAccountId = ma.Id
							WHERE (pnara.AdjusmentValue - ars.Balance) > @RoundTolerance AND ma.Nature = 2
					END
				END
				ELSE IF @NoteType = 3
				BEGIN --Anticipos
					IF @Nature = 1
					BEGIN
						INSERT INTO @TableErrors 
							SELECT 'El saldo del anticipo ' + pa.Code  + ' es menor que el valor del ajuste'+ CHAR(13) + CHAR(10) 
							FROM @PortfolioNoteAccountReceivableAdvance  pnara                         
							JOIN Portfolio.PortfolioAdvance pa  ON pnara.PortfolioAdvanceId    = pa .Id  
							JOIN GeneralLedger.MainAccounts ma  ON ma.Id = pa.MainAccountId
							WHERE (pnara.AdjusmentValue - pa.Balance) > @RoundTolerance AND ma.Nature = 2
					END
					ELSE
					BEGIN
						INSERT INTO @TableErrors
							SELECT 'El saldo del anticipo ' + pa.Code  + ' es menor que el valor del ajuste'+ CHAR(13) + CHAR(10)
							FROM @PortfolioNoteAccountReceivableAdvance  pnara
							JOIN Portfolio.PortfolioAdvance pa  ON pnara.PortfolioAdvanceId    = pa .Id
							JOIN GeneralLedger.MainAccounts ma  ON ma.Id = pa.MainAccountId
							WHERE (pnara.AdjusmentValue - pa.Balance) > @RoundTolerance AND ma.Nature = 1
					END
				END
			END
			ELSE IF (@NoteType = 4) 
			BEGIN --distribucion de anticipos
				IF ISNULL(@Nature, 0) <> 1
				BEGIN
					INSERT INTO @TableErrors 
						SELECT 'La Nota de distribución de anticipos debe ser de Naturaleza Débito'
				END

				IF NOT EXISTS (SELECT 1 FROM @PortfolioNoteDistribution)
				BEGIN
					INSERT INTO @TableErrors 
						SELECT 'La Nota debe tener detalles para la distribución'
				END

				IF EXISTS (SELECT 1 FROM @PortfolioNoteDistribution WHERE Value <= 0)
				BEGIN
					INSERT INTO @TableErrors 
						SELECT 'La Nota tiene detalles para la distribución en 0 o negativos'
				END

				IF EXISTS 
				(
					SELECT 1
					FROM Portfolio.PortfolioAdvance pa 
					JOIN
					(
						SELECT	@PortfolioAdvanceId PortfolioAdvanceId,
								SUM(Value) Value
						FROM @PortfolioNoteDistribution 
					) pnd ON pa.Id = pnd.PortfolioAdvanceId
					WHERE pa.Id = @PortfolioAdvanceId
						AND pnd.Value > pa.Balance
				)
				BEGIN
					INSERT INTO @TableErrors 
						SELECT 'El saldo del anticipo es menor al total de la distribucion'
				END
			END

			-----  Validación de saldo negativo en detalles de factura (InvoiceDetail) ----
			--Se valida antes de actualizar Billing.InvoiceDetail.Balance para evitar que se dispare el Constraint que impide valores negativos
			IF EXISTS
			(
				SELECT 1
				FROM @PortfolioNoteAccountReceivableDetail pnardTmp
				JOIN Billing.InvoiceDetail id  ON pnardTmp.EntityName = 'InvoiceDetail' AND pnardTmp.EntityId = id.Id
				WHERE (id.Balance + (pnardTmp.Value * IIF(@Nature = 1, 1, -1))) < 0
			)
			BEGIN
				INSERT INTO @TableErrors
					SELECT CONCAT(
							'El detalle de la factura ', i.InvoiceNumber,
							' quedaría con saldo negativo. Saldo actual: ', CONVERT(VARCHAR(30), id.Balance),
							', valor a ajustar: ', CONVERT(VARCHAR(30), pnardTmp.Value),
							'. Servicio relacionado - CUPS ', ISNULL(ce.Code, 'N/A'), ': ', ISNULL(ce.Description, 'N/A')
						)
					FROM @PortfolioNoteAccountReceivableDetail pnardTmp
					JOIN Billing.InvoiceDetail id  ON pnardTmp.EntityName = 'InvoiceDetail' AND pnardTmp.EntityId = id.Id
					JOIN Billing.Invoice i ON i.Id = id.InvoiceId
					LEFT JOIN Billing.ServiceOrderDetail sod  ON id.ServiceOrderDetailId = sod.Id
					LEFT JOIN Contract.CUPSEntity ce  ON sod.CUPSEntityId = ce.Id
					WHERE (id.Balance + (pnardTmp.Value * IIF(@Nature = 1, 1, -1))) < 0
			END

			IF EXISTS (SELECT 1 FROM @TableErrors)
			BEGIN
				SELECT @Message = STUFF((
						SELECT DISTINCT CHAR(13) + CHAR(10) + ' - ' + Message
						FROM @TableErrors
						FOR XML PATH(N''), TYPE).value(N'.[1]', N'nvarchar(max)'), 1, 2, N'')
            
				SELECT	@CodeResult = 999, 
						@MessageResult = @Message, 
						@Id = @Id, 
						@Code = ''
				RETURN
			END

			/************************************************** ************* **************************************************/

			DECLARE @ConfirmationUser VARCHAR(20) = CASE WHEN @Status = 2 THEN @User ELSE NULL END
			DECLARE @ConfirmationDate DATETIME = CASE WHEN @Status = 2 THEN [Common].[GETDATE]() ELSE NULL END

			IF @Id = 0
			BEGIN 
				EXEC Common.SP_GetSequence 160, @IdForm, @OperatingUnitId, NULL, NULL, @IsManual OUT, @Code OUT, @Code_Output OUT, @Message_Output OUT

				IF @Code_Output <> 0
				BEGIN
					SELECT	@CodeResult = 999, 
							@MessageResult = REPLACE(@Message_Output, '{0}', 'Notas de Cartera'), 
							@Id = 0, 
							@Code = ''
					RETURN
				END

				--Inserto la cabecera de la nota
				INSERT INTO [Portfolio].[PortfolioNote]
				(
					OperatingUnitId,Code,NoteDate,CustomerId,Observations,Nature,NoteType,PortfolioAdvanceId,PortfolioTransferId,
					[Status],CreationUser,CreationDate,ModificationUser,ModificationDate,ConfirmationUser,ConfirmationDate,CurrencyId,EntityName
				)
				SELECT @OperatingUnitId,@Code,@NoteDate,@CustomerId,@Observations,@Nature,@NoteType,@PortfolioAdvanceId,IIF(@PortfolioTransferId > 0, @PortfolioTransferId, NULL),
					@Status,@User,[Common].[GETDATE](),@ConfirmationUser,@ConfirmationDate,@ConfirmationUser,@ConfirmationDate,@CurrencyId,@EntityName
            
				--obtengo el id de la nota
				SET @Id = SCOPE_IDENTITY()
			END        
			ELSE 
			BEGIN 
				--si se esta actualizando
				UPDATE [Portfolio].[PortfolioNote]
				SET	[OperatingUnitId] = @OperatingUnitId,
					[Code] = @Code,
					[NoteDate] = @NoteDate,
					[CustomerId] = @CustomerId,
					[Observations] = @Observations,
					[Nature] = @Nature,
					[NoteType] = @NoteType,
					[PortfolioAdvanceId] = @PortfolioAdvanceId,
					[PortfolioTransferId] = @PortfolioTransferId,
					[Status] = @Status,
					[ModificationUser] = @User,
					[ModificationDate] = [Common].[GETDATE](),
					[ConfirmationUser] = @ConfirmationUser,
					[ConfirmationDate] = @ConfirmationDate,
					[CurrencyId]	   = @CurrencyId,
					[EntityName] = @EntityName
				WHERE Id = @Id 
			END

			------------------  Detalles ------------------
			
			INSERT INTO [Portfolio].[PortfolioNoteDetail]
			(
				PortfolioNoteId, PortfolioNoteConceptId, MainAccountId, ThirdPartyId, CostCenterId, Nature, Value, RetentionConceptId, BaseValue, Percentage, Observations, IdGeneralLedgerIVA, IvaRate, TotalConcept
			)
			SELECT @Id,PortfolioNoteConceptId,MainAccountId,ThirdPartyId,CostCenterId,Nature,Value,RetentionConceptId,BaseValue,Percentage,Observations, IdGeneralLedgerIVA, IvaRate, TotalConcept 
			FROM @PortfolioNoteDetail WHERE ChangeTracker = 'Added'

			UPDATE pnd
				SET [PortfolioNoteConceptId] = pndTmp.PortfolioNoteConceptId,
					[MainAccountId] = pndTmp.MainAccountId,
					[ThirdPartyId] = pndtmp.ThirdPartyId,
					[CostCenterId] = pndTmp.CostCenterId,
					[Nature] = pndTmp.Nature,
					[Value] = pndTmp.Value,
					[RetentionConceptId] = pndTmp.RetentionConceptId,
					[BaseValue] = pndTmp.BaseValue,
					[Percentage] = pndTmp.Percentage,
					[Observations] = pndTmp.Observations
			FROM @PortfolioNoteDetail pndTmp 
			JOIN Portfolio.PortfolioNoteDetail pnd  ON pndTmp.Id = pnd.Id 
			WHERE pnd.PortfolioNoteId = @Id AND pndTmp.ChangeTracker <> 'Added'
            
			If @NoteType = 2 begin --Esto lo hago por que cuando es pago de cuota no se esta enviando el main account
				UPDATE pnaraTmp
					SET MainAccountId = ara.MainAccountId
				FROM @PortfolioNoteAccountReceivableAdvance pnaraTmp
				JOIN Portfolio.AccountReceivableAccounting ara WITH(NOLOCK) on ara.Id = pnaraTmp.AccountReceivableAccountingId
			end
			------------------  Facturas ------------------
			INSERT INTO [Portfolio].[PortfolioNoteAccountReceivableAdvance]
			(
				PortfolioNoteId, AccountReceivableId, AccountReceivableShareId, MainAccountId, AccountReceivableAccountingId, PortfolioAdvanceId, AdjusmentValue, PercentageValue, PreviousBalance, Balance, ConceptId
			)
			SELECT @Id,[AccountReceivableId],[AccountReceivableShareId],[MainAccountId],[AccountReceivableAccountingId],[PortfolioAdvanceId],[AdjusmentValue],[PercentageValue],[PreviousBalance],[Balance],[ConceptId] 
			FROM @PortfolioNoteAccountReceivableAdvance WHERE ChangeTracker ='Added'

			UPDATE pnaraTmp
				SET pnaraTmp.Id = pnara.Id
			FROM @PortfolioNoteAccountReceivableAdvance pnaraTmp
			JOIN Portfolio.PortfolioNoteAccountReceivableAdvance pnara  ON pnaraTmp.AccountReceivableId = pnara.AccountReceivableId
			WHERE pnara.PortfolioNoteId = @Id AND pnaraTmp.ChangeTracker = 'Added'

			UPDATE pnara
				SET [AccountReceivableId] = pnaraTmp.AccountReceivableId,
					[AccountReceivableShareId] = pnaraTmp.AccountReceivableShareId,
					[MainAccountId] = pnaraTmp.MainAccountId,
					[AccountReceivableAccountingId] = pnaraTmp.AccountReceivableAccountingId,
					[PortfolioAdvanceId] = pnaraTmp.PortfolioAdvanceId,
					[AdjusmentValue] = pnaraTmp.AdjusmentValue,
					[PercentageValue] = pnaraTmp.PercentageValue,
					[PreviousBalance] = pnaraTmp.PreviousBalance,
					[Balance] = pnaraTmp.Balance,
					[ConceptId] = pnaraTmp.ConceptId
			FROM @PortfolioNoteAccountReceivableAdvance pnaraTmp 
			JOIN Portfolio.PortfolioNoteAccountReceivableAdvance pnara  ON pnaraTmp.Id = pnara.Id 
			WHERE pnara.PortfolioNoteId = @Id AND pnaraTmp.ChangeTracker <> 'Added'

			--------------  Detalle Facturas --------------
			INSERT INTO [Portfolio].[PortfolioNoteAccountReceivableDetail]
			(
				PortfolioNoteAccountReceivableId, EntityName, EntityId, MainAccountId, CostCenterId, Value,BaseValue,TaxValue,TaxPercentage,TaxId,ThirdPartyId
			)
			SELECT pnaraTmp.Id, pnardTmp.EntityName, pnardTmp.EntityId, pnardTmp.MainAccountId, pnardTmp.CostCenterId, pnardTmp.Value,pnardTmp.BaseValue,pnardTmp.TaxValue,pnardTmp.TaxPercentage,pnardTmp.TaxId,pnardTmp.ThirdPartyId
			FROM @PortfolioNoteAccountReceivableAdvance pnaraTmp
			JOIN @PortfolioNoteAccountReceivableDetail pnardTmp ON pnaraTmp.PortfolioNoteAccountReceivableAdvanceIdTmp = pnardTmp.PortfolioNoteAccountReceivableAdvanceIdTmp
			WHERE pnardTmp.ChangeTracker ='Added'

			UPDATE pnard
				SET [EntityName] = pnardTmp.EntityName,
					[EntityId] = pnardTmp.EntityId,
					[MainAccountId] = pnardTmp.MainAccountId,
					[CostCenterId] = pnardTmp.CostCenterId,
					[Value] = pnardTmp.Value,
					[BaseValue] = pnardTmp.BaseValue,
					[TaxValue] = pnardTmp.TaxValue,
					[TaxPercentage] = pnardTmp.TaxPercentage,
					[TaxId] = pnardTmp.TaxId,
					[ThirdPartyId] = pnardTmp.ThirdPartyId
			FROM @PortfolioNoteAccountReceivableDetail pnardTmp 
			JOIN Portfolio.PortfolioNoteAccountReceivableDetail pnard  ON pnardTmp.Id = pnard.Id
			JOIN Portfolio.PortfolioNoteAccountReceivableAdvance pnara  ON pnard.PortfolioNoteAccountReceivableId = pnara.Id 
			WHERE pnara.PortfolioNoteId = @Id AND pnardTmp.ChangeTracker <> 'Added'

			------------ Distribución Anticipo ------------
			INSERT INTO [Portfolio].[PortfolioNoteDistribution]
			(
				PortfolioNoteId, CustomerId, MainAccountId, CostCenterId, Value, PortfolioAdvanceId
			)
			SELECT @Id ,[CustomerId],[MainAccountId],[CostCenterId],[Value],[PortfolioAdvanceId] 
			FROM @PortfolioNoteDistribution 
			WHERE ChangeTracker ='Added'			

			UPDATE pnd
			SET [CustomerId] = pndTmp.CustomerId,
				[MainAccountId] = pndTmp.MainAccountId,
				[CostCenterId] = pndTmp.CostCenterId,
				[Value] = pndTmp.Value,
				[PortfolioAdvanceId] = pndTmp.PortfolioAdvanceId
			FROM @PortfolioNoteDistribution pndTmp 
			JOIN Portfolio.PortfolioNoteDistribution pnd  ON pndTmp.Id = pnd.Id 
			WHERE pnd.PortfolioNoteId = @Id AND pndTmp.ChangeTracker <> 'Added'

			/*******************************************************************************************************************/

			IF(@Status = 2) BEGIN
				IF @NoteType IN (1, 2, 3, 4, 6)
				BEGIN
					IF @NoteType IN (1, 2, 6)

					BEGIN 
						---- Retenciones de la factura ----

						INSERT INTO Portfolio.PortfolioNoteAccountReceivableRetention
						(
							PortfolioNoteAccountReceivableId,InvoiceCustomerRetentionId, Nature, RetentionRate, BaseValue, Value
						)
						SELECT pnara.Id, icr.Id, IIF(pnd.PortfolioNoteId IS NULL, @Nature, IIF(@Nature = 1, 2, 1)), icr.RetentionRate, ISNULL(pnd.BaseValue, pnara.AdjusmentValue), ROUND(ISNULL(pnd.BaseValue, pnara.AdjusmentValue) * icr.RetentionRate / 100, 2)
						FROM Portfolio.PortfolioNoteAccountReceivableAdvance pnara 
						JOIN Portfolio.AccountReceivable ar  ON pnara.AccountReceivableId = ar.Id AND ar.AccountReceivableType NOT IN (4, 6)
						JOIN Billing.InvoiceCustomerRetention icr  ON ar.InvoiceId = icr.InvoiceId
						JOIN Common.CustomerRetention cr  ON icr.CustomerRetentionId = cr.Id
						LEFT JOIN 
						(
							SELECT PortfolioNoteId, PortfolioNoteConceptId, RetentionConceptId, ISNULL(SUM(BaseValue), 0) BaseValue
							FROM Portfolio.PortfolioNoteDetail 
							GROUP BY PortfolioNoteId, PortfolioNoteConceptId, RetentionConceptId
						) pnd ON pnara.PortfolioNoteId = pnd.PortfolioNoteId AND cr.PortfolioNoteConceptId = pnd.PortfolioNoteConceptId AND cr.RetentionConceptId = pnd.RetentionConceptId
						WHERE pnara.PortfolioNoteId = @Id
							AND icr.CalculateTaxAdvance IN (1, 2)
							AND ROUND(ISNULL(pnd.BaseValue, pnara.AdjusmentValue) * icr.RetentionRate / 100, 2) <> 0

						--factura total o factura cuota
						UPDATE ara
							SET ara.Balance =
								CASE
									WHEN (ara.Balance + (pnara.AdjusmentValue * IIF(@Nature = ma.Nature, 1, -1)) + ISNULL(pnarr.Value, 0)) < 0
										AND ABS(ara.Balance + (pnara.AdjusmentValue * IIF(@Nature = ma.Nature, 1, -1)) + ISNULL(pnarr.Value, 0)) <= @RoundTolerance
									THEN 0
									ELSE ara.Balance + (pnara.AdjusmentValue * IIF(@Nature = ma.Nature, 1, -1)) + ISNULL(pnarr.Value, 0)
								END
						FROM Portfolio.PortfolioNoteAccountReceivableAdvance pnara 
						JOIN Portfolio.AccountReceivableAccounting ara  ON pnara.AccountReceivableAccountingId  = ara.Id 
						JOIN GeneralLedger.MainAccounts ma  ON ara.MainAccountId = ma.Id
						LEFT JOIN
						(
							SELECT pnara.Id, SUM(pnarr.Value * IIF(@Nature = 1, -1, 1)) Value
							FROM Common.CustomerRetention cr 
							JOIN Billing.InvoiceCustomerRetention icr  ON cr.Id = icr.CustomerRetentionId
							JOIN Portfolio.PortfolioNoteAccountReceivableRetention pnarr  ON icr.Id = pnarr.InvoiceCustomerRetentionId
							JOIN Portfolio.PortfolioNoteAccountReceivableAdvance pnara  ON pnarr.PortfolioNoteAccountReceivableId = pnara.Id
							LEFT JOIN Portfolio.PortfolioNoteDetail pnd  ON cr.PortfolioNoteConceptId = pnd.PortfolioNoteConceptId AND cr.RetentionConceptId = pnd.RetentionConceptId AND pnara.PortfolioNoteId = pnd.PortfolioNoteId
							WHERE pnara.PortfolioNoteId = @Id AND icr.CalculateTaxAdvance = 2 AND pnd.Id IS NULL
							GROUP BY pnara.Id
						) pnarr ON pnara.Id = pnarr.Id
						WHERE pnara.PortfolioNoteId = @Id

						UPDATE ars
							SET ars.Balance =
									CASE
										WHEN (ars.Balance + (pnara.AdjusmentValue * IIF(@Nature = ma.Nature, 1, -1)) + ISNULL(pnarr.Value, 0)) < 0
											AND ABS(ars.Balance + (pnara.AdjusmentValue * IIF(@Nature = ma.Nature, 1, -1)) + ISNULL(pnarr.Value, 0)) <= @RoundTolerance
										THEN 0
										ELSE ars.Balance + (pnara.AdjusmentValue * IIF(@Nature = ma.Nature, 1, -1)) + ISNULL(pnarr.Value, 0)
									END,
								ars.DebitValue = ars.DebitValue + IIF(@Nature = 1, pnara.AdjusmentValue, 0) - IIF(@Nature = 1, ISNULL(pnarr.Value, 0), 0),
								ars.CreditValue = ars.CreditValue + IIF(@Nature = 2, pnara.AdjusmentValue, 0) - IIF(@Nature = 2, ISNULL(pnarr.Value, 0), 0)
						FROM Portfolio.PortfolioNoteAccountReceivableAdvance pnara 
						JOIN Portfolio.AccountReceivableShare ars  ON pnara.AccountReceivableShareId = ars.Id                    
						JOIN Portfolio.AccountReceivableAccounting ara  ON ars.AccountReceivableId = ara.AccountReceivableId 
						JOIN GeneralLedger.MainAccounts ma  ON ara.MainAccountId = ma.Id
						LEFT JOIN
						(
							SELECT pnara.Id, SUM(pnarr.Value * IIF(@Nature = 1, -1, 1)) Value
							FROM Common.CustomerRetention cr 
							JOIN Billing.InvoiceCustomerRetention icr  ON cr.Id = icr.CustomerRetentionId
							JOIN Portfolio.PortfolioNoteAccountReceivableRetention pnarr  ON icr.Id = pnarr.InvoiceCustomerRetentionId
							JOIN Portfolio.PortfolioNoteAccountReceivableAdvance pnara  ON pnarr.PortfolioNoteAccountReceivableId = pnara.Id
							LEFT JOIN Portfolio.PortfolioNoteDetail pnd  ON cr.PortfolioNoteConceptId = pnd.PortfolioNoteConceptId AND cr.RetentionConceptId = pnd.RetentionConceptId AND pnara.PortfolioNoteId = pnd.PortfolioNoteId
							WHERE pnara.PortfolioNoteId = @Id AND icr.CalculateTaxAdvance = 2 AND pnd.Id IS NULL
							GROUP BY pnara.Id
						) pnarr ON pnara.Id = pnarr.Id
						WHERE pnara.PortfolioNoteId = @Id

						UPDATE pnara 
							SET pnara.PreviousBalance = ar.Balance, 
								pnara.Balance = ar.Balance + (pnara.AdjusmentValue * IIF(@Nature = 1, 1, -1))
						FROM Portfolio.PortfolioNoteAccountReceivableAdvance pnara 
						JOIN Portfolio.AccountReceivable ar  ON pnara.AccountReceivableId = ar.Id
						WHERE pnara.PortfolioNoteId = @Id

						-----  Detalles de la factura -----

						UPDATE idbi 
							SET idbi.Balance = idbi.Balance + (pnard.Value * IIF(@Nature = 1, 1, -1))
						FROM Portfolio.PortfolioNoteAccountReceivableAdvance pnara 
						JOIN Portfolio.PortfolioNoteAccountReceivableDetail pnard  ON pnara.Id = pnard.PortfolioNoteAccountReceivableId
						JOIN Billing.InvoiceDetailBasicInvoice idbi  ON pnard.EntityName = 'InvoiceDetailBasicInvoice' AND pnard.EntityId = idbi.Id
						WHERE pnara.PortfolioNoteId = @Id

						UPDATE id 
							SET id.Balance = id.Balance + (pnard.Value * IIF(@Nature = 1, 1, -1))
						FROM Portfolio.PortfolioNoteAccountReceivableAdvance pnara 
						JOIN Portfolio.PortfolioNoteAccountReceivableDetail pnard  ON pnara.Id = pnard.PortfolioNoteAccountReceivableId
						JOIN Billing.InvoiceDetail id  ON pnard.EntityName = 'InvoiceDetail' AND pnard.EntityId = id.Id
						WHERE pnara.PortfolioNoteId = @Id

						UPDATE ids 
							SET ids.Balance = ids.Balance + (pnard.Value * IIF(@Nature = 1, 1, -1))
						FROM Portfolio.PortfolioNoteAccountReceivableAdvance pnara 
						JOIN Portfolio.PortfolioNoteAccountReceivableDetail pnard  ON pnara.Id = pnard.PortfolioNoteAccountReceivableId
						JOIN Billing.InvoiceDetailSurgical ids  ON pnard.EntityName = 'InvoiceDetailSurgical' AND pnard.EntityId = ids.Id
						WHERE pnara.PortfolioNoteId = @Id

						UPDATE idps 
							SET idps.Balance = idps.Balance + (pnard.Value * IIF(@Nature = 1, 1, -1))
						FROM Portfolio.PortfolioNoteAccountReceivableAdvance pnara 
						JOIN Portfolio.PortfolioNoteAccountReceivableDetail pnard  ON pnara.Id = pnard.PortfolioNoteAccountReceivableId
						JOIN Billing.InvoiceDetailProductSales idps  ON pnard.EntityName = 'InvoiceDetailProductSales' AND pnard.EntityId = idps.Id
						WHERE pnara.PortfolioNoteId = @Id

						--------- --------------- ---------

					--Obtengo los datos que se asignarán en el xml para enviar al sp
					SELECT @SubXml = CONVERT(xml, 
					(
						SELECT Data.*
						FROM (
							SELECT pnara.AccountReceivableId, @NoteDate DocumentDate, ar.InvoiceNumber, pnara.AdjusmentValue as Value
							FROM Portfolio.PortfolioNoteAccountReceivableAdvance pnara 
							JOIN Portfolio.AccountReceivable ar  ON pnara.AccountReceivableId = ar.Id
							JOIN Portfolio.AccountReceivableAccounting ara  ON pnara.AccountReceivableAccountingId  = ara.Id 
							JOIN GeneralLedger.MainAccounts ma  ON ara.MainAccountId = ma.Id
							WHERE pnara.PortfolioNoteId = @Id AND ma.Nature = IIF(@Nature = 1, 2, 1)
						) AS Data
						For xml AUTO, TYPE, ELEMENTS
					))

						--Se ejecuta el sp que genera los detalles de comprobante para provision y deterioro
						INSERT @ResultProvisionAndDeterioration
							EXEC Portfolio.SP_CreateDetailsJournalVoucher @SubXml

						--Se valida que el sp no haya devuelto algun error
						IF EXISTS (SELECT 1 FROM @ResultProvisionAndDeterioration WHERE [Status] = 0 OR [Status] = 2)
						BEGIN
							SELECT @Message = STUFF((
									SELECT DISTINCT CHAR(13) + CHAR(10) + ' - ' + Message
									FROM @ResultProvisionAndDeterioration 
									WHERE [Status] = 0 OR [Status] = 2
									FOR XML PATH(N''), TYPE).value(N'.[1]', N'nvarchar(max)'), 1, 2, N'')

							SELECT	@CodeResult = 999, 
									@MessageResult = ISNULL(@Message, ''),
									@Id = 0, 
									@Code = ''
							RETURN 
						END

					--Si no hay ningun error entonces se procede a crear los demas detalles de comprobante de provision y deterioro
					--siempre y cuando hayan registros en estado ok
					IF EXISTS (SELECT 1 FROM @ResultProvisionAndDeterioration WHERE [Status] = 1)
					BEGIN
							INSERT INTO @JournalVoucherDetails 
							SELECT 0, 0, rpd.IdMainAccount, 
								   CASE WHEN ma.HandlesThirdParty = 1 THEN rpd.IdThirdParty ELSE NULL END, 
								   CASE WHEN ma.HandlesCostCenter = 1 THEN rpd.IdCostCenter ELSE NULL END,
								   rpd.DebitValue, rpd.CreditValue, 'Detalle generado con Provision/Deterioro', null, null, null, null  
							FROM @ResultProvisionAndDeterioration rpd
							JOIN GeneralLedger.MainAccounts ma WITH(NOLOCK) ON ma.Id = rpd.IdMainAccount
							WHERE rpd.[Status] = 1
						END

						--se actualiza el saldo del AccountReceivable
						UPDATE ar
							SET Balance = ara.Balance
						FROM Portfolio.PortfolioNoteAccountReceivableAdvance pnara 
						JOIN Portfolio.AccountReceivable ar  ON pnara.AccountReceivableId = ar.Id
						JOIN 
						(
							SELECT AccountReceivableId, SUM(Balance) Balance 
							FROM Portfolio.AccountReceivableAccounting 
							GROUP BY AccountReceivableId
						) ara ON ara.AccountReceivableId = ar.Id 
						WHERE pnara.PortfolioNoteId = @Id

						/* Ajuste diferencial por documento de cuentas por cobrar */
						DELETE from @responseRevaluation
								SET @MessageJvRevaluation = ''
						declare @AccountReceivableId int,
								@ValueAdjustment DECIMAL(20,2)

						DECLARE Revaluation_Cursor CURSOR FOR  
						
								SELECT	ara.AccountReceivableId,
										SUM((pnara.AdjusmentValue) + ISNULL(pnarr.Value, 0)) as  ValueAdjustment
								FROM Portfolio.PortfolioNoteAccountReceivableAdvance pnara 
								JOIN Portfolio.AccountReceivableAccounting ara  ON pnara.AccountReceivableAccountingId  = ara.Id 
								JOIN GeneralLedger.MainAccounts ma  ON ara.MainAccountId = ma.Id
								LEFT JOIN
								(
									SELECT pnara.Id, SUM(pnarr.Value * IIF(@Nature = 1, -1, 1)) Value
									FROM Common.CustomerRetention cr 
									JOIN Billing.InvoiceCustomerRetention icr  ON cr.Id = icr.CustomerRetentionId
									JOIN Portfolio.PortfolioNoteAccountReceivableRetention pnarr  ON icr.Id = pnarr.InvoiceCustomerRetentionId
									JOIN Portfolio.PortfolioNoteAccountReceivableAdvance pnara  ON pnarr.PortfolioNoteAccountReceivableId = pnara.Id
									LEFT JOIN Portfolio.PortfolioNoteDetail pnd  ON cr.PortfolioNoteConceptId = pnd.PortfolioNoteConceptId AND cr.RetentionConceptId = pnd.RetentionConceptId AND pnara.PortfolioNoteId = pnd.PortfolioNoteId
									WHERE pnara.PortfolioNoteId = @Id AND icr.CalculateTaxAdvance = 2 AND pnd.Id IS NULL
									GROUP BY pnara.Id
								) pnarr ON pnara.Id = pnarr.Id
								WHERE pnara.PortfolioNoteId = @Id
								GROUP BY ara.AccountReceivableId

						OPEN Revaluation_Cursor  
						FETCH NEXT FROM Revaluation_Cursor   
						INTO @AccountReceivableId,@ValueAdjustment
  
						WHILE @@FETCH_STATUS = 0  
						BEGIN 
							
							INSERT @responseRevaluation
							EXEC [Portfolio].[SP_AccountReceivableRevaluation]
								@AccountReceivableId,
								@ValueAdjustment,
								@Code,
								@Id,
								'PortfolioNote',
								@User,
								@NoteDate

						if EXISTS (SELECT 1 FROM @responseRevaluation WHERE Code = '999')
						Begin
								set @Message = (select STRING_AGG(MessageResult, ', ')  from @responseRevaluation)

								CLOSE Revaluation_Cursor;  
								DEALLOCATE Revaluation_Cursor;

								SELECT	@CodeResult = 999, 
								@MessageResult = CONCAT(@Message, ' - ', 'Notas de Cartera'), 
								@Id = 0, 
								@Code = ''						
								RETURN																
							End 
							SET @MessageJvRevaluation = (select STRING_AGG(MessageResult, ', ')  from @responseRevaluation)
						NEXT_ROW:
						FETCH NEXT FROM Revaluation_Cursor   
						INTO @AccountReceivableId,@ValueAdjustment

						END
						CLOSE Revaluation_Cursor;  
						DEALLOCATE Revaluation_Cursor;

						/*------------------------------------------------------------*/

						-- realizar la modificación al reconocimiento respectivo de la vigencia del año actual
						IF EXISTS 
						(
							SELECT 1								
									FROM @PortfolioNoteAccountReceivableAdvance pnara
									JOIN Portfolio.AccountReceivable ar  ON pnara.AccountReceivableId = ar.Id
									WHERE YEAR(ar.AccountReceivableDate) = YEAR(GETDATE())
								GROUP BY AccountReceivableId, AccountReceivableDate
							UNION	
							SELECT 1								
								FROM @PortfolioNoteAccountReceivableAdvance pnara
								JOIN Portfolio.AccountReceivable ar  ON pnara.AccountReceivableId = ar.Id
								JOIN Billing.Invoice i  ON ar.InvoiceId = i.Id
								WHERE year(i.InvoiceDate) = YEAR(GETDATE())
							GROUP BY AccountReceivableId, InvoiceDate
						)
						BEGIN
							EXEC [Portfolio].[SP_GenerateRecognitionModificationByPortfolioNoteId_Output] @Id, @User, @Code_Output OUTPUT, @Message_Output OUTPUT

							IF ISNULL(@Code_Output, 999) <> 0 
							BEGIN
								SELECT	@CodeResult = 999, 
										@MessageResult = ISNULL(@Message_Output, 'Error al generar la modificacion del reconocimiento'), 
										@Id = 0, 
										@Code = ''
								RETURN
							END
						END
						ELSE
						BEGIN

							INSERT INTO @TableErrors 
								SELECT DISTINCT CONCAT('No se genero la modificación del reconocimiento de la factura  ', ar.InvoiceNumber, ' debido a que no corresponde con el año de la vigencia')
								FROM @PortfolioNoteAccountReceivableAdvance pnaraTmp							
								JOIN Portfolio.AccountReceivable ar  ON pnaraTmp.AccountReceivableId = ar.Id
						END

						SET @Message = ISNULL(@Message, '') + IIF(@Message_Output = '', '', IIF(ISNULL(@Message, '') = '', '', CHAR(13) + CHAR(10)) + @Message_Output)
					END
					ELSE IF(@NoteType = 3) 
					BEGIN --anticipos
						
						UPDATE pa
							SET pa.Balance =
									CASE
										WHEN (pa.Balance + (pnara.AdjusmentValue * IIF(@Nature = 2, 1, -1))) < 0
											AND ABS(pa.Balance + (pnara.AdjusmentValue * IIF(@Nature = 2, 1, -1))) <= @RoundTolerance
										THEN 0
										ELSE pa.Balance + (pnara.AdjusmentValue * IIF(@Nature = 2, 1, -1))
									END,
								pa.DebitValue = pa.DebitValue + IIF(@Nature = 1, pnara.AdjusmentValue, 0),
								pa.CreditValue = pa.CreditValue + IIF(@Nature = 2, pnara.AdjusmentValue, 0)
						FROM Portfolio.PortfolioNoteAccountReceivableAdvance pnara 
						JOIN Portfolio.PortfolioAdvance pa  ON pnara.PortfolioAdvanceId = pa.Id 
						JOIN GeneralLedger.MainAccounts ma  ON pa.MainAccountId = ma.Id
						WHERE pnara.PortfolioNoteId = @Id

						UPDATE pnara 
							SET pnara.PreviousBalance = pa.Balance - (pnara.AdjusmentValue * IIF(@Nature = 2, 1, -1)), 
								pnara.Balance = pa.Balance
						FROM Portfolio.PortfolioNoteAccountReceivableAdvance pnara 
						JOIN Portfolio.PortfolioAdvance pa  ON pnara.PortfolioAdvanceId = pa.Id
						JOIN GeneralLedger.MainAccounts ma  ON pa.MainAccountId = ma.Id
						WHERE pnara.PortfolioNoteId = @Id
					END
					ELSE IF @NoteType = 4 
					BEGIN --distribucion de anticipos
						DECLARE @PortfolioNoteDistributionRows INT = 1,
								@PortfolioNoteDistributionId INT = 0,
								@PortfolioNoteDistributionValue DECIMAL(18,2),
								------------------------------------------
								@CodePortfolioAdvance VARCHAR(20)
					
						WHILE @PortfolioNoteDistributionRows > 0
						BEGIN
							SELECT TOP 1
								@PortfolioNoteDistributionId = pnd.Id,
								@PortfolioNoteDistributionValue = pnd.Value
							FROM Portfolio.PortfolioNoteDistribution pnd 
							WHERE pnd.PortfolioNoteId = @Id
								AND pnd.Id > @PortfolioNoteDistributionId
							ORDER BY pnd.Id

							SET @PortfolioNoteDistributionRows = @@ROWCOUNT
							IF @PortfolioNoteDistributionRows = 0
							BEGIN
								BREAK
							END

							EXEC Common.SP_GetSequence 160, 1507, @OperatingUnitId, NULL, NULL, @IsManual OUT, @CodePortfolioAdvance OUT, @Code_Output OUT, @Message_Output OUT

							IF @Code_Output <> 0
							BEGIN
								SELECT	@CodeResult = 999, 
										@MessageResult = REPLACE(@Message_Output, '{0}', 'Anticipos de Cartera'), 
										@Id = 0, 
										@Code = ''
								RETURN
							END

							IF @IsManual = 1
							BEGIN
								SELECT	@CodeResult = 999, 
										@MessageResult = 'La secuencia de Anticipos de Cartera no puede ser manual', 
										@Id = 0, 
										@Code = ''
								RETURN
							END

							--se crea un anticipo por cada detalle de la distribucion
							INSERT INTO [Portfolio].[PortfolioAdvance] 
							(
								[Code],[ThirdPartyId],[MainAccountId],[CostCenterId],[DocumentDate],
								[CustomerId],[Value],[TransferValue],[DebitValue],[CreditValue],[DistributionValue],[Balance],[Observations],[OpeningBalance],
								[Status],[CreationUser],[CreationDate],[ModificationUser],[ModificationDate],[ConfirmationUser],[ConfirmationDate],[CurrencyId],[TRMValue],ValueInCurrencyHeader
							)
							SELECT 
								@codePortfolioAdvance,tp.Id,pnd.MainAccountId,pnd.CostCenterId,@NoteDate,
								c.Id,pnd.Value,0,0,0,0,pnd.Value,CONCAT('Anticipo creado desde la distribución ',@Code),0,
								2,@User,[Common].[GETDATE](),@User,[Common].[GETDATE](),@User,[Common].[GETDATE](),@CurrencyId,0,0
							FROM Portfolio.PortfolioNoteDistribution pnd 
							JOIN Common.Customer c  ON pnd.CustomerId = c.Id
							JOIN Common.ThirdParty tp  ON c.ThirdPartyId  = tp.Id						
							WHERE pnd.Id = @PortfolioNoteDistributionId

							--actualixo el detalle de la distribucion
							UPDATE Portfolio.PortfolioNoteDistribution 
								SET PortfolioAdvanceId = SCOPE_IDENTITY() 
							WHERE Id = @PortfolioNoteDistributionId

							--actualizo el anticipo
							UPDATE Portfolio.PortfolioAdvance 
								SET Balance -= @PortfolioNoteDistributionValue,
									DistributionValue += @PortfolioNoteDistributionValue  ,
									ValueInCurrencyHeader += @PortfolioNoteDistributionValue 
							WHERE Id = @PortfolioAdvanceId 
						END
					END

					IF @NoteType IN(3, 4) BEGIN
						/*********************** AJUSTE DIFERENCIAL ANTICPOS ******************************/
							DELETE from @responseRevaluation
							SET @MessageJvRevaluation = ''
							declare @ListPortfolioAdvance TABLE (	Id INT  NOT NULL,
																	ValueAdjustment NUMERIC(20,2) NOT NULL,
																	EntityName VARCHAR(250),
																	EntityId INT,
																	DocumentDate DATE)

							DECLARE @ListPortfolioAdvanceXml as XML,
									@XmlOutput as XML
								
								INSERT INTO @ListPortfolioAdvance (Id,ValueAdjustment,EntityName,EntityId, DocumentDate)
								
								SELECT pa.Id,pa.ValueAdjustment,pa.EntityName,pa.PortfolioNoteId,pa.NoteDate
								FROM (								
										SELECT	pa.Id,
												ABS(pnara.AdjusmentValue) ValueAdjustment,
												'PortfolioNote' as EntityName,
												pnara.PortfolioNoteId,
												pn.NoteDate
										FROM Portfolio.PortfolioNoteAccountReceivableAdvance pnara 
										JOIN Portfolio.PortfolioAdvance pa  ON pnara.PortfolioAdvanceId = pa.Id 
										JOIN Portfolio.PortfolioNote pn WITH(NOLOCK) ON pn.Id=pnara.PortfolioNoteId
										WHERE pnara.PortfolioNoteId = @Id and @NoteType=3
										
										UNION ALL

										SELECT	pn.PortfolioAdvanceId as Id,
												SUM(pnd.Value) ValueAdjustment,
												'PortfolioNote' as EntityName,
												pn.Id as PortfolioNoteId,
												pn.NoteDate
										FROM Portfolio.PortfolioNoteDistribution pnd 
										JOIN Portfolio.PortfolioNote pn WITH(NOLOCK) on pnd.PortfolioNoteId=pn.Id
										WHERE pnd.PortfolioNoteId = @Id and  @NoteType=4
										GROUP by pn.Id,pn.PortfolioAdvanceId,pn.NoteDate

										) pa

								SELECT @ListPortfolioAdvanceXml = CONVERT(xml, 
																			(
																				SELECT * FROM @ListPortfolioAdvance AS PortfolioAdvance 
																				For xml AUTO,TYPE, ELEMENTS
																			))
				
					
								EXEC [Portfolio].[SP_PortfolioAdvanceRevaluation_Output]
										@ListPortfolioAdvanceXml,
										@User,@XmlOutput OUTPUT
						
								INSERT @responseRevaluation
								SELECT
								t.x.value('Code[1]', 'Varchar(20)')  Code,
								t.x.value('MessageOutput[1]', 'varchar(max)')  MessageOutput,
								t.x.value('JournalVoucherId[1]', 'INT')  JournalVoucherId
								from @XmlOutput.nodes('/TableResult') t(x);

								set @MessageJvRevaluation = (select CONCAT( 'Ajuste diferencial anticipo : ',STRING_AGG(MessageResult, ', '))   from @responseRevaluation)

								if EXISTS(select 1 from @responseRevaluation WHERE Code = '999')
								Begin	
						
									SELECT	@CodeResult = 999, 
											@MessageResult = CONCAT(@MessageJvRevaluation, ' - ', 'Notas de cartera'), 
											@Id = 0, 
											@Code = ''
									RETURN
								End 
					END
					/*********************** ************************ ******************************/

					/************************************* GENERACION COMPROBANTE CONTABLE *************************************/

					DECLARE @DetailJournalVoucher as VARCHAR(MAX) = 'Nota de Cartera '
					IF (SELECT count(*) FROM @PortfolioNoteAccountReceivableAdvance tmp JOIN Portfolio.AccountReceivable ar  ON ar.Id = tmp.AccountReceivableId) > 0 BEGIN
						DECLARE @facturas as VARCHAR(MAX)
						SELECT @facturas=stuff((SELECT '   ' + ar.InvoiceNumber
						FROM @PortfolioNoteAccountReceivableAdvance tmp JOIN Portfolio.AccountReceivable ar  ON ar.Id = tmp.AccountReceivableId
						for xml path(N''), type).value(N'.[1]', N'nvarchar(MAX)'), 1, 2, N'')
						set @DetailJournalVoucher += 'Facturas ' + @facturas
					END
					else IF (SELECT count(*) FROM @PortfolioNoteAccountReceivableAdvance tmp JOIN Portfolio.PortfolioAdvance pa  ON pa.Id = tmp.PortfolioAdvanceId) > 0 BEGIN
						DECLARE @anticipos as VARCHAR(MAX)
						SELECT @anticipos=stuff((SELECT '   ' + pa.Code
						FROM @PortfolioNoteAccountReceivableAdvance tmp JOIN Portfolio.PortfolioAdvance pa  ON pa.Id = tmp.PortfolioAdvanceId
						for xml path(N''), type).value(N'.[1]', N'nvarchar(MAX)'), 1, 2, N'')
						set @DetailJournalVoucher += 'Anticipos ' + @anticipos
					END
					--inserto la cabecera del comprobante
					INSERT INTO @JournalVoucher 
					(
						IdJournalVoucher,
						VoucherDate,
						Status,
						Detail,
						EntityId,
						EntityCode,
						EntityName,
						CurrencyId
					)
					SELECT 
						IIF(@Nature =1, sp.JournalVoucherTypeDebitNotesId, sp.JournalVoucherTypeCreditNotesId),
						@NoteDate,
						@Status,
						CONCAT(@DetailJournalVoucher, ', ' , @Observations),
						@Id,
						@Code,
						'PortfolioNote',
						@CurrencyId
					FROM Portfolio.SettingPortfolio sp 
					WHERE OperatingUnitId = @OperatingUnitId
					
					--inserto los detalles del comprobante
					DECLARE @OfficialLegalBook int =(SELECT top 1 Id from GeneralLedger.LegalBook WITH(NOLOCK) where OfficialBook=1)

					IF @EntityName = 'ReverseBasicBilling' BEGIN
						
						INSERT INTO @JournalVoucherDetails 

						select	0,
								0, 
								CASE
									WHEN jvd.Detail like 'Detalle cuenta ingreso de la Factura Basica%' AND DATEPART(YEAR,common.GETDATE()) > DATEPART(YEAR,bb.DocumentDate) and  @ReversalPreviousYearsGenericBillingMainAccountId is not null then @ReversalPreviousYearsGenericBillingMainAccountId
									ELSE  jvd.IdMainAccount
								END ,
								jvd.IdThirdParty,
								jvd.IdCostCenter,
								jvd.CreditValue,
								jvd.DebitValue,
								jvd.Detail,
								jvd.IdRetention,
								jvd.RetentionRate,
								jvd.BaseValue, 
								jvd.BillingValue
						from GeneralLedger.JournalVouchers jv WITH(NOLOCK)
						JOIN GeneralLedger.JournalVoucherDetails jvd WITH(NOLOCK) on jvd.IdAccounting = jv.Id
						JOIN Billing.BasicBilling bb WITH(NOLOCK) ON bb.Id = @EntityId
						JOIN (	select top 1 lo.Id
								from GeneralLedger.LegalBook lo
								where lo.OfficialBook = 1 ) l on jv.LegalBookId = l.Id
						where jv.EntityId =  @EntityId and EntityName = 'BasicBilling'  and @CurrencyId = @OfficialCurrencyId 

						UNION ALL

						select	0,
								0,
								CASE
									WHEN jvd.Detail like 'Detalle cuenta ingreso de la Factura Basica%' AND DATEPART(YEAR,common.GETDATE()) > DATEPART(YEAR,bb.DocumentDate) and  @ReversalPreviousYearsGenericBillingMainAccountId is not null then @ReversalPreviousYearsGenericBillingMainAccountId
									ELSE  ha.OfficialMainAccountId
								END,
								jvd.IdThirdParty,
								jvd.IdCostCenter,
								jvd.CreditValue, 
								jvd.DebitValue,
								jvd.Detail,
								jvd.IdRetention,
								jvd.RetentionRate,
								jvd.BaseValue,
								jvd.BillingValue
						from GeneralLedger.JournalVouchers jv WITH(NOLOCK)
						JOIN GeneralLedger.JournalVoucherDetails jvd WITH(NOLOCK) on jvd.IdAccounting = jv.Id
						JOIN GeneralLedger.HomologationAccount ha ON ha.MainAccountId = jvd.IdMainAccount
						JOIN Billing.BasicBilling bb WITH(NOLOCK) ON bb.Id = @EntityId
						JOIN (	select top 1 lo.Id
								from GeneralLedger.LegalBook lo
								where lo.OfficialCurrencyId = @CurrencyId  ) l on jv.LegalBookId = l.Id
						where jv.EntityId =  @EntityId and EntityName = 'BasicBilling' and @CurrencyId <> @OfficialCurrencyId

						IF EXISTS(	SELECT 1 
									FROM @JournalVoucherDetails jvd
									JOIN GeneralLedger.MainAccounts ma ON ma.Id = jvd.IdMainAccount
									WHERE ma.HandlesCostCenter = 1 AND jvd.IdCostCenter IS NULL) 
						BEGIN
								-- Actualizar el centro de costos desde la unidad funcional de la factura básica
								UPDATE jvdTarget
								SET jvdTarget.IdCostCenter = fu.CostCenterId
								FROM @JournalVoucherDetails jvdTarget
								JOIN GeneralLedger.MainAccounts ma ON ma.Id = jvdTarget.IdMainAccount
								CROSS APPLY (
									SELECT TOP 1 bbd.FunctionalUnitId
									FROM Billing.BasicBillingDetail bbd 
									WHERE bbd.BasicBillingId = @EntityId
								) bbd
								JOIN Payroll.FunctionalUnit fu  ON fu.Id = bbd.FunctionalUnitId
								WHERE ma.HandlesCostCenter = 1 AND jvdTarget.IdCostCenter IS NULL
						END
					END
					else begin
						IF @NoteType = 4
						BEGIN
							--se inserta un detalle al credito por la suma de la distribucion
							DECLARE @totalDistribution numeric(18,2) 
							SELECT @totalDistribution = SUM(Value ) FROM @PortfolioNoteDistribution
							INSERT INTO @JournalVoucherDetails 
							SELECT 0,0,pa.MainAccountId,
								   CASE WHEN ma.HandlesThirdParty = 1 THEN pa.ThirdPartyId ELSE NULL END,
								   CASE WHEN ma.HandlesCostCenter = 1 THEN pa.CostCenterId ELSE NULL END,
								   case @Nature when 1 then @totalDistribution else 0 END, case @Nature when 2 then @totalDistribution else 0 END,null,null,0,0,0
							FROM Portfolio.PortfolioAdvance pa 
							JOIN GeneralLedger.MainAccounts ma WITH(NOLOCK) ON ma.Id = pa.MainAccountId
							WHERE pa.Id = @PortfolioAdvanceId 

							--se inserta un detalle al debito por cada distribucion
							INSERT INTO @JournalVoucherDetails 
							SELECT 0,0,pndTmp.MainAccountId,
								   CASE WHEN ma.HandlesThirdParty = 1 THEN c.ThirdPartyId ELSE NULL END,
								   CASE WHEN ma.HandlesCostCenter = 1 THEN pndTmp.CostCenterId ELSE NULL END,
								   case @Nature when 1 then 0 else pndTmp.Value END,case @Nature when 2 then 0 else pndTmp.Value END,null,null,0,0,0
							FROM @PortfolioNoteDistribution pndTmp 
							JOIN Common.Customer c  ON pndTmp.CustomerId = c.Id
							JOIN GeneralLedger.MainAccounts ma WITH(NOLOCK) ON ma.Id = pndTmp.MainAccountId
						END
						ELSE 
						BEGIN
							--se inserta un detalle en el comprobante por cada detalle de la nota
							INSERT INTO @JournalVoucherDetails 
							SELECT	0,
									0,
									ptd.MainAccountId,
									CASE WHEN ma.HandlesThirdParty = 1 THEN ptd.ThirdPartyId ELSE NULL END,
									CASE WHEN ma.HandlesCostCenter = 1 THEN ptd.CostCenterId ELSE NULL END,
									case ptd.Nature 
										when 1 then ptd.Value 
									else 0 END,
									case  ptd.Nature 
										when 2 then ptd.Value
									else 0 END,
									null,
									ptd.RetentionConceptId,
									ptd.Percentage,
									ptd.BaseValue,
									IIF(ISNULL(ptd.TotalConcept, 0) < ptd.BaseValue, ptd.BaseValue, ISNULL(ptd.TotalConcept, 0)) 
							FROM @PortfolioNoteDetail ptd
							INNER JOIN Portfolio.PortfolioNoteConcept pnc WITH(NOLOCK) ON pnc.Id = ptd.PortfolioNoteConceptId
							INNER JOIN GeneralLedger.MainAccounts ma WITH(NOLOCK) ON ma.Id = ptd.MainAccountId
							where  ISNULL(@EntityName,'') ='' OR @EntityName = 'Glosas'

							UNION ALL

							-- Se agrega el IVA(cuando maneja impuesto)
							SELECT		0,
										0,
										gli.IdAccountSale,
										CASE WHEN ma.HandlesThirdParty = 1 THEN pnd.ThirdPartyId ELSE NULL END,
										CASE WHEN ma.HandlesCostCenter = 1 THEN pnd.CostCenterId ELSE NULL END,
										case pnd.Nature 
											when 1 then pnd.IvaRate 
										else 0 END,
										case  pnd.Nature 
											when 2 then pnd.IvaRate
										else 0 END,
										null,
										pnd.RetentionConceptId,
										pnd.Percentage,
										pnd.BaseValue,
										IIF(ISNULL(pnd.TotalConcept, 0) < pnd.BaseValue, pnd.BaseValue, ISNULL(pnd.TotalConcept, 0)) 								
							FROM @PortfolioNoteDetail pnd
							INNER JOIN GeneralLedger.GeneralLedgerIVA gli WITH(NOLOCK) ON gli.Id = pnd.IdGeneralLedgerIVA
							INNER JOIN Portfolio.PortfolioNoteConcept pnc WITH(NOLOCK) ON pnc.Id = pnd.PortfolioNoteConceptId
							INNER JOIN GeneralLedger.MainAccounts ma WITH(NOLOCK) ON ma.Id = gli.IdAccountSale
							WHERE (ISNULL(@EntityName,'') ='' OR @EntityName = 'Glosas') and pnc.HandleTax = 1 AND ISNULL(pnd.IvaRate, 0) <> 0

						DECLARE @ThirdPartyIdCopay INT
						
						IF @EntityName = 'ReverseBasicBilling' BEGIN
							SELECT TOP 1 @ThirdPartyIdCopay = ThirdPartyEntityCopayId
							FROM Billing.BasicBilling WITH(NOLOCK)
							WHERE Id = @EntityId
						END

						INSERT INTO @JournalVoucherDetails 
						SELECT	0,
								0,
								iif( DATEPART(YEAR,common.GETDATE()) > DATEPART(YEAR,bb.DocumentDate) and @ReversalPreviousYearsGenericBillingMainAccountId is not null,@ReversalPreviousYearsGenericBillingMainAccountId,ptd.MainAccountId),
								CASE WHEN ma.HandlesThirdParty = 1 THEN ISNULL(@ThirdPartyIdCopay, ptd.ThirdPartyId) ELSE NULL END,
								CASE WHEN ma.HandlesCostCenter = 1 THEN ptd.CostCenterId ELSE NULL END,
								case ptd.Nature 
									when 1 then ptd.Value 
								else 0 END,
								case  ptd.Nature 
									when 2 then ptd.Value 
								else 0 END,
								null,--esta
								ptd.RetentionConceptId,
								ptd.Percentage,
								ptd.BaseValue,
								IIF(ISNULL(ptd.TotalConcept, 0) < ptd.BaseValue, ptd.BaseValue, ISNULL(ptd.TotalConcept, 0)) 
						FROM @PortfolioNoteDetail ptd
						JOIN Portfolio.PortfolioNoteAccountReceivableAdvance pnara WITH(NOLOCK) on pnara.PortfolioNoteId =@Id
						JOIN Portfolio.AccountReceivable ar with(NOLOCK) on ar.Id =pnara.AccountReceivableId
						JOIN Billing.BasicBilling bb WITH(NOLOCK) on bb.InvoiceId = ar.InvoiceId
						JOIN GeneralLedger.MainAccounts ma WITH(NOLOCK) ON ma.Id = iif( DATEPART(YEAR,common.GETDATE()) > DATEPART(YEAR,bb.DocumentDate) and @ReversalPreviousYearsGenericBillingMainAccountId is not null,@ReversalPreviousYearsGenericBillingMainAccountId,ptd.MainAccountId)
						where @EntityName = 'ReverseBasicBilling'
				
						--se insertan los detalles por cada PortfolioNoteAccountReceivableAdvance
						INSERT INTO @JournalVoucherDetails 
							SELECT	0,
									0,
									IIF(@NoteType = 3, pa.MainAccountId, ara.MainAccountId),
									CASE WHEN ma.HandlesThirdParty = 1 THEN IIF(@NoteType = 3, pa.ThirdPartyId, ar.ThirdPartyId) ELSE NULL END,
									CASE WHEN ma.HandlesCostCenter = 1 THEN IIF(@NoteType = 3, COALESCE(pa.CostCenterId, crd.IdCostCenter), ara.CostCenterId) ELSE NULL END,
									IIF(@Nature = 1, pnara.AdjusmentValue, 0), 
									IIF(@Nature = 2, pnara.AdjusmentValue, 0),
									null,null,0,0,0
							FROM Portfolio.PortfolioNoteAccountReceivableAdvance pnara 
							LEFT JOIN Portfolio.AccountReceivable ar  ON ar.Id = pnara.AccountReceivableId 
							LEFT JOIN Portfolio.AccountReceivableAccounting ara  ON ar.Id = ara.AccountReceivableId 
										AND (pnara.MainAccountId is null or 
												(
													ara.MainAccountId = pnara.MainAccountId 
													and 
													(@NoteType IN (1,6) OR (ar.Value - ar.Balance) >= pnara.AdjusmentValue)
												)
											)
							LEFT JOIN Portfolio.PortfolioAdvance pa  ON pnara.PortfolioAdvanceId = pa.Id
							LEFT JOIN Treasury.CashReceiptDetails crd  ON crd.Id = pa.CashReceiptDetailId
							JOIN GeneralLedger.MainAccounts ma WITH(NOLOCK) ON ma.Id = IIF(@NoteType = 3, pa.MainAccountId, ara.MainAccountId)
							WHERE pnara.PortfolioNoteId = @Id

						--se insertan los detalles de las retenciones
						INSERT INTO @JournalVoucherDetails 
							SELECT 0,0,
								ara.MainAccountId,
								CASE WHEN ma.HandlesThirdParty = 1 THEN ar.ThirdPartyId ELSE NULL END,
								CASE WHEN ma.HandlesCostCenter = 1 THEN ara.CostCenterId ELSE NULL END,
								IIF(@Nature = 2, pnarr.Value, 0) DebitValue, 
								IIF(@Nature = 1, pnarr.Value, 0) CreditValue,
								'Anticipos de impuestos' Detail,
								null,0,0,0
							FROM Common.CustomerRetention cr 
							JOIN Billing.InvoiceCustomerRetention icr  ON cr.Id = icr.CustomerRetentionId
							JOIN Portfolio.PortfolioNoteAccountReceivableRetention pnarr  ON icr.Id = pnarr.InvoiceCustomerRetentionId
							JOIN Portfolio.PortfolioNoteAccountReceivableAdvance pnara  ON pnarr.PortfolioNoteAccountReceivableId = pnara.Id
							JOIN Portfolio.AccountReceivable ar  ON ar.Id = pnara.AccountReceivableId 
							JOIN Portfolio.AccountReceivableAccounting ara  ON ar.Id = ara.AccountReceivableId AND ara.MainAccountId = pnara.MainAccountId
							LEFT JOIN Portfolio.PortfolioNoteDetail pnd  ON cr.PortfolioNoteConceptId = pnd.PortfolioNoteConceptId AND cr.RetentionConceptId = pnd.RetentionConceptId AND pnara.PortfolioNoteId = pnd.PortfolioNoteId
							JOIN GeneralLedger.MainAccounts ma WITH(NOLOCK) ON ma.Id = ara.MainAccountId
							WHERE pnara.PortfolioNoteId = @Id AND icr.CalculateTaxAdvance = 2 AND pnd.Id IS NULL
						UNION ALL
							SELECT 0,0,
								pnc.IdAccount,
								CASE WHEN ma.HandlesThirdParty = 1 THEN ar.ThirdPartyId ELSE NULL END,
								CASE WHEN ma.HandlesCostCenter = 1 THEN ar.CostCenterId ELSE NULL END,
								IIF(@Nature = 1, pnarr.Value, 0) DebitValue, 
								IIF(@Nature = 2, pnarr.Value, 0) CreditValue,
								'Anticipos de impuestos' Detail,
								null,0,0,0
							FROM Portfolio.PortfolioNoteConcept pnc 
							JOIN Common.CustomerRetention cr  ON pnc.Id = cr.PortfolioNoteConceptId
							JOIN Billing.InvoiceCustomerRetention icr  ON cr.Id = icr.CustomerRetentionId
							JOIN Portfolio.PortfolioNoteAccountReceivableRetention pnarr  ON icr.Id = pnarr.InvoiceCustomerRetentionId
							JOIN Portfolio.PortfolioNoteAccountReceivableAdvance pnara  ON pnarr.PortfolioNoteAccountReceivableId = pnara.Id
							JOIN Portfolio.AccountReceivable ar  ON ar.Id = pnara.AccountReceivableId 
							LEFT JOIN Portfolio.PortfolioNoteDetail pnd  ON cr.PortfolioNoteConceptId = pnd.PortfolioNoteConceptId AND cr.RetentionConceptId = pnd.RetentionConceptId AND pnara.PortfolioNoteId = pnd.PortfolioNoteId
							JOIN GeneralLedger.MainAccounts ma WITH(NOLOCK) ON ma.Id = pnc.IdAccount
							WHERE pnara.PortfolioNoteId = @Id AND icr.CalculateTaxAdvance = 2 AND pnd.Id IS NULL

							/***********INSERCION DETALLES CXC, NOTA TIPO (6) DETALLE***************/
							--***FACTURA SALUD***
							-- Override de tercero solo para Glosas NoteType=6 cuando pnard.ThirdPartyId viene informado (Type 2).
							-- La CxC (bloque superior) sigue usando siempre ar.ThirdPartyId.
							--======================================================================
								INSERT INTO @JournalVoucherDetails 
								SELECT	0,
										0,
										ma.Id MainAccountId,
										IIF(ma.HandlesThirdParty=1,
											CASE WHEN @EntityName = 'Glosas' AND pnard.ThirdPartyId IS NOT NULL THEN pnard.ThirdPartyId ELSE ar.ThirdPartyId END,
											null) ThirdPartyId,
										IIF(ma.HandlesCostCenter = 1,pnard.CostCenterId,null) CostCenterId ,
										IIF(@Nature = 2, sum(pnard.BaseValue), 0), 
										IIF(@Nature = 1,sum( pnard.BaseValue), 0),
										null,
										null,
										0,0,0
								FROM Portfolio.PortfolioNoteAccountReceivableAdvance pnara 
								JOIN Portfolio.PortfolioNoteAccountReceivableDetail pnard WITH(NOLOCK) on pnara.Id =pnard.PortfolioNoteAccountReceivableId
								JOIN Portfolio.AccountReceivable ar  ON ar.Id = pnara.AccountReceivableId
								JOIN GeneralLedger.MainAccounts ma WITH(NOLOCK) on ma.Id = pnard.MainAccountId
								WHERE pnara.PortfolioNoteId = @Id AND @NoteType =6
								GROUP by ma.Id,ar.ThirdPartyId,pnard.ThirdPartyId,pnard.CostCenterId,ma.HandlesThirdParty,ma.HandlesCostCenter

								INSERT INTO @JournalVoucherDetails 
								SELECT	0,
										0,
										ma.Id MainAccountId,
										IIF(ma.HandlesThirdParty=1,
											CASE WHEN @EntityName = 'Glosas' AND pnard.ThirdPartyId IS NOT NULL THEN pnard.ThirdPartyId ELSE ar.ThirdPartyId END,
											null) ThirdPartyId,
										IIF(ma.HandlesCostCenter = 1,pnard.CostCenterId,null) CostCenterId ,
										IIF(@Nature = 2, sum(pnard.TaxValue), 0), 
										IIF(@Nature = 1,sum(pnard.TaxValue), 0),
										CONCAT(gi.Code,' - ', gi.Name),
										null,
										gi.Percentage,
										sum(pnard.BaseValue),
										sum(pnard.BaseValue)
								FROM Portfolio.PortfolioNoteAccountReceivableAdvance pnara 
								JOIN Portfolio.PortfolioNoteAccountReceivableDetail pnard WITH(NOLOCK) on pnara.Id =pnard.PortfolioNoteAccountReceivableId
								JOIN Portfolio.AccountReceivable ar  ON ar.Id = pnara.AccountReceivableId 
								JOIN GeneralLedger.GeneralLedgerIVA gi with(NOLOCK) on pnard.TaxId = gi.Id
								JOIN GeneralLedger.MainAccounts ma WITH(NOLOCK) on ma.Id = gi.IdAccountSale
								WHERE pnara.PortfolioNoteId = @Id AND @NoteType =6 and pnard.TaxValue >0
								GROUP by ma.Id,ar.ThirdPartyId,pnard.ThirdPartyId,pnard.CostCenterId,gi.Code,gi.Name,gi.Percentage,ma.HandlesThirdParty,ma.HandlesCostCenter
							--========================================================================================================	
							/*************************************************************************/
						END
					end

					SELECT @SubXml = CONVERT
					(
						XML,
						(
							SELECT * FROM @JournalVoucher JournalVoucher 
							JOIN @JournalVoucherDetails JournalVoucherDetail ON JournalVoucher.Id = JournalVoucherDetail.IdAccounting
							FOR XML AUTO,TYPE, ELEMENTS
						)
					)
					----------

					insert @resultJournalVoucher exec GeneralLedger.SP_CreateAndValidateJournalVoucherMovement @SubXml,@User 
					select 
						@Code_Output = rjv.code, 
						@Message_Output = rjv.MessageResult, 
						@JournalVoucherId_Output = rjv.IdJournalVoucher
					from @resultJournalVoucher rjv
					
					IF ISNULL(@Code_Output, 999) <> 0 
					BEGIN
						SELECT	@CodeResult = 999, 
								@MessageResult = ISNULL(@Message_Output, 'Error al generar el comprobante contable'), 
								@Id = 0, 
								@Code = ''
						RETURN
					END

					--consultar tipo de documento
					SELECT @Message_Output = 'Se generó el Comprobante contable de tipo ' + jvt.Code + ' - ' + jvt.Name
					FROM Portfolio.SettingPortfolio sp
					JOIN GeneralLedger.JournalVoucherTypes jvt ON jvt.Id = IIF(@Nature = 1, sp.JournalVoucherTypeDebitNotesId, sp.JournalVoucherTypeCreditNotesId)
					WHERE sp.OperatingUnitId = @OperatingUnitId

					SET @Message = ISNULL(@Message, '') + IIF(@Message_Output = '', '', IIF(ISNULL(@Message, '') = '', '', CHAR(13) + CHAR(10)) + @Message_Output)

						------------------
					-- si es notade tipo 6 => factura detallada se genera sp para crear comporbnate dereversion parcial
					IF @NoteType = 6
					BEGIN

					DECLARE @InvoiceId as INT
					DECLARE @PortfolioCode as varchar(max)
			        --Se obtiene el id del invoice y se valida que exista el registro en facturacion basica con este id
					select top 1 
						@InvoiceId= i.Id,
						@PortfolioCode = pn.Code
					from Portfolio.PortfolioNoteAccountReceivableAdvance pnar WITH(NOLOCK)
					join Portfolio.PortfolioNote pn WITH(NOLOCK) on pn.Id = pnar.PortfolioNoteId
					join Portfolio.AccountReceivable ar WITH(NOLOCK) on pnar.AccountReceivableId = ar.Id
					join Billing.Invoice i WITH(NOLOCK) on  i.id= ar.InvoiceId
					join Billing.BasicBilling bb WITH(NOLOCK) on bb.InvoiceId = i.Id
					where pn.Id=@Id and i.DocumentType=6

						if @InvoiceId IS NOT NULL
						BEGIN
							DECLARE @TableResult as TABLE (Code_Output INT,[Message] VARCHAR(150),  InvoiceId int)

							insert into  @TableResult
							EXEC  [Portfolio].[SP_GenerateJournalVoucher_ReversalsConsignmentSale] @InvoiceId, @Id, @PortfolioCode,@OperatingUnitId, @User
							
							select top 1 @Code_Output = Code_Output, @Message_Output= [message]
							from @TableResult

							IF ISNULL(@Code_Output, 999) <> 0 
							BEGIN
								SELECT	@CodeResult = 999, 
										@MessageResult = ISNULL(@Message_Output, 'Error al generar el comprobante contable de reversión'), 
										@Id = 0, 
										@Code = ''
								RETURN
							END
									--consultar tipo de documento
							SELECT @Message_Output = 'Se generó el Comprobante contable para reversion de reconocimiento de los productos en consignacion de la factura basica de tipo ' + jvt.Code + ' - ' + jvt.Name
							FROM GeneralLedger.JournalVoucherTypes jvt 
							INNER JOIN Billing.SettingsBilling SB ON JVT.ID = SB.BasicBillingAnnulmentJournalVoucherTypeId
							where SB.IdOperatingUnit= @OperatingUnitId
							

							SET @Message = ISNULL(@Message, '') + IIF(@Message_Output = '', '', IIF(ISNULL(@Message, '') = '', '', CHAR(13) + CHAR(10)) + @Message_Output)
						END
					END
				END
				ELSE IF @NoteType = 5
				BEGIN
					DECLARE @XmlReverseTransfer XML
					SELECT @XmlReverseTransfer = CONVERT
											(
												XML, 
												(
													SELECT 
														PortfolioNote.*
													FROM 
													(
														SELECT 
															@Id Id,
															@User as CodeUser,
															@CompanyType as CompanyType,
															@EntityName as EntityName
													) PortfolioNote
													For xml AUTO,TYPE, ELEMENTS
												)
											)

					EXEC [Portfolio].[SP_ReversePortfolioTransferXML_Output] @XmlReverseTransfer, @Code_Output OUTPUT, @Message_Output OUTPUT

					IF ISNULL(@Code_Output, 999) <> 0 
					BEGIN
						SELECT	@CodeResult = 999, 
								@MessageResult = ISNULL(@Message_Output, 'Error al generar reversar el cruce de anticipo'), 
								@Id = 0, 
								@Code = ''
						RETURN
					END

					SET @Message = ISNULL(@Message, '') + IIF(@Message_Output = '', '', IIF(ISNULL(@Message, '') = '', '', CHAR(13) + CHAR(10)) + @Message_Output)
				END
			END
		END

		/**************************************************  TABLA DE CONTROL **************************************************/

		IF @Status = 1
		BEGIN
			IF NOT EXISTS (SELECT 1 FROM Portfolio.PortfolioControl  WHERE DocumentType = @DocumentType AND DocumentNumber = @Code)
			BEGIN
				INSERT INTO Portfolio.PortfolioControl (DocumentNumber, DocumentType, DocumentUser, DocumentDate)
				SELECT @Code, @DocumentType, @User, @NoteDate
			END
		END
		ELSE
		BEGIN
			DELETE FROM Portfolio.PortfolioControl WHERE DocumentType = @DocumentType AND DocumentNumber = @Code
		END	

		/****************************************************** RESULTADO ******************************************************/

		SELECT	@CodeResult = 0, 
					@MessageResult = CASE @Status
											WHEN 2 THEN CONCAT('Se guardó y confirmó la Nota de Cartera con código ', @Code)
											WHEN 3 THEN CONCAT('Se anuló la Nota de Cartera con código ', @Code)
											ELSE CONCAT('Se guardó la Nota de Cartera con código ', @Code)
										END + IIF(ISNULL(@Message, '') = '', '', CHAR(13) + CHAR(10) + @Message) + ' ' + COALESCE(@MessageJvRevaluation,''),
					@Id = @Id, 
					@Code = @Code
	END TRY
	BEGIN CATCH
		SELECT	@CodeResult = 999,
				@MessageResult = 'SP_SavePortfolioNote_Output: ' + ERROR_MESSAGE() + ' - Linea: ' + CAST(ERROR_LINE() AS VARCHAR(20)),
				@Id = 0,
				@Code = ''
	END CATCH
END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Procedimiento para crear, actualizar, confirmar o anular notas de cartera (notas débito o crédito) en el módulo de cartera. Recibe los datos de la nota en formato XML, gestiona el ciclo de vida completo del documento —incluyendo sus líneas de detalle contable, anticipos y distribuciones— y genera o reversa los comprobantes contables correspondientes en el libro oficial de contabilidad. Consulta la configuración de facturación por unidad operativa y los parámetros de la empresa (moneda oficial, tipo de libro legal) para asegurar que los movimientos contables se registren correctamente. Retorna como resultado el identificador, código, estado y mensajes de éxito o error de la operación.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'PROCEDURE', @level1name = N'SP_SavePortfolioNote_Output';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'PROCEDURE', @level1name = N'SP_SavePortfolioNote_Output';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Persiste, actualiza, confirma o anula una nota de cartera (débito/crédito), recalcula saldos de facturas/cuotas/anticipos, genera retenciones, ajustes diferenciales y el comprobante contable asociado.', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'PROCEDURE', @level1name=N'SP_SavePortfolioNote_Output';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'La nota debe existir en estado Borrador (Status=1) para poder ser modificada; si Status<>1 se aborta indicando ''Confirmado'' o ''Anulado''.; Debe existir configuración de cartera en Portfolio.SettingPortfolio para la unidad operativa (OperatingUnitId).; El mes/año de NoteDate debe estar abierto en GeneralLedger.ClosedMonth (Status=1); de lo contrario se rechaza por mes cerrado.; Si CurrencyId viene vacío se asume la moneda oficial de GeneralLedger.CompanySettings.OfficialCurrencyId.; Para NoteType IN (1,2,3,6) debe existir al menos un detalle (excepto NoteType=6 que admite vacío) y al menos una factura/anticipo a ajustar.; Para NoteType=4 (distribución de anticipos) la naturaleza debe ser Débito (Nature=1) y el saldo del anticipo debe cubrir la suma de la distribución.; Los detalles, facturas/anticipos y subdetalles existentes en BD que no vengan en el XML se consideran no procesados y se rechazan.; La suma de débitos debe igualar la suma de créditos sobre el conjunto (anticipos/facturas + detalles + detalle de facturas para tipo 6).', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'PROCEDURE', @level1name=N'SP_SavePortfolioNote_Output';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'PROCEDURE', @level1name=N'SP_SavePortfolioNote_Output';
-- GO
