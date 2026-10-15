-- =============================================================================
-- Author:      Juan David Capera
-- Create date: 2023-04-13
-- Description: Vista que consolida la información requerida por el API de
--              integración de factura electrónica. Retorna, en un mismo
--              conjunto de columnas, los datos de:
--                  * Facturas              (EntityName = 'Invoice')
--                  * Notas de facturación  (EntityName = 'BillingNote')
-- =============================================================================

CREATE VIEW [Billing].[ViewElectronicDocumentMoreInformation]
AS
    -- -------------------------------------------------------------------------
    -- CTE: cte_LastElectronicDocument
    -- Obtiene el último ElectronicDocument emitido por cada factura.
    -- -------------------------------------------------------------------------
    WITH cte_LastElectronicDocument AS (
        SELECT
            ed.EntityId,
            MAX(ed.Id) AS LastEdId
        FROM Billing.ElectronicDocument ed WITH (NOLOCK)
        WHERE ed.EntityName = 'Invoice'
        GROUP BY ed.EntityId
    ),

    -- -------------------------------------------------------------------------
    -- CTE: cte_Header
    -- Datos base de la factura cruzados con su último documento electrónico
    -- y su cuenta por cobrar asociada.
    -- -------------------------------------------------------------------------
    cte_Header AS (
        SELECT
            i.Id                        AS EntityId,
            'Invoice'                   AS EntityName,
            i.InvoiceValue              AS Value,
            i.TotalValue,
            i.Observation,
            i.TaxDevolutionValue,
            i.InvoiceNumber             AS InternalInvoice,
            i.CurrencyId,
            i.ThirdPartyId,
            i.CareGroupId,
            i.AdmissionNumber,
            led.LastEdId                AS Id,
            ar.InvoiceId,
            ar.Term,
            ar.Id                       AS AccountReceivableId,
            ar.Balance,
            i.RevenueControlDetailId,
            i.InvoiceDate
        FROM Billing.Invoice i WITH (NOLOCK)
        JOIN cte_LastElectronicDocument led
            ON led.EntityId = i.Id
        JOIN Billing.ElectronicDocument ed WITH (NOLOCK)
            ON ed.Id = led.LastEdId
        JOIN Portfolio.AccountReceivable ar WITH (NOLOCK)
            ON i.Id = ar.InvoiceId
    ),

    -- -------------------------------------------------------------------------
    -- CTE: PaymentMethods
    -- Consolida los medios de pago de la factura a partir de tres orígenes:
    --   1. Traslados de cartera con anticipos liquidados (Balance = 0).
    --   2. Recibos de caja aplicados directamente a la cuenta por cobrar.
    --   3. Folios tipo particular con saldo pendiente (se asigna código 99).
    -- -------------------------------------------------------------------------
    PaymentMethods AS (
        SELECT
            payment.InvoiceId,
            STRING_AGG(payment.PaymentMethodTypes, ',') AS PaymentMethodTypes,
            0.0                                         AS Value
        FROM (
            -- (1) Medios de pago por traslados de cartera
            SELECT
                ar.InvoiceId,
                CAST(mc.PaymentMethodTypes AS VARCHAR(2)) AS PaymentMethodTypes,
                0.0                                       AS Value
            FROM cte_Header ar
            JOIN Portfolio.PortfolioTransferDetail ptd WITH (NOLOCK)
                ON ar.AccountReceivableId = ptd.AccountReceivableId
            JOIN Portfolio.PortfolioTransfer pt WITH (NOLOCK)
                ON ptd.PortfolioTrasferId = pt.Id
            JOIN Portfolio.PortfolioAdvance pa WITH (NOLOCK)
                ON pt.PortfolioAdvanceId = pa.Id
            JOIN Treasury.CashReceipts cr WITH (NOLOCK)
                ON pa.CashReceiptId = cr.Id
            LEFT JOIN Treasury.PaymentMethods mc WITH (NOLOCK)
                ON mc.IdCashReceipt = cr.Id
            WHERE pt.Status = 2
              AND cr.Status = 2
              AND ROUND(ar.Balance, -1) = 0
            GROUP BY ar.InvoiceId, mc.PaymentMethodTypes

            UNION ALL

            -- (2) Medios de pago por recibos de caja aplicados a la CxC
            SELECT
                ar.InvoiceId,
                CAST(mc.PaymentMethodTypes AS VARCHAR(2)) AS PaymentMethodTypes,
                0.0                                       AS Value
            FROM cte_Header ar WITH (NOLOCK)
            JOIN Treasury.CashReceiptAccountReceivable crar WITH (NOLOCK)
                ON ar.AccountReceivableId = crar.AccountReceivableId
            JOIN Treasury.CashReceiptDetails crd WITH (NOLOCK)
                ON crar.CashReceiptDetailId = crd.Id
            JOIN Treasury.CashReceipts cr WITH (NOLOCK)
                ON crd.IdCashReceipt = cr.Id
            LEFT JOIN Treasury.PaymentMethods mc WITH (NOLOCK)
                ON mc.IdCashReceipt = cr.Id
            WHERE cr.Status = 2
              AND ROUND(ar.Balance, -1) = 0
            GROUP BY ar.InvoiceId, mc.PaymentMethodTypes

            UNION ALL

            -- (3) Folio tipo particular con saldo pendiente
            SELECT
                ar.InvoiceId,
                '99' AS PaymentMethodTypes,
                0.0  AS Value
            FROM cte_Header ar WITH (NOLOCK)
            JOIN Billing.RevenueControlDetail rcd
                ON rcd.id = ar.RevenueControlDetailId
            WHERE rcd.FolioType = 3
              AND ROUND(ar.Balance, -1) <> 0
        ) payment
        GROUP BY payment.InvoiceId
    ),

    -- -------------------------------------------------------------------------
    -- CTE: cte_AdditionalData
    -- Calcula, por documento electrónico:
    --   * EconomicActivityCode         -> actividad económica del emisor
    --     (detalle de servicio o, en su defecto, facturación básica).
    --   * DocumentTypeCode             -> código DIAN del tipo de documento.
    --   * ReceiverEconomicActivityCode -> actividad económica del receptor.
    -- -------------------------------------------------------------------------
    cte_AdditionalData AS (
        SELECT
            ed.Id,
            COALESCE(sod.Code, bb.Code, '') AS EconomicActivityCode,
            CASE
                WHEN ed.DocumentType       = 91 THEN '03'
                WHEN ed.DocumentType       = 92 THEN '02'
                WHEN i.IsElectronicTicket  = 0  THEN '01'
                WHEN i.IsElectronicTicket  = 1  THEN '04'
                ELSE ''
            END                             AS DocumentTypeCode,
            ISNULL(ea.Code, '')             AS ReceiverEconomicActivityCode
        FROM Billing.ElectronicDocument ed WITH (NOLOCK)
        LEFT JOIN Billing.Invoice i WITH (NOLOCK)
            ON i.Id = ed.EntityId
           AND 'Invoice' = ed.EntityName
        LEFT JOIN Common.EconomicActivity ea WITH (NOLOCK)
            ON i.EconomicActivityId = ea.Id
        -- Actividad económica a partir de detalles de órdenes de servicio
        LEFT JOIN (
            SELECT
                id.InvoiceId,
                MIN(ea.Code) AS Code
            FROM Billing.InvoiceDetail id WITH (NOLOCK)
            INNER JOIN Billing.ServiceOrderDetail sod WITH (NOLOCK)
                ON sod.Id = id.ServiceOrderDetailId
            INNER JOIN Common.EconomicActivity ea WITH (NOLOCK)
                ON ea.Id = sod.EconomicActivityId
            GROUP BY id.InvoiceId
        ) AS sod
            ON sod.InvoiceId = i.Id
        -- Fallback: actividad económica a partir de facturación básica
        LEFT JOIN (
            SELECT
                bb.InvoiceId,
                MIN(ea.Code) AS Code
            FROM Billing.BasicBilling bb WITH (NOLOCK)
            INNER JOIN Billing.BasicBillingDetail bbd WITH (NOLOCK)
                ON bbd.BasicBillingId = bb.Id
            INNER JOIN Common.EconomicActivity ea WITH (NOLOCK)
                ON ea.Id = bbd.EconomicActivityId
            GROUP BY bb.InvoiceId
        ) AS bb
            ON bb.InvoiceId = i.Id
    ),

    -- -------------------------------------------------------------------------
    -- CTE: cte_ThirdPartyEA
    -- Fallback para ReceiverEconomicActivityCode cuando la factura no tiene
    -- actividad económica asignada: toma la actividad del tercero, priorizando
    -- la marcada por defecto y, en empate, la de menor Id.
    -- -------------------------------------------------------------------------
    cte_ThirdPartyEA AS (
        SELECT
            tea.ThirdPartyId,
            ea.Code AS EconomicActivityCode_ThirdParty,
            ROW_NUMBER() OVER (
                PARTITION BY tea.ThirdPartyId
                ORDER BY
                    CASE WHEN ISNULL(tea.Defect, 0) = 1 THEN 0 ELSE 1 END,
                    tea.Id ASC
            ) AS rn
        FROM Common.ThirdPartyEconomicActivities tea WITH (NOLOCK)
        INNER JOIN Common.EconomicActivity ea WITH (NOLOCK)
            ON ea.Id = tea.EconomicActivityId
    ),

    -- -------------------------------------------------------------------------
    -- CTE: cte_BillingNoteTRM
    -- TRM aplicable a cada nota de facturación, tomada de la primera factura
    -- asociada (menor BillingNoteDetail.Id). Para fechas <= 2026-01-19 se
    -- fuerza TRM = 1 por regla de negocio.
    -- -------------------------------------------------------------------------
    cte_BillingNoteTRM AS (
        SELECT
            trm.BillingNoteId,
            trm.TRMValue,
            trm.TRMValueReverse
        FROM (
            SELECT
                bnd.BillingNoteId,
                IIF(i.InvoiceDate <= '2026-01-19', 1, ISNULL(trm.ValueOfficialToCurrency, 1)) AS TRMValue,
                arer.ValueReverse AS TRMValueReverse,
                ROW_NUMBER() OVER (PARTITION BY bnd.BillingNoteId ORDER BY bnd.Id) AS RowNum
            FROM Billing.BillingNoteDetail bnd WITH (NOLOCK)
            JOIN Billing.Invoice i WITH (NOLOCK)
                ON bnd.InvoiceId = i.Id
            JOIN Portfolio.AccountReceivable ar WITH (NOLOCK)
                ON i.Id = ar.InvoiceId
            JOIN Portfolio.AccountReceivableExchangeRate arer WITH (NOLOCK)
                ON ar.Id = arer.AccountReceivableId
            LEFT JOIN Common.TRM trm
                ON trm.MeasurementDate = CONVERT(DATE, i.InvoiceDate)
        ) trm
        WHERE trm.RowNum = 1
    )

    /* =========================================================================
       RECOLECCIÓN DE DATOS
       -------------------------------------------------------------------------
       Bloque 1: Facturas        (EntityName = 'Invoice')
       Bloque 2: Notas de factur. (EntityName = 'BillingNote')
       ========================================================================= */

    -- =========================================================================
    -- Bloque 1: FACTURAS
    -- =========================================================================
    SELECT
        -------- Datos de la factura --------
        cte.Id,
        cte.EntityId,
        'Invoice' AS EntityName,
        ISNULL(cte.CurrencyId, cs.OfficialCurrencyId) AS CurrencyDocumentId,
        ISNULL(c.Abbreviation, co.Abbreviation)       AS CurrencyAbbreviation,
        cs.OfficialCurrencyId,
        CASE
            WHEN cte.InvoiceDate <= '2026-01-19' THEN 1
            WHEN ISNULL(cte.CurrencyId, cs.OfficialCurrencyId) = 2
                THEN COALESCE(trm.ValueOfficialToCurrency, 1)
            ELSE 1
        END                                   AS TRMValue,
        ISNULL(arer.ValueReverse, 1)          AS TRMValueReverse,
        cte.Value,
        cte.TotalValue,
        cte.Observation,
        cte.Term,

        -------- Datos del tercero --------
        tp.Nit                                AS ThirdPartyNit,
        tp.Name                               AS ThirdPartyName,
        tpIden.SIGLA                          AS ThirdPartyIdentification,
        tipId.SIGLA                           AS PatientIdentification,
        ISNULL(tp.Class, 1)                   AS ThirdPartyClass,
        tp.ContributionType,
        RIGHT('00' + LEFT(LTRIM(RTRIM(distrito.UBICODIGO)), 2), 2) AS 'Distrito',
        RIGHT('00' + LEFT(LTRIM(RTRIM(canton.UBICODIGO)),   2), 2) AS 'Canton',
        LEFT(RTRIM(provincia.UBICODIGO), 1)                        AS 'Provincia',
        '0'                                   AS Pais,
        pa.Address                            AS ThirdPartyAddress,
        SUBSTRING(
            REPLACE(
                (SELECT ',' + e.Email
                 FROM Common.Email e
                 WHERE e.Type = 2 AND e.IdPerson = tp.PersonId
                 FOR XML PATH ('')),
                '&#x0D;', ''),
            2, 100)                           AS Emails,
        '0'                                   AS Phone,

        -------- Datos del pago --------
        ISNULL(pm.Value, 0)                   AS PayValue,
        pm.PaymentMethodTypes,
        CAST(
            IIF(bb.Id IS NOT NULL,
                IIF(bb.SaleModality = 1, 1, 0),
                IIF(pm.Value > 0, 1, 0)
            ) AS BIT)                         AS IsCounted,
        css.Code                              AS ConditionSalesCode,
        cte.TaxDevolutionValue,
        cte.InternalInvoice,

        -------- Datos del paciente / admisión --------
        TRIM(realPac.IPCODPACI)               AS RealPatientIdentification,
        TRIM(realPac.IPNOMCOMP)               AS RealPatientName,
        TRIM(ing.NUMINGRES)                   AS Admission,

        -------- Datos adicionales (códigos DIAN) --------
        ad.DocumentTypeCode,
        ad.EconomicActivityCode,
        COALESCE(NULLIF(ad.ReceiverEconomicActivityCode, ''), tpea.EconomicActivityCode_ThirdParty, '') AS ReceiverEconomicActivityCode
    FROM cte_Header cte
    JOIN Common.ThirdParty tp WITH (NOLOCK)
        ON tp.Id = cte.ThirdPartyId
    JOIN Common.Person p WITH (NOLOCK)
        ON p.Id = tp.PersonId
    JOIN GeneralLedger.CompanySettings cs WITH (NOLOCK)
        ON 1 = 1
    JOIN Common.Currency co WITH (NOLOCK)
        ON co.Id = cs.OfficialCurrencyId
    -- Propiedades electrónicas y condición de venta
    LEFT JOIN Billing.ElectronicsProperties ep
        ON ep.EntityId = cte.EntityId
       AND 'Invoice'   = cte.EntityName
    LEFT JOIN Billing.ConditionSales css
        ON css.Id = ep.ConditionSalesId
    -- Tipo de identificación del tercero
    LEFT JOIN dbo.ADTIPOIDENTIFICA tpIden
        ON tpIden.ID = p.IdentificationTypeId
    -- Facturación básica y grupo de atención
    LEFT JOIN Billing.BasicBilling bb
        ON bb.InvoiceId = cte.EntityId
    LEFT JOIN Contract.CareGroup cg WITH (NOLOCK)
        ON cg.Id = cte.CareGroupId
    LEFT JOIN Common.Currency c WITH (NOLOCK)
        ON c.Id = cte.CurrencyId
    -- Paciente
    LEFT JOIN ADINGRESO ing
        ON ing.NUMINGRES = cte.AdmissionNumber
    LEFT JOIN INPACIENT realPac WITH (NOLOCK)
        ON realPac.IPCODPACI = ing.IPCODPACI
    LEFT JOIN INPACIENT pac WITH (NOLOCK)
        ON pac.IPCODPACI = tp.Nit
    LEFT JOIN ADTIPOIDENTIFICA tipId
        ON tipId.CODIGO = pac.IPTIPODOC
    LEFT JOIN Admissions.PatientAddress pa WITH (NOLOCK)
        ON pac.ID = pa.IdPatient
    -- Ubicación (distrito / cantón / provincia)
    LEFT JOIN INUBICACI distrito WITH (NOLOCK)
        ON distrito.ID = pa.IdUbication
    LEFT JOIN INUBICACI canton WITH (NOLOCK)
        ON distrito.UbicationId = canton.ID
    LEFT JOIN INUBICACI provincia WITH (NOLOCK)
        ON canton.UbicationId = provincia.ID
    -- Medios de pago
    LEFT JOIN PaymentMethods pm
        ON cte.EntityId = pm.InvoiceId
    -- Datos adicionales y fallback de actividad económica
    LEFT JOIN cte_AdditionalData ad
        ON cte.Id = ad.Id
    LEFT JOIN cte_ThirdPartyEA tpea
        ON tpea.ThirdPartyId = cte.ThirdPartyId
       AND tpea.rn = 1
    -- Tasas de cambio
    LEFT JOIN Portfolio.AccountReceivableExchangeRate arer WITH (NOLOCK)
        ON cte.AccountReceivableId = arer.AccountReceivableId
    LEFT JOIN Common.TRM trm
        ON trm.MeasurementDate = CONVERT(DATE, cte.InvoiceDate)

    UNION ALL

    -- =========================================================================
    -- Bloque 2: NOTAS DE FACTURACIÓN
    -- =========================================================================
    SELECT
        -------- Datos de la nota --------
        ed.Id,
        bn.Id                                 AS EntityId,
        'BillingNote'                         AS EntityName,
        ISNULL(pn.CurrencyId, i.CurrencyId)   AS CurrencyDocumentId,
        ISNULL(cn.Abbreviation, ci.Abbreviation) AS CurrencyDocumentAbbreviation,
        cs.OfficialCurrencyId,
        CASE
            WHEN ISNULL(pn.CurrencyId, i.CurrencyId) = 2
                THEN trm.TRMValue
            ELSE 1
        END                                   AS TRMValue,
        ISNULL(trm.TRMValueReverse, 1)        AS TRMValueReverse,
        0                                     AS Value,
        0                                     AS TotalValue,
        bn.Observations                       AS Observation,
        0                                     AS Term,

        -------- Datos del tercero --------
        tp.Nit                                AS ThirdPartyNit,
        tp.Name                               AS ThirdPartyName,
        tpIden.SIGLA                          AS ThirdPartyIdentification,
        tipId.SIGLA                           AS PatientIdentification,
        ISNULL(tp.Class, 1)                   AS ThirdPartyClass,
        tp.ContributionType,
        RIGHT('00' + LEFT(LTRIM(RTRIM(distrito.UBICODIGO)), 2), 2) AS 'Distrito',
        RIGHT('00' + LEFT(LTRIM(RTRIM(canton.UBICODIGO)),   2), 2) AS 'Canton',
        LEFT(RTRIM(provincia.UBICODIGO), 1)                        AS 'Provincia',
        '0'                                   AS 'Pais',
        pa.Address                            AS ThirdPartyAddress,
        SUBSTRING(
            REPLACE(
                (SELECT ',' + e.Email
                 FROM Common.Email e
                 WHERE e.Type = 2 AND e.IdPerson = tp.PersonId
                 FOR XML PATH ('')),
                '&#x0D;', ''),
            2, 100)                           AS Emails,
        '0'                                   AS Phone,

        -------- Datos del pago (no aplica a notas) --------
        0                                     AS PayValue,
        NULL                                  AS PaymentMethodTypes,
        CAST(1 AS BIT)                        AS IsCounted,
        NULL                                  AS ConditionSalesCode,
        ISNULL(i.TaxDevolutionValue, 0)       AS TaxDevolutionValue,
        NULL                                  AS InternalInvoice,

        -------- Datos del paciente / admisión (no aplica a notas) --------
        NULL                                  AS RealPatientIdentification,
        NULL                                  AS RealPatientName,
        NULL                                  AS Admission,

        -------- Datos adicionales (códigos DIAN) --------
        ad.DocumentTypeCode,
        ad.EconomicActivityCode,
        COALESCE(NULLIF(ad.ReceiverEconomicActivityCode, ''), tpea.EconomicActivityCode_ThirdParty, '') AS ReceiverEconomicActivityCode
    FROM Billing.ElectronicDocument ed WITH (NOLOCK)
    JOIN Billing.BillingNote bn WITH (NOLOCK)
        ON bn.Id = ed.EntityId
       AND 'BillingNote' = ed.EntityName
    JOIN Common.ThirdParty tp WITH (NOLOCK)
        ON tp.Id = ed.CustomerPartyId
    JOIN Common.Person p WITH (NOLOCK)
        ON p.Id = tp.PersonId
    JOIN GeneralLedger.CompanySettings cs WITH (NOLOCK)
        ON 1 = 1
    JOIN Common.Currency co WITH (NOLOCK)
        ON co.Id = cs.OfficialCurrencyId
    -- Tipo de identificación del tercero
    LEFT JOIN dbo.ADTIPOIDENTIFICA tpIden
        ON tpIden.ID = p.IdentificationTypeId
    -- Origen de la nota: PortfolioNote o Invoice
    LEFT JOIN Portfolio.PortfolioNote pn WITH (NOLOCK)
        ON pn.Id = bn.EntityId
       AND 'PortfolioNote' = bn.EntityName
    LEFT JOIN Common.Currency cn WITH (NOLOCK)
        ON cn.Id = pn.CurrencyId
    LEFT JOIN Billing.Invoice i WITH (NOLOCK)
        ON i.Id = bn.EntityId
       AND 'Invoice' = bn.EntityName
    LEFT JOIN Common.Currency ci WITH (NOLOCK)
        ON ci.Id = i.CurrencyId
    LEFT JOIN Common.EconomicActivity ea
        ON i.EconomicActivityId = ea.Id
    -- Paciente
    LEFT JOIN INPACIENT pac WITH (NOLOCK)
        ON pac.IPCODPACI = tp.Nit
    LEFT JOIN ADTIPOIDENTIFICA tipId
        ON tipId.CODIGO = pac.IPTIPODOC
    LEFT JOIN Admissions.PatientAddress pa WITH (NOLOCK)
        ON pac.ID = pa.IdPatient
    -- Ubicación (distrito / cantón / provincia)
    LEFT JOIN INUBICACI distrito WITH (NOLOCK)
        ON distrito.ID = pa.IdUbication
    LEFT JOIN INUBICACI canton WITH (NOLOCK)
        ON distrito.UbicationId = canton.ID
    LEFT JOIN INUBICACI provincia WITH (NOLOCK)
        ON canton.UbicationId = provincia.ID
    -- Datos adicionales, fallback de actividad económica y TRM
    LEFT JOIN cte_AdditionalData ad
        ON ed.Id = ad.Id
    LEFT JOIN cte_ThirdPartyEA tpea
        ON tpea.ThirdPartyId = ed.CustomerPartyId
       AND tpea.rn = 1
    LEFT JOIN cte_BillingNoteTRM trm
        ON bn.Id = trm.BillingNoteId
GO
