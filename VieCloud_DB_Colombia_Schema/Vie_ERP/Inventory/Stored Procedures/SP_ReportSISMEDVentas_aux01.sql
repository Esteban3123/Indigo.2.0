
--exec [Inventory].[SP_ReportSISMEDVentas_aux01] '01/04/2018 00:00:00','30/06/2018 23:59:59'
-- =============================================
-- Author:		Juan Bermudez
-- Create date: 15/04/2016
-- Description:	Procedimiento para el reporte de SISMED de ventas
-- =============================================
CREATE PROCEDURE [Inventory].[SP_ReportSISMEDVentas_aux01] @dateStart AS DATETIME, 
                                                          @dateEnd AS   DATETIME
AS
    BEGIN

        -- Creamos la tabla temporal que devolveremos con los datos
        DECLARE @tableExecution TABLE
        (tipo            INT, 
         Mes             VARCHAR(2), 
         canal           VARCHAR(5), 
         CUM             VARCHAR(20), 
         Comercializa    VARCHAR(2), 
         ValorMinimo     DECIMAL(18, 2), 
         ValorMaximo     DECIMAL(18, 2), 
         TotalVentas     DECIMAL(18, 2), 
         Cantidad        DECIMAL(18, 2), 
         FacturaVentaMin VARCHAR(100), 
         FacturaVentaMax VARCHAR(100)
        );
        DECLARE @dateTemp AS DATETIME;
        SET @dateTemp = @dateStart;
        PRINT '@dateTemp: ' + CAST(@dateTemp AS VARCHAR(50));
        WHILE @dateTemp <= @dateEnd
            BEGIN
                DECLARE @dateTempEnd AS DATETIME;
                SET @dateTempEnd = DATEADD(day, -1, DATEADD(MONTH, 1, @dateTemp));
                PRINT '@dateTempEnd: ' + CAST(@dateTempEnd AS VARCHAR(50));
                INSERT INTO @TableExecution
                (tipo, 
                 Mes, 
                 canal, 
                 CUM, 
                 Comercializa, 
                 ValorMinimo, 
                 ValorMaximo, 
                 TotalVentas, 
                 Cantidad, 
                 FacturaVentaMin, 
                 FacturaVentaMax
                )
                       SELECT 2 AS Tipo, 
                              RIGHT('0' + CAST(MONTH(@dateTemp) AS VARCHAR(2)), 2) AS Mes, 
                              'INS' AS Canal, 
                              ip.CodeCUM AS CUM, 
                              'SI' AS Comercializa, 
                              MIN(sod.SubTotalSalesPrice) AS 'Valor Minimo', 
                              MAX(sod.SubTotalSalesPrice) AS 'Valor Maximo', 
                              SUM(sod.grandTotalSalesPrice) AS 'Total Ventas', 
                              SUM(sod.invoicedquantity) AS 'Cantidad', 
                              inventory.InvoiceSISMED(sod.ProductId, MIN(sod.SubTotalSalesPrice), MONTH(@dateTemp), 1, 0) AS 'Factura de Venta Min', 
                              inventory.InvoiceSISMED(sod.ProductId, MAX(sod.SubTotalSalesPrice), MONTH(@dateTemp), 1, 0) AS 'Factura de Venta Max'
                       FROM Billing.ServiceOrderDetail AS sod WITH(NOLOCK)
                            INNER JOIN Billing.InvoiceDetail AS id WITH(NOLOCK) ON id.ServiceOrderDetailId = sod.Id
                                                                                   AND sod.GrandTotalSalesPrice > 0
                            INNER JOIN Billing.Invoice AS i WITH(NOLOCK) ON id.InvoiceId = i.Id
                                                                            AND i.InvoiceDate BETWEEN @dateTemp AND @dateTempEnd
                                                                            AND i.[Status] = 1
                            INNER JOIN Inventory.InventoryProduct AS ip WITH(NOLOCK) ON sod.ProductId = ip.Id
                                                                                        AND ip.ATCId IS NOT NULL
                            INNER JOIN Inventory.ProductType AS pt WITH(NOLOCK) ON pt.Id = ip.ProductTypeId
                                                                                   AND pt.Class = 2
                       GROUP BY ip.CodeCUM, 
                                sod.ProductId
                       UNION ALL
                       SELECT 2 AS Tipo, 
                              RIGHT('0' + CAST(MONTH(@dateTemp) AS VARCHAR(2)), 2) AS Mes, 
                              'INS' AS Canal, 
                              ip.CodeCUM AS CUM, 
                              'SI' AS Comercializa, 
                              MIN(dipsd.SubTotalValue) AS 'Valor Minimo', 
                              MAX(dipsd.SubTotalValue) AS 'Valor Maximo', 
                              SUM(dipsd.TotalValue) AS 'Total Ventas', 
                              SUM(dipsd.Quantity) AS 'Cantidad', 
                              inventory.InvoiceSISMEDProductSales(dipsd.ProductId, MIN(dipsd.SubTotalValue), MONTH(@dateTemp)) AS 'Factura de Venta Min', 
                              inventory.InvoiceSISMEDProductSales(dipsd.ProductId, MAX(dipsd.SubTotalValue), MONTH(@dateTemp)) AS 'Factura de Venta Max'
                       FROM Inventory.DocumentInvoiceProductSalesDetail dipsd WITH(NOLOCK)
                            INNER JOIN Inventory.DocumentInvoiceProductSales dips WITH(NOLOCK) ON dips.Id = dipsd.DocumentInvoiceProductSalesId
                            INNER JOIN Billing.Invoice i WITH(NOLOCK) ON i.Id = dips.InvoiceId
                                                                         AND i.InvoiceDate BETWEEN @dateTemp AND @dateTempEnd
                                                                         AND i.[Status] = 1
                            INNER JOIN Inventory.InventoryProduct ip WITH(NOLOCK) ON ip.Id = dipsd.ProductId
                                                                                     AND ip.ATCId IS NOT NULL
                            INNER JOIN Inventory.ProductType pt WITH(NOLOCK) ON pt.Id = ip.ProductTypeId
                                                                                AND pt.Class = 2
                       GROUP BY ip.CodeCUM, 
                                dipsd.ProductId;
                SET @dateTemp = DATEADD(MONTH, 1, @dateTemp);
            END;
        SELECT tipo, 
               ROW_NUMBER() OVER(
               ORDER BY Mes, 
                        CUM) AS consecutivo, 
               Mes, 
               canal, 
               CUM, 
               Comercializa, 
               ValorMinimo, 
               ValorMaximo, 
               TotalVentas, 
               Cantidad, 
               FacturaVentaMin, 
               FacturaVentaMax
        FROM @tableExecution;
        WITH cte
             AS (SELECT *, 
                        ROW_NUMBER() OVER(PARTITION BY Mes, 
                                                       CUM
                        ORDER BY Mes) AS [rn]
                 FROM @tableExecution)
             SELECT *
             FROM cte;
        --SELECT tipo, mes, canal, CUM, comercializa, (SELECT MIN(aux.ValorMinimo) from cte aux where aux.CUM = tt.CUM and aux.Mes = tt.Mes) as ValorMinimo,
        --(SELECT MAX(aux.ValorMaximo) from cte aux where aux.CUM = tt.CUM and aux.Mes = tt.Mes) as ValorMaximo, 
        --(SELECT SUM(aux.TotalVentas) from cte aux where aux.CUM = tt.CUM and aux.Mes = tt.Mes) as TotalVentas,
        --(SELECT SUM(aux.Cantidad) from cte aux where aux.CUM = tt.CUM and aux.Mes = tt.Mes) as Cantidad, 
        --(SELECT aux.FacturaVentaMin from cte aux where aux.CUM = tt.CUM and aux.Mes = tt.Mes AND aux.ValorMinimo = (SELECT MIN(aux.ValorMinimo) from cte aux where aux.CUM = tt.CUM and aux.Mes = tt.Mes)) AS FacturaVentaMin,
        --(SELECT aux.FacturaVentaMax from cte aux where aux.CUM = tt.CUM and aux.Mes = tt.Mes AND aux.ValorMaximo = (SELECT MAX(aux.ValorMaximo) from cte aux where aux.CUM = tt.CUM and aux.Mes = tt.Mes)) AS FacturaVentaMax
        -- from cte tt where [rn] = 2
        --
        -- UNION
        --
        --SELECT tipo, mes, canal, CUM, comercializa, (SELECT MIN(aux.ValorMinimo) from cte aux where aux.CUM = tt.CUM and aux.Mes = tt.Mes) as ValorMinimo,
        --(SELECT MAX(aux.ValorMaximo) from cte aux where aux.CUM = tt.CUM and aux.Mes = tt.Mes) as ValorMaximo, 
        --(SELECT SUM(aux.TotalVentas) from cte aux where aux.CUM = tt.CUM and aux.Mes = tt.Mes) as TotalVentas,
        --(SELECT SUM(aux.Cantidad) from cte aux where aux.CUM = tt.CUM and aux.Mes = tt.Mes) as Cantidad,
        --(SELECT aux.FacturaVentaMin from cte aux where aux.CUM = tt.CUM and aux.Mes = tt.Mes AND aux.ValorMinimo = (SELECT MIN(aux.ValorMinimo) from cte aux where aux.CUM = tt.CUM and aux.Mes = tt.Mes)) AS FacturaVentaMin,
        --(SELECT aux.FacturaVentaMax from cte aux where aux.CUM = tt.CUM and aux.Mes = tt.Mes AND aux.ValorMaximo = (SELECT MAX(aux.ValorMaximo) from cte aux where aux.CUM = tt.CUM and aux.Mes = tt.Mes)) AS FacturaVentaMax
        --from cte tt where [rn] = 1

    END;
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Procedimiento auxiliar para el reporte SISMED de ventas de medicamentos e insumos. Consolida mes a mes, dentro del rango de fechas indicado, las ventas facturadas: recorre cada mes del período, extrae de las facturas activas (estado 1) los productos con código CUM y clasificación ATC que pertenecen a la clase de producto 2 (medicamentos/insumos sujetos a reporte SISMED), y calcula por producto el valor mínimo, valor máximo, total de ventas y cantidad vendida, identificando además el número de factura asociada a cada extremo de precio mediante las funciones InvoiceSISMED e InvoiceSISMEDProductSales. Integra dos fuentes de venta: las líneas de órdenes de servicio facturadas (ServiceOrderDetail → InvoiceDetail → Invoice) y las ventas directas de productos de inventario (DocumentInvoiceProductSalesDetail → DocumentInvoiceProductSales → Invoice). El resultado final agrupa por CUM y mes, acumulando los totales de todo el período, para ser utilizado en la generación del reporte oficial de precios y comercialización de medicamentos exigido por SISMED (Sistema de Información de Precios de Medicamentos).', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'PROCEDURE', @level1name = N'SP_ReportSISMEDVentas_aux01';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'PROCEDURE', @level1name = N'SP_ReportSISMEDVentas_aux01';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Genera el insumo mensual del reporte SISMED de ventas, agregando por CUM y producto los valores mínimo, máximo, total vendido y cantidades a partir de facturas de servicios y de ventas de inventario dentro del rango de fechas.', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_ReportSISMEDVentas_aux01';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'@dateStart y @dateEnd deben estar definidos y @dateStart <= @dateEnd para que el ciclo mensual ejecute al menos una iteración; Los productos deben tener ATCId no nulo y pertenecer a un ProductType con Class = 2 (medicamentos) para ser incluidos; Las facturas deben tener Status = 1 (activas/vigentes) para ser consideradas; En la rama de ServiceOrderDetail, sólo se consideran ítems con GrandTotalSalesPrice > 0', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_ReportSISMEDVentas_aux01';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Todos los registros insertados llevan Tipo=2, Canal=''INS'' y Comercializa=''SI''; El campo Mes siempre se almacena con dos dígitos (relleno con cero a la izquierda); Solo se reportan productos clasificados como medicamentos (ProductType.Class=2) y con ATCId; Solo se consideran facturas activas (Status=1) cuya fecha cae dentro del mes iterado; Las facturas de venta provenientes de servicios sólo se incluyen si el ítem tiene GrandTotalSalesPrice > 0; Las agregaciones (MIN/MAX/SUM) se calculan por combinación CodeCUM + ProductId dentro de cada mes; Los números de factura mínima y máxima se resuelven mediante funciones escalares según el valor mínimo/máximo del producto en el mes', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_ReportSISMEDVentas_aux01';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Reporte SISMED; CUM (Código Único de Medicamento); ATC (clasificación anatómica terapéutica); Factura de venta; Medicamentos; Canal institucional (INS); Ventas de inventario/farmacia', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_ReportSISMEDVentas_aux01';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[INSERT] @tableExecution: Por cada mes del rango, inserta agregados (MIN/MAX/SUM) por CodeCUM y ProductId provenientes de Billing.ServiceOrderDetail unidos con InvoiceDetail e Invoice cuando InvoiceDate está en el mes, Status=1, GrandTotalSalesPrice>0, ATCId no nulo y ProductType.Class=2; [INSERT] @tableExecution: Por cada mes del rango, inserta agregados por CodeCUM y ProductId desde Inventory.DocumentInvoiceProductSalesDetail unidos con DocumentInvoiceProductSales e Invoice cuando InvoiceDate está en el mes, Status=1, ATCId no nulo y ProductType.Class=2; [RETURN_RESULT] @tableExecution: Devuelve un primer result set con todas las filas acumuladas numeradas con ROW_NUMBER ordenado por Mes y CUM; [RETURN_RESULT] @tableExecution: Devuelve un segundo result set con las mismas filas más un rn = ROW_NUMBER particionado por (Mes, CUM)', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_ReportSISMEDVentas_aux01';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si WHILE @dateTemp <= @dateEnd → Itera mes a mes calculando @dateTempEnd como último día del mes (DATEADD(day,-1,DATEADD(MONTH,1,@dateTemp))) y ejecuta los dos INSERT del mes else Termina el ciclo y emite los result sets finales; si ProductType.Class = 2 e InventoryProduct.ATCId IS NOT NULL → El producto se considera medicamento con código ATC y entra al reporte SISMED else Se excluye del reporte; si Invoice.Status = 1 e InvoiceDate dentro del mes en curso → La factura aporta sus ítems al agregado mensual else La factura es ignorada', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_ReportSISMEDVentas_aux01';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'Inventory.InvoiceSISMED; Inventory.InvoiceSISMEDProductSales', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_ReportSISMEDVentas_aux01';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Billing.ServiceOrderDetail; Billing.InvoiceDetail; Billing.Invoice; Inventory.InventoryProduct; Inventory.ProductType; Inventory.DocumentInvoiceProductSalesDetail; Inventory.DocumentInvoiceProductSales', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_ReportSISMEDVentas_aux01';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_ReportSISMEDVentas_aux01';
-- GO
