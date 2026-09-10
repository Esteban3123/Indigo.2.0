CREATE PROCEDURE [Glosas].[SP_GenerateAdresFurServiciosData]
    @XmlParameters AS XML,
    @XmlInvoices   AS XML
AS
BEGIN
    SET NOCOUNT ON;

    /***************************************** VARIABLES *****************************************/

    DECLARE @RadicateInvoiceId INT,
            @FilterByInvoices  BIT = 0;

    DECLARE @Table_Invoices TABLE(InvoiceId INT);

    DECLARE @Invoices TABLE
    (
        InvoiceId           INT,
        InvoiceNumber       VARCHAR(20),
        DetailInvoiceId     INT,
        DetailInvoiceNumber VARCHAR(20)
    );

    BEGIN TRY

        /*************************************** CRITERIOS ***************************************/

        SELECT @RadicateInvoiceId = t.x.value('RadicateInvoiceId[1]','int')
        FROM @XmlParameters.nodes('/Data') t(x);

        INSERT INTO @Table_Invoices
            SELECT DISTINCT t.x.value('InvoiceId[1]','int') InvoiceId
            FROM @XmlInvoices.nodes('/Data') t(x);

        IF EXISTS(SELECT 1 FROM @Table_Invoices)
        BEGIN
            SET @FilterByInvoices = 1;
        END

        /*********************** RESOLUCIÓN DE FACTURAS (filtro Aseguradora/Fosyga) ***************/

        INSERT INTO @Invoices (InvoiceId, InvoiceNumber, DetailInvoiceId, DetailInvoiceNumber)
            SELECT DISTINCT
                i.Id                                                                   AS InvoiceId,
                i.InvoiceNumber                                                        AS InvoiceNumber,
                IIF(i.DocumentType = 4, cc.Id,            i.Id)                        AS DetailInvoiceId,
                IIF(i.DocumentType = 4, cc.InvoiceNumber, i.InvoiceNumber)             AS DetailInvoiceNumber
            FROM @Table_Invoices ti
            JOIN Billing.Invoice i           ON ti.InvoiceId = i.Id
            JOIN Contract.CareGroup cg       ON i.CareGroupId = cg.Id
                                            AND cg.CareGroupType = 4
                                            AND cg.EntityType    = 11
            LEFT JOIN Billing.Invoice cc 
                                             ON i.DocumentType = 4
                                            AND cc.DocumentType = 5 AND cc.Status = 1
                                            AND i.ThirdPartyId       = cc.ThirdPartyId
                                            AND i.CareGroupId        = cc.CareGroupId
                                            AND i.InvoiceCategoryId  = cc.InvoiceCategoryId
                                            AND CAST(cc.InvoiceDate AS DATE) BETWEEN i.CapitationInitialDate
                                                                                 AND i.CapitationEndDate
            WHERE ISNULL(@RadicateInvoiceId, 0) = 0
        UNION ALL
            SELECT DISTINCT
                i.Id                                                                   AS InvoiceId,
                i.InvoiceNumber                                                        AS InvoiceNumber,
                IIF(i.DocumentType = 4, cc.Id,            i.Id)                        AS DetailInvoiceId,
                IIF(i.DocumentType = 4, cc.InvoiceNumber, i.InvoiceNumber)             AS DetailInvoiceNumber
            FROM Portfolio.RadicateInvoiceC ri 
            JOIN Portfolio.RadicateInvoiceD rid 
                                             ON ri.Id = rid.RadicateInvoiceCId AND rid.State <> 4
            JOIN Billing.Invoice i 
                                             ON rid.InvoiceNumber = i.InvoiceNumber AND i.Status = 1
            JOIN Contract.CareGroup cg       ON i.CareGroupId = cg.Id
                                            AND cg.CareGroupType = 4
                                            AND cg.EntityType    = 11
            LEFT JOIN @Table_Invoices ti     ON i.Id = ti.InvoiceId
            LEFT JOIN Billing.Invoice cc 
                                             ON i.DocumentType = 4
                                            AND cc.DocumentType = 5 AND cc.Status = 1
                                            AND i.ThirdPartyId       = cc.ThirdPartyId
                                            AND i.CareGroupId        = cc.CareGroupId
                                            AND i.InvoiceCategoryId  = cc.InvoiceCategoryId
                                            AND CAST(cc.InvoiceDate AS DATE) BETWEEN i.CapitationInitialDate
                                                                                 AND i.CapitationEndDate
            WHERE ri.Id = @RadicateInvoiceId
              AND (@FilterByInvoices = 0 OR ti.InvoiceId IS NOT NULL);

        /****************** CONSECUTIVOS DE PROCEDIMIENTOS QUIRÚRGICOS POR FACTURA ****************/

        ;WITH SurgicalProceduresRanked AS (
            SELECT
                inv.InvoiceId                                                          AS InvoiceId,
                sod.Id                                                                 AS SodId,
                DENSE_RANK() OVER (PARTITION BY inv.InvoiceId ORDER BY sod.Id)         AS QxConsecutive
            FROM @Invoices inv
            JOIN Billing.InvoiceDetail id  ON inv.DetailInvoiceId = id.InvoiceId
            JOIN Billing.ServiceOrderDetail sod  ON id.ServiceOrderDetailId = sod.Id
            WHERE sod.Presentation = 2
        )

        /******************************* SELECT FINAL (alias ADRES) ******************************/

        -- Notas:
        --   * NIT_PRESTADOR no se incluye aquí; lo proyecta el servicio aplicativo
        --     leyendo INEMPRESU.INDNUMIDE (mismo enfoque que SP_ADM_ExportarDatosFur).
        --   * Para procedimientos quirúrgicos (rama B), Cantidad_de_servicios se
        --     fuerza a 1 según la spec ADRES: "Procedimientos quirúrgicos: cuando
        --     registre código general de procedimiento quirúrgico y corresponda
        --     a detalles del procedimiento, registrar 1".

        SELECT
            data.InvoiceId,
            data.NUM_FACTURA,
            data.Tipo_de_servicio,
            data.Codigo_del_servicio,
            data.Codigo_general_del_procedimiento_quirurgico,
            data.Consecutivo_procedimiento_quirurgico,
            data.Codificacion_CUPS,
            data.Descripcion_del_servicio_o_elemento_reclamado,
            data.Cantidad_de_servicios,
            data.Valor_unitario_facturado,
            data.Valor_unitario_reclamado,
            data.Valor_total_facturado,
            data.Valor_total_reclamado
        FROM
        (
            /*** RAMA A: servicios CUPS NO quirúrgicos (consultas, procedimientos sin paquete) ***/
            -- Si el servicio está incluido en un paquete quirúrgico (vía
            -- sod.PackageServiceOrderDetailId), hereda el Codigo_general y el
            -- Consecutivo del paquete padre.
            SELECT
                inv.InvoiceId                                                          AS InvoiceId,
                inv.InvoiceNumber                                                      AS NUM_FACTURA,
                CASE ce.RIPSConcept WHEN '14' THEN '5'
                                    WHEN '09' THEN '5'
                                    ELSE '2' END                                       AS Tipo_de_servicio,
                CASE ce.RIPSConcept WHEN '14' THEN NULL
                                    WHEN '09' THEN NULL
                                    ELSE ips.Code END                                  AS Codigo_del_servicio,
                LEFT(ips_pkg.Code, 6)                                                  AS Codigo_general_del_procedimiento_quirurgico,
                CASE WHEN spr_pkg.QxConsecutive IS NOT NULL
                     THEN RIGHT('0' + CAST(spr_pkg.QxConsecutive AS VARCHAR(2)), 2)
                     ELSE NULL END                                                     AS Consecutivo_procedimiento_quirurgico,
                CASE WHEN ce.RIPSConcept NOT IN ('14','09')
                     THEN LEFT(ce.RIPSCode, 6) ELSE NULL END                           AS Codificacion_CUPS,
                LEFT(ips.Name, 200)                                                    AS Descripcion_del_servicio_o_elemento_reclamado,
                CAST(sod.InvoicedQuantity AS DECIMAL(18,2))                            AS Cantidad_de_servicios,
                sod.SubTotalSalesPrice                                                 AS Valor_unitario_facturado,
                sod.SubTotalSalesPrice                                                 AS Valor_unitario_reclamado,
                CAST(sod.InvoicedQuantity AS DECIMAL(18,2)) * sod.SubTotalSalesPrice   AS Valor_total_facturado,
                CAST(sod.InvoicedQuantity AS DECIMAL(18,2)) * sod.SubTotalSalesPrice   AS Valor_total_reclamado,
                sod.Id                                                                 AS SodId,
                1                                                                      AS BranchOrder
            FROM @Invoices inv
            JOIN Billing.InvoiceDetail id         ON inv.DetailInvoiceId = id.InvoiceId
            JOIN Billing.ServiceOrderDetail sod   ON id.ServiceOrderDetailId = sod.Id
            JOIN Contract.CUPSEntity ce           ON sod.CUPSEntityId = ce.Id
            JOIN Contract.IPSService ips          ON sod.IPSServiceId = ips.Id
            LEFT JOIN Billing.ServiceOrderDetail sod_pkg 
                                                                ON sod.PackageServiceOrderDetailId = sod_pkg.Id
                                                               AND sod_pkg.Presentation = 2
            LEFT JOIN Contract.IPSService ips_pkg  ON sod_pkg.IPSServiceId = ips_pkg.Id
            LEFT JOIN SurgicalProceduresRanked spr_pkg          ON spr_pkg.InvoiceId = inv.InvoiceId
                                                               AND spr_pkg.SodId    = sod_pkg.Id
            WHERE sod.SubTotalSalesPrice > 0
              AND ISNULL(sod.Presentation, 0) <> 2

            UNION ALL

            /*** RAMA B: componentes del paquete quirúrgico ***/
            SELECT
                inv.InvoiceId                                                          AS InvoiceId,
                inv.InvoiceNumber                                                      AS NUM_FACTURA,
                '2'                                                                    AS Tipo_de_servicio,
                ips_comp.Code                                                          AS Codigo_del_servicio,
                LEFT(ips_main.Code, 6)                                                 AS Codigo_general_del_procedimiento_quirurgico,
                RIGHT('0' + CAST(spr.QxConsecutive AS VARCHAR(2)), 2)                  AS Consecutivo_procedimiento_quirurgico,
                LEFT(ce.RIPSCode, 6)                                                   AS Codificacion_CUPS,
                LEFT(ips_comp.Name, 200)                                               AS Descripcion_del_servicio_o_elemento_reclamado,
                CAST(1 AS DECIMAL(18,2))                                               AS Cantidad_de_servicios,
                sods.TotalSalesPrice                                                   AS Valor_unitario_facturado,
                sods.TotalSalesPrice                                                   AS Valor_unitario_reclamado,
                sods.TotalSalesPrice                                                   AS Valor_total_facturado,
                sods.TotalSalesPrice                                                   AS Valor_total_reclamado,
                sod.Id                                                                 AS SodId,
                2                                                                      AS BranchOrder
            FROM @Invoices inv
            JOIN Billing.InvoiceDetail id                ON inv.DetailInvoiceId = id.InvoiceId
            JOIN Billing.ServiceOrderDetail sod          ON id.ServiceOrderDetailId = sod.Id
            JOIN Billing.ServiceOrderDetailSurgical sods  ON sod.Id = sods.ServiceOrderDetailId
            JOIN Contract.IPSService ips_comp            ON sods.IPSServiceId = ips_comp.Id
            JOIN Contract.IPSService ips_main            ON sod.IPSServiceId  = ips_main.Id
            LEFT JOIN Contract.CUPSEntity ce             ON sod.CUPSEntityId = ce.Id
            JOIN SurgicalProceduresRanked spr                          ON spr.InvoiceId = inv.InvoiceId
                                                                      AND spr.SodId    = sod.Id
            WHERE sod.Presentation = 2
              AND sods.TotalSalesPrice > 0

            UNION ALL

            /*** RAMA C: productos (medicamentos, dispositivos, osteosíntesis, otros insumos) ***/
            -- Si el producto está incluido en un paquete quirúrgico (vía
            -- sod.PackageServiceOrderDetailId), hereda el Codigo_general y el
            -- Consecutivo del paquete padre. La Cantidad_de_servicios se
            -- conserva con la cantidad real (puede ser > 1 para varios
            -- dispositivos/insumos consumidos en la cirugía).
            SELECT
                inv.InvoiceId                                                          AS InvoiceId,
                inv.InvoiceNumber                                                      AS NUM_FACTURA,
                CAST(
                    IIF(su.MedicalDevice = 1, 6,
                    IIF(su.OsteosynthesisMaterial = 1, 7,
                    CASE pt.Class WHEN 2 THEN 1 ELSE 5 END))
                    AS VARCHAR(1))                                                     AS Tipo_de_servicio,
                IIF(su.MedicalDevice = 1, ip.Code,
                    CASE pt.Class WHEN 2 THEN ip.CodeCUM 
                     WHEN 3 THEN RTRIM(COALESCE(CUPS.RIPSCode,IP.Code))
                    ELSE NULL END)                                                     AS Codigo_del_servicio,
                LEFT(ips_pkg.Code, 6)                                                  AS Codigo_general_del_procedimiento_quirurgico,
                CASE WHEN spr_pkg.QxConsecutive IS NOT NULL
                     THEN RIGHT('0' + CAST(spr_pkg.QxConsecutive AS VARCHAR(2)), 2)
                     ELSE NULL END                                                     AS Consecutivo_procedimiento_quirurgico,
                CAST(NULL AS VARCHAR(6))                                               AS Codificacion_CUPS,
                CASE WHEN pt.Class = 2 THEN NULL
                     ELSE LEFT(IIF(su.OsteosynthesisMaterial = 1,
                                   CONCAT(RTRIM(ip.Description), ' ', RTRIM(Billing.GetSupplierSod(sod.Id))),
                                   ip.Name), 60)
                END                                                                    AS Descripcion_del_servicio_o_elemento_reclamado,
                CAST(sod.InvoicedQuantity AS DECIMAL(18,2))                            AS Cantidad_de_servicios,
                sod.SubTotalSalesPrice                                                 AS Valor_unitario_facturado,
                sod.SubTotalSalesPrice                                                 AS Valor_unitario_reclamado,
                CAST(sod.InvoicedQuantity AS DECIMAL(18,2)) * sod.SubTotalSalesPrice   AS Valor_total_facturado,
                CAST(sod.InvoicedQuantity AS DECIMAL(18,2)) * sod.SubTotalSalesPrice   AS Valor_total_reclamado,
                sod.Id                                                                 AS SodId,
                3                                                                      AS BranchOrder
            FROM @Invoices inv
            JOIN Billing.InvoiceDetail id         ON inv.DetailInvoiceId = id.InvoiceId
            JOIN Billing.ServiceOrderDetail sod   ON id.ServiceOrderDetailId = sod.Id
            JOIN Inventory.InventoryProduct ip    ON sod.ProductId = ip.Id
            JOIN Inventory.ProductType pt         ON ip.ProductTypeId = pt.Id
            LEFT JOIN Inventory.InventorySupplie su  ON su.id = ip.SupplieId
            LEFT JOIN Billing.ServiceOrderDetail sod_pkg 
                                                                ON sod.PackageServiceOrderDetailId = sod_pkg.Id
                                                               AND sod_pkg.Presentation = 2
            LEFT JOIN Contract.IPSService ips_pkg  ON sod_pkg.IPSServiceId = ips_pkg.Id
            LEFT JOIN SurgicalProceduresRanked spr_pkg          ON spr_pkg.InvoiceId = inv.InvoiceId
                                                               AND spr_pkg.SodId    = sod_pkg.Id
            LEFT JOIN Contract.CUPSEntity CUPS ON CUPS.ID = sod.CUPSEntityId
            WHERE sod.SubTotalSalesPrice > 0
        ) AS data
        ORDER BY
            data.InvoiceId,
            CAST(SUBSTRING(data.NUM_FACTURA + '0',
                  PATINDEX('%[0-9]%', data.NUM_FACTURA + '0'),
                  LEN(data.NUM_FACTURA + '0')) AS DECIMAL),
            data.BranchOrder,
            data.SodId,
            data.Codigo_del_servicio;

    END TRY
    BEGIN CATCH
        PRINT ERROR_MESSAGE() + ' Linea: ' + CAST(ERROR_LINE() AS VARCHAR(20));
    END CATCH
END
