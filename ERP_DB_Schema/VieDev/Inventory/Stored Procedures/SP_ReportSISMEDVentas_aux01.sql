
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