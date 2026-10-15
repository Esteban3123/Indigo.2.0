CREATE PROCEDURE [GeneralLedger].[SP_UpdateJournalVoucherMovement]
    @MovementId INT
AS
BEGIN
    SET NOCOUNT ON;

    -- Variables esenciales
    DECLARE @CodeMessage       INT = 0;
    DECLARE @Message           VARCHAR(MAX);
    DECLARE @JournalVoucherXml XML;
    DECLARE @CodeUser          VARCHAR(20);
    DECLARE @VoucherDate       DATETIME;
    DECLARE @IsClosedYear      TINYINT;
    DECLARE @Status            TINYINT;
    DECLARE @LegalBookId       INT;
	DECLARE @DateTRM            DATE;
    DECLARE @OriginalCurrencyId INT;
	DECLARE @EntityName         VARCHAR(250);
	DECLARE @EntityNameConversion  VARCHAR(250);
	DECLARE @JournalVoucherID	INT;
    DECLARE @Detail			   VARCHAR(MAX);

	--
	CREATE TABLE #JournalVoucherTmp (
		Id						INT,
        Consecutive				BIGINT,
		VoucherDate				DATETIME,
		LegalBookId				INT
	);

    -- Tabla temporal para los detalles originales
    CREATE TABLE #JournalVoucherDetails (
		IdJournalVoucher INT,
        IdMainAccount INT,
        IdThirdParty  INT,
        IdCostCenter  INT,
        DebitValue    DECIMAL(21,5),
        CreditValue   DECIMAL(21,5),
        Detail        VARCHAR(MAX),
        IdRetention   INT,
        RetentionRate DECIMAL(6,3),
        BaseValue     DECIMAL(18,2),
        BillingValue  DECIMAL(18,2),
		IsOriginal	  BIT
    );

    -- 1. Leer los datos del movimiento
    SELECT
        @JournalVoucherXml    = am.JournalVoucherXml,
        @CodeUser             = am.CreationUser,
        @VoucherDate          = am.VoucherDate,
        @LegalBookId          = am.LegalBookId,
		@Detail				  = am.Detail,
		@EntityName           = am.EntityName,
        @Status               = am.JournalVoucherXml.value('(/JournalVoucher/Status)[1]','tinyint'),
        @IsClosedYear         = ISNULL(am.JournalVoucherXml.value('(/JournalVoucher/IsClosedYear)[1]','tinyint'),0),
        @OriginalCurrencyId   = am.JournalVoucherXml.value('(/JournalVoucher/CurrencyId)[1]','int'),
		@DateTRM              = am.JournalVoucherXml.value('(/JournalVoucher/DateTRM)[1]','date'),
        @JournalVoucherID     = am.JournalVoucherXml.value('(/JournalVoucher/Id)[1]','int')
    FROM GeneralLedger.AccountingMovement am WITH (NOLOCK)
    WHERE am.Id = @MovementId;

    IF @JournalVoucherXml IS NULL
        RETURN;

	IF @DateTRM IS NULL OR @DateTRM ='' or @DateTRM = '0001-01-01' BEGIN
		SET @DateTRM =cast( @VoucherDate as DATE)
	END
	SET @EntityNameConversion =
        CASE WHEN @EntityName LIKE 'Automatic%' OR @EntityName IN ('AutomaticPortfolioTransfer','AutomaticPortfolioNote')
             THEN 'Invoice' ELSE @EntityName END;

    -- 2. Validaciones de duplicidad/homólogos
    DECLARE @OfficialLegalBookId INT;

    SELECT @OfficialLegalBookId = Id
    FROM GeneralLedger.LegalBook WITH (NOLOCK)
    WHERE OfficialBook = 1 AND Status = 1;

    IF EXISTS (
        SELECT 1
        FROM GeneralLedger.JournalVouchers j1
        JOIN GeneralLedger.JournalVouchers j2 ON j1.AccountingMovementId = j2.AccountingMovementId AND j1.Id <> j2.Id
        WHERE j1.AccountingMovementId = @MovementId AND j1.Status <> j2.Status
    )
    BEGIN
        SET @CodeMessage = 999;
        SET @Message = 'Existe un comprobante homólogo en estado diferente';
        SELECT @CodeMessage AS CodeMessage, @Message AS [Message], @MovementId AS IdJournalVoucher;
        RETURN;
    END

    IF @LegalBookId <> @OfficialLegalBookId
       AND EXISTS (
           SELECT 1
           FROM GeneralLedger.JournalVouchers
           WHERE AccountingMovementId = @MovementId
           GROUP BY AccountingMovementId
           HAVING COUNT(1) > 1
       )
    BEGIN
        SET @CodeMessage = 999;
        SET @Message = 'No se permite procesar un libro no oficial cuando existen homólogos';
        SELECT @CodeMessage AS CodeMessage, @Message AS [Message], @MovementId AS IdJournalVoucher;
        RETURN;
    END

	-- ===============================================================================

	-- Comprobantes contables asociados al movimiento
	INSERT INTO #JournalVoucherTmp
		SELECT 
			jv.Id,
			jv.Consecutive,
			jv.VoucherDate,
			jv.LegalBookId
		FROM GeneralLedger.JournalVouchers jv
		JOIN GeneralLedger.LegalBook lb ON jv.LegalBookId = lb.Id
		WHERE jv.AccountingMovementId = @MovementId;

	IF NOT EXISTS (SELECT 1 FROM #JournalVoucherTmp WHERE Id = @JournalVoucherID)
	BEGIN
        SET @CodeMessage = 999;
        SET @Message = 'El comprobante contable no esta asociado al movimiento';
        SELECT @CodeMessage AS CodeMessage, @Message AS [Message], @MovementId AS IdJournalVoucher;
        RETURN;
    END

    -- 3. Extraer detalles originales del XML
    INSERT INTO #JournalVoucherDetails
		SELECT
			@JournalVoucherID,
			t.x.value('(IdMainAccount/text())[1]','int'),
			NULLIF(t.x.value('(IdThirdParty/text())[1]','int'),0),
			NULLIF(t.x.value('(IdCostCenter/text())[1]','int'),0),
			ISNULL(t.x.value('(DebitValue/text())[1]','decimal(21,5)'),0),
			ISNULL(t.x.value('(CreditValue/text())[1]','decimal(21,5)'),0),
			t.x.value('(Detail/text())[1]','varchar(max)'),
			t.x.value('(IdRetention/text())[1]','int'),
			t.x.value('(RetentionRate/text())[1]','decimal(6,3)'),
			t.x.value('(BaseValue/text())[1]','decimal(18,2)'),
			t.x.value('(BillingValue/text())[1]','decimal(18,2)'),
			1 IsOriginal
		FROM @JournalVoucherXml.nodes('/JournalVoucher/JournalVoucherDetail') t(x)
		WHERE ISNULL(t.x.value('(IsDelete/text())[1]','bit'),0) = 0;

	IF EXISTS (SELECT 1 FROM #JournalVoucherTmp jvt WHERE jvt.Id <> @JournalVoucherID)
	BEGIN
		-- Insercion de los detalles homologos
		INSERT INTO #JournalVoucherDetails
			SELECT jvt.Id,
				   ha.MainAccountId,
				   od.IdThirdParty,
				   od.IdCostCenter,
				   [Common].[CurrencyConverterByModule](od.DebitValue,@OriginalCurrencyId,lb.OfficialCurrencyId,NULL,@EntityNameConversion,@DateTRM),
				   [Common].[CurrencyConverterByModule](od.CreditValue,@OriginalCurrencyId,lb.OfficialCurrencyId,NULL,@EntityNameConversion,@DateTRM),
				   od.Detail,
				   od.IdRetention,
				   od.RetentionRate,
				   [Common].[CurrencyConverterByModule](od.BaseValue,@OriginalCurrencyId,lb.OfficialCurrencyId,NULL,@EntityNameConversion,@DateTRM),
				   [Common].[CurrencyConverterByModule](od.BillingValue,@OriginalCurrencyId,lb.OfficialCurrencyId,NULL,@EntityNameConversion,@DateTRM),
				   0 IsOriginal
			FROM #JournalVoucherTmp jvt
			JOIN GeneralLedger.MainAccounts ma_homol ON jvt.LegalBookId = ma_homol.LegalBookId
			JOIN GeneralLedger.HomologationAccount ha ON ma_homol.Id = ha.MainAccountId
			JOIN #JournalVoucherDetails od ON ha.OfficialMainAccountId = od.IdMainAccount AND od.IsOriginal = 1
			JOIN GeneralLedger.LegalBook lb ON jvt.LegalBookId = lb.Id
			WHERE jvt.Id <> @JournalVoucherID;

		-- Validar que la cantidad de detalles del original sea la misma de los homologados 
		IF EXISTS (
			SELECT 1
			FROM 
				(
					SELECT COUNT(1) Rows 
					FROM #JournalVoucherDetails 
					WHERE IsOriginal = 1
				) Original,
				(
					SELECT IdJournalVoucher, COUNT(1) Rows
					FROM #JournalVoucherDetails 
					WHERE IsOriginal = 0
					GROUP BY IdJournalVoucher
				) Homologations 
			WHERE Original.Rows <> Homologations.Rows
			
		) BEGIN
			SET @CodeMessage = 999;
			SET @Message = 'Existen detalles que no estan completamente homologados';
			SELECT @CodeMessage AS CodeMessage, @Message AS [Message], @MovementId AS IdJournalVoucher;
			RETURN;
		END
	END

    -- 4. Borrado de detalles por lotes (mantener así)
    DECLARE @BatchSize INT = 1000;
    WHILE 1=1
    BEGIN
        DELETE TOP (@BatchSize) jvd
        FROM #JournalVoucherTmp jvt
		JOIN GeneralLedger.JournalVoucherDetails jvd WITH (ROWLOCK, READPAST)
			ON jvt.Id = jvd.IdAccounting;

        IF @@ROWCOUNT < @BatchSize BREAK;
    END

    -- 5. Actualizar encabezado
    UPDATE JV
		SET Status            = @Status,
			ModificationUser  = @CodeUser,
			ModificationDate  = [Common].[GETDATE](),
            Detail			  = @Detail,
            VoucherDate       = @VoucherDate,              
            YearMovement      = YEAR(@VoucherDate),
			ConfirmationUser  = CASE WHEN @Status = 2 THEN @CodeUser ELSE JV.ConfirmationUser END,
			ConfirmationDate  = CASE WHEN @Status = 2 THEN [Common].[GETDATE]() ELSE JV.ConfirmationDate END
    FROM GeneralLedger.JournalVouchers JV WITH (ROWLOCK)
	JOIN #JournalVoucherTmp jvt ON jv.Id = jvt.Id;

    -- 6. Insertar detalles
    INSERT INTO GeneralLedger.JournalVoucherDetails
        (IdAccounting, IdMainAccount, IdThirdParty, IdCostCenter,
         DebitValue, CreditValue, Detail, IdRetention, RetentionRate, BaseValue, BillingValue)
    SELECT hd.IdJournalVoucher,
           hd.IdMainAccount,
           hd.IdThirdParty,
           hd.IdCostCenter,
           hd.DebitValue,
           hd.CreditValue,
           hd.Detail,
           hd.IdRetention,
           hd.RetentionRate,
           hd.BaseValue,
           hd.BillingValue
    FROM #JournalVoucherDetails hd;

    -- 7. Actualizar saldos SOLO para este libro y periodo si el comprobante fue confirmado
    IF @Status = 2 
    BEGIN
        DECLARE @MonthMovement INT =
            CASE @IsClosedYear
                 WHEN 1 THEN 14
                 WHEN 2 THEN 13
                 ELSE MONTH(@VoucherDate)
            END;

        MERGE GeneralLedger.GeneralLedgerBalance AS tgt
        USING (
               SELECT YEAR(@VoucherDate)  AS [Year],
                      @MonthMovement       AS [Month],
                      IdMainAccount,
                      IdThirdParty,
                      IdCostCenter,
                      SUM(DebitValue)  AS TotalDebit,
                      SUM(CreditValue) AS TotalCredit
               FROM #JournalVoucherDetails
               GROUP BY IdMainAccount, IdThirdParty, IdCostCenter
              ) AS src
        ON  tgt.Year          = src.Year
        AND tgt.Month         = src.Month
        AND tgt.IdMainAccount = src.IdMainAccount
        AND ISNULL(tgt.IdThirdParty,0) = ISNULL(src.IdThirdParty,0)
        AND ISNULL(tgt.IdCostCenter,0)= ISNULL(src.IdCostCenter,0)
        WHEN MATCHED THEN
             UPDATE SET DebitValue  = tgt.DebitValue  + src.TotalDebit,
                        CreditValue = tgt.CreditValue + src.TotalCredit
        WHEN NOT MATCHED BY TARGET THEN
             INSERT (Year,Month,IdMainAccount,IdThirdParty,IdCostCenter,DebitValue,CreditValue)
             VALUES (src.Year,src.Month,src.IdMainAccount,src.IdThirdParty,src.IdCostCenter,
                     src.TotalDebit,src.TotalCredit);
    END

    -- 8. Mensaje final
    SET @CodeMessage = 0; 
    SET @Message = 'Proceso terminado correctamente';
    SELECT @CodeMessage AS CodeMessage, @Message AS [Message], @MovementId AS IdJournalVoucher;
    RETURN;
END