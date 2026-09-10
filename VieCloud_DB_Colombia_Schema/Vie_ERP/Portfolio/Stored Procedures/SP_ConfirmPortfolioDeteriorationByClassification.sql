-- =============================================
-- Author:      Andrés Steven Rojas
-- Create date: 2026-02-18
-- Description: Procesa el lote de deterioro (Process=2, ApplyDeterioration=3)
--              para un ThirdPartyId específico. Diseñado para ser invocado
--              en paralelo desde la capa de aplicación VB.NET.
--              La cabecera debe estar confirmada (Status=2) antes de llamar
--              este SP, mediante SP_PrepareConfirmPortfolioDeteriorationByClassification.
-- =============================================
CREATE PROCEDURE [Portfolio].[SP_ConfirmPortfolioDeteriorationByClassification]
    @PortfolioProvisionId   INT,
    @CodeUser               VARCHAR(20),
    @OperativeUnitId        INT,
    @ThirdPartyId           INT     -- Filtro del lote paralelo
AS
BEGIN
    SET NOCOUNT ON;

    /***************************************** VARIABLES *****************************************/
    DECLARE
        @DocumentDate                               DATE,
        @CourtDate                                  DATE,
        @OperatingUnitId                            INT,
        @Code                                       VARCHAR(20),
        @IdJournalVoucher                           INT,
        @LegalBookId                                INT,
        @JournalVoucherId                           INT,
        @CurrentLegalBookId                         INT,
        @Message                                    VARCHAR(MAX),
        @errorJV                                    VARCHAR(MAX),
        @JournalVoucherXML                          XML,
        @BasicBillingDebitAccount                   INT,
        @BasicBillingCreditAccount                  INT,
        @BasicBillingReversalAccount                INT,
        @BasicBillingPreviousPeriodReversalAccount  INT,
        @ThirdPartyName                             VARCHAR(300),
        @ThirdPartyNit                              VARCHAR(25);

    -- Tabla temporal REAL con índices: reemplaza @TableXmlObject del SP original
    CREATE TABLE #BatchDetails (
        PortfolioProvisionDetailId          INT,
        AccountReceivableId                 INT             NOT NULL,
        InvoiceNumber                       VARCHAR(50),
        [Value]                             NUMERIC(18,2),
        BalanceAccountReceivable            NUMERIC(18,2),
        AccumulatedDeterioration            NUMERIC(18,2),
        DeteriorationBalanceCurrentYear     NUMERIC(18,2),
        DeteriorationBalancePreviousYear    NUMERIC(18,2),
        CurrentDeteriorationYear            NUMERIC(18,2),
        InvoiceDocumentType                 TINYINT,
        LegalBookId                         INT,
        -- MovementType pre-calculado: evita re-evaluar CASE en cada INSERT posterior
        -- 2 = Deterioro neto (Value > AccumulatedDeterioration)
        -- 3 = Reversión año actual (Value < Accumulated, saldo año actual > 0, mismo año)
        -- 4 = Reversión años anteriores
        MovementType                        TINYINT
    );
    CREATE INDEX IX_BatchDetails_AR
        ON #BatchDetails (AccountReceivableId);
    CREATE INDEX IX_BatchDetails_LegalBook
        ON #BatchDetails (LegalBookId, MovementType);

    -- Tablas para comprobante contable (por iteración de libro)
    DECLARE @JournalVoucherTmp TABLE (
        Id              INTEGER,
        Consecutive     BIGINT,
        LegalBookId     INTEGER,
        IdJournalVoucher INTEGER,
        VoucherDate     VARCHAR(30),
        Imported        VARCHAR(5),
        [Status]        TINYINT,
        Detail          VARCHAR(500),
        EntityCode      VARCHAR(20),
        EntityId        INTEGER,
        EntityName      VARCHAR(250),
        IsClosedYear    TINYINT
    );
    DECLARE @JournalVoucherDetailTmp TABLE (
        Id              INTEGER         DEFAULT(0),
        IdAccounting    INTEGER         DEFAULT(0),
        IdMainAccount   INTEGER,
        IdThirdParty    INTEGER,
        IdCostCenter    INTEGER,
        DebitValue      DECIMAL(18,2),
        CreditValue     DECIMAL(18,2),
        Detail          VARCHAR(500),
        IdRetention     INTEGER,
        RetentionRate   DECIMAL(5,2)    DEFAULT(0),
        BaseValue       DECIMAL(18,0)   DEFAULT(0),
        BillingValue    DECIMAL(18,0)   DEFAULT(0)
    );
    DECLARE @resultJournalVoucher TABLE (
        Code            VARCHAR(20),
        MessageResult   VARCHAR(MAX),
        IdJournalVoucher INTEGER
    );
    -- Libros omitidos por desbalance débito/crédito (solo demostración).
    -- Los registros de estos libros tampoco se actualizan en AccountReceivable.
    DECLARE @SkippedLegalBooks TABLE (LegalBookId INT);

    BEGIN TRY

        /***************************************** ASIGNACIONES *****************************************/
        SELECT
            @Code               = pp.Code,
            @DocumentDate       = pp.DocumentDate,
            @CourtDate          = pp.CourtDate,
            @OperatingUnitId    = pp.OperatingUnitId
        FROM Portfolio.PortfolioProvision pp
        WHERE pp.Id = @PortfolioProvisionId;

        -- Cuentas de facturación básica
        SELECT
            @BasicBillingDebitAccount                   = dbbp.DebitDeteriorationAccount,
            @BasicBillingCreditAccount                  = dbbp.CreditDeteriorationAccount,
            @BasicBillingReversalAccount                = dbbp.ReversalDeteriorationAccount,
            @BasicBillingPreviousPeriodReversalAccount  = dbbp.PreviousPeriodReversalAccount
        FROM Portfolio.DeteriorationBasicBillingPortfolio dbbp
        JOIN Portfolio.SettingPortfolio sp ON sp.Id = dbbp.SettingPortfolioId
        WHERE sp.OperatingUnitId = @OperativeUnitId;

        -- Tipo de comprobante y libro oficial
        SELECT @IdJournalVoucher = JournalVoucherTypeDeteriorationAccountId
        FROM Portfolio.SettingPortfolio
        WHERE OperatingUnitId = @OperatingUnitId;

        SELECT @LegalBookId = Id
        FROM GeneralLedger.LegalBook
        WHERE OfficialBook = 1;

        SELECT @ThirdPartyName = tp.Name, @ThirdPartyNit = tp.Nit
        FROM Common.ThirdParty tp
        WHERE tp.Id = @ThirdPartyId;

        /***************************************** CARGA DEL LOTE CON MOVEMENTTYPE PRE-CALCULADO *****************************************/
        INSERT INTO #BatchDetails (
            PortfolioProvisionDetailId,
            AccountReceivableId,
            InvoiceNumber,
            [Value],
            BalanceAccountReceivable,
            AccumulatedDeterioration,
            DeteriorationBalanceCurrentYear,
            DeteriorationBalancePreviousYear,
            CurrentDeteriorationYear,
            InvoiceDocumentType,
            LegalBookId,
            MovementType
        )
        SELECT
            ppd.Id,
            ppd.AccountReceivableId,
            ppd.InvoiceNumber,
            ppd.[Value],
            ppd.BalanceAccountReceivable,
            ppd.AccumulatedDeterioration,
            ppd.DeteriorationBalanceCurrentYear,
            ppd.DeteriorationBalancePreviousYear,
            ppd.CurrentDeteriorationYear,
            ISNULL(inv.DocumentType, 0),
            ppd.LegalBookId,
            -- Pre-cálculo único de MovementType
            CASE
                WHEN ppd.[Value] > ppd.AccumulatedDeterioration
                    THEN 2  -- Deterioro neto
                WHEN ppd.[Value] < ppd.AccumulatedDeterioration
                     AND ppd.DeteriorationBalanceCurrentYear > 0
                     AND ppd.CurrentDeteriorationYear = YEAR(@CourtDate)
                    THEN 3  -- Reversión año actual
                WHEN ppd.[Value] < ppd.AccumulatedDeterioration
                     AND (
                            ppd.CurrentDeteriorationYear < YEAR(@CourtDate)
                         OR (ppd.AccumulatedDeterioration - ppd.[Value]) > ppd.DeteriorationBalanceCurrentYear
                     )
                    THEN 4  -- Reversión años anteriores
                ELSE 0
            END
        FROM Portfolio.PortfolioProvisionDetail ppd
        JOIN Portfolio.AccountReceivable ar ON ar.Id = ppd.AccountReceivableId
        LEFT JOIN Billing.Invoice inv ON inv.Id = ar.InvoiceId
        WHERE ppd.PortfolioProvisionId = @PortfolioProvisionId
          AND ar.ThirdPartyId = @ThirdPartyId;  -- Filtro clave del lote

        /***************************************** COMPROBANTES CONTABLES POR LIBRO *****************************************/
        -- Itera sobre los LegalBookId distintos del lote (set pequeño, no 500K registros)
        DECLARE LegalBookCursor CURSOR LOCAL FAST_FORWARD FOR
            SELECT DISTINCT LegalBookId
            FROM #BatchDetails
            WHERE LegalBookId IS NOT NULL;

        OPEN LegalBookCursor;
        FETCH NEXT FROM LegalBookCursor INTO @CurrentLegalBookId;

        WHILE @@FETCH_STATUS = 0
        BEGIN
            DELETE FROM @JournalVoucherTmp;
            DELETE FROM @JournalVoucherDetailTmp;

            -- Cabecera del comprobante
            INSERT INTO @JournalVoucherTmp
                (Id, Consecutive, LegalBookId, IdJournalVoucher, VoucherDate,
                 Imported, [Status], Detail, EntityCode, EntityId, EntityName, IsClosedYear)
            VALUES
                (0, 0, @CurrentLegalBookId, @IdJournalVoucher, @DocumentDate,
                 'False', 2, 'Comprobante contable generado desde Deterioro de Cartera por Clasificación - ' + @ThirdPartyName + ' - ' + @ThirdPartyNit,
                 @Code, @PortfolioProvisionId, 'PortfolioProvisionAndDeterioration', 0);

            -- -- DÉBITOS --
            -- UPDATE: dos INSERT separados por MovementType en lugar del CASE anidado original.
            -- MovementType=2 (deterioro neto): cuenta débito de deterioro
            INSERT INTO @JournalVoucherDetailTmp
                (IdMainAccount, IdThirdParty, IdCostCenter, DebitValue, CreditValue, Detail)
            SELECT
                IdMainAccount, IdThirdParty, IdCostCenter,
                SUM(DebitValue), SUM(CreditValue), ''
            FROM (
                SELECT
                    ma.Id                                                                   AS IdMainAccount,
                    CASE ma.HandlesThirdParty  WHEN 1 THEN ar.ThirdPartyId  ELSE NULL END  AS IdThirdParty,
                    CASE ma.HandlesCostCenter  WHEN 1 THEN ar.CostCenterId  ELSE NULL END  AS IdCostCenter,
                    ABS(bd.[Value] - bd.AccumulatedDeterioration)                           AS DebitValue,
                    0                                                                       AS CreditValue
                FROM #BatchDetails bd
                JOIN Portfolio.AccountReceivable ar ON ar.Id = bd.AccountReceivableId
                JOIN GeneralLedger.MainAccounts ma ON ma.Id =
                    CASE
                        WHEN bd.InvoiceDocumentType IN (6, 7) THEN @BasicBillingDebitAccount
                        ELSE ar.DebitAccountDeteriorationId
                    END
                WHERE bd.LegalBookId = @CurrentLegalBookId
                  AND bd.MovementType = 2
            ) jvd
            GROUP BY IdMainAccount, IdThirdParty, IdCostCenter
            HAVING SUM(DebitValue - CreditValue) <> 0;

            -- MovementType=3 o 4 (reversiones): la cuenta "débito" usa CreditAccountDeteriorationId
            INSERT INTO @JournalVoucherDetailTmp
                (IdMainAccount, IdThirdParty, IdCostCenter, DebitValue, CreditValue, Detail)
            SELECT
                IdMainAccount, IdThirdParty, IdCostCenter,
                SUM(DebitValue), SUM(CreditValue), ''
            FROM (
                SELECT
                    ma.Id                                                                   AS IdMainAccount,
                    CASE ma.HandlesThirdParty  WHEN 1 THEN ar.ThirdPartyId  ELSE NULL END  AS IdThirdParty,
                    CASE ma.HandlesCostCenter  WHEN 1 THEN ar.CostCenterId  ELSE NULL END  AS IdCostCenter,
                    ABS(bd.AccumulatedDeterioration - bd.[Value])                           AS DebitValue,
                    0                                                                       AS CreditValue
                FROM #BatchDetails bd
                JOIN Portfolio.AccountReceivable ar ON ar.Id = bd.AccountReceivableId
                JOIN GeneralLedger.MainAccounts ma ON ma.Id =
                    CASE
                        WHEN bd.InvoiceDocumentType IN (6, 7) THEN @BasicBillingCreditAccount
                        ELSE ar.CreditAccountDeteriorationId
                    END
                WHERE bd.LegalBookId = @CurrentLegalBookId
                  AND bd.MovementType IN (3, 4)
            ) jvd
            GROUP BY IdMainAccount, IdThirdParty, IdCostCenter
            HAVING SUM(DebitValue - CreditValue) <> 0;

            -- -- CRÉDITOS --
            -- MovementType=2: crédito a CreditAccountDeteriorationId
            INSERT INTO @JournalVoucherDetailTmp
                (IdMainAccount, IdThirdParty, IdCostCenter, DebitValue, CreditValue, Detail)
            SELECT
                IdMainAccount, IdThirdParty, IdCostCenter,
                SUM(DebitValue), SUM(CreditValue), ''
            FROM (
                SELECT
                    ma.Id                                                                   AS IdMainAccount,
                    CASE ma.HandlesThirdParty  WHEN 1 THEN ar.ThirdPartyId  ELSE NULL END  AS IdThirdParty,
                    CASE ma.HandlesCostCenter  WHEN 1 THEN ar.CostCenterId  ELSE NULL END  AS IdCostCenter,
                    0                                                                       AS DebitValue,
                    (bd.[Value] - bd.AccumulatedDeterioration)                              AS CreditValue
                FROM #BatchDetails bd
                JOIN Portfolio.AccountReceivable ar ON ar.Id = bd.AccountReceivableId
                JOIN GeneralLedger.MainAccounts ma ON ma.Id =
                    CASE
                        WHEN bd.InvoiceDocumentType IN (6, 7) THEN @BasicBillingCreditAccount
                        ELSE ar.CreditAccountDeteriorationId
                    END
                WHERE bd.LegalBookId = @CurrentLegalBookId
                  AND bd.MovementType = 2
            ) jvd
            GROUP BY IdMainAccount, IdThirdParty, IdCostCenter
            HAVING SUM(DebitValue - CreditValue) <> 0;

            -- MovementType=3: crédito reversión año actual
            INSERT INTO @JournalVoucherDetailTmp
                (IdMainAccount, IdThirdParty, IdCostCenter, DebitValue, CreditValue, Detail)
            SELECT
                IdMainAccount, IdThirdParty, IdCostCenter,
                SUM(DebitValue), SUM(CreditValue), ''
            FROM (
                SELECT
                    ma.Id                                                                   AS IdMainAccount,
                    CASE ma.HandlesThirdParty  WHEN 1 THEN ar.ThirdPartyId  ELSE NULL END  AS IdThirdParty,
                    CASE ma.HandlesCostCenter  WHEN 1 THEN ar.CostCenterId  ELSE NULL END  AS IdCostCenter,
                    0                                                                       AS DebitValue,
                    IIF(
                        (bd.AccumulatedDeterioration - bd.[Value]) > bd.DeteriorationBalanceCurrentYear,
                        bd.DeteriorationBalanceCurrentYear,
                        (bd.AccumulatedDeterioration - bd.[Value])
                    )                                                                       AS CreditValue
                FROM #BatchDetails bd
                JOIN Portfolio.AccountReceivable ar ON ar.Id = bd.AccountReceivableId
                JOIN GeneralLedger.MainAccounts ma ON ma.Id =
                    CASE
                        WHEN bd.InvoiceDocumentType IN (6, 7) THEN @BasicBillingReversalAccount
                        ELSE ar.ReversalAccountDeteriorationId
                    END
                WHERE bd.LegalBookId = @CurrentLegalBookId
                  AND bd.MovementType = 3
            ) jvd
            GROUP BY IdMainAccount, IdThirdParty, IdCostCenter
            HAVING SUM(DebitValue - CreditValue) <> 0;

            -- MovementType=4: crédito reversión años anteriores
            INSERT INTO @JournalVoucherDetailTmp
                (IdMainAccount, IdThirdParty, IdCostCenter, DebitValue, CreditValue, Detail)
            SELECT
                IdMainAccount, IdThirdParty, IdCostCenter,
                SUM(DebitValue), SUM(CreditValue), ''
            FROM (
                SELECT
                    ma.Id                                                                   AS IdMainAccount,
                    CASE ma.HandlesThirdParty  WHEN 1 THEN ar.ThirdPartyId  ELSE NULL END  AS IdThirdParty,
                    CASE ma.HandlesCostCenter  WHEN 1 THEN ar.CostCenterId  ELSE NULL END  AS IdCostCenter,
                    0                                                                       AS DebitValue,
                    IIF(
                        bd.AccumulatedDeterioration = bd.DeteriorationBalanceCurrentYear,
                        bd.DeteriorationBalanceCurrentYear - bd.[Value],
                        (bd.AccumulatedDeterioration - bd.[Value]) - bd.DeteriorationBalanceCurrentYear
                    )                                                                       AS CreditValue
                FROM (
                    -- Ajuste de balances para años anteriores (misma lógica del UNION ALL original)
                    SELECT
                        bd2.AccountReceivableId,
                        bd2.[Value],
                        bd2.AccumulatedDeterioration,
                        IIF(bd2.CurrentDeteriorationYear < YEAR(@CourtDate), 0, bd2.DeteriorationBalanceCurrentYear)
                            AS DeteriorationBalanceCurrentYear,
                        bd2.DeteriorationBalancePreviousYear
                            + IIF(bd2.CurrentDeteriorationYear < YEAR(@CourtDate), bd2.DeteriorationBalanceCurrentYear, 0)
                            AS DeteriorationBalancePreviousYear,
                        bd2.InvoiceDocumentType
                    FROM #BatchDetails bd2
                    WHERE bd2.LegalBookId = @CurrentLegalBookId
                      AND bd2.MovementType = 4
                ) bd
                JOIN Portfolio.AccountReceivable ar ON ar.Id = bd.AccountReceivableId
                JOIN GeneralLedger.MainAccounts ma ON ma.Id =
                    CASE
                        WHEN bd.InvoiceDocumentType IN (6, 7) THEN @BasicBillingPreviousPeriodReversalAccount
                        ELSE ar.PreviousPeriodReversalAccountDeteriorationId
                    END
            ) jvd
            GROUP BY IdMainAccount, IdThirdParty, IdCostCenter
            HAVING SUM(DebitValue - CreditValue) <> 0;

            -- Si no hay detalles contabilizables para este libro (ej. todos con MovementType=0),
            -- se omite el comprobante para no enviar un XML nulo al SP contable.
            IF NOT EXISTS (SELECT 1 FROM @JournalVoucherDetailTmp)
            BEGIN
                FETCH NEXT FROM LegalBookCursor INTO @CurrentLegalBookId;
                CONTINUE;
            END

            -- Si los débitos y créditos no cuadran, omitir el comprobante y el update
            -- para este libro (solo demostración: permite que el proceso continúe).
            IF ABS(
                ISNULL((SELECT SUM(DebitValue)  FROM @JournalVoucherDetailTmp), 0) -
                ISNULL((SELECT SUM(CreditValue) FROM @JournalVoucherDetailTmp), 0)
               ) > 0.01
            BEGIN
                INSERT INTO @SkippedLegalBooks VALUES (@CurrentLegalBookId);
                FETCH NEXT FROM LegalBookCursor INTO @CurrentLegalBookId;
                CONTINUE;
            END

            -- Construir XML y guardar comprobante
            SELECT @JournalVoucherXML = CONVERT(XML, (
                SELECT *
                FROM @JournalVoucherTmp JournalVoucher
                JOIN @JournalVoucherDetailTmp JournalVoucherDetail
                    ON JournalVoucher.Id = JournalVoucherDetail.IdAccounting
                FOR XML AUTO, TYPE, ELEMENTS
            ));

            DELETE FROM @resultJournalVoucher;

            INSERT @resultJournalVoucher
                EXEC GeneralLedger.SP_CreateAndValidateJournalVoucherMovement
                    @JournalVoucherXML, @CodeUser;

            IF (SELECT Code FROM @resultJournalVoucher) = '999'
            BEGIN
                SELECT @errorJV = MessageResult FROM @resultJournalVoucher;
                SELECT 999 AS CodeMessage, @errorJV AS Message, '' AS Consecutive;
                CLOSE LegalBookCursor;
                DEALLOCATE LegalBookCursor;
                DROP TABLE IF EXISTS #BatchDetails;
                RETURN;
            END

            FETCH NEXT FROM LegalBookCursor INTO @CurrentLegalBookId;
        END

        CLOSE LegalBookCursor;
        DEALLOCATE LegalBookCursor;

        /***************************************** UPDATE ACCOUNTRECEIVABLE *****************************************/
        -- Separado en dos sentencias por MovementType para planes de ejecución más eficientes.

        -- MovementType = 2: Deterioro neto
        UPDATE ar
        SET
            DeteriorationBalance = bd.[Value],
            DeteriorationBalanceCurrentYear =
                (bd.[Value] - bd.AccumulatedDeterioration)
                + IIF(bd.CurrentDeteriorationYear < YEAR(@CourtDate), 0, bd.DeteriorationBalanceCurrentYear),
            DeteriorationBalancePreviousYear =
                ar.DeteriorationBalancePreviousYear
                + IIF(bd.CurrentDeteriorationYear < YEAR(@CourtDate), bd.DeteriorationBalanceCurrentYear, 0),
            CurrentDeteriorationYear = YEAR(@CourtDate)
        FROM Portfolio.AccountReceivable ar
        JOIN #BatchDetails bd ON bd.AccountReceivableId = ar.Id
        WHERE bd.MovementType = 2
          AND (bd.LegalBookId IS NULL OR bd.LegalBookId = @LegalBookId)
          AND NOT EXISTS (SELECT 1 FROM @SkippedLegalBooks sl WHERE sl.LegalBookId = bd.LegalBookId);

        -- MovementType = 3 y 4: Reversiones
        UPDATE ar
        SET
            DeteriorationBalance = bd.[Value],
            DeteriorationBalanceCurrentYear =
                ar.DeteriorationBalanceCurrentYear
                - IIF(
                    bd.CurrentDeteriorationYear < YEAR(@CourtDate),
                    bd.DeteriorationBalanceCurrentYear,
                    IIF(
                        (bd.AccumulatedDeterioration - bd.[Value]) > bd.DeteriorationBalanceCurrentYear,
                        bd.DeteriorationBalanceCurrentYear,
                        bd.AccumulatedDeterioration - bd.[Value]
                    )
                  ),
            DeteriorationBalancePreviousYear =
                ar.DeteriorationBalancePreviousYear
                - IIF(
                    (bd.AccumulatedDeterioration - bd.[Value]) > bd.DeteriorationBalanceCurrentYear,
                    (bd.AccumulatedDeterioration - bd.[Value]) - bd.DeteriorationBalanceCurrentYear,
                    0
                  )
                + IIF(
                    bd.CurrentDeteriorationYear < YEAR(@CourtDate),
                    IIF(
                        (bd.AccumulatedDeterioration - bd.[Value]) > bd.DeteriorationBalanceCurrentYear,
                        0,
                        bd.DeteriorationBalanceCurrentYear - (bd.AccumulatedDeterioration - bd.[Value])
                    ),
                    0
                  ),
            CurrentDeteriorationYear = YEAR(@CourtDate)
        FROM Portfolio.AccountReceivable ar
        JOIN #BatchDetails bd ON bd.AccountReceivableId = ar.Id
        WHERE bd.MovementType IN (3, 4)
          AND (bd.LegalBookId IS NULL OR bd.LegalBookId = @LegalBookId)
          AND NOT EXISTS (SELECT 1 FROM @SkippedLegalBooks sl WHERE sl.LegalBookId = bd.LegalBookId);

        /***************************************** RETORNO *****************************************/
        SET @Message = 'Comprobante contable validado y encolado exitosamente para el tercero ' + @ThirdPartyName + ' - ' + @ThirdPartyNit;

        SELECT 0 AS CodeMessage, @Message AS Message, @ThirdPartyNit + ' - ' + @ThirdPartyName AS Consecutive;

        DROP TABLE IF EXISTS #BatchDetails;

    END TRY
    BEGIN CATCH
        IF CURSOR_STATUS('local', 'LegalBookCursor') >= 0
        BEGIN
            CLOSE LegalBookCursor;
            DEALLOCATE LegalBookCursor;
        END
        DROP TABLE IF EXISTS #BatchDetails;

        SELECT
            999 AS CodeMessage,
            ERROR_MESSAGE() + ' línea: ' + CAST(ERROR_LINE() AS VARCHAR(20)) AS Message,
            '' AS Consecutive;
    END CATCH
END
GO

