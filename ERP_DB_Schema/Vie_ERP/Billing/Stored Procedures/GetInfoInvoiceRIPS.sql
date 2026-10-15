CREATE PROCEDURE [Billing].[GetInfoInvoiceRIPS]
    @EntityCode NVARCHAR(50) = NULL,
    @EntityName NVARCHAR(50) = NULL
AS
BEGIN
    SET NOCOUNT ON;
 
    DECLARE @dbName NVARCHAR(128);
    SET @dbName = DB_NAME();
 
    WITH Cte_Invoice AS (
        SELECT 
            i.Id AS EntityId,
            i.InvoiceNumber AS EntityCode,
            ed.EntityName,
            ed.FilePath AS FilePathXML,
            CONCAT('ad',
                RIGHT(CONCAT('0000000000', th.Nit), 10),
                '000',
                SUBSTRING(CAST(ed.Year AS VARCHAR), 3, 2),
                RIGHT(CONCAT('00000000', ed.Consecutive), 8), '.xml') AS AdFileName,
            i.CareGroupId,
            i.DocumentType,
            i.InvoiceCategoryId
        FROM Billing.Invoice i WITH(NOLOCK)
        INNER JOIN Billing.ElectronicDocument ed ON ed.EntityId = i.Id AND ed.EntityName = 'Invoice'
        INNER JOIN GeneralLedger.GeneralLedgerSettings gs WITH(NOLOCK) ON gs.IdOperatingUnit = ed.OperatingUnitId
        INNER JOIN Common.ThirdParty th WITH(NOLOCK) ON th.Id = gs.IdDian
        WHERE i.DocumentType IN (1, 2, 4)
    ),
    Cte_Capited AS (
        SELECT 
            iec.*
        FROM Billing.InvoiceEntityCapitated iec WITH(NOLOCK)
        INNER JOIN Cte_Invoice cte ON cte.EntityId = iec.InvoiceId
        WHERE iec.Status = 2
    )
 
    SELECT -- 1er SELECT
        i.EntityId,
        i.EntityCode,
        i.EntityName,
        i.FilePathXML,
        er.Retry AS RetryRIPS,
        CAST(1 AS TINYINT) AS DocumentType,
        NULL AS CareGroupCode,
        NULL AS CategoryInvoiceCode,
        NULL AS InitialDateCapitated,
        NULL AS EndDateCapitated,
        i.AdFileName,
        CAST(NULL AS TINYINT) AS NoteNature,
        er.CosmoDBId AS InvoiceCosmoDBId,
        CAST(0 AS BIT) AS IsTotalNote,
        ep.CUV AS InvoiceCUV,
        CAST(cg.LiquidationType AS TINYINT) AS CareGroupLiquidationType,
        CAST(NULL AS TINYINT) AS CapitedInvoicePeriod,
        CAST(ep.StatusRIPS AS TINYINT) AS StatusRIPS
    FROM Cte_Invoice i
    LEFT JOIN Contract.CareGroup cg WITH(NOLOCK) ON i.CareGroupId = cg.Id
    LEFT JOIN Billing.ElectronicsProperties ep ON i.EntityId = ep.EntityId AND i.EntityName = ep.EntityName
    LEFT JOIN Billing.ElectronicsRIPS er ON er.ElectronicsPropertiesId = ep.Id
    WHERE i.DocumentType <> 4
      AND (@EntityCode IS NULL OR i.EntityCode = @EntityCode)
      AND (@EntityName IS NULL OR i.EntityName = @EntityName)
 
    UNION ALL
 
    SELECT -- 2do SELECT
        i.EntityId,
        i.EntityCode,
        'InvoiceFixedAmount' AS EntityName,
        i.FilePathXML,
        er.Retry AS RetryRIPS,
        CAST(i.DocumentType AS TINYINT) AS DocumentType,
        cg.Code AS CareGroupCode,
        ic.Code AS CategoryInvoiceCode,
  CASE cg.LiquidationType
   WHEN 2 THEN CAST(icdTemp.InitialDate as DATETIME)
   WHEN 5 THEN CAST(icd.InitialDate as DATETIME)
        ELSE NULL
  END AS InitialDateCapitated,
  CASE cg.LiquidationType
   WHEN 2 THEN 
    CAST(
      DATEADD(SECOND, -1,
     DATEADD(DAY, 1, CAST(CONVERT(date, icdTemp.EndDate) AS DATETIME))
      ) AS DATETIME
    )
   WHEN 5 THEN 
    CAST(
      DATEADD(SECOND, -1,
     DATEADD(DAY, 1, CAST(CONVERT(date, icd.EndDate) AS DATETIME))
      ) AS DATETIME
    )
        ELSE NULL
  END AS EndDateCapitated,
        i.AdFileName,
        CAST(NULL AS TINYINT) AS NoteNature,
        er.CosmoDBId AS InvoiceCosmoDBId,
        CAST(0 AS BIT) AS IsTotalNote,
        ep.CUV AS InvoiceCUV,
        CAST(cg.LiquidationType AS TINYINT) AS CareGroupLiquidationType,
        CAST(icd.InvoicePeriod AS TINYINT) AS CapitedInvoicePeriod,
        CAST(ep.StatusRIPS AS TINYINT) AS StatusRIPS
    FROM Cte_Invoice i
    INNER JOIN Contract.CareGroup cg WITH(NOLOCK) ON i.CareGroupId = cg.Id
    INNER JOIN Billing.InvoiceCategories ic WITH(NOLOCK) ON i.InvoiceCategoryId = ic.Id
    INNER JOIN Cte_Capited icd ON icd.InvoiceId = i.EntityId
    LEFT JOIN Cte_Capited icdTemp ON icd.PreviousRIPSInvoice = icdTemp.Id AND icd.InvoicePeriod IN (2, 3)
    LEFT JOIN Billing.ElectronicsProperties ep ON i.EntityId = ep.EntityId AND i.EntityName = ep.EntityName
    LEFT JOIN Billing.ElectronicsRIPS er ON er.ElectronicsPropertiesId = ep.Id
    WHERE i.DocumentType = 4
      AND (@EntityCode IS NULL OR i.EntityCode = @EntityCode)
      AND (@EntityName IS NULL OR 'InvoiceFixedAmount' = @EntityName)
 
    UNION ALL
 
    SELECT -- 3er SELECT (Billing Note)
        bn.Id AS EntityId,
        bn.Code AS EntityCode,
        ed.EntityName,
        ed.FilePath AS FilePathXML,
        er.Retry AS RetryRIPS,
        CAST(NULL AS TINYINT) AS DocumentType,
        NULL AS CareGroupCode,
        NULL AS CategoryInvoiceCode,
        NULL AS InitialDateCapitated,
        NULL AS EndDateCapitated,
        CONCAT('ad',
            RIGHT(CONCAT('0000000000', th.Nit), 10),
            '000',
            SUBSTRING(CAST(ed.Year AS VARCHAR), 3, 2),
            RIGHT(CONCAT('00000000', ed.Consecutive), 8), '.xml') AS AdFileName,
        CAST(bn.Nature AS TINYINT) AS NoteNature,
        eri.CosmoDBId AS InvoiceCosmoDBId,
        CAST(
            CASE
                WHEN bn.EntityName = 'Invoice' THEN 1
                WHEN bnd.AdjusmentValue = i.TotalInvoice AND bn.Nature = 2 THEN 1
                ELSE 0
            END AS BIT
        ) AS IsTotalNote,
        epi.CUV AS InvoiceCUV,
        CAST(NULL AS TINYINT) AS CareGroupLiquidationType,
        CAST(NULL AS TINYINT) AS CapitedInvoicePeriod,
        CAST(ep.StatusRIPS AS TINYINT) AS StatusRIPS
    FROM Billing.BillingNote bn WITH(NOLOCK)
    INNER JOIN Billing.BillingNoteDetail bnd WITH(NOLOCK) ON bn.Id = bnd.BillingNoteId
    INNER JOIN Billing.Invoice i WITH(NOLOCK) ON bnd.InvoiceId = i.Id
    INNER JOIN Billing.ElectronicsProperties epi WITH(NOLOCK) ON i.Id = epi.EntityId AND epi.EntityName = 'Invoice'
    INNER JOIN Billing.ElectronicsRIPS eri WITH(NOLOCK) ON epi.Id = eri.ElectronicsPropertiesId
    INNER JOIN Billing.ElectronicDocument ed ON ed.EntityId = bn.Id
    INNER JOIN GeneralLedger.GeneralLedgerSettings gs WITH(NOLOCK) ON gs.IdOperatingUnit = ed.OperatingUnitId
    INNER JOIN Common.ThirdParty th WITH(NOLOCK) ON th.Id = gs.IdDian
    LEFT JOIN Billing.ElectronicsProperties ep ON ed.EntityId = ep.EntityId AND ed.EntityName = ep.EntityName
    LEFT JOIN Billing.ElectronicsRIPS er ON er.ElectronicsPropertiesId = ep.Id
    WHERE ed.EntityName = 'BillingNote'
      AND (@EntityCode IS NULL OR bn.Code = @EntityCode)
      AND (@EntityName IS NULL OR ed.EntityName = @EntityName)
 
    UNION ALL
 
    SELECT -- 4to SELECT (Invoice tipo 5)
        i.Id AS EntityId,
        i.InvoiceNumber AS EntityCode,
        'Invoice' AS EntityName,
        CONCAT('C:\ProgramData\Indigo Technologies\ElectronicDocuments\', @dbName, '\', YEAR(i.InvoiceDate), '\', MONTH(i.InvoiceDate), '\', 'Control Capitacion', '\', i.InvoiceNumber) AS FilePathXML,
        er.Retry AS RetryRIPS,
        CAST(i.DocumentType AS TINYINT) AS DocumentType,
        cg.Code AS CareGroupCode,
        NULL AS CategoryInvoiceCode,
        NULL AS InitialDateCapitated,
        NULL AS EndDateCapitated,
        '' AS AdFileName,
        CAST(NULL AS TINYINT) AS NoteNature,
        er.CosmoDBId AS InvoiceCosmoDBId,
        CAST(0 AS BIT) AS IsTotalNote,
        ep.CUV AS InvoiceCUV,
        CAST(cg.LiquidationType AS TINYINT) AS CareGroupLiquidationType,
        CAST(NULL AS TINYINT) AS CapitedInvoicePeriod,
        CAST(ep.StatusRIPS AS TINYINT) AS StatusRIPS
    FROM Billing.Invoice i WITH(NOLOCK)
    INNER JOIN Contract.CareGroup cg WITH(NOLOCK) ON i.CareGroupId = cg.Id
    LEFT JOIN Billing.ElectronicsProperties ep ON i.Id = ep.EntityId AND ep.EntityName = 'Invoice'
    LEFT JOIN Billing.ElectronicsRIPS er ON er.ElectronicsPropertiesId = ep.Id
    WHERE i.DocumentType = 5
      AND (@EntityCode IS NULL OR i.InvoiceNumber = @EntityCode)
      AND (@EntityName IS NULL OR 'Invoice' = @EntityName)
 
    UNION ALL
 
    SELECT -- 5to SELECT (InvoiceEntityCapitated)
        iec.Id AS EntityId,
        iec.Code AS EntityCode,
        'InvoiceEntityCapitated' AS EntityName,
        CONCAT('C:\ProgramData\Indigo Technologies\ElectronicDocuments\', @dbName, '\', YEAR(iec.DocumentDate), '\', MONTH(iec.DocumentDate), '\', 'FacturaCapitacion', '\', iec.Code) AS FilePathXML,
        er.Retry AS RetryRIPS,
        CAST(4 AS TINYINT) AS DocumentType,
        cg.Code AS CareGroupCode,
        ic.Code AS CategoryInvoiceCode,
        iecPr.InitialDate AS InitialDateCapitated,
        iecPr.EndDate AS EndDateCapitated,
        '' AS AdFileName,
        CAST(NULL AS TINYINT) AS NoteNature,
        er.CosmoDBId AS InvoiceCosmoDBId,
        CAST(0 AS BIT) AS IsTotalNote,
        ep.CUV AS InvoiceCUV,
        CAST(cg.LiquidationType AS TINYINT) AS CareGroupLiquidationType,
        iec.InvoicePeriod AS CapitedInvoicePeriod,
        CAST(ep.StatusRIPS AS TINYINT) AS StatusRIPS
    FROM Billing.InvoiceEntityCapitated iec WITH(NOLOCK)
    INNER JOIN Contract.CareGroup cg WITH(NOLOCK) ON iec.CareGroupId = cg.Id
    INNER JOIN Billing.InvoiceCategories ic WITH(NOLOCK) ON iec.InvoiceCategoryId = ic.Id
    INNER JOIN Cte_Capited iecPr ON iecPr.Id = iec.PreviousRIPSInvoice
    INNER JOIN Billing.ElectronicsProperties ep ON iec.Id = ep.EntityId AND ep.EntityName = 'InvoiceEntityCapitated'
    INNER JOIN Billing.ElectronicsRIPS er ON er.ElectronicsPropertiesId = ep.Id
    WHERE iec.InvoicePeriod = 3 AND iec.Status = 5
      AND (@EntityCode IS NULL OR iec.Code = @EntityCode)
      AND (@EntityName IS NULL OR 'InvoiceEntityCapitated' = @EntityName)
END