CREATE PROCEDURE [FixedAsset].[SP_SaveDepreciationDetail]
    @Id                     INT,
    @ModeConfirm            BIT,
    @OperatingUnitId        INT,
    @DateDepreciateInitial  DATE,
    @DateDepreciateEnd      DATE,
    @DaysDepreciateMonth    INT,
    @CodeUser               VARCHAR(20),
    ------------------------------------------------------
    @CodeResult             INT OUTPUT,
    @MessageResult          VARCHAR(MAX) OUTPUT,
    @JournalVoucherType     VARCHAR(MAX) OUTPUT,
    @ResultConsecutives     VARCHAR(MAX) OUTPUT
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @ThirdPartyId INT = (
        SELECT IdDian
        FROM GeneralLedger.GeneralLedgerSettings
        WHERE IdOperatingUnit = @OperatingUnitId
    );
    DECLARE @Depreciation30Days BIT = (
        SELECT Depreciation30Days
        FROM FixedAsset.SettingFixedAsset
        WHERE OperatingUnitId = @OperatingUnitId
    );
    DECLARE @errors VARCHAR(MAX);

    -- Pre-compute closing period to avoid correlated subqueries in #AssetEntrySchedulePayment
    DECLARE @ClosingMonth INT, @ClosingYear INT;
    SELECT @ClosingMonth = ClosingMonth, @ClosingYear = ClosingYear
    FROM FixedAsset.FixedAssetDepreciation
    WHERE Id = @Id;

    /* --------------------- Eliminamos los datos anteriores -------------------- */
    IF @ModeConfirm = 0
    BEGIN
        DELETE FROM FixedAsset.FixedAssetDepreciationDetailCost
        WHERE FixedAssetDepreciationDetailId IN
            (SELECT Id FROM FixedAsset.FixedAssetDepreciationDetail WHERE FixedAssetDepreciationId = @Id);
        DELETE FROM FixedAsset.FixedAssetDepreciationDetail
        WHERE FixedAssetDepreciationId = @Id;
    END;

    /* ----------------------- Tablas temporales --------------------- */
    CREATE TABLE #AssetEntrySchedulePayment (
        Id                              INT,
        LegalBookId                     INT,
        PhysicalAssetId                 INT,
        DepreciationType                INT,
        DepreciatedValue                DECIMAL(18,6),
        ResidualValue                   DECIMAL(18,6),
        PercentageRescue                DECIMAL(18,6),
        DepreciatedDays                 INT,
        DaysPendingDepreciate           INT,
        LifeTime                        INT,
        Valorization                    DECIMAL(18,6),
        Devaluation                     DECIMAL(18,6),
        AdjustedValue                   DECIMAL(18,6),
        TransactionValue                DECIMAL(18,6),
        FinantialDiscountDepreciated    DECIMAL(18,6),
        DepreciateByTimeUse             BIT,
        UseTime                         INT,
        Status                          INT,
        HasOutput                       BIT,
        Depreciate                      BIT,
        FinancialDiscount               DECIMAL(18,6)
    );

    CREATE TABLE #DepreciationDetailTemp (
        Id                                       INT IDENTITY(1,1) PRIMARY KEY,
        FixedAssetDepreciationId                 INT,
        LegalBookId                              INT,
        ActiveClass                              TINYINT,
        FixedAssetPhysicalAssetId                INT NULL,
        FixedAssetPhysicalAssetDetailBookId      INT NULL,
        FixedAssetPhysicalAssetPartsId           INT NULL,
        FixedAssetPhysicalAssetPartsDetailBookId INT NULL,
        DepreciationValue                        NUMERIC(18,2),
        LifeTime                                 INT,
        RemainingLifeTime                        INT,
        DepreciatedDays                          INT,
        ValorizationValue                        NUMERIC(18,2),
        DevaluationValue                         NUMERIC(18,2),
        AdjustedValue                            NUMERIC(18,2),
        TransactionValue                         NUMERIC(18,2),
        AccumulatedDepreciation                  NUMERIC(18,2),
        ResidualValue                            NUMERIC(18,2),
        FinantialDiscount                        NUMERIC(18,2),
        FinantialDiscountDepreciation            NUMERIC(18,2),
        FinantialDiscountAdjusment               NUMERIC(18,2) NULL
    );

    CREATE TABLE #DetailCostTemp (
        Id                             INT IDENTITY(1,1) PRIMARY KEY,
        FixedAssetDepreciationDetailId INT,
        MainAccountId                  INT,
        ResponsibleId                  INT,
        LocationId                     INT,
        CostCenterId                   INT,
        DepreciatedDays                INT,
        DepreciationValue              NUMERIC(20,4)
    );

    /* -----------------------------------------------------------------------
       Conjunto de activos válidos para depreciar (reemplaza cursor InfoItem)
    ----------------------------------------------------------------------- */
    CREATE TABLE #ValidAssets (
        PhysicalAssetId INT  NOT NULL PRIMARY KEY,
        ResponsibleId   INT,
        LocationId      INT  NOT NULL,
        LocationClass   TINYINT NOT NULL,
        CostCenterId    INT  NOT NULL,
        AdquisitionDate DATE NOT NULL
    );

    INSERT INTO #ValidAssets (PhysicalAssetId, ResponsibleId, LocationId, LocationClass, CostCenterId, AdquisitionDate)
    SELECT pa.Id, pa.ResponsibleId, pa.LocationId, l.Class, fu.CostCenterId, pa.AdquisitionDate
    FROM FixedAsset.FixedAssetItemCatalog faic
    JOIN FixedAsset.FixedAssetItem fai                      ON faic.Id = fai.ItemCatalogId
    JOIN FixedAsset.FixedAssetPhysicalAsset pa              ON fai.Id  = pa.ItemId
    JOIN FixedAsset.FixedAssetPhysicalAssetDetailBook padb  ON padb.PhysicalAssetId = pa.Id
    JOIN GeneralLedger.LegalBook lb                         ON lb.Id   = padb.LegalBookId AND lb.Status = 1
    JOIN FixedAsset.FixedAssetLocation l                    ON l.Id    = pa.LocationId
    JOIN Payroll.FunctionalUnit fu                          ON fu.Id   = l.FunctionalUnitId
    WHERE pa.HasOutput = 0 AND pa.Depreciate = 1 AND pa.Status = 1 AND faic.Classification = 1
      AND pa.AdquisitionDate <= @DateDepreciateEnd
    GROUP BY pa.Id, pa.ResponsibleId, pa.LocationId, l.Class, fu.CostCenterId, pa.AdquisitionDate
    HAVING SUM(padb.DaysPendingDepreciate) > 0 AND SUM(padb.ResidualValue) > 0;

    /* -----------------------------------------------------------------------
       Tabla de movimientos (set-based, reemplaza InfoItem + movement_cursor)
    ----------------------------------------------------------------------- */
    DECLARE @TableMovement TABLE (
        PhysicalAssetId INT,
        ResponsibleId   INT,
        LocationId      INT,
        CostCenterId    INT,
        DepreciatedDays INT
    );

    /* Activos SIN movimientos en el período */
    INSERT INTO @TableMovement (PhysicalAssetId, ResponsibleId, LocationId, CostCenterId, DepreciatedDays)
    SELECT
        va.PhysicalAssetId,
        va.ResponsibleId,
        va.LocationId,
        va.CostCenterId,
        IIF(@DateDepreciateInitial > va.AdquisitionDate,
            @DaysDepreciateMonth,
            DATEDIFF(DAY, va.AdquisitionDate, @DateDepreciateEnd))
    FROM #ValidAssets va
    WHERE va.LocationClass IN (1, 2)
      AND NOT EXISTS (
          SELECT 1
          FROM FixedAsset.FixedAssetKardexItem k
          WHERE k.PhysicalAssetId = va.PhysicalAssetId
            AND k.DocumentDate >= @DateDepreciateInitial
            AND k.DocumentDate <  DATEADD(DAY, 1, @DateDepreciateEnd)
      );

    /* Kardex ranqueado para activos CON movimientos (reemplaza movement_cursor) */
    CREATE TABLE #KardexRanked (
        PhysicalAssetId   INT     NOT NULL,
        DocumentDate      DATE    NOT NULL,
        PrevResponsibleId INT,
        ResponsibleId     INT,
        PrevLocationId    INT,
        LocationId        INT     NOT NULL,
        PrevCostCenterId  INT,
        CostCenterId      INT     NOT NULL,
        PrevClass         TINYINT,
        CurrClass         TINYINT NOT NULL,
        rn                INT     NOT NULL,
        TotalMov          INT     NOT NULL,
        PrevDate          DATE
    );

    INSERT INTO #KardexRanked
    SELECT
        k.PhysicalAssetId,
        k.DocumentDate,
        ISNULL(k.PreviousResponsibleId, k.ResponsibleId),
        k.ResponsibleId,
        ISNULL(k.PreviousLocationId, k.LocationId),
        k.LocationId,
        ISNULL(fup.CostCenterId, fu.CostCenterId),
        fu.CostCenterId,
        ISNULL(lp.Class, l.Class),
        l.Class,
        ROW_NUMBER() OVER (PARTITION BY k.PhysicalAssetId ORDER BY k.DocumentDate ASC),
        COUNT(*)      OVER (PARTITION BY k.PhysicalAssetId),
        LAG(k.DocumentDate) OVER (PARTITION BY k.PhysicalAssetId ORDER BY k.DocumentDate ASC)
    FROM FixedAsset.FixedAssetKardexItem k
    INNER JOIN #ValidAssets va                   ON va.PhysicalAssetId = k.PhysicalAssetId
    INNER JOIN FixedAsset.FixedAssetLocation l   ON l.Id  = k.LocationId
    INNER JOIN Payroll.FunctionalUnit fu          ON fu.Id = l.FunctionalUnitId
    LEFT  JOIN FixedAsset.FixedAssetLocation lp  ON lp.Id = k.PreviousLocationId
    LEFT  JOIN Payroll.FunctionalUnit fup         ON fup.Id = lp.FunctionalUnitId
    WHERE k.DocumentDate >= @DateDepreciateInitial
      AND k.DocumentDate <  DATEADD(DAY, 1, @DateDepreciateEnd);

    /* Segmentos intermedios (antes de cada movimiento) */
    INSERT INTO @TableMovement (PhysicalAssetId, ResponsibleId, LocationId, CostCenterId, DepreciatedDays)
    SELECT
        kr.PhysicalAssetId,
        kr.PrevResponsibleId,
        kr.PrevLocationId,
        kr.PrevCostCenterId,
        DATEDIFF(DAY,
            CASE WHEN kr.rn = 1
                 THEN IIF(@DateDepreciateInitial < va.AdquisitionDate, va.AdquisitionDate, @DateDepreciateInitial)
                 ELSE kr.PrevDate
            END,
            kr.DocumentDate)
    FROM #KardexRanked kr
    JOIN #ValidAssets va ON va.PhysicalAssetId = kr.PhysicalAssetId
    WHERE kr.PrevClass IN (1, 2)
      AND DATEDIFF(DAY,
              CASE WHEN kr.rn = 1
                   THEN IIF(@DateDepreciateInitial < va.AdquisitionDate, va.AdquisitionDate, @DateDepreciateInitial)
                   ELSE kr.PrevDate
              END,
              kr.DocumentDate) > 0;

    /* Último segmento por activo (después del último movimiento) */
    INSERT INTO @TableMovement (PhysicalAssetId, ResponsibleId, LocationId, CostCenterId, DepreciatedDays)
    SELECT
        kr.PhysicalAssetId,
        kr.ResponsibleId,
        kr.LocationId,
        kr.CostCenterId,
        CASE
            WHEN @Depreciation30Days = 1 THEN
                CASE
                    WHEN DAY(@DateDepreciateEnd) = 28 THEN DATEDIFF(DAY, kr.DocumentDate, @DateDepreciateEnd) + 3
                    WHEN DAY(@DateDepreciateEnd) = 29 THEN DATEDIFF(DAY, kr.DocumentDate, @DateDepreciateEnd) + 2
                    WHEN DAY(@DateDepreciateEnd) = 30 THEN DATEDIFF(DAY, kr.DocumentDate, @DateDepreciateEnd) + 1
                    WHEN DAY(va.AdquisitionDate) = 31 THEN 0
                    ELSE DATEDIFF(DAY, kr.DocumentDate, @DateDepreciateEnd)
                END
            ELSE DATEDIFF(DAY, kr.DocumentDate, @DateDepreciateEnd)
        END
    FROM #KardexRanked kr
    JOIN #ValidAssets va ON va.PhysicalAssetId = kr.PhysicalAssetId
    WHERE kr.rn = kr.TotalMov
      AND kr.CurrClass IN (1, 2);

    DROP TABLE #KardexRanked;
    DROP TABLE #ValidAssets;

    /* ----------------- #AssetEntrySchedulePayment ----------------- */
    INSERT INTO #AssetEntrySchedulePayment
    SELECT DISTINCT
        padb.Id,
        padb.LegalBookId,
        pa.Id AS PhysicalAssetId,
        padb.DepreciationType,
        padb.DepreciatedValue,
        padb.ResidualValue,
        padb.PercentageRescue,
        padb.DepreciatedDays,
        padb.DaysPendingDepreciate,
        padb.LifeTime,
        padb.Valorization,
        padb.Devaluation,
        padb.AdjustedValue,
        padb.TransactionValue,
        padb.FinantialDiscountDepreciated,
        fai.DepreciateByTimeUse,
        fal.UseTime,
        pa.Status,
        pa.HasOutput,
        pa.Depreciate,
        CASE
            WHEN MONTH(sp.ScheduledDate) = @ClosingMonth AND YEAR(sp.ScheduledDate) = @ClosingYear
            THEN pa.FinancialDiscount
            ELSE 0
        END AS FinancialDiscount
    FROM FixedAsset.FixedAssetPhysicalAssetDetailBook padb
    INNER JOIN FixedAsset.FixedAssetPhysicalAsset pa       ON pa.Id  = padb.PhysicalAssetId
    INNER JOIN FixedAsset.FixedAssetItem fai               ON fai.Id = pa.ItemId
    INNER JOIN FixedAsset.FixedAssetLocation fal           ON fal.Id = pa.LocationId
    LEFT  JOIN FixedAsset.FixedAssetEntryItemDetail faeid  ON pa.Plate = faeid.Plate
    LEFT  JOIN FixedAsset.FixedAssetEntryItem faei         ON faei.Id = faeid.FixedAssetEntryItemId
    LEFT  JOIN FixedAsset.FixedAssetEntry fae              ON fae.Id  = faei.FixedAssetEntryId
    LEFT  JOIN FixedAsset.FixedAssetInitialBalanceItem faibi ON faibi.ItemId = fai.Id
    LEFT  JOIN Payments.AccountPayable ap                  ON ap.Id   = fae.AccountPayableId
    LEFT  JOIN Treasury.SchedulePaymentDetail spd          ON spd.AccountPayableId = ap.Id
    LEFT  JOIN Treasury.SchedulePayment sp                 ON sp.Id   = spd.SchedulePaymentId
    WHERE ((fae.Status IS NULL OR fae.Status = 2) OR (faibi.Status = 1));

    /* ---------------------- #DepreciationDetailTemp --------------- */
    INSERT INTO #DepreciationDetailTemp
    (
        FixedAssetDepreciationId, LegalBookId, ActiveClass,
        FixedAssetPhysicalAssetId, FixedAssetPhysicalAssetDetailBookId,
        FixedAssetPhysicalAssetPartsId, FixedAssetPhysicalAssetPartsDetailBookId,
        DepreciationValue, LifeTime, RemainingLifeTime, DepreciatedDays,
        ValorizationValue, DevaluationValue, AdjustedValue, TransactionValue,
        AccumulatedDepreciation, ResidualValue, FinantialDiscount,
        FinantialDiscountDepreciation, FinantialDiscountAdjusment
    )
    SELECT
        @Id,
        a.LegalBookId,
        1,
        a.PhysicalAssetId,
        a.Id,
        NULL,
        NULL,
        FixedAsset.fnCalculateDepreciateValue(
            a.DepreciationType,
            a.DepreciatedValue,
            a.ResidualValue - IIF(a.FinancialDiscount > a.FinantialDiscountDepreciated, a.FinancialDiscount, 0),
            a.PercentageRescue,
            a.DepreciateByTimeUse,
            a.UseTime,
            a.DepreciatedDays,
            a.DaysPendingDepreciate,
            tm.DepreciateDays
        ),
        a.LifeTime,
        0,
        IIF(tm.DepreciateDays > a.DaysPendingDepreciate, a.DaysPendingDepreciate, tm.DepreciateDays),
        a.Valorization,
        a.Devaluation,
        a.AdjustedValue,
        a.TransactionValue,
        0,
        a.ResidualValue,
        a.FinancialDiscount,
        FixedAsset.fnCalculateDepreciateValue(
            a.DepreciationType,
            0,
            IIF(a.FinancialDiscount > a.FinantialDiscountDepreciated, a.FinancialDiscount, 0),
            a.PercentageRescue,
            a.DepreciateByTimeUse,
            a.UseTime,
            a.DepreciatedDays,
            a.DaysPendingDepreciate,
            tm.DepreciateDays
        ),
        0
    FROM #AssetEntrySchedulePayment a
    INNER JOIN (
        SELECT PhysicalAssetId, SUM(DepreciatedDays) AS DepreciateDays
        FROM @TableMovement
        GROUP BY PhysicalAssetId
    ) tm ON tm.PhysicalAssetId = a.PhysicalAssetId
    INNER JOIN GeneralLedger.LegalBook lb ON lb.Id = a.LegalBookId AND lb.Status = 1
    WHERE a.Status = 1 AND a.HasOutput = 0 AND a.Depreciate = 1
      AND a.DaysPendingDepreciate > 0 AND a.ResidualValue > 0 AND tm.DepreciateDays > 0;

    /* ---- Ajuste por descuento financiero ---- */
    UPDATE d
       SET d.FinantialDiscountAdjusment = [FixedAsset].[fnCalculateFinantialDiscountAdjusment](
                fapadb.FinantialDiscountDepreciated,
                d.FinantialDiscountDepreciation,
                d.FinantialDiscount,
                fapadb.DepreciatedDays,
                fapadb.DaysPendingDepreciate
           )
    FROM #DepreciationDetailTemp d
    JOIN FixedAsset.FixedAssetDepreciation fad   ON d.FixedAssetDepreciationId = fad.Id
    JOIN FixedAsset.FixedAssetDepreciation fadp  ON fadp.Status = 2
         AND (
             (fad.ClosingMonth = 1  AND fad.ClosingYear = fadp.ClosingYear + 1 AND fadp.ClosingMonth = 12) OR
             (fad.ClosingYear = fadp.ClosingYear AND fad.ClosingMonth = fadp.ClosingMonth + 1)
         )
    JOIN FixedAsset.FixedAssetPhysicalAssetDetailBook fapadb ON d.FixedAssetPhysicalAssetDetailBookId = fapadb.Id
    LEFT JOIN FixedAsset.FixedAssetDepreciationDetail faddp  ON fadp.Id = faddp.FixedAssetDepreciationId
         AND d.FixedAssetPhysicalAssetDetailBookId = faddp.FixedAssetPhysicalAssetDetailBookId
         AND d.FixedAssetPhysicalAssetId           = faddp.FixedAssetPhysicalAssetId
         AND d.LegalBookId                         = faddp.LegalBookId
         AND faddp.FixedAssetPhysicalAssetPartsId IS NULL
    WHERE d.FinantialDiscount <> 0
      AND ISNULL(faddp.FinantialDiscount, 0) = 0
      AND d.FinantialDiscount > fapadb.FinantialDiscountDepreciated;

    /* -----------------------------------------------------------------------
       #DetailCostTemp  (set-based, reemplaza movcur + @tableTempDetailCost)

       Path 1: activos con múltiples movimientos cuya suma de días supera los
               días pendientes → distribuye con suma acumulada (FIFO por días).
               Maneja correctamente múltiples libros contables del mismo activo.
       Path 2: resto de activos (un solo movimiento o suma <= pendientes).
    ----------------------------------------------------------------------- */
    ;WITH MultiMovementAssets AS (
        SELECT PhysicalAssetId, SUM(DepreciatedDays) AS TotalDays
        FROM @TableMovement
        GROUP BY PhysicalAssetId
        HAVING COUNT(*) > 1
    ),
    MovCumulative AS (
        SELECT
            tm.PhysicalAssetId,
            tm.ResponsibleId,
            tm.LocationId,
            tm.CostCenterId,
            tm.DepreciatedDays,
            SUM(tm.DepreciatedDays) OVER (PARTITION BY tm.PhysicalAssetId ORDER BY tm.DepreciatedDays
                ROWS BETWEEN UNBOUNDED PRECEDING AND CURRENT ROW)      AS CumDays,
            ISNULL(SUM(tm.DepreciatedDays) OVER (PARTITION BY tm.PhysicalAssetId ORDER BY tm.DepreciatedDays
                ROWS BETWEEN UNBOUNDED PRECEDING AND 1 PRECEDING), 0)  AS PrevCumDays
        FROM @TableMovement tm
        INNER JOIN MultiMovementAssets mma ON mma.PhysicalAssetId = tm.PhysicalAssetId
    )
    INSERT INTO #DetailCostTemp (FixedAssetDepreciationDetailId, MainAccountId, ResponsibleId, LocationId, CostCenterId, DepreciatedDays, DepreciationValue)
    SELECT
        d.Id,
        pa.MainAccountId,
        mc.ResponsibleId,
        mc.LocationId,
        mc.CostCenterId,
        CASE
            WHEN mc.CumDays <= padb.DaysPendingDepreciate THEN mc.DepreciatedDays
            ELSE padb.DaysPendingDepreciate - mc.PrevCumDays
        END,
        FixedAsset.fnCalculateDepreciationValueCost(
            d.DepreciationValue - ISNULL(d.FinantialDiscountAdjusment, 0),
            d.DepreciatedDays,
            CASE
                WHEN mc.CumDays <= padb.DaysPendingDepreciate THEN mc.DepreciatedDays
                ELSE padb.DaysPendingDepreciate - mc.PrevCumDays
            END
        )
    FROM #DepreciationDetailTemp d
    JOIN FixedAsset.FixedAssetPhysicalAsset pa                    ON pa.Id   = d.FixedAssetPhysicalAssetId
    JOIN FixedAsset.FixedAssetPhysicalAssetDetailBook padb        ON padb.Id = d.FixedAssetPhysicalAssetDetailBookId
    JOIN MultiMovementAssets mma ON mma.PhysicalAssetId = d.FixedAssetPhysicalAssetId
                                AND mma.TotalDays > padb.DaysPendingDepreciate
    JOIN MovCumulative mc        ON mc.PhysicalAssetId  = d.FixedAssetPhysicalAssetId
    WHERE mc.PrevCumDays < padb.DaysPendingDepreciate
      AND padb.DaysPendingDepreciate > 0;

    INSERT INTO #DetailCostTemp (FixedAssetDepreciationDetailId, MainAccountId, ResponsibleId, LocationId, CostCenterId, DepreciatedDays, DepreciationValue)
    SELECT
        d.Id,
        pa.MainAccountId,
        tm.ResponsibleId,
        tm.LocationId,
        tm.CostCenterId,
        IIF(tm.DepreciatedDays > d.DepreciatedDays, d.DepreciatedDays, tm.DepreciatedDays),
        FixedAsset.fnCalculateDepreciationValueCost(
            d.DepreciationValue - ISNULL(d.FinantialDiscountAdjusment, 0),
            d.DepreciatedDays,
            IIF(tm.DepreciatedDays > d.DepreciatedDays, d.DepreciatedDays, tm.DepreciatedDays)
        )
    FROM #DepreciationDetailTemp d
    JOIN FixedAsset.FixedAssetPhysicalAsset pa             ON pa.Id   = d.FixedAssetPhysicalAssetId
    JOIN FixedAsset.FixedAssetPhysicalAssetDetailBook padb ON padb.Id = d.FixedAssetPhysicalAssetDetailBookId
    JOIN @TableMovement tm                                 ON tm.PhysicalAssetId = d.FixedAssetPhysicalAssetId
    WHERE NOT EXISTS (
        SELECT 1
        FROM (
            SELECT PhysicalAssetId, SUM(DepreciatedDays) AS TotalDays
            FROM @TableMovement
            GROUP BY PhysicalAssetId
            HAVING COUNT(*) > 1
        ) mma
        WHERE mma.PhysicalAssetId = d.FixedAssetPhysicalAssetId
          AND mma.TotalDays > padb.DaysPendingDepreciate
    );

    /* ---- Ajustamos diferencias por redondeos #DetailCostTemp ---- */
    ;WITH sums AS (
        SELECT FixedAssetDepreciationDetailId, SUM(DepreciationValue) AS SumValue, MAX(Id) AS MaxId
        FROM #DetailCostTemp
        GROUP BY FixedAssetDepreciationDetailId
    )
    UPDATE dc
       SET dc.DepreciationValue = dc.DepreciationValue + (d.DepreciationValue - ISNULL(d.FinantialDiscountAdjusment, 0) - s.SumValue)
    FROM #DepreciationDetailTemp d
    JOIN sums s            ON d.Id   = s.FixedAssetDepreciationDetailId
    JOIN #DetailCostTemp dc ON dc.Id = s.MaxId
    WHERE d.DepreciationValue <> s.SumValue;

    DELETE FROM #DetailCostTemp WHERE DepreciationValue <= 0;

    /* =========================== CONFIRM PATH ============================ */
    IF @ModeConfirm = 1
    BEGIN
        /* ---- Validaciones ---- */
        IF ISNULL((SELECT SUM(DepreciationValue) FROM #DepreciationDetailTemp), 0)
           <> ISNULL((SELECT SUM(DepreciationValue) FROM FixedAsset.FixedAssetDepreciationDetail WHERE FixedAssetDepreciationId = @Id), 0)
        BEGIN
            SELECT @CodeResult = 999, @MessageResult = 'Los valores del detalle de la depreciación han cambiado y debe recalcular';
            RETURN;
        END

        IF EXISTS (
            SELECT 1
            FROM #DetailCostTemp t
            JOIN #DepreciationDetailTemp d ON d.Id = t.FixedAssetDepreciationDetailId
            JOIN FixedAsset.FixedAssetDepreciationDetail dd
                ON dd.FixedAssetPhysicalAssetId = d.FixedAssetPhysicalAssetId
               AND dd.LegalBookId               = d.LegalBookId
               AND dd.FixedAssetDepreciationId  = @Id
            JOIN FixedAsset.FixedAssetDepreciationDetailCost ddc
                ON ddc.FixedAssetDepreciationDetailId = dd.Id
               AND ddc.DepreciatedDays = d.DepreciatedDays
            WHERE t.DepreciationValue <> ddc.DepreciationValue
        )
        BEGIN
            DECLARE @activosmal VARCHAR(MAX);
            SELECT @activosmal = STRING_AGG(CONCAT('Placa: ', pas.Plate, ' Valores: ', CAST(t.DepreciationValue AS VARCHAR(30)), ' - ', CAST(ddc.DepreciationValue AS VARCHAR(30))), ',')
            FROM #DetailCostTemp t
            JOIN #DepreciationDetailTemp d ON d.Id = t.FixedAssetDepreciationDetailId
            JOIN FixedAsset.FixedAssetDepreciationDetail dd
                ON dd.FixedAssetPhysicalAssetId = d.FixedAssetPhysicalAssetId
               AND dd.LegalBookId               = d.LegalBookId
               AND dd.FixedAssetDepreciationId  = @Id
            JOIN FixedAsset.FixedAssetPhysicalAsset pas ON pas.Id = d.FixedAssetPhysicalAssetId
            JOIN FixedAsset.FixedAssetDepreciationDetailCost ddc
                ON ddc.FixedAssetDepreciationDetailId = dd.Id
               AND ddc.DepreciatedDays = d.DepreciatedDays
            WHERE t.DepreciationValue <> ddc.DepreciationValue;

            SELECT @CodeResult = 999,
                   @MessageResult = CONCAT('Los valores del costo de la depreciación han cambiado y debe recalcular ', @activosmal);
            RETURN;
        END

        IF EXISTS (
            SELECT 1
            FROM FixedAsset.FixedAssetDepreciationDetail fadd WITH (NOLOCK)
            JOIN (
                SELECT FixedAssetDepreciationDetailId, SUM(DepreciationValue) AS DepreciationValue
                FROM FixedAsset.FixedAssetDepreciationDetailCost WITH (NOLOCK)
                GROUP BY FixedAssetDepreciationDetailId
            ) faddc ON fadd.Id = faddc.FixedAssetDepreciationDetailId
            WHERE fadd.FixedAssetDepreciationId = @Id
              AND fadd.DepreciationValue - ISNULL(fadd.FinantialDiscountAdjusment, 0) <> faddc.DepreciationValue
        )
        BEGIN
            SELECT @CodeResult = 999,
                   @MessageResult = 'El valor a depreciar del costo de la depreciación es diferente al valor detallado';
            RETURN;
        END

        IF EXISTS (
            SELECT 1
            FROM FixedAsset.FixedAssetDepreciation d
            INNER JOIN FixedAsset.FixedAssetDepreciationDetail dd         ON d.Id  = dd.FixedAssetDepreciationId
            INNER JOIN FixedAsset.FixedAssetDepreciationDetailCost ddc    ON dd.Id = ddc.FixedAssetDepreciationDetailId
            INNER JOIN FixedAsset.FixedAssetLocation l                    ON l.Id  = ddc.LocationId
            INNER JOIN Payroll.FunctionalUnit f                           ON f.Id  = l.FunctionalUnitId
            WHERE d.Id = @Id AND f.AccountingStructureId IS NULL
        )
        BEGIN
            SELECT @errors = STUFF((
                SELECT DISTINCT N'; La unidad funcional ' + f.Code + ' no tiene estructura contable'
                FROM FixedAsset.FixedAssetDepreciation d
                INNER JOIN FixedAsset.FixedAssetDepreciationDetail dd      ON d.Id  = dd.FixedAssetDepreciationId
                INNER JOIN FixedAsset.FixedAssetDepreciationDetailCost ddc ON dd.Id = ddc.FixedAssetDepreciationDetailId
                INNER JOIN FixedAsset.FixedAssetLocation l                 ON l.Id  = ddc.LocationId
                INNER JOIN Payroll.FunctionalUnit f                        ON f.Id  = l.FunctionalUnitId
                WHERE d.Id = @Id AND f.AccountingStructureId IS NULL
                FOR XML PATH(N''), TYPE).value(N'.[1]', N'nvarchar(MAX)'), 1, 2, N'');

            SELECT @CodeResult = 999, @MessageResult = @errors;
            RETURN;
        END

        DECLARE @errorsEstructure NVARCHAR(MAX);
        ;WITH Base AS (
            SELECT
                d.Id                            AS DepId,
                c.Id                            AS CatalogId,
                c.Code                          AS CatalogCode,
                c.HandlesDepreciationbyDistribution,
                f.AccountingStructureId         AS FU_AccountingStructureId
            FROM FixedAsset.FixedAssetDepreciation d
            INNER JOIN FixedAsset.FixedAssetDepreciationDetail dd       ON d.Id  = dd.FixedAssetDepreciationId
            INNER JOIN FixedAsset.FixedAssetPhysicalAsset pa            ON pa.Id = dd.FixedAssetPhysicalAssetId
            INNER JOIN FixedAsset.FixedAssetItem i                      ON i.Id  = pa.ItemId
            INNER JOIN FixedAsset.FixedAssetItemCatalog c               ON c.Id  = i.ItemCatalogId
            INNER JOIN FixedAsset.FixedAssetDepreciationDetailCost ddc  ON dd.Id = ddc.FixedAssetDepreciationDetailId
            INNER JOIN FixedAsset.FixedAssetLocation l                  ON l.Id  = ddc.LocationId
            INNER JOIN Payroll.FunctionalUnit f                         ON f.Id  = l.FunctionalUnitId
            INNER JOIN Payroll.AccountingStructure ast                  ON ast.Id = f.AccountingStructureId
            WHERE d.Id = @Id
        ),
        Issues AS (
            SELECT DISTINCT b.CatalogCode,
                Msg = CASE
                        WHEN b.HandlesDepreciationbyDistribution = 1 AND (cd_all.AccountingStructureId IS NULL AND cd_all.CostCenterId IS NULL)
                            THEN N'El catálogo ' + b.CatalogCode + N' no tiene parametrizada la estructura contable o el centro de costos'
                        WHEN b.HandlesDepreciationbyDistribution = 0 AND cd_fu.ItemCatalogId IS NULL
                            THEN N'El catálogo ' + b.CatalogCode + N' no tiene parametrizada la estructura contable'
                      END
            FROM Base b
            LEFT JOIN FixedAsset.FixedAssetItemCatalogDetail cd_all ON cd_all.ItemCatalogId = b.CatalogId
            LEFT JOIN FixedAsset.FixedAssetItemCatalogDetail cd_fu  ON cd_fu.ItemCatalogId  = b.CatalogId AND cd_fu.AccountingStructureId = b.FU_AccountingStructureId
            WHERE (b.HandlesDepreciationbyDistribution = 1 AND (cd_all.AccountingStructureId IS NULL AND cd_all.CostCenterId IS NULL))
               OR (b.HandlesDepreciationbyDistribution = 0 AND cd_fu.ItemCatalogId IS NULL)
        )
        SELECT @errorsEstructure = STUFF((
            SELECT N'; ' + i.Msg
            FROM (SELECT DISTINCT CatalogCode, Msg FROM Issues WHERE Msg IS NOT NULL) i
            FOR XML PATH(N''), TYPE
        ).value(N'.[1]', N'nvarchar(MAX)'), 1, 2, N'');

        IF (@errorsEstructure IS NOT NULL AND @errorsEstructure <> N'')
        BEGIN
            SELECT @CodeResult = 999, @MessageResult = @errorsEstructure;
            RETURN;
        END

        /* ---- Comprobantes contables ----
           Los datos se construyen set-based para todos los libros a la vez;
           solo el loop de llamada al SP permanece como cursor (necesario por la firma del SP).
        ---- */
        DECLARE @TableJournalVoucher TABLE (
            IdJournalVoucher INT          NOT NULL,
            VoucherDate      DATETIME     NOT NULL,
            Imported         BIT          NOT NULL,
            [Status]         TINYINT      NOT NULL,
            Detail           VARCHAR(500) NULL,
            EntityCode       VARCHAR(20)  NULL,
            EntityId         INT          NULL,
            EntityName       VARCHAR(250) NULL,
            IsClosedYear     BIT          NOT NULL,
            LegalBookId      INT          NOT NULL
        );
        DECLARE @TableJournalVoucherDetail TABLE (
            IdMainAccount  INT              NOT NULL,
            IdThirdParty   INT              NULL,
            IdCostCenter   INT              NULL,
            DebitValue     DECIMAL(20, 4)   NOT NULL,
            CreditValue    DECIMAL(20, 4)   NOT NULL,
            Detail         VARCHAR(MAX)     NULL,
            IdRetention    INT              NULL,
            RetentionRate  DECIMAL(5, 2)    NULL,
            BaseValue      DECIMAL(18, 0)   NULL,
            BillingValue   DECIMAL(18, 0)   NULL,
            LegalBookId    INT              NOT NULL
        );

        DECLARE @IdJournalVoucher INT, @ProcessDate DATE;
        SELECT @IdJournalVoucher = IdDepreciationAccountingVoucher, @ProcessDate = ProcessDate
        FROM FixedAsset.SettingFixedAsset WHERE OperatingUnitId = @OperatingUnitId;

        /* Cabecera: una por cada libro contable con detalles de depreciación */
        INSERT INTO @TableJournalVoucher
            (IdJournalVoucher, VoucherDate, Imported, [Status], Detail, EntityCode, EntityId, EntityName, IsClosedYear, LegalBookId)
        SELECT @IdJournalVoucher, @ProcessDate, 0, 2,
               CONCAT('Comprobante generado desde depreciación para cerrar el mes ', DATENAME(MONTH, @DateDepreciateInitial), ' del año ', fad.ClosingYear),
               fad.Code, @Id, 'FixedAssetDepreciation', 0, lb.LegalBookId
        FROM FixedAsset.FixedAssetDepreciation fad
        CROSS JOIN (SELECT DISTINCT LegalBookId FROM FixedAsset.FixedAssetDepreciationDetail WHERE FixedAssetDepreciationId = @Id) lb
        WHERE fad.Id = @Id;

        /* Créditos con distribución por porcentaje */
        INSERT INTO @TableJournalVoucherDetail
            (IdMainAccount, IdThirdParty, IdCostCenter, DebitValue, CreditValue, Detail, IdRetention, RetentionRate, BaseValue, BillingValue, LegalBookId)
        SELECT
            CASE pa.AdquisitionType
                WHEN 3 THEN c.LoanLeasingAccountId
                WHEN 7 THEN c.DepreciationLeasingAccountId
                WHEN 9 THEN c.FinancialRentingAccountId
                ELSE c.DepreciationAccountId
            END,
            CASE pa.AdquisitionType
                WHEN 3 THEN CASE maloan.HandlesThirdParty    WHEN 1 THEN @ThirdPartyId ELSE NULL END
                WHEN 7 THEN CASE maleasing.HandlesThirdParty WHEN 1 THEN @ThirdPartyId ELSE NULL END
                ELSE        CASE ma.HandlesThirdParty        WHEN 1 THEN @ThirdPartyId ELSE NULL END
            END,
            CASE pa.AdquisitionType
                WHEN 3 THEN CASE maloan.HandlesCostCenter    WHEN 1 THEN ddc.CostCenterId ELSE NULL END
                WHEN 7 THEN CASE maleasing.HandlesCostCenter WHEN 1 THEN ddc.CostCenterId ELSE NULL END
                ELSE        CASE ma.HandlesCostCenter        WHEN 1 THEN ddc.CostCenterId ELSE NULL END
            END,
            0,
            SUM(ddc.DepreciationValue),
            CONCAT('Detalle generado desde depreciación - Placa: ', pa.Plate),
            NULL, NULL, NULL, NULL,
            dd.LegalBookId
        FROM FixedAsset.FixedAssetDepreciation d
        INNER JOIN FixedAsset.FixedAssetDepreciationDetail dd       ON d.Id   = dd.FixedAssetDepreciationId
        INNER JOIN FixedAsset.FixedAssetPhysicalAsset pa            ON pa.Id  = dd.FixedAssetPhysicalAssetId
        INNER JOIN FixedAsset.FixedAssetItem i                      ON i.Id   = pa.ItemId
        INNER JOIN FixedAsset.FixedAssetItemCatalog c               ON c.Id   = i.ItemCatalogId
        INNER JOIN FixedAsset.FixedAssetDepreciationDetailCost ddc  ON dd.Id  = ddc.FixedAssetDepreciationDetailId
        INNER JOIN FixedAsset.FixedAssetLocation l                  ON l.Id   = ddc.LocationId
        INNER JOIN GeneralLedger.MainAccounts ma                    ON ma.Id  = c.DepreciationAccountId
        INNER JOIN GeneralLedger.MainAccounts maloan                ON maloan.Id = c.LoanLeasingAccountId
        INNER JOIN GeneralLedger.MainAccounts maleasing             ON maleasing.Id = c.DepreciationLeasingAccountId
        INNER JOIN FixedAsset.FixedAssetResponsible r               ON r.Id   = ddc.ResponsibleId
        WHERE d.Id = @Id AND c.HandlesDepreciationbyDistribution = 1
        GROUP BY r.ThirdPartyId, ddc.CostCenterId, pa.AdquisitionType,
                 c.DepreciationAccountId, ma.HandlesCostCenter, ma.HandlesThirdParty,
                 c.DepreciationLeasingAccountId, maleasing.HandlesCostCenter, maleasing.HandlesThirdParty,
                 c.LoanLeasingAccountId, maloan.HandlesCostCenter, maloan.HandlesThirdParty,
                 c.FinancialRentingAccountId, pa.Plate, dd.LegalBookId;

        /* Débitos con distribución por porcentaje */
        INSERT INTO @TableJournalVoucherDetail
            (IdMainAccount, IdThirdParty, IdCostCenter, DebitValue, CreditValue, Detail, IdRetention, RetentionRate, BaseValue, BillingValue, LegalBookId)
        SELECT
            CASE pa.AdquisitionType
                WHEN 3 THEN maloan.Id
                WHEN 7 THEN maleasing.Id
                WHEN 9 THEN maFinancialRenting.Id
                ELSE ma.Id
            END,
            CASE pa.AdquisitionType
                WHEN 3 THEN CASE maloan.HandlesThirdParty    WHEN 1 THEN @ThirdPartyId ELSE NULL END
                WHEN 7 THEN CASE maleasing.HandlesThirdParty WHEN 1 THEN @ThirdPartyId ELSE NULL END
                ELSE        CASE ma.HandlesThirdParty        WHEN 1 THEN @ThirdPartyId ELSE NULL END
            END,
            cd.CostCenterId,
            SUM(ddc.DepreciationValue * cd.DistributionPercentage / 100),
            0,
            CONCAT('Detalle generado desde depreciación - Placa: ', pa.Plate),
            NULL, NULL, NULL, NULL,
            dd.LegalBookId
        FROM FixedAsset.FixedAssetDepreciation d
        INNER JOIN FixedAsset.FixedAssetDepreciationDetail dd        ON d.Id   = dd.FixedAssetDepreciationId
        INNER JOIN FixedAsset.FixedAssetPhysicalAsset pa             ON pa.Id  = dd.FixedAssetPhysicalAssetId
        INNER JOIN FixedAsset.FixedAssetItem i                       ON i.Id   = pa.ItemId
        INNER JOIN FixedAsset.FixedAssetItemCatalog c                ON c.Id   = i.ItemCatalogId
        INNER JOIN FixedAsset.FixedAssetItemCatalogDetail cd         ON cd.ItemCatalogId = c.Id
        INNER JOIN FixedAsset.FixedAssetDepreciationDetailCost ddc   ON dd.Id  = ddc.FixedAssetDepreciationDetailId
        INNER JOIN FixedAsset.FixedAssetLocation l                   ON l.Id   = ddc.LocationId
        INNER JOIN GeneralLedger.MainAccounts ma                     ON ma.Id  = cd.LoanSpendAccountId
        INNER JOIN GeneralLedger.MainAccounts maloan                 ON maloan.Id = cd.ExpenseLoanAccountId
        INNER JOIN GeneralLedger.MainAccounts maleasing              ON maleasing.Id = cd.LoanLeasingSpendAccountId
        INNER JOIN GeneralLedger.MainAccounts maFinancialRenting     ON maFinancialRenting.Id = cd.LoanFinancialRentingAccountId
        INNER JOIN FixedAsset.FixedAssetResponsible r                ON r.Id   = ddc.ResponsibleId
        WHERE d.Id = @Id AND c.HandlesDepreciationbyDistribution = 1
        GROUP BY r.ThirdPartyId, ddc.CostCenterId, pa.AdquisitionType,
                 cd.LoanSpendAccountId, ma.HandlesCostCenter, ma.HandlesThirdParty,
                 cd.LoanLeasingSpendAccountId, maleasing.HandlesCostCenter, maleasing.HandlesThirdParty,
                 cd.ExpenseLoanAccountId, maloan.HandlesCostCenter, maloan.HandlesThirdParty,
                 cd.LoanFinancialRentingAccountId, cd.CostCenterId,
                 maloan.Id, maleasing.Id, maFinancialRenting.Id, ma.Id, pa.Plate, dd.LegalBookId;

        /* Créditos sin distribución (estructura contable directa) */
        INSERT INTO @TableJournalVoucherDetail
            (IdMainAccount, IdThirdParty, IdCostCenter, DebitValue, CreditValue, Detail, IdRetention, RetentionRate, BaseValue, BillingValue, LegalBookId)
        SELECT
            CASE pa.AdquisitionType
                WHEN 3 THEN c.LoanLeasingAccountId
                WHEN 7 THEN c.DepreciationLeasingAccountId
                WHEN 9 THEN c.FinancialRentingAccountId
                ELSE c.DepreciationAccountId
            END,
            CASE pa.AdquisitionType
                WHEN 3 THEN CASE maloan.HandlesThirdParty    WHEN 1 THEN @ThirdPartyId ELSE NULL END
                WHEN 7 THEN CASE maleasing.HandlesThirdParty WHEN 1 THEN @ThirdPartyId ELSE NULL END
                ELSE        CASE ma.HandlesThirdParty        WHEN 1 THEN @ThirdPartyId ELSE NULL END
            END,
            CASE pa.AdquisitionType
                WHEN 3 THEN CASE maloan.HandlesCostCenter    WHEN 1 THEN ddc.CostCenterId ELSE NULL END
                WHEN 7 THEN CASE maleasing.HandlesCostCenter WHEN 1 THEN ddc.CostCenterId ELSE NULL END
                ELSE        CASE ma.HandlesCostCenter        WHEN 1 THEN ddc.CostCenterId ELSE NULL END
            END,
            0,
            SUM(ddc.DepreciationValue),
            CONCAT('Detalle generado desde depreciación - Placa: ', pa.Plate),
            NULL, NULL, NULL, NULL,
            dd.LegalBookId
        FROM FixedAsset.FixedAssetDepreciation d
        INNER JOIN FixedAsset.FixedAssetDepreciationDetail dd       ON d.Id   = dd.FixedAssetDepreciationId
        INNER JOIN FixedAsset.FixedAssetPhysicalAsset pa            ON pa.Id  = dd.FixedAssetPhysicalAssetId
        INNER JOIN FixedAsset.FixedAssetItem i                      ON i.Id   = pa.ItemId
        INNER JOIN FixedAsset.FixedAssetItemCatalog c               ON c.Id   = i.ItemCatalogId
        INNER JOIN FixedAsset.FixedAssetDepreciationDetailCost ddc  ON dd.Id  = ddc.FixedAssetDepreciationDetailId
        INNER JOIN FixedAsset.FixedAssetLocation l                  ON l.Id   = ddc.LocationId
        INNER JOIN GeneralLedger.MainAccounts ma                    ON ma.Id  = c.DepreciationAccountId
        INNER JOIN GeneralLedger.MainAccounts maloan                ON maloan.Id = c.LoanLeasingAccountId
        INNER JOIN GeneralLedger.MainAccounts maleasing             ON maleasing.Id = c.DepreciationLeasingAccountId
        INNER JOIN FixedAsset.FixedAssetResponsible r               ON r.Id   = ddc.ResponsibleId
        WHERE d.Id = @Id AND c.HandlesDepreciationbyDistribution = 0
        GROUP BY r.ThirdPartyId, ddc.CostCenterId, pa.AdquisitionType,
                 c.DepreciationAccountId, ma.HandlesCostCenter, ma.HandlesThirdParty,
                 c.DepreciationLeasingAccountId, maleasing.HandlesCostCenter, maleasing.HandlesThirdParty,
                 c.LoanLeasingAccountId, maloan.HandlesCostCenter, maloan.HandlesThirdParty,
                 c.FinancialRentingAccountId, pa.Plate, dd.LegalBookId;

        /* Débitos sin distribución (estructura contable directa) */
        INSERT INTO @TableJournalVoucherDetail
            (IdMainAccount, IdThirdParty, IdCostCenter, DebitValue, CreditValue, Detail, IdRetention, RetentionRate, BaseValue, BillingValue, LegalBookId)
        SELECT
            CASE pa.AdquisitionType
                WHEN 3 THEN maloan.Id
                WHEN 7 THEN maleasing.Id
                WHEN 9 THEN maFinancialRenting.Id
                ELSE ma.Id
            END,
            CASE pa.AdquisitionType
                WHEN 3 THEN CASE maloan.HandlesThirdParty    WHEN 1 THEN @ThirdPartyId ELSE NULL END
                WHEN 7 THEN CASE maleasing.HandlesThirdParty WHEN 1 THEN @ThirdPartyId ELSE NULL END
                ELSE        CASE ma.HandlesThirdParty        WHEN 1 THEN @ThirdPartyId ELSE NULL END
            END,
            CASE pa.AdquisitionType
                WHEN 3 THEN CASE maloan.HandlesCostCenter    WHEN 1 THEN ddc.CostCenterId ELSE NULL END
                WHEN 7 THEN CASE maleasing.HandlesCostCenter WHEN 1 THEN ddc.CostCenterId ELSE NULL END
                ELSE        CASE ma.HandlesCostCenter        WHEN 1 THEN ddc.CostCenterId ELSE NULL END
            END,
            SUM(ddc.DepreciationValue),
            0,
            CONCAT('Detalle generado desde depreciación - Placa: ', pa.Plate),
            NULL, NULL, NULL, NULL,
            dd.LegalBookId
        FROM FixedAsset.FixedAssetDepreciation d
        INNER JOIN FixedAsset.FixedAssetDepreciationDetail dd       ON d.Id   = dd.FixedAssetDepreciationId
        INNER JOIN FixedAsset.FixedAssetPhysicalAsset pa            ON pa.Id  = dd.FixedAssetPhysicalAssetId
        INNER JOIN FixedAsset.FixedAssetItem i                      ON i.Id   = pa.ItemId
        INNER JOIN FixedAsset.FixedAssetItemCatalog c               ON c.Id   = i.ItemCatalogId
        INNER JOIN FixedAsset.FixedAssetDepreciationDetailCost ddc  ON dd.Id  = ddc.FixedAssetDepreciationDetailId
        INNER JOIN FixedAsset.FixedAssetLocation l                  ON l.Id   = ddc.LocationId
        INNER JOIN Payroll.FunctionalUnit f                         ON f.Id   = l.FunctionalUnitId
        INNER JOIN FixedAsset.FixedAssetItemCatalogDetail cd        ON cd.ItemCatalogId = c.Id AND cd.AccountingStructureId = f.AccountingStructureId
        INNER JOIN GeneralLedger.MainAccounts ma                    ON ma.Id  = cd.LoanSpendAccountId
        INNER JOIN GeneralLedger.MainAccounts maloan                ON maloan.Id = cd.ExpenseLoanAccountId
        INNER JOIN GeneralLedger.MainAccounts maleasing             ON maleasing.Id = cd.LoanLeasingSpendAccountId
        INNER JOIN GeneralLedger.MainAccounts maFinancialRenting    ON maFinancialRenting.Id = cd.LoanFinancialRentingAccountId
        INNER JOIN FixedAsset.FixedAssetResponsible r               ON r.Id   = ddc.ResponsibleId
        WHERE d.Id = @Id AND c.HandlesDepreciationbyDistribution = 0
        GROUP BY r.ThirdPartyId, ddc.CostCenterId, pa.AdquisitionType,
                 ma.Id, ma.HandlesCostCenter, ma.HandlesThirdParty,
                 maleasing.Id, maleasing.HandlesCostCenter, maleasing.HandlesThirdParty,
                 maloan.Id, maloan.HandlesCostCenter, maloan.HandlesThirdParty,
                 maFinancialRenting.Id, pa.Plate, dd.LegalBookId;

        /* ---- Loop de llamada al SP por libro contable (no puede eliminarse) ---- */
        DECLARE @LegalBookId INT;
        DECLARE InfoItemDepreciarion CURSOR FOR
        SELECT DISTINCT LegalBookId FROM FixedAsset.FixedAssetDepreciationDetail WHERE FixedAssetDepreciationId = @Id;

        OPEN InfoItemDepreciarion;
        FETCH NEXT FROM InfoItemDepreciarion INTO @LegalBookId;
        WHILE @@FETCH_STATUS = 0
        BEGIN
            DECLARE @resultJournalVoucher TABLE (codeMessage VARCHAR(20), [Message] VARCHAR(MAX), IdJournalVoucher INT);
            DELETE FROM @resultJournalVoucher;

            DECLARE @JournalVoucherXML AS XML;
            SELECT @JournalVoucherXML = CONVERT(XML, (
                SELECT *
                FROM @TableJournalVoucher JournalVoucher
                INNER JOIN @TableJournalVoucherDetail JournalVoucherDetail
                    ON JournalVoucher.LegalBookId = JournalVoucherDetail.LegalBookId
                WHERE JournalVoucher.LegalBookId = @LegalBookId
                FOR XML AUTO, TYPE, ELEMENTS
            ));

            INSERT @resultJournalVoucher
            EXEC GeneralLedger.SP_CreateAndValidateJournalVoucherMovement @JournalVoucherXML, @CodeUser;

            IF (SELECT codeMessage FROM @resultJournalVoucher) = '999'
            BEGIN
                CLOSE InfoItemDepreciarion;
                DEALLOCATE InfoItemDepreciarion;
                DECLARE @errorJV VARCHAR(MAX);
                SELECT @errorJV = [Message] FROM @resultJournalVoucher;
                SELECT @CodeResult = 999, @MessageResult = @errorJV;
                RETURN;
            END

            DECLARE @JournalVoucherId INT;
            SELECT @JournalVoucherId = IdJournalVoucher FROM @resultJournalVoucher;

            DECLARE @Consecutive VARCHAR(MAX) = ISNULL(
                (SELECT CAST(Consecutive AS VARCHAR(30)) FROM GeneralLedger.JournalVouchers WHERE Id = @JournalVoucherId), 0);

            SET @ResultConsecutives = CASE
                WHEN @ResultConsecutives IS NULL OR @ResultConsecutives = '' THEN @Consecutive
                ELSE @ResultConsecutives + ', ' + @Consecutive
            END;

            FETCH NEXT FROM InfoItemDepreciarion INTO @LegalBookId;
        END
        CLOSE InfoItemDepreciarion;
        DEALLOCATE InfoItemDepreciarion;

        SELECT @JournalVoucherType = CONCAT(jvt.Code, ' - ', jvt.Name)
        FROM FixedAsset.SettingFixedAsset sfa
        INNER JOIN GeneralLedger.JournalVoucherTypes jvt ON jvt.Id = sfa.IdDepreciationAccountingVoucher
        WHERE sfa.OperatingUnitId = @OperatingUnitId;

        UPDATE padb
            SET padb.DaysPendingDepreciate        = IIF(padb.ResidualValue = dd.DepreciationValue, 0, padb.DaysPendingDepreciate - dd.DepreciatedDays),
                padb.DepreciatedDays              = padb.DepreciatedDays + dd.DepreciatedDays,
                padb.DepreciatedValue             = padb.DepreciatedValue + dd.DepreciationValue,
                padb.ResidualValue                = padb.ResidualValue - dd.DepreciationValue,
                padb.FinantialDiscountDepreciated  = dd.FinantialDiscountDepreciation
        FROM FixedAsset.FixedAssetPhysicalAssetDetailBook padb
        JOIN (
            SELECT dd.FixedAssetPhysicalAssetDetailBookId,
                   SUM(dd.DepreciatedDays)                                              AS DepreciatedDays,
                   SUM(dd.DepreciationValue - dd.FinantialDiscountAdjusment)            AS DepreciationValue,
                   SUM(dd.FinantialDiscountDepreciation + dd.FinantialDiscountAdjusment) AS FinantialDiscountDepreciation
            FROM FixedAsset.FixedAssetDepreciationDetail dd
            WHERE dd.FixedAssetDepreciationId = @Id
            GROUP BY dd.FixedAssetPhysicalAssetDetailBookId
        ) dd ON padb.Id = dd.FixedAssetPhysicalAssetDetailBookId;

        SELECT @CodeResult = 0,
               @MessageResult = CONCAT('Se confirmó la Depreciación de Activos, generando los comprobantes contables ', @ResultConsecutives, ' de tipo ', @JournalVoucherType);
        RETURN;
    END
    ELSE
    BEGIN
        /* =========================== PREVIEW PATH ======================== */

        INSERT INTO FixedAsset.FixedAssetDepreciationDetail
        (
            FixedAssetDepreciationId, LegalBookId, ActiveClass, FixedAssetPhysicalAssetId,
            FixedAssetPhysicalAssetDetailBookId, FixedAssetPhysicalAssetPartsId,
            FixedAssetPhysicalAssetPartsDetailBookId, DepreciationValue, LifeTime, RemainingLifeTime,
            DepreciatedDays, ValorizationValue, DevaluationValue, AdjustedValue, TransactionValue,
            AccumulatedDepreciation, ResidualValue, FinantialDiscount, FinantialDiscountDepreciation
        )
        SELECT
            FixedAssetDepreciationId, LegalBookId, ActiveClass, FixedAssetPhysicalAssetId,
            FixedAssetPhysicalAssetDetailBookId, FixedAssetPhysicalAssetPartsId,
            FixedAssetPhysicalAssetPartsDetailBookId, DepreciationValue, LifeTime, 0,
            DepreciatedDays, ValorizationValue, DevaluationValue, AdjustedValue, TransactionValue,
            0, ResidualValue, FinantialDiscount, FinantialDiscountDepreciation
        FROM #DepreciationDetailTemp;

        UPDATE fadd
           SET fadd.FinantialDiscountAdjusment = d.FinantialDiscountAdjusment
        FROM FixedAsset.FixedAssetDepreciationDetail fadd
        JOIN #DepreciationDetailTemp d ON d.FixedAssetDepreciationId          = fadd.FixedAssetDepreciationId
                          AND d.FixedAssetPhysicalAssetDetailBookId = fadd.FixedAssetPhysicalAssetDetailBookId
                          AND d.FixedAssetPhysicalAssetId           = fadd.FixedAssetPhysicalAssetId
                          AND d.LegalBookId                         = fadd.LegalBookId
        WHERE fadd.FixedAssetDepreciationId = @Id;

        INSERT INTO FixedAsset.FixedAssetDepreciationDetailCost
        (
            FixedAssetDepreciationDetailId, MainAccountId, ResponsibleId, LocationId,
            CostCenterId, DepreciatedDays, DepreciationValue
        )
        SELECT dd.Id, pa.MainAccountId, c.ResponsibleId, c.LocationId, c.CostCenterId, c.DepreciatedDays,
               FixedAsset.fnCalculateDepreciationValueCost(dd.DepreciationValue - ISNULL(dd.FinantialDiscountAdjusment, 0), dd.DepreciatedDays, c.DepreciatedDays)
        FROM FixedAsset.FixedAssetDepreciationDetail dd
        JOIN #DepreciationDetailTemp d ON d.FixedAssetDepreciationId          = dd.FixedAssetDepreciationId
                          AND d.FixedAssetPhysicalAssetDetailBookId = dd.FixedAssetPhysicalAssetDetailBookId
                          AND d.FixedAssetPhysicalAssetId           = dd.FixedAssetPhysicalAssetId
                          AND d.LegalBookId                         = dd.LegalBookId
        JOIN #DetailCostTemp c ON c.FixedAssetDepreciationDetailId = d.Id
        JOIN FixedAsset.FixedAssetPhysicalAsset pa ON pa.Id = d.FixedAssetPhysicalAssetId
        WHERE dd.FixedAssetDepreciationId = @Id;

        /* Ajuste de diferencias por redondeos */
        ;WITH sums AS (
            SELECT FixedAssetDepreciationDetailId, SUM(DepreciationValue) AS SumValue, MAX(Id) AS MaxId
            FROM FixedAsset.FixedAssetDepreciationDetailCost
            WHERE FixedAssetDepreciationDetailId IN (SELECT Id FROM FixedAsset.FixedAssetDepreciationDetail WHERE FixedAssetDepreciationId = @Id)
            GROUP BY FixedAssetDepreciationDetailId
        )
        UPDATE ddc
           SET ddc.DepreciationValue = ddc.DepreciationValue + (dd.DepreciationValue - ISNULL(dd.FinantialDiscountAdjusment, 0) - s.SumValue)
        FROM FixedAsset.FixedAssetDepreciationDetail dd
        JOIN sums s ON dd.Id = s.FixedAssetDepreciationDetailId
        JOIN FixedAsset.FixedAssetDepreciationDetailCost ddc ON ddc.Id = s.MaxId
        WHERE dd.FixedAssetDepreciationId = @Id;

        DELETE ddc
        FROM FixedAsset.FixedAssetDepreciationDetail dd
        JOIN FixedAsset.FixedAssetDepreciationDetailCost ddc ON dd.Id = ddc.FixedAssetDepreciationDetailId
        WHERE dd.FixedAssetDepreciationId = @Id AND ddc.DepreciationValue <= 0;

        SELECT @CodeResult = 0,
               @MessageResult = 'Se guardó la Depreciación de Activos';
        RETURN;
    END
END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Procedimiento que calcula y registra el detalle de depreciación periódica de los activos fijos de una unidad operativa, para un rango de fechas determinado. Determina cuáles activos son válidos para depreciar (activos físicos activos, sin baja, marcados para depreciación), calcula el valor de depreciación, valorización, devaluación y descuento financiero por activo y libro contable, y distribuye el costo de depreciación por centro de costo, ubicación y responsable. Cuando opera en modo borrador (ModeConfirm = 0) elimina el detalle previo del período antes de recalcular; cuando se confirma, genera los comprobantes contables (vouchers) en el libro mayor. Consulta la configuración de activos fijos (SettingFixedAsset) para determinar si la depreciación usa base de 30 días, y la configuración contable (GeneralLedgerSettings) para obtener el tercero DIAN de la unidad operativa. Es el núcleo del proceso mensual de cierre de depreciación de activos fijos.', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'PROCEDURE', @level1name = N'SP_SaveDepreciationDetail';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'PROCEDURE', @level1name = N'SP_SaveDepreciationDetail';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Calcula y persiste el detalle de depreciación mensual de activos fijos por libro contable (modo previsualización o confirmación), generando los comprobantes contables y actualizando los saldos depreciados de cada activo cuando se confirma.', @level0type=N'SCHEMA', @level0name=N'FixedAsset', @level1type=N'PROCEDURE', @level1name=N'SP_SaveDepreciationDetail';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Debe existir un registro en FixedAsset.FixedAssetDepreciation con el Id recibido (de allí se obtienen ClosingMonth/ClosingYear); La unidad operativa debe tener configuración en FixedAsset.SettingFixedAsset (Depreciation30Days, IdDepreciationAccountingVoucher, ProcessDate) y en GeneralLedger.GeneralLedgerSettings (IdDian); Los activos a depreciar deben tener Status=1, HasOutput=0, Depreciate=1, Classification=1 en su catálogo, AdquisitionDate <= fecha fin, días pendientes de depreciar > 0 y valor residual > 0; El libro contable (LegalBook) asociado debe estar activo (Status=1); Para confirmar (ModeConfirm=1), las unidades funcionales involucradas deben tener AccountingStructureId definido y los catálogos deben tener parametrizada la estructura contable (y centro de costos si manejan distribución)', @level0type=N'SCHEMA', @level0name=N'FixedAsset', @level1type=N'PROCEDURE', @level1name=N'SP_SaveDepreciationDetail';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[DELETE] FixedAsset.FixedAssetDepreciationDetailCost: Cuando ModeConfirm=0, se eliminan previamente los costos de detalle asociados a la depreciación @Id antes de recalcular; [DELETE] FixedAsset.FixedAssetDepreciationDetail: Cuando ModeConfirm=0, se eliminan los detalles previos de la depreciación @Id antes de recalcular; [INSERT] FixedAsset.FixedAssetDepreciationDetail: En modo previsualización (ModeConfirm=0), inserta un detalle por cada activo válido con días depreciados>0, valor residual>0 y status=1, calculando DepreciationValue mediante fnCalculateDepreciateValue; [UPDATE] FixedAsset.FixedAssetDepreciationDetail: En previsualización, actualiza FinantialDiscountAdjusment usando fnCalculateFinantialDiscountAdjusment cuando hay descuento financiero pendiente respecto al período anterior; [INSERT] FixedAsset.FixedAssetDepreciationDetailCost: En previsualización, inserta el costo distribuido por responsable/ubicación/centro de costos según los movimientos de kardex del período; [UPDATE] FixedAsset.FixedAssetDepreciationDetailCost: Ajusta diferencias por redondeo sumando al registro de mayor Id la diferencia entre el valor del detalle y la suma de costos; [DELETE] FixedAsset.FixedAssetDepreciationDetailCost: Elimina los registros de costo cuyo DepreciationValue resulte <= 0 tras los ajustes; [UPDATE] FixedAsset.FixedAssetPhysicalAssetDetailBook: Al confirmar, actualiza por activo: DaysPendingDepreciate (=0 si ResidualValue=DepreciationValue, sino se resta), DepreciatedDays acumulado, DepreciatedValue acumulado, ResidualValue restado y FinantialDiscountDepreciated; [EXECUTE] GeneralLedger.JournalVouchers: Al confirmar, por cada LegalBook con detalles, llama a SP_CreateAndValidateJournalVoucherMovement con un XML que contiene cabecera y débitos/créditos de depreciación; [RETURN_RESULT] RESULT: Devuelve CodeResult=0 con mensaje de éxito (incluyendo consecutivos y tipo de comprobante en confirmación) o CodeResult=999 con el mensaje de error correspondiente', @level0type=N'SCHEMA', @level0name=N'FixedAsset', @level1type=N'PROCEDURE', @level1name=N'SP_SaveDepreciationDetail';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'FixedAsset', @level1type=N'PROCEDURE', @level1name=N'SP_SaveDepreciationDetail';
-- GO
