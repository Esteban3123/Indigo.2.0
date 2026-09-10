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
        @NiifBook   = rdc.NiifBook,
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

    SET @NiifBook   = ISNULL(@NiifBook,   1);
    SET @FiscalBook = ISNULL(@FiscalBook, 1);

    /* 1) Base: vista + mínimos para DocType/ThirdParty */
    SELECT
        v.Id                              AS AccountReceivableId,
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
        v.Value            AS DocumentValue,
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
    LEFT JOIN Billing.Invoice i WITH (NOLOCK)          ON i.Id = ar.InvoiceId
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

    CREATE CLUSTERED INDEX IX_baseC_AR  ON #baseC (AccountReceivableId);
    CREATE NONCLUSTERED INDEX IX_baseC_C ON #baseC (ClassificationId);

    /* 3) Porcentajes + AgesDescription + AgesId por libro */
    SELECT
        bc.*,
        detN.DeteriorationNiifPercent / 100    AS NiifPercentage,
        detN.AgesDescriptionNiif,
        detN.AgesIdNiif,
        detF.DeteriorationFiscalPercent / 100  AS FiscalPercentage,
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
                ap.Id               AS AgesIdNiif,
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
                ap.Id               AS AgesIdFiscal,
                -- Rangos normalizados con variables
                IIF(d.RangeType = 2, @MaxAge, IIF(d.RangeType = 1, NULL, ap.InitialRange)) AS InitialRange,
                IIF(d.RangeType = 2, NULL, IIF(d.RangeType = 1, @MinAge, ap.EndRange)) AS EndRange,
                d.RangeType
            FROM Portfolio.PortfolioDeteriorationClassificationDetails d WITH (NOLOCK)
            LEFT JOIN Portfolio.AgesPortfolio ap WITH (NOLOCK) ON ap.Id = d.AgesPortfolioId
            WHERE d.PortfolioDeteriorationClassificationId = bc.ClassificationId
        ) det
        WHERE (det.InitialRange IS NULL AND bc.AgeDaysFiscal <= det.EndRange ) 
        OR (det.InitialRange <= bc.AgeDaysFiscal AND det.EndRange >= bc.AgeDaysFiscal) 
        OR (det.EndRange    IS NULL AND bc.AgeDaysFiscal >= det.InitialRange)
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
    WHERE lb.TypeBook IN (1,2)      -- 1=Fiscal, 2=NIIF
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
        WHERE (b.TypeBook = 2 AND (bp.ClassificationId IS NULL OR bp.NiifPercentage   IS NULL))  -- NIIF
           OR (b.TypeBook = 1 AND (bp.ClassificationId IS NULL OR bp.FiscalPercentage IS NULL))  -- Fiscal
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
                CAST(NULL AS INT)               AS TypeBook,
                CAST(NULL AS INT)               AS LegalBookId,
                CAST(NULL AS DECIMAL(18,6))     AS Percentage,
                CAST(NULL AS DECIMAL(18,2))     AS [Value],
                CAST(NULL AS DECIMAL(18,2))     AS AccumulatedDeterioration,
                /* Edad aplicada (no aplica en errores) */
                CAST(NULL AS NVARCHAR(200))     AS AgesDescription,
                CAST(NULL AS INT)               AS [Days],
                CAST(NULL AS INT)               AS AgesId,
                /* Datos del documento (NULL en errores) */
                CAST(NULL AS INT)               AS AccountReceivableId,
                CAST(NULL AS INT)               AS AccountReceivableType,
                CAST(NULL AS NVARCHAR(50))      AS InvoiceNumber,
                CAST(NULL AS DATETIME)          AS AccountReceivableDate,
                CAST(NULL AS DATETIME)          AS RadicatedDate,
                CAST(NULL AS DATETIME)          AS DocumentDate,
                /* Tercero */
                itp.ThirdPartyId,
                (itp.ThirdPartyNit + N' - ' + itp.ThirdPartyName) AS ThirdPartyNitName,

                /* Contexto (NULL) */
            CAST(NULL AS NVARCHAR(200))     AS RegimenName,
            CAST(NULL AS DECIMAL(18,2))     AS DocumentValue,
            CAST(NULL AS DECIMAL(18,2))     AS Balance,
            CAST(NULL AS INT)               AS GlosaPortfolioGlosadaId,
            CAST(NULL AS DECIMAL(18,2))     AS ValueGlosado,
            CAST(NULL AS DECIMAL(18,2))     AS BalanceGlosa,
            CAST(NULL AS DECIMAL(18,2))     AS DeteriorationBalance,

                /* Clasificación (NULL) */
                CAST(NULL AS INT)               AS PortfolioClassificationId,
                CAST(NULL AS NVARCHAR(300))     AS PortfolioClassification
            FROM #invalidTP itp

            UNION ALL

            /* --- VÁLIDOS --- */
            SELECT
            0     AS Code,
            NULL  AS [Message],

                /* Identificación del libro y cálculo */
                b.TypeBook,
                b.LegalBookId,
            CASE WHEN b.TypeBook = 2 THEN bp.NiifPercentage   ELSE bp.FiscalPercentage END AS Percentage,
            -- Value: Nuevo deterioro TOTAL a aplicar (Balance * Percentage)
                ROUND(bp.Balance * ISNULL(CASE WHEN b.TypeBook = 2 THEN bp.NiifPercentage ELSE bp.FiscalPercentage END, 0), 2) AS [Value],
                ISNULL(lppd.AccumulatedDeterioration, 0) AS AccumulatedDeterioration,
                /* Edad aplicada */
                CASE WHEN b.TypeBook = 2 THEN bp.AgesDescriptionNiif ELSE bp.AgesDescriptionFiscal END AS AgesDescription,
            CASE WHEN b.TypeBook = 2 THEN bp.AgeDaysNiif        ELSE bp.AgeDaysFiscal        END AS [Days],
            CASE WHEN b.TypeBook = 2 THEN bp.AgesIdNiif         ELSE bp.AgesIdFiscal         END AS AgesId,

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
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Consulta paginada del deterioro de cartera clasificado por categoría de riesgo, para una unidad operativa y una fecha de corte determinadas. Combina la configuración de cartera (SettingPortfolio), las reglas de clasificación por deterioro (RulesDeteriorationClassification) y los rangos de envejecimiento (AgesPortfolio) para calcular la antigüedad de cada cuenta por cobrar según el libro NIIF y el libro fiscal, aplicando los porcentajes de deterioro correspondientes. Integra las cuentas por cobrar (AccountReceivable y ViewAccountReceivableByPortfolioProvision) con las facturas (Invoice), el tercero pagador y la clasificación de deterioro asignada (por entidad administradora o cliente), devolviendo el saldo, el valor deteriorado y el porcentaje de provisión por cada documento de cobro. Se utiliza para el reporte de deterioro de cartera (cuentas por cobrar vencidas), análisis de riesgo de recaudo y soporte al cierre contable bajo normas NIIF y fiscales.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'PROCEDURE', @level1name = N'SP_GetPortfolioDeteriorationByClassification';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'PROCEDURE', @level1name = N'SP_GetPortfolioDeteriorationByClassification';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Calcula el deterioro de cartera por clasificación a una fecha de corte y unidad operativa, devolviendo por cada cuenta por cobrar el porcentaje y valor de deterioro NIIF y/o Fiscal, además de errores por terceros sin clasificación o porcentaje aplicable.', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'PROCEDURE', @level1name=N'SP_GetPortfolioDeteriorationByClassification';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Debe existir un registro en Portfolio.SettingPortfolio para la unidad operativa indicada con su correspondiente Portfolio.RulesDeteriorationClassification; en su defecto se asume NiifBook=1 y FiscalBook=1 (RadicatedDate); Las cuentas por cobrar consideradas deben tener DocumentDate no nulo y menor o igual a la fecha de corte; Se excluyen documentos cuyo Billing.Invoice.DocumentType = 5; Deben existir libros activos (TypeBook 1=Fiscal, 2=NIIF) en GeneralLedger.VieBot ↔ GeneralLedger.LegalBook para que se generen filas válidas', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'PROCEDURE', @level1name=N'SP_GetPortfolioDeteriorationByClassification';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Los porcentajes NIIF y Fiscal devueltos están divididos por 100 (DeteriorationNiifPercent/100, DeteriorationFiscalPercent/100); La selección del rango de edad (AgesPortfolio) usa el primer rango cuyo InitialRange/EndRange contenga la edad calculada, ordenado por InitialRange ascendente; rangos con extremos NULL se consideran abiertos; Cada AccountReceivableId aparece a lo sumo una vez en #baseP_dedup (ROW_NUMBER por AccountReceivableId); Solo se procesan TypeBook 1 (Fiscal) y 2 (NIIF); para cada TypeBook se toma el LegalBookId mínimo activo en VieBot; Se excluyen explícitamente documentos con Billing.Invoice.DocumentType = 5; Las filas de error (Code=1001) se emiten una sola vez por tercero inválido (gracias a SELECT DISTINCT y unique index sobre ThirdPartyId en #invalidTP); Un tercero clasificado como inválido no genera filas válidas (NOT EXISTS contra #invalidTP)', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'PROCEDURE', @level1name=N'SP_GetPortfolioDeteriorationByClassification';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] RESULT_SET: Devuelve un resultado UNION ALL ordenado: primero filas de error (Code=1001, una por tercero inválido) y luego filas válidas (Code=0) con cálculo de deterioro por libro; ordenadas por ThirdPartyNitName, DocumentDate, AccountReceivableId y TypeBook; [RETURN_RESULT] RESULT_SET: Para filas válidas: Percentage = NiifPercentage si TypeBook=2, FiscalPercentage si TypeBook=1; Value = ROUND(Balance * Percentage, 2); AccumulatedDeterioration = último PortfolioProvisionDetail.AccumulatedDeterioration por AccountReceivableId (0 si no existe); [RETURN_RESULT] RESULT_SET: Para terceros inválidos se emite una sola fila con Code=1001 y mensaje ''El Tercero <Nombre> no tiene una calificación de cartera asociada a la edad de cartera de la factura'', con campos de cálculo y documento en NULL', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'PROCEDURE', @level1name=N'SP_GetPortfolioDeteriorationByClassification';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si NiifBook = 1 (RadicatedDate) o 2 (DocumentDate) según Portfolio.RulesDeteriorationClassification → AgeDaysNiif = DATEDIFF(DAY, RadicatedDate o DocumentDate, @ClosingDate) else Si no hay regla configurada se usa por defecto RadicatedDate (NiifBook=1); si FiscalBook = 1 o 2 según configuración → AgeDaysFiscal se calcula con RadicatedDate o DocumentDate análogamente else Por defecto RadicatedDate (FiscalBook=1); si RegimenName de la vista es NULL → Si DocumentType=6 → ''Factura Básica''; si DocumentType=7 → ''Factura de Productos''; en otro caso NULL else Se conserva el RegimenName original de la vista; si Existe Contract.HealthAdministrator para el ThirdPartyId → ClassificationId = HealthAdministrator.PortfolioDeteriorationClassificationId (prioridad Entidad Administradora) else Se usa Common.Customer: BasicBillingDeteriorationClassificationId si DocumentType ∈ (6,7); de lo contrario HealthInvoiceDeteriorationClassificationId; si Para un tercero, ClassificationId es NULL o el porcentaje NIIF/Fiscal correspondiente no se pudo resolver según rangos de edad → El tercero se marca como inválido y se emite fila de error Code=1001 (excluyendo sus filas válidas) else Se generan filas válidas haciendo CROSS JOIN con los libros activos (#books); si TypeBook = 2 (NIIF) en la fila de salida válida → Se utilizan NiifPercentage, AgeDaysNiif, AgesDescriptionNiif y AgesIdNiif else Para TypeBook=1 (Fiscal) se utilizan FiscalPercentage, AgeDaysFiscal, AgesDescriptionFiscal y AgesIdFiscal', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'PROCEDURE', @level1name=N'SP_GetPortfolioDeteriorationByClassification';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Portfolio.SettingPortfolio; Portfolio.RulesDeteriorationClassification; Portfolio.ViewAccountReceivableByPortfolioProvision; Portfolio.AccountReceivable; Billing.Invoice; Contract.HealthAdministrator; Common.Customer; Portfolio.PortfolioDeteriorationClassification; Portfolio.PortfolioDeteriorationClassificationDetails; Portfolio.AgesPortfolio; Portfolio.PortfolioProvisionDetail; GeneralLedger.VieBot; GeneralLedger.LegalBook', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'PROCEDURE', @level1name=N'SP_GetPortfolioDeteriorationByClassification';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'PROCEDURE', @level1name=N'SP_GetPortfolioDeteriorationByClassification';
-- GO
