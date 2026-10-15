-- =============================================
-- Author:		Juan Pablo Castillo
-- Create date: 15/04/2016
-- Description:	Procedimiento para el reporte de SISMED de ventas
-- =============================================
CREATE PROCEDURE [Inventory].[SP_ReportSISMEDVentas]
	@dateStart DATE,
	@dateEnd DATE
AS
BEGIN
	SET NOCOUNT ON

	-- Tabla temporal para almacenar los resultados
	CREATE TABLE #tableExecution
	(
		Mes VARCHAR(2), 
		Tipo INT, 		
		CUM VARCHAR(20),
		IUM VARCHAR(20),
		Canal VARCHAR(20),
		Comercializa VARCHAR(20),
		ValorMinimo DECIMAL(18,2), 
		ValorMaximo DECIMAL(18,2), 
		TotalVentas DECIMAL(18,2), 
		Cantidad DECIMAL(18,2), 
		FacturaVentaMin VARCHAR(100), 
		FacturaVentaMax VARCHAR(100)
	)

	-- Indice para mejor rendimiento en inserciones y consultas
	CREATE INDEX IX_Temp_Mes_CUM ON #tableExecution(Mes, CUM)

	-- ===================================================================
	-- Obtener datos de ventas
	-- ===================================================================
	;WITH SalesData AS
	(
		-- ServiceOrderDetail
		SELECT	
				pt.Class, 
				ip.Id AS ProductId, 
				ip.CodeCUM, 
				ip.IUM, 
				i.InvoiceDate,
				sod.SubTotalSalesPrice AS UnitValue,
				sod.InvoicedQuantity AS Quantity,
				sod.GrandTotalSalesPrice AS Subtotalvalue
		FROM Billing.Invoice i WITH (NOLOCK)    
		JOIN Billing.InvoiceDetail id WITH (NOLOCK) ON i.Id = id.InvoiceId
		JOIN Billing.ServiceOrderDetail sod WITH (NOLOCK) ON id.ServiceOrderDetailId = sod.Id AND sod.GrandTotalSalesPrice > 0
		JOIN Inventory.InventoryProduct ip WITH (NOLOCK) ON sod.ProductId = ip.Id
		JOIN Inventory.ATC atc WITH (NOLOCK) ON ip.ATCId = atc.Id
		JOIN Inventory.ProductType pt WITH (NOLOCK) ON pt.Id = ip.ProductTypeId AND pt.Class = 2
		WHERE i.Status = 1  AND i.InvoiceDate BETWEEN @dateStart AND @dateEnd AND ISNULL(atc.ProductNPT, 0) = 0
		UNION ALL
		-- DocumentInvoiceProductSalesDetail
		SELECT	
				pt.Class, 
				ip.Id AS ProductId, 
				ip.CodeCUM, 
				ip.IUM, 
				i.InvoiceDate,
				dipsd.SalePrice AS UnitValue,
				dipsd.Quantity AS Quantity,
				dipsd.TotalValue AS Subtotalvalue
		FROM Billing.Invoice i WITH (NOLOCK)
		JOIN Inventory.DocumentInvoiceProductSales dips WITH (NOLOCK) ON i.Id = dips.InvoiceId
		JOIN Inventory.DocumentInvoiceProductSalesDetail dipsd WITH (NOLOCK) ON dips.Id = dipsd.DocumentInvoiceProductSalesId
		JOIN Inventory.InventoryProduct ip WITH (NOLOCK) ON dipsd.ProductId = ip.Id
		JOIN Inventory.ATC atc WITH (NOLOCK) ON ip.ATCId = atc.Id
		JOIN Inventory.ProductType pt WITH (NOLOCK) ON pt.Id = ip.ProductTypeId AND pt.Class = 2
		WHERE i.Status = 1 AND i.InvoiceDate BETWEEN @dateStart AND @dateEnd AND ISNULL(atc.ProductNPT, 0) = 0
	),
	AggregatedSales AS
	(
		-- Agregación de datos de ventas
		SELECT	
				YEAR(InvoiceDate) AS Year,
				MONTH(InvoiceDate) AS Month,
				ProductId,
				Class,
				CodeCUM,
				IUM,
				MIN(UnitValue) AS MinValue, 
				MAX(UnitValue) AS MaxValue,
				SUM(Subtotalvalue) AS TotalSales,
				SUM(Quantity) AS TotalQuantity
		FROM SalesData
		GROUP BY YEAR(InvoiceDate), MONTH(InvoiceDate), ProductId, Class, CodeCUM, IUM
	)
	INSERT INTO #tableExecution 
		(Mes, Tipo, CUM, IUM, Canal, Comercializa, ValorMinimo, ValorMaximo, TotalVentas, Cantidad, FacturaVentaMin, FacturaVentaMax)
	SELECT	
			RIGHT('0' + CAST(agg.Month AS VARCHAR(2)), 2) AS Mes,
			agg.Class AS Tipo,
			ISNULL(agg.CodeCUM, '') AS CUM,
			ISNULL(agg.IUM, '') AS IUM,
			'INS' AS Canal,
			'SI' AS Comercializa,
			agg.MinValue AS ValorMinimo, 
			agg.MaxValue AS ValorMaximo,
			agg.TotalSales AS TotalVentas,
			agg.TotalQuantity AS Cantidad,
			-- Obtener factura con valor mínimo
			minInvoice.InvoiceNumber AS FacturaVentaMin,
			-- Obtener factura con valor máximo
			maxInvoice.InvoiceNumber AS FacturaVentaMax
	FROM AggregatedSales agg
	-- OUTER APPLY para obtener la factura con el valor mínimo
	OUTER APPLY (
		SELECT TOP 1 i.InvoiceNumber
		FROM Billing.Invoice i WITH (NOLOCK) 
		JOIN
		(
			SELECT id.InvoiceId
			FROM Billing.InvoiceDetail id WITH (NOLOCK)
			JOIN Billing.ServiceOrderDetail sod WITH (NOLOCK) ON id.ServiceOrderDetailId = sod.Id
			WHERE sod.ProductId = agg.ProductId AND sod.SubTotalSalesPrice = agg.MinValue 
			UNION ALL
			SELECT dips.InvoiceId
			FROM Inventory.DocumentInvoiceProductSales dips WITH (NOLOCK)
			JOIN Inventory.DocumentInvoiceProductSalesDetail dipsd WITH (NOLOCK) ON dips.Id = dipsd.DocumentInvoiceProductSalesId
			WHERE dipsd.ProductId = agg.ProductId AND dipsd.SalePrice = agg.MinValue
		) inv ON i.Id = inv.InvoiceId
		WHERE i.Status = 1 AND YEAR(i.InvoiceDate) = agg.Year AND MONTH(i.InvoiceDate) = agg.Month
	) minInvoice
	-- OUTER APPLY para obtener la factura con el valor máximo
	OUTER APPLY (
		SELECT TOP 1 i.InvoiceNumber
		FROM Billing.Invoice i WITH (NOLOCK) 
		JOIN
		(
			SELECT id.InvoiceId
			FROM Billing.InvoiceDetail id WITH (NOLOCK)
			JOIN Billing.ServiceOrderDetail sod WITH (NOLOCK) ON id.ServiceOrderDetailId = sod.Id
			WHERE sod.ProductId = agg.ProductId AND sod.SubTotalSalesPrice = agg.MaxValue 
			UNION ALL
			SELECT dips.InvoiceId
			FROM Inventory.DocumentInvoiceProductSales dips WITH (NOLOCK)
			JOIN Inventory.DocumentInvoiceProductSalesDetail dipsd WITH (NOLOCK) ON dips.Id = dipsd.DocumentInvoiceProductSalesId
			WHERE dipsd.ProductId = agg.ProductId AND dipsd.SalePrice = agg.MaxValue
		) inv ON i.Id = inv.InvoiceId
		WHERE i.Status = 1  AND YEAR(i.InvoiceDate) = agg.Year AND MONTH(i.InvoiceDate) = agg.Month
	) maxInvoice

	-- Resultado final
	SELECT	
		ROW_NUMBER() OVER(ORDER BY Mes, CUM) AS Consecutivo, 
		Mes,
		Tipo,
		CUM,
		IUM,
		Canal,
		Comercializa,
		ValorMinimo,
		ValorMaximo,
		TotalVentas,
		Cantidad,
		FacturaVentaMin,
		FacturaVentaMax
	FROM #tableExecution

	-- Limpiar tabla temporal
	DROP TABLE #tableExecution
END