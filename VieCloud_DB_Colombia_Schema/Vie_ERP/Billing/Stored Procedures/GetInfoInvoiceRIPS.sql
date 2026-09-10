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
        COALESCE(ibi.CosmosId, eri.CosmoDBId) AS InvoiceCosmoDBId,
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
    LEFT JOIN Portfolio.InitialBalanceInvoice ibi on ibi.InvoiceId = epi.EntityId
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
        DATEADD(SECOND, -1, DATEADD(DAY, 1, CAST(iecPr.EndDate AS DATETIME))) AS EndDateCapitated,
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
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Procedimiento que obtiene la información completa de facturas, facturas de capitación y notas de crédito/débito necesaria para generar y enviar los archivos RIPS (Registros Individuales de Prestación de Servicios) ante la DIAN y las entidades aseguradoras. Combina datos de facturas electrónicas (con su CUFE, ruta XML y consecutivo), propiedades electrónicas de cada entidad pagadora (CUV, estado RIPS), grupos de atención del contrato y terceros (NIT del prestador) para construir el nombre del archivo RIPS en formato estándar (''ad'' + NIT + año + consecutivo). Maneja tres tipos de documentos: facturas ordinarias, facturas de capitación por monto fijo (con fechas de período de vigencia) y notas de ajuste (crédito/débito), permitiendo filtrar por número de factura o nombre de entidad. Se usa en el proceso de facturación electrónica para determinar qué documentos están listos para enviar como RIPS, cuántos reintentos llevan y cuál es su estado actual.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'PROCEDURE', @level1name = N'GetInfoInvoiceRIPS';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'PROCEDURE', @level1name = N'GetInfoInvoiceRIPS';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Consolida en un único resultset la información necesaria para generar/consultar archivos RIPS electrónicos a partir de facturas, facturas de capitación, notas de facturación y controles de capitación, calculando rutas XML, nombre AD, CUV, períodos y estado RIPS.', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'GetInfoInvoiceRIPS';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'La base de datos debe ser accesible vía DB_NAME() para construir rutas locales de XML; Debe existir configuración GeneralLedgerSettings con IdDian asociada a la unidad operativa de cada documento electrónico, y el tercero DIAN debe tener NIT; Para notas se requiere ElectronicDocument con EntityName=''BillingNote'' y la factura referenciada debe tener ElectronicsProperties y ElectronicsRIPS; Para registros de capitación con período 2 o 3 debe existir el registro previo (PreviousRIPSInvoice) en InvoiceEntityCapitated con Status=2', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'GetInfoInvoiceRIPS';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'AdFileName se construye con prefijo ''ad'' + NIT DIAN a 10 dígitos + ''000'' + 2 dígitos del año + consecutivo a 8 dígitos + ''.xml''; Solo se consideran facturas con DocumentType en (1,2,4) dentro del CTE base Cte_Invoice; Solo se consideran InvoiceEntityCapitated con Status = 2 dentro de Cte_Capited; EndDateCapitated siempre se ajusta a fin de día (último segundo del día) cuando aplica; El NIT del emisor DIAN se obtiene vía GeneralLedgerSettings.IdDian → ThirdParty; ElectronicsRIPS se enlaza siempre vía ElectronicsProperties.Id; FilePathXML para DocumentType=5 e InvoiceEntityCapitated se construye localmente bajo C:\ProgramData\Indigo Technologies\ElectronicDocuments\<DB>\<Año>\<Mes>\; Los filtros @EntityCode y @EntityName son opcionales; si son NULL no filtran', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'GetInfoInvoiceRIPS';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'RIPS (Registros Individuales de Prestación de Servicios); Factura electrónica; Notas crédito/débito (BillingNote); Capitación; CUV (Código Único de Validación); CUFE/DIAN; NIT; Grupo de atención (CareGroup); Tipo de liquidación; Período de facturación de capitación; Categoría de factura; Nota total vs parcial', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'GetInfoInvoiceRIPS';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si i.DocumentType IN (1,2) (Cte_Invoice y DocumentType <> 4) → Devuelve la fila como factura estándar con DocumentType=1, sin datos de capitación ni nota; si i.DocumentType = 4 (factura de monto fijo / capitación con liquidación) → Devuelve fila como ''InvoiceFixedAmount'' uniendo InvoiceEntityCapitated (Status=2), CareGroup e InvoiceCategories, calculando InitialDate/EndDate según LiquidationType; si cg.LiquidationType = 2 → Toma InitialDate/EndDate del registro previo (icdTemp = PreviousRIPSInvoice) y EndDate se ajusta a fin de día (DATEADD(SECOND,-1, DATEADD(DAY,1, EndDate))) else Si LiquidationType = 5 toma fechas del registro actual icd; otro valor → NULL; si ed.EntityName = ''BillingNote'' → Devuelve nota de facturación (crédito/débito) con NoteNature y AdFileName generado a partir del NIT DIAN; si bn.EntityName = ''Invoice'' OR (bnd.AdjusmentValue = i.TotalInvoice AND bn.Nature = 2) → Marca la nota como IsTotalNote = 1 (nota total) else IsTotalNote = 0 (nota parcial); si i.DocumentType = 5 → Devuelve factura de Control de Capitación con FilePathXML construido bajo carpeta ''Control Capitacion'' usando año/mes de InvoiceDate; si iec.InvoicePeriod = 3 AND iec.Status = 5 → Devuelve InvoiceEntityCapitated cerrada del último período, con FilePathXML bajo ''FacturaCapitacion'' y fechas tomadas de su PreviousRIPSInvoice; si icd.InvoicePeriod IN (2,3) → Habilita el JOIN con icdTemp (PreviousRIPSInvoice) para heredar fechas del período previo', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'GetInfoInvoiceRIPS';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Billing.Invoice; Billing.ElectronicDocument; GeneralLedger.GeneralLedgerSettings; Common.ThirdParty; Billing.InvoiceEntityCapitated; Contract.CareGroup; Billing.ElectronicsProperties; Billing.ElectronicsRIPS; Billing.InvoiceCategories; Billing.BillingNote; Billing.BillingNoteDetail', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'GetInfoInvoiceRIPS';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'GetInfoInvoiceRIPS';
-- GO
