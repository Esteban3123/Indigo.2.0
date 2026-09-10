/* ================================================================================================================
   [GeneralLedger].[ProcessJournalVoucherMovement]
   ==============================================================================================================*/
CREATE PROCEDURE [GeneralLedger].[SP_ProcessJournalVoucherMovement]
    @MovementId INT
AS
BEGIN
    SET NOCOUNT ON;

	BEGIN TRY

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
    DECLARE @OriginalInvoiceDate	DATETIME;
	DECLARE @EntityNameOriginalConversion VARCHAR(250);

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
       @OperatingUnitId      = am.JournalVoucherXml.value('(/JournalVoucher/OperatingUnitId)[1]','int'),
        @OriginalInvoiceDate  = am.JournalVoucherXml.value('(/JournalVoucher/OriginalInvoiceDate)[1]','date'),
		@EntityNameOriginalConversion = am.JournalVoucherXml.value('(/JournalVoucher/EntityNameOriginalConversion)[1]','varchar(250)')
    FROM GeneralLedger.AccountingMovement am WITH (NOLOCK)
    WHERE am.Id = @MovementId;

    IF @JournalVoucherXml IS NULL
        RETURN;

    IF @DateTRM IS NULL OR @DateTRM ='' or @DateTRM = '0001-01-01' BEGIN
		SET @DateTRM =cast( @VoucherDate as DATE)
	END
	IF @OriginalInvoiceDate IS NOT NULL BEGIN
		SET @DateTRM =cast( @OriginalInvoiceDate as DATE)
	END
    SET @EntityNameConversion =
        CASE WHEN @EntityName LIKE 'Automatic%' OR @EntityName IN ('AutomaticPortfolioTransfer','AutomaticPortfolioNote')
             THEN 'Invoice' ELSE @EntityName END;

	IF @EntityNameOriginalConversion IS NOT NULL AND  @EntityNameOriginalConversion  LIKE 'Automatic%'	
		BEGIN 
		SET @EntityNameConversion =  'Invoice'
	END 

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
		FROM GeneralLedger.AccountingMovement i 
		inner join GeneralLedger.JournalVouchers  jv
			ON i.JournalVoucherTypeId = jv.IdJournalVoucher
				AND i.LegalBookId = jv.LegalBookId
				AND i.EntityName = jv.EntityName
				AND i.EntityCode = jv.EntityCode
				AND i.EntityId = jv.EntityId
				AND i.Detail = jv.Detail
		WHERE i.id =@MovementId and  i.EntityName NOT IN ('JournalVouchers', 'PayrollLiquidation', 'GlosaObjectionsReceptionD', 'FixedAssetDepreciation', 'ConsignmentCostList')
			AND jv.Status = 2
			AND i.Id <> jv.Id
    )
    BEGIN
        SET @CodeMessage = 999;
        SET @Message = 'Ya existe un documento contabilizado con la misma información';
        SET @AccountingMovementId = @MovementId;
        SELECT @CodeMessage AS CodeMessage, @Message AS [Message], @AccountingMovementId AS IdJournalVoucher;
        RETURN;
    END

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

	UPDATE od
	  SET IdCostCenter = NULL
	FROM #OriginalDetails od
	JOIN GeneralLedger.MainAccounts ma ON od.IdMainAccount = ma.Id
	WHERE ma.HandlesCostCenter = 0

    UPDATE od
	  SET IdThirdParty = NULL
	FROM #OriginalDetails od
	JOIN GeneralLedger.MainAccounts ma ON od.IdMainAccount = ma.Id
	WHERE ma.HandlesThirdParty = 0

    /* =======================================================
       7. HOMOLOGACIÓN + CONVERSIÓN
       =======================================================*/
	-- Insercion detalles desde el libro del movimiento
    INSERT INTO #HomologatedDetails
    SELECT br.LegalBookId,
           od.IdMainAccount, od.IdThirdParty, od.IdCostCenter,
           [Common].[CurrencyConverterByModule](od.DebitValue,@OriginalCurrencyId,bm.BookCurrencyId,NULL,@EntityNameConversion,@DateTRM),
		   [Common].[CurrencyConverterByModule](od.CreditValue,@OriginalCurrencyId,bm.BookCurrencyId,NULL,@EntityNameConversion,@DateTRM),
           od.Detail,
           od.IdRetention,
           od.RetentionRate,
           [Common].[CurrencyConverterByModule](od.BaseValue,@OriginalCurrencyId,bm.BookCurrencyId,NULL,@EntityNameConversion,@DateTRM),
		   [Common].[CurrencyConverterByModule](od.BillingValue,@OriginalCurrencyId,bm.BookCurrencyId,NULL,@EntityNameConversion,@DateTRM)
    FROM #OriginalDetails od
	JOIN GeneralLedger.MainAccounts ma ON od.IdMainAccount = ma.Id
    JOIN #BookRates br ON br.LegalBookId = ma.LegalBookId
    JOIN #BookMovements bm on bm.LegalBookId = br.LegalBookId;

	-- Insercion detalles homologados
    INSERT INTO #HomologatedDetails
    SELECT bm.LegalBookId,
           ha.MainAccountId,
           od.IdThirdParty,
           od.IdCostCenter,
           [Common].[CurrencyConverterByModule](od.DebitValue,@OriginalCurrencyId,bm.BookCurrencyId,NULL,@EntityNameConversion,@DateTRM),
		   [Common].[CurrencyConverterByModule](od.CreditValue,@OriginalCurrencyId,bm.BookCurrencyId,NULL,@EntityNameConversion,@DateTRM),
           od.Detail,
           od.IdRetention,
           od.RetentionRate,
           [Common].[CurrencyConverterByModule](od.BaseValue,@OriginalCurrencyId,bm.BookCurrencyId,NULL,@EntityNameConversion,@DateTRM),
		   [Common].[CurrencyConverterByModule](od.BillingValue,@OriginalCurrencyId,bm.BookCurrencyId,NULL,@EntityNameConversion,@DateTRM)
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

	 IF @Status = 2 
        BEGIN
            /*------------------------------------------------------------
              A. actualizar balances
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

            /*------------------------------------------------------------
			  B. asegurar sequence y obtener siguiente valor
			  ------------------------------------------------------------*/
			DECLARE @SchemaName NVARCHAR(128) = 'GeneralLedger';
			DECLARE @ObjectName NVARCHAR(128) = FORMATMESSAGE('Seq_JV_T%d_L%d_Y%d', @IdJournalVoucherType, @CurrentBookId, YEAR(@VoucherDate));

			-- Obtener siguiente valor
			DECLARE @NextVal BIGINT;
			DECLARE @sql NVARCHAR(MAX);
			SET @sql = N'SELECT @NextVal = NEXT VALUE FOR ' + QUOTENAME(@SchemaName) + '.' + QUOTENAME(@ObjectName);
			EXEC sp_executesql @sql, N'@NextVal BIGINT OUTPUT', @NextVal = @Consecutive OUTPUT;

            /*------------------------------------------------------------
              C. insertar encabezado
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
                   IIF(am.EntityName = 'BasicBillingFixedAsset', 'BasicBilling', am.EntityName),
                   @IsClosedYear,
                   am.CreationUser,
                   [Common].[GETDATE](),
				   CASE WHEN @Status = 2 THEN am.CreationUser ELSE NULL END,
				   CASE WHEN @Status = 2 THEN [Common].[GETDATE]() ELSE NULL END
            FROM GeneralLedger.AccountingMovement am
            WHERE am.Id = @MovementId;

            SET @NewJournalVoucherId = SCOPE_IDENTITY();

        /*------------------------------------------------------------
          D. insertar detalles
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

        FETCH NEXT FROM book_cursor INTO @CurrentBookId;
    END
    CLOSE book_cursor;
    DEALLOCATE book_cursor;

    SET @CodeMessage = 0; 
    SET @Message = 'Proceso terminado correctamente';
    SET @AccountingMovementId = @MovementId;
    SELECT @CodeMessage AS CodeMessage, @Message AS [Message], @AccountingMovementId AS IdJournalVoucher;
    RETURN;
	END TRY
	BEGIN CATCH
		SELECT	@CodeMessage = 999, 
				@Message = CONCAT('Se presentó un error al crear el comprobante contable: ', ERROR_MESSAGE(), ' - Linea: ', ERROR_LINE()),
				@AccountingMovementId = 0
	END CATCH
END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Procedimiento contable que procesa un movimiento de comprobante de diario (journal voucher) en el libro mayor general. Recibe el identificador del movimiento pendiente, lee su configuración desde la tabla de movimientos contables (AccountingMovement), valida que no exista un comprobante ya contabilizado con la misma información de entidad, código y detalle, y luego genera o actualiza el comprobante en la tabla JournalVouchers aplicando las tasas de conversión de moneda según el libro legal (LegalBook) correspondiente. Maneja múltiples libros contables simultáneamente (oficial y auxiliares), construye parte de su lógica con SQL dinámico para adaptarse al tipo de entidad de negocio (facturas, traslados de cartera, liquidaciones de nómina, activos fijos, entre otros), y devuelve un código de resultado indicando éxito o el motivo del rechazo (duplicidad, año cerrado, etc.).', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'PROCEDURE', @level1name = N'SP_ProcessJournalVoucherMovement';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'PROCEDURE', @level1name = N'SP_ProcessJournalVoucherMovement';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Procesa un movimiento contable generando los comprobantes de diario en cada libro legal afectado, aplicando homologación de cuentas, conversión de moneda por TRM, validaciones de duplicidad/homólogos y actualización de saldos cuando queda contabilizado.', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'PROCEDURE', @level1name=N'SP_ProcessJournalVoucherMovement';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Debe existir el AccountingMovement identificado por el parámetro de entrada; El AccountingMovement debe tener un JournalVoucherXml válido con la estructura esperada (Status, CurrencyId, detalles, etc.); Debe existir un LegalBook marcado como OfficialBook=1 y Status=1; Debe existir la configuración VieBot para el EntityName cuando se requiera procesar varios libros con homologación; Deben existir las secuencias ''Seq_JV_T{tipo}_L{libro}_Y{año}'' en el esquema GeneralLedger para cada combinación procesada; Debe existir TRM cargada para la fecha (DateTRM/VoucherDate/OriginalInvoiceDate) cuando moneda origen ≠ moneda del libro; Las cuentas referenciadas en los detalles deben existir en GeneralLedger.MainAccounts; para libros no oficiales también en HomologationAccount', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'PROCEDURE', @level1name=N'SP_ProcessJournalVoucherMovement';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se actualizan saldos en GeneralLedgerBalance cuando el comprobante se procesa en estado 2 (contabilizado); Los libros a afectar siempre incluyen el libro recibido, aunque no esté configurado en VieBot; La conversión de moneda se realiza siempre a la moneda oficial de cada libro destino; Los detalles cuyo IsDelete=1 en el XML no se procesan; El centro de costo nunca se asigna a cuentas que no manejan centro de costo; Cada libro destino genera un encabezado JournalVouchers independiente con su consecutivo propio (sequence Seq_JV_T{tipo}_L{libro}_Y{año}); ConfirmationUser/ConfirmationDate solo se setean cuando el comprobante se inserta en estado 2; Los comprobantes de tipos en la lista de excepciones (JournalVouchers, PayrollLiquidation, GlosaObjectionsReceptionD, FixedAssetDepreciation, ConsignmentCostList) no aplican validación de duplicidad; La homologación entre cuentas oficiales y de otros libros se aplica solo a libros distintos del oficial', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'PROCEDURE', @level1name=N'SP_ProcessJournalVoucherMovement';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Comprobante contable / Journal Voucher; Movimiento contable; Libro legal / oficial; Homologación de cuentas; Plan de cuentas (Main Accounts); Centro de costo; Tercero; Retención y base de retención; TRM / conversión de moneda; Saldos contables (debit/credit); Año cerrado (mes 13/14); Consecutivo por tipo-libro-año (Sequence); Unidad operativa', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'PROCEDURE', @level1name=N'SP_ProcessJournalVoucherMovement';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si JournalVoucherXml IS NULL en el movimiento contable → Termina la ejecución sin procesar nada (RETURN silencioso); si DateTRM no informado, vacío o ''0001-01-01'' → Toma la fecha del comprobante (VoucherDate) como fecha de TRM; si Existe OriginalInvoiceDate → Sobrescribe la fecha de TRM con la fecha original de la factura; si EntityName empieza por ''Automatic'' o es ''AutomaticPortfolioTransfer''/''AutomaticPortfolioNote'', o EntityNameOriginalConversion empieza por ''Automatic'' → Usa ''Invoice'' como entidad para la conversión de moneda; si Existe otro AccountingMovement con misma combinación (tipo, libro, entidad, código, id, detalle) ya contabilizado (Status=2) y EntityName no está en la lista de excepciones → Aborta con mensaje ''Ya existe un documento contabilizado con la misma información'' (CodeMessage=999); si Existen comprobantes homólogos del mismo movimiento con estados distintos → Aborta con mensaje ''Existe un comprobante homólogo en estado diferente'' (CodeMessage=999); si El libro recibido no es el oficial y ya existen homólogos para el movimiento → Aborta con mensaje ''no se permite procesar un libro no oficial cuando existen homólogos''; si Algún libro de #BookMovements no tiene TRM válida (ConversionRate IS NULL) → Aborta con mensaje ''no existe TRM válida para al menos un libro de contabilización''; si Cuenta principal tiene HandlesCostCenter=0 → Se anula (NULL) el centro de costo del detalle original; si No se generaron detalles homologados (comprobante manual no proveniente del libro oficial) → Se cargan los detalles originales tal cual al libro recibido sin homologación; si Status del comprobante = 2 (contabilizado) → Actualiza/inserta saldos en GeneralLedgerBalance vía MERGE y registra usuario/fecha de confirmación; si IsClosedYear = 1 → Asigna mes 14 al movimiento de balance; si IsClosedYear = 2 → Asigna mes 13 al movimiento de balance else Usa el mes real de VoucherDate; si OriginalCurrencyId nulo o 0 → Toma la moneda oficial del libro como moneda original; si BookCurrencyId del libro = OriginalCurrencyId → Tasa de conversión = 1.0; en caso contrario se calcula con CurrencyConverterByModule; si Error en cualquier punto (CATCH) → Captura el error y arma mensaje con ERROR_MESSAGE y línea, sin propagar excepción', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'PROCEDURE', @level1name=N'SP_ProcessJournalVoucherMovement';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'Common.CurrencyConverterByModule; Common.GETDATE', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'PROCEDURE', @level1name=N'SP_ProcessJournalVoucherMovement';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'GeneralLedger.AccountingMovement; GeneralLedger.LegalBook; GeneralLedger.JournalVouchers; GeneralLedger.VieBot; GeneralLedger.MainAccounts; GeneralLedger.HomologationAccount', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'PROCEDURE', @level1name=N'SP_ProcessJournalVoucherMovement';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'PROCEDURE', @level1name=N'SP_ProcessJournalVoucherMovement';
-- GO
