-- =============================================
-- Author:		Miguel Angel Fonseca Castro
-- Create date: 2018-08-03
-- Description:	Genera el informe de Deterioro de Inventarios
-- =============================================
CREATE PROCEDURE [Inventory].[SP_ReportDeterioration]
	@Year INT,
	@Month INT
AS
BEGIN
	SELECT 
		i.Code InventoryProductCode,
		i.Name InventoryProductName,
		i.Quantity,
		i.AverageCost,
		i.Quantity * i.AverageCost CostTotal,
		i.AverageValue,
		i.Quantity * i.AverageValue ValueTotal,
		i.AverageValue - i.AverageCost DiffAverage,
		i.Quantity * (i.AverageValue - i.AverageCost) DiffTotal,
		((i.AverageValue - i.AverageCost) / (i.AverageValue)) Utility,
		IIF((i.AverageValue - i.AverageCost) < 0, i.Quantity * (i.AverageValue - i.AverageCost), 0) Deterioration
	FROM
	(
		SELECT 
			ip.Code,
			ip.Name,
			ISNULL(so.Quantity, 0) + ISNULL(dips.Quantity, 0) Quantity,
			CASE 
				WHEN ISNULL(so.Quantity, 0) + ISNULL(dips.Quantity, 0) = 0 THEN 0
				ELSE ((ISNULL(so.Quantity, 0) * ISNULL(so.AverageCost, 0)) + (ISNULL(dips.Quantity, 0) * ISNULL(dips.AverageCost, 0))) / (ISNULL(so.Quantity, 0) + ISNULL(dips.Quantity, 0))
			END AverageCost,
			CASE 
				WHEN ISNULL(so.Quantity, 0) + ISNULL(dips.Quantity, 0) = 0 THEN 0
				ELSE ((ISNULL(so.Quantity, 0) * ISNULL(so.AverageValue, 0)) + (ISNULL(dips.Quantity, 0) * ISNULL(dips.AverageValue, 0))) / (ISNULL(so.Quantity, 0) + ISNULL(dips.Quantity, 0))
			END AverageValue
		FROM Inventory.InventoryProduct ip
		LEFT JOIN
		(
			SELECT 
				sod.ProductId, 
				SUM(sod.InvoicedQuantity) Quantity, 
				SUM(sod.InvoicedQuantity * sod.CostValue) / SUM(sod.InvoicedQuantity) AverageCost,
				SUM(sod.InvoicedQuantity * sod.RateManualSalePrice) / SUM(sod.InvoicedQuantity) AverageValue
			FROM Billing.Invoice i
			JOIN Billing.InvoiceDetail id ON i.Id = id.InvoiceId
			JOIN Billing.ServiceOrderDetail sod ON id.ServiceOrderDetailId = sod.Id
			WHERE i.Status = 1 AND YEAR(i.InvoiceDate) = @Year AND MONTH(i.InvoiceDate) = @Month
			GROUP BY sod.ProductId
		) so ON ip.Id = so.ProductId
		LEFT JOIN 
		(
			SELECT 
				dips.ProductId, 
				SUM(k.Quantity) Quantity, 
				SUM(k.Quantity * k.AverageCost) / SUM(k.Quantity) AverageCost,
				SUM(k.Quantity * dips.AverageValue) / SUM(k.Quantity) AverageValue
			FROM 
			(
				SELECT 
					dips.Id, 
					dipsd.ProductId,
					SUM(dipsd.Quantity * dipsd.SalePrice) / SUM(dipsd.Quantity) AverageValue
				FROM Billing.Invoice i
				JOIN Inventory.DocumentInvoiceProductSales dips ON i.Id = dips.InvoiceId
				JOIN Inventory.DocumentInvoiceProductSalesDetail dipsd ON dips.Id = dipsd.DocumentInvoiceProductSalesId
				WHERE i.Status = 1 AND YEAR(i.InvoiceDate) = @Year AND MONTH(i.InvoiceDate) = @Month
				GROUP BY dips.Id, dipsd.ProductId
			) dips
			JOIN
			(
				SELECT 
					k.EntityId, 
					k.ProductId, 
					SUM(k.Quantity) Quantity,
					SUM(k.Quantity * k.AverageCost) / SUM(k.Quantity) AverageCost
				FROM Inventory.Kardex k
				WHERE k.MovementType = 2 AND k.EntityName = 'DocumentInvoiceProductSales'
				GROUP BY k.EntityId, k.ProductId
			) k ON dips.Id = k.EntityId AND dips.ProductId = k.ProductId
			GROUP BY dips.ProductId
		) dips ON ip.Id = dips.ProductId
	) i
	WHERE i.Quantity <> 0
END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Genera el informe de deterioro de inventarios para un año y mes específicos, calculando por cada producto del catálogo la cantidad vendida, el costo promedio, el precio promedio de venta y la diferencia entre ambos (margen). Combina dos fuentes de ventas del período: las órdenes de servicio facturadas (Billing) y las facturas directas de venta de productos de inventario (farmacia/insumos), cruzando estas últimas con el kardex para obtener costos reales de salida. Identifica el deterioro de inventario como el valor negativo resultante cuando el costo promedio supera al precio promedio de venta, es decir, productos que se están vendiendo por debajo de su costo, lo cual es clave para contabilidad y control financiero del inventario.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'PROCEDURE', @level1name = N'SP_ReportDeterioration';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'PROCEDURE', @level1name = N'SP_ReportDeterioration';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Genera un informe mensual de deterioro de inventario comparando, por producto, el costo promedio contra el valor promedio de venta para detectar productos cuyo valor de venta es inferior al costo.', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_ReportDeterioration';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Se deben proporcionar año y mes para filtrar las facturas del período.; Las facturas consideradas deben tener Status = 1 (facturas activas/válidas).; Los movimientos de Kardex relevantes son aquellos con MovementType = 2 y EntityName = ''DocumentInvoiceProductSales'' (salidas asociadas a ventas de inventario).', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_ReportDeterioration';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'El deterioro reportado nunca es positivo: solo se computa cuando AverageValue < AverageCost; en otro caso es 0.; La cantidad total por producto consolida ventas vía órdenes de servicio (Billing.ServiceOrderDetail) y ventas directas de inventario (Inventory.DocumentInvoiceProductSales).; Solo se consideran facturas con Status = 1 dentro del año y mes especificados.; El costo promedio de las ventas directas proviene exclusivamente del Kardex con MovementType = 2 y EntityName = ''DocumentInvoiceProductSales''.; El promedio combinado se calcula como promedio ponderado por cantidad entre ambas fuentes de venta.', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_ReportDeterioration';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Deterioro de inventario; Costo promedio; Valor promedio de venta; Kardex; Facturación; Orden de servicio; Venta de productos de inventario; Utilidad', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_ReportDeterioration';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] ResultSet: Devuelve, por cada producto con cantidad vendida distinta de cero en el período, código, nombre, cantidad, costo promedio, valor promedio, totales, diferencia y deterioro (Quantity * (AverageValue - AverageCost) cuando es negativo, sino 0).', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_ReportDeterioration';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si ISNULL(so.Quantity,0) + ISNULL(dips.Quantity,0) = 0 → AverageCost y AverageValue del producto se fijan en 0 para evitar división por cero else Se calcula el promedio ponderado de costo y valor combinando ventas por orden de servicio (ServiceOrderDetail) y ventas directas de productos de inventario (DocumentInvoiceProductSales); si (i.AverageValue - i.AverageCost) < 0 → Se reporta como Deterioration el valor Quantity * (AverageValue - AverageCost) (pérdida) else Deterioration = 0 (no hay deterioro reportable cuando el valor de venta supera al costo); si i.Quantity <> 0 → El producto se incluye en el reporte final else Productos sin cantidad vendida en el mes son excluidos', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_ReportDeterioration';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Inventory.InventoryProduct; Billing.Invoice; Billing.InvoiceDetail; Billing.ServiceOrderDetail; Inventory.DocumentInvoiceProductSales; Inventory.DocumentInvoiceProductSalesDetail; Inventory.Kardex', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_ReportDeterioration';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_ReportDeterioration';
-- GO
