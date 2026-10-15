CREATE PROCEDURE [FixedAsset].[SP_FixedAssetPhysicalAsset]
    @year int,
    @month int,
    @PlateIdStart int,
    @PlateIdEND int,
    @ItemStart int,
    @ItemEnd int,
    @ItemTypeStart int,
    @ItemTypeEnd int,
    @LocationStart int,
    @LocationEnd int,
    @Responsible int,
    @ItemCatalog int,
    @StatusAsset int
AS
BEGIN

    SET NOCOUNT ON;

    -- Indice para optimizar la consulta de depreciaciones por activo fijo.
    -- Mejora la búsqueda y agregación de información de depreciación
    -- usando FixedAssetPhysicalAssetId y el join con FixedAssetDepreciationId.
    IF NOT EXISTS (
    SELECT 1
    FROM sys.indexes
    WHERE name = 'IX_FixedAssetDepreciationDetail_PhysicalAsset_Depreciation'
      AND object_id = OBJECT_ID('FixedAsset.FixedAssetDepreciationDetail')
    )
    BEGIN
        CREATE INDEX IX_FixedAssetDepreciationDetail_PhysicalAsset_Depreciation
        ON FixedAsset.FixedAssetDepreciationDetail
        (
            FixedAssetPhysicalAssetId,
            FixedAssetDepreciationId
        )
        INCLUDE (Id, DepreciationValue);
    END

    -- Indice para optimizar la consulta de transacciones por activo fijo.
    -- Mejora la obtención de adiciones y movimientos asociados al activo,
    -- usando PhysicalAssetId y el join con FixedAssetTransactionId.
    IF NOT EXISTS (
    SELECT 1
    FROM sys.indexes
    WHERE name = 'IX_FixedAssetTransactionDetail_PhysicalAsset_Transaction'
      AND object_id = OBJECT_ID('FixedAsset.FixedAssetTransactionDetail')
    )
    BEGIN
        CREATE INDEX IX_FixedAssetTransactionDetail_PhysicalAsset_Transaction
        ON FixedAsset.FixedAssetTransactionDetail
        (
            PhysicalAssetId,
            FixedAssetTransactionId
        )
        INCLUDE (Value);
    END

    -- Indice para optimizar el acceso al detalle contable/libro del activo fijo.
    -- Mejora el join por PhysicalAssetId y cubre los campos utilizados
    -- en el reporte de valores contables y depreciación acumulada.
    IF NOT EXISTS (
    SELECT 1
    FROM sys.indexes
    WHERE name = 'IX_FixedAssetPhysicalAssetDetailBook_PhysicalAsset'
      AND object_id = OBJECT_ID('FixedAsset.FixedAssetPhysicalAssetDetailBook')
    )
    BEGIN
        CREATE INDEX IX_FixedAssetPhysicalAssetDetailBook_PhysicalAsset
        ON FixedAsset.FixedAssetPhysicalAssetDetailBook
        (
            PhysicalAssetId
        )
        INCLUDE
        (
            LegalBookId,
            ResidualValue,
            DepreciatedValue,
            InflationAdjustmentValue,
            AdjustedValue
        );
    END

    DECLARE @MonthStartDate date = DATEFROMPARTS(@year, @month, 1);
    DECLARE @MonthEndDate   date = DATEADD(MONTH, 1, @MonthStartDate);

    -------------------------------------------------------------------------
    -- 1) Base de activos: MISMA lógica del FROM/WHERE original
    -------------------------------------------------------------------------
    SELECT DISTINCT
        pa.Id
    INTO #BaseAssets
    FROM FixedAsset.FixedAssetPhysicalAsset AS pa
    INNER JOIN FixedAsset.FixedAssetItem AS i
        ON i.Id = pa.ItemId
    INNER JOIN FixedAsset.FixedAssetItemType AS it
        ON it.Id = i.ItemTypeId
    INNER JOIN FixedAsset.FixedAssetItemCatalog AS c
        ON c.Id = i.ItemCatalogId
    INNER JOIN FixedAsset.FixedAssetPhysicalAssetDetailBook AS db
        ON pa.Id = db.PhysicalAssetId
    INNER JOIN FixedAsset.FixedAssetLocation AS l
        ON l.Id = pa.LocationId
    INNER JOIN FixedAsset.FixedAssetResponsible AS r
        ON r.Id = pa.ResponsibleId
    INNER JOIN FixedAsset.FixedAssetStatusAsset AS sa
        ON sa.Id = pa.StatusAssetId
    INNER JOIN Common.ThirdParty AS thi
        ON thi.Id = r.ThirdPartyId
    INNER JOIN GeneralLedger.LegalBook AS lb
        ON lb.Id = db.LegalBookId
    LEFT JOIN FixedAsset.FixedAssetCatalogOfPropertyandServices cps
        ON cps.Id = i.CatalogOfPropertyandServicesId
    WHERE pa.Id BETWEEN IIF(@PlateIdStart = '', 0, ISNULL(@PlateIdStart, 0))
                    AND IIF(@PlateIdEND   = '', 999999999, ISNULL(@PlateIdEND, 999999999))
      AND i.Id  BETWEEN IIF(@ItemStart    = '', 0, ISNULL(@ItemStart, 0))
                    AND IIF(@ItemEnd      = '', 999999999, ISNULL(@ItemEnd, 999999999))
      AND it.Id BETWEEN IIF(@ItemTypeStart = '', 0, ISNULL(@ItemTypeStart, 0))
                    AND IIF(@ItemTypeEnd   = '', 999999999, ISNULL(@ItemTypeEnd, 999999999))
      AND l.Id  BETWEEN IIF(@LocationStart = '', 0, ISNULL(@LocationStart, 0))
                    AND IIF(@LocationEnd   = '', 999999999, ISNULL(@LocationEnd, 999999999))
      AND (((@Responsible IS NULL OR @Responsible = '') AND thi.Nit LIKE '%%') OR (thi.Nit = @Responsible))
      AND (((@ItemCatalog IS NULL OR @ItemCatalog = '') AND c.Code LIKE '%%') OR (c.Code = @ItemCatalog))
      AND (((@StatusAsset IS NULL OR @StatusAsset = '') AND sa.Code LIKE '%%') OR (sa.Code = @StatusAsset));

    CREATE CLUSTERED INDEX IX_BaseAssets_Id
        ON #BaseAssets(Id);

    -------------------------------------------------------------------------
    -- 2) Base de depreciación solo para activos filtrados
    -------------------------------------------------------------------------
    SELECT
        dd.FixedAssetPhysicalAssetId,
        d.ClosingYear,
        d.ClosingMonth,
        d.Id  AS FixedAssetDepreciationId,
        dd.Id AS DetailId,
        dd.DepreciationValue
    INTO #DepBase
    FROM FixedAsset.FixedAssetDepreciationDetail AS dd
    INNER JOIN FixedAsset.FixedAssetDepreciation AS d
        ON d.Id = dd.FixedAssetDepreciationId
    INNER JOIN #BaseAssets AS b
        ON b.Id = dd.FixedAssetPhysicalAssetId;

    CREATE CLUSTERED INDEX IX_DepBase_PhysicalAsset
        ON #DepBase(FixedAssetPhysicalAssetId);

    -------------------------------------------------------------------------
    -- 3) Último período real de depreciación
    -------------------------------------------------------------------------
    SELECT
        x.FixedAssetPhysicalAssetId,
        x.ClosingYear,
        x.ClosingMonth
    INTO #LastDepreciation
    FROM
    (
        SELECT
            FixedAssetPhysicalAssetId,
            ClosingYear,
            ClosingMonth,
            ROW_NUMBER() OVER
            (
                PARTITION BY FixedAssetPhysicalAssetId
                ORDER BY ClosingYear DESC, ClosingMonth DESC, FixedAssetDepreciationId DESC
            ) AS rn
        FROM #DepBase
        WHERE ClosingYear IS NOT NULL
    ) AS x
    WHERE x.rn = 1;

    CREATE CLUSTERED INDEX IX_LastDepreciation
        ON #LastDepreciation(FixedAssetPhysicalAssetId);

    -------------------------------------------------------------------------
    -- 4) Agregados de depreciación
    -------------------------------------------------------------------------
    SELECT
        FixedAssetPhysicalAssetId,
        COUNT(*) AS PeriodCount,
        SUM(CASE
                WHEN ClosingYear = @year
                 AND ClosingMonth = @month
                THEN DepreciationValue
                ELSE 0
            END) AS DepreciationMonth
    INTO #DepreciationAgg
    FROM #DepBase
    GROUP BY FixedAssetPhysicalAssetId;

    CREATE CLUSTERED INDEX IX_DepreciationAgg
        ON #DepreciationAgg(FixedAssetPhysicalAssetId);

    -------------------------------------------------------------------------
    -- 5) Agregados de transacciones solo para activos filtrados
    -------------------------------------------------------------------------
    SELECT
        td.PhysicalAssetId,
        SUM(td.Value) AS TotalAdditions,
        SUM(CASE
                WHEN t.DocumentDate >= @MonthStartDate
                 AND t.DocumentDate <  @MonthEndDate
                THEN td.Value
                ELSE 0
            END) AS AdditionsMonth
    INTO #TransactionAgg
    FROM FixedAsset.FixedAssetTransactionDetail AS td
    INNER JOIN FixedAsset.FixedAssetTransaction AS t
        ON t.Id = td.FixedAssetTransactionId
    INNER JOIN #BaseAssets AS b
        ON b.Id = td.PhysicalAssetId
    GROUP BY td.PhysicalAssetId;

    CREATE CLUSTERED INDEX IX_TransactionAgg
        ON #TransactionAgg(PhysicalAssetId);

    -------------------------------------------------------------------------
    -- 6) Consulta final
    -------------------------------------------------------------------------
    SELECT
        pa.Plate,
        i.[Description],
        pa.Serie,
        pa.AdquisitionDate,
        CONCAT(thi.Nit, ' - ', thi.Name) AS ThirdPartyNitName,
        CONCAT(i.Code, ' - ', i.Description) AS ItemCodeName,
        CONCAT(c.Code, ' - ', c.Description) AS ItemCatalogCodeName,
        CONCAT(cps.Code, ' - ', cps.Name) AS CatalogOfPropertyandServicesCodeDescription,
        CONCAT(l.Code, ' - ', l.Name) AS LocationCodeName,
        CASE pa.Status
            WHEN 0 THEN 'Inactivo'
            WHEN 1 THEN IIF(pa.OutputRefund = 0, 'Activo', 'Devuelto')
        END AS StatusName,
        pa.EntryNumber,
        pa.PurchaseDate,
        pa.OutputDate,
        pa.HistoricalValue AS valueAcquisition,
        lb.Name,
        (db.ResidualValue + db.DepreciatedValue) AS InitialBalanceDepreciation,
        ISNULL(ld.ClosingYear, YEAR(pa.AdquisitionDate)) AS [year],
        ISNULL(ld.ClosingMonth, MONTH(pa.AdquisitionDate)) AS [month],
        db.InflationAdjustmentValue AS PAAG,
        ISNULL(ta.TotalAdditions, 0) AS additions,
        ISNULL(ta.AdditionsMonth, 0) AS additionsMonth,
        db.AdjustedValue AS accumulatedValueAsset,
        db.InflationAdjustmentValue AS InflationAdjustmentAccumulatedAct,
        db.AdjustedValue AS TotalValueAsset,
        ISNULL(da.PeriodCount, 0) AS period,
        db.DepreciatedValue AS historicalDepreciated,
        ISNULL(da.DepreciationMonth, 0) AS depreciationMonth,
        db.DepreciatedValue AS accumulatedDepreciationMonthToMonth,
        0 AS ATAccumulatedDepreciation,
        db.ResidualValue AS [balance books]
    FROM #BaseAssets AS base
    INNER JOIN FixedAsset.FixedAssetPhysicalAsset AS pa
        ON pa.Id = base.Id
    INNER JOIN FixedAsset.FixedAssetItem AS i
        ON i.Id = pa.ItemId
    INNER JOIN FixedAsset.FixedAssetItemType AS it
        ON it.Id = i.ItemTypeId
    INNER JOIN FixedAsset.FixedAssetItemCatalog AS c
        ON c.Id = i.ItemCatalogId
    INNER JOIN FixedAsset.FixedAssetPhysicalAssetDetailBook AS db
        ON pa.Id = db.PhysicalAssetId
    INNER JOIN FixedAsset.FixedAssetLocation AS l
        ON l.Id = pa.LocationId
    INNER JOIN FixedAsset.FixedAssetResponsible AS r
        ON r.Id = pa.ResponsibleId
    INNER JOIN FixedAsset.FixedAssetStatusAsset AS sa
        ON sa.Id = pa.StatusAssetId
    INNER JOIN Common.ThirdParty AS thi
        ON thi.Id = r.ThirdPartyId
    INNER JOIN GeneralLedger.LegalBook AS lb
        ON lb.Id = db.LegalBookId
    LEFT JOIN FixedAsset.FixedAssetCatalogOfPropertyandServices AS cps
        ON cps.Id = i.CatalogOfPropertyandServicesId
    LEFT JOIN #LastDepreciation AS ld
        ON ld.FixedAssetPhysicalAssetId = pa.Id
    LEFT JOIN #DepreciationAgg AS da
        ON da.FixedAssetPhysicalAssetId = pa.Id
    LEFT JOIN #TransactionAgg AS ta
        ON ta.PhysicalAssetId = pa.Id
    OPTION (RECOMPILE);

    DROP TABLE IF EXISTS #BaseAssets;
    DROP TABLE IF EXISTS #DepBase;
    DROP TABLE IF EXISTS #LastDepreciation;
    DROP TABLE IF EXISTS #DepreciationAgg;
    DROP TABLE IF EXISTS #TransactionAgg;
END
GO