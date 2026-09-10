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
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Procedimiento almacenado que genera el reporte SISDIS requerido por la Circular 015, destinado al control y vigilancia de precios de medicamentos e insumos ante el regulador. Para un rango de fechas dado, consolida las ventas de productos farmacéuticos (clasificados como tipo de producto clase 3 con reporte SISMED activo) provenientes de tres fuentes de facturación: detalle de facturas ordinarias, ventas directas de productos y facturación básica. Por cada mes y código IUM (Identificación Única de Medicamentos), calcula la cantidad total vendida, el valor total de ventas, el precio unitario mínimo y máximo, e identifica la factura y el tercero pagador (persona natural o jurídica con su NIT) asociados a esos valores extremos. El resultado se estructura según el formato oficial del reporte SISDIS, incluyendo el código habilitación de la IPS (tomado de los centros de atención), el canal de venta, el tipo de operación (ventas ''VN''), el tipo de transacción y la unidad de medida, para ser reportado al ente regulador de precios de medicamentos.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'PROCEDURE', @level1name = N'SP_ReportSISDISCircular015';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'PROCEDURE', @level1name = N'SP_ReportSISDISCircular015';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Genera el reporte SISDIS Circular 015 consolidando, por mes e IUM, las operaciones de venta (VN) y compra (CM) de medicamentos/insumos marcados para reporte SISMED, con cantidades, totales y valores mínimo/máximo junto al tercero asociado.', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_ReportSISDISCircular015';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El rango @dateStart/@dateEnd debe estar definido para acotar las operaciones consideradas.; Debe existir al menos un registro en ADCENATEN con CODIPSSEC para obtener el código de habilitación (Qualification) del prestador.; Solo se procesan productos con Inventory.InventoryProduct.SismedReport = 1.; Solo aplica a productos cuya ProductType.Class = 3 (categoría de producto reportable a SISMED).', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_ReportSISDISCircular015';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Channel siempre se reporta como ''INS'', Role como ''3'', TransactionType como ''03'' y UnitType como ''B'' (valores fijos exigidos por la Circular 015).; El campo Month se formatea siempre con dos dígitos (RIGHT(''0''+...,2)).; El Qualification corresponde a los 10 primeros caracteres de CODIPSSEC del primer registro de ADCENATEN y es el mismo para todas las filas del reporte.; Las ventas se consolidan a partir de tres orígenes mutuamente complementarios (InvoiceDetail/ServiceOrderDetail, DocumentInvoiceProductSales y BasicBilling) unidas vía UNION ALL.; Las compras solo consideran comprobantes de entrada confirmados (Status=2).; Los valores mínimo y máximo de venta/compra siempre se acompañan del tercero (NIT y tipo) y número de factura del documento donde se observaron.', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_ReportSISDISCircular015';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Reporte SISMED Circular 015; IUM (Identificador Único de Medicamento); CUM; Código de habilitación del prestador (CODIPSSEC); Ventas de medicamentos/insumos; Compras / entradas de inventario; Tercero (persona natural/jurídica, NIT); Proveedor; Factura de venta; Comprobante de entrada de mercancía; Tipo de operación (VN/CM)', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_ReportSISDISCircular015';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[INSERT] #tableExecution: Para ventas (TypeOperation=''VN''): inserta una fila por (Año, Mes, Producto, IUM) con SUM(Quantity), SUM(Subtotalvalue), MIN/MAX(UnitValue), tomando datos de Billing.Invoice (Status=1) en el rango de fechas, unificando tres fuentes: Billing.InvoiceDetail/ServiceOrderDetail (con GrandTotalSalesPrice>0), Inventory.DocumentInvoiceProductSales(Detail) y Billing.BasicBilling(Detail).; [INSERT] #tableExecution: Para compras (TypeOperation=''CM''): inserta una fila por (Año, Mes, Producto, CodeCUM, IUM) agregando Inventory.EntranceVoucher con Status=2 y DocumentDate en el rango, sumando cantidades y subtotales, y calculando MIN/MAX UnitValue.; [INSERT] #tableExecution: En cada fila se completan TypePersonMin/Max (''PN'' si ThirdParty.PersonType=1, sino ''NI''), NitMin/Max y InvoiceSaleMin/Max tomando el primer documento (TOP 1) cuyo precio unitario coincide con el mínimo o máximo del grupo.; [RETURN_RESULT] RESULT: Devuelve el contenido de #tableExecution con un consecutivo (ROW_NUMBER OVER ORDER BY Month) y campos fijos Channel=''INS'', Role=''3'', TransactionType=''03'', UnitType=''B''.', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_ReportSISDISCircular015';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si ProductType.Class = 3 y InventoryProduct.SismedReport = 1 → El producto se incluye en los cálculos de ventas y compras del reporte else El producto se excluye del reporte; si Invoice.Status = 1 y InvoiceDate BETWEEN @dateStart AND @dateEnd → La factura participa en la sección de ventas (VN) else La factura es ignorada; si EntranceVoucher.Status = 2 y DocumentDate BETWEEN @dateStart AND @dateEnd → El comprobante de entrada participa en la sección de compras (CM) else El comprobante es ignorado; si ServiceOrderDetail.GrandTotalSalesPrice > 0 → La línea de orden de servicio se incluye en la fuente de ventas vía InvoiceDetail else Se omite la línea (líneas sin valor no se reportan); si ThirdParty.PersonType = 1 → TypePerson se marca como ''PN'' (persona natural) else TypePerson se marca como ''NI'' (persona jurídica/NIT)', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_ReportSISDISCircular015';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'ADCENATEN; Billing.Invoice; Billing.InvoiceDetail; Billing.ServiceOrderDetail; Inventory.InventoryProduct; Inventory.InventorySupplie; Inventory.ProductType; Inventory.DocumentInvoiceProductSales; Inventory.DocumentInvoiceProductSalesDetail; Billing.BasicBilling; Billing.BasicBillingDetail; Common.ThirdParty; Inventory.EntranceVoucher; Inventory.EntranceVoucherDetail; Common.Supplier', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_ReportSISDISCircular015';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_ReportSISDISCircular015';
-- GO
