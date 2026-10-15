/****** Object:  StoredProcedure [GeneralLedger].[SP_ProcessJournalVoucherMovement] ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/* ================================================================================================================
   [GeneralLedger].[ProcessJournalVoucherMovement]
   ==============================================================================================================*/
CREATE PROCEDURE [GeneralLedger].[SP_ProcessJournalVoucherMovement]
    @MovementId INT
AS
BEGIN
    SET NOCOUNT ON;

    /* =======================================================
       0. VARIABLES GENERALES
       =======================================================*/
    DECLARE @CodeMessage           INT = 0;
    DECLARE @Message               VARCHAR(MAX);
    DECLARE @AccountingMovementId  INT;
    DECLARE @JournalVoucherXml     XML;
    DECLARE @CodeUser              VARCHAR(20);
    DECLARE @VoucherDate           DATETIME;
    DECLARE @IsClosedYear          TINYINT;
    DECLARE @Status                TINYINT;
    DECLARE @LegalBookId           INT;
    DECLARE @IdJournalVoucherType  INT;
    DECLARE @EntityName            VARCHAR(250);
    DECLARE @OriginalCurrencyId    INT;
    DECLARE @DateTRM               DATE;
    DECLARE @OperatingUnitId       INT;
    DECLARE @OfficialLegalBookId   INT;
    DECLARE @EntityNameConversion  VARCHAR(250);
    DECLARE @BookCurrencyId        INT;

    /* =======================================================
       1. TABLAS TEMPORALES
       =======================================================*/
    CREATE TABLE #BookMovements (
        LegalBookId      INT PRIMARY KEY,
        BookCurrencyId   INT,
        JournalVoucherId INT
    );
    CREATE TABLE #BookRates (
        LegalBookId    INT PRIMARY KEY,
        ConversionRate DECIMAL(21,10) NULL
    );
    CREATE TABLE #HomologatedDetails (
        LegalBookId   INT,
        IdMainAccount INT,
        IdThirdParty  INT,
        IdCostCenter  INT,
        DebitValue    DECIMAL(21,5),
        CreditValue   DECIMAL(21,5),
        Detail        VARCHAR(MAX),
        IdRetention   INT,
        RetentionRate DECIMAL(6,3),
        BaseValue     DECIMAL(18,2),
        BillingValue  DECIMAL(18,2)
    );
    CREATE TABLE #OriginalDetails (
        IdMainAccount INT,
        IdThirdParty  INT,
        IdCostCenter  INT,
        DebitValue    DECIMAL(21,5),
        CreditValue   DECIMAL(21,5),
        Detail        VARCHAR(MAX),
        IdRetention   INT,
        RetentionRate DECIMAL(6,3),
        BaseValue     DECIMAL(18,2),
        BillingValue  DECIMAL(18,2)
    );

    /* =======================================================
       2. LECTURA DEL MOVIMIENTO
       =======================================================*/
    SELECT
        @JournalVoucherXml    = am.JournalVoucherXml,
        @CodeUser             = am.CreationUser,
        @VoucherDate          = am.VoucherDate,
        @LegalBookId          = am.LegalBookId,
        @IdJournalVoucherType = am.JournalVoucherTypeId,
        @EntityName           = am.EntityName,
        @Status               = am.JournalVoucherXml.value('(/JournalVoucher/Status)[1]','tinyint'),
        @IsClosedYear         = ISNULL(am.JournalVoucherXml.value('(/JournalVoucher/IsClosedYear)[1]','tinyint'),0),
        @OriginalCurrencyId   = am.JournalVoucherXml.value('(/JournalVoucher/CurrencyId)[1]','int'),
        @DateTRM              = am.JournalVoucherXml.value('(/JournalVoucher/DateTRM)[1]','date'),
        @OperatingUnitId      = am.JournalVoucherXml.value('(/JournalVoucher/OperatingUnitId)[1]','int')
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

    /* =======================================================
       3. VALIDACIONES DE DUPLICIDAD / HOMÓLOGOS
       =======================================================*/
    SELECT @OfficialLegalBookId = Id
    FROM GeneralLedger.LegalBook WITH (NOLOCK)
    WHERE OfficialBook = 1 AND Status = 1;

    SET @BookCurrencyId = (SELECT OfficialCurrencyId FROM GeneralLedger.LegalBook WHERE Id = @LegalBookId);
    SELECT @OriginalCurrencyId = IIF(ISNULL(@OriginalCurrencyId, 0) = 0, @BookCurrencyId, @OriginalCurrencyId);

    IF EXISTS (
        SELECT 1
        FROM GeneralLedger.JournalVouchers j1
        JOIN GeneralLedger.JournalVouchers j2 ON j1.AccountingMovementId = j2.AccountingMovementId AND j1.Id <> j2.Id
        WHERE j1.AccountingMovementId = @MovementId AND j1.Status <> j2.Status
    )
    BEGIN
        SET @CodeMessage = 999;
        SET @Message = 'Existe un comprobante homólogo en estado diferente';
        SET @AccountingMovementId = @MovementId;
        SELECT @CodeMessage AS CodeMessage, @Message AS [Message], @AccountingMovementId AS IdJournalVoucher;
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
        SET @Message = 'no se permite procesar un libro no oficial cuando existen homólogos';
        SET @AccountingMovementId = @MovementId;
        SELECT @CodeMessage AS CodeMessage, @Message AS [Message], @AccountingMovementId AS IdJournalVoucher;
        RETURN;
    END

    /* =======================================================
       4. LIBROS A AFECTAR
       =======================================================*/
    INSERT INTO #BookMovements (LegalBookId, BookCurrencyId, JournalVoucherId)
    SELECT vb.LegalBookId,
           lb.OfficialCurrencyId,
           0
    FROM GeneralLedger.VieBot vb WITH (NOLOCK)
    JOIN GeneralLedger.LegalBook lb WITH (NOLOCK) ON lb.Id = vb.LegalBookId
    WHERE vb.Form = @EntityName
      AND vb.Allow = 1
      AND (vb.HandlesHomologation = 1 OR vb.LegalBookId = @LegalBookId);

    IF NOT EXISTS (SELECT 1 FROM #BookMovements WHERE LegalBookId = @LegalBookId)
        INSERT INTO #BookMovements
        SELECT lb.Id, lb.OfficialCurrencyId, 0
        FROM GeneralLedger.LegalBook lb WITH (NOLOCK)
        WHERE lb.Id = @LegalBookId;

    /* =======================================================
       5. TRM POR LIBRO
       =======================================================*/
    INSERT INTO #BookRates (LegalBookId, ConversionRate)
    SELECT bm.LegalBookId,
           CONVERT(DECIMAL(21,10),
                   IIF(bm.BookCurrencyId = @OriginalCurrencyId,
                       1.0,
                       [Common].[CurrencyConverterByModule]
                           (1.0,
                            @OriginalCurrencyId,
                            bm.BookCurrencyId,
                            NULL,
                            @EntityNameConversion,
                            @DateTRM)))
    FROM #BookMovements bm;

    IF EXISTS (SELECT 1 FROM #BookRates WHERE ConversionRate IS NULL)
    BEGIN
        SET @CodeMessage = 999;
        SET @Message = 'no existe TRM válida para al menos un libro de contabilización';
        SET @AccountingMovementId = @MovementId;
        SELECT @CodeMessage AS CodeMessage, @Message AS [Message], @AccountingMovementId AS IdJournalVoucher;
        RETURN;
    END

    /* =======================================================
       6. DETALLES ORIGINALES
       =======================================================*/
    INSERT INTO #OriginalDetails
    SELECT
        t.x.value('(IdMainAccount/text())[1]','int'),
        NULLIF(t.x.value('(IdThirdParty/text())[1]','int'),0),
        NULLIF(t.x.value('(IdCostCenter/text())[1]','int'),0),
        ISNULL(t.x.value('(DebitValue/text())[1]','decimal(21,5)'),0),
        ISNULL(t.x.value('(CreditValue/text())[1]','decimal(21,5)'),0),
        t.x.value('(Detail/text())[1]','varchar(max)'),
        t.x.value('(IdRetention/text())[1]','int'),
        t.x.value('(RetentionRate/text())[1]','decimal(6,3)'),
        t.x.value('(BaseValue/text())[1]','decimal(18,2)'),
        t.x.value('(BillingValue/text())[1]','decimal(18,2)')
    FROM @JournalVoucherXml.nodes('/JournalVoucher/JournalVoucherDetail') t(x)
    WHERE ISNULL(t.x.value('(IsDelete/text())[1]','bit'),0) = 0;

    /* =======================================================
       7. HOMOLOGACIÓN + CONVERSIÓN
       =======================================================*/
	-- Insercion detalles desde el libro del movimiento
    INSERT INTO #HomologatedDetails
    SELECT br.LegalBookId,
           od.IdMainAccount, od.IdThirdParty, od.IdCostCenter,
           od.DebitValue * br.ConversionRate,
           od.CreditValue * br.ConversionRate,
           od.Detail,
           od.IdRetention,
           od.RetentionRate,
           od.BaseValue * br.ConversionRate,
           od.BillingValue * br.ConversionRate
    FROM #OriginalDetails od
	JOIN GeneralLedger.MainAccounts ma ON od.IdMainAccount = ma.Id
    JOIN #BookRates br ON br.LegalBookId = ma.LegalBookId; 

	-- Insercion detalles homologados
    INSERT INTO #HomologatedDetails
    SELECT bm.LegalBookId,
           ha.MainAccountId,
           od.IdThirdParty,
           od.IdCostCenter,
           od.DebitValue * br.ConversionRate,
           od.CreditValue * br.ConversionRate,
           od.Detail,
           od.IdRetention,
           od.RetentionRate,
           od.BaseValue * br.ConversionRate,
           od.BillingValue * br.ConversionRate
    FROM #BookMovements bm
	JOIN GeneralLedger.MainAccounts ma_homol ON bm.LegalBookId = ma_homol.LegalBookId
	JOIN GeneralLedger.HomologationAccount ha ON ma_homol.Id = ha.MainAccountId
	JOIN #OriginalDetails od ON ha.OfficialMainAccountId = od.IdMainAccount
    JOIN #BookRates br ON br.LegalBookId = bm.LegalBookId    
	WHERE bm.LegalBookId <> @OfficialLegalBookId;

	--Quiere decir que se creo manualmente el comprobante y no viene del libro oficial por tanto no hay que hacer homologación
	IF NOT EXISTS (SELECT 1 FROM #HomologatedDetails)
	BEGIN
		 INSERT INTO #HomologatedDetails
		 SELECT @LegalBookId,
           od.IdMainAccount,
           od.IdThirdParty,
           od.IdCostCenter,
           od.DebitValue,
           od.CreditValue,
           od.Detail,
           od.IdRetention,
           od.RetentionRate,
           od.BaseValue,
           od.BillingValue
    FROM #OriginalDetails od
	END


    /* =======================================================
       8. PROCESO POR LIBRO
       =======================================================*/
    DECLARE @CurrentBookId         INT;
    DECLARE @NewJournalVoucherId   INT;
    DECLARE @Consecutive           BIGINT;

    DECLARE book_cursor CURSOR LOCAL FAST_FORWARD FOR
        SELECT LegalBookId FROM #BookMovements;
    OPEN book_cursor;

    FETCH NEXT FROM book_cursor INTO @CurrentBookId;
    WHILE @@FETCH_STATUS = 0
    BEGIN

            /*------------------------------------------------------------
			  A. asegurar sequence y obtener siguiente valor
			  ------------------------------------------------------------*/
			DECLARE @SchemaName NVARCHAR(128) = 'GeneralLedger';
			DECLARE @ObjectName NVARCHAR(128) = FORMATMESSAGE('Seq_JV_T%d_L%d_Y%d', @IdJournalVoucherType, @CurrentBookId, YEAR(@VoucherDate));

			-- Obtener siguiente valor
			DECLARE @NextVal BIGINT;
			DECLARE @sql NVARCHAR(MAX);
			SET @sql = N'SELECT @NextVal = NEXT VALUE FOR ' + QUOTENAME(@SchemaName) + '.' + QUOTENAME(@ObjectName);
			EXEC sp_executesql @sql, N'@NextVal BIGINT OUTPUT', @NextVal = @Consecutive OUTPUT;

            /*------------------------------------------------------------
              B. insertar encabezado
              ------------------------------------------------------------*/
            INSERT INTO GeneralLedger.JournalVouchers
                  (Consecutive, AccountingMovementId, LegalBookId,
                   IdJournalVoucher, VoucherDate, YearMovement, Status,
                   Detail, EntityCode, EntityId, EntityName,
                   IsClosedYear, CreationUser, CreationDate, ConfirmationUser, ConfirmationDate)
            SELECT @Consecutive,
                   am.Id,
                   @CurrentBookId,
                   @IdJournalVoucherType,
                   @VoucherDate,
                   YEAR(@VoucherDate),
                   @Status,
                   am.Detail,
                   am.EntityCode,
                   am.EntityId,
                   am.EntityName,
                   @IsClosedYear,
                   am.CreationUser,
                   [Common].[GETDATE](),
				   CASE WHEN @Status = 2 THEN am.CreationUser ELSE NULL END,
				   CASE WHEN @Status = 2 THEN [Common].[GETDATE]() ELSE NULL END
            FROM GeneralLedger.AccountingMovement am
            WHERE am.Id = @MovementId;

            SET @NewJournalVoucherId = SCOPE_IDENTITY();

        /*------------------------------------------------------------
          C. insertar detalles
          ------------------------------------------------------------*/
        INSERT INTO GeneralLedger.JournalVoucherDetails
                (IdAccounting, IdMainAccount, IdThirdParty, IdCostCenter,
                 DebitValue, CreditValue, Detail, IdRetention, RetentionRate, BaseValue, BillingValue)
        SELECT @NewJournalVoucherId,
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
        FROM #HomologatedDetails hd
        WHERE hd.LegalBookId = @CurrentBookId;

        IF @Status = 2 
        BEGIN
            /*------------------------------------------------------------
              D. actualizar balances
              ------------------------------------------------------------*/
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
                   FROM #HomologatedDetails
                   WHERE LegalBookId = @CurrentBookId
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

        FETCH NEXT FROM book_cursor INTO @CurrentBookId;
    END
    CLOSE book_cursor;
    DEALLOCATE book_cursor;

    SET @CodeMessage = 0; 
    SET @Message = 'Proceso terminado correctamente';
    SET @AccountingMovementId = @MovementId;
    SELECT @CodeMessage AS CodeMessage, @Message AS [Message], @AccountingMovementId AS IdJournalVoucher;
    RETURN;
END
