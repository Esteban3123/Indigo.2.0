-- =============================================
-- Author:		Andres Alarcon
-- Create date: 21/09/2023
-- Description:	Procedimiento para el reporte de SISMED Circular 015
-- =============================================

CREATE PROCEDURE [Inventory].[SP_ReportSISMEDCircular015]
	@dateStart DATE,
	@dateEnd DATE
AS
BEGIN
	SET NOCOUNT ON

	-- Creamos la tabla temporal que devolveremos con los datos
	DECLARE @tableExecution TABLE
	(
		Habilitacion varchar(20),
		Mes VARCHAR(2), 
		TypeOperation VARCHAR(2), 
		TipoPersonaMin VARCHAR(2),
		NITMin varchar(20),
		TipoPersonaMax VARCHAR(2),
		NITMax varchar(20),
		IUM VARCHAR(20),
		ValorMinimo DECIMAL(18,2), 
		ValorMaximo DECIMAL(18,2), 
		TotalVentas DECIMAL(18,2), 
		Cantidad DECIMAL(18,2), 
		FacturaVentaMin VARCHAR(100), 
		FacturaVentaMax VARCHAR(100)
	)

		INSERT INTO @TableExecution 
			(Habilitacion, Mes, IUM, ValorMinimo, ValorMaximo, TotalVentas, Cantidad, TypeOperation, TipoPersonaMin, NITMin, FacturaVentaMin, TipoPersonaMax, NITMax, FacturaVentaMax)

			--Ventas'VN'
			SELECT	(SELECT TOP 1 CODIPSSEC FROM ADCENATEN),
					RIGHT('0'+CAST(MONTH(ip.InvoiceDate) AS VARCHAR(2)),2),
					ISNULL(ip.IUM, '') IUM,
					MIN(ip.UnitValue) AS 'Valor Minimo', 
					MAX(ip.UnitValue) AS 'Valor Maximo',
					SUM(ip.Subtotalvalue) AS 'Total Ventas',
					SUM(ip.Quantity) AS 'Cantidad',
					'VN' TypeOperation,
					Inventory.TypePersonThirdPartySISMED('VN', YEAR(ip.InvoiceDate), MONTH(ip.InvoiceDate), ip.id, MIN(ip.UnitValue)) TipoPersonaMin,
					Inventory.NITThirdPartySISMED('VN', YEAR(ip.InvoiceDate), MONTH(ip.InvoiceDate), ip.id, MIN(ip.UnitValue)) NITMin,
					Inventory.InvoiceSISMED(1, YEAR(ip.InvoiceDate), MONTH(ip.InvoiceDate), ip.id, MIN(ip.UnitValue)) AS 'Factura de Venta Min',  
					Inventory.TypePersonThirdPartySISMED('VN', YEAR(ip.InvoiceDate), MONTH(ip.InvoiceDate), ip.id, MAX(ip.UnitValue)) TipoPersonaMax,
					Inventory.NITThirdPartySISMED('VN', YEAR(ip.InvoiceDate), MONTH(ip.InvoiceDate), ip.id,MAX(ip.UnitValue)) NITMax,
					Inventory.InvoiceSISMED(1, YEAR(ip.InvoiceDate), MONTH(ip.InvoiceDate), ip.id, MAX(ip.UnitValue)) AS 'Factura de Venta Max'  
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
					JOIN Inventory.ATC atc WITH (NOLOCK) ON ip.ATCId = atc.Id
					JOIN Inventory.ProductType pt WITH (NOLOCK) ON pt.Id = ip.ProductTypeId AND pt.Class = 2
					WHERE i.Status = 1 
						AND i.InvoiceDate BETWEEN @dateStart AND @dateEnd 
						AND ISNULL(atc.ProductNPT, 0) = 0

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
					JOIN Inventory.ATC atc WITH (NOLOCK) ON ip.ATCId = atc.Id
					JOIN Inventory.ProductType pt WITH (NOLOCK) ON pt.Id = ip.ProductTypeId AND pt.Class = 2
					WHERE i.Status = 1 
						AND i.InvoiceDate BETWEEN @dateStart AND @dateEnd 
						AND ISNULL(atc.ProductNPT, 0) = 0

			) ip
			GROUP BY YEAR(ip.InvoiceDate), MONTH(ip.InvoiceDate),
				ip.Id, ip.Class, ip.IUM

		UNION ALL
			--Compras'CM'
			SELECT  (SELECT TOP 1 CODIPSSEC FROM ADCENATEN),
					RIGHT('0'+CAST(MONTH(ev.DocumentDate) AS VARCHAR(2)),2),
					ISNULL(ip.IUM, '') IUM,
					MIN(evd.UnitValue) AS 'Valor Minimo',
					MAX(evd.UnitValue) AS 'Valor Maximo',
					SUM(evd.Subtotalvalue) AS 'Total Ventas',
					SUM(evd.Quantity) AS 'Cantidad',
					'CM' TypeOperation,
					Inventory.TypePersonThirdPartySISMED('CM', YEAR(ev.DocumentDate), MONTH(ev.DocumentDate), ip.id, MIN(evd.UnitValue)) TipoPersonaMin,
					Inventory.NITThirdPartySISMED('CM', YEAR(ev.DocumentDate), MONTH(ev.DocumentDate), ip.id, MIN(evd.UnitValue)) NITMin,
					Inventory.InvoiceSISMED(2, YEAR(ev.DocumentDate), MONTH(ev.DocumentDate), ip.id, MIN(evd.UnitValue)) AS 'Factura de Venta Min',  
					Inventory.TypePersonThirdPartySISMED('CM', YEAR(ev.DocumentDate), MONTH(ev.DocumentDate), ip.id, MAX(evd.UnitValue)) TipoPersonaMax,
					Inventory.NITThirdPartySISMED('CM', YEAR(ev.DocumentDate), MONTH(ev.DocumentDate), ip.id,MAX(evd.UnitValue)) NITMax,
					Inventory.InvoiceSISMED(2, YEAR(ev.DocumentDate), MONTH(ev.DocumentDate), ip.id, MAX(evd.UnitValue)) AS 'Factura de Venta Max'  
			FROM Inventory.EntranceVoucher ev WITH (NOLOCK)
			JOIN Inventory.EntranceVoucherDetail evd WITH (NOLOCK) ON ev.Id = evd.EntranceVoucherId
			JOIN Inventory.InventoryProduct ip WITH (NOLOCK) ON evd.ProductId = ip.Id
			JOIN Inventory.ATC atc WITH (NOLOCK) ON ip.ATCId = atc.Id
			JOIN Inventory.ProductType pt WITH (NOLOCK) ON pt.Id = ip.ProductTypeId AND pt.Class = 2
			WHERE ev.Status = 2 
				AND ev.DocumentDate BETWEEN @dateStart and @dateEnd
				AND ISNULL(atc.ProductNPT, 0) = 0
			GROUP BY YEAR(ev.DocumentDate), MONTH(ev.DocumentDate),
			ip.id, pt.Class, ip.CodeCUM, ip.IUM
			

	SELECT	ROW_NUMBER() OVER(ORDER BY Mes) as Consecutivo, 
			*
	FROM @tableExecution
END