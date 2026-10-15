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

	-- Creamos la tabla temporal que devolveremos con los datos
	DECLARE @tableExecution TABLE
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

		INSERT INTO @TableExecution 
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

			--Ventas'VN'
			SELECT	(SELECT TOP 1 CODIPSSEC FROM ADCENATEN),
					RIGHT('0'+CAST(MONTH(ip.InvoiceDate) AS VARCHAR(2)),2),
					'VN' TypeOperation,
					ISNULL(ip.IUM, '') IUM,
					SUM(ip.Quantity) AS 'Cantidad',
					SUM(ip.Subtotalvalue) AS 'Total Ventas',
					MIN(ip.UnitValue) AS 'Valor Minimo', 
					MAX(ip.UnitValue) AS 'Valor Maximo',
					Inventory.TypePersonThirdPartySISMED('VN', YEAR(ip.InvoiceDate), MONTH(ip.InvoiceDate), ip.id, MIN(ip.UnitValue)) TypePersonMin,
					Inventory.NITThirdPartySISMED('VN', YEAR(ip.InvoiceDate), MONTH(ip.InvoiceDate), ip.id, MIN(ip.UnitValue)) NitMin,
					Inventory.InvoiceSISMED(1, YEAR(ip.InvoiceDate), MONTH(ip.InvoiceDate), ip.id, MIN(ip.UnitValue)) AS 'InvoiceSaleMin',  
					Inventory.TypePersonThirdPartySISMED('VN', YEAR(ip.InvoiceDate), MONTH(ip.InvoiceDate), ip.id, MAX(ip.UnitValue)) TypePersonMax,
					Inventory.NITThirdPartySISMED('VN', YEAR(ip.InvoiceDate), MONTH(ip.InvoiceDate), ip.id,MAX(ip.UnitValue)) NitMax,
					Inventory.InvoiceSISMED(1, YEAR(ip.InvoiceDate), MONTH(ip.InvoiceDate), ip.id, MAX(ip.UnitValue)) AS 'InvoiceSaleMax'  
			FROM
			(
					SELECT	pt.Class, 
							ip.Id, 
							ip.IUM, 
							i.InvoiceDate,
							sod.SubTotalSalesPrice UnitValue,
							sod.InvoicedQuantity Quantity,
							sod.GrandTotalSalesPrice Subtotalvalue
					FROM Billing.Invoice i WITH (NOLOCK)    
					JOIN Billing.InvoiceDetail id WITH (NOLOCK) ON i.Id = id.InvoiceId
					JOIN Billing.ServiceOrderDetail sod WITH (NOLOCK) ON id.ServiceOrderDetailId = sod.Id AND sod.GrandTotalSalesPrice > 0
					JOIN Inventory.InventoryProduct ip WITH (NOLOCK) ON sod.ProductId = ip.Id
					JOIN Inventory.InventorySupplie isp WITH(NOLOCK) ON isp.Id =ip.SupplieId
					JOIN Inventory.ProductType pt WITH (NOLOCK) ON pt.Id = ip.ProductTypeId AND pt.Class = 3
					WHERE i.Status = 1 AND i.InvoiceDate BETWEEN @dateStart AND @dateEnd and ip.SismedReport = 1

				UNION ALL

					SELECT	pt.Class, 
							ip.Id,
							ip.IUM, 
							i.InvoiceDate,
							dipsd.SalePrice UnitValue,
							dipsd.Quantity Quantity,
							dipsd.TotalValue Subtotalvalue
					FROM Billing.Invoice i WITH (NOLOCK)
					JOIN Inventory.DocumentInvoiceProductSales dips WITH (NOLOCK) ON i.Id = dips.InvoiceId
					JOIN Inventory.DocumentInvoiceProductSalesDetail dipsd WITH (NOLOCK) ON dips.Id = dipsd.DocumentInvoiceProductSalesId
					JOIN Inventory.InventoryProduct ip WITH (NOLOCK) ON dipsd.ProductId = ip.Id
					JOIN Inventory.InventorySupplie isp WITH(NOLOCK) on isp.Id = ip.SupplieId
					JOIN Inventory.ProductType pt WITH (NOLOCK) ON pt.Id = ip.ProductTypeId AND pt.Class = 3
					WHERE i.Status = 1 AND i.InvoiceDate BETWEEN @dateStart AND @dateEnd AND ip.SismedReport = 1

				UNION ALL

					SELECT	pt.Class, 
							ip.Id,
							ip.IUM, 
							i.InvoiceDate,
							bbd.Price UnitValue,
							bbd.Quantity Quantity,
							bbd.Value Subtotalvalue
					FROM Billing.Invoice i WITH (NOLOCK)
					JOIN Billing.BasicBilling bb WITH(NOLOCK) ON bb.InvoiceId = i.Id
					JOIN Billing.BasicBillingDetail bbd WITH(NOLOCK) on bbd.BasicBillingId = bb.Id
					JOIN Inventory.InventoryProduct ip WITH (NOLOCK) ON bbd.ProductId = ip.Id
					JOIN Inventory.InventorySupplie isp WITH(NOLOCK) on isp.Id = ip.SupplieId
					JOIN Inventory.ProductType pt WITH (NOLOCK) ON pt.Id = ip.ProductTypeId AND pt.Class = 3
					WHERE i.Status = 1 AND i.InvoiceDate BETWEEN @dateStart AND @dateEnd AND ip.SismedReport = 1

			) ip
			GROUP BY YEAR(ip.InvoiceDate), MONTH(ip.InvoiceDate),
				ip.Id, ip.Class, ip.IUM

		UNION ALL
			--Compras'CM'
			SELECT  (SELECT TOP 1 CODIPSSEC FROM ADCENATEN),
					RIGHT('0'+CAST(MONTH(ev.DocumentDate) AS VARCHAR(2)),2),
					'CM' TypeOperation,
					ISNULL(ip.IUM, '') IUM,
					SUM(evd.Quantity) AS 'Cantidad',
					SUM(evd.Subtotalvalue) AS 'Total Ventas',
					MIN(evd.UnitValue) AS 'Valor Minimo',
					MAX(evd.UnitValue) AS 'Valor Maximo',
					Inventory.TypePersonThirdPartySISMED('CM', YEAR(ev.DocumentDate), MONTH(ev.DocumentDate), ip.id, MIN(evd.UnitValue)) TypePersonMin,
					Inventory.NITThirdPartySISMED('CM', YEAR(ev.DocumentDate), MONTH(ev.DocumentDate), ip.id, MIN(evd.UnitValue)) NitMin,
					Inventory.InvoiceSISMED(2, YEAR(ev.DocumentDate), MONTH(ev.DocumentDate), ip.id, MIN(evd.UnitValue)) AS 'InvoiceSaleMin',  
					Inventory.TypePersonThirdPartySISMED('CM', YEAR(ev.DocumentDate), MONTH(ev.DocumentDate), ip.id, MAX(evd.UnitValue)) TypePersonMax,
					Inventory.NITThirdPartySISMED('CM', YEAR(ev.DocumentDate), MONTH(ev.DocumentDate), ip.id,MAX(evd.UnitValue)) NitMax,
					Inventory.InvoiceSISMED(2, YEAR(ev.DocumentDate), MONTH(ev.DocumentDate), ip.id, MAX(evd.UnitValue)) AS 'InvoiceSaleMax'  
			FROM Inventory.EntranceVoucher ev WITH (NOLOCK)
			JOIN Inventory.EntranceVoucherDetail evd WITH (NOLOCK) ON ev.Id = evd.EntranceVoucherId
			JOIN Inventory.InventoryProduct ip WITH (NOLOCK) ON evd.ProductId = ip.Id
			JOIN Inventory.InventorySupplie isp WITH(NOLOCK) on isp.Id = ip.SupplieId
			JOIN Inventory.ProductType pt WITH (NOLOCK) ON pt.Id = ip.ProductTypeId AND pt.Class = 3
			WHERE ev.Status = 2 AND ev.DocumentDate BETWEEN @dateStart and @dateEnd AND ip.SismedReport = 1
			GROUP BY YEAR(ev.DocumentDate), MONTH(ev.DocumentDate),
			ip.id, pt.Class, ip.CodeCUM, ip.IUM
			

	SELECT	ROW_NUMBER() OVER(ORDER BY Month) as Consecutive, 
			*
	FROM @tableExecution
END