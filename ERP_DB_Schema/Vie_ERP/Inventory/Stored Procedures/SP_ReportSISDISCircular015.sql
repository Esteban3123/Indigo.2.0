-- =============================================
-- Author:		Andres Alarcon
-- Create date: 21/09/2023
-- Description:	Procedimiento para el reporte de SISDIS Circular 015
-- =============================================

CREATE PROCEDURE [Inventory].[SP_ReportSISDISCircular015]
	@dateStart DATE,
	@dateEnd DATE
AS
BEGIN
	SET NOCOUNT ON

	-- Precalcular el Qualification una sola vez
	DECLARE @Qualification VARCHAR(20) = (SELECT TOP 1 LEFT(CODIPSSEC, 10) FROM ADCENATEN)

	-- Tabla temporal para almacenar los resultados
	CREATE TABLE #tableExecution
	(
		Qualification varchar(20),
		Month VARCHAR(2), 
		Channel varchar(3) default 'INS',
		Role varchar(1) default '3',
		TypeOperation VARCHAR(2),
		TransactionType varchar(2) default '03',
		IUM VARCHAR(20),
		UnitType varchar(1) default 'B',
		Quantity DECIMAL(18,2), 
		TotalSales DECIMAL(18,2), 
		MinimumValue DECIMAL(18,2), 
		MaximumValue DECIMAL(18,2), 
		TypePersonMin VARCHAR(2),
		NitMin varchar(20),
		InvoiceSaleMin VARCHAR(100), 
		TypePersonMax VARCHAR(2),
		NitMax varchar(20),
		InvoiceSaleMax VARCHAR(100)
	)

	-- Indice para mejor rendimiento en inserciones y consultas
	CREATE INDEX IX_Temp_Month_IUM ON #tableExecution(Month, IUM)

	-- ===================================================================
	-- SECCIÓN DE VENTAS (VN)
	-- ===================================================================
	;WITH SalesData AS
	(
		-- Unión de las tres fuentes de datos de ventas
		SELECT	
				pt.Class, 
				ip.Id AS ProductId, 
				ip.IUM, 
				i.InvoiceDate,
				sod.SubTotalSalesPrice AS UnitValue,
				sod.InvoicedQuantity AS Quantity,
				sod.GrandTotalSalesPrice AS Subtotalvalue,
				id.InvoiceId
		FROM Billing.Invoice i WITH (NOLOCK)    
		JOIN Billing.InvoiceDetail id WITH (NOLOCK) ON i.Id = id.InvoiceId
		JOIN Billing.ServiceOrderDetail sod WITH (NOLOCK) ON id.ServiceOrderDetailId = sod.Id AND sod.GrandTotalSalesPrice > 0
		JOIN Inventory.InventoryProduct ip WITH (NOLOCK) ON sod.ProductId = ip.Id
		JOIN Inventory.InventorySupplie isp WITH(NOLOCK) ON isp.Id = ip.SupplieId
		JOIN Inventory.ProductType pt WITH (NOLOCK) ON pt.Id = ip.ProductTypeId AND pt.Class = 3
		WHERE i.Status = 1 AND i.InvoiceDate BETWEEN @dateStart AND @dateEnd AND ip.SismedReport = 1

		UNION ALL

		SELECT	
				pt.Class, 
				ip.Id AS ProductId,
				ip.IUM, 
				i.InvoiceDate,
				dipsd.SalePrice AS UnitValue,
				dipsd.Quantity AS Quantity,
				dipsd.TotalValue AS Subtotalvalue,
				dips.InvoiceId
		FROM Billing.Invoice i WITH (NOLOCK)
		JOIN Inventory.DocumentInvoiceProductSales dips WITH (NOLOCK) ON i.Id = dips.InvoiceId
		JOIN Inventory.DocumentInvoiceProductSalesDetail dipsd WITH (NOLOCK) ON dips.Id = dipsd.DocumentInvoiceProductSalesId
		JOIN Inventory.InventoryProduct ip WITH (NOLOCK) ON dipsd.ProductId = ip.Id
		JOIN Inventory.InventorySupplie isp WITH(NOLOCK) ON isp.Id = ip.SupplieId
		JOIN Inventory.ProductType pt WITH (NOLOCK) ON pt.Id = ip.ProductTypeId AND pt.Class = 3
		WHERE i.Status = 1 AND i.InvoiceDate BETWEEN @dateStart AND @dateEnd AND ip.SismedReport = 1

		UNION ALL

		SELECT	
				pt.Class, 
				ip.Id AS ProductId,
				ip.IUM, 
				i.InvoiceDate,
				bbd.Price AS UnitValue,
				bbd.Quantity AS Quantity,
				bbd.Value AS Subtotalvalue,
				bb.InvoiceId
		FROM Billing.Invoice i WITH (NOLOCK)
		JOIN Billing.BasicBilling bb WITH(NOLOCK) ON bb.InvoiceId = i.Id
		JOIN Billing.BasicBillingDetail bbd WITH(NOLOCK) ON bbd.BasicBillingId = bb.Id
		JOIN Inventory.InventoryProduct ip WITH (NOLOCK) ON bbd.ProductId = ip.Id
		JOIN Inventory.InventorySupplie isp WITH(NOLOCK) ON isp.Id = ip.SupplieId
		JOIN Inventory.ProductType pt WITH (NOLOCK) ON pt.Id = ip.ProductTypeId AND pt.Class = 3
		WHERE i.Status = 1 AND i.InvoiceDate BETWEEN @dateStart AND @dateEnd AND ip.SismedReport = 1
	),
	AggregatedSales AS
	(
		-- Agregación de datos de ventas
		SELECT	
				YEAR(InvoiceDate) AS Year,
				MONTH(InvoiceDate) AS Month,
				ProductId, 
				Class, 
				IUM,
				SUM(Quantity) AS TotalQuantity,
				SUM(Subtotalvalue) AS TotalSales,
				MIN(UnitValue) AS MinValue, 
				MAX(UnitValue) AS MaxValue
		FROM SalesData
		GROUP BY YEAR(InvoiceDate), MONTH(InvoiceDate), ProductId, Class, IUM
	)
	INSERT INTO #tableExecution 
		(Qualification, 
		Month,
		TypeOperation, 
		IUM, 
		Quantity, 
		TotalSales, 
		MinimumValue, 
		MaximumValue, 
		TypePersonMin, 
		NitMin, 
		InvoiceSaleMin, 
		TypePersonMax, 
		NitMax, 
		InvoiceSaleMax)
	SELECT	
			@Qualification,
			RIGHT('0' + CAST(agg.Month AS VARCHAR(2)), 2),
			'VN' AS TypeOperation,
			ISNULL(agg.IUM, '') AS IUM,
			agg.TotalQuantity,
			agg.TotalSales,
			agg.MinValue, 
			agg.MaxValue,
			-- Información para valor mínimo
			minData.TypePerson AS TypePersonMin,
			minData.Nit AS NitMin,
			minData.InvoiceNumber AS InvoiceSaleMin,
			-- Información para valor máximo
			maxData.TypePerson AS TypePersonMax,
			maxData.Nit AS NitMax,
			maxData.InvoiceNumber AS InvoiceSaleMax
	FROM AggregatedSales agg
	-- OUTER APPLY para obtener datos del valor mínimo
	OUTER APPLY (
		SELECT TOP 1 
			IIF(t.PersonType = 1, 'PN','NI') AS TypePerson,
			t.Nit,
			i.InvoiceNumber
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
			UNION ALL
			SELECT bb.InvoiceId
			FROM Billing.BasicBilling bb WITH (NOLOCK)
			JOIN Billing.BasicBillingDetail bbd WITH (NOLOCK) ON bb.Id = bbd.BasicBillingId
			WHERE bbd.ProductId = agg.ProductId AND bbd.Price = agg.MinValue
		) inv ON i.Id = inv.InvoiceId
		JOIN Common.ThirdParty t WITH(NOLOCK) ON t.Id = i.ThirdPartyId
		WHERE i.Status = 1 AND YEAR(i.InvoiceDate) = agg.Year AND MONTH(i.InvoiceDate) = agg.Month
	) minData
	-- OUTER APPLY para obtener datos del valor máximo
	OUTER APPLY (
		SELECT TOP 1 
			IIF(t.PersonType = 1, 'PN','NI') AS TypePerson,
			t.Nit,
			i.InvoiceNumber
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
			UNION ALL
			SELECT bb.InvoiceId
			FROM Billing.BasicBilling bb WITH (NOLOCK)
			JOIN Billing.BasicBillingDetail bbd WITH (NOLOCK) ON bb.Id = bbd.BasicBillingId
			WHERE bbd.ProductId = agg.ProductId AND bbd.Price = agg.MaxValue
		) inv ON i.Id = inv.InvoiceId
		JOIN Common.ThirdParty t WITH(NOLOCK) ON t.Id = i.ThirdPartyId
		WHERE i.Status = 1 AND YEAR(i.InvoiceDate) = agg.Year AND MONTH(i.InvoiceDate) = agg.Month
	) maxData

	-- ===================================================================
	-- SECCIÓN DE COMPRAS (CM)
	-- ===================================================================
	;WITH PurchaseData AS
	(
		-- Agregación de datos de compras
		SELECT 
			YEAR(ev.DocumentDate) AS Year,
			MONTH(ev.DocumentDate) AS Month,
			ip.Id AS ProductId,
			pt.Class,
			ip.CodeCUM,
			ip.IUM,
			SUM(evd.Quantity) AS TotalQuantity,
			SUM(evd.Subtotalvalue) AS TotalPurchases,
			MIN(evd.UnitValue) AS MinValue,
			MAX(evd.UnitValue) AS MaxValue
		FROM Inventory.EntranceVoucher ev WITH (NOLOCK)
		JOIN Inventory.EntranceVoucherDetail evd WITH (NOLOCK) ON ev.Id = evd.EntranceVoucherId
		JOIN Inventory.InventoryProduct ip WITH (NOLOCK) ON evd.ProductId = ip.Id
		JOIN Inventory.InventorySupplie isp WITH(NOLOCK) ON isp.Id = ip.SupplieId
		JOIN Inventory.ProductType pt WITH (NOLOCK) ON pt.Id = ip.ProductTypeId AND pt.Class = 3
		WHERE ev.Status = 2 AND ev.DocumentDate BETWEEN @dateStart AND @dateEnd AND ip.SismedReport = 1
		GROUP BY YEAR(ev.DocumentDate), MONTH(ev.DocumentDate), ip.Id, pt.Class, ip.CodeCUM, ip.IUM
	)
	INSERT INTO #tableExecution 
		(Qualification, 
		Month,
		TypeOperation, 
		IUM, 
		Quantity, 
		TotalSales, 
		MinimumValue, 
		MaximumValue, 
		TypePersonMin, 
		NitMin, 
		InvoiceSaleMin, 
		TypePersonMax, 
		NitMax, 
		InvoiceSaleMax)
	SELECT  
			@Qualification,
			RIGHT('0' + CAST(pd.Month AS VARCHAR(2)), 2),
			'CM' AS TypeOperation,
			ISNULL(pd.IUM, '') AS IUM,
			pd.TotalQuantity,
			pd.TotalPurchases,
			pd.MinValue,
			pd.MaxValue,
			-- Información para valor mínimo
			minData.TypePerson AS TypePersonMin,
			minData.Nit AS NitMin,
			minData.InvoiceNumber AS InvoiceSaleMin,
			-- Información para valor máximo
			maxData.TypePerson AS TypePersonMax,
			maxData.Nit AS NitMax,
			maxData.InvoiceNumber AS InvoiceSaleMax
	FROM PurchaseData pd
	-- OUTER APPLY para obtener datos del valor mínimo
	OUTER APPLY (
		SELECT TOP 1 
			IIF(t.PersonType = 1, 'PN','NI') AS TypePerson,
			t.Nit,
			ev.InvoiceNumber
		FROM Inventory.EntranceVoucher ev WITH (NOLOCK)
		JOIN Inventory.EntranceVoucherDetail evd WITH (NOLOCK) ON ev.Id = evd.EntranceVoucherId
		JOIN Common.Supplier s WITH(NOLOCK) ON s.Id = ev.SupplierId
		JOIN Common.ThirdParty t WITH(NOLOCK) ON t.Id = s.IdThirdParty
		WHERE ev.Status = 2 
			AND YEAR(ev.DocumentDate) = pd.Year
			AND MONTH(ev.DocumentDate) = pd.Month
			AND evd.ProductId = pd.ProductId
			AND evd.UnitValue = pd.MinValue
	) minData
	-- OUTER APPLY para obtener datos del valor máximo
	OUTER APPLY (
		SELECT TOP 1 
			IIF(t.PersonType = 1, 'PN','NI') AS TypePerson,
			t.Nit,
			ev.InvoiceNumber
		FROM Inventory.EntranceVoucher ev WITH (NOLOCK)
		JOIN Inventory.EntranceVoucherDetail evd WITH (NOLOCK) ON ev.Id = evd.EntranceVoucherId
		JOIN Common.Supplier s WITH(NOLOCK) ON s.Id = ev.SupplierId
		JOIN Common.ThirdParty t WITH(NOLOCK) ON t.Id = s.IdThirdParty
		WHERE ev.Status = 2 
			AND YEAR(ev.DocumentDate) = pd.Year
			AND MONTH(ev.DocumentDate) = pd.Month
			AND evd.ProductId = pd.ProductId
			AND evd.UnitValue = pd.MaxValue
	) maxData

	-- Resultado final
	SELECT	
		ROW_NUMBER() OVER(ORDER BY Month) AS Consecutive, 
		Qualification,
		Month, 
		Channel,
		Role,
		TypeOperation,
		TransactionType,
		IUM,
		UnitType,
		Quantity, 
		TotalSales, 
		MinimumValue, 
		MaximumValue, 
		TypePersonMin,
		NitMin,
		InvoiceSaleMin, 
		TypePersonMax,
		NitMax,
		InvoiceSaleMax
	FROM #tableExecution

	-- Limpiar tabla temporal
	DROP TABLE #tableExecution
END