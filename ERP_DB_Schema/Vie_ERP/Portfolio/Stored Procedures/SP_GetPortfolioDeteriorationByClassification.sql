-- =============================================
-- Author:		Andrés Steven Rojas
-- Create date: 2025-10-08
-- Description:	Procedimiento que se encarga de guardar, actualizar el archivo plano de banco
-- =============================================
CREATE PROCEDURE [Portfolio].[SP_GetPortfolioDeteriorationByClassification]
    @ClosingDate DATE,
    @OperativeUnitId INT,
    @PageNumber INT = 1,
    @PageSize INT = 50000
AS
BEGIN
    SET NOCOUNT ON;

    -- Validación de parámetros de paginación
    IF @PageNumber < 1 SET @PageNumber = 1;
    IF @PageSize < 1 OR @PageSize > 50000 SET @PageSize = 50000;

    /* 0) Reglas por libro (1=RadicatedDate, 2=AccountReceivableDate) */
    DECLARE @NiifBook TINYINT,
            @FiscalBook TINYINT,
            @MaxAge INT,
            @MinAge INT;

    SELECT TOP (1)
        @NiifBook = rdc.NiifBook,
        @FiscalBook = rdc.FiscalBook
    FROM Portfolio.SettingPortfolio sp WITH (NOLOCK)
    JOIN Portfolio.RulesDeteriorationClassification rdc WITH (NOLOCK)
        ON rdc.SettingPortfolioId = sp.Id
    WHERE sp.OperatingUnitId = @OperativeUnitId;

    SELECT
        @MinAge = MIN(ap.InitialRange),
        @MaxAge = MAX(ap.EndRange)
    FROM Portfolio.AgesPortfolio ap
    JOIN Portfolio.SettingPortfolio sp ON ap.SettingPortfolioId = sp.Id
    WHERE sp.OperatingUnitId = @OperativeUnitId

    SET @NiifBook = ISNULL(@NiifBook, 1);
    SET @FiscalBook = ISNULL(@FiscalBook, 1);

    /* 1) Base: vista + mínimos para DocType/ThirdParty */
    SELECT
        v.Id AS AccountReceivableId,
        ar.ThirdPartyId,
        i.DocumentType,
        v.AccountReceivableType,
        v.InvoiceNumber,
        v.AccountReceivableDate,
        v.RadicatedDate,
        v.DocumentDate,
        v.ThirdPartyNit,
        v.ThirdPartyName,
        COALESCE(
            v.RegimenName,
            CASE WHEN i.DocumentType = 6 THEN N'Factura Básica'
                 WHEN i.DocumentType = 7 THEN N'Factura de Productos'
                 ELSE NULL END
        ) AS RegimenName,
        v.Value AS DocumentValue,
        v.Balance,
        v.GlosaPortfolioGlosadaId,
        v.ValueGlosado,
        v.BalanceGlosa,
        v.DeteriorationBalance,
        DATEDIFF(DAY,
            CASE WHEN @NiifBook = 1
                 THEN ISNULL(v.RadicatedDate, v.AccountReceivableDate)
                 ELSE v.AccountReceivableDate
            END,
            @ClosingDate
        ) AS AgeDaysNiif,
        DATEDIFF(DAY,
            CASE WHEN @FiscalBook = 1
                 THEN ISNULL(v.RadicatedDate, v.AccountReceivableDate)
                 ELSE v.AccountReceivableDate
            END,
            @ClosingDate
        ) AS AgeDaysFiscal
    INTO #base
    FROM Portfolio.ViewAccountReceivableByPortfolioProvision v WITH (NOLOCK)
    JOIN Portfolio.AccountReceivable ar WITH (NOLOCK) ON ar.Id = v.Id
    LEFT JOIN Billing.Invoice i WITH (NOLOCK) ON i.Id = ar.InvoiceId
    WHERE v.DocumentDate IS NOT NULL
      AND v.DocumentDate <= @ClosingDate
      AND v.AccountReceivableDate <= @ClosingDate
      AND ISNULL(i.DocumentType,1) <> 5
    OPTION (RECOMPILE);

    CREATE CLUSTERED INDEX IX_base_AR ON #base (AccountReceivableId);
    CREATE NONCLUSTERED INDEX IX_base_TP ON #base (ThirdPartyId, DocumentType);
    CREATE NONCLUSTERED INDEX IX_base_Ag ON #base (AgeDaysNiif, AgeDaysFiscal) INCLUDE (Balance);

    /* 2) Clasificación: Entidad Administradora > Cliente (básica si DocType 6/7) */
    ;WITH cte_class AS (
        SELECT
            b.AccountReceivableId,
            COALESCE(
                ha.PortfolioDeteriorationClassificationId,
                CASE WHEN b.DocumentType IN (6,7)
                     THEN cu.BasicBillingDeteriorationClassificationId
                     ELSE cu.HealthInvoiceDeteriorationClassificationId
                END
            ) AS ClassificationId
        FROM #base b
        LEFT JOIN Contract.HealthAdministrator ha WITH (NOLOCK)
            ON ha.ThirdPartyId = b.ThirdPartyId
        LEFT JOIN Common.Customer cu WITH (NOLOCK)
            ON cu.ThirdPartyId = b.ThirdPartyId
    )
    SELECT
        b.*,
        cl.ClassificationId,
        (pdc.Code + N' - ' + pdc.[Description]) AS PortfolioClassification
    INTO #baseC
    FROM #base b
    LEFT JOIN cte_class cl
        ON cl.AccountReceivableId = b.AccountReceivableId
    LEFT JOIN Portfolio.PortfolioDeteriorationClassification pdc WITH (NOLOCK)
        ON pdc.Id = cl.ClassificationId;

    CREATE CLUSTERED INDEX IX_baseC_AR ON #baseC (AccountReceivableId);
    CREATE NONCLUSTERED INDEX IX_baseC_C ON #baseC (ClassificationId);

    /* 3) Porcentajes + AgesDescription + AgesId por libro */
    SELECT
        bc.*,
        detN.DeteriorationNiifPercent / 100 AS NiifPercentage,
        detN.AgesDescriptionNiif,
        detN.AgesIdNiif,
        detF.DeteriorationFiscalPercent / 100 AS FiscalPercentage,
        detF.AgesDescriptionFiscal,
        detF.AgesIdFiscal
    INTO #baseP
    FROM #baseC bc
    OUTER APPLY (
        SELECT 
            det.DeteriorationNiifPercent,
            det.AgesDescriptionNiif,
            det.AgesIdNiif
        FROM (
            SELECT 
                d.DeteriorationNiifPercent,
                d.AgesPortfolioName AS AgesDescriptionNiif,
                ap.Id AS AgesIdNiif,
                IIF(d.RangeType = 2, @MaxAge, IIF(d.RangeType = 1, NULL, ap.InitialRange)) AS InitialRange,
                IIF(d.RangeType = 2, NULL, IIF(d.RangeType = 1, @MinAge, ap.EndRange))   AS EndRange,
                d.RangeType
            FROM Portfolio.PortfolioDeteriorationClassificationDetails d WITH (NOLOCK)
            LEFT JOIN Portfolio.AgesPortfolio ap WITH (NOLOCK) 
                ON ap.Id = d.AgesPortfolioId
            WHERE d.PortfolioDeteriorationClassificationId = bc.ClassificationId
        ) det
        WHERE (det.InitialRange IS NULL AND bc.AgeDaysNiif <= det.EndRange)
           OR (det.InitialRange <= bc.AgeDaysNiif AND det.EndRange >= bc.AgeDaysNiif)
           OR (det.EndRange IS NULL AND bc.AgeDaysNiif >= det.InitialRange)
    ) detN
    OUTER APPLY (
        SELECT 
            det.DeteriorationFiscalPercent,
            det.AgesDescriptionFiscal,
            det.AgesIdFiscal
        FROM (
            SELECT 
                d.DeteriorationFiscalPercent,
                d.AgesPortfolioName AS AgesDescriptionFiscal,
                ap.Id AS AgesIdFiscal,
                -- Rangos normalizados con variables
                IIF(d.RangeType = 2, @MaxAge, IIF(d.RangeType = 1, NULL, ap.InitialRange)) AS InitialRange,
                IIF(d.RangeType = 2, NULL, IIF(d.RangeType = 1, @MinAge, ap.EndRange)) AS EndRange,
                d.RangeType
            FROM Portfolio.PortfolioDeteriorationClassificationDetails d WITH (NOLOCK)
            LEFT JOIN Portfolio.AgesPortfolio ap WITH (NOLOCK) ON ap.Id = d.AgesPortfolioId
            WHERE d.PortfolioDeteriorationClassificationId = 1
        ) det
        WHERE (det.InitialRange IS NULL AND bc.AgeDaysFiscal <= det.EndRange ) 
        OR (det.InitialRange <= bc.AgeDaysFiscal AND det.EndRange >= bc.AgeDaysFiscal) 
        OR (det.EndRange IS NULL AND bc.AgeDaysFiscal >= det.InitialRange)
    ) detF;

    CREATE CLUSTERED INDEX IX_baseP_AR ON #baseP (AccountReceivableId);

    /* 3.1) Deduplicar: dejar una sola fila por AccountReceivableId */
    SELECT *
    INTO #baseP_dedup
    FROM (
        SELECT bp.*,
            ROW_NUMBER() OVER (PARTITION BY bp.AccountReceivableId ORDER BY bp.AccountReceivableId) AS rn
        FROM #baseP bp
    ) x
    WHERE x.rn = 1;

    CREATE UNIQUE CLUSTERED INDEX IX_basePdedup_AR ON #baseP_dedup (AccountReceivableId);

    /* 4) Último acumulado de deterioro */
    WITH LastPPD AS (
        SELECT AccountReceivableId, AccumulatedDeterioration
        FROM (
            SELECT
                ar.Id AS AccountReceivableId,
                ar.DeteriorationBalance AS AccumulatedDeterioration
            FROM Portfolio.AccountReceivable ar WITH (NOLOCK)
        ) x
    )
    SELECT *
    INTO #lastPPD
    FROM LastPPD;

    CREATE UNIQUE CLUSTERED INDEX IX_lastPPD_AR ON #lastPPD (AccountReceivableId);

    /* 5) Libros activos y su LegalBookId */
    SELECT
        lb.TypeBook,
        MIN(lb.Id) AS LegalBookId
    INTO #books
    FROM GeneralLedger.VieBot vb WITH (NOLOCK)
    JOIN GeneralLedger.LegalBook lb WITH (NOLOCK) ON lb.Id = vb.LegalBookId
    WHERE lb.TypeBook IN (1,2) -- 1=Fiscal, 2=NIIF
    GROUP BY lb.TypeBook;

    /* 6) Terceros inválidos (deduplicados) */
    SELECT DISTINCT
        bp.ThirdPartyId,
        bp.ThirdPartyNit,
        bp.ThirdPartyName
    INTO #invalidTP
    FROM #baseP_dedup bp
    WHERE EXISTS (
        SELECT 1
        FROM #books b
        WHERE (b.TypeBook = 2 AND (bp.ClassificationId IS NULL OR bp.NiifPercentage IS NULL)) -- NIIF
           OR (b.TypeBook = 1 AND (bp.ClassificationId IS NULL OR bp.FiscalPercentage IS NULL)) -- Fiscal
    );

    CREATE UNIQUE CLUSTERED INDEX IX_invalidTP_TP ON #invalidTP(ThirdPartyId);

    /* 7) Conteo rápido sin materializar resultado final */
    DECLARE @TotalRecords INT,
            @ErrorCount   INT,
            @ValidCount   INT;

    SELECT @ErrorCount = COUNT(*) FROM #invalidTP;
    SELECT @ValidCount = COUNT(*)
    FROM #books b
    CROSS JOIN #baseP_dedup bp
    WHERE NOT EXISTS (SELECT 1 FROM #invalidTP it WHERE it.ThirdPartyId = bp.ThirdPartyId)
      AND CASE WHEN b.TypeBook = 2 THEN bp.AgesIdNiif ELSE bp.AgesIdFiscal END IS NOT NULL;

    SET @TotalRecords = @ErrorCount + @ValidCount;

    /* 8) Paginación directa sobre el UNION ALL sin #finalResult */
    SELECT
        *,
        @TotalRecords AS TotalRecords
    FROM (
        SELECT
            *,
            ROW_NUMBER() OVER (
                ORDER BY
                    CASE WHEN Code = 1001 THEN 0 ELSE 1 END,
                    CASE WHEN Code = 1001 THEN ThirdPartyId ELSE NULL END,
                    CASE WHEN Code = 0 THEN AccountReceivableId ELSE NULL END,
                    CASE WHEN Code = 0 THEN TypeBook ELSE NULL END,
                    -- desempate final para garantizar orden determinístico
                    ThirdPartyId,
                    AccountReceivableId,
                    TypeBook
            ) AS RowNum
        FROM (
            /* --- ERRORES --- */
            SELECT
                1001 AS Code,
                N'El Tercero ' + itp.ThirdPartyNit + ' - ' + itp.ThirdPartyName
                    + N' no tiene una calificación de cartera asociada a la edad de cartera de la factura' AS [Message],
                /* Libros / cálculo (no aplica en errores) */
                CAST(NULL AS INT) AS TypeBook,
                CAST(NULL AS INT) AS LegalBookId,
                CAST(NULL AS DECIMAL(18,6)) AS Percentage,
                CAST(NULL AS DECIMAL(18,2)) AS [Value],
                CAST(NULL AS DECIMAL(18,2)) AS AccumulatedDeterioration,
                /* Edad aplicada (no aplica en errores) */
                CAST(NULL AS NVARCHAR(200)) AS AgesDescription,
                CAST(NULL AS INT) AS [Days],
                CAST(NULL AS INT) AS AgesId,
                /* Datos del documento (NULL en errores) */
                CAST(NULL AS INT) AS AccountReceivableId,
                CAST(NULL AS INT) AS AccountReceivableType,
                CAST(NULL AS NVARCHAR(50)) AS InvoiceNumber,
                CAST(NULL AS DATETIME) AS AccountReceivableDate,
                CAST(NULL AS DATETIME) AS RadicatedDate,
                CAST(NULL AS DATETIME) AS DocumentDate,
                /* Tercero */
                itp.ThirdPartyId,
                (itp.ThirdPartyNit + N' - ' + itp.ThirdPartyName) AS ThirdPartyNitName,
                /* Contexto (NULL) */
                CAST(NULL AS NVARCHAR(200)) AS RegimenName,
                CAST(NULL AS DECIMAL(18,2)) AS DocumentValue,
                CAST(NULL AS DECIMAL(18,2)) AS Balance,
                CAST(NULL AS INT) AS GlosaPortfolioGlosadaId,
                CAST(NULL AS DECIMAL(18,2)) AS ValueGlosado,
                CAST(NULL AS DECIMAL(18,2)) AS BalanceGlosa,
                CAST(NULL AS DECIMAL(18,2)) AS DeteriorationBalance,
                /* Clasificación (NULL) */
                CAST(NULL AS INT) AS PortfolioClassificationId,
                CAST(NULL AS NVARCHAR(300)) AS PortfolioClassification
            FROM #invalidTP itp

            UNION ALL

            /* --- VÁLIDOS --- */
            SELECT
                0 AS Code,
                NULL AS [Message],
                /* Identificación del libro y cálculo */
                b.TypeBook,
                b.LegalBookId,
                CASE WHEN b.TypeBook = 2 THEN bp.NiifPercentage ELSE bp.FiscalPercentage END AS Percentage,
                ROUND(bp.Balance * ISNULL(CASE WHEN b.TypeBook = 2 THEN bp.NiifPercentage ELSE bp.FiscalPercentage END, 0), 2) AS [Value],
                ISNULL(lppd.AccumulatedDeterioration, 0) AS AccumulatedDeterioration,
                /* Edad aplicada */
                CASE WHEN b.TypeBook = 2 THEN bp.AgesDescriptionNiif ELSE bp.AgesDescriptionFiscal END AS AgesDescription,
                CASE WHEN b.TypeBook = 2 THEN bp.AgeDaysNiif ELSE bp.AgeDaysFiscal END AS [Days],
                CASE WHEN b.TypeBook = 2 THEN bp.AgesIdNiif ELSE bp.AgesIdFiscal END AS AgesId,
                /* Datos del documento */
                bp.AccountReceivableId,
                bp.AccountReceivableType,
                bp.InvoiceNumber,
                bp.AccountReceivableDate,
                bp.RadicatedDate,
                bp.DocumentDate,
                bp.ThirdPartyId,
                CONCAT(bp.ThirdPartyNit, ' - ', bp.ThirdPartyName) AS ThirdPartyNitName,
                bp.RegimenName,
                bp.DocumentValue,
                bp.Balance,
                bp.GlosaPortfolioGlosadaId,
                bp.ValueGlosado,
                bp.BalanceGlosa,
                bp.DeteriorationBalance,
                /* Clasificación */
                bp.ClassificationId AS PortfolioClassificationId,
                bp.PortfolioClassification
            FROM #books b
            CROSS JOIN #baseP_dedup bp
            LEFT JOIN #lastPPD lppd ON lppd.AccountReceivableId = bp.AccountReceivableId
            WHERE NOT EXISTS (SELECT 1 FROM #invalidTP it WHERE it.ThirdPartyId = bp.ThirdPartyId)
        ) r
    ) AS Numbered
    WHERE RowNum BETWEEN ((@PageNumber - 1) * @PageSize + 1) AND (@PageNumber * @PageSize)
    ORDER BY RowNum;

END
