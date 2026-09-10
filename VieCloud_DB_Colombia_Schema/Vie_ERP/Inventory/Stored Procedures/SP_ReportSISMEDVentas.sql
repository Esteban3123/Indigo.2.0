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
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Genera el reporte SISMED de ventas de medicamentos e insumos (productos de clase 2) para el período indicado, consolidando las ventas facturadas tanto desde órdenes de servicio como desde documentos directos de venta de inventario. Para cada producto y mes, calcula el valor mínimo, máximo y total de ventas, la cantidad vendida, e identifica los números de factura donde ocurrió el precio mínimo y máximo. El resultado está diseñado para ser reportado al sistema de vigilancia de precios de medicamentos SISMED del Ministerio de Salud de Colombia, filtrando productos no NPT, con facturas activas (estado 1) dentro del rango de fechas recibido como parámetro (@dateStart, @dateEnd).', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'PROCEDURE', @level1name = N'SP_ReportSISMEDVentas';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'PROCEDURE', @level1name = N'SP_ReportSISMEDVentas';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Genera el reporte mensual SISMED de ventas de medicamentos consolidando facturación clínica e inventario por producto (CUM/IUM), con valores mínimo, máximo, total y facturas asociadas en un rango de fechas.', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_ReportSISMEDVentas';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Se requiere un rango de fechas (@dateStart, @dateEnd) para filtrar Invoice.InvoiceDate; Solo considera facturas con Status = 1 (activas/emitidas); Solo considera productos cuyo ProductType.Class = 2 (medicamentos); Excluye productos marcados como ProductNPT = 1 en Inventory.ATC (no incluidos en reporte SISMED); En ServiceOrderDetail solo se consideran ítems con GrandTotalSalesPrice > 0', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_ReportSISMEDVentas';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'El reporte siempre clasifica cada fila como Canal=''INS'' (institucional) y Comercializa=''SI'' fijos; Mes se formatea siempre a 2 dígitos con cero a la izquierda; Solo se reportan medicamentos (ProductType.Class=2) no marcados como NPT; La factura mínima/máxima reportada pertenece al mismo año y mes de la agregación; Las ventas de servicios y las ventas directas de inventario se consolidan vía UNION ALL bajo el mismo producto (CUM/IUM); CUM e IUM nulos se reemplazan por cadena vacía', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_ReportSISMEDVentas';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'SISMED (Sistema de Información de Precios de Medicamentos); CUM (Código Único de Medicamento); IUM (Identificador Único de Medicamento); Canal institucional (INS); Clasificación ATC; Producto NPT; Factura de venta; Medicamento', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_ReportSISMEDVentas';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[INSERT] #tableExecution: Por cada combinación (Año, Mes, ProductId, Class, CodeCUM, IUM) inserta una fila con Canal=''INS'', Comercializa=''SI'', valores mínimo/máximo/total/cantidad agregados y los números de factura correspondientes a la venta de menor y mayor valor unitario del mes; [RETURN_RESULT] RESULTSET: Devuelve el contenido de #tableExecution con un consecutivo (ROW_NUMBER ordenado por Mes, CUM) y las columnas del reporte SISMED', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_ReportSISMEDVentas';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si Origen de la venta: Billing.Invoice + ServiceOrderDetail (facturación de servicios clínicos) → Toma SubTotalSalesPrice como valor unitario, InvoicedQuantity como cantidad y GrandTotalSalesPrice como subtotal; si Origen de la venta: Inventory.DocumentInvoiceProductSales (ventas directas de farmacia/inventario) → Toma SalePrice como valor unitario, Quantity como cantidad y TotalValue como subtotal', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_ReportSISMEDVentas';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Billing.Invoice; Billing.InvoiceDetail; Billing.ServiceOrderDetail; Inventory.InventoryProduct; Inventory.ATC; Inventory.ProductType; Inventory.DocumentInvoiceProductSales; Inventory.DocumentInvoiceProductSalesDetail', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_ReportSISMEDVentas';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_ReportSISMEDVentas';
-- GO
